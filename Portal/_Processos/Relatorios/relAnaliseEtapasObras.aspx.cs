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
using System.Collections.Generic;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.Web;
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;

public partial class _Processos_Relatorios_relAnaliseEtapasObras : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoUsuario;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";
    private string dbName, dbOwner;

    public string alturaTabela = "";
    public string larguraTabela = "";
    public bool exportaOLAPTodosFormatos = false;

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            populaDdlGrupoFluxo();

            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
        }
      
        //MyEditorsLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        buscaDadosGrid();


        defineLarguraTela();
        grid.OptionsPager.Visible = false;

        if (!IsPostBack)
        {
            grid.CollapseAll();

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        hfGeral.Set("FoiMechido", "");
    }



    private void buscaDadosGrid()
    {

        //if ((0 == ddlGrupoFluxo.Text.Length) )
        //    return;

        string codigoGrupoFluxo = "";
        codigoGrupoFluxo =string.Format("{0}", ddlGrupoFluxo.Value.ToString());

        string sWhere = (codigoGrupoFluxo != "-1") ? " AND f.CodigoGrupoFluxo = " + codigoGrupoFluxo : "";


        string comandoSQL = string.Format(@"BEGIN
       DECLARE @CodigoEntidade Int
       

       SET @CodigoEntidade = {2}

       SELECT cont.numeroContrato, iw.NomeInstancia AS NomeProcesso,
           Convert(Decimal(25,2),DATEDIFF(HH,eiwf.DataInicioEtapa, IsNull(eiwf.DataTerminoEtapa,GetDate()))/24.) AS TempoEtapaDias,
           DATEDIFF(HH,eiwf.DataInicioEtapa, IsNull(eiwf.DataTerminoEtapa,GetDate())) AS TempoEtapaHoras,
           CASE WHEN iw.DataCancelamentoInstancia IS Not NULL THEN 'Cancelado'
           WHEN iw.DataTerminoInstancia IS Not NULL THEN 'Finalizado'
           ELSE 'Ativo' END AS StatusProcesso,
           eiwf.DataInicioEtapa AS InicioEtapa,
           eiwf.DataTerminoEtapa AS TerminoEtapa,
           p.NomeProjeto,
           IsNull(tso.DescricaoSegmentoObra,'S/I') AS SegmentoObra,
           w.VersaoWorkflow AS Versao,
           f.Descricao AS TipoProcesso,
           tp.TipoProjeto ,
           CASE WHEN DATEDIFF(HH,dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout, iw.DataTerminoInstancia, ewf.indicaDataCalculo, @CodigoEntidade, eiwf.CodigoWorkflow, eiwf.CodigoInstanciaWf), IsNull(eiwf.DataTerminoEtapa,GetDate()) ) > 0 THEN Convert(Decimal(25,2), DATEDIFF(HH, dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout, iw.DataTerminoInstancia, ewf.indicaDataCalculo, @CodigoEntidade, eiwf.CodigoWorkflow, eiwf.CodigoInstanciaWf  ), IsNull(eiwf.DataTerminoEtapa,GetDate()) )/24.) ELSE 0 END AS AtrasoDias,
           CASE WHEN DATEDIFF(HH,dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout, iw.DataTerminoInstancia, ewf.indicaDataCalculo, @CodigoEntidade, eiwf.CodigoWorkflow, eiwf.CodigoInstanciaWf), IsNull(eiwf.DataTerminoEtapa,GetDate()) ) > 0 THEN DATEDIFF(HH, dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout, iw.DataTerminoInstancia, ewf.indicaDataCalculo, @CodigoEntidade, eiwf.CodigoWorkflow, eiwf.CodigoInstanciaWf ), IsNull(eiwf.DataTerminoEtapa,GetDate()) ) ELSE 0 END AS AtrasoHoras,           
--           CASE WHEN DATEDIFF(HH,dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout ), IsNull(eiwf.DataTerminoEtapa,GetDate()) ) > 0 THEN Convert(Decimal(25,2), DATEDIFF(HH, dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout ), IsNull(eiwf.DataTerminoEtapa,GetDate()) )/24.) ELSE 0 END AS AtrasoDias,
--           CASE WHEN DATEDIFF(HH,dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout ), IsNull(eiwf.DataTerminoEtapa,GetDate()) ) > 0 THEN DATEDIFF(HH, dbo.[f_wf_CalculaDataPrevisaoTerminoEtapa]( eiwf.DataInicioEtapa, ewf.ValorTimeoutEtapa, ewf.UnidadeMedidaTimeout ), IsNull(eiwf.DataTerminoEtapa,GetDate()) ) ELSE 0 END AS AtrasoHoras,           
           YEAR(eiwf.DataInicioEtapa) AS AnoInicioEtapa,
           MONTH(eiwf.DataInicioEtapa) AS MesInicioEtapa,
           YEAR(eiwf.DataTerminoEtapa) AS AnoTerminoEtapa,
           MONTH(eiwf.DataTerminoEtapa) AS MesTerminoEtapa,
           ewf.NomeEtapaWF AS NomeEtapa,
           CASE WHEN eiwf.DataTerminoEtapa IS Not NULL THEN 'Finalizada'
                 ELSE 'Ativa' END AS StatusEtapa,
           u.NomeUsuario AS ResponsavelEtapa,
           ewf.NomeEtapaReduzidoWf NomeEtapaReduzido       
       FROM {0}.{1}.InstanciasWorkflows AS iw INNER JOIN

            {0}.{1}.Workflows AS w ON (w.CodigoWorkflow = iw.CodigoWorkflow)       INNER JOIN

            {0}.{1}.Fluxos AS f ON (f.CodigoFluxo = w.CodigoFluxo)                 INNER JOIN

            {0}.{1}.Projeto AS p ON (p.CodigoProjeto = iw.IdentificadorProjetoRelacionado AND 
                                     p.DataExclusao IS NULL AND 
                                     p.CodigoEntidade = @CodigoEntidade)           LEFT JOIN

            {0}.{1}.Contrato cont ON (p.CodigoProjeto = cont.CodigoProjeto
																AND p.DataExclusao IS NULL
                                      and cont.tipoPessoa = 'F')  INNER JOIN

            {0}.{1}.TipoProjeto AS tp ON (tp.CodigoTipoProjeto = p.CodigoTipoProjeto) LEFT JOIN 

            {0}.{1}.Obra AS o ON (o.CodigoProjeto = p.CodigoProjeto) LEFT JOIN

            {0}.{1}.TipoSegmentoObra AS tso ON (tso.CodigoSegmentoObra = o.CodigoSegmentoObra) LEFT JOIN

            {0}.{1}.EtapasInstanciasWf AS eiwf ON ( eiwf.CodigoWorkflow = iw.CodigoWorkflow AND 
                                                    eiwf.CodigoInstanciaWf = iw.CodigoInstanciaWf) LEFT JOIN

            {0}.{1}.EtapasWf AS ewf ON (ewf.CodigoEtapaWf = eiwf.CodigoEtapaWf AND 
                                        ewf.CodigoWorkflow = eiwf.CodigoWorkflow) LEFT JOIN 

            {0}.{1}.Usuario AS u ON (u.CodigoUsuario = eiwf.IdentificadorUsuarioFinalizador) LEFT JOIN

	        {0}.{1}.[Pessoa] AS [pes] ON (pes.[CodigoPessoa] = cont.[CodigoPessoaContratada]) LEFT JOIN 

            {0}.{1}.[PessoaEntidade] AS [pe] ON (
			pe.[CodigoPessoa] = cont.[CodigoPessoaContratada]
			AND pe.codigoEntidade = {2}
            --AND pe.IndicaFornecedor = 'S'  
			) 
   
    WHERE iw.DataCancelamentoInstancia IS NULL
      AND RTrim(LTrim(iw.NomeInstancia)) <> ''                     
      {3} 
  END  ", dbName, dbOwner, codigoEntidadeUsuarioResponsavel, sWhere);

        grid.DataSource = cDados.getDataSet(comandoSQL);
        grid.DataBind();

        if (!hfGeral.Contains("FoiMechido") || ("S" != hfGeral.Get("FoiMechido").ToString()))
        {
            grid.CollapseAll();

        }
        else
        {

        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = (largura - 33).ToString() + "px";

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 20).ToString() + "px";


        alturaTabela = (altura - 190) + "px";//a div vai ficar com essa altura
        larguraTabela = (largura - 20) + "px";
    }



    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
    {
        grid.JSProperties["cp_Alterado"] = "";
        if (e.Parameters.Equals("PopularGrid"))
            buscaDadosGrid();
        else
        {
            grid.JSProperties["cp_Alterado"] = "S";
        }
    }



    protected void grid_AfterPerformCallback(object sender, EventArgs e)
    {
        grid.JSProperties["cp_Alterado"] = "S";
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
                    nomeArquivo = "AnaliseProcessosObras_" + dataHora + ".html";
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
                    nomeArquivo = "AnaliseProcessosObras_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxPivotGridExporter1.ExportToPdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "AnaliseProcessosObras_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    ASPxPivotGridExporter1.ExportToXls(stream, x);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "AnaliseProcessosObras_" + dataHora + ".rtf";
                    ASPxPivotGridExporter1.ExportToRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    nomeArquivo = "AnaliseProcessosObras_" + dataHora + ".csv";
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
                                    window.top.mostraMensagem(traducao.relAnaliseEtapasObras_erro_ao_exportar_os_dados__verifique_se_n_o_foi_ultrapassado_o_n_mero_m_ximo_de_256_colunas_, 'erro', true, false, null);                                   
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

                case PivotGridStringId.GrandTotal: return "Média";
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
    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName == "TempoEtapaDias" || e.DataField.FieldName == "TempoEtapaHoras" || e.DataField.FieldName == "AtrasoDias" || e.DataField.FieldName == "AtrasoHoras")
            {
                // Specifies the cell's display text.
                ((TextBrick)e.Brick).XlsExportNativeFormat = DevExpress.Utils.DefaultBoolean.True;
                ((TextBrick)e.Brick).XlsxFormatString = "n1";
            }
        }
    }

    private void populaDdlGrupoFluxo()
    {
        DataTable dt = null;

        string sComando = string.Format(@"
                    SELECT CodigoGrupoFluxo
                          ,DescricaoGrupoFluxo
                          ,OrdemGrupoMenu
                          ,IniciaisGrupoFluxo
                          ,CodigoEntidade
                      FROM {0}.{1}.GrupoFluxo
                     WHERE CodigoEntidade = {2}
                     ORDER BY DescricaoGrupoFluxo
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(sComando);

        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            ddlGrupoFluxo.DataSource = dt;
            ddlGrupoFluxo.TextField = "DescricaoGrupoFluxo";
            ddlGrupoFluxo.ValueField = "CodigoGrupoFluxo";
            ddlGrupoFluxo.DataBind();
            ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

            ddlGrupoFluxo.Items.Insert(ddlGrupoFluxo.Items.Count, lei);
            ddlGrupoFluxo.SelectedIndex = ddlGrupoFluxo.SelectedIndex == -1 ? 0 : ddlGrupoFluxo.SelectedIndex;
        }
    }


}
