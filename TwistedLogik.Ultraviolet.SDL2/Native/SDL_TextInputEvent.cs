﻿using System;
using System.Runtime.InteropServices;

namespace TwistedLogik.Ultraviolet.SDL2.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_TextInputEvent
    {
        public const Int32 TEXT_SIZE = 32;

        public UInt32 type;
        public UInt32 timestamp;
        public UInt32 windowID;
        public fixed char text[TEXT_SIZE];
    }
}
