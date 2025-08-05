using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.Function
{
    /// <nameFunction>
    /// [dbo].f_crono_GetVersoesLBProjeto
    /// </nameFunction>  
    public class FCronoGetVersoesLbProjetoDomain
    {
        public string Anotacoes { get; set; }
        public string CodigoCronogramaProjeto { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public DateTime? DataStatusAprovacao { get; set; }
        public Int16? ModeloLinhaBase { get; set; }
        public string NomeAprovador { get; set; }
        public string NomeSolicitante { get; set; }
        public int NumeroVersao { get; set; }
        public string StatusAprovacao { get; set; }
        public Int16? VersaoLinhaBase { get; set; }

        [NotMapped]
        public string Situacao
        {
            get
            {
                return
                    StatusAprovacao == "AP"
                        ? "Aprovado"
                        : StatusAprovacao == "RP"
                        ? "Reprovado"
                        : "Pendente de Aprovação";
            }
        }
    }
}
