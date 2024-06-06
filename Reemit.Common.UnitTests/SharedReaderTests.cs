using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

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
        var actualBytes = sharedReader.ReadBytes(readBytesCount);
        
        // Assert
        Assert.Equal(sharedReaderOffset + readBytesCount, sharedReader.Offset);
        Assert.Equal(readBytesCount, sharedReader.RelativeOffset);
        Assert.Equal(readBytesCount, actualBytes.Length);
        Assert.Equal([0x1, 0x2], actualBytes);
    }

    [Fact]
    public void ReadBytes_Called_EmptyBufferKeepsRelativeOffsetAndOffset ()
    {
        // Arrange
        byte[] bytes = [];
        using var stream = new MemoryStream(bytes);
        using var binaryReader = new BinaryReader(stream);
        const int sharedReaderOffset = 1;
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());
        const int readBytesCount = 0x100;

        // Act
        var actualBytes = sharedReader.ReadBytes(readBytesCount);

        // Assert
        Assert.Equal(sharedReaderOffset, sharedReader.Offset);
        Assert.Equal(0, sharedReader.RelativeOffset);
        Assert.Equal([], actualBytes);
    }

    [Theory]
    [MemberData(nameof(GetReadUnmanagedData))]
    public void ReadUnmanaged_Called_AdvancesRelativeOffsetAndOffset<T>(
        int sharedReaderOffset,
        T[] expectedUnmanagedValues,
        Delegate readUnmanaged)
        where T : unmanaged
    {
        // Arrange
        var getBytes =
            typeof(T) == typeof(byte) ?
                x => [Unsafe.As<T, byte>(ref x)] :
            typeof(T) == typeof(char) ?
                x => Encoding.UTF8.GetBytes(Unsafe.As<T, char>(ref x).ToString()) :
                typeof(BitConverter)
                    .GetMethod(
                        nameof(BitConverter.GetBytes),
                        BindingFlags.Public | BindingFlags.Static,
                        [typeof(T)])!
                    .CreateDelegate<Func<T, byte[]>>();

        // We're going to assume every char is 1 byte. Technically not correct
        // since chars are UTF-8, but we're only testing single byte chars 
        // with this function. I could revert to Marshal.SizeOf here as that
        // returns 1 for char, but that's not really "correct" as it's intended
        // for interop.
        var size = typeof(T) != typeof(char) ? Unsafe.SizeOf<T>() : 1;

        var expectedBytes = expectedUnmanagedValues.SelectMany(getBytes).ToArray();
        var bytes = new byte[sharedReaderOffset].Concat(expectedBytes).ToArray();

        using var stream = new MemoryStream(bytes);
        using var binaryReader = new BinaryReader(stream);
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());
        var actualUnmanagedValues = new T[expectedUnmanagedValues.Length];
        var isRangeMapped = false;
        var expectedPositions = new int[expectedUnmanagedValues.Length];
        var actualRangeMaps = new IRangeMapped[expectedUnmanagedValues.Length];

        // Act
        for (var i = 0; i < actualUnmanagedValues.Length; i++)
        {
            var expectedPosition = sharedReader.Offset;
            var actualValue = ((Delegate)readUnmanaged).DynamicInvoke(sharedReader)!;

            if (actualValue is IRangeMapped rangeMapped)
            {
                isRangeMapped = true;
                var actualValueType = actualValue.GetType();
                var valueProp = actualValueType.GetProperty(nameof(RangeMapped<T>.Value))!;
                actualUnmanagedValues[i] = (T)valueProp.GetValue(actualValue)!;
                actualRangeMaps[i] = rangeMapped;
                expectedPositions[i] = expectedPosition;
            }
            else
            {
                actualUnmanagedValues[i] = (T)actualValue;
            }
        }

        // Assert
        Assert.Equal(expectedUnmanagedValues, actualUnmanagedValues);
        Assert.Equal(sharedReaderOffset + actualUnmanagedValues.Length * size, sharedReader.Offset);
        Assert.Throws<EndOfStreamException>(() => sharedReader.ReadByte());

        if (isRangeMapped)
        {
            Assert.Equal(expectedPositions, actualRangeMaps.Select(x => x.Position));

            Assert.Equal(
                Enumerable.Repeat(size, expectedUnmanagedValues.Length),
                actualRangeMaps.Select(x => x.Length));
        }
    }

    public static IEnumerable<object[]> GetReadUnmanagedData() =>
        new[]
        {
            CreatedReadUnmanagedTestCases(
                new byte[] { 0xef, 0xbe, 0xad, 0xde },
                r => r.ReadByte(),
                r => r.ReadMappedByte()),
            CreatedReadUnmanagedTestCases(
                "Hello world".ToCharArray(),
                r => r.ReadChar(),
                r => r.ReadMappedChar()),
            CreatedReadUnmanagedTestCases(
                "\x00\x01\x02\x03\x04\x05\x06".ToCharArray(),
                r => r.ReadChar(),
                r => r.ReadMappedChar()),
            CreatedReadUnmanagedTestCases(
                new ushort[] { 0xdead, 0xbeef, 0x5230, 0x1592, ushort.MinValue, ushort.MaxValue },
                r => r.ReadUInt16(),
                r => r.ReadMappedUInt16()),
            CreatedReadUnmanagedTestCases(
                new uint[] { 0xdeadbeef, 0xcafebabe, 0xcdcdcdcd, 0xc0c0c0c0, uint.MinValue, uint.MaxValue },
                r => r.ReadUInt32(),
                r => r.ReadMappedUInt32()),
            CreatedReadUnmanagedTestCases(
                new ulong[] { 0xdeadbeefcafebabe, 0xcdcdcdcdcdcdcdcd, 0xc0c0c0c0c0c0c0c0, 0xffffffffffffffff, ulong.MinValue, uint.MaxValue },
                r => r.ReadUInt64(),
                r => r.ReadMappedUInt64()),
            CreatedReadUnmanagedTestCases(
                new short[] { 123, 456, 789, 1011, short.MinValue, short.MaxValue },
                r => r.ReadInt16(),
                r => r.ReadMappedInt16()),
            CreatedReadUnmanagedTestCases(
                new int[] { 12345, 678910, 1112131415, 1617181920, int.MinValue, int.MaxValue },
                r => r.ReadInt32(),
                r => r.ReadMappedInt32()),
            CreatedReadUnmanagedTestCases(
                new long[] { 123456789101112, 131415161718192021, 222324252627282930, 313233343536373839, long.MinValue, int.MaxValue },
                r => r.ReadInt64(),
                r => r.ReadMappedInt64()),
        }
        .SelectMany(x => x);

    private static IEnumerable<object[]> CreatedReadUnmanagedTestCases<T>(
        T[] expectedUnmanagedValues,
        Func<SharedReader, T> readUnmanaged,
        Func<SharedReader, RangeMapped<T>> readRangeMappedUnmanaged)
        where T : unmanaged =>
        Enumerable
            .Range(1, expectedUnmanagedValues.Length)
            .SelectMany(x =>
                new[] { 0, 1, 512 }
                    .SelectMany(y =>
                        new object[] { (Delegate)readUnmanaged, (Delegate)readRangeMappedUnmanaged }
                            .Select(z => new object[] { y, expectedUnmanagedValues.Take(x).ToArray(), z })));

    [Theory]
    [MemberData(nameof(GetNotImplementedData))]
    public void UnimplementedRead_Called_ThrowsNotImplementedException<T>(Action<SharedReader> readAction)
    {
        // Arrange
        using var stream = new MemoryStream();
        using var binaryReader = new BinaryReader(stream);
        using var sharedReader = new SharedReader(0, binaryReader, new object());

        // Act
        var act = () => readAction(sharedReader);

        // Assert
        Assert.Throws<NotImplementedException>(act);
    }

    public static IEnumerable<object[]> GetNotImplementedData() =>
    [
        CreateNotImplementedTestCase(x => x.Read(new byte[0], 0, 0)),
        CreateNotImplementedTestCase(x => x.Read(new char[0], 0, 0)),
        CreateNotImplementedTestCase(x => x.Read(Span<byte>.Empty)),
        CreateNotImplementedTestCase(x => x.Read(Span<char>.Empty)),
        CreateNotImplementedTestCase(x => x.ReadBoolean()),
        CreateNotImplementedTestCase(x => x.ReadChars(0)),
        CreateNotImplementedTestCase(x => x.ReadDecimal()),
        CreateNotImplementedTestCase(x => x.ReadDouble()),
        CreateNotImplementedTestCase(x => x.ReadHalf()),
        CreateNotImplementedTestCase(x => x.ReadSByte()),
        CreateNotImplementedTestCase(x => x.ReadSingle()),
        CreateNotImplementedTestCase(x => x.ReadString()),
    ];

    private static object[] CreateNotImplementedTestCase(Action<SharedReader> readAction) => [readAction];

    [Fact]
    public void SynchronizationObject_Get_EqualsConstructorArgument()
    {
        // Arrange
        using var stream = new MemoryStream();
        using var binaryReader = new BinaryReader(stream);
        var syncObject = new object();
        using var sharedReader = new SharedReader(1, binaryReader, syncObject);

        // Act
        var actualSyncObject = sharedReader.SynchronizationObject;

        // Assert
        Assert.Equal(syncObject, actualSyncObject);
    }

    [Fact]
    public void CreateDerivedAtRelativeOffset_Called_RelativeOffsetSet()
    {
        // Arrange
        using var stream = new MemoryStream();
        using var binaryReader = new BinaryReader(stream);
        const int sharedReaderOffset = 1;
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());
        const int derivedSharedReaderOffset = 2;

        // Act
        var actualDerivedSharedReader = sharedReader.CreateDerivedAtRelativeOffset(derivedSharedReaderOffset);

        // Assert
        Assert.Equal(sharedReaderOffset, sharedReader.Offset);
        Assert.Equal(0, sharedReader.RelativeOffset);
        Assert.Equal(sharedReaderOffset + derivedSharedReaderOffset, actualDerivedSharedReader.Offset);
        Assert.Equal(0, actualDerivedSharedReader.RelativeOffset);
    }
}