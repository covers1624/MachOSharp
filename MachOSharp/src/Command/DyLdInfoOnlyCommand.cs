namespace MachOSharp.Command;

public class DyLdInfoOnlyCommand : LoadCommand {

    public LoadCommandType Type => LoadCommandType.DyLdInfoOnly;
    public long Size { get; }
    public long MachOOffset { get; }
    public long Offset { get; }

    public uint RebaseOff { get; }
    public uint RebaseSize { get; }

    public uint BindOff { get; }
    public uint BindSize { get; }

    public uint WeakBindOff { get; }
    public uint WeakBindSize { get; }

    public uint LazyBindOff { get; }
    public uint LazyBindSize { get; }

    public uint ExportBindOff { get; }
    public uint ExportBindSize { get; }


    public DyLdInfoOnlyCommand(MachO64 macho, BinaryReader reader, long size, long offset) {
        Size = size;
        Offset = offset;
        MachOOffset = offset - macho.FileOffset;

        RebaseOff = reader.ReadUInt32();
        RebaseSize = reader.ReadUInt32();
        BindOff = reader.ReadUInt32();
        BindSize = reader.ReadUInt32();
        WeakBindOff = reader.ReadUInt32();
        WeakBindSize = reader.ReadUInt32();
        LazyBindOff = reader.ReadUInt32();
        LazyBindSize = reader.ReadUInt32();
        ExportBindOff = reader.ReadUInt32();
        ExportBindSize = reader.ReadUInt32();
    }

}
