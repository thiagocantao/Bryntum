/*
 OBSERVAÇÕES
 * 
 * MUDANÇA
 * 07/02/2011 - Alejandro: 
                a.- Implementar Compartilhamento dos indicadores.
                    #region SESSION COMPARTILHAR...
 * 
 * 18/03/2011 :: Alejandro : Adicionar o control do acesso a tela.
 * 21/03/2011 :: Alejandro : Adicionar o control do botão de permissao [IN_AdmPrs].
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
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class _Estrategias_wizard_indicadores : System.Web.UI.Page
{
    /// <summary>
    /// classe para guardar uma lista de unidades com seus códigos e descrições.
    /// </summary>
    /// <remarks>
    /// Usada nas funções de verificações antes da gravação dos dados;
    /// </remarks>
    protected class ListaDeUnidades
    {
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;
        public ListaDeUnidades()
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
        public void Add(int codigoUnidade, string descricaoUnidade)
        {
            ListaDeCodigos.Add(codigoUnidade);
            ListaDeNomes.Add(descricaoUnidade);
        }

        public string GetDescricaoUnidade(int codigoUnidade)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoUnidade);
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoUnidade)
        {
            return ListaDeCodigos.Contains(codigoUnidade);
        }
    }

    dados cDados;

    private string dbName;
    private string dbOwner;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    private int codigoUsuarioResponsavel = 0;
    private int codigoEntidadeUsuarioResponsavel = 0;

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;
    public bool podeCompartilhar = false;
    public bool podeIncluirComponente = false; 
    public bool podeTrocarResponsavel = false;
    public string definicaoEntidadePluralDisp = "";
    public string definicaoEntidadePluralSel = "";
    string podeAgruparPorMapa = "N";
    
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
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }        

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        if (!IsPostBack)
        {
            bool bPodeAcessarTela;
            bPodeAcessarTela = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadInd");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_AdmPrs");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_AltRsp");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_Compart");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_Alt");

            // se não puder, redireciona para a página sem acesso
            if (bPodeAcessarTela == false)
                cDados.RedirecionaParaTelaSemAcesso(this);
        }

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        dsFuncao.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource2.ConnectionString = cDados.classeDados.getStringConexao();
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        if (!hfGeral.Contains("TipoOperacao"))
            hfGeral.Set("TipoOperacao", "Consultar");
        
        defineTituloGridDados(hfGeral.Get("TipoOperacao").ToString() != "Consultar");       
        
        if (!IsPostBack && !IsCallback)
            TabControl.ActiveTabIndex = 0;
        
        //todo: [incluir indicador] acesso a incluir novo indicador.
        //if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "INCINDIC"))
        //    podeIncluir = true;

        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadInd"))
            podeIncluir = true;

        definicaoEntidadePluralDisp = "Entidades disponíveis";
        definicaoEntidadePluralSel = "Entidades selecionadas";
       
        DataSet ds = cDados.getDefinicaoEntidade(codigoEntidadeUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoEntidadePluralDisp = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString() + " disponíveis";
            definicaoEntidadePluralSel = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString() + " selecionadas";
            
        }
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "utilizaColunaMapaTelaIndicadores");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            podeAgruparPorMapa = dsParam.Tables[0].Rows[0]["utilizaColunaMapaTelaIndicadores"].ToString();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        carregaGvDados(); 
        carregaGridDadosIndicador();    //Ok  
        //carregaFaixaTolerancia();
        populaCombos();
        cDados.aplicaEstiloVisual(Page);//Ok
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowPager;
        gvDados.SettingsPager.PageSize = 10;
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        gridDadosIndicador.Settings.ShowFilterRow = false;
        gridDadosIndicador.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;

        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblUnidade.Text = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString() + ":";
            gvDados.Columns["NomeUnidadeNegocio"].Caption = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

        ddlUnidadeNegocio.JSProperties["cp_NomeUnidade"] = gvDados.Columns["NomeUnidadeNegocio"].Caption;       
    }

    #region GRIDVIEW'S

    #region gvDados

    private void carregaGvDados() //Carga a grid Lista.
    {
        string where = ""; // "AND iu.IndicaUnidadeCriadoraIndicador = 'S'";        

        if (podeAgruparPorMapa == "N")
        {
            gvDados.Columns["MapaEstrategico"].Visible = false;
            ((GridViewDataTextColumn)gvDados.Columns["MapaEstrategico"]).GroupIndex = -1;
        }

        DataSet ds = cDados.getIndicadores(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, podeAgruparPorMapa, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void pegaDadosParaGrid(string codigoIndicador)
    {
        DataSet ds = cDados.getDadosGrid(codigoIndicador);
        if (cDados.DataSetOk(ds))
        {
            gridDadosIndicador.DataSource = ds;
            gridDadosIndicador.DataBind();
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        

        if (e.RowType == GridViewRowType.Data)
        {
            string unidadeAtivo = e.GetValue("IndicaUnidadeCriadoraIndicador").ToString();

            if (unidadeAtivo != "S")
            {
                e.Row.BackColor = Color.FromName("#DDFFCC");
                e.Row.ForeColor = Color.Black;
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            string unidadeCriadora = "";
            unidadeCriadora = gvDados.GetRowValues(e.VisibleIndex, "IndicaUnidadeCriadoraIndicador").ToString();
            int permissoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
            string podeCompartilharIndi = gvDados.GetRowValues(e.VisibleIndex, "PodeCompartilhar").ToString();

            podeEditar = (permissoes & 2) > 0;
            podePermissao = (permissoes & 4) > 0;
            podeCompartilhar = (permissoes & 8) > 0 && podeCompartilharIndi == "S";
            podeTrocarResponsavel = (permissoes & 16) > 0;

            podeExcluir = podeEditar;

            if (e.ButtonID.Equals("btnEditar"))
            {
                if (unidadeCriadora.Equals("S") && podeEditar)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Text = "Editar";
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnExcluir"))
            {
                if (unidadeCriadora.Equals("S") && podeExcluir)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Text = "Excluir";
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnResponsavel"))
            {
                if (podeTrocarResponsavel)
                    e.Enabled = true;
                else
                {
                    //e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    e.Enabled = false;
                    e.Text = "Editar Responsável";
                    e.Image.Url = "~/imagens/TrocaRespDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnPermissoesCustom"))
            {
                if (podePermissao)
                    e.Enabled = true;
                else
                {
                    e.Text = "Alterar Permissões";
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnCompartilhar"))
            {
                if (podeCompartilhar)
                    e.Enabled = true;
                else
                {
                    //e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    e.Enabled = false;
                    e.Text = "Compartilhar";
                    e.Image.Url = "~/imagens/compartilharDes.png";
                }
            }
        }
    }

    #endregion

    #region GridDadosIndicador

    private void carregaGridDadosIndicador()
    {
        string codigoIndicador = getChavePrimaria();
        pegaDadosParaGrid(codigoIndicador);
    }

    private void defineTituloGridDados(bool mostrarBotao)
    {
        podeIncluirComponente                 = mostrarBotao && (hfGeral.Get("TipoOperacao").ToString() != "Consultar");
        gridDadosIndicador.Columns[0].Visible = podeIncluirComponente;
    }

    private void limpaGridDados()
    {
        gridDadosIndicador.DataSource = null;
        gridDadosIndicador.DataBind();
    }

    protected void gridDadosIndicador_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        bool definirTitulo = false;

        if (e.Args.Length > 0)
        {
            string parametro = e.Args[0].ToString();



            if (parametro.Equals("Incluir"))
            {
                limpaGridDados();
                TabControl.ActiveTabIndex = 0;
            }
            else if (parametro.Equals("Editar") || parametro.Equals("Excluir"))
                definirTitulo = true;


            try
            {
                int paramtroKey = int.Parse(parametro);
                definirTitulo = true;
            }
            catch
            {
            }

           defineTituloGridDados(definirTitulo);
        }
    }

    protected void gridDadosIndicador_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        string gruposEnLaLista = ""; // by Alejandro 17/07/2010.
        string codigoIndicador = getChavePrimaria();
        pegaDadosParaGrid(codigoIndicador);

        //si nao ta no modo edicao e nao ta null o conteudo do banco de dados.
        if ((!gridDadosIndicador.IsEditing) || (e.KeyValue == DBNull.Value))
            return; //retorno

        //vejo si nao ta disenhando campo NomeResponsavel, aonde fica o comboBox
        if (e.Column.FieldName == "CodigoDado")
        {
            //-----------------------------------------------------------------by Alejandro 17/07/2010.
            string where = ""; 

            string whereIndicador = "";

            for (int i = 0; i < gridDadosIndicador.VisibleRowCount; i++)
            { //Listar los CodigoPerfilWf que se encuentran en la Grid. De esta forma se evitará su 
                //listado en el combo salvando de una selección repetitiva.
                                
                if(e.KeyValue == null)
                    gruposEnLaLista += "," + gridDadosIndicador.GetRowValues(i, "CodigoDado").ToString();
                else if ( e.KeyValue.ToString() != gridDadosIndicador.GetRowValues(i, "CodigoDado").ToString())
                {
                    gruposEnLaLista += "," + gridDadosIndicador.GetRowValues(i, "CodigoDado").ToString();
                }
            }
            if (gruposEnLaLista != "")
            {
                where += " AND d.CodigoDado NOT IN(" + gruposEnLaLista.Substring(1) + ")";
                if (e.KeyValue != null)
                    whereIndicador = string.Format(@" OR i.CodigoIndicador = " + (int.Parse(e.KeyValue.ToString()) * -1));
            }
            DataTable dtForms = cDados.getDadosIndicadorEstrategiaGrid(codigoEntidadeUsuarioResponsavel, int.Parse(codigoIndicador), where, whereIndicador).Tables[0];
            //-----------------------------------------------------------------

            //si e asim, cargo o cambo...
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            combo.Callback += new CallbackEventHandlerBase(cmbDado_OnCallback);
            combo.DataSource = dtForms; // by Alejandro 17/07/2010.
            combo.DataBind();

            bool novaLinha = (sender as ASPxGridView).IsNewRowEditing;
            // se NÃO for uma nova linha, desabilite o controle
            if (!novaLinha)
            {
                e.Editor.ClientEnabled = false;
                return;
            }

            //carregaPopUpResponsaveis(combo);
        }
    }

    protected void gridDadosIndicador_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int CodigoIndicador = int.Parse(getChavePrimaria());
        int CodigoComponente = int.Parse(hfGeral.Get("NovoCodigoDado").ToString());
        int CodigoFuncaoIndicador = hfGeral.Get("NovoCodigoFuncao") != null && hfGeral.Get("NovoCodigoFuncao").ToString() != "" ? int.Parse(hfGeral.Get("NovoCodigoFuncao").ToString()) : 0;

        cDados.incluiComponenteIndicador(CodigoIndicador, CodigoComponente, CodigoFuncaoIndicador);
        
        e.Cancel = true;
        gridDadosIndicador.CancelEdit();
        carregaGridDadosIndicador();
    }

    protected void gridDadosIndicador_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int CodigoIndicador = int.Parse(getChavePrimaria());

        int CodigoComponente = int.Parse(e.Keys[0].ToString());

        int CodigoFuncaoIndicador = hfGeral.Get("NovoCodigoFuncao") != null && hfGeral.Get("NovoCodigoFuncao").ToString() != "" ? int.Parse(hfGeral.Get("NovoCodigoFuncao").ToString()) : 0;

        cDados.atualizaComponenteIndicador(CodigoIndicador, CodigoComponente, CodigoFuncaoIndicador);

        e.Cancel = true;
        gridDadosIndicador.CancelEdit();
        carregaGridDadosIndicador();
    }

    protected void gridDadosIndicador_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        gridDadosIndicador.JSProperties["cp_DadoExcluido"] = "";

        int CodigoIndicador = int.Parse(getChavePrimaria());
        int CodigoComponente = int.Parse(e.Keys[0].ToString());

        cDados.excluiComponenteIndicador(CodigoIndicador, CodigoComponente);
        gridDadosIndicador.JSProperties["cp_DadoExcluido"] = "S";

        e.Cancel = true;
        //gridDadosIndicador.CancelEdit();
        carregaGridDadosIndicador();
    }

    protected void gridDadosIndicador_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        defineTituloGridDados(true);
    }

    protected void gridDadosIndicador_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        bool definirTitulo = false;
        string parametro = e.Parameters.ToString();

        if (parametro.Length > 0)
        {
            if ("Incluir" == parametro)
            {
                limpaGridDados();
                TabControl.ActiveTabIndex = 0;
            }
            if (parametro.Equals("Editar") || parametro.Equals("Excluir"))
                definirTitulo = true;
        }
        
        defineTituloGridDados(definirTitulo);
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
        {
            ddlUnidadeMedida.DataSource = ds;
            ddlUnidadeMedida.TextField = "SiglaUnidadeMedida";
            ddlUnidadeMedida.ValueField = "CodigoUnidadeMedida";
            ddlUnidadeMedida.DataBind();

            if (!IsPostBack)
                ddlUnidadeMedida.SelectedIndex = 0;
        }

        //combo Periodicidade: [CodigoPeriodicidade, DescricaoPeriodicidade_PT]
        ds = cDados.getPeriodicidade("AND IntervaloMeses >= 1");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlPeriodicidade.DataSource = ds;
            ddlPeriodicidade.TextField = "DescricaoPeriodicidade_PT";
            ddlPeriodicidade.ValueField = "CodigoPeriodicidade";
            ddlPeriodicidade.DataBind();

            if (!IsPostBack)
                ddlPeriodicidade.SelectedIndex = 0;
        }
            
        
            ddlResponsavelIndicador.TextField = "NomeUsuario";
            ddlResponsavelIndicador.ValueField = "CodigoUsuario";
            

            ddlResponsavelIndicador.Columns[0].FieldName = "NomeUsuario";
            ddlResponsavelIndicador.Columns[1].FieldName = "EMail";
            ddlResponsavelIndicador.TextFormatString = "{0}";

            ddlResponsavelResultado.TextField = "NomeUsuario";
            ddlResponsavelResultado.ValueField = "CodigoUsuario";


            ddlResponsavelResultado.Columns[0].FieldName = "NomeUsuario";
            ddlResponsavelResultado.Columns[1].FieldName = "EMail";
            ddlResponsavelResultado.TextFormatString = "{0}";
        
        
        
        //combo Agrupamento da Meta
        ds = cDados.getAgrupamentoFuncao();

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            cmbAgrupamentoDaMeta.DataSource = ds;
            cmbAgrupamentoDaMeta.TextField = "NomeFuncao";
            cmbAgrupamentoDaMeta.ValueField = "CodigoFuncao";
            cmbAgrupamentoDaMeta.DataBind();

            if (!IsPostBack)
                cmbAgrupamentoDaMeta.SelectedIndex = 0;
        }

        
            ddlResponsavel.TextField = "NomeUsuario";
            ddlResponsavel.ValueField = "CodigoUsuario";

            ddlResponsavelResultados2.TextField = "NomeUsuario";
            ddlResponsavelResultados2.ValueField = "CodigoUsuario";

            //Unidades de negocios
            string where = string.Format(@" AND DataExclusao IS NULL AND IndicaUnidadeNegocioAtiva = 'S' AND CodigoEntidade = {0}", codigoEntidadeUsuarioResponsavel);

           where += string.Format(@" AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, CodigoUnidadeNegocio, NULL, 'UN', 0, NULL, 'UN_IncInd') = 1 
                        ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);
           
            ds = cDados.getUnidade(where);
            if (cDados.DataSetOk(ds))
            {
                ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
                ddlUnidadeNegocio.TextField = "NomeUnidadeNegocio";

                ddlUnidadeNegocio.DataSource = ds.Tables[0];
                ddlUnidadeNegocio.DataBind();

                //if (!IsPostBack)
                //    ddlUnidadeNegocio.SelectedIndex = 0;
            }
    }

    private void cmbDado_OnCallback(object source, CallbackEventArgsBase e) //Inicia alguma atividade no combo.
    {
        ASPxComboBox combo = source as ASPxComboBox; //pego o combo.
        insereNovoDado(); //chamo a o metodo que faz...
        combo.DataBind();
    }

    #endregion

    #region varios

    private void insereNovoDado()
    {
        //metodo que insere um novo registro do usuario. de um jeito rapido.
        //ele so precisa do nome, mail, codigoUnidade, senha  e  ID do usuario que ta cadastrando.
        cDados.incluiDadoGrid(txtNome.Text, codigoUsuarioResponsavel.ToString(), codigoEntidadeUsuarioResponsavel.ToString());
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 195);
        
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 125;
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/indicadores.js""></script>"));
        this.TH(this.TS("barraNavegacao", "indicadores"));
    }

    #endregion

    #region CALLBACK'S

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
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

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            pcMensagemGravacao.Modal = false;
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
        {
            pcMensagemGravacao.Modal = true;
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }

    protected void pnCallbackResponsavel_Callback(object sender, CallbackEventArgsBase e)
    {
        pnCallbackResponsavel.JSProperties["cp_sucesso"] = "";
        pnCallbackResponsavel.JSProperties["cp_erro"] = "";

        string opcao = e.Parameter;

        if (opcao == "salvar" && ddlResponsavel.SelectedIndex != -1 && ddlResponsavelResultados2.SelectedIndex != -1)
        {
            string codigoIndicador = getChavePrimaria();
            string codigoUnidadeNegocio = "";

            if (gvDados.GetSelectedFieldValues("CodigoUnidadeNegocio").Count > 0)
                codigoUnidadeNegocio = gvDados.GetSelectedFieldValues("CodigoUnidadeNegocio")[0].ToString();
            else
                codigoUnidadeNegocio = "-1";

            string comandoSQL = string.Format(@"

                IF EXISTS (SELECT 1 FROM {0}.{1}.IndicadorUnidade iu 
							          WHERE iu.CodigoIndicador = {3} 
								        AND iu.CodigoUnidadeNegocio = {4}
                                        AND iu.IndicaUnidadeCriadoraIndicador = 'S')
              BEGIN
                    UPDATE {0}.{1}.Indicador 
                       SET CodigoUsuarioResponsavel = {2}
                     WHERE CodigoIndicador = {3}
              END
                UPDATE {0}.{1}.IndicadorUnidade
                   SET CodigoResponsavelIndicadorUnidade = {2}
                     , CodigoReservado = '{5}'
                     , CodigoResponsavelAtualizacaoIndicador = {6}
                 WHERE CodigoIndicador      = {3}
                   AND CodigoUnidadeNegocio = {4}

            ", cDados.getDbName(), cDados.getDbOwner(), ddlResponsavel.Value.ToString()
             , codigoIndicador, codigoUnidadeNegocio, txtCodigoReservadoNovoResp.Text, ddlResponsavelResultados2.Value.ToString());

            string mensagem = "";
            DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + Environment.NewLine + comandoSQL + Environment.NewLine + cDados.geraBlocoEndTran());
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                mensagem = ds.Tables[0].Rows[0][0].ToString();
            }
            if (mensagem.ToUpper().Trim() == "OK")
            {
                pnCallbackResponsavel.JSProperties["cp_sucesso"] = "Dados alterados com sucesso!";
            }
            else
            {
                pnCallbackResponsavel.JSProperties["cp_erro"] = mensagem;
            }
        }
    }

    protected void pnCallbackFaixaTolerancia_Callback(object sender, CallbackEventArgsBase e)
    {
        string opcao = e.Parameter;
        pnCallbackFaixaTolerancia.JSProperties["cp_FallaEditada"] = "";

        if (opcao.Equals("Salvar"))
        {
            string chave = getChavePrimaria();
            gravaFaixaTolerancia(int.Parse(chave));
            pnCallbackFaixaTolerancia.JSProperties["cp_FallaEditada"] = "OK";
        }
        else
            carregaFaixaTolerancia();
    }

    #endregion

    #region BANCO DE DADOS.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    // INDICADOR.

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

    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro
    {
        int novoCodigoIndicador = 0;
        string msg = "";
        string formulaIndicador = "";
        if (hfGeral.Contains("FormulaIndicador"))
            formulaIndicador = hfGeral.Get("FormulaIndicador").ToString();

        string dataInicio = (ddlInicioVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlInicioVigencia.Date));
        string dataTermino = (ddlTerminoVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlTerminoVigencia.Date));
        string indicaAcompanhamentoMetaVigencia = cbVigencia.Checked ? "S" : "N";

        try
        {
            string indicaCriterio = (rbCriterio.Value == null) ? "NULL" : rbCriterio.Value.ToString();

            if (cDados.incluiIndicador(txtIndicador.Text.Replace("'", "''"), 
                                       ddlResponsavelIndicador.Value.ToString(),
                                       txtFonte.Text.Replace("'", "''"), 
                                       ddlUnidadeMedida.Value.ToString(), ddlPeriodicidade.Value.ToString(), 
                                       ddlPolaridade.Value.ToString(),
                                       formulaIndicador, 
                                       ddlUnidadeNegocio.Value.ToString(), codigoUsuarioResponsavel.ToString(),
                                       heGlossario.Html.Replace("'", "''"), 
                                       ddlCasasDecimais.Value.ToString(),
                                       (cbCheckResultante.Checked ? "S" : "N"),
                                       cmbAgrupamentoDaMeta.Value.ToString(),
                                       indicaCriterio, int.Parse(txtLimite.Text),txtCodigoReservado.Text, ddlResponsavelResultado.Value.ToString(),
                                       dataInicio, dataTermino, indicaAcompanhamentoMetaVigencia,
                                       ref novoCodigoIndicador))
            {
                hfGeral.Set("hfCodigoIndicador", novoCodigoIndicador);
                carregaGvDados();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigoIndicador);
                gvDados.ClientVisible = false;

                //Seteo a Aba DADOS.
                lblCaptionIndicador.Text = txtIndicador.Text;
                hfGeral.Set("modoGridDados", "Editar");
                limpaGridDados();
                defineTituloGridDados(true);
                msg = "";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        // busca a chave primaria
        string formulaIndicador = hfGeral.Get("FormulaIndicador").ToString();
        string chave = getChavePrimaria();

        string indicaCriterio = (rbCriterio.Value == null) ? "NULL" : rbCriterio.Value.ToString();
        string dataInicio = (ddlInicioVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlInicioVigencia.Date));
        string dataTermino = (ddlTerminoVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlTerminoVigencia.Date));
        string indicaAcompanhamentoMetaVigencia = cbVigencia.Checked ? "S" : "N";
        string msgErro = "";

        bool retorno = cDados.atualizaIndicador(chave, txtIndicador.Text.Replace("'", "''"),
                                 ddlResponsavelIndicador.Value.ToString(),
                                 txtFonte.Text.Replace("'", "''"),
                                 ddlUnidadeMedida.Value.ToString(), ddlPeriodicidade.Value.ToString(),
                                 ddlPolaridade.Value.ToString(),
                                 formulaIndicador,
                                 codigoUsuarioResponsavel.ToString(), ddlCasasDecimais.Value.ToString(),
                                 (cbCheckResultante.Checked ? "S" : "N"),
                                 heGlossario.Html.ToString().Replace("'", "''"),
                                 cmbAgrupamentoDaMeta.Value.ToString(),
                                 indicaCriterio, int.Parse(txtLimite.Text), txtCodigoReservado.Text, int.Parse(ddlUnidadeNegocio.Value.ToString()), ddlResponsavelResultado.Value.ToString(),
                                 dataInicio, dataTermino, indicaAcompanhamentoMetaVigencia, ref msgErro);

        
        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        lblCaptionIndicador.Text = txtIndicador.Text;
        return msgErro;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        // busca a chave primaria
        string chave = getChavePrimaria();

        cDados.excluiIndicador(chave, codigoUsuarioResponsavel.ToString());
        carregaGvDados();
        return "";
    }

    // FAIXAS.

    private void carregaFaixaTolerancia()
    {
        int codigoIndicador = int.Parse(getChavePrimaria());

        DataSet dsTolerancia = cDados.getFaixaToleranciaObjeto(codigoIndicador, "IN", "");

        ddlC4.Value = "S";

        txtD1.Text = "";
        txtD2.Text = "";
        txtD3.Text = "";
        txtD4.Text = "";

        txtA1.Text = "";
        txtA2.Text = "";
        txtA3.Text = "";
        txtA4.Text = "";

        if (cDados.DataSetOk(dsTolerancia) && cDados.DataTableOk(dsTolerancia.Tables[0]))
        {
            DataTable dtFT = dsTolerancia.Tables[0];
            int quantidade = dtFT.Rows.Count;

            if (quantidade > 0)
            {
                txtD1.Text = dtFT.Rows[0]["ValorLimiteInferior"].ToString();
                txtA1.Text = dtFT.Rows[0]["ValorLimiteSuperior"].ToString();

                if (quantidade > 1)
                {
                    txtD2.Text = dtFT.Rows[1]["ValorLimiteInferior"].ToString();
                    txtA2.Text = dtFT.Rows[1]["ValorLimiteSuperior"].ToString();

                    if (quantidade > 2)
                    {
                        txtD3.Text = dtFT.Rows[2]["ValorLimiteInferior"].ToString();
                        txtA3.Text = dtFT.Rows[2]["ValorLimiteSuperior"].ToString();

                        if (quantidade > 3)
                        {
                            ddlC4.Value = dtFT.Rows[3]["CorDesempenho"].ToString();
                            txtD4.Text = dtFT.Rows[3]["ValorLimiteInferior"].ToString();
                            txtA4.Text = dtFT.Rows[3]["ValorLimiteSuperior"].ToString();
                        }
                    }
                }
            }
        }
    }

    private void gravaFaixaTolerancia(int codigoIndicador)
    {
        cDados.excluiFaixasToleranciaObjeto("IN", codigoIndicador);

        insereFaixa(ddlC1.Value.ToString(), txtD1.Text, txtA1.Text, codigoIndicador);
        insereFaixa(ddlC2.Value.ToString(), txtD2.Text, txtA2.Text, codigoIndicador);
        insereFaixa(ddlC3.Value.ToString(), txtD3.Text, txtA3.Text, codigoIndicador);
        insereFaixa(ddlC4.Value.ToString(), txtD4.Text, txtA4.Text, codigoIndicador);
    }

    private void insereFaixa(string valorCmb, string valorTxt1, string valorTxt2, int codigoIndicador)
    {
        if (valorCmb != "S" && (double.Parse(valorTxt2) != 0))
        {
            cDados.incluiFaixaTolerancia("IN", codigoIndicador, double.Parse(valorTxt1), double.Parse(valorTxt2), valorCmb, "Meta");
        }
    }
      
    #endregion
    
    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //carregaGvDados();
    }

    protected void ddlResponsavelIndicador_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    protected void ddlResponsavelIndicador_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;

        ASPxComboBox comboBox = (ASPxComboBox)source;

        SqlDataSource2.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
        comboBox.DataSource = SqlDataSource2;
        comboBox.DataBind();
    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

        dsResponsavel.SelectParameters.Clear();
        dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
        comboBox.DataSource = dsResponsavel;
        comboBox.DataBind();
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //cDados.aplicaEstiloVisual(this);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "IndEstrat");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "IncluirNovoRegistro();", true, true, false, "IndEstrat", lblTituloTela.Text, this);
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluirComponente, "gridDadosIndicador.AddNewRow();", true, false, false, "IndEstrat", lblTituloTela.Text, this);
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
