using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

/// <summary>
/// Summary description for rel_BoletimStatus
/// </summary>
public class rel_BoletimStatus : XtraReport
{
    #region fields

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private PageFooterBand PageFooter;
    private XRSubreport xrSubreport1;
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private XRPageInfo xrPageInfo1;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel3;
    private XRLine xrLine1;
    private XRLine xrLine2;
    #endregion

    private XRLabel xrLabel6;
    private DsBoletimStatus ds;
    private XRLabel lblDataAnalise;
    private XRRichText xrRichText2;
    private XRLabel xrlAnaliseCritica;
    private XRLabel xrlStatus;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRLabel xrLabel19;
    private XRPanel pnlValoresReceitaCapa;
    private XRLabel xrLabel14;
    private XRLabel xrLabel15;
    private XRLabel xrLabel16;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox3;
    private XRLabel xrLabel17;
    private XRLabel xrLabel12;
    private XRChart xrChart3;
    private XRLabel xrLabel13;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel25;
    private XRLabel xrLabel21;
    private XRSubreport xrSubreport2;
    private XRLabel xrLabel20;
    private XRPanel pnlValoresReceita;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private XRLabel xrLabel28;
    private XRPictureBox xrPictureBox6;
    private XRPictureBox xrPictureBox7;
    private XRLabel xrLabel29;
    private XRLabel xrLabel30;
    private XRChart xrChart1;
    private XRLabel xrLabel31;
    private XRPanel pnlValoresFisico;
    private XRChart xrChart4;
    private XRLabel xrLabel37;
    private XRPictureBox xrPictureBox9;
    private XRPictureBox xrPictureBox8;
    private XRLabel xrLabel36;
    private XRLabel xrLabel35;
    private XRLabel xrLabel34;
    private XRLabel xrLabel33;
    private XRLabel xrLabel32;
    private XRPanel pnlValoresCusto;
    private XRLabel xrLabel42;
    private XRPictureBox xrPictureBox11;
    private XRChart xrChart5;
    private XRLabel xrLabel43;
    private XRPictureBox xrPictureBox10;
    private XRLabel xrLabel41;
    private XRLabel xrLabel40;
    private XRLabel xrLabel39;
    private XRLabel xrLabel38;
    private XRPanel pnlValoresCustoCapa;
    private XRPictureBox xrPictureBox5;
    private XRPictureBox xrPictureBox4;
    private XRChart xrChart2;
    private XRLabel xrLabel7;
    private XRLabel xrLabel18;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRLabel xrLabel8;
    private XRLabel xrLabel11;
    private XRRichText xrRichText1;


    private dados cDados = CdadosUtil.GetCdados(null);

    #region Constructors

    public rel_BoletimStatus(int codigoStatusReport)
    {
        InitializeComponent();
        InitData(codigoStatusReport);
    }

    #endregion

    #region Methods
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

    private void DefineCoresSeriesGraficosStatus(XRChart chart)
    {
        Series serieRealizado = chart.Series["SerieRealizado"];
        Color cor = Color.Black;
        string nomeColunaCor = string.Empty;
        string valueMember = serieRealizado.ValueDataMembersSerializable;

        if (valueMember.Contains("PercentualFisicoRealizado"))
            nomeColunaCor = "CorDesempenhoFisico";
        else if (valueMember.Contains("ValorCustoRealizado"))
            nomeColunaCor = "CorDesempenhoFinanceiro";
        else if (valueMember.Contains("ValorReceitaRealizado"))
            nomeColunaCor = "CorDesempenhoReceita";
        string nomeCor = (string)GetCurrentColumnValue(nomeColunaCor);

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

    private void InitData(int codigoStatusReport)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        xrPictureBox1.Image = logo;

        #region Comando SQL

        comandoSql = string.Format(@"exec p_rel_BoletimQuinzenal01 {0}", codigoStatusReport);

        #endregion

        string[] tableNames = new string[] { "Projetos", "DadosEntrega", "LegendaDesempenho", "Produtos" };

        DataSet dsTemp = cDados.getDataSet(comandoSql);
        ds.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);

        DefineCodigoObjetoSuperior();

        xrSubreport1.ReportSource = new rel_SubreportLegendasFisicoFinanceiro(ds.LegendaDesempenho);
        //DsBoletimStatus dsSub = (DsBoletimStatus) xrSubreport2.ReportSource.DataSource;
        //dsSub.Produtos.Load(ds.Produtos.CreateDataReader());
        xrSubreport2.ReportSource.DataSource = ds.Copy();
        xrSubreport2.ReportSource.DataMember = "Produtos";
    }

    private void DefineCodigoObjetoSuperior()
    {
        DsBoletimStatus.ProjetosRow objetoSuperior =
            ds.Projetos.Where(p => p.IndicaPrograma.ToUpper() == "S").Single();
        List<DsBoletimStatus.ProjetosRow> projetos =
            ds.Projetos.Where(p => p.IndicaPrograma.ToUpper() != "S").ToList();
        projetos.ForEach(p => p.CodigoObjetoSuperior = objetoSuperior.CodigoObjeto);
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
    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "rel_BoletimStatus.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_BoletimStatus.ResourceManager;
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
        this.pnlValoresCustoCapa = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPictureBox5 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
        this.ds = new DsBoletimStatus();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlValoresReceitaCapa = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDataAnalise = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrlAnaliseCritica = new DevExpress.XtraReports.UI.XRLabel();
        this.xrlStatus = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.pnlValoresFisico = new DevExpress.XtraReports.UI.XRPanel();
        this.xrChart4 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox9 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox8 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlValoresCusto = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox11 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrChart5 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox10 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlValoresReceita = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox6 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPictureBox7 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlValoresCustoCapa,
            this.pnlValoresReceitaCapa,
            this.lblDataAnalise,
            this.xrRichText2,
            this.xrlAnaliseCritica,
            this.xrlStatus,
            this.xrLabel3});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 750F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("IndicaPrograma", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending),
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // pnlValoresCustoCapa
        // 
        this.pnlValoresCustoCapa.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox5,
            this.xrPictureBox4,
            this.xrChart2,
            this.xrLabel7,
            this.xrLabel18,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel8,
            this.xrLabel11});
        this.pnlValoresCustoCapa.Dpi = 254F;
        this.pnlValoresCustoCapa.LocationFloat = new DevExpress.Utils.PointFloat(0F, 299.9999F);
        this.pnlValoresCustoCapa.Name = "pnlValoresCustoCapa";
        this.pnlValoresCustoCapa.SizeF = new System.Drawing.SizeF(900.5F, 400F);
        // 
        // xrPictureBox5
        // 
        this.xrPictureBox5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Projetos.CorDesempenhoFinanceiro"),
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox5.Dpi = 254F;
        this.xrPictureBox5.LocationFloat = new DevExpress.Utils.PointFloat(0.2499695F, 300F);
        this.xrPictureBox5.Name = "xrPictureBox5";
        this.xrPictureBox5.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox5.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrPictureBox4
        // 
        this.xrPictureBox4.Dpi = 254F;
        this.xrPictureBox4.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(0.2499695F, 249.9999F);
        this.xrPictureBox4.Name = "xrPictureBox4";
        this.xrPictureBox4.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrChart2
        // 
        this.xrChart2.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart2.DataMember = "Projetos";
        this.xrChart2.DataSource = this.ds;
        xyDiagram1.AxisX.Label.Visible = false;
        xyDiagram1.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram1.AxisX.Title.Text = "Custo (R$)";
        xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.Label.Angle = 15;
        xyDiagram1.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram1.AxisY.Label.TextPattern = "{V:N0} K";
        xyDiagram1.AxisY.MinorCount = 1;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.Rotated = true;
        this.xrChart2.Diagram = xyDiagram1;
        this.xrChart2.Dpi = 254F;
        this.xrChart2.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(0.2499695F, 0F);
        this.xrChart2.Name = "xrChart2";
        series1.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series1.Label = sideBySideBarSeriesLabel1;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.Name = "SerieRealizado";
        series1.ValueDataMembersSerializable = "ValorCustoRealizado";
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series1.View = sideBySideBarSeriesView1;
        series2.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series2.Label = sideBySideBarSeriesLabel2;
        series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.Name = "SeriePrevisto";
        series2.ValueDataMembersSerializable = "ValorCustoPrevisto";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series2.View = sideBySideBarSeriesView2;
        this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart2.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
        this.xrChart2.SizeF = new System.Drawing.SizeF(900F, 250F);
        this.xrChart2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // ds
        // 
        this.ds.DataSetName = "DsBoletimStatus";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(50.24999F, 249.9998F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(224.9792F, 50F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Previsto:";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(50.24999F, 350F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(224.9792F, 50F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = "Desvio:";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.ValorCustoPrevisto", "{0:n0}")});
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(275.2292F, 249.9999F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(625.0209F, 50F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "xrLabel9";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel9.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.ValorCustoRealizado", "{0:n0}")});
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(275.2292F, 299.9998F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(625.0209F, 50F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "xrLabel10";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel10.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(50.24999F, 299.9998F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(224.9792F, 50F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Realizado:";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DesvioCusto", "{0:n0} %")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(275.2292F, 350F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(625.0209F, 50.00006F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "xrLabel9";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // pnlValoresReceitaCapa
        // 
        this.pnlValoresReceitaCapa.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel15,
            this.xrLabel16,
            this.xrPictureBox2,
            this.xrPictureBox3,
            this.xrLabel17,
            this.xrLabel12,
            this.xrChart3,
            this.xrLabel13});
        this.pnlValoresReceitaCapa.Dpi = 254F;
        this.pnlValoresReceitaCapa.LocationFloat = new DevExpress.Utils.PointFloat(900.5F, 300F);
        this.pnlValoresReceitaCapa.Name = "pnlValoresReceitaCapa";
        this.pnlValoresReceitaCapa.SizeF = new System.Drawing.SizeF(900F, 400F);
        this.pnlValoresReceitaCapa.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.pnlReceita_BeforePrint);
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.ValorReceitaPrevisto", "{0:n0}")});
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(275F, 250F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(624.5F, 50F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "xrLabel14";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel14.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.ValorReceitaRealizado", "{0:n0}")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(275F, 300F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(624.5F, 50F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "xrLabel15";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel15.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel16
        // 
        this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DesvioReceita", "{0:n0} %")});
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(275F, 350F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(624.5F, 50F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "xrLabel9";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoReceita", "~/imagens/{0}.gif")});
        this.xrPictureBox3.Dpi = 254F;
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 300F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(50F, 350F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "Desvio:";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(50F, 250F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Previsto:";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrChart3
        // 
        this.xrChart3.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart3.DataMember = "Projetos";
        this.xrChart3.DataSource = this.ds;
        xyDiagram2.AxisX.Label.Visible = false;
        xyDiagram2.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram2.AxisX.Title.Text = "Receita (R$)";
        xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram2.AxisY.Label.Angle = 15;
        xyDiagram2.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisY.Label.TextPattern = "{V:N0} K";
        xyDiagram2.AxisY.MinorCount = 1;
        xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram2.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.Rotated = true;
        this.xrChart3.Diagram = xyDiagram2;
        this.xrChart3.Dpi = 254F;
        this.xrChart3.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart3.LocationFloat = new DevExpress.Utils.PointFloat(5F, 0F);
        this.xrChart3.Name = "xrChart3";
        series3.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series3.Label = sideBySideBarSeriesLabel4;
        series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.Name = "SerieRealizado";
        series3.ValueDataMembersSerializable = "ValorReceitaRealizado";
        sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series3.View = sideBySideBarSeriesView3;
        series4.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series4.Label = sideBySideBarSeriesLabel5;
        series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series4.Name = "SeriePrevisto";
        series4.ValueDataMembersSerializable = "ValorReceitaPrevisto";
        sideBySideBarSeriesView4.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series4.View = sideBySideBarSeriesView4;
        this.xrChart3.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart3.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
        this.xrChart3.SizeF = new System.Drawing.SizeF(894.5F, 250F);
        this.xrChart3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(50F, 300F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "Realizado:";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblDataAnalise
        // 
        this.lblDataAnalise.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataAnalisePerformance", "Data desta Análise Crítica: {0:dd/MM/yyyy}")});
        this.lblDataAnalise.Dpi = 254F;
        this.lblDataAnalise.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDataAnalise.LocationFloat = new DevExpress.Utils.PointFloat(0F, 175F);
        this.lblDataAnalise.Name = "lblDataAnalise";
        this.lblDataAnalise.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDataAnalise.SizeF = new System.Drawing.SizeF(762F, 38.1F);
        this.lblDataAnalise.StylePriority.UseFont = false;
        this.lblDataAnalise.Text = "lblDataAnalise";
        // 
        // xrRichText2
        // 
        this.xrRichText2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projetos.AnaliseCritica")});
        this.xrRichText2.Dpi = 254F;
        this.xrRichText2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 125F);
        this.xrRichText2.Name = "xrRichText2";
        this.xrRichText2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
        this.xrRichText2.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrRichText2.StylePriority.UseFont = false;
        this.xrRichText2.StylePriority.UseTextAlignment = false;
        this.xrRichText2.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblAnaliseCritica_EvaluateBinding);
        // 
        // xrlAnaliseCritica
        // 
        this.xrlAnaliseCritica.Dpi = 254F;
        this.xrlAnaliseCritica.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrlAnaliseCritica.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrlAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
        this.xrlAnaliseCritica.Name = "xrlAnaliseCritica";
        this.xrlAnaliseCritica.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlAnaliseCritica.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrlAnaliseCritica.StylePriority.UseFont = false;
        this.xrlAnaliseCritica.StylePriority.UseForeColor = false;
        this.xrlAnaliseCritica.Text = "Análise Crítica";
        // 
        // xrlStatus
        // 
        this.xrlStatus.Dpi = 254F;
        this.xrlStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrlStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrlStatus.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrlStatus.Name = "xrlStatus";
        this.xrlStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlStatus.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrlStatus.StylePriority.UseFont = false;
        this.xrlStatus.StylePriority.UseForeColor = false;
        this.xrlStatus.Text = "Desempenho";
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.NomeObjeto")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 100F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Format = "{0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1546F, 41.58F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 100F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLine1,
            this.xrLabel1,
            this.xrPictureBox1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 325F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataEmissaoRelatorio", "Emissão: {0:dd/MM/yyyy HH:mm:ss}")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1150F, 225F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(650F, 50F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 275F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1800F, 10F);
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 225F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(830F, 50F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "BOLETIM DE STATUS";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(400F, 200F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrSubreport1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 60F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1800F, 10F);
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // xrSubreport1
        // 
        this.xrSubreport1.Dpi = 254F;
        this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrSubreport1.Name = "xrSubreport1";
        this.xrSubreport1.SizeF = new System.Drawing.SizeF(1800F, 50F);
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1});
        this.DetailReport.DataMember = "Projetos.Projetos_Projetos";
        this.DetailReport.DataSource = this.ds;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1,
            this.pnlValoresFisico,
            this.pnlValoresCusto,
            this.pnlValoresReceita,
            this.xrLabel25,
            this.xrLabel21,
            this.xrSubreport2,
            this.xrLabel20});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 1125F;
        this.Detail1.Name = "Detail1";
        this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail1.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projetos.Projetos_Projetos.AnaliseCritica")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 175F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrRichText1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblAnaliseCritica_EvaluateBinding);
        // 
        // pnlValoresFisico
        // 
        this.pnlValoresFisico.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart4,
            this.xrLabel37,
            this.xrPictureBox9,
            this.xrPictureBox8,
            this.xrLabel36,
            this.xrLabel35,
            this.xrLabel34,
            this.xrLabel33,
            this.xrLabel32});
        this.pnlValoresFisico.Dpi = 254F;
        this.pnlValoresFisico.LocationFloat = new DevExpress.Utils.PointFloat(0F, 300F);
        this.pnlValoresFisico.Name = "pnlValoresFisico";
        this.pnlValoresFisico.SizeF = new System.Drawing.SizeF(900.5F, 400F);
        // 
        // xrChart4
        // 
        this.xrChart4.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart4.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart4.DataSource = this.ds;
        xyDiagram3.AxisX.Label.Visible = false;
        xyDiagram3.AxisX.MinorCount = 1;
        xyDiagram3.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram3.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram3.AxisX.Title.Text = "Fisico (%)";
        xyDiagram3.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram3.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram3.AxisY.MinorCount = 1;
        xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram3.AxisY.VisualRange.Auto = false;
        xyDiagram3.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram3.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram3.AxisY.WholeRange.Auto = false;
        xyDiagram3.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram3.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram3.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram3.Rotated = true;
        this.xrChart4.Diagram = xyDiagram3;
        this.xrChart4.Dpi = 254F;
        this.xrChart4.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart4.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrChart4.Name = "xrChart4";
        series5.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel7.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series5.Label = sideBySideBarSeriesLabel7;
        series5.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series5.Name = "SerieRealizado";
        series5.ValueDataMembersSerializable = "PercentualFisicoRealizado";
        sideBySideBarSeriesView5.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series5.View = sideBySideBarSeriesView5;
        series6.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel8.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series6.Label = sideBySideBarSeriesLabel8;
        series6.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series6.Name = "SeriePrevisto";
        series6.ValueDataMembersSerializable = "PercentualFisicoPrevisto";
        sideBySideBarSeriesView6.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView6.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series6.View = sideBySideBarSeriesView6;
        this.xrChart4.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
        sideBySideBarSeriesLabel9.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart4.SeriesTemplate.Label = sideBySideBarSeriesLabel9;
        this.xrChart4.SizeF = new System.Drawing.SizeF(900F, 250F);
        this.xrChart4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel37
        // 
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(50F, 250F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "Previsto:";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox9
        // 
        this.xrPictureBox9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Projetos.Projetos_Projetos.CorDesempenhoFisico"),
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox9.Dpi = 254F;
        this.xrPictureBox9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 300F);
        this.xrPictureBox9.Name = "xrPictureBox9";
        this.xrPictureBox9.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox9.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrPictureBox8
        // 
        this.xrPictureBox8.Dpi = 254F;
        this.xrPictureBox8.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrPictureBox8.Name = "xrPictureBox8";
        this.xrPictureBox8.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox8.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel36
        // 
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(50F, 350F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = "Desvio:";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel35
        // 
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(50F, 300F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "Realizado:";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel34
        // 
        this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioFisico", "{0:n0} %")});
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(275F, 350F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = "xrLabel9";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel33
        // 
        this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.PercentualFisicoPrevisto", "{0:n0} %")});
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(275F, 250F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = "xrLabel9";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel32
        // 
        this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.PercentualFisicoRealizado", "{0:n0} %")});
        this.xrLabel32.Dpi = 254F;
        this.xrLabel32.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(275F, 300F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.Text = "xrLabel9";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // pnlValoresCusto
        // 
        this.pnlValoresCusto.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel42,
            this.xrPictureBox11,
            this.xrChart5,
            this.xrLabel43,
            this.xrPictureBox10,
            this.xrLabel41,
            this.xrLabel40,
            this.xrLabel39,
            this.xrLabel38});
        this.pnlValoresCusto.Dpi = 254F;
        this.pnlValoresCusto.LocationFloat = new DevExpress.Utils.PointFloat(900.5F, 300F);
        this.pnlValoresCusto.Name = "pnlValoresCusto";
        this.pnlValoresCusto.SizeF = new System.Drawing.SizeF(900.4999F, 400F);
        // 
        // xrLabel42
        // 
        this.xrLabel42.Dpi = 254F;
        this.xrLabel42.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(50F, 350F);
        this.xrLabel42.Name = "xrLabel42";
        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel42.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel42.StylePriority.UseFont = false;
        this.xrLabel42.StylePriority.UseTextAlignment = false;
        this.xrLabel42.Text = "Desvio:";
        this.xrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox11
        // 
        this.xrPictureBox11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Projetos.Projetos_Projetos.CorDesempenhoFinanceiro"),
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoFinanceiro", "~/imagens/{0}.gif")});
        this.xrPictureBox11.Dpi = 254F;
        this.xrPictureBox11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 300F);
        this.xrPictureBox11.Name = "xrPictureBox11";
        this.xrPictureBox11.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox11.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrChart5
        // 
        this.xrChart5.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart5.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart5.DataSource = this.ds;
        xyDiagram4.AxisX.Label.Visible = false;
        xyDiagram4.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram4.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram4.AxisX.Title.Text = "Custo (R$)";
        xyDiagram4.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram4.AxisY.Label.Angle = 15;
        xyDiagram4.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram4.AxisY.Label.TextPattern = "{V:N0} K";
        xyDiagram4.AxisY.MinorCount = 1;
        xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram4.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram4.Rotated = true;
        this.xrChart5.Diagram = xyDiagram4;
        this.xrChart5.Dpi = 254F;
        this.xrChart5.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart5.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart5.LocationFloat = new DevExpress.Utils.PointFloat(5F, 0F);
        this.xrChart5.Name = "xrChart5";
        series7.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel10.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series7.Label = sideBySideBarSeriesLabel10;
        series7.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series7.Name = "SerieRealizado";
        series7.ValueDataMembersSerializable = "ValorCustoRealizado";
        sideBySideBarSeriesView7.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series7.View = sideBySideBarSeriesView7;
        series8.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel11.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series8.Label = sideBySideBarSeriesLabel11;
        series8.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series8.Name = "SeriePrevisto";
        series8.ValueDataMembersSerializable = "ValorCustoPrevisto";
        sideBySideBarSeriesView8.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView8.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series8.View = sideBySideBarSeriesView8;
        this.xrChart5.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7,
        series8};
        sideBySideBarSeriesLabel12.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart5.SeriesTemplate.Label = sideBySideBarSeriesLabel12;
        this.xrChart5.SizeF = new System.Drawing.SizeF(891.4999F, 243F);
        this.xrChart5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel43
        // 
        this.xrLabel43.Dpi = 254F;
        this.xrLabel43.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(50F, 250F);
        this.xrLabel43.Name = "xrLabel43";
        this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel43.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel43.StylePriority.UseFont = false;
        this.xrLabel43.StylePriority.UseTextAlignment = false;
        this.xrLabel43.Text = "Previsto:";
        this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox10
        // 
        this.xrPictureBox10.Dpi = 254F;
        this.xrPictureBox10.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrPictureBox10.Name = "xrPictureBox10";
        this.xrPictureBox10.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox10.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel41
        // 
        this.xrLabel41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.ValorCustoPrevisto", "{0:n0}")});
        this.xrLabel41.Dpi = 254F;
        this.xrLabel41.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(275F, 250F);
        this.xrLabel41.Name = "xrLabel41";
        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel41.SizeF = new System.Drawing.SizeF(624.5F, 50F);
        this.xrLabel41.StylePriority.UseFont = false;
        this.xrLabel41.StylePriority.UseTextAlignment = false;
        this.xrLabel41.Text = "xrLabel41";
        this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel41.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel40
        // 
        this.xrLabel40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.ValorCustoRealizado", "{0:n0}")});
        this.xrLabel40.Dpi = 254F;
        this.xrLabel40.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(275F, 300F);
        this.xrLabel40.Name = "xrLabel40";
        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel40.SizeF = new System.Drawing.SizeF(624.5F, 50F);
        this.xrLabel40.StylePriority.UseFont = false;
        this.xrLabel40.StylePriority.UseTextAlignment = false;
        this.xrLabel40.Text = "xrLabel40";
        this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel40.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel39
        // 
        this.xrLabel39.Dpi = 254F;
        this.xrLabel39.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(50F, 300F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(225F, 50F);
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.StylePriority.UseTextAlignment = false;
        this.xrLabel39.Text = "Realizado:";
        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel38
        // 
        this.xrLabel38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioCusto", "{0:n0} %")});
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(275F, 350F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(624.5F, 50F);
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "xrLabel38";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // pnlValoresReceita
        // 
        this.pnlValoresReceita.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel26,
            this.xrLabel27,
            this.xrLabel28,
            this.xrPictureBox6,
            this.xrPictureBox7,
            this.xrLabel29,
            this.xrLabel30,
            this.xrChart1,
            this.xrLabel31});
        this.pnlValoresReceita.Dpi = 254F;
        this.pnlValoresReceita.LocationFloat = new DevExpress.Utils.PointFloat(0F, 725F);
        this.pnlValoresReceita.Name = "pnlValoresReceita";
        this.pnlValoresReceita.SizeF = new System.Drawing.SizeF(900.9999F, 400F);
        this.pnlValoresReceita.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.pnlReceita_BeforePrint);
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.ValorReceitaPrevisto", "{0:n0}")});
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(264.8959F, 250F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(635.604F, 50F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "xrLabel14";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel26.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel27
        // 
        this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.ValorReceitaRealizado", "{0:n0}")});
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(264.8959F, 299.9998F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(635.604F, 49.99994F);
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "xrLabel15";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrLabel27.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblValoresAbsolutos_EvaluateBinding);
        // 
        // xrLabel28
        // 
        this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioReceita", "{0:n0} %")});
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(264.8959F, 350F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(635.604F, 50.00006F);
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "xrLabel9";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox6
        // 
        this.xrPictureBox6.Dpi = 254F;
        this.xrPictureBox6.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox6.LocationFloat = new DevExpress.Utils.PointFloat(0.500061F, 250F);
        this.xrPictureBox6.Name = "xrPictureBox6";
        this.xrPictureBox6.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox6.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrPictureBox7
        // 
        this.xrPictureBox7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "Projetos.CorDesempenhoReceita", "~/imagens/{0}.gif"),
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Projetos.Projetos_Projetos.CorDesempenhoReceita")});
        this.xrPictureBox7.Dpi = 254F;
        this.xrPictureBox7.LocationFloat = new DevExpress.Utils.PointFloat(0.500061F, 299.9998F);
        this.xrPictureBox7.Name = "xrPictureBox7";
        this.xrPictureBox7.SizeF = new System.Drawing.SizeF(50F, 50F);
        this.xrPictureBox7.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel29
        // 
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(50.49994F, 350F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(214.3959F, 50.00006F);
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "Desvio:";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(50.50006F, 250F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(214.3959F, 50F);
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = "Previsto:";
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrChart1
        // 
        this.xrChart1.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart1.DataSource = this.ds;
        xyDiagram5.AxisX.Label.Visible = false;
        xyDiagram5.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram5.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram5.AxisX.Title.Text = "Receita (R$)";
        xyDiagram5.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram5.AxisY.Label.Angle = 15;
        xyDiagram5.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram5.AxisY.Label.TextPattern = "{V:N0} K";
        xyDiagram5.AxisY.MinorCount = 1;
        xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram5.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram5.Rotated = true;
        this.xrChart1.Diagram = xyDiagram5;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(0.499939F, 0F);
        this.xrChart1.Name = "xrChart1";
        series9.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel13.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series9.Label = sideBySideBarSeriesLabel13;
        series9.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series9.Name = "SerieRealizado";
        series9.ValueDataMembersSerializable = "ValorReceitaRealizado";
        sideBySideBarSeriesView9.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series9.View = sideBySideBarSeriesView9;
        series10.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel14.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series10.Label = sideBySideBarSeriesLabel14;
        series10.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series10.Name = "SeriePrevisto";
        series10.ValueDataMembersSerializable = "ValorReceitaPrevisto";
        sideBySideBarSeriesView10.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView10.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series10.View = sideBySideBarSeriesView10;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series9,
        series10};
        sideBySideBarSeriesLabel15.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel15;
        this.xrChart1.SizeF = new System.Drawing.SizeF(900F, 250F);
        this.xrChart1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel31
        // 
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(50.50006F, 299.9998F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(214.3959F, 50F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "Realizado:";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseForeColor = false;
        this.xrLabel25.Text = "3. Desempenho";
        // 
        // xrLabel21
        // 
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 125F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseForeColor = false;
        this.xrLabel21.Text = "2. Análise Crítica";
        // 
        // xrSubreport2
        // 
        this.xrSubreport2.Dpi = 254F;
        this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.00003F);
        this.xrSubreport2.Name = "xrSubreport2";
        this.xrSubreport2.ReportSource = new relSubreportProdutos();
        this.xrSubreport2.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrSubreport2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport2_BeforePrint);
        // 
        // xrLabel20
        // 
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseForeColor = false;
        this.xrLabel20.Text = "1. Produtos";
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 100F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // xrLabel19
        // 
        this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.NomeObjeto")});
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.Text = "xrLabel3";
        // 
        // rel_BoletimStatus
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport});
        this.DataMember = "Projetos";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.FilterString = "[IndicaPrograma] Like \'S\'";
        this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(150, 150, 100, 100);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "15.1";
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Event Handlers

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

    private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        if (label.Text.Equals(" %"))
            label.Text = "- %";
    }

    private void lblAnaliseCritica_EvaluateBinding(object sender, BindingEventArgs e)
    {
        XRRichText richText = (XRRichText)sender;
        String value = (e.Value as string) ?? string.Empty;
        if (String.IsNullOrEmpty(value.Trim()))
        {
            richText.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            e.Value = "[Sem análise crítica para o período]";
            lblDataAnalise.Visible = false;
        }
        else
        {
            lblDataAnalise.Visible = true;
        }
    }

    private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRSubreport sub = (XRSubreport)sender;
        int codigoProjeto = Convert.ToInt32(
            sub.Report.GetCurrentColumnValue("CodigoObjeto"));
        ((relSubreportProdutos)sub.ReportSource).CodProjeto.Value = codigoProjeto;
    }

    #endregion

    private void lblValoresAbsolutos_EvaluateBinding(object sender, BindingEventArgs e)
    {
        if (e.Value == null || Convert.IsDBNull(e.Value))
        {
            e.Value = 0;
        }
        else
        {
            decimal valor = Convert.ToDecimal(e.Value);
            e.Value = valor * 1000;
        }
    }

    private void pnlReceita_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPanel panel = (XRPanel)sender;
        decimal valorReceitaPrevisto;
        if (Convert.IsDBNull(panel.Report.GetCurrentColumnValue("ValorReceitaPrevisto")))
            valorReceitaPrevisto = 0;
        else
            valorReceitaPrevisto = Convert.ToDecimal(panel.Report.GetCurrentColumnValue("ValorReceitaPrevisto"));

        decimal valorReceitaRealizado;
        if (Convert.IsDBNull(panel.Report.GetCurrentColumnValue("ValorReceitaRealizado")))
            valorReceitaRealizado = 0;
        else
            valorReceitaRealizado = Convert.ToDecimal(panel.Report.GetCurrentColumnValue("ValorReceitaRealizado"));

        panel.Visible = valorReceitaPrevisto != 0 || valorReceitaRealizado != 0;
        //if (panel.Visible)
        //    panel.Report.Bands[BandKind.Detail].HeightF = 1400;
        //else
        //{

        //}
    }
}
