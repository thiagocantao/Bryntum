using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for relImpressaoTai
/// </summary>
public class relImpressaoTai : DevExpress.XtraReports.UI.XtraReport
{
    private dsImpressaoTai ds;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private XRControlStyle TableCaption;
    private XRControlStyle TableDataField;
    private XRPictureBox picLogoEntidade;
    private DetailReportBand DetailReport;
    private DetailBand Detail10;
    private GroupHeaderBand GroupHeader6;
    private DetailReportBand DetailReport1;
    private DetailBand Detail11;
    private GroupHeaderBand GroupHeader7;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public relImpressaoTai(Int32 codigoProjeto)
    {
        InitializeComponent();

        InitData(codigoProjeto);
    }

    private void InitData(int codigoProjeto)
    {
        DefineLogoEntidade();
        string connectionString = ConnectionString;
        string comandoSql;
        #region Comando SQL
        comandoSql = string.Format(@"

--SELECT CR, CodigoProjeto, DataInicio, DataTermino, DescricaoDesafio, DescricaoDirecionadorEstrategico, Escopo, Justificativa, LimiteEscopo, NomeFocoEstrategico, NomeGerenteIniciativa, NomeIniciativa, NomeUnidadeNegocio, ObjetivoGeral, Premissas, PublicoAlvo, Restricoes, ValorEstimado FROM TermoAbertura WHERE (CodigoProjeto = @CodigoProjeto)
SELECT   ta.CR, ta.CodigoProjeto, tp.TipoProjeto as TipoIniciativa,
ta.DataInicio, ta.DataTermino, 
ta.DescricaoDesafio, 
ta.DescricaoDirecionadorEstrategico, ta.Escopo, ta.Justificativa, 
ta.LimiteEscopo, ta.NomeFocoEstrategico, ta.NomeGerenteIniciativa, ta.NomeIniciativa, ta.NomeUnidadeNegocio, ta.ObjetivoGeral, ta.Premissas, ta.PublicoAlvo, ta.Restricoes, ta.ValorEstimado FROM TermoAbertura ta
inner join Projeto pr on (pr.CodigoProjeto = ta.CodigoProjeto)
inner join TipoProjeto tp on (tp.CodigoTipoProjeto = pr.CodigoTipoProjeto)
WHERE (ta.CodigoProjeto = @CodigoProjeto)

SELECT CodigoEntidadeResponsavel, CodigoProjeto, CodigoUsuarioResponsavel, Inicio, NomeAcao, NomeEntidadeResponsavel, NomeUsuarioResponsavel, SequenciaAcao, Termino, ValorPrevisto FROM tai_AcoesIniciativa WHERE (CodigoProjeto = @CodigoProjeto) ORDER BY NomeAcao


SELECT CodigoProjeto, DataLimitePrevista, (SELECT NomeAcao FROM tai_AcoesIniciativa AS a WHERE (CodigoProjeto = m.CodigoProjeto) AND (SequenciaAcao = m.SequenciaAcao)) AS NomeAcao, NomeMarco, SequenciaAcao, SequenciaRegistro FROM tai_MarcosAcoesIniciativa AS m WHERE (CodigoProjeto = @CodigoProjeto) ORDER BY NomeMarco


SELECT        CodigoProjeto, DataPrevistaEntrega, CASE GrauInfluencia WHEN 'B' THEN 'Baixo' WHEN 'M' THEN 'Médio' WHEN 'A' THEN 'Alto' END AS GrauInfluencia, 
                         Interlocutor, NomeParceiro, ProdutoSolicitado, SequenciaRegistro, CASE TipoParceiro WHEN 'I' THEN 'Interno' WHEN 'E' THEN 'Externo' END AS TipoParceiro
FROM            tai_ParceirosIniciativa
WHERE        (CodigoProjeto = @CodigoProjeto)
ORDER BY NomeParceiro


SELECT CodigoProjeto, DataLimitePrevista, DescricaoProduto, (SELECT NomeAcao FROM tai_AcoesIniciativa AS a WHERE (CodigoProjeto = p.CodigoProjeto) AND (SequenciaAcao = p.SequenciaAcao)) AS NomeAcao, Quantidade, SequenciaAcao, SequenciaRegistro FROM tai_ProdutosAcoesIniciativa AS p WHERE (CodigoProjeto = @CodigoProjeto) ORDER BY DescricaoProduto


SELECT CodigoIndicador, CodigoProjeto, DataLimitePrevista, NomeIndicador, SequenciaRegistro, SetencaResultado, TransformacaoProduto, ValorFinalTransformacao, ValorInicialTransformacao FROM tai_ResultadosIniciativa WHERE (CodigoProjeto = @CodigoProjeto) ORDER BY SetencaResultado");
        #endregion
        SqlDataAdapter adapter = new SqlDataAdapter(comandoSql, connectionString);
        adapter.SelectCommand.Parameters.Add(new SqlParameter("@CodigoProjeto", codigoProjeto));
        adapter.TableMappings.Clear();
        adapter.TableMappings.Add("Table", "TermoAbertura");
        adapter.TableMappings.Add("Table1", "tai_AcoesIniciativa");
        adapter.TableMappings.Add("Table2", "tai_MarcosAcoesIniciativa");
        adapter.TableMappings.Add("Table3", "tai_ParceirosIniciativa");
        adapter.TableMappings.Add("Table4", "tai_ProdutosAcoesIniciativa");
        adapter.TableMappings.Add("Table5", "tai_ResultadosIniciativa");

        adapter.Fill(ds);
        #region Comentario
        /*TermoAberturaTableAdapter termoAberturaAdapter = new TermoAberturaTableAdapter();
        tai_AcoesIniciativaTableAdapter acoesIniciativaAdapter = new tai_AcoesIniciativaTableAdapter();
        tai_MarcosAcoesIniciativaTableAdapter marcosAcoesIniciativaAdapter = new tai_MarcosAcoesIniciativaTableAdapter();
        tai_ParceirosIniciativaTableAdapter parceirosIniciativaAdapter = new tai_ParceirosIniciativaTableAdapter();
        tai_ProdutosAcoesIniciativaTableAdapter produtosAcoesIniciativaAdapter = new tai_ProdutosAcoesIniciativaTableAdapter();
        tai_ResultadosIniciativaTableAdapter resultadosIniciativaAdapter = new tai_ResultadosIniciativaTableAdapter();

        termoAberturaAdapter.Connection.ConnectionString =
            acoesIniciativaAdapter.Connection.ConnectionString =
            marcosAcoesIniciativaAdapter.Connection.ConnectionString =
            parceirosIniciativaAdapter.Connection.ConnectionString =
            produtosAcoesIniciativaAdapter.Connection.ConnectionString =
            resultadosIniciativaAdapter.Connection.ConnectionString = connectionString;

        termoAberturaAdapter.FillByCodigoProjeto(ds.TermoAbertura, codigoProjeto);
        acoesIniciativaAdapter.FillByCodigoProjeto(ds.tai_AcoesIniciativa, codigoProjeto);
        marcosAcoesIniciativaAdapter.FillByCodigoProjeto(ds.tai_MarcosAcoesIniciativa, codigoProjeto);
        parceirosIniciativaAdapter.FillByCodigoProjeto(ds.tai_ParceirosIniciativa, codigoProjeto);
        produtosAcoesIniciativaAdapter.FillByCodigoProjeto(ds.tai_ProdutosAcoesIniciativa, codigoProjeto);
        resultadosIniciativaAdapter.FillByCodigoProjeto(ds.tai_ResultadosIniciativa, codigoProjeto);*/

        #endregion
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

    private String ConnectionString
    {
        get
        {
            dados cDados = CdadosUtil.GetCdados(null);
            return cDados.classeDados.getStringConexao();
        }
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
        string resourceFileName = "relImpressaoTai.resx";
        DevExpress.XtraReports.UI.DetailBand Detail;
        DevExpress.XtraReports.UI.XRLine xrLine1;
        DevExpress.XtraReports.UI.PageFooterBand pageFooterBand1;
        DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        DevExpress.XtraReports.UI.XRLabel xrLabel35;
        DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        DevExpress.XtraReports.UI.DetailReportBand detailIdentificacao;
        DevExpress.XtraReports.UI.DetailBand Detail1;
        DevExpress.XtraReports.UI.XRLabel xrLabel17;
        DevExpress.XtraReports.UI.XRLabel xrLabel16;
        DevExpress.XtraReports.UI.XRLabel xrLabel34;
        DevExpress.XtraReports.UI.XRLabel xrLabel33;
        DevExpress.XtraReports.UI.XRLabel xrLabel3;
        DevExpress.XtraReports.UI.XRLabel xrLabel4;
        DevExpress.XtraReports.UI.XRLabel xrLabel1;
        DevExpress.XtraReports.UI.XRLabel xrLabel2;
        DevExpress.XtraReports.UI.XRLabel xrLabel7;
        DevExpress.XtraReports.UI.XRLabel xrLabel8;
        DevExpress.XtraReports.UI.XRLabel xrLabel5;
        DevExpress.XtraReports.UI.XRLabel xrLabel20;
        DevExpress.XtraReports.UI.XRLabel xrLabel21;
        DevExpress.XtraReports.UI.XRLabel xrLabel18;
        DevExpress.XtraReports.UI.XRLabel xrLabel19;
        DevExpress.XtraReports.UI.XRLabel xrLabel24;
        DevExpress.XtraReports.UI.XRLabel xrLabel25;
        DevExpress.XtraReports.UI.XRLabel xrLabel22;
        DevExpress.XtraReports.UI.XRLabel xrLabel28;
        DevExpress.XtraReports.UI.XRLabel xrLabel27;
        DevExpress.XtraReports.UI.XRLabel xrLabel26;
        DevExpress.XtraReports.UI.XRLabel xrLabel11;
        DevExpress.XtraReports.UI.XRLabel xrLabel10;
        DevExpress.XtraReports.UI.XRLabel xrLabel9;
        DevExpress.XtraReports.UI.DetailReportBand detailElementosResultado1;
        DevExpress.XtraReports.UI.DetailBand Detail2;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport3;
        DevExpress.XtraReports.UI.DetailBand Detail4;
        DevExpress.XtraReports.UI.XRTable xrTable2;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        DevExpress.XtraReports.UI.XRLabel xrLabel6;
        DevExpress.XtraReports.UI.XRTable xrTable1;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        DevExpress.XtraReports.UI.DetailReportBand detailElementosResultado2;
        DevExpress.XtraReports.UI.DetailBand Detail3;
        DevExpress.XtraReports.UI.XRLabel xrLabel30;
        DevExpress.XtraReports.UI.XRLabel xrLabel29;
        DevExpress.XtraReports.UI.XRLabel xrLabel32;
        DevExpress.XtraReports.UI.XRLabel xrLabel31;
        DevExpress.XtraReports.UI.XRLabel xrLabel13;
        DevExpress.XtraReports.UI.XRLabel xrLabel12;
        DevExpress.XtraReports.UI.XRLabel xrLabel15;
        DevExpress.XtraReports.UI.XRLabel xrLabel14;
        DevExpress.XtraReports.UI.DetailReportBand DetailReport4;
        DevExpress.XtraReports.UI.DetailBand Detail5;
        DevExpress.XtraReports.UI.XRTable xrTable4;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell22;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell24;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        DevExpress.XtraReports.UI.XRLabel xrLabel23;
        DevExpress.XtraReports.UI.XRTable xrTable3;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        DevExpress.XtraReports.UI.DetailReportBand detailElementosOperacionais;
        DevExpress.XtraReports.UI.DetailBand Detail9;
        DevExpress.XtraReports.UI.DetailReportBand detailAcoes;
        DevExpress.XtraReports.UI.DetailBand Detail6;
        DevExpress.XtraReports.UI.XRTable xrTable6;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell35;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell36;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell38;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell39;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell40;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell41;
        DevExpress.XtraReports.UI.XRTable xrTable5;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell28;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell31;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell32;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell33;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
        DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader3;
        DevExpress.XtraReports.UI.XRLabel xrLabel36;
        DevExpress.XtraReports.UI.XRTable xrTable7;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell27;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell42;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell37;
        DevExpress.XtraReports.UI.XRTable xrTable8;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow10;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell47;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell48;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell26;
        DevExpress.XtraReports.UI.XRTable xrTable9;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow11;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell44;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell46;
        DevExpress.XtraReports.UI.XRTable xrTable10;
        DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell52;
        DevExpress.XtraReports.UI.XRTableCell xrTableCell43;
        DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        DevExpress.XtraReports.UI.XRLabel xrLabel37;
        DevExpress.XtraReports.UI.XRLabel xrLabel38;
        this.ds = new dsImpressaoTai();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail10 = new DevExpress.XtraReports.UI.DetailBand();
        this.GroupHeader6 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail11 = new DevExpress.XtraReports.UI.DetailBand();
        this.GroupHeader7 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.picLogoEntidade = new DevExpress.XtraReports.UI.XRPictureBox();
        this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
        this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
        this.TableCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.TableDataField = new DevExpress.XtraReports.UI.XRControlStyle();
        Detail = new DevExpress.XtraReports.UI.DetailBand();
        xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand();
        xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        detailIdentificacao = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        detailElementosResultado1 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        detailElementosResultado2 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail5 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        detailElementosOperacionais = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail9 = new DevExpress.XtraReports.UI.DetailBand();
        detailAcoes = new DevExpress.XtraReports.UI.DetailReportBand();
        Detail6 = new DevExpress.XtraReports.UI.DetailBand();
        xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        Detail.Dpi = 254F;
        Detail.HeightF = 0F;
        Detail.MultiColumn.ColumnSpacing = 25.4F;
        Detail.MultiColumn.ColumnWidth = 901.7F;
        Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
        Detail.Name = "Detail";
        Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        Detail.StyleName = "DataField";
        Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLine1
        // 
        xrLine1.Dpi = 254F;
        xrLine1.LineWidth = 3;
        xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(16F, 160F);
        xrLine1.Name = "xrLine1";
        xrLine1.SizeF = new System.Drawing.SizeF(1904F, 5F);
        // 
        // pageFooterBand1
        // 
        pageFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrPageInfo1,
            xrPageInfo2});
        pageFooterBand1.Dpi = 254F;
        pageFooterBand1.HeightF = 74.42F;
        pageFooterBand1.Name = "pageFooterBand1";
        // 
        // xrPageInfo1
        // 
        xrPageInfo1.Dpi = 254F;
        xrPageInfo1.Format = "Emitido em {0:dd/MM/yyyy - HH:mm}";
        xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(16F, 16F);
        xrPageInfo1.Name = "xrPageInfo1";
        xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        xrPageInfo1.SizeF = new System.Drawing.SizeF(936F, 58.42F);
        xrPageInfo1.StyleName = "PageInfo";
        // 
        // xrPageInfo2
        // 
        xrPageInfo2.Dpi = 254F;
        xrPageInfo2.Format = "Pág. {0}/{1}";
        xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(984F, 16F);
        xrPageInfo2.Name = "xrPageInfo2";
        xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrPageInfo2.SizeF = new System.Drawing.SizeF(936F, 58.42F);
        xrPageInfo2.StyleName = "PageInfo";
        xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrLabel35
        // 
        xrLabel35.Dpi = 254F;
        xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(349.9999F, 25.00001F);
        xrLabel35.Name = "xrLabel35";
        xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel35.SizeF = new System.Drawing.SizeF(1570F, 84F);
        xrLabel35.StyleName = "Title";
        xrLabel35.StylePriority.UseFont = false;
        xrLabel35.StylePriority.UseForeColor = false;
        xrLabel35.Text = "Termo de Abertura";
        // 
        // topMarginBand1
        // 
        topMarginBand1.Dpi = 254F;
        topMarginBand1.HeightF = 201F;
        topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        bottomMarginBand1.Dpi = 254F;
        bottomMarginBand1.HeightF = 0F;
        bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // detailIdentificacao
        // 
        detailIdentificacao.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail1});
        detailIdentificacao.DataMember = "TermoAbertura";
        detailIdentificacao.DataSource = this.ds;
        detailIdentificacao.Dpi = 254F;
        detailIdentificacao.Level = 0;
        detailIdentificacao.Name = "detailIdentificacao";
        // 
        // Detail1
        // 
        Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel38,
            xrLabel37,
            xrLabel17,
            xrLabel16,
            xrLabel34,
            xrLabel33,
            xrLabel3,
            xrLabel4,
            xrLabel1,
            xrLabel2,
            xrLabel7,
            xrLabel8,
            xrLabel5,
            xrLabel20,
            xrLabel21,
            xrLabel18,
            xrLabel19,
            xrLabel24,
            xrLabel25,
            xrLabel22});
        Detail1.Dpi = 254F;
        Detail1.HeightF = 550F;
        Detail1.Name = "Detail1";
        // 
        // xrLabel17
        // 
        xrLabel17.Dpi = 254F;
        xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(18.00003F, 400F);
        xrLabel17.Name = "xrLabel17";
        xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel17.SizeF = new System.Drawing.SizeF(1902F, 45F);
        xrLabel17.StyleName = "FieldCaption";
        xrLabel17.StylePriority.UseFont = false;
        xrLabel17.StylePriority.UseForeColor = false;
        xrLabel17.Text = "Grandes Desafios";
        // 
        // xrLabel16
        // 
        xrLabel16.Dpi = 254F;
        xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(970.0004F, 300F);
        xrLabel16.Name = "xrLabel16";
        xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel16.SizeF = new System.Drawing.SizeF(950F, 45F);
        xrLabel16.StyleName = "FieldCaption";
        xrLabel16.StylePriority.UseFont = false;
        xrLabel16.StylePriority.UseForeColor = false;
        xrLabel16.Text = "Direcionador";
        // 
        // xrLabel34
        // 
        xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DescricaoDesafio")});
        xrLabel34.Dpi = 254F;
        xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(18.00003F, 445F);
        xrLabel34.Multiline = true;
        xrLabel34.Name = "xrLabel34";
        xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel34.SizeF = new System.Drawing.SizeF(1902F, 45F);
        xrLabel34.StyleName = "DataField";
        xrLabel34.StylePriority.UseFont = false;
        xrLabel34.Text = "xrLabel34";
        // 
        // xrLabel33
        // 
        xrLabel33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DescricaoDirecionadorEstrategico")});
        xrLabel33.Dpi = 254F;
        xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(970.0004F, 345F);
        xrLabel33.Name = "xrLabel33";
        xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel33.SizeF = new System.Drawing.SizeF(950F, 45F);
        xrLabel33.StyleName = "DataField";
        xrLabel33.StylePriority.UseFont = false;
        xrLabel33.Text = "xrLabel33";
        // 
        // xrLabel3
        // 
        xrLabel3.Dpi = 254F;
        xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(970F, 100F);
        xrLabel3.Name = "xrLabel3";
        xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel3.SizeF = new System.Drawing.SizeF(950F, 45F);
        xrLabel3.StyleName = "FieldCaption";
        xrLabel3.StylePriority.UseFont = false;
        xrLabel3.StylePriority.UseForeColor = false;
        xrLabel3.Text = "Unidade Gestora";
        // 
        // xrLabel4
        // 
        xrLabel4.Dpi = 254F;
        xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 200F);
        xrLabel4.Name = "xrLabel4";
        xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel4.SizeF = new System.Drawing.SizeF(547.83F, 45F);
        xrLabel4.StyleName = "FieldCaption";
        xrLabel4.StylePriority.UseFont = false;
        xrLabel4.StylePriority.UseForeColor = false;
        xrLabel4.Text = "Data Início";
        // 
        // xrLabel1
        // 
        xrLabel1.Dpi = 254F;
        xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        xrLabel1.Name = "xrLabel1";
        xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel1.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel1.StyleName = "FieldCaption";
        xrLabel1.StylePriority.UseFont = false;
        xrLabel1.StylePriority.UseForeColor = false;
        xrLabel1.Text = "Nome da Iniciativa";
        // 
        // xrLabel2
        // 
        xrLabel2.Dpi = 254F;
        xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 100F);
        xrLabel2.Name = "xrLabel2";
        xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel2.SizeF = new System.Drawing.SizeF(950F, 45F);
        xrLabel2.StyleName = "FieldCaption";
        xrLabel2.StylePriority.UseFont = false;
        xrLabel2.StylePriority.UseForeColor = false;
        xrLabel2.Text = "Líder";
        // 
        // xrLabel7
        // 
        xrLabel7.Dpi = 254F;
        xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(16F, 300F);
        xrLabel7.Name = "xrLabel7";
        xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel7.SizeF = new System.Drawing.SizeF(950F, 45F);
        xrLabel7.StyleName = "FieldCaption";
        xrLabel7.StylePriority.UseFont = false;
        xrLabel7.StylePriority.UseForeColor = false;
        xrLabel7.Text = "Foco Estrategico";
        // 
        // xrLabel8
        // 
        xrLabel8.Dpi = 254F;
        xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 500F);
        xrLabel8.Name = "xrLabel8";
        xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel8.SizeF = new System.Drawing.SizeF(575F, 45F);
        xrLabel8.StyleName = "FieldCaption";
        xrLabel8.StylePriority.UseFont = false;
        xrLabel8.StylePriority.UseForeColor = false;
        xrLabel8.Text = "Valor Estimado da Inciativa";
        // 
        // xrLabel5
        // 
        xrLabel5.Dpi = 254F;
        xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(574.4573F, 200F);
        xrLabel5.Name = "xrLabel5";
        xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel5.SizeF = new System.Drawing.SizeF(547.8342F, 45F);
        xrLabel5.StyleName = "FieldCaption";
        xrLabel5.StylePriority.UseFont = false;
        xrLabel5.StylePriority.UseForeColor = false;
        xrLabel5.Text = "Data Término";
        // 
        // xrLabel20
        // 
        xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NomeUnidadeNegocio")});
        xrLabel20.Dpi = 254F;
        xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(969.9999F, 145F);
        xrLabel20.Name = "xrLabel20";
        xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel20.SizeF = new System.Drawing.SizeF(949.9999F, 45F);
        xrLabel20.StyleName = "DataField";
        xrLabel20.StylePriority.UseFont = false;
        xrLabel20.Text = "xrLabel20";
        // 
        // xrLabel21
        // 
        xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataInicio", "{0:dd/MM/yyyy}")});
        xrLabel21.Dpi = 254F;
        xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(16.00023F, 245F);
        xrLabel21.Name = "xrLabel21";
        xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel21.SizeF = new System.Drawing.SizeF(547.83F, 45F);
        xrLabel21.StyleName = "DataField";
        xrLabel21.StylePriority.UseFont = false;
        xrLabel21.Text = "xrLabel21";
        // 
        // xrLabel18
        // 
        xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NomeIniciativa")});
        xrLabel18.Dpi = 254F;
        xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 45F);
        xrLabel18.Name = "xrLabel18";
        xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel18.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel18.StyleName = "DataField";
        xrLabel18.StylePriority.UseFont = false;
        xrLabel18.Text = "xrLabel18";
        // 
        // xrLabel19
        // 
        xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NomeGerenteIniciativa")});
        xrLabel19.Dpi = 254F;
        xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(16.00007F, 145F);
        xrLabel19.Name = "xrLabel19";
        xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel19.SizeF = new System.Drawing.SizeF(950.0002F, 45F);
        xrLabel19.StyleName = "DataField";
        xrLabel19.StylePriority.UseFont = false;
        xrLabel19.Text = "xrLabel19";
        // 
        // xrLabel24
        // 
        xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NomeFocoEstrategico")});
        xrLabel24.Dpi = 254F;
        xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 345F);
        xrLabel24.Name = "xrLabel24";
        xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel24.SizeF = new System.Drawing.SizeF(950F, 45F);
        xrLabel24.StyleName = "DataField";
        xrLabel24.StylePriority.UseFont = false;
        xrLabel24.Text = "xrLabel24";
        // 
        // xrLabel25
        // 
        xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ValorEstimado", "{0:C2}")});
        xrLabel25.Dpi = 254F;
        xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(590.9999F, 500F);
        xrLabel25.Name = "xrLabel25";
        xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel25.SizeF = new System.Drawing.SizeF(1329F, 45F);
        xrLabel25.StyleName = "DataField";
        xrLabel25.StylePriority.UseFont = false;
        xrLabel25.Text = "xrLabel25";
        // 
        // xrLabel22
        // 
        xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTermino", "{0:dd/MM/yyyy}")});
        xrLabel22.Dpi = 254F;
        xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(574.8345F, 245F);
        xrLabel22.Name = "xrLabel22";
        xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel22.SizeF = new System.Drawing.SizeF(547.83F, 45F);
        xrLabel22.StyleName = "DataField";
        xrLabel22.StylePriority.UseFont = false;
        xrLabel22.Text = "xrLabel22";
        // 
        // ds
        // 
        this.ds.DataSetName = "dsImpressaoTai";
        this.ds.EnforceConstraints = false;
        this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrLabel28
        // 
        xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ObjetivoGeral")});
        xrLabel28.Dpi = 254F;
        xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(16F, 270F);
        xrLabel28.Multiline = true;
        xrLabel28.Name = "xrLabel28";
        xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel28.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel28.StyleName = "DataField";
        xrLabel28.StylePriority.UseFont = false;
        xrLabel28.Text = "xrLabel28";
        // 
        // xrLabel27
        // 
        xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Justificativa")});
        xrLabel27.Dpi = 254F;
        xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(16F, 170F);
        xrLabel27.Multiline = true;
        xrLabel27.Name = "xrLabel27";
        xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel27.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel27.StyleName = "DataField";
        xrLabel27.StylePriority.UseFont = false;
        xrLabel27.Text = "xrLabel27";
        // 
        // xrLabel26
        // 
        xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PublicoAlvo")});
        xrLabel26.Dpi = 254F;
        xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(16F, 70F);
        xrLabel26.Multiline = true;
        xrLabel26.Name = "xrLabel26";
        xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel26.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel26.StyleName = "DataField";
        xrLabel26.StylePriority.UseFont = false;
        xrLabel26.Text = "xrLabel26";
        // 
        // xrLabel11
        // 
        xrLabel11.Dpi = 254F;
        xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 225F);
        xrLabel11.Name = "xrLabel11";
        xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel11.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel11.StyleName = "FieldCaption";
        xrLabel11.StylePriority.UseFont = false;
        xrLabel11.StylePriority.UseForeColor = false;
        xrLabel11.Text = "Objetivo Geral";
        // 
        // xrLabel10
        // 
        xrLabel10.Dpi = 254F;
        xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 125F);
        xrLabel10.Name = "xrLabel10";
        xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel10.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel10.StyleName = "FieldCaption";
        xrLabel10.StylePriority.UseFont = false;
        xrLabel10.StylePriority.UseForeColor = false;
        xrLabel10.Text = "Justificativa";
        // 
        // xrLabel9
        // 
        xrLabel9.Dpi = 254F;
        xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 25F);
        xrLabel9.Name = "xrLabel9";
        xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel9.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel9.StyleName = "FieldCaption";
        xrLabel9.StylePriority.UseFont = false;
        xrLabel9.StylePriority.UseForeColor = false;
        xrLabel9.Text = "Público Alvo";
        // 
        // detailElementosResultado1
        // 
        detailElementosResultado1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail2,
            DetailReport3});
        detailElementosResultado1.DataMember = "TermoAbertura";
        detailElementosResultado1.DataSource = this.ds;
        detailElementosResultado1.Dpi = 254F;
        detailElementosResultado1.Expanded = false;
        detailElementosResultado1.Level = 1;
        detailElementosResultado1.Name = "detailElementosResultado1";
        // 
        // Detail2
        // 
        Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel9,
            xrLabel28,
            xrLabel27,
            xrLabel26,
            xrLabel11,
            xrLabel10});
        Detail2.Dpi = 254F;
        Detail2.HeightF = 325F;
        Detail2.Name = "Detail2";
        // 
        // DetailReport3
        // 
        DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail4,
            GroupHeader1});
        DetailReport3.DataMember = "TermoAbertura.TermoAbertura_tai_ResultadosIniciativa";
        DetailReport3.DataSource = this.ds;
        DetailReport3.Dpi = 254F;
        DetailReport3.Level = 0;
        DetailReport3.Name = "DetailReport3";
        // 
        // Detail4
        // 
        Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable2});
        Detail4.Dpi = 254F;
        Detail4.HeightF = 50F;
        Detail4.Name = "Detail4";
        // 
        // xrTable2
        // 
        xrTable2.Dpi = 254F;
        xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        xrTable2.Name = "xrTable2";
        xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow4});
        xrTable2.SizeF = new System.Drawing.SizeF(1904F, 50F);
        xrTable2.StyleName = "TableDataField";
        xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow4
        // 
        xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell21});
        xrTableRow4.Dpi = 254F;
        xrTableRow4.Name = "xrTableRow4";
        xrTableRow4.Weight = 0.5679012345679012D;
        // 
        // xrTableCell21
        // 
        xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_ResultadosIniciativa.SetencaResultado")});
        xrTableCell21.Dpi = 254F;
        xrTableCell21.Name = "xrTableCell21";
        xrTableCell21.Text = "xrTableCell21";
        xrTableCell21.Weight = 1.1807851239669422D;
        // 
        // GroupHeader1
        // 
        GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel6,
            xrTable1});
        GroupHeader1.Dpi = 254F;
        GroupHeader1.HeightF = 150F;
        GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel6
        // 
        xrLabel6.Dpi = 254F;
        xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(16F, 25F);
        xrLabel6.Name = "xrLabel6";
        xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel6.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel6.StyleName = "FieldCaption";
        xrLabel6.StylePriority.UseFont = false;
        xrLabel6.Text = "Resultados";
        // 
        // xrTable1
        // 
        xrTable1.BackColor = System.Drawing.Color.LightGray;
        xrTable1.Dpi = 254F;
        xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(18.00011F, 100F);
        xrTable1.Name = "xrTable1";
        xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow3});
        xrTable1.SizeF = new System.Drawing.SizeF(1904F, 50F);
        xrTable1.StyleName = "TableCaption";
        xrTable1.StylePriority.UseBackColor = false;
        // 
        // xrTableRow3
        // 
        xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell12});
        xrTableRow3.Dpi = 254F;
        xrTableRow3.Name = "xrTableRow3";
        xrTableRow3.Weight = 0.5679012345679012D;
        // 
        // xrTableCell12
        // 
        xrTableCell12.Dpi = 254F;
        xrTableCell12.Name = "xrTableCell12";
        xrTableCell12.Text = "Descrição do Resultado";
        xrTableCell12.Weight = 1.1807851239669422D;
        // 
        // detailElementosResultado2
        // 
        detailElementosResultado2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail3,
            DetailReport4});
        detailElementosResultado2.DataMember = "TermoAbertura";
        detailElementosResultado2.DataSource = this.ds;
        detailElementosResultado2.Dpi = 254F;
        detailElementosResultado2.Expanded = false;
        detailElementosResultado2.Level = 2;
        detailElementosResultado2.Name = "detailElementosResultado2";
        // 
        // Detail3
        // 
        Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel30,
            xrLabel29,
            xrLabel32,
            xrLabel31,
            xrLabel13,
            xrLabel12,
            xrLabel15,
            xrLabel14});
        Detail3.Dpi = 254F;
        Detail3.HeightF = 425F;
        Detail3.Name = "Detail3";
        // 
        // xrLabel30
        // 
        xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LimiteEscopo")});
        xrLabel30.Dpi = 254F;
        xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(16F, 170F);
        xrLabel30.Multiline = true;
        xrLabel30.Name = "xrLabel30";
        xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel30.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel30.StyleName = "DataField";
        xrLabel30.StylePriority.UseFont = false;
        xrLabel30.Text = "xrLabel30";
        // 
        // xrLabel29
        // 
        xrLabel29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Escopo")});
        xrLabel29.Dpi = 254F;
        xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(18F, 70F);
        xrLabel29.Multiline = true;
        xrLabel29.Name = "xrLabel29";
        xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel29.SizeF = new System.Drawing.SizeF(1902F, 45F);
        xrLabel29.StyleName = "DataField";
        xrLabel29.StylePriority.UseFont = false;
        xrLabel29.Text = "xrLabel29";
        // 
        // xrLabel32
        // 
        xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Restricoes")});
        xrLabel32.Dpi = 254F;
        xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(18F, 370F);
        xrLabel32.Multiline = true;
        xrLabel32.Name = "xrLabel32";
        xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel32.SizeF = new System.Drawing.SizeF(1902.001F, 45F);
        xrLabel32.StyleName = "DataField";
        xrLabel32.StylePriority.UseFont = false;
        xrLabel32.Text = "xrLabel32";
        // 
        // xrLabel31
        // 
        xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Premissas")});
        xrLabel31.Dpi = 254F;
        xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(18F, 270F);
        xrLabel31.Multiline = true;
        xrLabel31.Name = "xrLabel31";
        xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel31.SizeF = new System.Drawing.SizeF(1902F, 45F);
        xrLabel31.StyleName = "DataField";
        xrLabel31.StylePriority.UseFont = false;
        xrLabel31.Text = "xrLabel31";
        // 
        // xrLabel13
        // 
        xrLabel13.Dpi = 254F;
        xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 125F);
        xrLabel13.Name = "xrLabel13";
        xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel13.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel13.StyleName = "FieldCaption";
        xrLabel13.StylePriority.UseFont = false;
        xrLabel13.StylePriority.UseForeColor = false;
        xrLabel13.Text = "Limites do Escopo";
        // 
        // xrLabel12
        // 
        xrLabel12.Dpi = 254F;
        xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(18.00003F, 25F);
        xrLabel12.Name = "xrLabel12";
        xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel12.SizeF = new System.Drawing.SizeF(1902.001F, 45F);
        xrLabel12.StyleName = "FieldCaption";
        xrLabel12.StylePriority.UseFont = false;
        xrLabel12.StylePriority.UseForeColor = false;
        xrLabel12.Text = "Escopo da Iniciativa";
        // 
        // xrLabel15
        // 
        xrLabel15.Dpi = 254F;
        xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(18.00011F, 325F);
        xrLabel15.Name = "xrLabel15";
        xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel15.SizeF = new System.Drawing.SizeF(1902.001F, 45F);
        xrLabel15.StyleName = "FieldCaption";
        xrLabel15.StylePriority.UseFont = false;
        xrLabel15.StylePriority.UseForeColor = false;
        xrLabel15.Text = "Restrições";
        // 
        // xrLabel14
        // 
        xrLabel14.Dpi = 254F;
        xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(18.00003F, 225F);
        xrLabel14.Name = "xrLabel14";
        xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel14.SizeF = new System.Drawing.SizeF(1902F, 45F);
        xrLabel14.StyleName = "FieldCaption";
        xrLabel14.StylePriority.UseFont = false;
        xrLabel14.StylePriority.UseForeColor = false;
        xrLabel14.Text = "Premissas";
        // 
        // DetailReport4
        // 
        DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail5,
            GroupHeader2});
        DetailReport4.DataMember = "TermoAbertura.TermoAbertura_tai_ParceirosIniciativa";
        DetailReport4.DataSource = this.ds;
        DetailReport4.Dpi = 254F;
        DetailReport4.Level = 0;
        DetailReport4.Name = "DetailReport4";
        // 
        // Detail5
        // 
        Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable4});
        Detail5.Dpi = 254F;
        Detail5.HeightF = 50F;
        Detail5.Name = "Detail5";
        // 
        // xrTable4
        // 
        xrTable4.Dpi = 254F;
        xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        xrTable4.Name = "xrTable4";
        xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow6});
        xrTable4.SizeF = new System.Drawing.SizeF(1904F, 50F);
        xrTable4.StyleName = "TableDataField";
        xrTable4.StylePriority.UseBorders = false;
        // 
        // xrTableRow6
        // 
        xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell17,
            xrTableCell18,
            xrTableCell19,
            xrTableCell20,
            xrTableCell22,
            xrTableCell24});
        xrTableRow6.Dpi = 254F;
        xrTableRow6.Name = "xrTableRow6";
        xrTableRow6.Weight = 0.5679012345679012D;
        // 
        // xrTableCell17
        // 
        xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_ParceirosIniciativa.NomeParceiro")});
        xrTableCell17.Dpi = 254F;
        xrTableCell17.Name = "xrTableCell17";
        xrTableCell17.Text = "xrTableCell17";
        xrTableCell17.Weight = 0.1929387457462324D;
        // 
        // xrTableCell18
        // 
        xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_ParceirosIniciativa.ProdutoSolicitado")});
        xrTableCell18.Dpi = 254F;
        xrTableCell18.Name = "xrTableCell18";
        xrTableCell18.Text = "xrTableCell18";
        xrTableCell18.Weight = 0.34398222098756864D;
        // 
        // xrTableCell19
        // 
        xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_ParceirosIniciativa.DataPrevistaEntrega", "{0:dd/MM/yyyy}")});
        xrTableCell19.Dpi = 254F;
        xrTableCell19.Name = "xrTableCell19";
        xrTableCell19.Text = "xrTableCell19";
        xrTableCell19.Weight = 0.11025071185498991D;
        // 
        // xrTableCell20
        // 
        xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_ParceirosIniciativa.Interlocutor")});
        xrTableCell20.Dpi = 254F;
        xrTableCell20.Name = "xrTableCell20";
        xrTableCell20.Text = "xrTableCell20";
        xrTableCell20.Weight = 0.1929387457462324D;
        // 
        // xrTableCell22
        // 
        xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_ParceirosIniciativa.GrauInfluencia")});
        xrTableCell22.Dpi = 254F;
        xrTableCell22.Name = "xrTableCell22";
        xrTableCell22.Text = "xrTableCell22";
        xrTableCell22.Weight = 0.10473817626224044D;
        // 
        // xrTableCell24
        // 
        xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_ParceirosIniciativa.TipoParceiro")});
        xrTableCell24.Dpi = 254F;
        xrTableCell24.Name = "xrTableCell24";
        xrTableCell24.Text = "xrTableCell24";
        xrTableCell24.Weight = 0.10473817626224044D;
        // 
        // GroupHeader2
        // 
        GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel23,
            xrTable3});
        GroupHeader2.Dpi = 254F;
        GroupHeader2.HeightF = 150F;
        GroupHeader2.Name = "GroupHeader2";
        // 
        // xrLabel23
        // 
        xrLabel23.Dpi = 254F;
        xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(16F, 25F);
        xrLabel23.Name = "xrLabel23";
        xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel23.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel23.StyleName = "FieldCaption";
        xrLabel23.StylePriority.UseFont = false;
        xrLabel23.Text = "Parceiros";
        // 
        // xrTable3
        // 
        xrTable3.BackColor = System.Drawing.Color.LightGray;
        xrTable3.Dpi = 254F;
        xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(16F, 100F);
        xrTable3.Name = "xrTable3";
        xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow5});
        xrTable3.SizeF = new System.Drawing.SizeF(1904F, 50F);
        xrTable3.StyleName = "TableCaption";
        xrTable3.StylePriority.UseBackColor = false;
        xrTable3.StylePriority.UseBorders = false;
        xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell8,
            xrTableCell9,
            xrTableCell10,
            xrTableCell11,
            xrTableCell13,
            xrTableCell15});
        xrTableRow5.Dpi = 254F;
        xrTableRow5.Name = "xrTableRow5";
        xrTableRow5.Weight = 0.5679012345679012D;
        // 
        // xrTableCell8
        // 
        xrTableCell8.Dpi = 254F;
        xrTableCell8.Name = "xrTableCell8";
        xrTableCell8.Text = "Nome do Parceiro";
        xrTableCell8.Weight = 0.1929387457462324D;
        // 
        // xrTableCell9
        // 
        xrTableCell9.Dpi = 254F;
        xrTableCell9.Name = "xrTableCell9";
        xrTableCell9.Text = "Produtos Solicitados";
        xrTableCell9.Weight = 0.34398222098756864D;
        // 
        // xrTableCell10
        // 
        xrTableCell10.Dpi = 254F;
        xrTableCell10.Name = "xrTableCell10";
        xrTableCell10.Text = "Data Limite";
        xrTableCell10.Weight = 0.11025071185498991D;
        // 
        // xrTableCell11
        // 
        xrTableCell11.Dpi = 254F;
        xrTableCell11.Name = "xrTableCell11";
        xrTableCell11.Text = "Interlocutor";
        xrTableCell11.Weight = 0.1929387457462324D;
        // 
        // xrTableCell13
        // 
        xrTableCell13.Dpi = 254F;
        xrTableCell13.Name = "xrTableCell13";
        xrTableCell13.Text = "Grau de Influência";
        xrTableCell13.Weight = 0.10473817626224044D;
        // 
        // xrTableCell15
        // 
        xrTableCell15.Dpi = 254F;
        xrTableCell15.Name = "xrTableCell15";
        xrTableCell15.Text = "Tipo";
        xrTableCell15.Weight = 0.10473817626224044D;
        // 
        // detailElementosOperacionais
        // 
        detailElementosOperacionais.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail9,
            detailAcoes});
        detailElementosOperacionais.DataMember = "TermoAbertura";
        detailElementosOperacionais.DataSource = this.ds;
        detailElementosOperacionais.Dpi = 254F;
        detailElementosOperacionais.Level = 3;
        detailElementosOperacionais.Name = "detailElementosOperacionais";
        // 
        // Detail9
        // 
        Detail9.Dpi = 254F;
        Detail9.HeightF = 0F;
        Detail9.Name = "Detail9";
        // 
        // detailAcoes
        // 
        detailAcoes.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail6,
            GroupHeader3,
            this.DetailReport,
            this.DetailReport1});
        detailAcoes.DataMember = "TermoAbertura.TermoAbertura_tai_AcoesIniciativa";
        detailAcoes.DataSource = this.ds;
        detailAcoes.Dpi = 254F;
        detailAcoes.Level = 0;
        detailAcoes.Name = "detailAcoes";
        // 
        // Detail6
        // 
        Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable6,
            xrTable5});
        Detail6.Dpi = 254F;
        Detail6.HeightF = 150F;
        Detail6.Name = "Detail6";
        // 
        // xrTable6
        // 
        xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTable6.Dpi = 254F;
        xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(16F, 100F);
        xrTable6.Name = "xrTable6";
        xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow8});
        xrTable6.SizeF = new System.Drawing.SizeF(1904F, 50F);
        xrTable6.StylePriority.UseBorders = false;
        // 
        // xrTableRow8
        // 
        xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell35,
            xrTableCell36,
            xrTableCell38,
            xrTableCell39,
            xrTableCell40,
            xrTableCell41});
        xrTableRow8.Dpi = 254F;
        xrTableRow8.Name = "xrTableRow8";
        xrTableRow8.Weight = 0.5679012345679012D;
        // 
        // xrTableCell35
        // 
        xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.NomeAcao")});
        xrTableCell35.Dpi = 254F;
        xrTableCell35.Name = "xrTableCell35";
        xrTableCell35.Text = "xrTableCell35";
        xrTableCell35.Weight = 0.30318941872472321D;
        // 
        // xrTableCell36
        // 
        xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.NomeEntidadeResponsavel")});
        xrTableCell36.Dpi = 254F;
        xrTableCell36.Name = "xrTableCell36";
        xrTableCell36.Text = "xrTableCell36";
        xrTableCell36.Weight = 0.30318941872472333D;
        // 
        // xrTableCell38
        // 
        xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.NomeUsuarioResponsavel")});
        xrTableCell38.Dpi = 254F;
        xrTableCell38.Name = "xrTableCell38";
        xrTableCell38.Text = "xrTableCell38";
        xrTableCell38.Weight = 0.30318941872472327D;
        // 
        // xrTableCell39
        // 
        xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.Inicio", "{0:dd/MM/yyyy}")});
        xrTableCell39.Dpi = 254F;
        xrTableCell39.Name = "xrTableCell39";
        xrTableCell39.Text = "xrTableCell39";
        xrTableCell39.Weight = 0.12058671944891376D;
        // 
        // xrTableCell40
        // 
        xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.Termino", "{0:dd/MM/yyyy}")});
        xrTableCell40.Dpi = 254F;
        xrTableCell40.Name = "xrTableCell40";
        xrTableCell40.Text = "xrTableCell40";
        xrTableCell40.Weight = 0.12058671944891378D;
        // 
        // xrTableCell41
        // 
        xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.ValorPrevisto", "{0:n2}")});
        xrTableCell41.Dpi = 254F;
        xrTableCell41.Name = "xrTableCell41";
        xrTableCell41.Text = "xrTableCell41";
        xrTableCell41.Weight = 0.16124177600238288D;
        // 
        // xrTable5
        // 
        xrTable5.BackColor = System.Drawing.Color.LightGray;
        xrTable5.Dpi = 254F;
        xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(16F, 50F);
        xrTable5.Name = "xrTable5";
        xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow7});
        xrTable5.SizeF = new System.Drawing.SizeF(1904F, 50F);
        xrTable5.StyleName = "TableCaption";
        xrTable5.StylePriority.UseBackColor = false;
        xrTable5.StylePriority.UseBorders = false;
        xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow7
        // 
        xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell28,
            xrTableCell29,
            xrTableCell31,
            xrTableCell32,
            xrTableCell33,
            xrTableCell34});
        xrTableRow7.Dpi = 254F;
        xrTableRow7.Name = "xrTableRow7";
        xrTableRow7.Weight = 0.5679012345679012D;
        // 
        // xrTableCell28
        // 
        xrTableCell28.Dpi = 254F;
        xrTableCell28.Name = "xrTableCell28";
        xrTableCell28.Text = "Nome da Ação";
        xrTableCell28.Weight = 0.30318941872472321D;
        // 
        // xrTableCell29
        // 
        xrTableCell29.Dpi = 254F;
        xrTableCell29.Name = "xrTableCell29";
        xrTableCell29.Text = "Entidade";
        xrTableCell29.Weight = 0.30318941872472333D;
        // 
        // xrTableCell31
        // 
        xrTableCell31.Dpi = 254F;
        xrTableCell31.Name = "xrTableCell31";
        xrTableCell31.Text = "Responsável";
        xrTableCell31.Weight = 0.30318941872472327D;
        // 
        // xrTableCell32
        // 
        xrTableCell32.Dpi = 254F;
        xrTableCell32.Name = "xrTableCell32";
        xrTableCell32.Text = "Data de Início";
        xrTableCell32.Weight = 0.12058671944891376D;
        // 
        // xrTableCell33
        // 
        xrTableCell33.Dpi = 254F;
        xrTableCell33.Name = "xrTableCell33";
        xrTableCell33.Text = "Data de Termino";
        xrTableCell33.Weight = 0.12058671944891378D;
        // 
        // xrTableCell34
        // 
        xrTableCell34.Dpi = 254F;
        xrTableCell34.Name = "xrTableCell34";
        xrTableCell34.Text = "Valor";
        xrTableCell34.Weight = 0.16124177600238288D;
        // 
        // GroupHeader3
        // 
        GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrLabel36});
        GroupHeader3.Dpi = 254F;
        GroupHeader3.HeightF = 70F;
        GroupHeader3.Name = "GroupHeader3";
        // 
        // xrLabel36
        // 
        xrLabel36.Dpi = 254F;
        xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(16F, 25F);
        xrLabel36.Name = "xrLabel36";
        xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel36.SizeF = new System.Drawing.SizeF(1904F, 45F);
        xrLabel36.StyleName = "FieldCaption";
        xrLabel36.StylePriority.UseFont = false;
        xrLabel36.Text = "Ações do Projeto";
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail10,
            this.GroupHeader6});
        this.DetailReport.DataMember = "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.FK_tai_ProdutosAcoesIniciativa_ta" +
"i_AcoesIniciativa";
        this.DetailReport.DataSource = this.ds;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Expanded = false;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        this.DetailReport.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail10
        // 
        this.Detail10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable7});
        this.Detail10.Dpi = 254F;
        this.Detail10.HeightF = 50F;
        this.Detail10.Name = "Detail10";
        // 
        // xrTable7
        // 
        xrTable7.Dpi = 254F;
        xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(120.9966F, 0F);
        xrTable7.Name = "xrTable7";
        xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow9});
        xrTable7.SizeF = new System.Drawing.SizeF(1799.003F, 50F);
        xrTable7.StyleName = "TableDataField";
        xrTable7.StylePriority.UseBorders = false;
        // 
        // xrTableRow9
        // 
        xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell27,
            xrTableCell42,
            xrTableCell37});
        xrTableRow9.Dpi = 254F;
        xrTableRow9.Name = "xrTableRow9";
        xrTableRow9.Weight = 0.5679012345679012D;
        // 
        // xrTableCell27
        // 
        xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.FK_tai_ProdutosAcoesIniciativa_ta" +
                    "i_AcoesIniciativa.DescricaoProduto")});
        xrTableCell27.Dpi = 254F;
        xrTableCell27.Name = "xrTableCell27";
        xrTableCell27.Text = "xrTableCell27";
        xrTableCell27.Weight = 1.7232413373882134D;
        // 
        // xrTableCell42
        // 
        xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.FK_tai_ProdutosAcoesIniciativa_ta" +
                    "i_AcoesIniciativa.Quantidade", "{0:n2}")});
        xrTableCell42.Dpi = 254F;
        xrTableCell42.Name = "xrTableCell42";
        xrTableCell42.Text = "xrTableCell42";
        xrTableCell42.Weight = 0.43967356279451753D;
        // 
        // xrTableCell37
        // 
        xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.FK_tai_ProdutosAcoesIniciativa_ta" +
                    "i_AcoesIniciativa.DataLimitePrevista", "{0:dd/MM/yyyy}")});
        xrTableCell37.Dpi = 254F;
        xrTableCell37.Name = "xrTableCell37";
        xrTableCell37.Text = "xrTableCell37";
        xrTableCell37.Weight = 0.38861454561013531D;
        // 
        // GroupHeader6
        // 
        this.GroupHeader6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable8});
        this.GroupHeader6.Dpi = 254F;
        this.GroupHeader6.HeightF = 50F;
        this.GroupHeader6.Name = "GroupHeader6";
        // 
        // xrTable8
        // 
        xrTable8.BackColor = System.Drawing.Color.LightGray;
        xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        xrTable8.Dpi = 254F;
        xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(120.9967F, 0F);
        xrTable8.Name = "xrTable8";
        xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow10});
        xrTable8.SizeF = new System.Drawing.SizeF(1799.003F, 50F);
        xrTable8.StyleName = "TableCaption";
        xrTable8.StylePriority.UseBackColor = false;
        xrTable8.StylePriority.UseBorders = false;
        // 
        // xrTableRow10
        // 
        xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell47,
            xrTableCell48,
            xrTableCell26});
        xrTableRow10.Dpi = 254F;
        xrTableRow10.Name = "xrTableRow10";
        xrTableRow10.Weight = 0.5679012345679012D;
        // 
        // xrTableCell47
        // 
        xrTableCell47.Dpi = 254F;
        xrTableCell47.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        xrTableCell47.Name = "xrTableCell47";
        xrTableCell47.StylePriority.UseFont = false;
        xrTableCell47.Text = "Produto(s) da ação";
        xrTableCell47.Weight = 1.6784335484616868D;
        // 
        // xrTableCell48
        // 
        xrTableCell48.Dpi = 254F;
        xrTableCell48.Name = "xrTableCell48";
        xrTableCell48.Text = "Quantidade";
        xrTableCell48.Weight = 0.42824181039359382D;
        // 
        // xrTableCell26
        // 
        xrTableCell26.Dpi = 254F;
        xrTableCell26.Name = "xrTableCell26";
        xrTableCell26.Text = "Data de Término";
        xrTableCell26.Weight = 0.37850965395478625D;
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail11,
            this.GroupHeader7});
        this.DetailReport1.DataMember = "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.FK_tai_MarcosAcoesIniciativa_tai_" +
"AcoesIniciativa";
        this.DetailReport1.DataSource = this.ds;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 1;
        this.DetailReport1.Name = "DetailReport1";
        this.DetailReport1.ReportPrintOptions.PrintOnEmptyDataSource = false;
        // 
        // Detail11
        // 
        this.Detail11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable9});
        this.Detail11.Dpi = 254F;
        this.Detail11.HeightF = 50F;
        this.Detail11.Name = "Detail11";
        // 
        // xrTable9
        // 
        xrTable9.Dpi = 254F;
        xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(120.9966F, 0F);
        xrTable9.Name = "xrTable9";
        xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow11});
        xrTable9.SizeF = new System.Drawing.SizeF(1799.003F, 50F);
        xrTable9.StyleName = "TableDataField";
        xrTable9.StylePriority.UseBorders = false;
        // 
        // xrTableRow11
        // 
        xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell44,
            xrTableCell46});
        xrTableRow11.Dpi = 254F;
        xrTableRow11.Name = "xrTableRow11";
        xrTableRow11.Weight = 0.5679012345679012D;
        // 
        // xrTableCell44
        // 
        xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.FK_tai_MarcosAcoesIniciativa_tai_" +
                    "AcoesIniciativa.NomeMarco")});
        xrTableCell44.Dpi = 254F;
        xrTableCell44.Name = "xrTableCell44";
        xrTableCell44.Text = "xrTableCell44";
        xrTableCell44.Weight = 1.1162559858924266D;
        // 
        // xrTableCell46
        // 
        xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TermoAbertura_tai_AcoesIniciativa.FK_tai_MarcosAcoesIniciativa_tai_" +
                    "AcoesIniciativa.DataLimitePrevista", "{0:dd/MM/yyyy}")});
        xrTableCell46.Dpi = 254F;
        xrTableCell46.Name = "xrTableCell46";
        xrTableCell46.Text = "xrTableCell46";
        xrTableCell46.Weight = 0.2005597390570491D;
        // 
        // GroupHeader7
        // 
        this.GroupHeader7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTable10});
        this.GroupHeader7.Dpi = 254F;
        this.GroupHeader7.HeightF = 50F;
        this.GroupHeader7.Name = "GroupHeader7";
        // 
        // xrTable10
        // 
        xrTable10.BackColor = System.Drawing.Color.LightGray;
        xrTable10.Dpi = 254F;
        xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(120.9966F, 0F);
        xrTable10.Name = "xrTable10";
        xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            xrTableRow12});
        xrTable10.SizeF = new System.Drawing.SizeF(1799.003F, 50F);
        xrTable10.StyleName = "TableCaption";
        xrTable10.StylePriority.UseBackColor = false;
        xrTable10.StylePriority.UseBorders = false;
        xrTable10.StylePriority.UseFont = false;
        // 
        // xrTableRow12
        // 
        xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            xrTableCell52,
            xrTableCell43});
        xrTableRow12.Dpi = 254F;
        xrTableRow12.Name = "xrTableRow12";
        xrTableRow12.Weight = 0.5679012345679012D;
        // 
        // xrTableCell52
        // 
        xrTableCell52.Dpi = 254F;
        xrTableCell52.Name = "xrTableCell52";
        xrTableCell52.Text = "Marco(s) da ação";
        xrTableCell52.Weight = 1.1162559858924266D;
        // 
        // xrTableCell43
        // 
        xrTableCell43.Dpi = 254F;
        xrTableCell43.Name = "xrTableCell43";
        xrTableCell43.Text = "Data Limite";
        xrTableCell43.Weight = 0.2005597390570491D;
        // 
        // PageHeader
        // 
        PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.picLogoEntidade,
            xrLabel35,
            xrLine1});
        PageHeader.Dpi = 254F;
        PageHeader.Expanded = false;
        PageHeader.HeightF = 175F;
        PageHeader.Name = "PageHeader";
        // 
        // picLogoEntidade
        // 
        this.picLogoEntidade.Dpi = 254F;
        this.picLogoEntidade.LocationFloat = new DevExpress.Utils.PointFloat(15.99999F, 0F);
        this.picLogoEntidade.Name = "picLogoEntidade";
        this.picLogoEntidade.SizeF = new System.Drawing.SizeF(300F, 150F);
        this.picLogoEntidade.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // Title
        // 
        this.Title.BackColor = System.Drawing.Color.White;
        this.Title.BorderColor = System.Drawing.SystemColors.ControlText;
        this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.Title.BorderWidth = 1F;
        this.Title.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
        this.Title.ForeColor = System.Drawing.SystemColors.ControlText;
        this.Title.Name = "Title";
        // 
        // FieldCaption
        // 
        this.FieldCaption.BackColor = System.Drawing.Color.White;
        this.FieldCaption.BorderColor = System.Drawing.SystemColors.ControlText;
        this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.FieldCaption.BorderWidth = 1F;
        this.FieldCaption.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
        this.FieldCaption.ForeColor = System.Drawing.SystemColors.ControlText;
        this.FieldCaption.Name = "FieldCaption";
        // 
        // PageInfo
        // 
        this.PageInfo.BackColor = System.Drawing.Color.White;
        this.PageInfo.BorderColor = System.Drawing.SystemColors.ControlText;
        this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PageInfo.BorderWidth = 1F;
        this.PageInfo.Font = new System.Drawing.Font("Verdana", 8F);
        this.PageInfo.ForeColor = System.Drawing.SystemColors.ControlText;
        this.PageInfo.Name = "PageInfo";
        // 
        // DataField
        // 
        this.DataField.BackColor = System.Drawing.Color.White;
        this.DataField.BorderColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.DataField.BorderWidth = 1F;
        this.DataField.Font = new System.Drawing.Font("Verdana", 8F);
        this.DataField.ForeColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Name = "DataField";
        this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        // 
        // TableCaption
        // 
        this.TableCaption.BackColor = System.Drawing.Color.White;
        this.TableCaption.BorderColor = System.Drawing.SystemColors.ControlText;
        this.TableCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TableCaption.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.TableCaption.ForeColor = System.Drawing.SystemColors.ControlText;
        this.TableCaption.Name = "TableCaption";
        // 
        // TableDataField
        // 
        this.TableDataField.BackColor = System.Drawing.Color.White;
        this.TableDataField.BorderColor = System.Drawing.SystemColors.ControlText;
        this.TableDataField.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TableDataField.Font = new System.Drawing.Font("Verdana", 8F);
        this.TableDataField.ForeColor = System.Drawing.SystemColors.ControlText;
        this.TableDataField.Name = "TableDataField";
        // 
        // xrLabel37
        // 
        xrLabel37.Dpi = 254F;
        xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(1131.292F, 200F);
        xrLabel37.Name = "xrLabel37";
        xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel37.SizeF = new System.Drawing.SizeF(672.1877F, 45F);
        xrLabel37.StyleName = "FieldCaption";
        xrLabel37.StylePriority.UseFont = false;
        xrLabel37.StylePriority.UseForeColor = false;
        xrLabel37.Text = "Tipo de Iniciativa";
        // 
        // xrLabel38
        // 
        xrLabel38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TermoAbertura.TipoIniciativa")});
        xrLabel38.Dpi = 254F;
        xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(1130.292F, 244.9999F);
        xrLabel38.Name = "xrLabel38";
        xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        xrLabel38.SizeF = new System.Drawing.SizeF(767.4375F, 45.00003F);
        xrLabel38.StyleName = "DataField";
        xrLabel38.StylePriority.UseFont = false;
        // 
        // relImpressaoTai
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            Detail,
            pageFooterBand1,
            topMarginBand1,
            bottomMarginBand1,
            detailIdentificacao,
            detailElementosResultado1,
            detailElementosResultado2,
            detailElementosOperacionais,
            PageHeader});
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Margins = new System.Drawing.Printing.Margins(165, 0, 201, 0);
        this.PageHeight = 2970;
        this.PageWidth = 2100;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportPrintOptions.DetailCountOnEmptyDataSource = 12;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 25F;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField,
            this.TableCaption,
            this.TableDataField});
        this.Version = "16.2";
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
