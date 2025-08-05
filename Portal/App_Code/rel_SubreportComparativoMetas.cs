using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rel_SubresportComparativoMetas
/// </summary>
public class rel_SubreportComparativoMetas : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private System.Data.DataSet ds;
    private System.Data.DataTable dataTable1;
    private System.Data.DataColumn dataColumn1;
    private System.Data.DataColumn dataColumn2;
    private System.Data.DataColumn dataColumn3;
    private System.Data.DataColumn dataColumn4;
    private System.Data.DataColumn dataColumn5;
    private System.Data.DataColumn dataColumn6;
    private System.Data.DataColumn dataColumn7;
    private System.Data.DataColumn dataColumn8;
    public DevExpress.XtraReports.Parameters.Parameter CodigoStatusReport;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRLabel xrLabel1;
    private XRLabel xrLabel10;
    private XRControlStyle xrControlStyle1;
    private XRControlStyle xrControlStyle2;

    dados cDados = CdadosUtil.GetCdados(null);
    private System.Data.DataColumn dataColumn9;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_SubreportComparativoMetas(int codigoStatusReport)
    {
        InitializeComponent();
        this.CodigoStatusReport.Value = codigoStatusReport;
        InitData();
    }

    private void InitData()
    {
        #region Comando SQL
        string comandoSql = string.Format(@"
DECLARE @CodigoTipoAssociacao Int, 
		@CodigoStatusReport Int            

 SELECT @CodigoStatusReport = {0}
 SELECT @CodigoTipoAssociacao = CodigoTipoAssociacao FROM TipoAssociacao WHERE IniciaisTipoAssociacao = 'PR'

 SELECT @CodigoStatusReport AS CodigoStatusReport,
        ISNULL(cat.[CodigoCategoria],0) AS [CodigoCategoria], 
		ISNULL(cat.[DescricaoCategoria],'-') AS [DescricaoCategoria], 
		i.[CodigoIndicador] AS [CodigoIndicador], 
		i.[NomeIndicador] AS [NomeIndicador],
		tum.[DescricaoUnidadeMedida_PT] AS [DescricaoUnidadeMedida], 
		tum.[SiglaUnidadeMedida] AS [SiglaUnidadeMedida], 
		rio.[MetaAcumuladaAno] AS [ValorMeta], 
		rio.[ResultadoAcumuladoAno] AS [ValorResultado]
   FROM [dbo].[StatusReport] AS [sr] INNER JOIN
		[dbo].[Projeto] AS [p] ON ( p.[CodigoProjeto] = sr.[CodigoProjeto]) INNER JOIN 
		[dbo].[ResumoIndicadorOperacional] AS [rio] ON (rio.[CodigoProjeto] = p.[CodigoProjeto] )INNER JOIN  
		[dbo].[IndicadorOperacional] AS [i] ON i.[CodigoIndicador] = rio.[CodigoIndicador] INNER JOIN  
		[dbo].[TipoUnidadeMedida] AS [tum]  ON tum.[CodigoUnidadeMedida] = i.[CodigoUnidadeMedida] LEFT JOIN 
		[dbo].[Categoria] AS [cat] ON cat.[CodigoCategoria] = p.[CodigoCategoria]
  WHERE sr.[CodigoStatusReport] = @CodigoStatusReport
	AND CONVERT(Datetime, '01/' + CAST(rio.[Mes] AS Varchar(2)) + '/' + CAST(rio.[Ano] AS Varchar(4)), 103) BETWEEN sr.[DataInicioPeriodoRelatorio] AND sr.[DataTerminoPeriodoRelatorio]
  ORDER BY
		2, 
		4", CodigoStatusReport.Value);
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
        //string resourceFileName = "rel_SubreportComparativoMetas.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.CodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.xrTable2.SizeF = new System.Drawing.SizeF(2369F, 63.5F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5679012345679012D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.DescricaoCategoria")});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 1.4482415673267339D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.NomeIndicador")});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.Weight = 2.2907160115888585D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.DescricaoUnidadeMedida")});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.Weight = 0.96719120489307364D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorMeta", "{0:n2}")});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell15.Weight = 0.6617624033478926D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Table1.ValorResultado", "{0:n2}")});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell16.Weight = 0.66176240334789249D;
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
            this.dataColumn9});
        this.dataTable1.TableName = "Table1";
        // 
        // dataColumn1
        // 
        this.dataColumn1.ColumnName = "CodigoCategoria";
        this.dataColumn1.DataType = typeof(int);
        // 
        // dataColumn2
        // 
        this.dataColumn2.ColumnName = "DescricaoCategoria";
        // 
        // dataColumn3
        // 
        this.dataColumn3.ColumnName = "CodigoIndicador";
        this.dataColumn3.DataType = typeof(int);
        // 
        // dataColumn4
        // 
        this.dataColumn4.ColumnName = "NomeIndicador";
        // 
        // dataColumn5
        // 
        this.dataColumn5.ColumnName = "DescricaoUnidadeMedida";
        // 
        // dataColumn6
        // 
        this.dataColumn6.ColumnName = "SiglaUnidadeMedida";
        // 
        // dataColumn7
        // 
        this.dataColumn7.ColumnName = "ValorMeta";
        this.dataColumn7.DataType = typeof(decimal);
        // 
        // dataColumn8
        // 
        this.dataColumn8.ColumnName = "ValorResultado";
        this.dataColumn8.DataType = typeof(decimal);
        // 
        // dataColumn9
        // 
        this.dataColumn9.ColumnName = "CodigoStatusReport";
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
        this.ReportHeader.HeightF = 233.5F;
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
        this.xrLabel10.Text = "2 - COMPARATIVO ENTRE AS METAS PREVISTAS E REALIZADAS";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(2369F, 58.42001F);
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "QUADRO 1 - COMPARATIVO ENTRE AS METAS PREVISTAS E REALIZADAS  Nº TRIMESTRE";
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
        this.xrTable1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 170F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(2369F, 63.5F);
        this.xrTable1.StylePriority.UseBackColor = false;
        this.xrTable1.StylePriority.UseBorderColor = false;
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "Área Temática";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell3.Weight = 1.4482415673267339D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "Indicador";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell5.Weight = 2.2907160115888585D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "Unidade de Medida";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell6.Weight = 0.96719120489307364D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Meta";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell7.Weight = 0.6617624033478926D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "Realizado";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell8.Weight = 0.66176240334789249D;
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
        // rel_SubreportComparativoMetas
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
        this.DataMember = "Table1";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.FilterString = "[CodigoStatusReport] Like Concat(?CodigoStatusReport, \'%\')";
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
