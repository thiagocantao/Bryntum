using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for relImprimeCronograma
/// </summary>
public class relImprimeCronograma : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell celCodigoTarefa;
    private XRTableCell celNomeTarefa;
    private XRTableCell celConcluido;
    private XRTableCell celDuracao;
    private XRTableCell celInicio;
    private XRTableCell celTermino;
    private PageHeaderBand PageHeader;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell celCabecalhoInicio;
    private XRTableCell celCabecalhoTermino;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRTableCell xrTableCell13;
    private XRTableCell celAlocacao;
    private XRLabel lblNomeProjeto;
    private XRLabel lblUnidade;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLabel lblDataImpressao;
    private XRLine xrLine1;
    private XRLabel lblFiltro;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private dsRelImprimeCronograma1 dsRelImprimeCronograma11;
    private string imprimeDadosLinhaBase = "N";
    dados cDados = CdadosUtil.GetCdados(null);

    public relImprimeCronograma(int codigoProjeto, int versaoLinhaBase, int codigoRecurso, int indicaMarco, int indicaTarefaResumo, int indicaTarefaNaoConcluida, int indicaRecursoSelecionado, string nomeProjeto, string pathArquivo, string dataImpressao, string nomeRecurso, string imprimeDadosLinhaBaseCronograma, int? filtro_percentualConcluido, DateTime? filtroData)
    {

        InitializeComponent();
        imprimeDadosLinhaBase = imprimeDadosLinhaBaseCronograma;
        cDados = CdadosUtil.GetCdados(null);
        InitData(codigoProjeto, codigoRecurso, indicaMarco, indicaTarefaResumo, indicaTarefaNaoConcluida,
              indicaRecursoSelecionado, versaoLinhaBase, imprimeDadosLinhaBase, filtro_percentualConcluido, filtroData);
        DataSet ds = cDados.getProposta("", codigoProjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblUnidade.Text = "Unidade: " + ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }

        lblNomeProjeto.Text = "Projeto: " + nomeProjeto + (versaoLinhaBase == -1 ? "" : " (LB Versão " + versaoLinhaBase + ")");

        if (dsRelImprimeCronograma11.TabImprimeCronograma.Count == 0)
        {
            xrTable2.Visible = false;
        }

        if (string.IsNullOrEmpty(pathArquivo))
            xrPictureBox1.Image = cDados.ObtemLogoEntidade();
        else
            xrPictureBox1.ImageUrl = pathArquivo;
        if (lblDataImpressao.Text == "")
        {
            DataSet dsdata = cDados.getDataSet("select getdate()");
            dataImpressao = dsdata.Tables[0].Rows[0][0].ToString();
            DateTime dt = DateTime.Parse(dataImpressao);
            dataImpressao = "Impresso em: " + dt.ToString("dd/MM/yyyy hh:mm:ss");
            lblDataImpressao.Text = dataImpressao;
        }
        string textoLblFiltro = "Filtro(s) aplicado(s):";//length: 22
        if (indicaMarco == 1)
        {
            textoLblFiltro += " Marco";
        }
        if (indicaTarefaResumo == 1)
        {
            if (textoLblFiltro.Length > 22)
                textoLblFiltro += ", Tarefas Atrasadas";
            else
                textoLblFiltro += "Tarefas Atrasadas";
        }
        //int? filtro_percentualConcluido, DateTime? filtroData
        if (filtro_percentualConcluido.HasValue)
        {
            if (textoLblFiltro.Length > 22)
                textoLblFiltro += ", Percentual concluído igual a: " + filtro_percentualConcluido.Value.ToString() + "%";
            else
                textoLblFiltro += "Percentual concluído igual a: " + filtro_percentualConcluido.Value.ToString() + "%";
        }
        if (filtroData.HasValue)
        {
            if (textoLblFiltro.Length > 22)
                textoLblFiltro += ", Data de término é menor ou igual a: " + string.Format("{0:dd/MM/yyyy}", (DateTime)filtroData.Value);
            else
                textoLblFiltro += "Data de término é menor ou igual a: " + string.Format("{0:dd/MM/yyyy}", (DateTime)filtroData.Value);
        }
        if (indicaTarefaNaoConcluida == 1)
        {
            if (textoLblFiltro.Length > 22)
                textoLblFiltro += ", Tarefas não concluídas";
            else
                textoLblFiltro += "Tarefas não concluídas";
        }
        if (indicaRecursoSelecionado == 0)
        {
            if (textoLblFiltro.Length > 22)
                textoLblFiltro += " e Todos os recursos";
            else
                textoLblFiltro += "Todos os recursos";
        }
        if (indicaRecursoSelecionado == 1)
        {
            if (textoLblFiltro.Length > 22)
                textoLblFiltro += " e Recurso específico:" + nomeRecurso;
            else
                textoLblFiltro += "Recurso específico:" + nomeRecurso;
        }
        lblFiltro.Text = textoLblFiltro + ".";



    }

    private void InitData(int codigoProjeto, int codigoRecurso, int indicaMarco, int indicaTarefaResumo, int indicaTarefaNaoConcluida,
                     int indicaRecursoSelecionado, int versaoLB, string imprimeDadosLinhaBase, int? filtro_percentualConcluido, DateTime? filtroData)
    {

        cDados = CdadosUtil.GetCdados(null);
        string comandoSql = string.Empty;
        Image logo = cDados.ObtemLogoEntidade();

        string innerJoinLB = "";

        string replanejamentoTasques = "N";

        DataSet dsParam = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "TASQUES_ReplanejarCronograma");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            replanejamentoTasques = dsParam.Tables[0].Rows[0]["TASQUES_ReplanejarCronograma"].ToString();

        bool usaVersaoReplanejamento = false;

        if (replanejamentoTasques == "S")
        {
            string comandoSqlLB = string.Format(
                 @"BEGIN	               
                    DECLARE @CodigoProjeto INT

	                SET @CodigoProjeto = {2}
	                    
	                SELECT ModeloLinhaBase, NumeroVersao
		             FROM {0}.{1}.f_crono_GetVersoesLBProjeto(@CodigoProjeto) vlb
		            WHERE VersaoLinhaBase = {3}
                END
               ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, versaoLB);

            DataSet dsLB = cDados.getDataSet(comandoSqlLB);

            if (cDados.DataSetOk(dsLB) && cDados.DataTableOk(dsLB.Tables[0]))
            {
                usaVersaoReplanejamento = dsLB.Tables[0].Rows[0]["ModeloLinhaBase"].ToString().Trim() == "2";

                if (!usaVersaoReplanejamento && dsLB.Tables[0].Rows[0]["NumeroVersao"].ToString() == "-1")
                    versaoLB = -1;
            }
        }

        if (usaVersaoReplanejamento)
        {
            comandoSql = string.Format(
                 @"BEGIN 
                DECLARE @in_CodigoProjeto as int
                DECLARE @in_VersaoLinhaBase as smallint
                DECLARE @in_CodigoRecurso as int
                DECLARE @in_SoAtrasadas as char(1)
                DECLARE @in_SoMarcos as  char(1)
                DECLARE @in_PercentualConcluido  int
                DECLARE @in_DataFiltro  datetime

                SET @in_CodigoProjeto = {2}
                SET @in_VersaoLinhaBase = {4}
                SET @in_CodigoRecurso =  {3}
                SET @in_SoAtrasadas = '{5}'
                SET @in_SoMarcos = '{6}'
                SET @in_PercentualConcluido = {11}
                SET @in_DataFiltro = {12}
  
            SELECT   f.[EstruturaHierarquica]
			    , f.[CodigoTarefa]
			    , f.[NomeTarefa]
			    , f.[Concluido] / 100 as Concluido
			    , f.[Trabalho]
			    , f.[Custo]
                , f.[Inicio] 
                , f.[Termino]
                , f.[InicioLB] AS [InicioLB]
                , f.[TerminoLB] AS [TerminoLB] 
                , f.[IndicaMarco]
                , f.[IndicaTarefaSumario]
                , f.[Nivel]
                , f.[IndicaCritica]
                , f.[TerminoReal]
                , f.[DuracaoLB]
                , f.[Duracao]
                , f.[InicioPrevisto]
                , f.[TerminoPrevisto]
                , f.[Predecessoras]
                , f.[CodigoRealTarefa]
                , f.[CodigoProjeto]
                , f.[SequenciaTarefaCronograma]
                , f.[TarefaSuperior]
                , f.[Desvio]
                , f.[PercentualPrevisto]
                , f.[CodigoCronogramaProjeto]
	            , f.[StringAlocacaoRecursoTarefa]
	            , f.[UnidadeDuracao]   
                , f.[DuracaoLBSemConversao]	
           FROM {0}.{1}.[f_GetCronogramaGanttProjeto] (@in_CodigoProjeto, @in_VersaoLinhaBase, @in_CodigoRecurso, @in_SoAtrasadas, @in_SoMarcos, @in_PercentualConcluido
               ,@in_DataFiltro) as f
           ORDER BY [SequenciaTarefaCronograma]                                                                    
            END ", /*{0}*/ cDados.getDbName(),
            /*{1}*/cDados.getDbOwner(),
            /*{2}*/codigoProjeto,
            /*{3}*/codigoRecurso,
            /*{4}*/versaoLB,
            /*{5}*/ (indicaTarefaResumo == 1) ? "S" : "N",
            /*{6}*/ (indicaMarco == 1) ? "S" : "N",
            /*{7}*/ innerJoinLB != "" ? "f.InicioLB" : "f.InicioPrevisto",
            /*{8}*/ innerJoinLB != "" ? "f.TerminoLB" : "f.TerminoPrevisto",
            /*{9}*/ innerJoinLB != "" ? "f.DuracaoLBSemConversao" : "f.DuracaoLBSemConversao",
            /*{10}*/imprimeDadosLinhaBase,
            /*{11}*/filtro_percentualConcluido.HasValue ? filtro_percentualConcluido.Value.ToString() : "NULL",
            /*{12}*/filtroData.HasValue ? string.Format(@"CONVERT(DateTime,'{0}',103)", filtroData.Value.ToShortDateString()) : "NULL");

        }
        else
        {
            if (versaoLB != -1)
            {
                innerJoinLB = string.Format(@" INNER JOIN
                               {0}.{1}.CronogramaProjeto AS cp ON (cp.CodigoProjeto = f.CodigoProjeto) LEFT JOIN
                               {0}.{1}.TarefaCronogramaProjetoLinhaBase AS lb ON (lb.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto
                                                                                AND lb.CodigoTarefa = f.CodigoNumeroTarefa
                                                                                AND lb.VersaoLinhaBase = {2})", cDados.getDbName(), cDados.getDbOwner(), versaoLB + 1);
            }

            comandoSql = string.Format(
                 @"BEGIN 
                DECLARE @in_CodigoProjeto as int
                DECLARE @in_VersaoLinhaBase as smallint
                DECLARE @in_CodigoRecurso as int
                DECLARE @in_SoAtrasadas as char(1)
                DECLARE @in_SoMarcos as  char(1)
                DECLARE @in_PercentualConcluido  int
                DECLARE @in_DataFiltro  datetime

                SET @in_CodigoProjeto = {2}
                SET @in_VersaoLinhaBase = {4}
                SET @in_CodigoRecurso =  {3}
                SET @in_SoAtrasadas = '{5}'
                SET @in_SoMarcos = '{6}'
                SET @in_PercentualConcluido = {11}
                SET @in_DataFiltro = {12} 
  
                 SELECT   f.[EstruturaHierarquica]
			                 , f.[CodigoTarefa]
			                 , f.[NomeTarefa]
			                 , f.[Concluido] / 100 as Concluido
			                 , f.[Trabalho]
			                 , f.[Custo]
                             , CASE WHEN '{10}' = 'N' THEN f.[Inicio]  ELSE {7}  end as [Inicio] 
                             , CASE WHEN '{10}' = 'N' THEN f.[Termino] ELSE {8}  end as [Termino]
                             , f.[InicioLB] AS [InicioLB]
                             , f.[TerminoLB] AS [TerminoLB] 
                             , f.[IndicaMarco]
                             , f.[IndicaTarefaSumario]
                             , f.[Nivel]
                             , f.[IndicaCritica]
                             , f.[TerminoReal]
                             , f.[DuracaoLB]
                             , f.[Duracao]
                             , f.[InicioPrevisto]
                             , f.[TerminoPrevisto]
                             , f.[Predecessoras]
                             , f.[CodigoRealTarefa]
                             , f.[CodigoProjeto]
                             , f.[SequenciaTarefaCronograma]
                             , f.[TarefaSuperior]
                             , f.[Desvio]
                             , f.[PercentualPrevisto]
                             , f.[CodigoCronogramaProjeto]
	                         , f.[StringAlocacaoRecursoTarefa]
	                         , f.[UnidadeDuracao]  
                             , f.[DuracaoLBSemConversao]                     
           FROM {0}.{1}.[f_GetCronogramaGanttProjeto] (@in_CodigoProjeto, @in_VersaoLinhaBase, @in_CodigoRecurso, @in_SoAtrasadas, @in_SoMarcos, @in_PercentualConcluido
               ,@in_DataFiltro) as f
           ORDER BY [SequenciaTarefaCronograma]                                                                    
            END ", /*{0}*/ cDados.getDbName(),
            /*{1}*/cDados.getDbOwner(),
            /*{2}*/codigoProjeto,
            /*{3}*/codigoRecurso,
            /*{4}*/versaoLB,
            /*{5}*/ (indicaTarefaResumo == 1) ? "S" : "N",
            /*{6}*/(indicaMarco == 1) ? "S" : "N",
            /*{7}*/ innerJoinLB != "" ? "f.InicioLB" : "f.InicioPrevisto",
            /*{8}*/ innerJoinLB != "" ? "f.TerminoLB" : "f.TerminoPrevisto",
            /*{9}*/ innerJoinLB != "" ? "f.DuracaoLBSemConversao" : "f.DuracaoLBSemConversao",
            /*{10}*/ imprimeDadosLinhaBase,
            /*{11}*/filtro_percentualConcluido.HasValue ? filtro_percentualConcluido.Value.ToString() : "NULL",
            /*{12}*/filtroData.HasValue ? string.Format(@"CONVERT(DateTime,'{0}',103)", filtroData.Value.ToShortDateString()) : "NULL");
        }

        DataSet ds = cDados.getDataSet(comandoSql);
        dsRelImprimeCronograma11.Load(ds.Tables[0].CreateDataReader(), LoadOption.OverwriteChanges, "TabImprimeCronograma");

    }

    //private Image ObtemLogoEntidade()
    //{
    //    int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
    //    DataSet dsLogoUnidade = cDados.getLogoEntidade(codEntidade, "");
    //    if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
    //    {
    //        byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
    //        System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
    //        Image logo = Image.FromStream(stream);
    //        return logo;
    //    }
    //    else
    //        return null;
    //}

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
        string resourceFileName = "relImprimeCronograma.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celCodigoTarefa = new DevExpress.XtraReports.UI.XRTableCell();
        this.celNomeTarefa = new DevExpress.XtraReports.UI.XRTableCell();
        this.celConcluido = new DevExpress.XtraReports.UI.XRTableCell();
        this.celDuracao = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.celInicio = new DevExpress.XtraReports.UI.XRTableCell();
        this.celTermino = new DevExpress.XtraReports.UI.XRTableCell();
        this.celAlocacao = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblFiltro = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.lblDataImpressao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoInicio = new DevExpress.XtraReports.UI.XRTableCell();
        this.celCabecalhoTermino = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblNomeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.lblUnidade = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.dsRelImprimeCronograma11 = new dsRelImprimeCronograma1();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelImprimeCronograma11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrLabel1,
            this.xrLabel2});
        this.Detail.HeightF = 25.00001F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodigoProjeto", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("SequenciaTarefaCronograma", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1030F, 25F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celCodigoTarefa,
            this.celNomeTarefa,
            this.celConcluido,
            this.celDuracao,
            this.celInicio,
            this.celTermino,
            this.celAlocacao});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1D;
        // 
        // celCodigoTarefa
        // 
        this.celCodigoTarefa.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celCodigoTarefa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.SequenciaTarefaCronograma")});
        this.celCodigoTarefa.Font = new System.Drawing.Font("Verdana", 8F);
        this.celCodigoTarefa.Name = "celCodigoTarefa";
        this.celCodigoTarefa.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.celCodigoTarefa.StylePriority.UseBorders = false;
        this.celCodigoTarefa.StylePriority.UseFont = false;
        this.celCodigoTarefa.StylePriority.UsePadding = false;
        this.celCodigoTarefa.StylePriority.UseTextAlignment = false;
        this.celCodigoTarefa.Text = "celCodigoTarefa";
        this.celCodigoTarefa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.celCodigoTarefa.Weight = 0.13822114103085234D;
        this.celCodigoTarefa.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell1_BeforePrint);
        // 
        // celNomeTarefa
        // 
        this.celNomeTarefa.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celNomeTarefa.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.NomeTarefa")});
        this.celNomeTarefa.Font = new System.Drawing.Font("Verdana", 8F);
        this.celNomeTarefa.Name = "celNomeTarefa";
        this.celNomeTarefa.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.celNomeTarefa.StylePriority.UseBorders = false;
        this.celNomeTarefa.StylePriority.UseFont = false;
        this.celNomeTarefa.StylePriority.UsePadding = false;
        this.celNomeTarefa.StylePriority.UseTextAlignment = false;
        this.celNomeTarefa.Text = "celNomeTarefa";
        this.celNomeTarefa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celNomeTarefa.Weight = 0.97078098078978969D;
        // 
        // celConcluido
        // 
        this.celConcluido.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celConcluido.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.Concluido", "{0:0%}")});
        this.celConcluido.Font = new System.Drawing.Font("Verdana", 8F);
        this.celConcluido.Name = "celConcluido";
        this.celConcluido.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.celConcluido.StylePriority.UseBorders = false;
        this.celConcluido.StylePriority.UseFont = false;
        this.celConcluido.StylePriority.UsePadding = false;
        this.celConcluido.StylePriority.UseTextAlignment = false;
        this.celConcluido.Text = "celConcluido";
        this.celConcluido.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.celConcluido.Weight = 0.21190858017370434D;
        // 
        // celDuracao
        // 
        this.celDuracao.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celDuracao.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel3});
        this.celDuracao.Font = new System.Drawing.Font("Verdana", 8F);
        this.celDuracao.Name = "celDuracao";
        this.celDuracao.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.celDuracao.StylePriority.UseBorders = false;
        this.celDuracao.StylePriority.UseFont = false;
        this.celDuracao.StylePriority.UsePadding = false;
        this.celDuracao.StylePriority.UseTextAlignment = false;
        this.celDuracao.Text = "celDuracao";
        this.celDuracao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.celDuracao.Weight = 0.23076923039507014D;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.UnidadeDuracao")});
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(59.35999F, 0F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(18.63995F, 22.99999F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UsePadding = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "xrLabel4";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.DuracaoLBSemConversao")});
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(3.242493E-05F, 0F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(59.35995F, 22.99999F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UsePadding = false;
        // 
        // celInicio
        // 
        this.celInicio.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celInicio.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.InicioPrevisto", "{0:ddd dd/MM/yyyy hh:mm}")});
        this.celInicio.Font = new System.Drawing.Font("Verdana", 8F);
        this.celInicio.Name = "celInicio";
        this.celInicio.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.celInicio.StylePriority.UseBorders = false;
        this.celInicio.StylePriority.UseFont = false;
        this.celInicio.StylePriority.UsePadding = false;
        this.celInicio.StylePriority.UseTextAlignment = false;
        this.celInicio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celInicio.Weight = 0.32307692670198335D;
        // 
        // celTermino
        // 
        this.celTermino.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celTermino.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.TerminoPrevisto", "{0:ddd dd/MM/yyyy hh:mm}")});
        this.celTermino.Font = new System.Drawing.Font("Verdana", 8F);
        this.celTermino.Name = "celTermino";
        this.celTermino.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.celTermino.StylePriority.UseBorders = false;
        this.celTermino.StylePriority.UseFont = false;
        this.celTermino.StylePriority.UsePadding = false;
        this.celTermino.StylePriority.UseTextAlignment = false;
        this.celTermino.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celTermino.Weight = 0.32307691856383747D;
        // 
        // celAlocacao
        // 
        this.celAlocacao.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celAlocacao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.StringAlocacaoRecursoTarefa")});
        this.celAlocacao.Font = new System.Drawing.Font("Verdana", 8F);
        this.celAlocacao.Name = "celAlocacao";
        this.celAlocacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.celAlocacao.StylePriority.UseBorders = false;
        this.celAlocacao.StylePriority.UseFont = false;
        this.celAlocacao.StylePriority.UsePadding = false;
        this.celAlocacao.StylePriority.UseTextAlignment = false;
        this.celAlocacao.Text = "celAlocacao";
        this.celAlocacao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.celAlocacao.Weight = 0.7733200684986089D;
        // 
        // xrLabel1
        // 
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.IndicaTarefaSumario")});
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1.666689F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(8.333312F, 2F);
        this.xrLabel1.Text = "xrLabel1";
        this.xrLabel1.Visible = false;
        this.xrLabel1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell1_BeforePrint);
        // 
        // xrLabel2
        // 
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TabImprimeCronograma.Nivel")});
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1.666689F, 7.625008F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(5.555555F, 7.374999F);
        this.xrLabel2.Text = "1";
        this.xrLabel2.Visible = false;
        this.xrLabel2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel2_BeforePrint);
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 30F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 30F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblFiltro,
            this.xrLine1,
            this.lblDataImpressao,
            this.xrTable2,
            this.xrPictureBox1,
            this.lblNomeProjeto,
            this.lblUnidade});
        this.PageHeader.HeightF = 157F;
        this.PageHeader.Name = "PageHeader";
        // 
        // lblFiltro
        // 
        this.lblFiltro.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblFiltro.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 85.99998F);
        this.lblFiltro.Name = "lblFiltro";
        this.lblFiltro.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblFiltro.SizeF = new System.Drawing.SizeF(631.7499F, 23F);
        this.lblFiltro.StylePriority.UseFont = false;
        // 
        // xrLine1
        // 
        this.xrLine1.BorderColor = System.Drawing.Color.Transparent;
        this.xrLine1.ForeColor = System.Drawing.Color.Silver;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(9.999996F, 121F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1030F, 2F);
        this.xrLine1.StylePriority.UseBorderColor = false;
        this.xrLine1.StylePriority.UseForeColor = false;
        // 
        // lblDataImpressao
        // 
        this.lblDataImpressao.Font = new System.Drawing.Font("Verdana", 9F);
        this.lblDataImpressao.ForeColor = System.Drawing.Color.DarkGray;
        this.lblDataImpressao.LocationFloat = new DevExpress.Utils.PointFloat(797.5833F, 105.2917F);
        this.lblDataImpressao.Name = "lblDataImpressao";
        this.lblDataImpressao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblDataImpressao.SizeF = new System.Drawing.SizeF(240.4167F, 11.70832F);
        this.lblDataImpressao.StylePriority.UseFont = false;
        this.lblDataImpressao.StylePriority.UseForeColor = false;
        this.lblDataImpressao.StylePriority.UseTextAlignment = false;
        this.lblDataImpressao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrTable2
        // 
        this.xrTable2.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 131F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1030F, 25F);
        this.xrTable2.StylePriority.UseBackColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.celCabecalhoInicio,
            this.celCabecalhoTermino,
            this.xrTableCell13});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "#";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell7.Weight = 0.13398480124554052D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "Nome Tarefa";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell8.Weight = 0.941027639864418D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.xrTableCell9.StylePriority.UsePadding = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "Concluído";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell9.Weight = 0.20541369907705306D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
        this.xrTableCell10.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
        this.xrTableCell10.StylePriority.UsePadding = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Duração";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell10.Weight = 0.22369638505437381D;
        this.xrTableCell10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell10_BeforePrint);
        // 
        // celCabecalhoInicio
        // 
        this.celCabecalhoInicio.Name = "celCabecalhoInicio";
        this.celCabecalhoInicio.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.celCabecalhoInicio.StylePriority.UsePadding = false;
        this.celCabecalhoInicio.StylePriority.UseTextAlignment = false;
        this.celCabecalhoInicio.Text = "Início";
        this.celCabecalhoInicio.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoInicio.Weight = 0.31317492094607186D;
        this.celCabecalhoInicio.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell11_BeforePrint);
        // 
        // celCabecalhoTermino
        // 
        this.celCabecalhoTermino.Name = "celCabecalhoTermino";
        this.celCabecalhoTermino.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.celCabecalhoTermino.StylePriority.UsePadding = false;
        this.celCabecalhoTermino.StylePriority.UseTextAlignment = false;
        this.celCabecalhoTermino.Text = "Término";
        this.celCabecalhoTermino.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.celCabecalhoTermino.Weight = 0.31317492689041643D;
        this.celCabecalhoTermino.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell12_BeforePrint);
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
        this.xrTableCell13.StylePriority.UsePadding = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "Alocação";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell13.Weight = 0.74961846183432135D;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(384.4541F, 79.99998F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // lblNomeProjeto
        // 
        this.lblNomeProjeto.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.lblNomeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(410.4167F, 0F);
        this.lblNomeProjeto.Name = "lblNomeProjeto";
        this.lblNomeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeProjeto.SizeF = new System.Drawing.SizeF(534.1667F, 23F);
        this.lblNomeProjeto.StylePriority.UseFont = false;
        this.lblNomeProjeto.Text = "lblNomeProjeto";
        // 
        // lblUnidade
        // 
        this.lblUnidade.Font = new System.Drawing.Font("Verdana", 13F);
        this.lblUnidade.LocationFloat = new DevExpress.Utils.PointFloat(410.4167F, 34.12498F);
        this.lblUnidade.Name = "lblUnidade";
        this.lblUnidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblUnidade.SizeF = new System.Drawing.SizeF(534.1667F, 23F);
        this.lblUnidade.StylePriority.UseFont = false;
        this.lblUnidade.Text = "lblUnidade";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
        this.PageFooter.HeightF = 28.00001F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(951.8889F, 3.000005F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(86.11115F, 23F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // dsRelImprimeCronograma11
        // 
        this.dsRelImprimeCronograma11.DataSetName = "dsRelImprimeCronograma1";
        this.dsRelImprimeCronograma11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // relImprimeCronograma
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
        this.DataMember = "TabImprimeCronograma";
        this.DataSource = this.dsRelImprimeCronograma11;
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(30, 30, 30, 30);
        this.PageHeight = 850;
        this.PageWidth = 1100;
        this.SnapGridSize = 1F;
        this.Version = "17.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelImprimeCronograma11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrLabel1.Text == "1")
        {
            celCodigoTarefa.Font = new Font("Verdana", 8f, FontStyle.Bold);
            celNomeTarefa.Font = new Font("Verdana", 8f, FontStyle.Bold);
            celConcluido.Font = new Font("Verdana", 8f, FontStyle.Bold);
            celDuracao.Font = new Font("Verdana", 8f, FontStyle.Bold);
            celInicio.Font = new Font("Verdana", 8f, FontStyle.Bold);
            celTermino.Font = new Font("Verdana", 8f, FontStyle.Bold);
            celAlocacao.Font = new Font("Verdana", 8f, FontStyle.Bold);
        }
        else
        {
            celCodigoTarefa.Font = new Font("Verdana", 8f, FontStyle.Regular);
            celNomeTarefa.Font = new Font("Verdana", 8f, FontStyle.Regular);
            celConcluido.Font = new Font("Verdana", 8f, FontStyle.Regular);
            celDuracao.Font = new Font("Verdana", 8f, FontStyle.Regular);
            celInicio.Font = new Font("Verdana", 8f, FontStyle.Regular);
            celTermino.Font = new Font("Verdana", 8f, FontStyle.Regular);
            celAlocacao.Font = new Font("Verdana", 8f, FontStyle.Regular);
        }
    }

    private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int recuo = (3 + int.Parse(xrLabel2.Text == "" ? "1" : xrLabel2.Text)) * 6;
        celNomeTarefa.Padding = new DevExpress.XtraPrinting.PaddingInfo(recuo, 3, 0, 0, 100F);
    }

    private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell10.Text = imprimeDadosLinhaBase == "S" ? "Duração LB" : "Duração";
    }

    private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        celCabecalhoInicio.Text = imprimeDadosLinhaBase == "S" ? "Início LB" : "Início";
    }

    private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        celCabecalhoTermino.Text = imprimeDadosLinhaBase == "S" ? "Término LB" : "Término";
    }
}
