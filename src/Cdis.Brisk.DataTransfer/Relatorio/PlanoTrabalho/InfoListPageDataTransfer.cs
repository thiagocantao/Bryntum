namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public enum TipoItemList
    {
        Entrega = 1,
        Produto = 2,
        ResultadoEsperado = 3,
        AtividadeEntrega = 4,
    }
    public class InfoListPageDataTransfer
    {
        public TipoItemList TipoItemList { get; set; }
        public object Item { get; set; }
    }
}
