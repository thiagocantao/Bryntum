using DevExpress.XtraReports.UI;
using System;
using System.Data.SqlClient;

/// <summary>
/// Summary description for relAssinaturasFormulario
/// </summary>
public class relAssinaturasFormulario : DevExpress.XtraReports.UI.XtraReport
{
    private readonly int _CodigoFormularioAssinatura;
    private DetailBand Detail;
    private BottomMarginBand BottomMargin;
    private TopMarginBand TopMargin;
    private dsRelSubAssinaturaFormulario dsRelSubAssinaturaFormulario1;
    private PageHeaderBand PageHeader;
    private string codigoFormularioGlobal = "";
    private XRLabel lblTitulo;
    private CalculatedField cfNomeMaisCPF;
    private CalculatedField cfAssinadoEmMaisIDAssinatura;
    private XRLine xrLine1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private CalculatedField cfCPFFormatado;
    private int qtd;
    private XRLine xrLine2;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relAssinaturasFormulario(string codigoFormulario, int codigoFormularioAssinatura)
    {
        _CodigoFormularioAssinatura = codigoFormularioAssinatura;
        codigoFormularioGlobal = codigoFormulario;
        InitializeComponent();
        InitData();
    }

    private void InitData()
    {

        dados cDados = CdadosUtil.GetCdados(null);
        string comandoSQL = string.Format(@"
 SELECT NomeUsuario, CPF, DataAssinatura, ChaveAssinatura, CodigoFormulario 
   FROM {0}.{1}.f_getAssinaturasFormulario({2})
UNION
 SELECT NomeUsuario, CPF, DataInclusaoRegistro as DataAssinatura, ChaveAssinatura, CodigoFormulario 
   FROM Log_FormularioAssinatura l inner join
        Usuario u on u.CodigoUsuario = l.CodigoUsuario
  WHERE CodigoFormularioAssinatura = {3}
    AND (CodigoResultadoAssinatura IS NULL OR CodigoResultadoAssinatura <> 1)
  order by DataAssinatura ASC", cDados.getDbName(), cDados.getDbOwner(), codigoFormularioGlobal, _CodigoFormularioAssinatura);
        string connectionString = cDados.classeDados.getStringConexao();

        SqlDataAdapter adapter = new SqlDataAdapter(comandoSQL, connectionString);

        //adapter.TableMappings.Add("Table", "tbAssinaturas");

        adapter.Fill(dsRelSubAssinaturaFormulario1.tbAssinaturas);
        qtd = dsRelSubAssinaturaFormulario1.Tables[0].Rows.Count;

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
        string resourceFileName = "relAssinaturasFormulario.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.dsRelSubAssinaturaFormulario1 = new dsRelSubAssinaturaFormulario();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.lblTitulo = new DevExpress.XtraReports.UI.XRLabel();
        this.cfNomeMaisCPF = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfAssinadoEmMaisIDAssinatura = new DevExpress.XtraReports.UI.CalculatedField();
        this.cfCPFFormatado = new DevExpress.XtraReports.UI.CalculatedField();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelSubAssinaturaFormulario1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrLabel3,
            this.xrLabel2});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 91.21268F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLine1
        // 
        this.xrLine1.BorderWidth = 1F;
        this.xrLine1.Dpi = 254F;
        this.xrLine1.ForeColor = System.Drawing.Color.LightGray;
        this.xrLine1.LineWidth = 2;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 80.37177F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1800F, 5F);
        this.xrLine1.StylePriority.UseBorderWidth = false;
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrLabel3
        // 
        this.xrLabel3.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbAssinaturas.cfAssinadoEmMaisIDAssinatura")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 36.05468F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1775F, 36.31709F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLabel2
        // 
        this.xrLabel2.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "tbAssinaturas.cfNomeMaisCPF")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(1775F, 31.05466F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "xrLabel2";
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 28.27926F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // dsRelSubAssinaturaFormulario1
        // 
        this.dsRelSubAssinaturaFormulario1.DataSetName = "dsRelSubAssinaturaFormulario";
        this.dsRelSubAssinaturaFormulario1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.lblTitulo});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 51.0148F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33.67379F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(1800F, 5F);
        // 
        // lblTitulo
        // 
        this.lblTitulo.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
        this.lblTitulo.BackColor = System.Drawing.Color.Gainsboro;
        this.lblTitulo.Dpi = 254F;
        this.lblTitulo.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
        this.lblTitulo.ForeColor = System.Drawing.Color.Black;
        this.lblTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblTitulo.Name = "lblTitulo";
        this.lblTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTitulo.SizeF = new System.Drawing.SizeF(1800F, 33.67379F);
        this.lblTitulo.StylePriority.UseBackColor = false;
        this.lblTitulo.StylePriority.UseFont = false;
        this.lblTitulo.StylePriority.UseForeColor = false;
        this.lblTitulo.Text = "Assinatura Digital";
        this.lblTitulo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblTitulo_BeforePrint);
        // 
        // cfNomeMaisCPF
        // 
        this.cfNomeMaisCPF.DataMember = "tbAssinaturas";
        this.cfNomeMaisCPF.Expression = "[cfCPFFormatado] + \' - \' + [NomeUsuario]";
        this.cfNomeMaisCPF.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfNomeMaisCPF.Name = "cfNomeMaisCPF";
        // 
        // cfAssinadoEmMaisIDAssinatura
        // 
        this.cfAssinadoEmMaisIDAssinatura.DataMember = "tbAssinaturas";
        this.cfAssinadoEmMaisIDAssinatura.Expression = " [DataAssinatura] + \' - Chave: \' +  [ChaveAssinatura]";
        this.cfAssinadoEmMaisIDAssinatura.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfAssinadoEmMaisIDAssinatura.Name = "cfAssinadoEmMaisIDAssinatura";
        // 
        // cfCPFFormatado
        // 
        this.cfCPFFormatado.DataMember = "tbAssinaturas";
        this.cfCPFFormatado.Expression = "Insert(Insert(Insert([CPF],3 , \'.\'),7 , \'.\'),11 , \'-\')";
        this.cfCPFFormatado.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfCPFFormatado.Name = "cfCPFFormatado";
        // 
        // relAssinaturasFormulario
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cfNomeMaisCPF,
            this.cfAssinadoEmMaisIDAssinatura,
            this.cfCPFFormatado});
        this.DataMember = "tbAssinaturas";
        this.DataSource = this.dsRelSubAssinaturaFormulario1;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.HorizontalContentSplitting = DevExpress.XtraPrinting.HorizontalContentSplitting.Smart;
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 28);
        this.PageHeight = 2970;
        this.PageWidth = 1800;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.ReportPrintOptions.PrintOnEmptyDataSource = false;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.Version = "19.1";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relAssinaturasFormulario_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.dsRelSubAssinaturaFormulario1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void relAssinaturasFormulario_DataSourceDemanded(object sender, EventArgs e)
    {
        //InitData();
    }

    private void lblTitulo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (qtd > 1)
        {
            lblTitulo.Text = "Assinaturas Digitais";
        }
        else
        {
            lblTitulo.Text = "Assinatura Digital";
        }
    }
}
