using MachOSharp.Util;

namespace MachOSharp.Command;

public class SymTabCommand : LoadCommand {

    public LoadCommandType Type => LoadCommandType.SymTab;
    public long Size { get; }
    public long MachOOffset { get; }
    public long Offset { get; }

    public uint SymOffset { get; }

    public uint StrOffset { get; }
    public uint StrSize { get; }

    public IEnumerable<Symbol> Symbols { get; }

    public SymTabCommand(MachO64 macho, BinaryReader reader, long size, long offset) {
        Size = size;
        Offset = offset;
        MachOOffset = offset - macho.FileOffset;

        SymOffset = reader.ReadUInt32();
        var nSyms = reader.ReadUInt32();
        StrOffset = reader.ReadUInt32();
        StrSize = reader.ReadUInt32();

        List<Symbol> symbols = [];
        Symbols = symbols;
        
        // Read nSyms of nlist_64's at symOff
        reader.BaseStream.Seek(macho.FileOffset + SymOffset, SeekOrigin.Begin);
        for (int i = 0; i < nSyms; i++) {
            var nstrx = reader.ReadUInt32();
            SymbolType nType = (SymbolType)reader.ReadByte();
            var nSect = reader.ReadByte();
            var nDesc = reader.ReadUInt16();
            var nValue = reader.ReadUInt64();

            var name = nstrx == 0 ? null : reader.ReadNTString(macho.FileOffset + StrOffset + nstrx);
            symbols.Add(new Symbol(name, nType, nSect, nDesc, nValue));
        }
    }
}

public class Symbol(string? name, SymbolType type, byte? sectionIndex, ushort desc, ulong value) {

    public string? Name => name;
    public SymbolType SymbolType => type;
    public byte? SectionIndex => sectionIndex;
    public ushort Desc => desc;
    public ulong Value => value;

}

public enum SymbolType {

    N_UNDF = 0x0, /* undefined, n_sect == NO_SECT */
    N_ABS  = 0x2, /* absolute, n_sect == NO_SECT */
    N_SECT = 0xe, /* defined in section number n_sect */
    N_PBUD = 0xc, /* prebound undefined (defined in a dylib) */
    N_INDR = 0xa, /* indirect */

    N_GSYM    = 0x20, /* global symbol: name,,NO_SECT,type,0 */
    N_FNAME   = 0x22, /* procedure name (f77 kludge): name,,NO_SECT,0,0 */
    N_FUN     = 0x24, /* procedure: name,,n_sect,linenumber,address */
    N_STSYM   = 0x26, /* static symbol: name,,n_sect,type,address */
    N_LCSYM   = 0x28, /* .lcomm symbol: name,,n_sect,type,address */
    N_BNSYM   = 0x2e, /* begin nsect sym: 0,,n_sect,0,address */
    N_AST     = 0x32, /* AST file path: name,,NO_SECT,0,0 */
    N_OPT     = 0x3c, /* emitted with gcc2_compiled and in gcc source */
    N_RSYM    = 0x40, /* register sym: name,,NO_SECT,type,register */
    N_SLINE   = 0x44, /* src line: 0,,n_sect,linenumber,address */
    N_ENSYM   = 0x4e, /* end nsect sym: 0,,n_sect,0,address */
    N_SSYM    = 0x60, /* structure elt: name,,NO_SECT,type,struct_offset */
    N_SO      = 0x64, /* source file name: name,,n_sect,0,address */
    N_OSO     = 0x66, /* object file name: name,,0,0,st_mtime */
    N_LSYM    = 0x80, /* local sym: name,,NO_SECT,type,offset */
    N_BINCL   = 0x82, /* include file beginning: name,,NO_SECT,0,sum */
    N_SOL     = 0x84, /* #included file name: name,,n_sect,0,address */
    N_PARAMS  = 0x86, /* compiler parameters: name,,NO_SECT,0,0 */
    N_VERSION = 0x88, /* compiler version: name,,NO_SECT,0,0 */
    N_OLEVEL  = 0x8A, /* compiler -O level: name,,NO_SECT,0,0 */
    N_PSYM    = 0xa0, /* parameter: name,,NO_SECT,type,offset */
    N_EINCL   = 0xa2, /* include file end: name,,NO_SECT,0,0 */
    N_ENTRY   = 0xa4, /* alternate entry: name,,n_sect,linenumber,address */
    N_LBRAC   = 0xc0, /* left bracket: 0,,NO_SECT,nesting level,address */
    N_EXCL    = 0xc2, /* deleted include file: name,,NO_SECT,0,sum */
    N_RBRAC   = 0xe0, /* right bracket: 0,,NO_SECT,nesting level,address */
    N_BCOMM   = 0xe2, /* begin common: name,,NO_SECT,0,0 */
    N_ECOMM   = 0xe4, /* end common: name,,n_sect,0,0 */
    N_ECOML   = 0xe8, /* end common (local name): 0,,n_sect,0,address */
    N_LENG    = 0xfe, /* second stab entry with length information */
    N_PC      = 0x30, /* global pascal symbol: name,,NO_SECT,subtype,line */

}
