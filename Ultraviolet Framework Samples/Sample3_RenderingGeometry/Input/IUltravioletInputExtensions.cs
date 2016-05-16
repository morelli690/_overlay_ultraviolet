﻿using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Input;

namespace UltravioletSample.Sample3_RenderingGeometry.Input
{
    public static class IUltravioletInputExtensions
    {
        public static GameInputActions GetActions(this IUltravioletInput @this)
        {
            return actions;
        }

        private static readonly UltravioletSingleton<GameInputActions> actions = 
            InputActionCollection.CreateSingleton<GameInputActions>();
    }
}
