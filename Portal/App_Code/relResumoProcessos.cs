using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.Hosting;

/// <summary>
/// Summary description for rel_RiscosProjetos
/// </summary>
public class relResumoProcessos : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;

    dados cDados;
    private XRLabel lblTituloNomeProcesso;

    private DevExpress.XtraReports.Parameters.Parameter pCodigoWorkflow = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoInstanciaWorkFlow = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeInstanciaWf = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pLogoUnidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pDataImpressao = new DevExpress.XtraReports.Parameters.Parameter();
    /// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;
    private XRPictureBox logoUnidade;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRLabel lblTerminoEtapa;
    private XRLabel lblInicioEtapa;
    private XRLabel lblDescricaoEtapa;
    private dsRelResumoProcessosTableAdapters.dtResumoProcessoTableAdapter dtResumoProcessoTableAdapter1;
    private dsRelResumoProcessos dsRelResumoProcessos1;
    private XRLabel lblResponsavelEtapa;
    private XRLabel lblImpressoEm;
    private XRLabel xrLabel1;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRLabel xrLabel2;
    private XRLabel xrLabel6;
    PaddingInfo recuo;
    public relResumoProcessos(int codigoEntidade, int codigoWokflow, int codigoInstanciaWf)
    {
        InitializeComponent();
        cDados = CdadosUtil.GetCdados(codigoEntidade, null);
        recuo = new PaddingInfo(3, 3, 0, 0);

        pCodigoWorkflow.Name = "pCodigoWorkflow";
        pCodigoInstanciaWorkFlow.Name = "pCodigoInstanciaWorkFlow";
        pNomeInstanciaWf.Name = "pNomeInstanciaWf";
        pDataImpressao.Name = "pDataImpressao";
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
       {
            this.pCodigoWorkflow,
            this.pCodigoInstanciaWorkFlow,
            this.pNomeInstanciaWf,
            this.pDataImpressao
       });
        dtResumoProcessoTableAdapter1.Connection.ConnectionString = cDados.classeDados.getStringConexao();
        dtResumoProcessoTableAdapter1.Fill(dsRelResumoProcessos1.dtResumoProcesso, codigoWokflow, codigoInstanciaWf);
        //metaIndicadorUnidadeTableAdapter1.Connection.ConnectionString = cDados.classeDados.getStringConexao();

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
        //string resourceFileName = "relResumoProcessos.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblTerminoEtapa = new DevExpress.XtraReports.UI.XRLabel();
        this.lblInicioEtapa = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDescricaoEtapa = new DevExpress.XtraReports.UI.XRLabel();
        this.lblResponsavelEtapa = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblImpressoEm = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTituloNomeProcesso = new DevExpress.XtraReports.UI.XRLabel();
        this.logoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dtResumoProcessoTableAdapter1 = new dsRelResumoProcessosTableAdapters.dtResumoProcessoTableAdapter();
        this.dsRelResumoProcessos1 = new dsRelResumoProcessos();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelResumoProcessos1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTerminoEtapa,
            this.lblInicioEtapa,
            this.lblDescricaoEtapa,
            this.lblResponsavelEtapa});
        this.Detail.HeightF = 23F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
        // 
        // lblTerminoEtapa
        // 
        this.lblTerminoEtapa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtResumoProcesso.Termino", "{0:dd/MM/yyyy HH:mm}")});
        this.lblTerminoEtapa.LocationFloat = new DevExpress.Utils.PointFloat(454.8749F, 0F);
        this.lblTerminoEtapa.Name = "lblTerminoEtapa";
        this.lblTerminoEtapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTerminoEtapa.SizeF = new System.Drawing.SizeF(150.75F, 23F);
        this.lblTerminoEtapa.StylePriority.UseTextAlignment = false;
        this.lblTerminoEtapa.Text = "lblTerminoEtapa";
        this.lblTerminoEtapa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblInicioEtapa
        // 
        this.lblInicioEtapa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtResumoProcesso.Inicio", "{0:dd/MM/yyyy HH:mm}")});
        this.lblInicioEtapa.LocationFloat = new DevExpress.Utils.PointFloat(301.0417F, 0F);
        this.lblInicioEtapa.Name = "lblInicioEtapa";
        this.lblInicioEtapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblInicioEtapa.SizeF = new System.Drawing.SizeF(153.8333F, 23F);
        this.lblInicioEtapa.StylePriority.UseTextAlignment = false;
        this.lblInicioEtapa.Text = "lblInicioEtapa";
        this.lblInicioEtapa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblDescricaoEtapa
        // 
        this.lblDescricaoEtapa.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblDescricaoEtapa.BorderWidth = 0;
        this.lblDescricaoEtapa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtResumoProcesso.Etapa")});
        this.lblDescricaoEtapa.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblDescricaoEtapa.Name = "lblDescricaoEtapa";
        this.lblDescricaoEtapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDescricaoEtapa.SizeF = new System.Drawing.SizeF(301.0417F, 23F);
        this.lblDescricaoEtapa.StylePriority.UseBorders = false;
        this.lblDescricaoEtapa.StylePriority.UseBorderWidth = false;
        this.lblDescricaoEtapa.StylePriority.UseTextAlignment = false;
        this.lblDescricaoEtapa.Text = "lblDescricaoEtapa";
        this.lblDescricaoEtapa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblResponsavelEtapa
        // 
        this.lblResponsavelEtapa.BackColor = System.Drawing.Color.Transparent;
        this.lblResponsavelEtapa.BorderColor = System.Drawing.Color.Red;
        this.lblResponsavelEtapa.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblResponsavelEtapa.BorderWidth = 2;
        this.lblResponsavelEtapa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtResumoProcesso.ResponsavelEtapa")});
        this.lblResponsavelEtapa.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.lblResponsavelEtapa.LocationFloat = new DevExpress.Utils.PointFloat(605.6249F, 0F);
        this.lblResponsavelEtapa.Name = "lblResponsavelEtapa";
        this.lblResponsavelEtapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblResponsavelEtapa.SizeF = new System.Drawing.SizeF(184.375F, 23F);
        this.lblResponsavelEtapa.StylePriority.UseBackColor = false;
        this.lblResponsavelEtapa.StylePriority.UseBorderColor = false;
        this.lblResponsavelEtapa.StylePriority.UseBorders = false;
        this.lblResponsavelEtapa.StylePriority.UseBorderWidth = false;
        this.lblResponsavelEtapa.StylePriority.UseFont = false;
        this.lblResponsavelEtapa.StylePriority.UseTextAlignment = false;
        this.lblResponsavelEtapa.Text = "lblResponsavelEtapa";
        this.lblResponsavelEtapa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.lblImpressoEm,
            this.lblTituloNomeProcesso,
            this.logoUnidade});
        this.PageHeader.HeightF = 182.75F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        this.PageHeader.AfterPrint += new System.EventHandler(this.PageHeader_AfterPrint);
        // 
        // xrLabel6
        // 
        this.xrLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(789.9999F, 25F);
        this.xrLabel6.StylePriority.UseBackColor = false;
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Resumo de Tramitação de Processo";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel5
        // 
        this.xrLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(605.6248F, 159.75F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(184.3752F, 23F);
        this.xrLabel5.StylePriority.UseBackColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "Responsável";
        // 
        // xrLabel4
        // 
        this.xrLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(454.8749F, 159.75F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(150.7498F, 23F);
        this.xrLabel4.StylePriority.UseBackColor = false;
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "Fim";
        // 
        // xrLabel3
        // 
        this.xrLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(301.0417F, 159.75F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(153.8333F, 23F);
        this.xrLabel3.StylePriority.UseBackColor = false;
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Início";
        // 
        // xrLabel2
        // 
        this.xrLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 159.75F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(301.0417F, 23F);
        this.xrLabel2.StylePriority.UseBackColor = false;
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "Etapa";
        // 
        // xrLabel1
        // 
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(491.3749F, 123.1667F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(165.2916F, 23F);
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Data de Impressão:  ";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // lblImpressoEm
        // 
        this.lblImpressoEm.LocationFloat = new DevExpress.Utils.PointFloat(656.6665F, 123.1667F);
        this.lblImpressoEm.Name = "lblImpressoEm";
        this.lblImpressoEm.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblImpressoEm.SizeF = new System.Drawing.SizeF(133.3334F, 23F);
        this.lblImpressoEm.StylePriority.UseTextAlignment = false;
        this.lblImpressoEm.Text = "28/01/2013 18:18:18";
        this.lblImpressoEm.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        // 
        // lblTituloNomeProcesso
        // 
        this.lblTituloNomeProcesso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
        this.lblTituloNomeProcesso.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblTituloNomeProcesso.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.lblTituloNomeProcesso.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 25F);
        this.lblTituloNomeProcesso.Name = "lblTituloNomeProcesso";
        this.lblTituloNomeProcesso.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTituloNomeProcesso.SizeF = new System.Drawing.SizeF(789.9999F, 25F);
        this.lblTituloNomeProcesso.StylePriority.UseBackColor = false;
        this.lblTituloNomeProcesso.StylePriority.UseBorders = false;
        this.lblTituloNomeProcesso.StylePriority.UseFont = false;
        this.lblTituloNomeProcesso.StylePriority.UseTextAlignment = false;
        this.lblTituloNomeProcesso.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // logoUnidade
        // 
        this.logoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60.41673F);
        this.logoUnidade.Name = "logoUnidade";
        this.logoUnidade.SizeF = new System.Drawing.SizeF(253.4583F, 85.75F);
        this.logoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.HeightF = 30F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.HeightF = 0F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // dtResumoProcessoTableAdapter1
        // 
        this.dtResumoProcessoTableAdapter1.ClearBeforeFill = true;
        // 
        // dsRelResumoProcessos1
        // 
        this.dsRelResumoProcessos1.DataSetName = "dsRelResumoProcessos";
        this.dsRelResumoProcessos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // PageFooter
        // 
        this.PageFooter.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.HeightF = 23F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.StylePriority.UseBorders = false;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Format = "Pág: {0} de {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(789.9998F, 23F);
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // relResumoProcessos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.PageFooter});
        this.DataAdapter = this.dtResumoProcessoTableAdapter1;
        this.DataMember = "dtResumoProcesso";
        this.DataSource = this.dsRelResumoProcessos1;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 0);
        this.Version = "12.2";
        ((System.ComponentModel.ISupportInitialize)(this.dsRelResumoProcessos1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

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
        lblTituloNomeProcesso.Text = pNomeInstanciaWf.Value.ToString();
        lblImpressoEm.Text = pDataImpressao.Value.ToString();
        DataSet dsLogoUnidade = cDados.getLogoEntidade(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "");
        ASPxBinaryImage image1 = new ASPxBinaryImage();
        string montaNomeArquivo = "", montaNomeImagemParametro = "";
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];

                if (image1.ContentBytes != null)
                {
                    string pathArquivo = "logoRelResumoProcesso_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + pathArquivo;
                    montaNomeImagemParametro = @"~\ArquivosTemporarios\" + pathArquivo;
                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();
                    //rel.Parameters["pathArquivo"].Value = montaNomeImagemParametro;
                    logoUnidade.ImageUrl = montaNomeImagemParametro;
                }
            }
            catch (Exception ex)
            {
                string mensage = ex.Message;
            }
        }



        //logoUnidade.ImageUrl = pLogoUnidade.Value.ToString();

    }

    private void PageHeader_AfterPrint(object sender, EventArgs e)
    {

    }

}
