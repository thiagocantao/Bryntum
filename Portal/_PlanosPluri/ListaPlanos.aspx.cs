using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _PlanosPluri_ListaPlanos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;
    bool podeIncluir = true;
    bool podeEditar = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/listaPPA.js""></script>"));

        cDados.aplicaEstiloVisual(Page);

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
           codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AltPlnPla");

        podeEditar = podeIncluir;

        verificaInclusaoNovoPlano();

        carregaGrid();

        if (!IsPostBack)
        {            
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "LPPA", "ENT", -1, "Adicionar Lista aos Favoritos");
        }
    }

    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT p.CodigoPlano, p.NomePlano, p.CodigoPlanoSuperior, p.CodigoUnidadeNegocio, p.CodigoPortfolioAssociado		
			  ,p.CodigoStatusPlano, p.IndicaPlanoConsolidador, un.NomeUnidadeNegocio, tsp.TipoStatus 
         FROM dbo.Plano p INNER JOIN
			  dbo.TipoStatusPlano tsp ON tsp.CodigoStatus = p.CodigoStatusPlano INNER JOIN
			  dbo.UnidadeNegocio un ON (un.CodigoUnidadeNegocio = p.CodigoUnidadeNegocio
														 AND un.CodigoEntidade = {0})"
        ,codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    private void verificaInclusaoNovoPlano()
    {
        string comandoSQL = string.Format(@"
        SELECT 1
         FROM dbo.Plano p 
        WHERE CodigoUnidadeNegocio = {0}
          AND CodigoStatusPlano IN(1, 2)"
        , codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            podeIncluir = false;
        }
    }

    public string getLinkPlano()
    {
        string nomePlano = Eval("NomePlano").ToString();
        string codigoPlano = Eval("CodigoPlano").ToString();
        string url = string.Format(@"{0}_PlanosPluri/DadosPlano/indexResumoPlano.aspx?CP={1}&NP={2}"
        ,cDados.getPathSistema()
        ,codigoPlano
        ,Server.UrlEncode(nomePlano));

        return "<a style='cursor:pointer' href='" + url + "'>" + nomePlano + "<a/>";
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstPlPl");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abreEdicaoPlano(-1);", true, true, false, "LstPlPl", lblTituloTela.Text, this);
    }

    #endregion

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if(e.ButtonID == "btnEditar" && !podeEditar)
        {
            e.Enabled = false;
            e.Image.Url = "~/imagens/botoes/editarRegDes.png";
        }
    }
}