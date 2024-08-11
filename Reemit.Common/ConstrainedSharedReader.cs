namespace Reemit.Common;

public class ConstrainedSharedReader(int startOffset, int length, BinaryReader reader) : SharedReader(startOffset, reader)
{
    public int Length => length;

    public bool IsEndOfStream => RelativeOffset == length;
}