using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for rel_TAI_sesipe_projeto
/// </summary>
public class rel_TAI_sesipe_projeto : DevExpress.XtraReports.UI.XtraReport
{
    private dados cDados;
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    public int codigoProjeto;
    private DevExpress.XtraReports.Parameters.Parameter pNomeIniciativa;
    private DS_rel_TAI_sesipe_projetos dS_rel_TAI_sesipe_projetos1;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel1;
    private XRRichText xrRichText2;
    private XRLabel xrLabel21;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRRichText xrRichText1;
    private XRRichText xrRichText3;
    private XRRichText xrRichText5;
    private XRLabel lblDataAprovacao;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRRichText xrRichText4;
    private XRLabel xrLabel7;
    private XRLabel xrLabel3;
    private XRLabel xrLabel15;
    private XRLabel xrLabel2;
    private XRLabel lblNomeIniciativa;
    private XRLabel xrLabel14;
    private XRLabel xrLabel16;
    private XRLabel xrLabel18;
    private XRLabel xrLabel4;
    private XRLabel xrLabel17;
    private XRLabel xrLabel8;
    private CalculatedField cc_PeriodoVigencia;
    private XRRichText xrRichText6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel6;
    private XRRichText xrRichText7;
    private XRLabel xrLabel9;
    private XRRichText xrRichText8;
    private XRRichText xrRichText9;
    private XRLabel xrLabel10;
    private XRLabel lblValorExercicioAno2;
    private XRLabel lblValorExercicioAno1;
    private XRPictureBox xrLogoEntidade;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_TAI_sesipe_projeto(int codigoProjeto)
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
        DECLARE @CodigoProjeto int
        set @CodigoProjeto = {2}
        EXECUTE @RC = {0}.{1}.p_SesiPE_DadosRelatorioProjeto   @CodigoProjeto", cDados.getDbName(), cDados.getDbOwner(), this.codigoProjeto);
        string connectionString = cDados.classeDados.getStringConexao();
        DataSet ds = cDados.getDataSet(comandosql);
        SqlDataAdapter adapter = new SqlDataAdapter(comandosql, connectionString);
        adapter.TableMappings.Add("Table", "tbProjetosTAI");
        adapter.Fill(dS_rel_TAI_sesipe_projetos1);
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
        string resourceFileName = "rel_TAI_sesipe_projeto.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_TAI_sesipe_projeto.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblValorExercicioAno2 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblValorExercicioAno1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText9 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText8 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText7 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText6 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText3 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrRichText5 = new DevExpress.XtraReports.UI.XRRichText();
        this.lblDataAprovacao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrRichText4 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeIniciativa = new DevExpress.XtraReports.UI.XRLabel();
        this.pNomeIniciativa = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dS_rel_TAI_sesipe_projetos1 = new DS_rel_TAI_sesipe_projetos();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.cc_PeriodoVigencia = new DevExpress.XtraReports.UI.CalculatedField();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_rel_TAI_sesipe_projetos1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblValorExercicioAno2,
            this.lblValorExercicioAno1,
            this.xrRichText9,
            this.xrLabel10,
            this.xrLabel9,
            this.xrRichText8,
            this.xrLabel6,
            this.xrRichText7,
            this.xrRichText6,
            this.xrLabel5,
            this.xrLabel8,
            this.xrRichText2,
            this.xrLabel21,
            this.xrLabel20,
            this.xrLabel19,
            this.xrRichText1,
            this.xrRichText3,
            this.xrRichText5,
            this.lblDataAprovacao,
            this.xrLabel23,
            this.xrLabel22,
            this.xrRichText4,
            this.xrLabel7,
            this.xrLabel3,
            this.xrLabel15,
            this.xrLabel2,
            this.lblNomeIniciativa,
            this.xrLabel14,
            this.xrLabel16,
            this.xrLabel18,
            this.xrLabel4,
            this.xrLabel17});
        this.Detail.HeightF = 847.1635F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblValorExercicioAno2
        // 
        this.lblValorExercicioAno2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblValorExercicioAno2.Font = new System.Drawing.Font("Calibri", 12F);
        this.lblValorExercicioAno2.LocationFloat = new DevExpress.Utils.PointFloat(155.25F, 199.2916F);
        this.lblValorExercicioAno2.Name = "lblValorExercicioAno2";
        this.lblValorExercicioAno2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblValorExercicioAno2.SizeF = new System.Drawing.SizeF(515.75F, 25.08331F);
        this.lblValorExercicioAno2.StylePriority.UseBorders = false;
        this.lblValorExercicioAno2.StylePriority.UseFont = false;
        this.lblValorExercicioAno2.Text = "2º. Ano (Exercício 2015) - R$ 00,00 - Fonte dos recursos: ";
        this.lblValorExercicioAno2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblValorExercicioAno2_BeforePrint);
        // 
        // lblValorExercicioAno1
        // 
        this.lblValorExercicioAno1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblValorExercicioAno1.Font = new System.Drawing.Font("Calibri", 12F);
        this.lblValorExercicioAno1.LocationFloat = new DevExpress.Utils.PointFloat(155.25F, 174.2083F);
        this.lblValorExercicioAno1.Name = "lblValorExercicioAno1";
        this.lblValorExercicioAno1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblValorExercicioAno1.SizeF = new System.Drawing.SizeF(515.75F, 25.08331F);
        this.lblValorExercicioAno1.StylePriority.UseBorders = false;
        this.lblValorExercicioAno1.StylePriority.UseFont = false;
        this.lblValorExercicioAno1.Text = "1º. Ano (Exercício 2014) - R$ 00,00 - Fonte dos recursos: ";
        this.lblValorExercicioAno1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblValorExercicioAno1_BeforePrint);
        // 
        // xrRichText9
        // 
        this.xrRichText9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.InfraEstruturaEquipe")});
        this.xrRichText9.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 762.0818F);
        this.xrRichText9.Name = "xrRichText9";
        this.xrRichText9.SerializableRtfString = resources.GetString("xrRichText9.SerializableRtfString");
        this.xrRichText9.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrLabel10
        // 
        this.xrLabel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 739.0817F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel10.StylePriority.UseBackColor = false;
        this.xrLabel10.StylePriority.UseBorderColor = false;
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseForeColor = false;
        this.xrLabel10.Text = "9. Infraestrutura e Equipe";
        // 
        // xrLabel9
        // 
        this.xrLabel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0.499939F, 616.0817F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(670.0001F, 22.99994F);
        this.xrLabel9.StylePriority.UseBackColor = false;
        this.xrLabel9.StylePriority.UseBorderColor = false;
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseForeColor = false;
        this.xrLabel9.Text = "7. Riscos e Restrições";
        // 
        // xrRichText8
        // 
        this.xrRichText8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.RiscosRestricoes")});
        this.xrRichText8.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText8.LocationFloat = new DevExpress.Utils.PointFloat(0.499939F, 639.0817F);
        this.xrRichText8.Name = "xrRichText8";
        this.xrRichText8.SerializableRtfString = resources.GetString("xrRichText8.SerializableRtfString");
        this.xrRichText8.SizeF = new System.Drawing.SizeF(669F, 23F);
        // 
        // xrLabel6
        // 
        this.xrLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0.499939F, 553.5817F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(670.0001F, 22.99994F);
        this.xrLabel6.StylePriority.UseBackColor = false;
        this.xrLabel6.StylePriority.UseBorderColor = false;
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseForeColor = false;
        this.xrLabel6.Text = "6. Não Escopo";
        // 
        // xrRichText7
        // 
        this.xrRichText7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.NaoEscopo")});
        this.xrRichText7.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText7.LocationFloat = new DevExpress.Utils.PointFloat(0.499939F, 576.5817F);
        this.xrRichText7.Name = "xrRichText7";
        this.xrRichText7.SerializableRtfString = resources.GetString("xrRichText7.SerializableRtfString");
        this.xrRichText7.SizeF = new System.Drawing.SizeF(669F, 23F);
        // 
        // xrRichText6
        // 
        this.xrRichText6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.ObjetivosEspecificos")});
        this.xrRichText6.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 398.5817F);
        this.xrRichText6.Name = "xrRichText6";
        this.xrRichText6.SerializableRtfString = resources.GetString("xrRichText6.SerializableRtfString");
        this.xrRichText6.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrLabel5
        // 
        this.xrLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 375.5817F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel5.StylePriority.UseBackColor = false;
        this.xrLabel5.StylePriority.UseBorderColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseForeColor = false;
        this.xrLabel5.Text = "3. Objetivos Específicos";
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbProjetosTAI.cc_PeriodoVigencia")});
        this.xrLabel8.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(155.25F, 123.125F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(514.7504F, 23.99998F);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "xrLabel8";
        // 
        // xrRichText2
        // 
        this.xrRichText2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.ObjetivoGeral")});
        this.xrRichText2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 336.5F);
        this.xrRichText2.Name = "xrRichText2";
        this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
        this.xrRichText2.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrLabel21
        // 
        this.xrLabel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel21.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 433.8301F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel21.StylePriority.UseBackColor = false;
        this.xrLabel21.StylePriority.UseBorderColor = false;
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseForeColor = false;
        this.xrLabel21.Text = "4. Justificativa";
        // 
        // xrLabel20
        // 
        this.xrLabel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel20.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel20.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 313.5F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel20.StylePriority.UseBackColor = false;
        this.xrLabel20.StylePriority.UseBorderColor = false;
        this.xrLabel20.StylePriority.UseBorders = false;
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseForeColor = false;
        this.xrLabel20.Text = "2. Objetivo Geral";
        // 
        // xrLabel19
        // 
        this.xrLabel19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel19.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel19.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 256.2083F);
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
        // xrRichText1
        // 
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.AlinhamentoEstrategico")});
        this.xrRichText1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 279.2083F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrRichText3
        // 
        this.xrRichText3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.Justificativa")});
        this.xrRichText3.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 456.8301F);
        this.xrRichText3.Name = "xrRichText3";
        this.xrRichText3.SerializableRtfString = resources.GetString("xrRichText3.SerializableRtfString");
        this.xrRichText3.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // xrRichText5
        // 
        this.xrRichText5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.IndicadoresEMetas")});
        this.xrRichText5.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 701.0817F);
        this.xrRichText5.Name = "xrRichText5";
        this.xrRichText5.SerializableRtfString = resources.GetString("xrRichText5.SerializableRtfString");
        this.xrRichText5.SizeF = new System.Drawing.SizeF(671F, 23F);
        // 
        // lblDataAprovacao
        // 
        this.lblDataAprovacao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbProjetosTAI.DataAprovacao", "{0:\'Data da aprovação:\' dd/MM/yyyy}")});
        this.lblDataAprovacao.Font = new System.Drawing.Font("Calibri", 12F);
        this.lblDataAprovacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.lblDataAprovacao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 824.1635F);
        this.lblDataAprovacao.Name = "lblDataAprovacao";
        this.lblDataAprovacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDataAprovacao.SizeF = new System.Drawing.SizeF(671F, 23F);
        this.lblDataAprovacao.StylePriority.UseFont = false;
        this.lblDataAprovacao.StylePriority.UseForeColor = false;
        this.lblDataAprovacao.StylePriority.UseTextAlignment = false;
        this.lblDataAprovacao.Text = "lblDataAprovacao";
        this.lblDataAprovacao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel23
        // 
        this.xrLabel23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel23.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel23.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel23.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 678.0817F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(669.0001F, 23F);
        this.xrLabel23.StylePriority.UseBackColor = false;
        this.xrLabel23.StylePriority.UseBorderColor = false;
        this.xrLabel23.StylePriority.UseBorders = false;
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseForeColor = false;
        this.xrLabel23.Text = "8. Indicadores e Metas";
        // 
        // xrLabel22
        // 
        this.xrLabel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
        this.xrLabel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel22.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 492.2501F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(670.0001F, 22.99994F);
        this.xrLabel22.StylePriority.UseBackColor = false;
        this.xrLabel22.StylePriority.UseBorderColor = false;
        this.xrLabel22.StylePriority.UseBorders = false;
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseForeColor = false;
        this.xrLabel22.Text = "5. Escopo";
        // 
        // xrRichText4
        // 
        this.xrRichText4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "tbProjetosTAI.Escopo")});
        this.xrRichText4.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrRichText4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 515.2501F);
        this.xrRichText4.Name = "xrRichText4";
        this.xrRichText4.SerializableRtfString = resources.GetString("xrRichText4.SerializableRtfString");
        this.xrRichText4.SizeF = new System.Drawing.SizeF(669F, 23F);
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbProjetosTAI.ValorTotal", "{0:c2}")});
        this.xrLabel7.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(155.25F, 149.125F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(221.6667F, 25.08331F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "[ValorTotal]";
        this.xrLabel7.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel7_BeforePrint);
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbProjetosTAI.GestorResponsavel")});
        this.xrLabel3.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(155.25F, 77.12498F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(515.4165F, 23F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLabel15
        // 
        this.xrLabel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel15.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 77.12498F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(153.25F, 23F);
        this.xrLabel15.StylePriority.UseBackColor = false;
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.StylePriority.UseForeColor = false;
        this.xrLabel15.Text = "Gestor Responsável";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbProjetosTAI.NumeroIniciativa")});
        this.xrLabel2.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel2.ForeColor = System.Drawing.Color.Black;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(155.25F, 54.12496F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(514.7501F, 23F);
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
        // xrLabel14
        // 
        this.xrLabel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel14.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 54.12496F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(153.25F, 23F);
        this.xrLabel14.StylePriority.UseBackColor = false;
        this.xrLabel14.StylePriority.UseBorders = false;
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseForeColor = false;
        this.xrLabel14.Text = "Número da Iniciativa";
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
        this.xrLabel16.SizeF = new System.Drawing.SizeF(153.2499F, 23F);
        this.xrLabel16.StylePriority.UseBackColor = false;
        this.xrLabel16.StylePriority.UseBorders = false;
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseForeColor = false;
        this.xrLabel16.Text = "Área Unidade";
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
        this.xrLabel18.SizeF = new System.Drawing.SizeF(153.2499F, 78.24995F);
        this.xrLabel18.StylePriority.UseBackColor = false;
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseForeColor = false;
        this.xrLabel18.Text = "Valor Estimado por exercício";
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbProjetosTAI.AreaUnidade")});
        this.xrLabel4.Font = new System.Drawing.Font("Calibri", 12F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(155.25F, 100.125F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(514.7501F, 23F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
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
        this.xrLabel17.SizeF = new System.Drawing.SizeF(153.25F, 23F);
        this.xrLabel17.StylePriority.UseBackColor = false;
        this.xrLabel17.StylePriority.UseBorders = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseForeColor = false;
        this.xrLabel17.Text = "Período de Vigência";
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 29F;
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
        // dS_rel_TAI_sesipe_projetos1
        // 
        this.dS_rel_TAI_sesipe_projetos1.DataSetName = "DS_rel_TAI_sesipe_projetos";
        this.dS_rel_TAI_sesipe_projetos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogoEntidade,
            this.xrLabel1});
        this.PageHeader.HeightF = 82.29166F;
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
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(212.0416F, 10F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(458.9584F, 53.58334F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "TERMO DE ABERTURA DE INICIATIVA (TAI)";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // cc_PeriodoVigencia
        // 
        this.cc_PeriodoVigencia.DataMember = "tbProjetosTAI";
        this.cc_PeriodoVigencia.DisplayName = "cc_PeriodoVigencia";
        this.cc_PeriodoVigencia.Expression = resources.GetString("cc_PeriodoVigencia.Expression");
        this.cc_PeriodoVigencia.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_PeriodoVigencia.Name = "cc_PeriodoVigencia";
        // 
        // rel_TAI_sesipe_projeto
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cc_PeriodoVigencia});
        this.DataMember = "tbProjetosTAI";
        this.DataSource = this.dS_rel_TAI_sesipe_projetos1;
        this.Margins = new System.Drawing.Printing.Margins(78, 78, 29, 29);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pNomeIniciativa});
        this.SnapGridSize = 5F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "15.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_rel_TAI_sesipe_projetos1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void lblValorExercicioAno1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel detail = (XRLabel)sender;

        DataRowView drv = (DataRowView)detail.Report.GetCurrentRow();

        string valor = string.Format("{0:c2}", drv.Row.Field<decimal?>("ValorRecursosAno1"));
        string fonte = drv.Row.Field<string>("FonteRecursosAno1");
        if (valor == "")
            valor = "R$ 0,00";
        lblValorExercicioAno1.Text = string.Format(@"1º. Ano - {0} - Fonte dos recursos: {1}", valor, fonte);
    }

    private void lblValorExercicioAno2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel detail = (XRLabel)sender;

        DataRowView drv = (DataRowView)detail.Report.GetCurrentRow();

        string valor = string.Format("{0:c2}", drv.Row.Field<decimal?>("ValorRecursosAno2"));
        string fonte = drv.Row.Field<string>("FonteRecursosAno2");
        if (valor == "")
            valor = "R$ 0,00";
        lblValorExercicioAno2.Text = string.Format(@"2º. Ano - {0} - Fonte dos recursos: {1}", valor, fonte);
    }

    private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel detail = (XRLabel)sender;

        DataRowView drv = (DataRowView)detail.Report.GetCurrentRow();

        string valor = string.Format("{0:c2}", drv.Row.Field<decimal?>("ValorTotal"));
        if (valor == "")
        {
            valor = "R$ 0,00";
        }
        xrLabel7.Text = string.Format(@"Total - {0}", valor);
    }
}
