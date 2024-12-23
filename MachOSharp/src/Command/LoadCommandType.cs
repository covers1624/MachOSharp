using static MachOSharp.Command.LoadCommandConstants;

namespace MachOSharp.Command;

public enum LoadCommandType : uint {

    Segment                = 0x1,
    SymTab                 = 0x2,
    SymSeg                 = 0x3,
    Thread                 = 0x4,
    UnixThread             = 0x5,
    LoadFvmLib             = 0x6,
    IdFvmLib               = 0x7,
    Ident                  = 0x8,
    FvmFile                = 0x9,
    PrePage                = 0xa,
    DySymTab               = 0xb,
    LoadDylib              = 0xc,
    IdDylib                = 0xd,
    LoadDyLinker           = 0xe,
    IdDyLinker             = 0xf,
    PreBoundDyLib          = 0x10,
    Routines               = 0x11,
    SubFramework           = 0x12,
    SubUmbrella            = 0x13,
    SubClient              = 0x14,
    SubLibrary             = 0x15,
    TwoLevelHits           = 0x16,
    PreBindCksum           = 0x17,
    LoadWeakDylib          = 0x18 | LC_REQ_DYLD,
    Segment64              = 0x19,
    Routines64             = 0x1a,
    Uuid                   = 0x1b,
    RPath                  = 0x1c | LC_REQ_DYLD,
    CodeSignature          = 0x1d,
    SegmentSplitInfo       = 0x1e,
    ReExportDylib          = 0x1f | LC_REQ_DYLD,
    LazyLoadDylib          = 0x20,
    EncryptionInfo         = 0x21,
    DyLdInfo               = 0x22,
    DyLdInfoOnly           = 0x22 | LC_REQ_DYLD,
    LoadUpwardDylib        = 0x23 | LC_REQ_DYLD,
    VersionMinMacOS        = 0x24,
    VersionMinIphoneOS     = 0x25,
    FunctionStarts         = 0x26,
    DyLdEnvironment        = 0x27,
    Main                   = 0x28 | LC_REQ_DYLD,
    DataInCode             = 0x29,
    SourceVersion          = 0x2A,
    DylibCodeSignDRs       = 0x2B,
    EncryptionInfo64       = 0x2C,
    LinkerOption           = 0x2D,
    LinkerOptimizationHint = 0x2E,
    VersionMinTVOS         = 0x2F,
    VersionMinWatchOS      = 0x30,
    Note                   = 0x31,
    BuildVersion           = 0x32,
    DyLibExportsTrie       = 0x33 | LC_REQ_DYLD,
    DyLdChainedFixups      = 0x34 | LC_REQ_DYLD,
    FileSetEntry           = 0x35 | LC_REQ_DYLD,

}

public class LoadCommandConstants {

    public const uint LC_REQ_DYLD = 0x80000000;

}
