using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Streams;
using Reemit.Decompiler.PE;

namespace Reemit.Decompiler;

public record ModuleReaderContext(
    PEFile PEFile,
    MetadataTablesStream MetadataTablesStream,
    StringsHeapStream StringsHeapStream,
    TableReferenceResolver TableReferenceResolver);