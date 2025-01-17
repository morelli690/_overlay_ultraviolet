using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Foundation;
using Ultraviolet.Core;
using Ultraviolet.Messages;
using UIKit;

namespace Ultraviolet
{
    partial class UltravioletApplication
    {
        /// <summary>
        /// Contains native function declarations.
        /// </summary>
        private static class Native
        {
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SDL_SetDynamicMain(IntPtr proc);
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="UltravioletApplication"/> class.
        /// </summary>
        public static UltravioletApplication Instance { get; private set; }

        /// <summary>
        /// Invokes the <see cref="SDLMainProc"/> method from native code.
        /// </summary>
        /// <returns></returns>
        [MonoPInvokeCallback(typeof(Func<Int32>))]
        private static Int32 SDLMainProcThunk()
        {
            return Instance.SDLMainProc();
        }

        /// <summary>
        /// Represents the main procedure which will be executed by SDL.
        /// </summary>
        private Int32 SDLMainProc()
        {
            Run();

            if (uv != null)
            {
                uv.Dispose();
                uv = null;
            }

            return 0;
        }

        /// <summary>
        /// Initializes the application's state.
        /// </summary>
        partial void InitializeApplication()
        {
            lock (typeof(UltravioletApplication))
            {
                if (Instance != null)
                    throw new InvalidOperationException();

                Instance = this;

                mainProcDelegate = new Func<Int32>(SDLMainProcThunk);
                mainProcPtr = Marshal.GetFunctionPointerForDelegate(mainProcDelegate);

                Native.SDL_SetDynamicMain(mainProcPtr);
            }

            UIApplication.Main(new String[0], null, nameof(UltravioletAppDelegate));
        }

        /// <summary>
        /// Initializes the application's context after it has been acquired.
        /// </summary>
        partial void InitializeContext()
        {
            orientationDidChangeNotification = UIDevice.Notifications.ObserveOrientationDidChange((sender, args) =>
            {
                var messageData = Ultraviolet.Messages.CreateMessageData<OrientationChangedMessageData>();
                messageData.Display = Ultraviolet.GetPlatform().Displays[0];
                Ultraviolet.Messages.Publish(UltravioletMessages.OrientationChanged, messageData);
            });
        }

        /// <summary>
        /// Disposes any platform-specific resources.
        /// </summary>
        partial void DisposePlatformResources()
        {
            lock (typeof(UltravioletApplication))
            {
                Instance = null;

                Native.SDL_SetDynamicMain(IntPtr.Zero);

                mainProcDelegate = null;
                mainProcPtr = IntPtr.Zero;
            }

            SafeDispose.DisposeRef(ref orientationDidChangeNotification);
        }

        /// <summary>
        /// Loads the application's settings.
        /// </summary>
        partial void LoadSettings()
        {
            lock (stateSyncObject)
            {
                if (!PreserveApplicationSettings)
                    return;

                var directory = GetLocalApplicationSettingsDirectory();
                var path = Path.Combine(directory, "UltravioletSettings.xml");

                try
                {
                    this.settings = UltravioletApplicationSettings.Load(path);
                }
                catch (FileNotFoundException) { }
                catch (DirectoryNotFoundException) { }
                catch (XmlException) { }
            }
        }

        /// <summary>
        /// Saves the application's settings.
        /// </summary>
        partial void SaveSettings()
        {
            lock (stateSyncObject)
            {
                if (!PreserveApplicationSettings)
                    return;

                var directory = GetLocalApplicationSettingsDirectory();
                var path = Path.Combine(directory, "UltravioletSettings.xml");

                Directory.CreateDirectory(directory);

                this.settings = UltravioletApplicationSettings.FromCurrentSettings(Ultraviolet);
                UltravioletApplicationSettings.Save(path, settings);
            }
        }

        /// <summary>
        /// Applies the application's settings.
        /// </summary>
        partial void ApplySettings()
        {
            lock (stateSyncObject)
            {
                if (this.settings == null)
                    return;

                this.settings.Apply(uv);
            }
        }

        // The SDL main procedure.
        private Func<Int32> mainProcDelegate;
        private IntPtr mainProcPtr;

        // The application's settings.
        private UltravioletApplicationSettings settings;

        // Notifications.
        private NSObject orientationDidChangeNotification;
    }
}