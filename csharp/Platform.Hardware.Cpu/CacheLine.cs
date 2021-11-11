using System;
using System.Runtime.InteropServices;

namespace Platform.Hardware.Cpu
{
    /// <summary>
    /// <para>Contains constants related to CPUs cache line.</para>
    /// <para>Содержит константы, относящиеся к строке кэша ЦП.</para>
    /// </summary>
    public static class CacheLine
    {
        /// <summary>
        /// <para>Gets the size of CPUs cache line in bytes.</para>
        /// <para>Получает размер строки кэша ЦП в байтах.</para>
        /// </summary>
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
