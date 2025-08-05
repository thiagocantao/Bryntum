using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_Relatorios_rel_RDO : System.Web.UI.Page
{

    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    public bool podeIncluir = false;
    protected void Page_Init(object sender, EventArgs e)
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());



    }






    protected void Page_Load(object sender, EventArgs e)
    {

        int codigoObra = int.Parse(Request.QueryString["CodigoProjeto"]);
        int codigoRDO = int.Parse(Request.QueryString["CodigoRdo"]);
        string dataRDO = Request.QueryString["DataRdo"];
        rel_RDO relatorio1 = new rel_RDO(codigoObra, codigoRDO, dataRDO);
        ReportViewer1.Report = relatorio1;
        ReportViewer1.WritePdfTo(Response);
    }
}