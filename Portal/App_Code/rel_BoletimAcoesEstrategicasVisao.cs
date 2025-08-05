using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;

/// <summary>
/// Summary description for rel_BoletimAcoesEstrategicasVisao
/// </summary>
public class rel_BoletimAcoesEstrategicasVisao : DevExpress.XtraReports.UI.XtraReport
{
    #region Fields

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DsBoletimStatus dsBoletimStatus;
    private PageHeaderBand PageHeader;
    private XRLine xrLine1;
    private XRLabel xrLabel1;
    private XRLabel lblValorOrcamento;
    private XRLabel xrLabel7;
    private XRLine xrLine2;
    private XRRichText xrRichText1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRPictureBox picLogoEntidade;
    private XRLabel xrLabel10;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel8;
    private XRPageInfo xrPageInfo1;
    private XRPanel xrPanel1;
    private XRChart xrChart5;
    private XRLabel xrLabel12;
    private XRPanel xrPanel2;
    private XRPictureBox xrPictureBox7;
    private XRLabel xrLabel13;
    private XRChart xrChart4;
    private XRLabel xrLabel9;
    private XRLabel xrLabel6;
    public DevExpress.XtraReports.Parameters.Parameter ParamDataInicioPeriodoRelatorio;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    private XRLabel xrLabel26;
    private XRLabel xrLabel25;
    private XRSubreport xrSubreportLegenda;
    private XRPanel panelInfoLegenda;
    private XRLabel xrLabel4;
    private XRLabel xrLabel2;
    private XRLabel xrLabel14;
    private XRPanel pnlReceita;
    private XRLabel xrLabel15;
    private XRChart xrChart3;
    private XRLabel xrLabel3;
    private XRLabel xrLabel11;

    private dados cDados = CdadosUtil.GetCdados(null);

    string fontFamilyAnaliseCritica;
    private XRLabel xrLabel18;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel5;
    private GroupFooterBand GroupFooter1;
    private ReportFooterBand ReportFooter;
    string fontSizeAnaliseCritica;

    #endregion

    private string destaquesMes;
    private XRSubreport xrSubreportInformacoesAdicionais;
    private XRLabel xrLabel16;
    private XRPanel pnlCusto;
    private XRChart xrChart1;
    private XRChart xrChart2;
    private XRLabel xrLabel20;
    private XRLabel xrLabel21;


    #region Constructors

    public rel_BoletimAcoesEstrategicasVisao(int codigoStatusReport)
    {
        InitializeComponent();
        InitData(codigoStatusReport);
        var projeto = dsBoletimStatus.Projetos.First();
        string periodo = string.Format("{0:d} a {1:d}",
            projeto.DataInicioPeriodoRelatorio,
            projeto.DataTerminoPeriodoRelatorio);
        ParamDataInicioPeriodoRelatorio.Value = projeto.DataInicioPeriodoRelatorio;
    }

    #endregion

    #region Methods

    private void InitData(int codigoStatusReport)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        //Image logo = cDados.ObtemLogoEntidade();
        //picLogoEntidade.Image = logo;

        #region Comando SQL

        comandoSql = string.Format(@"exec p_rel_BoletimQuinzenal01 {0}", codigoStatusReport);

        #endregion

        string[] tableNames = new string[] { "Projetos", "DadosEntrega", "LegendaDesempenho", "Produtos" };

        DataSet dsTemp = cDados.getDataSet(comandoSql);
        dsBoletimStatus.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);
        /*dsBoletimStatus.Projetos
            .Where(p => string.IsNullOrWhiteSpace(p.NomeFocoEstrategico))
            .ToList().ForEach(p =>
            {
                p.CodigoFoco = int.MaxValue;
                p.NomeFocoEstrategico = "Nenhum"; 
            });*/
        foreach (var p in dsBoletimStatus.Projetos)
        {
            if (!p.IsPercentualFisicoRealizadoNull() && p.PercentualFisicoRealizado > 100)
                p.PercentualFisicoRealizado = 100;
            if (!p.IsPercentualReceitaRealizadoNull() && p.PercentualReceitaRealizado > 100)
                p.PercentualReceitaRealizado = 100;
            if (!p.IsPercentualFinanceiroRealizadoNull() && p.PercentualFinanceiroRealizado > 100)
                p.PercentualFinanceiroRealizado = 100;
        }

        DefineLogoRelatorio();
        xrSubreportLegenda.ReportSource.DataSource = dsBoletimStatus;

        DefineConfiguracoesFontAnaliseCritica();
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

        string nomeCor = GetCurrentColumnValue(nomeColunaCor).ToString();

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

    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "rel_BoletimAcoesEstrategicasVisao.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_BoletimAcoesEstrategicasVisao.ResourceManager;
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
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.lblValorOrcamento = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.pnlCusto = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.dsBoletimStatus = new DsBoletimStatus();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart5 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlReceita = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart4 = new DevExpress.XtraReports.UI.XRChart();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.ParamDataInicioPeriodoRelatorio = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrSubreportLegenda = new DevExpress.XtraReports.UI.XRSubreport();
        this.panelInfoLegenda = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox7 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrSubreportInformacoesAdicionais = new DevExpress.XtraReports.UI.XRSubreport();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatus)).BeginInit();
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
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2,
            this.xrPanel1,
            this.xrRichText1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1450F;
        this.Detail.KeepTogether = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel7,
            this.xrLine2,
            this.lblValorOrcamento});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(1900F, 150F);
        // 
        // xrLabel24
        // 
        this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataInicioProjeto", "{0:dd/MM/yyyy}")});
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel24.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(1240F, 75.00002F);
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
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(1150F, 75.00002F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(90F, 50F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseForeColor = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.Text = "Início:";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataTerminoPrevistoProjeto", "{0:dd/MM/yyyy}")});
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel26.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(1690F, 75.00002F);
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
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(1450F, 75.00002F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(240F, 50F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseForeColor = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "Término Previsto:";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        // lblValorOrcamento
        // 
        this.lblValorOrcamento.CanShrink = true;
        this.lblValorOrcamento.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.ValorOrcamento", "{0:c2}")});
        this.lblValorOrcamento.Dpi = 254F;
        this.lblValorOrcamento.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.lblValorOrcamento.ForeColor = System.Drawing.Color.Maroon;
        this.lblValorOrcamento.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00002F);
        this.lblValorOrcamento.Name = "lblValorOrcamento";
        this.lblValorOrcamento.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblValorOrcamento.SizeF = new System.Drawing.SizeF(900F, 50F);
        this.lblValorOrcamento.StylePriority.UseFont = false;
        this.lblValorOrcamento.StylePriority.UseForeColor = false;
        this.lblValorOrcamento.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblValorOrcamento_BeforePrint);
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlCusto,
            this.pnlReceita,
            this.xrLabel6,
            this.xrLabel13,
            this.xrChart4});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1900F, 1250F);
        this.xrPanel1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPanel1_BeforePrint);
        // 
        // pnlCusto
        // 
        this.pnlCusto.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20,
            this.xrChart1,
            this.xrLabel12,
            this.xrChart5,
            this.xrLabel9});
        this.pnlCusto.Dpi = 254F;
        this.pnlCusto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 430F);
        this.pnlCusto.Name = "pnlCusto";
        this.pnlCusto.SizeF = new System.Drawing.SizeF(1900F, 410F);
        // 
        // xrLabel20
        // 
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(974.9999F, 0F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(922.0002F, 50F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseForeColor = false;
        this.xrLabel20.Text = "Despesa no Ano";
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
        // dsBoletimStatus
        // 
        this.dsBoletimStatus.DataSetName = "DsBoletimStatus";
        this.dsBoletimStatus.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(1.499989F, 0F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(922.0002F, 50F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.Text = "Despesa Total";
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
        // xrLabel9
        // 
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(1.499666F, 350.0002F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(924.9999F, 60.00006F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        // 
        // pnlReceita
        // 
        this.pnlReceita.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21,
            this.xrChart2,
            this.xrLabel15,
            this.xrChart3,
            this.xrLabel3});
        this.pnlReceita.Dpi = 254F;
        this.pnlReceita.LocationFloat = new DevExpress.Utils.PointFloat(0F, 840F);
        this.pnlReceita.Name = "pnlReceita";
        this.pnlReceita.SizeF = new System.Drawing.SizeF(1900F, 410F);
        // 
        // xrLabel21
        // 
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(975F, 0F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(925F, 50F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseForeColor = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = "Receita no Ano";
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
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
        // xrLabel15
        // 
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(925F, 50F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseForeColor = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "Receita Total";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
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
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 349.9999F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(924.9999F, 60.00006F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 359.9998F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(925F, 60.00006F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "(*) 100% refere-se a data fim do projeto.";
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(925F, 50F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseForeColor = false;
        this.xrLabel13.Text = "Físico";
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
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projetos.AnaliseCritica")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 10F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1400F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrRichText1.StylePriority.UseBorders = false;
        this.xrRichText1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblAnaliseCritica_EvaluateBinding);
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
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1646F, 40.58001F);
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
            this.xrLabel8,
            this.xrLabel10,
            this.picLogoEntidade,
            this.xrLine1,
            this.xrLabel1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 310F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.ParamDataInicioPeriodoRelatorio, "Text", "Período: {0}")});
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.ForeColor = System.Drawing.Color.Black;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseForeColor = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Período: Novembro de 2012";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // ParamDataInicioPeriodoRelatorio
        // 
        this.ParamDataInicioPeriodoRelatorio.Name = "ParamDataInicioPeriodoRelatorio";
        this.ParamDataInicioPeriodoRelatorio.Type = typeof(System.String);
        this.ParamDataInicioPeriodoRelatorio.ValueInfo = "04/01/2013 16:48:04";
        this.ParamDataInicioPeriodoRelatorio.Visible = false;
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataEmissaoRelatorio", "Emissão: {0:dd/MM/yyyy HH:mm:ss}")});
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(1275F, 250F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "xrLabel10";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
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
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001614888F, 299.9999F);
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
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1275F, 75.00002F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Boletim de Ações Estratégicas";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrSubreportLegenda,
            this.panelInfoLegenda,
            this.xrPictureBox7});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 355F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.PrintOn = ((DevExpress.XtraReports.UI.PrintOnPages)((DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader | DevExpress.XtraReports.UI.PrintOnPages.NotWithReportFooter)));
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 315F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1902F, 40F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrLabel18.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel18_BeforePrint);
        // 
        // xrSubreportLegenda
        // 
        this.xrSubreportLegenda.Dpi = 254F;
        this.xrSubreportLegenda.LocationFloat = new DevExpress.Utils.PointFloat(251.5F, 0F);
        this.xrSubreportLegenda.Name = "xrSubreportLegenda";
        this.xrSubreportLegenda.ReportSource = new rel_LegendasFisicoFinanceiro();
        this.xrSubreportLegenda.SizeF = new System.Drawing.SizeF(1648.5F, 140F);
        // 
        // panelInfoLegenda
        // 
        this.panelInfoLegenda.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.xrLabel4,
            this.xrLabel2,
            this.xrLabel14});
        this.panelInfoLegenda.Dpi = 254F;
        this.panelInfoLegenda.LocationFloat = new DevExpress.Utils.PointFloat(1.499989F, 0F);
        this.panelInfoLegenda.Name = "panelInfoLegenda";
        this.panelInfoLegenda.SizeF = new System.Drawing.SizeF(250F, 140F);
        // 
        // xrLabel11
        // 
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(130F, 100F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseForeColor = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "Receita";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(130F, 40F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseForeColor = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Legenda:";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(130F, 50F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Despesa";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
        this.xrPictureBox7.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
        this.xrPictureBox7.Dpi = 254F;
        this.xrPictureBox7.ImageUrl = "~\\espacoCliente\\Rodape01BoletimStatus.png";
        this.xrPictureBox7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 140F);
        this.xrPictureBox7.Name = "xrPictureBox7";
        this.xrPictureBox7.SizeF = new System.Drawing.SizeF(1900F, 175F);
        this.xrPictureBox7.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel16,
            this.xrLabel5});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoFoco", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 100F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel16
        // 
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(360F, 50F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.Text = "Foco Estratégico: ";
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.NomeFocoEstrategico", "{0}")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(360F, 25.00001F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(1540F, 50F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "[NomeFocoEstrategico]";
        this.xrLabel5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel5_BeforePrint);
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.GroupUnion = DevExpress.XtraReports.UI.GroupFooterUnion.WithLastDetail;
        this.GroupFooter1.HeightF = 0F;
        this.GroupFooter1.Name = "GroupFooter1";
        this.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportInformacoesAdicionais});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 100F;
        this.ReportFooter.Name = "ReportFooter";
        this.ReportFooter.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
        this.ReportFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportFooter_BeforePrint);
        // 
        // xrSubreportInformacoesAdicionais
        // 
        this.xrSubreportInformacoesAdicionais.Dpi = 254F;
        this.xrSubreportInformacoesAdicionais.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
        this.xrSubreportInformacoesAdicionais.Name = "xrSubreportInformacoesAdicionais";
        this.xrSubreportInformacoesAdicionais.ReportSource = new rel_InformacoesAdicionaisBAE();
        this.xrSubreportInformacoesAdicionais.SizeF = new System.Drawing.SizeF(1900F, 50F);
        // 
        // rel_BoletimAcoesEstrategicasVisao
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.GroupHeader1,
            this.GroupFooter1,
            this.ReportFooter});
        this.DataMember = "Projetos";
        this.DataSource = this.dsBoletimStatus;
        this.Dpi = 254F;
        this.FilterString = "[IndicaPrograma] <> \'S\' And [IndicaPrograma] <> \'s\'";
        this.Font = new System.Drawing.Font("Verdana", 10F);
        this.Margins = new System.Drawing.Printing.Margins(99, 99, 99, 99);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ParamDataInicioPeriodoRelatorio});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "15.1";
        this.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
        this.Watermark.PageRange = "1";
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatus)).EndInit();
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
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Event Handlers

    private void xrLabel11_TextChanged(object sender, EventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        label.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(label.Text);
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

    private void lblAnaliseCritica_EvaluateBinding(object sender, BindingEventArgs e)
    {
        XRRichText richText = (XRRichText)sender;
        String strValue = (e.Value as string) ?? string.Empty;
        if (String.IsNullOrEmpty(strValue.Trim()))
        {
            richText.Font = new Font(
                                fontFamilyAnaliseCritica,
                                float.Parse(fontSizeAnaliseCritica),
                                FontStyle.Italic,
                                GraphicsUnit.Point,
                                ((byte)(0)));
            strValue = "[Sem análise crítica para o período]";
        }
        strValue = string.Format(
            "<div style=\"font-family: {1}; font-size: {2}pt;\">{0}</div>"
            , strValue, fontFamilyAnaliseCritica, fontSizeAnaliseCritica);
        e.Value = strValue;
    }

    private void lblValorOrcamento_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //string dataPrevisaoConclusao = string.Format(
        //    "{0:MMM/yyyy}", GetCurrentColumnValue("DataPrevisaoConclusao"));
        //dataPrevisaoConclusao = UppercaseFirst(dataPrevisaoConclusao);
        //lblValorOrcamento.Text = string.Concat(
        //    lblValorOrcamento.Text, dataPrevisaoConclusao);
        string text = lblValorOrcamento.Text;
        lblValorOrcamento.Text = string.Format("Orçamento até Dez/{0}: {1}"
            , DateTime.Today.Year, text);
    }

    private void xrChart5_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XtraReportBase report = ((XRControl)sender).Report;
        string nomeColuna = e.Series.Name == "SerieRealizado" ?
            "ValorCustoRealizadoOriginal" : "ValorCustoPrevistoOriginal";
        object valor = report.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);
    }

    #endregion

    private void xrPanel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrChart3_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XtraReportBase report = ((XRControl)sender).Report;
        string nomeColuna = e.Series.Name == "SerieRealizado" ?
            "ValorReceitaRealizadoOriginal" : "ValorReceitaPrevistoOriginal";
        object valor = report.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);
    }

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        #region Comentario
        /*XRPanel panel = xrPanel1;
        object valorReceitaPrev =
            panel.Report.GetCurrentColumnValue("PercentualReceitaPrevisto");
        object valorReceitaReal =
            panel.Report.GetCurrentColumnValue("PercentualReceitaRealizado");
        bool existeValoresReceita =
            !(Convert.IsDBNull(valorReceitaPrev) || ((decimal)valorReceitaPrev) == 0) ||
            !(Convert.IsDBNull(valorReceitaReal) || ((decimal)valorReceitaReal) == 0);
        if (existeValoresReceita)
        {
            if (!panel.Controls.Contains(pnlReceita))
            {
                panel.Band.HeightF = 1035;
                panel.HeightF = 775;
                xrRichText1.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
                panel.Controls.Add(pnlReceita);
                pnlReceita.LocationF = new PointF(0, 425);
            }
        }
        else if (panel.Controls.Contains(pnlReceita))
        {
            panel.Controls.Remove(pnlReceita);
            panel.HeightF = 425;
            xrRichText1.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
            panel.Band.HeightF = 675;
        }*/

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
        DataSet dsParams = cDados.getParametrosSistema("TextoRodape_BAE");
        if (dsParams.Tables[0].Rows.Count > 0)
            label.Text = dsParams.Tables[0].Rows[0]["TextoRodape_BAE"] as string;
    }

    private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        destaquesMes = dsBoletimStatus.Projetos
            .Single(r => r.IndicaPrograma.ToUpper() == "S").DestaquesMes;
        if (string.IsNullOrWhiteSpace(destaquesMes))
            e.Cancel = true;
        else
        {
            DataSet dsParams = cDados.getParametrosSistema("TextoRodape_BAE");
            string textoRodape = dsParams.Tables[0].Rows[0]["TextoRodape_BAE"] as string;

            rel_InformacoesAdicionaisBAE subReport =
                (rel_InformacoesAdicionaisBAE)xrSubreportInformacoesAdicionais.ReportSource;
            subReport.pDestaquesMes.Value = destaquesMes;
            subReport.pTextoRodape.Value = textoRodape;
        }
    }

    private void xrSubreportInformacoesAdicionais_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        if (string.IsNullOrWhiteSpace(label.Text))
        {
            label.Text = "Nenhum";
            label.Font = new Font("Verdana", 10, FontStyle.Italic | FontStyle.Bold);
        }
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
