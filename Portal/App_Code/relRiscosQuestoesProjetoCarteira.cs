using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.Hosting;

/// <summary>
/// Summary description for relRiscosQuestoesProjetoCarteira
/// </summary>
public class relRiscosQuestoesProjetoCarteira : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private dsImpressaoPainelProjetos dsImpressaoPainelProjetos1;
    public DevExpress.XtraReports.Parameters.Parameter pRiscoQuestaoChar;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoProjeto;
    public DevExpress.XtraReports.Parameters.Parameter pIndicaRetornaTodosChar;
    public DevExpress.XtraReports.Parameters.Parameter pCodigoCarteira;
    private dados cDados = CdadosUtil.GetCdados(null);
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private XRLabel lblCarteiraDeProjetos;
    private XRLabel xrLabel7;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrLogoUnidade;
    private GroupHeaderBand GroupHeaderProjeto;
    private XRLabel xrLabel3;
    private XRPictureBox imgDesempenhoReceita;
    private XRPictureBox imgDesempenhoDespesa;
    private XRLabel lblDespesaReal;
    private XRLabel lblDespesaPrevistaAteAData;
    private XRLabel lblInicio;
    private XRLabel xrLabel4;
    private XRLabel lblReceitaPrevistaAteAData;
    private XRLabel lblNomeProjeto;
    private XRLabel lblLiderProjeto;
    private XRLabel lblTerminoPrevisto;
    private XRLabel lblReceitaRealizada;
    private GroupHeaderBand GroupHeader4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell celulaProblema;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relRiscosQuestoesProjetoCarteira()
    {
        InitializeComponent();
    }

    private void initData()
    {
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        //xrPictureBox1.Image = logo;

        int codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        comandoSql = string.Format(
        @"BEGIN
	        DECLARE @in_Risco_ou_QuestaoChar AS CHAR(1),  -- ""R"" para riscos  ou  ""Q"" para questões (problemas)
					@in_CodigoCarteiraInt AS INT,      -- Se diferente de null, retorna os Riscos ou Questoes de todos os projetos da carteria escolhida
					@in_CodigoProjetoInt AS INT,  -- Se defirente de null, retorna os Riscos ou Questoes apenas do projeto escolhido
					@in_IndicaRetornaTodosChar AS CHAR(1),  -- Se igual a 'T' (todos) retorna todos os riscos ou questões do projeto/carteira. Senão, retorna apenas os Ativos.
        		    @in_CodigoUsuario AS INT

                SET @in_CodigoCarteiraInt = {1}
                SET @in_Risco_ou_QuestaoChar = 'Q'
		        SET @in_CodigoProjetoInt = {0}
			    SET @in_IndicaRetornaTodosChar = 'T'
                SET @in_CodigoUsuario = {2}     
             SELECT CodigoRiscoQuestao, 
                    CodigoProjeto, 
                    NomeProjeto, 
                    DescricaoRiscoQuestao, 
                    ConsequenciaRiscoQuestao, 
                    TratamentoRiscoQuestao, 
                    DataLimiteResolucao, 
                    StatusRiscoQuestao 
               FROM f_GetRiscosQuestoesProjetoCarteira(@in_Risco_ou_QuestaoChar, 
                                                       @in_CodigoCarteiraInt,
                                                       @in_CodigoProjetoInt, 
                                                       @in_IndicaRetornaTodosChar,
                                                       @in_CodigoUsuario)

          END", pCodigoProjeto.Value, pCodigoCarteira.Value, codigoUsuarioLogado);

        /*
         CREATE FUNCTION [dbo].[f_GetRiscosQuestoesProjetoCarteira]
(@in_Risco_ou_Questao            Char(1),  -- "R" para riscos ;  "Q" para questões (problemas)    ou "A" para ambos
 @in_CodigoCarteira                Int,      -- Se diferente de null, retorna os Riscos ou Questoes de todos os projetos da carteria escolhida
 @in_CodigoProjeto                Int,      -- Se defirente de null, retorna os Riscos ou Questoes apenas do projeto escolhido
 @in_IndicaRetornaTodos            Char(1),  -- Se igual a 'T' (todos) retorna todos os riscos ou questões do projeto/carteira. Senão, retorna apenas os Riscos e/ou Questões Ativos.
 @in_codigoUsuario                Int
    )
         */


        comandoSql = string.Format(
        @"select * from [dbo].[f_GetRiscosQuestoesProjetoCarteira]('A', {0}, {1}, 'T', {2}) order by nomeprojeto asc", pCodigoCarteira.Value, pCodigoProjeto.Value, codigoUsuarioLogado);


        DataSet ds = cDados.getDataSet(comandoSql); ;
        dsImpressaoPainelProjetos1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "RiscosQuestoesProjetoCarteira");


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
        string resourceFileName = "relRiscosQuestoesProjetoCarteira.resx";
        System.Resources.ResourceManager resources = global::Resources.relRiscosQuestoesProjetoCarteira.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dsImpressaoPainelProjetos1 = new dsImpressaoPainelProjetos();
        this.pRiscoQuestaoChar = new DevExpress.XtraReports.Parameters.Parameter();
        this.pCodigoProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.pIndicaRetornaTodosChar = new DevExpress.XtraReports.Parameters.Parameter();
        this.pCodigoCarteira = new DevExpress.XtraReports.Parameters.Parameter();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblCarteiraDeProjetos = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrLogoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.GroupHeaderProjeto = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblReceitaRealizada = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.imgDesempenhoReceita = new DevExpress.XtraReports.UI.XRPictureBox();
        this.imgDesempenhoDespesa = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblDespesaReal = new DevExpress.XtraReports.UI.XRLabel();
        this.lblDespesaPrevistaAteAData = new DevExpress.XtraReports.UI.XRLabel();
        this.lblInicio = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblReceitaPrevistaAteAData = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblLiderProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTerminoPrevisto = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celulaProblema = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsImpressaoPainelProjetos1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.HeightF = 19F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash;
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1103.263F, 19F);
        this.xrTable1.StylePriority.UseBorderDashStyle = false;
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 11.5D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "RiscosQuestoesProjetoCarteira.DescricaoRiscoQuestao")});
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.Weight = 0.44287334164044873D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "RiscosQuestoesProjetoCarteira.ConsequenciaRiscoQuestao")});
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.Weight = 0.244055237659905D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "RiscosQuestoesProjetoCarteira.TratamentoRiscoQuestao")});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Weight = 0.37988370454477527D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "RiscosQuestoesProjetoCarteira.DataLimiteResolucao", "{0:dd/MM/yyyy}")});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.Weight = 0.1639569469241019D;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 25F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 25F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // dsImpressaoPainelProjetos1
        // 
        this.dsImpressaoPainelProjetos1.DataSetName = "dsImpressaoPainelProjetos";
        this.dsImpressaoPainelProjetos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // pRiscoQuestaoChar
        // 
        this.pRiscoQuestaoChar.Description = "Questao problema";
        this.pRiscoQuestaoChar.Name = "pRiscoQuestaoChar";
        this.pRiscoQuestaoChar.ValueInfo = "Q";
        this.pRiscoQuestaoChar.Visible = false;
        // 
        // pCodigoProjeto
        // 
        this.pCodigoProjeto.Description = "Codigo do Projeto";
        this.pCodigoProjeto.Name = "pCodigoProjeto";
        this.pCodigoProjeto.Visible = false;
        // 
        // pIndicaRetornaTodosChar
        // 
        this.pIndicaRetornaTodosChar.Description = "Retorna todos char";
        this.pIndicaRetornaTodosChar.Name = "pIndicaRetornaTodosChar";
        this.pIndicaRetornaTodosChar.Visible = false;
        // 
        // pCodigoCarteira
        // 
        this.pCodigoCarteira.Description = "Codigo da Carteira";
        this.pCodigoCarteira.Name = "pCodigoCarteira";
        this.pCodigoCarteira.Type = typeof(int);
        this.pCodigoCarteira.ValueInfo = "0";
        this.pCodigoCarteira.Visible = false;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2,
            this.xrLabel7,
            this.lblCarteiraDeProjetos});
        this.PageHeader.HeightF = 69.79166F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(1006.334F, 1F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(108.6664F, 63.79165F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(412.5F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(585.8306F, 23F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Relatório de Status";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblCarteiraDeProjetos
        // 
        this.lblCarteiraDeProjetos.Font = new System.Drawing.Font("Verdana", 15F);
        this.lblCarteiraDeProjetos.LocationFloat = new DevExpress.Utils.PointFloat(0F, 38.45832F);
        this.lblCarteiraDeProjetos.Name = "lblCarteiraDeProjetos";
        this.lblCarteiraDeProjetos.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCarteiraDeProjetos.SizeF = new System.Drawing.SizeF(998.3306F, 31.33333F);
        this.lblCarteiraDeProjetos.StylePriority.UseFont = false;
        this.lblCarteiraDeProjetos.StylePriority.UseTextAlignment = false;
        this.lblCarteiraDeProjetos.Text = "Carteira de projetos: Projetos que acesso";
        this.lblCarteiraDeProjetos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLogoUnidade,
            this.xrPageInfo1,
            this.xrPageInfo2});
        this.PageFooter.HeightF = 35.41667F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrLogoUnidade
        // 
        this.xrLogoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(957.2917F, 1.999982F);
        this.xrLogoUnidade.Name = "xrLogoUnidade";
        this.xrLogoUnidade.SizeF = new System.Drawing.SizeF(145.9683F, 33.41669F);
        this.xrLogoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        this.xrLogoUnidade.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLogoUnidade_BeforePrint);
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo1.Format = "Data de Impressão: {0:dd/MM/yyyy}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0.9999911F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(211.4583F, 23F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 9F);
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(212.4583F, 1.999982F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(744.8333F, 23F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // GroupHeaderProjeto
        // 
        this.GroupHeaderProjeto.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblReceitaRealizada,
            this.xrLabel3,
            this.imgDesempenhoReceita,
            this.imgDesempenhoDespesa,
            this.lblDespesaReal,
            this.lblDespesaPrevistaAteAData,
            this.lblInicio,
            this.xrLabel4,
            this.lblReceitaPrevistaAteAData,
            this.lblNomeProjeto,
            this.lblLiderProjeto,
            this.lblTerminoPrevisto});
        this.GroupHeaderProjeto.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoProjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeaderProjeto.KeepTogether = true;
        this.GroupHeaderProjeto.Level = 1;
        this.GroupHeaderProjeto.Name = "GroupHeaderProjeto";
        this.GroupHeaderProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeaderProjeto_BeforePrint);
        // 
        // lblReceitaRealizada
        // 
        this.lblReceitaRealizada.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblReceitaRealizada.LocationFloat = new DevExpress.Utils.PointFloat(319.3076F, 75.1667F);
        this.lblReceitaRealizada.Name = "lblReceitaRealizada";
        this.lblReceitaRealizada.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblReceitaRealizada.SizeF = new System.Drawing.SizeF(350.1351F, 20F);
        this.lblReceitaRealizada.StylePriority.UseFont = false;
        this.lblReceitaRealizada.StylePriority.UseTextAlignment = false;
        this.lblReceitaRealizada.Text = "Real: R$ 1000,00";
        this.lblReceitaRealizada.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(16.8671F, 33.83331F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(57.13504F, 20F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Despesa";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // imgDesempenhoReceita
        // 
        this.imgDesempenhoReceita.LocationFloat = new DevExpress.Utils.PointFloat(371.4426F, 33.83329F);
        this.imgDesempenhoReceita.Name = "imgDesempenhoReceita";
        this.imgDesempenhoReceita.SizeF = new System.Drawing.SizeF(23.95834F, 20F);
        this.imgDesempenhoReceita.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // imgDesempenhoDespesa
        // 
        this.imgDesempenhoDespesa.LocationFloat = new DevExpress.Utils.PointFloat(75.00214F, 33.83331F);
        this.imgDesempenhoDespesa.Name = "imgDesempenhoDespesa";
        this.imgDesempenhoDespesa.SizeF = new System.Drawing.SizeF(20.95834F, 20F);
        this.imgDesempenhoDespesa.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // lblDespesaReal
        // 
        this.lblDespesaReal.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblDespesaReal.LocationFloat = new DevExpress.Utils.PointFloat(16.8671F, 75.1667F);
        this.lblDespesaReal.Name = "lblDespesaReal";
        this.lblDespesaReal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDespesaReal.SizeF = new System.Drawing.SizeF(277.7601F, 20F);
        this.lblDespesaReal.StylePriority.UseFont = false;
        this.lblDespesaReal.StylePriority.UseTextAlignment = false;
        this.lblDespesaReal.Text = "Real: R$ 1000,00";
        this.lblDespesaReal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblDespesaPrevistaAteAData
        // 
        this.lblDespesaPrevistaAteAData.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblDespesaPrevistaAteAData.LocationFloat = new DevExpress.Utils.PointFloat(16.3671F, 55.16666F);
        this.lblDespesaPrevistaAteAData.Name = "lblDespesaPrevistaAteAData";
        this.lblDespesaPrevistaAteAData.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDespesaPrevistaAteAData.SizeF = new System.Drawing.SizeF(278.26F, 20F);
        this.lblDespesaPrevistaAteAData.StylePriority.UseFont = false;
        this.lblDespesaPrevistaAteAData.StylePriority.UseTextAlignment = false;
        this.lblDespesaPrevistaAteAData.Text = "Prev. até a data: R$ 1000,00";
        this.lblDespesaPrevistaAteAData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblInicio
        // 
        this.lblInicio.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblInicio.LocationFloat = new DevExpress.Utils.PointFloat(722.0062F, 33.83331F);
        this.lblInicio.Name = "lblInicio";
        this.lblInicio.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblInicio.SizeF = new System.Drawing.SizeF(165.7599F, 20F);
        this.lblInicio.StylePriority.UseFont = false;
        this.lblInicio.StylePriority.UseTextAlignment = false;
        this.lblInicio.Text = "Inicio: 07/12/2013";
        this.lblInicio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(319.3076F, 33.83331F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(52.13504F, 20F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "Receita";
        // 
        // lblReceitaPrevistaAteAData
        // 
        this.lblReceitaPrevistaAteAData.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblReceitaPrevistaAteAData.LocationFloat = new DevExpress.Utils.PointFloat(319.3076F, 54.83332F);
        this.lblReceitaPrevistaAteAData.Name = "lblReceitaPrevistaAteAData";
        this.lblReceitaPrevistaAteAData.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblReceitaPrevistaAteAData.SizeF = new System.Drawing.SizeF(350.1351F, 20F);
        this.lblReceitaPrevistaAteAData.StylePriority.UseFont = false;
        this.lblReceitaPrevistaAteAData.StylePriority.UseTextAlignment = false;
        this.lblReceitaPrevistaAteAData.Text = "Prev. até a data: R$ 1000,00";
        this.lblReceitaPrevistaAteAData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblNomeProjeto
        // 
        this.lblNomeProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "RiscosQuestoesProjetoCarteira.NomeProjeto")});
        this.lblNomeProjeto.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.lblNomeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(7.869995F, 4.833321F);
        this.lblNomeProjeto.Name = "lblNomeProjeto";
        this.lblNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeProjeto.SizeF = new System.Drawing.SizeF(1103.26F, 20F);
        this.lblNomeProjeto.StylePriority.UseFont = false;
        // 
        // lblLiderProjeto
        // 
        this.lblLiderProjeto.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblLiderProjeto.LocationFloat = new DevExpress.Utils.PointFloat(722.0062F, 75.1667F);
        this.lblLiderProjeto.Name = "lblLiderProjeto";
        this.lblLiderProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblLiderProjeto.SizeF = new System.Drawing.SizeF(381.2567F, 20F);
        this.lblLiderProjeto.StylePriority.UseFont = false;
        this.lblLiderProjeto.StylePriority.UseTextAlignment = false;
        this.lblLiderProjeto.Text = "Líder de Projeto: Alexandre Barros";
        this.lblLiderProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // lblTerminoPrevisto
        // 
        this.lblTerminoPrevisto.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblTerminoPrevisto.LocationFloat = new DevExpress.Utils.PointFloat(914.1587F, 33.83331F);
        this.lblTerminoPrevisto.Name = "lblTerminoPrevisto";
        this.lblTerminoPrevisto.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTerminoPrevisto.SizeF = new System.Drawing.SizeF(196.9713F, 20F);
        this.lblTerminoPrevisto.StylePriority.UseFont = false;
        this.lblTerminoPrevisto.StylePriority.UseTextAlignment = false;
        this.lblTerminoPrevisto.Text = "Termino Previsto: 12/12/2015";
        this.lblTerminoPrevisto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.GroupHeader4.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoProjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("StatusRiscoQuestao", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader4.HeightF = 24.75F;
        this.GroupHeader4.KeepTogether = true;
        this.GroupHeader4.Name = "GroupHeader4";
        this.GroupHeader4.RepeatEveryPage = true;
        // 
        // xrTable2
        // 
        this.xrTable2.BackColor = System.Drawing.Color.Black;
        this.xrTable2.BorderColor = System.Drawing.Color.White;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 1;
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable2.ForeColor = System.Drawing.Color.White;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1103.263F, 24.75F);
        this.xrTable2.StylePriority.UseBackColor = false;
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseForeColor = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celulaProblema,
            this.xrTableCell2,
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 11.5D;
        // 
        // celulaProblema
        // 
        this.celulaProblema.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(142)))), ((int)(((byte)(213)))));
        this.celulaProblema.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaProblema.Name = "celulaProblema";
        this.celulaProblema.StylePriority.UseBackColor = false;
        this.celulaProblema.StylePriority.UseBorders = false;
        this.celulaProblema.Text = "Risco";
        this.celulaProblema.Weight = 0.44287337568500407D;
        this.celulaProblema.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.celulaProblema_BeforePrint);
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(142)))), ((int)(((byte)(213)))));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBackColor = false;
        this.xrTableCell2.Text = "Impactos";
        this.xrTableCell2.Weight = 0.24405520361534969D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(142)))), ((int)(((byte)(213)))));
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBackColor = false;
        this.xrTableCell7.Text = "Tratamento";
        this.xrTableCell7.Weight = 0.37988363645566459D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(142)))), ((int)(((byte)(213)))));
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBackColor = false;
        this.xrTableCell8.Text = "Prazo";
        this.xrTableCell8.Weight = 0.16395701501321253D;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.GroupFooter1.HeightF = 23F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLabel1
        // 
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(75.00214F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 23F);
        // 
        // relRiscosQuestoesProjetoCarteira
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.GroupHeaderProjeto,
            this.GroupHeader4,
            this.GroupFooter1});
        this.DataMember = "RiscosQuestoesProjetoCarteira";
        this.DataSource = this.dsImpressaoPainelProjetos1;
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(25, 25, 25, 25);
        this.PageHeight = 827;
        this.PageWidth = 1169;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pRiscoQuestaoChar,
            this.pCodigoProjeto,
            this.pIndicaRetornaTodosChar,
            this.pCodigoCarteira});
        this.ReportPrintOptions.PrintOnEmptyDataSource = false;
        this.SnapGridSize = 5F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "12.2";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.relRiscosQuestoesProjetoCarteira_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsImpressaoPainelProjetos1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        xrLogoUnidade.Image = Bitmap.FromStream(ms);
    }

    private void relRiscosQuestoesProjetoCarteira_DataSourceDemanded(object sender, EventArgs e)
    {
        initData();
    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet ds = cDados.getParametrosSistema(codigoEntidade, "labelQuestoes");

        //celulaProblema.Text = "Problemas";
        //if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        //{
        //    celulaProblema.Text = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
        //}
        int codigoCarteira = int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString());
        string where = string.Format(" AND cp.CodigoCarteira = {0}", codigoCarteira);
        DataSet dsNomeCarteira = cDados.getCarteirasDeProjetos(where);
        DataSet dsLabelCarteira = cDados.getParametrosSistema(codigoEntidade, "labelCarteiras");
        string nomeEntidade = "";
        DataSet dsNomeEntidade = cDados.getUnidade(string.Format(@" and CodigoUnidadeNegocio = {0}", cDados.getInfoSistema("CodigoEntidade").ToString()));
        if (cDados.DataSetOk(dsLabelCarteira))
        {
            nomeEntidade = dsNomeEntidade.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        string labelCarteira = "";
        if (cDados.DataSetOk(dsLabelCarteira))
        {
            labelCarteira = dsLabelCarteira.Tables[0].Rows[0]["labelCarteiras"].ToString();
        }
        string nomeCarteira = "";
        if (cDados.DataSetOk(dsNomeCarteira))
        {
            nomeCarteira = dsNomeCarteira.Tables[0].Rows[0]["NomeCarteira"].ToString();
        }
        lblCarteiraDeProjetos.Text = nomeEntidade;

    }

    private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet ds = cDados.getParametrosSistema(codigoEntidade, "labelQuestoes");
    }

    private void xrLogoUnidade_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DefineLogoEntidade();
    }

    private void GroupHeaderProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        GroupHeaderBand detail = (GroupHeaderBand)sender;

        DataRowView drv = (DataRowView)detail.Report.GetCurrentRow();
        string corStatusDespesa = drv.Row.Field<string>("CorStatusDespesa");
        string corStatusReceita = drv.Row.Field<string>("CorStatusReceita");

        if (corStatusDespesa.ToLower().Contains("amarelo") == true)
        {
            imgDesempenhoDespesa.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/amarelo.gif";
        }
        if (corStatusDespesa.ToLower().Contains("vermelho") == true)
        {
            imgDesempenhoDespesa.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/vermelho.gif";
        }
        if (corStatusDespesa.ToLower().Contains("branco") == true)
        {
            imgDesempenhoDespesa.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/branco.gif";
        }
        if (corStatusDespesa.ToLower().Contains("verde") == true)
        {
            imgDesempenhoDespesa.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/verde.gif";
        }

        if (corStatusReceita.ToLower().Contains("amarelo") == true)
        {
            imgDesempenhoReceita.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/amarelo.gif";
        }
        if (corStatusReceita.ToLower().Contains("vermelho") == true)
        {
            imgDesempenhoReceita.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/vermelho.gif";
        }
        if (corStatusReceita.ToLower().Contains("branco") == true)
        {
            imgDesempenhoReceita.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/branco.gif";
        }
        if (corStatusReceita.ToLower().Contains("verde") == true)
        {
            imgDesempenhoReceita.ImageUrl = HostingEnvironment.ApplicationPhysicalPath + "/imagens/verde.gif";
        }

        lblDespesaPrevistaAteAData.Text = string.Format(@"Previsto até a Data: {0:c2}", drv.Row.Field<decimal>("DespesaPrevistaAteDate"));
        lblReceitaPrevistaAteAData.Text = string.Format(@"Previsto até a Data: {0:c2}", drv.Row.Field<decimal>("ReceitaPrevistaAteDate"));
        lblInicio.Text = string.Format(@"Início: {0:dd/MM/yyyy}", drv.Row.Field<DateTime?>("DataInicioProjeto"));
        lblTerminoPrevisto.Text = string.Format(@"Término Previsto: {0:dd/MM/yyyy}", drv.Row.Field<DateTime?>("DateTerminoProjeto"));
        lblDespesaReal.Text = string.Format(@"Real: {0:c2}", drv.Row.Field<decimal>("DespesaRealizada"));
        lblReceitaRealizada.Text = string.Format(@"Real: {0:c2}", drv.Row.Field<decimal>("ReceitaRealizada"));
        lblLiderProjeto.Text = string.Format(@"Líder de Projeto: {0}", drv.Row.Field<string>("NomeGerenteProjeto"));
    }

    private void celulaProblema_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet ds = cDados.getParametrosSistema(codigoEntidade, "labelQuestoes");

        DataRowView drv = (DataRowView)celulaProblema.Report.GetCurrentRow();
        string riscoQuestao = drv.Row.Field<string>("StatusRiscoQuestao");
        if (riscoQuestao == "QA")
        {
            celulaProblema.Text = "Problemas";
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                celulaProblema.Text = ds.Tables[0].Rows[0]["labelQuestoes"].ToString();
            }
        }
        else
        {
            celulaProblema.Text = "Risco";
        }

    }
}
