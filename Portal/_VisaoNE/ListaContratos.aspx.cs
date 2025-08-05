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
using DevExpress.XtraPrinting;
using System.Web.Hosting;
using System.Diagnostics;
using DevExpress.Web;
using System.IO;

public partial class _VisaoNE_ListaContratos : System.Web.UI.Page
{
    public string alturaTabela = "";
    public string larguraTabela = "";
    dados cDados;
    private bool exportaOLAPTodosFormatos = false;
    private int codigoUsuarioLogado = 0;
    private int codigoEntidade = 0;
    private int codigoMunicipio = -1;
    private bool indicaVigentesAno = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
               
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
                       
        if (!Page.IsPostBack)
        {            
            hfGeral.Set("tipoArquivo", "XLS");

            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
            //MyEditorsLocalizer.Activate();
        }
       
        populaGrid();
        defineAlturaTela();
        setDllExporta();
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            if (Request.QueryString["CodigoMunicipio"] != null && Request.QueryString["CodigoMunicipio"].ToString() != "")
            {
                codigoMunicipio = int.Parse(Request.QueryString["CodigoMunicipio"].ToString());
            }

            indicaVigentesAno = (Request.QueryString["IndicaVigentesAno"] != null && Request.QueryString["IndicaVigentesAno"].ToString() == "S");

            if (codigoMunicipio != -1)
            {
                string nomeMunicipio = "";

                DataSet dsMunicipio = cDados.getMunicipios(" AND CodigoMunicipio = " + codigoMunicipio);

                if (cDados.DataSetOk(dsMunicipio) && cDados.DataTableOk(dsMunicipio.Tables[0]))
                {
                    nomeMunicipio = dsMunicipio.Tables[0].Rows[0]["NomeMunicipio"].ToString();

                    if (gvDados.FilterExpression != "")
                        gvDados.FilterExpression += " AND [Municipio] = '" + nomeMunicipio + "'";
                    else
                        gvDados.FilterExpression = " [Municipio] = '" + nomeMunicipio + "'";
                }
            }

            if (indicaVigentesAno)
            {
                if (gvDados.FilterExpression != "")
                    gvDados.FilterExpression += " AND [VigenteAno] = 'Sim'";
                else
                    gvDados.FilterExpression = " [VigenteAno] = 'Sim'";
            }

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        this.Title = cDados.getNomeSistema();
    }

    private void setDllExporta()
    {
        string item = ddlExporta.SelectedItem.ToString();
        setImagenExportacao(item);
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        larguraTabela = gvDados.Width.ToString();
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 325;

    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();

        setImagenExportacao(parametro);
    }

    private void setImagenExportacao(string opcao)
    {
        string nomeArquivo = "";

        if (opcao == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (opcao == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (opcao == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (opcao == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (opcao == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }
    
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioLogado;

            string nomeArquivo = "", app = "", erro = "";

            try
            {

                if (hfGeral.Get("tipoArquivo").ToString() == "PDF")
                {
                    nomeArquivo = "contratos_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxGridViewExporter1.WritePdfToResponse(p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "contratos_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();

                    ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    app = "application/ms-excel";
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
                                    window.top.mostraMensagem(traducao.ListaContratos_erro_ao_exportar_os_dados__verifique_se_n_o_foi_ultrapassado_o_n_mero_m_ximo_de_256_colunas_, 'erro', true, false, null);                                   
                                 </script>";
                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();
        ListEditItem liXLS = new ListEditItem("XLS", "XLS");
        liXLS.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";

        ddlExporta.Items.Add(liXLS);
        ddlExporta.ClientEnabled = false;
        
        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);
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

    private void populaGrid()
    {

        DataSet ds = cDados.getListaInformacoesContratos(codigoEntidade, codigoUsuarioLogado, "F");
        
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            gvDados.TotalSummary.Clear();

            ASPxSummaryItem valorContrato = new ASPxSummaryItem();
            valorContrato.FieldName = "ValorContrato";
            valorContrato.ShowInColumn = "ValorContrato";
            valorContrato.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorContrato.DisplayFormat = "{0:c2}";
            gvDados.TotalSummary.Add(valorContrato);


            ASPxSummaryItem valorPago = new ASPxSummaryItem();
            valorPago.FieldName = "ValorPago";
            valorPago.ShowInColumn = "ValorPago";
            valorPago.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorPago.DisplayFormat = "{0:c2}";
            gvDados.TotalSummary.Add(valorPago);

            ASPxSummaryItem valorSaldo = new ASPxSummaryItem();
            valorSaldo.FieldName = "Saldo";
            valorSaldo.ShowInColumn = "Saldo";
            valorSaldo.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            valorSaldo.DisplayFormat = "{0:c2}";
            gvDados.TotalSummary.Add(valorSaldo);
        }

    }

    public class MyEditorsLocalizer : DevExpress.Web.ASPxPivotGrid.ASPxPivotGridResLocalizer
    {
        public static void Activate()
        {
            MyEditorsLocalizer localizer = new MyEditorsLocalizer();
            DefaultActiveLocalizerProvider<PivotGridStringId> provider = new DefaultActiveLocalizerProvider<PivotGridStringId>(localizer);
            MyEditorsLocalizer.SetActiveLocalizerProvider(provider);

        }
        
        public override string GetLocalizedString(PivotGridStringId id)
        {
            switch (id)
            {
                case PivotGridStringId.GrandTotal: return "Total";
                case PivotGridStringId.FilterShowAll: return "Todos";
                case PivotGridStringId.FilterCancel: return "Cancelar";
                case PivotGridStringId.PopupMenuRemoveAllSortByColumn: return "Remover Ordenação";
                case PivotGridStringId.PopupMenuShowPrefilter: return "Mostrar Pré-filtro";
                case PivotGridStringId.PopupMenuShowFieldList: return "Listar Colunas Disponíveis";
                case PivotGridStringId.PopupMenuRefreshData: return "Atualizar Dados";
                case PivotGridStringId.PrefilterFormCaption: return "Pré-filtro";
                case PivotGridStringId.PopupMenuCollapse: return "Contrair";
                case PivotGridStringId.PopupMenuCollapseAll: return "Contrair Todos";
                case PivotGridStringId.PopupMenuExpand: return "Expandir";
                case PivotGridStringId.PopupMenuExpandAll: return "Expandir Todos";
                case PivotGridStringId.PopupMenuHideField: return "Remover Coluna";
                case PivotGridStringId.PopupMenuHideFieldList: return "Remover Coluna";
                case PivotGridStringId.PopupMenuHidePrefilter: return "Remover Pré-filtro";
                case PivotGridStringId.RowHeadersCustomization: return "Arraste as colunas que irão formar os agrupamentos da consulta";
                case PivotGridStringId.ColumnHeadersCustomization: return "Arraste as colunas que irão formar as colunas da consulta";
                case PivotGridStringId.FilterHeadersCustomization: return "Arraste as colunas que irão formar os filtros da consulta";
                case PivotGridStringId.DataHeadersCustomization: return "Arraste as colunas que irão formar os dados da consulta";
                
                case PivotGridStringId.FilterInvert: return "Inverter Filtro";
                case PivotGridStringId.PopupMenuFieldOrder: return "Ordenar";
                case PivotGridStringId.PopupMenuSortFieldByColumn: return "Ordenar pela coluna {0}";
                case PivotGridStringId.PopupMenuSortFieldByRow: return "Ordenar pela linha {0}";
                case PivotGridStringId.CustomizationFormCaption: return "Colunas Disponíveis";
                default: return base.GetLocalizedString(id);
            }
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
    {        
        gvDados.ExpandAll();
    }
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
