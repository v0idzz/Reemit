using System.Runtime.InteropServices;
using System.Text;

namespace Reemit.Common;

public static class BinaryReaderExtensions
{
    public static string ReadAsciiString(this BinaryReader reader, int size)
    {
        var bytes = reader.ReadBytes(size);
        return Encoding.ASCII.GetString(bytes);
    }

    public static string ReadUtf8String(this BinaryReader reader, int size)
    {
        var bytes = reader.ReadBytes(size);
        return Encoding.UTF8.GetString(bytes);
    }

    public static T ReadStruct<T>(this BinaryReader reader) where T : unmanaged
    {
        var size = Marshal.SizeOf<T>();
        var bytes = reader.ReadBytes(size);
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(bytes, 0, ptr, size);

        try
        {
            return Marshal.PtrToStructure<T>(ptr);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }
}