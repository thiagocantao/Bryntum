using System;

namespace Cdis.Brisk.Infra.Core.Extensions
{
    /// <summary>
    /// Extensões para array de byte
    /// </summary>
    public static class ArrayByteExtensions
    {
        /// <summary>
        /// Tipos de imagens
        /// </summary>
        public enum TypeImgArrayByteCore
        {
            BMP,
            PNG,
            JPG
        }

        /// <summary>
        /// Monta a imagem na base 64 apartir de um array de byte
        /// </summary>        
        public static string GetImgBase64(this byte[] array, TypeImgArrayByteCore type)
        {
            try
            {
                string extensao = type == TypeImgArrayByteCore.JPG ? "jpeg" : type.ToString().ToLower();
                return "data:image/" + extensao + ";base64," + Convert.ToBase64String(array);
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }
    }
}
