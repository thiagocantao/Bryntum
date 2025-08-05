using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;


/// <summary>
/// Summary description for rel_BoletimStatusNacional
/// </summary>
public class rel_BoletimStatusNacional : XtraReport
{
    #region fields

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private XRPageInfo xrPageInfo1;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel1;
    private XRPictureBox picLogoEntidade;
    private XRLabel xrLabel3;
    private XRLine xrLine1;
    #endregion

    private XRLabel xrLabel6;
    private DsBoletimStatus ds;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRLabel xrLabel19;
    private XRChart xrChart3;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel25;
    private XRLabel xrLabel21;
    private XRSubreport xrSubreport2;
    private XRLabel xrLabel20;
    private XRLabel xrLabel28;
    private XRChart xrChart1;
    private XRChart xrChart4;
    private XRLabel xrLabel34;
    private XRChart xrChart5;
    private XRLabel xrLabel38;
    private XRChart xrChart2;
    private XRLabel xrLabel11;
    private XRRichText xrRichText1;
    private XRLabel xrLabel2;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox12;
    private XRPanel pnlAnaliseCritica;
    private XRLabel xrlStatus;
    private XRChart xrChart6;
    private XRLabel xrLabel23;
    private XRLabel xrLabel45;
    private XRLabel xrLabel46;
    private XRLabel xrLabel47;
    private XRLabel xrLabel48;
    private XRRichText xrRichText2;
    private XRLabel xrLabel49;
    private XRPanel pnlGraficos;
    private XRPanel pnlGraficosSuperior;
    private XRPanel pnlGraficoReceitaSuperior;
    private XRLabel xrLabel16;
    private XRPanel pnlGraficoReceita;
    private XRLabel xrLabel22;
    private XRLabel xrLabel26;
    private XRLabel xrLabel24;
    private XRLabel xrLabel27;
    private XRLabel xrLabel31;
    private XRLabel xrLabel30;
    private XRSubreport xrSubreportLegenda;
    private XRLabel xrLabel7;
    private XRLabel xrLabel4;
    private XRLabel xrLabel14;
    private XRPanel panelInfoLegenda;
    private XRLabel xrLabel5;
    private XRLabel xrLabel12;
    private XRLabel xrLabel13;


    private dados cDados = CdadosUtil.GetCdados(null);

    string fontFamilyAnaliseCritica;
    private XRPanel pnlSuperiorAnaliseCritica;
    private XRLabel xrLabel18;
    string fontSizeAnaliseCritica;
    private XRPanel pnlGraficoDespesaSuperior;
    private XRLabel xrLabel10;
    private XRChart xrChart7;
    private XRLabel xrLabel15;
    private XRChart xrChart8;
    private XRLabel xrLabel17;
    private XRChart xrChart9;
    private XRPanel pnlGraficoDespesa;
    private XRLabel xrLabel33;
    private XRChart xrChart10;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRLabel xrLabel37;
    private XRLabel xrLabel39;
    int codigoStatusReport;
    private DataSet dsGlobal;
    #region Constructors

    public rel_BoletimStatusNacional(int codigoStatusReport)
    {
        this.codigoStatusReport = codigoStatusReport;
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
        if (nomeCor == null)
            return;

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

        #region Comando SQL

        comandoSql = string.Format(@"exec p_rel_BoletimQuinzenal01 {0}", codigoStatusReport);

        #endregion

        string[] tableNames = new string[] { "Projetos", "DadosEntrega", "LegendaDesempenho", "Produtos" };

        DataSet dsTemp = cDados.getDataSet(comandoSql);
        dsGlobal = dsTemp.Copy();
        ds.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);
        foreach (var p in ds.Projetos)
        {
            if (!p.IsPercentualFisicoRealizadoNull() && p.PercentualFisicoRealizado > 100)
                p.PercentualFisicoRealizado = 100;
            if (!p.IsPercentualReceitaRealizadoNull() && p.PercentualReceitaRealizado > 100)
                p.PercentualReceitaRealizado = 100;
            if (!p.IsPercentualFinanceiroRealizadoNull() && p.PercentualFinanceiroRealizado > 100)
                p.PercentualFinanceiroRealizado = 100;
        }

        DefineCodigoObjetoSuperior();

        DefineLogoRelatorio();

        xrSubreport2.ReportSource.DataSource = ds.Copy();
        xrSubreport2.ReportSource.DataMember = "Produtos";

        xrSubreportLegenda.ReportSource.DataSource = ds;
        //xrSubreportLegendaSuperior.ReportSource.DataSource = ds;

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

    private void DefineCodigoObjetoSuperior()
    {
        if (ds.Projetos.Any(p => p.IndicaPrograma.ToUpper() == "S"))
        {
            DsBoletimStatus.ProjetosRow objetoSuperior =
                ds.Projetos.Where(p => p.IndicaPrograma.ToUpper() == "S").Single();
            List<DsBoletimStatus.ProjetosRow> projetos =
                ds.Projetos.Where(p => p.IndicaPrograma.ToUpper() != "S").ToList();
            projetos.ForEach(p => p.CodigoObjetoSuperior = objetoSuperior.CodigoObjeto);
        }
        else if (ds.Projetos.Count == 1)
        {
            Detail.Visible = false;
            DsBoletimStatus.ProjetosRow row = ds.Projetos.Single();
            DsBoletimStatus.ProjetosRow parentRow = ds.Projetos.NewProjetosRow();
            parentRow.ItemArray = row.ItemArray;
            parentRow.CodigoObjeto -= 1;//Subtrai 1 do código para os códigos das duas linhas não sejam os mesmos
            parentRow.IndicaPrograma = "S";
            ds.Projetos.AddProjetosRow(parentRow);
            row.SetParentRow(parentRow);
        }
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
        string resourceFileName = "rel_BoletimStatusNacional.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_BoletimStatusNacional.ResourceManager;
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
        DevExpress.XtraCharts.XYDiagram xyDiagram6 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series11 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel16 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView11 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series12 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel17 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView12 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel18 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram7 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series13 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel19 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView13 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series14 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel20 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView14 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel21 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram8 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series15 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel22 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView15 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series16 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel23 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView16 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel24 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram9 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series17 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel25 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView17 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series18 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel26 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView18 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel27 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.XYDiagram xyDiagram10 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series19 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel28 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView19 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series20 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel29 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView20 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel30 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.pnlGraficosSuperior = new DevExpress.XtraReports.UI.XRPanel();
        this.pnlGraficoDespesaSuperior = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart7 = new DevExpress.XtraReports.UI.XRChart();
        this.ds = new DsBoletimStatus();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlGraficoReceitaSuperior = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart8 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrlStatus = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart6 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlSuperiorAnaliseCritica = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.pnlGraficos = new DevExpress.XtraReports.UI.XRPanel();
        this.pnlGraficoDespesa = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart10 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart5 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlGraficoReceita = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart9 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart4 = new DevExpress.XtraReports.UI.XRChart();
        this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.pnlAnaliseCritica = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrSubreportLegenda = new DevExpress.XtraReports.UI.XRSubreport();
        this.panelInfoLegenda = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox12 = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart8)).BeginInit();
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
        ((System.ComponentModel.ISupportInitialize)(this.xrChart6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel19)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel20)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel21)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel22)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel23)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel24)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel25)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel26)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel27)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series19)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel28)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView19)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series20)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel29)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView20)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel30)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlGraficosSuperior,
            this.xrLabel3,
            this.pnlSuperiorAnaliseCritica});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1680F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("IndicaPrograma", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending),
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // pnlGraficosSuperior
        // 
        this.pnlGraficosSuperior.CanGrow = false;
        this.pnlGraficosSuperior.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlGraficoDespesaSuperior,
            this.xrLabel24,
            this.pnlGraficoReceitaSuperior,
            this.xrlStatus,
            this.xrChart6,
            this.xrLabel45,
            this.xrLabel23});
        this.pnlGraficosSuperior.Dpi = 254F;
        this.pnlGraficosSuperior.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
        this.pnlGraficosSuperior.Name = "pnlGraficosSuperior";
        this.pnlGraficosSuperior.SizeF = new System.Drawing.SizeF(1800F, 1430F);
        // 
        // pnlGraficoDespesaSuperior
        // 
        this.pnlGraficoDespesaSuperior.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35,
            this.xrLabel10,
            this.xrChart7,
            this.xrLabel26,
            this.xrChart2,
            this.xrLabel46,
            this.xrLabel11});
        this.pnlGraficoDespesaSuperior.Dpi = 254F;
        this.pnlGraficoDespesaSuperior.LocationFloat = new DevExpress.Utils.PointFloat(0F, 510F);
        this.pnlGraficoDespesaSuperior.Name = "pnlGraficoDespesaSuperior";
        this.pnlGraficoDespesaSuperior.SizeF = new System.Drawing.SizeF(1800F, 460F);
        // 
        // xrLabel35
        // 
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(925.0001F, 0F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(875F, 50F);
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseForeColor = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "Despesa no Ano";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DesvioCustoAno", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(925F, 410F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(875F, 50F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "xrLabel10";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrChart7
        // 
        this.xrChart7.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart7.BackImage.Image")));
        this.xrChart7.BackImage.Stretch = true;
        this.xrChart7.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart7.DataMember = "Projetos";
        this.xrChart7.DataSource = this.ds;
        xyDiagram1.AxisX.Label.Visible = false;
        xyDiagram1.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram1.AxisX.Title.Text = "Despesa (R$)";
        xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram1.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram1.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
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
        this.xrChart7.Diagram = xyDiagram1;
        this.xrChart7.Dpi = 254F;
        this.xrChart7.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart7.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart7.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart7.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart7.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart7.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart7.Legend.HorizontalIndent = 25;
        this.xrChart7.LocationFloat = new DevExpress.Utils.PointFloat(925F, 50F);
        this.xrChart7.Name = "xrChart7";
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
        series2.View = sideBySideBarSeriesView2;
        this.xrChart7.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart7.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
        this.xrChart7.SizeF = new System.Drawing.SizeF(875F, 300F);
        this.xrChart7.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartNovos_CustomDrawSeriesPoint);
        this.xrChart7.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // ds
        // 
        this.ds.DataSetName = "DsBoletimStatus";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrLabel26
        // 
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(875F, 50F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseForeColor = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "Despesa Total";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrChart2
        // 
        this.xrChart2.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart2.BackImage.Image")));
        this.xrChart2.BackImage.Stretch = true;
        this.xrChart2.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart2.DataMember = "Projetos";
        this.xrChart2.DataSource = this.ds;
        xyDiagram2.AxisX.Label.Visible = false;
        xyDiagram2.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram2.AxisX.Title.Text = "Despesa (R$)";
        xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram2.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram2.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram2.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
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
        this.xrChart2.Diagram = xyDiagram2;
        this.xrChart2.Dpi = 254F;
        this.xrChart2.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart2.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart2.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart2.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart2.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart2.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart2.Legend.HorizontalIndent = 25;
        this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001614888F, 49.99994F);
        this.xrChart2.Name = "xrChart2";
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
        series4.View = sideBySideBarSeriesView4;
        this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart2.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
        this.xrChart2.SizeF = new System.Drawing.SizeF(875F, 300F);
        this.xrChart2.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_CustomDrawSeriesPoint);
        this.xrChart2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel46
        // 
        this.xrLabel46.Dpi = 254F;
        this.xrLabel46.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(0.0001614888F, 350F);
        this.xrLabel46.Name = "xrLabel46";
        this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel46.SizeF = new System.Drawing.SizeF(874.9999F, 60.00006F);
        this.xrLabel46.StylePriority.UseFont = false;
        this.xrLabel46.StylePriority.UseTextAlignment = false;
        this.xrLabel46.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        this.xrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DesvioCusto", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(1.000585F, 409.9998F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(875F, 50F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "xrLabel9";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel24
        // 
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0.0002422333F, 50.00003F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(874.9996F, 50F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseForeColor = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "Físico";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // pnlGraficoReceitaSuperior
        // 
        this.pnlGraficoReceitaSuperior.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36,
            this.xrLabel15,
            this.xrChart8,
            this.xrLabel22,
            this.xrChart3,
            this.xrLabel5,
            this.xrLabel16});
        this.pnlGraficoReceitaSuperior.Dpi = 254F;
        this.pnlGraficoReceitaSuperior.LocationFloat = new DevExpress.Utils.PointFloat(0F, 970F);
        this.pnlGraficoReceitaSuperior.Name = "pnlGraficoReceitaSuperior";
        this.pnlGraficoReceitaSuperior.SizeF = new System.Drawing.SizeF(1800F, 460F);
        // 
        // xrLabel36
        // 
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(925.0002F, 0.0001614888F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(875F, 50F);
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseForeColor = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = "Receita no Ano";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DesvioReceitaAno", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(924.0009F, 410.0003F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(874.9985F, 50F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "xrLabel15";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrChart8
        // 
        this.xrChart8.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart8.BackImage.Image")));
        this.xrChart8.BackImage.Stretch = true;
        this.xrChart8.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart8.DataMember = "Projetos";
        this.xrChart8.DataSource = this.ds;
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
        this.xrChart8.Diagram = xyDiagram3;
        this.xrChart8.Dpi = 254F;
        this.xrChart8.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart8.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart8.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart8.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart8.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart8.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart8.Legend.HorizontalIndent = 25;
        this.xrChart8.LocationFloat = new DevExpress.Utils.PointFloat(925F, 50F);
        this.xrChart8.Name = "xrChart8";
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
        this.xrChart8.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
        sideBySideBarSeriesLabel9.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart8.SeriesTemplate.Label = sideBySideBarSeriesLabel9;
        this.xrChart8.SizeF = new System.Drawing.SizeF(875F, 300F);
        this.xrChart8.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartNovos_CustomDrawSeriesPoint);
        this.xrChart8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel22
        // 
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0.0002422333F, 0F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(875F, 50F);
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseForeColor = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = "Receita Total";
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrChart3
        // 
        this.xrChart3.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart3.BackImage.Image")));
        this.xrChart3.BackImage.Stretch = true;
        this.xrChart3.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart3.DataMember = "Projetos";
        this.xrChart3.DataSource = this.ds;
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
        this.xrChart3.LocationFloat = new DevExpress.Utils.PointFloat(0.0002422333F, 50.00002F);
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
        this.xrChart3.SizeF = new System.Drawing.SizeF(875F, 300F);
        this.xrChart3.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_CustomDrawSeriesPoint);
        this.xrChart3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 350.0001F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(874.9984F, 60F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        // 
        // xrLabel16
        // 
        this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DesvioReceita", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 410.0002F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(874.9985F, 50F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "xrLabel9";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrlStatus
        // 
        this.xrlStatus.Dpi = 254F;
        this.xrlStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrlStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrlStatus.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrlStatus.Name = "xrlStatus";
        this.xrlStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrlStatus.SizeF = new System.Drawing.SizeF(1799F, 50F);
        this.xrlStatus.StylePriority.UseFont = false;
        this.xrlStatus.StylePriority.UseForeColor = false;
        this.xrlStatus.Text = "Desempenho";
        // 
        // xrChart6
        // 
        this.xrChart6.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart6.BackImage.Image")));
        this.xrChart6.BackImage.Stretch = true;
        this.xrChart6.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart6.DataMember = "Projetos";
        this.xrChart6.DataSource = this.ds;
        xyDiagram5.AxisX.Label.Visible = false;
        xyDiagram5.AxisX.MinorCount = 1;
        xyDiagram5.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram5.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram5.AxisX.Title.Text = "Fisico (%)";
        xyDiagram5.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram5.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
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
        this.xrChart6.Diagram = xyDiagram5;
        this.xrChart6.Dpi = 254F;
        this.xrChart6.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart6.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart6.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart6.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart6.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart6.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart6.Legend.HorizontalIndent = 25;
        this.xrChart6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
        this.xrChart6.Name = "xrChart6";
        series9.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel13.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel13.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel13.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series9.Label = sideBySideBarSeriesLabel13;
        series9.LegendText = "Realizado";
        series9.Name = "SerieRealizado";
        series9.ValueDataMembersSerializable = "PercentualFisicoRealizado";
        sideBySideBarSeriesView9.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
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
        series10.View = sideBySideBarSeriesView10;
        this.xrChart6.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series9,
        series10};
        sideBySideBarSeriesLabel15.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart6.SeriesTemplate.Label = sideBySideBarSeriesLabel15;
        this.xrChart6.SizeF = new System.Drawing.SizeF(875F, 300F);
        this.xrChart6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel45
        // 
        this.xrLabel45.Dpi = 254F;
        this.xrLabel45.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(0.0009689331F, 399.9999F);
        this.xrLabel45.Name = "xrLabel45";
        this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel45.SizeF = new System.Drawing.SizeF(874.9999F, 60.00006F);
        this.xrLabel45.StylePriority.UseFont = false;
        this.xrLabel45.StylePriority.UseTextAlignment = false;
        this.xrLabel45.Text = "(*) 100% refere-se a data fim do projeto.";
        this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // xrLabel23
        // 
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DesvioFisico", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0.001291911F, 460.0001F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(874.9995F, 49.99982F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.Text = "xrLabel9";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
        // pnlSuperiorAnaliseCritica
        // 
        this.pnlSuperiorAnaliseCritica.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel49,
            this.xrRichText2});
        this.pnlSuperiorAnaliseCritica.Dpi = 254F;
        this.pnlSuperiorAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1560F);
        this.pnlSuperiorAnaliseCritica.Name = "pnlSuperiorAnaliseCritica";
        this.pnlSuperiorAnaliseCritica.SizeF = new System.Drawing.SizeF(1800F, 100F);
        // 
        // xrLabel49
        // 
        this.xrLabel49.Dpi = 254F;
        this.xrLabel49.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel49.Name = "xrLabel49";
        this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel49.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel49.StylePriority.UseFont = false;
        this.xrLabel49.StylePriority.UseForeColor = false;
        this.xrLabel49.Text = "Análise Crítica";
        // 
        // xrRichText2
        // 
        this.xrRichText2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projetos.AnaliseCritica")});
        this.xrRichText2.Dpi = 254F;
        this.xrRichText2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.99969F);
        this.xrRichText2.Name = "xrRichText2";
        this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
        this.xrRichText2.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrRichText2.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblAnaliseCritica_EvaluateBinding);
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
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrPageInfo1.Format = "{0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1547F, 41.58F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(250F, 58.42F);
        this.xrPageInfo1.StylePriority.UseFont = false;
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
            this.xrLabel2,
            this.xrLine1,
            this.xrLabel1,
            this.picLogoEntidade,
            this.xrLabel6});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 310F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.ForeColor = System.Drawing.Color.Black;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 250F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Período: Novembro de 2012";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        this.xrLabel2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel2_BeforePrint);
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 300F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1800F, 10F);
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.99998F);
        this.xrLabel1.Multiline = true;
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1175F, 160F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Relatório de Acompanhamento \nde Projeto";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(1175F, 0F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(625F, 250F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.DataEmissaoRelatorio", "Emissão: {0:dd/MM/yyyy HH:mm:ss}")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1175F, 250F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(625F, 50F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
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
            this.pnlGraficos,
            this.pnlAnaliseCritica,
            this.xrSubreport2,
            this.xrLabel20});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 1680F;
        this.Detail1.Name = "Detail1";
        this.Detail1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.Detail1.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeObjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // pnlGraficos
        // 
        this.pnlGraficos.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlGraficoDespesa,
            this.xrLabel30,
            this.pnlGraficoReceita,
            this.xrChart4,
            this.xrLabel47,
            this.xrLabel34,
            this.xrLabel25});
        this.pnlGraficos.Dpi = 254F;
        this.pnlGraficos.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60.31252F);
        this.pnlGraficos.Name = "pnlGraficos";
        this.pnlGraficos.SizeF = new System.Drawing.SizeF(1800F, 1440F);
        // 
        // pnlGraficoDespesa
        // 
        this.pnlGraficoDespesa.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37,
            this.xrLabel33,
            this.xrChart10,
            this.xrLabel31,
            this.xrChart5,
            this.xrLabel48,
            this.xrLabel38});
        this.pnlGraficoDespesa.Dpi = 254F;
        this.pnlGraficoDespesa.LocationFloat = new DevExpress.Utils.PointFloat(0F, 510F);
        this.pnlGraficoDespesa.Name = "pnlGraficoDespesa";
        this.pnlGraficoDespesa.SizeF = new System.Drawing.SizeF(1800F, 460F);
        // 
        // xrLabel37
        // 
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(925.002F, 0F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(874.9984F, 50F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseForeColor = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "Despesa no Ano";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel33
        // 
        this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioCustoAno", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(925F, 409.9997F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(874.9996F, 50F);
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = "xrLabel33";
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrChart10
        // 
        this.xrChart10.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart10.BackImage.Image")));
        this.xrChart10.BackImage.Stretch = true;
        this.xrChart10.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart10.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart10.DataSource = this.ds;
        xyDiagram6.AxisX.Label.Visible = false;
        xyDiagram6.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram6.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram6.AxisX.Title.Text = "Despesa (R$)";
        xyDiagram6.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram6.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram6.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram6.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram6.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram6.AxisY.MinorCount = 1;
        xyDiagram6.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram6.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram6.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram6.AxisY.VisualRange.Auto = false;
        xyDiagram6.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram6.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram6.AxisY.WholeRange.Auto = false;
        xyDiagram6.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram6.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram6.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram6.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram6.Rotated = true;
        this.xrChart10.Diagram = xyDiagram6;
        this.xrChart10.Dpi = 254F;
        this.xrChart10.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart10.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart10.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart10.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart10.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart10.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart10.Legend.HorizontalIndent = 25;
        this.xrChart10.LocationFloat = new DevExpress.Utils.PointFloat(925F, 50F);
        this.xrChart10.Name = "xrChart10";
        series11.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel16.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel16.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel16.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel16.TextPattern = "{V:C2}";
        series11.Label = sideBySideBarSeriesLabel16;
        series11.LegendText = "Realizado";
        series11.Name = "SerieRealizado";
        series11.ValueDataMembersSerializable = "PercentualCustoRealDataNoAno";
        sideBySideBarSeriesView11.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series11.View = sideBySideBarSeriesView11;
        series12.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel17.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel17.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel17.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel17.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel17.TextPattern = "{V:C2}";
        series12.Label = sideBySideBarSeriesLabel17;
        series12.LegendText = "Previsto";
        series12.Name = "SeriePrevisto";
        series12.ValueDataMembersSerializable = "PercentualCustoLBDataNoAno";
        sideBySideBarSeriesView12.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView12.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series12.View = sideBySideBarSeriesView12;
        this.xrChart10.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series11,
        series12};
        sideBySideBarSeriesLabel18.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart10.SeriesTemplate.Label = sideBySideBarSeriesLabel18;
        this.xrChart10.SizeF = new System.Drawing.SizeF(874.9999F, 299.9998F);
        this.xrChart10.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartNovos_CustomDrawSeriesPoint);
        this.xrChart10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel31
        // 
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0.0002441406F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(874.9984F, 50F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseForeColor = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "Despesa Total";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrChart5
        // 
        this.xrChart5.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart5.BackImage.Image")));
        this.xrChart5.BackImage.Stretch = true;
        this.xrChart5.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart5.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart5.DataSource = this.ds;
        xyDiagram7.AxisX.Label.Visible = false;
        xyDiagram7.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram7.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram7.AxisX.Title.Text = "Despesa (R$)";
        xyDiagram7.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram7.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram7.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram7.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram7.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram7.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram7.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram7.AxisY.MinorCount = 1;
        xyDiagram7.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram7.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram7.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram7.AxisY.VisualRange.Auto = false;
        xyDiagram7.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram7.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram7.AxisY.WholeRange.Auto = false;
        xyDiagram7.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram7.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram7.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram7.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram7.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram7.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram7.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram7.Rotated = true;
        this.xrChart5.Diagram = xyDiagram7;
        this.xrChart5.Dpi = 254F;
        this.xrChart5.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart5.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart5.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart5.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart5.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart5.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart5.Legend.HorizontalIndent = 25;
        this.xrChart5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.00018F);
        this.xrChart5.Name = "xrChart5";
        series13.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel19.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel19.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel19.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel19.TextPattern = "{V:C2}";
        series13.Label = sideBySideBarSeriesLabel19;
        series13.LegendText = "Realizado";
        series13.Name = "SerieRealizado";
        series13.ValueDataMembersSerializable = "PercentualFinanceiroRealizado";
        sideBySideBarSeriesView13.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series13.View = sideBySideBarSeriesView13;
        series14.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel20.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel20.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel20.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel20.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel20.TextPattern = "{V:C2}";
        series14.Label = sideBySideBarSeriesLabel20;
        series14.LegendText = "Previsto";
        series14.Name = "SeriePrevisto";
        series14.ValueDataMembersSerializable = "PercentualFinanceiroPrevisto";
        sideBySideBarSeriesView14.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView14.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series14.View = sideBySideBarSeriesView14;
        this.xrChart5.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series13,
        series14};
        sideBySideBarSeriesLabel21.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart5.SeriesTemplate.Label = sideBySideBarSeriesLabel21;
        this.xrChart5.SizeF = new System.Drawing.SizeF(874.9999F, 299.9998F);
        this.xrChart5.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_CustomDrawSeriesPoint);
        this.xrChart5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel48
        // 
        this.xrLabel48.Dpi = 254F;
        this.xrLabel48.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(0F, 349.9999F);
        this.xrLabel48.Name = "xrLabel48";
        this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel48.SizeF = new System.Drawing.SizeF(875.0001F, 60F);
        this.xrLabel48.StylePriority.UseFont = false;
        this.xrLabel48.StylePriority.UseTextAlignment = false;
        this.xrLabel48.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        this.xrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // xrLabel38
        // 
        this.xrLabel38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioCusto", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(0F, 409.9998F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(874.9996F, 50F);
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "xrLabel38";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0.0008074443F, 50.00002F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(874.9996F, 50F);
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.StylePriority.UseForeColor = false;
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = "Físico";
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // pnlGraficoReceita
        // 
        this.pnlGraficoReceita.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel39,
            this.xrLabel17,
            this.xrChart9,
            this.xrLabel27,
            this.xrChart1,
            this.xrLabel28,
            this.xrLabel12});
        this.pnlGraficoReceita.Dpi = 254F;
        this.pnlGraficoReceita.LocationFloat = new DevExpress.Utils.PointFloat(0F, 970F);
        this.pnlGraficoReceita.Name = "pnlGraficoReceita";
        this.pnlGraficoReceita.SizeF = new System.Drawing.SizeF(1800F, 460F);
        // 
        // xrLabel39
        // 
        this.xrLabel39.Dpi = 254F;
        this.xrLabel39.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(925.0023F, 0.0001614888F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(874.998F, 50.00012F);
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.StylePriority.UseForeColor = false;
        this.xrLabel39.StylePriority.UseTextAlignment = false;
        this.xrLabel39.Text = "Receita no Ano";
        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel17
        // 
        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioReceitaAno", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(925F, 410F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(874.9985F, 49.99976F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "xrLabel17";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrChart9
        // 
        this.xrChart9.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart9.BackImage.Image")));
        this.xrChart9.BackImage.Stretch = true;
        this.xrChart9.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart9.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart9.DataSource = this.ds;
        xyDiagram8.AxisX.Label.Visible = false;
        xyDiagram8.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram8.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram8.AxisX.Title.Text = "Receita (R$)";
        xyDiagram8.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram8.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram8.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram8.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram8.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram8.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram8.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram8.AxisY.MinorCount = 1;
        xyDiagram8.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram8.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram8.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram8.AxisY.VisualRange.Auto = false;
        xyDiagram8.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram8.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram8.AxisY.WholeRange.Auto = false;
        xyDiagram8.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram8.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram8.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram8.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram8.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram8.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram8.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram8.Rotated = true;
        this.xrChart9.Diagram = xyDiagram8;
        this.xrChart9.Dpi = 254F;
        this.xrChart9.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart9.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart9.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart9.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart9.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart9.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart9.Legend.HorizontalIndent = 25;
        this.xrChart9.LocationFloat = new DevExpress.Utils.PointFloat(925F, 51F);
        this.xrChart9.Name = "xrChart9";
        series15.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel22.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel22.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel22.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel22.TextPattern = "{V:C2}";
        series15.Label = sideBySideBarSeriesLabel22;
        series15.LegendText = "Realizado";
        series15.Name = "SerieRealizado";
        series15.ValueDataMembersSerializable = "PercentualReceitaRealDataNoAno";
        sideBySideBarSeriesView15.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series15.View = sideBySideBarSeriesView15;
        series16.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel23.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel23.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel23.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel23.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel23.TextPattern = "{V:C2}";
        series16.Label = sideBySideBarSeriesLabel23;
        series16.LegendText = "Previsto";
        series16.Name = "SeriePrevisto";
        series16.ValueDataMembersSerializable = "PercentualReceitaLBDataNoAno";
        sideBySideBarSeriesView16.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView16.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series16.View = sideBySideBarSeriesView16;
        this.xrChart9.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series15,
        series16};
        sideBySideBarSeriesLabel24.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart9.SeriesTemplate.Label = sideBySideBarSeriesLabel24;
        this.xrChart9.SizeF = new System.Drawing.SizeF(874.9998F, 300.0002F);
        this.xrChart9.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartNovos_CustomDrawSeriesPoint);
        this.xrChart9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel27
        // 
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(0.0002422333F, 0F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(874.998F, 50.00012F);
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseForeColor = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "Receita Total";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrChart1
        // 
        this.xrChart1.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart1.BackImage.Image")));
        this.xrChart1.BackImage.Stretch = true;
        this.xrChart1.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart1.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart1.DataSource = this.ds;
        xyDiagram9.AxisX.Label.Visible = false;
        xyDiagram9.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram9.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram9.AxisX.Title.Text = "Receita (R$)";
        xyDiagram9.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram9.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram9.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram9.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram9.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram9.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram9.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram9.AxisY.MinorCount = 1;
        xyDiagram9.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram9.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram9.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram9.AxisY.VisualRange.Auto = false;
        xyDiagram9.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram9.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram9.AxisY.WholeRange.Auto = false;
        xyDiagram9.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram9.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram9.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram9.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram9.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram9.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram9.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram9.Rotated = true;
        this.xrChart1.Diagram = xyDiagram9;
        this.xrChart1.Dpi = 254F;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart1.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart1.Legend.HorizontalIndent = 25;
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.99998F);
        this.xrChart1.Name = "xrChart1";
        series17.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel25.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel25.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel25.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel25.TextPattern = "{V:C2}";
        series17.Label = sideBySideBarSeriesLabel25;
        series17.LegendText = "Realizado";
        series17.Name = "SerieRealizado";
        series17.ValueDataMembersSerializable = "PercentualReceitaRealizado";
        sideBySideBarSeriesView17.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series17.View = sideBySideBarSeriesView17;
        series18.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel26.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel26.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel26.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel26.TextColor = System.Drawing.Color.Black;
        sideBySideBarSeriesLabel26.TextPattern = "{V:C2}";
        series18.Label = sideBySideBarSeriesLabel26;
        series18.LegendText = "Previsto";
        series18.Name = "SeriePrevisto";
        series18.ValueDataMembersSerializable = "PercentualReceitaPrevisto";
        sideBySideBarSeriesView18.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView18.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series18.View = sideBySideBarSeriesView18;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series17,
        series18};
        sideBySideBarSeriesLabel27.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel27;
        this.xrChart1.SizeF = new System.Drawing.SizeF(874.9998F, 300.0002F);
        this.xrChart1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_CustomDrawSeriesPoint);
        this.xrChart1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel28
        // 
        this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioReceita", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 410F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(874.9985F, 49.99976F);
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "xrLabel9";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 353F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(875.0001F, 57F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "(**) 100% refere-se ao acumulado do início do projeto até dezembro do ano corrent" +
"e.";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // xrChart4
        // 
        this.xrChart4.BackImage.Image = ((System.Drawing.Image)(resources.GetObject("xrChart4.BackImage.Image")));
        this.xrChart4.BackImage.Stretch = true;
        this.xrChart4.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart4.DataMember = "Projetos.Projetos_Projetos";
        this.xrChart4.DataSource = this.ds;
        xyDiagram10.AxisX.Label.Visible = false;
        xyDiagram10.AxisX.MinorCount = 1;
        xyDiagram10.AxisX.Title.Alignment = System.Drawing.StringAlignment.Far;
        xyDiagram10.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8.5F);
        xyDiagram10.AxisX.Title.Text = "Fisico (%)";
        xyDiagram10.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram10.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram10.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram10.AxisY.DateTimeScaleOptions.AutoGrid = false;
        xyDiagram10.AxisY.DateTimeScaleOptions.GridSpacing = 25D;
        xyDiagram10.AxisY.Label.TextPattern = "{V:N0}%";
        xyDiagram10.AxisY.MinorCount = 1;
        xyDiagram10.AxisY.NumericScaleOptions.AutoGrid = false;
        xyDiagram10.AxisY.NumericScaleOptions.GridSpacing = 25D;
        xyDiagram10.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram10.AxisY.VisualRange.Auto = false;
        xyDiagram10.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram10.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram10.AxisY.WholeRange.Auto = false;
        xyDiagram10.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram10.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram10.DefaultPane.BackColor = System.Drawing.Color.Transparent;
        xyDiagram10.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram10.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram10.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram10.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
        xyDiagram10.Rotated = true;
        this.xrChart4.Diagram = xyDiagram10;
        this.xrChart4.Dpi = 254F;
        this.xrChart4.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart4.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart4.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart4.Legend.BackColor = System.Drawing.Color.Transparent;
        this.xrChart4.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart4.Legend.Direction = DevExpress.XtraCharts.LegendDirection.RightToLeft;
        this.xrChart4.Legend.HorizontalIndent = 25;
        this.xrChart4.LocationFloat = new DevExpress.Utils.PointFloat(0.001291911F, 100F);
        this.xrChart4.Name = "xrChart4";
        series19.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel28.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel28.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel28.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series19.Label = sideBySideBarSeriesLabel28;
        series19.LegendText = "Realizado";
        series19.Name = "SerieRealizado";
        series19.ValueDataMembersSerializable = "PercentualFisicoRealizado";
        sideBySideBarSeriesView19.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series19.View = sideBySideBarSeriesView19;
        series20.ArgumentDataMember = "CodigoObjeto";
        sideBySideBarSeriesLabel29.BackColor = System.Drawing.Color.Transparent;
        sideBySideBarSeriesLabel29.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel29.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        sideBySideBarSeriesLabel29.TextColor = System.Drawing.Color.Black;
        series20.Label = sideBySideBarSeriesLabel29;
        series20.LegendText = "Previsto";
        series20.Name = "SeriePrevisto";
        series20.ValueDataMembersSerializable = "PercentualFisicoPrevisto";
        sideBySideBarSeriesView20.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView20.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series20.View = sideBySideBarSeriesView20;
        this.xrChart4.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series19,
        series20};
        sideBySideBarSeriesLabel30.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart4.SeriesTemplate.Label = sideBySideBarSeriesLabel30;
        this.xrChart4.SizeF = new System.Drawing.SizeF(874.9998F, 300.0003F);
        this.xrChart4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.chart_BeforePrint);
        // 
        // xrLabel47
        // 
        this.xrLabel47.Dpi = 254F;
        this.xrLabel47.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(0.001291911F, 400.0003F);
        this.xrLabel47.Name = "xrLabel47";
        this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel47.SizeF = new System.Drawing.SizeF(874.9998F, 60F);
        this.xrLabel47.StylePriority.UseFont = false;
        this.xrLabel47.StylePriority.UseTextAlignment = false;
        this.xrLabel47.Text = "(*) 100% refere-se a data fim do projeto.";
        this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // xrLabel34
        // 
        this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.Projetos_Projetos.DesvioFisico", "O realizado encontra-se a {0:n0}% do previsto.")});
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(0.001372655F, 460.0001F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(874.9998F, 49.99988F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = "xrLabel9";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel25
        // 
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(1800F, 50.00012F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseForeColor = false;
        this.xrLabel25.Text = "Desempenho";
        // 
        // pnlAnaliseCritica
        // 
        this.pnlAnaliseCritica.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21,
            this.xrRichText1});
        this.pnlAnaliseCritica.Dpi = 254F;
        this.pnlAnaliseCritica.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1560F);
        this.pnlAnaliseCritica.Name = "pnlAnaliseCritica";
        this.pnlAnaliseCritica.SizeF = new System.Drawing.SizeF(1800F, 100F);
        // 
        // xrLabel21
        // 
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseForeColor = false;
        this.xrLabel21.Text = "Análise Crítica";
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Projetos.Projetos_Projetos.AnaliseCritica")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrRichText1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblAnaliseCritica_EvaluateBinding);
        // 
        // xrSubreport2
        // 
        this.xrSubreport2.Dpi = 254F;
        this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.00002F);
        this.xrSubreport2.Name = "xrSubreport2";
        this.xrSubreport2.ReportSource = new relSubreportProdutos();
        this.xrSubreport2.SizeF = new System.Drawing.SizeF(1800F, 10.3125F);
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
        this.xrLabel20.Text = "Entregas";
        this.xrLabel20.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel20_BeforePrint);
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
        // xrSubreportLegenda
        // 
        this.xrSubreportLegenda.Dpi = 254F;
        this.xrSubreportLegenda.LocationFloat = new DevExpress.Utils.PointFloat(250.0014F, 0F);
        this.xrSubreportLegenda.Name = "xrSubreportLegenda";
        this.xrSubreportLegenda.ReportSource = new rel_LegendasFisicoFinanceiro();
        this.xrSubreportLegenda.SizeF = new System.Drawing.SizeF(1549F, 140F);
        // 
        // panelInfoLegenda
        // 
        this.panelInfoLegenda.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel4,
            this.xrLabel7,
            this.xrLabel14});
        this.panelInfoLegenda.Dpi = 254F;
        this.panelInfoLegenda.LocationFloat = new DevExpress.Utils.PointFloat(0.001372655F, 0F);
        this.panelInfoLegenda.Name = "panelInfoLegenda";
        this.panelInfoLegenda.SizeF = new System.Drawing.SizeF(250F, 140F);
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(130F, 100F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseForeColor = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "Receita";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.ForeColor = System.Drawing.Color.Blue;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(130F, 50F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseForeColor = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Despesa";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrPictureBox12,
            this.panelInfoLegenda,
            this.xrSubreportLegenda});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 360F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0.001372655F, 320F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1796.998F, 40F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrLabel18.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel18_BeforePrint);
        // 
        // xrPictureBox12
        // 
        this.xrPictureBox12.Dpi = 254F;
        this.xrPictureBox12.ImageUrl = "~\\espacoCliente\\Rodape01BoletimStatus.png";
        this.xrPictureBox12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 145F);
        this.xrPictureBox12.Name = "xrPictureBox12";
        this.xrPictureBox12.SizeF = new System.Drawing.SizeF(1800F, 175F);
        this.xrPictureBox12.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // rel_BoletimStatusNacional
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
        ((System.ComponentModel.ISupportInitialize)(this.xrChart7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart8)).EndInit();
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
        ((System.ComponentModel.ISupportInitialize)(this.xrChart6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel19)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel20)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel21)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel22)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel23)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel24)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel25)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel26)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel27)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel28)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView19)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series19)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel29)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView20)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series20)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel30)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
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
            "<div style=\"font-family: {1}; font-size: {2}pt;\">{0}</div><br /><br />"
            , strValue, fontFamilyAnaliseCritica, fontSizeAnaliseCritica);
        e.Value = strValue;
    }

    private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRSubreport sub = (XRSubreport)sender;
        int codigoProjeto = Convert.ToInt32(
            sub.Report.GetCurrentColumnValue("CodigoObjeto"));
        ((relSubreportProdutos)sub.ReportSource).CodProjeto.Value = codigoProjeto;
    }

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

    private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        var projeto = ds.Projetos.First();
        string periodo = string.Format("{0:d} a {1:d}",
            projeto.DataInicioPeriodoRelatorio,
            projeto.DataTerminoPeriodoRelatorio);
        label.Text = string.Format("Período: {0:}", UppercaseFirst(periodo));
    }

    private void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XtraReportBase report = ((XRControl)sender).Report;
        string strCustoReceita = e.Series.ValueDataMembersSerializable.Contains("Financeiro") ? "Custo" : "Receita";
        string strPrevistoRealizado = e.Series.Name == "SerieRealizado" ? "Realizado" : "Previsto";
        string nomeColuna = string.Format("Valor{0}{1}Original", strCustoReceita, strPrevistoRealizado);
        object valor = report.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);
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

    #endregion

    private bool VerificaIncluiGraficoReceita()
    {
        string comandoSql = string.Format(@"
DECLARE @CodigoStatusReport INT
    SET @CodigoStatusReport = {0}

 SELECT CASE WHEN EXISTS(SELECT 1 
                          FROM GraficoReceitaNoRAP AS gr 
                         WHERE gr.CodigoObjeto = sr.CodigoObjeto
                           AND gr.CodigoTipoAssociacaoObjeto = sr.CodigoTipoAssociacaoObjeto
                           AND gr.CodigoModeloStatusReport = sr.CodigoModeloStatusReport
                           AND gr.DataDesativacao IS NULL) 
            THEN 'S'
            ELSE 'N'
        END AS IndicaIncluiGraficoReceita
   FROM StatusReport AS sr
  WHERE sr.CodigoStatusReport = @CodigoStatusReport", codigoStatusReport);
        DataSet ds = cDados.getDataSet(comandoSql);
        string indicaIncluiGraficoReceita =
            ds.Tables[0].Rows[0].Field<string>("IndicaIncluiGraficoReceita");

        return indicaIncluiGraficoReceita.Equals("S");
    }

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DetailBand detail = (DetailBand)sender;
        IEnumerable<XRPanel> controles = detail.AllControls<XRPanel>();
        XRPanel pnlContainer = controles.Single(p => p.Name.Contains("Graficos"));
        XRPanel pnlGraficoDespesa = controles.Single(p => p.Name.Contains("Despesa"));
        XRPanel pnlGraficoReceita = controles.SingleOrDefault(p => p.Name.Contains("Receita"));
        XRPanel pnlAnaliseCritica = controles.Single(p => p.Name.Contains("AnaliseCritica"));
        bool graficosSuperior = pnlContainer.Name.Contains("Superior");

        DataRowView drv = (DataRowView)detail.Report.GetCurrentRow();
        decimal? valorReceitaPrev = drv.Row.Field<decimal?>("PercentualReceitaPrevisto");
        decimal? valorReceitaReal = drv.Row.Field<decimal?>("PercentualReceitaRealizado");
        bool existeValoresReceita =
            (valorReceitaPrev.HasValue || valorReceitaReal.HasValue) &&
            (valorReceitaPrev.Value != 0 || valorReceitaReal.Value != 0);
        bool mostraGraficosReceita =
            existeValoresReceita &&
            VerificaIncluiGraficoReceita();

        const int INT_alturaPanelGraficoReceita = 460;
        if (pnlGraficoReceita == null)
        {
            if (mostraGraficosReceita)
            {
                pnlGraficoReceita = graficosSuperior ?
                    this.pnlGraficoReceitaSuperior :
                    this.pnlGraficoReceita;
                detail.HeightF += INT_alturaPanelGraficoReceita;
                float x = 0;
                float y = pnlContainer.LocationF.Y + pnlContainer.SizeF.Height +
                    INT_alturaPanelGraficoReceita + 25;
                pnlAnaliseCritica.LocationF = new PointF(x, y);
                pnlContainer.HeightF += INT_alturaPanelGraficoReceita;
                pnlContainer.Controls.Add(pnlGraficoReceita);
                x = 0;
                y = pnlGraficoDespesa.LocationF.Y + pnlGraficoDespesa.SizeF.Height;
                pnlGraficoReceita.LocationF = new PointF(x, y);
            }
        }
        else
        {
            if (!mostraGraficosReceita)
            {
                pnlContainer.Controls.Remove(pnlGraficoReceita);
                pnlContainer.HeightF -= INT_alturaPanelGraficoReceita;
                float x = 0;
                float y = pnlContainer.LocationF.Y + pnlContainer.SizeF.Height + 25;
                pnlAnaliseCritica.LocationF = new PointF(x, y);
                detail.HeightF -= INT_alturaPanelGraficoReceita;
            }
        }

        #region Comentario
        /*DetailBand detail = (DetailBand)sender;
        bool indicaObjetoSuperior = detail.Report == this;
        XRPanel panel = indicaObjetoSuperior ? pnlGraficosSuperior : pnlGraficos;
        XRPanel pnlReceita = indicaObjetoSuperior ? pnlGraficoReceitaSuperior : pnlGraficoReceita;
        XRPanel pnlAnaliseCritica = indicaObjetoSuperior ? this.pnlSuperiorAnaliseCritica : this.pnlAnaliseCritica;
        //XRPanel pnlInfoLegenda = indicaObjetoSuperior ? panelInfoLegendaSuperior : panelInfoLegenda;
        //XRSubreport subReport = indicaObjetoSuperior ? xrSubreportLegendaSuperior : xrSubreportLegenda;
        object valorReceitaPrev =
            panel.Report.GetCurrentColumnValue("PercentualReceitaPrevisto");
        object valorReceitaReal =
            panel.Report.GetCurrentColumnValue("PercentualReceitaRealizado");
        bool existeValoresReceita =
            !(Convert.IsDBNull(valorReceitaPrev) || ((decimal)valorReceitaPrev) == 0) ||
            !(Convert.IsDBNull(valorReceitaReal) || ((decimal)valorReceitaReal) == 0);
        if (existeValoresReceita && VerificaIncluiGraficoReceita())
        {
            if (!panel.Controls.Contains(pnlReceita))
            {
                panel.Band.HeightF = 1230;
                panel.HeightF = 840;
                //pnlInfoLegenda.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
                //subReport.LocationF = new PointF(250, panel.LocationF.Y + panel.HeightF);
                pnlAnaliseCritica.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
                panel.Controls.Add(pnlReceita);
                pnlReceita.LocationF = new PointF(0, 510);
            }
        }
        else if (panel.Controls.Contains(pnlReceita))
        {
            panel.Controls.Remove(pnlReceita);
            panel.HeightF = 380;
            //pnlInfoLegenda.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
            //subReport.LocationF = new PointF(250, panel.LocationF.Y + panel.HeightF);
            pnlAnaliseCritica.LocationF = new PointF(0, panel.LocationF.Y + panel.HeightF);
            panel.Band.HeightF = 770;
        }*/

        //XRPanel panel = (XRPanel)sender;
        //XRPanel pnlReceita = panel.Report == this ? pnlGraficoReceitaSuperior : pnlGraficoReceita;
        //XRPanel pnlLegendas = panel.Report == this ? pnlLegendasSuperior : this.pnlLegendas;
        //object valorReceitaPrev = panel.Report.GetCurrentColumnValue("ValorReceitaPrevisto");
        //object valorReceitaReal = panel.Report.GetCurrentColumnValue("ValorReceitaRealizado");
        //bool existeValoresReceita =
        //    !(Convert.IsDBNull(valorReceitaPrev) || ((decimal)valorReceitaPrev) == 0) ||
        //    !(Convert.IsDBNull(valorReceitaReal) || ((decimal)valorReceitaReal) == 0);
        //if (existeValoresReceita)
        //{
        //    if (!panel.Controls.Contains(pnlReceita))
        //    {
        //        panel.Band.HeightF = 1250;
        //        panel.HeightF = 1000;
        //        pnlLegendas.LocationF = new PointF(0, 910);
        //        panel.Controls.Add(pnlReceita);
        //        pnlReceita.LocationF = new PointF(0, 510);
        //    }
        //}
        //else
        //{
        //    if (panel.Controls.Contains(pnlReceita))
        //    {
        //        panel.Controls.Remove(pnlReceita);
        //        pnlLegendas.LocationF = new PointF(0, 510);
        //        panel.HeightF = 600;
        //        panel.Band.HeightF = 850;
        //    }
        //} 
        #endregion
    }

    private void xrLabel18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        DataSet dsParams = cDados.getParametrosSistema("TextoRodape_RAP");
        if (dsParams.Tables[0].Rows.Count > 0)
            label.Text = dsParams.Tables[0].Rows[0]["TextoRodape_RAP"] as string;
    }

    private void chartNovos_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        XRChart chart = (XRChart)sender;
        string nomeColuna = e.Series.ValueDataMembersSerializable.Replace(
            "Percentual", string.Empty);
        object valor = chart.Report.GetCurrentColumnValue(nomeColuna);

        e.LabelText = string.Format("{0:c2}", valor);
    }

    private void xrLabel20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel lbl = (XRLabel)sender;
        int codigoProjeto = Convert.ToInt32(
            lbl.Report.GetCurrentColumnValue("CodigoObjeto"));

        DataRow[] dr = dsGlobal.Tables[3].Select("[CodigoProjeto] = " + codigoProjeto);
        if (dr.Length == 0)
        {
            lbl.Visible = false;
            e.Cancel = true;
        }
    }

    //private void pnlGraficos_SizeChanged(object sender, ChangeEventArgs e)
    //{
    //    XRPanel pnlContainer = (XRPanel)sender;
    //    XRPanel pnlAnaliseCritica = pnlContainer.Parent.AllControls<XRPanel>().
    //        Single(pnl => pnl.Name.Contains("AnaliseCritica"));
    //    float x = 0;
    //    float y = pnlContainer.LocationF.X + pnlContainer.SizeF.Height + 25;
    //    pnlAnaliseCritica.LocationF = new PointF(x, y);
    //}

}
