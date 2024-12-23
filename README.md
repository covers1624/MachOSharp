# MachOSharp
A small library for reading macOS Mach-O binaries.

## Examples

### Universal binaries
```c#
        MachOFat? fat;
        using (var stream = File.OpenRead("/path/to/binary")) {
            MachOReader.TryLoadFat(stream, out fat, true);
        }
```

### Regular binaries
```c#
        MachO64? macho;
        using (var stream = File.OpenRead("/path/to/binary")) {
            MachOReader.TryLoad64(stream, out macho, true);
        }
```