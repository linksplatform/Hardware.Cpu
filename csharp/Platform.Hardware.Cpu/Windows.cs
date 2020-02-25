using System;
using System.Linq;
using System.Runtime.InteropServices;

#pragma warning disable 0649
#pragma warning disable IDE0044 // Add readonly modifier

namespace Platform.Hardware.Cpu
{
    internal static class Windows
    {
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
        [StructLayout(LayoutKind.Sequential)]
        struct PROCESSORCORE
        {
            public byte Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct NUMANODE
        {
            public uint NodeNumber;
        }

        enum PROCESSOR_CACHE_TYPE
        {
            CacheUnified,
            CacheInstruction,
            CacheData,
            CacheTrace
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CACHE_DESCRIPTOR
        {
            public Byte Level;
            public Byte Associativity;
            public UInt16 LineSize;
            public UInt32 Size;
            public PROCESSOR_CACHE_TYPE Type;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION
        {
            [FieldOffset(0)]
            public PROCESSORCORE ProcessorCore;
            [FieldOffset(0)]
            public NUMANODE NumaNode;
            [FieldOffset(0)]
            public CACHE_DESCRIPTOR Cache;
            [FieldOffset(0)]
            private UInt64 Reserved1;
            [FieldOffset(8)]
            private UInt64 Reserved2;
        }

        enum LOGICAL_PROCESSOR_RELATIONSHIP
        {
            RelationProcessorCore,
            RelationNumaNode,
            RelationCache,
            RelationProcessorPackage,
            RelationGroup,
            RelationAll = 0xffff
        }

        private struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION
        {
            public UIntPtr ProcessorMask;
            public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
            public SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION ProcessorInformation;
        }

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
