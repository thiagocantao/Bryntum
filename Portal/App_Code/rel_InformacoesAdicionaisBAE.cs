using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rel_InformacoesAdicionaisBAE
/// </summary>
public class rel_InformacoesAdicionaisBAE : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRRichText rtDestaquesMes;
    private XRLabel xrLabel16;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel18;
    private XRPictureBox xrPictureBox7;
    public DevExpress.XtraReports.Parameters.Parameter pDestaquesMes;
    public DevExpress.XtraReports.Parameters.Parameter pTextoRodape;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_InformacoesAdicionaisBAE()
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
        //string resourceFileName = "rel_InformacoesAdicionaisBAE.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_InformacoesAdicionaisBAE.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.rtDestaquesMes = new DevExpress.XtraReports.UI.XRRichText();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPictureBox7 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.pDestaquesMes = new DevExpress.XtraReports.Parameters.Parameter();
        this.pTextoRodape = new DevExpress.XtraReports.Parameters.Parameter();
        ((System.ComponentModel.ISupportInitialize)(this.rtDestaquesMes)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtDestaquesMes,
            this.xrLabel16});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 125F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.Text = "Informações Adicionais - Destaques do Mês";
        // 
        // rtDestaquesMes
        // 
        this.rtDestaquesMes.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pDestaquesMes, "Html", "")});
        this.rtDestaquesMes.Dpi = 254F;
        this.rtDestaquesMes.Font = new System.Drawing.Font("Verdana", 10F);
        this.rtDestaquesMes.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
        this.rtDestaquesMes.Name = "rtDestaquesMes";
        this.rtDestaquesMes.SerializableRtfString = resources.GetString("rtDestaquesMes.SerializableRtfString");
        this.rtDestaquesMes.SizeF = new System.Drawing.SizeF(1900F, 50F);
        this.rtDestaquesMes.StylePriority.UseBorders = false;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrPictureBox7});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 215F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPictureBox7
        // 
        this.xrPictureBox7.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
        this.xrPictureBox7.Dpi = 254F;
        this.xrPictureBox7.ImageUrl = "~\\espacoCliente\\Rodape01BoletimStatus.png";
        this.xrPictureBox7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox7.Name = "xrPictureBox7";
        this.xrPictureBox7.SizeF = new System.Drawing.SizeF(1900F, 175F);
        this.xrPictureBox7.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel18
        // 
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pTextoRodape, "Text", "")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 6.5F);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 175F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1900F, 40F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // pDestaquesMes
        // 
        this.pDestaquesMes.Name = "pDestaquesMes";
        this.pDestaquesMes.Visible = false;
        // 
        // pTextoRodape
        // 
        this.pTextoRodape.Name = "pTextoRodape";
        this.pTextoRodape.Visible = false;
        // 
        // rel_InformacoesAdicionaisBAE
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageFooter});
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pDestaquesMes,
            this.pTextoRodape});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.rtDestaquesMes)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
