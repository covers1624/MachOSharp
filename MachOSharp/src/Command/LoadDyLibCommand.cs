using System.Diagnostics;
using MachOSharp.Util;

namespace MachOSharp.Command;

[DebuggerDisplay("LoadDylib({DyLib.Name})")]
public class LoadDyLibCommand : LoadCommand {
    
    public LoadCommandType Type => LoadCommandType.LoadDylib;
    public long Size { get; }
    public long MachOOffset { get; }
    public long Offset { get; }
    
    public DyLib DyLib { get; }
    

    public LoadDyLibCommand(MachO64 macho, BinaryReader reader, long size, long offset) {
        Size = size;
        Offset = offset;
        MachOOffset = offset - macho.FileOffset;

        DyLib = new DyLib(reader);
    }

}

public class DyLib {

    public string Name { get; }
    public uint Timestamp { get; }
    public uint CurrentVersion { get; }
    public uint CompatabilityVersion { get; }

    public DyLib(BinaryReader reader) {
        uint strIdx = reader.ReadUInt32();
        
        Timestamp = reader.ReadUInt32();
        CurrentVersion = reader.ReadUInt32();
        CompatabilityVersion = reader.ReadUInt32();

        Name = reader.ReadNTString();
    }
}
