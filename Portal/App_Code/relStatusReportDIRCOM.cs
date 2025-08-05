using DevExpress.XtraReports.UI;
using System;
using System.Data.SqlClient;

/// <summary>
/// Summary description for relStatusReportDIRCOM
/// </summary>
public class relStatusReportDIRCOM : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail1;
    private TopMarginBand TopMargin1;
    private BottomMarginBand BottomMargin1;
    private XRCrossBandBox xrCrossBandBox2;
    private XRCrossBandBox xrCrossBandBox1;
    private XRControlStyle styleInfo;
    private XRControlStyle styleTitulo;
    public DevExpress.XtraReports.Parameters.Parameter pTextoDoCabecalhoPretoDoRelatorio;
    dados cDados;
    public DevExpress.XtraReports.Parameters.Parameter pSiglaEntidade;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoConsultor;
    public DevExpress.XtraReports.Parameters.Parameter pDataInicioPeriodoTarefa;
    public DevExpress.XtraReports.Parameters.Parameter pDataFimPeriodoTarefa;
    public DevExpress.XtraReports.Parameters.Parameter pStatusBriefing;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoUnidadeNegocio;
    private dsStatusReportDIRCOM dsStatusReportDIRCOM1;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell celulaDataBriefing;
    private XRTableCell celulaNomeProjeto;
    private XRTableCell celulaNomeGerenteProjeto;
    private XRTableCell celulaDataInicioTarefa;
    private XRTableCell celulaNomeTarefa;
    private XRTableCell celulaRecursosAlocadosTarefa;
    private XRTableCell celulaDataTerminoTarefa;
    private XRTableCell celulaUnidadeDemandante;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell4;
    private DevExpress.XtraReports.Parameters.Parameter pStatusProjeto;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relStatusReportDIRCOM()
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
        string resourceFileName = "relStatusReportDIRCOM.resx";
        System.Resources.ResourceManager resources = global::Resources.relStatusReportDIRCOM.ResourceManager;
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celulaUnidadeDemandante = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaDataBriefing = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaNomeProjeto = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaNomeGerenteProjeto = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaDataInicioTarefa = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaNomeTarefa = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaRecursosAlocadosTarefa = new DevExpress.XtraReports.UI.XRTableCell();
        this.celulaDataTerminoTarefa = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.pStatusBriefing = new DevExpress.XtraReports.Parameters.Parameter();
        this.pSiglaEntidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.pDataInicioPeriodoTarefa = new DevExpress.XtraReports.Parameters.Parameter();
        this.pDataFimPeriodoTarefa = new DevExpress.XtraReports.Parameters.Parameter();
        this.pCodigoUnidadeNegocio = new DevExpress.XtraReports.Parameters.Parameter();
        this.pCodigoConsultor = new DevExpress.XtraReports.Parameters.Parameter();
        this.TopMargin1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrCrossBandBox2 = new DevExpress.XtraReports.UI.XRCrossBandBox();
        this.xrCrossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
        this.styleInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.styleTitulo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.pTextoDoCabecalhoPretoDoRelatorio = new DevExpress.XtraReports.Parameters.Parameter();
        this.dsStatusReportDIRCOM1 = new dsStatusReportDIRCOM();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.pStatusProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsStatusReportDIRCOM1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail1.HeightF = 14.99999F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1095F, 14.99999F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celulaUnidadeDemandante,
            this.celulaDataBriefing,
            this.celulaNomeProjeto,
            this.celulaNomeGerenteProjeto,
            this.celulaDataInicioTarefa,
            this.celulaNomeTarefa,
            this.celulaRecursosAlocadosTarefa,
            this.celulaDataTerminoTarefa,
            this.xrTableCell5});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 11.5D;
        // 
        // celulaUnidadeDemandante
        // 
        this.celulaUnidadeDemandante.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaUnidadeDemandante.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtStatusReportDircom.UnidadeDemandante")});
        this.celulaUnidadeDemandante.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaUnidadeDemandante.Name = "celulaUnidadeDemandante";
        this.celulaUnidadeDemandante.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaUnidadeDemandante.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Merge;
        this.celulaUnidadeDemandante.StylePriority.UseBorders = false;
        this.celulaUnidadeDemandante.StylePriority.UseFont = false;
        this.celulaUnidadeDemandante.StylePriority.UsePadding = false;
        this.celulaUnidadeDemandante.Weight = 0.10726388809468421D;
        // 
        // celulaDataBriefing
        // 
        this.celulaDataBriefing.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaDataBriefing.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaDataBriefing.Name = "celulaDataBriefing";
        this.celulaDataBriefing.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaDataBriefing.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Merge;
        this.celulaDataBriefing.StylePriority.UseBorders = false;
        this.celulaDataBriefing.StylePriority.UseFont = false;
        this.celulaDataBriefing.StylePriority.UsePadding = false;
        this.celulaDataBriefing.Weight = 0.15323424250233933D;
        this.celulaDataBriefing.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaDataBriefing_BeforePrint);
        // 
        // celulaNomeProjeto
        // 
        this.celulaNomeProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaNomeProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtStatusReportDircom.NomeProjeto")});
        this.celulaNomeProjeto.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaNomeProjeto.Name = "celulaNomeProjeto";
        this.celulaNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaNomeProjeto.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Merge;
        this.celulaNomeProjeto.StylePriority.UseBorders = false;
        this.celulaNomeProjeto.StylePriority.UseFont = false;
        this.celulaNomeProjeto.StylePriority.UsePadding = false;
        this.celulaNomeProjeto.Weight = 0.15623117523963109D;
        // 
        // celulaNomeGerenteProjeto
        // 
        this.celulaNomeGerenteProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaNomeGerenteProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtStatusReportDircom.NomeGerenteProjeto")});
        this.celulaNomeGerenteProjeto.Name = "celulaNomeGerenteProjeto";
        this.celulaNomeGerenteProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaNomeGerenteProjeto.ProcessDuplicatesMode = DevExpress.XtraReports.UI.ProcessDuplicatesMode.Merge;
        this.celulaNomeGerenteProjeto.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;
        this.celulaNomeGerenteProjeto.StylePriority.UseBorders = false;
        this.celulaNomeGerenteProjeto.StylePriority.UsePadding = false;
        this.celulaNomeGerenteProjeto.Weight = 0.11896006739998807D;
        // 
        // celulaDataInicioTarefa
        // 
        this.celulaDataInicioTarefa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaDataInicioTarefa.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaDataInicioTarefa.Name = "celulaDataInicioTarefa";
        this.celulaDataInicioTarefa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaDataInicioTarefa.StylePriority.UseBorders = false;
        this.celulaDataInicioTarefa.StylePriority.UseFont = false;
        this.celulaDataInicioTarefa.StylePriority.UsePadding = false;
        this.celulaDataInicioTarefa.Weight = 0.16918769943988501D;
        this.celulaDataInicioTarefa.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaDataInicioTarefa_BeforePrint);
        // 
        // celulaNomeTarefa
        // 
        this.celulaNomeTarefa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaNomeTarefa.Name = "celulaNomeTarefa";
        this.celulaNomeTarefa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaNomeTarefa.StylePriority.UseBorders = false;
        this.celulaNomeTarefa.StylePriority.UsePadding = false;
        this.celulaNomeTarefa.StylePriority.UseTextAlignment = false;
        this.celulaNomeTarefa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celulaNomeTarefa.Weight = 0.16855754108682153D;
        this.celulaNomeTarefa.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaNomeTarefa_BeforePrint);
        // 
        // celulaRecursosAlocadosTarefa
        // 
        this.celulaRecursosAlocadosTarefa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaRecursosAlocadosTarefa.Name = "celulaRecursosAlocadosTarefa";
        this.celulaRecursosAlocadosTarefa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaRecursosAlocadosTarefa.StylePriority.UseBorders = false;
        this.celulaRecursosAlocadosTarefa.StylePriority.UsePadding = false;
        this.celulaRecursosAlocadosTarefa.Weight = 0.18388087619312812D;
        this.celulaRecursosAlocadosTarefa.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaRecursosAlocadosTarefa_BeforePrint);
        // 
        // celulaDataTerminoTarefa
        // 
        this.celulaDataTerminoTarefa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaDataTerminoTarefa.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celulaDataTerminoTarefa.Name = "celulaDataTerminoTarefa";
        this.celulaDataTerminoTarefa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.celulaDataTerminoTarefa.StylePriority.UseBorders = false;
        this.celulaDataTerminoTarefa.StylePriority.UseFont = false;
        this.celulaDataTerminoTarefa.StylePriority.UsePadding = false;
        this.celulaDataTerminoTarefa.Weight = 0.18388077760378413D;
        this.celulaDataTerminoTarefa.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaDataTerminoTarefa_BeforePrint);
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtStatusReportDircom.DiasUteisCorridos")});
        this.xrTableCell5.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Weight = 0.39506248118859927D;
        // 
        // pStatusBriefing
        // 
        this.pStatusBriefing.Name = "pStatusBriefing";
        // 
        // pSiglaEntidade
        // 
        this.pSiglaEntidade.Name = "pSiglaEntidade";
        // 
        // pDataInicioPeriodoTarefa
        // 
        this.pDataInicioPeriodoTarefa.Name = "pDataInicioPeriodoTarefa";
        // 
        // pDataFimPeriodoTarefa
        // 
        this.pDataFimPeriodoTarefa.Name = "pDataFimPeriodoTarefa";
        // 
        // pCodigoUnidadeNegocio
        // 
        this.pCodigoUnidadeNegocio.Name = "pCodigoUnidadeNegocio";
        // 
        // pCodigoConsultor
        // 
        this.pCodigoConsultor.Name = "pCodigoConsultor";
        // 
        // TopMargin1
        // 
        this.TopMargin1.HeightF = 59F;
        this.TopMargin1.Name = "TopMargin1";
        // 
        // BottomMargin1
        // 
        this.BottomMargin1.Font = new System.Drawing.Font("Arial", 12F);
        this.BottomMargin1.HeightF = 59F;
        this.BottomMargin1.Name = "BottomMargin1";
        this.BottomMargin1.Padding = new DevExpress.XtraPrinting.PaddingInfo(4, 2, 0, 0, 100F);
        this.BottomMargin1.StylePriority.UseFont = false;
        this.BottomMargin1.StylePriority.UsePadding = false;
        // 
        // xrCrossBandBox2
        // 
        this.xrCrossBandBox2.BorderWidth = 1F;
        this.xrCrossBandBox2.EndBand = null;
        this.xrCrossBandBox2.EndPointFloat = new DevExpress.Utils.PointFloat(0F, 370.0788F);
        this.xrCrossBandBox2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrCrossBandBox2.Name = "xrCrossBandBox2";
        this.xrCrossBandBox2.StartBand = null;
        this.xrCrossBandBox2.StartPointFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrCrossBandBox2.WidthF = 707.0866F;
        // 
        // xrCrossBandBox1
        // 
        this.xrCrossBandBox1.BorderWidth = 1F;
        this.xrCrossBandBox1.EndBand = null;
        this.xrCrossBandBox1.EndPointFloat = new DevExpress.Utils.PointFloat(0F, 283.4646F);
        this.xrCrossBandBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrCrossBandBox1.Name = "xrCrossBandBox1";
        this.xrCrossBandBox1.StartBand = null;
        this.xrCrossBandBox1.StartPointFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrCrossBandBox1.WidthF = 707.0866F;
        // 
        // styleInfo
        // 
        this.styleInfo.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.styleInfo.BorderWidth = 1F;
        this.styleInfo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.styleInfo.Name = "styleInfo";
        // 
        // styleTitulo
        // 
        this.styleTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
        this.styleTitulo.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.styleTitulo.BorderWidth = 1F;
        this.styleTitulo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.styleTitulo.Name = "styleTitulo";
        this.styleTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // pTextoDoCabecalhoPretoDoRelatorio
        // 
        this.pTextoDoCabecalhoPretoDoRelatorio.Name = "pTextoDoCabecalhoPretoDoRelatorio";
        // 
        // dsStatusReportDIRCOM1
        // 
        this.dsStatusReportDIRCOM1.DataSetName = "dsStatusReportDIRCOM";
        this.dsStatusReportDIRCOM1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("UnidadeDemandante", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 21.875F;
        this.GroupHeader2.KeepTogether = true;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.RepeatEveryPage = true;
        // 
        // xrTable3
        // 
        this.xrTable3.BackColor = System.Drawing.Color.Silver;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1095F, 21.875F);
        this.xrTable3.StylePriority.UseBackColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell4});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "ÁREA";
        this.xrTableCell1.Weight = 0.7774061147746546D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.Text = "ENTRADA DO JOB";
        this.xrTableCell2.Weight = 1.1105805416427546D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.Text = "PROJETO";
        this.xrTableCell3.Weight = 1.1323026699982541D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.Text = "RESPONSÁVEL";
        this.xrTableCell15.Weight = 0.86217437230728178D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.Text = "DATA DA DEMANDA";
        this.xrTableCell16.Weight = 1.2262059038555546D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell17.Multiline = true;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.Text = "DEMANDA\r\n";
        this.xrTableCell17.Weight = 1.2216381550374835D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.Text = "ONDE ESTÁ";
        this.xrTableCell18.Weight = 1.3326962919748415D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.Text = "PREVISÃO DE ENTREGA";
        this.xrTableCell19.Weight = 1.3326956178499279D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.Text = "DIAS ÚTEIS CORRIDOS DESDE O ÚLTIMO RETORNO";
        this.xrTableCell4.Weight = 2.863254800481311D;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("UnidadeDemandante", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader3.HeightF = 23F;
        this.GroupHeader3.KeepTogether = true;
        this.GroupHeader3.Level = 1;
        this.GroupHeader3.Name = "GroupHeader3";
        this.GroupHeader3.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBandExceptFirstEntry;
        this.GroupHeader3.RepeatEveryPage = true;
        // 
        // xrLabel1
        // 
        this.xrLabel1.BackColor = System.Drawing.Color.Black;
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pTextoDoCabecalhoPretoDoRelatorio, "Text", "")});
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.ForeColor = System.Drawing.Color.White;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1095F, 23F);
        this.xrLabel1.StylePriority.UseBackColor = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // pStatusProjeto
        // 
        this.pStatusProjeto.Name = "pStatusProjeto";
        // 
        // relStatusReportDIRCOM
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin1,
            this.Detail1,
            this.BottomMargin1,
            this.GroupHeader2,
            this.GroupHeader3});
        this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandBox2,
            this.xrCrossBandBox1});
        this.DataMember = "dtStatusReportDircom";
        this.DataSource = this.dsStatusReportDIRCOM1;
        this.Extensions.Add("DataSerializationExtension", "DevExpress.XtraReports.Web.ReportDesigner.DefaultDataSerializer");
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(34, 40, 59, 59);
        this.PageHeight = 827;
        this.PageWidth = 1169;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pTextoDoCabecalhoPretoDoRelatorio,
            this.pSiglaEntidade,
            this.pCodigoConsultor,
            this.pDataInicioPeriodoTarefa,
            this.pDataFimPeriodoTarefa,
            this.pStatusBriefing,
            this.pCodigoUnidadeNegocio,
            this.pStatusProjeto});
        this.ScriptsSource = resources.GetString("$this.ScriptsSource");
        this.SnapGridSize = 9.84252F;
        this.SnappingMode = ((DevExpress.XtraReports.UI.SnappingMode)((DevExpress.XtraReports.UI.SnappingMode.SnapLines | DevExpress.XtraReports.UI.SnappingMode.SnapToGrid)));
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleInfo,
            this.styleTitulo});
        this.Version = "17.2";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relStatusReportDIRCOM_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsStatusReportDIRCOM1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relStatusReportDIRCOM_DataSourceDemanded(object sender, EventArgs e)
    {
        InitData();
    }

    private void InitData()
    {
        cDados = CdadosUtil.GetCdados(null);

        string connectionString = cDados.classeDados.getStringConexao();

        string siglaEntidade = pSiglaEntidade.Value.ToString();
        string codigoConsultor = pCodigoConsultor.Value.ToString();
        string dataInicioPeriodoTarefa = pDataInicioPeriodoTarefa.Value.ToString();
        string dataFimPeriodoTarefa = pDataFimPeriodoTarefa.Value.ToString();
        string statusProjeto = pStatusProjeto.Value.ToString();

        string codigoUnidadeNegocio = pCodigoUnidadeNegocio.Value.ToString();

        #region Comando SQL
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_SiglaEntidade varchar(50)
        DECLARE @in_CodigoUnidadeNegocio int
        DECLARE @in_CodigoConsultor int
        DECLARE @in_DataInicioPeriodoTarefa datetime
        DECLARE @in_DataFimPeriodoTarefa datetime
        DECLARE @in_NomeProjeto varchar(255)
        DECLARE @in_StatusProjeto varchar(50)

        SET @in_SiglaEntidade = {0}
        SET @in_NomeProjeto = {1}
        SET @in_DataInicioPeriodoTarefa = {2}
        SET @in_DataFimPeriodoTarefa = {3}
        SET @in_StatusProjeto = {4}
        SET @in_CodigoUnidadeNegocio = {5}

        EXECUTE @RC = [dbo].[p_cni_ImprimeRelatorioStatusDIRCOM] 
           @in_SiglaEntidade
          ,@in_CodigoUnidadeNegocio
          ,@in_NomeProjeto
          ,@in_DataInicioPeriodoTarefa
          ,@in_DataFimPeriodoTarefa
          ,@in_StatusProjeto

", siglaEntidade,
        codigoConsultor,
        dataInicioPeriodoTarefa,
        dataFimPeriodoTarefa,
        statusProjeto,
        codigoUnidadeNegocio);
        #endregion

        SqlDataAdapter da = new SqlDataAdapter(comandoSQL, connectionString);
        da.TableMappings.Add("Table", "dtStatusReportDircom");
        //especialmente neste caso Fill é utilizado 
        da.Fill(dsStatusReportDIRCOM1);

    }

    private void celulaDataTerminoTarefa_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DateTime valorData = DateTime.MinValue;
        string valorCelula = (((XRTableCell)sender).Report.GetCurrentColumnValue("DataTerminoTarefa") != null) ?
             ((XRTableCell)sender).Report.GetCurrentColumnValue("DataTerminoTarefa").ToString() : "";

        DateTime.TryParse(valorCelula, out valorData);
        if (valorData != DateTime.MinValue)
        {
            ((XRTableCell)sender).Text = string.Format(@"{0:dd/MMM}", valorData);
        }
        else if (valorCelula != "")
        {
            ((XRTableCell)sender).Text = string.Format(@"{0}", valorCelula);
        }
        else
        {
            ((XRTableCell)sender).Text = "-";
        }
    }

    private void celulaDataBriefing_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DateTime valorData = DateTime.MinValue;
        string valorCelula = (((XRTableCell)sender).Report.GetCurrentColumnValue("DataBriefing") != null) ?
             ((XRTableCell)sender).Report.GetCurrentColumnValue("DataBriefing").ToString() : "";

        DateTime.TryParse(valorCelula, out valorData);
        if (valorData != DateTime.MinValue)
        {
            ((XRTableCell)sender).Text = string.Format(@"{0:dd/MMM}", valorData);
        }
        else
        {
            ((XRTableCell)sender).Text = "-";
        }
    }

    private void celulaDataInicioTarefa_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DateTime valorData = DateTime.MinValue;
        string valorCelula = (((XRTableCell)sender).Report.GetCurrentColumnValue("DataInicioTarefa") != null) ?
             ((XRTableCell)sender).Report.GetCurrentColumnValue("DataInicioTarefa").ToString() : "";

        DateTime.TryParse(valorCelula, out valorData);
        if (valorData != DateTime.MinValue)
        {
            ((XRTableCell)sender).Text = string.Format(@"{0:dd/MMM}", valorData);
        }
        else
        {
            ((XRTableCell)sender).Text = "-";
        }
    }

    private void celulaNomeTarefa_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string valorCelula = (((XRTableCell)sender).Report.GetCurrentColumnValue("NomeTarefa") != null) ?
             ((XRTableCell)sender).Report.GetCurrentColumnValue("NomeTarefa").ToString() : "";

        if (valorCelula != "")
        {
            ((XRTableCell)sender).Text = valorCelula;
        }
        else
        {
            ((XRTableCell)sender).Text = "-";
        }
    }

    private void celulaRecursosAlocadosTarefa_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string valorCelula = (((XRTableCell)sender).Report.GetCurrentColumnValue("RecursosAlocadosTarefa") != null) ?
     ((XRTableCell)sender).Report.GetCurrentColumnValue("RecursosAlocadosTarefa").ToString() : "";

        if (valorCelula != "")
        {
            ((XRTableCell)sender).Text = valorCelula;
        }
        else
        {
            ((XRTableCell)sender).Text = "-";
        }
    }
}
