using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_EnvioPautaReuniao
/// </summary>
public class rel_EnvioPautaReuniao : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLine xrLine3;
    private XRLine xrLine4;
    private XRLabel xrLabel3;
    private GroupHeaderBand GroupHeader1;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel11;
    private XRLabel xrLabel12;
    private XRLabel xrLabel9;
    private XRLabel xrLabel10;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel13;
    private XRLabel xrLabel15;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel14;
    public DevExpress.XtraReports.Parameters.Parameter pDataTerminoPrevistoReuniao;
    public DevExpress.XtraReports.Parameters.Parameter pDataInicioPrevistoReuniao;
    public DevExpress.XtraReports.Parameters.Parameter pAssuntoReuniao;
    public DevExpress.XtraReports.Parameters.Parameter pNomeObjetoReuniao;
    public DevExpress.XtraReports.Parameters.Parameter pDescricaoTipoObjetoReuniao;
    public DevExpress.XtraReports.Parameters.Parameter pTextoApresentacao;
    public DevExpress.XtraReports.Parameters.Parameter pLocalReuniao;
    public DevExpress.XtraReports.Parameters.Parameter pResponsavelReuniao;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoEvento;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRCrossBandBox xrCrossBandBox1;
    private XRLabel xrLabel19;
    public DevExpress.XtraReports.Parameters.Parameter pResumoPauta;
    private XRPanel pnlQuando;
    private XRPanel pnlInicioTermino;
    private XRControlStyle stlNegrito;
    public DevExpress.XtraReports.Parameters.Parameter pEmailResponsavelReuniao;

    dados cDados;

    public rel_EnvioPautaReuniao()
    {
        InitializeComponent();
        cDados = CdadosUtil.GetCdados(null);
    }

    private void InitData()
    {
        string comandoSql = string.Format(@"
DECLARE @CodigoEvento INT 
    SET @CodigoEvento = {0}

 SELECT CASE 
            WHEN oae.CodigoTipoObjetoAssociado = -1 
            THEN 'Dados Financeiros'
            WHEN oae.CodigoTipoObjetoAssociado =  0 
            THEN 'Pendências da Reunião Anterior'
            WHEN dbo.f_GetIniciaisTipoAssociacao(oae.CodigoTipoObjetoAssociado) = 'RQ' 
            THEN ISNULL( (SELECT CASE WHEN rq.IndicaRiscoQuestao = 'R' THEN 'Risco'
                                        ELSE ( SELECT p.Valor
                                                 FROM ParametroConfiguracaoSistema   p
                                                WHERE p.Parametro = 'labelQuestao'
                                                  AND p.CodigoEntidade = rq.CodigoEntidade
                                            )
                                    END
                            FROM RiscoQuestao rq
                            WHERE rq.CodigoRiscoQuestao = oae.CodigoObjetoAssociado), '')
            WHEN dbo.f_GetIniciaisTipoAssociacao(oae.CodigoTipoObjetoAssociado) = 'TC' 
            THEN 'Tarefa do Cronograma'
            WHEN dbo.f_GetIniciaisTipoAssociacao(oae.CodigoTipoObjetoAssociado) = 'IN' 
            THEN 'Indicador'
             WHEN dbo.f_GetIniciaisTipoAssociacao(oae.CodigoTipoObjetoAssociado) = 'PR' 
                THEN 'Projeto'
        END AS DescricaoTipoObjetoAssociado,
        
        CASE 
             WHEN CodigoTipoObjetoAssociado = -1 
                THEN ''
             WHEN CodigoTipoObjetoAssociado =  0 
                THEN ''
             WHEN dbo.f_GetIniciaisTipoAssociacao(CodigoTipoObjetoAssociado) = 'RQ' 
                THEN ISNULL((SELECT DescricaoRiscoQuestao 
                             FROM RiscoQuestao 
                             WHERE CodigoRiscoQuestao = CodigoObjetoAssociado), '')
             WHEN dbo.f_GetIniciaisTipoAssociacao(CodigoTipoObjetoAssociado) = 'TC' 
                THEN ISNULL((SELECT NomeTarefa 
                             FROM TarefaCronogramaProjeto  tcp INNER JOIN
                                  CronogramaProjeto tc ON ( tc.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto )
                             WHERE tcp.CodigoTarefa = oae.CodigoObjetoAssociado 
                             AND tc.CodigoProjeto = oae.CodigoObjetoReuniao ), '')
             WHEN dbo.f_GetIniciaisTipoAssociacao(CodigoTipoObjetoAssociado) = 'IN' 
                THEN  ISNULL((SELECT ino.NomeIndicador
                             FROM IndicadorOperacional ino
                             WHERE ino.CodigoIndicador = oae.CodigoObjetoAssociado ), '')
             WHEN dbo.f_GetIniciaisTipoAssociacao(CodigoTipoObjetoAssociado) = 'PR' 
                THEN  ISNULL((SELECT p.NomeProjeto
                             FROM Projeto p
                             WHERE p.CodigoProjeto = oae.CodigoObjetoAssociado ), '')
        END AS DescricaoObjetoAssociado
        
   FROM [ObjetoAssociadoEvento] AS oae
  WHERE oae.CodigoEvento = @CodigoEvento
  ORDER BY
        oae.SequenciaApresentacao", pCodigoEvento.Value);

        DataSet ds = cDados.getDataSet(comandoSql);
        ds.DataSetName = "ItensPauta";
        ds.Tables[0].TableName = "Item";
        DataSource = ds;
        DataMember = "Item";
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
        //string resourceFileName = "rel_EnvioPautaReuniao.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_EnvioPautaReuniao.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.pnlInicioTermino = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.pDataTerminoPrevistoReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.pDataInicioPrevistoReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        this.pnlQuando = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.pLocalReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.pResponsavelReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.pAssuntoReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.pNomeObjetoReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.pDescricaoTipoObjetoReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.pTextoApresentacao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.pResumoPauta = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.pCodigoEvento = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrCrossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
        this.stlNegrito = new DevExpress.XtraReports.UI.XRControlStyle();
        this.pEmailResponsavelReuniao = new DevExpress.XtraReports.Parameters.Parameter();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 50F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(2000F, 50F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Item.DescricaoTipoObjetoAssociado")});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 0.89999993896484365D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Item.DescricaoObjetoAssociado")});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 2.1000000610351561D;
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
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pnlInicioTermino,
            this.pnlQuando,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 550F;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
        // 
        // pnlInicioTermino
        // 
        this.pnlInicioTermino.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel16,
            this.xrLabel13,
            this.xrLabel15});
        this.pnlInicioTermino.Dpi = 254F;
        this.pnlInicioTermino.LocationFloat = new DevExpress.Utils.PointFloat(34.99996F, 300F);
        this.pnlInicioTermino.Name = "pnlInicioTermino";
        this.pnlInicioTermino.SizeF = new System.Drawing.SizeF(2030F, 100F);
        // 
        // xrLabel14
        // 
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(15.00002F, 49.99994F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(400F, 50.00003F);
        this.xrLabel14.StyleName = "stlNegrito";
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.Text = "Término Previsto:";
        // 
        // xrLabel16
        // 
        this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pDataTerminoPrevistoReuniao, "Text", "{0:g}")});
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(415.0001F, 50.0001F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(1600F, 50F);
        this.xrLabel16.Text = "xrLabel4";
        // 
        // pDataTerminoPrevistoReuniao
        // 
        this.pDataTerminoPrevistoReuniao.Name = "pDataTerminoPrevistoReuniao";
        this.pDataTerminoPrevistoReuniao.Type = typeof(System.DateTime);
        this.pDataTerminoPrevistoReuniao.ValueInfo = "08/02/2013 11:13:33";
        this.pDataTerminoPrevistoReuniao.Visible = false;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(15.00002F, 3.051758E-05F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(400F, 50F);
        this.xrLabel13.StyleName = "stlNegrito";
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.Text = "Início Previsto:";
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pDataInicioPrevistoReuniao, "Text", "{0:g}")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(415.0002F, 0F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(1600F, 50F);
        this.xrLabel15.Text = "xrLabel4";
        // 
        // pDataInicioPrevistoReuniao
        // 
        this.pDataInicioPrevistoReuniao.Name = "pDataInicioPrevistoReuniao";
        this.pDataInicioPrevistoReuniao.Type = typeof(System.DateTime);
        this.pDataInicioPrevistoReuniao.ValueInfo = "08/02/2013 11:12:44";
        this.pDataInicioPrevistoReuniao.Visible = false;
        // 
        // pnlQuando
        // 
        this.pnlQuando.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17,
            this.xrLabel18});
        this.pnlQuando.Dpi = 254F;
        this.pnlQuando.LocationFloat = new DevExpress.Utils.PointFloat(34.99996F, 400.0001F);
        this.pnlQuando.Name = "pnlQuando";
        this.pnlQuando.SizeF = new System.Drawing.SizeF(2030F, 50F);
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(15.00002F, 3.051758E-05F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(400.0001F, 50F);
        this.xrLabel17.StyleName = "stlNegrito";
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.Text = "Quando:";
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(415.0001F, 0F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1600F, 50F);
        this.xrLabel18.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblQuando_BeforePrint);
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pLocalReuniao, "Text", "")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(450F, 450F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1600F, 50F);
        this.xrLabel11.Text = "xrLabel4";
        // 
        // pLocalReuniao
        // 
        this.pLocalReuniao.Name = "pLocalReuniao";
        this.pLocalReuniao.Visible = false;
        // 
        // xrLabel12
        // 
        this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pResponsavelReuniao, "Text", "")});
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(450F, 500F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(1600F, 50F);
        this.xrLabel12.Text = "xrLabel4";
        // 
        // pResponsavelReuniao
        // 
        this.pResponsavelReuniao.Name = "pResponsavelReuniao";
        this.pResponsavelReuniao.Visible = false;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(50.00002F, 450.0002F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(400F, 49.99994F);
        this.xrLabel9.StyleName = "stlNegrito";
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = "Local:";
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(50.00002F, 500F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(400F, 50F);
        this.xrLabel10.StyleName = "stlNegrito";
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = "Responsável:";
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pAssuntoReuniao, "Text", "")});
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(450.0003F, 250F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(1600F, 50F);
        this.xrLabel8.Text = "xrLabel4";
        // 
        // pAssuntoReuniao
        // 
        this.pAssuntoReuniao.Name = "pAssuntoReuniao";
        this.pAssuntoReuniao.Visible = false;
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pNomeObjetoReuniao, "Text", "")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(450F, 200F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(1600F, 50F);
        this.xrLabel7.Text = "xrLabel4";
        // 
        // pNomeObjetoReuniao
        // 
        this.pNomeObjetoReuniao.Name = "pNomeObjetoReuniao";
        this.pNomeObjetoReuniao.Visible = false;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(50.00002F, 250F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(400.0003F, 50F);
        this.xrLabel6.StyleName = "stlNegrito";
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "Assunto:";
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pDescricaoTipoObjetoReuniao, "Text", "")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(50.00002F, 200F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(400.0003F, 50F);
        this.xrLabel5.StyleName = "stlNegrito";
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "Unidade de Negócio:";
        // 
        // pDescricaoTipoObjetoReuniao
        // 
        this.pDescricaoTipoObjetoReuniao.Name = "pDescricaoTipoObjetoReuniao";
        this.pDescricaoTipoObjetoReuniao.Visible = false;
        // 
        // xrLabel4
        // 
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pTextoApresentacao, "Text", "")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(50F, 125F);
        this.xrLabel4.Multiline = true;
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(2000F, 50.00001F);
        this.xrLabel4.Text = "xrLabel4";
        // 
        // pTextoApresentacao
        // 
        this.pTextoApresentacao.Name = "pTextoApresentacao";
        this.pTextoApresentacao.Visible = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 50F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(250F, 50F);
        this.xrLabel1.StyleName = "stlNegrito";
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Prezado(a),";
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 250F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
        this.xrLabel2.Multiline = true;
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(2000F, 200F);
        this.xrLabel2.Text = "Att.,\r\n\r\nPortal da Estratégia - DESENV.\r\n\r\nPS: Por favor, não responda esse e-mai" +
"l.";
        // 
        // xrLine3
        // 
        this.xrLine3.Dpi = 254F;
        this.xrLine3.ForeColor = System.Drawing.Color.DarkGray;
        this.xrLine3.LineWidth = 3;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(50.0001F, 25.00001F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(2000F, 5F);
        this.xrLine3.StylePriority.UseBackColor = false;
        this.xrLine3.StylePriority.UseBorderColor = false;
        this.xrLine3.StylePriority.UseForeColor = false;
        // 
        // xrLine4
        // 
        this.xrLine4.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
        this.xrLine4.Dpi = 254F;
        this.xrLine4.ForeColor = System.Drawing.Color.DarkGray;
        this.xrLine4.LineWidth = 3;
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(49.99994F, 24.99993F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(2000F, 5F);
        this.xrLine4.StylePriority.UseBackColor = false;
        this.xrLine4.StylePriority.UseBorderColor = false;
        this.xrLine4.StylePriority.UseForeColor = false;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(50.0001F, 49.99994F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(250F, 50F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Pauta:";
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19,
            this.xrTable1,
            this.xrLine3,
            this.xrLabel3});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 300F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel19
        // 
        this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pResumoPauta, "Text", "")});
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(50F, 125F);
        this.xrLabel19.Multiline = true;
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(2000F, 50F);
        this.xrLabel19.Text = "xrLabel19";
        // 
        // pResumoPauta
        // 
        this.pResumoPauta.Name = "pResumoPauta";
        this.pResumoPauta.Visible = false;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.BorderWidth = 1;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 250F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(2000F, 50F);
        this.xrTable1.StyleName = "stlNegrito";
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.CanGrow = false;
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Item da Pauta";
        this.xrTableCell2.Weight = 0.89999993896484365D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.CanGrow = false;
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Descrição";
        this.xrTableCell3.Weight = 2.1000000610351561D;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine4});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 50F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // pCodigoEvento
        // 
        this.pCodigoEvento.Name = "pCodigoEvento";
        this.pCodigoEvento.Type = typeof(short);
        this.pCodigoEvento.ValueInfo = "0";
        this.pCodigoEvento.Visible = false;
        // 
        // xrCrossBandBox1
        // 
        this.xrCrossBandBox1.BorderColor = System.Drawing.Color.DarkGray;
        this.xrCrossBandBox1.Dpi = 254F;
        this.xrCrossBandBox1.EndBand = this.ReportFooter;
        this.xrCrossBandBox1.EndPointFloat = new DevExpress.Utils.PointFloat(25F, 225F);
        this.xrCrossBandBox1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 25F);
        this.xrCrossBandBox1.Name = "xrCrossBandBox1";
        this.xrCrossBandBox1.StartBand = this.ReportHeader;
        this.xrCrossBandBox1.StartPointFloat = new DevExpress.Utils.PointFloat(25F, 25F);
        this.xrCrossBandBox1.WidthF = 2050F;
        // 
        // stlNegrito
        // 
        this.stlNegrito.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.stlNegrito.Name = "stlNegrito";
        // 
        // pEmailResponsavelReuniao
        // 
        this.pEmailResponsavelReuniao.Name = "pEmailResponsavelReuniao";
        this.pEmailResponsavelReuniao.Visible = false;
        // 
        // rel_EnvioPautaReuniao
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.GroupFooter1});
        this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandBox1});
        this.DataSourceSchema = resources.GetString("$this.DataSourceSchema");
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 9F);
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pDescricaoTipoObjetoReuniao,
            this.pNomeObjetoReuniao,
            this.pAssuntoReuniao,
            this.pDataInicioPrevistoReuniao,
            this.pDataTerminoPrevistoReuniao,
            this.pTextoApresentacao,
            this.pCodigoEvento,
            this.pLocalReuniao,
            this.pResponsavelReuniao,
            this.pResumoPauta,
            this.pEmailResponsavelReuniao});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.stlNegrito});
        this.Version = "12.2";
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rel_EnvioPautaReuniao_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void lblQuando_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel label = (XRLabel)sender;
        label.Text = string.Format("{0:D} {0:t}-{1:t}",
            pDataInicioPrevistoReuniao.Value, pDataTerminoPrevistoReuniao.Value);
    }

    private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DateTime dataInicio = (DateTime)pDataInicioPrevistoReuniao.Value;
        DateTime dataTermino = (DateTime)pDataTerminoPrevistoReuniao.Value;
        if (dataInicio.Date == dataTermino.Date)
        {
            if (ReportHeader.Controls.Contains(pnlInicioTermino))
                ReportHeader.Controls.Remove(pnlInicioTermino);
            if (!ReportHeader.Controls.Contains(pnlQuando))
                ReportHeader.Controls.Add(pnlQuando);
            pnlQuando.LocationF = new PointF(35, 350);
        }
        else
        {
            if (ReportHeader.Controls.Contains(pnlQuando))
                ReportHeader.Controls.Remove(pnlQuando);
            if (!ReportHeader.Controls.Contains(pnlInicioTermino))
                ReportHeader.Controls.Add(pnlInicioTermino);
            pnlInicioTermino.LocationF = new PointF(35, 325);
        }
    }

    private void rel_EnvioPautaReuniao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        InitData();
    }
}
