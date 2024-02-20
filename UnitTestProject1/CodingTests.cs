using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EncodeCode;
using System.IO;
using System.Security.Cryptography;

namespace UnitTestProject1
{
    [TestClass]
    public class CodingTests
    {

        [DataTestMethod]
        [DataRow("Hello world")]
        [DataRow("3242434")]
        [DataRow("   Hello world       ")]
        [DataRow("Hell4%world")]
        [DataRow("      ")]
        public void Encrypt_EqualEncrypt_WouldBeEqual(string text)
        {
            byte[] encrypted;
            string decrypted;
            Coding code = new Coding();
            using (Aes myAes = Aes.Create())
            {
                encrypted = code.Encrypt(text, myAes.Key, myAes.IV);
                decrypted = code.BackEncrypt(encrypted, myAes.Key, myAes.IV);
            }

            Assert.AreEqual(text, decrypted);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow()]
        public void Encrypt_NullAndEmpty_ReturnException(string text)
        {
            Coding code = new Coding();
            using (Aes myAes = Aes.Create())
            {
                Action actual = () => code.Encrypt(text,myAes.Key, myAes.IV);
                Assert.ThrowsException <ArgumentNullException>(actual);
            }
        }

        [TestMethod]
        public void Decrypt_ExceptionEncrypt_ReturnException()
        {
            byte[] array = null;
            Coding code = new Coding();
            using (Aes myAes = Aes.Create())
            {
                Action actual = () => code.BackEncrypt(array, myAes.Key, myAes.IV);
                Assert.ThrowsException<NullReferenceException>(actual);
            }
        }

    }
}
