using Cdis.Brisk.Infra.Core.Extensions;

namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public class PlanoTrabalhoProdutoDataTransfer
    {
        public string Descricao { get; set; }

        public int TotalCaracteres
        {
            get
            {
                return this.GetTotalCharacters();
            }
        }
    }
}
