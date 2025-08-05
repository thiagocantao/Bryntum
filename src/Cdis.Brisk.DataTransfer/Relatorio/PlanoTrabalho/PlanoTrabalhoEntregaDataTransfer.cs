using Cdis.Brisk.Infra.Core.Extensions;

namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public class PlanoTrabalhoEntregaDataTransfer
    {
        public string Acao { get; set; }
        public string EntregaPrevista { get; set; }
        public string EntregaRealizada { get; set; }
        public string StatusEntrega { get; set; }

        public int TotalCaracteres
        {
            get
            {
                return this.GetTotalCharacters();
            }
        }
    }
}
