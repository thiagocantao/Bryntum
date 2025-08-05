using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_CAPA_RAPU
/// </summary>
public class rel_CAPA_RAPU : DevExpress.XtraReports.UI.XtraReport
{
    private int codigoUsuarioLogado;
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPictureBox imgLogoUnidade;
    private DevExpress.XtraReports.Parameters.Parameter pNomeUnidade;
    private XRLabel lblTituloRelatorio;
    private dados cDados;
    private DevExpress.XtraReports.Parameters.Parameter pCodigoStatusReport;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_CAPA_RAPU(int codigoUsuarioLogado)
    {
        this.codigoUsuarioLogado = codigoUsuarioLogado;
        InitializeComponent();
        InitData();
    }

    private void InitData()
    {

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
        string resourceFileName = "rel_CAPA_RAPU.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblTituloRelatorio = new DevExpress.XtraReports.UI.XRLabel();
        this.pNomeUnidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.imgLogoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.pCodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTituloRelatorio,
            this.imgLogoUnidade});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1731.934F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblTituloRelatorio
        // 
        this.lblTituloRelatorio.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pNomeUnidade, "Text", "")});
        this.lblTituloRelatorio.Dpi = 254F;
        this.lblTituloRelatorio.Font = new System.Drawing.Font("Calibri", 40F);
        this.lblTituloRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1193.767F);
        this.lblTituloRelatorio.Name = "lblTituloRelatorio";
        this.lblTituloRelatorio.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTituloRelatorio.SizeF = new System.Drawing.SizeF(1901F, 366.0417F);
        this.lblTituloRelatorio.StylePriority.UseFont = false;
        this.lblTituloRelatorio.StylePriority.UseTextAlignment = false;
        this.lblTituloRelatorio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // pNomeUnidade
        // 
        this.pNomeUnidade.Name = "pNomeUnidade";
        this.pNomeUnidade.Visible = false;
        // 
        // imgLogoUnidade
        // 
        this.imgLogoUnidade.Dpi = 254F;
        this.imgLogoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(635.91F, 306.7F);
        this.imgLogoUnidade.Name = "imgLogoUnidade";
        this.imgLogoUnidade.SizeF = new System.Drawing.SizeF(595.6425F, 260.5284F);
        this.imgLogoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
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
        this.BottomMargin.HeightF = 50F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // pCodigoStatusReport
        // 
        this.pCodigoStatusReport.Name = "pCodigoStatusReport";
        this.pCodigoStatusReport.Type = typeof(int);
        this.pCodigoStatusReport.ValueInfo = "0";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 111.125F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.xrPageInfo1.Format = "{0:\'Brasilia-DF,\'  dd \'de\' MMMM \'de\' yyyy}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 26.35254F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(1901F, 58.41991F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // rel_CAPA_RAPU
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageFooter});
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(101, 98, 0, 50);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pNomeUnidade,
            this.pCodigoStatusReport});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "17.2";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.rel_CAPA_RAPU_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void rel_CAPA_RAPU_DataSourceDemanded(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Format(
            "exec p_rel_BoletimQuinzenalRAPU {0}", pCodigoStatusReport.Value);

        DataSet ds = cDados.getDataSet(comandoSql);

        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[7]))
        {
            nomeUnidade = "Carteira de Projetos - " + ds.Tables[7].Rows[0][0].ToString();
        }
        pNomeUnidade.Value = nomeUnidade;
        imgLogoUnidade.Image = ObtemLogoUnidade(codigoUsuarioLogado);
    }
}
