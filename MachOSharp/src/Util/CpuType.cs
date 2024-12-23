namespace MachOSharp.Util;

public enum CpuType {

    Any = -1,
    Vax = 1,

    // skip 2
    // skip 3
    // skip 4
    // skip 5
    Mc680x0 = 6,
    X86     = 7,
    I386    = X86,
    X86_64  = X86 | CpuConstants.CPU_ARCH_ABI64,

    // skip Mips = 8,
    // skip 9
    Mc98000  = 10,
    Hppa     = 11,
    Arm      = 12,
    Arm64    = Arm | CpuConstants.CPU_ARCH_ABI64,
    Arm64_32 = Arm | CpuConstants.CPU_ARCH_ABI64_32,
    Mc88000  = 13,
    Sparc    = 14,
    I860     = 15,

    // skip Alpha = 16,
    // skip 17
    PowerPc   = 18,
    PowerPc64 = PowerPc | CpuConstants.CPU_ARCH_ABI64,
    // skip 19
    // skip 20
    // skip 21
    // skip 22
    // skip 23
    // skip 24

}

public class CpuConstants {

    public const uint CPU_ARCH_MASK = 0xFF000000;

    public const int CPU_ARCH_ABI64    = 0x01000000;
    public const int CPU_ARCH_ABI64_32 = 0x02000000;

    /* mask for feature flags */
    public const uint CPU_SUBTYPE_MASK = 0xff000000;

    /* 64 bit libraries */
    public const uint CPU_SUBTYPE_LIB64 = 0x80000000;

    /* pointer authentication with versioned ABI */
    public const uint CPU_SUBTYPE_PTRAUTH_ABI = 0x80000000;
}
