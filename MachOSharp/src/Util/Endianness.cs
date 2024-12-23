namespace MachOSharp.Util;

public enum Endianness {

    LittleEndian,
    BigEndian,

}

internal static class Extensions {

    public static BinaryReader AsEndianness(this BinaryReader reader, Endianness endianness) {
        if (endianness == Endianness.LittleEndian ^ BitConverter.IsLittleEndian) {
            if (reader is FlippedEndiannessBinaryReader flipped) {
                return flipped.BaseBinaryReader;
            }
            return new FlippedEndiannessBinaryReader(reader);
        }

        return reader;
    }

}
