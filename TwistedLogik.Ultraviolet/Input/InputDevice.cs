﻿using System;

namespace TwistedLogik.Ultraviolet.Input
{
    /// <summary>
    /// Represents any input device.
    /// </summary>
    public abstract class InputDevice<T> : UltravioletResource
    {
        /// <summary>
        /// Initializes a new instance of the InputDevice class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        internal InputDevice(UltravioletContext uv)
            : base(uv)
        {

        }

        /// <summary>
        /// Updates the device's state.
        /// </summary>
        /// <param name="time">Time elapsed since the last call to Update.</param>
        public abstract void Update(UltravioletTime time);

        /// <summary>
        /// Gets a value indicating whether the specified button is currently down.
        /// </summary>
        /// <param name="button">The button to evaluate.</param>
        /// <returns>true if the button is down; otherwise, false.</returns>
        public abstract Boolean IsButtonDown(T button);

        /// <summary>
        /// Gets a value indicating whether the specified button is currently up.
        /// </summary>
        /// <param name="button">The button to evaluate.</param>
        /// <returns>true if the button is up; otherwise, false.</returns>
        public abstract Boolean IsButtonUp(T button);

        /// <summary>
        /// Gets a value indicating whether the specified button is currently pressed.
        /// </summary>
        /// <param name="button">The button to evaluate.</param>
        /// <param name="ignoreRepeats">A value indicating whether to ignore repeated button press events on devices which support them.</param>
        /// <returns>true if the button is pressed; otherwise, false.</returns>        
        public abstract Boolean IsButtonPressed(T button, Boolean ignoreRepeats = true);

        /// <summary>
        /// Gets a value indicating whether the specified button is currently released.
        /// </summary>
        /// <param name="button">The button to evaluate.</param>
        /// <returns>true if the button is released; otherwise, false.</returns>
        public abstract Boolean IsButtonReleased(T button);

        /// <summary>
        /// Gets the current state of the specified button.
        /// </summary>
        /// <param name="button">The button for which to retrieve a state.</param>
        /// <returns>The current state of the specified button.</returns>
        public abstract ButtonState GetButtonState(T button);
    }
}
