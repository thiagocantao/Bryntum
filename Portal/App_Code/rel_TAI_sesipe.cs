using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for rel_TAI_sesipe
/// </summary>
public class rel_TAI_sesipe : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    /// 
    private dados cDados;
    private System.ComponentModel.IContainer components = null;
    private DS_rel_TAI_sesipe dS_rel_TAI_sesipe1;
    private DevExpress.XtraReports.Parameters.Parameter pNomeIniciativa;
    private XRLabel lblNomeIniciativa;
    private XRLabel xrLabel7;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel lblDataAprovacao;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel xrLabel21;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel1;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel24;
    private XRLine xrLine1;
    private XRControlStyle xrControlStyle1;
    private XRRichText xrRichText5;
    private XRRichText xrRichText4;
    private XRRichText xrRichText3;
    private XRRichText xrRichText2;
    private XRRichText xrRichText1;
    private CalculatedField cc_PeriodoVigencia;
    private XRLabel xrLabel5;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrLogoEntidade;
    public int codigoProjeto;

    public rel_TAI_sesipe(int codigoProjeto)
    {
        InitializeComponent();
        this.codigoProjeto = codigoProjeto;
        cDados = CdadosUtil.GetCdados(null);
        InitData();
        DefineLogoEntidade();
    }

    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        xrLogoEntidade.Image = Bitmap.FromStream(ms);
    }

    private void InitData()
    {
        int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        string comandosql = string.Format(@"
        DECLARE @RC int
        DECLARE @CodigoProcesso int
        set @CodigoProcesso = {2}
        EXECUTE @RC = {0}.{1}.p_SesiPE_DadosRelatorioProcesso   @CodigoProcesso", cDados.getDbName(), cDados.getDbOwner(), this.codigoProjeto);
        string connectionString = cDados.classeDados.getStringConexao();

        SqlDataAdapter adapter = new SqlDataAdapter(comandosql, connectionString);
        adapter.TableMappings.Add("Table", "tbTAIsesipe");
        adapter.Fill(dS_rel_TAI_sesipe1);
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
        string resourceFileName = "rel_TAI_sesipe.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_TAI_sesipe.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText5 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText4 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText3 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDataAprovacao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeIniciativa = new DevExpress.XtraReports.UI.XRLabel();
        this.pNomeIniciativa = new DevExpress.XtraReports.Parameters.Parameter();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dS_rel_TAI_sesipe1 = new DS_rel_TAI_sesipe();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.cc_PeriodoVigencia = new DevExpress.XtraReports.UI.CalculatedField();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_rel_TAI_sesipe1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrRichText5,
            this.xrRichText4,
            this.xrRichText3,
            this.xrRichText2,
            this.xrRichText1,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.lblDataAprovacao,
            this.xrLabel7,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.lblNomeIniciativa});
        this.Detail.HeightF = 510.6251F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbTAIsesipe.cc_PeriodoVigencia")});
        this.xrLabel5.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(211.0417F, 123.125F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(458.9586F, 23F);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrRichText5
        // 
        this.xrRichText5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbTAIsesipe.IndicadoresEMetas")});
        this.xrRichText5.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 434.4551F);
        this.xrRichText5.Name = "xrRichText5";
        this.xrRichText5.SerializableRtfString = resources.GetString("xrRichText5.SerializableRtfString");
        this.xrRichText5.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrRichText4
        // 
        this.xrRichText4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbTAIsesipe.Escopo")});
        this.xrRichText4.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText4.LocationFloat = new DevExpress.Utils.PointFloat(1.00015F, 374.625F);
        this.xrRichText4.Name = "xrRichText4";
        this.xrRichText4.SerializableRtfString = resources.GetString("xrRichText4.SerializableRtfString");
        this.xrRichText4.SizeF = new System.Drawing.SizeF(669F, 23F);
        // 
        // xrRichText3
        // 
        this.xrRichText3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbTAIsesipe.Justificativa")});
        this.xrRichText3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 317.3301F);
        this.xrRichText3.Name = "xrRichText3";
        this.xrRichText3.SerializableRtfString = resources.GetString("xrRichText3.SerializableRtfString");
        this.xrRichText3.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrRichText2
        // 
        this.xrRichText2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbTAIsesipe.Objetivo")});
        this.xrRichText2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 260.5F);
        this.xrRichText2.Name = "xrRichText2";
        this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
        this.xrRichText2.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbTAIsesipe.AlinhamentoEstrategico")});
        this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 203.2083F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrLabel23
        // 
        this.xrLabel23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel23.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel23.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel23.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(1.000166F, 411.4551F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel23.StylePriority.UseBackColor = false;
        this.xrLabel23.StylePriority.UseBorderColor = false;
        this.xrLabel23.StylePriority.UseBorders = false;
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseForeColor = false;
        this.xrLabel23.Text = "5. Indicadores e Metas";
        // 
        // xrLabel22
        // 
        this.xrLabel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel22.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 351.6251F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(670.0001F, 22.99994F);
        this.xrLabel22.StylePriority.UseBackColor = false;
        this.xrLabel22.StylePriority.UseBorderColor = false;
        this.xrLabel22.StylePriority.UseBorders = false;
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseForeColor = false;
        this.xrLabel22.Text = "4. Escopo";
        // 
        // xrLabel21
        // 
        this.xrLabel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel21.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 294.3301F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel21.StylePriority.UseBackColor = false;
        this.xrLabel21.StylePriority.UseBorderColor = false;
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseForeColor = false;
        this.xrLabel21.Text = "3. Justificativa";
        // 
        // xrLabel20
        // 
        this.xrLabel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel20.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel20.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 237.5F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel20.StylePriority.UseBackColor = false;
        this.xrLabel20.StylePriority.UseBorderColor = false;
        this.xrLabel20.StylePriority.UseBorders = false;
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseForeColor = false;
        this.xrLabel20.Text = "2. Objetivo";
        // 
        // xrLabel19
        // 
        this.xrLabel19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel19.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel19.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 180.2083F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel19.StylePriority.UseBackColor = false;
        this.xrLabel19.StylePriority.UseBorderColor = false;
        this.xrLabel19.StylePriority.UseBorders = false;
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseForeColor = false;
        this.xrLabel19.Text = "1. Alinhamento estratégico DR/PE";
        // 
        // xrLabel18
        // 
        this.xrLabel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel18.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 146.125F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(209.0416F, 23F);
        this.xrLabel18.StylePriority.UseBackColor = false;
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        this.xrLabel18.Text = "Valor estimado de recursos";
        // 
        // xrLabel17
        // 
        this.xrLabel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel17.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 123.125F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(209.0416F, 23F);
        this.xrLabel17.StylePriority.UseBackColor = false;
        this.xrLabel17.StylePriority.UseBorders = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseForeColor = false;
        this.xrLabel17.Text = "Período de Vigência";
        // 
        // xrLabel16
        // 
        this.xrLabel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel16.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 100.125F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(209.0417F, 23F);
        this.xrLabel16.StylePriority.UseBackColor = false;
        this.xrLabel16.StylePriority.UseBorders = false;
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseForeColor = false;
        this.xrLabel16.Text = "Área Unidade";
        // 
        // xrLabel15
        // 
        this.xrLabel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel15.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 77.12501F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(209.0417F, 23F);
        this.xrLabel15.StylePriority.UseBackColor = false;
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseForeColor = false;
        this.xrLabel15.Text = "Gestor Responsável";
        // 
        // xrLabel14
        // 
        this.xrLabel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel14.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 54.12499F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(209.0416F, 23F);
        this.xrLabel14.StyleName = "xrControlStyle1";
        this.xrLabel14.StylePriority.UseBackColor = false;
        this.xrLabel14.StylePriority.UseBorders = false;
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseForeColor = false;
        this.xrLabel14.Text = "Número da Iniciativa";
        // 
        // lblDataAprovacao
        // 
        this.lblDataAprovacao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbTAIsesipe.DataAprovacao", "{0:\'Data da aprovação:\' dd/MM/yyyy}")});
        this.lblDataAprovacao.Font = new System.Drawing.Font("Calibri", 12F);
        this.lblDataAprovacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.lblDataAprovacao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 474.4551F);
        this.lblDataAprovacao.Name = "lblDataAprovacao";
        this.lblDataAprovacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDataAprovacao.SizeF = new System.Drawing.SizeF(671F, 23F);
        this.lblDataAprovacao.StylePriority.UseFont = false;
        this.lblDataAprovacao.StylePriority.UseForeColor = false;
        this.lblDataAprovacao.StylePriority.UseTextAlignment = false;
        this.lblDataAprovacao.Text = "lblDataAprovacao";
        this.lblDataAprovacao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbTAIsesipe.ValorEstimado", "{0:c2}")});
        this.xrLabel7.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(211.0417F, 146.125F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(459.6249F, 25F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "xrLabel7";
        this.xrLabel7.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel7_BeforePrint);
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbTAIsesipe.AreaUnidade")});
        this.xrLabel4.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(211.0417F, 100.125F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(458.9584F, 23F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbTAIsesipe.GestorResponsavel")});
        this.xrLabel3.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(211.0417F, 77.12501F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(459.6248F, 23F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbTAIsesipe.NumeroIniciativa")});
        this.xrLabel2.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel2.ForeColor = System.Drawing.Color.Black;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(211.0417F, 54.12499F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(458.9584F, 23F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.Text = "xrLabel2";
        // 
        // lblNomeIniciativa
        // 
        this.lblNomeIniciativa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
        this.lblNomeIniciativa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pNomeIniciativa, "Text", "")});
        this.lblNomeIniciativa.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        this.lblNomeIniciativa.ForeColor = System.Drawing.Color.White;
        this.lblNomeIniciativa.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblNomeIniciativa.Name = "lblNomeIniciativa";
        this.lblNomeIniciativa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeIniciativa.SizeF = new System.Drawing.SizeF(671F, 40.04167F);
        this.lblNomeIniciativa.StylePriority.UseBackColor = false;
        this.lblNomeIniciativa.StylePriority.UseFont = false;
        this.lblNomeIniciativa.StylePriority.UseForeColor = false;
        this.lblNomeIniciativa.StylePriority.UseTextAlignment = false;
        this.lblNomeIniciativa.Text = "lblNomeIniciativa";
        this.lblNomeIniciativa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // pNomeIniciativa
        // 
        this.pNomeIniciativa.Name = "pNomeIniciativa";
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 79F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 29F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // dS_rel_TAI_sesipe1
        // 
        this.dS_rel_TAI_sesipe1.DataSetName = "DS_rel_TAI_sesipe";
        this.dS_rel_TAI_sesipe1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogoEntidade,
            this.xrLabel1});
        this.PageHeader.HeightF = 73.95834F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLogoEntidade
        // 
        this.xrLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLogoEntidade.Name = "xrLogoEntidade";
        this.xrLogoEntidade.SizeF = new System.Drawing.SizeF(27.08333F, 20.91667F);
        this.xrLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(211.0417F, 7.208332F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(458.9584F, 53.58334F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "TERMO DE ABERTURA DE INICIATIVA (TAI)";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24,
            this.xrLine1,
            this.xrPageInfo1});
        this.PageFooter.HeightF = 29.87499F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLabel24
        // 
        this.xrLabel24.Font = new System.Drawing.Font("Arial", 10F);
        this.xrLabel24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4.999987F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(637.5F, 23F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseForeColor = false;
        this.xrLabel24.Text = "TERMO DE ABERTURA DE INICIATIVA";
        // 
        // xrLine1
        // 
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(1.00015F, 0F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(667.9999F, 5F);
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 10F);
        this.xrPageInfo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(642.6667F, 4.999987F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(27.33331F, 23F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // 
        // cc_PeriodoVigencia
        // 
        this.cc_PeriodoVigencia.DataMember = "tbTAIsesipe";
        this.cc_PeriodoVigencia.Expression = resources.GetString("cc_PeriodoVigencia.Expression");
        this.cc_PeriodoVigencia.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_PeriodoVigencia.Name = "cc_PeriodoVigencia";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(5.5F, 7.208332F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(197.9583F, 61.95832F);
        // 
        // rel_TAI_sesipe
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cc_PeriodoVigencia});
        this.DataMember = "tbTAIsesipe";
        this.DataSource = this.dS_rel_TAI_sesipe1;
        this.Margins = new System.Drawing.Printing.Margins(78, 78, 79, 29);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pNomeIniciativa});
        this.SnapGridSize = 5F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "15.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_rel_TAI_sesipe1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void rel_TAI_sesipe_DataSourceDemanded(object sender, EventArgs e)
    {
        InitData();
    }

    private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel detail = (XRLabel)sender;

        DataRowView drv = (DataRowView)detail.Report.GetCurrentRow();

        string valor = string.Format("{0:c2}", drv.Row.Field<decimal?>("ValorEstimado"));
        if (valor == "")
        {
            valor = "R$ 0,00";
        }
        xrLabel7.Text = string.Format(@"{0}", valor);
    }
}
