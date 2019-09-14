# CacheLineSize.NET
A cross-platform .NET Standard 1.5 library to get the cache line size of the processor, in bytes. Windows, Linux, and macOS supported.

[![NuGet Status](http://img.shields.io/nuget/v/CacheLine.Size.svg?style=flat)](https://www.nuget.org/packages/CacheLine.Size/)

## Example

```csharp
using System;
using NickStrupat;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(CacheLine.Size); // print the cache line size in bytes
        
        var array = new CacheLineAlignedArray<string>(10);
        Interlocked.Exchange(ref array[0], "Hello"); // All threads can now see the latest value at `array[0]` without risk of ruining performance with false-sharing

        // This can be used to build collections which share elements across threads at the fastest possible synchronization.
    }
    
    // An array-like type where each element is on it's own cache-line. This is a building block for avoiding false-sharing.
    public struct CacheLineAlignedArray<T> where T : class {
        private readonly T[] buffer;
        public CacheLineAlignedArray(Int32 size) => buffer = new T[Multiplier * size];
        public Int32 Length => buffer.Length / Multiplier;
        public ref T this[Int32 index] => ref buffer[Multiplier * index];
        private static readonly Int32 Multiplier = CacheLine.Size / IntPtr.Size;
    }
}
```

## See also

- [https://github.com/NickStrupat/CacheLineSize](https://github.com/NickStrupat/CacheLineSize) for the equivalent C function
