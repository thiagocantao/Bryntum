using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_DesembolsoFinanceiro : System.Web.UI.Page
{
    dados cDados;

    int codigoEntidade;
    int idProjeto = 0;
    DataSet ds = new DataSet();
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
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

        cDados.aplicaEstiloVisual(this);

        if (Request.QueryString["IDProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            carregaUheDesembolsoFinanceiro();
        }
        carregaComboAnos();        
        ajustaCabecalhoGrid();
        carregaGrid();
        defineAlturaObjetos();
    }

    private int getTrimestreAtual()
    {
        return Convert.ToInt32(Math.Round(Convert.ToDouble(DateTime.Now.Month) / 3 + 0.25));
    }

    private void defineAlturaObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        gvDados.Settings.VerticalScrollableHeight = altura - 620;
        gvDados.Width = new Unit("100%");
    }

    private void ajustaCabecalhoGrid()
    {
        string nomeColuna = rbValores.Value.ToString() == "P" ? "Prev." : "Real";

        if (rbTrimestres.Value.ToString() == "1")
        {
            gvDados.Columns["1"].Caption = nomeColuna + " Jan (R$)";
            gvDados.Columns["2"].Caption = nomeColuna + " Fev (R$)";
            gvDados.Columns["3"].Caption = nomeColuna + " Mar (R$)";
        }
        else if (rbTrimestres.Value.ToString() == "2")
        {
            gvDados.Columns["1"].Caption = nomeColuna + " Abr (R$)";
            gvDados.Columns["2"].Caption = nomeColuna + " Mai (R$)";
            gvDados.Columns["3"].Caption = nomeColuna + " Jun (R$)";
        }
        else if (rbTrimestres.Value.ToString() == "3")
        {
            gvDados.Columns["1"].Caption = nomeColuna + " Jul (R$)";
            gvDados.Columns["2"].Caption = nomeColuna + " Ago (R$)";
            gvDados.Columns["3"].Caption = nomeColuna + " Set (R$)";
        }
        else if (rbTrimestres.Value.ToString() == "4")
        {
            gvDados.Columns["1"].Caption = nomeColuna + " Out (R$)";
            gvDados.Columns["2"].Caption = nomeColuna + " Nov (R$)";
            gvDados.Columns["3"].Caption = nomeColuna + " Dez (R$)";
        }
    }

    private void carregaUheDesembolsoFinanceiro()
    {
      string  comandoSQL = string.Format(@" 
             BEGIN
	            BEGIN TRAN

	            BEGIN TRY
		            DECLARE @MESES INT
		            DECLARE @CODIGOPROJETO INT

		            SELECT @CODIGOPROJETO = CODIGOPROJETO
		            FROM {0}.{1}.Projeto p
		            WHERE RTRIM(LTRIM(p.CodigoReservado)) = 'UHE_Principal'
                                    AND p.CodigoEntidade = {2}

		            --SELECT @CODIGOPROJETO
		            SET @MESES = 1
   
		            WHILE (@CODIGOPROJETO = {3} AND @MESES < 13)
		            BEGIN
			            INSERT INTO {0}.{1}.uhe_DesembolsoFinanceiro
			            SELECT codigoconta
				            ,YEAR(GETDATE())
				            ,@MESES
				            ,@CODIGOPROJETO
				            ,NULL
				            ,NULL
			            FROM {0}.{1}.uhe_CONTADesembolsoFinanceiro cdf
			            WHERE NOT EXISTS (
					            SELECT 1
					            FROM {0}.{1}.uhe_DesembolsoFinanceiro
					            WHERE CodigoConta = cdf.CodigoConta
						            AND Ano = YEAR(GETDATE())
						            AND Mes = @MESES
						            AND CodigoProjeto = @CODIGOPROJETO
					            )
                          AND cdf.CodigoEntidade = {2}

			            SET @MESES = @MESES + 1
		            END
				    --select * from uhe_DesembolsoFinanceiro where ano = 2015
				    COMMIT
	            END TRY

	            BEGIN CATCH
		            DECLARE @ErrorMessage NVARCHAR(4000)
			            ,@ErrorSeverity INT
			            ,@ErrorState INT
			            ,@ErrorNumber INT;

		            SET @ErrorMessage = ERROR_MESSAGE();
		            SET @ErrorSeverity = ERROR_SEVERITY();
		            SET @ErrorState = ERROR_STATE();
		            SET @ErrorNumber = ERROR_NUMBER();

		            ROLLBACK TRANSACTION

		            RAISERROR (
				            @ErrorMessage
				            ,@ErrorSeverity
				            ,@ErrorState
				            );
	            END CATCH
            END", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, idProjeto);
      int registrosAfetados = 0;
      try
      {
          bool retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);

          if (!retorno)
          {
              throw new Exception("Erro desconhecido");
          }
      }
      catch (Exception ex)
      {
          throw ex;
      }
    }

    private void carregaGrid()
    {
        int ano = ddlAno.SelectedIndex == -1 ? -1 : int.Parse(ddlAno.Value.ToString());

        string comandoSQL = "";

        string colunasValores = "";
        string colunaPrevistoReal = "";

        if (rbValores.Value.ToString() == "P")
            colunaPrevistoReal = "ValorPrevisto";
        else
            colunaPrevistoReal = "ValorRealizado";

        if (rbTrimestres.Value.ToString() == "1")
        {
            colunasValores = string.Format(@"
                SUM(CASE WHEN Mes = 1  THEN {0}  ELSE 0 END) AS Valor1,
                SUM(CASE WHEN Mes = 2  THEN {0}  ELSE 0 END) AS Valor2,
                SUM(CASE WHEN Mes = 3  THEN {0}  ELSE 0 END) AS Valor3", colunaPrevistoReal);
        }
        else if (rbTrimestres.Value.ToString() == "2")
        {
            colunasValores = string.Format(@"
                SUM(CASE WHEN Mes = 4  THEN {0}  ELSE 0 END) AS Valor1,
                SUM(CASE WHEN Mes = 5  THEN {0}  ELSE 0 END) AS Valor2,
                SUM(CASE WHEN Mes = 6  THEN {0}  ELSE 0 END) AS Valor3", colunaPrevistoReal);
        }
        else if (rbTrimestres.Value.ToString() == "3")
        {
            colunasValores = string.Format(@"
                SUM(CASE WHEN Mes = 7  THEN {0}  ELSE 0 END) AS Valor1,
                SUM(CASE WHEN Mes = 8  THEN {0}  ELSE 0 END) AS Valor2,
                SUM(CASE WHEN Mes = 9  THEN {0}  ELSE 0 END) AS Valor3", colunaPrevistoReal);
        }
        else if (rbTrimestres.Value.ToString() == "4")
        {
            colunasValores = string.Format(@"
                SUM(CASE WHEN Mes = 10  THEN {0}  ELSE 0 END) AS Valor1,
                SUM(CASE WHEN Mes = 11  THEN {0}  ELSE 0 END) AS Valor2,
                SUM(CASE WHEN Mes = 12  THEN {0}  ELSE 0 END) AS Valor3", colunaPrevistoReal);
        }


        comandoSQL = string.Format(@"
                    BEGIN
                      DECLARE @CodigoEntidade INT,
                              @CodigoCategoria INT,
                              @Ano INT,
                              @CodigoProjeto INT

                      SET @CodigoEntidade = {0}
                      SET @Ano = {1}
                      SET @CodigoProjeto = {2}

                      SELECT CodigoConta,
                             Tipo,
                             GrupoConta,
                             DescricaoConta,
                             {3}
                        FROM (SELECT cdf.CodigoConta,
                                     cdf.DescricaoConta,
                                     CASE WHEN cdf.IndicaInvestimentoFinanciavel = 'S' THEN 'Investimento Financiável' ELSE 'Investimento Não Financiável' END AS Tipo,
                                     cdf.GrupoConta,
                                     Sum(IsNull(df.ValorPrevisto,0)) AS ValorPrevisto,
                                     Sum(IsNull(df.ValorReal,0)) AS ValorRealizado,
                                     df.Ano,
                                     df.Mes
                                FROM uhe_ContaDesembolsoFinanceiro AS cdf INNER JOIN
                                     uhe_DesembolsoFinanceiro AS df ON (df.CodigoConta = cdf.CodigoConta) INNER JOIN
                                     Projeto AS p ON (p.CodigoProjeto = df.CodigoProjeto)
                              WHERE p.CodigoProjeto = @CodigoProjeto
                                    AND p.CodigoEntidade = @CodigoEntidade
                                    AND p.CodigoEntidade = cdf.CodigoEntidade
                                    AND p.DataExclusao IS NULL
                                    AND df.Ano = @Ano
                              GROUP BY cdf.CodigoConta,
                                       cdf.DescricaoConta,
                                       cdf.IndicaInvestimentoFinanciavel,
                                       cdf.GrupoConta,
                                       df.Ano,
                                       df.Mes) AS tb
                        GROUP BY CodigoConta,
                                 Tipo,
                                 GrupoConta,
                                 DescricaoConta

                END ", codigoEntidade, ano, idProjeto, colunasValores);


        ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds;
        gvDados.DataBind();

        getSomaColuna("Valor1", "spinTotal1");
        getSomaColuna("Valor2", "spinTotal2");
        getSomaColuna("Valor3", "spinTotal3");
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        Font fonte = new Font("Verdana", 9);
        e.BrickStyle.Font = fonte;
    }
    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Group)
            e.Row.BackColor = Color.FromName("#EBEBEB");
    }

    private void carregaComboAnos()
    {
        string where = " AND IndicaAnoPeriodoEditavel = 'S'";
        DataSet dsAnos = cDados.getPeriodoAnalisePortfolio(codigoEntidade, where);

        if (cDados.DataSetOk(dsAnos) && cDados.DataTableOk(dsAnos.Tables[0]))
        {
            ddlAno.DataSource = dsAnos;

            ddlAno.TextField = "Ano";

            ddlAno.ValueField = "Ano";

            ddlAno.DataBind();

            if (!IsPostBack)
            {
                if (ddlAno.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
                {
                    ddlAno.Value = DateTime.Now.Year.ToString();
                    rbTrimestres.Value = getTrimestreAtual().ToString();
                }
                else
                    ddlAno.SelectedIndex = 0;
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Index > 3)
        {
            e.Cell.BackColor = Color.FromName("#E1EAFF");
            ASPxSpinEdit spin = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txt" + e.DataColumn.Name) as ASPxSpinEdit;
            int anoSelecionado = ddlAno.SelectedIndex == -1 ? -1 : int.Parse(ddlAno.Value.ToString());
            int trimestreSelecionado = int.Parse(rbTrimestres.Value.ToString());
            int trimestreAtual = getTrimestreAtual();
            int mesBaseAtual = 0;
            int anoAtual = DateTime.Now.Year;
            int mesColunaAtual = int.Parse(e.DataColumn.Name);

            switch (DateTime.Now.Month)
            {
                case 1: mesBaseAtual = 1;
                    break;
                case 2: mesBaseAtual = 2;
                    break;
                case 3: mesBaseAtual = 3;
                    break;
                case 4: mesBaseAtual = 1;
                    break;
                case 5: mesBaseAtual = 2;
                    break;
                case 6: mesBaseAtual = 3;
                    break;
                case 7: mesBaseAtual = 1;
                    break;
                case 8: mesBaseAtual = 2;
                    break;
                case 9: mesBaseAtual = 3;
                    break;
                case 10: mesBaseAtual = 1;
                    break;
                case 11: mesBaseAtual = 2;
                    break;
                case 12: mesBaseAtual = 3;
                    break;
                default: mesBaseAtual = 0;
                    break;
            }

            if (rbValores.Value + "" == "R")
            {
                if (anoAtual < anoSelecionado)
                {
                    spin.ClientEnabled = false;
                    e.Cell.BackColor = Color.FromName("#EBEBEB");
                }
                else if (anoAtual == anoSelecionado)
                {
                    if (trimestreAtual < trimestreSelecionado)
                    {
                        spin.ClientEnabled = false;
                        e.Cell.BackColor = Color.FromName("#EBEBEB");
                    }
                    else if (trimestreAtual == trimestreSelecionado)
                    {
                        if (mesBaseAtual < mesColunaAtual)
                        {
                            spin.ClientEnabled = false;
                            e.Cell.BackColor = Color.FromName("#EBEBEB");
                        }
                    }
                }
            }

            spin.ClientInstanceName = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Name; 

            string comandoJS = string.Format(" executaCalculoCampo(s.GetValue(), txt_{1}_{2}, spinTotal{0}); "
                , e.DataColumn.Name
                , e.VisibleIndex
                , e.DataColumn.Name);

            spin.ClientSideEvents.Validation = @"function(s, e) { " + comandoJS + " }";

            spin.ClientSideEvents.GotFocus = @"function(s, e) { valorAtual = s.GetValue(); }";

            spin.ClientSideEvents.LostFocus = @"function(s, e) { valorAtual = 0; }";

            string comandoFocus = string.Format(@"if(e.htmlEvent.keyCode == 38)
                                                      navegaSetas('C');", (e.VisibleIndex - 1));

            comandoFocus += string.Format(@"if(e.htmlEvent.keyCode == 40)
                                                        navegaSetas('B');", (e.VisibleIndex - 1)
                                                                                        , e.VisibleIndex + 1);

            spin.ClientSideEvents.KeyDown = @"function(s, e) { " + comandoFocus + " }";
            spin.ClientSideEvents.KeyPress = @"function(s, e) { if(e.htmlEvent.keyCode == 13)
                                                                    navegaSetas('B'); }";

            string codigoConta = gvDados.GetRowValues(e.VisibleIndex, "CodigoConta") + "";

            spin.ClientSideEvents.ValueChanged = @"function(s, e) { callbackSalvar.PerformCallback('" + e.DataColumn.Name + ";" + codigoConta + ";' + s.GetValue());}"; 
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            int mesCorrespondente = 0;
            int valorVariavel = int.Parse(e.Parameter.Split(';')[0]);
            int codigoConta = e.Parameter.Split(';')[1] != "" ? int.Parse(e.Parameter.Split(';')[1]) : -1;
            string nomeColunaAtualizacao = rbValores.Value.ToString() == "P" ? "ValorPrevisto" : "ValorReal";
            string valor = e.Parameter.Split(';')[2] != "" ? e.Parameter.Split(';')[2].Replace(",", ".") : "NULL";

            if (rbTrimestres.Value.ToString() == "1")
            {
                mesCorrespondente = valorVariavel;
            }
            else if (rbTrimestres.Value.ToString() == "2")
            {
                mesCorrespondente = valorVariavel + 3;
            }
            else if (rbTrimestres.Value.ToString() == "3")
            {
                mesCorrespondente = valorVariavel + 6;
            }
            else if (rbTrimestres.Value.ToString() == "4")
            {
                mesCorrespondente = valorVariavel + 9;
            }

            string comandoUpdate = "";

            comandoUpdate += string.Format(@" 
                IF NOT EXISTS(SELECT 1 FROM {0}.{1}.uhe_DesembolsoFinanceiro WHERE CodigoProjeto = {4} AND Ano = {5} AND Mes = {6} AND CodigoConta = {7})
                    INSERT INTO {0}.{1}.uhe_DesembolsoFinanceiro(CodigoConta, Ano, Mes, CodigoProjeto, {2})
                                                          VALUES({7}, {5}, {6}, {4}, {3})
                ELSE
                    UPDATE {0}.{1}.uhe_DesembolsoFinanceiro SET {2} = {3} WHERE CodigoProjeto = {4} AND Ano = {5} AND Mes = {6} AND CodigoConta = {7}"
                , cDados.getDbName()
                , cDados.getDbOwner()
                , nomeColunaAtualizacao
                , valor
                , idProjeto
                , ddlAno.Value
                , mesCorrespondente
                , codigoConta);

            string msg = "";

            if (comandoUpdate != "")
            {

                int regAf = 0;

                msg = "";

                try
                {
                    cDados.execSQL(comandoUpdate, ref regAf);
                }
                catch (Exception ex)
                {
                    msg = Resources.traducao.DesembolsoFinanceiro_erro_ao_salvar_informa__es___n + ex.Message;
                }
            }

            callbackSalvar.JSProperties["cp_Msg"] = msg;
        }
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "A")
            gvDados.ExpandAll();
    }

    public void getSomaColuna(string nomeColuna, string nomeSpin)
    {
        var summaryItem = gvDados.TotalSummary.First(i => i.Tag == nomeColuna);

        object sumObject = gvDados.GetTotalSummaryValue(summaryItem);

        double valorRetorno = sumObject == null ? 0 : double.Parse(sumObject.ToString());

        gvDados.JSProperties["cp_" + nomeSpin] = valorRetorno.ToString();
    }
}