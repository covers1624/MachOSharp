using MachOSharp.Command;
using MachOSharp.Util;

namespace MachOSharp;

/// <summary>
/// Represents a MachO 64 file, as defined in <see href="https://github.com/apple-oss-distributions/xnu/blob/main/EXTERNAL_HEADERS/mach-o/loader.h">mach-o/loader.h</see>
/// </summary>
public class MachO64 {

    /// <summary>
    /// The offset in the given initial stream this file was read from.
    /// </summary>
    public long FileOffset { get; }

    public CpuType CpuType { get; }
    public int CpuSubType { get; }
    public MachOFileType FileType { get; }
    public MachOFlags Flags { get; }

    public IEnumerable<LoadCommand> LoadCommands { get; }

    public MachO64(BinaryReader reader, long fileOffset) {
        FileOffset = fileOffset;
        CpuType = (CpuType)reader.ReadInt32();
        CpuSubType = reader.ReadInt32();

        FileType = (MachOFileType)reader.ReadUInt32();
        uint numCmds = reader.ReadUInt32();
        uint sizeOfCmds = reader.ReadUInt32();
        Flags = (MachOFlags)reader.ReadUInt32();
        reader.ReadBytes(4); // Reserved

        List<LoadCommand> commands = [];
        LoadCommands = commands;
        for (uint i = 0; i < numCmds; i++) {
            long readerPos = reader.BaseStream.Position;

            LoadCommandType type = (LoadCommandType)reader.ReadUInt32();
            uint size = reader.ReadUInt32();
            commands.Add(type switch {
                LoadCommandType.SymTab => new SymTabCommand(this, reader, size, readerPos),
                LoadCommandType.LoadDylib => new LoadDyLibCommand(this, reader, size, readerPos),
                LoadCommandType.Segment64 => new SegmentCommand64(this, reader, size, readerPos),
                LoadCommandType.RPath => new RPathCommand(this, reader, size, readerPos),
                LoadCommandType.CodeSignature => new LinkEditDataCommand(this, reader, type, size, readerPos),
                LoadCommandType.SegmentSplitInfo => new LinkEditDataCommand(this, reader, type, size, readerPos),
                LoadCommandType.DyLdInfoOnly => new DyLdInfoOnlyCommand(this, reader, size, readerPos),
                LoadCommandType.FunctionStarts => new LinkEditDataCommand(this, reader, type, size, readerPos),
                LoadCommandType.DataInCode => new LinkEditDataCommand(this, reader, type, size, readerPos),
                LoadCommandType.DylibCodeSignDRs => new LinkEditDataCommand(this, reader, type, size, readerPos),
                LoadCommandType.LinkerOptimizationHint => new LinkEditDataCommand(this, reader, type, size, readerPos),
                LoadCommandType.DyLibExportsTrie => new LinkEditDataCommand(this, reader, type, size, readerPos),
                LoadCommandType.DyLdChainedFixups => new LinkEditDataCommand(this, reader, type, size, readerPos),
                _ => new UnknownLoadCommand(type, size, readerPos - fileOffset, readerPos),
            });

            // Seek to the next command.
            reader.BaseStream.Seek(readerPos + size, SeekOrigin.Begin);
        }
    }

}
