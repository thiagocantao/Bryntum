using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_Administracao_PlanoDeTrabalho : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;

    private int codigoProjeto = -1;

    DataSet dsMetas = new DataSet();
    DataSet dsParcerias = new DataSet();
    DataSet dsMarcos = new DataSet();

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        if (Request.QueryString["CP"] != null)
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
            cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);
        }

        carregaGvDados();

        cDados.aplicaEstiloVisual(Page);

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvDados.Settings.VerticalScrollableHeight = altura - 295;
    }

    private void carregaGvDados()
    {
        DataSet ds = cDados.getPlanoTrabalhoProjeto(codigoProjeto, "");

        gvDados.Columns["Parcerias"].Caption = "<table><tr><td style='width:150px'>Área Parceira</td><td style='width:230px'>Elementos da Parceria</td></tr></table>";

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

        dsMetas = cDados.getMetasPlanoTrabalhoProjeto(codigoProjeto, "");
        dsParcerias = cDados.getParceriasPlanoTrabalhoProjeto(codigoProjeto, "");
        dsMarcos = cDados.getMarcosPlanoTrabalhoProjeto(codigoProjeto, "");
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Name == "Metas")
        {
            e.Cell.Text = getTabelaMetas(e.CellValue + "");
        }
        else if (e.DataColumn.Name == "Parcerias")
        {
            e.Cell.Text = getTabelaParcerias(e.CellValue + "");
        }
        else if (e.DataColumn.Name == "Marcos")
        {
            e.Cell.Text = getTabelaMarcos(e.CellValue + "");
        }
    }

    private string getTabelaMetas(string codigoObjeto)
    {
        codigoObjeto = codigoObjeto == "" ? "-1" : codigoObjeto;

        DataRow[] drs = dsMetas.Tables[0].Select("Codigo = " + codigoObjeto);

        string linhas = "";

        foreach (DataRow dr in drs)
        {
            linhas += string.Format(@"<tr><td>{0}</td></tr>", dr["Meta"]);
        }

        return string.Format(@"<table style='width:100%'>{0}</table>", linhas);
    }

    private string getTabelaParcerias(string codigoObjeto)
    {
        codigoObjeto = codigoObjeto == "" ? "-1" : codigoObjeto;

        DataRow[] drs = dsParcerias.Tables[0].Select("Codigo = " + codigoObjeto + " OR CodigoObjetoPai = " + codigoObjeto);

        string linhas = "";

        foreach (DataRow dr in drs)
        {
            linhas += string.Format(@"<tr><td style='width:150px'>{0}</td><td style='width:230px'>{1}</td></tr>", dr["Area"], dr["Elemento"]);
        }
        
        return string.Format(@"<table>{0}</table>", linhas);
    }

    private string getTabelaMarcos(string codigoObjeto)
    {
        codigoObjeto = codigoObjeto == "" ? "-1" : codigoObjeto;

        DataRow[] drs = dsMarcos.Tables[0].Select("Codigo = " + codigoObjeto + " OR CodigoObjetoPai = " + codigoObjeto);

        string linhas = "";

        foreach (DataRow dr in drs)
        {
            linhas += string.Format(@"<tr><td>{0}</td></tr>", dr["Marco"]);
        }

        return string.Format(@"<table style='width:100%'>{0}</table>", linhas);
    }
}