using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Reemit.Disassembler;

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

        /*
         * This is a hackish way to tell whether the type itself accepts generic type args or if the generic type args
         * are inferred from the inheritance hierarchy of the class where the type is referenced.
         *
         * Consider this example:
         *
         * ```csharp
         * class GenericClass<T>
         * {
         *     public delegate T GenericMethod(T c);
         * }
         *
         * class B : GenericClass<int>
         * {
         *     public B(GenericMethod genericMethod) {}
         * }
         * ```
         *
         * Looking at the code above, we can tell the `GenericMethod genericMethod` .ctor() parameter has `int` as its
         * generic type arg.
         *
         * The type for `GenericMethod genericMethod` is encoded as ELEMENT_TYPE_GENERICINST.
         * This gives us the information about the type parameter being `int` even though we cannot write the parameter
         * type as `GenericMethod<int>` in the example above as that would result in invalid C#.
         *
         * I am sure that checking whether the generic args need to be appended to the type name could be done in a better
         * way than looking for the backtick, but this keeps us going for now.
         */
        var indexOfTick = name.LastIndexOf('`');
        if (indexOfTick != -1)
        {
            var nameBuilder = new StringBuilder(name[..indexOfTick]);
            nameBuilder.Append('<');
            nameBuilder.Append(
                string.Join(", ",
                    genericArguments
                        .Select(typeInfo => typeInfo.AliasOrName)));
            nameBuilder.Append('>');

            Name = nameBuilder.ToString();
        }
        else
        {
            Name = name;
        }
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