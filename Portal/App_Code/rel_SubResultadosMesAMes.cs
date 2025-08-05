using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for relSubreportProdutos
/// </summary>
public class rel_SubResultadosMesAMes : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    public DevExpress.XtraReports.Parameters.Parameter CodProjeto;
    private dados cDados = CdadosUtil.GetCdados(null);
    private FormattingRule formattingRule1;
    private ReportFooterBand ReportFooter;

    private int codigoUnidadeGlobal = 0;
    private XRLabel xrLabel1;
    private XRLabel lblDescricaoRelatorio;
    private dsResultadosMesAMes dsResultadosMesAMes1;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell41;
    private XRTableCell celStatusJaneiro;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell44;
    private XRTableCell celStatusFevereiro;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell45;
    private XRTableCell celStatusMarco;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell4;
    private XRTableCell celStatusAbril;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell5;
    private XRTableCell celStatusMaio;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell12;
    private XRTableCell celStatusJunho;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable1;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRTable tbResultados2;
    private XRTableRow xrTableRow6;
    private XRTableCell celulaProjeto;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell92;
    private XRTableCell celStatusJulho;
    private XRTableCell xrTableCell94;
    private XRTableCell xrTableCell95;
    private XRTableCell celStatusAgosto;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTableCell celStatusSetembro;
    private XRTableCell xrTableCell100;
    private XRTableCell xrTableCell101;
    private XRTableCell celStatusOutubro;
    private XRTableCell xrTableCell103;
    private XRTableCell xrTableCell104;
    private XRTableCell celStatusNovembro;
    private XRTableCell xrTableCell106;
    private XRTableCell xrTableCell107;
    private XRTableCell celStatusDezembro;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell102;
    private XRTableCell xrTableCell105;
    private XRTableCell celCabecalhoJaneiro;
    private XRTableCell celCabecalhoFevereiro;
    private XRTableCell celCabecalhoMarco;
    private XRTableCell celCabecalhoAbril;
    private XRTableCell celCabecalhoMaio;
    private XRTableCell celCabecalhoJunho;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell114;
    private XRTableCell xrTableCell115;
    private XRTableCell xrTableCell116;
    private XRTableCell xrTableCell117;
    private XRTableCell xrTableCell118;
    private XRTableCell celCabecalhoJulho;
    private XRTableCell celCabecalhoAgosto;
    private XRTableCell celCabecalhoSetembro;
    private XRTableCell celCabecalhoOutubro;
    private XRTableCell celCabecalhoNovembro;
    private XRTableCell celCabecalhoDezembro;
    private XRTable xrTable5;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell125;
    private XRTableCell xrTableCell126;
    private XRTableCell xrTableCell127;
    private XRControlStyle xrControlStyle1;
    private int anoGlobal = 0;
    private XRTable xrTable4;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTable xrTable6;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTable xrTable7;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell31;
    private XRTable xrTable8;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTable xrTable9;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell42;
    private XRTable xrTable15;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell77;
    private XRTable xrTable14;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell74;
    private XRTable xrTable13;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTable xrTable12;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell53;
    private XRTable xrTable11;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRTable xrTable10;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPictureBox imgCabecalhoPagina;
    private XRPictureBox xrPictureBox2;
    private XRPageInfo xrPageInfo1;
    private XRControlStyle xrControlStyle2;
    private XRPictureBox xrPictureBox3;
    private XRPageInfo xrPageInfo3;
    Bitmap img;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_SubResultadosMesAMes(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {
        InitializeComponent();
        codigoUnidadeGlobal = codigoUnidade;
        anoGlobal = ano;
        InitData(codigoUsuario, codigoEntidade, codigoUnidade, ano);

    }

    private void InsereCamposDinamicosNaCapa()
    {

        Graphics g = Graphics.FromImage(img);
        Font drawFontRelProc = new Font("Microsoft Sans Serif", 42);
        Font drawFontMarAbr2013 = new Font("Century Gothic", 34);

        SolidBrush drawBrush = new SolidBrush(Color.Black/*Color.FromArgb(0x1F, 0x49, 0x91)*/);

        SizeF stringSize = g.MeasureString("Relatório de Processos Redesenhados", drawFontRelProc);
        float x = (img.Width - stringSize.Width) / 2;
        float y = img.Height * 0.9f;
        g.DrawString("Relatório de Processos Redesenhados", drawFontRelProc, drawBrush, x, 30);

        stringSize = g.MeasureString(getMesReferencia(), drawFontMarAbr2013);
        x = (img.Width - stringSize.Width) / 2;
        y = img.Height * 0.65f;
        g.DrawString(getMesReferencia(), drawFontMarAbr2013, drawBrush, x, y);
        g.Flush();
        g.Save();

    }

    private void DefineImagemCapa()
    {
        //string resourceFileName = "rel_SubResultadosMesAMes.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_SubResultadosMesAMes.ResourceManager;
        img = (Bitmap)resources.GetObject("imgCabecalhoPagina.Image");
        imgCabecalhoPagina.Image = img;
    }

    private void InitData(int codigoUsuario, int codigoEntidade, int codigoUnidade, int ano)
    {

        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;

        comandoSql = string.Format(@"DECLARE @RC int
                                     DECLARE @codigoUsuario int
                                     DECLARE @codigoEntidade int
                                     DECLARE @codigoUnidade int
                                     DECLARE @ano smallint
                                     
                                     SET @codigoUsuario = {2}
                                     SET @codigoEntidade = {3}
                                     SET @codigoUnidade = {4}
                                     SET @ano = {5}
                              
                                 EXECUTE @RC = {0}.{1}.p_DadosRelatorioReprojetos 
                                                       @codigoUsuario
                                                      ,@codigoEntidade
                                                      ,@codigoUnidade,@ano", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, codigoUnidade, ano);

        DataSet ds = cDados.getDataSet(comandoSql);
        //dsAcompanhamento1.Load(ds.Tables[6].CreateDataReader(), LoadOption.OverwriteChanges, "resultadosMesAMesSem1");
        //dsAcompanhamento1.Load(ds.Tables[7].CreateDataReader(), LoadOption.OverwriteChanges, "resultadosMesAMesSem2");

        dsResultadosMesAMes1.Load(ds.Tables[6].CreateDataReader(), LoadOption.OverwriteChanges, "resultadosMesAMesSem1");
        dsResultadosMesAMes1.Load(ds.Tables[7].CreateDataReader(), LoadOption.OverwriteChanges, "resultadosMesAMesSem2");
        DefineImagemCapa();
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
        //string resourceFileName = "rel_SubResultadosMesAMes.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_SubResultadosMesAMes.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDescricaoRelatorio = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.CodProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.dsResultadosMesAMes1 = new dsResultadosMesAMes();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusJaneiro = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusFevereiro = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusMarco = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusAbril = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusMaio = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusJunho = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoJaneiro = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoFevereiro = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoMarco = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoAbril = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoMaio = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoJunho = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell125 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell126 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell127 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.tbResultados2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celulaProjeto = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusJulho = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusAgosto = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusSetembro = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusOutubro = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusNovembro = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celStatusDezembro = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell114 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell115 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell117 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoJulho = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoAgosto = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoSetembro = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoOutubro = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoNovembro = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoDezembro = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.imgCabecalhoPagina = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
        ((System.ComponentModel.ISupportInitialize)(this.dsResultadosMesAMes1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbResultados2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.lblDescricaoRelatorio});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 207.2579F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // xrLabel1
        // 
        this.xrLabel1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(140.2292F, 39.77398F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(2748.707F, 52.21165F);
        this.xrLabel1.StylePriority.UseBorderDashStyle = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "2.  RESULTADOS CONSOLIDADOS MÊS A MÊS";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // lblDescricaoRelatorio
        // 
        this.lblDescricaoRelatorio.Dpi = 254F;
        this.lblDescricaoRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDescricaoRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(137.5833F, 127.2359F);
        this.lblDescricaoRelatorio.Name = "lblDescricaoRelatorio";
        this.lblDescricaoRelatorio.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDescricaoRelatorio.SizeF = new System.Drawing.SizeF(2748.314F, 60F);
        this.lblDescricaoRelatorio.StylePriority.UseFont = false;
        this.lblDescricaoRelatorio.Text = " Painel de Indicadores dos Processos redesenhados da <<Unidade>> - Período de Apu" +
"ração: xx/xxxx  a xx/xxxx";
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
        this.BottomMargin.HeightF = 44.87339F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // formattingRule1
        // 
        // 
        // 
        // 
        this.formattingRule1.Formatting.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.formattingRule1.Formatting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        this.formattingRule1.Name = "formattingRule1";
        // 
        // CodProjeto
        // 
        this.CodProjeto.Name = "CodProjeto";
        this.CodProjeto.Type = typeof(int);
        this.CodProjeto.ValueInfo = "0";
        this.CodProjeto.Visible = false;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 63.91557F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // dsResultadosMesAMes1
        // 
        this.dsResultadosMesAMes1.DataSetName = "dsResultadosMesAMes";
        this.dsResultadosMesAMes1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader2});
        this.DetailReport.DataMember = "resultadosMesAMesSem1";
        this.DetailReport.DataSource = this.dsResultadosMesAMes1;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 13.41375F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable2
        // 
        this.xrTable2.BorderColor = System.Drawing.Color.Silver;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(145.1976F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable2.SizeF = new System.Drawing.SizeF(2746.361F, 13.41375F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCell9,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell41,
            this.celStatusJaneiro,
            this.xrTableCell30,
            this.xrTableCell44,
            this.celStatusFevereiro,
            this.xrTableCell36,
            this.xrTableCell45,
            this.celStatusMarco,
            this.xrTableCell37,
            this.xrTableCell4,
            this.celStatusAbril,
            this.xrTableCell38,
            this.xrTableCell5,
            this.celStatusMaio,
            this.xrTableCell39,
            this.xrTableCell12,
            this.celStatusJunho});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.StylePriority.UseFont = false;
        this.xrTableRow3.Weight = 0.5679012345679012D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.Projeto")});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell6.Weight = 0.286997778237644D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.GerenteProjeto")});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UsePadding = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell9.Weight = 0.18803302640745429D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.NomeIndicador")});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.StylePriority.UsePadding = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell14.Weight = 0.24741186685640049D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.Periodicidade")});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell15.StylePriority.UseFont = false;
        this.xrTableCell15.StylePriority.UsePadding = false;
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell15.Weight = 0.076384369580487388D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.MetaDescritiva")});
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell28.StylePriority.UseFont = false;
        this.xrTableCell28.StylePriority.UsePadding = false;
        this.xrTableCell28.StylePriority.UseTextAlignment = false;
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell28.Weight = 0.31189802799815192D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.MetaJaneiro")});
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseFont = false;
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell29.Weight = 0.092997172649534915D;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.ResultadoJaneiro")});
        this.xrTableCell41.Dpi = 254F;
        this.xrTableCell41.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseFont = false;
        this.xrTableCell41.StylePriority.UseTextAlignment = false;
        this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell41.Weight = 0.10493232036552871D;
        // 
        // celStatusJaneiro
        // 
        this.celStatusJaneiro.BackColor = System.Drawing.Color.Transparent;
        this.celStatusJaneiro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.DesempenhoJaneiro")});
        this.celStatusJaneiro.Dpi = 254F;
        this.celStatusJaneiro.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
        this.celStatusJaneiro.ForeColor = System.Drawing.Color.Black;
        this.celStatusJaneiro.Name = "celStatusJaneiro";
        this.celStatusJaneiro.StylePriority.UseBackColor = false;
        this.celStatusJaneiro.StylePriority.UseFont = false;
        this.celStatusJaneiro.StylePriority.UseForeColor = false;
        this.celStatusJaneiro.StylePriority.UseTextAlignment = false;
        this.celStatusJaneiro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusJaneiro.Weight = 0.06993781059709088D;
        this.celStatusJaneiro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusJaneiro_BeforePrint);
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.MetaFevereiro")});
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.StylePriority.UseTextAlignment = false;
        this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell30.Weight = 0.092997176399411463D;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.ResultadoFevereiro")});
        this.xrTableCell44.Dpi = 254F;
        this.xrTableCell44.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.StylePriority.UseFont = false;
        this.xrTableCell44.StylePriority.UseTextAlignment = false;
        this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell44.Weight = 0.10493231940975861D;
        // 
        // celStatusFevereiro
        // 
        this.celStatusFevereiro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.DesempenhoFevereiro")});
        this.celStatusFevereiro.Dpi = 254F;
        this.celStatusFevereiro.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
        this.celStatusFevereiro.Name = "celStatusFevereiro";
        this.celStatusFevereiro.StylePriority.UseFont = false;
        this.celStatusFevereiro.StylePriority.UseTextAlignment = false;
        this.celStatusFevereiro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusFevereiro.Weight = 0.06993780073915809D;
        this.celStatusFevereiro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusFevereiro_BeforePrint);
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.MetaMarco")});
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseFont = false;
        this.xrTableCell36.StylePriority.UseTextAlignment = false;
        this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell36.Weight = 0.092997176622811359D;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.ResultadoMarco")});
        this.xrTableCell45.Dpi = 254F;
        this.xrTableCell45.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.StylePriority.UseFont = false;
        this.xrTableCell45.StylePriority.UseTextAlignment = false;
        this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell45.Weight = 0.10493232270011885D;
        // 
        // celStatusMarco
        // 
        this.celStatusMarco.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.DesempenhoMarco")});
        this.celStatusMarco.Dpi = 254F;
        this.celStatusMarco.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusMarco.Name = "celStatusMarco";
        this.celStatusMarco.StylePriority.UseFont = false;
        this.celStatusMarco.StylePriority.UseTextAlignment = false;
        this.celStatusMarco.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusMarco.Weight = 0.069937806251468948D;
        this.celStatusMarco.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusMarco_BeforePrint);
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.MetaAbril")});
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.StylePriority.UseFont = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell37.Weight = 0.092997174211462752D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.ResultadoAbril")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.10493232142672351D;
        // 
        // celStatusAbril
        // 
        this.celStatusAbril.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.DesempenhoAbril")});
        this.celStatusAbril.Dpi = 254F;
        this.celStatusAbril.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusAbril.Name = "celStatusAbril";
        this.celStatusAbril.StylePriority.UseFont = false;
        this.celStatusAbril.StylePriority.UseTextAlignment = false;
        this.celStatusAbril.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusAbril.Weight = 0.069937804867319833D;
        this.celStatusAbril.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusAbril_BeforePrint);
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.MetaMaio")});
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseFont = false;
        this.xrTableCell38.StylePriority.UseTextAlignment = false;
        this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell38.Weight = 0.092997172925113142D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.ResultadoMaio")});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell5.Weight = 0.10493232432778513D;
        // 
        // celStatusMaio
        // 
        this.celStatusMaio.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.DesempenhoMaio")});
        this.celStatusMaio.Dpi = 254F;
        this.celStatusMaio.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusMaio.Name = "celStatusMaio";
        this.celStatusMaio.StylePriority.UseFont = false;
        this.celStatusMaio.StylePriority.UseTextAlignment = false;
        this.celStatusMaio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusMaio.Weight = 0.069937803320559253D;
        this.celStatusMaio.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusMaio_BeforePrint);
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.MetaJunho")});
        this.xrTableCell39.Dpi = 254F;
        this.xrTableCell39.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.StylePriority.UseFont = false;
        this.xrTableCell39.StylePriority.UseTextAlignment = false;
        this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell39.Weight = 0.092997175061391074D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.ResultadoJunho")});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell12.Weight = 0.1049323230129951D;
        // 
        // celStatusJunho
        // 
        this.celStatusJunho.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem1.DesempenhoJunho")});
        this.celStatusJunho.Dpi = 254F;
        this.celStatusJunho.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusJunho.Name = "celStatusJunho";
        this.celStatusJunho.StylePriority.UseFont = false;
        this.celStatusJunho.StylePriority.UseTextAlignment = false;
        this.celStatusJunho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusJunho.Weight = 0.069938287380723663D;
        this.celStatusJunho.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusJunho_BeforePrint);
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.HeightF = 58.20835F;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.RepeatEveryPage = true;
        this.GroupHeader2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader2_BeforePrint);
        // 
        // xrTable1
        // 
        this.xrTable1.BorderColor = System.Drawing.Color.Silver;
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.BorderWidth = 1;
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(141.1976F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow4});
        this.xrTable1.SizeF = new System.Drawing.SizeF(2746.793F, 58.20833F);
        this.xrTable1.StylePriority.UseBorderColor = false;
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseBorderWidth = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.BackColor = System.Drawing.Color.DarkKhaki;
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell93,
            this.xrTableCell96,
            this.xrTableCell99,
            this.xrTableCell102,
            this.xrTableCell105,
            this.celCabecalhoJaneiro,
            this.celCabecalhoFevereiro,
            this.celCabecalhoMarco,
            this.celCabecalhoAbril,
            this.celCabecalhoMaio,
            this.celCabecalhoJunho});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Font = new System.Drawing.Font("Verdana", 3.75F);
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.StylePriority.UseBackColor = false;
        this.xrTableRow7.StylePriority.UseFont = false;
        this.xrTableRow7.Weight = 0.5679012345679012D;
        // 
        // xrTableCell93
        // 
        this.xrTableCell93.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell93.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell93.Dpi = 254F;
        this.xrTableCell93.Name = "xrTableCell93";
        this.xrTableCell93.StylePriority.UseBackColor = false;
        this.xrTableCell93.StylePriority.UseBorders = false;
        this.xrTableCell93.Weight = 0.19849142576349915D;
        // 
        // xrTableCell96
        // 
        this.xrTableCell96.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell96.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell96.Dpi = 254F;
        this.xrTableCell96.Name = "xrTableCell96";
        this.xrTableCell96.StylePriority.UseBackColor = false;
        this.xrTableCell96.StylePriority.UseBorders = false;
        this.xrTableCell96.Weight = 0.19849141794425068D;
        // 
        // xrTableCell99
        // 
        this.xrTableCell99.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell99.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell99.Dpi = 254F;
        this.xrTableCell99.Name = "xrTableCell99";
        this.xrTableCell99.StylePriority.UseBackColor = false;
        this.xrTableCell99.StylePriority.UseBorders = false;
        this.xrTableCell99.Weight = 0.2381897125549314D;
        // 
        // xrTableCell102
        // 
        this.xrTableCell102.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell102.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell102.Dpi = 254F;
        this.xrTableCell102.Name = "xrTableCell102";
        this.xrTableCell102.StylePriority.UseBackColor = false;
        this.xrTableCell102.StylePriority.UseBorders = false;
        this.xrTableCell102.Weight = 0.23818972177843434D;
        // 
        // xrTableCell105
        // 
        this.xrTableCell105.BackColor = System.Drawing.Color.Empty;
        this.xrTableCell105.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell105.Dpi = 254F;
        this.xrTableCell105.Name = "xrTableCell105";
        this.xrTableCell105.StylePriority.UseBackColor = false;
        this.xrTableCell105.StylePriority.UseBorders = false;
        this.xrTableCell105.Weight = 0.23736279103902239D;
        // 
        // celCabecalhoJaneiro
        // 
        this.celCabecalhoJaneiro.Dpi = 254F;
        this.celCabecalhoJaneiro.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoJaneiro.Name = "celCabecalhoJaneiro";
        this.celCabecalhoJaneiro.StylePriority.UseFont = false;
        this.celCabecalhoJaneiro.StylePriority.UseTextAlignment = false;
        this.celCabecalhoJaneiro.Text = "Janeiro / 2012";
        this.celCabecalhoJaneiro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoJaneiro.Weight = 0.26786728971532953D;
        // 
        // celCabecalhoFevereiro
        // 
        this.celCabecalhoFevereiro.Dpi = 254F;
        this.celCabecalhoFevereiro.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoFevereiro.Name = "celCabecalhoFevereiro";
        this.celCabecalhoFevereiro.StylePriority.UseFont = false;
        this.celCabecalhoFevereiro.StylePriority.UseTextAlignment = false;
        this.celCabecalhoFevereiro.Text = "Fevereiro / 2012";
        this.celCabecalhoFevereiro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoFevereiro.Weight = 0.26786729915089241D;
        // 
        // celCabecalhoMarco
        // 
        this.celCabecalhoMarco.Dpi = 254F;
        this.celCabecalhoMarco.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoMarco.Name = "celCabecalhoMarco";
        this.celCabecalhoMarco.StylePriority.UseFont = false;
        this.celCabecalhoMarco.StylePriority.UseTextAlignment = false;
        this.celCabecalhoMarco.Text = "Março/2012";
        this.celCabecalhoMarco.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoMarco.Weight = 0.26786730477997694D;
        // 
        // celCabecalhoAbril
        // 
        this.celCabecalhoAbril.Dpi = 254F;
        this.celCabecalhoAbril.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoAbril.Name = "celCabecalhoAbril";
        this.celCabecalhoAbril.StylePriority.UseFont = false;
        this.celCabecalhoAbril.StylePriority.UseTextAlignment = false;
        this.celCabecalhoAbril.Text = "Abril / 2012";
        this.celCabecalhoAbril.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoAbril.Weight = 0.26786729048984714D;
        // 
        // celCabecalhoMaio
        // 
        this.celCabecalhoMaio.Dpi = 254F;
        this.celCabecalhoMaio.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoMaio.Name = "celCabecalhoMaio";
        this.celCabecalhoMaio.StylePriority.UseFont = false;
        this.celCabecalhoMaio.StylePriority.UseTextAlignment = false;
        this.celCabecalhoMaio.Text = "Maio/2012";
        this.celCabecalhoMaio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoMaio.Weight = 0.26786729506102325D;
        // 
        // celCabecalhoJunho
        // 
        this.celCabecalhoJunho.Dpi = 254F;
        this.celCabecalhoJunho.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoJunho.Name = "celCabecalhoJunho";
        this.celCabecalhoJunho.StylePriority.UseFont = false;
        this.celCabecalhoJunho.StylePriority.UseTextAlignment = false;
        this.celCabecalhoJunho.Text = "Junho / 2012";
        this.celCabecalhoJunho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoJunho.Weight = 0.2678678130718859D;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.BackColor = System.Drawing.Color.DarkKhaki;
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell58,
            this.xrTableCell60,
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67,
            this.xrTableCell68});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Font = new System.Drawing.Font("Verdana", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.StylePriority.UseBackColor = false;
        this.xrTableRow4.StylePriority.UseFont = false;
        this.xrTableRow4.Weight = 0.5679012345679012D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "Projeto";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell3.Weight = 0.28695264973957263D;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.Dpi = 254F;
        this.xrTableCell58.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell58.StylePriority.UseFont = false;
        this.xrTableCell58.StylePriority.UsePadding = false;
        this.xrTableCell58.StylePriority.UseTextAlignment = false;
        this.xrTableCell58.Text = "Gerente";
        this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell58.Weight = 0.18800346291438227D;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.Dpi = 254F;
        this.xrTableCell60.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell60.StylePriority.UseFont = false;
        this.xrTableCell60.StylePriority.UsePadding = false;
        this.xrTableCell60.StylePriority.UseTextAlignment = false;
        this.xrTableCell60.Text = "Indicador";
        this.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell60.Weight = 0.2473729762782565D;
        // 
        // xrTableCell61
        // 
        this.xrTableCell61.Dpi = 254F;
        this.xrTableCell61.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell61.Name = "xrTableCell61";
        this.xrTableCell61.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell61.StylePriority.UseFont = false;
        this.xrTableCell61.StylePriority.UsePadding = false;
        this.xrTableCell61.StylePriority.UseTextAlignment = false;
        this.xrTableCell61.Text = "Period.";
        this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell61.Weight = 0.076478523367672135D;
        // 
        // xrTableCell62
        // 
        this.xrTableCell62.Dpi = 254F;
        this.xrTableCell62.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell62.Name = "xrTableCell62";
        this.xrTableCell62.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell62.StylePriority.UseFont = false;
        this.xrTableCell62.StylePriority.UsePadding = false;
        this.xrTableCell62.StylePriority.UseTextAlignment = false;
        this.xrTableCell62.Text = "Meta";
        this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell62.Weight = 0.31192259494857655D;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.xrTableCell63.Dpi = 254F;
        this.xrTableCell63.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.StylePriority.UseFont = false;
        this.xrTableCell63.StylePriority.UseTextAlignment = false;
        this.xrTableCell63.Text = "Janeiro / 2012";
        this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell63.Weight = 0.26786215154700743D;
        // 
        // xrTable5
        // 
        this.xrTable5.Dpi = 254F;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(1.072884E-05F, 3.197058F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable5.SizeF = new System.Drawing.SizeF(269.9F, 22.71004F);
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell125,
            this.xrTableCell126,
            this.xrTableCell127});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1D;
        // 
        // xrTableCell125
        // 
        this.xrTableCell125.Dpi = 254F;
        this.xrTableCell125.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell125.Name = "xrTableCell125";
        this.xrTableCell125.StylePriority.UseFont = false;
        this.xrTableCell125.Text = "Meta";
        this.xrTableCell125.Weight = 1.0444957527990018D;
        // 
        // xrTableCell126
        // 
        this.xrTableCell126.Dpi = 254F;
        this.xrTableCell126.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell126.Name = "xrTableCell126";
        this.xrTableCell126.StylePriority.UseFont = false;
        this.xrTableCell126.Text = "Resultado";
        this.xrTableCell126.Weight = 1.1785111611948225D;
        // 
        // xrTableCell127
        // 
        this.xrTableCell127.Dpi = 254F;
        this.xrTableCell127.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell127.Name = "xrTableCell127";
        this.xrTableCell127.StylePriority.UseFont = false;
        this.xrTableCell127.Text = "Status";
        this.xrTableCell127.Weight = 0.77694093340103842D;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.xrTableCell64.Dpi = 254F;
        this.xrTableCell64.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseFont = false;
        this.xrTableCell64.StylePriority.UseTextAlignment = false;
        this.xrTableCell64.Text = "Fevereiro / 2012";
        this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell64.Weight = 0.26786729915089241D;
        // 
        // xrTable4
        // 
        this.xrTable4.Dpi = 254F;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.4036407F, 3.197062F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable4.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.Text = "Meta";
        this.xrTableCell20.Weight = 1.044479812108156D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseFont = false;
        this.xrTableCell21.Text = "Resultado";
        this.xrTableCell21.Weight = 1.1785271018856685D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.Text = "Status";
        this.xrTableCell22.Weight = 0.77699308600617556D;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrTableCell65.Dpi = 254F;
        this.xrTableCell65.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.StylePriority.UseFont = false;
        this.xrTableCell65.StylePriority.UseTextAlignment = false;
        this.xrTableCell65.Text = "Março/2012";
        this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell65.Weight = 0.26786730477997694D;
        // 
        // xrTable6
        // 
        this.xrTable6.Dpi = 254F;
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0.4036407F, 3.197062F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable6.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.Text = "Meta";
        this.xrTableCell23.Weight = 1.044479812108156D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseFont = false;
        this.xrTableCell24.Text = "Resultado";
        this.xrTableCell24.Weight = 1.1785271018856685D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseFont = false;
        this.xrTableCell25.Text = "Status";
        this.xrTableCell25.Weight = 0.77699308600617556D;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.xrTableCell66.Dpi = 254F;
        this.xrTableCell66.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.StylePriority.UseFont = false;
        this.xrTableCell66.StylePriority.UseTextAlignment = false;
        this.xrTableCell66.Text = "Abril / 2012";
        this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell66.Weight = 0.26786729048984714D;
        // 
        // xrTable7
        // 
        this.xrTable7.Dpi = 254F;
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0.4036407F, 3.197062F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable7.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell31});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseFont = false;
        this.xrTableCell26.Text = "Meta";
        this.xrTableCell26.Weight = 1.044479812108156D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseFont = false;
        this.xrTableCell27.Text = "Resultado";
        this.xrTableCell27.Weight = 1.1785271018856685D;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseFont = false;
        this.xrTableCell31.Text = "Status";
        this.xrTableCell31.Weight = 0.77699308600617556D;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.xrTableCell67.Dpi = 254F;
        this.xrTableCell67.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseFont = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.Text = "Maio/2012";
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell67.Weight = 0.26786729506102325D;
        // 
        // xrTable8
        // 
        this.xrTable8.Dpi = 254F;
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0.4036407F, 3.197062F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
        this.xrTable8.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1D;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseFont = false;
        this.xrTableCell32.Text = "Meta";
        this.xrTableCell32.Weight = 1.044479812108156D;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.Text = "Resultado";
        this.xrTableCell33.Weight = 1.1785271018856685D;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseFont = false;
        this.xrTableCell34.Text = "Status";
        this.xrTableCell34.Weight = 0.77699308600617556D;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
        this.xrTableCell68.Dpi = 254F;
        this.xrTableCell68.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseFont = false;
        this.xrTableCell68.StylePriority.UseTextAlignment = false;
        this.xrTableCell68.Text = "Junho / 2012";
        this.xrTableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell68.Weight = 0.2678678130718859D;
        // 
        // xrTable9
        // 
        this.xrTable9.Dpi = 254F;
        this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0.4039764F, 3.197062F);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
        this.xrTable9.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell40,
            this.xrTableCell42});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1D;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Dpi = 254F;
        this.xrTableCell35.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.Text = "Meta";
        this.xrTableCell35.Weight = 1.044479812108156D;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseFont = false;
        this.xrTableCell40.Text = "Resultado";
        this.xrTableCell40.Weight = 1.1785271018856685D;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.Dpi = 254F;
        this.xrTableCell42.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.StylePriority.UseFont = false;
        this.xrTableCell42.Text = "Status";
        this.xrTableCell42.Weight = 0.77699308600617556D;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader1});
        this.DetailReport1.DataMember = "resultadosMesAMesSem2";
        this.DetailReport1.DataSource = this.dsResultadosMesAMes1;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 1;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tbResultados2});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 13.41375F;
        this.Detail2.Name = "Detail2";
        // 
        // tbResultados2
        // 
        this.tbResultados2.BorderColor = System.Drawing.Color.Silver;
        this.tbResultados2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.tbResultados2.Dpi = 254F;
        this.tbResultados2.Font = new System.Drawing.Font("Verdana", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.tbResultados2.LocationFloat = new DevExpress.Utils.PointFloat(146.0287F, 0F);
        this.tbResultados2.Name = "tbResultados2";
        this.tbResultados2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.tbResultados2.SizeF = new System.Drawing.SizeF(2745.53F, 13.41375F);
        this.tbResultados2.StylePriority.UseBorderColor = false;
        this.tbResultados2.StylePriority.UseBorders = false;
        this.tbResultados2.StylePriority.UseFont = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celulaProjeto,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell59,
            this.xrTableCell92,
            this.celStatusJulho,
            this.xrTableCell94,
            this.xrTableCell95,
            this.celStatusAgosto,
            this.xrTableCell97,
            this.xrTableCell98,
            this.celStatusSetembro,
            this.xrTableCell100,
            this.xrTableCell101,
            this.celStatusOutubro,
            this.xrTableCell103,
            this.xrTableCell104,
            this.celStatusNovembro,
            this.xrTableCell106,
            this.xrTableCell107,
            this.celStatusDezembro});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.StylePriority.UseFont = false;
        this.xrTableRow6.StylePriority.UseTextAlignment = false;
        this.xrTableRow6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableRow6.Weight = 0.5679012345679012D;
        // 
        // celulaProjeto
        // 
        this.celulaProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaProjeto.CanShrink = true;
        this.celulaProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.Projeto")});
        this.celulaProjeto.Dpi = 254F;
        this.celulaProjeto.Font = new System.Drawing.Font("Arial Narrow", 5.25F);
        this.celulaProjeto.KeepTogether = true;
        this.celulaProjeto.Multiline = true;
        this.celulaProjeto.Name = "celulaProjeto";
        this.celulaProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.celulaProjeto.StylePriority.UseBorders = false;
        this.celulaProjeto.StylePriority.UseFont = false;
        this.celulaProjeto.StylePriority.UsePadding = false;
        this.celulaProjeto.StylePriority.UseTextAlignment = false;
        this.celulaProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celulaProjeto.Weight = 0.25745771156888003D;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.GerenteProjeto")});
        this.xrTableCell54.Dpi = 254F;
        this.xrTableCell54.Font = new System.Drawing.Font("Arial Narrow", 5.25F);
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell54.StylePriority.UseBorders = false;
        this.xrTableCell54.StylePriority.UseFont = false;
        this.xrTableCell54.StylePriority.UsePadding = false;
        this.xrTableCell54.StylePriority.UseTextAlignment = false;
        this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell54.Weight = 0.16867919096990386D;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.NomeIndicador")});
        this.xrTableCell55.Dpi = 254F;
        this.xrTableCell55.Font = new System.Drawing.Font("Arial Narrow", 5.25F);
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell55.StylePriority.UseBorders = false;
        this.xrTableCell55.StylePriority.UseFont = false;
        this.xrTableCell55.StylePriority.UsePadding = false;
        this.xrTableCell55.StylePriority.UseTextAlignment = false;
        this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell55.Weight = 0.22194630228971282D;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.Periodicidade")});
        this.xrTableCell56.Dpi = 254F;
        this.xrTableCell56.Font = new System.Drawing.Font("Arial Narrow", 5.25F);
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell56.StylePriority.UseBorders = false;
        this.xrTableCell56.StylePriority.UseFont = false;
        this.xrTableCell56.StylePriority.UsePadding = false;
        this.xrTableCell56.StylePriority.UseTextAlignment = false;
        this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell56.Weight = 0.068608043182190451D;
        // 
        // xrTableCell57
        // 
        this.xrTableCell57.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.MetaDescritiva")});
        this.xrTableCell57.Dpi = 254F;
        this.xrTableCell57.Font = new System.Drawing.Font("Arial Narrow", 5.25F);
        this.xrTableCell57.Name = "xrTableCell57";
        this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell57.StylePriority.UseBorders = false;
        this.xrTableCell57.StylePriority.UseFont = false;
        this.xrTableCell57.StylePriority.UsePadding = false;
        this.xrTableCell57.StylePriority.UseTextAlignment = false;
        this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell57.Weight = 0.27984764893098213D;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.MetaJulho")});
        this.xrTableCell59.Dpi = 254F;
        this.xrTableCell59.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.StylePriority.UseBorders = false;
        this.xrTableCell59.StylePriority.UseFont = false;
        this.xrTableCell59.StylePriority.UseTextAlignment = false;
        this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell59.Weight = 0.088778516284452616D;
        // 
        // xrTableCell92
        // 
        this.xrTableCell92.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell92.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.ResultadoJulho")});
        this.xrTableCell92.Dpi = 254F;
        this.xrTableCell92.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell92.Name = "xrTableCell92";
        this.xrTableCell92.StylePriority.UseBorders = false;
        this.xrTableCell92.StylePriority.UseFont = false;
        this.xrTableCell92.StylePriority.UseTextAlignment = false;
        this.xrTableCell92.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell92.Weight = 0.0887785146642513D;
        // 
        // celStatusJulho
        // 
        this.celStatusJulho.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celStatusJulho.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.DesempenhoJulho")});
        this.celStatusJulho.Dpi = 254F;
        this.celStatusJulho.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusJulho.Name = "celStatusJulho";
        this.celStatusJulho.StylePriority.UseBorders = false;
        this.celStatusJulho.StylePriority.UseFont = false;
        this.celStatusJulho.StylePriority.UseTextAlignment = false;
        this.celStatusJulho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusJulho.Weight = 0.062065075849409616D;
        this.celStatusJulho.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusJulho_BeforePrint);
        // 
        // xrTableCell94
        // 
        this.xrTableCell94.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.MetaAgosto")});
        this.xrTableCell94.Dpi = 254F;
        this.xrTableCell94.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell94.Name = "xrTableCell94";
        this.xrTableCell94.StylePriority.UseBorders = false;
        this.xrTableCell94.StylePriority.UseFont = false;
        this.xrTableCell94.StylePriority.UseTextAlignment = false;
        this.xrTableCell94.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell94.Weight = 0.083425166616574065D;
        // 
        // xrTableCell95
        // 
        this.xrTableCell95.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.ResultadoAgosto")});
        this.xrTableCell95.Dpi = 254F;
        this.xrTableCell95.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell95.Name = "xrTableCell95";
        this.xrTableCell95.StylePriority.UseBorders = false;
        this.xrTableCell95.StylePriority.UseFont = false;
        this.xrTableCell95.StylePriority.UseTextAlignment = false;
        this.xrTableCell95.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell95.Weight = 0.09413185357971024D;
        // 
        // celStatusAgosto
        // 
        this.celStatusAgosto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celStatusAgosto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.DesempenhoAgosto")});
        this.celStatusAgosto.Dpi = 254F;
        this.celStatusAgosto.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusAgosto.Name = "celStatusAgosto";
        this.celStatusAgosto.StylePriority.UseBorders = false;
        this.celStatusAgosto.StylePriority.UseFont = false;
        this.celStatusAgosto.StylePriority.UseTextAlignment = false;
        this.celStatusAgosto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusAgosto.Weight = 0.061327061346489714D;
        this.celStatusAgosto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusAgosto_BeforePrint);
        // 
        // xrTableCell97
        // 
        this.xrTableCell97.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell97.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.MetaSetembro")});
        this.xrTableCell97.Dpi = 254F;
        this.xrTableCell97.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell97.Name = "xrTableCell97";
        this.xrTableCell97.StylePriority.UseBorders = false;
        this.xrTableCell97.StylePriority.UseFont = false;
        this.xrTableCell97.StylePriority.UseTextAlignment = false;
        this.xrTableCell97.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell97.Weight = 0.083425166927602379D;
        // 
        // xrTableCell98
        // 
        this.xrTableCell98.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell98.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.ResultadoSetembro")});
        this.xrTableCell98.Dpi = 254F;
        this.xrTableCell98.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell98.Name = "xrTableCell98";
        this.xrTableCell98.StylePriority.UseBorders = false;
        this.xrTableCell98.StylePriority.UseFont = false;
        this.xrTableCell98.StylePriority.UseTextAlignment = false;
        this.xrTableCell98.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell98.Weight = 0.094131856894426227D;
        // 
        // celStatusSetembro
        // 
        this.celStatusSetembro.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celStatusSetembro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.DesempenhoSetembro")});
        this.celStatusSetembro.Dpi = 254F;
        this.celStatusSetembro.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusSetembro.Name = "celStatusSetembro";
        this.celStatusSetembro.StylePriority.UseBorders = false;
        this.celStatusSetembro.StylePriority.UseFont = false;
        this.celStatusSetembro.StylePriority.UseTextAlignment = false;
        this.celStatusSetembro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusSetembro.Weight = 0.064248209404512854D;
        this.celStatusSetembro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusSetembro_BeforePrint);
        // 
        // xrTableCell100
        // 
        this.xrTableCell100.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell100.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.MetaOutubro")});
        this.xrTableCell100.Dpi = 254F;
        this.xrTableCell100.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell100.Name = "xrTableCell100";
        this.xrTableCell100.StylePriority.UseBorders = false;
        this.xrTableCell100.StylePriority.UseFont = false;
        this.xrTableCell100.StylePriority.UseTextAlignment = false;
        this.xrTableCell100.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell100.Weight = 0.0834251678060938D;
        // 
        // xrTableCell101
        // 
        this.xrTableCell101.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell101.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.ResultadoOutubro")});
        this.xrTableCell101.Dpi = 254F;
        this.xrTableCell101.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell101.Name = "xrTableCell101";
        this.xrTableCell101.StylePriority.UseBorders = false;
        this.xrTableCell101.StylePriority.UseFont = false;
        this.xrTableCell101.StylePriority.UseTextAlignment = false;
        this.xrTableCell101.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell101.Weight = 0.0941318556732104D;
        // 
        // celStatusOutubro
        // 
        this.celStatusOutubro.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celStatusOutubro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.DesempenhoOutubro")});
        this.celStatusOutubro.Dpi = 254F;
        this.celStatusOutubro.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusOutubro.Name = "celStatusOutubro";
        this.celStatusOutubro.StylePriority.UseBorders = false;
        this.celStatusOutubro.StylePriority.UseFont = false;
        this.celStatusOutubro.StylePriority.UseTextAlignment = false;
        this.celStatusOutubro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusOutubro.Weight = 0.062822734094930957D;
        this.celStatusOutubro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusOutubro_BeforePrint);
        // 
        // xrTableCell103
        // 
        this.xrTableCell103.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.MetaNovembro")});
        this.xrTableCell103.Dpi = 254F;
        this.xrTableCell103.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell103.Name = "xrTableCell103";
        this.xrTableCell103.StylePriority.UseBorders = false;
        this.xrTableCell103.StylePriority.UseFont = false;
        this.xrTableCell103.StylePriority.UseTextAlignment = false;
        this.xrTableCell103.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell103.Weight = 0.0834251665690664D;
        // 
        // xrTableCell104
        // 
        this.xrTableCell104.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell104.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.ResultadoNovembro")});
        this.xrTableCell104.Dpi = 254F;
        this.xrTableCell104.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell104.Name = "xrTableCell104";
        this.xrTableCell104.StylePriority.UseBorders = false;
        this.xrTableCell104.StylePriority.UseFont = false;
        this.xrTableCell104.StylePriority.UseTextAlignment = false;
        this.xrTableCell104.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell104.Weight = 0.094131858497736171D;
        // 
        // celStatusNovembro
        // 
        this.celStatusNovembro.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celStatusNovembro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.DesempenhoNovembro")});
        this.celStatusNovembro.Dpi = 254F;
        this.celStatusNovembro.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusNovembro.Name = "celStatusNovembro";
        this.celStatusNovembro.StylePriority.UseBorders = false;
        this.celStatusNovembro.StylePriority.UseFont = false;
        this.celStatusNovembro.StylePriority.UseTextAlignment = false;
        this.celStatusNovembro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusNovembro.Weight = 0.06251669620489067D;
        this.celStatusNovembro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusNovembro_BeforePrint);
        // 
        // xrTableCell106
        // 
        this.xrTableCell106.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell106.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.MetaDezembro")});
        this.xrTableCell106.Dpi = 254F;
        this.xrTableCell106.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell106.Name = "xrTableCell106";
        this.xrTableCell106.StylePriority.UseBorders = false;
        this.xrTableCell106.StylePriority.UseFont = false;
        this.xrTableCell106.StylePriority.UseTextAlignment = false;
        this.xrTableCell106.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell106.Weight = 0.083425168683469456D;
        // 
        // xrTableCell107
        // 
        this.xrTableCell107.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell107.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.ResultadoDezembro")});
        this.xrTableCell107.Dpi = 254F;
        this.xrTableCell107.Font = new System.Drawing.Font("Arial Narrow", 7F);
        this.xrTableCell107.Name = "xrTableCell107";
        this.xrTableCell107.StylePriority.UseBorders = false;
        this.xrTableCell107.StylePriority.UseFont = false;
        this.xrTableCell107.StylePriority.UseTextAlignment = false;
        this.xrTableCell107.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell107.Weight = 0.094131857193874249D;
        // 
        // celStatusDezembro
        // 
        this.celStatusDezembro.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celStatusDezembro.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "resultadosMesAMesSem2.DesempenhoDezembro")});
        this.celStatusDezembro.Dpi = 254F;
        this.celStatusDezembro.Font = new System.Drawing.Font("Wingdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celStatusDezembro.Name = "celStatusDezembro";
        this.celStatusDezembro.StylePriority.UseBorders = false;
        this.celStatusDezembro.StylePriority.UseFont = false;
        this.celStatusDezembro.StylePriority.UseTextAlignment = false;
        this.celStatusDezembro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celStatusDezembro.Weight = 0.062579914243722223D;
        this.celStatusDezembro.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celStatusDezembro_BeforePrint);
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 80.20757F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
        // 
        // xrTable3
        // 
        this.xrTable3.BorderColor = System.Drawing.Color.Silver;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(143.1976F, 21.99921F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8,
            this.xrTableRow1});
        this.xrTable3.SizeF = new System.Drawing.SizeF(2746.73F, 58.20836F);
        this.xrTable3.StylePriority.UseBorderColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.BackColor = System.Drawing.Color.DarkKhaki;
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell114,
            this.xrTableCell115,
            this.xrTableCell116,
            this.xrTableCell117,
            this.xrTableCell118,
            this.celCabecalhoJulho,
            this.celCabecalhoAgosto,
            this.celCabecalhoSetembro,
            this.celCabecalhoOutubro,
            this.celCabecalhoNovembro,
            this.celCabecalhoDezembro});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Font = new System.Drawing.Font("Verdana", 3.75F);
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.StylePriority.UseBackColor = false;
        this.xrTableRow8.StylePriority.UseFont = false;
        this.xrTableRow8.Weight = 0.5679012345679012D;
        // 
        // xrTableCell114
        // 
        this.xrTableCell114.BackColor = System.Drawing.Color.Transparent;
        this.xrTableCell114.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell114.Dpi = 254F;
        this.xrTableCell114.Name = "xrTableCell114";
        this.xrTableCell114.StylePriority.UseBackColor = false;
        this.xrTableCell114.StylePriority.UseBorders = false;
        this.xrTableCell114.Weight = 0.19849142576349915D;
        // 
        // xrTableCell115
        // 
        this.xrTableCell115.BackColor = System.Drawing.Color.Transparent;
        this.xrTableCell115.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell115.Dpi = 254F;
        this.xrTableCell115.Name = "xrTableCell115";
        this.xrTableCell115.StylePriority.UseBackColor = false;
        this.xrTableCell115.StylePriority.UseBorders = false;
        this.xrTableCell115.Weight = 0.19849141794425068D;
        // 
        // xrTableCell116
        // 
        this.xrTableCell116.BackColor = System.Drawing.Color.Transparent;
        this.xrTableCell116.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell116.Dpi = 254F;
        this.xrTableCell116.Name = "xrTableCell116";
        this.xrTableCell116.StylePriority.UseBackColor = false;
        this.xrTableCell116.StylePriority.UseBorders = false;
        this.xrTableCell116.Weight = 0.2381897125549314D;
        // 
        // xrTableCell117
        // 
        this.xrTableCell117.BackColor = System.Drawing.Color.Transparent;
        this.xrTableCell117.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell117.Dpi = 254F;
        this.xrTableCell117.Name = "xrTableCell117";
        this.xrTableCell117.StylePriority.UseBackColor = false;
        this.xrTableCell117.StylePriority.UseBorders = false;
        this.xrTableCell117.Weight = 0.14558258039037178D;
        // 
        // xrTableCell118
        // 
        this.xrTableCell118.BackColor = System.Drawing.Color.Transparent;
        this.xrTableCell118.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell118.Dpi = 254F;
        this.xrTableCell118.Name = "xrTableCell118";
        this.xrTableCell118.StylePriority.UseBackColor = false;
        this.xrTableCell118.StylePriority.UseBorders = false;
        this.xrTableCell118.Weight = 0.32996993242708494D;
        // 
        // celCabecalhoJulho
        // 
        this.celCabecalhoJulho.Dpi = 254F;
        this.celCabecalhoJulho.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoJulho.Name = "celCabecalhoJulho";
        this.celCabecalhoJulho.StylePriority.UseFont = false;
        this.celCabecalhoJulho.StylePriority.UseTextAlignment = false;
        this.celCabecalhoJulho.Text = "Julho/ 2012";
        this.celCabecalhoJulho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoJulho.Weight = 0.26786728971532953D;
        // 
        // celCabecalhoAgosto
        // 
        this.celCabecalhoAgosto.Dpi = 254F;
        this.celCabecalhoAgosto.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoAgosto.Name = "celCabecalhoAgosto";
        this.celCabecalhoAgosto.StylePriority.UseFont = false;
        this.celCabecalhoAgosto.StylePriority.UseTextAlignment = false;
        this.celCabecalhoAgosto.Text = "Agosto / 2012";
        this.celCabecalhoAgosto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoAgosto.Weight = 0.26786729915089241D;
        // 
        // celCabecalhoSetembro
        // 
        this.celCabecalhoSetembro.Dpi = 254F;
        this.celCabecalhoSetembro.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoSetembro.Name = "celCabecalhoSetembro";
        this.celCabecalhoSetembro.StylePriority.UseFont = false;
        this.celCabecalhoSetembro.StylePriority.UseTextAlignment = false;
        this.celCabecalhoSetembro.Text = "Setembro/ 2012";
        this.celCabecalhoSetembro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoSetembro.Weight = 0.26786730477997694D;
        // 
        // celCabecalhoOutubro
        // 
        this.celCabecalhoOutubro.Dpi = 254F;
        this.celCabecalhoOutubro.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoOutubro.Name = "celCabecalhoOutubro";
        this.celCabecalhoOutubro.StylePriority.UseFont = false;
        this.celCabecalhoOutubro.StylePriority.UseTextAlignment = false;
        this.celCabecalhoOutubro.Text = "Outubro / 2012";
        this.celCabecalhoOutubro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoOutubro.Weight = 0.26786729048984714D;
        // 
        // celCabecalhoNovembro
        // 
        this.celCabecalhoNovembro.Dpi = 254F;
        this.celCabecalhoNovembro.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoNovembro.Name = "celCabecalhoNovembro";
        this.celCabecalhoNovembro.StylePriority.UseFont = false;
        this.celCabecalhoNovembro.StylePriority.UseTextAlignment = false;
        this.celCabecalhoNovembro.Text = "Novembro / 2012";
        this.celCabecalhoNovembro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoNovembro.Weight = 0.26786729506102325D;
        // 
        // celCabecalhoDezembro
        // 
        this.celCabecalhoDezembro.Dpi = 254F;
        this.celCabecalhoDezembro.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.celCabecalhoDezembro.Name = "celCabecalhoDezembro";
        this.celCabecalhoDezembro.StylePriority.UseFont = false;
        this.celCabecalhoDezembro.StylePriority.UseTextAlignment = false;
        this.celCabecalhoDezembro.Text = "Dezembro/ 2012";
        this.celCabecalhoDezembro.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoDezembro.Weight = 0.2678678130718859D;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.BackColor = System.Drawing.Color.DarkKhaki;
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell13,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.StylePriority.UseBackColor = false;
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.DarkKhaki;
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Projeto";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell1.Weight = 0.28695923132055257D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Gerente";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 0.18800776556129711D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Indicador";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell7.Weight = 0.24737865089225869D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "Period.";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell8.Weight = 0.07646479925663284D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UsePadding = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Meta";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell10.Weight = 0.31191462204939679D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable15});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "Julho / 2012";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell11.Weight = 0.26786728971532953D;
        // 
        // xrTable15
        // 
        this.xrTable15.Dpi = 254F;
        this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(0.400528F, 3.19707F);
        this.xrTable15.Name = "xrTable15";
        this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18});
        this.xrTable15.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow18
        // 
        this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell75,
            this.xrTableCell76,
            this.xrTableCell77});
        this.xrTableRow18.Dpi = 254F;
        this.xrTableRow18.Name = "xrTableRow18";
        this.xrTableRow18.Weight = 1D;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Dpi = 254F;
        this.xrTableCell75.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseFont = false;
        this.xrTableCell75.Text = "Meta";
        this.xrTableCell75.Weight = 1.044479812108156D;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.StylePriority.UseFont = false;
        this.xrTableCell76.Text = "Resultado";
        this.xrTableCell76.Weight = 1.1785271018856685D;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.Dpi = 254F;
        this.xrTableCell77.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.StylePriority.UseFont = false;
        this.xrTableCell77.Text = "Status";
        this.xrTableCell77.Weight = 0.77699308600617556D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "Agosto / 2012";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell13.Weight = 0.26786729915089241D;
        // 
        // xrTable14
        // 
        this.xrTable14.Dpi = 254F;
        this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(0.400528F, 3.19707F);
        this.xrTable14.Name = "xrTable14";
        this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
        this.xrTable14.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow17
        // 
        this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell72,
            this.xrTableCell73,
            this.xrTableCell74});
        this.xrTableRow17.Dpi = 254F;
        this.xrTableRow17.Name = "xrTableRow17";
        this.xrTableRow17.Weight = 1D;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Dpi = 254F;
        this.xrTableCell72.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseFont = false;
        this.xrTableCell72.Text = "Meta";
        this.xrTableCell72.Weight = 1.044479812108156D;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Dpi = 254F;
        this.xrTableCell73.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.StylePriority.UseFont = false;
        this.xrTableCell73.Text = "Resultado";
        this.xrTableCell73.Weight = 1.1785271018856685D;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Dpi = 254F;
        this.xrTableCell74.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.StylePriority.UseFont = false;
        this.xrTableCell74.Text = "Status";
        this.xrTableCell74.Weight = 0.77699308600617556D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseFont = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "Setembro / 2012";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell16.Weight = 0.26786730477997694D;
        // 
        // xrTable13
        // 
        this.xrTable13.Dpi = 254F;
        this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(0.4005432F, 3.19707F);
        this.xrTable13.Name = "xrTable13";
        this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
        this.xrTable13.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71});
        this.xrTableRow16.Dpi = 254F;
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 1D;
        // 
        // xrTableCell69
        // 
        this.xrTableCell69.Dpi = 254F;
        this.xrTableCell69.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell69.Name = "xrTableCell69";
        this.xrTableCell69.StylePriority.UseFont = false;
        this.xrTableCell69.Text = "Meta";
        this.xrTableCell69.Weight = 1.044479812108156D;
        // 
        // xrTableCell70
        // 
        this.xrTableCell70.Dpi = 254F;
        this.xrTableCell70.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell70.Name = "xrTableCell70";
        this.xrTableCell70.StylePriority.UseFont = false;
        this.xrTableCell70.Text = "Resultado";
        this.xrTableCell70.Weight = 1.1785271018856685D;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Dpi = 254F;
        this.xrTableCell71.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.StylePriority.UseFont = false;
        this.xrTableCell71.Text = "Status";
        this.xrTableCell71.Weight = 0.77699308600617556D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "Outubro / 2012";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell17.Weight = 0.26786729048984714D;
        // 
        // xrTable12
        // 
        this.xrTable12.Dpi = 254F;
        this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0.400528F, 3.19707F);
        this.xrTable12.Name = "xrTable12";
        this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
        this.xrTable12.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell53});
        this.xrTableRow15.Dpi = 254F;
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 1D;
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Dpi = 254F;
        this.xrTableCell51.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.StylePriority.UseFont = false;
        this.xrTableCell51.Text = "Meta";
        this.xrTableCell51.Weight = 1.044479812108156D;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.Dpi = 254F;
        this.xrTableCell52.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.StylePriority.UseFont = false;
        this.xrTableCell52.Text = "Resultado";
        this.xrTableCell52.Weight = 1.1785271018856685D;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.Dpi = 254F;
        this.xrTableCell53.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.StylePriority.UseFont = false;
        this.xrTableCell53.Text = "Status";
        this.xrTableCell53.Weight = 0.77699308600617556D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseFont = false;
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = "Novembro / 2012";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell18.Weight = 0.26786729506102325D;
        // 
        // xrTable11
        // 
        this.xrTable11.Dpi = 254F;
        this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0.400528F, 3.19707F);
        this.xrTable11.Name = "xrTable11";
        this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
        this.xrTable11.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell48,
            this.xrTableCell49,
            this.xrTableCell50});
        this.xrTableRow14.Dpi = 254F;
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 1D;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.Dpi = 254F;
        this.xrTableCell48.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.StylePriority.UseFont = false;
        this.xrTableCell48.Text = "Meta";
        this.xrTableCell48.Weight = 1.044479812108156D;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.Dpi = 254F;
        this.xrTableCell49.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.StylePriority.UseFont = false;
        this.xrTableCell49.Text = "Resultado";
        this.xrTableCell49.Weight = 1.1785271018856685D;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.Dpi = 254F;
        this.xrTableCell50.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.StylePriority.UseFont = false;
        this.xrTableCell50.Text = "Status";
        this.xrTableCell50.Weight = 0.77699308600617556D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Font = new System.Drawing.Font("Verdana", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "Dezembro / 2012";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell19.Weight = 0.2678678130718859D;
        // 
        // xrTable10
        // 
        this.xrTable10.Dpi = 254F;
        this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0.4006805F, 3.19707F);
        this.xrTable10.Name = "xrTable10";
        this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
        this.xrTable10.SizeF = new System.Drawing.SizeF(269.9047F, 22.71004F);
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell43,
            this.xrTableCell46,
            this.xrTableCell47});
        this.xrTableRow13.Dpi = 254F;
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1D;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.Dpi = 254F;
        this.xrTableCell43.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.StylePriority.UseFont = false;
        this.xrTableCell43.Text = "Meta";
        this.xrTableCell43.Weight = 1.044479812108156D;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.Dpi = 254F;
        this.xrTableCell46.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.StylePriority.UseFont = false;
        this.xrTableCell46.Text = "Resultado";
        this.xrTableCell46.Weight = 1.1785271018856685D;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.Dpi = 254F;
        this.xrTableCell47.Font = new System.Drawing.Font("Arial Narrow", 7F, System.Drawing.FontStyle.Bold);
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.StylePriority.UseFont = false;
        this.xrTableCell47.Text = "Status";
        this.xrTableCell47.Weight = 0.77699308600617556D;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgCabecalhoPagina});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 399.5208F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint_1);
        // 
        // imgCabecalhoPagina
        // 
        this.imgCabecalhoPagina.Dpi = 254F;
        this.imgCabecalhoPagina.Image = ((System.Drawing.Image)(resources.GetObject("imgCabecalhoPagina.Image")));
        this.imgCabecalhoPagina.LocationFloat = new DevExpress.Utils.PointFloat(7.9375F, 0F);
        this.imgCabecalhoPagina.Name = "imgCabecalhoPagina";
        this.imgCabecalhoPagina.SizeF = new System.Drawing.SizeF(2950.104F, 399.5208F);
        this.imgCabecalhoPagina.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo3,
            this.xrPictureBox3,
            this.xrPageInfo1,
            this.xrPictureBox2});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 180.0224F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageFooter_BeforePrint);
        // 
        // xrPageInfo3
        // 
        this.xrPageInfo3.Dpi = 254F;
        this.xrPageInfo3.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrPageInfo3.ForeColor = System.Drawing.Color.DimGray;
        this.xrPageInfo3.Format = "Data de emissão: {0:dd/MM/yyyy HH:mm:ss}";
        this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(2190.938F, 0F);
        this.xrPageInfo3.Name = "xrPageInfo3";
        this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo3.SizeF = new System.Drawing.SizeF(703.7916F, 33.41979F);
        this.xrPageInfo3.StylePriority.UseFont = false;
        this.xrPageInfo3.StylePriority.UseForeColor = false;
        this.xrPageInfo3.StylePriority.UseTextAlignment = false;
        this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.Dpi = 254F;
        this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(2013.472F, 42F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(880.453F, 60.85417F);
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.Format = "|{0}|";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(2.645995F, 102.9566F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.Number;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(2960.687F, 58.42006F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Dpi = 254F;
        this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(144.521F, 41.99997F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(1868.426F, 60.85416F);
        // 
        // xrControlStyle2
        // 
        this.xrControlStyle2.Name = "xrControlStyle2";
        this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // rel_SubResultadosMesAMes
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter,
            this.DetailReport,
            this.DetailReport1,
            this.PageHeader,
            this.PageFooter});
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 45);
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CodProjeto});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 10F;
        this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.SnapToGrid;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2});
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.dsResultadosMesAMes1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbResultados2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        object x = "";
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));


        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);


        lblDescricaoRelatorio.Text = String.Format("Painel de Indicadores dos Processos redesenhados da unidade: {0} - Período de Apuração: {1}  a {2}", nomeUnidade, "Janeiro/" + anoGlobal, "Dezembro/" + anoGlobal);


        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoUnidadeGlobal, x);
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            //xrPictureBox1.Image = logo;
        }
    }

    private void celStatusJaneiro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusJaneiro);
    }

    private void modificaStatus(XRTableCell celula)
    {
        //J sorriso fonte vcerde ou azul
        //L tris fonte vermelha
        //K neutro fonte amarela

        if (celula.Text == "Branco")
        {
            celula.Text = "l";
            celula.ForeColor = Color.White;
        }
        else if (celula.Text == "Azul")
        {
            celula.Text = "l";
            celula.ForeColor = Color.Blue;
        }
        else if (celula.Text == "Verde")
        {
            celula.Text = "l";
            celula.ForeColor = Color.Green;
        }
        else if (celula.Text == "Vermelho")
        {
            celula.Text = "l";
            celula.ForeColor = Color.Red;
        }
        else if (celula.Text == "Amarelo")
        {
            celula.Text = "l";
            celula.ForeColor = Color.White;
        }
        else if (celula.Text == "Laranja")
        {
            celula.Text = "l";
            celula.ForeColor = Color.Orange;
        }
    }

    private void celStatusFevereiro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusFevereiro);
    }

    private void celStatusMarco_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusMarco);
    }

    private void celStatusAbril_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusAbril);
    }

    private void celStatusMaio_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusMaio);
    }

    private void celStatusJunho_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusJunho);
    }

    private void celStatusJulho_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusJulho);
    }

    private void celStatusAgosto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusAgosto);
    }

    private void celStatusSetembro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusSetembro);
    }

    private void celStatusOutubro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusOutubro);
    }

    private void celStatusNovembro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusNovembro);
    }

    private void celStatusDezembro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        modificaStatus(celStatusDezembro);
    }


    private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        celCabecalhoJulho.Text = "Julho/" + anoGlobal.ToString();
        celCabecalhoAgosto.Text = "Agosto/" + anoGlobal.ToString();
        celCabecalhoSetembro.Text = "Setembro/" + anoGlobal.ToString();
        celCabecalhoOutubro.Text = "Outubro/" + anoGlobal.ToString();
        celCabecalhoNovembro.Text = "Novembro/" + anoGlobal.ToString();
        celCabecalhoDezembro.Text = "Dezembro/" + anoGlobal.ToString();
    }

    private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        celCabecalhoJaneiro.Text = "Janeiro/" + anoGlobal.ToString();
        celCabecalhoFevereiro.Text = "Fevereiro/" + anoGlobal.ToString();
        celCabecalhoMarco.Text = "Março/" + anoGlobal.ToString();
        celCabecalhoAbril.Text = "Abril/" + anoGlobal.ToString();
        celCabecalhoMaio.Text = "Maio/" + anoGlobal.ToString();
        celCabecalhoJunho.Text = "Junho/" + anoGlobal.ToString();
    }

    private string getMesReferencia()
    {
        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1 || mes == 2)
        {
            mesporextenso = "Jan/Fev";
        }
        else if (mes == 3 || mes == 4)
        {
            mesporextenso = "Mar/Abr";
        }
        else if (mes == 5 || mes == 6)
        {
            mesporextenso = "Mai/Jun";
        }
        else if (mes == 7 || mes == 8)
        {
            mesporextenso = "Jul/Ago";
        }
        else if (mes == 9 || mes == 10)
        {
            mesporextenso = "Set/Out";
        }
        else if (mes == 11 || mes == 12)
        {
            mesporextenso = "Nov/Dez";
        }
        mesporextenso += " - " + ano;
        return mesporextenso;

    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }



        object x = "";
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoUnidadeGlobal, x);
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            //xrPictureBox1.Image = logo;
        }
        //lblMesReferencia.Text = "MÊS DE REFERÊNCIA: " + getMesReferencia();

    }

    private void PageHeader_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        InsereCamposDinamicosNaCapa();
    }

    private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        /*DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
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

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataSet ds = cDados.getUnidadeNegocio(" and un.CodigoUnidadeNegocio = " + codigoUnidadeGlobal);
        string nomeUnidade = "";
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        object x = "";
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));


        string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);


        lblDescricaoRelatorio.Text = String.Format("Painel de Indicadores dos Processos redesenhados da unidade: {0} - Período de Apuração: {1}  a {2}", nomeUnidade, "Janeiro/" + anoGlobal, "Dezembro/" + anoGlobal);


        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoUnidadeGlobal, x);
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            //xrPictureBox1.Image = logo;
        }
    }

    private void xrTableCell54_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void celulaProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrTableCell55_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void xrTableCell56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

}
