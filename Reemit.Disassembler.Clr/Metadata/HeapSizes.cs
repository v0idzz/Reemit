namespace Reemit.Disassembler.Clr.Metadata;

[Flags]
public enum HeapSizes : byte
{
    StringStream = 0x01,
    GuidStream = 0x02,
    BlobStream = 0x04
}