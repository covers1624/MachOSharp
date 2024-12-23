using MachOSharp.Util;

namespace MachOSharp.Command;

public class RPathCommand : LoadCommand {

    public LoadCommandType Type => LoadCommandType.RPath;
    public long Size { get; }
    public long MachOOffset { get; }
    public long Offset { get; }

    public string Path { get; }

    public RPathCommand(MachO64 macho, BinaryReader reader, long size, long offset) {
        Size = size;
        Offset = offset;
        MachOOffset = offset - macho.FileOffset;

        Path = reader.ReadNTString();
    }

}
