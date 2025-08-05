using DevExpress.Web;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.UI.WebControls;

public partial class espacoTrabalho_frameEspacoTrabalho_BibliotecaInterno : System.Web.UI.Page
{
    dados cDados;
    string ResolucaoCliente;
    private int codigoTipoAssociacao;
    int IDObjetoAssociado, IDObjetoPai;
    string IniciaisTipoAssociacao = "";
    bool somenteLeitura = false;
    bool podeEditarPermissoes = true;
    protected string paddingTela = "0";


    private int? _contadorDeAnexos;
    public int? ContadorDeAnexos
    {
        get
        {
            return _contadorDeAnexos.HasValue ? _contadorDeAnexos : 0;
        }
        set { _contadorDeAnexos = value; }
    }


    public string estiloFooter = "dxsplPane dxfm-toolbar";

    public int codigoProjeto = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        //Declarando variável Session para controle de Motragem de Mensagens
        if (Session["existeAnexoMesmoNome"] == null)
        {
            Session["existeAnexoMesmoNome"] = 0;
        }

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        IniciaisTipoAssociacao = (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "") ? Request.QueryString["TA"].ToString() : "";
        IDObjetoAssociado = (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "") ? int.Parse(Request.QueryString["ID"].ToString()) : -1;
        IDObjetoPai = (Request.QueryString["IDOP"] != null && Request.QueryString["IDOP"].ToString() != "") ? int.Parse(Request.QueryString["IDOP"].ToString()) : 0;


        codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(IniciaisTipoAssociacao);

        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource1.SelectParameters["CodigoObjetoAssociado"].DefaultValue = IDObjetoAssociado.ToString();
        SqlDataSource1.SelectParameters["CodigoEntidadeContexto"].DefaultValue = cDados.getInfoSistema("CodigoEntidade").ToString();
        SqlDataSource1.SelectParameters["CodigoUsuarioSistema"].DefaultValue = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        SqlDataSource1.SelectParameters["IniciaisTipoObjetoAssociado"].DefaultValue = IniciaisTipoAssociacao;
        SqlDataSource1.SelectParameters["PalavraChave"].DefaultValue = txtPesquisa.Text;
        SqlDataSource1.SelectParameters["CodigoTipoAssociacao"].DefaultValue = cDados.getCodigoTipoAssociacao(IniciaisTipoAssociacao).ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this);
        string extensoesPermitidas = "";
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "" && IniciaisTipoAssociacao == "PR")
            codigoProjeto = int.Parse(Request.QueryString["ID"].ToString());

        if (Request.QueryString["Popup"] != null && Request.QueryString["Popup"].ToString() == "S")
        {
            paddingTela = "10px 0 0 0";
            fmArquivos.JSProperties["cp_Popup"] = Request.QueryString["Popup"].ToString();
            btnFechar.ClientVisible = true;
            fmArquivos.JSProperties["cp_MostraBotaoFechar"] = "S";

            Regex regex = new Regex(@"^[+-]?\d+$");
            if (Request.QueryString["PopupOffset"] != null && (regex.IsMatch(Request.QueryString["PopupOffset"].ToString())))
            {
                fmArquivos.JSProperties["cp_PopupOffset"] = int.Parse(Request.QueryString["PopupOffset"]);
            }
        }
        else if (Request.QueryString["Frame"] != null && Request.QueryString["Frame"].ToString() == "S")
        {
            paddingTela = "10px";
        }
        else
        {
            fmArquivos.JSProperties["cp_MostraBotaoFechar"] = "N";
            td_btnFechar.Style.Add("display", "none");
        }

        if (!IsPostBack && (Request.QueryString["TO"] != null && Request.QueryString["TO"].ToString() == "Consultar") || (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S"))
        {
            fmArquivos.SettingsEditing.AllowCopy = false;
            fmArquivos.SettingsEditing.AllowCreate = false;
            fmArquivos.SettingsEditing.AllowDelete = false;
            fmArquivos.SettingsEditing.AllowMove = false;
            fmArquivos.SettingsEditing.AllowRename = false;
            fmArquivos.SettingsUpload.Enabled = false;
            fmArquivos.SettingsContextMenu.Items.Clear();
            fmArquivos.SettingsToolbar.Items.Remove(fmArquivos.SettingsToolbar.Items.FindByCommandName("btnLink"));
            fmArquivos.SettingsContextMenu.Items.Remove(fmArquivos.SettingsContextMenu.Items.FindByCommandName("btnLink2"));
            somenteLeitura = true;
        }
        else if (IniciaisTipoAssociacao == "EN")
            fmArquivos.SettingsToolbar.Items.Remove(fmArquivos.SettingsToolbar.Items.FindByCommandName("btnLink"));
        else if (IniciaisTipoAssociacao != "EN")
            fmArquivos.SettingsContextMenu.Items.Remove(fmArquivos.SettingsContextMenu.Items.FindByCommandName("btnLink2"));

        bool permiteLink = false;

        if (IniciaisTipoAssociacao == "EN")
            podeEditarPermissoes = somenteLeitura == false && cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()),
                IDObjetoAssociado, "null", IniciaisTipoAssociacao, 0, "null", "EN_AdmBib");
        else
            podeEditarPermissoes = somenteLeitura == false && cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()),
                IDObjetoAssociado, "null", IniciaisTipoAssociacao, 0, "null", IniciaisTipoAssociacao + "_AdmPrsAnx");

        if (!IsPostBack && !podeEditarPermissoes)
        {
            fmArquivos.SettingsToolbar.Items.Remove(fmArquivos.SettingsToolbar.Items.FindByCommandName("btnPermissao"));
            fmArquivos.SettingsContextMenu.Items.Remove(fmArquivos.SettingsContextMenu.Items.FindByCommandName("btnPermissao"));

        }

        if (!IsPostBack && IniciaisTipoAssociacao != "EN")
        {
            permiteLink = somenteLeitura == false && cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()),
            IDObjetoAssociado, "null", IniciaisTipoAssociacao, 0, "null", "EN_AdmBib");

            if (!permiteLink)
            {
                if (fmArquivos.SettingsToolbar.Items.FindByCommandName("btnLink") != null)
                    fmArquivos.SettingsToolbar.Items.Remove(fmArquivos.SettingsToolbar.Items.FindByCommandName("btnLink"));
            }
        }

        if (!IsPostBack && IniciaisTipoAssociacao == "EN")
        {
            permiteLink = somenteLeitura == false && cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()),
            IDObjetoAssociado, "null", IniciaisTipoAssociacao, 0, "null", "EN_AdmBib");
            bool utilizaLinkVariosObjetos = false;

            DataSet dsParam = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "UtilizaLinkVariosObjetos");

            if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
                utilizaLinkVariosObjetos = dsParam.Tables[0].Rows[0]["UtilizaLinkVariosObjetos"].ToString() == "S";

            if (!permiteLink || !utilizaLinkVariosObjetos)
            {
                if (fmArquivos.SettingsContextMenu.Items.FindByCommandName("btnLink2") != null)
                    fmArquivos.SettingsContextMenu.Items.Remove(fmArquivos.SettingsContextMenu.Items.FindByCommandName("btnLink2"));
            }
        }

        int alturaListaAnexos = 500;

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            fmArquivos.JSProperties["cp_ALT"] = Request.QueryString["ALT"].ToString();
            alturaListaAnexos = int.Parse(Request.QueryString["ALT"].ToString()) - 25;
        }
        else
        {
            string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            alturaListaAnexos = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1)) - 250;
        }

        //fmArquivos.Height = alturaListaAnexos;
        fmArquivos.JSProperties["cp_Altura"] = alturaListaAnexos;

        fmArquivos.JSProperties["cp_Visao"] = "D";

        int tamanhoMaximoArquivoUpload = 2; // inicialmente, são dois megas
        DataSet ds = cDados.getParametrosSistema("tamanhoMaximoArquivoAnexoEmMegaBytes", "ExtensoesPermitidasAnexos");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString() != "")
            tamanhoMaximoArquivoUpload = int.Parse(ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["ExtensoesPermitidasAnexos"].ToString() != "")
        {
            fmArquivos.Settings.AllowedFileExtensions = ds.Tables[0].Rows[0]["ExtensoesPermitidasAnexos"].ToString().Split(',');
            lblExtensoesPermitidas.Text = "Tipos de arquivos permitidos: " + ds.Tables[0].Rows[0]["ExtensoesPermitidasAnexos"].ToString();

            string strong = "<strong>";
            string barra_strong = "</strong>";

            string trataRetiradaPonto = strong + ds.Tables[0].Rows[0]["ExtensoesPermitidasAnexos"].ToString().Replace(".", "") + barra_strong;


            extensoesPermitidas = Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_extens_es_de_arquivo_permitidas_ + trataRetiradaPonto;
        }

        fmArquivos.SettingsUpload.ValidationSettings.MaxFileSize = tamanhoMaximoArquivoUpload * 1024 * 1024;
        fmArquivos.SettingsUpload.ValidationSettings.MaxFileSizeErrorText = Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_o_tamanho_m_ximo_de_upload_de_um_arquivo___ + tamanhoMaximoArquivoUpload + " Megabytes";
        spnLegenda.InnerHtml = string.Format(@"<p align=""right"" style=""font-size:11px"" >{0}</p>", fmArquivos.SettingsUpload.ValidationSettings.MaxFileSizeErrorText + " " + extensoesPermitidas);
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/styles.css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../Content/sprite.css"" />"));
        this.TH(this.TS("frameEspacoTrabalho_bibliotecaInterno"));
        fmArquivos.Settings.RootFolder = string.IsNullOrWhiteSpace(txtPesquisa.Text) ? Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_raiz : Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_resultado_da_pesquisa;

        DataSet dsParamMultiSelecao = cDados.getParametrosSistema("utilizaMultiSelecaoDeAnexos");
        if (cDados.DataSetOk(dsParamMultiSelecao) && cDados.DataTableOk(dsParamMultiSelecao.Tables[0]))
        {
            fmArquivos.Settings.EnableMultiSelect = (dsParamMultiSelecao.Tables[0].Rows[0]["utilizaMultiSelecaoDeAnexos"].ToString().ToUpper() + "") == "S";
            if (!fmArquivos.Settings.EnableMultiSelect)
            {
                fmArquivos.SettingsToolbar.Items.Remove(fmArquivos.SettingsToolbar.Items.FindByCommandName("selectAll"));
            }
        }

        if (codigoProjeto != -1)
        {
            int codigoStatusProjeto = cDados.getStatusProjeto(codigoProjeto);

            bool indicaProjetoInativo = codigoStatusProjeto == 4 || codigoStatusProjeto == 5 || codigoStatusProjeto == 6
              || codigoStatusProjeto == 17 || codigoStatusProjeto == 18 || codigoStatusProjeto == 19
              || codigoStatusProjeto == 22 || codigoStatusProjeto == 23 || codigoStatusProjeto == 24;

            bool perfilAdmin = cDados.PerfilAdministrador(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()));

            if (indicaProjetoInativo && !perfilAdmin)
            {
                fmArquivos.SettingsUpload.Enabled = false;
                fmArquivos.SettingsEditing.AllowCreate = false;
                fmArquivos.SettingsEditing.AllowRename = false;
                fmArquivos.SettingsEditing.AllowDelete = false;
                fmArquivos.SettingsEditing.AllowMove = false;
            }
        }

        if (!IsPostBack)
        {
            try
            {
                ApplyRules(fmArquivos.SelectedFolder);
            }
            catch (Exception ex)
            {
                string mensagem = ex.Message;
            }
        }
    }

    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        long codigoAnexo = Convert.ToInt32(e.Command.Parameters["@CodigoAnexo"].Value);
        bool indicaArquivoFisico = VerificaIndicaArquivoFisico(codigoAnexo);
        var newFileName = e.Command.Parameters["@Nome"].Value as string;
        if (indicaArquivoFisico)
        {
            try
            {
                string caminhoArquivo = RenomearArquivoAnexo(codigoAnexo, newFileName);
            }
            catch
            {
                e.Cancel = true;
            }
        }


        string codigoPasta = e.Command.Parameters["@CodigoPastaSuperior"].Value.ToString();

        e.Command.Parameters["@CodigoPastaSuperior"].Value = codigoPasta == "-1" ? null : codigoPasta;
    }

    private string RenomearArquivoAnexo(long codigoAnexo, string novoNomeArquivo)
    {
        string caminhoAtualizadoArquivo = string.Empty;
        string dbOwner = cDados.getDbOwner();
        string dbName = cDados.getDbName();
        string comandoObtemUltimaVersao =
            @"DECLARE @codigoSequencialAnexo bigint
              ";
        comandoObtemUltimaVersao += string.Format("SELECT @codigoSequencialAnexo = max(codigoSequencialAnexo) from {0}.{1}.AnexoVersao where codigoAnexo = {2} ", dbName, dbOwner, codigoAnexo);

        string comandoSql = string.Format(
             @"{2}

               SELECT replace(replace(a.Nome, '/', '-'), '?', ''), a.CodigoEntidade, aa.CodigoObjetoAssociado, aa.CodigoTipoAssociacao, a.CodigoPastaSuperior, AV.NumeroVersao
                 FROM {0}.{1}.Anexo AS a INNER JOIN
                      {0}.{1}.AnexoVersao AS AV ON AV.codigoAnexo = a.codigoAnexo INNER JOIN
                      {0}.{1}.AnexoAssociacao AS aa ON (aa.CodigoAnexo = a.CodigoAnexo ) 
                WHERE     (AV.codigoSequencialAnexo = @codigoSequencialAnexo )", dbName, dbOwner, comandoObtemUltimaVersao);


        DataSet ds = cDados.getDataSet(comandoSql);

        var row = ds.Tables[0].Rows[0];
        var nomeAntigoAntigo = row["Nome"] as string;
        var codigoEntidade = row["CodigoEntidade"];
        var codigoObjeto = row["CodigoObjetoAssociado"];
        var codigoAssociacao = row["CodigoTipoAssociacao"];
        var codigoPasta = row["CodigoPastaSuperior"];
        var numeroVersao = row["NumeroVersao"];

        DataSet dsParamatro = cDados.getParametrosSistema("DiretorioGravacaoAnexosEmDisco");
        string diretorioGravacaoAnexosEmDisco = dsParamatro.Tables[0].Rows[0]["DiretorioGravacaoAnexosEmDisco"] as string;
        if (!string.IsNullOrEmpty(diretorioGravacaoAnexosEmDisco))
        {
            diretorioGravacaoAnexosEmDisco = diretorioGravacaoAnexosEmDisco.Replace("%PathPortal%", HostingEnvironment.ApplicationPhysicalPath);
            string nomeArquivo = string.Format("{0}_{1}_{2}_V{3}_{4}", codigoPasta, codigoObjeto, codigoAssociacao, numeroVersao, nomeAntigoAntigo);
            var tempPath = Path.Combine(diretorioGravacaoAnexosEmDisco, nomeArquivo);
            if (File.Exists(tempPath))
            {
                caminhoAtualizadoArquivo = tempPath.Replace(nomeAntigoAntigo, novoNomeArquivo);
                File.Move(tempPath, caminhoAtualizadoArquivo);
            }
        }

        return caminhoAtualizadoArquivo;
    }

    private bool VerificaIndicaArquivoFisico(long codigoAnexo)
    {
        string comandoSQL = string.Format(@"
            BEGIN
                DECLARE @codigoSequencialAnexo AS bigint;
                DECLARE @numeroUltimaVersao int
                SET @numeroUltimaVersao = (SELECT max(numeroVersao) FROM {0}.{1}.AnexoVersao WHERE codigoAnexo = {2} )
                SELECT IndicaDestinoGravacaoAnexo FROM {0}.{1}.AnexoVersao WHERE CodigoAnexo = {2} AND numeroVersao = @numeroUltimaVersao
            END", cDados.getDbName()
                , cDados.getDbOwner()
                , codigoAnexo);

        DataSet ds = cDados.getDataSet(comandoSQL);
        string indicaDestinoGravacaoAnexo = "BD";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            indicaDestinoGravacaoAnexo = ds.Tables[0].Rows[0]["IndicaDestinoGravacaoAnexo"] as string;
        }

        return string.Equals(indicaDestinoGravacaoAnexo, "DI", StringComparison.CurrentCultureIgnoreCase);
    }

    protected void fmArquivos_FileDownloading(object source, DevExpress.Web.FileManagerFileDownloadingEventArgs e)
    {
        int? codigoSequencialAnexo = null;
        string comandoSQL1 = string.Format(@"SELECT CodigoAnexo FROM {0}.{1}.Anexo WHERE CONVERT(varchar(30), DataInclusao, 120) = CONVERT(varchar(30), CONVERT(DateTime, '{2:dd/MM/yyyy HH:mm:ss}', 103), 120) AND Nome = '{3}' AND DataExclusao IS NULL", cDados.getDbName()
    , cDados.getDbOwner()
    , e.File.LastWriteTime
    , e.File.Name.Replace("'", "''"));

        DataSet dsx = cDados.getDataSet(comandoSQL1);
        if (cDados.DataSetOk(dsx) && cDados.DataTableOk(dsx.Tables[0]))
        {
            string CodigoAnexo = dsx.Tables[0].Rows[0]["CodigoAnexo"].ToString();
            if (codigoSequencialAnexo.HasValue)
                comandoSQL1 = string.Format(@"SELECT IndicaDestinoGravacaoAnexo FROM {0}.{1}.AnexoVersao WHERE CodigoSequencialAnexo = {2}", cDados.getDbName()
                    , cDados.getDbOwner()
                    , codigoSequencialAnexo.Value);
            else
                comandoSQL1 = string.Format(@"
            BEGIN
                DECLARE @codigoSequencialAnexo AS bigint;
                DECLARE @numeroUltimaVersao int
                SET @numeroUltimaVersao = (SELECT max(numeroVersao) FROM {0}.{1}.AnexoVersao WHERE codigoAnexo = {2} )
                SELECT IndicaDestinoGravacaoAnexo FROM {0}.{1}.AnexoVersao WHERE CodigoAnexo = {2} AND numeroVersao = @numeroUltimaVersao
            END", cDados.getDbName()
                    , cDados.getDbOwner()
                    , CodigoAnexo);

            DataSet ds = cDados.getDataSet(comandoSQL1);
            string IndicaDestinoGravacaoAnexo = "BD";

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                IndicaDestinoGravacaoAnexo = ds.Tables[0].Rows[0]["IndicaDestinoGravacaoAnexo"].ToString();
            }

            string NomeArquivo = "";
            byte[] imagem = cDados.getConteudoAnexo(int.Parse(CodigoAnexo), codigoSequencialAnexo, ref NomeArquivo, IndicaDestinoGravacaoAnexo);
            var memoryStream = new MemoryStream(imagem);
            e.OutputStream = memoryStream;
        }
    }

    protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        string codigoPasta = e.Command.Parameters["@CodigoPastaSuperior"].Value.ToString();
        string indicaPasta = e.Command.Parameters["@IndicaPasta"].Value.ToString();
        int? CodigoPastaDestino = null;

        if (codigoPasta != "-1")
            CodigoPastaDestino = int.Parse(codigoPasta);

        e.Command.Parameters["@IndicaPasta"].Value = indicaPasta == "1" ? "S" : "N";
        e.Command.Parameters["@CodigoEntidade"].Value = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        e.Command.Parameters["@CodigoUsuarioInclusao"].Value = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (indicaPasta == "1" || indicaPasta.ToUpper() == "TRUE")
        {
            try
            {
                string nomeNovaPasta = "?" + e.Command.Parameters["@Nome"].Value.ToString();
                string descricaoNovaPasta = "";
                string mensagem = "";

                mensagem = cDados.incluirAnexo(descricaoNovaPasta, cDados.getInfoSistema("IDUsuarioLogado").ToString(), nomeNovaPasta, cDados.getInfoSistema("CodigoEntidade").ToString(), CodigoPastaDestino, 'S', 'N', codigoTipoAssociacao, "NULL", "", IDObjetoAssociado.ToString(), null, "N");
                if (mensagem != "")
                {
                    throw new Exception(mensagem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_erro_ao_incluir_a_pasta_ + Environment.NewLine + ex.Message);
            }
        }
        else
        {
            try
            {

                string nomeNovoAnexo = "";
                string[] nomesNovoAnexo = hfCodigoAnexo.Get("NomeArquivoUPL").ToString().Split('|');
                nomeNovoAnexo = nomesNovoAnexo[ContadorDeAnexos.Value];
                ContadorDeAnexos += 1;

                bool existeAnexoMesmoNome = cDados.VerificaDuplicadeAnexo(cDados.getInfoSistema("IDUsuarioLogado").ToString(), nomeNovoAnexo, cDados.getInfoSistema("CodigoEntidade").ToString(), int.Parse(codigoPasta), codigoTipoAssociacao, IDObjetoAssociado.ToString());

                //Verifica duplicidade de Arquivos em anexo.
                if (existeAnexoMesmoNome)
                {
                    Session["existeAnexoMesmoNome"] = 1;

                    e.Cancel = true;
                    return;
                }
                //##########################################

                int tamanhoImagem = e.Command.Parameters["@ConteudoAnexo"].Size;

                if (nomeNovoAnexo.Length > 255)
                    throw new Exception(Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_o_nome_do_arquivo_n_o_pode_ter_mais_que_255_caracteres__renomeie_o_arquivo_e_tente_novamente_);

                string extensao = Path.GetExtension(nomeNovoAnexo);
                // VERIFICA A EXTENSÃO DO ARQUIVO.                
                if (extensao.ToLower() == ".exe" || extensao.ToLower() == ".com" || extensao.ToLower() == ".dll")
                    throw new Exception(Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_o_tipo_do_arquivo_n_o_pode_ser_anexado_);

                //##########################################
                // insere o arquivo no banco de dados
                string mensagem = cDados.incluirAnexo(string.Empty, cDados.getInfoSistema("IDUsuarioLogado").ToString(), nomeNovoAnexo, cDados.getInfoSistema("CodigoEntidade").ToString(), CodigoPastaDestino, 'N', 'N', codigoTipoAssociacao, "'S'", "", IDObjetoAssociado.ToString(), (byte[])e.Command.Parameters["@ConteudoAnexo"].Value, "N");

                if (!string.IsNullOrEmpty(mensagem))
                    throw new Exception(mensagem);
            }
            catch (Exception ex)
            {
                throw new Exception(Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_erro_ao_incluir_o_arquivo_ + Environment.NewLine + ex.Message);
            }
        }

        e.Cancel = true;
    }

    protected void SqlDataSource1_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        int codigoAnexo = int.Parse(e.Command.Parameters["@CodigoAnexo"].Value.ToString());

        string indicaPasta = "N";

        string comandoSQL = string.Format(@"SELECT IndicaPasta FROM {0}.{1}.Anexo WHERE CodigoAnexo = {2}", cDados.getDbName()
            , cDados.getDbOwner()
            , codigoAnexo);

        DataSet ds = cDados.getDataSet(comandoSQL);


        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            indicaPasta = ds.Tables[0].Rows[0]["IndicaPasta"].ToString();
        }

        indicaPasta = (indicaPasta == "1" || indicaPasta.ToUpper() == "TRUE") ? "S" : "N";

        string erro = "";

        bool estaCompartilhado = cDados.anexoEstaCompartilhadoEmEntidades(codigoAnexo, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), 1, 1);

        if (estaCompartilhado)
        {
           fmArquivos.JSProperties["cpErro"] = Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_o_arquivo_ou_pasta_em_que_se_deseja_excluir_est__compartilhado_com_outras_unidades__para_excluir_remova_o_compartilhamento_;

        }
        else if (indicaPasta != "")
        {
            bool retorno = cDados.excluiAnexoProjeto(char.Parse(indicaPasta), codigoAnexo, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), IDObjetoAssociado, codigoTipoAssociacao, ref erro);

            if (!retorno)
            {
                fmArquivos.JSProperties["cpErro"] = erro;
            }
        }

        e.Cancel = true;
    }

    protected void fmArquivos_ItemDeleting(object source, DevExpress.Web.FileManagerItemDeleteEventArgs e)
    {
        if (e.Item is FileManagerFolder)
        {
            if ((e.Item as FileManagerFolder).GetFiles().Length > 0 || (e.Item as FileManagerFolder).GetFolders().Length > 0)
            {
                e.Cancel = true;
               fmArquivos.JSProperties["cpErro"] = Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_s____poss_vel_excluir_pastas_que_estejam_vazias_;
            }
        }
    }

    protected void fmArquivos_CustomFileInfoDisplayText(object source, FileManagerCustomFileInfoDisplayTextEventArgs e)
    {
        if (e.FileInfoType == FileInfoType.Size)
        {
            string comandoSQL = string.Format(@"SELECT  ca.codigoSequencialAnexo,  ca.Anexo
					   FROM Anexo a 
					   INNER JOIN AnexoVersao av on (av.codigoAnexo = a.CodigoAnexo)
					   LEFT JOIN ConteudoAnexo ca on (ca.codigoSequencialAnexo = av.codigoSequencialAnexo)					   
					   WHERE a.CodigoAnexo = {0} ", e.File.Id);
            DataSet dstemp = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(dstemp) && cDados.DataTableOk(dstemp.Tables[0]))
            {
                ASPxBinaryImage image1 = new ASPxBinaryImage();
                if (dstemp.Tables[0].Rows[0]["Anexo"].ToString() == "")
                {
                    e.DisplayText = "0B - não encontrado";
                    return;
                }
                image1.ContentBytes = (byte[])dstemp.Tables[0].Rows[0]["Anexo"];
                long tamanho = image1.ContentBytes.Length;
                e.DisplayText = tamanho.ToString();

                if (tamanho <= 1023)
                {
                    e.DisplayText = tamanho.ToString() + "B";
                }
                if (tamanho > 1024 && tamanho <= (1024 * 1024))
                {
                    e.DisplayText = (tamanho / 1024).ToString() + "KB";
                }
                if (tamanho > (1024 * 1024) && tamanho <= (1024 * 1024 * 1024))
                {
                    e.DisplayText = (tamanho / (1024 * 1024)).ToString() + "MB";
                }
            }
        }
    }

    protected void callbackArquivo_Callback(object source, CallbackEventArgs e)
    {
        string CodigoAnexo = "-1";
        string comandoSQL = "";
        string indicaPasta = "A";
        string nomeComando = e.Parameter.Split('|')[0];
        string area = e.Parameter.Split('|')[1];
        string relativeFolder = "";
        string nomeArquivo = "";

        if (area == "Folders")
        {
            relativeFolder = fmArquivos.SelectedFolder.RelativeName;
            indicaPasta = "P";
            nomeArquivo = fmArquivos.SelectedFolder.Name;
        }
        else if (fmArquivos.SelectedFile != null)
        {
            relativeFolder = fmArquivos.SelectedFile.RelativeName;
            indicaPasta = "A";
            nomeArquivo = fmArquivos.SelectedFile.Name;
        }

        if (relativeFolder.LastIndexOf('\\') == -1)
            relativeFolder = "";
        else
            relativeFolder = relativeFolder.Substring(0, relativeFolder.LastIndexOf('\\')).Replace("\\", "/");

        comandoSQL = string.Format(@"
            SELECT a.CodigoAnexo
              FROM Anexo a INNER JOIN
			       AnexoAssociacao aa ON aa.CodigoAnexo = a.CodigoAnexo
									 AND aa.CodigoObjetoAssociado = {4}
									 AND aa.CodigoTipoAssociacao = {5}
             WHERE a.Nome = '{3}'
               AND a.DataExclusao IS NULL
               AND (ISNULL(dbo.f_GetCaminhoPastasAnexo(a.CodigoAnexo), '') = '{2}' OR aa.IndicaLinkCompartilhado = 'S')", cDados.getDbName()
                , cDados.getDbOwner()
                , relativeFolder.Replace("'", "''")
                , nomeArquivo.Replace("'", "''")
                , IDObjetoAssociado
                , codigoTipoAssociacao);


        if (comandoSQL != "")
        {
            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                CodigoAnexo = ds.Tables[0].Rows[0]["CodigoAnexo"].ToString();
            }
        }

        callbackArquivo.JSProperties["cp_CA"] = CodigoAnexo;
        callbackArquivo.JSProperties["cp_TA"] = IniciaisTipoAssociacao;
        callbackArquivo.JSProperties["cp_COA"] = IDObjetoAssociado;
        callbackArquivo.JSProperties["cp_COP"] = IDObjetoPai;
        callbackArquivo.JSProperties["cp_CN"] = nomeComando;
        callbackArquivo.JSProperties["cp_Nome"] = nomeArquivo;
        callbackArquivo.JSProperties["cp_O"] = indicaPasta;
    }

    protected void fmArquivos_CustomThumbnail(object source, FileManagerThumbnailCreateEventArgs e)
    {
        string comandoSQL = "";

        comandoSQL = string.Format(@"
        SELECT a.IndicaControladoSistema, aa.IndicaLinkCompartilhado 
          FROM {0}.{1}.Anexo a INNER JOIN
               {0}.{1}.AnexoAssociacao aa ON aa.CodigoAnexo = a.CodigoAnexo
                                         AND aa.CodigoObjetoAssociado = {4}
                                         AND aa.CodigoTipoAssociacao = {5}
         WHERE CONVERT(varchar(30), DataInclusao, 120) = CONVERT(varchar(30), CONVERT(DateTime, '{2:dd/MM/yyyy HH:mm:ss}', 103), 120) 
           AND Nome = '{3}'
           AND a.DataExclusao IS NULL", cDados.getDbName()
             , cDados.getDbOwner()
             , e.Item.LastWriteTime
             , e.Item.Name.Replace("'", "''")
                 , IDObjetoAssociado
                 , codigoTipoAssociacao);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["IndicaLinkCompartilhado"].ToString() == "S" && IniciaisTipoAssociacao != "EN")
            {
                e.ThumbnailImage.Url = "~/imagens/anexo/arquivoLink.png";
                e.ThumbnailImage.ToolTip = Resources.traducao.frameEspacoTrabalho_BibliotecaInterno_link_para_o_arquivo__ + e.Item.Name;

                if (!somenteLeitura)
                {
                    FileManagerFileAccessRule permissao = new FileManagerFileAccessRule();
                    permissao.Path = e.Item.RelativeName;
                    permissao.Edit = Rights.Allow;
                    permissao.Download = Rights.Allow;


                    fmArquivos.SettingsPermissions.AccessRules.Add(permissao);
                }
            }
            else
            {
                if (!somenteLeitura && ds.Tables[0].Rows[0]["IndicaControladoSistema"].ToString() == "S")
                {
                    FileManagerFileAccessRule permissao = new FileManagerFileAccessRule();
                    permissao.Path = e.Item.RelativeName;
                    permissao.Edit = Rights.Deny;

                    fmArquivos.SettingsPermissions.AccessRules.Add(permissao);
                }
            }

        }
    }

    private void ApplyRules(FileManagerFolder pastaSelecionada)
    {
        try
        {
            FileManagerFolder[] pastas = pastaSelecionada.GetFolders();

            for (int i = 0; i < pastas.Length; i++)
            {
                if (pastas[i].Id != "")
                {
                    string relativeFolder = pastas[i].RelativeName;

                    if (relativeFolder.LastIndexOf('\\') == -1)
                        relativeFolder = "";
                    else
                        relativeFolder = relativeFolder.Substring(0, relativeFolder.LastIndexOf('\\')).Replace("\\", "/");

                    string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoPasta INT
            SELECT @CodigoPasta = a.CodigoAnexo
              FROM Anexo a INNER JOIN
			       AnexoAssociacao aa ON aa.CodigoAnexo = a.CodigoAnexo
									 AND aa.CodigoObjetoAssociado = {5}
									 AND aa.CodigoTipoAssociacao = {6}
             WHERE a.Nome = '{4}'
               AND a.DataExclusao IS NULL
               AND ISNULL(dbo.f_GetCaminhoPastasAnexo(a.CodigoAnexo), '') = '{3}'

            select * from dbo.f_pax_GetPermissoesAnexoUsuario({2}, {5}, '{7}', {8}, @CodigoPasta, {9});

        END", cDados.getDbName()
                            , cDados.getDbOwner()
                            , int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString())
                            , relativeFolder.Replace("'", "''")
                            , pastas[i].Name.Replace("'", "''")
                            , IDObjetoAssociado
                            , codigoTipoAssociacao
                            , IniciaisTipoAssociacao
                            , IDObjetoPai
                            , int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()));

                    DataSet ds = cDados.getDataSet(comandoSQL);

                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        string consultar = ds.Tables[0].Rows[0]["ValorAcessoConsultar"].ToString();
                        string incluir = ds.Tables[0].Rows[0]["ValorAcessoIncluir"].ToString();
                        string alterar = ds.Tables[0].Rows[0]["ValorAcessoAlterar"].ToString();
                        string excluir = ds.Tables[0].Rows[0]["ValorAcessoExcluir"].ToString();

                        FileManagerFolderAccessRule permissao = new FileManagerFolderAccessRule(pastas[i].RelativeName);

                        if (consultar == "0" || consultar == "2" || consultar == "6")
                            permissao.Browse = Rights.Deny;
                        else if (consultar == "1" || consultar == "5")
                            permissao.Browse = Rights.Allow;

                        if (excluir == "0" || excluir == "2" || excluir == "6")
                            permissao.EditContents = Rights.Deny;
                        else if (excluir == "1" || excluir == "5")
                            permissao.EditContents = Rights.Allow;

                        if (alterar == "0" || alterar == "2" || alterar == "6")
                            permissao.Edit = Rights.Deny;
                        else if (alterar == "1" || alterar == "5")
                            permissao.Edit = Rights.Allow;

                        if (incluir == "0" || incluir == "2" || incluir == "6")
                            permissao.Upload = Rights.Deny;
                        else if (incluir == "1" || incluir == "5")
                            permissao.Upload = Rights.Allow;

                        fmArquivos.SettingsPermissions.AccessRules.Add(permissao);
                    }
                }

                ApplyRules(pastas[i]);
            }
        }
        catch { }
    }

    protected void fmArquivos_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        if (fmArquivos.SelectedFolder != null)
            ApplyRules(fmArquivos.SelectedFolder.Parent == null ? fmArquivos.SelectedFolder : fmArquivos.SelectedFolder.Parent);

        fmArquivos.DataBind();
    }

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        fmArquivos.SelectedFile = null;
        fmArquivos.DataBind();
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.Parameters["@PalavraChave"].Value = (txtPesquisa.Text ?? string.Empty).Trim();
        e.Command.Parameters["@NomePastaRaiz"].Value = fmArquivos.Settings.RootFolder;
    }

    protected void fmArquivos_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        if (Session["existeAnexoMesmoNome"].ToString() == "1")
        {
            e.Properties["cpErro"] = "O nome do arquivo que você deseja anexar já existe para esse projeto.";
            Session["existeAnexoMesmoNome"] = 0;
        }
    }

    protected void fmArquivos_DetailsViewCustomColumnDisplayText(object source, FileManagerDetailsViewCustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.Name == "colNomeUsuarioInclusao")
        {
            string where = string.Format(" AND u.CodigoUsuario IN (SELECT CodigoUsuarioInclusao FROM Anexo WHERE CodigoAnexo IN ({0})) ", e.Item.Id);
            DataSet ds = cDados.getUsuarios(where);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                e.DisplayText = ds.Tables[0].Rows[0]["NomeUsuario"].ToString();
            }
        }
    }
}