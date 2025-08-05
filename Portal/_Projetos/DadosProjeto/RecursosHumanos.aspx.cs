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
using System.IO;
using DevExpress.XtraPivotGrid;
using System.Collections.Generic;
using DevExpress.Web;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Drawing;

public partial class _Projetos_DadosProjeto_RecursosHumanos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int idProjeto;
    public string alturaTabela;
    public string larguraTabela = "";
    private string bancodb; 
    private string Ownerdb; 
    private int qtdColunasVisiveis = 0;
    public string tipoArquivo = "";

    DataSet ds;
    List<string> valores = new List<string>(12);

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        bancodb = cDados.getDbName();
        Ownerdb = cDados.getDbOwner();

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
        

        valores.Add(Resources.traducao.RecursosHumanos_janeiro);//indice 1
        valores.Add(Resources.traducao.RecursosHumanos_fevereiro);//indice 2
        valores.Add(Resources.traducao.RecursosHumanos_mar_o);
        valores.Add(Resources.traducao.RecursosHumanos_abril);
        valores.Add(Resources.traducao.RecursosHumanos_maio);
        valores.Add(Resources.traducao.RecursosHumanos_junho);
        valores.Add(Resources.traducao.RecursosHumanos_julho);
        valores.Add(Resources.traducao.RecursosHumanos_agosto);
        valores.Add(Resources.traducao.RecursosHumanos_setembro);
        valores.Add(Resources.traducao.RecursosHumanos_outubro);
        valores.Add(Resources.traducao.RecursosHumanos_novembro);
        valores.Add(Resources.traducao.RecursosHumanos_dezembro);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());


        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_AcsTlaRHU");
        }
        pvgRecursosHumanos.Theme = "MaterialCompact";

        pvgRecursosHumanos.OptionsPager.Position = PagerPosition.Bottom;
        pvgRecursosHumanos.OptionsFilter.ShowOnlyAvailableItems = true;

        pvgRecursosHumanos.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Visible;
        pvgRecursosHumanos.OptionsView.VerticalScrollBarMode = ScrollBarMode.Visible;

        //MyEditorsLocalizer.Activate();

        ds = cDados.getDadosRecursosHumanos(idProjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgRecursosHumanos.DataSource = ds.Tables[0];
            pvgRecursosHumanos.DataBind();
        }
        atualizaAreaDados();
        defineAlturaTela();

        string nomeUrlGrafico = "RecursosHumanosGraficos";

        nomeUrlGrafico = "RecursosHumanosCurvaS";
        imgGraficos.ToolTip = Resources.traducao.RecursosHumanos_curva_s_de_aloca__o_de_recursos;
        
        imgGraficos.ClientSideEvents.Click = "function(s, e) {window.location.href = '" + nomeUrlGrafico + ".aspx?idProjeto=" + idProjeto + "';}";
        imgGraficos.Style.Add("cursor", "pointer");
        

        cDados.configuraPainelBotoesOLAP(tbBotoes);

        DataSet dsParametros = cDados.getParametrosSistema("labelDespesa", "labelDespesa_f", "genero_labelDespesa");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelDespesa"].ToString().Trim() != "")
        {

            string genero = dsParametros.Tables[0].Rows[0]["genero_labelDespesa"].ToString() + "";
            string labelDespesaFem = dsParametros.Tables[0].Rows[0]["labelDespesa_f"].ToString() + "";
            string labelDespesa = dsParametros.Tables[0].Rows[0]["labelDespesa"].ToString() + "";


            fieldDespesa.Caption = ((genero.Trim() == "M") || (genero.Trim() == "")) ? labelDespesa : labelDespesaFem;
            fieldDespesaReal.Caption = ((genero.Trim() == "M") || (genero.Trim() == "")) ? labelDespesa + " Real" : labelDespesaFem + " Real";
            fieldDespesaPrevista.Caption = ((genero.Trim() == "M") || (genero.Trim() == "")) ? labelDespesa + " Previsto" : labelDespesaFem + " Prevista";
            fieldDespesaHoraExtra.Caption = ((genero.Trim() == "M") || (genero.Trim() == "")) ? labelDespesa + " Hora Extra" : labelDespesaFem + " Hora Extra";
            fieldDespesaRestante.Caption = ((genero.Trim() == "M") || (genero.Trim() == "")) ? labelDespesa + " Restante" : labelDespesaFem + " Restante";
            fieldVariacaoDaDespesa.Caption = "Variação " + (((genero.Trim() == "M") || (genero.Trim() == "")) ? "do " + labelDespesa : "da " + labelDespesaFem);
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0,resolucaoCliente.IndexOf('x')));
        
        pnDiv.Height = (alturaPrincipal - 230);//a div vai ficar com essa altura
        pnDiv.Width = (larguraPrincipal - 200);

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
                case PivotGridStringId.GrandTotal: return Resources.traducao.RecursosHumanos_total;
                case PivotGridStringId.FilterShowAll: return Resources.traducao.RecursosHumanos_todos;
                case PivotGridStringId.FilterCancel: return Resources.traducao.RecursosHumanos_cancelar;
                case PivotGridStringId.PopupMenuRemoveAllSortByColumn: return Resources.traducao.RecursosHumanos_remover_ordena__o;
                case PivotGridStringId.PopupMenuShowPrefilter: return Resources.traducao.RecursosHumanos_mostrar_pr__filtro;
                case PivotGridStringId.PopupMenuShowFieldList: return Resources.traducao.RecursosHumanos_listar_colunas_dispon_veis;
                case PivotGridStringId.PopupMenuRefreshData: return Resources.traducao.RecursosHumanos_atualizar_dados;
                case PivotGridStringId.PrefilterFormCaption: return Resources.traducao.RecursosHumanos_pr__filtro;
                case PivotGridStringId.PopupMenuCollapse: return Resources.traducao.RecursosHumanos_contrair;
                case PivotGridStringId.PopupMenuCollapseAll: return Resources.traducao.RecursosHumanos_contrair_todos;
                case PivotGridStringId.PopupMenuExpand: return Resources.traducao.RecursosHumanos_expandir;
                case PivotGridStringId.PopupMenuExpandAll: return Resources.traducao.RecursosHumanos_expandir_todos;
                case PivotGridStringId.PopupMenuHideField: return Resources.traducao.RecursosHumanos_remover_coluna;
                case PivotGridStringId.PopupMenuHideFieldList: return Resources.traducao.RecursosHumanos_remover_coluna;
                case PivotGridStringId.PopupMenuHidePrefilter: return Resources.traducao.RecursosHumanos_remover_pr__filtro;
                case PivotGridStringId.RowHeadersCustomization: return Resources.traducao.RecursosHumanos_arraste_as_colunas_que_ir_o_formar_os_agrupamentos_da_consulta;
                case PivotGridStringId.ColumnHeadersCustomization: return Resources.traducao.RecursosHumanos_arraste_as_colunas_que_ir_o_formar_as_colunas_da_consulta;
                case PivotGridStringId.FilterHeadersCustomization: return Resources.traducao.RecursosHumanos_arraste_as_colunas_que_ir_o_formar_os_filtros_da_consulta;
                case PivotGridStringId.DataHeadersCustomization: return Resources.traducao.RecursosHumanos_arraste_as_colunas_que_ir_o_formar_os_dados_da_consulta;
                
                case PivotGridStringId.FilterInvert: return Resources.traducao.RecursosHumanos_inverter_filtro;
                case PivotGridStringId.PopupMenuFieldOrder: return Resources.traducao.RecursosHumanos_ordenar;
                case PivotGridStringId.PopupMenuSortFieldByColumn: return Resources.traducao.RecursosHumanos_ordenar_pela_coluna__0_;
                case PivotGridStringId.PopupMenuSortFieldByRow: return Resources.traducao.RecursosHumanos_ordenar_pela_linha__0_;
                case PivotGridStringId.CustomizationFormCaption: return Resources.traducao.RecursosHumanos_colunas_dispon_veis;
                default: return base.GetLocalizedString(id);
            }
        }
    }

    private void atualizaAreaDados()
    {
        hfGeral.Clear();
        qtdColunasVisiveis = 0;
        for (int i = 0; i < pvgRecursosHumanos.Fields.Count; i++)
        {
            if (pvgRecursosHumanos.Fields[i].Area == PivotArea.DataArea && pvgRecursosHumanos.Fields[i].Visible == true)
            {
                hfGeral.Set(pvgRecursosHumanos.Fields[i].FieldName, pvgRecursosHumanos.Fields[i].AreaIndex);
                if (qtdColunasVisiveis <= pvgRecursosHumanos.Fields[i].AreaIndex)
                    qtdColunasVisiveis = pvgRecursosHumanos.Fields[i].AreaIndex + 1;
            }
        }
    }

    protected void pvgRecursosHumanos_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        int incrementaVariacaoCusto = -2;
        int incrementaVariacaoTrabalho = -2;

        if (hfGeral.Contains("VariacaoCusto"))
        {
            incrementaVariacaoCusto = int.Parse(hfGeral.Get("VariacaoCusto").ToString());
        }
        if (hfGeral.Contains("VariacaoTrabalho"))
        {
            incrementaVariacaoTrabalho = int.Parse(hfGeral.Get("VariacaoTrabalho").ToString());
        }


        for (int i = 0; i < pvgRecursosHumanos.ColumnCount; i = i + 1)
        {


            if (((e.ColumnIndex == incrementaVariacaoCusto) && Convert.ToDouble(e.Value) > 0) || ((e.ColumnIndex == incrementaVariacaoTrabalho) && Convert.ToDouble(e.Value) > 0))
                e.CellStyle.ForeColor = System.Drawing.Color.Red;   
            
            if(incrementaVariacaoCusto !=-2)
            {  
                incrementaVariacaoCusto += qtdColunasVisiveis;
            }

            if (incrementaVariacaoTrabalho != -2)
            {
                incrementaVariacaoTrabalho += qtdColunasVisiveis;
            }
        }
    }

    protected void pvgRecursosHumanos_FieldAreaIndexChanged(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldEventArgs e)
    {
        atualizaAreaDados();
    }

    protected void pvgRecursosHumanos_FieldAreaChanged(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldEventArgs e)
    {
        atualizaAreaDados();
    }

    protected void pvgRecursosHumanos_FieldVisibleChanged(object sender, DevExpress.Web.ASPxPivotGrid.PivotFieldEventArgs e)
    {
        atualizaAreaDados();
    }
    protected void pvgRecursosHumanos_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        if (e.Field.FieldName == "Mes")
        {
            object valor1 = valores.IndexOf(e.Value1.ToString());
            object valor2 = valores.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png" && e.Item.Image.Url != "~/imagens/botoes/layout.png")
        {
            cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "RecHumPrj", pvgRecursosHumanos);
        }

    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "RecHumPrj", Resources.traducao.RecursosHumanos_recursos_do_projeto, this, pvgRecursosHumanos);
    }

    #endregion


    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        e.Brick.Style.ForeColor = Color.Black;

        if (e.DataField == field5)
        {
            if (Convert.ToDouble(e.Value) > 0)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }
    }
}
