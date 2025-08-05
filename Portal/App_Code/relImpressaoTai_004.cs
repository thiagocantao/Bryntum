using DevExpress.XtraReports.UI;
using dsImpressaoTai_004TableAdapters;
using System;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for relImpressaoTai_004
/// </summary>
public class relImpressaoTai_004 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private dsImpressaoTai_004 ds;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private XRControlStyle TableCaption;
    private XRControlStyle TableDataField;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPictureBox picLogoEntidade;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRLabel xrLabel7;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel13;
    private XRLabel xrLabel10;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRLabel xrLabel14;
    private DetailReportBand DetailReport3;
    private DetailBand Detail4;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel22;
    private XRLabel xrLabel21;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    public DevExpress.XtraReports.Parameters.Parameter pTitulo;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relImpressaoTai_004(Int32 codigoProjeto)
    {
        InitializeComponent();

        InitData(codigoProjeto);
    }

    private void InitData(int codigoProjeto)
    {
        DefineLogoEntidade();
        String connectionString = ConnectionString;
        TermoAbertura04TableAdapter termoAberturaAdapter = new TermoAbertura04TableAdapter();
        tai04_ParceirosIniciativaTableAdapter acoesIniciativaAdapter = new tai04_ParceirosIniciativaTableAdapter();
        tai04_ResultadosIniciativaTableAdapter marcosAcoesIniciativaAdapter = new tai04_ResultadosIniciativaTableAdapter();

        termoAberturaAdapter.Connection.ConnectionString =
            acoesIniciativaAdapter.Connection.ConnectionString =
            marcosAcoesIniciativaAdapter.Connection.ConnectionString = connectionString;

        termoAberturaAdapter.FillByCodigoProjeto(ds.TermoAbertura04, codigoProjeto);
        acoesIniciativaAdapter.FillByCodigoProjeto(ds.tai04_ParceirosIniciativa, codigoProjeto);
        marcosAcoesIniciativaAdapter.FillByCodigoProjeto(ds.tai04_ResultadosIniciativa, codigoProjeto);
    }

    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        Int32 codigoEntidade = (Int32)cDados.getInfoSistema("CodigoEntidade");
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        picLogoEntidade.Image = Bitmap.FromStream(ms);
    }

    private String ConnectionString
    {
        get
        {
            dados cDados = CdadosUtil.GetCdados(null);
            return cDados.classeDados.getStringConexao();
        }
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
        //string resourceFileName = "relImpressaoTai_004.resx";
        DevExpress.XtraReports.UI.XRLine xrLine1;
        DevExpress.XtraReports.UI.XRLabel xrLabel35;
        DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ds = new dsImpressaoTai_004();
        this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
        this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
        this.TableCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.TableDataField = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.pTitulo = new DevExpress.XtraReports.Parameters.Parameter();
        xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // xrLine1
        // 
        xrLine1.Dpi = 254F;
        xrLine1.LineWidth = 3;
        xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(16F, 160F);
        xrLine1.Name = "xrLine1";
        xrLine1.SizeF = new System.Drawing.SizeF(1890F, 5F);
        // 
        // xrLabel35
        // 
        xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pTitulo, "Text", "")});
        xrLabel35.Dpi = 254F;
        xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(575F, 25F);
        xrLabel35.Name = "xrLabel35";
        xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel35.SizeF = new System.Drawing.SizeF(800F, 84F);
        xrLabel35.StyleName = "Title";
        xrLabel35.StylePriority.UseFont = false;
        xrLabel35.StylePriority.UseForeColor = false;
        xrLabel35.StylePriority.UseTextAlignment = false;
        xrLabel35.Text = "Termo de Abertura";
        xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPageInfo2
        // 
        xrPageInfo2.Dpi = 254F;
        xrPageInfo2.Format = "Pág. {0}/{1}";
        xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(984F, 8.290001F);
        xrPageInfo2.Name = "xrPageInfo2";
        xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrPageInfo2.SizeF = new System.Drawing.SizeF(936F, 58.42F);
        xrPageInfo2.StyleName = "PageInfo";
        xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        xrPageInfo1.Dpi = 254F;
        xrPageInfo1.Format = "Emitido em {0:dd/MM/yyyy - HH:mm}";
        xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(16F, 8.290001F);
        xrPageInfo1.Name = "xrPageInfo1";
        xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        xrPageInfo1.SizeF = new System.Drawing.SizeF(936F, 58.42F);
        xrPageInfo1.StyleName = "PageInfo";
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 562.8558F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel24
        // 
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(16.0001F, 375.2179F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel24.StyleName = "FieldCaption";
        this.xrLabel24.Text = "Nome Solicitante";
        // 
        // xrLabel23
        // 
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeSolicitante")});
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(15.99994F, 425.2179F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(1890F, 58.42004F);
        this.xrLabel23.Text = "xrLabel22";
        // 
        // xrLabel22
        // 
        this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeGerenteUnidade")});
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(15.99983F, 303.3525F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(1890F, 58.42004F);
        this.xrLabel22.Text = "xrLabel22";
        // 
        // xrLabel21
        // 
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 253.3525F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel21.StyleName = "FieldCaption";
        this.xrLabel21.Text = "Nome Gerente da Unidade Responsável Pelo Projeto";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeUnidadeNegocio")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(969.9999F, 175F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(936.0001F, 58.42003F);
        this.xrLabel6.Text = "xrLabel6";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(969.9999F, 125F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(936.0004F, 49.99999F);
        this.xrLabel5.StyleName = "FieldCaption";
        this.xrLabel5.Text = "Unidade Responsável Pelo Projeto ";
        // 
        // xrLabel4
        // 
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeGerenteIniciativa")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 175F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(950F, 58.42001F);
        this.xrLabel4.Text = "xrLabel4";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(16F, 125F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(950F, 50F);
        this.xrLabel3.StyleName = "FieldCaption";
        this.xrLabel3.Text = "Gerente do Projeto";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel2.StyleName = "FieldCaption";
        this.xrLabel2.Text = "Nome do Projeto/Processo";
        // 
        // xrLabel1
        // 
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeIniciativa")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 50.00002F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1890F, 58.41998F);
        this.xrLabel1.Text = "xrLabel1";
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 201F;
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
        // ds
        // 
        this.ds.DataSetName = "dsImpressaoTai_004";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // Title
        // 
        this.Title.BackColor = System.Drawing.Color.White;
        this.Title.BorderColor = System.Drawing.SystemColors.ControlText;
        this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.Title.BorderWidth = 1;
        this.Title.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
        this.Title.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Title.Name = "Title";
        // 
        // FieldCaption
        // 
        this.FieldCaption.BackColor = System.Drawing.Color.White;
        this.FieldCaption.BorderColor = System.Drawing.SystemColors.ControlText;
        this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.FieldCaption.BorderWidth = 1;
        this.FieldCaption.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.FieldCaption.ForeColor = System.Drawing.SystemColors.ControlText;
        this.FieldCaption.Name = "FieldCaption";
        // 
        // PageInfo
        // 
        this.PageInfo.BackColor = System.Drawing.Color.White;
        this.PageInfo.BorderColor = System.Drawing.SystemColors.ControlText;
        this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PageInfo.BorderWidth = 1;
        this.PageInfo.Font = new System.Drawing.Font("Verdana", 8F);
        this.PageInfo.ForeColor = System.Drawing.SystemColors.ControlText;
        this.PageInfo.Name = "PageInfo";
        // 
        // DataField
        // 
        this.DataField.BackColor = System.Drawing.Color.White;
        this.DataField.BorderColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.DataField.BorderWidth = 1;
        this.DataField.Font = new System.Drawing.Font("Verdana", 8F);
        this.DataField.ForeColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Name = "DataField";
        this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        // 
        // TableCaption
        // 
        this.TableCaption.BackColor = System.Drawing.Color.White;
        this.TableCaption.BorderColor = System.Drawing.SystemColors.ControlText;
        this.TableCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TableCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.TableCaption.ForeColor = System.Drawing.SystemColors.ControlText;
        this.TableCaption.Name = "TableCaption";
        // 
        // TableDataField
        // 
        this.TableDataField.BackColor = System.Drawing.Color.White;
        this.TableDataField.BorderColor = System.Drawing.SystemColors.ControlText;
        this.TableDataField.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TableDataField.Font = new System.Drawing.Font("Verdana", 8F);
        this.TableDataField.ForeColor = System.Drawing.SystemColors.ControlText;
        this.TableDataField.Name = "TableDataField";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.picLogoEntidade,
            xrLabel35,
            xrLine1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 175F;
        this.PageHeader.Name = "PageHeader";
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(16F, 0F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(300F, 150F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrPageInfo1,
            xrPageInfo2});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 75F;
        this.PageFooter.Name = "PageFooter";
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1});
        this.DetailReport.DataMember = "TermoAbertura04.TermoAbertura04_tai04_ParceirosIniciativa";
        this.DetailReport.DataSource = this.ds;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 50F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrTable2.StyleName = "TableDataField";
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5679012345679012D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ParceirosIniciativa.NomeUsuario")});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Email";
        this.xrTableCell1.Weight = 3.6990291262135919D;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrTable1});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel7.StyleName = "FieldCaption";
        this.xrLabel7.Text = "Equipe";
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 50.00002F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrTable1.StyleName = "TableCaption";
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Nome Usuário";
        this.xrTableCell3.Weight = 3.6990291262135919D;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
        this.DetailReport1.DataMember = "TermoAbertura04";
        this.DetailReport1.DataSource = this.ds;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 1;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 275F;
        this.Detail2.Name = "Detail2";
        // 
        // xrLabel13
        // 
        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.Justificativa")});
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(16F, 200F);
        this.xrLabel13.Multiline = true;
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel13.Text = "xrLabel9";
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(16F, 150F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel10.StyleName = "FieldCaption";
        this.xrLabel10.Text = "Justificativa";
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.ObjetivoGeral")});
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(16F, 75F);
        this.xrLabel9.Multiline = true;
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel9.Text = "xrLabel9";
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(16F, 25F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel8.StyleName = "FieldCaption";
        this.xrLabel8.Text = "Objetivo do Projeto ";
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader2});
        this.DetailReport2.DataMember = "TermoAbertura04.TermoAbertura04_tai04_ResultadosIniciativa";
        this.DetailReport2.DataSource = this.ds;
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Level = 2;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 50F;
        this.Detail3.Name = "Detail3";
        // 
        // xrTable4
        // 
        this.xrTable4.Dpi = 254F;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrTable4.StyleName = "TableDataField";
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 0.5679012345679012D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ResultadosIniciativa.SetencaResultado")});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "10/10/2012";
        this.xrTableCell8.Weight = 0.91728593051926133D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ResultadosIniciativa.ValorTrimestre1")});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "1000000000";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell17.Weight = 0.19707314428649636D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ResultadosIniciativa.ValorTrimestre2")});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = "Valor Trimestre2";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell18.Weight = 0.19707314586364538D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ResultadosIniciativa.ValorTrimestre3")});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "Valor Trimestre3";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell19.Weight = 0.19707315269795811D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ResultadosIniciativa.ValorTrimestre4")});
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "Valor Trimestre4";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell20.Weight = 0.19707313902933271D;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrTable3});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrLabel14
        // 
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(16F, 0F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel14.StyleName = "FieldCaption";
        this.xrLabel14.Text = "Metas ";
        // 
        // xrTable3
        // 
        this.xrTable3.Dpi = 254F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 50.00002F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrTable3.StyleName = "TableCaption";
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.5679012345679012D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "Descrição do Resultado";
        this.xrTableCell10.Weight = 0.91728593051926133D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "Valor 1º Trim";
        this.xrTableCell12.Weight = 0.19707314428649636D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "Valor 2º Trim";
        this.xrTableCell13.Weight = 0.19707314586364538D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "Valor 3º Trim";
        this.xrTableCell14.Weight = 0.19707315269795811D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "Valor 4º Trim";
        this.xrTableCell15.Weight = 0.19707313902933271D;
        // 
        // DetailReport3
        // 
        this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4});
        this.DetailReport3.DataMember = "TermoAbertura04";
        this.DetailReport3.DataSource = this.ds;
        this.DetailReport3.Dpi = 254F;
        this.DetailReport3.Level = 3;
        this.DetailReport3.Name = "DetailReport3";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel12,
            this.xrLabel11});
        this.Detail4.Dpi = 254F;
        this.Detail4.HeightF = 525F;
        this.Detail4.Name = "Detail4";
        // 
        // xrLabel20
        // 
        this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.ValorEstimado")});
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(16F, 325F);
        this.xrLabel20.Multiline = true;
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel20.Text = "xrLabel9";
        // 
        // xrLabel19
        // 
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(16F, 275F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel19.StyleName = "FieldCaption";
        this.xrLabel19.Text = "Estimativa de Orçamento";
        // 
        // xrLabel18
        // 
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.CronogramaBasico")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(16F, 200F);
        this.xrLabel18.Multiline = true;
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel18.Text = "xrLabel9";
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(16F, 150F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel17.StyleName = "FieldCaption";
        this.xrLabel17.Text = "Cronograma Básico";
        // 
        // xrLabel16
        // 
        this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.Escopo")});
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(16F, 75F);
        this.xrLabel16.Multiline = true;
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel16.Text = "xrLabel9";
        // 
        // xrLabel15
        // 
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(16F, 25F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel15.StyleName = "FieldCaption";
        this.xrLabel15.Text = "Produto Final / Resultados Esperados ";
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(16F, 400F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel12.StyleName = "FieldCaption";
        this.xrLabel12.Text = "Observações";
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.Observacoes")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(16F, 450F);
        this.xrLabel11.Multiline = true;
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1890F, 50F);
        this.xrLabel11.Text = "xrLabel9";
        // 
        // pTitulo
        // 
        this.pTitulo.Description = "Titulo do Relatório";
        this.pTitulo.Name = "pTitulo";
        this.pTitulo.ValueInfo = "Termo de Abertura";
        this.pTitulo.Visible = false;
        // 
        // relImpressaoTai_004
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport,
            this.DetailReport1,
            this.DetailReport2,
            this.DetailReport3});
        this.DataMember = "TermoAbertura04";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(150, 0, 201, 0);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pTitulo});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField,
            this.TableCaption,
            this.TableDataField});
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
