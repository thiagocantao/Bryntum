using DevExpress.XtraReports.UI;
using System.Drawing;

/// <summary>
/// Summary description for rel_LegendasFisicoFinanceiro
/// </summary>
public class rel_LegendasFisicoFinanceiro : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRPanel xrPanelFisico;
    private DsBoletimStatus ds;
    private XRPanel xrPanelFinanceiro;
    private XRLabel xrLabelFinanceiro;
    private XRLabel xrLabelFisico;
    private XRLabel xrLabelReceita;
    private XRPanel xrPanelReceita;
    private XRPanel xrPanel1;
    public DevExpress.XtraReports.Parameters.Parameter pMostarReceita;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_LegendasFisicoFinanceiro()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
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
        string resourceFileName = "rel_LegendasFisicoFinanceiro.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabelReceita = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanelReceita = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabelFinanceiro = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabelFisico = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanelFinanceiro = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanelFisico = new DevExpress.XtraReports.UI.XRPanel();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ds = new DsBoletimStatus();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.pMostarReceita = new DevExpress.XtraReports.Parameters.Parameter();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelReceita,
            this.xrPanelReceita,
            this.xrLabelFinanceiro,
            this.xrLabelFisico,
            this.xrPanelFinanceiro,
            this.xrPanelFisico});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 140F;
        this.Detail.KeepTogether = true;
        this.Detail.MultiColumn.ColumnCount = 4;
        this.Detail.MultiColumn.ColumnWidth = 375F;
        this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabelReceita
        // 
        this.xrLabelReceita.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LegendaDesempenho.DescricaoFaixaReceita")});
        this.xrLabelReceita.Dpi = 254F;
        this.xrLabelReceita.Font = new System.Drawing.Font("Verdana", 6F);
        this.xrLabelReceita.ForeColor = System.Drawing.Color.Blue;
        this.xrLabelReceita.LocationFloat = new DevExpress.Utils.PointFloat(35F, 100F);
        this.xrLabelReceita.Name = "xrLabelReceita";
        this.xrLabelReceita.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelReceita.SizeF = new System.Drawing.SizeF(340F, 40F);
        this.xrLabelReceita.StylePriority.UseFont = false;
        this.xrLabelReceita.StylePriority.UseForeColor = false;
        this.xrLabelReceita.StylePriority.UseTextAlignment = false;
        this.xrLabelReceita.Text = "xrLabelReceita";
        this.xrLabelReceita.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPanelReceita
        // 
        this.xrPanelReceita.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "LegendaDesempenho.CorFaixaReceita")});
        this.xrPanelReceita.Dpi = 254F;
        this.xrPanelReceita.LocationFloat = new DevExpress.Utils.PointFloat(0F, 105F);
        this.xrPanelReceita.Name = "xrPanelReceita";
        this.xrPanelReceita.SizeF = new System.Drawing.SizeF(30F, 30F);
        this.xrPanelReceita.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.panel_EvaluateBinding);
        this.xrPanelReceita.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPanelReceita_BeforePrint);
        // 
        // xrLabelFinanceiro
        // 
        this.xrLabelFinanceiro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LegendaDesempenho.DescricaoFaixaFinanceiro")});
        this.xrLabelFinanceiro.Dpi = 254F;
        this.xrLabelFinanceiro.Font = new System.Drawing.Font("Verdana", 6F);
        this.xrLabelFinanceiro.ForeColor = System.Drawing.Color.Blue;
        this.xrLabelFinanceiro.LocationFloat = new DevExpress.Utils.PointFloat(35F, 50F);
        this.xrLabelFinanceiro.Name = "xrLabelFinanceiro";
        this.xrLabelFinanceiro.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelFinanceiro.SizeF = new System.Drawing.SizeF(340F, 40F);
        this.xrLabelFinanceiro.StylePriority.UseFont = false;
        this.xrLabelFinanceiro.StylePriority.UseForeColor = false;
        this.xrLabelFinanceiro.StylePriority.UseTextAlignment = false;
        this.xrLabelFinanceiro.Text = "De 110.00% a 99999.99%";
        this.xrLabelFinanceiro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabelFisico
        // 
        this.xrLabelFisico.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LegendaDesempenho.DescricaoFaixaFisico")});
        this.xrLabelFisico.Dpi = 254F;
        this.xrLabelFisico.Font = new System.Drawing.Font("Verdana", 6F);
        this.xrLabelFisico.ForeColor = System.Drawing.Color.Blue;
        this.xrLabelFisico.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
        this.xrLabelFisico.Name = "xrLabelFisico";
        this.xrLabelFisico.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabelFisico.SizeF = new System.Drawing.SizeF(340F, 40F);
        this.xrLabelFisico.StylePriority.UseFont = false;
        this.xrLabelFisico.StylePriority.UseForeColor = false;
        this.xrLabelFisico.StylePriority.UseTextAlignment = false;
        this.xrLabelFisico.Text = "De 110.00% a 99999.99%";
        this.xrLabelFisico.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPanelFinanceiro
        // 
        this.xrPanelFinanceiro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "LegendaDesempenho.CorFaixaFinanceiro")});
        this.xrPanelFinanceiro.Dpi = 254F;
        this.xrPanelFinanceiro.LocationFloat = new DevExpress.Utils.PointFloat(0F, 55F);
        this.xrPanelFinanceiro.Name = "xrPanelFinanceiro";
        this.xrPanelFinanceiro.SizeF = new System.Drawing.SizeF(30F, 30F);
        this.xrPanelFinanceiro.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.panel_EvaluateBinding);
        // 
        // xrPanelFisico
        // 
        this.xrPanelFisico.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "LegendaDesempenho.CorFaixaFisico")});
        this.xrPanelFisico.Dpi = 254F;
        this.xrPanelFisico.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);
        this.xrPanelFisico.Name = "xrPanelFisico";
        this.xrPanelFisico.SizeF = new System.Drawing.SizeF(30F, 30F);
        this.xrPanelFisico.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.panel_EvaluateBinding);
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 0F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 0F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // ds
        // 
        this.ds.DataSetName = "DsBoletimStatus";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrPanel1
        // 
        this.xrPanel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "LegendaDesempenho.CorFaixaFisico")});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(30F, 30F);
        this.xrPanel1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.panel_EvaluateBinding);
        // 
        // pMostarReceita
        // 
        this.pMostarReceita.Name = "pMostarReceita";
        this.pMostarReceita.Type = typeof(bool);
        this.pMostarReceita.ValueInfo = "True";
        this.pMostarReceita.Visible = false;
        // 
        // rel_LegendasFisicoFinanceiro
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.DataMember = "LegendaDesempenho";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 6.5F, System.Drawing.FontStyle.Bold);
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pMostarReceita});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "17.2";
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void panel_EvaluateBinding(object sender, BindingEventArgs e)
    {
        XRPanel panel = (XRPanel)sender;
        Color cor = ObtemCorPeloNome(e.Value as string);
        panel.BackColor = cor;
    }

    private Color ObtemCorPeloNome(string strCor)
    {
        switch ((strCor ?? string.Empty).ToLower())
        {
            case "amarelo":
                return Color.Yellow;
            case "branco":
                return Color.White;
            case "laranja":
                return Color.Orange;
            case "verde":
                return Color.Green;
            case "vermelho":
                return Color.Red;
            default:
                return Color.Transparent;
        }
    }

    private void xrPanelReceita_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        bool mostarReceitaValue = (bool)pMostarReceita.Value;
        xrPanelReceita.Visible = mostarReceitaValue;
        xrLabelReceita.Visible = mostarReceitaValue;
    }
}
