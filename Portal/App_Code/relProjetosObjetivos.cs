using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for relProjetosPortfolios
/// </summary>
public class relProjetosObjetivos : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private GroupHeaderBand GroupHeader1;
    private XRLabel lblTitulo;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel1;
    private XRLabel lblCategoria;
    private XRLabel lblUnidade;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel lblStatus;
    private XRLabel lblDescricao;
    private XRLabel xrLabel4;
    private GroupHeaderBand GroupHeader3;
    private XRLabel lblDescricaoObjetivo;
    private XRLabel lblTituloOE;
    private GroupHeaderBand GroupHeader4;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel5;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relProjetosObjetivos()
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
        //string resourceFileName = "relProjetosObjetivos.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblTitulo = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCategoria = new DevExpress.XtraReports.UI.XRLabel();
        this.lblUnidade = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblStatus = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDescricao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblDescricaoObjetivo = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTituloOE = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Font = new System.Drawing.Font("Verdana", 9F);
        this.Detail.HeightF = 10F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.StylePriority.UseFont = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrLabel5});
        this.PageHeader.HeightF = 38F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.BorderColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(725F, 5F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(31F, 26F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel5
        // 
        this.xrLabel5.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel5.BorderWidth = 1F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.ForeColor = System.Drawing.Color.Silver;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(725F, 25F);
        this.xrLabel5.StylePriority.UseBorderColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseBorderWidth = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseForeColor = false;
        this.xrLabel5.Text = "Lista de Projetos e Propostas de Iniciativas";
        // 
        // PageFooter
        // 
        this.PageFooter.HeightF = 30F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTitulo});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Titulo", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 60F;
        this.GroupHeader1.KeepTogether = true;
        this.GroupHeader1.Level = 3;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // lblTitulo
        // 
        this.lblTitulo.BorderColor = System.Drawing.Color.Goldenrod;
        this.lblTitulo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblTitulo.BorderWidth = 3F;
        this.lblTitulo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.Titulo")});
        this.lblTitulo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitulo.ForeColor = System.Drawing.Color.Black;
        this.lblTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 19F);
        this.lblTitulo.Name = "lblTitulo";
        this.lblTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTitulo.SizeF = new System.Drawing.SizeF(762F, 25F);
        this.lblTitulo.StylePriority.UseBorderColor = false;
        this.lblTitulo.StylePriority.UseBorders = false;
        this.lblTitulo.StylePriority.UseBorderWidth = false;
        this.lblTitulo.StylePriority.UseFont = false;
        this.lblTitulo.StylePriority.UseForeColor = false;
        this.lblTitulo.Text = "lblTitulo";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.lblCategoria,
            this.lblUnidade,
            this.xrLabel2,
            this.xrLabel3,
            this.lblStatus,
            this.lblDescricao,
            this.xrLabel4});
        this.GroupHeader2.HeightF = 164F;
        this.GroupHeader2.KeepTogether = true;
        this.GroupHeader2.Level = 2;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(32F, 38F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(112F, 15F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Categoria:";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblCategoria
        // 
        this.lblCategoria.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.DescricaoCategoria")});
        this.lblCategoria.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblCategoria.LocationFloat = new DevExpress.Utils.PointFloat(32F, 53F);
        this.lblCategoria.Name = "lblCategoria";
        this.lblCategoria.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCategoria.SizeF = new System.Drawing.SizeF(730F, 15F);
        this.lblCategoria.StylePriority.UseFont = false;
        this.lblCategoria.StylePriority.UseTextAlignment = false;
        this.lblCategoria.Text = "lblCategoria";
        this.lblCategoria.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblUnidade
        // 
        this.lblUnidade.BorderColor = System.Drawing.Color.Transparent;
        this.lblUnidade.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblUnidade.BorderWidth = 0F;
        this.lblUnidade.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.NomeUnidadeNegocio")});
        this.lblUnidade.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblUnidade.LocationFloat = new DevExpress.Utils.PointFloat(32F, 15F);
        this.lblUnidade.Name = "lblUnidade";
        this.lblUnidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblUnidade.SizeF = new System.Drawing.SizeF(730F, 15F);
        this.lblUnidade.StylePriority.UseBorderColor = false;
        this.lblUnidade.StylePriority.UseBorders = false;
        this.lblUnidade.StylePriority.UseBorderWidth = false;
        this.lblUnidade.StylePriority.UseFont = false;
        this.lblUnidade.StylePriority.UseTextAlignment = false;
        this.lblUnidade.Text = "lblUnidade";
        this.lblUnidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(32F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(112F, 15F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Unidade:";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(32F, 77F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(112F, 15F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Status:";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblStatus
        // 
        this.lblStatus.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.DescricaoStatus")});
        this.lblStatus.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblStatus.LocationFloat = new DevExpress.Utils.PointFloat(32F, 92F);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblStatus.SizeF = new System.Drawing.SizeF(730F, 15F);
        this.lblStatus.StylePriority.UseFont = false;
        this.lblStatus.StylePriority.UseTextAlignment = false;
        this.lblStatus.Text = "lblStatus";
        this.lblStatus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblDescricao
        // 
        this.lblDescricao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.Descricao")});
        this.lblDescricao.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblDescricao.LocationFloat = new DevExpress.Utils.PointFloat(32F, 133F);
        this.lblDescricao.Name = "lblDescricao";
        this.lblDescricao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDescricao.SizeF = new System.Drawing.SizeF(730F, 15F);
        this.lblDescricao.StylePriority.UseFont = false;
        this.lblDescricao.StylePriority.UseTextAlignment = false;
        this.lblDescricao.Text = "lblDescricao";
        this.lblDescricao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(32F, 118F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(112F, 15F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Descrição:";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDescricaoObjetivo});
        this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("DescricaoObjetoEstrategia", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader3.HeightF = 26F;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // lblDescricaoObjetivo
        // 
        this.lblDescricaoObjetivo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.DescricaoObjetoEstrategia")});
        this.lblDescricaoObjetivo.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblDescricaoObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
        this.lblDescricaoObjetivo.Name = "lblDescricaoObjetivo";
        this.lblDescricaoObjetivo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDescricaoObjetivo.SizeF = new System.Drawing.SizeF(712F, 25F);
        this.lblDescricaoObjetivo.StylePriority.UseFont = false;
        this.lblDescricaoObjetivo.StylePriority.UseTextAlignment = false;
        this.lblDescricaoObjetivo.Text = "lblDescricao";
        this.lblDescricaoObjetivo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblTituloOE
        // 
        this.lblTituloOE.BorderColor = System.Drawing.Color.Goldenrod;
        this.lblTituloOE.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblTituloOE.BorderWidth = 2F;
        this.lblTituloOE.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.tituloOE")});
        this.lblTituloOE.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
        this.lblTituloOE.ForeColor = System.Drawing.Color.Goldenrod;
        this.lblTituloOE.LocationFloat = new DevExpress.Utils.PointFloat(25F, 6F);
        this.lblTituloOE.Name = "lblTituloOE";
        this.lblTituloOE.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTituloOE.SizeF = new System.Drawing.SizeF(738F, 25F);
        this.lblTituloOE.StylePriority.UseBorderColor = false;
        this.lblTituloOE.StylePriority.UseBorders = false;
        this.lblTituloOE.StylePriority.UseBorderWidth = false;
        this.lblTituloOE.StylePriority.UseFont = false;
        this.lblTituloOE.StylePriority.UseForeColor = false;
        this.lblTituloOE.StylePriority.UseTextAlignment = false;
        this.lblTituloOE.Text = "Objetivos Estratégicos";
        this.lblTituloOE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTituloOE});
        this.GroupHeader4.HeightF = 34F;
        this.GroupHeader4.Level = 1;
        this.GroupHeader4.Name = "GroupHeader4";
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
        // relProjetosObjetivos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupHeader3,
            this.GroupHeader4,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.SnapGridSize = 5F;
        this.Version = "14.2";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
