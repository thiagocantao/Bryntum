/*
 09/12//2010: Mudança by Alejandro: 
            Foi implementado o filtro de mapa, so da entidade logada.
            private void populaMapaEstrategico()
            
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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraPivotGrid;
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;

public partial class _Processos_Visualizacao_RelatoriosURL_Estrategia_url_relMatrizSuficienciaProjEstrategicos : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoMapa = 0;
    public string alturaTabela;
    public string larguraTabela = "";

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
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_EstMatSufPrj");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
        }

        cDados.aplicaEstiloVisual(Page);
        ////MyEditorsLocalizer.Activate();
        populaMapaEstrategico();
        carregaGrid();
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

    private void populaMapaEstrategico()
    {
        string where = @"
                    AND Mapa.IndicaMapaEstrategicoAtivo = 'S'
                    AND un.CodigoEntidade               = " + codigoEntidadeUsuarioResponsavel.ToString();
        DataSet ds = cDados.getMapasEstrategicos(null, where);
        ddlMapa.ValueField = "CodigoMapaEstrategico";
        ddlMapa.TextField = "TituloMapaEstrategico";
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlMapa.DataSource = ds.Tables[0];
            ddlMapa.DataBind();
        }

        if (!IsPostBack && ddlMapa.Items.Count > 0)
            ddlMapa.SelectedIndex = 0;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 275) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 220) + "px";
        //pvgMapaEntregas.Width = new Unit(larguraTabela);

        Div1.Style.Add("overflow", "auto");
        //Div1.Style.Add("width", larguraTabela);

        Div1.Style.Add("height", alturaTabela);

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
    
    private void carregaGrid()
    {
        if (ddlMapa.SelectedItem != null)
        {
            codigoMapa = int.Parse(ddlMapa.SelectedItem.Value.ToString());
        }
        
        DataSet ds = cDados.getMatrizSuficiencia(codigoEntidadeUsuarioResponsavel, codigoMapa);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgMapaEntregas.DataSource = ds.Tables[0];
            pvgMapaEntregas.DataBind();
        }
    }

    protected void pvgMapaEntregas_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        if (e.DataField != null &&  e.DataField.FieldName == "PossuiObjetivoAssociado")
        {
            if (Convert.ToInt32(e.Value) > 0)
            {
                e.CellStyle.BackColor = System.Drawing.Color.DarkGreen;
                e.CellStyle.ForeColor = System.Drawing.Color.DarkGreen;
            }

            else
            {
                e.CellStyle.BackColor = System.Drawing.Color.White;
                e.CellStyle.ForeColor = System.Drawing.Color.White;
            }
        }
    }

    /*private void atualizaAreaDados()
    {
        hfGeral.Clear();
        qtdColunasVisiveis = 0;
        for (int i = 0; i < pvgMapaEntregas.Fields.Count; i++)
        {
            if (pvgMapaEntregas.Fields[i].Area == PivotArea.DataArea && pvgMapaEntregas.Fields[i].Visible == true)
            {
                hfGeral.Set(pvgMapaEntregas.Fields[i].FieldName, pvgMapaEntregas.Fields[i].AreaIndex);
                if (qtdColunasVisiveis <= pvgMapaEntregas.Fields[i].AreaIndex)
                    qtdColunasVisiveis = pvgMapaEntregas.Fields[i].AreaIndex + 1;
            }
        }
    }*/

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            nomeArquivo = "matrizSuficiencia_" + dataHora + ".xls";
            try
            {
                ASPxPivotGridExporter1.ExportToXls(stream);
            }
            catch
            {
                erro = "S";
            }

            app = "application/ms-excel";

            if (erro == "")
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();
            }
            else
            {
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem(traducao.url_relMatrizSuficienciaProjEstrategicos_erro_ao_exportar_os_dados__verifique_se_n_o_foi_ultrapassado_o_n_mero_m_ximo_de_256_colunas_, 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName == "PossuiObjetivoAssociado")
            {
                if (Convert.ToInt32(e.Value) > 0)
                {
                    e.Appearance.BackColor = System.Drawing.Color.DarkGreen;
                    e.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
                }
                else if (e.DataField.FieldName != "PercentualConcluido")
                {
                    e.Appearance.BackColor = System.Drawing.Color.White;
                    e.Appearance.ForeColor = System.Drawing.Color.White;
                }
            }
        }
    }

    protected void pnCallbackDados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaGrid();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapMatrizSufic", pvgMapaEntregas);
    }

    protected void menu_Init(object sender, EventArgs e)
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

        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapMatrizSufic", Resources.traducao.url_relMatrizSuficienciaProjEstrategicos_olap_matriz_sufici_ncia, this, pvgMapaEntregas);
    }

    #endregion
}
