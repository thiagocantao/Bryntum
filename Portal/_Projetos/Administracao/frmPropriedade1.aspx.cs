using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;

public partial class frmPropriedade1 : System.Web.UI.Page
{
    #region Properties

    private string _connectionString;
    /// <summary>
    /// String de conexão para acesso a dados da tela.
    /// </summary>
    public string ConnectionString
    {
        get { return _connectionString; }
        private set
        {
            sdsDadosFomulario.ConnectionString =
            sdsDocumentosRegistrados.ConnectionString = 
            sdsAreaAverbada.ConnectionString =
            sdsPessoaImovel.ConnectionString =
            sdsUF.ConnectionString = 
            _connectionString = value;
        }
    }

    private int _codigoProjeto;
    /// <summary>
    /// Código do projeto vinculado a proposta de iniciativa. Caso seja igual a '-1' trata-se de uma nova proposta de iniciativa
    /// </summary>
    public int CodigoProjeto
    {
        get { return _codigoProjeto; }
        set
        {
            Session["codigoProjeto"] = value;
            _codigoProjeto = value;
        }
    }

    private int _codigoImovel;
    /// <summary>
    /// Código do imovel sendo editado. Caso seja igual a '-1' trata-se de um imovel ainda não gravado na base de dados.
    /// </summary>
    public int CodigoImovel
    {
        get { return _codigoImovel; }
        set
        {
            Session["codigoImovel"] = value;
            _codigoImovel = value;
        }
    }

    /// <summary>
    /// Indica se a edição atual é de novo imóvel (e não de um imóvel existente)
    /// </summary>
    public bool IndicaNovoImovel
    {
        get
        {
            return _codigoImovel == -1;
        }
    }


    private int _codigoEntidadeUsuarioResponsavel;
    /// <summary>
    /// Código da entidade do usuário logado
    /// </summary>
    public int CodigoEntidadeUsuarioResponsavel
    {
        get { return _codigoEntidadeUsuarioResponsavel; }
        set
        {
            cDados.setInfoSistema("CodigoEntidade", value);
            Session["codigoEntidade"] = value;
            _codigoEntidadeUsuarioResponsavel = value;
        }
    }

    private int _codigoUsuarioResponsavel;
    /// <summary>
    /// Código do usuário logado
    /// </summary>
    public int CodigoUsuarioResponsavel
    {
        get { return _codigoUsuarioResponsavel; }
        set
        {
            cDados.setInfoSistema("IDUsuarioLogado", value);
            Session["codigoUsuario"] = value;
            _codigoUsuarioResponsavel = value;
        }
    }

    #endregion

    #region Private Fields
    private bool readOnly;
    private bool podeEditar;
    public bool podeIncluir;
    private bool podeExcluir;
    private dados cDados;

    public string alturaTela = "";
    public string resolucaoCliente = "";
    #endregion

    #region Event Handlers

    #region Page

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
         
        if (!IsPostBack)
        {
            int activeTabIndex;
            int alturaInt;
            if (int.TryParse(Request.QueryString["tab"], out activeTabIndex))
                pageControl.ActiveTabIndex = activeTabIndex;
            if (int.TryParse(Request.QueryString["Altura"], out alturaInt))
                alturaTela = (alturaInt - 120).ToString();//era 113 originalmente
            if (!string.IsNullOrEmpty(alturaTela))
            {
                //style="overflow: auto; height: 500px"
                divRolagem.Style.Add("height", (alturaInt - 120 + 12) + "px");
                divRolagem.Style.Add("overflow", "auto");
                
                //dv01.Style.Add("overflow", "auto");
                //dv02.Style.Add("overflow", "auto");
                gvProprietarioOcupante.Settings.VerticalScrollableHeight = int.Parse(alturaTela);
            }
        }

        string cp = Request.QueryString["CP"];
        CodigoProjeto = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);
        readOnly = "S".Equals(Request.QueryString["RO"]);
        podeEditar = !readOnly;
        podeExcluir = !readOnly;
        CodigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HeaderOnTela();
        //Obtem a string de conexao e seta a propriedade 'ConnectionString' dos SQL Data Sources da página.
        ConnectionString = cDados.classeDados.getStringConexao();
        DefineVisibilidadeCampos();
        DefineCamposSomenteLeitura();
        
        // Busca os dados do imóvel
        if (!IsPostBack)
        {
            CarregaDadosFormulario();
        }
        _codigoImovel = int.Parse(Session["codigoImovel"].ToString());
        podeIncluir = !readOnly && !IndicaNovoImovel;
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        hfGeral.Set("alturaTela", altura - 15);
        hfGeral.Set("larguraTela", largura);
        
        cDados.aplicaEstiloVisual(pageControl);
    }

    private void HeaderOnTela()
    {
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/frmPropriedades.js""></script>"));
    }

    #endregion

    #region Todas as grids

    protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        switch (e.ButtonType)
        {
            case ColumnCommandButtonType.Delete:
                if (!podeExcluir)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
                else if (grid == gvDocRegistradoImovel)
                {
                    SqlDataSource[] sources = new SqlDataSource[] { sdsDocumentosRegistrados};
                    //string codigoAcao = gvDocRegistradoImovel.GetDataRow(e.VisibleIndex)[nomeColunaCodigo].ToString();
                }
                break;
            case ColumnCommandButtonType.Edit:
                if (!podeEditar)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
                break;
            default:
                break;
        }
    }

    protected void grid_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpMyRowCount"] = ((ASPxGridView)sender).VisibleRowCount;
    }

    #endregion

    #region gvAcoes



    protected void gvAcoes_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        sdsDocumentosRegistrados.Update();
    }

    #endregion


    #region gvEstrategico

   

    
    #endregion

    #region gvResultados




    #endregion

    #region cmbLider

    

    #endregion


    #endregion

    #region Methods

    private void DefineCamposSomenteLeitura()
    {
        bool permiteEdicao = !readOnly;
        //Define se a tela vai estar disponível somente pra leitura
        //pnlDescricaoProjeto.Enabled = permiteEdicao;
        //pnlElementosResultado.Enabled = permiteEdicao;
        btnSalvar.ClientVisible = permiteEdicao;
    }

    private void DefineVisibilidadeCampos()
    {
        //Alguns campos estarão visíveis apenas quando a edição do formulário estiver habilitada...
        if (readOnly)
            Header.Controls.Add(cDados.getLiteral("<style type=\"text/css\"> .AvailableOnlyEditing {display: none;}</style>"));
        //...enquanto outros estarão visíveis apenas quando o formulário estiver diponível apenas para leitura
        else
            Header.Controls.Add(cDados.getLiteral("<style type=\"text/css\"> .AvailableReadOnly {display: none;}</style>"));
    }

    /// <summary>
    /// Define o campo descrição (keyTextField) para um determinado campo chave (keyValueField), 
    /// que é obtido, na fonte de dados (source), pela coluna descrição (sourceTextField).
    /// Este método é util para obter a descrição na grid de uma coluna do tipo 'ComboBox'
    /// </summary>
    /// <param name="fields">Dicionário com os campos.</param>
    /// <param name="keyValueField">Nome do campo, no dicionário, correspondente a coluna chave.</param>
    /// <param name="keyTextField">Nome do campo, no dicionário, correspondente a coluna de descrição.</param>
    /// <param name="source">SqlDataSource que serve de fonte de dados.</param>
    /// <param name="sourceValueField">Nome da coluna chave na fonte de dados.</param>
    /// <param name="sourceTextField">Nome da coluna descrição na fonte de dados.</param>

    protected string ObtemBotaoInclusaoRegistro(string nomeGrid, string assuntoGrid)
    {
        if (nomeGrid != "gvProprietarioOcupante")
        {
            string tituloBotaoDesabilitado = IndicaNovoImovel ? string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid) : assuntoGrid;
            string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
            string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""{1}"" alt=""Novo"" onclick=""{0}.AddNewRow();"" style=""cursor: pointer;""/>", nomeGrid, "Incluir " + assuntoGrid);

            string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
                , (podeIncluir) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);

            return strRetorno;
        }
        else
        {
            int altura = 0;
            int largura = 0;
            bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

            string tituloBotaoDesabilitado = IndicaNovoImovel ? string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid) : assuntoGrid;
            string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
            string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""{3}"" alt=""Novo"" onclick=""abrejanelaPessoaImovel({0},{1},{2})"" style=""cursor: pointer;""/>", "new Array(" + CodigoProjeto + ",-1 )", largura, altura, "Incluir " + assuntoGrid);

            string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
                , (podeIncluir) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);

            return strRetorno;
        }
        
    }

    protected string ObtemBotaoInclusaoPessoaImovel()
    {
        int codigoImovel = CodigoProjeto;
        bool  podeIncluirPessoa = (codigoImovel != -1);
        string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", "Novo Proprietário/ocupante");
        
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        //passar a largura e a altura da tela para  função de javascript responsavel por abrir o popup
        string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""abrejanelaPessoaImovel({0},{1},{2});"" style=""cursor: pointer;""/>", "new Array(" + CodigoProjeto + ",-1)", largura, altura);
        //novaMensagem.aspx?CP=" + codProj, "Nova Mensagem", 720, 450, "", myObject);
        string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
            , (podeIncluirPessoa) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);

        return strRetorno;
    }
    
    private string getChavePrimaria() // retorna a primary key da tabela
    {
        if (gvProprietarioOcupante.FocusedRowIndex >= 0)
            return gvProprietarioOcupante.GetRowValues(gvProprietarioOcupante.FocusedRowIndex, gvProprietarioOcupante.KeyFieldName).ToString();
        else
            return "";
    }


    private void CarregaDadosFormulario()
    {
        sdsDadosFomulario.DataBind();
        DataView dv = (DataView)sdsDadosFomulario.Select(new DataSourceSelectArguments());

        if (dv.Count > 0)
        {
            DataRowView row = dv[0];

            CodigoImovel = (int)row["CodigoImovel"];

            txtSeq.Value = row["CodigoAntigo"];
            txtSeq1.Value = row["CodigoNovo"];

            txtLocalizacao.Value = row["Localizacao"];
            txtDistrito.Value = row["Distrito"];
            ddlMunicipio.Value = row["NomeMunicipio"];
            txtComarca.Value = row["Comarca"];
            txtRegiao.Value = row["NomeRegiao"];
            txtIdFundiaria.Value = row["IdentificacaoFundiaria"];
            txtCoordenadas.Value = row["Coordenadas"];
            speAreaTotal.Value = row["AreaTotal"];
            speAreaAtingida.Value = row["AreaAtingida"];
            ddlSpolio.Value = row["IndicaEspolio"];

            txtNomeInventariante.ClientEnabled = ddlSpolio.Value.ToString().Trim() == "S";
            txtJuizo.ClientEnabled = ddlSpolio.Value.ToString().Trim() == "S";
            txtCartorio.ClientEnabled = ddlSpolio.Value.ToString().Trim() == "S";
            txtAdvogado.ClientEnabled = ddlSpolio.Value.ToString().Trim() == "S";
            txtEndereco.ClientEnabled = ddlSpolio.Value.ToString().Trim() == "S";
            txtFone.ClientEnabled = ddlSpolio.Value.ToString().Trim() == "S";

            txtNomeInventariante.Value = row["NomeInventariante"];
            txtJuizo.Value = row["JuizoEspolio"];
            txtCartorio.Value = row["CartorioEspolio"];
            txtAdvogado.Value = row["NomeAdvogadoEspolio"];
            txtEndereco.Value = row["EnderecoAdvogadoEspolio"];
            txtFone.Value = row["TelefoneAdvogadoEspolio"];
            memoObservacoes.Value = row["Observacao"];
            Session["codigoImovel"] = row["CodigoImovel"];
            ddlUF.Value = row["SiglaUF"];


            string comandoSQL = string.Format(@"
            SELECT CodigoMunicipio
                   ,NomeMunicipio
                   ,SiglaUF
              FROM {0}.{1}.Municipio
             WHERE SiglaUF = '{2}'", cDados.getDbName(), cDados.getDbOwner(), row["SiglaUF"]);

            DataSet ds = cDados.getDataSet(comandoSQL);
            ddlMunicipio.DataSource = ds.Tables[0];
            ddlMunicipio.TextField = "NomeMunicipio";
            ddlMunicipio.ValueField = "CodigoMunicipio";
            ddlMunicipio.DataBind();

            ddlMunicipio.Text = row["NomeMunicipio"].ToString();
            ddlMunicipio.Value = row["CodigoMunicipio"].ToString();
            txtProcedenciaAquisicao.Value = row["ProcedenciaAquisicao"].ToString();
            txtDadosUltimaDeclaracaoITR.Value = row["DadosUltimaDeclaracaoITR"].ToString();
        }
        else
        {
            CodigoImovel = -1;
        }
    }

    protected void limpaDadosFormulario()
    {
        //DataRowView row = dv[0];

        txtSeq.Value = "";
        txtSeq1.Value = "";
        txtLocalizacao.Value = "";
        txtDistrito.Value = "";
        ddlMunicipio.Value = "";
        txtComarca.Value = "";
        txtRegiao.Value = "";
        txtIdFundiaria.Value = "";
        txtCoordenadas.Value = "";
        speAreaTotal.Value = "";
        speAreaAtingida.Value = "";
        ddlSpolio.Value = "";
        txtNomeInventariante.Value = "";
        txtJuizo.Value = "";
        txtCartorio.Value = "";
        txtAdvogado.Value = "";
        txtEndereco.Value = "";
        txtFone.Value = "";
        memoObservacoes.Value = "";
    }
    #endregion


    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {

        string tipoOperacao;
        if (IndicaNovoImovel)
            tipoOperacao = "I";
        else
            tipoOperacao = "U";
        SalvaEdicaoPropriedade();
        e.Result = tipoOperacao + Request.QueryString.ToString();
    }

    protected void SalvaEdicaoPropriedade()
    {
        #region Comando SQL

        string comandoSql = string.Format(@"
        DECLARE @Erro INT
        DECLARE @MensagemErro nvarchar(2048)
        SET @Erro = 0
        BEGIN TRAN
        BEGIN TRY
     DECLARE @SequencialImovel varchar(25)
            ,@CodigoImovel int
            ,@Localizacao  varchar(250)
            ,@CodigoMunicipio  int
            ,@Distrito varchar(250)
            ,@Comarca  varchar(250)
            ,@NomeRegiao  varchar(250)
            ,@IdentificacaoFundiaria  varchar(250)
            ,@Coordenadas  varchar(50)
            ,@AreaTotal  decimal(25,6)
            ,@AreaAtingida decimal(25,6)
            ,@ProcedenciaAquisicao varchar(250)
            ,@Observacao  varchar(500)
            ,@IndicaEspolio char(1)
            ,@NomeInventariante  varchar(150)
            ,@JuizoEspolio  varchar(250)
            ,@CartorioEspolio  varchar(250)
            ,@NomeAdvogadoEspolio  varchar(150)
            ,@EnderecoAdvogadoEspolio  varchar(500)
            ,@TelefoneAdvogadoEspolio varchar(50)
            ,@CodigoProjeto  int
            ,@DadosUltimaDeclaracaoITR varchar(250)
            ,@CodigoAntigo varchar(50)
            ,@CodigoNovo varchar(50)
        

            SET @SequencialImovel = '{2}'
            SET @CodigoImovel = {23}
            SET @Localizacao = '{3}'
            SET @CodigoMunicipio = {4}
            SET @Distrito = '{5}'
            SET @Comarca = '{6}'
            SET @NomeRegiao = '{7}'
            SET @IdentificacaoFundiaria = '{8}'
            SET @Coordenadas = '{9}'
            SET @AreaTotal = {10}
            SET @AreaAtingida = {11}
            SET @ProcedenciaAquisicao = '{12}'
            SET @Observacao = '{13}'
            SET @IndicaEspolio = '{14}'
            SET @NomeInventariante = '{15}'
            SET @JuizoEspolio = '{16}'
            SET @CartorioEspolio = '{17}'
            SET @NomeAdvogadoEspolio = '{18}'
            SET @EnderecoAdvogadoEspolio = '{19}'
            SET @TelefoneAdvogadoEspolio = '{20}'
            SET @CodigoProjeto  = {21}
            SET @DadosUltimaDeclaracaoITR = '{22}'
            SET @CodigoImovel = {23}
            SET @CodigoAntigo = '{2}'
            SET @CodigoNovo = '{24}'          
            IF ( @CodigoImovel = -1) BEGIN
                INSERT INTO {0}.{1}.Prop_Imovel ( SequencialImovel         ,CodigoMunicipio         ,IndicaEspolio            ,CodigoProjeto
                                                 ,Localizacao              ,Distrito                ,Comarca                  ,NomeRegiao
                                                 ,IdentificacaoFundiaria   ,Coordenadas             ,AreaTotal                ,AreaAtingida
                                                 ,ProcedenciaAquisicao     ,Observacao              ,NomeInventariante        ,JuizoEspolio            
                                                 ,CartorioEspolio          ,NomeAdvogadoEspolio     ,EnderecoAdvogadoEspolio  ,TelefoneAdvogadoEspolio
                                                 ,DadosUltimaDeclaracaoITR ,CodigoAntigo            ,CodigoNovo)
                                          VALUES (@SequencialImovel        ,@CodigoMunicipio        ,@IndicaEspolio           ,@CodigoProjeto
                                                 ,@Localizacao             ,@Distrito               ,@Comarca                 ,@NomeRegiao
                                                 ,@IdentificacaoFundiaria  ,@Coordenadas            ,@AreaTotal               ,@AreaAtingida
                                                 ,@ProcedenciaAquisicao    ,@Observacao             ,@NomeInventariante       ,@JuizoEspolio
                                                 ,@CartorioEspolio         ,@NomeAdvogadoEspolio    ,@EnderecoAdvogadoEspolio ,@TelefoneAdvogadoEspolio
                                                 ,@DadosUltimaDeclaracaoITR,@CodigoAntigo           ,@CodigoNovo)
                SET @Erro = @Erro + @@ERROR                
                SET @CodigoImovel = SCOPE_IDENTITY();
                SET @Erro = @Erro + @@ERROR
            END

            UPDATE {0}.{1}.Prop_Imovel
                SET SequencialImovel           =@SequencialImovel
                   ,CodigoMunicipio            =@CodigoMunicipio
                   ,IndicaEspolio              =@IndicaEspolio
                   ,CodigoProjeto              =@CodigoProjeto
                   ,Localizacao                =@Localizacao         
                   ,Distrito                   =@Distrito
                   ,Comarca                    =@Comarca
                   ,NomeRegiao                 =@NomeRegiao
                   ,IdentificacaoFundiaria     =@IdentificacaoFundiaria 
                   ,Coordenadas                =@Coordenadas
                   ,AreaTotal                  =@AreaTotal
                   ,AreaAtingida               =@AreaAtingida
                   ,ProcedenciaAquisicao       =@ProcedenciaAquisicao
                   ,Observacao                 =@Observacao
                   ,NomeInventariante          =@NomeInventariante
                   ,JuizoEspolio               =@JuizoEspolio
                   ,CartorioEspolio            =@CartorioEspolio
                   ,NomeAdvogadoEspolio        =@NomeAdvogadoEspolio
                   ,EnderecoAdvogadoEspolio    =@EnderecoAdvogadoEspolio
                   ,TelefoneAdvogadoEspolio    =@TelefoneAdvogadoEspolio 
                   ,DadosUltimaDeclaracaoITR   =@DadosUltimaDeclaracaoITR
                   ,CodigoAntigo               =@SequencialImovel
                   ,CodigoNovo                 =@CodigoNovo
            WHERE CodigoImovel = @CodigoImovel
       END TRY
        BEGIN CATCH
            SET @Erro = ERROR_NUMBER()
            SET @MensagemErro = ERROR_MESSAGE()
        END CATCH

        IF @Erro = 0
        BEGIN
            COMMIT
        END
        ELSE
        BEGIN
            ROLLBACK
        END
        SELECT @CodigoImovel AS CodigoImovel,
        @Erro AS CodigoErro, 
        @MensagemErro AS MensagemErro", cDados.getDbName(), cDados.getDbOwner()
            ,/*{2}*/txtSeq.Text
            ,/*{3}*/txtLocalizacao.Text
            ,/*{4}*/(ddlMunicipio.Value != null) ? ddlMunicipio.Value.ToString() : "null"
            ,/*{5}*/txtDistrito.Text
            ,/*{6}*/txtComarca.Text
            ,/*{7}*/txtRegiao.Text
            ,/*{8}*/txtIdFundiaria.Text
            ,/*{9}*/txtCoordenadas.Text
            ,/*{10}*/decimal.Parse(speAreaTotal.Text.Replace(".", "").Replace(",", "."))
            ,/*{11}*/ decimal.Parse(speAreaAtingida.Text.Replace(".", "").Replace(",", "."))
            ,/*{12}*/txtProcedenciaAquisicao.Text
            ,/*{13}*/memoObservacoes.Text
            ,/*{14}*/ddlSpolio.Value.ToString()
            ,/*{15}*/txtNomeInventariante.Text
            ,/*{16}*/txtJuizo.Text
            ,/*{17}*/txtCartorio.Text
            ,/*{18}*/txtAdvogado.Text
            ,/*{19}*/txtEndereco.Text
            ,/*{20}*/txtFone.Text
            ,/*{21}*/CodigoProjeto
            ,/*{22}*/txtDadosUltimaDeclaracaoITR.Text
            ,/*{23}*/int.Parse(Session["codigoImovel"].ToString())
            ,/*{23}*/txtSeq1.Text);
        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            string mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("Violação da restrição UNIQUE KEY 'UQ_Prop_Imovel_SequencialImovel'"))
                    throw new Exception("Já existe um imóvel com essa sequência.");
                else
                    throw new Exception(mensagemErro.ToString());
            }
            CodigoImovel = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoImovel"]);
        }

        #endregion
    }

    protected void SalvaNovaPropriedade()
    {
        #region Comando SQL

        string comandoSql = string.Format(@"
        BEGIN TRAN
        BEGIN TRY

        DECLARE @Erro INT
        DECLARE @MensagemErro nvarchar(2048)
        DECLARE @NovoCodigoImovel INT
        SET @Erro = 0
    
    DECLARE @SequencialImovel int
           ,@Localizacao varchar(250)
           ,@CodigoMunicipio int
           ,@Distrito varchar(250)
           ,@Comarca varchar(250)
           ,@NomeRegiao varchar(250)
           ,@IdentificacaoFundiaria varchar(250)
           ,@Coordenadas varchar(50)
           ,@AreaTotal decimal(25,6)
           ,@AreaAtingida decimal(25,6)
           ,@ProcedenciaAquisicao varchar(250)
           ,@Observacao varchar(500)
           ,@IndicaEspolio char(1)
           ,@NomeInventariante varchar(150)
           ,@JuizoEspolio varchar(250)
           ,@CartorioEspolio varchar(250)
           ,@NomeAdvogadoEspolio varchar(150)
           ,@EnderecoAdvogadoEspolio varchar(500)
           ,@TelefoneAdvogadoEspolio varchar(50)
           ,@CodigoProjeto int
           ,@DadosUltimaDeclaracaoITR varchar(250)
           ,@CodigoAntigo varchar(50)
           ,@CodigoNovo varchar(50)

           set @SequencialImovel = {2}
           set @Localizacao  = '{3}'
           set @CodigoMunicipio  = {4}
           set @Distrito  = '{5}'
           set @Comarca  = '{6}'
           set @NomeRegiao  = '{7}'
           set @IdentificacaoFundiaria  = '{8}'
           set @Coordenadas  = '{9}'
           set @AreaTotal  = {10}
           set @AreaAtingida  = {11}
           set @ProcedenciaAquisicao  = '{12}'
           set @Observacao  = '{13}'
           set @IndicaEspolio  = '{14}'
           set @NomeInventariante  = '{15}'
           set @JuizoEspolio  = '{16}'
           set @CartorioEspolio  = '{17}'
           set @NomeAdvogadoEspolio  = '{18}'
           set @EnderecoAdvogadoEspolio  = '{19}'
           set @TelefoneAdvogadoEspolio  = '{20}'
           set @CodigoProjeto  = {21}
           set @DadosUltimaDeclaracaoITR = '{22}'
           set @CodigoAntigo = '{2}'
           set @CodigoNovo = '{23}'

INSERT INTO {0}.{1}.Prop_Imovel
           (SequencialImovel        ,Localizacao              ,CodigoMunicipio          ,Distrito
           ,Comarca                 ,NomeRegiao               ,IdentificacaoFundiaria   ,Coordenadas
           ,AreaTotal               ,AreaAtingida             ,ProcedenciaAquisicao     ,Observacao
           ,IndicaEspolio           ,NomeInventariante        ,JuizoEspolio             ,CartorioEspolio
           ,NomeAdvogadoEspolio     ,EnderecoAdvogadoEspolio  ,TelefoneAdvogadoEspolio  ,CodigoProjeto
           ,DadosUltimaDeclaracaoITR,CodigoAntigo             ,CodigoNovo)
     VALUES
           (@SequencialImovel        ,@Localizacao             ,@CodigoMunicipio         ,@Distrito 
           ,@Comarca                 ,@NomeRegiao              ,@IdentificacaoFundiaria  ,@Coordenadas 
           ,@AreaTotal               ,@AreaAtingida            ,@ProcedenciaAquisicao    ,@Observacao 
           ,@IndicaEspolio           ,@NomeInventariante       ,@JuizoEspolio            ,@CartorioEspolio 
           ,@NomeAdvogadoEspolio     ,@EnderecoAdvogadoEspolio ,@TelefoneAdvogadoEspolio ,@CodigoProjeto 
           ,@DadosUltimaDeclaracaoITR,@CodigoAntigo            ,@CodigoNovo)
 SET @Erro = @Erro + @@ERROR

set @NovoCodigoImovel  = scope_identity()   
SELECT @NovoCodigoImovel AS resposta
 END TRY
BEGIN CATCH
    SET @Erro = ERROR_NUMBER()
    SET @MensagemErro = ERROR_MESSAGE()
END CATCH
IF @Erro = 0
BEGIN
    COMMIT
END
ELSE
BEGIN
    ROLLBACK
END
 SELECT @NovoCodigoImovel AS resposta,
        @Erro AS CodigoErro, 
        @MensagemErro AS MensagemErro"
            , cDados.getDbName()
            , cDados.getDbOwner()
            , txtSeq.Text
            , txtLocalizacao.Text
            , 1//fica 1 por enquanto
            , txtDistrito.Text
            , txtComarca.Text
            , txtRegiao.Text
            , txtIdFundiaria.Text
            , txtCoordenadas.Text
            , decimal.Parse(speAreaTotal.Text.Replace(".", "").Replace(",", "."))
            , decimal.Parse(speAreaAtingida.Text.Replace(".", "").Replace(",", "."))
            , txtProcedenciaAquisicao.Text
            , memoObservacoes.Text
            , ddlSpolio.Value.ToString()
            , txtNomeInventariante.Text
            , txtJuizo.Text
            , txtCartorio.Text
            , txtAdvogado.Text
            , txtEndereco.Text
            , txtFone.Text
            , CodigoProjeto
            , txtDadosUltimaDeclaracaoITR.Text
            , txtSeq1.Text);
        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            int naoInteiro = 0;
            string resposta = ds.Tables[0].Rows[0]["resposta"].ToString();
            if (int.TryParse(resposta, out naoInteiro) == false)
            {
                if (resposta != "")
                {
                    throw new Exception(resposta.ToString());
                }
            }
            else
            {
                Session["codigoImovel"] = Convert.ToInt32(ds.Tables[0].Rows[0]["resposta"]);
            }
        }
    }

    protected void gvDocRegistradoImovel_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        


    }
    protected void gvAreaAverbada_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        switch (e.ButtonType)
        {
            case ColumnCommandButtonType.Delete:
                if (!podeExcluir)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
                else if (grid == gvDocRegistradoImovel)
                {
                    SqlDataSource[] sources = new SqlDataSource[] { sdsAreaAverbada };
                    string nomeColunaCodigo = "CodigoAreaAverbada";
                    string codigoAcao = gvDocRegistradoImovel.GetDataRow(e.VisibleIndex)[nomeColunaCodigo].ToString();
                }
                break;
            case ColumnCommandButtonType.Edit:
                if (!podeEditar)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
                break;
            default:
                break;
        }
    }
    protected void gvAreaAverbada_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }
    protected void gvAreaAverbada_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        sdsAreaAverbada.DataBind();
    }

    protected void ddlMunicipio_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string comandoSQL = string.Format(@"SELECT CodigoMunicipio
      ,NomeMunicipio
      ,SiglaUF
  FROM {0}.{1}.Municipio
where SiglaUF = '{2}'", cDados.getDbName(), cDados.getDbOwner(), ddlUF.SelectedItem.Value);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ddlMunicipio.DataSource = ds.Tables[0];
        ddlMunicipio.TextField = "NomeMunicipio";
        ddlMunicipio.ValueField = "CodigoMunicipio";
        ddlMunicipio.DataBind();
    }

    protected void gvProprietarioOcupante_CellEditorInitialize1(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "IndicaProprietario")
        {
            if (e.Value.ToString() == "S")
            {
                e.Editor.Value = "Sim";
            }
            else
            {
                e.Editor.Value = "Não";
            }
        }
    }
    protected void gvProprietarioOcupante_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        gvProprietarioOcupante.DataBind();
    }
}