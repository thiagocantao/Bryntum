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
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;
using System.Collections.Generic;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using System.Linq;
using PivotGrid = DevExpress.XtraPivotGrid;
using System.Text;
using DevExpress.Data.Filtering;

public partial class _Projetos_Relatorios_OLAP_ContratosEstendida : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    int codigoEntidade = 0;

    public string alturaTabela = "";
    public string larguraTabela = "";

    public bool exportaOLAPTodosFormatos = false;

    private List<string> Meses = new List<string>();

    #endregion

    public string vlabelNumeroInterno2 = "";
    public string vlabelNumeroInterno3 = "";

    public IEnumerable<string> TiposTarefas
    {
        get
        {
            var filterValues = Session["FilterValues"] as List<PivotGridFilterItem>;
            if (filterValues == null)
            {
                filterValues = new List<PivotGridFilterItem>();
                filterValues.Add(new PivotGridFilterItem("S/I", "S/I", true));
                Session["FilterValues"] = filterValues;

                var valores = grid.Fields["TipoTarefa"].GetVisibleValues();
                var dt = ObtemOpcoesFiltro();
                foreach (DataRow dr in dt.Rows)
                {
                    string tipoTarefa = dr.Field<string>("DescricaoTipoTarefaCronograma");
                    foreach (string val in valores)
                    {
                        if (VerificaEstaContido(val.Split(';'), tipoTarefa))
                        {
                            var filterItem = new PivotGridFilterItem(tipoTarefa, tipoTarefa, true);
                            filterValues.Add(filterItem);
                            break;
                        }
                    }
                }
            }

            foreach (var filter in filterValues)
                yield return filter.Text;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
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

        if (!IsCallback && !IsPostBack)
            Session["FilterValues"] = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            AtribuiPeriodo();
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_RelAnaTarCrn");
        }

        if (!IsPostBack)
        {
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");

            populaOpcoesExportacao();
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
        this.Title = cDados.getNomeSistema();
    }

    private void AtribuiPeriodo()
    {
        dteInicio.Date = DateTime.Today.AddDays(-15);
        dteFim.Date = DateTime.Today.AddDays(15);
    }

    private void populaGrid()
    {
        if ((!dteInicio.IsValid) || (!dteFim.IsValid))
            return;

        string strDataInicial = string.Format("{0}/{1}/{2}", dteInicio.Date.Day, dteInicio.Date.Month, dteInicio.Date.Year);
        string strDataFinal = string.Format("{0}/{1}/{2}", dteFim.Date.Day, dteFim.Date.Month, dteFim.Date.Year);

        string where = string.Format(" AND t.Termino >= CONVERT(SmallDateTime, '{0}', 103) AND t.Termino <= CONVERT(SmallDateTime, '{1}', 103)", strDataInicial
            , strDataFinal);

        int codigoCarteira = int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString());

        DataSet ds = cDados.getOLAPTarefasAtribuicoes(codigoEntidade, codigoUsuarioResponsavel, codigoCarteira, strDataInicial, strDataFinal);
        if (cDados.DataSetOk(ds))
        {
            grid.DataSource = ds.Tables[0];
            grid.DataBind();
        }
    }
    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 191) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 10) + "px";

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
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    nomeArquivo = "analiseTarefas_" + dataHora + ".html";
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
                    nomeArquivo = "analiseTarefas_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "analiseTarefas_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;

                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "analiseTarefas_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "analiseTarefas_" + dataHora + ".csv";
                    ASPxPivotGridExporter1.ExportToText(stream, ";");
                    app = "application/CSV";
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
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

                string script = string.Format(@"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('{0}', 'atencao', true, false, null);                                  
                                 </script>", erro);

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

    protected void grid_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
    {
        // se estiver ordenando a coluna 'NomeTarefa' 
        if (e.Field.FieldName == "NomeTarefa")
        {
            if (e.Value1 != null && e.Value2 != null && e.Value1.ToString().IndexOf('-') > 0 && e.Value2.ToString().IndexOf('-') > 0)
            {
                try
                {
                    string strNomeTarefa1 = e.Value1.ToString();
                    string strNomeTarefa2 = e.Value2.ToString();

                    int Seq1, Seq2;
                    Seq1 = int.Parse(strNomeTarefa1.Substring(0, strNomeTarefa1.IndexOf('-') - 1));
                    Seq2 = int.Parse(strNomeTarefa2.Substring(0, strNomeTarefa2.IndexOf('-') - 1));
                    e.Result = System.Collections.Comparer.Default.Compare(Seq1, Seq2);
                    e.Handled = true;
                }
                catch
                {

                }
            }
        } // if (e.Field == fldMes)
    }

    protected void grid_CustomFilterPopupItems(object sender, PivotCustomFilterPopupItemsEventArgs e)
    {
        if (e.Field.FieldName == "TipoTarefa")
        {
            e.Items.Clear();
            var filterValues = Session["FilterValues"] as List<PivotGridFilterItem>;
            if (filterValues == null)
            {
                foreach (var tipo in TiposTarefas)
                    e.Items.Add(new PivotGridFilterItem(tipo, tipo, true));
            }
            else
            {
                foreach (var filter in filterValues)
                    e.Items.Add(filter);
            }
        }
    }

    private DataTable ObtemOpcoesFiltro()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoEntidade INT
    SET @CodigoEntidade = {0}
    
 SELECT ttc.CodigoTipoTarefaCronograma,
        ttc.DescricaoTipoTarefaCronograma
   FROM TipoTarefaCronograma AS ttc
  WHERE codigoEntidade = @CodigoEntidade
  ORDER BY
        ttc.DescricaoTipoTarefaCronograma", codigoEntidade);

        #endregion

        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];

        return dt;
    }

    private static bool VerificaEstaContido(IEnumerable<string> itens, string valor)
    {
        return itens.Any(tipo => tipo.Trim() == valor);
    }

    protected void grid_FieldFilterChanging(object sender, PivotFieldFilterChangingEventArgs e)
    {
        var field = grid.Fields["IDTarefa"];
        if (e.Field.FieldName == "TipoTarefa" && field.GetVisibleValues().Count > 0)
        {
            var filterValues = Session["FilterValues"] as List<PivotGridFilterItem>;
            DataTable dt = ObtemDados();
            if (e.Values.Count > 0)
            {
                List<string> valoresFiltrados;
                if (e.FilterType == PivotGrid.PivotFilterType.Excluded)
                    valoresFiltrados = new List<string>(TiposTarefas.Except(e.Values.OfType<string>()));
                else
                    valoresFiltrados = new List<string>(e.Values.OfType<string>());

                e.Values.Clear();

                if (filterValues != null)
                {
                    foreach (PivotGridFilterItem filter in filterValues)
                        filter.IsChecked = valoresFiltrados.Contains(filter.Text);
                }
                RealizaFiltroPersonalizado(field, dt, valoresFiltrados);
            }
            else
            {
                if (filterValues != null)
                {
                    foreach (PivotGridFilterItem filter in filterValues)
                        filter.IsChecked = true;
                }
                RealizaFiltroPersonalizado(field, dt, TiposTarefas);
            }
        }
    }

    private void RealizaFiltroPersonalizado(PivotGridField field, DataTable dt, IEnumerable<string> valoresFiltrados)
    {
        grid.BeginUpdate();
        try
        {
            field.FilterValues.Clear();
            foreach (var val in valoresFiltrados)
            {
                var rows = dt.Select(string.Format("TipoTarefa Like '%{0}%'", val));
                foreach (var row in rows)
                    field.FilterValues.Add(row["IDTarefa"]);
            }
            field.FilterValues.FilterType = PivotGrid.PivotFilterType.Included;
        }
        finally
        {
            grid.EndUpdate();
        }
    }

    private DataTable ObtemDados()
    {
        DataTable dt;
        if (grid.DataSource == null)
        {
            string strDataInicial = string.Format("{0}/{1}/{2}", dteInicio.Date.Day, dteInicio.Date.Month, dteInicio.Date.Year);
            string strDataFinal = string.Format("{0}/{1}/{2}", dteFim.Date.Day, dteFim.Date.Month, dteFim.Date.Year);

            string where = string.Format(" AND t.Termino >= CONVERT(SmallDateTime, '{0}', 103) AND t.Termino <= CONVERT(SmallDateTime, '{1}', 103)", strDataInicial
                , strDataFinal);

            int codigoCarteira = int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString());
            dt = cDados.getOLAPTarefasAtribuicoes(codigoEntidade, codigoUsuarioResponsavel, codigoCarteira, strDataInicial, strDataFinal).Tables[0];
        }
        else
            dt = (DataTable)grid.DataSource;
        return dt;
    }
}
