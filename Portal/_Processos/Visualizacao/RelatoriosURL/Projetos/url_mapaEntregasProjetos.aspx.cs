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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraPivotGrid;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_mapaEntregasProjetos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    public string alturaTabela;
    public string larguraTabela = "";
    List<string> valores;
    public bool exportaOLAPTodosFormatos = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_PrjRelMapEnt");
        }

        this.Title = cDados.getNomeSistema();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            txtInicio.Value = DateTime.Now.AddDays(-5);
            txtTermino.Value = DateTime.Now.AddDays(5);
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
        }

        cDados.aplicaEstiloVisual(Page);

        //MyEditorsLocalizer.Activate();

        carregaGrid();
        defineAlturaTela();

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 240) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 220) + "px";

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
        string where = "";

        if (txtInicio.Text != "")
        {
            where += " AND he.DataPrevistaEntrega >= CONVERT(DateTime, '" + txtInicio.Text + "', 103)";
        }

        if (txtTermino.Text != "")
        {
            where += " AND he.DataPrevistaEntrega <= CONVERT(DateTime, '" + txtTermino.Text + "', 103)";
        }
        DataSet ds = cDados.getMapaEntregaProjeto(txtInicio.Text, txtTermino.Text, codigoUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), codigoEntidadeUsuarioResponsavel);

        valores = new List<string>(ds.Tables[0].Rows.Count);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            valores.Add(dr["Tarefa"].ToString());
        }

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgMapaEntregas.DataSource = ds.Tables[0];
            pvgMapaEntregas.DataBind();
        }
        
    }
    
    protected void pvgMapaEntregas_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        if (e.DataField.FieldName != "SequenciaHistoricoEntrega" || e.GetFieldValue(pvgMapaEntregas.Fields["SequenciaHistoricoEntrega"]) + "" == "") return;

        DevExpress.Web.ASPxPivotGrid.PivotGridField dfDataPrevistaEntrega = pvgMapaEntregas.Fields["DataPrevistaEntrega"];
        DevExpress.Web.ASPxPivotGrid.PivotGridField dfValorFaturamento = pvgMapaEntregas.Fields["ValorFaturamento"];
        DevExpress.Web.ASPxPivotGrid.PivotGridField dfDataRealEntrega = pvgMapaEntregas.Fields["DataRealEntrega"];

        PivotDrillDownDataSource dataSource = e.CreateDrillDownDataSource();

        string dataPrevistaEntrega = dataSource.GetValue(0, dfDataPrevistaEntrega) + "";
        string dataRealEntrega = dataSource.GetValue(0, dfDataRealEntrega) + "";
        double valorFaturamento = (dataSource.GetValue(0, dfValorFaturamento) + "" != "") ? double.Parse(dataSource.GetValue(0, dfValorFaturamento) + "") : 0;

        if (valorFaturamento > 0)
        {
            e.CellStyle.Border.BorderWidth = 4;
            e.CellStyle.Border.BorderColor = System.Drawing.Color.FromName("#C6C6C6");
        }
        if (dataRealEntrega != "")
        {
            e.CellStyle.BackColor = ColorTranslator.FromHtml("#B0C6FF");
        }
        else
        {
            if (dataPrevistaEntrega != "" && DateTime.Parse(dataPrevistaEntrega) < DateTime.Now)
            {
                e.CellStyle.BackColor = System.Drawing.Color.FromName("#FAC19C");
            }
            else
            {
                e.CellStyle.BackColor = System.Drawing.Color.FromName("#ACFFB0");
            }
        }
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        bool campoDataRealvisivel = pvgMapaEntregas.Fields["fieldDataRealEntrega"].Visible;
        bool campoDataPrevistavisivel = pvgMapaEntregas.Fields["fieldDataPrevistaEntrega"].Visible;

        PivotArea areaInicialDataReal = pvgMapaEntregas.Fields["fieldDataRealEntrega"].Area;
        PivotArea areaInicialDataPrevista = pvgMapaEntregas.Fields["fieldDataRealEntrega"].Area;

        pvgMapaEntregas.Fields["fieldDataPrevistaEntrega"].Area = PivotArea.ColumnArea;
        pvgMapaEntregas.Fields["fieldDataRealEntrega"].Area = PivotArea.ColumnArea;

        pvgMapaEntregas.Fields["fieldDataRealEntrega"].Visible = true;
        pvgMapaEntregas.Fields["fieldDataPrevistaEntrega"].Visible = true;


        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";


            nomeArquivo = "mapaEntrega_" + dataHora + ".xls";
            try
            {
                XlsExportOptionsEx x = new XlsExportOptionsEx();
                x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                ASPxPivotGridExporter1.ExportToXls(stream, x);
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
                                    window.top.mostraMensagem(traducao.url_mapaEntregasProjetos_erro_ao_exportar_os_dados__verifique_se_n_o_foi_ultrapassado_o_n_mero_m_ximo_de_256_colunas_, 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
        //Tudo isso é pra voltar a area e a visibilidade dos campos do jeito que tava antes
        pvgMapaEntregas.Fields["fieldDataRealEntrega"].Area = areaInicialDataReal;
        pvgMapaEntregas.Fields["fieldDataPrevistaEntrega"].Area = areaInicialDataPrevista;

        pvgMapaEntregas.Fields["fieldDataRealEntrega"].Visible = campoDataRealvisivel;
        pvgMapaEntregas.Fields["fieldDataPrevistaEntrega"].Visible = campoDataPrevistavisivel;


    }

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName == "SequenciaHistoricoEntrega")
            {
                if (Convert.ToInt32(e.Value) > 0)
                {
                    DevExpress.Web.ASPxPivotGrid.PivotGridField dfDataPrevistaEntrega = pvgMapaEntregas.Fields["DataPrevistaEntrega"];
                    DevExpress.Web.ASPxPivotGrid.PivotGridField dfDataRealEntrega = pvgMapaEntregas.Fields["DataRealEntrega"];
                    PivotDrillDownDataSource dataSource = pvgMapaEntregas.CreateDrillDownDataSource(e.ColumnIndex, e.RowIndex, e.RowValue.Index);
                    string dataPrevistaEntrega = dataSource.GetValue(0, dfDataPrevistaEntrega) + "";
                    string dataRealEntrega = dataSource.GetValue(0, dfDataRealEntrega) + "";

                    if (dataRealEntrega != "")
                    {
                        e.Appearance.BackColor = ColorTranslator.FromHtml("#B0C6FF");
                    }
                    else
                    {
                        if (dataPrevistaEntrega != "" && DateTime.Parse(dataPrevistaEntrega) < DateTime.Now)
                        {
                            e.Appearance.BackColor = ColorTranslator.FromHtml("#FAC19C");
                        }
                        else
                        {
                            e.Appearance.BackColor = ColorTranslator.FromHtml("#ACFFB0");
                        }
                    }
                }
            }
        }
    }

    protected void pvgMapaEntregas_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        if (e.Field.FieldName.Equals("Tarefa"))
        {
            e.Result = valores.IndexOf(e.Value1.ToString()) - valores.IndexOf(e.Value2.ToString());
            e.Handled = true;
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

        cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapMpEntrPrj", pvgMapaEntregas);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapMpEntrPrj", Resources.traducao.url_mapaEntregasProjetos_olap_mapa_de_entregas, this, pvgMapaEntregas);
    }

    #endregion
    protected void pvgMapaEntregas_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
    {
        carregaGrid();
    }
    protected void pvgMapaEntregas_FieldValueDisplayText(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldDisplayTextEventArgs e)
    {
        e.DisplayText = string.Format("{0:dd/MM/yyyy}", e.Value);
    }
}
