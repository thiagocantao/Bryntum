using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for relSubreportProdutos
/// </summary>
public class relSubreportProjetos : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell9;
    private ReportHeaderBand ReportHeader;
    public DevExpress.XtraReports.Parameters.Parameter CodProjeto;
    private DsAcompanhamento dsAcompanhamento1;
    private dados cDados = CdadosUtil.GetCdados(null);
    private XRLabel xrLabel1;
    private XRRichText xrRichText1;
    private FormattingRule formattingRule1;

    private int codigoUnidadeGlobal = 0;
    private XRPageInfo xrPageInfo1;
    private PageFooterBand PageFooter;
    private XRLine xrLine2;
    private PageHeaderBand PageHeader;
    private XRPictureBox imgCabecalhoPagina;
    private XRPictureBox imgCabecalhoRelatorio;
    private XRPictureBox xrPictureBox4;
    private int anoGlobal = 0;
    private XRPageInfo xrPageInfo2;
    Bitmap img;
    Bitmap img1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relSubreportProjetos(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {
        InitializeComponent();
        codigoUnidadeGlobal = codigoUnidade;
        anoGlobal = DateTime.Now.Year;//ano;
        InitData(codigoUsuario, codigoEntidade, codigoUnidade, ano);

    }

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

    private void InitData(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {

        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;

        comandoSql = string.Format(@"DECLARE @RC int
                                     DECLARE @codigoUsuario int
                                     DECLARE @codigoEntidade int
                                     DECLARE @codigoUnidade int
                                     declare @anoRef as smallint

                                     SET @codigoUsuario = {2}
                                     SET @codigoEntidade = {3}
                                     SET @codigoUnidade = {4}
                                     SET @anoRef = {5}                              
                                 EXECUTE @RC = {0}.{1}.p_DadosRelatorioReprojetos 
                                                       @codigoUsuario
                                                      ,@codigoEntidade
                                                      ,@codigoUnidade,@anoRef", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, codigoUnidade, ano);

        DataSet ds = cDados.getDataSet(comandoSql);
        dsAcompanhamento1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "Projetos");
        DefineImagemCapa();
    }

    private void DefineImagemCapa()
    {
        //string resourceFileName = "relSubReportProjetos.resx";
        System.Resources.ResourceManager resources = global::Resources.relSubReportProjetos.ResourceManager;
        img = (Bitmap)resources.GetObject("imgCabecalhoRelatorio.Image");
        img1 = (Bitmap)resources.GetObject("imgCabecalhoPagina.Image");
        imgCabecalhoRelatorio.Image = img;
        imgCabecalhoPagina.Image = img1;
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
        //string resourceFileName = "relSubReportProjetos.resx";
        System.Resources.ResourceManager resources = global::Resources.relSubReportProjetos.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.imgCabecalhoRelatorio = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.CodProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.dsAcompanhamento1 = new DsAcompanhamento();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.imgCabecalhoPagina = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 37.12839F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(99.99999F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1870F, 37.12839F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5679012345679012D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Projetos.NomeProjeto")});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell9.Weight = 1.1276356123949896D;
        this.xrTableCell9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell9_BeforePrint);
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
        this.BottomMargin.HeightF = 48F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgCabecalhoRelatorio,
            this.xrLine2,
            this.xrLabel1,
            this.xrRichText1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 939.2708F;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
        // 
        // imgCabecalhoRelatorio
        // 
        this.imgCabecalhoRelatorio.Dpi = 254F;
        this.imgCabecalhoRelatorio.Image = ((System.Drawing.Image)(resources.GetObject("imgCabecalhoRelatorio.Image")));
        this.imgCabecalhoRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.imgCabecalhoRelatorio.Name = "imgCabecalhoRelatorio";
        this.imgCabecalhoRelatorio.SizeF = new System.Drawing.SizeF(2100F, 400F);
        this.imgCabecalhoRelatorio.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(90F, 420F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1880F, 29.99998F);
        // 
        // xrLabel1
        // 
        this.xrLabel1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(90F, 460F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1880F, 52.21169F);
        this.xrLabel1.StylePriority.UseBorderDashStyle = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "1.   ESCOPO DO ACOMPANHAMENTO";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrRichText1
        // 
        this.xrRichText1.CanShrink = true;
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrRichText1.FormattingRules.Add(this.formattingRule1);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(90F, 520F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1880F, 395.8992F);
        this.xrRichText1.StylePriority.UseFont = false;
        // 
        // formattingRule1
        // 
        // 
        // 
        // 
        this.formattingRule1.Formatting.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.formattingRule1.Formatting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        this.formattingRule1.Name = "formattingRule1";
        // 
        // CodProjeto
        // 
        this.CodProjeto.Name = "CodProjeto";
        this.CodProjeto.Type = typeof(int);
        this.CodProjeto.Visible = false;
        // 
        // dsAcompanhamento1
        // 
        this.dsAcompanhamento1.DataSetName = "DsAcompanhamento";
        this.dsAcompanhamento1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.BorderColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.Format = "|{0}|";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 120F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.Number;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(2100F, 39.89922F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPictureBox4,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 169.3576F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportFooter_BeforePrint);
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrPageInfo2.ForeColor = System.Drawing.Color.DimGray;
        this.xrPageInfo2.Format = "Data de emissão: {0:dd/MM/yyyy HH:mm:ss}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1260F, 10.00004F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(703.7916F, 33.41979F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseForeColor = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrPictureBox4
        // 
        this.xrPictureBox4.Dpi = 254F;
        this.xrPictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox4.Image")));
        this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(86F, 50F);
        this.xrPictureBox4.Name = "xrPictureBox4";
        this.xrPictureBox4.SizeF = new System.Drawing.SizeF(1884F, 54.99995F);
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgCabecalhoPagina});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 410.0002F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.PrintOn = DevExpress.XtraReports.UI.PrintOnPages.NotWithReportHeader;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // imgCabecalhoPagina
        // 
        this.imgCabecalhoPagina.BackColor = System.Drawing.Color.Transparent;
        this.imgCabecalhoPagina.Dpi = 254F;
        this.imgCabecalhoPagina.Image = ((System.Drawing.Image)(resources.GetObject("imgCabecalhoPagina.Image")));
        this.imgCabecalhoPagina.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00004F);
        this.imgCabecalhoPagina.Name = "imgCabecalhoPagina";
        this.imgCabecalhoPagina.SizeF = new System.Drawing.SizeF(2100F, 400.0002F);
        this.imgCabecalhoPagina.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.imgCabecalhoPagina.StylePriority.UseBackColor = false;
        // 
        // relSubreportProjetos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.ReportHeader,
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageFooter,
            this.PageHeader});
        this.DataMember = "Projetos";
        this.DataSource = this.dsAcompanhamento1;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 48);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CodProjeto});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 10F;
        this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.SnapToGrid;
        this.Version = "12.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrTableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell9.Text = " * " + xrTableCell9.Text;
    }

    private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        InsereCamposDinamicosNaCapa();
    }

    private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
}
