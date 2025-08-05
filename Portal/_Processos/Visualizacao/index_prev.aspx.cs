using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using DevExpress.Web;
using System.Text;
using System.Web.UI.WebControls;

public partial class _Processos_Visualizacao_index_prev : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
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
        /*
        lblTituloTela.Text = string.Format("<%# Resources.traducao.index_gest_o_din_mica_de__0___1_ %>",
            indicaVisualizacaoProcessos ? "<%# Resources.traducao.index_processos %>" : "<%# Resources.traducao.index_relat_rios %>", descricaoModulo);]
        */



        if (!IsPostBack)
        {
            CarregaMenuLateral();
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
            DefineTelaInicial();
        }
        this.Title = cDados.getNomeSistema();
        cDados.aplicaEstiloVisual(sp_Tela);
        cDados.aplicaEstiloVisual(mvbMenu);

        cDados.aplicaEstiloVisual(this);

        sdsConsultas.ConnectionString = cDados.classeDados.getStringConexao();
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/index_prev.js""></script>"));
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
            SelecionaLinhaPadrão();            
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

    private void DefineTelaInicial()
    {


        if (mvbMenu.SelectedItem == null)
        {
            var grupo = mvbMenu.Groups.FirstOrDefault(g => g.Items.Count > 0);
            if (grupo == null)
                return;

            var item = grupo.Items.First();
            DefineTelaInicial(item.NavigateUrl);
        }
        else
            DefineTelaInicial(mvbMenu.SelectedItem.NavigateUrl);
    }

    private void DefineTelaInicial(string url)
    {

        telaInicial = url;
        telaInicial = telaInicial.Replace("~/", String.Format("{0}{1}",
            Request.Url.GetLeftPart(UriPartial.Authority), cDados.getPathSistema()));
        sp_Tela.Panes[1].ContentUrl = telaInicial;
    }

    private short RetornaValorMaximoSeNulo(DataRow row, string nomeCampo)
    {
        if (row.IsNull(nomeCampo))
            return short.MaxValue;

        return row.Field<short>(nomeCampo);
    }

    private void CarregaMenuLateral()
    {
        string tiposListaVisualizacao = indicaVisualizacaoProcessos ?
            "'PROCESSO'" : "'OLAP', 'RELATORIO', 'ARVORE', 'DASHBOARD', 'REPORT'";
        string comandoSql;

        #region Comando SQL

        string filtroModuloMenu = string.IsNullOrEmpty(codigoModuloMenu) ?
            String.Empty : string.Format("AND CodigoModuloMenu = '{0}'", codigoModuloMenu);

        comandoSql = string.Format(@"
    DECLARE @CodigoEntidade INT
        SET @CodigoEntidade = {0}

    SELECT [CodigoLista]
          ,[NomeLista]
          ,[GrupoMenu]
          ,[ItemMenu]
          ,[GrupoPermissao]
          ,[ItemPermissao]
          ,[IniciaisPermissao]
          ,[TituloLista]
          ,[ComandoSelect]
          ,[IndicaPaginacao]
          ,[QuantidadeItensPaginacao]
          ,[IndicaOpcaoDisponivel]
          ,[TipoLista]
          ,[URL]
          ,[CodigoEntidade]
          ,[CodigoModuloMenu]
          ,[OrdemGrupoMenu]
          ,[OrdemItemGrupoMenu]
      FROM [Lista] AS [l]
     WHERE IndicaOpcaoDisponivel = 'S'
       AND l.CodigoEntidade = @CodigoEntidade
       AND l.TipoLista IN ({2})
       AND (dbo.f_VerificaAcessoConcedido({1}, @CodigoEntidade, @CodigoEntidade, null, 'EN', 0, null, l.IniciaisPermissao) = 1)
       {3}
     ORDER BY
           l.GrupoMenu,
           l.TituloLista"
                    , codigoEntidade
                    , codigoUsuarioLogado
                    , tiposListaVisualizacao
                    , filtroModuloMenu);

        #endregion

        ds = cDados.getDataSet(comandoSql);
        var dt = ds.Tables[0];
        var gruposLinhasOrdenadas = dt.AsEnumerable()
            .GroupBy(r => r.Field<string>("GrupoMenu"))
            .OrderBy(g => g.Max(r => RetornaValorMaximoSeNulo(r, "OrdemGrupoMenu")))
            .ThenBy(g => g.Key);

        string sufixo = ObtemSufixoPaginasRelatoriosDinamicos();
        foreach (var grupo in gruposLinhasOrdenadas)
        {
            NavBarGroup grupoMenu = mvbMenu.Groups.Add(grupo.Key);
            foreach (var item in grupo)
            {
                short ordemItemGrupoMenu = RetornaValorMaximoSeNulo(item, "OrdemItemGrupoMenu");
                int codigoLista = item.Field<int>("CodigoLista");
                string tipoLista = item.Field<string>("TipoLista");
                string titulo = item.Field<string>("TituloLista");
                string nome = item.Field<string>("NomeLista");
                string url;
                string target;
                if (Convert.IsDBNull(item["URL"]))
                {
                    url = ObtemUrlItem(codigoLista, tipoLista, sufixo);
                    target = "framePrincipal";
                }
                else
                {
                    url = item.Field<string>("URL");
                    target = "framePrincipal";
                }
                grupoMenu.Items.Add(titulo, nome, string.Empty, url, target);
            }
        }
    }

    private string ObtemUrlItem(int codLista, string tipoLista, string sufixo)
    {
        switch (tipoLista.ToUpper())
        {
            case "FORMULARIO":
                throw new NotImplementedException("Página de FORMULARIO ainda não foi criada");
            case "OLAP":
                return string.Format("VisualizacaoOlap" + sufixo + ".aspx?cl={0}", codLista);
            case "PROCESSO":
                return string.Format("VisualizacaoGrid" + sufixo + ".aspx?cl={0}", codLista);
            case "RELATORIO":
                return string.Format("VisualizacaoGrid" + sufixo + ".aspx?cl={0}&ir=S", codLista);
            case "ARVORE":
                return string.Format("VisualizacaoTreeGrid" + sufixo + ".aspx?cl={0}", codLista);
            case "DASHBOARD":
                return string.Format("VisualizacaoDashboard.aspx?cl={0}", codLista);
            case "REPORT":
                return string.Format("VisualizacaoRelatorio.aspx?cl={0}", codLista);
            default:
                return string.Empty;
        }
    }


    private string ObtemSufixoPaginasRelatoriosDinamicos()
    {
        if (ObtemParametroIndicaListaRelatoriosEmCard())
            return string.Empty;

        const string sufixo = "_prev";
        return sufixo;
    }

    private bool ObtemParametroIndicaListaRelatoriosEmCard()
    {
        const string nomeParametro = "indicaListaRelatoriosEmCard";
        var drParametro = cDados.getParametrosSistema(nomeParametro).Tables[0].AsEnumerable().FirstOrDefault();
        return
            drParametro != null &&
            !drParametro.IsNull(nomeParametro) &&
            drParametro.Field<string>(nomeParametro).ToUpper().Equals("S");
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

    private void SelecionaLinhaPadrão()
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
    protected void callbackBotoes_Callback(object sender, CallbackEventArgsBase e)
    {
        if(e.Parameter == "DELETEROW")
        {
            if (gvConsultas.VisibleRowCount == 0)
            {
                btnConfirmar_SelecaoConsultas.Text = "Fechar";
                btnCancelar_SelecaoConsultas.ClientVisible = false;
            }
        }
    }


}
