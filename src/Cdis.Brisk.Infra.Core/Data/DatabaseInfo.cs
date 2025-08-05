namespace Cdis.Brisk.Infra.Core.Data
{
    public static class DatabaseInfo
    {
        /// <summary>
        /// Retornar o dbOwner do web.config
        /// </summary>        
        public static string GetOwnerBb()
        {
            return System.Configuration.ConfigurationManager.AppSettings["dbOwner"].ToString();
        }

        /// <summary>
        /// Retornar o nome do database da StringConnection do web.config
        /// </summary>        
        public static string GetDatabaseNameSqlServer(string strCon)
        {
            string nome = strCon.Substring(strCon.IndexOf("Initial"), strCon.IndexOf(';', strCon.IndexOf("Initial")) - strCon.IndexOf("Initial"));
            nome = nome.Substring(nome.IndexOf("=") + 1).Trim();
            return nome;
        }
    }
}
