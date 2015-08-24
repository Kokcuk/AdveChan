namespace AdveChan.Common
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class DefaultCryptoProvider : ICryptoProvider
    {
        public DefaultCryptoProvider(string key, string vector)
        {
            cryptoKey = Convert.FromBase64String(key);
            cryptoVector = Convert.FromBase64String(vector);
        }

        public bool VerifyHash(string inputValue, string hash, string salt)
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                return false;
            }
            var inputHash = GetHash(inputValue.Trim(), (salt ?? string.Empty).Trim());

            return string.Compare(inputHash, hash, StringComparison.InvariantCulture) == 0;
        }

        public string GetHash(string inputValue, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var plainTextBytes = Encoding.UTF8.GetBytes(inputValue);

            var plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            plainTextBytes.CopyTo(plainTextWithSaltBytes, 0);
            saltBytes.CopyTo(plainTextWithSaltBytes, plainTextBytes.Length);

            var hashBytes = hashAlgorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(hashBytes);
        }

        public string Encrypt(object data)
        {
            if (data == null)
            {
                return string.Empty;
            }

            using (var ms = new MemoryStream())
            {
                using (var aes = Aes.Create())
                {
                    if (aes != null)
                    {
                        using (var encryptor = aes.CreateEncryptor(cryptoKey, cryptoVector))
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public string Decrypt(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return data;
            }

            using (var aes = Aes.Create())
            using (var ms = new MemoryStream(Convert.FromBase64String(data)))
            using (var decryptor = aes.CreateDecryptor(cryptoKey, cryptoVector))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }

        #region Fields

        private readonly HashAlgorithm hashAlgorithm = new SHA512Managed();
        private readonly byte[] cryptoVector;
        private readonly byte[] cryptoKey;

        #endregion
    }
}