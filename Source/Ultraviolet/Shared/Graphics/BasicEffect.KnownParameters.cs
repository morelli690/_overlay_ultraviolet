namespace Ultraviolet.Graphics
{
    partial class BasicEffect
    {
        /// <summary>
        /// Represents the list of effect parameters which are required to implement <see cref="BasicEffect"/>.
        /// </summary>
        protected enum KnownParameter
        {
            /// <summary>
            /// The effect's ambient light color.
            /// </summary>
            AmbientLightColor,

            /// <summary>
            /// The effect's diffuse color.
            /// </summary>
            DiffuseColor,

            /// <summary>
            /// THe effect's emissive color.
            /// </summary>
            EmissiveColor,

            /// <summary>
            /// The effect's specular color.
            /// </summary>
            SpecularColor,

            /// <summary>
            /// The effect's fog color.
            /// </summary>
            FogColor,

            /// <summary>
            /// The effect's alpha.
            /// </summary>
            Alpha,

            /// <summary>
            /// The starting distance of the effect's fog.
            /// </summary>
            FogStart,

            /// <summary>
            /// The ending distance of the effect's fog.
            /// </summary>
            FogEnd,

            /// <summary>
            /// The effect's specular power.
            /// </summary>
            SpecularPower,

            /// <summary>
            /// The effect's world matrix.
            /// </summary>
            World,

            /// <summary>
            /// The effect's view matrix.
            /// </summary>
            View,

            /// <summary>
            /// The effect's projection matrix.
            /// </summary>
            Projection,

            /// <summary>
            /// The effect's texture.
            /// </summary>
            Texture,

            /// <summary>
            /// A value indicating whether fog is enabled.
            /// </summary>
            FogEnabled,

            /// <summary>
            /// A value indicating whether sRGB color is enabled for this effect.
            /// </summary>
            SrgbColor,

            /// <summary>
            /// The first light's direction.
            /// </summary>
            Light0Direction,

            /// <summary>
            /// The first light's diffuse color.
            /// </summary>
            Light0DiffuseColor,

            /// <summary>
            /// The first light's specular color.
            /// </summary>
            Light0SpecularColor,

            /// <summary>
            /// The second light's direction.
            /// </summary>
            Light1Direction,

            /// <summary>
            /// The second light's diffuse color.
            /// </summary>
            Light1DiffuseColor,

            /// <summary>
            /// The second light's specular color.
            /// </summary>
            Light1SpecularColor,

            /// <summary>
            /// The third light's direction.
            /// </summary>
            Light2Direction,

            /// <summary>
            /// The third light's diffuse color.
            /// </summary>
            Light2DiffuseColor,

            /// <summary>
            /// The third light's specular color.
            /// </summary>
            Light2SpecularColor,
        }
    }
}
