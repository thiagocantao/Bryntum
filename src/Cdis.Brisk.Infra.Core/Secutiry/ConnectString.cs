using Crypto;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cdis.Brisk.Infra.Core.Secutiry
{
    public static class ConnectString
    {
        /// <summary>
        /// Buscar a string de conexão
        /// </summary>    
        public static string GetStringConexao()
        {
            try
            {
                CDIS_Crypto oCrypto = new CDIS_Crypto(CDIS_Crypto.SymmProvEnum.Rijndael);
                try
                {
                    string chave = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();
                    string pathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
                    chave = chave.Replace("{", "");
                    chave = chave.Replace("-", "");
                    chave = chave.Substring(0, 16);

                    return oCrypto.descriptografaString(pathDB, chave);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                //string chave = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();
                //string pathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
                //chave = chave.Replace("{", "");
                //chave = chave.Replace("-", "");
                //chave = chave.Substring(0, 16);

                //return Descriptografar(pathDB, chave);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Criptografa uma string
        /// </summary>
        /// <param name="textoQueSeraCriptografado">Texto a ser criptografado</param>
        /// <param name="chave">Chave a ser usada como semente</param>
        /// <returns>texto criptografado</returns>
        public static string Criptografar(string textoQueSeraCriptografado, string chave)
        {
            return Crypto(textoQueSeraCriptografado, chave, true);
        }

        /// <summary>
        /// Descriptografa uma string
        /// </summary>
        /// <param name="textoQueSeraDescriptografado">Texto a ser criptografado</param>
        /// <param name="chave">Mesma chave usada no processo de criptrografia</param>
        /// <returns>Texto descriptografado. Se der algo errado, será retornado string vazia</returns>
        public static string Descriptografar(string textoQueSeraDescriptografado, string chave)
        {
            return Crypto(textoQueSeraDescriptografado, chave, false);
        }

        private static string Crypto(string entrada, string chave, bool cifrar)
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
