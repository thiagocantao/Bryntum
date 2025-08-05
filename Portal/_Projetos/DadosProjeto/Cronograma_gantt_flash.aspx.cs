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
using System.Web.Hosting;
using System.IO;
using System.Xml;
using CDIS;
 
public partial class _Projeto_DadosProjeto_Cronograma_gantt_flash : System.Web.UI.Page
{
    dados cDados;

    //pega a data e hora atual do sistema
    string dataHora;

    public int codigoProjeto = 0;
    
    public string grafico_xml = "";
    public string alturaGrafico = "", larguraGrafico = "", nenhumGrafico = "";
    public string nomeProjeto = "";
    public int codigoEntidadeUsuarioResponsavel = -1;
    private string utilizaCronoInstalado = "N", utilizaNovaEAP = "N";
    int codigoUsuario = -1;
    public string estiloFooter = "dxtlFooter";
    private string imprimeDadosLinhaBaseCronograma = "N";
    private string where = "";
    private int versaoLinhaBase;
    string codigoCronogramaReplanejamento = "", codigoCronogramaOficial = "", eapBloqueadaEm = "", eapBloqueadaPor = "";
    bool edicaoEAP = false;

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

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        
        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
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

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter += "_" + cssPostfix;

        dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";
        
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }
        
        if (Request.QueryString["NP"] != null && Request.QueryString["NP"].ToString() != "")
        {
            nomeProjeto = Request.QueryString["NP"].ToString();
        }

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CnsCrn");
        }               

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "utilizaCronoInstalado", "utilizaNovaEAP");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
        {
            if (dsParam.Tables[0].Rows[0]["utilizaCronoInstalado"].ToString() != "")
                utilizaCronoInstalado = dsParam.Tables[0].Rows[0]["utilizaCronoInstalado"].ToString();

            if (dsParam.Tables[0].Rows[0]["utilizaNovaEAP"].ToString() != "")
                utilizaNovaEAP = dsParam.Tables[0].Rows[0]["utilizaNovaEAP"].ToString();
        }

        verificaPermissoesBotoes();

        hfCodigoProjeto.Set("CodigoProjeto", codigoProjeto);
        hfCodigoProjeto.Set("NomeProjeto", nomeProjeto);

        defineLarguraTela();

        carregaComboRecursos();
        carregaComboLinhaBase();

        if (!IsPostBack && Request.QueryString["Atrasadas"] != null && Request.QueryString["Atrasadas"].ToString() == "S")
        {
            ckTarefasAtrasadas.Checked = true;
        }

        if (!IsPostBack && Request.QueryString["ApenasMarcos"] != null && Request.QueryString["ApenasMarcos"].ToString() == "S")
        {
            ckMarcos.Checked = true;
        }

        //Função que gera o gráfico
        geraGrafico();

        imgGraficos.Style.Add("cursor", "pointer");

        verificaBloqueioCronograma();

        defineOpcoesEAP();
        getIconeAlertaCronograma();

        tbBotoes.Attributes.Add("class", estiloFooter);
        tbLegenda.Attributes.Add("class", estiloFooter);

        lblMsgFlash.Font.Size = new FontUnit("7pt");
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        alturaGrafico = (altura - 300).ToString();
        larguraGrafico = (largura - 175).ToString();
    }



    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico()
    {        
        //cria  a variável para armazenar o XML
        string xml = "";
        string xmlZoom = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/GanttTarefasProjeto_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;
        string nomeGraficoZoom = @"/ArquivosTemporarios/GanttTarefasProjeto_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "Zoom_" + dataHora;

        bool removerIdentacao = false;


        if (ckMarcos.Checked)
        {
            where += " AND f.Marco = 1 ";
            removerIdentacao = true;
        }

        if (ckTarefasAtrasadas.Checked)
        {
            bool bModoCalculoAtrasoTotal = false;
            DataSet dsTemp = cDados.getParametrosSistema("calculoDesempenhoFisicoTarefa");
            if (cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) 
            {
                string modoCalculoAtraso = dsTemp.Tables[0].Rows[0]["calculoDesempenhoFisicoTarefa"].ToString();
                bModoCalculoAtrasoTotal = modoCalculoAtraso.ToUpper().Equals("TOTAL");
            }

            if (bModoCalculoAtrasoTotal)
                where += " AND f.TarefaResumo = 0  AND (f.TerminoPrevisto < GetDate() AND f.TerminoReal IS NULL AND f.PercentualReal <> 100) ";
            else
                where += " AND f.TarefaResumo =  0 AND f.PercentualReal < f.PercentualPrevisto ";
            removerIdentacao = true;

        }

        if (ckNaoConcluidas.Checked)
        {
            where += " AND f.TarefaResumo = 0 AND f.PercentualReal <> 100 ";
            removerIdentacao = true;
        }
        if (ddlRecurso.Value.ToString() != "-1")
        {
            where += " AND f.TarefaResumo = 0 ";
            removerIdentacao = true;
        }

        if (txtConcluido.Text != "")
        {
            where += " AND f.PercentualReal = " + txtConcluido.Text;
            removerIdentacao = true;
        }

        if (ddlData.Text != "")
        {
            where += string.Format(" AND CONVERT(DateTime, '{0}', 103) BETWEEN InicioPrevisto AND TerminoPrevisto", ddlData.Date);
            removerIdentacao = true;
        }

        versaoLinhaBase = ddlLinhaBase.SelectedIndex == -1 ? -1 : int.Parse(ddlLinhaBase.Value.ToString());

        var percentualConcluido = txtConcluido.Value == null ? new int?() : Convert.ToInt32(txtConcluido.Value);
        var data = ddlData.Value == null ? new DateTime?() : ddlData.Date;
        DataSet ds = cDados.getCronogramaGantt(codigoProjeto, ddlRecurso.Value.ToString(), versaoLinhaBase, removerIdentacao, ckTarefasAtrasadas.Checked, ckMarcos.Checked, percentualConcluido, data);

        bool indicaCronogramaTasques = cDados.indicaCronogramaVersaoTasques(codigoProjeto);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            xml = cDados.getGraficoGanttTarefasProjeto(ds.Tables[0], removerIdentacao, indicaCronogramaTasques);
            xmlZoom = cDados.getGraficoZoomGanttTarefasProjeto(ds.Tables[0], removerIdentacao, indicaCronogramaTasques);
            imgGraficos.ClientEnabled = true;
        }
        else
        {
            nenhumGrafico = cDados.getGanttVazio((int.Parse(alturaGrafico) - 30).ToString());
            
            alturaGrafico = "0";
            
            imgGraficos.ClientEnabled = false;
        }

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);
        cDados.escreveXML(xmlZoom, nomeGraficoZoom);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico;
    }

    private void carregaComboRecursos()
    {
        DataSet dsRecursos = cDados.getRecursosCronograma(codigoProjeto, "");

        if (cDados.DataSetOk(dsRecursos))
        {
            ddlRecurso.DataSource = dsRecursos;

            ddlRecurso.TextField = "Recurso";

            ddlRecurso.ValueField = "CodigoRecurso";

            ddlRecurso.DataBind();
        }

        ListEditItem lei = new ListEditItem("Todos os Recursos", "-1");

        ddlRecurso.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlRecurso.SelectedIndex = 0;
    }

    private void carregaComboLinhaBase()
    {
        DataSet dsLinhaBase = cDados.getVersoesLinhaBase(codigoProjeto, "");

        if (cDados.DataSetOk(dsLinhaBase))
        {
            ddlLinhaBase.DataSource = dsLinhaBase;

            ddlLinhaBase.TextField = "VersaoLinhaBase";

            ddlLinhaBase.ValueField = "NumeroVersao";

            ddlLinhaBase.DataBind();
        }

        if (!IsPostBack)
            ddlLinhaBase.SelectedIndex = 0;

        carregaInformacoesLBSelecionada();
    }

    private void carregaInformacoesLBSelecionada()
    {
        if (ddlLinhaBase.SelectedIndex != -1)
        {
            DataSet dsLinhaBase = cDados.getDetalhesLinhaBase(codigoProjeto, "" + ddlLinhaBase.Value);

            if (cDados.DataSetOk(dsLinhaBase) && cDados.DataTableOk(dsLinhaBase.Tables[0]))
            {
                DataRow dr = dsLinhaBase.Tables[0].Rows[0];

                txtVersao.Text = dr["Descricao"].ToString();
                txtStatus.Text = dr["StatusLB"].ToString();
                txtDataSolicitacao.Value = dr["DataSolicitacao"];
                txtSolicitante.Text = dr["Solicitante"].ToString();
                txtDataAprovacao.Value = dr["DataAprovacao"];
                txtAprovador.Text = dr["Aprovador"].ToString();
                txtAnotacao.Text = dr["Anotacao"].ToString();
            }
        }
    }
    
    private void abreCronograma()
    {        
        string msgErro = "";
        int tamanhoCodigoProjeto = codigoProjeto.ToString().Length, regAf = 0;
        string minuto = string.Format("{0:D2}", DateTime.Now.Minute);
        string guid = System.Guid.NewGuid().ToString("D").Replace("-", "");
        string dia = string.Format("{0:D2}", DateTime.Now.Day);
        string hora = string.Format("{0:D2}", DateTime.Now.Hour);

        string stringCrono = tamanhoCodigoProjeto.ToString() + minuto + guid + dia + hora + codigoProjeto;

        bool retorno = cDados.atualizaCronogramaAcessoUsuario(codigoUsuario, stringCrono, ref regAf, ref msgErro);

        if (retorno)
        {
            string corpoTexto = "";
            string extensao = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuario.ToString() + ".tsq";
            string nomeArquivoGravacao = "/ArquivosTemporarios/Crono_" + extensao;
            string urlApp = cDados.getPathSistema();
            string chaveApp = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();

            chaveApp = chaveApp.Replace("-", "").Replace("{", "").Replace("}", "");

            string identificacaoProduto = string.Format("{0} - {1:dd/MM/yyyy}", 
                System.Configuration.ConfigurationManager.AppSettings["nomeSistema"].ToString()
               ,DateTime.Now);

            string chaveCriptografada = Cripto.criptografar(stringCrono + urlApp, chaveApp);

            corpoTexto = identificacaoProduto + Environment.NewLine +
                chaveApp + Environment.NewLine +
                chaveCriptografada;

            cDados.escreveTexto(corpoTexto, nomeArquivoGravacao);

            Response.Clear();
            Response.Buffer = false;
            Response.AppendHeader("Content-Type", "application/unknown");
            Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"Crono_" + extensao + "\"");
            Response.WriteFile(urlApp + nomeArquivoGravacao.Substring(1));
            Response.End();

            //callbackEditarCronograma.JSProperties["cp_Crono"] = urlApp + nomeArquivoGravacao.Substring(1);            
        }        
    }

    protected void btnEditarCronograma_Click(object sender, EventArgs e)
    {
        abreCronograma();
    }

    private void verificaBloqueioCronograma()
    {
        DataSet ds = cDados.getCronogramasComCheckout(codigoEntidadeUsuarioResponsavel, " AND cp.CodigoProjeto = " + codigoProjeto);

        string eventoBotaoEditarCrono = "", processOnServer = "", eventoClickOnce = "";

        if (utilizaCronoInstalado == "S")
        {
            eventoBotaoEditarCrono = "pcDownload.Show();";
        }
        else
        {
            string linkOpcao = cDados.getLinkPortalDesktop(Request.Url, codigoEntidadeUsuarioResponsavel, codigoUsuario, codigoProjeto, "./../../");
            eventoBotaoEditarCrono = "window.open('" + linkOpcao + "', 'framePrincipal'); ";

            eventoBotaoEditarCrono += getComandoDownloadClickOnce();

            processOnServer = "e.processOnServer = false;";
        }

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            btnDesbloquear.ClientEnabled = true;

            string mensagem = "O cronograma está bloqueado com o usuário " + ds.Tables[0].Rows[0]["NomeUsuarioCheckoutCronograma"] + "."
                + Environment.NewLine + "Deseja abrir o cronograma somente para leitura?";

            lblInformacao.Text = mensagem;

            btnEditarCronograma.ClientSideEvents.Click = "function(s, e) {pcInformacao.Show();e.processOnServer = false;}";
            btnEditarCronograma2.ClientSideEvents.Click = "function(s, e) {pcInformacao.Show();e.processOnServer = false;}";
            btnAbrirCronoBloqueado.ClientSideEvents.Click = "function(s, e) {pcInformacao.Hide();" + eventoBotaoEditarCrono + processOnServer + "}";

            string mensagemDesbloqueio = "O cronograma está bloqueado com o usuário " + ds.Tables[0].Rows[0]["NomeUsuarioCheckoutCronograma"] + "."
                + Environment.NewLine + "Ao fazer o desbloqueio as atualizações pendentes serão perdidas. Deseja realmente desbloquear o projeto?";

            lblDesbloqueio.Text = mensagemDesbloqueio;
        }
        else
        {
            btnDesbloquear.ClientEnabled = false;
            btnEditarCronograma.ClientSideEvents.Click = "function(s, e) {" + eventoBotaoEditarCrono + processOnServer + "}";
            btnEditarCronograma2.ClientSideEvents.Click = "function(s, e) {" + eventoBotaoEditarCrono + processOnServer + "}";
        }

        if (edicaoEAP && eapBloqueadaEm != "")
        {
            btnDesbloquear.ClientEnabled = true;

            string mensagem = "A EAP está bloqueada com o usuário " + eapBloqueadaPor + "."
                + Environment.NewLine + "Deseja abrir somente para leitura?";

            lblInformacaoEAP.Text = mensagem;           
        }
    }

    private string getComandoDownloadClickOnce()
    {
        string comandoDownload = "";

        string comandoSQL = string.Format(@"
            SELECT Valor, DescricaoParametro_PT, DescricaoParametro_EN, DescricaoParametro_ES
              FROM ParametroConfiguracaoSistema
             WHERE CodigoEntidade = {0}
               AND Parametro = 'ClickOnce{1}'", codigoEntidadeUsuarioResponsavel, Request.Browser.Browser);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            imgClickOnce.ClientVisible = true;
            lblDownloadOnce.Text = dr["DescricaoParametro_PT"].ToString().Replace("@link", dr["Valor"].ToString());
        }

        return comandoDownload;
    }

    private void defineOpcoesEAP()
    {
        if (btnEditarEAP.ClientVisible || btnVisualizarEAP.ClientVisible)
        { 
            string codigoCrono = edicaoEAP ? codigoCronogramaReplanejamento : codigoCronogramaOficial;
            string urlEAP = "EdicaoEAP.aspx?IDProjeto=" + codigoProjeto + "&CCR=" + codigoCrono + "&CU=" + codigoUsuario + "&CE=" + codigoEntidadeUsuarioResponsavel + "&NP=" + nomeProjeto;
            if(utilizaNovaEAP == "S")
                urlEAP = "../../GoJs/EAP/graficoEAP.aspx?IDProjeto=" + codigoProjeto + "&CCR=" + codigoCrono + "&CU=" + codigoUsuario + "&CE=" + codigoEntidadeUsuarioResponsavel + "&NP=" + nomeProjeto;
            string corpoEvento = string.Format(@"
                                             var Thiswidth=screen.availWidth - 60;
                                             var Thisheight=screen.availHeight - 150;
                                             var myArguments = new Object();
   		                                     myArguments.param1 = '{0}';
                                             myArguments.param2 = 'ARG1';
                                             window.top.showModal('{1}&AM=ARG2&Altura=' + (Thisheight - 40), 'Edição', Thiswidth, Thisheight, ARG3, myArguments);"
                , nomeProjeto
                , urlEAP);

            if (edicaoEAP && eapBloqueadaEm != "")
            {
                btnAbrirCronoBloqueadoEAP.ClientSideEvents.Click = "function(s, e) {" + corpoEvento.Replace("ARG1", " (VISUALIZAÇÃO) ").Replace("ARG2", "RO").Replace("ARG3", "''") + "pcInformacaoEAP.Hide();}";
            
                corpoEvento = "pcInformacaoEAP.Show();";
            }

            btnEditarEAP.ClientSideEvents.Click = "function(s, e) {" + corpoEvento.Replace("ARG1", "").Replace("ARG2", "RW").Replace("ARG3", "funcaoPosModalEdicao") + "}";
            btnVisualizarEAP.ClientSideEvents.Click = "function(s, e) {" + corpoEvento.Replace("ARG1", " (VISUALIZAÇÃO) ").Replace("ARG2", "RO").Replace("ARG3", "funcaoPosModal") + "}";
        }
    }

    private bool getPodeEditarEAP()
    {
        if (utilizaNovaEAP == "S")
        {
            string comandoSQL = string.Format(@"
               EXEC {0}.{1}.p_eap_BuscaCodigoEAP {2}
                ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);


            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow dr = ds.Tables[0].Rows[0];
                codigoCronogramaReplanejamento = dr["CronogramaReplanejamento"].ToString();
                codigoCronogramaOficial = dr["CronogramaOficial"].ToString();
                eapBloqueadaEm = string.Format("{0:dd/MM/yyyy}", dr["EAPBloqueadaEm"]);
                eapBloqueadaPor = dr["EAPBloqueadaPor"].ToString();
                edicaoEAP = dr["IndicaEAPEditavel"].ToString() == "S";

                if (codigoCronogramaOficial == "" && codigoCronogramaReplanejamento == "")
                    codigoCronogramaReplanejamento = "-1";
            }
            else
            {
                codigoCronogramaReplanejamento = "-1";
                edicaoEAP = true;
            }
        }else
        {
            string comandoSQL = string.Format(@"
                SELECT  DataUltimaGravacaoDesktop 
                FROM    {0}.{1}.CronogramaProjeto 
                WHERE   CodigoProjeto = {2}
                  AND   DataUltimaGravacaoDesktop IS NOT NULL
                ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                edicaoEAP = false;
            else
                edicaoEAP = true;

        }

        return edicaoEAP;
    }

    protected void cbkGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string parametroEAP = e.Parameter;

       if ("" != parametroEAP)
            desbloquear(parametroEAP);
    }

    private void desbloquear(string parametroEAP)
    {
        //fazer as mudanças do desbloquei do cronograma.
        //pasando como parametro o codigoEAP
        string comandoSQL = "";
        comandoSQL = string.Format(@"
            --Desbloquear Cronograma.
            EXEC {0}.{1}.[p_crono_UndoCheckoutEdicaoEAP] @in_IdEdicaoEAP = '{2}'
            ", cDados.getDbName(), cDados.getDbOwner(), parametroEAP);
        System.Diagnostics.Debug.WriteLine(comandoSQL);
        int regAfetados = 0;
        cDados.execSQL(comandoSQL, ref regAfetados);
    }

    protected void btnDesbloquearCrono_Click(object sender, EventArgs e)
    {
        // ACG: 04/10/2015 - O segundo parametro é para desbloquear cronogramas de replanejamento
        cDados.atualizaCronogramaCheckin(codigoProjeto, "");
        verificaPermissoesBotoes();
        verificaBloqueioCronograma();
        defineOpcoesEAP();
    }

    private void verificaPermissoesBotoes()
    {
        bool podeEditarEAP, podeVisualizarEAP, podeEditarCronograma, podeDesbloquearCronograma;

        podeVisualizarEAP = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CnsEAP");
        podeEditarEAP = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CadEAP");
        podeEditarCronograma = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CadCrn");
        podeDesbloquearCronograma = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_DesBlq");        
        
        cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeEditarEAP, ref podeEditarCronograma, ref podeDesbloquearCronograma);
        getPodeEditarEAP();
       
        btnEditarEAP.ClientVisible = podeEditarEAP && edicaoEAP && (codigoCronogramaReplanejamento != "" || utilizaNovaEAP == "N");
        btnVisualizarEAP.ClientVisible = podeVisualizarEAP && !edicaoEAP && (codigoCronogramaOficial != "" || utilizaNovaEAP == "N");

        btnEditarCronograma.ClientEnabled = podeEditarCronograma;
        btnEditarCronograma2.ClientVisible = podeEditarCronograma;

        btnDesbloquear.ClientEnabled = podeDesbloquearCronograma;
    }

    public void getIconeAlertaCronograma()
    {
        string corIcone = "";
        string mensagem = "";

        bool mostrarIcone = cDados.possuiAlertaCronograma(codigoProjeto, ref corIcone, ref mensagem);
        
        lblAlerta.Text = mensagem;

        imgAlerta.ClientVisible = mostrarIcone;
        imgAlerta.ImageUrl = "~/imagens/Alert" + corIcone.Trim() + ".png";
    }

    protected void cbImprimeCronograma_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        /*
        //[0]   var url = "MA=" + ((ckMarcos.GetChecked() == true) ? "S" : "N");	
        //[1]   url += "&NC=" + ((ckNaoConcluidas.GetChecked() == true) ? "S" : "N");	
        //[2]   url += "&TA=" + ((ckTarefasAtrasadas.GetChecked() == true) ? "S" : "N");	
        //[3]   url += "&REC=" + ddlRecurso.GetValue();	
        //[4]   url += "&CP=" + hfCodigoProjeto.Get('CodigoProjeto');	
        //[5]   url += "&NP=" + hfCodigoProjeto.Get('NomeProjeto');	
        //[6]   url += "&NR=" + ddlRecurso.GetText();
        //[7]   url += "&LB=" + (ddlLinhaBase.GetValue() == null ? '-1' : ddlLinhaBase.GetValue());
        strVetor[0]	""	string
		strVetor[1]	"N"	string
		strVetor[2]	"N"	string
		strVetor[3]	"N"	string
		strVetor[4]	"-1"	string
		strVetor[5]	"2106"	string
		strVetor[6]	"PRG - SUB2 - TESTE - DESENV - Versão 2014-05-16 - Doc.sql"	string
		strVetor[7]	"Todos"	string
		strVetor[8]	"-1"	string
         */

        string mensagemErro = "";
        try
        {
            string strParametro = e.Parameter;
            string[] strVetor = strParametro.Split('#');

            string marcos = "";//OK
            string naoConcluidas = "";//OK
            string tarefasAtrasadas = "";//OK
            int codigoRecurso = -1;//OK
            int codigoProjeto = -1;//OK
            int versaoLB = -1;//OK
            string nomeProjeto = "";//OK
            string montaNomeImagemParametro = "";
            string dataImpressao = "";
            string nomeRecurso = "";

            marcos = strVetor[1];
            naoConcluidas = strVetor[2];
            tarefasAtrasadas = strVetor[3];
            codigoRecurso = int.Parse(strVetor[4]);
            codigoProjeto = int.Parse(strVetor[5]);
            versaoLB = int.Parse(strVetor[8]);

            montaNomeImagemParametro = "";
            dataImpressao = "";
            nomeRecurso = strVetor[7];


            DataSet dsAux = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "imprimeDadosLinhaBaseCronograma");
            if (cDados.DataSetOk(dsAux) && cDados.DataTableOk(dsAux.Tables[0]) && dsAux.Tables[0].Rows[0]["imprimeDadosLinhaBaseCronograma"].ToString() == "S")
            {
                imprimeDadosLinhaBaseCronograma = "S";
            }
            if (Request.QueryString["NP"] != null && Request.QueryString["NP"].ToString() != "")
            {
                nomeProjeto = Request.QueryString["NP"].ToString();
            }
            if (Request.QueryString["DI"] != null && Request.QueryString["DI"].ToString() != "")
            {
                dataImpressao = Request.QueryString["DI"].ToString();
            }
            if (Request.QueryString["NR"] != null && Request.QueryString["NR"].ToString() != "")
            {
                nomeRecurso = Request.QueryString["NR"].ToString();
            }
            DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");
            ASPxBinaryImage image1 = new ASPxBinaryImage();
            if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
            {
                try
                {
                    image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
                    string montaNomeArquivo = "";
                    if (image1.ContentBytes != null)
                    {
                        string pathArquivo = "logoRelAnalisePerform_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                        montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + pathArquivo;
                        montaNomeImagemParametro = @"~\ArquivosTemporarios\" + pathArquivo;
                        FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                        fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                        fs.Close();
                        fs.Dispose();
                        //rel.Parameters["pathArquivo"].Value = montaNomeImagemParametro;
                        pathArquivo = montaNomeImagemParametro;
                    }
                }
                catch (Exception ex)
                {
                    string mensage = ex.Message;
                }
            }

            int indicaMarco = 0, indicaTarefaResumo = 0, indicaTarefaNaoConcluida = 0, indicaRecursoSelecionado = 0;

            if (marcos == "S")
            {
                indicaMarco = 1;
            }

            if (tarefasAtrasadas == "S")
            {
                indicaTarefaResumo = 1;
            }

            if (naoConcluidas == "S")
            {
                indicaTarefaNaoConcluida = 1;
            }

            if (codigoRecurso.ToString() != "-1")
            {
                indicaRecursoSelecionado = 1;
            }
            var percentualConcluido = txtConcluido.Value == null ? new int?() : Convert.ToInt32(txtConcluido.Value);
            var data = ddlData.Value == null ? new DateTime?() : ddlData.Date;


            string cmdGetNomeProjeto = string.Format("SELECT NomeProjeto FROM Projeto WHERE CodigoProjeto = {0}", codigoProjeto);
            DataSet dsNomeProjeto = cDados.getDataSet(cmdGetNomeProjeto);
            if(cDados.DataSetOk(dsNomeProjeto) && cDados.DataTableOk(dsNomeProjeto.Tables[0]))
            {
                nomeProjeto = dsNomeProjeto.Tables[0].Rows[0]["NomeProjeto"].ToString();
            }
            relImprimeCronograma relatorio1 = new relImprimeCronograma(codigoProjeto, versaoLB, codigoRecurso, indicaMarco, indicaTarefaResumo, indicaTarefaNaoConcluida, indicaRecursoSelecionado, nomeProjeto, montaNomeImagemParametro, dataImpressao, nomeRecurso, imprimeDadosLinhaBaseCronograma, percentualConcluido, data);

            MemoryStream stream = new MemoryStream();

            relatorio1.ExportToPdf(stream);
            Session["exportStream"] = stream;
            
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }

        if (mensagemErro == "")
        {
            cbImprimeCronograma.JSProperties["cp_MensagemErro"] = "OK";
        }
        else
        {
            cbImprimeCronograma.JSProperties["cp_MensagemErro"] = mensagemErro;
        }
    }
}
