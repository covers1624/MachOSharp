using System.Text;
using MachOSharp.Util;

namespace MachOSharp;

public class MachOReader {

    public static bool TryLoadFat(Stream stream, out MachOFat? machoFat, bool leaveOpen = false) {
        machoFat = null;

        using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen);

        uint magic = reader.ReadUInt32();
        if (DetectMagic(magic) is not (type: MachOType.MachOFat, endianness: var endianness)) return false;

        var endianReader = reader.AsEndianness(endianness);
        machoFat = new MachOFat(endianReader);
        return true;
    }

    public static bool TryLoad64(Stream stream, out MachO64? macho, bool leaveOpen = false) {
        macho = null;

        using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen);

        long pos = stream.Position;
        uint magic = reader.ReadUInt32();
        if (DetectMagic(magic) is not (type: MachOType.MachO64, endianness: var endianness)) return false;

        var endianReader = reader.AsEndianness(endianness);
        macho = new MachO64(endianReader, pos);
        return true;
    }

    public static (MachOType type, Endianness endianness)? DetectMagic(uint magic) {
        return magic switch {
            MachOConstants.HEADER_MAGIC => (MachOType.MachO, Endianness.LittleEndian),
            MachOConstants.HEADER_CIGAM => (MachOType.MachO, Endianness.BigEndian),
            MachOConstants.HEADER_MAGIC_64 => (MachOType.MachO64, Endianness.LittleEndian),
            MachOConstants.HEADER_CIGAM_64 => (MachOType.MachO64, Endianness.BigEndian),
            MachOConstants.FAT_MAGIC => (MachOType.MachOFat, Endianness.LittleEndian),
            MachOConstants.FAT_CIGAM => (MachOType.MachOFat, Endianness.BigEndian),
            _ => null
        };
    }

}
