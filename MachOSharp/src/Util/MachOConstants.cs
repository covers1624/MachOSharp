namespace MachOSharp.Util;

public class MachOConstants {

    public const uint HEADER_MAGIC = 0xfeedface; // LE
    public const uint HEADER_CIGAM = 0xcefaedfe; // BE

    public const uint HEADER_MAGIC_64 = 0xfeedfacf; // LE 
    public const uint HEADER_CIGAM_64 = 0xcffaedfe; // BE

    public const uint FAT_MAGIC = 0xcafebabe; // LE
    public const uint FAT_CIGAM = 0xbebafeca; // BE

}

public enum MachOType {

    MachO,
    MachO64,
    MachOFat,

}
