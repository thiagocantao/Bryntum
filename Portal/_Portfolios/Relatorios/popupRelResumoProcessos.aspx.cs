using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Portfolios_Relatorios_popupRelResumoProcessos : System.Web.UI.Page
{
    
    dados cDados;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codWf = 0;
        int codInstanciaWf = 0;
        string nomeInstanciaWf = "";

        if (Request.QueryString["cwf"] != null && Request.QueryString["cwf"] != "null")
        {
            codWf = int.Parse(Request.QueryString["cwf"]);
        }
        if (Request.QueryString["ciwf"] != null && Request.QueryString["ciwf"] != "null")
        {
            codInstanciaWf = int.Parse(Request.QueryString["ciwf"]);
        }
        if (Request.QueryString["niwf"] != null && Request.QueryString["niwf"] != "null")
        {
            nomeInstanciaWf = Request.QueryString["niwf"];
        }
        
        relResumoProcessos rel = new relResumoProcessos(codigoEntidade, codWf, codInstanciaWf);
        DataSet dsTemporario = cDados.getResumoProcessosReport(codWf, codInstanciaWf);
        rel.Parameters["pCodigoWorkflow"].Value = codWf;
        rel.Parameters["pCodigoInstanciaWorkFlow"].Value = codInstanciaWf;
        rel.Parameters["pNomeInstanciaWf"].Value = nomeInstanciaWf;
        rel.Parameters["pDataImpressao"].Value = DateTime.Now.ToLocalTime();
        ReportViewer1.Report = rel;
        ReportViewer1.WritePdfTo(Response);
        //ReportViewer1.Visible = true;
    }
}