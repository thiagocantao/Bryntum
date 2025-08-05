using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Text;
using System.Globalization;

/// <summary>
/// Summary description for rel_ImpressaoFormularios
/// </summary>
public class rel_ImpressaoFormularios : DevExpress.XtraReports.UI.XtraReport
{
    #region Fields

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private System.Data.DataSet ds;
    private XRLabel xrLabel1;
    private XRLine xrLine1;
    private XRLabel xrLabel2;
    private DevExpress.XtraReports.Parameters.Parameter ParamCodigoFormulario;
    private XRRichText xrRichText1;
    private System.Data.DataTable dtFromularios;
    private System.Data.DataTable dtAbas;
    private System.Data.DataTable dtCampos;
    private XRLabel xrLabel3;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private DataTable dtModeloFormulario;
    private DataTable dtProjeto;
    private DataTable dtColunas;
    private DataTable dtSubFormularios;
    private XRShape xrShape1;
    private DataTable dtEntidade;
    private XRPanel xrPanel1;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox1;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private FormattingRule frSubFormulario;
    private FormattingRule frAbaUnica;
    private PageHeaderBand PageHeader;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null; 

    #endregion

    #region Constructors

    public rel_ImpressaoFormularios(int codigoModeloFormulario)
    {
        InitializeComponent();
        ParamCodigoFormulario.Value = codigoModeloFormulario;
        InitData();
    } 

    #endregion

    #region Methods

    private void InitData()
    {
        dados cDados = new dados();
        String comandoSql = string.Format("exec [dbo].[p_GetDadosFormularios] null, '{0}'", ParamCodigoFormulario.Value);
        DataSet dsTemp = cDados.getDataSet(comandoSql);
        DataTableReader reader = dsTemp.CreateDataReader();
        DataTable[] tables = new System.Data.DataTable[] {   
            this.dtEntidade,
            this.dtProjeto,
            this.dtFromularios,
            this.dtModeloFormulario,
            this.dtAbas,
            this.dtColunas,
            this.dtSubFormularios,
            this.dtCampos
        };
        ds.Load(reader, LoadOption.OverwriteChanges, tables);
        string subFormularios = string.Empty;
        foreach (DataRow row in dtSubFormularios.Rows)
        {
            if (!string.IsNullOrEmpty(subFormularios))
                subFormularios = subFormularios + ",";
            subFormularios = row["SubFormularios"].ToString();
        }
        if (!string.IsNullOrEmpty(subFormularios))
        {
            comandoSql = string.Format("exec [dbo].[p_GetDadosFormularios] null, '{0}', 'N'", subFormularios);
            dsTemp = cDados.getDataSet(comandoSql);
            foreach (DataRow dr in dtCampos.Select("CodigoTipoCampo = 'SUB'"))
            {
                string strConteudo = ObtemConteudoHtmlFormulario(
                    dsTemp.Tables[0], Convert.ToInt32(dr["DefinicaoCampo"]));
                dr["ConteudoCampo"] = strConteudo;
            }
        }
        FormatValues();
    }

    private string ObtemConteudoHtmlFormulario(DataTable dadosSubFormulario, int codModeloSub)
    {
        string filter = string.Format("codigoModeloFormulario = {0}", codModeloSub);
        DataRow[] colunas = dtColunas.Select(filter, "OrdemCampoFormulario");
        StringBuilder conteudoHtml = new StringBuilder();
        int qtdColunas = colunas.Length;
        conteudoHtml.Append("<table border=\"0\" CELLPADDING=\"0\" CELLSPACING=\"0\"><tr><td  width=\"1\">&nbsp;</td><td>"); 
        conteudoHtml.Append("<table border=\"1\" CELLPADDING=\"3\" CELLSPACING=\"3\">");
        conteudoHtml.Append("<tr>");
        for (int i = 0; i < qtdColunas; i++)
        {
            conteudoHtml.Append("<td><b>");
            conteudoHtml.Append(colunas[i]["NomeCampo"]);
            conteudoHtml.Append("</b></td>");
        }
        conteudoHtml.Append("</tr>");
        int contCol = 0;
        DataRow[] rows = dadosSubFormulario.Select(filter, "codigoFormulario, ordemCampoFormulario");
        foreach (DataRow drSub in rows)
        {
            contCol++;
            if (contCol % qtdColunas == 1)
                conteudoHtml.Append("<tr>");

            if (Convert.IsDBNull(drSub["ConteudoCampo"]))
                conteudoHtml.Append("<td>&nbsp;</td>");
            else
                conteudoHtml.Append(string.Format("<td>{0}</td>", drSub["ConteudoCampo"]));

            if (contCol % qtdColunas == 0)
                conteudoHtml.Append("</tr>");
        }
        conteudoHtml.Append("</table>");
        conteudoHtml.Append("</td></tr></table>");
        string strConteudo = conteudoHtml.ToString();
        return strConteudo;
    }

    private void FormatValues()
    {
        foreach (DataRow row in dtCampos.Select("CodigoTipoCampo = 'NUM' OR CodigoTipoCampo = 'DAT'"))
        {
            object value = row["ConteudoCampo"];
            string format = row["DefinicaoCampo"].ToString();
            string tipoCampo = row["CodigoTipoCampo"].ToString();
            if (Convert.IsDBNull(value) || string.IsNullOrEmpty(value.ToString()))
                continue;
            CultureInfo ci = new CultureInfo("en-US");
            switch (tipoCampo)
            {
                case "NUM":
                    value = Convert.ToDecimal(value, ci);
                    break;
                case "DAT":
                    value = Convert.ToDateTime(value);
                    break;
            }
            row["ConteudoCampo"] = string.Format(format, value); ;
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

    #endregion

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.Resources.ResourceManager resources = global::Resources.rel_ImpressaoFormularios.ResourceManager;
        System.Data.DataColumn dataColumn1;
        System.Data.DataColumn dataColumn2;
        System.Data.DataColumn dataColumn4;
        System.Data.DataColumn dataColumn5;
        System.Data.DataColumn dataColumn6;
        System.Data.DataColumn dataColumn7;
        System.Data.DataColumn dataColumn8;
        System.Data.DataColumn dataColumn9;
        System.Data.DataColumn dataColumn10;
        System.Data.DataColumn dataColumn11;
        System.Data.DataColumn dataColumn12;
        System.Data.DataColumn dataColumn13;
        System.Data.DataColumn dataColumn14;
        System.Data.DataColumn dataColumn3;
        System.Data.DataColumn dataColumn15;
        System.Data.DataColumn dataColumn16;
        System.Data.DataColumn dataColumn17;
        System.Data.DataColumn dataColumn18;
        System.Data.DataColumn dataColumn19;
        System.Data.DataColumn dataColumn20;
        System.Data.DataColumn dataColumn21;
        System.Data.DataColumn dataColumn22;
        System.Data.DataColumn dataColumn23;
        System.Data.DataColumn dataColumn24;
        System.Data.DataColumn dataColumn25;
        System.Data.DataColumn dataColumn26;
        System.Data.DataColumn dataColumn27;
        System.Data.DataColumn dataColumn28;
        System.Data.DataColumn dataColumn29;
        System.Data.DataColumn dataColumn30;
        System.Data.DataColumn dataColumn31;
        DevExpress.XtraPrinting.Shape.ShapeArrow shapeArrow1 = new DevExpress.XtraPrinting.Shape.ShapeArrow();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ds = new System.Data.DataSet();
        this.dtFromularios = new System.Data.DataTable();
        this.dtAbas = new System.Data.DataTable();
        this.dtCampos = new System.Data.DataTable();
        this.dtModeloFormulario = new System.Data.DataTable();
        this.dtProjeto = new System.Data.DataTable();
        this.dtColunas = new System.Data.DataTable();
        this.dtSubFormularios = new System.Data.DataTable();
        this.dtEntidade = new System.Data.DataTable();
        this.ParamCodigoFormulario = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.frSubFormulario = new DevExpress.XtraReports.UI.FormattingRule();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.frAbaUnica = new DevExpress.XtraReports.UI.FormattingRule();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        dataColumn1 = new System.Data.DataColumn();
        dataColumn2 = new System.Data.DataColumn();
        dataColumn4 = new System.Data.DataColumn();
        dataColumn5 = new System.Data.DataColumn();
        dataColumn6 = new System.Data.DataColumn();
        dataColumn7 = new System.Data.DataColumn();
        dataColumn8 = new System.Data.DataColumn();
        dataColumn9 = new System.Data.DataColumn();
        dataColumn10 = new System.Data.DataColumn();
        dataColumn11 = new System.Data.DataColumn();
        dataColumn12 = new System.Data.DataColumn();
        dataColumn13 = new System.Data.DataColumn();
        dataColumn14 = new System.Data.DataColumn();
        dataColumn3 = new System.Data.DataColumn();
        dataColumn15 = new System.Data.DataColumn();
        dataColumn16 = new System.Data.DataColumn();
        dataColumn17 = new System.Data.DataColumn();
        dataColumn18 = new System.Data.DataColumn();
        dataColumn19 = new System.Data.DataColumn();
        dataColumn20 = new System.Data.DataColumn();
        dataColumn21 = new System.Data.DataColumn();
        dataColumn22 = new System.Data.DataColumn();
        dataColumn23 = new System.Data.DataColumn();
        dataColumn24 = new System.Data.DataColumn();
        dataColumn25 = new System.Data.DataColumn();
        dataColumn26 = new System.Data.DataColumn();
        dataColumn27 = new System.Data.DataColumn();
        dataColumn28 = new System.Data.DataColumn();
        dataColumn29 = new System.Data.DataColumn();
        dataColumn30 = new System.Data.DataColumn();
        dataColumn31 = new System.Data.DataColumn();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtFromularios)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtAbas)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtCampos)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtModeloFormulario)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtProjeto)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtColunas)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtSubFormularios)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtEntidade)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // dataColumn1
        // 
        dataColumn1.ColumnName = "CodigoFormulario";
        dataColumn1.DataType = typeof(int);
        // 
        // dataColumn2
        // 
        dataColumn2.ColumnName = "DescricaoFormulario";
        // 
        // dataColumn4
        // 
        dataColumn4.ColumnName = "CodigoModeloFormulario";
        dataColumn4.DataType = typeof(int);
        // 
        // dataColumn5
        // 
        dataColumn5.ColumnName = "CodigoAba";
        dataColumn5.DataType = typeof(int);
        // 
        // dataColumn6
        // 
        dataColumn6.ColumnName = "DescricaoAba";
        // 
        // dataColumn7
        // 
        dataColumn7.ColumnName = "Sequencial";
        dataColumn7.DataType = typeof(short);
        // 
        // dataColumn8
        // 
        dataColumn8.Caption = "CodigoFormulario";
        dataColumn8.ColumnName = "CodigoFormulario";
        dataColumn8.DataType = typeof(int);
        // 
        // dataColumn9
        // 
        dataColumn9.ColumnName = "CodigoAba";
        dataColumn9.DataType = typeof(int);
        // 
        // dataColumn10
        // 
        dataColumn10.ColumnName = "OrdemCampoFormulario";
        dataColumn10.DataType = typeof(short);
        // 
        // dataColumn11
        // 
        dataColumn11.ColumnName = "NomeCampo";
        // 
        // dataColumn12
        // 
        dataColumn12.ColumnName = "CodigoTipoCampo";
        // 
        // dataColumn13
        // 
        dataColumn13.ColumnName = "ConteudoCampo";
        // 
        // dataColumn14
        // 
        dataColumn14.ColumnName = "DefinicaoCampo";
        // 
        // dataColumn3
        // 
        dataColumn3.ColumnName = "CodigoProjeto";
        dataColumn3.DataType = typeof(int);
        // 
        // dataColumn15
        // 
        dataColumn15.ColumnName = "CodigoModeloFormulario";
        dataColumn15.DataType = typeof(int);
        // 
        // dataColumn16
        // 
        dataColumn16.ColumnName = "CodigoModeloFormulario";
        dataColumn16.DataType = typeof(int);
        // 
        // dataColumn17
        // 
        dataColumn17.ColumnName = "NomeFormulario";
        // 
        // dataColumn18
        // 
        dataColumn18.ColumnName = "DescricaoFormulario";
        // 
        // dataColumn19
        // 
        dataColumn19.ColumnName = "CodigoProjeto";
        dataColumn19.DataType = typeof(int);
        // 
        // dataColumn20
        // 
        dataColumn20.ColumnName = "NomeProjeto";
        // 
        // dataColumn21
        // 
        dataColumn21.ColumnName = "CodigoModeloFormulario";
        dataColumn21.DataType = typeof(int);
        // 
        // dataColumn22
        // 
        dataColumn22.ColumnName = "CodigoCampo";
        dataColumn22.DataType = typeof(int);
        // 
        // dataColumn23
        // 
        dataColumn23.ColumnName = "NomeCampo";
        // 
        // dataColumn24
        // 
        dataColumn24.ColumnName = "OrdemCampoFormulario";
        dataColumn24.DataType = typeof(short);
        // 
        // dataColumn25
        // 
        dataColumn25.ColumnName = "CodigoModeloFormulario";
        dataColumn25.DataType = typeof(int);
        // 
        // dataColumn26
        // 
        dataColumn26.ColumnName = "CodigoCampo";
        dataColumn26.DataType = typeof(int);
        // 
        // dataColumn27
        // 
        dataColumn27.ColumnName = "Abas";
        // 
        // dataColumn28
        // 
        dataColumn28.ColumnName = "SubFormularios";
        // 
        // dataColumn29
        // 
        dataColumn29.ColumnName = "CodigoEntidade";
        dataColumn29.DataType = typeof(int);
        // 
        // dataColumn30
        // 
        dataColumn30.ColumnName = "NomeEntidade";
        // 
        // dataColumn31
        // 
        dataColumn31.ColumnName = "LogoEntidade";
        dataColumn31.DataType = typeof(byte[]);
        // 
        // Detail
        // 
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 0F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ModeloFormulario.DescricaoFormulario")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(310F, 10F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1490F, 60F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel2
        // 
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Formularios.Rel_Formularios_Abas.Rel_Abas_Campos.NomeCampo")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1750F, 50F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Nome Campo";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 40F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1800F, 5F);
        // 
        // xrLabel1
        // 
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Formularios.Rel_Formularios_Abas.DescricaoAba")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1800F, 40F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Nome Aba";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.StylePriority.UseTextAlignment = false;
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ds
        // 
        this.ds.DataSetName = "NewDataSet";
        this.ds.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("Rel_Formularios_Abas", "Formularios", "Abas", new string[] {
                        "CodigoModeloFormulario"}, new string[] {
                        "CodigoModeloFormulario"}, false),
            new System.Data.DataRelation("Rel_Abas_Campos", "Abas", "Campos", new string[] {
                        "CodigoModeloFormulario",
                        "CodigoAba"}, new string[] {
                        "CodigoModeloFormulario",
                        "CodigoAba"}, false)});
        this.ds.Tables.AddRange(new System.Data.DataTable[] {
            this.dtFromularios,
            this.dtAbas,
            this.dtCampos,
            this.dtModeloFormulario,
            this.dtProjeto,
            this.dtColunas,
            this.dtSubFormularios,
            this.dtEntidade});
        // 
        // dtFromularios
        // 
        this.dtFromularios.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn1,
            dataColumn2,
            dataColumn3,
            dataColumn15});
        this.dtFromularios.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "CodigoModeloFormulario"}, false)});
        this.dtFromularios.TableName = "Formularios";
        // 
        // dtAbas
        // 
        this.dtAbas.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn4,
            dataColumn5,
            dataColumn6});
        this.dtAbas.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Rel_Formularios_Abas", "Formularios", new string[] {
                        "CodigoModeloFormulario"}, new string[] {
                        "CodigoModeloFormulario"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade),
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "CodigoModeloFormulario",
                        "CodigoAba"}, false)});
        this.dtAbas.TableName = "Abas";
        // 
        // dtCampos
        // 
        this.dtCampos.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn7,
            dataColumn8,
            dataColumn9,
            dataColumn10,
            dataColumn11,
            dataColumn12,
            dataColumn13,
            dataColumn14,
            dataColumn25,
            dataColumn26});
        this.dtCampos.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Rel_Abas_Campos", "Abas", new string[] {
                        "CodigoModeloFormulario",
                        "CodigoAba"}, new string[] {
                        "CodigoModeloFormulario",
                        "CodigoAba"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade)});
        this.dtCampos.TableName = "Campos";
        // 
        // dtModeloFormulario
        // 
        this.dtModeloFormulario.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn16,
            dataColumn17,
            dataColumn18,
            dataColumn27});
        this.dtModeloFormulario.TableName = "ModeloFormulario";
        // 
        // dtProjeto
        // 
        this.dtProjeto.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn19,
            dataColumn20});
        this.dtProjeto.TableName = "Projeto";
        // 
        // dtColunas
        // 
        this.dtColunas.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn21,
            dataColumn22,
            dataColumn23,
            dataColumn24});
        this.dtColunas.TableName = "Colunas";
        // 
        // dtSubFormularios
        // 
        this.dtSubFormularios.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn28});
        this.dtSubFormularios.TableName = "SubFormularios";
        // 
        // dtEntidade
        // 
        this.dtEntidade.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumn29,
            dataColumn30,
            dataColumn31});
        this.dtEntidade.TableName = "Entidade";
        // 
        // ParamCodigoFormulario
        // 
        this.ParamCodigoFormulario.Name = "ParamCodigoFormulario";
        this.ParamCodigoFormulario.Type = typeof(Int32);// DevExpress.XtraReports.Parameters.ParameterType.Int32;
        //this.ParamCodigoFormulario.ParameterType = DevExpress.XtraReports.Parameters.ParameterType.Int32;
        this.ParamCodigoFormulario.Value = 0;
        this.ParamCodigoFormulario.Visible = false;
        // 
        // xrRichText1
        // 
        this.xrRichText1.BorderColor = System.Drawing.Color.Black;
        this.xrRichText1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Formularios.Rel_Formularios_Abas.Rel_Abas_Campos.ConteudoCampo")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.xrRichText1.FormattingRules.Add(this.frSubFormulario);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1800F, 40F);
        this.xrRichText1.StylePriority.UseBorderColor = false;
        this.xrRichText1.StylePriority.UseBorders = false;
        this.xrRichText1.StylePriority.UseFont = false;
        // 
        // frSubFormulario
        // 
        this.frSubFormulario.Condition = "[CodigoTipoCampo] == \'SUB\'";
        this.frSubFormulario.DataMember = "Formularios.Rel_Formularios_Abas.Rel_Abas_Campos";
        this.frSubFormulario.DataSource = this.ds;
        // 
        // 
        // 
        this.frSubFormulario.Formatting.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.frSubFormulario.Name = "frSubFormulario";
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.DetailReport1});
        this.DetailReport.DataMember = "Formularios.Rel_Formularios_Abas";
        this.DetailReport.DataSource = this.ds;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLine1});
        this.Detail1.Dpi = 254F;
        this.Detail1.FormattingRules.Add(this.frAbaUnica);
        this.Detail1.HeightF = 60F;
        this.Detail1.Name = "Detail1";
        // 
        // frAbaUnica
        // 
        this.frAbaUnica.Condition = "[DataSource.RowCount] == 1";
        this.frAbaUnica.DataMember = "Formularios.Rel_Formularios_Abas";
        this.frAbaUnica.DataSource = this.ds;
        // 
        // 
        // 
        this.frAbaUnica.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
        this.frAbaUnica.Name = "frAbaUnica";
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
        this.DetailReport1.DataMember = "Formularios.Rel_Formularios_Abas.Rel_Abas_Campos";
        this.DetailReport1.DataSource = this.ds;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 150F;
        this.Detail2.Name = "Detail2";
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrShape1,
            this.xrLabel2,
            this.xrRichText1});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1800F, 100F);
        // 
        // xrShape1
        // 
        this.xrShape1.Angle = 270;
        this.xrShape1.Dpi = 254F;
        this.xrShape1.FillColor = System.Drawing.Color.Black;
        this.xrShape1.LineWidth = 0;
        this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(15F, 15F);
        this.xrShape1.Name = "xrShape1";
        this.xrShape1.Shape = shapeArrow1;
        this.xrShape1.SizeF = new System.Drawing.SizeF(20F, 20F);
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Entidade.LogoEntidade")});
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(300F, 150F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 50F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo2.Format = "Pág. {0}/{1}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1601F, 10F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(200F, 30F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo1.Format = "Emitido em {0:dd/MM/yyyy - HH:mm}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(500F, 30F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel3});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 175F;
        this.PageHeader.Name = "PageHeader";
        // 
        // rel_ImpressaoFormularios
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.PageFooter,
            this.PageHeader});
        this.DataMember = "Formularios";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.frSubFormulario,
            this.frAbaUnica});
        this.Margins = new System.Drawing.Printing.Margins(150, 150, 100, 100);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ParamCodigoFormulario});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "10.1";
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtFromularios)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtAbas)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtCampos)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtModeloFormulario)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtProjeto)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtColunas)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtSubFormularios)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dtEntidade)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
