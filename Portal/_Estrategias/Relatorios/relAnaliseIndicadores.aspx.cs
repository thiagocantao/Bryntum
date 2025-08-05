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
using DevExpress.Export;

public partial class _relAnaliseIndicadores : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    int codigoEntidade;

    public string alturaTabela = "";
    public string larguraTabela = "";

    object metaAtual = new object();
    object resultadoAtual = new object();
    bool exportaOLAPTodosFormatos = false;

    System.Globalization.CultureInfo wCultureInfo;
    private List<string> Meses = new List<string>();

    #endregion

    protected void Page_Init(object sender, EventArgs e)
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_EstRelInd");
        }

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        setMeses();

        if (!(IsPostBack || IsCallback))
        {
            rbOpcao.Value = "U";
        }

        if (!IsCallback)
        {
            ajustaTelaSelecaoFiltro();
        }

        // variável usada para formatar dinamicamente os valores a serem mostrados na grid.
        wCultureInfo = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID);
        carregaFiltros();
   
        if (!IsPostBack)
        {
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");

            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            
            populaOpcoesExportacao();

            if (!hfGeral.Contains("tipoArquivo"))
                hfGeral.Set("tipoArquivo", "XLS");
        }

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        populaGrid(IsPostBack);
        defineAlturaTela();
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
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

    private void ajustaTelaSelecaoFiltro()
    {
        if (rbOpcao.Value.ToString() == "U")
        {
            ddlIndicador.ClientVisible = false;
            ddlUnidades.ClientVisible = true;
            fldIndicador.AreaIndex = 1;
            fldUnidade.AreaIndex = 0;
        }
        else
        {
            ddlIndicador.ClientVisible = true;
            ddlUnidades.ClientVisible = false;
            fldUnidade.AreaIndex = 1;
            fldIndicador.AreaIndex = 0;
        }
    }

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();

        ListEditItem liExcel = new ListEditItem("XLS", "XLS");
        liExcel.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";


        ddlExporta.Items.Add(liExcel);
        ddlExporta.ClientEnabled = false;
        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;
            //ddlExporta.Items.Add(new ListEditItem("XLS", "XLS"));

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);


            ListEditItem liHTML = new ListEditItem("HTML", "HTML");
            liHTML.ImageUrl = "~/imagens/menuExportacao/iconoHtml.png";
            ddlExporta.Items.Add(liHTML);

            ListEditItem liRTF = new ListEditItem("RTF", "RTF");
            liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
            ddlExporta.Items.Add(liRTF);

            ListEditItem liCSV = new ListEditItem("CSV", "CSV");
            liCSV.ImageUrl = "~/imagens/menuExportacao/iconoCSV.png";
            ddlExporta.Items.Add(liCSV);

        }
        ddlExporta.SelectedIndex = 0;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        Div2.Style.Add("height", (alturaPrincipal - 250) + "px");//a div vai ficar com essa altura
        Div2.Style.Add("width", (larguraPrincipal - 10) + "px");

    }
        
    private void populaGrid(bool lerXML)
    {
        DataSet ds = new DataSet();

        //fldMeta.Options.ShowValues = !fldMes.Visible; foi corrigido um bug workitem Bug 2005: [PBH] - Demandas MAR_2018 - Desconfiguração do relatório Análise de Indicadores da Estratégia

        if (!lerXML)//se não for postback então escreve xml
        {
            string nomeArquivo = "OLAP_IND_" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel + ".xml";

            int? codIndic = null;
            int? codUnid = null;

            if (ddlIndicador.SelectedIndex != -1 && rbOpcao.Value.ToString() == "I")
            {
                 codIndic = int.Parse(ddlIndicador.Value.ToString());
            }

            if (ddlUnidades.SelectedIndex != -1 && rbOpcao.Value.ToString() == "U")
            {
                codUnid = int.Parse(ddlUnidades.Value.ToString());                
            }

            ds = cDados.getDadosOLAP_AnaliseIndicador(codigoUsuarioResponsavel, codIndic, codUnid, codigoEntidade);

            StreamWriter strWriter;

            hfGeral.Set("NomeArquivoXML", nomeArquivo);

            strWriter = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + nomeArquivo, false, System.Text.Encoding.UTF8);
            strWriter.Close();

            ds.WriteXml(HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + hfGeral.Get("NomeArquivoXML"));
           
        }
        else
        {
            ds.ReadXml(HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + hfGeral.Get("NomeArquivoXML"));

        }

        if (cDados.DataSetOk(ds))
        {
            if (checkAcumulado.Checked)
            {
                fldMeta.FieldName = "MetaAcumulada";
                fldResultado.FieldName = "ResultadoAcumulado";
                fldDesempenho.FieldName = "DesempenhoAcumulado";
                fldMeta.Caption = "Meta Acumulada";
                fldResultado.Caption = "Resultado Acumulado";
                fldDesempenho.Caption = "Desempenho Acumulado";
                //fldMeta.Options.ShowValues = true;
            }
            else
            {
                fldMeta.FieldName = "Meta";
                fldResultado.FieldName = "Resultado";
                fldDesempenho.FieldName = "Desempenho";
                fldMeta.Caption = "Meta";
                fldResultado.Caption = "Resultado";
                fldDesempenho.Caption = "Desempenho";
            }
        }
        pvgDadosIndicador.DataSource = ds.Tables.Count > 0 ? ds.Tables[0] : null;
        pvgDadosIndicador.DataBind();
    }

    #region --- [Pivot Grid]

    protected void pvgDadosIndicador_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        // se estiver ordenando a coluna 'Mes' 
        if (e.Field == fldMes)
        {
            object valor1 = Meses.IndexOf(e.Value1.ToString());
            object valor2 = Meses.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        } // if (e.Field == fldMes)
    }

    protected void pvgDadosIndicador_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
    {
        if ((e.DataField == fldResultado) || (e.DataField == fldMeta))
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

                        if (null == e.Value)
                            e.DisplayText = "";
                        else
                        {
                            double.TryParse(e.Value.ToString(), out valor);
                            e.DisplayText = formataValorGrid(valor, dataRow["Medida"].ToString(), Decimais);
                        }
                    }
                    break;
                }
            }
        }
        else if (e.DataField == fldDesempenho)
        {
            if (e.Value != null)
                e.DisplayText = "<img alt='' src='../../imagens/" + e.Value.ToString() + "Menor.gif' />";
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
        populaGrid(true);
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    nomeArquivo = "analiseIndicador_" + dataHora + ".html";
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
                    nomeArquivo = "analiseIndicador_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = Resources.traducao.relAnaliseIndicadores_cdis_inform_tica;
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "analiseIndicador_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Value;
                    x.ExportType = ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "analiseIndicador_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "analiseIndicador_" + dataHora + ".csv";
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
                                    window.top.mostraMensagem(traducao.relAnaliseIndicadores_erro_ao_exportar_os_dados__verifique_se_n_o_foi_ultrapassado_o_n_mero_m_ximo_de_256_colunas_, 'erro', true, false, null);                                 
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


    protected void pvgDadosIndicador_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
    {
        string campoAObter;

        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        List<PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
        List<PivotGridField> colFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);

        DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
        DataTable dt = (DataTable)grid.DataSource;
        
        // se a soma que estiver sendo feita for para a COLUNA [Ano], então o valor a mostrar 
        // como total será o campo já calculado para o ano
        if (e.ColumnField == fldAno)
        {
            if (e.DataField  == fldMeta)
                campoAObter = "MetaRefAno";
            else if (e.DataField == fldDesempenho)
                campoAObter = "DesempenhoRefAno";
            else
                campoAObter = "ResultadoRefAno";

            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                {
                    DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                    if (campoAObter == "DesempenhoRefAno")
                    {
                        e.CustomValue = dataRow[campoAObter];
                    }
                    else
                    {
                        try
                        {
                            if (dataRow[campoAObter] + "" != "")
                                e.CustomValue = dataRow[campoAObter].ToString().Replace('.', ',');
                        }
                        catch { }
                    }
                    break; // busca uma linha já que será igual para todas as linhas, uma vez que unidade está presente
                } // if ((summaryRow.ListSourceRowIndex >= 0) && ...
            } // foreach summaryRow in ds
        }// if (e.ColumnField == fldAno) 
        else
        {
            if(e.SummaryValue.Max != null)
                e.CustomValue = e.SummaryValue.Max.ToString().Replace('.', ',');
        }// else (e.ColumnField == fldAno) 
    }

    protected void pvgDadosIndicador_FieldVisibleChanged(object sender, PivotFieldEventArgs e)
    {
        if (e.Field == fldAno)
        {
            e.Field.Visible = true;
            e.Field.AreaIndex = 0;
        }
        else if ((e.Field == fldUnidade) || (e.Field == fldIndicador))
            e.Field.Visible = true;
        else if (e.Field == fldMes) // quando 'incluindo' o campo Período na tela, esconde o campo Meta para o período.
            fldMeta.Options.ShowValues = !e.Field.Visible;
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
                e.Brick.Text = "l";
                e.Brick.TextValue = "l";
                if (e.Value.ToString().Equals("Vermelho"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Equals("Amarelo"))
                {
                    e.Appearance.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Equals("Verde"))
                {
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (e.Value.ToString().Equals("Azul"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.Appearance.ForeColor = Color.WhiteSmoke;
                }
                else if (e.Value.ToString().Equals("Laranja"))
                {
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

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";


        if (e.Parameter == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (e.Parameter == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";
        
        if (e.Parameter == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";
        
        if (e.Parameter == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";
        
        if (e.Parameter == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }

    private void carregaFiltros()
    {
        DataSet dsFiltro = new DataSet();

        dsFiltro = cDados.getUnidadesIndicadoresAcessoUsuario(codigoUsuarioResponsavel, codigoEntidade, "");
        ddlUnidades.DataSource = dsFiltro;
        ddlUnidades.TextField = "NomeUnidadeNegocio";
        ddlUnidades.ValueField = "CodigoUnidadeNegocio";
        ddlUnidades.DataBind();

        dsFiltro = cDados.getIndicadoresAcessoUsuario(codigoUsuarioResponsavel, codigoEntidade, "");
        ddlIndicador.DataSource = dsFiltro;
        ddlIndicador.TextField = "NomeIndicador";
        ddlIndicador.ValueField = "CodigoIndicador";
        ddlIndicador.DataBind();

        if (!IsPostBack)
        {
            if (ddlIndicador.Items.Count > 0)
                ddlIndicador.SelectedIndex = 0;

            if (ddlUnidades.Items.Count > 0)
            {
                if (ddlUnidades.Items.FindByValue(codigoEntidade.ToString()) != null)
                    ddlUnidades.Value = codigoEntidade.ToString();
                else
                    ddlUnidades.SelectedIndex = 0;
            }

        }
    }

    protected void pnCallbackDados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        fldAno.FilterValues.Clear();
        fldIndicador.FilterValues.Clear();
        fldUnidade.FilterValues.Clear();
        fldMes.FilterValues.Clear();
        ajustaTelaSelecaoFiltro();

        populaGrid(false);
    }

    protected void pvgDadosIndicador_HtmlCellPrepared(object sender, PivotHtmlCellPreparedEventArgs e)
    {
        if (e.DataField == null) return;

        var fieldName = e.DataField.FieldName;
        if (fieldName.Equals("Desempenho") || fieldName.Equals("DesempenhoAcumulado"))
        {
            string value = e.Value as string;
            if (!(string.IsNullOrWhiteSpace(value)))
                e.Cell.Text = string.Format("<img alt='' src='../../imagens/{0}Menor.gif' />", value);
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
                e.Cell.Text = string.Format("<img alt='' src='../../imagens/{0}Menor.gif' />", value);
        }
    }
}
