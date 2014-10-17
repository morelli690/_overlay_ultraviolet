﻿using TwistedLogik.Nucleus;

namespace TwistedLogik.Ultraviolet.Graphics.Graphics2D
{
    /// <summary>
    /// Represents a factory method which constructs instances of the SpriteBatchEffect class.
    /// </summary>
    /// <param name="uv">The Ultraviolet context.</param>
    /// <returns>The instance of SpriteBatchEffect that was created.</returns>
    public delegate SpriteBatchEffect SpriteBatchEffectFactory(UltravioletContext uv);

    /// <summary>
    /// Represents the effect used to render sprites.
    /// </summary>
    public abstract class SpriteBatchEffect : Effect, ISpriteBatchEffect
    {
        /// <summary>
        /// Initializes a new instance of the SpriteBatchEffect class.
        /// </summary>
        /// <param name="impl">The effect implementation.</param>
        protected SpriteBatchEffect(EffectImplementation impl)
            : base(impl)
        {
            this.epMatrixTransform = Parameters["MatrixTransform"];
        }

        /// <summary>
        /// Creates a new instance of the SpriteBatchEffect class.
        /// </summary>
        /// <returns>The instance of SpriteBatchEffect that was created.</returns>
        public static SpriteBatchEffect Create()
        {
            var uv = UltravioletContext.DemandCurrent();
            return uv.GetFactoryMethod<SpriteBatchEffectFactory>()(uv);
        }

        /// <summary>
        /// Gets or sets the effect's transformation matrix.
        /// </summary>
        public Matrix MatrixTransform
        {
            get
            {
                Contract.EnsureNotDisposed(this, Disposed);

                return epMatrixTransform.GetValueMatrix();
            }
            set
            {
                Contract.EnsureNotDisposed(this, Disposed);

                epMatrixTransform.SetValue(value);
            }
        }

        // Cached effect parameters.
        private readonly EffectParameter epMatrixTransform;
    }
}
