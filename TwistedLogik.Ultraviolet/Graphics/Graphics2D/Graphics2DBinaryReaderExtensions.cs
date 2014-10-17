﻿using System;
using System.IO;
using TwistedLogik.Nucleus;
using TwistedLogik.Ultraviolet.Content;

namespace TwistedLogik.Ultraviolet.Graphics.Graphics2D
{
    /// <summary>
    /// Contains extension methods for the BinaryReader class.
    /// </summary>
    public static class Graphics2DBinaryReaderExtensions
    {
        /// <summary>
        /// Reads a sprite animation identifier from the stream using the content manifest registry
        /// belonging to the current Ultraviolet context.
        /// </summary>
        /// <param name="reader">The binary reader from which to read the sprite animation identifier.</param>
        /// <returns>The sprite animation identifier that was read from the stream.</returns>
        public static SpriteAnimationID ReadSpriteAnimationID(this BinaryReader reader)
        {
            Contract.Require(reader, "reader");

            return ReadSpriteAnimationID(reader, UltravioletContext.DemandCurrent().GetContent().Manifests);
        }

        /// <summary>
        /// Reads a nullable asset identifier from the stream using the content manifest registry
        /// belonging to the current Ultraviolet context.
        /// </summary>
        /// <param name="reader">The binary reader from which to read the sprite animation identifier.</param>
        /// <returns>The sprite animation identifier that was read from the stream.</returns>
        public static SpriteAnimationID? ReadNullableSpriteAnimationID(this BinaryReader reader)
        {
            Contract.Require(reader, "reader");

            return ReadNullableSpriteAnimationID(reader, UltravioletContext.DemandCurrent().GetContent().Manifests);
        }

        /// <summary>
        /// Reads a sprite animation identifier from the stream.
        /// </summary>
        /// <param name="reader">The binary reader from which to read the sprite animation identifier.</param>
        /// <param name="manifests">The registry that contains the application's loaded manifests.</param>
        /// <returns>The sprite animation identifier that was read from the stream.</returns>
        public static SpriteAnimationID ReadSpriteAnimationID(this BinaryReader reader, ContentManifestRegistry manifests)
        {
            Contract.Require(reader, "reader");
            Contract.Require(manifests, "manifests");

            var valid = reader.ReadBoolean();
            if (valid)
            {
                var spriteAssetID = reader.ReadAssetID();
                var animationName = reader.ReadString();
                var animationIndex = reader.ReadInt32();

                return String.IsNullOrEmpty(animationName) ? 
                    new SpriteAnimationID(spriteAssetID, animationIndex) :
                    new SpriteAnimationID(spriteAssetID, animationName);
            }
            return SpriteAnimationID.Invalid;
        }

        /// <summary>
        /// Reads a nullable asset identifier from the stream.
        /// </summary>
        /// <param name="reader">The binary reader from which to read the sprite animation identifier.</param>
        /// <param name="manifests">The registry that contains the application's loaded manifests.</param>
        /// <returns>The sprite animation identifier that was read from the stream.</returns>
        public static SpriteAnimationID? ReadNullableSpriteAnimationID(this BinaryReader reader, ContentManifestRegistry manifests)
        {
            Contract.Require(reader, "reader");
            Contract.Require(manifests, "manifests");

            var hasValue = reader.ReadBoolean();
            if (hasValue)
            {
                return reader.ReadSpriteAnimationID(manifests);
            }
            return null;
        }
    }
}
