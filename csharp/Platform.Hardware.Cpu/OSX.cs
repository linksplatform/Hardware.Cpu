using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Platform.Hardware.Cpu
{
    internal static class OSX
    {
        public static int GetSize()
        {
            var sizeOfLineSize = (IntPtr)IntPtr.Size;
            sysctlbyname("hw.cachelinesize", out IntPtr lineSize, ref sizeOfLineSize, IntPtr.Zero, IntPtr.Zero);
            return lineSize.ToInt32();
        }

        [DllImport("libc")]
        private static extern Int32 sysctlbyname(string name, out IntPtr oldp, ref IntPtr oldlenp, IntPtr newp, IntPtr newlen);
    }
}
