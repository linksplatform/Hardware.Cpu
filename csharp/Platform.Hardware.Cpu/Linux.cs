using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Platform.Hardware.Cpu
{
    /// <summary>
    /// <para>
    /// Represents the linux.
    /// </para>
    /// <para></para>
    /// </summary>
    internal static class Linux
    {
        /// <summary>
        /// <para>
        /// Gets the size.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        public static int GetSize() => (int)sysconf(_SC_LEVEL1_DCACHE_LINESIZE);

        /// <summary>
        /// <para>
        /// Sysconfs the name.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="name">
        /// <para>The name.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int 64</para>
        /// <para></para>
        /// </returns>
        [DllImport("libc")]
        private static extern Int64 sysconf(Int32 name);
        private const Int32 _SC_LEVEL1_DCACHE_LINESIZE = 190;
    }
}
