using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler;

public class ClrMetadataContext(MetadataTablesStream metadataTablesStream, StringsHeapStream stringsHeapStream)
{
    public MetadataTablesStream MetadataTablesStream { get; } = metadataTablesStream;
    public StringsHeapStream StringsHeapStream { get; } = stringsHeapStream;
}