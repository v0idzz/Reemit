using Reemit.Decompiler.Clr;
using Reemit.Decompiler.Clr.Metadata.Tables;
using Reemit.Decompiler.Clr.Signatures;
using Reemit.Decompiler.Clr.Signatures.Types;

namespace Reemit.Decompiler;

public class TypeSigToClrTypeInfoMapper(ModuleReaderContext moduleReaderContext)
{
    public ClrTypeInfo Map(ITypeSig typeSig)
    {
        try
        {
            return typeSig switch
            {
                NativeTypeSig nativeTypeSig => MapNativeTypeSig(nativeTypeSig),
                ArraySig arraySig => MapArraySig(arraySig),
                ClassSig classSig => MapClassSig(classSig),
                GenericInstSig genericInstSig => MapGenericInstSig(genericInstSig),
                SZArraySig szArraySig => MapSZArraySig(szArraySig),
                ValueTypeSig valueTypeSig => MapValueTypeSig(valueTypeSig),
                not null => throw new NotImplementedException($"{typeSig.GetType().Name} resolving isn't implemented"),
                null => throw new ArgumentNullException(nameof(typeSig))
            };
        }
        catch (NotImplementedException)
        {
            return ClrTypeInfo.CreateSimpleTypeInfo(string.Empty, "?");
        }

        ClrTypeInfo MapNativeTypeSig(NativeTypeSig nativeTypeSig)
        {
            const string @namespace = "System";

            return nativeTypeSig.NativeType switch
            {
                NativeType.Void => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Void", "void"),
                NativeType.Boolean => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Boolean", "bool"),
                NativeType.Char => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Char", "char"),
                NativeType.I1 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "SByte", "sbyte"),
                NativeType.U1 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Byte", "byte"),
                NativeType.I2 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Int16", "short"),
                NativeType.U2 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "UInt16", "ushort"),
                NativeType.I4 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Int32", "int"),
                NativeType.U4 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "UInt32", "uint"),
                NativeType.I8 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Int64", "long"),
                NativeType.U8 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "UInt64", "ulong"),
                NativeType.R4 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Single", "float"),
                NativeType.R8 => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Double", "double"),
                NativeType.I => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "IntPtr", "nint"),
                NativeType.U => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "UIntPtr", "nuint"),
                NativeType.String => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "String", "string"),
                NativeType.TypedByRef => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "TypedReference"),
                NativeType.Object => ClrTypeInfo.CreateSimpleTypeInfo(@namespace, "Object", "object"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        ClrTypeInfo MapArraySig(ArraySig arraySig) =>
            ClrTypeInfo.CreateArrayTypeInfo(Map(arraySig.Type), (int)arraySig.Shape.Rank);

        ClrTypeInfo MapClassSig(ClassSig classSig) =>
            ResolveDefOrRefOrSpecEncodedSig(classSig.TypeDefOrRefOrSpecEncoded);

        ClrTypeInfo MapGenericInstSig(GenericInstSig genericInstSig)
        {
            var type = ResolveDefOrRefOrSpecEncodedSig(genericInstSig.Type);
            return ClrTypeInfo.CreateGenericTypeInfo(type.Namespace, type.Name, genericInstSig.GenericArguments
                .Select(genArg => Map(genArg.Type))
                .ToArray());
        }

        ClrTypeInfo ResolveDefOrRefOrSpecEncodedSig(TypeDefOrRefOrSpecEncodedSig typeDefOrRefOrSpecEncodedSig)
        {
            uint name, @namespace;
            switch (typeDefOrRefOrSpecEncodedSig.TypeDefOrRefOrSpec)
            {
                case TypeDefOrRefOrSpec.TypeDef:
                    var typeDef =
                        moduleReaderContext.TableReferenceResolver.GetReferencedRow<TypeDefRow>(
                            typeDefOrRefOrSpecEncodedSig.RowIndex);
                    name = typeDef.TypeName;
                    @namespace = typeDef.TypeNamespace;
                    break;
                case TypeDefOrRefOrSpec.TypeRef:
                    var typeRef =
                        moduleReaderContext.TableReferenceResolver.GetReferencedRow<TypeRefRow>(
                            typeDefOrRefOrSpecEncodedSig.RowIndex);
                    name = typeRef.TypeName;
                    @namespace = typeRef.TypeNamespace;
                    break;
                case TypeDefOrRefOrSpec.TypeSpec:
                    throw new NotImplementedException("TypeSpec resolving isn't implemented");
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return ClrTypeInfo.CreateSimpleTypeInfo(
                moduleReaderContext.StringsHeapStream.Read(@namespace),
                moduleReaderContext.StringsHeapStream.Read(@name)
            );
        }

        ClrTypeInfo MapSZArraySig(SZArraySig arraySig) => ClrTypeInfo.CreateSZArrayTypeInfo(Map(arraySig.Type));

        ClrTypeInfo MapValueTypeSig(ValueTypeSig valueTypeSig) =>
            ResolveDefOrRefOrSpecEncodedSig(valueTypeSig.TypeDefOrRefOrSpecEncoded);
    }
}