using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;

/// <summary>
/// Summary description for rel_AcompanhamentoProjetosUnidade
/// </summary>
public class rel_AcompanhamentoProjetosUnidade : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private int codigoUsuarioLogado;
    private dados cDados;
    private DsBoletimStatusRAPU dsBoletimStatusRAPU;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private CalculatedField cfNomeMes;
    private XRLabel xrLabelNomeProjeto;
    private XRLine xrLine2;
    private int contadorProjetos;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRLabel lblTituloPaginasWeb;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private DetailReportBand DetailReport5;
    private DetailBand Detail6;
    private DetailReportBand DetailReport6;
    private DetailBand Detail7;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell celPercentualPrevistoFisico;
    private XRTableCell celPercentualRealizadoFisico;
    private XRTableCell celPercentualPrevistoFinanceiro;
    private XRTableCell celPercentualRealizadoFinanceiro;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel12;
    private XRLabel xrLabel9;
    private XRLabel xrLabel7;
    private XRLabel xrLabel2;
    private XRLabel xrLabel21;
    private XRLabel xrLabel22;
    private XRChart xrChartValores;
    private XRLabel xrLabel3;
    private XRLine xrLine1;
    private XRChart xrChart1;
    private CalculatedField ValorCustoPrevistoPorExtenso;
    private CalculatedField ValorCustoRealizadoPorExtenso;
    private GroupHeaderBand GroupHeader5;
    private CalculatedField nomeGerenteProjetoPorExtenso;
    private CalculatedField NomeDoCRPorExtenso;
    private XRPanel xrPanel1;
    private XRPanel panelInfoLegenda;
    private XRLabel xrLabel5;
    private XRLabel xrLabel11;
    private XRLabel xrLabel14;
    private XRSubreport xrSubreportLegenda;
    private XRPanel xrPanel2;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRChart xrChart4;
    private XRLabel xrLabel13;
    private XRChart xrChart5;
    private XRLabel xrLabel15;
    private CalculatedField cfNomeMes1;
    private XRLabel xrLabel10;
    private DetailReportBand DetailReport3;
    private DetailBand Detail4;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel xrLabel24;
    private XRLabel xrLabel25;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRLabel xrLabel28;
    private XRLabel xrLabel31;
    private XRLabel xrLabel33;
    private XRLabel xrLabel32;
    private XRLabel xrLabel34;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel37;
    private XRLabel xrLabel40;
    private DetailBand Detail5;
    private DetailReportBand DetailReport7;
    private DetailBand Detail8;
    private DetailReportBand DetailReport9;
    private DetailBand Detail10;
    private XRLabel xrLabel23;
    private XRLabel xrLabel38;
    private DetailReportBand DetailReport11;
    private DetailBand Detail12;
    private XRSubreport xrSubreport1;
    private DetailReportBand DetailReport12;
    private DetailBand Detail13;
    private XRSubreport xrSubreport2;
    private XRTable xrTable4;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell7;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell10;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow4;
    private XRTableCell cellValorPrevisto;
    private XRTableRow xrTableRow6;
    private XRTableCell cellValorRealizado;
    private XRTableRow xrTableRow10;
    private XRTableCell cellVariacao;
    private XRLabel xrLabel42;
    private CalculatedField cfDesvio;
    private XRLabel xrLabel43;
    private XRTableCell xrTableCell11;
    private XRLabel xrLabel1;
    private DevExpress.XtraReports.Parameters.Parameter pNomeUnidade;
    private XRLabel xrLabel41;
    private XRLabel xrLabel44;
    private XRLabel lblDataFimPeriodoRelatorio;
    private XRRichText xrRichText2;
    private XRRichText xrRichText1;
    private XRTable xrTable6;
    private XRTableRow xrTableRow12;
    private XRTableCell celItemEntregasDetail;
    private XRTableCell celTipoEntregasDetail;
    private XRTableCell celNomeEntregasDetail;
    private XRTableCell celDataEntregasDetail;
    private XRTableCell celStatusEntregasDetail;
    private CalculatedField cfIdentacaoNomeTarefa;
    private XRLabel xrLabel6;
    private CalculatedField cfLegendaEntregasProjeto;
    private CalculatedField cfTituloItem;
    private CalculatedField cfTituloTipoEvento;
    private CalculatedField cfTituloData;
    private CalculatedField cfTituloStatusTarefa;
    private CalculatedField cfTituloEntrega;
    private CalculatedField cfMensagemDeNenhumaEntrega;
    private CalculatedField cfEntregasDoProjeto;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell5;
    private XRLabel xrLabel4;
    private GroupHeaderBand GroupHeader1;
    private CalculatedField cfNomeObjeto;
    private XRLabel xrLabel39;
    private XRLabel xrLabel8;
    private CalculatedField cfTituloValorReceitaRealizado;
    private XRLabel xrLabel45;
    private XRLabel xrLabel46;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoStatusReport;

    public rel_AcompanhamentoProjetosUnidade(int codigoUsuarioLogado)
    {
        this.codigoUsuarioLogado = codigoUsuarioLogado;
        InitializeComponent();
        contadorProjetos = 0;
        xrSubreportLegenda.ReportSource.DataSource = dsBoletimStatusRAPU;
        xrSubreportLegenda.ReportSource.Parameters["pMostarReceita"].Value = false;

    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "rel_AcompanhamentoProjetosUnidade.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_AcompanhamentoProjetosUnidade.ResourceManager;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport4;
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel3 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView3 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
        DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.ScaleBreak scaleBreak1 = new DevExpress.XtraCharts.ScaleBreak();
        DevExpress.XtraCharts.ScaleBreak scaleBreak2 = new DevExpress.XtraCharts.ScaleBreak();
        DevExpress.XtraCharts.ScaleBreak scaleBreak3 = new DevExpress.XtraCharts.ScaleBreak();
        DevExpress.XtraCharts.ScaleBreak scaleBreak4 = new DevExpress.XtraCharts.ScaleBreak();
        DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView4 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel4 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView4 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel5 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView5 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel6 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView6 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
        this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.dsBoletimStatusRAPU = new DsBoletimStatusRAPU();
        this.DetailReport9 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail10 = new DevExpress.XtraReports.UI.DetailBand();
        this.DetailReport12 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail13 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.lblTituloPaginasWeb = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.pNomeUnidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.pCodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabelNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celItemEntregasDetail = new DevExpress.XtraReports.UI.XRTableCell();
        this.celTipoEntregasDetail = new DevExpress.XtraReports.UI.XRTableCell();
        this.celNomeEntregasDetail = new DevExpress.XtraReports.UI.XRTableCell();
        this.celDataEntregasDetail = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusEntregasDetail = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.panelInfoLegenda = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrSubreportLegenda = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart4 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart5 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.cfNomeMes = new DevExpress.XtraReports.UI.CalculatedField();
        this.DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.lblDataFimPeriodoRelatorio = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrChartValores = new DevExpress.XtraReports.UI.XRChart();
        this.DetailReport7 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail8 = new DevExpress.XtraReports.UI.DetailBand();
        this.DetailReport11 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail12 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellValorPrevisto = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellValorRealizado = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellVariacao = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
        this.DetailReport6 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celPercentualPrevistoFisico = new DevExpress.XtraReports.UI.XRTableCell();
        this.celPercentualRealizadoFisico = new DevExpress.XtraReports.UI.XRTableCell();
        this.celPercentualPrevistoFinanceiro = new DevExpress.XtraReports.UI.XRTableCell();
        this.celPercentualRealizadoFinanceiro = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.ValorCustoPrevistoPorExtenso = new DevExpress.XtraReports.UI.CalculatedField();
        this.ValorCustoRealizadoPorExtenso = new DevExpress.XtraReports.UI.CalculatedField();
        this.nomeGerenteProjetoPorExtenso = new DevExpress.XtraReports.UI.CalculatedField();
        this.NomeDoCRPorExtenso = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfNomeMes1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfDesvio = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfIdentacaoNomeTarefa = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfLegendaEntregasProjeto = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfTituloItem = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfTituloTipoEvento = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfTituloData = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfTituloStatusTarefa = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfTituloEntrega = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfMensagemDeNenhumaEntrega = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfEntregasDoProjeto = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfNomeObjeto = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfTituloValorReceitaRealizado = new DevExpress.XtraReports.UI.CalculatedField();
        DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatusRAPU)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChartValores)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // DetailReport4
        // 
        DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.DetailReport9});
        DetailReport4.Dpi = 254F;
        DetailReport4.Expanded = false;
        DetailReport4.Level = 2;
        DetailReport4.Name = "DetailReport4";
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart1});
        this.Detail5.Dpi = 254F;
        this.Detail5.HeightF = 843.9F;
        this.Detail5.Name = "Detail5";
        // 
        // xrChart1
        // 
        this.xrChart1.BorderColor = System.Drawing.Color.Black;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "ValoresAcumulados";
        this.xrChart1.DataSource = this.dsBoletimStatusRAPU;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.Diagram = xyDiagram1;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
        this.xrChart1.Legend.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrChart1.Legend.Name = "Default Legend";
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(1F, 0F);
        this.xrChart1.Name = "xrChart1";
        series1.ArgumentDataMember = "cfNomeMes1";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel1.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAroundPoint;
        pointSeriesLabel1.TextPattern = "{V:C2}";
        series1.Label = pointSeriesLabel1;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.LegendText = "Previsto";
        series1.Name = "Series 1a";
        series1.ValueDataMembersSerializable = "ValorPrevisto";
        series1.View = lineSeriesView1;
        series2.ArgumentDataMember = "cfNomeMes1";
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel2.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAroundPoint;
        pointSeriesLabel2.TextPattern = "{V:C2}";
        series2.Label = pointSeriesLabel2;
        series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.LegendText = "Realizado";
        series2.Name = "Series 2a";
        series2.ValueDataMembersSerializable = "ValorRealizado";
        series2.View = lineSeriesView2;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        pointSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = pointSeriesLabel3;
        this.xrChart1.SeriesTemplate.View = lineSeriesView3;
        this.xrChart1.SizeF = new System.Drawing.SizeF(1900F, 843.9F);
        chartTitle1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        chartTitle1.Text = "Gráfico com Visão Acumulada";
        this.xrChart1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
        // 
        // dsBoletimStatusRAPU
        // 
        this.dsBoletimStatusRAPU.DataSetName = "DsBoletimStatus";
        this.dsBoletimStatusRAPU.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // DetailReport9
        // 
        this.DetailReport9.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail10,
            this.DetailReport12});
        this.DetailReport9.Dpi = 254F;
        this.DetailReport9.Expanded = false;
        this.DetailReport9.Level = 0;
        this.DetailReport9.Name = "DetailReport9";
        // 
        // Detail10
        // 
        this.Detail10.Dpi = 254F;
        this.Detail10.HeightF = 0F;
        this.Detail10.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail10.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
        this.Detail10.Name = "Detail10";
        // 
        // DetailReport12
        // 
        this.DetailReport12.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail13});
        this.DetailReport12.Dpi = 254F;
        this.DetailReport12.Level = 0;
        this.DetailReport12.Name = "DetailReport12";
        this.DetailReport12.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.DetailReport12_BeforePrint);
        // 
        // Detail13
        // 
        this.Detail13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4,
            this.xrSubreport2});
        this.Detail13.Dpi = 254F;
        this.Detail13.HeightF = 256.9766F;
        this.Detail13.Name = "Detail13";
        // 
        // xrTable4
        // 
        this.xrTable4.Dpi = 254F;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(109.4792F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow11});
        this.xrTable4.SizeF = new System.Drawing.SizeF(176.4922F, 256.9766F);
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableRow7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.StylePriority.UseBackColor = false;
        this.xrTableRow7.StylePriority.UseBorders = false;
        this.xrTableRow7.StylePriority.UseTextAlignment = false;
        this.xrTableRow7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableRow7.Weight = 1D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.CanGrow = false;
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.Weight = 0.75254640369681369D;
        this.xrTableCell7.WordWrap = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.CanGrow = false;
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Font = new System.Drawing.Font("Calibri", 8F);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "Previsto";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell8.Weight = 0.75254640369681369D;
        this.xrTableCell8.WordWrap = false;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Calibri", 8F);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "Realizado";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell9.Weight = 0.75254640369681369D;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("Calibri", 8F);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Desvio";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell10.Weight = 0.75254640369681369D;
        // 
        // xrSubreport2
        // 
        this.xrSubreport2.Dpi = 254F;
        this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(285.9714F, 0F);
        this.xrSubreport2.Name = "xrSubreport2";
        this.xrSubreport2.ReportSource = new rel_sub_Valores();
        this.xrSubreport2.SizeF = new System.Drawing.SizeF(1612.529F, 254F);
        // 
        // Detail
        // 
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 0F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 99F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 99F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 49.72916F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.lblTituloPaginasWeb,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 100.0003F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrPageInfo2.ForeColor = System.Drawing.Color.DimGray;
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1369.532F, 50.0003F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.Number;
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(530.9675F, 50.00002F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseForeColor = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // lblTituloPaginasWeb
        // 
        this.lblTituloPaginasWeb.Dpi = 254F;
        this.lblTituloPaginasWeb.Font = new System.Drawing.Font("Calibri", 11F);
        this.lblTituloPaginasWeb.ForeColor = System.Drawing.Color.DimGray;
        this.lblTituloPaginasWeb.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.00013F);
        this.lblTituloPaginasWeb.Name = "lblTituloPaginasWeb";
        this.lblTituloPaginasWeb.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTituloPaginasWeb.SizeF = new System.Drawing.SizeF(1018.195F, 50.00001F);
        this.lblTituloPaginasWeb.StylePriority.UseFont = false;
        this.lblTituloPaginasWeb.StylePriority.UseForeColor = false;
        this.lblTituloPaginasWeb.StylePriority.UseTextAlignment = false;
        this.lblTituloPaginasWeb.Text = "lblTituloPaginasWeb";
        this.lblTituloPaginasWeb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DimGray;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1018.195F, 49.99974F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(336.428F, 50F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrPageInfo1.TextFormatString = "Data: {0:dd/MM/yyyy}";
        // 
        // pNomeUnidade
        // 
        this.pNomeUnidade.Name = "pNomeUnidade";
        // 
        // pCodigoStatusReport
        // 
        this.pCodigoStatusReport.Name = "pCodigoStatusReport";
        this.pCodigoStatusReport.Type = typeof(short);
        this.pCodigoStatusReport.ValueInfo = "0";
        this.pCodigoStatusReport.Visible = false;
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.DetailReport1,
            this.DetailReport2,
            this.DetailReport3});
        this.DetailReport.DataMember = "StatusReport.StatusReport_StatusReport";
        this.DetailReport.DataSource = this.dsBoletimStatusRAPU;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Font = new System.Drawing.Font("Calibri", 11F);
        this.DetailReport.Level = 3;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 436.0835F;
        this.Detail1.Name = "Detail1";
        this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel46,
            this.xrLabel45,
            this.xrLabel39,
            this.xrLabel8,
            this.xrLabel44,
            this.xrLabel41,
            this.xrLabel43,
            this.xrLabel42,
            this.xrLabel33,
            this.xrLabel32,
            this.xrLabel31,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLine2,
            this.xrLabelNomeProjeto,
            this.xrLabel24,
            this.xrLabel25,
            this.xrLabel26,
            this.xrLabel27,
            this.xrLabel28});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1898.5F, 434.8961F);
        // 
        // xrLabel46
        // 
        this.xrLabel46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.ValorForecast")});
        this.xrLabel46.Dpi = 254F;
        this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(251.4505F, 373.0883F);
        this.xrLabel46.Name = "xrLabel46";
        this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel46.SizeF = new System.Drawing.SizeF(1637.549F, 58.41995F);
        this.xrLabel46.TextFormatString = "{0:c2}";
        // 
        // xrLabel45
        // 
        this.xrLabel45.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel45.Dpi = 254F;
        this.xrLabel45.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(2.770826F, 373.0883F);
        this.xrLabel45.Name = "xrLabel45";
        this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel45.SizeF = new System.Drawing.SizeF(248.6797F, 63.49988F);
        this.xrLabel45.StylePriority.UseBorders = false;
        this.xrLabel45.StylePriority.UseFont = false;
        this.xrLabel45.StylePriority.UseTextAlignment = false;
        this.xrLabel45.Text = "Valor Forecast:";
        this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel39
        // 
        this.xrLabel39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.ValorReceitaRealizado", "{0:c2}")});
        this.xrLabel39.Dpi = 254F;
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(1528.282F, 309.5883F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(370.2181F, 58.41986F);
        this.xrLabel39.StylePriority.UseTextAlignment = false;
        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.cfTituloValorReceitaRealizado")});
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(1014.292F, 309.5883F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(513.9899F, 63.5F);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Receita Real:";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel44
        // 
        this.xrLabel44.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel44.Dpi = 254F;
        this.xrLabel44.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(2.770826F, 309.5883F);
        this.xrLabel44.Name = "xrLabel44";
        this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel44.SizeF = new System.Drawing.SizeF(111.1459F, 63.49997F);
        this.xrLabel44.StylePriority.UseBorders = false;
        this.xrLabel44.StylePriority.UseFont = false;
        this.xrLabel44.Text = "Saldo:";
        // 
        // xrLabel41
        // 
        this.xrLabel41.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.Saldo", "{0:c2}")});
        this.xrLabel41.Dpi = 254F;
        this.xrLabel41.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(113.9167F, 309.5883F);
        this.xrLabel41.Name = "xrLabel41";
        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel41.SizeF = new System.Drawing.SizeF(900.3754F, 63.49997F);
        this.xrLabel41.StylePriority.UseBorders = false;
        this.xrLabel41.StylePriority.UseFont = false;
        // 
        // xrLabel43
        // 
        this.xrLabel43.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel43.Dpi = 254F;
        this.xrLabel43.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(1018.195F, 246.0886F);
        this.xrLabel43.Name = "xrLabel43";
        this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel43.SizeF = new System.Drawing.SizeF(139.7707F, 63.5F);
        this.xrLabel43.StylePriority.UseBorders = false;
        this.xrLabel43.StylePriority.UseFont = false;
        this.xrLabel43.StylePriority.UseTextAlignment = false;
        this.xrLabel43.Text = "Desvio:";
        this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel42
        // 
        this.xrLabel42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.cfDesvio", "{0:c2}")});
        this.xrLabel42.Dpi = 254F;
        this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(1528.282F, 246.0886F);
        this.xrLabel42.Name = "xrLabel42";
        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel42.SizeF = new System.Drawing.SizeF(363.2174F, 58.41992F);
        this.xrLabel42.StylePriority.UseTextAlignment = false;
        this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrLabel42.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel42_BeforePrint);
        // 
        // xrLabel33
        // 
        this.xrLabel33.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.ValorCustoRealizado", "{0:c2}")});
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(1528.282F, 182.5883F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(363.2174F, 63.50003F);
        this.xrLabel33.StylePriority.UseBorders = false;
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel32
        // 
        this.xrLabel32.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.ValorCustoPrevisto", "{0:c2}")});
        this.xrLabel32.Dpi = 254F;
        this.xrLabel32.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(1528.282F, 118.3177F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(363.2174F, 63.5F);
        this.xrLabel32.StylePriority.UseBorders = false;
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel31
        // 
        this.xrLabel31.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(2.499969F, 246.0883F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(201.1042F, 63.49997F);
        this.xrLabel31.StylePriority.UseBorders = false;
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.Text = "Valor total:";
        // 
        // xrLabel30
        // 
        this.xrLabel30.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.CodigosCR")});
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(231.3448F, 182.5886F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(782.9474F, 63.50002F);
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel29
        // 
        this.xrLabel29.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 118.3177F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(338.888F, 63.50005F);
        this.xrLabel29.StylePriority.UseBorders = false;
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "Gerente do projeto:";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel29.WordWrap = false;
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 66F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1889F, 10F);
        // 
        // xrLabelNomeProjeto
        // 
        this.xrLabelNomeProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.NomeObjeto")});
        this.xrLabelNomeProjeto.Dpi = 254F;
        this.xrLabelNomeProjeto.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabelNomeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabelNomeProjeto.Name = "xrLabelNomeProjeto";
        this.xrLabelNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelNomeProjeto.SizeF = new System.Drawing.SizeF(1886.5F, 66.00001F);
        this.xrLabelNomeProjeto.StylePriority.UseFont = false;
        this.xrLabelNomeProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabelNomeProjeto_BeforePrint);
        // 
        // xrLabel24
        // 
        this.xrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.NomeGerenteProjeto")});
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(338.8882F, 118.3177F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(675.4041F, 63.50005F);
        this.xrLabel24.StylePriority.UseBorders = false;
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.ValorCustoPrevistoPorExtenso")});
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(1014.292F, 118.3177F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(513.9904F, 63.50003F);
        this.xrLabel25.StylePriority.UseBorders = false;
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel26
        // 
        this.xrLabel26.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 182.5883F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(231.3448F, 63.5F);
        this.xrLabel26.StylePriority.UseBorders = false;
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "Nome do CR:";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel27
        // 
        this.xrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.ValorCustoRealizadoPorExtenso")});
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(1014.292F, 182.5886F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(513.9904F, 63.50002F);
        this.xrLabel27.StylePriority.UseBorders = false;
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "xrLabel27";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel28
        // 
        this.xrLabel28.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.ValorOrcamento", "{0:c2}")});
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(203.875F, 246.0883F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(810.4171F, 63.49997F);
        this.xrLabel28.StylePriority.UseBorders = false;
        this.xrLabel28.StylePriority.UseFont = false;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupFooter1,
            this.GroupHeader1});
        this.DetailReport1.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.DetailReport1.DataSource = this.dsBoletimStatusRAPU;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 50F;
        this.Detail2.Name = "Detail2";
        this.Detail2.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("SequenciaTarefaCronograma", DevExpress.XtraReports.UI.XRColumnSortOrder.None)});
        // 
        // xrTable6
        // 
        this.xrTable6.BackColor = System.Drawing.Color.Transparent;
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 0F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
        this.xrTable6.SizeF = new System.Drawing.SizeF(1901F, 50F);
        this.xrTable6.StylePriority.UseBackColor = false;
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseFont = false;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celItemEntregasDetail,
            this.celTipoEntregasDetail,
            this.celNomeEntregasDetail,
            this.celDataEntregasDetail,
            this.celStatusEntregasDetail});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 0.567901234567901D;
        // 
        // celItemEntregasDetail
        // 
        this.celItemEntregasDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celItemEntregasDetail.CanShrink = true;
        this.celItemEntregasDetail.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos.edt")});
        this.celItemEntregasDetail.Dpi = 254F;
        this.celItemEntregasDetail.Font = new System.Drawing.Font("Calibri", 11F);
        this.celItemEntregasDetail.Name = "celItemEntregasDetail";
        this.celItemEntregasDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celItemEntregasDetail.StylePriority.UseBorders = false;
        this.celItemEntregasDetail.StylePriority.UseFont = false;
        this.celItemEntregasDetail.StylePriority.UsePadding = false;
        this.celItemEntregasDetail.StylePriority.UseTextAlignment = false;
        this.celItemEntregasDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celItemEntregasDetail.Weight = 0.32259644742158838D;
        this.celItemEntregasDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celItemEntregas_BeforePrint);
        // 
        // celTipoEntregasDetail
        // 
        this.celTipoEntregasDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celTipoEntregasDetail.CanShrink = true;
        this.celTipoEntregasDetail.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos.TipoEvento")});
        this.celTipoEntregasDetail.Dpi = 254F;
        this.celTipoEntregasDetail.Font = new System.Drawing.Font("Calibri", 11F);
        this.celTipoEntregasDetail.Name = "celTipoEntregasDetail";
        this.celTipoEntregasDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celTipoEntregasDetail.StylePriority.UseBorders = false;
        this.celTipoEntregasDetail.StylePriority.UseFont = false;
        this.celTipoEntregasDetail.StylePriority.UsePadding = false;
        this.celTipoEntregasDetail.StylePriority.UseTextAlignment = false;
        this.celTipoEntregasDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celTipoEntregasDetail.Weight = 0.22654442367124691D;
        this.celTipoEntregasDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celItemEntregas_BeforePrint);
        // 
        // celNomeEntregasDetail
        // 
        this.celNomeEntregasDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celNomeEntregasDetail.CanShrink = true;
        this.celNomeEntregasDetail.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos.NomeTarefa")});
        this.celNomeEntregasDetail.Dpi = 254F;
        this.celNomeEntregasDetail.Font = new System.Drawing.Font("Calibri", 11F);
        this.celNomeEntregasDetail.Name = "celNomeEntregasDetail";
        this.celNomeEntregasDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celNomeEntregasDetail.StylePriority.UseBorders = false;
        this.celNomeEntregasDetail.StylePriority.UseFont = false;
        this.celNomeEntregasDetail.StylePriority.UsePadding = false;
        this.celNomeEntregasDetail.StylePriority.UseTextAlignment = false;
        this.celNomeEntregasDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celNomeEntregasDetail.Weight = 2.5012319561168463D;
        this.celNomeEntregasDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celItemEntregas_BeforePrint);
        // 
        // celDataEntregasDetail
        // 
        this.celDataEntregasDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celDataEntregasDetail.CanShrink = true;
        this.celDataEntregasDetail.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos.TerminoPrevist" +
                    "o", "{0:dd/MM/yyyy}")});
        this.celDataEntregasDetail.Dpi = 254F;
        this.celDataEntregasDetail.Font = new System.Drawing.Font("Calibri", 11F);
        this.celDataEntregasDetail.Name = "celDataEntregasDetail";
        this.celDataEntregasDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celDataEntregasDetail.StylePriority.UseBorders = false;
        this.celDataEntregasDetail.StylePriority.UseFont = false;
        this.celDataEntregasDetail.StylePriority.UsePadding = false;
        this.celDataEntregasDetail.StylePriority.UseTextAlignment = false;
        this.celDataEntregasDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celDataEntregasDetail.Weight = 0.45163558822927735D;
        this.celDataEntregasDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celItemEntregas_BeforePrint);
        // 
        // celStatusEntregasDetail
        // 
        this.celStatusEntregasDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celStatusEntregasDetail.CanShrink = true;
        this.celStatusEntregasDetail.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos.StatusTarefa")});
        this.celStatusEntregasDetail.Dpi = 254F;
        this.celStatusEntregasDetail.Font = new System.Drawing.Font("Calibri", 11F);
        this.celStatusEntregasDetail.Name = "celStatusEntregasDetail";
        this.celStatusEntregasDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celStatusEntregasDetail.StylePriority.UseBorders = false;
        this.celStatusEntregasDetail.StylePriority.UseFont = false;
        this.celStatusEntregasDetail.StylePriority.UsePadding = false;
        this.celStatusEntregasDetail.StylePriority.UseTextAlignment = false;
        this.celStatusEntregasDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusEntregasDetail.Weight = 0.40120718635895319D;
        this.celStatusEntregasDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celItemEntregas_BeforePrint);
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 87.52422F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLabel6
        // 
        this.xrLabel6.CanShrink = true;
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Calibri", 8F);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrLabel6.SizeF = new System.Drawing.SizeF(1900F, 58.42F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Legenda: ENT - Entrega, EI - Evento Instucional, EP - Evento do Projeto";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrTable1});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 125.2709F;
        this.GroupHeader1.KeepTogether = true;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // xrLabel4
        // 
        this.xrLabel4.CanShrink = true;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0.0004882813F, 0F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrLabel4.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UsePadding = false;
        this.xrLabel4.Text = "Entregas";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.27092F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1901F, 50.00001F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell5});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.567901234567901D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.CanShrink = true;
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell2.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Item";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 0.18356487686678272D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.CanShrink = true;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell3.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "Tipo";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell3.Weight = 0.12890902325439435D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.CanShrink = true;
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell4.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Entrega";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell4.Weight = 1.4232583752022792D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.CanShrink = true;
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell1.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Data";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell1.Weight = 0.2569908747120348D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.CanShrink = true;
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell5.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "Status";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell5.Weight = 0.22829617041753886D;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3});
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Level = 1;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel23,
            this.xrLabel38,
            this.xrLabel37,
            this.xrLabel22,
            this.panelInfoLegenda,
            this.xrSubreportLegenda,
            this.xrPanel2});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 672.6226F;
        this.Detail3.KeepTogether = true;
        this.Detail3.Name = "Detail3";
        // 
        // xrLabel23
        // 
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.DataUltimaAlteracao")});
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(362.3703F, 615.9264F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(577.2538F, 50.00019F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseForeColor = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel38
        // 
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(1.00002F, 615.9256F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(361.3702F, 50.00018F);
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseForeColor = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "Data de Atualização:";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel37
        // 
        this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_StatusReport.NomeUsuarioUltimaAlteracao")});
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(130.5F, 565.9254F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(1770.5F, 49.99994F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseForeColor = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel22
        // 
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(2.499969F, 565.9256F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(128F, 50.00012F);
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseForeColor = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = "Nome:";
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // panelInfoLegenda
        // 
        this.panelInfoLegenda.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel11,
            this.xrLabel14});
        this.panelInfoLegenda.Dpi = 254F;
        this.panelInfoLegenda.LocationFloat = new DevExpress.Utils.PointFloat(0.500006F, 404.6792F);
        this.panelInfoLegenda.Name = "panelInfoLegenda";
        this.panelInfoLegenda.SizeF = new System.Drawing.SizeF(250F, 140F);
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(130F, 40F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseForeColor = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Legenda:";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(130F, 50F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseForeColor = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "Despesa";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel14
        // 
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel14.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(130F, 0F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseForeColor = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "Físico";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrSubreportLegenda
        // 
        this.xrSubreportLegenda.Dpi = 254F;
        this.xrSubreportLegenda.LocationFloat = new DevExpress.Utils.PointFloat(261.5709F, 404.6791F);
        this.xrSubreportLegenda.Name = "xrSubreportLegenda";
        this.xrSubreportLegenda.ReportSource = new rel_LegendasFisicoFinanceiro();
        this.xrSubreportLegenda.SizeF = new System.Drawing.SizeF(1629.929F, 140.0001F);
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35,
            this.xrLabel36,
            this.xrChart4,
            this.xrLabel13,
            this.xrChart5,
            this.xrLabel15});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(1898.5F, 404.6791F);
        // 
        // xrLabel35
        // 
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(2.499973F, 336.7708F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(925F, 60.00006F);
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.Text = "(Obs:) 100% refere-se a data fim do projeto.";
        // 
        // xrLabel36
        // 
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(925F, 36.77085F);
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseForeColor = false;
        this.xrLabel36.Text = "Físico";
        // 
        // xrChart4
        // 
        this.xrChart4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
        this.xrChart4.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart4.BackImage.Image")));
        this.xrChart4.BackImage.Stretch = true;
        this.xrChart4.BorderColor = System.Drawing.Color.Black;
        this.xrChart4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart4.DataMember = "StatusReport.StatusReport_StatusReport";
        this.xrChart4.DataSource = this.dsBoletimStatusRAPU;
        xyDiagram2.AxisX.Label.Visible = false;
        xyDiagram2.AxisX.MinorCount = 1;
        xyDiagram2.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram2.AxisX.Title.Text = "Fisico (%)";
        xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram2.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram2.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram2.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram2.AxisY.MinorCount = 1;
        xyDiagram2.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram2.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram2.AxisY.VisualRange.Auto = false;
        xyDiagram2.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram2.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram2.AxisY.WholeRange.Auto = false;
        xyDiagram2.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram2.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram2.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram2.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.Rotated = true;
        this.xrChart4.Diagram = xyDiagram2;
        this.xrChart4.Dpi = 254F;
        this.xrChart4.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart4.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart4.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart4.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart4.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart4.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart4.Legend.HorizontalIndent = 25;
        this.xrChart4.Legend.Name = "Default Legend";
        this.xrChart4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 36.77085F);
        this.xrChart4.Name = "xrChart4";
        series3.ArgumentDataMember = "CodigoObjeto";
        series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        //series3.DataFilters.ClearAndAddRange(new DevExpress.XtraCharts.DataFilter[] {
        //    new DevExpress.XtraCharts.XRDataFilter("CodigoObjeto", "System.Int32", DevExpress.XtraCharts.DataFilterCondition.Equal, null, "StatusReport.StatusReport_StatusReport.CodigoObjeto")});
        sideBySideBarSeriesLabel1.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel1.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
        sideBySideBarSeriesLabel1.TextPattern = "{V:G}";
        series3.Label = sideBySideBarSeriesLabel1;
        series3.LegendText = "Realizado";
        series3.LegendTextPattern = "{V:G}";
        series3.Name = "SerieRealizado";
        series3.ValueDataMembersSerializable = "PercentualFisicoRealizado";
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView1.Shadow.Size = 5;
        series3.View = sideBySideBarSeriesView1;
        series4.ArgumentDataMember = "CodigoObjeto";
        series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        //series4.DataFilters.ClearAndAddRange(new DevExpress.XtraCharts.DataFilter[] {
        //    new DevExpress.XtraCharts.XRDataFilter("CodigoObjeto", "System.Int32", DevExpress.XtraCharts.DataFilterCondition.Equal, null, "StatusReport.StatusReport_StatusReport.CodigoObjeto")});
        sideBySideBarSeriesLabel2.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel2.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel2.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
        sideBySideBarSeriesLabel2.TextColor = System.Drawing.Color.Black;
        series4.Label = sideBySideBarSeriesLabel2;
        series4.LegendText = "Previsto";
        series4.Name = "SeriePrevisto";
        series4.ValueDataMembersSerializable = "PercentualFisicoPrevisto";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView2.Shadow.Size = 5;
        series4.View = sideBySideBarSeriesView2;
        this.xrChart4.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart4.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
        this.xrChart4.SizeF = new System.Drawing.SizeF(925F, 300F);
        this.xrChart4.StylePriority.UseBackColor = false;
        this.xrChart4.StylePriority.UseBorderColor = false;
        this.xrChart4.StylePriority.UseBorders = false;
        this.xrChart4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(958.5004F, 0F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(918.4995F, 36.77082F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseForeColor = false;
        this.xrLabel13.Text = "Financeiro";
        // 
        // xrChart5
        // 
        this.xrChart5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
        this.xrChart5.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart5.BackImage.Image")));
        this.xrChart5.BackImage.Stretch = true;
        this.xrChart5.BorderColor = System.Drawing.Color.Black;
        this.xrChart5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart5.DataMember = "StatusReport.StatusReport_StatusReport";
        this.xrChart5.DataSource = this.dsBoletimStatusRAPU;
        xyDiagram3.AxisX.Label.Visible = false;
        xyDiagram3.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram3.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram3.AxisX.Title.Text = "Custo (R$)";
        xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram3.AxisX.VisualRange.Auto = false;
        xyDiagram3.AxisX.VisualRange.MaxValueSerializable = "9";
        xyDiagram3.AxisX.VisualRange.MinValueSerializable = "0";
        xyDiagram3.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram3.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram3.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram3.AxisY.MinorCount = 1;
        xyDiagram3.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram3.AxisY.NumericScaleOptions.GridSpacing = 25D;
        scaleBreak1.Edge1Serializable = "0";
        scaleBreak1.Edge2Serializable = "0";
        scaleBreak1.Name = "Scale Break 1";
        scaleBreak1.Visible = false;
        scaleBreak2.Edge1Serializable = "0";
        scaleBreak2.Edge2Serializable = "0";
        scaleBreak2.Name = "Scale Break 2";
        scaleBreak2.Visible = false;
        scaleBreak3.Edge1Serializable = "0";
        scaleBreak3.Edge2Serializable = "0";
        scaleBreak3.Name = "Scale Break 3";
        scaleBreak3.Visible = false;
        scaleBreak4.Edge1Serializable = "0";
        scaleBreak4.Edge2Serializable = "0";
        scaleBreak4.Name = "Scale Break 4";
        scaleBreak4.Visible = false;
        xyDiagram3.AxisY.ScaleBreaks.AddRange(new DevExpress.XtraCharts.ScaleBreak[] {
            scaleBreak1,
            scaleBreak2,
            scaleBreak3,
            scaleBreak4});
        xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram3.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram3.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.Rotated = true;
        this.xrChart5.Diagram = xyDiagram3;
        this.xrChart5.Dpi = 254F;
        this.xrChart5.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart5.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart5.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart5.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart5.Legend.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
        this.xrChart5.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart5.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart5.Legend.HorizontalIndent = 25;
        this.xrChart5.Legend.Name = "Default Legend";
        this.xrChart5.LocationFloat = new DevExpress.Utils.PointFloat(958.5004F, 36.77085F);
        this.xrChart5.Name = "xrChart5";
        series5.ArgumentDataMember = "CodigoObjeto";
        //series5.DataFilters.ClearAndAddRange(new DevExpress.XtraCharts.DataFilter[] {
        //    new DevExpress.XtraCharts.XRDataFilter("CodigoObjeto", "System.Int32", DevExpress.XtraCharts.DataFilterCondition.Equal, null, "StatusReport.StatusReport_StatusReport.CodigoObjeto")});
        sideBySideBarSeriesLabel4.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel4.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel4.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel4.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
        sideBySideBarSeriesLabel4.TextPattern = "{V:C2}";
        series5.Label = sideBySideBarSeriesLabel4;
        series5.LegendText = "Realizado";
        series5.Name = "SerieRealizado";
        series5.ValueDataMembersSerializable = "PercentualFinanceiroRealizado";
        sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView3.Shadow.Size = 5;
        series5.View = sideBySideBarSeriesView3;
        series6.ArgumentDataMember = "CodigoObjeto";
        //series6.DataFilters.ClearAndAddRange(new DevExpress.XtraCharts.DataFilter[] {
        //    new DevExpress.XtraCharts.XRDataFilter("CodigoObjeto", "System.Int32", DevExpress.XtraCharts.DataFilterCondition.Equal, null, "StatusReport.StatusReport_StatusReport.CodigoObjeto")});
        sideBySideBarSeriesLabel5.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel5.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel5.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel5.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
        sideBySideBarSeriesLabel5.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel5.TextPattern = "{V:C2}";
        series6.Label = sideBySideBarSeriesLabel5;
        series6.LegendName = "Default Legend";
        series6.LegendText = "Previsto";
        series6.Name = "SeriePrevisto";
        series6.ValueDataMembersSerializable = "PercentualFinanceiroPrevisto";
        sideBySideBarSeriesView4.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView4.Shadow.Size = 5;
        series6.View = sideBySideBarSeriesView4;
        this.xrChart5.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart5.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
        this.xrChart5.SizeF = new System.Drawing.SizeF(924.2505F, 300F);
        this.xrChart5.StylePriority.UseBackColor = false;
        this.xrChart5.StylePriority.UseBorderColor = false;
        this.xrChart5.StylePriority.UseBorderDashStyle = false;
        this.xrChart5.StylePriority.UseBorders = false;
        this.xrChart5.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart_CustomDrawSeriesPoint);
        this.xrChart5.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.xrChart5_CustomDrawAxisLabel);
        this.xrChart5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel15
        // 
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(964.0003F, 336.7708F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(924.9999F, 60.00006F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "(Obs:) 100% refere-se ao acumulado do início do projeto até dezembro do ano corre" +
"nte.";
        // 
        // DetailReport3
        // 
        this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4});
        this.DetailReport3.Dpi = 254F;
        this.DetailReport3.Level = 2;
        this.DetailReport3.Name = "DetailReport3";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText2,
            this.xrRichText1,
            this.xrLabel21,
            this.xrLabel10});
        this.Detail4.Dpi = 254F;
        this.Detail4.HeightF = 254F;
        this.Detail4.Name = "Detail4";
        // 
        // xrRichText2
        // 
        this.xrRichText2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "StatusReport.StatusReport_StatusReport.PontosAtencao")});
        this.xrRichText2.Dpi = 254F;
        this.xrRichText2.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 163.2297F);
        this.xrRichText2.Name = "xrRichText2";
        this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
        this.xrRichText2.SizeF = new System.Drawing.SizeF(1898.501F, 58.42F);
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "StatusReport.StatusReport_StatusReport.PrincipaisResultados")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0.5000098F, 50.00018F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1899.999F, 58.42F);
        // 
        // xrLabel21
        // 
        this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(4.037221E-05F, 113.2295F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(1901F, 50F);
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.Text = "Análise da execução financeira:";
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(4.037221E-05F, 0F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(1901F, 50F);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = "Análise da execução física:";
        // 
        // cfNomeMes
        // 
        this.cfNomeMes.DataMember = "Valores";
        this.cfNomeMes.Expression = resources.GetString("cfNomeMes.Expression");
        this.cfNomeMes.Name = "cfNomeMes";
        // 
        // DetailReport5
        // 
        this.DetailReport5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.DetailReport7});
        this.DetailReport5.Dpi = 254F;
        this.DetailReport5.Expanded = false;
        this.DetailReport5.Level = 1;
        this.DetailReport5.Name = "DetailReport5";
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDataFimPeriodoRelatorio,
            this.xrLabel40,
            this.xrLabel34,
            this.xrLabel3,
            this.xrLine1,
            this.xrChartValores});
        this.Detail6.Dpi = 254F;
        this.Detail6.HeightF = 1159.413F;
        this.Detail6.Name = "Detail6";
        this.Detail6.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
        // 
        // lblDataFimPeriodoRelatorio
        // 
        this.lblDataFimPeriodoRelatorio.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Periodo.DataFimPeriodoRelatorio")});
        this.lblDataFimPeriodoRelatorio.Dpi = 254F;
        this.lblDataFimPeriodoRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(625.0624F, 137.7464F);
        this.lblDataFimPeriodoRelatorio.Name = "lblDataFimPeriodoRelatorio";
        this.lblDataFimPeriodoRelatorio.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDataFimPeriodoRelatorio.SizeF = new System.Drawing.SizeF(550.3334F, 58.42001F);
        this.lblDataFimPeriodoRelatorio.StylePriority.UseFont = false;
        this.lblDataFimPeriodoRelatorio.StylePriority.UseTextAlignment = false;
        this.lblDataFimPeriodoRelatorio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lblDataFimPeriodoRelatorio.Visible = false;
        // 
        // xrLabel40
        // 
        this.xrLabel40.Dpi = 254F;
        this.xrLabel40.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(0F, 62.04176F);
        this.xrLabel40.Name = "xrLabel40";
        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel40.SizeF = new System.Drawing.SizeF(1898.5F, 58.42001F);
        this.xrLabel40.StylePriority.UseFont = false;
        this.xrLabel40.StylePriority.UseTextAlignment = false;
        this.xrLabel40.Text = "Relatório de Acompanhamento de Projetos";
        this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel34
        // 
        this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Periodo.Periodo")});
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(0F, 155.7195F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(550.3334F, 58.42001F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.NomeObjeto", "Unidade: {0}")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.5000098F, 234.2916F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0.5000098F, 214.1395F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1900F, 8F);
        // 
        // xrChartValores
        // 
        this.xrChartValores.BorderColor = System.Drawing.Color.Black;
        this.xrChartValores.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChartValores.DataMember = "Valores";
        this.xrChartValores.DataSource = this.dsBoletimStatusRAPU;
        xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram4.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram4.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        this.xrChartValores.Diagram = xyDiagram4;
        this.xrChartValores.Dpi = 254F;
        this.xrChartValores.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChartValores.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChartValores.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChartValores.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
        this.xrChartValores.Legend.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrChartValores.Legend.Name = "Default Legend";
        this.xrChartValores.LocationFloat = new DevExpress.Utils.PointFloat(0F, 315.5168F);
        this.xrChartValores.Name = "xrChartValores";
        series7.ArgumentDataMember = "cfNomeMes";
        series7.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel4.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAroundPoint;
        pointSeriesLabel4.TextPattern = "{V:C2}";
        series7.Label = pointSeriesLabel4;
        series7.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series7.LegendText = "Previsto";
        series7.Name = "Series 1";
        series7.ValueDataMembersSerializable = "ValorPrevisto";
        series7.View = lineSeriesView4;
        series8.ArgumentDataMember = "cfNomeMes";
        series8.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel5.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAroundPoint;
        pointSeriesLabel5.TextPattern = "{V:C2}";
        series8.Label = pointSeriesLabel5;
        series8.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series8.LegendText = "Realizado";
        series8.Name = "Series 2";
        series8.ValueDataMembersSerializable = "ValorRealizado";
        series8.View = lineSeriesView5;
        this.xrChartValores.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7,
        series8};
        pointSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChartValores.SeriesTemplate.Label = pointSeriesLabel6;
        this.xrChartValores.SeriesTemplate.View = lineSeriesView6;
        this.xrChartValores.SizeF = new System.Drawing.SizeF(1900F, 843.8959F);
        chartTitle2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
        chartTitle2.Text = "Gráfico Geral";
        this.xrChartValores.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
        // 
        // DetailReport7
        // 
        this.DetailReport7.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail8,
            this.DetailReport11});
        this.DetailReport7.Dpi = 254F;
        this.DetailReport7.Level = 0;
        this.DetailReport7.Name = "DetailReport7";
        // 
        // Detail8
        // 
        this.Detail8.Dpi = 254F;
        this.Detail8.HeightF = 0F;
        this.Detail8.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail8.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
        this.Detail8.Name = "Detail8";
        // 
        // DetailReport11
        // 
        this.DetailReport11.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail12});
        this.DetailReport11.Dpi = 254F;
        this.DetailReport11.Level = 0;
        this.DetailReport11.Name = "DetailReport11";
        this.DetailReport11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.DetailReport11_BeforePrint);
        // 
        // Detail12
        // 
        this.Detail12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrSubreport1});
        this.Detail12.Dpi = 254F;
        this.Detail12.HeightF = 256.9766F;
        this.Detail12.Name = "Detail12";
        // 
        // xrTable3
        // 
        this.xrTable3.Dpi = 254F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(109.4792F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow6,
            this.xrTableRow10});
        this.xrTable3.SizeF = new System.Drawing.SizeF(176.4922F, 256.9766F);
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableRow3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.StylePriority.UseBackColor = false;
        this.xrTableRow3.StylePriority.UseBorders = false;
        this.xrTableRow3.StylePriority.UseTextAlignment = false;
        this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.CanGrow = false;
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.Weight = 0.75254640369681369D;
        this.xrTableCell6.WordWrap = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellValorPrevisto});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // cellValorPrevisto
        // 
        this.cellValorPrevisto.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellValorPrevisto.CanGrow = false;
        this.cellValorPrevisto.Dpi = 254F;
        this.cellValorPrevisto.Font = new System.Drawing.Font("Calibri", 8F);
        this.cellValorPrevisto.Name = "cellValorPrevisto";
        this.cellValorPrevisto.StylePriority.UseBorders = false;
        this.cellValorPrevisto.StylePriority.UseFont = false;
        this.cellValorPrevisto.StylePriority.UseTextAlignment = false;
        this.cellValorPrevisto.Text = "Previsto";
        this.cellValorPrevisto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.cellValorPrevisto.Weight = 0.75254640369681369D;
        this.cellValorPrevisto.WordWrap = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellValorRealizado});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1D;
        // 
        // cellValorRealizado
        // 
        this.cellValorRealizado.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellValorRealizado.Dpi = 254F;
        this.cellValorRealizado.Font = new System.Drawing.Font("Calibri", 8F);
        this.cellValorRealizado.Name = "cellValorRealizado";
        this.cellValorRealizado.StylePriority.UseBorders = false;
        this.cellValorRealizado.StylePriority.UseFont = false;
        this.cellValorRealizado.StylePriority.UseTextAlignment = false;
        this.cellValorRealizado.Text = "Realizado";
        this.cellValorRealizado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.cellValorRealizado.Weight = 0.75254640369681369D;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellVariacao});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1D;
        // 
        // cellVariacao
        // 
        this.cellVariacao.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellVariacao.Dpi = 254F;
        this.cellVariacao.Font = new System.Drawing.Font("Calibri", 8F);
        this.cellVariacao.Name = "cellVariacao";
        this.cellVariacao.StylePriority.UseBorders = false;
        this.cellVariacao.StylePriority.UseFont = false;
        this.cellVariacao.StylePriority.UseTextAlignment = false;
        this.cellVariacao.Text = "Desvio";
        this.cellVariacao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.cellVariacao.Weight = 0.75254640369681369D;
        // 
        // xrSubreport1
        // 
        this.xrSubreport1.Dpi = 254F;
        this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(285.9714F, 0F);
        this.xrSubreport1.Name = "xrSubreport1";
        this.xrSubreport1.ReportSource = new rel_sub_Valores();
        this.xrSubreport1.SizeF = new System.Drawing.SizeF(1615.029F, 256.9766F);
        // 
        // DetailReport6
        // 
        this.DetailReport6.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.GroupHeader3,
            this.GroupHeader5});
        this.DetailReport6.DataMember = "StatusReport";
        this.DetailReport6.DataSource = this.dsBoletimStatusRAPU;
        this.DetailReport6.Dpi = 254F;
        this.DetailReport6.FilterString = "[IndicaPrograma] = \'N\'";
        this.DetailReport6.Level = 0;
        this.DetailReport6.Name = "DetailReport6";
        // 
        // Detail7
        // 
        this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.Detail7.Dpi = 254F;
        this.Detail7.HeightF = 50F;
        this.Detail7.Name = "Detail7";
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(2.499973F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(1898.5F, 50F);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell13,
            this.xrTableCell14,
            this.celPercentualPrevistoFisico,
            this.celPercentualRealizadoFisico,
            this.celPercentualPrevistoFinanceiro,
            this.celPercentualRealizadoFinanceiro});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 0.567901234567901D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.RowNum")});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Font = new System.Drawing.Font("Calibri", 9F);
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.StylePriority.UsePadding = false;
        this.xrTableCell11.Weight = 0.12807412498863402D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.NomeObjeto")});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.StylePriority.UsePadding = false;
        this.xrTableCell13.Weight = 1.0458785071407044D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.NomeGerenteProjeto")});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Font = new System.Drawing.Font("Calibri", 11F);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.StylePriority.UsePadding = false;
        this.xrTableCell14.Weight = 0.81215459698259385D;
        // 
        // celPercentualPrevistoFisico
        // 
        this.celPercentualPrevistoFisico.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celPercentualPrevistoFisico.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.PercentualFisicoPrevisto", "{0}%")});
        this.celPercentualPrevistoFisico.Dpi = 254F;
        this.celPercentualPrevistoFisico.Font = new System.Drawing.Font("Calibri", 11F);
        this.celPercentualPrevistoFisico.Name = "celPercentualPrevistoFisico";
        this.celPercentualPrevistoFisico.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celPercentualPrevistoFisico.StylePriority.UseBorders = false;
        this.celPercentualPrevistoFisico.StylePriority.UseFont = false;
        this.celPercentualPrevistoFisico.StylePriority.UsePadding = false;
        this.celPercentualPrevistoFisico.StylePriority.UseTextAlignment = false;
        this.celPercentualPrevistoFisico.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celPercentualPrevistoFisico.Weight = 0.36712271070786279D;
        this.celPercentualPrevistoFisico.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.setarACorDaFonteDeAcordoComOValorDoDesempenho_BeforePrint);
        // 
        // celPercentualRealizadoFisico
        // 
        this.celPercentualRealizadoFisico.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celPercentualRealizadoFisico.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.PercentualFisicoRealizado", "{0}%")});
        this.celPercentualRealizadoFisico.Dpi = 254F;
        this.celPercentualRealizadoFisico.Font = new System.Drawing.Font("Calibri", 11F);
        this.celPercentualRealizadoFisico.Name = "celPercentualRealizadoFisico";
        this.celPercentualRealizadoFisico.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celPercentualRealizadoFisico.StylePriority.UseBorders = false;
        this.celPercentualRealizadoFisico.StylePriority.UseFont = false;
        this.celPercentualRealizadoFisico.StylePriority.UsePadding = false;
        this.celPercentualRealizadoFisico.StylePriority.UseTextAlignment = false;
        this.celPercentualRealizadoFisico.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celPercentualRealizadoFisico.Weight = 0.40107250555517215D;
        this.celPercentualRealizadoFisico.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.setarACorDaFonteDeAcordoComOValorDoDesempenho_BeforePrint);
        // 
        // celPercentualPrevistoFinanceiro
        // 
        this.celPercentualPrevistoFinanceiro.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celPercentualPrevistoFinanceiro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.PercentualFinanceiroPrevisto", "{0}%")});
        this.celPercentualPrevistoFinanceiro.Dpi = 254F;
        this.celPercentualPrevistoFinanceiro.Font = new System.Drawing.Font("Calibri", 11F);
        this.celPercentualPrevistoFinanceiro.Name = "celPercentualPrevistoFinanceiro";
        this.celPercentualPrevistoFinanceiro.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celPercentualPrevistoFinanceiro.StylePriority.UseBorders = false;
        this.celPercentualPrevistoFinanceiro.StylePriority.UseFont = false;
        this.celPercentualPrevistoFinanceiro.StylePriority.UsePadding = false;
        this.celPercentualPrevistoFinanceiro.StylePriority.UseTextAlignment = false;
        this.celPercentualPrevistoFinanceiro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celPercentualPrevistoFinanceiro.Weight = 0.35863542144814609D;
        this.celPercentualPrevistoFinanceiro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.setarACorDaFonteDeAcordoComOValorDoDesempenho_BeforePrint);
        // 
        // celPercentualRealizadoFinanceiro
        // 
        this.celPercentualRealizadoFinanceiro.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celPercentualRealizadoFinanceiro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.PercentualFinanceiroRealizado", "{0}%")});
        this.celPercentualRealizadoFinanceiro.Dpi = 254F;
        this.celPercentualRealizadoFinanceiro.Font = new System.Drawing.Font("Calibri", 11F);
        this.celPercentualRealizadoFinanceiro.Name = "celPercentualRealizadoFinanceiro";
        this.celPercentualRealizadoFinanceiro.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.celPercentualRealizadoFinanceiro.StylePriority.UseBorders = false;
        this.celPercentualRealizadoFinanceiro.StylePriority.UseFont = false;
        this.celPercentualRealizadoFinanceiro.StylePriority.UsePadding = false;
        this.celPercentualRealizadoFinanceiro.StylePriority.UseTextAlignment = false;
        this.celPercentualRealizadoFinanceiro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celPercentualRealizadoFinanceiro.Weight = 0.40570930236791469D;
        this.celPercentualRealizadoFinanceiro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.setarACorDaFonteDeAcordoComOValorDoDesempenho_BeforePrint);
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel12,
            this.xrLabel9,
            this.xrLabel7});
        this.GroupHeader3.Dpi = 254F;
        this.GroupHeader3.HeightF = 193.3858F;
        this.GroupHeader3.Name = "GroupHeader3";
        this.GroupHeader3.RepeatEveryPage = true;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(2.499973F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(69.1029F, 193.3855F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel20
        // 
        this.xrLabel20.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel20.CanGrow = false;
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(1682.098F, 50.0001F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(218.9019F, 143.3856F);
        this.xrLabel20.StylePriority.UseBackColor = false;
        this.xrLabel20.StylePriority.UseBorders = false;
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "% REALIZADO";
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel19
        // 
        this.xrLabel19.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel19.CanGrow = false;
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(1488.595F, 50.003F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(193.5033F, 143.3827F);
        this.xrLabel19.StylePriority.UseBackColor = false;
        this.xrLabel19.StylePriority.UseBorders = false;
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "% PREVISTO";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel18
        // 
        this.xrLabel18.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel18.CanGrow = false;
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(1272.195F, 50.0001F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(216.4F, 143.3856F);
        this.xrLabel18.StylePriority.UseBackColor = false;
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = "% REALIZADO";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel17
        // 
        this.xrLabel17.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel17.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel17.CanGrow = false;
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(1074.112F, 50.0001F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(198.0825F, 143.3856F);
        this.xrLabel17.StylePriority.UseBackColor = false;
        this.xrLabel17.StylePriority.UseBorders = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "% PREVISTO";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(635.9108F, 0.0003341149F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(438.2012F, 193.3855F);
        this.xrLabel16.StylePriority.UseBorders = false;
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "GERENTE DO PROJETO";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(71.60287F, 0F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(564.3079F, 193.3855F);
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "NOME DO PROJETO";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel9.CanGrow = false;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(1488.595F, 9.918213E-05F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(412.4049F, 50.00001F);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "FINANCEIRO";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel7.CanGrow = false;
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Calibri", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(1074.112F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(414.4827F, 50.00002F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "FÍSICO";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // GroupHeader5
        // 
        this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.GroupHeader5.Dpi = 254F;
        this.GroupHeader5.HeightF = 116.4167F;
        this.GroupHeader5.Level = 1;
        this.GroupHeader5.Name = "GroupHeader5";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1898.5F, 58.41999F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Análise Simplificada -  Visão Acumulada";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // ValorCustoPrevistoPorExtenso
        // 
        this.ValorCustoPrevistoPorExtenso.DataMember = "StatusReport.StatusReport_StatusReport";
        this.ValorCustoPrevistoPorExtenso.Expression = "\'Valor Planejado em \' + [mesExtenso] + \':\'";
        this.ValorCustoPrevistoPorExtenso.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.ValorCustoPrevistoPorExtenso.Name = "ValorCustoPrevistoPorExtenso";
        // 
        // ValorCustoRealizadoPorExtenso
        // 
        this.ValorCustoRealizadoPorExtenso.DataMember = "StatusReport.StatusReport_StatusReport";
        this.ValorCustoRealizadoPorExtenso.DisplayName = "ValorCustoRealizadoPorExtenso";
        this.ValorCustoRealizadoPorExtenso.Expression = "\'Valor Realizado em \' + [mesExtenso] + \':\'";
        this.ValorCustoRealizadoPorExtenso.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.ValorCustoRealizadoPorExtenso.Name = "ValorCustoRealizadoPorExtenso";
        // 
        // nomeGerenteProjetoPorExtenso
        // 
        this.nomeGerenteProjetoPorExtenso.DataMember = "StatusReport";
        this.nomeGerenteProjetoPorExtenso.Expression = "\'Gerente do Projeto:\' + Iif(IsNullOrEmpty([NomeGerenteProjeto]), \'-\' , [NomeGeren" +
"teProjeto])";
        this.nomeGerenteProjetoPorExtenso.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.nomeGerenteProjetoPorExtenso.Name = "nomeGerenteProjetoPorExtenso";
        // 
        // NomeDoCRPorExtenso
        // 
        this.NomeDoCRPorExtenso.DataMember = "StatusReport";
        this.NomeDoCRPorExtenso.Expression = "\'Nome do CR: \' + Iif(IsNullOrEmpty([CodigosCR]), \'Sem CR vinculado\' , [CodigosCR]" +
")";
        this.NomeDoCRPorExtenso.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.NomeDoCRPorExtenso.Name = "NomeDoCRPorExtenso";
        // 
        // cfNomeMes1
        // 
        this.cfNomeMes1.DataMember = "ValoresAcumulados";
        this.cfNomeMes1.Expression = resources.GetString("cfNomeMes1.Expression");
        this.cfNomeMes1.Name = "cfNomeMes1";
        // 
        // cfDesvio
        // 
        this.cfDesvio.DataMember = "StatusReport.StatusReport_StatusReport";
        this.cfDesvio.Expression = "Iif(IsNullOrEmpty([ValorCustoPrevisto]),0  , [ValorCustoPrevisto]) - \r\nIif(IsNull" +
"OrEmpty([ValorCustoRealizado]),0  , [ValorCustoRealizado])";
        this.cfDesvio.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.cfDesvio.Name = "cfDesvio";
        // 
        // cfIdentacaoNomeTarefa
        // 
        this.cfIdentacaoNomeTarefa.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfIdentacaoNomeTarefa.Expression = "Iif(IsNullOrEmpty([NomeTarefa]),\'\', \'1\')";
        this.cfIdentacaoNomeTarefa.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfIdentacaoNomeTarefa.Name = "cfIdentacaoNomeTarefa";
        // 
        // cfLegendaEntregasProjeto
        // 
        this.cfLegendaEntregasProjeto.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfLegendaEntregasProjeto.Expression = "\'Legenda: ENT - Entrega, EI - Evento Institucional, EP - Evento do Projeto\'";
        this.cfLegendaEntregasProjeto.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfLegendaEntregasProjeto.Name = "cfLegendaEntregasProjeto";
        // 
        // cfTituloItem
        // 
        this.cfTituloItem.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfTituloItem.Expression = "Iif(IsNullOrEmpty([edtSuperior]),\'\',\'Item\' )";
        this.cfTituloItem.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfTituloItem.Name = "cfTituloItem";
        // 
        // cfTituloTipoEvento
        // 
        this.cfTituloTipoEvento.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfTituloTipoEvento.Expression = "Iif(IsNullOrEmpty([TipoEvento]),\'\',\'Tipo\' )";
        this.cfTituloTipoEvento.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfTituloTipoEvento.Name = "cfTituloTipoEvento";
        // 
        // cfTituloData
        // 
        this.cfTituloData.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfTituloData.Expression = "Iif(IsNullOrEmpty([TerminoPrevisto]),\'\',\'Data\' )";
        this.cfTituloData.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfTituloData.Name = "cfTituloData";
        // 
        // cfTituloStatusTarefa
        // 
        this.cfTituloStatusTarefa.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfTituloStatusTarefa.Expression = "Iif(IsNullOrEmpty([StatusTarefa]),\'\',\'Status\' )";
        this.cfTituloStatusTarefa.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfTituloStatusTarefa.Name = "cfTituloStatusTarefa";
        // 
        // cfTituloEntrega
        // 
        this.cfTituloEntrega.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfTituloEntrega.Expression = "Iif(IsNullOrEmpty([NomeTarefa]),\'\',\'Entrega\' )";
        this.cfTituloEntrega.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfTituloEntrega.Name = "cfTituloEntrega";
        // 
        // cfMensagemDeNenhumaEntrega
        // 
        this.cfMensagemDeNenhumaEntrega.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfMensagemDeNenhumaEntrega.Expression = "Iif(\r\nIsNullOrEmpty([NomeTarefa])\r\n,\'Não há entregas cadastradas para o projeto.\'" +
"\r\n,\'\')";
        this.cfMensagemDeNenhumaEntrega.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfMensagemDeNenhumaEntrega.Name = "cfMensagemDeNenhumaEntrega";
        // 
        // cfEntregasDoProjeto
        // 
        this.cfEntregasDoProjeto.DataMember = "StatusReport.StatusReport_StatusReport.StatusReport_MarcosCriticos";
        this.cfEntregasDoProjeto.DisplayName = "cfEntregasDoProjeto";
        this.cfEntregasDoProjeto.Expression = "Iif(IsNullOrEmpty([CodigoObjeto]),\'Não há entregas cadastradas para o projeto\',\'E" +
"ntregas do Projeto\' )";
        this.cfEntregasDoProjeto.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfEntregasDoProjeto.Name = "cfEntregasDoProjeto";
        // 
        // cfNomeObjeto
        // 
        this.cfNomeObjeto.DataMember = "StatusReport.StatusReport_StatusReport";
        this.cfNomeObjeto.Expression = "\'Dados do projeto: \' + [NomeObjeto]";
        this.cfNomeObjeto.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfNomeObjeto.Name = "cfNomeObjeto";
        // 
        // cfTituloValorReceitaRealizado
        // 
        this.cfTituloValorReceitaRealizado.DataMember = "StatusReport.StatusReport_StatusReport";
        this.cfTituloValorReceitaRealizado.Expression = "Iif(IsNullOrEmpty([ValorReceitaRealizado]), \'\' , \'Receita Realizada:\')";
        this.cfTituloValorReceitaRealizado.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfTituloValorReceitaRealizado.Name = "cfTituloValorReceitaRealizado";
        // 
        // rel_AcompanhamentoProjetosUnidade
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport,
            this.DetailReport5,
            this.DetailReport6,
            DetailReport4});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cfNomeMes,
            this.ValorCustoPrevistoPorExtenso,
            this.ValorCustoRealizadoPorExtenso,
            this.nomeGerenteProjetoPorExtenso,
            this.NomeDoCRPorExtenso,
            this.cfNomeMes1,
            this.cfDesvio,
            this.cfIdentacaoNomeTarefa,
            this.cfLegendaEntregasProjeto,
            this.cfTituloItem,
            this.cfTituloTipoEvento,
            this.cfTituloData,
            this.cfTituloStatusTarefa,
            this.cfTituloEntrega,
            this.cfMensagemDeNenhumaEntrega,
            this.cfEntregasDoProjeto,
            this.cfNomeObjeto,
            this.cfTituloValorReceitaRealizado});
        this.DataMember = "StatusReport";
        this.DataSource = this.dsBoletimStatusRAPU;
        this.Dpi = 254F;
        this.FilterString = "[CodigoStatusReportSuperior] Is Null";
        this.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(101, 98, 99, 99);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pCodigoStatusReport,
            this.pNomeUnidade});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "17.2";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.rel_AcompanhamentoProjetosUnidade_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatusRAPU)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChartValores)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrLabelNomeProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrLabelNomeProjeto.Text = string.Format("{0} - {1}"
            , ++contadorProjetos, xrLabelNomeProjeto.Text);
    }

    private void rel_AcompanhamentoProjetosUnidade_DataSourceDemanded(object sender, System.EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Format(
            "exec p_rel_BoletimQuinzenalRAPU {0}", pCodigoStatusReport.Value);
        string connectionString = cDados.classeDados.getStringConexao();
        SqlDataAdapter adapter = new SqlDataAdapter(comandoSql, connectionString);
        adapter.TableMappings.Add("Table", "StatusReport");
        adapter.TableMappings.Add("Table1", "MarcosCriticos");
        adapter.TableMappings.Add("Table2", "Valores");
        adapter.TableMappings.Add("Table3", "ValoresAcumulados");
        adapter.TableMappings.Add("Table4", "LegendaDesempenho");
        adapter.TableMappings.Add("Table5", "Periodo");
        adapter.TableMappings.Add("Table7", "TabNomeUnidade");


        adapter.Fill(dsBoletimStatusRAPU);

        string[] colunasMantidas = { "CodigoObjeto", "PercentualFisicoRealizado", "PercentualFisicoPrevisto", "PercentualFinanceiroRealizado", "PercentualFinanceiroPrevisto" };
        var dt = dsBoletimStatusRAPU.StatusReport.Copy();
        dt.TableName = "DadosProjetos";
        foreach (var columnName in dt.Columns.OfType<DataColumn>().Select(c => c.ColumnName).Where(cn => !colunasMantidas.Contains(cn)).ToList())
        {
            dt.Columns.Remove(columnName);
        }
        dsBoletimStatusRAPU.Tables.Add(dt);
        dsBoletimStatusRAPU.Relations.Add("StatusReport_DadosProjetos", dsBoletimStatusRAPU.StatusReport.CodigoObjetoColumn, dt.Columns["CodigoObjeto"]);
        xrChart4.DataMember += ".StatusReport_DadosProjetos";
        xrChart5.DataMember += ".StatusReport_DadosProjetos";

        DataSet ds = cDados.getParametrosSistema("tituloPaginasWeb");
        //if (ds.Tables[0].Rows.Count > 0)
        lblTituloPaginasWeb.Text = "Coordenação de Planejamento e Orçamento";//ds.Tables[0].Rows[0]["tituloPaginasWeb"] as string;

    }

    private void chart_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRChart chart = (XRChart)sender;
        DefineCoresSeriesGraficosStatus(chart);

    }

    private void xrChart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XRChart chart = (XRChart)sender;
        var dataMember = e.Series.ValueDataMembersSerializable;
        string nomeColuna = dataMember.Contains("Previsto") ? "CustoLBDataNoAno" : "CustoRealDataNoAno";
        //e.Series.ValueDataMembersSerializable.Replace("PercentualFinanceiro", "ValorCusto");
        object valor = DetailReport.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);


    }

    private void DefineCoresSeriesGraficosStatus(XRChart chart)
    {
        Series serieRealizado = chart.Series["SerieRealizado"];
        Color cor = Color.Black;
        string nomeColunaCor = string.Empty;
        string valueMember = serieRealizado.ValueDataMembersSerializable;

        if (valueMember.Contains("Fisico"))
            nomeColunaCor = "CorDesempenhoFisico";
        else if (valueMember.Contains("Custo") || valueMember.Contains("Financeiro"))
        {
            if (valueMember.Contains("NoAno"))
                nomeColunaCor = "CorDesempenhoCustoNoAno";
            else
                nomeColunaCor = "CorDesempenhoFinanceiro";
        }
        else if (valueMember.Contains("Receita"))
        {
            if (valueMember.Contains("NoAno"))
                nomeColunaCor = "CorDesempenhoReceitaNoAno";
            else
                nomeColunaCor = "CorDesempenhoReceita";
        }
        string nomeCor = "branco";
        if (DetailReport2.Report != null)
        {
            if (DetailReport2.Report.GetCurrentColumnValue(nomeColunaCor) != null)
            {
                if (DetailReport2.Report.GetCurrentColumnValue(nomeColunaCor).ToString().ToLower().Trim() != "")
                {
                    nomeCor = DetailReport2.Report.GetCurrentColumnValue(nomeColunaCor).ToString();
                }
            }
        }



        switch (nomeCor.ToLower())
        {
            case "verde":
                cor = Color.Green;
                break;
            case "vermelho":
                cor = Color.Red;
                break;
            case "amarelo":
                cor = Color.Yellow;
                break;
            case "azul":
                cor = Color.Blue;
                break;
            case "branco":
                cor = Color.WhiteSmoke;
                break;
        }
        serieRealizado.View.Color = cor;
    }

    private Image ObtemLogoUnidade(int codigoUsuario)
    {
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codEntidade, "");

        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            return logo;
        }
        else
            return null;
    }

    private void setarACorDaFonteDeAcordoComOValorDoDesempenho_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        /*
         Use as colunas [CorDesempenhoFisico] e [CorDesempenhoFinanceiro] (result set principal)para alterar a cor nas colunas  "Físico % Realizado" e "Financeiro % Realizado", respectivamente.
Se nestas colunas estiver escrito "Vermelho", você altera a cor da fonte para vermelho.
Qualquer outra cor que estiver nestas colunas, deixe na cor preta.         
         */

        var cor = Color.Black;

        XRTableCell celula = (XRTableCell)sender;
        if (celula.Name.Contains("Fisico"))
        {
            string corAuxiliar = (celula.Report.GetCurrentColumnValue("CorDesempenhoFisico") == null) ? "Preto" : celula.Report.GetCurrentColumnValue("CorDesempenhoFisico").ToString();
            if (corAuxiliar == "Vermelho")
            {
                cor = Color.Red;
            }
            else
            {
                cor = Color.Black;
            }
        }
        else if (celula.Name.Contains("Financeiro"))
        {
            string corAuxiliar = (celula.Report.GetCurrentColumnValue("CorDesempenhoFinanceiro") == null) ? "Preto" : celula.Report.GetCurrentColumnValue("CorDesempenhoFinanceiro").ToString();
            if (corAuxiliar == "Vermelho")
            {
                cor = Color.Red;
            }
            else
            {
                cor = Color.Black;
            }
        }
        celula.ForeColor = cor;

    }

    private void celItemEntregas_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        try
        {
            XRTableCell celula = (XRTableCell)sender;
            string indicaTarefaNegrito = (celula.Report.GetCurrentColumnValue("IndicaTarefaNegrito") != null) ?
                celula.Report.GetCurrentColumnValue("IndicaTarefaNegrito").ToString() : "";

            string statusTarefa = (celula.Report.GetCurrentColumnValue("StatusTarefa") != null) ?
                celula.Report.GetCurrentColumnValue("StatusTarefa").ToString() : "";

            string fonteImpressao = (celula.Report.GetCurrentColumnValue("FonteImpressao") != null) ?
                celula.Report.GetCurrentColumnValue("FonteImpressao").ToString() : "";

            DateTime terminoPrevisto = DateTime.MaxValue;
            if (celula.Report.GetCurrentColumnValue("TerminoPrevisto") != null)
            {
                terminoPrevisto = DateTime.Parse(celula.Report.GetCurrentColumnValue("TerminoPrevisto").ToString());
            }


            DateTime dataFimPeriodoRelatorio = DateTime.Parse(lblDataFimPeriodoRelatorio.Text);

            Color corLetra = Color.Black;
            if (statusTarefa == "Atrasada" && ((terminoPrevisto != null) && terminoPrevisto <= dataFimPeriodoRelatorio))
            {
                corLetra = Color.Red;
            }
            celula.Font = new Font("Calibri", (fonteImpressao == "FonteMenor") ? 9 : 11, indicaTarefaNegrito == "S" ? FontStyle.Bold : FontStyle.Regular);
            celula.ForeColor = corLetra;

            if (celula.Name.Equals("celNomeEntregasDetail"))
            {
                if (celula.Report.GetCurrentColumnValue("Nivel") != null)
                {
                    int nivel = int.Parse(celula.Report.GetCurrentColumnValue("Nivel").ToString());
                    celula.Padding = new DevExpress.XtraPrinting.PaddingInfo(5 + nivel * 2, 0, 0, 0);
                }
            }
        }
        catch (Exception ex)
        {
            string x = ex.Message;
        }

    }

    private void DetailReport11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrSubreport1.ReportSource.Parameters["pCodigoStatusReport"].Value = pCodigoStatusReport.Value;
        xrSubreport1.ReportSource.Parameters["pTipoGrafico"].Value = "Geral";

    }

    private void DetailReport12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrSubreport2.ReportSource.Parameters["pCodigoStatusReport"].Value = pCodigoStatusReport.Value;
        xrSubreport2.ReportSource.Parameters["pTipoGrafico"].Value = "Acumulado";
    }

    private void xrLabel42_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        decimal valorCustoPrevisto = 0;
        decimal valorCustoRealizado = 0;

        if (((XRLabel)sender).Report.GetCurrentColumnValue("ValorCustoPrevisto") != null)
        {
            valorCustoPrevisto = (decimal)((XRLabel)sender).Report.GetCurrentColumnValue("ValorCustoPrevisto");
        }
        if (((XRLabel)sender).Report.GetCurrentColumnValue("ValorCustoRealizado") != null)
        {
            valorCustoRealizado = (decimal)((XRLabel)sender).Report.GetCurrentColumnValue("ValorCustoRealizado");
        }

        //--> Se o valor Previsto no Mês igual a zero e o valor realizado for maior que zero, mostrar em vermelho
        if (valorCustoPrevisto == 0 && valorCustoRealizado > 0)
        {
            ((XRLabel)sender).ForeColor = Color.Red;
        }
        //--> Se o valor Realizado no Mês igual a zero e o valor previsto for maior que zero, mostrar em vermelho
        else if (valorCustoRealizado == 0 && valorCustoPrevisto > 0)
        {
            ((XRLabel)sender).ForeColor = Color.Red;
        }
        //--> Se o valor Previsto no Mês igual a zero e o valor realizado for igual a zero, mostrar normalmente o valor zero
        else if (valorCustoPrevisto == 0 && valorCustoRealizado == 0)
        {
            ((XRLabel)sender).ForeColor = Color.Black;
        }
        else
        {
            decimal valorDesvio = 0;
            if (valorCustoPrevisto == 0)
            {
                valorCustoPrevisto = 1;
            }
            valorDesvio = (valorCustoRealizado / valorCustoPrevisto);
            // Se o Desvio for menor que 0.70 ou Desvio for maior que 1.30, mostrar em vermelho
            if (valorDesvio < decimal.Parse("0.70") || valorDesvio > decimal.Parse("1.30"))
            {
                ((XRLabel)sender).ForeColor = Color.Red;
            }
        }
    }

    private void xrChart5_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {

        if (e.Item.AxisValueInternal > 100)
        {
            e.Item.Axis.VisualRange.Auto = true;
            e.Item.Axis.WholeRange.Auto = true;
        }
        else
        {
            e.Item.Axis.VisualRange.Auto = false;
            e.Item.Axis.WholeRange.Auto = false;
            e.Item.Axis.WholeRange.MinValue = 0;
            e.Item.Axis.WholeRange.MaxValue = 100;
            e.Item.Axis.VisualRange.MinValue = 0;
            e.Item.Axis.VisualRange.MaxValue = 100;
        }
    }
}