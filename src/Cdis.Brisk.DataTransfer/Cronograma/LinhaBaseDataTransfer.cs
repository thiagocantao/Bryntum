namespace Cdis.Brisk.DataTransfer.Cronograma
{
    public class LinhaBaseDataTransfer
    {
        public short NumVersao { get; set; }
        public short NumLinhaBase { get; set; }
        public string Situacao { get; set; }
        //public string Descricao { get; set; }
        public string DataSolicitacao { get; set; }
        public string NomeSolicitante { get; set; }
        public string DataAprovacao { get; set; }
        public string NomeAprovador { get; set; }
        public string Anotacao { get; set; }
    }
}
