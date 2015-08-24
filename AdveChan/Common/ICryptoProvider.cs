using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdveChan.Common
{
    public interface ICryptoProvider
    {
        bool VerifyHash(string inputValue, string hash, string salt);
        string GetHash(string inputValue, string salt);
        string Encrypt(object data);
        string Decrypt(string data);
    }
}