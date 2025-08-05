using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for rel_Contrato
/// </summary>
public class rel_Contrato : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private System.Data.DataSet ds;
    private System.Data.DataTable dataTable1;
    private System.Data.DataColumn dataColumn1;
    private System.Data.DataColumn dataColumn2;
    private System.Data.DataTable dataTable2;
    private System.Data.DataTable dataTable3;
    private System.Data.DataTable dataTable4;
    private System.Data.DataColumn dataColumn3;
    private System.Data.DataColumn dataColumn4;
    private System.Data.DataColumn dataColumn5;
    private System.Data.DataColumn dataColumn6;
    private System.Data.DataColumn dataColumn7;
    private System.Data.DataColumn dataColumn8;
    private System.Data.DataColumn dataColumn9;
    private System.Data.DataColumn dataColumn10;
    private System.Data.DataColumn dataColumn11;
    private System.Data.DataColumn dataColumn12;
    private System.Data.DataColumn dataColumn13;
    private System.Data.DataColumn dataColumn14;
    private System.Data.DataColumn dataColumn15;
    private System.Data.DataColumn dataColumn16;
    private System.Data.DataColumn dataColumn17;
    private System.Data.DataColumn dataColumn18;
    private System.Data.DataColumn dataColumn19;
    private System.Data.DataColumn dataColumn20;
    private System.Data.DataColumn dataColumn21;
    private System.Data.DataColumn dataColumn22;
    private System.Data.DataColumn dataColumn23;
    private System.Data.DataColumn dataColumn24;
    private System.Data.DataColumn dataColumn25;
    private System.Data.DataColumn dataColumn26;
    private System.Data.DataColumn dataColumn27;
    private System.Data.DataColumn dataColumn28;
    private System.Data.DataColumn dataColumn29;
    private System.Data.DataColumn dataColumn30;
    private System.Data.DataColumn dataColumn31;
    private System.Data.DataColumn dataColumn32;
    private System.Data.DataColumn dataColumn33;
    private System.Data.DataColumn dataColumn34;
    private ReportHeaderBand ReportHeader;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLine xrLine1;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRLabel xrLabel20;
    private XRLabel xrLabel19;
    private XRLabel xrLabel18;
    private XRLabel xrLabel17;
    private XRLabel xrLabel16;
    private XRLabel xrLabel15;
    private XRLabel xrLabel14;
    private XRLabel xrLabel13;
    private XRLabel xrLabel12;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRLabel xrLabel23;
    private XRLabel xrLabel24;
    private XRLabel xrLabel21;
    private XRLabel xrLabel22;
    private XRLabel xrLabel25;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private CalculatedField calculatedField1;
    private XRLabel xrLabel28;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel xrLabel31;
    private XRLabel xrLabel32;
    private XRLabel xrLabel34;
    private XRLabel xrLabel33;
    private XRLabel xrLabel35;
    private XRLabel xrLabel36;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private GroupHeaderBand GroupHeader3;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell24;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRLabel xrLabel37;
    private XRLabel xrLabel38;
    private XRLabel xrLabel39;
    private XRLabel xrLabel40;
    private CalculatedField calculatedField2;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable7;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell32;
    private GroupFooterBand GroupFooter2;
    private XRTable xrTable8;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell28;
    private DataColumn dataColumn35;
    private XRLabel xrLabel41;
    private DataColumn dataColumn36;
    private DataColumn dataColumn37;
    private DataColumn dataColumn38;
    private XRLabel xrLabel43;
    private XRLabel xrLabel42;
    private CalculatedField PercValorGarantia;
    private DataTable dataTable5;
    private DataColumn dataColumn39;
    private DataColumn dataColumn40;
    private DataColumn dataColumn41;
    private DetailReportBand DetailReport3;
    private DetailBand Detail4;
    private XRTable xrTable9;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private GroupHeaderBand ObrigacoesContratada;
    private XRLabel xrLabel45;
    private DataColumn dataColumn42;
    private XRTable xrTable10;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private DataColumn dataColumn43;
    private XRLabel xrLabel48;
    private XRLabel xrLabel47;
    private XRLabel xrLabel46;
    private CalculatedField txtGarantia;
    private XRLabel xrLabel50;
    private XRLabel xrLabel49;

    private dados cDados = CdadosUtil.GetCdados(null);

    public rel_Contrato(int codigoContrato)
    {
        InitializeComponent();


        DataSet dsTemp = cDados.getParametrosSistema("labelPrevistoParcelaContrato");

        string labelValorMedido = "Valor Medido";

        if (cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0]))
        {
            if (dsTemp.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() != "")
                labelValorMedido = dsTemp.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString();
        }

        this.xrLabel23.Text = labelValorMedido;
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "calculaSaldoContratualPorValorContrato");
        this.calculatedField1.DataMember = "DadosContrato";
        this.calculatedField2.DataMember = "DadosContrato";

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["calculaSaldoContratualPorValorContrato"].ToString() == "S")
        {
            this.calculatedField1.Expression = "[ValorContratoCalculo] - [ValorPago]";
            this.calculatedField2.Expression = "[ValorContrato]";
        }
        else
        {
            this.calculatedField1.Expression = "[ValorContratoCalculo] - [ValorMedicao]";
            this.calculatedField2.Expression = "[ValorMedicao]";
        }
        InitData(codigoContrato);
    }

    //private Image ObtemLogoEntidade()
    //{
    //    int codEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
    //    DataSet dsLogoUnidade = cDados.getLogoEntidade(codEntidade, "");

    //    if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]) && dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"] + "" != "")
    //    {
    //        try
    //        {
    //            byte[] bytesLogo = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
    //            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytesLogo);
    //            Image logo = Image.FromStream(stream);
    //            return logo;
    //        }
    //        catch
    //        {
    //            return null;
    //        } 
    //    }
    //    else
    //        return null;
    //} 

    private void InitData(int codigoContrato)
    {
        cDados = CdadosUtil.GetCdados(null);
        Image logo = cDados.ObtemLogoEntidade();
        xrPictureBox1.Image = logo;
        string comandoSql = string.Format(@"declare @CodigoContrato int
                                            set @CodigoContrato  = {0}


               SELECT @CodigoContrato CodigoContrato, cont.NumeroContrato  
	                     ,CASE WHEN cont.StatusContrato = 'A' THEN 'Ativo' 
                             WHEN cont.StatusContrato = 'I' THEN 'Encerrado' ELSE 'Previsto' END AS SituacaoContrato,
                       prs.NomePessoa,
                       cont.DescricaoObjetoContrato,
                       mun.NomeMunicipio, 
                       tso.DescricaoSegmentoObra,
                       cont.DataAssinatura,
                       cont.DataInicio,
                       cont.DataTerminoOriginal,
                       CASE WHEN (ISNULL(cont.DataTermino,0) <> ISNULL(cont.DataTerminoOriginal,0)) THEN cont.DataTermino
                       ELSE NULL END DataTermino,
                       cont.ValorContratoOriginal, 
                       CASE WHEN (ISNULL(cont.ValorContrato,0) <> ISNULL(cont.ValorContratoOriginal,0)) and (ISNULL(cont.ValorContrato,0) > 0 ) THEN ISNULL(cont.ValorContrato,0)
                       ELSE ISNULL(cont.ValorContratoOriginal,0) END ValorContratoCalculo, 
                       CASE WHEN (ISNULL(cont.ValorContrato,0) <> ISNULL(cont.ValorContratoOriginal,0)) and (ISNULL(cont.ValorContrato,0) > 0 ) THEN ISNULL(cont.ValorContrato,0)
                       ELSE NULL END ValorContrato,
                       (SELECT isnull(SUM(isnull(ValorPago,0)),0) FROM ParcelaContrato parc where parc.CodigoContrato = cont.CodigoContrato AND parc.[DataExclusao] IS NULL) AS ValorPago, 
                       (SELECT isnull(SUM(isnull(ValorPrevisto,0)),0) FROM ParcelaContrato parc where parc.CodigoContrato = cont.CodigoContrato AND parc.[DataExclusao] IS NULL) AS ValorMedicao, 
	                   tps.DescricaoTipoServico, 
	                   fonte.NomeFonte, 
	                   u.NomeUsuario AS GestorContrato,
	                   prs.NomeContato,
                       cont.NumeroContratoSap numeroSap,
                       cont.Observacao,
                       cont.valorGarantia,
                       cont.percGarantia,
                       case when cont.RetencaoGarantia = 'GR' then 'Garantia'
                            when cont.RetencaoGarantia = 'CF' then 'Carta Fiança'
                            else 'Não Informado'
                       end RetencaoGarantia 
	                  
                  FROM Contrato cont 
											LEFT JOIN Projeto p ON (p.CodigoProjeto = cont.CodigoProjeto
																AND p.DataExclusao IS NULL)  
										 	LEFT JOIN Obra o ON (o.CodigoContrato = cont.CodigoContrato
																AND o.DataExclusao IS NULL) 
											INNER JOIN Pessoa prs ON prs.CodigoPessoa = cont.CodigoPessoaContratada 
											INNER JOIN Municipio mrs ON mrs.CodigoMunicipio = prs.CodigoMunicipioEnderecoPessoa 
											INNER JOIN TipoContrato tc ON tc.CodigoTipoContrato = cont.CodigoTipoContrato 
											INNER JOIN Usuario u ON u.CodigoUsuario = cont.CodigoUsuarioResponsavel 
                      LEFT JOIN FonteRecursosFinanceiros fonte on fonte.CodigoFonteRecursosFinanceiros = cont.CodigoFonteRecursosFinanceiros
                      LEFT JOIN TipoServico tps on tps.CodigoTipoServico = o.CodigoTipoServico
                      LEFT JOIN Municipio  mun on mun.CodigoMunicipio = o.CodigoMunicipioObra
                      LEFT JOIN TipoSegmentoObra  tso  on tso.CodigoSegmentoObra = o.CodigoSegmentoObra
                       
                 WHERE cont.CodigoContrato = @CodigoContrato


--financeiro



                SELECT @CodigoContrato CodigoContrato,NumeroParcela
                    , DataVencimento as DataMedicao, ValorPrevisto as ValorMedicao
                    , DataPagamento, ValorPago
                FROM ParcelaContrato
                WHERE CodigoContrato = @CodigoContrato AND [DataExclusao] IS NULL

--aditivos
                SELECT @CodigoContrato CodigoContrato,
                       ac.NumeroContratoAditivo,
											 ac.DataInclusao,
											 ac.ValorAditivo,
											 ac.NovoValorContrato,
											 ac.NovaDataVigencia
                 FROM AditivoContrato ac LEFT JOIN
	                  Usuario ap ON ap.CodigoUsuario = ac.CodigoUsuarioAprovacao INNER JOIN
	                  Usuario ui ON ui.CodigoUsuario = ac.CodigoUsuarioInclusao INNER JOIN
                      TipoContrato tc ON tc.CodigoTipoContrato = CodigoTipoContratoAditivo
                WHERE ac.DataExclusao IS NULL
                   AND ac.CodigoContrato = @CodigoContrato
  
  --comentarios
  
  
                SELECT @CodigoContrato CodigoContrato,cc.DataInclusao,  ui.NomeUsuario AS UsuarioInclusao,cc.Comentario
                 FROM ComentarioContrato cc LEFT JOIN
	                  Usuario ui ON ui.CodigoUsuario = cc.CodigoUsuarioInclusao
                WHERE cc.CodigoContrato = @CodigoContrato
                order by cc.DataInclusao

 --Obrigacao
                select 
                @CodigoContrato CodigoContrato, oc.CodigoObrigacoesContratada, oc.Ocorrencia , toc.DescricaoTipoObrigacoesContratada as ObrigacaoContratada
                from ObrigacoesContratada oc
                inner join TipoObrigacoesContratada toc on (toc.CodigoTipoObrigacoesContratada = oc.CodigoObrigacoesContratada)
                where oc.CodigoContrato = @CodigoContrato
                 


", codigoContrato);

        DataSet dsTemp = cDados.getDataSet(comandoSql);

        ds.Load(dsTemp.CreateDataReader(), LoadOption.OverwriteChanges, "DadosContrato", "Financeiro", "Aditivos", "Comentarios", "Obrigacao");
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
        string resourceFileName = "rel_Contrato.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel50 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel46 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel42 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel33 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel28 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ds = new System.Data.DataSet();
        this.dataTable1 = new System.Data.DataTable();
        this.dataColumn1 = new System.Data.DataColumn();
        this.dataColumn2 = new System.Data.DataColumn();
        this.dataColumn3 = new System.Data.DataColumn();
        this.dataColumn4 = new System.Data.DataColumn();
        this.dataColumn5 = new System.Data.DataColumn();
        this.dataColumn6 = new System.Data.DataColumn();
        this.dataColumn7 = new System.Data.DataColumn();
        this.dataColumn8 = new System.Data.DataColumn();
        this.dataColumn9 = new System.Data.DataColumn();
        this.dataColumn10 = new System.Data.DataColumn();
        this.dataColumn11 = new System.Data.DataColumn();
        this.dataColumn12 = new System.Data.DataColumn();
        this.dataColumn13 = new System.Data.DataColumn();
        this.dataColumn14 = new System.Data.DataColumn();
        this.dataColumn15 = new System.Data.DataColumn();
        this.dataColumn16 = new System.Data.DataColumn();
        this.dataColumn17 = new System.Data.DataColumn();
        this.dataColumn18 = new System.Data.DataColumn();
        this.dataColumn35 = new System.Data.DataColumn();
        this.dataColumn36 = new System.Data.DataColumn();
        this.dataColumn37 = new System.Data.DataColumn();
        this.dataColumn38 = new System.Data.DataColumn();
        this.dataColumn43 = new System.Data.DataColumn();
        this.dataTable2 = new System.Data.DataTable();
        this.dataColumn19 = new System.Data.DataColumn();
        this.dataColumn20 = new System.Data.DataColumn();
        this.dataColumn21 = new System.Data.DataColumn();
        this.dataColumn22 = new System.Data.DataColumn();
        this.dataColumn23 = new System.Data.DataColumn();
        this.dataColumn24 = new System.Data.DataColumn();
        this.dataTable3 = new System.Data.DataTable();
        this.dataColumn25 = new System.Data.DataColumn();
        this.dataColumn26 = new System.Data.DataColumn();
        this.dataColumn27 = new System.Data.DataColumn();
        this.dataColumn28 = new System.Data.DataColumn();
        this.dataColumn29 = new System.Data.DataColumn();
        this.dataColumn30 = new System.Data.DataColumn();
        this.dataTable4 = new System.Data.DataTable();
        this.dataColumn31 = new System.Data.DataColumn();
        this.dataColumn32 = new System.Data.DataColumn();
        this.dataColumn33 = new System.Data.DataColumn();
        this.dataColumn34 = new System.Data.DataColumn();
        this.dataTable5 = new System.Data.DataTable();
        this.dataColumn39 = new System.Data.DataColumn();
        this.dataColumn40 = new System.Data.DataColumn();
        this.dataColumn41 = new System.Data.DataColumn();
        this.dataColumn42 = new System.Data.DataColumn();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel38 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLabel39 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.calculatedField2 = new DevExpress.XtraReports.UI.CalculatedField();
        this.PercValorGarantia = new DevExpress.XtraReports.UI.CalculatedField();
        this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
        this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ObrigacoesContratada = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
        this.txtGarantia = new DevExpress.XtraReports.UI.CalculatedField();
        ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel50,
            this.xrLabel49,
            this.xrLabel48,
            this.xrLabel47,
            this.xrLabel46,
            this.xrLabel43,
            this.xrLabel42,
            this.xrLabel40,
            this.xrLabel36,
            this.xrLabel35,
            this.xrLabel34,
            this.xrLabel33,
            this.xrLabel32,
            this.xrLabel31,
            this.xrLabel30,
            this.xrLabel29,
            this.xrLabel28,
            this.xrLabel27,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel23,
            this.xrLabel24,
            this.xrLabel21,
            this.xrLabel22,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel18,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel15,
            this.xrLabel14,
            this.xrLabel13,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLine1,
            this.xrLabel4});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 830.5834F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel50
        // 
        this.xrLabel50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.ValorGarantia", "{0:c2}")});
        this.xrLabel50.Dpi = 254F;
        this.xrLabel50.LocationFloat = new DevExpress.Utils.PointFloat(436.6667F, 780.5383F);
        this.xrLabel50.Name = "xrLabel50";
        this.xrLabel50.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel50.SizeF = new System.Drawing.SizeF(325.4375F, 39.89917F);
        this.xrLabel50.Text = "xrLabel50";
        // 
        // xrLabel49
        // 
        this.xrLabel49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.PercGarantia", "{0:#.0000}")});
        this.xrLabel49.Dpi = 254F;
        this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(436.6667F, 780.5383F);
        this.xrLabel49.Name = "xrLabel49";
        this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel49.SizeF = new System.Drawing.SizeF(325.4375F, 39.89911F);
        this.xrLabel49.Text = "xrLabel49";
        // 
        // xrLabel48
        // 
        this.xrLabel48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.txtGarantia")});
        this.xrLabel48.Dpi = 254F;
        this.xrLabel48.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(436.6667F, 738.639F);
        this.xrLabel48.Name = "xrLabel48";
        this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel48.SizeF = new System.Drawing.SizeF(254F, 39.89911F);
        this.xrLabel48.StylePriority.UseFont = false;
        this.xrLabel48.Text = "xrLabel48";
        // 
        // xrLabel47
        // 
        this.xrLabel47.Dpi = 254F;
        this.xrLabel47.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(25.00009F, 735.6392F);
        this.xrLabel47.Name = "xrLabel47";
        this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel47.SizeF = new System.Drawing.SizeF(399.9996F, 39.89911F);
        this.xrLabel47.StylePriority.UseFont = false;
        this.xrLabel47.Text = "Retenção Garantia";
        // 
        // xrLabel46
        // 
        this.xrLabel46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.RetencaoGarantia")});
        this.xrLabel46.Dpi = 254F;
        this.xrLabel46.LocationFloat = new DevExpress.Utils.PointFloat(24.99985F, 780.5383F);
        this.xrLabel46.Name = "xrLabel46";
        this.xrLabel46.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel46.SizeF = new System.Drawing.SizeF(399.9999F, 39.89917F);
        this.xrLabel46.Text = "xrLabel46";
        this.xrLabel46.TextChanged += new System.EventHandler(this.xrLabel46_TextChanged);
        // 
        // xrLabel43
        // 
        this.xrLabel43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Observacao")});
        this.xrLabel43.Dpi = 254F;
        this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(24.99985F, 695.7399F);
        this.xrLabel43.Name = "xrLabel43";
        this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel43.SizeF = new System.Drawing.SizeF(1543F, 39.8992F);
        this.xrLabel43.Text = "xrLabel43";
        // 
        // xrLabel42
        // 
        this.xrLabel42.Dpi = 254F;
        this.xrLabel42.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel42.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 655.8408F);
        this.xrLabel42.Name = "xrLabel42";
        this.xrLabel42.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel42.SizeF = new System.Drawing.SizeF(254F, 39.89917F);
        this.xrLabel42.StylePriority.UseFont = false;
        this.xrLabel42.Text = "Observações";
        // 
        // xrLabel40
        // 
        this.xrLabel40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "calculatedField2", "{0:c2}")});
        this.xrLabel40.Dpi = 254F;
        this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(870.6455F, 458.9907F);
        this.xrLabel40.Name = "xrLabel40";
        this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel40.SizeF = new System.Drawing.SizeF(343.9589F, 39.8992F);
        this.xrLabel40.Text = "xrLabel40";
        // 
        // xrLabel36
        // 
        this.xrLabel36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.NomeContato")});
        this.xrLabel36.Dpi = 254F;
        this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(870.6458F, 613.2957F);
        this.xrLabel36.Name = "xrLabel36";
        this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel36.SizeF = new System.Drawing.SizeF(697.3541F, 42.54504F);
        this.xrLabel36.Text = "xrLabel36";
        // 
        // xrLabel35
        // 
        this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.GestorContrato")});
        this.xrLabel35.Dpi = 254F;
        this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 613.2957F);
        this.xrLabel35.Name = "xrLabel35";
        this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel35.SizeF = new System.Drawing.SizeF(833.4375F, 42.54504F);
        this.xrLabel35.Text = "xrLabel35";
        // 
        // xrLabel34
        // 
        this.xrLabel34.Dpi = 254F;
        this.xrLabel34.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(870.6458F, 576.0425F);
        this.xrLabel34.Name = "xrLabel34";
        this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel34.SizeF = new System.Drawing.SizeF(697.3544F, 37.2533F);
        this.xrLabel34.StylePriority.UseFont = false;
        this.xrLabel34.Text = "Gestor Contratada";
        // 
        // xrLabel33
        // 
        this.xrLabel33.Dpi = 254F;
        this.xrLabel33.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel33.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 576.0425F);
        this.xrLabel33.Name = "xrLabel33";
        this.xrLabel33.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel33.SizeF = new System.Drawing.SizeF(833.4375F, 37.2533F);
        this.xrLabel33.StylePriority.UseFont = false;
        this.xrLabel33.Text = "Gestor Contratante";
        // 
        // xrLabel32
        // 
        this.xrLabel32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.NomeFonte")});
        this.xrLabel32.Dpi = 254F;
        this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(870.6458F, 536.1433F);
        this.xrLabel32.Name = "xrLabel32";
        this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel32.SizeF = new System.Drawing.SizeF(697.3544F, 39.89917F);
        this.xrLabel32.Text = "xrLabel32";
        // 
        // xrLabel31
        // 
        this.xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.DescricaoTipoServico")});
        this.xrLabel31.Dpi = 254F;
        this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 536.1433F);
        this.xrLabel31.Name = "xrLabel31";
        this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel31.SizeF = new System.Drawing.SizeF(833.4375F, 39.89917F);
        this.xrLabel31.Text = "xrLabel31";
        // 
        // xrLabel30
        // 
        this.xrLabel30.Dpi = 254F;
        this.xrLabel30.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(870.6457F, 498.8899F);
        this.xrLabel30.Name = "xrLabel30";
        this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel30.SizeF = new System.Drawing.SizeF(253.9999F, 37.25336F);
        this.xrLabel30.StylePriority.UseFont = false;
        this.xrLabel30.Text = "Fonte";
        // 
        // xrLabel29
        // 
        this.xrLabel29.Dpi = 254F;
        this.xrLabel29.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 498.8899F);
        this.xrLabel29.Name = "xrLabel29";
        this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel29.SizeF = new System.Drawing.SizeF(447.1458F, 37.25339F);
        this.xrLabel29.StylePriority.UseFont = false;
        this.xrLabel29.Text = "Tipo de Contratação";
        // 
        // xrLabel28
        // 
        this.xrLabel28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.calculatedField1", "{0:c2}")});
        this.xrLabel28.Dpi = 254F;
        this.xrLabel28.LocationFloat = new DevExpress.Utils.PointFloat(1232.146F, 458.9909F);
        this.xrLabel28.Name = "xrLabel28";
        this.xrLabel28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel28.SizeF = new System.Drawing.SizeF(309.5625F, 39.89902F);
        this.xrLabel28.Text = "xrLabel28";
        // 
        // xrLabel27
        // 
        this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.ValorPago", "{0:c2}")});
        this.xrLabel27.Dpi = 254F;
        this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(1232.146F, 735.6391F);
        this.xrLabel27.Name = "xrLabel27";
        this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel27.SizeF = new System.Drawing.SizeF(343.9586F, 39.89917F);
        this.xrLabel27.Text = "xrLabel27";
        this.xrLabel27.Visible = false;
        // 
        // xrLabel26
        // 
        this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.ValorContrato", "{0:c2}")});
        this.xrLabel26.Dpi = 254F;
        this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(487.646F, 458.9907F);
        this.xrLabel26.Name = "xrLabel26";
        this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel26.SizeF = new System.Drawing.SizeF(309.5625F, 39.89917F);
        this.xrLabel26.Text = "xrLabel26";
        // 
        // xrLabel25
        // 
        this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.ValorContratoOriginal", "{0:c2}")});
        this.xrLabel25.Dpi = 254F;
        this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 458.9907F);
        this.xrLabel25.Name = "xrLabel25";
        this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel25.SizeF = new System.Drawing.SizeF(330.7292F, 39.89917F);
        this.xrLabel25.Text = "xrLabel25";
        // 
        // xrLabel23
        // 
        this.xrLabel23.Dpi = 254F;
        this.xrLabel23.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(870.6458F, 421.7375F);
        this.xrLabel23.Name = "xrLabel23";
        this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel23.SizeF = new System.Drawing.SizeF(343.9584F, 37.2533F);
        this.xrLabel23.StylePriority.UseFont = false;
        this.xrLabel23.Text = "Valor Medido";
        // 
        // xrLabel24
        // 
        this.xrLabel24.Dpi = 254F;
        this.xrLabel24.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(1232.146F, 421.7375F);
        this.xrLabel24.Name = "xrLabel24";
        this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel24.SizeF = new System.Drawing.SizeF(314.8541F, 37.25333F);
        this.xrLabel24.StylePriority.UseFont = false;
        this.xrLabel24.Text = "Saldo Contratual";
        // 
        // xrLabel21
        // 
        this.xrLabel21.Dpi = 254F;
        this.xrLabel21.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 421.7374F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(330.7292F, 37.25333F);
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.Text = "Valor do Contrato";
        // 
        // xrLabel22
        // 
        this.xrLabel22.Dpi = 254F;
        this.xrLabel22.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(487.6459F, 421.7375F);
        this.xrLabel22.Name = "xrLabel22";
        this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel22.SizeF = new System.Drawing.SizeF(347.7916F, 37.2533F);
        this.xrLabel22.StylePriority.UseFont = false;
        this.xrLabel22.Text = "Valor c/ Aditivo/TEC";
        // 
        // xrLabel20
        // 
        this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.DataTermino", "{0:dd/MM/yyyy}")});
        this.xrLabel20.Dpi = 254F;
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(1232.146F, 384.4842F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(309.5625F, 37.2533F);
        this.xrLabel20.Text = "xrLabel20";
        // 
        // xrLabel19
        // 
        this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.DataTerminoOriginal", "{0:dd/MM/yyyy}")});
        this.xrLabel19.Dpi = 254F;
        this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(870.6458F, 384.4841F);
        this.xrLabel19.Name = "xrLabel19";
        this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel19.SizeF = new System.Drawing.SizeF(309.5624F, 37.25333F);
        this.xrLabel19.Text = "xrLabel19";
        // 
        // xrLabel18
        // 
        this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.DataInicio", "{0:dd/MM/yyyy}")});
        this.xrLabel18.Dpi = 254F;
        this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(487.6459F, 384.4842F);
        this.xrLabel18.Name = "xrLabel18";
        this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel18.SizeF = new System.Drawing.SizeF(309.5625F, 37.2533F);
        this.xrLabel18.Text = "xrLabel18";
        // 
        // xrLabel17
        // 
        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.DataAssinatura", "{0:dd/MM/yyyy}")});
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 384.4841F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(309.5625F, 37.25333F);
        this.xrLabel17.Text = "xrLabel17";
        // 
        // xrLabel16
        // 
        this.xrLabel16.Dpi = 254F;
        this.xrLabel16.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(1191.146F, 347.2308F);
        this.xrLabel16.Name = "xrLabel16";
        this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel16.SizeF = new System.Drawing.SizeF(398.854F, 37.25333F);
        this.xrLabel16.StylePriority.UseFont = false;
        this.xrLabel16.Text = "Término c/ Aditivo/TEC";
        // 
        // xrLabel15
        // 
        this.xrLabel15.Dpi = 254F;
        this.xrLabel15.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(870.6457F, 347.2308F);
        this.xrLabel15.Name = "xrLabel15";
        this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel15.SizeF = new System.Drawing.SizeF(309.5626F, 37.25333F);
        this.xrLabel15.StylePriority.UseFont = false;
        this.xrLabel15.Text = "Témino Original";
        // 
        // xrLabel14
        // 
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(487.6458F, 347.2307F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(347.7917F, 37.2533F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.Text = "Emissão OS externa";
        // 
        // xrLabel13
        // 
        this.xrLabel13.Dpi = 254F;
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 347.2308F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(330.7292F, 37.25333F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.Text = "Data Assinatura";
        // 
        // xrLabel12
        // 
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(870.6458F, 262.6783F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(253.9999F, 37.25336F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.Text = "Segmento";
        // 
        // xrLabel11
        // 
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 262.6783F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(254F, 37.25336F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "Município";
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.DescricaoSegmentoObra")});
        this.xrLabel10.Dpi = 254F;
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(870.6458F, 307.3316F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(697.3542F, 39.89923F);
        this.xrLabel10.Text = "xrLabel10";
        // 
        // xrLabel9
        // 
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.NomeMunicipio")});
        this.xrLabel9.Dpi = 254F;
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 307.3316F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(833.4375F, 39.89923F);
        this.xrLabel9.Text = "xrLabel9";
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.DescricaoObjetoContrato")});
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 222.7791F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(1543F, 39.8992F);
        this.xrLabel8.Text = "xrLabel8";
        // 
        // xrLabel7
        // 
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 182.8799F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(254F, 39.89917F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "Objeto";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.NomePessoa")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 142.9808F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(1543F, 39.89917F);
        this.xrLabel6.Text = "xrLabel6";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 105.7275F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(254F, 37.25333F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "Razão Social";
        // 
        // xrLine1
        // 
        this.xrLine1.Dpi = 254F;
        this.xrLine1.LineWidth = 3;
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 83.41998F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(1543F, 10F);
        // 
        // xrLabel4
        // 
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 25.00001F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(1543F, 58.42F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.StylePriority.UseTextAlignment = false;
        this.xrLabel4.Text = "Identificação do contrato";
        this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Dpi = 254F;
        this.TopMargin.HeightF = 254F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.Dpi = 254F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // ds
        // 
        this.ds.DataSetName = "NewDataSet";
        this.ds.EnforceConstraints = false;
        this.ds.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("Rel_DadosContrato_Financeiro", "DadosContrato", "Financeiro", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, false),
            new System.Data.DataRelation("Rel_DadosContrato_Aditivo", "DadosContrato", "Aditivos", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, false),
            new System.Data.DataRelation("Rel_DadosContrato_Comentarios", "DadosContrato", "Comentarios", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, false),
            new System.Data.DataRelation("Rel_DadosContrato_Obrigacao", "DadosContrato", "Obrigacao", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, false)});
        this.ds.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3,
            this.dataTable4,
            this.dataTable5});
        // 
        // dataTable1
        // 
        this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn35,
            this.dataColumn36,
            this.dataColumn37,
            this.dataColumn38,
            this.dataColumn43});
        this.dataTable1.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "CodigoContrato"}, false)});
        this.dataTable1.TableName = "DadosContrato";
        // 
        // dataColumn1
        // 
        this.dataColumn1.ColumnName = "CodigoContrato";
        this.dataColumn1.DataType = typeof(int);
        // 
        // dataColumn2
        // 
        this.dataColumn2.ColumnName = "NumeroContrato";
        // 
        // dataColumn3
        // 
        this.dataColumn3.ColumnName = "SituacaoContrato";
        // 
        // dataColumn4
        // 
        this.dataColumn4.ColumnName = "NomePessoa";
        // 
        // dataColumn5
        // 
        this.dataColumn5.ColumnName = "DescricaoObjetoContrato";
        // 
        // dataColumn6
        // 
        this.dataColumn6.ColumnName = "NomeMunicipio";
        // 
        // dataColumn7
        // 
        this.dataColumn7.ColumnName = "DescricaoSegmentoObra";
        // 
        // dataColumn8
        // 
        this.dataColumn8.ColumnName = "DataAssinatura";
        this.dataColumn8.DataType = typeof(System.DateTime);
        // 
        // dataColumn9
        // 
        this.dataColumn9.ColumnName = "DataInicio";
        this.dataColumn9.DataType = typeof(System.DateTime);
        // 
        // dataColumn10
        // 
        this.dataColumn10.ColumnName = "DataTerminoOriginal";
        this.dataColumn10.DataType = typeof(System.DateTime);
        // 
        // dataColumn11
        // 
        this.dataColumn11.ColumnName = "DataTermino";
        this.dataColumn11.DataType = typeof(System.DateTime);
        // 
        // dataColumn12
        // 
        this.dataColumn12.ColumnName = "ValorContratoOriginal";
        this.dataColumn12.DataType = typeof(decimal);
        // 
        // dataColumn13
        // 
        this.dataColumn13.ColumnName = "ValorContrato";
        this.dataColumn13.DataType = typeof(double);
        // 
        // dataColumn14
        // 
        this.dataColumn14.ColumnName = "ValorPago";
        this.dataColumn14.DataType = typeof(decimal);
        // 
        // dataColumn15
        // 
        this.dataColumn15.ColumnName = "DescricaoTipoServico";
        // 
        // dataColumn16
        // 
        this.dataColumn16.ColumnName = "NomeFonte";
        // 
        // dataColumn17
        // 
        this.dataColumn17.ColumnName = "GestorContrato";
        // 
        // dataColumn18
        // 
        this.dataColumn18.ColumnName = "NomeContato";
        // 
        // dataColumn35
        // 
        this.dataColumn35.Caption = "NumeroSAP";
        this.dataColumn35.ColumnName = "NumeroSAP";
        // 
        // dataColumn36
        // 
        this.dataColumn36.Caption = "Observacao";
        this.dataColumn36.ColumnName = "Observacao";
        // 
        // dataColumn37
        // 
        this.dataColumn37.Caption = "ValorGarantia";
        this.dataColumn37.ColumnName = "ValorGarantia";
        this.dataColumn37.DataType = typeof(decimal);
        // 
        // dataColumn38
        // 
        this.dataColumn38.Caption = "PercGarantia";
        this.dataColumn38.ColumnName = "PercGarantia";
        this.dataColumn38.DataType = typeof(decimal);
        // 
        // dataColumn43
        // 
        this.dataColumn43.ColumnName = "RetencaoGarantia";
        // 
        // dataTable2
        // 
        this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24});
        this.dataTable2.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Rel_DadosContrato_Financeiro", "DadosContrato", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade)});
        this.dataTable2.TableName = "Financeiro";
        // 
        // dataColumn19
        // 
        this.dataColumn19.ColumnName = "CodigoContrato";
        this.dataColumn19.DataType = typeof(int);
        // 
        // dataColumn20
        // 
        this.dataColumn20.ColumnName = "NumeroParcela";
        this.dataColumn20.DataType = typeof(int);
        // 
        // dataColumn21
        // 
        this.dataColumn21.ColumnName = "DataMedicao";
        this.dataColumn21.DataType = typeof(System.DateTime);
        // 
        // dataColumn22
        // 
        this.dataColumn22.ColumnName = "ValorMedicao";
        this.dataColumn22.DataType = typeof(decimal);
        // 
        // dataColumn23
        // 
        this.dataColumn23.ColumnName = "DataPagamento";
        this.dataColumn23.DataType = typeof(System.DateTime);
        // 
        // dataColumn24
        // 
        this.dataColumn24.ColumnName = "ValorPago";
        this.dataColumn24.DataType = typeof(decimal);
        // 
        // dataTable3
        // 
        this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn28,
            this.dataColumn29,
            this.dataColumn30});
        this.dataTable3.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Rel_DadosContrato_Aditivo", "DadosContrato", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade)});
        this.dataTable3.TableName = "Aditivos";
        // 
        // dataColumn25
        // 
        this.dataColumn25.ColumnName = "CodigoContrato";
        this.dataColumn25.DataType = typeof(int);
        // 
        // dataColumn26
        // 
        this.dataColumn26.ColumnName = "NumeroContratoAditivo";
        // 
        // dataColumn27
        // 
        this.dataColumn27.ColumnName = "DataInclusao";
        this.dataColumn27.DataType = typeof(System.DateTime);
        // 
        // dataColumn28
        // 
        this.dataColumn28.ColumnName = "ValorAditivo";
        this.dataColumn28.DataType = typeof(decimal);
        // 
        // dataColumn29
        // 
        this.dataColumn29.ColumnName = "NovoValorContrato";
        this.dataColumn29.DataType = typeof(decimal);
        // 
        // dataColumn30
        // 
        this.dataColumn30.ColumnName = "NovaDataVigencia";
        this.dataColumn30.DataType = typeof(System.DateTime);
        // 
        // dataTable4
        // 
        this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn33,
            this.dataColumn34});
        this.dataTable4.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Rel_DadosContrato_Comentarios", "DadosContrato", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade)});
        this.dataTable4.TableName = "Comentarios";
        // 
        // dataColumn31
        // 
        this.dataColumn31.ColumnName = "CodigoContrato";
        this.dataColumn31.DataType = typeof(int);
        // 
        // dataColumn32
        // 
        this.dataColumn32.ColumnName = "DataInclusao";
        this.dataColumn32.DataType = typeof(System.DateTime);
        // 
        // dataColumn33
        // 
        this.dataColumn33.ColumnName = "UsuarioInclusao";
        // 
        // dataColumn34
        // 
        this.dataColumn34.ColumnName = "Comentario";
        // 
        // dataTable5
        // 
        this.dataTable5.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn39,
            this.dataColumn40,
            this.dataColumn41,
            this.dataColumn42});
        this.dataTable5.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("Rel_DadosContrato_Obrigacao", "DadosContrato", new string[] {
                        "CodigoContrato"}, new string[] {
                        "CodigoContrato"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade)});
        this.dataTable5.TableName = "Obrigacao";
        // 
        // dataColumn39
        // 
        this.dataColumn39.ColumnName = "CodigoContrato";
        this.dataColumn39.DataType = typeof(int);
        // 
        // dataColumn40
        // 
        this.dataColumn40.ColumnName = "CodigoObrigacoesContratada";
        this.dataColumn40.DataType = typeof(int);
        // 
        // dataColumn41
        // 
        this.dataColumn41.ColumnName = "Ocorrencia";
        // 
        // dataColumn42
        // 
        this.dataColumn42.ColumnName = "ObrigacaoContratada";
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel41,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrPictureBox1});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 250F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // xrLabel41
        // 
        this.xrLabel41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.NumeroSAP", "Nº SAP - {0}")});
        this.xrLabel41.Dpi = 254F;
        this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(1009.896F, 83.41998F);
        this.xrLabel41.Name = "xrLabel41";
        this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel41.SizeF = new System.Drawing.SizeF(558.1039F, 58.41996F);
        this.xrLabel41.Text = "xrLabel41";
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.SituacaoContrato", "Status - {0}")});
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(425.0001F, 141.8399F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(1143F, 58.42001F);
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLabel2
        // 
        this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.NumeroContrato", "Nº do Contrato - {0}")});
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(425.0001F, 83.41998F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(568.8542F, 58.41996F);
        this.xrLabel2.Text = "Nº do Contrato";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(425.0001F, 25.00001F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(1143F, 58.42F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Relatório de Contrato";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Dpi = 254F;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 25.00001F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(400F, 200F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        // 
        // DetailReport
        // 
        this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1,
            this.GroupFooter1});
        this.DetailReport.DataMember = "DadosContrato.Rel_DadosContrato_Financeiro";
        this.DetailReport.DataSource = this.ds;
        this.DetailReport.Dpi = 254F;
        this.DetailReport.Level = 0;
        this.DetailReport.Name = "DetailReport";
        // 
        // Detail1
        // 
        this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail1.Dpi = 254F;
        this.Detail1.HeightF = 63.5F;
        this.Detail1.Name = "Detail1";
        // 
        // xrTable1
        // 
        this.xrTable1.Dpi = 254F;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow1.Dpi = 254F;
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 0.5679012345679012D;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Financeiro.NumeroParcela")});
        this.xrTableCell2.Dpi = 254F;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell2.Weight = 2.5490816643627263D;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Financeiro.DataMedicao", "{0:dd/MM/yyyy}")});
        this.xrTableCell3.Dpi = 254F;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseTextAlignment = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell3.Weight = 4.4957848710294614D;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Financeiro.ValorMedicao", "{0:c2}")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell4.Weight = 4.8573764572074829D;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Financeiro.DataPagamento", "{0:dd/MM/yyyy}")});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell5.Weight = 4.7525198012513137D;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Financeiro.ValorPago", "{0:c2}")});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell6.Weight = 5.4321937278881451D;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel37,
            this.xrTable2});
        this.GroupHeader1.Dpi = 254F;
        this.GroupHeader1.HeightF = 116F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLabel37
        // 
        this.xrLabel37.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel37.BorderWidth = 2;
        this.xrLabel37.Dpi = 254F;
        this.xrLabel37.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel37.Name = "xrLabel37";
        this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel37.SizeF = new System.Drawing.SizeF(1593F, 41.9167F);
        this.xrLabel37.StylePriority.UseBorders = false;
        this.xrLabel37.StylePriority.UseBorderWidth = false;
        this.xrLabel37.StylePriority.UseFont = false;
        this.xrLabel37.StylePriority.UseTextAlignment = false;
        this.xrLabel37.Text = "Financeiro";
        this.xrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Dpi = 254F;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 52.50003F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        this.xrTable2.StylePriority.UseTextAlignment = false;
        this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 0.5679012345679012D;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Dpi = 254F;
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBackColor = false;
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Parcela";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell1.Weight = 2.5491394211219811D;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell7.Dpi = 254F;
        this.xrTableCell7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBackColor = false;
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Data Medição";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell7.Weight = 4.4957288067759711D;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell8.Dpi = 254F;
        this.xrTableCell8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBackColor = false;
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "Valor Medição";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell8.Weight = 4.8573734953223937D;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell9.Dpi = 254F;
        this.xrTableCell9.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBackColor = false;
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "Data Pagamento";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell9.Weight = 4.7525181087455479D;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBackColor = false;
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Valor Pago";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell10.Weight = 5.4321966897732352D;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
        this.GroupFooter1.Dpi = 254F;
        this.GroupFooter1.HeightF = 63.5F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.Dpi = 254F;
        this.xrTable7.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable7.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        this.xrTable7.StylePriority.UseBorders = false;
        this.xrTable7.StylePriority.UseFont = false;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell27,
            this.xrTableCell30,
            this.xrTableCell32});
        this.xrTableRow7.Dpi = 254F;
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 0.5679012345679012D;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell29.Dpi = 254F;
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseBackColor = false;
        this.xrTableCell29.StylePriority.UseBorders = false;
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.Text = "Total";
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell29.Weight = 5.5873083526694112D;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Financeiro.ValorMedicao")});
        this.xrTableCell27.Dpi = 254F;
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseBackColor = false;
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        xrSummary1.FormatString = "{0:c2}";
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell27.Summary = xrSummary1;
        this.xrTableCell27.Text = "xrTableCell27";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell27.Weight = 3.8524005480382422D;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell30.Dpi = 254F;
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBackColor = false;
        this.xrTableCell30.Weight = 3.7692369248043178D;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.BackColor = System.Drawing.Color.Silver;
        this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Financeiro.ValorPago")});
        this.xrTableCell32.Dpi = 254F;
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseBackColor = false;
        this.xrTableCell32.StylePriority.UseTextAlignment = false;
        xrSummary2.FormatString = "{0:c2}";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell32.Summary = xrSummary2;
        this.xrTableCell32.Text = "xrTableCell32";
        this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell32.Weight = 4.3082955537983754D;
        // 
        // calculatedField1
        // 
        this.calculatedField1.Name = "calculatedField1";
        // 
        // DetailReport1
        // 
        this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader2,
            this.GroupFooter2});
        this.DetailReport1.DataMember = "DadosContrato.Rel_DadosContrato_Aditivo";
        this.DetailReport1.DataSource = this.ds;
        this.DetailReport1.Dpi = 254F;
        this.DetailReport1.Level = 1;
        this.DetailReport1.Name = "DetailReport1";
        // 
        // Detail2
        // 
        this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.Detail2.Dpi = 254F;
        this.Detail2.HeightF = 63.5F;
        this.Detail2.KeepTogether = true;
        this.Detail2.Name = "Detail2";
        // 
        // xrTable3
        // 
        this.xrTable3.Dpi = 254F;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16});
        this.xrTableRow3.Dpi = 254F;
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 0.5679012345679012D;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Aditivo.NumeroContratoAditivo")});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell12.Weight = 4.2441367668864629D;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Aditivo.DataInclusao", "{0:dd/MM/yyyy}")});
        this.xrTableCell13.Dpi = 254F;
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell13.Weight = 3.5344204397817633D;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Aditivo.ValorAditivo", "{0:c2}")});
        this.xrTableCell14.Dpi = 254F;
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseBorders = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell14.Weight = 5.1485399165890513D;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Aditivo.NovoValorContrato", "{0:c2}")});
        this.xrTableCell15.Dpi = 254F;
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell15.Weight = 5.6254372661943215D;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Aditivo.NovaDataVigencia", "{0:dd/MM/yyyy}")});
        this.xrTableCell16.Dpi = 254F;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell16.Weight = 3.53442213228753D;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel38,
            this.xrTable4});
        this.GroupHeader2.Dpi = 254F;
        this.GroupHeader2.HeightF = 116F;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrLabel38
        // 
        this.xrLabel38.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel38.BorderWidth = 2;
        this.xrLabel38.Dpi = 254F;
        this.xrLabel38.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel38.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel38.Name = "xrLabel38";
        this.xrLabel38.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel38.SizeF = new System.Drawing.SizeF(1593F, 41.9167F);
        this.xrLabel38.StylePriority.UseBorders = false;
        this.xrLabel38.StylePriority.UseBorderWidth = false;
        this.xrLabel38.StylePriority.UseFont = false;
        this.xrLabel38.StylePriority.UseTextAlignment = false;
        this.xrLabel38.Text = "Aditivos";
        this.xrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTable4
        // 
        this.xrTable4.Dpi = 254F;
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 51.50001F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20});
        this.xrTableRow4.Dpi = 254F;
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 0.5679012345679012D;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell11.Dpi = 254F;
        this.xrTableCell11.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBackColor = false;
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "Nº Aditivo";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell11.Weight = 4.2441367668864629D;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell17.Dpi = 254F;
        this.xrTableCell17.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBackColor = false;
        this.xrTableCell17.StylePriority.UseBorders = false;
        this.xrTableCell17.StylePriority.UseFont = false;
        this.xrTableCell17.StylePriority.UseTextAlignment = false;
        this.xrTableCell17.Text = "Data Inclusão";
        this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell17.Weight = 3.5344204397817633D;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.Dpi = 254F;
        this.xrTableCell18.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBackColor = false;
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.StylePriority.UseFont = false;
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = "Valor Aditado";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell18.Weight = 5.1485399165890513D;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell19.Dpi = 254F;
        this.xrTableCell19.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseBackColor = false;
        this.xrTableCell19.StylePriority.UseBorders = false;
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "Novo Valor";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell19.Weight = 5.6254372661943215D;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell20.Dpi = 254F;
        this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBackColor = false;
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "Novo Prazo";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell20.Weight = 3.53442213228753D;
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
        this.GroupFooter2.Dpi = 254F;
        this.GroupFooter2.HeightF = 63.5F;
        this.GroupFooter2.KeepTogether = true;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // xrTable8
        // 
        this.xrTable8.BackColor = System.Drawing.Color.Silver;
        this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable8.Dpi = 254F;
        this.xrTable8.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable8.Name = "xrTable8";
        this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable8.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        this.xrTable8.StylePriority.UseBackColor = false;
        this.xrTable8.StylePriority.UseBorders = false;
        this.xrTable8.StylePriority.UseFont = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31,
            this.xrTableCell36,
            this.xrTableCell28});
        this.xrTableRow8.Dpi = 254F;
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 0.5679012345679012D;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Dpi = 254F;
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "Total";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.xrTableCell31.Weight = 6.1692006875765788D;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Aditivo.ValorAditivo")});
        this.xrTableCell36.Dpi = 254F;
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseTextAlignment = false;
        xrSummary3.FormatString = "{0:c2}";
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell36.Summary = xrSummary3;
        this.xrTableCell36.Text = "xrTableCell36";
        this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        this.xrTableCell36.Weight = 4.0833252095016714D;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Dpi = 254F;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Weight = 7.2647154822320958D;
        // 
        // DetailReport2
        // 
        this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader3});
        this.DetailReport2.DataMember = "DadosContrato.Rel_DadosContrato_Comentarios";
        this.DetailReport2.DataSource = this.ds;
        this.DetailReport2.Dpi = 254F;
        this.DetailReport2.Level = 2;
        this.DetailReport2.Name = "DetailReport2";
        // 
        // Detail3
        // 
        this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.Detail3.Dpi = 254F;
        this.Detail3.HeightF = 63.5F;
        this.Detail3.Name = "Detail3";
        // 
        // xrTable5
        // 
        this.xrTable5.Dpi = 254F;
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell24});
        this.xrTableRow5.Dpi = 254F;
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 0.5679012345679012D;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Comentarios.DataInclusao", "{0:dd/MM/yyyy}")});
        this.xrTableCell21.Dpi = 254F;
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseBorders = false;
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.Weight = 0.33835405767205773D;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Comentarios.UsuarioInclusao")});
        this.xrTableCell22.Dpi = 254F;
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseBorders = false;
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.Weight = 0.54206188686867018D;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Comentarios.Comentario")});
        this.xrTableCell24.Dpi = 254F;
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.Weight = 0.88041594454072791D;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel39,
            this.xrTable6});
        this.GroupHeader3.Dpi = 254F;
        this.GroupHeader3.HeightF = 119F;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // xrLabel39
        // 
        this.xrLabel39.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel39.BorderWidth = 2;
        this.xrLabel39.Dpi = 254F;
        this.xrLabel39.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel39.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 0F);
        this.xrLabel39.Name = "xrLabel39";
        this.xrLabel39.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel39.SizeF = new System.Drawing.SizeF(1593F, 41.9167F);
        this.xrLabel39.StylePriority.UseBorders = false;
        this.xrLabel39.StylePriority.UseBorderWidth = false;
        this.xrLabel39.StylePriority.UseFont = false;
        this.xrLabel39.StylePriority.UseTextAlignment = false;
        this.xrLabel39.Text = "Comentários";
        this.xrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTable6
        // 
        this.xrTable6.Dpi = 254F;
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(8.074442E-05F, 54.27091F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
        this.xrTable6.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell25,
            this.xrTableCell26});
        this.xrTableRow6.Dpi = 254F;
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 0.5679012345679012D;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell23.Dpi = 254F;
        this.xrTableCell23.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseBackColor = false;
        this.xrTableCell23.StylePriority.UseBorders = false;
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.Text = "Data";
        this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell23.Weight = 4.2441367668864629D;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell25.Dpi = 254F;
        this.xrTableCell25.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBackColor = false;
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.StylePriority.UseFont = false;
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.Text = "Responsável";
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell25.Weight = 6.7993410708566593D;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell26.Dpi = 254F;
        this.xrTableCell26.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBackColor = false;
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.StylePriority.UseFont = false;
        this.xrTableCell26.StylePriority.UseTextAlignment = false;
        this.xrTableCell26.Text = "Comentário";
        this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell26.Weight = 11.043478683996007D;
        // 
        // calculatedField2
        // 
        this.calculatedField2.Name = "calculatedField2";
        // 
        // PercValorGarantia
        // 
        this.PercValorGarantia.DataMember = "DadosContrato";
        this.PercValorGarantia.DisplayName = "PercValorGarantia";
        this.PercValorGarantia.Expression = "Iif([PercGarantia]>0,[PercGarantia]  ,[ValorGarantia] )";
        this.PercValorGarantia.Name = "PercValorGarantia";
        // 
        // DetailReport3
        // 
        this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.ObrigacoesContratada});
        this.DetailReport3.DataMember = "DadosContrato.Rel_DadosContrato_Obrigacao";
        this.DetailReport3.DataSource = this.ds;
        this.DetailReport3.Dpi = 254F;
        this.DetailReport3.Level = 3;
        this.DetailReport3.Name = "DetailReport3";
        // 
        // Detail4
        // 
        this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
        this.Detail4.Dpi = 254F;
        this.Detail4.HeightF = 63.5F;
        this.Detail4.Name = "Detail4";
        // 
        // xrTable9
        // 
        this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable9.Dpi = 254F;
        this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable9.Name = "xrTable9";
        this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable9.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        this.xrTable9.StylePriority.UseBorders = false;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell35});
        this.xrTableRow9.Dpi = 254F;
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 0.5679012345679012D;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Obrigacao.ObrigacaoContratada")});
        this.xrTableCell34.Dpi = 254F;
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Text = "xrTableCell34";
        this.xrTableCell34.Weight = 0.34566916946146092D;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DadosContrato.Rel_DadosContrato_Obrigacao.Ocorrencia")});
        this.xrTableCell35.Dpi = 254F;
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.Text = "xrTableCell35";
        this.xrTableCell35.Weight = 0.24493366224194005D;
        // 
        // ObrigacoesContratada
        // 
        this.ObrigacoesContratada.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10,
            this.xrLabel45});
        this.ObrigacoesContratada.Dpi = 254F;
        this.ObrigacoesContratada.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WholePage;
        this.ObrigacoesContratada.HeightF = 113F;
        this.ObrigacoesContratada.Name = "ObrigacoesContratada";
        this.ObrigacoesContratada.RepeatEveryPage = true;
        // 
        // xrTable10
        // 
        this.xrTable10.Dpi = 254F;
        this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.5F);
        this.xrTable10.Name = "xrTable10";
        this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable10.SizeF = new System.Drawing.SizeF(1593F, 63.5F);
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37,
            this.xrTableCell38});
        this.xrTableRow10.Dpi = 254F;
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 0.5679012345679012D;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell37.Dpi = 254F;
        this.xrTableCell37.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.StylePriority.UseBackColor = false;
        this.xrTableCell37.StylePriority.UseBorders = false;
        this.xrTableCell37.StylePriority.UseFont = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        this.xrTableCell37.Text = "Obrigação";
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell37.Weight = 12.927097123257276D;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.BackColor = System.Drawing.Color.PowderBlue;
        this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell38.Dpi = 254F;
        this.xrTableCell38.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTableCell38.Multiline = true;
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseBackColor = false;
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.StylePriority.UseFont = false;
        this.xrTableCell38.StylePriority.UseTextAlignment = false;
        this.xrTableCell38.Text = "Ocorrência\r\n";
        this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
        this.xrTableCell38.Weight = 9.1598593984818528D;
        // 
        // xrLabel45
        // 
        this.xrLabel45.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrLabel45.BorderWidth = 2;
        this.xrLabel45.Dpi = 254F;
        this.xrLabel45.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(0.0001614888F, 0F);
        this.xrLabel45.Name = "xrLabel45";
        this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel45.SizeF = new System.Drawing.SizeF(1593F, 41.9167F);
        this.xrLabel45.StylePriority.UseBorders = false;
        this.xrLabel45.StylePriority.UseBorderWidth = false;
        this.xrLabel45.StylePriority.UseFont = false;
        this.xrLabel45.StylePriority.UseTextAlignment = false;
        this.xrLabel45.Text = "Obrigações Contratada";
        this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // txtGarantia
        // 
        this.txtGarantia.DataMember = "DadosContrato";
        this.txtGarantia.Expression = "Iif([PercGarantia]>0,\'% Garantia\'  ,\'Valor Garantia\' )";
        this.txtGarantia.Name = "txtGarantia";
        // 
        // rel_Contrato
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.DetailReport,
            this.DetailReport1,
            this.DetailReport2,
            this.DetailReport3});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1,
            this.calculatedField2,
            this.PercValorGarantia,
            this.txtGarantia});
        this.DataMember = "DadosContrato";
        this.DataSource = this.ds;
        this.Dpi = 254F;
        this.Font = new System.Drawing.Font("Verdana", 8F);
        this.Margins = new System.Drawing.Printing.Margins(246, 243, 254, 100);
        this.PageHeight = 2969;
        this.PageWidth = 2101;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.SnapGridSize = 31.75F;
        this.Version = "11.2";
        ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrLabel46_TextChanged(object sender, EventArgs e)
    {
        xrLabel48.Visible = xrLabel46.Text != "Não Informado";
        xrLabel49.Visible = xrLabel46.Text == "Garantia";
        xrLabel50.Visible = xrLabel46.Text == "Carta Fiança";
    }
}
