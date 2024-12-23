using MachOSharp.Util;

namespace MachOSharp;

/// <summary>
/// Represents a Fat MachO file, as defined in <see href="https://github.com/apple-oss-distributions/xnu/blob/main/EXTERNAL_HEADERS/mach-o/fat.h">mach-o/fat.h</see>
/// </summary>
public class MachOFat {

    private readonly List<MachOFatArch> arches = [];

    public IReadOnlyList<MachOFatArch> Arches => arches;

    public MachOFat(BinaryReader reader) {
        uint nfatArch = reader.ReadUInt32();
        for (int i = 0; i < nfatArch; i++) {
            CpuType cpuType = (CpuType)reader.ReadInt32();
            int cpuSubType = reader.ReadInt32();
            uint offset = reader.ReadUInt32();
            uint size = reader.ReadUInt32();
            uint align = reader.ReadUInt32();

            long prevPos = reader.BaseStream.Position;
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            if (!MachOReader.TryLoad64(reader.BaseStream, out var macho, true) || macho == null) {
                throw new ApplicationException("Currently only support Fat MachO files with 64 bit Macho binaries.");
            }

            arches.Add(new MachOFatArch(cpuType, cpuSubType, offset, size, align, macho));
            
            reader.BaseStream.Seek(prevPos, SeekOrigin.Begin);
        }
    }
}

public record MachOFatArch(
    CpuType cpuType,
    int cpuSubType,
    uint offset,
    uint size,
    uint align,
    MachO64 macho
);
