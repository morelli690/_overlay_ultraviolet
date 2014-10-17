﻿using System;
using System.Xml.Linq;
using TwistedLogik.Nucleus;

namespace TwistedLogik.Ultraviolet
{
    /// <summary>
    /// Represents an Ultraviolet application's internal settings.
    /// </summary>
    internal class UltravioletApplicationSettings
    {
        /// <summary>
        /// Initializes a new instance of the UltravioletApplicationSettings class.
        /// </summary>
        private UltravioletApplicationSettings()
        {
            Window = new UltravioletApplicationWindowSettings();
        }

        /// <summary>
        /// Saves the specified application settings to the specified file.
        /// </summary>
        /// <param name="path">The path to the file in which to save the application settings.</param>
        /// <param name="settings">The application settings to save to the specified file.</param>
        public static void Save(String path, UltravioletApplicationSettings settings)
        {
            var xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Settings",
                    UltravioletApplicationWindowSettings.Save(settings.Window)
                ));
            xml.Save(path);
        }

        /// <summary>
        /// Loads a set of application settings from the specified file.
        /// </summary>
        /// <param name="path">The path to the file from which to load the application settings.</param>
        /// <returns>The application settings which were loaded from the specified file.</returns>
        public static UltravioletApplicationSettings Load(String path)
        {
            var xml = XDocument.Load(path);

            var settings = new UltravioletApplicationSettings();

            settings.Window = UltravioletApplicationWindowSettings.Load(xml.Root.Element("Window"));

            return settings;
        }

        /// <summary>
        /// Creates a set of application settings from the current application state.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <returns>The application settings which were retrieved.</returns>
        public static UltravioletApplicationSettings FromCurrentSettings(UltravioletContext uv)
        {
            Contract.Require(uv, "uv");

            var settings = new UltravioletApplicationSettings();

            settings.Window = UltravioletApplicationWindowSettings.FromCurrentSettings(uv);

            return settings;
        }

        /// <summary>
        /// Applies the specified settings.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        public void Apply(UltravioletContext uv)
        {
            if (Window != null)
            {
                Window.Apply(uv);
            }
        }

        /// <summary>
        /// Gets the application's window settings.
        /// </summary>
        public UltravioletApplicationWindowSettings Window
        {
            get;
            private set;
        }
    }
}
