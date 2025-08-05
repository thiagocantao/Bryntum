using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_ImpressaoFormularios
/// </summary>
public class rel_AcompProcessosReproj1 : DevExpress.XtraReports.UI.XtraReport
{
    #region Fields

    private CalculatedField analiseCriticaCalculado;
    private BottomMarginBand bottomMarginBand1;
    private DetailBand Detail1;
    private DetailBand detailBand1;
    private DetailReportBand DetailReport1;
    private XRLabel dois_pontos1;
    private XRLabel dois_pontos2;
    private XRLabel dois_pontos3;
    DataTable dtDesdobramentoGlobal = null;
    DataTable dtResultadoGlobal = null;
    private XRLabel lblCodigoIndicador;
    private XRLabel lblCodigoMeta;
    private XRLabel lblCodigoProjeto;
    private XRLabel lblDescIndicador;
    private XRLabel lblDescMeta;
    private XRLabel lblDescPeriodicidade;
    private XRLabel lblNomeIndicador;
    private XRLabel lblNomeMeta;
    private XRLabel lblPolaridade;



    private CalculatedField mensagemCalculado;
    private PageFooterBand PageFooter;
    private PageHeaderBand PageHeader;
    private Parameter pCodigoEntidade = new Parameter();
    private Parameter pNomeUnidade = new Parameter();
    private CalculatedField recomendacoesCalculado;
    private ReportFooterBand ReportFooter;
    private ReportHeaderBand ReportHeader;


    private XRShape seta;
    private CalculatedField tituloAnaliseCritica;
    private CalculatedField tituloAnaliseCritica1;
    private TopMarginBand topMarginBand1;
    private XRChart xrChart1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel12;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo3;
    private XRPictureBox imgCabecalhoRelatorio;
    private XRPictureBox imgCabecalhoPagina;
    private XRPictureBox xrPictureBox3;
    private XRPictureBox xrPictureBox4;




    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    //private XRChart graficoBarras = new XRChart();
    private XRChart graficoBarrasEmpilhadas = new XRChart();
    private XRChart graficoPizzaSituacaoAtual = new XRChart();

    private XRLine linha = new XRLine();
    private dsAcompanhamento2 dsAcompanhamento1;
    private XRControlStyle xrControlStyle1;
    public DevExpress.XtraCharts.TextAnnotation textAnnotation1;
    public DevExpress.XtraCharts.ChartAnchorPoint chartAnchorPoint1 = new DevExpress.XtraCharts.ChartAnchorPoint();
    public DevExpress.XtraCharts.FreePosition freePosition1 = new DevExpress.XtraCharts.FreePosition();
    private XRLabel lblDescNomeProjeto;
    private XRLabel lblNomeGerenteProjeto;
    private XRLabel lblDescLiderProjeto;
    private XRLabel lblNomeProjeto;
    private dados cDados = CdadosUtil.GetCdados(null);

    private int codigoUsuarioGlobal = 0, codigoEntidadeGlobal = 0, codigoUnidadeGlobal = 0, anoGlobal = 0;


    private XRTable tblAnaliseCritica;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRPanel xrPanel2;
    private XRLabel lblMensagem;
    private XRPanel xrPanel4;
    Bitmap img;
    private Parameter CodigoProjeto;
    private DetailReportBand DetailReport;
    private DetailBand Detail;
    private XRPanel xrPanel1;
    private XRLabel lblTituloAnaliseCritica;
    private XRRichText xrTxtAnaliseCritica;
    private XRPanel xrPanel3;

    private XRLabel lblAvisoPlanoAcao;
    private XRChart xrChart2;
    Bitmap img1;
    #endregion

    #region Construtores

    public rel_AcompProcessosReproj1(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {
        InitializeComponent();


        pCodigoEntidade.Name = "pCodigoEntidade";
        pNomeUnidade.Name = "pNomeUnidade";

        this.Parameters.AddRange(new Parameter[]
        {
            this.pCodigoEntidade,this.pNomeUnidade
        });

        InitData(codigoUsuario, codigoEntidade, codigoUnidade, ano);
    }

    private void InitData(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {

        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        //xrPictureBox1.Image = logo;
        comandoSql = string.Format(@"DECLARE @RC int
        DECLARE @codigoUsuario int
        DECLARE @codigoEntidade int
        DECLARE @codigoUnidade int
        DECLARE @anoRef int 

        SET @codigoUsuario = {2}
        SET @codigoEntidade = {3}
        SET @codigoUnidade = {4}
        SET @anoRef = {5}        

        EXECUTE @RC = {0}.{1}.p_DadosRelatorioReprojetos 
        @codigoUsuario
        ,@codigoEntidade
        ,@codigoUnidade, @anoRef", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, codigoUnidade, ano);
        codigoUsuarioGlobal = codigoUsuario;
        codigoEntidadeGlobal = codigoEntidade;
        codigoUnidadeGlobal = codigoUnidade;
        anoGlobal = ano;

        DataSet ds = cDados.getDataSet(comandoSql);
        //dsAcompanhamento1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "Projetos1", "Projetos", "Indicadores", "Desdobramento", "Resultado", "ResultadoMeta");
        dsAcompanhamento1.Load(ds.Tables[0].CreateDataReader(), LoadOption.OverwriteChanges, "Projetos1");
        dsAcompanhamento1.Load(ds.Tables[1].CreateDataReader(), LoadOption.OverwriteChanges, "Projetos");
        dsAcompanhamento1.Load(ds.Tables[2].CreateDataReader(), LoadOption.OverwriteChanges, "Indicadores");
        dsAcompanhamento1.Load(ds.Tables[3].CreateDataReader(), LoadOption.OverwriteChanges, "Desdobramento");
        dsAcompanhamento1.Load(ds.Tables[4].CreateDataReader(), LoadOption.OverwriteChanges, "Resultado");
        dtDesdobramentoGlobal = ds.Tables[3];
        dtResultadoGlobal = ds.Tables[4];
        dsAcompanhamento1.Load(ds.Tables[8].CreateDataReader(), LoadOption.OverwriteChanges, "SituacoesPossiveis");
        DefineImagemCapa();
    }

    private void DefineImagemCapa()
    {
        //string resourceFileName = "rel_AcompProcessosReproj.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_AcompProcessosReproj.ResourceManager;
        img = (Bitmap)resources.GetObject("imgCabecalhoRelatorio.Image");
        img1 = (Bitmap)resources.GetObject("imgCabecalhoPagina.Image");

        imgCabecalhoRelatorio.Image = img;
        imgCabecalhoPagina.Image = img1;
    }

    private void InsereCamposDinamicosNaCapa()
    {

        Graphics g = Graphics.FromImage(img);
        Font drawFontRelProc = new Font("Microsoft Sans Serif", 42);
        Font drawFontMarAbr2013 = new Font("Century Gothic", 34);

        SolidBrush drawBrush = new SolidBrush(Color.Black/*Color.FromArgb(0x1F, 0x49, 0x91)*/);

        SizeF stringSize = g.MeasureString("Relatório de Processos Redesenhados", drawFontRelProc);
        float x = (img.Width - stringSize.Width) / 2;
        float y = img.Height * 0.9f;
        g.DrawString("Relatório de Processos Redesenhados", drawFontRelProc, drawBrush, x, 30);

        stringSize = g.MeasureString(getMesReferencia(), drawFontMarAbr2013);
        x = (img.Width - stringSize.Width) / 2;
        y = img.Height * 0.65f;
        g.DrawString(getMesReferencia(), drawFontMarAbr2013, drawBrush, x, y);
        g.Flush();
        g.Save();


        Graphics g1 = Graphics.FromImage(img1);

        stringSize = g1.MeasureString("Relatório de Processos Redesenhados", drawFontRelProc);
        x = (img.Width - stringSize.Width) / 2;
        y = img.Height * 0.9f;
        g1.DrawString("Relatório de Processos Redesenhados", drawFontRelProc, drawBrush, x, 30);

        stringSize = g1.MeasureString(getMesReferencia(), drawFontMarAbr2013);
        x = (img.Width - stringSize.Width) / 2;
        y = img.Height * 0.65f;
        g1.DrawString(getMesReferencia(), drawFontMarAbr2013, drawBrush, x, y);
        g1.Flush();
        g1.Save();


    }

    #endregion

    #region Methods default

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
        string resourceFileName = "rel_AcompProcessosReproj1.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_AcompProcessosReproj1.ResourceManager;
        DevExpress.XtraPrinting.Shape.ShapeArrow shapeArrow1 = new DevExpress.XtraPrinting.Shape.ShapeArrow();
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView4 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView5 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SimpleDiagram3D simpleDiagram3D1 = new DevExpress.XtraCharts.SimpleDiagram3D();
        DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel1 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
        DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView1 = new DevExpress.XtraCharts.Pie3DSeriesView();
        DevExpress.XtraCharts.Pie3DSeriesLabel pie3DSeriesLabel2 = new DevExpress.XtraCharts.Pie3DSeriesLabel();
        DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView2 = new DevExpress.XtraCharts.Pie3DSeriesView();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDescNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeGerenteProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDescLiderProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dsAcompanhamento1 = new dsAcompanhamento2();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.lblDescIndicador = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeIndicador = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeMeta = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDescPeriodicidade = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDescMeta = new DevExpress.XtraReports.UI.XRLabel();
        this.dois_pontos1 = new DevExpress.XtraReports.UI.XRLabel();
        this.dois_pontos2 = new DevExpress.XtraReports.UI.XRLabel();
        this.dois_pontos3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.lblMensagem = new DevExpress.XtraReports.UI.XRLabel();
        this.tblAnaliseCritica = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.lblCodigoMeta = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCodigoIndicador = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCodigoProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblPolaridade = new DevExpress.XtraReports.UI.XRLabel();
        this.seta = new DevExpress.XtraReports.UI.XRShape();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.analiseCriticaCalculado = new DevExpress.XtraReports.UI.CalculatedField();
        this.recomendacoesCalculado = new DevExpress.XtraReports.UI.CalculatedField();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.imgCabecalhoPagina = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.imgCabecalhoRelatorio = new DevExpress.XtraReports.UI.XRPictureBox();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.tituloAnaliseCritica = new DevExpress.XtraReports.UI.CalculatedField();
        this.mensagemCalculado = new DevExpress.XtraReports.UI.CalculatedField();
        this.tituloAnaliseCritica1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.CodigoProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.lblTituloAnaliseCritica = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTxtAnaliseCritica = new DevExpress.XtraReports.UI.XRRichText();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
        this.lblAvisoPlanoAcao = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tblAnaliseCritica)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTxtAnaliseCritica)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 0F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // detailBand1
        // 
        this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel4,
            this.lblDescNomeProjeto,
            this.lblNomeGerenteProjeto,
            this.lblDescLiderProjeto,
            this.lblNomeProjeto});
        this.detailBand1.Dpi = 254F;
        this.detailBand1.Font = new System.Drawing.Font("Verdana", 9F);
        this.detailBand1.HeightF = 139.2931F;
        this.detailBand1.Name = "detailBand1";
        this.detailBand1.StylePriority.UseFont = false;
        this.detailBand1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.detailBand1_BeforePrint);
        // 
        // xrLabel5
        // 
        this.xrLabel5.CanGrow = false;
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(369.307F, 77.7648F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(14.16F, 34.42F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UsePadding = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = ":";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.CanGrow = false;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(369.1403F, 17.34474F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(14.16187F, 34.42006F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UsePadding = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = ":";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblDescNomeProjeto
        // 
        this.lblDescNomeProjeto.CanGrow = false;
        this.lblDescNomeProjeto.Dpi = 254F;
        this.lblDescNomeProjeto.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.lblDescNomeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(65F, 18.41068F);
        this.lblDescNomeProjeto.Name = "lblDescNomeProjeto";
        this.lblDescNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDescNomeProjeto.SizeF = new System.Drawing.SizeF(297.4893F, 45.0713F);
        this.lblDescNomeProjeto.StylePriority.UseFont = false;
        this.lblDescNomeProjeto.StylePriority.UseTextAlignment = false;
        this.lblDescNomeProjeto.Text = "Processo";
        this.lblDescNomeProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblNomeGerenteProjeto
        // 
        this.lblNomeGerenteProjeto.CanGrow = false;
        this.lblNomeGerenteProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.NomeGerenteProjeto")});
        this.lblNomeGerenteProjeto.Dpi = 254F;
        this.lblNomeGerenteProjeto.Font = new System.Drawing.Font("Verdana", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.lblNomeGerenteProjeto.LocationFloat = new DevExpress.Utils.PointFloat(390.9518F, 75.76479F);
        this.lblNomeGerenteProjeto.Name = "lblNomeGerenteProjeto";
        this.lblNomeGerenteProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNomeGerenteProjeto.SizeF = new System.Drawing.SizeF(1648.049F, 37.55434F);
        this.lblNomeGerenteProjeto.StylePriority.UseFont = false;
        this.lblNomeGerenteProjeto.StylePriority.UseTextAlignment = false;
        this.lblNomeGerenteProjeto.Text = "lblNomeGerenteProjeto";
        this.lblNomeGerenteProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lblNomeGerenteProjeto.TextChanged += new System.EventHandler(this.lblNomeGerenteProjeto_TextChanged);
        // 
        // lblDescLiderProjeto
        // 
        this.lblDescLiderProjeto.CanGrow = false;
        this.lblDescLiderProjeto.Dpi = 254F;
        this.lblDescLiderProjeto.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.lblDescLiderProjeto.LocationFloat = new DevExpress.Utils.PointFloat(65F, 75.76472F);
        this.lblDescLiderProjeto.Name = "lblDescLiderProjeto";
        this.lblDescLiderProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDescLiderProjeto.SizeF = new System.Drawing.SizeF(298.0096F, 38.55431F);
        this.lblDescLiderProjeto.StylePriority.UseFont = false;
        this.lblDescLiderProjeto.StylePriority.UseTextAlignment = false;
        this.lblDescLiderProjeto.Text = "Líder do Processo";
        this.lblDescLiderProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblNomeProjeto
        // 
        this.lblNomeProjeto.CanGrow = false;
        this.lblNomeProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.NomeProjeto")});
        this.lblNomeProjeto.Dpi = 254F;
        this.lblNomeProjeto.Font = new System.Drawing.Font("Verdana", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
        this.lblNomeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(390.9518F, 18.34476F);
        this.lblNomeProjeto.Name = "lblNomeProjeto";
        this.lblNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNomeProjeto.SizeF = new System.Drawing.SizeF(1648.049F, 45.13721F);
        this.lblNomeProjeto.StylePriority.UseFont = false;
        this.lblNomeProjeto.StylePriority.UseTextAlignment = false;
        this.lblNomeProjeto.Text = "lblNomeProjeto";
        this.lblNomeProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lblNomeProjeto.TextChanged += new System.EventHandler(this.lblNomeProjeto_TextChanged);
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 0F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // dsAcompanhamento1
        // 
        this.dsAcompanhamento1.DataSetName = "dsAcompanhamento2";
        this.dsAcompanhamento1.EnforceConstraints = false;
        this.dsAcompanhamento1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPictureBox4,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 161.1735F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportFooter;
        this.PageFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageFooter_BeforePrint);
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrPageInfo2.ForeColor = System.Drawing.Color.DimGray;
        this.xrPageInfo2.Format = "Data de emissão: {0:dd/MM/yyyy HH:mm:ss}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1265.605F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(773.3953F, 33.41979F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseForeColor = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrPictureBox4
        // 
        this.xrPictureBox4.Dpi = 254F;
        this.xrPictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox4.Image")));
        this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(65F, 39.42021F);
        this.xrPictureBox4.Name = "xrPictureBox4";
        this.xrPictureBox4.SizeF = new System.Drawing.SizeF(1974F, 39.9F);
        this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.BorderColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.Format = "| {0} |";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(65F, 102.194F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.Number;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(1974F, 41.89917F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
        this.DetailReport1.DataMember = "Projetos1.Proj_Ind_CodigoProjeto";
        this.DetailReport1.DataSource = this.dsAcompanhamento1;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.ReportPrintOptions.PrintOnEmptyDataSource = false;
        this.DetailReport1.DataSourceRowChanged += new DevExpress.XtraReports.UI.DataSourceRowEventHandler(this.DetailReport1_DataSourceRowChanged);
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4,
            this.xrPanel2,
            this.lblCodigoMeta,
            this.lblCodigoIndicador,
            this.lblCodigoProjeto,
            this.lblPolaridade,
            this.seta,
            this.xrChart1});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 809.3846F;
        this.Detail1.Name = "Detail1";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDescIndicador,
            this.lblNomeIndicador,
            this.lblNomeMeta,
            this.xrLabel3,
            this.lblDescPeriodicidade,
            this.lblDescMeta,
            this.dois_pontos1,
            this.dois_pontos2,
            this.dois_pontos3});
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.LocationFloat = new DevExpress.Utils.PointFloat(65F, 0F);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.SizeF = new System.Drawing.SizeF(1972.681F, 139.4632F);
        this.xrPanel4.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // lblDescIndicador
        // 
        this.lblDescIndicador.CanGrow = false;
        this.lblDescIndicador.Dpi = 254F;
        this.lblDescIndicador.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblDescIndicador.LocationFloat = new DevExpress.Utils.PointFloat(7.629395E-06F, 11.14224F);
        this.lblDescIndicador.Name = "lblDescIndicador";
        this.lblDescIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDescIndicador.SizeF = new System.Drawing.SizeF(297.4893F, 39.97F);
        this.lblDescIndicador.StylePriority.UseFont = false;
        this.lblDescIndicador.StylePriority.UseTextAlignment = false;
        this.lblDescIndicador.Text = "Indicador";
        this.lblDescIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblNomeIndicador
        // 
        this.lblNomeIndicador.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.NomeIndicador")});
        this.lblNomeIndicador.Dpi = 254F;
        this.lblNomeIndicador.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblNomeIndicador.LocationFloat = new DevExpress.Utils.PointFloat(325.9518F, 11.14223F);
        this.lblNomeIndicador.Name = "lblNomeIndicador";
        this.lblNomeIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNomeIndicador.SizeF = new System.Drawing.SizeF(1646.729F, 37.97001F);
        this.lblNomeIndicador.StylePriority.UseFont = false;
        this.lblNomeIndicador.StylePriority.UseTextAlignment = false;
        this.lblNomeIndicador.Text = "lblNomeIndicador";
        this.lblNomeIndicador.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblNomeMeta
        // 
        this.lblNomeMeta.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.NomeMeta")});
        this.lblNomeMeta.Dpi = 254F;
        this.lblNomeMeta.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblNomeMeta.LocationFloat = new DevExpress.Utils.PointFloat(325.9518F, 51.32138F);
        this.lblNomeMeta.Name = "lblNomeMeta";
        this.lblNomeMeta.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNomeMeta.SizeF = new System.Drawing.SizeF(1646.729F, 39.79501F);
        this.lblNomeMeta.StylePriority.UseFont = false;
        this.lblNomeMeta.StylePriority.UseTextAlignment = false;
        this.lblNomeMeta.Text = "lblNomeMeta";
        this.lblNomeMeta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.DescricaoPeriodicidade")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(325.9518F, 93.14137F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1646.729F, 38.05582F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // lblDescPeriodicidade
        // 
        this.lblDescPeriodicidade.CanGrow = false;
        this.lblDescPeriodicidade.Dpi = 254F;
        this.lblDescPeriodicidade.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblDescPeriodicidade.LocationFloat = new DevExpress.Utils.PointFloat(7.629395E-06F, 91.11645F);
        this.lblDescPeriodicidade.Name = "lblDescPeriodicidade";
        this.lblDescPeriodicidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDescPeriodicidade.SizeF = new System.Drawing.SizeF(297.4893F, 42.08077F);
        this.lblDescPeriodicidade.StylePriority.UseFont = false;
        this.lblDescPeriodicidade.StylePriority.UseTextAlignment = false;
        this.lblDescPeriodicidade.Text = "Periodicidade";
        this.lblDescPeriodicidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblDescMeta
        // 
        this.lblDescMeta.CanGrow = false;
        this.lblDescMeta.Dpi = 254F;
        this.lblDescMeta.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblDescMeta.LocationFloat = new DevExpress.Utils.PointFloat(7.629395E-06F, 51.32132F);
        this.lblDescMeta.Name = "lblDescMeta";
        this.lblDescMeta.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDescMeta.SizeF = new System.Drawing.SizeF(297.4893F, 38.61587F);
        this.lblDescMeta.StylePriority.UseFont = false;
        this.lblDescMeta.StylePriority.UseTextAlignment = false;
        this.lblDescMeta.Text = "Meta";
        this.lblDescMeta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // dois_pontos1
        // 
        this.dois_pontos1.CanGrow = false;
        this.dois_pontos1.Dpi = 254F;
        this.dois_pontos1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.dois_pontos1.LocationFloat = new DevExpress.Utils.PointFloat(304.307F, 11.14224F);
        this.dois_pontos1.Name = "dois_pontos1";
        this.dois_pontos1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.dois_pontos1.SizeF = new System.Drawing.SizeF(14.16F, 34.42F);
        this.dois_pontos1.StylePriority.UseFont = false;
        this.dois_pontos1.StylePriority.UsePadding = false;
        this.dois_pontos1.StylePriority.UseTextAlignment = false;
        this.dois_pontos1.Text = ":";
        this.dois_pontos1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // dois_pontos2
        // 
        this.dois_pontos2.CanGrow = false;
        this.dois_pontos2.Dpi = 254F;
        this.dois_pontos2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.dois_pontos2.LocationFloat = new DevExpress.Utils.PointFloat(304.307F, 53.5172F);
        this.dois_pontos2.Name = "dois_pontos2";
        this.dois_pontos2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.dois_pontos2.SizeF = new System.Drawing.SizeF(14.16F, 34.42F);
        this.dois_pontos2.StylePriority.UseFont = false;
        this.dois_pontos2.StylePriority.UsePadding = false;
        this.dois_pontos2.StylePriority.UseTextAlignment = false;
        this.dois_pontos2.Text = ":";
        this.dois_pontos2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // dois_pontos3
        // 
        this.dois_pontos3.CanGrow = false;
        this.dois_pontos3.Dpi = 254F;
        this.dois_pontos3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.dois_pontos3.LocationFloat = new DevExpress.Utils.PointFloat(304.307F, 93.93721F);
        this.dois_pontos3.Name = "dois_pontos3";
        this.dois_pontos3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.dois_pontos3.SizeF = new System.Drawing.SizeF(14.16F, 34.42F);
        this.dois_pontos3.StylePriority.UseFont = false;
        this.dois_pontos3.StylePriority.UsePadding = false;
        this.dois_pontos3.StylePriority.UseTextAlignment = false;
        this.dois_pontos3.Text = ":";
        this.dois_pontos3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblMensagem,
            this.tblAnaliseCritica});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(65F, 642.697F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(1974F, 166.6876F);
        this.xrPanel2.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // lblMensagem
        // 
        this.lblMensagem.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.Mensagem")});
        this.lblMensagem.Dpi = 254F;
        this.lblMensagem.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblMensagem.Name = "lblMensagem";
        this.lblMensagem.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.lblMensagem.SizeF = new System.Drawing.SizeF(1974F, 79.58649F);
        this.lblMensagem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // tblAnaliseCritica
        // 
        this.tblAnaliseCritica.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.tblAnaliseCritica.Dpi = 254F;
        this.tblAnaliseCritica.Font = new System.Drawing.Font("Verdana", 6F);
        this.tblAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0F, 87.29169F);
        this.tblAnaliseCritica.Name = "tblAnaliseCritica";
        this.tblAnaliseCritica.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.tblAnaliseCritica.SizeF = new System.Drawing.SizeF(1974F, 66.89581F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Análise Crítica do indicador:";
        this.xrTableCell1.Weight = 0.499350887975368D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.AnaliseCritica")});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Weight = 2.60964621644919D;
        // 
        // lblCodigoMeta
        // 
        this.lblCodigoMeta.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.CodigoMetaOperacional")});
        this.lblCodigoMeta.Dpi = 254F;
        this.lblCodigoMeta.LocationFloat = new DevExpress.Utils.PointFloat(1900.658F, 394.7626F);
        this.lblCodigoMeta.Name = "lblCodigoMeta";
        this.lblCodigoMeta.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.lblCodigoMeta.SizeF = new System.Drawing.SizeF(137.0236F, 62.53311F);
        this.lblCodigoMeta.StylePriority.UsePadding = false;
        this.lblCodigoMeta.Text = "lblCodigoMeta";
        this.lblCodigoMeta.Visible = false;
        // 
        // lblCodigoIndicador
        // 
        this.lblCodigoIndicador.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.CodigoIndicador")});
        this.lblCodigoIndicador.Dpi = 254F;
        this.lblCodigoIndicador.LocationFloat = new DevExpress.Utils.PointFloat(1900.658F, 344.7843F);
        this.lblCodigoIndicador.Name = "lblCodigoIndicador";
        this.lblCodigoIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblCodigoIndicador.SizeF = new System.Drawing.SizeF(137.0236F, 49.97824F);
        this.lblCodigoIndicador.Text = "lblCodigoIndicador";
        this.lblCodigoIndicador.Visible = false;
        // 
        // lblCodigoProjeto
        // 
        this.lblCodigoProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.CodigoProjeto")});
        this.lblCodigoProjeto.Dpi = 254F;
        this.lblCodigoProjeto.LocationFloat = new DevExpress.Utils.PointFloat(1900.658F, 294.6208F);
        this.lblCodigoProjeto.Name = "lblCodigoProjeto";
        this.lblCodigoProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.lblCodigoProjeto.SizeF = new System.Drawing.SizeF(138.3424F, 50.16348F);
        this.lblCodigoProjeto.StylePriority.UsePadding = false;
        this.lblCodigoProjeto.Text = "lblCodigoProjeto";
        this.lblCodigoProjeto.Visible = false;
        // 
        // lblPolaridade
        // 
        this.lblPolaridade.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.Proj_Ind_CodigoProjeto.Polaridade")});
        this.lblPolaridade.Dpi = 254F;
        this.lblPolaridade.LocationFloat = new DevExpress.Utils.PointFloat(1900.658F, 237.3964F);
        this.lblPolaridade.Name = "lblPolaridade";
        this.lblPolaridade.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.lblPolaridade.SizeF = new System.Drawing.SizeF(138.3429F, 57.22452F);
        this.lblPolaridade.StylePriority.UsePadding = false;
        this.lblPolaridade.Text = "lblPolaridade";
        this.lblPolaridade.Visible = false;
        this.lblPolaridade.TextChanged += new System.EventHandler(this.lblPolaridade_TextChanged);
        // 
        // seta
        // 
        this.seta.Dpi = 254F;
        this.seta.FillColor = System.Drawing.Color.Black;
        this.seta.LocationFloat = new DevExpress.Utils.PointFloat(1959.625F, 157.0797F);
        this.seta.Name = "seta";
        shapeArrow1.ArrowWidth = 30;
        this.seta.Shape = shapeArrow1;
        this.seta.SizeF = new System.Drawing.SizeF(79.375F, 58.41995F);
        // 
        // xrChart1
        // 
        this.xrChart1.AppearanceNameSerializable = "Gray";
        this.xrChart1.BorderColor = System.Drawing.Color.Silver;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "Projetos1.Proj_Ind_CodigoProjeto";
        this.xrChart1.DataSource = this.dsAcompanhamento1;
        xyDiagram1.AxisX.Label.Angle = -60;
        xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Verdana", 6.5F);
        xyDiagram1.AxisX.MinorCount = 1;
        xyDiagram1.AxisX.ScaleBreakOptions.SizeInPixels = 5;
        xyDiagram1.AxisX.Tickmarks.CrossAxis = true;
        xyDiagram1.AxisX.Tickmarks.Length = 3;
        xyDiagram1.AxisX.Tickmarks.MinorLength = 5;
        xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Verdana", 5F);
        xyDiagram1.AxisX.Title.Text = "Período";
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.GridLines.Visible = false;
        xyDiagram1.AxisY.Label.Font = new System.Drawing.Font("Verdana", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Verdana", 5F);
        xyDiagram1.AxisY.Title.Text = "Quantidade";
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.DefaultPane.BackColor = System.Drawing.Color.White;
        xyDiagram1.DefaultPane.BorderVisible = false;
        xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.DefaultPane.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        xyDiagram1.Margins.Bottom = 0;
        xyDiagram1.Margins.Left = 0;
        xyDiagram1.Margins.Right = 0;
        xyDiagram1.Margins.Top = 0;
        this.xrChart1.Diagram = xyDiagram1;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.EmptyChartText.Text = "Indicador com meta pendente de desdobramento";
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart1.Legend.EnableAntialiasing = DefaultBoolean.True;
        this.xrChart1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
        this.xrChart1.Legend.Font = new System.Drawing.Font("Verdana", 5F);
        this.xrChart1.Legend.HorizontalIndent = 40;
        this.xrChart1.Legend.MarkerSize = new System.Drawing.Size(15, 10);
        this.xrChart1.Legend.TextOffset = 0;
        this.xrChart1.Legend.VerticalIndent = 1;
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(65F, 157.0797F);
        this.xrChart1.Name = "xrChart1";
        this.xrChart1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
        this.xrChart1.PaletteBaseColorNumber = 5;
        this.xrChart1.PaletteName = "Office";
        series1.ArgumentDataMember = "Indicadores_Resultado.DataReferencia";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel1.EnableAntialiasing = DefaultBoolean.True;
        sideBySideBarSeriesLabel1.Font = new System.Drawing.Font("Verdana", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        sideBySideBarSeriesLabel1.ShowForZeroValues = true;
        sideBySideBarSeriesLabel1.TextColor = System.Drawing.Color.Black;
        series1.Label = sideBySideBarSeriesLabel1;
        series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.Name = "Atingiu a meta";
        series1.ValueDataMembersSerializable = "Indicadores_Resultado.Valor";
        sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
        series1.View = sideBySideBarSeriesView1;
        series2.ArgumentDataMember = "Indicadores_Resultado.DataReferencia";
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel2.Font = new System.Drawing.Font("Verdana", 5F);
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        sideBySideBarSeriesLabel2.ShowForZeroValues = true;
        series2.Label = sideBySideBarSeriesLabel2;
        series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.Name = "Próximo à meta";
        series2.ValueDataMembersSerializable = "Indicadores_Resultado.Valor";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.Yellow;
        series2.View = sideBySideBarSeriesView2;
        series3.ArgumentDataMember = "Indicadores_Resultado.DataReferencia";
        series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel3.Font = new System.Drawing.Font("Verdana", 5F);
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        sideBySideBarSeriesLabel3.ShowForZeroValues = true;
        series3.Label = sideBySideBarSeriesLabel3;
        series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.Name = "Meta não atingida";
        series3.ValueDataMembersSerializable = "Indicadores_Resultado.Valor";
        sideBySideBarSeriesView3.Color = System.Drawing.Color.Red;
        series3.View = sideBySideBarSeriesView3;
        series4.ArgumentDataMember = "Indicadores_Resultado.DataReferencia";
        series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        sideBySideBarSeriesLabel4.ShowForZeroValues = true;
        series4.Label = sideBySideBarSeriesLabel4;
        series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series4.Name = "Acima da meta";
        series4.ValueDataMembersSerializable = "Indicadores_Resultado.Valor";
        sideBySideBarSeriesView4.Color = System.Drawing.Color.Blue;
        series4.View = sideBySideBarSeriesView4;
        series5.ArgumentDataMember = "Indicadores_Resultado.DataReferencia";
        series5.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series5.Label = sideBySideBarSeriesLabel5;
        series5.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series5.Name = "Meta não definida";
        series5.ValueDataMembersSerializable = "Indicadores_Resultado.Valor";
        sideBySideBarSeriesView5.Border.Color = System.Drawing.Color.Black;
        sideBySideBarSeriesView5.Color = System.Drawing.Color.WhiteSmoke;
        sideBySideBarSeriesView5.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series5.View = sideBySideBarSeriesView5;
        series6.ArgumentDataMember = "Indicadores_Desdobramento.DataReferencia";
        series6.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series6.DataFiltersConjunctionMode = DevExpress.XtraCharts.ConjunctionTypes.Or;
        pointSeriesLabel1.Font = new System.Drawing.Font("Verdana", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        pointSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        series6.Label = pointSeriesLabel1;
        series6.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series6.Name = "Meta";
        series6.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending;
        series6.ValueDataMembersSerializable = "Indicadores_Desdobramento.Valor";
        lineSeriesView1.Color = System.Drawing.Color.Black;
        lineSeriesView1.LineMarkerOptions.Size = 5;
        series6.View = lineSeriesView1;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4,
        series5,
        series6};
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
        this.xrChart1.SizeF = new System.Drawing.SizeF(1819.876F, 470.9299F);
        this.xrChart1.StylePriority.UseBorderColor = false;
        this.xrChart1.StylePriority.UseBorders = false;
        this.xrChart1.StylePriority.UsePadding = false;
        this.xrChart1.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.xrChart1_CustomDrawAxisLabel);
        this.xrChart1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChart1_BeforePrint);
        // 
        // analiseCriticaCalculado
        // 
        this.analiseCriticaCalculado.DataMember = "Projetos.Proj_Ind_CodigoProjeto";
        this.analiseCriticaCalculado.DisplayName = "analiseCriticaCalculado";
        this.analiseCriticaCalculado.Expression = "Iif(IsNullOrEmpty([AnaliseCritica]) == true,\'Sem análise crítica para o período\'," +
" [AnaliseCritica])";
        this.analiseCriticaCalculado.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.analiseCriticaCalculado.Name = "analiseCriticaCalculado";
        // 
        // recomendacoesCalculado
        // 
        this.recomendacoesCalculado.DataMember = "Projetos.Proj_Ind_CodigoProjeto";
        this.recomendacoesCalculado.Expression = "Iif(IsNullOrEmpty([Recomendacoes]) == true,\'Sem recomendações para o periodo\',[Re" +
"comendacoes])";
        this.recomendacoesCalculado.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.recomendacoesCalculado.Name = "recomendacoesCalculado";
        // 
        // PageHeader
        // 
        this.PageHeader.BackColor = System.Drawing.Color.Transparent;
        this.PageHeader.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgCabecalhoPagina});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 389.2674F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
        this.PageHeader.StylePriority.UseBackColor = false;
        this.PageHeader.StylePriority.UseBorders = false;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // imgCabecalhoPagina
        // 
        this.imgCabecalhoPagina.Dpi = 254F;
        this.imgCabecalhoPagina.Image = ((System.Drawing.Image)(resources.GetObject("imgCabecalhoPagina.Image")));
        this.imgCabecalhoPagina.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5.791717F);
        this.imgCabecalhoPagina.Name = "imgCabecalhoPagina";
        this.imgCabecalhoPagina.SizeF = new System.Drawing.SizeF(2100F, 383.4757F);
        this.imgCabecalhoPagina.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel1
        // 
        this.xrLabel1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel1.CanGrow = false;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel1.ForeColor = System.Drawing.Color.Black;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(65F, 409.0175F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1972.681F, 52.94589F);
        this.xrLabel1.StylePriority.UseBorderDashStyle = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "3.  ACOMPANHAMENTO DA IMPLANTAÇÃO DOS PROCESSOS";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.imgCabecalhoRelatorio});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 486.9634F;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // imgCabecalhoRelatorio
        // 
        this.imgCabecalhoRelatorio.Dpi = 254F;
        this.imgCabecalhoRelatorio.Image = ((System.Drawing.Image)(resources.GetObject("imgCabecalhoRelatorio.Image")));
        this.imgCabecalhoRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.imgCabecalhoRelatorio.Name = "imgCabecalhoRelatorio";
        this.imgCabecalhoRelatorio.SizeF = new System.Drawing.SizeF(2100F, 383.4757F);
        this.imgCabecalhoRelatorio.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo3,
            this.xrLabel12,
            this.xrPictureBox3});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 257.6476F;
        this.ReportFooter.Name = "ReportFooter";
        this.ReportFooter.PrintAtBottom = true;
        this.ReportFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportFooter_BeforePrint);
        // 
        // xrPageInfo3
        // 
        this.xrPageInfo3.Dpi = 254F;
        this.xrPageInfo3.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrPageInfo3.ForeColor = System.Drawing.Color.DimGray;
        this.xrPageInfo3.Format = "Data de emissão: {0:dd/MM/yyyy HH:mm:ss}";
        this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(1232.104F, 0F);
        this.xrPageInfo3.Name = "xrPageInfo3";
        this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo3.SizeF = new System.Drawing.SizeF(806.8961F, 33.41979F);
        this.xrPageInfo3.StylePriority.UseFont = false;
        this.xrPageInfo3.StylePriority.UseForeColor = false;
        this.xrPageInfo3.StylePriority.UseTextAlignment = false;
        this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrLabel12
        // 
        this.xrLabel12.CanGrow = false;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Cambria", 10F);
        this.xrLabel12.ForeColor = System.Drawing.Color.DimGray;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(65F, 80.87652F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(1972.681F, 168.1458F);
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.Dpi = 254F;
        this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(65F, 37.99989F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(1974F, 39.89916F);
        this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // tituloAnaliseCritica
        // 
        this.tituloAnaliseCritica.DataMember = "Projetos";
        this.tituloAnaliseCritica.Expression = "Iif(IsNullOrEmpty(Trim([AnaliseCriticaProjeto])), \'\' ,\'Análise Crítica:\' )";
        this.tituloAnaliseCritica.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.tituloAnaliseCritica.Name = "tituloAnaliseCritica";
        // 
        // mensagemCalculado
        // 
        this.mensagemCalculado.DataMember = "Projetos.Proj_Ind_CodigoProjeto.Indicadores_Desdobramento";
        this.mensagemCalculado.Expression = "[NomeIndicador]";
        this.mensagemCalculado.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.mensagemCalculado.Name = "mensagemCalculado";
        // 
        // tituloAnaliseCritica1
        // 
        this.tituloAnaliseCritica1.DataMember = "Projetos1";
        this.tituloAnaliseCritica1.Expression = "Iif(IsNullOrEmpty([AnaliseCriticaProjeto]),\'\',\'Análise do projeto de implantação " +
"do processo\' )";
        this.tituloAnaliseCritica1.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.tituloAnaliseCritica1.Name = "tituloAnaliseCritica1";
        // 
        // CodigoProjeto
        // 
        this.CodigoProjeto.Description = "Parameter1";
        this.CodigoProjeto.Name = "CodigoProjeto";
        this.CodigoProjeto.Type = typeof(int);
        this.CodigoProjeto.ValueInfo = "0";
        this.CodigoProjeto.Visible = false;
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.DetailReport.DataMember = "Projetos1";
        this.DetailReport.DataSource = this.dsAcompanhamento1;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.FilterString = "[CodigoProjeto] = ?CodigoProjeto";
        this.DetailReport.Level = 1;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,
            this.xrPanel3});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 767.4344F;
        this.Detail.Name = "Detail";
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter2_BeforePrint);
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTituloAnaliseCritica,
            this.xrTxtAnaliseCritica});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(65F, 633.5697F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1974F, 133.8647F);
        this.xrPanel1.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // lblTituloAnaliseCritica
        // 
        this.lblTituloAnaliseCritica.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos1.tituloAnaliseCritica1")});
        this.lblTituloAnaliseCritica.Dpi = 254F;
        this.lblTituloAnaliseCritica.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.lblTituloAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(3.000008F, 6.999939F);
        this.lblTituloAnaliseCritica.Name = "lblTituloAnaliseCritica";
        this.lblTituloAnaliseCritica.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTituloAnaliseCritica.SizeF = new System.Drawing.SizeF(1955.271F, 58.41992F);
        this.lblTituloAnaliseCritica.StylePriority.UseFont = false;
        this.lblTituloAnaliseCritica.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTxtAnaliseCritica
        // 
        this.xrTxtAnaliseCritica.BackColor = System.Drawing.Color.Transparent;
        this.xrTxtAnaliseCritica.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTxtAnaliseCritica.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projetos1.AnaliseCriticaProjeto")});
        this.xrTxtAnaliseCritica.Dpi = 254F;
        this.xrTxtAnaliseCritica.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTxtAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(3.000008F, 72.0658F);
        this.xrTxtAnaliseCritica.Name = "xrTxtAnaliseCritica";
        this.xrTxtAnaliseCritica.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrTxtAnaliseCritica.SerializableRtfString = resources.GetString("xrTxtAnaliseCritica.SerializableRtfString");
        this.xrTxtAnaliseCritica.SizeF = new System.Drawing.SizeF(1955.271F, 57.03998F);
        this.xrTxtAnaliseCritica.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTxtAnaliseCritica_BeforePrint);
        // 
        // xrPanel3
        // 
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart2,
            this.lblAvisoPlanoAcao});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(65F, 0F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(1975.501F, 632.3542F);
        this.xrPanel3.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // xrChart2
        // 
        this.xrChart2.BorderColor = System.Drawing.Color.Black;
        this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart2.DataMember = "Projetos1.Projetos1_SituacoesPossiveis";
        simpleDiagram3D1.Dimension = 1;
        simpleDiagram3D1.HorizontalScrollPercent = -0.27027027027027D;
        simpleDiagram3D1.LabelsResolveOverlappingMinIndent = 3;
        simpleDiagram3D1.RotationMatrixSerializable = "-0.519484374356956;0.712891184531907;-0.47108613205619;0;-0.853852853598244;-0.45" +
"4204728050663;0.254230937178323;0;-0.0327305545493074;0.534307237489;0.844656431" +
"198972;0;0;0;0;1";
        simpleDiagram3D1.VerticalScrollPercent = 1.05820105820106D;
        this.xrChart2.Diagram = simpleDiagram3D1;
        this.xrChart2.Dpi = 254F;
        this.xrChart2.EmptyChartText.Font = new System.Drawing.Font("Verdana", 12F);
        this.xrChart2.EmptyChartText.Text = "Sem dados de situação atual do plano de ação";
        this.xrChart2.EmptyChartText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.xrChart2.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart2.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart2.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart2.Legend.EnableAntialiasing = DefaultBoolean.True;
        this.xrChart2.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
        this.xrChart2.Legend.HorizontalIndent = 40;
        this.xrChart2.Legend.Margins.Bottom = 4;
        this.xrChart2.Legend.Margins.Left = 0;
        this.xrChart2.Legend.Margins.Right = 0;
        this.xrChart2.Legend.Margins.Top = 0;
        this.xrChart2.Legend.MarkerSize = new System.Drawing.Size(10, 10);
        this.xrChart2.Legend.Shadow.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.xrChart2.Legend.Shadow.Size = 3;
        this.xrChart2.Legend.VerticalIndent = 3;
        this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 65.41984F);
        this.xrChart2.Name = "xrChart2";
        this.xrChart2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
        series7.ArgumentDataMember = "Situacao";
        series7.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pie3DSeriesLabel1.EnableAntialiasing = DefaultBoolean.True;
        pie3DSeriesLabel1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        pie3DSeriesLabel1.Font = new System.Drawing.Font("Verdana", 8F);
        pie3DSeriesLabel1.LineLength = 4;
        pie3DSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        pie3DSeriesLabel1.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.Default;
        pie3DSeriesLabel1.TextColor = System.Drawing.Color.Black;
        pie3DSeriesLabel1.TextPattern = "{VP:P1}";
        series7.Label = pie3DSeriesLabel1;
        series7.LegendText = "Situação Atual";
        series7.LegendTextPattern = "{A}";
        series7.Name = "Serie";
        series7.TopNOptions.Count = 10;
        series7.TopNOptions.ShowOthers = false;
        series7.ValueDataMembersSerializable = "QtdSituacao";
        pie3DSeriesView1.ExplodedDistancePercentage = 1D;
        pie3DSeriesView1.PieFillStyle.FillMode = DevExpress.XtraCharts.FillMode3D.Solid;
        series7.View = pie3DSeriesView1;
        this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7};
        pie3DSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart2.SeriesTemplate.Label = pie3DSeriesLabel2;
        this.xrChart2.SeriesTemplate.View = pie3DSeriesView2;
        this.xrChart2.SizeF = new System.Drawing.SizeF(1956.771F, 562.3751F);
        this.xrChart2.SmallChartText.EnableAntialiasing = DefaultBoolean.False;
        this.xrChart2.StylePriority.UseBorders = false;
        this.xrChart2.StylePriority.UsePadding = false;
        this.xrChart2.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart2_CustomDrawSeriesPoint);
        // 
        // lblAvisoPlanoAcao
        // 
        this.lblAvisoPlanoAcao.Dpi = 254F;
        this.lblAvisoPlanoAcao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblAvisoPlanoAcao.Name = "lblAvisoPlanoAcao";
        this.lblAvisoPlanoAcao.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.lblAvisoPlanoAcao.SizeF = new System.Drawing.SizeF(1956.771F, 58.42004F);
        this.lblAvisoPlanoAcao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // rel_AcompProcessosReproj1
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.ReportHeader,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport1,
            this.ReportFooter,
            this.DetailReport});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.analiseCriticaCalculado,
            this.recomendacoesCalculado,
            this.tituloAnaliseCritica,
            this.mensagemCalculado,
            this.tituloAnaliseCritica1});
        this.DataMember = "Projetos1";
        this.DataSource = this.dsAcompanhamento1;
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CodigoProjeto});
        this.ReportPrintOptions.DetailCountOnEmptyDataSource = 0;
        this.ReportPrintOptions.PrintOnEmptyDataSource = false;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "15.1";
        this.Watermark.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tblAnaliseCritica)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTxtAnaliseCritica)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Métodos de componentes Incluídos pelo usuário

    private string getMesReferencia()
    {
        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1 || mes == 2)
        {
            mesporextenso = "Jan/Fev";
        }
        else if (mes == 3 || mes == 4)
        {
            mesporextenso = "Mar/Abr";
        }
        else if (mes == 5 || mes == 6)
        {
            mesporextenso = "Mai/Jun";
        }
        else if (mes == 7 || mes == 8)
        {
            mesporextenso = "Jul/Ago";
        }
        else if (mes == 9 || mes == 10)
        {
            mesporextenso = "Set/Out";
        }
        else if (mes == 11 || mes == 12)
        {
            mesporextenso = "Nov/Dez";
        }

        mesporextenso += " - " + ano;

        return mesporextenso;

    }

    private void lblNomeGerenteProjeto_TextChanged(object sender, EventArgs e)
    {
        int outInt = 0;
        if (int.TryParse(lblNomeGerenteProjeto.Text, out outInt) == true)
        {
            DataSet ds = cDados.getProjetos(" and P.CodigoProjeto = " + lblNomeGerenteProjeto.Text);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                lblNomeGerenteProjeto.Text = ds.Tables[0].Rows[0]["Gerente"].ToString();
            }
        }
    }

    private void lblNomeProjeto_TextChanged(object sender, EventArgs e)
    {
        int outInt = 0;
        if (int.TryParse(lblNomeProjeto.Text, out outInt) == true)
        {
            DataSet ds = cDados.getProjetos(" and P.CodigoProjeto = " + lblNomeProjeto.Text);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                lblNomeProjeto.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
            }
        }
    }

    private void lblPolaridade_TextChanged(object sender, EventArgs e)
    {
        seta.Angle = lblPolaridade.Text == "POS" ? 0 : 180;
    }

    private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        /*DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1)
        {
            mesporextenso = "Janeiro";
        }
        else if (mes == 2)
        {
            mesporextenso = "Fevereiro";
        }
        else if (mes == 3)
        {
            mesporextenso = "Março";
        }
        else if (mes == 4)
        {
            mesporextenso = "Abril";
        }
        else if (mes == 5)
        {
            mesporextenso = "Maio";
        }
        else if (mes == 6)
        {
            mesporextenso = "Junho";
        }
        else if (mes == 7)
        {
            mesporextenso = "Julho";
        }
        else if (mes == 8)
        {
            mesporextenso = "Agosto";
        }
        else if (mes == 9)
        {
            mesporextenso = "Setembro";
        }
        else if (mes == 10)
        {
            mesporextenso = "Outubro";
        }
        else if (mes == 11)
        {
            mesporextenso = "Novembro";
        }
        else if (mes == 12)
        {
            mesporextenso = "Dezembro";
        }*/

        //lblPeriodoReferencia.Text = "Período de Referência: " + mesporextenso + "/" + ano.ToString();
    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        InsereCamposDinamicosNaCapa();
    }

    private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataSet dsUnidade = cDados.getUnidadeNegocio(string.Format(" and CodigoUnidadeNegocio = {0}", codigoEntidadeGlobal));
        string nomeUnidadeNegocio = "";
        string siglaUnidadeNegocio = "";
        string textoASubstituir = "";
        if (cDados.DataSetOk(dsUnidade) && cDados.DataTableOk(dsUnidade.Tables[0]))
        {
            nomeUnidadeNegocio = dsUnidade.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
            siglaUnidadeNegocio = dsUnidade.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
        }
        textoASubstituir = string.Format(@"{0}", (nomeUnidadeNegocio.IndexOf(siglaUnidadeNegocio) >= 0 ? nomeUnidadeNegocio : nomeUnidadeNegocio + " - " + siglaUnidadeNegocio));
        xrLabel12.Text = string.Format(@"EXPEDIENTE: Relatório de Processos Redesenhados é uma publicação bimestral da {0}. Gerente Executiva: Eliane Fernandes da Silva. Gerente Executiva Adjunta de Gestão e Desempenho: Cristiana Araújo de Almeida. Núcleo de Processos: Fernanda Wippel (ramal: 8932 e-mail: fpires@cni.org.br); Cristina Barbosa (ramal: 8817 e-mail: cneiva@cni.org.br); Denise Rodrigues (ramal: 9360 e-mail: denise.silva@cni.org.br).", textoASubstituir);

    }

    private void xrChart1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int codigoProjeto = 0;
        int codigoIndicador = 0;
        int codigoMeta = 0;
        if (int.TryParse(lblCodigoProjeto.Text, out codigoProjeto) == true)
        {
            codigoProjeto = int.Parse(lblCodigoProjeto.Text);
        }

        if (int.TryParse(lblCodigoIndicador.Text, out codigoIndicador) == true)
        {
            codigoIndicador = int.Parse(lblCodigoIndicador.Text);
        }

        if (int.TryParse(lblCodigoMeta.Text, out codigoMeta) == true)
        {
            codigoMeta = int.Parse(lblCodigoMeta.Text);
        }

        //filtrar meta (linha que aparece no gráfico)
        DataFilter filtroMetaCodPrj = new DataFilter();
        DataFilter filtroMetaCodInd = new DataFilter();
        DataFilter filtroMetaCodMeta = new DataFilter();

        //Filtro de todas as situações das barras que aparecem no gráfico
        DataFilter filtroAtingiuMeta = new DataFilter();
        DataFilter filtroAcimaDaMeta = new DataFilter();
        DataFilter filtroProximoMeta = new DataFilter();
        DataFilter filtroMetaNaoAtingida = new DataFilter();
        DataFilter filtroMetaNaoDefinida = new DataFilter();

        //Para cada um dos filtros devem ter codigo de INDICADOR
        DataFilter filtroAtingiuMetaCodInd = new DataFilter();
        DataFilter filtroAcimaDaMetaCodInd = new DataFilter();
        DataFilter filtroProximoMetaCodInd = new DataFilter();
        DataFilter filtroMetaNaoAtingidaCodInd = new DataFilter();
        DataFilter filtroMetaNaoDefinidaCodInd = new DataFilter();

        //Para cada um dos filtros devem ter codigo de PROJETO
        DataFilter filtroAtingiuMetaCodPrj = new DataFilter();
        DataFilter filtroAcimaDaMetaCodPrj = new DataFilter();
        DataFilter filtroProximoMetaCodPrj = new DataFilter();
        DataFilter filtroMetaNaoAtingidaCodPrj = new DataFilter();
        DataFilter filtroMetaNaoDefinidaCodPrj = new DataFilter();

        ////Para cada um dos filtros devem ter codigo de META
        DataFilter filtroAtingiuMetaCodMeta = new DataFilter();
        DataFilter filtroAcimaDaMetaCodMeta = new DataFilter();
        DataFilter filtroProximoMetaCodMeta = new DataFilter();
        DataFilter filtroMetaNaoAtingidaCodMeta = new DataFilter();
        DataFilter filtroMetaNaoDefinidaCodMeta = new DataFilter();


        // -- filtro meta não definida vai aparecer barra cinza no grafico (CONFIGURAÇÕES)
        filtroMetaNaoDefinida.ColumnName = "Indicadores_Resultado.CorDesempenho";
        filtroMetaNaoDefinida.DataType = System.Type.GetType("System.String");
        filtroMetaNaoDefinida.Condition = DataFilterCondition.Equal;
        filtroMetaNaoDefinida.Value = "0";

        filtroMetaNaoDefinidaCodPrj.ColumnName = "Indicadores_Resultado.CodigoProjeto";
        filtroMetaNaoDefinidaCodPrj.DataType = System.Type.GetType("System.Int");
        filtroMetaNaoDefinidaCodPrj.Condition = DataFilterCondition.Equal;
        filtroMetaNaoDefinidaCodPrj.Value = codigoProjeto;

        filtroMetaNaoDefinidaCodInd.ColumnName = "Indicadores_Resultado.CodigoIndicador";
        filtroMetaNaoDefinidaCodInd.DataType = System.Type.GetType("System.Int");
        filtroMetaNaoDefinidaCodInd.Condition = DataFilterCondition.Equal;
        filtroMetaNaoDefinidaCodInd.Value = codigoIndicador;

        filtroMetaNaoDefinidaCodMeta.ColumnName = "Indicadores_Resultado.CodigoMeta";
        filtroMetaNaoDefinidaCodMeta.DataType = System.Type.GetType("System.Int");
        filtroMetaNaoDefinidaCodMeta.Condition = DataFilterCondition.Equal;
        filtroMetaNaoDefinidaCodMeta.Value = codigoMeta;
        // -- fim filtro meta não definida vai apaerceer barra cinza no grafico

        //--- filtro acima da meta (CONFIGURAÇÕES)
        filtroAcimaDaMeta.ColumnName = "Indicadores_Resultado.CorDesempenho";
        filtroAcimaDaMeta.DataType = System.Type.GetType("System.String");
        filtroAcimaDaMeta.Condition = DataFilterCondition.Equal;
        filtroAcimaDaMeta.Value = "4";

        filtroAcimaDaMetaCodPrj.ColumnName = "Indicadores_Resultado.CodigoProjeto";
        filtroAcimaDaMetaCodPrj.DataType = System.Type.GetType("System.Int");
        filtroAcimaDaMetaCodPrj.Condition = DataFilterCondition.Equal;
        filtroAcimaDaMetaCodPrj.Value = codigoProjeto;

        filtroAcimaDaMetaCodInd.ColumnName = "Indicadores_Resultado.CodigoIndicador";
        filtroAcimaDaMetaCodInd.DataType = System.Type.GetType("System.Int");
        filtroAcimaDaMetaCodInd.Condition = DataFilterCondition.Equal;
        filtroAcimaDaMetaCodInd.Value = codigoIndicador;

        filtroAcimaDaMetaCodMeta.ColumnName = "Indicadores_Resultado.CodigoMeta";
        filtroAcimaDaMetaCodMeta.DataType = System.Type.GetType("System.Int");
        filtroAcimaDaMetaCodMeta.Condition = DataFilterCondition.Equal;
        filtroAcimaDaMetaCodMeta.Value = codigoMeta;
        // -- fim filtro acima da meta

        // --- filtro proximo meta (CONFIGURAÇÕES)
        filtroProximoMeta.ColumnName = "Indicadores_Resultado.CorDesempenho";
        filtroProximoMeta.DataType = System.Type.GetType("System.String");
        filtroProximoMeta.Condition = DataFilterCondition.Equal;
        filtroProximoMeta.Value = "2";

        filtroProximoMetaCodPrj.ColumnName = "Indicadores_Resultado.CodigoProjeto";
        filtroProximoMetaCodPrj.DataType = System.Type.GetType("System.Int");
        filtroProximoMetaCodPrj.Condition = DataFilterCondition.Equal;
        filtroProximoMetaCodPrj.Value = codigoProjeto;

        filtroProximoMetaCodInd.ColumnName = "Indicadores_Resultado.CodigoIndicador";
        filtroProximoMetaCodInd.DataType = System.Type.GetType("System.Int");
        filtroProximoMetaCodInd.Condition = DataFilterCondition.Equal;
        filtroProximoMetaCodInd.Value = codigoIndicador;

        filtroProximoMetaCodMeta.ColumnName = "Indicadores_Resultado.CodigoMeta";
        filtroProximoMetaCodMeta.DataType = System.Type.GetType("System.Int");
        filtroProximoMetaCodMeta.Condition = DataFilterCondition.Equal;
        filtroProximoMetaCodMeta.Value = codigoMeta;
        // -- fim filtro proximo da meta

        // --- filtro atingiu meta (CONFIGURAÇÕES)
        filtroAtingiuMeta.ColumnName = "Indicadores_Resultado.CorDesempenho";
        filtroAtingiuMeta.DataType = System.Type.GetType("System.String");
        filtroAtingiuMeta.Condition = DataFilterCondition.Equal;
        filtroAtingiuMeta.Value = "3";

        filtroAtingiuMetaCodPrj.ColumnName = "Indicadores_Resultado.CodigoProjeto";
        filtroAtingiuMetaCodPrj.DataType = System.Type.GetType("System.Int");
        filtroAtingiuMetaCodPrj.Condition = DataFilterCondition.Equal;
        filtroAtingiuMetaCodPrj.Value = codigoProjeto;

        filtroAtingiuMetaCodInd.ColumnName = "Indicadores_Resultado.CodigoIndicador";
        filtroAtingiuMetaCodInd.DataType = System.Type.GetType("System.Int");
        filtroAtingiuMetaCodInd.Condition = DataFilterCondition.Equal;
        filtroAtingiuMetaCodInd.Value = codigoIndicador;

        filtroAtingiuMetaCodMeta.ColumnName = "Indicadores_Resultado.CodigoMeta";
        filtroAtingiuMetaCodMeta.DataType = System.Type.GetType("System.Int");
        filtroAtingiuMetaCodMeta.Condition = DataFilterCondition.Equal;
        filtroAtingiuMetaCodMeta.Value = codigoMeta;
        // --- fim filtro atingiu meta


        // -- filtro meta não atingida (CONFIGURAÇÕES)
        filtroMetaNaoAtingida.ColumnName = "Indicadores_Resultado.CorDesempenho";
        filtroMetaNaoAtingida.DataType = System.Type.GetType("System.String");
        filtroMetaNaoAtingida.Condition = DataFilterCondition.Equal;
        filtroMetaNaoAtingida.Value = "1";

        filtroMetaNaoAtingidaCodPrj.ColumnName = "Indicadores_Resultado.CodigoProjeto";
        filtroMetaNaoAtingidaCodPrj.DataType = System.Type.GetType("System.Int");
        filtroMetaNaoAtingidaCodPrj.Condition = DataFilterCondition.Equal;
        filtroMetaNaoAtingidaCodPrj.Value = codigoProjeto;

        filtroMetaNaoAtingidaCodInd.ColumnName = "Indicadores_Resultado.CodigoIndicador";
        filtroMetaNaoAtingidaCodInd.DataType = System.Type.GetType("System.Int");
        filtroMetaNaoAtingidaCodInd.Condition = DataFilterCondition.Equal;
        filtroMetaNaoAtingidaCodInd.Value = codigoIndicador;

        filtroMetaNaoAtingidaCodMeta.ColumnName = "Indicadores_Resultado.CodigoMeta";
        filtroMetaNaoAtingidaCodMeta.DataType = System.Type.GetType("System.Int");
        filtroMetaNaoAtingidaCodMeta.Condition = DataFilterCondition.Equal;
        filtroMetaNaoAtingidaCodMeta.Value = codigoMeta;
        // -- fim filtro meta não atingida





        // -- filtro meta, linha preta que aparece no grafico (CONFIGURAÇÕES)
        filtroMetaCodPrj.ColumnName = "Indicadores_Desdobramento.CodigoProjeto";
        filtroMetaCodPrj.DataType = System.Type.GetType("System.Int");
        filtroMetaCodPrj.Condition = DataFilterCondition.Equal;
        filtroMetaCodPrj.Value = codigoProjeto;

        filtroMetaCodInd.ColumnName = "Indicadores_Desdobramento.CodigoIndicador";
        filtroMetaCodInd.DataType = System.Type.GetType("System.Int");
        filtroMetaCodInd.Condition = DataFilterCondition.Equal;
        filtroMetaCodInd.Value = codigoIndicador;

        filtroMetaCodMeta.ColumnName = "Indicadores_Desdobramento.CodigoMetaOperacional";
        filtroMetaCodMeta.DataType = System.Type.GetType("System.Int");
        filtroMetaCodMeta.Condition = DataFilterCondition.Equal;
        filtroMetaCodMeta.Value = codigoMeta;
        // -- fim filtro meta, linha preta que aparece no grafico

        xrChart1.Series[0].DataFilters.Clear();
        xrChart1.Series[1].DataFilters.Clear();
        xrChart1.Series[2].DataFilters.Clear();
        xrChart1.Series[3].DataFilters.Clear();
        xrChart1.Series[4].DataFilters.Clear();
        xrChart1.Series[5].DataFilters.Clear();



        xrChart1.Series[0].DataFiltersConjunctionMode = ConjunctionTypes.And;
        xrChart1.Series[1].DataFiltersConjunctionMode = ConjunctionTypes.And;
        xrChart1.Series[2].DataFiltersConjunctionMode = ConjunctionTypes.And;
        xrChart1.Series[3].DataFiltersConjunctionMode = ConjunctionTypes.And;
        xrChart1.Series[4].DataFiltersConjunctionMode = ConjunctionTypes.And;
        xrChart1.Series[5].DataFiltersConjunctionMode = ConjunctionTypes.And;

        xrChart1.Series[0].DataFilters.Add(filtroAtingiuMeta);
        xrChart1.Series[0].DataFilters.Add(filtroAtingiuMetaCodPrj);
        xrChart1.Series[0].DataFilters.Add(filtroAtingiuMetaCodInd);
        xrChart1.Series[0].DataFilters.Add(filtroAtingiuMetaCodMeta);

        xrChart1.Series[1].DataFilters.Add(filtroProximoMeta);
        xrChart1.Series[1].DataFilters.Add(filtroProximoMetaCodPrj);
        xrChart1.Series[1].DataFilters.Add(filtroProximoMetaCodInd);
        xrChart1.Series[1].DataFilters.Add(filtroProximoMetaCodMeta);

        xrChart1.Series[2].DataFilters.Add(filtroMetaNaoAtingida);
        xrChart1.Series[2].DataFilters.Add(filtroMetaNaoAtingidaCodPrj);
        xrChart1.Series[2].DataFilters.Add(filtroMetaNaoAtingidaCodInd);
        xrChart1.Series[2].DataFilters.Add(filtroMetaNaoAtingidaCodMeta);

        xrChart1.Series[3].DataFilters.Add(filtroAcimaDaMeta);
        xrChart1.Series[3].DataFilters.Add(filtroAcimaDaMetaCodPrj);
        xrChart1.Series[3].DataFilters.Add(filtroAcimaDaMetaCodInd);
        xrChart1.Series[3].DataFilters.Add(filtroAcimaDaMetaCodMeta);

        xrChart1.Series[4].DataFilters.Add(filtroMetaNaoDefinida);
        xrChart1.Series[4].DataFilters.Add(filtroMetaNaoDefinidaCodPrj);
        xrChart1.Series[4].DataFilters.Add(filtroMetaNaoDefinidaCodInd);
        xrChart1.Series[4].DataFilters.Add(filtroMetaNaoDefinidaCodMeta);

        xrChart1.Series[5].DataFilters.Add(filtroMetaCodPrj);
        xrChart1.Series[5].DataFilters.Add(filtroMetaCodInd);
        xrChart1.Series[5].DataFilters.Add(filtroMetaCodMeta);

    }

    private void xrChart1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
    {
        int codigoProjeto = 0;
        int codigoIndicador = 0;
        int codigoMeta = 0;
        DateTime dataReferencia = new DateTime();
        if (int.TryParse(lblCodigoProjeto.Text, out codigoProjeto) == true)
        {
            codigoProjeto = int.Parse(lblCodigoProjeto.Text);
        }

        if (int.TryParse(lblCodigoIndicador.Text, out codigoIndicador) == true)
        {
            codigoIndicador = int.Parse(lblCodigoIndicador.Text);
        }

        if (int.TryParse(lblCodigoMeta.Text, out codigoMeta) == true)
        {
            codigoMeta = int.Parse(lblCodigoMeta.Text);
        }
        if (e.Item.Axis is AxisX)
        {
            object value = e.Item.AxisValue;
            if (value != null)
            {
                string strData = value.ToString();

                if (DateTime.TryParse(strData, out dataReferencia))
                {
                    dataReferencia = DateTime.Parse(strData);
                }
            }
        }

        string select1 = string.Format(
        @"CodigoProjeto = {0} 
          AND CodigoIndicador = {1} 
          AND CodigoMeta = {2}
          AND DataReferencia = '{3}'", codigoProjeto, codigoIndicador, codigoMeta, dataReferencia);
        string select2 = string.Format(
        @"CodigoProjeto = {0} 
          AND CodigoIndicador = {1} 
          AND CodigoMetaOperacional = {2}
          AND DataReferencia = '{3}'", codigoProjeto, codigoIndicador, codigoMeta, dataReferencia);

        DataRow[] rowsResultado = dsAcompanhamento1.Resultado.Select(select1);
        DataRow[] rowsDesdobramento = dsAcompanhamento1.Desdobramento.Select(select2);

        if (rowsResultado.Length > 0)
        {
            e.Item.Text = rowsResultado[0]["Periodo"].ToString();
        }
        else if (rowsDesdobramento.Length > 0)
        {
            e.Item.Text = rowsDesdobramento[0]["Periodo"].ToString();
        }
    }

    private void xrChart2_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        var Ldraw = e.LegendDrawOptions as Pie3DDrawOptions;

        string x = e.LabelText;
        string argumento = e.SeriesPoint.Argument;
        string valor = (e.SeriesPoint.Values.Length > 0) ? e.SeriesPoint.Values[0].ToString() : "";

        Ldraw.FillStyle.FillMode = FillMode3D.Solid;


        if (argumento == "Sem LB")
        {
            Ldraw.Color = Color.Gray;
            e.SeriesDrawOptions.Color = Color.Gray;
            e.LegendTextColor = Color.Black;

        }
        else if (argumento == "Com início atrasado")
        {
            Ldraw.Color = Color.Yellow;
            e.SeriesDrawOptions.Color = Color.Yellow;
            e.LegendTextColor = Color.Black;
        }
        else if (argumento == "Concluída")
        {
            Ldraw.Color = Color.Blue;
            e.SeriesDrawOptions.Color = Color.Blue;
            e.LegendTextColor = Color.Black;
        }
        else if (argumento == "Em andamento")
        {
            Ldraw.Color = Color.Green;
            e.SeriesDrawOptions.Color = Color.Green;
            e.LegendTextColor = Color.Black;
        }
        else if (argumento == "A iniciar")
        {
            Ldraw.Color = Color.Orange;
            e.SeriesDrawOptions.Color = Color.Orange;
            e.LegendTextColor = Color.Black;

        }
        else if (argumento == "Atrasada")
        {
            Ldraw.Color = Color.Red;
            e.SeriesDrawOptions.Color = Color.Red;
            e.LegendTextColor = Color.Black;
        }
        else if (argumento == null)
        {
            Ldraw.Color = Color.White;
            e.SeriesDrawOptions.Color = Color.White;
            e.LegendTextColor = Color.Black;
        }
    }

    /// <summary>
    /// Esta função será utilizada para converter a formatação em HTML em seu equivalente com a formatação do Microsoft Word
    /// Ainda não está sendo utilizada, pois nao funcionou corretamente
    /// Pretende-se aprimorá-la para uso posterior.
    /// Esta função tem como argumento uma string em HTML
    /// e retorna um texto com formatação RTF
    /// </summary>
    public static string ConvertHtmlToText(string source)
    {

        string result;
        result = source.Replace("\r", " ");
        result = result.Replace("\n", " ");
        result = result.Replace("\t", string.Empty);
        // Remove repeating speces becuase browsers ignore them
        result = System.Text.RegularExpressions.Regex.Replace(result,
                                                              @"( )+", " ");

        // Remove the header (prepare first by clearing attributes)
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*head([^>])*>", "<head>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<( )*(/)( )*head( )*>)", "</head>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(<head>).*(</head>)", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // remove all scripts (prepare first by clearing attributes)
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*script([^>])*>", "<script>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<( )*(/)( )*script( )*>)", "</script>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //result = System.Text.RegularExpressions.Regex.Replace(result, 
        //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
        //         string.Empty, 
        //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<script>).*(</script>)", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // remove all styles (prepare first by clearing attributes)
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*style([^>])*>", "<style>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<( )*(/)( )*style( )*>)", "</style>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(<style>).*(</style>)", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // insert tabs in spaces of <td> tags
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*td([^>])*>", "\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // insert line breaks in places of <BR> and <LI> tags
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*br( )*>", "\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*li( )*>", "\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // insert line paragraphs (double line breaks) in place
        // if <P>, <DIV> and <TR> tags
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*div([^>])*>", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*tr([^>])*>", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*p([^>])*>", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // Remove remaining tags like <a>, links, images,
        // comments etc - anything thats enclosed inside < >
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<[^>]*>", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // replace special characters:
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&nbsp;", " ",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&bull;", " * ",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&lsaquo;", "<",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&rsaquo;", ">",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&trade;", "(tm)",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&frasl;", "/",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<", "<",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @">", ">",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&copy;", "(c)",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&reg;", "(r)",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Remove all others. More can be added, see
        // http://hotwired.lycos.com/webmonkey/reference/special_characters/
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&(.{2,6});", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);


        // make line breaking consistent
        result = result.Replace("\n", "\r");

        // Remove extra line breaks and tabs:
        // replace over 2 breaks with 2 and over 4 tabs with 4. 
        // Prepare first to remove any whitespaces inbetween
        // the escaped characters and remove redundant tabs inbetween linebreaks
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)( )+(\r)", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\t)( )+(\t)", "\t\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\t)( )+(\r)", "\t\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)( )+(\t)", "\r\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Remove redundant tabs
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)(\t)+(\r)", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Remove multible tabs followind a linebreak with just one tab
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)(\t)+", "\r\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Initial replacement target string for linebreaks
        string breaks = "\r\r\r";
        // Initial replacement target string for tabs
        string tabs = "\t\t\t\t\t";
        for (int index = 0; index < result.Length; index++)
        {
            result = result.Replace(breaks, "\r\r");
            result = result.Replace(tabs, "\t\t\t\t");
            breaks = breaks + "\r";
            tabs = tabs + "\t";
        }

        // Thats it.
        return result;

    }

    private void xrTxtAnaliseCritica_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!string.IsNullOrEmpty(xrTxtAnaliseCritica.Text.TrimEnd()))
        {
            xrTxtAnaliseCritica.Borders = BorderSide.All;
            xrTxtAnaliseCritica.BorderColor = Color.Black;
            //xrTxtAnaliseCritica.Html = ConvertHtmlToText(xrTxtAnaliseCritica.Text);
        }
        else
        {
            xrTxtAnaliseCritica.Borders = BorderSide.None;
            xrTxtAnaliseCritica.BorderColor = Color.Transparent;
        }

    }

    private void DetailReport1_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {
        object codigoProjeto = GetCurrentColumnValue("CodigoProjeto");
        string mensagem = dsAcompanhamento1.Indicadores.Select("CodigoProjeto = " + codigoProjeto)[e.CurrentRow]["Mensagem"] as string;
        if (string.IsNullOrEmpty(mensagem))
        {
            xrChart1.Visible = true;
            xrChart1.SizeF = new System.Drawing.SizeF(1799, 486);
            seta.Visible = true;
            tblAnaliseCritica.Visible = true;

            xrPanel2.LocationF = new PointF(65f, xrPanel4.HeightF + (xrChart1.HeightF + 30f));
            Detail1.HeightF = 150f;

        }
        else
        {
            xrChart1.SizeF = new System.Drawing.SizeF(1799, 10);
            xrChart1.Visible = false;
            seta.Visible = false;
            xrPanel2.LocationF = new PointF(65f, xrPanel4.HeightF + 30f);
            tblAnaliseCritica.Visible = false;
            Detail1.HeightF = 150f;
        }
    }

    private void GroupFooter2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataRow[] rows = dsAcompanhamento1.SituacoesPossiveis.Select("CodigoProjeto = " + CodigoProjeto.Value);

        lblAvisoPlanoAcao.Text = "";
        if (rows.Length > 0)
        {
            lblAvisoPlanoAcao.Text = "Situação atual do plano de ação";

            DataFilter filtroCodigoProjeto = new DataFilter();
            filtroCodigoProjeto.ColumnName = "CodigoProjeto";
            filtroCodigoProjeto.DataType = System.Type.GetType("System.Int");
            filtroCodigoProjeto.Condition = DataFilterCondition.Equal;
            filtroCodigoProjeto.Value = CodigoProjeto.Value;

            xrChart2.Series[0].DataFilters.Clear();
            xrChart2.Series[0].DataFilters.Add(filtroCodigoProjeto);

            Pie3DSeriesView myView = (Pie3DSeriesView)xrChart2.Series[0].View;


            // Specify a data filter to explode points.
            myView.ExplodedPointsFilters.Clear();

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.Equal, "Sem LB"));

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.Equal, "Com início atrasado"));

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.Equal, "Concluída"));

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.Equal, "Em andamento"));

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.Equal, "A iniciar"));
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
            DataFilterCondition.Equal, "Atrasada"));

            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.Equal, ""));

            myView.ExplodeMode = PieExplodeMode.UseFilters;
            myView.ExplodedDistancePercentage = 20;
            //myView.RuntimeExploding = false;
            //myView.HeightToWidthRatio = 99;

            if (xrChart2.Series[0].Points.Count > 0)
            {
                Detail.HeightF = 791.8126f;
                xrChart2.HeightF = 562.38f;
                xrChart2.Visible = true;
                xrPanel1.LocationF = new PointF(65.00f, (xrChart2.HeightF + 20f + lblAvisoPlanoAcao.HeightF));

            }
            else
            {
                //altura do chart 562,38
                lblAvisoPlanoAcao.Text = "Sem dados de situação atual do plano de ação";
                //74,08; 643,65
                xrChart2.HeightF = 10f;//562.38f;
                xrChart2.Visible = false;
                xrPanel1.LocationF = new PointF(65f, lblAvisoPlanoAcao.HeightF + xrChart2.HeightF + 20f);
                Detail.HeightF = 10f;
            }
        }
        else
        {
            lblAvisoPlanoAcao.Text = "Sem dados de situação atual do plano de ação";
            //74,08; 643,65
            xrChart2.HeightF = 10f;//562.38f;
            xrChart2.Visible = false;
            xrPanel1.LocationF = new PointF(65f, lblAvisoPlanoAcao.HeightF + xrChart2.HeightF + 20f);
            Detail.HeightF = 10f;
        }
    }

    private void detailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        CodigoProjeto.Value = GetCurrentColumnValue("CodigoProjeto");
    }

    #endregion Methods

}
