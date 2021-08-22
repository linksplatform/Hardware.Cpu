using Xunit;

namespace Platform.Hardware.Cpu.Tests
{
    /// <summary>
    /// <para>
    /// Represents the tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class Tests
    {
        /// <summary>
        /// <para>
        /// Tests that test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void Test() => Assert.NotEqual(0, CacheLine.Size);
    }
}
