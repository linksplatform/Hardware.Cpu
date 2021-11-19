using Xunit;

namespace Platform.Hardware.Cpu.Tests
{
    public static class Tests
    {
        [Fact]
        public static void Test() => Assert.NotEqual(0, CacheLine.Size);
    }
}
