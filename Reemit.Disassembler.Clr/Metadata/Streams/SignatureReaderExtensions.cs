using System.Runtime.CompilerServices;
using Reemit.Common;

namespace Reemit.Disassembler.Clr.Metadata.Streams;

public static class SignatureReaderExtensions
{
    public static uint ReadSignatureUInt(this SharedReader reader)
    {
        var firstByte = reader.ReadByte();

        // NOTE: All the comments below contain instructions from ECMA-335 (II.23.2 Blobs and signatures) describing
        //       the compression algorithm. The code under them, however, is doing decompression.
        
        /*
         * If the value lies between 0 (0x00) and 127 (0x7F), inclusive, encode as a one-byte integer (bit 7 is clear,
         * value held in bits 6 through 0)
         */
        const int encodeAsSingleByteIntegerMask = 0b10000000;
        if ((firstByte & encodeAsSingleByteIntegerMask) == 0)
        {
            return firstByte;
        }

        /*
         * If the value lies between 28 (0x80) and 214 – 1 (0x3FFF), inclusive, encode as a 2-byte integer with bit 15
         * set, bit 14 clear (value held in bits 13 through 0)
         */
        const int encodeAsTwoByteIntegerMask = 0b11000000;
        if ((firstByte & encodeAsTwoByteIntegerMask) == 0b10000000)
        {
            var secondByte = reader.ReadByte();
            Span<byte> val = [(byte)(firstByte & ~encodeAsTwoByteIntegerMask), secondByte];

            if (BitConverter.IsLittleEndian)
            {
                val.Reverse();
            }

            return BitConverter.ToUInt16(val);
        }

        /*
         * Otherwise, encode as a 4-byte integer, with bit 31 set, bit 30 set, bit 29 clear (value held in bits 28
         * through 0)
         */
        const int encodeAsFourByteIntegerMask = 0b11100000;
        if ((firstByte & encodeAsFourByteIntegerMask) == 0b11000000)
        {
            var bytes = reader.ReadBytes(3);
            Span<byte> val = [(byte)(firstByte & ~encodeAsFourByteIntegerMask), bytes[0], bytes[1], bytes[2]];

            if (BitConverter.IsLittleEndian)
            {
                val.Reverse();
            }

            return BitConverter.ToUInt32(val);
        }
     
        throw new BadImageFormatException("The compressed unsigned integer didn't match any known pattern");
    }

    public static RangeMapped<uint> ReadMappedSignatureUInt(this SharedReader reader)
    {
        using (var scope = reader.CreateRangeScope())
        {
            var value = ReadSignatureUInt(reader);
            return scope.ToRangeMapped(value);
        }
    }

    public static int ReadSignatureInt(this SharedReader reader)
    {
        var firstByte = reader.ReadByte();
        
        // NOTE: All the comments below contain instructions from ECMA-335 (II.23.2 Blobs and signatures) describing
        //       the compression algorithm. The code under them, however, is doing decompression.

        /*
         * If the value lies between -2^6 and 2^6 - 1 inclusive:
         * - Represent the value as a 7-bit 2’s complement number, giving 0x40 (-2^6) to 0x3F (2^6 - 1);
         * - Rotate this value 1 bit left, giving 0x01 (-2^6) to 0x7E (2^6 - 1);
         * - Encode as a one-byte integer, bit 7 clear, rotated value in bits 6 through 0, giving 0x01 (-2^6) to
         *   0x7E (2^6 - 1). 
         */
        const int encodeAsSingleByteIntegerMask = 0b10000000;
        if ((firstByte & encodeAsSingleByteIntegerMask) == 0)
        {
            return MakeTwosComplement((int)RotateRight(firstByte, 7), 7);
        }

        /*
         * If the value lies between -2^13 and 2^13 - 1 inclusive:
         * - Represent the value as a 14-bit 2’s complement number, giving 0x2000 (-2^13) to 0x1FFF (2^13 - 1);
         * - Rotate this value 1 bit left, giving 0x0001 (-213) to 0x3FFE (213-1);
         * - Encode as a two-byte integer: bit 15 set, bit 14 clear, rotated value in bits 13 through 0, giving 0x8001
         *   (-2^13) to 0xBFFE (2^13 - 1).
         */
        const int encodeAsTwoByteIntegerMask = 0b11000000;
        if ((firstByte & encodeAsTwoByteIntegerMask) == 0b10000000)
        {
            var secondByte = reader.ReadByte();
            Span<byte> val = [(byte)(firstByte & ~encodeAsTwoByteIntegerMask), secondByte];
            
            if (BitConverter.IsLittleEndian)
            {
                val.Reverse();
            }

            var value = BitConverter.ToUInt16(val);

            return MakeTwosComplement((int)RotateRight(value, 14), 14);
        }

        /*
         * If the value lies between -2^28 and 2^28 - 1 inclusive:
         * - Represent the value as a 29-bit 2’s complement representation, giving 0x10000000 (-2^28) to
         *   0xFFFFFFF (2^28 - 1);
         * - Rotate this value 1-bit left, giving 0x00000001 (-2^28) to 0x1FFFFFFE (2^28 - 1);
         * - Encode as a four-byte integer: bit 31 set, 30 set, bit 29 clear, rotated value in bits 28 through 0,
         *   giving 0xC0000001 (-2^28) to 0xDFFFFFFE (2^28 - 1). 
         */
        const int encodeAsFourByteIntegerMask = 0b11100000;
        if ((firstByte & encodeAsFourByteIntegerMask) == 0b11000000)
        {
            var bytes = reader.ReadBytes(3);
            Span<byte> val = [(byte)(firstByte & ~encodeAsFourByteIntegerMask), bytes[0], bytes[1], bytes[2]];

            if (BitConverter.IsLittleEndian)
            {
                val.Reverse();
            }

            var value = BitConverter.ToUInt32(val);

            return MakeTwosComplement((int)RotateRight(value, 29), 29);
        }
     
        throw new BadImageFormatException("The compressed integer didn't match any known pattern");
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static uint RotateRight(uint value, int rotateBits) => ((value & 1) << (rotateBits - 1)) | (value >> 1);

        static int MakeTwosComplement(int value, int bits)
        {
            var signMask = 1 << (bits - 1);

            if ((value & signMask) == 0)
            {
                return value;
            }
            else
            {
                return (~(value - 1) & ((1 << bits) - 1)) * -1;
            }
        }
    }

    public static RangeMapped<int> ReadMappedSignatureInt(this SharedReader reader)
    {
        using (var scope = reader.CreateRangeScope())
        {
            var value = ReadSignatureInt(reader);
            return scope.ToRangeMapped(value);
        }
    }
}