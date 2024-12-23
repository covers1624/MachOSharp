using System.Text;

namespace MachOSharp.Util;

internal static class BinaryReaderExtensions {

    public static string ReadSizedString(this BinaryReader reader, int size) {
        return Encoding.UTF8.GetString(reader.ReadBytes(size).TakeWhile(b => b > 0).ToArray());
    }

    public static string ReadNTString(this BinaryReader reader, long offset) {
        var pos = reader.BaseStream.Position;
        try {
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            return reader.ReadNTString();
        } finally {
            reader.BaseStream.Position = pos;
        }
    }

    public static string ReadNTString(this BinaryReader reader) {
        StringBuilder builder = new StringBuilder();
        char ch;
        while ((ch = reader.ReadChar()) != 0) {
            builder.Append(ch);
        }

        return builder.ToString();
    }

}
