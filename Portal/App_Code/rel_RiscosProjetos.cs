using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Web.Hosting;

/// <summary>
/// Summary description for rel_RiscosProjetos
/// </summary>
public class rel_RiscosProjetos : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    dados cDados;

    private XRLabel lblRiscoQuestao;
    private XRLabel lblNomeProjeto;
    private XRTable tbRiscoQuestao;
    private XRTableRow xrTableRow1;
    private XRTableCell celNomeRiscoQuestao;
    private XRTableRow xrTableRow2;
    private XRTableCell celNomeRisco;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell2;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow5;
    private XRTableCell celDetalhes;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell4;
    private XRTableRow xrTableRow7;
    private XRTableCell celSeveridadeHeader;
    private XRTableCell celProbabilidadeHeader;
    private XRTableRow xrTableRow8;
    private XRTableCell celProbabilidade;
    private XRTableCell celSeveridade;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell8;
    private XRTableCell celImpactoHeader;
    private XRTableCell celImpacto;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow11;
    private XRTableCell celResponsavel;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell10;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell121;
    private XRTableRow xrTableRow14;
    private XRTableCell celConsequencias;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell11;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow17;
    private XRTableCell celEstrategiaTratamento;
    private XRTable tbTarefas;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell15;

    private DevExpress.XtraReports.Parameters.Parameter pCodigoRiscoQuestaoSelecionada = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoTipoAssociacao = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pPathLogo = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pVetorBytes = new DevExpress.XtraReports.Parameters.Parameter();
    /// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;
    private XRPictureBox logoUnidade;
    private XRLine xrLine1;
    private XRPictureBox imgSeveridade;
    PaddingInfo recuo;
    public rel_RiscosProjetos(int codigoEntidade)
    {
        InitializeComponent();
        cDados = CdadosUtil.GetCdados(codigoEntidade, null);
        recuo = new PaddingInfo(3, 3, 0, 0);

        pCodigoRiscoQuestaoSelecionada.Name = "pCodigoRiscoQuestaoSelecionada";
        pCodigoTipoAssociacao.Name = "pCodigoTipoAssociacao";
        pPathLogo.Name = "pPathLogo";
        pVetorBytes.Name = "pVetorBytes";
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
       {
            this.pCodigoRiscoQuestaoSelecionada,
            this.pCodigoTipoAssociacao,
            this.pPathLogo,
            this.pVetorBytes
       });
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
        //string resourceFileName = "rel_RiscosProjetos.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.tbRiscoQuestao = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celNomeRiscoQuestao = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celNomeRisco = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celDetalhes = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celProbabilidadeHeader = new DevExpress.XtraReports.UI.XRTableCell();
        this.celImpactoHeader = new DevExpress.XtraReports.UI.XRTableCell();
        this.celSeveridadeHeader = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celProbabilidade = new DevExpress.XtraReports.UI.XRTableCell();
        this.celImpacto = new DevExpress.XtraReports.UI.XRTableCell();
        this.celSeveridade = new DevExpress.XtraReports.UI.XRTableCell();
        this.imgSeveridade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celResponsavel = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celConsequencias = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celEstrategiaTratamento = new DevExpress.XtraReports.UI.XRTableCell();
        this.tbTarefas = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblRiscoQuestao = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.logoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        ((System.ComponentModel.ISupportInitialize)(this.tbRiscoQuestao)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbTarefas)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tbRiscoQuestao,
            this.tbTarefas});
        this.Detail.Height = 517;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // tbRiscoQuestao
        // 
        this.tbRiscoQuestao.Location = new System.Drawing.Point(8, 8);
        this.tbRiscoQuestao.Name = "tbRiscoQuestao";
        this.tbRiscoQuestao.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow10,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow15,
            this.xrTableRow16,
            this.xrTableRow17});
        this.tbRiscoQuestao.Size = new System.Drawing.Size(775, 450);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celNomeRiscoQuestao});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // celNomeRiscoQuestao
        // 
        this.celNomeRiscoQuestao.BackColor = System.Drawing.Color.WhiteSmoke;
        this.celNomeRiscoQuestao.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celNomeRiscoQuestao.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.celNomeRiscoQuestao.Name = "celNomeRiscoQuestao";
        this.celNomeRiscoQuestao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celNomeRiscoQuestao.StylePriority.UseBackColor = false;
        this.celNomeRiscoQuestao.StylePriority.UseBorders = false;
        this.celNomeRiscoQuestao.StylePriority.UseFont = false;
        this.celNomeRiscoQuestao.StylePriority.UsePadding = false;
        this.celNomeRiscoQuestao.Text = Resources.traducao.risco;
        this.celNomeRiscoQuestao.Weight = 1;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celNomeRisco});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // celNomeRisco
        // 
        this.celNomeRisco.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celNomeRisco.Name = "celNomeRisco";
        this.celNomeRisco.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celNomeRisco.StylePriority.UseBorders = false;
        this.celNomeRisco.StylePriority.UsePadding = false;
        this.celNomeRisco.StylePriority.UseTextAlignment = false;
        this.celNomeRisco.Text = Resources.traducao.nome_do_risco;
        this.celNomeRisco.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celNomeRisco.Weight = 1;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Weight = 1;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell3.StylePriority.UseBackColor = false;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = Resources.traducao.detalhes;
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell3.Weight = 1;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celDetalhes});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 2.0000000000000004;
        // 
        // celDetalhes
        // 
        this.celDetalhes.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celDetalhes.Name = "celDetalhes";
        this.celDetalhes.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celDetalhes.StylePriority.UseBorders = false;
        this.celDetalhes.StylePriority.UsePadding = false;
        this.celDetalhes.Text = Resources.traducao.detalhes;
        this.celDetalhes.Weight = 1;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.99999999999999967;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Weight = 1;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celProbabilidadeHeader,
            this.celImpactoHeader,
            this.celSeveridadeHeader});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.99999999999999967;
        // 
        // celProbabilidadeHeader
        // 
        this.celProbabilidadeHeader.BackColor = System.Drawing.Color.WhiteSmoke;
        this.celProbabilidadeHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celProbabilidadeHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.celProbabilidadeHeader.Name = "celProbabilidadeHeader";
        this.celProbabilidadeHeader.StylePriority.UseBackColor = false;
        this.celProbabilidadeHeader.StylePriority.UseBorders = false;
        this.celProbabilidadeHeader.StylePriority.UseFont = false;
        this.celProbabilidadeHeader.StylePriority.UseTextAlignment = false;
        this.celProbabilidadeHeader.Text = Resources.traducao.probabilidade;
        this.celProbabilidadeHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celProbabilidadeHeader.Weight = 0.31193548387096776;
        // 
        // celImpactoHeader
        // 
        this.celImpactoHeader.BackColor = System.Drawing.Color.WhiteSmoke;
        this.celImpactoHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celImpactoHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.celImpactoHeader.Name = "celImpactoHeader";
        this.celImpactoHeader.StylePriority.UseBackColor = false;
        this.celImpactoHeader.StylePriority.UseBorders = false;
        this.celImpactoHeader.StylePriority.UseFont = false;
        this.celImpactoHeader.StylePriority.UseTextAlignment = false;
        this.celImpactoHeader.Text = Resources.traducao.impacto;
        this.celImpactoHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celImpactoHeader.Weight = 0.33258064516129032;
        // 
        // celSeveridadeHeader
        // 
        this.celSeveridadeHeader.BackColor = System.Drawing.Color.WhiteSmoke;
        this.celSeveridadeHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celSeveridadeHeader.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.celSeveridadeHeader.Name = "celSeveridadeHeader";
        this.celSeveridadeHeader.StylePriority.UseBackColor = false;
        this.celSeveridadeHeader.StylePriority.UseBorders = false;
        this.celSeveridadeHeader.StylePriority.UseFont = false;
        this.celSeveridadeHeader.StylePriority.UseTextAlignment = false;
        this.celSeveridadeHeader.Text = Resources.traducao.severidade;
        this.celSeveridadeHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celSeveridadeHeader.Weight = 0.35548387096774192;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celProbabilidade,
            this.celImpacto,
            this.celSeveridade});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.99999999999999967;
        // 
        // celProbabilidade
        // 
        this.celProbabilidade.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celProbabilidade.Name = "celProbabilidade";
        this.celProbabilidade.StylePriority.UseBorders = false;
        this.celProbabilidade.StylePriority.UseTextAlignment = false;
        this.celProbabilidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celProbabilidade.Weight = 0.31193548387096776;
        // 
        // celImpacto
        // 
        this.celImpacto.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celImpacto.Name = "celImpacto";
        this.celImpacto.StylePriority.UseBorders = false;
        this.celImpacto.StylePriority.UseTextAlignment = false;
        this.celImpacto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celImpacto.Weight = 0.33258064516129032;
        // 
        // celSeveridade
        // 
        this.celSeveridade.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celSeveridade.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgSeveridade});
        this.celSeveridade.Name = "celSeveridade";
        this.celSeveridade.StylePriority.UseBorders = false;
        this.celSeveridade.StylePriority.UseTextAlignment = false;
        this.celSeveridade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celSeveridade.Weight = 0.35548387096774192;
        // 
        // imgSeveridade
        // 
        this.imgSeveridade.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgSeveridade.Location = new System.Drawing.Point(125, 0);
        this.imgSeveridade.Name = "imgSeveridade";
        this.imgSeveridade.Size = new System.Drawing.Size(25, 20);
        this.imgSeveridade.StylePriority.UseBorders = false;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8});
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 0.99999999999999967;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Weight = 1;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 0.99999999999999967;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell9.StylePriority.UseBackColor = false;
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UsePadding = false;
        this.xrTableCell9.Text = Resources.traducao.respons_vel;
        this.xrTableCell9.Weight = 1;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celResponsavel});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 0.99999999999999967;
        // 
        // celResponsavel
        // 
        this.celResponsavel.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celResponsavel.Name = "celResponsavel";
        this.celResponsavel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celResponsavel.StylePriority.UseBorders = false;
        this.celResponsavel.StylePriority.UsePadding = false;
        this.celResponsavel.Weight = 1;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 0.99999999999999967;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Weight = 1;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell121});
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 0.99999999999999967;
        // 
        // xrTableCell121
        // 
        this.xrTableCell121.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell121.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell121.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell121.Name = "xrTableCell121";
        this.xrTableCell121.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell121.StylePriority.UseBackColor = false;
        this.xrTableCell121.StylePriority.UseBorders = false;
        this.xrTableCell121.StylePriority.UseFont = false;
        this.xrTableCell121.StylePriority.UsePadding = false;
        this.xrTableCell121.Text = Resources.traducao.consequ_ncias;
        this.xrTableCell121.Weight = 1;
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celConsequencias});
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 0.99999999999999967;
        // 
        // celConsequencias
        // 
        this.celConsequencias.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celConsequencias.Name = "celConsequencias";
        this.celConsequencias.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celConsequencias.StylePriority.UseBorders = false;
        this.celConsequencias.StylePriority.UsePadding = false;
        this.celConsequencias.Text = "celConsequencias";
        this.celConsequencias.Weight = 1;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 0.99999999999999967;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Weight = 1;
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12});
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 0.99999999999999967;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell12.StylePriority.UseBackColor = false;
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.StylePriority.UsePadding = false;
        this.xrTableCell12.Text = Resources.traducao.estrat_gia_para_tratamento;
        this.xrTableCell12.Weight = 1;
        // 
        // xrTableRow17
        // 
        this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celEstrategiaTratamento});
        this.xrTableRow17.Name = "xrTableRow17";
        this.xrTableRow17.Weight = 0.99999999999999967;
        // 
        // celEstrategiaTratamento
        // 
        this.celEstrategiaTratamento.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celEstrategiaTratamento.Name = "celEstrategiaTratamento";
        this.celEstrategiaTratamento.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celEstrategiaTratamento.StylePriority.UseBorders = false;
        this.celEstrategiaTratamento.StylePriority.UsePadding = false;
        this.celEstrategiaTratamento.Weight = 1;
        // 
        // tbTarefas
        // 
        this.tbTarefas.Location = new System.Drawing.Point(8, 483);
        this.tbTarefas.Name = "tbTarefas";
        this.tbTarefas.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20});
        this.tbTarefas.Size = new System.Drawing.Size(775, 25);
        // 
        // xrTableRow20
        // 
        this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15});
        this.xrTableRow20.Name = "xrTableRow20";
        this.xrTableRow20.Weight = 1;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Weight = 3;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblRiscoQuestao,
            this.lblNomeProjeto,
            this.logoUnidade,
            this.xrLine1});
        this.PageHeader.Height = 99;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblRiscoQuestao
        // 
        this.lblRiscoQuestao.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.lblRiscoQuestao.Location = new System.Drawing.Point(308, 17);
        this.lblRiscoQuestao.Name = "lblRiscoQuestao";
        this.lblRiscoQuestao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblRiscoQuestao.Size = new System.Drawing.Size(475, 25);
        this.lblRiscoQuestao.StylePriority.UseFont = false;
        this.lblRiscoQuestao.Text = Resources.traducao.risco_do_projeto;
        // 
        // lblNomeProjeto
        // 
        this.lblNomeProjeto.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblNomeProjeto.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblNomeProjeto.Location = new System.Drawing.Point(308, 50);
        this.lblNomeProjeto.Name = "lblNomeProjeto";
        this.lblNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeProjeto.Size = new System.Drawing.Size(475, 25);
        this.lblNomeProjeto.StylePriority.UseBorders = false;
        this.lblNomeProjeto.StylePriority.UseFont = false;
        this.lblNomeProjeto.StylePriority.UseTextAlignment = false;
        this.lblNomeProjeto.Text = Resources.traducao.nome_do_projeto;
        this.lblNomeProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // logoUnidade
        // 
        this.logoUnidade.Location = new System.Drawing.Point(8, 8);
        this.logoUnidade.Name = "logoUnidade";
        this.logoUnidade.Size = new System.Drawing.Size(292, 67);
        this.logoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLine1
        // 
        this.xrLine1.Location = new System.Drawing.Point(8, 83);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.Size = new System.Drawing.Size(775, 8);
        // 
        // rel_RiscosProjetos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader});
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.Version = "9.2";
        ((System.ComponentModel.ISupportInitialize)(this.tbRiscoQuestao)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbTarefas)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataSet dsInformacoesRisco;
        DataSet dsTarefasDoRisco;
        XRTableRow linha = new XRTableRow();

        // buscar as informações do risco selecionado - Master
        string labelQuestao = "Questão";
        string labelQuestoes = "Questões";
        string genero = "F";
        DataSet dsQuestoes = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");
        if (cDados.DataSetOk(dsQuestoes) && cDados.DataTableOk(dsQuestoes.Tables[0]))
        {
            labelQuestao = dsQuestoes.Tables[0].Rows[0]["labelQuestao"] + "";
            labelQuestoes = dsQuestoes.Tables[0].Rows[0]["labelQuestoes"] + "";
            genero = dsQuestoes.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
        }

        dsInformacoesRisco = cDados.getRiscoQuestao(int.Parse(pCodigoRiscoQuestaoSelecionada.Value.ToString()));
        string CSRQ = "";
        if (cDados.DataSetOk(dsInformacoesRisco) && cDados.DataTableOk(dsInformacoesRisco.Tables[0]))
        {
            CSRQ = dsInformacoesRisco.Tables[0].Rows[0]["CodigoStatusRiscoQuestao"].ToString().Substring(0, 1);
            if (CSRQ == "Q")
            {
                lblRiscoQuestao.Text = string.Format(@"{0} " + Resources.traducao.do_projeto, labelQuestoes);

                celProbabilidadeHeader.Text = Resources.traducao.urg_ncia;

                celImpactoHeader.Text = Resources.traducao.prioridade;

                celNomeRiscoQuestao.Text = labelQuestao;
            }

            int valorProbabilidade = int.Parse(dsInformacoesRisco.Tables[0].Rows[0]["ProbabilidadePrioridade"].ToString());
            int valorImpacto = int.Parse(dsInformacoesRisco.Tables[0].Rows[0]["ImpactoUrgencia"].ToString());
            //probabilidade urgencia

            if (valorProbabilidade == 1)
            {
                celProbabilidade.Text = Resources.traducao.baixa;
            }
            if (valorProbabilidade == 2)
            {
                celProbabilidade.Text = Resources.traducao.m_dia;
            }
            if (valorProbabilidade == 3)
            {
                celProbabilidade.Text = Resources.traducao.alta;
            }

            //impacto ou prioridade
            if (valorImpacto == 1)
            {
                celImpacto.Text = (CSRQ == "R") ? Resources.traducao.baixo : Resources.traducao.baixa;
            }
            if (valorImpacto == 2)
            {
                celImpacto.Text = (CSRQ == "R") ? Resources.traducao.m_dio : Resources.traducao.m_dia;
            }
            if (valorImpacto == 3)
            {
                celImpacto.Text = (CSRQ == "R") ? Resources.traducao.alto : Resources.traducao.alta;
            }


            lblNomeProjeto.Text = dsInformacoesRisco.Tables[0].Rows[0]["NomeProjeto"].ToString();
            celNomeRisco.Text = dsInformacoesRisco.Tables[0].Rows[0]["DescricaoRiscoQuestao"].ToString();
            celDetalhes.Text = dsInformacoesRisco.Tables[0].Rows[0]["DetalheRiscoQuestao"].ToString();


            string cor = dsInformacoesRisco.Tables[0].Rows[0]["CorRiscoQuestao"].ToString();
            if (cor == "Amarelo")
            {
                imgSeveridade.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/amarelo.gif";
            }
            if (cor == "Vermelho")
            {
                imgSeveridade.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/vermelho.gif";
            }
            if (cor == "Verde")
            {
                imgSeveridade.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/verde.gif";
            }

            celResponsavel.Text = dsInformacoesRisco.Tables[0].Rows[0]["NomeUsuarioResponsavel"].ToString();
            celConsequencias.Text = dsInformacoesRisco.Tables[0].Rows[0]["ConsequenciaRiscoQuestao"].ToString();
            celEstrategiaTratamento.Text = dsInformacoesRisco.Tables[0].Rows[0]["TratamentoRiscoQuestao"].ToString();

            string descUltimoCom = "";
            string dataComentario = "";
            object dataComentarioTraduzida;
            string nomeUsuarioComent = "";


            tbRiscoQuestao.Rows.Add(getRow(20, recuo, ""));

            if (dsInformacoesRisco.Tables[0].Rows[0]["DescricaoComentario"].ToString() != "")
            {
                descUltimoCom = dsInformacoesRisco.Tables[0].Rows[0]["DescricaoComentario"].ToString();
                dataComentario = dsInformacoesRisco.Tables[0].Rows[0]["DataComentario"].ToString();
                dataComentarioTraduzida = dsInformacoesRisco.Tables[0].Rows[0]["DataComentarioData"];
                nomeUsuarioComent = dsInformacoesRisco.Tables[0].Rows[0]["NomeUsuarioComentario"].ToString();

                XRTableRow linhaCabecalho = getRow(20, recuo, Resources.traducao.data_de_inclus_o_do__ltimo_coment_rio + ": " + ((DateTime)dataComentarioTraduzida).ToString(Resources.traducao.geral_formato_data_csharp), Resources.traducao.nome_do_usu_rio.Substring(0, 1).ToUpper() + Resources.traducao.nome_do_usu_rio.Substring(1).ToLower() + ": " + nomeUsuarioComent);
                XRTableRow linhaDescricaoComentario = getRow(20, recuo, descUltimoCom);

                FontStyle negrito = FontStyle.Bold;
                linhaCabecalho.BackColor = Color.WhiteSmoke;
                linhaCabecalho.Borders = BorderSide.All;

                Font fonteNegrito =
                linhaCabecalho.Font = new Font("Verdana", 8f, negrito);

                linhaDescricaoComentario.Borders = BorderSide.Bottom | BorderSide.Right | BorderSide.Left;

                tbRiscoQuestao.Rows.Add(linhaCabecalho);
                tbRiscoQuestao.Rows.Add(linhaDescricaoComentario);
            }
            else
            {
                XRTableRow linhaSemComentario = new XRTableRow();
                linhaSemComentario = getRow(20, recuo, Resources.traducao.n_o_h__coment_rios_para_serem_exibidos);
                linhaSemComentario.Font = new Font("Verdana", 8.0f, FontStyle.Bold);

                linhaSemComentario.Borders = BorderSide.All;

                tbTarefas.Rows.Clear();
                tbTarefas.Rows.Add(linhaSemComentario);
                tbTarefas.Rows.Add(getRow(20, recuo, ""));
            }




            //celUltimoComentario.Text = ""

        }

        FontStyle fonteBold = FontStyle.Bold;
        FontStyle fonteNormal = FontStyle.Regular;

        Font fnegrito = new Font(this.Font.FontFamily.Name, this.Font.SizeInPoints, fonteBold);

        Font fcomum = new Font(this.Font.FontFamily.Name, this.Font.SizeInPoints, fonteNormal);
        // buscar as tarefas do risco selecionado
        dsTarefasDoRisco = cDados.getTarefasRiscoQuestoes(int.Parse(pCodigoTipoAssociacao.Value.ToString()), int.Parse(pCodigoRiscoQuestaoSelecionada.Value.ToString()));
        if (cDados.DataSetOk(dsTarefasDoRisco) && cDados.DataTableOk(dsTarefasDoRisco.Tables[0]))
        {


            linha.Cells.Clear();
            linha = getRow(20, recuo, Resources.traducao.tarefa, Resources.traducao.inicio, Resources.traducao.termino, Resources.traducao.status, Resources.traducao.respons_vel);
            linha.Borders = BorderSide.All;


            linha.Font = fnegrito;

            linha.BackColor = Color.WhiteSmoke;

            tbTarefas.Rows.Add(linha);

            for (int i = 0; i < dsTarefasDoRisco.Tables[0].Rows.Count; i++)
            {
                string tarefa = dsTarefasDoRisco.Tables[0].Rows[i]["DescricaoTarefa"].ToString();
                string inicio = string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", dsTarefasDoRisco.Tables[0].Rows[i]["InicioPrevisto"]);
                string termino = string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", dsTarefasDoRisco.Tables[0].Rows[i]["TerminoPrevisto"]);
                string status = dsTarefasDoRisco.Tables[0].Rows[i]["DescricaoStatusTarefa"].ToString();
                string responsavel = dsTarefasDoRisco.Tables[0].Rows[i]["NomeUsuarioResponsavelTarefa"].ToString();
                linha = getRow(20, recuo, tarefa, inicio, termino, status, responsavel);
                linha.Borders = BorderSide.All;
                tbTarefas.Rows.Add(linha);
                tbTarefas.Height += 20;
            }
            for (int j = 0; j < tbTarefas.Rows.Count; j++)
            {
                if (tbTarefas.Rows[j].Cells.Count == 5)
                {
                    tbTarefas.Rows[j].Cells[0].Width = 260;//tarefa
                    tbTarefas.Rows[j].Cells[1].Width = 90;//inicio
                    tbTarefas.Rows[j].Cells[2].Width = 90;//termino
                    tbTarefas.Rows[j].Cells[3].Width = 110;//status -20
                    tbTarefas.Rows[j].Cells[4].Width = 220;//responsavel
                }
            }
        }
        else
        {
            linha.Cells.Clear();
            string aviso = (CSRQ == "R") ? Resources.traducao.o_risco_selecionado_n_o_possui_tarefas : string.Format(@"{0} {2} {1} " + Resources.traducao.n_o_possui_tarefas, (genero == "M" ? Resources.traducao.artigo_o : Resources.traducao.artigo_a), (genero == "M" ? Resources.traducao.selecionado_minusculas : Resources.traducao.selecionada_minusculas), labelQuestao);
            linha.Font = fnegrito;
            linha.Borders = BorderSide.All;
            linha = getRow(20, recuo, aviso);
            tbTarefas.Rows.Add(linha);
        }
    }

    private XRTableRow getRow(int alturaLinha, PaddingInfo recuo, params object[] conteudoColunas)
    {
        if (conteudoColunas.Length <= 0)
            return null;
        FontStyle fonteBold = FontStyle.Bold;
        FontStyle fonteNormal = FontStyle.Regular;

        Font fnegrito = new Font(this.Font.FontFamily.Name, this.Font.SizeInPoints + 1, fonteBold);

        Font fcomum = new Font(this.Font.FontFamily.Name, this.Font.SizeInPoints + 1, fonteNormal);

        XRTableRow row = new XRTableRow();

        row.Height = alturaLinha;
        foreach (object conteudoColuna in conteudoColunas)
        {
            XRTableCell celula = new XRTableCell();
            celula.Font = fcomum;
            celula.Text = conteudoColuna.ToString();
            celula.Padding = recuo;
            row.Cells.Add(celula);
        }
        return row;
    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        logoUnidade.ImageUrl = pPathLogo.Value.ToString();
    }
}
