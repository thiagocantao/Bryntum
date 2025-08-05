using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for relPropostaIniciativa
/// </summary>
public class relPropostaIniciativa : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private dados cDados = CdadosUtil.GetCdados(null);
    private dsRelPropostaIniciativa dsRelPropostaIniciativa1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel10;
    private XRLabel lblFonteRecurso;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel4;
    private XRLabel lblTipoDeProjeto;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel18;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRLabel xrLabel19;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell6;
    private XRLabel xrLabel24;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell12;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private DetailReportBand DetailReport3;
    private DetailBand Detail4;
    private DetailReportBand DetailReport4;
    private DetailBand Detail5;
    private PageHeaderBand PageHeader;
    private XRLabel lblAreaAtuacao;
    private XRLabel xrLabel14;
    private XRLabel xrLabel26;
    private XRTable xrTable10;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell26;
    private XRLabel lblAreaAtuacao1;
    private XRLabel xrLabel31;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel xrLabel28;
    private XRTable xrTable8;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell16;
    private XRTable xrTable9;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell30;
    private XRTable xrTable7;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRPictureBox picLogoEntidade;
    private DetailReportBand DetailReport5;
    private DetailBand Detail6;
    private ReportFooterBand ReportFooter;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel xrLabel20;
    private XRLabel lblIndicaClassificacaoProjeto;
    private XRLabel lblCabecalho;
    private int codigoProjetoGlobal = -1;
    private DetailReportBand DetailReport6;
    private DetailBand Detail7;
    private XRTable xrTable11;
    private XRTableRow xrTableRow11;
    private XRTableCell celNumeroAcao;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell celFonteRecurso;
    private XRLabel xrLabel3;
    private DetailReportBand DetailReport7;
    private DetailBand Detail8;
    private XRLabel xrLabel13;
    private XRLabel lblTituloMetaProduto;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private DetailReportBand DetailReport8;
    private DetailBand Detail9;
    private XRTable xrTable13;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell49;
    private XRTable xrTable14;
    private XRTableRow xrTableRow15;
    private XRTableCell celTituloAreaParceria;
    private XRTableCell celTituloElementoParceria;
    private DetailReportBand DetailReport9;
    private DetailBand Detail10;
    private XRTable xrTable15;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRTable xrTable16;
    private XRTableRow xrTableRow17;
    private XRTableCell celTitMarcoProjeto;
    private XRTableCell xrTableCell45;
    private CalculatedField titElementoDeParceriaCalc;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell33;
    private XRTableCell celTituloAtividadeAcao;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private ReportFooterBand ReportFooter1;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private CalculatedField titAreaDeParceriaCalc;
    private CalculatedField titMetaProduto;
    private CalculatedField indicaProdutoMeta;
    private CalculatedField indicaAcaoAtividade;
    private CalculatedField titMarcoProjeto;
    private CalculatedField titData;
    private XRLine xrLine2;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell34;
    private CalculatedField titLocalEvento;
    private CalculatedField titDetalhesEvento;
    private CalculatedField indicaInstitucional;
    private DetailReportBand DetailReport11;
    private DetailBand Detail12;
    private XRTable xrTable12;
    private GroupHeaderBand GroupHeader1;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private CalculatedField calculatedField1;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell78;
    private XRTableCell xrTableCell79;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private XRTable xrTable17;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell95;
    private XRTableCell celFonteRecurso2;
    private XRTableCell celTotal;
    private XRLabel xrLabel9;
    private CalculatedField calculatedField2;
    private CalculatedField fonteRecursoCalculado1;
    private ReportFooterBand ReportFooter2;
    private XRLabel xrLabel25;
    private XRLabel xrLabel21;
    private XRLabel xrLabel35;
    private XRLabel xrLabel34;
    private XRLabel xrLabel33;
    private XRLabel xrLabel32;
    private XRLabel xrLabel27;
    private XRLine xrLine3;
    private CalculatedField calculatedField3;
    private CalculatedField calculatedField4;
    private CalculatedField calculatedField5;
    private CalculatedField calculatedField6;
    private CalculatedField calculatedField7;
    private CalculatedField calculatedField8;
    private CalculatedField calculatedField9;
    private CalculatedField calculatedField10;
    private CalculatedField calculatedField11;
    private CalculatedField calculatedField12;
    private CalculatedField calculatedField13;
    private CalculatedField calculatedField14;
    private CalculatedField calculatedField15;
    private CalculatedField calculatedField16;
    private CalculatedField calculatedField17;
    private CalculatedField calculatedField18;
    private CalculatedField calculatedField19;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell84;
    private XRTableCell xrTableCell86;
    private XRTableCell xrTableCell88;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell83;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell87;
    private XRTableCell xrTableCell89;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell91;
    private XRTableCell xrTableCell92;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell94;
    private XRTableCell xrTableCell96;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTableCell xrTableCell99;
    private XRTableCell xrTableCell100;
    private XRTableCell xrTableCell101;
    private XRTableCell xrTableCell102;
    private CalculatedField calculatedField20;
    private CalculatedField calculatedField21;
    private CalculatedField calculatedField22;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private decimal totalUN = 0;
    private CalculatedField calculatedField23;
    private GroupHeaderBand GroupHeader2;
    private GroupHeaderBand GroupHeader3;
    private GroupHeaderBand GroupHeader4;
    private GroupHeaderBand GroupHeader5;
    private GroupHeaderBand GroupHeader6;
    private GroupHeaderBand GroupHeader8;
    private GroupHeaderBand GroupHeader9;
    private GroupHeaderBand GroupHeader10;
    private GroupHeaderBand GroupHeader7;
    private GroupHeaderBand GroupHeader11;
    private decimal totalFundecoop = 0;

    public relPropostaIniciativa(int codigoProjeto)
    {
        InitializeComponent();
        codigoProjetoGlobal = codigoProjeto;
        DefineLogoEntidade();
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
        dsRelPropostaIniciativa1.Load(ds.CreateDataReader(), LoadOption.OverwriteChanges, "detalhesProposta", "equipeApoio", "ResultadosIniciativa", "ResultadosEsperados", "ObjetivosEstrategicos", "LinhaDeAcao", "AcoesIniciativa", "ProdutosAcoesIniciativa", "ParceirosIniciativa", "MarcosIniciativa", "CronogramaOrc");
    }


    private void DefineLogoEntidade()
    {
        dados cDados = CdadosUtil.GetCdados(null);
        Int32 codigoEntidade = (Int32)cDados.getInfoSistema("CodigoEntidade");
        DataSet dsTemp = cDados.getLogoEntidade(codigoEntidade, "");
        Byte[] binaryImage = (Byte[])dsTemp.Tables[0].Rows[0]["LogoUnidadeNegocio"];
        MemoryStream ms = new MemoryStream(binaryImage);
        picLogoEntidade.Image = Bitmap.FromStream(ms);
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
        //string resourceFileName = "relPropostaIniciativa.resx";
        DevExpress.XtraReports.UI.XRLine xrLine1;
        DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.lblAreaAtuacao = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblFonteRecurso = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTipoDeProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.dsRelPropostaIniciativa1 = new dsRelPropostaIniciativa();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblIndicaClassificacaoProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.lblAreaAtuacao1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblCabecalho = new DevExpress.XtraReports.UI.XRLabel();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.DetailReport6 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celTituloAtividadeAcao = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celNumeroAcao = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celFonteRecurso = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport7 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail8 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader8 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblTituloMetaProduto = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport8 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail9 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader9 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celTituloAreaParceria = new DevExpress.XtraReports.UI.XRTableCell();
        this.celTituloElementoParceria = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport9 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail10 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupHeader10 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
        this.celTitMarcoProjeto = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader7 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.titElementoDeParceriaCalc = new DevExpress.XtraReports.UI.CalculatedField();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.titAreaDeParceriaCalc = new DevExpress.XtraReports.UI.CalculatedField();
        this.titMetaProduto = new DevExpress.XtraReports.UI.CalculatedField();
        this.indicaProdutoMeta = new DevExpress.XtraReports.UI.CalculatedField();
        this.indicaAcaoAtividade = new DevExpress.XtraReports.UI.CalculatedField();
        this.titMarcoProjeto = new DevExpress.XtraReports.UI.CalculatedField();
        this.titData = new DevExpress.XtraReports.UI.CalculatedField();
        this.titLocalEvento = new DevExpress.XtraReports.UI.CalculatedField();
        this.titDetalhesEvento = new DevExpress.XtraReports.UI.CalculatedField();
        this.indicaInstitucional = new DevExpress.XtraReports.UI.CalculatedField();
        this.DetailReport11 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail12 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celFonteRecurso2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.celTotal = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter2 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupHeader11 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField2 = new DevExpress.XtraReports.UI.CalculatedField();
        this.fonteRecursoCalculado1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField3 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField4 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField5 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField6 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField7 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField8 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField9 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField10 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField11 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField12 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField13 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField14 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField15 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField16 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField17 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField18 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField19 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField20 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField21 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField22 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField23 = new DevExpress.XtraReports.UI.CalculatedField();
        xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelPropostaIniciativa1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // xrLine1
        // 
        xrLine1.Dpi = 254F;
        xrLine1.LineWidth = 3;
        xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 174.21F);
        xrLine1.Name = "xrLine1";
        xrLine1.SizeF = new System.Drawing.SizeF(2593F, 20.87503F);
        // 
        // xrPageInfo2
        // 
        xrPageInfo2.BorderColor = System.Drawing.Color.DarkOliveGreen;
        xrPageInfo2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        xrPageInfo2.Dpi = 254F;
        xrPageInfo2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic);
        xrPageInfo2.Format = "Emitido em {0:dd/MM/yyyy - HH:mm}";
        xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(2063.833F, 0F);
        xrPageInfo2.Name = "xrPageInfo2";
        xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        xrPageInfo2.SizeF = new System.Drawing.SizeF(529.1667F, 58.42F);
        xrPageInfo2.StylePriority.UseBorderColor = false;
        xrPageInfo2.StylePriority.UseBorders = false;
        xrPageInfo2.StylePriority.UseFont = false;
        xrPageInfo2.StylePriority.UseTextAlignment = false;
        xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAreaAtuacao,
            this.xrLabel14,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.lblFonteRecurso,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.lblTipoDeProjeto,
            this.xrLabel2,
            this.xrLabel1});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 909.3471F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblAreaAtuacao
        // 
        this.lblAreaAtuacao.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblAreaAtuacao.CanShrink = true;
        this.lblAreaAtuacao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.NomeUnidadeNegocio")});
        this.lblAreaAtuacao.Dpi = 254F;
        this.lblAreaAtuacao.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblAreaAtuacao.LocationFloat = new DevExpress.Utils.PointFloat(1129.417F, 323.446F);
        this.lblAreaAtuacao.Name = "lblAreaAtuacao";
        this.lblAreaAtuacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblAreaAtuacao.SizeF = new System.Drawing.SizeF(1459.583F, 58.41992F);
        this.lblAreaAtuacao.StylePriority.UseBorders = false;
        this.lblAreaAtuacao.StylePriority.UseFont = false;
        this.lblAreaAtuacao.Text = "lblAreaAtuacao";
        this.lblAreaAtuacao.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblAreaAtuacao_BeforePrint);
        // 
        // xrLabel14
        // 
        this.xrLabel14.CanShrink = true;
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(1132.417F, 265.0258F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(444.1875F, 58.42004F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "Área Responsável:";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel18
        // 
        this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel18.CanShrink = true;
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.Beneficiarios")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 687.0465F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(2592F, 188.0701F);
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.Text = "xrLabel18";
        // 
        // xrLabel17
        // 
        this.xrLabel17.CanShrink = true;
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 628.6266F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(304.2708F, 58.42001F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "Beneficiários:";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel16
        // 
        this.xrLabel16.CanShrink = true;
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 381.9115F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(304.2708F, 58.42001F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "Público alvo:";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel15.CanShrink = true;
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.PublicoAlvo")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 440.5609F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(2592F, 188.0658F);
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "xrLabel15";
        // 
        // xrLabel12
        // 
        this.xrLabel12.CanShrink = true;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 265.0258F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(1111.25F, 58.42001F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Coordenador do Projeto:";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel11.CanShrink = true;
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.NomeGerenteIniciativa")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 323.4459F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1111.25F, 58.42001F);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "[NomeGerenteIniciativa]";
        // 
        // xrLabel10
        // 
        this.xrLabel10.CanShrink = true;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(1132.417F, 132.1858F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(870.4789F, 58.42F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "Fonte de Recurso:";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // lblFonteRecurso
        // 
        this.lblFonteRecurso.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblFonteRecurso.CanShrink = true;
        this.lblFonteRecurso.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.FonteRecurso")});
        this.lblFonteRecurso.Dpi = 254F;
        this.lblFonteRecurso.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblFonteRecurso.LocationFloat = new DevExpress.Utils.PointFloat(1132.417F, 190.6058F);
        this.lblFonteRecurso.Name = "lblFonteRecurso";
        this.lblFonteRecurso.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblFonteRecurso.SizeF = new System.Drawing.SizeF(870.4789F, 58.42001F);
        this.lblFonteRecurso.StylePriority.UseBorders = false;
        this.lblFonteRecurso.StylePriority.UseFont = false;
        this.lblFonteRecurso.Text = "lblFonteRecurso";
        this.lblFonteRecurso.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblFonteRecurso_BeforePrint);
        // 
        // xrLabel8
        // 
        this.xrLabel8.CanShrink = true;
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 132.1858F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(1111.25F, 58.41998F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        this.xrLabel8.Text = "Tipo de Projeto:";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.CanShrink = true;
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(2290.385F, 132.1858F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(301.6145F, 58.41998F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.StylePriority.UseTextAlignment = false;
        this.xrLabel7.Text = "Data Término:";
        this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel6
        // 
        this.xrLabel6.CanShrink = true;
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(2021.261F, 132.1858F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Data Início:";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel5.CanShrink = true;
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.DataTermino", "{0:dd/MM/yyyy}")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(2287.385F, 190.6057F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(301.6145F, 58.42F);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel4.CanShrink = true;
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.DataInicio", "{0:dd/MM/yyyy}")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(2021.261F, 190.6057F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // lblTipoDeProjeto
        // 
        this.lblTipoDeProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTipoDeProjeto.CanShrink = true;
        this.lblTipoDeProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.IndicaTipoProjeto")});
        this.lblTipoDeProjeto.Dpi = 254F;
        this.lblTipoDeProjeto.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTipoDeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 190.6058F);
        this.lblTipoDeProjeto.Name = "lblTipoDeProjeto";
        this.lblTipoDeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTipoDeProjeto.SizeF = new System.Drawing.SizeF(1111.25F, 58.42003F);
        this.lblTipoDeProjeto.StylePriority.UseBorders = false;
        this.lblTipoDeProjeto.StylePriority.UseFont = false;
        this.lblTipoDeProjeto.Text = "lblTipoDeProjeto";
        this.lblTipoDeProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblTipoDeProjeto_BeforePrint);
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(2592F, 58.42F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Nome do Projeto";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.NomeIniciativa")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58.41996F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(2592F, 58.42F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 76F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 76F;
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
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.ReportFooter,
            this.GroupHeader2});
        this.DetailReport.DataMember = "equipeApoio";
        this.DetailReport.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 38.5F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Dpi = 254F;
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable1.KeepTogether = true;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(2592F, 38.5F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UseTextAlignment = false;
        this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.CanShrink = true;
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "equipeApoio.NomeColaborador")});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UsePadding = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.Weight = 0.3847214875395587D;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel20,
            this.lblIndicaClassificacaoProjeto});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 443.0779F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrLabel23
        // 
        this.xrLabel23.CanShrink = true;
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 140.4857F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(1076.854F, 58.42001F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.Text = "Justificativa:";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel22
        // 
        this.xrLabel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel22.CanShrink = true;
        this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.Justificativa")});
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 199.135F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(2591.562F, 188.07F);
        this.xrLabel22.StylePriority.UseBorders = false;
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.Text = "xrLabel22";
        // 
        // xrLabel20
        // 
        this.xrLabel20.CanShrink = true;
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(1076.854F, 58.41998F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "Projeto novo ou continuidade:";
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // lblIndicaClassificacaoProjeto
        // 
        this.lblIndicaClassificacaoProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblIndicaClassificacaoProjeto.CanShrink = true;
        this.lblIndicaClassificacaoProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.IndicaClassificacaoProjeto")});
        this.lblIndicaClassificacaoProjeto.Dpi = 254F;
        this.lblIndicaClassificacaoProjeto.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblIndicaClassificacaoProjeto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58.41988F);
        this.lblIndicaClassificacaoProjeto.Name = "lblIndicaClassificacaoProjeto";
        this.lblIndicaClassificacaoProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblIndicaClassificacaoProjeto.SizeF = new System.Drawing.SizeF(2591.001F, 58.41992F);
        this.lblIndicaClassificacaoProjeto.StylePriority.UseBorders = false;
        this.lblIndicaClassificacaoProjeto.StylePriority.UseFont = false;
        this.lblIndicaClassificacaoProjeto.Text = "lblIndicaClassificacaoProjeto";
        this.lblIndicaClassificacaoProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblIndicaClassificacaoProjeto_BeforePrint);
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19,
            this.xrTable10});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader2.HeightF = 111.125F;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrLabel19
        // 
        this.xrLabel19.CanShrink = true;
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(346.6042F, 58.42001F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "Equipe de apoio:";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTable10
        // 
        this.xrTable10.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable10.Dpi = 254F;
        this.xrTable10.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable10.KeepTogether = true;
        this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60.27087F);
        this.xrTable10.Name = "xrTable10";
        this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable10.SizeF = new System.Drawing.SizeF(2592F, 49.08339F);
        this.xrTable10.StylePriority.UseBackColor = false;
        this.xrTable10.StylePriority.UseBorders = false;
        this.xrTable10.StylePriority.UseFont = false;
        this.xrTable10.StylePriority.UseTextAlignment = false;
        this.xrTable10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 0.5679012345679012D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.CanShrink = true;
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell26.StylePriority.UsePadding = false;
        this.xrTableCell26.Text = "Nome do Colaborador";
        this.xrTableCell26.Weight = 0.38457306071518438D;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader3});
        this.DetailReport1.DataMember = "ObjetivosEstrategicos";
        this.DetailReport1.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 1;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 50.27083F;
        this.Detail2.Name = "Detail2";
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable2.KeepTogether = true;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(2592F, 50.27083F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell6});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5679012345679012D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.CanShrink = true;
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ObjetivosEstrategicos.DescricaoObjetoEstrategia")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 0.831591963478864D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.CanShrink = true;
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ObjetivosEstrategicos.NomeIndicador")});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UsePadding = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 0.44109226007789037D;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24,
            this.xrTable3});
        this.GroupHeader3.Dpi = 254F;
        this.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader3.HeightF = 169.2083F;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // xrLabel24
        // 
        this.xrLabel24.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(2592F, 58.42F);
        this.xrLabel24.StylePriority.UseBackColor = false;
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "Alinhamento Estratégico";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrTable3
        // 
        this.xrTable3.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable3.BorderColor = System.Drawing.Color.Black;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.KeepTogether = true;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 130.7083F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(2592F, 38.49999F);
        this.xrTable3.StylePriority.UseBackColor = false;
        this.xrTable3.StylePriority.UseBorderColor = false;
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        this.xrTable3.StylePriority.UseTextAlignment = false;
        this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.5679012345679012D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.CanShrink = true;
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.Text = "Objetivo";
        this.xrTableCell7.Weight = 0.831591963478864D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.CanShrink = true;
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.Text = "Indicador";
        this.xrTableCell8.Weight = 0.44109226007789037D;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader4});
        this.DetailReport2.DataMember = "LinhaDeAcao";
        this.DetailReport2.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Level = 2;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 38.5F;
        this.Detail3.Name = "Detail3";
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.Dpi = 254F;
        this.xrTable4.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable4.KeepTogether = true;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(2591F, 38.5F);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        this.xrTable4.StylePriority.UseTextAlignment = false;
        this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell12});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 0.5679012345679012D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.CanShrink = true;
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LinhaDeAcao.DescricaoObjetoEstrategia")});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UsePadding = false;
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 0.86658653567668531D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.CanShrink = true;
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LinhaDeAcao.DescricaoAcao")});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UsePadding = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 0.45863085562766254D;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.GroupHeader4.Dpi = 254F;
        this.GroupHeader4.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader4.HeightF = 109.5F;
        this.GroupHeader4.Name = "GroupHeader4";
        // 
        // xrTable5
        // 
        this.xrTable5.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable5.KeepTogether = true;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 71.00003F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(2591F, 38.49997F);
        this.xrTable5.StylePriority.UseBackColor = false;
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 0.5679012345679012D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.CanShrink = true;
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell13.StylePriority.UsePadding = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "Objetivo Estratégico";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell13.Weight = 0.86658665667122625D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.CanShrink = true;
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell14.StylePriority.UsePadding = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "Linha de ação";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell14.Weight = 0.45863073463312171D;
        // 
        // lblAreaAtuacao1
        // 
        this.lblAreaAtuacao1.CanShrink = true;
        this.lblAreaAtuacao1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.IndicaAreaAtuacao")});
        this.lblAreaAtuacao1.Dpi = 254F;
        this.lblAreaAtuacao1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblAreaAtuacao1.KeepTogether = true;
        this.lblAreaAtuacao1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46.42021F);
        this.lblAreaAtuacao1.Name = "lblAreaAtuacao1";
        this.lblAreaAtuacao1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblAreaAtuacao1.SizeF = new System.Drawing.SizeF(2592F, 58.42002F);
        this.lblAreaAtuacao1.StylePriority.UseFont = false;
        this.lblAreaAtuacao1.StylePriority.UseTextAlignment = false;
        this.lblAreaAtuacao1.Text = "lblAreaAtuacao1";
        this.lblAreaAtuacao1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lblAreaAtuacao1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblAreaAtuacao1_BeforePrint);
        // 
        // xrLabel26
        // 
        this.xrLabel26.CanShrink = true;
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(682.625F, 45.19083F);
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "Area de atuação";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReport3
        // 
        this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupHeader5});
        this.DetailReport3.DataMember = "ResultadosIniciativa";
        this.DetailReport3.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport3.Dpi = 254F;
        this.DetailReport3.Level = 4;
        this.DetailReport3.Name = "DetailReport3";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.Detail4.Dpi = 254F;
        this.Detail4.HeightF = 38.5F;
        this.Detail4.Name = "Detail4";
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.Dpi = 254F;
        this.xrTable6.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable6.KeepTogether = true;
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable6.SizeF = new System.Drawing.SizeF(2591.001F, 38.5F);
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseFont = false;
        this.xrTable6.StylePriority.UseTextAlignment = false;
        this.xrTable6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell30});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.5679012345679012D;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell30.CanShrink = true;
        this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadosIniciativa.NomeIndicador")});
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UsePadding = false;
        this.xrTableCell30.Text = "xrTableCell30";
        this.xrTableCell30.Weight = 5.8917525773195871D;
        // 
        // GroupHeader5
        // 
        this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel28,
            this.xrLabel31,
            this.xrLabel30,
            this.xrLabel29,
            this.xrTable8});
        this.GroupHeader5.Dpi = 254F;
        this.GroupHeader5.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader5.HeightF = 338.2556F;
        this.GroupHeader5.Name = "GroupHeader5";
        // 
        // xrLabel28
        // 
        this.xrLabel28.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(2591F, 58.42F);
        this.xrLabel28.StylePriority.UseBackColor = false;
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "Quadro Lógico";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel31
        // 
        this.xrLabel31.CanShrink = true;
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(0F, 225.8218F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(2591F, 51.83496F);
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "Indicadores de Desempenho";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel30
        // 
        this.xrLabel30.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel30.CanShrink = true;
        this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.ObjetivoGeral")});
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 112.8936F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(2591F, 102.1058F);
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.Text = "xrLabel30";
        // 
        // xrLabel29
        // 
        this.xrLabel29.CanShrink = true;
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58.41988F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(2591F, 47.83665F);
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "Objetivo Geral do Projeto";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTable8
        // 
        this.xrTable8.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.Dpi = 254F;
        this.xrTable8.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable8.KeepTogether = true;
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 299.7553F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable8.SizeF = new System.Drawing.SizeF(2591F, 38.49997F);
        this.xrTable8.StylePriority.UseBackColor = false;
        this.xrTable8.StylePriority.UseBorders = false;
        this.xrTable8.StylePriority.UseFont = false;
        this.xrTable8.StylePriority.UseTextAlignment = false;
        this.xrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.5679012345679012D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.CanShrink = true;
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell16.StylePriority.UsePadding = false;
        this.xrTableCell16.Text = "Indicador";
        this.xrTableCell16.Weight = 5.6853389064322579D;
        // 
        // DetailReport4
        // 
        this.DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.GroupHeader6});
        this.DetailReport4.DataMember = "ResultadosEsperados";
        this.DetailReport4.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport4.Dpi = 254F;
        this.DetailReport4.Level = 5;
        this.DetailReport4.Name = "DetailReport4";
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.Detail5.Dpi = 254F;
        this.Detail5.HeightF = 38.5F;
        this.Detail5.Name = "Detail5";
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable7.KeepTogether = true;
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable7.SizeF = new System.Drawing.SizeF(2591.001F, 38.5F);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseFont = false;
        this.xrTable7.StylePriority.UseTextAlignment = false;
        this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell19});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.5679012345679012D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.CanShrink = true;
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadosEsperados.DescricaoResultado")});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UsePadding = false;
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.Weight = 0.59150927790872776D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.CanShrink = true;
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadosEsperados.DescricaoIndicadorOperacional")});
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UsePadding = false;
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.Weight = 0.31304912664967688D;
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
        this.GroupHeader6.Dpi = 254F;
        this.GroupHeader6.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader6.HeightF = 104.4998F;
        this.GroupHeader6.Name = "GroupHeader6";
        // 
        // xrTable9
        // 
        this.xrTable9.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable9.Dpi = 254F;
        this.xrTable9.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable9.KeepTogether = true;
        this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 65.99985F);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable9.SizeF = new System.Drawing.SizeF(2590F, 38.49995F);
        this.xrTable9.StylePriority.UseBackColor = false;
        this.xrTable9.StylePriority.UseBorders = false;
        this.xrTable9.StylePriority.UseFont = false;
        this.xrTable9.StylePriority.UseTextAlignment = false;
        this.xrTable9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 0.5679012345679012D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.CanShrink = true;
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell23.StylePriority.UsePadding = false;
        this.xrTableCell23.Text = "Resultados Esperados";
        this.xrTableCell23.Weight = 0.59150907969829758D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.CanShrink = true;
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell24.StylePriority.UsePadding = false;
        this.xrTableCell24.Text = "Indicadores Operacionais";
        this.xrTableCell24.Weight = 0.31304932486010695D;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCabecalho,
            xrLine1,
            this.picLogoEntidade});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 207.9095F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblCabecalho
        // 
        this.lblCabecalho.Dpi = 254F;
        this.lblCabecalho.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblCabecalho.LocationFloat = new DevExpress.Utils.PointFloat(486.8333F, 25.00001F);
        this.lblCabecalho.Name = "lblCabecalho";
        this.lblCabecalho.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblCabecalho.SizeF = new System.Drawing.SizeF(2102.166F, 135.21F);
        this.lblCabecalho.StylePriority.UseFont = false;
        this.lblCabecalho.StylePriority.UseTextAlignment = false;
        this.lblCabecalho.Text = "SESCOOP - Proposta de impressão do TAI elaborada em 11/10/2012";
        this.lblCabecalho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(7.385531F, 10.20997F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(400.0728F, 150F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.picLogoEntidade.StylePriority.UseBorders = false;
        // 
        // DetailReport5
        // 
        this.DetailReport5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6});
        this.DetailReport5.DataMember = "detalhesProposta";
        this.DetailReport5.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport5.Dpi = 254F;
        this.DetailReport5.Level = 3;
        this.DetailReport5.Name = "DetailReport5";
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAreaAtuacao1,
            this.xrLabel26});
        this.Detail6.Dpi = 254F;
        this.Detail6.HeightF = 104.8402F;
        this.Detail6.Name = "Detail6";
        // 
        // DetailReport6
        // 
        this.DetailReport6.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.DetailReport7,
            this.DetailReport8,
            this.DetailReport9,
            this.GroupHeader7});
        this.DetailReport6.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.DetailReport6.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport6.Dpi = 254F;
        this.DetailReport6.Level = 6;
        this.DetailReport6.Name = "DetailReport6";
        // 
        // Detail7
        // 
        this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
        this.Detail7.Dpi = 254F;
        this.Detail7.HeightF = 218.5209F;
        this.Detail7.Name = "Detail7";
        // 
        // xrTable11
        // 
        this.xrTable11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable11.Dpi = 254F;
        this.xrTable11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable11.KeepTogether = true;
        this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable11.Name = "xrTable11";
        this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow13});
        this.xrTable11.SizeF = new System.Drawing.SizeF(2593F, 218.5209F);
        this.xrTable11.StylePriority.UseBorders = false;
        this.xrTable11.StylePriority.UseFont = false;
        // 
        // xrTableRow18
        // 
        this.xrTableRow18.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.celTituloAtividadeAcao,
            this.xrTableCell50,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56});
        this.xrTableRow18.Dpi = 254F;
        this.xrTableRow18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow18.Name = "xrTableRow18";
        this.xrTableRow18.StylePriority.UseBackColor = false;
        this.xrTableRow18.StylePriority.UseFont = false;
        this.xrTableRow18.StylePriority.UseTextAlignment = false;
        this.xrTableRow18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableRow18.Weight = 0.44716646200932642D;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.Text = "Num.";
        this.xrTableCell33.Weight = 0.0703693016243219D;
        // 
        // celTituloAtividadeAcao
        // 
        this.celTituloAtividadeAcao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.indicaAcaoAtividade")});
        this.celTituloAtividadeAcao.Dpi = 254F;
        this.celTituloAtividadeAcao.Name = "celTituloAtividadeAcao";
        this.celTituloAtividadeAcao.Text = "Atividade";
        this.celTituloAtividadeAcao.Weight = 0.26827407341524462D;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.Dpi = 254F;
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.Text = "Início";
        this.xrTableCell50.Weight = 0.13034048714249602D;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.Dpi = 254F;
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.Text = "Término";
        this.xrTableCell53.Weight = 0.10635191886137881D;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.Dpi = 254F;
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.Text = "Inst.";
        this.xrTableCell54.Weight = 0.0463808924386822D;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.Dpi = 254F;
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.Text = "Responsável";
        this.xrTableCell55.Weight = 0.27906897133663494D;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.Dpi = 254F;
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.Text = "Fonte de Recurso";
        this.xrTableCell56.Weight = 0.27468678094290766D;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celNumeroAcao,
            this.xrTableCell21,
            this.xrTableCell27,
            this.xrTableCell29,
            this.xrTableCell31,
            this.xrTableCell32,
            this.celFonteRecurso});
        this.xrTableRow11.Dpi = 254F;
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.StylePriority.UseTextAlignment = false;
        this.xrTableRow11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableRow11.Weight = 0.76696913476777329D;
        // 
        // celNumeroAcao
        // 
        this.celNumeroAcao.CanShrink = true;
        this.celNumeroAcao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.Numero")});
        this.celNumeroAcao.Dpi = 254F;
        this.celNumeroAcao.Name = "celNumeroAcao";
        this.celNumeroAcao.Text = "celNumeroAcao";
        this.celNumeroAcao.Weight = 0.0703693016243219D;
        this.celNumeroAcao.TextChanged += new System.EventHandler(this.celNumeroAcao_TextChanged);
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.CanShrink = true;
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.Descricao")});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.Weight = 0.26827407341524462D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.CanShrink = true;
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.Inicio", "{0:dd/MM/yyyy}")});
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Text = "xrTableCell27";
        this.xrTableCell27.Weight = 0.13034048714249602D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.CanShrink = true;
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.Termino", "{0:dd/MM/yyyy}")});
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Text = "xrTableCell29";
        this.xrTableCell29.Weight = 0.10635191886137881D;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.CanShrink = true;
        this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.indicaInstitucional")});
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "xrTableCell31";
        this.xrTableCell31.Weight = 0.0463808924386822D;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.CanShrink = true;
        this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.Responsavel")});
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "xrTableCell32";
        this.xrTableCell32.Weight = 0.27906897133663494D;
        // 
        // celFonteRecurso
        // 
        this.celFonteRecurso.CanShrink = true;
        this.celFonteRecurso.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.FonteRecurso")});
        this.celFonteRecurso.Dpi = 254F;
        this.celFonteRecurso.Name = "celFonteRecurso";
        this.celFonteRecurso.Text = "[detalhesProposta_AcoesIniciativa.FonteRecurso]";
        this.celFonteRecurso.Weight = 0.27468678094290766D;
        this.celFonteRecurso.TextChanged += new System.EventHandler(this.celFonteRecurso_TextChanged);
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell34});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.StylePriority.UseBackColor = false;
        this.xrTableRow12.Weight = 0.6952650341796498D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableCell20.CanShrink = true;
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.titLocalEvento")});
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBackColor = false;
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.Text = "Local do Evento";
        this.xrTableCell20.Weight = 0.46898386218206262D;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.CanShrink = true;
        this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.titDetalhesEvento")});
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseFont = false;
        this.xrTableCell34.Text = "Detalhes do Evento";
        this.xrTableCell34.Weight = 0.70648856357960366D;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell47});
        this.xrTableRow13.Dpi = 254F;
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1.0150677069380967D;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.CanShrink = true;
        this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.LocalEvento")});
        this.xrTableCell46.Dpi = 254F;
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.Text = "xrTableCell46";
        this.xrTableCell46.Weight = 0.46898386218206262D;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.CanShrink = true;
        this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.DetalhesEvento")});
        this.xrTableCell47.Dpi = 254F;
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.Text = "xrTableCell47";
        this.xrTableCell47.Weight = 0.70648856357960366D;
        // 
        // DetailReport7
        // 
        this.DetailReport7.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail8,
            this.GroupHeader8});
        this.DetailReport7.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ProdutosAcoesIn" +
            "iciativa";
        this.DetailReport7.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport7.Dpi = 254F;
        this.DetailReport7.Level = 0;
        this.DetailReport7.Name = "DetailReport7";
        // 
        // Detail8
        // 
        this.Detail8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13});
        this.Detail8.Dpi = 254F;
        this.Detail8.HeightF = 40F;
        this.Detail8.Name = "Detail8";
        // 
        // xrLabel13
        // 
        this.xrLabel13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel13.CanShrink = true;
        this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ProdutosAcoesIn" +
                    "iciativa.Meta")});
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.KeepTogether = true;
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(130.6459F, 0F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(2462.354F, 40F);
        this.xrLabel13.StylePriority.UseBorders = false;
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.Text = "xrLabel13";
        // 
        // GroupHeader8
        // 
        this.GroupHeader8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTituloMetaProduto});
        this.GroupHeader8.Dpi = 254F;
        this.GroupHeader8.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader8.HeightF = 125.1908F;
        this.GroupHeader8.Name = "GroupHeader8";
        // 
        // lblTituloMetaProduto
        // 
        this.lblTituloMetaProduto.BackColor = System.Drawing.Color.WhiteSmoke;
        this.lblTituloMetaProduto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTituloMetaProduto.CanShrink = true;
        this.lblTituloMetaProduto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.indicaProdutoMeta")});
        this.lblTituloMetaProduto.Dpi = 254F;
        this.lblTituloMetaProduto.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.lblTituloMetaProduto.KeepTogether = true;
        this.lblTituloMetaProduto.LocationFloat = new DevExpress.Utils.PointFloat(130.6459F, 79.99996F);
        this.lblTituloMetaProduto.Name = "lblTituloMetaProduto";
        this.lblTituloMetaProduto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTituloMetaProduto.SizeF = new System.Drawing.SizeF(2461.354F, 45.19082F);
        this.lblTituloMetaProduto.StylePriority.UseBackColor = false;
        this.lblTituloMetaProduto.StylePriority.UseBorders = false;
        this.lblTituloMetaProduto.StylePriority.UseFont = false;
        this.lblTituloMetaProduto.StylePriority.UseTextAlignment = false;
        this.lblTituloMetaProduto.Text = "Metas/Produtos";
        this.lblTituloMetaProduto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReport8
        // 
        this.DetailReport8.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail9,
            this.GroupHeader9});
        this.DetailReport8.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
            "tiva";
        this.DetailReport8.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport8.Dpi = 254F;
        this.DetailReport8.Level = 1;
        this.DetailReport8.Name = "DetailReport8";
        // 
        // Detail9
        // 
        this.Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
        this.Detail9.Dpi = 254F;
        this.Detail9.HeightF = 45F;
        this.Detail9.Name = "Detail9";
        // 
        // xrTable13
        // 
        this.xrTable13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable13.Dpi = 254F;
        this.xrTable13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable13.KeepTogether = true;
        this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(130.6459F, 0F);
        this.xrTable13.Name = "xrTable13";
        this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
        this.xrTable13.SizeF = new System.Drawing.SizeF(2462.354F, 45F);
        this.xrTable13.StylePriority.UseBorders = false;
        this.xrTable13.StylePriority.UseFont = false;
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell41,
            this.xrTableCell49});
        this.xrTableRow14.Dpi = 254F;
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 0.5679012345679012D;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell41.CanShrink = true;
        this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
                    "tiva.Area")});
        this.xrTableCell41.Dpi = 254F;
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.Text = "xrTableCell41";
        this.xrTableCell41.Weight = 0.92827129248623D;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell49.CanShrink = true;
        this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
                    "tiva.Elemento")});
        this.xrTableCell49.Dpi = 254F;
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.StylePriority.UseBorders = false;
        this.xrTableCell49.Text = "xrTableCell49";
        this.xrTableCell49.Weight = 1.2533237995383098D;
        // 
        // GroupHeader9
        // 
        this.GroupHeader9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14});
        this.GroupHeader9.Dpi = 254F;
        this.GroupHeader9.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader9.HeightF = 125F;
        this.GroupHeader9.Name = "GroupHeader9";
        // 
        // xrTable14
        // 
        this.xrTable14.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top;
        this.xrTable14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable14.Dpi = 254F;
        this.xrTable14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable14.KeepTogether = true;
        this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(130.6459F, 75.00002F);
        this.xrTable14.Name = "xrTable14";
        this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
        this.xrTable14.SizeF = new System.Drawing.SizeF(2462.354F, 50F);
        this.xrTable14.StylePriority.UseBorders = false;
        this.xrTable14.StylePriority.UseFont = false;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celTituloAreaParceria,
            this.celTituloElementoParceria});
        this.xrTableRow15.Dpi = 254F;
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.StylePriority.UseBackColor = false;
        this.xrTableRow15.Weight = 0.44716640702060584D;
        // 
        // celTituloAreaParceria
        // 
        this.celTituloAreaParceria.CanShrink = true;
        this.celTituloAreaParceria.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
                    "tiva.titAreaDeParceriaCalc")});
        this.celTituloAreaParceria.Dpi = 254F;
        this.celTituloAreaParceria.Name = "celTituloAreaParceria";
        this.celTituloAreaParceria.Text = "Área de Parceria";
        this.celTituloAreaParceria.Weight = 0.92827129248623D;
        // 
        // celTituloElementoParceria
        // 
        this.celTituloElementoParceria.CanShrink = true;
        this.celTituloElementoParceria.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
                    "tiva.titElementoDeParceriaCalc")});
        this.celTituloElementoParceria.Dpi = 254F;
        this.celTituloElementoParceria.Name = "celTituloElementoParceria";
        this.celTituloElementoParceria.Text = "Elemento da Parceria";
        this.celTituloElementoParceria.Weight = 1.2533237995383098D;
        // 
        // DetailReport9
        // 
        this.DetailReport9.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail10,
            this.ReportFooter1,
            this.GroupHeader10});
        this.DetailReport9.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
            "a";
        this.DetailReport9.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport9.Dpi = 254F;
        this.DetailReport9.Level = 2;
        this.DetailReport9.Name = "DetailReport9";
        // 
        // Detail10
        // 
        this.Detail10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable15});
        this.Detail10.Dpi = 254F;
        this.Detail10.HeightF = 40F;
        this.Detail10.Name = "Detail10";
        // 
        // xrTable15
        // 
        this.xrTable15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable15.Dpi = 254F;
        this.xrTable15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable15.KeepTogether = true;
        this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(130.6459F, 0F);
        this.xrTable15.Name = "xrTable15";
        this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
        this.xrTable15.SizeF = new System.Drawing.SizeF(2462.354F, 40F);
        this.xrTable15.StylePriority.UseBorders = false;
        this.xrTable15.StylePriority.UseFont = false;
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell51,
            this.xrTableCell52});
        this.xrTableRow16.Dpi = 254F;
        this.xrTableRow16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.StylePriority.UseFont = false;
        this.xrTableRow16.Weight = 0.35773306114513459D;
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell51.CanShrink = true;
        this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
                    "a.Marco")});
        this.xrTableCell51.Dpi = 254F;
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.StylePriority.UseBorders = false;
        this.xrTableCell51.Text = "xrTableCell51";
        this.xrTableCell51.Weight = 1.8795669190803204D;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell52.CanShrink = true;
        this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
                    "a.Data", "{0:dd/MM/yyyy}")});
        this.xrTableCell52.Dpi = 254F;
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.StylePriority.UseBorders = false;
        this.xrTableCell52.Text = "xrTableCell52";
        this.xrTableCell52.Weight = 0.30202817294421958D;
        // 
        // ReportFooter1
        // 
        this.ReportFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2});
        this.ReportFooter1.Dpi = 254F;
        this.ReportFooter1.HeightF = 58.42F;
        this.ReportFooter1.Name = "ReportFooter1";
        // 
        // xrLine2
        // 
        this.xrLine2.Dpi = 254F;
        this.xrLine2.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine2.LineWidth = 3;
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLine2.StylePriority.UseForeColor = false;
        // 
        // GroupHeader10
        // 
        this.GroupHeader10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable16});
        this.GroupHeader10.Dpi = 254F;
        this.GroupHeader10.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader10.HeightF = 125F;
        this.GroupHeader10.Name = "GroupHeader10";
        // 
        // xrTable16
        // 
        this.xrTable16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable16.Dpi = 254F;
        this.xrTable16.KeepTogether = true;
        this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(130.646F, 75.00002F);
        this.xrTable16.Name = "xrTable16";
        this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
        this.xrTable16.SizeF = new System.Drawing.SizeF(2462.354F, 50F);
        this.xrTable16.StylePriority.UseBorders = false;
        // 
        // xrTableRow17
        // 
        this.xrTableRow17.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celTitMarcoProjeto,
            this.xrTableCell45});
        this.xrTableRow17.Dpi = 254F;
        this.xrTableRow17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow17.Name = "xrTableRow17";
        this.xrTableRow17.StylePriority.UseBackColor = false;
        this.xrTableRow17.StylePriority.UseFont = false;
        this.xrTableRow17.Weight = 0.44716632643141829D;
        // 
        // celTitMarcoProjeto
        // 
        this.celTitMarcoProjeto.CanShrink = true;
        this.celTitMarcoProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
                    "a.titMarcoProjeto")});
        this.celTitMarcoProjeto.Dpi = 254F;
        this.celTitMarcoProjeto.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celTitMarcoProjeto.Name = "celTitMarcoProjeto";
        this.celTitMarcoProjeto.StylePriority.UseFont = false;
        this.celTitMarcoProjeto.Text = "Marco do  Projeto";
        this.celTitMarcoProjeto.Weight = 1.8798530887182676D;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.CanShrink = true;
        this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
                    "a.titData")});
        this.xrTableCell45.Dpi = 254F;
        this.xrTableCell45.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.StylePriority.UseFont = false;
        this.xrTableCell45.Text = "Data";
        this.xrTableCell45.Weight = 0.30174200330627221D;
        // 
        // GroupHeader7
        // 
        this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.GroupHeader7.Dpi = 254F;
        this.GroupHeader7.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader7.HeightF = 140F;
        this.GroupHeader7.Name = "GroupHeader7";
        // 
        // xrLabel3
        // 
        this.xrLabel3.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1.00002F, 41.79008F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(2591F, 58.42F);
        this.xrLabel3.StylePriority.UseBackColor = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Plano de Trabalho";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // titElementoDeParceriaCalc
        // 
        this.titElementoDeParceriaCalc.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
            "tiva";
        this.titElementoDeParceriaCalc.Expression = "Iif([Elemento]   != \'\' , \'Elemento da Parceria\' ,\'\')";
        this.titElementoDeParceriaCalc.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.titElementoDeParceriaCalc.Name = "titElementoDeParceriaCalc";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 58.42F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.BorderColor = System.Drawing.Color.DarkOliveGreen;
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0.9999995F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(2062.833F, 58.42F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // titAreaDeParceriaCalc
        // 
        this.titAreaDeParceriaCalc.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
            "tiva";
        this.titAreaDeParceriaCalc.Expression = "Iif([Area]   != \'\' , \'Área de Parceria\' ,\'\')";
        this.titAreaDeParceriaCalc.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.titAreaDeParceriaCalc.Name = "titAreaDeParceriaCalc";
        // 
        // titMetaProduto
        // 
        this.titMetaProduto.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ProdutosAcoesIn" +
            "iciativa";
        this.titMetaProduto.Name = "titMetaProduto";
        // 
        // indicaProdutoMeta
        // 
        this.indicaProdutoMeta.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.indicaProdutoMeta.Expression = "Iif(Contains([Numero], \'.\'),\'Produto\'  ,\'Meta\' )";
        this.indicaProdutoMeta.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.indicaProdutoMeta.Name = "indicaProdutoMeta";
        // 
        // indicaAcaoAtividade
        // 
        this.indicaAcaoAtividade.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.indicaAcaoAtividade.Expression = "Iif(Contains([Numero], \'.\'),\'Atividade\'  ,\'Ação\' )";
        this.indicaAcaoAtividade.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.indicaAcaoAtividade.Name = "indicaAcaoAtividade";
        // 
        // titMarcoProjeto
        // 
        this.titMarcoProjeto.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
            "a";
        this.titMarcoProjeto.Expression = "Iif([Marco]   != \'\' , \'Marco do projeto\' ,\'\')";
        this.titMarcoProjeto.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.titMarcoProjeto.Name = "titMarcoProjeto";
        // 
        // titData
        // 
        this.titData.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
            "a";
        this.titData.Expression = "Iif([Data]   != \'\' , \'Data\' ,\'\')";
        this.titData.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.titData.Name = "titData";
        // 
        // titLocalEvento
        // 
        this.titLocalEvento.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.titLocalEvento.Expression = "Iif( IsNullOrEmpty([LocalEvento]), \'\' ,\'Local do Evento\')";
        this.titLocalEvento.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.titLocalEvento.Name = "titLocalEvento";
        // 
        // titDetalhesEvento
        // 
        this.titDetalhesEvento.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.titDetalhesEvento.Expression = "Iif( IsNullOrEmpty([DetalhesEvento]), \'\' ,\'Detalhes do Evento\')";
        this.titDetalhesEvento.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.titDetalhesEvento.Name = "titDetalhesEvento";
        // 
        // indicaInstitucional
        // 
        this.indicaInstitucional.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.indicaInstitucional.Expression = "Iif(Contains([Institucional], \'S\'),\'Sim\'  ,\'Não\' )";
        this.indicaInstitucional.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.indicaInstitucional.Name = "indicaInstitucional";
        // 
        // DetailReport11
        // 
        this.DetailReport11.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail12,
            this.GroupHeader1,
            this.ReportFooter2,
            this.GroupHeader11});
        this.DetailReport11.DataMember = "CronogramaOrc";
        this.DetailReport11.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport11.Dpi = 254F;
        this.DetailReport11.Level = 7;
        this.DetailReport11.Name = "DetailReport11";
        // 
        // Detail12
        // 
        this.Detail12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
        this.Detail12.Dpi = 254F;
        this.Detail12.HeightF = 233.4375F;
        this.Detail12.Name = "Detail12";
        // 
        // xrTable12
        // 
        this.xrTable12.Dpi = 254F;
        this.xrTable12.KeepTogether = true;
        this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(86.95836F, 0F);
        this.xrTable12.Name = "xrTable12";
        this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow22,
            this.xrTableRow19,
            this.xrTableRow24,
            this.xrTableRow25,
            this.xrTableRow26,
            this.xrTableRow27});
        this.xrTable12.SizeF = new System.Drawing.SizeF(2503.042F, 233.4375F);
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableRow22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell78,
            this.xrTableCell79,
            this.xrTableCell80,
            this.xrTableCell81});
        this.xrTableRow22.Dpi = 254F;
        this.xrTableRow22.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.StylePriority.UseBackColor = false;
        this.xrTableRow22.StylePriority.UseBorders = false;
        this.xrTableRow22.StylePriority.UseFont = false;
        this.xrTableRow22.Weight = 0.69663555527255383D;
        // 
        // xrTableCell78
        // 
        this.xrTableCell78.CanShrink = true;
        this.xrTableCell78.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField16")});
        this.xrTableCell78.Dpi = 254F;
        this.xrTableCell78.Name = "xrTableCell78";
        this.xrTableCell78.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell78.StylePriority.UsePadding = false;
        this.xrTableCell78.StylePriority.UseTextAlignment = false;
        this.xrTableCell78.Text = "Conta Orçamentaria";
        this.xrTableCell78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell78.Weight = 0.62036792447568423D;
        // 
        // xrTableCell79
        // 
        this.xrTableCell79.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell79.CanShrink = true;
        this.xrTableCell79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField17")});
        this.xrTableCell79.Dpi = 254F;
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell79.StylePriority.UseBorders = false;
        this.xrTableCell79.StylePriority.UsePadding = false;
        this.xrTableCell79.StylePriority.UseTextAlignment = false;
        this.xrTableCell79.Text = "Quantidade";
        this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell79.Weight = 0.23149035895462022D;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell80.CanShrink = true;
        this.xrTableCell80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField18")});
        this.xrTableCell80.Dpi = 254F;
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell80.StylePriority.UseBorders = false;
        this.xrTableCell80.StylePriority.UsePadding = false;
        this.xrTableCell80.StylePriority.UseTextAlignment = false;
        this.xrTableCell80.Text = "Val. Unit.";
        this.xrTableCell80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell80.Weight = 0.23378557532037153D;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell81.CanShrink = true;
        this.xrTableCell81.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField19")});
        this.xrTableCell81.Dpi = 254F;
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell81.StylePriority.UseBorders = false;
        this.xrTableCell81.StylePriority.UsePadding = false;
        this.xrTableCell81.StylePriority.UseTextAlignment = false;
        this.xrTableCell81.Text = "Memória de Cálculo";
        this.xrTableCell81.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell81.Weight = 0.77552081537196171D;
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40});
        this.xrTableRow19.Dpi = 254F;
        this.xrTableRow19.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.StylePriority.UseBorders = false;
        this.xrTableRow19.StylePriority.UseFont = false;
        this.xrTableRow19.Weight = 0.43916691386324858D;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell37.CanShrink = true;
        this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField21")});
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell37.StylePriority.UseBorders = false;
        this.xrTableCell37.StylePriority.UseFont = false;
        this.xrTableCell37.StylePriority.UsePadding = false;
        this.xrTableCell37.Text = "xrTableCell37";
        this.xrTableCell37.Weight = 0.62036792447568434D;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell38.CanShrink = true;
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Quantidade")});
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.StylePriority.UseFont = false;
        this.xrTableCell38.StylePriority.UsePadding = false;
        this.xrTableCell38.StylePriority.UseTextAlignment = false;
        this.xrTableCell38.Text = "xrTableCell38";
        this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell38.Weight = 0.23149035895462022D;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell39.CanShrink = true;
        this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorUnitario", "{0:c2}")});
        this.xrTableCell39.Dpi = 254F;
        this.xrTableCell39.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell39.StylePriority.UseBorders = false;
        this.xrTableCell39.StylePriority.UseFont = false;
        this.xrTableCell39.StylePriority.UsePadding = false;
        this.xrTableCell39.StylePriority.UseTextAlignment = false;
        this.xrTableCell39.Text = "xrTableCell39";
        this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell39.Weight = 0.23378557532037153D;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell40.CanShrink = true;
        this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.MemoriaCalculo")});
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell40.StylePriority.UseBorders = false;
        this.xrTableCell40.StylePriority.UseFont = false;
        this.xrTableCell40.StylePriority.UsePadding = false;
        this.xrTableCell40.StylePriority.UseTextAlignment = false;
        this.xrTableCell40.Text = "xrTableCell40";
        this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell40.Weight = 0.77552081537196171D;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell63,
            this.xrTableCell76,
            this.xrTableCell82,
            this.xrTableCell84,
            this.xrTableCell86,
            this.xrTableCell88});
        this.xrTableRow24.Dpi = 254F;
        this.xrTableRow24.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.StylePriority.UseBackColor = false;
        this.xrTableRow24.StylePriority.UseFont = false;
        this.xrTableRow24.StylePriority.UseTextAlignment = false;
        this.xrTableRow24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableRow24.Weight = 0.42592587858115571D;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.CanShrink = true;
        this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField3")});
        this.xrTableCell63.Dpi = 254F;
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.Text = "xrTableCell63";
        this.xrTableCell63.Weight = 0.31019387201785964D;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.CanShrink = true;
        this.xrTableCell76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField4")});
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.Text = "xrTableCell76";
        this.xrTableCell76.Weight = 0.31019140033300313D;
        // 
        // xrTableCell82
        // 
        this.xrTableCell82.CanShrink = true;
        this.xrTableCell82.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField5")});
        this.xrTableCell82.Dpi = 254F;
        this.xrTableCell82.Name = "xrTableCell82";
        this.xrTableCell82.Text = "xrTableCell82";
        this.xrTableCell82.Weight = 0.31019139378143995D;
        // 
        // xrTableCell84
        // 
        this.xrTableCell84.CanShrink = true;
        this.xrTableCell84.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField6")});
        this.xrTableCell84.Dpi = 254F;
        this.xrTableCell84.Name = "xrTableCell84";
        this.xrTableCell84.Text = "xrTableCell84";
        this.xrTableCell84.Weight = 0.31019139648078864D;
        // 
        // xrTableCell86
        // 
        this.xrTableCell86.CanShrink = true;
        this.xrTableCell86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField7")});
        this.xrTableCell86.Dpi = 254F;
        this.xrTableCell86.Name = "xrTableCell86";
        this.xrTableCell86.Text = "xrTableCell86";
        this.xrTableCell86.Weight = 0.3101913955107386D;
        // 
        // xrTableCell88
        // 
        this.xrTableCell88.CanShrink = true;
        this.xrTableCell88.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField8")});
        this.xrTableCell88.Dpi = 254F;
        this.xrTableCell88.Name = "xrTableCell88";
        this.xrTableCell88.Text = "xrTableCell88";
        this.xrTableCell88.Weight = 0.31020521599880796D;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell75,
            this.xrTableCell77,
            this.xrTableCell83,
            this.xrTableCell85,
            this.xrTableCell87,
            this.xrTableCell89});
        this.xrTableRow25.Dpi = 254F;
        this.xrTableRow25.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.StylePriority.UseFont = false;
        this.xrTableRow25.StylePriority.UseTextAlignment = false;
        this.xrTableRow25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableRow25.Weight = 0.42592587858115571D;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.CanShrink = true;
        this.xrTableCell75.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan01", "{0:c2}")});
        this.xrTableCell75.Dpi = 254F;
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.Text = "xrTableCell75";
        this.xrTableCell75.Weight = 0.31019387201785964D;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.CanShrink = true;
        this.xrTableCell77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan02", "{0:c2}")});
        this.xrTableCell77.Dpi = 254F;
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.Text = "xrTableCell77";
        this.xrTableCell77.Weight = 0.31019140033300313D;
        // 
        // xrTableCell83
        // 
        this.xrTableCell83.CanShrink = true;
        this.xrTableCell83.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan03", "{0:c2}")});
        this.xrTableCell83.Dpi = 254F;
        this.xrTableCell83.Name = "xrTableCell83";
        this.xrTableCell83.Text = "xrTableCell83";
        this.xrTableCell83.Weight = 0.31019139378143995D;
        // 
        // xrTableCell85
        // 
        this.xrTableCell85.CanShrink = true;
        this.xrTableCell85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan04", "{0:c2}")});
        this.xrTableCell85.Dpi = 254F;
        this.xrTableCell85.Name = "xrTableCell85";
        this.xrTableCell85.Text = "xrTableCell85";
        this.xrTableCell85.Weight = 0.31019139648078864D;
        // 
        // xrTableCell87
        // 
        this.xrTableCell87.CanShrink = true;
        this.xrTableCell87.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan05", "{0:c2}")});
        this.xrTableCell87.Dpi = 254F;
        this.xrTableCell87.Name = "xrTableCell87";
        this.xrTableCell87.Text = "xrTableCell87";
        this.xrTableCell87.Weight = 0.3101913955107386D;
        // 
        // xrTableCell89
        // 
        this.xrTableCell89.CanShrink = true;
        this.xrTableCell89.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan06", "{0:c2}")});
        this.xrTableCell89.Dpi = 254F;
        this.xrTableCell89.Name = "xrTableCell89";
        this.xrTableCell89.Text = "xrTableCell89";
        this.xrTableCell89.Weight = 0.31020521599880796D;
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell90,
            this.xrTableCell91,
            this.xrTableCell92,
            this.xrTableCell93,
            this.xrTableCell94,
            this.xrTableCell96});
        this.xrTableRow26.Dpi = 254F;
        this.xrTableRow26.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.StylePriority.UseBackColor = false;
        this.xrTableRow26.StylePriority.UseFont = false;
        this.xrTableRow26.StylePriority.UseTextAlignment = false;
        this.xrTableRow26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableRow26.Weight = 0.42592587858115571D;
        // 
        // xrTableCell90
        // 
        this.xrTableCell90.CanShrink = true;
        this.xrTableCell90.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField9")});
        this.xrTableCell90.Dpi = 254F;
        this.xrTableCell90.Name = "xrTableCell90";
        this.xrTableCell90.Text = "xrTableCell90";
        this.xrTableCell90.Weight = 0.31019387201785964D;
        // 
        // xrTableCell91
        // 
        this.xrTableCell91.CanShrink = true;
        this.xrTableCell91.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField10")});
        this.xrTableCell91.Dpi = 254F;
        this.xrTableCell91.Name = "xrTableCell91";
        this.xrTableCell91.Text = "xrTableCell91";
        this.xrTableCell91.Weight = 0.31019140033300313D;
        // 
        // xrTableCell92
        // 
        this.xrTableCell92.CanShrink = true;
        this.xrTableCell92.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField11")});
        this.xrTableCell92.Dpi = 254F;
        this.xrTableCell92.Name = "xrTableCell92";
        this.xrTableCell92.Text = "xrTableCell92";
        this.xrTableCell92.Weight = 0.31019139378143995D;
        // 
        // xrTableCell93
        // 
        this.xrTableCell93.CanShrink = true;
        this.xrTableCell93.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField12")});
        this.xrTableCell93.Dpi = 254F;
        this.xrTableCell93.Name = "xrTableCell93";
        this.xrTableCell93.Text = "xrTableCell93";
        this.xrTableCell93.Weight = 0.31019139648078864D;
        // 
        // xrTableCell94
        // 
        this.xrTableCell94.CanShrink = true;
        this.xrTableCell94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField13")});
        this.xrTableCell94.Dpi = 254F;
        this.xrTableCell94.Name = "xrTableCell94";
        this.xrTableCell94.Text = "xrTableCell94";
        this.xrTableCell94.Weight = 0.3101913955107386D;
        // 
        // xrTableCell96
        // 
        this.xrTableCell96.CanShrink = true;
        this.xrTableCell96.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField14")});
        this.xrTableCell96.Dpi = 254F;
        this.xrTableCell96.Name = "xrTableCell96";
        this.xrTableCell96.Text = "xrTableCell96";
        this.xrTableCell96.Weight = 0.31020521599880796D;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell97,
            this.xrTableCell98,
            this.xrTableCell99,
            this.xrTableCell100,
            this.xrTableCell101,
            this.xrTableCell102});
        this.xrTableRow27.Dpi = 254F;
        this.xrTableRow27.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.StylePriority.UseFont = false;
        this.xrTableRow27.StylePriority.UseTextAlignment = false;
        this.xrTableRow27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableRow27.Weight = 0.42592587858115571D;
        // 
        // xrTableCell97
        // 
        this.xrTableCell97.CanShrink = true;
        this.xrTableCell97.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan07")});
        this.xrTableCell97.Dpi = 254F;
        this.xrTableCell97.Name = "xrTableCell97";
        this.xrTableCell97.Text = "xrTableCell97";
        this.xrTableCell97.Weight = 0.31019387201785964D;
        // 
        // xrTableCell98
        // 
        this.xrTableCell98.CanShrink = true;
        this.xrTableCell98.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan08")});
        this.xrTableCell98.Dpi = 254F;
        this.xrTableCell98.Name = "xrTableCell98";
        this.xrTableCell98.Text = "xrTableCell98";
        this.xrTableCell98.Weight = 0.31019140033300313D;
        // 
        // xrTableCell99
        // 
        this.xrTableCell99.CanShrink = true;
        this.xrTableCell99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan09", "{0:c2}")});
        this.xrTableCell99.Dpi = 254F;
        this.xrTableCell99.Name = "xrTableCell99";
        this.xrTableCell99.Text = "xrTableCell99";
        this.xrTableCell99.Weight = 0.31019139378143995D;
        // 
        // xrTableCell100
        // 
        this.xrTableCell100.CanShrink = true;
        this.xrTableCell100.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan10", "{0:c2}")});
        this.xrTableCell100.Dpi = 254F;
        this.xrTableCell100.Name = "xrTableCell100";
        this.xrTableCell100.Text = "xrTableCell100";
        this.xrTableCell100.Weight = 0.31019139648078864D;
        // 
        // xrTableCell101
        // 
        this.xrTableCell101.CanShrink = true;
        this.xrTableCell101.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan11", "{0:c2}")});
        this.xrTableCell101.Dpi = 254F;
        this.xrTableCell101.Name = "xrTableCell101";
        this.xrTableCell101.Text = "xrTableCell101";
        this.xrTableCell101.Weight = 0.3101913955107386D;
        // 
        // xrTableCell102
        // 
        this.xrTableCell102.CanShrink = true;
        this.xrTableCell102.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Plan12", "{0:c2}")});
        this.xrTableCell102.Dpi = 254F;
        this.xrTableCell102.Name = "xrTableCell102";
        this.xrTableCell102.Text = "xrTableCell102";
        this.xrTableCell102.Weight = 0.31020521599880796D;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable17});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Numero", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("DescricaoAcao", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 73.81252F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrTable17
        // 
        this.xrTable17.Dpi = 254F;
        this.xrTable17.KeepTogether = true;
        this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable17.Name = "xrTable17";
        this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23});
        this.xrTable17.SizeF = new System.Drawing.SizeF(2593F, 50F);
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableRow23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell95,
            this.celFonteRecurso2,
            this.celTotal});
        this.xrTableRow23.Dpi = 254F;
        this.xrTableRow23.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.StylePriority.UseBackColor = false;
        this.xrTableRow23.StylePriority.UseBorders = false;
        this.xrTableRow23.StylePriority.UseFont = false;
        this.xrTableRow23.Weight = 0.5679012345679012D;
        // 
        // xrTableCell95
        // 
        this.xrTableCell95.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField2")});
        this.xrTableCell95.Dpi = 254F;
        this.xrTableCell95.Name = "xrTableCell95";
        this.xrTableCell95.StylePriority.UseBackColor = false;
        this.xrTableCell95.Text = "xrTableCell95";
        this.xrTableCell95.Weight = 0.87010280460071332D;
        this.xrTableCell95.TextChanged += new System.EventHandler(this.xrTableCell95_TextChanged);
        // 
        // celFonteRecurso2
        // 
        this.celFonteRecurso2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.FonteRecurso")});
        this.celFonteRecurso2.Dpi = 254F;
        this.celFonteRecurso2.Name = "celFonteRecurso2";
        this.celFonteRecurso2.Text = "[fonteRecursoCalculado1]";
        this.celFonteRecurso2.Weight = 1.0963041619879756D;
        this.celFonteRecurso2.TextChanged += new System.EventHandler(this.celFonteRecurso2_TextChanged);
        // 
        // celTotal
        // 
        this.celTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField23", "{0:c2}")});
        this.celTotal.Dpi = 254F;
        this.celTotal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celTotal.Name = "celTotal";
        this.celTotal.StylePriority.UseFont = false;
        this.celTotal.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:c2}";
        this.celTotal.Summary = xrSummary1;
        this.celTotal.Text = "celTotal";
        this.celTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.celTotal.Weight = 0.48249392041478223D;
        this.celTotal.SummaryRowChanged += new System.EventHandler(this.celTotal_SummaryRowChanged);
        // 
        // ReportFooter2
        // 
        this.ReportFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine3});
        this.ReportFooter2.Dpi = 254F;
        this.ReportFooter2.HeightF = 58.42F;
        this.ReportFooter2.Name = "ReportFooter2";
        // 
        // xrLine3
        // 
        this.xrLine3.Dpi = 254F;
        this.xrLine3.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine3.LineWidth = 3;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLine3.StylePriority.UseForeColor = false;
        // 
        // GroupHeader11
        // 
        this.GroupHeader11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel33,
            this.xrLabel32,
            this.xrLabel27,
            this.xrLabel35,
            this.xrLabel34,
            this.xrLabel25,
            this.xrLabel21});
        this.GroupHeader11.Dpi = 254F;
        this.GroupHeader11.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader11.HeightF = 259.2917F;
        this.GroupHeader11.Level = 1;
        this.GroupHeader11.Name = "GroupHeader11";
        // 
        // xrLabel9
        // 
        this.xrLabel9.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(2.00012F, 25.00001F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(2591F, 58.42F);
        this.xrLabel9.StylePriority.UseBackColor = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Cronograma Orçamentário";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel33
        // 
        this.xrLabel33.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel33.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.calculatedField22")});
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(1.0001F, 155.8318F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(706.4375F, 45.19084F);
        this.xrLabel33.StylePriority.UseBackColor = false;
        this.xrLabel33.StylePriority.UseBorders = false;
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.Text = "xrLabel33";
        // 
        // xrLabel32
        // 
        this.xrLabel32.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel32.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorUnitario")});
        this.xrLabel32.Dpi = 254F;
        this.xrLabel32.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(890.7927F, 155.8318F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(383.6459F, 45.19092F);
        this.xrLabel32.StylePriority.UseBackColor = false;
        this.xrLabel32.StylePriority.UseBorders = false;
        this.xrLabel32.StylePriority.UseFont = false;
        this.xrLabel32.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:c2}";
        xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel32.Summary = xrSummary2;
        this.xrLabel32.Text = "xrLabel32";
        this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrLabel32.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel32_SummaryGetResult);
        this.xrLabel32.SummaryRowChanged += new System.EventHandler(this.xrLabel32_SummaryRowChanged);
        // 
        // xrLabel27
        // 
        this.xrLabel27.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(731.8019F, 155.8318F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(158.9908F, 45.19093F);
        this.xrLabel27.StylePriority.UseBackColor = false;
        this.xrLabel27.StylePriority.UseBorders = false;
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "Total UN:";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel35
        // 
        this.xrLabel35.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(1339.438F, 155.8318F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(290.0618F, 45.19093F);
        this.xrLabel35.StylePriority.UseBackColor = false;
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "Total FUNDECOOP:";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel34
        // 
        this.xrLabel34.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel34.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorUnitario")});
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(1629.5F, 155.8318F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(293.6875F, 45.19092F);
        this.xrLabel34.StylePriority.UseBackColor = false;
        this.xrLabel34.StylePriority.UseBorders = false;
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:c2}";
        xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
        xrSummary3.IgnoreNullValues = true;
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel34.Summary = xrSummary3;
        this.xrLabel34.Text = "xrLabel34";
        this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrLabel34.SummaryCalculated += new DevExpress.XtraReports.UI.TextFormatEventHandler(this.xrLabel34_SummaryCalculated);
        this.xrLabel34.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel34_SummaryGetResult);
        this.xrLabel34.SummaryRowChanged += new System.EventHandler(this.xrLabel34_SummaryRowChanged);
        // 
        // xrLabel25
        // 
        this.xrLabel25.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(2082.115F, 155.8316F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(193.146F, 45.19084F);
        this.xrLabel25.StylePriority.UseBackColor = false;
        this.xrLabel25.StylePriority.UseBorders = false;
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "Total geral:";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel21
        // 
        this.xrLabel21.BackColor = System.Drawing.Color.Gainsboro;
        this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.calculatedField1")});
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(2275.261F, 155.8316F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(317.7388F, 45.19092F);
        this.xrLabel21.StylePriority.UseBackColor = false;
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.StylePriority.UseTextAlignment = false;
        xrSummary4.FormatString = "{0:c2}";
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel21.Summary = xrSummary4;
        this.xrLabel21.Text = "xrLabel21";
        this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // calculatedField1
        // 
        this.calculatedField1.DataMember = "CronogramaOrc";
        this.calculatedField1.DisplayName = "somatorio";
        this.calculatedField1.Expression = "[Plan01] + [Plan02] + [Plan03] + [Plan04] + [Plan05] + [Plan06] + [Plan07] + [Pla" +
            "n08] + [Plan09] + [Plan10] + [Plan11] + [Plan12]";
        this.calculatedField1.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.calculatedField1.Name = "calculatedField1";
        // 
        // calculatedField2
        // 
        this.calculatedField2.DataMember = "CronogramaOrc";
        this.calculatedField2.DisplayName = "numeroEDescricao";
        this.calculatedField2.Expression = "[Numero] + \' - \' + [DescricaoAcao]";
        this.calculatedField2.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField2.Name = "calculatedField2";
        // 
        // fonteRecursoCalculado1
        // 
        this.fonteRecursoCalculado1.DataMember = "CronogramaOrc";
        this.fonteRecursoCalculado1.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.fonteRecursoCalculado1.Name = "fonteRecursoCalculado1";
        // 
        // calculatedField3
        // 
        this.calculatedField3.DataMember = "CronogramaOrc";
        this.calculatedField3.DisplayName = "cabecalhoJaneiro";
        this.calculatedField3.Expression = "Iif(IsNullOrEmpty([Plan01]), \'\' ,\'Janeiro\' )";
        this.calculatedField3.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField3.Name = "calculatedField3";
        // 
        // calculatedField4
        // 
        this.calculatedField4.DataMember = "CronogramaOrc";
        this.calculatedField4.DisplayName = "cabecalhoFevereiro";
        this.calculatedField4.Expression = "Iif(IsNullOrEmpty([Plan02]), \'\' ,\'Fevereiro\' )";
        this.calculatedField4.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField4.Name = "calculatedField4";
        // 
        // calculatedField5
        // 
        this.calculatedField5.DataMember = "CronogramaOrc";
        this.calculatedField5.DisplayName = "cabecalhoMarco";
        this.calculatedField5.Expression = "Iif(IsNullOrEmpty([Plan03]), \'\' ,\'Março\' )";
        this.calculatedField5.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField5.Name = "calculatedField5";
        // 
        // calculatedField6
        // 
        this.calculatedField6.DataMember = "CronogramaOrc";
        this.calculatedField6.DisplayName = "cabecalhoAbril";
        this.calculatedField6.Expression = "Iif(IsNullOrEmpty([Plan04]), \'\' ,\'Abril\' )";
        this.calculatedField6.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField6.Name = "calculatedField6";
        // 
        // calculatedField7
        // 
        this.calculatedField7.DataMember = "CronogramaOrc";
        this.calculatedField7.DisplayName = "cabecalhoMaio";
        this.calculatedField7.Expression = "Iif(IsNullOrEmpty([Plan05]), \'\' ,\'Maio\' )";
        this.calculatedField7.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField7.Name = "calculatedField7";
        // 
        // calculatedField8
        // 
        this.calculatedField8.DataMember = "CronogramaOrc";
        this.calculatedField8.DisplayName = "cabecalhoJunho";
        this.calculatedField8.Expression = "Iif(IsNullOrEmpty([Plan06]), \'\' ,\'Junho\' )";
        this.calculatedField8.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField8.Name = "calculatedField8";
        // 
        // calculatedField9
        // 
        this.calculatedField9.DataMember = "CronogramaOrc";
        this.calculatedField9.DisplayName = "cabecalhoJulho";
        this.calculatedField9.Expression = "Iif(IsNullOrEmpty([Plan07]), \'\' ,\'Julho\' )";
        this.calculatedField9.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField9.Name = "calculatedField9";
        // 
        // calculatedField10
        // 
        this.calculatedField10.DataMember = "CronogramaOrc";
        this.calculatedField10.DisplayName = "cabecalhoAgosto";
        this.calculatedField10.Expression = "Iif(IsNullOrEmpty([Plan08]), \'\' ,\'Agosto\' )";
        this.calculatedField10.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField10.Name = "calculatedField10";
        // 
        // calculatedField11
        // 
        this.calculatedField11.DataMember = "CronogramaOrc";
        this.calculatedField11.DisplayName = "cabecalhoSetembro";
        this.calculatedField11.Expression = "Iif(IsNullOrEmpty([Plan09]), \'\' ,\'Setembro\' )";
        this.calculatedField11.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField11.Name = "calculatedField11";
        // 
        // calculatedField12
        // 
        this.calculatedField12.DataMember = "CronogramaOrc";
        this.calculatedField12.DisplayName = "cabecalhoOutubro";
        this.calculatedField12.Expression = "Iif(IsNullOrEmpty([Plan10]), \'\' ,\'Outubro\' )";
        this.calculatedField12.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField12.Name = "calculatedField12";
        // 
        // calculatedField13
        // 
        this.calculatedField13.DataMember = "CronogramaOrc";
        this.calculatedField13.DisplayName = "cabecalhoNovembro";
        this.calculatedField13.Expression = "Iif(IsNullOrEmpty([Plan11]), \'\' ,\'Novembro\' )";
        this.calculatedField13.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField13.Name = "calculatedField13";
        // 
        // calculatedField14
        // 
        this.calculatedField14.DataMember = "CronogramaOrc";
        this.calculatedField14.DisplayName = "cabecalhoDezembro";
        this.calculatedField14.Expression = "Iif(IsNullOrEmpty([Plan12]), \'\' ,\'Dezembro\' )";
        this.calculatedField14.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField14.Name = "calculatedField14";
        // 
        // calculatedField15
        // 
        this.calculatedField15.DataMember = "CronogramaOrc";
        this.calculatedField15.DisplayName = "cabecalhoTotal";
        this.calculatedField15.Expression = "Iif(([Plan01] + [Plan02] + [Plan03] + [Plan04] + [Plan05] + [Plan06] + [Plan07] +" +
            " [Plan08] + [Plan09] + [Plan10] + [Plan11] + [Plan12]) == 0, \'\' , \'Total\')";
        this.calculatedField15.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField15.Name = "calculatedField15";
        // 
        // calculatedField16
        // 
        this.calculatedField16.DataMember = "CronogramaOrc";
        this.calculatedField16.DisplayName = "cabecalhoContaOrc";
        this.calculatedField16.Expression = "Iif(IsNullOrEmpty([CONTA_DES]),\'\',\'Conta Orçamentária\' )";
        this.calculatedField16.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField16.Name = "calculatedField16";
        // 
        // calculatedField17
        // 
        this.calculatedField17.DataMember = "CronogramaOrc";
        this.calculatedField17.DisplayName = "cabecalhoQuantidade";
        this.calculatedField17.Expression = "Iif(IsNullOrEmpty([Quantidade]) Or ([Quantidade]==0),\'\', \'Quantidade\')";
        this.calculatedField17.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField17.Name = "calculatedField17";
        // 
        // calculatedField18
        // 
        this.calculatedField18.DataMember = "CronogramaOrc";
        this.calculatedField18.DisplayName = "cabecalhoValorUnitario";
        this.calculatedField18.Expression = "Iif(IsNullOrEmpty([ValorUnitario]) Or [ValorUnitario] == 0 ,\'\',\'Val. Unit.\')";
        this.calculatedField18.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField18.Name = "calculatedField18";
        // 
        // calculatedField19
        // 
        this.calculatedField19.DataMember = "CronogramaOrc";
        this.calculatedField19.DisplayName = "cabecalhoMemoriaCalculo";
        this.calculatedField19.Expression = "Iif(IsNullOrEmpty([MemoriaCalculo]),\'\',\'Memória de Cálculo\')";
        this.calculatedField19.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField19.Name = "calculatedField19";
        // 
        // calculatedField20
        // 
        this.calculatedField20.DataMember = "CronogramaOrc";
        this.calculatedField20.DisplayName = "descricaoECodigoConta";
        this.calculatedField20.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField20.Name = "calculatedField20";
        // 
        // calculatedField21
        // 
        this.calculatedField21.DataMember = "CronogramaOrc";
        this.calculatedField21.DisplayName = "contaCodEDescricao";
        this.calculatedField21.Expression = "Iif(IsNullOrEmpty([CONTA_COD]) And IsNullOrEmpty( [CONTA_DES]) ,\'\'  , [CONTA_COD]" +
            " + \' - \' + [CONTA_DES])";
        this.calculatedField21.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField21.Name = "calculatedField21";
        // 
        // calculatedField22
        // 
        this.calculatedField22.DataMember = "detalhesProposta";
        this.calculatedField22.DisplayName = "nomeIniciativaComProjeto";
        this.calculatedField22.Expression = "\'Projeto: \' + [NomeIniciativa]";
        this.calculatedField22.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.calculatedField22.Name = "calculatedField22";
        // 
        // calculatedField23
        // 
        this.calculatedField23.DataMember = "CronogramaOrc";
        this.calculatedField23.DisplayName = "somaTotalAcaoAtividade";
        this.calculatedField23.Expression = "Iif( Contains([Numero], \'.\') , [totalAtividade] , [totalAcao])";
        this.calculatedField23.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
        this.calculatedField23.Name = "calculatedField23";
        // 
        // relPropostaIniciativa
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.DetailReport1,
            this.DetailReport2,
            this.DetailReport3,
            this.DetailReport4,
            this.PageHeader,
            this.DetailReport5,
            this.DetailReport6,
            this.PageFooter,
            this.DetailReport11});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.titElementoDeParceriaCalc,
            this.titAreaDeParceriaCalc,
            this.titMetaProduto,
            this.indicaProdutoMeta,
            this.indicaAcaoAtividade,
            this.titMarcoProjeto,
            this.titData,
            this.titLocalEvento,
            this.titDetalhesEvento,
            this.indicaInstitucional,
            this.calculatedField1,
            this.calculatedField2,
            this.fonteRecursoCalculado1,
            this.calculatedField3,
            this.calculatedField4,
            this.calculatedField5,
            this.calculatedField6,
            this.calculatedField7,
            this.calculatedField8,
            this.calculatedField9,
            this.calculatedField10,
            this.calculatedField11,
            this.calculatedField12,
            this.calculatedField13,
            this.calculatedField14,
            this.calculatedField15,
            this.calculatedField16,
            this.calculatedField17,
            this.calculatedField18,
            this.calculatedField19,
            this.calculatedField20,
            this.calculatedField21,
            this.calculatedField22,
            this.calculatedField23});
        this.DataMember = "detalhesProposta";
        this.DataSource = this.dsRelPropostaIniciativa1;
        this.Dpi = 254F;
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(94, 107, 76, 76);
        this.PageHeight = 2159;
        this.PageWidth = 2794;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "12.1";
        ((System.ComponentModel.ISupportInitialize)(this.dsRelPropostaIniciativa1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int codUnidadeNegocio = 0;
        string nomeUnidadeNegocio = "";
        string inicioProposta = "";
        DataSet dsProjeto = cDados.getProjetos(" and p.CodigoProjeto = " + codigoProjetoGlobal);
        if (cDados.DataSetOk(dsProjeto) && cDados.DataTableOk(dsProjeto.Tables[0]))
        {
            int outint = 0;
            if (int.TryParse(dsProjeto.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString(), out outint) == true)
                codUnidadeNegocio = int.Parse(dsProjeto.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());
            inicioProposta = dsProjeto.Tables[0].Rows[0]["InicioProposta"].ToString();
        }

        DataSet dsCodUnidade = cDados.getEntidades(" and e.CodigoUnidadeNegocio = " + codUnidadeNegocio);
        if (cDados.DataSetOk(dsCodUnidade) && cDados.DataTableOk(dsCodUnidade.Tables[0]))
        {
            nomeUnidadeNegocio = dsProjeto.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        }
        //lblCabecalho.Text = string.Format("SESCOOP - Proposta de impressão do TAI elaborada em 11/10/2012", nomeUnidadeNegocio, inicioProposta);
        lblCabecalho.Text = "Impressão do TAI";
    }

    private void lblTipoDeProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (lblTipoDeProjeto.Text == "FI")
        {
            lblTipoDeProjeto.Text = "Finalístico";
        }
        if (lblTipoDeProjeto.Text == "AA")
        {
            lblTipoDeProjeto.Text = "Administração e Apoio";
        }
    }

    private void lblFonteRecurso_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (lblFonteRecurso.Text == "FU")
        {
            lblFonteRecurso.Text = "FUNDECOOP";
        }
        if (lblFonteRecurso.Text == "RP")
        {
            lblFonteRecurso.Text = "Recursos Próprios";
        }
        if (lblFonteRecurso.Text == "AO")
        {
            lblFonteRecurso.Text = "FUNDECOOP e Recursos Próprios";
        }
    }

    private void lblAreaAtuacao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        if (lblAreaAtuacao.Text == "FO")
        {
            lblAreaAtuacao.Text = "Formação e Capacitação Profissional";
        }
        if (lblAreaAtuacao.Text == "MO")
        {
            lblAreaAtuacao.Text = "Monitoramento e Desenvolvimento de Cooperativas";
        }
        if (lblAreaAtuacao.Text == "PR")
        {
            lblAreaAtuacao.Text = "Promoção Social";
        }
        if (lblAreaAtuacao.Text == "GE")
        {
            lblAreaAtuacao.Text = "Gestão Interna do Sistema";
        }

    }

    private void lblIndicaClassificacaoProjeto_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (lblIndicaClassificacaoProjeto.Text == "NO")
        {
            lblIndicaClassificacaoProjeto.Text = "Projeto Novo";
        }
        if (lblIndicaClassificacaoProjeto.Text == "CO")
        {
            lblIndicaClassificacaoProjeto.Text = "Continuação de Projeto";
        }
    }

    private void lblAreaAtuacao1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (lblAreaAtuacao1.Text == "FO")
        {
            lblAreaAtuacao1.Text = "Formação e Capacitação Profissional";
        }
        if (lblAreaAtuacao1.Text == "MO")
        {
            lblAreaAtuacao1.Text = "Monitoramento e Desenvolvimento de Cooperativas";
        }
        if (lblAreaAtuacao1.Text == "PR")
        {
            lblAreaAtuacao1.Text = "Promoção Social";
        }
        if (lblAreaAtuacao1.Text == "GE")
        {
            lblAreaAtuacao1.Text = "Gestão Interna do Sistema";
        }
    }

    private void celFonteRecurso_TextChanged(object sender, EventArgs e)
    {
        if (celFonteRecurso.Text == "FU")
        {
            celFonteRecurso.Text = "FUNDECOOP";
        }
        if (celFonteRecurso.Text == "RP")
        {
            celFonteRecurso.Text = "Recursos Próprios";
        }
        if (celFonteRecurso.Text == "AO")
        {
            celFonteRecurso.Text = "FUNDECOOP e Recursos Próprios";
        }
        if (celFonteRecurso.Text == "SR")
        {
            celFonteRecurso.Text = "Sem fonte de recursos";
        }
    }

    private void celNumeroAcao_TextChanged(object sender, EventArgs e)
    {

        if (celNumeroAcao.Text.IndexOf(".") != -1)//achou ponto
        {
            //é uma atividade
            xrTable13.Visible = true;
            xrTable14.Visible = true;
            xrTable16.Visible = true;
            xrTable15.Visible = true;
        }
        else
        {
            //é uma ação
            xrTable13.Visible = false;
            xrTable14.Visible = false;
            xrTable16.Visible = false;
            xrTable15.Visible = false;
        }

        /*     
*/
    }

    private void xrTable12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void celFonteRecurso2_TextChanged(object sender, EventArgs e)
    {
        if (celFonteRecurso2.Text == "FU")
        {
            celFonteRecurso2.Text = "FUNDECOOP";
        }
        if (celFonteRecurso2.Text == "RP")
        {
            celFonteRecurso2.Text = "Recursos Próprios";
        }
        if (celFonteRecurso2.Text == "AO")
        {
            celFonteRecurso2.Text = "FUNDECOOP e Recursos Próprios";
        }
        if (celFonteRecurso2.Text == "SR")
        {
            celFonteRecurso2.Text = "Sem fonte de recursos";
        }
    }

    private void xrTableCell95_TextChanged(object sender, EventArgs e)
    {
        if (xrTableCell95.Text.IndexOf(".") != -1)//achou ponto
        {
            //é uma atividade
            xrTable12.Visible = true;
            xrTableCell95.BackColor = Color.LightGray;
            celFonteRecurso2.BackColor = Color.LightGray;
            celTotal.BackColor = Color.LightGray;

        }
        else
        {
            //é uma ação
            xrTable12.Visible = false;
            xrTableCell95.BackColor = Color.DarkGray;
            celFonteRecurso2.BackColor = Color.DarkGray;
            celTotal.BackColor = Color.DarkGray;
        }
    }

    private void xrLabel34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = totalFundecoop;
        e.Handled = true;
    }

    private void xrLabel34_SummaryCalculated(object sender, TextFormatEventArgs e)
    {
        //string fonteRecurso = (string)DetailReport11.GetCurrentColumnValue("FonteRecurso");
        //if (fonteRecurso == "FU" && !Convert.IsDBNull(e.Value))
        //    totalFundecoop += Convert.ToDecimal(e.Value);
        //xrLabel34.Text = totalFundecoop.ToString();
    }

    private void xrLabel34_SummaryRowChanged(object sender, EventArgs e)
    {
        XRLabel lbl = (XRLabel)sender;
        string fonteRecurso1 = (string)lbl.Report.GetCurrentColumnValue("FonteRecurso");
        if (fonteRecurso1 == "FU" && !Convert.IsDBNull(lbl.Report.GetCurrentColumnValue("ValorTotal")))
            totalFundecoop += Convert.ToDecimal(lbl.Report.GetCurrentColumnValue("ValorTotal"));
    }

    private void xrLabel32_SummaryRowChanged(object sender, EventArgs e)
    {
        XRLabel lbl = (XRLabel)sender;
        string fonteRecurso1 = (string)lbl.Report.GetCurrentColumnValue("FonteRecurso");
        if (fonteRecurso1 == "RP" && !Convert.IsDBNull(lbl.Report.GetCurrentColumnValue("ValorTotal")))
            totalUN += Convert.ToDecimal(lbl.Report.GetCurrentColumnValue("ValorTotal"));
    }

    private void xrLabel32_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        e.Result = totalUN;
        e.Handled = true;
    }

    private void celTotal_SummaryRowChanged(object sender, EventArgs e)
    {
        XRLabel lbl = (XRLabel)sender;
        string fonteRecurso1 = (string)lbl.Report.GetCurrentColumnValue("FonteRecurso");
        if (fonteRecurso1 == "FU" && !Convert.IsDBNull(lbl.Report.GetCurrentColumnValue("ValorTotal")))
            totalFundecoop += Convert.ToDecimal(lbl.Report.GetCurrentColumnValue("ValorTotal"));
    }


}
