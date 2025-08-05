using DevExpress.XtraReports.UI;
using System;

/// <summary>
/// Summary description for rel_LicoesAprendidas
/// </summary>
public class rel_LicoesAprendidas : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private XRLabel lblData;
    /// <summary>
    /// Required designer variable.
    /// </summary>

    private DevExpress.XtraReports.Parameters.Parameter pDataInclusao = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pIncluidaPor = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pTipo = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pAssunto = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeProjeto = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pLicao = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pPathLogo = new DevExpress.XtraReports.Parameters.Parameter();

    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableRow xrTableRow2;
    private XRTableCell celDataInclusao;
    private XRTableCell celIncluidoPor;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow4;
    private XRTableCell celTipo;
    private XRTableCell celAssunto;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow6;
    private XRTableCell celProjeto;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow8;
    private XRTableCell celLicaoAprendida;
    private FormattingRule formattingRule1;
    private XRPictureBox logoEntidade;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;

    private System.ComponentModel.IContainer components = null;

    public rel_LicoesAprendidas()
    {

        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        pDataInclusao.Name = "pDataInclusao";
        pIncluidaPor.Name = "pIncluidaPor";
        pTipo.Name = "pTipo";
        pAssunto.Name = "pAssunto";
        pNomeProjeto.Name = "pNomeProjeto";
        pLicao.Name = "pLicao";
        pPathLogo.Name = "pPathLogo";

        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
        {
            this.pDataInclusao,
            this.pIncluidaPor,
            this.pTipo,
            this.pAssunto,
            this.pNomeProjeto,
            this.pLicao,
            this.pPathLogo
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
        //string resourceFileName = "rel_LicoesAprendidas.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celDataInclusao = new DevExpress.XtraReports.UI.XRTableCell();
        this.celIncluidoPor = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celTipo = new DevExpress.XtraReports.UI.XRTableCell();
        this.celAssunto = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celProjeto = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celLicaoAprendida = new DevExpress.XtraReports.UI.XRTableCell();
        this.lblData = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.logoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.HeightF = 286F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // xrTable1
        // 
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow8});
        this.xrTable1.SizeF = new System.Drawing.SizeF(767F, 283F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.Text = "Data de Inclusão:";
        this.xrTableCell1.Weight = 1D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell2.StylePriority.UseBackColor = false;
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.Text = "Incluído por:";
        this.xrTableCell2.Weight = 2D;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celDataInclusao,
            this.celIncluidoPor});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // celDataInclusao
        // 
        this.celDataInclusao.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celDataInclusao.Name = "celDataInclusao";
        this.celDataInclusao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celDataInclusao.StylePriority.UseBorders = false;
        this.celDataInclusao.StylePriority.UsePadding = false;
        this.celDataInclusao.Weight = 1D;
        // 
        // celIncluidoPor
        // 
        this.celIncluidoPor.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celIncluidoPor.Name = "celIncluidoPor";
        this.celIncluidoPor.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celIncluidoPor.StylePriority.UseBorders = false;
        this.celIncluidoPor.StylePriority.UsePadding = false;
        this.celIncluidoPor.Weight = 2D;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell7.StylePriority.UseBackColor = false;
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.Text = "Tipo:";
        this.xrTableCell7.Weight = 1D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell8.StylePriority.UseBackColor = false;
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.Text = "Assunto:";
        this.xrTableCell8.Weight = 2D;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celTipo,
            this.celAssunto});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // celTipo
        // 
        this.celTipo.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celTipo.Name = "celTipo";
        this.celTipo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celTipo.StylePriority.UseBorders = false;
        this.celTipo.StylePriority.UsePadding = false;
        this.celTipo.Weight = 1D;
        // 
        // celAssunto
        // 
        this.celAssunto.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celAssunto.Name = "celAssunto";
        this.celAssunto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celAssunto.StylePriority.UseBorders = false;
        this.celAssunto.StylePriority.UsePadding = false;
        this.celAssunto.Weight = 2D;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell9.StylePriority.UseBackColor = false;
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UsePadding = false;
        this.xrTableCell9.Text = "Projeto:";
        this.xrTableCell9.Weight = 3D;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celProjeto});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1D;
        // 
        // celProjeto
        // 
        this.celProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celProjeto.Name = "celProjeto";
        this.celProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celProjeto.StylePriority.UseBorders = false;
        this.celProjeto.StylePriority.UsePadding = false;
        this.celProjeto.Weight = 3D;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell12.StylePriority.UseBackColor = false;
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UsePadding = false;
        this.xrTableCell12.Text = "Lição Aprendida:";
        this.xrTableCell12.Weight = 3D;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celLicaoAprendida});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 4.32D;
        // 
        // celLicaoAprendida
        // 
        this.celLicaoAprendida.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celLicaoAprendida.Name = "celLicaoAprendida";
        this.celLicaoAprendida.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celLicaoAprendida.StylePriority.UseBorders = false;
        this.celLicaoAprendida.StylePriority.UsePadding = false;
        this.celLicaoAprendida.Weight = 3D;
        // 
        // lblData
        // 
        this.lblData.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblData.LocationFloat = new DevExpress.Utils.PointFloat(317F, 8F);
        this.lblData.Name = "lblData";
        this.lblData.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblData.SizeF = new System.Drawing.SizeF(450F, 25F);
        this.lblData.StylePriority.UseBorders = false;
        this.lblData.StylePriority.UseTextAlignment = false;
        this.lblData.Text = "LIÇÕES APRENDIDAS";
        this.lblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblData,
            this.logoEntidade});
        this.PageHeader.HeightF = 82F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // logoEntidade
        // 
        this.logoEntidade.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.logoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.logoEntidade.Name = "logoEntidade";
        this.logoEntidade.SizeF = new System.Drawing.SizeF(275F, 80F);
        this.logoEntidade.StylePriority.UseBorders = false;
        // 
        // PageFooter
        // 
        this.PageFooter.HeightF = 30F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
        // rel_LicoesAprendidas
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        celAssunto.Text = pAssunto.Value.ToString();
        celDataInclusao.Text = String.Format("{0:dd/MM/yyyy}", pDataInclusao.Value);
        celIncluidoPor.Text = pIncluidaPor.Value.ToString();
        celLicaoAprendida.Text = pLicao.Value.ToString();
        celProjeto.Text = pNomeProjeto.Value.ToString();
        celTipo.Text = pTipo.Value.ToString();

    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        logoEntidade.ImageUrl = pPathLogo.Value.ToString();
    }
}
