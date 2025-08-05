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

public partial class frame_Cronograma_gantt : System.Web.UI.Page
{
    dados cDados;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    public int codigoProjeto = 0;
    
    public string grafico_xml = "";
    public string alturaGrafico = "", larguraGrafico = "", nenhumGrafico = "";
    public string nomeProjeto = "";
    public int codigoEntidadeUsuarioResponsavel = -1;
    private string utilizaCronoInstalado = "N";
    int codigoUsuario = -1;

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
        
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
            if (codigoProjeto == -1)
                return;
        }
        
        if (Request.QueryString["NP"] != null && Request.QueryString["NP"].ToString() != "")
        {
            nomeProjeto = Request.QueryString["NP"].ToString();
        }

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CnsCrn");
        }

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "utilizaCronoInstalado");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["utilizaCronoInstalado"].ToString() != "")
        {
            utilizaCronoInstalado = dsParam.Tables[0].Rows[0]["utilizaCronoInstalado"].ToString();
        }

        hfCodigoProjeto.Set("CodigoProjeto", codigoProjeto);
        hfCodigoProjeto.Set("NomeProjeto", nomeProjeto);

        defineLarguraTela();

        carregaComboLinhaBase();

        //Função que gera o gráfico
        geraGrafico();

        verificaBloqueioCronograma();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        alturaGrafico = (altura - 230).ToString();
        larguraGrafico = (largura - 500).ToString();
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

        string where = "";

        int versaoLinhaBase = ddlLinhaBase.SelectedIndex == -1 ? -1 : int.Parse(ddlLinhaBase.Value.ToString());

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(codigoProjeto, "-1", 1, true, false, false, percentualConcluido, data);

        bool indicaCronogramaTasques = cDados.indicaCronogramaVersaoTasques(codigoProjeto);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            xml = cDados.getGraficoGanttTarefasProjeto(ds.Tables[0], removerIdentacao, indicaCronogramaTasques);
            xmlZoom = cDados.getGraficoZoomGanttTarefasProjeto(ds.Tables[0], removerIdentacao, indicaCronogramaTasques);
        }
        else
        {
            nenhumGrafico = cDados.getGanttVazio((int.Parse(alturaGrafico) - 20).ToString());
            
            alturaGrafico = "0";
        }

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);
        cDados.escreveXML(xmlZoom, nomeGraficoZoom);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = "../.." + nomeGrafico;
        int tamanhoZoom = ds.Tables[0].Rows.Count * 25 + 120;
    }

    private void carregaComboLinhaBase()
    {
        DataSet dsLinhaBase = cDados.getVersoesLinhaBase(codigoProjeto, "");

        if (cDados.DataSetOk(dsLinhaBase))
        {
            ddlLinhaBase.DataSource = dsLinhaBase;

            ddlLinhaBase.TextField = "Descricao";

            ddlLinhaBase.ValueField = "Versao";

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
        
        string eventoBotaoEditarCrono = "", processOnServer = "";

        if (utilizaCronoInstalado == "S")
        {
            eventoBotaoEditarCrono = "pcDownload.Show();";
        }
        else
        {
            string linkOpcao = cDados.getLinkPortalDesktop(Request.Url, codigoEntidadeUsuarioResponsavel, codigoUsuario, codigoProjeto, "./../../");
            eventoBotaoEditarCrono = "window.open('" + linkOpcao + "', 'framePrincipal');";
            processOnServer = "e.processOnServer = false;";
        }

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string mensagem = "O cronograma está bloqueado com o usuário " + ds.Tables[0].Rows[0]["NomeUsuarioCheckoutCronograma"] + "."
                + Environment.NewLine + "Deseja abrir o cronograma somente para leitura?";

            lblInformacao.Text = mensagem;

            btnAbrirCronoBloqueado.ClientSideEvents.Click = "function(s, e) {pcInformacao.Hide();" + eventoBotaoEditarCrono + processOnServer + "}";

            string mensagemDesbloqueio = "O cronograma está bloqueado com o usuário " + ds.Tables[0].Rows[0]["NomeUsuarioCheckoutCronograma"] + "."
                + Environment.NewLine + "Ao fazer o desbloqueio as atualizações pendentes serão perdidas. Deseja realmente desbloquear o projeto?";
            
            lblDesbloqueio.Text = mensagemDesbloqueio;
        }
    }

    private bool getPodeEditarEAP()
    {
        bool retorno = true;
        string comandoSQL = string.Format(@"
                SELECT  DataUltimaGravacaoDesktop 
                FROM    {0}.{1}.CronogramaProjeto 
                WHERE   CodigoProjeto = {2}
                  AND   DataUltimaGravacaoDesktop IS NOT NULL
                ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            retorno = false;

        return retorno;
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
        verificaBloqueioCronograma();
    }
}
