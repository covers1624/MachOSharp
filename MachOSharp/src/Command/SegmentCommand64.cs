using System.Diagnostics;
using MachOSharp.Util;

namespace MachOSharp.Command;

[DebuggerDisplay("SegmentCommand64({Name})")]
public class SegmentCommand64 : LoadCommand {

    public LoadCommandType Type => LoadCommandType.Segment64;
    public long Size { get; }
    public string Name { get; }
    public ulong VMAddr { get; }
    public ulong VMSize { get; }
    public ulong FileOff { get; }
    public ulong FileSize { get; }
    public VMProtection MaxProt { get; }
    public VMProtection InitProt { get; }
    public SegmentFlags Flags { get; }

    public IEnumerable<Section64> Sections { get; }

    public long Offset { get; }
    public long MachOOffset { get; }

    public SegmentCommand64(MachO64 macho, BinaryReader reader, long size, long offset) {
        Size = size;
        Offset = offset;
        MachOOffset = offset - macho.FileOffset;

        Name = reader.ReadSizedString(16);
        VMAddr = reader.ReadUInt64();
        VMSize = reader.ReadUInt64();
        FileOff = reader.ReadUInt64();
        FileSize = reader.ReadUInt64();
        MaxProt = (VMProtection)reader.ReadInt32();
        InitProt = (VMProtection)reader.ReadInt32();
        uint nsects = reader.ReadUInt32();
        Flags = (SegmentFlags)reader.ReadUInt32();

        List<Section64> sections = [];
        Sections = sections;
        for (int i = 0; i < nsects; i++) {
            sections.Add(new Section64(reader));
        }
    }

}

[Flags]
public enum VMProtection {

    NONE    = 0x00,
    READ    = 0x01,
    WRITE   = 0x02,
    EXECUTE = 0x04,

}

[Flags]
public enum SegmentFlags : uint {

    None        = 0x00,
    HighVM      = 0x01,
    FVMLib      = 0x02,
    NoReloc     = 0x04,
    ProtectedV1 = 0x08,
    ReadOnly    = 0x10,

}

public class Section64 {

    public string Name { get; }
    public string SegmentName { get; }
    public ulong Address { get; }
    public ulong Size { get; }
    public ulong Offset { get; }
    public ulong Align { get; }
    public ulong Reloc { get; }
    public ulong NReloc { get; }

    public SectionType Type { get; }
    public SectionAttributes Attributes { get; }

    public Section64(BinaryReader reader) {
        Name = reader.ReadSizedString(16);
        SegmentName = reader.ReadSizedString(16);

        Address = reader.ReadUInt64();
        Size = reader.ReadUInt64();
        Offset = reader.ReadUInt32();
        Align = reader.ReadUInt32();
        Reloc = reader.ReadUInt32();
        NReloc = reader.ReadUInt32();
        var flags = reader.ReadUInt32();
        reader.ReadBytes(4 * 3); // reserved

        const uint SECTION_TYPE = 0x000000ff;
        const uint SECTION_ATTRIBUTES = 0xffffff00;

        Type = (SectionType)(flags & SECTION_TYPE);
        Attributes = (SectionAttributes)(flags & SECTION_ATTRIBUTES);
    }

}

public enum SectionType {

    Regular                         = 0x0,
    ZeroFill                        = 0x1,
    CStringLiterals                 = 0x2,
    FourByteLiterals                = 0x3,
    EightByteLiterals               = 0x4,
    LiteralPointers                 = 0x5,
    NonLazySymbolPointers           = 0x6,
    LazySymbolPointers              = 0x7,
    SymbolStubs                     = 0x8,
    ModInitFuncPointers             = 0x9,
    ModTermFuncPointers             = 0xa,
    Coalesced                       = 0xb,
    GBZeroFill                      = 0xc,
    Interposing                     = 0xd,
    SixteenBytePointers             = 0xe,
    DTraceDOF                       = 0xf,
    LazyDylibSymbolPointers         = 0x10,
    ThreadLocalRegular              = 0x11,
    ThreadLocalZeroFill             = 0x12,
    ThreadLocalVariables            = 0x13,
    ThreadLocalVariablePointers     = 0x14,
    ThreadLocalInitFunctionPointers = 0x15,
    InitFuncOffsets                 = 0x16,

}

[Flags]
public enum SectionAttributes : uint {

    None = 0x00,

    PureInstructions  = 0x80000000,
    NoToc             = 0x40000000,
    StripStaticSyms   = 0x20000000,
    NoDeadStrip       = 0x10000000,
    LiveSupport       = 0x08000000,
    SelfModifyingCode = 0x04000000,
    Debug             = 0x02000000,

    SomeInstructions = 0x00000400,
    ExtReloc         = 0x00000200,
    LocReloc         = 0x00000100,

}
