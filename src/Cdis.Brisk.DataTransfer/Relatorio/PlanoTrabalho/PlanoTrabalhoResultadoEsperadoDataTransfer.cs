using Cdis.Brisk.Infra.Core.Extensions;

namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public class PlanoTrabalhoResultadoEsperadoDataTransfer
    {
        public string NomeTransformacao { get; set; }
        public string NomeIndicador { get; set; }
        public string DscMeta { get; set; }
        public string StrDtPrazo { get; set; }

        public int TotalCaracteres
        {
            get
            {
                return this.GetTotalCharacters();
            }
        }

    }
}
