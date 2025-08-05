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
using DevExpress.XtraReports.Web;

public partial class _Projetos_DadosProjeto_popupRelStatusReport : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    public string larguraTela = "";
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        defineAlturaTela();
        this.Title = cDados.getNomeSistema();

        mostraStatusReport();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        // Calcula a largura da tela
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTela = (alturaPrincipal - 40).ToString() + "px";
        larguraTela = (larguraPrincipal - 90).ToString() + "px";

        divRelatorio.Style.Add("overflow", "auto");
    }

    private ReportToolbarItem GetBtnByName(string name)
    {
        foreach (ReportToolbarItem item in ReportToolbar1.Items)
        {
            if (item.Name == name)
                return item;
        }
        return null;
    }

    private string mostraStatusReport()
    {
        int codigoStatusReport = Convert.ToInt32(Request.QueryString["codigoStatusReport"] ?? "0");
        bool podeEditar = Request.QueryString["podeEditar"].Equals("S");
        if (!podeEditar)
        {
            ReportToolbarItem btnCancelar = GetBtnByName("btnPublicar");
            ReportToolbarItem btnPublicar = GetBtnByName("btnCancelar");
            //foreach (ReportToolbarItem item in ReportToolbar1.Items)
            //{
            //    if (item.Name == "btnPublicar")
            //        btnPublicar = item;
            //    else if (item.Name.Equals("btnCancelar"))
            //        btnCancelar = item;
            //}
            ReportToolbar1.Items.Remove(btnPublicar);
            ReportToolbar1.Items.Remove(btnCancelar);
        }
        string msgErro = string.Empty;

        rel_StatusReport rel = new rel_StatusReport();
        #region Comando SQL
        string comandoSql = string.Format(@" exec [dbo].[p_getDadosStatusReportPadrao] {0} ", codigoStatusReport);
        #endregion
        DataSet ds = cDados.getDataSet(comandoSql);
        DataSet dsRel = (DataSet)rel.DataSource;
        string[] tableNames = new string[] { "StatusReport", "TarefasConcluidas", "TarefasAtrasadas", "TarefasProximoPeriodo", "ItensCusto", "ItensReceita", "Marcos", "Riscos", "Questoes", "TarefasToDoList", "Metas", "Contratos" };
        try
        {
            dsRel.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);

            rel.DataSource = dsRel;
            rptViewer.Report = rel;
        }
        catch (Exception ex)
        {
            msgErro = ex.Message;
        }
        return msgErro;
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 150).ToString();
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        return largura - 140;
    }
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string key = "cp_msg";
        string mensagem = persistePublicacaoRelatorio();
        pnCallback.JSProperties["cp_status"] = "erro";
        if (string.IsNullOrEmpty(mensagem))
        {
            mensagem = "Publicação realizada com sucesso!";
            pnCallback.JSProperties["cp_status"] = "ok";
        }

        if (pnCallback.JSProperties.ContainsKey(key))
            pnCallback.JSProperties[key] = mensagem;
        else
            pnCallback.JSProperties.Add(key, mensagem);
    }

    private string persistePublicacaoRelatorio()
    {
        string msg = "";
        int codigoStatusReport;
        try
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                //DevExpress.XtraPrinting.PdfExportOptions op =
                //    new DevExpress.XtraPrinting.PdfExportOptions()
                //    {
                //        Compressed = true,
                //        ConvertImagesToJpeg = true,
                //        ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.Lowest
                //    };

                rel_StatusReport rel = (rel_StatusReport)rptViewer.Report;
                rel.CreateDocument();
                rel.PrintingSystem.ExportToPdf(stream);
                byte[] arquivo = stream.GetBuffer();

                codigoStatusReport = int.Parse(Request.QueryString["codigoStatusReport"]);
                cDados.publicaStatusReport(codigoStatusReport, arquivo);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
}
