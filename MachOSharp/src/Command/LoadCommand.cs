using System.Diagnostics;

namespace MachOSharp.Command;

public interface LoadCommand {

    /// <summary>
    /// The type of this Load Command.
    /// </summary>
    public LoadCommandType Type { get; }

    /// <summary>
    /// The load command size in bytes. Including any padding or alignment bytes.
    /// </summary>
    public long Size { get; }

    /// <summary>
    /// The offset from the start of the MachO file this command is contained in.
    /// </summary>
    public long MachOOffset { get; }

    /// <summary>
    /// The offset in the initial stream that this command was read from.
    /// </summary>
    public long Offset { get; }

}

[DebuggerDisplay("UnknownCommand({Type})")]
public class UnknownLoadCommand(LoadCommandType type, long size, long machoOffset, long fileOffset) : LoadCommand {

    public LoadCommandType Type => type;
    public long Size => size;
    public long MachOOffset => machoOffset;
    public long Offset => fileOffset;

}
