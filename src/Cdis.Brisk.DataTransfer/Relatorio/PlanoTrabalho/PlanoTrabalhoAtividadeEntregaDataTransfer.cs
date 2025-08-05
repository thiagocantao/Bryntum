using Cdis.Brisk.Infra.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public class PlanoTrabalhoAtividadeEntregaDataTransfer
    {
        public string ProdutoServico { get; set; }
        public string Meta { get; set; }
        public string Prazo { get; set; }
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
