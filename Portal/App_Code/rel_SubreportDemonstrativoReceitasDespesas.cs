using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rel_SubresportComparativoMetas
/// </summary>
public class rel_SubreportDemonstrativoReceitasDespesas : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private System.Data.DataSet ds;
    private System.Data.DataTable dataTable1;
    private System.Data.DataColumn dataColumn1;
    public DevExpress.XtraReports.Parameters.Parameter CodigoStatusReport;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell16;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell8;
    private XRLabel xrLabel1;
    private XRLabel xrLabel10;
    private XRControlStyle xrControlStyle1;
    private XRControlStyle xrControlStyle2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;

    dados cDados = CdadosUtil.GetCdados(null);
    private System.Data.DataColumn dataColumn2;
    private System.Data.DataColumn dataColumn3;
    private System.Data.DataColumn dataColumn4;
    private System.Data.DataColumn dataColumn5;
    private System.Data.DataColumn dataColumn6;
    private System.Data.DataColumn dataColumn7;
    private System.Data.DataColumn dataColumn8;
    private System.Data.DataColumn dataColumn9;
    private System.Data.DataColumn dataColumn10;
    private System.Data.DataColumn dataColumn11;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_SubreportDemonstrativoReceitasDespesas(int codigoStatusReport)
    {
        InitializeComponent();
        this.CodigoStatusReport.Value = codigoStatusReport;
        InitData();
    }

    private void InitData()
    {
        #region Comando SQL
        string comandoSql = string.Format(@"

BEGIN
	DECLARE 
			@CodigoStatusReport				Int
		, @CodigoProjeto						Int       
		, @DataIniPerRel						Datetime
		, @DataFimPerRel						Datetime
		
	SELECT @CodigoStatusReport		= {0}
        	
	SELECT @CodigoProjeto = sr.[CodigoProjeto], @DataIniPerRel = sr.[DataInicioPeriodoRelatorio], @DataFimPerRel = sr.[DataTerminoPeriodoRelatorio]
	 FROM [dbo].[StatusReport] AS [sr] WHERE sr.[CodigoStatusReport] = @CodigoStatusReport
	 
	DECLARE @Valores TABLE
		(		[DespesaReceita]			Varchar(20)
			, [Grupo]								Varchar(255)
			, [ValorPrevistoMes1]		Decimal(25,4)
			, [ValorPrevistoMes2]		Decimal(25,4)
			, [ValorPrevistoMes3]		Decimal(25,4)
			, [ValorRealMes1]				Decimal(25,4)
			, [ValorRealMes2]				Decimal(25,4)
			, [ValorRealMes3]				Decimal(25,4)
		)
	
	INSERT INTO @Valores 
		(		[DespesaReceita]			
			, [Grupo]								
			, [ValorPrevistoMes1]		
			, [ValorPrevistoMes2]		
			, [ValorPrevistoMes3]		
		)
		SELECT 
				[DespesaReceita], [Grupo], [7], [8], [9]
				FROM
					(	SELECT 
								CAST( CASE [DespesaReceita] WHEN 'D' THEN 'Despesas' WHEN 'R' THEN 'Receitas' ELSE NULL END AS Varchar(20) ) AS [DespesaReceita]
							,	[DescricaoGrupoConta]					AS [Grupo]
							, [Mes] 
						,	[ValorOrcado]
						FROM dbo.f_GetOrcamentoProjeto(@CodigoProjeto, year(@DataIniPerRel))
						WHERE 
							CONVERT(DateTime, '28/' + CAST(Mes AS Varchar(2)) + '/' + CAST(Ano AS Varchar(4) ), 103) BETWEEN @DataIniPerRel and @DataFimPerRel
							AND [ValorOrcado] != 0 
					) AS [tabDados]
					PIVOT ( SUM([ValorOrcado]) FOR [Mes] IN ([7], [8], [9]) ) AS tabOlap
				
	INSERT INTO @Valores 
		(		[DespesaReceita]			
			, [Grupo]								
			, [ValorRealMes1]		
			, [ValorRealMes2]		
			, [ValorRealMes3]		
		)
		SELECT 
				[DespesaReceita], [Grupo], [7], [8], [9]
				FROM
					(	SELECT 
								CAST( CASE [DespesaReceita] WHEN 'D' THEN 'Despesas' WHEN 'R' THEN 'Receitas' ELSE NULL END AS Varchar(20) ) AS [DespesaReceita]
							,	[DescricaoGrupoConta]					AS [Grupo]
							, [Mes] 
						,	[ValorReal]
						FROM dbo.f_GetOrcamentoProjeto(@CodigoProjeto, year(@DataIniPerRel))
						WHERE 
							CONVERT(DateTime, '28/' + CAST(Mes AS Varchar(2)) + '/' + CAST(Ano AS Varchar(4) ), 103) BETWEEN @DataIniPerRel and @DataFimPerRel
							AND [ValorReal] != 0
					) AS [tabDados]
					PIVOT ( SUM([ValorReal]) FOR [Mes] IN ([7], [8], [9]) ) AS tabOlap

		SELECT 
					@CodigoStatusReport AS CodigoStatusReport 
				,	[DespesaReceita]			
				, [Grupo]								
				, SUM([ValorPrevistoMes1])		AS [ValorPrevistoMes1]
				,	SUM([ValorPrevistoMes2])		AS [ValorPrevistoMes2]
				,	SUM([ValorPrevistoMes3])		AS [ValorPrevistoMes3]
				, ISNULL(SUM([ValorPrevistoMes1]),0) +	ISNULL(SUM([ValorPrevistoMes2]),0) +	ISNULL(SUM([ValorPrevistoMes3]),0)	AS [ValorPrevistoTotal]
				,	SUM([ValorRealMes1])				AS [ValorRealMes1]
				,	SUM([ValorRealMes2])				AS [ValorRealMes2]
				,	SUM([ValorRealMes3])				AS [ValorRealMes3]
				, ISNULL(SUM([ValorRealMes1]),0) +	ISNULL(SUM([ValorRealMes2]),0) +	ISNULL(SUM([ValorRealMes3]),0)	AS [ValorRealTotal]
			FROM
				@Valores
			GROUP BY 
					[DespesaReceita]			
				, [Grupo]								
END   ", CodigoStatusReport.Value);
        #endregion

        ds.Load(cDados.getDataSet(comandoSql).CreateDataReader(), System.Data.LoadOption.OverwriteChanges, "Table1");
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
        //string resourceFileName = "rel_SubreportDemonstrativoReceitasDespesas.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ds = new System.Data.DataSet();
        this.dataTable1 = new System.Data.DataTable();
        this.dataColumn1 = new System.Data.DataColumn();
        this.dataColumn2 = new System.Data.DataColumn();
        this.dataColumn3 = new System.Data.DataColumn();
        this.dataColumn4 = new System.Data.DataColumn();
        this.dataColumn5 = new System.Data.DataColumn();
        this.dataColumn6 = new System.Data.DataColumn();
        this.dataColumn7 = new System.Data.DataColumn();
        this.dataColumn8 = new System.Data.DataColumn();
        this.dataColumn9 = new System.Data.DataColumn();
        this.dataColumn10 = new System.Data.DataColumn();
        this.dataColumn11 = new System.Data.DataColumn();
        this.CodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 63.5F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.BorderColor = System.Drawing.Color.White;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.EvenStyleName = "xrControlStyle1";
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(2368.995F, 63.5F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell15,
            this.xrTableCell14,
            this.xrTableCell18,
            this.xrTableCell13,
            this.xrTableCell21,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell16});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5679012345679012D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.Grupo")});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 1.4482415673267339D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorPrevistoMes1", "{0:n2}")});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell15.Weight = 0.57267900289721474D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorPrevistoMes2", "{0:n2}")});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell14.Weight = 0.57267900289721474D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorPrevistoMes3", "{0:n2}")});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell18.Weight = 0.57267900289721474D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorPrevistoTotal", "{0:n2}")});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell13.Weight = 0.57267900289721474D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorRealMes1", "{0:n2}")});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell21.Weight = 0.57267589591477708D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorRealMes2", "{0:n2}")});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell19.Weight = 0.57267589591477708D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorRealMes3", "{0:n2}")});
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell20.Weight = 0.57267589591477708D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorRealTotal", "{0:n2}")});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell16.Weight = 0.57267589591477708D;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 249F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 249F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ds
        // 
        this.ds.DataSetName = "NewDataSet";
        this.ds.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
        // 
        // dataTable1
        // 
        this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11});
        this.dataTable1.TableName = "Table1";
        // 
        // dataColumn1
        // 
        this.dataColumn1.ColumnName = "CodigoStatusReport";
        this.dataColumn1.DataType = typeof(int);
        // 
        // dataColumn2
        // 
        this.dataColumn2.ColumnName = "DespesaReceita";
        // 
        // dataColumn3
        // 
        this.dataColumn3.ColumnName = "Grupo";
        // 
        // dataColumn4
        // 
        this.dataColumn4.ColumnName = "ValorPrevistoMes1";
        this.dataColumn4.DataType = typeof(decimal);
        // 
        // dataColumn5
        // 
        this.dataColumn5.ColumnName = "ValorPrevistoMes2";
        this.dataColumn5.DataType = typeof(decimal);
        // 
        // dataColumn6
        // 
        this.dataColumn6.ColumnName = "ValorPrevistoMes3";
        this.dataColumn6.DataType = typeof(decimal);
        // 
        // dataColumn7
        // 
        this.dataColumn7.ColumnName = "ValorRealMes1";
        this.dataColumn7.DataType = typeof(decimal);
        // 
        // dataColumn8
        // 
        this.dataColumn8.ColumnName = "ValorRealMes2";
        this.dataColumn8.DataType = typeof(decimal);
        // 
        // dataColumn9
        // 
        this.dataColumn9.ColumnName = "ValorRealMes3";
        this.dataColumn9.DataType = typeof(decimal);
        // 
        // dataColumn10
        // 
        this.dataColumn10.ColumnName = "ValorPrevistoTotal";
        this.dataColumn10.DataType = typeof(decimal);
        // 
        // dataColumn11
        // 
        this.dataColumn11.ColumnName = "ValorRealTotal";
        this.dataColumn11.DataType = typeof(decimal);
        // 
        // CodigoStatusReport
        // 
        this.CodigoStatusReport.Name = "CodigoStatusReport";
        this.CodigoStatusReport.Type = typeof(int);
        this.CodigoStatusReport.ValueInfo = "0";
        this.CodigoStatusReport.Visible = false;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel1,
            this.xrTable1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 297F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(2369F, 58.41992F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "4 - DEMONSTRATIVO DE RECEITAS E DESPESAS DO PERÍODO";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(2369F, 58.42001F);
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "QUADRO 3 - COMPARATIVO DE RECEITAS E DESPESAS NO PERÍODO";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrTable1
        // 
        this.xrTable1.BackColor = System.Drawing.Color.LightGray;
        this.xrTable1.BorderColor = System.Drawing.Color.White;
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 170F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow3});
        this.xrTable1.SizeF = new System.Drawing.SizeF(2368.995F, 127F);
        this.xrTable1.StylePriority.UseBackColor = false;
        this.xrTable1.StylePriority.UseBorderColor = false;
        this.xrTable1.StylePriority.UseBorders = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell8});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "CATEGORIA";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell3.Weight = 1.4482415673267339D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "Previsto (R$)";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell5.Weight = 2.290716011588859D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "Realizado (R$)";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell8.Weight = 2.2907035836591083D;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell2,
            this.xrTableCell12,
            this.xrTableCell9,
            this.xrTableCell17,
            this.xrTableCell10});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.5679012345679012D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "CONTÁBIL";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell1.Weight = 1.4482415673267339D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Mês 1";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell4.Weight = 0.572679002897215D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "Mês 2";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell6.Weight = 0.57267900289721485D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Mês 3";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell7.Weight = 0.57267900289721485D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Total";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell2.Weight = 0.57267900289721485D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "Mês 1";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell12.Weight = 0.57267589591477708D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "Mês 2";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell9.Weight = 0.57267589591477708D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "Mês 3";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell17.Weight = 0.57267589591477708D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Total";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell10.Weight = 0.57267589591477708D;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.BackColor = System.Drawing.Color.Gainsboro;
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // xrControlStyle2
        // 
        this.xrControlStyle2.Name = "xrControlStyle2";
        this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // rel_SubreportDemonstrativoReceitasDespesas
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
        this.DataMember = "Table1";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.FilterString = "[CodigoStatusReport] = ?CodigoStatusReport";
        this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(300, 300, 249, 249);
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CodigoStatusReport});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2});
        this.Version = "14.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
