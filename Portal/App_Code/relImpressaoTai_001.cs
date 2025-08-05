using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary> 
/// Summary description for relPropostaIniciativa
/// </summary>
public class relImpressaoTai_001 : DevExpress.XtraReports.UI.XtraReport
{
    #region FIELDS
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
    private XRLabel lblAreaAtuacao1;
    private XRLabel xrLabel31;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel xrLabel28;
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
    private CalculatedField indicaProdutoMeta;
    private CalculatedField indicaAcaoAtividade;
    private CalculatedField titMarcoProjeto;
    private CalculatedField titData;
    private XRLine xrLine2;
    private CalculatedField titLocalEvento;
    private CalculatedField titDetalhesEvento;
    private CalculatedField indicaInstitucional;
    private DetailReportBand DetailReport11;
    private DetailBand Detail12;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable17;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell95;
    private XRLabel xrLabel9;
    private ReportFooterBand ReportFooter2;
    private XRLabel xrLabel25;
    private XRLabel lblTotalGeralEtapaOrcMenorQue2;
    private XRLabel xrLabel35;
    private XRLabel lblTotalFundecoopEtapaOrcMenorQue2;
    private XRLabel lblTotalUNEtapaOrcMenorQue2ValProp;
    private XRLabel xrLabel27;
    private XRLine xrLine3;
    private CalculatedField nomeIniciativaComProjeto;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private decimal totalUN = 0;
    private GroupHeaderBand GroupHeader3;
    private GroupHeaderBand GroupHeader4;
    private GroupHeaderBand GroupHeader5;
    private GroupHeaderBand GroupHeader6;
    private GroupHeaderBand GroupHeader9;
    private GroupHeaderBand GroupHeader10;
    private GroupHeaderBand GroupHeader7;
    private GroupHeaderBand GroupHeader11;
    private XRSubreport xrSubreport1;
    private XRTable tbDadosEvento;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell5;
    private XRTable tbEventoDetalhe;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell22;
    private XRLabel celulaTituloProduto;
    private DetailReportBand DetailReport7;
    private DetailBand Detail8;
    private XRTable xrTable20;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell9;
    private GroupHeaderBand GroupHeader8;
    private XRLabel lblMeta;
    private XRTable xrTable8;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell11;
    private XRLine xrLine1;
    private XRPageInfo xrPageInfo2;
    private XRLabel lblResultadosContinuidade;
    private XRLabel xrLabel13;
    private GroupHeaderBand GroupHeader12;
    private GroupHeaderBand GroupHeader13;
    private GroupHeaderBand GroupHeader14;
    private GroupHeaderBand GroupHeader15;
    private GroupHeaderBand GroupHeader16;
    private XRLine xrLine7;
    private XRLine xrLine6;
    private XRTable xrTable10;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell28;
    private DetailReportBand DetailReport10;
    private DetailBand Detail11;
    private XRTable xrTable22;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable19;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell46;
    private GroupHeaderBand GroupHeader17;
    private XRLabel lblTotalUNDispReformulada;
    private XRLabel xrLabel39;
    private XRTable xrTable18;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell38;
    private XRLabel xrLabel41;
    private XRLabel xrLabel36;
    private XRLabel lblTotalGeralDispRefEtapaOrcMaiorQue1;
    private XRLabel xrLabel38;
    private XRLabel lblTotalFUNDDispRefEtapaOrMairQue1;
    private ReportFooterBand ReportFooter3;
    private XRLine xrLine4;
    private GroupFooterBand GroupFooter2;
    private XRTable xrTable24;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell76;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable23;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRTableCell xrTableCell74;
    private GroupHeaderBand GroupHeader18;
    private XRTable xrTable21;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell61;
    private GroupHeaderBand GroupHeader19;
    private XRTable xrTable12;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell42;
    private decimal totalFundecoop = 0;

    #endregion

    public relImpressaoTai_001(int codigoProjeto)
    {
        InitializeComponent();
        codigoProjetoGlobal = codigoProjeto;
        DefineLogoEntidade();
        InitData(codigoProjeto);
        xrSubreport1.ReportSource = new subReportProduto_Tai001(codigoProjetoGlobal);
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
            SELECT 
               
              ROW_NUMBER() OVER (partition by x.codigoprojeto order by x.ordem ) AS seq,
              x.CodigoProjeto,
              x.CodigoAcao AS Codigo, 
              x.CodigoAcaoSuperior AS CodigoPai,
              numero, 
              descricao, 
              inicio, 
              termino,
              Institucional,
              x.CodigoUsuarioResponsavel,
              x.NomeUsuarioResponsavel AS Responsavel,
              x.FonteRecurso ,
              x.LocalEvento,
              x.DetalhesEvento
              FROM (SELECT t1.codigoprojeto, 
                           t1.CodigoAcao , 
                           t1.CodigoAcaoSuperior,
                           t1.NomeUsuarioResponsavel , 
                           CASE WHEN t1.IndicaSemRecurso = 'S' THEN 'SR'
                           ELSE  t1.FonteRecurso
                           END FonteRecurso,
                           t1.LocalEvento ,
                           t1.DetalhesEvento,
                           CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior 
                                THEN RIGHT('0000'+CONVERT(VARCHAR, t1.NumeroAcao),4)
                                ELSE RIGHT('0000'+CONVERT(VARCHAR, tSup.NumeroAcao),4) + '.' + RIGHT('0000'+CONVERT(VARCHAR, t1.NumeroAcao),4) 
                           END AS ordem, 
                           ROW_NUMBER() OVER (partition by t1.codigoAcaoSuperior order by t1.codigoacao) AS linha,
                           CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior 
                                THEN CONVERT(VARCHAR, t1.NumeroAcao)
                                ELSE CONVERT(VARCHAR, tSup.NumeroAcao) + '.' + CONVERT(VARCHAR, t1.NumeroAcao) 
                           END AS Numero,
                           CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior 
                                THEN CONVERT(VARCHAR, t1.NumeroAcao) +' '+t1.[NomeAcao] 
                                ELSE CONVERT(VARCHAR, tSup.NumeroAcao) + '.' + CONVERT(VARCHAR, t1.NumeroAcao) +' '+t1.[NomeAcao] 
                           END AS Descricao, 
                           t1.Inicio, 
                           t1.Termino ,
                           CASE WHEN t1.CodigoAcao = t1.CodigoAcaoSuperior THEN ' - '
                                ELSE t1.IndicaEventoInstitucional 
                           END AS Institucional,
                           t1.CodigoUsuarioResponsavel
                      FROM {0}.{1}.tai02_AcoesIniciativa t1 INNER JOIN
                           {0}.{1}.tai02_AcoesIniciativa tSup ON tSup.CodigoAcao = t1.CodigoAcaoSuperior
                     WHERE t1.CodigoProjeto = @CodigoProjeto) x
                     ORDER BY  x.ordem

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
                ISNULL((select sum(isnull(valorProposto,0)) 
                        from {0}.{1}.CronogramaOrcamentarioAcao soma 
                        where soma.CodigoProjeto = resultado.CodigoProjeto ),0) totalProjeto,                                   
                ISNULL((select sum(isnull(valorProposto,0)) 
                        from   {0}.{1}.CronogramaOrcamentarioAcao soma inner join
                        {0}.{1}.tai02_AcoesIniciativa ai on (ai.CodigoProjeto = soma.CodigoProjeto and ai.CodigoAcao = soma.CodigoAcao)
                        where soma.CodigoProjeto = resultado.CodigoProjeto 
                        and ai.CodigoAcaoSuperior = resultado.codigoPai),0) totalAcao,
                ISNULL((select sum(isnull(DisponibilidadeReformulada,0)) 
                        from   {0}.{1}.CronogramaOrcamentarioAcao soma inner join
                        {0}.{1}.tai02_AcoesIniciativa ai on (ai.CodigoProjeto = soma.CodigoProjeto and ai.CodigoAcao = soma.CodigoAcao)
                        where soma.CodigoProjeto = resultado.CodigoProjeto 
                        and ai.CodigoAcaoSuperior = resultado.codigoPai),0) totalReformulacao,
                        
                ISNULL((select sum(isnull(DisponibilidadeReformulada,0)) 
                        from {0}.{1}.CronogramaOrcamentarioAcao soma 
                        where soma.CodigoProjeto = resultado.CodigoProjeto ),0)  totalProjetoReformulacao  


          
            from (  
            SELECT t1.CodigoProjeto,
            t1.CodigoAcao AS Codigo, 
            t1.CodigoAcaoSuperior AS CodigoPai,
            t1.NomeAcao AS DescricaoAcao,
            t1.FonteRecurso,
            co.SeqPlanoContas, 
            co.Quantidade Quantidade, 
            co.ValorUnitario ValorUnitario,
            co.ValorProposto ValorProposto, 
            co.MemoriaCalculo,
            co.ValorRealizado,
            co.ValorSuplemento,
            co.ValorTransposto,
            co.DisponibilidadeAtual,
            co.DisponibilidadeReformulada,
            opc.CONTA_DES, 
            opc.CONTA_COD,
            tSup.NumeroAcao acaoSup,
            t1.NumeroAcao ,
            ta.EtapaOrcamento,            
            right('0000'+CONVERT(VARCHAR, t1.NumeroAcao),4) AS ordem,
            case when (t1.FonteRecurso = 'FU') THEN 'Fundecoop' 
                 when (t1.FonteRecurso = 'RP') THEN 'Unidade Nacional'
                 else 'Sem recurso'
            end as fonte
            FROM  {0}.{1}.tai02_AcoesIniciativa t1 INNER JOIN 
                             {0}.{1}.TermoAbertura02 ta on ta.CodigoProjeto = t1.CodigoProjeto LEFT JOIN
                             {0}.{1}.tai02_AcoesIniciativa tSup ON tSup.CodigoAcao = t1.CodigoAcaoSuperior and tSup.CodigoProjeto = t1.CodigoProjeto left JOIN
                             {0}.{1}.CronogramaOrcamentarioAcao co ON (co.CodigoAcao = t1.CodigoAcao and t1.CodigoProjeto = co.CodigoProjeto) left join
                             {0}.{1}.orc_planoContas opc ON opc.SeqPlanoContas = co.SeqPlanoContas
            WHERE t1.CodigoProjeto = @CodigoProjeto
              and t1.CodigoAcao = t1.CodigoAcaoSuperior
              and t1.FonteRecurso <> 'SR'
            ) resultado        
            ORDER BY resultado.ordem, resultado.CONTA_DES
end", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);
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
        //string resourceFileName = "relImpressaoTai_001.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
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
        this.lblResultadosContinuidade = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblIndicaClassificacaoProjeto = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader12 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader13 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
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
        this.GroupHeader14 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLine7 = new DevExpress.XtraReports.UI.XRLine();
        this.lblAreaAtuacao1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader15 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
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
        this.GroupHeader16 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblCabecalho = new DevExpress.XtraReports.UI.XRLabel();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        this.DetailReport6 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
        this.tbDadosEvento = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.tbEventoDetalhe = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
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
        this.celulaTituloProduto = new DevExpress.XtraReports.UI.XRLabel();
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
        this.DetailReport7 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail8 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable20 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader8 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblMeta = new DevExpress.XtraReports.UI.XRLabel();
        this.titElementoDeParceriaCalc = new DevExpress.XtraReports.UI.CalculatedField();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.titAreaDeParceriaCalc = new DevExpress.XtraReports.UI.CalculatedField();
        this.indicaProdutoMeta = new DevExpress.XtraReports.UI.CalculatedField();
        this.indicaAcaoAtividade = new DevExpress.XtraReports.UI.CalculatedField();
        this.titMarcoProjeto = new DevExpress.XtraReports.UI.CalculatedField();
        this.titData = new DevExpress.XtraReports.UI.CalculatedField();
        this.titLocalEvento = new DevExpress.XtraReports.UI.CalculatedField();
        this.titDetalhesEvento = new DevExpress.XtraReports.UI.CalculatedField();
        this.indicaInstitucional = new DevExpress.XtraReports.UI.CalculatedField();
        this.DetailReport11 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail12 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportFooter2 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupHeader11 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTotalUNEtapaOrcMenorQue2ValProp = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTotalFundecoopEtapaOrcMenorQue2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTotalGeralEtapaOrcMenorQue2 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable24 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader19 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.nomeIniciativaComProjeto = new DevExpress.XtraReports.UI.CalculatedField();
        this.DetailReport10 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail11 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable22 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable19 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader17 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.lblTotalUNDispReformulada = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable18 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblTotalFUNDDispRefEtapaOrMairQue1 = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportFooter3 = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable23 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader18 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable21 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.dsRelPropostaIniciativa1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbDadosEvento)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbEventoDetalhe)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 174.21F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(2539.25F, 20.87503F);
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.BorderColor = System.Drawing.Color.DarkOliveGreen;
        this.xrPageInfo2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPageInfo2.Dpi = 254F;
        this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic);
        this.xrPageInfo2.Format = "Emitido em {0:dd/MM/yyyy - HH:mm}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1285.778F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(1254.191F, 58.41999F);
        this.xrPageInfo2.StylePriority.UseBorderColor = false;
        this.xrPageInfo2.StylePriority.UseBorders = false;
        this.xrPageInfo2.StylePriority.UseFont = false;
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
        this.Detail.Expanded = false;
        this.Detail.HeightF = 900.4992F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblAreaAtuacao
        // 
        this.lblAreaAtuacao.BorderColor = System.Drawing.Color.DarkGray;
        this.lblAreaAtuacao.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblAreaAtuacao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.NomeUnidadeNegocio")});
        this.lblAreaAtuacao.Dpi = 254F;
        this.lblAreaAtuacao.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblAreaAtuacao.LocationFloat = new DevExpress.Utils.PointFloat(1081.123F, 420.2759F);
        this.lblAreaAtuacao.Name = "lblAreaAtuacao";
        this.lblAreaAtuacao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblAreaAtuacao.SizeF = new System.Drawing.SizeF(1459.127F, 62.49115F);
        this.lblAreaAtuacao.StylePriority.UseBorderColor = false;
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
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(1079.944F, 360.2759F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(444.1875F, 60F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "Área Responsável:";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel18
        // 
        this.xrLabel18.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.Beneficiarios")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(2.384356F, 781.7285F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(2539.969F, 63.20813F);
        this.xrLabel18.StylePriority.UseBorderColor = false;
        this.xrLabel18.StylePriority.UseBorders = false;
        this.xrLabel18.StylePriority.UseFont = false;
        this.xrLabel18.Text = "xrLabel18";
        // 
        // xrLabel17
        // 
        this.xrLabel17.CanShrink = true;
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(2.384383F, 723.3085F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(304.2708F, 58.41992F);
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
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(0F, 540.7081F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(304.2708F, 60F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.StylePriority.UseTextAlignment = false;
        this.xrLabel16.Text = "Público alvo:";
        this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel15
        // 
        this.xrLabel15.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.PublicoAlvo")});
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 600.7081F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(2539.969F, 54.45099F);
        this.xrLabel15.StylePriority.UseBorderColor = false;
        this.xrLabel15.StylePriority.UseBorders = false;
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "xrLabel15";
        // 
        // xrLabel12
        // 
        this.xrLabel12.CanShrink = true;
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 360.2759F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(1051.828F, 60F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "Coordenador do Projeto:";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel11
        // 
        this.xrLabel11.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.NomeGerenteIniciativa")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 420.2759F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(1051.828F, 62.49115F);
        this.xrLabel11.StylePriority.UseBorderColor = false;
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "[NomeGerenteIniciativa]";
        // 
        // xrLabel10
        // 
        this.xrLabel10.CanShrink = true;
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(1078.944F, 181.1317F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(826.7058F, 60.72809F);
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.Text = "Fonte de Recurso:";
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // lblFonteRecurso
        // 
        this.lblFonteRecurso.BorderColor = System.Drawing.Color.DarkGray;
        this.lblFonteRecurso.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblFonteRecurso.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.FonteRecurso")});
        this.lblFonteRecurso.Dpi = 254F;
        this.lblFonteRecurso.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblFonteRecurso.LocationFloat = new DevExpress.Utils.PointFloat(1078.944F, 241.8598F);
        this.lblFonteRecurso.Name = "lblFonteRecurso";
        this.lblFonteRecurso.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblFonteRecurso.SizeF = new System.Drawing.SizeF(826.5276F, 54F);
        this.lblFonteRecurso.StylePriority.UseBorderColor = false;
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
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 181.1317F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(1050.828F, 60.72809F);
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
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(2222.358F, 181.1317F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(316.8916F, 60.72809F);
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
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1925.725F, 181.1317F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(254F, 60.72809F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.StylePriority.UseTextAlignment = false;
        this.xrLabel6.Text = "Data Início:";
        this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel5
        // 
        this.xrLabel5.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.DataTermino", "{0:dd/MM/yyyy}")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(2223.64F, 241.8598F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(315.6096F, 54F);
        this.xrLabel5.StylePriority.UseBorderColor = false;
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrLabel4
        // 
        this.xrLabel4.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.DataInicio", "{0:dd/MM/yyyy}")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1925.725F, 241.8598F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(254F, 54F);
        this.xrLabel4.StylePriority.UseBorderColor = false;
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // lblTipoDeProjeto
        // 
        this.lblTipoDeProjeto.BorderColor = System.Drawing.Color.DarkGray;
        this.lblTipoDeProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTipoDeProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.IndicaTipoProjeto")});
        this.lblTipoDeProjeto.Dpi = 254F;
        this.lblTipoDeProjeto.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTipoDeProjeto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 241.8598F);
        this.lblTipoDeProjeto.Name = "lblTipoDeProjeto";
        this.lblTipoDeProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTipoDeProjeto.SizeF = new System.Drawing.SizeF(1049.444F, 54F);
        this.lblTipoDeProjeto.StylePriority.UseBorderColor = false;
        this.lblTipoDeProjeto.StylePriority.UseBorders = false;
        this.lblTipoDeProjeto.StylePriority.UseFont = false;
        this.lblTipoDeProjeto.Text = "[IndicaTipoProjeto]";
        this.lblTipoDeProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblTipoDeProjeto_BeforePrint);
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(2539.25F, 58.42F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "Nome do Projeto";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.NomeIniciativa")});
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58.42F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrLabel1.StylePriority.UseBorderColor = false;
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "[NomeIniciativa]";
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 10F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.HeightF = 10F;
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
            this.GroupHeader12});
        this.DetailReport.DataMember = "equipeApoio";
        this.DetailReport.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Expanded = false;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 60F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable1
        // 
        this.xrTable1.BorderColor = System.Drawing.Color.DarkGray;
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
        this.xrTable1.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrTable1.StylePriority.UseBorderColor = false;
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
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
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
            this.lblResultadosContinuidade,
            this.xrLabel13,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel20,
            this.lblIndicaClassificacaoProjeto});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 597.5519F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // lblResultadosContinuidade
        // 
        this.lblResultadosContinuidade.BorderColor = System.Drawing.Color.DarkGray;
        this.lblResultadosContinuidade.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblResultadosContinuidade.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.UltimosResultados")});
        this.lblResultadosContinuidade.Dpi = 254F;
        this.lblResultadosContinuidade.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblResultadosContinuidade.LocationFloat = new DevExpress.Utils.PointFloat(0F, 294.4571F);
        this.lblResultadosContinuidade.Name = "lblResultadosContinuidade";
        this.lblResultadosContinuidade.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblResultadosContinuidade.SizeF = new System.Drawing.SizeF(2539.969F, 67.05551F);
        this.lblResultadosContinuidade.StylePriority.UseBorderColor = false;
        this.lblResultadosContinuidade.StylePriority.UseBorders = false;
        this.lblResultadosContinuidade.StylePriority.UseFont = false;
        // 
        // xrLabel13
        // 
        this.xrLabel13.CanShrink = true;
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 239.7487F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(1191.63F, 54.70834F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.StylePriority.UseTextAlignment = false;
        this.xrLabel13.Text = "Em caso de continuidade, descreva os últimos resultados";
        this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel23
        // 
        this.xrLabel23.CanShrink = true;
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(0F, 422.3747F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(1076.854F, 60F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.StylePriority.UseTextAlignment = false;
        this.xrLabel23.Text = "Justificativa:";
        this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrLabel22
        // 
        this.xrLabel22.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.Justificativa")});
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 482.3747F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(2539.969F, 58.2916F);
        this.xrLabel22.StylePriority.UseBorderColor = false;
        this.xrLabel22.StylePriority.UseBorders = false;
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.Text = "xrLabel22";
        // 
        // xrLabel20
        // 
        this.xrLabel20.CanShrink = true;
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 59.83203F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(1076.854F, 60F);
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.StylePriority.UseTextAlignment = false;
        this.xrLabel20.Text = "Projeto novo ou continuidade:";
        this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // lblIndicaClassificacaoProjeto
        // 
        this.lblIndicaClassificacaoProjeto.BorderColor = System.Drawing.Color.DarkGray;
        this.lblIndicaClassificacaoProjeto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblIndicaClassificacaoProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.IndicaClassificacaoProjeto")});
        this.lblIndicaClassificacaoProjeto.Dpi = 254F;
        this.lblIndicaClassificacaoProjeto.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblIndicaClassificacaoProjeto.LocationFloat = new DevExpress.Utils.PointFloat(0F, 119.832F);
        this.lblIndicaClassificacaoProjeto.Name = "lblIndicaClassificacaoProjeto";
        this.lblIndicaClassificacaoProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblIndicaClassificacaoProjeto.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.lblIndicaClassificacaoProjeto.StylePriority.UseBorderColor = false;
        this.lblIndicaClassificacaoProjeto.StylePriority.UseBorders = false;
        this.lblIndicaClassificacaoProjeto.StylePriority.UseFont = false;
        this.lblIndicaClassificacaoProjeto.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblIndicaClassificacaoProjeto_BeforePrint);
        // 
        // GroupHeader12
        // 
        this.GroupHeader12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel19});
        this.GroupHeader12.Dpi = 254F;
        this.GroupHeader12.HeightF = 60F;
        this.GroupHeader12.Name = "GroupHeader12";
        // 
        // xrLabel19
        // 
        this.xrLabel19.CanShrink = true;
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(2.384356F, 0F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(346.6042F, 60F);
        this.xrLabel19.StylePriority.UseFont = false;
        this.xrLabel19.StylePriority.UseTextAlignment = false;
        this.xrLabel19.Text = "Equipe de apoio:";
        this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader3,
            this.GroupHeader13});
        this.DetailReport1.DataMember = "ObjetivosEstrategicos";
        this.DetailReport1.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Expanded = false;
        this.DetailReport1.Level = 1;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 60F;
        this.Detail2.Name = "Detail2";
        // 
        // xrTable2
        // 
        this.xrTable2.BorderColor = System.Drawing.Color.DarkGray;
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
        this.xrTable2.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrTable2.StylePriority.UseBorderColor = false;
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
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.CanShrink = true;
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ObjetivosEstrategicos.DescricaoObjetoEstrategia")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UsePadding = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 0.73058176846807854D;
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
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell6.Weight = 0.54062956081523883D;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.GroupHeader3.Dpi = 254F;
        this.GroupHeader3.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader3.HeightF = 60F;
        this.GroupHeader3.Name = "GroupHeader3";
        this.GroupHeader3.RepeatEveryPage = true;
        this.GroupHeader3.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        // 
        // xrTable3
        // 
        this.xrTable3.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable3.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Dpi = 254F;
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.KeepTogether = true;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
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
        this.xrTableCell7.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell7.CanShrink = true;
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell7.StylePriority.UseBackColor = false;
        this.xrTableCell7.StylePriority.UsePadding = false;
        this.xrTableCell7.Text = "Objetivo Estratégico";
        this.xrTableCell7.Weight = 0.73058176846807854D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell8.CanShrink = true;
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell8.StylePriority.UseBackColor = false;
        this.xrTableCell8.StylePriority.UsePadding = false;
        this.xrTableCell8.Text = "Indicador";
        this.xrTableCell8.Weight = 0.54062956081523883D;
        // 
        // GroupHeader13
        // 
        this.GroupHeader13.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24});
        this.GroupHeader13.Dpi = 254F;
        this.GroupHeader13.HeightF = 149.0486F;
        this.GroupHeader13.Level = 1;
        this.GroupHeader13.Name = "GroupHeader13";
        // 
        // xrLabel24
        // 
        this.xrLabel24.BackColor = System.Drawing.Color.Gray;
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 39.55217F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrLabel24.StylePriority.UseBackColor = false;
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.StylePriority.UseTextAlignment = false;
        this.xrLabel24.Text = "Alinhamento Estratégico";
        this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader4,
            this.GroupHeader14});
        this.DetailReport2.DataMember = "LinhaDeAcao";
        this.DetailReport2.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Expanded = false;
        this.DetailReport2.Level = 2;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 58.80523F;
        this.Detail3.Name = "Detail3";
        // 
        // xrTable4
        // 
        this.xrTable4.BorderColor = System.Drawing.Color.DarkGray;
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
        this.xrTable4.SizeF = new System.Drawing.SizeF(2539.969F, 58.80523F);
        this.xrTable4.StylePriority.UseBorderColor = false;
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
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.CanShrink = true;
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LinhaDeAcao.DescricaoObjetoEstrategia")});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UsePadding = false;
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 0.76103196180554067D;
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
        this.xrTableCell12.Weight = 0.56418542949880723D;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.GroupHeader4.Dpi = 254F;
        this.GroupHeader4.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader4.HeightF = 50.4986F;
        this.GroupHeader4.Name = "GroupHeader4";
        this.GroupHeader4.RepeatEveryPage = true;
        // 
        // xrTable5
        // 
        this.xrTable5.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable5.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.Dpi = 254F;
        this.xrTable5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable5.KeepTogether = true;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(2539.969F, 50.4986F);
        this.xrTable5.StylePriority.UseBackColor = false;
        this.xrTable5.StylePriority.UseBorderColor = false;
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
        this.xrTableCell13.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell13.CanShrink = true;
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell13.StylePriority.UseBackColor = false;
        this.xrTableCell13.StylePriority.UsePadding = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "Objetivo Estratégico";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell13.Weight = 0.76103214523531937D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell14.CanShrink = true;
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell14.StylePriority.UseBackColor = false;
        this.xrTableCell14.StylePriority.UsePadding = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "Linha de ação";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell14.Weight = 0.56418524606902865D;
        // 
        // GroupHeader14
        // 
        this.GroupHeader14.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine7});
        this.GroupHeader14.Dpi = 254F;
        this.GroupHeader14.HeightF = 58.42F;
        this.GroupHeader14.Level = 1;
        this.GroupHeader14.Name = "GroupHeader14";
        // 
        // xrLine7
        // 
        this.xrLine7.Dpi = 254F;
        this.xrLine7.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine7.LineWidth = 3;
        this.xrLine7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine7.Name = "xrLine7";
        this.xrLine7.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLine7.StylePriority.UseForeColor = false;
        // 
        // lblAreaAtuacao1
        // 
        this.lblAreaAtuacao1.BorderColor = System.Drawing.Color.DarkGray;
        this.lblAreaAtuacao1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblAreaAtuacao1.CanShrink = true;
        this.lblAreaAtuacao1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.IndicaAreaAtuacao")});
        this.lblAreaAtuacao1.Dpi = 254F;
        this.lblAreaAtuacao1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblAreaAtuacao1.KeepTogether = true;
        this.lblAreaAtuacao1.LocationFloat = new DevExpress.Utils.PointFloat(0.07686869F, 120.9958F);
        this.lblAreaAtuacao1.Name = "lblAreaAtuacao1";
        this.lblAreaAtuacao1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblAreaAtuacao1.SizeF = new System.Drawing.SizeF(2539.173F, 63.31578F);
        this.lblAreaAtuacao1.StylePriority.UseBorderColor = false;
        this.lblAreaAtuacao1.StylePriority.UseBorders = false;
        this.lblAreaAtuacao1.StylePriority.UseFont = false;
        this.lblAreaAtuacao1.StylePriority.UseTextAlignment = false;
        this.lblAreaAtuacao1.Text = "lblAreaAtuacao1";
        this.lblAreaAtuacao1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lblAreaAtuacao1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.lblAreaAtuacao1_BeforePrint);
        // 
        // xrLabel26
        // 
        this.xrLabel26.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel26.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel26.CanShrink = true;
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(0.07686869F, 63.5F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(2539.173F, 57.49581F);
        this.xrLabel26.StylePriority.UseBackColor = false;
        this.xrLabel26.StylePriority.UseBorderColor = false;
        this.xrLabel26.StylePriority.UseBorders = false;
        this.xrLabel26.StylePriority.UseFont = false;
        this.xrLabel26.StylePriority.UseTextAlignment = false;
        this.xrLabel26.Text = "Área de atuação";
        this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // DetailReport3
        // 
        this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupHeader5,
            this.GroupHeader15});
        this.DetailReport3.DataMember = "ResultadosIniciativa";
        this.DetailReport3.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport3.Dpi = 254F;
        this.DetailReport3.Expanded = false;
        this.DetailReport3.Level = 4;
        this.DetailReport3.Name = "DetailReport3";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.Detail4.Dpi = 254F;
        this.Detail4.HeightF = 60F;
        this.Detail4.Name = "Detail4";
        // 
        // xrTable6
        // 
        this.xrTable6.BorderColor = System.Drawing.Color.DarkGray;
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
        this.xrTable6.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrTable6.StylePriority.UseBorderColor = false;
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
        this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell30.CanShrink = true;
        this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadosIniciativa.NomeIndicador")});
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UsePadding = false;
        this.xrTableCell30.Text = "xrTableCell30";
        this.xrTableCell30.Weight = 5.89110138994077D;
        // 
        // GroupHeader5
        // 
        this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel31});
        this.GroupHeader5.Dpi = 254F;
        this.GroupHeader5.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader5.HeightF = 60F;
        this.GroupHeader5.Name = "GroupHeader5";
        this.GroupHeader5.RepeatEveryPage = true;
        // 
        // xrLabel31
        // 
        this.xrLabel31.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel31.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel31.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel31.CanShrink = true;
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrLabel31.StylePriority.UseBackColor = false;
        this.xrLabel31.StylePriority.UseBorderColor = false;
        this.xrLabel31.StylePriority.UseBorders = false;
        this.xrLabel31.StylePriority.UseFont = false;
        this.xrLabel31.StylePriority.UseTextAlignment = false;
        this.xrLabel31.Text = "Indicadores de Desempenho";
        this.xrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // GroupHeader15
        // 
        this.GroupHeader15.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel28,
            this.xrLabel29,
            this.xrLabel30});
        this.GroupHeader15.Dpi = 254F;
        this.GroupHeader15.HeightF = 368.3333F;
        this.GroupHeader15.Level = 1;
        this.GroupHeader15.Name = "GroupHeader15";
        // 
        // xrLabel28
        // 
        this.xrLabel28.BackColor = System.Drawing.Color.Gray;
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(0F, 68.6909F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(2539.969F, 58.41999F);
        this.xrLabel28.StylePriority.UseBackColor = false;
        this.xrLabel28.StylePriority.UseFont = false;
        this.xrLabel28.StylePriority.UseTextAlignment = false;
        this.xrLabel28.Text = "Quadro Lógico";
        this.xrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel29
        // 
        this.xrLabel29.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel29.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel29.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel29.CanShrink = true;
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(0F, 190.3766F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrLabel29.StylePriority.UseBackColor = false;
        this.xrLabel29.StylePriority.UseBorderColor = false;
        this.xrLabel29.StylePriority.UseBorders = false;
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.StylePriority.UseTextAlignment = false;
        this.xrLabel29.Text = "Objetivo Geral do Projeto";
        this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // xrLabel30
        // 
        this.xrLabel30.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel30.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.ObjetivoGeral")});
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(0F, 251.3765F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(2539.969F, 61.8194F);
        this.xrLabel30.StylePriority.UseBorderColor = false;
        this.xrLabel30.StylePriority.UseBorders = false;
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.Text = "xrLabel30";
        // 
        // DetailReport4
        // 
        this.DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.GroupHeader6,
            this.GroupHeader16});
        this.DetailReport4.DataMember = "ResultadosEsperados";
        this.DetailReport4.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport4.Dpi = 254F;
        this.DetailReport4.Expanded = false;
        this.DetailReport4.Level = 5;
        this.DetailReport4.Name = "DetailReport4";
        // 
        // Detail5
        // 
        this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.Detail5.Dpi = 254F;
        this.Detail5.HeightF = 60F;
        this.Detail5.Name = "Detail5";
        // 
        // xrTable7
        // 
        this.xrTable7.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable7.KeepTogether = true;
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(2.565063F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable7.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrTable7.StylePriority.UseBorderColor = false;
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
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.CanShrink = true;
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultadosEsperados.DescricaoResultado")});
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UsePadding = false;
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.Weight = 0.59150910406121771D;
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
        this.xrTableCell19.Weight = 0.31304934401399986D;
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
        this.GroupHeader6.Dpi = 254F;
        this.GroupHeader6.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader6.HeightF = 60F;
        this.GroupHeader6.Name = "GroupHeader6";
        this.GroupHeader6.RepeatEveryPage = true;
        // 
        // xrTable9
        // 
        this.xrTable9.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable9.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable9.Dpi = 254F;
        this.xrTable9.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.xrTable9.KeepTogether = true;
        this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(2.565008F, 0F);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable9.SizeF = new System.Drawing.SizeF(2539.969F, 60F);
        this.xrTable9.StylePriority.UseBackColor = false;
        this.xrTable9.StylePriority.UseBorderColor = false;
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
        this.xrTableCell23.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell23.CanShrink = true;
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell23.StylePriority.UseBackColor = false;
        this.xrTableCell23.StylePriority.UsePadding = false;
        this.xrTableCell23.Text = "Resultados Esperados";
        this.xrTableCell23.Weight = 0.59150907969829758D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell24.CanShrink = true;
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell24.StylePriority.UseBackColor = false;
        this.xrTableCell24.StylePriority.UsePadding = false;
        this.xrTableCell24.Text = "Indicadores Operacionais";
        this.xrTableCell24.Weight = 0.31304932486010695D;
        // 
        // GroupHeader16
        // 
        this.GroupHeader16.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine6});
        this.GroupHeader16.Dpi = 254F;
        this.GroupHeader16.HeightF = 60.8542F;
        this.GroupHeader16.Level = 1;
        this.GroupHeader16.Name = "GroupHeader16";
        // 
        // xrLine6
        // 
        this.xrLine6.Dpi = 254F;
        this.xrLine6.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine6.LineWidth = 3;
        this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(1.00002F, 0.434202F);
        this.xrLine6.Name = "xrLine6";
        this.xrLine6.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLine6.StylePriority.UseForeColor = false;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCabecalho,
            this.xrLine1,
            this.picLogoEntidade});
        this.PageHeader.Dpi = 254F;
        this.PageHeader.Expanded = false;
        this.PageHeader.HeightF = 207.9095F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblCabecalho
        // 
        this.lblCabecalho.Dpi = 254F;
        this.lblCabecalho.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblCabecalho.LocationFloat = new DevExpress.Utils.PointFloat(490.834F, 25.00001F);
        this.lblCabecalho.Name = "lblCabecalho";
        this.lblCabecalho.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblCabecalho.SizeF = new System.Drawing.SizeF(2048.416F, 135.21F);
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
        this.DetailReport5.Expanded = false;
        this.DetailReport5.Level = 3;
        this.DetailReport5.Name = "DetailReport5";
        // 
        // Detail6
        // 
        this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAreaAtuacao1,
            this.xrLabel26});
        this.Detail6.Dpi = 254F;
        this.Detail6.HeightF = 205.4783F;
        this.Detail6.Name = "Detail6";
        // 
        // DetailReport6
        // 
        this.DetailReport6.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.DetailReport8,
            this.DetailReport9,
            this.GroupHeader7,
            this.DetailReport7});
        this.DetailReport6.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.DetailReport6.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport6.Dpi = 254F;
        this.DetailReport6.Level = 6;
        this.DetailReport6.Name = "DetailReport6";
        // 
        // Detail7
        // 
        this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tbDadosEvento,
            this.tbEventoDetalhe,
            this.xrSubreport1,
            this.xrTable11,
            this.celulaTituloProduto});
        this.Detail7.Dpi = 254F;
        this.Detail7.HeightF = 178.3062F;
        this.Detail7.Name = "Detail7";
        this.Detail7.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(30, 30, 30, 30, 254F);
        // 
        // tbDadosEvento
        // 
        this.tbDadosEvento.BorderColor = System.Drawing.Color.DarkGray;
        this.tbDadosEvento.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.tbDadosEvento.Dpi = 254F;
        this.tbDadosEvento.LocationFloat = new DevExpress.Utils.PointFloat(58.39288F, 153.754F);
        this.tbDadosEvento.Name = "tbDadosEvento";
        this.tbDadosEvento.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
        this.tbDadosEvento.SizeF = new System.Drawing.SizeF(1285.016F, 12.5F);
        this.tbDadosEvento.StylePriority.UseBorderColor = false;
        this.tbDadosEvento.StylePriority.UseBorders = false;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell5});
        this.xrTableRow13.Dpi = 254F;
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.CanShrink = true;
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.LocalEvento")});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.Text = "[detalhesProposta_AcoesIniciativa.LocalEvento]";
        this.xrTableCell2.Weight = 1.3612243640434825D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.CanShrink = true;
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.DetalhesEvento")});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UsePadding = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1.2365258859535639D;
        // 
        // tbEventoDetalhe
        // 
        this.tbEventoDetalhe.BackColor = System.Drawing.Color.WhiteSmoke;
        this.tbEventoDetalhe.BorderColor = System.Drawing.Color.DarkGray;
        this.tbEventoDetalhe.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.tbEventoDetalhe.Dpi = 254F;
        this.tbEventoDetalhe.LocationFloat = new DevExpress.Utils.PointFloat(58.39291F, 141.254F);
        this.tbEventoDetalhe.Name = "tbEventoDetalhe";
        this.tbEventoDetalhe.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow21});
        this.tbEventoDetalhe.SizeF = new System.Drawing.SizeF(1285.016F, 12.5F);
        this.tbEventoDetalhe.StylePriority.UseBackColor = false;
        this.tbEventoDetalhe.StylePriority.UseBorderColor = false;
        this.tbEventoDetalhe.StylePriority.UseBorders = false;
        // 
        // xrTableRow21
        // 
        this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell22});
        this.xrTableRow21.Dpi = 254F;
        this.xrTableRow21.Name = "xrTableRow21";
        this.xrTableRow21.Weight = 0.85745257939223452D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell17.CanShrink = true;
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.titLocalEvento")});
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBackColor = false;
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.Text = "Local";
        this.xrTableCell17.Weight = 0.81667834562215413D;
        this.xrTableCell17.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell17_BeforePrint);
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell22.CanShrink = true;
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.titDetalhesEvento")});
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBackColor = false;
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.Text = "Detalhes";
        this.xrTableCell22.Weight = 0.74186441169127781D;
        // 
        // xrSubreport1
        // 
        this.xrSubreport1.Dpi = 254F;
        this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(1343.41F, 153.7539F);
        this.xrSubreport1.Name = "xrSubreport1";
        this.xrSubreport1.SizeF = new System.Drawing.SizeF(1199.94F, 12.49998F);
        this.xrSubreport1.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrSubreport1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport1_BeforePrint);
        // 
        // xrTable11
        // 
        this.xrTable11.BorderColor = System.Drawing.Color.DarkGray;
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
            this.xrTableRow11});
        this.xrTable11.SizeF = new System.Drawing.SizeF(2543.353F, 120F);
        this.xrTable11.StylePriority.UseBorderColor = false;
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
        this.xrTableRow18.Weight = 0.58030169309341428D;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.BackColor = System.Drawing.Color.DarkGray;
        this.xrTableCell33.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell33.Dpi = 254F;
        this.xrTableCell33.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell33.StylePriority.UseBackColor = false;
        this.xrTableCell33.StylePriority.UseBorderColor = false;
        this.xrTableCell33.StylePriority.UseBorders = false;
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.StylePriority.UsePadding = false;
        this.xrTableCell33.Text = "Num.";
        this.xrTableCell33.Weight = 0.0703693016243219D;
        // 
        // celTituloAtividadeAcao
        // 
        this.celTituloAtividadeAcao.BackColor = System.Drawing.Color.DarkGray;
        this.celTituloAtividadeAcao.BorderColor = System.Drawing.Color.DimGray;
        this.celTituloAtividadeAcao.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.celTituloAtividadeAcao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.indicaAcaoAtividade")});
        this.celTituloAtividadeAcao.Dpi = 254F;
        this.celTituloAtividadeAcao.Name = "celTituloAtividadeAcao";
        this.celTituloAtividadeAcao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.celTituloAtividadeAcao.StylePriority.UseBackColor = false;
        this.celTituloAtividadeAcao.StylePriority.UseBorderColor = false;
        this.celTituloAtividadeAcao.StylePriority.UseBorders = false;
        this.celTituloAtividadeAcao.StylePriority.UsePadding = false;
        this.celTituloAtividadeAcao.Text = "Atividade";
        this.celTituloAtividadeAcao.Weight = 0.26827407341524462D;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.BackColor = System.Drawing.Color.DarkGray;
        this.xrTableCell50.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell50.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell50.Dpi = 254F;
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell50.StylePriority.UseBackColor = false;
        this.xrTableCell50.StylePriority.UseBorderColor = false;
        this.xrTableCell50.StylePriority.UseBorders = false;
        this.xrTableCell50.StylePriority.UsePadding = false;
        this.xrTableCell50.Text = "Início";
        this.xrTableCell50.Weight = 0.13034048714249602D;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.BackColor = System.Drawing.Color.DarkGray;
        this.xrTableCell53.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell53.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell53.Dpi = 254F;
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell53.StylePriority.UseBackColor = false;
        this.xrTableCell53.StylePriority.UseBorderColor = false;
        this.xrTableCell53.StylePriority.UseBorders = false;
        this.xrTableCell53.StylePriority.UsePadding = false;
        this.xrTableCell53.Text = "Término";
        this.xrTableCell53.Weight = 0.10635191886137881D;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.BackColor = System.Drawing.Color.DarkGray;
        this.xrTableCell54.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell54.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell54.Dpi = 254F;
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell54.StylePriority.UseBackColor = false;
        this.xrTableCell54.StylePriority.UseBorderColor = false;
        this.xrTableCell54.StylePriority.UseBorders = false;
        this.xrTableCell54.StylePriority.UsePadding = false;
        this.xrTableCell54.Text = "Inst.";
        this.xrTableCell54.Weight = 0.0463808924386822D;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.BackColor = System.Drawing.Color.DarkGray;
        this.xrTableCell55.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell55.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell55.Dpi = 254F;
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell55.StylePriority.UseBackColor = false;
        this.xrTableCell55.StylePriority.UseBorderColor = false;
        this.xrTableCell55.StylePriority.UseBorders = false;
        this.xrTableCell55.StylePriority.UsePadding = false;
        this.xrTableCell55.Text = "Responsável";
        this.xrTableCell55.Weight = 0.27953097639148561D;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.BackColor = System.Drawing.Color.DarkGray;
        this.xrTableCell56.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell56.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell56.Dpi = 254F;
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell56.StylePriority.UseBackColor = false;
        this.xrTableCell56.StylePriority.UseBorderColor = false;
        this.xrTableCell56.StylePriority.UseBorders = false;
        this.xrTableCell56.StylePriority.UsePadding = false;
        this.xrTableCell56.Text = "Fonte de Recurso";
        this.xrTableCell56.Weight = 0.274224775888057D;
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
        this.xrTableRow11.Weight = 0.580301831388856D;
        // 
        // celNumeroAcao
        // 
        this.celNumeroAcao.CanShrink = true;
        this.celNumeroAcao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.Numero")});
        this.celNumeroAcao.Dpi = 254F;
        this.celNumeroAcao.Name = "celNumeroAcao";
        this.celNumeroAcao.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.celNumeroAcao.StylePriority.UsePadding = false;
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
        this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell21.StylePriority.UsePadding = false;
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
        this.xrTableCell27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell27.StylePriority.UsePadding = false;
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
        this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell29.StylePriority.UsePadding = false;
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
        this.xrTableCell31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell31.StylePriority.UsePadding = false;
        this.xrTableCell31.Text = "xrTableCell31";
        this.xrTableCell31.Weight = 0.0463808924386822D;
        this.xrTableCell31.TextChanged += new System.EventHandler(this.xrTableCell31_TextChanged);
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.CanShrink = true;
        this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.Responsavel")});
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell32.StylePriority.UsePadding = false;
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
        this.celFonteRecurso.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.celFonteRecurso.StylePriority.UsePadding = false;
        this.celFonteRecurso.Text = "[detalhesProposta_AcoesIniciativa.FonteRecurso]";
        this.celFonteRecurso.Weight = 0.27468678094290766D;
        this.celFonteRecurso.TextChanged += new System.EventHandler(this.celFonteRecurso_TextChanged);
        // 
        // celulaTituloProduto
        // 
        this.celulaTituloProduto.BackColor = System.Drawing.Color.LightGray;
        this.celulaTituloProduto.BorderColor = System.Drawing.Color.DarkGray;
        this.celulaTituloProduto.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.celulaTituloProduto.CanShrink = true;
        this.celulaTituloProduto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.indicaProdutoMeta")});
        this.celulaTituloProduto.Dpi = 254F;
        this.celulaTituloProduto.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.celulaTituloProduto.LocationFloat = new DevExpress.Utils.PointFloat(1343.41F, 141.254F);
        this.celulaTituloProduto.Name = "celulaTituloProduto";
        this.celulaTituloProduto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.celulaTituloProduto.SizeF = new System.Drawing.SizeF(1199.94F, 12.50003F);
        this.celulaTituloProduto.StylePriority.UseBackColor = false;
        this.celulaTituloProduto.StylePriority.UseBorderColor = false;
        this.celulaTituloProduto.StylePriority.UseBorders = false;
        this.celulaTituloProduto.StylePriority.UseFont = false;
        this.celulaTituloProduto.Text = "Produto";
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
        this.DetailReport8.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail9
        // 
        this.Detail9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
        this.Detail9.Dpi = 254F;
        this.Detail9.HeightF = 60F;
        this.Detail9.Name = "Detail9";
        // 
        // xrTable13
        // 
        this.xrTable13.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable13.Dpi = 254F;
        this.xrTable13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable13.KeepTogether = true;
        this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(58.39289F, 0F);
        this.xrTable13.Name = "xrTable13";
        this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
        this.xrTable13.SizeF = new System.Drawing.SizeF(2484.963F, 60F);
        this.xrTable13.StylePriority.UseBorderColor = false;
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
        this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.StylePriority.UsePadding = false;
        this.xrTableCell41.Text = "xrTableCell41";
        this.xrTableCell41.Weight = 0.92711195002270053D;
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
        this.xrTableCell49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell49.StylePriority.UseBorders = false;
        this.xrTableCell49.StylePriority.UsePadding = false;
        this.xrTableCell49.Text = "xrTableCell49";
        this.xrTableCell49.Weight = 1.2544854996958859D;
        // 
        // GroupHeader9
        // 
        this.GroupHeader9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14});
        this.GroupHeader9.Dpi = 254F;
        this.GroupHeader9.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader9.HeightF = 120.8541F;
        this.GroupHeader9.Name = "GroupHeader9";
        this.GroupHeader9.RepeatEveryPage = true;
        // 
        // xrTable14
        // 
        this.xrTable14.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top;
        this.xrTable14.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable14.Dpi = 254F;
        this.xrTable14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable14.KeepTogether = true;
        this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(58.39291F, 60.85406F);
        this.xrTable14.Name = "xrTable14";
        this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
        this.xrTable14.SizeF = new System.Drawing.SizeF(2484.963F, 60F);
        this.xrTable14.StylePriority.UseBorderColor = false;
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
        this.celTituloAreaParceria.BackColor = System.Drawing.Color.LightGray;
        this.celTituloAreaParceria.CanShrink = true;
        this.celTituloAreaParceria.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
                    "tiva.titAreaDeParceriaCalc")});
        this.celTituloAreaParceria.Dpi = 254F;
        this.celTituloAreaParceria.Name = "celTituloAreaParceria";
        this.celTituloAreaParceria.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.celTituloAreaParceria.StylePriority.UseBackColor = false;
        this.celTituloAreaParceria.StylePriority.UsePadding = false;
        this.celTituloAreaParceria.Text = "Área de Parceria";
        this.celTituloAreaParceria.Weight = 0.92827129248623D;
        // 
        // celTituloElementoParceria
        // 
        this.celTituloElementoParceria.BackColor = System.Drawing.Color.LightGray;
        this.celTituloElementoParceria.CanShrink = true;
        this.celTituloElementoParceria.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
                    "tiva.titElementoDeParceriaCalc")});
        this.celTituloElementoParceria.Dpi = 254F;
        this.celTituloElementoParceria.Name = "celTituloElementoParceria";
        this.celTituloElementoParceria.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.celTituloElementoParceria.StylePriority.UseBackColor = false;
        this.celTituloElementoParceria.StylePriority.UsePadding = false;
        this.celTituloElementoParceria.Text = "Elemento da Parceria";
        this.celTituloElementoParceria.Weight = 1.2560540966927056D;
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
        this.Detail10.HeightF = 60F;
        this.Detail10.Name = "Detail10";
        // 
        // xrTable15
        // 
        this.xrTable15.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable15.Dpi = 254F;
        this.xrTable15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable15.KeepTogether = true;
        this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(58.39582F, 0F);
        this.xrTable15.Name = "xrTable15";
        this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
        this.xrTable15.SizeF = new System.Drawing.SizeF(2484.957F, 60F);
        this.xrTable15.StylePriority.UseBorderColor = false;
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
        this.xrTableCell51.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell51.StylePriority.UseBorders = false;
        this.xrTableCell51.StylePriority.UsePadding = false;
        this.xrTableCell51.Text = "xrTableCell51";
        this.xrTableCell51.Weight = 1.8803740305400614D;
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
        this.xrTableCell52.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell52.StylePriority.UseBorders = false;
        this.xrTableCell52.StylePriority.UsePadding = false;
        this.xrTableCell52.Text = "xrTableCell52";
        this.xrTableCell52.Weight = 0.30122106148447853D;
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
        this.GroupHeader10.HeightF = 120.8541F;
        this.GroupHeader10.Name = "GroupHeader10";
        this.GroupHeader10.RepeatEveryPage = true;
        // 
        // xrTable16
        // 
        this.xrTable16.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable16.Dpi = 254F;
        this.xrTable16.KeepTogether = true;
        this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(58.39283F, 60.85406F);
        this.xrTable16.Name = "xrTable16";
        this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
        this.xrTable16.SizeF = new System.Drawing.SizeF(2484.96F, 60F);
        this.xrTable16.StylePriority.UseBorderColor = false;
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
        this.celTitMarcoProjeto.BackColor = System.Drawing.Color.LightGray;
        this.celTitMarcoProjeto.CanShrink = true;
        this.celTitMarcoProjeto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
                    "a.titMarcoProjeto")});
        this.celTitMarcoProjeto.Dpi = 254F;
        this.celTitMarcoProjeto.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.celTitMarcoProjeto.Name = "celTitMarcoProjeto";
        this.celTitMarcoProjeto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.celTitMarcoProjeto.StylePriority.UseBackColor = false;
        this.celTitMarcoProjeto.StylePriority.UseFont = false;
        this.celTitMarcoProjeto.StylePriority.UsePadding = false;
        this.celTitMarcoProjeto.Text = "Marco do  Projeto";
        this.celTitMarcoProjeto.Weight = 1.8798530887182676D;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell45.CanShrink = true;
        this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_MarcosIniciativ" +
                    "a.titData")});
        this.xrTableCell45.Dpi = 254F;
        this.xrTableCell45.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell45.StylePriority.UseBackColor = false;
        this.xrTableCell45.StylePriority.UseFont = false;
        this.xrTableCell45.StylePriority.UsePadding = false;
        this.xrTableCell45.Text = "Data";
        this.xrTableCell45.Weight = 0.30174200330627221D;
        // 
        // GroupHeader7
        // 
        this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3});
        this.GroupHeader7.Dpi = 254F;
        this.GroupHeader7.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader7.HeightF = 220.5179F;
        this.GroupHeader7.Name = "GroupHeader7";
        // 
        // xrLabel3
        // 
        this.xrLabel3.BackColor = System.Drawing.Color.Gray;
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(2543.353F, 60F);
        this.xrLabel3.StylePriority.UseBackColor = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Plano de Trabalho";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
        this.DetailReport7.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail8
        // 
        this.Detail8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable20});
        this.Detail8.Dpi = 254F;
        this.Detail8.HeightF = 60F;
        this.Detail8.Name = "Detail8";
        // 
        // xrTable20
        // 
        this.xrTable20.BackColor = System.Drawing.Color.WhiteSmoke;
        this.xrTable20.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable20.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable20.Dpi = 254F;
        this.xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(58.39291F, 0F);
        this.xrTable20.Name = "xrTable20";
        this.xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
        this.xrTable20.SizeF = new System.Drawing.SizeF(2484.963F, 60F);
        this.xrTable20.StylePriority.UseBackColor = false;
        this.xrTable20.StylePriority.UseBorderColor = false;
        this.xrTable20.StylePriority.UseBorders = false;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9});
        this.xrTableRow12.Dpi = 254F;
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.BackColor = System.Drawing.Color.Transparent;
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.CanShrink = true;
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ProdutosAcoesIn" +
                    "iciativa.Meta")});
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell9.StylePriority.UseBackColor = false;
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UsePadding = false;
        this.xrTableCell9.Text = "xrTableCell22";
        this.xrTableCell9.Weight = 2.2989538446514239D;
        // 
        // GroupHeader8
        // 
        this.GroupHeader8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblMeta});
        this.GroupHeader8.Dpi = 254F;
        this.GroupHeader8.HeightF = 60F;
        this.GroupHeader8.Name = "GroupHeader8";
        this.GroupHeader8.RepeatEveryPage = true;
        // 
        // lblMeta
        // 
        this.lblMeta.BackColor = System.Drawing.Color.DarkGray;
        this.lblMeta.BorderColor = System.Drawing.Color.DarkGray;
        this.lblMeta.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblMeta.CanShrink = true;
        this.lblMeta.Dpi = 254F;
        this.lblMeta.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.lblMeta.LocationFloat = new DevExpress.Utils.PointFloat(58.39582F, 0F);
        this.lblMeta.Name = "lblMeta";
        this.lblMeta.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblMeta.SizeF = new System.Drawing.SizeF(2484.96F, 60F);
        this.lblMeta.StylePriority.UseBackColor = false;
        this.lblMeta.StylePriority.UseBorderColor = false;
        this.lblMeta.StylePriority.UseBorders = false;
        this.lblMeta.StylePriority.UseFont = false;
        this.lblMeta.Text = "Meta";
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
            this.xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.Expanded = false;
        this.PageFooter.HeightF = 58.42F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.BorderColor = System.Drawing.Color.DarkOliveGreen;
        this.xrPageInfo1.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0.9999909F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(1284.778F, 58.42F);
        this.xrPageInfo1.StylePriority.UseBorderColor = false;
        this.xrPageInfo1.StylePriority.UseBorders = false;
        this.xrPageInfo1.StylePriority.UseFont = false;
        this.xrPageInfo1.StylePriority.UseTextAlignment = false;
        this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // titAreaDeParceriaCalc
        // 
        this.titAreaDeParceriaCalc.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa.AcoesIniciativa_ParceirosInicia" +
"tiva";
        this.titAreaDeParceriaCalc.Expression = "Iif([Area]   != \'\' , \'Área de Parceria\' ,\'\')";
        this.titAreaDeParceriaCalc.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.titAreaDeParceriaCalc.Name = "titAreaDeParceriaCalc";
        // 
        // indicaProdutoMeta
        // 
        this.indicaProdutoMeta.DataMember = "detalhesProposta.detalhesProposta_AcoesIniciativa";
        this.indicaProdutoMeta.Expression = "Iif(Contains([Numero], \'.\'),\'Produto\'  ,\'\' )";
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
            this.GroupHeader11,
            this.GroupFooter2,
            this.GroupHeader19});
        this.DetailReport11.DataMember = "CronogramaOrc";
        this.DetailReport11.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport11.Dpi = 254F;
        this.DetailReport11.FilterString = "[EtapaOrcamento] < 2";
        this.DetailReport11.Level = 8;
        this.DetailReport11.Name = "DetailReport11";
        this.DetailReport11.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail12
        // 
        this.Detail12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
        this.Detail12.Dpi = 254F;
        this.Detail12.HeightF = 50F;
        this.Detail12.Name = "Detail12";
        // 
        // xrTable10
        // 
        this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable10.Dpi = 254F;
        this.xrTable10.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(58.39F, 0F);
        this.xrTable10.Name = "xrTable10";
        this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable10.SizeF = new System.Drawing.SizeF(2486.066F, 50F);
        this.xrTable10.StylePriority.UseBorders = false;
        this.xrTable10.StylePriority.UseFont = false;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell28});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 0.5679012345679012D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.CONTA_DES")});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell15.Weight = 0.44688091057780088D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.Quantidade")});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell16.Weight = 0.10140103801659464D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorUnitario", "{0:c2}")});
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.Text = "xrTableCell25";
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell25.Weight = 0.13385167858620947D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorProposto", "{0:c2}")});
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.StylePriority.UsePadding = false;
        this.xrTableCell26.StylePriority.UseTextAlignment = false;
        this.xrTableCell26.Text = "xrTableCell26";
        this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell26.Weight = 0.16972359778628157D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.MemoriaCalculo")});
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UsePadding = false;
        this.xrTableCell28.StylePriority.UseTextAlignment = false;
        this.xrTableCell28.Text = "xrTableCell28";
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell28.Weight = 0.48331807785695474D;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable17});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ordem", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 146.8336F;
        this.GroupHeader1.Level = 1;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrTable17
        // 
        this.xrTable17.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable17.Dpi = 254F;
        this.xrTable17.KeepTogether = true;
        this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 47F);
        this.xrTable17.Name = "xrTable17";
        this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23});
        this.xrTable17.SizeF = new System.Drawing.SizeF(2544.455F, 50F);
        this.xrTable17.StylePriority.UseBorderColor = false;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableRow23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell95,
            this.xrTableCell72});
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
        this.xrTableCell95.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DescricaoAcao")});
        this.xrTableCell95.Dpi = 254F;
        this.xrTableCell95.Name = "xrTableCell95";
        this.xrTableCell95.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell95.StylePriority.UseBackColor = false;
        this.xrTableCell95.StylePriority.UseBorderColor = false;
        this.xrTableCell95.StylePriority.UsePadding = false;
        this.xrTableCell95.Text = "[DescricaoAcao]";
        this.xrTableCell95.Weight = 1.5745374145665527D;
        this.xrTableCell95.TextChanged += new System.EventHandler(this.xrTableCell95_TextChanged);
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.fonte")});
        this.xrTableCell72.Dpi = 254F;
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorderColor = false;
        this.xrTableCell72.Text = "xrTableCell72";
        this.xrTableCell72.Weight = 0.861624548497699D;
        // 
        // ReportFooter2
        // 
        this.ReportFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine3});
        this.ReportFooter2.Dpi = 254F;
        this.ReportFooter2.HeightF = 47.83667F;
        this.ReportFooter2.Name = "ReportFooter2";
        // 
        // xrLine3
        // 
        this.xrLine3.Dpi = 254F;
        this.xrLine3.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine3.LineWidth = 3;
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(254F, 47.83667F);
        this.xrLine3.StylePriority.UseForeColor = false;
        // 
        // GroupHeader11
        // 
        this.GroupHeader11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8,
            this.xrLabel9,
            this.lblTotalUNEtapaOrcMenorQue2ValProp,
            this.xrLabel27,
            this.xrLabel35,
            this.lblTotalFundecoopEtapaOrcMenorQue2,
            this.xrLabel25,
            this.lblTotalGeralEtapaOrcMenorQue2});
        this.GroupHeader11.Dpi = 254F;
        this.GroupHeader11.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader11.HeightF = 232.8333F;
        this.GroupHeader11.Level = 2;
        this.GroupHeader11.Name = "GroupHeader11";
        // 
        // xrTable8
        // 
        this.xrTable8.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.Dpi = 254F;
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 147.1599F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable8.SizeF = new System.Drawing.SizeF(969.7225F, 49.41666F);
        this.xrTable8.StylePriority.UseBorderColor = false;
        this.xrTable8.StylePriority.UseBorders = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell11});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UsePadding = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Projeto:";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell1.Weight = 0.49850676153818685D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.nomeIniciativaComProjeto")});
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.StylePriority.UsePadding = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 3.1839260194694896D;
        // 
        // xrLabel9
        // 
        this.xrLabel9.BackColor = System.Drawing.Color.Gray;
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 47.76017F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(2543.353F, 47.83665F);
        this.xrLabel9.StylePriority.UseBackColor = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.StylePriority.UseTextAlignment = false;
        this.xrLabel9.Text = "Cronograma Orçamentário";
        this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // lblTotalUNEtapaOrcMenorQue2ValProp
        // 
        this.lblTotalUNEtapaOrcMenorQue2ValProp.BackColor = System.Drawing.Color.Transparent;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.BorderColor = System.Drawing.Color.DarkGray;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTotalUNEtapaOrcMenorQue2ValProp.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorProposto")});
        this.lblTotalUNEtapaOrcMenorQue2ValProp.Dpi = 254F;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblTotalUNEtapaOrcMenorQue2ValProp.LocationFloat = new DevExpress.Utils.PointFloat(1138.713F, 147.1599F);
        this.lblTotalUNEtapaOrcMenorQue2ValProp.Name = "lblTotalUNEtapaOrcMenorQue2ValProp";
        this.lblTotalUNEtapaOrcMenorQue2ValProp.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTotalUNEtapaOrcMenorQue2ValProp.SizeF = new System.Drawing.SizeF(318.3313F, 49.4166F);
        this.lblTotalUNEtapaOrcMenorQue2ValProp.StylePriority.UseBackColor = false;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.StylePriority.UseBorderColor = false;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.StylePriority.UseBorders = false;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.StylePriority.UseFont = false;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:c2}";
        xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.Summary = xrSummary1;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.Text = "lblTotalUNEtapaOrcMenorQue2ValProp";
        this.lblTotalUNEtapaOrcMenorQue2ValProp.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.lblTotalUNEtapaOrcMenorQue2ValProp.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.lblTotalUNEtapaOrcMenorQue2ValProp_SummaryGetResult);
        this.lblTotalUNEtapaOrcMenorQue2ValProp.SummaryRowChanged += new System.EventHandler(this.lblTotalUNEtapaOrcMenorQue2ValProp_SummaryRowChanged);
        // 
        // xrLabel27
        // 
        this.xrLabel27.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel27.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel27.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(979.7222F, 147.16F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(158.9908F, 49.41653F);
        this.xrLabel27.StylePriority.UseBackColor = false;
        this.xrLabel27.StylePriority.UseBorderColor = false;
        this.xrLabel27.StylePriority.UseBorders = false;
        this.xrLabel27.StylePriority.UseFont = false;
        this.xrLabel27.StylePriority.UseTextAlignment = false;
        this.xrLabel27.Text = "Total UN:";
        this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel35
        // 
        this.xrLabel35.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel35.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(1467.573F, 147.16F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(290.0618F, 49.41649F);
        this.xrLabel35.StylePriority.UseBackColor = false;
        this.xrLabel35.StylePriority.UseBorderColor = false;
        this.xrLabel35.StylePriority.UseBorders = false;
        this.xrLabel35.StylePriority.UseFont = false;
        this.xrLabel35.StylePriority.UseTextAlignment = false;
        this.xrLabel35.Text = "Total FUNDECOOP:";
        this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblTotalFundecoopEtapaOrcMenorQue2
        // 
        this.lblTotalFundecoopEtapaOrcMenorQue2.BackColor = System.Drawing.Color.Transparent;
        this.lblTotalFundecoopEtapaOrcMenorQue2.BorderColor = System.Drawing.Color.DarkGray;
        this.lblTotalFundecoopEtapaOrcMenorQue2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTotalFundecoopEtapaOrcMenorQue2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorProposto")});
        this.lblTotalFundecoopEtapaOrcMenorQue2.Dpi = 254F;
        this.lblTotalFundecoopEtapaOrcMenorQue2.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblTotalFundecoopEtapaOrcMenorQue2.LocationFloat = new DevExpress.Utils.PointFloat(1757.635F, 147.16F);
        this.lblTotalFundecoopEtapaOrcMenorQue2.Name = "lblTotalFundecoopEtapaOrcMenorQue2";
        this.lblTotalFundecoopEtapaOrcMenorQue2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTotalFundecoopEtapaOrcMenorQue2.SizeF = new System.Drawing.SizeF(280.4583F, 49.41646F);
        this.lblTotalFundecoopEtapaOrcMenorQue2.StylePriority.UseBackColor = false;
        this.lblTotalFundecoopEtapaOrcMenorQue2.StylePriority.UseBorderColor = false;
        this.lblTotalFundecoopEtapaOrcMenorQue2.StylePriority.UseBorders = false;
        this.lblTotalFundecoopEtapaOrcMenorQue2.StylePriority.UseFont = false;
        this.lblTotalFundecoopEtapaOrcMenorQue2.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:c2}";
        xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
        xrSummary2.IgnoreNullValues = true;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lblTotalFundecoopEtapaOrcMenorQue2.Summary = xrSummary2;
        this.lblTotalFundecoopEtapaOrcMenorQue2.Text = "lblTotalFundecoopEtapaOrcMenorQue2";
        this.lblTotalFundecoopEtapaOrcMenorQue2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.lblTotalFundecoopEtapaOrcMenorQue2.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.lblTotalFundecoopEtapaOrcMenorQue2_SummaryGetResult);
        this.lblTotalFundecoopEtapaOrcMenorQue2.SummaryRowChanged += new System.EventHandler(this.lblTotalFundecoopEtapaOrcMenorQue2_SummaryRowChanged);
        // 
        // xrLabel25
        // 
        this.xrLabel25.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel25.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(2048.915F, 147.16F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(191.0637F, 49.41641F);
        this.xrLabel25.StylePriority.UseBackColor = false;
        this.xrLabel25.StylePriority.UseBorderColor = false;
        this.xrLabel25.StylePriority.UseBorders = false;
        this.xrLabel25.StylePriority.UseFont = false;
        this.xrLabel25.StylePriority.UseTextAlignment = false;
        this.xrLabel25.Text = "Total geral:";
        this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblTotalGeralEtapaOrcMenorQue2
        // 
        this.lblTotalGeralEtapaOrcMenorQue2.BackColor = System.Drawing.Color.Transparent;
        this.lblTotalGeralEtapaOrcMenorQue2.BorderColor = System.Drawing.Color.DarkGray;
        this.lblTotalGeralEtapaOrcMenorQue2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTotalGeralEtapaOrcMenorQue2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorProposto")});
        this.lblTotalGeralEtapaOrcMenorQue2.Dpi = 254F;
        this.lblTotalGeralEtapaOrcMenorQue2.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblTotalGeralEtapaOrcMenorQue2.LocationFloat = new DevExpress.Utils.PointFloat(2239.979F, 147.16F);
        this.lblTotalGeralEtapaOrcMenorQue2.Name = "lblTotalGeralEtapaOrcMenorQue2";
        this.lblTotalGeralEtapaOrcMenorQue2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTotalGeralEtapaOrcMenorQue2.SizeF = new System.Drawing.SizeF(303.3743F, 49.41638F);
        this.lblTotalGeralEtapaOrcMenorQue2.StylePriority.UseBackColor = false;
        this.lblTotalGeralEtapaOrcMenorQue2.StylePriority.UseBorderColor = false;
        this.lblTotalGeralEtapaOrcMenorQue2.StylePriority.UseBorders = false;
        this.lblTotalGeralEtapaOrcMenorQue2.StylePriority.UseFont = false;
        this.lblTotalGeralEtapaOrcMenorQue2.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:c2}";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lblTotalGeralEtapaOrcMenorQue2.Summary = xrSummary3;
        this.lblTotalGeralEtapaOrcMenorQue2.Text = "lblTotalGeralEtapaOrcMenorQue2";
        this.lblTotalGeralEtapaOrcMenorQue2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable24});
        this.GroupFooter2.Dpi = 254F;
        this.GroupFooter2.HeightF = 49.99999F;
        this.GroupFooter2.Level = 1;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // xrTable24
        // 
        this.xrTable24.BackColor = System.Drawing.Color.NavajoWhite;
        this.xrTable24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable24.Dpi = 254F;
        this.xrTable24.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable24.LocationFloat = new DevExpress.Utils.PointFloat(58.39F, 0F);
        this.xrTable24.Name = "xrTable24";
        this.xrTable24.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27});
        this.xrTable24.SizeF = new System.Drawing.SizeF(2486.065F, 49.99999F);
        this.xrTable24.StylePriority.UseBackColor = false;
        this.xrTable24.StylePriority.UseBorders = false;
        this.xrTable24.StylePriority.UseFont = false;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36,
            this.xrTableCell73,
            this.xrTableCell76});
        this.xrTableRow27.Dpi = 254F;
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 0.5679012345679012D;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseBorders = false;
        this.xrTableCell36.Text = "Total:";
        this.xrTableCell36.Weight = 0.68212944508364337D;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorProposto")});
        this.xrTableCell73.Dpi = 254F;
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell73.StylePriority.UseBorders = false;
        this.xrTableCell73.StylePriority.UsePadding = false;
        this.xrTableCell73.StylePriority.UseTextAlignment = false;
        xrSummary4.FormatString = "{0:c2}";
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell73.Summary = xrSummary4;
        this.xrTableCell73.Text = "xrTableCell73";
        this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell73.Weight = 0.16972163996462794D;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell76.Dpi = 254F;
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell76.StylePriority.UseBorders = false;
        this.xrTableCell76.StylePriority.UsePadding = false;
        this.xrTableCell76.StylePriority.UseTextAlignment = false;
        this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell76.Weight = 0.48331621089809162D;
        // 
        // GroupHeader19
        // 
        this.GroupHeader19.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
        this.GroupHeader19.Dpi = 254F;
        this.GroupHeader19.HeightF = 50F;
        this.GroupHeader19.Name = "GroupHeader19";
        this.GroupHeader19.RepeatEveryPage = true;
        // 
        // xrTable12
        // 
        this.xrTable12.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTable12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable12.Dpi = 254F;
        this.xrTable12.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(58.39F, 0F);
        this.xrTable12.Name = "xrTable12";
        this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19});
        this.xrTable12.SizeF = new System.Drawing.SizeF(2486.066F, 50F);
        this.xrTable12.StylePriority.UseBackColor = false;
        this.xrTable12.StylePriority.UseBorders = false;
        this.xrTable12.StylePriority.UseFont = false;
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell37,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell42});
        this.xrTableRow19.Dpi = 254F;
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.Weight = 0.5679012345679012D;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Dpi = 254F;
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.Text = "Conta";
        this.xrTableCell35.Weight = 0.44688100666041664D;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell37.StylePriority.UseBorders = false;
        this.xrTableCell37.StylePriority.UsePadding = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        this.xrTableCell37.Text = "Qtde.";
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell37.Weight = 0.10140094193397886D;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell39.Dpi = 254F;
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell39.StylePriority.UseBorders = false;
        this.xrTableCell39.StylePriority.UsePadding = false;
        this.xrTableCell39.StylePriority.UseTextAlignment = false;
        this.xrTableCell39.Text = "V. Unitário";
        this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell39.Weight = 0.13385167858620947D;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell40.Dpi = 254F;
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell40.StylePriority.UseBorders = false;
        this.xrTableCell40.StylePriority.UsePadding = false;
        this.xrTableCell40.StylePriority.UseTextAlignment = false;
        this.xrTableCell40.Text = "V. Proposto";
        this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell40.Weight = 0.16972372589643597D;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell42.Dpi = 254F;
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell42.StylePriority.UseBorders = false;
        this.xrTableCell42.StylePriority.UsePadding = false;
        this.xrTableCell42.StylePriority.UseTextAlignment = false;
        this.xrTableCell42.Text = "Memória de Cálculo";
        this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.xrTableCell42.Weight = 0.48331794974680031D;
        // 
        // nomeIniciativaComProjeto
        // 
        this.nomeIniciativaComProjeto.DataMember = "detalhesProposta";
        this.nomeIniciativaComProjeto.DisplayName = "nomeIniciativaComProjeto";
        this.nomeIniciativaComProjeto.Expression = "[NomeIniciativa]";
        this.nomeIniciativaComProjeto.FieldType = DevExpress.XtraReports.UI.FieldType.String;
        this.nomeIniciativaComProjeto.Name = "nomeIniciativaComProjeto";
        // 
        // DetailReport10
        // 
        this.DetailReport10.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail11,
            this.GroupHeader2,
            this.GroupHeader17,
            this.ReportFooter3,
            this.GroupFooter1,
            this.GroupHeader18});
        this.DetailReport10.DataMember = "CronogramaOrc";
        this.DetailReport10.DataSource = this.dsRelPropostaIniciativa1;
        this.DetailReport10.Dpi = 254F;
        this.DetailReport10.FilterString = "[EtapaOrcamento] > 1";
        this.DetailReport10.Level = 7;
        this.DetailReport10.Name = "DetailReport10";
        this.DetailReport10.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail11
        // 
        this.Detail11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable22});
        this.Detail11.Dpi = 254F;
        this.Detail11.HeightF = 50F;
        this.Detail11.Name = "Detail11";
        // 
        // xrTable22
        // 
        this.xrTable22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable22.Dpi = 254F;
        this.xrTable22.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable22.LocationFloat = new DevExpress.Utils.PointFloat(58.39001F, 0F);
        this.xrTable22.Name = "xrTable22";
        this.xrTable22.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow25});
        this.xrTable22.SizeF = new System.Drawing.SizeF(2496.649F, 50F);
        this.xrTable22.StylePriority.UseBorders = false;
        this.xrTable22.StylePriority.UseFont = false;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67,
            this.xrTableCell68});
        this.xrTableRow25.Dpi = 254F;
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 0.5679012345679012D;
        // 
        // xrTableCell62
        // 
        this.xrTableCell62.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell62.CanShrink = true;
        this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.CONTA_DES")});
        this.xrTableCell62.Dpi = 254F;
        this.xrTableCell62.Name = "xrTableCell62";
        this.xrTableCell62.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell62.StylePriority.UseBorders = false;
        this.xrTableCell62.StylePriority.UsePadding = false;
        this.xrTableCell62.Text = "xrTableCell15";
        this.xrTableCell62.Weight = 0.28860661180423425D;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell63.CanShrink = true;
        this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.MemoriaCalculo")});
        this.xrTableCell63.Dpi = 254F;
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell63.StylePriority.UseBorders = false;
        this.xrTableCell63.StylePriority.UsePadding = false;
        this.xrTableCell63.Text = "xrTableCell16";
        this.xrTableCell63.Weight = 0.25967533679016125D;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell64.CanShrink = true;
        this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorRealizado", "{0:c2}")});
        this.xrTableCell64.Dpi = 254F;
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.StylePriority.UsePadding = false;
        this.xrTableCell64.StylePriority.UseTextAlignment = false;
        this.xrTableCell64.Text = "xrTableCell25";
        this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell64.Weight = 0.13385167858620947D;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell65.CanShrink = true;
        this.xrTableCell65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DisponibilidadeAtual", "{0:c2}")});
        this.xrTableCell65.Dpi = 254F;
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell65.StylePriority.UseBorders = false;
        this.xrTableCell65.StylePriority.UsePadding = false;
        this.xrTableCell65.StylePriority.UseTextAlignment = false;
        this.xrTableCell65.Text = "xrTableCell26";
        this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell65.Weight = 0.16531752159151331D;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell66.CanShrink = true;
        this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorSuplemento", "{0:c2}")});
        this.xrTableCell66.Dpi = 254F;
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell66.StylePriority.UseBorders = false;
        this.xrTableCell66.StylePriority.UsePadding = false;
        this.xrTableCell66.StylePriority.UseTextAlignment = false;
        this.xrTableCell66.Text = "xrTableCell28";
        this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell66.Weight = 0.12884151437423441D;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell67.CanShrink = true;
        this.xrTableCell67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorTransposto", "{0:c2}")});
        this.xrTableCell67.Dpi = 254F;
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.StylePriority.UsePadding = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.Text = "xrTableCell34";
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell67.Weight = 0.15736593607468477D;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell68.CanShrink = true;
        this.xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DisponibilidadeReformulada", "{0:c2}")});
        this.xrTableCell68.Dpi = 254F;
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell68.StylePriority.UseBorders = false;
        this.xrTableCell68.StylePriority.UsePadding = false;
        this.xrTableCell68.StylePriority.UseTextAlignment = false;
        this.xrTableCell68.Text = "xrTableCell36";
        this.xrTableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell68.Weight = 0.2015167036028038D;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable19});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ordem", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 150.5001F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrTable19
        // 
        this.xrTable19.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable19.Dpi = 254F;
        this.xrTable19.KeepTogether = true;
        this.xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(0F, 46.37484F);
        this.xrTable19.Name = "xrTable19";
        this.xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow22});
        this.xrTable19.SizeF = new System.Drawing.SizeF(2555.038F, 50F);
        this.xrTable19.StylePriority.UseBorderColor = false;
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableRow22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell74});
        this.xrTableRow22.Dpi = 254F;
        this.xrTableRow22.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.StylePriority.UseBackColor = false;
        this.xrTableRow22.StylePriority.UseBorders = false;
        this.xrTableRow22.StylePriority.UseFont = false;
        this.xrTableRow22.Weight = 0.5679012345679012D;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTableCell46.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DescricaoAcao")});
        this.xrTableCell46.Dpi = 254F;
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell46.StylePriority.UseBackColor = false;
        this.xrTableCell46.StylePriority.UseBorderColor = false;
        this.xrTableCell46.StylePriority.UsePadding = false;
        this.xrTableCell46.Text = "xrTableCell95";
        this.xrTableCell46.Weight = 1.7813450677646745D;
        this.xrTableCell46.TextChanged += new System.EventHandler(this.xrTableCell46_TextChanged);
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.BorderColor = System.Drawing.Color.DimGray;
        this.xrTableCell74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.fonte")});
        this.xrTableCell74.Dpi = 254F;
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.StylePriority.UseBorderColor = false;
        this.xrTableCell74.Text = "xrTableCell74";
        this.xrTableCell74.Weight = 0.65481689529957721D;
        // 
        // GroupHeader17
        // 
        this.GroupHeader17.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotalUNDispReformulada,
            this.xrLabel39,
            this.xrTable18,
            this.xrLabel41,
            this.xrLabel36,
            this.lblTotalGeralDispRefEtapaOrcMaiorQue1,
            this.xrLabel38,
            this.lblTotalFUNDDispRefEtapaOrMairQue1});
        this.GroupHeader17.Dpi = 254F;
        this.GroupHeader17.HeightF = 232.8333F;
        this.GroupHeader17.Level = 2;
        this.GroupHeader17.Name = "GroupHeader17";
        // 
        // lblTotalUNDispReformulada
        // 
        this.lblTotalUNDispReformulada.BackColor = System.Drawing.Color.Transparent;
        this.lblTotalUNDispReformulada.BorderColor = System.Drawing.Color.DarkGray;
        this.lblTotalUNDispReformulada.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTotalUNDispReformulada.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DisponibilidadeReformulada")});
        this.lblTotalUNDispReformulada.Dpi = 254F;
        this.lblTotalUNDispReformulada.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblTotalUNDispReformulada.LocationFloat = new DevExpress.Utils.PointFloat(1140.296F, 147.1555F);
        this.lblTotalUNDispReformulada.Name = "lblTotalUNDispReformulada";
        this.lblTotalUNDispReformulada.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTotalUNDispReformulada.SizeF = new System.Drawing.SizeF(318.3314F, 50F);
        this.lblTotalUNDispReformulada.StylePriority.UseBackColor = false;
        this.lblTotalUNDispReformulada.StylePriority.UseBorderColor = false;
        this.lblTotalUNDispReformulada.StylePriority.UseBorders = false;
        this.lblTotalUNDispReformulada.StylePriority.UseFont = false;
        this.lblTotalUNDispReformulada.StylePriority.UseTextAlignment = false;
        xrSummary5.FormatString = "{0:c2}";
        xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
        xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lblTotalUNDispReformulada.Summary = xrSummary5;
        this.lblTotalUNDispReformulada.Text = "xrLabel32";
        this.lblTotalUNDispReformulada.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.lblTotalUNDispReformulada.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.lblTotalUNDispReformulada_SummaryGetResult);
        this.lblTotalUNDispReformulada.SummaryRowChanged += new System.EventHandler(this.lblTotalUNDispReformulada_SummaryRowChanged);
        // 
        // xrLabel39
        // 
        this.xrLabel39.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel39.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel39.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel39.Dpi = 254F;
        this.xrLabel39.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(981.3051F, 147.1555F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(158.9908F, 50F);
        this.xrLabel39.StylePriority.UseBackColor = false;
        this.xrLabel39.StylePriority.UseBorderColor = false;
        this.xrLabel39.StylePriority.UseBorders = false;
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.StylePriority.UseTextAlignment = false;
        this.xrLabel39.Text = "Total UN:";
        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable18
        // 
        this.xrTable18.BorderColor = System.Drawing.Color.DarkGray;
        this.xrTable18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable18.Dpi = 254F;
        this.xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(1.582764F, 147.1555F);
        this.xrTable18.Name = "xrTable18";
        this.xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20});
        this.xrTable18.SizeF = new System.Drawing.SizeF(969.7225F, 50F);
        this.xrTable18.StylePriority.UseBorderColor = false;
        this.xrTable18.StylePriority.UseBorders = false;
        // 
        // xrTableRow20
        // 
        this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell38});
        this.xrTableRow20.Dpi = 254F;
        this.xrTableRow20.Name = "xrTableRow20";
        this.xrTableRow20.Weight = 1D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BackColor = System.Drawing.Color.LightGray;
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell20.StylePriority.UseBackColor = false;
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.StylePriority.UsePadding = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "Projeto:";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell20.Weight = 0.49850676153818685D;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "detalhesProposta.nomeIniciativaComProjeto")});
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell38.StylePriority.UseFont = false;
        this.xrTableCell38.StylePriority.UsePadding = false;
        this.xrTableCell38.Text = "xrTableCell11";
        this.xrTableCell38.Weight = 3.1839260194694896D;
        // 
        // xrLabel41
        // 
        this.xrLabel41.BackColor = System.Drawing.Color.Gray;
        this.xrLabel41.Dpi = 254F;
        this.xrLabel41.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(1.582781F, 46.84449F);
        this.xrLabel41.Name = "xrLabel41";
        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel41.SizeF = new System.Drawing.SizeF(2553.456F, 50F);
        this.xrLabel41.StylePriority.UseBackColor = false;
        this.xrLabel41.StylePriority.UseFont = false;
        this.xrLabel41.StylePriority.UseTextAlignment = false;
        this.xrLabel41.Text = "Cronograma Orçamentário";
        this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // xrLabel36
        // 
        this.xrLabel36.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel36.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel36.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(2050.498F, 147.1555F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(191.0637F, 50F);
        this.xrLabel36.StylePriority.UseBackColor = false;
        this.xrLabel36.StylePriority.UseBorderColor = false;
        this.xrLabel36.StylePriority.UseBorders = false;
        this.xrLabel36.StylePriority.UseFont = false;
        this.xrLabel36.StylePriority.UseTextAlignment = false;
        this.xrLabel36.Text = "Total geral:";
        this.xrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblTotalGeralDispRefEtapaOrcMaiorQue1
        // 
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.BackColor = System.Drawing.Color.Transparent;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.BorderColor = System.Drawing.Color.DarkGray;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DisponibilidadeReformulada")});
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.Dpi = 254F;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.LocationFloat = new DevExpress.Utils.PointFloat(2241.562F, 147.1555F);
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.Name = "lblTotalGeralDispRefEtapaOrcMaiorQue1";
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.SizeF = new System.Drawing.SizeF(313.4766F, 50F);
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.StylePriority.UseBackColor = false;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.StylePriority.UseBorderColor = false;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.StylePriority.UseBorders = false;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.StylePriority.UseFont = false;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.StylePriority.UseTextAlignment = false;
        xrSummary6.FormatString = "{0:c2}";
        xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.Summary = xrSummary6;
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.Text = "lblTotalGeralDispRefEtapaOrcMaiorQue1";
        this.lblTotalGeralDispRefEtapaOrcMaiorQue1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabel38
        // 
        this.xrLabel38.BackColor = System.Drawing.Color.LightGray;
        this.xrLabel38.BorderColor = System.Drawing.Color.DarkGray;
        this.xrLabel38.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(1469.156F, 147.1555F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(290.0618F, 50F);
        this.xrLabel38.StylePriority.UseBackColor = false;
        this.xrLabel38.StylePriority.UseBorderColor = false;
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "Total FUNDECOOP:";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblTotalFUNDDispRefEtapaOrMairQue1
        // 
        this.lblTotalFUNDDispRefEtapaOrMairQue1.BackColor = System.Drawing.Color.Transparent;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.BorderColor = System.Drawing.Color.DarkGray;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.lblTotalFUNDDispRefEtapaOrMairQue1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DisponibilidadeReformulada")});
        this.lblTotalFUNDDispRefEtapaOrMairQue1.Dpi = 254F;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblTotalFUNDDispRefEtapaOrMairQue1.LocationFloat = new DevExpress.Utils.PointFloat(1759.218F, 147.1555F);
        this.lblTotalFUNDDispRefEtapaOrMairQue1.Name = "lblTotalFUNDDispRefEtapaOrMairQue1";
        this.lblTotalFUNDDispRefEtapaOrMairQue1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblTotalFUNDDispRefEtapaOrMairQue1.SizeF = new System.Drawing.SizeF(280.4583F, 50F);
        this.lblTotalFUNDDispRefEtapaOrMairQue1.StylePriority.UseBackColor = false;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.StylePriority.UseBorderColor = false;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.StylePriority.UseBorders = false;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.StylePriority.UseFont = false;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.StylePriority.UseTextAlignment = false;
        xrSummary7.FormatString = "{0:c2}";
        xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
        xrSummary7.IgnoreNullValues = true;
        xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.Summary = xrSummary7;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.Text = "xrLabel34";
        this.lblTotalFUNDDispRefEtapaOrMairQue1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.lblTotalFUNDDispRefEtapaOrMairQue1.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.lblTotalFUNDDispRefEtapaOrMairQue1_SummaryGetResult);
        this.lblTotalFUNDDispRefEtapaOrMairQue1.SummaryRowChanged += new System.EventHandler(this.lblTotalFUNDDispRefEtapaOrMairQue1_SummaryRowChanged);
        // 
        // ReportFooter3
        // 
        this.ReportFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine4});
        this.ReportFooter3.Dpi = 254F;
        this.ReportFooter3.HeightF = 58.42F;
        this.ReportFooter3.Name = "ReportFooter3";
        // 
        // xrLine4
        // 
        this.xrLine4.Dpi = 254F;
        this.xrLine4.ForeColor = System.Drawing.Color.Transparent;
        this.xrLine4.LineWidth = 3;
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(254F, 58.42F);
        this.xrLine4.StylePriority.UseForeColor = false;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable23});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 49.99999F;
        this.GroupFooter1.Level = 1;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable23
        // 
        this.xrTable23.BackColor = System.Drawing.Color.NavajoWhite;
        this.xrTable23.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable23.Dpi = 254F;
        this.xrTable23.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable23.LocationFloat = new DevExpress.Utils.PointFloat(58.39F, 0F);
        this.xrTable23.Name = "xrTable23";
        this.xrTable23.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
        this.xrTable23.SizeF = new System.Drawing.SizeF(2496.649F, 49.99999F);
        this.xrTable23.StylePriority.UseBackColor = false;
        this.xrTable23.StylePriority.UseBorders = false;
        this.xrTable23.StylePriority.UseFont = false;
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71});
        this.xrTableRow26.Dpi = 254F;
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 0.5679012345679012D;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.Text = "Total:";
        this.xrTableCell34.Weight = 0.54912662735234774D;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorRealizado")});
        this.xrTableCell43.Dpi = 254F;
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell43.StylePriority.UseBorders = false;
        this.xrTableCell43.StylePriority.UsePadding = false;
        this.xrTableCell43.StylePriority.UseTextAlignment = false;
        xrSummary8.FormatString = "{0:c2}";
        xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell43.Summary = xrSummary8;
        this.xrTableCell43.Text = "xrTableCell43";
        this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell43.Weight = 0.13300699982825712D;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DisponibilidadeAtual")});
        this.xrTableCell44.Dpi = 254F;
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell44.StylePriority.UseBorders = false;
        this.xrTableCell44.StylePriority.UsePadding = false;
        this.xrTableCell44.StylePriority.UseTextAlignment = false;
        xrSummary9.FormatString = "{0:c2}";
        xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell44.Summary = xrSummary9;
        this.xrTableCell44.Text = "xrTableCell44";
        this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell44.Weight = 0.16531748172291708D;
        // 
        // xrTableCell69
        // 
        this.xrTableCell69.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorSuplemento")});
        this.xrTableCell69.Dpi = 254F;
        this.xrTableCell69.Name = "xrTableCell69";
        this.xrTableCell69.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell69.StylePriority.UseBorders = false;
        this.xrTableCell69.StylePriority.UsePadding = false;
        this.xrTableCell69.StylePriority.UseTextAlignment = false;
        xrSummary10.FormatString = "{0:c2}";
        xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell69.Summary = xrSummary10;
        this.xrTableCell69.Text = "xrTableCell69";
        this.xrTableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell69.Weight = 0.12884164992746161D;
        // 
        // xrTableCell70
        // 
        this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.ValorTransposto")});
        this.xrTableCell70.Dpi = 254F;
        this.xrTableCell70.Name = "xrTableCell70";
        this.xrTableCell70.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell70.StylePriority.UseBorders = false;
        this.xrTableCell70.StylePriority.UsePadding = false;
        this.xrTableCell70.StylePriority.UseTextAlignment = false;
        xrSummary11.FormatString = "{0:c2}";
        xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell70.Summary = xrSummary11;
        this.xrTableCell70.Text = "xrTableCell70";
        this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell70.Weight = 0.15736584039005377D;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CronogramaOrc.DisponibilidadeReformulada")});
        this.xrTableCell71.Dpi = 254F;
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell71.StylePriority.UseBorders = false;
        this.xrTableCell71.StylePriority.UsePadding = false;
        this.xrTableCell71.StylePriority.UseTextAlignment = false;
        xrSummary12.FormatString = "{0:c2}";
        xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell71.Summary = xrSummary12;
        this.xrTableCell71.Text = "xrTableCell71";
        this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell71.Weight = 0.2015167036028038D;
        // 
        // GroupHeader18
        // 
        this.GroupHeader18.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable21});
        this.GroupHeader18.Dpi = 254F;
        this.GroupHeader18.HeightF = 49.99998F;
        this.GroupHeader18.Name = "GroupHeader18";
        this.GroupHeader18.RepeatEveryPage = true;
        // 
        // xrTable21
        // 
        this.xrTable21.BackColor = System.Drawing.Color.Gainsboro;
        this.xrTable21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable21.Dpi = 254F;
        this.xrTable21.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable21.LocationFloat = new DevExpress.Utils.PointFloat(58.39001F, 0F);
        this.xrTable21.Name = "xrTable21";
        this.xrTable21.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow24});
        this.xrTable21.SizeF = new System.Drawing.SizeF(2496.649F, 49.99998F);
        this.xrTable21.StylePriority.UseBackColor = false;
        this.xrTable21.StylePriority.UseBorders = false;
        this.xrTable21.StylePriority.UseFont = false;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell60,
            this.xrTableCell61});
        this.xrTableRow24.Dpi = 254F;
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.Weight = 0.5679012345679012D;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.Dpi = 254F;
        this.xrTableCell47.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell47.StylePriority.UseFont = false;
        this.xrTableCell47.StylePriority.UsePadding = false;
        this.xrTableCell47.Text = "Conta";
        this.xrTableCell47.Weight = 0.28860661180423425D;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell48.Dpi = 254F;
        this.xrTableCell48.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
        this.xrTableCell48.StylePriority.UseBorders = false;
        this.xrTableCell48.StylePriority.UseFont = false;
        this.xrTableCell48.StylePriority.UsePadding = false;
        this.xrTableCell48.Text = "Memória de cálculo";
        this.xrTableCell48.Weight = 0.25967533679016125D;
        // 
        // xrTableCell57
        // 
        this.xrTableCell57.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell57.Dpi = 254F;
        this.xrTableCell57.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell57.Name = "xrTableCell57";
        this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell57.StylePriority.UseBorders = false;
        this.xrTableCell57.StylePriority.UseFont = false;
        this.xrTableCell57.StylePriority.UsePadding = false;
        this.xrTableCell57.StylePriority.UseTextAlignment = false;
        this.xrTableCell57.Text = "Valor Realizado";
        this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell57.Weight = 0.13385167858620947D;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell58.Dpi = 254F;
        this.xrTableCell58.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell58.StylePriority.UseBorders = false;
        this.xrTableCell58.StylePriority.UseFont = false;
        this.xrTableCell58.StylePriority.UsePadding = false;
        this.xrTableCell58.StylePriority.UseTextAlignment = false;
        this.xrTableCell58.Text = "Disponibilidade Atual";
        this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell58.Weight = 0.16531760132870577D;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell59.Dpi = 254F;
        this.xrTableCell59.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell59.StylePriority.UseBorders = false;
        this.xrTableCell59.StylePriority.UseFont = false;
        this.xrTableCell59.StylePriority.UsePadding = false;
        this.xrTableCell59.StylePriority.UseTextAlignment = false;
        this.xrTableCell59.Text = "Suplementado";
        this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell59.Weight = 0.12884138669823975D;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell60.Dpi = 254F;
        this.xrTableCell60.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell60.StylePriority.UseBorders = false;
        this.xrTableCell60.StylePriority.UseFont = false;
        this.xrTableCell60.StylePriority.UsePadding = false;
        this.xrTableCell60.StylePriority.UseTextAlignment = false;
        this.xrTableCell60.Text = "Transposto";
        this.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell60.Weight = 0.15736598401348695D;
        // 
        // xrTableCell61
        // 
        this.xrTableCell61.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell61.Dpi = 254F;
        this.xrTableCell61.Font = new System.Drawing.Font("Verdana", 7F);
        this.xrTableCell61.Name = "xrTableCell61";
        this.xrTableCell61.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254F);
        this.xrTableCell61.StylePriority.UseBorders = false;
        this.xrTableCell61.StylePriority.UseFont = false;
        this.xrTableCell61.StylePriority.UsePadding = false;
        this.xrTableCell61.StylePriority.UseTextAlignment = false;
        this.xrTableCell61.Text = "Disponibilidade Reformulada";
        this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell61.Weight = 0.2015167036028038D;
        // 
        // relImpressaoTai_001
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
            this.DetailReport11,
            this.DetailReport10});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.titElementoDeParceriaCalc,
            this.titAreaDeParceriaCalc,
            this.indicaProdutoMeta,
            this.indicaAcaoAtividade,
            this.titMarcoProjeto,
            this.titData,
            this.titLocalEvento,
            this.titDetalhesEvento,
            this.indicaInstitucional,
            this.nomeIniciativaComProjeto});
        this.DataMember = "detalhesProposta";
        this.DataSource = this.dsRelPropostaIniciativa1;
        this.Dpi = 254F;
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(152, 5, 10, 10);
        this.PageHeight = 2159;
        this.PageWidth = 2794;
        this.ReportPrintOptions.PrintOnEmptyDataSource = false;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 50F;
        this.SnappingMode = SnappingMode.SnapToGrid;
        this.Version = "12.1";
        ((System.ComponentModel.ISupportInitialize)(this.dsRelPropostaIniciativa1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbDadosEvento)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.tbEventoDetalhe)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //int codUnidadeNegocio = 0;
        //string nomeUnidadeNegocio = "";
        //string inicioProposta = "";
        //DataSet dsProjeto = cDados.getProjetos(" and p.CodigoProjeto = " + codigoProjetoGlobal);
        //if (cDados.DataSetOk(dsProjeto) && cDados.DataTableOk(dsProjeto.Tables[0]))
        //{
        //    int outint = 0;
        //    if (int.TryParse(dsProjeto.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString(), out outint) == true)
        //        codUnidadeNegocio = int.Parse(dsProjeto.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());
        //    inicioProposta = dsProjeto.Tables[0].Rows[0]["InicioProposta"].ToString();
        //}

        //DataSet dsCodUnidade = cDados.getEntidades(" and e.CodigoUnidadeNegocio = " + codUnidadeNegocio);
        //if (cDados.DataSetOk(dsCodUnidade) && cDados.DataTableOk(dsCodUnidade.Tables[0]))
        //{
        //    nomeUnidadeNegocio = dsProjeto.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();
        //}
        //lblCabecalho.Text = string.Format("SESCOOP - Proposta de impressão do TAI elaborada em 11/10/2012", nomeUnidadeNegocio, inicioProposta);
        lblCabecalho.Text = "Termo de Abertura de Iniciativa";
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
        else if (lblFonteRecurso.Text == "RP")
        {
            lblFonteRecurso.Text = "Recursos Próprios";
        }
        else if (lblFonteRecurso.Text == "AO")
        {
            lblFonteRecurso.Text = "FUNDECOOP e Recursos Próprios";
        }
        else if (celFonteRecurso.Text == "SR")
        {
            celFonteRecurso.Text = "Sem fonte de recursos";
        }
        else
        {
            lblFonteRecurso.Text = "Não informado";
        }
    }

    private void lblAreaAtuacao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        if (lblAreaAtuacao.Text == "FO")
        {
            lblAreaAtuacao.Text = "Formação e Capacitação Profissional";
        }
        else if (lblAreaAtuacao.Text == "MO")
        {
            lblAreaAtuacao.Text = "Monitoramento e Desenvolvimento de Cooperativas";
        }
        else if (lblAreaAtuacao.Text == "PR")
        {
            lblAreaAtuacao.Text = "Promoção Social";
        }
        else if (lblAreaAtuacao.Text == "GE")
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
        else if (lblAreaAtuacao1.Text == "MO")
        {
            lblAreaAtuacao1.Text = "Monitoramento e Desenvolvimento de Cooperativas";
        }
        else if (lblAreaAtuacao1.Text == "PR")
        {
            lblAreaAtuacao1.Text = "Promoção Social";
        }
        else if (lblAreaAtuacao1.Text == "GE")
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
        else if (celFonteRecurso.Text == "RP")
        {
            celFonteRecurso.Text = "Recursos Próprios";
        }
        else if (celFonteRecurso.Text == "AO")
        {
            celFonteRecurso.Text = "FUNDECOOP e Recursos Próprios";
        }
        else if (celFonteRecurso.Text == "SR")
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
            tbEventoDetalhe.Visible = true;
            xrTable20.Visible = false;
            xrSubreport1.Visible = true;
            celulaTituloProduto.Visible = true;
            lblMeta.Visible = false;
            lblMeta.Text = "";
            xrTableRow18.BackColor = Color.LightGray;

            xrTableCell33.BackColor = Color.LightGray;
            celTituloAtividadeAcao.BackColor = Color.LightGray;
            xrTableCell50.BackColor = Color.LightGray;
            xrTableCell53.BackColor = Color.LightGray;
            xrTableCell54.BackColor = Color.LightGray;
            xrTableCell55.BackColor = Color.LightGray;
            xrTableCell56.BackColor = Color.LightGray;

        }
        else
        {
            //é uma ação
            xrTable13.Visible = false;
            xrTable14.Visible = false;
            xrTable16.Visible = false;
            xrTable15.Visible = false;
            tbEventoDetalhe.Visible = false;
            xrTable20.Visible = true;
            xrSubreport1.Visible = false;
            celulaTituloProduto.Visible = false;
            lblMeta.Visible = true;
            lblMeta.Text = "Meta";
            xrTableRow18.BackColor = Color.DarkGray;


            xrTableCell33.BackColor = Color.DarkGray;
            celTituloAtividadeAcao.BackColor = Color.DarkGray;
            xrTableCell50.BackColor = Color.DarkGray;
            xrTableCell53.BackColor = Color.DarkGray;
            xrTableCell54.BackColor = Color.DarkGray;
            xrTableCell55.BackColor = Color.DarkGray;
            xrTableCell56.BackColor = Color.DarkGray;

        }




    }

    private void celFonteRecurso2_TextChanged(object sender, EventArgs e)
    {
        /*if (celFonteRecurso2.Text == "FU")
        {
            celFonteRecurso2.Text = "FUNDECOOP";
        }
        else if (celFonteRecurso2.Text == "RP")
        {
            celFonteRecurso2.Text = "Recursos Próprios";
        }
        else if (celFonteRecurso2.Text == "AO")
        {
            celFonteRecurso2.Text = "FUNDECOOP e Recursos Próprios";
        }
        else if (celFonteRecurso2.Text == "SR")
        {
            celFonteRecurso2.Text = "Sem fonte de recursos";
        }*/
    }

    private void xrTableCell95_TextChanged(object sender, EventArgs e)
    {
        if (xrTableCell95.Text.IndexOf(".") != -1)//achou ponto
        {
            /*
             * LightGrey #D3D3D3
             */
            //é uma atividade
            //xrTable12.Visible = true;
            xrTableCell95.BackColor = Color.LightGray;
            //celFonteRecurso2.BackColor = Color.LightGray;
            //celTotal.BackColor = Color.LightGray;
            xrTable17.BackColor = Color.LightGray;

            xrTableRow18.BackColor = Color.LightGray;
            xrTableCell33.BackColor = Color.LightGray;
            xrTableCell72.BackColor = Color.LightGray;
        }
        else
        {
            /*
             * DarkGray #A9A9A9
             */
            //é uma ação
            //xrTable12.Visible = false;
            xrTableCell95.BackColor = Color.DarkGray;
            //celFonteRecurso2.BackColor = Color.DarkGray;
            //celTotal.BackColor = Color.DarkGray;
            xrTable17.BackColor = Color.DarkGray;
            xrTableRow18.BackColor = Color.DarkGray;
            xrTableCell33.BackColor = Color.DarkGray;
            xrTableCell72.BackColor = Color.DarkGray;
        }
    }


    private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        subReportProduto_Tai001 report = (subReportProduto_Tai001)xrSubreport1.ReportSource;
        report.codigoAtividade.Value = xrSubreport1.Report.GetCurrentColumnValue("Codigo");
    }

    private void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell17.Text == "")
        {
            xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            tbEventoDetalhe.Borders = DevExpress.XtraPrinting.BorderSide.None;
            xrTableRow21.Borders = DevExpress.XtraPrinting.BorderSide.None;
        }
        else
        {
            xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.All;
            tbEventoDetalhe.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTableRow21.Borders = DevExpress.XtraPrinting.BorderSide.All;
        }
    }

    private void xrTableCell31_TextChanged(object sender, EventArgs e)
    {
        //indica se é um evento institucional
        if (xrTableCell31.Text == "Sim")
        {

            tbEventoDetalhe.Visible = true;
            tbDadosEvento.Visible = true;
        }
        else
        {
            tbEventoDetalhe.Visible = false;
            tbDadosEvento.Visible = false;
        }
    }

    private void lblTotalUNDispReformulada_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        //[EtapaOrcamento] > 1
        e.Result = totalUN;
        e.Handled = true;
    }

    private void lblTotalUNDispReformulada_SummaryRowChanged(object sender, EventArgs e)
    {
        //[EtapaOrcamento] > 1
        XRLabel lbl = (XRLabel)sender;
        string fonteRecurso1 = (string)lbl.Report.GetCurrentColumnValue("FonteRecurso");
        if (fonteRecurso1 == "RP" && !Convert.IsDBNull(lbl.Report.GetCurrentColumnValue("DisponibilidadeReformulada")))
        {
            totalUN += Convert.ToDecimal(lbl.Report.GetCurrentColumnValue("DisponibilidadeReformulada"));
        }
    }

    private void lblTotalFUNDDispRefEtapaOrMairQue1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        //Etapa Orcamento > 1
        e.Result = totalFundecoop;
        e.Handled = true;
    }

    private void lblTotalFUNDDispRefEtapaOrMairQue1_SummaryRowChanged(object sender, EventArgs e)
    {
        //Etapa Orcamento > 1
        XRLabel lbl = (XRLabel)sender;
        string fonteRecurso1 = (string)lbl.Report.GetCurrentColumnValue("FonteRecurso");
        if (fonteRecurso1 == "FU" && !Convert.IsDBNull(lbl.Report.GetCurrentColumnValue("DisponibilidadeReformulada")))
            totalFundecoop += Convert.ToDecimal(lbl.Report.GetCurrentColumnValue("DisponibilidadeReformulada"));

    }

    private void lblTotalUNEtapaOrcMenorQue2ValProp_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        //Etapa Orcamento < 2
        e.Result = totalUN;
        e.Handled = true;
    }

    private void lblTotalUNEtapaOrcMenorQue2ValProp_SummaryRowChanged(object sender, EventArgs e)
    {
        //Etapa Orcamento < 2
        //vem fu e rp
        XRLabel lbl = (XRLabel)sender;
        string fonteRecurso1 = (string)lbl.Report.GetCurrentColumnValue("FonteRecurso");
        if (fonteRecurso1 == "RP" && !Convert.IsDBNull(lbl.Report.GetCurrentColumnValue("ValorProposto")))
            totalUN += Convert.ToDecimal(lbl.Report.GetCurrentColumnValue("ValorProposto"));
    }

    private void lblTotalFundecoopEtapaOrcMenorQue2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        //Etapa Orcamento < 2
        e.Result = totalFundecoop;
        e.Handled = true;
    }

    private void lblTotalFundecoopEtapaOrcMenorQue2_SummaryRowChanged(object sender, EventArgs e)
    {
        //Etapa Orcamento < 2
        XRLabel lbl = (XRLabel)sender;
        string fonteRecurso1 = (string)lbl.Report.GetCurrentColumnValue("FonteRecurso");
        if (fonteRecurso1 == "FU" && !Convert.IsDBNull(lbl.Report.GetCurrentColumnValue("ValorProposto")))
        {
            totalFundecoop += Convert.ToDecimal(lbl.Report.GetCurrentColumnValue("ValorProposto"));
        }

    }

    private void xrTableCell46_TextChanged(object sender, EventArgs e)
    {
        if (xrTableCell46.Text.IndexOf(".") != -1)//achou ponto
        {

            xrTableCell74.BackColor = Color.LightGray;
            xrTableCell46.BackColor = Color.LightGray;
        }
        else
        {
            xrTableCell74.BackColor = Color.DarkGray;
            xrTableCell46.BackColor = Color.DarkGray;
        }
    }
}
