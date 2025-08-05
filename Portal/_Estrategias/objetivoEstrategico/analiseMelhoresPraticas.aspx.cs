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
using System.Diagnostics;
using DevExpress.Web;
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Drawing;
using DevExpress.XtraPivotGrid.Data;

public partial class _Estrategias_objetivoEstrategico_analiseMelhoresPraticas : System.Web.UI.Page
{
    dados cDados;
    DataSet ds;

    public string alturaTabela = "";
    public string larguraTabela = "";
    public int codigoEntidadeUsuarioResponsavel;
    private int codigoObjetivoEstrategico = -1;
    private int codigoEntidade = -1;
    private int idUsuarioLogado = -1;
    private int idObjetoPai = 0;
    private string tipoAssociacao = "";

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() + "" != "")
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());

        if (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() + "" != "")
            tipoAssociacao = Request.QueryString["TA"].ToString();

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        defineAlturaTela();
        carregaGrid();

        cDados.aplicaEstiloVisual(this.Page);

        if (!Page.IsPostBack)
        {
            bool bPodeAcessarTela = false;
            bPodeAcessarTela = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoObjetivoEstrategico, "NULL", "OB", idObjetoPai, "NULL", "OB_AnlAcnSug");
            if (!bPodeAcessarTela)
                RedirecionaParaTelaSemAcesso(this);


            carregaCampos();
            
            codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());            
            
            //MyEditorsLocalizer.Activate();
        }

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }
    public void RedirecionaParaTelaSemAcesso(System.Web.UI.Page page)
    {
        try
        {

            page.Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
        }
        catch
        {
            page.Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcessoNoMaster.aspx";
            page.Response.End();
        }
    }
    private void carregaCampos()
    {
        DataTable dt = cDados.getObjetivoEstrategico(null, codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            //txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            //txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtResponsavel.Text = dt.Rows[0]["NomeUsuario"].ToString();
            //txtTema.Text = dt.Rows[0]["Tema"].ToString();
        }
        else
        {
            //txtPerspectiva.Text = "";
            txtObjetivoEstrategico.Text = "";
            //txtMapa.Text = "";
            txtResponsavel.Text = "";
            //txtTema.Text = "";
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 285) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 200) + "px";

        //pnMelhoresPraticas.Height = new Unit(alturaTabela);

    }

    private void carregaGrid()
    {
        //quando estiver concluido essa função deve ser colocada na classe dados



        string whereCodigoMapa = " and oe.CodigoObjetoEstrategia = " + codigoObjetivoEstrategico;// ddlMapaEstrategico.SelectedItem != null ? " AND oe.CodigoMapaEstrategico = " + ddlMapaEstrategico.SelectedItem.Value : "";
        
        string comandoSQL = string.Format(@"
	    SELECT un.SiglaUnidadeNegocio AS Unidade,
		   i.NomeIndicador AS Indicador,
		  CASE {0}.{1}.f_GetUltimoDesempenhoIndicador(un.CodigoUnidadeNegocio,i.CodigoIndicador,YEAR(GetDate()),MONTH(GetDate()),'A') 
		       WHEN 'Vermelho'  THEN 'Crítico'
		       WHEN 'Amarelo' THEN 'Atenção'
		       WHEN 'Verde' THEN 'Satisfatório'
		       WHEN 'Azul' THEN 'Excelente'
		       ELSE 'Sem Informação' END AS Desempenho,     
		   CASE WHEN pas.CodigoAcaoSugerido IS NULL THEN 'Não' ELSE 'Sim' END AS AcaoSugerida1,
		   p.NomeProjeto AS Projeto,
           IsNull(Acoes.DescricaoAcao,'Sem Ação Sugerida') AS AcaoSugerida,
		   1 AS Quantidade 
	 FROM {0}.{1}.IndicadorObjetivoEstrategico AS ioe INNER JOIN
		  {0}.{1}.Indicador AS i ON (i.CodigoIndicador = ioe.CodigoIndicador) INNER JOIN
		  {0}.{1}.IndicadorUnidade AS iu ON (iu.CodigoIndicador = i.CodigoIndicador) INNER JOIN
		  {0}.{1}.UnidadeNegocio AS un ON (un.CodigoUnidadeNegocio = iu.CodigoUnidadeNegocio
							   AND un.CodigoEntidade = {2}
                               AND un.IndicaUnidadeNegocioAtiva = 'S'
                               AND un.DataExclusao IS NULL) INNER JOIN
		  {0}.{1}.ObjetoEstrategia AS oe ON (oe.CodigoObjetoEstrategia = ioe.CodigoObjetivoEstrategico) LEFT JOIN
		  {0}.{1}.ProjetoObjetivoEstrategico AS poe ON (poe.CodigoObjetivoEstrategico = oe.CodigoObjetoEstrategia) LEFT JOIN   
		  {0}.{1}.ProjetoAcoesSugeridas AS pas ON (pas.CodigoObjetivoEstrategico = oe.CodigoObjetoEstrategia
									   AND pas.CodigoProjeto = poe.CodigoProjeto) LEFT JOIN
          {0}.{1}.AcoesSugeridas AS acoes ON (acoes.CodigoAcaoSugerida = pas.CodigoAcaoSugerido)INNER JOIN
		  {0}.{1}.Projeto AS p ON (p.CodigoProjeto = poe.CodigoProjeto
					   AND p.DataExclusao IS NULL)                                   
	WHERE oe.DataExclusao IS NULL
	  AND i.DataExclusao IS NULL
	  AND p.NomeProjeto IS NOT NULL
      {3}
", cDados.getDbName(), cDados.getDbOwner(),codigoEntidade, whereCodigoMapa);

        ds = cDados.getDataSet(comandoSQL);
        gvMelhoresPraticas.DataSource = ds.Tables[0];
        gvMelhoresPraticas.DataBind();
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

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.exportaOlap(ASPxPivotGridExporter2, parameter, this);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, false, "OlapMelPrat", Resources.traducao.analiseMelhoresPraticas_olap_melhores_pr_ticas, this, gvMelhoresPraticas);
    }

    #endregion
}
