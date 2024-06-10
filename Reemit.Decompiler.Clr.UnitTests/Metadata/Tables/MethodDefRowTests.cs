using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Tables;

public class MethodDefRowTests
{
    [Theory]
    // Invalid impl flags
    [InlineData(
        new byte[]
        {
            // RVA
            0x50, 0x20, 0x00, 0x00,

            // Impl Flags
            0xfe, 0xff,

            // Flags
            0x96, 0x00,

            // Other
            0x6a, 0x03, 0x72, 0x00, 0x01, 0x00
        })]

    // Invalid flag combination (static | final)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x80, 0x00,

            // Flags
            0xb1, 0x20,

            // Other
            0x0c, 0x00, 0x72, 0x00, 0x02, 0x00
        })]

    // Invalid flag combination (static | virtual)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x80, 0x00,

            // Flags
            0xd1, 0x20,

            // Other
            0x0c, 0x00, 0x72, 0x00, 0x02, 0x00
        })]

    // Invalid flag combination (static | newslot)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x80, 0x00,

            // Flags
            0x91, 0x21,

            // Other
            0x0c, 0x00, 0x72, 0x00, 0x02, 0x00
        })]

    // Invalid flag combination (final | abstract)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x00, 0x00,

            // Flags
            0xe4, 0x05,

            // other
            0x8e, 0x03, 0x10, 0x00, 0x01, 0x00
        })]

    // Invalid flag combination (abstract | pinvoke)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x00, 0x00,

            // Flags
            0xc4, 0x25,

            // other
            0x8e, 0x03, 0x10, 0x00, 0x01, 0x00
        })]

    // Invalid flag combination (compilercontrolled | specialname)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x00, 0x00,

            // Flags
            0xc0, 0x0d,

            // other
            0x8e, 0x03, 0x10, 0x00, 0x01, 0x00
        })]

    // Invalid flag combination (compilercontrolled | rtspecialname)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x00, 0x00,

            // Flags
            0xc0, 0x15,

            // other
            0x8e, 0x03, 0x10, 0x00, 0x01, 0x00
        })]

    // Invalid flag combination (abstract & ~virtual)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x00, 0x00,

            // Flags
            0x84, 0x05,

            // other
            0x8e, 0x03, 0x10, 0x00, 0x01, 0x00
        })]

    // Invalid flag combination (rtspecialname & ~specialname)
    [InlineData(
        new byte[]
        {
            // RVA
            0x00, 0x00, 0x00, 0x00,

            // Impl Flags
            0x00, 0x00,

            // Flags
            0xc4, 0x15,

            // other
            0x8e, 0x03, 0x10, 0x00, 0x01, 0x00
        })]
    public void Constructor_InvalidMethodDefRow_ThrowsBadImageFormatException(byte[] bytes)
    {
        // Arrange
        using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);
        var tableReader = new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>());

        // Act
        Action readRow = () => MethodDefRow.Read(tableReader);

        // Assert
        Assert.Throws<BadImageFormatException>(readRow);
    }

    [Theory]
    [MemberData(nameof(GetMethodDefData))]
    public void Constructor_ValidMethodDefRow_ReadsMethodDefRow(
        byte[] bytes,
        uint expectedRva,
        ushort expectedImplFlags,
        ushort expectedFlags,
        uint expectedName,
        uint expectedSignature,
        uint expectedParamList,
        MethodImplCodeTypeAttributes expectedCodeType,
        MethodImplManagedAttributes expectedManaged,
        MethodImplAttributes expectedMethodImpl,
        MethodMemberAccessAttributes expectedMethodMemberAccess,
        MethodVtableLayoutAttributes expectedMethodVtableLayout,
        MethodAttributes expectedMethodFlags,
        bool expectedIsForwardRef,
        bool expectedIsPreserveSig,
        bool expectedIsInternalCall,
        bool expectedIsSynchronized,
        bool expectedIsNoInlining,
        bool expectedIsNoOptimization,
        bool expectedIsMaxMethodImplVal,
        bool expectedIsStatic,
        bool expectedIsFinal,
        bool expectedIsVirtual,
        bool expectedIsHideBySig,
        bool expectedIsStrict,
        bool expectedIsAbstract,
        bool expectedIsSpecialName,
        bool expectedIsPInvokeImpl,
        bool expectedIsUnmanagedExport,
        bool expectedIsRTSpecialName,
        bool expectedHasSecurity,
        bool expectedRequireSecObject)
    {
        // Arrange
        using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);
        var tableReader = new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>());

        // Act
        var row = MethodDefRow.Read(tableReader);

        // Assert
        Assert.Equal(expectedRva, row.Rva);
        Assert.Equal(expectedImplFlags, row.ImplFlags);
        Assert.Equal(expectedFlags, row.Flags);
        Assert.Equal(expectedName, row.Name);
        Assert.Equal(expectedSignature, row.Signature);
        Assert.Equal(expectedParamList, row.ParamList);
        Assert.Equal(expectedCodeType, row.CodeType);
        Assert.Equal(expectedManaged, row.Managed);
        Assert.Equal(expectedMethodImpl, row.MethodImpl);
        Assert.Equal(expectedMethodMemberAccess, row.MethodMemberAccess);
        Assert.Equal(expectedMethodVtableLayout, row.MethodVtableLayout);
        Assert.Equal(expectedMethodFlags, row.MethodFlags);
        Assert.Equal(expectedIsForwardRef, row.IsForwardRef);
        Assert.Equal(expectedIsPreserveSig, row.IsPreserveSig);
        Assert.Equal(expectedIsInternalCall, row.IsInternalCall);
        Assert.Equal(expectedIsSynchronized, row.IsSynchronized);
        Assert.Equal(expectedIsNoInlining, row.IsNoInlining);
        Assert.Equal(expectedIsNoOptimization, row.IsNoOptimization);
        Assert.Equal(expectedIsMaxMethodImplVal, row.IsMaxMethodImplVal);
        Assert.Equal(expectedIsStatic, row.IsStatic);
        Assert.Equal(expectedIsFinal, row.IsFinal);
        Assert.Equal(expectedIsVirtual, row.IsVirtual);
        Assert.Equal(expectedIsHideBySig, row.IsHideBySig);
        Assert.Equal(expectedIsStrict, row.IsStrict);
        Assert.Equal(expectedIsAbstract, row.IsAbstract);
        Assert.Equal(expectedIsSpecialName, row.IsSpecialName);
        Assert.Equal(expectedIsPInvokeImpl, row.IsPInvokeImpl);
        Assert.Equal(expectedIsUnmanagedExport, row.IsUnmanagedExport);
        Assert.Equal(expectedIsRTSpecialName, row.IsRTSpecialName);
        Assert.Equal(expectedHasSecurity, row.HasSecurity);
        Assert.Equal(expectedRequireSecObject, row.RequireSecObject);
    }

    public static IEnumerable<object[]> GetMethodDefData() =>
        new[]
        {
            CreateMethodDefTestCase(
                bytes:
                [
                    // RVA
                    0x50, 0x20, 0x00, 0x00,

                    // Impl Flags
                    0x00, 0x00,
                    
                    // Flags
                    0x96, 0x00,
                    
                    // Other
                    0x6a, 0x03, 0x72, 0x00, 0x01, 0x00
                ],
                expectedRva: 0x2050u,
                expectedImplFlags: 0x0,
                expectedFlags: 0x96,
                expectedName: 0x36au,
                expectedSignature: 0x72u,
                expectedParamList: 0x1u,
                expectedCodeType: MethodImplCodeTypeAttributes.IL,
                expectedManaged: MethodImplManagedAttributes.Managed,
                expectedMethodImpl: MethodImplAttributes.None,
                expectedMethodMemberAccess: MethodMemberAccessAttributes.Public,
                expectedMethodVtableLayout: MethodVtableLayoutAttributes.ReuseSlot,
                expectedMethodFlags: MethodAttributes.Static | MethodAttributes.HideBySig,
                expectedIsForwardRef: false,
                expectedIsPreserveSig: false,
                expectedIsInternalCall: false,
                expectedIsSynchronized: false,
                expectedIsNoInlining: false,
                expectedIsNoOptimization: false,
                expectedIsMaxMethodImplVal: false,
                expectedIsStatic: true,
                expectedIsFinal: false,
                expectedIsVirtual: false,
                expectedIsHideBySig: true,
                expectedIsStrict: false,
                expectedIsAbstract: false,
                expectedIsSpecialName: false,
                expectedIsPInvokeImpl: false,
                expectedIsUnmanagedExport: false,
                expectedIsRTSpecialName: false,
                expectedHasSecurity: false,
                expectedRequireSecObject: false),
            CreateMethodDefTestCase(
                bytes:
                [
                    // RVA
                    0x00, 0x00, 0x00, 0x00,
                    
                    // Impl Flags
                    0x00, 0x00,
                    
                    // Flags
                    0xc4, 0x05,
                    
                    // Other
                    0x8e, 0x03, 0x10, 0x00, 0x01, 0x00
                ],
                expectedRva: 0x0u,
                expectedImplFlags: 0x0,
                expectedFlags: 0x05c4,
                expectedName: 0x38eu,
                expectedSignature: 0x10u,
                expectedParamList: 0x1u,
                expectedCodeType: MethodImplCodeTypeAttributes.IL,
                expectedManaged: MethodImplManagedAttributes.Managed,
                expectedMethodImpl: MethodImplAttributes.None,
                expectedMethodMemberAccess: MethodMemberAccessAttributes.Family,
                expectedMethodVtableLayout: MethodVtableLayoutAttributes.NewSlot,
                expectedMethodFlags: MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.Abstract,
                expectedIsForwardRef: false,
                expectedIsPreserveSig: false,
                expectedIsInternalCall: false,
                expectedIsSynchronized: false,
                expectedIsNoInlining: false,
                expectedIsNoOptimization: false,
                expectedIsMaxMethodImplVal: false,
                expectedIsStatic: false,
                expectedIsFinal: false,
                expectedIsVirtual: true,
                expectedIsHideBySig: true,
                expectedIsStrict: false,
                expectedIsAbstract: true,
                expectedIsSpecialName: false,
                expectedIsPInvokeImpl: false,
                expectedIsUnmanagedExport: false,
                expectedIsRTSpecialName: false,
                expectedHasSecurity: false,
                expectedRequireSecObject: false),
            CreateMethodDefTestCase(
                bytes:
                [
                    // RVA
                    0x00, 0x00, 0x00, 0x00,
                    
                    // Impl Flags
                    0x80, 0x00,
                    

                    // Flags
                    0x91, 0x20,
                    
                    // Other
                    0x0c, 0x00, 0x72, 0x00, 0x02, 0x00
                ],
                expectedRva: 0x0u,
                expectedImplFlags: 0x80,
                expectedFlags: 0x2091,
                expectedName: 0xcu,
                expectedSignature: 0x72u,
                expectedParamList: 0x2u,
                expectedCodeType: MethodImplCodeTypeAttributes.IL,
                expectedManaged: MethodImplManagedAttributes.Managed,
                expectedMethodImpl: MethodImplAttributes.PreserveSig,
                expectedMethodMemberAccess: MethodMemberAccessAttributes.Private,
                expectedMethodVtableLayout: MethodVtableLayoutAttributes.ReuseSlot,
                expectedMethodFlags: MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.PInvokeImpl,
                expectedIsForwardRef: false,
                expectedIsPreserveSig: true,
                expectedIsInternalCall: false,
                expectedIsSynchronized: false,
                expectedIsNoInlining: false,
                expectedIsNoOptimization: false,
                expectedIsMaxMethodImplVal: false,
                expectedIsStatic: true,
                expectedIsFinal: false,
                expectedIsVirtual: false,
                expectedIsHideBySig: true,
                expectedIsStrict: false,
                expectedIsAbstract: false,
                expectedIsSpecialName: false,
                expectedIsPInvokeImpl: true,
                expectedIsUnmanagedExport: false,
                expectedIsRTSpecialName: false,
                expectedHasSecurity: false,
                expectedRequireSecObject: false),
            CreateMethodDefTestCase(
                bytes:
                [
                    // RVA
                    0x50, 0x20, 0x00, 0x00,
                    
                    // Impl Flags
                    0x08, 0x00,
                    
                    // Flags
                    0xc4, 0x01,
                    
                    // Other
                    0x1a, 0x00, 0x79, 0x00, 0x05, 0x00
                ],
                expectedRva: 0x2050u,
                expectedImplFlags: 0x08,
                expectedFlags: 0x01c4,
                expectedName: 0x1au,
                expectedSignature: 0x79u,
                expectedParamList: 0x5u,
                expectedCodeType: MethodImplCodeTypeAttributes.IL,
                expectedManaged: MethodImplManagedAttributes.Managed,
                expectedMethodImpl: MethodImplAttributes.NoInlining,
                expectedMethodMemberAccess: MethodMemberAccessAttributes.Family,
                expectedMethodVtableLayout: MethodVtableLayoutAttributes.NewSlot,
                expectedMethodFlags: MethodAttributes.Virtual | MethodAttributes.HideBySig,
                expectedIsForwardRef: false,
                expectedIsPreserveSig: false,
                expectedIsInternalCall: false,
                expectedIsSynchronized: false,
                expectedIsNoInlining: true,
                expectedIsNoOptimization: false,
                expectedIsMaxMethodImplVal: false,
                expectedIsStatic: false,
                expectedIsFinal: false,
                expectedIsVirtual: true,
                expectedIsHideBySig: true,
                expectedIsStrict: false,
                expectedIsAbstract: false,
                expectedIsSpecialName: false,
                expectedIsPInvokeImpl: false,
                expectedIsUnmanagedExport: false,
                expectedIsRTSpecialName: false,
                expectedHasSecurity: false,
                expectedRequireSecObject: false),
            CreateMethodDefTestCase(
                bytes:
                [
                    // RVA
                    0x50, 0x20, 0x00, 0x00,

                    // Impl Flags
                    0xff, 0xff,
                    
                    // Flags
                    0x96, 0x00,
                    
                    // Other
                    0x6a, 0x03, 0x72, 0x00, 0x01, 0x00
                ],
                expectedRva: 0x2050u,
                expectedImplFlags: 0xffff,
                expectedFlags: 0x96,
                expectedName: 0x36au,
                expectedSignature: 0x72u,
                expectedParamList: 0x1u,
                expectedCodeType: MethodImplCodeTypeAttributes.Runtime,
                expectedManaged: MethodImplManagedAttributes.Unmanaged,
                expectedMethodImpl: MethodImplAttributes.Mask,
                expectedMethodMemberAccess: MethodMemberAccessAttributes.Public,
                expectedMethodVtableLayout: MethodVtableLayoutAttributes.ReuseSlot,
                expectedMethodFlags: MethodAttributes.Static | MethodAttributes.HideBySig,
                expectedIsForwardRef: true,
                expectedIsPreserveSig: true,
                expectedIsInternalCall: true,
                expectedIsSynchronized: true,
                expectedIsNoInlining: true,
                expectedIsNoOptimization: true,
                expectedIsMaxMethodImplVal: true,
                expectedIsStatic: true,
                expectedIsFinal: false,
                expectedIsVirtual: false,
                expectedIsHideBySig: true,
                expectedIsStrict: false,
                expectedIsAbstract: false,
                expectedIsSpecialName: false,
                expectedIsPInvokeImpl: false,
                expectedIsUnmanagedExport: false,
                expectedIsRTSpecialName: false,
                expectedHasSecurity: false,
                expectedRequireSecObject: false),
        };

    private static object[] CreateMethodDefTestCase(
        byte[] bytes,
        uint expectedRva,
        ushort expectedImplFlags,
        ushort expectedFlags,
        uint expectedName,
        uint expectedSignature,
        uint expectedParamList,
        MethodImplCodeTypeAttributes expectedCodeType,
        MethodImplManagedAttributes expectedManaged,
        MethodImplAttributes expectedMethodImpl,
        MethodMemberAccessAttributes expectedMethodMemberAccess,
        MethodVtableLayoutAttributes expectedMethodVtableLayout,
        MethodAttributes expectedMethodFlags,
        bool expectedIsForwardRef,
        bool expectedIsPreserveSig,
        bool expectedIsInternalCall,
        bool expectedIsSynchronized,
        bool expectedIsNoInlining,
        bool expectedIsNoOptimization,
        bool expectedIsMaxMethodImplVal,
        bool expectedIsStatic,
        bool expectedIsFinal,
        bool expectedIsVirtual,
        bool expectedIsHideBySig,
        bool expectedIsStrict,
        bool expectedIsAbstract,
        bool expectedIsSpecialName,
        bool expectedIsPInvokeImpl,
        bool expectedIsUnmanagedExport,
        bool expectedIsRTSpecialName,
        bool expectedHasSecurity,
        bool expectedRequireSecObject) =>
        [
            bytes,
            expectedRva,
            expectedImplFlags,
            expectedFlags,
            expectedName,
            expectedSignature,
            expectedParamList,
            expectedCodeType,
            expectedManaged,
            expectedMethodImpl,
            expectedMethodMemberAccess,
            expectedMethodVtableLayout,
            expectedMethodFlags,
            expectedIsForwardRef,
            expectedIsPreserveSig,
            expectedIsInternalCall,
            expectedIsSynchronized,
            expectedIsNoInlining,
            expectedIsNoOptimization,
            expectedIsMaxMethodImplVal,
            expectedIsStatic,
            expectedIsFinal,
            expectedIsVirtual,
            expectedIsHideBySig,
            expectedIsStrict,
            expectedIsAbstract,
            expectedIsSpecialName,
            expectedIsPInvokeImpl,
            expectedIsUnmanagedExport,
            expectedIsRTSpecialName,
            expectedHasSecurity,
            expectedRequireSecObject
        ];
}
