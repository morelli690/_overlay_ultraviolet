﻿using System;
using System.Collections.Generic;
using ImGuiNET;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace Ultraviolet.ImGuiViewProvider
{
    /// <summary>
    /// Represents a buffer for ImGui geometry.
    /// </summary>
    public sealed unsafe class ImGuiGeometryBuffer : UltravioletResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImGuiGeometryBuffer"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="view">The view which owns the geometry buffer.</param>
        public ImGuiGeometryBuffer(UltravioletContext uv, ImGuiView view)
            : base(uv)
        {
            this.effect = SpriteBatchEffect.Create();
            this.view = view;
        }

        /// <summary>
        /// Draws the specified data.
        /// </summary>
        /// <param name="drawDataPtr">A pointer to the ImGui data to draw.</param>
        public void Draw(ref ImDrawDataPtr drawDataPtr)
        {
            if (drawDataPtr.CmdListsCount == 0)
                return;

            EnsureBuffers(ref drawDataPtr);
            PopulateBuffers(ref drawDataPtr);
            DrawBuffers(ref drawDataPtr);            
        }

        /// <inheritdoc/>
        protected override void Dispose(Boolean disposing)
        {
            if (geometryStream != null)
            {
                geometryStream.Dispose();
                geometryStream = null;
            }
            if (vertexBuffer != null)
            {
                vertexBuffer.Dispose();
                vertexBuffer = null;
            }
            if (indexBuffer != null)
            {
                indexBuffer.Dispose();
                indexBuffer = null;
            }
            if (effect != null)
            {
                effect.Dispose();
                effect = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Ensures that the geometry buffers exist and have sufficient size.
        /// </summary>
        private void EnsureBuffers(ref ImDrawDataPtr drawDataPtr)
        {
            var dirty = false;
            var vtxCount = drawDataPtr.TotalVtxCount;
            var idxCount = drawDataPtr.TotalIdxCount;

            if (vertexBuffer == null || vertexBuffer.VertexCount < vtxCount)
            {
                if (vertexBuffer != null)
                    vertexBuffer.Dispose();

                vertexBuffer = DynamicVertexBuffer.Create(ImGuiVertex.VertexDeclaration, vtxCount);
                dirty = true;
            }

            if (indexBuffer == null || indexBuffer.IndexCount < idxCount)
            {
                if (indexBuffer != null)
                    indexBuffer.Dispose();

                indexBuffer = DynamicIndexBuffer.Create(IndexBufferElementType.Int16, idxCount);
                dirty = true;
            }

            if (geometryStream == null || dirty)
            {
                this.geometryStream = GeometryStream.Create();
                this.geometryStream.Attach(this.vertexBuffer);
                this.geometryStream.Attach(this.indexBuffer);
            }
        }

        /// <summary>
        /// Populates the vertex and index buffer with data.
        /// </summary>
        private void PopulateBuffers(ref ImDrawDataPtr drawDataPtr)
        {
            var vtxOffset = 0;
            var idxOffset = 0;

            var setDataOptions = SetDataOptions.Discard;
            for (var i = 0; i < drawDataPtr.CmdListsCount; i++)
            {
                var cmdList = drawDataPtr.CmdListsRange[i];

                this.vertexBuffer.SetRawData(cmdList.VtxBuffer.Data, 0, vtxOffset * sizeof(ImGuiVertex),
                    cmdList.VtxBuffer.Size * sizeof(ImGuiVertex), setDataOptions);
                vtxOffset += cmdList.VtxBuffer.Size;

                this.indexBuffer.SetRawData(cmdList.IdxBuffer.Data, 0, idxOffset * sizeof(UInt16),
                    cmdList.IdxBuffer.Size * sizeof(UInt16), setDataOptions);
                idxOffset += cmdList.IdxBuffer.Size;

                setDataOptions = SetDataOptions.NoOverwrite;
            }
        }

        /// <summary>
        /// Draws the contents of the vertex and index buffer.
        /// </summary>
        private void DrawBuffers(ref ImDrawDataPtr drawDataPtr)
        {
            var gfx = Ultraviolet.GetGraphics();
            gfx.SetGeometryStream(geometryStream);
            gfx.SetBlendState(BlendState.NonPremultiplied);
            gfx.SetDepthStencilState(DepthStencilState.None);
            gfx.SetRasterizerState(RasterizerState.CullNone);
            gfx.SetSamplerState(0, SamplerState.LinearClamp);

            var vtxOffset = 0;
            var idxOffset = 0;

            for (var i = 0; i < drawDataPtr.CmdListsCount; i++)
            {
                var cmdList = drawDataPtr.CmdListsRange[i];
                for (int j = 0; j < cmdList.CmdBuffer.Size; j++)
                {
                    var cmd = cmdList.CmdBuffer[j];

                    gfx.SetScissorRectangle(
                        (Int32)cmd.ClipRect.X,
                        (Int32)cmd.ClipRect.Y,
                        (Int32)(cmd.ClipRect.Z - cmd.ClipRect.X),
                        (Int32)(cmd.ClipRect.W - cmd.ClipRect.Y));

                    var texture = view.Textures.Get((Int32)cmd.TextureId);
                    if (texture != null)
                    {
                        effect.SrgbColor = gfx.Capabilities.SrgbEncodingEnabled;
                        effect.Texture = texture;
                        effect.TextureSize = new Size2(1, 1);
                        effect.MatrixTransform = Matrix.CreateOrthographicOffCenter(0,
                            ImGui.GetIO().DisplaySize.X,
                            ImGui.GetIO().DisplaySize.Y, 0, 0, 1);

                        foreach (var pass in effect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            gfx.DrawIndexedPrimitives(PrimitiveType.TriangleList, vtxOffset, idxOffset, (Int32)cmd.ElemCount / 3);
                        }
                    }

                    idxOffset += (Int32)cmd.ElemCount;
                }
                vtxOffset += cmdList.VtxBuffer.Size * vertexBuffer.VertexDeclaration.VertexStride;
            }

            Ultraviolet.GetGraphics().SetGeometryStream(null);
        }

        // The view which owns the geometry buffer.
        private readonly ImGuiView view;

        // Geometry resources.
        private GeometryStream geometryStream;
        private DynamicVertexBuffer vertexBuffer;
        private DynamicIndexBuffer indexBuffer;
        private SpriteBatchEffect effect;
    }
}
