using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_ImpressaoFormularios
/// </summary>
public class rel_AcompProcessosReprojPrincipal : DevExpress.XtraReports.UI.XtraReport
{
    #region variáveis do usuário

    private TopMarginBand topMarginBand1;
    private DetailBand detailBand1;
    private BottomMarginBand bottomMarginBand1;

    private Parameter pCodigoEntidade = new Parameter();
    private Parameter pNomeUnidade = new Parameter();
    private int codigoUnidadeNegocio = 0;
    private int codigoUsuarioGlobal = 0;
    private int codigoEntidadeGlobal = 0;

    Bitmap img;

    #endregion



    #region Fields

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    //private XRChart graficoBarras = new XRChart();
    private XRChart graficoBarrasEmpilhadas = new XRChart();
    private XRChart graficoPizzaSituacaoAtual = new XRChart();

    private XRLine linha = new XRLine();
    private DsAcompanhamento dsAcompanhamento1;
    private XRControlStyle xrControlStyle1;
    private FormattingRule formattingRule1;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel3;
    private XRPictureBox xrPictureBox1;

    private dados cDados = CdadosUtil.GetCdados(null);

    #endregion

    #region Constructors

    public rel_AcompProcessosReprojPrincipal(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {

        InitializeComponent();

        ReportHeader.Visible = codigoUnidade != -1;
        //PageHeader.Visible = codigoUnidade != -1;
        //DetailReport.Visible = codigoUnidade != -1;
        //DetailReport1.Visible = codigoUnidade != -1;
        //DetailReport2.Visible = codigoUnidade != -1;
        //PageFooter.Visible = codigoUnidade != -1;
        pCodigoEntidade.Name = "pCodigoEntidade";
        pNomeUnidade.Name = "pNomeUnidade";

        this.Parameters.AddRange(new Parameter[]
        {
            this.pCodigoEntidade,this.pNomeUnidade
        });
        codigoUnidadeNegocio = codigoUnidade; codigoUsuarioGlobal = codigoUsuario;
        codigoEntidadeGlobal = codigoEntidade;
        InitData(codigoUsuario, codigoEntidade, codigoUnidade, ano);
        DefineImagemCapa();
    }

    private void InitData(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;

        comandoSql = string.Format(@"DECLARE @RC int
        DECLARE @codigoUsuario int
        DECLARE @codigoEntidade int
        DECLARE @codigoUnidade int
        DECLARE @anoRef as smallint
         
        SET @codigoUsuario = {2}
        SET @codigoEntidade = {3}
        SET @codigoUnidade = {4}
        SET @anoRef = {5}
 
        EXECUTE @RC = {0}.{1}.p_DadosRelatorioReprojetos 
            @codigoUsuario
            ,@codigoEntidade
            ,@codigoUnidade, @anoRef", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, codigoUnidade, ano);

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

    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //string resourceFileName = "rel_AcompProcessosReprojPrincipal.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_AcompProcessosReprojPrincipal.ResourceManager;
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dsAcompanhamento1 = new DsAcompanhamento();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 1.833343F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // detailBand1
        // 
        this.detailBand1.Dpi = 254F;
        this.detailBand1.Font = new System.Drawing.Font("Verdana", 9F);
        this.detailBand1.HeightF = 0F;
        this.detailBand1.Name = "detailBand1";
        this.detailBand1.StylePriority.UseFont = false;
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 0F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // dsAcompanhamento1
        // 
        this.dsAcompanhamento1.DataSetName = "DsAcompanhamento";
        this.dsAcompanhamento1.EnforceConstraints = false;
        this.dsAcompanhamento1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        // 
        // formattingRule1
        // 
        this.formattingRule1.Name = "formattingRule1";
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 2965F;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(2101F, 2965F);
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Cambria", 14F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.ForeColor = System.Drawing.Color.MidnightBlue;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(373.0625F, 2684.415F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1259.417F, 53.12866F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseForeColor = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Diretoria de Educação e Tecnologia  DIRET";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // rel_AcompProcessosReprojPrincipal
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.ReportHeader});
        this.Dpi = 254F;
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 2, 0);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "12.1";
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rel_AcompProcessosReprojPrincipal_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this.dsAcompanhamento1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    private void DefineImagemCapa()
    {
        //string resourceFileName = "rel_AcompProcessosReprojPrincipal.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_AcompProcessosReprojPrincipal.ResourceManager;
        img = (Bitmap)resources.GetObject("xrPictureBox1.Image");
        xrPictureBox1.Image = img;
    }


    private void InsereCamposDinamicosNaCapa()
    {
        DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeNegocio);
        string nomeUnidade = "";
        string siglaUnidadeNegocio = "";
        string nomeEntidade = "";
        string siglaEntidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
            siglaUnidadeNegocio = ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
        }

        DataSet ds1 = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoEntidadeGlobal);
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            nomeEntidade = ds1.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
            siglaEntidade = ds1.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
        }

        String lblRelatorioDeProcesso = "Relatório de Processos";
        String lblRedesenhados = "Redesenhados";
        String lblUnidadeCapa = "";
        String lblEntidadeCapa = "";

        lblUnidadeCapa = string.Format(@"{0}", (nomeUnidade.IndexOf(siglaUnidadeNegocio) >= 0 ? nomeUnidade : nomeUnidade + " - " + siglaUnidadeNegocio));
        lblEntidadeCapa = string.Format(@"{0}", (nomeEntidade.IndexOf(siglaEntidade) >= 0 ? nomeEntidade : nomeEntidade + " - " + siglaEntidade));

        //testa se tem dois traços - que fazem repetir o nome da unidade e a sigla da unidade
        if (nomeUnidade.IndexOf(siglaUnidadeNegocio) != nomeUnidade.LastIndexOf(siglaUnidadeNegocio))
        {
            //se entrou aqui é porque tem esta característica
            //DIRET - Diretoria de Educação e Tecnologia - h_DIRET
            nomeUnidade = nomeUnidade.Substring(0, nomeUnidade.LastIndexOf(siglaUnidadeNegocio));
        }
        if (nomeEntidade.IndexOf(siglaEntidade) != nomeEntidade.LastIndexOf(siglaEntidade))
        {
            //a mesma coisa deve ser feita com a descrição da entidade
            nomeEntidade = nomeEntidade.Substring(0, nomeEntidade.LastIndexOf(siglaEntidade));
        }

        lblUnidadeCapa = nomeUnidade;//string.Format(@"{0}", (nomeUnidade.IndexOf(siglaUnidadeNegocio) >= 0 ? nomeUnidade : nomeUnidade + " - " + siglaUnidadeNegocio));
        lblEntidadeCapa = nomeEntidade;// string.Format(@"{0}", (nomeEntidade.IndexOf(siglaEntidade) >= 0 ? nomeEntidade : nomeEntidade + " - " + siglaEntidade));

        Graphics g = Graphics.FromImage(img);
        Font drawFont = new Font("Cambria", 22);
        Font drawFontRodapeEntidade = new Font("Cambria", 14, FontStyle.Bold);
        Font drawFontRodapeUnidade = new Font("Cambria", 14, FontStyle.Regular);

        SolidBrush drawBrush = new SolidBrush(Color.DarkBlue/*Color.FromArgb(0x1F, 0x49, 0x91)*/);

        SizeF stringSize = g.MeasureString(lblUnidadeCapa, drawFontRodapeUnidade);
        float x = (img.Width - stringSize.Width) / 2;
        float y = img.Height * 0.90f;
        g.DrawString(lblUnidadeCapa, drawFontRodapeUnidade, drawBrush, x, y);


        stringSize = g.MeasureString(lblEntidadeCapa, drawFontRodapeEntidade);
        x = (img.Width - stringSize.Width) / 2;
        y = img.Height * 0.90f;
        g.DrawString(lblEntidadeCapa, drawFontRodapeEntidade, drawBrush, x, y + 25);
        g.Flush();

        g.DrawString(lblRelatorioDeProcesso, drawFont, drawBrush, 425, 490);
        g.Flush();

        g.DrawString(lblRedesenhados, drawFont, drawBrush, 490, 545);
        g.Flush();
        g.Save();


    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        /*DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeNegocio);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }



        object x = "";
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoUnidadeNegocio, x);
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            //xrPictureBox1.Image = logo;
        }

        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1)
        {
            mesporextenso = "Janeiro";
        }
        else if (mes == 2)
        {
            mesporextenso = "Fevereiro";
        }
        else if (mes == 3)
        {
            mesporextenso = "Março";
        }
        else if (mes == 4)
        {
            mesporextenso = "Abril";
        }
        else if (mes == 5)
        {
            mesporextenso = "Maio";
        }
        else if (mes == 6)
        {
            mesporextenso = "Junho";
        }
        else if (mes == 7)
        {
            mesporextenso = "Julho";
        }
        else if (mes == 8)
        {
            mesporextenso = "Agosto";
        }
        else if (mes == 9)
        {
            mesporextenso = "Setembro";
        }
        else if (mes == 10)
        {
            mesporextenso = "Outubro";
        }
        else if (mes == 11)
        {
            mesporextenso = "Novembro";
        }
        else if (mes == 12)
        {
            mesporextenso = "Dezembro";
        }*/

        //lblCabecalho.Text = "RELATORIO DE ACOMPANHAMENTO DOS PROCESSOS REDESENHADOS - " + nomeUnidade;

    }

    private void rel_AcompProcessosReprojPrincipal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        InsereCamposDinamicosNaCapa();
    }

    private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        /*DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeNegocio);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1)
        {
            mesporextenso = "Janeiro";
        }
        else if (mes == 2)
        {
            mesporextenso = "Fevereiro";
        }
        else if (mes == 3)
        {
            mesporextenso = "Março";
        }
        else if (mes == 4)
        {
            mesporextenso = "Abril";
        }
        else if (mes == 5)
        {
            mesporextenso = "Maio";
        }
        else if (mes == 6)
        {
            mesporextenso = "Junho";
        }
        else if (mes == 7)
        {
            mesporextenso = "Julho";
        }
        else if (mes == 8)
        {
            mesporextenso = "Agosto";
        }
        else if (mes == 9)
        {
            mesporextenso = "Setembro";
        }
        else if (mes == 10)
        {
            mesporextenso = "Outubro";
        }
        else if (mes == 11)
        {
            mesporextenso = "Novembro";
        }
        else if (mes == 12)
        {
            mesporextenso = "Dezembro";
        }*/

        //lblPeriodoReferencia.Text = "Período de Referência: " + mesporextenso + "/" + ano.ToString();
    }

}
