using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class _Portfolios_frameProposta_DemandaRelacionada : System.Web.UI.Page
{
    dados cDados;

    public int codigoProjetoSelecionado, codigoEntidadeUsuarioResponsavel;    

    public string alturaTela = "";
    private int codigoUsuarioResponsavel;

    public string definelegenda = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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


        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["CP"] != null)
        {
            string temp = Request.QueryString["CP"].ToString();
            if (temp != "")
            {
                cDados.setInfoSistema("CodigoProjeto", int.Parse(temp));
            }
        }
        codigoProjetoSelecionado = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        carregaGvDados();

    }

    private void carregaGvDados()
    {
        string where = string.Format(@" AND p.codigoProjeto in(select lp.CodigoProjetoFilho from LinkProjeto lp where lp.CodigoProjetoPai = {0} and lp.TipoLink = 'PJPJ')", codigoProjetoSelecionado);
        DataSet ds = cDados.getDemandasRelacionadas(where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }


    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

    }


  

}
