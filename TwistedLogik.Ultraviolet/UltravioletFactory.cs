﻿using System;
using System.Collections.Generic;
using System.Security;
using TwistedLogik.Nucleus;

namespace TwistedLogik.Ultraviolet
{
    /// <summary>
    /// Represents an Ultraviolet context's object factory.
    /// </summary>
    [SecuritySafeCritical]
    public sealed class UltravioletFactory
    {
        /// <summary>
        /// Gets the default factory method of the specified delegate type.
        /// </summary>
        /// <returns>The default factory method of the specified type.</returns>
        public T GetFactoryMethod<T>() where T : class
        {
            var key = typeof(T).TypeHandle.Value.ToInt64();
            var value = default(Delegate);

            defaultFactoryMethods.TryGetValue(key, out value);

            if (value == null)
                throw new InvalidOperationException(UltravioletStrings.MissingFactoryMethod.Format(typeof(T).FullName));

            var typed = value as T;
            if (typed == null)
                throw new InvalidCastException();

            return typed;
        }

        /// <summary>
        /// Gets a named factory method of the specified delegate type.
        /// </summary>
        /// <param name="name">The name of the factory method to retrieve.</param>
        /// <returns>The default factory method of the specified type.</returns>
        public T GetFactoryMethod<T>(String name) where T : class
        {
            Contract.RequireNotEmpty(name, "name");
            
            var key = typeof(T).TypeHandle.Value.ToInt64();
            var registry = default(Dictionary<String, Delegate>);
            if (!namedFactoryMethods.TryGetValue(key, out registry))
                throw new InvalidOperationException(UltravioletStrings.NoNamedFactoryMethods.Format(typeof(T).FullName));
            
            var value = default(Delegate);
            registry.TryGetValue(name, out value);

            if (value == null)
                throw new InvalidOperationException(UltravioletStrings.MissingNamedFactoryMethod.Format(name));
            
            var typed = value as T;
            if (typed == null)
                throw new InvalidCastException();

            return typed;
        }

        /// <summary>
        /// Registers the default factory method of the specified delegate type.
        /// </summary>
        /// <param name="factory">A delegate representing the factory method to register.</param>
        public void SetFactoryMethod<T>(T factory) where T : class
        {
            Contract.Require(factory, "factory");

            var key = typeof(T).TypeHandle.Value.ToInt64();
            var del = factory as Delegate;
            if (del == null)
                throw new InvalidOperationException(UltravioletStrings.FactoryMethodInvalidDelegate);
            
            if (defaultFactoryMethods.ContainsKey(key))
                throw new InvalidOperationException(UltravioletStrings.FactoryMethodAlreadyRegistered);
            
            defaultFactoryMethods[key] = del;
        }

        /// <summary>
        /// Registers a named factory method of the specified delegate type.
        /// </summary>
        /// <param name="name">The name of the factory method to register.</param>
        /// <param name="factory">A delegate representing the factory method to register.</param>
        public void SetFactoryMethod<T>(String name, T factory) where T : class
        {
            Contract.RequireNotEmpty(name, "name");
            Contract.Require(factory, "factory");

            var key = typeof(T).TypeHandle.Value.ToInt64();
            var registry = default(Dictionary<String, Delegate>);
            if (!namedFactoryMethods.TryGetValue(key, out registry))
                namedFactoryMethods[key] = registry = new Dictionary<String, Delegate>();
            
            var del = factory as Delegate;
            if (del == null)
                throw new InvalidOperationException(UltravioletStrings.FactoryMethodInvalidDelegate);
            
            if (registry.ContainsKey(name))
                throw new InvalidOperationException(UltravioletStrings.NamedFactoryMethodAlreadyRegistered);

            registry[name] = del;
        }

        // The factory method registry.
        private readonly Dictionary<Int64, Delegate> defaultFactoryMethods = 
            new Dictionary<Int64, Delegate>();
        private readonly Dictionary<Int64, Dictionary<String, Delegate>> namedFactoryMethods = 
            new Dictionary<Int64, Dictionary<String, Delegate>>();
    }
}
