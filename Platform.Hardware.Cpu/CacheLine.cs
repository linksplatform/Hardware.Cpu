using System;
using System.Runtime.InteropServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Hardware.Cpu
{
    public static class CacheLine
    {
        public static readonly int Size = GetSize();

        private static int GetSize()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.GetSize();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.GetSize();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSX.GetSize();
            }
            throw new NotSupportedException("Unrecognized OS platform.");
        }
    }
}
