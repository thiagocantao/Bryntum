using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using System.IO;
using DevExpress.XtraPrinting;

public partial class _Projetos_DadosProjeto_FinanceiroDetalhes : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int idProjeto;
    public string alturaTabela;
    public string larguraTabela = "";
    string nomeBancoSistemaOrcamento;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        nomeBancoSistemaOrcamento = getNomeBancoSistemaOrcamento();

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_CnsFinDtl");
            cDados.aplicaEstiloVisual(Page);

            populaComboAnoProjeto();

            populaGridDetalhe();
        }
       
        defineAlturaTela();
    }

    private string getNomeBancoSistemaOrcamento()
    {
        // Busca o nome do banco de dados do sistema de orçamento
        string nomeBancoSistemaOrcamento = "dbCdisOrcamento";

        DataSet ds = cDados.getParametrosSistema("nomeBancoSistemaOrcamento");

        if (ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0]["nomeBancoSistemaOrcamento"] + "" != "")
            nomeBancoSistemaOrcamento = ds.Tables[0].Rows[0]["nomeBancoSistemaOrcamento"] + "";

        return nomeBancoSistemaOrcamento;
    }

    private void populaComboAnoProjeto()
    {
        try
        {
            string comandoSQL = string.Format(
                @"SELECT distinct mo.ano
                    FROM projetocr pcr inner join
                         {0}.dbo.cr on cr.codigocr = pcr.codigoCr inner join
                         {0}.dbo.MovimentoOrcamento mo on mo.codigoMovimentoOrcamento = cr.codigoMovimentoOrcamento
                   WHERE codigoProjeto = {1}
                   ORDER BY ano", nomeBancoSistemaOrcamento, idProjeto);

            DataSet ds = cDados.getDataSet(comandoSQL);
            cmbAnoReferencia.DataSource = ds;
            cmbAnoReferencia.ValueType = Type.GetType("System.Int32");
            cmbAnoReferencia.TextField = "Ano";
            cmbAnoReferencia.ValueField = "Ano";
            cmbAnoReferencia.DataBind();

            // se o ano corrente está na lista, este deve ser o selecionado
            if (cmbAnoReferencia.Items.FindByValue(DateTime.Now.Year) != null)
                cmbAnoReferencia.Value = DateTime.Now.Year;
            else
            {
                //se existir apenas um item, este será o selecionado
                if (cmbAnoReferencia.Items.Count == 1)
                    cmbAnoReferencia.SelectedIndex = 0;
                else
                    // seleciona o último ano da lista
                    cmbAnoReferencia.SelectedIndex = cmbAnoReferencia.Items.Count - 1;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void cmbAnoReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        populaGridDetalhe();
    }

    private void populaGridDetalhe()
    {
        DataSet ds = getDadosFinanceiro_DetalheNivel1(idProjeto, int.Parse(cmbAnoReferencia.Text == "" ? "-1" : cmbAnoReferencia.Text));
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            foreach (GridViewColumn coluna in gvDados.Columns)
            {
                if (coluna.Name.Substring(0, 2) == "M_")
                {
                    string mes = coluna.Name.Substring(2);
                    mes = getMesPorExtenso(int.Parse(mes)).Substring(0, 3);

                    coluna.Caption = mes + "/" + cmbAnoReferencia.Text;
                }
                else if (coluna.Name == "TotalAno")
                    coluna.Caption = "Total " + cmbAnoReferencia.Text;

            }
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

            // se a grid de detalhes está vísivel, atualiza os valores
            if (hfGeral.Contains("CodigoProjeto"))
            {
               // populaGridDetalhesMes();
                pnGridDetalheMes.ClientVisible = false;
            }
        }
    }

    private void populaGridDetalhesMes()
    {
        int codigoProjeto = int.Parse(hfGeral["CodigoProjeto"].ToString());
        int mes = int.Parse(hfGeral["Mes"].ToString());
        string tipo = hfGeral["Tipo"].ToString();
        string DespesaReceita = hfGeral["DespesaReceita"].ToString();

        // se tipo for "Realizado" - busca os valores na tabela "LancamentoFinanceiroERP_Espelho"
        string comandoSQL = string.Format(
            @"declare @codigoEntidadeOrcamento int
                  set @codigoEntidadeOrcamento = (select codigoReservado from unidadeNegocio where codigoUnidadeNegocio = {1})
              declare @codigoEmpresaERP varchar(10)
                  set @codigoEmpresaERP = (select codigoReservado from {0}.dbo.entidadeOrcamento where codigoentidade = @codigoEntidadeOrcamento )

              select esp.UNIDADE_COD as codigoUnidade, esp.UNIDADE_DES as nomeUnidade,
                     cr_cod as codigoCR, esp.CR_DES as nomeCR,
                     CONTA2_COD as codigoGrupo, CONTA2_DES as nomeGrupo,
                     CONTA_COD as codigoConta, CONTA_DES as nomeConta,
                     esp.descricao as lancamento, valor, DATA_LANCTO as dataLancamento
                from {0}.dbo.CR inner join
                     {0}.dbo.UnidadeNegocioOrcamento uno on uno.CodigoUnidadeNegocio = cr.codigoUnidadeNegocioOrcamento inner join
                     {0}.dbo.EntidadeOrcamento eo on eo.CodigoEntidade = uno.CodigoEntidade inner join
                     ProjetoCR pcr on pcr.CodigoCR = cr.codigocr inner join
                     {0}.dbo.LancamentoFinanceiroERP_Espelho esp on esp.CR_COD = cr.CodigoReservado and 
                                                                    esp.UNIDADE_COD = uno.CodigoReservado and
                                                                    esp.EMPRESA_COD = eo.CodigoReservado
              WHERE CodigoProjeto = {2} 
                and esp.ANO = {3}
                and esp.MES = {4}
                and left(conta_cod,1) = {5}
                and cr.dataExclusao is null
                --and esp.empresa_cod = @codigoEmpresaERP 
               AND valor != 0
              ORDER BY codigoUnidade, codigoCR, codigoConta", nomeBancoSistemaOrcamento, codigoEntidadeUsuarioResponsavel, codigoProjeto, cmbAnoReferencia.Text, mes, DespesaReceita[0] == 'D' ? 3 : 4);

        gvDetalhesMes.Columns["codigoUnidade"].Visible = true;
        gvDetalhesMes.Columns["nomeUnidade"].Visible = true;
        gvDetalhesMes.Columns["lancamento"].Visible = true;
        gvDetalhesMes.Columns["dataLancamento"].Visible = true;


        // se tipo for "Orcado" - busca os valores na visão "vwOrcamentoProjeto", mas é preciso localizar a etapa em execução para o ano de referência
        if (tipo == "Orcado")
        {
//            comandoSQL = string.Format(
//            @"DECLARE @CodigoEntidade int
//                  SET @CodigoEntidade = ( SELECT CodigoEntidade
//                                            FROM {0}.dbo.CR inner join
//                                                 {0}.dbo.UnidadeNegocioOrcamento uno on uno.CodigoUnidadeNegocio = cr.codigoUnidadeNegocioOrcamento
//                                           WHERE CodigoCR = (SELECT Max(CodigoCR) FROM projetoCR WHERE codigoProjeto = {1}) )
//
//              DECLARE @CodigoEtapaMovimentoOrcamento int
//                  SET @CodigoEtapaMovimentoOrcamento = (SELECT CodigoEtapaMovimentoOrcamento 
//                                                          FROM {0}.dbo.MovimentoOrcamentoEntidadeEtapa
//                                                         WHERE CodigoStatusMovimentoOrcamento = 1
//                                                           AND CodigoMovimentoOrcamento = (SELECT CodigoMovimentoOrcamento 
//                                                                                             FROM {0}.dbo.MovimentoOrcamento 
//                                                                                            WHERE Ano = {2} )
//                                                           AND CodigoEntidade = @CodigoEntidade )
//
//              SELECT v.CodigoReservadoUnidade as codigoUnidade, v.NomeUnidadeNegocio as nomeUnidade,
//                     v.CodigoReservadoCR as codigoCR, v.NomeCR as nomeCR,
//                     v.CodigoReservadoContaNivel4 as codigoGrupo, v.DescricaoContaNivel4 as nomeGrupo,
//                     v.CodigoReservadoConta as codigoConta, v.DescricaoConta as nomeConta,
//                     v.DescricaoItem as lancamento, v.ValorOrcamento as valor, v.DataInclusao as dataLancamento 
//                FROM {0}.dbo.CR INNER JOIN
//                     {0}.dbo.UnidadeNegocioOrcamento uno on uno.CodigoUnidadeNegocio = cr.codigoUnidadeNegocioOrcamento INNER JOIN
//                     ProjetoCR pcr on pcr.CodigoCR = cr.codigocr INNER JOIN
//                     {0}.dbo.vwOrcamentoProjeto v on v.CodigoCR = pcr.CodigoCR
//               WHERE CodigoProjeto = {1} 
//                 AND v.ANO = {2}
//                 AND v.MES = {3}
//                 AND v.DespesaReceita = '{4}'
//                 AND v.CodigoEtapaMovimentoOrcamento = @CodigoEtapaMovimentoOrcamento
//               ORDER BY codigoUnidade, codigoCR, codigoConta", nomeBancoSistemaOrcamento, codigoProjeto, cmbAnoReferencia.Text, mes, DespesaReceita[0]);

            comandoSQL = string.Format(
                @"SELECT codigoReservadoCR as codigoCR, nomeCR,
                         codigoReservadoGrupoConta as codigoGrupo, DescricaoGrupoConta as nomeGrupo, 
                         codigoReservadoConta as codigoConta, DescricaoConta as nomeConta, valorOrcado as valor
                    FROM f_GetOrcamentoProjetoComCR({0}, year(GETDATE()))
                   WHERE ano = {1}
                     AND mes = {2}
                     AND despesaReceita = '{3}'
                     AND valorOrcado != 0", codigoProjeto, cmbAnoReferencia.Text, mes, DespesaReceita[0]);

            gvDetalhesMes.Columns["codigoUnidade"].Visible = false;
            gvDetalhesMes.Columns["nomeUnidade"].Visible = false;
            gvDetalhesMes.Columns["lancamento"].Visible = false;
            gvDetalhesMes.Columns["dataLancamento"].Visible = false;
        }

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDetalhesMes.DataSource = ds;
        gvDetalhesMes.DataBind();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

     //   pnDiv.Height = (alturaPrincipal - 225);//a div vai ficar com essa altura
     //   pnDiv.Width = (larguraPrincipal - 200);

    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
        string nomeArquivo = "";
        if (((Control)sender).ID == "btnExcel1")
        {
            populaGridDetalhe();
            gvExporter.GridViewID = "gvDados";
            nomeArquivo = "DetalhesFinanceiro";
        }
        else
        {
            populaGridDetalhesMes();
            gvExporter.GridViewID = "gvDetalhesMes";
            nomeArquivo = "DetalhesFinanceiroMes";
        }
        nomeArquivo += dataHora + ".xls";

        using (MemoryStream stream = new MemoryStream())
        {
            string app = "", erro = "";

            try
            {
                XlsExportOptionsEx x = new XlsExportOptionsEx();

                gvExporter.WriteXls(stream, x);
                //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                app = "application/ms-excel";
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();
            }
            else
            {
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }

    private DataSet getDadosFinanceiro_DetalheNivel1(int codigoProjeto, int anoReferencia)
    {
        string comandoSQL = string.Format(
            @"DECLARE @CodigoProjetoParam INT, 
                      @AnoRef int
           
                  SET @CodigoProjetoParam = {2}
                  SET @AnoRef = {3}
                               
               SELECT DespesaReceita = (case when DespesaReceita = 'D' then 'Despesa' else 'Receita' end),
                      'Orcado' as TipoValor, 
                      AnoAnterior = isnull(SUM(case when Ano < @AnoRef then ValorOrcado end),0),
                      Mes_01 = isnull(SUM(case when Ano = @AnoRef and mes = 1 then ValorOrcado end),0),
                      Mes_02 = isnull(SUM(case when Ano = @AnoRef and mes = 2 then ValorOrcado end),0),
                      Mes_03 = isnull(SUM(case when Ano = @AnoRef and mes = 3 then ValorOrcado end),0),
                      Mes_04 = isnull(SUM(case when Ano = @AnoRef and mes = 4 then ValorOrcado end),0),
                      Mes_05 = isnull(SUM(case when Ano = @AnoRef and mes = 5 then ValorOrcado end),0),
                      Mes_06 = isnull(SUM(case when Ano = @AnoRef and mes = 6 then ValorOrcado end),0),
                      Mes_07 = isnull(SUM(case when Ano = @AnoRef and mes = 7 then ValorOrcado end),0),
                      Mes_08 = isnull(SUM(case when Ano = @AnoRef and mes = 8 then ValorOrcado end),0),
                      Mes_09 = isnull(SUM(case when Ano = @AnoRef and mes = 9 then ValorOrcado end),0),
                      Mes_10 = isnull(SUM(case when Ano = @AnoRef and mes =10 then ValorOrcado end),0),
                      Mes_11 = isnull(SUM(case when Ano = @AnoRef and mes =11 then ValorOrcado end),0),
                      Mes_12 = isnull(SUM(case when Ano = @AnoRef and mes =12 then ValorOrcado end),0),  
                      AnoPosterior = isnull(SUM(case when Ano > @AnoRef then ValorOrcado end),0)
                 FROM {0}.{1}.f_GetOrcamentoProjetoComCR(@CodigoProjetoParam,year(GETDATE()))
                GROUP BY DespesaReceita
          UNION
               SELECT DespesaReceita = (case when DespesaReceita = 'D' then 'Despesa' else 'Receita' end),
                      'Realizado' as TipoValor, 
                      AnoAnterior = isnull(SUM(case when Ano < @AnoRef then ValorReal end),0),
                      Mes_01 = isnull(SUM(case when Ano = @AnoRef and mes = 1 then ValorReal end),0),
                      Mes_02 = isnull(SUM(case when Ano = @AnoRef and mes = 2 then ValorReal end),0),
                      Mes_03 = isnull(SUM(case when Ano = @AnoRef and mes = 3 then ValorReal end),0),
                      Mes_04 = isnull(SUM(case when Ano = @AnoRef and mes = 4 then ValorReal end),0),
                      Mes_05 = isnull(SUM(case when Ano = @AnoRef and mes = 5 then ValorReal end),0),
                      Mes_06 = isnull(SUM(case when Ano = @AnoRef and mes = 6 then ValorReal end),0),
                      Mes_07 = isnull(SUM(case when Ano = @AnoRef and mes = 7 then ValorReal end),0),
                      Mes_08 = isnull(SUM(case when Ano = @AnoRef and mes = 8 then ValorReal end),0),
                      Mes_09 = isnull(SUM(case when Ano = @AnoRef and mes = 9 then ValorReal end),0),
                      Mes_10 = isnull(SUM(case when Ano = @AnoRef and mes =10 then ValorReal end),0),
                      Mes_11 = isnull(SUM(case when Ano = @AnoRef and mes =11 then ValorReal end),0),
                      Mes_12 = isnull(SUM(case when Ano = @AnoRef and mes =12 then ValorReal end),0),
                      AnoPosterior = isnull(SUM(case when Ano > @AnoRef then ValorReal end),0)
                 FROM {0}.{1}.f_GetOrcamentoProjetoComCR(@CodigoProjetoParam,year(GETDATE()))
                GROUP BY DespesaReceita
                ORDER BY DespesaReceita, TipoValor", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, anoReferencia);

        return cDados.getDataSet(comandoSQL);
    }

    protected void gvDados_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == "TipoValor")
        {
            if (e.Value.ToString() == "Orcado")
                e.DisplayText = "Valor Orçado";
            else 
                e.DisplayText = "Valor Realizado";
        }
    }

    protected void gvDados_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
    {
        if (e.Column.Name == "TotalAno")
        {
            decimal Janeiro = (decimal)e.GetListSourceFieldValue("Mes_01");
            decimal Fevereiro = (decimal)e.GetListSourceFieldValue("Mes_02");
            decimal Marco = (decimal)e.GetListSourceFieldValue("Mes_03");
            decimal Abril = (decimal)e.GetListSourceFieldValue("Mes_04");
            decimal Maio = (decimal)e.GetListSourceFieldValue("Mes_05");
            decimal Junho = (decimal)e.GetListSourceFieldValue("Mes_06");
            decimal Julho = (decimal)e.GetListSourceFieldValue("Mes_07");
            decimal Agosto = (decimal)e.GetListSourceFieldValue("Mes_08");
            decimal Setembro = (decimal)e.GetListSourceFieldValue("Mes_09");
            decimal Outubro = (decimal)e.GetListSourceFieldValue("Mes_10");
            decimal Novembro = (decimal)e.GetListSourceFieldValue("Mes_11");
            decimal Dezembro = (decimal)e.GetListSourceFieldValue("Mes_12");
            e.Value = Janeiro + Fevereiro + Marco + Abril + Maio + Junho + Julho + Agosto + Setembro + Outubro + Novembro + Dezembro;
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        string Mes = e.DataColumn.FieldName;
        if (Mes.Length < 4 || Mes.Substring(0, 4) != "Mes_")
            return;

        int mes = int.Parse(Mes.Substring(4));

        DataRowView linhaGrid = (DataRowView)gvDados.GetRow(e.VisibleIndex);
        string tipo = linhaGrid["TipoValor"].ToString();
        string despesaReceita = linhaGrid["DespesaReceita"].ToString();

        // parametro -> Projeto;Mes;Tipo;despesaReceita;texto
        string texto = char.ToUpper(despesaReceita[0]) + despesaReceita.Substring(1).ToLower() + " " + (tipo.ToUpper()[0] == 'O' ? "orçada" : "realizada") + " para " + e.DataColumn.EditFormCaption;
        string parametro = Request.QueryString["idProjeto"].ToString() + ";" + mes + ";" + tipo + ";" + despesaReceita + ";" + texto;
        e.Cell.Attributes.Add("onclick", "ExibeDetalheValorMes('" + parametro + "')");
        e.Cell.ForeColor = Color.Blue;
        e.Cell.Font.Underline = true;
        e.Cell.Style.Add("cursor", "pointer");
    }

    private string getMesPorExtenso(int mes)
    {
        string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(mes).ToLower();
        // retorna o nome do mês com a primeira letra em maiuscula
        return char.ToUpper(mesExtenso[0]) + mesExtenso.Substring(1);
    }

    protected void gvDetalhesMes_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        populaGridDetalhesMes();
    }


    protected void gvExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }
}