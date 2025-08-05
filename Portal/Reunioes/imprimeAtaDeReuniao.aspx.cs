using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reunioes_imprimeAtaDeReuniao : System.Web.UI.Page
{
    public int codigoProjeto = 0;
    public int codigoEvento = 0;
    private dados cDados;
    public int codigoUsuarioResponsavel = 0;
    public int codigoEntidadeUsuarioResponsavel = 0;
    public string moduloSistema = "";


    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

    }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        codigoProjeto = int.Parse(Request.QueryString["CP"]);
        codigoEvento = int.Parse(Request.QueryString["CE"]);
        moduloSistema = Request.QueryString["MOD"];

        rel_ImprimeAtaDeReuniao relatorio = new rel_ImprimeAtaDeReuniao(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, codigoEvento, moduloSistema);

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        ReportViewer1.Report = relatorio;
        ReportViewer1.WritePdfTo(Response);
    }
}