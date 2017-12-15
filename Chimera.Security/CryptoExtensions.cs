using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chimera.Security
{
    public static class StringCryptoExtensions
    {
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        public static string Encrypt(this string plainText, string sharedSecret = "0000")
        {
            string outStr; 

            using (var aesAlg = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt);
                
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        // prepend the IV
                        msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                        }

                        outStr = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }

            return outStr;
        }

        public static string Decrypt(this string cipherText, string sharedSecret = "0000")
        {
            string outStr;
            byte[] bytes = Convert.FromBase64String(cipherText);

            using (var aesAlg = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

                using (var msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object with the specified key and IV.
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);

                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        // Create a decrytor to perform the stream transform.
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream and place them in a string.
                                outStr = srDecrypt.ReadToEnd();
                            }  
                        }
                    }
                }
            }

            return outStr;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];

            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];

            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
