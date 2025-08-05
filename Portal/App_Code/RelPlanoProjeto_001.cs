using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;

/// <summary>
/// Summary description for RelPlanoProjeto_001
/// </summary>
public class RelPlanoProjeto_001 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoProjeto;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoUsuario;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoEntidade;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private PageHeaderBand PageHeader;
    private XRPictureBox picLogoRelatorio;
    private XRControlStyle Title;
    private DsPlanoProjeto dsPlanoProjeto;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel32;
    private XRLabel xrLabel26;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    private XRLabel xrLabel14;
    private XRLabel xrLabel8;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel15;
    private XRLabel xrLabel17;
    private XRLabel xrLabel18;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private DetailReportBand DetailReport11;
    private DetailBand Detail12;
    private DetailReportBand DetailReport3;
    private DetailBand Detail4;
    private XRLabel xrLabel16;
    private DetailReportBand DetailReport4;
    private DetailBand Detail5;
    private DetailReportBand DetailReport5;
    private DetailBand Detail6;
    private DetailReportBand DetailReport6;
    private DetailBand Detail7;
    private XRLabel xrLabel19;
    private DetailReportBand DetailReport12;
    private DetailBand Detail13;
    private XRLabel xrLabel20;
    private DetailReportBand DetailReport7;
    private DetailBand Detail8;
    private XRLabel xrLabel21;
    private DetailReportBand DetailReport8;
    private DetailBand Detail9;
    private XRLabel xrLabel22;
    private XRControlStyle Table;
    private XRTable xrTable7;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable9;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private GroupHeaderBand GroupHeader3;
    private XRTable xrTable8;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRLabel xrLabel33;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRTable xrTable11;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell47;
    private GroupHeaderBand GroupHeader4;
    private XRTable xrTable10;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell48;
    private XRTable xrTable13;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private GroupHeaderBand GroupHeader5;
    private XRTable xrTable12;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell53;
    private XRTableCell cellDescricaoIntegrantesGrupo1;
    private XRTableCell cellDescricaoIntegrantesGrupo2;
    private XRTableCell cellDescricaoIntegrantesGrupo3;
    private XRTableCell cellDescricaoIntegrantesGrupo4;
    private XRTableCell cellDescricaoIntegrantesGrupo5;
    private XRTableCell cellDescricaoIntegrantesGrupo6;
    private XRTableCell cellDescricaoIntegrantesGrupo7;
    private XRTableCell cellDescricaoIntegrantesGrupo8;
    private XRTableCell cellDescricaoIntegrantesGrupo9;
    private XRTable xrTable15;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell61;
    private GroupHeaderBand GroupHeader6;
    private XRLabel xrLabel35;
    private XRTable xrTable14;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTable xrTable16;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell78;
    private XRTableCell xrTableCell79;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private GroupHeaderBand GroupHeader7;
    private XRTable xrTable17;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private GroupHeaderBand GroupHeader8;
    private XRTable xrTable18;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell89;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell83;
    private XRTableCell xrTableCell91;
    private XRTableCell xrTableCell84;
    private XRTableCell xrTableCell92;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell86;
    private XRTableCell xrTableCell87;
    private XRTable xrTable19;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell94;
    private XRTableCell xrTableCell95;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell100;
    private XRTableCell xrTableCell101;
    private XRTableCell xrTableCell102;
    private XRTableCell xrTableCell103;
    private XRTable xrTable21;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell106;
    private XRTableCell xrTableCell108;
    private XRTableCell xrTableCell109;
    private GroupHeaderBand GroupHeader9;
    private XRTable xrTable20;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell104;
    private XRTableCell xrTableCell105;
    private XRTableCell xrTableCell107;
    private XRTable xrTable23;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell114;
    private XRTableCell xrTableCell115;
    private XRTableCell xrTableCell116;
    private XRTableCell xrTableCell117;
    private GroupHeaderBand GroupHeader10;
    private XRTable xrTable22;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell110;
    private XRTableCell xrTableCell111;
    private XRTableCell xrTableCell112;
    private XRTableCell xrTableCell113;
    private XRLabel xrLabel38;
    private XRLabel xrLabel37;
    private XRLabel lblCabecalhoPagina;
    private XRLine xrLine2;
    private XRLine xrLine1;
    private PageFooterBand PageFooter;
    private XRLine xrLine3;
    private XRLabel xrLabel34;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoStatusReport;
    private DetailReportBand DetailReport9;
    private DetailBand Detail10;
    private XRTable xrTable25;
    private FormattingRule EAP_FonteNegrito;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell118;
    private XRTableCell xrTableCell124;
    private XRTableCell xrTableCell125;
    private XRTableCell xrTableCell126;
    private XRTableCell xrTableCell127;
    private GroupHeaderBand GroupHeader11;
    private XRLabel xrLabel41;
    private XRTable xrTable24;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell119;
    private XRTableCell xrTableCell120;
    private XRTableCell xrTableCell121;
    private XRTableCell xrTableCell122;
    private XRTableCell xrTableCell123;
    private GroupHeaderBand GroupHeader12;
    private GroupHeaderBand GroupHeader13;
    private XRTable xrTable6;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private GroupHeaderBand GroupHeader14;
    private GroupHeaderBand GroupHeader16;
    private GroupHeaderBand GroupHeader15;
    private GroupHeaderBand GroupHeader17;
    private GroupHeaderBand GroupHeader18;
    private GroupHeaderBand GroupHeader19;
    private XRPageInfo xrPageInfo1;
    private GroupHeaderBand GroupHeader20;
    private GroupHeaderBand GroupHeader21;
    private DevExpress.XtraReports.Parameters.Parameter pDataGeracao;
    private DevExpress.XtraReports.Parameters.Parameter pNomeProjeto;
    private XRPanel xrPanel1;
    private XRPanel xrPanel5;
    private XRLabel xrLabel29;
    private XRLabel xrLabel11;
    private XRPanel xrPanel4;
    private XRLabel xrLabel30;
    private XRLabel xrLabel12;
    private XRPanel xrPanel3;
    private XRLabel xrLabel13;
    private XRLabel xrLabel31;
    private XRPanel xrPanel2;
    private XRPanel xrPanel8;
    private XRLabel xrLabel25;
    private XRLabel xrLabel7;
    private XRPanel xrPanel7;
    private XRLabel xrLabel27;
    private XRLabel xrLabel9;
    private XRPanel xrPanel6;
    private XRLabel xrLabel10;
    private XRLabel xrLabel28;
    private XRPanel xrPanel9;
    private XRPanel xrPanel10;
    private XRPanel xrPanel13;
    private XRLabel xrLabel4;
    private XRTable xrTable5;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRPanel xrPanel12;
    private XRLabel xrLabel3;
    private XRTable xrTable4;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRPanel xrPanel11;
    private XRLabel xrLabel2;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private GroupHeaderBand GroupHeader22;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public RelPlanoProjeto_001()
    {
        InitializeComponent();
    }

    private void InitData()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format("exec p_rel_StatusReportPPJ01 {0}", pCodigoStatusReport.Value);

        #endregion

        //DataSet ds = cDados.getDataSet(comandoSql);

        SqlDataAdapter adapter = new SqlDataAdapter(comandoSql, cDados.classeDados.getStringConexao());
        adapter.TableMappings.Add("Table", "Revisao");
        adapter.TableMappings.Add("Table1", "HistoricoVersoesPlano");
        adapter.TableMappings.Add("Table2", "AlinhamentoEstrategico");
        adapter.TableMappings.Add("Table3", "Entregas");
        adapter.TableMappings.Add("Table4", "EstruturaTopicosEAP");
        adapter.TableMappings.Add("Table5", "MatrizResponsabilidadeIntegrantes");
        adapter.TableMappings.Add("Table6", "EquipeProjeto");
        adapter.TableMappings.Add("Table7", "PlanoComunicacao");
        adapter.TableMappings.Add("Table8", "Riscos");
        adapter.TableMappings.Add("Table9", "RelacionamentoProjeto");
        adapter.TableMappings.Add("Table10", "MatrizResponsabilidadesIdentificacaoGrupos");
        DsPlanoProjeto ds = new DsPlanoProjeto();
        adapter.Fill(ds);
        DataSource = ds;

        var rowMtzResp = ds.MatrizResponsabilidadesIdentificacaoGrupos.AsEnumerable().SingleOrDefault();
        if (rowMtzResp != null)
        {
            cellDescricaoIntegrantesGrupo1.Text = rowMtzResp.IdentificacaoGrupo1;
            cellDescricaoIntegrantesGrupo2.Text = rowMtzResp.IdentificacaoGrupo2;
            cellDescricaoIntegrantesGrupo3.Text = rowMtzResp.IdentificacaoGrupo3;
            cellDescricaoIntegrantesGrupo4.Text = rowMtzResp.IdentificacaoGrupo4;
            cellDescricaoIntegrantesGrupo5.Text = rowMtzResp.IdentificacaoGrupo5;
            cellDescricaoIntegrantesGrupo6.Text = rowMtzResp.IdentificacaoGrupo6;
            cellDescricaoIntegrantesGrupo7.Text = rowMtzResp.IdentificacaoGrupo7;
            cellDescricaoIntegrantesGrupo8.Text = rowMtzResp.IdentificacaoGrupo8;
            cellDescricaoIntegrantesGrupo9.Text = rowMtzResp.IdentificacaoGrupo9;
        }
        var rowRevisao = ds.Revisao.AsEnumerable().SingleOrDefault();
        if (rowRevisao != null)
        {
            pNomeProjeto.Value = rowRevisao.NomeProjeto;
            pDataGeracao.Value = rowRevisao.DataGeracao;
        }
        int nCodigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        lblCabecalhoPagina.Text = cDados.getTextoPadraoEntidade(nCodigoEntidade, "TtlSttRptPPJ01");

        DataSet dsTemp = cDados.getParametrosSistema("UrlLogo01PlanoProjeto");
        if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0 &&
            dsTemp.Tables[0].Columns.IndexOf("UrlLogo01PlanoProjeto") > -1)
        {
            string urlLogoPlanoProjeto =
                dsTemp.Tables[0].Rows[0]["UrlLogo01PlanoProjeto"] as string;
            picLogoRelatorio.ImageUrl = urlLogoPlanoProjeto;
        }

        if (string.IsNullOrWhiteSpace(picLogoRelatorio.ImageUrl))
        {
            System.Drawing.Image logo = cDados.ObtemLogoEntidade();
            picLogoRelatorio.Image = logo;
        }

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
        string resourceFileName = "RelPlanoProjeto_001.resx";
        System.Resources.ResourceManager resources = global::Resources.RelPlanoProjeto_001.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
        this.pCodigoProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.pCodigoUsuario = new DevExpress.XtraReports.Parameters.Parameter();
        this.pCodigoEntidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.pDataGeracao = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.pNomeProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.lblCabecalhoPagina = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.picLogoRelatorio = new DevExpress.XtraReports.UI.XRPictureBox();
        this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
        this.dsPlanoProjeto = new DsPlanoProjeto();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader22 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel13 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPanel12 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPanel11 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPanel10 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel9 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel8 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel7 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel6 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.DetailReport11 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail12 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.GroupHeader13 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader12 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.cellDescricaoIntegrantesGrupo9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader19 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader14 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport6 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable19 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader8 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable18 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader16 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport12 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail13 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader7 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader15 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport7 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail8 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable21 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell108 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader9 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable20 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader20 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport8 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail9 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable23 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell114 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell115 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell117 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader10 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable22 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell113 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader17 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.Table = new DevExpress.XtraReports.UI.XRControlStyle();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader18 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.pCodigoStatusReport = new DevExpress.XtraReports.Parameters.Parameter();
        this.DetailReport9 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail10 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable25 = new DevExpress.XtraReports.UI.XRTable();
        this.EAP_FonteNegrito = new DevExpress.XtraReports.UI.FormattingRule();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell125 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell126 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell127 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader11 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable24 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell119 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell120 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell122 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell123 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader21 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.dsPlanoProjeto)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Dpi = 254F;
        this.Detail.Expanded = false;
        this.Detail.HeightF = 0F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // FieldCaption
        // 
        this.FieldCaption.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FieldCaption.Name = "FieldCaption";
        // 
        // PageInfo
        // 
        this.PageInfo.Name = "PageInfo";
        // 
        // DataField
        // 
        this.DataField.Name = "DataField";
        // 
        // pCodigoProjeto
        // 
        this.pCodigoProjeto.Name = "pCodigoProjeto";
        this.pCodigoProjeto.Type = typeof(int);
        this.pCodigoProjeto.ValueInfo = "0";
        this.pCodigoProjeto.Visible = false;
        // 
        // pCodigoUsuario
        // 
        this.pCodigoUsuario.Name = "pCodigoUsuario";
        this.pCodigoUsuario.Type = typeof(int);
        this.pCodigoUsuario.ValueInfo = "0";
        this.pCodigoUsuario.Visible = false;
        // 
        // pCodigoEntidade
        // 
        this.pCodigoEntidade.Name = "pCodigoEntidade";
        this.pCodigoEntidade.Type = typeof(int);
        this.pCodigoEntidade.ValueInfo = "0";
        this.pCodigoEntidade.Visible = false;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 100F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 100F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrLabel37,
            this.lblCabecalhoPagina,
            this.xrLine2,
            this.xrLine1,
            this.picLogoRelatorio});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 250F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLabel38
        // 
        this.xrLabel38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pDataGeracao, "Text", "Data: {0:dd/MM/yyyy}")});
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel38.ForeColor = System.Drawing.Color.Gray;
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 192F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(2225F, 40F);
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseForeColor = false;
        this.xrLabel38.Text = "Data:";
        // 
        // pDataGeracao
        // 
        this.pDataGeracao.Name = "pDataGeracao";
        this.pDataGeracao.Type = typeof(System.DateTime);
        this.pDataGeracao.Visible = false;
        // 
        // xrLabel37
        // 
        this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pNomeProjeto, "Text", "Nome Projeto: {0}")});
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel37.ForeColor = System.Drawing.Color.Gray;
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 152F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(2225F, 40F);
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseForeColor = false;
        this.xrLabel37.Text = "Nome do Projeto: ";
        // 
        // pNomeProjeto
        // 
        this.pNomeProjeto.Description = "Parameter1";
        this.pNomeProjeto.Name = "pNomeProjeto";
        this.pNomeProjeto.Visible = false;
        // 
        // lblCabecalhoPagina
        // 
        this.lblCabecalhoPagina.Dpi = 254F;
        this.lblCabecalhoPagina.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
        this.lblCabecalhoPagina.ForeColor = System.Drawing.Color.Gray;
        this.lblCabecalhoPagina.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 25.00001F);
        this.lblCabecalhoPagina.Multiline = true;
        this.lblCabecalhoPagina.Name = "lblCabecalhoPagina";
        this.lblCabecalhoPagina.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblCabecalhoPagina.SizeF = new System.Drawing.SizeF(2225F, 127F);
        this.lblCabecalhoPagina.StylePriority.UseFont = false;
        this.lblCabecalhoPagina.StylePriority.UseForeColor = false;
        this.lblCabecalhoPagina.Text = "METODOLOGIA CAIXA DE GERENCIAMENTO DE PROJETOS – TI [MCGP-TI]\t\r\nPLANO DE PROJETO";
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.ForeColor = System.Drawing.Color.LightGray;
        this.xrLine2.LineWidth = 5;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 240F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(2770F, 10F);
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.LightGray;
        this.xrLine1.LineWidth = 5;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(2770F, 10F);
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // picLogoRelatorio
        // 
        this.picLogoRelatorio.Dpi = 254F;
        this.picLogoRelatorio.Image = ((System.Drawing.Image)(resources.GetObject("picLogoRelatorio.Image")));
        this.picLogoRelatorio.KeepTogether = false;
        this.picLogoRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(2300F, 25F);
        this.picLogoRelatorio.Name = "picLogoRelatorio";
        this.picLogoRelatorio.SizeF = new System.Drawing.SizeF(444.5F, 127F);
        this.picLogoRelatorio.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        // 
        // Title
        // 
        this.Title.Name = "Title";
        // 
        // dsPlanoProjeto
        // 
        this.dsPlanoProjeto.DataSetName = "DsPlanoProjeto";
        this.dsPlanoProjeto.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader1,
            this.GroupHeader22});
        this.DetailReport1.DataMember = "Revisao.DadosRevisao_HistoricoVersoesPlano";
        this.DetailReport1.DataSource = this.dsPlanoProjeto;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 50F;
        this.Detail2.Name = "Detail2";
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable2.StyleName = "Table";
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.567901234567901D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DadosRevisao_HistoricoVersoesPlano.NumeroVersaoRevisaoProjeto")});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 11, 0, 0, 254F);
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Versão";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell1.Weight = 0.0529868900594362D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DadosRevisao_HistoricoVersoesPlano.DataRevisao", "{0:d}")});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(11, 0, 0, 0, 254F);
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Data da Revisão";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell2.Weight = 0.110389352044608D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DadosRevisao_HistoricoVersoesPlano.DescricaoRevisao")});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.Text = "Descrição";
        this.xrTableCell7.Weight = 0.830127942018575D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DadosRevisao_HistoricoVersoesPlano.IdentificacaoAutorRevisao")});
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.Text = "Autor";
        this.xrTableCell8.Weight = 0.229609645811756D;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 50F;
        this.GroupHeader1.KeepTogether = true;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.0003051758F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable1.StyleName = "Table";
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.567901234567901D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "Versão";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell3.Weight = 0.0529868900594362D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Data da Revisão";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.110389352044608D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Text = "Descrição";
        this.xrTableCell5.Weight = 0.830127942018575D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(7, 0, 0, 0, 254F);
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.Text = "Autor";
        this.xrTableCell6.Weight = 0.229609645811756D;
        // 
        // GroupHeader22
        // 
        this.GroupHeader22.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.GroupHeader22.Dpi = 254F;
        this.GroupHeader22.HeightF = 92.39583F;
        this.GroupHeader22.Level = 1;
        this.GroupHeader22.Name = "GroupHeader22";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 42.39583F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel1.StyleName = "FieldCaption";
        this.xrLabel1.StylePriority.UseBorderColor = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "1. Histórico de Revisões no documento";
        // 
        // xrLabel32
        // 
        this.xrLabel32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoOrcamentoPrevisto")});
        this.xrLabel32.Dpi = 254F;
        this.xrLabel32.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 48F);
        this.xrLabel32.Multiline = true;
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(2770F, 129.6599F);
        this.xrLabel32.StyleName = "DataField";
        this.xrLabel32.StylePriority.UseBorders = false;
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.Text = "13. Orçamento Previsto";
        // 
        // xrLabel26
        // 
        this.xrLabel26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoStakeholders")});
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel26.KeepTogether = true;
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 47.34924F);
        this.xrLabel26.Multiline = true;
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(2769.998F, 129.38F);
        this.xrLabel26.StyleName = "DataField";
        this.xrLabel26.StylePriority.UseBorders = false;
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.Text = "7. Stakeholders";
        // 
        // xrLabel24
        // 
        this.xrLabel24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoObjetivosEspecificos")});
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.99977F);
        this.xrLabel24.Multiline = true;
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(2770F, 127.6603F);
        this.xrLabel24.StyleName = "DataField";
        this.xrLabel24.StylePriority.UseBorders = false;
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.Text = "5.1 Objetivos Específicos";
        // 
        // xrLabel23
        // 
        this.xrLabel23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoObjetivoGeral")});
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrLabel23.Multiline = true;
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(2769.999F, 127.66F);
        this.xrLabel23.StyleName = "DataField";
        this.xrLabel23.StylePriority.UseBorders = false;
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.Text = "5. Objetivo Geral";
        // 
        // xrLabel14
        // 
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(2770F, 48F);
        this.xrLabel14.StyleName = "FieldCaption";
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.Text = "13. Orçamento Previsto";
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(2769.998F, 47.34924F);
        this.xrLabel8.StyleName = "FieldCaption";
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "7. Stakeholders";
        // 
        // xrLabel6
        // 
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrLabel6.StyleName = "FieldCaption";
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "5.1 Objetivos Específicos";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrLabel5.StyleName = "FieldCaption";
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "5. Objetivo Geral";
        // 
        // xrLabel15
        // 
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel15.StyleName = "FieldCaption";
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "14. Alinhamento Estratégico";
        // 
        // xrLabel17
        // 
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 11.7706F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel17.StyleName = "FieldCaption";
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.Text = "16. Matriz de Responsabilidades";
        // 
        // xrLabel18
        // 
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 25.00001F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel18.StyleName = "FieldCaption";
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.Text = "17. Plano de Recursos Humanos";
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
        this.DetailReport.DataMember = "Revisao";
        this.DetailReport.DataSource = this.dsPlanoProjeto;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 1;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel13,
            this.xrPanel12,
            this.xrPanel11,
            this.xrPanel10,
            this.xrPanel9,
            this.xrPanel8,
            this.xrPanel7,
            this.xrPanel6,
            this.xrPanel5,
            this.xrPanel4,
            this.xrPanel3,
            this.xrPanel2,
            this.xrPanel1});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 2799.958F;
        this.Detail1.Name = "Detail1";
        // 
        // xrPanel13
        // 
        this.xrPanel13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrTable5});
        this.xrPanel13.Dpi = 254F;
        this.xrPanel13.LocationFloat = new DevExpress.Utils.PointFloat(0.0006863276F, 410.7083F);
        this.xrPanel13.Name = "xrPanel13";
        this.xrPanel13.SizeF = new System.Drawing.SizeF(2769.998F, 150.5F);
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(2769.998F, 50F);
        this.xrLabel4.StyleName = "FieldCaption";
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "4. Patrocinador";
        // 
        // xrTable5
        // 
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow8});
        this.xrTable5.SizeF = new System.Drawing.SizeF(2769.998F, 100F);
        this.xrTable5.StyleName = "Table";
        this.xrTable5.StylePriority.UseFont = false;
        this.xrTable5.StylePriority.UsePadding = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseFont = false;
        this.xrTableCell21.Text = "Nome Completo";
        this.xrTableCell21.Weight = 1.89530655072484D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.Text = "Sigla da Unidade";
        this.xrTableCell22.Weight = 0.660605221965253D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.Text = "Matrícula";
        this.xrTableCell23.Weight = 0.444088227309905D;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.NomeUsuarioPatrocinador")});
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "xrTableCell12";
        this.xrTableCell24.Weight = 1.89530655072484D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.SiglaUnidadeNegocioPatrocinador")});
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = "xrTableCell13";
        this.xrTableCell25.Weight = 0.660605221965253D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.NumeroMatriculaPatrocinador")});
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Text = "xrTableCell14";
        this.xrTableCell26.Weight = 0.444088227309905D;
        // 
        // xrPanel12
        // 
        this.xrPanel12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrTable4});
        this.xrPanel12.Dpi = 254F;
        this.xrPanel12.LocationFloat = new DevExpress.Utils.PointFloat(0.0006863276F, 208.5625F);
        this.xrPanel12.Name = "xrPanel12";
        this.xrPanel12.SizeF = new System.Drawing.SizeF(2770F, 150.4374F);
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrLabel3.StyleName = "FieldCaption";
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "3. Responsável";
        // 
        // xrTable4
        // 
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5,
            this.xrTableRow6});
        this.xrTable4.SizeF = new System.Drawing.SizeF(2770F, 100F);
        this.xrTable4.StyleName = "Table";
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UsePadding = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseFont = false;
        this.xrTableCell15.Text = "Nome Completo";
        this.xrTableCell15.Weight = 1.8953061541065D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseFont = false;
        this.xrTableCell16.Text = "Sigla da Unidade";
        this.xrTableCell16.Weight = 0.660606147408055D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.Text = "Matrícula";
        this.xrTableCell17.Weight = 0.444087698485447D;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.NomeGerenteProjeto")});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "xrTableCell12";
        this.xrTableCell18.Weight = 1.8953070795493D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.SiglaUnidadeNegocioGP")});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "xrTableCell13";
        this.xrTableCell19.Weight = 0.660605221965253D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.NumeroMatriculaGP")});
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "xrTableCell14";
        this.xrTableCell20.Weight = 0.444087698485447D;
        // 
        // xrPanel11
        // 
        this.xrPanel11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrTable3});
        this.xrPanel11.Dpi = 254F;
        this.xrPanel11.LocationFloat = new DevExpress.Utils.PointFloat(0.0006661415F, 10.58333F);
        this.xrPanel11.Name = "xrPanel11";
        this.xrPanel11.SizeF = new System.Drawing.SizeF(2769.998F, 150.1458F);
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(2.11398E-05F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(2769.998F, 50F);
        this.xrLabel2.StyleName = "FieldCaption";
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "2. Informações iniciais";
        // 
        // xrTable3
        // 
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4});
        this.xrTable3.SizeF = new System.Drawing.SizeF(2769.998F, 100F);
        this.xrTable3.StyleName = "Table";
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UsePadding = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.Text = "Categoria";
        this.xrTableCell9.Weight = 1.89530681513707D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.Text = "Data Início Planejada";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell10.Weight = 0.660605221965253D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.Text = "Data Fim Planejada";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell11.Weight = 0.444087962897676D;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoCategoriaProjeto")});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 1.8953061541065D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DataInicioProjeto", "{0:d}")});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell13.Weight = 0.660606147408055D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DataTerminoProjeto", "{0:d}")});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell14.Weight = 0.444087698485447D;
        // 
        // xrPanel10
        // 
        this.xrPanel10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel23});
        this.xrPanel10.Dpi = 254F;
        this.xrPanel10.LocationFloat = new DevExpress.Utils.PointFloat(0.0006863276F, 618.553F);
        this.xrPanel10.Name = "xrPanel10";
        this.xrPanel10.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrPanel9
        // 
        this.xrPanel9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel24});
        this.xrPanel9.Dpi = 254F;
        this.xrPanel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 843.213F);
        this.xrPanel9.Name = "xrPanel9";
        this.xrPanel9.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrPanel8
        // 
        this.xrPanel8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel25,
            this.xrLabel7});
        this.xrPanel8.Dpi = 254F;
        this.xrPanel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1058.873F);
        this.xrPanel8.Name = "xrPanel8";
        this.xrPanel8.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrLabel25
        // 
        this.xrLabel25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoJustificativa")});
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrLabel25.Multiline = true;
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(2769.999F, 127.66F);
        this.xrLabel25.StyleName = "DataField";
        this.xrLabel25.StylePriority.UseBorders = false;
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.Text = "6. Justificativa";
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrLabel7.StyleName = "FieldCaption";
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "6. Justificativa";
        // 
        // xrPanel7
        // 
        this.xrPanel7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel27,
            this.xrLabel9});
        this.xrPanel7.Dpi = 254F;
        this.xrPanel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1501.195F);
        this.xrPanel7.Name = "xrPanel7";
        this.xrPanel7.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrLabel27
        // 
        this.xrLabel27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoEscopo")});
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(0.0003210704F, 50F);
        this.xrLabel27.Multiline = true;
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(2770F, 127.66F);
        this.xrLabel27.StyleName = "DataField";
        this.xrLabel27.StylePriority.UseBorders = false;
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.Text = "8. Escopo";
        // 
        // xrLabel9
        // 
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel9.StyleName = "FieldCaption";
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = "8. Escopo";
        // 
        // xrPanel6
        // 
        this.xrPanel6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel28});
        this.xrPanel6.Dpi = 254F;
        this.xrPanel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1726.855F);
        this.xrPanel6.Name = "xrPanel6";
        this.xrPanel6.SizeF = new System.Drawing.SizeF(2770F, 172.66F);
        // 
        // xrLabel10
        // 
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0.0002441406F, 0F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrLabel10.StyleName = "FieldCaption";
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.Text = "9. Não Escopo";
        // 
        // xrLabel28
        // 
        this.xrLabel28.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoNaoEscopo")});
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0.0002441406F, 50F);
        this.xrLabel28.Multiline = true;
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(2770F, 122.1F);
        this.xrLabel28.StyleName = "DataField";
        this.xrLabel28.StylePriority.UseBorders = false;
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.Text = "9. Não Escopo";
        // 
        // xrPanel5
        // 
        this.xrPanel5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel29,
            this.xrLabel11});
        this.xrPanel5.Dpi = 254F;
        this.xrPanel5.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 1950.515F);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrLabel29
        // 
        this.xrLabel29.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoPremissas")});
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 50F);
        this.xrLabel29.Multiline = true;
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(2770F, 127.6599F);
        this.xrLabel29.StyleName = "DataField";
        this.xrLabel29.StylePriority.UseBorders = false;
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.Text = "10. Premissas";
        // 
        // xrLabel11
        // 
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 0F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(2769.998F, 50F);
        this.xrLabel11.StyleName = "FieldCaption";
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "10. Premissas";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel30,
            this.xrLabel12});
        this.xrPanel4.Dpi = 254F;
        this.xrPanel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2181.175F);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrLabel30
        // 
        this.xrLabel30.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoRestricoes")});
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrLabel30.Multiline = true;
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(2770F, 127.6599F);
        this.xrLabel30.StyleName = "DataField";
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.Text = "11. Restrições";
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 0F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrLabel12.StyleName = "FieldCaption";
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.Text = "11. Restrições";
        // 
        // xrPanel3
        // 
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel31});
        this.xrPanel3.Dpi = 254F;
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(0.0005450249F, 2405.835F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(2769.998F, 50F);
        this.xrLabel13.StyleName = "FieldCaption";
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.Text = "12. Benefícios Esperados";
        // 
        // xrLabel31
        // 
        this.xrLabel31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.DescricaoBeneficiosEsperados")});
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
        this.xrLabel31.Multiline = true;
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(2769.998F, 127.6599F);
        this.xrLabel31.StyleName = "DataField";
        this.xrLabel31.StylePriority.UseBorders = false;
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.Text = "12. Benefícios Esperados";
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel32});
        this.xrPanel2.Dpi = 254F;
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 2621.938F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(2770F, 177.66F);
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel26});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0.0005450249F, 1279.533F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(2769.998F, 177.6617F);
        // 
        // DetailReport11
        // 
        this.DetailReport11.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail12,
            this.GroupHeader2,
            this.GroupHeader13});
        this.DetailReport11.DataMember = "Revisao.Revisao_AlinhamentoEstrategico";
        this.DetailReport11.DataSource = this.dsPlanoProjeto;
        this.DetailReport11.Dpi = 254F;
        this.DetailReport11.Level = 2;
        this.DetailReport11.Name = "DetailReport11";
        // 
        // Detail12
        // 
        this.Detail12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.Detail12.Dpi = 254F;
        this.Detail12.HeightF = 50F;
        this.Detail12.KeepTogether = true;
        this.Detail12.Name = "Detail12";
        // 
        // xrTable7
        // 
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0.0003027916F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable7.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrTable7.StyleName = "Table";
        this.xrTable7.StylePriority.UseFont = false;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell28});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 0.567901234567901D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_AlinhamentoEstrategico.DescricaoObjetoMapaInstituicao")});
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell27.StylePriority.UseBorders = false;
        this.xrTableCell27.StylePriority.UsePadding = false;
        this.xrTableCell27.Text = "Alinhamento Estratégico CAIXA";
        this.xrTableCell27.Weight = 0.611557020060201D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_AlinhamentoEstrategico.DescricaoObjetivoTI")});
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UsePadding = false;
        this.xrTableCell28.Text = "Alinhamento Estratégico Ti";
        this.xrTableCell28.Weight = 0.611556704664184D;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader2.HeightF = 74.99993F;
        this.GroupHeader2.KeepTogether = true;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // GroupHeader13
        // 
        this.GroupHeader13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.GroupHeader13.Dpi = 254F;
        this.GroupHeader13.HeightF = 50F;
        this.GroupHeader13.KeepTogether = true;
        this.GroupHeader13.Name = "GroupHeader13";
        this.GroupHeader13.RepeatEveryPage = true;
        // 
        // xrTable6
        // 
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable6.SizeF = new System.Drawing.SizeF(2769.999F, 50F);
        this.xrTable6.StyleName = "Table";
        this.xrTable6.StylePriority.UseFont = false;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell30});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 0.567901234567901D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell29.StylePriority.UseFont = false;
        this.xrTableCell29.StylePriority.UsePadding = false;
        this.xrTableCell29.Text = "Alinhamento Estratégico CAIXA";
        this.xrTableCell29.Weight = 0.611557020060201D;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.StylePriority.UsePadding = false;
        this.xrTableCell30.Text = "Alinhamento Estratégico TI";
        this.xrTableCell30.Weight = 0.611556704664184D;
        // 
        // DetailReport3
        // 
        this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupHeader3,
            this.GroupHeader12});
        this.DetailReport3.DataMember = "Revisao.Revisao_Entregas";
        this.DetailReport3.DataSource = this.dsPlanoProjeto;
        this.DetailReport3.Dpi = 254F;
        this.DetailReport3.Level = 3;
        this.DetailReport3.Name = "DetailReport3";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
        this.Detail4.Dpi = 254F;
        this.Detail4.HeightF = 50F;
        this.Detail4.Name = "Detail4";
        // 
        // xrTable9
        // 
        this.xrTable9.Dpi = 254F;
        this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
        this.xrTable9.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable9.StyleName = "Table";
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1D;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.SequenciaEntrega")});
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 11, 0, 0, 254F);
        this.xrTableCell37.StylePriority.UseBorders = false;
        this.xrTableCell37.StylePriority.UseFont = false;
        this.xrTableCell37.StylePriority.UsePadding = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        this.xrTableCell37.Text = "Número";
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell37.Weight = 0.270758111726506D;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.IdentificacaoEntrega")});
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.StylePriority.UseFont = false;
        this.xrTableCell38.StylePriority.UsePadding = false;
        this.xrTableCell38.Text = "Nome da Entrega";
        this.xrTableCell38.Weight = 2.16606503703534D;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.DescricaoUnidadesRelacionadas")});
        this.xrTableCell39.Dpi = 254F;
        this.xrTableCell39.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell39.StylePriority.UseBorders = false;
        this.xrTableCell39.StylePriority.UseFont = false;
        this.xrTableCell39.StylePriority.UsePadding = false;
        this.xrTableCell39.Text = "Unidades Relacionadas";
        this.xrTableCell39.Weight = 1.13718413755782D;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.PercentualRepresentacao", "{0} %")});
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 150, 0, 0, 254F);
        this.xrTableCell40.StylePriority.UseBorders = false;
        this.xrTableCell40.StylePriority.UseFont = false;
        this.xrTableCell40.StylePriority.UsePadding = false;
        this.xrTableCell40.StylePriority.UseTextAlignment = false;
        this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell40.Weight = 0.67D;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.DataInicio", "{0:d}")});
        this.xrTableCell41.Dpi = 254F;
        this.xrTableCell41.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.StylePriority.UseFont = false;
        this.xrTableCell41.StylePriority.UsePadding = false;
        this.xrTableCell41.Text = "Data início planejada";
        this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell41.Weight = 0.671480133387156D;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.DataTermino", "{0:d}")});
        this.xrTableCell42.Dpi = 254F;
        this.xrTableCell42.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell42.StylePriority.UseBorders = false;
        this.xrTableCell42.StylePriority.UseFont = false;
        this.xrTableCell42.StylePriority.UsePadding = false;
        this.xrTableCell42.Text = "Data fim planejada";
        this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell42.Weight = 0.671480067284099D;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel33,
            this.xrLabel16});
        this.GroupHeader3.Dpi = 254F;
        this.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader3.HeightF = 124.9998F;
        this.GroupHeader3.Level = 1;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // xrLabel33
        // 
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(0F, 74.99979F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel33.StyleName = "FieldCaption";
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.Text = "15.1 Entregas Planejadas – visão executiva";
        // 
        // xrLabel16
        // 
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99993F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel16.StyleName = "FieldCaption";
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.Text = "15. EAP e Dicionário EAP";
        // 
        // GroupHeader12
        // 
        this.GroupHeader12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.GroupHeader12.Dpi = 254F;
        this.GroupHeader12.HeightF = 50F;
        this.GroupHeader12.Name = "GroupHeader12";
        this.GroupHeader12.RepeatEveryPage = true;
        // 
        // xrTable8
        // 
        this.xrTable8.Dpi = 254F;
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
        this.xrTable8.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable8.StyleName = "Table";
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1D;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell31.StylePriority.UseFont = false;
        this.xrTableCell31.StylePriority.UsePadding = false;
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "Número";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell31.Weight = 0.270758111726506D;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell32.StylePriority.UseBorders = false;
        this.xrTableCell32.StylePriority.UseFont = false;
        this.xrTableCell32.StylePriority.UsePadding = false;
        this.xrTableCell32.Text = "Nome da Entrega";
        this.xrTableCell32.Weight = 2.16606503703534D;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell33.StylePriority.UseBorders = false;
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.StylePriority.UsePadding = false;
        this.xrTableCell33.Text = "Unidades Relacionadas";
        this.xrTableCell33.Weight = 1.13718413755782D;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseFont = false;
        this.xrTableCell34.StylePriority.UsePadding = false;
        this.xrTableCell34.Text = "% de representatividade p/ o projeto";
        this.xrTableCell34.Weight = 0.67D;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell35.Dpi = 254F;
        this.xrTableCell35.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell35.StylePriority.UseBorders = false;
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.StylePriority.UsePadding = false;
        this.xrTableCell35.StylePriority.UseTextAlignment = false;
        this.xrTableCell35.Text = "Data início planejada";
        this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell35.Weight = 0.671480133387156D;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell36.StylePriority.UseBorders = false;
        this.xrTableCell36.StylePriority.UseFont = false;
        this.xrTableCell36.StylePriority.UsePadding = false;
        this.xrTableCell36.StylePriority.UseTextAlignment = false;
        this.xrTableCell36.Text = "Data fim planejada";
        this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell36.Weight = 0.671480067284099D;
        // 
        // DetailReport4
        // 
        this.DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.GroupHeader5,
            this.GroupHeader19});
        this.DetailReport4.DataMember = "Revisao.Revisao_MatrizResponsabilidadeIntegrantes";
        this.DetailReport4.DataSource = this.dsPlanoProjeto;
        this.DetailReport4.Dpi = 254F;
        this.DetailReport4.Level = 6;
        this.DetailReport4.Name = "DetailReport4";
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
        this.Detail5.Dpi = 254F;
        this.Detail5.HeightF = 50F;
        this.Detail5.Name = "Detail5";
        this.Detail5.StyleName = "Table";
        // 
        // xrTable13
        // 
        this.xrTable13.Dpi = 254F;
        this.xrTable13.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 0F);
        this.xrTable13.Name = "xrTable13";
        this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
        this.xrTable13.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable13.StyleName = "Table";
        this.xrTable13.StylePriority.UseFont = false;
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67,
            this.xrTableCell68});
        this.xrTableRow16.Dpi = 254F;
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 0.567901234567901D;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.IdentificacaoEntregavel")});
        this.xrTableCell49.Dpi = 254F;
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell49.StylePriority.UseBorders = false;
        this.xrTableCell49.StylePriority.UsePadding = false;
        this.xrTableCell49.Text = "Entregável / Grupo de Trabalho";
        this.xrTableCell49.Weight = 0.220307810337871D;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo1")});
        this.xrTableCell50.Dpi = 254F;
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell50.StylePriority.UseBorders = false;
        this.xrTableCell50.StylePriority.UsePadding = false;
        this.xrTableCell50.Text = "1";
        this.xrTableCell50.Weight = 0.118270503806233D;
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo2")});
        this.xrTableCell51.Dpi = 254F;
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell51.StylePriority.UseBorders = false;
        this.xrTableCell51.StylePriority.UsePadding = false;
        this.xrTableCell51.Text = "2";
        this.xrTableCell51.Weight = 0.118270503806233D;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo3")});
        this.xrTableCell52.Dpi = 254F;
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell52.StylePriority.UseBorders = false;
        this.xrTableCell52.StylePriority.UsePadding = false;
        this.xrTableCell52.Text = "3";
        this.xrTableCell52.Weight = 0.118270503806233D;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo4")});
        this.xrTableCell63.Dpi = 254F;
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell63.StylePriority.UseBorders = false;
        this.xrTableCell63.StylePriority.UsePadding = false;
        this.xrTableCell63.Text = "4";
        this.xrTableCell63.Weight = 0.118270503806233D;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo5")});
        this.xrTableCell64.Dpi = 254F;
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.StylePriority.UsePadding = false;
        this.xrTableCell64.Text = "5";
        this.xrTableCell64.Weight = 0.118270517960467D;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo6")});
        this.xrTableCell65.Dpi = 254F;
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell65.StylePriority.UseBorders = false;
        this.xrTableCell65.StylePriority.UsePadding = false;
        this.xrTableCell65.Text = "6";
        this.xrTableCell65.Weight = 0.118270517960467D;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo7")});
        this.xrTableCell66.Dpi = 254F;
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell66.StylePriority.UseBorders = false;
        this.xrTableCell66.StylePriority.UsePadding = false;
        this.xrTableCell66.Text = "7";
        this.xrTableCell66.Weight = 0.118270517960467D;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo8")});
        this.xrTableCell67.Dpi = 254F;
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.StylePriority.UsePadding = false;
        this.xrTableCell67.Text = "8";
        this.xrTableCell67.Weight = 0.118270517960467D;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_MatrizResponsabilidadeIntegrantes.DescricaoIntegrantesGrupo9")});
        this.xrTableCell68.Dpi = 254F;
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell68.StylePriority.UseBorders = false;
        this.xrTableCell68.StylePriority.UsePadding = false;
        this.xrTableCell68.Text = "9";
        this.xrTableCell68.Weight = 0.11827087181631D;
        // 
        // GroupHeader5
        // 
        this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
        this.GroupHeader5.Dpi = 254F;
        this.GroupHeader5.HeightF = 100F;
        this.GroupHeader5.Name = "GroupHeader5";
        this.GroupHeader5.RepeatEveryPage = true;
        // 
        // xrTable12
        // 
        this.xrTable12.Dpi = 254F;
        this.xrTable12.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable12.Name = "xrTable12";
        this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
        this.xrTable12.SizeF = new System.Drawing.SizeF(2770F, 100F);
        this.xrTable12.StyleName = "Table";
        this.xrTable12.StylePriority.UseFont = false;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell53,
            this.cellDescricaoIntegrantesGrupo1,
            this.cellDescricaoIntegrantesGrupo2,
            this.cellDescricaoIntegrantesGrupo3,
            this.cellDescricaoIntegrantesGrupo4,
            this.cellDescricaoIntegrantesGrupo5,
            this.cellDescricaoIntegrantesGrupo6,
            this.cellDescricaoIntegrantesGrupo7,
            this.cellDescricaoIntegrantesGrupo8,
            this.cellDescricaoIntegrantesGrupo9});
        this.xrTableRow15.Dpi = 254F;
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 0.567901234567901D;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.Dpi = 254F;
        this.xrTableCell53.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell53.StylePriority.UsePadding = false;
        this.xrTableCell53.Text = "Entregável / Grupo de Trabalho";
        this.xrTableCell53.Weight = 0.220307810337871D;
        // 
        // cellDescricaoIntegrantesGrupo1
        // 
        this.cellDescricaoIntegrantesGrupo1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo1.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo1.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo1.Name = "cellDescricaoIntegrantesGrupo1";
        this.cellDescricaoIntegrantesGrupo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo1.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo1.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo1.Text = "1";
        this.cellDescricaoIntegrantesGrupo1.Weight = 0.118270503806233D;
        // 
        // cellDescricaoIntegrantesGrupo2
        // 
        this.cellDescricaoIntegrantesGrupo2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo2.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo2.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo2.Name = "cellDescricaoIntegrantesGrupo2";
        this.cellDescricaoIntegrantesGrupo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo2.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo2.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo2.Text = "2";
        this.cellDescricaoIntegrantesGrupo2.Weight = 0.118270503806233D;
        // 
        // cellDescricaoIntegrantesGrupo3
        // 
        this.cellDescricaoIntegrantesGrupo3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo3.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo3.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo3.Name = "cellDescricaoIntegrantesGrupo3";
        this.cellDescricaoIntegrantesGrupo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo3.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo3.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo3.Text = "3";
        this.cellDescricaoIntegrantesGrupo3.Weight = 0.118270503806233D;
        // 
        // cellDescricaoIntegrantesGrupo4
        // 
        this.cellDescricaoIntegrantesGrupo4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo4.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo4.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo4.Name = "cellDescricaoIntegrantesGrupo4";
        this.cellDescricaoIntegrantesGrupo4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo4.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo4.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo4.Text = "4";
        this.cellDescricaoIntegrantesGrupo4.Weight = 0.118270503806233D;
        // 
        // cellDescricaoIntegrantesGrupo5
        // 
        this.cellDescricaoIntegrantesGrupo5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo5.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo5.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo5.Name = "cellDescricaoIntegrantesGrupo5";
        this.cellDescricaoIntegrantesGrupo5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo5.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo5.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo5.Text = "5";
        this.cellDescricaoIntegrantesGrupo5.Weight = 0.118270517960467D;
        // 
        // cellDescricaoIntegrantesGrupo6
        // 
        this.cellDescricaoIntegrantesGrupo6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo6.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo6.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo6.Name = "cellDescricaoIntegrantesGrupo6";
        this.cellDescricaoIntegrantesGrupo6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo6.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo6.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo6.Text = "6";
        this.cellDescricaoIntegrantesGrupo6.Weight = 0.118270517960467D;
        // 
        // cellDescricaoIntegrantesGrupo7
        // 
        this.cellDescricaoIntegrantesGrupo7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo7.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo7.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo7.Name = "cellDescricaoIntegrantesGrupo7";
        this.cellDescricaoIntegrantesGrupo7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo7.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo7.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo7.Text = "7";
        this.cellDescricaoIntegrantesGrupo7.Weight = 0.118270517960467D;
        // 
        // cellDescricaoIntegrantesGrupo8
        // 
        this.cellDescricaoIntegrantesGrupo8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo8.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo8.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo8.Name = "cellDescricaoIntegrantesGrupo8";
        this.cellDescricaoIntegrantesGrupo8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo8.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo8.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo8.Text = "8";
        this.cellDescricaoIntegrantesGrupo8.Weight = 0.118270517960467D;
        // 
        // cellDescricaoIntegrantesGrupo9
        // 
        this.cellDescricaoIntegrantesGrupo9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.cellDescricaoIntegrantesGrupo9.Dpi = 254F;
        this.cellDescricaoIntegrantesGrupo9.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
        this.cellDescricaoIntegrantesGrupo9.Name = "cellDescricaoIntegrantesGrupo9";
        this.cellDescricaoIntegrantesGrupo9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.cellDescricaoIntegrantesGrupo9.StylePriority.UseBorders = false;
        this.cellDescricaoIntegrantesGrupo9.StylePriority.UsePadding = false;
        this.cellDescricaoIntegrantesGrupo9.Text = "9";
        this.cellDescricaoIntegrantesGrupo9.Weight = 0.11827087181631D;
        // 
        // GroupHeader19
        // 
        this.GroupHeader19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17});
        this.GroupHeader19.Dpi = 254F;
        this.GroupHeader19.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader19.HeightF = 61.7706F;
        this.GroupHeader19.Level = 1;
        this.GroupHeader19.Name = "GroupHeader19";
        // 
        // DetailReport5
        // 
        this.DetailReport5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.GroupHeader6,
            this.GroupHeader14});
        this.DetailReport5.DataMember = "Revisao.Revisao_EquipeProjeto";
        this.DetailReport5.DataSource = this.dsPlanoProjeto;
        this.DetailReport5.Dpi = 254F;
        this.DetailReport5.Level = 7;
        this.DetailReport5.Name = "DetailReport5";
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable15});
        this.Detail6.Dpi = 254F;
        this.Detail6.HeightF = 50F;
        this.Detail6.Name = "Detail6";
        // 
        // xrTable15
        // 
        this.xrTable15.Dpi = 254F;
        this.xrTable15.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 0F);
        this.xrTable15.Name = "xrTable15";
        this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18});
        this.xrTable15.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable15.StyleName = "Table";
        this.xrTable15.StylePriority.UseFont = false;
        // 
        // xrTableRow18
        // 
        this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell60,
            this.xrTableCell61});
        this.xrTableRow18.Dpi = 254F;
        this.xrTableRow18.Name = "xrTableRow18";
        this.xrTableRow18.Weight = 1D;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EquipeProjeto.NomeIntegrante")});
        this.xrTableCell58.Dpi = 254F;
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell58.StylePriority.UseBorders = false;
        this.xrTableCell58.StylePriority.UsePadding = false;
        this.xrTableCell58.Text = "Nome";
        this.xrTableCell58.Weight = 0.758122754699487D;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EquipeProjeto.IdentificacaoFuncaoIntegrante")});
        this.xrTableCell59.Dpi = 254F;
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell59.StylePriority.UseBorders = false;
        this.xrTableCell59.StylePriority.UsePadding = false;
        this.xrTableCell59.Text = "Função";
        this.xrTableCell59.Weight = 0.758122754699487D;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EquipeProjeto.EmailIntegrante")});
        this.xrTableCell60.Dpi = 254F;
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell60.StylePriority.UseBorders = false;
        this.xrTableCell60.StylePriority.UsePadding = false;
        this.xrTableCell60.Text = "E-mail";
        this.xrTableCell60.Weight = 0.758122732665134D;
        // 
        // xrTableCell61
        // 
        this.xrTableCell61.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EquipeProjeto.NumeroTelefoneIntegrante")});
        this.xrTableCell61.Dpi = 254F;
        this.xrTableCell61.Name = "xrTableCell61";
        this.xrTableCell61.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell61.StylePriority.UseBorders = false;
        this.xrTableCell61.StylePriority.UsePadding = false;
        this.xrTableCell61.Text = "Telefone";
        this.xrTableCell61.Weight = 0.725631757935892D;
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14});
        this.GroupHeader6.Dpi = 254F;
        this.GroupHeader6.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader6.HeightF = 50F;
        this.GroupHeader6.Name = "GroupHeader6";
        this.GroupHeader6.RepeatEveryPage = true;
        // 
        // xrTable14
        // 
        this.xrTable14.Dpi = 254F;
        this.xrTable14.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable14.Name = "xrTable14";
        this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
        this.xrTable14.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable14.StyleName = "Table";
        this.xrTable14.StylePriority.UseFont = false;
        // 
        // xrTableRow17
        // 
        this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell57,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56});
        this.xrTableRow17.Dpi = 254F;
        this.xrTableRow17.Name = "xrTableRow17";
        this.xrTableRow17.Weight = 1D;
        // 
        // xrTableCell57
        // 
        this.xrTableCell57.Dpi = 254F;
        this.xrTableCell57.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell57.Name = "xrTableCell57";
        this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell57.StylePriority.UseFont = false;
        this.xrTableCell57.StylePriority.UsePadding = false;
        this.xrTableCell57.Text = "Nome";
        this.xrTableCell57.Weight = 0.758122754699487D;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell54.Dpi = 254F;
        this.xrTableCell54.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell54.StylePriority.UseBorders = false;
        this.xrTableCell54.StylePriority.UseFont = false;
        this.xrTableCell54.StylePriority.UsePadding = false;
        this.xrTableCell54.Text = "Função";
        this.xrTableCell54.Weight = 0.758122754699487D;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell55.Dpi = 254F;
        this.xrTableCell55.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell55.StylePriority.UseBorders = false;
        this.xrTableCell55.StylePriority.UseFont = false;
        this.xrTableCell55.StylePriority.UsePadding = false;
        this.xrTableCell55.Text = "E-mail";
        this.xrTableCell55.Weight = 0.758122732665134D;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell56.Dpi = 254F;
        this.xrTableCell56.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell56.StylePriority.UseBorders = false;
        this.xrTableCell56.StylePriority.UseFont = false;
        this.xrTableCell56.StylePriority.UsePadding = false;
        this.xrTableCell56.Text = "Telefone";
        this.xrTableCell56.Weight = 0.725631757935892D;
        // 
        // GroupHeader14
        // 
        this.GroupHeader14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrLabel35});
        this.GroupHeader14.Dpi = 254F;
        this.GroupHeader14.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader14.HeightF = 125F;
        this.GroupHeader14.Level = 1;
        this.GroupHeader14.Name = "GroupHeader14";
        // 
        // xrLabel35
        // 
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 75.00001F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel35.StyleName = "FieldCaption";
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.Text = "Equipe do Projeto";
        // 
        // DetailReport6
        // 
        this.DetailReport6.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.GroupHeader8,
            this.GroupHeader16});
        this.DetailReport6.DataMember = "Revisao.Revisao_Riscos";
        this.DetailReport6.DataSource = this.dsPlanoProjeto;
        this.DetailReport6.Dpi = 254F;
        this.DetailReport6.Level = 9;
        this.DetailReport6.Name = "DetailReport6";
        // 
        // Detail7
        // 
        this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable19});
        this.Detail7.Dpi = 254F;
        this.Detail7.HeightF = 50F;
        this.Detail7.Name = "Detail7";
        // 
        // xrTable19
        // 
        this.xrTable19.Dpi = 254F;
        this.xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable19.Name = "xrTable19";
        this.xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow22});
        this.xrTable19.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable19.StyleName = "Table";
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell88,
            this.xrTableCell94,
            this.xrTableCell95,
            this.xrTableCell96,
            this.xrTableCell97,
            this.xrTableCell98,
            this.xrTableCell99,
            this.xrTableCell100,
            this.xrTableCell101,
            this.xrTableCell102,
            this.xrTableCell103});
        this.xrTableRow22.Dpi = 254F;
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.Weight = 1D;
        // 
        // xrTableCell88
        // 
        this.xrTableCell88.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell88.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.SequenciaRisco")});
        this.xrTableCell88.Dpi = 254F;
        this.xrTableCell88.Name = "xrTableCell88";
        this.xrTableCell88.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 254F);
        this.xrTableCell88.StylePriority.UseBorders = false;
        this.xrTableCell88.StylePriority.UsePadding = false;
        this.xrTableCell88.StylePriority.UseTextAlignment = false;
        this.xrTableCell88.Text = "Número";
        this.xrTableCell88.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell88.Weight = 0.147089106231504D;
        // 
        // xrTableCell94
        // 
        this.xrTableCell94.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.IdentificacaoRisco")});
        this.xrTableCell94.Dpi = 254F;
        this.xrTableCell94.Name = "xrTableCell94";
        this.xrTableCell94.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell94.StylePriority.UseBorders = false;
        this.xrTableCell94.StylePriority.UsePadding = false;
        this.xrTableCell94.Text = "Risco Identificado";
        this.xrTableCell94.Weight = 0.342817381080131D;
        // 
        // xrTableCell95
        // 
        this.xrTableCell95.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.GrauProbabilidadeRisco")});
        this.xrTableCell95.Dpi = 254F;
        this.xrTableCell95.Name = "xrTableCell95";
        this.xrTableCell95.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell95.StylePriority.UseBorders = false;
        this.xrTableCell95.StylePriority.UsePadding = false;
        this.xrTableCell95.Text = "Probabilidade";
        this.xrTableCell95.Weight = 0.237328857333512D;
        // 
        // xrTableCell96
        // 
        this.xrTableCell96.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell96.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.GrauImpactoRisco")});
        this.xrTableCell96.Dpi = 254F;
        this.xrTableCell96.Name = "xrTableCell96";
        this.xrTableCell96.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell96.StylePriority.UseBorders = false;
        this.xrTableCell96.StylePriority.UsePadding = false;
        this.xrTableCell96.Text = "Impacto";
        this.xrTableCell96.Weight = 0.18767298502571D;
        // 
        // xrTableCell97
        // 
        this.xrTableCell97.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell97.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.GrauCriticidadeRisco")});
        this.xrTableCell97.Dpi = 254F;
        this.xrTableCell97.Name = "xrTableCell97";
        this.xrTableCell97.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell97.StylePriority.UseBorders = false;
        this.xrTableCell97.StylePriority.UsePadding = false;
        this.xrTableCell97.Text = "Criticidade";
        this.xrTableCell97.Weight = 0.187673500486376D;
        // 
        // xrTableCell98
        // 
        this.xrTableCell98.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell98.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.IdentificacaoCategoriaRisco")});
        this.xrTableCell98.Dpi = 254F;
        this.xrTableCell98.Name = "xrTableCell98";
        this.xrTableCell98.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell98.StylePriority.UseBorders = false;
        this.xrTableCell98.StylePriority.UsePadding = false;
        this.xrTableCell98.Text = "Classificação";
        this.xrTableCell98.Weight = 0.232363711107968D;
        // 
        // xrTableCell99
        // 
        this.xrTableCell99.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.IdentificacaoTipoResposta")});
        this.xrTableCell99.Dpi = 254F;
        this.xrTableCell99.Name = "xrTableCell99";
        this.xrTableCell99.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell99.StylePriority.UseBorders = false;
        this.xrTableCell99.StylePriority.UsePadding = false;
        this.xrTableCell99.Text = "Estratégia";
        this.xrTableCell99.Weight = 0.180225322960802D;
        // 
        // xrTableCell100
        // 
        this.xrTableCell100.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell100.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.DescricaoAcaoProposta")});
        this.xrTableCell100.Dpi = 254F;
        this.xrTableCell100.Name = "xrTableCell100";
        this.xrTableCell100.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell100.StylePriority.UseBorders = false;
        this.xrTableCell100.StylePriority.UsePadding = false;
        this.xrTableCell100.Text = "Ação Proposta";
        this.xrTableCell100.Weight = 0.455401379080398D;
        // 
        // xrTableCell101
        // 
        this.xrTableCell101.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell101.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.NomeUsuarioResponsavel")});
        this.xrTableCell101.Dpi = 254F;
        this.xrTableCell101.Name = "xrTableCell101";
        this.xrTableCell101.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell101.StylePriority.UseBorders = false;
        this.xrTableCell101.StylePriority.UsePadding = false;
        this.xrTableCell101.Text = "Responsável";
        this.xrTableCell101.Weight = 0.291457727128528D;
        // 
        // xrTableCell102
        // 
        this.xrTableCell102.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell102.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.DataLimiteTratamento")});
        this.xrTableCell102.Dpi = 254F;
        this.xrTableCell102.Name = "xrTableCell102";
        this.xrTableCell102.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell102.StylePriority.UseBorders = false;
        this.xrTableCell102.StylePriority.UsePadding = false;
        this.xrTableCell102.Text = "Data de Conclusão";
        this.xrTableCell102.Weight = 0.196489798179388D;
        // 
        // xrTableCell103
        // 
        this.xrTableCell103.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Riscos.IdentificacaoStatusRisco")});
        this.xrTableCell103.Dpi = 254F;
        this.xrTableCell103.Name = "xrTableCell103";
        this.xrTableCell103.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell103.StylePriority.UseBorders = false;
        this.xrTableCell103.StylePriority.UsePadding = false;
        this.xrTableCell103.Text = "Status do Risco";
        this.xrTableCell103.Weight = 0.140758220742207D;
        // 
        // GroupHeader8
        // 
        this.GroupHeader8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable18});
        this.GroupHeader8.Dpi = 254F;
        this.GroupHeader8.HeightF = 100F;
        this.GroupHeader8.Name = "GroupHeader8";
        this.GroupHeader8.RepeatEveryPage = true;
        // 
        // xrTable18
        // 
        this.xrTable18.Dpi = 254F;
        this.xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 0F);
        this.xrTable18.Name = "xrTable18";
        this.xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow21});
        this.xrTable18.SizeF = new System.Drawing.SizeF(2770F, 100F);
        this.xrTable18.StyleName = "Table";
        // 
        // xrTableRow21
        // 
        this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell89,
            this.xrTableCell82,
            this.xrTableCell90,
            this.xrTableCell83,
            this.xrTableCell91,
            this.xrTableCell84,
            this.xrTableCell92,
            this.xrTableCell85,
            this.xrTableCell93,
            this.xrTableCell86,
            this.xrTableCell87});
        this.xrTableRow21.Dpi = 254F;
        this.xrTableRow21.Name = "xrTableRow21";
        this.xrTableRow21.Weight = 1D;
        // 
        // xrTableCell89
        // 
        this.xrTableCell89.Dpi = 254F;
        this.xrTableCell89.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell89.Name = "xrTableCell89";
        this.xrTableCell89.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell89.StylePriority.UseFont = false;
        this.xrTableCell89.StylePriority.UsePadding = false;
        this.xrTableCell89.StylePriority.UseTextAlignment = false;
        this.xrTableCell89.Text = "Número";
        this.xrTableCell89.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell89.Weight = 0.147089106231504D;
        // 
        // xrTableCell82
        // 
        this.xrTableCell82.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell82.Dpi = 254F;
        this.xrTableCell82.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell82.Name = "xrTableCell82";
        this.xrTableCell82.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell82.StylePriority.UseBorders = false;
        this.xrTableCell82.StylePriority.UseFont = false;
        this.xrTableCell82.StylePriority.UsePadding = false;
        this.xrTableCell82.Text = "Risco Identificado";
        this.xrTableCell82.Weight = 0.342816922892872D;
        // 
        // xrTableCell90
        // 
        this.xrTableCell90.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell90.Dpi = 254F;
        this.xrTableCell90.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell90.Name = "xrTableCell90";
        this.xrTableCell90.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell90.StylePriority.UseBorders = false;
        this.xrTableCell90.StylePriority.UseFont = false;
        this.xrTableCell90.StylePriority.UsePadding = false;
        this.xrTableCell90.Text = "Probabilidade";
        this.xrTableCell90.Weight = 0.237328800060105D;
        // 
        // xrTableCell83
        // 
        this.xrTableCell83.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell83.Dpi = 254F;
        this.xrTableCell83.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell83.Name = "xrTableCell83";
        this.xrTableCell83.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell83.StylePriority.UseBorders = false;
        this.xrTableCell83.StylePriority.UseFont = false;
        this.xrTableCell83.StylePriority.UsePadding = false;
        this.xrTableCell83.Text = "Impacto";
        this.xrTableCell83.Weight = 0.187673500486376D;
        // 
        // xrTableCell91
        // 
        this.xrTableCell91.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell91.Dpi = 254F;
        this.xrTableCell91.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell91.Name = "xrTableCell91";
        this.xrTableCell91.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell91.StylePriority.UseBorders = false;
        this.xrTableCell91.StylePriority.UseFont = false;
        this.xrTableCell91.StylePriority.UsePadding = false;
        this.xrTableCell91.Text = "Criticidade";
        this.xrTableCell91.Weight = 0.187673500486376D;
        // 
        // xrTableCell84
        // 
        this.xrTableCell84.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell84.Dpi = 254F;
        this.xrTableCell84.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell84.Name = "xrTableCell84";
        this.xrTableCell84.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell84.StylePriority.UseBorders = false;
        this.xrTableCell84.StylePriority.UseFont = false;
        this.xrTableCell84.StylePriority.UsePadding = false;
        this.xrTableCell84.Text = "Classificação";
        this.xrTableCell84.Weight = 0.232363138373895D;
        // 
        // xrTableCell92
        // 
        this.xrTableCell92.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell92.Dpi = 254F;
        this.xrTableCell92.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell92.Name = "xrTableCell92";
        this.xrTableCell92.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell92.StylePriority.UseBorders = false;
        this.xrTableCell92.StylePriority.UseFont = false;
        this.xrTableCell92.StylePriority.UsePadding = false;
        this.xrTableCell92.Text = "Estratégia";
        this.xrTableCell92.Weight = 0.180225322960802D;
        // 
        // xrTableCell85
        // 
        this.xrTableCell85.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell85.Dpi = 254F;
        this.xrTableCell85.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell85.Name = "xrTableCell85";
        this.xrTableCell85.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell85.StylePriority.UseBorders = false;
        this.xrTableCell85.StylePriority.UseFont = false;
        this.xrTableCell85.StylePriority.UsePadding = false;
        this.xrTableCell85.Text = "Ação Proposta";
        this.xrTableCell85.Weight = 0.45540092089314D;
        // 
        // xrTableCell93
        // 
        this.xrTableCell93.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell93.Dpi = 254F;
        this.xrTableCell93.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell93.Name = "xrTableCell93";
        this.xrTableCell93.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell93.StylePriority.UseBorders = false;
        this.xrTableCell93.StylePriority.UseFont = false;
        this.xrTableCell93.StylePriority.UsePadding = false;
        this.xrTableCell93.Text = "Responsável";
        this.xrTableCell93.Weight = 0.291458185315786D;
        // 
        // xrTableCell86
        // 
        this.xrTableCell86.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell86.Dpi = 254F;
        this.xrTableCell86.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell86.Name = "xrTableCell86";
        this.xrTableCell86.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell86.StylePriority.UseBorders = false;
        this.xrTableCell86.StylePriority.UseFont = false;
        this.xrTableCell86.StylePriority.UsePadding = false;
        this.xrTableCell86.Text = "Data de Conclusão";
        this.xrTableCell86.Weight = 0.196490370913461D;
        // 
        // xrTableCell87
        // 
        this.xrTableCell87.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell87.Dpi = 254F;
        this.xrTableCell87.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell87.Name = "xrTableCell87";
        this.xrTableCell87.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell87.StylePriority.UseBorders = false;
        this.xrTableCell87.StylePriority.UseFont = false;
        this.xrTableCell87.StylePriority.UsePadding = false;
        this.xrTableCell87.Text = "Status do Risco";
        this.xrTableCell87.Weight = 0.140758220742207D;
        // 
        // GroupHeader16
        // 
        this.GroupHeader16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19});
        this.GroupHeader16.Dpi = 254F;
        this.GroupHeader16.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader16.HeightF = 75.00001F;
        this.GroupHeader16.Level = 1;
        this.GroupHeader16.Name = "GroupHeader16";
        // 
        // xrLabel19
        // 
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 25.00001F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel19.StyleName = "FieldCaption";
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.Text = "19. Análise de Riscos";
        // 
        // DetailReport12
        // 
        this.DetailReport12.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail13,
            this.GroupHeader7,
            this.GroupHeader15});
        this.DetailReport12.DataMember = "Revisao.Revisao_PlanoComunicacao";
        this.DetailReport12.DataSource = this.dsPlanoProjeto;
        this.DetailReport12.Dpi = 254F;
        this.DetailReport12.Level = 8;
        this.DetailReport12.Name = "DetailReport12";
        // 
        // Detail13
        // 
        this.Detail13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable16});
        this.Detail13.Dpi = 254F;
        this.Detail13.HeightF = 50F;
        this.Detail13.Name = "Detail13";
        // 
        // xrTable16
        // 
        this.xrTable16.Dpi = 254F;
        this.xrTable16.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 0F);
        this.xrTable16.Name = "xrTable16";
        this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19});
        this.xrTable16.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable16.StyleName = "Table";
        this.xrTable16.StylePriority.UseFont = false;
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell71,
            this.xrTableCell76,
            this.xrTableCell77,
            this.xrTableCell78,
            this.xrTableCell79,
            this.xrTableCell80,
            this.xrTableCell81});
        this.xrTableRow19.Dpi = 254F;
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.Weight = 1D;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_PlanoComunicacao.IdentificacaoObjetoComunicacao")});
        this.xrTableCell71.Dpi = 254F;
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell71.StylePriority.UseBorders = false;
        this.xrTableCell71.StylePriority.UsePadding = false;
        this.xrTableCell71.Text = "Objeto de comunicação";
        this.xrTableCell71.Weight = 0.43321300189848D;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_PlanoComunicacao.IdentificacaoAssunto")});
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell76.StylePriority.UseBorders = false;
        this.xrTableCell76.StylePriority.UsePadding = false;
        this.xrTableCell76.Text = "Assuntos abordados";
        this.xrTableCell76.Weight = 0.43321300189848D;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_PlanoComunicacao.IdentificacaoMeioComunicacao")});
        this.xrTableCell77.Dpi = 254F;
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell77.StylePriority.UseBorders = false;
        this.xrTableCell77.StylePriority.UsePadding = false;
        this.xrTableCell77.Text = "Meios Utilizados";
        this.xrTableCell77.Weight = 0.43321300189848D;
        // 
        // xrTableCell78
        // 
        this.xrTableCell78.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell78.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_PlanoComunicacao.IdentificacaoPeriodicidade")});
        this.xrTableCell78.Dpi = 254F;
        this.xrTableCell78.Name = "xrTableCell78";
        this.xrTableCell78.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell78.StylePriority.UseBorders = false;
        this.xrTableCell78.StylePriority.UsePadding = false;
        this.xrTableCell78.Text = "Periodicidade";
        this.xrTableCell78.Weight = 0.43321300189848D;
        // 
        // xrTableCell79
        // 
        this.xrTableCell79.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_PlanoComunicacao.IdentificacaoPrazoValidacao")});
        this.xrTableCell79.Dpi = 254F;
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell79.StylePriority.UseBorders = false;
        this.xrTableCell79.StylePriority.UsePadding = false;
        this.xrTableCell79.Text = "Prazos de Validação";
        this.xrTableCell79.Weight = 0.433212990881303D;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_PlanoComunicacao.IdentificacaoDestinatarios")});
        this.xrTableCell80.Dpi = 254F;
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell80.StylePriority.UseBorders = false;
        this.xrTableCell80.StylePriority.UsePadding = false;
        this.xrTableCell80.Text = "Destinatários";
        this.xrTableCell80.Weight = 0.433212990881304D;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell81.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_PlanoComunicacao.IdentificacaoResponsavel")});
        this.xrTableCell81.Dpi = 254F;
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell81.StylePriority.UseBorders = false;
        this.xrTableCell81.StylePriority.UsePadding = false;
        this.xrTableCell81.Text = "Responsável";
        this.xrTableCell81.Weight = 0.400722010643474D;
        // 
        // GroupHeader7
        // 
        this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable17});
        this.GroupHeader7.Dpi = 254F;
        this.GroupHeader7.HeightF = 50F;
        this.GroupHeader7.Name = "GroupHeader7";
        this.GroupHeader7.RepeatEveryPage = true;
        // 
        // xrTable17
        // 
        this.xrTable17.Dpi = 254F;
        this.xrTable17.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable17.Name = "xrTable17";
        this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20});
        this.xrTable17.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable17.StyleName = "Table";
        this.xrTable17.StylePriority.UseFont = false;
        // 
        // xrTableRow20
        // 
        this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell62,
            this.xrTableCell72,
            this.xrTableCell69,
            this.xrTableCell73,
            this.xrTableCell70,
            this.xrTableCell74,
            this.xrTableCell75});
        this.xrTableRow20.Dpi = 254F;
        this.xrTableRow20.Name = "xrTableRow20";
        this.xrTableRow20.Weight = 1D;
        // 
        // xrTableCell62
        // 
        this.xrTableCell62.Dpi = 254F;
        this.xrTableCell62.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell62.Name = "xrTableCell62";
        this.xrTableCell62.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell62.StylePriority.UseFont = false;
        this.xrTableCell62.StylePriority.UsePadding = false;
        this.xrTableCell62.Text = "Objeto de comunicação";
        this.xrTableCell62.Weight = 0.43321300189848D;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell72.Dpi = 254F;
        this.xrTableCell72.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.StylePriority.UseFont = false;
        this.xrTableCell72.StylePriority.UsePadding = false;
        this.xrTableCell72.Text = "Assuntos abordados";
        this.xrTableCell72.Weight = 0.43321300189848D;
        // 
        // xrTableCell69
        // 
        this.xrTableCell69.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell69.Dpi = 254F;
        this.xrTableCell69.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell69.Name = "xrTableCell69";
        this.xrTableCell69.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell69.StylePriority.UseBorders = false;
        this.xrTableCell69.StylePriority.UseFont = false;
        this.xrTableCell69.StylePriority.UsePadding = false;
        this.xrTableCell69.Text = "Meios Utilizados";
        this.xrTableCell69.Weight = 0.43321300189848D;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell73.Dpi = 254F;
        this.xrTableCell73.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell73.StylePriority.UseBorders = false;
        this.xrTableCell73.StylePriority.UseFont = false;
        this.xrTableCell73.StylePriority.UsePadding = false;
        this.xrTableCell73.Text = "Periodicidade";
        this.xrTableCell73.Weight = 0.43321300189848D;
        // 
        // xrTableCell70
        // 
        this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell70.Dpi = 254F;
        this.xrTableCell70.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell70.Name = "xrTableCell70";
        this.xrTableCell70.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell70.StylePriority.UseBorders = false;
        this.xrTableCell70.StylePriority.UseFont = false;
        this.xrTableCell70.StylePriority.UsePadding = false;
        this.xrTableCell70.Text = "Prazos de Validação";
        this.xrTableCell70.Weight = 0.433212990881303D;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell74.Dpi = 254F;
        this.xrTableCell74.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell74.StylePriority.UseBorders = false;
        this.xrTableCell74.StylePriority.UseFont = false;
        this.xrTableCell74.StylePriority.UsePadding = false;
        this.xrTableCell74.Text = "Destinatários";
        this.xrTableCell74.Weight = 0.433212990881304D;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell75.Dpi = 254F;
        this.xrTableCell75.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell75.StylePriority.UseBorders = false;
        this.xrTableCell75.StylePriority.UseFont = false;
        this.xrTableCell75.StylePriority.UsePadding = false;
        this.xrTableCell75.Text = "Responsável";
        this.xrTableCell75.Weight = 0.400722010643474D;
        // 
        // GroupHeader15
        // 
        this.GroupHeader15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
        this.GroupHeader15.Dpi = 254F;
        this.GroupHeader15.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader15.HeightF = 75.00001F;
        this.GroupHeader15.Level = 1;
        this.GroupHeader15.Name = "GroupHeader15";
        // 
        // xrLabel20
        // 
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 25.00001F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel20.StyleName = "FieldCaption";
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.Text = "18. Plano de Comunicação";
        // 
        // DetailReport7
        // 
        this.DetailReport7.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail8,
            this.GroupHeader9,
            this.GroupHeader20});
        this.DetailReport7.DataMember = "Revisao.Revisao_RelacionamentoProjeto";
        this.DetailReport7.DataSource = this.dsPlanoProjeto;
        this.DetailReport7.Dpi = 254F;
        this.DetailReport7.Level = 10;
        this.DetailReport7.Name = "DetailReport7";
        // 
        // Detail8
        // 
        this.Detail8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable21});
        this.Detail8.Dpi = 254F;
        this.Detail8.HeightF = 50F;
        this.Detail8.Name = "Detail8";
        // 
        // xrTable21
        // 
        this.xrTable21.Dpi = 254F;
        this.xrTable21.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable21.Name = "xrTable21";
        this.xrTable21.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow24});
        this.xrTable21.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable21.StyleName = "Table";
        this.xrTable21.StylePriority.UseFont = false;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell106,
            this.xrTableCell108,
            this.xrTableCell109});
        this.xrTableRow24.Dpi = 254F;
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.Weight = 1D;
        // 
        // xrTableCell106
        // 
        this.xrTableCell106.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell106.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_RelacionamentoProjeto.IdentificacaoProjetoRelacionado")});
        this.xrTableCell106.Dpi = 254F;
        this.xrTableCell106.Name = "xrTableCell106";
        this.xrTableCell106.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell106.StylePriority.UseBorders = false;
        this.xrTableCell106.StylePriority.UsePadding = false;
        this.xrTableCell106.Text = "Projeto Relacionado";
        this.xrTableCell106.Weight = 0.758122754699487D;
        // 
        // xrTableCell108
        // 
        this.xrTableCell108.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell108.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_RelacionamentoProjeto.IdentificacaoRelacionamento")});
        this.xrTableCell108.Dpi = 254F;
        this.xrTableCell108.Name = "xrTableCell108";
        this.xrTableCell108.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell108.StylePriority.UseBorders = false;
        this.xrTableCell108.StylePriority.UsePadding = false;
        this.xrTableCell108.Text = "Descrição do relacionamento";
        this.xrTableCell108.Weight = 0.758122754699487D;
        // 
        // xrTableCell109
        // 
        this.xrTableCell109.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell109.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_RelacionamentoProjeto.DescricaoPlanoAcao")});
        this.xrTableCell109.Dpi = 254F;
        this.xrTableCell109.Name = "xrTableCell109";
        this.xrTableCell109.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell109.StylePriority.UseBorders = false;
        this.xrTableCell109.StylePriority.UsePadding = false;
        this.xrTableCell109.Text = "Plano de Ação";
        this.xrTableCell109.Weight = 1.48375449060103D;
        // 
        // GroupHeader9
        // 
        this.GroupHeader9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable20});
        this.GroupHeader9.Dpi = 254F;
        this.GroupHeader9.HeightF = 50F;
        this.GroupHeader9.Name = "GroupHeader9";
        this.GroupHeader9.RepeatEveryPage = true;
        // 
        // xrTable20
        // 
        this.xrTable20.Dpi = 254F;
        this.xrTable20.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable20.Name = "xrTable20";
        this.xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23});
        this.xrTable20.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable20.StyleName = "Table";
        this.xrTable20.StylePriority.UseFont = false;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell104,
            this.xrTableCell105,
            this.xrTableCell107});
        this.xrTableRow23.Dpi = 254F;
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.Weight = 1D;
        // 
        // xrTableCell104
        // 
        this.xrTableCell104.Dpi = 254F;
        this.xrTableCell104.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell104.Name = "xrTableCell104";
        this.xrTableCell104.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell104.StylePriority.UseFont = false;
        this.xrTableCell104.StylePriority.UsePadding = false;
        this.xrTableCell104.Text = "Projeto Relacionado";
        this.xrTableCell104.Weight = 0.758122754699487D;
        // 
        // xrTableCell105
        // 
        this.xrTableCell105.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell105.Dpi = 254F;
        this.xrTableCell105.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell105.Name = "xrTableCell105";
        this.xrTableCell105.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell105.StylePriority.UseBorders = false;
        this.xrTableCell105.StylePriority.UseFont = false;
        this.xrTableCell105.StylePriority.UsePadding = false;
        this.xrTableCell105.Text = "Descrição do relacionamento";
        this.xrTableCell105.Weight = 0.758122754699487D;
        // 
        // xrTableCell107
        // 
        this.xrTableCell107.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell107.Dpi = 254F;
        this.xrTableCell107.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell107.Name = "xrTableCell107";
        this.xrTableCell107.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell107.StylePriority.UseBorders = false;
        this.xrTableCell107.StylePriority.UseFont = false;
        this.xrTableCell107.StylePriority.UsePadding = false;
        this.xrTableCell107.Text = "Plano de Ação";
        this.xrTableCell107.Weight = 1.48375449060103D;
        // 
        // GroupHeader20
        // 
        this.GroupHeader20.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21});
        this.GroupHeader20.Dpi = 254F;
        this.GroupHeader20.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader20.HeightF = 75.00001F;
        this.GroupHeader20.Level = 1;
        this.GroupHeader20.Name = "GroupHeader20";
        // 
        // xrLabel21
        // 
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel21.StyleName = "FieldCaption";
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.Text = "20. Relacionamento com outros projetos";
        // 
        // DetailReport8
        // 
        this.DetailReport8.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail9,
            this.GroupHeader10,
            this.GroupHeader17});
        this.DetailReport8.DataMember = "Revisao";
        this.DetailReport8.DataSource = this.dsPlanoProjeto;
        this.DetailReport8.Dpi = 254F;
        this.DetailReport8.Level = 11;
        this.DetailReport8.Name = "DetailReport8";
        // 
        // Detail9
        // 
        this.Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable23});
        this.Detail9.Dpi = 254F;
        this.Detail9.HeightF = 50F;
        this.Detail9.Name = "Detail9";
        // 
        // xrTable23
        // 
        this.xrTable23.Dpi = 254F;
        this.xrTable23.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable23.Name = "xrTable23";
        this.xrTable23.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable23.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable23.StyleName = "Table";
        this.xrTable23.StylePriority.UseFont = false;
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell114,
            this.xrTableCell115,
            this.xrTableCell116,
            this.xrTableCell117});
        this.xrTableRow26.Dpi = 254F;
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 1D;
        // 
        // xrTableCell114
        // 
        this.xrTableCell114.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell114.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.NomeUsuarioPatrocinador")});
        this.xrTableCell114.Dpi = 254F;
        this.xrTableCell114.Name = "xrTableCell114";
        this.xrTableCell114.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell114.StylePriority.UseBorders = false;
        this.xrTableCell114.StylePriority.UsePadding = false;
        this.xrTableCell114.Text = "Patrocinador do Projeto";
        this.xrTableCell114.Weight = 0.758122754699487D;
        // 
        // xrTableCell115
        // 
        this.xrTableCell115.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell115.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.NomeUsuarioGerenteNacional")});
        this.xrTableCell115.Dpi = 254F;
        this.xrTableCell115.Name = "xrTableCell115";
        this.xrTableCell115.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell115.StylePriority.UseBorders = false;
        this.xrTableCell115.StylePriority.UsePadding = false;
        this.xrTableCell115.Weight = 0.758122754699487D;
        // 
        // xrTableCell116
        // 
        this.xrTableCell116.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell116.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.NomeUsuarioSuporteEP")});
        this.xrTableCell116.Dpi = 254F;
        this.xrTableCell116.Name = "xrTableCell116";
        this.xrTableCell116.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell116.StylePriority.UseBorders = false;
        this.xrTableCell116.StylePriority.UsePadding = false;
        this.xrTableCell116.Text = "Escritório de Projetos / GEDTI";
        this.xrTableCell116.Weight = 0.758122732665134D;
        // 
        // xrTableCell117
        // 
        this.xrTableCell117.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell117.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.IdentificacaoAprovacaoDocumento")});
        this.xrTableCell117.Dpi = 254F;
        this.xrTableCell117.Name = "xrTableCell117";
        this.xrTableCell117.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell117.StylePriority.UseBorders = false;
        this.xrTableCell117.StylePriority.UsePadding = false;
        this.xrTableCell117.Text = "Comprovação de aprovação do documento";
        this.xrTableCell117.Weight = 0.725631757935892D;
        // 
        // GroupHeader10
        // 
        this.GroupHeader10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable22});
        this.GroupHeader10.Dpi = 254F;
        this.GroupHeader10.HeightF = 50F;
        this.GroupHeader10.Name = "GroupHeader10";
        this.GroupHeader10.RepeatEveryPage = true;
        // 
        // xrTable22
        // 
        this.xrTable22.Dpi = 254F;
        this.xrTable22.Font = new System.Drawing.Font("Century Gothic", 9F);
        this.xrTable22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable22.Name = "xrTable22";
        this.xrTable22.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow25});
        this.xrTable22.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable22.StyleName = "Table";
        this.xrTable22.StylePriority.UseFont = false;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell110,
            this.xrTableCell111,
            this.xrTableCell112,
            this.xrTableCell113});
        this.xrTableRow25.Dpi = 254F;
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 1D;
        // 
        // xrTableCell110
        // 
        this.xrTableCell110.Dpi = 254F;
        this.xrTableCell110.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell110.Name = "xrTableCell110";
        this.xrTableCell110.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell110.StylePriority.UseFont = false;
        this.xrTableCell110.StylePriority.UsePadding = false;
        this.xrTableCell110.Text = "Patrocinador do Projeto";
        this.xrTableCell110.Weight = 0.758122754699487D;
        // 
        // xrTableCell111
        // 
        this.xrTableCell111.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell111.Dpi = 254F;
        this.xrTableCell111.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell111.Name = "xrTableCell111";
        this.xrTableCell111.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell111.StylePriority.UseBorders = false;
        this.xrTableCell111.StylePriority.UseFont = false;
        this.xrTableCell111.StylePriority.UsePadding = false;
        this.xrTableCell111.Text = "Gerente Nacional do Projeto";
        this.xrTableCell111.Weight = 0.758122754699487D;
        // 
        // xrTableCell112
        // 
        this.xrTableCell112.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell112.Dpi = 254F;
        this.xrTableCell112.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell112.Name = "xrTableCell112";
        this.xrTableCell112.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell112.StylePriority.UseBorders = false;
        this.xrTableCell112.StylePriority.UseFont = false;
        this.xrTableCell112.StylePriority.UsePadding = false;
        this.xrTableCell112.Text = "Escritório de Projetos / GEDTI";
        this.xrTableCell112.Weight = 0.758122732665134D;
        // 
        // xrTableCell113
        // 
        this.xrTableCell113.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell113.Dpi = 254F;
        this.xrTableCell113.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell113.Name = "xrTableCell113";
        this.xrTableCell113.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell113.StylePriority.UseBorders = false;
        this.xrTableCell113.StylePriority.UseFont = false;
        this.xrTableCell113.StylePriority.UsePadding = false;
        this.xrTableCell113.Text = "Comprovação de aprovação do documento";
        this.xrTableCell113.Weight = 0.725631757935892D;
        // 
        // GroupHeader17
        // 
        this.GroupHeader17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel22});
        this.GroupHeader17.Dpi = 254F;
        this.GroupHeader17.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader17.HeightF = 64.41644F;
        this.GroupHeader17.Level = 1;
        this.GroupHeader17.Name = "GroupHeader17";
        // 
        // xrLabel22
        // 
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 14.41643F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel22.StyleName = "FieldCaption";
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.Text = "21. Aprovadores";
        // 
        // Table
        // 
        this.Table.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.Table.Name = "Table";
        this.Table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader4,
            this.GroupHeader18});
        this.DetailReport2.DataMember = "Revisao.Revisao_Entregas";
        this.DetailReport2.DataSource = this.dsPlanoProjeto;
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Level = 5;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 50F;
        this.Detail3.Name = "Detail3";
        // 
        // xrTable11
        // 
        this.xrTable11.Dpi = 254F;
        this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0.0004037221F, 0F);
        this.xrTable11.Name = "xrTable11";
        this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
        this.xrTable11.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable11.StyleName = "Table";
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell47});
        this.xrTableRow14.Dpi = 254F;
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 1D;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.IdentificacaoEntrega")});
        this.xrTableCell43.Dpi = 254F;
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell43.StylePriority.UseBorders = false;
        this.xrTableCell43.StylePriority.UsePadding = false;
        this.xrTableCell43.Text = "Entrega da EAP";
        this.xrTableCell43.Weight = 1.66787010220414D;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.DescricaoEntrega")});
        this.xrTableCell44.Dpi = 254F;
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell44.StylePriority.UseBorders = false;
        this.xrTableCell44.StylePriority.UsePadding = false;
        this.xrTableCell44.Text = "Descrição da Entrega";
        this.xrTableCell44.Weight = 2.16606500398381D;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_Entregas.DescricaoCriterioAceitacao")});
        this.xrTableCell47.Dpi = 254F;
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell47.StylePriority.UseBorders = false;
        this.xrTableCell47.StylePriority.UsePadding = false;
        this.xrTableCell47.Text = "Critério de Aceitação";
        this.xrTableCell47.Weight = 2.16606489381205D;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
        this.GroupHeader4.Dpi = 254F;
        this.GroupHeader4.HeightF = 50F;
        this.GroupHeader4.Name = "GroupHeader4";
        this.GroupHeader4.RepeatEveryPage = true;
        // 
        // xrTable10
        // 
        this.xrTable10.Dpi = 254F;
        this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable10.Name = "xrTable10";
        this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
        this.xrTable10.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrTable10.StyleName = "Table";
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell45,
            this.xrTableCell46,
            this.xrTableCell48});
        this.xrTableRow13.Dpi = 254F;
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1D;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.Dpi = 254F;
        this.xrTableCell45.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell45.StylePriority.UseFont = false;
        this.xrTableCell45.StylePriority.UsePadding = false;
        this.xrTableCell45.Text = "Entrega da EAP";
        this.xrTableCell45.Weight = 1.66787010220414D;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell46.Dpi = 254F;
        this.xrTableCell46.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell46.StylePriority.UseBorders = false;
        this.xrTableCell46.StylePriority.UseFont = false;
        this.xrTableCell46.StylePriority.UsePadding = false;
        this.xrTableCell46.Text = "Descrição da Entrega";
        this.xrTableCell46.Weight = 2.16606500398381D;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell48.Dpi = 254F;
        this.xrTableCell48.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell48.StylePriority.UseBorders = false;
        this.xrTableCell48.StylePriority.UseFont = false;
        this.xrTableCell48.StylePriority.UsePadding = false;
        this.xrTableCell48.Text = "Critério de Aceitação";
        this.xrTableCell48.Weight = 2.16606489381205D;
        // 
        // GroupHeader18
        // 
        this.GroupHeader18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel34});
        this.GroupHeader18.Dpi = 254F;
        this.GroupHeader18.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader18.HeightF = 64.41676F;
        this.GroupHeader18.Level = 1;
        this.GroupHeader18.Name = "GroupHeader18";
        // 
        // xrLabel34
        // 
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(0F, 14.41676F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel34.StyleName = "FieldCaption";
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.Text = "15.3 Dicionário da EAP";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine3,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 100F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLine3
        // 
        this.xrLine3.Dpi = 254F;
        this.xrLine3.ForeColor = System.Drawing.Color.LightGray;
        this.xrLine3.LineWidth = 5;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(2770F, 10F);
        this.xrLine3.StylePriority.UseForeColor = false;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Format = "Pagina {0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(2457F, 20.00007F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(287.9995F, 58.42F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // pCodigoStatusReport
        // 
        this.pCodigoStatusReport.Name = "pCodigoStatusReport";
        this.pCodigoStatusReport.Type = typeof(int);
        this.pCodigoStatusReport.ValueInfo = "0";
        this.pCodigoStatusReport.Visible = false;
        // 
        // DetailReport9
        // 
        this.DetailReport9.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail10,
            this.GroupHeader11,
            this.GroupHeader21});
        this.DetailReport9.DataMember = "Revisao.Revisao_EstruturaTopicosEAP";
        this.DetailReport9.DataSource = this.dsPlanoProjeto;
        this.DetailReport9.Dpi = 254F;
        this.DetailReport9.Level = 4;
        this.DetailReport9.Name = "DetailReport9";
        // 
        // Detail10
        // 
        this.Detail10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable25});
        this.Detail10.Dpi = 254F;
        this.Detail10.HeightF = 50F;
        this.Detail10.Name = "Detail10";
        // 
        // xrTable25
        // 
        this.xrTable25.Dpi = 254F;
        this.xrTable25.FormattingRules.Add(this.EAP_FonteNegrito);
        this.xrTable25.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable25.Name = "xrTable25";
        this.xrTable25.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28});
        this.xrTable25.SizeF = new System.Drawing.SizeF(2770F, 50F);
        // 
        // EAP_FonteNegrito
        // 
        this.EAP_FonteNegrito.Condition = "[IndicaFonteNegrito] == \'S\'";
        this.EAP_FonteNegrito.DataMember = "EstruturaTopicosEAP";
        // 
        // 
        // 
        this.EAP_FonteNegrito.Formatting.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.EAP_FonteNegrito.Name = "EAP_FonteNegrito";
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell118,
            this.xrTableCell124,
            this.xrTableCell125,
            this.xrTableCell126,
            this.xrTableCell127});
        this.xrTableRow28.Dpi = 254F;
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1D;
        // 
        // xrTableCell118
        // 
        this.xrTableCell118.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EstruturaTopicosEAP.IdentificacaoTarefa")});
        this.xrTableCell118.Dpi = 254F;
        this.xrTableCell118.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell118.Name = "xrTableCell118";
        this.xrTableCell118.StylePriority.UseFont = false;
        this.xrTableCell118.Text = "Nome da Entrega";
        this.xrTableCell118.Weight = 3.70603050779251D;
        this.xrTableCell118.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell118_BeforePrint);
        // 
        // xrTableCell124
        // 
        this.xrTableCell124.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EstruturaTopicosEAP.Duracao")});
        this.xrTableCell124.Dpi = 254F;
        this.xrTableCell124.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell124.Name = "xrTableCell124";
        this.xrTableCell124.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 25, 0, 0, 254F);
        this.xrTableCell124.StylePriority.UseFont = false;
        this.xrTableCell124.StylePriority.UsePadding = false;
        this.xrTableCell124.Text = "Duração";
        this.xrTableCell124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell124.Weight = 0.296482418756878D;
        // 
        // xrTableCell125
        // 
        this.xrTableCell125.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EstruturaTopicosEAP.PercentualReal", "{0} %")});
        this.xrTableCell125.Dpi = 254F;
        this.xrTableCell125.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell125.Name = "xrTableCell125";
        this.xrTableCell125.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 30, 0, 0, 254F);
        this.xrTableCell125.StylePriority.UseFont = false;
        this.xrTableCell125.StylePriority.UsePadding = false;
        this.xrTableCell125.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell125.Weight = 0.402252526417102D;
        // 
        // xrTableCell126
        // 
        this.xrTableCell126.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EstruturaTopicosEAP.Inicio", "{0:d}")});
        this.xrTableCell126.Dpi = 254F;
        this.xrTableCell126.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell126.Name = "xrTableCell126";
        this.xrTableCell126.StylePriority.UseFont = false;
        this.xrTableCell126.Text = "Início";
        this.xrTableCell126.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell126.Weight = 0.352072886021493D;
        // 
        // xrTableCell127
        // 
        this.xrTableCell127.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Revisao.Revisao_EstruturaTopicosEAP.Termino", "{0:d}")});
        this.xrTableCell127.Dpi = 254F;
        this.xrTableCell127.Font = new System.Drawing.Font("Century Gothic", 8F);
        this.xrTableCell127.Name = "xrTableCell127";
        this.xrTableCell127.StylePriority.UseFont = false;
        this.xrTableCell127.Text = "Término";
        this.xrTableCell127.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell127.Weight = 0.376013602502877D;
        // 
        // GroupHeader11
        // 
        this.GroupHeader11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable24});
        this.GroupHeader11.Dpi = 254F;
        this.GroupHeader11.HeightF = 50F;
        this.GroupHeader11.Name = "GroupHeader11";
        this.GroupHeader11.RepeatEveryPage = true;
        // 
        // xrTable24
        // 
        this.xrTable24.Dpi = 254F;
        this.xrTable24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable24.Name = "xrTable24";
        this.xrTable24.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable24.SizeF = new System.Drawing.SizeF(2770F, 50F);
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell119,
            this.xrTableCell120,
            this.xrTableCell121,
            this.xrTableCell122,
            this.xrTableCell123});
        this.xrTableRow27.Dpi = 254F;
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1D;
        // 
        // xrTableCell119
        // 
        this.xrTableCell119.Dpi = 254F;
        this.xrTableCell119.Font = new System.Drawing.Font("Century Gothic", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrTableCell119.Name = "xrTableCell119";
        this.xrTableCell119.StylePriority.UseFont = false;
        this.xrTableCell119.Text = "Item";
        this.xrTableCell119.Weight = 4.86126464947284D;
        // 
        // xrTableCell120
        // 
        this.xrTableCell120.Dpi = 254F;
        this.xrTableCell120.Font = new System.Drawing.Font("Century Gothic", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrTableCell120.Name = "xrTableCell120";
        this.xrTableCell120.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 254F);
        this.xrTableCell120.StylePriority.UseFont = false;
        this.xrTableCell120.StylePriority.UsePadding = false;
        this.xrTableCell120.StylePriority.UseTextAlignment = false;
        this.xrTableCell120.Text = "Duração";
        this.xrTableCell120.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell120.Weight = 0.388901189520311D;
        // 
        // xrTableCell121
        // 
        this.xrTableCell121.Dpi = 254F;
        this.xrTableCell121.Font = new System.Drawing.Font("Century Gothic", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrTableCell121.Name = "xrTableCell121";
        this.xrTableCell121.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 254F);
        this.xrTableCell121.StylePriority.UseFont = false;
        this.xrTableCell121.StylePriority.UsePadding = false;
        this.xrTableCell121.StylePriority.UseTextAlignment = false;
        this.xrTableCell121.Text = "% Concluído";
        this.xrTableCell121.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell121.Weight = 0.527636540047465D;
        // 
        // xrTableCell122
        // 
        this.xrTableCell122.Dpi = 254F;
        this.xrTableCell122.Font = new System.Drawing.Font("Century Gothic", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrTableCell122.Name = "xrTableCell122";
        this.xrTableCell122.StylePriority.UseFont = false;
        this.xrTableCell122.StylePriority.UseTextAlignment = false;
        this.xrTableCell122.Text = "Início";
        this.xrTableCell122.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell122.Weight = 0.461820167652857D;
        // 
        // xrTableCell123
        // 
        this.xrTableCell123.Dpi = 254F;
        this.xrTableCell123.Font = new System.Drawing.Font("Century Gothic", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrTableCell123.Name = "xrTableCell123";
        this.xrTableCell123.StylePriority.UseFont = false;
        this.xrTableCell123.StylePriority.UseTextAlignment = false;
        this.xrTableCell123.Text = "Término";
        this.xrTableCell123.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell123.Weight = 0.493229394797386D;
        // 
        // GroupHeader21
        // 
        this.GroupHeader21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel41});
        this.GroupHeader21.Dpi = 254F;
        this.GroupHeader21.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader21.HeightF = 75.00001F;
        this.GroupHeader21.Level = 1;
        this.GroupHeader21.Name = "GroupHeader21";
        // 
        // xrLabel41
        // 
        this.xrLabel41.Dpi = 254F;
        this.xrLabel41.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(0.000565211F, 25.00001F);
        this.xrLabel41.Name = "xrLabel41";
        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel41.SizeF = new System.Drawing.SizeF(2770F, 50F);
        this.xrLabel41.StyleName = "FieldCaption";
        this.xrLabel41.StylePriority.UseFont = false;
        this.xrLabel41.Text = "15.2 Estrutura de Tópicos";
        // 
        // RelPlanoProjeto_001
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.PageHeader,
            this.DetailReport1,
            this.DetailReport,
            this.DetailReport11,
            this.DetailReport3,
            this.DetailReport4,
            this.DetailReport5,
            this.DetailReport6,
            this.DetailReport12,
            this.DetailReport7,
            this.DetailReport8,
            this.DetailReport2,
            this.PageFooter,
            this.DetailReport9});
        this.DataMember = "Revisao";
        this.DataSource = this.dsPlanoProjeto;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Century Gothic", 9.75F);
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.EAP_FonteNegrito});
        this.Landscape = true;
        this.PageHeight = 2100;
        this.PageWidth = 2970;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pCodigoProjeto,
            this.pCodigoUsuario,
            this.pCodigoEntidade,
            this.pCodigoStatusReport,
            this.pNomeProjeto,
            this.pDataGeracao});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.FieldCaption,
            this.PageInfo,
            this.DataField,
            this.Title,
            this.Table});
        this.Version = "15.1";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.RelPlanoProjeto_001_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.dsPlanoProjeto)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void RelPlanoProjeto_001_DataSourceDemanded(object sender, EventArgs e)
    {
        InitData();
    }

    private void xrTableCell118_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRTableCell cell = (XRTableCell)sender;
        PaddingInfo padding = cell.Padding;
        System.Drawing.Font font;

        int nivel = cell.Report.GetCurrentColumnValue<int>("Nivel");
        padding.Left = nivel * 20;
        cell.Padding = padding;

        string indicaFonteNegrito = cell.Report.GetCurrentColumnValue<string>("IndicaFonteNegrito");
        if (indicaFonteNegrito == "S")
            font = new System.Drawing.Font("Century Gothic", 8F, FontStyle.Bold);
        else
            font = new System.Drawing.Font("Century Gothic", 8F, FontStyle.Italic);

        cell.Font = font;
    }
}
