//Here i decided to make additional condition and archive document's text

namespace TextEditor.StringCompressing
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    internal class StringCompressor : IStringCompressor
    {
        public byte[] Compress(string value) //This method archive string type into byte array
        {
            Byte[] byteArray = new byte[0];
            if (!string.IsNullOrEmpty(value))
            {
                byteArray = Encoding.UTF8.GetBytes(value);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (GZipStream zip = new GZipStream(stream, CompressionMode.Compress))
                    {
                        zip.Write(byteArray, 0, byteArray.Length);
                    }
                    byteArray = stream.ToArray();
                }
            }
            return byteArray;
        }

        public string Decompress(byte[] value) //This method unzips byte array to string type
        {
            string resultString = String.Empty;
            if (value != null && value.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream(value))
                using (GZipStream zip = new GZipStream(stream, CompressionMode.Decompress))
                using (StreamReader reader = new StreamReader(zip))
                {
                    resultString = reader.ReadToEnd();
                }
            }
            return resultString;
        }
    }
}