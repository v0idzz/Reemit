using Reemit.Common;
using Reemit.Disassembler.Clr.Signatures;
using Reemit.Disassembler.Clr.Signatures.Types;

namespace Reemit.Disassembler.Clr.UnitTests.Signatures;

public class TypeSigReaderTests
{
    [Fact]
    public void Read_ValidArrayTypeSig_ReadsArrayTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_ARRAY
            0x14,
            
            // Type
            0x0D,
            
            // Rank
            0x02,

            // NumSizes
            0x00,

            // NumLoBounds
            0x02,

            // LoBounds
            0x00, 0x00
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var arrayShapeSig = (ArraySig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Equal(NativeType.R8, ((NativeTypeSig)arrayShapeSig.Type).NativeType);
        Assert.Equal(0x02u, arrayShapeSig.Shape.Rank);
        Assert.Empty(arrayShapeSig.Shape.Sizes);
        Assert.Equal([0x00u, 0x00u], arrayShapeSig.Shape.LoBounds);
    }
    
    [Fact]
    public void Read_ValidClassTypeSig_ReadsClassTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_CLASS
            0x12,
            
            // TypeDefOrRefOrSpecEncoded
            0x80, 0x85
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var typeSig = (ClassSig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Equal(33u, typeSig.TypeDefOrRefOrSpecEncoded.RowIndex);
        Assert.Equal(TypeDefOrRefOrSpec.TypeRef, typeSig.TypeDefOrRefOrSpecEncoded.TypeDefOrRefOrSpec);
    }
    
    [Fact]
    public void Read_ValidFnPtrSig_ReadsClassTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_FNPTR
            0x1B,
            
            // MethodDef
            0x00, 0x01, 0x01, 0x08
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var fnPtrSig = (FnPtrSig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Collection(fnPtrSig.MethodSig.Params, paramSig =>
        {
            Assert.Empty(paramSig.CustomMods);
            Assert.Equal(NativeType.I4, ((NativeTypeSig)paramSig.Type!).NativeType);
        });
        var retType = fnPtrSig.MethodSig.RetType;
        Assert.False(retType.ByRef);
        Assert.Empty(retType.CustomMods);
        Assert.Equal(NativeType.Void, ((NativeTypeSig)retType.Type!).NativeType);
    }
    
    [Fact]
    public void Read_ValidGenericInstTypeSig_ReadsGenericInstTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_GENERICINST
            0x15,
            
            // ELEMENT_TYPE_CLASS
            0x12,
            
            // TypeDefOrRefOrSpecEncoded
            0x80, 0x89,
            
            // GenArgCount
            0x01,
            
            // ELEMENT_TYPE_VAR
            0x13,
            
            // VAR number
            0x00
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var genericInstSig = (GenericInstSig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Equal(GenericInstSigTypeKind.Class, genericInstSig.TypeKind);
        Assert.Equal(34u, genericInstSig.Type.RowIndex);
        Assert.Equal(TypeDefOrRefOrSpec.TypeRef, genericInstSig.Type.TypeDefOrRefOrSpec);
        Assert.Collection(genericInstSig.GenericArguments, paramSig =>
        {
            Assert.Empty(paramSig.CustomMods);
            Assert.False(paramSig.ByRef);
            Assert.Equal(0u, ((VarSig)paramSig.Type!).Number);
        });
    }
    
    [Fact]
    public void Read_ValidMVarTypeSig_ReadsMVarTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_MVAR
            0x1E,
            
            // MVAR number
            0x00
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var mVarSig = (MVarSig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Equal(0u, mVarSig.Number);
    }
    
    [Fact]
    public void Read_ValidNativeTypeSig_ReadsNativeTypeSig()
    {
        foreach (var nativeType in Enum.GetValues<NativeType>())
        {
            // Arrange
            byte[] bytes = [(byte)nativeType];

            using var reader = CreateReader(bytes);

            // Act
            var typeSig = (NativeTypeSig)TypeSigReader.ReadType(reader);

            // Assert
            Assert.Equal(nativeType, typeSig.NativeType);
        }
    }
    
    [Fact]
    public void Read_ValidPtrTypeSig_ReadsPtrTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_PTR
            0x0F,
            
            // ELEMENT_TYPE_I4
            0x08
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var ptrSig = (PtrSig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Equal(NativeType.I4, ((NativeTypeSig)ptrSig.Type).NativeType);
    }
    
    [Fact]
    public void Read_ValidSZArrayTypeSig_ReadsSZArrayTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_SZARRAY
            0x1D,
            
            // ELEMENT_TYPE_R8
            0x0D
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var szArraySig = (SZArraySig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Empty(szArraySig.CustomMods);
        Assert.Equal(NativeType.R8, ((NativeTypeSig)szArraySig.Type).NativeType);
    }

    [Fact]
    public void Read_ValidValueTypeTypeSig_ReadsValueTypeTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_VALUETYPE
            0x11,

            // TypeDefOrRefOrSpecEncoded
            0x69
        ];

        using var reader = CreateReader(bytes);

        // Act
        var valueTypeSig = (ValueTypeSig)TypeSigReader.ReadType(reader);

        // Assert
        Assert.Equal(26u, valueTypeSig.TypeDefOrRefOrSpecEncoded.RowIndex);
        Assert.Equal(TypeDefOrRefOrSpec.TypeRef, valueTypeSig.TypeDefOrRefOrSpecEncoded.TypeDefOrRefOrSpec);
    }
    
    [Fact]
    public void Read_ValidVarTypeSig_ReadsVarTypeSig()
    {
        // Arrange
        byte[] bytes =
        [
            // ELEMENT_TYPE_VAR
            0x13,
            
            // VAR number
            0x00
        ];

        using var reader = CreateReader(bytes);
        
        // Act
        var varSig = (VarSig)TypeSigReader.ReadType(reader);
        
        // Assert
        Assert.Equal(0u, varSig.Number);
    }

    private static ConstrainedSharedReader CreateReader(byte[] sourceBytes)
    {
        var ms = new MemoryStream(sourceBytes);
        return new ConstrainedSharedReader(0, sourceBytes.Length, new SharedReader(0, new BinaryReader(ms)));
    }
}