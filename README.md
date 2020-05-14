[![NuGet Version and Downloads count](https://buildstats.info/nuget/Platform.Hardware.Cpu)](https://www.nuget.org/packages/Platform.Hardware.Cpu)
[![Actions Status](https://github.com/linksplatform/Hardware.Cpu/workflows/CD/badge.svg)](https://github.com/linksplatform/Hardware.Cpu/actions?workflow=CD)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/9c4395d5eb1a4c0eb578fe69abb109f2)](https://www.codacy.com/manual/drakonard/Hardware.Cpu?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=linksplatform/Hardware.Cpu&amp;utm_campaign=Badge_Grade)
[![CodeFactor](https://www.codefactor.io/repository/github/linksplatform/hardware.cpu/badge)](https://www.codefactor.io/repository/github/linksplatform/hardware.cpu)

# [Hardware.Cpu](https://github.com/linksplatform/Hardware.Cpu)

LinksPlatform's Platform.Hardware.Cpu Class Library.

Namespace: [Platform.Hardware.Cpu](https://linksplatform.github.io/Hardware.Cpu/csharp/api/Platform.Hardware.Cpu.html)

Forked from: [NickStrupat/CacheLineSize.NET](https://github.com/NickStrupat/CacheLineSize.NET)

NuGet package: [Platform.Hardware.Cpu](https://www.nuget.org/packages/Platform.Hardware.Cpu)

## Example
```csharp
using System;
using Platform.Hardware.Cpu;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(CacheLine.Size); // Print the cache line size in bytes
        
        var array = new CacheLineAlignedArray<string>(10);
        Interlocked.Exchange(ref array[0], "Hello"); // All threads can now see the latest value at `array[0]` without risk of ruining performance with false-sharing

        // This can be used to build collections which share elements across threads at the fastest possible synchronization.
    }
    
    // An array-like type where each element is on it's own cache-line. This is a building block for avoiding false-sharing.
    public struct CacheLineAlignedArray<T>
        where T : class
    {
        private readonly T[] buffer;
        public CacheLineAlignedArray(Int32 size) => buffer = new T[Multiplier * size];
        public Int32 Length => buffer.Length / Multiplier;
        public ref T this[Int32 index] => ref buffer[Multiplier * index];
        private static readonly Int32 Multiplier = CacheLine.Size / IntPtr.Size;
    }
}
```

## [Documentation](https://linksplatform.github.io/Hardware.Cpu)
[PDF file](https://linksplatform.github.io/Hardware.Cpu/csharp/Platform.Hardware.Cpu.pdf) with code for e-readers.

## Dependent libraries
*   [Platform.Unsafe](https://github.com/linksplatform/Unsafe)

## See also
*   [NickStrupat/CacheLineSize.NET](https://github.com/NickStrupat/CacheLineSize.NET) for the equivalent .NET Standard 1.5 library (without .NET Framework support)
*   [NickStrupat/CacheLineSize](https://github.com/NickStrupat/CacheLineSize) for the equivalent C function
*   [ulipollo/CacheLineSizeMex](https://github.com/ulipollo/CacheLineSizeMex) for the MATLAB function
