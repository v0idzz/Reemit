using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Reemit.Common.UnitTests;

public sealed class SharedReaderTests
{
    [Fact]
    public void ReadBytes_Called_AdvancesRelativeOffsetAndOffset()
    {
        // Arrange
        byte[] bytes = [0x0, 0x1, 0x2, 0x3, 0x4, 0x5];
        using var stream = new MemoryStream(bytes);
        using var binaryReader = new BinaryReader(stream);
        const int sharedReaderOffset = 1;
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());
        const int readBytesCount = 2;
        
        // Act
        sharedReader.ReadBytes(readBytesCount);
        
        // Assert
        Assert.Equal(sharedReaderOffset + readBytesCount, sharedReader.Offset);
        Assert.Equal(readBytesCount, sharedReader.RelativeOffset);
    }

    [Theory]
    [MemberData(nameof(GetReadUnmanagedData))]
    public void ReadUnmanaged_Called_AdvancesRelativeOffsetAndOffset<T>(
        int sharedReaderOffset,
        T[] expectedUnmanagedValues,
        Func<SharedReader, T> readUnmanaged)
        where T : unmanaged
    {
        // Arrange
        var size = Marshal.SizeOf<T>();

        var getBytes =
            typeof(T) != typeof(byte) ?
                typeof(BitConverter)
                    .GetMethod(
                        nameof(BitConverter.GetBytes),
                        BindingFlags.Public | BindingFlags.Static,
                        [typeof(T)])!
                    .CreateDelegate<Func<T, byte[]>>() :
            x => [Unsafe.As<T, byte>(ref x)];

        var expectedBytes = expectedUnmanagedValues.SelectMany(getBytes).ToArray();
        var bytes = new byte[sharedReaderOffset].Concat(expectedBytes).ToArray();

        using var stream = new MemoryStream(bytes);
        using var binaryReader = new BinaryReader(stream);
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());

        // Act
        var actualUnmanagedValues = new T[expectedUnmanagedValues.Length];

        for (var i = 0; i < actualUnmanagedValues.Length; i++)
        {
            actualUnmanagedValues[i] = readUnmanaged(sharedReader);
        }

        // Assert
        Assert.Equal(expectedUnmanagedValues, actualUnmanagedValues);
        Assert.Equal(sharedReaderOffset + actualUnmanagedValues.Length * size, sharedReader.Offset);
        Assert.Throws<EndOfStreamException>(() => sharedReader.ReadByte());
    }

    public static IEnumerable<object[]> GetReadUnmanagedData() =>
        new[]
        {
            CreatedReadUnmanagedTestCases(
                new byte[] { 0xef, 0xbe, 0xad, 0xde },
                r => r.ReadByte()),
            // Doesn't work--need to figure out exactly how SharedReader.ReadChar should behave.
            //CreatedReadUnmanagedTestCases(
            //    "Hello world".ToCharArray(),
            //    r => r.ReadChar()),
            CreatedReadUnmanagedTestCases(
                new ushort[] { 0xdead, 0xbeef, 0x5230, 0x1592, ushort.MinValue, ushort.MaxValue },
                r => r.ReadUInt16()),
            CreatedReadUnmanagedTestCases(
                new uint[] { 0xdeadbeef, 0xcafebabe, 0xcdcdcdcd, 0xc0c0c0c0, uint.MinValue, uint.MaxValue },
                r => r.ReadUInt32()),
            CreatedReadUnmanagedTestCases(
                new ulong[] { 0xdeadbeefcafebabe, 0xcdcdcdcdcdcdcdcd, 0xc0c0c0c0c0c0c0c0, 0xffffffffffffffff, ulong.MinValue, uint.MaxValue },
                r => r.ReadUInt64()),
            CreatedReadUnmanagedTestCases(
                new short[] { 123, 456, 789, 1011, short.MinValue, short.MaxValue },
                r => r.ReadInt16()),
            CreatedReadUnmanagedTestCases(
                new int[] { 12345, 678910, 1112131415, 1617181920, int.MinValue, int.MaxValue },
                r => r.ReadInt32()),
            CreatedReadUnmanagedTestCases(
                new long[] { 123456789101112, 131415161718192021, 222324252627282930, 313233343536373839, long.MinValue, int.MaxValue },
                r => r.ReadInt64()),
        }
        .SelectMany(x => x);

    private static IEnumerable<object[]> CreatedReadUnmanagedTestCases<T>(
        T[] expectedUnmanagedValues,
        Func<SharedReader, T> readUnmanaged)
        where T : unmanaged =>
        Enumerable
            .Range(1, expectedUnmanagedValues.Length)
            .SelectMany(x =>
                new[] { 0, 1, 512 }
                    .Select(y => new object[]
                    {
                        y,
                        expectedUnmanagedValues.Take(x).ToArray(),
                        readUnmanaged
                    }));
}