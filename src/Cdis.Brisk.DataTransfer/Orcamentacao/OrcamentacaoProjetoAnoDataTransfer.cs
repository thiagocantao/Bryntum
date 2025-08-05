using System.Data;

namespace Cdis.Brisk.DataTransfer.Orcamentacao
{
    /// <summary>
    /// 
    /// </summary>
    public class OrcamentacaoProjetoAnoDataTransfer
    {
        public short? NumAno { get; set; }
        public short CodigoPrevisao { get; set; }
        public DataTable SourceDataTableOrcamentacao { get; set; }
    }
}
