using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Text_Editor.Helpers
{
    interface IArchiver
    {
        Byte[] Compress(string value);
        string Decompress(Byte[] value);
    }
}
