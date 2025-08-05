using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;

public partial class _Projetos_Administracao_LicoesAprendidas_popupArvoresConhecimento : System.Web.UI.Page
{
    dados cDados;
    public int alturaTelaUrl = 0;
    private int codigoUsuarioLogado;
    private int codigoEntidadeContexto;
    private int codigoElementoRaizArvore = -1;
    private int codigoElementoSelecionado = -1;
    public string resolucaoCliente;
    public int larguraTela = 0;
    public int alturaTela = 0;

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
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        cDados.getLarguraAlturaTela(resolucaoCliente, out larguraTela, out alturaTela);


    }

    protected void Page_Load(object sender, EventArgs e)
    {
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeContexto = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioLogado, codigoEntidadeContexto, codigoEntidadeContexto, "null", "EN", 0, "null", "LA_CnsArvCnh");
        }

        if (Request.QueryString["ALT"] != null)
        {
            int.TryParse(Request.QueryString["ALT"] + "", out alturaTelaUrl);
        }

        if (Request.QueryString["CERARV"] != null)
        {
            int.TryParse(Request.QueryString["CERARV"] + "", out codigoElementoRaizArvore);
        }
        if (Request.QueryString["CS"] != null)
        {
            int.TryParse(Request.QueryString["CS"] + "", out codigoElementoSelecionado);
        }
        carregaTlDados();
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            if (codigoElementoSelecionado > -1)
            {
                tlArvore.UnselectAll();

                TreeListNode node = tlArvore.FindNodeByKeyValue(codigoElementoSelecionado.ToString());
                if (node != null)
                    node.Selected = true;

            }
        }
        cDados.aplicaEstiloVisual(Page);
        //tlArvore.Settings.ScrollableHeight = alturaTela - 10;

    }
    private void carregaTlDados()
    {
        DataSet ds = getElementosArvore();

        if (cDados.DataSetOk(ds))
        {
            tlArvore.DataSource = ds;
            tlArvore.DataBind();
        }
    }
    public DataSet getElementosArvore()
    {
        string comandoSQL = string.Format(@"

DECLARE
	@CodigoEntidadeContexto			int						= {0}
  , @CodigoUsuarioSistema			int						= {1}
  , @CodigoElementoRaizArvore		int						= {2}
  , @IniciaisTipoAssociacao			char(2)				    = NULL
  , @CodigoTipoAssociacao			smallint			    = NULL
  , @IndicaExibicaoLCA               char(1)                = 'N'

SELECT * FROM [dbo].[f_lca_GetArvoreConhecimento] (@CodigoEntidadeContexto, @CodigoUsuarioSistema, @CodigoElementoRaizArvore, @IniciaisTipoAssociacao, @CodigoTipoAssociacao, @IndicaExibicaoLCA );
        ", codigoEntidadeContexto, codigoUsuarioLogado, codigoElementoRaizArvore);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    public string getDescricaoObjetosLista()
    {
        StringBuilder retornoHTML = new StringBuilder();

        string codigoElementoArvore = Eval("CodigoElementoArvore").ToString();
        string descricaoElementoArvore = Eval("DescricaoElementoArvore").ToString();
        string indicaElementoFolha = Eval("IndicaElementoFolha").ToString();
        string estiloIndicaEementoFolha = indicaElementoFolha == "S" ? "style='color: #1A0DAB'" : "";
        retornoHTML.AppendLine(string.Format(@"<table {0}>", estiloIndicaEementoFolha));
        retornoHTML.AppendLine("<tr>");
        retornoHTML.AppendLine("<td>");
        retornoHTML.AppendLine("<img border='0' src='../../../imagens/anexo/pastaAberta.gif' style='width: 21px; height: 18px;cursor:pointer;margin-right:5px' title=''/><span style='cursor:pointer'>" + descricaoElementoArvore + "</span>");
        retornoHTML.AppendLine("</td></tr></table>");

        return retornoHTML.ToString();
    }
    protected void menu_Init(object sender, EventArgs e)
    {
        DevExpress.Web.MenuItem btnIncluir = (sender as ASPxMenu).Items.FindByName("btnIncluir");
        btnIncluir.ToolTip = Resources.traducao.planoContasFluxoCaixa_incluir_uma_nova_conta_abaixo_da_conta_selecionada_;
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "abrePopUp('Conta','Incluir');TipoOperacao = 'Incluir';", false, false, false, "ArConh", "Arvore Conhecimento", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        cDados.exportaTreeList(ASPxTreeListExporter2, "XLS");
    }

    protected void ASPxTreeListExporter1_RenderBrick(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.Column.FieldName == "IndicaElementoFolha")
        {

            e.BrickStyle.BackColor = Color.Red;
        }
    }

    protected void tlArvore_DataBound(object sender, EventArgs e)
    {
        SetNodeSelectionSettings();
    }
    void SetNodeSelectionSettings()
    {
        TreeListNodeIterator iterator = tlArvore.CreateNodeIterator();
        TreeListNode node;
        while (true)
        {
            node = iterator.GetNext();
            if (node == null) break;
            node.AllowSelect = node.GetValue("IndicaElementoFolha").ToString().Trim() == "S";
        }
    }






}