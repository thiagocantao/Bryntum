using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Linq;

/// <summary>
/// Summary description for rel_BoletimAcoesEstrategicasUnidade
/// </summary>
public class rel_BoletimAcoesEstrategicasUnidade : XtraReport
{
    #region Fields

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DsBoletimStatus dsBoletimStatus;
    private PageHeaderBand PageHeader;
    private XRLine xrLine1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel7;
    private XRLine xrLine2;
    private XRRichText xrRichText1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRPictureBox picLogoEntidade;
    private DetailReportBand DetailReportUnidadeSemReceita;
    private DetailBand Detail1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel2;
    private XRLine xrLine3;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel6;
    private XRPictureBox xrPictureBox3;
    private XRLabel xrLabel5;
    private XRPictureBox xrPictureBox2;
    private XRLabel xrLabel4;
    private XRPictureBox xrPictureBox1;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRPictureBox xrPictureBox4;
    private XRPictureBox xrPictureBox5;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell7;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell2;
    private XRLabel xrLabel8;
    private XRPictureBox xrPictureBox6;
    private XRLabel xrLabel10;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel9;
    private XRPageInfo xrPageInfo1;
    private XRPanel xrPanel2;
    private XRPictureBox xrPictureBox7;
    private XRLabel xrLabel21;
    private XRLabel xrLabel22;
    private XRLabel xrLabel26;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    private XRLabel lblNomeUnidade;
    private XRRichText richTextAnaliseCriticaUnidade;
    private XRSubreport xrSubreportLegenda;
    private XRPanel panelInfoLegenda;
    private XRLabel xrLabel3;
    private XRLabel xrLabel11;
    private XRLabel xrLabel14;
    private XRLabel xrLabel17;

    private dados cDados = CdadosUtil.GetCdados(null);

    string fontFamilyAnaliseCritica;
    private XRLabel xrLabel18;
    private DetailReportBand DetailReportUnidadeComReceita;
    private DetailBand Detail3;
    private GroupHeaderBand GroupHeader2;
    private GroupFooterBand GroupFooter2;
    private XRTable xrTable8;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRPictureBox xrPictureBox8;
    private XRTableCell xrTableCell18;
    private XRPictureBox xrPictureBox9;
    private XRRichText xrRichText2;
    private XRLabel lblNomeUnidadeComReceita;
    private XRTable xrTable5;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell10;
    private XRTable xrTable6;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTable xrTable7;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell13;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRLabel xrLabel19;
    private XRLine xrLine4;
    private XRPictureBox xrPictureBox13;
    private XRLabel xrLabel31;
    private XRLabel xrLabel32;
    private XRLabel xrLabel34;
    private XRLabel xrLabel33;
    private XRLabel xrLabel29;
    private XRPictureBox xrPictureBox10;
    private XRPictureBox xrPictureBox11;
    private XRPictureBox xrPictureBox12;
    private XRLabel xrLabel30;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRPictureBox xrPictureBox14;
    private XRPanel xrPanel1;
    private XRPanel pnlCusto;
    private XRChart xrChart1;
    private XRLabel xrLabel13;
    private XRChart xrChart5;
    private XRLabel xrLabel15;
    private XRPanel pnlReceita;
    private XRChart xrChart2;
    private XRLabel xrLabel27;
    private XRChart xrChart3;
    private XRLabel xrLabel28;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRChart xrChart4;
    private XRLabel xrLabel37;
    private XRLabel xrLabel38;
    private XRLabel xrLabel12;
    private CalculatedField labelAnaliseCritica;
    private XRPanel xrPanel3;
    string fontSizeAnaliseCritica;

    #endregion

    #region Constructors

    public rel_BoletimAcoesEstrategicasUnidade(int codigoStatusReport)
    {
        InitializeComponent();
        InitData(codigoStatusReport);
    }

    #endregion

    #region Methods

    private void InitData(int codigoStatusReport)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;

        #region Comando SQL

        comandoSql = string.Format(@"exec p_rel_BoletimQuinzenal01 {0}", codigoStatusReport);

        #endregion

        string[] tableNames = new string[] { "Projetos", "DadosEntrega", "LegendaDesempenho", "Produtos" };

        DataSet dsTemp = cDados.getDataSet(comandoSql);
        dsBoletimStatus.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);
        foreach (var p in dsBoletimStatus.Projetos)
        {
            if (!p.IsPercentualFisicoPrevistoNull() && p.PercentualFisicoPrevisto > 100)
                p.PercentualFisicoPrevisto = 100;
            if (!p.IsPercentualReceitaPrevistoNull() && p.PercentualReceitaPrevisto > 100)
                p.PercentualReceitaPrevisto = 100;
            if (!p.IsPercentualFinanceiroPrevistoNull() && p.PercentualFinanceiroPrevisto > 100)
                p.PercentualFinanceiroPrevisto = 100;

            if (!p.IsPercentualFisicoRealizadoNull() && p.PercentualFisicoRealizado > 100)
                p.PercentualFisicoRealizado = 100;
            if (!p.IsPercentualReceitaRealizadoNull() && p.PercentualReceitaRealizado > 100)
                p.PercentualReceitaRealizado = 100;
            if (!p.IsPercentualFinanceiroRealizadoNull() && p.PercentualFinanceiroRealizado > 100)
                p.PercentualFinanceiroRealizado = 100;
        }
        DataView projetos = dsBoletimStatus.Projetos.DefaultView;
        projetos.Sort = "NomeObjeto";

        DefineLogoRelatorio();
        var unidade = dsBoletimStatus.Projetos.Single(p => p.IndicaPrograma.ToUpper() == "S");
        lblNomeUnidadeComReceita.Text = unidade.NomeObjeto;
        lblNomeUnidade.Text = unidade.NomeObjeto;

        xrSubreportLegenda.ReportSource.DataSource = dsBoletimStatus;

        DefineConfiguracoesFontAnaliseCritica();
        richTextAnaliseCriticaUnidade.Html =
            string.Format("<div style=\"font-family: {1}; font-size: {2}pt;\">{0}</div>"
            , unidade.AnaliseCritica, fontFamilyAnaliseCritica, fontSizeAnaliseCritica);

        if (unidade.IsValorReceitaPrevistoNull() || unidade.ValorReceitaPrevisto == 0)
        {
            DetailReportUnidadeComReceita.Visible = false;
            DetailReportUnidadeSemReceita.Visible = true;
        }
        else
        {
            DetailReportUnidadeComReceita.Visible = true;
            DetailReportUnidadeSemReceita.Visible = false;
        }
    }

    private void DefineConfiguracoesFontAnaliseCritica()
    {
        DataSet dsParams = cDados.getParametrosSistema(
                    "fontFamilyAnaliseCritica_Boletim",
                    "fontSizeAnaliseCritica_Boletim");
        DataRow drParam = dsParams.Tables[0].Rows[0];
        fontFamilyAnaliseCritica =
            drParam["fontFamilyAnaliseCritica_Boletim"] as string;
        fontSizeAnaliseCritica =
            drParam["fontSizeAnaliseCritica_Boletim"] as string;
        if (string.IsNullOrWhiteSpace(fontFamilyAnaliseCritica))
            fontFamilyAnaliseCritica = "Verdana";
        if (string.IsNullOrWhiteSpace(fontSizeAnaliseCritica))
            fontSizeAnaliseCritica = "11";
    }

    private void DefineLogoRelatorio()
    {
        DataSet dsTemp = cDados.getParametrosSistema("UrlLogo01BoletimStatus");
        if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0 &&
            dsTemp.Tables[0].Columns.IndexOf("UrlLogo01BoletimStatus") > -1)
        {
            string urlLogo01BoletimStatus =
                dsTemp.Tables[0].Rows[0]["UrlLogo01BoletimStatus"] as string;
            picLogoEntidade.ImageUrl = urlLogo01BoletimStatus;
        }

        if (string.IsNullOrWhiteSpace(picLogoEntidade.ImageUrl))
        {
            System.Drawing.Image logo = cDados.ObtemLogoEntidade();
            picLogoEntidade.Image = logo;
        }
    }

    //private Image ObtemLogoEntidade()
    //{
    //    int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
    //    DataSet dsLogoUnidade = cDados.getLogoEntidade(codEntidade, "");
    //    if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
    //    {
    //        byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
    //        System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
    //        Image logo = Image.FromStream(stream);
    //        return logo;
    //    }
    //    else
    //        return null;
    //}

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

    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "rel_BoletimAcoesEstrategicasUnidade.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_BoletimAcoesEstrategicasUnidade.ResourceManager;
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView4 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel7 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView5 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel8 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView6 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel9 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel10 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView7 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel11 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView8 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel12 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram5 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel13 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView9 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series10 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel14 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView10 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel15 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.dsBoletimStatus = new DsBoletimStatus();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportUnidadeSemReceita = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox5 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.richTextAnaliseCriticaUnidade = new DevExpress.XtraReports.UI.XRRichText();
        this.lblNomeUnidade = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox6 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.pnlCusto = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart5 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlReceita = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart4 = new DevExpress.XtraReports.UI.XRChart();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrSubreportLegenda = new DevExpress.XtraReports.UI.XRSubreport();
        this.panelInfoLegenda = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox7 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportUnidadeComReceita = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox8 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox9 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox14 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
        this.lblNomeUnidadeComReceita = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrPictureBox13 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox10 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox11 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox12 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.labelAnaliseCritica = new DevExpress.XtraReports.UI.CalculatedField();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatus)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.richTextAnaliseCriticaUnidade)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Dpi = 254F;
        this.Detail.Expanded = false;
        this.Detail.HeightF = 0F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // dsBoletimStatus
        // 
        this.dsBoletimStatus.DataSetName = "DsBoletimStatus";
        this.dsBoletimStatus.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projetos.AnaliseCritica")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 10F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 69.06281F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1894F, 50F);
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.NomeObjeto")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseForeColor = false;
        this.xrLabel7.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblNomeObjeto_EvaluateBinding);
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.ForeColor = System.Drawing.Color.Maroon;
        this.xrLine2.LineWidth = 5;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.00002F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1900F, 25F);
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 99.00001F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrPageInfo1.Format = "{0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1644.953F, 40.58001F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel10,
            this.picLogoEntidade,
            this.xrLine1,
            this.xrLabel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 309.9999F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel9
        // 
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.ForeColor = System.Drawing.Color.Black;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseForeColor = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Período: Novembro de 2012";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        this.xrLabel9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel9_BeforePrint);
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(1273.953F, 250F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(624.9999F, 50F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "Emissão: {0:dd/MM/yyyy HH:mm:ss}";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        this.xrLabel10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel10_BeforePrint);
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(1275F, 0F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(625F, 250F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.Blue;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 299.9999F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1900F, 10F);
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 90F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1275F, 74.99998F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Relatório de Projetos da Unidade ";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // DetailReportUnidadeSemReceita
        // 
        this.DetailReportUnidadeSemReceita.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1,
            this.GroupFooter1});
        this.DetailReportUnidadeSemReceita.DataMember = "Projetos";
        this.DetailReportUnidadeSemReceita.DataSource = this.dsBoletimStatus;
        this.DetailReportUnidadeSemReceita.Dpi = 254F;
        this.DetailReportUnidadeSemReceita.FilterString = "[IndicaPrograma] <> \'S\' And [IndicaPrograma] <> \'s\'";
        this.DetailReportUnidadeSemReceita.Level = 0;
        this.DetailReportUnidadeSemReceita.Name = "DetailReportUnidadeSemReceita";
        this.DetailReportUnidadeSemReceita.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 50F;
        this.Detail1.Name = "Detail1";
        this.Detail1.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.NomeObjeto")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 2.36842118112664D;
        this.xrTableCell4.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblNomeObjeto_EvaluateBinding);
        this.xrTableCell4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.nomeProjeto_BeforePrint);
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox4});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell5.Weight = 0.315789481715152D;
        // 
        // xrPictureBox4
        // 
        this.xrPictureBox4.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoFisico", "~/imagens/{0}.gif")});
        this.xrPictureBox4.Dpi = 254F;
        this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(75F, 2.5F);
        this.xrPictureBox4.Name = "xrPictureBox4";
        this.xrPictureBox4.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox4.StylePriority.UseBorders = false;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox5});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.315789529900802D;
        // 
        // xrPictureBox5
        // 
        this.xrPictureBox5.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox5.Dpi = 254F;
        this.xrPictureBox5.LocationFloat = new DevExpress.Utils.PointFloat(75F, 2.5F);
        this.xrPictureBox5.Name = "xrPictureBox5";
        this.xrPictureBox5.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox5.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox5.StylePriority.UseBorders = false;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.richTextAnaliseCriticaUnidade,
            this.lblNomeUnidade,
            this.xrTable1,
            this.xrLabel2,
            this.xrLine3});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 250F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // richTextAnaliseCriticaUnidade
        // 
        this.richTextAnaliseCriticaUnidade.Dpi = 254F;
        this.richTextAnaliseCriticaUnidade.Font = new System.Drawing.Font("Verdana", 10F);
        this.richTextAnaliseCriticaUnidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
        this.richTextAnaliseCriticaUnidade.Name = "richTextAnaliseCriticaUnidade";
        this.richTextAnaliseCriticaUnidade.SerializableRtfString = resources.GetString("richTextAnaliseCriticaUnidade.SerializableRtfString");
        this.richTextAnaliseCriticaUnidade.SizeF = new System.Drawing.SizeF(1900F, 50F);
        // 
        // lblNomeUnidade
        // 
        this.lblNomeUnidade.Dpi = 254F;
        this.lblNomeUnidade.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.lblNomeUnidade.ForeColor = System.Drawing.Color.Maroon;
        this.lblNomeUnidade.LocationFloat = new DevExpress.Utils.PointFloat(390F, 0F);
        this.lblNomeUnidade.Name = "lblNomeUnidade";
        this.lblNomeUnidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNomeUnidade.SizeF = new System.Drawing.SizeF(1509.047F, 50F);
        this.lblNomeUnidade.StylePriority.UseFont = false;
        this.lblNomeUnidade.StylePriority.UseForeColor = false;
        this.lblNomeUnidade.StylePriority.UseTextAlignment = false;
        this.lblNomeUnidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.BackColor = System.Drawing.Color.SkyBlue;
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1900F, 100F);
        this.xrTable1.StylePriority.UseBackColor = false;
        this.xrTable1.StylePriority.UseBorders = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell7});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Weight = 2.36842105263158D;
        // 
        // xrTable4
        // 
        this.xrTable4.BackColor = System.Drawing.Color.SkyBlue;
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable4.ForeColor = System.Drawing.Color.White;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable4.SizeF = new System.Drawing.SizeF(1500F, 100F);
        this.xrTable4.StylePriority.UseBackColor = false;
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UseForeColor = false;
        this.xrTable4.StylePriority.UseTextAlignment = false;
        this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1.99999938964862D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "Projetos";
        this.xrTableCell9.Weight = 2.36842111687911D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Weight = 0.631578947368421D;
        // 
        // xrTable3
        // 
        this.xrTable3.BackColor = System.Drawing.Color.SkyBlue;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable3.ForeColor = System.Drawing.Color.White;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4});
        this.xrTable3.SizeF = new System.Drawing.SizeF(400F, 100F);
        this.xrTable3.StylePriority.UseBackColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseForeColor = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.Text = "Desempenho";
        this.xrTableCell3.Weight = 0.317451491252867D;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell2});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.Text = "Físico";
        this.xrTableCell8.Weight = 0.158725744611999D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.Text = "R$";
        this.xrTableCell2.Weight = 0.158725746640868D;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(390F, 50F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Projetos da Unidade: ";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLine3
        // 
        this.xrLine3.Dpi = 254F;
        this.xrLine3.ForeColor = System.Drawing.Color.Maroon;
        this.xrLine3.LineWidth = 5;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.00002F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(1900F, 25F);
        this.xrLine3.StylePriority.UseForeColor = false;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel8,
            this.xrPictureBox6,
            this.xrLabel6,
            this.xrPictureBox3,
            this.xrLabel5,
            this.xrPictureBox2,
            this.xrLabel4,
            this.xrPictureBox1});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 100F;
        this.GroupFooter1.Name = "GroupFooter1";
        this.GroupFooter1.StylePriority.UseBorderWidth = false;
        // 
        // xrLabel22
        // 
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(1900F, 30F);
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseForeColor = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = "(*) Os projetos que estão em negrito referem-se aos projetos estratégicos.";
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel21
        // 
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 29.99994F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(130F, 50F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseForeColor = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = "Legenda:";
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(793.9531F, 29.99994F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(230F, 50F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseForeColor = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Sem informações";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox6
        // 
        this.xrPictureBox6.Dpi = 254F;
        this.xrPictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox6.Image")));
        this.xrPictureBox6.LocationFloat = new DevExpress.Utils.PointFloat(743.9531F, 29.99994F);
        this.xrPictureBox6.Name = "xrPictureBox6";
        this.xrPictureBox6.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox6.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(593.953F, 29.99994F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(150F, 50F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseForeColor = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Crítico";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.Dpi = 254F;
        this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(543.953F, 29.99994F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(393.9529F, 29.99994F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(150F, 50F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseForeColor = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "Atenção";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(343.9531F, 29.99994F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(193.953F, 29.99994F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(150F, 50F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseForeColor = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Bom";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(143.953F, 29.99994F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
        this.DetailReport1.DataMember = "Projetos";
        this.DetailReport1.DataSource = this.dsBoletimStatus;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.FilterString = "[IndicaPrograma] <> \'S\' And [IndicaPrograma] <> \'s\'";
        this.DetailReport1.Level = 2;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3,
            this.xrPanel1,
            this.xrPanel2});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 1553.104F;
        this.Detail2.KeepTogether = true;
        this.Detail2.Name = "Detail2";
        this.Detail2.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
        this.Detail2.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail2_BeforePrint);
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlCusto,
            this.pnlReceita,
            this.xrLabel35,
            this.xrLabel36,
            this.xrChart4});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1900F, 1250F);
        // 
        // pnlCusto
        // 
        this.pnlCusto.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37,
            this.xrChart1,
            this.xrLabel13,
            this.xrChart5,
            this.xrLabel15});
        this.pnlCusto.Dpi = 254F;
        this.pnlCusto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 430F);
        this.pnlCusto.Name = "pnlCusto";
        this.pnlCusto.SizeF = new System.Drawing.SizeF(1900F, 410F);
        // 
        // xrLabel37
        // 
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(973.5003F, 8.074442E-05F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(924.9996F, 50F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseForeColor = false;
        this.xrLabel37.Text = "Despesa no Ano";
        // 
        // xrChart1
        // 
        this.xrChart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
        this.xrChart1.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart1.BackImage.Image")));
        this.xrChart1.BackImage.Stretch = true;
        this.xrChart1.BorderColor = System.Drawing.Color.Black;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "Projetos";
        this.xrChart1.DataSource = this.dsBoletimStatus;
        xyDiagram1.AxisX.Label.Visible = false;
        xyDiagram1.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram1.AxisX.Title.Text = "Custo (R$)";
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram1.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram1.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram1.AxisY.MinorCount = 1;
        xyDiagram1.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram1.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.VisualRange.Auto = false;
        xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram1.AxisY.WholeRange.Auto = false;
        xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram1.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.Rotated = true;
        this.xrChart1.Diagram = xyDiagram1;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart1.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart1.Legend.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
        this.xrChart1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart1.Legend.HorizontalIndent = 25;
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(975F, 50F);
        this.xrChart1.Name = "xrChart1";
        series1.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel1.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel1.TextPattern = "{V:C2}";
        series1.Label = sideBySideBarSeriesLabel1;
        series1.LegendText = "Realizado";
        series1.Name = "SerieRealizado";
        series1.ValueDataMembersSerializable = "PercentualCustoRealDataNoAno";
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView1.Shadow.Size = 5;
        series1.View = sideBySideBarSeriesView1;
        series2.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel2.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel2.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel2.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel2.TextPattern = "{V:C2}";
        series2.Label = sideBySideBarSeriesLabel2;
        series2.LegendText = "Previsto";
        series2.Name = "SeriePrevisto";
        series2.ValueDataMembersSerializable = "PercentualCustoLBDataNoAno";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView2.Shadow.Size = 5;
        series2.View = sideBySideBarSeriesView2;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
        this.xrChart1.SizeF = new System.Drawing.SizeF(925F, 300F);
        this.xrChart1.StylePriority.UseBackColor = false;
        this.xrChart1.StylePriority.UseBorderColor = false;
        this.xrChart1.StylePriority.UseBorderDashStyle = false;
        this.xrChart1.StylePriority.UseBorders = false;
        this.xrChart1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart_CustomDrawSeriesPoint);
        this.xrChart1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(1.499989F, 0F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(924.9996F, 50F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseForeColor = false;
        this.xrLabel13.Text = "Despesa Total";
        // 
        // xrChart5
        // 
        this.xrChart5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
        this.xrChart5.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart5.BackImage.Image")));
        this.xrChart5.BackImage.Stretch = true;
        this.xrChart5.BorderColor = System.Drawing.Color.Black;
        this.xrChart5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart5.DataMember = "Projetos";
        this.xrChart5.DataSource = this.dsBoletimStatus;
        xyDiagram2.AxisX.Label.Visible = false;
        xyDiagram2.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram2.AxisX.Title.Text = "Custo (R$)";
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
        this.xrChart5.Diagram = xyDiagram2;
        this.xrChart5.Dpi = 254F;
        this.xrChart5.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart5.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart5.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart5.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart5.Legend.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
        this.xrChart5.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart5.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart5.Legend.HorizontalIndent = 25;
        this.xrChart5.LocationFloat = new DevExpress.Utils.PointFloat(1.499666F, 50.00018F);
        this.xrChart5.Name = "xrChart5";
        series3.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel4.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel4.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel4.TextPattern = "{V:C2}";
        series3.Label = sideBySideBarSeriesLabel4;
        series3.LegendText = "Realizado";
        series3.Name = "SerieRealizado";
        series3.ValueDataMembersSerializable = "PercentualFinanceiroRealizado";
        sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView3.Shadow.Size = 5;
        series3.View = sideBySideBarSeriesView3;
        series4.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel5.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel5.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel5.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel5.TextPattern = "{V:C2}";
        series4.Label = sideBySideBarSeriesLabel5;
        series4.LegendText = "Previsto";
        series4.Name = "SeriePrevisto";
        series4.ValueDataMembersSerializable = "PercentualFinanceiroPrevisto";
        sideBySideBarSeriesView4.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView4.Shadow.Size = 5;
        series4.View = sideBySideBarSeriesView4;
        this.xrChart5.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart5.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
        this.xrChart5.SizeF = new System.Drawing.SizeF(925F, 300F);
        this.xrChart5.StylePriority.UseBackColor = false;
        this.xrChart5.StylePriority.UseBorderColor = false;
        this.xrChart5.StylePriority.UseBorderDashStyle = false;
        this.xrChart5.StylePriority.UseBorders = false;
        this.xrChart5.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart5_CustomDrawSeriesPoint);
        this.xrChart5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel15
        // 
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(1.499666F, 350.0002F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(924.9999F, 60.00006F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        // 
        // pnlReceita
        // 
        this.pnlReceita.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrChart2,
            this.xrLabel27,
            this.xrChart3,
            this.xrLabel28});
        this.pnlReceita.Dpi = 254F;
        this.pnlReceita.LocationFloat = new DevExpress.Utils.PointFloat(0F, 840F);
        this.pnlReceita.Name = "pnlReceita";
        this.pnlReceita.SizeF = new System.Drawing.SizeF(1900F, 410F);
        // 
        // xrLabel38
        // 
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(973.5003F, 0F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(923.5001F, 50.00012F);
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseForeColor = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "Receita no Ano";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrChart2
        // 
        this.xrChart2.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart2.BackImage.Image")));
        this.xrChart2.BackImage.Stretch = true;
        this.xrChart2.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart2.DataMember = "Projetos";
        xyDiagram3.AxisX.Label.Visible = false;
        xyDiagram3.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram3.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram3.AxisX.Title.Text = "Receita (R$)";
        xyDiagram3.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram3.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram3.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram3.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram3.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram3.AxisY.MinorCount = 1;
        xyDiagram3.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram3.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram3.AxisY.VisualRange.Auto = false;
        xyDiagram3.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram3.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram3.AxisY.WholeRange.Auto = false;
        xyDiagram3.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram3.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram3.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram3.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.Rotated = true;
        this.xrChart2.Diagram = xyDiagram3;
        this.xrChart2.Dpi = 254F;
        this.xrChart2.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart2.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart2.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart2.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart2.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart2.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart2.Legend.HorizontalIndent = 25;
        this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(974.9999F, 50.00002F);
        this.xrChart2.Name = "xrChart2";
        series5.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel7.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel7.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel7.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel7.TextPattern = "{V:C2}";
        series5.Label = sideBySideBarSeriesLabel7;
        series5.LegendText = "Realizado";
        series5.Name = "SerieRealizado";
        series5.ValueDataMembersSerializable = "PercentualReceitaRealDataNoAno";
        sideBySideBarSeriesView5.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series5.View = sideBySideBarSeriesView5;
        series6.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel8.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel8.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel8.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel8.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel8.TextPattern = "{V:C2}";
        series6.Label = sideBySideBarSeriesLabel8;
        series6.LegendText = "Previsto";
        series6.Name = "SeriePrevisto";
        series6.ValueDataMembersSerializable = "PercentualReceitaLBDataNoAno";
        sideBySideBarSeriesView6.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView6.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series6.View = sideBySideBarSeriesView6;
        this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
        sideBySideBarSeriesLabel9.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart2.SeriesTemplate.Label = sideBySideBarSeriesLabel9;
        this.xrChart2.SizeF = new System.Drawing.SizeF(923.5001F, 300F);
        this.xrChart2.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart_CustomDrawSeriesPoint);
        this.xrChart2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel27
        // 
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(923.5001F, 50.00012F);
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseForeColor = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "Receita Total";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrChart3
        // 
        this.xrChart3.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart3.BackImage.Image")));
        this.xrChart3.BackImage.Stretch = true;
        this.xrChart3.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart3.DataMember = "Projetos";
        xyDiagram4.AxisX.Label.Visible = false;
        xyDiagram4.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram4.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram4.AxisX.Title.Text = "Receita (R$)";
        xyDiagram4.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram4.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram4.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram4.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram4.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram4.AxisY.MinorCount = 1;
        xyDiagram4.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram4.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram4.AxisY.VisualRange.Auto = false;
        xyDiagram4.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram4.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram4.AxisY.WholeRange.Auto = false;
        xyDiagram4.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram4.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram4.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram4.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.Rotated = true;
        this.xrChart3.Diagram = xyDiagram4;
        this.xrChart3.Dpi = 254F;
        this.xrChart3.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart3.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart3.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart3.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart3.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart3.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart3.Legend.HorizontalIndent = 25;
        this.xrChart3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.99994F);
        this.xrChart3.Name = "xrChart3";
        series7.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel10.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel10.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel10.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel10.TextPattern = "{V:C2}";
        series7.Label = sideBySideBarSeriesLabel10;
        series7.LegendText = "Realizado";
        series7.Name = "SerieRealizado";
        series7.ValueDataMembersSerializable = "PercentualReceitaRealizado";
        sideBySideBarSeriesView7.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series7.View = sideBySideBarSeriesView7;
        series8.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel11.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel11.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel11.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel11.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel11.TextPattern = "{V:C2}";
        series8.Label = sideBySideBarSeriesLabel11;
        series8.LegendText = "Previsto";
        series8.Name = "SeriePrevisto";
        series8.ValueDataMembersSerializable = "PercentualReceitaPrevisto";
        sideBySideBarSeriesView8.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView8.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series8.View = sideBySideBarSeriesView8;
        this.xrChart3.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7,
        series8};
        sideBySideBarSeriesLabel12.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart3.SeriesTemplate.Label = sideBySideBarSeriesLabel12;
        this.xrChart3.SizeF = new System.Drawing.SizeF(923.5001F, 300F);
        this.xrChart3.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart3_CustomDrawSeriesPoint);
        this.xrChart3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel28
        // 
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 349.9999F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(924.9999F, 60.00006F);
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        // 
        // xrLabel35
        // 
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(0F, 359.9998F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(925F, 60.00006F);
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.Text = "(*) 100% refere-se a data fim do projeto.";
        // 
        // xrLabel36
        // 
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(925F, 50F);
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
        this.xrChart4.DataMember = "Projetos";
        this.xrChart4.DataSource = this.dsBoletimStatus;
        xyDiagram5.AxisX.Label.Visible = false;
        xyDiagram5.AxisX.MinorCount = 1;
        xyDiagram5.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram5.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram5.AxisX.Title.Text = "Fisico (%)";
        xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram5.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram5.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram5.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram5.AxisY.MinorCount = 1;
        xyDiagram5.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram5.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram5.AxisY.VisualRange.Auto = false;
        xyDiagram5.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram5.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram5.AxisY.WholeRange.Auto = false;
        xyDiagram5.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram5.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram5.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram5.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.Rotated = true;
        this.xrChart4.Diagram = xyDiagram5;
        this.xrChart4.Dpi = 254F;
        this.xrChart4.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart4.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart4.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart4.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart4.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart4.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart4.Legend.HorizontalIndent = 25;
        this.xrChart4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 59.99985F);
        this.xrChart4.Name = "xrChart4";
        series9.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel13.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel13.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel13.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel13.TextPattern = "{V:G}";
        series9.Label = sideBySideBarSeriesLabel13;
        series9.LegendText = "Realizado";
        series9.LegendTextPattern = "{V:G}";
        series9.Name = "SerieRealizado";
        series9.ValueDataMembersSerializable = "PercentualFisicoRealizado";
        sideBySideBarSeriesView9.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView9.Shadow.Size = 5;
        series9.View = sideBySideBarSeriesView9;
        series10.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel14.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel14.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel14.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel14.TextColor = System.Drawing.Color.Black;
        series10.Label = sideBySideBarSeriesLabel14;
        series10.LegendText = "Previsto";
        series10.Name = "SeriePrevisto";
        series10.ValueDataMembersSerializable = "PercentualFisicoPrevisto";
        sideBySideBarSeriesView10.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView10.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        sideBySideBarSeriesView10.Shadow.Size = 5;
        series10.View = sideBySideBarSeriesView10;
        this.xrChart4.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series9,
        series10};
        sideBySideBarSeriesLabel15.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart4.SeriesTemplate.Label = sideBySideBarSeriesLabel15;
        this.xrChart4.SizeF = new System.Drawing.SizeF(925F, 300F);
        this.xrChart4.StylePriority.UseBackColor = false;
        this.xrChart4.StylePriority.UseBorderColor = false;
        this.xrChart4.StylePriority.UseBorders = false;
        this.xrChart4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel7,
            this.xrLine2});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(1900F, 150F);
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataTerminoPrevistoProjeto", "{0:dd/MM/yyyy}")});
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel26.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(540F, 74.99995F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(210F, 50F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseForeColor = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "Início:";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel25.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(300F, 74.99995F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(240F, 50F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseForeColor = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "Término Previsto:";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel24
        // 
        this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataInicioProjeto", "{0:dd/MM/yyyy}")});
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel24.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(90F, 74.99995F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(210F, 50F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseForeColor = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "Início:";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel23
        // 
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel23.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(90F, 50F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseForeColor = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.Text = "Início:";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportLegenda,
            this.panelInfoLegenda,
            this.xrPictureBox7,
            this.xrLabel18});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Expanded = false;
        this.PageFooter.HeightF = 360F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrSubreportLegenda
        // 
        this.xrSubreportLegenda.Dpi = 254F;
        this.xrSubreportLegenda.LocationFloat = new DevExpress.Utils.PointFloat(250F, 0F);
        this.xrSubreportLegenda.Name = "xrSubreportLegenda";
        this.xrSubreportLegenda.ReportSource = new rel_LegendasFisicoFinanceiro();
        this.xrSubreportLegenda.SizeF = new System.Drawing.SizeF(1648.5F, 140F);
        // 
        // panelInfoLegenda
        // 
        this.panelInfoLegenda.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17,
            this.xrLabel3,
            this.xrLabel11,
            this.xrLabel14});
        this.panelInfoLegenda.Dpi = 254F;
        this.panelInfoLegenda.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.panelInfoLegenda.Name = "panelInfoLegenda";
        this.panelInfoLegenda.SizeF = new System.Drawing.SizeF(250F, 140F);
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel17.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(130F, 100F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseForeColor = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "Receita";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(130F, 40F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseForeColor = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Legenda:";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
        // xrPictureBox7
        // 
        this.xrPictureBox7.Dpi = 254F;
        this.xrPictureBox7.ImageUrl = "~\\espacoCliente\\Rodape01BoletimStatus.png";
        this.xrPictureBox7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 140F);
        this.xrPictureBox7.Name = "xrPictureBox7";
        this.xrPictureBox7.SizeF = new System.Drawing.SizeF(1900F, 175F);
        this.xrPictureBox7.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 320F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1902F, 40F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrLabel18.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel18_BeforePrint);
        // 
        // DetailReportUnidadeComReceita
        // 
        this.DetailReportUnidadeComReceita.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader2,
            this.GroupFooter2});
        this.DetailReportUnidadeComReceita.DataMember = "Projetos";
        this.DetailReportUnidadeComReceita.DataSource = this.dsBoletimStatus;
        this.DetailReportUnidadeComReceita.Dpi = 254F;
        this.DetailReportUnidadeComReceita.FilterString = "[IndicaPrograma] <> \'S\' And [IndicaPrograma] <> \'s\'";
        this.DetailReportUnidadeComReceita.Level = 1;
        this.DetailReportUnidadeComReceita.Name = "DetailReportUnidadeComReceita";
        this.DetailReportUnidadeComReceita.Visible = false;
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 50F;
        this.Detail3.Name = "Detail3";
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.Dpi = 254F;
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable8.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrTable8.StylePriority.UseBorders = false;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell20});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.NomeObjeto")});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "xrTableCell4";
        this.xrTableCell16.Weight = 2.17105256733141D;
        this.xrTableCell16.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblNomeObjeto_EvaluateBinding);
        this.xrTableCell16.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.nomeProjeto_BeforePrint);
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox8});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell17.Weight = 0.276315797504626D;
        // 
        // xrPictureBox8
        // 
        this.xrPictureBox8.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoFisico", "~/imagens/{0}.gif")});
        this.xrPictureBox8.Dpi = 254F;
        this.xrPictureBox8.LocationFloat = new DevExpress.Utils.PointFloat(65F, 0F);
        this.xrPictureBox8.Name = "xrPictureBox8";
        this.xrPictureBox8.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox8.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox8.StylePriority.UseBorders = false;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox9});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell18.Weight = 0.276315781442743D;
        // 
        // xrPictureBox9
        // 
        this.xrPictureBox9.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox9.Dpi = 254F;
        this.xrPictureBox9.LocationFloat = new DevExpress.Utils.PointFloat(65F, 0F);
        this.xrPictureBox9.Name = "xrPictureBox9";
        this.xrPictureBox9.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox9.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox9.StylePriority.UseBorders = false;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox14});
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Weight = 0.276315853721217D;
        // 
        // xrPictureBox14
        // 
        this.xrPictureBox14.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrPictureBox14.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoReceita", "~/imagens/{0}.gif")});
        this.xrPictureBox14.Dpi = 254F;
        this.xrPictureBox14.LocationFloat = new DevExpress.Utils.PointFloat(65F, 0F);
        this.xrPictureBox14.Name = "xrPictureBox14";
        this.xrPictureBox14.SizeF = new System.Drawing.SizeF(45F, 45F);
        this.xrPictureBox14.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox14.StylePriority.UseBorders = false;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText2,
            this.lblNomeUnidadeComReceita,
            this.xrTable5,
            this.xrLabel19,
            this.xrLine4});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.HeightF = 250F;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrRichText2
        // 
        this.xrRichText2.Dpi = 254F;
        this.xrRichText2.Font = new System.Drawing.Font("Verdana", 10F);
        this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(1F, 75F);
        this.xrRichText2.Name = "xrRichText2";
        this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
        this.xrRichText2.SizeF = new System.Drawing.SizeF(1900F, 50F);
        // 
        // lblNomeUnidadeComReceita
        // 
        this.lblNomeUnidadeComReceita.Dpi = 254F;
        this.lblNomeUnidadeComReceita.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.lblNomeUnidadeComReceita.ForeColor = System.Drawing.Color.Maroon;
        this.lblNomeUnidadeComReceita.LocationFloat = new DevExpress.Utils.PointFloat(391F, 0F);
        this.lblNomeUnidadeComReceita.Name = "lblNomeUnidadeComReceita";
        this.lblNomeUnidadeComReceita.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNomeUnidadeComReceita.SizeF = new System.Drawing.SizeF(1509.047F, 50F);
        this.lblNomeUnidadeComReceita.StylePriority.UseFont = false;
        this.lblNomeUnidadeComReceita.StylePriority.UseForeColor = false;
        this.lblNomeUnidadeComReceita.StylePriority.UseTextAlignment = false;
        this.lblNomeUnidadeComReceita.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTable5
        // 
        this.xrTable5.BackColor = System.Drawing.Color.SkyBlue;
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.Dpi = 254F;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(1F, 150F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable5.SizeF = new System.Drawing.SizeF(1900F, 100F);
        this.xrTable5.StylePriority.UseBackColor = false;
        this.xrTable5.StylePriority.UseBorders = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell12});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Weight = 2.17105263157895D;
        // 
        // xrTable6
        // 
        this.xrTable6.BackColor = System.Drawing.Color.SkyBlue;
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable6.ForeColor = System.Drawing.Color.White;
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable6.SizeF = new System.Drawing.SizeF(1375F, 100F);
        this.xrTable6.StylePriority.UseBackColor = false;
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseBorderWidth = false;
        this.xrTable6.StylePriority.UseFont = false;
        this.xrTable6.StylePriority.UseForeColor = false;
        this.xrTable6.StylePriority.UseTextAlignment = false;
        this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1.99999938964862D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "Projetos";
        this.xrTableCell11.Weight = 2.36842111687911D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Weight = 0.828947368421053D;
        // 
        // xrTable7
        // 
        this.xrTable7.BackColor = System.Drawing.Color.SkyBlue;
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable7.ForeColor = System.Drawing.Color.White;
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8,
            this.xrTableRow9});
        this.xrTable7.SizeF = new System.Drawing.SizeF(525F, 100F);
        this.xrTable7.StylePriority.UseBackColor = false;
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseFont = false;
        this.xrTable7.StylePriority.UseForeColor = false;
        this.xrTable7.StylePriority.UseTextAlignment = false;
        this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.Text = "Desempenho";
        this.xrTableCell13.Weight = 0.317451491252867D;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell19,
            this.xrTableCell15});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.Text = "Físico";
        this.xrTableCell14.Weight = 0.105817162736521D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "Despesa";
        this.xrTableCell19.Weight = 0.105817164258173D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.Text = "Receita";
        this.xrTableCell15.Weight = 0.105817164258173D;
        // 
        // xrLabel19
        // 
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(1F, 0F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(390F, 50F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseForeColor = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "Projetos da Unidade: ";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLine4
        // 
        this.xrLine4.Dpi = 254F;
        this.xrLine4.ForeColor = System.Drawing.Color.Maroon;
        this.xrLine4.LineWidth = 5;
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(1F, 50.00002F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(1900F, 25F);
        this.xrLine4.StylePriority.UseForeColor = false;
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox13,
            this.xrLabel31,
            this.xrLabel32,
            this.xrLabel34,
            this.xrLabel33,
            this.xrLabel29,
            this.xrPictureBox10,
            this.xrPictureBox11,
            this.xrPictureBox12,
            this.xrLabel30});
        this.GroupFooter2.Dpi = 254F;
        this.GroupFooter2.HeightF = 100F;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // xrPictureBox13
        // 
        this.xrPictureBox13.Dpi = 254F;
        this.xrPictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox13.Image")));
        this.xrPictureBox13.LocationFloat = new DevExpress.Utils.PointFloat(744.9532F, 29.99994F);
        this.xrPictureBox13.Name = "xrPictureBox13";
        this.xrPictureBox13.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox13.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel31
        // 
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(594.953F, 29.99994F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(150F, 50F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseForeColor = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "Crítico";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel32
        // 
        this.xrLabel32.Dpi = 254F;
        this.xrLabel32.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(794.9531F, 29.99994F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(230F, 50F);
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseForeColor = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.Text = "Sem informações";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel34
        // 
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(1.00002F, 0F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(1900F, 30F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseForeColor = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = "(*) Os projetos que estão em negrito referem-se aos projetos estratégicos.";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel33
        // 
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(1.00002F, 29.99994F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(130F, 50F);
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseForeColor = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = "Legenda:";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel29
        // 
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(194.953F, 29.99994F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(150F, 50F);
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseForeColor = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "Bom";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox10
        // 
        this.xrPictureBox10.Dpi = 254F;
        this.xrPictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox10.Image")));
        this.xrPictureBox10.LocationFloat = new DevExpress.Utils.PointFloat(144.953F, 29.99994F);
        this.xrPictureBox10.Name = "xrPictureBox10";
        this.xrPictureBox10.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox10.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrPictureBox11
        // 
        this.xrPictureBox11.Dpi = 254F;
        this.xrPictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox11.Image")));
        this.xrPictureBox11.LocationFloat = new DevExpress.Utils.PointFloat(344.9531F, 29.99994F);
        this.xrPictureBox11.Name = "xrPictureBox11";
        this.xrPictureBox11.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox11.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrPictureBox12
        // 
        this.xrPictureBox12.Dpi = 254F;
        this.xrPictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox12.Image")));
        this.xrPictureBox12.LocationFloat = new DevExpress.Utils.PointFloat(544.953F, 29.99994F);
        this.xrPictureBox12.Name = "xrPictureBox12";
        this.xrPictureBox12.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox12.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(394.953F, 29.99994F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(150F, 50F);
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.StylePriority.UseForeColor = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = "Atenção";
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel12
        // 
        this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.labelAnaliseCritica")});
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.64292F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(1896.917F, 58.42004F);
        // 
        // labelAnaliseCritica
        // 
        this.labelAnaliseCritica.DataMember = "Projetos";
        this.labelAnaliseCritica.DataSource = this.dsBoletimStatus;
        this.labelAnaliseCritica.Expression = "Iif(IsNullOrEmpty([AnaliseCritica]), \'\' ,\'Análise Crítica:\' )";
        this.labelAnaliseCritica.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.labelAnaliseCritica.Name = "labelAnaliseCritica";
        // 
        // xrPanel3
        // 
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12,
            this.xrRichText1});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1416.979F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(1897F, 129.6458F);
        // 
        // rel_BoletimAcoesEstrategicasUnidade
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.DetailReportUnidadeSemReceita,
            this.DetailReport1,
            this.PageFooter,
            this.DetailReportUnidadeComReceita});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.labelAnaliseCritica});
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 10F);
        this.Margins = new System.Drawing.Printing.Margins(99, 99, 99, 99);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "15.1";
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatus)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.richTextAnaliseCriticaUnidade)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Event Handlers

    private void lblAnaliseCritica_EvaluateBinding(object sender, BindingEventArgs e)
    {
        //XRRichText richText = (XRRichText)sender;
        //String strValue = (e.Value as string) ?? string.Empty;
        //if (String.IsNullOrEmpty(strValue.Trim()))
        //{
        //    richText.Font = new Font(
        //                        fontFamilyAnaliseCritica,
        //                        float.Parse(fontSizeAnaliseCritica),
        //                        FontStyle.Italic,
        //                        GraphicsUnit.Point,
        //                        ((byte)(0)));
        //    strValue = "[Sem análise crítica para o período]";
        //}
        //strValue = string.Format(
        //    "<div style=\"font-family: {1}; font-size: {2}pt;\">{0}</div>"
        //    , strValue, fontFamilyAnaliseCritica, fontSizeAnaliseCritica);
        //e.Value = strValue;
    }

    #endregion

    private void xrLabel9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        var projeto = dsBoletimStatus.Projetos.First();
        string periodo = string.Format("{0:d} a {1:d}",
            projeto.DataInicioPeriodoRelatorio,
            projeto.DataTerminoPeriodoRelatorio);
        label.Text = string.Format("Período: {0:}", UppercaseFirst(periodo));
    }

    static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    private void chart_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRChart chart = (XRChart)sender;
        DefineCoresSeriesGraficosStatus(chart);
        int codigoObjeto = Convert.ToInt32(chart.Report.GetCurrentColumnValue("CodigoObjeto"));
        foreach (Series serie in chart.Series)
        {
            serie.DataFilters.ClearAndAddRange(new DataFilter[] {
                new DataFilter("CodigoObjeto", "System.Int32", DataFilterCondition.Equal, codigoObjeto)});
        }
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

        string nomeCor = chart.Report.GetCurrentColumnValue(nomeColunaCor).ToString();

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
        }
        serieRealizado.View.Color = cor;
    }

    private void xrLabel10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        label.Text = string.Format("Emissão: {0:dd/MM/yyyy HH:mm:ss}",
            dsBoletimStatus.Projetos.First().DataEmissaoRelatorio);
    }

    private void lblNomeObjeto_EvaluateBinding(object sender, BindingEventArgs e)
    {
        DataRowView row = (DataRowView)((XRControl)sender).Report.GetCurrentRow();
        var lista = row.DataView.OfType<DataRowView>()
            .Where(r => r["IndicaPrograma"].ToString().ToUpper() != "S")
            .OrderBy(r => "NomeObjeto")
            .ToList();
        int indice = lista.IndexOf(row);
        e.Value = string.Format("{0} - {1}", indice + 1, e.Value);
    }

    private void xrChart5_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XtraReportBase report = ((XRControl)sender).Report;
        string nomeColuna = e.Series.Name == "SerieRealizado" ?
            "ValorCustoRealizadoOriginal" : "ValorCustoPrevistoOriginal";
        object valor = report.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);
    }

    private void nomeProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRControl control = (XRControl)sender;
        XtraReportBase report = control.Report;
        string indicaProjetoEstrategico =
            report.GetCurrentColumnValue("IndicaProjetoEstrategico") as string;
        if (indicaProjetoEstrategico == "S")
            control.Font = new Font("Verdana", 10F, FontStyle.Bold | FontStyle.Italic);
        else
            control.Font = new Font("Verdana", 10F, FontStyle.Regular);
    }

    private void xrChart3_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XtraReportBase report = ((XRControl)sender).Report;
        string nomeColuna = e.Series.Name == "SerieRealizado" ?
            "ValorReceitaRealizadoOriginal" : "ValorReceitaPrevistoOriginal";
        object valor = report.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);
    }

    private void xrPanel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //XRPanel panel = (XRPanel)sender;
        //object valorReceitaPrev =
        //    panel.Report.GetCurrentColumnValue("PercentualReceitaPrevisto");
        //object valorReceitaReal =
        //    panel.Report.GetCurrentColumnValue("PercentualReceitaRealizado");
        //bool existeValoresReceita =
        //    !(Convert.IsDBNull(valorReceitaPrev) || ((decimal)valorReceitaPrev) == 0) ||
        //    !(Convert.IsDBNull(valorReceitaReal) || ((decimal)valorReceitaReal) == 0);
        //if (existeValoresReceita)
        //{
        //    if (!panel.Controls.Contains(pnlReceita))
        //    {
        //        panel.Band.HeightF = 1025;
        //        panel.HeightF = 775;
        //        panel.Controls.Add(pnlReceita);
        //        pnlReceita.LocationF = new PointF(0, 425);
        //    }
        //}
        //else if (panel.Controls.Contains(pnlReceita))
        //{
        //    panel.Controls.Remove(pnlReceita);
        //    panel.HeightF = 425;
        //    panel.Band.HeightF = 675;
        //}
    }

    private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        #region Comentario
        //XRPanel panel = xrPanel1;
        //object valorReceitaPrev =
        //    panel.Report.GetCurrentColumnValue("PercentualReceitaPrevisto");
        //object valorReceitaReal =
        //    panel.Report.GetCurrentColumnValue("PercentualReceitaRealizado");
        //bool existeValoresReceita =
        //    !(Convert.IsDBNull(valorReceitaPrev) || ((decimal)valorReceitaPrev) == 0) ||
        //    !(Convert.IsDBNull(valorReceitaReal) || ((decimal)valorReceitaReal) == 0);
        //if (existeValoresReceita)
        //{
        //    if (!panel.Controls.Contains(pnlReceita))
        //    {
        //        panel.Band.HeightF = 1025;
        //        panel.HeightF = 775;
        //        xrRichText1.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
        //        panel.Controls.Add(pnlReceita);
        //        pnlReceita.LocationF = new PointF(0, 425);
        //    }
        //}
        //else if (panel.Controls.Contains(pnlReceita))
        //{
        //    panel.Controls.Remove(pnlReceita);
        //    panel.HeightF = 425;
        //    xrRichText1.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
        //    panel.Band.HeightF = 675;
        //} 
        #endregion

        DetailBand detail = (DetailBand)sender;
        XRPanel panel = xrPanel1;
        XRRichText rtAnaliseCritica = xrRichText1;
        DataRowView drv = (DataRowView)detail.Report.GetCurrentRow();
        decimal? valorReceitaPrev = drv.Row.Field<decimal?>("PercentualReceitaPrevisto");
        decimal? valorReceitaReal = drv.Row.Field<decimal?>("PercentualReceitaRealizado");
        bool existeValoresReceita =
            (valorReceitaPrev.HasValue || valorReceitaReal.HasValue) &&
            (valorReceitaPrev.Value != 0 || valorReceitaReal.Value != 0);

        const int INT_alturaPanelGraficoReceita = 410;
        if (panel.Controls.Contains(pnlReceita))
        {
            if (!existeValoresReceita)
            {
                panel.Controls.Remove(pnlReceita);
                panel.HeightF -= INT_alturaPanelGraficoReceita;
                float x = 0;
                float y = panel.LocationF.Y + panel.SizeF.Height + 25;
                rtAnaliseCritica.LocationF = new PointF(x, y);
                detail.HeightF -= INT_alturaPanelGraficoReceita;
            }
        }
        else
        {
            if (existeValoresReceita)
            {
                detail.HeightF += INT_alturaPanelGraficoReceita;
                float x = 0;
                float y = panel.LocationF.Y + panel.SizeF.Height +
                    INT_alturaPanelGraficoReceita + 25;
                rtAnaliseCritica.LocationF = new PointF(x, y);
                panel.HeightF += INT_alturaPanelGraficoReceita;
                panel.Controls.Add(pnlReceita);
                x = 0;
                y = pnlCusto.LocationF.Y + pnlCusto.SizeF.Height;
                pnlReceita.LocationF = new PointF(x, y);
            }
        }
    }

    private void xrLabel18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        DataSet dsParams = cDados.getParametrosSistema("TextoRodape_RPU");
        if (dsParams.Tables[0].Rows.Count > 0)
            label.Text = dsParams.Tables[0].Rows[0]["TextoRodape_RPU"] as string;
    }

    private void xrChart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XRChart chart = (XRChart)sender;
        string nomeColuna = e.Series.ValueDataMembersSerializable.Replace(
            "Percentual", string.Empty);
        object valor = chart.Report.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);
    }
}
