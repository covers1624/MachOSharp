namespace MachOSharp.Util;

[Flags]
public enum MachOFlags : uint {

    NoUnDefs                       = 0x1,
    IncRLink                       = 0x2,
    DyLdLink                       = 0x4,
    BindAtLoad                     = 0x8,
    PreBound                       = 0x10,
    SplitSegs                      = 0x20,
    LazyInit                       = 0x40,
    TwoLevel                       = 0x80,
    ForceFlat                      = 0x100,
    NoMultiDefs                    = 0x200,
    NoFixPreBinding                = 0x400,
    PreBindable                    = 0x800,
    AllModsBound                   = 0x1000,
    SubsectionsViaSymbols          = 0x2000,
    Canonical                      = 0x4000,
    WeakDefines                    = 0x8000,
    BindsToWeak                    = 0x10000,
    AllowStackExecution            = 0x20000,
    RootSafe                       = 0x40000,
    SetUidSafe                     = 0x80000,
    NoReExportedDylibs             = 0x100000,
    PositionIndependentExecution   = 0x200000,
    DeadStripableDylib             = 0x400000,
    HasThreadLocalValueDescriptors = 0x800000,
    NoHeapExecution                = 0x1000000,
    AppExtensionSafe               = 0x02000000,
    NListOutOfSyncWithDyldInfo     = 0x04000000,
    SimulatorSupport               = 0x08000000,
    DylibInCache                   = 0x80000000,

}
