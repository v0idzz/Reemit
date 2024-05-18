using System.Text;

namespace Reemit.Decompiler.Clr.Metadata;

public class MetadataRoot
{
    public uint Signature { get; }
    public ushort MajorVersion { get; }
    public ushort MinorVersion { get; }
    public uint Reserved { get; }
    public string Version { get; }
    public ushort Flags { get; }
    public IReadOnlyList<StreamHeader> StreamHeaders { get; }

    private const uint MetadataSignatureMagic = 0x424A5342;

    public MetadataRoot(BinaryReader reader)
    {
        Signature = reader.ReadUInt32();

        if (Signature != MetadataSignatureMagic)
        {
            throw new BadImageFormatException("Invalid magic signature");
        }

        MajorVersion = reader.ReadUInt16();
        MinorVersion = reader.ReadUInt16();
        Reserved = reader.ReadUInt32();

        var versionBytesCount = reader.ReadInt32();
        var versionBytes = reader.ReadBytes(versionBytesCount);
        var indexOfNull = Array.IndexOf(versionBytes, (byte)'\0');
        Version = Encoding.UTF8.GetString(versionBytes[..indexOfNull]);

        Flags = reader.ReadUInt16();
        var streams = reader.ReadUInt16();

        var streamHeaders = new List<StreamHeader>(streams);
        for (var i = 0; i < streams; i++)
        {
            streamHeaders.Add(new StreamHeader(reader));
        }

        StreamHeaders = streamHeaders.AsReadOnly();
    }
}