using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler;

public class TypeDefOrRefToClrTypeInfoMapper(ModuleReaderContext moduleReaderContext)
{
    public bool TryResolveDefOrRefCodedIndex(CodedIndex typeDefOrRefOrSpec, out ClrTypeInfo? clrTypeInfo)
    {
        uint name, @namespace;

        switch (typeDefOrRefOrSpec.ReferencedTable)
        {
            case MetadataTableName.TypeDef:
                var typeDef =
                    moduleReaderContext.TableReferenceResolver.GetReferencedRow<TypeDefRow>(
                        typeDefOrRefOrSpec.Rid);
                name = typeDef.TypeName;
                @namespace = typeDef.TypeNamespace;
                break;
            case MetadataTableName.TypeRef:
                var typeRef =
                    moduleReaderContext.TableReferenceResolver.GetReferencedRow<TypeRefRow>(
                        typeDefOrRefOrSpec.Rid);
                name = typeRef.TypeName;
                @namespace = typeRef.TypeNamespace;
                break;
            case MetadataTableName.TypeSpec:
                clrTypeInfo = null;
                return false;
            default:
                throw new ArgumentOutOfRangeException();
        }

        clrTypeInfo = ClrTypeInfo.CreateSimpleTypeInfo(
            moduleReaderContext.StringsHeapStream.Read(@namespace),
            moduleReaderContext.StringsHeapStream.Read(@name)
        );
        return true;
    }
}