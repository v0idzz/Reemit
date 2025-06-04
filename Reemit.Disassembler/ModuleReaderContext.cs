using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Streams;
using Reemit.Disassembler.PE;

namespace Reemit.Disassembler;

public record ModuleReaderContext(
    PEFile PEFile,
    MetadataTablesStream MetadataTablesStream,
    StringsHeapStream StringsHeapStream,
    BlobHeapStream BlobHeapStream,
    TableReferenceResolver TableReferenceResolver);