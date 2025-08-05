using System;
using System.Linq;

namespace Cdis.Brisk.Infra.Core.Util
{
    public static class StringUtil
    {
        /// <summary>
        /// Retorna caracteres 
        /// </summary>        
        public static string GetValueFake(int length)
        {
            try
            {
                if (length > 0)
                {
                    string strValue = "VALUE FAKE";
                    while (strValue.Length < length)
                    {
                        strValue += " VALUE FAKE";
                    }

                    return strValue.Substring(0, length);
                }
                return null;
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Gerar uma string curta
        /// </summary>
        public static string GenerateShortString()
        {
            try
            {
                string strHash = string.Empty;
                Enumerable.Range(48, 75)
                  .OrderBy(o => new Random().Next())
                  .ToList()
                  .ForEach(i => strHash += Convert.ToChar(i));

                return strHash.GetHashCode().ToString("X2");
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }
    }
}
