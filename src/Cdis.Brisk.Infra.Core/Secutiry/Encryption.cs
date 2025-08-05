using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cdis.Brisk.Infra.Core.Secutiry
{
    public static class Encryption
    {
        public static string descriptografar(string textoQueSeraDescriptografado, string chave)
        {
            return Encrypt(textoQueSeraDescriptografado, chave, false);
        }

        private static string Encrypt(string entrada, string chave, bool cifrar)
        {
            byte[] salt = new byte[] { 0x24, 0x41, 0x6E, 0x74, 0x25, 0x3F, 0x6E, 0x69, 0x6F, 0x2A, 0x43, 0x47, 0x40 };

            Rfc2898DeriveBytes chaveInterna = new Rfc2898DeriveBytes(chave, salt);

            Rijndael aes = Rijndael.Create();
            aes.Key = chaveInterna.GetBytes(32);
            aes.IV = chaveInterna.GetBytes(16);

            ICryptoTransform oCryptoTransform = (cifrar) ? aes.CreateEncryptor() : aes.CreateDecryptor();

            MemoryStream oMemoryStream = new MemoryStream();
            CryptoStream oCryptoStream = new CryptoStream(oMemoryStream, oCryptoTransform, CryptoStreamMode.Write);

            try
            {
                byte[] oByte;
                if (cifrar)
                    oByte = Encoding.Unicode.GetBytes(entrada);
                else
                    oByte = Convert.FromBase64String(entrada);

                try
                {
                    oCryptoStream.Write(oByte, 0, oByte.Length);
                    oCryptoStream.Close();
                }
                catch { return null; }

                if (cifrar)
                    return Convert.ToBase64String(oMemoryStream.ToArray());
                else
                    return Encoding.Unicode.GetString(oMemoryStream.ToArray());
            }
            catch
            {
                return null;
            }
        }
    }
}
