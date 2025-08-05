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
using System.Collections.Generic;
using System.Collections.Specialized;

public partial class _Projetos_Administracao_IndicadoresOperacional : System.Web.UI.Page
{
    //checkin
    protected class ListaDeProjetos
    {
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;
        public ListaDeProjetos()
        {
            ListaDeCodigos = new List<int>();
            ListaDeNomes = new List<string>();
        }
        public void Clear()
        {
            ListaDeCodigos.Clear();
            ListaDeNomes.Clear();
        }

        /// <summary>
        /// Adiciona um item na lista de unidades
        /// </summary>
        /// <param name="codigoUnidade">Código da unidade a adicionar</param>
        /// <param name="descricaoUnidade">Descrição da unidade a adicionar</param>
        public void Add(int codigoProjeto, string descricaoProjeto)
        {
            ListaDeCodigos.Add(codigoProjeto);
            ListaDeNomes.Add(descricaoProjeto);
        }

        public string GetDescricaoProjeto(int codigoProjeto)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoProjeto);
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoProjeto)
        {
            return ListaDeCodigos.Contains(codigoProjeto);
        }

    }

    dados cDados;
    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;

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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "PR_CadInd");
        }

        if (!IsPostBack && !IsCallback)
            TabControl.ActiveTabIndex = 0;

        //A continuação vai a determinar si o usuario logado tem o não acesso ao varios permissos.
        podeIncluir = true;
        podeEditar = true;
        podeExcluir = true;

        this.Title = cDados.getNomeSistema();
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        cDados.aplicaEstiloVisual(Page);//Ok
        carregaGvDados();               //Ok
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        
        populaCombos();           

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region GRID's

    private void carregaCriterioIndicador()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #region GRID gvDADOS

    private void carregaGvDados()
    {
        string where = "";
        //int index;
        DataSet ds = cDados.getIndicadoresOperacional(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            DataRow row = gvDados.GetDataRow(e.VisibleIndex);
            bool permitirAlteracaoCampos = row == null ? 
                true : row["PermitirAlteracaoCampos"].Equals("S");
            if (podeExcluir && permitirAlteracaoCampos)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    #endregion

    #endregion

    #region COMBOBOX

    private void populaCombos()
    {
        DataSet ds = new DataSet();

        //combo Unidade Medida: [CodigoUnidadeMedida, SiglaUnidadeMedida]
        ds = cDados.getUnidadeMedida();
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            cDados.PopulaDropDownASPx(this, ds.Tables[0], "CodigoUnidadeMedida", "SiglaUnidadeMedida", "", ref ddlUnidadeMedida);

        //combo Responsavel pelo Indicador: [CodigoUsuario, NomeUsuario]
//        ddlResponsavelIndicador.Columns[0].FieldName = "NomeUsuario";
//        ddlResponsavelIndicador.Columns[1].FieldName = "EMail";


        ddlResponsavelIndicador.TextField = "NomeUsuario";
        ddlResponsavelIndicador.ValueField = "CodigoUsuario";


        ddlResponsavelIndicador.Columns[0].FieldName = "NomeUsuario";
        ddlResponsavelIndicador.Columns[1].FieldName = "EMail";
        ddlResponsavelIndicador.TextFormatString = "{0}";


        //combo Agrupamento da Meta
        ds = cDados.getAgrupamentoFuncao();
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            cDados.PopulaDropDownASPx(this, ds.Tables[0], "CodigoFuncao", "NomeFuncao", "", ref cmbAgrupamentoDaMeta);
    }

    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        int altura = (alturaPrincipal - 270);
        if (altura > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 10;
            heGlossario.Height = new Unit((altura - 250) + "px");
        }
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/IndicadoresOperacional.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "IndicadoresOperacional", "ASPxListbox"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoIndicador").ToString();
        return codigoDado;
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        //defineTituloGridDados(false);

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        if (e.Parameter == "Compartilhar")
        {
            throw new Exception("Funcionaliadade removida.");
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }


    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro
    {
        int novoCodigoIndicador = 0;
        int codigoIndicador = 0;
        string msg = "";
        //string formulaIndicador = "";
        //if (hfGeral.Contains("FormulaIndicador"))
        //    formulaIndicador = hfGeral.Get("FormulaIndicador").ToString();

        //Obtem Dados...
        string nomeIndicador = txtIndicador.Text.Replace("'", "''");
        //if (ddlResponsavelIndicador.SelectedIndex == -1)
        //{
        //    return Resources.traducao.IndicadoresOperacional_o_responsavel_pelo_indicador_deve_ser_informado;
        //}
        string responsavelIndicador = ddlResponsavelIndicador.Value.ToString();
        string unidadeMedida = ddlUnidadeMedida.Value.ToString();
        string polaridade = ddlPolaridade.Value.ToString();
        string glossario = heGlossario.Html.Replace("'", "''");
        string cassasDecimais = ddlCasasDecimais.Value.ToString();
        string agrupamentoMeta = cmbAgrupamentoDaMeta.Value != null ? cmbAgrupamentoDaMeta.Value.ToString() : "NULL";
        string criterio = rblCriterio.Value.ToString();
        string tipoIndicador = rblTipoIndicador.Value.ToString();
        //ref novoCodigoIndicador

        try
        {
            codigoIndicador = -1;
            verificaExistenciaNomeIndicador(codigoIndicador, nomeIndicador);

            if (cDados.incluiIndicadorOperacional(nomeIndicador, responsavelIndicador,  unidadeMedida,
                                                  polaridade, codigoEntidadeUsuarioResponsavel.ToString(),
                                                  codigoUsuarioResponsavel.ToString(), glossario, cassasDecimais,
                                                  agrupamentoMeta, criterio, tipoIndicador, "NULL", "NULL", "", "S", "N", ref novoCodigoIndicador))
            {
                hfGeral.Set("hfCodigoIndicador", novoCodigoIndicador);
                carregaGvDados();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigoIndicador);
                gvDados.ClientVisible = false;

                msg = "";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        //  oDadosFormulario.Add("DescricaoRiscoPadrao", txtRisco.Text);
        oDadosFormulario.Add("CodigoUsuarioInclusao", codigoUsuarioResponsavel);
        oDadosFormulario.Add("CodigoEntidade", 1);
        oDadosFormulario.Add("DataInclusao", DateTime.Now.Date.ToString());
        return oDadosFormulario;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        // busca a chave primaria
        int codigoIndicador = 0;
        //string formulaIndicador = hfGeral.Get("FormulaIndicador").ToString();
        string chave = getChavePrimaria();
        //Obtem Dados...
        string nomeIndicador = txtIndicador.Text.Replace("'", "''");
        string responsavelIndicador = ddlResponsavelIndicador.Value.ToString();
        string unidadeMedida = ddlUnidadeMedida.Value.ToString();
        string polaridade = ddlPolaridade.Value.ToString();
        string glossario = heGlossario.Html.Replace("'", "''");
        string cassasDecimais = ddlCasasDecimais.Value.ToString();
        string agrupamentoMeta = cmbAgrupamentoDaMeta.Value != null ? cmbAgrupamentoDaMeta.Value.ToString() : "NULL";
        string criterio = rblCriterio.Value.ToString();
        string tipoIndicador = rblTipoIndicador.Value.ToString();
        //ref novoCodigoIndicador
        string msg = "";

        try
        {
            codigoIndicador = int.Parse(getChavePrimaria());
            verificaExistenciaNomeIndicador(codigoIndicador, nomeIndicador);

            cDados.atualizaIndicadorOperacional(chave, nomeIndicador, responsavelIndicador,  unidadeMedida,
                                                polaridade, codigoUsuarioResponsavel.ToString(),
                                                cassasDecimais, glossario, agrupamentoMeta, criterio, tipoIndicador,"NULL", "NULL", "", "S", "N");

            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
            gvDados.ClientVisible = false;

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        // busca a chave primaria
        string chave = getChavePrimaria();

        cDados.excluiIndicadorOperacional(chave, codigoUsuarioResponsavel.ToString());
        // carregaGvDados();
        return "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nomeIndicador"></param>
    /// <returns></returns>
    private int verificaExistenciaNomeIndicador(int codigoIndicador, string nomeIndicador)
    {
        int codigoIndicadorBanco = 0;

        if (codigoIndicador == 0)
            throw new Exception(Resources.traducao.IndicadoresOperacional_erro_ao_gravar_os_dados__falha_interna_aplica__o___c_digo_4_);

        DataSet ds = cDados.getIndicadoresOperacional(codigoEntidadeUsuarioResponsavel, " AND NomeIndicador = '" + nomeIndicador + "'");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            int.TryParse(ds.Tables[0].Rows[0]["CodigoIndicador"].ToString(), out codigoIndicadorBanco);

        if ((codigoIndicadorBanco != 0) && (codigoIndicadorBanco != codigoIndicador))
            throw new Exception(Resources.traducao.IndicadoresOperacional_n_o_ser__poss_vel_gravar_o_registro_usando_este_nome_para_o_indicador__j__existe_um_indicador_cadastrado_com_este_nome_);

        return codigoIndicadorBanco;
    }
    #endregion

    #region Comentario
    /*
   1) PainelCallback Anidados:
    Se descubrio que o uso de panelCallback anidados, e nos diferentes objetos que sejam utilizado pelo 
    panelCallback mais interno, tendra que estar dentro do mesmo, já que dos panelCallback externos podem
    chegar ao objeto interno, mas não ao reves.Nesta tela se utilza duas panelCallback, o endCallback do 
    painel mas interno, não pôde aceder a objetos que se encontrabam fora do mesmo (ainda sendo javascript,
    sendo a nível cliente), é por isso que se colocaram os diferentes objetos que precisava o panelCallback
    interno dentro dele.
*/
    #endregion
    protected void ddlResponsavelIndicador_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavelIndicador_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    protected void gvDados_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        gvDados.ExpandAll();
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "IndOper");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abreNovoIndicador()", true, true, false, "IndOper", "Indicadores de Projetos", this);
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
