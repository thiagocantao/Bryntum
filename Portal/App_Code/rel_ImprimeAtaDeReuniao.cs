using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_ImpressaoFormularios
/// </summary>
public class rel_ImprimeAtaDeReuniao : DevExpress.XtraReports.UI.XtraReport
{
    private TopMarginBand topMarginBand1;
    private DetailBand detailBand1;
    private BottomMarginBand bottomMarginBand1;

    private Parameter pCodigoEntidade = new Parameter();
    private Parameter pNomeUnidade = new Parameter();

    #region Fields

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRLabel lblCabecalho;
    private XRPictureBox imgLogoUnidade;
    //private XRChart graficoBarras = new XRChart();
    private XRChart graficoBarrasEmpilhadas = new XRChart();
    private XRChart graficoPizzaSituacaoAtual = new XRChart();

    private XRLine linha = new XRLine();
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private DetailReportBand DetailReport;
    private DetailBand Detail;
    private dsAtaDeReuniao dsAtaDeReuniao1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell1;
    private DetailReportBand detalheParticipanteEvento;
    private DetailBand Detail5;
    private ReportHeaderBand rh_detalheParticipanteEvento;
    private XRLabel xrLabel1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private DetailReportBand detalheEncaminhamentos;
    private DetailBand Detail2;
    private ReportHeaderBand rh_detalheEncaminhamentos;
    private XRLabel xrLabel2;
    private DetailReportBand detalheDadosGerais;
    private DetailBand Detail1;
    private ReportHeaderBand rh_DetalheDadosGerais;
    private DetailReportBand rh_detalheObjetivos;
    private DetailBand Detail3;
    private ReportHeaderBand ReportHeader3;
    private XRLabel lblObjetivo;
    private DetailReportBand detalhePauta;
    private DetailBand Detail4;
    private ReportHeaderBand rh_DetalhePauta;
    private XRLabel lblPauta;
    private DetailReportBand detalheResenha;
    private DetailBand Detail6;
    private ReportHeaderBand rh_detalheResenha;
    private XRLabel xrLabel9;
    private XRLabel xrLabel3;
    private XRLabel lblNomeProjeto;
    private XRLabel lblDescricaoResumida;
    private XRLabel lblTipoReuniao;
    private XRLabel lblNomeUnidade;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell8;
    private XRTableCell celulaTerminoReal;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell16;

    private dados cDados = CdadosUtil.GetCdados(null);

    #endregion

    private XRRichText rtfDetalhesPauta;
    private XRRichText rtfDetalhesAta;
    private XRLabel lblTerminoReal;
    private XRLabel lblInicioReal;

    #region Constructors

    public rel_ImprimeAtaDeReuniao(int codigoUsuarioResponsavel, int codigoEntidadeUsuarioResponsavel, int codigoProjeto, int codigoEvento, string moduloSistema)
    {
        InitializeComponent();

        pCodigoEntidade.Name = "pCodigoEntidade";
        pNomeUnidade.Name = "pNomeUnidade";

        this.Parameters.AddRange(new Parameter[]
        {
            this.pCodigoEntidade,this.pNomeUnidade
        });

        InitData(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, codigoEvento, moduloSistema);
    }

    private void InitData(int codigoUsuarioResponsavel, int codigoEntidadeUsuarioResponsavel, int codigoProjeto, int codigoEvento, string moduloSistema)
    {
        DataSet dsDetalheReuniao = new DataSet();

        DataSet ds = new DataSet();

        string whereReunioes = string.Format(@"
                AND tev.CodigoModuloSistema = '{0}'
	            AND ev.CodigoObjetoAssociado = {1}", moduloSistema, codigoProjeto.ToString());

        string comandoSQL = String.Format(@"
        SELECT u.CodigoUsuario, u.NomeUsuario,u.EMail,un.NomeUnidadeNegocio as Unidade
                    FROM {0}.{1}.Usuario u INNER JOIN
		                 {0}.{1}.ParticipanteEvento pe ON pe.CodigoParticipante = u.CodigoUsuario INNER JOIN
                         {0}.{1}.Evento e on (pe.CodigoEvento = e.CodigoEvento) INNER JOIN
                         {0}.{1}.Projeto p on (e.CodigoTipoAssociacao = (SELECT CodigoTipoAssociacao 
                                                                           FROM TipoAssociacao  
                                                                          WHERE IniciaisTipoAssociacao = 'PR') AND 
                                                                                e.CodigoObjetoAssociado = p.CodigoProjeto)
        LEFT JOIN {0}.{1}.RecursoCorporativo AS rc ON (rc.CodigoUsuario = u.CodigoUsuario
                                                               AND rc.CodigoEntidade = p.CodigoEntidade) LEFT JOIN                                                                                             
                  {0}.{1}.Unidadenegocio un on (un.CodigoUnidadeNegocio = rc.CodigoUnidadeNegocio)
                    WHERE e.CodigoEvento = {2}
                    ORDER BY u.NomeUsuario        
       

        SELECT ttd.CodigoTarefa, 
               ttd.DescricaoTarefa, 
               ttd.TerminoPrevisto, 
               ttd.CodigoUsuarioResponsavelTarefa,
                us.NomeUsuario as responsavel,
               ttd.CodigoStatusTarefa,
                st.DescricaoStatusTarefa 
          FROM {0}.{1}.TarefaToDoList ttd INNER JOIN
               {0}.{1}.ToDoList AS tdl ON (tdl.CodigoToDoList = ttd.CodigoToDoList
                                                  AND tdl.CodigoObjetoAssociado = {2}) INNER JOIN
               {0}.{1}.TipoAssociacao AS ta ON (ta.CodigoTipoAssociacao = tdl.CodigoTipoAssociacao
                                                       AND ta.IniciaisTipoAssociacao = 'RE') INNER JOIN 
               {0}.{1}.Usuario us on (us.CodigoUsuario = ttd.CodigoUsuarioResponsavelTarefa) INNER JOIN 
               {0}.{1}.StatusTarefa st on (st.CodigoStatusTarefa = ttd.CodigoStatusTarefa)

                    SELECT   CodigoEvento,
                             DescricaoResumida,
                             CodigoResponsavelEvento,
			                 LocalEvento,
                             Pauta,
                             ResumoEvento,
                             ev.CodigoTipoEvento,
                             ev.CodigoObjetoAssociado,
                             ta.IniciaisTipoAssociacao,
                             p.NomeProjeto,
                             tev.DescricaoTipoEvento
                    FROM {0}.{1}.Evento AS ev INNER JOIN 
                         {0}.{1}.TipoEvento AS tev ON ev.CodigoTipoEvento = tev.CodigoTipoEvento INNER JOIN
                         {0}.{1}.TipoAssociacao AS ta ON ta.CodigoTipoAssociacao = ev.CodigoTipoAssociacao INNER JOIN
                         {0}.{1}.Projeto as p on p.CodigoProjeto = ev.CodigoObjetoAssociado
                    WHERE ev.CodigoEntidade = {4}
                      and ev.CodigoObjetoAssociado = {3}
                      and ev.CodigoEvento = {2}
                    ORDER BY InicioPrevisto", cDados.getDbName(), cDados.getDbOwner(), codigoEvento, codigoProjeto, codigoEntidadeUsuarioResponsavel);
        ds = cDados.getDataSet(comandoSQL);

        string comandoSQLReuniao = String.Format(@"SELECT   CodigoEvento,
                             DescricaoResumida,
                             CodigoResponsavelEvento,
			                 LocalEvento,
                             Pauta,
                             ResumoEvento,
                             ev.CodigoTipoEvento,
                             ev.CodigoObjetoAssociado,
                             ta.IniciaisTipoAssociacao,
                             p.NomeProjeto,
                             tev.DescricaoTipoEvento,
                             un.NomeUnidadeNegocio,
                             ev.InicioReal,
                             ev.TerminoReal
                    FROM {0}.{1}.Evento AS ev INNER JOIN 
                         {0}.{1}.TipoEvento AS tev ON ev.CodigoTipoEvento = tev.CodigoTipoEvento INNER JOIN
                         {0}.{1}.TipoAssociacao AS ta ON ta.CodigoTipoAssociacao = ev.CodigoTipoAssociacao INNER JOIN
                         {0}.{1}.Projeto as p on p.CodigoProjeto = ev.CodigoObjetoAssociado LEFT JOIN
                         {0}.{1}.UnidadeNegocio as un on (un.CodigoUnidadeNegocio = p.CodigoUnidadeNegocio)
                    WHERE ev.CodigoEntidade = {4}
                      and ev.CodigoObjetoAssociado = {3}
                      and ev.CodigoEvento = {2}
                    ORDER BY InicioPrevisto", cDados.getDbName(), cDados.getDbOwner(), codigoEvento, codigoProjeto, codigoEntidadeUsuarioResponsavel);
        dsDetalheReuniao = cDados.getDataSet(comandoSQLReuniao);

        if (cDados.DataSetOk(dsDetalheReuniao) && cDados.DataTableOk(dsDetalheReuniao.Tables[0]))
        {
            rtfDetalhesPauta.Html = dsDetalheReuniao.Tables[0].Rows[0]["Pauta"].ToString();

            rtfDetalhesAta.Html = dsDetalheReuniao.Tables[0].Rows[0]["ResumoEvento"].ToString();
            lblNomeProjeto.Text = dsDetalheReuniao.Tables[0].Rows[0]["NomeProjeto"].ToString();
            lblDescricaoResumida.Text = dsDetalheReuniao.Tables[0].Rows[0]["DescricaoResumida"].ToString();
            lblTipoReuniao.Text = dsDetalheReuniao.Tables[0].Rows[0]["DescricaoTipoEvento"].ToString();
            lblNomeUnidade.Text = dsDetalheReuniao.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();

            lblInicioReal.Text = "Início Real: " + dsDetalheReuniao.Tables[0].Rows[0]["InicioReal"].ToString();
            lblTerminoReal.Text = "Término Real: " + dsDetalheReuniao.Tables[0].Rows[0]["TerminoReal"].ToString();
        }

        imgLogoUnidade.Image = ObtemLogoUnidade(codigoUsuarioResponsavel);
        dsAtaDeReuniao1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "ParticipanteEvento", "TarefaToDoList", "Evento");

    }

    private Image ObtemLogoUnidade(int codigoUsuario)
    {
        int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codEntidade, "");

        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
            Image logo = Image.FromStream(stream);
            return logo;
        }
        else
            return null;
    }

    #endregion

    #region Methods

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
            string resourceFileName = "rel_ImprimeAtaDeReuniao.resx";
            System.Resources.ResourceManager resources = global::Resources.rel_ImprimeAtaDeReuniao.ResourceManager;
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.imgLogoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lblCabecalho = new DevExpress.XtraReports.UI.XRLabel();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.detalheParticipanteEvento = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.rh_detalheParticipanteEvento = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.dsAtaDeReuniao1 = new dsAtaDeReuniao();
            this.detalheEncaminhamentos = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.celulaTerminoReal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.rh_detalheEncaminhamentos = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.detalheDadosGerais = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.lblTerminoReal = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInicioReal = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTipoReuniao = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNomeUnidade = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
            this.rh_DetalheDadosGerais = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.rh_detalheObjetivos = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.lblDescricaoResumida = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader3 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblObjetivo = new DevExpress.XtraReports.UI.XRLabel();
            this.detalhePauta = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
            this.rtfDetalhesPauta = new DevExpress.XtraReports.UI.XRRichText();
            this.rh_DetalhePauta = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblPauta = new DevExpress.XtraReports.UI.XRLabel();
            this.detalheResenha = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
            this.rtfDetalhesAta = new DevExpress.XtraReports.UI.XRRichText();
            this.rh_detalheResenha = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAtaDeReuniao1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtfDetalhesPauta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtfDetalhesAta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 254F;
            this.topMarginBand1.HeightF = 0F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // imgLogoUnidade
            // 
            this.imgLogoUnidade.BorderColor = System.Drawing.Color.LightGray;
            this.imgLogoUnidade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.imgLogoUnidade.Dpi = 254F;
            this.imgLogoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(10.5833F, 47.00003F);
            this.imgLogoUnidade.Name = "imgLogoUnidade";
            this.imgLogoUnidade.SizeF = new System.Drawing.SizeF(329.8866F, 171.2549F);
            this.imgLogoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.imgLogoUnidade.StylePriority.UseBorderColor = false;
            this.imgLogoUnidade.StylePriority.UseBorders = false;
            // 
            // lblCabecalho
            // 
            this.lblCabecalho.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.lblCabecalho.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.lblCabecalho.Dpi = 254F;
            this.lblCabecalho.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCabecalho.ForeColor = System.Drawing.Color.Black;
            this.lblCabecalho.LocationFloat = new DevExpress.Utils.PointFloat(347.4699F, 175.6926F);
            this.lblCabecalho.Name = "lblCabecalho";
            this.lblCabecalho.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCabecalho.SizeF = new System.Drawing.SizeF(1549.524F, 42.56227F);
            this.lblCabecalho.StylePriority.UseBorderDashStyle = false;
            this.lblCabecalho.StylePriority.UseBorders = false;
            this.lblCabecalho.StylePriority.UseFont = false;
            this.lblCabecalho.StylePriority.UseForeColor = false;
            this.lblCabecalho.StylePriority.UseTextAlignment = false;
            this.lblCabecalho.Text = "ATA DE REUNIÃO";
            this.lblCabecalho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // detailBand1
            // 
            this.detailBand1.Dpi = 254F;
            this.detailBand1.Font = new System.Drawing.Font("Verdana", 9F);
            this.detailBand1.HeightF = 0F;
            this.detailBand1.Name = "detailBand1";
            this.detailBand1.StylePriority.UseFont = false;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 254F;
            this.bottomMarginBand1.HeightF = 35.36057F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgLogoUnidade,
            this.lblCabecalho});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 237.7582F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.Dpi = 254F;
            this.PageFooter.HeightF = 58.42F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Dpi = 254F;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1649F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.Number;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(253F, 58.42F);
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.detalheParticipanteEvento,
            this.detalheEncaminhamentos,
            this.detalheDadosGerais,
            this.rh_detalheObjetivos,
            this.detalhePauta,
            this.detalheResenha});
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // detalheParticipanteEvento
            // 
            this.detalheParticipanteEvento.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.rh_detalheParticipanteEvento});
            this.detalheParticipanteEvento.DataMember = "ParticipanteEvento";
            this.detalheParticipanteEvento.DataSource = this.dsAtaDeReuniao1;
            this.detalheParticipanteEvento.Dpi = 254F;
            this.detalheParticipanteEvento.Level = 1;
            this.detalheParticipanteEvento.Name = "detalheParticipanteEvento";
            // 
            // Detail5
            // 
            this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail5.Dpi = 254F;
            this.Detail5.HeightF = 30F;
            this.Detail5.KeepTogetherWithDetailReports = true;
            this.Detail5.Name = "Detail5";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(7.629395E-06F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1902F, 30F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell1,
            this.xrTableCell4});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 0.5679012345679012D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ParticipanteEvento.NomeUsuario")});
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 0.28635847510000606D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ParticipanteEvento.Unidade")});
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 0.28635847510000595D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ParticipanteEvento.EMail")});
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.28635854857530407D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "_________________________";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.28635854857530407D;
            // 
            // rh_detalheParticipanteEvento
            // 
            this.rh_detalheParticipanteEvento.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrTable3});
            this.rh_detalheParticipanteEvento.Dpi = 254F;
            this.rh_detalheParticipanteEvento.HeightF = 147.5F;
            this.rh_detalheParticipanteEvento.KeepTogether = true;
            this.rh_detalheParticipanteEvento.Name = "rh_detalheParticipanteEvento";
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1902F, 58.42F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "2.  Participantes";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Dpi = 254F;
            this.xrTable3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 84F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1902F, 63.5F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow3.Dpi = 254F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 0.5679012345679012D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBackColor = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Nome";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell9.Weight = 0.28635851183765504D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBackColor = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Unidade";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell10.Weight = 0.28635851183765504D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBackColor = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "E-Mail";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell11.Weight = 0.28635851183765504D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell12.Dpi = 254F;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBackColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Assinatura";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.28635851183765504D;
            // 
            // dsAtaDeReuniao1
            // 
            this.dsAtaDeReuniao1.DataSetName = "dsAtaDeReuniao";
            this.dsAtaDeReuniao1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // detalheEncaminhamentos
            // 
            this.detalheEncaminhamentos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.rh_detalheEncaminhamentos});
            this.detalheEncaminhamentos.DataMember = "TarefaToDoList";
            this.detalheEncaminhamentos.DataSource = this.dsAtaDeReuniao1;
            this.detalheEncaminhamentos.Dpi = 254F;
            this.detalheEncaminhamentos.Level = 5;
            this.detalheEncaminhamentos.Name = "detalheEncaminhamentos";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail2.Dpi = 254F;
            this.Detail2.HeightF = 27.03041F;
            this.Detail2.KeepTogether = true;
            this.Detail2.KeepTogetherWithDetailReports = true;
            this.Detail2.Name = "Detail2";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1902F, 27.03041F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.celulaTerminoReal,
            this.xrTableCell14,
            this.xrTableCell15});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.5679012345679012D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TarefaToDoList.DescricaoTarefa")});
            this.xrTableCell8.Dpi = 254F;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell8.Weight = 5.339539148712559D;
            // 
            // celulaTerminoReal
            // 
            this.celulaTerminoReal.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.celulaTerminoReal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TarefaToDoList.TerminoPrevisto", "{0:dd/MM/yyyy}")});
            this.celulaTerminoReal.Dpi = 254F;
            this.celulaTerminoReal.Name = "celulaTerminoReal";
            this.celulaTerminoReal.StylePriority.UseBorders = false;
            this.celulaTerminoReal.StylePriority.UseTextAlignment = false;
            this.celulaTerminoReal.Text = "celulaTerminoReal";
            this.celulaTerminoReal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.celulaTerminoReal.Weight = 2.2693149047145953D;
            this.celulaTerminoReal.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.celulaTerminoReal_EvaluateBinding);
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TarefaToDoList.Responsavel")});
            this.xrTableCell14.Dpi = 254F;
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell14.Weight = 3.0591452948469526D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TarefaToDoList.DescricaoStatusTarefa")});
            this.xrTableCell15.Dpi = 254F;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "xrTableCell15";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell15.Weight = 3.5560006517258933D;
            // 
            // rh_detalheEncaminhamentos
            // 
            this.rh_detalheEncaminhamentos.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4,
            this.xrLabel2});
            this.rh_detalheEncaminhamentos.Dpi = 254F;
            this.rh_detalheEncaminhamentos.HeightF = 140.2308F;
            this.rh_detalheEncaminhamentos.KeepTogether = true;
            this.rh_detalheEncaminhamentos.Name = "rh_detalheEncaminhamentos";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Dpi = 254F;
            this.xrTable4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 76.73077F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1902F, 63.5F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell16});
            this.xrTableRow4.Dpi = 254F;
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 0.5679012345679012D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell5.Dpi = 254F;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBackColor = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Tarefa";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell5.Weight = 5.339539148712559D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell6.Dpi = 254F;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBackColor = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Término Previsto";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 2.2693149047145953D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell7.Dpi = 254F;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBackColor = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Responsável";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 3.0591452948469526D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrTableCell16.Dpi = 254F;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBackColor = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "Situação/Status";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell16.Weight = 3.5560006517258933D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 18F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1902F, 58.42F);
            this.xrLabel2.StylePriority.UseBackColor = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "6.  Encaminhamentos";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // detalheDadosGerais
            // 
            this.detalheDadosGerais.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.rh_DetalheDadosGerais});
            this.detalheDadosGerais.Dpi = 254F;
            this.detalheDadosGerais.Level = 0;
            this.detalheDadosGerais.Name = "detalheDadosGerais";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTerminoReal,
            this.lblInicioReal,
            this.lblTipoReuniao,
            this.lblNomeUnidade,
            this.lblNomeProjeto});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 88.42737F;
            this.Detail1.Name = "Detail1";
            // 
            // lblTerminoReal
            // 
            this.lblTerminoReal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblTerminoReal.Dpi = 254F;
            this.lblTerminoReal.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblTerminoReal.LocationFloat = new DevExpress.Utils.PointFloat(970.8478F, 29.3158F);
            this.lblTerminoReal.Name = "lblTerminoReal";
            this.lblTerminoReal.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTerminoReal.SizeF = new System.Drawing.SizeF(931.1523F, 29.31583F);
            this.lblTerminoReal.StylePriority.UseBorders = false;
            this.lblTerminoReal.StylePriority.UseFont = false;
            this.lblTerminoReal.StylePriority.UseTextAlignment = false;
            this.lblTerminoReal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblInicioReal
            // 
            this.lblInicioReal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblInicioReal.Dpi = 254F;
            this.lblInicioReal.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblInicioReal.LocationFloat = new DevExpress.Utils.PointFloat(970.8478F, 0F);
            this.lblInicioReal.Name = "lblInicioReal";
            this.lblInicioReal.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInicioReal.SizeF = new System.Drawing.SizeF(931.1523F, 29.31583F);
            this.lblInicioReal.StylePriority.UseBorders = false;
            this.lblInicioReal.StylePriority.UseFont = false;
            this.lblInicioReal.StylePriority.UseTextAlignment = false;
            this.lblInicioReal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblTipoReuniao
            // 
            this.lblTipoReuniao.Dpi = 254F;
            this.lblTipoReuniao.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblTipoReuniao.LocationFloat = new DevExpress.Utils.PointFloat(0F, 59.11154F);
            this.lblTipoReuniao.Name = "lblTipoReuniao";
            this.lblTipoReuniao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTipoReuniao.SizeF = new System.Drawing.SizeF(951.4998F, 26.66999F);
            this.lblTipoReuniao.StylePriority.UseFont = false;
            this.lblTipoReuniao.StylePriority.UseTextAlignment = false;
            this.lblTipoReuniao.Text = "Motivo da reunião";
            this.lblTipoReuniao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblTipoReuniao.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblTipoReuniao_BeforePrint);
            // 
            // lblNomeUnidade
            // 
            this.lblNomeUnidade.Dpi = 254F;
            this.lblNomeUnidade.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeUnidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 29.79162F);
            this.lblNomeUnidade.Name = "lblNomeUnidade";
            this.lblNomeUnidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblNomeUnidade.SizeF = new System.Drawing.SizeF(951.5F, 29.32F);
            this.lblNomeUnidade.StylePriority.UseFont = false;
            this.lblNomeUnidade.StylePriority.UseTextAlignment = false;
            this.lblNomeUnidade.Text = "Nome da unidade";
            this.lblNomeUnidade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblNomeUnidade.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblNomeUnidade_BeforePrint);
            // 
            // lblNomeProjeto
            // 
            this.lblNomeProjeto.Dpi = 254F;
            this.lblNomeProjeto.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblNomeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblNomeProjeto.Name = "lblNomeProjeto";
            this.lblNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblNomeProjeto.SizeF = new System.Drawing.SizeF(951.5F, 29.31583F);
            this.lblNomeProjeto.StylePriority.UseFont = false;
            this.lblNomeProjeto.StylePriority.UseTextAlignment = false;
            this.lblNomeProjeto.Text = "Nome do projeto";
            this.lblNomeProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblNomeProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblNomeProjeto_BeforePrint);
            // 
            // rh_DetalheDadosGerais
            // 
            this.rh_DetalheDadosGerais.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
            this.rh_DetalheDadosGerais.Dpi = 254F;
            this.rh_DetalheDadosGerais.HeightF = 80.42001F;
            this.rh_DetalheDadosGerais.KeepTogether = true;
            this.rh_DetalheDadosGerais.Name = "rh_DetalheDadosGerais";
            // 
            // xrLabel3
            // 
            this.xrLabel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.00002F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(1902F, 58.41999F);
            this.xrLabel3.StylePriority.UseBackColor = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "1.  Dados gerais da reunião";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rh_detalheObjetivos
            // 
            this.rh_detalheObjetivos.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.ReportHeader3});
            this.rh_detalheObjetivos.Dpi = 254F;
            this.rh_detalheObjetivos.Level = 2;
            this.rh_detalheObjetivos.Name = "rh_detalheObjetivos";
            // 
            // Detail3
            // 
            this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDescricaoResumida});
            this.Detail3.Dpi = 254F;
            this.Detail3.HeightF = 24.09568F;
            this.Detail3.KeepTogetherWithDetailReports = true;
            this.Detail3.Name = "Detail3";
            // 
            // lblDescricaoResumida
            // 
            this.lblDescricaoResumida.Dpi = 254F;
            this.lblDescricaoResumida.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricaoResumida.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblDescricaoResumida.Name = "lblDescricaoResumida";
            this.lblDescricaoResumida.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblDescricaoResumida.SizeF = new System.Drawing.SizeF(1902F, 24.09568F);
            this.lblDescricaoResumida.StylePriority.UseFont = false;
            this.lblDescricaoResumida.Text = "descrição resumida";
            // 
            // ReportHeader3
            // 
            this.ReportHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblObjetivo});
            this.ReportHeader3.Dpi = 254F;
            this.ReportHeader3.HeightF = 79.42F;
            this.ReportHeader3.KeepTogether = true;
            this.ReportHeader3.Name = "ReportHeader3";
            // 
            // lblObjetivo
            // 
            this.lblObjetivo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblObjetivo.Dpi = 254F;
            this.lblObjetivo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21F);
            this.lblObjetivo.Name = "lblObjetivo";
            this.lblObjetivo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblObjetivo.SizeF = new System.Drawing.SizeF(1902F, 58.42F);
            this.lblObjetivo.StylePriority.UseBackColor = false;
            this.lblObjetivo.StylePriority.UseFont = false;
            this.lblObjetivo.StylePriority.UseTextAlignment = false;
            this.lblObjetivo.Text = "3.  Objetivos";
            this.lblObjetivo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // detalhePauta
            // 
            this.detalhePauta.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.rh_DetalhePauta});
            this.detalhePauta.Dpi = 254F;
            this.detalhePauta.Level = 3;
            this.detalhePauta.Name = "detalhePauta";
            // 
            // Detail4
            // 
            this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtfDetalhesPauta});
            this.Detail4.Dpi = 254F;
            this.Detail4.HeightF = 36.96729F;
            this.Detail4.KeepTogether = true;
            this.Detail4.KeepTogetherWithDetailReports = true;
            this.Detail4.Name = "Detail4";
            // 
            // rtfDetalhesPauta
            // 
            this.rtfDetalhesPauta.Dpi = 254F;
            this.rtfDetalhesPauta.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.rtfDetalhesPauta.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.rtfDetalhesPauta.Name = "rtfDetalhesPauta";
            this.rtfDetalhesPauta.SerializableRtfString = resources.GetString("rtfDetalhesPauta.SerializableRtfString");
            this.rtfDetalhesPauta.SizeF = new System.Drawing.SizeF(1902F, 36.96729F);
            // 
            // rh_DetalhePauta
            // 
            this.rh_DetalhePauta.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblPauta});
            this.rh_DetalhePauta.Dpi = 254F;
            this.rh_DetalhePauta.HeightF = 79.42F;
            this.rh_DetalhePauta.KeepTogether = true;
            this.rh_DetalhePauta.Name = "rh_DetalhePauta";
            // 
            // lblPauta
            // 
            this.lblPauta.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblPauta.Dpi = 254F;
            this.lblPauta.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPauta.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21F);
            this.lblPauta.Name = "lblPauta";
            this.lblPauta.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblPauta.SizeF = new System.Drawing.SizeF(1902F, 58.42F);
            this.lblPauta.StylePriority.UseBackColor = false;
            this.lblPauta.StylePriority.UseFont = false;
            this.lblPauta.StylePriority.UseTextAlignment = false;
            this.lblPauta.Text = "4.  Pauta";
            this.lblPauta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // detalheResenha
            // 
            this.detalheResenha.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.rh_detalheResenha});
            this.detalheResenha.Dpi = 254F;
            this.detalheResenha.Level = 4;
            this.detalheResenha.Name = "detalheResenha";
            // 
            // Detail6
            // 
            this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rtfDetalhesAta});
            this.Detail6.Dpi = 254F;
            this.Detail6.HeightF = 36.96729F;
            this.Detail6.KeepTogetherWithDetailReports = true;
            this.Detail6.Name = "Detail6";
            // 
            // rtfDetalhesAta
            // 
            this.rtfDetalhesAta.Dpi = 254F;
            this.rtfDetalhesAta.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.rtfDetalhesAta.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.rtfDetalhesAta.Name = "rtfDetalhesAta";
            this.rtfDetalhesAta.SerializableRtfString = resources.GetString("rtfDetalhesAta.SerializableRtfString");
            this.rtfDetalhesAta.SizeF = new System.Drawing.SizeF(1902F, 36.96729F);
            this.rtfDetalhesAta.StylePriority.UseFont = false;
            // 
            // rh_detalheResenha
            // 
            this.rh_detalheResenha.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9});
            this.rh_detalheResenha.Dpi = 254F;
            this.rh_detalheResenha.HeightF = 63.54838F;
            this.rh_detalheResenha.KeepTogether = true;
            this.rh_detalheResenha.Name = "rh_detalheResenha";
            // 
            // xrLabel9
            // 
            this.xrLabel9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 18F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(1902F, 45.54838F);
            this.xrLabel9.StylePriority.UseBackColor = false;
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "5.  Resenha/Decisões tomadas";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rel_ImprimeAtaDeReuniao
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.PageHeader,
            this.PageFooter,
            this.DetailReport});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(99, 99, 0, 35);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.SnapToGrid;
            this.Version = "19.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAtaDeReuniao1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtfDetalhesPauta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtfDetalhesAta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        /*string mesAno = cDados.classeDados.getDateDB();
        string mesAnoFormatado = mesAno.Substring(3, 7);
        string mesporextenso = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        int ano = int.Parse(mesAnoFormatado.Substring(3, 4));
        if (mes == 1)
        {
            mesporextenso = "Janeiro";
        }
        else if (mes == 2)
        {
            mesporextenso = "Fevereiro";
        }
        else if (mes == 3)
        {
            mesporextenso = "Março";
        }
        else if (mes == 4)
        {
            mesporextenso = "Abril";
        }
        else if (mes == 5)
        {
            mesporextenso = "Maio";
        }
        else if (mes == 6)
        {
            mesporextenso = "Junho";
        }
        else if (mes == 7)
        {
            mesporextenso = "Julho";
        }
        else if (mes == 8)
        {
            mesporextenso = "Agosto";
        }
        else if (mes == 9)
        {
            mesporextenso = "Setembro";
        }
        else if (mes == 10)
        {
            mesporextenso = "Outubro";
        }
        else if (mes == 11)
        {
            mesporextenso = "Novembro";
        }
        else if (mes == 12)
        {
            mesporextenso = "Dezembro";
        }*/

        //lblCabecalho.Text = "Ata de Reunião - " + mesporextenso + "/" + ano.ToString();

    }

    private void lblNomeProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblNomeProjeto.Text = "Projeto/Demanda: " + lblNomeProjeto.Text;
    }

    private void lblNomeUnidade_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblNomeUnidade.Text = "Unidade: " + lblNomeUnidade.Text;
    }

    private void lblTipoReuniao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblTipoReuniao.Text = "Motivo: " + lblTipoReuniao.Text;
    }

    private void celulaTerminoReal_EvaluateBinding(object sender, BindingEventArgs e)
    {
        //celulaTerminoReal.Text = String.Format("{0:dd/MM/yyyy}", e.Value.ToString());
    }
}
