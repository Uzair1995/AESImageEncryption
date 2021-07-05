using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;

namespace AESEncryption
{
    class Program
    {
        const string Key = "12345678123456781234567812345678"; // must be 32 character
        const string IV = "ABCDEFGHIJKLMNOP"; // must be 16 character

        static void Main(string[] args)
        {
            string fileName = @"D:\Personal\AESEncryption\AESEncryption\";
            string text = File.ReadAllText(fileName + "imageEncryptedFromNode.enc");

            AesEncrypter aesEncrypter = new AesEncrypter();
            var data = aesEncrypter.Decrypt(text);

            string filePath = fileName + "imageUnencrypted";
            File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(data));


            //using (FileStream fsOutput = new FileStream(fileName + "imageUnencrypted.jpg", FileMode.Create))
            //{
            //    int data;
            //    while ((data = fsOutput.ReadByte()) != -1)
            //    {
            //        cs.WriteByte((byte)data);
            //    }
            //}


            //Encrypt(fileName + "image.jpg", fileName + "imageEncrypted");
            //Decrypt(fileName + /*"imageEncrypted"*/"imageEncryptedFromNode.enc", fileName + "imageUnencrypted.jpg");
        }

        private static void Decrypt(string inputFilePath, string outputfilePath)
        {
            using (Aes encryptor = Aes.Create())
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] aesIV = UTF8Encoding.UTF8.GetBytes(IV);
                    byte[] aesKey = UTF8Encoding.UTF8.GetBytes(Key);
                    ICryptoTransform decryptor = aes.CreateDecryptor(aesKey, aesIV);
                    using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                    {
                        using (CryptoStream cs = new CryptoStream(fsInput, decryptor, CryptoStreamMode.Read))
                        {
                            using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                            {
                                int data;
                                while ((data = cs.ReadByte()) != -1)
                                {
                                    fsOutput.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Encrypt(string inputFilePath, string outputfilePath)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = UTF8Encoding.UTF8.GetBytes(IV);
            aes.Key = UTF8Encoding.UTF8.GetBytes(Key);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            
            using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
            {
                using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                    {
                        int data;
                        while ((data = fsInput.ReadByte()) != -1)
                        {
                            cs.WriteByte((byte)data);
                        }
                    }
                }
            }
        }


    }
}
