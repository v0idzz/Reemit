using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Reemit.Decompiler;

public class ClrTypeInfo
{
    private ClrTypeInfo(string @namespace, string name, string? alias = null)
    {
        Namespace = @namespace;
        Name = name;
        Alias = alias;
    }

    private ClrTypeInfo(ClrTypeInfo elementType, int rank)
    {
        Namespace = elementType.Namespace;
        IsArray = true;
        ElementType = elementType;
        ArrayRank = rank;

        var sb = new StringBuilder(
            elementType.AliasOrName,
            // type name, "," times array rank, "[]"; for example, TypeName[,,,]
            elementType.AliasOrName.Length + (rank - 1) + 2);

        sb.Append('[');

        for (var i = 0; i < rank - 1; i++)
        {
            sb.Append(',');
        }

        sb.Append(']');

        Name = sb.ToString();
    }

    private ClrTypeInfo(ClrTypeInfo elementType) : this(elementType, 1)
    {
        IsSZArray = true;
    }

    private ClrTypeInfo(string @namespace, string name, IReadOnlyList<ClrTypeInfo> genericArguments)
    {
        GenericArguments = genericArguments;
        IsGenericType = true;
        Namespace = @namespace;
        
        var nameBuilder = new StringBuilder(name[..name.LastIndexOf('`')]);
        nameBuilder.Append('<');
        nameBuilder.Append(
            string.Join(", ",
                genericArguments
                    .Select(typeInfo => typeInfo.AliasOrName)));
        nameBuilder.Append('>');

        Name = nameBuilder.ToString();
    }

    [MemberNotNullWhen(true, nameof(GenericArguments))]
    public bool IsGenericType { get; }

    public IReadOnlyList<ClrTypeInfo>? GenericArguments { get; }

    [MemberNotNullWhen(true, nameof(ElementType))]
    public bool IsSZArray { get; }

    [MemberNotNullWhen(true, nameof(ElementType))]
    public bool IsArray { get; }

    public int ArrayRank { get; }

    public ClrTypeInfo? ElementType { get; }
    
    public string AliasOrName => Alias ?? Name;
    
    public string Namespace { get; }
    
    public string Name { get; }
    
    public string? Alias { get; }

    public static ClrTypeInfo CreateSimpleTypeInfo(string @namespace, string name, string? alias = null)
        => new(@namespace, name, alias);

    public static ClrTypeInfo CreateArrayTypeInfo(ClrTypeInfo elementType, int rank)
        => new(elementType, rank);

    public static ClrTypeInfo CreateSZArrayTypeInfo(ClrTypeInfo elementType)
        => new(elementType);

    public static ClrTypeInfo CreateGenericTypeInfo(string @namespace, string name,
        IReadOnlyList<ClrTypeInfo> genericArguments)
        => new(@namespace, name, genericArguments);
}