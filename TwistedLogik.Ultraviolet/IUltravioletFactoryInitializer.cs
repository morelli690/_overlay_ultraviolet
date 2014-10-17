﻿
namespace TwistedLogik.Ultraviolet
{
    /// <summary>
    /// Represents an object which injects factory methods into the Ultraviolet context.
    /// </summary>
    public interface IUltravioletFactoryInitializer
    {
        /// <summary>
        /// Initializes the specified factory.
        /// </summary>
        /// <param name="owner">The Ultraviolet context that owns the initializer.</param>
        /// <param name="factory">The factory to initialize.</param>
        void Initialize(UltravioletContext owner, UltravioletFactory factory);
    }
}
