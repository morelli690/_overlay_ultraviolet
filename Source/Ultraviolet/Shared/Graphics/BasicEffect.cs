using System;

namespace Ultraviolet.Graphics
{
    /// <summary>
    /// Represents a factory method which constructs instances of the <see cref="BasicEffect"/> class.
    /// </summary>
    /// <param name="uv">The Ultraviolet context.</param>
    /// <returns>The instance of <see cref="BasicEffect"/> that was created.</returns>
    public delegate BasicEffect BasicEffectFactory(UltravioletContext uv);

    /// <summary>
    /// Represents a basic rendering effect.
    /// </summary>
    public abstract partial class BasicEffect : Effect, IEffectMatrices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEffect"/> class.
        /// </summary>
        /// <param name="impl">The <see cref="EffectImplementation"/> that implements this effect.</param>
        protected BasicEffect(EffectImplementation impl)
            : base(impl)
        {
            this.epAmbientLightColor = GetKnownParameter(KnownParameter.AmbientLightColor);
            //this.epAmbientLightColor.SetValue(Color.White);

            this.epDiffuseColor = GetKnownParameter(KnownParameter.DiffuseColor);
            this.epDiffuseColor.SetValue(Color.White);

            this.epEmissiveColor = GetKnownParameter(KnownParameter.EmissiveColor);
            //this.epEmissiveColor.SetValue(Color.Black);

            this.epSpecularColor = GetKnownParameter(KnownParameter.SpecularColor);
            //this.epSpecularColor.SetValue(Color.White);

            this.epFogColor = GetKnownParameter(KnownParameter.FogColor);
            //this.epFogColor.SetValue(Color.Transparent);

            this.epAlpha = GetKnownParameter(KnownParameter.Alpha);
            //this.epAlpha.SetValue(1f);

            this.epSpecularPower = GetKnownParameter(KnownParameter.SpecularPower);
            //this.epSpecularPower.SetValue(16f);

            this.epFogStart = GetKnownParameter(KnownParameter.FogStart);
            this.epFogEnd = GetKnownParameter(KnownParameter.FogEnd);

            this.epWorld = GetKnownParameter(KnownParameter.World);
            this.epWorld.SetValue(Matrix.Identity);

            this.epView = GetKnownParameter(KnownParameter.View);
            this.epView.SetValue(Matrix.Identity);

            this.epProjection = GetKnownParameter(KnownParameter.Projection);
            this.epProjection.SetValue(Matrix.Identity);

            this.epFogEnabled = GetKnownParameter(KnownParameter.FogEnabled);
            this.epSrgbColor = GetKnownParameter(KnownParameter.SrgbColor);
            this.epTexture = GetKnownParameter(KnownParameter.Texture);

            var epLight0Direction = GetKnownParameter(KnownParameter.Light0Direction);
            var epLight0DiffuseColor = GetKnownParameter(KnownParameter.Light0DiffuseColor);
            var epLight0SpecularColor = GetKnownParameter(KnownParameter.Light0SpecularColor);
            this.DirectionalLight0 = new DirectionalLight(epLight0Direction, epLight0DiffuseColor, epLight0SpecularColor);

            var epLight1Direction = GetKnownParameter(KnownParameter.Light1Direction);
            var epLight1DiffuseColor = GetKnownParameter(KnownParameter.Light1DiffuseColor);
            var epLight1SpecularColor = GetKnownParameter(KnownParameter.Light1SpecularColor);
            this.DirectionalLight1 = new DirectionalLight(epLight1Direction, epLight1DiffuseColor, epLight1SpecularColor);

            var epLight2Direction = GetKnownParameter(KnownParameter.Light2Direction);
            var epLight2DiffuseColor = GetKnownParameter(KnownParameter.Light2DiffuseColor);
            var epLight2SpecularColor = GetKnownParameter(KnownParameter.Light2SpecularColor);
            this.DirectionalLight2 = new DirectionalLight(epLight2Direction, epLight2DiffuseColor, epLight2SpecularColor);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BasicEffect"/> class.
        /// </summary>
        /// <returns>The instance of <see cref="BasicEffect"/> that was created.</returns>
        public static BasicEffect Create()
        {
            var uv = UltravioletContext.DemandCurrent();
            return uv.GetFactoryMethod<BasicEffectFactory>()(uv);
        }

        /// <summary>
        /// Enables default lighting for this effect.
        /// </summary>
        public void EnableDefaultLighting()
        {
            // Enable lightning.
            LightingEnabled = true;

            // Set up the key light.
            DirectionalLight0.Direction = new Vector3(-0.5265408f, -0.5735765f, -0.6275069f);
            DirectionalLight0.DiffuseColor = new Color(1, 0.9607844f, 0.8078432f);
            DirectionalLight0.SpecularColor = new Color(1, 0.9607844f, 0.8078432f);
            DirectionalLight0.Enabled = true;

            // Set up the fill light.
            DirectionalLight0.Direction = new Vector3(0.7198464f, 0.3420201f, 0.6040227f);
            DirectionalLight0.DiffuseColor = new Color(0.9647059f, 0.7607844f, 0.4078432f);
            DirectionalLight0.SpecularColor = new Color(0f, 0f, 0f);
            DirectionalLight0.Enabled = true;

            // Set up the back light.
            DirectionalLight0.Direction = new Vector3(0.4545195f, -0.7660444f, 0.4545195f);
            DirectionalLight0.DiffuseColor = new Color(0.3231373f, 0.3607844f, 0.3937255f);
            DirectionalLight0.SpecularColor = new Color(0.3231373f, 0.3607844f, 0.3937255f);
            DirectionalLight0.Enabled = true;

            // Set up the ambient light.
            AmbientLightColor = new Color(0.05333332f, 0.09882354f, 0.1819608f);
        }

        /// <summary>
        /// Gets the first directional light for this effect.
        /// </summary>
        public DirectionalLight DirectionalLight0 { get; }

        /// <summary>
        /// Gets the second directional light for this effect.
        /// </summary>
        public DirectionalLight DirectionalLight1 { get; }

        /// <summary>
        /// Gets the third directional light for this effect.
        /// </summary>
        public DirectionalLight DirectionalLight2 { get; }

        /// <summary>
        /// Gets or sets the effect's ambient light color.
        /// </summary>
        public Color AmbientLightColor
        {
            get => epAmbientLightColor.GetValueColor();
            set => epAmbientLightColor.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's diffuse color.
        /// </summary>
        public Color DiffuseColor
        {
            get => epDiffuseColor.GetValueColor();
            set => epDiffuseColor.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's emissive color.
        /// </summary>
        public Color EmissiveColor
        {
            get => epEmissiveColor.GetValueColor();
            set => epEmissiveColor.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's specular color.
        /// </summary>
        public Color SpecularColor
        {
            get => epSpecularColor.GetValueColor();
            set => epSpecularColor.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's fog color.
        /// </summary>
        public Color FogColor
        {
            get => epFogColor.GetValueColor();
            set => epFogColor.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the alpha of this material.
        /// </summary>
        public Single Alpha
        {
            get => epAlpha.GetValueSingle();
            set => epAlpha.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's specular power.
        /// </summary>
        public Single SpecularPower
        {
            get => epSpecularPower.GetValueSingle();
            set => epSpecularPower.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's starting fog distance.
        /// </summary>
        public Single FogStart
        {
            get => epFogStart.GetValueSingle();
            set => epFogStart.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's ending fog distance.
        /// </summary>
        public Single FogEnd
        {
            get => epFogEnd.GetValueSingle();
            set => epFogEnd.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's world matrix.
        /// </summary>
        public Matrix World
        {
            get => epWorld.GetValueMatrix();
            set => epWorld.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's view matrix.
        /// </summary>
        public Matrix View
        {
            get => epView.GetValueMatrix();
            set => epView.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the effect's projection matrix.
        /// </summary>
        public Matrix Projection
        {
            get => epProjection.GetValueMatrix();
            set => epProjection.SetValue(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether lighting is enabled.
        /// </summary>
        public Boolean LightingEnabled
        {
            get => lightingEnabled;
            set
            {
                if (lightingEnabled != value)
                {
                    lightingEnabled = value;
                    OnLightingEnabledChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether vertex colors are enabled for this effect.
        /// </summary>
        public Boolean VertexColorEnabled
        {
            get => vertexColorEnabled;
            set
            {
                if (vertexColorEnabled != value)
                {
                    vertexColorEnabled = value;
                    OnVertexColorEnabledChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether textures are enabled for this effect.
        /// </summary>
        public Boolean TextureEnabled
        {
            get => textureEnabled;
            set
            {
                if (textureEnabled != value)
                {
                    textureEnabled = value;
                    OnTextureEnabledChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether fog is enabled by this effect.
        /// </summary>
        public Boolean FogEnabled
        {
            get => epFogEnabled.GetValueBoolean();
            set => epFogEnabled.SetValue(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the colors used by this effect should be
        /// converted from the sRGB color space to the linear color space in the vertex shader.
        /// </summary>
        public Boolean SrgbColor
        {
            get => epSrgbColor?.GetValueBoolean() ?? false;
            set => epSrgbColor?.SetValue(value);
        }

        /// <summary>
        /// Gets or sets the texture that is applied to geometry rendered by this effect.
        /// </summary>
        public Texture2D Texture
        {
            get => epTexture.GetValueTexture2D();
            set => epTexture.SetValue(value);
        }

        /// <summary>
        /// Retrieves the <see cref="EffectParameter"/> which corresponds to the specified <see cref="KnownParameter"/> value.
        /// </summary>
        /// <param name="parameter">The <see cref="KnownParameter"/> value for which to retrieve an effect parameter.</param>
        /// <returns>The <see cref="EffectParameter"/> instance which implements the specified parameter.</returns>
        protected abstract EffectParameter GetKnownParameter(KnownParameter parameter);

        /// <summary>
        /// Occurs when the value of the <see cref="LightingEnabled"/> property changes.
        /// </summary>
        protected virtual void OnLightingEnabledChanged()
        {

        }

        /// <summary>
        /// Occurs when the value of the <see cref="VertexColorEnabled"/> property changes.
        /// </summary>
        protected virtual void OnVertexColorEnabledChanged()
        {

        }

        /// <summary>
        /// Occurs when the value of the <see cref="TextureEnabled"/> property changes.
        /// </summary>
        protected virtual void OnTextureEnabledChanged()
        {

        }

        /// <summary>
        /// Occurs when the value of the <see cref="SrgbColor"/> property changes.
        /// </summary>
        protected virtual void OnSrgbColorChanged()
        {

        }        

        // Cached effect parameters.
        private readonly EffectParameter epAmbientLightColor;
        private readonly EffectParameter epDiffuseColor;
        private readonly EffectParameter epEmissiveColor;
        private readonly EffectParameter epSpecularColor;
        private readonly EffectParameter epFogColor;
        private readonly EffectParameter epAlpha;
        private readonly EffectParameter epSpecularPower;
        private readonly EffectParameter epFogStart;
        private readonly EffectParameter epFogEnd;
        private readonly EffectParameter epWorld;
        private readonly EffectParameter epView;
        private readonly EffectParameter epProjection;
        private readonly EffectParameter epFogEnabled;
        private readonly EffectParameter epSrgbColor;
        private readonly EffectParameter epTexture;

        // Property values.
        private Boolean lightingEnabled;
        private Boolean vertexColorEnabled;
        private Boolean textureEnabled;
    }
}
