using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace Cdis.Brisk.Infra.Core.Util
{
    public struct ResourceItem
    {
        public string Key { get; set; }
        public string Text { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class ResourceUtil
    {
        /// <summary>
        /// Listar os itens de um arquivo Resource        
        /// </summary>        
        /// <param name="typeClassResource">Tipo do Resource</param>
        /// <param name="listKeyFilter">Itens que serão retornados, desconsiderando o restante</param>        
        public static List<ResourceItem> GetListResourceItem(Type typeClassResource, List<string> listKeyFilter)
        {
            ResourceManager resourceManager = new ResourceManager(typeClassResource);
            ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            List<ResourceItem> listResourceItem = new List<ResourceItem>();
            bool hasFilter = (listKeyFilter != null && listKeyFilter.Any());
            foreach (DictionaryEntry entry in resourceSet)
            {
                if ((!hasFilter) || (hasFilter && listKeyFilter.Contains(entry.Key.ToString())))
                {
                    listResourceItem.Add(new ResourceItem { Key = entry.Key.ToString(), Text = entry.Value.ToString() });
                }
            }

            return listResourceItem;
        }

        /// <summary>
        /// Listar os itens de um arquivo Resource        
        /// </summary>                
        public static List<ResourceItem> GetListResourceItem(Type typeClassResource)
        {
            return GetListResourceItem(typeClassResource, null);
        }
    }
}
