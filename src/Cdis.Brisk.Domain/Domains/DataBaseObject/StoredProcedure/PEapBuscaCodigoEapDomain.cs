
namespace Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure
{
    /// <summary>
    /// SP p_eap_BuscaCodigoEAP
    /// </summary>
    public class PEapBuscaCodigoEapDomain
    {
        public string CronogramaOficial { get; set; }
        public string CronogramaReplanejamento { get; set; }
        public string IndicaEAPEditavel { get; set; }
        public System.DateTime? EAPBloqueadaEm { get; set; }
        public string EAPBloqueadaPor { get; set; }
    }
}
