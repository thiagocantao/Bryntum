/*
 09/12//2010: Mudança by Alejandro: 
            Foi implementado o filtro de mapa, so da entidade logada.
            Criação do método: private void carregaComboMapas()
 11/01/2011: Mudança by Alejandro:
            Agregar un radioButton con as opções de 'Mapa' ou 'Dado', no momento de popular o relatorio.
            Criação do método:  private void carregaComboDados(){...}
            Mudo o método:      private void populaGrid(){...}
 */
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
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Web.Hosting;

public partial class _Processos_Visualizacao_RelatoriosURL_Estrategia_url_relAnaliseDados : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;
    private string dbName;
    private string dbOwner;
    public string alturaTabela = "";
    public string larguraTabela = ""; 
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeLogada;
    System.Globalization.CultureInfo wCultureInfo;
    private List<string> Meses = new List<string>();
    
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeLogada, codigoEntidadeLogada, "null", "EN", 0, "null", "EN_EstRelDad");
        }

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        setMeses();
        defineAlturaTela();
        // variável usada para formatar dinamicamente os valores a serem mostrados na grid.
        wCultureInfo = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID);

        if (!Page.IsPostBack)
        {
            cDados.aplicaEstiloVisual(this);
            

            if (!hfGeral.Contains("tipoArquivo"))
                hfGeral.Set("tipoArquivo", "XLS");

            carregaComboMapas();
            carregaComboDados();

            if (ddlMapa.Items.Count > 0)
                if (cDados.getInfoSistema("CodigoMapa") != null)
                    ddlMapa.Value = cDados.getInfoSistema("CodigoMapa").ToString();

            //if (!IsPostBack)
            //{
            //    cDados.excluiNiveisAbaixo(1);
            //    cDados.insereNivel(1, this);
            //    Master.geraRastroSite();
            //}
        }

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        

        //if (!Page.IsPostBack)
            populaGrid();

        ddlMapa.ClientVisible = rbOpcao.Value.ToString().Equals("M");
        ddlDado.ClientVisible = rbOpcao.Value.ToString().Equals("D");

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    private void setMeses()
    {
        Meses.Clear();
        Meses.Add("Jan");
        Meses.Add("Fev");
        Meses.Add("Mar");
        Meses.Add("Abr");
        Meses.Add("Mai");
        Meses.Add("Jun");
        Meses.Add("Jul");
        Meses.Add("Ago");
        Meses.Add("Set");
        Meses.Add("Out");
        Meses.Add("Nov");
        Meses.Add("Dez");
    }
    
    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 270) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 200) + "px";

        Div2.Style.Add("width", "100%");
        Div2.Style.Add("height", alturaTabela);
    }

    private void carregaComboMapas()
    {
        //DataSet dsMapas = cDados.getMapasUsuarioEntidade(cDados.getInfoSistema("CodigoEntidade").ToString(), codigoUsuarioResponsavel, "");
        string where = " AND un.CodigoEntidade = " + codigoEntidadeLogada.ToString();
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidadeLogada.ToString(), codigoUsuarioResponsavel, where);
        
        if (cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();
        }

        if (!IsPostBack && ddlMapa.Items.Count > 0)
        {
            ddlMapa.SelectedIndex = 0;
        }
    }

    private void carregaComboDados()
    {
        //DataSet dsMapas = cDados.getMapasUsuarioEntidade(cDados.getInfoSistema("CodigoEntidade").ToString(), codigoUsuarioResponsavel, "");
        DataSet dsDados = cDados.getDadosUsuarioEntidade(codigoEntidadeLogada.ToString(), codigoUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsDados))
        {
            ddlDado.DataSource = dsDados;
            ddlDado.TextField = "Dado";
            ddlDado.ValueField = "CodigoDado";
            ddlDado.DataBind();
        }

        if (!IsPostBack && ddlDado.Items.Count > 0)
        {
            ddlDado.SelectedIndex = 0;
        }
    }

    protected void btnSelecionar_Click(object sender, EventArgs e)
    {
        populaGrid();
    }

    private void populaGrid()
    {
        //RadioButtom: Values M:Mapa ou D:Dado.
        if (rbOpcao.Value.ToString().Equals("M"))
        {
            if (ddlMapa.SelectedItem != null)
            {
                int? codMapa = null;
                int? codDado = null;

                if (ddlMapa.SelectedIndex != -1)
                    codMapa = int.Parse(ddlMapa.SelectedItem.Value.ToString());

                DataSet ds = cDados.getRelAnaliseDados(codigoUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codMapa, codDado);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    pvgDadosIndicador.DataSource = ds.Tables[0];
                    pvgDadosIndicador.DataBind();

                    AjustaVisibilidadeTotais(pvgDadosIndicador);  // ajusta visibilidade de totais
                }
            }
        }
        else
        {
            if (ddlDado.SelectedItem != null)
            {
                int? codMapa = null;
                int? codDado = null;

                if (ddlDado.SelectedIndex != -1)
                    codDado = int.Parse(ddlDado.SelectedItem.Value.ToString());

                DataSet ds = cDados.getRelAnaliseDados(codigoUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codMapa, codDado);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    pvgDadosIndicador.DataSource = ds.Tables[0];
                    pvgDadosIndicador.DataBind();

                    AjustaVisibilidadeTotais(pvgDadosIndicador);  // ajusta visibilidade de totais
                }
            }
        }


    }

    #region --- [Pivot Grid]

    protected void pvgDadosIndicador_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        // se estiver ordenando a coluna 'Mes' 
        if (e.Field.Caption == fldMes.Caption)
        {
            object valor1 = Meses.IndexOf(e.Value1.ToString());
            object valor2 = Meses.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        } // if (e.Field == fldMes)
    }

    protected void pvgDadosIndicador_FieldAreaIndexChanged(object sender, PivotFieldEventArgs e)
    {
        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    protected void pvgDadosIndicador_FieldFilterChanged(object sender, PivotFieldEventArgs e)
    {
        // se estiver mudando o filtro do campo 'Dado'
        if ("Dado" == e.Field.FieldName)
            AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    protected void pvgDadosIndicador_FieldVisibleChanged(object sender, PivotFieldEventArgs e)
    {
        // se estiver mudando a visibilidade do campo 'Dado'
        if ("Dado" == e.Field.FieldName)
            e.Field.Visible = true;

        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    protected void pvgDadosIndicador_FieldAreaChanged(object sender, PivotFieldEventArgs e)
    {
        AjustaVisibilidadeTotais((ASPxPivotGrid)sender);
    }

    private void AjustaVisibilidadeTotais(ASPxPivotGrid grid)
    {
        DevExpress.XtraPivotGrid.PivotTotalsVisibility visibilidadeLinha, visibilidadeColuna, visibilidade;
        //bool grandTotalLinha, grandTotalColuna;

        // se o campo dado não estiver visível, nenhum total será permitido
        if (false == fldDado.Visible)
        {
            visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            //grandTotalLinha = false;
            //grandTotalColuna = false;
        }
        else if (1 == fldDado.FilterValues.Count)
        {
            visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
            visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
        }
        else
            switch (fldDado.Area)
            {
                case DevExpress.XtraPivotGrid.PivotArea.FilterArea:
                    // se estiver ná área de filtro, desativa os totais por que certamente estará 
                    // filtro mais de um valor, caso contrário teria entrado no 'else if' acima
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
                    break;
                case DevExpress.XtraPivotGrid.PivotArea.ColumnArea:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    break;
                case DevExpress.XtraPivotGrid.PivotArea.RowArea:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                    break;
                default:
                    visibilidadeLinha = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    visibilidadeColuna = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                    break;

            } // switch (fldDado.Area)

        List<PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
        List<PivotGridField> colFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);

        foreach (PivotGridField campo in rowFields)
        {
            if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals == visibilidadeLinha)
            {   // se a visibilidade Global for custom, é preciso avaliar as posições das colunas

                if (campo.AreaIndex >= fldDado.AreaIndex)
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                else
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            }
            else
                visibilidade = visibilidadeLinha;

            campo.TotalsVisibility = visibilidade;
        } // foreach (PivotGridField campo in campos)

        foreach (PivotGridField campo in colFields)
        {
            if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals == visibilidadeColuna)
            {   // se a visibilidade Global for custom, é preciso avaliar as posições das colunas

                if (campo.AreaIndex >= fldDado.AreaIndex)
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;
                else
                    visibilidade = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            }
            else
                visibilidade = visibilidadeColuna;

            campo.TotalsVisibility = visibilidade;
        } // foreach (PivotGridField campo in campos)

        if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals == visibilidadeLinha)
            grid.OptionsView.ShowRowGrandTotals = true;
        else
            grid.OptionsView.ShowRowGrandTotals = false;

        if (DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals == visibilidadeColuna)
            grid.OptionsView.ShowColumnGrandTotals = true;
        else
            grid.OptionsView.ShowColumnGrandTotals = false;
    }

    protected void pvgDadosIndicador_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        DataTable dt = (DataTable)grid.DataSource;
        string dbFuncao = string.Empty;

        if ((dt != null) && (ds != null))
        {
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                {
                    DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                    dbFuncao = dataRow["Funcao"].ToString().ToUpper();

                    if (dbFuncao.Equals("AVG"))
                        e.CustomValue = e.SummaryValue.Average;
                    else if (dbFuncao.Equals("COUNT"))
                        e.CustomValue = e.SummaryValue.Count;
                    else if (dbFuncao.Equals("MAX"))
                        e.CustomValue = e.SummaryValue.Max;
                    else if (dbFuncao.Equals("MIN"))
                        e.CustomValue = e.SummaryValue.Min;
                    else if (dbFuncao.Equals("SUM"))
                        e.CustomValue = e.SummaryValue.Summary;
                    else if (dbFuncao.Equals("STDDEV"))
                        e.CustomValue = e.SummaryValue.StdDev;
                    else if (dbFuncao.Equals("STDDEVP"))
                        e.CustomValue = e.SummaryValue.StdDevp;
                    else if (dbFuncao.Equals("VAR"))
                        e.CustomValue = e.SummaryValue.Var;
                    else if (dbFuncao.Equals("VARP"))
                        e.CustomValue = e.SummaryValue.Varp;
                    else
                        e.CustomValue = e.SummaryValue.Summary;
                }

                break;  // faz apenas para a primeira linha por que obviamente todos as linhas 
                // irão se referir à mesma função já que todas tratam do mesmo 'Dado'
            }
        }
    }

    protected void pvgDadosIndicador_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
    {
        if ("Valor" == e.DataField.FieldName)
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
    }

    private string formataValorGrid(double valor, string medida, int casasDecimais)
    {
        string numeroFormatado = "";
        wCultureInfo.NumberFormat.CurrencyDecimalDigits = casasDecimais;
        wCultureInfo.NumberFormat.NumberDecimalDigits = casasDecimais;
        wCultureInfo.NumberFormat.PercentDecimalDigits = casasDecimais;

        if (medida.Equals("%"))
        {
            valor /= 100;
            numeroFormatado = valor.ToString("P", wCultureInfo);
        }
        else if (medida.Equals("Nº"))
            numeroFormatado = valor.ToString("N", wCultureInfo);
        else if (medida.Equals("R$"))
            numeroFormatado = valor.ToString("C", wCultureInfo);
        else
        {
            numeroFormatado = valor.ToString("N", wCultureInfo);
            numeroFormatado = medida + " " + numeroFormatado;
        }

        return numeroFormatado;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    nomeArquivo = "analiseDados_" + dataHora + ".html";
                    nomeArquivo = caminhoArquivo + "\\" + nomeArquivo;
                    HtmlExportOptions h = new HtmlExportOptions();
                    h.ExportMode = HtmlExportMode.SingleFile;
                    //ASPxPivotGridExporter1.ExportToHtml(stream, h);
                    ASPxPivotGridExporter1.ExportToHtml(nomeArquivo, h);
                    StartProcess(nomeArquivo);
                    app = "text/html";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "PDF")
                {
                    nomeArquivo = "analiseDados_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "analiseDados_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "analiseDados_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "analiseDados_" + dataHora + ".csv";
                    ASPxPivotGridExporter1.ExportToText(stream, ";");
                    app = "application/CSV";
                }
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML")
                {
                    nomeArquivo = "\"" + nomeArquivo + "\"";
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", app);
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }
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

    public void StartProcess(string path)
    {
        Process process = new Process();
        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();
        }
        catch { }
    }

    #endregion
    
    protected void pnCallbackDados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        populaGrid();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

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
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OLAPANLDADOS", pvgDadosIndicador);
        }
        
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        ASPxMenu menu = (sender as ASPxMenu);

        cDados.setaDefinicoesBotoesExportarLayoutOLAP(menu, true, true, "OLAPANLDADOS", "Análise de Dados", this, pvgDadosIndicador);
    }

    #endregion
}
