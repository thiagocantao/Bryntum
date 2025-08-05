/*
 * MUDANÇAS:
 * 
 * 24/03/2011 :: Alejandro : Adaptação para permissões. [OB_Ass_IN], [OB_Ass_PR].
 * 29/03/2011 :: Alejandro : Adaptação para permissões. [IN_CnsDtl], [PR_Acs], [OB_CnsDtl].
 */
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
using System.Drawing;

public partial class _Estrategias_detalhesObjetivoEstrategico : System.Web.UI.Page
{
    dados cDados;
    DataTable dt;

    public int codigoUnidadeSelecionada = 0;
    public int codigoUnidadeLogada = 0;
    private int idUsuarioLogado = 0;
    private int idObjetoPai = 0;
    private int codigoObjetivoEstrategico = 0;
    private int alturaPrincipal;
    int codigoMapa = 0;

    public string alturaTabela;
    public string alturaLista;
    private string resolucaoCliente;

    public bool permissaoMapa = false;
    public bool podeAssIn = false;
    public bool podeAssPr = false;
    public bool podeCnsDtl = false;

    public bool podeEditarPlanoAcao = false;
    public bool podeExcluirPlanoAcao = false;

    bool indicaEntidade = true;

    public string displayLaranja = "";
    

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        this.TH(this.TS("detalhesObjetivoEstrategico"));

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        sdsProjetos.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
       if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        permissaoMapa = true; // cDados.verificaPermissaoOE(codigoObjetivoEstrategico, codigoUnidadeSelecionada);
        getPermissoesTela();

        if (!podeCnsDtl)
            cDados.RedirecionaParaTelaSemAcessoSemMasterPage(this);

        DataSet dsParametros = cDados.getParametrosSistema("MostraFisicoLaranja");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {

            if (dsParametros.Tables[0].Rows[0]["MostraFisicoLaranja"].ToString().Trim() == "N")
                displayLaranja = "display:none;";
        }

        string comandoSQL = string.Format(@"
            SELECT 1 FROM UnidadeNegocio WHERE CodigoUnidadeNegocio = CodigoEntidade AND CodigoUnidadeNegocio = " + codigoUnidadeSelecionada);

        DataSet ds = cDados.getDataSet(comandoSQL);

        indicaEntidade = cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]);

        //setea la tela
        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();



        gridIndicadores.Settings.ShowFilterRow = false;


        carregaGridIndicador("");
        carregaGridProjetos("");

        gridProjetos.JSProperties["cp_COE"] = codigoObjetivoEstrategico;
        gridProjetos.JSProperties["cp_UN"] = codigoUnidadeSelecionada;

        btnSalvarAssociacaoIndicador.JSProperties["cp_utilizaPesoDesempenhoObjetivo"] = getIndicaUtilizaPesoDesempenhoObjetivo() == true ? "S" : "N";
        if (!IsPostBack)
        {
            carregaCampos();
        }

        //imgGantt.ClientSideEvents.Click = "function(s, e) { abreGanttOE(" + codigoObjetivoEstrategico + ");}";

        carregaComboProjetos();
        carregaComboMeta();
        carregaGridAcoesSugeridas(-1, false);
        carregaComboAssociaIndicadores();

        gvAcoes.Settings.ShowFilterRow = false;
        gvAcoes.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gridIndicadores.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gridProjetos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gridProjetos.Settings.ShowFilterRow = false;

        tdPeso3.Style.Add("display", getIndicaUtilizaPesoDesempenhoObjetivo() == true ? "block" : "none");

    }

    public bool getIndicaUtilizaPesoDesempenhoObjetivo()
    {
        bool retorno = false;
        DataSet dsParamutilizaPesoDesempenhoObjetivo;
        dsParamutilizaPesoDesempenhoObjetivo = cDados.getParametrosSistema(codigoUnidadeLogada, "utilizaPesoDesempenhoObjetivo");
        retorno = dsParamutilizaPesoDesempenhoObjetivo != null &&
             dsParamutilizaPesoDesempenhoObjetivo.Tables[0].Rows.Count > 0 &&
             dsParamutilizaPesoDesempenhoObjetivo.Tables[0].Rows[0]["utilizaPesoDesempenhoObjetivo"] + "" == "S";
        return retorno;
    }

    protected void carregaComboAssociaIndicadores()
    {
        DataSet ds = cDados.getIndicadoresNaoAssociados(codigoUnidadeSelecionada, codigoMapa, "");
        if (cDados.DataSetOk(ds))
        {
            ddlIndicadorAssociado.DataSource = ds;
            ddlIndicadorAssociado.TextField = "NomeIndicador";
            ddlIndicadorAssociado.ValueField = "CodigoIndicador";
            ddlIndicadorAssociado.DataBind();
        }
    }

    #region GRIDVIEW

    // GRID INDICADOR.

    private void carregaGridIndicador(string where)
    {
        int idObjetoPai = codigoMapa * (-1); // o código do mapa, quando pai de um indicador, tem que ser negativo na f_verificaAcesso...
        string select = string.Format(@" {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, i.[CodigoIndicador], NULL, 'IN', {4}, NULL, 'IN_CnsDtl') AS Permissao
                        ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoUnidadeLogada, idObjetoPai);
        dt = cDados.getIndicadoresOE(where, codigoObjetivoEstrategico, codigoUnidadeSelecionada, select).Tables[0];

        gridIndicadores.DataSource = dt;
        gridIndicadores.DataBind();
    }

    protected void gridIndicadores_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        //carregaGridIndicador("");
    }

    protected void gridIndicadores_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (true == e.DataColumn.Name.Equals("SR"))
        {
            ASPxGridView grid = (ASPxGridView)sender;

            string indicadorResultante = grid.GetRowValues(e.VisibleIndex, "IndicadorResultante").ToString();

            if (indicadorResultante == "S")
                e.Cell.Text = @"<img src=""../../imagens/IndicadorResultante.png"" style=""border-width:0px;"" />";
        }
        if (true == e.DataColumn.Name.Equals("PO"))
        {
            ASPxGridView grid = (ASPxGridView)sender;

            string indicadorResultante = grid.GetRowValues(e.VisibleIndex, "Polaridade").ToString();

            if (indicadorResultante != null && indicadorResultante != "")
                if (indicadorResultante != "POS")
                    e.Cell.Text = @"<img src=""../../imagens/botoes/PolaridadeN.png"" style=""border-width:0px;"" />";
                else
                    e.Cell.Text = @"<img src=""../../imagens/botoes/PolaridadeP.png"" style=""border-width:0px;"" />";
        }
    }


    protected void gridIndicadores_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {

        if (podeAssIn)
            e.Enabled = podeAssIn;
        else
        {
            e.Enabled = podeAssIn;
            e.Text = "Exluir";
            e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
        }
    }

    //GRID PROJETOS.

    private void carregaGridProjetos(string where)
    {
        int codigoEntidadeUnidade = cDados.getEntidadUnidadeNegocio(codigoUnidadeSelecionada);

        if (codigoUnidadeLogada == codigoEntidadeUnidade || indicaEntidade == false)
        {
            DataSet dsProjetos = cDados.getProjetosPlanosAcaoOE(codigoObjetivoEstrategico, codigoEntidadeUnidade, idUsuarioLogado, "");

            if (cDados.DataSetOk(dsProjetos))
            {
                gridProjetos.DataSource = dsProjetos;
                gridProjetos.DataBind();
            }
        }
    }

    protected void gridProjetos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cp_Erro"] = "";

        string mensagemErro = "";

        int codigoMeta = -1;
        int codigoProjeto = -1;



        if (int.TryParse(e.Parameters.ToString(), out codigoProjeto))
        {
            codigoProjeto = int.Parse(e.Parameters.ToString());
            if (ddlMeta.Value != null && ddlMeta.Value.ToString() != "-1" && ddlMeta.Value.ToString() != "0")
                codigoMeta = int.Parse(ddlMeta.Value.ToString());

            int[] arrayAcoes = new int[gvAcoes.GetSelectedFieldValues("CodigoAcaoSugerida").Count];

            for (int i = 0; i < arrayAcoes.Length; i++)
                arrayAcoes[i] = int.Parse(gvAcoes.GetSelectedFieldValues("CodigoAcaoSugerida")[i].ToString());

            string prioridade = ddlPrioridade.SelectedIndex != -1 ? ddlPrioridade.Value.ToString() : "NULL";

            bool retorno;

            if (getIndicaUtilizaPesoDesempenhoObjetivo() == true)
            {
                retorno = cDados.incluiAssociacaoProjetoObjetivoPeso(codigoProjeto, codigoObjetivoEstrategico, arrayAcoes, prioridade, codigoMeta, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", PesoObjetoLink2.Value), ref mensagemErro);
            }
            else
            {
                retorno = cDados.incluiAssociacaoProjetoObjetivo(codigoProjeto, codigoObjetivoEstrategico, arrayAcoes, prioridade, codigoMeta);
            }

            if (retorno == true)
            {
                ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados alterados com sucesso!";
            }
            else
            {
                ((ASPxGridView)sender).JSProperties["cp_Erro"] = "Erro ao Associar o Objetivo Estratégico ao Projeto";
            }
        }
        else if (e.Parameters.ToString() == "Atualizar")
        {
            if (((ASPxGridView)sender).FocusedRowIndex >= 0)
            {
                codigoProjeto = int.Parse(gridProjetos.GetRowValues(gridProjetos.FocusedRowIndex, gridProjetos.KeyFieldName).ToString());
                int[] arrayAcoes = new int[gvAcoes.GetSelectedFieldValues("CodigoAcaoSugerida").Count];
                string prioridade = ddlPrioridade.SelectedIndex != -1 ? ddlPrioridade.Value.ToString() : "NULL";
                if (ddlMeta.Value != null && ddlMeta.Value.ToString() != "0" && ddlMeta.Value.ToString() != "-1")
                    codigoMeta = int.Parse(ddlMeta.Value.ToString());

                for (int i = 0; i < arrayAcoes.Length; i++)
                    arrayAcoes[i] = int.Parse(gvAcoes.GetSelectedFieldValues("CodigoAcaoSugerida")[i].ToString());

                cDados.incluiAcoesSugeridasProjeto(codigoProjeto, codigoObjetivoEstrategico, arrayAcoes, ref mensagemErro);
                cDados.atualizaAssociacaoProjetoObjetivo(codigoProjeto, codigoObjetivoEstrategico, prioridade, codigoMeta, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", PesoObjetoLink2.Value), ref mensagemErro);
                if (mensagemErro == "")
                {
                    ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados atualizados com sucesso!";
                }
                else
                {
                    ((ASPxGridView)sender).JSProperties["cp_Erro"] = mensagemErro;
                }
            }
            else
            {
                ((ASPxGridView)sender).JSProperties["cp_Erro"] = Resources.traducao.detalhesObjetivoEstrategico_nenhum_projeto_foi_selecionado;
            }
        }
        else
        {
            string[] parametros = e.Parameters.Split('|');

            ((ASPxGridView)sender).JSProperties["cp_Erro"] = "";
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "";

            string[] parametro = e.Parameters.Split('|');
            var comando = parametro[0];
            var codigo = parametro[1];
            int registrosfetados = 0;
            mensagemErro = "";
       
            if(comando == "Excluir")
            {
                string tipo = gridProjetos.GetRowValuesByKeyValue(codigo, "Tipo").ToString();
                bool retorno = false;
                string msgRetorno = "";

                if (tipo == "projeto")
                    retorno = cDados.excluiAssociacaoProjetoObjetivo(codigo, codigoObjetivoEstrategico.ToString(), ref msgRetorno);
                else
                    retorno = cDados.excluiToDoList(codigo, idUsuarioLogado, ref msgRetorno);

                if (retorno)
                {
                    carregaGridProjetos("");
                    ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados excluídos com sucesso!";
                }
                else
                {
                    ((ASPxGridView)sender).JSProperties["cp_Erro"] = "Erro ao Excluir! " + msgRetorno;
                }
            }
        }
    }

    protected void gridProjetos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        
    }

    protected void gridProjetos_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGridProjetos("");
    }

    protected void gridProjetos_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string tipo = gridProjetos.GetRowValues(e.VisibleIndex, "Tipo") + "";

        bool podeEditarRegistro = false;

        if (tipo == "projeto")
            podeEditarRegistro = podeAssPr;
        else
            podeEditarRegistro = podeEditarPlanoAcao;

        if (podeEditarRegistro)
            e.Enabled = true;
        else
        {
            e.Enabled = false;
            e.Text = "Editar";
            e.Image.Url = "~/imagens/botoes/editarRegDes.png";
        }
    }

    protected void gridProjetos_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        string tipo = gridProjetos.GetRowValues(e.VisibleIndex, "Tipo") + "";

        bool podeEditarRegistro = false;

        if (tipo == "projeto")
            podeEditarRegistro = podeAssPr;
        else
            podeEditarRegistro = podeExcluirPlanoAcao;

        if (podeEditarRegistro)
            e.Enabled = true;
        else
        {
            e.Enabled = false;
            e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            e.Text = "Excluir";
        }
    }

    //GRID AÇÕES SUGERIDAS.

    private void carregaGridAcoesSugeridas(int codigoProjeto, bool marcarSelecionados)
    {
        DataSet dsAcoesSugeridas = cDados.getAcoesSugeridasObjetivoProjeto(codigoObjetivoEstrategico, codigoProjeto, "");

        if (cDados.DataSetOk(dsAcoesSugeridas))
        {
            gvAcoes.DataSource = dsAcoesSugeridas;
            gvAcoes.DataBind();

            if (marcarSelecionados)
            {
                gvAcoes.Selection.UnselectAll();

                int i = 0;
                foreach (DataRow dr in dsAcoesSugeridas.Tables[0].Rows)
                {
                    if (dr["Selecionado"].ToString() == "S")
                        gvAcoes.Selection.SelectRow(i);

                    i++;
                }
            }
        }
    }

    protected void gvAcoes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString() != "")
        {
            int codigoProjeto = int.Parse(e.Parameters.ToString());

            carregaGridAcoesSugeridas(codigoProjeto, true);
        }
    }

    #endregion

    #region VARIOS

    private void carregaCampos()
    {
        dt = cDados.getObjetivoEstrategico(null, codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtResponsavel.Text = dt.Rows[0]["NomeUsuario"].ToString();
            txtTema.Text = dt.Rows[0]["Tema"].ToString();
        }
        else
        {
            txtPerspectiva.Text = "";
            txtObjetivoEstrategico.Text = "";
            txtMapa.Text = "";
            txtResponsavel.Text = "";
            txtTema.Text = "";

        }

        DataSet dsParam = cDados.getParametrosSistema("labelAcoesSugeridasEstrategia", "mostraColunaResponsavelProjeto");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
        {
            DataRow dr = dsParam.Tables[0].Rows[0];
            gvAcoes.Columns["DescricaoAcao"].Caption = (dr["labelAcoesSugeridasEstrategia"].ToString());
            gridProjetos.Columns["Responsavel"].Visible = (dr["mostraColunaResponsavelProjeto"].Equals("S"));
        }
    }

    private void defineAlturaTela()
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = alturaPrincipal - 440;
        gvAcoes.Settings.VerticalScrollableHeight = 100;

        gridIndicadores.Settings.VerticalScrollableHeight = altura / 2 - 8;
        gridProjetos.Settings.VerticalScrollableHeight = altura / 2 - 8;
    }

    private void getPermissoesTela()
    {
        //Procurar permissão para visualizar Ações Sugeridas.
        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, idObjetoPai, "OB", "OB_Ass_IN", "OB_Ass_PR", "OB_CnsDtl", "OB_AltPlnAcn", "OB_ExcPlnAcn");
        if (cDados.DataSetOk(ds))
        {
            podeAssIn = int.Parse(ds.Tables[0].Rows[0]["OB_Ass_IN"].ToString()) > 0;
            podeAssPr = int.Parse(ds.Tables[0].Rows[0]["OB_Ass_PR"].ToString()) > 0;
            podeCnsDtl = int.Parse(ds.Tables[0].Rows[0]["OB_CnsDtl"].ToString()) > 0;
            podeEditarPlanoAcao = int.Parse(ds.Tables[0].Rows[0]["OB_AltPlnAcn"].ToString()) > 0;
            podeExcluirPlanoAcao = int.Parse(ds.Tables[0].Rows[0]["OB_ExcPlnAcn"].ToString()) > 0;
        }
    }

    #endregion

    #region COMBOBOX

    private void carregaComboProjetos()
    {

        DataSet dsProjetos = cDados.getProjetosDisponiveisObjetivo(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoObjetivoEstrategico, "");

        if (cDados.DataSetOk(dsProjetos))
        {
            ddlProjeto.DataSource = dsProjetos;
            ddlProjeto.TextField = "NomeProjeto";
            ddlProjeto.ValueField = "CodigoProjeto";
            ddlProjeto.DataBind();
        }
    }

    private void carregaComboMeta()
    {
        DataSet dsProjetos = cDados.getMapaDoObjetivo(codigoUnidadeSelecionada, codigoObjetivoEstrategico);

        if (cDados.DataSetOk(dsProjetos))
        {
            ddlMeta.DataSource = dsProjetos;
            ddlMeta.TextField = "Meta";
            ddlMeta.ValueField = "CodigoIndicador";
            ddlMeta.DataBind();
        }


        ListEditItem lei = new ListEditItem(Resources.traducao.nenhuma, "0");
        ddlMeta.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlMeta.SelectedIndex = 0;
    }

    #endregion

    #region CALLBACK

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        int codigoMeta = -1;

        
    }


    #endregion

    public string getLinkIndicador(int codigoIndicador, string nomeIndicador, bool permissao)
    {
        string linkRetorno = "";

        if (permissao)
        {
            linkRetorno = string.Format(@"<a target=""_top"" href='../indicador/index.aspx?NivelNavegacao=2&COIN={0}&{2}' style=""cursor: pointer"">
                                            {1}
                                          </a>", codigoIndicador
                                               , nomeIndicador
                                               , Request.QueryString.ToString());
        }
        else
        {
            linkRetorno = nomeIndicador;
        }

        return linkRetorno;
    }

    public string getLinkProjeto()
    {
        int codigoProjetoLink = int.Parse(Eval("Codigo").ToString());
        string nomeProjetoLink = Eval("Descricao").ToString();
        bool permissao = Eval("Permissao") + "" == "" ? false : (bool)(Eval("Permissao"));

        string linkRetorno = "";

        if (permissao)
        {
            linkRetorno = string.Format(@"<a target=""_top"" href='../../_Projetos/DadosProjeto/indexResumoProjeto.aspx?NivelNavegacao=2&IDProjeto={0}&NomeProjeto={1}' style=""cursor: pointer"">
                                            {1}
                                          </a>", codigoProjetoLink
                                               , nomeProjetoLink);
        }
        else
        {
            linkRetorno = nomeProjetoLink;
        }

        return linkRetorno;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "DetObjEstr");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeAssIn, "abrePopupAssociaIndicadorModoInclusao();", true, true, false, "DetObjEstr", "Detalhes Objetivo", this);
    }

    protected void menu_Init2(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        int codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeAssPr, @"", permissaoMapa && codigoUnidadeMapa == codigoUnidadeSelecionada, true, false, "DetObjEstr", "Detalhes Objetivo", this);

        (sender as ASPxMenu).Items.FindByName("btnGantt").ClientVisible = permissaoMapa && codigoUnidadeMapa == codigoUnidadeSelecionada;

        string urlGantt = Session["baseUrl"] + "/_Public/Gantt/paginas/planoAcao/Default.aspx?COE=" + codigoObjetivoEstrategico + "&IniciaisObjeto=OB";
        (sender as ASPxMenu).ClientSideEvents.ItemClick =
        @"function(s, e){ 
            if(e.item.name == 'btnIncluir')
            {
                e.processOnServer = false;
                gvAcoes.UnselectAllRowsOnPage(); 
                tipoEdicao = 'I';
                pcAssociarProjeto.Show();
                txtNomeProjeto.SetVisible(false);
                ddlProjeto.SetVisible(true);
                ddlProjeto.SetValue(null);
                ddlProjeto.SetText('');
                PesoObjetoLink2.SetValue(null);
            }		                     
	        else if(e.item.name == 'btnGantt')
            {
                e.processOnServer = false;
                abreGanttOE('" + urlGantt + @"');
            }	
            else
	        {
                e.processOnServer = true;		                                        
	        }	
        }";
    }

    #endregion

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
        if (ASPxGridViewExporter1.GridViewID == "gridIndicadores")
        {
            renderBrickGridIndicadores(sender, e);
        }
        if (ASPxGridViewExporter1.GridViewID == "gridProjetos")
        {
            renderBrickGridProjetos(sender, e);
        }

    }

    private void renderBrickGridIndicadores(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {

        if (e.Column.Name == "StatusDesempenho" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.Text = "l";
            e.TextValue = "l";

            if (e.Value.ToString().Contains("Vermelho"))
            {

                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Contains("Amarelo"))
            {

                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Contains("Verde"))
            {
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Contains("Azul"))
            {
                e.BrickStyle.ForeColor = Color.Blue;
            }
            else if (e.Value.ToString().Contains("Branco"))
            {

                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }

    private void renderBrickGridProjetos(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {

        if (e.Column.Name == "Status" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.Text = "l";
            e.TextValue = "l";
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            if (e.Value.ToString().Contains("Vermelho"))
            {

                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Contains("Amarelo"))
            {

                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Contains("Verde"))
            {
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Contains("Azul"))
            {
                e.BrickStyle.ForeColor = Color.Blue;
            }
            else if (e.Value.ToString().Contains("Branco"))
            {

                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }

    protected void gridIndicadores_Load(object sender, EventArgs e)
    {
        if (getIndicaUtilizaPesoDesempenhoObjetivo() == true)
        {
            (gridIndicadores.Columns["PesoObjetoLink"] as GridViewDataSpinEditColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
            (gridIndicadores.Columns["CodigoIndicador"] as GridViewDataComboBoxColumn).EditFormSettings.ColumnSpan = 1;
        }
        else
        {
            (gridIndicadores.Columns["PesoObjetoLink"] as GridViewDataSpinEditColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            (gridIndicadores.Columns["CodigoIndicador"] as GridViewDataComboBoxColumn).EditFormSettings.ColumnSpan = 2;
        }
    }

    protected void gridProjetos_Load(object sender, EventArgs e)
    {
        if(getIndicaUtilizaPesoDesempenhoObjetivo() == true)
        {
            tdPeso2.Visible = true;
            gridIndicadores.Columns["col_PesoObjetoLink"].Visible = true;
            gridProjetos.Columns["PesoObjetoLink"].Visible = true;
        }
        else
        {
            tdPeso2.Visible = false;
            gridIndicadores.Columns["col_PesoObjetoLink"].Visible = false;
            gridProjetos.Columns["PesoObjetoLink"].Visible = false;
        }
    }

    protected void gridIndicadores_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditarPeso")
        {
            if (getIndicaUtilizaPesoDesempenhoObjetivo() == true)
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
    }

    protected void callbackAssociaIndicador_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "";
        ((ASPxCallback)source).JSProperties["cp_Erro"] = "";
        if (e.Parameter == "I")
        {

            int registroAfectados = 0;
            bool retorno;

            if (getIndicaUtilizaPesoDesempenhoObjetivo() == true)
            {
                retorno = cDados.incluiIndicadorOEPeso(int.Parse(ddlIndicadorAssociado.Value.ToString())
                           , codigoObjetivoEstrategico
                           , string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", spnPeso.Value), ref registroAfectados);
                ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Dados incluídos com sucesso!";
            }
            else
            {
                try
                {
                    retorno = cDados.incluiIndicadorOE(int.Parse(ddlIndicadorAssociado.Value.ToString())
                           , codigoObjetivoEstrategico
                           , ref registroAfectados);
                    ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Dados incluídos com sucesso!";
                }
                catch (Exception ex)
                {
                    ((ASPxCallback)source).JSProperties["cp_Erro"] = ex.Message;
                }

            }
            carregaGridIndicador("");

        }
        else if (e.Parameter == "E")
        {


            string codigoIndicador = ddlIndicadorAssociado.Value.ToString();
            int regAfetados = 0;
            string comandoSQL = string.Format(@"
                BEGIN
                    DECLARE @CodigoObjeto as bigint
                    DECLARE @CodigoTipoObjeto as smallint
                    DECLARE @CodigoObjetoLink as bigint
                    DECLARE @CodigoTipoObjetoLink as smallint
                    DECLARE @CodigoTipoLink as tinyint
                    DECLARE @PesoObjetoLink as decimal(9,4)   

                    SET @CodigoObjeto         = {1}
                    SET @CodigoTipoObjeto     = dbo.f_GetCodigoTipoAssociacao('OB')
                    SET @CodigoObjetoLink     = {2}
                    SET @CodigoTipoObjetoLink = dbo.f_GetCodigoTipoAssociacao('IN')
                    SET @CodigoTipoLink       = (SELECT CodigoTipoLink  FROM TipoLinkObjeto WHERE IniciaisTipoLink = 'AS')
                    SET @PesoObjetoLink       = {0}

                    IF EXISTS (SELECT 1 FROM [LinkObjeto] 
                                       WHERE [CodigoObjeto]          = @CodigoObjeto
                                         AND [CodigoTipoObjeto]      = @CodigoTipoObjeto
		                                 AND [CodigoTipoObjetoLink]  = @CodigoTipoObjetoLink
                                         AND [CodigoObjetoLink]      = @CodigoObjetoLink 
		                                 AND [CodigoTipoLink]        = @CodigoTipoLink)
                       
                            BEGIN
                                    UPDATE [LinkObjeto]
                                    SET [PesoObjetoLink]        = @PesoObjetoLink
                                    WHERE [CodigoObjeto]          = @CodigoObjeto
                                    AND [CodigoTipoObjeto]      = @CodigoTipoObjeto
		                            AND [CodigoTipoObjetoLink]  = @CodigoTipoObjetoLink 
                                    AND [CodigoObjetoLink]      = @CodigoObjetoLink 
		                            AND [CodigoTipoLink]        = @CodigoTipoLink
                            END
                            ELSE
                            BEGIN
                                    INSERT INTO [dbo].[LinkObjeto]
                                       ([CodigoObjeto]
                                       ,[CodigoTipoObjeto]
                                       ,[CodigoObjetoLink]
                                       ,[CodigoTipoObjetoLink]
                                       ,[CodigoTipoLink]
                                       ,[PesoObjetoLink])
                                    VALUES
                                       (@CodigoObjeto
                                       ,@CodigoTipoObjeto
                                       ,@CodigoObjetoLink
                                       ,@CodigoTipoObjetoLink
                                       ,@CodigoTipoLink
                                       ,@PesoObjetoLink)
                              END
          END",
          string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", spnPeso.Value),
          codigoObjetivoEstrategico,
          codigoIndicador);
            try
            {
                bool retorno = cDados.execSQL(comandoSQL, ref regAfetados);
                if (retorno == true && regAfetados == 0)
                {
                    ((ASPxCallback)source).JSProperties["cp_Erro"] = "Nenhum registro foi alterado!";
                }
                else
                {
                    ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Dados alterados com sucesso!";
                }
            }
            catch (Exception ex)
            {
                ((ASPxCallback)source).JSProperties["cp_Erro"] = ex.Message;
            }
        }
    }

    protected void pn_ddlIndicadorAssociado_Callback(object sender, CallbackEventArgsBase e)
    {
        carregaComboAssociaIndicadores();
    }

    protected void pn_ddlProjeto_Callback(object sender, CallbackEventArgsBase e)
    {
        carregaComboProjetos();
    }

    protected void gridIndicadores_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cp_Erro"] = "";
        ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "";

        string[] parametro = e.Parameters.Split('|');
        var comando = parametro[0];
        var codigo = parametro[1];
        int registroAfetados = 0;
        string mensagemErro = "";
        string codigoErro = "";

        if(comando == "Excluir")
        {
            bool retorno = cDados.excluiIndicadoresOE(int.Parse(codigo)
                                                    , codigoObjetivoEstrategico
                                                    , ref registroAfetados
                                                    , ref codigoErro,
                                                    ref mensagemErro);
            if (!retorno || int.Parse(codigoErro) > 0)
            {
                ((ASPxGridView)sender).JSProperties["cp_Erro"] = "Nenhum indicador foi desvinculado do objetivo estratégico,\n para maiores informações contate o administrador do sistema.\n\nInforme esta mensagem:\n" + mensagemErro;
                
            }
            else
            {
                ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados excluídos com sucesso!";
            }
        }
    }
}