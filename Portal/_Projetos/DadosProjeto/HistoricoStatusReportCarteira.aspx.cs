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
using System.Text;
using System.IO;

public partial class _Projetos_DadosProjeto_HistoricoStatusReportCarteira : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;

    private int codigoUsuarioResponsavel = 0;
    private int codigoEntidadeUsuarioResponsavel = 0;
    private int idObjeto;
    private string iniciaisTipoObjeto;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public string estiloDiv = "style='OVERFLOW: auto; WIDTH: 730px; max-height: 320px'";

    public bool podeIncluir = true;
    public bool podeEditar = false;
    public bool podeExcluir = false;

    public bool podeEditarComentarios = false;
    public bool podeEnviarDestinatarios = false;
    public bool podePublicar = false;
    public bool podeExcluirRelatorio = false;

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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        idObjeto = int.Parse(Request.QueryString["idObjeto"]);
        iniciaisTipoObjeto = Request.QueryString["tp"];

        string iniciaisPermissaoEditarComentario = string.Format("{0}_CmtSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoEnviarRelatorio = string.Format("{0}_EnvSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoPublicar = string.Format("{0}_PubSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoExcluir = "EN_ExcSttRpt";

        podeEditarComentarios = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            idObjeto, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoEditarComentario);
        podeEnviarDestinatarios = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            idObjeto, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoEnviarRelatorio);
        podePublicar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            idObjeto, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoPublicar);
        podeExcluirRelatorio = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", iniciaisPermissaoExcluir);//Ao contrário das demais a permissão de exclusão está a nível de entidade
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        //dsDado.SelectParameters[0].DefaultValue = codigoEntidadeUsuarioResponsavel.ToString();
        cDados.aplicaEstiloVisual(Page);//Ok

        carregaGvDados();               //Ok

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
        }

        populaCombos();
    }

    #region GRID's

    #region GRID gvDADOS

    private void carregaGvDados()
    {
        DataSet ds = cDados.getHistoricoStatusReport(codigoEntidadeUsuarioResponsavel, idObjeto, iniciaisTipoObjeto);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }

    #endregion

    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 300);
        if (altura > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 75;
        }
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/HistoricoStatusReportCarteira.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "HistoricoStatusReportCarteira", "ASPxListbox"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoStatusReport").ToString();
        return codigoDado;
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        switch (e.Parameter)
        {
            case "GerarRelatorio":
                mensagemErro_Persistencia = persisteGeracaoRelatorio();
                break;
            case "Excluir":
                mensagemErro_Persistencia = persisteExclusaoRelatorio();
                break;
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

    }

    private string persisteExclusaoRelatorio()
    {
        string msg = string.Empty;
        try
        {
            int codigoStatusReport = int.Parse(getChavePrimaria());
            cDados.ExcluiRelatorioStatusReport(codigoStatusReport, codigoUsuarioResponsavel);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private string persisteGeracaoRelatorio()
    {
        string msg = string.Empty;
        try
        {
            int codigoModeloStatusReport = (int)ddlModeloStatusReport.Value;
            cDados.GeraRelatoriosStatusReport(codigoModeloStatusReport, iniciaisTipoObjeto, idObjeto, codigoUsuarioResponsavel, out msg);

            #region Publica Status Report gerado

            string comandoSql = string.Format(@"
 SELECT TOP 1 sr.CodigoStatusReport 
   FROM {0}.{1}.StatusReport sr
  WHERE sr.CodigoObjeto = {2}
	AND sr.CodigoTipoAssociacaoObjeto = dbo.f_GetCodigoTipoAssociacao('CP')
  ORDER BY
		sr.CodigoStatusReport DESC"
                , cDados.getDbName(), cDados.getDbOwner(), idObjeto);
            DataSet ds = cDados.getDataSet(comandoSql);

            int codigoStatusReport = Convert.ToInt32(ds.Tables[0].Rows[0]["CodigoStatusReport"]);

            MemoryStream stream = new MemoryStream();

            DevExpress.XtraReports.UI.XtraReport rel;
            DataTable dtParam = cDados.getParametrosSistema("modeloBoletimStatus").Tables[0];
            object parametro = null;
            if (dtParam.Rows.Count > 0)
                parametro = dtParam.Rows[0]["modeloBoletimStatus"];

            if (parametro == null || Convert.IsDBNull(parametro))
                parametro = 1;

            rel = ObtemInstanciaBoletim(codigoStatusReport, Convert.ToInt32(parametro), "BLTQ");
            rel.CreateDocument();
            if (rel is rel_BoletimAcoesEstrategicasVisao)
                InserCapaRelatorio((rel_BoletimAcoesEstrategicasVisao)rel);

            rel.CreateDocument();
            rel.PrintingSystem.ExportToPdf(stream);
            byte[] arquivo = stream.GetBuffer();

            cDados.publicaStatusReport(codigoStatusReport, arquivo);

            #endregion

            carregaGvDados();
            //gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoStatusReport);
            //gvDados.ClientVisible = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private void InserCapaRelatorio(rel_BoletimAcoesEstrategicasVisao rel)
    {
        rel_CapaBAE capa = new rel_CapaBAE();
        capa.ParamDataInicioPeriodoRelatorio.Value = rel.ParamDataInicioPeriodoRelatorio.Value;
        capa.CreateDocument();
        rel.Pages.Insert(0, capa.Pages[0]);
    }

    private DevExpress.XtraReports.UI.XtraReport ObtemInstanciaBoletim(int codigoStatusReport, int parametroModeloBoletimStatus, string iniciais)
    {
        DevExpress.XtraReports.UI.XtraReport report = null;
        if (iniciais == "BLTQ")
        {
            if (parametroModeloBoletimStatus == 1)
                report = new rel_BoletimStatusNacional(codigoStatusReport);
            else if (parametroModeloBoletimStatus == 2)
                report = new rel_BoletimStatusBahia(codigoStatusReport);
            else
                report = new rel_BoletimStatus(codigoStatusReport);
        }
        else if (iniciais == "BLT_AE_UN")
            report = new rel_BoletimAcoesEstrategicasUnidade(codigoStatusReport);
        else if (iniciais == "BLT_AE_VI")
            report = new rel_BoletimAcoesEstrategicasVisao(codigoStatusReport);
        else if (iniciais == "GRF_BOLHAS")
        {
            RelGraficoBolha relGRF_BOLHAS = new RelGraficoBolha();
            relGRF_BOLHAS.ParamCodigoStatusReport.Value = codigoStatusReport;
            report = relGRF_BOLHAS;
        }
        else if (iniciais == "BLT_RAPU")
        {
            rel_AcompanhamentoProjetosUnidade rel_RAPU = new rel_AcompanhamentoProjetosUnidade(codigoUsuarioResponsavel);
            rel_RAPU.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_RAPU;
        }
        else if (iniciais == "PADRAONOVO")
        {
            rel_StatusReportNovoPadrao rel_novo = new rel_StatusReportNovoPadrao();
            rel_novo.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_novo;
        }
        return report;
    }

    private void Download()
    {
        string nomeArquivo = "StatusReport.pdf";
        int codigoStatusReport = int.Parse(getChavePrimaria());
        byte[] imagem = cDados.getConteudoStatusReport(codigoStatusReport);
        string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + nomeArquivo;
        string arquivo2 = "~/ArquivosTemporarios/" + nomeArquivo;
        System.IO.FileStream fs = new System.IO.FileStream(arquivo, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        fs.Write(imagem, 0, imagem.Length);
        fs.Close();

        ForceDownloadFile(arquivo2, true);
    }

    private void ForceDownloadFile(string fname, bool forceDownload)
    {
        //ASPxWebControl.RedirectOnCallback(fname);
        string path = MapPath(fname);
        string name = System.IO.Path.GetFileName(path);
        string ext = System.IO.Path.GetExtension(path);
        string type = "application/octet-stream";
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }
        Response.ContentType = type;
        Response.WriteFile(path);
        Response.Flush();
        Response.End();
    }

    private string getNomeCompletoArquivoTemporario(byte[] imagem)
    {
        string nomeArquivo = "StatusReport.pdf";
        string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + nomeArquivo;
        string arquivo2 = "~/ArquivosTemporarios/" + nomeArquivo;
        System.IO.FileStream fs = new System.IO.FileStream(arquivo, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        fs.Write(imagem, 0, imagem.Length);
        fs.Close();

        return arquivo;
    }

    protected void pnCallbackMensagem_Callback(object sender, CallbackEventArgsBase e)
    {
        pnCallbackMensagem.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallbackMensagem.JSProperties["cp_OperacaoOk"] = "";
        string mensagemErro_Persistencia = "";

        switch (e.Parameter)
        {
            case "GerarRelatorio":
                //mensagemErro_Persistencia = persisteEdicaoComentarios();
                break;
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallbackMensagem.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
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

    #endregion

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Download();
    }

    protected string ObtemHtmlBtnAdicionarStatusReport()
    {
        string botao = (podeIncluir) ?
@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""mostraPopupSelecaoModeloStatusReport()"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/>" :
@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>";
        string html = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", botao);

        return html;
    }

    private void populaCombos()
    {
        DataSet ds = cDados.ObtemModelosStatusReport(codigoEntidadeUsuarioResponsavel, idObjeto, iniciaisTipoObjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            cDados.PopulaDropDownASPx(this, ds.Tables[0], "CodigoModeloStatusReport", "DescricaoModeloStatusReport", "", ref ddlModeloStatusReport);
        }
    }

    protected void Button_Load(object sender, EventArgs e)
    {
        ASPxButton btn = (ASPxButton)sender;
        GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;
        int indexLinha = container.VisibleIndex;

        switch (btn.ID)
        {
            case "btnExcluir":
                btn.Visible = podeExcluirRelatorio;
                if (btn.Visible && btn.Enabled)
                    btn.ClientSideEvents.Click = @"function(s, e) {e.processOnServer = false;btnExcluir_Click(" + indexLinha + ");}";
                break;
        }
    }
}
