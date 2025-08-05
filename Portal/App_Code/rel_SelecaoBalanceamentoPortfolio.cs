using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_SelecaoBalanceamentoPortfolio
/// </summary>
public class rel_SelecaoBalanceamentoPortfolio : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

    private DevExpress.XtraReports.Parameters.Parameter ppathLogo = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pVetBytes = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoPortfolio = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoEntidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeUsuario = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomePortfolio = new DevExpress.XtraReports.Parameters.Parameter();

    private DevExpress.XtraReports.Parameters.Parameter pAno = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCaminhoArquivo = new DevExpress.XtraReports.Parameters.Parameter();
    private dados cDados;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRLabel lblDescricaoPortfolio;
    private XRPictureBox imgEntidade;
    private XRTable tbCenarios;
    private XRTableRow xrTableRow2;
    private XRTableCell cellItem_tbCenarios;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell1;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell10;
    private FormattingRule formattingRule1;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell17;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell21;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell25;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell29;
    private XRLine xrLine1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLine xrLine2;
    private XRLabel xrLabel3;
    private XRTable tbProjetosCenarios;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRPageInfo xrPageInfo1;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRLabel lblDataGeracaoRelatorio;

    public rel_SelecaoBalanceamentoPortfolio(int codigoEntidade)
    {
        InitializeComponent();

        cDados = CdadosUtil.GetCdados(codigoEntidade, null);
        //
        // TODO: Add constructor logic here
        //
        pCodigoPortfolio.Name = "pcodigoPortfolio";
        pAno.Name = "pAno";
        pCaminhoArquivo.Name = "pCaminhoArquivo";
        pCodigoEntidade.Name = "pCodigoEntidade";
        pNomeUsuario.Name = "pNomeUsuarioLogado";
        pNomePortfolio.Name = "pNomePortfolio";
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
        {
            this.pCodigoPortfolio,
            this.pAno,
            this.pCaminhoArquivo,
            this.pCodigoEntidade,
            this.pNomeUsuario,
            this.pNomePortfolio
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
        //string resourceFileName = "rel_SelecaoBalanceamentoPortfolio.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.tbCenarios = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.cellItem_tbCenarios = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.tbProjetosCenarios = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblDescricaoPortfolio = new DevExpress.XtraReports.UI.XRLabel();
        this.imgEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.lblDataGeracaoRelatorio = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this.tbCenarios)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbProjetosCenarios)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tbCenarios,
            this.xrLabel2,
            this.xrLabel3,
            this.tbProjetosCenarios});
        this.Detail.HeightF = 283F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // tbCenarios
        // 
        this.tbCenarios.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.tbCenarios.LocationFloat = new DevExpress.Utils.PointFloat(7.999992F, 42.00001F);
        this.tbCenarios.Name = "tbCenarios";
        this.tbCenarios.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow8});
        this.tbCenarios.SizeF = new System.Drawing.SizeF(120F, 175F);
        this.tbCenarios.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellItem_tbCenarios});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // cellItem_tbCenarios
        // 
        this.cellItem_tbCenarios.BackColor = System.Drawing.Color.WhiteSmoke;
        this.cellItem_tbCenarios.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.cellItem_tbCenarios.Name = "cellItem_tbCenarios";
        this.cellItem_tbCenarios.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.cellItem_tbCenarios.StylePriority.UseBackColor = false;
        this.cellItem_tbCenarios.StylePriority.UseFont = false;
        this.cellItem_tbCenarios.StylePriority.UsePadding = false;
        this.cellItem_tbCenarios.StylePriority.UseTextAlignment = false;
        this.cellItem_tbCenarios.Text = "Item";
        this.cellItem_tbCenarios.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.cellItem_tbCenarios.Weight = 0.12790862354657334;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Despesa (R$)";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell1.Weight = 0.12790862354657334;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell10.StylePriority.UseBackColor = false;
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UsePadding = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Receita (R$)";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell10.Weight = 0.12790862354657334;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell17.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell17.StylePriority.UseBackColor = false;
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UsePadding = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "Recursos (h)";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell17.Weight = 0.12790862354657334;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell21.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell21.StylePriority.UseBackColor = false;
        this.xrTableCell21.StylePriority.UseFont = false;
        this.xrTableCell21.StylePriority.UsePadding = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "Score";
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell21.Weight = 0.12790862354657334;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell25.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell25.StylePriority.UseBackColor = false;
        this.xrTableCell25.StylePriority.UseFont = false;
        this.xrTableCell25.StylePriority.UsePadding = false;
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.Text = "Riscos";
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell25.Weight = 0.12790862354657334;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell29.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell29.StylePriority.UseBackColor = false;
        this.xrTableCell29.StylePriority.UseFont = false;
        this.xrTableCell29.StylePriority.UsePadding = false;
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.Text = "Projetos";
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell29.Weight = 0.12790862354657334;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(8F, 8F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(642F, 25F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "Comparação Entre Cenários";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(8F, 233F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(642F, 25F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Projetos por cenário";
        // 
        // tbProjetosCenarios
        // 
        this.tbProjetosCenarios.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.tbProjetosCenarios.LocationFloat = new DevExpress.Utils.PointFloat(8F, 258F);
        this.tbProjetosCenarios.Name = "tbProjetosCenarios";
        this.tbProjetosCenarios.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.tbProjetosCenarios.SizeF = new System.Drawing.SizeF(1100F, 25F);
        this.tbProjetosCenarios.StylePriority.UseBorders = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell5,
            this.xrTableCell13,
            this.xrTableCell9,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16});
        this.xrTableRow1.KeepTogether = false;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Ranking";
        this.xrTableCell3.Weight = 0.15252272727272725;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "categoria";
        this.xrTableCell11.Weight = 0.37446074380165295;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "Projeto";
        this.xrTableCell12.Weight = 0.78378719008264486;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "SCOREriscos";
        this.xrTableCell5.Weight = 0.21796487603305781;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "Riscos";
        this.xrTableCell13.Weight = 0.1717190082644629;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "Despesa";
        this.xrTableCell9.Weight = 0.27254973558297657;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "Receita";
        this.xrTableCell14.Weight = 0.2790377681371764;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "RH";
        this.xrTableCell15.Weight = 0.30821141342917974;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "Status";
        this.xrTableCell16.Weight = 0.32383744648703094;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDescricaoPortfolio,
            this.imgEntidade,
            this.xrLine1,
            this.xrLabel1});
        this.PageHeader.HeightF = 105F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.StylePriority.UseBorders = false;
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblDescricaoPortfolio
        // 
        this.lblDescricaoPortfolio.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.lblDescricaoPortfolio.LocationFloat = new DevExpress.Utils.PointFloat(208F, 42F);
        this.lblDescricaoPortfolio.Name = "lblDescricaoPortfolio";
        this.lblDescricaoPortfolio.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDescricaoPortfolio.SizeF = new System.Drawing.SizeF(467F, 25F);
        this.lblDescricaoPortfolio.StylePriority.UseFont = false;
        // 
        // imgEntidade
        // 
        this.imgEntidade.ImageUrl = pCaminhoArquivo.Value.ToString();
        this.imgEntidade.LocationFloat = new DevExpress.Utils.PointFloat(8F, 0F);
        this.imgEntidade.Name = "imgEntidade";
        this.imgEntidade.SizeF = new System.Drawing.SizeF(192F, 84F);
        this.imgEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLine1
        // 
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(8F, 75F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1100F, 25F);
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(208F, 8F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(642F, 25F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Seleção e Balanceamento de Portfólio";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDataGeracaoRelatorio,
            this.xrLine2,
            this.xrPageInfo1});
        this.PageFooter.HeightF = 79F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.StylePriority.UseBorders = false;
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageFooter_BeforePrint);
        // 
        // lblDataGeracaoRelatorio
        // 
        this.lblDataGeracaoRelatorio.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblDataGeracaoRelatorio.LocationFloat = new DevExpress.Utils.PointFloat(8F, 8F);
        this.lblDataGeracaoRelatorio.Name = "lblDataGeracaoRelatorio";
        this.lblDataGeracaoRelatorio.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDataGeracaoRelatorio.SizeF = new System.Drawing.SizeF(1092F, 25F);
        this.lblDataGeracaoRelatorio.StylePriority.UseBorders = false;
        this.lblDataGeracaoRelatorio.StylePriority.UseTextAlignment = false;
        this.lblDataGeracaoRelatorio.Text = "lblDataGeracaoRelatorio";
        this.lblDataGeracaoRelatorio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLine2
        // 
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(8F, 0F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1092F, 2F);
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1050F, 33F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(50F, 25F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // formattingRule1
        // 
        this.formattingRule1.Name = "formattingRule1";
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.HeightF = 30F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.HeightF = 30F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // rel_SelecaoBalanceamentoPortfolio
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.PageHeight = 827;
        this.PageWidth = 1169;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "10.1";
        ((System.ComponentModel.ISupportInitialize)(this.tbCenarios)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbProjetosCenarios)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblDescricaoPortfolio.Text = pNomePortfolio.Value.ToString();
    }

    private void pulaLinha(XRTable objTabela, PaddingInfo recuo)
    {
        XRTableRow pulaLinha = getRow(20, recuo, " ");
        pulaLinha.Borders = BorderSide.None;
        objTabela.Rows.Add(pulaLinha);
    }

    private void adicionaLinhaCabecalho(XRTable tabela, PaddingInfo recuo)
    {
        DataSet dsParametro = cDados.getParametrosSistema("labelScore");

        string labelScore = "Pontuação";

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            labelScore = dsParametro.Tables[0].Rows[0]["labelScore"] + "";
        }

        XRTableRow linhaCabecalho = getRow(20, recuo,
        "Ranking",
        "Categoria",
        "Projeto",
        labelScore,
        "Riscos",
        "Despesa (R$)",
        "Receita (R$)",
        "Recursos (h)",
        "Status");

        linhaCabecalho.Cells[0].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[1].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[2].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[3].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[4].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[5].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[6].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[7].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);
        linhaCabecalho.Cells[8].Font = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);

        linhaCabecalho.Cells[3].TextAlignment = TextAlignment.TopRight;//Pontuação
        linhaCabecalho.Cells[4].TextAlignment = TextAlignment.TopRight;//Riscos
        linhaCabecalho.Cells[5].TextAlignment = TextAlignment.TopRight;//Despesas
        linhaCabecalho.Cells[6].TextAlignment = TextAlignment.TopRight;//Receitas
        linhaCabecalho.Cells[7].TextAlignment = TextAlignment.TopRight;//Recursos
        linhaCabecalho.Cells[8].TextAlignment = TextAlignment.TopLeft;//Status

        linhaCabecalho.BackColor = Color.LightGray;
        tabela.Rows.Add(linhaCabecalho);
        tabela.Height += 20;
    }

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataSet ds = cDados.getRelatorioBalanceamento(int.Parse(pCodigoPortfolio.Value.ToString()), -1, int.Parse(pCodigoEntidade.Value.ToString()));
        DataSet dsCenarios = cDados.getRelatorioProjetosPorCenario(int.Parse(pCodigoPortfolio.Value.ToString()), -1, int.Parse(pCodigoEntidade.Value.ToString()));

        PaddingInfo recuo = new PaddingInfo(3, 3, 0, 0);

        DataSet dsParametro = cDados.getParametrosSistema("labelScore");

        string labelScore = "Pontuação";

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            labelScore = dsParametro.Tables[0].Rows[0]["labelScore"] + "";
        }

        xrTableCell21.Text = labelScore;

        tbProjetosCenarios.Rows.Clear();

        if (cDados.DataSetOk(dsCenarios) && cDados.DataTableOk(dsCenarios.Tables[0]))
        {
            string sDataSourceCenario = "";

            for (int iCenarios = 1; iCenarios <= 9; iCenarios++)
            {
                sDataSourceCenario = string.Format("cenario = 'cenario{0}'", iCenarios);

                DataRow[] drProjCenario = dsCenarios.Tables[0].Select(sDataSourceCenario);
                if (drProjCenario.Length > 0)
                {
                    pulaLinha(tbProjetosCenarios, recuo);
                    //pulaLinha(tbProjetosCenarios, recuo);

                    XRTableRow l = getRow(20, recuo, string.Format("Projetos - Cenário {0}", iCenarios));

                    l.BackColor = Color.LightGray;
                    //tbProjetosCenarios.Rows.FirstRow
                    tbProjetosCenarios.Rows.Add(l);
                    tbProjetosCenarios.Height += 20;

                    pulaLinha(tbProjetosCenarios, recuo);

                    adicionaLinhaCabecalho(tbProjetosCenarios, recuo);

                    for (int i = 0; i < drProjCenario.Length; i++)
                    {
                        XRTableRow linhaTemp = getRow(20, recuo,
                        drProjCenario[i]["Ranking"].ToString(),
                        drProjCenario[i]["Categoria"].ToString(),
                        drProjCenario[i]["NomeProjeto"].ToString(),
                        string.Format("{0:N2}", double.Parse(drProjCenario[i]["ScoreCriterios"].ToString())),
                        string.Format("{0:N2}", double.Parse(drProjCenario[i]["ScoreRiscos"].ToString())),
                        string.Format("{0:N0}", double.Parse(drProjCenario[i]["Custo"].ToString())),
                        string.Format("{0:N0}", double.Parse(drProjCenario[i]["Receita"].ToString())),
                        string.Format("{0:N0}", double.Parse(drProjCenario[i]["RH"].ToString())),
                        drProjCenario[i]["DescricaoStatusProjeto"].ToString());

                        linhaTemp.Cells[0].TextAlignment = TextAlignment.MiddleCenter;
                        linhaTemp.Cells[3].TextAlignment = TextAlignment.TopRight;
                        linhaTemp.Cells[4].TextAlignment = TextAlignment.TopRight;
                        linhaTemp.Cells[5].TextAlignment = TextAlignment.TopRight;
                        linhaTemp.Cells[6].TextAlignment = TextAlignment.TopRight;
                        linhaTemp.Cells[7].TextAlignment = TextAlignment.TopRight;
                        linhaTemp.Cells[8].TextAlignment = TextAlignment.TopLeft;


                        tbProjetosCenarios.Rows.Add(linhaTemp);
                        tbProjetosCenarios.Height += 20;
                    } // for 
                } // if 
            } // for 
        }

        pulaLinha(tbProjetosCenarios, recuo);
        for (int j = 0; j < tbProjetosCenarios.Rows.Count; j++)
        {
            if (tbProjetosCenarios.Rows[j].Cells.Count > 2)
            {
                tbProjetosCenarios.Rows[j].Cells[0].Width = 63;//Ranking
                tbProjetosCenarios.Rows[j].Cells[1].Width = 143;//Categoria
                tbProjetosCenarios.Rows[j].Cells[2].Width = 299;//Projeto
                tbProjetosCenarios.Rows[j].Cells[3].Width = 83;//Pontuação
                tbProjetosCenarios.Rows[j].Cells[4].Width = 65;//Riscos
                tbProjetosCenarios.Rows[j].Cells[5].Width = 104;//Despesas
                tbProjetosCenarios.Rows[j].Cells[6].Width = 106;//Receita
                tbProjetosCenarios.Rows[j].Cells[7].Width = 118;//RH
                tbProjetosCenarios.Rows[j].Cells[8].Width = 119;//status
            }
        }

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            //cenario eixo x eixo y despesa receita

            string sDataSourceCenario = "";
            PaddingInfo padInfo = new PaddingInfo(0, 2, 0, 0);

            for (int iCenarios = 1; iCenarios <= 9; iCenarios++)
            {
                sDataSourceCenario = string.Format("Cenario = 'Cenario {0}'", iCenarios);
                DataRow[] drCenario = ds.Tables[0].Select(sDataSourceCenario);

                tbCenarios.WidthF = 90F + iCenarios * 110F;
                XRTableCell[] col = tbCenarios.InsertColumnToRight(null);
                foreach (XRTableRow tr in tbCenarios.Rows)
                {
                    foreach (XRTableCell cell in tr.Cells)
                    {
                        if (cell.Index == 0)
                            cell.WidthF = 90F;
                        else
                            cell.WidthF = 110F;
                    }
                }

                col[0].Text = string.Format("Cenário {0}", iCenarios);
                col[0].BackColor = System.Drawing.Color.WhiteSmoke;
                col[0].Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
                col[0].TextAlignment = TextAlignment.MiddleCenter;
                //col[0].WidthF = 170F;

                col[1].Text = string.Format("{0:N0}", double.Parse(drCenario[0]["Despesa"].ToString()));
                col[1].Font = new System.Drawing.Font("Verdana", 9F);
                col[1].Padding = padInfo;
                col[1].TextAlignment = TextAlignment.MiddleRight;
                //col[1].WidthF = 170F;

                col[2].Text = string.Format("{0:N0}", double.Parse(drCenario[0]["Receita"].ToString()));
                col[2].Font = new System.Drawing.Font("Verdana", 9F);
                col[2].Padding = padInfo;
                col[2].TextAlignment = TextAlignment.MiddleRight;
                //col[2].WidthF = 170F;

                col[3].Text = string.Format("{0:N0}", double.Parse(drCenario[0]["Trabalho"].ToString()));
                col[3].Font = new System.Drawing.Font("Verdana", 9F);
                col[3].Padding = padInfo;
                col[3].TextAlignment = TextAlignment.MiddleRight;
                //col[3].WidthF = 170F;

                col[4].Text = string.Format("{0:N2}", double.Parse(drCenario[0]["ValorRisco"].ToString()));
                col[4].Font = new System.Drawing.Font("Verdana", 9F);
                col[4].Padding = padInfo;
                col[4].TextAlignment = TextAlignment.MiddleRight;
                //col[4].WidthF = 170F;

                col[5].Text = string.Format("{0:N2}", double.Parse(drCenario[0]["ValorCriterio"].ToString()));
                col[5].Font = new System.Drawing.Font("Verdana", 9F);
                col[5].Padding = padInfo;
                col[5].TextAlignment = TextAlignment.MiddleRight;
                //col[5].WidthF = 170F;

                col[6].Text = drCenario[0]["QuantidadeProjetos"].ToString();
                col[6].Font = new System.Drawing.Font("Verdana", 9F);
                col[6].Padding = padInfo;
                col[6].TextAlignment = TextAlignment.MiddleRight;
                //col[6].WidthF = 170F;
            }
        }
        imgEntidade.ImageUrl = pCaminhoArquivo.Value.ToString();
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

    private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string msg = "Relatório emitido por: " + pNomeUsuario.Value.ToString() + " em " + DateTime.Now.ToString() + "";
        lblDataGeracaoRelatorio.Text = msg;
    }
}
