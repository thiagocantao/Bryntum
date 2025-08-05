using System;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Cdis.Brisk.Infra.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Convete o objeto para json
        /// </summary>        
        public static string ToJson(this object objectToConverter)
        {
            try
            {
                var javaScriptSerializer = new JavaScriptSerializer();
                return javaScriptSerializer.Serialize(objectToConverter);
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Convete o json para o objeto tipo
        /// </summary>
        public static TEntity JsonToEntity<TEntity>(this string strJson) where TEntity : class
        {
            try
            {
                var javaScriptSerializer = new JavaScriptSerializer();
                return javaScriptSerializer.Deserialize<TEntity>(strJson);
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Copia os do objectFrom para o objectTo quando o tipo e nome forem iguais.
        /// </summary>
        /// <typeparam name="TObjectTo"></typeparam>
        /// <param name="objectFrom"></param>
        /// <param name="objectTo"></param>
        public static void CopyProperties<TObjectTo>(this object objectFrom, ref TObjectTo objectTo)
        {
            try
            {
                Type tipoOrigem = objectFrom.GetType();
                Type tipoDestino = typeof(TObjectTo);

                PropertyInfo[] arrCamposOrigem = tipoOrigem.GetProperties();
                PropertyInfo[] arrCamposDestino = tipoDestino.GetProperties();

                foreach (PropertyInfo campoDestino in arrCamposDestino)
                {
                    PropertyInfo campoOrigem = arrCamposOrigem.Where(cpOrigem => cpOrigem.Name == campoDestino.Name).FirstOrDefault();

                    if (campoOrigem != null)
                    {
                        if (campoOrigem.PropertyType == campoDestino.PropertyType)
                        {
                            var vlrOrigem = tipoOrigem.GetProperty(campoOrigem.Name).GetValue(objectFrom, null);

                            //todo -> não está setando o valor
                            tipoDestino.GetProperty(campoDestino.Name).SetValue(objectTo, vlrOrigem, null);
                        }
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Retorna o total de caracteres de todas as variáveis do tipo String do objeto
        /// </summary>        
        public static int GetTotalCharacters(this object obj)
        {
            try
            {
                int total = 0;
                Type tipoObj = obj.GetType();
                PropertyInfo[] arrFields = tipoObj.GetProperties();

                foreach (PropertyInfo propertyInfo in arrFields.Where(p => p.PropertyType.Name == "String"))
                {
                    var proInfo = propertyInfo.GetValue(obj, null);
                    if (proInfo != null)
                    {
                        string vlrField = proInfo.ToString();
                        total += vlrField.Length;
                    }
                }

                return total;
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

    }
}
