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
using System.Text;
using DevExpress.Web;
using System.Drawing;

/*
 12/10/2010: Mudança by Alejandro: 
            Foi implementado a permissão do usuario na entidade atual para edição da Metas do projeto.
            Permissão do usuario: 'ALTMETPRJ'.
            
 */
public partial class atualizacaoMetas : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoProjetoSelecionado = -1;
    private int codigoWorkflow = 0;
    private long codigoInstanciaWf = 0;


    private string resolucaoCliente = "";
    public bool puedeEditar = false;
    public bool podeIncluir = false;
    private int alturaPrincipal = 0;
    int qtdDias = 0;
    public string larguraTabela = "", alturaTabela = "";


    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string auxCP = "";
        int nivelAcessoEtapa = 0;

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

        HearderOnTela();

        if (Request.QueryString["CP"] != null)
        {
            auxCP = Request.QueryString["CP"].ToString();
        }
        
        if ( string.IsNullOrEmpty(auxCP) || (false == int.TryParse(auxCP, out codigoProjetoSelecionado)) )
        {
            if (Request.QueryString["ID"] != null)
            {
                auxCP = Request.QueryString["ID"].ToString();
                if (false == int.TryParse(auxCP, out codigoProjetoSelecionado))
                {
                    codigoProjetoSelecionado = -1;
                }
            }
        }

        if (Request.QueryString["CWF"] != null)
        {
            if( false == int.TryParse(Request.QueryString["CWF"].ToString(), out codigoWorkflow ) )
            {
                codigoWorkflow = 0;
            }
        }

        if (codigoWorkflow > 0)
        {

            if (Request.QueryString["CIWF"] != null)
            {
                if (false == long.TryParse(Request.QueryString["CIWF"].ToString(), out codigoInstanciaWf))
                {
                    codigoInstanciaWf = 0;
                }
            }
        }

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        gvDados.JSProperties["cp_CodigoProjeto"] = codigoProjetoSelecionado.ToString();

        if (!IsPostBack)
        {
            // se a tela não está num fluxo, verifica se o usuário tem acesso à tela
            if (0 == codigoWorkflow)
            {
                cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, codigoEntidade, codigoProjetoSelecionado, "null", "PR", 0, "null", "PR_DefMta");
            }
        }

        if (codigoWorkflow > 0) 
        {
            if ((Request.QueryString["RO"] != null) && (Request.QueryString["RO"] == "S")) 
            {
                podeIncluir = false;
                puedeEditar = false;
            }
            else
            {
                // se ainda não instanciou o fluxo
                if (codigoInstanciaWf == 0)
                {
                    int codigoEtapaInicial;
                    bool bRet = cDados.getCodigoEtapaInicialWorkflow(codigoWorkflow, out codigoEtapaInicial);

                    nivelAcessoEtapa = cDados.obtemNivelAcessoEtapaWfNaoInstanciada(codigoWorkflow, codigoProjetoSelecionado.ToString(), codigoEtapaInicial, idUsuarioLogado.ToString());
                }
                else
                {
                    nivelAcessoEtapa = cDados.obtemNivelAcessoInstanciaWf(codigoWorkflow, codigoInstanciaWf, idUsuarioLogado.ToString());
                }
                if (2 == (nivelAcessoEtapa & 2))
                {
                    podeIncluir = true;
                    puedeEditar = true;
                }
            }
        }
        else  // se a tela não está dentro de um fluxo, verifica a permissão no projeto
        {
            //Obtemdo a permissão do usuario na entidade logada.
            if (cDados.verificaAcessoStatusProjeto(codigoProjetoSelecionado) && cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoProjetoSelecionado, "null", "PR", 0, "null", "PR_DefMta"))
            {
                podeIncluir = true;
                puedeEditar = true;

                cDados.verificaPermissaoProjetoInativo(codigoProjetoSelecionado, ref podeIncluir, ref puedeEditar, ref puedeEditar);
            }
        }

        if (!IsCallback)
        {
            pnCallback.HideContentOnCallback = false;
        }

        carregaComboIndicadores();
        carregaComboPeriodicidade();

        ddlResponsavelResultado.TextField = "NomeUsuario";
        ddlResponsavelResultado.ValueField = "CodigoUsuario";
        
        ddlResponsavelResultado.Columns[0].FieldName = "NomeUsuario";
        ddlResponsavelResultado.Columns[1].FieldName = "EMail";
        ddlResponsavelResultado.TextFormatString = "{0}";
        
        populaGrid();

        gvDados.JSProperties["cp_dias"] = qtdDias.ToString();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        gvDados.JSProperties["cp_Msg"] = "";
        gvDados.JSProperties["cp_AtualizaCampos"] = "N";
    }

#region VARIOS

    private void HearderOnTela()
    {
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/MetasDesempenhoProjeto.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        this.TH(this.TS("MetasDesempenhoProjeto", "atualizacaoMetas"));
        cDados.aplicaEstiloVisual(Page);
    }

    private void MenuUsuarioLogado()
    {
        gvDados.Columns[0].Visible = false;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "ALTMETA"))
            gvDados.Columns[0].Visible = true;
    }

    #endregion

#region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }

    #endregion

#region GRID DADOS

    private void populaGrid()
    {
        DataSet ds = cDados.getIndicadoresProjeto(codigoProjetoSelecionado, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

            string periodicidade = "";

            //quando inclui o primeiro registro, ele deveria funcionar essa linha

            if (gvDados.VisibleRowCount == 1)
            {
                gvDados.FocusedRowIndex = 0;
            }
            
            if (gvDados.FocusedRowIndex >= 0)
                periodicidade = gvDados.GetRowValues(gvDados.FocusedRowIndex, "DescricaoPeriodicidade_PT").ToString();

            if (periodicidade == "Diária")
                qtdDias = 15;
            else if (periodicidade == "Semanal")
                qtdDias = 30;
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        bool resultado = false;

        string chave = getChavePrimaria();
        string meta = txtMeta.Text.Replace("'", "''");
        string valorMeta = txtValorMeta.Text.Replace(".", "").Replace(",", ".");
        string periodicidade = (ddlPeriodicidadeCalculo.SelectedIndex != -1) ? ddlPeriodicidadeCalculo.Value.ToString() : "-1";
        string fonte = txtFonte.Text.Replace("'", "''");
        string responsavelAtualizacao = ddlResponsavelResultado.Value != null ? ddlResponsavelResultado.Value.ToString() : "null";
        /*
        string inicio = (dataInicio.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dataInicio.Date);
        string termino = (dataTermino.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dataTermino.Date);
        */
        string dataInicioVigencia = (ddlInicioVigencia.Value != null) ? string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicioVigencia.Date) : "null";
        string dataTerminoVigencia = (ddlTerminoVigencia.Value != null) ? string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoVigencia.Date) : "null";
        string indicaVigencia = (cbVigencia.Value != null) ? cbVigencia.Value.ToString() : (dataInicioVigencia != "null" && dataTerminoVigencia != "null") ? "S" : "N";

        if (e.Parameters.ToString() == "I")
        {
            int codigoIndicador = (ddlIndicador.SelectedIndex != -1) ? int.Parse(ddlIndicador.Value.ToString()) : -1;
            int novoCodigoMeta = 0;

            resultado = cDados.incluiIndicadorProjeto(codigoIndicador, codigoProjetoSelecionado, meta, periodicidade, idUsuarioLogado, fonte, responsavelAtualizacao, dataInicioVigencia, dataTerminoVigencia, indicaVigencia, ref novoCodigoMeta);

            if (resultado)
            {
                gvDados.JSProperties["cp_Msg"] = Resources.traducao.atualizacaoMetas_meta_inclu_da_no_projeto_com_sucesso_;
                gvDados.JSProperties["cp_AtualizaCampos"] = "S";
                populaGrid();
                gvDados.ExpandAll();
                //quando inclui pela primeira vez essa linha não funciona

                if (gvDados.VisibleRowCount == 1)
                {
                    gvDados.FocusedRowIndex = 0;

                    //urlMetas = './editaMetas.aspx?CodigoMeta=' + codigoMeta + '&CodigoIndicador=' + codigoIndicador + '&CasasDecimais=' + casasDecimais + '&SiglaUnidadeMedida=' + unidadeMedida + '&NomeIndicador=' + indicador + "&Periodicidade=" + periodicidade + "&CodigoProjeto=" + codigoProjeto;
                    gvDados.JSProperties["cp_codigoMeta"] = novoCodigoMeta;
                    gvDados.JSProperties["cp_indicador"] = txtIndicador.Text;
                    gvDados.JSProperties["cp_polaridade"] = txtPolaridade.Text;
                    gvDados.JSProperties["cp_unidadeMedida"] = txtUnidadeMedida.Text;
                    gvDados.JSProperties["cp_periodicidade"] = periodicidade;
                    gvDados.JSProperties["cp_meta"] = meta;
                    gvDados.JSProperties["cp_valorMeta"] = txtValorMeta.Text;
                    gvDados.JSProperties["cp_codigoIndicador"] = codigoIndicador;
                    gvDados.JSProperties["cp_Fonte"] = txtFonte.Text;
                    gvDados.JSProperties["cp_CodigoResponsavelAtualizacao"] = responsavelAtualizacao;
                    gvDados.JSProperties["cp_NomeResponsavelAtualizacao"] = ddlResponsavelResultado.Text;

                }
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigoMeta);
            }
            else
                gvDados.JSProperties["cp_Msg"] = Resources.traducao.atualizacaoMetas_erro_ao_incluir_a_meta_no_projeto_;
        }
        else if (e.Parameters.ToString() == "E")
        {
            resultado = cDados.atualizaMetaPrenchida(chave, meta, periodicidade, idUsuarioLogado.ToString(), fonte, responsavelAtualizacao, dataInicioVigencia, dataTerminoVigencia, indicaVigencia);

            if (resultado)
            {
                gvDados.JSProperties["cp_Msg"] = Resources.traducao.atualizacaoMetas_informa__es_atualizadas_com_sucesso_;
                populaGrid();
            }
            else
                gvDados.JSProperties["cp_Msg"] = Resources.traducao.atualizacaoMetas_erro_ao_atualizar_informa__es_;
        }
        else if (e.Parameters.ToString() == "X")
        {
            resultado = cDados.excluiIndicadorProjeto(int.Parse(chave));

            if (resultado)
            {
                gvDados.JSProperties["cp_Msg"] = Resources.traducao.atualizacaoMetas_meta_exclu_da_do_projeto_com_sucesso_;
                gvDados.FocusedRowIndex = -1;
                populaGrid();
            }
            else
                gvDados.JSProperties["cp_Msg"] = Resources.traducao.atualizacaoMetas_erro_ao_excluir_a_meta_do_projeto_;
        }
            
    }

    /// <summary>
    /// Personalização do botão segundo o a permissão do usuario de poder o nao editar o registro.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (!puedeEditar)
        {
            if (e.ButtonID == "imgEditar")
            {
                //e.IsVisible = DevExpress.Utils.DefaultBoolean.False;
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/editarRegDes.png";
            }
            else if (e.ButtonID == "imgExcluir")
            {
                //e.IsVisible = DevExpress.Utils.DefaultBoolean.False;
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/excluirRegDes.png";
            }
        }
    }

#endregion
        
    private void carregaComboIndicadores()
    {
        DataSet ds = cDados.getIndicadoresOperacionaisDisponiveisProjeto(codigoProjetoSelecionado, codigoEntidade, "");

        if (cDados.DataSetOk(ds))
        {
            ddlIndicador.DataSource = ds;
            ddlIndicador.TextField = "NomeIndicador";
            ddlIndicador.ValueField = "CodigoIndicador";
            ddlIndicador.DataBind();
        }
    }

    private void carregaComboPeriodicidade()
    {
        DataSet ds = cDados.getPeriodicidade(" AND IntervaloMeses > 0");
        if (cDados.DataSetOk(ds))
        {
            ddlPeriodicidadeCalculo.DataSource = ds;
            ddlPeriodicidadeCalculo.TextField = "DescricaoPeriodicidade_PT";
            ddlPeriodicidadeCalculo.ValueField = "CodigoPeriodicidade";
            ddlPeriodicidadeCalculo.DataBind();
        }
    }

    protected void callbackIndicador_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter.ToString() != "" && ddlIndicador.SelectedIndex != -1)
        {
            callbackIndicador.JSProperties["cp_NomeIndicador"] = "";
            callbackIndicador.JSProperties["cp_Agrupamento"] = "";
            callbackIndicador.JSProperties["cp_Polaridade"] = "";
            callbackIndicador.JSProperties["cp_NomeUsuario"] = "";
            callbackIndicador.JSProperties["cp_SiglaUnidadeMedida"] = "";
            callbackIndicador.JSProperties["cp_Meta"] = "";
            callbackIndicador.JSProperties["cp_Fonte"] = "";

            int codigoIndicador = int.Parse(ddlIndicador.Value.ToString());

            DataSet ds = cDados.getDadosIndicadorOperacional(codigoIndicador, codigoEntidade, "");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow dr = ds.Tables[0].Rows[0];

                callbackIndicador.JSProperties["cp_NomeIndicador"] = dr["NomeIndicador"].ToString();
                callbackIndicador.JSProperties["cp_Agrupamento"] = dr["Agrupamento"].ToString();
                callbackIndicador.JSProperties["cp_Polaridade"] = dr["Polaridade"].ToString();
                callbackIndicador.JSProperties["cp_NomeUsuario"] = dr["NomeUsuario"].ToString();
                callbackIndicador.JSProperties["cp_SiglaUnidadeMedida"] = dr["SiglaUnidadeMedida"].ToString();

            }
        }
    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {

        ASPxComboBox comboBox = (ASPxComboBox)source;

        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidade);

            comboBox.Columns[0].FieldName = "NomeUsuario";
            comboBox.Columns[1].FieldName = "EMail";
            comboBox.TextFormatString = "{0}";

            comboBox.TextField = "NomeUsuario";
            comboBox.ValueField = "CodigoUsuario";

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
        else
        {
            comboBox.DataSource = SqlDataSource1;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.Columns[0].FieldName = "NomeUsuario";
        comboBox.Columns[1].FieldName = "EMail";
        comboBox.TextFormatString = "{0}";

        comboBox.TextField = "NomeUsuario";
        comboBox.ValueField = "CodigoUsuario";

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidade
                                                           , e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "DesMetPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "OnClick_CustomIncluirGvDado();", true, true, false, "DesMetPrj", "Metas de Projeto", this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    }
}
