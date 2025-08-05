using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for relSRGerencial_FR
/// </summary>
public class relSRGerencial_FR : DevExpress.XtraReports.UI.XtraReport
{
    #region Fields
    private DevExpress.XtraReports.UI.DetailBand DetailGeral;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    #endregion
    private ReportHeaderBand ReportHeader;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel lblPeriodo;
    private XRLabel xrLabel4;
    private XRLabel lblNomeProjeto;
    private PageFooterBand PageFooter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel lblDataEntrega;
    private XRPageBreak xrPageBreak1;
    private XRRichText htmlIntroducao;
    private XRLabel lblDataReuniao;
    private XRPageBreak xrPageBreak3;
    private XRLabel xrLabel11;
    private XRRichText htmlMetas;
    private XRPageBreak xrPageBreak4;
    private XRLabel xrLabel13;
    private XRRichText htmlFisico;
    private XRPageBreak xrPageBreak2;
    private XRLabel xrLabel10;
    private XRRichText htmlFinanceiro;
    private XRPageBreak xrPageBreak5;
    private XRLabel xrLabel12;
    private XRRichText htmlConsideracoesFinais;

    private int codigoStatusReport;

    dados cDados;


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Constructors
    public relSRGerencial_FR(int codigoEntidade, int codigoStatusReport)
    {
        InitializeComponent();
        cDados = CdadosUtil.GetCdados(codigoEntidade, null);
        this.codigoStatusReport = codigoStatusReport;
        InitDada();
    }

    private void InitDada()
    {
        #region Comando SQL

        string comandoSql = string.Format(@"
DECLARE @CodigoStatusReport INT
	SET @CodigoStatusReport = {0}
	
 SELECT sr.ComentarioGeral,
		sr.ComentarioMeta,
		sr.ComentarioFisico,
		sr.ComentarioFinanceiro,
		sr.ComentarioPlanoAcao,
		sr.DataGeracao,
		sr.DataInicioPeriodoRelatorio,
		sr.DataTerminoPeriodoRelatorio,
		sr.DataEntrega,
		sr.DataReuniao,
		p.NomeProjeto
   FROM StatusReport sr INNER JOIN
		Projeto p ON p.CodigoProjeto = sr.CodigoProjeto
  WHERE sr.CodigoStatusReport = @CodigoStatusReport
", codigoStatusReport);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];

        //htmlIntroducao.Text = dr["ComentarioGeral"].ToString();
        //htmlMetas.Text = dr["ComentarioMeta"].ToString();
        //htmlFisico.Text = dr["ComentarioFisico"].ToString();
        //htmlFinanceiro.Text = dr["ComentarioFinanceiro"].ToString();
        //htmlConsideracoesFinais.Text = dr["ComentarioPlanoAcao"].ToString();

        SetHtmlText(htmlIntroducao, (string)dr["ComentarioGeral"]);
        SetHtmlText(htmlMetas, (string)dr["ComentarioMeta"]);
        SetHtmlText(htmlFisico, (string)dr["ComentarioFisico"]);
        SetHtmlText(htmlFinanceiro, (string)dr["ComentarioFinanceiro"]);
        SetHtmlText(htmlConsideracoesFinais, (string)dr["ComentarioPlanoAcao"]);

        xrTableCell1.Text = string.Format("{0:dd/MM/yyyy}", dr["DataGeracao"]);
        lblPeriodo.Text = string.Format("{0:dd} de {0:MMMM} de {0:yyyy} a {1:dd} de {1:MMMM} de {1:yyyy}", dr["DataInicioPeriodoRelatorio"], dr["DataTerminoPeriodoRelatorio"]);
        lblNomeProjeto.Text = dr["NomeProjeto"].ToString();
        lblDataEntrega.Text = string.Format("Data de entrega do relatório: {0:dd/MM/yyyy}", dr["DataEntrega"]);
        lblDataReuniao.Text = string.Format("Data da Reunião: {0:dd/MM/yyyy}", dr["DataReuniao"]);
    }

    public static void SetHtmlText(XRRichText richText, string htmlText)
    {
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(htmlText);
        System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray);
        richText.LoadFile(ms, XRRichTextStreamType.HtmlText);
    }

    #endregion

    #region Methods
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

    //private void InitializeLayout()
    //{
    //    dados cDados;
    //    int codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    //    DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelQuestao", "labelQuestoes");     
    //}
    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //string resourceFileName = "relSRGerencial_FR.resx";
        System.Resources.ResourceManager resources = global::Resources.relSRGerencial_FR.ResourceManager;
        this.DetailGeral = new DevExpress.XtraReports.UI.DetailBand();
        this.htmlConsideracoesFinais = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageBreak5 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.htmlFinanceiro = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageBreak2 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.htmlFisico = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageBreak4 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.htmlMetas = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageBreak3 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.htmlIntroducao = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.lblDataReuniao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.lblDataEntrega = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblPeriodo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.htmlConsideracoesFinais)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlFinanceiro)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlFisico)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlMetas)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlIntroducao)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // DetailGeral
        // 
        this.DetailGeral.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.htmlConsideracoesFinais,
            this.xrLabel12,
            this.xrPageBreak5,
            this.htmlFinanceiro,
            this.xrLabel10,
            this.xrPageBreak2,
            this.htmlFisico,
            this.xrLabel13,
            this.xrPageBreak4,
            this.htmlMetas,
            this.xrLabel11,
            this.xrPageBreak3,
            this.htmlIntroducao,
            this.xrLabel7});
        this.DetailGeral.Dpi = 254F;
        this.DetailGeral.HeightF = 1900F;
        this.DetailGeral.Name = "DetailGeral";
        this.DetailGeral.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.DetailGeral.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // htmlConsideracoesFinais
        // 
        this.htmlConsideracoesFinais.CanShrink = true;
        this.htmlConsideracoesFinais.Dpi = 254F;
        this.htmlConsideracoesFinais.LocationFloat = new DevExpress.Utils.PointFloat(0F, 900F);
        this.htmlConsideracoesFinais.Name = "htmlConsideracoesFinais";
        this.htmlConsideracoesFinais.SerializableRtfString = resources.GetString("htmlConsideracoesFinais.SerializableRtfString");
        this.htmlConsideracoesFinais.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 800F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(1500F, 58.41992F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "5 - CONSIDERAÇÕES FINAIS";
        // 
        // xrPageBreak5
        // 
        this.xrPageBreak5.Dpi = 254F;
        this.xrPageBreak5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 790F);
        this.xrPageBreak5.Name = "xrPageBreak5";
        // 
        // htmlFinanceiro
        // 
        this.htmlFinanceiro.CanShrink = true;
        this.htmlFinanceiro.Dpi = 254F;
        this.htmlFinanceiro.LocationFloat = new DevExpress.Utils.PointFloat(0F, 700F);
        this.htmlFinanceiro.Name = "htmlFinanceiro";
        this.htmlFinanceiro.SerializableRtfString = resources.GetString("htmlFinanceiro.SerializableRtfString");
        this.htmlFinanceiro.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 600F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        this.xrLabel10.Text = "4.1 - ANÁLISE DAS DESPESAS E RECEITAS";
        // 
        // xrPageBreak2
        // 
        this.xrPageBreak2.Dpi = 254F;
        this.xrPageBreak2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 590F);
        this.xrPageBreak2.Name = "xrPageBreak2";
        // 
        // htmlFisico
        // 
        this.htmlFisico.CanShrink = true;
        this.htmlFisico.Dpi = 254F;
        this.htmlFisico.LocationFloat = new DevExpress.Utils.PointFloat(0F, 500F);
        this.htmlFisico.Name = "htmlFisico";
        this.htmlFisico.SerializableRtfString = resources.GetString("htmlFisico.SerializableRtfString");
        this.htmlFisico.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 400F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        this.xrLabel13.Text = "3.1 - Detalhamento da realização das ações:";
        // 
        // xrPageBreak4
        // 
        this.xrPageBreak4.Dpi = 254F;
        this.xrPageBreak4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 390F);
        this.xrPageBreak4.Name = "xrPageBreak4";
        // 
        // htmlMetas
        // 
        this.htmlMetas.CanShrink = true;
        this.htmlMetas.Dpi = 254F;
        this.htmlMetas.LocationFloat = new DevExpress.Utils.PointFloat(0F, 300F);
        this.htmlMetas.Name = "htmlMetas";
        this.htmlMetas.SerializableRtfString = resources.GetString("htmlMetas.SerializableRtfString");
        this.htmlMetas.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        // 
        // xrLabel11
        // 
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 200F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        this.xrLabel11.Text = "2.1 - Análise crítica e detalhamento do(s) resultado(s) alcançado(s):";
        // 
        // xrPageBreak3
        // 
        this.xrPageBreak3.Dpi = 254F;
        this.xrPageBreak3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 190F);
        this.xrPageBreak3.Name = "xrPageBreak3";
        // 
        // htmlIntroducao
        // 
        this.htmlIntroducao.CanShrink = true;
        this.htmlIntroducao.Dpi = 254F;
        this.htmlIntroducao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 105.2917F);
        this.htmlIntroducao.Name = "htmlIntroducao";
        this.htmlIntroducao.SerializableRtfString = resources.GetString("htmlIntroducao.SerializableRtfString");
        this.htmlIntroducao.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(1500F, 58.41992F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "1 - INTRODUÇÃO";
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.TopMargin.HeightF = 249F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.StylePriority.UseFont = false;
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
            this.lblDataReuniao,
            this.xrPageBreak1,
            this.lblDataEntrega,
            this.xrLabel6,
            this.lblNomeProjeto,
            this.xrLabel4,
            this.lblPeriodo,
            this.xrLabel2,
            this.xrLabel1,
            this.xrPictureBox1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 2402F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // lblDataReuniao
        // 
        this.lblDataReuniao.Dpi = 254F;
        this.lblDataReuniao.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDataReuniao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2300F);
        this.lblDataReuniao.Name = "lblDataReuniao";
        this.lblDataReuniao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDataReuniao.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        this.lblDataReuniao.StylePriority.UseFont = false;
        this.lblDataReuniao.StylePriority.UseTextAlignment = false;
        this.lblDataReuniao.Text = "Data da Reunião:";
        // 
        // xrPageBreak1
        // 
        this.xrPageBreak1.Dpi = 254F;
        this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2400F);
        this.xrPageBreak1.Name = "xrPageBreak1";
        // 
        // lblDataEntrega
        // 
        this.lblDataEntrega.Dpi = 254F;
        this.lblDataEntrega.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDataEntrega.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2200F);
        this.lblDataEntrega.Name = "lblDataEntrega";
        this.lblDataEntrega.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDataEntrega.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        this.lblDataEntrega.StylePriority.UseFont = false;
        this.lblDataEntrega.StylePriority.UseTextAlignment = false;
        this.lblDataEntrega.Text = "Data de entrega do relatório:";
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 750F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Foco em Resultados";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // lblNomeProjeto
        // 
        this.lblNomeProjeto.Dpi = 254F;
        this.lblNomeProjeto.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblNomeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1700F);
        this.lblNomeProjeto.Name = "lblNomeProjeto";
        this.lblNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNomeProjeto.SizeF = new System.Drawing.SizeF(1500F, 75F);
        this.lblNomeProjeto.StylePriority.UseFont = false;
        this.lblNomeProjeto.StylePriority.UseTextAlignment = false;
        this.lblNomeProjeto.Text = "LOGOMARCA/NOME DO PROJETO";
        this.lblNomeProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1100F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1500F, 75F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Período de Avaliação";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // lblPeriodo
        // 
        this.lblPeriodo.Dpi = 254F;
        this.lblPeriodo.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblPeriodo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1200F);
        this.lblPeriodo.Name = "lblPeriodo";
        this.lblPeriodo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblPeriodo.SizeF = new System.Drawing.SizeF(1500F, 58.42F);
        this.lblPeriodo.StylePriority.UseFont = false;
        this.lblPeriodo.StylePriority.UseTextAlignment = false;
        this.lblPeriodo.Text = "(dia) de (mês) de (ano) a (dia) de (mês) de (ano)";
        this.lblPeriodo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 650F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1500F, 75F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Relatório Gerencial";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 450F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1500F, 75F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Diretoria de Educação e Tecnologia";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(200F, 150F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(1100F, 200F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 250F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrTable1
        // 
        this.xrTable1.BorderColor = System.Drawing.Color.Silver;
        this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.ForeColor = System.Drawing.Color.Silver;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1500F, 60F);
        this.xrTable1.StylePriority.UseBorderColor = false;
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseForeColor = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.Weight = 1;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Unidade de Gestão e Desempenho";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell2.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.Weight = 1;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPageInfo1.BorderWidth = 0;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Format = "Página {0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 5F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(490F, 50F);
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseBorderWidth = false;
        this.xrPageInfo1.StylePriority.UsePadding = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // relSRGerencial_FR
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailGeral,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(300, 300, 249, 0);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "10.1";
        ((System.ComponentModel.ISupportInitialize)(this.htmlConsideracoesFinais)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlFinanceiro)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlFisico)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlMetas)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.htmlIntroducao)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Event Handlers

    #endregion
}
