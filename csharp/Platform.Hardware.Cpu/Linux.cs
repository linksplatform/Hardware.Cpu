using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Platform.Hardware.Cpu
{
    internal static class Linux
    {
        public static int GetSize() => (int)sysconf(_SC_LEVEL1_DCACHE_LINESIZE);

        [DllImport("libc")]
        private static extern Int64 sysconf(Int32 name);

        private const Int32 _SC_LEVEL1_DCACHE_LINESIZE = 190;
    }
}
