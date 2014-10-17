﻿using System;
using System.Runtime.InteropServices;

namespace TwistedLogik.Ultraviolet.SDL2.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_DropEvent
    {
        public UInt32 type;
        public UInt32 timestamp;
        public char* file;
    }
}
