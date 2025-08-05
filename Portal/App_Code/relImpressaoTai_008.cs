using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for relImpressaoTai_008
/// </summary>
public class relImpressaoTai_008 : DevExpress.XtraReports.UI.XtraReport
{
    private dados cDados = CdadosUtil.GetCdados(null);
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private XRControlStyle TableCaption;
    private XRControlStyle TableDataField;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPictureBox picLogoEntidade;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    public DevExpress.XtraReports.Parameters.Parameter pTitulo;
    private XRLine xrLine1;
    private XRLabel xrLabel35;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private dsImpressaoTai_008 dsImpressaoTai_0081;
    private XRLabel xrLabel24;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel xrLabel21;
    private XRLabel xrLabel27;
    private XRLabel xrLabel28;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel xrLabel32;
    private XRLabel xrLabel31;
    private DetailReportBand DetailReport4;
    private DetailBand Detail5;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell21;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell5;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private DetailReportBand DetailReport5;
    private DetailBand Detail6;
    private XRTable xrTable7;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell18;
    private DetailReportBand DetailReport6;
    private DetailBand Detail7;
    private XRTable xrTable8;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell19;
    private DetailReportBand DetailReport7;
    private DetailBand Detail8;
    private XRTable xrTable12;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell24;
    private ReportFooterBand ReportFooter;
    private ReportFooterBand ReportFooter1;
    private ReportFooterBand ReportFooter2;
    private ReportFooterBand ReportFooter3;
    private ReportFooterBand ReportFooter4;
    private ReportFooterBand ReportFooter5;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel17;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel18;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel14;
    private GroupHeaderBand GroupHeader4;
    private XRLabel xrLabel9;
    private GroupHeaderBand GroupHeader5;
    private XRLabel xrLabel7;
    private GroupHeaderBand GroupHeader6;
    private XRLabel xrLabel8;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRLabel xrLabel15;
    private DetailReportBand DetailReport8;
    private DetailBand Detail9;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private GroupHeaderBand GroupHeader7;
    private XRLabel xrLabel16;
    private ReportFooterBand ReportFooter6;
    private XRLabel xrLabel12;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relImpressaoTai_008(Int32 codigoProjeto)
    {
        InitializeComponent();

        InitData(codigoProjeto);
    }

    private void InitData(int codigoProjeto)
    {
        DefineLogoEntidade();


        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        //xrPictureBox1.Image = logo;

        int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        comandoSql = string.Format(
        @"BEGIN
          
          DECLARE @CodigoProjeto as Int
              SET @CodigoProjeto = {2}
       
         --[INICIO] [TermoAbertura04] [INICIO] DATASET [0]
          SELECT ta.[CodigoProjeto]
                ,ta.[NomeIniciativa]
                ,ta.[CodigoGerenteIniciativa]
                ,ta.[NomeGerenteIniciativa]
                ,ta.[CodigoUnidadeNegocio]
                ,ta.[NomeUnidadeNegocio]
                ,ta.[ValorEstimado]
                ,ta.[ObjetivoGeral]
                ,ta.[Justificativa]
                ,ta.[Escopo]
                ,ta.[CronogramaBasico]
                ,ta.[Observacoes]
                ,ta.[ValorEstimadoOrcamento]
                ,ta.[ServicosCompartilhados]
                ,ta.[IndicaModeloTAI007]
                ,ta.[PublicoAlvo]
                ,ta.[ProdutoFinal]
                ,ta.[IndicaModeloTAI008]
                ,ta.[DataInicioProjeto]
                ,ta.[DataTerminoProjeto]
                ,u.NomeUsuario as nomeGerenteUnidade
           FROM {0}.{1}.TermoAbertura04 ta
     INNER JOIN {0}.{1}.UnidadeNegocio un on (un.CodigoUnidadeNegocio = ta.CodigoUnidadeNegocio)
     INNER JOIN {0}.{1}.usuario as u on (un.CodigoUsuarioGerente = u.CodigoUsuario) 
           WHERE ta.CodigoProjeto = @CodigoProjeto
        --[FIM] [TermoAbertura04] [FIM]


       --[INICIO] [tai04_AcoesIniciativa] [INICIO] DATASET [1]

SELECT [CodigoAcaoIniciativa]
      ,[CodigoProjeto]
      ,[NomeAcao]
      ,[CodigoUsuarioResponsavel]
      ,{0}.{1}.[f_getResponsaveisAcaoTAI08](CodigoAcaoIniciativa) as [NomeUsuarioResponsavel]
      ,[DataInicio]
      ,[DataTermino]
      ,[ValorPrevisto]
      ,{0}.{1}.f_getServicoCompartilhadoAcaoTai08(CodigoAcaoIniciativa) AS ServicoCompartilhado 
  FROM {0}.{1}.tai04_AcoesIniciativa WHERE ([CodigoProjeto] = @CodigoProjeto) ORDER BY [NomeAcao]
       --[FIM] [tai04_AcoesIniciativa] [FIM]
      

      --[INICIO] [tai04_Parceiro] [INICIO] DATASET [2]
      SELECT DISTINCT
            u.CodigoUsuario,
            u.NomeUsuario
       FROM Usuario AS u INNER JOIN 
            tai04_ResponsavelAcao AS ra ON ra.CodigoUsuarioResponsavel = u.CodigoUsuario INNER JOIN
            tai04_AcoesIniciativa AS ai ON ai.CodigoAcaoIniciativa = ra.CodigoAcaoIniciativa
      WHERE ai.CodigoProjeto = @CodigoProjeto
        ORDER BY u.NomeUsuario
      --[FIM] [tai04_Parceiro] [FIM]    

     --[INICIO] [tai04_ProdutoIntermediario] [INICIO]  DATASET [3]
      SELECT * FROM {0}.{1}.tai04_ProdutoIntermediario 
         WHERE (CodigoProjeto = @CodigoProjeto) 
      ORDER BY DescricaoProdutoIntermediario
     --[FIM] [tai04_ProdutoIntermediario] [FIM]


    --[INICIO] [tai04_Premissa] [INICIO]  DATASET [4]
    SELECT * FROM [tai04_Premissa] 
     WHERE ([CodigoProjeto] = @CodigoProjeto)
    --[FIM] [tai04_Premissa] [FIM]


    --[INICIO] [tai04_Restricao] [INICIO]  DATASET [5]
    SELECT * FROM [tai04_Restricao] 
     WHERE ([CodigoProjeto] = @CodigoProjeto)
    --[FIM] [tai04_Restricao] [FIM]



    --[INICIO] [tai04_ParceirosIniciativa] [INICIO]  DATASET [6]
    SELECT * FROM [tai04_Parceiro] 
     WHERE ([CodigoProjeto] = @CodigoProjeto)
    --[FIM] [tai04_ParceirosIniciativa] [FIM]

    --[INICIO] [tai04_ParceirosIniciativa] [INICIO]  DATASET [7]
    SELECT CodigoProdutoFinal, CodigoProjeto, DescricaoProdutoFinal 
     FROM tai04_ProdutoFinal
     WHERE ([CodigoProjeto] = @CodigoProjeto)
    --[FIM]
END
", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSql);

        dsImpressaoTai_0081.Load(ds.Tables[0].CreateDataReader(), LoadOption.OverwriteChanges, "TermoAbertura04");
        dsImpressaoTai_0081.Load(ds.Tables[1].CreateDataReader(), LoadOption.OverwriteChanges, "tai04_AcoesIniciativa");
        dsImpressaoTai_0081.Load(ds.Tables[2].CreateDataReader(), LoadOption.OverwriteChanges, "Usuario");
        dsImpressaoTai_0081.Load(ds.Tables[3].CreateDataReader(), LoadOption.OverwriteChanges, "tai04_ProdutoIntermediario");
        dsImpressaoTai_0081.Load(ds.Tables[4].CreateDataReader(), LoadOption.OverwriteChanges, "tai04_Premissa");
        dsImpressaoTai_0081.Load(ds.Tables[5].CreateDataReader(), LoadOption.OverwriteChanges, "tai04_Restricao");
        dsImpressaoTai_0081.Load(ds.Tables[6].CreateDataReader(), LoadOption.OverwriteChanges, "tai04_Parceiro");
        dsImpressaoTai_0081.Load(ds.Tables[7].CreateDataReader(), LoadOption.OverwriteChanges, "tai04_ProdutoFinal");

    }


    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        Int32 codigoEntidade = (Int32)cDados.getInfoSistema("CodigoEntidade");
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        picLogoEntidade.Image = Bitmap.FromStream(ms);
    }

    private String ConnectionString
    {
        get
        {
            dados cDados = CdadosUtil.GetCdados(null);
            return cDados.classeDados.getStringConexao();
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
        string resourceFileName = "relImpressaoTai_008.resx";
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.pTitulo = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
        this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
        this.TableCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.TableDataField = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.dsImpressaoTai_0081 = new dsImpressaoTai_008();
        this.DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter2 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter3 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport6 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter4 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport7 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail8 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter5 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport8 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail9 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader7 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter6 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsImpressaoTai_0081)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(16F, 207F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1890F, 5F);
        // 
        // xrLabel35
        // 
        this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.pTitulo, "Text", "")});
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(598.5573F, 52F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(1271.997F, 84F);
        this.xrLabel35.StyleName = "Title";
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseForeColor = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "Termo de Abertura";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // pTitulo
        // 
        this.pTitulo.Description = "Titulo do Relatório";
        this.pTitulo.Name = "pTitulo";
        this.pTitulo.ValueInfo = "Termo de Abertura";
        this.pTitulo.Visible = false;
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Format = "Pág. {0}/{1}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(970.0002F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(936F, 58.42F);
        this.xrPageInfo2.StyleName = "PageInfo";
        this.xrPageInfo2.StylePriority.UsePadding = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Format = "Emitido em {0:dd/MM/yyyy - HH:mm}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(936F, 58.42F);
        this.xrPageInfo1.StyleName = "PageInfo";
        this.xrPageInfo1.StylePriority.UsePadding = false;
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel32,
            this.xrLabel31,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel28,
            this.xrLabel27,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 774.5224F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel32
        // 
        this.xrLabel32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.PublicoAlvo")});
        this.xrLabel32.Dpi = 254F;
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 685.1315F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(1906F, 58.41986F);
        this.xrLabel32.StylePriority.UseBorders = false;
        this.xrLabel32.Text = "xrLabel32";
        // 
        // xrLabel31
        // 
        this.xrLabel31.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 634.1315F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(1904F, 50.00006F);
        this.xrLabel31.StyleName = "FieldCaption";
        this.xrLabel31.StylePriority.UseBackColor = false;
        this.xrLabel31.Text = "Público-alvo:";
        // 
        // xrLabel30
        // 
        this.xrLabel30.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.Justificativa")});
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 555.1282F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(1904F, 58.41992F);
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.Text = "xrLabel30";
        // 
        // xrLabel29
        // 
        this.xrLabel29.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 505.1283F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(1906F, 49.99997F);
        this.xrLabel29.StyleName = "FieldCaption";
        this.xrLabel29.StylePriority.UseBackColor = false;
        this.xrLabel29.Text = "Justificativa";
        // 
        // xrLabel28
        // 
        this.xrLabel28.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.ObjetivoGeral")});
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 419.7709F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(1904F, 58.41995F);
        this.xrLabel28.StylePriority.UseBorders = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "xrLabel28";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel27
        // 
        this.xrLabel27.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(0.0004037221F, 370.0016F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(1906F, 49.72757F);
        this.xrLabel27.StyleName = "FieldCaption";
        this.xrLabel27.StylePriority.UseBackColor = false;
        this.xrLabel27.Text = "Objetivo";
        // 
        // xrLabel24
        // 
        this.xrLabel24.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(970.0002F, 243.5815F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(935.9993F, 50.00005F);
        this.xrLabel24.StyleName = "FieldCaption";
        this.xrLabel24.StylePriority.UseBackColor = false;
        this.xrLabel24.StylePriority.UsePadding = false;
        this.xrLabel24.Text = "Data Término";
        // 
        // xrLabel23
        // 
        this.xrLabel23.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 243.5815F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(950F, 50F);
        this.xrLabel23.StyleName = "FieldCaption";
        this.xrLabel23.StylePriority.UseBackColor = false;
        this.xrLabel23.Text = "Data Início";
        // 
        // xrLabel22
        // 
        this.xrLabel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.DataTerminoProjeto", "{0:dd/MM/yyyy}")});
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(969.9999F, 293.5816F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(936.0004F, 58.41998F);
        this.xrLabel22.StylePriority.UseBorders = false;
        this.xrLabel22.StylePriority.UsePadding = false;
        this.xrLabel22.StylePriority.UseTextAlignment = false;
        this.xrLabel22.Text = "xrLabel22";
        this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel21
        // 
        this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.DataInicioProjeto", "{0:dd/MM/yyyy}")});
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 293.5815F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(950F, 58.41995F);
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.Text = "xrLabel21";
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeUnidadeNegocio")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(969.9999F, 175F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(936.0001F, 58.42003F);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UsePadding = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(969.9992F, 125F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(936.0004F, 49.99999F);
        this.xrLabel5.StyleName = "FieldCaption";
        this.xrLabel5.StylePriority.UseBackColor = false;
        this.xrLabel5.StylePriority.UsePadding = false;
        this.xrLabel5.Text = "Unidade Responsável";
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeGerenteIniciativa")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(2F, 175F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(950F, 58.42001F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "xrLabel4";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(2F, 125F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(950F, 50F);
        this.xrLabel3.StyleName = "FieldCaption";
        this.xrLabel3.StylePriority.UseBackColor = false;
        this.xrLabel3.Text = "Gerente do Projeto";
        // 
        // xrLabel2
        // 
        this.xrLabel2.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1905.999F, 50F);
        this.xrLabel2.StyleName = "FieldCaption";
        this.xrLabel2.StylePriority.UseBackColor = false;
        this.xrLabel2.Text = "Nome do Projeto";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.NomeIniciativa")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.0003229777F, 50.00002F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1906F, 58.41998F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "xrLabel1";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 63.16536F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
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
        // Title
        // 
        this.Title.BackColor = System.Drawing.Color.White;
        this.Title.BorderColor = System.Drawing.SystemColors.ControlText;
        this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.Title.BorderWidth = 1F;
        this.Title.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
        this.Title.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Title.Name = "Title";
        // 
        // FieldCaption
        // 
        this.FieldCaption.BackColor = System.Drawing.Color.White;
        this.FieldCaption.BorderColor = System.Drawing.SystemColors.ControlText;
        this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.FieldCaption.BorderWidth = 1F;
        this.FieldCaption.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.FieldCaption.ForeColor = System.Drawing.SystemColors.ControlText;
        this.FieldCaption.Name = "FieldCaption";
        // 
        // PageInfo
        // 
        this.PageInfo.BackColor = System.Drawing.Color.White;
        this.PageInfo.BorderColor = System.Drawing.SystemColors.ControlText;
        this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PageInfo.BorderWidth = 1F;
        this.PageInfo.Font = new System.Drawing.Font("Verdana", 8F);
        this.PageInfo.ForeColor = System.Drawing.SystemColors.ControlText;
        this.PageInfo.Name = "PageInfo";
        // 
        // DataField
        // 
        this.DataField.BackColor = System.Drawing.Color.White;
        this.DataField.BorderColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.DataField.BorderWidth = 1F;
        this.DataField.Font = new System.Drawing.Font("Verdana", 8F);
        this.DataField.ForeColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Name = "DataField";
        this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        // 
        // TableCaption
        // 
        this.TableCaption.BackColor = System.Drawing.Color.White;
        this.TableCaption.BorderColor = System.Drawing.SystemColors.ControlText;
        this.TableCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TableCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.TableCaption.ForeColor = System.Drawing.SystemColors.ControlText;
        this.TableCaption.Name = "TableCaption";
        // 
        // TableDataField
        // 
        this.TableDataField.BackColor = System.Drawing.Color.White;
        this.TableDataField.BorderColor = System.Drawing.SystemColors.ControlText;
        this.TableDataField.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TableDataField.Font = new System.Drawing.Font("Verdana", 8F);
        this.TableDataField.ForeColor = System.Drawing.SystemColors.ControlText;
        this.TableDataField.Name = "TableDataField";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.picLogoEntidade,
            this.xrLabel35,
            this.xrLine1});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 222.8911F;
        this.PageHeader.Name = "PageHeader";
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(15.99998F, 0F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(410.45F, 198.43F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrPageInfo2});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 75F;
        this.PageFooter.Name = "PageFooter";
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.ReportFooter1,
            this.GroupHeader5});
        this.DetailReport.DataMember = "Usuario";
        this.DetailReport.DataSource = this.dsImpressaoTai_0081;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 2;
        this.DetailReport.Name = "DetailReport";
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
        this.xrTable2.BackColor = System.Drawing.Color.Transparent;
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrTable2.StyleName = "TableDataField";
        this.xrTable2.StylePriority.UseBackColor = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5679012345679012D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Usuario.NomeUsuario")});
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.Text = "xrTableCell1";
        this.xrTableCell1.Weight = 3.6990291262135919D;
        // 
        // ReportFooter1
        // 
        this.ReportFooter1.Dpi = 254F;
        this.ReportFooter1.HeightF = 45.07397F;
        this.ReportFooter1.Name = "ReportFooter1";
        // 
        // GroupHeader5
        // 
        this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
        this.GroupHeader5.Dpi = 254F;
        this.GroupHeader5.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader5.HeightF = 85.50009F;
        this.GroupHeader5.Name = "GroupHeader5";
        this.GroupHeader5.RepeatEveryPage = true;
        // 
        // xrLabel7
        // 
        this.xrLabel7.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35.50008F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(1906F, 50.00001F);
        this.xrLabel7.StyleName = "FieldCaption";
        this.xrLabel7.StylePriority.UseBackColor = false;
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UsePadding = false;
        this.xrLabel7.Text = "Equipe";
        // 
        // dsImpressaoTai_0081
        // 
        this.dsImpressaoTai_0081.DataSetName = "dsImpressaoTai_008";
        this.dsImpressaoTai_0081.EnforceConstraints = false;
        this.dsImpressaoTai_0081.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // DetailReport4
        // 
        this.DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.ReportFooter,
            this.GroupHeader6});
        this.DetailReport4.DataMember = "TermoAbertura04.TermoAbertura04_tai04_AcoesIniciativa1";
        this.DetailReport4.DataSource = this.dsImpressaoTai_0081;
        this.DetailReport4.Dpi = 254F;
        this.DetailReport4.Level = 0;
        this.DetailReport4.Name = "DetailReport4";
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.Detail5.Dpi = 254F;
        this.Detail5.HeightF = 63.5F;
        this.Detail5.Name = "Detail5";
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.Dpi = 254F;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(1906F, 63.5F);
        this.xrTable5.StylePriority.UseBorders = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell9,
            this.xrTableCell11,
            this.xrTableCell16,
            this.xrTableCell21});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 0.5679012345679012D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_AcoesIniciativa1.NomeAcao")});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 0.18997397061259552D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_AcoesIniciativa1.NomeUsuarioResponsavel")});
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 0.17817574576052453D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_AcoesIniciativa1.DataInicio", "{0:dd/MM/yyyy}")});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UsePadding = false;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.Weight = 0.17638474458171533D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_AcoesIniciativa1.DataTermino", "{0:dd/MM/yyyy}")});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UsePadding = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 0.17956601914023973D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_AcoesIniciativa1.ServicoCompartilhado")});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UsePadding = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 0.23523795698116981D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_AcoesIniciativa1.ValorPrevisto", "{0:c2}")});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.StylePriority.UsePadding = false;
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.Weight = 0.21296925523144722D;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 50.36565F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrTable6});
        this.GroupHeader6.Dpi = 254F;
        this.GroupHeader6.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader6.HeightF = 136.5F;
        this.GroupHeader6.Name = "GroupHeader6";
        this.GroupHeader6.RepeatEveryPage = true;
        // 
        // xrLabel8
        // 
        this.xrLabel8.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.5F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrLabel8.StyleName = "FieldCaption";
        this.xrLabel8.StylePriority.UseBackColor = false;
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UsePadding = false;
        this.xrLabel8.Text = "Ações/Sprint da Iniciativa";
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.Dpi = 254F;
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 72.50002F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable6.SizeF = new System.Drawing.SizeF(1906F, 63.5F);
        this.xrTable6.StylePriority.UseBorders = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.5679012345679012D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.Text = "Ação/Sprint";
        this.xrTableCell4.Weight = 0.18997397061259552D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.Text = "Responsável";
        this.xrTableCell8.Weight = 0.17817574576052453D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UsePadding = false;
        this.xrTableCell10.Text = "Início";
        this.xrTableCell10.Weight = 0.17638474458171533D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UsePadding = false;
        this.xrTableCell12.Text = "Término";
        this.xrTableCell12.Weight = 0.17956601914023973D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UsePadding = false;
        this.xrTableCell13.Text = "Serviços Corporativos";
        this.xrTableCell13.Weight = 0.23523795698116981D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UsePadding = false;
        this.xrTableCell14.Text = "Valor";
        this.xrTableCell14.Weight = 0.21296925523144722D;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.ReportFooter2,
            this.GroupHeader4});
        this.DetailReport1.DataMember = "TermoAbertura04.TermoAbertura04_tai04_ProdutoIntermediario";
        this.DetailReport1.DataSource = this.dsImpressaoTai_0081;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Expanded = false;
        this.DetailReport1.Level = 3;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 50F;
        this.Detail2.Name = "Detail2";
        // 
        // xrTable3
        // 
        this.xrTable3.BackColor = System.Drawing.Color.Transparent;
        this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable3.Dpi = 254F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrTable3.StyleName = "TableDataField";
        this.xrTable3.StylePriority.UseBackColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.5679012345679012D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ProdutoIntermediario.DescricaoProdutoInterm" +
                    "ediario")});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 3.6990291262135919D;
        // 
        // ReportFooter2
        // 
        this.ReportFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15});
        this.ReportFooter2.Dpi = 254F;
        this.ReportFooter2.HeightF = 44.0363F;
        this.ReportFooter2.Name = "ReportFooter2";
        this.ReportFooter2.PrintAtBottom = true;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(15.99998F, 0F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(254F, 35F);
        this.xrLabel15.StylePriority.UseForeColor = false;
        this.xrLabel15.Text = "xrLabel15";
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9});
        this.GroupHeader4.Dpi = 254F;
        this.GroupHeader4.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader4.HeightF = 69F;
        this.GroupHeader4.Name = "GroupHeader4";
        this.GroupHeader4.RepeatEveryPage = true;
        // 
        // xrLabel9
        // 
        this.xrLabel9.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 18.5F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrLabel9.StyleName = "FieldCaption";
        this.xrLabel9.StylePriority.UseBackColor = false;
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UsePadding = false;
        this.xrLabel9.Text = "Entrega(s) intermediária(s)";
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3});
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Level = 1;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.xrLabel10});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 148.1075F;
        this.Detail3.Name = "Detail3";
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.ValorEstimadoOrcamento", "{0:c2}")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 69.00002F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1905.999F, 58.41993F);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UsePadding = false;
        this.xrLabel11.Text = "xrLabel11";
        // 
        // xrLabel10
        // 
        this.xrLabel10.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 18.99997F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrLabel10.StyleName = "FieldCaption";
        this.xrLabel10.StylePriority.UseBackColor = false;
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UsePadding = false;
        this.xrLabel10.Text = "Valor Estimado do Orçamento (R$):";
        // 
        // DetailReport5
        // 
        this.DetailReport5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.ReportFooter3,
            this.GroupHeader3});
        this.DetailReport5.DataMember = "TermoAbertura04.TermoAbertura04_tai04_Premissa";
        this.DetailReport5.DataSource = this.dsImpressaoTai_0081;
        this.DetailReport5.Dpi = 254F;
        this.DetailReport5.Expanded = false;
        this.DetailReport5.Level = 5;
        this.DetailReport5.Name = "DetailReport5";
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.Detail6.Dpi = 254F;
        this.Detail6.HeightF = 63.5F;
        this.Detail6.Name = "Detail6";
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.Dpi = 254F;
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable7.SizeF = new System.Drawing.SizeF(1906F, 63.5F);
        this.xrTable7.StylePriority.UseBorders = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.5679012345679012D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_Premissa.DescricaoPremissa")});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UsePadding = false;
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.Weight = 0.64141414141414144D;
        // 
        // ReportFooter3
        // 
        this.ReportFooter3.Dpi = 254F;
        this.ReportFooter3.HeightF = 23.8125F;
        this.ReportFooter3.Name = "ReportFooter3";
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14});
        this.GroupHeader3.Dpi = 254F;
        this.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader3.HeightF = 50F;
        this.GroupHeader3.Name = "GroupHeader3";
        this.GroupHeader3.RepeatEveryPage = true;
        // 
        // xrLabel14
        // 
        this.xrLabel14.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrLabel14.StyleName = "FieldCaption";
        this.xrLabel14.StylePriority.UseBackColor = false;
        this.xrLabel14.StylePriority.UseBorders = false;
        this.xrLabel14.StylePriority.UsePadding = false;
        this.xrLabel14.Text = "Premissas";
        // 
        // DetailReport6
        // 
        this.DetailReport6.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.ReportFooter4,
            this.GroupHeader2});
        this.DetailReport6.DataMember = "TermoAbertura04.TermoAbertura04_tai04_Restricao";
        this.DetailReport6.DataSource = this.dsImpressaoTai_0081;
        this.DetailReport6.Dpi = 254F;
        this.DetailReport6.Expanded = false;
        this.DetailReport6.Level = 6;
        this.DetailReport6.Name = "DetailReport6";
        // 
        // Detail7
        // 
        this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.Detail7.Dpi = 254F;
        this.Detail7.HeightF = 63.5F;
        this.Detail7.Name = "Detail7";
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.Dpi = 254F;
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable8.SizeF = new System.Drawing.SizeF(1906F, 63.5F);
        this.xrTable8.StylePriority.UseBorders = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.5679012345679012D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_Restricao.DescricaoRestricao")});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UsePadding = false;
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.Weight = 0.64141414141414144D;
        // 
        // ReportFooter4
        // 
        this.ReportFooter4.Dpi = 254F;
        this.ReportFooter4.HeightF = 26.45833F;
        this.ReportFooter4.Name = "ReportFooter4";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader2.HeightF = 76F;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.RepeatEveryPage = true;
        // 
        // xrLabel18
        // 
        this.xrLabel18.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(5.00679E-06F, 26F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrLabel18.StyleName = "FieldCaption";
        this.xrLabel18.StylePriority.UseBackColor = false;
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UsePadding = false;
        this.xrLabel18.Text = "Restrições";
        // 
        // DetailReport7
        // 
        this.DetailReport7.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail8,
            this.ReportFooter5,
            this.GroupHeader1});
        this.DetailReport7.DataMember = "tai04_Parceiro";
        this.DetailReport7.DataSource = this.dsImpressaoTai_0081;
        this.DetailReport7.Dpi = 254F;
        this.DetailReport7.Expanded = false;
        this.DetailReport7.Level = 7;
        this.DetailReport7.Name = "DetailReport7";
        // 
        // Detail8
        // 
        this.Detail8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
        this.Detail8.Dpi = 254F;
        this.Detail8.HeightF = 63.5F;
        this.Detail8.Name = "Detail8";
        // 
        // xrTable12
        // 
        this.xrTable12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable12.Dpi = 254F;
        this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable12.Name = "xrTable12";
        this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
        this.xrTable12.SizeF = new System.Drawing.SizeF(1906F, 63.5F);
        this.xrTable12.StylePriority.UseBorders = false;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 0.5679012345679012D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tai04_Parceiro.DescricaoParceiro")});
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.StylePriority.UsePadding = false;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.Weight = 0.64141414141414144D;
        // 
        // ReportFooter5
        // 
        this.ReportFooter5.Dpi = 254F;
        this.ReportFooter5.HeightF = 26.45833F;
        this.ReportFooter5.Name = "ReportFooter5";
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 75.99998F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // xrLabel17
        // 
        this.xrLabel17.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel17.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.99999F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrLabel17.StyleName = "FieldCaption";
        this.xrLabel17.StylePriority.UseBackColor = false;
        this.xrLabel17.StylePriority.UseBorders = false;
        this.xrLabel17.StylePriority.UsePadding = false;
        this.xrLabel17.Text = "Parcerias";
        // 
        // DetailReport8
        // 
        this.DetailReport8.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail9,
            this.GroupHeader7,
            this.ReportFooter6});
        this.DetailReport8.DataMember = "TermoAbertura04.TermoAbertura04_tai04_ProdutoFinal";
        this.DetailReport8.DataSource = this.dsImpressaoTai_0081;
        this.DetailReport8.Dpi = 254F;
        this.DetailReport8.Level = 4;
        this.DetailReport8.Name = "DetailReport8";
        // 
        // Detail9
        // 
        this.Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail9.Dpi = 254F;
        this.Detail9.HeightF = 63.5F;
        this.Detail9.Name = "Detail9";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1906F, 63.5F);
        this.xrTable1.StylePriority.UseBorders = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura04.TermoAbertura04_tai04_ProdutoFinal.DescricaoProdutoFinal")});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.Weight = 0.64141414141414144D;
        // 
        // GroupHeader7
        // 
        this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel16});
        this.GroupHeader7.Dpi = 254F;
        this.GroupHeader7.HeightF = 62.04158F;
        this.GroupHeader7.Name = "GroupHeader7";
        // 
        // xrLabel16
        // 
        this.xrLabel16.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12.04158F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(1906F, 50F);
        this.xrLabel16.StyleName = "FieldCaption";
        this.xrLabel16.StylePriority.UseBackColor = false;
        this.xrLabel16.StylePriority.UseBorders = false;
        this.xrLabel16.StylePriority.UsePadding = false;
        this.xrLabel16.Text = "Entregas(s) Final(is)";
        // 
        // ReportFooter6
        // 
        this.ReportFooter6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12});
        this.ReportFooter6.Dpi = 254F;
        this.ReportFooter6.HeightF = 58.20833F;
        this.ReportFooter6.Name = "ReportFooter6";
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.ForeColor = System.Drawing.Color.Transparent;
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(2.000039F, 11.60427F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(254F, 35F);
        this.xrLabel12.StylePriority.UseForeColor = false;
        this.xrLabel12.Text = "xrLabel15";
        // 
        // relImpressaoTai_008
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport,
            this.DetailReport4,
            this.DetailReport1,
            this.DetailReport2,
            this.DetailReport5,
            this.DetailReport6,
            this.DetailReport7,
            this.DetailReport8});
        this.DataMember = "TermoAbertura04";
        this.DataSource = this.dsImpressaoTai_0081;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Margins = new System.Drawing.Printing.Margins(150, 0, 63, 0);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pTitulo});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField,
            this.TableCaption,
            this.TableDataField});
        this.Version = "17.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsImpressaoTai_0081)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
