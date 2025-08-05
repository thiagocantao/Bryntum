using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for relOficioDemanda
/// </summary>
public class relOficioDemanda : DevExpress.XtraReports.UI.XtraReport
{
    int codigoWorkflow;
    long codigoInstanciaWf;
    int codigoUsuario;
    string comiteDeliberacao;

    private dados cDados;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private DetailBand Detail;
    private PageHeaderBand PageHeader;
    private XRPictureBox imgLogoEntidade;
    private XRControlStyle xrControlStyle1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private dsOficioDemanda dsOficioDemanda1;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel lblData;
    private XRLabel xrLabel9;
    private XRLabel xrLabel5;
    private CalculatedField cfCorpoDocumento;
    private CalculatedField cfOrgaoDemandanteMaisSequencial;
    private XRLabel xrLabel10;
    private CalculatedField cfDenominacaoSigla;
    private XRLabel xrLabel13;
    private CalculatedField cfChaveAssinatura;
    private XRLabel xrLabel14;
    private XRLabel xrLabel15;
    private CalculatedField cfNumeroDemandaMaisDescricao;
    private CalculatedField cfDeliberacao;
    private CalculatedField cfCCOuAoSenhor;
    private XRLabel xrLabel19;
    private CalculatedField cfDenominacaoSiglaPainel2;
    private XRPanel xrPanel1;
    private XRLabel xrLabel24;
    private DevExpress.XtraReports.Parameters.Parameter pMostraChave;
    public DevExpress.XtraReports.Parameters.Parameter pComiteDeliberacao;
    private CalculatedField cfApareceCC;
    private CalculatedField cfApareceBeloHorizonte;
    private ReportFooterBand ReportFooter;
    private SubBand SubBand5;
    private SubBand SubBand6;
    private XRPanel painelPrimeiroBloco;
    private XRLabel xrLabel4;
    private XRLabel xrLabel22;
    private XRLabel xrLabel21;
    private XRPanel painelSegundoBloco;
    private XRLabel xrLabel20;
    private XRLabel xrLabel11;
    private XRLabel xrLabel3;
    private SubBand SubBand2;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLine xrLine1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relOficioDemanda(int codigoWorkflow, long codigoInstanciaWf, int codigoUsuario, string comiteDeliberacao)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        this.codigoWorkflow = codigoWorkflow;
        this.codigoInstanciaWf = codigoInstanciaWf;
        this.codigoUsuario = codigoUsuario;
        this.comiteDeliberacao = comiteDeliberacao;
        InitData();
    }

    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        imgLogoEntidade.Image = Bitmap.FromStream(ms);
    }

    private void InitData()
    {

        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        //xrPictureBox1.Image = logo;
        comandoSql = string.Format(@"
        DECLARE @RC int
        DECLARE @CodigoWorkflowParam int
        DECLARE @CodigoInstanciaWFParam bigint
        DECLARE @CodigoUsuarioParam int
        DECLARE @ComiteDeliberacao varchar(10)

        set @CodigoWorkflowParam = {0}
        set @CodigoInstanciaWFParam = {1}
        set @CodigoUsuarioParam = {2}
        set @ComiteDeliberacao = '{3}'

        EXECUTE @RC = [dbo].[p_pbh_GetDadosImpressaoOficio] 
           @CodigoWorkflowParam
          ,@CodigoInstanciaWFParam
          ,@CodigoUsuarioParam
          ,@ComiteDeliberacao", codigoWorkflow, codigoInstanciaWf, codigoUsuario, comiteDeliberacao);

        DataSet ds = cDados.getDataSet(comandoSql);

        dsOficioDemanda1.Load(ds.Tables[0].CreateDataReader(), LoadOption.OverwriteChanges, "tbOficioDemanda");
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
        string resourceFileName = "relOficioDemanda.resx";
        System.Resources.ResourceManager resources = global::Resources.relOficioDemanda.ResourceManager;
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblData = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.imgLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.dsOficioDemanda1 = new dsOficioDemanda();
        this.cfCorpoDocumento = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfOrgaoDemandanteMaisSequencial = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfDenominacaoSigla = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfChaveAssinatura = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfNumeroDemandaMaisDescricao = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfDeliberacao = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfCCOuAoSenhor = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfDenominacaoSiglaPainel2 = new DevExpress.XtraReports.UI.CalculatedField();
        this.pMostraChave = new DevExpress.XtraReports.Parameters.Parameter();
        this.pComiteDeliberacao = new DevExpress.XtraReports.Parameters.Parameter();
        this.cfApareceCC = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfApareceBeloHorizonte = new DevExpress.XtraReports.UI.CalculatedField();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.SubBand5 = new DevExpress.XtraReports.UI.SubBand();
        this.painelPrimeiroBloco = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.SubBand6 = new DevExpress.XtraReports.UI.SubBand();
        this.painelSegundoBloco = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.SubBand2 = new DevExpress.XtraReports.UI.SubBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        ((System.ComponentModel.ISupportInitialize)(this.dsOficioDemanda1)).BeginInit();
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
        this.BottomMargin.HeightF = 18.93163F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,
            this.xrLabel19,
            this.xrLabel15,
            this.xrLabel13,
            this.xrLabel10,
            this.xrLabel7,
            this.xrLabel6,
            this.lblData});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 1426.027F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel5,
            this.xrLabel9,
            this.xrLabel14});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 893.4786F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1649.996F, 393.5485F);
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.KeepTogether = true;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0.0002849803F, 0F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(1649.996F, 58.41998F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Atenciosamente,";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.NomeSecretario")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.KeepTogether = true;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 216.7353F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(1649.995F, 58.41998F);
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "xrLabel5";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.Secretaria")});
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.KeepTogether = true;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 276.1545F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(1649.995F, 58.41992F);
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "xrLabel9";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfChaveAssinatura")});
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Times New Roman", 12F);
        this.xrLabel14.KeepTogether = true;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 334.5747F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(1649.996F, 58.42004F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "xrLabel14";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel19
        // 
        this.xrLabel19.CanShrink = true;
        this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfNumeroDemandaMaisDescricao")});
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0.0006459554F, 493.9326F);
        this.xrLabel19.Multiline = true;
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(1649.996F, 58.41998F);
        this.xrLabel19.Text = "xrLabel19";
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfDeliberacao")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 672.6039F);
        this.xrLabel15.Multiline = true;
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(1650F, 58.42004F);
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "xrLabel15";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // xrLabel13
        // 
        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.ValorSolicitado", "Valor solicitado de: {0:c2}")});
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0.0006459554F, 552.3526F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(1649.996F, 58.41992F);
        this.xrLabel13.Text = "xrLabel13";
        this.xrLabel13.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.xrLabel13_PrintOnPage);
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfOrgaoDemandanteMaisSequencial")});
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 0F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(1650F, 58.42F);
        this.xrLabel10.Text = "xrLabel10";
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfCorpoDocumento")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 352.4582F);
        this.xrLabel7.Multiline = true;
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(1650F, 92.81592F);
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0.0006459554F, 253.8941F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(506.8126F, 58.42F);
        this.xrLabel6.Text = "Sr(a). Secretário(a)/Dirigente";
        // 
        // lblData
        // 
        this.lblData.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.DataOficio", "Belo Horizonte, {0:d\' de \'MMMM\' de \'yyyy}")});
        this.lblData.Dpi = 254F;
        this.lblData.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblData.LocationFloat = new DevExpress.Utils.PointFloat(582.0833F, 84.56081F);
        this.lblData.Name = "lblData";
        this.lblData.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblData.SizeF = new System.Drawing.SizeF(1067.917F, 58.41996F);
        this.lblData.StylePriority.UseFont = false;
        this.lblData.StylePriority.UseTextAlignment = false;
        this.lblData.Text = "lblData";
        this.lblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1,
            this.imgLogoEntidade});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 187.96F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Italic);
        this.xrLabel2.ForeColor = System.Drawing.Color.Gray;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(582.0833F, 95.35586F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1067.917F, 42.54499F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Secretaria Executiva";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Italic);
        this.xrLabel1.ForeColor = System.Drawing.Color.Gray;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(582.0833F, 52.81085F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1067.917F, 42.54498F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseForeColor = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Camara de Coodenação Geral";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // imgLogoEntidade
        // 
        this.imgLogoEntidade.Dpi = 254F;
        this.imgLogoEntidade.Image = ((System.Drawing.Image)(resources.GetObject("imgLogoEntidade.Image")));
        this.imgLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.imgLogoEntidade.Name = "imgLogoEntidade";
        this.imgLogoEntidade.SizeF = new System.Drawing.SizeF(531.8125F, 153.5642F);
        // 
        // xrLabel24
        // 
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 0F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLabel24.Text = "   ";
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrControlStyle1.Name = "xrControlStyle1";
        // 
        // dsOficioDemanda1
        // 
        this.dsOficioDemanda1.DataSetName = "dsOficioDemanda";
        this.dsOficioDemanda1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // cfCorpoDocumento
        // 
        this.cfCorpoDocumento.DataMember = "tbOficioDemanda";
        this.cfCorpoDocumento.Expression = resources.GetString("cfCorpoDocumento.Expression");
        this.cfCorpoDocumento.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfCorpoDocumento.Name = "cfCorpoDocumento";
        // 
        // cfOrgaoDemandanteMaisSequencial
        // 
        this.cfOrgaoDemandanteMaisSequencial.DataMember = "tbOficioDemanda";
        this.cfOrgaoDemandanteMaisSequencial.Expression = "\'OF. \'+[Parameters.pComiteDeliberacao]+\'/ \' + [NomeOrgao] + \' Nº. \' + [NumeroOfic" +
"io] + \'/\' + [AnoOficio]";
        this.cfOrgaoDemandanteMaisSequencial.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfOrgaoDemandanteMaisSequencial.Name = "cfOrgaoDemandanteMaisSequencial";
        // 
        // cfDenominacaoSigla
        // 
        this.cfDenominacaoSigla.DataMember = "tbOficioDemanda";
        this.cfDenominacaoSigla.Expression = "Iif(IsNullOrEmpty([NomeOrgaoRespSolicitacao]),\'\', [NomeOrgaoRespSolicitacao])";
        this.cfDenominacaoSigla.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfDenominacaoSigla.Name = "cfDenominacaoSigla";
        // 
        // cfChaveAssinatura
        // 
        this.cfChaveAssinatura.DataMember = "tbOficioDemanda";
        this.cfChaveAssinatura.Expression = "Iif([Parameters.pMostraChave] == \'S\',\r\nIif(IsNullOrEmpty([ChaveAssinatura]), \' \' " +
",\r\n\'Documento assinado digitalmente – Código de verificação: \' + [ChaveAssinatur" +
"a]) ,\'\' )";
        this.cfChaveAssinatura.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfChaveAssinatura.Name = "cfChaveAssinatura";
        // 
        // cfNumeroDemandaMaisDescricao
        // 
        this.cfNumeroDemandaMaisDescricao.DataMember = "tbOficioDemanda";
        this.cfNumeroDemandaMaisDescricao.DisplayName = "cfNumeroDemandaMaisDescricao";
        this.cfNumeroDemandaMaisDescricao.Expression = "\'Nº \' + [Parameters.pComiteDeliberacao] + \' \'+ [NumeroDemanda] + \' - \' + [Descric" +
"ao]";
        this.cfNumeroDemandaMaisDescricao.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfNumeroDemandaMaisDescricao.Name = "cfNumeroDemandaMaisDescricao";
        // 
        // cfDeliberacao
        // 
        this.cfDeliberacao.DataMember = "tbOficioDemanda";
        this.cfDeliberacao.DisplayName = "cfDeliberacao";
        this.cfDeliberacao.Expression = "\'Deliberação: \'+ [Deliberacao]";
        this.cfDeliberacao.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfDeliberacao.Name = "cfDeliberacao";
        // 
        // cfCCOuAoSenhor
        // 
        this.cfCCOuAoSenhor.DataMember = "tbOficioDemanda";
        this.cfCCOuAoSenhor.DisplayName = "cfCCOuAoSenhor";
        this.cfCCOuAoSenhor.Expression = "Iif([NomeRespSolicitacao]  == [NomeTitularOrgao], \'C/C.\' ,\'Ao Senhor\' )";
        this.cfCCOuAoSenhor.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfCCOuAoSenhor.Name = "cfCCOuAoSenhor";
        // 
        // cfDenominacaoSiglaPainel2
        // 
        this.cfDenominacaoSiglaPainel2.DataMember = "tbOficioDemanda";
        this.cfDenominacaoSiglaPainel2.DisplayName = "cfDenominacaoSiglaPainel2";
        this.cfDenominacaoSiglaPainel2.Expression = "Iif(IsNullOrEmpty([NomeOrgao]), \' \' ,[NomeOrgao])";
        this.cfDenominacaoSiglaPainel2.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfDenominacaoSiglaPainel2.Name = "cfDenominacaoSiglaPainel2";
        // 
        // pMostraChave
        // 
        this.pMostraChave.Description = "Indica se é pra mostrar a chave ou não";
        this.pMostraChave.Name = "pMostraChave";
        this.pMostraChave.Visible = false;
        // 
        // pComiteDeliberacao
        // 
        this.pComiteDeliberacao.Description = "Tipo de ofício, pode ser CCG, SMAO E SMARH";
        this.pComiteDeliberacao.Name = "pComiteDeliberacao";
        this.pComiteDeliberacao.Visible = false;
        // 
        // cfApareceCC
        // 
        this.cfApareceCC.DataMember = "tbOficioDemanda";
        this.cfApareceCC.Expression = "Iif(IsNullOrEmpty([NomeRespSolicitacao]), \'\', \'C/C.\')";
        this.cfApareceCC.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfApareceCC.Name = "cfApareceCC";
        // 
        // cfApareceBeloHorizonte
        // 
        this.cfApareceBeloHorizonte.DataMember = "tbOficioDemanda";
        this.cfApareceBeloHorizonte.Expression = "Iif(IsNullOrEmpty([NomeRespSolicitacao]), \'\' ,\'Belo Horizonte/MG\' )";
        this.cfApareceBeloHorizonte.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfApareceBeloHorizonte.Name = "cfApareceBeloHorizonte";
        // 
        // ReportFooter
        // 
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 0F;
        this.ReportFooter.Name = "ReportFooter";
        this.ReportFooter.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBandExceptFirstEntry;
        this.ReportFooter.SubBands.AddRange(new DevExpress.XtraReports.UI.SubBand[] {
            this.SubBand5,
            this.SubBand6,
            this.SubBand2});
        // 
        // SubBand5
        // 
        this.SubBand5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.painelPrimeiroBloco});
        this.SubBand5.Dpi = 254F;
        this.SubBand5.HeightF = 254F;
        this.SubBand5.Name = "SubBand5";
        this.SubBand5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SubBand2_BeforePrint);
        // 
        // painelPrimeiroBloco
        // 
        this.painelPrimeiroBloco.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel22,
            this.xrLabel21});
        this.painelPrimeiroBloco.Dpi = 254F;
        this.painelPrimeiroBloco.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.5F);
        this.painelPrimeiroBloco.Name = "painelPrimeiroBloco";
        this.painelPrimeiroBloco.SizeF = new System.Drawing.SizeF(1650F, 181.1043F);
        // 
        // xrLabel4
        // 
        this.xrLabel4.CanShrink = true;
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.051758E-05F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1649.999F, 58.42004F);
        this.xrLabel4.Text = "Ao (À) Senhor (a)";
        // 
        // xrLabel22
        // 
        this.xrLabel22.CanShrink = true;
        this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfDenominacaoSiglaPainel2")});
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 116.8401F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(1649.999F, 58.42004F);
        this.xrLabel22.Text = "\'";
        // 
        // xrLabel21
        // 
        this.xrLabel21.CanShrink = true;
        this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.NomeTitularOrgao")});
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58.42007F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(1649.999F, 58.42004F);
        this.xrLabel21.Text = "xrLabel11";
        // 
        // SubBand6
        // 
        this.SubBand6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.painelSegundoBloco});
        this.SubBand6.Dpi = 254F;
        this.SubBand6.HeightF = 254F;
        this.SubBand6.Name = "SubBand6";
        this.SubBand6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SubBand3_BeforePrint);
        // 
        // painelSegundoBloco
        // 
        this.painelSegundoBloco.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20,
            this.xrLabel11,
            this.xrLabel3});
        this.painelSegundoBloco.Dpi = 254F;
        this.painelSegundoBloco.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.5F);
        this.painelSegundoBloco.Name = "painelSegundoBloco";
        this.painelSegundoBloco.SizeF = new System.Drawing.SizeF(1650F, 189.0418F);
        // 
        // xrLabel20
        // 
        this.xrLabel20.CanShrink = true;
        this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfApareceCC")});
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.051758E-05F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(1649.999F, 58.42004F);
        this.xrLabel20.Text = "C/C.";
        // 
        // xrLabel11
        // 
        this.xrLabel11.CanShrink = true;
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.NomeRespSolicitacao")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58.41993F);
        this.xrLabel11.Multiline = true;
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1649.999F, 58.42004F);
        this.xrLabel11.Text = "xrLabel11";
        this.xrLabel11.TextTrimming = System.Drawing.StringTrimming.None;
        // 
        // xrLabel3
        // 
        this.xrLabel3.CanShrink = true;
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbOficioDemanda.cfDenominacaoSigla")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 116.84F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1649.999F, 58.42004F);
        this.xrLabel3.Text = "xrLabel3";
        // 
        // SubBand2
        // 
        this.SubBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24});
        this.SubBand2.Dpi = 254F;
        this.SubBand2.HeightF = 74.79849F;
        this.SubBand2.Name = "SubBand2";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLine1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 105.9764F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel18.ForeColor = System.Drawing.Color.Gray;
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 68.60282F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1650.001F, 26.67001F);
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = "Telefone: 3246-1660";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.ForeColor = System.Drawing.Color.Gray;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 41.9328F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(1650.001F, 26.67001F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseForeColor = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "Av. Augusto de Lima, 30 – 11º andar – Centro – CEP 30.190-001 – Belo Horizonte – " +
"MG";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel16.ForeColor = System.Drawing.Color.Gray;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15.26279F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(1650.001F, 26.67001F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseForeColor = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "Secretaria Executiva da Câmara de Coordenação Geral ";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLine1
        // 
        this.xrLine1.BorderColor = System.Drawing.Color.LightGray;
        this.xrLine1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLine1.BorderWidth = 0.5F;
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4.267757F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1649.999F, 5F);
        this.xrLine1.StylePriority.UseBorderColor = false;
        this.xrLine1.StylePriority.UseBorders = false;
        this.xrLine1.StylePriority.UseBorderWidth = false;
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // relOficioDemanda
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportFooter,
            this.PageFooter});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cfCorpoDocumento,
            this.cfOrgaoDemandanteMaisSequencial,
            this.cfDenominacaoSigla,
            this.cfChaveAssinatura,
            this.cfNumeroDemandaMaisDescricao,
            this.cfDeliberacao,
            this.cfCCOuAoSenhor,
            this.cfDenominacaoSiglaPainel2,
            this.cfApareceCC,
            this.cfApareceBeloHorizonte});
        this.DataMember = "tbOficioDemanda";
        this.DataSource = this.dsOficioDemanda1;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Times New Roman", 12F);
        this.Margins = new System.Drawing.Printing.Margins(300, 150, 100, 19);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pMostraChave,
            this.pComiteDeliberacao});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "15.1";
        ((System.ComponentModel.ISupportInitialize)(this.dsOficioDemanda1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion



    private void xrPanel1_PrintOnPage_1(object sender, PrintOnPageEventArgs e)
    {

    }

    private void painelPrimeiroBloco_PrintOnPage(object sender, PrintOnPageEventArgs e)
    {
        //Ajustar as informações de destinatário da seguinte forma:
        XRPanel painel = ((XRPanel)sender);

        object valorNomeRespSolicitacao = painel.Report.GetCurrentColumnValue("NomeRespSolicitacao");
        object valorNomeTitularOrgao = painel.Report.GetCurrentColumnValue("NomeTitularOrgao");
        if (valorNomeRespSolicitacao.ToString() == valorNomeTitularOrgao.ToString())
        {
            painelSegundoBloco.Visible = false;
            ((XRPanel)sender).HeightF = 0.0f;
        }

        if (e.PageIndex == 0)
        {
            ((XRPanel)sender).Visible = true;
        }
        else
        {
            ((XRPanel)sender).Visible = false;
            ((XRPanel)sender).HeightF = 0.0f;
            e.Cancel = true;
        }

    }

    private void painelSegundoBloco_PrintOnPage(object sender, PrintOnPageEventArgs e)
    {
        //Ajustar as informações de destinatário da seguinte forma:

        XRPanel painel = ((XRPanel)sender);

        object valorNomeRespSolicitacao = painel.Report.GetCurrentColumnValue("NomeRespSolicitacao");
        object valorNomeTitularOrgao = painel.Report.GetCurrentColumnValue("NomeTitularOrgao");
        if (valorNomeRespSolicitacao.ToString() == valorNomeTitularOrgao.ToString())
        {
            ((XRPanel)sender).Visible = false;
            ((XRPanel)sender).HeightF = 0.0f;
            e.Cancel = true;
        }
        else
        {
            SubBand5.Visible = true;
            if (e.PageIndex == 0)
            {
                ((XRPanel)sender).Visible = true;
            }
            else
            {
                ((XRPanel)sender).Visible = false;
                ((XRPanel)sender).HeightF = 0.0f;
                e.Cancel = true;
            }
        }
        SubBand5.HeightF = 0.0f;
    }

    private void xrLabel13_PrintOnPage(object sender, PrintOnPageEventArgs e)
    {
        XRLabel lbl = ((XRLabel)sender);

        object valorSolicitado = lbl.Report.GetCurrentColumnValue("ValorSolicitado");
        if (valorSolicitado.ToString() == "")
        {
            lbl.Visible = false;
            e.Cancel = true;
        }
    }

    private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void SubBand2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void painelSegundoBloco_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPanel painel = ((XRPanel)sender);

        object valorNomeRespSolicitacao = painel.Report.GetCurrentColumnValue("NomeRespSolicitacao");
        object valorNomeTitularOrgao = painel.Report.GetCurrentColumnValue("NomeTitularOrgao");
        if (valorNomeRespSolicitacao.ToString() == valorNomeTitularOrgao.ToString())
        {
            ((XRPanel)sender).HeightF = 0.0f;
            ((XRPanel)sender).Visible = false;
            e.Cancel = true;
        }
        else
        {
            ((XRPanel)sender).Visible = true;
        }
    }

    private void SubBand3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {


        object valorNomeRespSolicitacao = SubBand5.Report.GetCurrentColumnValue("NomeRespSolicitacao");
        object valorNomeTitularOrgao = SubBand5.Report.GetCurrentColumnValue("NomeTitularOrgao");
        if (valorNomeRespSolicitacao.ToString() == valorNomeTitularOrgao.ToString())
        {
            SubBand5.HeightF = 0.0f;
            SubBand5.Visible = false;
            e.Cancel = true;
        }
        else
        {
            SubBand5.HeightF = 0.0f;
            SubBand5.Visible = true;
            e.Cancel = false;
        }
    }

}
