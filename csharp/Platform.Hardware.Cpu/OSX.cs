using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Platform.Hardware.Cpu
{
    /// <summary>
    /// <para>
    /// Represents the osx.
    /// </para>
    /// <para></para>
    /// </summary>
    internal static class OSX
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
        public static int GetSize()
        {
            var sizeOfLineSize = (IntPtr)IntPtr.Size;
            sysctlbyname("hw.cachelinesize", out IntPtr lineSize, ref sizeOfLineSize, IntPtr.Zero, IntPtr.Zero);
            return lineSize.ToInt32();
        }

        /// <summary>
        /// <para>
        /// Sysctlbynames the name.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="name">
        /// <para>The name.</para>
        /// <para></para>
        /// </param>
        /// <param name="oldp">
        /// <para>The oldp.</para>
        /// <para></para>
        /// </param>
        /// <param name="oldlenp">
        /// <para>The oldlenp.</para>
        /// <para></para>
        /// </param>
        /// <param name="newp">
        /// <para>The newp.</para>
        /// <para></para>
        /// </param>
        /// <param name="newlen">
        /// <para>The newlen.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The int 32</para>
        /// <para></para>
        /// </returns>
        [DllImport("libc")]
        private static extern Int32 sysctlbyname(string name, out IntPtr oldp, ref IntPtr oldlenp, IntPtr newp, IntPtr newlen);
    }
}
