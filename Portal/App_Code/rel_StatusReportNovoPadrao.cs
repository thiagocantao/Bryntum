using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

/// <summary>
/// Summary description for rel_StatusReportNovoPadrao
/// </summary>
public class rel_StatusReportNovoPadrao : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoStatusReport;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLine xrLine1;
    private XRLabel xrLabel3;
    private DsBoletimStatusNovoPadrao dsBoletimStatusNovoPadrao;
    private DetailReportBand DetailReportListaProjetos;
    private DetailBand Detail1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell7;
    private XRPictureBox xrPictureBox1;
    private DetailReportBand DetailReportDetalhesProjetos;
    private DetailBand Detail2;
    private DetailReportBand DetailReportAnaliseGeral;
    private DetailBand Detail3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel26;
    private XRLabel xrLabel25;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel xrLabel21;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private DetailReportBand DetailReportTarefasConcluidas;
    private DetailBand Detail4;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel27;
    private XRTable xrTable3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTable xrTable4;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private DetailReportBand DetailReportTarefasAtrasadas;
    private DetailBand Detail5;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel28;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private DetailReportBand DetailReportPendentes;
    private DetailBand Detail6;
    private GroupHeaderBand GroupHeader4;
    private DetailReportBand DetailReportMarcos;
    private DetailBand Detail7;
    private XRTable xrTable7;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell33;
    private XRPictureBox xrPictureBox2;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private GroupHeaderBand GroupHeader5;
    private XRLabel xrLabel29;
    private XRTable xrTable14;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private XRLabel xrLabel30;
    private XRTable xrTable8;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTable xrTable9;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private DetailReportBand DetailReportDespesa;
    private DetailBand Detail8;
    private DetailReportBand DetailReportReceita;
    private DetailBand Detail9;
    private GroupHeaderBand GroupHeader6;
    private XRLabel xrLabel31;
    private XRTable xrTable11;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell184;
    private XRTableCell xrTableCell53;
    private XRTable xrTable10;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private GroupHeaderBand GroupHeader7;
    private XRTable xrTable12;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell71;
    private XRTable xrTable13;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell77;
    private GroupFooterBand GroupFooter2;
    private XRTable xrTable16;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell84;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell86;
    private XRTableCell xrTableCell87;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell89;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable15;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell78;
    private XRTableCell xrTableCell79;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell83;
    private DetailReportBand DetailReportRiscos;
    private DetailBand Detail10;
    private DetailReportBand DetailReportMetas;
    private DetailBand Detail11;
    private XRTable xrTable17;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell91;
    private XRTableCell xrTableCell92;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell94;
    private XRTableCell xrTableCell95;
    private XRTable xrTable31;
    private XRTableRow xrTableRow40;
    private XRTableCell xrTableCell149;
    private XRTableCell xrTableCell150;
    private XRTableCell xrTableCell151;
    private XRTableCell xrTableCell152;
    private XRTableCell xrTableCell153;
    private XRTableCell xrTableCell154;
    private XRTable xrTable18;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell100;
    private XRTableCell xrTableCell101;
    private XRLabel xrLabel34;
    private XRTable xrTable19;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell102;
    private XRTableCell xrTableCell103;
    private XRTableCell xrTableCell104;
    private XRTableCell xrTableCell105;
    private XRTableCell xrTableCell106;
    private XRTableCell xrTableCell107;
    private GroupHeaderBand GroupHeader10;
    private DetailReportBand DetailReportToDoList;
    private DetailBand Detail13;
    private GroupHeaderBand GroupHeader11;
    private DetailReportBand DetailReportContratos;
    private DetailBand Detail14;
    private XRTable xrTable24;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTableCell120;
    private XRTableCell xrTableCell121;
    private XRTableCell xrTableCell122;
    private XRTableCell xrTableCell123;
    private XRTableCell xrTableCell124;
    private XRTableCell xrTableCell125;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRTable xrTable23;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell130;
    private XRTableCell xrTableCell131;
    private XRTableCell xrTableCell132;
    private XRTableCell xrTableCell133;
    private XRTableCell xrTableCell134;
    private XRTable xrTable21;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell119;
    private XRTableCell xrTableCell126;
    private XRTableCell xrTableCell127;
    private XRTableCell xrTableCell128;
    private XRTableCell xrTableCell129;
    private XRPictureBox xrPictureBox3;
    private CalculatedField cfTituloRelatorio;
    private DetailReportBand DetailReportQuestoes;
    private DetailBand Detail12;
    private GroupHeaderBand GroupHeader8;
    private GroupHeaderBand GroupHeader9;
    private XRTable xrTable25;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell136;
    private XRTableCell xrTableCell137;
    private XRTableCell xrTableCell138;
    private XRTableCell xrTableCell139;
    private XRTableCell xrTableCell140;
    private XRTableCell xrTableCell141;
    private XRLabel xrLabel37;
    private XRTable xrTable26;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell142;
    private XRTableCell xrTableCell143;
    private XRTableCell xrTableCell144;
    private XRTableCell xrTableCell145;
    private XRTableCell xrTableCell146;
    private XRTableCell xrTableCell147;
    private XRLabel xrLabel38;
    private XRTable xrTable27;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell148;
    private XRTableCell xrTableCell155;
    private XRTableCell xrTableCell156;
    private XRTableCell xrTableCell157;
    private XRTableCell xrTableCell158;
    private XRTableCell xrTableCell159;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private GroupFooterBand GroupFooter3;
    private XRLabel xrLabel41;
    private XRLabel xrLabel40;
    private GroupFooterBand GroupFooter4;
    private XRLabel xrLabel43;
    private XRLabel xrLabel42;
    private GroupFooterBand GroupFooter5;
    private XRLabel xrLabel45;
    private XRLabel xrLabel44;
    private GroupFooterBand GroupFooter6;
    private XRLabel xrLabel47;
    private XRLabel xrLabel46;
    private GroupFooterBand GroupFooter7;
    private XRLabel xrLabel49;
    private XRLabel xrLabel48;
    private GroupFooterBand GroupFooter9;
    private XRLabel xrLabel52;
    private XRLabel xrLabel53;
    private GroupFooterBand GroupFooter10;
    private XRLabel xrLabel54;
    private XRLabel xrLabel55;
    private GroupFooterBand GroupFooter8;
    private XRLabel xrLabel51;
    private XRLabel xrLabel50;
    private GroupFooterBand GroupFooter13;
    private XRLabel xrLabel60;
    private XRLabel xrLabel61;
    private GroupFooterBand GroupFooter14;
    private XRLabel xrLabel62;
    private XRLabel xrLabel63;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBox5;
    private XRRichText xrRichText1;
    private XRRichText xrRichText2;
    private XRLine xrLine2;
    private XRLabel xrLabel32;
    private XRLabel xrLabel5;
    private XRTableCell xrTableCell70;
    dados cDados;

    private readonly bool indicaIdiomaPortugues;
    private XRTable xrTable22;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell114;
    private XRTableCell xrTableCell115;
    private XRTableCell xrTableCell116;
    private XRTableCell xrTableCell117;
    private XRTableCell xrTableCell118;
    private XRTableCell xrTableCell135;
    private GroupHeaderBand GroupHeader12;
    private XRTable xrTable20;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell108;
    private XRTableCell xrTableCell109;
    private XRTableCell xrTableCell110;
    private XRTableCell xrTableCell111;
    private XRTableCell xrTableCell112;
    private XRTableCell xrTableCell113;
    private XRLabel xrLabel33;
    private GroupFooterBand GroupFooter11;
    private XRLabel xrLabel56;
    private XRLabel xrLabel57;
    private DetailReportBand DetailReport;
    private DetailBand Detail15;
    private XRTable xrTable29;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell162;
    private XRPictureBox xrPictureBox4;
    private XRTableCell xrTableCell166;
    private XRTableCell xrTableCell167;
    private XRTableCell xrTableCell168;
    private XRTableCell xrTableCell169;
    private GroupHeaderBand GroupHeader13;
    private XRLabel xrLabel39;
    private XRTable xrTable28;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell160;
    private XRTableCell xrTableCell161;
    private XRTableCell xrTableCell163;
    private XRTableCell xrTableCell164;
    private XRTableCell xrTableCell165;
    private GroupFooterBand GroupFooter12;
    private XRLabel xrLabel58;
    private XRLabel xrLabel59;
    private XRTableCell xrTableCell171;
    private XRTableCell xrTableCell170;
    private XRTableCell xrTableCell173;
    private XRTableCell xrTableCell172;
    private XRTableCell xrTableCell175;
    private XRTableCell xrTableCell174;
    private readonly string dateFormatString;

    public rel_StatusReportNovoPadrao()
    {
        indicaIdiomaPortugues = System.Globalization.CultureInfo.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        dateFormatString = indicaIdiomaPortugues ? "{0:dd/MM/yyyy}" : "{0:MM/dd/yyyy}";
        InitializeComponent();

        cDados = CdadosUtil.GetCdados(null);
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
            string resourceFileName = "rel_StatusReportNovoPadrao.resx";
            System.Resources.ResourceManager resources = global::Resources.rel_StatusReportNovoPadrao.ResourceManager;
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.pCodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
            this.dsBoletimStatusNovoPadrao = new DsBoletimStatusNovoPadrao();
            this.DetailReportListaProjetos = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReportDetalhesProjetos = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrRichText2 = new DevExpress.XtraReports.UI.XRRichText();
            this.DetailReportTarefasConcluidas = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell171 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell170 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportTarefasAtrasadas = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell173 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell172 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter4 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportPendentes = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell175 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell174 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter5 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel44 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportMarcos = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter6 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportDespesa = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail8 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader7 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter13 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel60 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel61 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportReceita = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail9 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell184 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter14 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel62 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportRiscos = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail10 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader9 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable25 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell136 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell137 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell138 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell139 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell140 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell141 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter7 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportMetas = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail11 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable19 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader10 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable24 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell120 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell122 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell123 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell125 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter9 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportToDoList = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail13 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable23 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell130 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell131 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell132 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell133 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell134 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader11 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable21 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell119 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell126 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell127 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell128 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell129 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter10 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportContratos = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail14 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable22 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell114 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell115 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell117 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell135 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader12 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable20 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell108 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell113 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter11 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportQuestoes = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail12 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable27 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell148 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell155 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell156 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell157 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell158 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell159 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader8 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable26 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell142 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell143 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell144 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell145 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell146 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell147 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter8 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail15 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable29 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell162 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTableCell166 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell167 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell168 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell169 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader13 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable28 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell160 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell161 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell163 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell164 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell165 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter12 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReportAnaliseGeral = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable31 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow40 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell149 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell150 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell151 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell152 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell153 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell154 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable18 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cfTituloRelatorio = new DevExpress.XtraReports.UI.CalculatedField();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPictureBox5 = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatusNovoPadrao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrLine1,
            this.xrLabel3});
            this.Detail.Dpi = 254F;
            this.Detail.DrillDownExpanded = false;
            this.Detail.HeightF = 200F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.AfterPrint += new System.EventHandler(this.Detail_AfterPrint);
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Verdana", 12F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Relatório de Status";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[cfTituloRelatorio]")});
            this.xrLabel2.Font = new System.Drawing.Font("Verdana", 12F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 125F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(1900F, 5F);
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[dataGeracaoStatusReport]")});
            this.xrLabel3.Font = new System.Drawing.Font("Verdana", 7F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1400F, 130F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(500F, 50F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "xrLabel3";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel3.TextFormatString = "Emissão: {0:dd/MMM/yyyy}";
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 61.62499F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.TopMargin.Visible = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 125F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.BottomMargin.Visible = false;
            // 
            // pCodigoStatusReport
            // 
            this.pCodigoStatusReport.Name = "pCodigoStatusReport";
            this.pCodigoStatusReport.Type = typeof(short);
            this.pCodigoStatusReport.ValueInfo = "0";
            this.pCodigoStatusReport.Visible = false;
            // 
            // dsBoletimStatusNovoPadrao
            // 
            this.dsBoletimStatusNovoPadrao.DataSetName = "DsBoletimStatusNovoPadrao";
            this.dsBoletimStatusNovoPadrao.EnforceConstraints = false;
            this.dsBoletimStatusNovoPadrao.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DetailReportListaProjetos
            // 
            this.DetailReportListaProjetos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1});
            this.DetailReportListaProjetos.DataMember = "StatusReport.StatusReport_Projeto";
            this.DetailReportListaProjetos.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportListaProjetos.Dpi = 254F;
            this.DetailReportListaProjetos.Level = 0;
            this.DetailReportListaProjetos.Name = "DetailReportListaProjetos";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 50F;
            this.Detail1.Name = "Detail1";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell9,
            this.xrTableCell10});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.Weight = 0.0789473664133172D;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(3.0001F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(43F, 45F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox1.StylePriority.UseBorderColor = false;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            this.xrPictureBox1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox1_BeforePrint);
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeProjeto]")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Weight = 1.57894740255255D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SiglaUnidadeNegocio]")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Weight = 0.378947376451994D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GerenteProjeto]")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Weight = 0.805263149863795D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PercentualFisicoConcluido]")});
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.TextFormatString = "{0:n0}";
            this.xrTableCell10.Weight = 0.157894704718339D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 50F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable1
            // 
            this.xrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrTable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell7});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.CanGrow = false;
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.Weight = 0.0789473664133172D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Projeto";
            this.xrTableCell1.Weight = 1.57894740255255D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.CanGrow = false;
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Unidade";
            this.xrTableCell2.Weight = 0.378947376451994D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.CanGrow = false;
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "Gerente";
            this.xrTableCell3.Weight = 0.805263149863795D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.CanGrow = false;
            this.xrTableCell7.Dpi = 254F;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "%";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.157894704718339D;
            // 
            // DetailReportDetalhesProjetos
            // 
            this.DetailReportDetalhesProjetos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.DetailReportTarefasConcluidas,
            this.DetailReportTarefasAtrasadas,
            this.DetailReportPendentes,
            this.DetailReportMarcos,
            this.DetailReportDespesa,
            this.DetailReportReceita,
            this.DetailReportRiscos,
            this.DetailReportMetas,
            this.DetailReportToDoList,
            this.DetailReportContratos,
            this.DetailReportQuestoes,
            this.DetailReport});
            this.DetailReportDetalhesProjetos.DataMember = "StatusReport.StatusReport_Projeto";
            this.DetailReportDetalhesProjetos.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportDetalhesProjetos.Dpi = 254F;
            this.DetailReportDetalhesProjetos.Level = 2;
            this.DetailReportDetalhesProjetos.Name = "DetailReportDetalhesProjetos";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrRichText2});
            this.Detail2.Dpi = 254F;
            this.Detail2.HeightF = 525F;
            this.Detail2.Name = "Detail2";
            // 
            // xrLine2
            // 
            this.xrLine2.Dpi = 254F;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(1900F, 5F);
            // 
            // xrLabel26
            // 
            this.xrLabel26.Dpi = 254F;
            this.xrLabel26.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ValorCustoRealizado]")});
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(1250F, 325F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(649.9999F, 50F);
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "xrLabel10";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabel26.TextFormatString = "{0:c2}";
            // 
            // xrLabel25
            // 
            this.xrLabel25.Dpi = 254F;
            this.xrLabel25.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ValorCustoPrevisto]")});
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(275F, 325F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(675F, 50F);
            this.xrLabel25.Text = "xrLabel10";
            this.xrLabel25.TextFormatString = "{0:c2}";
            // 
            // xrLabel24
            // 
            this.xrLabel24.Dpi = 254F;
            this.xrLabel24.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoReprogramado]")});
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(1250F, 249.9999F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(650.0001F, 49.99998F);
            this.xrLabel24.Text = "xrLabel10";
            this.xrLabel24.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // xrLabel23
            // 
            this.xrLabel23.Dpi = 254F;
            this.xrLabel23.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioReprogramado]")});
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(275F, 250.0001F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(675F, 50F);
            this.xrLabel23.Text = "xrLabel10";
            this.xrLabel23.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoLB]")});
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(1250F, 175F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(649.9999F, 50F);
            this.xrLabel22.Text = "xrLabel10";
            this.xrLabel22.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // xrLabel21
            // 
            this.xrLabel21.Dpi = 254F;
            this.xrLabel21.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioLB]")});
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(275F, 175F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(675F, 50F);
            this.xrLabel21.Text = "xrLabel10";
            this.xrLabel21.TextFormatString = "{0:dd/MM/yyyy}";
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 254F;
            this.xrLabel20.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GerenteProjeto]")});
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(1200F, 99.99996F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(700F, 50.00001F);
            this.xrLabel20.Text = "xrLabel10";
            this.xrLabel20.TextFormatString = "{0:n0}";
            // 
            // xrLabel19
            // 
            this.xrLabel19.Dpi = 254F;
            this.xrLabel19.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeUnidadeNegocio]")});
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(163.875F, 100F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(786.125F, 50F);
            this.xrLabel19.Text = "xrLabel10";
            this.xrLabel19.TextFormatString = "{0:n0}";
            // 
            // xrLabel17
            // 
            this.xrLabel17.Dpi = 254F;
            this.xrLabel17.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 400F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(1900F, 49.99997F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.Text = "Análise:";
            // 
            // xrLabel16
            // 
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 325F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(275F, 50F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.Text = "Custo Previsto:";
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 254F;
            this.xrLabel15.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(950F, 325F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(299.9999F, 50F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UsePadding = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Custo   Realizado:";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(949.9998F, 250.0001F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(300.0001F, 50.00002F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.Text = "Término Reprogr:";
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 249.9999F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(275F, 50F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.Text = "Início Reprogr:";
            // 
            // xrLabel12
            // 
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(950F, 175F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(300F, 50F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.Text = "Término Previsto:";
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 175F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(275F, 50F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "Início Previsto:";
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PercentualFisicoConcluido]")});
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(1825F, 25.00001F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(75F, 50F);
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "[StatusReport_Projeto.PercentualFisicoConcluido]";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel10.TextFormatString = "{0:n0}";
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(1575F, 25.00001F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(250F, 50F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "% Concluído:";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(950F, 100F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(250F, 50F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "Gerente:";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(163.875F, 50F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "Unidade:";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeProjeto]")});
            this.xrLabel6.Font = new System.Drawing.Font("Verdana", 12F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(1575F, 50F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "xrLabel6";
            this.xrLabel6.TextFormatString = "Projeto: {0}";
            // 
            // xrRichText2
            // 
            this.xrRichText2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrRichText2.Dpi = 254F;
            this.xrRichText2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Html", "[ComentarioGeral]")});
            this.xrRichText2.Font = new System.Drawing.Font("Verdana", 8F);
            this.xrRichText2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 450F);
            this.xrRichText2.Name = "xrRichText2";
            this.xrRichText2.SerializableRtfString = resources.GetString("xrRichText2.SerializableRtfString");
            this.xrRichText2.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrRichText2.StylePriority.UseBorders = false;
            this.xrRichText2.StylePriority.UseFont = false;
            this.xrRichText2.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrRichText_EvaluateBinding);
            // 
            // DetailReportTarefasConcluidas
            // 
            this.DetailReportTarefasConcluidas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupHeader2,
            this.GroupFooter3});
            this.DetailReportTarefasConcluidas.DataMember = "StatusReport.StatusReport_Projeto.Projeto_TarefaConcluida";
            this.DetailReportTarefasConcluidas.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportTarefasConcluidas.Dpi = 254F;
            this.DetailReportTarefasConcluidas.Level = 0;
            this.DetailReportTarefasConcluidas.Name = "DetailReportTarefasConcluidas";
            // 
            // Detail4
            // 
            this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.Detail4.Dpi = 254F;
            this.Detail4.HeightF = 50F;
            this.Detail4.Name = "Detail4";
            this.Detail4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Dpi = 254F;
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1904F, 50F);
            this.xrTable4.StylePriority.UseBackColor = false;
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UsePadding = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell171,
            this.xrTableCell22});
            this.xrTableRow3.Dpi = 254F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 11.5D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Dpi = 254F;
            this.xrTableCell17.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeTarefa]")});
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Weight = 0.370548270875319D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Dpi = 254F;
            this.xrTableCell18.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioPrevisto]")});
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell18.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell18.Weight = 0.123516090291773D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Dpi = 254F;
            this.xrTableCell19.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoPrevisto]")});
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell19.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell19.Weight = 0.123516090291773D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Dpi = 254F;
            this.xrTableCell20.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioReal]")});
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell20.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell20.Weight = 0.123516090291773D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Dpi = 254F;
            this.xrTableCell21.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoReal]")});
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell21.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell21.Weight = 0.123516090291773D;
            // 
            // xrTableCell171
            // 
            this.xrTableCell171.Dpi = 254F;
            this.xrTableCell171.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoReprogramado]")});
            this.xrTableCell171.Multiline = true;
            this.xrTableCell171.Name = "xrTableCell171";
            this.xrTableCell171.StylePriority.UseTextAlignment = false;
            this.xrTableCell171.Text = "xrTableCell171";
            this.xrTableCell171.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell171.Weight = 0.123516090291773D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Dpi = 254F;
            this.xrTableCell22.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StringAlocacaoRecursoTarefa]")});
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Weight = 0.308790236275712D;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel27,
            this.xrTable3});
            this.GroupHeader2.Dpi = 254F;
            this.GroupHeader2.HeightF = 150F;
            this.GroupHeader2.KeepTogether = true;
            this.GroupHeader2.Name = "GroupHeader2";
            this.GroupHeader2.RepeatEveryPage = true;
            this.GroupHeader2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrLabel27
            // 
            this.xrLabel27.Dpi = 254F;
            this.xrLabel27.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.Text = "Tarefas concluídas no período";
            // 
            // xrTable3
            // 
            this.xrTable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Dpi = 254F;
            this.xrTable3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1904F, 75F);
            this.xrTable3.StylePriority.UseBackColor = false;
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell170,
            this.xrTableCell16});
            this.xrTableRow4.Dpi = 254F;
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 11.5D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "Tarefa";
            this.xrTableCell11.Weight = 0.370548270875319D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Dpi = 254F;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Início Previsto";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell12.Weight = 0.123516090291773D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Dpi = 254F;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "Término Previsto";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell13.Weight = 0.123516090291773D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Dpi = 254F;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "Início Real";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell14.Weight = 0.123516090291773D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Dpi = 254F;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Término Real";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell15.Weight = 0.123516090291773D;
            // 
            // xrTableCell170
            // 
            this.xrTableCell170.Dpi = 254F;
            this.xrTableCell170.Multiline = true;
            this.xrTableCell170.Name = "xrTableCell170";
            this.xrTableCell170.StylePriority.UseTextAlignment = false;
            this.xrTableCell170.Text = "Término reprogr.";
            this.xrTableCell170.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell170.Weight = 0.123516090291773D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Dpi = 254F;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "Responsável";
            this.xrTableCell16.Weight = 0.308790236275712D;
            // 
            // GroupFooter3
            // 
            this.GroupFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel41,
            this.xrLabel40});
            this.GroupFooter3.Dpi = 254F;
            this.GroupFooter3.HeightF = 150F;
            this.GroupFooter3.KeepTogether = true;
            this.GroupFooter3.Name = "GroupFooter3";
            this.GroupFooter3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel41
            // 
            this.xrLabel41.Dpi = 254F;
            this.xrLabel41.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00002F);
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel41.StylePriority.UseFont = false;
            this.xrLabel41.StylePriority.UseForeColor = false;
            this.xrLabel41.StylePriority.UseTextAlignment = false;
            this.xrLabel41.Text = "Não existem tarefas concluídas para o projeto";
            this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Dpi = 254F;
            this.xrLabel40.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel40.StylePriority.UseFont = false;
            this.xrLabel40.Text = "Tarefas concluídas no período";
            // 
            // DetailReportTarefasAtrasadas
            // 
            this.DetailReportTarefasAtrasadas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.GroupHeader3,
            this.GroupFooter4});
            this.DetailReportTarefasAtrasadas.DataMember = "StatusReport.StatusReport_Projeto.Projeto_TarefaAtrasada";
            this.DetailReportTarefasAtrasadas.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportTarefasAtrasadas.Dpi = 254F;
            this.DetailReportTarefasAtrasadas.Level = 1;
            this.DetailReportTarefasAtrasadas.Name = "DetailReportTarefasAtrasadas";
            // 
            // Detail5
            // 
            this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
            this.Detail5.Dpi = 254F;
            this.Detail5.HeightF = 50F;
            this.Detail5.Name = "Detail5";
            this.Detail5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.Dpi = 254F;
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable6.SizeF = new System.Drawing.SizeF(1904F, 50F);
            this.xrTable6.StylePriority.UseBackColor = false;
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UsePadding = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell173,
            this.xrTableCell32});
            this.xrTableRow6.Dpi = 254F;
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 11.5D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Dpi = 254F;
            this.xrTableCell28.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeTarefa]")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Weight = 0.432306311838252D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Dpi = 254F;
            this.xrTableCell29.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PercentualReal]")});
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell29.Weight = 0.108076579227092D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Dpi = 254F;
            this.xrTableCell30.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioPrevisto]")});
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell30.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell30.Weight = 0.123516092987071D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Dpi = 254F;
            this.xrTableCell31.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoPrevisto]")});
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell31.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell31.Weight = 0.123516094420993D;
            // 
            // xrTableCell173
            // 
            this.xrTableCell173.Dpi = 254F;
            this.xrTableCell173.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoReprogramado]")});
            this.xrTableCell173.Multiline = true;
            this.xrTableCell173.Name = "xrTableCell173";
            this.xrTableCell173.StylePriority.UseTextAlignment = false;
            this.xrTableCell173.Text = "xrTableCell173";
            this.xrTableCell173.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell173.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell173.Weight = 0.123516094420993D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Dpi = 254F;
            this.xrTableCell32.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StringAlocacaoRecursoTarefa]")});
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Weight = 0.385987789844714D;
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel28,
            this.xrTable5});
            this.GroupHeader3.Dpi = 254F;
            this.GroupHeader3.HeightF = 150F;
            this.GroupHeader3.KeepTogether = true;
            this.GroupHeader3.Name = "GroupHeader3";
            this.GroupHeader3.RepeatEveryPage = true;
            this.GroupHeader3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrLabel28
            // 
            this.xrLabel28.Dpi = 254F;
            this.xrLabel28.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel28.Name = "xrLabel28";
            this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel28.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel28.StylePriority.UseFont = false;
            this.xrLabel28.Text = "Tarefas Atrasadas";
            // 
            // xrTable5
            // 
            this.xrTable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Dpi = 254F;
            this.xrTable5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1904F, 75F);
            this.xrTable5.StylePriority.UseBackColor = false;
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UsePadding = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell172,
            this.xrTableCell27});
            this.xrTableRow5.Dpi = 254F;
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 11.5D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Dpi = 254F;
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "Tarefa";
            this.xrTableCell23.Weight = 0.432306311838252D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Dpi = 254F;
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UsePadding = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.Text = "% Concl.";
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell24.Weight = 0.108076579227092D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Dpi = 254F;
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "Início Previsto";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell25.Weight = 0.123516092987071D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Dpi = 254F;
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.Text = "Término Previsto";
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell26.Weight = 0.123516094420993D;
            // 
            // xrTableCell172
            // 
            this.xrTableCell172.Dpi = 254F;
            this.xrTableCell172.Multiline = true;
            this.xrTableCell172.Name = "xrTableCell172";
            this.xrTableCell172.StylePriority.UseTextAlignment = false;
            this.xrTableCell172.Text = "Término reprogr.";
            this.xrTableCell172.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell172.Weight = 0.123516094420993D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Dpi = 254F;
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Text = "Responsável";
            this.xrTableCell27.Weight = 0.385987789844714D;
            // 
            // GroupFooter4
            // 
            this.GroupFooter4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel43,
            this.xrLabel42});
            this.GroupFooter4.Dpi = 254F;
            this.GroupFooter4.HeightF = 150F;
            this.GroupFooter4.KeepTogether = true;
            this.GroupFooter4.Name = "GroupFooter4";
            this.GroupFooter4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel43
            // 
            this.xrLabel43.Dpi = 254F;
            this.xrLabel43.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00002F);
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel43.StylePriority.UseFont = false;
            this.xrLabel43.StylePriority.UseForeColor = false;
            this.xrLabel43.StylePriority.UseTextAlignment = false;
            this.xrLabel43.Text = "Não existem tarefas atrasadas para o projeto";
            this.xrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Dpi = 254F;
            this.xrLabel42.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel42.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel42.StylePriority.UseFont = false;
            this.xrLabel42.Text = "Tarefas Atrasadas";
            // 
            // DetailReportPendentes
            // 
            this.DetailReportPendentes.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.GroupHeader4,
            this.GroupFooter5});
            this.DetailReportPendentes.DataMember = "StatusReport.StatusReport_Projeto.Projeto_TarefaPendente";
            this.DetailReportPendentes.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportPendentes.Dpi = 254F;
            this.DetailReportPendentes.Level = 2;
            this.DetailReportPendentes.Name = "DetailReportPendentes";
            // 
            // Detail6
            // 
            this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
            this.Detail6.Dpi = 254F;
            this.Detail6.HeightF = 50F;
            this.Detail6.Name = "Detail6";
            this.Detail6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable9
            // 
            this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable9.Dpi = 254F;
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable9.SizeF = new System.Drawing.SizeF(1904F, 50F);
            this.xrTable9.StylePriority.UseBackColor = false;
            this.xrTable9.StylePriority.UseBorders = false;
            this.xrTable9.StylePriority.UseFont = false;
            this.xrTable9.StylePriority.UsePadding = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell175,
            this.xrTableCell48});
            this.xrTableRow9.Dpi = 254F;
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 11.5D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Dpi = 254F;
            this.xrTableCell44.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeTarefa]")});
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.Weight = 0.432306311838252D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Dpi = 254F;
            this.xrTableCell45.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PercentualReal]")});
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell45.Weight = 0.108076579227092D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Dpi = 254F;
            this.xrTableCell46.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioPrevisto]")});
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell46.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell46.Weight = 0.123516092987071D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Dpi = 254F;
            this.xrTableCell47.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoPrevisto]")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell47.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell47.Weight = 0.123516094420993D;
            // 
            // xrTableCell175
            // 
            this.xrTableCell175.Dpi = 254F;
            this.xrTableCell175.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoReprogramado]")});
            this.xrTableCell175.Multiline = true;
            this.xrTableCell175.Name = "xrTableCell175";
            this.xrTableCell175.StylePriority.UseTextAlignment = false;
            this.xrTableCell175.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell175.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell175.Weight = 0.123516094420993D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Dpi = 254F;
            this.xrTableCell48.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StringAlocacaoRecursoTarefa]")});
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.Weight = 0.385987789844714D;
            // 
            // GroupHeader4
            // 
            this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel30,
            this.xrTable8});
            this.GroupHeader4.Dpi = 254F;
            this.GroupHeader4.HeightF = 150F;
            this.GroupHeader4.KeepTogether = true;
            this.GroupHeader4.Name = "GroupHeader4";
            this.GroupHeader4.RepeatEveryPage = true;
            this.GroupHeader4.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrLabel30
            // 
            this.xrLabel30.Dpi = 254F;
            this.xrLabel30.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.Text = "Tarefas para o Próximo Período";
            // 
            // xrTable8
            // 
            this.xrTable8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable8.Dpi = 254F;
            this.xrTable8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00002F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable8.SizeF = new System.Drawing.SizeF(1904F, 75F);
            this.xrTable8.StylePriority.UseBackColor = false;
            this.xrTable8.StylePriority.UseBorders = false;
            this.xrTable8.StylePriority.UseFont = false;
            this.xrTable8.StylePriority.UsePadding = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell174,
            this.xrTableCell43});
            this.xrTableRow8.Dpi = 254F;
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 11.5D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Dpi = 254F;
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Text = "Tarefa";
            this.xrTableCell39.Weight = 0.432306311838252D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Dpi = 254F;
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UsePadding = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            this.xrTableCell40.Text = "% Concl.";
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell40.Weight = 0.108076579227092D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Dpi = 254F;
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "Início Previsto";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell41.Weight = 0.123516092987071D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Dpi = 254F;
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = "Término Previsto";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell42.Weight = 0.123516094420993D;
            // 
            // xrTableCell174
            // 
            this.xrTableCell174.Dpi = 254F;
            this.xrTableCell174.Multiline = true;
            this.xrTableCell174.Name = "xrTableCell174";
            this.xrTableCell174.StylePriority.UseTextAlignment = false;
            this.xrTableCell174.Text = "Término reprogr.";
            this.xrTableCell174.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell174.Weight = 0.123516094420993D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Dpi = 254F;
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.Text = "Responsável";
            this.xrTableCell43.Weight = 0.385987789844714D;
            // 
            // GroupFooter5
            // 
            this.GroupFooter5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel45,
            this.xrLabel44});
            this.GroupFooter5.Dpi = 254F;
            this.GroupFooter5.HeightF = 150F;
            this.GroupFooter5.KeepTogether = true;
            this.GroupFooter5.Name = "GroupFooter5";
            this.GroupFooter5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel45
            // 
            this.xrLabel45.Dpi = 254F;
            this.xrLabel45.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00002F);
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel45.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel45.StylePriority.UseFont = false;
            this.xrLabel45.StylePriority.UseForeColor = false;
            this.xrLabel45.StylePriority.UseTextAlignment = false;
            this.xrLabel45.Text = "Não existem tarefas para o próximo período para o projeto";
            this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel44
            // 
            this.xrLabel44.Dpi = 254F;
            this.xrLabel44.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel44.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel44.Name = "xrLabel44";
            this.xrLabel44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel44.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel44.StylePriority.UseFont = false;
            this.xrLabel44.Text = "Tarefas para o Próximo Período";
            // 
            // DetailReportMarcos
            // 
            this.DetailReportMarcos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.GroupHeader5,
            this.GroupFooter6});
            this.DetailReportMarcos.DataMember = "StatusReport.StatusReport_Projeto.Projeto_Marco";
            this.DetailReportMarcos.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportMarcos.Dpi = 254F;
            this.DetailReportMarcos.Level = 3;
            this.DetailReportMarcos.Name = "DetailReportMarcos";
            // 
            // Detail7
            // 
            this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.Detail7.Dpi = 254F;
            this.Detail7.HeightF = 50F;
            this.Detail7.Name = "Detail7";
            this.Detail7.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable7
            // 
            this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable7.Dpi = 254F;
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable7.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable7.StylePriority.UseBackColor = false;
            this.xrTable7.StylePriority.UseBorders = false;
            this.xrTable7.StylePriority.UseFont = false;
            this.xrTable7.StylePriority.UsePadding = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38});
            this.xrTableRow7.Dpi = 254F;
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
            this.xrTableCell33.Dpi = 254F;
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Weight = 0.189473682614014D;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox2.Dpi = 254F;
            this.xrPictureBox2.KeepTogether = false;
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(47F, 47F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox2.StylePriority.UseBorderColor = false;
            this.xrPictureBox2.StylePriority.UseBorders = false;
            this.xrPictureBox2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox2_BeforePrint);
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Dpi = 254F;
            this.xrTableCell34.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Marco]")});
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Weight = 1.54736843400212D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Dpi = 254F;
            this.xrTableCell35.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoPrevisto]")});
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell35.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell35.Weight = 0.315789470845966D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Dpi = 254F;
            this.xrTableCell36.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoReprogramado]")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell36.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell36.Weight = 0.315789470845966D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Dpi = 254F;
            this.xrTableCell37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoReal]")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell37.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell37.Weight = 0.315789470845966D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Dpi = 254F;
            this.xrTableCell38.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Desvio]")});
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell38.Weight = 0.315789470845966D;
            // 
            // GroupHeader5
            // 
            this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel29,
            this.xrTable14});
            this.GroupHeader5.Dpi = 254F;
            this.GroupHeader5.HeightF = 150F;
            this.GroupHeader5.KeepTogether = true;
            this.GroupHeader5.Name = "GroupHeader5";
            this.GroupHeader5.RepeatEveryPage = true;
            this.GroupHeader5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 254F;
            this.xrLabel29.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.Text = "Marcos";
            // 
            // xrTable14
            // 
            this.xrTable14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable14.Dpi = 254F;
            this.xrTable14.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrTable14.Name = "xrTable14";
            this.xrTable14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
            this.xrTable14.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable14.StylePriority.UseBackColor = false;
            this.xrTable14.StylePriority.UseBorders = false;
            this.xrTable14.StylePriority.UseFont = false;
            this.xrTable14.StylePriority.UsePadding = false;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67,
            this.xrTableCell68});
            this.xrTableRow16.Dpi = 254F;
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.Dpi = 254F;
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.Text = "Status";
            this.xrTableCell63.Weight = 0.189473682614014D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.Dpi = 254F;
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.Text = "Marco";
            this.xrTableCell64.Weight = 1.54736843400212D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.Dpi = 254F;
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseTextAlignment = false;
            this.xrTableCell65.Text = "Término Previsto";
            this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell65.Weight = 0.315789470845966D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Dpi = 254F;
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "Término Reprogr.";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell66.Weight = 0.315789470845966D;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.Dpi = 254F;
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.StylePriority.UseTextAlignment = false;
            this.xrTableCell67.Text = "Término Real";
            this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell67.Weight = 0.315789470845966D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Dpi = 254F;
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseTextAlignment = false;
            this.xrTableCell68.Text = "Desvio (d)";
            this.xrTableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell68.Weight = 0.315789470845966D;
            // 
            // GroupFooter6
            // 
            this.GroupFooter6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel47,
            this.xrLabel46});
            this.GroupFooter6.Dpi = 254F;
            this.GroupFooter6.HeightF = 150F;
            this.GroupFooter6.KeepTogether = true;
            this.GroupFooter6.Name = "GroupFooter6";
            this.GroupFooter6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel47
            // 
            this.xrLabel47.Dpi = 254F;
            this.xrLabel47.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel47.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel47.StylePriority.UseFont = false;
            this.xrLabel47.StylePriority.UseForeColor = false;
            this.xrLabel47.StylePriority.UseTextAlignment = false;
            this.xrLabel47.Text = "Não existem marcos para o projeto";
            this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel46
            // 
            this.xrLabel46.Dpi = 254F;
            this.xrLabel46.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel46.Name = "xrLabel46";
            this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel46.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel46.StylePriority.UseFont = false;
            this.xrLabel46.Text = "Marcos";
            // 
            // DetailReportDespesa
            // 
            this.DetailReportDespesa.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail8,
            this.GroupHeader7,
            this.GroupFooter2,
            this.GroupFooter13});
            this.DetailReportDespesa.DataMember = "StatusReport.StatusReport_Projeto.Projeto_Despesa";
            this.DetailReportDespesa.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportDespesa.Dpi = 254F;
            this.DetailReportDespesa.Level = 4;
            this.DetailReportDespesa.Name = "DetailReportDespesa";
            this.DetailReportDespesa.Visible = false;
            // 
            // Detail8
            // 
            this.Detail8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
            this.Detail8.Dpi = 254F;
            this.Detail8.HeightF = 50F;
            this.Detail8.Name = "Detail8";
            this.Detail8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable13
            // 
            this.xrTable13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable13.Dpi = 254F;
            this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 0F);
            this.xrTable13.Name = "xrTable13";
            this.xrTable13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTable13.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable13.StylePriority.UseBackColor = false;
            this.xrTable13.StylePriority.UseBorders = false;
            this.xrTable13.StylePriority.UseFont = false;
            this.xrTable13.StylePriority.UsePadding = false;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell72,
            this.xrTableCell73,
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76,
            this.xrTableCell77});
            this.xrTableRow12.Dpi = 254F;
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 11.5D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Dpi = 254F;
            this.xrTableCell72.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemCusto]")});
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.Weight = 3.436763414147D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.Dpi = 254F;
            this.xrTableCell73.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustoPrevisto]")});
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseTextAlignment = false;
            this.xrTableCell73.Text = "xrTableCell73";
            this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell73.TextFormatString = "{0:n2}";
            this.xrTableCell73.Weight = 1.40219956973455D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.Dpi = 254F;
            this.xrTableCell74.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustoPrevistoAteData]")});
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.StylePriority.UseTextAlignment = false;
            this.xrTableCell74.Text = "xrTableCell74";
            this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell74.TextFormatString = "{0:n2}";
            this.xrTableCell74.Weight = 1.40219950107304D;
            // 
            // xrTableCell75
            // 
            this.xrTableCell75.Dpi = 254F;
            this.xrTableCell75.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustoReal]")});
            this.xrTableCell75.Name = "xrTableCell75";
            this.xrTableCell75.StylePriority.UseTextAlignment = false;
            this.xrTableCell75.Text = "xrTableCell75";
            this.xrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell75.TextFormatString = "{0:n2}";
            this.xrTableCell75.Weight = 1.40219950107304D;
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.Dpi = 254F;
            this.xrTableCell76.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustoRestante]")});
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.StylePriority.UseTextAlignment = false;
            this.xrTableCell76.Text = "xrTableCell76";
            this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell76.TextFormatString = "{0:n2}";
            this.xrTableCell76.Weight = 1.40219956204844D;
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.Dpi = 254F;
            this.xrTableCell77.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[VariacaoCusto]")});
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.StylePriority.UseTextAlignment = false;
            this.xrTableCell77.Text = "xrTableCell77";
            this.xrTableCell77.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell77.TextFormatString = "{0:n2}";
            this.xrTableCell77.Weight = 1.4021996459538D;
            // 
            // GroupHeader7
            // 
            this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12,
            this.xrLabel32});
            this.GroupHeader7.Dpi = 254F;
            this.GroupHeader7.HeightF = 150F;
            this.GroupHeader7.KeepTogether = true;
            this.GroupHeader7.Name = "GroupHeader7";
            this.GroupHeader7.RepeatEveryPage = true;
            this.GroupHeader7.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable12
            // 
            this.xrTable12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable12.Dpi = 254F;
            this.xrTable12.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.xrTable12.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable12.StylePriority.UseBackColor = false;
            this.xrTable12.StylePriority.UseBorders = false;
            this.xrTable12.StylePriority.UseFont = false;
            this.xrTable12.StylePriority.UsePadding = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell60,
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71});
            this.xrTableRow11.Dpi = 254F;
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 11.5D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.Dpi = 254F;
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.Text = "Item de Despesa";
            this.xrTableCell60.Weight = 3.436763414147D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.Dpi = 254F;
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseTextAlignment = false;
            this.xrTableCell61.Text = "Despesa Prevista TOTAL";
            this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell61.Weight = 1.40219956973455D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.Dpi = 254F;
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.StylePriority.UseTextAlignment = false;
            this.xrTableCell62.Text = "Despesa Prev. até a Data";
            this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell62.Weight = 1.40219950107304D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.Dpi = 254F;
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.StylePriority.UseTextAlignment = false;
            this.xrTableCell69.Text = "Despesa Realizada";
            this.xrTableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell69.Weight = 1.40219950107304D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Dpi = 254F;
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseTextAlignment = false;
            this.xrTableCell70.Text = "Despesa Restante";
            this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell70.Weight = 1.40219956204844D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Dpi = 254F;
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.StylePriority.UseTextAlignment = false;
            this.xrTableCell71.Text = "Desvio";
            this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell71.Weight = 1.4021996459538D;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Dpi = 254F;
            this.xrLabel32.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.Text = "Detalhes dos Itens de Despesa (Valores em R$)";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrTable16});
            this.GroupFooter2.Dpi = 254F;
            this.GroupFooter2.HeightF = 150F;
            this.GroupFooter2.Name = "GroupFooter2";
            this.GroupFooter2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Italic);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(2.0001F, 50.00002F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(1902F, 99.99998F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "*Desvio = Despesa Prevista até a Data - Despesa Realizada\r\n**Despesa Restante = D" +
    "espesa Prevista TOTAL - Despesa Realizada";
            // 
            // xrTable16
            // 
            this.xrTable16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable16.Dpi = 254F;
            this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable16.Name = "xrTable16";
            this.xrTable16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
            this.xrTable16.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable16.StylePriority.UseBackColor = false;
            this.xrTable16.StylePriority.UseBorders = false;
            this.xrTable16.StylePriority.UseFont = false;
            this.xrTable16.StylePriority.UsePadding = false;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell84,
            this.xrTableCell85,
            this.xrTableCell86,
            this.xrTableCell87,
            this.xrTableCell88,
            this.xrTableCell89});
            this.xrTableRow15.Dpi = 254F;
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 11.5D;
            // 
            // xrTableCell84
            // 
            this.xrTableCell84.Dpi = 254F;
            this.xrTableCell84.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell84.Name = "xrTableCell84";
            this.xrTableCell84.StylePriority.UseFont = false;
            this.xrTableCell84.StylePriority.UseTextAlignment = false;
            this.xrTableCell84.Text = "Total";
            this.xrTableCell84.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell84.Weight = 3.436763414147D;
            // 
            // xrTableCell85
            // 
            this.xrTableCell85.Dpi = 254F;
            this.xrTableCell85.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([CustoPrevisto])")});
            this.xrTableCell85.Name = "xrTableCell85";
            this.xrTableCell85.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell85.Summary = xrSummary1;
            this.xrTableCell85.Text = "xrTableCell79";
            this.xrTableCell85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell85.TextFormatString = "{0:n2}";
            this.xrTableCell85.Weight = 1.40219956973455D;
            // 
            // xrTableCell86
            // 
            this.xrTableCell86.Dpi = 254F;
            this.xrTableCell86.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([CustoPrevistoAteData])")});
            this.xrTableCell86.Name = "xrTableCell86";
            this.xrTableCell86.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell86.Summary = xrSummary2;
            this.xrTableCell86.Text = "xrTableCell80";
            this.xrTableCell86.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell86.TextFormatString = "{0:n2}";
            this.xrTableCell86.Weight = 1.40219950107304D;
            // 
            // xrTableCell87
            // 
            this.xrTableCell87.Dpi = 254F;
            this.xrTableCell87.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([CustoReal])")});
            this.xrTableCell87.Name = "xrTableCell87";
            this.xrTableCell87.StylePriority.UseTextAlignment = false;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell87.Summary = xrSummary3;
            this.xrTableCell87.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell87.TextFormatString = "{0:n2}";
            this.xrTableCell87.Weight = 1.40219950107304D;
            // 
            // xrTableCell88
            // 
            this.xrTableCell88.Dpi = 254F;
            this.xrTableCell88.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([CustoRestante])")});
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.StylePriority.UseTextAlignment = false;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell88.Summary = xrSummary4;
            this.xrTableCell88.Text = "xrTableCell82";
            this.xrTableCell88.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell88.TextFormatString = "{0:n2}";
            this.xrTableCell88.Weight = 1.40219956204844D;
            // 
            // xrTableCell89
            // 
            this.xrTableCell89.Dpi = 254F;
            this.xrTableCell89.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([VariacaoCusto])")});
            this.xrTableCell89.Name = "xrTableCell89";
            this.xrTableCell89.StylePriority.UseTextAlignment = false;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell89.Summary = xrSummary5;
            this.xrTableCell89.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell89.TextFormatString = "{0:n2}";
            this.xrTableCell89.Weight = 1.4021996459538D;
            // 
            // GroupFooter13
            // 
            this.GroupFooter13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel60,
            this.xrLabel61});
            this.GroupFooter13.Dpi = 254F;
            this.GroupFooter13.HeightF = 150F;
            this.GroupFooter13.KeepTogether = true;
            this.GroupFooter13.Level = 1;
            this.GroupFooter13.Name = "GroupFooter13";
            this.GroupFooter13.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel60
            // 
            this.xrLabel60.Dpi = 254F;
            this.xrLabel60.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel60.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 24.99993F);
            this.xrLabel60.Name = "xrLabel60";
            this.xrLabel60.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel60.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel60.StylePriority.UseFont = false;
            this.xrLabel60.Text = "Detalhes dos Itens de Despesa (Valores em R$)";
            // 
            // xrLabel61
            // 
            this.xrLabel61.Dpi = 254F;
            this.xrLabel61.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel61.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00002F);
            this.xrLabel61.Name = "xrLabel61";
            this.xrLabel61.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel61.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel61.StylePriority.UseFont = false;
            this.xrLabel61.StylePriority.UseForeColor = false;
            this.xrLabel61.StylePriority.UseTextAlignment = false;
            this.xrLabel61.Text = "Não existem detalhes de itens de despesa para o projeto";
            this.xrLabel61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // DetailReportReceita
            // 
            this.DetailReportReceita.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail9,
            this.GroupHeader6,
            this.GroupFooter1,
            this.GroupFooter14});
            this.DetailReportReceita.DataMember = "StatusReport.StatusReport_Projeto.Projeto_Receita";
            this.DetailReportReceita.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportReceita.Dpi = 254F;
            this.DetailReportReceita.Level = 5;
            this.DetailReportReceita.Name = "DetailReportReceita";
            this.DetailReportReceita.Visible = false;
            // 
            // Detail9
            // 
            this.Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
            this.Detail9.Dpi = 254F;
            this.Detail9.HeightF = 50F;
            this.Detail9.Name = "Detail9";
            this.Detail9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable10
            // 
            this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable10.Dpi = 254F;
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable10.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable10.StylePriority.UseBackColor = false;
            this.xrTable10.StylePriority.UseBorders = false;
            this.xrTable10.StylePriority.UseFont = false;
            this.xrTable10.StylePriority.UsePadding = false;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59});
            this.xrTableRow10.Dpi = 254F;
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 11.5D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Dpi = 254F;
            this.xrTableCell54.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ItemReceita]")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.Weight = 3.436763414147D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Dpi = 254F;
            this.xrTableCell55.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ReceitaPrevista]")});
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            this.xrTableCell55.Text = "xrTableCell55";
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell55.TextFormatString = "{0:n2}";
            this.xrTableCell55.Weight = 1.40219956973455D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Dpi = 254F;
            this.xrTableCell56.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ReceitaReal]")});
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.Text = "xrTableCell56";
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell56.TextFormatString = "{0:n2}";
            this.xrTableCell56.Weight = 1.40219950107304D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Dpi = 254F;
            this.xrTableCell57.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[VariacaoReceita]")});
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.Text = "xrTableCell57";
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell57.TextFormatString = "{0:n2}";
            this.xrTableCell57.Weight = 1.40219950107304D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Dpi = 254F;
            this.xrTableCell58.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ReceitaPrevistaAteData]")});
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            this.xrTableCell58.Text = "xrTableCell58";
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell58.TextFormatString = "{0:n2}";
            this.xrTableCell58.Weight = 1.40219956204844D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Dpi = 254F;
            this.xrTableCell59.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ReceitaRestante]")});
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell59.TextFormatString = "{0:n2}";
            this.xrTableCell59.Weight = 1.4021996459538D;
            // 
            // GroupHeader6
            // 
            this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel31,
            this.xrTable11});
            this.GroupHeader6.Dpi = 254F;
            this.GroupHeader6.HeightF = 150F;
            this.GroupHeader6.KeepTogether = true;
            this.GroupHeader6.Name = "GroupHeader6";
            this.GroupHeader6.RepeatEveryPage = true;
            this.GroupHeader6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrLabel31
            // 
            this.xrLabel31.Dpi = 254F;
            this.xrLabel31.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.Text = "Detalhes dos Itens de Receita (Valores em R$)";
            // 
            // xrTable11
            // 
            this.xrTable11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable11.Dpi = 254F;
            this.xrTable11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.xrTable11.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable11.StylePriority.UseBackColor = false;
            this.xrTable11.StylePriority.UseBorders = false;
            this.xrTable11.StylePriority.UseFont = false;
            this.xrTable11.StylePriority.UsePadding = false;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell184,
            this.xrTableCell53});
            this.xrTableRow13.Dpi = 254F;
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 11.5D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Dpi = 254F;
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Text = "Item de Receita";
            this.xrTableCell49.Weight = 3.436763414147D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Dpi = 254F;
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "Receita Prevista TOTAL";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell50.Weight = 1.40219956973455D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Dpi = 254F;
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = "Receita Realizada";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell51.Weight = 1.40219950107304D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Dpi = 254F;
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.Text = "Desvio";
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell52.Weight = 1.40219950107304D;
            // 
            // xrTableCell184
            // 
            this.xrTableCell184.Dpi = 254F;
            this.xrTableCell184.Name = "xrTableCell184";
            this.xrTableCell184.StylePriority.UseTextAlignment = false;
            this.xrTableCell184.Text = "Receita Prev. até a Data";
            this.xrTableCell184.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell184.Weight = 1.40219956204844D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Dpi = 254F;
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.StylePriority.UseTextAlignment = false;
            this.xrTableCell53.Text = "Receita Restante";
            this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell53.Weight = 1.4021996459538D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable15});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 50F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable15
            // 
            this.xrTable15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable15.Dpi = 254F;
            this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable15.Name = "xrTable15";
            this.xrTable15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
            this.xrTable15.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable15.StylePriority.UseBackColor = false;
            this.xrTable15.StylePriority.UseBorders = false;
            this.xrTable15.StylePriority.UseFont = false;
            this.xrTable15.StylePriority.UsePadding = false;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell78,
            this.xrTableCell79,
            this.xrTableCell80,
            this.xrTableCell81,
            this.xrTableCell82,
            this.xrTableCell83});
            this.xrTableRow14.Dpi = 254F;
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 11.5D;
            // 
            // xrTableCell78
            // 
            this.xrTableCell78.Dpi = 254F;
            this.xrTableCell78.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell78.Name = "xrTableCell78";
            this.xrTableCell78.StylePriority.UseFont = false;
            this.xrTableCell78.StylePriority.UseTextAlignment = false;
            this.xrTableCell78.Text = "Total";
            this.xrTableCell78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell78.Weight = 3.436763414147D;
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.Dpi = 254F;
            this.xrTableCell79.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ReceitaPrevista])")});
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.StylePriority.UseTextAlignment = false;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell79.Summary = xrSummary6;
            this.xrTableCell79.Text = "xrTableCell79";
            this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell79.TextFormatString = "{0:n2}";
            this.xrTableCell79.Weight = 1.40219956973455D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.Dpi = 254F;
            this.xrTableCell80.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ReceitaReal])")});
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseTextAlignment = false;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell80.Summary = xrSummary7;
            this.xrTableCell80.Text = "xrTableCell80";
            this.xrTableCell80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell80.TextFormatString = "{0:n2}";
            this.xrTableCell80.Weight = 1.40219950107304D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.Dpi = 254F;
            this.xrTableCell81.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([VariacaoReceita])")});
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.StylePriority.UseTextAlignment = false;
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell81.Summary = xrSummary8;
            this.xrTableCell81.Text = "xrTableCell81";
            this.xrTableCell81.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell81.TextFormatString = "{0:n2}";
            this.xrTableCell81.Weight = 1.40219950107304D;
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.Dpi = 254F;
            this.xrTableCell82.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ReceitaPrevistaAteData])")});
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.StylePriority.UseTextAlignment = false;
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell82.Summary = xrSummary9;
            this.xrTableCell82.Text = "xrTableCell82";
            this.xrTableCell82.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell82.TextFormatString = "{0:n2}";
            this.xrTableCell82.Weight = 1.40219956204844D;
            // 
            // xrTableCell83
            // 
            this.xrTableCell83.Dpi = 254F;
            this.xrTableCell83.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ReceitaRestante])")});
            this.xrTableCell83.Name = "xrTableCell83";
            this.xrTableCell83.StylePriority.UseTextAlignment = false;
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell83.Summary = xrSummary10;
            this.xrTableCell83.Text = "xrTableCell83";
            this.xrTableCell83.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell83.TextFormatString = "{0:n2}";
            this.xrTableCell83.Weight = 1.4021996459538D;
            // 
            // GroupFooter14
            // 
            this.GroupFooter14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel62,
            this.xrLabel63});
            this.GroupFooter14.Dpi = 254F;
            this.GroupFooter14.HeightF = 150F;
            this.GroupFooter14.KeepTogether = true;
            this.GroupFooter14.Level = 1;
            this.GroupFooter14.Name = "GroupFooter14";
            this.GroupFooter14.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel62
            // 
            this.xrLabel62.Dpi = 254F;
            this.xrLabel62.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel62.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrLabel62.Name = "xrLabel62";
            this.xrLabel62.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel62.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel62.StylePriority.UseFont = false;
            this.xrLabel62.StylePriority.UseForeColor = false;
            this.xrLabel62.StylePriority.UseTextAlignment = false;
            this.xrLabel62.Text = "Não existem detalhes de itens de receita para o projeto";
            this.xrLabel62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel63
            // 
            this.xrLabel63.Dpi = 254F;
            this.xrLabel63.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel63.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 24.99985F);
            this.xrLabel63.Name = "xrLabel63";
            this.xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel63.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel63.StylePriority.UseFont = false;
            this.xrLabel63.Text = "Detalhes dos Itens de Receita (Valores em R$)";
            // 
            // DetailReportRiscos
            // 
            this.DetailReportRiscos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail10,
            this.GroupHeader9,
            this.GroupFooter7});
            this.DetailReportRiscos.DataMember = "StatusReport.StatusReport_Projeto.Projeto_Risco";
            this.DetailReportRiscos.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportRiscos.Dpi = 254F;
            this.DetailReportRiscos.Level = 6;
            this.DetailReportRiscos.Name = "DetailReportRiscos";
            // 
            // Detail10
            // 
            this.Detail10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable17});
            this.Detail10.Dpi = 254F;
            this.Detail10.HeightF = 50F;
            this.Detail10.Name = "Detail10";
            this.Detail10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable17
            // 
            this.xrTable17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable17.Dpi = 254F;
            this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable17.Name = "xrTable17";
            this.xrTable17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
            this.xrTable17.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable17.StylePriority.UseBackColor = false;
            this.xrTable17.StylePriority.UseBorders = false;
            this.xrTable17.StylePriority.UseFont = false;
            this.xrTable17.StylePriority.UsePadding = false;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell90,
            this.xrTableCell91,
            this.xrTableCell92,
            this.xrTableCell93,
            this.xrTableCell94,
            this.xrTableCell95});
            this.xrTableRow17.Dpi = 254F;
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 11.5D;
            // 
            // xrTableCell90
            // 
            this.xrTableCell90.Dpi = 254F;
            this.xrTableCell90.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Status]")});
            this.xrTableCell90.Name = "xrTableCell90";
            this.xrTableCell90.Weight = 0.95931542107109868D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.Dpi = 254F;
            this.xrTableCell91.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Risco]")});
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.Weight = 3.7146830557829129D;
            // 
            // xrTableCell92
            // 
            this.xrTableCell92.Dpi = 254F;
            this.xrTableCell92.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Probabilidade]")});
            this.xrTableCell92.Name = "xrTableCell92";
            this.xrTableCell92.StylePriority.UseTextAlignment = false;
            this.xrTableCell92.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell92.Weight = 1.37470539266769D;
            // 
            // xrTableCell93
            // 
            this.xrTableCell93.Dpi = 254F;
            this.xrTableCell93.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Impacto]")});
            this.xrTableCell93.Name = "xrTableCell93";
            this.xrTableCell93.StylePriority.UseTextAlignment = false;
            this.xrTableCell93.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell93.Weight = 1.37470539266769D;
            // 
            // xrTableCell94
            // 
            this.xrTableCell94.Dpi = 254F;
            this.xrTableCell94.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LimiteEliminacao]")});
            this.xrTableCell94.Name = "xrTableCell94";
            this.xrTableCell94.StylePriority.UseTextAlignment = false;
            this.xrTableCell94.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell94.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell94.Weight = 1.09976431108539D;
            // 
            // xrTableCell95
            // 
            this.xrTableCell95.Dpi = 254F;
            this.xrTableCell95.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Responsavel]")});
            this.xrTableCell95.Name = "xrTableCell95";
            this.xrTableCell95.StylePriority.UseTextAlignment = false;
            this.xrTableCell95.Weight = 1.92458762075507D;
            // 
            // GroupHeader9
            // 
            this.GroupHeader9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable25,
            this.xrLabel37});
            this.GroupHeader9.Dpi = 254F;
            this.GroupHeader9.HeightF = 150F;
            this.GroupHeader9.KeepTogether = true;
            this.GroupHeader9.Name = "GroupHeader9";
            this.GroupHeader9.RepeatEveryPage = true;
            this.GroupHeader9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable25
            // 
            this.xrTable25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable25.Dpi = 254F;
            this.xrTable25.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable25.Name = "xrTable25";
            this.xrTable25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable25.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23});
            this.xrTable25.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable25.StylePriority.UseBackColor = false;
            this.xrTable25.StylePriority.UseBorders = false;
            this.xrTable25.StylePriority.UseFont = false;
            this.xrTable25.StylePriority.UsePadding = false;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell136,
            this.xrTableCell137,
            this.xrTableCell138,
            this.xrTableCell139,
            this.xrTableCell140,
            this.xrTableCell141});
            this.xrTableRow23.Dpi = 254F;
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 11.5D;
            // 
            // xrTableCell136
            // 
            this.xrTableCell136.Dpi = 254F;
            this.xrTableCell136.Name = "xrTableCell136";
            this.xrTableCell136.Text = "Status";
            this.xrTableCell136.Weight = 0.9593153371657378D;
            // 
            // xrTableCell137
            // 
            this.xrTableCell137.Dpi = 254F;
            this.xrTableCell137.Name = "xrTableCell137";
            this.xrTableCell137.Text = "Risco";
            this.xrTableCell137.Weight = 3.7146838109311529D;
            // 
            // xrTableCell138
            // 
            this.xrTableCell138.Dpi = 254F;
            this.xrTableCell138.Name = "xrTableCell138";
            this.xrTableCell138.StylePriority.UseTextAlignment = false;
            this.xrTableCell138.Text = "Probabilidade";
            this.xrTableCell138.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell138.Weight = 1.37470472142481D;
            // 
            // xrTableCell139
            // 
            this.xrTableCell139.Dpi = 254F;
            this.xrTableCell139.Name = "xrTableCell139";
            this.xrTableCell139.StylePriority.UseTextAlignment = false;
            this.xrTableCell139.Text = "Impacto";
            this.xrTableCell139.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell139.Weight = 1.37470539266769D;
            // 
            // xrTableCell140
            // 
            this.xrTableCell140.Dpi = 254F;
            this.xrTableCell140.Name = "xrTableCell140";
            this.xrTableCell140.StylePriority.UseTextAlignment = false;
            this.xrTableCell140.Text = "Limite Eliminação";
            this.xrTableCell140.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell140.Weight = 1.09976431108539D;
            // 
            // xrTableCell141
            // 
            this.xrTableCell141.Dpi = 254F;
            this.xrTableCell141.Name = "xrTableCell141";
            this.xrTableCell141.StylePriority.UseTextAlignment = false;
            this.xrTableCell141.Text = "Responsável";
            this.xrTableCell141.Weight = 1.92458762075507D;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Dpi = 254F;
            this.xrLabel37.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel37.StylePriority.UseFont = false;
            this.xrLabel37.Text = "Riscos";
            // 
            // GroupFooter7
            // 
            this.GroupFooter7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel49,
            this.xrLabel48});
            this.GroupFooter7.Dpi = 254F;
            this.GroupFooter7.HeightF = 150F;
            this.GroupFooter7.KeepTogether = true;
            this.GroupFooter7.Name = "GroupFooter7";
            this.GroupFooter7.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel49
            // 
            this.xrLabel49.Dpi = 254F;
            this.xrLabel49.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel49.StylePriority.UseFont = false;
            this.xrLabel49.StylePriority.UseForeColor = false;
            this.xrLabel49.StylePriority.UseTextAlignment = false;
            this.xrLabel49.Text = "Não existem riscos para o projeto";
            this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel48
            // 
            this.xrLabel48.Dpi = 254F;
            this.xrLabel48.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel48.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel48.StylePriority.UseFont = false;
            this.xrLabel48.Text = "Riscos";
            // 
            // DetailReportMetas
            // 
            this.DetailReportMetas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail11,
            this.GroupHeader10,
            this.GroupFooter9});
            this.DetailReportMetas.DataMember = "StatusReport.StatusReport_Projeto.Projeto_Meta";
            this.DetailReportMetas.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportMetas.Dpi = 254F;
            this.DetailReportMetas.Level = 8;
            this.DetailReportMetas.Name = "DetailReportMetas";
            // 
            // Detail11
            // 
            this.Detail11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable19});
            this.Detail11.Dpi = 254F;
            this.Detail11.HeightF = 55F;
            this.Detail11.Name = "Detail11";
            this.Detail11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable19
            // 
            this.xrTable19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable19.Dpi = 254F;
            this.xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable19.Name = "xrTable19";
            this.xrTable19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19});
            this.xrTable19.SizeF = new System.Drawing.SizeF(1900F, 55F);
            this.xrTable19.StylePriority.UseBackColor = false;
            this.xrTable19.StylePriority.UseBorders = false;
            this.xrTable19.StylePriority.UseFont = false;
            this.xrTable19.StylePriority.UsePadding = false;
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell102,
            this.xrTableCell103,
            this.xrTableCell104,
            this.xrTableCell105,
            this.xrTableCell106,
            this.xrTableCell107});
            this.xrTableRow19.Dpi = 254F;
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 11.5D;
            // 
            // xrTableCell102
            // 
            this.xrTableCell102.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox3});
            this.xrTableCell102.Dpi = 254F;
            this.xrTableCell102.Name = "xrTableCell102";
            this.xrTableCell102.Weight = 0.659858617180651D;
            // 
            // xrPictureBox3
            // 
            this.xrPictureBox3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox3.Dpi = 254F;
            this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            this.xrPictureBox3.Name = "xrPictureBox3";
            this.xrPictureBox3.SizeF = new System.Drawing.SizeF(50F, 50F);
            this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox3.StylePriority.UseBorderColor = false;
            this.xrPictureBox3.StylePriority.UseBorders = false;
            this.xrPictureBox3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox3_BeforePrint);
            // 
            // xrTableCell103
            // 
            this.xrTableCell103.Dpi = 254F;
            this.xrTableCell103.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Meta]")});
            this.xrTableCell103.Name = "xrTableCell103";
            this.xrTableCell103.Weight = 3.84917520924131D;
            // 
            // xrTableCell104
            // 
            this.xrTableCell104.Dpi = 254F;
            this.xrTableCell104.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeIndicador]")});
            this.xrTableCell104.Name = "xrTableCell104";
            this.xrTableCell104.StylePriority.UseTextAlignment = false;
            this.xrTableCell104.Weight = 2.36449329526D;
            // 
            // xrTableCell105
            // 
            this.xrTableCell105.Dpi = 254F;
            this.xrTableCell105.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Mes]")});
            this.xrTableCell105.Name = "xrTableCell105";
            this.xrTableCell105.StylePriority.UseTextAlignment = false;
            this.xrTableCell105.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell105.Weight = 1.37470539266769D;
            // 
            // xrTableCell106
            // 
            this.xrTableCell106.Dpi = 254F;
            this.xrTableCell106.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MetaAcumuladaAno]")});
            this.xrTableCell106.Name = "xrTableCell106";
            this.xrTableCell106.StylePriority.UseTextAlignment = false;
            this.xrTableCell106.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell106.TextFormatString = "{0:n2}";
            this.xrTableCell106.Weight = 1.09976431108538D;
            // 
            // xrTableCell107
            // 
            this.xrTableCell107.Dpi = 254F;
            this.xrTableCell107.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ResultadoAcumuladoAno]")});
            this.xrTableCell107.Name = "xrTableCell107";
            this.xrTableCell107.StylePriority.UseTextAlignment = false;
            this.xrTableCell107.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell107.TextFormatString = "{0:n2}";
            this.xrTableCell107.Weight = 1.09976436859482D;
            // 
            // GroupHeader10
            // 
            this.GroupHeader10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable24,
            this.xrLabel35});
            this.GroupHeader10.Dpi = 254F;
            this.GroupHeader10.HeightF = 150F;
            this.GroupHeader10.KeepTogether = true;
            this.GroupHeader10.Name = "GroupHeader10";
            this.GroupHeader10.RepeatEveryPage = true;
            this.GroupHeader10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable24
            // 
            this.xrTable24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable24.Dpi = 254F;
            this.xrTable24.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable24.Name = "xrTable24";
            this.xrTable24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable24.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow30});
            this.xrTable24.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable24.StylePriority.UseBackColor = false;
            this.xrTable24.StylePriority.UseBorders = false;
            this.xrTable24.StylePriority.UseFont = false;
            this.xrTable24.StylePriority.UsePadding = false;
            // 
            // xrTableRow30
            // 
            this.xrTableRow30.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell120,
            this.xrTableCell121,
            this.xrTableCell122,
            this.xrTableCell123,
            this.xrTableCell124,
            this.xrTableCell125});
            this.xrTableRow30.Dpi = 254F;
            this.xrTableRow30.Name = "xrTableRow30";
            this.xrTableRow30.Weight = 11.5D;
            // 
            // xrTableCell120
            // 
            this.xrTableCell120.Dpi = 254F;
            this.xrTableCell120.Name = "xrTableCell120";
            this.xrTableCell120.StylePriority.UseTextAlignment = false;
            this.xrTableCell120.Text = "Status";
            this.xrTableCell120.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell120.Weight = 0.659858613066614D;
            // 
            // xrTableCell121
            // 
            this.xrTableCell121.Dpi = 254F;
            this.xrTableCell121.Name = "xrTableCell121";
            this.xrTableCell121.Text = "Descrição da Meta";
            this.xrTableCell121.Weight = 3.84917503147645D;
            // 
            // xrTableCell122
            // 
            this.xrTableCell122.Dpi = 254F;
            this.xrTableCell122.Name = "xrTableCell122";
            this.xrTableCell122.StylePriority.UseTextAlignment = false;
            this.xrTableCell122.Text = "Indicador";
            this.xrTableCell122.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell122.Weight = 2.36449329210174D;
            // 
            // xrTableCell123
            // 
            this.xrTableCell123.Dpi = 254F;
            this.xrTableCell123.Name = "xrTableCell123";
            this.xrTableCell123.StylePriority.UseTextAlignment = false;
            this.xrTableCell123.Text = "Período";
            this.xrTableCell123.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell123.Weight = 1.37470538950943D;
            // 
            // xrTableCell124
            // 
            this.xrTableCell124.Dpi = 254F;
            this.xrTableCell124.Name = "xrTableCell124";
            this.xrTableCell124.StylePriority.UseTextAlignment = false;
            this.xrTableCell124.Text = "Meta (Nº)";
            this.xrTableCell124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell124.Weight = 1.09976430545602D;
            // 
            // xrTableCell125
            // 
            this.xrTableCell125.Dpi = 254F;
            this.xrTableCell125.Name = "xrTableCell125";
            this.xrTableCell125.StylePriority.UseTextAlignment = false;
            this.xrTableCell125.Text = "Resultado (Nº)";
            this.xrTableCell125.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell125.Weight = 1.0997645624196D;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Dpi = 254F;
            this.xrLabel35.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.Text = "Metas";
            // 
            // GroupFooter9
            // 
            this.GroupFooter9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel52,
            this.xrLabel53});
            this.GroupFooter9.Dpi = 254F;
            this.GroupFooter9.Expanded = false;
            this.GroupFooter9.HeightF = 150F;
            this.GroupFooter9.KeepTogether = true;
            this.GroupFooter9.Name = "GroupFooter9";
            this.GroupFooter9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel52
            // 
            this.xrLabel52.Dpi = 254F;
            this.xrLabel52.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel52.StylePriority.UseFont = false;
            this.xrLabel52.Text = "Metas";
            // 
            // xrLabel53
            // 
            this.xrLabel53.Dpi = 254F;
            this.xrLabel53.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel53.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel53.StylePriority.UseFont = false;
            this.xrLabel53.StylePriority.UseForeColor = false;
            this.xrLabel53.StylePriority.UseTextAlignment = false;
            this.xrLabel53.Text = "Não existem metas para o projeto";
            this.xrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // DetailReportToDoList
            // 
            this.DetailReportToDoList.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail13,
            this.GroupHeader11,
            this.GroupFooter10});
            this.DetailReportToDoList.DataMember = "StatusReport.StatusReport_Projeto.Projeto_ToDoList";
            this.DetailReportToDoList.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportToDoList.Dpi = 254F;
            this.DetailReportToDoList.Level = 9;
            this.DetailReportToDoList.Name = "DetailReportToDoList";
            this.DetailReportToDoList.Visible = false;
            // 
            // Detail13
            // 
            this.Detail13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable23});
            this.Detail13.Dpi = 254F;
            this.Detail13.HeightF = 50F;
            this.Detail13.Name = "Detail13";
            this.Detail13.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable23
            // 
            this.xrTable23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable23.Dpi = 254F;
            this.xrTable23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable23.Name = "xrTable23";
            this.xrTable23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable23.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow22});
            this.xrTable23.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable23.StylePriority.UseBackColor = false;
            this.xrTable23.StylePriority.UseBorders = false;
            this.xrTable23.StylePriority.UseFont = false;
            this.xrTable23.StylePriority.UsePadding = false;
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell130,
            this.xrTableCell131,
            this.xrTableCell132,
            this.xrTableCell133,
            this.xrTableCell134});
            this.xrTableRow22.Dpi = 254F;
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 11.5D;
            // 
            // xrTableCell130
            // 
            this.xrTableCell130.Dpi = 254F;
            this.xrTableCell130.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Tarefa]")});
            this.xrTableCell130.Name = "xrTableCell130";
            this.xrTableCell130.Weight = 0.432306330962845D;
            // 
            // xrTableCell131
            // 
            this.xrTableCell131.Dpi = 254F;
            this.xrTableCell131.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DescricaoStatusTarefa]")});
            this.xrTableCell131.Name = "xrTableCell131";
            this.xrTableCell131.StylePriority.UseTextAlignment = false;
            this.xrTableCell131.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell131.Weight = 0.123516089832345D;
            // 
            // xrTableCell132
            // 
            this.xrTableCell132.Dpi = 254F;
            this.xrTableCell132.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioPrevisto]")});
            this.xrTableCell132.Name = "xrTableCell132";
            this.xrTableCell132.StylePriority.UseTextAlignment = false;
            this.xrTableCell132.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell132.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell132.Weight = 0.123516092987071D;
            // 
            // xrTableCell133
            // 
            this.xrTableCell133.Dpi = 254F;
            this.xrTableCell133.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoPrevisto]")});
            this.xrTableCell133.Name = "xrTableCell133";
            this.xrTableCell133.StylePriority.UseTextAlignment = false;
            this.xrTableCell133.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell133.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell133.Weight = 0.123516094420993D;
            // 
            // xrTableCell134
            // 
            this.xrTableCell134.Dpi = 254F;
            this.xrTableCell134.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Responsavel]")});
            this.xrTableCell134.Name = "xrTableCell134";
            this.xrTableCell134.Weight = 0.370548260114868D;
            // 
            // GroupHeader11
            // 
            this.GroupHeader11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable21,
            this.xrLabel36});
            this.GroupHeader11.Dpi = 254F;
            this.GroupHeader11.HeightF = 150F;
            this.GroupHeader11.KeepTogether = true;
            this.GroupHeader11.Name = "GroupHeader11";
            this.GroupHeader11.RepeatEveryPage = true;
            this.GroupHeader11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable21
            // 
            this.xrTable21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable21.Dpi = 254F;
            this.xrTable21.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable21.Name = "xrTable21";
            this.xrTable21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable21.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow21});
            this.xrTable21.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable21.StylePriority.UseBackColor = false;
            this.xrTable21.StylePriority.UseBorders = false;
            this.xrTable21.StylePriority.UseFont = false;
            this.xrTable21.StylePriority.UsePadding = false;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell119,
            this.xrTableCell126,
            this.xrTableCell127,
            this.xrTableCell128,
            this.xrTableCell129});
            this.xrTableRow21.Dpi = 254F;
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 11.5D;
            // 
            // xrTableCell119
            // 
            this.xrTableCell119.Dpi = 254F;
            this.xrTableCell119.Name = "xrTableCell119";
            this.xrTableCell119.Text = "Tarefa";
            this.xrTableCell119.Weight = 0.432306330962845D;
            // 
            // xrTableCell126
            // 
            this.xrTableCell126.Dpi = 254F;
            this.xrTableCell126.Name = "xrTableCell126";
            this.xrTableCell126.StylePriority.UseTextAlignment = false;
            this.xrTableCell126.Text = "Status";
            this.xrTableCell126.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell126.Weight = 0.123516089832345D;
            // 
            // xrTableCell127
            // 
            this.xrTableCell127.Dpi = 254F;
            this.xrTableCell127.Name = "xrTableCell127";
            this.xrTableCell127.StylePriority.UseTextAlignment = false;
            this.xrTableCell127.Text = "Início Previsto";
            this.xrTableCell127.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell127.Weight = 0.123516092987071D;
            // 
            // xrTableCell128
            // 
            this.xrTableCell128.Dpi = 254F;
            this.xrTableCell128.Name = "xrTableCell128";
            this.xrTableCell128.StylePriority.UseTextAlignment = false;
            this.xrTableCell128.Text = "Término Previsto";
            this.xrTableCell128.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell128.Weight = 0.123516094420993D;
            // 
            // xrTableCell129
            // 
            this.xrTableCell129.Dpi = 254F;
            this.xrTableCell129.Name = "xrTableCell129";
            this.xrTableCell129.Text = "Responsável";
            this.xrTableCell129.Weight = 0.370548260114868D;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Dpi = 254F;
            this.xrLabel36.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.Text = "Pendências de To Do List";
            // 
            // GroupFooter10
            // 
            this.GroupFooter10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel54,
            this.xrLabel55});
            this.GroupFooter10.Dpi = 254F;
            this.GroupFooter10.HeightF = 150F;
            this.GroupFooter10.KeepTogether = true;
            this.GroupFooter10.Name = "GroupFooter10";
            this.GroupFooter10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel54
            // 
            this.xrLabel54.Dpi = 254F;
            this.xrLabel54.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel54.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel54.StylePriority.UseFont = false;
            this.xrLabel54.StylePriority.UseForeColor = false;
            this.xrLabel54.StylePriority.UseTextAlignment = false;
            this.xrLabel54.Text = "Não existem pendências de To Do List para o projeto";
            this.xrLabel54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel55
            // 
            this.xrLabel55.Dpi = 254F;
            this.xrLabel55.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel55.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel55.StylePriority.UseFont = false;
            this.xrLabel55.Text = "Pendências de To Do List";
            // 
            // DetailReportContratos
            // 
            this.DetailReportContratos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail14,
            this.GroupHeader12,
            this.GroupFooter11});
            this.DetailReportContratos.DataMember = "StatusReport.StatusReport_Projeto.Projeto_Contrato";
            this.DetailReportContratos.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportContratos.Dpi = 254F;
            this.DetailReportContratos.Level = 10;
            this.DetailReportContratos.Name = "DetailReportContratos";
            // 
            // Detail14
            // 
            this.Detail14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable22});
            this.Detail14.Dpi = 254F;
            this.Detail14.HeightF = 50F;
            this.Detail14.Name = "Detail14";
            this.Detail14.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable22
            // 
            this.xrTable22.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable22.Dpi = 254F;
            this.xrTable22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable22.Name = "xrTable22";
            this.xrTable22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable22.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20});
            this.xrTable22.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable22.StylePriority.UseBackColor = false;
            this.xrTable22.StylePriority.UseBorders = false;
            this.xrTable22.StylePriority.UseFont = false;
            this.xrTable22.StylePriority.UsePadding = false;
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell114,
            this.xrTableCell115,
            this.xrTableCell116,
            this.xrTableCell117,
            this.xrTableCell118,
            this.xrTableCell135});
            this.xrTableRow20.Dpi = 254F;
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 11.5D;
            // 
            // xrTableCell114
            // 
            this.xrTableCell114.Dpi = 254F;
            this.xrTableCell114.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NumeroContrato]")});
            this.xrTableCell114.Name = "xrTableCell114";
            this.xrTableCell114.Weight = 0.160570917296045D;
            // 
            // xrTableCell115
            // 
            this.xrTableCell115.Dpi = 254F;
            this.xrTableCell115.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Fornecedor]")});
            this.xrTableCell115.Name = "xrTableCell115";
            this.xrTableCell115.Weight = 0.426130540983216D;
            // 
            // xrTableCell116
            // 
            this.xrTableCell116.Dpi = 254F;
            this.xrTableCell116.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TerminoVigencia]")});
            this.xrTableCell116.Name = "xrTableCell116";
            this.xrTableCell116.StylePriority.UseTextAlignment = false;
            this.xrTableCell116.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell116.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell116.Weight = 0.123516090291773D;
            // 
            // xrTableCell117
            // 
            this.xrTableCell117.Dpi = 254F;
            this.xrTableCell117.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ValorContrato]")});
            this.xrTableCell117.Name = "xrTableCell117";
            this.xrTableCell117.StylePriority.UseTextAlignment = false;
            this.xrTableCell117.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell117.TextFormatString = "{0:n2}";
            this.xrTableCell117.Weight = 0.154395112864716D;
            // 
            // xrTableCell118
            // 
            this.xrTableCell118.Dpi = 254F;
            this.xrTableCell118.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ValorPago]")});
            this.xrTableCell118.Name = "xrTableCell118";
            this.xrTableCell118.StylePriority.UseTextAlignment = false;
            this.xrTableCell118.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell118.TextFormatString = "{0:n2}";
            this.xrTableCell118.Weight = 0.154395112864716D;
            // 
            // xrTableCell135
            // 
            this.xrTableCell135.Dpi = 254F;
            this.xrTableCell135.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ValorRestante]")});
            this.xrTableCell135.Name = "xrTableCell135";
            this.xrTableCell135.StylePriority.UseTextAlignment = false;
            this.xrTableCell135.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell135.TextFormatString = "{0:n2}";
            this.xrTableCell135.Weight = 0.154395094017656D;
            // 
            // GroupHeader12
            // 
            this.GroupHeader12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable20,
            this.xrLabel33});
            this.GroupHeader12.Dpi = 254F;
            this.GroupHeader12.HeightF = 150F;
            this.GroupHeader12.KeepTogether = true;
            this.GroupHeader12.Name = "GroupHeader12";
            this.GroupHeader12.RepeatEveryPage = true;
            this.GroupHeader12.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable20
            // 
            this.xrTable20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable20.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable20.Dpi = 254F;
            this.xrTable20.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable20.Name = "xrTable20";
            this.xrTable20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow25});
            this.xrTable20.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable20.StylePriority.UseBackColor = false;
            this.xrTable20.StylePriority.UseBorders = false;
            this.xrTable20.StylePriority.UseFont = false;
            this.xrTable20.StylePriority.UsePadding = false;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell108,
            this.xrTableCell109,
            this.xrTableCell110,
            this.xrTableCell111,
            this.xrTableCell112,
            this.xrTableCell113});
            this.xrTableRow25.Dpi = 254F;
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.Weight = 11.5D;
            // 
            // xrTableCell108
            // 
            this.xrTableCell108.Dpi = 254F;
            this.xrTableCell108.Name = "xrTableCell108";
            this.xrTableCell108.Text = "Nº do Contrato";
            this.xrTableCell108.Weight = 0.160570917296045D;
            // 
            // xrTableCell109
            // 
            this.xrTableCell109.Dpi = 254F;
            this.xrTableCell109.Name = "xrTableCell109";
            this.xrTableCell109.Text = "Fornecedor";
            this.xrTableCell109.Weight = 0.426130540983216D;
            // 
            // xrTableCell110
            // 
            this.xrTableCell110.Dpi = 254F;
            this.xrTableCell110.Name = "xrTableCell110";
            this.xrTableCell110.StylePriority.UseTextAlignment = false;
            this.xrTableCell110.Text = "Término Vigência";
            this.xrTableCell110.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell110.Weight = 0.123516090291773D;
            // 
            // xrTableCell111
            // 
            this.xrTableCell111.Dpi = 254F;
            this.xrTableCell111.Name = "xrTableCell111";
            this.xrTableCell111.StylePriority.UseTextAlignment = false;
            this.xrTableCell111.Text = "Valor Total (R$)";
            this.xrTableCell111.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell111.Weight = 0.154395112864716D;
            // 
            // xrTableCell112
            // 
            this.xrTableCell112.Dpi = 254F;
            this.xrTableCell112.Name = "xrTableCell112";
            this.xrTableCell112.StylePriority.UseTextAlignment = false;
            this.xrTableCell112.Text = "Valor Pago (R$)";
            this.xrTableCell112.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell112.Weight = 0.154395112864716D;
            // 
            // xrTableCell113
            // 
            this.xrTableCell113.Dpi = 254F;
            this.xrTableCell113.Name = "xrTableCell113";
            this.xrTableCell113.StylePriority.UseTextAlignment = false;
            this.xrTableCell113.Text = "Valor Restante (R$)";
            this.xrTableCell113.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell113.Weight = 0.154395094017656D;
            // 
            // xrLabel33
            // 
            this.xrLabel33.Dpi = 254F;
            this.xrLabel33.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel33.Name = "xrLabel33";
            this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel33.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel33.StylePriority.UseFont = false;
            this.xrLabel33.Text = "Contratos";
            // 
            // GroupFooter11
            // 
            this.GroupFooter11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel56,
            this.xrLabel57});
            this.GroupFooter11.Dpi = 254F;
            this.GroupFooter11.HeightF = 150F;
            this.GroupFooter11.KeepTogether = true;
            this.GroupFooter11.Name = "GroupFooter11";
            this.GroupFooter11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel56
            // 
            this.xrLabel56.Dpi = 254F;
            this.xrLabel56.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel56.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel56.StylePriority.UseFont = false;
            this.xrLabel56.Text = "Contratos";
            // 
            // xrLabel57
            // 
            this.xrLabel57.Dpi = 254F;
            this.xrLabel57.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel57.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel57.StylePriority.UseFont = false;
            this.xrLabel57.StylePriority.UseForeColor = false;
            this.xrLabel57.StylePriority.UseTextAlignment = false;
            this.xrLabel57.Text = "Não existem contratos para o projeto";
            this.xrLabel57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // DetailReportQuestoes
            // 
            this.DetailReportQuestoes.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail12,
            this.GroupHeader8,
            this.GroupFooter8});
            this.DetailReportQuestoes.DataMember = "StatusReport.StatusReport_Projeto.Projeto_Questao";
            this.DetailReportQuestoes.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReportQuestoes.Dpi = 254F;
            this.DetailReportQuestoes.Level = 7;
            this.DetailReportQuestoes.Name = "DetailReportQuestoes";
            // 
            // Detail12
            // 
            this.Detail12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable27});
            this.Detail12.Dpi = 254F;
            this.Detail12.HeightF = 50F;
            this.Detail12.Name = "Detail12";
            this.Detail12.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable27
            // 
            this.xrTable27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable27.Dpi = 254F;
            this.xrTable27.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable27.Name = "xrTable27";
            this.xrTable27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable27.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
            this.xrTable27.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable27.StylePriority.UseBackColor = false;
            this.xrTable27.StylePriority.UseBorders = false;
            this.xrTable27.StylePriority.UseFont = false;
            this.xrTable27.StylePriority.UsePadding = false;
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell148,
            this.xrTableCell155,
            this.xrTableCell156,
            this.xrTableCell157,
            this.xrTableCell158,
            this.xrTableCell159});
            this.xrTableRow26.Dpi = 254F;
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.Weight = 11.5D;
            // 
            // xrTableCell148
            // 
            this.xrTableCell148.Dpi = 254F;
            this.xrTableCell148.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Status]")});
            this.xrTableCell148.Name = "xrTableCell148";
            this.xrTableCell148.Weight = 0.824823183707341D;
            // 
            // xrTableCell155
            // 
            this.xrTableCell155.Dpi = 254F;
            this.xrTableCell155.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Questao]")});
            this.xrTableCell155.Name = "xrTableCell155";
            this.xrTableCell155.Weight = 3.84917596438955D;
            // 
            // xrTableCell156
            // 
            this.xrTableCell156.Dpi = 254F;
            this.xrTableCell156.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Urgencia]")});
            this.xrTableCell156.Name = "xrTableCell156";
            this.xrTableCell156.StylePriority.UseTextAlignment = false;
            this.xrTableCell156.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell156.Weight = 1.37470472142481D;
            // 
            // xrTableCell157
            // 
            this.xrTableCell157.Dpi = 254F;
            this.xrTableCell157.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Prioridade]")});
            this.xrTableCell157.Name = "xrTableCell157";
            this.xrTableCell157.StylePriority.UseTextAlignment = false;
            this.xrTableCell157.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell157.Weight = 1.37470539266769D;
            // 
            // xrTableCell158
            // 
            this.xrTableCell158.Dpi = 254F;
            this.xrTableCell158.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LimiteEliminacao]")});
            this.xrTableCell158.Name = "xrTableCell158";
            this.xrTableCell158.StylePriority.UseTextAlignment = false;
            this.xrTableCell158.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell158.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell158.Weight = 1.09976431108539D;
            // 
            // xrTableCell159
            // 
            this.xrTableCell159.Dpi = 254F;
            this.xrTableCell159.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Responsavel]")});
            this.xrTableCell159.Name = "xrTableCell159";
            this.xrTableCell159.StylePriority.UseTextAlignment = false;
            this.xrTableCell159.Weight = 1.92458762075507D;
            // 
            // GroupHeader8
            // 
            this.GroupHeader8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable26,
            this.xrLabel38});
            this.GroupHeader8.Dpi = 254F;
            this.GroupHeader8.HeightF = 150F;
            this.GroupHeader8.KeepTogether = true;
            this.GroupHeader8.Name = "GroupHeader8";
            this.GroupHeader8.RepeatEveryPage = true;
            this.GroupHeader8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable26
            // 
            this.xrTable26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable26.Dpi = 254F;
            this.xrTable26.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrTable26.Name = "xrTable26";
            this.xrTable26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable26.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow24});
            this.xrTable26.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable26.StylePriority.UseBackColor = false;
            this.xrTable26.StylePriority.UseBorders = false;
            this.xrTable26.StylePriority.UseFont = false;
            this.xrTable26.StylePriority.UsePadding = false;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell142,
            this.xrTableCell143,
            this.xrTableCell144,
            this.xrTableCell145,
            this.xrTableCell146,
            this.xrTableCell147});
            this.xrTableRow24.Dpi = 254F;
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 11.5D;
            // 
            // xrTableCell142
            // 
            this.xrTableCell142.Dpi = 254F;
            this.xrTableCell142.Name = "xrTableCell142";
            this.xrTableCell142.Text = "Status";
            this.xrTableCell142.Weight = 0.824823183707341D;
            // 
            // xrTableCell143
            // 
            this.xrTableCell143.Dpi = 254F;
            this.xrTableCell143.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StatusReport.labelQuestoes]")});
            this.xrTableCell143.Name = "xrTableCell143";
            this.xrTableCell143.Weight = 3.84917596438955D;
            // 
            // xrTableCell144
            // 
            this.xrTableCell144.Dpi = 254F;
            this.xrTableCell144.Name = "xrTableCell144";
            this.xrTableCell144.StylePriority.UseTextAlignment = false;
            this.xrTableCell144.Text = "Urgência";
            this.xrTableCell144.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell144.Weight = 1.37470472142481D;
            // 
            // xrTableCell145
            // 
            this.xrTableCell145.Dpi = 254F;
            this.xrTableCell145.Name = "xrTableCell145";
            this.xrTableCell145.StylePriority.UseTextAlignment = false;
            this.xrTableCell145.Text = "Prioridade";
            this.xrTableCell145.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell145.Weight = 1.37470539266769D;
            // 
            // xrTableCell146
            // 
            this.xrTableCell146.Dpi = 254F;
            this.xrTableCell146.Name = "xrTableCell146";
            this.xrTableCell146.StylePriority.UseTextAlignment = false;
            this.xrTableCell146.Text = "Limite Resolução";
            this.xrTableCell146.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell146.Weight = 1.09976431108539D;
            // 
            // xrTableCell147
            // 
            this.xrTableCell147.Dpi = 254F;
            this.xrTableCell147.Name = "xrTableCell147";
            this.xrTableCell147.StylePriority.UseTextAlignment = false;
            this.xrTableCell147.Text = "Responsável";
            this.xrTableCell147.Weight = 1.92458762075507D;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Dpi = 254F;
            this.xrLabel38.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StatusReport.labelQuestoes]")});
            this.xrLabel38.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel38.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel38.StylePriority.UseFont = false;
            // 
            // GroupFooter8
            // 
            this.GroupFooter8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel51,
            this.xrLabel50});
            this.GroupFooter8.Dpi = 254F;
            this.GroupFooter8.HeightF = 150F;
            this.GroupFooter8.KeepTogether = true;
            this.GroupFooter8.Name = "GroupFooter8";
            this.GroupFooter8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel51
            // 
            this.xrLabel51.Dpi = 254F;
            this.xrLabel51.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StatusReport.labelQuestoes]")});
            this.xrLabel51.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel51.StylePriority.UseFont = false;
            this.xrLabel51.StylePriority.UseForeColor = false;
            this.xrLabel51.StylePriority.UseTextAlignment = false;
            this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel51.TextFormatString = "Não existem {0} para o projeto";
            // 
            // xrLabel50
            // 
            this.xrLabel50.Dpi = 254F;
            this.xrLabel50.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StatusReport.labelQuestoes]")});
            this.xrLabel50.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel50.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel50.StylePriority.UseFont = false;
            this.xrLabel50.Text = "Marcos";
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail15,
            this.GroupHeader13,
            this.GroupFooter12});
            this.DetailReport.DataMember = "StatusReport.StatusReport_Projeto.Projeto_StatusReportEntregas";
            this.DetailReport.DataSource = this.dsBoletimStatusNovoPadrao;
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 11;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail15
            // 
            this.Detail15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable29});
            this.Detail15.Dpi = 254F;
            this.Detail15.HeightF = 50F;
            this.Detail15.Name = "Detail15";
            this.Detail15.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrTable29
            // 
            this.xrTable29.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable29.Dpi = 254F;
            this.xrTable29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable29.Name = "xrTable29";
            this.xrTable29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable29.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
            this.xrTable29.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrTable29.StylePriority.UseBackColor = false;
            this.xrTable29.StylePriority.UseBorders = false;
            this.xrTable29.StylePriority.UseFont = false;
            this.xrTable29.StylePriority.UsePadding = false;
            // 
            // xrTableRow28
            // 
            this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell162,
            this.xrTableCell166,
            this.xrTableCell167,
            this.xrTableCell168,
            this.xrTableCell169});
            this.xrTableRow28.Dpi = 254F;
            this.xrTableRow28.Name = "xrTableRow28";
            this.xrTableRow28.Weight = 11.5D;
            // 
            // xrTableCell162
            // 
            this.xrTableCell162.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox4});
            this.xrTableCell162.Dpi = 254F;
            this.xrTableCell162.Name = "xrTableCell162";
            this.xrTableCell162.Weight = 0.0741096533147093D;
            // 
            // xrPictureBox4
            // 
            this.xrPictureBox4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrPictureBox4.Dpi = 254F;
            this.xrPictureBox4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageUrl", "FormatString(\'~/imagens/{0}.gif\', [StatusTarefa])")});
            this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            this.xrPictureBox4.Name = "xrPictureBox4";
            this.xrPictureBox4.SizeF = new System.Drawing.SizeF(50F, 50F);
            this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox4.StylePriority.UseBorderColor = false;
            this.xrPictureBox4.StylePriority.UseBorders = false;
            // 
            // xrTableCell166
            // 
            this.xrTableCell166.Dpi = 254F;
            this.xrTableCell166.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeTarefa]")});
            this.xrTableCell166.Name = "xrTableCell166";
            this.xrTableCell166.Weight = 0.72874492722377D;
            // 
            // xrTableCell167
            // 
            this.xrTableCell167.Dpi = 254F;
            this.xrTableCell167.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioPrevisto]")});
            this.xrTableCell167.Name = "xrTableCell167";
            this.xrTableCell167.StylePriority.UseTextAlignment = false;
            this.xrTableCell167.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell167.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell167.Weight = 0.123516126598224D;
            // 
            // xrTableCell168
            // 
            this.xrTableCell168.Dpi = 254F;
            this.xrTableCell168.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioReprogramado]")});
            this.xrTableCell168.Name = "xrTableCell168";
            this.xrTableCell168.StylePriority.UseTextAlignment = false;
            this.xrTableCell168.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell168.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell168.Weight = 0.123516090014239D;
            // 
            // xrTableCell169
            // 
            this.xrTableCell169.Dpi = 254F;
            this.xrTableCell169.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[InicioReal]")});
            this.xrTableCell169.Name = "xrTableCell169";
            this.xrTableCell169.StylePriority.UseTextAlignment = false;
            this.xrTableCell169.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell169.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell169.Weight = 0.123516071167179D;
            // 
            // GroupHeader13
            // 
            this.GroupHeader13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel39,
            this.xrTable28});
            this.GroupHeader13.Dpi = 254F;
            this.GroupHeader13.HeightF = 150F;
            this.GroupHeader13.Name = "GroupHeader13";
            this.GroupHeader13.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Band_BeforePrint);
            // 
            // xrLabel39
            // 
            this.xrLabel39.Dpi = 254F;
            this.xrLabel39.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel39.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel39.StylePriority.UseFont = false;
            this.xrLabel39.Text = "Entregas";
            // 
            // xrTable28
            // 
            this.xrTable28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable28.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable28.Dpi = 254F;
            this.xrTable28.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable28.Name = "xrTable28";
            this.xrTable28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable28.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
            this.xrTable28.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable28.StylePriority.UseBackColor = false;
            this.xrTable28.StylePriority.UseBorders = false;
            this.xrTable28.StylePriority.UseFont = false;
            this.xrTable28.StylePriority.UsePadding = false;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell160,
            this.xrTableCell161,
            this.xrTableCell163,
            this.xrTableCell164,
            this.xrTableCell165});
            this.xrTableRow27.Dpi = 254F;
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 11.5D;
            // 
            // xrTableCell160
            // 
            this.xrTableCell160.Dpi = 254F;
            this.xrTableCell160.Name = "xrTableCell160";
            this.xrTableCell160.Text = "Status";
            this.xrTableCell160.Weight = 0.0741096533147093D;
            // 
            // xrTableCell161
            // 
            this.xrTableCell161.Dpi = 254F;
            this.xrTableCell161.Name = "xrTableCell161";
            this.xrTableCell161.Text = "Entrega";
            this.xrTableCell161.Weight = 0.72874492722377D;
            // 
            // xrTableCell163
            // 
            this.xrTableCell163.Dpi = 254F;
            this.xrTableCell163.Name = "xrTableCell163";
            this.xrTableCell163.StylePriority.UseTextAlignment = false;
            this.xrTableCell163.Text = "Início Previsto";
            this.xrTableCell163.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell163.Weight = 0.123516126598224D;
            // 
            // xrTableCell164
            // 
            this.xrTableCell164.Dpi = 254F;
            this.xrTableCell164.Name = "xrTableCell164";
            this.xrTableCell164.StylePriority.UseTextAlignment = false;
            this.xrTableCell164.Text = "Início Reprogr.";
            this.xrTableCell164.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell164.Weight = 0.123516090014239D;
            // 
            // xrTableCell165
            // 
            this.xrTableCell165.Dpi = 254F;
            this.xrTableCell165.Name = "xrTableCell165";
            this.xrTableCell165.StylePriority.UseTextAlignment = false;
            this.xrTableCell165.Text = "Início Real";
            this.xrTableCell165.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell165.Weight = 0.123516071167179D;
            // 
            // GroupFooter12
            // 
            this.GroupFooter12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel58,
            this.xrLabel59});
            this.GroupFooter12.Dpi = 254F;
            this.GroupFooter12.HeightF = 149.9999F;
            this.GroupFooter12.KeepTogether = true;
            this.GroupFooter12.Name = "GroupFooter12";
            this.GroupFooter12.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.BandCustomText_BeforePrint);
            // 
            // xrLabel58
            // 
            this.xrLabel58.Dpi = 254F;
            this.xrLabel58.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel58.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel58.StylePriority.UseFont = false;
            this.xrLabel58.Text = "Entregas";
            // 
            // xrLabel59
            // 
            this.xrLabel59.Dpi = 254F;
            this.xrLabel59.ForeColor = System.Drawing.Color.Gray;
            this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel59.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel59.StylePriority.UseFont = false;
            this.xrLabel59.StylePriority.UseForeColor = false;
            this.xrLabel59.StylePriority.UseTextAlignment = false;
            this.xrLabel59.Text = "Não existem entregas para o projeto";
            this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // DetailReportAnaliseGeral
            // 
            this.DetailReportAnaliseGeral.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3});
            this.DetailReportAnaliseGeral.Dpi = 254F;
            this.DetailReportAnaliseGeral.Level = 1;
            this.DetailReportAnaliseGeral.Name = "DetailReportAnaliseGeral";
            // 
            // Detail3
            // 
            this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRichText1,
            this.xrLabel4});
            this.Detail3.Dpi = 254F;
            this.Detail3.HeightF = 150F;
            this.Detail3.Name = "Detail3";
            // 
            // xrRichText1
            // 
            this.xrRichText1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrRichText1.Dpi = 254F;
            this.xrRichText1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Html", "[StatusReport.AnaliseGeral]")});
            this.xrRichText1.Font = new System.Drawing.Font("Verdana", 8F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.00002F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrRichText1.StylePriority.UseBorders = false;
            this.xrRichText1.StylePriority.UseFont = false;
            this.xrRichText1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrRichText_EvaluateBinding);
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "Análise Geral";
            // 
            // xrTable31
            // 
            this.xrTable31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable31.Dpi = 254F;
            this.xrTable31.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable31.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrTable31.Name = "xrTable31";
            this.xrTable31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable31.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow40});
            this.xrTable31.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable31.StylePriority.UseBackColor = false;
            this.xrTable31.StylePriority.UseBorders = false;
            this.xrTable31.StylePriority.UseFont = false;
            this.xrTable31.StylePriority.UsePadding = false;
            // 
            // xrTableRow40
            // 
            this.xrTableRow40.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell149,
            this.xrTableCell150,
            this.xrTableCell151,
            this.xrTableCell152,
            this.xrTableCell153,
            this.xrTableCell154});
            this.xrTableRow40.Dpi = 254F;
            this.xrTableRow40.Name = "xrTableRow40";
            this.xrTableRow40.Weight = 11.5D;
            // 
            // xrTableCell149
            // 
            this.xrTableCell149.Dpi = 254F;
            this.xrTableCell149.Name = "xrTableCell149";
            this.xrTableCell149.Text = "Status";
            this.xrTableCell149.Weight = 0.824823267612702D;
            // 
            // xrTableCell150
            // 
            this.xrTableCell150.Dpi = 254F;
            this.xrTableCell150.Name = "xrTableCell150";
            this.xrTableCell150.Text = "Risco";
            this.xrTableCell150.Weight = 3.84917520924131D;
            // 
            // xrTableCell151
            // 
            this.xrTableCell151.Dpi = 254F;
            this.xrTableCell151.Name = "xrTableCell151";
            this.xrTableCell151.StylePriority.UseTextAlignment = false;
            this.xrTableCell151.Text = "Probabilidade";
            this.xrTableCell151.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell151.Weight = 1.37470539266769D;
            // 
            // xrTableCell152
            // 
            this.xrTableCell152.Dpi = 254F;
            this.xrTableCell152.Name = "xrTableCell152";
            this.xrTableCell152.StylePriority.UseTextAlignment = false;
            this.xrTableCell152.Text = "Impacto";
            this.xrTableCell152.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell152.Weight = 1.37470539266769D;
            // 
            // xrTableCell153
            // 
            this.xrTableCell153.Dpi = 254F;
            this.xrTableCell153.Name = "xrTableCell153";
            this.xrTableCell153.StylePriority.UseTextAlignment = false;
            this.xrTableCell153.Text = "Limite Eliminação";
            this.xrTableCell153.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell153.Weight = 1.09976431108539D;
            // 
            // xrTableCell154
            // 
            this.xrTableCell154.Dpi = 254F;
            this.xrTableCell154.Name = "xrTableCell154";
            this.xrTableCell154.StylePriority.UseTextAlignment = false;
            this.xrTableCell154.Text = "Responsável";
            this.xrTableCell154.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell154.Weight = 1.92458762075507D;
            // 
            // xrLabel34
            // 
            this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.labelQuestoes")});
            this.xrLabel34.Dpi = 254F;
            this.xrLabel34.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(1900F, 50F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.Text = "Questões";
            // 
            // xrTable18
            // 
            this.xrTable18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.xrTable18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable18.Dpi = 254F;
            this.xrTable18.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99995F);
            this.xrTable18.Name = "xrTable18";
            this.xrTable18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18});
            this.xrTable18.SizeF = new System.Drawing.SizeF(1900F, 75F);
            this.xrTable18.StylePriority.UseBackColor = false;
            this.xrTable18.StylePriority.UseBorders = false;
            this.xrTable18.StylePriority.UseFont = false;
            this.xrTable18.StylePriority.UsePadding = false;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell96,
            this.xrTableCell97,
            this.xrTableCell98,
            this.xrTableCell99,
            this.xrTableCell100,
            this.xrTableCell101});
            this.xrTableRow18.Dpi = 254F;
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.Weight = 11.5D;
            // 
            // xrTableCell96
            // 
            this.xrTableCell96.Dpi = 254F;
            this.xrTableCell96.Name = "xrTableCell96";
            this.xrTableCell96.Text = "Status";
            this.xrTableCell96.Weight = 0.824823267612702D;
            // 
            // xrTableCell97
            // 
            this.xrTableCell97.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.labelQuestoes")});
            this.xrTableCell97.Dpi = 254F;
            this.xrTableCell97.Name = "xrTableCell97";
            this.xrTableCell97.Text = "Risco";
            this.xrTableCell97.Weight = 3.84917520924131D;
            // 
            // xrTableCell98
            // 
            this.xrTableCell98.Dpi = 254F;
            this.xrTableCell98.Name = "xrTableCell98";
            this.xrTableCell98.StylePriority.UseTextAlignment = false;
            this.xrTableCell98.Text = "Urgência";
            this.xrTableCell98.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell98.Weight = 1.37470539266769D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.Dpi = 254F;
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.StylePriority.UseTextAlignment = false;
            this.xrTableCell99.Text = "Prioridade";
            this.xrTableCell99.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell99.Weight = 1.37470539266769D;
            // 
            // xrTableCell100
            // 
            this.xrTableCell100.Dpi = 254F;
            this.xrTableCell100.Name = "xrTableCell100";
            this.xrTableCell100.StylePriority.UseTextAlignment = false;
            this.xrTableCell100.Text = "Limite Resolução";
            this.xrTableCell100.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell100.Weight = 1.09976431108539D;
            // 
            // xrTableCell101
            // 
            this.xrTableCell101.Dpi = 254F;
            this.xrTableCell101.Name = "xrTableCell101";
            this.xrTableCell101.StylePriority.UseTextAlignment = false;
            this.xrTableCell101.Text = "Responsável";
            this.xrTableCell101.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell101.Weight = 1.92458762075507D;
            // 
            // cfTituloRelatorio
            // 
            this.cfTituloRelatorio.DataMember = "StatusReport";
            this.cfTituloRelatorio.Expression = "Iif(Upper([iniciaisTipoAssociacao]) == \'UN\', Concat(Concat([SiglaUnidadeNegocio]," +
    " \' - \'), [NomeUnidade]) , Concat(Concat([labelCarteira],\': \'),[NomeCarteira]))";
            this.cfTituloRelatorio.FieldType = DevExpress.XtraReports.UI.FieldType.String;
            this.cfTituloRelatorio.Name = "cfTituloRelatorio";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox5});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 200F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPictureBox5
            // 
            this.xrPictureBox5.Dpi = 254F;
            this.xrPictureBox5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[Entidade.LogoUnidadeNegocio]")});
            this.xrPictureBox5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox5.Name = "xrPictureBox5";
            this.xrPictureBox5.SizeF = new System.Drawing.SizeF(400F, 141.7917F);
            this.xrPictureBox5.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // rel_StatusReportNovoPadrao
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReportListaProjetos,
            this.DetailReportDetalhesProjetos,
            this.DetailReportAnaliseGeral,
            this.PageHeader});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cfTituloRelatorio});
            this.DataMember = "StatusReport";
            this.DataSource = this.dsBoletimStatusNovoPadrao;
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Margins = new System.Drawing.Printing.Margins(98, 98, 62, 125);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pCodigoStatusReport});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.ScriptsSource = "\r\nprivate void xrPictureBox2_AfterPrint(object sender, System.EventArgs e) {\r\n\r\n}" +
    "\r\n";
            this.SnapGridSize = 25F;
            this.Version = "19.1";
            this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.rel_StatusReportNovoPadrao_DataSourceDemanded);
            ((System.ComponentModel.ISupportInitialize)(this.dsBoletimStatusNovoPadrao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private bool VerificaBandaVisivel(string nomeColuna)
    {
        if (Report.GetCurrentColumnValue(nomeColuna) is DBNull)
            return false;

        return Report.GetCurrentColumnValue<string>(nomeColuna).ToUpper().Equals("S");
    }

    private void Detail_AfterPrint(object sender, EventArgs e)
    {
        if (VerificaBandaVisivel("ListaComentarioGeral"))
        {
            DetailReportAnaliseGeral.Visible = !Report.GetCurrentColumnValue<string>("iniciaisTipoAssociacao").Equals("PR");
        }
        else
        {
            DetailReportAnaliseGeral.Visible = false;
            Detail2.Controls.Remove(xrRichText2);
            Detail2.Controls.Remove(xrLabel17);
            Detail2.HeightF -= 125;
        }
        if (!VerificaBandaVisivel("ListaContasCusto"))
        {
            Detail2.Controls.Remove(xrLabel15);
            Detail2.Controls.Remove(xrLabel16);
            Detail2.Controls.Remove(xrLabel25);
            Detail2.Controls.Remove(xrLabel26);
            xrLabel17.TopF -= 75;
            xrRichText2.TopF -= 75;
            Detail2.HeightF -= 75;
        }
        DetailReportTarefasConcluidas.Visible = VerificaBandaVisivel("ListaTarefasConcluidas");
        DetailReportTarefasAtrasadas.Visible = VerificaBandaVisivel("ListaTarefasAtrasadas");
        DetailReportPendentes.Visible = VerificaBandaVisivel("ListaTarefasFuturas");
        DetailReportMarcos.Visible =
            VerificaBandaVisivel("ListaMarcosConcluidos") ||
            VerificaBandaVisivel("ListaMarcosAtrasados") ||
            VerificaBandaVisivel("ListaMarcosFuturos");
        DetailReportRiscos.Visible =
            VerificaBandaVisivel("ListaRiscosAtivos") ||
            VerificaBandaVisivel("ListaRiscosEliminados");
        DetailReportQuestoes.Visible =
            VerificaBandaVisivel("ListaQuestoesAtivas") ||
            VerificaBandaVisivel("ListaQuestoesResolvidas");
        DetailReportMetas.Visible = VerificaBandaVisivel("ListaMetasResultados");
        DetailReportContratos.Visible = VerificaBandaVisivel("ListaContratos");
        DetailReport.Visible = VerificaBandaVisivel("ListaEntregas");
    }

    private void rel_StatusReportNovoPadrao_DataSourceDemanded(object sender, EventArgs e)
    {
        string connectionString = cDados.classeDados.getStringConexao();
        string strCommand = string.Format("exec  [dbo].[p_getDadosStatusReportPadraoNovo] {0}", pCodigoStatusReport.Value);
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(strCommand, connection);
                command.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
                dsBoletimStatusNovoPadrao.Load(
                    command.ExecuteReader(), LoadOption.OverwriteChanges,
                    "StatusReport", "Entidade", "Projeto", "TarefaConcluida",
                    "TarefaAtrasada", "TarefaPendente", "Despesa", "Receita",
                    "Marco", "Risco", "Questao", "ToDoList", "Meta", "Contrato", "Entrega");
            }
            finally
            {
                connection.Close();
            }
        }
    }

    private void Band_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        object row = ((Band)sender).Report.GetCurrentRow();
        e.Cancel = row == null;
    }

    private void BandCustomText_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        object row = ((Band)sender).Report.GetCurrentRow();
        e.Cancel = !(row == null);
    }
    private void xrRichText_EvaluateBinding(object sender, BindingEventArgs e)
    {
        XRRichText xrRichText = (XRRichText)sender;
        string strValue = e.Value as string;
        if (string.IsNullOrWhiteSpace(strValue))
            e.Value = string.Format("<p style='text-align:center;color:gray;font-family:Verdana;font-size:8pt;'>{0}</p>", "Sem Análise Geral para o Período");
    }

    private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        setaBullet("StatusTarefa", sender as XRPictureBox);
    }

    private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        setaBullet("CorGeral", sender as XRPictureBox);        
    }

    private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        setaBullet("Status", sender as XRPictureBox);
    }

    private void setaBullet(string nomeColuna, XRPictureBox pictureBox)
    {
        try
        {
            object corImagem = pictureBox.Report.GetCurrentColumnValue(nomeColuna);
            string cor = (corImagem != null) ? corImagem.ToString() : "Branco";
            cor = cor.Trim();
            string caminhoDaImagem = System.AppDomain.CurrentDomain.BaseDirectory + "imagens\\" + cor + ".gif";
            if (caminhoDaImagem != "")
                pictureBox.ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(caminhoDaImagem);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
