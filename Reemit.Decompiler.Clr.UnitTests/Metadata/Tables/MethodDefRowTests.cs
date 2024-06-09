using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Tables;

public class MethodDefRowTests
{
    [Theory]
    [MemberData(nameof(GetMethodDefData))]
    public async Task Constructor_ValidMethodDefRow_ReadsMethodDefRow(
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
        await using var memoryStream = new MemoryStream(bytes);
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
                bytes: [0x50, 0x20, 0x00, 0x00, 0x00, 0x00, 0x96, 0x00, 0x6a, 0x03, 0x72, 0x00, 0x01, 0x00],
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
                bytes: [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xc4, 0x05, 0x8e, 0x03, 0x10, 0x00, 0x01, 0x00],
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
