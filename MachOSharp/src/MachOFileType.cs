namespace MachOSharp;

public enum MachOFileType : uint {

    Object     = 0x1,
    Execute    = 0x2,
    FvmLib     = 0x3,
    Core       = 0x4,
    PreLoad    = 0x5,
    DyLib      = 0x6,
    DyLinker   = 0x7,
    Bundle     = 0x8,
    DyLibStub  = 0x9,
    DebugSym   = 0xa,
    KextBundle = 0xb,
    FileSet    = 0xc,

}
