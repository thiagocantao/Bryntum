using DevExpress.XtraReports.UI;
using System.Threading;

/// <summary>
/// Summary description for relIdentidade4
/// </summary>
public class relIdentidade4 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private ReportHeaderBand ReportHeader;
    private XRLabel lblNomeMapa;
    private XRLabel xrLabel5;
    private XRLabel lblMissao;
    private XRLabel xrLabel7;
    private XRLabel lblVisao;
    private XRLabel lblCrencasValores;
    private GroupHeaderBand GroupHeader1;
    private GroupHeaderBand GroupHeader2;
    private GroupHeaderBand GroupHeader3;

    private DevExpress.XtraReports.Parameters.Parameter pmissao = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pvisao = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pcrencasvalores = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeMapa = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pathArquivo = new DevExpress.XtraReports.Parameters.Parameter();

    private XRLabel lblPerspectiva;
    private XRLabel lblObjetivo;
    private XRLabel lblTipoDetalhe;
    private XRLabel lblDetalhe;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRPageInfo xrPageInfo1;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRPictureBox imgLogo;
    public relIdentidade4()
    {
        InitializeComponent();
        pmissao.Name = "missao";
        pvisao.Name = "visao";
        pcrencasvalores.Name = "crencasvalores";
        pNomeMapa.Name = "nomemapa";
        pathArquivo.Name = "pathArquivo";

        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
        {
            this.pmissao,
            this.pvisao,
            this.pcrencasvalores,
            this.pNomeMapa,
            this.pathArquivo});
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
        string resourceFileName = "relIdentidade4.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblDetalhe = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.imgLogo = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblMissao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblVisao = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCrencasValores = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeMapa = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblPerspectiva = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblObjetivo = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblTipoDetalhe = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDetalhe});
        this.Detail.HeightF = 20F;
        this.Detail.KeepTogether = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Detalhe", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblDetalhe
        // 
        this.lblDetalhe.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.Detalhe")});
        this.lblDetalhe.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblDetalhe.LocationFloat = new DevExpress.Utils.PointFloat(75F, 0F);
        this.lblDetalhe.Name = "lblDetalhe";
        this.lblDetalhe.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDetalhe.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblDetalhe.SizeF = new System.Drawing.SizeF(708F, 20F);
        this.lblDetalhe.StylePriority.UseFont = false;
        this.lblDetalhe.Text = "lblDetalhe";
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgLogo,
            this.xrLabel5,
            this.lblMissao,
            this.xrLabel7,
            this.lblVisao,
            this.lblCrencasValores});
        this.ReportHeader.HeightF = 173.2083F;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
        // 
        // imgLogo
        // 
        this.imgLogo.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 25.99999F);
        this.imgLogo.Name = "imgLogo";
        this.imgLogo.SizeF = new System.Drawing.SizeF(267F, 142F);
        // 
        // xrLabel5
        // 
        this.xrLabel5.BorderColor = System.Drawing.Color.Goldenrod;
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(283F, 25.99999F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(500F, 17F);
        this.xrLabel5.StylePriority.UseBorderColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "Missão";
        // 
        // lblMissao
        // 
        this.lblMissao.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblMissao.LocationFloat = new DevExpress.Utils.PointFloat(283F, 50.99999F);
        this.lblMissao.Name = "lblMissao";
        this.lblMissao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblMissao.SizeF = new System.Drawing.SizeF(500F, 17F);
        this.lblMissao.StylePriority.UseFont = false;
        // 
        // xrLabel7
        // 
        this.xrLabel7.BorderColor = System.Drawing.Color.Goldenrod;
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(283F, 75.99999F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(500F, 17F);
        this.xrLabel7.StylePriority.UseBorderColor = false;
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "Visão";
        // 
        // lblVisao
        // 
        this.lblVisao.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblVisao.LocationFloat = new DevExpress.Utils.PointFloat(283F, 101F);
        this.lblVisao.Name = "lblVisao";
        this.lblVisao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblVisao.SizeF = new System.Drawing.SizeF(500F, 17F);
        this.lblVisao.StylePriority.UseFont = false;
        // 
        // lblCrencasValores
        // 
        this.lblCrencasValores.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
        this.lblCrencasValores.Name = "lblCrencasValores";
        this.lblCrencasValores.SizeF = new System.Drawing.SizeF(773.0001F, 23F);
        // 
        // lblNomeMapa
        // 
        this.lblNomeMapa.BorderColor = System.Drawing.Color.DarkGoldenrod;
        this.lblNomeMapa.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblNomeMapa.BorderWidth = 1F;
        this.lblNomeMapa.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
        this.lblNomeMapa.ForeColor = System.Drawing.Color.DarkGray;
        this.lblNomeMapa.LocationFloat = new DevExpress.Utils.PointFloat(8F, 0F);
        this.lblNomeMapa.Name = "lblNomeMapa";
        this.lblNomeMapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeMapa.SizeF = new System.Drawing.SizeF(733F, 33F);
        this.lblNomeMapa.StylePriority.UseBorderColor = false;
        this.lblNomeMapa.StylePriority.UseBorders = false;
        this.lblNomeMapa.StylePriority.UseBorderWidth = false;
        this.lblNomeMapa.StylePriority.UseFont = false;
        this.lblNomeMapa.StylePriority.UseForeColor = false;
        this.lblNomeMapa.Text = "lblNomeMapa";
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblPerspectiva});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Perspectiva", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 38.54167F;
        this.GroupHeader1.KeepTogether = true;
        this.GroupHeader1.Level = 2;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // lblPerspectiva
        // 
        this.lblPerspectiva.BorderColor = System.Drawing.Color.Black;
        this.lblPerspectiva.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblPerspectiva.BorderWidth = 5F;
        this.lblPerspectiva.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.Perspectiva")});
        this.lblPerspectiva.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblPerspectiva.LocationFloat = new DevExpress.Utils.PointFloat(8F, 7F);
        this.lblPerspectiva.Name = "lblPerspectiva";
        this.lblPerspectiva.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblPerspectiva.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblPerspectiva.SizeF = new System.Drawing.SizeF(775F, 25F);
        this.lblPerspectiva.StylePriority.UseBorderColor = false;
        this.lblPerspectiva.StylePriority.UseBorders = false;
        this.lblPerspectiva.StylePriority.UseBorderWidth = false;
        this.lblPerspectiva.StylePriority.UseFont = false;
        this.lblPerspectiva.Text = "lblPerspectiva";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblObjetivo});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Objetivo", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 41.33332F;
        this.GroupHeader2.KeepTogether = true;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader2_BeforePrint);
        // 
        // lblObjetivo
        // 
        this.lblObjetivo.BorderColor = System.Drawing.Color.Goldenrod;
        this.lblObjetivo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblObjetivo.BorderWidth = 5F;
        this.lblObjetivo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.Objetivo")});
        this.lblObjetivo.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblObjetivo.ForeColor = System.Drawing.Color.DarkGoldenrod;
        this.lblObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(32.99998F, 4F);
        this.lblObjetivo.Name = "lblObjetivo";
        this.lblObjetivo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblObjetivo.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblObjetivo.SizeF = new System.Drawing.SizeF(750F, 33F);
        this.lblObjetivo.StylePriority.UseBorderColor = false;
        this.lblObjetivo.StylePriority.UseBorders = false;
        this.lblObjetivo.StylePriority.UseBorderWidth = false;
        this.lblObjetivo.StylePriority.UseFont = false;
        this.lblObjetivo.StylePriority.UseForeColor = false;
        this.lblObjetivo.Text = "lblObjetivo";
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTipoDetalhe});
        this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TipoDetalhe", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)});
        this.GroupHeader3.HeightF = 39.24999F;
        this.GroupHeader3.KeepTogether = true;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // lblTipoDetalhe
        // 
        this.lblTipoDetalhe.BorderColor = System.Drawing.Color.Goldenrod;
        this.lblTipoDetalhe.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblTipoDetalhe.BorderWidth = 5F;
        this.lblTipoDetalhe.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.TipoDetalhe")});
        this.lblTipoDetalhe.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTipoDetalhe.ForeColor = System.Drawing.Color.DarkGoldenrod;
        this.lblTipoDetalhe.LocationFloat = new DevExpress.Utils.PointFloat(46.99998F, 4F);
        this.lblTipoDetalhe.Name = "lblTipoDetalhe";
        this.lblTipoDetalhe.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTipoDetalhe.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblTipoDetalhe.SizeF = new System.Drawing.SizeF(736F, 33F);
        this.lblTipoDetalhe.StylePriority.UseBorderColor = false;
        this.lblTipoDetalhe.StylePriority.UseBorders = false;
        this.lblTipoDetalhe.StylePriority.UseBorderWidth = false;
        this.lblTipoDetalhe.StylePriority.UseFont = false;
        this.lblTipoDetalhe.StylePriority.UseForeColor = false;
        this.lblTipoDetalhe.Text = "lblTipoDetalhe";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNomeMapa,
            this.xrPageInfo1});
        this.PageHeader.HeightF = 34.70834F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.BorderColor = System.Drawing.Color.DarkGoldenrod;
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(741F, 16F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(42F, 17F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PageFooter
        // 
        this.PageFooter.HeightF = 36F;
        this.PageFooter.Name = "PageFooter";
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.HeightF = 30F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.HeightF = 30F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // relIdentidade4
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupHeader3,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "15.1";
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.relIdentidade4_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relIdentidade4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {


    }

    private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (GetCurrentColumnValue("Detalhe") != null)
        {
            string detalhe = GetCurrentColumnValue("Detalhe").ToString();

            if ((detalhe.Length > 0) || (CurrentRowIndex == 0))
                lblObjetivo.BorderWidth = 5;
            else
                lblObjetivo.BorderWidth = 0;
        }

    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblNomeMapa.Text = pNomeMapa.Value.ToString();
    }

    private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        Thread.Sleep(1000);
        lblMissao.Text = pmissao.Value.ToString();
        lblVisao.Text = pvisao.Value.ToString();
        lblCrencasValores.Text = pcrencasvalores.Value.ToString();
        imgLogo.ImageUrl = pathArquivo.Value.ToString();
    }
}
