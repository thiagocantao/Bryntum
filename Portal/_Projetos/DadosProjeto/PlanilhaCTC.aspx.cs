using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Drawing;
using DevExpress.Data;
using DevExpress.XtraPrinting;
using System.IO;

public partial class _Projetos_DadosProjeto_PlanilhaCTC : System.Web.UI.Page
{
    dados cDados;
    DataTable dtAux = new DataTable();
    DataTable dtAlteracoes = new DataTable();
    private int codigoProjeto = -1;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    bool podeEditar = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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

        headerOnTela();

        if (Request.QueryString["IDProjeto"] != null)
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        gvDados.JSProperties["cp_CodigoProjeto"] = codigoProjeto;

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PC", 0, "null", "PC_CnsCTC");
        }

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PC", 0, "null", "PC_AltCTC");

        cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeEditar, ref podeEditar, ref podeEditar);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        carregaComboPrevisoes();

        carregaGvDados();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        string codigoMestre = hfCodigoMestre.Contains("Valor") ? hfCodigoMestre.Get("Valor").ToString() : "-1";

        if (codigoMestre != "")
        {
            carregaGridAlteracoes(codigoMestre);
        }
    }

    #region GRID

    private void carregaGvDados()
    {
        int codigoPrevisao = ddlPrevisao.SelectedIndex == -1 ? -1 : int.Parse(ddlPrevisao.Value.ToString());
        
        DataSet ds = getPlanilhaCTC(codigoProjeto, codigoPrevisao);

        if (cDados.DataSetOk(ds))
        {
            dtAux = ds.Tables[0];
            gvDados.DataSource = ds;            
            gvDados.DataBind();
        }

        gvDados.TotalSummary.Clear();

        criaSomatoriaColuna("BudgetProjetado", SummaryItemType.Sum);
        criaSomatoriaColuna("CustoIncorrido", SummaryItemType.Sum);
        criaSomatoriaColuna("ValorCTC", SummaryItemType.Sum);
        criaSomatoriaColuna("PrevisaoRevisada", SummaryItemType.Sum);
        criaSomatoriaColuna("Variacao", SummaryItemType.Sum);
        criaSomatoriaColuna("SaldoaContratar", SummaryItemType.Sum);
        criaSomatoriaColuna("AvancoFinanceiro", SummaryItemType.Sum);

        gvDados.Columns["PrevisaoRevisada"].Caption = string.Format("Forecast <br>(Previsão Revisada <br>até {0:MMM/yyyy})", DateTime.Now.AddMonths(-1));
    }

    public DataSet getPlanilhaCTC(int codigoProjeto, int codigoPrevisao)
    {
        DataSet ds;
        string comandoSQL = string.Format(@"SELECT ctc.*, CASE WHEN PrevisaoRevisada = 0 THEN 0
                                                               ELSE CustoIncorrido / PrevisaoRevisada END AS AvancoFinanceiro,
                                                   (SELECT COUNT(1) 
                                                      FROM {0}.{1}.LogPlanilhaCTC log 
                                                     WHERE log.CodigoMestre = ctc.CodigoMestre
                                                       AND log.CodigoProjeto = {3}
                                                       AND log.CodigoPrevisao = {2}
                                                       AND log.SaldoContratualAntes <> log.SaldoContratualDepois) AS alteracoesSaldo,
                                                   (SELECT COUNT(1) 
                                                      FROM {0}.{1}.LogPlanilhaCTC log2
                                                     WHERE log2.CodigoMestre = ctc.CodigoMestre
                                                       AND log2.CodigoProjeto = {3}
                                                       AND log2.CodigoPrevisao = {2}
                                                       AND log2.ValorCTCAntes <> log2.ValorCTCDepois) AS alteracoesCTC
                                              FROM {0}.{1}.f_GetDadosPlanilhaCTC({2}, {3}) ctc
            ", cDados.getDbName(),cDados.getDbOwner(), codigoPrevisao, codigoProjeto);
        ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    private void criaSomatoriaColuna(string nomeColuna, SummaryItemType tipo)
    {
        ASPxSummaryItem sum = new ASPxSummaryItem(nomeColuna, tipo);
        sum.DisplayFormat = "N2";
        sum.ShowInColumn = nomeColuna;
        gvDados.TotalSummary.Add(sum);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int largura1 = 0;
        int altura1 = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura1, out altura1);
        
        if (altura1 > 0)
            gvDados.Settings.VerticalScrollableHeight = altura1 - 330;
    }

    #endregion

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        return codigoDado;
    }

    #endregion

    protected void gvDados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (podeEditar)
        {
            e.Enabled = true;
        }
        else
        {
            e.Enabled = false;
            e.Image.Url = "~/imagens/botoes/editarRegDes.png";
        }
    }

    private void carregaComboPrevisoes()
    {
        DataSet ds = cDados.getPrevisoesOrcamentarias(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds))
        {
            ddlPrevisao.DataSource = ds;
            ddlPrevisao.TextField = "DescricaoPrevisao";
            ddlPrevisao.ValueField = "CodigoPrevisao";

            ddlPrevisao.DataBind();

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow drOficial = ds.Tables[0].Select("IndicaPrevisaoOficial = 'S'")[0];

                ddlPrevisao.Value = drOficial["CodigoPrevisao"].ToString();
            }
        }
    }

    protected void gvDados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CodigoMestre" || e.Column.FieldName == "Descricao")
        {
            ASPxTextBox txt = ((ASPxTextBox)e.Editor);
            txt.DisabledStyle.Border.BorderStyle = BorderStyle.None;
            txt.ClientEnabled = false;
            txt.DisabledStyle.ForeColor = Color.Black;
        }
        else
        {
            if (e.Column.FieldName != "ValorCTC" && e.Column.FieldName != "SaldoaContratar")
            {
                ASPxSpinEdit txt = ((ASPxSpinEdit)e.Editor);

                txt.DisabledStyle.Border.BorderStyle = BorderStyle.None;
                txt.ClientEnabled = false;
                txt.DisabledStyle.ForeColor = Color.Black;
                txt.HorizontalAlign = HorizontalAlign.Right;

                if (e.Column.FieldName != "CodigoMestre" && e.Column.FieldName != "Descricao")
                    txt.HorizontalAlign = HorizontalAlign.Right;

                txt.ClientInstanceName = e.Column.FieldName.ToLower() + "_" + e.VisibleIndex;
            }
            else if (e.Column.FieldName == "ValorCTC")
            {
                ASPxSpinEdit txt = ((ASPxSpinEdit)e.Editor);

                string funcao = string.Format(@"var valorCTC = s.GetText();
                                            var valorAtual = custoincorrido_{0}.GetText();
                                            var projetado = budgetprojetado_{0}.GetText();
                                            var previsao = 0;
                                            var variacao = 0;
                                            var avanco = 0;

                                            if(valorCTC != null && valorCTC != '' && valorAtual != null && valorAtual != '')
                                                previsao = parseFloat(valorCTC.replace(',', '.')) + parseFloat(valorAtual.replace(',', '.'));

                                            if(projetado != null && projetado != '')
                                                variacao = parseFloat(projetado.replace(',', '.')) - previsao;
                                        
                                            if(valorAtual != null && valorAtual != '' && previsao != 0)
                                                avanco = parseFloat(valorAtual.replace(',', '.')) / previsao;

                                            previsaorevisada_{0}.SetText(previsao);
                                            variacao_{0}.SetText(variacao);
                                            avancofinanceiro_{0}.SetText(avanco);
                                            ", e.VisibleIndex);

                txt.ClientSideEvents.ValueChanged = "function(s, e) { " + funcao + " }";
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ValorCTC" || e.DataColumn.FieldName == "SaldoaContratar")
        {
            bool linkAlteracoes = false;
            string codigoMestre = gvDados.GetRowValues(e.VisibleIndex, "CodigoMestre").ToString();            

            if (e.DataColumn.FieldName == "ValorCTC")
            {
                linkAlteracoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "alteracoesCTC").ToString()) > 0;  
            }
            else
            {
                linkAlteracoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "alteracoesSaldo").ToString()) > 0;                
            }

            bool indicaValorAlterado = gvDados.GetRowValues(e.VisibleIndex, "ValorCTCAlterado").ToString() == "1";

            if (linkAlteracoes || (e.DataColumn.FieldName == "ValorCTC" && indicaValorAlterado))
            {
                e.Cell.Style.Add("background-image", "url(../../imagens/alteracao_cell.png);");
                e.Cell.Style.Add("background-repeat", "no-repeat;");
                e.Cell.Style.Add("background-position", "right top;");
            }

            if (linkAlteracoes)
            {
                e.Cell.Style.Add("cursor", "pointer;");

                e.Cell.ToolTip = "Clique para ver o log de alterações";

                e.Cell.Attributes.Add("onclick", "abreAlteracoes('" + codigoMestre + "')");// = string.Format("Valor alterado em {0:dd/MM/yyyy} por {1}", dr[0]["DataUltimaAlteracao"], dr[0]["NomeUsuario"].ToString());
            }
        }
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string msgErro = "";
        string codigoMestre = e.Keys[0].ToString();
        int codigoPrevisao = ddlPrevisao.SelectedIndex == -1 ? -1 : int.Parse(ddlPrevisao.Value.ToString());
        bool indicaValorAlterado = gvDados.GetRowValuesByKeyValue(codigoMestre, "ValorCTCAlterado").ToString() == "1";

        double budgetProjetado = e.NewValues["BudgetProjetado"] != null && e.NewValues["BudgetProjetado"].ToString() != "" ? double.Parse(e.NewValues["BudgetProjetado"].ToString()) : 0;
        double custoIncorrido = e.NewValues["CustoIncorrido"] != null && e.NewValues["CustoIncorrido"].ToString() != "" ? double.Parse(e.NewValues["CustoIncorrido"].ToString()) : 0;
        double novoValorCTC = e.NewValues["ValorCTC"] != null && e.NewValues["ValorCTC"].ToString() != "" ? double.Parse(e.NewValues["ValorCTC"].ToString()) : 0;
        
        string valorCTC = novoValorCTC != (budgetProjetado - custoIncorrido) || indicaValorAlterado ? e.NewValues["ValorCTC"].ToString() : "NULL";
        string valorSaldo = e.NewValues["SaldoaContratar"] != null && e.NewValues["SaldoaContratar"].ToString() != "" ? e.NewValues["SaldoaContratar"].ToString() : "NULL";

        atualizaPlanilhaCTC(codigoProjeto, codigoPrevisao, codigoMestre, valorCTC, valorSaldo, codigoUsuarioResponsavel, ref msgErro);

        if (msgErro != "")
            throw new Exception(msgErro);
        else
        {
            carregaGvDados();
            e.Cancel = true;
            gvDados.CancelEdit();
        }
    }

    public bool atualizaPlanilhaCTC(int codigoProjeto, int codigoPrevisao, string codigoMestre, string valorCTC, string saldoContratar, int usuarioAlteracao, ref string msgError)
    {
        bool retorno = false;
        string comandoSQL = "";
        int registrosAfetados = 0;

        try
        {
            if (valorCTC != "NULL")
                valorCTC = (double.Parse(valorCTC) * 1000).ToString();

            if (saldoContratar != "NULL")
                saldoContratar = (double.Parse(saldoContratar) * 1000).ToString();

            comandoSQL += string.Format(@"                                       
                                       IF EXISTS(SELECT 1 FROM {0}.{1}.PlanilhaCTC 
                                                  WHERE CodigoProjeto = {2} 
                                                    AND CodigoPrevisao = {3} 
                                                    AND CodigoMestre = '{4}')
                                            BEGIN
                                                 UPDATE {0}.{1}.PlanilhaCTC SET ValorCTC = {5},
                                                                                SaldoContratual = {6},
                                                                                UsuarioAlteracao = {7},
                                                                                DataUltimaAlteracao = GetDate()
                                                  WHERE CodigoProjeto = {2} 
                                                    AND CodigoPrevisao = {3} 
                                                    AND CodigoMestre = '{4}'
                                            END
                                        ELSE
                                            BEGIN
                                                INSERT INTO {0}.{1}.PlanilhaCTC
                                                       (CodigoProjeto
                                                       ,CodigoPrevisao
                                                       ,CodigoMestre
                                                       ,ValorCTC
                                                       ,SaldoContratual
                                                       ,UsuarioAlteracao
                                                       ,DataUltimaAlteracao)
                                                 VALUES
                                                       ({2}
                                                       ,{3}
                                                       ,'{4}'
                                                       ,{5}
                                                       ,{6}
                                                       ,{7}
                                                       ,GetDate())
                                                END
                                    
                  ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoPrevisao, codigoMestre, valorCTC.Replace(',', '.'), saldoContratar.Replace(',', '.'), usuarioAlteracao);

            cDados.execSQL(comandoSQL, ref registrosAfetados);
            retorno = true;
        }
        catch (Exception ex)
        {
            msgError = ex.Message + Environment.NewLine + comandoSQL;
            retorno = false;
        }
        return retorno;
    }


    protected void gvDados_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if (e.Item.FieldName == "AvancoFinanceiro" && cDados.DataTableOk(dtAux))
        {
            double sumCusto = double.Parse(dtAux.Compute("Sum(CustoIncorrido)", "").ToString());
            double sumPrevisao = double.Parse(dtAux.Compute("Sum(PrevisaoRevisada)", "").ToString());

            if (sumPrevisao != 0)
            {
                e.Text = string.Format("{0:p2}", (sumCusto/sumPrevisao));
            }
        }
    }

    private void carregaGridAlteracoes(string codigoMestre)
    {
        int codigoPrevisao = ddlPrevisao.SelectedIndex == -1 ? -1 : int.Parse(ddlPrevisao.Value.ToString());
        DataSet ds = getAlteracoesPlanilhaCTC(codigoProjeto, codigoPrevisao, codigoMestre);

        if (cDados.DataSetOk(ds))
        {
            gvAlteracoes.DataSource = ds;
            gvAlteracoes.DataBind();
        }
    }

    public DataSet getAlteracoesPlanilhaCTC(int codigoProjeto, int codigoPrevisao, string codigoMestre)
    {
        DataSet ds;
        string comandoSQL = string.Format(@"SELECT ctc.[idLogAlteracao]
                                                  ,ctc.[ValorCTCAntes] / 1000 AS ValorCTCAntes
                                                  ,ctc.[ValorCTCDepois] / 1000 AS ValorCTCDepois
                                                  ,ctc.[SaldoContratualAntes] / 1000 AS SaldoContratualAntes
                                                  ,ctc.[SaldoContratualDepois] / 1000 AS SaldoContratualDepois
                                                  ,ctc.[DataAlteracao]
                                                  ,u.[NomeUsuario]
                                              FROM {0}.{1}.LogPlanilhaCTC ctc INNER JOIN
                                                   {0}.{1}.Usuario u ON u.CodigoUsuario = ctc.UsuarioAlteracao
                                             WHERE CodigoProjeto = {2} 
                                               AND CodigoPrevisao = {3} 
                                               AND ctc.CodigoMestre = '{4}'
                                             ORDER BY ctc.[DataAlteracao] DESC
            ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoPrevisao, codigoMestre);
        ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        carregaGvDados();

        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            try
            {
                nomeArquivo = "Contratos_" + dataHora + ".xls";
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

    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {

        e.Text = e.Text.Replace("<br>", "");
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

