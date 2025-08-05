using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for rel_ResumoProjeto
/// </summary>
public class rel_ResumoProjeto : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    public DsResumoProjeto ds;
    private XRLine xrLine2;
    private XRLabel xrLabel10;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLine xrLine3;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRLabel xrLabel19;
    private XRPictureBox xrPictureBox1;
    private XRChart xrChart2;
    private XRChart xrChart3;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel20;
    private XRLabel xrLabel21;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private readonly int codigoProjeto;
    private readonly string strCodigosAnexos;

    private XRPictureBox pictureBoxLogo;
    private XRPanel xrPanel1;
    private XRLabel xrLabel1;
    private XRPanel xrPanel2;
    private XRLabel xrLabel2;
    private XRLabel xrLabel4;
    private XRPanel xrPanel5;
    private XRLabel xrLabel3;
    private XRPanel xrPanel3;
    private XRLabel xrLabel5;
    private XRPanel xrPanel4;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLine xrLine1;
    private DevExpress.XtraReports.Parameters.Parameter parametroLabelValorMedido;
    private dados cDados;

    public rel_ResumoProjeto(int codigoProjeto, string strCodigosAnexos)
    {
        cDados = CdadosUtil.GetCdados(null);
        this.codigoProjeto = codigoProjeto;
        this.strCodigosAnexos = strCodigosAnexos;
        InitializeComponent();

        InitData();
        pictureBoxLogo.Image = cDados.ObtemLogoEntidade();
    }

    private void InitData()
    {
        #region Comando SQL
        string comandoSql = string.Format(@"

DECLARE @CodigoProjeto Int,
        @NumeroFotos SmallInt,
        @UtilizaCalculoSaldoPorValorPago Char(1)
		
		SELECT @UtilizaCalculoSaldoPorValorPago = Valor
		  FROM {0}.{1}.ParametroConfiguracaoSistema
		 WHERE Parametro = 'calculaSaldoContratualPorValorContrato'
		   AND CodigoEntidade = {4}

    SET @CodigoProjeto = {2} --> Filtro para o código do projeto a ter o relatório emitido

 SELECT TOP 1
        @CodigoProjeto AS CodigoProjeto, 
        un.NomeUnidadeNegocio AS UnidadeResponsavel,
        p.NomeProjeto,
        (rp.PercentualPrevistoRealizacao * 100) AS PercentualPrevistoRealizacao,
        (rp.PercentualRealizacao * 100) AS PercentualRealizacao,
        CASE WHEN rp.PercentualPrevistoRealizacao <> 0 THEN Convert(Decimal(5,2),rp.PercentualRealizacao/rp.PercentualPrevistoRealizacao) ELSE 0 END AS DesempenhoFinanceiro,
        rp.InicioLB AS InicioPrevisto,
        rp.InicioReal AS InicioReal,
        rp.TerminoLB AS TerminoPrevisto,
        CASE WHEN rp.TerminoReprogramado <> rp.TerminoLB THEN rp.TerminoReprogramado ELSE NULL END AS TerminoReprogramadoFiscalizacao,
        IsNull(c.ValorContrato,0) AS ValorContratado,
        IsNull((SELECT SUM(pc.ValorPrevisto)
                  FROM {0}.{1}.ParcelaContrato AS pc
                 WHERE pc.CodigoContrato = c.CodigoContrato AND pc.[DataExclusao] IS NULL),0) AS ValorMedido,
        IsNull(c.ValorContrato,0) - IsNull((SELECT CASE WHEN ISNULL(@UtilizaCalculoSaldoPorValorPago, 'N') = 'S' THEN ISNULL(SUM(pc.[ValorPago]),0) ELSE ISNULL(SUM(pc.[ValorPrevisto]),0) END
                                              FROM {0}.{1}.ParcelaContrato AS pc
                                             WHERE pc.CodigoContrato = c.CodigoContrato AND pc.[DataExclusao] IS NULL),0) AS SaldoContratual,
        CodigoContrato 
   FROM {0}.{1}.ResumoProjeto AS rp INNER JOIN
        {0}.{1}.Projeto AS p ON (p.CodigoProjeto = rp.CodigoProjeto AND 
                                 p.DataExclusao IS NULL) INNER JOIN
        {0}.{1}.UnidadeNegocio AS un ON (un.CodigoUnidadeNegocio = p.CodigoUnidadeNegocio) LEFT JOIN
        {0}.{1}.Contrato AS c ON (c.CodigoProjeto = p.CodigoProjeto 
                                      and c.tipoPessoa = 'F') LEFT JOIN
		{0}.{1}.Pessoa pe ON pe.CodigoPessoa = c.CodigoPessoaContratada LEFT JOIN 
        {0}.{1}.[PessoaEntidade] AS [pen] ON (
	    pen.[CodigoPessoa] = c.[CodigoPessoaContratada]
	    AND pen.codigoEntidade = c.codigoEntidade
        --AND pen.IndicaFornecedor = 'S'
	    ) 
  WHERE p.CodigoProjeto = @CodigoProjeto

     SELECT @CodigoProjeto AS CodigoProjeto,
            ca.Anexo
       FROM {0}.{1}.ConteudoAnexo AS ca
      WHERE ca.codigoSequencialAnexo IN ({3})"
            , cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, strCodigosAnexos, cDados.getInfoSistema("CodigoEntidade").ToString());

        #endregion

        string[] tableNames = new string[] { "DadosProjeto", "FotosProjeto" };
        DataSet dsTemp = cDados.getDataSet(comandoSql);
        ds.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, tableNames);
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
        string resourceFileName = "rel_ResumoProjeto.resx";
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
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.parametroLabelValorMedido = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrChart2 = new DevExpress.XtraReports.UI.XRChart();
        this.ds = new DsResumoProjeto();
        this.xrChart3 = new DevExpress.XtraReports.UI.XRChart();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.pictureBoxLogo = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,
            this.xrLine3,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLine2,
            this.xrLabel19});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 800F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrPanel4,
            this.xrLabel3,
            this.xrPanel3,
            this.xrLabel4,
            this.xrLabel2,
            this.xrPanel2,
            this.xrLabel1,
            this.xrChart2,
            this.xrChart3,
            this.xrPanel5});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1900F, 452.7909F);
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.ValorMedido", "{0:n2}")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(1250F, 399.9998F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(254F, 40.00015F);
        this.xrLabel7.Text = "xrLabel6";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.ValorContratado", "{0:n2}")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1250F, 349.9999F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(254F, 40.00015F);
        this.xrLabel6.Text = "xrLabel6";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(990F, 349.9999F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(260F, 40F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "Valor contratado:";
        // 
        // xrPanel4
        // 
        this.xrPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.LocationFloat = new DevExpress.Utils.PointFloat(950F, 350F);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.SizeF = new System.Drawing.SizeF(40F, 40F);
        this.xrPanel4.StylePriority.UseBackColor = false;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(40F, 350F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(860F, 40F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Percentual previsto";
        // 
        // xrPanel3
        // 
        this.xrPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 350F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(40F, 40F);
        this.xrPanel3.StylePriority.UseBackColor = false;
        // 
        // xrLabel4
        // 
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.parametroLabelValorMedido, "Text", "")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(990F, 400.0001F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(259.9999F, 39.99994F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "labelValorMedido";
        // 
        // parametroLabelValorMedido
        // 
        this.parametroLabelValorMedido.Description = "parametroLabelValorMedido";
        this.parametroLabelValorMedido.Name = "parametroLabelValorMedido";
        this.parametroLabelValorMedido.Visible = false;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(40.00006F, 400F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(860F, 39.99997F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "Percentual realizado";
        // 
        // xrPanel2
        // 
        this.xrPanel2.BackColor = System.Drawing.Color.Silver;
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 400F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(40F, 40F);
        this.xrPanel2.StylePriority.UseBackColor = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 310F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(254F, 40F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Legenda";
        // 
        // xrChart2
        // 
        this.xrChart2.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart2.DataMember = "DadosProjeto";
        this.xrChart2.DataSource = this.ds;
        xyDiagram1.AxisX.Label.Visible = false;
        xyDiagram1.AxisX.MinorCount = 1;
        xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram1.AxisX.Title.Text = "Físico (%)";
        xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram1.AxisY.MinorCount = 1;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.VisualRange.Auto = false;
        xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.VisualRange.MinValueSerializable = "0";
        xyDiagram1.AxisY.WholeRange.Auto = false;
        xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "100";
        xyDiagram1.AxisY.WholeRange.MinValueSerializable = "0";
        xyDiagram1.Rotated = true;
        this.xrChart2.Diagram = xyDiagram1;
        this.xrChart2.Dpi = 254F;
        this.xrChart2.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrChart2.Name = "xrChart2";
        this.xrChart2.PaletteName = "Custom";
        this.xrChart2.PaletteRepository.Add("Custom", new DevExpress.XtraCharts.Palette("Custom", DevExpress.XtraCharts.PaletteScaleMode.Repeat, new DevExpress.XtraCharts.PaletteEntry[] {
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.Yellow, System.Drawing.Color.Yellow),
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))))}));
        series1.ArgumentDataMember = "CodigoProjeto";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series1.Label = sideBySideBarSeriesLabel1;
        series1.Name = "SeriePrevisto";
        series1.ValueDataMembersSerializable = "PercentualRealizacao";
        sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series1.View = sideBySideBarSeriesView1;
        series2.ArgumentDataMember = "CodigoProjeto";
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series2.Label = sideBySideBarSeriesLabel2;
        series2.Name = "SerieRealizado";
        series2.ValueDataMembersSerializable = "PercentualPrevistoRealizacao";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series2.View = sideBySideBarSeriesView2;
        this.xrChart2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
        sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart2.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
        this.xrChart2.SizeF = new System.Drawing.SizeF(900F, 300F);
        // 
        // ds
        // 
        this.ds.DataSetName = "DsResumoProjeto";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrChart3
        // 
        this.xrChart3.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrChart3.DataMember = "DadosProjeto";
        this.xrChart3.DataSource = this.ds;
        xyDiagram2.AxisX.Label.Visible = false;
        xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        xyDiagram2.AxisX.Title.Text = "Contrato (R$)";
        xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram2.AxisY.Label.Angle = 15;
        xyDiagram2.AxisY.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        xyDiagram2.AxisY.Label.TextPattern = "{V:N0}";
        xyDiagram2.AxisY.MinorCount = 1;
        xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram2.Rotated = true;
        this.xrChart3.Diagram = xyDiagram2;
        this.xrChart3.Dpi = 254F;
        this.xrChart3.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart3.LocationFloat = new DevExpress.Utils.PointFloat(950F, 0F);
        this.xrChart3.Name = "xrChart3";
        this.xrChart3.PaletteName = "Custom";
        this.xrChart3.PaletteRepository.Add("Custom", new DevExpress.XtraCharts.Palette("Custom", DevExpress.XtraCharts.PaletteScaleMode.Repeat, new DevExpress.XtraCharts.PaletteEntry[] {
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.Yellow, System.Drawing.Color.Yellow),
                new DevExpress.XtraCharts.PaletteEntry(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))))}));
        series3.ArgumentDataMember = "CodigoProjeto";
        series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.Label = sideBySideBarSeriesLabel4;
        series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series3.Name = "SeriePrevisto";
        series3.ValueDataMembersSerializable = "ValorMedido";
        sideBySideBarSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
        sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series3.View = sideBySideBarSeriesView3;
        series4.ArgumentDataMember = "CodigoProjeto";
        series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
        sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
        series4.Label = sideBySideBarSeriesLabel5;
        series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
        series4.Name = "SerieRealizado";
        series4.ValueDataMembersSerializable = "ValorContratado";
        sideBySideBarSeriesView4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
        sideBySideBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series4.View = sideBySideBarSeriesView4;
        this.xrChart3.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
        sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart3.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
        this.xrChart3.SizeF = new System.Drawing.SizeF(900F, 300F);
        // 
        // xrPanel5
        // 
        this.xrPanel5.BackColor = System.Drawing.Color.Silver;
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.LocationFloat = new DevExpress.Utils.PointFloat(950F, 400F);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.SizeF = new System.Drawing.SizeF(40F, 40F);
        this.xrPanel5.StylePriority.UseBackColor = false;
        // 
        // xrLine3
        // 
        this.xrLine3.Dpi = 254F;
        this.xrLine3.LineWidth = 3;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 700.746F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(1900F, 5.08F);
        // 
        // xrLabel18
        // 
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.TerminoReprogramadoFiscalizacao", "{0:dd/MM/yyyy}")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(1320.8F, 637.246F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(254F, 38.09998F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.Text = "xrLabel1";
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(762F, 637.246F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(558.8F, 38.09998F);
        this.xrLabel17.Text = "Término Reprogramado (fiscalização):  ";
        // 
        // xrLabel16
        // 
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(762F, 573.746F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(268.2875F, 38.1001F);
        this.xrLabel16.Text = "Término Previsto: ";
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.TerminoPrevisto", "{0:dd/MM/yyyy}")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(1031F, 573.746F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(254F, 38.09998F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "xrLabel1";
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.InicioReal", "{0:dd/MM/yyyy}")});
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(177.8F, 637.246F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(254F, 38.10004F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.Text = "xrLabel1";
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 637.246F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(177.8F, 38.09998F);
        this.xrLabel13.Text = "Início Real: ";
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 573.746F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(228.6F, 38.09998F);
        this.xrLabel12.Text = "Início Previsto: ";
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.InicioPrevisto", "{0:dd/MM/yyyy}")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(228.6F, 573.746F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(254F, 38.10004F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "xrLabel1";
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 510.246F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(635F, 38.09998F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = "Dados do projeto/ Obra: ";
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 480F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1900F, 5.08F);
        // 
        // xrLabel19
        // 
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 761.9F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(406.8F, 38.09998F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.Text = "Registros Fotográficos: ";
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
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
        this.DetailReport.DataMember = "DadosProjeto.FK_DadosProjeto_FotosProjeto";
        this.DetailReport.DataSource = this.ds;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 750F;
        this.Detail1.MultiColumn.ColumnCount = 2;
        this.Detail1.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        this.Detail1.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
        this.Detail1.Name = "Detail1";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "DadosProjeto.FK_DadosProjeto_FotosProjeto.Anexo")});
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(900F, 700F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox1.StylePriority.UseBorderColor = false;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        this.xrPictureBox1.StylePriority.UseBorderWidth = false;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.pictureBoxLogo,
            this.xrLabel21,
            this.xrLabel20});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 250F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 200F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1900F, 5.08F);
        // 
        // pictureBoxLogo
        // 
        this.pictureBoxLogo.Dpi = 254F;
        this.pictureBoxLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
        this.pictureBoxLogo.Name = "pictureBoxLogo";
        this.pictureBoxLogo.SizeF = new System.Drawing.SizeF(325F, 135F);
        this.pictureBoxLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel21
        // 
        this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.UnidadeResponsavel", "Unidade: {0}")});
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 12F);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(354F, 101.58F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(1546F, 58.41999F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = "xrLabel21";
        // 
        // xrLabel20
        // 
        this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosProjeto.NomeProjeto", "Projeto: {0}")});
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(354F, 25.00001F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(1546F, 58.42F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "xrLabel20";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 58.42007F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1646F, 18.42006F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(254F, 40F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // rel_ResumoProjeto
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.PageHeader,
            this.PageFooter});
        this.DataMember = "DadosProjeto";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parametroLabelValorMedido});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "15.1";
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
