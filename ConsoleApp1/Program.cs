using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "Hello world";
            byte[] encrypted;
            string decrypted;

            using (Aes myAes = Aes.Create())
            {
                encrypted = Encrypt(str,myAes.Key,myAes.IV);
                decrypted = BackEncrypt(encrypted,myAes.Key,myAes.IV);

                Console.WriteLine("key");

                foreach (var item in myAes.Key)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
                Console.WriteLine("IV");

                foreach (var item in myAes.IV)
                {
                    Console.Write(item);
                }
            }
            //string text = "U2FsdGVkX1+ACOWcGkKPeWfPMbbnzLH6wfqxY3B0q74=";
            //byte[] buffer = Encoding.UTF8.GetBytes(text);

            
            Console.WriteLine();
            Console.WriteLine("end");
            foreach (byte b in encrypted)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.WriteLine(decrypted);

            //Console.WriteLine();
            //foreach (var item in buffer)
            //{
            //    Console.Write(item);
            //}

        }

        static byte[] Encrypt(string originalString, byte[] key, byte[] vector )
        {
            byte[] encryptString;

            using (Aes myAes = Aes.Create())
            {
                myAes.Key = key;
                myAes.IV = vector;

                ICryptoTransform ecnryptor = myAes.CreateEncryptor(myAes.Key,myAes.IV);

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

        static string BackEncrypt(byte[] encText, byte[] key, byte[] vector)
        {
            string text = null;

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
