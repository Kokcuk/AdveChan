namespace TextEditor.StringCompressing
{
    using System;

    public interface IStringCompressor
    {
        Byte[] Compress(string value);
        string Decompress(Byte[] value);
    }
}