namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public class InfoItemPageDataTranfer
    {
        public InfoItemPageDataTranfer this[string name] { get { return new InfoItemPageDataTranfer(); } }
        public int NumPage { get; set; }
        public int NumGrupo { get; set; }
        public int NumOrdem { get; set; }
        public string NomeItem { get; set; }
        public string DescTitulo { get; set; }
        public string Valor { get; set; }
    }
}
