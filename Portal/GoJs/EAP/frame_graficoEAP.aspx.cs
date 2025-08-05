using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class GoJs_EAP_frame_graficoEAP : System.Web.UI.Page
{
    private dados cDados;
    private int codigoCarteira;
    private int codigoEntidade;
    private int codigoUsuarioLogado;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }

        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string codigoCronograma = "";
        int codigoWorkflow = -1;
        int codigoInstanciaWf = -1;
        int codigoProjetoRelacionado = -1;

        if(Request.QueryString["CWF"] != null && (Request.QueryString["CWF"] + "") != "")
        {
            codigoWorkflow = int.Parse(Request.QueryString["CWF"]);
        }
        if (Request.QueryString["CIWF"] != null && (Request.QueryString["CIWF"] + "") != "")
        {
            codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"]);
        }

        string comandoSQL_IDProjeto = string.Format(@"
        SELECT IdentificadorProjetoRelacionado
        FROM InstanciasWorkflows 
        WHERE CodigoWorkflow = {0} AND CodigoInstanciaWf = {1}", codigoWorkflow, codigoInstanciaWf);

        DataSet ds = cDados.getDataSet(comandoSQL_IDProjeto);
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoProjetoRelacionado = int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        string comandoSQL_Cronograma = string.Format(@"select CodigoCronogramaProjeto from CronogramaProjeto where CodigoProjeto = {0}", codigoProjetoRelacionado);
        ds = cDados.getDataSet(comandoSQL_Cronograma);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronograma = ds.Tables[0].Rows[0][0].ToString();
        }

        var frame = frameConteudo as HtmlControl;
        string url = "~/GoJs/EAP/graficoEAP.aspx" + Request.Url.Query.Replace("?&", "?");

        url += "&CCR=" + codigoCronograma;
        url += "&IDProjeto=" + codigoProjetoRelacionado;
        url += "&CE=" + codigoEntidade;        
        url += "&CU=" + codigoUsuarioLogado;        
        url += "&AM=RW";
        frame.Attributes["src"] = url;
    }
}