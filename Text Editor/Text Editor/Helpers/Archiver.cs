﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Text_Editor.Helpers
{
    class Archiver:IArchiver
    {
        public byte[] Compress(string value)
        {
            Byte[] byteArray = new byte[0];
            if (!string.IsNullOrEmpty(value))
            {
                byteArray = Encoding.UTF8.GetBytes(value);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (GZipStream zip = new GZipStream(stream, CompressionMode.Compress))
                    {
                        zip.Write(byteArray,0,byteArray.Length);
                    }
                    byteArray = stream.ToArray();
                }
            }
            return byteArray;
        }

        public string Decompress(byte[] value)
        {
           string resultString = String.Empty;
            if (value != null && value.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream(value))
                using (GZipStream zip = new GZipStream(stream,CompressionMode.Decompress))
                using (StreamReader reader = new StreamReader(zip))
                {
                    resultString = reader.ReadToEnd();
                }
            }
            return resultString;
        }
    }
}
