using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.Signatures;

public record CustomModSig(
    bool CmodOpt,
    bool CmodReqd,
    TypeDefOrRefOrSpecEncodedSig TypeDefOrRefOrSpecEncoded)
{
    public static CustomModSig Read(SharedReader reader)
    {
        var mod = (ElementType)reader.ReadSignatureUInt();
        var opt = mod == ElementType.CmodOpt;
        var reqd = mod == ElementType.CmodReqd;
        var typeDefOrRefOrSpecEncoded = TypeDefOrRefOrSpecEncodedSig.Read(reader);

        return new CustomModSig(opt, reqd, typeDefOrRefOrSpecEncoded);
    }

    public static IReadOnlyList<CustomModSig> ReadAll(SharedReader reader)
    {
        var customMods = new List<CustomModSig>();
        while (true)
        {
            var lookAheadReader = reader.CreateDerivedAtRelativeToCurrentOffset(0);
            var value = (ElementType)lookAheadReader.ReadSignatureUInt();
            if (value is ElementType.CmodOpt or ElementType.CmodReqd)
            {
                var mod = Read(reader);
                customMods.Add(mod);
            }
            else
            {
                break;
            }
        }

        return customMods.AsReadOnly();
    }
}