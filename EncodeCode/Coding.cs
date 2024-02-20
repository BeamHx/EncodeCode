using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace EncodeCode
{
    public class Coding
    {
       public byte[] Encrypt(string originalString, byte[] key, byte[] vector)
        {
            byte[] encryptString;

            using (Aes myAes = Aes.Create())
            {
                if (originalString.Length <= 0 || originalString == null)
                {
                    throw new ArgumentNullException("plainText");
                }
                if (key.Length <= 0)
                {
                    throw new ArgumentNullException("Key");
                }
                if (vector.Length <= 0)
                {
                    throw new ArgumentNullException("IV");
                } 

                myAes.Key = key;
                myAes.IV = vector;

                ICryptoTransform ecnryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, ecnryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(originalString);
                        }
                        encryptString = msEncrypt.ToArray();
                    }
                }
            }
            return encryptString;
        }

        public string BackEncrypt(byte[] encText, byte[] key, byte[] vector)
        {
            string text = null;

            if (encText.Length <= 0 || encText == null)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }
            if (vector.Length <= 0)
            {
                throw new ArgumentNullException("IV");
            }

            using (Aes myAes = Aes.Create())
            {
                myAes.Key = key;
                myAes.IV = vector;

                ICryptoTransform decryptor = myAes.CreateDecryptor(myAes.Key, myAes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            text = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return text;
        }
    }
}
