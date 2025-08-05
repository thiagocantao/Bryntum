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

public partial class _Processos_Visualizacao_RelatoriosURL_Processos_url_relAnaliseProcessosObras : System.Web.UI.Page
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        cDados.aplicaEstiloVisual(Page);
        if (!IsPostBack)
        {
            //AtribuiPeriodo();
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }          
        }
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        //MyEditorsLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        buscaDadosGrid();
      

        defineLarguraTela();
        grid.OptionsPager.Visible = false;

        if (!IsPostBack)
        {
            grid.CollapseAll();
            
            //cDados.excluiNiveisAbaixo(1);
            //cDados.insereNivel(1, this);
            //Master.geraRastroSite();
        }

        hfGeral.Set("FoiMechido", "");

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    //private void AtribuiPeriodo()
    //{
    //    DateTime auxData = DateTime.Today;
    //    auxData = auxData.AddMonths(-1);
    //    auxData = auxData.AddDays(-(auxData.Day - 1));
    //    dteInicio.Date = auxData;
    //    dteFim.Date = DateTime.Today; 
    //}

    private void buscaDadosGrid()
    {
        //if ((!dteInicio.IsValid) || (!dteFim.IsValid))
        //    return;

        //string strDataInicial = string.Format("{0}/{1}/{2}", dteInicio.Date.Day, dteInicio.Date.Month, dteInicio.Date.Year);
        //string strDataFinal = string.Format("{0}/{1}/{2}", dteFim.Date.Day, dteFim.Date.Month, dteFim.Date.Year);

        string comandoSQL = string.Format(@"BEGIN
                 DECLARE @CodigoEntidade Int
                 

                 SET @CodigoEntidade = {2}

                 SELECT iw.NomeInstancia AS NomeProcesso,
                     DATEDIFF(DD,DataInicioInstancia, IsNull(DataTerminoInstancia,GetDate())) AS TempoProcessoDias,
                     DATEDIFF(HH,DataInicioInstancia, IsNull(DataTerminoInstancia,GetDate())) AS TempoProcessoHoras,
                     CASE WHEN iw.DataCancelamentoInstancia IS Not NULL THEN 'Cancelado'
                     WHEN iw.DataTerminoInstancia IS Not NULL THEN 'Finalizado'
                     ELSE 'Ativo' END AS StatusProcesso,
                     iw.DataInicioInstancia AS InicioProcesso,
                     iw.DataTerminoInstancia AS TerminoProcesso,
                     p.NomeProjeto,
                     IsNull(tso.DescricaoSegmentoObra,'S/I') AS SegmentoObra,
                     w.VersaoWorkflow AS Versao,
                     f.Descricao AS TipoProcesso,
                     tp.TipoProjeto ,
                     dbo.f_wf_GetAtrasoHorasInstancia(w.CodigoWorkflow,iw.CodigoInstanciaWF)/24 AS AtrasoDias,
                     dbo.f_wf_GetAtrasoHorasInstancia(w.CodigoWorkflow,iw.CodigoInstanciaWF) AS AtrasoHoras,
                     IsNull(ewf.DescricaoEtapa,'S/I') AS EtapaAtual,
                     YEAR(iw.DataInicioInstancia) AS AnoInicioProcesso,
                     MONTH(iw.DataInicioInstancia) AS MesInicioProcesso,
                     YEAR(iw.DataTerminoInstancia) AS AnoTerminoProcesso,
                     MONTH(iw.DataTerminoInstancia) AS MesTerminoProcesso
                 FROM InstanciasWorkflows AS iw 
                    INNER JOIN
                            Workflows AS w ON (w.CodigoWorkflow = iw.CodigoWorkflow) 
                    INNER JOIN
                            Fluxos AS f ON (f.CodigoFluxo = w.CodigoFluxo) 
                    INNER JOIN
                            Projeto AS p ON (p.CodigoProjeto = iw.IdentificadorProjetoRelacionado
                                AND p.DataExclusao IS NULL
                                AND p.CodigoEntidade = @CodigoEntidade) 
                    INNER JOIN
                            TipoProjeto AS tp ON (tp.CodigoTipoProjeto = p.CodigoTipoProjeto) 
                    LEFT JOIN 
                           Obra AS o ON (o.CodigoProjeto = p.CodigoProjeto) 
                    LEFT JOIN
                            TipoSegmentoObra AS tso ON (tso.CodigoSegmentoObra = o.CodigoSegmentoObra) 
                    LEFT JOIN
                            EtapasInstanciasWf AS eiwf ON (eiwf.CodigoEtapaWf = iw.EtapaAtual
                                    AND eiwf.CodigoWorkflow = iw.CodigoWorkflow
                                    AND eiwf.CodigoInstanciaWf = iw.CodigoInstanciaWf) 
                    LEFT JOIN
                           EtapasWf AS ewf ON (ewf.CodigoEtapaWf = eiwf.CodigoEtapaWf
                                AND ewf.CodigoWorkflow = eiwf.CodigoWorkflow) 
                END ", 
                //dbName, dbOwner, codigoEntidadeUsuarioResponsavel);
                dbName, dbOwner, codigoEntidadeUsuarioResponsavel);

        grid.DataSource = cDados.getDataSet(comandoSQL);
        grid.DataBind();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaPrincipal = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 200) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 220) + "px";
        
        //int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        //larguraTela = (largura - 33).ToString() + "px";

        //int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        //alturaTela = (altura - 20).ToString() + "px";


        //alturaTabela = (alturaTabela - 250) + "px";//a div vai ficar com essa altura
        //larguraTabela = (largura-220) + "px";
        //grid.Height = new Unit((alturaPrincipal - 220) + "px");

        Div1.Style.Add("overflow", "auto");

        Div1.Style.Add("height", alturaTabela);
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

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapProcObras", grid);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapProcObras", Resources.traducao.url_relAnaliseProcessosObras_olap_processos_obras, this, grid);
    }

    #endregion

}
