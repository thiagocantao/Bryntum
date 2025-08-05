using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for rel_ImpressaoFormularios
/// </summary>
public class rel_ImpressaoFormularios : DevExpress.XtraReports.UI.XtraReport
{
    #region Fields

    private readonly int? _CodigoFormularioAssinatura;
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
    private DataTable dtEntidade;
    private XRPanel xrPanel1;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox1;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private FormattingRule frSubFormulario;
    private FormattingRule frAbaUnica;
    private PageHeaderBand PageHeader;
    private ReportFooterBand ReportFooter;
    private XRLine xrLine4;
    private XRLine xrLine5;
    private XRLine xrLine6;
    private XRLine xrLine7;
    private XRSubreport subRelAssinaturasFormulario;
    private XRPictureBox imgAssinatura;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel4;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion

    #region Constructors

    public rel_ImpressaoFormularios(int codigoModeloFormulario)
        : this(codigoModeloFormulario, null)
    {

    }

    public rel_ImpressaoFormularios(int codigoModeloFormulario, int? codigoFormularioAssinatura)
    {
        _CodigoFormularioAssinatura = codigoFormularioAssinatura;
        InitializeComponent();
        ParamCodigoFormulario.Value = codigoModeloFormulario;
        InitData();
    }

    #endregion

    #region Methods

    private void InitData()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        string nomeParametro = "mostrarAssinaturaImpressaoFormularioDinamico";
        DataTable dtParametro = cDados.getParametrosSistema(nomeParametro).Tables[0];


        bool oFormularioEAssinado = ReportFooter.Visible = dtParametro.Rows.Count > 0 &&
              dtParametro.Rows[0][nomeParametro].Equals("S") && _CodigoFormularioAssinatura.HasValue;
        //string comandSQLVerificaFormularioAssinado = string.Format("SELECT {0}.{1}.[f_VerificaFormularioAssinado] ({2})", cDados.getDbName(), cDados.getDbOwner(), (ParamCodigoFormulario.Value != null) ? ParamCodigoFormulario.Value.ToString() : "-1");
        //DataSet dsVerificaFormularioAssinado = cDados.getDataSet(comandSQLVerificaFormularioAssinado);
        //if (cDados.DataSetOk(dsVerificaFormularioAssinado) && cDados.DataTableOk(dsVerificaFormularioAssinado.Tables[0]))
        //{
        //    oFormularioEAssinado = (dsVerificaFormularioAssinado.Tables[0].Rows[0][0].ToString().ToLower().Trim() == "s");
        //}

        imgAssinatura.Visible = oFormularioEAssinado;

        Object codigoUsuario = cDados.getInfoSistema("IDUsuarioLogado");
        String comandoSql = String.Format("exec [dbo].[p_GetDadosFormularios] {0}, null, '{1}'", codigoUsuario, ParamCodigoFormulario.Value);
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
        String subFormularios = String.Empty;
        foreach (DataRow row in dtSubFormularios.Rows)
        {
            if (!String.IsNullOrEmpty(subFormularios))
                subFormularios = subFormularios + ",";
            subFormularios = row["SubFormularios"].ToString();
        }
        if (!String.IsNullOrEmpty(subFormularios))
        {
            comandoSql = String.Format("exec [dbo].[p_GetDadosFormularios] {0}, null, '{1}', 'N'", codigoUsuario, subFormularios);
            dsTemp = cDados.getDataSet(comandoSql);
            foreach (DataRow dr in dtCampos.Select("CodigoTipoCampo = 'SUB'"))
            {
                String strConteudo = ObtemConteudoHtmlFormulario(
                    dsTemp.Tables[0], Convert.ToInt32(dr["DefinicaoCampo"]), Convert.ToInt32(dr["codigoCampoModeloFormulario"]));
                dr["ConteudoCampo"] = strConteudo.Replace("\n", "<br>") + "<br>";
            }
        }
        FormatValues();
        foreach (DataRow row in dtCampos.Rows)
        {
            row["ConteudoCampo"] = String.Format(
                "<div style=\"font-family:Verdana;font-size:8pt\">{0}</div>", row["ConteudoCampo"].ToString().Replace("\n", "<br>") + "<br>");
        }
        if (oFormularioEAssinado)
        {
            relAssinaturasFormulario raf = new relAssinaturasFormulario(ParamCodigoFormulario.Value.ToString(), _CodigoFormularioAssinatura.Value);
            subRelAssinaturasFormulario.ReportSource = raf;
        }

        DataSet dsMostraEspacoAssinaturaAMao = cDados.getParametrosSistema("mostraEspacoAssinaturaAMao");
        if (cDados.DataSetOk(dsMostraEspacoAssinaturaAMao) && cDados.DataTableOk(dsMostraEspacoAssinaturaAMao.Tables[0]))
        {
            GroupFooter1.Visible = dsMostraEspacoAssinaturaAMao.Tables[0].Rows[0][0].ToString().Trim().ToLower() == "s";
        }

    }

    private String ObtemConteudoHtmlFormulario(DataTable dadosSubFormulario, int codModeloSub, int codigoCampoModeloFormulario)
    {
        String filter = String.Format("codigoModeloFormulario = {0} and codigoCampoModeloFormulario = {1}", codModeloSub, codigoCampoModeloFormulario);
        DataRow[] colunas = dtColunas.Select(filter, "OrdemCampoFormulario");
        StringBuilder conteudoHtml = new StringBuilder();
        Int32 qtdColunas = colunas.Length;

        conteudoHtml.Append("<table border=\"0\" CELLPADDING=\"0\" CELLSPACING=\"0\" style=\"width:100%\"><tr><td  width=\"1\">&nbsp;</td><td>");
        conteudoHtml.Append("<table border=\"1\" CELLPADDING=\"0\" CELLSPACING=\"0\" style=\"width:100%\">");
        conteudoHtml.Append("<tr>");
        for (Int32 i = 0; i < qtdColunas; i++)
        {
            conteudoHtml.Append("<td style=\"padding-right:10px;padding-left:10px;font-family: Verdana; font-size: 8pt; font-weight: bold; color: #000080;background-color: #F4F4F4;border:1px solid gainsboro;\"><b>");
            conteudoHtml.Append(colunas[i]["NomeCampo"]);
            conteudoHtml.Append("</b></td>");
        }
        conteudoHtml.Append("</tr>");
        Int32 contCol = 0;
        DataRow[] rows = dadosSubFormulario.Select(filter, "codigoFormulario, ordemCampoFormulario");
        foreach (DataRow drSub in rows)
        {
            contCol++;
            if (contCol % qtdColunas == 1)
                conteudoHtml.Append("<tr>");

            String tipoCampo = drSub["CodigoTipoCampo"].ToString().ToUpper();
            Object conteudoCampo = drSub["ConteudoCampo"];
            if (!Convert.IsDBNull(conteudoCampo))
            {
                switch (tipoCampo)
                {
                    case "NUM":
                    case "CAL":
                        conteudoCampo = Convert.ToDecimal(conteudoCampo, new CultureInfo("en-US"));
                        break;
                    case "DAT":
                        conteudoCampo = Convert.ToDateTime(conteudoCampo);
                        break;
                    default:
                        break;
                }
            }
            String format;
            format = GetTableDataFormat(drSub["DefinicaoCampo"].ToString(), tipoCampo);
            format = String.Format("<td style=\"padding-right:10px;padding-left:10px; font-family: Verdana; font-size: 7pt;border:1px solid gainsboro\">{0}</td>",
                String.IsNullOrEmpty(format) ? "{0}" : format);
            if (Convert.IsDBNull(conteudoCampo))
                conteudoHtml.Append("<td style=\"font-family: Verdana; font-size: 7pt;border:1px solid gainsboro\">&nbsp;</td>");
            else
                conteudoHtml.Append(String.Format(format, conteudoCampo));

            if (contCol % qtdColunas == 0)
                conteudoHtml.Append("</tr>");
        }
        conteudoHtml.Append("</table>");
        conteudoHtml.Append("</td></tr></table>");
        String strConteudo = conteudoHtml.ToString();
        return strConteudo;
    }

    private static String GetTableDataFormat(String definicaoCampo, String tipoCampo)
    {
        switch (tipoCampo)
        {
            case "NUM":
            case "DAT":
            case "CAL":
                String pattern = @"(?<format>{0:.+})";
                Match m = Regex.Match(definicaoCampo, pattern);
                return m.Success ? m.Result("${format}") : String.Empty;
            default:
                return String.Empty;
        }
    }

    private void FormatValues()
    {
        foreach (DataRow row in dtCampos.Select("CodigoTipoCampo = 'NUM' OR CodigoTipoCampo = 'DAT' OR CodigoTipoCampo = 'CAL'"))
        {
            Object value = row["ConteudoCampo"];
            String format = row["DefinicaoCampo"].ToString();
            String tipoCampo = row["CodigoTipoCampo"].ToString();
            if (Convert.IsDBNull(value) || String.IsNullOrEmpty(value.ToString()))
                continue;
            CultureInfo ci = new CultureInfo("en-US");
            switch (tipoCampo)
            {
                case "NUM":
                case "CAL":
                    value = Convert.ToDecimal(value, ci);
                    break;
                case "DAT":
                    value = Convert.ToDateTime(value);
                    break;
            }
            row["ConteudoCampo"] = String.Format(format, value);
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
        string resourceFileName = "rel_ImpressaoFormularios.resx";
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
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.imgAssinatura = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine7 = new DevExpress.XtraReports.UI.XRLine();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.subRelAssinaturasFormulario = new DevExpress.XtraReports.UI.XRSubreport();
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
        this.xrLabel3.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ModeloFormulario.NomeFormulario")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(304F, 11.81251F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1402.074F, 150F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLabel2.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Formularios.Rel_Formularios_Abas.Rel_Abas_Campos.NomeCampo")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.ForeColor = System.Drawing.Color.DarkBlue;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1800F, 50F);
        this.xrLabel2.StylePriority.UseBackColor = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Nome Campo";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLine1
        // 
        this.xrLine1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 40F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1800F, 5F);
        // 
        // xrLabel1
        // 
        this.xrLabel1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLabel1.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Formularios.Rel_Formularios_Abas.DescricaoAba")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1800F, 40F);
        this.xrLabel1.StylePriority.UseBackColor = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Nome Aba";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 28.1034F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.StylePriority.UseTextAlignment = false;
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 100F;
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
        this.ParamCodigoFormulario.Type = typeof(int);
        this.ParamCodigoFormulario.ValueInfo = "0";
        this.ParamCodigoFormulario.Visible = false;
        // 
        // xrRichText1
        // 
        this.xrRichText1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrRichText1.BorderColor = System.Drawing.Color.Black;
        this.xrRichText1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
        this.xrRichText1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "Formularios.Rel_Formularios_Abas.Rel_Abas_Campos.ConteudoCampo")});
        this.xrRichText1.Dpi = 254F;
        this.xrRichText1.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.xrRichText1.FormattingRules.Add(this.frSubFormulario);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(1800F, 40F);
        this.xrRichText1.StylePriority.UseBorderColor = false;
        this.xrRichText1.StylePriority.UseBorderDashStyle = false;
        this.xrRichText1.StylePriority.UseBorders = false;
        this.xrRichText1.StylePriority.UseFont = false;
        this.xrRichText1.StylePriority.UsePadding = false;
        // 
        // frSubFormulario
        // 
        this.frSubFormulario.Condition = "[CodigoTipoCampo] == \'SUB\'";
        this.frSubFormulario.DataMember = "Formularios.Rel_Formularios_Abas.Rel_Abas_Campos";
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
        // 
        // 
        // 
        this.frAbaUnica.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
        this.frAbaUnica.Name = "frAbaUnica";
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupFooter1});
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
        this.xrPanel1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrRichText1});
        this.xrPanel1.Dpi = 254F;
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1800F, 100F);
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.GroupUnion = DevExpress.XtraReports.UI.GroupFooterUnion.WithLastDetail;
        this.GroupFooter1.HeightF = 97.89584F;
        this.GroupFooter1.Name = "GroupFooter1";
        this.GroupFooter1.PrintAtBottom = true;
        this.GroupFooter1.StylePriority.UseBorders = false;
        this.GroupFooter1.Visible = false;
        // 
        // xrLabel4
        // 
        this.xrLabel4.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(5.291667F, 0F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1796.167F, 45.1908F);
        this.xrLabel4.Text = "Assinaturas";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left;
        this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "Entidade.LogoEntidade")});
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(14.22135F, 13.8125F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(285.7787F, 150F);
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
        this.xrPageInfo2.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
        this.xrPageInfo2.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrPageInfo2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPageInfo2.BorderWidth = 0.3F;
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo2.Format = "Pág. {0}/{1}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1587.438F, 9.999994F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(212.5625F, 30F);
        this.xrPageInfo2.StylePriority.UseBorderColor = false;
        this.xrPageInfo2.StylePriority.UseBorders = false;
        this.xrPageInfo2.StylePriority.UseBorderWidth = false;
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrPageInfo1.BorderColor = System.Drawing.Color.Gainsboro;
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPageInfo1.BorderWidth = 0.3F;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo1.Format = "Emitido em {0:dd/MM/yyyy - HH:mm}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00004F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(1587.438F, 30F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseBorderWidth = false;
        this.xrPageInfo1.StylePriority.UseFont = false;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgAssinatura,
            this.xrLine6,
            this.xrLine5,
            this.xrLine4,
            this.xrPictureBox1,
            this.xrLabel3,
            this.xrLine7});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 185.8547F;
        this.PageHeader.Name = "PageHeader";
        // 
        // imgAssinatura
        // 
        this.imgAssinatura.Dpi = 254F;
        this.imgAssinatura.Image = ((System.Drawing.Image)(resources.GetObject("imgAssinatura.Image")));
        this.imgAssinatura.LocationFloat = new DevExpress.Utils.PointFloat(1706.969F, 56.61492F);
        this.imgAssinatura.Name = "imgAssinatura";
        this.imgAssinatura.SizeF = new System.Drawing.SizeF(85.80615F, 81.02679F);
        this.imgAssinatura.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // xrLine6
        // 
        this.xrLine6.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right;
        this.xrLine6.Dpi = 254F;
        this.xrLine6.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine6.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine6.LineWidth = 3;
        this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(1793.984F, 2.999999F);
        this.xrLine6.Name = "xrLine6";
        this.xrLine6.SizeF = new System.Drawing.SizeF(5F, 167.9063F);
        this.xrLine6.StylePriority.UseForeColor = false;
        // 
        // xrLine5
        // 
        this.xrLine5.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLine5.Dpi = 254F;
        this.xrLine5.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(6.836227F, 166.8125F);
        this.xrLine5.Name = "xrLine5";
        this.xrLine5.SizeF = new System.Drawing.SizeF(1787.14F, 5F);
        this.xrLine5.StylePriority.UseForeColor = false;
        // 
        // xrLine4
        // 
        this.xrLine4.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLine4.Dpi = 254F;
        this.xrLine4.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(6.735749F, 1.999999F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(1786.249F, 5F);
        this.xrLine4.StylePriority.UseForeColor = false;
        // 
        // xrLine7
        // 
        this.xrLine7.AnchorHorizontal = DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left;
        this.xrLine7.Dpi = 254F;
        this.xrLine7.ForeColor = System.Drawing.Color.Gainsboro;
        this.xrLine7.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine7.LineWidth = 3;
        this.xrLine7.LocationFloat = new DevExpress.Utils.PointFloat(1.418064F, 3.06479F);
        this.xrLine7.Name = "xrLine7";
        this.xrLine7.SizeF = new System.Drawing.SizeF(5F, 168.1661F);
        this.xrLine7.StylePriority.UseForeColor = false;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subRelAssinaturasFormulario});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 58.41997F;
        this.ReportFooter.KeepTogether = true;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // subRelAssinaturasFormulario
        // 
        this.subRelAssinaturasFormulario.Dpi = 254F;
        this.subRelAssinaturasFormulario.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.subRelAssinaturasFormulario.Name = "subRelAssinaturasFormulario";
        this.subRelAssinaturasFormulario.SizeF = new System.Drawing.SizeF(1801.19F, 58.41996F);
        // 
        // rel_ImpressaoFormularios
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.PageFooter,
            this.PageHeader,
            this.ReportFooter});
        this.DataMember = "Formularios";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.frSubFormulario,
            this.frAbaUnica});
        this.Margins = new System.Drawing.Printing.Margins(149, 149, 28, 100);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ParamCodigoFormulario});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 5F;
        this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.SnapToGrid;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "19.1";
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
