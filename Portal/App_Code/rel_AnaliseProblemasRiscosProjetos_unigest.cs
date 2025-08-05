using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for rel_AnaliseProblemasRiscosProjetos_unigest
/// </summary>
public class rel_AnaliseProblemasRiscosProjetos_unigest : DevExpress.XtraReports.UI.XtraReport
{
    private dados cDados;
    public int CodigoFoco;
    public int CodigoDirecionador;
    public int CodigoGrandeDesafio;
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DS_AnaliseProblemasRiscosProjetos dS_AnaliseProblemasRiscosProjetos1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRLabel xrLabel2;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRLabel xrLabel3;
    private DetailReportBand DetailReport_riscoquestao;
    private DetailBand Detail4;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private DetailReportBand DetailReport4;
    private DetailBand Detail5;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private DetailReportBand DetailReport5;
    private DetailBand Detail6;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private PageHeaderBand PageHeader;
    private XRLabel xrLabel8;
    private XRPictureBox xrLogoEntidade;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private PageFooterBand PageFooter;
    private XRLabel lblNomeUnidadeFooter;
    private XRLabel lblNomeEntidadeFooter;
    private DevExpress.XtraReports.Parameters.Parameter pSiglaUnidade;
    private DevExpress.XtraReports.Parameters.Parameter pNomeUnidade;
    private DevExpress.XtraReports.Parameters.Parameter pSiglaEntidade;
    private DevExpress.XtraReports.Parameters.Parameter pNomeEntidade;
    private CalculatedField cc_NomeUnidadeRelatorio;
    private CalculatedField cc_NomeEntidadeRelatorio;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_rblTipoRelatorio;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_ddlTipo;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_ddlUnidade;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_ddlProjetoPrograma;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_ddStatusPlanoAcao;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_ckbAtivo;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_txtFoco;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_txtDirecionador;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_txtGrandeDesafio;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_ckbPlanosAcao;
    private XRPictureBox xrPictureBox1;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_entidade;
    private CalculatedField cc_RiscoQuestao;
    private ReportHeaderBand ReportHeader2;
    private XRLabel xrLabel16;
    private XRLine xrLine3;
    private XRLine xrLine1;
    private XRLine xrLine5;
    private XRLine xrLine6;
    private XRLine xrLine2;
    private XRLine xrLine4;
    private XRLine xrLine9;
    private XRLine xrLine10;
    private XRLine xrLine7;
    private XRLine xrLine8;
    private XRControlStyle xrControlStyle1;
    private XRLabel xrLabel13;
    private GroupFooterBand GroupFooter1;
    private XRLine xrLine11;
    private XRLine xrLine12;
    private XRLabel xrLabel17;
    private XRLabel xrLabel14;
    private CalculatedField cc_LabelTipoQuestao;
    private CalculatedField calculatedField1;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private DS_AnaliseProblemasRiscosProjetos_unigest dS_AnaliseProblemasRiscosProjetos_unigest1;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_TipoProjeto;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_AfetaOrcamento;
    private DevExpress.XtraReports.Parameters.Parameter pfiltro_AfetaMeta;
    private CalculatedField cfTituloProjeto;
    private CalculatedField cc_AfetaMeta;
    private CalculatedField cc_AfetaOrcamento;
    private CalculatedField cc_TipoVariacaoCusto;
    private XRLabel xrLabel19;
    private XRLabel xrLabel18;
    private XRLabel xrLabel15;
    private XRLabel xrLabel1;
    private GroupHeaderBand GroupHeader4;
    private XRLabel xrLabel21;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rel_AnaliseProblemasRiscosProjetos_unigest(int CodigoFoco, int CodigoDirecionador, int CodigoGrandeDesafio, int codUnidade, string pSiglaEntidade, string pNomeEntidade)
    {
        this.CodigoFoco = CodigoFoco;
        this.CodigoDirecionador = CodigoDirecionador;
        this.CodigoGrandeDesafio = CodigoGrandeDesafio;
        cDados = CdadosUtil.GetCdados(null);
        InitializeComponent();
        string exibeTracoUnidade = "";
        string parametro_SIGLA_UNIDADE = "";
        string parametro_NOME_UNIDADE = "";
        if (codUnidade != -1)
        {

            DataSet dsSiglaNomeEntidade = cDados.getUnidadeNegocio(string.Format(@" and un.CodigoUnidadeNegocio = {0} ", codUnidade));
            if (cDados.DataSetOk(dsSiglaNomeEntidade))
            {
                parametro_SIGLA_UNIDADE = dsSiglaNomeEntidade.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
                parametro_NOME_UNIDADE = dsSiglaNomeEntidade.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
                exibeTracoUnidade = " - ";
            }
        }
        lblNomeEntidadeFooter.Text = pNomeEntidade;
        lblNomeUnidadeFooter.Text = parametro_SIGLA_UNIDADE + exibeTracoUnidade + parametro_NOME_UNIDADE;

        cDados = CdadosUtil.GetCdados(null);

    }

    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        xrPictureBox1.Image = Bitmap.FromStream(ms);
        //essa imagem passa a ser fixa
    }

    private void DefineLogoUnidade(int codigoUnidadeSelecionada)
    {
        dados cDados = CdadosUtil.GetCdados(null);
        int codigoEntidade = int.Parse((codigoUnidadeSelecionada == -1) ? cDados.getInfoSistema("CodigoEntidade").ToString() : codigoUnidadeSelecionada.ToString());
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        if (cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0]))
        {
            try
            {
                Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
                MemoryStream ms = new MemoryStream(binaryImage);
                xrLogoEntidade.Image = Bitmap.FromStream(ms);
            }
            catch
            {
                xrLogoEntidade.Image = null;
                xrLogoEntidade.ImageUrl = "../imagens/sin_logo.gif";
            }
        }
        else
        {

        }
        //essa imagem passa a ser fixa
    }

    public void InitData()
    {

        /*
            - Disponibilizar uma nova opção que será apresentada no mesmo menu de relatórios dinâmicos com a denominação "Riscos e Problemas", devendo ser gerado um arquivo PDF. 
         * Este relatório deve ser exclusivamente disponibilizado para as entidades 1,82,83,84 do Sistema Indústria.
            - Disponibilizar uma tela onde o usuário poderá escolher os filtros a serem aplicados na geração do PDF:
            . Check Box para indicar se traz riscos, problemas ou ambos
            . Tipo do Risco/problema com opção para todos
            . Foco
            . Direcionador
            . Grande Desafio
            . Unidade de Negócio
            . Projeto/Programa- se selecionar Programa sai todos os projetos do programa
            . Permitir selecionar se somente problemas/riscos ativos, ou se todos os problemas/riscos
            . Apresentar planos de ação (sim/não)
            . Status dos planos de ação (em execução, concluído, cancelado)
            - A implementação deve considerar o template em anexo.


        dia 22/03/2018 quinta feira, aalterações no relatorio utilizado na unigeste
                
        PROCEDURE [dbo].[p_getRelAnaliseProblemasRiscos](
         @pfiltro_rblTipoRelatorio			as char(1),
         @pfiltro_ddlTipo								as smallint,
         @pfiltro_ddlUnidade						as int,
         @pfiltro_ddlProjetoPrograma		as int,
         @pfiltro_ddStatusPlanoAcao			as int,
         @pfiltro_ckbAtivo							as char(1),
         @pfiltro_txtFoco								as int,
         @pfiltro_txtDirecionador				as int,
         @pfiltro_txtGrandeDesafio			as int,
         @pfiltro_ckbPlanosAcao					as char(1),
         @pfiltro_entidade							as int,
		 @pfiltro_TipoProjeto						as int,
		 @pfiltro_AfetaOrcamento				as	char(1),
		 @pfiltro_AfetaMeta							as	char(1)


         */
        string comandosql = string.Format(@"
        DECLARE @RC int
        DECLARE @pfiltro_rblTipoRelatorio varchar(1)
        DECLARE @pfiltro_ddlTipo varchar(100)
        DECLARE @pfiltro_ddlUnidadeas int
        DECLARE @pfiltro_ddlProjetoPrograma int
        DECLARE @pfiltro_ddStatusPlanoAcao int
        DECLARE @pfiltro_ckbAtivo char(1)
        DECLARE @pfiltro_txtFoco int
        DECLARE @pfiltro_txtDirecionador int
        DECLARE @pfiltro_txtGrandeDesafio int
        DECLARE @pfiltro_ckbPlanosAcao char(1)
		DECLARE @pfiltro_entidade int

        DECLARE @pfiltro_TipoProjeto int
		DECLARE @pfiltro_AfetaOrcamento char(1)
		DECLARE @pfiltro_AfetaMeta as char(1)

        SET @pfiltro_rblTipoRelatorio  = '{2}'
        SET @pfiltro_ddlTipo = {3}
        SET @pfiltro_ddlUnidadeas = {4}
        SET @pfiltro_ddlProjetoPrograma = {5}
        SET @pfiltro_ddStatusPlanoAcao = {6}
        SET @pfiltro_ckbAtivo = '{7}'
        SET @pfiltro_txtFoco = {8}
        SET @pfiltro_txtDirecionador = {9}
        SET @pfiltro_txtGrandeDesafio = {10}
        SET @pfiltro_ckbPlanosAcao = '{11}'
		SET @pfiltro_entidade = {12}
        SET @pfiltro_TipoProjeto = {13}
		SET @pfiltro_AfetaOrcamento = '{14}'
		SET @pfiltro_AfetaMeta = '{15}'

        EXECUTE @RC = {0}.{1}.[p_getRelAnaliseProblemasRiscos] 
                @pfiltro_rblTipoRelatorio
                ,@pfiltro_ddlTipo
                ,@pfiltro_ddlUnidadeas
                ,@pfiltro_ddlProjetoPrograma
                ,@pfiltro_ddStatusPlanoAcao
                ,@pfiltro_ckbAtivo
                ,@pfiltro_txtFoco
                ,@pfiltro_txtDirecionador
                ,@pfiltro_txtGrandeDesafio
                ,@pfiltro_ckbPlanosAcao
				,@pfiltro_entidade		 
                ,@pfiltro_TipoProjeto
		        ,@pfiltro_AfetaOrcamento
		        ,@pfiltro_AfetaMeta", cDados.getDbName(), cDados.getDbOwner(), /*{2}*/pfiltro_rblTipoRelatorio.Value,
                                                               /*{3}*/pfiltro_ddlTipo.Value,
                                                               /*{4}*/pfiltro_ddlUnidade.Value,
                                                               /*{5}*/pfiltro_ddlProjetoPrograma.Value,
                                                               /*{6}*/pfiltro_ddStatusPlanoAcao.Value,
                                                               /*{7}*/pfiltro_ckbAtivo.Value,
                                                               /*{8}*/CodigoFoco,
                                                               /*{9}*/CodigoDirecionador,
                                                               /*{10}*/CodigoGrandeDesafio,
                                                               /*{11}*/pfiltro_ckbPlanosAcao.Value,
                                                               /*{12}*/int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()),
                                                               /*{13}*/pfiltro_TipoProjeto.Value,
                                                               /*{14}*/pfiltro_AfetaOrcamento.Value,
                                                               /*{15}*/pfiltro_AfetaMeta.Value);
        string connectionString = cDados.classeDados.getStringConexao();
        //DataSet ds = cDados.getDataSet(comandosql);
        SqlDataAdapter adapter = new SqlDataAdapter(comandosql, connectionString);



        adapter.TableMappings.Add("Table", "dtTemaObjetivo");
        adapter.TableMappings.Add("Table1", "dtMeta");
        adapter.TableMappings.Add("Table2", "dtUnidade");
        adapter.TableMappings.Add("Table3", "dtProjeto");
        adapter.TableMappings.Add("Table4", "dtRiscoQuestao");
        adapter.TableMappings.Add("Table5", "dtPlanoAcao");
        adapter.TableMappings.Add("Table6", "dtComentario");

        adapter.Fill(dS_AnaliseProblemasRiscosProjetos_unigest1);


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
        string resourceFileName = "rel_AnaliseProblemasRiscosProjetos_unigest.resx";
        System.Resources.ResourceManager resources = global::Resources.rel_AnaliseProblemasRiscosProjetos_unigest.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dS_AnaliseProblemasRiscosProjetos1 = new DS_AnaliseProblemasRiscosProjetos();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport_riscoquestao = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine12 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.dS_AnaliseProblemasRiscosProjetos_unigest1 = new DS_AnaliseProblemasRiscosProjetos_unigest();
        this.DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLine9 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine10 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLine7 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLine8 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLine11 = new DevExpress.XtraReports.UI.XRLine();
        this.ReportHeader2 = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.lblNomeUnidadeFooter = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeEntidadeFooter = new DevExpress.XtraReports.UI.XRLabel();
        this.pSiglaUnidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.pNomeUnidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.pSiglaEntidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.pNomeEntidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.cc_NomeUnidadeRelatorio = new DevExpress.XtraReports.UI.CalculatedField();
        this.cc_NomeEntidadeRelatorio = new DevExpress.XtraReports.UI.CalculatedField();
        this.pfiltro_rblTipoRelatorio = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_ddlTipo = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_ddlUnidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_ddlProjetoPrograma = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_ddStatusPlanoAcao = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_ckbAtivo = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_txtFoco = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_txtDirecionador = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_txtGrandeDesafio = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_ckbPlanosAcao = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_entidade = new DevExpress.XtraReports.Parameters.Parameter();
        this.cc_RiscoQuestao = new DevExpress.XtraReports.UI.CalculatedField();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.cc_LabelTipoQuestao = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.pfiltro_TipoProjeto = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_AfetaOrcamento = new DevExpress.XtraReports.Parameters.Parameter();
        this.pfiltro_AfetaMeta = new DevExpress.XtraReports.Parameters.Parameter();
        this.cfTituloProjeto = new DevExpress.XtraReports.UI.CalculatedField();
        this.cc_AfetaMeta = new DevExpress.XtraReports.UI.CalculatedField();
        this.cc_AfetaOrcamento = new DevExpress.XtraReports.UI.CalculatedField();
        this.cc_TipoVariacaoCusto = new DevExpress.XtraReports.UI.CalculatedField();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_AnaliseProblemasRiscosProjetos1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_AnaliseProblemasRiscosProjetos_unigest1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.Dpi = 100F;
        this.Detail.Expanded = false;
        this.Detail.HeightF = 46.00258F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 100F;
        this.xrTable1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1120F, 37.83333F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 100F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 11.5D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrTableCell2.BorderColor = System.Drawing.Color.LightGray;
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.BorderWidth = 1F;
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.DescricaoTema")});
        this.xrTableCell2.Dpi = 100F;
        this.xrTableCell2.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold);
        this.xrTableCell2.ForeColor = System.Drawing.Color.White;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell2.StylePriority.UseBackColor = false;
        this.xrTableCell2.StylePriority.UseBorderColor = false;
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseBorderWidth = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UseForeColor = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell2.Weight = 0.18911103828431994D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrTableCell3.BorderColor = System.Drawing.Color.LightGray;
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.DescricaoObjetivo")});
        this.xrTableCell3.Dpi = 100F;
        this.xrTableCell3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.ForeColor = System.Drawing.Color.White;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell3.StylePriority.UseBackColor = false;
        this.xrTableCell3.StylePriority.UseBorderColor = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UseForeColor = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell3.Weight = 0.72199013635494358D;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 100F;
        this.TopMargin.HeightF = 18F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 100F;
        this.BottomMargin.HeightF = 6.171846F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // dS_AnaliseProblemasRiscosProjetos1
        // 
        this.dS_AnaliseProblemasRiscosProjetos1.DataSetName = "DS_AnaliseProblemasRiscosProjetos";
        this.dS_AnaliseProblemasRiscosProjetos1.EnforceConstraints = false;
        this.dS_AnaliseProblemasRiscosProjetos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.DetailReport1});
        this.DetailReport.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta";
        this.DetailReport.DataSource = this.dS_AnaliseProblemasRiscosProjetos_unigest1;
        this.DetailReport.Dpi = 100F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.BorderWidth = 0.1F;
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.Detail1.Dpi = 100F;
        this.Detail1.Expanded = false;
        this.Detail1.HeightF = 24.14974F;
        this.Detail1.Name = "Detail1";
        this.Detail1.StylePriority.UseBorderWidth = false;
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.Dpi = 100F;
        this.xrTable6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.31641F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable6.SizeF = new System.Drawing.SizeF(1120F, 17.01F);
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseFont = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell16});
        this.xrTableRow6.Dpi = 100F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 11.5D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrTableCell15.BorderColor = System.Drawing.Color.LightGray;
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.BorderWidth = 1F;
        this.xrTableCell15.Dpi = 100F;
        this.xrTableCell15.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold);
        this.xrTableCell15.ForeColor = System.Drawing.Color.White;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell15.StylePriority.UseBackColor = false;
        this.xrTableCell15.StylePriority.UseBorderColor = false;
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseBorderWidth = false;
        this.xrTableCell15.StylePriority.UseFont = false;
        this.xrTableCell15.StylePriority.UseForeColor = false;
        this.xrTableCell15.StylePriority.UsePadding = false;
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell15.Weight = 0.14964862556883751D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrTableCell16.BorderColor = System.Drawing.Color.LightGray;
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.BorderWidth = 1F;
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.dS_AnaliseProblemasRiscosProjetos1, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.DescricaoMeta")});
        this.xrTableCell16.Dpi = 100F;
        this.xrTableCell16.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrTableCell16.ForeColor = System.Drawing.Color.White;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell16.StylePriority.UseBackColor = false;
        this.xrTableCell16.StylePriority.UseBorderColor = false;
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseBorderWidth = false;
        this.xrTableCell16.StylePriority.UseFont = false;
        this.xrTableCell16.StylePriority.UseForeColor = false;
        this.xrTableCell16.StylePriority.UsePadding = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell16.Weight = 0.761452549070426D;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.DetailReport2});
        this.DetailReport1.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade";
        this.DetailReport1.DataSource = this.dS_AnaliseProblemasRiscosProjetos_unigest1;
        this.DetailReport1.Dpi = 100F;
        this.DetailReport1.Level = 0;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel2});
        this.Detail2.Dpi = 100F;
        this.Detail2.Expanded = false;
        this.Detail2.HeightF = 24.16929F;
        this.Detail2.Name = "Detail2";
        // 
        // xrLabel13
        // 
        this.xrLabel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLabel13.BorderColor = System.Drawing.Color.Gray;
        this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel13.BorderWidth = 0.2F;
        this.xrLabel13.Dpi = 100F;
        this.xrLabel13.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.ForeColor = System.Drawing.Color.White;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4.000001F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(232.4707F, 16F);
        this.xrLabel13.StylePriority.UseBackColor = false;
        this.xrLabel13.StylePriority.UseBorderColor = false;
        this.xrLabel13.StylePriority.UseBorders = false;
        this.xrLabel13.StylePriority.UseBorderWidth = false;
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseForeColor = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "Unidade:";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel2
        // 
        this.xrLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLabel2.BorderColor = System.Drawing.Color.Gray;
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.BorderWidth = 0.2F;
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.NomeUnidadeNegocio")});
        this.xrLabel2.Dpi = 100F;
        this.xrLabel2.Font = new System.Drawing.Font("Arial Narrow", 11F);
        this.xrLabel2.ForeColor = System.Drawing.Color.White;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(232.4707F, 4.000001F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(887.5293F, 16F);
        this.xrLabel2.StylePriority.UseBackColor = false;
        this.xrLabel2.StylePriority.UseBorderColor = false;
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseBorderWidth = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.DetailReport_riscoquestao,
            this.ReportHeader2,
            this.GroupHeader4});
        this.DetailReport2.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto";
        this.DetailReport2.DataSource = this.dS_AnaliseProblemasRiscosProjetos_unigest1;
        this.DetailReport2.Dpi = 100F;
        this.DetailReport2.Level = 0;
        this.DetailReport2.Name = "DetailReport2";
        this.DetailReport2.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.Detail3.Dpi = 100F;
        this.Detail3.HeightF = 14F;
        this.Detail3.Name = "Detail3";
        // 
        // xrLabel3
        // 
        this.xrLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLabel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrLabel3.BorderWidth = 1F;
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.cfTitul" +
                    "oProjeto")});
        this.xrLabel3.Dpi = 100F;
        this.xrLabel3.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrLabel3.ForeColor = System.Drawing.Color.White;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1120F, 14F);
        this.xrLabel3.StylePriority.UseBackColor = false;
        this.xrLabel3.StylePriority.UseBorderColor = false;
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseBorderWidth = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseForeColor = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReport_riscoquestao
        // 
        this.DetailReport_riscoquestao.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.DetailReport4,
            this.DetailReport5,
            this.GroupFooter1});
        this.DetailReport_riscoquestao.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao";
        this.DetailReport_riscoquestao.DataSource = this.dS_AnaliseProblemasRiscosProjetos_unigest1;
        this.DetailReport_riscoquestao.Dpi = 100F;
        this.DetailReport_riscoquestao.Level = 0;
        this.DetailReport_riscoquestao.Name = "DetailReport_riscoquestao";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel15,
            this.xrLabel1,
            this.xrLabel17,
            this.xrLabel14,
            this.xrLine12,
            this.xrLine3,
            this.xrLine1,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4});
        this.Detail4.Dpi = 100F;
        this.Detail4.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.Detail4.HeightF = 100.6041F;
        this.Detail4.Name = "Detail4";
        this.Detail4.StylePriority.UseFont = false;
        // 
        // xrLabel19
        // 
        this.xrLabel19.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel19.BorderColor = System.Drawing.Color.LightGreen;
        this.xrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel19.BorderWidth = 3F;
        this.xrLabel19.Dpi = 100F;
        this.xrLabel19.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(4.459699F, 64.22911F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(195.5436F, 23.00003F);
        this.xrLabel19.StylePriority.UseBackColor = false;
        this.xrLabel19.StylePriority.UseBorderColor = false;
        this.xrLabel19.StylePriority.UseBorders = false;
        this.xrLabel19.StylePriority.UseBorderWidth = false;
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "Afeta a meta?:";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel18
        // 
        this.xrLabel18.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel18.BorderColor = System.Drawing.Color.LightGreen;
        this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel18.BorderWidth = 3F;
        this.xrLabel18.Dpi = 100F;
        this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(4.459699F, 41.22912F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(195.5436F, 23F);
        this.xrLabel18.StylePriority.UseBackColor = false;
        this.xrLabel18.StylePriority.UseBorderColor = false;
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseBorderWidth = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.StylePriority.UseTextAlignment = false;
        this.xrLabel18.Text = "Afeta o orçamento?:";
        this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel15
        // 
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.cc_AfetaOrcamento")});
        this.xrLabel15.Dpi = 100F;
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(200.0033F, 41.22912F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(783.8796F, 23F);
        this.xrLabel15.StylePriority.UseTextAlignment = false;
        this.xrLabel15.Text = "xrLabel15";
        this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.cc_AfetaMeta")});
        this.xrLabel1.Dpi = 100F;
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(200.0033F, 64.22914F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(783.8796F, 23F);
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "xrLabel1";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel17
        // 
        this.xrLabel17.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.cc_LabelTipoQuestao")});
        this.xrLabel17.Dpi = 100F;
        this.xrLabel17.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(769F, 2.999993F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(100F, 13F);
        this.xrLabel17.StylePriority.UseBackColor = false;
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "Tipo:";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.calculatedField1")});
        this.xrLabel14.Dpi = 100F;
        this.xrLabel14.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(869F, 2.99999F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(248.8533F, 13F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "xrLabel14";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLine12
        // 
        this.xrLine12.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine12.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLine12.BorderWidth = 1F;
        this.xrLine12.Dpi = 100F;
        this.xrLine12.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine12.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine12.Name = "xrLine12";
        this.xrLine12.SizeF = new System.Drawing.SizeF(1119F, 2F);
        this.xrLine12.StylePriority.UseBorderColor = false;
        this.xrLine12.StylePriority.UseBorders = false;
        this.xrLine12.StylePriority.UseBorderWidth = false;
        this.xrLine12.StylePriority.UseForeColor = false;
        // 
        // xrLine3
        // 
        this.xrLine3.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine3.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine3.BorderWidth = 1F;
        this.xrLine3.Dpi = 100F;
        this.xrLine3.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine3.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(1117.959F, 1.999998F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(2F, 98.60412F);
        this.xrLine3.StylePriority.UseBorderColor = false;
        this.xrLine3.StylePriority.UseBorders = false;
        this.xrLine3.StylePriority.UseBorderWidth = false;
        this.xrLine3.StylePriority.UseForeColor = false;
        // 
        // xrLine1
        // 
        this.xrLine1.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine1.BorderWidth = 1F;
        this.xrLine1.Dpi = 100F;
        this.xrLine1.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.00001F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(2F, 98.60409F);
        this.xrLine1.StylePriority.UseBorderColor = false;
        this.xrLine1.StylePriority.UseBorders = false;
        this.xrLine1.StylePriority.UseBorderWidth = false;
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // xrLabel12
        // 
        this.xrLabel12.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel12.BorderColor = System.Drawing.Color.LightGreen;
        this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel12.BorderWidth = 3F;
        this.xrLabel12.Dpi = 100F;
        this.xrLabel12.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(3.000019F, 29.22912F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(195.5436F, 12F);
        this.xrLabel12.StylePriority.UseBackColor = false;
        this.xrLabel12.StylePriority.UseBorderColor = false;
        this.xrLabel12.StylePriority.UseBorders = false;
        this.xrLabel12.StylePriority.UseBorderWidth = false;
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Estratégias para Tratamento:";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel11
        // 
        this.xrLabel11.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel11.Dpi = 100F;
        this.xrLabel11.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(769F, 16.00001F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(100F, 12F);
        this.xrLabel11.StylePriority.UseBackColor = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.Text = "Responsável:";
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel10
        // 
        this.xrLabel10.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel10.BorderColor = System.Drawing.Color.LightGreen;
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.BorderWidth = 3F;
        this.xrLabel10.Dpi = 100F;
        this.xrLabel10.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(3.143291F, 17.00002F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(196.3273F, 12F);
        this.xrLabel10.StylePriority.UseBackColor = false;
        this.xrLabel10.StylePriority.UseBorderColor = false;
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseBorderWidth = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "Consequências:";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel9
        // 
        this.xrLabel9.BackColor = System.Drawing.Color.Transparent;
        this.xrLabel9.BorderColor = System.Drawing.Color.LightGreen;
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.BorderWidth = 3F;
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.cc_RiscoQuestao")});
        this.xrLabel9.Dpi = 100F;
        this.xrLabel9.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(3.143291F, 3.000002F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(196.86F, 14F);
        this.xrLabel9.StylePriority.UseBackColor = false;
        this.xrLabel9.StylePriority.UseBorderColor = false;
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseBorderWidth = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Problema:";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel7
        // 
        this.xrLabel7.BorderColor = System.Drawing.Color.LightGreen;
        this.xrLabel7.BorderWidth = 3F;
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.Responsavel")});
        this.xrLabel7.Dpi = 100F;
        this.xrLabel7.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(869F, 16.00002F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(248.8566F, 12F);
        this.xrLabel7.StylePriority.UseBorderColor = false;
        this.xrLabel7.StylePriority.UseBorderWidth = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "xrLabel7";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.Tratamento")});
        this.xrLabel6.Dpi = 100F;
        this.xrLabel6.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(200.0033F, 29.00002F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(549F, 12F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.Consequencia")});
        this.xrLabel5.Dpi = 100F;
        this.xrLabel5.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(200.0033F, 17.00002F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(549F, 12F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.StylePriority.UseTextAlignment = false;
        this.xrLabel5.Text = "xrLabel5";
        this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.BorderColor = System.Drawing.Color.LightGreen;
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.BorderWidth = 3F;
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.Problema")});
        this.xrLabel4.Dpi = 100F;
        this.xrLabel4.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(200.0033F, 3.000002F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(549F, 14F);
        this.xrLabel4.StylePriority.UseBorderColor = false;
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseBorderWidth = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "xrLabel4";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReport4
        // 
        this.DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.GroupHeader1});
        this.DetailReport4.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao.dtRiscoQuestao_dtComentario";
        this.DetailReport4.DataSource = this.dS_AnaliseProblemasRiscosProjetos_unigest1;
        this.DetailReport4.Dpi = 100F;
        this.DetailReport4.Level = 0;
        this.DetailReport4.Name = "DetailReport4";
        this.DetailReport4.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine5,
            this.xrTable2,
            this.xrLine6});
        this.Detail5.Dpi = 100F;
        this.Detail5.HeightF = 13F;
        this.Detail5.Name = "Detail5";
        // 
        // xrLine5
        // 
        this.xrLine5.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine5.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine5.BorderWidth = 1F;
        this.xrLine5.Dpi = 100F;
        this.xrLine5.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine5.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine5.Name = "xrLine5";
        this.xrLine5.SizeF = new System.Drawing.SizeF(2.000019F, 13F);
        this.xrLine5.StylePriority.UseBorderColor = false;
        this.xrLine5.StylePriority.UseBorders = false;
        this.xrLine5.StylePriority.UseBorderWidth = false;
        this.xrLine5.StylePriority.UseForeColor = false;
        // 
        // xrTable2
        // 
        this.xrTable2.BorderColor = System.Drawing.Color.Gray;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.BorderWidth = 0.2F;
        this.xrTable2.Dpi = 100F;
        this.xrTable2.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(2.143271F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1115.769F, 13F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseBorderWidth = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Dpi = 100F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 11.5D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.BorderWidth = 0.2F;
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.dtRiscoQuestao_dtComentario.Descricao")});
        this.xrTableCell4.Dpi = 100F;
        this.xrTableCell4.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseBorderWidth = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.90993855304191029D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.BorderWidth = 0.2F;
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.dtRiscoQuestao_dtComentario.Responsavel")});
        this.xrTableCell5.Dpi = 100F;
        this.xrTableCell5.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseBorderWidth = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell5.Weight = 0.49775375465039751D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.BorderWidth = 0.2F;
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.dtRiscoQuestao_dtComentario.Data", "{0:dd/MM/yyyy}")});
        this.xrTableCell6.Dpi = 100F;
        this.xrTableCell6.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseBorderWidth = false;
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.19230769230769232D;
        // 
        // xrLine6
        // 
        this.xrLine6.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine6.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine6.BorderWidth = 1F;
        this.xrLine6.Dpi = 100F;
        this.xrLine6.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine6.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(1117.912F, 0F);
        this.xrLine6.Name = "xrLine6";
        this.xrLine6.SizeF = new System.Drawing.SizeF(2F, 13F);
        this.xrLine6.StylePriority.UseBorderColor = false;
        this.xrLine6.StylePriority.UseBorders = false;
        this.xrLine6.StylePriority.UseBorderWidth = false;
        this.xrLine6.StylePriority.UseForeColor = false;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrTable4,
            this.xrLine4});
        this.GroupHeader1.Dpi = 100F;
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 21.16667F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.RepeatEveryPage = true;
        // 
        // xrLine2
        // 
        this.xrLine2.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine2.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine2.BorderWidth = 1F;
        this.xrLine2.Dpi = 100F;
        this.xrLine2.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine2.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(1.865884E-05F, 0F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(2F, 21.16667F);
        this.xrLine2.StylePriority.UseBorderColor = false;
        this.xrLine2.StylePriority.UseBorders = false;
        this.xrLine2.StylePriority.UseBorderWidth = false;
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // xrTable4
        // 
        this.xrTable4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.BorderWidth = 0.5F;
        this.xrTable4.Dpi = 100F;
        this.xrTable4.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(2.110107F, 7.166666F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(1115.857F, 14F);
        this.xrTable4.StylePriority.UseBackColor = false;
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseBorderWidth = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.BorderWidth = 0.5F;
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11});
        this.xrTableRow4.Dpi = 100F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.StylePriority.UseBorderWidth = false;
        this.xrTableRow4.Weight = 11.5D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrTableCell9.BorderColor = System.Drawing.Color.Gray;
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.BorderWidth = 0.2F;
        this.xrTableCell9.Dpi = 100F;
        this.xrTableCell9.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBackColor = false;
        this.xrTableCell9.StylePriority.UseBorderColor = false;
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseBorderWidth = false;
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "Comentário";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell9.Weight = 0.90993846535637868D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrTableCell10.BorderColor = System.Drawing.Color.Gray;
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.BorderWidth = 0.2F;
        this.xrTableCell10.Dpi = 100F;
        this.xrTableCell10.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBackColor = false;
        this.xrTableCell10.StylePriority.UseBorderColor = false;
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseBorderWidth = false;
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Responsável";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell10.Weight = 0.49775384233592912D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrTableCell11.BorderColor = System.Drawing.Color.Gray;
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell11.BorderWidth = 0.2F;
        this.xrTableCell11.Dpi = 100F;
        this.xrTableCell11.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBackColor = false;
        this.xrTableCell11.StylePriority.UseBorderColor = false;
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseBorderWidth = false;
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "Data";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell11.Weight = 0.19230769230769232D;
        // 
        // xrLine4
        // 
        this.xrLine4.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine4.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine4.BorderWidth = 1F;
        this.xrLine4.Dpi = 100F;
        this.xrLine4.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine4.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(1117.967F, 0F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(2.033447F, 21.16667F);
        this.xrLine4.StylePriority.UseBorderColor = false;
        this.xrLine4.StylePriority.UseBorders = false;
        this.xrLine4.StylePriority.UseBorderWidth = false;
        this.xrLine4.StylePriority.UseForeColor = false;
        // 
        // dS_AnaliseProblemasRiscosProjetos_unigest1
        // 
        this.dS_AnaliseProblemasRiscosProjetos_unigest1.DataSetName = "DS_AnaliseProblemasRiscosProjetos_unigest";
        this.dS_AnaliseProblemasRiscosProjetos_unigest1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // DetailReport5
        // 
        this.DetailReport5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.GroupHeader2});
        this.DetailReport5.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao.dtRiscoQuestao_dtPlanoAcao";
        this.DetailReport5.DataSource = this.dS_AnaliseProblemasRiscosProjetos_unigest1;
        this.DetailReport5.Dpi = 100F;
        this.DetailReport5.Level = 1;
        this.DetailReport5.Name = "DetailReport5";
        this.DetailReport5.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrLine9,
            this.xrLine10});
        this.Detail6.Dpi = 100F;
        this.Detail6.HeightF = 13F;
        this.Detail6.Name = "Detail6";
        // 
        // xrTable3
        // 
        this.xrTable3.BorderColor = System.Drawing.Color.Gray;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.BorderWidth = 0.2F;
        this.xrTable3.Dpi = 100F;
        this.xrTable3.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(2.143271F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1115.857F, 13F);
        this.xrTable3.StylePriority.UseBorderColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseBorderWidth = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.BorderWidth = 0.2F;
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow3.Dpi = 100F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.StylePriority.UseBorderWidth = false;
        this.xrTableRow3.Weight = 11.5D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.BorderWidth = 0.2F;
        this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.dtRiscoQuestao_dtPlanoAcao.Descricao")});
        this.xrTableCell1.Dpi = 100F;
        this.xrTableCell1.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseBorderWidth = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Text = "xrTableCell1";
        this.xrTableCell1.Weight = 0.9099359875072548D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.BorderWidth = 0.2F;
        this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.dtRiscoQuestao_dtPlanoAcao.Responsavel")});
        this.xrTableCell7.Dpi = 100F;
        this.xrTableCell7.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UseBorderWidth = false;
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 0.49775648601996475D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.BorderWidth = 0.2F;
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
                    "to_dtRiscoQuestao.dtRiscoQuestao_dtPlanoAcao.Status")});
        this.xrTableCell8.Dpi = 100F;
        this.xrTableCell8.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseBorderWidth = false;
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 0.19230752647278063D;
        // 
        // xrLine9
        // 
        this.xrLine9.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine9.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine9.BorderWidth = 1F;
        this.xrLine9.Dpi = 100F;
        this.xrLine9.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine9.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine9.Name = "xrLine9";
        this.xrLine9.SizeF = new System.Drawing.SizeF(2F, 13F);
        this.xrLine9.StylePriority.UseBorderColor = false;
        this.xrLine9.StylePriority.UseBorders = false;
        this.xrLine9.StylePriority.UseBorderWidth = false;
        this.xrLine9.StylePriority.UseForeColor = false;
        // 
        // xrLine10
        // 
        this.xrLine10.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine10.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine10.BorderWidth = 1F;
        this.xrLine10.Dpi = 100F;
        this.xrLine10.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine10.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine10.LocationFloat = new DevExpress.Utils.PointFloat(1118F, 0F);
        this.xrLine10.Name = "xrLine10";
        this.xrLine10.SizeF = new System.Drawing.SizeF(2F, 13F);
        this.xrLine10.StylePriority.UseBorderColor = false;
        this.xrLine10.StylePriority.UseBorders = false;
        this.xrLine10.StylePriority.UseBorderWidth = false;
        this.xrLine10.StylePriority.UseForeColor = false;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5,
            this.xrLine7,
            this.xrLine8});
        this.GroupHeader2.Dpi = 100F;
        this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader2.HeightF = 18.17F;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.RepeatEveryPage = true;
        // 
        // xrTable5
        // 
        this.xrTable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
        this.xrTable5.BorderColor = System.Drawing.Color.Gray;
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.BorderWidth = 0.2F;
        this.xrTable5.Dpi = 100F;
        this.xrTable5.Font = new System.Drawing.Font("Arial Narrow", 10F);
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(2.143271F, 4.999993F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(1115.851F, 13.17F);
        this.xrTable5.StylePriority.UseBackColor = false;
        this.xrTable5.StylePriority.UseBorderColor = false;
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseBorderWidth = false;
        this.xrTable5.StylePriority.UseFont = false;
        this.xrTable5.StylePriority.UseTextAlignment = false;
        this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14});
        this.xrTableRow5.Dpi = 100F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 11.5D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrTableCell12.BorderWidth = 0.2F;
        this.xrTableCell12.Dpi = 100F;
        this.xrTableCell12.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBackColor = false;
        this.xrTableCell12.StylePriority.UseBorderWidth = false;
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.Text = "Plano de Ação";
        this.xrTableCell12.Weight = 0.90993594664216693D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.BorderWidth = 0.2F;
        this.xrTableCell13.Dpi = 100F;
        this.xrTableCell13.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBackColor = false;
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseBorderWidth = false;
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Text = "Responsável";
        this.xrTableCell13.Weight = 0.4977522388570253D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.BorderWidth = 0.2F;
        this.xrTableCell14.Dpi = 100F;
        this.xrTableCell14.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBackColor = false;
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseBorderWidth = false;
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.Text = "Status";
        this.xrTableCell14.Weight = 0.19231181450080784D;
        // 
        // xrLine7
        // 
        this.xrLine7.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine7.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine7.BorderWidth = 1F;
        this.xrLine7.Dpi = 100F;
        this.xrLine7.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine7.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine7.LocationFloat = new DevExpress.Utils.PointFloat(1117.995F, 0F);
        this.xrLine7.Name = "xrLine7";
        this.xrLine7.SizeF = new System.Drawing.SizeF(2.004639F, 18.16999F);
        this.xrLine7.StylePriority.UseBorderColor = false;
        this.xrLine7.StylePriority.UseBorders = false;
        this.xrLine7.StylePriority.UseBorderWidth = false;
        this.xrLine7.StylePriority.UseForeColor = false;
        // 
        // xrLine8
        // 
        this.xrLine8.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLine8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine8.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrLine8.BorderWidth = 1F;
        this.xrLine8.Dpi = 100F;
        this.xrLine8.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine8.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine8.LocationFloat = new DevExpress.Utils.PointFloat(2.006327E-05F, 0F);
        this.xrLine8.Name = "xrLine8";
        this.xrLine8.SizeF = new System.Drawing.SizeF(2F, 18.16999F);
        this.xrLine8.StylePriority.UseBorderColor = false;
        this.xrLine8.StylePriority.UseBorders = false;
        this.xrLine8.StylePriority.UseBorderWidth = false;
        this.xrLine8.StylePriority.UseForeColor = false;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine11});
        this.GroupFooter1.Dpi = 100F;
        this.GroupFooter1.HeightF = 15F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLine11
        // 
        this.xrLine11.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLine11.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrLine11.BorderWidth = 1F;
        this.xrLine11.Dpi = 100F;
        this.xrLine11.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine11.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
        this.xrLine11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine11.Name = "xrLine11";
        this.xrLine11.SizeF = new System.Drawing.SizeF(1119F, 2.833327F);
        this.xrLine11.StylePriority.UseBorderColor = false;
        this.xrLine11.StylePriority.UseBorders = false;
        this.xrLine11.StylePriority.UseBorderWidth = false;
        this.xrLine11.StylePriority.UseForeColor = false;
        // 
        // ReportHeader2
        // 
        this.ReportHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel16});
        this.ReportHeader2.Dpi = 100F;
        this.ReportHeader2.HeightF = 14.34911F;
        this.ReportHeader2.Name = "ReportHeader2";
        // 
        // xrLabel16
        // 
        this.xrLabel16.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.xrLabel16.CanGrow = false;
        this.xrLabel16.Dpi = 100F;
        this.xrLabel16.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel16.ForeColor = System.Drawing.Color.White;
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel16.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrLabel16.SizeF = new System.Drawing.SizeF(162.5F, 14.34911F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseForeColor = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "  ASA";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21});
        this.GroupHeader4.Dpi = 100F;
        this.GroupHeader4.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomePrograma", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader4.HeightF = 14F;
        this.GroupHeader4.Name = "GroupHeader4";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel8});
        this.PageHeader.Dpi = 100F;
        this.PageHeader.Expanded = false;
        this.PageHeader.HeightF = 59.84252F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 100F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(991.5857F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(127.4142F, 59.84252F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // xrLabel8
        // 
        this.xrLabel8.Dpi = 100F;
        this.xrLabel8.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(756.25F, 44.83334F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "Relatório de Análise de Problemas e Riscos - Projetos";
        // 
        // xrLogoEntidade
        // 
        this.xrLogoEntidade.Dpi = 100F;
        this.xrLogoEntidade.Image = ((System.Drawing.Image)(resources.GetObject("xrLogoEntidade.Image")));
        this.xrLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(920F, 0F);
        this.xrLogoEntidade.Name = "xrLogoEntidade";
        this.xrLogoEntidade.SizeF = new System.Drawing.SizeF(200F, 50F);
        this.xrLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNomeUnidadeFooter,
            this.lblNomeEntidadeFooter,
            this.xrLogoEntidade});
        this.PageFooter.Dpi = 100F;
        this.PageFooter.HeightF = 64.58334F;
        this.PageFooter.Name = "PageFooter";
        // 
        // lblNomeUnidadeFooter
        // 
        this.lblNomeUnidadeFooter.Dpi = 100F;
        this.lblNomeUnidadeFooter.Font = new System.Drawing.Font("Arial", 7F);
        this.lblNomeUnidadeFooter.ForeColor = System.Drawing.Color.DarkGray;
        this.lblNomeUnidadeFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 30.5F);
        this.lblNomeUnidadeFooter.Name = "lblNomeUnidadeFooter";
        this.lblNomeUnidadeFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeUnidadeFooter.SizeF = new System.Drawing.SizeF(669F, 19F);
        this.lblNomeUnidadeFooter.StylePriority.UseFont = false;
        this.lblNomeUnidadeFooter.StylePriority.UseForeColor = false;
        // 
        // lblNomeEntidadeFooter
        // 
        this.lblNomeEntidadeFooter.Dpi = 100F;
        this.lblNomeEntidadeFooter.Font = new System.Drawing.Font("Arial", 7F);
        this.lblNomeEntidadeFooter.ForeColor = System.Drawing.Color.DarkGray;
        this.lblNomeEntidadeFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 7.5F);
        this.lblNomeEntidadeFooter.Name = "lblNomeEntidadeFooter";
        this.lblNomeEntidadeFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeEntidadeFooter.SizeF = new System.Drawing.SizeF(671F, 19F);
        this.lblNomeEntidadeFooter.StylePriority.UseFont = false;
        this.lblNomeEntidadeFooter.StylePriority.UseForeColor = false;
        // 
        // pSiglaUnidade
        // 
        this.pSiglaUnidade.Name = "pSiglaUnidade";
        // 
        // pNomeUnidade
        // 
        this.pNomeUnidade.Name = "pNomeUnidade";
        // 
        // pSiglaEntidade
        // 
        this.pSiglaEntidade.Name = "pSiglaEntidade";
        // 
        // pNomeEntidade
        // 
        this.pNomeEntidade.Name = "pNomeEntidade";
        // 
        // cc_NomeUnidadeRelatorio
        // 
        this.cc_NomeUnidadeRelatorio.Expression = "[Parameters.pSiglaUnidade] + \' -  \' + [Parameters.pNomeUnidade]";
        this.cc_NomeUnidadeRelatorio.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_NomeUnidadeRelatorio.Name = "cc_NomeUnidadeRelatorio";
        // 
        // cc_NomeEntidadeRelatorio
        // 
        this.cc_NomeEntidadeRelatorio.Expression = "[Parameters.pSiglaEntidade] + \'  -  \' + [Parameters.pNomeEntidade]";
        this.cc_NomeEntidadeRelatorio.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_NomeEntidadeRelatorio.Name = "cc_NomeEntidadeRelatorio";
        // 
        // pfiltro_rblTipoRelatorio
        // 
        this.pfiltro_rblTipoRelatorio.Name = "pfiltro_rblTipoRelatorio";
        // 
        // pfiltro_ddlTipo
        // 
        this.pfiltro_ddlTipo.Name = "pfiltro_ddlTipo";
        // 
        // pfiltro_ddlUnidade
        // 
        this.pfiltro_ddlUnidade.Name = "pfiltro_ddlUnidade";
        // 
        // pfiltro_ddlProjetoPrograma
        // 
        this.pfiltro_ddlProjetoPrograma.Name = "pfiltro_ddlProjetoPrograma";
        // 
        // pfiltro_ddStatusPlanoAcao
        // 
        this.pfiltro_ddStatusPlanoAcao.Name = "pfiltro_ddStatusPlanoAcao";
        // 
        // pfiltro_ckbAtivo
        // 
        this.pfiltro_ckbAtivo.Name = "pfiltro_ckbAtivo";
        // 
        // pfiltro_txtFoco
        // 
        this.pfiltro_txtFoco.Name = "pfiltro_txtFoco";
        // 
        // pfiltro_txtDirecionador
        // 
        this.pfiltro_txtDirecionador.Name = "pfiltro_txtDirecionador";
        // 
        // pfiltro_txtGrandeDesafio
        // 
        this.pfiltro_txtGrandeDesafio.Name = "pfiltro_txtGrandeDesafio";
        // 
        // pfiltro_ckbPlanosAcao
        // 
        this.pfiltro_ckbPlanosAcao.Name = "pfiltro_ckbPlanosAcao";
        // 
        // pfiltro_entidade
        // 
        this.pfiltro_entidade.Name = "pfiltro_entidade";
        this.pfiltro_entidade.Type = typeof(short);
        this.pfiltro_entidade.ValueInfo = "0";
        this.pfiltro_entidade.Visible = false;
        // 
        // cc_RiscoQuestao
        // 
        this.cc_RiscoQuestao.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao";
        this.cc_RiscoQuestao.Expression = "Iif([RiscoQuestao] == \'R\', \'RISCO: \' , \'PROBLEMA:\')";
        this.cc_RiscoQuestao.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_RiscoQuestao.Name = "cc_RiscoQuestao";
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.BackColor = System.Drawing.Color.LightGreen;
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // 
        // cc_LabelTipoQuestao
        // 
        this.cc_LabelTipoQuestao.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao";
        this.cc_LabelTipoQuestao.Expression = "Iif([RiscoQuestao] == \'R\', \'Tipo: \' , \'\')";
        this.cc_LabelTipoQuestao.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_LabelTipoQuestao.Name = "cc_LabelTipoQuestao";
        // 
        // calculatedField1
        // 
        this.calculatedField1.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao";
        this.calculatedField1.DisplayName = "cc_TipoRiscoQuestao";
        this.calculatedField1.Expression = "Iif([RiscoQuestao] == \'R\', [TipoRiscoQuestao], \'\')";
        this.calculatedField1.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField1.Name = "calculatedField1";
        // 
        // pfiltro_TipoProjeto
        // 
        this.pfiltro_TipoProjeto.Name = "pfiltro_TipoProjeto";
        // 
        // pfiltro_AfetaOrcamento
        // 
        this.pfiltro_AfetaOrcamento.Name = "pfiltro_AfetaOrcamento";
        // 
        // pfiltro_AfetaMeta
        // 
        this.pfiltro_AfetaMeta.Name = "pfiltro_AfetaMeta";
        // 
        // cfTituloProjeto
        // 
        this.cfTituloProjeto.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto";
        this.cfTituloProjeto.Expression = "[TipoProjeto] + \': \' + [NomeProjeto]";
        this.cfTituloProjeto.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cfTituloProjeto.Name = "cfTituloProjeto";
        // 
        // cc_AfetaMeta
        // 
        this.cc_AfetaMeta.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao";
        this.cc_AfetaMeta.Expression = "Iif(IsNullOrEmpty([DescricaoMetaAfetada]),\'Não\', \'Sim, meta afetada: \' + [Descric" +
"aoMetaAfetada] )";
        this.cc_AfetaMeta.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_AfetaMeta.Name = "cc_AfetaMeta";
        // 
        // cc_AfetaOrcamento
        // 
        this.cc_AfetaOrcamento.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao";
        this.cc_AfetaOrcamento.DisplayName = "cc_AfetaOrcamento";
        this.cc_AfetaOrcamento.Expression = "Iif(IsNullOrEmpty([TipoVariacaoCusto]), \'Não\'  , \'Sim: Tipo de variação: \' + [cc_" +
"TipoVariacaoCusto] + \' Valor:  R$ \' + Round([ValorVariacaoCusto], 2) )";
        this.cc_AfetaOrcamento.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_AfetaOrcamento.Name = "cc_AfetaOrcamento";
        // 
        // cc_TipoVariacaoCusto
        // 
        this.cc_TipoVariacaoCusto.DataMember = "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.dtProje" +
"to_dtRiscoQuestao";
        this.cc_TipoVariacaoCusto.DisplayName = "cc_TipoVariacaoCusto";
        this.cc_TipoVariacaoCusto.Expression = resources.GetString("cc_TipoVariacaoCusto.Expression");
        this.cc_TipoVariacaoCusto.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.cc_TipoVariacaoCusto.Name = "cc_TipoVariacaoCusto";
        // 
        // xrLabel21
        // 
        this.xrLabel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLabel21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(184)))), ((int)(((byte)(80)))));
        this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrLabel21.BorderWidth = 1F;
        this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtTemaObjetivo.dtTemaObjetivo_dtMeta.dtMeta_dtUnidade.dtUnidade_dtProjeto.NomePro" +
                    "grama", "Programa: {0}")});
        this.xrLabel21.Dpi = 100F;
        this.xrLabel21.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.ForeColor = System.Drawing.Color.White;
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(1120F, 14F);
        this.xrLabel21.StylePriority.UseBackColor = false;
        this.xrLabel21.StylePriority.UseBorderColor = false;
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseBorderWidth = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseForeColor = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // rel_AnaliseProblemasRiscosProjetos_unigest
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.PageHeader,
            this.PageFooter});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.cc_NomeUnidadeRelatorio,
            this.cc_NomeEntidadeRelatorio,
            this.cc_RiscoQuestao,
            this.cc_LabelTipoQuestao,
            this.calculatedField1,
            this.cfTituloProjeto,
            this.cc_AfetaMeta,
            this.cc_AfetaOrcamento,
            this.cc_TipoVariacaoCusto});
        this.DataMember = "dtTemaObjetivo";
        this.DataSource = this.dS_AnaliseProblemasRiscosProjetos_unigest1;
        this.DrawGrid = false;
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(23, 26, 18, 6);
        this.PageHeight = 827;
        this.PageWidth = 1169;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.pSiglaUnidade,
            this.pNomeUnidade,
            this.pSiglaEntidade,
            this.pNomeEntidade,
            this.pfiltro_rblTipoRelatorio,
            this.pfiltro_ddlTipo,
            this.pfiltro_ddlUnidade,
            this.pfiltro_ddlProjetoPrograma,
            this.pfiltro_ddStatusPlanoAcao,
            this.pfiltro_ckbAtivo,
            this.pfiltro_txtFoco,
            this.pfiltro_txtDirecionador,
            this.pfiltro_txtGrandeDesafio,
            this.pfiltro_ckbPlanosAcao,
            this.pfiltro_entidade,
            this.pfiltro_TipoProjeto,
            this.pfiltro_AfetaOrcamento,
            this.pfiltro_AfetaMeta});
        this.SnapGridSize = 0.1F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "16.2";
        this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.rel_AnaliseProblemasRiscosProjetos_DataSourceDemanded);
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_AnaliseProblemasRiscosProjetos1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dS_AnaliseProblemasRiscosProjetos_unigest1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion

    private void rel_AnaliseProblemasRiscosProjetos_DataSourceDemanded(object sender, EventArgs e)
    {
        InitData();
        DefineLogoEntidade();
        //DefineLogoUnidade(int.Parse(pfiltro_ddlUnidade.Value.ToString()));
    }
}
