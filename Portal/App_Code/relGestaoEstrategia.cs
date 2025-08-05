using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for relEstruturacaoFormal
/// </summary>
public class relGestaoEstrategia : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private XRLabel xrLabel1;

    private DevExpress.XtraReports.Parameters.Parameter pNomeProjeto = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeGerente = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoProjeto = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pLogoUnidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoEntidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pCodigoUnidade = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pNomeMapa = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pdataImpressao = new DevExpress.XtraReports.Parameters.Parameter();

    dados cDados = CdadosUtil.GetCdados(null);
    private PageHeaderBand PageHeader;
    private XRPictureBox logoUnidade;
    private XRLabel lblNomeMapa;
    //private string dataImpressao = "";
    private XRPictureBox imgIndicaObjetivo;
    private XRLabel lblObjetivo;
    private XRLabel xrLabel2;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell celNomeProjeto;
    private XRTableCell celConcluido;
    private XRTableCell celResponsavel;
    private XRPictureBox imgDesempenhoProjeto;
    private XRTableCell celDesempenho;
    private XRPictureBox imgCorObjetivoEstrategico;
    private DetailReportBand dtlRptIniciativas;
    private DetailBand detailBandIniciativa;
    private DetailReportBand dtlRptIndicadores;
    private DetailBand detailBandIndicador;
    private DataSet3 dsRelGestaoEstrategia;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel3;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLine xrLine1;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRPageInfo xrPageInfo2;
    private GroupHeaderBand GroupHeader2;
    private GroupHeaderBand GroupHeader3;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRLabel xrLabel4;
    private GroupHeaderBand GroupHeader4;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell5;
    private GroupHeaderBand GroupHeader5;
    private XRTableCell xrTableCell6;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private GroupHeaderBand GroupHeader6;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel5;
    private XRLabel lblTituloUltimaRecomendacao;
    private XRLabel lblConteudoUltimaRecomendacao;
    private XRLabel lblConteudoUltimaAnalise;
    private XRLabel lblTituloUltimaAnalise;
    private DetailReportBand drLabelUltimaAnalise;
    private DetailBand Detail2;
    private DetailReportBand drUltimaAnalise;
    private DetailBand Detail3;
    private DetailReportBand drLabelUltimaRecomendacao;
    private DetailBand Detail4;
    private DetailReportBand drUltimaRecomendacao;
    private DetailBand Detail5;
    private XRLabel xrLabel6;
    private XRPictureBox imgPlanoAcao;
    private DetailReportBand drNomeIndicador;
    private DetailBand Detail6;
    private XRPictureBox imgDesempenhoIndicador;
    private XRLabel lblIndicador;
    private XRPictureBox imgIndicaIndicador;
    private DetailReportBand drGraficoIndicador;
    private DetailBand drGrafico;
    private XRChart xrChart1;
    private XRShape seta_cima;
    private XRLabel xrLabel7;
    private DetailReportBand DetailReport3;
    private DetailBand Detail9;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell21;
    private XRPictureBox imgPlanoAcaoIndicador;
    private GroupHeaderBand GroupHeader8;
    private XRTable xrTable7;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private GroupHeaderBand GroupHeader9;
    private XRTable xrTable8;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell14;
    private XRPictureBox imgStatusPlanoAcaoIndicadorTitulo;
    private GroupHeaderBand GroupHeader12;
    private GroupFooterBand GroupFooter3;
    private XRLabel xrLabel8;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relGestaoEstrategia(int CodigoMapa, int CodigoUnidade)
    {
        InitializeComponent();
        pNomeProjeto.Name = "pNomeProjeto";
        pNomeGerente.Name = "pNomeGerente";
        pCodigoProjeto.Name = "pCodigoProjeto";
        pLogoUnidade.Name = "pLogoUnidade";
        pCodigoEntidade.Name = "pCodigoEntidade";
        pCodigoUnidade.Name = "pCodigoUnidade";
        pNomeMapa.Name = "pNomeMapa";
        pdataImpressao.Name = "pdataImpressao";
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
        {
            this.pNomeProjeto,
            this.pNomeGerente,
            this.pCodigoProjeto,
            this.pLogoUnidade,
            this.pCodigoEntidade,
            this.pCodigoUnidade,
            this.pNomeMapa,
            this.pdataImpressao});


        InitData(CodigoMapa, CodigoUnidade);
    }

    private void InitData(int CodigoMapa, int CodigoUnidade)
    {
        int codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();
        //xrPictureBox1.Image = logo;
        comandoSql = string.Format(@"
        
        BEGIN
            DECLARE @CodigoMapa AS INTEGER
            SET @CodigoMapa = {2}
            DECLARE @CodigoUnidade AS INTEGER 
        SET @CodigoUnidade = {3}
	    SELECT persp.TituloObjetoEstrategia			    AS Perspectiva
	           ,oe.DescricaoObjetoEstrategia		    AS Objetivo
	           ,oe.CodigoObjetoEstrategia				AS CodigoObjetivo
	           ,{0}.{1}.f_GetCorObjetivo(mp.CodigoUnidadeNegocio, oe.CodigoObjetoEstrategia, YEAR(GETDATE()), MONTH(GETDATE())) AS CorObjetivo
	           ,tema.DescricaoObjetoEstrategia	        AS Tema
         FROM dbo.ObjetoEstrategia                      AS oe
	          INNER JOIN {0}.{1}.TipoObjetoEstrategia	AS toe1	ON (toe1.CodigoTipoObjetoEstrategia	= oe.CodigoTipoObjetoEstrategia)
    	      INNER JOIN {0}.{1}.ObjetoEstrategia		AS tema ON (tema.CodigoObjetoEstrategia		= oe.CodigoObjetoEstrategiaSuperior)
              INNER JOIN {0}.{1}.TipoObjetoEstrategia	AS toe2	ON (toe2.CodigoTipoObjetoEstrategia	= tema.CodigoTipoObjetoEstrategia)
		      INNER JOIN {0}.{1}.ObjetoEstrategia		AS persp ON (persp.CodigoObjetoEstrategia	= tema.CodigoObjetoEstrategiaSuperior)
	 	      INNER JOIN {0}.{1}.TipoObjetoEstrategia	AS toe3	ON (toe3.CodigoTipoObjetoEstrategia	= persp.CodigoTipoObjetoEstrategia)
		      INNER JOIN {0}.{1}.MapaEstrategico		AS mp	ON (mp.CodigoMapaEstrategico = oe.CodigoMapaEstrategico)
        WHERE toe1.IniciaisTipoObjeto = 'OBJ'
	      AND toe2.IniciaisTipoObjeto = 'TEM'
	      AND toe3.IniciaisTipoObjeto	= 'PSP'
	      AND oe.CodigoMapaEstrategico = @CodigoMapa 
	      AND oe.DataExclusao	IS  NULL
        UNION
   SELECT persp.TituloObjetoEstrategia                  AS Perspectiva
	      ,oe.DescricaoObjetoEstrategia                 AS Objetivo
	      ,oe.CodigoObjetoEstrategia                    AS CodigoObjetivo
	      ,{0}.{1}.f_GetCorObjetivo(mp.CodigoUnidadeNegocio, oe.CodigoObjetoEstrategia, YEAR(GETDATE()), MONTH(GETDATE())) AS CorObjetivo
	      ,CAST(NULL AS Varchar(500))                   AS Tema
     FROM {0}.{1}.ObjetoEstrategia	                    AS oe		
	      INNER JOIN {0}.{1}.TipoObjetoEstrategia	    AS toe1	 ON(toe1.CodigoTipoObjetoEstrategia	= oe.CodigoTipoObjetoEstrategia)
	      INNER JOIN {0}.{1}.ObjetoEstrategia		    AS persp ON(persp.CodigoObjetoEstrategia =  oe.CodigoObjetoEstrategiaSuperior)
	      INNER JOIN {0}.{1}.TipoObjetoEstrategia	    AS toe3	 ON(toe3.CodigoTipoObjetoEstrategia	= persp.CodigoTipoObjetoEstrategia)
	      INNER JOIN {0}.{1}.MapaEstrategico		    AS mp	 ON(mp.CodigoMapaEstrategico = oe.CodigoMapaEstrategico)
    WHERE toe1.IniciaisTipoObjeto	= 'OBJ'
	  AND toe3.IniciaisTipoObjeto	= 'PSP'
	  AND oe.CodigoMapaEstrategico= @CodigoMapa 
	AND oe.DataExclusao IS  NULL
ORDER BY 1,2, 5 -- Perspectiva -> Objetivo -> Indicador
--=================================================================================================================


	SELECT
         ioe.CodigoObjetivoEstrategico,
         i.NomeIndicador ,
         ioe.CodigoIndicador,
         {0}.{1}.f_GetUltimoDesempenhoIndicador(@CodigoUnidade,i.CodigoIndicador,year(GETDATE()),month(GETDATE()),'A') as desempenhoIndicador,
         a.Analise as UltimaAnalise,
         case when a.Analise like '%_%' then 'Última Análise:' else null end as labelUltimaAnalise,
         a.Recomendacoes as UltimaRecomendacao,
         case when a.Recomendacoes like '%_%' then 'Última Recomendação:' else null end as labelUltimaRecomendacao,
         a.DataAnalisePerformance as labelDataUltimaAnalise,
         i.Polaridade
  FROM
         {0}.{1}.IndicadorObjetivoEstrategico ioe
         INNER JOIN {0}.{1}.Indicador i on (i.CodigoIndicador = ioe.CodigoIndicador and i.DataExclusao is null)
         INNER JOIN {0}.{1}.ObjetoEstrategia AS oe ON (oe.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico AND oe.CodigoMapaEstrategico = @CodigoMapa)
          LEFT JOIN dbo.AnalisePerformance a ON (a.CodigoIndicador = ioe.CodigoIndicador  and a.CodigoAnalisePerformance in
(
SELECT sub.CodigoAnalisePerformance
FROM 
(SELECT MAX(ap.[CodigoAnalisePerformance]) AS CodigoAnalisePerformance, ap.[CodigoIndicador]
FROM [dbo].[AnalisePerformance] ap
WHERE ap.[DataExclusao] IS NULL
GROUP BY ap.[CodigoIndicador]
) AS sub
) )

  ORDER BY CodigoIndicador
--=======================================================================================================================
                    
  SELECT 
          poe.CodigoObjetivoEstrategico, p.NomeProjeto AS NomeProjeto,
          rp.PercentualRealizacao                      AS Concluido,
          u.NomeUsuario                                AS Responsavel,
          RTRIM(LTRIM(rp.CorGeral))                    AS Desempenho
   FROM
          {0}.{1}.ProjetoObjetivoEstrategico poe
          INNER JOIN {0}.{1}.ResumoProjeto rp ON (rp.CodigoProjeto = poe.CodigoProjeto)
          INNER JOIN {0}.{1}.Projeto p        ON (p.CodigoProjeto = rp.CodigoProjeto)
          INNER JOIN {0}.{1}.Usuario u        ON u.CodigoUsuario = p.CodigoGerenteProjeto
		  INNER JOIN {0}.{1}.ObjetoEstrategia AS oe ON (oe.CodigoObjetoEstrategia = poe.CodigoObjetivoEstrategico AND oe.CodigoMapaEstrategico = @CodigoMapa)
           LEFT JOIN {0}.{1}.ParametroConfiguracaoSistema AS pcs ON (pcs.Parametro = 'MostraProjetoEncerradoObjetivoEstrategico' and pcs.CodigoEntidade = p.CodigoEntidade)
          WHERE    p.DataExclusao IS NULL
                  AND p.CodigoEntidade = {4}
                  AND (pcs.Valor = 'S' OR p.CodigoStatusProjeto <> 6)
   ORDER BY  p.NomeProjeto, poe.CodigoObjetivoEstrategico
END
--=========================================================================================================================


SELECT tdl.[CodigoToDoList]
      ,tdl.[NomeToDoList]
      ,ttdl.DescricaoTarefa
      ,(f.PercentualConcluido) as PercentualConcluido
      ,u.NomeUsuario
      ,tdl.CodigoObjetoAssociado as CodigoObjetivo
      ,st.DescricaoStatusTarefa as Status
      ,{0}.{1}.f_GetDesempenhoPA(tdl.CodigoToDoList, @CodigoUnidade)  AS Desempenho
      ,CASE WHEN f.Estagio = 'Atrasada' THEN 'Vermelho' ELSE 'Verde' END AS DesempenhoTarefa
  FROM {0}.{1}.[ToDoList] tdl
inner join {0}.{1}.TarefaToDoList ttdl on (ttdl.CodigoToDoList = tdl.CodigoToDoList)
inner join {0}.{1}.Usuario u on (ttdl.CodigoUsuarioResponsavelTarefa = u.CodigoUsuario)
inner join {0}.{1}.StatusTarefa st on (st.CodigoStatusTarefa = ttdl.CodigoStatusTarefa)
INNER JOIN {0}.{1}.ObjetoEstrategia AS oe ON (oe.CodigoObjetoEstrategia = tdl.CodigoObjetoAssociado AND oe.CodigoMapaEstrategico = @CodigoMapa)
inner join {0}.{1}.f_GetTarefasToDoListProjeto({3},-1,-1) as f on (f.CodigoTarefa = ttdl.CodigoTarefa)
where tdl.[CodigoTipoAssociacao] = {0}.{1}.[f_GetCodigoTipoAssociacao] ('OB')  and st.DescricaoStatusTarefa <> 'Cancelada'

--=========================================================================================================================

SELECT tdl.[CodigoToDoList]
      ,tdl.[NomeToDoList]
      ,ttdl.DescricaoTarefa
      ,(f.PercentualConcluido/100) as PercentualConcluido
      ,u.NomeUsuario
      ,tdl.CodigoObjetoAssociado as CodigoIndicador
      ,st.DescricaoStatusTarefa as Status
      ,{0}.{1}.f_GetDesempenhoPA(tdl.CodigoToDoList, @CodigoUnidade)  AS Desempenho
      ,CASE WHEN f.Estagio = 'Atrasada' THEN 'Vermelho' ELSE 'Verde' END AS DesempenhoTarefa
  FROM {0}.{1}.[ToDoList] tdl
inner join {0}.{1}.TarefaToDoList ttdl on (ttdl.CodigoToDoList = tdl.CodigoToDoList)
inner join {0}.{1}.Usuario u on (ttdl.CodigoUsuarioResponsavelTarefa = u.CodigoUsuario)
inner join {0}.{1}.StatusTarefa st on (st.CodigoStatusTarefa = ttdl.CodigoStatusTarefa)
inner join {0}.{1}.IndicadorObjetivoEstrategico AS ioe ON ioe.CodigoIndicador = tdl.CodigoObjetoAssociado
inner join {0}.{1}.ObjetoEstrategia AS oe ON oe.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico AND oe.CodigoMapaEstrategico = @CodigoMapa
inner join {0}.{1}.f_GetTarefasToDoListProjeto({3},-1,-1) as f on (f.CodigoTarefa = ttdl.CodigoTarefa)
where tdl.[CodigoTipoAssociacao] = {0}.{1}.[f_GetCodigoTipoAssociacao] ('IN')  and st.DescricaoStatusTarefa <> 'Cancelada' ", cDados.getDbName(), cDados.getDbOwner(), CodigoMapa, CodigoUnidade, codigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSql);

        dsRelGestaoEstrategia.Load(ds.Tables[0].CreateDataReader(), LoadOption.OverwriteChanges, "Objetivos");
        dsRelGestaoEstrategia.Load(ds.Tables[1].CreateDataReader(), LoadOption.OverwriteChanges, "Indicadores");
        dsRelGestaoEstrategia.Load(ds.Tables[2].CreateDataReader(), LoadOption.OverwriteChanges, "Iniciativas");
        dsRelGestaoEstrategia.Load(ds.Tables[3].CreateDataReader(), LoadOption.OverwriteChanges, "PlanoAcao");
        dsRelGestaoEstrategia.Load(ds.Tables[4].CreateDataReader(), LoadOption.OverwriteChanges, "PlanoAcaoInd");
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
        string resourceFileName = "relGestaoEstrategia.resx";
        System.Resources.ResourceManager resources = global::Resources.relGestaoEstrategia.ResourceManager;
        DevExpress.XtraPrinting.Shape.ShapeArrow shapeArrow1 = new DevExpress.XtraPrinting.Shape.ShapeArrow();
        DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
        DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
        DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
        DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView4 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView5 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView6 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView7 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
        DevExpress.XtraCharts.RectangleGradientFillOptions rectangleGradientFillOptions1 = new DevExpress.XtraCharts.RectangleGradientFillOptions();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.imgIndicaObjetivo = new DevExpress.XtraReports.UI.XRPictureBox();
        this.imgCorObjetivoEstrategico = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblObjetivo = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.logoUnidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblNomeMapa = new DevExpress.XtraReports.UI.XRLabel();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.imgDesempenhoProjeto = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celNomeProjeto = new DevExpress.XtraReports.UI.XRTableCell();
        this.celConcluido = new DevExpress.XtraReports.UI.XRTableCell();
        this.celResponsavel = new DevExpress.XtraReports.UI.XRTableCell();
        this.celDesempenho = new DevExpress.XtraReports.UI.XRTableCell();
        this.dtlRptIniciativas = new DevExpress.XtraReports.UI.DetailReportBand();
        this.detailBandIniciativa = new DevExpress.XtraReports.UI.DetailBand();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.dsRelGestaoEstrategia = new DataSet3();
        this.dtlRptIndicadores = new DevExpress.XtraReports.UI.DetailReportBand();
        this.detailBandIndicador = new DevExpress.XtraReports.UI.DetailBand();
        this.drLabelUltimaAnalise = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTituloUltimaAnalise = new DevExpress.XtraReports.UI.XRLabel();
        this.drUltimaAnalise = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.lblConteudoUltimaAnalise = new DevExpress.XtraReports.UI.XRLabel();
        this.drLabelUltimaRecomendacao = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.lblTituloUltimaRecomendacao = new DevExpress.XtraReports.UI.XRLabel();
        this.drUltimaRecomendacao = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        this.lblConteudoUltimaRecomendacao = new DevExpress.XtraReports.UI.XRLabel();
        this.drNomeIndicador = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.imgDesempenhoIndicador = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblIndicador = new DevExpress.XtraReports.UI.XRLabel();
        this.imgIndicaIndicador = new DevExpress.XtraReports.UI.XRPictureBox();
        this.drGraficoIndicador = new DevExpress.XtraReports.UI.DetailReportBand();
        this.drGrafico = new DevExpress.XtraReports.UI.DetailBand();
        this.seta_cima = new DevExpress.XtraReports.UI.XRShape();
        this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
        this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail9 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.imgPlanoAcaoIndicador = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupHeader8 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader9 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.imgStatusPlanoAcaoIndicadorTitulo = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupHeader12 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.imgPlanoAcao = new DevExpress.XtraReports.UI.XRPictureBox();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelGestaoEstrategia)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgIndicaObjetivo,
            this.imgCorObjetivoEstrategico,
            this.lblObjetivo});
        this.Detail.HeightF = 33.29166F;
        this.Detail.KeepTogetherWithDetailReports = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // imgIndicaObjetivo
        // 
        this.imgIndicaObjetivo.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("imgIndicaObjetivo.ImageSource"));
        this.imgIndicaObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 7F);
        this.imgIndicaObjetivo.Name = "imgIndicaObjetivo";
        this.imgIndicaObjetivo.SizeF = new System.Drawing.SizeF(23F, 20F);
        // 
        // imgCorObjetivoEstrategico
        // 
        this.imgCorObjetivoEstrategico.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgCorObjetivoEstrategico.BorderWidth = 0F;
        this.imgCorObjetivoEstrategico.LocationFloat = new DevExpress.Utils.PointFloat(744.9999F, 7F);
        this.imgCorObjetivoEstrategico.Name = "imgCorObjetivoEstrategico";
        this.imgCorObjetivoEstrategico.SizeF = new System.Drawing.SizeF(21.99994F, 20.00001F);
        this.imgCorObjetivoEstrategico.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.imgCorObjetivoEstrategico.StylePriority.UseBorders = false;
        this.imgCorObjetivoEstrategico.StylePriority.UseBorderWidth = false;
        this.imgCorObjetivoEstrategico.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.imgCorObjetivoEstrategico_BeforePrint);
        // 
        // lblObjetivo
        // 
        this.lblObjetivo.BorderColor = System.Drawing.Color.Goldenrod;
        this.lblObjetivo.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblObjetivo.BorderWidth = 2F;
        this.lblObjetivo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivo]")});
        this.lblObjetivo.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblObjetivo.ForeColor = System.Drawing.Color.DarkGoldenrod;
        this.lblObjetivo.LocationFloat = new DevExpress.Utils.PointFloat(22.99997F, 7F);
        this.lblObjetivo.Name = "lblObjetivo";
        this.lblObjetivo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblObjetivo.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblObjetivo.SizeF = new System.Drawing.SizeF(721.9999F, 20F);
        this.lblObjetivo.StylePriority.UseBorderColor = false;
        this.lblObjetivo.StylePriority.UseBorders = false;
        this.lblObjetivo.StylePriority.UseBorderWidth = false;
        this.lblObjetivo.StylePriority.UseFont = false;
        this.lblObjetivo.StylePriority.UseForeColor = false;
        this.lblObjetivo.Text = "lblObjetivo";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(143.6849F, 2.999998F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(622.3152F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "Relatório de Gestão da Estratégia";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // logoUnidade
        // 
        this.logoUnidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.logoUnidade.Name = "logoUnidade";
        this.logoUnidade.SizeF = new System.Drawing.SizeF(143.6849F, 61.98672F);
        this.logoUnidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // lblNomeMapa
        // 
        this.lblNomeMapa.BorderColor = System.Drawing.Color.DarkGray;
        this.lblNomeMapa.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblNomeMapa.BorderWidth = 1F;
        this.lblNomeMapa.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.lblNomeMapa.ForeColor = System.Drawing.Color.DarkGray;
        this.lblNomeMapa.LocationFloat = new DevExpress.Utils.PointFloat(143.6849F, 28.00001F);
        this.lblNomeMapa.Name = "lblNomeMapa";
        this.lblNomeMapa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeMapa.SizeF = new System.Drawing.SizeF(622.3152F, 16F);
        this.lblNomeMapa.StylePriority.UseBorderColor = false;
        this.lblNomeMapa.StylePriority.UseBorders = false;
        this.lblNomeMapa.StylePriority.UseBorderWidth = false;
        this.lblNomeMapa.StylePriority.UseFont = false;
        this.lblNomeMapa.StylePriority.UseForeColor = false;
        this.lblNomeMapa.StylePriority.UseTextAlignment = false;
        this.lblNomeMapa.Text = "lblNomeMapa";
        this.lblNomeMapa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.lblNomeMapa,
            this.logoUnidade,
            this.xrLabel1,
            this.xrLine1});
        this.PageHeader.HeightF = 77.12885F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7.75F);
        this.xrPageInfo2.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(506.25F, 55F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(258.8413F, 13.98672F);
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseForeColor = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrPageInfo2.TextFormatString = "Impresso em: {0:dd/MM/yyyy HH:mm}";
        // 
        // xrLine1
        // 
        this.xrLine1.BorderColor = System.Drawing.Color.Silver;
        this.xrLine1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 69.39767F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(766.0001F, 2F);
        this.xrLine1.StylePriority.UseBorderColor = false;
        this.xrLine1.StylePriority.UseBorders = false;
        // 
        // imgDesempenhoProjeto
        // 
        this.imgDesempenhoProjeto.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgDesempenhoProjeto.BorderWidth = 0F;
        this.imgDesempenhoProjeto.LocationFloat = new DevExpress.Utils.PointFloat(44.01765F, 2F);
        this.imgDesempenhoProjeto.Name = "imgDesempenhoProjeto";
        this.imgDesempenhoProjeto.SizeF = new System.Drawing.SizeF(18.69513F, 12F);
        this.imgDesempenhoProjeto.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        this.imgDesempenhoProjeto.StylePriority.UseBorders = false;
        this.imgDesempenhoProjeto.StylePriority.UseBorderWidth = false;
        this.imgDesempenhoProjeto.StylePriority.UsePadding = false;
        this.imgDesempenhoProjeto.StylePriority.UseTextAlignment = false;
        this.imgDesempenhoProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.imgDesempenhoProjeto_BeforePrint);
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.ForeColor = System.Drawing.Color.Black;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(765.9999F, 23F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseForeColor = false;
        this.xrLabel2.Text = "Iniciativas do Objetivo";
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001659393F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(765.9998F, 14.00002F);
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.Text = "Nome do Projeto";
        this.xrTableCell1.Weight = 0.962435696577542D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.xrTableCell2.StylePriority.UseBackColor = false;
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "(%) Concluído";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell2.Weight = 0.399708755699825D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell3.StylePriority.UseBackColor = false;
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.Text = "Responsável";
        this.xrTableCell3.Weight = 1.22945860115775D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrTableCell4.StylePriority.UseBackColor = false;
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Desempenho";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell4.Weight = 0.427578532242635D;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(765.9998F, 16F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celNomeProjeto,
            this.celConcluido,
            this.celResponsavel,
            this.celDesempenho});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // celNomeProjeto
        // 
        this.celNomeProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celNomeProjeto.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeProjeto]")});
        this.celNomeProjeto.Name = "celNomeProjeto";
        this.celNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celNomeProjeto.StylePriority.UseBorders = false;
        this.celNomeProjeto.StylePriority.UsePadding = false;
        this.celNomeProjeto.StylePriority.UseTextAlignment = false;
        this.celNomeProjeto.Text = "celNomeProjeto";
        this.celNomeProjeto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celNomeProjeto.Weight = 0.826135440879127D;
        // 
        // celConcluido
        // 
        this.celConcluido.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celConcluido.CanShrink = true;
        this.celConcluido.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Concluido]")});
        this.celConcluido.Multiline = true;
        this.celConcluido.Name = "celConcluido";
        this.celConcluido.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.celConcluido.StylePriority.UseBorders = false;
        this.celConcluido.StylePriority.UsePadding = false;
        this.celConcluido.StylePriority.UseTextAlignment = false;
        this.celConcluido.Text = "celConcluido";
        this.celConcluido.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.celConcluido.TextFormatString = "{0:0%}";
        this.celConcluido.Weight = 0.343101043740843D;
        // 
        // celResponsavel
        // 
        this.celResponsavel.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celResponsavel.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Responsavel]")});
        this.celResponsavel.Name = "celResponsavel";
        this.celResponsavel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.celResponsavel.StylePriority.UseBorders = false;
        this.celResponsavel.StylePriority.UsePadding = false;
        this.celResponsavel.StylePriority.UseTextAlignment = false;
        this.celResponsavel.Text = "celResponsavel";
        this.celResponsavel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celResponsavel.Weight = 1.05534234196185D;
        // 
        // celDesempenho
        // 
        this.celDesempenho.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celDesempenho.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgDesempenhoProjeto});
        this.celDesempenho.Name = "celDesempenho";
        this.celDesempenho.StylePriority.UseBorders = false;
        this.celDesempenho.StylePriority.UseTextAlignment = false;
        this.celDesempenho.Text = "celDesempenho";
        this.celDesempenho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celDesempenho.Weight = 0.367024226853291D;
        // 
        // dtlRptIniciativas
        // 
        this.dtlRptIniciativas.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.detailBandIniciativa,
            this.GroupHeader2,
            this.GroupHeader3});
        this.dtlRptIniciativas.DataMember = "Objetivos.Objetivos_Iniciativas";
        this.dtlRptIniciativas.DataSource = this.dsRelGestaoEstrategia;
        this.dtlRptIniciativas.Level = 1;
        this.dtlRptIniciativas.Name = "dtlRptIniciativas";
        this.dtlRptIniciativas.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // detailBandIniciativa
        // 
        this.detailBandIniciativa.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.detailBandIniciativa.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.detailBandIniciativa.HeightF = 16F;
        this.detailBandIniciativa.Name = "detailBandIniciativa";
        this.detailBandIniciativa.StylePriority.UseBorders = false;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.GroupHeader2.HeightF = 14.00002F;
        this.GroupHeader2.Name = "GroupHeader2";
        this.GroupHeader2.RepeatEveryPage = true;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
        this.GroupHeader3.HeightF = 39.58333F;
        this.GroupHeader3.Level = 1;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // dsRelGestaoEstrategia
        // 
        this.dsRelGestaoEstrategia.DataSetName = "dsRelGestaoEstrategia";
        this.dsRelGestaoEstrategia.EnforceConstraints = false;
        this.dsRelGestaoEstrategia.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // dtlRptIndicadores
        // 
        this.dtlRptIndicadores.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.detailBandIndicador,
            this.drLabelUltimaAnalise,
            this.drUltimaAnalise,
            this.drLabelUltimaRecomendacao,
            this.drUltimaRecomendacao,
            this.drNomeIndicador,
            this.drGraficoIndicador,
            this.DetailReport3});
        this.dtlRptIndicadores.DataMember = "Objetivos.Objetivos_Indicadores";
        this.dtlRptIndicadores.DataSource = this.dsRelGestaoEstrategia;
        this.dtlRptIndicadores.Level = 0;
        this.dtlRptIndicadores.Name = "dtlRptIndicadores";
        this.dtlRptIndicadores.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // detailBandIndicador
        // 
        this.detailBandIndicador.Expanded = false;
        this.detailBandIndicador.HeightF = 0F;
        this.detailBandIndicador.KeepTogetherWithDetailReports = true;
        this.detailBandIndicador.Name = "detailBandIndicador";
        // 
        // drLabelUltimaAnalise
        // 
        this.drLabelUltimaAnalise.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
        this.drLabelUltimaAnalise.Expanded = false;
        this.drLabelUltimaAnalise.Level = 2;
        this.drLabelUltimaAnalise.Name = "drLabelUltimaAnalise";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.lblTituloUltimaAnalise});
        this.Detail2.Expanded = false;
        this.Detail2.HeightF = 14F;
        this.Detail2.Name = "Detail2";
        // 
        // xrLabel6
        // 
        this.xrLabel6.CanShrink = true;
        this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivos.Objetivos_Indicadores.labelDataUltimaAnalise]")});
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(671.1027F, 0F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(94.39709F, 13F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.TextFormatString = "{0:dd/MM/yyyy}";
        // 
        // lblTituloUltimaAnalise
        // 
        this.lblTituloUltimaAnalise.CanShrink = true;
        this.lblTituloUltimaAnalise.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivos.Objetivos_Indicadores.labelUltimaAnalise]")});
        this.lblTituloUltimaAnalise.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.lblTituloUltimaAnalise.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblTituloUltimaAnalise.Name = "lblTituloUltimaAnalise";
        this.lblTituloUltimaAnalise.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTituloUltimaAnalise.SizeF = new System.Drawing.SizeF(671.1027F, 14F);
        this.lblTituloUltimaAnalise.StylePriority.UseFont = false;
        this.lblTituloUltimaAnalise.TextFormatString = "{0:dd/MM/yyyy}";
        // 
        // drUltimaAnalise
        // 
        this.drUltimaAnalise.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3});
        this.drUltimaAnalise.Expanded = false;
        this.drUltimaAnalise.Level = 3;
        this.drUltimaAnalise.Name = "drUltimaAnalise";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblConteudoUltimaAnalise});
        this.Detail3.HeightF = 15F;
        this.Detail3.Name = "Detail3";
        // 
        // lblConteudoUltimaAnalise
        // 
        this.lblConteudoUltimaAnalise.CanShrink = true;
        this.lblConteudoUltimaAnalise.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivos.Objetivos_Indicadores.UltimaAnalise]")});
        this.lblConteudoUltimaAnalise.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblConteudoUltimaAnalise.LocationFloat = new DevExpress.Utils.PointFloat(29.00024F, 0F);
        this.lblConteudoUltimaAnalise.Name = "lblConteudoUltimaAnalise";
        this.lblConteudoUltimaAnalise.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblConteudoUltimaAnalise.SizeF = new System.Drawing.SizeF(737.9998F, 15F);
        this.lblConteudoUltimaAnalise.StylePriority.UseFont = false;
        this.lblConteudoUltimaAnalise.Text = "lblConteudoUltimaAnalise";
        // 
        // drLabelUltimaRecomendacao
        // 
        this.drLabelUltimaRecomendacao.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4});
        this.drLabelUltimaRecomendacao.Expanded = false;
        this.drLabelUltimaRecomendacao.Level = 4;
        this.drLabelUltimaRecomendacao.Name = "drLabelUltimaRecomendacao";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTituloUltimaRecomendacao});
        this.Detail4.Expanded = false;
        this.Detail4.HeightF = 19F;
        this.Detail4.Name = "Detail4";
        // 
        // lblTituloUltimaRecomendacao
        // 
        this.lblTituloUltimaRecomendacao.CanShrink = true;
        this.lblTituloUltimaRecomendacao.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivos.Objetivos_Indicadores.labelUltimaRecomendacao]")});
        this.lblTituloUltimaRecomendacao.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.lblTituloUltimaRecomendacao.LocationFloat = new DevExpress.Utils.PointFloat(0.4998364F, 0F);
        this.lblTituloUltimaRecomendacao.Name = "lblTituloUltimaRecomendacao";
        this.lblTituloUltimaRecomendacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTituloUltimaRecomendacao.SizeF = new System.Drawing.SizeF(761.9998F, 19F);
        this.lblTituloUltimaRecomendacao.StylePriority.UseFont = false;
        this.lblTituloUltimaRecomendacao.Text = "lblTituloUltimaRecomendacao";
        // 
        // drUltimaRecomendacao
        // 
        this.drUltimaRecomendacao.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5});
        this.drUltimaRecomendacao.Expanded = false;
        this.drUltimaRecomendacao.Level = 5;
        this.drUltimaRecomendacao.Name = "drUltimaRecomendacao";
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblConteudoUltimaRecomendacao});
        this.Detail5.HeightF = 12F;
        this.Detail5.Name = "Detail5";
        // 
        // lblConteudoUltimaRecomendacao
        // 
        this.lblConteudoUltimaRecomendacao.CanShrink = true;
        this.lblConteudoUltimaRecomendacao.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivos.Objetivos_Indicadores.UltimaRecomendacao]")});
        this.lblConteudoUltimaRecomendacao.Font = new System.Drawing.Font("Verdana", 7F);
        this.lblConteudoUltimaRecomendacao.LocationFloat = new DevExpress.Utils.PointFloat(29.00024F, 0F);
        this.lblConteudoUltimaRecomendacao.Name = "lblConteudoUltimaRecomendacao";
        this.lblConteudoUltimaRecomendacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblConteudoUltimaRecomendacao.SizeF = new System.Drawing.SizeF(737.9998F, 12F);
        this.lblConteudoUltimaRecomendacao.StylePriority.UseFont = false;
        this.lblConteudoUltimaRecomendacao.Text = "lblConteudoUltimaRecomendacao";
        // 
        // drNomeIndicador
        // 
        this.drNomeIndicador.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6});
        this.drNomeIndicador.Level = 0;
        this.drNomeIndicador.Name = "drNomeIndicador";
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgDesempenhoIndicador,
            this.lblIndicador,
            this.imgIndicaIndicador});
        this.Detail6.HeightF = 38.75F;
        this.Detail6.KeepTogether = true;
        this.Detail6.KeepTogetherWithDetailReports = true;
        this.Detail6.Name = "Detail6";
        // 
        // imgDesempenhoIndicador
        // 
        this.imgDesempenhoIndicador.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgDesempenhoIndicador.BorderWidth = 0F;
        this.imgDesempenhoIndicador.LocationFloat = new DevExpress.Utils.PointFloat(747.4998F, 11.99999F);
        this.imgDesempenhoIndicador.Name = "imgDesempenhoIndicador";
        this.imgDesempenhoIndicador.SizeF = new System.Drawing.SizeF(18F, 16F);
        this.imgDesempenhoIndicador.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.imgDesempenhoIndicador.StylePriority.UseBorders = false;
        this.imgDesempenhoIndicador.StylePriority.UseBorderWidth = false;
        this.imgDesempenhoIndicador.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.imgDesempenhoIndicador_BeforePrint);
        // 
        // lblIndicador
        // 
        this.lblIndicador.BorderColor = System.Drawing.Color.Black;
        this.lblIndicador.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblIndicador.BorderWidth = 2F;
        this.lblIndicador.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Objetivos.Objetivos_Indicadores.NomeIndicador]")});
        this.lblIndicador.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblIndicador.ForeColor = System.Drawing.Color.SaddleBrown;
        this.lblIndicador.LocationFloat = new DevExpress.Utils.PointFloat(29.50014F, 10F);
        this.lblIndicador.Name = "lblIndicador";
        this.lblIndicador.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblIndicador.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.lblIndicador.SizeF = new System.Drawing.SizeF(715.9998F, 19.99998F);
        this.lblIndicador.StylePriority.UseBorderColor = false;
        this.lblIndicador.StylePriority.UseBorders = false;
        this.lblIndicador.StylePriority.UseBorderWidth = false;
        this.lblIndicador.StylePriority.UseFont = false;
        this.lblIndicador.StylePriority.UseForeColor = false;
        this.lblIndicador.Text = "[Objetivos_Indicadores.NomeIndicador]";
        // 
        // imgIndicaIndicador
        // 
        this.imgIndicaIndicador.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("imgIndicaIndicador.ImageSource"));
        this.imgIndicaIndicador.LocationFloat = new DevExpress.Utils.PointFloat(2.500122F, 10.49999F);
        this.imgIndicaIndicador.Name = "imgIndicaIndicador";
        this.imgIndicaIndicador.SizeF = new System.Drawing.SizeF(26F, 19F);
        this.imgIndicaIndicador.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // drGraficoIndicador
        // 
        this.drGraficoIndicador.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.drGrafico});
        this.drGraficoIndicador.Level = 1;
        this.drGraficoIndicador.Name = "drGraficoIndicador";
        this.drGraficoIndicador.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.drGraficoIndicador_BeforePrint);
        // 
        // drGrafico
        // 
        this.drGrafico.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.seta_cima,
            this.xrChart1});
        this.drGrafico.HeightF = 222.4584F;
        this.drGrafico.KeepTogetherWithDetailReports = true;
        this.drGrafico.Name = "drGrafico";
        this.drGrafico.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.drGrafico_BeforePrint);
        // 
        // seta_cima
        // 
        this.seta_cima.FillColor = System.Drawing.Color.Black;
        this.seta_cima.ForeColor = System.Drawing.Color.Green;
        this.seta_cima.LocationFloat = new DevExpress.Utils.PointFloat(749.6251F, 0F);
        this.seta_cima.Name = "seta_cima";
        this.seta_cima.Shape = shapeArrow1;
        this.seta_cima.SizeF = new System.Drawing.SizeF(17.37494F, 14.66667F);
        this.seta_cima.StylePriority.UseForeColor = false;
        this.seta_cima.Visible = false;
        // 
        // xrChart1
        // 
        this.xrChart1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top;
        this.xrChart1.BorderColor = System.Drawing.SystemColors.ControlText;
        this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        xyDiagram1.AxisX.Label.Angle = 330;
        xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Verdana", 5F);
        xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
        xyDiagram1.AxisX.NumericScaleOptions.AutoGrid = false;
        xyDiagram1.AxisX.NumericScaleOptions.GridSpacing = 0.5D;
        xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
        xyDiagram1.AxisY.Label.Font = new System.Drawing.Font("Verdana", 5F);
        xyDiagram1.AxisY.Label.ResolveOverlappingOptions.AllowHide = false;
        xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
        xyDiagram1.PaneDistance = 9;
        this.xrChart1.Diagram = xyDiagram1;
        this.xrChart1.ImageType = DevExpress.XtraReports.UI.ChartImageType.Bitmap;
        this.xrChart1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
        this.xrChart1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
        this.xrChart1.Legend.Border.Color = System.Drawing.Color.Transparent;
        this.xrChart1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        this.xrChart1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
        this.xrChart1.Legend.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.Legend.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrChart1.Legend.HorizontalIndent = 50;
        this.xrChart1.Legend.Margins.Bottom = 1;
        this.xrChart1.Legend.Margins.Left = 1;
        this.xrChart1.Legend.Margins.Right = 1;
        this.xrChart1.Legend.Margins.Top = 1;
        this.xrChart1.Legend.Name = "Default Legend";
        this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
        this.xrChart1.Name = "xrChart1";
        series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        series1.Name = "Meta";
        series1.ShowInLegend = false;
        sideBySideBarSeriesView1.BarWidth = 0.4D;
        sideBySideBarSeriesView1.Color = System.Drawing.Color.WhiteSmoke;
        sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series1.View = sideBySideBarSeriesView1;
        series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        pointSeriesLabel1.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel1.Font = new System.Drawing.Font("Verdana", 5F);
        pointSeriesLabel1.LineLength = 1;
        pointSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        pointSeriesLabel1.ResolveOverlappingMode = DevExpress.XtraCharts.ResolveOverlappingMode.JustifyAllAroundPoint;
        pointSeriesLabel1.TextPattern = "{V:F2}";
        series2.Label = pointSeriesLabel1;
        series2.LegendTextPattern = "{V:G}";
        series2.Name = "Metas";
        splineSeriesView1.Color = System.Drawing.Color.Black;
        series2.View = splineSeriesView1;
        series3.Name = "Meta não Apurada";
        sideBySideBarSeriesView2.Color = System.Drawing.Color.Gainsboro;
        sideBySideBarSeriesView2.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series3.View = sideBySideBarSeriesView2;
        series4.Name = "Acima da Meta";
        sideBySideBarSeriesView3.Color = System.Drawing.Color.Blue;
        sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series4.View = sideBySideBarSeriesView3;
        series5.Name = "Próximo da Meta";
        sideBySideBarSeriesView4.Color = System.Drawing.Color.Yellow;
        sideBySideBarSeriesView4.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series5.View = sideBySideBarSeriesView4;
        series6.Name = "Atingiu a Meta";
        sideBySideBarSeriesView5.Color = System.Drawing.Color.Green;
        sideBySideBarSeriesView5.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series6.View = sideBySideBarSeriesView5;
        series7.Name = "Meta não atingida";
        sideBySideBarSeriesView6.Color = System.Drawing.Color.Red;
        sideBySideBarSeriesView6.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
        series7.View = sideBySideBarSeriesView6;
        this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4,
        series5,
        series6,
        series7};
        sideBySideBarSeriesLabel1.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
        this.xrChart1.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
        sideBySideBarSeriesView7.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Gradient;
        rectangleGradientFillOptions1.Color2 = System.Drawing.Color.DarkSalmon;
        rectangleGradientFillOptions1.GradientMode = DevExpress.XtraCharts.RectangleGradientMode.BottomToTop;
        sideBySideBarSeriesView7.FillStyle.Options = rectangleGradientFillOptions1;
        this.xrChart1.SeriesTemplate.View = sideBySideBarSeriesView7;
        this.xrChart1.SideBySideBarDistanceVariable = 3D;
        this.xrChart1.SizeF = new System.Drawing.SizeF(747.6251F, 222.4584F);
        this.xrChart1.StylePriority.UseBorders = false;
        this.xrChart1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.xrChart1_CustomDrawSeriesPoint);
        this.xrChart1.PivotChartingCustomizeLegend += new DevExpress.XtraCharts.CustomizeLegendEventHandler(this.xrChart1_CustomizeLegend);
        this.xrChart1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChart1_BeforePrint);
        // 
        // DetailReport3
        // 
        this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail9,
            this.GroupHeader8,
            this.GroupHeader9,
            this.GroupHeader12,
            this.GroupFooter3});
        this.DetailReport3.DataMember = "Objetivos.Objetivos_Indicadores.Indicadores_PlanoAcaoInd";
        this.DetailReport3.DataSource = this.dsRelGestaoEstrategia;
        this.DetailReport3.Level = 6;
        this.DetailReport3.Name = "DetailReport3";
        this.DetailReport3.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail9
        // 
        this.Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.Detail9.HeightF = 15F;
        this.Detail9.Name = "Detail9";
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 0F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable6.SizeF = new System.Drawing.SizeF(766.9998F, 15F);
        this.xrTable6.StylePriority.UseBorders = false;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell19,
            this.xrTableCell21});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 11.5D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DescricaoTarefa]")});
        this.xrTableCell16.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseFont = false;
        this.xrTableCell16.StylePriority.UsePadding = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 0.264436327255717D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell17.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeUsuario]")});
        this.xrTableCell17.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UsePadding = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell17.Weight = 0.190976381138558D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PercentualConcluido]")});
        this.xrTableCell19.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.StylePriority.UsePadding = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell19.TextFormatString = "{0:0%}";
        this.xrTableCell19.Weight = 0.0909315132354374D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.imgPlanoAcaoIndicador});
        this.xrTableCell21.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.StylePriority.UseFont = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell21.Weight = 0.0909701374321964D;
        // 
        // imgPlanoAcaoIndicador
        // 
        this.imgPlanoAcaoIndicador.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgPlanoAcaoIndicador.BorderWidth = 0F;
        this.imgPlanoAcaoIndicador.LocationFloat = new DevExpress.Utils.PointFloat(44.8931F, 1.5F);
        this.imgPlanoAcaoIndicador.Name = "imgPlanoAcaoIndicador";
        this.imgPlanoAcaoIndicador.SizeF = new System.Drawing.SizeF(18.69513F, 12F);
        this.imgPlanoAcaoIndicador.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
        this.imgPlanoAcaoIndicador.StylePriority.UseBorders = false;
        this.imgPlanoAcaoIndicador.StylePriority.UseBorderWidth = false;
        this.imgPlanoAcaoIndicador.StylePriority.UsePadding = false;
        this.imgPlanoAcaoIndicador.StylePriority.UseTextAlignment = false;
        this.imgPlanoAcaoIndicador.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.imgPlanoAcaoIndicador_BeforePrint);
        // 
        // GroupHeader8
        // 
        this.GroupHeader8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.GroupHeader8.Expanded = false;
        this.GroupHeader8.HeightF = 14.00002F;
        this.GroupHeader8.Name = "GroupHeader8";
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0.2499084F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable7.SizeF = new System.Drawing.SizeF(766.5002F, 14.00002F);
        this.xrTable7.StylePriority.UseBorders = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell22.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
        this.xrTableCell22.StylePriority.UseBackColor = false;
        this.xrTableCell22.StylePriority.UseBorders = false;
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.StylePriority.UsePadding = false;
        this.xrTableCell22.Text = "Tarefa";
        this.xrTableCell22.Weight = 1.25255996372911D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell23.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
        this.xrTableCell23.StylePriority.UseBackColor = false;
        this.xrTableCell23.StylePriority.UseBorders = false;
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.StylePriority.UsePadding = false;
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.Text = "Responsável";
        this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell23.Weight = 0.905312065235485D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell24.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.xrTableCell24.StylePriority.UseBackColor = false;
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.StylePriority.UseFont = false;
        this.xrTableCell24.StylePriority.UsePadding = false;
        this.xrTableCell24.StylePriority.UseTextAlignment = false;
        this.xrTableCell24.Text = "(%) Concluído";
        this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell24.Weight = 0.431054962960486D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell25.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrTableCell25.StylePriority.UseBackColor = false;
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.StylePriority.UseFont = false;
        this.xrTableCell25.StylePriority.UsePadding = false;
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.Text = "Desempenho";
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell25.Weight = 0.43025459375267D;
        // 
        // GroupHeader9
        // 
        this.GroupHeader9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8,
            this.imgStatusPlanoAcaoIndicadorTitulo});
        this.GroupHeader9.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NomeToDoList", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader9.HeightF = 36.81734F;
        this.GroupHeader9.Level = 1;
        this.GroupHeader9.Name = "GroupHeader9";
        // 
        // xrTable8
        // 
        this.xrTable8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0.5F, 8F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable8.SizeF = new System.Drawing.SizeF(744.5F, 20.79F);
        this.xrTable8.StylePriority.UseBorders = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeToDoList]")});
        this.xrTableCell14.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
        this.xrTableCell14.StylePriority.UseBackColor = false;
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.StylePriority.UsePadding = false;
        this.xrTableCell14.Weight = 1.04865581795362D;
        // 
        // imgStatusPlanoAcaoIndicadorTitulo
        // 
        this.imgStatusPlanoAcaoIndicadorTitulo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgStatusPlanoAcaoIndicadorTitulo.BorderWidth = 0F;
        this.imgStatusPlanoAcaoIndicadorTitulo.LocationFloat = new DevExpress.Utils.PointFloat(745F, 10F);
        this.imgStatusPlanoAcaoIndicadorTitulo.Name = "imgStatusPlanoAcaoIndicadorTitulo";
        this.imgStatusPlanoAcaoIndicadorTitulo.SizeF = new System.Drawing.SizeF(18F, 16F);
        this.imgStatusPlanoAcaoIndicadorTitulo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.imgStatusPlanoAcaoIndicadorTitulo.StylePriority.UseBorders = false;
        this.imgStatusPlanoAcaoIndicadorTitulo.StylePriority.UseBorderWidth = false;
        this.imgStatusPlanoAcaoIndicadorTitulo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.imgStatusPlanoAcaoIndicadorTitulo_BeforePrint);
        // 
        // GroupHeader12
        // 
        this.GroupHeader12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7});
        this.GroupHeader12.HeightF = 41.75F;
        this.GroupHeader12.KeepTogether = true;
        this.GroupHeader12.Level = 2;
        this.GroupHeader12.Name = "GroupHeader12";
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.ForeColor = System.Drawing.Color.Black;
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0.5F, 18.75F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(765.5F, 20F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseForeColor = false;
        this.xrLabel7.Text = "Planos de ação do Indicador";
        // 
        // GroupFooter3
        // 
        this.GroupFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
        this.GroupFooter3.HeightF = 13.00001F;
        this.GroupFooter3.Name = "GroupFooter3";
        // 
        // xrLabel8
        // 
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(99.99999F, 13.00001F);
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Perspectiva", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 34.387F;
        this.GroupHeader1.KeepTogether = true;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel3
        // 
        this.xrLabel3.BorderColor = System.Drawing.Color.Black;
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel3.BorderWidth = 2F;
        this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Perspectiva]")});
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.ForeColor = System.Drawing.Color.Black;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(2F, 6F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrLabel3.SizeF = new System.Drawing.SizeF(763.9999F, 23F);
        this.xrLabel3.StylePriority.UseBorderColor = false;
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseBorderWidth = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseForeColor = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.HeightF = 13F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrPageInfo1.ForeColor = System.Drawing.Color.DarkGray;
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(716.0914F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(49F, 13F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseForeColor = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader4,
            this.GroupHeader5,
            this.GroupHeader6,
            this.GroupFooter1});
        this.DetailReport.DataMember = "Objetivos.Objetivos_PlanoAcao";
        this.DetailReport.DataSource = this.dsRelGestaoEstrategia;
        this.DetailReport.Level = 2;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.Detail1.HeightF = 16F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable3
        // 
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001589457F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(766.9999F, 16F);
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell6});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 11.5D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DescricaoTarefa]")});
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 0.993826974311405D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeUsuario]")});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UsePadding = false;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.Weight = 0.717743900199685D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PercentualConcluido]")});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UsePadding = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell10.Weight = 0.341746802538146D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Status]")});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell6.Weight = 0.341891903789087D;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.GroupHeader4.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoObjetivo", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("CodigoToDoList", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader4.HeightF = 15F;
        this.GroupHeader4.Name = "GroupHeader4";
        this.GroupHeader4.RepeatEveryPage = true;
        // 
        // xrTable5
        // 
        this.xrTable5.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(766.9998F, 15F);
        this.xrTable5.StylePriority.UseBackColor = false;
        this.xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 11.5D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.Text = "Tarefa";
        this.xrTableCell8.Weight = 0.993826989179156D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UsePadding = false;
        this.xrTableCell11.Text = "Responsável";
        this.xrTableCell11.Weight = 0.717744025695121D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UsePadding = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "(%) Concluído";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell12.Weight = 0.341747233982146D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UsePadding = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "Desempenho";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell13.Weight = 0.3418913319819D;
        // 
        // GroupHeader5
        // 
        this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4});
        this.GroupHeader5.HeightF = 41.75F;
        this.GroupHeader5.Level = 2;
        this.GroupHeader5.Name = "GroupHeader5";
        this.GroupHeader5.RepeatEveryPage = true;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.ForeColor = System.Drawing.Color.Black;
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0.4998366F, 18.75F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(765.5F, 20F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseForeColor = false;
        this.xrLabel4.Text = "Planos de ação do Objetivo";
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4,
            this.imgPlanoAcao});
        this.GroupHeader6.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoToDoList", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader6.HeightF = 36.81734F;
        this.GroupHeader6.Level = 1;
        this.GroupHeader6.Name = "GroupHeader6";
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.4998366F, 7.999992F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(744.5001F, 20.79169F);
        this.xrTable4.StylePriority.UseBorders = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NomeToDoList]")});
        this.xrTableCell5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell5.StylePriority.UseBackColor = false;
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Text = "Nome do Projeto";
        this.xrTableCell5.Weight = 1.04865581795362D;
        // 
        // imgPlanoAcao
        // 
        this.imgPlanoAcao.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.imgPlanoAcao.BorderWidth = 0F;
        this.imgPlanoAcao.LocationFloat = new DevExpress.Utils.PointFloat(744.9999F, 10.00001F);
        this.imgPlanoAcao.Name = "imgPlanoAcao";
        this.imgPlanoAcao.SizeF = new System.Drawing.SizeF(18F, 16F);
        this.imgPlanoAcao.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.imgPlanoAcao.StylePriority.UseBorders = false;
        this.imgPlanoAcao.StylePriority.UseBorderWidth = false;
        this.imgPlanoAcao.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.imgPlanoAcao_BeforePrint);
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
        this.GroupFooter1.HeightF = 13.00001F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLabel5
        // 
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(4.499884F, 0F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(99.99999F, 13.00001F);
        // 
        // relGestaoEstrategia
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.dtlRptIniciativas,
            this.dtlRptIndicadores,
            this.GroupHeader1,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.DetailReport});
        this.DataMember = "Objetivos";
        this.DataSource = this.dsRelGestaoEstrategia;
        this.DrawGrid = false;
        this.Font = new System.Drawing.Font("Verdana", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.SnapGridSize = 5F;
        this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.SnapToGrid;
        this.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
        this.Version = "19.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelGestaoEstrategia)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        lblNomeMapa.Text = pNomeMapa.Value.ToString();
        logoUnidade.ImageUrl = pLogoUnidade.Value.ToString();
    }

    private void drGraficoIndicador_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        object codIndicador1 = drGraficoIndicador.Report.GetCurrentColumnValue("CodigoIndicador");
        object desempenhoIndicador1 = drGraficoIndicador.Report.GetCurrentColumnValue("desempenhoIndicador");

        string codIndicador = (codIndicador1 != null) ? codIndicador1.ToString() : "-1";
        int erroInt = 0;
        if (int.TryParse(codIndicador, out erroInt))
        {
            DataTable dtCampos = cDados.getDadosIndicador(int.Parse(codIndicador), "").Tables[0];

            int casasDecimais = 0;

            string unidadeMedida = "";
            if (cDados.DataTableOk(dtCampos))
            {
                casasDecimais = int.TryParse(dtCampos.Rows[0]["CasasDecimais"].ToString(), out erroInt) ? int.Parse(dtCampos.Rows[0]["CasasDecimais"].ToString()) : 0;
                unidadeMedida = dtCampos.Rows[0]["SiglaUnidadeMedida"].ToString();
            }

            int codUnidadeAux = int.Parse((pCodigoUnidade.Value == null) ? "-1" : pCodigoUnidade.Value.ToString());

            DataSet ds = cDados.getPeriodicidadeIndicador(codUnidadeAux, int.Parse(codIndicador), "");

            drGraficoIndicador.Visible = cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]);

        }

    }

    private void xrChart1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        /*Azul, Verde, Amarelo, Vermelho ou Branco
        Fazer filtros para as metas, primeiro fazer as séries
         
         * */
        XRChart chart = (XRChart)sender;

        object codIndicador1 = chart.Report.Report.GetCurrentColumnValue("CodigoIndicador");
        object desempenhoIndicador1 = chart.Report.Report.GetCurrentColumnValue("desempenhoIndicador");

        string codIndicador = (codIndicador1 != null) ? codIndicador1.ToString() : "-1";
        int erroInt = 0;
        if (int.TryParse(codIndicador, out erroInt))
        {
            DataTable dtCampos = cDados.getDadosIndicador(int.Parse(codIndicador), "").Tables[0];

            int casasDecimais = 0;

            string unidadeMedida = "";
            if (cDados.DataTableOk(dtCampos))
            {
                casasDecimais = int.TryParse(dtCampos.Rows[0]["CasasDecimais"].ToString(), out erroInt) ? int.Parse(dtCampos.Rows[0]["CasasDecimais"].ToString()) : 0;
                unidadeMedida = dtCampos.Rows[0]["SiglaUnidadeMedida"].ToString();
            }

            int codUnidadeAux = int.Parse((pCodigoUnidade.Value == null) ? "-1" : pCodigoUnidade.Value.ToString());

            DataSet ds = cDados.getPeriodicidadeIndicador(codUnidadeAux, int.Parse(codIndicador), "");
            chart.DataSource = ds.Tables[0];

            chart.Series[0].ArgumentDataMember = "Periodo";
            chart.Series[0].ValueDataMembers.AddRange(new string[] { "ValorRealizado" });
            chart.Series[0].LegendText = "Atingiu a meta";
            chart.Series[0].SeriesPointsSorting = SortingMode.None;
            chart.Series[0].SeriesPointsSortingKey = SeriesPointKey.Argument;

            chart.SeriesSorting = SortingMode.None;

            //chart.Series[1].ArgumentDataMember = "Periodo";
            //chart.Series[1].ValueDataMembers.AddRange(new string[] { "ValorRealizado" });
            //chart.Series[1].LegendText = "Próximo à meta";
            //chart.Series[1].SeriesPointsSorting = SortingMode.None;
            //chart.Series[1].SeriesPointsSortingKey = SeriesPointKey.Argument;

            //chart.Series[2].ArgumentDataMember = "Periodo";
            //chart.Series[2].ValueDataMembers.AddRange(new string[] { "ValorRealizado" });
            //chart.Series[2].LegendText = "Meta não atingida";
            //chart.Series[2].SeriesPointsSorting = SortingMode.None;
            //chart.Series[2].SeriesPointsSortingKey = SeriesPointKey.Argument;


            //chart.Series[3].ArgumentDataMember = "Periodo";
            //chart.Series[3].ValueDataMembers.AddRange(new string[] { "ValorRealizado" });
            //chart.Series[3].LegendText = "Acima da meta";
            //chart.Series[3].SeriesPointsSorting = SortingMode.None;
            //chart.Series[3].SeriesPointsSortingKey = SeriesPointKey.Argument;


            //chart.Series[4].ArgumentDataMember = "Periodo";
            //chart.Series[4].ValueDataMembers.AddRange(new string[] { "ValorRealizado" });
            //chart.Series[4].LegendText = "Meta não definida";
            //chart.Series[4].SeriesPointsSorting = SortingMode.None;
            //chart.Series[4].SeriesPointsSortingKey = SeriesPointKey.Argument;


            chart.Series[1].ArgumentDataMember = "Periodo";
            chart.Series[1].ValueDataMembers.AddRange(new string[] { "ValorPrevisto" });
            chart.Series[1].LegendText = "Meta" + "(" + unidadeMedida + ")";
            chart.Series[1].SeriesPointsSorting = SortingMode.None;
            chart.Series[1].SeriesPointsSortingKey = SeriesPointKey.Argument;

            ////Filtro de todas as situações das barras que aparecem no gráfico
            //DataFilter filtroAtingiuMeta = new DataFilter();
            //DataFilter filtroAcimaDaMeta = new DataFilter();
            //DataFilter filtroProximoMeta = new DataFilter();
            //DataFilter filtroMetaNaoAtingida = new DataFilter();
            //DataFilter filtroMetaNaoDefinida = new DataFilter();

            //// -- filtro meta não definida vai aparecer barra cinza no grafico (CONFIGURAÇÕES)
            //filtroMetaNaoDefinida.ColumnName = "CorIndicador";
            //filtroMetaNaoDefinida.DataType = System.Type.GetType("System.String");
            //filtroMetaNaoDefinida.Condition = DataFilterCondition.Equal;
            //filtroMetaNaoDefinida.Value = "Branco";

            ////--- filtro acima da meta (CONFIGURAÇÕES)
            //filtroAcimaDaMeta.ColumnName = "CorIndicador";
            //filtroAcimaDaMeta.DataType = System.Type.GetType("System.String");
            //filtroAcimaDaMeta.Condition = DataFilterCondition.Equal;
            //filtroAcimaDaMeta.Value = "Azul";

            //// --- filtro proximo meta (CONFIGURAÇÕES)
            //filtroProximoMeta.ColumnName = "CorIndicador";
            //filtroProximoMeta.DataType = System.Type.GetType("System.String");
            //filtroProximoMeta.Condition = DataFilterCondition.Equal;
            //filtroProximoMeta.Value = "Amarelo";

            //// --- filtro atingiu meta (CONFIGURAÇÕES)
            //filtroAtingiuMeta.ColumnName = "CorIndicador";
            //filtroAtingiuMeta.DataType = System.Type.GetType("System.String");
            //filtroAtingiuMeta.Condition = DataFilterCondition.Equal;
            //filtroAtingiuMeta.Value = "Verde";


            //// -- filtro meta não atingida (CONFIGURAÇÕES)
            //filtroMetaNaoAtingida.ColumnName = "CorIndicador";
            //filtroMetaNaoAtingida.DataType = System.Type.GetType("System.String");
            //filtroMetaNaoAtingida.Condition = DataFilterCondition.Equal;
            //filtroMetaNaoAtingida.Value = "Vermelho";

            //chart.Series[0].DataFilters.Clear();
            //chart.Series[1].DataFilters.Clear();
            //chart.Series[2].DataFilters.Clear();
            //chart.Series[3].DataFilters.Clear();
            //chart.Series[4].DataFilters.Clear();


            //chart.Series[0].DataFiltersConjunctionMode = ConjunctionTypes.And;
            //chart.Series[1].DataFiltersConjunctionMode = ConjunctionTypes.And;
            //chart.Series[2].DataFiltersConjunctionMode = ConjunctionTypes.And;
            //chart.Series[3].DataFiltersConjunctionMode = ConjunctionTypes.And;
            //chart.Series[4].DataFiltersConjunctionMode = ConjunctionTypes.And;


            //chart.Series[0].DataFilters.Add(filtroAtingiuMeta);
            //chart.Series[1].DataFilters.Add(filtroProximoMeta);
            //chart.Series[2].DataFilters.Add(filtroMetaNaoAtingida);
            //chart.Series[3].DataFilters.Add(filtroAcimaDaMeta);
            //chart.Series[4].DataFilters.Add(filtroMetaNaoDefinida);


            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel_1 = new DevExpress.XtraCharts.PointSeriesLabel();
            pointSeriesLabel_1.TextPattern = "{V:F{" + casasDecimais + "}";
            //chart.Series[1].Label = pointSeriesLabel_1;

            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel_0 = new DevExpress.XtraCharts.PointSeriesLabel();
            pointSeriesLabel_0.TextPattern = "{V:F{" + casasDecimais + "}";
            //chart.Series[0].Label = pointSeriesLabel_0;
            chart.Series[0].ShowInLegend = false;// (chart.Series[0].Points.Count > 0);
            chart.Series[1].ShowInLegend = (chart.Series[1].Points.Count > 0);
            //chart.Series[2].ShowInLegend = (chart.Series[2].Points.Count > 0);
            //chart.Series[3].ShowInLegend = (chart.Series[3].Points.Count > 0);
            //chart.Series[4].ShowInLegend = (chart.Series[4].Points.Count > 0);
            //chart.Series[5].ShowInLegend = (chart.Series[5].Points.Count > 0);
            DataRow[] drBranco = ((DataTable)chart.DataSource).Select("CorIndicador = 'Branco'");
            DataRow[] drAzul = ((DataTable)chart.DataSource).Select("CorIndicador = 'Azul'");
            DataRow[] drAmarelo = ((DataTable)chart.DataSource).Select("CorIndicador = 'Amarelo'");
            DataRow[] drVerde = ((DataTable)chart.DataSource).Select("CorIndicador = 'Verde'");
            DataRow[] drVermelho = ((DataTable)chart.DataSource).Select("CorIndicador = 'Vermelho'");

            chart.Series[2].ShowInLegend = false;
            chart.Series[3].ShowInLegend = false;
            chart.Series[4].ShowInLegend = false;
            chart.Series[5].ShowInLegend = false;
            chart.Series[6].ShowInLegend = false;

            if (drBranco.Length > 0)
            {
                chart.Series[2].ShowInLegend = true;
            }
            if (drAzul.Length > 0)
            {
                chart.Series[3].ShowInLegend = true;
            }
            if (drAmarelo.Length > 0)
            {
                chart.Series[4].ShowInLegend = true;
            }
            if (drVerde.Length > 0)
            {
                chart.Series[5].ShowInLegend = true;
            }
            if (drVermelho.Length > 0)
            {
                chart.Series[6].ShowInLegend = true;
            }


        }

        string cor = (desempenhoIndicador1 != null) ? desempenhoIndicador1.ToString() : "-1";
        if (cor == "Vermelho")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/Azul.gif";
        if (cor == "Branco")
            imgDesempenhoIndicador.ImageUrl = "~/imagens/Branco.gif";
    }



    private void imgCorObjetivoEstrategico_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPictureBox img = (XRPictureBox)sender;
        object corObjetivo1 = img.Report.GetCurrentColumnValue("CorObjetivo");

        img.Visible = imgIndicaObjetivo.Visible = (corObjetivo1 != null);

        string cor = (corObjetivo1 != null) ? corObjetivo1.ToString() : "Branco";
        cor = cor.Trim();
        if (cor == "Branco")
            img.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            img.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            img.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            img.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            img.ImageUrl = "~/imagens/azul.gif";
    }

    private void imgDesempenhoProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPictureBox img = (XRPictureBox)sender;
        object corDesempenho = img.Report.GetCurrentColumnValue("Desempenho");


        string cor = (corDesempenho != null) ? corDesempenho.ToString() : "Branco";
        cor = cor.Trim();
        if (cor == "Branco")
            img.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            img.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            img.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            img.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            img.ImageUrl = "~/imagens/azul.gif";
    }

    private void imgPlanoAcao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPictureBox img = (XRPictureBox)sender;
        object corDesempenho = img.Report.GetCurrentColumnValue("Desempenho");


        string cor = (corDesempenho != null) ? corDesempenho.ToString() : "Branco";
        cor = cor.Trim();
        if (cor == "Branco")
            img.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            img.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            img.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            img.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            img.ImageUrl = "~/imagens/azul.gif";
    }

    private void imgPlanoAcaoIndicador_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPictureBox img = (XRPictureBox)sender;
        object corDesempenho = img.Report.GetCurrentColumnValue("DesempenhoTarefa");


        string cor = (corDesempenho != null) ? corDesempenho.ToString() : "Branco";
        cor = cor.Trim();
        if (cor == "Branco")
            img.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            img.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            img.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            img.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            img.ImageUrl = "~/imagens/azul.gif";
    }

    private void drGrafico_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DetailBand dr = ((DetailBand)(sender));
        object pol = dr.Report.Report.GetCurrentColumnValue("Polaridade");
        if (pol == null)
        {
            seta_cima.Visible = false;
        }
        else if (pol.ToString().ToLower().Trim().Contains("pos"))
        {
            seta_cima.Visible = true;
            seta_cima.Angle = 0;
        }
        else
        {
            seta_cima.Visible = true;
            seta_cima.Angle = 180;
        }
    }

    private void imgStatusPlanoAcaoIndicadorTitulo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPictureBox img = (XRPictureBox)sender;
        object corDesempenho = img.Report.GetCurrentColumnValue("Desempenho");


        string cor = (corDesempenho != null) ? corDesempenho.ToString() : "Branco";
        cor = cor.Trim();
        if (cor == "Branco")
            img.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            img.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            img.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            img.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            img.ImageUrl = "~/imagens/azul.gif";
    }

    private void GroupHeader7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void imgDesempenhoIndicador_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRPictureBox img = (XRPictureBox)sender;
        object corDesempenho = img.Report.Report.GetCurrentColumnValue("desempenhoIndicador");

        string cor = (corDesempenho != null) ? corDesempenho.ToString() : "Branco";
        cor = cor.Trim();
        if (cor == "Branco")
            img.ImageUrl = "~/imagens/Branco.gif";
        if (cor == "Vermelho")
            img.ImageUrl = "~/imagens/vermelho.gif";
        if (cor == "Verde")
            img.ImageUrl = "~/imagens/verde.gif";
        if (cor == "Amarelo")
            img.ImageUrl = "~/imagens/amarelo.gif";
        if (cor == "Azul")
            img.ImageUrl = "~/imagens/azul.gif";
    }

    private void xrChart1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
    {
        //Azul, Verde, Amarelo, Vermelho ou Branco
        string cor = ((DataTable)xrChart1.DataSource).Select("Periodo = '" + e.SeriesPoint.Argument + "'")[0]["CorIndicador"].ToString();
        if (cor.Trim() == "Azul")
        {
            e.SeriesDrawOptions.Color = Color.Blue;
        }
        else if (cor.Trim() == "Verde")
        {
            e.SeriesDrawOptions.Color = Color.Green;
        }
        else if (cor.Trim() == "Amarelo")
        {
            e.SeriesDrawOptions.Color = Color.Yellow;
        }
        else if (cor.Trim() == "Vermelho")
        {
            e.SeriesDrawOptions.Color = Color.Red;
        }
        else if (cor.Trim() == "Branco")
        {
            e.SeriesDrawOptions.Color = Color.WhiteSmoke;
        }

    }

    private void xrChart1_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
    {

    }
}
