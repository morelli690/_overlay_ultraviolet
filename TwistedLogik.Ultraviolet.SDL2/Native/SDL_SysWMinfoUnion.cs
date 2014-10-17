﻿using System;
using System.Runtime.InteropServices;

namespace TwistedLogik.Ultraviolet.SDL2.Native
{
    [StructLayout(LayoutKind.Explicit)]
    public struct SDL_SysWMinfoUnion
    {
        [FieldOffset(0)]
        public SDL_SysWMinfo_win win;
        [FieldOffset(0)]
        public SDL_SysWMinfo_winrt winrt;
        [FieldOffset(0)]
        public SDL_SysWMinfo_x11 x11;
        [FieldOffset(0)]
        public SDL_SysWMinfo_dfb dfb;
        [FieldOffset(0)]
        public SDL_SysWMinfo_cocoa cocoa;
        [FieldOffset(0)]
        public SDL_SysWMinfo_uikit uikit;
        [FieldOffset(0)]
        public SDL_SysWMinfo_wl wl;
        [FieldOffset(0)]
        public SDL_SysWMinfo_mir mir;
        [FieldOffset(0)]
        public Int32 dummy;
    }
}
