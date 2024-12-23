namespace MachOSharp.Command;

public class LinkEditDataCommand : LoadCommand {

    public LoadCommandType Type { get; }
    public long Size { get; }
    public long MachOOffset { get; }
    public long Offset { get; }

    public uint DataOffset { get; }
    public uint DataSize { get; }

    public LinkEditDataCommand(MachO64 macho, BinaryReader reader, LoadCommandType type, long size, long offset) {
        Type = type;
        Size = size;
        Offset = offset;
        MachOOffset = offset - macho.FileOffset;

        DataOffset = reader.ReadUInt32();
        DataSize = reader.ReadUInt32();
    }

}
