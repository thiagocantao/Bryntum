using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using DevExpress.Web;
using System.Web.Hosting;

public partial class _Estrategias_Relatorios_popupRelIdentidadeOrganizacional : System.Web.UI.Page
{
    dados cDados;

    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoUsuarioLogado = 0;
    public int codUnidade = -1;
    public int codigoStatus = -1;
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

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (Request.QueryString["CUN"] != null && Request.QueryString["CUN"].ToString() + "" != "")
        {
            codUnidade = int.Parse(Request.QueryString["CUN"].ToString());
        }
        if (Request.QueryString["STA"] != null && Request.QueryString["STA"].ToString() + "" != "")
        {
            codigoStatus = int.Parse(Request.QueryString["STA"].ToString());
        }
        relProjetosObjetivos rel = new relProjetosObjetivos();

        string where = "";
        if (codUnidade != -1 )
        {
            where = " AND un.CodigoUnidadeNegocio = " + codUnidade;
        }
        if (codigoStatus != -1)
        {
            where += " AND s.CodigoStatus = " + codigoStatus;
        }
        rel.DataSource = cDados.getRelProjetosPropostas(codigoEntidadeUsuarioResponsavel, codigoUsuarioLogado, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), where).Tables[0];

        ReportViewer1.Report = rel;
        ReportViewer1.WritePdfTo(Response);

    }
}