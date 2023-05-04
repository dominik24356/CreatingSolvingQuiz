using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class AesEncryptionHelper
{
    private static readonly byte[] Key = new byte[] { 0x5A, 0x5C, 0x25, 0xE3, 0x8F, 0x1E, 0x4D, 0x9D, 0xAB, 0x31, 0x37, 0x22, 0xB1, 0x05, 0xF9, 0x87 };
    private static readonly byte[] IV = new byte[] { 0x13, 0x44, 0x9B, 0x33, 0x55, 0x1C, 0x86, 0x72, 0x4A, 0xC8, 0xA1, 0x98, 0x8F, 0x2B, 0xD5, 0xC3 };

    public static string EncryptString(string plainText)
    {
        byte[] encrypted;
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encrypted);
    }

    public static string DecryptString(string cipherText)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        string plaintext = null;

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new MemoryStream(cipherBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}
