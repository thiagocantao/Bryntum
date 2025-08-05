using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using DevExpress.Web;
using System.Text;
using System.Web.UI.WebControls;

public partial class _Processos_Visualizacao_index : System.Web.UI.Page
{
    #region Fields

    protected string telaInicial;
    protected string alturaTabela;

    bool indicaVisualizacaoProcessos;
    string codigoModuloMenu;
    int codigoEntidade;
    int codigoUsuarioLogado;
    dados cDados;
    DataSet ds;

    #endregion

    #region Properties

    protected string IniciaisMenuFavoritos
    {
        get
        {
            StringBuilder imf = new StringBuilder();
            imf.Append(indicaVisualizacaoProcessos ? "LDP" : "LDR");
            imf.Append(string.IsNullOrWhiteSpace(codigoModuloMenu) ? "ENT" : codigoModuloMenu);

            return imf.ToString();
        }
    }

    #endregion

    #region Events Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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
            Response.RedirectLocation = string.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        string tipo = Request.QueryString["tipo"];
        codigoModuloMenu = Request.QueryString["cmm"];
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        indicaVisualizacaoProcessos = string.IsNullOrEmpty(tipo) || tipo.ToUpper().Equals("P");

        pnlModulos.Visible = !indicaVisualizacaoProcessos;

        if (!IsPostBack)
            DefineVariaveisSessao();

        string descricaoModulo;
        switch (codigoModuloMenu)
        {
            case "ADM":
                descricaoModulo = "(" + Resources.traducao.index_administra__o + ")";
                break;
            case "PRJ":
                descricaoModulo = "(" + Resources.traducao.index_projetos + ")";
                break;
            case "ESP":
                descricaoModulo = "(" + Resources.traducao.index_espa_o_de_trabalho + ")";
                break;
            case "EST":
                descricaoModulo = "(" + Resources.traducao.index_estrat_gia + ")";
                break;
            default:
                descricaoModulo = string.Empty;
                break;
        }
        lblTituloTela.Text = String.Format(Resources.traducao.index_gest_o_din_mica_de__0___1_, indicaVisualizacaoProcessos ? Resources.traducao.index_processos : Resources.traducao.index_relat_rios, descricaoModulo);

        if (!IsPostBack)
            cardViewRelatorios.FocusedCardIndex = -1;

        cardViewRelatorios.CardLayoutProperties.SettingsAdaptivity.AdaptivityMode = FormLayoutAdaptivityMode.SingleColumnWindowLimit;
        cardViewRelatorios.CardLayoutProperties.SettingsAdaptivity.SwitchToSingleColumnAtWindowInnerWidth = 600;
        /*
        cardViewRelatorios.Settings.LayoutMode = DevExpress.Web.Layout.Flow;
        cardViewRelatorios.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
        cardViewRelatorios.Styles.FlowCard.Width = 530;
        cardViewRelatorios.Styles.FlowCard.Height = 250;
        cardViewRelatorios.Styles.FlowCard.CssClass = "FlowCard";
        cardViewRelatorios.CardLayoutProperties.ColCount = 2;
        */

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int nivel = 1;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["NivelNavegacao"]))
                nivel = int.Parse(Request.QueryString["NivelNavegacao"]);
            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();

            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, IniciaisMenuFavoritos, "ENT", codigoEntidade, "<%# Resources.traducao.index_adicionar_aos_favoritos %>");
        }
        if (!IsCallback)
        {
            DefineAlturaTela();
        }
        this.Title = cDados.getNomeSistema();

        cDados.aplicaEstiloVisual(this);

        DefineStringConexao();
    }

    private void DefineStringConexao()
    {
        string connectionString = cDados.classeDados.getStringConexao();
        sdsListaConsultas.ConnectionString = connectionString;
        sdsConsultas.ConnectionString = connectionString;
    }

    private void DefineVariaveisSessao()
    {
        Session["ce"] = codigoEntidade;
        Session["cu"] = codigoUsuarioLogado;
        Session["ip"] = indicaVisualizacaoProcessos;
        Session["cmm"] = new string(' ', 3);
    }

    protected void gvConsultas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "IndicaListaPadrao")
            e.Editor.ReadOnly = (e.Value as string) == "S";
    }

    protected void popup_WindowCallback(object source, PopupWindowCallbackArgs e)
    {
        if (e.Window.Name == "winGerenciarConsultas")
        {
            string[] parametros = e.Parameter.Split(';');
            int codigoUsuario = int.Parse(parametros[0]);
            int codigoLista = int.Parse(parametros[1]);
            Session["codUsuario"] = codigoUsuario;
            Session["codLista"] = codigoLista;
            gvConsultas.CancelEdit();
            gvConsultas.DataBind();
            SelecionaLinhaPadrao();
        }
    }

    protected void gvConsultas_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (gvConsultas.VisibleRowCount <= 1)
            return;

        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            string indicaListaPadrao = gvConsultas.GetRowValues(
                e.VisibleIndex, "IndicaListaPadrao") as string;
            if (indicaListaPadrao == "S")
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
                e.Image.ToolTip = "<%# Resources.traducao.index_s____poss_vel_excluir_uma_consulta_definida_como_padr_o_quando_n_o_houver_outras_consultas_ %>";
            }
        }
    }

    #endregion

    #region Methods

    private short RetornaValorMaximoSeNulo(DataRow row, string nomeCampo)
    {
        if (row.IsNull(nomeCampo))
            return short.MaxValue;

        return row.Field<short>(nomeCampo);
    }

    private string ObtemUrlItem(int codLista, string tipoLista, string titulo)
    {
        switch (tipoLista.ToUpper())
        {
            case "FORMULARIO":
                throw new NotImplementedException("Página de FORMULARIO ainda não foi criada");
            case "OLAP":
                return string.Format("VisualizacaoOlap.aspx?cl={0}&titulo={1}", codLista, titulo);
            case "PROCESSO":
                return string.Format("VisualizacaoGrid.aspx?cl={0}&titulo={1}", codLista, titulo);
            case "RELATORIO":
                return string.Format("VisualizacaoGrid.aspx?cl={0}&ir=S&titulo={1}", codLista, titulo);
            case "ARVORE":
                return string.Format("VisualizacaoTreeGrid.aspx?cl={0}&titulo={1}", codLista, titulo);
            case "DASHBOARD":
                return string.Format("VisualizacaoDashboard.aspx?cl={0}&titulo={1}", codLista, titulo);
            case "REPORT":
                return string.Format("VisualizacaoRelatorio.aspx?cl={0}&titulo={1}", codLista, titulo);
            default:
                return string.Empty;
        }
    }

    private void DefineAlturaTela()
    {

        string res = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(res.Substring(res.IndexOf('x') + 1));
        int larguraTela = int.Parse(res.Substring(0, res.IndexOf('x')));
        alturaTabela = String.Format("{0}px", (alturaTela - 140));
        //sp_Tela.Height = alturaTela + 100;
        //sp_Tela.Panes[1].Size = new Unit(larguraTela - 180);

    }

    private void SelecionaLinhaPadrao()
    {
        string indicaListaPadrao = "N";
        for (int i = 0; i < gvConsultas.VisibleRowCount; i++)
        {
            indicaListaPadrao = gvConsultas.GetRowValues(i, "IndicaListaPadrao") as string;
            if (indicaListaPadrao == "S")
            {
                gvConsultas.Selection.UnselectAll();
                gvConsultas.Selection.SelectRow(i);
                break;
            }
        }
        if (indicaListaPadrao == "N")
        {
            btnConfirmar_SelecaoConsultas.ClientVisible = false;
            btnCancelar_SelecaoConsultas.Text = "Fechar";
        }
    }

    #endregion

    protected void cardViewRelatorios_CustomCallback(object sender, ASPxCardViewCustomCallbackEventArgs e)
    {
        cardViewRelatorios.FocusedCardIndex = - 1;
        const int ModuloNameLength = 3;
        Session["cmm"] = e.Parameters ?? new string(' ', ModuloNameLength);
        cardViewRelatorios.DataBind();
    }

    protected void callbackBotoes_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter == "DELETEROW")
        {
            if (gvConsultas.VisibleRowCount == 0)
            {
                btnConfirmar_SelecaoConsultas.Text = "Fechar";
                btnCancelar_SelecaoConsultas.ClientVisible = false;
            }
        }
    }
}
