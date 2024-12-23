using System.Buffers.Binary;
using System.Text;

namespace MachOSharp.Util;

/// <summary>
/// A BinaryReader implementation which flips the endianness of the underlying BinaryReader.
/// </summary>
/// <param name="_delegate"></param>
/// <param name="leaveOpen"></param>
public sealed class FlippedEndiannessBinaryReader(BinaryReader _delegate, bool leaveOpen = false) : BinaryReader(Stream.Null, Encoding.UTF8, true) {

    public BinaryReader BaseBinaryReader => _delegate;
    public override Stream BaseStream => _delegate.BaseStream;

    protected override void Dispose(bool disposing) {
        if (!leaveOpen) {
            _delegate.Dispose();
        }
    }

    public override int PeekChar() {
        return _delegate.PeekChar();
    }

    public override int Read() {
        return _delegate.Read();
    }

    public override byte ReadByte() {
        return _delegate.ReadByte();
    }

    public override sbyte ReadSByte() {
        return _delegate.ReadSByte();
    }

    public override bool ReadBoolean() {
        return _delegate.ReadBoolean();
    }

    public override char ReadChar() {
        return _delegate.ReadChar();
    }

    public override short ReadInt16() {
        return BinaryPrimitives.ReverseEndianness(_delegate.ReadInt16());
    }

    public override ushort ReadUInt16() {
        return BinaryPrimitives.ReverseEndianness(_delegate.ReadUInt16());
    }

    public override int ReadInt32() {
        return BinaryPrimitives.ReverseEndianness(_delegate.ReadInt32());
    }

    public override uint ReadUInt32() {
        return BinaryPrimitives.ReverseEndianness(_delegate.ReadUInt32());
    }

    public override long ReadInt64() {
        return BinaryPrimitives.ReverseEndianness(_delegate.ReadInt64());
    }

    public override ulong ReadUInt64() {
        return BinaryPrimitives.ReverseEndianness(_delegate.ReadUInt64());
    }

    public override Half ReadHalf() {
        if (BitConverter.IsLittleEndian) {
            return BinaryPrimitives.ReadHalfBigEndian(_delegate.ReadBytes(2));
        }

        return BinaryPrimitives.ReadHalfLittleEndian(_delegate.ReadBytes(2));
    }

    public override float ReadSingle() {
        if (BitConverter.IsLittleEndian) {
            return BinaryPrimitives.ReadSingleBigEndian(_delegate.ReadBytes(4));
        }

        return BinaryPrimitives.ReadSingleLittleEndian(_delegate.ReadBytes(4));
    }

    public override double ReadDouble() {
        if (BitConverter.IsLittleEndian) {
            return BinaryPrimitives.ReadDoubleBigEndian(_delegate.ReadBytes(8));
        }

        return BinaryPrimitives.ReadDoubleLittleEndian(_delegate.ReadBytes(8));
    }

    public override decimal ReadDecimal() {
        return _delegate.ReadDecimal();
    }

    public override string ReadString() {
        return _delegate.ReadString();
    }

    public override int Read(char[] buffer, int index, int count) {
        return _delegate.Read(buffer, index, count);
    }

    public override int Read(Span<char> buffer) {
        return _delegate.Read(buffer);
    }

    public override char[] ReadChars(int count) {
        return _delegate.ReadChars(count);
    }

    public override int Read(byte[] buffer, int index, int count) {
        return _delegate.Read(buffer, index, count);
    }

    public override int Read(Span<byte> buffer) {
        return _delegate.Read(buffer);
    }

    public override byte[] ReadBytes(int count) {
        return _delegate.ReadBytes(count);
    }

}
