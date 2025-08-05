using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using naObra.Relatorios;

public partial class administracao_PrevisualizacaoImpressaoMedicao : System.Web.UI.Page
{
    int codigoEntidade;
    int codigoContrato;
    int codigoMedicao;
    decimal valorMes;
    decimal valorTotalAteMes;
    decimal valorPrevistoTotal;
    string nomeEmpresa;

    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoContrato = int.Parse(Request.QueryString["cc"]);
        codigoMedicao = int.Parse(Request.QueryString["cm"]);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string comandoSql = string.Format(@"
SELECT 
		  CASE WHEN CHARINDEX('Teles Pires', CAST(pcs.Valor as varchar(128)), 0 ) > 0
		        THEN 'Companhia Hidrelétrica Teles Pires S/A (CHTP)'
		        ELSE CAST(pcs.Valor as varchar(128))
		  END	AS NomeEmpresa,
	    ( SELECT count(1)
	      FROM Medicao m1
	      WHERE m1.CodigoContrato = c.CodigoContrato
	      AND ISNULL(m1.CodigoProjeto,0) = ISNULL(m.CodigoProjeto,0) 
	      AND m1.CodigoMedicao <= m.CodigoMedicao ) AS NumeroMedicao
FROM Contrato AS c INNER JOIN
		 Medicao AS m ON (m.CodigoContrato = c.CodigoContrato) INNER  JOIN
     ParametroConfiguracaoSistema AS pcs ON (  pcs.Parametro = 'nomeEmpresa' 
                                           AND pcs.CodigoEntidade = c.CodigoEntidade )
WHERE m.CodigoMedicao = {1}"
            , codigoEntidade, codigoMedicao);
        string numeroMedicao = string.Empty;
        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            valorMes = 0;//Convert.ToDecimal(dr["ValorMes"]);
            valorTotalAteMes = 0;//Convert.ToDecimal(dr["ValorTotalAteMes"]);
            valorPrevistoTotal = 0;//Convert.ToDecimal(dr["ValorPrevistoTotal"]);
            nomeEmpresa = dr["NomeEmpresa"].ToString();
            numeroMedicao = dr["NumeroMedicao"].ToString();
        }

        relMedicoes rel = new relMedicoes(codigoMedicao, 
                              codigoContrato, 
                              null, 
                              nomeEmpresa, 
                              false, 
                              valorPrevistoTotal, 
                              valorTotalAteMes, 
                              valorMes, 
                              new DataTable(), 
                              null, numeroMedicao);
        rel.CreateDocument();
        ReportViewer1.Report = rel;
    }
}