using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for subReportProduto_Tai001
/// </summary>
public class subReportProduto_Tai001 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private dsRelPropostaIniciativa dsRelPropostaIniciativa1;
    private dados cDados = CdadosUtil.GetCdados(null);
    public DevExpress.XtraReports.Parameters.Parameter codigoAtividade;
    private XRLabel xrLabel1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public subReportProduto_Tai001(int codigoProjeto)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
        InitData(codigoProjeto);
    }

    public void InitData(int codigoProjeto)
    {
        string comandoSQL = string.Format(@"
        BEGIN
            declare @CodigoProjeto int 
            set @CodigoProjeto = {2}
            
            -- TABELA detalhesProposta
            SELECT * FROM {0}.{1}.TermoAbertura02 WHERE CodigoProjeto = @CodigoProjeto

            -- TABELA equipeApoio
            SELECT SequenciaRegistro, NomeColaborador
            FROM tai02_EquipeApoio 
            WHERE CodigoProjeto = @CodigoProjeto
            ORDER BY NomeColaborador

            -- TABELA ResultadosIniciativa
            SELECT * FROM {0}.{1}.tai02_ResultadosIniciativa 
            WHERE CodigoProjeto = @CodigoProjeto 
            ORDER BY SetencaResultado

            -- TABELA ResultadosEsperados
            SELECT * FROM {0}.{1}.tai02_ResultadosEsperados WHERE CodigoProjeto = @CodigoProjeto

            -- TABELA ObjetivosEstrategicos
            SELECT to1.SequenciaObjetivo, to1.CodigoObjetoEstrategia, to1.DescricaoObjetoEstrategia, to1.CodigoIndicador, to1.Meta, i.NomeIndicador
            FROM {0}.{1}.tai02_ObjetivosEstrategicos to1
            LEFT JOIN {0}.{1}.Indicador i on i.CodigoIndicador = to1.CodigoIndicador
            WHERE to1.CodigoProjeto = @CodigoProjeto
            ORDER BY DescricaoObjetoEstrategia

            -- TABELA LinhaDeAcao
            SELECT l.SequenciaRegistro, l.CodigoAcaoSugerida, l.DescricaoAcao, l.SequenciaObjetivo, o.CodigoObjetoEstrategia,o.DescricaoObjetoEstrategia
            FROM {0}.{1}.tai02_LinhaDeAcao l INNER JOIN
                 {0}.{1}.tai02_ObjetivosEstrategicos o ON o.CodigoProjeto = l.CodigoProjeto AND
                                                       o.SequenciaObjetivo = l.SequenciaObjetivo
            WHERE l.CodigoProjeto = @CodigoProjeto
            ORDER BY o.DescricaoObjetoEstrategia, l.DescricaoAcao

            -- TABELA AcoesIniciativa
             SELECT t1.CodigoProjeto,
                    t1.CodigoAcao AS Codigo, 
                    t1.CodigoAcaoSuperior AS CodigoPai
			        ,CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior 
                          THEN CONVERT(VARCHAR, t1.NumeroAcao)
						  ELSE CONVERT(VARCHAR, tSup.NumeroAcao) + '.' + CONVERT(VARCHAR, t1.NumeroAcao) END AS Numero
			        ,t1.NomeAcao AS Descricao
                    ,t1.Inicio
                    ,t1.Termino
			        ,CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior 
                          THEN ' - '
			              ELSE t1.IndicaEventoInstitucional END AS Institucional
                    ,t1.CodigoUsuarioResponsavel
			        ,t1.NomeUsuarioResponsavel AS Responsavel
                    ,t1.FonteRecurso
                    ,t1.LocalEvento
                    ,t1.DetalhesEvento
	           FROM {0}.{1}.tai02_AcoesIniciativa t1 INNER JOIN
			        {0}.{1}.tai02_AcoesIniciativa tSup ON tSup.CodigoAcao = t1.CodigoAcaoSuperior
                  WHERE t1.CodigoProjeto = @CodigoProjeto
                  ORDER BY tSup.NumeroAcao, CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior THEN CONVERT(VARCHAR, t1.NumeroAcao)
						            ELSE CONVERT(VARCHAR, tSup.NumeroAcao) + '.' + CONVERT(VARCHAR, t1.NumeroAcao) END
               

              -- TABELA ProdutosAcoesIniciativa
              SELECT SequenciaRegistro AS CodigoMeta, 
                     CodigoAcao AS Codigo, 
                     DescricaoProduto AS Meta
                FROM {0}.{1}.tai02_ProdutosAcoesIniciativa
               WHERE CodigoProjeto = @CodigoProjeto
            ORDER BY DescricaoProduto
                

              -- TABELA ParceirosIniciativa
              SELECT p.CodigoAcao AS Codigo, 
                     ai.CodigoAcaoSuperior AS CodigoObjetoPai, 
                     p.SequenciaRegistro AS CodigoParceria,
			         p.CodigoParceiro, 
                     p.NomeParceiro AS Area, 
                     p.ProdutoSolicitado AS Elemento 
               FROM {0}.{1}.tai02_ParceirosIniciativa p 
         INNER JOIN {0}.{1}.tai02_AcoesIniciativa ai ON ai.CodigoAcao = p.CodigoAcao
              WHERE p.CodigoProjeto = @CodigoProjeto
           ORDER BY p.NomeParceiro, p.ProdutoSolicitado

             -- TABELA MarcosIniciativa
             SELECT m.CodigoProjeto,
                    m.CodigoAcao AS Codigo, 
                    ai.CodigoAcaoSuperior AS CodigoObjetoPai, 
                    m.SequenciaRegistro AS CodigoMarco,
			        m.NomeMarco AS Marco,
                    m.DataLimitePrevista AS Data,
                    '<b>'+CONVERT(varchar, m.DataLimitePrevista,103)+'</b> - ' + m.NomeMarco As MarcoComData 
              FROM {0}.{1}.tai02_MarcosAcoesIniciativa m INNER JOIN
			       {0}.{1}.tai02_AcoesIniciativa ai ON ai.CodigoAcao = m.CodigoAcao
                 WHERE m.CodigoProjeto = @CodigoProjeto                   
                 ORDER BY m.DataLimitePrevista, m.NomeMarco
       
             -- TABELA CronogramaOrc
            select resultado.*,
            (select sum(isnull(valorTotal,0)) 
            from {0}.{1}.CronogramaOrcamentario soma 
           where soma.CodigoProjeto = resultado.CodigoProjeto ) totalProjeto,                                   
                       (select sum(isnull(valorTotal,0)) 
           from   {0}.{1}.CronogramaOrcamentario soma inner join
            {0}.{1}.tai02_AcoesIniciativa ai on (ai.CodigoProjeto = soma.CodigoProjeto and ai.CodigoAcao = soma.CodigoAcao)
      where soma.CodigoProjeto = resultado.CodigoProjeto 
      and ai.CodigoAcaoSuperior = resultado.codigoPai) totalAcao,
(select sum(isnull(valorTotal,0)) 
            from {0}.{1}.CronogramaOrcamentario soma inner join
            {0}.{1}.tai02_AcoesIniciativa ai on (ai.CodigoProjeto = soma.CodigoProjeto and ai.CodigoAcao = soma.CodigoAcao)
            where soma.CodigoProjeto = resultado.CodigoProjeto 
            and soma.CodigoAcao = resultado.Codigo) totalAtividade
from (  
            SELECT t1.CodigoProjeto,
            t1.CodigoAcao AS Codigo, 
            t1.CodigoAcaoSuperior AS CodigoPai,
            CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior 
            THEN CONVERT(VARCHAR, t1.NumeroAcao)
            ELSE CONVERT(VARCHAR, tSup.NumeroAcao) + '.' + CONVERT(VARCHAR, t1.NumeroAcao) END AS Numero,
            t1.NomeAcao AS DescricaoAcao,
            t1.FonteRecurso,
            co.SeqPlanoContas, 
            co.Quantidade, 
            co.ValorUnitario,
            co.ValorTotal, 
            co.MemoriaCalculo, 
            co.Plan01, 
            co.Plan02, 
            co.Plan03, 
            co.Plan04, 
            co.Plan05,
            co.Plan06, 
            co.Plan07, 
            co.Plan08, 
            co.Plan09, 
            co.Plan10, 
            co.Plan11, 
            co.Plan12, 
            opc.CONTA_DES, 
            opc.CONTA_COD,
            tSup.NumeroAcao acaoSup,
            t1.NumeroAcao 
            FROM  {0}.{1}.tai02_AcoesIniciativa t1 LEFT JOIN
                             {0}.{1}.tai02_AcoesIniciativa tSup ON tSup.CodigoAcao = t1.CodigoAcaoSuperior left JOIN
                             {0}.{1}.CronogramaOrcamentario co ON (co.CodigoAcao = t1.CodigoAcao) left join
                             {0}.{1}.orc_planoContas opc ON opc.SeqPlanoContas = co.SeqPlanoContas
            WHERE t1.CodigoProjeto = @CodigoProjeto
) resultado        
ORDER BY resultado.Numero, 
         CASE WHEN resultado.Codigo = resultado.CodigoPai THEN CONVERT(VARCHAR, resultado.Numero)
              ELSE CONVERT(VARCHAR, resultado.acaoSup) + '.' + CONVERT(VARCHAR, resultado.NumeroAcao) 
         END


        END", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);
        DataSet ds = new DataSet();
        ds = cDados.getDataSet(comandoSQL);
        dsRelPropostaIniciativa1.Load(ds.Tables[7].CreateDataReader(), LoadOption.OverwriteChanges, "ProdutosAcoesIniciativa");
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
        //string resourceFileName = "subReportProduto_Tai001.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dsRelPropostaIniciativa1 = new dsRelPropostaIniciativa();
        this.codigoAtividade = new DevExpress.XtraReports.Parameters.Parameter();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelPropostaIniciativa1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 33.42F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.Detail.StylePriority.UsePadding = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel1.CanShrink = true;
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ProdutosAcoesIniciativa.Meta")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1199F, 33.42F);
        this.xrLabel1.StylePriority.UseBorderColor = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // dsRelPropostaIniciativa1
        // 
        this.dsRelPropostaIniciativa1.DataSetName = "dsRelPropostaIniciativa";
        this.dsRelPropostaIniciativa1.EnforceConstraints = false;
        this.dsRelPropostaIniciativa1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // codigoAtividade
        // 
        this.codigoAtividade.Name = "codigoAtividade";
        this.codigoAtividade.Type = typeof(short);
        this.codigoAtividade.Visible = false;
        // 
        // subReportProduto_Tai001
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.DataMember = "ProdutosAcoesIniciativa";
        this.DataSource = this.dsRelPropostaIniciativa1;
        this.Dpi = 254F;
        this.FilterString = "[Codigo] = ?codigoAtividade";
        this.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        this.PageHeight = 2794;
        this.PageWidth = 1199;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.codigoAtividade});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "12.1";
        ((System.ComponentModel.ISupportInitialize)(this.dsRelPropostaIniciativa1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
