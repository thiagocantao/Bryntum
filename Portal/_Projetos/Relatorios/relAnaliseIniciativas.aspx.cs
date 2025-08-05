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
using DevExpress.XtraPivotGrid;
using System.Drawing;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Collections.Generic;

public partial class _Projetos_Relatorios_relAnaliseIniciativas : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;

    public string alturaTabela = "";
    public string larguraTabela = "";

    object metaAtual = new object();
    object resultadoAtual = new object();
    bool exportaOLAPTodosFormatos = false;
    #endregion

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
        carregaComboMapas();

        if (!IsPostBack)
        {
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
        }


        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack && ddlMapa.Items.Count > 0)
        {
            if (!hfGeral.Contains("tipoArquivo"))
                hfGeral.Set("tipoArquivo", "XLS");

            if (cDados.getInfoSistema("CodigoMapa") != null)
                ddlMapa.Value = cDados.getInfoSistema("CodigoMapa").ToString();
        }

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        populaGrid();
        defineAlturaTela();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
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

        alturaTabela = (alturaPrincipal - 210) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 10) + "px";

    }

    private void carregaComboMapas()
    {
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(cDados.getInfoSistema("CodigoEntidade").ToString(), codigoUsuarioResponsavel, "");

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

    protected void btnSelecionar_Click(object sender, EventArgs e)
    {
        populaGrid();
    }

    private void populaGrid()
    {
        string comandoSQL = string.Format(@"
            SELECT
		          [Indicador]		
		        , [Unidade]	
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
		        , [UltimaMeta]			
		        , [UltimoResultado]		
		        , [UltimoDesempenho]
            FROM {0}.{1}.f_GetDadosOLAP_IndicadorUndidades({2}, NULL)
        ", dbName, dbOwner, codigoUsuarioResponsavel);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
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
        if (e.Field.FieldName == "Periodo")
        {
            if ((e.ListSourceRowIndex1 >= 0) && (e.ListSourceRowIndex2 >= 0))
            {
                object valor1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "DataReferencia");
                object valor2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "DataReferencia");
                if ((null != valor1) && (null != valor2))
                {
                    // se as duas datas de referência forem iguais, compara por periodicidade
                    if ((System.DateTime)valor1 == (System.DateTime)valor2)
                    {
                        valor1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Periodicidade");
                        valor2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Periodicidade");
                    }
                    e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
                    e.Handled = true;
                } // if ((null != valor1) && ...
            } // if ((e.ListSourceRowIndex1 >= 0) && ...

        } // if (e.Field.FieldName == "Periodo") 
    }

    protected void pvgDadosIndicador_CustomCellDisplayText(object sender, DevExpress.Web.ASPxPivotGrid.PivotCellDisplayTextEventArgs e)
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
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".html";
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
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Value;
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "analiseIniciativas_" + dataHora + ".csv";
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


    protected void pvgDadosIndicador_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        bool periodoNaTela = false, anoNaTela = false;

        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        List<DevExpress.Web.ASPxPivotGrid.PivotGridField> rowFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.RowArea);
        List<DevExpress.Web.ASPxPivotGrid.PivotGridField> colFields = grid.GetFieldsByArea(DevExpress.XtraPivotGrid.PivotArea.ColumnArea);

        periodoNaTela = (rowFields.Contains(fldPeriodo) || colFields.Contains(fldPeriodo));

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
                anoNaTela = (rowFields.Contains(fldAno) || colFields.Contains(fldAno));

                // se o ano estiver na tela, vai pegar a último valor disponível
                if (anoNaTela)
                {
                    DateTime dtAux;
                    DateTime ultimaData = new DateTime();
                    object ultimoValor = new object();

                    string campoAObter;
                    if (e.FieldName.Equals("Meta") || e.FieldName.Equals("MetaAcumulada"))
                        campoAObter = "MetaAcumulada";
                    else if (e.FieldName.Equals("Desempenho") || e.FieldName.Equals("DesempenhoAcumulado"))
                        campoAObter = "DesempenhoAcumulado";
                    else
                        campoAObter = "ResultadoAcumulado";

                    foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                    {
                        if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                        {
                            DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];

                            dtAux = (DateTime)dataRow["DataReferencia"];
                            if (dtAux > ultimaData)
                            {
                                ultimaData = dtAux;
                                ultimoValor = dataRow[campoAObter];
                            } // if (dtAux > ultimaData)
                        } // if ((summaryRow.ListSourceRowIndex >= 0) && ...
                    } // foreach 

                    e.CustomValue = ultimoValor;
                } // if (anoNaTela
                else
                {
                    // se nem o período nem o ano estiverem na tela, busca o valor atual para a unidade em questão

                    string campoAObter;
                    if (e.FieldName.Equals("Meta") || e.FieldName.Equals("MetaAcumulada"))
                        campoAObter = "UltimaMeta";
                    else if (e.FieldName.Equals("Desempenho") || e.FieldName.Equals("DesempenhoAcumulado"))
                        campoAObter = "UltimoDesempenho";
                    else
                        campoAObter = "UltimoResultado";

                    foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                    {
                        if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                        {
                            DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                            e.CustomValue = dataRow[campoAObter];
                            break; // busca uma linha já que será igual para todas as linhas, uma vez que unidade está presente
                        } // if ((summaryRow.ListSourceRowIndex >= 0) && ...
                    } // foreach 
                }
            }// if ((dt != null) && ...
        } // else (periodoNaTela)
    }

    protected void pvgDadosIndicador_FieldVisibleChanged(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldEventArgs e)
    {
        // não permite esconder o campo unidade
        if (e.Field.FieldName.Equals("Unidade"))
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
            nomeArquivo = "~/imagens/menuExportacao/excel.png";

        if (e.Parameter == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (e.Parameter == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }

}
