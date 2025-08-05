namespace Cdis.Brisk.Domain.Domains.Cronograma
{
    /// <summary>
    /// 
    /// </summary>
    /// <tableName>
    /// 
    /// </tableName>
    /// <schemaName>
    /// 
    /// </schemaName>  
    public class CronogramaDependenciaDomain
    {
        public int ToTask { get; set; }
        public int FromTask { get; set; }
        public string TipoLatencia { get; set; }
    }
}
