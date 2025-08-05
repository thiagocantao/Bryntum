using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for relTramitacao
/// </summary>
public class relTramitacao : DevExpress.XtraReports.UI.XtraReport
{
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private DetailBand Detail;
    private PageHeaderBand PageHeader;
    private XRLine xrLine5;
    private XRLine xrLine4;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel3;
    private XRLine xrLine7;
    private XRLine xrLine1;
    private XRLabel xrLabel5;
    private XRRichText xrRichText1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private dsRelTramitacao dsRelTramitacao1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    public DevExpress.XtraReports.Parameters.Parameter p_UrlLogo;
    public DevExpress.XtraReports.Parameters.Parameter p_NomeFluxo;
    public DevExpress.XtraReports.Parameters.Parameter p_NomeEtapa;
    private FormattingRule formattingRule1;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    public dados cDados;
    public relTramitacao(int codigoWorkflow, int codigoInstanciaWorkflow)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        cDados = CdadosUtil.GetCdados(null);
        InitData(codigoWorkflow, codigoInstanciaWorkflow);
    }
    private void InitData(int codigoWorkflow, int codigoInstanciaWorkflow)
    {
        #region Comando SQL
        string comandoSql = string.Format(@"SELECT  
			                 up.NomeUsuario AS UsuarioParecer
			                ,  DataParecer, Parecer
			                ,CASE WHEN tef.DataParecer IS NULL AND (tef.CodigoEtapa <> 1 OR tef.SequenciaEtapa <> 1) THEN 'Expirado'                                  
						          WHEN tef.DataSolicitacaoParecer IS NULL THEN 'Aguardando Envio'
						          WHEN tef.DataParecer IS NULL AND tef.DataPrevistaParecer + 1 < GETDATE() THEN 'Atrasado'
						          WHEN tef.DataParecer IS NULL AND tef.DataPrevistaParecer + 1 >= GETDATE() THEN 'Aguardando Tramitação'
						          WHEN tef.DataExclusao IS NOT NULL THEN 'Excluído'
						          WHEN tef.DataParecer IS NOT NULL THEN 'Concluído'
						          ELSE '' END AS StatusParecer
                  FROM {0}.{1}.TramitacaoEtapaFluxo tef INNER JOIN
			           {0}.{1}.Usuario up ON up.CodigoUsuario = tef.CodigoUsuarioParecer
                 WHERE tef.CodigoWorkflow = {2}
                   AND tef.CodigoInstancia = {3}
                   AND tef.DataExclusao IS NULL
                    AND tef.DataParecer IS NOT NULL 
                 ORDER BY DataParecer desc", cDados.getDbName(), cDados.getDbOwner(), codigoWorkflow, codigoInstanciaWorkflow);

        #endregion

        string[] tableNames = new string[] { "Table" };
        DataSet dsTemp = cDados.getDataSet(comandoSql);
        dsRelTramitacao1.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);
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
        string resourceFileName = "relTramitacao.resx";
        System.Resources.ResourceManager resources = global::Resources.relTramitacao.ResourceManager;
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.p_UrlLogo = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.p_NomeFluxo = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLine7 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.p_NomeEtapa = new DevExpress.XtraReports.Parameters.Parameter();
        this.dsRelTramitacao1 = new dsRelTramitacao();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelTramitacao1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 100F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 100F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1,
            this.xrLabel2,
            this.xrLabel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 140.2292F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "Table.Parecer")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrRichText1.FormattingRules.Add(this.formattingRule1);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(5.023656F, 61.41994F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1894.976F, 58.41997F);
        this.xrRichText1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrRichText1_BeforePrint);
        // 
        // formattingRule1
        // 
        this.formattingRule1.Condition = " Not IsNullOrEmpty([Parecer])";
        // 
        // 
        // 
        this.formattingRule1.Formatting.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.formattingRule1.Name = "formattingRule1";
        // 
        // xrLabel2
        // 
        this.xrLabel2.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table.UsuarioParecer")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.ForeColor = System.Drawing.Color.DarkBlue;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(341.3125F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1558.688F, 61.42001F);
        this.xrLabel2.StylePriority.UseBorderColor = false;
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.BorderColor = System.Drawing.Color.DarkBlue;
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table.DataParecer", "{0:dd/MM/yyyy HH:mm}")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.ForeColor = System.Drawing.Color.DarkBlue;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(341.3125F, 61.42F);
        this.xrLabel1.StylePriority.UseBorderColor = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "xrLabel1";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine5,
            this.xrLine4,
            this.xrPictureBox1,
            this.xrLabel3,
            this.xrLine7,
            this.xrLine1,
            this.xrLabel5});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 254F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLine5
        // 
        this.xrLine5.Dpi = 254F;
        this.xrLine5.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 202.0119F);
        this.xrLine5.Name = "xrLine5";
        this.xrLine5.SizeF = new System.Drawing.SizeF(1900F, 5.08194F);
        this.xrLine5.StylePriority.UseForeColor = false;
        // 
        // xrLine4
        // 
        this.xrLine4.Dpi = 254F;
        this.xrLine4.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46.90616F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(1900F, 5.023734F);
        this.xrLine4.StylePriority.UseForeColor = false;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.p_UrlLogo, "ImageUrl", "")});
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(5.023744F, 51.9299F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(349.606F, 150F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // p_UrlLogo
        // 
        this.p_UrlLogo.Description = "Parameter1";
        this.p_UrlLogo.Name = "p_UrlLogo";
        this.p_UrlLogo.Visible = false;
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.p_NomeFluxo, "Text", "")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(354.6298F, 52.01183F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1539.289F, 82.39433F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // p_NomeFluxo
        // 
        this.p_NomeFluxo.Description = "Parameter1";
        this.p_NomeFluxo.Name = "p_NomeFluxo";
        this.p_NomeFluxo.Visible = false;
        // 
        // xrLine7
        // 
        this.xrLine7.Dpi = 254F;
        this.xrLine7.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine7.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine7.LineWidth = 3;
        this.xrLine7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.9299F);
        this.xrLine7.Name = "xrLine7";
        this.xrLine7.SizeF = new System.Drawing.SizeF(5.023736F, 150.0819F);
        this.xrLine7.StylePriority.UseForeColor = false;
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(1894.918F, 52.01182F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(5.081909F, 150F);
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.p_NomeEtapa, "Text", "")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(354.6297F, 134.4062F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(1539.289F, 67.52374F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "xrLabel3";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // p_NomeEtapa
        // 
        this.p_NomeEtapa.Description = "Parameter1";
        this.p_NomeEtapa.Name = "p_NomeEtapa";
        this.p_NomeEtapa.Visible = false;
        // 
        // dsRelTramitacao1
        // 
        this.dsRelTramitacao1.DataSetName = "dsRelTramitacao";
        this.dsRelTramitacao1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 68.79166F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrPageInfo2.Format = "Data de emissão: {0:dd/MM/yyyy HH:mm:ss}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(619.1249F, 58.42F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1646F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // relTramitacao
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.DataMember = "Table";
        this.DataSource = this.dsRelTramitacao1;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.p_NomeFluxo,
            this.p_NomeEtapa,
            this.p_UrlLogo});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "19.1";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relTramitacao_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelTramitacao1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relTramitacao_DataSourceDemanded(object sender, EventArgs e)
    {

    }

    private void xrRichText1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        Font fonte = new Font("Verdana", 8, FontStyle.Regular);
        ((XRRichText)sender).Font = fonte;
    }

}
