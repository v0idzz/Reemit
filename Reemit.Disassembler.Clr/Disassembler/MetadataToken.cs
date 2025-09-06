using System.Diagnostics.CodeAnalysis;
using Reemit.Disassembler.Clr.Metadata;

namespace Reemit.Disassembler.Clr.Disassembler;

public struct MetadataToken
{
    public MetadataTableName? TableRef { get; }
    
    [MemberNotNullWhen(false, nameof(TableRef))]
    public bool IsUserStringHeapRef => TableRef is null;

    public uint Index { get; }

    private MetadataToken(MetadataTableName? tableRef, uint index)
    {
        TableRef = tableRef;
        Index = index;
    }

    public static MetadataToken CreateUserStringHeapRef(uint byteOffset)
    {
        return new MetadataToken(null, byteOffset);
    }

    public static MetadataToken CreateMetadataTableRef(MetadataTableName tableName, uint rid)
    {
        return new MetadataToken(tableName, rid);
    }

    public static MetadataToken FromByteArray(byte[] bytes)
    {
        // From ECMA-335 "III.1.9 Metadata tokens":
        const byte userStringHeapMarker = 0x70;

        var targetRef = BitConverter.IsLittleEndian ? bytes[3] : bytes[0];
        MetadataTableName? tableRef = null;
        if (targetRef != userStringHeapMarker)
        {
            tableRef = (MetadataTableName)targetRef;
        }
        
        var value = BitConverter.ToUInt32(bytes, 0);
        var index = value & 0x00FFFFFF;
        
        return new MetadataToken(tableRef, index);
    }
}