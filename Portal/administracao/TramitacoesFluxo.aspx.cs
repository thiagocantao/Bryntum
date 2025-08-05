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
using System.IO;
using System.Web.Hosting;

public partial class administracao_TramitacoesFluxo : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    
    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

    private bool podeInteragir = false;

    private int codigoWorkflow = 0;
    private int codigoEtapaWf = 0;
    private int codigoInstanciaWf = 0;
    private int sequencia = 0;
    bool readOnly;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        readOnly = "S".Equals(Request.QueryString["RO"]);
        
                
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {            
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }

        codigoWorkflow = int.Parse(string.IsNullOrEmpty(Request.QueryString["CWF"]) ? "-1" : Request.QueryString["CWF"]);
        codigoEtapaWf = int.Parse(string.IsNullOrEmpty(Request.QueryString["CEWF"]) ? "-1" : Request.QueryString["CEWF"]);
        codigoInstanciaWf = int.Parse(string.IsNullOrEmpty(Request.QueryString["CIWF"]) ? "-1" : Request.QueryString["CIWF"]);
        sequencia = int.Parse(string.IsNullOrEmpty(Request.QueryString["CSOWF"]) ? "-1" : Request.QueryString["CSOWF"]);

        podeInteragir = cDados.getAcessoInteracaoFluxo(codigoEntidadeUsuarioResponsavel, codigoWorkflow, codigoInstanciaWf, codigoEtapaWf, sequencia, codigoUsuarioResponsavel);

        podeEditar =  !readOnly && podeInteragir;
        podeIncluir = !readOnly && podeInteragir;
        podeExcluir = !readOnly && podeInteragir;
        btnSolicitarParecer.ClientVisible = !readOnly && podeInteragir;

        if (!IsPostBack)
        {
            carregaGvDados();
        }

        carregaComboUsuarios();
        montaTextoPadrao();

        configuraBotaoAnexo();
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void configuraBotaoAnexo()
    {
        string comandoSQL = string.Format(@"
                     SELECT CodigoTramitacaoEtapaFluxo 
                       FROM {0}.{1}.TramitacaoEtapaFluxo
                      WHERE CodigoUsuarioParecer = {2}
                        AND DataParecer IS NULL
                        AND DataExclusao IS NULL
                        AND CodigoWorkflow = {3}
                        AND CodigoInstancia =  {4}
                        ", cDados.getDbName()
                         , cDados.getDbOwner()
                         , codigoUsuarioResponsavel
                         , codigoWorkflow
                         , codigoInstanciaWf);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string urlAnexos = "espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=PT&ID=" + ds.Tables[0].Rows[0]["CodigoTramitacaoEtapaFluxo"] + "&ALT=425";

            btnAnexo.ClientSideEvents.Click = "function(s,e){window.top.showModal(window.top.pcModal.cp_Path + '" + urlAnexos + "', 'Anexos', 800, 430, '', null);}";
            btnAnexo.ClientVisible = true;
        }
        else
        {
            btnAnexo.ClientVisible = false;
        }
    }

    #region VARIOS

    private void montaTextoPadrao()
    {
        string texto = "", nomeSistema = "", linkSistema = "", nomeUsuarioLogado = "";

        texto = cDados.getTextoPadraoEntidade(codigoEntidadeUsuarioResponsavel, "CorpoSlcPrcEtpTrm");

        if (cDados.getInfoSistema("NomeUsuarioLogado") != null)
            nomeUsuarioLogado = cDados.getInfoSistema("NomeUsuarioLogado").ToString();

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "tituloPaginasWEB", "urlAplicacao_AcessoInternet");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            nomeSistema = dsParametros.Tables[0].Rows[0]["tituloPaginasWEB"].ToString();
            linkSistema = dsParametros.Tables[0].Rows[0]["urlAplicacao_AcessoInternet"].ToString();
        }

        texto = texto.Replace("[nomeUsuario]", nomeUsuarioLogado);
        texto = texto.Replace("[nomeSistema]", nomeSistema);
        texto = texto.Replace("[linkSistema]", linkSistema);

        gvDados.JSProperties["cp_TextPadrao"] = texto;
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 550;
    }
    #endregion
    
    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getUsuariosParecerFluxo(codigoWorkflow, codigoInstanciaWf, codigoEtapaWf, sequencia, "");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            DataRow[] dr = ds.Tables[0].Select("DataParecer IS NULL AND DataSolicitacaoParecer IS NOT NULL AND CodigoUsuarioParecer = " + codigoUsuarioResponsavel);

            if (dr.Length > 0 && !readOnly)
            {
                ASPxPageControl1.TabPages.FindByName("tbComentarios").Enabled = true;

                txtParecerUsuario.Text = dr[0]["Parecer"].ToString();
                txtComentarioSolicitacaoParecer.Text = dr[0]["ComentarioSolicitacaoParecer"].ToString();
                if (dr[0]["Parecer"].ToString() != "")
                    pnCallback.JSProperties["cp_EnviarParecer"] = "S";

                if (!podeInteragir)
                {
                    ASPxPageControl1.ActiveTabPage = ASPxPageControl1.TabPages.FindByName("tbComentarios");                    
                }
            }
            else if (dr.Length == 0)
            {
                ASPxPageControl1.TabPages.FindByName("tbComentarios").Enabled = false;
                ASPxPageControl1.TabPages.FindByName("tbGestao").ClientVisible = true;
            }
        }        
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == DevExpress.Web.GridViewTableCommandCellType.Data)
        {
            bool podeInteragirRegistro = verificaEdicaoLinha(e.VisibleIndex);

            if (e.ButtonID == "btnEditar")
            {
                if (podeEditar && podeInteragirRegistro)
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
                if (podeExcluir && podeInteragirRegistro)
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

    }

    private bool verificaEdicaoLinha(int index)
    {
        bool retorno = true;
        string dataParecer = gvDados.GetRowValues(index, "DataParecer").ToString();
        int codigoEtapaLinha = int.Parse(gvDados.GetRowValues(index, "CodigoEtapa").ToString());
        int sequenciaLinha = int.Parse(gvDados.GetRowValues(index, "SequenciaEtapa").ToString());

        if (dataParecer != "" || codigoEtapaLinha != codigoEtapaWf || sequenciaLinha != sequencia)
            retorno = false;
        
        return retorno;
    }

    #endregion

    
    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {

        string dataPrevista = ddlDataPrevista.Text == "" ? "NULL" : string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrevista.Date);

        int novoCodigoTramitacaoEtapaFluxo = 0;

        bool result = cDados.incluiUsuarioTramitacao(codigoWorkflow, codigoInstanciaWf, codigoEtapaWf, sequencia, int.Parse(ddlUsuario.Value.ToString()), codigoUsuarioResponsavel, dataPrevista, txtComentarios.Text, ref novoCodigoTramitacaoEtapaFluxo);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvDados();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigoTramitacaoEtapaFluxo);
            gvDados.Selection.SelectRow(gvDados.FocusedRowIndex);
            return "";
        }

    }

    private string persisteParecerRegistro()
    {
        string parecer = txtParecerUsuario.Text;

        gvDados.ClientVisible = true;

        bool result = cDados.atualizaParecerUsuario(codigoUsuarioResponsavel, codigoWorkflow, codigoInstanciaWf, parecer);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            pnCallback.JSProperties["cp_EnviarParecer"] = "S";
            carregaGvDados();
            ASPxPageControl1.ActiveTabPage = ASPxPageControl1.TabPages.FindByName("tbComentarios");
            return "";
        }

    }

    private string persisteEnvioParecerRegistro()
    {
        string parecer = txtParecerUsuario.Text;
        int codigoItem = 0;

        int[] array = new int[gvDados.GetSelectedFieldValues("CodigoTramitacaoEtapaFluxo").Count];

        gvDados.ClientVisible = true;

        if (array.Length == 0)
            return "";

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(gvDados.GetSelectedFieldValues("CodigoTramitacaoEtapaFluxo")[i].ToString());
            codigoItem = array[i];
        }
            

        bool result = cDados.enviaParecerUsuario(codigoUsuarioResponsavel, array);

        carregaGvDados();

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoItem);
            return "";
        }

    }
    
    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoTramitacao = int.Parse(getChavePrimaria());

        string dataPrevista = ddlDataPrevista.Text == "" ? "NULL" : string.Format(@"CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrevista.Date);

        bool result = cDados.atualizaUsuarioTramitacao(codigoTramitacao, codigoUsuarioResponsavel, dataPrevista, txtComentarios.Text);

        carregaGvDados();

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            return "";
        }    
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoTramitacao = int.Parse(getChavePrimaria());

        bool result = cDados.excluiUsuarioTramitacao(codigoTramitacao, codigoUsuarioResponsavel);

        carregaGvDados();

        if (result == false)
            return "Erro ao excluir o registro!";
        else
        {            
            return "";
        }
    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    private void carregaComboUsuarios()
    {
        int codigoUsuarioSelecionado = -1;

        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Excluir" && gvDados.FocusedRowIndex != -1)
        {
            codigoUsuarioSelecionado = int.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoUsuarioParecer").ToString());
        }

        string where = string.Format(
                   @"AND us.CodigoUsuario NOT IN(SELECT CodigoUsuarioParecer
                                                   FROM {0}.{1}.TramitacaoEtapaFluxo tef
                                                  WHERE tef.CodigoWorkflow = {2}
                                                    AND tef.CodigoInstancia = {3}
                                                    AND tef.CodigoEtapa = {4}
                                                    AND tef.SequenciaEtapa = {5}
                                                    AND tef.DataExclusao IS NULL
                                                    AND tef.DataParecer IS NULL)             
               ", cDados.getDbName(), cDados.getDbOwner(), codigoWorkflow, codigoInstanciaWf, codigoEtapaWf, sequencia, codigoUsuarioSelecionado);

        

        DataSet ds = cDados.getUsuariosAtivosEntidade(codigoEntidadeUsuarioResponsavel, where);

        ddlUsuario.TextField = "NomeUsuario";
        ddlUsuario.ValueField = "CodigoUsuario";

        ddlUsuario.Columns[0].FieldName = "NomeUsuario";
        ddlUsuario.Columns[1].FieldName = "EMail";
        ddlUsuario.TextFormatString = "{0}";
        ddlUsuario.DataSource = ds;
        ddlUsuario.DataBind();
    }

    protected void cbExportacao_Callback(object source, CallbackEventArgs e)
    {
        string nomeFluxo = "";
        string nomeEtapa = "";

        string sqlNomeFluxoNomeEtapa = string.Format(@"
       BEGIN 
          DECLARE @CodigoWorkflow as int
          DECLARE @CodigoInstanciaWf as int
          DECLARE @CodigoEtapaWf as int
          DECLARE @SequenciaOcorrenciaEtapaWf as int

          SET @CodigoWorkflow = {2}
          SET @CodigoInstanciaWf = {3}
          SET @CodigoEtapaWf = {4}
          SET @SequenciaOcorrenciaEtapaWf = {5}

          SELECT iw.NomeInstancia, ewf.DescricaoEtapa
            FROM {0}.{1}.InstanciasWorkflows iw 
            INNER JOIN {0}.{1}.EtapasInstanciasWf eiwf ON (eiwf.CodigoInstanciaWf = iw.CodigoInstanciaWf)
            INNER JOIN {0}.{1}.EtapasWf ewf ON (ewf.CodigoEtapaWf = eiwf.CodigoEtapaWf)
          WHERE eiwf.CodigoWorkflow = @CodigoWorkflow
            AND ewf.CodigoWorkflow = @CodigoWorkflow
            AND eiwf.CodigoInstanciaWf = @CodigoInstanciaWf
            AND eiwf.CodigoEtapaWf = @CodigoEtapaWf 
            AND eiwf.SequenciaOcorrenciaEtapaWf = @SequenciaOcorrenciaEtapaWf
      END", cDados.getDbName(), cDados.getDbOwner(), codigoWorkflow, codigoInstanciaWf, codigoEtapaWf, sequencia);

        DataSet dsNomeFluxoNomeEtapa = cDados.getDataSet(sqlNomeFluxoNomeEtapa);
        if (cDados.DataSetOk(dsNomeFluxoNomeEtapa) && cDados.DataTableOk(dsNomeFluxoNomeEtapa.Tables[0]))
        {
            nomeFluxo = dsNomeFluxoNomeEtapa.Tables[0].Rows[0]["NomeInstancia"].ToString();
            nomeEtapa = dsNomeFluxoNomeEtapa.Tables[0].Rows[0]["DescricaoEtapa"].ToString();
        }

        string montaNomeImagemParametro = MontaNomeImagemParametro();
        
        relTramitacao relatoriox = new relTramitacao(codigoWorkflow, codigoInstanciaWf);
        MemoryStream stream = new MemoryStream();
        
        relatoriox.Parameters["p_NomeEtapa"].Value = nomeEtapa;
        relatoriox.Parameters["p_NomeFluxo"].Value = nomeFluxo;
        relatoriox.Parameters["p_UrlLogo"].Value = montaNomeImagemParametro;
        relatoriox.CreateDocument();
        relatoriox.ExportToPdf(stream);
        Session["exportStream"] = stream;
    }

    private string MontaNomeImagemParametro()
    {
        DataSet dsLogoEntidade = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");
        ASPxBinaryImage image1 = new ASPxBinaryImage();

        string montaNomeArquivo = "";
        string montaNomeImagemParametro = "";

        if (cDados.DataSetOk(dsLogoEntidade) && cDados.DataTableOk(dsLogoEntidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoEntidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];

                if (image1.ContentBytes != null)
                {
                    string pathArquivo = "logoEntidadeRelTram_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + pathArquivo;
                    montaNomeImagemParametro = @"~\ArquivosTemporarios\" + pathArquivo;
                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();

                }
            }
            catch (Exception ex)
            {
                string mensage = ex.Message;
            }
        }
        return montaNomeImagemParametro;
    }

    protected void pnCallback_Callback1(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        if (e.Parameter == "GravarParecer")
        {
            mensagemErro_Persistencia = persisteParecerRegistro();
        }
        if (e.Parameter == "Parecer")
        {
            mensagemErro_Persistencia = persisteEnvioParecerRegistro();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            carregaComboUsuarios();
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

}
