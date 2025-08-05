using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.Web;
using System.Data;
using System.Linq;
using DevExpress.XtraReports.UI;

public partial class _Projetos_DadosProjeto_popupRelProgramas : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    public string larguraTela = "";
    private int codigoStatusReport;
    private string iniciais;
    private int codigoUsuarioLogado;


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
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        if (!int.TryParse(Request.QueryString["codStatusReport"], out codigoStatusReport))
            codigoStatusReport = -1;
        iniciais = Request.QueryString["iniciais"];
        if (string.IsNullOrWhiteSpace(iniciais))
            iniciais = "BLTQ";

        defineAlturaTela();
        this.Title = cDados.getNomeSistema();

        bool podeEditar = Request.QueryString["podeEditar"].Equals("S");
        if (!podeEditar)
        {
            ReportToolbarItem btnCancelar = GetBtnByName("btnPublicar");
            ReportToolbarItem btnPublicar = GetBtnByName("btnCancelar");
            ReportToolbar1.Items.Remove(btnPublicar);
            ReportToolbar1.Items.Remove(btnCancelar);
        }

        DataTable dtParam = cDados.getParametrosSistema("modeloBoletimStatus", "urlCapaBAE").Tables[0];
        object parametro = null;
        string urlCapaBae = string.Empty;
        if (dtParam.Rows.Count > 0)
        {
            DataRow dr = dtParam.AsEnumerable().First();
            parametro = dr["modeloBoletimStatus"];
            urlCapaBae = dr["urlCapaBAE"] as string;
        }

        if (parametro == null || Convert.IsDBNull(parametro))
            parametro = 1;

        DevExpress.XtraReports.UI.XtraReport rel = ObtemInstanciaBoletim(Convert.ToInt32(parametro));
        rel.CreateDocument();
        if (rel is rel_BoletimAcoesEstrategicasVisao && !string.IsNullOrWhiteSpace(urlCapaBae))
            InserCapaRelatorio((rel_BoletimAcoesEstrategicasVisao)rel, Server.MapPath(urlCapaBae));
        if(rel is rel_AcompanhamentoProjetosUnidade)
        {
            InserCapaRelatorio_RAPU(rel);
        }
        ReportViewer1.Report = rel;
        cDados.aplicaEstiloVisual(this);
        this.TH(this.TS("popupRelProgramas"));
    }

    private void InserCapaRelatorio_RAPU(XtraReport rel)
    {
        rel_CAPA_RAPU capa = new rel_CAPA_RAPU(codigoUsuarioLogado);
        capa.Parameters["pCodigoStatusReport"].Value = codigoStatusReport;
        capa.CreateDocument();
        rel.Pages.Insert(0, capa.Pages[0]);
    }

    private DevExpress.XtraReports.UI.XtraReport ObtemInstanciaBoletim(int parametroModeloBoletimStatus)
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
            rel_AcompanhamentoProjetosUnidade rel_RAPU = new rel_AcompanhamentoProjetosUnidade(codigoUsuarioLogado);
            rel_RAPU.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_RAPU;

        }
        else if (iniciais == "SR_MDL0007")
        {
            rel_StatusReport0007 rel0007 = new rel_StatusReport0007();
            rel0007.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel0007;
        }
        else if (iniciais == "SR_PPJ01")
        {
            RelPlanoProjeto_001 rel_ppj = new RelPlanoProjeto_001();
            rel_ppj.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_ppj;
        }
        else if (iniciais == "PADRAONOVO")
        {
            rel_StatusReportNovoPadrao rel_novo = new rel_StatusReportNovoPadrao();
            rel_novo.pCodigoStatusReport.Value = codigoStatusReport;
            report = rel_novo;
        }
        return report;
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

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        // Calcula a largura da tela
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTela = (alturaPrincipal - 40).ToString() + "px";
        larguraTela = (larguraPrincipal - 90).ToString() + "px";

        divRelatorio.Style.Add("height", (alturaPrincipal - 170) + "px");
        divRelatorio.Style.Add("overflow", "auto");

        divToolBox.Style.Add("overflow", "auto");
        divToolBox.Style.Add("height", "40px");
    }
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_msg"] = "";
        pnCallback.JSProperties["cp_erro"] = "";

        string mensagem = persistePublicacaoRelatorio();
        pnCallback.JSProperties["cp_status"] = "erro";
        if (string.IsNullOrEmpty(mensagem))
        {
            mensagem = Resources.traducao.popupRelProgramas_publica__o_realizada_com_sucesso_;
            pnCallback.JSProperties["cp_msg"] = mensagem;
        }
        else
        {
            pnCallback.JSProperties["cp_erro"] = mensagem;
        }
    }

    private string persistePublicacaoRelatorio()
    {
        string msg = "";
        try
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                DevExpress.XtraReports.UI.XtraReport rel;
                DataTable dtParam = cDados.getParametrosSistema("modeloBoletimStatus", "urlCapaBAE").Tables[0];
                object parametro = null;
                string urlCapaBae = string.Empty;
                if (dtParam.Rows.Count > 0)
                {
                    DataRow dr = dtParam.AsEnumerable().First();
                    parametro = dr["modeloBoletimStatus"];
                    urlCapaBae = Server.MapPath(dr["urlCapaBAE"] as string);
                }

                if (parametro == null || Convert.IsDBNull(parametro))
                    parametro = 1;

                rel = ObtemInstanciaBoletim(Convert.ToInt32(parametro));
                rel.CreateDocument();
                if (rel is rel_BoletimAcoesEstrategicasVisao && !string.IsNullOrWhiteSpace(urlCapaBae))
                    InserCapaRelatorio((rel_BoletimAcoesEstrategicasVisao)rel, urlCapaBae);

                if (rel is rel_AcompanhamentoProjetosUnidade)
                {
                    InserCapaRelatorio_RAPU(rel);
                }
                
                DevExpress.XtraPrinting.PdfExportOptions op = new DevExpress.XtraPrinting.PdfExportOptions();
                rel.PrintingSystem.ExportToPdf(stream, op);
                byte[] arquivo = stream.GetBuffer();

                cDados.publicaStatusReport(codigoStatusReport, arquivo);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private void InserCapaRelatorio(rel_BoletimAcoesEstrategicasVisao rel, string urlCapaBae)
    {
        rel_CapaBAE capa = new rel_CapaBAE();
        capa.ParamDataInicioPeriodoRelatorio.Value = rel.ParamDataInicioPeriodoRelatorio.Value;
        capa.ParamUrlImagemCapaBAE.Value = urlCapaBae;
        capa.CreateDocument();
        rel.Pages.Insert(0, capa.Pages[0]);
    }

}
