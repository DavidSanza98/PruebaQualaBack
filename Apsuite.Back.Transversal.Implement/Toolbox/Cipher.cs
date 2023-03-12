using Apsuite.Back.Transversal.Contract.Global.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Apsuite.Back.Transversal.Implement.Toolbox
{
    public class Cipher : ICipher
    {
        private readonly IConfiguration Configuration;
        private readonly string SecretKey;
        private readonly bool IsEncrypt;

        public Cipher(IConfiguration configuration)
        {
            Configuration = configuration;
            SecretKey = Configuration["SecretKeyCipher"]!;
            IsEncrypt = Convert.ToBoolean(Configuration["IsEncrypt"]);
        }

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="plainText">String to be encrypted</param>
        public string Encrypt(string plainText)
        {
            if (IsEncrypt)
            {
                // Get the bytes of the string
                var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
                var passwordBytes = Encoding.UTF8.GetBytes(SecretKey);

                // Hash the password with SHA256
                passwordBytes = SHA512.Create().ComputeHash(passwordBytes);
                var bytesEncrypted = Cipher.Encrypt(bytesToBeEncrypted, passwordBytes);
                return Convert.ToBase64String(bytesEncrypted);
            }
            else
            {
                return plainText;
            }

        }

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="plainText">String to be encrypted</param>
        public string EncryptPass(string plainText)
        {
            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(SecretKey);

            // Hash the password with SHA256
            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);
            var bytesEncrypted = Cipher.Encrypt(bytesToBeEncrypted, passwordBytes);
            return Convert.ToBase64String(bytesEncrypted);
        }

        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="encryptedText">String to be decrypted</param>
        /// <param name="password">Password used during encryption</param>
        /// <exception cref="FormatException"></exception>
        public string Decrypt(string encryptedText)
        {
            if (IsEncrypt)
            {
                // Get the bytes of the string
                var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
                var passwordBytes = Encoding.UTF8.GetBytes(SecretKey);

                passwordBytes = SHA512.Create().ComputeHash(passwordBytes);
                var bytesDecrypted = Cipher.Decrypt(bytesToBeDecrypted, passwordBytes);
                return Encoding.UTF8.GetString(bytesDecrypted);
            }
            else
            {
                return encryptedText;
            }
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[]? encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new())
            {
                using var AES = Aes.Create("AesManaged")!;
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[]? decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using var AES = Aes.Create("AesManaged")!;
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}
