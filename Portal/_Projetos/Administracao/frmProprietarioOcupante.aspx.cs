using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_Administracao_frmProprietarioOcupante : System.Web.UI.Page
{
    private int _codigoProjeto;
    /// <summary>
    /// Indica se está sendo criado um novo projeto (codigoProjeto == -1).
    /// </summary>
    public string alturaDivRolagem = "";

    public bool IndicaNovoProjeto
    {
        get
        {
            return _codigoProjeto == -1;
        }
    }

    public int _codigoOcupante;
    //{
    //    get { return _codigoOcupante; }
    //    set
    //    {
    //        //Session["codigoOcupante"] = value;
    //        _codigoOcupante = value;
    //    }
    //}

    public bool IndicaNovoOcupante
    {
        get
        {
            return _codigoOcupante == -1;
        }
    }

    private dados cDados;

    public int codigoProjeto = -1;
    public int codigoPessoaImovel = -1;


    private bool podeIncluir;
    private string _connectionString;
    /// <summary>
    /// String de conexão para acesso a dados da tela.
    /// </summary>
    public string ConnectionString
    {
        get { return _connectionString; }
        private set
        {

            sdsUFPessoa.ConnectionString =
            sdsUFConjuge.ConnectionString =
            sdsMunicipioPessoa.ConnectionString =
            sdsMunicipioConjuge.ConnectionString =
            sdsDadosFormulario.ConnectionString =
            sdsDocumentoOcupanteImovel.ConnectionString =
            sdsUFResidenciaPessoa.ConnectionString =
            sdsMunicipioResidenciaPessoa.ConnectionString =
            _connectionString = value;
        }
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        ConnectionString = cDados.classeDados.getStringConexao();
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
         
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        if (Request.QueryString["CP"] != null && (Request.QueryString["CP"] + "" != ""))
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
            Session["codigoProjeto"] = codigoProjeto;
        }
        if (Request.QueryString["CPI"] != null && (Request.QueryString["CPI"] + "" != ""))
        {
            codigoPessoaImovel = int.Parse(Request.QueryString["CPI"].ToString());
            Session["codigoPessoaImovel"] = codigoPessoaImovel;
            _codigoOcupante = codigoPessoaImovel;
        }
        if (!Page.IsPostBack && codigoProjeto != -1)
        {
            carregaDadosFormulario();
        }
        _codigoOcupante = int.Parse((Session["codigoOcupante"] != null) ? Session["codigoOcupante"].ToString() : "-1");
        podeIncluir = codigoPessoaImovel != -1;

       string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        int auxiliar = (int)(altura - (0.25) * altura);
        alturaDivRolagem = auxiliar + "px";
        
        cDados.aplicaEstiloVisual(this.Page);
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/frmPropriedades.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/frmProprietarioOcupante.js""></script>"));
        
        Header.Controls.Add(cDados.getLiteral(@"<title>Cadastro de Proprietários e Ocupantes</title>"));
    }

    public void carregaMunicipios(string codMunicipio, ASPxComboBox ddlMunicipioX, ASPxComboBox ddlUFX)
    {
        string comandoSQL = string.Format(@"
            SELECT CodigoMunicipio
                   ,NomeMunicipio
                   ,SiglaUF
              FROM {0}.{1}.Municipio
             WHERE CodigoMunicipio = {2}", cDados.getDbName(), cDados.getDbOwner(), codMunicipio);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ddlMunicipioX.DataSource = ds.Tables[0];
        ddlMunicipioX.TextField = "NomeMunicipio";
        ddlMunicipioX.ValueField = "CodigoMunicipio";
        ddlMunicipioX.DataBind();
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlMunicipioX.Value = codMunicipio;
            ddlMunicipioX.Text = ds.Tables[0].Rows[0]["NomeMunicipio"].ToString();
            ddlUFX.Value = ds.Tables[0].Rows[0]["SiglaUF"].ToString();
            ddlUFX.Text = ds.Tables[0].Rows[0]["SiglaUF"].ToString();

        }
    }

    public void habilitaCamposConjuge(bool sim)
    {
        txtNomeConjuge.ClientEnabled = sim;
        dtNascimentoConjuge.ClientEnabled = sim;
        ddlNacionalidadeConjuge.ClientEnabled = sim;
        txtPaisConjuge.ClientEnabled = sim;
        ddlUFConjuge.ClientEnabled = sim;
        ddlMunicipioConjuge.ClientEnabled = sim;
        txtProfissaoConjuge.ClientEnabled = sim;
        txtCPFConjuge.ClientEnabled = sim;
        //ckbSabeAssinarConjuge.SetEnabled(sim);
        ddlTipoDocumentoConjuge.ClientEnabled = sim;
        txtNumeroDocumentoConjuge.ClientEnabled = sim;
        txtOrgaoExpeditorConjuge.ClientEnabled = sim;
        txtNomePaiConjuge.ClientEnabled = sim;
        txtNomeMaeConjuge.ClientEnabled = sim;
        ddlEstadoCivilConjuge.ClientEnabled = sim;
        txtCertidaoConjuge.ClientEnabled = sim;
        dtEmissaoConjuge.ClientEnabled = sim;
        txtLivroConjuge.ClientEnabled = sim;
        txtFolhasConjuge.ClientEnabled = sim;
        txtCartorioConjuge.ClientEnabled = sim;
    }

    public void carregaDadosFormulario()
    {

        sdsDadosFormulario.DataBind();
        DataView dv = (DataView)sdsDadosFormulario.Select(new DataSourceSelectArguments());

        if (dv.Count > 0)
        {
            DataRowView row = dv[0];
            //row["CodigoPessoaImovel"];
            //row["CodigoProjeto"];

            //trata os tres combos de municipio
            string codmunicipio = (row["CodigoMunicipioNaturalidadePessoa"].ToString() == "") ? "-1" : row["CodigoMunicipioNaturalidadePessoa"].ToString();
            carregaMunicipios(codmunicipio, ddlMunicipioPessoa, ddlUFPessoa);
            
            codmunicipio = (row["CodigoMunicipioResidenciaPessoa"].ToString() == "") ? "-1" : row["CodigoMunicipioResidenciaPessoa"].ToString();
            carregaMunicipios(codmunicipio, ddlMunicipioResidenciaPessoa, ddlUFResidenciaPessoa);
            
            codmunicipio = (row["CodigoMunicipioNaturalidadeConjuge"].ToString() == "") ? "-1" : row["CodigoMunicipioNaturalidadeConjuge"].ToString();
            carregaMunicipios(codmunicipio, ddlMunicipioConjuge, ddlUFConjuge);
            
            txtNome.Text = row["NomePessoa"].ToString();
            dtNascimento.Value = row["DataNascimentoPessoa"];
            ddlIndicaNacionalidade.Value = row["IndicaNacionalidadePessoa"];
            if ((ddlIndicaNacionalidade.Value != null) && ddlIndicaNacionalidade.Value.ToString().Trim() == "E")
            {
                //procede com as validações apenas se tiver habilitado, senao deixa como está
                txtPais.ClientEnabled = true;
                ddlUFPessoa.ClientEnabled = false;
                ddlMunicipioPessoa.ClientEnabled = false;
            }
            else
            {

                    txtPais.ClientEnabled = false;
                    ddlUFPessoa.ClientEnabled = true;
                    ddlMunicipioPessoa.ClientEnabled = true;
                
            }
            
            
            txtPais.Text = row["NomePaisPessoa"].ToString();


            txtProfissao.Text = row["ProfissaoPessoa"].ToString();
            txtCPF.Text = row["NumeroCPFCNPJPessoa"].ToString();
            ckbSabeAssinar.Checked = (row["IndicaPessoaSabeAssinar"].ToString() == "S");
            ddlTipoDocumento.Value = row["TipoDocumentoPessoa"];
            txtNumeroDocumento.Value = row["NumeroDocumentoPessoa"];
            txtOrgaoExpeditor.Value = row["OrgaoExpedidorDocumentoPessoa"];
            txtNomePai.Value = row["NomePaiPessoa"];
            txtNomeMae.Value = row["NomeMaePessoa"];
            rblIndicaEstadoCivilPessoa.Value = row["IndicaEstadoCivilPessoa"].ToString().Trim();


            string valor = (rblIndicaEstadoCivilPessoa.Value != null) ? rblIndicaEstadoCivilPessoa.Value.ToString().Trim() : "";
            if (valor == "S" || valor == "C")
            {
                txtSoltCasCertidao.ClientEnabled = true;
                txtSoltCasFolhas.ClientEnabled = true;
                txtSoltCasLivro.ClientEnabled = true;
                dtSoltCasEmissao.ClientEnabled = true;
                txtSoltCasCartorio.ClientEnabled = true;
                if (valor == "S")
                {
                    //se for solteiro
                    //pnConjuge.Enabled = false;
                    rblRegimeSeparacaoBens.ClientEnabled = false;
                }
                else
                {
                    //se for casado
                    //pnConjuge.Enabled = true;
                    rblRegimeSeparacaoBens.ClientEnabled = true;

                    //escritura registro do pacto antenupcial
                    ddlIndicaEscRegPactoAnteNupcialPessoa.ClientEnabled = true;
                    txtNumeroIndEscRegPacAntNupPes.ClientEnabled = true;
                    dtEmissaoIndEscRegPacAntNupPes.ClientEnabled = true;
                    txtLivroIndEscRegPacAntNupPes.ClientEnabled = true;
                    txtFolhasIndEscRegPacAntNupPes.ClientEnabled = true;
                    txtCartorioIndEscRegPacAntNupPes.ClientEnabled = true;
                    habilitaCamposConjuge(true);

                }

            }
            if (valor == "SJ" || valor == "D")
            {
                txtSepDivSentencaAutosN.ClientEnabled = true;
                dtSepDivData.ClientEnabled = true;
                txtSepDivJuizo.ClientEnabled = true;
            }
            if (valor == "UE")
            {
                dtUniaoEstavelDesde.ClientEnabled = true;
                rblRegimeSeparacaoBens.ClientEnabled = true;

                ddlIndicaEscRegPactoAnteNupcialPessoa.ClientEnabled = true;
                txtNumeroIndEscRegPacAntNupPes.ClientEnabled = true;
                dtEmissaoIndEscRegPacAntNupPes.ClientEnabled = true;
                txtLivroIndEscRegPacAntNupPes.ClientEnabled = true;
                txtFolhasIndEscRegPacAntNupPes.ClientEnabled = true;
                txtCartorioIndEscRegPacAntNupPes.ClientEnabled = true;
                habilitaCamposConjuge(true);
            }
            if (valor == "V")
            {
                txtViuvoNumeroCertObito.ClientEnabled = true;
                txtViuvoFolhas.ClientEnabled = true;
                txtViuvoLivro.ClientEnabled = true;
                dtViuvoEmissao.ClientEnabled = true;
                txtViuvoCartorio.ClientEnabled = true;
            }

            rblRegimeSeparacaoBens.Value = row["RegimeSeparacaoBensPessoa"].ToString().Trim();

            txtSoltCasCertidao.Value = row["CertidaoEstadoCivilPessoa"];
            txtSoltCasLivro.Value = row["LivroCertidaoEstadoCivilPessoa"];
            txtSoltCasFolhas.Value = row["FolhaCertidaoEstadoCivilPessoa"];
            dtSoltCasEmissao.Value = row["EmissaoCertidaoEstadoCivilPessoa"];
            txtSoltCasCartorio.Value = row["NomeCartorioCertidaoEstadoCivilPessoa"];
            txtSepDivSentencaAutosN.Value = row["AutosSeparacaoPessoa"];
            dtSepDivData.Value = row["DataSeparacaoPessoa"];
            txtSepDivJuizo.Value = row["JuizoSeparacaoPessoa"];
            dtUniaoEstavelDesde.Value = row["DataUniaoEstavel"];
            txtViuvoNumeroCertObito.Value = row["CertidaoViuvoPessoa"];
            txtViuvoFolhas.Value = row["FolhaCertidaoViuvoPessoa"];
            txtViuvoLivro.Value = row["LivroCertidaoViuvoPessoa"];
            ddlIndicaEscRegPactoAnteNupcialPessoa.Value = row["IndicaEscrituraRegistroPactoAnteNupcialPessoa"];
            txtNumeroIndEscRegPacAntNupPes.Value = row["NumeroPactoAnteNupcialPessoa"];
            txtFolhasIndEscRegPacAntNupPes.Value = row["FolhaRegistroPactoAnteNupcialPessoa"];
            txtLivroIndEscRegPacAntNupPes.Value = row["LivroRegistroPactoAnteNupcialPessoa"];
            txtCartorioIndEscRegPacAntNupPes.Value = row["NomeCartorioRegistroPactoAnteNupcialPessoa"];
            txtEnderecoResidencial.Value = row["EnderecoResidencialPessoa"];
            txtNumero.Value = row["NumeroEnderecoResidencialPessoa"];
            txtFone.Value = row["TelefonePessoa"];
            speTempoOcupacaoAnos.Value = row["TempoOcupacaoPessoa"];
            txtNomeConjuge.Value = row["NomeConjuge"];
            dtNascimentoConjuge.Value = row["DataNascimentoConjuge"];
            ddlNacionalidadeConjuge.Value = row["IndicaNacionalidadeConjuge"];
            if ((ddlNacionalidadeConjuge.ClientEnabled == true) && (ddlNacionalidadeConjuge.Value != null) && ddlNacionalidadeConjuge.Value.ToString().Trim() == "E")
            {
                txtPaisConjuge.ClientEnabled = true;
                ddlUFConjuge.ClientEnabled = false;
                ddlMunicipioConjuge.ClientEnabled = false;
            }
            else
            {
                if (ddlNacionalidadeConjuge.ClientEnabled == true)
                {
                    txtPaisConjuge.ClientEnabled = false;
                    ddlUFConjuge.ClientEnabled = true;
                    ddlMunicipioConjuge.ClientEnabled = true;
                }
                
            }
            txtPaisConjuge.Value = row["NomePaisConjuge"];

            txtProfissaoConjuge.Value = row["ProfissaoConjuge"];
            txtCPFConjuge.Value = row["NumeroCPFCNPJConjuge"];
            ckbSabeAssinarConjuge.Value = row["IndicaConjugeSabeAssinar"];
            ddlTipoDocumentoConjuge.Value = row["TipoDocumentoConjuge"];
            txtNumeroDocumentoConjuge.Value = row["NumeroDocumentoConjuge"];
            txtOrgaoExpeditorConjuge.Value = row["OrgaoExpedidorDocumentoConjuge"];
            txtNomePaiConjuge.Value = row["NomePaiConjuge"];
            txtNomeMaeConjuge.Value = row["NomeMaeConjuge"];
            ddlEstadoCivilConjuge.Value = row["IndicaEstadoCivilConjuge"];
            txtCertidaoConjuge.Value = row["CertidaoEstadoCivilConjuge"];
            txtLivroConjuge.Value = row["LivroCertidaoEstadoCivilConjuge"];
            txtFolhasConjuge.Value = row["FolhaCertidaoEstadoCivilConjuge"];
            dtEmissaoConjuge.Value = row["EmissaoCertidaoEstadoCivilConjuge"];
            txtCartorioConjuge.Value = row["NomeCartorioCertidaoEstadoCivilConjuge"];
            ckbIndicaProprietario.Checked = (row["IndicaProprietario"] != null && row["IndicaProprietario"].ToString() == "S");
            ckbIndicaOcupante.Checked = (row["IndicaOcupante"] !=null  && row["IndicaOcupante"].ToString() == "S");
            txtViuvoCartorio.Value = row["NomeCartorioCertidaoViuvoPessoa"];
            dtViuvoEmissao.Value = row["EmissaoCertidaoViuvoPessoa"];
            dtEmissaoIndEscRegPacAntNupPes.Value = row["EmissaoPactoAnteNupcialPessoal"];
        }
    }


    protected string ObtemBotaoInclusaoRegistro(string nomeGrid, string assuntoGrid)
    {
        string tituloBotaoDesabilitado = string.Format("Clique em 'Salvar' para poder registrar as informações de [{0}]", assuntoGrid);
        string htmlBotaoDesabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", tituloBotaoDesabilitado);
        string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""{0}.AddNewRow();"" style=""cursor: pointer;""/>", nomeGrid);

        string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
            , (podeIncluir) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);

        return strRetorno;
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string tipoOperacao;
        if (Session["codigoPessoaImovel"].ToString() == "-1")
        {
            incluiNovoOcupante();
            tipoOperacao = "I";
        }
        else
        {
            atualizaOcupanteAtual();
            tipoOperacao = "U";
        }

        e.Result = tipoOperacao + codigoProjeto.ToString() + "p" + codigoPessoaImovel;
    }

    protected void incluiNovoOcupante()
    {
        #region Comando SQL

        string comandoSql = string.Format(@"
        DECLARE @Erro INT
        DECLARE @MensagemErro nvarchar(2048)
        SET @Erro = 0
        BEGIN TRAN
        BEGIN TRY
            DECLARE
            @CodigoPessoaImovel as int
           ,@CodigoProjeto as int           
           ,@NomePessoa as varchar(150)           
           ,@DataNascimentoPessoa as datetime
           ,@IndicaNacionalidadePessoa as char(1)
           ,@NomePaisPessoa varchar(150)
           ,@CodigoMunicipioNaturalidadePessoa int
           ,@ProfissaoPessoa varchar(250)
           ,@NumeroCPFCNPJPessoa varchar(15)
           ,@IndicaPessoaSabeAssinar char(1)
           ,@TipoDocumentoPessoa varchar(50)
           ,@NumeroDocumentoPessoa varchar(50)
           ,@OrgaoExpedidorDocumentoPessoa varchar(50)
           ,@NomePaiPessoa varchar(150)
           ,@NomeMaePessoa varchar(150)
           ,@IndicaEstadoCivilPessoa char(2)
           ,@RegimeSeparacaoBensPessoa char(3)
           ,@CertidaoEstadoCivilPessoa varchar(50)
           ,@LivroCertidaoEstadoCivilPessoa varchar(50)
           ,@FolhaCertidaoEstadoCivilPessoa varchar(50)
           ,@EmissaoCertidaoEstadoCivilPessoa datetime
           ,@NomeCartorioCertidaoEstadoCivilPessoa varchar(250)
           ,@AutosSeparacaoPessoa varchar(50)
           ,@DataSeparacaoPessoa datetime
           ,@JuizoSeparacaoPessoa varchar(250)
           ,@DataUniaoEstavel datetime
           ,@CertidaoViuvoPessoa varchar(50)
           ,@FolhaCertidaoViuvoPessoa varchar(50)
           ,@LivroCertidaoViuvoPessoa varchar(50)
           ,@IndicaEscrituraRegistroPactoAnteNupcialPessoa char(1)
           ,@NumeroPactoAnteNupcialPessoa varchar(50)
           ,@FolhaRegistroPactoAnteNupcialPessoa varchar(50)
           ,@LivroRegistroPactoAnteNupcialPessoa varchar(50)
           ,@NomeCartorioRegistroPactoAnteNupcialPessoa varchar(250)
           ,@EnderecoResidencialPessoa varchar(250)
           ,@NumeroEnderecoResidencialPessoa varchar(50)
           ,@CodigoMunicipioResidenciaPessoa int
           ,@TelefonePessoa varchar(50)
           ,@TempoOcupacaoPessoa decimal(5,2)
           ,@NomeConjuge varchar(150)
           ,@DataNascimentoConjuge datetime
           ,@IndicaNacionalidadeConjuge char(1)
           ,@NomePaisConjuge varchar(150)
           ,@CodigoMunicipioNaturalidadeConjuge int
           ,@ProfissaoConjuge varchar(250)
           ,@NumeroCPFCNPJConjuge varchar(15)
           ,@IndicaConjugeSabeAssinar char(1)
           ,@TipoDocumentoConjuge varchar(50)
           ,@NumeroDocumentoConjuge varchar(50)
           ,@OrgaoExpedidorDocumentoConjuge varchar(50)
           ,@NomePaiConjuge varchar(150)
           ,@NomeMaeConjuge varchar(150)
           ,@IndicaEstadoCivilConjuge char(2)
           ,@CertidaoEstadoCivilConjuge varchar(50)
           ,@LivroCertidaoEstadoCivilConjuge varchar(50)
           ,@FolhaCertidaoEstadoCivilConjuge varchar(50)
           ,@EmissaoCertidaoEstadoCivilConjuge datetime
           ,@NomeCartorioCertidaoEstadoCivilConjuge  varchar(250)
           ,@IndicaProprietario char(1)
           ,@IndicaOcupante as char(1)
           ,@NomeCartorioCertidaoViuvoPessoa varchar(250)
           ,@EmissaoCertidaoViuvoPessoa datetime
           ,@EmissaoPactoAnteNupcialPessoal datetime
          
           SET @CodigoProjeto ={2}--int
           SET @NomePessoa ='{3}'
           SET @DataNascimentoPessoa ={4}
           SET @IndicaNacionalidadePessoa ='{5}'
           SET @NomePaisPessoa='{6}'
           SET @CodigoMunicipioNaturalidadePessoa = ISNULL({7},null)
           SET @ProfissaoPessoa ='{8}'
           SET @NumeroCPFCNPJPessoa ='{9}'
           SET @IndicaPessoaSabeAssinar ='{10}'
           SET @TipoDocumentoPessoa ='{11}'
           SET @NumeroDocumentoPessoa ='{12}'
           SET @OrgaoExpedidorDocumentoPessoa ='{13}'
           SET @NomePaiPessoa ='{14}'
           SET @NomeMaePessoa ='{15}'
           SET @IndicaEstadoCivilPessoa ='{16}'
           SET @RegimeSeparacaoBensPessoa='{17}'
           SET @CertidaoEstadoCivilPessoa ='{18}'
           SET @LivroCertidaoEstadoCivilPessoa ='{19}'
           SET @FolhaCertidaoEstadoCivilPessoa ='{20}'
           SET @EmissaoCertidaoEstadoCivilPessoa ={21}
           SET @NomeCartorioCertidaoEstadoCivilPessoa ='{22}'
           SET @AutosSeparacaoPessoa ='{23}'
           SET @DataSeparacaoPessoa ={24}
           SET @JuizoSeparacaoPessoa ='{25}'
           SET @DataUniaoEstavel ={26}
           SET @CertidaoViuvoPessoa ='{27}'
           SET @FolhaCertidaoViuvoPessoa ='{28}'
           SET @LivroCertidaoViuvoPessoa ='{29}'
           SET @IndicaEscrituraRegistroPactoAnteNupcialPessoa ='{30}'
           SET @NumeroPactoAnteNupcialPessoa ='{31}'
           SET @FolhaRegistroPactoAnteNupcialPessoa ='{32}'
           SET @LivroRegistroPactoAnteNupcialPessoa ='{33}'
           SET @NomeCartorioRegistroPactoAnteNupcialPessoa ='{34}'
           SET @EnderecoResidencialPessoa ='{35}'
           SET @NumeroEnderecoResidencialPessoa ='{36}'
           SET @CodigoMunicipioResidenciaPessoa =ISNULL({37},null)
           SET @TelefonePessoa ='{38}'
           SET @TempoOcupacaoPessoa ={39}
           SET @NomeConjuge ='{40}'
           SET @DataNascimentoConjuge ={41}
           SET @IndicaNacionalidadeConjuge ='{42}'
           SET @NomePaisConjuge ='{43}'
           SET @CodigoMunicipioNaturalidadeConjuge = ISNULL({44},NULL)
           SET @ProfissaoConjuge ='{45}'
           SET @NumeroCPFCNPJConjuge ='{46}'
           SET @IndicaConjugeSabeAssinar ='{47}'
           SET @TipoDocumentoConjuge ='{48}'
           SET @NumeroDocumentoConjuge ='{49}'
           SET @OrgaoExpedidorDocumentoConjuge ='{50}'
           SET @NomePaiConjuge ='{51}'
           SET @NomeMaeConjuge ='{52}'
           SET @IndicaEstadoCivilConjuge ='{53}'
           SET @CertidaoEstadoCivilConjuge ='{54}'
           SET @LivroCertidaoEstadoCivilConjuge ='{55}'
           SET @FolhaCertidaoEstadoCivilConjuge ='{56}'
           SET @EmissaoCertidaoEstadoCivilConjuge ={57}
           SET @NomeCartorioCertidaoEstadoCivilConjuge  ='{58}'
           SET @IndicaProprietario ='{59}'
           SET @IndicaOcupante ='{60}'
           SET @NomeCartorioCertidaoViuvoPessoa = '{61}'
           SET @EmissaoCertidaoViuvoPessoa = {62}
           SET @EmissaoPactoAnteNupcialPessoal = {63}

INSERT INTO {0}.{1}.Prop_PessoaImovel
           (CodigoProjeto                                ,NomePessoa                       ,DataNascimentoPessoa                ,IndicaNacionalidadePessoa
           ,NomePaisPessoa                               ,CodigoMunicipioNaturalidadePessoa,ProfissaoPessoa                     ,NumeroCPFCNPJPessoa
           ,IndicaPessoaSabeAssinar                      ,TipoDocumentoPessoa              ,NumeroDocumentoPessoa               ,OrgaoExpedidorDocumentoPessoa
           ,NomePaiPessoa                                ,NomeMaePessoa                    ,IndicaEstadoCivilPessoa             ,RegimeSeparacaoBensPessoa
           ,CertidaoEstadoCivilPessoa                    ,LivroCertidaoEstadoCivilPessoa   ,FolhaCertidaoEstadoCivilPessoa      ,EmissaoCertidaoEstadoCivilPessoa
           ,NomeCartorioCertidaoEstadoCivilPessoa        ,AutosSeparacaoPessoa             ,DataSeparacaoPessoa                 ,JuizoSeparacaoPessoa
           ,DataUniaoEstavel                             ,CertidaoViuvoPessoa              ,FolhaCertidaoViuvoPessoa            ,LivroCertidaoViuvoPessoa
           ,IndicaEscrituraRegistroPactoAnteNupcialPessoa,NumeroPactoAnteNupcialPessoa     ,FolhaRegistroPactoAnteNupcialPessoa ,LivroRegistroPactoAnteNupcialPessoa
           ,NomeCartorioRegistroPactoAnteNupcialPessoa   ,EnderecoResidencialPessoa        ,NumeroEnderecoResidencialPessoa     ,CodigoMunicipioResidenciaPessoa
           ,TelefonePessoa                               ,TempoOcupacaoPessoa              ,NomeConjuge                         ,DataNascimentoConjuge
           ,IndicaNacionalidadeConjuge                   ,NomePaisConjuge                  ,CodigoMunicipioNaturalidadeConjuge  ,ProfissaoConjuge
           ,NumeroCPFCNPJConjuge                         ,IndicaConjugeSabeAssinar         ,TipoDocumentoConjuge                ,NumeroDocumentoConjuge
           ,OrgaoExpedidorDocumentoConjuge               ,NomePaiConjuge                   ,NomeMaeConjuge                      ,IndicaEstadoCivilConjuge
           ,CertidaoEstadoCivilConjuge                   ,LivroCertidaoEstadoCivilConjuge  ,FolhaCertidaoEstadoCivilConjuge     ,EmissaoCertidaoEstadoCivilConjuge
           ,NomeCartorioCertidaoEstadoCivilConjuge       ,IndicaProprietario               ,IndicaOcupante                      ,NomeCartorioCertidaoViuvoPessoa
           ,EmissaoCertidaoViuvoPessoa                   ,EmissaoPactoAnteNupcialPessoal)
     
     VALUES(@CodigoProjeto                                ,@NomePessoa                       ,@DataNascimentoPessoa             ,@IndicaNacionalidadePessoa
           ,@NomePaisPessoa                               ,@CodigoMunicipioNaturalidadePessoa,@ProfissaoPessoa                  ,@NumeroCPFCNPJPessoa
           ,@IndicaPessoaSabeAssinar                      ,@TipoDocumentoPessoa              ,@NumeroDocumentoPessoa            ,@OrgaoExpedidorDocumentoPessoa
           ,@NomePaiPessoa                                ,@NomeMaePessoa                    ,@IndicaEstadoCivilPessoa          ,@RegimeSeparacaoBensPessoa
           ,@CertidaoEstadoCivilPessoa                    ,@LivroCertidaoEstadoCivilPessoa   ,@FolhaCertidaoEstadoCivilPessoa   ,@EmissaoCertidaoEstadoCivilPessoa
           ,@NomeCartorioCertidaoEstadoCivilPessoa        ,@AutosSeparacaoPessoa             ,@DataSeparacaoPessoa              ,@JuizoSeparacaoPessoa
           ,@DataUniaoEstavel                             ,@CertidaoViuvoPessoa              ,@FolhaCertidaoViuvoPessoa         ,@LivroCertidaoViuvoPessoa
           ,@IndicaEscrituraRegistroPactoAnteNupcialPessoa,@NumeroPactoAnteNupcialPessoa     ,@FolhaRegistroPactoAnteNupcialPessoa,@LivroRegistroPactoAnteNupcialPessoa
           ,@NomeCartorioRegistroPactoAnteNupcialPessoa   ,@EnderecoResidencialPessoa        ,@NumeroEnderecoResidencialPessoa  ,@CodigoMunicipioResidenciaPessoa
           ,@TelefonePessoa                               ,@TempoOcupacaoPessoa              ,@NomeConjuge                      ,@DataNascimentoConjuge
           ,@IndicaNacionalidadeConjuge                   ,@NomePaisConjuge                  ,@CodigoMunicipioNaturalidadeConjuge,@ProfissaoConjuge
           ,@NumeroCPFCNPJConjuge                         ,@IndicaConjugeSabeAssinar         ,@TipoDocumentoConjuge             ,@NumeroDocumentoConjuge
           ,@OrgaoExpedidorDocumentoConjuge               ,@NomePaiConjuge                   ,@NomeMaeConjuge                   ,@IndicaEstadoCivilConjuge
           ,@CertidaoEstadoCivilConjuge                   ,@LivroCertidaoEstadoCivilConjuge  ,@FolhaCertidaoEstadoCivilConjuge  ,@EmissaoCertidaoEstadoCivilConjuge
           ,@NomeCartorioCertidaoEstadoCivilConjuge       ,@IndicaProprietario               ,@IndicaOcupante                   ,@NomeCartorioCertidaoViuvoPessoa
           ,@EmissaoCertidaoViuvoPessoa                   ,@EmissaoPactoAnteNupcialPessoal)
           SET @Erro = @Erro + @@ERROR      
       set @CodigoPessoaImovel = SCOPE_IDENTITY()        
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
           SELECT @CodigoProjeto AS CodigoProjeto,
                  @Erro AS CodigoErro, 
                  @MensagemErro AS MensagemErro,
                  @CodigoPessoaImovel AS CodigoPessoaImovel
", cDados.getDbName()
            , cDados.getDbOwner()
            /*@CodigoProjeto,@NomePessoa,@DataNascimentoPessoa,@IndicaNacionalidadePessoa*/
            , codigoProjeto, txtNome.Text, (dtNascimento.Value != null && dtNascimento.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtNascimento.Value.ToString()) : "NULL", ddlIndicaNacionalidade.Value.ToString()
            /*,@NomePaisPessoa,@CodigoMunicipioNaturalidadePessoa,@ProfissaoPessoa,@NumeroCPFCNPJPessoa*/
            , txtPais.Text, (ddlMunicipioPessoa.Value != null) ? ddlMunicipioPessoa.Value.ToString() : "null", txtProfissao.Text, txtCPF.Text
            /*,@IndicaPessoaSabeAssinar,@TipoDocumentoPessoa,@NumeroDocumentoPessoa,@OrgaoExpedidorDocumentoPessoa*/
            , (ckbSabeAssinar.Checked == true) ? "S" : "N", ddlTipoDocumento.Value, txtNumeroDocumento.Text, txtOrgaoExpeditor.Text
            /*,@NomePaiPessoa,@NomeMaePessoa,@IndicaEstadoCivilPessoa,@RegimeSeparacaoBensPessoa*/
            , txtNomePai.Text, txtNomeMae.Text, rblIndicaEstadoCivilPessoa.Value, rblRegimeSeparacaoBens.Value
            /*,@CertidaoEstadoCivilPessoa,@LivroCertidaoEstadoCivilPessoa,@FolhaCertidaoEstadoCivilPessoa,@EmissaoCertidaoEstadoCivilPessoa*/
            , txtSoltCasCertidao.Text, txtSoltCasLivro.Text, txtSoltCasFolhas.Text, (dtSoltCasEmissao.Value != null && dtSoltCasEmissao.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtSoltCasEmissao.Value.ToString()) : "NULL"
            /*,@NomeCartorioCertidaoEstadoCivilPessoa,@AutosSeparacaoPessoa,@DataSeparacaoPessoa,@JuizoSeparacaoPessoa*/
            , txtSoltCasCartorio.Text, txtSepDivSentencaAutosN.Text, (dtSepDivData.Value != null && dtSepDivData.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtSepDivData.Value.ToString()) : "NULL", txtSepDivJuizo.Text
            /*,@DataUniaoEstavel,@CertidaoViuvoPessoa,@FolhaCertidaoViuvoPessoa,@LivroCertidaoViuvoPessoa*/
            , (dtUniaoEstavelDesde.Value != null && dtUniaoEstavelDesde.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtUniaoEstavelDesde.Value.ToString()) : "NULL", txtViuvoNumeroCertObito.Text, txtViuvoFolhas.Text, txtViuvoLivro.Text
            /*,@IndicaEscrituraRegistroPactoAnteNupcialPessoa,@NumeroPactoAnteNupcialPessoa,@FolhaRegistroPactoAnteNupcialPessoa,@LivroRegistroPactoAnteNupcialPessoa*/
            , ddlIndicaEscRegPactoAnteNupcialPessoa.Value, txtNumeroIndEscRegPacAntNupPes.Text, txtFolhasIndEscRegPacAntNupPes.Text, txtLivroIndEscRegPacAntNupPes.Text
            /*,@NomeCartorioRegistroPactoAnteNupcialPessoa,@EnderecoResidencialPessoa,@NumeroEnderecoResidencialPessoa,@CodigoMunicipioResidenciaPessoa*/
            , txtCartorioIndEscRegPacAntNupPes.Text, txtEnderecoResidencial.Text, txtNumero.Text, (ddlMunicipioResidenciaPessoa.Value != null) ? ddlMunicipioResidenciaPessoa.Value.ToString() : "NULL"
            /* ,@TelefonePessoa,@TempoOcupacaoPessoa,@NomeConjuge,@DataNascimentoConjuge*/
            , txtFone.Text, decimal.Parse(speTempoOcupacaoAnos.Text.Replace(",", ".")), txtNomeConjuge.Text, (dtNascimentoConjuge.Value != null && dtNascimentoConjuge.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtNascimentoConjuge.Value.ToString()) : "NULL"
            /*,@IndicaNacionalidadeConjuge ,@NomePaisConjuge,@CodigoMunicipioNaturalidadeConjuge,@ProfissaoConjuge*/
            , ddlNacionalidadeConjuge.Value, txtPaisConjuge.Text, (ddlMunicipioConjuge.Value != null) ? ddlMunicipioConjuge.Value : "NULL", txtProfissaoConjuge.Text
            /*,@NumeroCPFCNPJConjuge,@IndicaConjugeSabeAssinar,@TipoDocumentoConjuge,@NumeroDocumentoConjuge*/
            , txtCPFConjuge.Text, (ckbSabeAssinarConjuge.Checked == true) ? "S" : "N", ddlTipoDocumentoConjuge.Value, txtNumeroDocumentoConjuge.Text
            /*,@OrgaoExpedidorDocumentoConjuge,@NomePaiConjuge,@NomeMaeConjuge,@IndicaEstadoCivilConjuge*/
            , txtOrgaoExpeditorConjuge.Text, txtNomePaiConjuge.Text, txtNomeMaeConjuge.Text, ddlEstadoCivilConjuge.Text
            /*,@CertidaoEstadoCivilConjuge,@LivroCertidaoEstadoCivilConjuge,@FolhaCertidaoEstadoCivilConjuge  ,@EmissaoCertidaoEstadoCivilConjuge*/
            , txtCertidaoConjuge.Text, txtLivroConjuge.Text, txtFolhasConjuge.Text, (dtEmissaoConjuge.Value != null && dtEmissaoConjuge.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtEmissaoConjuge.Value.ToString()) : "NULL"
            /*,@NomeCartorioCertidaoEstadoCivilConjuge,@IndicaProprietario,@IndicaOcupante, @NomeCartorioCertidaoViuvoPessoa*/
            , txtCartorioConjuge.Text, (ckbIndicaProprietario.Checked == true) ? "S" : "N", ckbIndicaOcupante.Checked == true ? "S" : "N", txtViuvoCartorio.Text,
            /*@EmissaoCertidaoViuvoPessoa,@EmissaoPactoAnteNupcialPessoal*/
            (dtViuvoEmissao.Value != null && dtViuvoEmissao.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtViuvoEmissao.Value.ToString()) : "NULL", (dtEmissaoIndEscRegPacAntNupPes.Value != null && dtEmissaoIndEscRegPacAntNupPes.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtEmissaoIndEscRegPacAntNupPes.Value.ToString()) : "NULL");
        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            string mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("UQ_Projeto_NomeProjeto"))
                    throw new Exception("Nome de projeto já existente.");
                else
                    throw new Exception(mensagemErro.ToString());
            }
            _codigoProjeto = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoProjeto"]);
            codigoPessoaImovel = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoPessoaImovel"]);
        }

    }

    protected void atualizaOcupanteAtual()
    {
        string comandoSql = string.Format(@"

DECLARE @Erro INT
DECLARE @MensagemErro nvarchar(2048)
    SET @Erro = 0

BEGIN TRAN

BEGIN TRY

DECLARE
       @CodigoPessoaImovel int
      ,@CodigoProjeto  int                        
      ,@NomePessoa  varchar(150)
      ,@DataNascimentoPessoa  datetime
      ,@IndicaNacionalidadePessoa  char(1)
      ,@NomePaisPessoa  varchar(150)
      ,@CodigoMunicipioNaturalidadePessoa  int
      ,@ProfissaoPessoa  varchar(250)
      ,@NumeroCPFCNPJPessoa varchar(15)
      ,@IndicaPessoaSabeAssinar  char(1)
      ,@TipoDocumentoPessoa  varchar(50)
      ,@NumeroDocumentoPessoa varchar(50)
      ,@OrgaoExpedidorDocumentoPessoa  varchar(50)
      ,@NomePaiPessoa varchar(150)
      ,@NomeMaePessoa  varchar(150)
      ,@IndicaEstadoCivilPessoa  char(2)
      ,@RegimeSeparacaoBensPessoa  char(3)
      ,@CertidaoEstadoCivilPessoa varchar(50)
      ,@LivroCertidaoEstadoCivilPessoa  varchar(50)
      ,@FolhaCertidaoEstadoCivilPessoa  varchar(50)
      ,@EmissaoCertidaoEstadoCivilPessoa datetime
      ,@NomeCartorioCertidaoEstadoCivilPessoa  varchar(250)
      ,@AutosSeparacaoPessoa  varchar(50)
      ,@DataSeparacaoPessoa  datetime
      ,@JuizoSeparacaoPessoa  varchar(250)
      ,@DataUniaoEstavel  datetime
      ,@CertidaoViuvoPessoa varchar(50)
      ,@FolhaCertidaoViuvoPessoa varchar(50)
      ,@LivroCertidaoViuvoPessoa  varchar(50)
      ,@IndicaEscrituraRegistroPactoAnteNupcialPessoa char(1)
      ,@NumeroPactoAnteNupcialPessoa varchar(50)
      ,@FolhaRegistroPactoAnteNupcialPessoa varchar(50)
      ,@LivroRegistroPactoAnteNupcialPessoa  varchar(50)
      ,@NomeCartorioRegistroPactoAnteNupcialPessoa  varchar(250)
      ,@EnderecoResidencialPessoa  varchar(250)
      ,@NumeroEnderecoResidencialPessoa  varchar(50)
      ,@CodigoMunicipioResidenciaPessoa  int
      ,@TelefonePessoa varchar(50)
      ,@TempoOcupacaoPessoa  decimal(5,2)
      ,@NomeConjuge varchar(150)
      ,@DataNascimentoConjuge datetime
      ,@IndicaNacionalidadeConjuge char(1)
      ,@NomePaisConjuge varchar(150)
      ,@CodigoMunicipioNaturalidadeConjuge int
      ,@ProfissaoConjuge  varchar(250)
      ,@NumeroCPFCNPJConjuge  varchar(15)
      ,@IndicaConjugeSabeAssinar char(1)
      ,@TipoDocumentoConjuge varchar(50)
      ,@NumeroDocumentoConjuge varchar(50)
      ,@OrgaoExpedidorDocumentoConjuge  varchar(50)
      ,@NomePaiConjuge varchar(150)
      ,@NomeMaeConjuge  varchar(150)
      ,@IndicaEstadoCivilConjuge  char(2)
      ,@CertidaoEstadoCivilConjuge varchar(50)
      ,@LivroCertidaoEstadoCivilConjuge  varchar(50)
      ,@FolhaCertidaoEstadoCivilConjuge varchar(50)
      ,@EmissaoCertidaoEstadoCivilConjuge datetime
      ,@NomeCartorioCertidaoEstadoCivilConjuge  varchar(250)
      ,@IndicaProprietario char(1)
      ,@IndicaOcupante char(1)
      ,@NomeCartorioCertidaoViuvoPessoa varchar(250)
      ,@EmissaoCertidaoViuvoPessoa datetime
      ,@EmissaoPactoAnteNupcialPessoal datetime
        
      SET @CodigoProjeto = {2}                       
      SET @NomePessoa = '{3}'
      SET @DataNascimentoPessoa = {4}
      SET @IndicaNacionalidadePessoa = '{5}'
      SET @NomePaisPessoa = '{6}'
      SET @CodigoMunicipioNaturalidadePessoa = {7}
      SET @ProfissaoPessoa = '{8}'
      SET @NumeroCPFCNPJPessoa = '{9}'
      SET @IndicaPessoaSabeAssinar = '{10}'
      SET @TipoDocumentoPessoa = '{11}'
      SET @NumeroDocumentoPessoa = '{12}'
      SET @OrgaoExpedidorDocumentoPessoa = '{13}'
      SET @NomePaiPessoa = '{14}'
      SET @NomeMaePessoa = '{15}'
      SET @IndicaEstadoCivilPessoa = '{16}'
      SET @RegimeSeparacaoBensPessoa = '{17}'
      SET @CertidaoEstadoCivilPessoa = '{18}'
      SET @LivroCertidaoEstadoCivilPessoa = '{19}'
      SET @FolhaCertidaoEstadoCivilPessoa = '{20}'
      SET @EmissaoCertidaoEstadoCivilPessoa = {21}
      SET @NomeCartorioCertidaoEstadoCivilPessoa = '{22}'
      SET @AutosSeparacaoPessoa = '{23}'
      SET @DataSeparacaoPessoa = {24}
      SET @JuizoSeparacaoPessoa = '{25}'
      SET @DataUniaoEstavel = {26}
      SET @CertidaoViuvoPessoa = '{27}'
      SET @FolhaCertidaoViuvoPessoa = '{28}'
      SET @LivroCertidaoViuvoPessoa  = '{29}'
      SET @IndicaEscrituraRegistroPactoAnteNupcialPessoa = '{30}'
      SET @NumeroPactoAnteNupcialPessoa = '{31}'
      SET @FolhaRegistroPactoAnteNupcialPessoa = '{32}'
      SET @LivroRegistroPactoAnteNupcialPessoa = '{33}'
      SET @NomeCartorioRegistroPactoAnteNupcialPessoa = '{34}'
      SET @EnderecoResidencialPessoa = '{35}'
      SET @NumeroEnderecoResidencialPessoa = '{36}'
      SET @CodigoMunicipioResidenciaPessoa = {37}
      SET @TelefonePessoa = '{38}'
      SET @TempoOcupacaoPessoa = {39}
      SET @NomeConjuge = '{40}'
      SET @DataNascimentoConjuge = {41}
      SET @IndicaNacionalidadeConjuge = '{42}'
      SET @NomePaisConjuge = '{43}'
      SET @CodigoMunicipioNaturalidadeConjuge = {44}
      SET @ProfissaoConjuge = '{45}'
      SET @NumeroCPFCNPJConjuge = '{46}'
      SET @IndicaConjugeSabeAssinar = '{47}'
      SET @TipoDocumentoConjuge = '{48}'
      SET @NumeroDocumentoConjuge = '{49}'
      SET @OrgaoExpedidorDocumentoConjuge = '{50}'
      SET @NomePaiConjuge = '{51}'
      SET @NomeMaeConjuge = '{52}'
      SET @IndicaEstadoCivilConjuge = '{53}'
      SET @CertidaoEstadoCivilConjuge = '{54}'
      SET @LivroCertidaoEstadoCivilConjuge = '{55}'
      SET @FolhaCertidaoEstadoCivilConjuge = '{56}'
      SET @EmissaoCertidaoEstadoCivilConjuge = {57}
      SET @NomeCartorioCertidaoEstadoCivilConjuge = '{58}'
      SET @IndicaProprietario = '{59}'
      SET @IndicaOcupante = '{60}'
      SET @CodigoPessoaImovel = {61}
      SET @NomeCartorioCertidaoViuvoPessoa = '{62}'
      SET @EmissaoCertidaoViuvoPessoa = {63}
      SET @EmissaoPactoAnteNupcialPessoal = {64}

UPDATE {0}.{1}.Prop_PessoaImovel
   SET NomePessoa = @NomePessoa
      ,DataNascimentoPessoa = @DataNascimentoPessoa
      ,IndicaNacionalidadePessoa = @IndicaNacionalidadePessoa
      ,NomePaisPessoa = @NomePaisPessoa
      ,CodigoMunicipioNaturalidadePessoa = @CodigoMunicipioNaturalidadePessoa
      ,ProfissaoPessoa = @ProfissaoPessoa
      ,NumeroCPFCNPJPessoa = @NumeroCPFCNPJPessoa
      ,IndicaPessoaSabeAssinar = @IndicaPessoaSabeAssinar
      ,TipoDocumentoPessoa = @TipoDocumentoPessoa
      ,NumeroDocumentoPessoa = @NumeroDocumentoPessoa
      ,OrgaoExpedidorDocumentoPessoa = @OrgaoExpedidorDocumentoPessoa
      ,NomePaiPessoa = @NomePaiPessoa
      ,NomeMaePessoa = @NomeMaePessoa
      ,IndicaEstadoCivilPessoa = @IndicaEstadoCivilPessoa
      ,RegimeSeparacaoBensPessoa = @RegimeSeparacaoBensPessoa
      ,CertidaoEstadoCivilPessoa = @CertidaoEstadoCivilPessoa
      ,LivroCertidaoEstadoCivilPessoa = @LivroCertidaoEstadoCivilPessoa
      ,FolhaCertidaoEstadoCivilPessoa = @FolhaCertidaoEstadoCivilPessoa
      ,EmissaoCertidaoEstadoCivilPessoa = @EmissaoCertidaoEstadoCivilPessoa
      ,NomeCartorioCertidaoEstadoCivilPessoa = @NomeCartorioCertidaoEstadoCivilPessoa
      ,AutosSeparacaoPessoa = @AutosSeparacaoPessoa
      ,DataSeparacaoPessoa = @DataSeparacaoPessoa
      ,JuizoSeparacaoPessoa = @JuizoSeparacaoPessoa
      ,DataUniaoEstavel = @DataUniaoEstavel
      ,CertidaoViuvoPessoa = @CertidaoViuvoPessoa
      ,FolhaCertidaoViuvoPessoa = @FolhaCertidaoViuvoPessoa
      ,LivroCertidaoViuvoPessoa = @LivroCertidaoViuvoPessoa
      ,NomeCartorioCertidaoViuvoPessoa = @NomeCartorioCertidaoViuvoPessoa
      ,IndicaEscrituraRegistroPactoAnteNupcialPessoa = @IndicaEscrituraRegistroPactoAnteNupcialPessoa
      ,NumeroPactoAnteNupcialPessoa = @NumeroPactoAnteNupcialPessoa
      ,FolhaRegistroPactoAnteNupcialPessoa = @FolhaRegistroPactoAnteNupcialPessoa
      ,LivroRegistroPactoAnteNupcialPessoa= @LivroRegistroPactoAnteNupcialPessoa
      ,NomeCartorioRegistroPactoAnteNupcialPessoa = @NomeCartorioRegistroPactoAnteNupcialPessoa
      ,EnderecoResidencialPessoa = @EnderecoResidencialPessoa
      ,NumeroEnderecoResidencialPessoa = @NumeroEnderecoResidencialPessoa
      ,CodigoMunicipioResidenciaPessoa = @CodigoMunicipioResidenciaPessoa
      ,TelefonePessoa = @TelefonePessoa
      ,TempoOcupacaoPessoa = @TempoOcupacaoPessoa
      ,NomeConjuge = @NomeConjuge
      ,DataNascimentoConjuge = @DataNascimentoConjuge
      ,IndicaNacionalidadeConjuge = @IndicaNacionalidadeConjuge
      ,NomePaisConjuge = @NomePaisConjuge
      ,CodigoMunicipioNaturalidadeConjuge = @CodigoMunicipioNaturalidadeConjuge
      ,ProfissaoConjuge = @ProfissaoConjuge
      ,NumeroCPFCNPJConjuge = @NumeroCPFCNPJConjuge
      ,IndicaConjugeSabeAssinar = @IndicaConjugeSabeAssinar
      ,TipoDocumentoConjuge = @TipoDocumentoConjuge
      ,NumeroDocumentoConjuge = @NumeroDocumentoConjuge
      ,OrgaoExpedidorDocumentoConjuge = @OrgaoExpedidorDocumentoConjuge
      ,NomePaiConjuge = @NomePaiConjuge
      ,NomeMaeConjuge = @NomeMaeConjuge
      ,IndicaEstadoCivilConjuge = @IndicaEstadoCivilConjuge
      ,CertidaoEstadoCivilConjuge= @CertidaoEstadoCivilConjuge
      ,LivroCertidaoEstadoCivilConjuge = @LivroCertidaoEstadoCivilConjuge
      ,FolhaCertidaoEstadoCivilConjuge = @FolhaCertidaoEstadoCivilConjuge
      ,EmissaoCertidaoEstadoCivilConjuge = @EmissaoCertidaoEstadoCivilConjuge
      ,NomeCartorioCertidaoEstadoCivilConjuge = @NomeCartorioCertidaoEstadoCivilConjuge
      ,IndicaProprietario = @IndicaProprietario
      ,IndicaOcupante = @IndicaOcupante
      ,EmissaoCertidaoViuvoPessoa = @EmissaoCertidaoViuvoPessoa
      ,EmissaoPactoAnteNupcialPessoal = @EmissaoPactoAnteNupcialPessoal
 WHERE CodigoPessoaImovel = @CodigoPessoaImovel
SET @Erro = @Erro + @@ERROR
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
 SELECT @Erro AS CodigoErro, 
        @MensagemErro AS MensagemErro", cDados.getDbName(), cDados.getDbOwner(),
            /*{2}CodigoProjeto*/codigoProjeto,
            /*{3}NomePessoa*/txtNome.Text,
            /*{4}DataNascimentoPessoa*/(dtNascimento.Value != null && dtNascimento.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtNascimento.Value.ToString()) : "NULL",
            /*{5}IndicaNacionalidadePessoa*/ ddlIndicaNacionalidade.Value,
            /*{6}NomePaisPessoa*/ txtPais.Text,
            /*{7}CodigoMunicipioNaturalidadePessoa*/(ddlMunicipioPessoa.Value != null) ? ddlMunicipioPessoa.Value.ToString() : "NULL",
            /*{8}ProfissaoPessoa*/ txtProfissao.Text,
            /*{9}NumeroCPFCNPJPessoa*/ txtCPF.Text,
            /*{10}IndicaPessoaSabeAssinar*/ (ckbSabeAssinar.Checked == true) ? "S" : "N",
            /*{11}TipoDocumentoPessoa*/ ddlTipoDocumento.Value,
            /*{12}NumeroDocumentoPessoa*/txtNumeroDocumento.Text,
            /*{13}OrgaoExpedidorDocumentoPessoa*/txtOrgaoExpeditor.Text,
            /*{14}NomePaiPessoa*/ txtNomePai.Text,
            /*{15}NomeMaePessoa*/ txtNomeMae.Text,
            /*{16}IndicaEstadoCivilPessoa*/ rblIndicaEstadoCivilPessoa.Value,
            /*{17}RegimeSeparacaoBensPessoa*/rblRegimeSeparacaoBens.Value,
            /*{18}CertidaoEstadoCivilPessoa*/ txtSoltCasCertidao.Text,
            /*{19}LivroCertidaoEstadoCivilPessoa*/ txtSoltCasLivro.Text,
            /*{20}FolhaCertidaoEstadoCivilPessoa*/txtSoltCasFolhas.Text,
            /*{21}EmissaoCertidaoEstadoCivilPessoa*/ (dtSoltCasEmissao.Value != null && dtSoltCasEmissao.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtSoltCasEmissao.Value.ToString()) : "NULL",
            /*{22}NomeCartorioCertidaoEstadoCivilPessoa*/ txtSoltCasCartorio.Text,
            /*{23}AutosSeparacaoPessoa*/ txtSepDivSentencaAutosN.Text,
            /*{24}DataSeparacaoPessoa*/ (dtSepDivData.Value != null && dtSepDivData.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtSepDivData.Value.ToString()) : "NULL",
            /*{25}JuizoSeparacaoPessoa*/  txtSepDivJuizo.Text,
            /*{26}DataUniaoEstavel*/ (dtUniaoEstavelDesde.Value != null && dtUniaoEstavelDesde.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtUniaoEstavelDesde.Value.ToString()) : "NULL",
            /*{27}CertidaoViuvoPessoa*/ txtViuvoNumeroCertObito.Text,
            /*{28}FolhaCertidaoViuvoPessoa*/ txtViuvoFolhas.Text,
            /*{29}LivroCertidaoViuvoPessoa*/txtViuvoLivro.Text,
            /*{30}IndicaEscrituraRegistroPactoAnteNupcialPessoa*/ddlIndicaEscRegPactoAnteNupcialPessoa.Value,
            /*{31}NumeroPactoAnteNupcialPessoa*/ txtNumeroIndEscRegPacAntNupPes.Text,
            /*{32}FolhaRegistroPactoAnteNupcialPessoa*/ txtFolhasIndEscRegPacAntNupPes.Text,
            /*{33}LivroRegistroPactoAnteNupcialPessoa*/ txtLivroIndEscRegPacAntNupPes.Text,
            /*{34}NomeCartorioRegistroPactoAnteNupcialPessoa*/ txtCartorioIndEscRegPacAntNupPes.Text,
            /*{35}EnderecoResidencialPessoa*/ txtEnderecoResidencial.Text,
            /*{36}NumeroEnderecoResidencialPessoa*/txtNumero.Text,
            /*{37}CodigoMunicipioResidenciaPessoa*/  (ddlMunicipioResidenciaPessoa.Value != null) ? ddlMunicipioResidenciaPessoa.Value.ToString() : "null",
            /*{38}TelefonePessoa*/txtFone.Text,
            /*{39}TempoOcupacaoPessoa*/speTempoOcupacaoAnos.Text.Replace(",", "."),
            /*{40}NomeConjuge*/ txtNomeConjuge.Text,
            /*{41}DataNascimentoConjuge*/(dtNascimentoConjuge.Value != null && dtNascimentoConjuge.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtNascimentoConjuge.Value.ToString()) : "NULL",
            /*{42}IndicaNacionalidadeConjuge*/ ddlNacionalidadeConjuge.Value,
            /*{43}NomePaisConjuge*/ txtPaisConjuge.Text,
            /*{44}CodigoMunicipioNaturalidadeConjuge*/  (ddlMunicipioConjuge.Value != null) ? ddlMunicipioConjuge.Value.ToString() : "null",
            /*{45}ProfissaoConjuge*/ txtProfissaoConjuge.Text,
            /*{46}NumeroCPFCNPJConjuge*/ txtCPFConjuge.Text,
            /*{47}IndicaConjugeSabeAssinar*/ (ckbSabeAssinarConjuge.Checked == true) ? "S" : "N",
            /*{48}TipoDocumentoConjuge*/  ddlTipoDocumentoConjuge.Value,
            /*{49}NumeroDocumentoConjuge*/ txtNumeroDocumentoConjuge.Text,
            /*{50}OrgaoExpedidorDocumentoConjuge*/ txtOrgaoExpeditorConjuge.Text,
            /*{51}NomePaiConjuge*/ txtNomePaiConjuge.Text,
            /*{52}NomeMaeConjuge*/ txtNomeMaeConjuge.Text,
            /*{53}IndicaEstadoCivilConjuge*/ ddlEstadoCivilConjuge.Value,
            /*{54}CertidaoEstadoCivilConjuge*/ txtCertidaoConjuge.Text,
            /*{55}LivroCertidaoEstadoCivilConjuge*/ txtLivroConjuge.Text,
            /*{56}FolhaCertidaoEstadoCivilConjuge*/ txtFolhasConjuge.Text,
            /*{57}EmissaoCertidaoEstadoCivilConjuge*/ (dtEmissaoConjuge.Value != null && dtEmissaoConjuge.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtEmissaoConjuge.Value.ToString()) : "NULL",
            /*{58}NomeCartorioCertidaoEstadoCivilConjuge*/ txtCartorioConjuge.Text,
            /*{59}IndicaProprietario*/(ckbIndicaProprietario.Checked == true) ? "S" : "N",
            /*{60}IndicaOcupante*/ (ckbIndicaOcupante.Checked == true) ? "S" : "N",
            /*{61}CodigoPessoaImovel*/  codigoPessoaImovel,
            /*{62}NomeCartorioCertidaoViuvoPessoa*/ txtViuvoCartorio.Text,
            /*{63}EmissaoCertidaoViuvoPessoa*/ (dtViuvoEmissao.Value != null && dtViuvoEmissao.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtViuvoEmissao.Value.ToString()) : "NULL",
            /*{64}dtEmissaoIndEscRegPacAntNupPes*/(dtEmissaoIndEscRegPacAntNupPes.Value != null && dtEmissaoIndEscRegPacAntNupPes.Value.ToString() != "") ? String.Format(@"CONVERT(DATETIME,'{0}',103)", dtEmissaoIndEscRegPacAntNupPes.Value.ToString()) : "NULL");



        DataSet ds = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            object codigoErro = ds.Tables[0].Rows[0]["CodigoErro"];
            string mensagemErro = ds.Tables[0].Rows[0]["MensagemErro"].ToString();
            if (!(Convert.IsDBNull(codigoErro) || codigoErro.Equals(0)))
            {
                if (mensagemErro.Contains("UQ_Projeto_NomeProjeto"))
                    throw new Exception("Nome de projeto já existente.");
                else
                    throw new Exception(mensagemErro.ToString());
            }
        }
    }

    protected void sdsMunicipio_Filtering(object sender, SqlDataSourceFilteringEventArgs e)
    {
        //todos os sds de municipio utilizam essa função
        //if (e.ParameterValues.Contains("SiglaUF"))
        //    e.Cancel = e.ParameterValues["SiglaUF"] == null;
        //e.ParameterValues.Clear();
        //e.ParameterValues.Add("SiglaUF", ddlUFPessoa.Text);

       
    }
    protected void ddlUFPessoa_SelectedIndexChanged(object sender, EventArgs e)
    {
        sdsMunicipioPessoa.Select(new DataSourceSelectArguments());
    }
    protected void ddlUFConjuge_SelectedIndexChanged(object sender, EventArgs e)
    {
        sdsMunicipioConjuge.Select(new DataSourceSelectArguments());
    }
    protected void ddlUFResidenciaPessoa_SelectedIndexChanged(object sender, EventArgs e)
    {
        sdsMunicipioResidenciaPessoa.Select(new DataSourceSelectArguments());
    }

    protected void ddlMunicipioPessoa_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string comandoSQL = string.Format(@"SELECT CodigoMunicipio
      ,NomeMunicipio
      ,SiglaUF
  FROM {0}.{1}.Municipio
where SiglaUF = '{2}'", cDados.getDbName(), cDados.getDbOwner(), ddlUFPessoa.SelectedItem.Value);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ddlMunicipioPessoa.DataSource = ds.Tables[0];
        ddlMunicipioPessoa.TextField = "NomeMunicipio";
        ddlMunicipioPessoa.ValueField = "CodigoMunicipio";
        ddlMunicipioPessoa.DataBind();
    }
    protected void ddlMunicipioConjuge_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string comandoSQL = string.Format(@"SELECT CodigoMunicipio
      ,NomeMunicipio
      ,SiglaUF
  FROM {0}.{1}.Municipio
where SiglaUF = '{2}'", cDados.getDbName(), cDados.getDbOwner(), ddlUFConjuge.SelectedItem.Value);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ddlMunicipioConjuge.DataSource = ds.Tables[0];
        ddlMunicipioConjuge.TextField = "NomeMunicipio";
        ddlMunicipioConjuge.ValueField = "CodigoMunicipio";
        ddlMunicipioConjuge.DataBind();
    }
    protected void ddlMunicipioResidenciaPessoa_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string comandoSQL = string.Format(@"SELECT CodigoMunicipio
      ,NomeMunicipio
      ,SiglaUF
  FROM {0}.{1}.Municipio
where SiglaUF = '{2}'", cDados.getDbName(), cDados.getDbOwner(), ddlUFResidenciaPessoa.SelectedItem.Value);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ddlMunicipioResidenciaPessoa.DataSource = ds.Tables[0];
        ddlMunicipioResidenciaPessoa.TextField = "NomeMunicipio";
        ddlMunicipioResidenciaPessoa.ValueField = "CodigoMunicipio";
        ddlMunicipioResidenciaPessoa.DataBind();
    }
}