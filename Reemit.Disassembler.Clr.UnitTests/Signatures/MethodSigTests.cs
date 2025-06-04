using Reemit.Common;
using Reemit.Disassembler.Clr.Signatures;
using Reemit.Disassembler.Clr.Signatures.Types;

namespace Reemit.Disassembler.Clr.UnitTests.Signatures;

public class MethodSigTests
{
    [Fact]
    public void Read_ValidMethodSigWithGenericCallingConvention_ReadsMethodSigIncludingGenParamCount()
    {
        // Arrange
        byte[] bytes =
        [
            // Calling Convention (Generic | HasThis)
            0x30,

            // GenParamCount
            0x01,

            // ParamCount
            0x02,

            // RetType (ELEMENT_TYPE_SZARRAY, ELEMENT_TYPE_I4)
            0x1D, 0x08,

            // param 1 (ByRef, ELEMENT_TYPE_MVAR, MVar number = 0)
            0x10, 0x1E, 0x00,

            // param 2 (ELEMENT_TYPE_SZARRAY, ELEMENT_TYPE_I4)
            0x1D, 0x08,
        ];

        using var ms = new MemoryStream(bytes);
        using var reader = new ConstrainedSharedReader(0, bytes.Length, new BinaryReader(ms));
        
        // Act
        var methodSig = (GenericMethodSig)MethodSig.Read(reader);
        
        // Assert
        Assert.Equal(MethodDefSigCallingConvention.Generic | MethodDefSigCallingConvention.HasThis,
            methodSig.CallingConvention);
        Assert.Equal(1u, methodSig.GenParamCount);
        Assert.Collection(methodSig.Params, param =>
            {
                Assert.True(param.ByRef);
                Assert.Empty(param.CustomMods);
                Assert.Equal(0u, ((MVarSig)param.Type!).Number);
            },
            param =>
            {
                Assert.False(param.ByRef);
                Assert.Empty(param.CustomMods);
                var type = (SZArraySig)param.Type!;
                Assert.Equal(NativeType.I4, ((NativeTypeSig)type.Type).NativeType);
                Assert.Empty(type.CustomMods);
            });
        var retType = methodSig.RetType;
        Assert.False(retType.ByRef);
        Assert.Empty(retType.CustomMods);
        var retTypeType = (SZArraySig)retType.Type!;
        Assert.Empty(retTypeType.CustomMods);
        Assert.Equal(NativeType.I4, ((NativeTypeSig)retTypeType.Type).NativeType);
    }
    
    [Fact]
    public void Read_ValidMethodSig_ReadsMethodSig()
    {
        // Arrange
        byte[] bytes =
        [
            // Calling Convention (HasThis)
            0x20,

            // ParamCount
            0x01,

            // RetType (ELEMENT_TYPE_I4)
            0x08,

            // param 1 (ELEMENT_TYPE_I4)
            0x08
        ];

        using var ms = new MemoryStream(bytes);
        using var reader = new ConstrainedSharedReader(0, bytes.Length, new BinaryReader(ms));
        
        // Act
        var methodSig = MethodSig.Read(reader);
        
        // Assert
        Assert.Equal(MethodDefSigCallingConvention.HasThis,
            methodSig.CallingConvention);
        Assert.Collection(methodSig.Params, param =>
        {
            Assert.False(param.ByRef);
            Assert.Empty(param.CustomMods);
            Assert.Equal(NativeType.I4, ((NativeTypeSig)param.Type!).NativeType);
        });
        var retType = methodSig.RetType;
        Assert.False(retType.ByRef);
        Assert.Empty(retType.CustomMods);
        Assert.Equal(NativeType.I4, ((NativeTypeSig)retType.Type!).NativeType);
    }
}