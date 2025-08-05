using DevExpress.Web;
using DevExpress.XtraReports.UI;
using dsRelatorioAcompanhamentoTableAdapters;

/// <summary>
/// Summary description for relRelatorioAcompanhamento
/// </summary>
public class relRelatorioAcompanhamento : DevExpress.XtraReports.UI.XtraReport
{
    private dsRelatorioAcompanhamento ds;
    private XRControlStyle Title;
    private XRControlStyle Header;
    private XRControlStyle TextControlStyle;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRControlStyle PagaInfo;
    private XRTableRow xrTableRow55;
    private XRTableCell xrTableCell136;
    private XRTableCell xrTableCell104;
    private XRTableCell xrTableCell100;
    private XRTableCell xrTableCell133;
    private XRTableCell xrTableCell137;
    private XRTableCell xrTableCell138;
    private XRTableCell xrTableCell134;
    private XRTableCell xrTableCell135;
    private XRTableCell xrTableCell95;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell139;
    private XRTableCell xrTableCell140;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTableCell73;
    private DetailReportBand detailReportBand1;
    private DetailBand Detail17;
    private GroupHeaderBand GroupHeader7;
    private GroupFooterBand GroupFooter3;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relRelatorioAcompanhamento(int codigoRelatorio)
    {
        dados cDados = CdadosUtil.GetCdados(null);
        InitializeComponent();
        string connectionString = cDados.classeDados.getStringConexao();

        PopulaRelatorio(codigoRelatorio, connectionString);
    }

    private void PopulaRelatorio(int codigoRelatorio, string connectionString)
    {
        pbh_RelatorioAcompanhamentoTableAdapter ta =
            new pbh_RelatorioAcompanhamentoTableAdapter();
        ta.Connection = new System.Data.SqlClient.SqlConnection();
        ta.Connection.ConnectionString = connectionString;
        ta.Fill(ds.pbh_RelatorioAcompanhamento, codigoRelatorio);

        pbh_RelatorioAcompanhamentoAquisicoesTableAdapter taAquisicoes =
            new pbh_RelatorioAcompanhamentoAquisicoesTableAdapter();
        taAquisicoes.Connection = new System.Data.SqlClient.SqlConnection();
        taAquisicoes.Connection.ConnectionString = connectionString;
        taAquisicoes.Fill(ds.pbh_RelatorioAcompanhamentoAquisicoes, codigoRelatorio);

        pbh_RelatorioAcompanhamentoDestinatariosTableAdapter taDestinatarios =
            new pbh_RelatorioAcompanhamentoDestinatariosTableAdapter();
        taDestinatarios.Connection = new System.Data.SqlClient.SqlConnection();
        taDestinatarios.Connection.ConnectionString = connectionString;
        taDestinatarios.Fill(ds.pbh_RelatorioAcompanhamentoDestinatarios, codigoRelatorio);

        pbh_RelatorioAcompanhamentoEntregasTableAdapter taEntregas =
            new pbh_RelatorioAcompanhamentoEntregasTableAdapter();
        taEntregas.Connection = new System.Data.SqlClient.SqlConnection();
        taEntregas.Connection.ConnectionString = connectionString;
        taEntregas.Fill(ds.pbh_RelatorioAcompanhamentoEntregas, codigoRelatorio);

        pbh_RelatorioAcompanhamentoIndicadoresTableAdapter taIndicadores =
            new pbh_RelatorioAcompanhamentoIndicadoresTableAdapter();
        taIndicadores.Connection = new System.Data.SqlClient.SqlConnection();
        taIndicadores.Connection.ConnectionString = connectionString;
        taIndicadores.Fill(ds.pbh_RelatorioAcompanhamentoIndicadores, codigoRelatorio);

        pbh_RelatorioAcompanhamentoProblemasTableAdapter taProblemas =
            new pbh_RelatorioAcompanhamentoProblemasTableAdapter();
        taProblemas.Connection = new System.Data.SqlClient.SqlConnection();
        taProblemas.Connection.ConnectionString = connectionString;
        taProblemas.Fill(ds.pbh_RelatorioAcompanhamentoProblemas, codigoRelatorio);

        pbh_RelatorioAcompanhamentoRiscosTableAdapter taRiscos =
            new pbh_RelatorioAcompanhamentoRiscosTableAdapter();
        taRiscos.Connection = new System.Data.SqlClient.SqlConnection();
        taRiscos.Connection.ConnectionString = connectionString;
        taRiscos.Fill(ds.pbh_RelatorioAcompanhamentoRiscos, codigoRelatorio);
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
        //string resourceFileName = "relRelatorioAcompanhamento.resx";
        System.Resources.ResourceManager resources = global::Resources.relRelatorioAcompanhamento.ResourceManager;
        DevExpress.XtraReports.UI.DetailBand Detail;
        DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        DevExpress.XtraReports.UI.XRTable xrTable1;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        DevExpress.XtraReports.UI.DetailReportBand drIdentificaoProjeto;
        DevExpress.XtraReports.UI.DetailBand Detail1;
        DevExpress.XtraReports.UI.XRTable xrTable2;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell22;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell23;
        DevExpress.XtraReports.UI.DetailReportBand drParametrosProjeto;
        DevExpress.XtraReports.UI.DetailBand Detail2;
        DevExpress.XtraReports.UI.XRTable xrTable3;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell25;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell31;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell32;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell33;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell35;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell36;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow10;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell37;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell38;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell39;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell40;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell41;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell42;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow11;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell43;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell44;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell45;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell46;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell47;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell48;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell49;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell50;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell51;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell52;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell53;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell54;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow13;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell55;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell56;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell57;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell58;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell59;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell60;
        DevExpress.XtraReports.UI.DetailReportBand drAnaliseIndicadores;
        DevExpress.XtraReports.UI.DetailBand Detail3;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        DevExpress.XtraReports.UI.DetailBand Detail4;
        DevExpress.XtraReports.UI.XRTable xrTable5;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow16;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell28;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell27;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        DevExpress.XtraReports.UI.XRTable xrTable4;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow14;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow15;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell24;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell61;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell26;
        DevExpress.XtraReports.UI.DetailReportBand drMonitoramento;
        DevExpress.XtraReports.UI.DetailBand Detail5;
        DevExpress.XtraReports.UI.XRTable xrTable6;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow17;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow18;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell30;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow19;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell62;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow27;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell70;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow28;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell71;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow29;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell72;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow26;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell69;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow20;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell63;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow25;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell68;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow24;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell67;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow22;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell65;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow23;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell66;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow21;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell64;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport2;
        DevExpress.XtraReports.UI.DetailBand Detail6;
        DevExpress.XtraReports.UI.XRTable xrTable8;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow32;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell74;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell77;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        DevExpress.XtraReports.UI.XRTable xrTable7;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow31;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell76;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell78;
        DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        DevExpress.XtraReports.UI.XRTable xrTable9;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow33;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell75;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell79;
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRTable xrTable21;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow57;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell142;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell143;
        DevExpress.XtraReports.UI.XRTable xrTable20;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow56;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell105;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell141;
        DevExpress.XtraReports.UI.XRTable xrTable22;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow58;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell144;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell145;
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.DetailReportBand drARealizar;
        DevExpress.XtraReports.UI.DetailBand Detail7;
        DevExpress.XtraReports.UI.XRTable xrTable10;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow35;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell80;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow37;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell83;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow38;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell84;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow39;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell85;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        DevExpress.XtraReports.UI.DetailBand Detail8;
        DevExpress.XtraReports.UI.XRTable xrTable12;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow42;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell86;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell94;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell87;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell89;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader3;
        DevExpress.XtraReports.UI.XRTable xrTable11;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow40;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell88;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow41;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell90;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell91;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell92;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell93;
        DevExpress.XtraReports.UI.DetailReportBand drSustentacao;
        DevExpress.XtraReports.UI.DetailBand Detail9;
        DevExpress.XtraReports.UI.XRPanel xrPanel1;
        DevExpress.XtraReports.UI.XRLabel xrLabel7;
        DevExpress.XtraReports.UI.XRLabel xrLabel6;
        DevExpress.XtraReports.UI.XRLabel xrLabel5;
        DevExpress.XtraReports.UI.XRLabel xrLabel4;
        DevExpress.XtraReports.UI.XRCheckBox xrCheckBox6;
        DevExpress.XtraReports.UI.XRCheckBox xrCheckBox5;
        DevExpress.XtraReports.UI.XRCheckBox xrCheckBox4;
        DevExpress.XtraReports.UI.XRLabel xrLabel3;
        DevExpress.XtraReports.UI.XRCheckBox xrCheckBox3;
        DevExpress.XtraReports.UI.XRCheckBox xrCheckBox2;
        DevExpress.XtraReports.UI.XRCheckBox xrCheckBox1;
        DevExpress.XtraReports.UI.XRLabel xrLabel2;
        DevExpress.XtraReports.UI.XRLabel xrLabel1;
        DevExpress.XtraReports.UI.DetailReportBand drDesvios;
        DevExpress.XtraReports.UI.DetailBand Detail10;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport3;
        DevExpress.XtraReports.UI.DetailBand Detail11;
        DevExpress.XtraReports.UI.XRTable xrTable14;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow45;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell102;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell106;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell103;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell107;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader5;
        DevExpress.XtraReports.UI.XRTable xrTable13;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow43;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell97;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow44;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell98;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell96;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell99;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell101;
        DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        DevExpress.XtraReports.UI.XRLabel xrLabel8;
        DevExpress.XtraReports.UI.DetailReportBand drControleMudancas;
        DevExpress.XtraReports.UI.DetailBand Detail12;
        DevExpress.XtraReports.UI.XRTable xrTable15;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow46;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell110;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow47;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell111;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell112;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell108;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell113;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell118;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow48;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell114;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell115;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell109;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell116;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell119;
        DevExpress.XtraReports.UI.DetailReportBand drEntregas;
        DevExpress.XtraReports.UI.DetailBand Detail13;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport4;
        DevExpress.XtraReports.UI.DetailBand Detail15;
        DevExpress.XtraReports.UI.XRTable xrTable18;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow53;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell127;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell128;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell129;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader4;
        DevExpress.XtraReports.UI.XRTable xrTable16;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow49;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell121;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow50;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell122;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell123;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell124;
        DevExpress.XtraReports.UI.DetailReportBand drDestinatarios;
        DevExpress.XtraReports.UI.DetailBand Detail14;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport5;
        DevExpress.XtraReports.UI.DetailBand Detail16;
        DevExpress.XtraReports.UI.XRTable xrTable19;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow54;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell130;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell131;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell132;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader6;
        DevExpress.XtraReports.UI.XRTable xrTable17;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow51;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell117;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow52;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell120;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell125;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell126;
        DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        this.ds = new dsRelatorioAcompanhamento();
        this.xrTableRow55 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell136 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell133 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell137 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell138 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell134 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell135 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.detailReportBand1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail17 = new DevExpress.XtraReports.UI.DetailBand();
        this.GroupHeader7 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell139 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell140 = new DevExpress.XtraReports.UI.XRTableCell();
        this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
        this.Header = new DevExpress.XtraReports.UI.XRControlStyle();
        this.TextControlStyle = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.PagaInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        Detail = new DevExpress.XtraReports.UI.DetailBand();
        TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        drIdentificaoProjeto = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        drParametrosProjeto = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        drAnaliseIndicadores = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        drMonitoramento = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow33 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTable21 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow57 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell142 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell143 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTable20 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow56 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell141 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTable22 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow58 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell144 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell145 = new DevExpress.XtraReports.UI.XRTableCell();
        drARealizar = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail7 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow37 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow38 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow39 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
        DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail8 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable12 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow42 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrTable11 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow40 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow41 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
        drSustentacao = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail9 = new DevExpress.XtraReports.UI.DetailBand();
        xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        xrCheckBox6 = new DevExpress.XtraReports.UI.XRCheckBox();
        xrCheckBox5 = new DevExpress.XtraReports.UI.XRCheckBox();
        xrCheckBox4 = new DevExpress.XtraReports.UI.XRCheckBox();
        xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        xrCheckBox3 = new DevExpress.XtraReports.UI.XRCheckBox();
        xrCheckBox2 = new DevExpress.XtraReports.UI.XRCheckBox();
        xrCheckBox1 = new DevExpress.XtraReports.UI.XRCheckBox();
        xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        drDesvios = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail10 = new DevExpress.XtraReports.UI.DetailBand();
        DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail11 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable14 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow45 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrTable13 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow43 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow44 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        drControleMudancas = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail12 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable15 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow46 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow47 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell108 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell113 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow48 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell114 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell115 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell119 = new DevExpress.XtraReports.UI.XRTableCell();
        drEntregas = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail13 = new DevExpress.XtraReports.UI.DetailBand();
        DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail15 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable18 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow53 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell127 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell128 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell129 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrTable16 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow49 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow50 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell122 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell123 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
        drDestinatarios = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail14 = new DevExpress.XtraReports.UI.DetailBand();
        DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail16 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable19 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow54 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell130 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell131 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell132 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrTable17 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow51 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell117 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableRow52 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell120 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell125 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell126 = new DevExpress.XtraReports.UI.XRTableCell();
        BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable21)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable20)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable22)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable19)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        Detail.Dpi = 254F;
        Detail.Expanded = false;
        Detail.HeightF = 0F;
        Detail.Name = "Detail";
        Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        TopMargin.Dpi = 254F;
        TopMargin.HeightF = 99F;
        TopMargin.Name = "TopMargin";
        TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable1});
        PageHeader.Dpi = 254F;
        PageHeader.HeightF = 250F;
        PageHeader.Name = "PageHeader";
        // 
        // xrTable1
        // 
        xrTable1.Dpi = 254F;
        xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 40F);
        xrTable1.Name = "xrTable1";
        xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow1});
        xrTable1.SizeF = new System.Drawing.SizeF(1900F, 160F);
        xrTable1.StyleName = "Title";
        // 
        // xrTableRow1
        // 
        xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell2,
            xrTableCell3});
        xrTableRow1.Dpi = 254F;
        xrTableRow1.Name = "xrTableRow1";
        xrTableRow1.Weight = 1.0666666666666667D;
        // 
        // xrTableCell2
        // 
        xrTableCell2.Dpi = 254F;
        xrTableCell2.Name = "xrTableCell2";
        xrTableCell2.StylePriority.UseTextAlignment = false;
        xrTableCell2.Text = "Relatório de Acompanhamento do Projeto";
        xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        xrTableCell2.Weight = 2.6052631257709704D;
        // 
        // xrTableCell3
        // 
        xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrPictureBox1});
        xrTableCell3.Dpi = 254F;
        xrTableCell3.Name = "xrTableCell3";
        xrTableCell3.StylePriority.UseTextAlignment = false;
        xrTableCell3.Weight = 0.39473687422902959D;
        // 
        // xrPictureBox1
        // 
        xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrPictureBox1.Dpi = 254F;
        xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 5F);
        xrPictureBox1.Name = "xrPictureBox1";
        xrPictureBox1.SizeF = new System.Drawing.SizeF(150F, 150F);
        xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // drIdentificaoProjeto
        // 
        drIdentificaoProjeto.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail1});
        drIdentificaoProjeto.DataMember = "pbh_RelatorioAcompanhamento";
        drIdentificaoProjeto.DataSource = this.ds;
        drIdentificaoProjeto.Dpi = 254F;
        drIdentificaoProjeto.Level = 0;
        drIdentificaoProjeto.Name = "drIdentificaoProjeto";
        // 
        // Detail1
        // 
        Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable2});
        Detail1.Dpi = 254F;
        Detail1.HeightF = 300F;
        Detail1.Name = "Detail1";
        // 
        // xrTable2
        // 
        xrTable2.Dpi = 254F;
        xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable2.Name = "xrTable2";
        xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow3,
            xrTableRow4,
            xrTableRow5,
            xrTableRow6,
            xrTableRow2});
        xrTable2.SizeF = new System.Drawing.SizeF(1900F, 250F);
        // 
        // xrTableRow3
        // 
        xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell19});
        xrTableRow3.Dpi = 254F;
        xrTableRow3.Name = "xrTableRow3";
        xrTableRow3.StyleName = "Header";
        xrTableRow3.Weight = 1D;
        // 
        // xrTableCell19
        // 
        xrTableCell19.Dpi = 254F;
        xrTableCell19.Name = "xrTableCell19";
        xrTableCell19.Text = "1. IDENTIFICAÇÃO DO PROJETO";
        xrTableCell19.Weight = 3D;
        // 
        // xrTableRow4
        // 
        xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell10,
            xrTableCell11,
            xrTableCell12,
            xrTableCell20});
        xrTableRow4.Dpi = 254F;
        xrTableRow4.Name = "xrTableRow4";
        xrTableRow4.Weight = 1D;
        // 
        // xrTableCell10
        // 
        xrTableCell10.Dpi = 254F;
        xrTableCell10.Name = "xrTableCell10";
        xrTableCell10.StyleName = "Header";
        xrTableCell10.Text = "Código do projeto";
        xrTableCell10.Weight = 0.8052632382041528D;
        // 
        // xrTableCell11
        // 
        xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.CodigoReservado")});
        xrTableCell11.Dpi = 254F;
        xrTableCell11.Name = "xrTableCell11";
        xrTableCell11.StyleName = "Text";
        xrTableCell11.Weight = 0.69473677785773025D;
        // 
        // xrTableCell12
        // 
        xrTableCell12.Dpi = 254F;
        xrTableCell12.Name = "xrTableCell12";
        xrTableCell12.StyleName = "Header";
        xrTableCell12.Text = "Data de elaboração";
        xrTableCell12.Weight = 0.75000001606188316D;
        // 
        // xrTableCell20
        // 
        xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.DataElaboracao", "{0:dd/MM/yyyy}")});
        xrTableCell20.Dpi = 254F;
        xrTableCell20.Name = "xrTableCell20";
        xrTableCell20.StyleName = "Text";
        xrTableCell20.Text = "xrTableCell20";
        xrTableCell20.Weight = 0.74999996787623358D;
        // 
        // xrTableRow5
        // 
        xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell13,
            xrTableCell21});
        xrTableRow5.Dpi = 254F;
        xrTableRow5.Name = "xrTableRow5";
        xrTableRow5.Weight = 1D;
        // 
        // xrTableCell13
        // 
        xrTableCell13.Dpi = 254F;
        xrTableCell13.Name = "xrTableCell13";
        xrTableCell13.StyleName = "Header";
        xrTableCell13.Text = "Projeto/subprojeto";
        xrTableCell13.Weight = 0.80526314183285364D;
        // 
        // xrTableCell21
        // 
        xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.NomeProjeto")});
        xrTableCell21.Dpi = 254F;
        xrTableCell21.Name = "xrTableCell21";
        xrTableCell21.StyleName = "Text";
        xrTableCell21.Text = "xrTableCell21";
        xrTableCell21.Weight = 2.1947368581671465D;
        // 
        // xrTableRow6
        // 
        xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell16,
            xrTableCell17,
            xrTableCell18,
            xrTableCell22});
        xrTableRow6.Dpi = 254F;
        xrTableRow6.Name = "xrTableRow6";
        xrTableRow6.Weight = 1D;
        // 
        // xrTableCell16
        // 
        xrTableCell16.Dpi = 254F;
        xrTableCell16.Name = "xrTableCell16";
        xrTableCell16.StyleName = "Header";
        xrTableCell16.Text = "Gestor do projeto";
        xrTableCell16.Weight = 0.80526314183285364D;
        // 
        // xrTableCell17
        // 
        xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.NomeGerente")});
        xrTableCell17.Dpi = 254F;
        xrTableCell17.Name = "xrTableCell17";
        xrTableCell17.StyleName = "Text";
        xrTableCell17.Text = "xrTableCell17";
        xrTableCell17.Weight = 0.69473692241467921D;
        // 
        // xrTableCell18
        // 
        xrTableCell18.Dpi = 254F;
        xrTableCell18.Name = "xrTableCell18";
        xrTableCell18.StyleName = "Header";
        xrTableCell18.Text = "Unidade funcional";
        xrTableCell18.Weight = 0.74999996787623358D;
        // 
        // xrTableCell22
        // 
        xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.SiglaUnidadeNegocio")});
        xrTableCell22.Dpi = 254F;
        xrTableCell22.Name = "xrTableCell22";
        xrTableCell22.StyleName = "Text";
        xrTableCell22.Text = "xrTableCell22";
        xrTableCell22.Weight = 0.74999996787623358D;
        // 
        // xrTableRow2
        // 
        xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell4,
            xrTableCell5,
            xrTableCell6,
            xrTableCell23});
        xrTableRow2.Dpi = 254F;
        xrTableRow2.Name = "xrTableRow2";
        xrTableRow2.Weight = 1D;
        // 
        // xrTableCell4
        // 
        xrTableCell4.Dpi = 254F;
        xrTableCell4.Name = "xrTableCell4";
        xrTableCell4.StyleName = "Header";
        xrTableCell4.Text = "Data de Início Planejada";
        xrTableCell4.Weight = 0.80526314183285364D;
        // 
        // xrTableCell5
        // 
        xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.DataInicioPlanejada", "{0:dd/MM/yyyy}")});
        xrTableCell5.Dpi = 254F;
        xrTableCell5.Name = "xrTableCell5";
        xrTableCell5.StyleName = "Text";
        xrTableCell5.Text = "xrTableCell5";
        xrTableCell5.Weight = 0.69473692241467921D;
        // 
        // xrTableCell6
        // 
        xrTableCell6.Dpi = 254F;
        xrTableCell6.Name = "xrTableCell6";
        xrTableCell6.StyleName = "Header";
        xrTableCell6.Text = "Data de Início Real";
        xrTableCell6.Weight = 0.74999996787623358D;
        // 
        // xrTableCell23
        // 
        xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.DataInicioReal", "{0:dd/MM/yyyy}")});
        xrTableCell23.Dpi = 254F;
        xrTableCell23.Name = "xrTableCell23";
        xrTableCell23.StyleName = "Text";
        xrTableCell23.Text = "xrTableCell23";
        xrTableCell23.Weight = 0.74999996787623358D;
        // 
        // ds
        // 
        this.ds.DataSetName = "dsRelatorioAcompanhamento";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // drParametrosProjeto
        // 
        drParametrosProjeto.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail2});
        drParametrosProjeto.DataMember = "pbh_RelatorioAcompanhamento";
        drParametrosProjeto.DataSource = this.ds;
        drParametrosProjeto.Dpi = 254F;
        drParametrosProjeto.Expanded = false;
        drParametrosProjeto.Level = 1;
        drParametrosProjeto.Name = "drParametrosProjeto";
        // 
        // Detail2
        // 
        Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable3});
        Detail2.Dpi = 254F;
        Detail2.HeightF = 850F;
        Detail2.Name = "Detail2";
        // 
        // xrTable3
        // 
        xrTable3.Dpi = 254F;
        xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable3.Name = "xrTable3";
        xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow7,
            xrTableRow8,
            xrTableRow9,
            this.xrTableRow55,
            xrTableRow10,
            xrTableRow11,
            xrTableRow12,
            xrTableRow13});
        xrTable3.SizeF = new System.Drawing.SizeF(1900F, 800F);
        // 
        // xrTableRow7
        // 
        xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell7});
        xrTableRow7.Dpi = 254F;
        xrTableRow7.Name = "xrTableRow7";
        xrTableRow7.StyleName = "Header";
        xrTableRow7.Weight = 0.99714289113435928D;
        // 
        // xrTableCell7
        // 
        xrTableCell7.Dpi = 254F;
        xrTableCell7.Name = "xrTableCell7";
        xrTableCell7.Text = "2. PARÂMETROS DO PROJETO";
        xrTableCell7.Weight = 3D;
        // 
        // xrTableRow8
        // 
        xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell25});
        xrTableRow8.Dpi = 254F;
        xrTableRow8.Name = "xrTableRow8";
        xrTableRow8.StyleName = "Header";
        xrTableRow8.Weight = 0.99714289113435928D;
        // 
        // xrTableCell25
        // 
        xrTableCell25.Dpi = 254F;
        xrTableCell25.Name = "xrTableCell25";
        xrTableCell25.Text = "2.1 DADOS DO PROJETO";
        xrTableCell25.Weight = 3D;
        // 
        // xrTableRow9
        // 
        xrTableRow9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell31,
            xrTableCell32,
            xrTableCell33,
            xrTableCell34,
            xrTableCell35,
            xrTableCell36});
        xrTableRow9.Dpi = 254F;
        xrTableRow9.Name = "xrTableRow9";
        xrTableRow9.StyleName = "Header";
        xrTableRow9.StylePriority.UseBorders = false;
        xrTableRow9.Weight = 0.997142921604416D;
        // 
        // xrTableCell31
        // 
        xrTableCell31.Dpi = 254F;
        xrTableCell31.Name = "xrTableCell31";
        xrTableCell31.Text = "Unidades";
        xrTableCell31.Weight = 0.47368422658819903D;
        // 
        // xrTableCell32
        // 
        xrTableCell32.Dpi = 254F;
        xrTableCell32.Name = "xrTableCell32";
        xrTableCell32.Text = "Planejado";
        xrTableCell32.Weight = 0.82894738448293581D;
        // 
        // xrTableCell33
        // 
        xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell33.Dpi = 254F;
        xrTableCell33.Name = "xrTableCell33";
        xrTableCell33.StylePriority.UseBorders = false;
        xrTableCell33.StylePriority.UseTextAlignment = false;
        xrTableCell33.Text = "Progresso";
        xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        xrTableCell33.Weight = 0.47368420249537435D;
        // 
        // xrTableCell34
        // 
        xrTableCell34.Dpi = 254F;
        xrTableCell34.Name = "xrTableCell34";
        xrTableCell34.Text = "Tendência de término";
        xrTableCell34.Weight = 0.59209995470548915D;
        // 
        // xrTableCell35
        // 
        xrTableCell35.Dpi = 254F;
        xrTableCell35.Name = "xrTableCell35";
        xrTableCell35.Text = "Status";
        xrTableCell35.Weight = 0.315789465653269D;
        // 
        // xrTableCell36
        // 
        xrTableCell36.Dpi = 254F;
        xrTableCell36.Name = "xrTableCell36";
        xrTableCell36.Text = "Variação";
        xrTableCell36.Weight = 0.31579476607473272D;
        // 
        // xrTableRow55
        // 
        this.xrTableRow55.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell136,
            this.xrTableCell104,
            this.xrTableCell100,
            this.xrTableCell133,
            this.xrTableCell137,
            this.xrTableCell138,
            this.xrTableCell134});
        this.xrTableRow55.Dpi = 254F;
        this.xrTableRow55.Name = "xrTableRow55";
        this.xrTableRow55.StyleName = "Header";
        this.xrTableRow55.Weight = 0.99714289113435928D;
        // 
        // xrTableCell136
        // 
        this.xrTableCell136.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell136.Dpi = 254F;
        this.xrTableCell136.Name = "xrTableCell136";
        this.xrTableCell136.StylePriority.UseBorders = false;
        this.xrTableCell136.Weight = 0.47368422658819903D;
        // 
        // xrTableCell104
        // 
        this.xrTableCell104.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell104.Dpi = 254F;
        this.xrTableCell104.Name = "xrTableCell104";
        this.xrTableCell104.StylePriority.UseBorders = false;
        this.xrTableCell104.Weight = 0.82894738448293581D;
        // 
        // xrTableCell100
        // 
        this.xrTableCell100.Dpi = 254F;
        this.xrTableCell100.Name = "xrTableCell100";
        this.xrTableCell100.StylePriority.UseTextAlignment = false;
        this.xrTableCell100.Text = "Anterior";
        this.xrTableCell100.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell100.Weight = 0.23684210124768718D;
        // 
        // xrTableCell133
        // 
        this.xrTableCell133.Dpi = 254F;
        this.xrTableCell133.Name = "xrTableCell133";
        this.xrTableCell133.StylePriority.UseTextAlignment = false;
        this.xrTableCell133.Text = "Atual";
        this.xrTableCell133.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell133.Weight = 0.23684210124768718D;
        // 
        // xrTableCell137
        // 
        this.xrTableCell137.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell137.Dpi = 254F;
        this.xrTableCell137.Name = "xrTableCell137";
        this.xrTableCell137.StylePriority.UseBorders = false;
        this.xrTableCell137.Weight = 0.59209985431871914D;
        // 
        // xrTableCell138
        // 
        this.xrTableCell138.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell138.Dpi = 254F;
        this.xrTableCell138.Name = "xrTableCell138";
        this.xrTableCell138.StylePriority.UseBorders = false;
        this.xrTableCell138.Weight = 0.31579216605738586D;
        // 
        // xrTableCell134
        // 
        this.xrTableCell134.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell134.Dpi = 254F;
        this.xrTableCell134.Name = "xrTableCell134";
        this.xrTableCell134.StylePriority.UseBorders = false;
        this.xrTableCell134.Weight = 0.31579216605738586D;
        // 
        // xrTableRow10
        // 
        xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell37,
            xrTableCell38,
            this.xrTableCell135,
            xrTableCell39,
            xrTableCell40,
            xrTableCell41,
            xrTableCell42});
        xrTableRow10.Dpi = 254F;
        xrTableRow10.Name = "xrTableRow10";
        xrTableRow10.StyleName = "Text";
        xrTableRow10.Weight = 0.99714289113435928D;
        // 
        // xrTableCell37
        // 
        xrTableCell37.Dpi = 254F;
        xrTableCell37.Name = "xrTableCell37";
        xrTableCell37.Text = "Término (data)";
        xrTableCell37.Weight = 0.47368422658819903D;
        // 
        // xrTableCell38
        // 
        xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.TerminoPlanejado")});
        xrTableCell38.Dpi = 254F;
        xrTableCell38.Name = "xrTableCell38";
        xrTableCell38.Text = "xrTableCell38";
        xrTableCell38.Weight = 0.82894738448293581D;
        // 
        // xrTableCell135
        // 
        this.xrTableCell135.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ProgressoFisicoAnterior", "{0}%")});
        this.xrTableCell135.Dpi = 254F;
        this.xrTableCell135.Name = "xrTableCell135";
        this.xrTableCell135.StylePriority.UseTextAlignment = false;
        this.xrTableCell135.Text = "xrTableCell135";
        this.xrTableCell135.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell135.Weight = 0.23684210124768718D;
        // 
        // xrTableCell39
        // 
        xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ProgressoFisico", "{0}%")});
        xrTableCell39.Dpi = 254F;
        xrTableCell39.Name = "xrTableCell39";
        xrTableCell39.StylePriority.UseTextAlignment = false;
        xrTableCell39.Text = "xrTableCell39";
        xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        xrTableCell39.Weight = 0.23684210124768718D;
        // 
        // xrTableCell40
        // 
        xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.TendenciaTermino", "{0:dd/MM/yyyy}")});
        xrTableCell40.Dpi = 254F;
        xrTableCell40.Name = "xrTableCell40";
        xrTableCell40.Text = "xrTableCell40";
        xrTableCell40.Weight = 0.59209995470548915D;
        // 
        // xrTableCell41
        // 
        xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.StatusTermino")});
        xrTableCell41.Dpi = 254F;
        xrTableCell41.Name = "xrTableCell41";
        xrTableCell41.Text = "xrTableCell41";
        xrTableCell41.Weight = 0.315789465653269D;
        // 
        // xrTableCell42
        // 
        xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.VariacaoTermino")});
        xrTableCell42.Dpi = 254F;
        xrTableCell42.Name = "xrTableCell42";
        xrTableCell42.Weight = 0.31579476607473272D;
        // 
        // xrTableRow11
        // 
        xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell43,
            xrTableCell44,
            xrTableCell45,
            xrTableCell46,
            xrTableCell47,
            xrTableCell48});
        xrTableRow11.Dpi = 254F;
        xrTableRow11.Name = "xrTableRow11";
        xrTableRow11.StyleName = "Text";
        xrTableRow11.Weight = 0.99714288142173313D;
        // 
        // xrTableCell43
        // 
        xrTableCell43.Dpi = 254F;
        xrTableCell43.Name = "xrTableCell43";
        xrTableCell43.Text = "Tamanho (PF)";
        xrTableCell43.Weight = 0.47368422658819903D;
        // 
        // xrTableCell44
        // 
        xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.PontoFuncaoPlanejado")});
        xrTableCell44.Dpi = 254F;
        xrTableCell44.Name = "xrTableCell44";
        xrTableCell44.Text = "xrTableCell44";
        xrTableCell44.Weight = 0.82894738448293581D;
        // 
        // xrTableCell45
        // 
        xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.PontoFuncaoProgresso")});
        xrTableCell45.Dpi = 254F;
        xrTableCell45.Name = "xrTableCell45";
        xrTableCell45.Text = "xrTableCell45";
        xrTableCell45.Weight = 0.47368420249537435D;
        // 
        // xrTableCell46
        // 
        xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.PontoFuncaoTendencia")});
        xrTableCell46.Dpi = 254F;
        xrTableCell46.Name = "xrTableCell46";
        xrTableCell46.Text = "xrTableCell46";
        xrTableCell46.Weight = 0.59209995470548915D;
        // 
        // xrTableCell47
        // 
        xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.PontoFuncaoStatus")});
        xrTableCell47.Dpi = 254F;
        xrTableCell47.Name = "xrTableCell47";
        xrTableCell47.Text = "xrTableCell47";
        xrTableCell47.Weight = 0.315789465653269D;
        // 
        // xrTableCell48
        // 
        xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.VariacaoPontoFuncao")});
        xrTableCell48.Dpi = 254F;
        xrTableCell48.Name = "xrTableCell48";
        xrTableCell48.Weight = 0.31579476607473272D;
        // 
        // xrTableRow12
        // 
        xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell49,
            xrTableCell50,
            xrTableCell51,
            xrTableCell52,
            xrTableCell53,
            xrTableCell54});
        xrTableRow12.Dpi = 254F;
        xrTableRow12.Name = "xrTableRow12";
        xrTableRow12.StyleName = "Text";
        xrTableRow12.Weight = 4.9857143519365668D;
        // 
        // xrTableCell49
        // 
        xrTableCell49.Dpi = 254F;
        xrTableCell49.Name = "xrTableCell49";
        xrTableCell49.Text = "Investimento (R$)";
        xrTableCell49.Weight = 0.47368422658819903D;
        // 
        // xrTableCell50
        // 
        xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.InvestimentoPlanejado")});
        xrTableCell50.Dpi = 254F;
        xrTableCell50.Name = "xrTableCell50";
        xrTableCell50.Text = "xrTableCell50";
        xrTableCell50.Weight = 0.82894738448293581D;
        // 
        // xrTableCell51
        // 
        xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.InvestimentoProgresso")});
        xrTableCell51.Dpi = 254F;
        xrTableCell51.Name = "xrTableCell51";
        xrTableCell51.Text = "xrTableCell51";
        xrTableCell51.Weight = 0.47368420249537435D;
        // 
        // xrTableCell52
        // 
        xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.InvestimentoTendencia")});
        xrTableCell52.Dpi = 254F;
        xrTableCell52.Name = "xrTableCell52";
        xrTableCell52.Text = "xrTableCell52";
        xrTableCell52.Weight = 0.59209995470548915D;
        // 
        // xrTableCell53
        // 
        xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.InvestimentoStatus")});
        xrTableCell53.Dpi = 254F;
        xrTableCell53.Name = "xrTableCell53";
        xrTableCell53.Text = "xrTableCell53";
        xrTableCell53.Weight = 0.315789465653269D;
        // 
        // xrTableCell54
        // 
        xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.VaricaoInvestimento")});
        xrTableCell54.Dpi = 254F;
        xrTableCell54.Name = "xrTableCell54";
        xrTableCell54.Weight = 0.31579476607473272D;
        // 
        // xrTableRow13
        // 
        xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell55,
            xrTableCell56,
            xrTableCell57,
            xrTableCell58,
            xrTableCell59,
            xrTableCell60});
        xrTableRow13.Dpi = 254F;
        xrTableRow13.Name = "xrTableRow13";
        xrTableRow13.StyleName = "Text";
        xrTableRow13.Weight = 4.9857141237086768D;
        // 
        // xrTableCell55
        // 
        xrTableCell55.Dpi = 254F;
        xrTableCell55.Name = "xrTableCell55";
        xrTableCell55.Text = "Custeio (R$)";
        xrTableCell55.Weight = 0.47368422658819903D;
        // 
        // xrTableCell56
        // 
        xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.CusteioPlanejado")});
        xrTableCell56.Dpi = 254F;
        xrTableCell56.Name = "xrTableCell56";
        xrTableCell56.Text = "xrTableCell56";
        xrTableCell56.Weight = 0.82894738448293581D;
        // 
        // xrTableCell57
        // 
        xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.CusteioProgresso")});
        xrTableCell57.Dpi = 254F;
        xrTableCell57.Name = "xrTableCell57";
        xrTableCell57.Text = "xrTableCell57";
        xrTableCell57.Weight = 0.47368420249537435D;
        // 
        // xrTableCell58
        // 
        xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.CusteioTendencia")});
        xrTableCell58.Dpi = 254F;
        xrTableCell58.Name = "xrTableCell58";
        xrTableCell58.Text = "xrTableCell58";
        xrTableCell58.Weight = 0.59209995470548915D;
        // 
        // xrTableCell59
        // 
        xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.CusteioStatus")});
        xrTableCell59.Dpi = 254F;
        xrTableCell59.Name = "xrTableCell59";
        xrTableCell59.Text = "xrTableCell59";
        xrTableCell59.Weight = 0.315789465653269D;
        // 
        // xrTableCell60
        // 
        xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.VariacaoCusteio")});
        xrTableCell60.Dpi = 254F;
        xrTableCell60.Name = "xrTableCell60";
        xrTableCell60.Weight = 0.31579476607473272D;
        // 
        // drAnaliseIndicadores
        // 
        drAnaliseIndicadores.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail3,
            DetailReport1});
        drAnaliseIndicadores.DataMember = "pbh_RelatorioAcompanhamento";
        drAnaliseIndicadores.DataSource = this.ds;
        drAnaliseIndicadores.Dpi = 254F;
        drAnaliseIndicadores.Expanded = false;
        drAnaliseIndicadores.Level = 2;
        drAnaliseIndicadores.Name = "drAnaliseIndicadores";
        // 
        // Detail3
        // 
        Detail3.Dpi = 254F;
        Detail3.HeightF = 0F;
        Detail3.Name = "Detail3";
        // 
        // DetailReport1
        // 
        DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail4,
            GroupHeader1});
        DetailReport1.DataMember = "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoIndicadores_pbh_Relator" +
"ioAcompanhamento";
        DetailReport1.DataSource = this.ds;
        DetailReport1.Dpi = 254F;
        DetailReport1.Level = 0;
        DetailReport1.Name = "DetailReport1";
        // 
        // Detail4
        // 
        Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable5});
        Detail4.Dpi = 254F;
        Detail4.HeightF = 50F;
        Detail4.Name = "Detail4";
        // 
        // xrTable5
        // 
        xrTable5.Dpi = 254F;
        xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable5.Name = "xrTable5";
        xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow16});
        xrTable5.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable5.StyleName = "Text";
        // 
        // xrTableRow16
        // 
        xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell9,
            xrTableCell14,
            xrTableCell28,
            xrTableCell27});
        xrTableRow16.Dpi = 254F;
        xrTableRow16.Name = "xrTableRow16";
        xrTableRow16.Weight = 1D;
        // 
        // xrTableCell9
        // 
        xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoIndicadores_pbh_Relator" +
                    "ioAcompanhamento.NomeIndicador")});
        xrTableCell9.Dpi = 254F;
        xrTableCell9.Name = "xrTableCell9";
        xrTableCell9.Text = "xrTableCell9";
        xrTableCell9.Weight = 0.86842108475534541D;
        // 
        // xrTableCell14
        // 
        xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoIndicadores_pbh_Relator" +
                    "ioAcompanhamento.Previsto")});
        xrTableCell14.Dpi = 254F;
        xrTableCell14.Name = "xrTableCell14";
        xrTableCell14.Text = "xrTableCell14";
        xrTableCell14.Weight = 0.39473685816714632D;
        // 
        // xrTableCell28
        // 
        xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoIndicadores_pbh_Relator" +
                    "ioAcompanhamento.Realizado")});
        xrTableCell28.Dpi = 254F;
        xrTableCell28.Name = "xrTableCell28";
        xrTableCell28.Text = "xrTableCell28";
        xrTableCell28.Weight = 0.39473685816714632D;
        // 
        // xrTableCell27
        // 
        xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoIndicadores_pbh_Relator" +
                    "ioAcompanhamento.Comentarios")});
        xrTableCell27.Dpi = 254F;
        xrTableCell27.Name = "xrTableCell27";
        xrTableCell27.Text = "xrTableCell27";
        xrTableCell27.Weight = 1.3421051989103618D;
        // 
        // GroupHeader1
        // 
        GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable4});
        GroupHeader1.Dpi = 254F;
        GroupHeader1.HeightF = 150F;
        GroupHeader1.Name = "GroupHeader1";
        // 
        // xrTable4
        // 
        xrTable4.Dpi = 254F;
        xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable4.Name = "xrTable4";
        xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow14,
            xrTableRow15});
        xrTable4.SizeF = new System.Drawing.SizeF(1900F, 150F);
        xrTable4.StyleName = "Text";
        // 
        // xrTableRow14
        // 
        xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell8});
        xrTableRow14.Dpi = 254F;
        xrTableRow14.Name = "xrTableRow14";
        xrTableRow14.StyleName = "Header";
        xrTableRow14.Weight = 1D;
        // 
        // xrTableCell8
        // 
        xrTableCell8.Dpi = 254F;
        xrTableCell8.Name = "xrTableCell8";
        xrTableCell8.Text = "2.2 ANÁLISE DE INDICADORES";
        xrTableCell8.Weight = 3D;
        // 
        // xrTableRow15
        // 
        xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell15,
            xrTableCell24,
            xrTableCell61,
            xrTableCell26});
        xrTableRow15.Dpi = 254F;
        xrTableRow15.Name = "xrTableRow15";
        xrTableRow15.StyleName = "Header";
        xrTableRow15.Weight = 2D;
        // 
        // xrTableCell15
        // 
        xrTableCell15.Dpi = 254F;
        xrTableCell15.Name = "xrTableCell15";
        xrTableCell15.Text = "Indicador";
        xrTableCell15.Weight = 0.86842108475534541D;
        // 
        // xrTableCell24
        // 
        xrTableCell24.Dpi = 254F;
        xrTableCell24.Name = "xrTableCell24";
        xrTableCell24.Text = "% planejado";
        xrTableCell24.Weight = 0.39473685816714632D;
        // 
        // xrTableCell61
        // 
        xrTableCell61.Dpi = 254F;
        xrTableCell61.Name = "xrTableCell61";
        xrTableCell61.Text = "% alcançado";
        xrTableCell61.Weight = 0.39473685816714632D;
        // 
        // xrTableCell26
        // 
        xrTableCell26.Dpi = 254F;
        xrTableCell26.Multiline = true;
        xrTableCell26.Name = "xrTableCell26";
        xrTableCell26.Text = "Justificativa\r\n(caso o valor planejado não tenha sido alcançado)\r\n";
        xrTableCell26.Weight = 1.3421051989103616D;
        // 
        // drMonitoramento
        // 
        drMonitoramento.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail5,
            DetailReport2,
            this.detailReportBand1});
        drMonitoramento.DataMember = "pbh_RelatorioAcompanhamento";
        drMonitoramento.DataSource = this.ds;
        drMonitoramento.Dpi = 254F;
        drMonitoramento.Expanded = false;
        drMonitoramento.Level = 3;
        drMonitoramento.Name = "drMonitoramento";
        // 
        // Detail5
        // 
        Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable6});
        Detail5.Dpi = 254F;
        Detail5.HeightF = 750F;
        Detail5.Name = "Detail5";
        // 
        // xrTable6
        // 
        xrTable6.Dpi = 254F;
        xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        xrTable6.Name = "xrTable6";
        xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow17,
            xrTableRow18,
            xrTableRow19,
            xrTableRow27,
            xrTableRow28,
            xrTableRow29,
            xrTableRow26,
            xrTableRow20,
            xrTableRow25,
            xrTableRow24,
            xrTableRow22,
            xrTableRow23,
            xrTableRow21,
            this.xrTableRow30});
        xrTable6.SizeF = new System.Drawing.SizeF(1900F, 700F);
        xrTable6.StyleName = "Text";
        // 
        // xrTableRow17
        // 
        xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell29});
        xrTableRow17.Dpi = 254F;
        xrTableRow17.Name = "xrTableRow17";
        xrTableRow17.Weight = 1D;
        // 
        // xrTableCell29
        // 
        xrTableCell29.Dpi = 254F;
        xrTableCell29.Name = "xrTableCell29";
        xrTableCell29.StyleName = "Header";
        xrTableCell29.Text = "3. MONITORAMENTO";
        xrTableCell29.Weight = 3D;
        // 
        // xrTableRow18
        // 
        xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell30});
        xrTableRow18.Dpi = 254F;
        xrTableRow18.Name = "xrTableRow18";
        xrTableRow18.Weight = 1D;
        // 
        // xrTableCell30
        // 
        xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell30.Dpi = 254F;
        xrTableCell30.Name = "xrTableCell30";
        xrTableCell30.StyleName = "Header";
        xrTableCell30.StylePriority.UseBorders = false;
        xrTableCell30.Text = " 3.1. Prazo";
        xrTableCell30.Weight = 3D;
        // 
        // xrTableRow19
        // 
        xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell62});
        xrTableRow19.Dpi = 254F;
        xrTableRow19.Name = "xrTableRow19";
        xrTableRow19.Weight = 1D;
        // 
        // xrTableCell62
        // 
        xrTableCell62.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioPrazo")});
        xrTableCell62.Dpi = 254F;
        xrTableCell62.Multiline = true;
        xrTableCell62.Name = "xrTableCell62";
        xrTableCell62.StylePriority.UseBorders = false;
        xrTableCell62.Text = "xrTableCell62";
        xrTableCell62.Weight = 3D;
        // 
        // xrTableRow27
        // 
        xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell70});
        xrTableRow27.Dpi = 254F;
        xrTableRow27.Name = "xrTableRow27";
        xrTableRow27.Weight = 1D;
        // 
        // xrTableCell70
        // 
        xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell70.Dpi = 254F;
        xrTableCell70.Name = "xrTableCell70";
        xrTableCell70.StyleName = "Header";
        xrTableCell70.StylePriority.UseBorders = false;
        xrTableCell70.Text = "3.1.1 Motivo do atraso";
        xrTableCell70.Weight = 3D;
        // 
        // xrTableRow28
        // 
        xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell71});
        xrTableRow28.Dpi = 254F;
        xrTableRow28.Name = "xrTableRow28";
        xrTableRow28.Weight = 1D;
        // 
        // xrTableCell71
        // 
        xrTableCell71.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioAtraso")});
        xrTableCell71.Dpi = 254F;
        xrTableCell71.Multiline = true;
        xrTableCell71.Name = "xrTableCell71";
        xrTableCell71.StylePriority.UseBorders = false;
        xrTableCell71.Text = "xrTableCell71";
        xrTableCell71.Weight = 3D;
        // 
        // xrTableRow29
        // 
        xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell72});
        xrTableRow29.Dpi = 254F;
        xrTableRow29.Name = "xrTableRow29";
        xrTableRow29.Weight = 1D;
        // 
        // xrTableCell72
        // 
        xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell72.Dpi = 254F;
        xrTableCell72.Name = "xrTableCell72";
        xrTableCell72.StyleName = "Header";
        xrTableCell72.StylePriority.UseBorders = false;
        xrTableCell72.Text = "3.2. Escopo";
        xrTableCell72.Weight = 3D;
        // 
        // xrTableRow26
        // 
        xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell69});
        xrTableRow26.Dpi = 254F;
        xrTableRow26.Name = "xrTableRow26";
        xrTableRow26.Weight = 1D;
        // 
        // xrTableCell69
        // 
        xrTableCell69.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioEscopo")});
        xrTableCell69.Dpi = 254F;
        xrTableCell69.Multiline = true;
        xrTableCell69.Name = "xrTableCell69";
        xrTableCell69.StylePriority.UseBorders = false;
        xrTableCell69.Text = "xrTableCell69";
        xrTableCell69.Weight = 3D;
        // 
        // xrTableRow20
        // 
        xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell63});
        xrTableRow20.Dpi = 254F;
        xrTableRow20.Name = "xrTableRow20";
        xrTableRow20.Weight = 1D;
        // 
        // xrTableCell63
        // 
        xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell63.Dpi = 254F;
        xrTableCell63.Name = "xrTableCell63";
        xrTableCell63.StyleName = "Header";
        xrTableCell63.StylePriority.UseBorders = false;
        xrTableCell63.Text = "3.3. Custos";
        xrTableCell63.Weight = 3D;
        // 
        // xrTableRow25
        // 
        xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell68});
        xrTableRow25.Dpi = 254F;
        xrTableRow25.Name = "xrTableRow25";
        xrTableRow25.Weight = 1D;
        // 
        // xrTableCell68
        // 
        xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioCusto")});
        xrTableCell68.Dpi = 254F;
        xrTableCell68.Multiline = true;
        xrTableCell68.Name = "xrTableCell68";
        xrTableCell68.StylePriority.UseBorders = false;
        xrTableCell68.Text = "xrTableCell68";
        xrTableCell68.Weight = 3D;
        // 
        // xrTableRow24
        // 
        xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell67});
        xrTableRow24.Dpi = 254F;
        xrTableRow24.Name = "xrTableRow24";
        xrTableRow24.Weight = 1D;
        // 
        // xrTableCell67
        // 
        xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell67.Dpi = 254F;
        xrTableCell67.Name = "xrTableCell67";
        xrTableCell67.StyleName = "Header";
        xrTableCell67.StylePriority.UseBorders = false;
        xrTableCell67.Text = "3.4. Recursos Humanos";
        xrTableCell67.Weight = 3D;
        // 
        // xrTableRow22
        // 
        xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell65});
        xrTableRow22.Dpi = 254F;
        xrTableRow22.Name = "xrTableRow22";
        xrTableRow22.Weight = 1D;
        // 
        // xrTableCell65
        // 
        xrTableCell65.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioRH")});
        xrTableCell65.Dpi = 254F;
        xrTableCell65.Multiline = true;
        xrTableCell65.Name = "xrTableCell65";
        xrTableCell65.StylePriority.UseBorders = false;
        xrTableCell65.Text = "xrTableCell65";
        xrTableCell65.Weight = 3D;
        // 
        // xrTableRow23
        // 
        xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell66});
        xrTableRow23.Dpi = 254F;
        xrTableRow23.Name = "xrTableRow23";
        xrTableRow23.Weight = 1D;
        // 
        // xrTableCell66
        // 
        xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell66.Dpi = 254F;
        xrTableCell66.Name = "xrTableCell66";
        xrTableCell66.StyleName = "Header";
        xrTableCell66.StylePriority.UseBorders = false;
        xrTableCell66.Text = "3.5. Comunicações";
        xrTableCell66.Weight = 3D;
        // 
        // xrTableRow21
        // 
        xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell64});
        xrTableRow21.Dpi = 254F;
        xrTableRow21.Name = "xrTableRow21";
        xrTableRow21.Weight = 1D;
        // 
        // xrTableCell64
        // 
        xrTableCell64.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioComunicacoes")});
        xrTableCell64.Dpi = 254F;
        xrTableCell64.Multiline = true;
        xrTableCell64.Name = "xrTableCell64";
        xrTableCell64.StylePriority.UseBorders = false;
        xrTableCell64.Text = "xrTableCell64";
        xrTableCell64.Weight = 3D;
        // 
        // xrTableRow30
        // 
        this.xrTableRow30.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell73});
        this.xrTableRow30.Dpi = 254F;
        this.xrTableRow30.Name = "xrTableRow30";
        this.xrTableRow30.Weight = 1D;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Dpi = 254F;
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.StyleName = "Header";
        this.xrTableCell73.Text = "3.6. Aquisições";
        this.xrTableCell73.Weight = 3D;
        // 
        // DetailReport2
        // 
        DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail6,
            GroupHeader2,
            GroupFooter1});
        DetailReport2.DataMember = "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
"oAcompanhamento";
        DetailReport2.DataSource = this.ds;
        DetailReport2.Dpi = 254F;
        DetailReport2.FilterString = "[IndicaAquisicaoPendente] = \'N\'";
        DetailReport2.Level = 0;
        DetailReport2.Name = "DetailReport2";
        // 
        // Detail6
        // 
        Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable8});
        Detail6.Dpi = 254F;
        Detail6.HeightF = 50F;
        Detail6.Name = "Detail6";
        // 
        // xrTable8
        // 
        xrTable8.Dpi = 254F;
        xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable8.Name = "xrTable8";
        xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow32});
        xrTable8.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable8.StyleName = "Text";
        // 
        // xrTableRow32
        // 
        xrTableRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell74,
            xrTableCell77});
        xrTableRow32.Dpi = 254F;
        xrTableRow32.Name = "xrTableRow32";
        xrTableRow32.Weight = 1D;
        // 
        // xrTableCell74
        // 
        xrTableCell74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
                    "oAcompanhamento.Aquisicao")});
        xrTableCell74.Dpi = 254F;
        xrTableCell74.Name = "xrTableCell74";
        xrTableCell74.Text = "xrTableCell74";
        xrTableCell74.Weight = 2.3684156879625822D;
        // 
        // xrTableCell77
        // 
        xrTableCell77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
                    "oAcompanhamento.ValorAquisicao", "{0:n2}")});
        xrTableCell77.Dpi = 254F;
        xrTableCell77.Name = "xrTableCell77";
        xrTableCell77.Text = "xrTableCell77";
        xrTableCell77.Weight = 0.63158431203741783D;
        // 
        // GroupHeader2
        // 
        GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable7});
        GroupHeader2.Dpi = 254F;
        GroupHeader2.HeightF = 50F;
        GroupHeader2.Name = "GroupHeader2";
        // 
        // xrTable7
        // 
        xrTable7.Dpi = 254F;
        xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable7.Name = "xrTable7";
        xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow31});
        xrTable7.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable7.StyleName = "Header";
        // 
        // xrTableRow31
        // 
        xrTableRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell76,
            xrTableCell78});
        xrTableRow31.Dpi = 254F;
        xrTableRow31.Name = "xrTableRow31";
        xrTableRow31.Weight = 1D;
        // 
        // xrTableCell76
        // 
        xrTableCell76.Dpi = 254F;
        xrTableCell76.Name = "xrTableCell76";
        xrTableCell76.Text = "Realizadas";
        xrTableCell76.Weight = 2.3684210847553455D;
        // 
        // xrTableCell78
        // 
        xrTableCell78.Dpi = 254F;
        xrTableCell78.Name = "xrTableCell78";
        xrTableCell78.Text = "Valor (R$)";
        xrTableCell78.Weight = 0.6315789152446547D;
        // 
        // GroupFooter1
        // 
        GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable9});
        GroupFooter1.Dpi = 254F;
        GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable9
        // 
        xrTable9.Dpi = 254F;
        xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable9.Name = "xrTable9";
        xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow33});
        xrTable9.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable9.StyleName = "Header";
        // 
        // xrTableRow33
        // 
        xrTableRow33.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell75,
            xrTableCell79});
        xrTableRow33.Dpi = 254F;
        xrTableRow33.Name = "xrTableRow33";
        xrTableRow33.Weight = 1D;
        // 
        // xrTableCell75
        // 
        xrTableCell75.Dpi = 254F;
        xrTableCell75.Name = "xrTableCell75";
        xrTableCell75.Text = "Total Realizado";
        xrTableCell75.Weight = 2.3684156879625822D;
        // 
        // xrTableCell79
        // 
        xrTableCell79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
                    "oAcompanhamento.ValorAquisicao")});
        xrTableCell79.Dpi = 254F;
        xrTableCell79.Name = "xrTableCell79";
        xrSummary1.FormatString = "{0:n2}";
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        xrTableCell79.Summary = xrSummary1;
        xrTableCell79.Text = "xrTableCell79";
        xrTableCell79.Weight = 0.63158431203741783D;
        // 
        // detailReportBand1
        // 
        this.detailReportBand1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail17,
            this.GroupHeader7,
            this.GroupFooter3});
        this.detailReportBand1.DataMember = "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
"oAcompanhamento";
        this.detailReportBand1.DataSource = this.ds;
        this.detailReportBand1.Dpi = 254F;
        this.detailReportBand1.FilterString = "[IndicaAquisicaoPendente] = \'S\'";
        this.detailReportBand1.Level = 1;
        this.detailReportBand1.Name = "detailReportBand1";
        // 
        // Detail17
        // 
        this.Detail17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable21});
        this.Detail17.Dpi = 254F;
        this.Detail17.HeightF = 50F;
        this.Detail17.Name = "Detail17";
        // 
        // xrTable21
        // 
        xrTable21.Dpi = 254F;
        xrTable21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable21.Name = "xrTable21";
        xrTable21.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow57});
        xrTable21.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable21.StyleName = "Text";
        // 
        // xrTableRow57
        // 
        xrTableRow57.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell142,
            xrTableCell143});
        xrTableRow57.Dpi = 254F;
        xrTableRow57.Name = "xrTableRow57";
        xrTableRow57.Weight = 1D;
        // 
        // xrTableCell142
        // 
        xrTableCell142.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
                    "oAcompanhamento.Aquisicao")});
        xrTableCell142.Dpi = 254F;
        xrTableCell142.Name = "xrTableCell142";
        xrTableCell142.Text = "xrTableCell74";
        xrTableCell142.Weight = 2.3684156879625822D;
        // 
        // xrTableCell143
        // 
        xrTableCell143.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
                    "oAcompanhamento.ValorAquisicao", "{0:n2}")});
        xrTableCell143.Dpi = 254F;
        xrTableCell143.Name = "xrTableCell143";
        xrTableCell143.Text = "xrTableCell77";
        xrTableCell143.Weight = 0.63158431203741783D;
        // 
        // GroupHeader7
        // 
        this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable20});
        this.GroupHeader7.Dpi = 254F;
        this.GroupHeader7.HeightF = 50F;
        this.GroupHeader7.Name = "GroupHeader7";
        // 
        // xrTable20
        // 
        xrTable20.Dpi = 254F;
        xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable20.Name = "xrTable20";
        xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow56});
        xrTable20.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable20.StyleName = "Header";
        // 
        // xrTableRow56
        // 
        xrTableRow56.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell105,
            xrTableCell141});
        xrTableRow56.Dpi = 254F;
        xrTableRow56.Name = "xrTableRow56";
        xrTableRow56.Weight = 1D;
        // 
        // xrTableCell105
        // 
        xrTableCell105.Dpi = 254F;
        xrTableCell105.Name = "xrTableCell105";
        xrTableCell105.Text = "A Realizar";
        xrTableCell105.Weight = 2.3684210847553455D;
        // 
        // xrTableCell141
        // 
        xrTableCell141.Dpi = 254F;
        xrTableCell141.Name = "xrTableCell141";
        xrTableCell141.Text = "Valor (R$)";
        xrTableCell141.Weight = 0.6315789152446547D;
        // 
        // GroupFooter3
        // 
        this.GroupFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable22});
        this.GroupFooter3.Dpi = 254F;
        this.GroupFooter3.Name = "GroupFooter3";
        // 
        // xrTable22
        // 
        xrTable22.Dpi = 254F;
        xrTable22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable22.Name = "xrTable22";
        xrTable22.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow58});
        xrTable22.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable22.StyleName = "Header";
        // 
        // xrTableRow58
        // 
        xrTableRow58.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell144,
            xrTableCell145});
        xrTableRow58.Dpi = 254F;
        xrTableRow58.Name = "xrTableRow58";
        xrTableRow58.Weight = 1D;
        // 
        // xrTableCell144
        // 
        xrTableCell144.Dpi = 254F;
        xrTableCell144.Name = "xrTableCell144";
        xrTableCell144.Text = "Total a Realizar";
        xrTableCell144.Weight = 2.3684156879625822D;
        // 
        // xrTableCell145
        // 
        xrTableCell145.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoAquisicoes_pbh_Relatori" +
                    "oAcompanhamento.ValorAquisicao")});
        xrTableCell145.Dpi = 254F;
        xrTableCell145.Name = "xrTableCell145";
        xrSummary2.FormatString = "{0:n2}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        xrTableCell145.Summary = xrSummary2;
        xrTableCell145.Text = "xrTableCell79";
        xrTableCell145.Weight = 0.63158431203741783D;
        // 
        // drARealizar
        // 
        drARealizar.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail7,
            DetailReport});
        drARealizar.DataMember = "pbh_RelatorioAcompanhamento";
        drARealizar.DataSource = this.ds;
        drARealizar.Dpi = 254F;
        drARealizar.Expanded = false;
        drARealizar.Level = 4;
        drARealizar.Name = "drARealizar";
        // 
        // Detail7
        // 
        Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable10});
        Detail7.Dpi = 254F;
        Detail7.HeightF = 200F;
        Detail7.Name = "Detail7";
        // 
        // xrTable10
        // 
        xrTable10.Dpi = 254F;
        xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable10.Name = "xrTable10";
        xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow35,
            xrTableRow37,
            xrTableRow38,
            xrTableRow39});
        xrTable10.SizeF = new System.Drawing.SizeF(1900F, 200F);
        xrTable10.StyleName = "Text";
        // 
        // xrTableRow35
        // 
        xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell80});
        xrTableRow35.Dpi = 254F;
        xrTableRow35.Name = "xrTableRow35";
        xrTableRow35.Weight = 1D;
        // 
        // xrTableCell80
        // 
        xrTableCell80.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell80.Dpi = 254F;
        xrTableCell80.Name = "xrTableCell80";
        xrTableCell80.StyleName = "Header";
        xrTableCell80.StylePriority.UseBorders = false;
        xrTableCell80.Text = "3.7. Garantia da Qualidade";
        xrTableCell80.Weight = 3D;
        // 
        // xrTableRow37
        // 
        xrTableRow37.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell83});
        xrTableRow37.Dpi = 254F;
        xrTableRow37.Name = "xrTableRow37";
        xrTableRow37.Weight = 1D;
        // 
        // xrTableCell83
        // 
        xrTableCell83.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell83.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioGarantiaQualidade")});
        xrTableCell83.Dpi = 254F;
        xrTableCell83.Multiline = true;
        xrTableCell83.Name = "xrTableCell83";
        xrTableCell83.StylePriority.UseBorders = false;
        xrTableCell83.Text = "xrTableCell83";
        xrTableCell83.Weight = 3D;
        // 
        // xrTableRow38
        // 
        xrTableRow38.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell84});
        xrTableRow38.Dpi = 254F;
        xrTableRow38.Name = "xrTableRow38";
        xrTableRow38.Weight = 1D;
        // 
        // xrTableCell84
        // 
        xrTableCell84.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        xrTableCell84.Dpi = 254F;
        xrTableCell84.Name = "xrTableCell84";
        xrTableCell84.StyleName = "Header";
        xrTableCell84.StylePriority.UseBorders = false;
        xrTableCell84.Text = "3.8. Gerência de configuração";
        xrTableCell84.Weight = 3D;
        // 
        // xrTableRow39
        // 
        xrTableRow39.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell85});
        xrTableRow39.Dpi = 254F;
        xrTableRow39.Name = "xrTableRow39";
        xrTableRow39.Weight = 1D;
        // 
        // xrTableCell85
        // 
        xrTableCell85.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTableCell85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.ComentarioGerenciaConfiguracao")});
        xrTableCell85.Dpi = 254F;
        xrTableCell85.Multiline = true;
        xrTableCell85.Name = "xrTableCell85";
        xrTableCell85.StylePriority.UseBorders = false;
        xrTableCell85.Text = "xrTableCell85";
        xrTableCell85.Weight = 3D;
        // 
        // DetailReport
        // 
        DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail8,
            GroupHeader3});
        DetailReport.DataMember = "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoRiscos_pbh_RelatorioAco" +
"mpanhamento";
        DetailReport.DataSource = this.ds;
        DetailReport.Dpi = 254F;
        DetailReport.Level = 0;
        DetailReport.Name = "DetailReport";
        // 
        // Detail8
        // 
        Detail8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable12});
        Detail8.Dpi = 254F;
        Detail8.HeightF = 50F;
        Detail8.Name = "Detail8";
        // 
        // xrTable12
        // 
        xrTable12.Dpi = 254F;
        xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable12.Name = "xrTable12";
        xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow42});
        xrTable12.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable12.StyleName = "Text";
        // 
        // xrTableRow42
        // 
        xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell86,
            xrTableCell94,
            xrTableCell87,
            xrTableCell89});
        xrTableRow42.Dpi = 254F;
        xrTableRow42.Name = "xrTableRow42";
        xrTableRow42.Weight = 1D;
        // 
        // xrTableCell86
        // 
        xrTableCell86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoRiscos_pbh_RelatorioAco" +
                    "mpanhamento.Risco")});
        xrTableCell86.Dpi = 254F;
        xrTableCell86.Name = "xrTableCell86";
        xrTableCell86.Text = "xrTableCell86";
        xrTableCell86.Weight = 0.82894740054481908D;
        // 
        // xrTableCell94
        // 
        xrTableCell94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoRiscos_pbh_RelatorioAco" +
                    "mpanhamento.Impacto")});
        xrTableCell94.Dpi = 254F;
        xrTableCell94.Name = "xrTableCell94";
        xrTableCell94.Text = "xrTableCell94";
        xrTableCell94.Weight = 0.67105264764083072D;
        // 
        // xrTableCell87
        // 
        xrTableCell87.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoRiscos_pbh_RelatorioAco" +
                    "mpanhamento.Acao")});
        xrTableCell87.Dpi = 254F;
        xrTableCell87.Name = "xrTableCell87";
        xrTableCell87.Text = "xrTableCell87";
        xrTableCell87.Weight = 0.94736843711451479D;
        // 
        // xrTableCell89
        // 
        xrTableCell89.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoRiscos_pbh_RelatorioAco" +
                    "mpanhamento.Responsavel")});
        xrTableCell89.Dpi = 254F;
        xrTableCell89.Name = "xrTableCell89";
        xrTableCell89.Text = "xrTableCell89";
        xrTableCell89.Weight = 0.55263151469983551D;
        // 
        // GroupHeader3
        // 
        GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable11});
        GroupHeader3.Dpi = 254F;
        GroupHeader3.Name = "GroupHeader3";
        // 
        // xrTable11
        // 
        xrTable11.Dpi = 254F;
        xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable11.Name = "xrTable11";
        xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow40,
            xrTableRow41});
        xrTable11.SizeF = new System.Drawing.SizeF(1900F, 100F);
        xrTable11.StyleName = "Header";
        // 
        // xrTableRow40
        // 
        xrTableRow40.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell88});
        xrTableRow40.Dpi = 254F;
        xrTableRow40.Name = "xrTableRow40";
        xrTableRow40.Weight = 1D;
        // 
        // xrTableCell88
        // 
        xrTableCell88.Dpi = 254F;
        xrTableCell88.Name = "xrTableCell88";
        xrTableCell88.Text = "3.9. Riscos altos";
        xrTableCell88.Weight = 3D;
        // 
        // xrTableRow41
        // 
        xrTableRow41.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell90,
            xrTableCell91,
            xrTableCell92,
            xrTableCell93});
        xrTableRow41.Dpi = 254F;
        xrTableRow41.Name = "xrTableRow41";
        xrTableRow41.Weight = 1D;
        // 
        // xrTableCell90
        // 
        xrTableCell90.Dpi = 254F;
        xrTableCell90.Name = "xrTableCell90";
        xrTableCell90.Text = "Risco";
        xrTableCell90.Weight = 0.82894740054481908D;
        // 
        // xrTableCell91
        // 
        xrTableCell91.Dpi = 254F;
        xrTableCell91.Name = "xrTableCell91";
        xrTableCell91.Text = "Consequências";
        xrTableCell91.Weight = 0.67105259945518092D;
        // 
        // xrTableCell92
        // 
        xrTableCell92.Dpi = 254F;
        xrTableCell92.Name = "xrTableCell92";
        xrTableCell92.Text = "Tratamento";
        xrTableCell92.Weight = 0.94736843711451479D;
        // 
        // xrTableCell93
        // 
        xrTableCell93.Dpi = 254F;
        xrTableCell93.Name = "xrTableCell93";
        xrTableCell93.Text = "Responsável";
        xrTableCell93.Weight = 0.55263156288548532D;
        // 
        // drSustentacao
        // 
        drSustentacao.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail9});
        drSustentacao.DataMember = "pbh_RelatorioAcompanhamento";
        drSustentacao.DataSource = this.ds;
        drSustentacao.Dpi = 254F;
        drSustentacao.Expanded = false;
        drSustentacao.Level = 5;
        drSustentacao.Name = "drSustentacao";
        // 
        // Detail9
        // 
        Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrPanel1});
        Detail9.Dpi = 254F;
        Detail9.HeightF = 450F;
        Detail9.Name = "Detail9";
        // 
        // xrPanel1
        // 
        xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel7,
            xrLabel6,
            xrLabel5,
            xrLabel4,
            xrCheckBox6,
            xrCheckBox5,
            xrCheckBox4,
            xrLabel3,
            xrCheckBox3,
            xrCheckBox2,
            xrCheckBox1,
            xrLabel2,
            xrLabel1});
        xrPanel1.Dpi = 254F;
        xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrPanel1.Name = "xrPanel1";
        xrPanel1.SizeF = new System.Drawing.SizeF(1900F, 405F);
        xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrLabel7
        // 
        xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.JustificativaAreaSustentacao")});
        xrLabel7.Dpi = 254F;
        xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(410F, 205.0001F);
        xrLabel7.Name = "xrLabel7";
        xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel7.SizeF = new System.Drawing.SizeF(1485F, 49.99998F);
        xrLabel7.StylePriority.UseBorders = false;
        xrLabel7.StylePriority.UseTextAlignment = false;
        xrLabel7.Text = "Justificar: ";
        xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel6
        // 
        xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.SiglaUnidadeSustentacao")});
        xrLabel6.Dpi = 254F;
        xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(360F, 149.9999F);
        xrLabel6.Name = "xrLabel6";
        xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel6.SizeF = new System.Drawing.SizeF(1535F, 50.00002F);
        xrLabel6.StylePriority.UseBorders = false;
        xrLabel6.StylePriority.UseTextAlignment = false;
        xrLabel6.Text = "Área: ";
        xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel5
        // 
        xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrLabel5.Dpi = 254F;
        xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(260F, 205F);
        xrLabel5.Name = "xrLabel5";
        xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel5.SizeF = new System.Drawing.SizeF(150F, 50F);
        xrLabel5.StylePriority.UseBorders = false;
        xrLabel5.StylePriority.UseTextAlignment = false;
        xrLabel5.Text = "Justificar: ";
        xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel4
        // 
        xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrLabel4.Dpi = 254F;
        xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(260F, 150F);
        xrLabel4.Name = "xrLabel4";
        xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel4.SizeF = new System.Drawing.SizeF(100F, 50F);
        xrLabel4.StylePriority.UseBorders = false;
        xrLabel4.StylePriority.UseTextAlignment = false;
        xrLabel4.Text = "Área: ";
        xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrCheckBox6
        // 
        xrCheckBox6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrCheckBox6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("CheckState", null, "pbh_RelatorioAcompanhamento.IndicaEmissaoDocumentoRepasse")});
        xrCheckBox6.Dpi = 254F;
        xrCheckBox6.LocationFloat = new DevExpress.Utils.PointFloat(1625F, 350F);
        xrCheckBox6.Name = "xrCheckBox6";
        xrCheckBox6.SizeF = new System.Drawing.SizeF(254F, 50F);
        xrCheckBox6.StylePriority.UseBorders = false;
        xrCheckBox6.StylePriority.UseTextAlignment = false;
        xrCheckBox6.Text = "Não se aplica";
        xrCheckBox6.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cbNaoSeAplica_EvaluateBinding);
        // 
        // xrCheckBox5
        // 
        xrCheckBox5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrCheckBox5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("CheckState", null, "pbh_RelatorioAcompanhamento.IndicaEmissaoDocumentoRepasse")});
        xrCheckBox5.Dpi = 254F;
        xrCheckBox5.LocationFloat = new DevExpress.Utils.PointFloat(1450F, 350F);
        xrCheckBox5.Name = "xrCheckBox5";
        xrCheckBox5.SizeF = new System.Drawing.SizeF(125F, 49.99998F);
        xrCheckBox5.StylePriority.UseBorders = false;
        xrCheckBox5.StylePriority.UseTextAlignment = false;
        xrCheckBox5.Text = "Não";
        xrCheckBox5.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cbNao_EvaluateBinding);
        // 
        // xrCheckBox4
        // 
        xrCheckBox4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrCheckBox4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("CheckState", null, "pbh_RelatorioAcompanhamento.IndicaEmissaoDocumentoRepasse")});
        xrCheckBox4.Dpi = 254F;
        xrCheckBox4.LocationFloat = new DevExpress.Utils.PointFloat(1275F, 350F);
        xrCheckBox4.Name = "xrCheckBox4";
        xrCheckBox4.SizeF = new System.Drawing.SizeF(125F, 50F);
        xrCheckBox4.StylePriority.UseBorders = false;
        xrCheckBox4.StylePriority.UseTextAlignment = false;
        xrCheckBox4.Text = "Sim";
        xrCheckBox4.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cbSim_EvaluateBinding);
        // 
        // xrLabel3
        // 
        xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrLabel3.Dpi = 254F;
        xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(5.000018F, 349.9999F);
        xrLabel3.Name = "xrLabel3";
        xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel3.SizeF = new System.Drawing.SizeF(1250F, 50F);
        xrLabel3.StylePriority.UseBorders = false;
        xrLabel3.StylePriority.UseTextAlignment = false;
        xrLabel3.Text = "Foi emitido o documento para repasse da solução/sistema para área de Sustentação?" +
" ";
        xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrCheckBox3
        // 
        xrCheckBox3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrCheckBox3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("CheckState", null, "pbh_RelatorioAcompanhamento.IndicaDefinicaoAreaSustentacao")});
        xrCheckBox3.Dpi = 254F;
        xrCheckBox3.LocationFloat = new DevExpress.Utils.PointFloat(5F, 260F);
        xrCheckBox3.Name = "xrCheckBox3";
        xrCheckBox3.SizeF = new System.Drawing.SizeF(250F, 50F);
        xrCheckBox3.StylePriority.UseBorders = false;
        xrCheckBox3.StylePriority.UseTextAlignment = false;
        xrCheckBox3.Text = "Não se aplica";
        xrCheckBox3.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cbNaoSeAplica_EvaluateBinding);
        // 
        // xrCheckBox2
        // 
        xrCheckBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrCheckBox2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("CheckState", null, "pbh_RelatorioAcompanhamento.IndicaDefinicaoAreaSustentacao")});
        xrCheckBox2.Dpi = 254F;
        xrCheckBox2.LocationFloat = new DevExpress.Utils.PointFloat(5F, 205F);
        xrCheckBox2.Name = "xrCheckBox2";
        xrCheckBox2.SizeF = new System.Drawing.SizeF(125F, 49.99998F);
        xrCheckBox2.StylePriority.UseBorders = false;
        xrCheckBox2.StylePriority.UseTextAlignment = false;
        xrCheckBox2.Text = "Não";
        xrCheckBox2.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cbNao_EvaluateBinding);
        // 
        // xrCheckBox1
        // 
        xrCheckBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrCheckBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("CheckState", null, "pbh_RelatorioAcompanhamento.IndicaDefinicaoAreaSustentacao")});
        xrCheckBox1.Dpi = 254F;
        xrCheckBox1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 150F);
        xrCheckBox1.Name = "xrCheckBox1";
        xrCheckBox1.SizeF = new System.Drawing.SizeF(125F, 50F);
        xrCheckBox1.StylePriority.UseBorders = false;
        xrCheckBox1.StylePriority.UseTextAlignment = false;
        xrCheckBox1.Text = "Sim";
        xrCheckBox1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cbSim_EvaluateBinding);
        // 
        // xrLabel2
        // 
        xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrLabel2.Dpi = 254F;
        xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(5F, 100F);
        xrLabel2.Name = "xrLabel2";
        xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel2.SizeF = new System.Drawing.SizeF(1890F, 50F);
        xrLabel2.StylePriority.UseBorders = false;
        xrLabel2.Text = "Foi definida a área responsável pela sustentação?";
        // 
        // xrLabel1
        // 
        xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xrLabel1.Dpi = 254F;
        xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 5F);
        xrLabel1.Name = "xrLabel1";
        xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel1.SizeF = new System.Drawing.SizeF(1890F, 50F);
        xrLabel1.StyleName = "Header";
        xrLabel1.StylePriority.UseBorders = false;
        xrLabel1.Text = "3.10 Sustentação";
        // 
        // drDesvios
        // 
        drDesvios.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail10,
            DetailReport3});
        drDesvios.DataMember = "pbh_RelatorioAcompanhamento";
        drDesvios.DataSource = this.ds;
        drDesvios.Dpi = 254F;
        drDesvios.Expanded = false;
        drDesvios.Level = 6;
        drDesvios.Name = "drDesvios";
        // 
        // Detail10
        // 
        Detail10.Dpi = 254F;
        Detail10.HeightF = 0F;
        Detail10.Name = "Detail10";
        // 
        // DetailReport3
        // 
        DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail11,
            GroupHeader5,
            GroupFooter2});
        DetailReport3.DataMember = "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoProblemas_pbh_Relatorio" +
"Acompanhamento";
        DetailReport3.DataSource = this.ds;
        DetailReport3.Dpi = 254F;
        DetailReport3.Level = 0;
        DetailReport3.Name = "DetailReport3";
        // 
        // Detail11
        // 
        Detail11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable14});
        Detail11.Dpi = 254F;
        Detail11.HeightF = 50F;
        Detail11.Name = "Detail11";
        // 
        // xrTable14
        // 
        xrTable14.Dpi = 254F;
        xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable14.Name = "xrTable14";
        xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow45});
        xrTable14.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable14.StyleName = "Text";
        // 
        // xrTableRow45
        // 
        xrTableRow45.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell102,
            xrTableCell106,
            xrTableCell103,
            xrTableCell107,
            this.xrTableCell95});
        xrTableRow45.Dpi = 254F;
        xrTableRow45.Name = "xrTableRow45";
        xrTableRow45.Weight = 1D;
        // 
        // xrTableCell102
        // 
        xrTableCell102.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoProblemas_pbh_Relatorio" +
                    "Acompanhamento.Problema")});
        xrTableCell102.Dpi = 254F;
        xrTableCell102.Name = "xrTableCell102";
        xrTableCell102.Text = "xrTableCell102";
        xrTableCell102.Weight = 0.95983185903634716D;
        // 
        // xrTableCell106
        // 
        xrTableCell106.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoProblemas_pbh_Relatorio" +
                    "Acompanhamento.Acao")});
        xrTableCell106.Dpi = 254F;
        xrTableCell106.Name = "xrTableCell106";
        xrTableCell106.Text = "xrTableCell106";
        xrTableCell106.Weight = 0.5484753410802593D;
        // 
        // xrTableCell103
        // 
        xrTableCell103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoProblemas_pbh_Relatorio" +
                    "Acompanhamento.Responsavel")});
        xrTableCell103.Dpi = 254F;
        xrTableCell103.Name = "xrTableCell103";
        xrTableCell103.Text = "xrTableCell103";
        xrTableCell103.Weight = 0.4799159766167071D;
        // 
        // xrTableCell107
        // 
        xrTableCell107.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoProblemas_pbh_Relatorio" +
                    "Acompanhamento.Prazo", "{0:dd/MM/yyyy}")});
        xrTableCell107.Dpi = 254F;
        xrTableCell107.Name = "xrTableCell107";
        xrTableCell107.Text = "xrTableCell107";
        xrTableCell107.Weight = 0.308517356431863D;
        // 
        // xrTableCell95
        // 
        this.xrTableCell95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoProblemas_pbh_Relatorio" +
                    "Acompanhamento.Status")});
        this.xrTableCell95.Dpi = 254F;
        this.xrTableCell95.Name = "xrTableCell95";
        this.xrTableCell95.Text = "xrTableCell95";
        this.xrTableCell95.Weight = 0.308517356431863D;
        // 
        // GroupHeader5
        // 
        GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable13});
        GroupHeader5.Dpi = 254F;
        GroupHeader5.Name = "GroupHeader5";
        // 
        // xrTable13
        // 
        xrTable13.Dpi = 254F;
        xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable13.Name = "xrTable13";
        xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow43,
            xrTableRow44});
        xrTable13.SizeF = new System.Drawing.SizeF(1900F, 100F);
        xrTable13.StyleName = "Header";
        // 
        // xrTableRow43
        // 
        xrTableRow43.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell97});
        xrTableRow43.Dpi = 254F;
        xrTableRow43.Name = "xrTableRow43";
        xrTableRow43.Weight = 1D;
        // 
        // xrTableCell97
        // 
        xrTableCell97.Dpi = 254F;
        xrTableCell97.Name = "xrTableCell97";
        xrTableCell97.Text = "4. DESVIOS/PROBLEMAS/PENDÊNCIAS";
        xrTableCell97.Weight = 3D;
        // 
        // xrTableRow44
        // 
        xrTableRow44.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell98,
            xrTableCell96,
            xrTableCell99,
            this.xrTableCell1,
            xrTableCell101});
        xrTableRow44.Dpi = 254F;
        xrTableRow44.Name = "xrTableRow44";
        xrTableRow44.Weight = 1D;
        // 
        // xrTableCell98
        // 
        xrTableCell98.Dpi = 254F;
        xrTableCell98.Name = "xrTableCell98";
        xrTableCell98.Text = "Descrição";
        xrTableCell98.Weight = 0.95983185903634716D;
        // 
        // xrTableCell96
        // 
        xrTableCell96.Dpi = 254F;
        xrTableCell96.Name = "xrTableCell96";
        xrTableCell96.Text = "Tratamento";
        xrTableCell96.Weight = 0.5484753410802593D;
        // 
        // xrTableCell99
        // 
        xrTableCell99.Dpi = 254F;
        xrTableCell99.Name = "xrTableCell99";
        xrTableCell99.Text = "Responsável";
        xrTableCell99.Weight = 0.4799159766167071D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Prazo";
        this.xrTableCell1.Weight = 0.308517356431863D;
        // 
        // xrTableCell101
        // 
        xrTableCell101.Dpi = 254F;
        xrTableCell101.Name = "xrTableCell101";
        xrTableCell101.Text = "Status";
        xrTableCell101.Weight = 0.308517356431863D;
        // 
        // GroupFooter2
        // 
        GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel8});
        GroupFooter2.Dpi = 254F;
        GroupFooter2.Name = "GroupFooter2";
        // 
        // xrLabel8
        // 
        xrLabel8.Dpi = 254F;
        xrLabel8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrLabel8.Name = "xrLabel8";
        xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel8.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrLabel8.StylePriority.UseFont = false;
        xrLabel8.Text = "Status: Não iniciado / Em execução / Resolvido";
        // 
        // drControleMudancas
        // 
        drControleMudancas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail12});
        drControleMudancas.DataMember = "pbh_RelatorioAcompanhamento";
        drControleMudancas.DataSource = this.ds;
        drControleMudancas.Dpi = 254F;
        drControleMudancas.Expanded = false;
        drControleMudancas.Level = 7;
        drControleMudancas.Name = "drControleMudancas";
        // 
        // Detail12
        // 
        Detail12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable15});
        Detail12.Dpi = 254F;
        Detail12.HeightF = 200F;
        Detail12.Name = "Detail12";
        // 
        // xrTable15
        // 
        xrTable15.Dpi = 254F;
        xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable15.Name = "xrTable15";
        xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow46,
            xrTableRow47,
            xrTableRow48});
        xrTable15.SizeF = new System.Drawing.SizeF(1900F, 150F);
        xrTable15.StyleName = "Header";
        // 
        // xrTableRow46
        // 
        xrTableRow46.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell110});
        xrTableRow46.Dpi = 254F;
        xrTableRow46.Name = "xrTableRow46";
        xrTableRow46.Weight = 1D;
        // 
        // xrTableCell110
        // 
        xrTableCell110.Dpi = 254F;
        xrTableCell110.Name = "xrTableCell110";
        xrTableCell110.Text = "5. CONTROLE DE MUDANÇAS";
        xrTableCell110.Weight = 3D;
        // 
        // xrTableRow47
        // 
        xrTableRow47.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell111,
            xrTableCell112,
            xrTableCell108,
            xrTableCell113,
            xrTableCell118,
            this.xrTableCell139});
        xrTableRow47.Dpi = 254F;
        xrTableRow47.Name = "xrTableRow47";
        xrTableRow47.Weight = 1D;
        // 
        // xrTableCell111
        // 
        xrTableCell111.Dpi = 254F;
        xrTableCell111.Name = "xrTableCell111";
        xrTableCell111.Weight = 0.63157893130653786D;
        // 
        // xrTableCell112
        // 
        xrTableCell112.Dpi = 254F;
        xrTableCell112.Name = "xrTableCell112";
        xrTableCell112.Text = "Escopo";
        xrTableCell112.Weight = 0.47368422658819909D;
        // 
        // xrTableCell108
        // 
        xrTableCell108.Dpi = 254F;
        xrTableCell108.Name = "xrTableCell108";
        xrTableCell108.Text = "Cronograma";
        xrTableCell108.Weight = 0.47368422658819892D;
        // 
        // xrTableCell113
        // 
        xrTableCell113.Dpi = 254F;
        xrTableCell113.Name = "xrTableCell113";
        xrTableCell113.Text = "Custos";
        xrTableCell113.Weight = 0.47368422658819909D;
        // 
        // xrTableCell118
        // 
        xrTableCell118.Dpi = 254F;
        xrTableCell118.Name = "xrTableCell118";
        xrTableCell118.Text = "Paralisação";
        xrTableCell118.Weight = 0.47368419446443255D;
        // 
        // xrTableCell139
        // 
        this.xrTableCell139.Dpi = 254F;
        this.xrTableCell139.Name = "xrTableCell139";
        this.xrTableCell139.Text = "Reativação";
        this.xrTableCell139.Weight = 0.47368419446443255D;
        // 
        // xrTableRow48
        // 
        xrTableRow48.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell114,
            xrTableCell115,
            xrTableCell109,
            xrTableCell116,
            xrTableCell119,
            this.xrTableCell140});
        xrTableRow48.Dpi = 254F;
        xrTableRow48.Name = "xrTableRow48";
        xrTableRow48.Weight = 1D;
        // 
        // xrTableCell114
        // 
        xrTableCell114.Dpi = 254F;
        xrTableCell114.Name = "xrTableCell114";
        xrTableCell114.Text = "Volume de Mudanças";
        xrTableCell114.Weight = 0.63157893130653786D;
        // 
        // xrTableCell115
        // 
        xrTableCell115.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.QuantMudancaEscopo")});
        xrTableCell115.Dpi = 254F;
        xrTableCell115.Name = "xrTableCell115";
        xrTableCell115.StyleName = "Text";
        xrTableCell115.Text = "xrTableCell115";
        xrTableCell115.Weight = 0.47368422658819909D;
        // 
        // xrTableCell109
        // 
        xrTableCell109.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.QuantMudancaCronograma")});
        xrTableCell109.Dpi = 254F;
        xrTableCell109.Name = "xrTableCell109";
        xrTableCell109.StyleName = "Text";
        xrTableCell109.Text = "xrTableCell109";
        xrTableCell109.Weight = 0.47368422658819892D;
        // 
        // xrTableCell116
        // 
        xrTableCell116.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.QuantMudancaCusto")});
        xrTableCell116.Dpi = 254F;
        xrTableCell116.Name = "xrTableCell116";
        xrTableCell116.StyleName = "Text";
        xrTableCell116.Text = "xrTableCell116";
        xrTableCell116.Weight = 0.47368422658819909D;
        // 
        // xrTableCell119
        // 
        xrTableCell119.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.QuantParalisacao")});
        xrTableCell119.Dpi = 254F;
        xrTableCell119.Name = "xrTableCell119";
        xrTableCell119.StyleName = "Text";
        xrTableCell119.Text = "xrTableCell119";
        xrTableCell119.Weight = 0.47368419446443255D;
        // 
        // xrTableCell140
        // 
        this.xrTableCell140.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.QuantReativacao")});
        this.xrTableCell140.Dpi = 254F;
        this.xrTableCell140.Name = "xrTableCell140";
        this.xrTableCell140.StyleName = "Text";
        this.xrTableCell140.Text = "xrTableCell140";
        this.xrTableCell140.Weight = 0.47368419446443255D;
        // 
        // drEntregas
        // 
        drEntregas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail13,
            DetailReport4});
        drEntregas.DataMember = "pbh_RelatorioAcompanhamento";
        drEntregas.DataSource = this.ds;
        drEntregas.Dpi = 254F;
        drEntregas.Expanded = false;
        drEntregas.Level = 8;
        drEntregas.Name = "drEntregas";
        // 
        // Detail13
        // 
        Detail13.Dpi = 254F;
        Detail13.HeightF = 0F;
        Detail13.Name = "Detail13";
        // 
        // DetailReport4
        // 
        DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail15,
            GroupHeader4});
        DetailReport4.DataMember = "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoEntregas_pbh_RelatorioA" +
"companhamento";
        DetailReport4.DataSource = this.ds;
        DetailReport4.Dpi = 254F;
        DetailReport4.Level = 0;
        DetailReport4.Name = "DetailReport4";
        // 
        // Detail15
        // 
        Detail15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable18});
        Detail15.Dpi = 254F;
        Detail15.HeightF = 50F;
        Detail15.Name = "Detail15";
        // 
        // xrTable18
        // 
        xrTable18.Dpi = 254F;
        xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable18.Name = "xrTable18";
        xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow53});
        xrTable18.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable18.StyleName = "Text";
        // 
        // xrTableRow53
        // 
        xrTableRow53.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell127,
            xrTableCell128,
            xrTableCell129});
        xrTableRow53.Dpi = 254F;
        xrTableRow53.Name = "xrTableRow53";
        xrTableRow53.Weight = 1D;
        // 
        // xrTableCell127
        // 
        xrTableCell127.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoEntregas_pbh_RelatorioA" +
                    "companhamento.Produto")});
        xrTableCell127.Dpi = 254F;
        xrTableCell127.Name = "xrTableCell127";
        xrTableCell127.Text = "xrTableCell127";
        xrTableCell127.Weight = 1.0263158215974506D;
        // 
        // xrTableCell128
        // 
        xrTableCell128.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoEntregas_pbh_RelatorioA" +
                    "companhamento.TipoEntrega")});
        xrTableCell128.Dpi = 254F;
        xrTableCell128.Name = "xrTableCell128";
        xrTableCell128.Text = "xrTableCell128";
        xrTableCell128.Weight = 0.4736842426500823D;
        xrTableCell128.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cell_EvaluateBinding);
        // 
        // xrTableCell129
        // 
        xrTableCell129.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoEntregas_pbh_RelatorioA" +
                    "companhamento.DescricaoEntrega")});
        xrTableCell129.Dpi = 254F;
        xrTableCell129.Name = "xrTableCell129";
        xrTableCell129.Text = "xrTableCell129";
        xrTableCell129.Weight = 1.4999999357524672D;
        // 
        // GroupHeader4
        // 
        GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable16});
        GroupHeader4.Dpi = 254F;
        GroupHeader4.HeightF = 125F;
        GroupHeader4.Name = "GroupHeader4";
        // 
        // xrTable16
        // 
        xrTable16.Dpi = 254F;
        xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable16.Name = "xrTable16";
        xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow49,
            xrTableRow50});
        xrTable16.SizeF = new System.Drawing.SizeF(1900F, 125F);
        xrTable16.StyleName = "Header";
        // 
        // xrTableRow49
        // 
        xrTableRow49.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell121});
        xrTableRow49.Dpi = 254F;
        xrTableRow49.Name = "xrTableRow49";
        xrTableRow49.Weight = 1D;
        // 
        // xrTableCell121
        // 
        xrTableCell121.Dpi = 254F;
        xrTableCell121.Name = "xrTableCell121";
        xrTableCell121.Text = "6. ENTREGAS";
        xrTableCell121.Weight = 3D;
        // 
        // xrTableRow50
        // 
        xrTableRow50.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell122,
            xrTableCell123,
            xrTableCell124});
        xrTableRow50.Dpi = 254F;
        xrTableRow50.Name = "xrTableRow50";
        xrTableRow50.Weight = 1.5D;
        // 
        // xrTableCell122
        // 
        xrTableCell122.Dpi = 254F;
        xrTableCell122.Name = "xrTableCell122";
        xrTableCell122.Text = "Produto";
        xrTableCell122.Weight = 1.0263158215974506D;
        // 
        // xrTableCell123
        // 
        xrTableCell123.Dpi = 254F;
        xrTableCell123.Multiline = true;
        xrTableCell123.Name = "xrTableCell123";
        xrTableCell123.Text = "Tipo de Entrega\r\n(Parcial/Total)";
        xrTableCell123.Weight = 0.4736842185572574D;
        // 
        // xrTableCell124
        // 
        xrTableCell124.Dpi = 254F;
        xrTableCell124.Name = "xrTableCell124";
        xrTableCell124.Text = "Descrição";
        xrTableCell124.Weight = 1.499999959845292D;
        // 
        // drDestinatarios
        // 
        drDestinatarios.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail14,
            DetailReport5});
        drDestinatarios.DataMember = "pbh_RelatorioAcompanhamento";
        drDestinatarios.DataSource = this.ds;
        drDestinatarios.Dpi = 254F;
        drDestinatarios.Expanded = false;
        drDestinatarios.Level = 9;
        drDestinatarios.Name = "drDestinatarios";
        // 
        // Detail14
        // 
        Detail14.Dpi = 254F;
        Detail14.HeightF = 0F;
        Detail14.Name = "Detail14";
        // 
        // DetailReport5
        // 
        DetailReport5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail16,
            GroupHeader6});
        DetailReport5.DataMember = "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoDestinatarios_pbh_Relat" +
"orioAcompanhamento";
        DetailReport5.DataSource = this.ds;
        DetailReport5.Dpi = 254F;
        DetailReport5.Level = 0;
        DetailReport5.Name = "DetailReport5";
        // 
        // Detail16
        // 
        Detail16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable19});
        Detail16.Dpi = 254F;
        Detail16.HeightF = 50F;
        Detail16.Name = "Detail16";
        // 
        // xrTable19
        // 
        xrTable19.Dpi = 254F;
        xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        xrTable19.Name = "xrTable19";
        xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow54});
        xrTable19.SizeF = new System.Drawing.SizeF(1900F, 50F);
        xrTable19.StyleName = "Text";
        // 
        // xrTableRow54
        // 
        xrTableRow54.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell130,
            xrTableCell131,
            xrTableCell132});
        xrTableRow54.Dpi = 254F;
        xrTableRow54.Name = "xrTableRow54";
        xrTableRow54.Weight = 1D;
        // 
        // xrTableCell130
        // 
        xrTableCell130.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoDestinatarios_pbh_Relat" +
                    "orioAcompanhamento.Unidade")});
        xrTableCell130.Dpi = 254F;
        xrTableCell130.Name = "xrTableCell130";
        xrTableCell130.Text = "xrTableCell127";
        xrTableCell130.Weight = 1D;
        // 
        // xrTableCell131
        // 
        xrTableCell131.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoDestinatarios_pbh_Relat" +
                    "orioAcompanhamento.Destinatario")});
        xrTableCell131.Dpi = 254F;
        xrTableCell131.Name = "xrTableCell131";
        xrTableCell131.Text = "xrTableCell128";
        xrTableCell131.Weight = 1D;
        // 
        // xrTableCell132
        // 
        xrTableCell132.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "pbh_RelatorioAcompanhamento.FK_pbh_RelatorioAcompanhamentoDestinatarios_pbh_Relat" +
                    "orioAcompanhamento.EmailDestinatario")});
        xrTableCell132.Dpi = 254F;
        xrTableCell132.Name = "xrTableCell132";
        xrTableCell132.Text = "xrTableCell129";
        xrTableCell132.Weight = 1D;
        // 
        // GroupHeader6
        // 
        GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable17});
        GroupHeader6.Dpi = 254F;
        GroupHeader6.HeightF = 150F;
        GroupHeader6.Name = "GroupHeader6";
        // 
        // xrTable17
        // 
        xrTable17.Dpi = 254F;
        xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        xrTable17.Name = "xrTable17";
        xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow51,
            xrTableRow52});
        xrTable17.SizeF = new System.Drawing.SizeF(1900F, 100F);
        xrTable17.StyleName = "Header";
        // 
        // xrTableRow51
        // 
        xrTableRow51.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell117});
        xrTableRow51.Dpi = 254F;
        xrTableRow51.Name = "xrTableRow51";
        xrTableRow51.Weight = 1D;
        // 
        // xrTableCell117
        // 
        xrTableCell117.Dpi = 254F;
        xrTableCell117.Name = "xrTableCell117";
        xrTableCell117.Text = "7. DESTINATÁRIOS";
        xrTableCell117.Weight = 3D;
        // 
        // xrTableRow52
        // 
        xrTableRow52.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell120,
            xrTableCell125,
            xrTableCell126});
        xrTableRow52.Dpi = 254F;
        xrTableRow52.Name = "xrTableRow52";
        xrTableRow52.Weight = 1D;
        // 
        // xrTableCell120
        // 
        xrTableCell120.Dpi = 254F;
        xrTableCell120.Name = "xrTableCell120";
        xrTableCell120.Text = "Unidade";
        xrTableCell120.Weight = 1D;
        // 
        // xrTableCell125
        // 
        xrTableCell125.Dpi = 254F;
        xrTableCell125.Name = "xrTableCell125";
        xrTableCell125.Text = "Responsável";
        xrTableCell125.Weight = 1D;
        // 
        // xrTableCell126
        // 
        xrTableCell126.Dpi = 254F;
        xrTableCell126.Name = "xrTableCell126";
        xrTableCell126.Text = "E-mail";
        xrTableCell126.Weight = 1D;
        // 
        // BottomMargin
        // 
        BottomMargin.Dpi = 254F;
        BottomMargin.HeightF = 99F;
        BottomMargin.Name = "BottomMargin";
        BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // Title
        // 
        this.Title.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.Title.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Title.Name = "Title";
        // 
        // Header
        // 
        this.Header.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.Header.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.Header.Name = "Header";
        this.Header.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        // 
        // Text
        // 
        this.TextControlStyle.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TextControlStyle.Font = new System.Drawing.Font("Verdana", 8F);
        this.TextControlStyle.Name = "Text";
        this.TextControlStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 50F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Format = "Página {0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1650F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(250F, 50F);
        this.xrPageInfo1.StyleName = "PagaInfo";
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // PagaInfo
        // 
        this.PagaInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PagaInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.PagaInfo.Name = "PagaInfo";
        this.PagaInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // relRelatorioAcompanhamento
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail,
            TopMargin,
            BottomMargin,
            PageHeader,
            drIdentificaoProjeto,
            drParametrosProjeto,
            drAnaliseIndicadores,
            drMonitoramento,
            drARealizar,
            drSustentacao,
            drDesvios,
            drControleMudancas,
            drEntregas,
            drDestinatarios,
            this.PageFooter});
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Margins = new System.Drawing.Printing.Margins(99, 99, 99, 99);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.Header,
            this.TextControlStyle,
            this.PagaInfo});
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable21)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable20)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable22)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable19)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void cbSim_EvaluateBinding(object sender, BindingEventArgs e)
    {
        //XRCheckBox cb = (XRCheckBox)sender;
        //cb.Checked = e.Value.ToString().ToUpper() == "S";
        if (e.Value.ToString().ToUpper() != "S")
            e.Value = CheckState.Checked;
        else
            e.Value = CheckState.Unchecked;
    }

    private void cbNao_EvaluateBinding(object sender, BindingEventArgs e)
    {
        //XRCheckBox cb = (XRCheckBox)sender;
        //cb.Checked = e.Value.ToString().ToUpper() == "N";
        if (e.Value.ToString().ToUpper() != "N")
            e.Value = CheckState.Checked;
        else
            e.Value = CheckState.Unchecked;
    }

    private void cbNaoSeAplica_EvaluateBinding(object sender, BindingEventArgs e)
    {
        //XRCheckBox cb = (XRCheckBox)sender;
        //cb.Checked = e.Value.ToString().ToUpper() == "X";
        if (e.Value.ToString().ToUpper() != "X")
            e.Value = CheckState.Checked;
        else
            e.Value = CheckState.Unchecked;
    }

    private void cell_EvaluateBinding(object sender, BindingEventArgs e)
    {
        if (e.Value.ToString().ToUpper() == "P")
            e.Value = "Parcial";
        else if (e.Value.ToString().ToUpper() == "T")
            e.Value = "Total";
    }
}
