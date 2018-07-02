using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GREEDY.Extensions
{
    public static class StringEncryptExtension
    {
        public static string Encrypt(this string clearText)
        {
            var encryptionKey = Environments.AppConfig.EncryptionKey;
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[]
                    {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                if (encryptor == null) return clearText;
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }

                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        public static string Decrypt(this string cipherText)
        {
            var encryptionKey = Environments.AppConfig.EncryptionKey;
            cipherText = cipherText.Replace(" ", "+");
            try
            {
                var cipherBytes = Convert.FromBase64String(cipherText);
                using (var encryptor = Aes.Create())
                {
                    var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[]
                        {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                    if (encryptor != null)
                    {
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(),
                                CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                            }

                            cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid format of cipher text.\r\n{ex}");
                return cipherText;
            }

            return cipherText;
        }
    }
}