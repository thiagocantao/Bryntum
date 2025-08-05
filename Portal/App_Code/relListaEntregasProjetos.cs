using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for relListaEntregasProjetos
/// </summary>
public class relListaEntregasProjetos : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRLabel lblDataEmissao;
    private XRLabel lblTituloRelatorio;
    private XRPictureBox logoEntidade;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private dsListaEntregasProjeto dsListaEntregasProjeto1;
    private GroupHeaderBand GroupHeader1;
    private XRTable tbCabecalho;
    private XRTableRow xrTableRow2;
    private XRTableCell celEntrega;
    private XRTableCell celDataPactuada;
    private XRTableCell celDataReal;
    private XRTableCell celSituacao;
    private dados cDados = CdadosUtil.GetCdados(null);
    private string dataEmissaoGlobal = "";
    private string nomeProjeto = "";
    private XRLabel lblProjeto;
    private CalculatedField cf_Entrega;
    private CalculatedField cf_DataPactuada;
    private CalculatedField cf_DataReal;
    private CalculatedField cf_Situacao;
    private XRLabel lblSemDados;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relListaEntregasProjetos(int codigoProjeto, string dataGeracaoRel, string where)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        dataEmissaoGlobal = dataGeracaoRel;
        nomeProjeto = getNomeProjeto(codigoProjeto);
        InitData(codigoProjeto, where);
    }

    private void InitData(int codigoProjeto, string where)
    {

        cDados = CdadosUtil.GetCdados(null);
        string comandoSQL = string.Format(
        @"SELECT * FROM (SELECT p.CodigoProjeto, 
                 tc.NomeTarefa, 
                 tc.TerminoLB, 
                 tc.TerminoReal,
				 CASE WHEN tc.TerminoLB is null THEN ''
					  WHEN tc.TerminoReal is not null THEN 'Concluída'
					  WHEN tc.TerminoReal is null  and tc.TerminoLB < GETDATE() THEN 'Atrasada'
					  WHEN tc.TerminoReal is null  and tc.TerminoLB >= GETDATE() THEN 'Planejada'
				 END AS Situacao     
			FROM {0}.{1}.Projeto p INNER JOIN
				 {0}.{1}.CronogramaProjeto cp ON ( cp.CodigoProjeto = p.CodigoProjeto ) INNER JOIN
				 {0}.{1}.TarefaCronogramaProjeto tc ON ( tc.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto ) INNER JOIN
				 {0}.{1}.TipoTarefaCronograma ttc ON ( ttc.CodigoTipoTarefaCronograma = tc.CodigoTipoTarefaCronograma )
			WHERE tc.DataExclusao IS NULL
			AND ttc.codigoEntidade = p.CodigoEntidade
			AND ttc.IniciaisTipoControladoSistema = 'ENTREGA'
			AND p.CodigoProjeto = {2}) AS temp
          WHERE 1 = 1 
            {3}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, where);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataTableOk(ds.Tables[0]) == false)
        {
            lblSemDados.Text = "Não existem entregas para este projeto.";
            tbCabecalho.Visible = false;
        }
        else
        {
            dsListaEntregasProjeto1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "tbListaEntregasProjetos");
            lblSemDados.Text = "";
            tbCabecalho.Visible = true;
        }
    }

    private string getNomeProjeto(int codigoProjeto)
    {
        string nomeProjeto = "";
        cDados = CdadosUtil.GetCdados(null);
        string comandoSQL = string.Format(
        @"SELECT NomeProjeto from projeto where codigoprojeto = {0}", codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeProjeto = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
        }
        return nomeProjeto;
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
        //string resourceFileName = "relListaEntregasProjetos.resx";
        System.Resources.ResourceManager resources = global::Resources.relListaEntregasProjetos.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.lblProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDataEmissao = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTituloRelatorio = new DevExpress.XtraReports.UI.XRLabel();
        this.logoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.dsListaEntregasProjeto1 = new dsListaEntregasProjeto();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.tbCabecalho = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celEntrega = new DevExpress.XtraReports.UI.XRTableCell();
        this.celDataPactuada = new DevExpress.XtraReports.UI.XRTableCell();
        this.celDataReal = new DevExpress.XtraReports.UI.XRTableCell();
        this.celSituacao = new DevExpress.XtraReports.UI.XRTableCell();
        this.cf_Entrega = new DevExpress.XtraReports.UI.CalculatedField();
        this.cf_DataPactuada = new DevExpress.XtraReports.UI.CalculatedField();
        this.cf_DataReal = new DevExpress.XtraReports.UI.CalculatedField();
        this.cf_Situacao = new DevExpress.XtraReports.UI.CalculatedField();
        this.lblSemDados = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsListaEntregasProjeto1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbCabecalho)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblSemDados,
            this.xrTable1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 110.1492F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1881F, 51.72917F);
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.CanShrink = true;
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbListaEntregasProjetos.NomeTarefa")});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 0.99222842600452954D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.CanShrink = true;
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbListaEntregasProjetos.TerminoLB", "{0:dd/MM/yyyy}")});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell3.Weight = 0.38940120744057494D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.CanShrink = true;
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbListaEntregasProjetos.TerminoReal", "{0:dd/MM/yyyy}")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.3894012074405751D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.CanShrink = true;
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbListaEntregasProjetos.Situacao")});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell5.Weight = 0.3075288972485265D;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblProjeto,
            this.lblDataEmissao,
            this.lblTituloRelatorio,
            this.logoEntidade});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 251.3542F;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
        // 
        // lblProjeto
        // 
        this.lblProjeto.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
        this.lblProjeto.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblProjeto.Dpi = 254F;
        this.lblProjeto.Font = new System.Drawing.Font("Verdana", 10F);
        this.lblProjeto.LocationFloat = new DevExpress.Utils.PointFloat(480.9559F, 132.8747F);
        this.lblProjeto.Name = "lblProjeto";
        this.lblProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblProjeto.SizeF = new System.Drawing.SizeF(1399.441F, 73.37836F);
        this.lblProjeto.StylePriority.UseBorderDashStyle = false;
        this.lblProjeto.StylePriority.UseBorders = false;
        this.lblProjeto.StylePriority.UseFont = false;
        this.lblProjeto.StylePriority.UseTextAlignment = false;
        this.lblProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblDataEmissao
        // 
        this.lblDataEmissao.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblDataEmissao.Dpi = 254F;
        this.lblDataEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDataEmissao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 206.2531F);
        this.lblDataEmissao.Name = "lblDataEmissao";
        this.lblDataEmissao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDataEmissao.SizeF = new System.Drawing.SizeF(1880.397F, 29.31584F);
        this.lblDataEmissao.StylePriority.UseBorders = false;
        this.lblDataEmissao.StylePriority.UseFont = false;
        this.lblDataEmissao.StylePriority.UseTextAlignment = false;
        this.lblDataEmissao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // lblTituloRelatorio
        // 
        this.lblTituloRelatorio.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
        this.lblTituloRelatorio.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblTituloRelatorio.Dpi = 254F;
        this.lblTituloRelatorio.Font = new System.Drawing.Font("Verdana", 15.75F);
        this.lblTituloRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(481.559F, 58.7909F);
        this.lblTituloRelatorio.Name = "lblTituloRelatorio";
        this.lblTituloRelatorio.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTituloRelatorio.SizeF = new System.Drawing.SizeF(1399.441F, 73.37836F);
        this.lblTituloRelatorio.StylePriority.UseBorderDashStyle = false;
        this.lblTituloRelatorio.StylePriority.UseBorders = false;
        this.lblTituloRelatorio.StylePriority.UseFont = false;
        this.lblTituloRelatorio.StylePriority.UseTextAlignment = false;
        this.lblTituloRelatorio.Text = "Lista de Entregas do Projeto";
        this.lblTituloRelatorio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // logoEntidade
        // 
        this.logoEntidade.BorderColor = System.Drawing.Color.Silver;
        this.logoEntidade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.logoEntidade.Dpi = 254F;
        this.logoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 18.43107F);
        this.logoEntidade.Name = "logoEntidade";
        this.logoEntidade.SizeF = new System.Drawing.SizeF(461.1821F, 187.5726F);
        this.logoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.logoEntidade.StylePriority.UseBorderColor = false;
        this.logoEntidade.StylePriority.UseBorders = false;
        // 
        // dsListaEntregasProjeto1
        // 
        this.dsListaEntregasProjeto1.DataSetName = "dsListaEntregasProjeto";
        this.dsListaEntregasProjeto1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tbCabecalho});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 50.27083F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // tbCabecalho
        // 
        this.tbCabecalho.BackColor = System.Drawing.Color.AntiqueWhite;
        this.tbCabecalho.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.tbCabecalho.Dpi = 254F;
        this.tbCabecalho.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.tbCabecalho.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.tbCabecalho.Name = "tbCabecalho";
        this.tbCabecalho.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.tbCabecalho.SizeF = new System.Drawing.SizeF(1881F, 50.27083F);
        this.tbCabecalho.StylePriority.UseBackColor = false;
        this.tbCabecalho.StylePriority.UseBorders = false;
        this.tbCabecalho.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celEntrega,
            this.celDataPactuada,
            this.celDataReal,
            this.celSituacao});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.44958846599422736D;
        // 
        // celEntrega
        // 
        this.celEntrega.CanShrink = true;
        this.celEntrega.Dpi = 254F;
        this.celEntrega.Name = "celEntrega";
        this.celEntrega.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 5, 0, 254F);
        this.celEntrega.StylePriority.UsePadding = false;
        this.celEntrega.StylePriority.UseTextAlignment = false;
        this.celEntrega.Text = "Entrega";
        this.celEntrega.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celEntrega.Weight = 0.99222856089577727D;
        // 
        // celDataPactuada
        // 
        this.celDataPactuada.CanShrink = true;
        this.celDataPactuada.Dpi = 254F;
        this.celDataPactuada.Name = "celDataPactuada";
        this.celDataPactuada.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 5, 0, 254F);
        this.celDataPactuada.StylePriority.UsePadding = false;
        this.celDataPactuada.StylePriority.UseTextAlignment = false;
        this.celDataPactuada.Text = "Data Pactuada";
        this.celDataPactuada.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celDataPactuada.Weight = 0.38940120744057494D;
        // 
        // celDataReal
        // 
        this.celDataReal.CanShrink = true;
        this.celDataReal.Dpi = 254F;
        this.celDataReal.Name = "celDataReal";
        this.celDataReal.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 5, 0, 254F);
        this.celDataReal.StylePriority.UsePadding = false;
        this.celDataReal.StylePriority.UseTextAlignment = false;
        this.celDataReal.Text = "Data Real";
        this.celDataReal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celDataReal.Weight = 0.3894012074405751D;
        // 
        // celSituacao
        // 
        this.celSituacao.CanShrink = true;
        this.celSituacao.Dpi = 254F;
        this.celSituacao.Name = "celSituacao";
        this.celSituacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 5, 0, 254F);
        this.celSituacao.StylePriority.UsePadding = false;
        this.celSituacao.StylePriority.UseTextAlignment = false;
        this.celSituacao.Text = "Situação";
        this.celSituacao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celSituacao.Weight = 0.30752876235727883D;
        // 
        // cf_Entrega
        // 
        this.cf_Entrega.DataMember = "tbListaEntregasProjetos";
        this.cf_Entrega.Expression = resources.GetString("cf_Entrega.Expression");
        this.cf_Entrega.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cf_Entrega.Name = "cf_Entrega";
        // 
        // cf_DataPactuada
        // 
        this.cf_DataPactuada.DataMember = "tbListaEntregasProjetos";
        this.cf_DataPactuada.Expression = resources.GetString("cf_DataPactuada.Expression");
        this.cf_DataPactuada.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cf_DataPactuada.Name = "cf_DataPactuada";
        // 
        // cf_DataReal
        // 
        this.cf_DataReal.DataMember = "tbListaEntregasProjetos";
        this.cf_DataReal.DisplayName = "cf_DataReal";
        this.cf_DataReal.Expression = resources.GetString("cf_DataReal.Expression");
        this.cf_DataReal.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cf_DataReal.Name = "cf_DataReal";
        // 
        // cf_Situacao
        // 
        this.cf_Situacao.DataMember = "tbListaEntregasProjetos";
        this.cf_Situacao.Expression = resources.GetString("cf_Situacao.Expression");
        this.cf_Situacao.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cf_Situacao.Name = "cf_Situacao";
        // 
        // lblSemDados
        // 
        this.lblSemDados.BorderColor = System.Drawing.Color.Transparent;
        this.lblSemDados.CanShrink = true;
        this.lblSemDados.Dpi = 254F;
        this.lblSemDados.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblSemDados.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.72916F);
        this.lblSemDados.Name = "lblSemDados";
        this.lblSemDados.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.lblSemDados.SizeF = new System.Drawing.SizeF(1881F, 58.42F);
        this.lblSemDados.StylePriority.UseBorderColor = false;
        this.lblSemDados.StylePriority.UseFont = false;
        this.lblSemDados.StylePriority.UsePadding = false;
        this.lblSemDados.StylePriority.UseTextAlignment = false;
        this.lblSemDados.Text = "lblSemDados";
        this.lblSemDados.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // relListaEntregasProjetos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cf_Entrega,
            this.cf_DataPactuada,
            this.cf_DataReal,
            this.cf_Situacao});
        this.DataMember = "tbListaEntregasProjetos";
        this.DataSource = this.dsListaEntregasProjeto1;
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(110, 110, 100, 100);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "12.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsListaEntregasProjeto1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbCabecalho)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblDataEmissao.Text = "Data de emissão: " + dataEmissaoGlobal;
        lblProjeto.Text = nomeProjeto;
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codEntidade, "");
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            logoEntidade.Image = logo;
        }


    }

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }
}
