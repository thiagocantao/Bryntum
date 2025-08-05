using DevExpress.XtraReports.UI;
using System;
using System.Data.SqlClient;

/// <summary>
/// Summary description for relProjetosEstrategicos
/// </summary>
public class relProjetosEstrategicos : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel1;
    private XRPictureBox xrPictureBox2;
    private XRRichText txtObjetivo;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRLabel xrLabel3;
    private XRLabel xrLabel6;
    private dsRelProjetosEstrategicos dsRelProjetosEstrategicos1;



    private DevExpress.XtraReports.Parameters.Parameter pathArquivo = new DevExpress.XtraReports.Parameters.Parameter();
    private XRTable xrTable3;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRPageInfo xrPageInfo2;
    private XRTable xrTable5;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell23;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    dados cDados;
    int CodigoMapa;
    int CodigoUsuario;
    private XRTableCell xrTableCell17;
    private XRLine xrLine2;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRTable xrTable4;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell16;
    private ReportFooterBand ReportFooter1;
    private XRLabel xrLabel7;
    private XRRichText xrRichText1;
    private GroupHeaderBand GroupHeader1;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox1;
    int Ano;

    public relProjetosEstrategicos(int CodigoMapa, int CodigoUsuario, int Ano)
    {
        this.CodigoMapa = CodigoMapa;
        this.CodigoUsuario = CodigoUsuario;
        this.Ano = Ano;
        InitializeComponent();

        pathArquivo.Name = "pathArquivo";

        cDados = CdadosUtil.GetCdados(null);

        InitData();

        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] { this.pathArquivo });
    }

    private void InitData()
    {
        string connectionString = cDados.classeDados.getStringConexao();
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
        DECLARE @CodigoMapa INT, 
				@CodigoUsuario INT, 
				@Ano INT,
				@CodigoProjeto INT

SET @CodigoMapa = {0}
SET @CodigoUsuario = {1}
SET @Ano = {2}

        DECLARE @tblProjetos TABLE
                (CodigoProjeto INT,
                 NomeProjeto Varchar(500),
                 IndicaProjetoCarteiraPrioritaria Char(1),
                 NomeResponsavel Varchar(150),
                CorDesempenho Varchar(30),
                PercentualConcluido Decimal(10,2),
                PercentualPrevist Int,    
                ObjetivoProjeto Varchar(2000),
                AnaliseCritica Varchar(2000))
   
        DECLARE @tblRetorno TABLE
                (CodigoProjeto Int,
                 NomeTarefa Varchar(255),
                 TerminoPrevisto DateTime,
                 Termino DateTime,
                 TerminoReal DateTime,
                 Desvio Int,
                 StatusTarefa Varchar(20))

 INSERT INTO @tblProjetos
 SELECT CodigoProjeto, 
        Convert(Varchar(500),NomeProjeto), 
        IndicaProjetoCarteiraPrioritaria, 
        NomeResponsavel,  
        CorDesempenho,  
        PercentualConcluido/100. as PercentualConcluido,  
        PercentualPrevisto,  
        Convert(Varchar(2000),ObjetivoProjeto),
        Convert(Varchar(2000),AnaliseCritica)
   FROM dbo.f_cni_GetProjetosMapaEstrategico(@CodigoMapa, @CodigoUsuario, @Ano)
   
DECLARE crs CURSOR FOR 
 SELECT tbl.CodigoProjeto
   FROM @tblProjetos AS tbl
   
   OPEN crs
   
FETCH NEXT FROM crs INTO @CodigoProjeto
    
WHILE @@FETCH_STATUS = 0
BEGIN
 INSERT INTO @tblRetorno
 SELECT CodigoProjeto,
				NomeTarefa, 
				TerminoPrevisto, 
				Termino, 
				TerminoReal, 
				DesvioPrazo AS Desvio, 
				StatusTarefa AS Status
   FROM dbo.f_GetCronogramaProjeto(@CodigoProjeto, GetDate(), -1) f
  WHERE Marco = 1 
  ORDER BY Termino
  
  FETCH NEXT FROM crs INTO @CodigoProjeto
END

 SELECT * FROM @tblProjetos
 
 SELECT * FROM @tblRetorno
 
 CLOSE crs
 DEALLOCATE crs"
            , CodigoMapa
            , CodigoUsuario
            , Ano);

        #endregion

        SqlConnection conn = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter(comandoSql, conn);
        //adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { 
        //    new SqlParameter("@CodigoMapa", CodigoMapa),
        //    new SqlParameter("@CodigoUsuario", CodigoUsuario),
        //    new SqlParameter("@Ano", Ano)});
        adapter.TableMappings.Add("Table", "f_cni_GetProjetosMapaEstrategico");
        adapter.TableMappings.Add("Table1", "f_GetCronogramaProjeto");
        adapter.Fill(dsRelProjetosEstrategicos1);

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
        //string resourceFileName = "relProjetosEstrategicos.resx";
        System.Resources.ResourceManager resources = global::Resources.relProjetosEstrategicos.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.txtObjetivo = new DevExpress.XtraReports.UI.XRRichText();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.dsRelProjetosEstrategicos1 = new dsRelProjetosEstrategicos();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtObjetivo)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelProjetosEstrategicos1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5,
            this.xrPictureBox2,
            this.xrLine2,
            this.xrLabel6,
            this.txtObjetivo});
        this.Detail.HeightF = 102.5053F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrLabel1});
        this.TopMargin.HeightF = 117.75F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 8.25F);
        this.xrPageInfo1.Format = "Emissão: {0:dd/MM/yyyy}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(505.6938F, 81.13274F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(117.7653F, 13.62498F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Arial", 20F);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 75.12495F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(408.1054F, 36.29942F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Boletim de Projetos Estratégicos";
        // 
        // BottomMargin
        // 
        this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2});
        this.BottomMargin.HeightF = 40F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Font = new System.Drawing.Font("Arial", 8F);
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(583.4021F, 6.999969F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(105.6818F, 23.00002F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLine2
        // 
        this.xrLine2.ForeColor = System.Drawing.Color.Maroon;
        this.xrLine2.LineWidth = 2;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(20.31431F, 28.06371F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(668.4233F, 9.458353F);
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // xrTable5
        // 
        this.xrTable5.BackColor = System.Drawing.Color.Transparent;
        this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(20.31431F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable5.SizeF = new System.Drawing.SizeF(668.7698F, 28.06371F);
        this.xrTable5.StylePriority.UseBackColor = false;
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell23,
            this.xrTableCell17});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 11.5D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "f_cni_GetProjetosMapaEstrategico.NomeProjeto")});
        this.xrTableCell20.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.xrTableCell20.ForeColor = System.Drawing.Color.Maroon;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.StylePriority.UseForeColor = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell20.Weight = 0.75052482136400145D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "f_cni_GetProjetosMapaEstrategico.PercentualConcluido", "{0:0%}")});
        this.xrTableCell23.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.xrTableCell23.ForeColor = System.Drawing.Color.Maroon;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.StylePriority.UseForeColor = false;
        this.xrTableCell23.StylePriority.UsePadding = false;
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.Text = "xrTableCell23";
        this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell23.Weight = 0.14528480520483758D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.xrTableCell17.ForeColor = System.Drawing.Color.Maroon;
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UseForeColor = false;
        this.xrTableCell17.StylePriority.UsePadding = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = " Concluído";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell17.Weight = 0.1662154183954084D;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 12F);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 40.52206F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(688.084F, 20.51562F);
        this.xrLabel6.StylePriority.UseBackColor = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "Objetivo";
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.ImageUrl = "~\\imagens\\verdeMenor.gif";
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 10.00001F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(14.91407F, 13.04382F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // txtObjetivo
        // 
        this.txtObjetivo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.txtObjetivo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Rtf", null, "f_cni_GetProjetosMapaEstrategico.ObjetivoProjeto")});
        this.txtObjetivo.Font = new System.Drawing.Font("Arial", 10F);
        this.txtObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 61.03767F);
        this.txtObjetivo.Name = "txtObjetivo";
        this.txtObjetivo.SerializableRtfString = resources.GetString("txtObjetivo.SerializableRtfString");
        this.txtObjetivo.SizeF = new System.Drawing.SizeF(688.1576F, 28.57361F);
        this.txtObjetivo.StylePriority.UseBorders = false;
        this.txtObjetivo.StylePriority.UseFont = false;
        // 
        // xrTable3
        // 
        this.xrTable3.BackColor = System.Drawing.Color.LightGray;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33.94477F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable3.SizeF = new System.Drawing.SizeF(691.0839F, 24.99999F);
        this.xrTable3.StylePriority.UseBackColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Font = new System.Drawing.Font("Arial", 12F);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Marco";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell1.Weight = 1.9655957108607858D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "Termino Previsto";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell2.Weight = 0.50586008955792683D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "Término Real";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell3.Weight = 0.47920936042023593D;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Arial", 12F);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(2.435105F, 12F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(688.6489F, 21.34654F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Principais Marcos";
        // 
        // dsRelProjetosEstrategicos1
        // 
        this.dsRelProjetosEstrategicos1.DataSetName = "dsRelProjetosEstrategicos";
        this.dsRelProjetosEstrategicos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "xrTableCell1";
        this.xrTableCell4.Weight = 1D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "xrTableCell2";
        this.xrTableCell5.Weight = 1D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "xrTableCell3";
        this.xrTableCell6.Weight = 1D;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.ReportFooter1,
            this.GroupHeader1});
        this.DetailReport1.DataMember = "f_cni_GetProjetosMapaEstrategico.f_cni_GetProjetosMapaEstrategico_f_GetCronograma" +
"Projeto";
        this.DetailReport1.DataSource = this.dsRelProjetosEstrategicos1;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.Detail2.HeightF = 24.99999F;
        this.Detail2.Name = "Detail2";
        // 
        // ReportFooter1
        // 
        this.ReportFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrRichText1});
        this.ReportFooter1.HeightF = 116.1153F;
        this.ReportFooter1.Name = "ReportFooter1";
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrTable3});
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 58.94476F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(1.275539E-05F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable4.SizeF = new System.Drawing.SizeF(691.084F, 24.99999F);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell14,
            this.xrTableCell16});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "f_cni_GetProjetosMapaEstrategico.f_cni_GetProjetosMapaEstrategico_f_GetCronograma" +
                    "Projeto.NomeTarefa")});
        this.xrTableCell12.Font = new System.Drawing.Font("Arial", 10F);
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.StylePriority.UsePadding = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell12.Weight = 1.9655636190386177D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "f_cni_GetProjetosMapaEstrategico.f_cni_GetProjetosMapaEstrategico_f_GetCronograma" +
                    "Projeto.TerminoPrevisto", "{0:dd/MM/yyyy}")});
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UsePadding = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell14.Weight = 0.50585241572570772D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "f_cni_GetProjetosMapaEstrategico.f_cni_GetProjetosMapaEstrategico_f_GetCronograma" +
                    "Projeto.TerminoReal", "{0:dd/MM/yyyy}")});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell16.Weight = 0.4792016621561887D;
        // 
        // xrRichText1
        // 
        this.xrRichText1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrRichText1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Html", null, "f_cni_GetProjetosMapaEstrategico.AnaliseCritica")});
        this.xrRichText1.Font = new System.Drawing.Font("Arial", 10F);
        this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46.08334F);
        this.xrRichText1.Name = "xrRichText1";
        this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
        this.xrRichText1.SizeF = new System.Drawing.SizeF(689.084F, 65.03194F);
        this.xrRichText1.StylePriority.UseBorders = false;
        this.xrRichText1.StylePriority.UseFont = false;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 12F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(689.084F, 21.34654F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "Análise Crítica";
        // 
        // PageFooter
        // 
        this.PageFooter.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.PageFooter.HeightF = 79.58334F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.StylePriority.UseBorders = false;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(546.3406F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(142.3971F, 79.58334F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // relProjetosEstrategicos
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport1,
            this.PageFooter});
        this.DataMember = "f_cni_GetProjetosMapaEstrategico";
        this.DataSource = this.dsRelProjetosEstrategicos1;
        this.Margins = new System.Drawing.Printing.Margins(40, 40, 118, 40);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.SnapGridSize = 3F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "12.2";
        this.Watermark.Image = ((System.Drawing.Image)(resources.GetObject("relProjetosEstrategicos.Watermark.Image")));
        this.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relProjetosEstrategicos_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtObjetivo)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelProjetosEstrategicos1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void imgLogoEntidade_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //imgLogoEntidade.ImageUrl = pathArquivo.Value.ToString();
    }

    private void xrLabel7_TextChanged(object sender, EventArgs e)
    {
        //string cor = ((XRLabel)sender).Text.Trim();

        //if (cor == "Vermelho")
        //    imgDesempenhoProjeto.ImageUrl = "~/imagens/vermelho.gif";
        //if (cor == "Verde")
        //    imgDesempenhoProjeto.ImageUrl = "~/imagens/verde.gif";
        //if (cor == "Amarelo")
        //    imgDesempenhoProjeto.ImageUrl = "~/imagens/amarelo.gif";
        //if (cor == "Azul")
        //    imgDesempenhoProjeto.ImageUrl = "~/imagens/Azul.gif";
    }

    private void relProjetosEstrategicos_DataSourceDemanded(object sender, EventArgs e)
    {

    }
}
