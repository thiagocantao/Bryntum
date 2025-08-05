namespace Cdis.Brisk.DataTransfer.Cronograma
{
    public struct InfoEapDataTransfer
    {
        public string CronogramaOficial { get; set; }
        public string CronogramaReplanejamento { get; set; }
        public System.DateTime? EAPBloqueadaEm { get; set; }
        public string EAPBloqueadaEmFormat
        {
            get
            {
                return EAPBloqueadaEm.HasValue ? EAPBloqueadaEm.Value.ToString("{0:dd/MM/yyyy}") : "";
            }
        }
        public string EAPBloqueadaPor { get; set; }
        public bool IsEdicaoEAP { get; set; }
        public bool IsUtilizaCronoInstalado { get; set; }
        public bool IsUtilizaNovaEAP { get; set; }
        public bool IsIndicaEAPEditavel
        {
            get
            {
                if (string.IsNullOrEmpty(IndicaEAPEditavel))
                {
                    return false;
                }
                return IndicaEAPEditavel.Equals("S");
            }
        }
        public string IndicaEAPEditavel { get; set; }

        public string BaseUrl { get; set; }

        public int IdUsuarioLogado { get; set; }
        public int CodProjeto { get; set; }
        public int CodEntidade { get; set; }
    }
}
