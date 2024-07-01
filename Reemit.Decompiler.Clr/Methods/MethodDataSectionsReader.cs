using Reemit.Common;

namespace Reemit.Decompiler.Clr.Methods;

public static class MethodDataSectionsReader
{
    public static IEnumerable<IMethodDataSection> ReadMethodDataSections(SharedReader reader)
    {
        var hasMore = true;
        while (hasMore)
        {
            var paddingBytes = reader.Offset % 4;

            if (paddingBytes != 0)
            {
                reader = reader.CreateDerivedAtRelativeToCurrentOffset((uint)(4 - paddingBytes));
            }

            // Create a temporary reader to peek first byte.
            var tempReader = reader.CreateDerivedAtRelativeToCurrentOffset(0);
            var flags = (CorILMethodSectionFlags)tempReader.ReadByte();

            if (flags.HasFlag(CorILMethodSectionFlags.FatFormat))
            {
                yield return FatExceptionHeader.Read(reader);
            }
            else
            {
                yield return SmallExceptionHeader.Read(reader);
            }

            hasMore = flags.HasFlag(CorILMethodSectionFlags.MoreSects);
        }
    }
}