using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Streams;
using Reemit.Decompiler.PE;

namespace Reemit.Decompiler;

public class ClrModule
{
    public string Name { get; }
    public IReadOnlyList<ClrType>? Types { get; }

    public ClrModule(string fileName)
    {
        var fileStream = new FileStream(fileName, FileMode.Open);
        var peFile = new PEFile(new BinaryReader(fileStream));

        var clrRuntimeHeaderDir = peFile.DataDirectories[14];
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

        Types = metadataStream.TypeDef?.Rows.Select(x => ClrType.FromTypeDefRow(x, context)).ToArray().AsReadOnly();

        if (metadataStream.Module is not null)
        {
            Name = stringsStream.Read(metadataStream.Module.Rows[0].Name);
        }
    }
}