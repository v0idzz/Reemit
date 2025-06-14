using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler;

public class TypeDefOrRefToClrTypeInfoMapper(ModuleReaderContext moduleReaderContext)
{
    public ClrTypeInfo ResolveDefOrRefCodedIndex(CodedIndex typeDefOrRefOrSpec)
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
                throw new NotImplementedException("TypeSpec resolving isn't implemented");
            default:
                throw new ArgumentOutOfRangeException();
        }

        return ClrTypeInfo.CreateSimpleTypeInfo(
            moduleReaderContext.StringsHeapStream.Read(@namespace),
            moduleReaderContext.StringsHeapStream.Read(@name)
        );
    }
}