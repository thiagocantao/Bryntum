using DevExpress.XtraReports.UI;
using System;
using System.Data;

/// <summary>
/// Summary description for rel_StatusReport
/// </summary>
public class rel_StatusReport : DevExpress.XtraReports.UI.XtraReport
{
    #region Fields
    private DevExpress.XtraReports.UI.DetailBand DetailGeral;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRTable xrtComentarioGeral;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell6;
    private XRLabel xrLabel9;
    private XRTable xrTable3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private DetailReportBand DetailReportFisico;
    private DetailBand Detail2;
    private DetailReportBand DetailReportTarefasConcluidas;
    private DetailBand Detail3;
    private GroupHeaderBand GroupHeader2;
    private DetailReportBand DetailReportTarefasAtrasadas;
    private DetailBand Detail1;
    private XRLabel xrLabel10;
    private XRTable xrTable4;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTable xrTable5;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private GroupHeaderBand GroupHeader1;
    private DetailReportBand DetailReportTarefasFuturas;
    private DetailBand Detail4;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel11;
    private XRTable xrTable6;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTable xrTable7;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private GroupFooterBand GroupFooterComentarioFisico;
    private DetailReportBand DetailReportFinanceiro;
    private DetailBand Detail5;
    private XRTable xrTable8;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell35;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell36;
    private XRLabel xrLabel12;
    private DetailReportBand DetailReportItemCusto;
    private DetailBand Detail6;
    private XRTable xrTable10;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private GroupHeaderBand GroupHeader4;
    private XRTable xrTable9;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRLabel xrLabel13;
    private DetailReportBand DetailReportItemReceita;
    private DetailBand Detail7;
    private GroupHeaderBand GroupHeader5;
    private XRTable xrTable12;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRLabel xrLabel14;
    private XRTable xrTable11;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell51;
    private DetailReportBand DetailReportMarcos;
    private DetailBand Detail8;
    private XRTable xrTable13;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private GroupHeaderBand GroupHeader6;
    private XRTable xrTable14;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private XRLabel xrLabel15;
    private dsStatusReport ds;
    private GroupFooterBand GroupFooter2;
    private XRTable xrTable15;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell73;
    private GroupFooterBand GroupFooter3;
    private XRTable xrTable16;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell78;
    private GroupFooterBand GroupFooterAnaliseValorAgregado;
    private XRTable xrTable17;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell79;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell80;
    private XRTable xrTable18;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell83;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell84;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell86;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell87;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell89;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell91;
    private XRTableCell xrTableCell92;
    private XRLabel xrLabel16;
    private DetailReportBand DetailReportRiscos;
    private DetailBand Detail9;
    private DetailReportBand DetailReportQuestoes;
    private DetailBand Detail10;
    private DetailReportBand DetailReportMetas;
    private DetailBand Detail11;
    private DetailReportBand DetailReportToDoList;
    private DetailBand Detail12;
    private DetailReportBand DetailReportContratos;
    private DetailBand Detail13;
    private XRLabel xrLabel18;
    private DetailReportBand DetailReportListaContrato;
    private DetailBand Detail14;
    private GroupHeaderBand GroupHeader7;
    private XRTable xrTable19;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell94;
    private XRTableCell xrTableCell95;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTable xrTable20;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell100;
    private XRTableCell xrTableCell101;
    private XRTableCell xrTableCell102;
    private XRTableCell xrTableCell103;
    private XRTableCell xrTableCell104;
    private GroupFooterBand GroupFooter5;
    private XRTable xrTable21;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell106;
    private XRTableCell xrTableCell108;
    private XRTableCell xrTableCell109;
    private XRTableCell xrTableCell110;
    private XRLabel xrLabel19;
    private DetailReportBand DetailReportListaPendencia;
    private DetailBand Detail15;
    private XRTable xrTable23;
    private XRTableRow xrTableRow29;
    private XRTableCell xrTableCell114;
    private XRTableCell xrTableCell115;
    private XRTableCell xrTableCell116;
    private XRTableCell xrTableCell117;
    private XRTableCell xrTableCell118;
    private GroupHeaderBand GroupHeader8;
    private XRTable xrTable22;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell105;
    private XRTableCell xrTableCell107;
    private XRTableCell xrTableCell111;
    private XRTableCell xrTableCell112;
    private XRTableCell xrTableCell113;
    private XRLabel xrLabel17;
    private DetailReportBand DetailReportListaMeta;
    private DetailBand Detail16;
    private XRTable xrTable25;
    private XRTableRow xrTableRow31;
    private XRTableCell xrTableCell127;
    private XRTableCell xrTableCell128;
    private XRTableCell xrTableCell129;
    private XRTableCell xrTableCell130;
    private XRTableCell xrTableCell131;
    private XRTableCell xrTableCell132;
    private GroupHeaderBand GroupHeader9;
    private XRTable xrTable24;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTableCell120;
    private XRTableCell xrTableCell121;
    private XRTableCell xrTableCell122;
    private XRTableCell xrTableCell123;
    private XRTableCell xrTableCell124;
    private XRTableCell xrTableCell125;
    private XRLabel xrLabel20;
    private DetailReportBand DetailReportListaQuestao;
    private DetailBand Detail17;
    private XRTable xrTable27;
    private XRTableRow xrTableRow33;
    private XRTableCell xrTableCell139;
    private XRTableCell xrTableCell140;
    private XRTableCell xrTableCell141;
    private XRTableCell xrTableCell142;
    private XRTableCell xrTableCell143;
    private XRTableCell xrTableCell144;
    private GroupHeaderBand GroupHeader10;
    private XRTable xrTable26;
    private XRTableRow xrTableRow32;
    private XRTableCell xrTableCell126;
    private XRTableCell xtcHeaderQuestoes;
    private XRTableCell xrTableCell134;
    private XRTableCell xrTableCell135;
    private XRTableCell xrTableCell136;
    private XRTableCell xrTableCell137;
    private GroupFooterBand GroupFooterComentarioQuestao;
    private XRTable xrTable28;
    private XRTableRow xrTableRow34;
    private XRTableCell xtcComentarioQuestoes;
    private XRTableRow xrTableRow35;
    private XRTableCell xrTableCell138;
    private GroupFooterBand GroupFooterComentarioMeta;
    private XRTable xrTable29;
    private XRTableRow xrTableRow36;
    private XRTableCell xrTableCell145;
    private XRTableRow xrTableRow37;
    private XRTableCell xrTableCell146;
    private GroupFooterBand GroupFooterComentarioRisco;
    private DetailReportBand DetailReportListaRisco;
    private DetailBand Detail18;
    private XRTable xrTable30;
    private XRTableRow xrTableRow38;
    private XRTableCell xrTableCell147;
    private XRTableRow xrTableRow39;
    private XRTableCell xrTableCell148;
    private XRLabel xrLabel21;
    private XRTable xrTable31;
    private XRTableRow xrTableRow40;
    private XRTableCell xrTableCell149;
    private XRTableCell xrTableCell150;
    private XRTableCell xrTableCell151;
    private XRTableCell xrTableCell152;
    private XRTableCell xrTableCell153;
    private XRTableCell xrTableCell154;
    private XRTable xrTable32;
    private XRTableRow xrTableRow41;
    private XRTableCell xrTableCell155;
    private XRTableCell xrTableCell156;
    private XRTableCell xrTableCell157;
    private XRTableCell xrTableCell158;
    private XRTableCell xrTableCell159;
    private XRTableCell xrTableCell160;
    private GroupHeaderBand GroupHeader11;
    private XRLabel xrLabel22;
    private XRLabel xrLabel23;
    private XRLabel xrLabel24;
    private XRLabel xrLabel25;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private GroupFooterBand GroupFooterComentarioFinanceiro;
    private XRTableCell xrTableCell133;
    private XRTable xrtPlanoAcao;
    private XRTableRow xrTableRow42;
    private XRTableCell xrTableCell119;
    private XRTableRow xrTableRow43;
    private XRTableCell xrTableCell161;
    private DetailReportBand DetailComentarioGeral;
    private DetailBand Detail;
    private DetailReportBand DetailPlanoAcao;
    private DetailBand Detail19;
    private XRPictureBox xrPictureBox3;
    private XRPictureBox xrPictureBox6;
    private DetailReportBand DetailDesempenhoFisico;
    private DetailBand Detail20;
    private XRLabel xrLabel31;
    private XRPictureBox xrPictureBox2;
    private XRLabel xrLabel32;
    private XRLabel xrLabel28;
    private XRLabel xrLabel29;
    private XRLabel xrLabel30;
    private DetailReportBand DetailDesempenhoFinanceiro;
    private DetailBand Detail21;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private XRLabel xrLabel37;
    private XRPictureBox xrPictureBox4;
    private XRLabel xrLabel33;
    private XRLabel xrLabel34;
    private GroupFooterBand GroupFooter4;
    private XRTable xrTable33;
    private XRTableRow xrTableRow45;
    private XRTableCell xrTableCell172;
    private XRPictureBox xrPictureBox11;
    private XRTableCell xrTableCell173;
    private XRTableCell xrTableCell174;
    private XRPictureBox xrPictureBox12;
    private XRTableCell xrTableCell175;
    private XRTableCell xrTableCell176;
    private XRPictureBox xrPictureBox13;
    private XRTableCell xrTableCell177;
    private XRTableCell xrTableCell178;
    private XRPictureBox xrPictureBox14;
    private XRTableCell xrTableCell179;
    private XRTableCell xrTableCell180;
    private XRPictureBox xrPictureBox15;
    private XRTableCell xrTableCell181;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow44;
    private XRTableCell xrTableCell162;
    private XRPictureBox xrPictureBox5;
    private XRTableCell xrTableCell163;
    private XRTableCell xrTableCell164;
    private XRPictureBox xrPictureBox7;
    private XRTableCell xrTableCell165;
    private XRTableCell xrTableCell166;
    private XRPictureBox xrPictureBox8;
    private XRTableCell xrTableCell167;
    private XRTableCell xrTableCell168;
    private XRPictureBox xrPictureBox9;
    private XRTableCell xrTableCell169;
    private XRTableCell xrTableCell170;
    private XRPictureBox xrPictureBox10;
    private XRTableCell xrTableCell171;
    #endregion
    private XRLabel lblDataAnalise;
    private XRRichText xrLabel38;
    private XRTableCell xrTableCell182;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell183;
    private XRTableCell xrTableCell185;
    private XRTableCell xrTableCell184;
    private XRTableCell xrTableCell186;
    private DetailReportBand DetailReportItemCusto2;
    private DetailBand Detail22;
    private GroupHeaderBand GroupHeader12;
    private GroupFooterBand GroupFooter6;
    private XRTable xrTable35;
    private XRTableRow xrTableRow46;
    private XRTableCell xrTableCell193;
    private XRTableCell xrTableCell194;
    private XRTableCell xrTableCell195;
    private XRTableCell xrTableCell197;
    private XRLabel xrLabel39;
    private XRTable xrTable34;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell187;
    private XRTableCell xrTableCell188;
    private XRTableCell xrTableCell189;
    private XRTableCell xrTableCell191;
    private XRTable xrTable36;
    private XRTableRow xrTableRow47;
    private XRTableCell xrTableCell199;
    private XRTableCell xrTableCell200;
    private XRTableCell xrTableCell201;
    private XRTableCell xrTableCell203;
    private XRLabel xrLabel40;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Constructors
    public rel_StatusReport()
    {
        InitializeComponent();
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

    private void InitializeLayout()
    {
        dados cDados = CdadosUtil.GetCdados(null); ;
        int codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelQuestao", "labelQuestoes", "labelToDoList", "lblGeneroLabelQuestao");
        if (dsParametros.Tables[0].Rows.Count > 0)
        {
            DataRow rowParam = dsParametros.Tables[0].Rows[0];
            string labelPlural = (string)rowParam["labelQuestoes"];
            string labelSingular = (string)rowParam["labelQuestao"];
            string labelToDoList = (string)rowParam["labelToDoList"];
            string generoLabel = (string)rowParam["lblGeneroLabelQuestao"];

            xrLabel19.Text = string.Format("{1} {0}", labelToDoList, labelPlural);

            xtcHeaderQuestoes.Text = labelSingular;
            xtcComentarioQuestoes.Text = string.Format(Resources.rel_StatusReport.ComentáriosSobreX1SX0DoProjeto, labelPlural.ToLower(), generoLabel == "F" ? Resources.rel_StatusReport.ArtigoDefinidoFeminino : Resources.rel_StatusReport.ArtigoDefinidoMasculino);
            xrLabel20.Text = labelPlural;


            xrTableCell133.Text = labelSingular;

        }

        dsStatusReport.StatusReportRow row = null;

        if (ds.StatusReport.Rows.Count > 0)
        {
            row = ds.StatusReport[0];
        }

        #region Variáveis
        bool mostraAnaliseValorAgregado = (row == null) ? false : row.AnaliseValorAgregado.Equals("S");
        bool mostraComentarioGeral = (row == null) ? false : row.ListaComentarioGeral.Equals("S");
        bool mostraTarefasConcluidas = (row == null) ? false : row.ListaTarefasConcluidas.Equals("S");
        bool mostraTarefasAtrasadas = (row == null) ? false : row.ListaTarefasAtrasadas.Equals("S");
        bool mostraTarefasFuturas = (row == null) ? false : row.ListaTarefasFuturas.Equals("S");
        bool mostraMarcosConcluidos = (row == null) ? false : row.ListaMarcosConcluidos.Equals("S");
        bool mostraMarcosAtrasados = (row == null) ? false : row.ListaMarcosAtrasados.Equals("S");
        bool mostraMarcosFuturos = (row == null) ? false : row.ListaMarcosFuturos.Equals("S");
        bool mostraComentarioFisico = (row == null) ? false : row.ListaComentarioFisico.Equals("S");
        bool mostraContasCusto = (row == null) ? false : row.ListaContasCusto.Equals("S");
        bool TASQUES_OcultarColunaCusto = (row == null) ? false : row.TASQUES_OcultarColunaCusto.Equals("S");
        bool mostraContasReceita = (row == null) ? false : row.ListaContasReceita.Equals("S");
        bool mostraComentarioFinanceiro = (row == null) ? false : row.ListaComentarioFinanceiro.Equals("S");
        bool mostraRiscosAtivos = (row == null) ? false : row.ListaRiscosAtivos.Equals("S");
        bool mostraRiscosEliminados = (row == null) ? false : row.ListaRiscosEliminados.Equals("S");
        bool mostraComentarioRisco = (row == null) ? false : row.ListaComentarioRisco.Equals("S");
        bool mostraQuestoesAtivas = (row == null) ? false : row.ListaQuestoesResolvidas.Equals("S");
        bool mostraQuestoesResolvidas = (row == null) ? false : row.ListaQuestoesAtivas.Equals("S");
        bool mostraComentarioQuestao = (row == null) ? false : row.ListaComentarioQuestao.Equals("S");
        bool mostraMetasResultados = (row == null) ? false : row.ListaMetasResultados.Equals("S");
        bool mostraComentarioMeta = (row == null) ? false : row.ListaComentarioMeta.Equals("S");
        bool mostraPendenciasToDoList = (row == null) ? false : row.ListaPendenciasToDoList.Equals("S");
        bool mostraContratos = (row == null) ? false : row.ListaContratos.Equals("S");
        bool mostraComentarioPlanoAcao = (row == null) ? false : row.ListaComentarioPlanoAcao.Equals("S");
        #endregion


        #region Outros Tópicos

        DetailComentarioGeral.Visible = mostraComentarioGeral;
        DetailPlanoAcao.Visible = mostraComentarioPlanoAcao;
        DetailReportToDoList.Visible = mostraPendenciasToDoList;
        DetailReportContratos.Visible = mostraContratos;

        #endregion

        #region Desempenho Físico

        DetailReportTarefasConcluidas.Visible = mostraTarefasConcluidas;
        DetailReportTarefasAtrasadas.Visible = mostraTarefasAtrasadas;
        DetailReportTarefasFuturas.Visible = mostraTarefasFuturas;
        DetailReportMarcos.Visible = mostraMarcosConcluidos || mostraMarcosAtrasados || mostraMarcosFuturos;
        GroupFooterComentarioFisico.Visible = mostraComentarioFisico;
        DetailReportFisico.Visible = mostraTarefasConcluidas || mostraTarefasAtrasadas || mostraTarefasFuturas ||
            mostraMarcosConcluidos || mostraMarcosAtrasados || mostraMarcosFuturos || mostraComentarioFisico;

        #endregion

        #region Desempenho Financeiro

        DetailReportItemCusto.Visible = mostraContasCusto && !TASQUES_OcultarColunaCusto;
        DetailReportItemCusto2.Visible = mostraContasCusto && TASQUES_OcultarColunaCusto;
        DetailReportItemReceita.Visible = mostraContasReceita;
        GroupFooterAnaliseValorAgregado.Visible = mostraAnaliseValorAgregado;
        GroupFooterComentarioFinanceiro.Visible = mostraComentarioFinanceiro;
        DetailReportFinanceiro.Visible = mostraContasCusto || mostraContasReceita || mostraComentarioFinanceiro || mostraAnaliseValorAgregado;

        #endregion

        #region Riscos

        DetailReportListaRisco.Visible = mostraRiscosAtivos || mostraRiscosEliminados;
        GroupFooterComentarioRisco.Visible = mostraComentarioRisco;
        DetailReportRiscos.Visible = mostraRiscosAtivos || mostraRiscosEliminados || mostraComentarioRisco;

        #endregion

        #region Questões

        DetailReportListaQuestao.Visible = mostraQuestoesAtivas || mostraQuestoesResolvidas;
        GroupFooterComentarioQuestao.Visible = mostraComentarioQuestao;
        DetailReportQuestoes.Visible = mostraComentarioQuestao || mostraQuestoesAtivas || mostraQuestoesResolvidas;

        #endregion

        #region Metas

        DetailReportListaMeta.Visible = mostraMetasResultados;
        GroupFooterComentarioMeta.Visible = mostraComentarioMeta;
        DetailReportMetas.Visible = mostraMetasResultados || mostraComentarioMeta;

        #endregion
    }
    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "rel_StatusReport.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_StatusReport.ResourceManager;
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
        DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary13 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary14 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary15 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary16 = new DevExpress.XtraReports.UI.XRSummary();
        this.DetailGeral = new DevExpress.XtraReports.UI.DetailBand();
        this.xrtPlanoAcao = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow42 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell119 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow43 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell161 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ds = new dsStatusReport();
        this.xrtComentarioGeral = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportFisico = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.DetailReportTarefasConcluidas = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.DetailReportTarefasAtrasadas = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportTarefasFuturas = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooterComentarioFisico = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportMarcos = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail8 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox6 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter4 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable33 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow45 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell172 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox11 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell173 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell174 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox12 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell175 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell176 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox13 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell177 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell178 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox14 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell179 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell180 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox15 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell181 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailDesempenhoFisico = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail20 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.DetailReportFinanceiro = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportItemCusto = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell182 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell183 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportItemReceita = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell185 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell184 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell186 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooterAnaliseValorAgregado = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable18 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooterComentarioFinanceiro = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailDesempenhoFinanceiro = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail21 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportItemCusto2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail22 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable35 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow46 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell193 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell194 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell195 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell197 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader12 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable34 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell187 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell188 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell189 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell191 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter6 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable36 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow47 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell199 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell200 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell201 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell203 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportRiscos = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail9 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooterComentarioRisco = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable30 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow38 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell147 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow39 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell148 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportListaRisco = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail18 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable32 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow41 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell155 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell156 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell157 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell158 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell159 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell160 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader11 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable31 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow40 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell149 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell150 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell151 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell152 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell153 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell154 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportQuestoes = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail10 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportListaQuestao = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail17 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable27 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow33 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell139 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell140 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell141 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell142 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell143 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell144 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader10 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable26 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell126 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xtcHeaderQuestoes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell134 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell135 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell136 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell137 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooterComentarioQuestao = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable28 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow34 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xtcComentarioQuestoes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell138 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportMetas = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail11 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportListaMeta = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail16 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable25 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell127 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell128 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell129 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell130 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell131 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell132 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader9 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable24 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell120 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell122 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell123 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell125 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow44 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell162 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox5 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell163 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell164 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox7 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell165 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell166 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox8 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell167 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell168 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox9 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell169 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell170 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox10 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableCell171 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooterComentarioMeta = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable29 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow36 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell145 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow37 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell146 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportToDoList = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail12 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportListaPendencia = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail15 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable23 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell114 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell115 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell117 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader8 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable22 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell113 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReportContratos = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail13 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReportListaContrato = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail14 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable20 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader7 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable19 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter5 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable21 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell108 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell133 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailComentarioGeral = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRRichText();
        this.lblDataAnalise = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailPlanoAcao = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail19 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrtPlanoAcao)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrtComentarioGeral)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable33)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable35)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable34)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable36)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable30)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable32)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable31)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrLabel38)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // DetailGeral
        // 
        this.DetailGeral.HeightF = 0F;
        this.DetailGeral.Name = "DetailGeral";
        this.DetailGeral.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.DetailGeral.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrtPlanoAcao
        // 
        this.xrtPlanoAcao.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrtPlanoAcao.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
        this.xrtPlanoAcao.Name = "xrtPlanoAcao";
        this.xrtPlanoAcao.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow42,
            this.xrTableRow43});
        this.xrtPlanoAcao.SizeF = new System.Drawing.SizeF(747F, 40F);
        this.xrtPlanoAcao.StylePriority.UseBorders = false;
        // 
        // xrTableRow42
        // 
        this.xrTableRow42.BackColor = System.Drawing.Color.Silver;
        this.xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell119});
        this.xrTableRow42.Name = "xrTableRow42";
        this.xrTableRow42.StylePriority.UseBackColor = false;
        this.xrTableRow42.Weight = 9.2D;
        // 
        // xrTableCell119
        // 
        this.xrTableCell119.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell119.Name = "xrTableCell119";
        this.xrTableCell119.StylePriority.UseFont = false;
        this.xrTableCell119.StylePriority.UseTextAlignment = false;
        this.xrTableCell119.Text = Resources.rel_StatusReport.PlanoDeAçãoProposto;
        this.xrTableCell119.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell119.Weight = 1.3037809647979139D;
        // 
        // xrTableRow43
        // 
        this.xrTableRow43.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell161});
        this.xrTableRow43.Name = "xrTableRow43";
        this.xrTableRow43.Weight = 9.2D;
        // 
        // xrTableCell161
        // 
        this.xrTableCell161.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ComentarioPlanoAcao")});
        this.xrTableCell161.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell161.Multiline = true;
        this.xrTableCell161.Name = "xrTableCell161";
        this.xrTableCell161.StylePriority.UseFont = false;
        this.xrTableCell161.StylePriority.UseTextAlignment = false;
        this.xrTableCell161.Text = "xrTableCell4";
        this.xrTableCell161.Weight = 1.3037809647979139D;
        // 
        // ds
        // 
        this.ds.DataSetName = "dsStatusReport";
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrtComentarioGeral
        // 
        this.xrtComentarioGeral.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrtComentarioGeral.LocationFloat = new DevExpress.Utils.PointFloat(9.999497F, 10.00001F);
        this.xrtComentarioGeral.Name = "xrtComentarioGeral";
        this.xrtComentarioGeral.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrtComentarioGeral.SizeF = new System.Drawing.SizeF(747F, 20F);
        this.xrtComentarioGeral.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.BackColor = System.Drawing.Color.Silver;
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.StylePriority.UseBackColor = false;
        this.xrTableRow2.Weight = 9.2D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = Resources.rel_StatusReport.AnáliseCrítica;
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell1.Weight = 1.3037809647979139D;
        // 
        // xrLabel8
        // 
        this.xrLabel8.BackColor = System.Drawing.Color.Silver;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 10.00001F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(746.9998F, 22.99998F);
        this.xrLabel8.StylePriority.UseBackColor = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = Resources.rel_StatusReport.DesempenhoFísico;
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.TopMargin.HeightF = 30F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.StylePriority.UseFont = false;
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 30F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(262.5F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(233.5417F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = Resources.rel_StatusReport.RelatórioDeStatusDeProjeto;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(200F, 32.99996F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(68.96F, 18F);
        this.xrLabel2.StylePriority.UseBackColor = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = Resources.rel_StatusReport.Projeto;
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.NomeProjeto")});
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(268.96F, 32.99996F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(488.0412F, 18F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(200F, 70.99997F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(68.96F, 20F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = Resources.rel_StatusReport.Unidade;
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.UnidadeProjeto")});
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(268.9609F, 70.99997F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(488.04F, 20F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "StatusReport.LogoEntidade")});
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 32.99999F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(177F, 78F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(200F, 90.99998F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(68.96F, 20F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = Resources.rel_StatusReport.Gerente;
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.GerenteProjeto")});
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(268.9595F, 90.99998F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(488.04F, 20F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "xrLabel7";
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable2.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell10,
            this.xrTableCell11});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 11.5D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasConcluidas.NomeTarefa")});
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 0.37699690548373416D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasConcluidas.InicioPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell3.Weight = 0.12566563516124474D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasConcluidas.TerminoPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "[StatusReport_TarefasConcluidas.TerminoPrevisto]";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell5.Weight = 0.12566563516124474D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasConcluidas.InicioReal", "{0:dd/MM/yyyy}")});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "[StatusReport_TarefasConcluidas.InicioReal]";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell6.Weight = 0.12566563516124471D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasConcluidas.TerminoReal", "{0:dd/MM/yyyy}")});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell10.Weight = 0.12566563516124474D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasConcluidas.StringAlocacaoRecursoTarefa")});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 0.29374342218940963D;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 10.00001F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(747F, 22.99999F);
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = Resources.rel_StatusReport.TarefasConcluídas;
        // 
        // xrTable3
        // 
        this.xrTable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 32.99999F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable3.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable3.StylePriority.UseBackColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 11.5D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = Resources.rel_StatusReport.Tarefa;
        this.xrTableCell7.Weight = 0.37699690548373416D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = Resources.rel_StatusReport.InícioPrevisto;
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell8.Weight = 0.12566563516124474D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = Resources.rel_StatusReport.TérminoPrevisto;
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell9.Weight = 0.12566563516124474D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = Resources.rel_StatusReport.InícioReal;
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell12.Weight = 0.12566563516124471D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = Resources.rel_StatusReport.TérminoReal;
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell13.Weight = 0.12566563516124474D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = Resources.rel_StatusReport.Responsável;
        this.xrTableCell14.Weight = 0.29374342218940963D;
        // 
        // DetailReportFisico
        // 
        this.DetailReportFisico.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.DetailReportTarefasConcluidas,
            this.DetailReportTarefasAtrasadas,
            this.DetailReportTarefasFuturas,
            this.GroupFooterComentarioFisico,
            this.DetailReportMarcos,
            this.DetailDesempenhoFisico});
        this.DetailReportFisico.DataMember = "StatusReport";
        this.DetailReportFisico.DataSource = this.ds;
        this.DetailReportFisico.Expanded = false;
        this.DetailReportFisico.Level = 2;
        this.DetailReportFisico.Name = "DetailReportFisico";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
        this.Detail2.Expanded = false;
        this.Detail2.HeightF = 32.99999F;
        this.Detail2.Name = "Detail2";
        // 
        // DetailReportTarefasConcluidas
        // 
        this.DetailReportTarefasConcluidas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader2});
        this.DetailReportTarefasConcluidas.DataMember = "StatusReport.StatusReport_TarefasConcluidas";
        this.DetailReportTarefasConcluidas.DataSource = this.ds;
        this.DetailReportTarefasConcluidas.Expanded = false;
        this.DetailReportTarefasConcluidas.Level = 1;
        this.DetailReportTarefasConcluidas.Name = "DetailReportTarefasConcluidas";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail3.HeightF = 15F;
        this.Detail3.Name = "Detail3";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrTable3});
        this.GroupHeader2.HeightF = 62.99999F;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.RepeatEveryPage = true;
        // 
        // DetailReportTarefasAtrasadas
        // 
        this.DetailReportTarefasAtrasadas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1});
        this.DetailReportTarefasAtrasadas.DataMember = "StatusReport.StatusReport_TarefasAtrasadas";
        this.DetailReportTarefasAtrasadas.DataSource = this.ds;
        this.DetailReportTarefasAtrasadas.Expanded = false;
        this.DetailReportTarefasAtrasadas.Level = 2;
        this.DetailReportTarefasAtrasadas.Name = "DetailReportTarefasAtrasadas";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.Detail1.HeightF = 15F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable5.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell20,
            this.xrTableCell23,
            this.xrTableCell24});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 11.5D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasAtrasadas.NomeTarefa")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "xrTableCell17";
        this.xrTableCell15.Weight = 0.42412152377101048D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasAtrasadas.PercentualReal", "{0} %")});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "xrTableCell18";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell16.Weight = 0.13351978019464844D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasAtrasadas.InicioPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "xrTableCell19";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell20.Weight = 0.12566563785654294D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasAtrasadas.TerminoPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.Text = "xrTableCell21";
        this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell23.Weight = 0.12566562986693508D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasAtrasadas.StringAlocacaoRecursoTarefa")});
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "xrTableCell22";
        this.xrTableCell24.Weight = 0.36443029662898563D;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrTable4});
        this.GroupHeader1.HeightF = 63.00002F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 10.00001F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(747F, 22.99999F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = Resources.rel_StatusReport.TarefasAtrasadas;
        // 
        // xrTable4
        // 
        this.xrTable4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 33.00002F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable4.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable4.StylePriority.UseBackColor = false;
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell21,
            this.xrTableCell22});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 11.5D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = Resources.rel_StatusReport.Tarefa1;
        this.xrTableCell17.Weight = 0.42412152377101048D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = Resources.rel_StatusReport.Concluído;
        this.xrTableCell18.Weight = 0.13351978019464844D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = Resources.rel_StatusReport.InícioPrevisto1;
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell19.Weight = 0.12566563785654294D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = Resources.rel_StatusReport.TérminoPrevisto1;
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell21.Weight = 0.12566562986693508D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = Resources.rel_StatusReport.Responsável1;
        this.xrTableCell22.Weight = 0.36443029662898563D;
        // 
        // DetailReportTarefasFuturas
        // 
        this.DetailReportTarefasFuturas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupHeader3});
        this.DetailReportTarefasFuturas.DataMember = "StatusReport.StatusReport_TarefasProximoPeriodo";
        this.DetailReportTarefasFuturas.DataSource = this.ds;
        this.DetailReportTarefasFuturas.Expanded = false;
        this.DetailReportTarefasFuturas.Level = 3;
        this.DetailReportTarefasFuturas.Name = "DetailReportTarefasFuturas";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.Detail4.HeightF = 15F;
        this.Detail4.Name = "Detail4";
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable7.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseFont = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 11.5D;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasProximoPeriodo.NomeTarefa")});
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.Text = "xrTableCell17";
        this.xrTableCell30.Weight = 0.42412152377101048D;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasProximoPeriodo.PercentualReal", "{0} %")});
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "xrTableCell18";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell31.Weight = 0.13351978019464844D;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasProximoPeriodo.InicioPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseTextAlignment = false;
        this.xrTableCell32.Text = "xrTableCell19";
        this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell32.Weight = 0.12566563785654294D;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasProximoPeriodo.TerminoPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseTextAlignment = false;
        this.xrTableCell33.Text = "xrTableCell21";
        this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell33.Weight = 0.12566562986693508D;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasProximoPeriodo.StringAlocacaoRecursoTarefa")});
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Text = "xrTableCell22";
        this.xrTableCell34.Weight = 0.36443029662898563D;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.xrTable6});
        this.GroupHeader3.HeightF = 62.99999F;
        this.GroupHeader3.Name = "GroupHeader3";
        this.GroupHeader3.RepeatEveryPage = true;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 10F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(747F, 22.99999F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = Resources.rel_StatusReport.TarefasParaOPróximoPeríodo;
        // 
        // xrTable6
        // 
        this.xrTable6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 32.99999F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable6.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable6.StylePriority.UseBackColor = false;
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseFont = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell29});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 11.5D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = Resources.rel_StatusReport.Tarefa3;
        this.xrTableCell25.Weight = 0.42412152377101048D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Text = Resources.rel_StatusReport.Concluído1;
        this.xrTableCell26.Weight = 0.13351978019464844D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = Resources.rel_StatusReport.InícioPrevisto2;
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell27.Weight = 0.12566563785654294D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseTextAlignment = false;
        this.xrTableCell28.Text = Resources.rel_StatusReport.TérminoPrevisto2;
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell28.Weight = 0.12566562986693508D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Text = Resources.rel_StatusReport.Responsável2;
        this.xrTableCell29.Weight = 0.36443029662898563D;
        // 
        // GroupFooterComentarioFisico
        // 
        this.GroupFooterComentarioFisico.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.GroupFooterComentarioFisico.Expanded = false;
        this.GroupFooterComentarioFisico.HeightF = 60F;
        this.GroupFooterComentarioFisico.Name = "GroupFooterComentarioFisico";
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(10.00036F, 10.00001F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9,
            this.xrTableRow10});
        this.xrTable8.SizeF = new System.Drawing.SizeF(747F, 40F);
        this.xrTable8.StylePriority.UseBorders = false;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.BackColor = System.Drawing.Color.Silver;
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35});
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.StylePriority.UseBackColor = false;
        this.xrTableRow9.Weight = 9.2D;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.StylePriority.UseTextAlignment = false;
        this.xrTableCell35.Text = Resources.rel_StatusReport.ComentárioSobreODesempenhoFísicoDoProjeto;
        this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell35.Weight = 1.3037809647979139D;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 9.2D;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ComentarioFisico")});
        this.xrTableCell36.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell36.Multiline = true;
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseFont = false;
        this.xrTableCell36.StylePriority.UseTextAlignment = false;
        this.xrTableCell36.Text = "xrTableCell36";
        this.xrTableCell36.Weight = 1.3037809647979139D;
        // 
        // DetailReportMarcos
        // 
        this.DetailReportMarcos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail8,
            this.GroupHeader6,
            this.GroupFooter4});
        this.DetailReportMarcos.DataMember = "StatusReport.StatusReport_Marcos";
        this.DetailReportMarcos.DataSource = this.ds;
        this.DetailReportMarcos.Expanded = false;
        this.DetailReportMarcos.Level = 4;
        this.DetailReportMarcos.Name = "DetailReportMarcos";
        this.DetailReportMarcos.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail8
        // 
        this.Detail8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
        this.Detail8.HeightF = 17F;
        this.Detail8.Name = "Detail8";
        // 
        // xrTable13
        // 
        this.xrTable13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(9.999721F, 0F);
        this.xrTable13.Name = "xrTable13";
        this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
        this.xrTable13.SizeF = new System.Drawing.SizeF(747F, 17F);
        this.xrTable13.StylePriority.UseBorders = false;
        this.xrTable13.StylePriority.UseFont = false;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell57,
            this.xrTableCell60,
            this.xrTableCell58,
            this.xrTableCell61,
            this.xrTableCell59,
            this.xrTableCell62});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 1D;
        // 
        // xrTableCell57
        // 
        this.xrTableCell57.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox6});
        this.xrTableCell57.Name = "xrTableCell57";
        this.xrTableCell57.StylePriority.UseTextAlignment = false;
        this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell57.Weight = 0.18072289156626506D;
        // 
        // xrPictureBox6
        // 
        this.xrPictureBox6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "StatusReport.StatusReport_Marcos.StatusTarefa", "~/imagens/{0}.gif")});
        this.xrPictureBox6.LocationFloat = new DevExpress.Utils.PointFloat(15F, 1F);
        this.xrPictureBox6.Name = "xrPictureBox6";
        this.xrPictureBox6.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox6.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox6.StylePriority.UseBorders = false;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Marcos.Marco")});
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.Text = "xrTableCell60";
        this.xrTableCell60.Weight = 1.5341365461847392D;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Marcos.TerminoPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.StylePriority.UseTextAlignment = false;
        this.xrTableCell58.Text = "xrTableCell58";
        this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell58.Weight = 0.321285140562249D;
        // 
        // xrTableCell61
        // 
        this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Marcos.TerminoReprogramado", "{0:dd/MM/yyyy}")});
        this.xrTableCell61.Name = "xrTableCell61";
        this.xrTableCell61.StylePriority.UseTextAlignment = false;
        this.xrTableCell61.Text = "xrTableCell61";
        this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell61.Weight = 0.321285140562249D;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Marcos.TerminoReal", "{0:dd/MM/yyyy}")});
        this.xrTableCell59.Multiline = true;
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.StylePriority.UseTextAlignment = false;
        this.xrTableCell59.Text = "105\r\n";
        this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell59.Weight = 0.321285140562249D;
        // 
        // xrTableCell62
        // 
        this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Marcos.Desvio")});
        this.xrTableCell62.Name = "xrTableCell62";
        this.xrTableCell62.StylePriority.UseTextAlignment = false;
        this.xrTableCell62.Text = "xrTableCell62";
        this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell62.Weight = 0.321285140562249D;
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14,
            this.xrLabel15});
        this.GroupHeader6.HeightF = 63F;
        this.GroupHeader6.Name = "GroupHeader6";
        this.GroupHeader6.RepeatEveryPage = true;
        // 
        // xrTable14
        // 
        this.xrTable14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(10.00036F, 33F);
        this.xrTable14.Name = "xrTable14";
        this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
        this.xrTable14.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable14.StylePriority.UseBackColor = false;
        this.xrTable14.StylePriority.UseBorders = false;
        this.xrTable14.StylePriority.UseFont = false;
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
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 1D;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.Text = Resources.rel_StatusReport.Status;
        this.xrTableCell63.Weight = 0.18072289156626506D;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.Text = Resources.rel_StatusReport.Marco;
        this.xrTableCell64.Weight = 1.5341365461847389D;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.StylePriority.UseTextAlignment = false;
        this.xrTableCell65.Text = Resources.rel_StatusReport.TérminoPrevisto3;
        this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell65.Weight = 0.32128514056224894D;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.StylePriority.UseTextAlignment = false;
        this.xrTableCell66.Text = Resources.rel_StatusReport.TérminoReprogr;
        this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell66.Weight = 0.321285140562249D;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.Text = Resources.rel_StatusReport.TérminoReal1;
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell67.Weight = 0.321285140562249D;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseTextAlignment = false;
        this.xrTableCell68.Text = Resources.rel_StatusReport.DesvioD;
        this.xrTableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell68.Weight = 0.32128514056224905D;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 10.00001F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(747F, 22.99999F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = Resources.rel_StatusReport.Marcos;
        // 
        // GroupFooter4
        // 
        this.GroupFooter4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable33});
        this.GroupFooter4.HeightF = 25F;
        this.GroupFooter4.Name = "GroupFooter4";
        // 
        // xrTable33
        // 
        this.xrTable33.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable33.LocationFloat = new DevExpress.Utils.PointFloat(10F, 5F);
        this.xrTable33.Name = "xrTable33";
        this.xrTable33.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow45});
        this.xrTable33.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable33.StylePriority.UseFont = false;
        // 
        // xrTableRow45
        // 
        this.xrTableRow45.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell172,
            this.xrTableCell173,
            this.xrTableCell174,
            this.xrTableCell175,
            this.xrTableCell176,
            this.xrTableCell177,
            this.xrTableCell178,
            this.xrTableCell179,
            this.xrTableCell180,
            this.xrTableCell181});
        this.xrTableRow45.Name = "xrTableRow45";
        this.xrTableRow45.Weight = 0.67999999999999994D;
        // 
        // xrTableCell172
        // 
        this.xrTableCell172.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox11});
        this.xrTableCell172.Name = "xrTableCell172";
        this.xrTableCell172.StylePriority.UseTextAlignment = false;
        this.xrTableCell172.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell172.Weight = 0.060240961968618251D;
        // 
        // xrPictureBox11
        // 
        this.xrPictureBox11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox11.ImageUrl = "~\\imagens\\VerdeOK.gif";
        this.xrPictureBox11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.178914E-05F);
        this.xrPictureBox11.Name = "xrPictureBox11";
        this.xrPictureBox11.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox11.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox11.StylePriority.UseBorders = false;
        // 
        // xrTableCell173
        // 
        this.xrTableCell173.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell173.Name = "xrTableCell173";
        this.xrTableCell173.StylePriority.UseFont = false;
        this.xrTableCell173.StylePriority.UseTextAlignment = false;
        this.xrTableCell173.Text = Resources.rel_StatusReport.ConcluídoNoPrazo;
        this.xrTableCell173.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell173.Weight = 0.53975904415940945D;
        // 
        // xrTableCell174
        // 
        this.xrTableCell174.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox12});
        this.xrTableCell174.Name = "xrTableCell174";
        this.xrTableCell174.Weight = 0.060240963855421686D;
        // 
        // xrPictureBox12
        // 
        this.xrPictureBox12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox12.ImageUrl = "~\\imagens\\VermelhoOK.gif";
        this.xrPictureBox12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox12.Name = "xrPictureBox12";
        this.xrPictureBox12.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox12.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox12.StylePriority.UseBorders = false;
        // 
        // xrTableCell175
        // 
        this.xrTableCell175.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell175.Name = "xrTableCell175";
        this.xrTableCell175.StylePriority.UseFont = false;
        this.xrTableCell175.StylePriority.UseTextAlignment = false;
        this.xrTableCell175.Text = Resources.rel_StatusReport.ConcluídoComAtraso;
        this.xrTableCell175.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell175.Weight = 0.53975904227260607D;
        // 
        // xrTableCell176
        // 
        this.xrTableCell176.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox13});
        this.xrTableCell176.Name = "xrTableCell176";
        this.xrTableCell176.Weight = 0.060240963855421686D;
        // 
        // xrPictureBox13
        // 
        this.xrPictureBox13.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox13.ImageUrl = "~\\imagens\\verde.gif";
        this.xrPictureBox13.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 3.178914E-05F);
        this.xrPictureBox13.Name = "xrPictureBox13";
        this.xrPictureBox13.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox13.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox13.StylePriority.UseBorders = false;
        // 
        // xrTableCell177
        // 
        this.xrTableCell177.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell177.Name = "xrTableCell177";
        this.xrTableCell177.StylePriority.UseFont = false;
        this.xrTableCell177.StylePriority.UseTextAlignment = false;
        this.xrTableCell177.Text = Resources.rel_StatusReport.NoPrazoAdiantado;
        this.xrTableCell177.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell177.Weight = 0.539759042272606D;
        // 
        // xrTableCell178
        // 
        this.xrTableCell178.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox14});
        this.xrTableCell178.Name = "xrTableCell178";
        this.xrTableCell178.Weight = 0.060240963855421714D;
        // 
        // xrPictureBox14
        // 
        this.xrPictureBox14.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox14.ImageUrl = "~\\imagens\\amarelo.gif";
        this.xrPictureBox14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox14.Name = "xrPictureBox14";
        this.xrPictureBox14.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox14.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox14.StylePriority.UseBorders = false;
        // 
        // xrTableCell179
        // 
        this.xrTableCell179.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell179.Name = "xrTableCell179";
        this.xrTableCell179.StylePriority.UseFont = false;
        this.xrTableCell179.StylePriority.UseTextAlignment = false;
        this.xrTableCell179.Text = Resources.rel_StatusReport.TendênciaDeAtraso;
        this.xrTableCell179.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell179.Weight = 0.53975904227260607D;
        // 
        // xrTableCell180
        // 
        this.xrTableCell180.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox15});
        this.xrTableCell180.Name = "xrTableCell180";
        this.xrTableCell180.Weight = 0.060240963855421742D;
        // 
        // xrPictureBox15
        // 
        this.xrPictureBox15.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox15.ImageUrl = "~\\imagens\\vermelho.gif";
        this.xrPictureBox15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.178914E-05F);
        this.xrPictureBox15.Name = "xrPictureBox15";
        this.xrPictureBox15.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox15.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox15.StylePriority.UseBorders = false;
        // 
        // xrTableCell181
        // 
        this.xrTableCell181.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell181.Name = "xrTableCell181";
        this.xrTableCell181.StylePriority.UseFont = false;
        this.xrTableCell181.StylePriority.UseTextAlignment = false;
        this.xrTableCell181.Text = Resources.rel_StatusReport.Atrasado;
        this.xrTableCell181.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell181.Weight = 0.53975901163246742D;
        // 
        // DetailDesempenhoFisico
        // 
        this.DetailDesempenhoFisico.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail20});
        this.DetailDesempenhoFisico.Expanded = false;
        this.DetailDesempenhoFisico.Level = 0;
        this.DetailDesempenhoFisico.Name = "DetailDesempenhoFisico";
        // 
        // Detail20
        // 
        this.Detail20.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel31,
            this.xrPictureBox2,
            this.xrLabel32,
            this.xrLabel28,
            this.xrLabel29,
            this.xrLabel30});
        this.Detail20.HeightF = 40F;
        this.Detail20.Name = "Detail20";
        // 
        // xrLabel31
        // 
        this.xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.PercentualFisicoReal", "{0:n2} %")});
        this.xrLabel31.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(600F, 0F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "xrLabel30";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "StatusReport.CorDesempenhoFisico", "~/imagens/Fisico{0}.png")});
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 5F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(30F, 30F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel32
        // 
        this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ResultadoFisico")});
        this.xrLabel32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(200F, 40F);
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        this.xrLabel32.Text = "xrLabel32";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel28
        // 
        this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.PercentualFisicoPrevisto", "{0:n2} %")});
        this.xrLabel28.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(400F, 0F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(125F, 40F);
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "xrLabel28";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel29
        // 
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(260F, 0F);
        this.xrLabel29.Multiline = true;
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(140F, 40F);
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = Resources.rel_StatusReport.PrevistoAtéAData;
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel30
        // 
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(525F, 0F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(75F, 40F);
        this.xrLabel30.StylePriority.UseTextAlignment = false;
        this.xrLabel30.Text = Resources.rel_StatusReport.Realizado;
        this.xrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel27,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel1,
            this.xrLabel6,
            this.xrLabel2,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrPictureBox1,
            this.xrLabel7});
        this.PageHeader.Expanded = false;
        this.PageHeader.HeightF = 120F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel27
        // 
        this.xrLabel27.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(516.5828F, 50.99996F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(90.41678F, 20F);
        this.xrLabel27.StylePriority.UseBackColor = false;
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = Resources.rel_StatusReport.GeradoEm;
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.DataGeracao", "{0:dd/MM/yy HH:mm}")});
        this.xrLabel26.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(606.9995F, 50.99996F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(150F, 20F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.Text = "xrLabel26";
        // 
        // xrLabel25
        // 
        this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.DataTerminoPeriodoRelatorio", "{0:dd/MM/yyyy}")});
        this.xrLabel25.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(371.961F, 50.99996F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(80F, 20F);
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.Text = "xrLabel25";
        // 
        // xrLabel24
        // 
        this.xrLabel24.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(348.9609F, 50.99996F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(23F, 20F);
        this.xrLabel24.StylePriority.UseBackColor = false;
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = Resources.rel_StatusReport.A;
        // 
        // xrLabel23
        // 
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.DataInicioPeriodoRelatorio", "{0:dd/MM/yyyy}")});
        this.xrLabel23.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(268.9609F, 50.99996F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(80F, 20F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.Text = "xrLabel23";
        // 
        // xrLabel22
        // 
        this.xrLabel22.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(200F, 50.99996F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(68.96F, 20F);
        this.xrLabel22.StylePriority.UseBackColor = false;
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = Resources.rel_StatusReport.Período;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.Expanded = false;
        this.PageFooter.HeightF = 32.99997F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Format = Resources.rel_StatusReport.PágX0DeX1;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(667F, 9.999974F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // DetailReportFinanceiro
        // 
        this.DetailReportFinanceiro.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.DetailReportItemCusto,
            this.DetailReportItemReceita,
            this.GroupFooterAnaliseValorAgregado,
            this.GroupFooterComentarioFinanceiro,
            this.DetailDesempenhoFinanceiro,
            this.DetailReportItemCusto2});
        this.DetailReportFinanceiro.DataMember = "StatusReport";
        this.DetailReportFinanceiro.DataSource = this.ds;
        this.DetailReportFinanceiro.Level = 3;
        this.DetailReportFinanceiro.Name = "DetailReportFinanceiro";
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.Detail5.HeightF = 32.99996F;
        this.Detail5.Name = "Detail5";
        // 
        // xrLabel12
        // 
        this.xrLabel12.BackColor = System.Drawing.Color.Silver;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 9.999974F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(746.9998F, 22.99998F);
        this.xrLabel12.StylePriority.UseBackColor = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = Resources.rel_StatusReport.DesempenhoFinanceiro;
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReportItemCusto
        // 
        this.DetailReportItemCusto.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.GroupHeader4,
            this.GroupFooter2});
        this.DetailReportItemCusto.DataMember = "StatusReport.StatusReport_ItensCusto";
        this.DetailReportItemCusto.DataSource = this.ds;
        this.DetailReportItemCusto.Level = 2;
        this.DetailReportItemCusto.Name = "DetailReportItemCusto";
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
        this.Detail6.HeightF = 15F;
        this.Detail6.Name = "Detail6";
        // 
        // xrTable10
        // 
        this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(10.0002F, 0F);
        this.xrTable10.Name = "xrTable10";
        this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
        this.xrTable10.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable10.StylePriority.UseBorders = false;
        this.xrTable10.StylePriority.UseFont = false;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell45,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell49,
            this.xrTableCell182,
            this.xrTableCell50});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 11.5D;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.ItemCusto")});
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.Text = "xrTableCell45";
        this.xrTableCell45.Weight = 3.4546144473240443D;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevisto", "{0:n2}")});
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.StylePriority.UseTextAlignment = false;
        this.xrTableCell47.Text = "xrTableCell47";
        this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell47.Weight = 1.3986293127559208D;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevistoAteData", "{0:n2}")});
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.StylePriority.UseTextAlignment = false;
        this.xrTableCell48.Text = "xrTableCell48";
        this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell48.Weight = 1.398629327999771D;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoReal", "{0:n2}")});
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.StylePriority.UseTextAlignment = false;
        this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell49.Weight = 1.3986293356216963D;
        // 
        // xrTableCell182
        // 
        this.xrTableCell182.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoRestante", "{0:n2}")});
        this.xrTableCell182.Name = "xrTableCell182";
        this.xrTableCell182.StylePriority.UseTextAlignment = false;
        this.xrTableCell182.Text = "xrTableCell182";
        this.xrTableCell182.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell182.Weight = 1.3986293356216963D;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.VariacaoCusto", "{0:n2}")});
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.StylePriority.UseTextAlignment = false;
        this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell50.Weight = 1.3986294347067219D;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9,
            this.xrLabel13});
        this.GroupHeader4.HeightF = 60F;
        this.GroupHeader4.Name = "GroupHeader4";
        this.GroupHeader4.RepeatEveryPage = true;
        // 
        // xrTable9
        // 
        this.xrTable9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(10F, 30F);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
        this.xrTable9.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable9.StylePriority.UseBackColor = false;
        this.xrTable9.StylePriority.UseBorders = false;
        this.xrTable9.StylePriority.UseFont = false;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell4,
            this.xrTableCell43});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 11.5D;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.Text = Resources.rel_StatusReport.ItemDeDespesa;
        this.xrTableCell38.Weight = 3.4546144473240443D;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseTextAlignment = false;
        this.xrTableCell40.Text = Resources.rel_StatusReport.DespesaPrevistaTOTAL;
        this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell40.Weight = 1.3986293127559213D;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseTextAlignment = false;
        this.xrTableCell41.Text = Resources.rel_StatusReport.DespesaPrevAtéAData;
        this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell41.Weight = 1.398629327999771D;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.StylePriority.UseTextAlignment = false;
        this.xrTableCell42.Text = Resources.rel_StatusReport.DespesaRealizada;
        this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell42.Weight = 1.3986293356216963D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = Resources.rel_StatusReport.DespesaRestante;
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell4.Weight = 1.3986293356216963D;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.StylePriority.UseTextAlignment = false;
        this.xrTableCell43.Text = Resources.rel_StatusReport.Desvio;
        this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell43.Weight = 1.3986294347067221D;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(9.999721F, 9.999974F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(747F, 20F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.Text = Resources.rel_StatusReport.DetalhesDosItensDeDespesaValoresEmR;
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel40,
            this.xrTable15});
        this.GroupFooter2.HeightF = 45F;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // xrTable15
        // 
        this.xrTable15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(10.00036F, 0F);
        this.xrTable15.Name = "xrTable15";
        this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
        this.xrTable15.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable15.StylePriority.UseBorders = false;
        this.xrTable15.StylePriority.UseFont = false;
        // 
        // xrTableRow17
        // 
        this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell72,
            this.xrTableCell183,
            this.xrTableCell73});
        this.xrTableRow17.Name = "xrTableRow17";
        this.xrTableRow17.Weight = 11.5D;
        // 
        // xrTableCell69
        // 
        this.xrTableCell69.Name = "xrTableCell69";
        this.xrTableCell69.Text = Resources.rel_StatusReport.TOTAL;
        this.xrTableCell69.Weight = 3.4546144473240443D;
        // 
        // xrTableCell70
        // 
        this.xrTableCell70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevisto")});
        this.xrTableCell70.Name = "xrTableCell70";
        this.xrTableCell70.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:n2}";
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell70.Summary = xrSummary1;
        this.xrTableCell70.Text = "xrTableCell70";
        this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell70.Weight = 1.3986293127559208D;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevistoAteData")});
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:n2}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell71.Summary = xrSummary2;
        this.xrTableCell71.Text = "xrTableCell71";
        this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell71.Weight = 1.398629327999771D;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoReal")});
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:n2}";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell72.Summary = xrSummary3;
        this.xrTableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell72.Weight = 1.3986293356216963D;
        // 
        // xrTableCell183
        // 
        this.xrTableCell183.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoRestante")});
        this.xrTableCell183.Name = "xrTableCell183";
        this.xrTableCell183.StylePriority.UseTextAlignment = false;
        xrSummary4.FormatString = "{0:n2}";
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell183.Summary = xrSummary4;
        this.xrTableCell183.Text = "xrTableCell183";
        this.xrTableCell183.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell183.Weight = 1.3986293356216963D;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.VariacaoCusto")});
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.StylePriority.UseTextAlignment = false;
        xrSummary5.FormatString = "{0:n2}";
        xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell73.Summary = xrSummary5;
        this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell73.Weight = 1.3986294347067219D;
        // 
        // DetailReportItemReceita
        // 
        this.DetailReportItemReceita.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.GroupHeader5,
            this.GroupFooter3});
        this.DetailReportItemReceita.DataMember = "StatusReport.StatusReport_ItensReceita";
        this.DetailReportItemReceita.DataSource = this.ds;
        this.DetailReportItemReceita.Expanded = false;
        this.DetailReportItemReceita.Level = 1;
        this.DetailReportItemReceita.Name = "DetailReportItemReceita";
        // 
        // Detail7
        // 
        this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
        this.Detail7.HeightF = 15F;
        this.Detail7.Name = "Detail7";
        // 
        // xrTable12
        // 
        this.xrTable12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(10.00023F, 0F);
        this.xrTable12.Name = "xrTable12";
        this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
        this.xrTable12.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable12.StylePriority.UseBorders = false;
        this.xrTable12.StylePriority.UseFont = false;
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell185,
            this.xrTableCell56});
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 11.5D;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ItemReceita")});
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.Text = Resources.rel_StatusReport.ItemDeReceita;
        this.xrTableCell52.Weight = 3.4546144473240443D;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaPrevista", "{0:n2}")});
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.StylePriority.UseTextAlignment = false;
        this.xrTableCell53.Text = Resources.rel_StatusReport.ReceitaPrevista;
        this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell53.Weight = 1.3986293127559213D;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaReal", "{0:n2}")});
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.StylePriority.UseTextAlignment = false;
        this.xrTableCell54.Text = Resources.rel_StatusReport.ReceitaReal;
        this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell54.Weight = 1.3986293279997712D;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.VariacaoReceita", "{0:n2}")});
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.StylePriority.UseTextAlignment = false;
        this.xrTableCell55.Text = Resources.rel_StatusReport.Desvio1;
        this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell55.Weight = 1.398629327999771D;
        // 
        // xrTableCell185
        // 
        this.xrTableCell185.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaPrevistaAteData", "{0:n2}")});
        this.xrTableCell185.Name = "xrTableCell185";
        this.xrTableCell185.StylePriority.UseTextAlignment = false;
        this.xrTableCell185.Text = "xrTableCell185";
        this.xrTableCell185.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell185.Weight = 1.3986293889751718D;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaRestante", "{0:n2}")});
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.StylePriority.UseTextAlignment = false;
        this.xrTableCell56.Text = Resources.rel_StatusReport.ReceitaRestante;
        this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell56.Weight = 1.3986293889751718D;
        // 
        // GroupHeader5
        // 
        this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrTable11});
        this.GroupHeader5.HeightF = 60F;
        this.GroupHeader5.Name = "GroupHeader5";
        this.GroupHeader5.RepeatEveryPage = true;
        // 
        // xrLabel14
        // 
        this.xrLabel14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 10.00001F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(747F, 20F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.Text = Resources.rel_StatusReport.DetalhesDosItensDeReceitasValoresEmR;
        // 
        // xrTable11
        // 
        this.xrTable11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(10F, 30F);
        this.xrTable11.Name = "xrTable11";
        this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
        this.xrTable11.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable11.StylePriority.UseBackColor = false;
        this.xrTable11.StylePriority.UseBorders = false;
        this.xrTable11.StylePriority.UseFont = false;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37,
            this.xrTableCell39,
            this.xrTableCell44,
            this.xrTableCell46,
            this.xrTableCell184,
            this.xrTableCell51});
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 11.5D;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Text = Resources.rel_StatusReport.ItemDeReceita1;
        this.xrTableCell37.Weight = 3.4546144473240443D;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.StylePriority.UseTextAlignment = false;
        this.xrTableCell39.Text = Resources.rel_StatusReport.ReceitaPrevistaTOTAL;
        this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell39.Weight = 1.3986293127559213D;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.StylePriority.UseTextAlignment = false;
        this.xrTableCell44.Text = Resources.rel_StatusReport.ReceitaRealizada;
        this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell44.Weight = 1.3986293279997712D;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.StylePriority.UseTextAlignment = false;
        this.xrTableCell46.Text = Resources.rel_StatusReport.Desvio2;
        this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell46.Weight = 1.398629327999771D;
        // 
        // xrTableCell184
        // 
        this.xrTableCell184.Name = "xrTableCell184";
        this.xrTableCell184.StylePriority.UseTextAlignment = false;
        this.xrTableCell184.Text = Resources.rel_StatusReport.ReceitaPrevAtéAData;
        this.xrTableCell184.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell184.Weight = 1.3986293889751718D;
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.StylePriority.UseTextAlignment = false;
        this.xrTableCell51.Text = Resources.rel_StatusReport.ReceitaRestante1;
        this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell51.Weight = 1.3986293889751718D;
        // 
        // GroupFooter3
        // 
        this.GroupFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable16});
        this.GroupFooter3.HeightF = 15F;
        this.GroupFooter3.Name = "GroupFooter3";
        // 
        // xrTable16
        // 
        this.xrTable16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(10.00036F, 0F);
        this.xrTable16.Name = "xrTable16";
        this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18});
        this.xrTable16.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable16.StylePriority.UseBorders = false;
        this.xrTable16.StylePriority.UseFont = false;
        // 
        // xrTableRow18
        // 
        this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76,
            this.xrTableCell77,
            this.xrTableCell186,
            this.xrTableCell78});
        this.xrTableRow18.Name = "xrTableRow18";
        this.xrTableRow18.Weight = 11.5D;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.Text = Resources.rel_StatusReport.TOTAL1;
        this.xrTableCell74.Weight = 3.4546144473240443D;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaPrevista")});
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseTextAlignment = false;
        xrSummary6.FormatString = "{0:n2}";
        xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell75.Summary = xrSummary6;
        this.xrTableCell75.Text = "xrTableCell75";
        this.xrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell75.Weight = 1.3986293127559213D;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaReal")});
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.StylePriority.UseTextAlignment = false;
        xrSummary7.FormatString = "{0:n2}";
        xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell76.Summary = xrSummary7;
        this.xrTableCell76.Text = "xrTableCell76";
        this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell76.Weight = 1.3986293279997712D;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.VariacaoReceita")});
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.StylePriority.UseTextAlignment = false;
        xrSummary8.FormatString = "{0:n2}";
        xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell77.Summary = xrSummary8;
        this.xrTableCell77.Text = "xrTableCell77";
        this.xrTableCell77.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell77.Weight = 1.398629327999771D;
        // 
        // xrTableCell186
        // 
        this.xrTableCell186.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaPrevistaAteData")});
        this.xrTableCell186.Name = "xrTableCell186";
        this.xrTableCell186.StylePriority.UseTextAlignment = false;
        xrSummary9.FormatString = "{0:n2}";
        xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell186.Summary = xrSummary9;
        this.xrTableCell186.Text = "xrTableCell186";
        this.xrTableCell186.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell186.Weight = 1.3986293889751718D;
        // 
        // xrTableCell78
        // 
        this.xrTableCell78.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensReceita.ReceitaRestante")});
        this.xrTableCell78.Name = "xrTableCell78";
        this.xrTableCell78.StylePriority.UseTextAlignment = false;
        xrSummary10.FormatString = "{0:n2}";
        xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell78.Summary = xrSummary10;
        this.xrTableCell78.Text = "xrTableCell78";
        this.xrTableCell78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell78.Weight = 1.3986293889751718D;
        // 
        // GroupFooterAnaliseValorAgregado
        // 
        this.GroupFooterAnaliseValorAgregado.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable18,
            this.xrLabel16});
        this.GroupFooterAnaliseValorAgregado.Expanded = false;
        this.GroupFooterAnaliseValorAgregado.HeightF = 93F;
        this.GroupFooterAnaliseValorAgregado.Name = "GroupFooterAnaliseValorAgregado";
        // 
        // xrTable18
        // 
        this.xrTable18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(10.00036F, 33F);
        this.xrTable18.Name = "xrTable18";
        this.xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow21,
            this.xrTableRow22,
            this.xrTableRow23,
            this.xrTableRow24});
        this.xrTable18.SizeF = new System.Drawing.SizeF(747F, 60F);
        this.xrTable18.StylePriority.UseBorders = false;
        this.xrTable18.StylePriority.UseTextAlignment = false;
        this.xrTable18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrTableRow21
        // 
        this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81,
            this.xrTableCell82,
            this.xrTableCell83});
        this.xrTableRow21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow21.Name = "xrTableRow21";
        this.xrTableRow21.StylePriority.UseFont = false;
        this.xrTableRow21.Weight = 1D;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Text = Resources.rel_StatusReport.ValorAgregadoRVA;
        this.xrTableCell81.Weight = 1.0240963855421688D;
        // 
        // xrTableCell82
        // 
        this.xrTableCell82.Name = "xrTableCell82";
        this.xrTableCell82.Text = Resources.rel_StatusReport.ValorPlanejadoRVP;
        this.xrTableCell82.Weight = 1.0240963855421688D;
        // 
        // xrTableCell83
        // 
        this.xrTableCell83.Name = "xrTableCell83";
        this.xrTableCell83.Text = Resources.rel_StatusReport.CustoRealizadoRCR;
        this.xrTableCell83.Weight = 0.95180722891566261D;
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell84,
            this.xrTableCell85,
            this.xrTableCell86});
        this.xrTableRow22.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.StylePriority.UseFont = false;
        this.xrTableRow22.Weight = 1D;
        // 
        // xrTableCell84
        // 
        this.xrTableCell84.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ValorAgregadoProjeto", "{0:n2}")});
        this.xrTableCell84.Name = "xrTableCell84";
        this.xrTableCell84.Text = "xrTableCell84";
        this.xrTableCell84.Weight = 1.0240963855421688D;
        // 
        // xrTableCell85
        // 
        this.xrTableCell85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ValorPlanejadoProjeto", "{0:n2}")});
        this.xrTableCell85.Name = "xrTableCell85";
        this.xrTableCell85.Text = "xrTableCell85";
        this.xrTableCell85.Weight = 1.0240963855421688D;
        // 
        // xrTableCell86
        // 
        this.xrTableCell86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.CustoRealizado", "{0:n2}")});
        this.xrTableCell86.Name = "xrTableCell86";
        this.xrTableCell86.Text = "xrTableCell86";
        this.xrTableCell86.Weight = 0.95180722891566261D;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell87,
            this.xrTableCell88,
            this.xrTableCell89});
        this.xrTableRow23.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.StylePriority.UseFont = false;
        this.xrTableRow23.Weight = 1D;
        // 
        // xrTableCell87
        // 
        this.xrTableCell87.Name = "xrTableCell87";
        this.xrTableCell87.Text = Resources.rel_StatusReport.ÍndiceDeDesempenhoDoPrazoIDP;
        this.xrTableCell87.Weight = 1.0240963855421688D;
        // 
        // xrTableCell88
        // 
        this.xrTableCell88.Name = "xrTableCell88";
        this.xrTableCell88.Text = Resources.rel_StatusReport.ÍndiceDeDesempenhoDoCustoIDC;
        this.xrTableCell88.Weight = 1.0240963855421688D;
        // 
        // xrTableCell89
        // 
        this.xrTableCell89.Name = "xrTableCell89";
        this.xrTableCell89.Text = Resources.rel_StatusReport.EstimativaAoConcluirEAC;
        this.xrTableCell89.Weight = 0.95180722891566261D;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell90,
            this.xrTableCell91,
            this.xrTableCell92});
        this.xrTableRow24.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.StylePriority.UseFont = false;
        this.xrTableRow24.Weight = 1D;
        // 
        // xrTableCell90
        // 
        this.xrTableCell90.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.IDP", "{0:0.00%}")});
        this.xrTableCell90.Name = "xrTableCell90";
        this.xrTableCell90.Text = "xrTableCell90";
        this.xrTableCell90.Weight = 1.0240963855421688D;
        // 
        // xrTableCell91
        // 
        this.xrTableCell91.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ICC", "{0:0.00%}")});
        this.xrTableCell91.Name = "xrTableCell91";
        this.xrTableCell91.Text = "xrTableCell91";
        this.xrTableCell91.Weight = 1.0240963855421688D;
        // 
        // xrTableCell92
        // 
        this.xrTableCell92.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.EstimativaConcluir", "{0:n2}")});
        this.xrTableCell92.Name = "xrTableCell92";
        this.xrTableCell92.Text = "xrTableCell92";
        this.xrTableCell92.Weight = 0.95180722891566261D;
        // 
        // xrLabel16
        // 
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(9.999879F, 10.00001F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(747F, 22.99999F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.Text = Resources.rel_StatusReport.AnáliseDoValorAgregado;
        // 
        // GroupFooterComentarioFinanceiro
        // 
        this.GroupFooterComentarioFinanceiro.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable17});
        this.GroupFooterComentarioFinanceiro.Expanded = false;
        this.GroupFooterComentarioFinanceiro.HeightF = 60F;
        this.GroupFooterComentarioFinanceiro.Name = "GroupFooterComentarioFinanceiro";
        // 
        // xrTable17
        // 
        this.xrTable17.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(9.999497F, 10F);
        this.xrTable17.Name = "xrTable17";
        this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow20});
        this.xrTable17.SizeF = new System.Drawing.SizeF(747F, 40F);
        this.xrTable17.StylePriority.UseBorders = false;
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.BackColor = System.Drawing.Color.Silver;
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79});
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.StylePriority.UseBackColor = false;
        this.xrTableRow19.Weight = 9.2D;
        // 
        // xrTableCell79
        // 
        this.xrTableCell79.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.StylePriority.UseFont = false;
        this.xrTableCell79.StylePriority.UseTextAlignment = false;
        this.xrTableCell79.Text = Resources.rel_StatusReport.ComentárioSobreODesempenhoFinanceiroDoProj;
        this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell79.Weight = 1.3037809647979139D;
        // 
        // xrTableRow20
        // 
        this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow20.Name = "xrTableRow20";
        this.xrTableRow20.Weight = 9.2D;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ComentarioFinanceiro")});
        this.xrTableCell80.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell80.Multiline = true;
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.StylePriority.UseFont = false;
        this.xrTableCell80.StylePriority.UseTextAlignment = false;
        this.xrTableCell80.Text = "xrTableCell80";
        this.xrTableCell80.Weight = 1.3037809647979139D;
        // 
        // DetailDesempenhoFinanceiro
        // 
        this.DetailDesempenhoFinanceiro.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail21});
        this.DetailDesempenhoFinanceiro.Expanded = false;
        this.DetailDesempenhoFinanceiro.Level = 0;
        this.DetailDesempenhoFinanceiro.Name = "DetailDesempenhoFinanceiro";
        // 
        // Detail21
        // 
        this.Detail21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35,
            this.xrLabel36,
            this.xrLabel37,
            this.xrPictureBox4,
            this.xrLabel33,
            this.xrLabel34});
        this.Detail21.HeightF = 40F;
        this.Detail21.Name = "Detail21";
        // 
        // xrLabel35
        // 
        this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ResultadoCusto")});
        this.xrLabel35.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(200F, 40F);
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "xrLabel32";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel36
        // 
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(525F, 0F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(75F, 40F);
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = Resources.rel_StatusReport.Realizado1;
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel37
        // 
        this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ValorCustoRealizado", "{0:n2}")});
        this.xrLabel37.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(600F, 0F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(120F, 40F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "xrLabel30";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPictureBox4
        // 
        this.xrPictureBox4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "StatusReport.CorDesempenhoCusto", "~/imagens/Financeiro{0}.png")});
        this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(10F, 5F);
        this.xrPictureBox4.Name = "xrPictureBox4";
        this.xrPictureBox4.SizeF = new System.Drawing.SizeF(30F, 30F);
        this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLabel33
        // 
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(260F, 0F);
        this.xrLabel33.Multiline = true;
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(140F, 40F);
        this.xrLabel33.StylePriority.UseTextAlignment = false;
        this.xrLabel33.Text = Resources.rel_StatusReport.PrevistoAtéAData1;
        this.xrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel34
        // 
        this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ValorCustoPrevisto", "{0:n2}")});
        this.xrLabel34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(400F, 0F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(125F, 40F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        this.xrLabel34.Text = "xrLabel28";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReportItemCusto2
        // 
        this.DetailReportItemCusto2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail22,
            this.GroupHeader12,
            this.GroupFooter6});
        this.DetailReportItemCusto2.DataMember = "StatusReport.StatusReport_ItensCusto";
        this.DetailReportItemCusto2.DataSource = this.ds;
        this.DetailReportItemCusto2.Expanded = false;
        this.DetailReportItemCusto2.Level = 3;
        this.DetailReportItemCusto2.Name = "DetailReportItemCusto2";
        // 
        // Detail22
        // 
        this.Detail22.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable35});
        this.Detail22.HeightF = 16.66667F;
        this.Detail22.Name = "Detail22";
        // 
        // xrTable35
        // 
        this.xrTable35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable35.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable35.LocationFloat = new DevExpress.Utils.PointFloat(9.999204F, 0F);
        this.xrTable35.Name = "xrTable35";
        this.xrTable35.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow46});
        this.xrTable35.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable35.StylePriority.UseBorders = false;
        this.xrTable35.StylePriority.UseFont = false;
        // 
        // xrTableRow46
        // 
        this.xrTableRow46.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell193,
            this.xrTableCell194,
            this.xrTableCell195,
            this.xrTableCell197});
        this.xrTableRow46.Name = "xrTableRow46";
        this.xrTableRow46.Weight = 11.5D;
        // 
        // xrTableCell193
        // 
        this.xrTableCell193.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.ItemCusto")});
        this.xrTableCell193.Name = "xrTableCell193";
        this.xrTableCell193.Text = "xrTableCell45";
        this.xrTableCell193.Weight = 3.4546144473240443D;
        // 
        // xrTableCell194
        // 
        this.xrTableCell194.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevisto", "{0:n2}")});
        this.xrTableCell194.Name = "xrTableCell194";
        this.xrTableCell194.StylePriority.UseTextAlignment = false;
        this.xrTableCell194.Text = "xrTableCell47";
        this.xrTableCell194.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell194.Weight = 2.0000510278735431D;
        // 
        // xrTableCell195
        // 
        this.xrTableCell195.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoReal", "{0:n2}")});
        this.xrTableCell195.Name = "xrTableCell195";
        this.xrTableCell195.StylePriority.UseTextAlignment = false;
        this.xrTableCell195.Text = "xrTableCell48";
        this.xrTableCell195.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell195.Weight = 2.19583695612577D;
        // 
        // xrTableCell197
        // 
        this.xrTableCell197.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevistoAteData", "{0:n2}")});
        this.xrTableCell197.Name = "xrTableCell197";
        this.xrTableCell197.StylePriority.UseTextAlignment = false;
        this.xrTableCell197.Text = "xrTableCell182";
        this.xrTableCell197.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell197.Weight = 2.7972587627064929D;
        // 
        // GroupHeader12
        // 
        this.GroupHeader12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel39,
            this.xrTable34});
        this.GroupHeader12.HeightF = 52.08333F;
        this.GroupHeader12.Name = "GroupHeader12";
        // 
        // xrLabel39
        // 
        this.xrLabel39.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(9.99922F, 0F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(747F, 20F);
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.Text = Resources.rel_StatusReport.DetalhesDosItensDeDespesaValoresEmR1;
        // 
        // xrTable34
        // 
        this.xrTable34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable34.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable34.LocationFloat = new DevExpress.Utils.PointFloat(9.999497F, 20.00001F);
        this.xrTable34.Name = "xrTable34";
        this.xrTable34.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable34.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable34.StylePriority.UseBackColor = false;
        this.xrTable34.StylePriority.UseBorders = false;
        this.xrTable34.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell187,
            this.xrTableCell188,
            this.xrTableCell189,
            this.xrTableCell191});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 11.5D;
        // 
        // xrTableCell187
        // 
        this.xrTableCell187.Name = "xrTableCell187";
        this.xrTableCell187.Text = Resources.rel_StatusReport.ItemDeDespesa1;
        this.xrTableCell187.Weight = 3.4546144473240443D;
        // 
        // xrTableCell188
        // 
        this.xrTableCell188.Name = "xrTableCell188";
        this.xrTableCell188.StylePriority.UseTextAlignment = false;
        this.xrTableCell188.Text = Resources.rel_StatusReport.DespesaPrevistaTOTAL1;
        this.xrTableCell188.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell188.Weight = 2.0000467595955183D;
        // 
        // xrTableCell189
        // 
        this.xrTableCell189.Name = "xrTableCell189";
        this.xrTableCell189.StylePriority.UseTextAlignment = false;
        this.xrTableCell189.Text = Resources.rel_StatusReport.DespesaRealizada1;
        this.xrTableCell189.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell189.Weight = 2.19583695612577D;
        // 
        // xrTableCell191
        // 
        this.xrTableCell191.Name = "xrTableCell191";
        this.xrTableCell191.StylePriority.UseTextAlignment = false;
        this.xrTableCell191.Text = Resources.rel_StatusReport.DespesaPrevAtéAData1;
        this.xrTableCell191.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell191.Weight = 2.7972630309845181D;
        // 
        // GroupFooter6
        // 
        this.GroupFooter6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable36});
        this.GroupFooter6.HeightF = 15.625F;
        this.GroupFooter6.Name = "GroupFooter6";
        // 
        // xrTable36
        // 
        this.xrTable36.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable36.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable36.LocationFloat = new DevExpress.Utils.PointFloat(9.999204F, 0F);
        this.xrTable36.Name = "xrTable36";
        this.xrTable36.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow47});
        this.xrTable36.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable36.StylePriority.UseBorders = false;
        this.xrTable36.StylePriority.UseFont = false;
        // 
        // xrTableRow47
        // 
        this.xrTableRow47.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell199,
            this.xrTableCell200,
            this.xrTableCell201,
            this.xrTableCell203});
        this.xrTableRow47.Name = "xrTableRow47";
        this.xrTableRow47.Weight = 11.5D;
        // 
        // xrTableCell199
        // 
        this.xrTableCell199.Name = "xrTableCell199";
        this.xrTableCell199.Text = Resources.rel_StatusReport.TOTAL2;
        this.xrTableCell199.Weight = 3.4546144473240443D;
        // 
        // xrTableCell200
        // 
        this.xrTableCell200.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevisto")});
        this.xrTableCell200.Name = "xrTableCell200";
        this.xrTableCell200.StylePriority.UseTextAlignment = false;
        xrSummary11.FormatString = "{0:n2}";
        xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell200.Summary = xrSummary11;
        this.xrTableCell200.Text = "xrTableCell70";
        this.xrTableCell200.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell200.Weight = 2.0000510278735431D;
        // 
        // xrTableCell201
        // 
        this.xrTableCell201.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoReal")});
        this.xrTableCell201.Name = "xrTableCell201";
        this.xrTableCell201.StylePriority.UseTextAlignment = false;
        xrSummary12.FormatString = "{0:n2}";
        xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell201.Summary = xrSummary12;
        this.xrTableCell201.Text = "xrTableCell71";
        this.xrTableCell201.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell201.Weight = 2.19583695612577D;
        // 
        // xrTableCell203
        // 
        this.xrTableCell203.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_ItensCusto.CustoPrevistoAteData")});
        this.xrTableCell203.Name = "xrTableCell203";
        this.xrTableCell203.StylePriority.UseTextAlignment = false;
        xrSummary13.FormatString = "{0:n2}";
        xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell203.Summary = xrSummary13;
        this.xrTableCell203.Text = "xrTableCell183";
        this.xrTableCell203.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell203.Weight = 2.7972587627064929D;
        // 
        // DetailReportRiscos
        // 
        this.DetailReportRiscos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail9,
            this.GroupFooterComentarioRisco,
            this.DetailReportListaRisco});
        this.DetailReportRiscos.DataMember = "StatusReport";
        this.DetailReportRiscos.DataSource = this.ds;
        this.DetailReportRiscos.Expanded = false;
        this.DetailReportRiscos.Level = 4;
        this.DetailReportRiscos.Name = "DetailReportRiscos";
        // 
        // Detail9
        // 
        this.Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21});
        this.Detail9.Expanded = false;
        this.Detail9.HeightF = 32.99999F;
        this.Detail9.Name = "Detail9";
        // 
        // xrLabel21
        // 
        this.xrLabel21.BackColor = System.Drawing.Color.Silver;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(10.00107F, 10.00001F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(746.9998F, 22.99998F);
        this.xrLabel21.StylePriority.UseBackColor = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = Resources.rel_StatusReport.Riscos;
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupFooterComentarioRisco
        // 
        this.GroupFooterComentarioRisco.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable30});
        this.GroupFooterComentarioRisco.HeightF = 60F;
        this.GroupFooterComentarioRisco.Name = "GroupFooterComentarioRisco";
        // 
        // xrTable30
        // 
        this.xrTable30.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable30.LocationFloat = new DevExpress.Utils.PointFloat(10.00093F, 10.00001F);
        this.xrTable30.Name = "xrTable30";
        this.xrTable30.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow38,
            this.xrTableRow39});
        this.xrTable30.SizeF = new System.Drawing.SizeF(747F, 40F);
        this.xrTable30.StylePriority.UseBorders = false;
        // 
        // xrTableRow38
        // 
        this.xrTableRow38.BackColor = System.Drawing.Color.Silver;
        this.xrTableRow38.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell147});
        this.xrTableRow38.Name = "xrTableRow38";
        this.xrTableRow38.StylePriority.UseBackColor = false;
        this.xrTableRow38.Weight = 9.2D;
        // 
        // xrTableCell147
        // 
        this.xrTableCell147.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell147.Name = "xrTableCell147";
        this.xrTableCell147.StylePriority.UseFont = false;
        this.xrTableCell147.StylePriority.UseTextAlignment = false;
        this.xrTableCell147.Text = Resources.rel_StatusReport.ComentárioSobreOsRiscosDoProjeto;
        this.xrTableCell147.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell147.Weight = 1.3037809647979139D;
        // 
        // xrTableRow39
        // 
        this.xrTableRow39.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell148});
        this.xrTableRow39.Name = "xrTableRow39";
        this.xrTableRow39.Weight = 9.2D;
        // 
        // xrTableCell148
        // 
        this.xrTableCell148.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ComentarioRisco")});
        this.xrTableCell148.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell148.Multiline = true;
        this.xrTableCell148.Name = "xrTableCell148";
        this.xrTableCell148.StylePriority.UseFont = false;
        this.xrTableCell148.StylePriority.UseTextAlignment = false;
        this.xrTableCell148.Text = "xrTableCell148";
        this.xrTableCell148.Weight = 1.3037809647979139D;
        // 
        // DetailReportListaRisco
        // 
        this.DetailReportListaRisco.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail18,
            this.GroupHeader11});
        this.DetailReportListaRisco.DataMember = "StatusReport.StatusReport_Riscos";
        this.DetailReportListaRisco.DataSource = this.ds;
        this.DetailReportListaRisco.Level = 0;
        this.DetailReportListaRisco.Name = "DetailReportListaRisco";
        // 
        // Detail18
        // 
        this.Detail18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable32});
        this.Detail18.HeightF = 17F;
        this.Detail18.Name = "Detail18";
        // 
        // xrTable32
        // 
        this.xrTable32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable32.LocationFloat = new DevExpress.Utils.PointFloat(9.999625F, 0F);
        this.xrTable32.Name = "xrTable32";
        this.xrTable32.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow41});
        this.xrTable32.SizeF = new System.Drawing.SizeF(747F, 17F);
        this.xrTable32.StylePriority.UseBorders = false;
        this.xrTable32.StylePriority.UseFont = false;
        // 
        // xrTableRow41
        // 
        this.xrTableRow41.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell155,
            this.xrTableCell156,
            this.xrTableCell157,
            this.xrTableCell158,
            this.xrTableCell159,
            this.xrTableCell160});
        this.xrTableRow41.Name = "xrTableRow41";
        this.xrTableRow41.Weight = 11.5D;
        // 
        // xrTableCell155
        // 
        this.xrTableCell155.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Riscos.Status")});
        this.xrTableCell155.Name = "xrTableCell155";
        this.xrTableCell155.StylePriority.UseTextAlignment = false;
        this.xrTableCell155.Text = "xrTableCell155";
        this.xrTableCell155.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell155.Weight = 0.839177628760952D;
        // 
        // xrTableCell156
        // 
        this.xrTableCell156.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Riscos.Risco")});
        this.xrTableCell156.Name = "xrTableCell156";
        this.xrTableCell156.Text = "xrTableCell133";
        this.xrTableCell156.Weight = 3.6384343115375555D;
        // 
        // xrTableCell157
        // 
        this.xrTableCell157.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Riscos.Probabilidade")});
        this.xrTableCell157.Name = "xrTableCell157";
        this.xrTableCell157.StylePriority.UseTextAlignment = false;
        this.xrTableCell157.Text = "xrTableCell134";
        this.xrTableCell157.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell157.Weight = 1.3986295260678183D;
        // 
        // xrTableCell158
        // 
        this.xrTableCell158.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Riscos.Impacto")});
        this.xrTableCell158.Name = "xrTableCell158";
        this.xrTableCell158.StylePriority.UseTextAlignment = false;
        this.xrTableCell158.Text = "xrTableCell135";
        this.xrTableCell158.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell158.Weight = 1.3986295260678185D;
        // 
        // xrTableCell159
        // 
        this.xrTableCell159.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Riscos.LimiteEliminacao", "{0:dd/MM/yyyy}")});
        this.xrTableCell159.Name = "xrTableCell159";
        this.xrTableCell159.StylePriority.UseTextAlignment = false;
        this.xrTableCell159.Text = "xrTableCell136";
        this.xrTableCell159.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell159.Weight = 1.1189036117079434D;
        // 
        // xrTableCell160
        // 
        this.xrTableCell160.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Riscos.Responsavel")});
        this.xrTableCell160.Name = "xrTableCell160";
        this.xrTableCell160.Text = "xrTableCell137";
        this.xrTableCell160.Weight = 2.0539865898877636D;
        // 
        // GroupHeader11
        // 
        this.GroupHeader11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable31});
        this.GroupHeader11.HeightF = 40.00001F;
        this.GroupHeader11.Name = "GroupHeader11";
        this.GroupHeader11.RepeatEveryPage = true;
        // 
        // xrTable31
        // 
        this.xrTable31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable31.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable31.LocationFloat = new DevExpress.Utils.PointFloat(10.00093F, 10.00001F);
        this.xrTable31.Name = "xrTable31";
        this.xrTable31.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow40});
        this.xrTable31.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable31.StylePriority.UseBackColor = false;
        this.xrTable31.StylePriority.UseBorders = false;
        this.xrTable31.StylePriority.UseFont = false;
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
        this.xrTableRow40.Name = "xrTableRow40";
        this.xrTableRow40.Weight = 11.5D;
        // 
        // xrTableCell149
        // 
        this.xrTableCell149.Name = "xrTableCell149";
        this.xrTableCell149.Text = Resources.rel_StatusReport.Status1;
        this.xrTableCell149.Weight = 0.83917762881194791D;
        // 
        // xrTableCell150
        // 
        this.xrTableCell150.Name = "xrTableCell150";
        this.xrTableCell150.Text = Resources.rel_StatusReport.Risco;
        this.xrTableCell150.Weight = 3.6384343114865603D;
        // 
        // xrTableCell151
        // 
        this.xrTableCell151.Name = "xrTableCell151";
        this.xrTableCell151.StylePriority.UseTextAlignment = false;
        this.xrTableCell151.Text = Resources.rel_StatusReport.Probabilidade;
        this.xrTableCell151.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell151.Weight = 1.3986293279997712D;
        // 
        // xrTableCell152
        // 
        this.xrTableCell152.Name = "xrTableCell152";
        this.xrTableCell152.StylePriority.UseTextAlignment = false;
        this.xrTableCell152.Text = Resources.rel_StatusReport.Impacto;
        this.xrTableCell152.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell152.Weight = 1.398629327999771D;
        // 
        // xrTableCell153
        // 
        this.xrTableCell153.Name = "xrTableCell153";
        this.xrTableCell153.StylePriority.UseTextAlignment = false;
        this.xrTableCell153.Text = Resources.rel_StatusReport.LimiteEliminação;
        this.xrTableCell153.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell153.Weight = 1.118903459351047D;
        // 
        // xrTableCell154
        // 
        this.xrTableCell154.Name = "xrTableCell154";
        this.xrTableCell154.StylePriority.UseTextAlignment = false;
        this.xrTableCell154.Text = Resources.rel_StatusReport.Responsavel;
        this.xrTableCell154.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell154.Weight = 2.0539871383807542D;
        // 
        // DetailReportQuestoes
        // 
        this.DetailReportQuestoes.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail10,
            this.DetailReportListaQuestao,
            this.GroupFooterComentarioQuestao});
        this.DetailReportQuestoes.DataMember = "StatusReport";
        this.DetailReportQuestoes.DataSource = this.ds;
        this.DetailReportQuestoes.Expanded = false;
        this.DetailReportQuestoes.Level = 5;
        this.DetailReportQuestoes.Name = "DetailReportQuestoes";
        // 
        // Detail10
        // 
        this.Detail10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
        this.Detail10.HeightF = 32.99999F;
        this.Detail10.Name = "Detail10";
        // 
        // xrLabel20
        // 
        this.xrLabel20.BackColor = System.Drawing.Color.Silver;
        this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.labelQuestoes")});
        this.xrLabel20.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(9.999688F, 10.00001F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(746.9998F, 22.99998F);
        this.xrLabel20.StylePriority.UseBackColor = false;
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = Resources.rel_StatusReport.Questões;
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReportListaQuestao
        // 
        this.DetailReportListaQuestao.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail17,
            this.GroupHeader10});
        this.DetailReportListaQuestao.DataMember = "StatusReport.StatusReport_Questoes";
        this.DetailReportListaQuestao.DataSource = this.ds;
        this.DetailReportListaQuestao.Level = 0;
        this.DetailReportListaQuestao.Name = "DetailReportListaQuestao";
        // 
        // Detail17
        // 
        this.Detail17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable27});
        this.Detail17.HeightF = 17F;
        this.Detail17.Name = "Detail17";
        // 
        // xrTable27
        // 
        this.xrTable27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable27.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable27.LocationFloat = new DevExpress.Utils.PointFloat(9.999688F, 0F);
        this.xrTable27.Name = "xrTable27";
        this.xrTable27.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow33});
        this.xrTable27.SizeF = new System.Drawing.SizeF(747F, 17F);
        this.xrTable27.StylePriority.UseBorders = false;
        this.xrTable27.StylePriority.UseFont = false;
        // 
        // xrTableRow33
        // 
        this.xrTableRow33.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell139,
            this.xrTableCell140,
            this.xrTableCell141,
            this.xrTableCell142,
            this.xrTableCell143,
            this.xrTableCell144});
        this.xrTableRow33.Name = "xrTableRow33";
        this.xrTableRow33.Weight = 11.5D;
        // 
        // xrTableCell139
        // 
        this.xrTableCell139.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Questoes.Status")});
        this.xrTableCell139.Name = "xrTableCell139";
        this.xrTableCell139.StylePriority.UseTextAlignment = false;
        this.xrTableCell139.Text = "xrTableCell139";
        this.xrTableCell139.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell139.Weight = 0.839177628760952D;
        // 
        // xrTableCell140
        // 
        this.xrTableCell140.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Questoes.Questao")});
        this.xrTableCell140.Name = "xrTableCell140";
        this.xrTableCell140.Text = "xrTableCell133";
        this.xrTableCell140.Weight = 3.6384343115375555D;
        // 
        // xrTableCell141
        // 
        this.xrTableCell141.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Questoes.Urgencia")});
        this.xrTableCell141.Name = "xrTableCell141";
        this.xrTableCell141.StylePriority.UseTextAlignment = false;
        this.xrTableCell141.Text = "xrTableCell134";
        this.xrTableCell141.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell141.Weight = 1.3986295260678183D;
        // 
        // xrTableCell142
        // 
        this.xrTableCell142.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Questoes.Prioridade")});
        this.xrTableCell142.Name = "xrTableCell142";
        this.xrTableCell142.StylePriority.UseTextAlignment = false;
        this.xrTableCell142.Text = "xrTableCell135";
        this.xrTableCell142.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell142.Weight = 1.3986295260678185D;
        // 
        // xrTableCell143
        // 
        this.xrTableCell143.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Questoes.LimiteEliminacao", "{0:dd/MM/yyyy}")});
        this.xrTableCell143.Name = "xrTableCell143";
        this.xrTableCell143.StylePriority.UseTextAlignment = false;
        this.xrTableCell143.Text = "xrTableCell136";
        this.xrTableCell143.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell143.Weight = 1.1189036117079434D;
        // 
        // xrTableCell144
        // 
        this.xrTableCell144.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Questoes.Responsavel")});
        this.xrTableCell144.Name = "xrTableCell144";
        this.xrTableCell144.Text = "xrTableCell137";
        this.xrTableCell144.Weight = 2.0539865898877636D;
        // 
        // GroupHeader10
        // 
        this.GroupHeader10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable26});
        this.GroupHeader10.HeightF = 40.00001F;
        this.GroupHeader10.Name = "GroupHeader10";
        this.GroupHeader10.RepeatEveryPage = true;
        // 
        // xrTable26
        // 
        this.xrTable26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable26.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable26.LocationFloat = new DevExpress.Utils.PointFloat(9.999657F, 10.00001F);
        this.xrTable26.Name = "xrTable26";
        this.xrTable26.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow32});
        this.xrTable26.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable26.StylePriority.UseBackColor = false;
        this.xrTable26.StylePriority.UseBorders = false;
        this.xrTable26.StylePriority.UseFont = false;
        // 
        // xrTableRow32
        // 
        this.xrTableRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell126,
            this.xtcHeaderQuestoes,
            this.xrTableCell134,
            this.xrTableCell135,
            this.xrTableCell136,
            this.xrTableCell137});
        this.xrTableRow32.Name = "xrTableRow32";
        this.xrTableRow32.Weight = 11.5D;
        // 
        // xrTableCell126
        // 
        this.xrTableCell126.Name = "xrTableCell126";
        this.xrTableCell126.Text = Resources.rel_StatusReport.Status2;
        this.xrTableCell126.Weight = 0.83917762881194791D;
        // 
        // xtcHeaderQuestoes
        // 
        this.xtcHeaderQuestoes.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.labelQuestoes")});
        this.xtcHeaderQuestoes.Name = "xtcHeaderQuestoes";
        this.xtcHeaderQuestoes.Text = Resources.rel_StatusReport.Questão;
        this.xtcHeaderQuestoes.Weight = 3.6384343114865603D;
        // 
        // xrTableCell134
        // 
        this.xrTableCell134.Name = "xrTableCell134";
        this.xrTableCell134.StylePriority.UseTextAlignment = false;
        this.xrTableCell134.Text = Resources.rel_StatusReport.Urgência;
        this.xrTableCell134.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell134.Weight = 1.3986293279997712D;
        // 
        // xrTableCell135
        // 
        this.xrTableCell135.Name = "xrTableCell135";
        this.xrTableCell135.StylePriority.UseTextAlignment = false;
        this.xrTableCell135.Text = Resources.rel_StatusReport.Prioridade;
        this.xrTableCell135.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell135.Weight = 1.398629327999771D;
        // 
        // xrTableCell136
        // 
        this.xrTableCell136.Name = "xrTableCell136";
        this.xrTableCell136.StylePriority.UseTextAlignment = false;
        this.xrTableCell136.Text = Resources.rel_StatusReport.LimiteResolução;
        this.xrTableCell136.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell136.Weight = 1.118903459351047D;
        // 
        // xrTableCell137
        // 
        this.xrTableCell137.Name = "xrTableCell137";
        this.xrTableCell137.StylePriority.UseTextAlignment = false;
        this.xrTableCell137.Text = Resources.rel_StatusReport.Responsavel1;
        this.xrTableCell137.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell137.Weight = 2.0539871383807542D;
        // 
        // GroupFooterComentarioQuestao
        // 
        this.GroupFooterComentarioQuestao.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable28});
        this.GroupFooterComentarioQuestao.HeightF = 60F;
        this.GroupFooterComentarioQuestao.Name = "GroupFooterComentarioQuestao";
        // 
        // xrTable28
        // 
        this.xrTable28.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable28.LocationFloat = new DevExpress.Utils.PointFloat(10.00093F, 9.999974F);
        this.xrTable28.Name = "xrTable28";
        this.xrTable28.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow34,
            this.xrTableRow35});
        this.xrTable28.SizeF = new System.Drawing.SizeF(747F, 40F);
        this.xrTable28.StylePriority.UseBorders = false;
        // 
        // xrTableRow34
        // 
        this.xrTableRow34.BackColor = System.Drawing.Color.Silver;
        this.xrTableRow34.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xtcComentarioQuestoes});
        this.xrTableRow34.Name = "xrTableRow34";
        this.xrTableRow34.StylePriority.UseBackColor = false;
        this.xrTableRow34.Weight = 9.2D;
        // 
        // xtcComentarioQuestoes
        // 
        this.xtcComentarioQuestoes.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.labelQuestoes", Resources.rel_StatusReport.ComentárioSobreX0DoProjeto)});
        this.xtcComentarioQuestoes.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xtcComentarioQuestoes.Name = "xtcComentarioQuestoes";
        this.xtcComentarioQuestoes.StylePriority.UseFont = false;
        this.xtcComentarioQuestoes.StylePriority.UseTextAlignment = false;
        this.xtcComentarioQuestoes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xtcComentarioQuestoes.Weight = 1.3037809647979139D;
        // 
        // xrTableRow35
        // 
        this.xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell138});
        this.xrTableRow35.Name = "xrTableRow35";
        this.xrTableRow35.Weight = 9.2D;
        // 
        // xrTableCell138
        // 
        this.xrTableCell138.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ComentarioQuestao")});
        this.xrTableCell138.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell138.Multiline = true;
        this.xrTableCell138.Name = "xrTableCell138";
        this.xrTableCell138.StylePriority.UseFont = false;
        this.xrTableCell138.StylePriority.UseTextAlignment = false;
        this.xrTableCell138.Text = "xrTableCell138";
        this.xrTableCell138.Weight = 1.3037809647979139D;
        // 
        // DetailReportMetas
        // 
        this.DetailReportMetas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail11,
            this.DetailReportListaMeta,
            this.GroupFooterComentarioMeta});
        this.DetailReportMetas.DataMember = "StatusReport";
        this.DetailReportMetas.DataSource = this.ds;
        this.DetailReportMetas.Expanded = false;
        this.DetailReportMetas.Level = 6;
        this.DetailReportMetas.Name = "DetailReportMetas";
        // 
        // Detail11
        // 
        this.Detail11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17});
        this.Detail11.Expanded = false;
        this.Detail11.HeightF = 32.99999F;
        this.Detail11.Name = "Detail11";
        // 
        // xrLabel17
        // 
        this.xrLabel17.BackColor = System.Drawing.Color.Silver;
        this.xrLabel17.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(9.999721F, 10.00001F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(746.9998F, 22.99998F);
        this.xrLabel17.StylePriority.UseBackColor = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = Resources.rel_StatusReport.MetasEResultados;
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReportListaMeta
        // 
        this.DetailReportListaMeta.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail16,
            this.GroupHeader9,
            this.GroupFooter1});
        this.DetailReportListaMeta.DataMember = "StatusReport.StatusReport_Metas";
        this.DetailReportListaMeta.DataSource = this.ds;
        this.DetailReportListaMeta.Expanded = false;
        this.DetailReportListaMeta.Level = 0;
        this.DetailReportListaMeta.Name = "DetailReportListaMeta";
        // 
        // Detail16
        // 
        this.Detail16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable25});
        this.Detail16.HeightF = 17F;
        this.Detail16.Name = "Detail16";
        // 
        // xrTable25
        // 
        this.xrTable25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable25.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable25.LocationFloat = new DevExpress.Utils.PointFloat(10.00093F, 0F);
        this.xrTable25.Name = "xrTable25";
        this.xrTable25.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow31});
        this.xrTable25.SizeF = new System.Drawing.SizeF(747F, 17F);
        this.xrTable25.StylePriority.UseBackColor = false;
        this.xrTable25.StylePriority.UseBorders = false;
        this.xrTable25.StylePriority.UseFont = false;
        // 
        // xrTableRow31
        // 
        this.xrTableRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell127,
            this.xrTableCell128,
            this.xrTableCell129,
            this.xrTableCell130,
            this.xrTableCell131,
            this.xrTableCell132});
        this.xrTableRow31.Name = "xrTableRow31";
        this.xrTableRow31.Weight = 11.5D;
        // 
        // xrTableCell127
        // 
        this.xrTableCell127.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox3});
        this.xrTableCell127.Name = "xrTableCell127";
        this.xrTableCell127.StylePriority.UseTextAlignment = false;
        this.xrTableCell127.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell127.Weight = 0.62938322732540475D;
        // 
        // xrPictureBox3
        // 
        this.xrPictureBox3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "StatusReport.StatusReport_Metas.Status", "~/imagens/{0}.gif")});
        this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(15F, 1F);
        this.xrPictureBox3.Name = "xrPictureBox3";
        this.xrPictureBox3.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox3.StylePriority.UseBorders = false;
        // 
        // xrTableCell128
        // 
        this.xrTableCell128.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Metas.Meta")});
        this.xrTableCell128.Name = "xrTableCell128";
        this.xrTableCell128.Text = "xrTableCell121";
        this.xrTableCell128.Weight = 4.2238671228036253D;
        // 
        // xrTableCell129
        // 
        this.xrTableCell129.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Metas.NomeIndicador")});
        this.xrTableCell129.Name = "xrTableCell129";
        this.xrTableCell129.Text = "xrTableCell122";
        this.xrTableCell129.Weight = 1.3986319259076763D;
        // 
        // xrTableCell130
        // 
        this.xrTableCell130.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Metas.Mes")});
        this.xrTableCell130.Name = "xrTableCell130";
        this.xrTableCell130.Text = "xrTableCell123";
        this.xrTableCell130.Weight = 1.398631925907676D;
        // 
        // xrTableCell131
        // 
        this.xrTableCell131.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Metas.MetaAcumuladaAno")});
        this.xrTableCell131.Name = "xrTableCell131";
        this.xrTableCell131.StylePriority.UseTextAlignment = false;
        this.xrTableCell131.Text = "xrTableCell131";
        this.xrTableCell131.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell131.Weight = 1.3986319259076763D;
        // 
        // xrTableCell132
        // 
        this.xrTableCell132.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Metas.ResultadoAcumuladoAno")});
        this.xrTableCell132.Name = "xrTableCell132";
        this.xrTableCell132.StylePriority.UseTextAlignment = false;
        this.xrTableCell132.Text = "xrTableCell132";
        this.xrTableCell132.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell132.Weight = 1.3986150661777936D;
        // 
        // GroupHeader9
        // 
        this.GroupHeader9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable24});
        this.GroupHeader9.HeightF = 25F;
        this.GroupHeader9.Name = "GroupHeader9";
        this.GroupHeader9.RepeatEveryPage = true;
        // 
        // xrTable24
        // 
        this.xrTable24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable24.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable24.LocationFloat = new DevExpress.Utils.PointFloat(10.00093F, 9.999974F);
        this.xrTable24.Name = "xrTable24";
        this.xrTable24.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow30});
        this.xrTable24.SizeF = new System.Drawing.SizeF(746.9986F, 15.00003F);
        this.xrTable24.StylePriority.UseBackColor = false;
        this.xrTable24.StylePriority.UseBorders = false;
        this.xrTable24.StylePriority.UseFont = false;
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
        this.xrTableRow30.Name = "xrTableRow30";
        this.xrTableRow30.Weight = 11.5D;
        // 
        // xrTableCell120
        // 
        this.xrTableCell120.Name = "xrTableCell120";
        this.xrTableCell120.Text = Resources.rel_StatusReport.Status3;
        this.xrTableCell120.Weight = 0.62938439603307827D;
        // 
        // xrTableCell121
        // 
        this.xrTableCell121.Name = "xrTableCell121";
        this.xrTableCell121.Text = Resources.rel_StatusReport.DescriçãoDaMeta;
        this.xrTableCell121.Weight = 4.2238659540959507D;
        // 
        // xrTableCell122
        // 
        this.xrTableCell122.Name = "xrTableCell122";
        this.xrTableCell122.StylePriority.UseTextAlignment = false;
        this.xrTableCell122.Text = Resources.rel_StatusReport.Indicador;
        this.xrTableCell122.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell122.Weight = 1.3986319259076763D;
        // 
        // xrTableCell123
        // 
        this.xrTableCell123.Name = "xrTableCell123";
        this.xrTableCell123.StylePriority.UseTextAlignment = false;
        this.xrTableCell123.Text = Resources.rel_StatusReport.Período1;
        this.xrTableCell123.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell123.Weight = 1.398631925907676D;
        // 
        // xrTableCell124
        // 
        this.xrTableCell124.Name = "xrTableCell124";
        this.xrTableCell124.StylePriority.UseTextAlignment = false;
        this.xrTableCell124.Text = Resources.rel_StatusReport.MetaNº;
        this.xrTableCell124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell124.Weight = 1.3986319259076763D;
        // 
        // xrTableCell125
        // 
        this.xrTableCell125.Name = "xrTableCell125";
        this.xrTableCell125.StylePriority.UseTextAlignment = false;
        this.xrTableCell125.Text = Resources.rel_StatusReport.ResultadoNº;
        this.xrTableCell125.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell125.Weight = 1.3986150661777936D;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.GroupFooter1.HeightF = 25F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable1
        // 
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 5F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow44});
        this.xrTable1.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow44
        // 
        this.xrTableRow44.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell162,
            this.xrTableCell163,
            this.xrTableCell164,
            this.xrTableCell165,
            this.xrTableCell166,
            this.xrTableCell167,
            this.xrTableCell168,
            this.xrTableCell169,
            this.xrTableCell170,
            this.xrTableCell171});
        this.xrTableRow44.Name = "xrTableRow44";
        this.xrTableRow44.Weight = 0.67999999999999994D;
        // 
        // xrTableCell162
        // 
        this.xrTableCell162.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox5});
        this.xrTableCell162.Name = "xrTableCell162";
        this.xrTableCell162.StylePriority.UseTextAlignment = false;
        this.xrTableCell162.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell162.Weight = 0.060240961968618251D;
        // 
        // xrPictureBox5
        // 
        this.xrPictureBox5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox5.ImageUrl = "~\\imagens\\Azul.gif";
        this.xrPictureBox5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.178914E-05F);
        this.xrPictureBox5.Name = "xrPictureBox5";
        this.xrPictureBox5.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox5.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox5.StylePriority.UseBorders = false;
        // 
        // xrTableCell163
        // 
        this.xrTableCell163.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell163.Name = "xrTableCell163";
        this.xrTableCell163.StylePriority.UseFont = false;
        this.xrTableCell163.StylePriority.UseTextAlignment = false;
        this.xrTableCell163.Text = Resources.rel_StatusReport.MetaSuperada;
        this.xrTableCell163.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell163.Weight = 0.53975904415940945D;
        // 
        // xrTableCell164
        // 
        this.xrTableCell164.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox7});
        this.xrTableCell164.Name = "xrTableCell164";
        this.xrTableCell164.Weight = 0.060240963855421686D;
        // 
        // xrPictureBox7
        // 
        this.xrPictureBox7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox7.ImageUrl = "~\\imagens\\verde.gif";
        this.xrPictureBox7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox7.Name = "xrPictureBox7";
        this.xrPictureBox7.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox7.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox7.StylePriority.UseBorders = false;
        // 
        // xrTableCell165
        // 
        this.xrTableCell165.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell165.Name = "xrTableCell165";
        this.xrTableCell165.StylePriority.UseFont = false;
        this.xrTableCell165.StylePriority.UseTextAlignment = false;
        this.xrTableCell165.Text = Resources.rel_StatusReport.MetaAtingida;
        this.xrTableCell165.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell165.Weight = 0.53975904227260607D;
        // 
        // xrTableCell166
        // 
        this.xrTableCell166.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox8});
        this.xrTableCell166.Name = "xrTableCell166";
        this.xrTableCell166.Weight = 0.060240963855421686D;
        // 
        // xrPictureBox8
        // 
        this.xrPictureBox8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox8.ImageUrl = "~\\imagens\\amarelo.gif";
        this.xrPictureBox8.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 3.178914E-05F);
        this.xrPictureBox8.Name = "xrPictureBox8";
        this.xrPictureBox8.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox8.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox8.StylePriority.UseBorders = false;
        // 
        // xrTableCell167
        // 
        this.xrTableCell167.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell167.Name = "xrTableCell167";
        this.xrTableCell167.StylePriority.UseFont = false;
        this.xrTableCell167.StylePriority.UseTextAlignment = false;
        this.xrTableCell167.Text = Resources.rel_StatusReport.AbaixoDaMetaAlerta;
        this.xrTableCell167.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell167.Weight = 0.539759042272606D;
        // 
        // xrTableCell168
        // 
        this.xrTableCell168.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox9});
        this.xrTableCell168.Name = "xrTableCell168";
        this.xrTableCell168.Weight = 0.060240963855421714D;
        // 
        // xrPictureBox9
        // 
        this.xrPictureBox9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox9.ImageUrl = "~\\imagens\\vermelho.gif";
        this.xrPictureBox9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox9.Name = "xrPictureBox9";
        this.xrPictureBox9.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox9.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox9.StylePriority.UseBorders = false;
        // 
        // xrTableCell169
        // 
        this.xrTableCell169.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell169.Name = "xrTableCell169";
        this.xrTableCell169.StylePriority.UseFont = false;
        this.xrTableCell169.StylePriority.UseTextAlignment = false;
        this.xrTableCell169.Text = Resources.rel_StatusReport.MuitoAbaixoDaMeta;
        this.xrTableCell169.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell169.Weight = 0.53975904227260607D;
        // 
        // xrTableCell170
        // 
        this.xrTableCell170.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox10});
        this.xrTableCell170.Name = "xrTableCell170";
        this.xrTableCell170.Weight = 0.060240963855421742D;
        // 
        // xrPictureBox10
        // 
        this.xrPictureBox10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox10.ImageUrl = "~\\imagens\\Branco.gif";
        this.xrPictureBox10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.178914E-05F);
        this.xrPictureBox10.Name = "xrPictureBox10";
        this.xrPictureBox10.SizeF = new System.Drawing.SizeF(15F, 15F);
        this.xrPictureBox10.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.xrPictureBox10.StylePriority.UseBorders = false;
        // 
        // xrTableCell171
        // 
        this.xrTableCell171.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell171.Name = "xrTableCell171";
        this.xrTableCell171.StylePriority.UseFont = false;
        this.xrTableCell171.StylePriority.UseTextAlignment = false;
        this.xrTableCell171.Text = Resources.rel_StatusReport.AtualizaçãoPendente;
        this.xrTableCell171.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell171.Weight = 0.53975901163246742D;
        // 
        // GroupFooterComentarioMeta
        // 
        this.GroupFooterComentarioMeta.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable29});
        this.GroupFooterComentarioMeta.Expanded = false;
        this.GroupFooterComentarioMeta.HeightF = 60F;
        this.GroupFooterComentarioMeta.Name = "GroupFooterComentarioMeta";
        // 
        // xrTable29
        // 
        this.xrTable29.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable29.LocationFloat = new DevExpress.Utils.PointFloat(10.00093F, 9.999974F);
        this.xrTable29.Name = "xrTable29";
        this.xrTable29.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow36,
            this.xrTableRow37});
        this.xrTable29.SizeF = new System.Drawing.SizeF(747F, 40F);
        this.xrTable29.StylePriority.UseBorders = false;
        // 
        // xrTableRow36
        // 
        this.xrTableRow36.BackColor = System.Drawing.Color.Silver;
        this.xrTableRow36.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell145});
        this.xrTableRow36.Name = "xrTableRow36";
        this.xrTableRow36.StylePriority.UseBackColor = false;
        this.xrTableRow36.Weight = 9.2D;
        // 
        // xrTableCell145
        // 
        this.xrTableCell145.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell145.Name = "xrTableCell145";
        this.xrTableCell145.StylePriority.UseFont = false;
        this.xrTableCell145.StylePriority.UseTextAlignment = false;
        this.xrTableCell145.Text = Resources.rel_StatusReport.ComentárioSobreAsMetasEResultadosDoProjeto;
        this.xrTableCell145.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell145.Weight = 1.3037809647979139D;
        // 
        // xrTableRow37
        // 
        this.xrTableRow37.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell146});
        this.xrTableRow37.Name = "xrTableRow37";
        this.xrTableRow37.Weight = 9.2D;
        // 
        // xrTableCell146
        // 
        this.xrTableCell146.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.ComentarioMeta")});
        this.xrTableCell146.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell146.Multiline = true;
        this.xrTableCell146.Name = "xrTableCell146";
        this.xrTableCell146.StylePriority.UseFont = false;
        this.xrTableCell146.StylePriority.UseTextAlignment = false;
        this.xrTableCell146.Text = "xrTableCell146";
        this.xrTableCell146.Weight = 1.3037809647979139D;
        // 
        // DetailReportToDoList
        // 
        this.DetailReportToDoList.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail12,
            this.DetailReportListaPendencia});
        this.DetailReportToDoList.DataMember = "StatusReport";
        this.DetailReportToDoList.DataSource = this.ds;
        this.DetailReportToDoList.Expanded = false;
        this.DetailReportToDoList.Level = 7;
        this.DetailReportToDoList.Name = "DetailReportToDoList";
        // 
        // Detail12
        // 
        this.Detail12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19});
        this.Detail12.Expanded = false;
        this.Detail12.HeightF = 32.99999F;
        this.Detail12.Name = "Detail12";
        // 
        // xrLabel19
        // 
        this.xrLabel19.BackColor = System.Drawing.Color.Silver;
        this.xrLabel19.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(10.00093F, 10.00001F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(746.9998F, 22.99998F);
        this.xrLabel19.StylePriority.UseBackColor = false;
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = Resources.rel_StatusReport.PendênciasDeToDoList;
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReportListaPendencia
        // 
        this.DetailReportListaPendencia.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail15,
            this.GroupHeader8});
        this.DetailReportListaPendencia.DataMember = "StatusReport.StatusReport_TarefasToDoList";
        this.DetailReportListaPendencia.DataSource = this.ds;
        this.DetailReportListaPendencia.Level = 0;
        this.DetailReportListaPendencia.Name = "DetailReportListaPendencia";
        // 
        // Detail15
        // 
        this.Detail15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable23});
        this.Detail15.HeightF = 15F;
        this.Detail15.Name = "Detail15";
        // 
        // xrTable23
        // 
        this.xrTable23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable23.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable23.LocationFloat = new DevExpress.Utils.PointFloat(10.00074F, 0F);
        this.xrTable23.Name = "xrTable23";
        this.xrTable23.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow29});
        this.xrTable23.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable23.StylePriority.UseBackColor = false;
        this.xrTable23.StylePriority.UseBorders = false;
        this.xrTable23.StylePriority.UseFont = false;
        // 
        // xrTableRow29
        // 
        this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell114,
            this.xrTableCell115,
            this.xrTableCell116,
            this.xrTableCell117,
            this.xrTableCell118});
        this.xrTableRow29.Name = "xrTableRow29";
        this.xrTableRow29.Weight = 11.5D;
        // 
        // xrTableCell114
        // 
        this.xrTableCell114.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasToDoList.Tarefa")});
        this.xrTableCell114.Name = "xrTableCell114";
        this.xrTableCell114.Text = Resources.rel_StatusReport.TarefaDeToDoList;
        this.xrTableCell114.Weight = 0.39270511498069932D;
        // 
        // xrTableCell115
        // 
        this.xrTableCell115.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasToDoList.DescricaoStatusTarefa")});
        this.xrTableCell115.Name = "xrTableCell115";
        this.xrTableCell115.Text = Resources.rel_StatusReport.Status4;
        this.xrTableCell115.Weight = 0.13351973225701297D;
        // 
        // xrTableCell116
        // 
        this.xrTableCell116.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasToDoList.InicioPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell116.Name = "xrTableCell116";
        this.xrTableCell116.StylePriority.UseTextAlignment = false;
        this.xrTableCell116.Text = Resources.rel_StatusReport.InícioPrevisto3;
        this.xrTableCell116.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell116.Weight = 0.12566563785654294D;
        // 
        // xrTableCell117
        // 
        this.xrTableCell117.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasToDoList.TerminoPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell117.Name = "xrTableCell117";
        this.xrTableCell117.StylePriority.UseTextAlignment = false;
        this.xrTableCell117.Text = Resources.rel_StatusReport.TérminoPrevisto4;
        this.xrTableCell117.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell117.Weight = 0.12566562986693508D;
        // 
        // xrTableCell118
        // 
        this.xrTableCell118.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_TarefasToDoList.Responsavel")});
        this.xrTableCell118.Name = "xrTableCell118";
        this.xrTableCell118.Text = Resources.rel_StatusReport.Responsável3;
        this.xrTableCell118.Weight = 0.39584675335693226D;
        // 
        // GroupHeader8
        // 
        this.GroupHeader8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable22});
        this.GroupHeader8.Expanded = false;
        this.GroupHeader8.HeightF = 40F;
        this.GroupHeader8.Name = "GroupHeader8";
        this.GroupHeader8.RepeatEveryPage = true;
        // 
        // xrTable22
        // 
        this.xrTable22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable22.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable22.LocationFloat = new DevExpress.Utils.PointFloat(10.00074F, 10F);
        this.xrTable22.Name = "xrTable22";
        this.xrTable22.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable22.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable22.StylePriority.UseBackColor = false;
        this.xrTable22.StylePriority.UseBorders = false;
        this.xrTable22.StylePriority.UseFont = false;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell105,
            this.xrTableCell107,
            this.xrTableCell111,
            this.xrTableCell112,
            this.xrTableCell113});
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 11.5D;
        // 
        // xrTableCell105
        // 
        this.xrTableCell105.Name = "xrTableCell105";
        this.xrTableCell105.Text = Resources.rel_StatusReport.Tarefa2;
        this.xrTableCell105.Weight = 0.39270511498069932D;
        // 
        // xrTableCell107
        // 
        this.xrTableCell107.Name = "xrTableCell107";
        this.xrTableCell107.Text = Resources.rel_StatusReport.Status5;
        this.xrTableCell107.Weight = 0.13351973225701297D;
        // 
        // xrTableCell111
        // 
        this.xrTableCell111.Name = "xrTableCell111";
        this.xrTableCell111.StylePriority.UseTextAlignment = false;
        this.xrTableCell111.Text = Resources.rel_StatusReport.InícioPrevisto4;
        this.xrTableCell111.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell111.Weight = 0.12566563785654294D;
        // 
        // xrTableCell112
        // 
        this.xrTableCell112.Name = "xrTableCell112";
        this.xrTableCell112.StylePriority.UseTextAlignment = false;
        this.xrTableCell112.Text = Resources.rel_StatusReport.TérminoPrevisto5;
        this.xrTableCell112.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell112.Weight = 0.12566562986693508D;
        // 
        // xrTableCell113
        // 
        this.xrTableCell113.Name = "xrTableCell113";
        this.xrTableCell113.Text = Resources.rel_StatusReport.Responsável4;
        this.xrTableCell113.Weight = 0.39584675335693226D;
        // 
        // DetailReportContratos
        // 
        this.DetailReportContratos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail13,
            this.DetailReportListaContrato});
        this.DetailReportContratos.DataMember = "StatusReport";
        this.DetailReportContratos.DataSource = this.ds;
        this.DetailReportContratos.Expanded = false;
        this.DetailReportContratos.Level = 8;
        this.DetailReportContratos.Name = "DetailReportContratos";
        // 
        // Detail13
        // 
        this.Detail13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18});
        this.Detail13.HeightF = 32.99995F;
        this.Detail13.Name = "Detail13";
        // 
        // xrLabel18
        // 
        this.xrLabel18.BackColor = System.Drawing.Color.Silver;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(10.00055F, 9.999974F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(746.9998F, 22.99998F);
        this.xrLabel18.StylePriority.UseBackColor = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = Resources.rel_StatusReport.Contratos;
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReportListaContrato
        // 
        this.DetailReportListaContrato.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail14,
            this.GroupHeader7,
            this.GroupFooter5});
        this.DetailReportListaContrato.DataMember = "StatusReport.StatusReport_Contratos";
        this.DetailReportListaContrato.DataSource = this.ds;
        this.DetailReportListaContrato.Level = 0;
        this.DetailReportListaContrato.Name = "DetailReportListaContrato";
        // 
        // Detail14
        // 
        this.Detail14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable20});
        this.Detail14.HeightF = 15F;
        this.Detail14.Name = "Detail14";
        // 
        // xrTable20
        // 
        this.xrTable20.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(10.00055F, 0F);
        this.xrTable20.Name = "xrTable20";
        this.xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable20.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable20.StylePriority.UseBorders = false;
        this.xrTable20.StylePriority.UseFont = false;
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell102,
            this.xrTableCell99,
            this.xrTableCell103,
            this.xrTableCell100,
            this.xrTableCell104,
            this.xrTableCell101});
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 1D;
        // 
        // xrTableCell102
        // 
        this.xrTableCell102.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.NumeroContrato")});
        this.xrTableCell102.Name = "xrTableCell102";
        this.xrTableCell102.Text = "xrTableCell102";
        this.xrTableCell102.Weight = 0.44176754842557292D;
        // 
        // xrTableCell99
        // 
        this.xrTableCell99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.Fornecedor")});
        this.xrTableCell99.Name = "xrTableCell99";
        this.xrTableCell99.Text = "xrTableCell99";
        this.xrTableCell99.Weight = 1.0321285293162885D;
        // 
        // xrTableCell103
        // 
        this.xrTableCell103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.TerminoVigencia", "{0:dd/MM/yyyy}")});
        this.xrTableCell103.Name = "xrTableCell103";
        this.xrTableCell103.StylePriority.UseTextAlignment = false;
        this.xrTableCell103.Text = "xrTableCell103";
        this.xrTableCell103.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell103.Weight = 0.32128515459118273D;
        // 
        // xrTableCell100
        // 
        this.xrTableCell100.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.ValorContrato", "{0:n2}")});
        this.xrTableCell100.Name = "xrTableCell100";
        this.xrTableCell100.StylePriority.UseTextAlignment = false;
        this.xrTableCell100.Text = "xrTableCell100";
        this.xrTableCell100.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell100.Weight = 0.40160643973121934D;
        // 
        // xrTableCell104
        // 
        this.xrTableCell104.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.ValorPago", "{0:n2}")});
        this.xrTableCell104.Name = "xrTableCell104";
        this.xrTableCell104.StylePriority.UseTextAlignment = false;
        this.xrTableCell104.Text = "xrTableCell104";
        this.xrTableCell104.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell104.Weight = 0.401606439731069D;
        // 
        // xrTableCell101
        // 
        this.xrTableCell101.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.ValorRestante", "{0:n2}")});
        this.xrTableCell101.Name = "xrTableCell101";
        this.xrTableCell101.StylePriority.UseTextAlignment = false;
        this.xrTableCell101.Text = "xrTableCell101";
        this.xrTableCell101.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell101.Weight = 0.40160588820466736D;
        // 
        // GroupHeader7
        // 
        this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable19});
        this.GroupHeader7.Expanded = false;
        this.GroupHeader7.HeightF = 40F;
        this.GroupHeader7.Name = "GroupHeader7";
        this.GroupHeader7.RepeatEveryPage = true;
        // 
        // xrTable19
        // 
        this.xrTable19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrTable19.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(10.00055F, 10F);
        this.xrTable19.Name = "xrTable19";
        this.xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow25});
        this.xrTable19.SizeF = new System.Drawing.SizeF(747F, 30F);
        this.xrTable19.StylePriority.UseBackColor = false;
        this.xrTable19.StylePriority.UseBorders = false;
        this.xrTable19.StylePriority.UseFont = false;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell93,
            this.xrTableCell94,
            this.xrTableCell95,
            this.xrTableCell96,
            this.xrTableCell97,
            this.xrTableCell98});
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 11.5D;
        // 
        // xrTableCell93
        // 
        this.xrTableCell93.Name = "xrTableCell93";
        this.xrTableCell93.Text = Resources.rel_StatusReport.NºDoContrato;
        this.xrTableCell93.Weight = 0.17279024834671147D;
        // 
        // xrTableCell94
        // 
        this.xrTableCell94.Name = "xrTableCell94";
        this.xrTableCell94.Text = Resources.rel_StatusReport.Fornecedor;
        this.xrTableCell94.Weight = 0.40370085295549868D;
        // 
        // xrTableCell95
        // 
        this.xrTableCell95.Name = "xrTableCell95";
        this.xrTableCell95.StylePriority.UseTextAlignment = false;
        this.xrTableCell95.Text = Resources.rel_StatusReport.TérminoVigência;
        this.xrTableCell95.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell95.Weight = 0.12566563516124465D;
        // 
        // xrTableCell96
        // 
        this.xrTableCell96.Name = "xrTableCell96";
        this.xrTableCell96.StylePriority.UseTextAlignment = false;
        this.xrTableCell96.Text = Resources.rel_StatusReport.ValorTotalR;
        this.xrTableCell96.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell96.Weight = 0.15708204395155592D;
        // 
        // xrTableCell97
        // 
        this.xrTableCell97.Name = "xrTableCell97";
        this.xrTableCell97.StylePriority.UseTextAlignment = false;
        this.xrTableCell97.Text = Resources.rel_StatusReport.ValorPagoR;
        this.xrTableCell97.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell97.Weight = 0.15708204395155589D;
        // 
        // xrTableCell98
        // 
        this.xrTableCell98.Name = "xrTableCell98";
        this.xrTableCell98.StylePriority.UseTextAlignment = false;
        this.xrTableCell98.Text = Resources.rel_StatusReport.ValorRestanteR;
        this.xrTableCell98.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell98.Weight = 0.157082043951556D;
        // 
        // GroupFooter5
        // 
        this.GroupFooter5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable21});
        this.GroupFooter5.HeightF = 15F;
        this.GroupFooter5.Name = "GroupFooter5";
        // 
        // xrTable21
        // 
        this.xrTable21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable21.LocationFloat = new DevExpress.Utils.PointFloat(10.00058F, 0F);
        this.xrTable21.Name = "xrTable21";
        this.xrTable21.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable21.SizeF = new System.Drawing.SizeF(747F, 15F);
        this.xrTable21.StylePriority.UseBorders = false;
        this.xrTable21.StylePriority.UseFont = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell106,
            this.xrTableCell108,
            this.xrTableCell109,
            this.xrTableCell110});
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1D;
        // 
        // xrTableCell106
        // 
        this.xrTableCell106.Name = "xrTableCell106";
        this.xrTableCell106.Text = Resources.rel_StatusReport.TOTAL3;
        this.xrTableCell106.Weight = 1.7951810957435557D;
        // 
        // xrTableCell108
        // 
        this.xrTableCell108.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.ValorContrato")});
        this.xrTableCell108.Name = "xrTableCell108";
        this.xrTableCell108.StylePriority.UseTextAlignment = false;
        xrSummary14.FormatString = "{0:n2}";
        xrSummary14.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell108.Summary = xrSummary14;
        this.xrTableCell108.Text = "xrTableCell108";
        this.xrTableCell108.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell108.Weight = 0.40160657632070773D;
        // 
        // xrTableCell109
        // 
        this.xrTableCell109.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.ValorPago")});
        this.xrTableCell109.Name = "xrTableCell109";
        this.xrTableCell109.StylePriority.UseTextAlignment = false;
        xrSummary15.FormatString = "{0:n2}";
        xrSummary15.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell109.Summary = xrSummary15;
        this.xrTableCell109.Text = "xrTableCell109";
        this.xrTableCell109.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell109.Weight = 0.401606439731069D;
        // 
        // xrTableCell110
        // 
        this.xrTableCell110.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.StatusReport_Contratos.ValorRestante")});
        this.xrTableCell110.Name = "xrTableCell110";
        this.xrTableCell110.StylePriority.UseTextAlignment = false;
        xrSummary16.FormatString = "{0:n2}";
        xrSummary16.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell110.Summary = xrSummary16;
        this.xrTableCell110.Text = "xrTableCell110";
        this.xrTableCell110.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell110.Weight = 0.40160588820466736D;
        // 
        // xrTableCell133
        // 
        this.xrTableCell133.Name = "xrTableCell133";
        this.xrTableCell133.Text = Resources.rel_StatusReport.Questão1;
        this.xrTableCell133.Weight = 3.0789826275425867D;
        // 
        // DetailComentarioGeral
        // 
        this.DetailComentarioGeral.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
        this.DetailComentarioGeral.Expanded = false;
        this.DetailComentarioGeral.Level = 0;
        this.DetailComentarioGeral.Name = "DetailComentarioGeral";
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.lblDataAnalise,
            this.xrtComentarioGeral});
        this.Detail.HeightF = 75F;
        this.Detail.Name = "Detail";
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "StatusReport.ComentarioGeral")});
        this.xrLabel38.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(10.00106F, 30.00002F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.SerializableRtfString = resources.GetString("xrLabel38.SerializableRtfString");
        this.xrLabel38.SizeF = new System.Drawing.SizeF(746.9985F, 20F);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.cellAnaliseCritica_EvaluateBinding);
        // 
        // lblDataAnalise
        // 
        this.lblDataAnalise.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StatusReport.DataAnalisePerformance", Resources.rel_StatusReport.DataDestaAnáliseCríticaX0DdMMYyyy)});
        this.lblDataAnalise.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDataAnalise.LocationFloat = new DevExpress.Utils.PointFloat(10F, 60F);
        this.lblDataAnalise.Name = "lblDataAnalise";
        this.lblDataAnalise.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDataAnalise.SizeF = new System.Drawing.SizeF(300F, 15F);
        this.lblDataAnalise.StylePriority.UseFont = false;
        this.lblDataAnalise.Text = "lblDataAnalise";
        // 
        // DetailPlanoAcao
        // 
        this.DetailPlanoAcao.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail19});
        this.DetailPlanoAcao.Expanded = false;
        this.DetailPlanoAcao.Level = 1;
        this.DetailPlanoAcao.Name = "DetailPlanoAcao";
        // 
        // Detail19
        // 
        this.Detail19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtPlanoAcao});
        this.Detail19.HeightF = 60F;
        this.Detail19.Name = "Detail19";
        // 
        // xrLabel40
        // 
        this.xrLabel40.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Italic);
        this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 14.99999F);
        this.xrLabel40.Multiline = true;
        this.xrLabel40.Name = "xrLabel40";
        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel40.SizeF = new System.Drawing.SizeF(748.8188F, 30.00001F);
        this.xrLabel40.StylePriority.UseFont = false;
        this.xrLabel40.StylePriority.UseTextAlignment = false;
        this.xrLabel40.Text =
        Resources.rel_StatusReport.DesvioDespesaPrevistaAtéADataDespesaRealiz;
        // 
        // rel_StatusReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.DetailGeral,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReportFisico,
            this.PageHeader,
            this.PageFooter,
            this.DetailReportFinanceiro,
            this.DetailReportRiscos,
            this.DetailReportQuestoes,
            this.DetailReportMetas,
            this.DetailReportToDoList,
            this.DetailReportContratos,
            this.DetailComentarioGeral,
            this.DetailPlanoAcao});
        this.DataMember = "StatusReport";
        this.DataSource = this.ds;
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "15.1";
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rel_StatusReport_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this.xrtPlanoAcao)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrtComentarioGeral)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable33)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable35)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable34)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable36)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable30)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable32)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable31)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrLabel38)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Event Handlers
    private void rel_StatusReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        InitializeLayout();
    }

    private void cellAnaliseCritica_EvaluateBinding(object sender, BindingEventArgs e)
    {
        XRControl control = (XRControl)sender;
        String value = (e.Value as string) ?? string.Empty;
        if (String.IsNullOrEmpty(value.Trim()))
        {
            control.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            e.Value = Resources.rel_StatusReport.SemAnáliseCríticaParaOPeríodo;
            lblDataAnalise.Visible = false;
        }
        else
        {
            lblDataAnalise.Visible = true;
        }
    }
    #endregion
}
