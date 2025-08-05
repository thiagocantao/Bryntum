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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Drawing;
using DevExpress.XtraPivotGrid;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using System.Collections.Generic;
using System.IO;
using DevExpress.Utils;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;

public partial class _OLAP_RH_Tabela : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoUsuario;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";
    private string dbName, dbOwner;

    public string alturaTabela = "";
    public string larguraTabela = "";
    private bool exportaOLAPTodosFormatos = false;
    protected class Alocacoes
    { 
        private List<string> ListaDeRecursos;
        
        private List<DateTime> ListaDeDatas;
        
        public Alocacoes()
        {
            ListaDeRecursos = new List<string>();
            ListaDeDatas = new List<DateTime>();
        }
        
        public void Clear()
        {
            ListaDeRecursos.Clear();
            ListaDeDatas.Clear();
        }

        public void Add(string recurso, DateTime data)
        {
            ListaDeRecursos.Add(recurso);
            ListaDeDatas.Add(data);
        }

        public bool ContemAlocacao(string recurso, DateTime data)
        {
            return ( ListaDeRecursos.Contains(recurso) && ListaDeDatas.Contains(data) );
        }

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        hfGeral.Set("tipoArquivo", "XLS");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            AtribuiPeriodo();
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
        }

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        buscaDadosGrid();

        defineLarguraTela();

        grid.OptionsPager.Visible = true;

        if (!IsPostBack)
            grid.CollapseAll();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void AtribuiPeriodo()
    {
        dteInicio.Date = DateTime.Today.AddDays(-15);
        dteFim.Date = DateTime.Today.AddDays(15);
    }

    private void buscaDadosGrid()
    {
        if ((!dteInicio.IsValid) || (!dteFim.IsValid))
            return;

        string strDataInicial = string.Format("{0}/{1}/{2}", dteInicio.Date.Day, dteInicio.Date.Month, dteInicio.Date.Year);
        string strDataFinal = string.Format("{0}/{1}/{2}", dteFim.Date.Day, dteFim.Date.Month, dteFim.Date.Year);

        string comandoSQL = string.Format(@" 

            DECLARE @DataInicial    SmallDateTime
            DECLARE @DataFinal      SmallDateTime
            SET @DataInicial        = CONVERT(SmallDateTime, '{4}', 103)
            SET @DataFinal          = CONVERT(SmallDateTime, '{5}', 103)

            /*
            SELECT 
	            [Data]		                AS [DataReferencia]
			    , [Ano]	
			    , [Mes] 
			    , [Semana] 
                , [ResourceUID]
                , [SiglaUnidadeNegocio]		
                , [NomeProjeto]				
                , [DescricaoStatus]			
                , [NomeRecurso]				
                , [Trabalho]				
                , [TrabalhoLB]				
                , [TrabalhoReal]			
                , [Capacidade]				
                , [Disponibilidade]
                , [CustoHora]
		        , [Anotacoes]
                , [NomeUnidadeNegocio]
				, [NomeEntidade]
            FROM 
                {0}.{1}.f_GetDadosOLAP_ProjetosAnaliseRH({2}, {3}, @DataInicial, @DataFinal )
            */

            EXEC {0}.{1}.p_GetDadosOLAP_ProjetosAnaliseRH {2}, {3}, @DataInicial, @DataFinal

		    ", dbName, dbOwner, codigoUsuario, codigoEntidadeUsuarioResponsavel, strDataInicial, strDataFinal);

        grid.DataSource = cDados.getDataSet(comandoSQL);

        grid.DataBind();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = (largura - 33).ToString() + "px";

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 20).ToString() + "px";


        alturaTabela = (altura - 200) + "px";//a div vai ficar com essa altura
        larguraTabela = (largura - 20) + "px";

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
    
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuario;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    nomeArquivo = "OLAPRecursosHumanos_" + dataHora + ".html";
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
                    nomeArquivo = "OLAPRecursosHumanos_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "OLAPRecursosHumanos_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "OLAPRecursosHumanos_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "OLAPRecursosHumanos_" + dataHora + ".csv";
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
    
    protected void grid_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        if (e.DataField == fieldDisponibilidade)
        {
            double x;
            if ( (e.Value != null) && double.TryParse(e.Value.ToString(), out x) && (x<0) )
                e.CellStyle.ForeColor = Color.Red;
        }

        if ( (e.DataField == fieldDisponibilidade) || (e.DataField == fieldCapacidade) )
        {
            if (e.Value != null) 
            {
                double valor;
                double.TryParse(e.Value.ToString(), out valor);
                e.CellStyle.Font.Italic = (valor > 0 );
            }
        }
    }

    protected void grid_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        if ( e.Field == fieldNomeProjeto && (e.Value1 != e.Value2) )
        {
            if (e.Value1.ToString().Equals("Disponibilidade") )
            {
                e.Result = -1;
                e.Handled = true;
            }
            else if (e.Value2.ToString().Equals("Disponibilidade") )
            {
                e.Result = 1;
                e.Handled = true;
            }
        }
        else if ( (e.Field == fieldMes) || ( e.Field == fieldSemana  ) )
        { // se estiver ordenando a coluna 'Mes' ou 'Semana'
            ASPxPivotGrid grid = (ASPxPivotGrid)sender;
            if (e.Value1.ToString() == e.Value2.ToString())
            {
                e.Result = 0;
                e.Handled = true;
            }
            else
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
            } // else (e.Value1 == e.Value2)

        } // 
    }
    
    protected void grid_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
    {
        if ( e.Parameters.Equals("PopularGrid") )
            buscaDadosGrid();
    }
    
    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        //Font fonte1 = new Font("Verdana", 8.0f, FontStyle.Regular);
        //e.Brick.Style.Font = fonte1;
        if (e.DataField != null)
        {
            if (e.DataField == fieldDisponibilidade)
            {
                double x;
                if ((e.Value != null) && double.TryParse(e.Value.ToString(), out x) && (x < 0))
                    e.Appearance.ForeColor = Color.Red;
            }

            if ((e.DataField == fieldDisponibilidade) || (e.DataField == fieldCapacidade))
            {
                if (e.Value != null)
                {
                    double valor;
                    double.TryParse(e.Value.ToString(), out valor);
                    Font fonteItalica = new Font("Verdana", 8.0f, (valor > 0) ? FontStyle.Italic : FontStyle.Regular);
                    e.Appearance.Font = fonteItalica;
                }
            }
        }
    }
    protected void ASPxPivotGridExporter1_CustomExportHeader(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportHeaderEventArgs e)
    {
        //Font fonte1 = new Font("Verdana", 8.0f, FontStyle.Regular);
        //e.Brick.Style.Font = fonte1;
    }
    protected void ASPxPivotGridExporter1_CustomExportFieldValue(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportFieldValueEventArgs e)
    {
        //Font fonte1 = new Font("Verdana", 8.0f, FontStyle.Regular);
        //e.Brick.Style.Font = fonte1;
    }

    protected void grid_CustomCellDisplayText(object sender, DevExpress.Web.ASPxPivotGrid.PivotCellDisplayTextEventArgs e)
    {
        //if (e.DataField.FieldName == "ResourceUID")
        //{
        //    DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
        //    ASPxPivotGrid gridPV = (ASPxPivotGrid)sender;
        //    DataSet dt = (DataSet)gridPV.DataSource;          
        //    double valorTrabalho = 0, valorCapacidade = 0;            

        //    if ((dt != null) && (ds != null))
        //    {
        //        foreach (DataRow dr in dt.Tables[0].Select("ResourceUID='" + e.Value + "'"))
        //        {
        //            valorCapacidade += dr["Capacidade"].ToString() == "" ? 0 : double.Parse(dr["Capacidade"].ToString());
        //            valorTrabalho += dr["Trabalho"].ToString() == "" ? 0 : double.Parse(dr["Trabalho"].ToString());
        //        }
        //    }

        //    e.DisplayText = string.Format("{0:p2}", valorCapacidade == 0 ? 0 : valorTrabalho / valorCapacidade);
        //}
    }
}

/*
    protected void grid_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if ((e.FieldName.Equals("Disponibilidade")) || (e.FieldName.Equals("Capacidade")))
        {
            ASPxPivotGrid grid = (ASPxPivotGrid)sender;
            Alocacoes alocacoes = new Alocacoes();
            if (MostrarTotalDisponibilidade(grid, e))
            {
                DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                DateTime data;
                string recurso;
                double valorSomar = 0;
                double dblAux;

                foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                {
                    data = DateTime.MinValue;
                    recurso = string.Empty;

                    if (summaryRow["DataReferencia"] != null)
                        data = (DateTime) summaryRow["DataReferencia"];

                    if (summaryRow["NomeRecurso"] != null)
                        recurso = summaryRow["NomeRecurso"].ToString();

                    if (!alocacoes.ContemAlocacao(recurso, data))
                    {
                        if (double.TryParse(summaryRow[e.FieldName].ToString(), out dblAux))
                            valorSomar += dblAux;

                        alocacoes.Add(recurso, data);
                   } // 
                }
                e.CustomValue = valorSomar;
            } // if (MostrarTotalDisponibilidade(grid, e))
        } // if (e.FieldName.Equals("Disponibilidade"))
    }

    private bool MostrarTotalDisponibilidade(ASPxPivotGrid grid, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        bool mostrar = false;

        if ((e.RowField == null) || (e.ColumnField == null))
            mostrar = true;
        else
        {
            int fieldIndex = -1;
            if ((e.ColumnField != null) && (e.ColumnField.FieldName.Equals("NomeRecurso")))
                fieldIndex = e.ColumnField.AreaIndex;
            else if ((e.RowField != null) && (e.RowField.FieldName.Equals("NomeRecurso")))
                fieldIndex = e.RowField.AreaIndex;

            if (fieldIndex == 0)
                mostrar = true;
        }

        return mostrar;
    }
*/

