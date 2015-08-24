using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdveChan.Tests
{
    using Common;

    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void Crypt()
        {
            string Salt = "ohGPRtQpvRYcKscrFntAFBXQ9xIhiLtHlyRg81Hg2sUe4IcbV74zo3dNuuCzZdtIzWiBPakP9QR6hP+OHV/DTw==";
            var cryptoProvider = new DefaultCryptoProvider("RnObFfS6tQKWfHJmwCC1yGLtl/1T3oP+FGMZOyr77us=", "JOgQUaVye+6qoThNrqZDjA==");

            string passwordHash = cryptoProvider.GetHash("123456", Salt);
        }
    }
}
