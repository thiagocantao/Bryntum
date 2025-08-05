//Revisado
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
using DevExpress.Web.ASPxPivotGrid;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Web.Hosting;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnaliseIndicadores : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    int codigoEntidade = 0;

    public string alturaTabela = "";
    public string larguraTabela = "";

    object metaAtual = new object();
    object resultadoAtual = new object();

    public bool exportaOLAPTodosFormatos = false;

    #endregion
    protected void Page_Init(object sender, EventArgs e)
    {

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        verificaRedirecionamentoUnigest();
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
    }

    private void verificaRedirecionamentoUnigest()
    {
        string IndicaTelaRiscoUNIGEST = "N";
        DataSet ds = cDados.getParametrosSistema("IndicaTelaRiscoUNIGEST");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            IndicaTelaRiscoUNIGEST = ds.Tables[0].Rows[0]["IndicaTelaRiscoUNIGEST"].ToString().Trim();
        }
        if (IndicaTelaRiscoUNIGEST == "S")
        {
            Response.Redirect("~/_Processos/Visualizacao/RelatoriosURL/Projetos/url_relAnaliseIndicadoresNovo.aspx?" + Request.QueryString.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_PrjRelIndPrj");
        }


        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        populaGrid();
        defineAlturaTela();

        //if (!IsPostBack)
        //{
        //    cDados.excluiNiveisAbaixo(1);
        //    cDados.insereNivel(1, this);
        //    Master.geraRastroSite();
        //}
        this.Title = cDados.getNomeSistema();
        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        //alturaTabela = (alturaPrincipal - 350) + "px";//a div vai ficar com essa altura
        //larguraTabela = (larguraPrincipal - 410) + "px";

        alturaTabela = (alturaPrincipal - 220) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 200) + "px";

        divPivotGrid.Style.Add("height", alturaTabela);
        divPivotGrid.Style.Add("overflow", "auto");
        divPivotGrid.Style.Add("width", "100%");

        //pvgDadosIndicador.Height = new Unit(alturaTabela);



    }

    private void populaGrid()
    {
        string comandoSQL = string.Format(@"
            SELECT
                  [MetaDescritiva]
		        , [Indicador]			
		        , [Projeto]				
	            , [StatusProjeto]
	            , [Unidade]
	            , [GerenteProjeto]
		        , [Programa]				
		        , [Ano]					
		        , [Periodo]				
		        , [Meta]				
		        , [Resultado]			
		        , [Desempenho]			
		        , [MetaAcumulada]		
		        , [ResultadoAcumulado]	
		        , [DesempenhoAcumulado]	
		        , [Medida]				
		        , [Decimais]			
		        , [DataReferencia]		
		        , [Periodicidade]		
		        , [MetaRefAno]			
		        , [ResultadoRefAno]		
		        , [DesempenhoRefAno]	
		        , [MetaRefIndicador]	
		        , [ResultadoRefIndicador]
		        , [DesempenhoRefIndicador]
                , [PeriodoNum]
            FROM {0}.{1}.f_GetDadosOLAP_IndicadorOperacionalProjetos( {2}, {3}, {4})
        ", dbName, dbOwner, codigoUsuarioResponsavel, codigoEntidade, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            pvgDadosIndicador.DataSource = ds.Tables[0];
            pvgDadosIndicador.DataBind();
        }
    }

    #region --- [Pivot Grid]

    protected void pvgDadosIndicador_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        ASPxPivotGrid grid = (ASPxPivotGrid)sender;

        // se estiver ordenando a coluna 'Periodo' 
        if (e.Field == grid.Fields["Periodo"])
        {
            if ((e.ListSourceRowIndex1 >= 0) && (e.ListSourceRowIndex2 >= 0))
            {
                int periodo1 = Convert.ToInt32(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "PeriodoNum"));
                int periodo2 = Convert.ToInt32(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "PeriodoNum"));
                e.Result = periodo1.CompareTo(periodo2);
                e.Handled = true;
            }

        } // if (e.Field.FieldName == "PeriodoNum") 
    }

    protected void pvgDadosIndicador_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
    {
        if (("Resultado" == e.DataField.FieldName) || ("ResultadoAcumulado" == e.DataField.FieldName) ||
            ("Meta" == e.DataField.FieldName) || ("MetaAcumulada" == e.DataField.FieldName))
        {
            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            ASPxPivotGrid grid = (ASPxPivotGrid)sender;
            DataTable dt = (DataTable)grid.DataSource;
            int Decimais = 0;
            double valor = 0;

            if ((dt != null) && (ds != null))
            {
                foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                {
                    if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                    {
                        DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];

                        int.TryParse(dataRow["Decimais"].ToString(), out Decimais);

                        if (null != e.Value)
                            double.TryParse(e.Value.ToString(), out valor);

                        e.DisplayText = formataValorGrid(valor, dataRow["Medida"].ToString(), Decimais);
                    }
                    break;
                }
            }
        }
        else if (e.DataField.FieldName.Equals("Desempenho") || e.DataField.FieldName.Equals("DesempenhoAcumulado"))
        {
            //if (e.Value != null)
            //    e.DisplayText = "<img alt='' src='../../imagens/" + e.Value.ToString() + "Menor.gif' />";
        }
        else if (e.DataField.FieldName.Equals("PeriodoNum"))
        {

        }
    }

    private string formataValorGrid(double valor, string medida, int casasDecimais)
    {
        string numeroFormatado = "";
        System.Globalization.CultureInfo ci = new
            System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID);
        ci.NumberFormat.CurrencyDecimalDigits = casasDecimais;
        ci.NumberFormat.NumberDecimalDigits = casasDecimais;
        ci.NumberFormat.PercentDecimalDigits = casasDecimais;

        if (medida.Equals("%"))
        {
            valor /= 100;
            numeroFormatado = valor.ToString("P", ci);
        }
        else if (medida.Equals("Nº"))
            numeroFormatado = valor.ToString("N", ci);
        else if (medida.Equals("R$"))
            numeroFormatado = valor.ToString("C", ci);
        else
            numeroFormatado = valor.ToString();

        return numeroFormatado;
    }



    protected void pvgDadosIndicador_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
    {
        bool periodoNaTela = false, anoNaTela = false;

        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        List<PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
        List<PivotGridField> colFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);

        periodoNaTela = (rowFields.Contains(grid.Fields["Periodo"]) || colFields.Contains(grid.Fields["Periodo"]));

        // se o período estiver na tela, o total é qualquer valor 'summary', uma vez 
        // que período é a menor unidade
        if (periodoNaTela)
            e.CustomValue = e.SummaryValue.Max;
        else
        {
            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            DataTable dt = (DataTable)grid.DataSource;

            if ((dt != null) && (ds != null))
            {
                string campoAObter;
                anoNaTela = (rowFields.Contains(grid.Fields["Ano"]) || colFields.Contains(grid.Fields["Ano"]));

                // se o ano estiver na tela, vai pegar o valor de referência para o ano 
                // (a última linha do ano tenha resultado
                if (anoNaTela)
                {
                    if (e.FieldName.Equals("Meta") || e.FieldName.Equals("MetaAcumulada"))
                        campoAObter = "MetaRefAno";
                    else if (e.FieldName.Equals("Desempenho") || e.FieldName.Equals("DesempenhoAcumulado"))
                        campoAObter = "DesempenhoRefAno";
                    else
                        campoAObter = "ResultadoRefAno";
                } // if (anoNaTela
                else
                {
                    // se nem o período nem o ano estiverem na tela, busca o valor de referência para o indicador/projeto
                    // (última linha para o indicador/projeto com resultado.
                    if (e.FieldName.Equals("Meta"))
                        campoAObter = "MetaRefIndicador";
                    else if (e.FieldName.Equals("MetaAcumulada"))
                        campoAObter = "MetaRefIndicador";
                    else if (e.FieldName.Equals("Resultado"))
                        campoAObter = "ResultadoRefIndicador";
                    else if (e.FieldName.Equals("ResultadoAcumulado"))
                        campoAObter = "ResultadoRefIndicador";
                    else if (e.FieldName.Equals("Desempenho"))
                        campoAObter = "DesempenhoRefIndicador";
                    else
                        campoAObter = "DesempenhoRefIndicador";
                }
                foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                {
                    if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                    {
                        DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                        e.CustomValue = dataRow[campoAObter];
                        break; // busca uma linha já que será igual para todas as linhas, uma vez que unidade está presente
                    } // if ((summaryRow.ListSourceRowIndex >= 0) && ...
                } // foreach 
            }// if ((dt != null) && ...
        } // else (periodoNaTela)
    }

    protected void pvgDadosIndicador_FieldVisibleChanged(object sender, PivotFieldEventArgs e)
    {
        // não permite esconder o campo MetaDescritiva
        if (e.Field.FieldName.Equals("MetaDescritiva"))
            e.Field.Visible = true;

    }

    #endregion

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName.Contains("Desempenho") && (e.Value != null))
            {
                e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                if (e.Value.ToString().Equals("Vermelho"))
                {
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Equals("Amarelo"))
                {
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Equals("Verde"))
                {
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (e.Value.ToString().Equals("Azul"))
                {
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.WhiteSmoke;
                }
                else if (e.Value.ToString().Equals("Laranja"))
                {
                    e.Brick.Text = "l";
                    e.Brick.TextValue = "l";
                    e.Appearance.ForeColor = Color.Orange;
                }
                else
                {
                    e.Brick.Text = " ";
                    e.Brick.Value = " ";
                }
            }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapAnlIndPrj", pvgDadosIndicador);
        }        
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapAnlIndPrj", "OLAP Indicadores de Projetos", this, pvgDadosIndicador);
    }

    #endregion

    protected void pvgDadosIndicador_HtmlCellPrepared(object sender, PivotHtmlCellPreparedEventArgs e)
    {
        if (e.DataField == null) return;

        var fieldName = e.DataField.FieldName;
        if (fieldName.Equals("Desempenho") || fieldName.Equals("DesempenhoAcumulado"))
        {
            string value = e.Value as string;
            if (!(string.IsNullOrWhiteSpace(value)))
                e.Cell.Text = string.Format("<img alt='' src='../../../../imagens/{0}Menor.gif' />", value);
        }
    }

    protected void pvgDadosIndicador_HtmlFieldValuePrepared(object sender, PivotHtmlFieldValuePreparedEventArgs e)
    {
        if (e.Field == null) return;

        var fieldName = e.Field.FieldName;
        if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value &&
            (fieldName.Equals("Desempenho") || fieldName.Equals("DesempenhoAcumulado")))
        {
            string value = e.Value as string;
            if (!(string.IsNullOrWhiteSpace(value)))
                e.Cell.Text = string.Format("<img alt='' src='../../../../imagens/{0}Menor.gif' />", value);
        }
    }
}
