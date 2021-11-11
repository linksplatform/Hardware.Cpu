using System;
using System.Linq;
using System.Runtime.InteropServices;

#pragma warning disable 0649
#pragma warning disable IDE0044 // Add readonly modifier

namespace Platform.Hardware.Cpu
{
    /// <summary>
    /// <para>
    /// Represents the windows.
    /// </para>
    /// <para></para>
    /// </summary>
    internal static class Windows
    {
        /// <summary>
        /// <para>
        /// Gets the size.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <para>Could not retrieve the cache line size.</para>
        /// <para></para>
        /// </exception>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        public static int GetSize()
        {
            var info = ManagedGetLogicalProcessorInformation();
            if (info == null)
            {
                throw new InvalidOperationException("Could not retrieve the cache line size.");
            }
            return info.First(x => x.Relationship == LOGICAL_PROCESSOR_RELATIONSHIP.RelationCache).ProcessorInformation.Cache.LineSize;
        }

        // http://stackoverflow.com/a/6972620/232574
        /// <summary>
        /// <para>
        /// The processorcore.
        /// </para>
        /// <para></para>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct PROCESSORCORE
        {
            /// <summary>
            /// <para>
            /// The flags.
            /// </para>
            /// <para></para>
            /// </summary>
            public byte Flags;
        }

        /// <summary>
        /// <para>
        /// The numanode.
        /// </para>
        /// <para></para>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct NUMANODE
        {
            /// <summary>
            /// <para>
            /// The node number.
            /// </para>
            /// <para></para>
            /// </summary>
            public uint NodeNumber;
        }

        /// <summary>
        /// <para>
        /// The processor cache type enum.
        /// </para>
        /// <para></para>
        /// </summary>
        enum PROCESSOR_CACHE_TYPE
        {
            /// <summary>
            /// <para>
            /// The cache unified processor cache type.
            /// </para>
            /// <para></para>
            /// </summary>
            CacheUnified,
            /// <summary>
            /// <para>
            /// The cache instruction processor cache type.
            /// </para>
            /// <para></para>
            /// </summary>
            CacheInstruction,
            /// <summary>
            /// <para>
            /// The cache data processor cache type.
            /// </para>
            /// <para></para>
            /// </summary>
            CacheData,
            /// <summary>
            /// <para>
            /// The cache trace processor cache type.
            /// </para>
            /// <para></para>
            /// </summary>
            CacheTrace
        }

        /// <summary>
        /// <para>
        /// The cache descriptor.
        /// </para>
        /// <para></para>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct CACHE_DESCRIPTOR
        {
            /// <summary>
            /// <para>
            /// The level.
            /// </para>
            /// <para></para>
            /// </summary>
            public Byte Level;
            /// <summary>
            /// <para>
            /// The associativity.
            /// </para>
            /// <para></para>
            /// </summary>
            public Byte Associativity;
            /// <summary>
            /// <para>
            /// The line size.
            /// </para>
            /// <para></para>
            /// </summary>
            public UInt16 LineSize;
            /// <summary>
            /// <para>
            /// The size.
            /// </para>
            /// <para></para>
            /// </summary>
            public UInt32 Size;
            /// <summary>
            /// <para>
            /// The type.
            /// </para>
            /// <para></para>
            /// </summary>
            public PROCESSOR_CACHE_TYPE Type;
        }

        /// <summary>
        /// <para>
        /// The system logical processor information union.
        /// </para>
        /// <para></para>
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION
        {
            /// <summary>
            /// <para>
            /// The processor core.
            /// </para>
            /// <para></para>
            /// </summary>
            [FieldOffset(0)]
            public PROCESSORCORE ProcessorCore;
            /// <summary>
            /// <para>
            /// The numa node.
            /// </para>
            /// <para></para>
            /// </summary>
            [FieldOffset(0)]
            public NUMANODE NumaNode;
            /// <summary>
            /// <para>
            /// The cache.
            /// </para>
            /// <para></para>
            /// </summary>
            [FieldOffset(0)]
            public CACHE_DESCRIPTOR Cache;
            /// <summary>
            /// <para>
            /// The reserved.
            /// </para>
            /// <para></para>
            /// </summary>
            [FieldOffset(0)]
            private UInt64 Reserved1;
            /// <summary>
            /// <para>
            /// The reserved.
            /// </para>
            /// <para></para>
            /// </summary>
            [FieldOffset(8)]
            private UInt64 Reserved2;
        }

        /// <summary>
        /// <para>
        /// The logical processor relationship enum.
        /// </para>
        /// <para></para>
        /// </summary>
        enum LOGICAL_PROCESSOR_RELATIONSHIP
        {
            /// <summary>
            /// <para>
            /// The relation processor core logical processor relationship.
            /// </para>
            /// <para></para>
            /// </summary>
            RelationProcessorCore,
            /// <summary>
            /// <para>
            /// The relation numa node logical processor relationship.
            /// </para>
            /// <para></para>
            /// </summary>
            RelationNumaNode,
            /// <summary>
            /// <para>
            /// The relation cache logical processor relationship.
            /// </para>
            /// <para></para>
            /// </summary>
            RelationCache,
            /// <summary>
            /// <para>
            /// The relation processor package logical processor relationship.
            /// </para>
            /// <para></para>
            /// </summary>
            RelationProcessorPackage,
            /// <summary>
            /// <para>
            /// The relation group logical processor relationship.
            /// </para>
            /// <para></para>
            /// </summary>
            RelationGroup,
            /// <summary>
            /// <para>
            /// The relation all logical processor relationship.
            /// </para>
            /// <para></para>
            /// </summary>
            RelationAll = 0xffff
        }
        private struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION
        {
            /// <summary>
            /// <para>
            /// The processor mask.
            /// </para>
            /// <para></para>
            /// </summary>
            public UIntPtr ProcessorMask;
            /// <summary>
            /// <para>
            /// The relationship.
            /// </para>
            /// <para></para>
            /// </summary>
            public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
            /// <summary>
            /// <para>
            /// The processor information.
            /// </para>
            /// <para></para>
            /// </summary>
            public SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION ProcessorInformation;
        }

        /// <summary>
        /// <para>
        /// Determines whether get logical processor information.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="Buffer">
        /// <para>The buffer.</para>
        /// <para></para>
        /// </param>
        /// <param name="ReturnLength">
        /// <para>The return length.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [DllImport(@"kernel32.dll", SetLastError = true)]
        private static extern bool GetLogicalProcessorInformation(IntPtr Buffer, ref UInt32 ReturnLength);
        private const int ERROR_INSUFFICIENT_BUFFER = 122;
        private static SYSTEM_LOGICAL_PROCESSOR_INFORMATION[] ManagedGetLogicalProcessorInformation()
        {
            var ReturnLength = 0u;
            GetLogicalProcessorInformation(IntPtr.Zero, ref ReturnLength);
            if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
            {
                return null;
            }
            var pointer = Marshal.AllocHGlobal((int)ReturnLength);
            try
            {
                if (GetLogicalProcessorInformation(pointer, ref ReturnLength))
                {
                    var size = Marshal.SizeOf<SYSTEM_LOGICAL_PROCESSOR_INFORMATION>();
                    var length = (int)ReturnLength / size;
                    var buffer = new SYSTEM_LOGICAL_PROCESSOR_INFORMATION[length];
                    var itemPointer = pointer;
                    for (int i = 0; i < length; i++)
                    {
                        buffer[i] = Marshal.PtrToStructure<SYSTEM_LOGICAL_PROCESSOR_INFORMATION>(itemPointer);
                        itemPointer += size;
                    }
                    return buffer;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
            return null;
        }
    }
}
