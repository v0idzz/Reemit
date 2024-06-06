using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Streams;
using Reemit.Decompiler.PE;

namespace Reemit.Decompiler;

public class ClrModule
{
    public string Name { get; }
    public IReadOnlyList<ClrType>? Types { get; }

    private ClrModule(string name, IReadOnlyList<ClrType>? types)
    {
        Name = name;
        Types = types;
    }

    public static ClrModule Open(string fileName)
    {
        var fileStream = new FileStream(fileName, FileMode.Open);
        var peFile = new PEFile(new BinaryReader(fileStream));

        const int clrHeaderDirIndex = 14;

        if (peFile.DataDirectories.Count - 1 < clrHeaderDirIndex)
        {
            throw new BadImageFormatException("This image does not have a CLR header");
        }

        var clrRuntimeHeaderDir = peFile.DataDirectories[clrHeaderDirIndex];
        var clrRuntimeHeader = peFile.GetStructureDescribedByDataDirectory<CliHeader>(clrRuntimeHeaderDir);

        var metadataOffset = peFile.GetFileOffset(clrRuntimeHeader.Metadata.VirtualAddress);
        var metadataReader = peFile.CreateReaderAt(metadataOffset);
        var metadata = new MetadataRoot(metadataReader);

        var stringsStreamHeader = metadata.StreamHeaders.Single(x => x.Name == StringsHeapStream.Name);
        var stringsStreamOffset = metadataOffset + stringsStreamHeader.Offset;
        var stringsStream = new StringsHeapStream(peFile.CreateReaderAt(stringsStreamOffset), stringsStreamHeader);

        var metadataStreamHeader = metadata.StreamHeaders.Single(x => x.Name == MetadataTablesStream.Name);
        var metadataStreamOffset = metadataOffset + metadataStreamHeader.Offset;
        var metadataStream = new MetadataTablesStream(peFile.CreateReaderAt(metadataStreamOffset));

        var context = new ClrMetadataContext(metadataStream, stringsStream);

        var types = metadataStream.TypeDef?.Rows.Select(x => ClrType.FromTypeDefRow(x, context)).ToArray().AsReadOnly();

        var name = stringsStream.Read(metadataStream.Module.Rows[0].Value.Name);

        return new ClrModule(name, types);
    }

    public string DebugDump() =>
        $"Module: {Name}, Types: {string.Join(", ", Types?.Select(x => x.Name) ?? Array.Empty<string>())}";
}