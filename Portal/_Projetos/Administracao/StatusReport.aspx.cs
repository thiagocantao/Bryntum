using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using DevExpress.Web;

public partial class _Projetos_Administracao_StatusReport : System.Web.UI.Page
{
    protected class ListaDeObjetos
    {
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;
        public ListaDeObjetos()
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
        /// Adiciona um item na lista de objetos
        /// </summary>
        /// <param name="codigoUnidade">Código do objeto a adicionar</param>
        /// <param name="descricaoUnidade">Descrição do objeto a adicionar</param>
        public void Add(int codigoObjeto, string descricaoObjeto)
        {
            ListaDeCodigos.Add(codigoObjeto);
            ListaDeNomes.Add(descricaoObjeto);
        }

        public string GetDescricaoObjeto(int codigoObjeto)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoObjeto);
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoObjeto)
        {
            return ListaDeCodigos.Contains(codigoObjeto);
        }

    }

    dados cDados;
    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    private string resolucaoCliente = "";
    private char delimitadorValores = 'ֆ';
    private char delimitadorElementoLista = '¢';
    private int alturaPrincipal = 0;

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;

    protected string labelCarteiras;

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
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "AdmMdlSttRpt");
        }

        dsDado.ConnectionString = cDados.classeDados.getStringConexao();
        dsDadosIndicador.ConnectionString = cDados.classeDados.getStringConexao();
        dsFuncao.ConnectionString = cDados.classeDados.getStringConexao();

        DataSet ds = cDados.getParametrosSistema("labelCarteiras", "Associar_BAE", "Associar_RAP", "Associar_RPU", "Associar_SR", "Associar_GRF_BOLHA", "Associar_RAPU", "Associar_SR_Novo", "Associar_SR_MDL0007", "Associar_SR_PPJ01");
        DataRow dr = ds.Tables[0].AsEnumerable().First();
        labelCarteiras = dr["labelCarteiras"].ToString();
        lblCarteira.Text = labelCarteiras;
        hfGeral.Set("Associar_BAE", dr["Associar_BAE"] as string);
        hfGeral.Set("Associar_RAP", dr["Associar_RAP"] as string);
        hfGeral.Set("Associar_RPU", dr["Associar_RPU"] as string);
        hfGeral.Set("Associar_SR", dr["Associar_SR"] as string);
        hfGeral.Set("Associar_GRF_BOLHA", dr["Associar_GRF_BOLHA"] as string);
		hfGeral.Set("Associar_RAPU", dr["Associar_RAPU"] as string);
		hfGeral.Set("Associar_SR_Novo", dr["Associar_SR_Novo"] as string);
        hfGeral.Set("Associar_SR_PPJ01", dr["Associar_SR_PPJ01"] as string);
        hfGeral.Set("Associar_SR_MDL0007", dr["Associar_SR_MDL0007"] as string);

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "AdmMdlSttRpt");
        podeEditar = podeIncluir;
        podeExcluir = podeIncluir;

        btnSalvarCompartilhar.Visible = podeExcluir || podeEditar;
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelQuestao", "labelQuestoes","lblGeneroLabelQuestao");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string label = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
            string generoLabel = ds.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();
            lblQuestoes.Text = label;

            ceQuestoesAtivas.Text = string.Format("{0} Ativ{1}s", label, generoLabel == "M" ? "o" : "a");
            ceQuestoesResolvidas.Text = string.Format("{0} Resolvid{1}s", label, generoLabel == "M" ? "o" : "a");
            ceComentarQuestoes.Text = string.Format("Comentar {0}", label);

        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        dsDado.SelectParameters[0].DefaultValue = codigoEntidadeUsuarioResponsavel.ToString();
        cDados.aplicaEstiloVisual(Page);//Ok
        carregaGvDados();               //Ok
        carregaGridDadosModeloStatusReport();    //Ok

        if (!IsPostBack)
        {
           populaCombos();                 //Ok
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        gvAssociacaoModelos.Settings.ShowFilterRow = false;
        gvAssociacaoModelos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvAssociacaoModelos.SettingsBehavior.AllowSort = false;
    }

    #region GRID's

    private void carregaGridDadosModeloStatusReport()
    {
        //string codigoModeloStatusReport = (gvDados.FocusedRowIndex == -1) ? "-1" : gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoModeloStatusReport").ToString();
    }

    private void carregaCriterioModeloStatusReport()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #region GRID gvDADOS

    private void carregaGvDados()
    {
        string where = "";
        //int index;
        DataSet ds = cDados.getModeloStatusReport(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
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
            if (podeExcluir)
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

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }

    #endregion

    #endregion

    #region COMBOBOX

    private void populaCombos()
    {
        DataSet ds = new DataSet();

        //combo Periodicidade: [CodigoPeriodicidade, DescricaoPeriodicidade_PT]
        ds = cDados.getPeriodicidade("");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            //cDados.PopulaDropDownASPx(this, ds.Tables[0], "CodigoPeriodicidade", "DescricaoPeriodicidade_PT", "", ref ddlPeriodicidade);
            cDados.PopulaDropDownASPx(this, ds.Tables[0], "CodigoPeriodicidade", "DescricaoPeriodicidade_PT", "", ref ddlPeriodicidade);
        }
    }

    private void cmbDado_OnCallback(object source, CallbackEventArgsBase e) //Inicia alguma atividade no combo.
    {
        ASPxComboBox combo = source as ASPxComboBox; //pego o combo.
        insereNovoDado(); //chamo a o metodo que faz...
        combo.DataBind();
    }

    #endregion

    #region LISTBOX

    protected void lbItensDisponiveis_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string tipoObjeto = e.Parameter.Substring(7, 2);
            string codModeloStatusReport;
            int codigoModeloStatusReport;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do modelo do relatório a partir da 7a. posição
                codModeloStatusReport = e.Parameter.Substring(9);
                Session["TipoObjeto"] = tipoObjeto;

                if (int.TryParse(codModeloStatusReport, out codigoModeloStatusReport))
                {
                    populaListaBox_ObjetosDisponiveis(codigoModeloStatusReport, tipoObjeto);
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_ObjetosDisponiveis(int codigoModeloStatusReport, string iniciaisTipoObjeto)
    {
        string nomeTabela, valueField, textField, clausulaWhere;
        iniciaisTipoObjeto = iniciaisTipoObjeto.ToUpper();

        #region Define variáveis
        switch (iniciaisTipoObjeto)
        {
            case "PR":
                nomeTabela = "Projeto";
                valueField = "CodigoProjeto";
                textField = "NomeProjeto";
                clausulaWhere = @"
        	AND DataExclusao IS NULL
            AND CodigoStatusProjeto IN (SELECT [CodigoStatus] FROM [Status] WHERE [IndicaMostraNoBoletimStatus] = 'S')
        	AND CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ' OR tp.IndicaTipoProjeto = 'PRG')";
                break;
            case "UN":
                nomeTabela = "UnidadeNegocio";
                valueField = "CodigoUnidadeNegocio";
                textField = "NomeUnidadeNegocio";
                clausulaWhere = @"
        	AND DataExclusao IS NULL
        	AND IndicaUnidadeNegocioAtiva = 'S'
        	AND CodigoUnidadeNegocio <> CodigoEntidade";
                break;
            case "EN":
                nomeTabela = "UnidadeNegocio";
                valueField = "CodigoUnidadeNegocio";
                textField = "NomeUnidadeNegocio";
                clausulaWhere = @"
        	AND DataExclusao IS NULL
        	AND IndicaUnidadeNegocioAtiva = 'S'
        	AND CodigoUnidadeNegocio = CodigoEntidade ";
                break;
            case "CP":
                nomeTabela = "Carteira";
                valueField = "CodigoCarteira";
                textField = "NomeCarteira";
                clausulaWhere = "AND ISNULL(IniciaisCarteiraControladaSistema,'') <> 'PR'";
                break;
            default:
                return;
        }
        #endregion

        #region Comando SQL

        string comandoSql = string.Format(@"
                        DECLARE @CodigoEntidade INT
                        DECLARE @CodigoModeloStatusReport INT
                        DECLARE @CodigoTipoAssociacaoObjeto INT
        	                SET @CodigoEntidade = {2}
        	                SET @CodigoModeloStatusReport = {7}
        	                SET @CodigoTipoAssociacaoObjeto = {0}.{1}.f_GetCodigoTipoAssociacao('{8}')
        
                         SELECT {5} AS CodigoObjeto, 
                                {6} AS DescricaoObjeto
                           FROM {0}.{1}.{3} obj
                          WHERE NOT EXISTS (SELECT 1 
        					                  FROM {0}.{1}.ModeloStatusReportObjeto msro 
        					                 WHERE msro.CodigoModeloStatusReport = @CodigoModeloStatusReport
        					                   AND msro.CodigoTipoAssociacaoObjeto = @CodigoTipoAssociacaoObjeto
                                               AND msro.IndicaModeloAtivoObjeto = 'S'
        					                   AND msro.CodigoObjeto = obj.{5})
                            AND CodigoEntidade = @CodigoEntidade {4}
                          ORDER BY
                obj.{6}"
                    , dbName, dbOwner, codigoEntidadeUsuarioResponsavel, nomeTabela, clausulaWhere
                    , valueField, textField, codigoModeloStatusReport, iniciaisTipoObjeto);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataTable dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensDisponiveis.DataSource = dt;
            lbItensDisponiveis.TextField = "DescricaoObjeto";
            lbItensDisponiveis.ValueField = "CodigoObjeto";
            lbItensDisponiveis.DataBind();
        }

        for (int i = 0; i < lbItensDisponiveis.Items.Count; i++)
        {
            if (i % 2 == 0)
                lbItensDisponiveis.Items[i].ImageUrl = "~/~/imagens/brancoMenor.gif";
        }
    }

    protected void lbItensSelecionados_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string tipoObjeto = e.Parameter.Substring(7, 2);
            string codModeloStatusReport;
            int codigoModeloStatusReport;

            if (comando == "POPLBX")
            {
                codModeloStatusReport = e.Parameter.Substring(9);
                Session["TipoObjeto"] = tipoObjeto;

                if (int.TryParse(codModeloStatusReport, out codigoModeloStatusReport))
                {
                    populaListaBox_ObjetosSelecionados(codigoModeloStatusReport, tipoObjeto);
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_ObjetosSelecionados(int codigoModeloStatusReport, string iniciaisTipoObjeto)
    {
        string nomeTabela, valueField, textField, clausulaWhere;
        iniciaisTipoObjeto = iniciaisTipoObjeto.ToUpper();

        #region Define variáveis
        switch (iniciaisTipoObjeto)
        {
            case "PR":
                nomeTabela = "Projeto";
                valueField = "CodigoProjeto";
                textField = "NomeProjeto";
                clausulaWhere = @"
        	AND DataExclusao IS NULL
            AND CodigoStatusProjeto = 3
        	AND CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ' OR tp.IndicaTipoProjeto = 'PRG')";
                break;
            case "UN":
                nomeTabela = "UnidadeNegocio";
                valueField = "CodigoUnidadeNegocio";
                textField = "NomeUnidadeNegocio";
                clausulaWhere = @"
        	AND DataExclusao IS NULL
        	AND IndicaUnidadeNegocioAtiva = 'S'
        	AND CodigoUnidadeNegocio <> CodigoEntidade";
                break;
            case "EN":
                nomeTabela = "UnidadeNegocio";
                valueField = "CodigoUnidadeNegocio";
                textField = "NomeUnidadeNegocio";
                clausulaWhere = @"
        	AND DataExclusao IS NULL
        	AND IndicaUnidadeNegocioAtiva = 'S'
        	AND CodigoUnidadeNegocio = CodigoEntidade ";
                break;
            case "CP":
                nomeTabela = "Carteira";
                valueField = "CodigoCarteira";
                textField = "NomeCarteira";
                clausulaWhere = "AND ISNULL(IniciaisCarteiraControladaSistema,'') <> 'PR'";
                break;
            default:
                return;
        }
        #endregion

        #region Comando SQL

        string comandoSql = string.Format(@"
                        DECLARE @CodigoEntidade INT
                        DECLARE @CodigoModeloStatusReport INT
                        DECLARE @CodigoTipoAssociacaoObjeto INT
        	                SET @CodigoEntidade = {2}
        	                SET @CodigoModeloStatusReport = {7}
        	                SET @CodigoTipoAssociacaoObjeto = {0}.{1}.f_GetCodigoTipoAssociacao('{8}')
        
                         SELECT {5} AS CodigoObjeto, 
                                {6} AS DescricaoObjeto
                           FROM {0}.{1}.{3} obj INNER JOIN
								{0}.{1}.ModeloStatusReportObjeto msro ON obj.{5} = msro.CodigoObjeto AND 
                                                                 msro.CodigoTipoAssociacaoObjeto = @CodigoTipoAssociacaoObjeto
                          WHERE msro.CodigoModeloStatusReport = @CodigoModeloStatusReport
                            AND msro.IndicaModeloAtivoObjeto = 'S'
							AND CodigoEntidade = @CodigoEntidade {4}
                          ORDER BY
                obj.{6}"
                    , dbName, dbOwner, codigoEntidadeUsuarioResponsavel, nomeTabela, clausulaWhere
                    , valueField, textField, codigoModeloStatusReport, iniciaisTipoObjeto);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataTable dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensSelecionados.DataSource = dt;
            lbItensSelecionados.TextField = "DescricaoObjeto";
            lbItensSelecionados.ValueField = "CodigoObjeto";
            lbItensSelecionados.DataBind();
        }
    }

    private void obtemListaObjetosSelecionados(string codigoModeloStatusReport, ref ListaDeObjetos listaDeObjetos)
    {
        obtemListaObjetos("Sel_", codigoModeloStatusReport, ref listaDeObjetos);
    }

    private bool obtemListaObjetos(string inicial, string codigoModeloStatusReport, ref ListaDeObjetos listaDeObjetos)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaObjetos, temp;

        idLista = inicial + codigoModeloStatusReport + delimitadorValores;

        listaDeObjetos.Clear();

        if (hfObjetos.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfObjetos.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaObjetos = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaObjetos.Length; j++)
            {
                if (strListaObjetos[j].Length > 0)
                {
                    temp = strListaObjetos[j].Split(delimitadorValores);
                    listaDeObjetos.Add(int.Parse(temp[1]), temp[0]);
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }

    #endregion

    #region VARIOS

    private void insereNovoDado()
    {
        //metodo que insere um novo registro do usuario. de um jeito rapido.
        //ele so precisa do nome, mail, codigoUnidade, senha  e  ID do usuario que ta cadastrando.
        cDados.incluiDadoOperacionalGrid(txtNome.Text, codigoUsuarioResponsavel.ToString(), codigoEntidadeUsuarioResponsavel.ToString());
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 140;
        }
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/StatusReport.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "StatusReport", "ASPxListbox"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoModeloStatusReport").ToString();
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
            mensagemErro_Persistencia = persisteInclusaoCompartilhar();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    protected void pnCallbackMensagem_Callback(object sender, CallbackEventArgsBase e)
    {
        pnCallbackMensagem.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallbackMensagem.JSProperties["cp_OperacaoOk"] = "";
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Compartilhar")
        {
            mensagemErro_Persistencia = persisteInclusaoCompartilhar();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallbackMensagem.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro
    {
        string msg = "";
        int codigoNovoModeloStatusReport = 0;
        try
        {
            bool indicaPadraoNovo = hfGeral["TipoSR"].ToString().ToUpper().Equals("PADRAONOVO");

            cDados.incluiuModeloStatusReport(codigoEntidadeUsuarioResponsavel, txtNomeNovoModeloRelatorio.Text.Replace("'", "''"), codigoUsuarioResponsavel,
                getCharValue(ceTarefasAtrasadas), getCharValue(ceTarefasConcluidasPeriodo), getCharValue(ceTarefasProximoPeriodo),
                getCharValue(ceMarcosConcluidos), getCharValue(ceMarcosAtrasados), getCharValue(ceMarcosProximoPeriodo), 'N'/*getCharValue(ceGraficoDesempenhoFisico)*/,
                getCharValue(ceListaDesempenhoRecursos), 'N'/*getCharValue(ceGraficoDesempenhoCusto)*/, getCharValue(ceDetalhesCusto.Checked ? ceDetalhesCusto : ceInformacoesCusto), 'N'/*getCharValue(ceGraficoDesempenhoReceita)*/,
                getCharValue(ceDetalhesReceita), getCharValue(ceAnaliseValorAgregado), getCharValue(ceRiscosAtivos), getCharValue(ceRiscosEliminados),
                getCharValue(ceQuestoesAtivas), getCharValue(ceQuestoesResolvidas), getCharValue(ceMetasResultados), getCharValue(ceListaPendencias),
                getCharValue(ceComentarioGeral), getCharValue(ceComentarFisico), getCharValue(ceComentarFinanceiro), getCharValue(ceComentarRiscos),
                getCharValue(ceComentarQuestoes), getCharValue(ceComentarMetasResultados), getCharValue(ceContratos), getCharValue(cePlanoAcaoGeral), getCharValue(ceListaEntregas),
                Convert.ToInt32(ddlPeriodicidade.Value), string.IsNullOrEmpty(txtEspera.Text) ? 0 : int.Parse(txtEspera.Text), ref codigoNovoModeloStatusReport, indicaPadraoNovo);

            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoNovoModeloStatusReport);
            gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private char getCharValue(ASPxCheckBox ce)
    {
        return ce.Checked ? 'S' : 'N';
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
        string msg = "";
        try
        {
            int codigoModeloStatusReport = int.Parse(getChavePrimaria());
            cDados.atualizaModeloStatusReport(codigoEntidadeUsuarioResponsavel, txtNomeNovoModeloRelatorio.Text.Replace("'", "''"), codigoUsuarioResponsavel,
                    getCharValue(ceTarefasAtrasadas), getCharValue(ceTarefasConcluidasPeriodo), getCharValue(ceTarefasProximoPeriodo),
                    getCharValue(ceMarcosConcluidos), getCharValue(ceMarcosAtrasados), getCharValue(ceMarcosProximoPeriodo), 'N'/*getCharValue(ceGraficoDesempenhoFisico)*/,
                    getCharValue(ceListaDesempenhoRecursos), 'N'/*getCharValue(ceGraficoDesempenhoCusto)*/, getCharValue(ceDetalhesCusto.Checked ? ceDetalhesCusto: ceInformacoesCusto), 'N'/*getCharValue(ceGraficoDesempenhoReceita)*/,
                    getCharValue(ceDetalhesReceita), getCharValue(ceAnaliseValorAgregado), getCharValue(ceRiscosAtivos), getCharValue(ceRiscosEliminados),
                    getCharValue(ceQuestoesAtivas), getCharValue(ceQuestoesResolvidas), getCharValue(ceMetasResultados), getCharValue(ceListaPendencias),
                    getCharValue(ceComentarioGeral), getCharValue(ceComentarFisico), getCharValue(ceComentarFinanceiro), getCharValue(ceComentarRiscos),
					getCharValue(ceComentarQuestoes), getCharValue(ceComentarMetasResultados), getCharValue(ceContratos), getCharValue(cePlanoAcaoGeral), getCharValue(ceListaEntregas),
                    Convert.ToInt32(ddlPeriodicidade.Value), string.IsNullOrEmpty(txtEspera.Text) ? 0 : int.Parse(txtEspera.Text), codigoModeloStatusReport);

            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoModeloStatusReport);
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
        string msg = "";
        string chave = getChavePrimaria();
        try
        {
            cDados.excluiModeloStatusReport(int.Parse(chave), codigoUsuarioResponsavel);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        // carregaGvDados();
        return msg;
    }

    #endregion

    #region ==== Compartilhamento do Modelo com objetos ====

    private string persisteInclusaoCompartilhar()
    {
        try
        {
            int codigoModeloStatusReport = int.Parse(getChavePrimaria());
            string sqlCompartilhamento = string.Empty;

            montaComandoSQLCompartilhamento(codigoModeloStatusReport, out sqlCompartilhamento);

            if (sqlCompartilhamento != "")
            {
                int registrosAfetados = 0;
                cDados.execSQL(sqlCompartilhamento, ref registrosAfetados);
            }

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void montaComandoSQLCompartilhamento(int codigoModeloStatusReport, out string comandoSQL)
    {
        StringBuilder sqlDeleteObjetosDesselecionados = new StringBuilder();
        StringBuilder sqlInsertNovosObjetos = new StringBuilder();
        ListaDeObjetos ObjetosSelecionados = new ListaDeObjetos();
        ListaDeObjetos ObjetosPreExistentes = new ListaDeObjetos();
        string variaveis = @"
            DECLARE @CodigoModeloStatusReport INT
            DECLARE @CodigoTipoAssociacaoObjeto SMALLINT
            DECLARE @CodigoObjeto INT
            DECLARE @CodigoUsuarioDestinatario INT
";

        obtemListaObjetosSelecionados(ref ObjetosSelecionados);
        obtemListaObjetosPreExistentes(ref ObjetosPreExistentes);

        // se alguma das funções que monta os comandos para insert ou delete retornou true, é porque há alterações
        // nos registro. Neste caso, incluir a chamada a proc que recalcula os pesos dos objetos 
        montaDeleteObjetosDesselecionados(codigoModeloStatusReport, ObjetosSelecionados, ObjetosPreExistentes, sqlDeleteObjetosDesselecionados);
        montaInsertNovosObjetos(codigoModeloStatusReport, ObjetosSelecionados, ObjetosPreExistentes, sqlInsertNovosObjetos);

        comandoSQL = variaveis + sqlDeleteObjetosDesselecionados.ToString() + sqlInsertNovosObjetos.ToString();
    }

    private void obtemListaObjetosSelecionados(ref ListaDeObjetos listaDeObjetos)
    {
        obtemListaObjetos("Sel_", ref listaDeObjetos);
    }

    private void obtemListaObjetosPreExistentes(ref ListaDeObjetos listaDeObjetos)
    {
        obtemListaObjetos("InDB_", ref listaDeObjetos);
    }

    private bool obtemListaObjetos(string inicial, ref ListaDeObjetos listaDeObjetos)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaObjetos, temp;

        idLista = inicial;

        listaDeObjetos.Clear();

        if (hfObjetos.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfObjetos.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaObjetos = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaObjetos.Length; j++)
            {
                if (strListaObjetos[j].Length > 0)
                {
                    temp = strListaObjetos[j].Split(delimitadorValores);
                    listaDeObjetos.Add(int.Parse(temp[1]), temp[0]);
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }

    private bool montaDeleteObjetosDesselecionados(int codigoModeloStatusReport, ListaDeObjetos objetosSelecionados, ListaDeObjetos objetosPreExistentes, StringBuilder comandoSQL)
    {
        bool bRet = false;
        string tipoObjeto = ((string)Session["TipoObjeto"]).ToUpper();

        foreach (int objetoPreExistente in objetosPreExistentes.ListaDeCodigos)
        {
            // se o objeto não constar mais nos objetos selecionados, desativa o objeto na lista 
            if (false == objetosSelecionados.ContemCodigo(objetoPreExistente))
            {
                bRet = true;
                comandoSQL.Append(string.Format(@"
                UPDATE {0}.{1}.[ModeloStatusReportObjeto] SET [IndicaModeloAtivoObjeto] = 'N'
                    WHERE [CodigoModeloStatusReport] = {2} 
                      AND [CodigoObjeto] = {3} 
                      AND [CodigoTipoAssociacaoObjeto] = {0}.{1}.f_GetCodigoTipoAssociacao('{4}')
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoModeloStatusReport, objetoPreExistente, tipoObjeto));

            } // if (false == projetosSelecionados.ContemCodigo(projetoPreExistente))
        } // foreach (int projetoPreExistente in projetosPreExistentes.ListaDeCodigos)
        return bRet;
    }

    private bool montaInsertNovosObjetos(int codigoModeloStatusReport, ListaDeObjetos objetoSelecionados, ListaDeObjetos objetosPreExistentes, StringBuilder comandoSQL)
    {
        bool bRet = false;
        string tipoObjeto = ((string)Session["TipoObjeto"]).ToUpper();

        foreach (int objetoSelecionado in objetoSelecionados.ListaDeCodigos)
        {
            // se o objeto selecionado não constar nos objetos pré-existentes
            // compõe comando que irá incluí-lo no compartilhamento
            if (false == objetosPreExistentes.ContemCodigo(objetoSelecionado))
            {
                bRet = true;
                comandoSQL.Append(string.Format(@"
                UPDATE {0}.{1}.[ModeloStatusReportObjeto] SET [IndicaModeloAtivoObjeto] = 'S'
                    WHERE [CodigoModeloStatusReport] = {2} 
                      AND [CodigoObjeto] = {3} 
                      AND [CodigoTipoAssociacaoObjeto] = {0}.{1}.f_GetCodigoTipoAssociacao('{4}')
                
                IF (@@ROWCOUNT = 0) BEGIN
                    INSERT INTO {0}.{1}.[ModeloStatusReportObjeto] 
	                    ( [CodigoModeloStatusReport], [CodigoObjeto], [IndicaModeloAtivoObjeto], [CodigoTipoAssociacaoObjeto])
	                    VALUES ( {2}, {3}, 'S', {0}.{1}.f_GetCodigoTipoAssociacao('{4}'));
                END
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoModeloStatusReport, objetoSelecionado, tipoObjeto));

            } // if (false == projetosSelecionados.ContemCodigo(projetoPreExistente))
        } // foreach (int projetoSelecionado in projetosSelecionados.ListaDeCodigos)
        return bRet;
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
    protected void gvAssociacaoModelos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        int codigoModeloStatusReport = int.Parse(string.IsNullOrEmpty(e.Parameters) ? getChavePrimaria() : e.Parameters);

        carregaGvAssociacaoModelos(codigoModeloStatusReport);
    }

    protected void gvAssociacaoModelos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        object codigoModeloStatusReport = e.Values["CodigoModeloStatusReport"];
        object codigoTipoAssociacaoObjeto = e.Values["CodigoTipoAssociacaoObjeto"];
        object codigoObjeto = e.Values["CodigoObjeto"];
        string comandoSql = string.Format(@"
DECLARE @CodigoModeloStatusReport INT
DECLARE @CodigoTipoAssociacaoObjeto INT
DECLARE @CodigoObjeto INT
	SET @CodigoModeloStatusReport = {2}
	SET @CodigoTipoAssociacaoObjeto = {3}
	SET @CodigoObjeto = {4}

 UPDATE {0}.{1}.ModeloStatusReportObjeto
	SET IndicaModeloAtivoObjeto = 'N'
  WHERE CodigoModeloStatusReport = @CodigoModeloStatusReport
	AND CodigoTipoAssociacaoObjeto = @CodigoTipoAssociacaoObjeto
	AND CodigoObjeto = @CodigoObjeto"
            , dbName, dbOwner, codigoModeloStatusReport, codigoTipoAssociacaoObjeto, codigoObjeto);

        int regAfetados = 0;
        cDados.execSQL(comandoSql, ref regAfetados);

        carregaGvAssociacaoModelos((int)codigoModeloStatusReport);

        e.Cancel = true;
    }

    private void carregaGvAssociacaoModelos(int codigoModeloStatusReport)
    {
        string comandoSql = string.Format(@"
DECLARE @CodigoEntidade INT
DECLARE @CodigoModeloStatusReport INT
	SET @CodigoEntidade = {2}
	SET @CodigoModeloStatusReport = {3}

 SELECT CodigoModeloStatusReport,
		CodigoObjeto,
		CodigoTipoAssociacaoObjeto,
        (SELECT dbo.f_GetDescricaoOrigemAssociacaoObjeto(@CodigoEntidade, CodigoTipoAssociacaoObjeto, null, CodigoObjeto,0,null) ) AS DescricaoObjeto
   FROM {0}.{1}.ModeloStatusReportObjeto
  WHERE IndicaModeloAtivoObjeto = 'S'
	AND CodigoModeloStatusReport = @CodigoModeloStatusReport"
            , dbName, dbOwner, codigoEntidadeUsuarioResponsavel, codigoModeloStatusReport);

        DataSet ds = cDados.getDataSet(comandoSql);
        gvAssociacaoModelos.DataSource = ds.Tables[0];
        gvAssociacaoModelos.DataBind();
    }
    protected void cmbObjeto_Callback(object sender, CallbackEventArgsBase e)
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ConfigStatusRep");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
		cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "AbreFormularioNovoSR('padraonovo');", true, true, false, "ConfigStatusRep", lblTituloTela.Text, this);
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
