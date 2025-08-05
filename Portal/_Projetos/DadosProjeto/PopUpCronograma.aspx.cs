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

public partial class _Projetos_DadosProjeto_graficos_PopUpCronograma : System.Web.UI.Page
{
    dados cDados;
    string codigoTarefa = "-1";
    int codigoProjeto = 0;
    string dataParam = "";
    public bool mostraCusto = true;

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
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
            pcCronograma.ActiveTabIndex = 0;

        cDados.aplicaEstiloVisual(this);

        codigoTarefa = Request.QueryString["CT"].ToString();
        codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        if (Request.QueryString["Data"].ToString() == "")
        {
            dataParam = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
        }
        else
        {
            dataParam = Request.QueryString["Data"].ToString();
        }

        DataSet dsTemp = cDados.getParametrosSistema("TASQUES_OcultarColunaCusto");
        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["TASQUES_OcultarColunaCusto"].ToString() + "" != "")
        {
            mostraCusto = dsTemp.Tables[0].Rows[0]["TASQUES_OcultarColunaCusto"].ToString() + "" == "S";
        }

        carregaDetalhes();

        gvCustos.Settings.ShowFilterRow = false;
        gvCustos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;

        gvRH.Settings.ShowFilterRow = false;
        gvRH.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    #region Detalhes

    private void carregaDetalhes()
    {
        DataSet dsDetalhes = cDados.getTarefasProjeto(codigoProjeto, dataParam, -1, " AND CodigoTarefa = '" + codigoTarefa + "'", "CodigoTarefa");
        string codigoNumeroTarefa = "-1";

        if (cDados.DataSetOk(dsDetalhes) && cDados.DataTableOk(dsDetalhes.Tables[0]))
        {
            DataTable dt = dsDetalhes.Tables[0];

            txtTarefa.Text = dt.Rows[0]["NomeTarefa"].ToString();
            txtInicioPrevisto.Text = dt.Rows[0]["InicioPrevisto"].ToString() == "" ? "---" : string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Parse(dt.Rows[0]["InicioPrevisto"].ToString()));
            txtInicioReprogramado.Text = dt.Rows[0]["Inicio"].ToString() == "" ? "---" : string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Parse(dt.Rows[0]["Inicio"].ToString()));
            txtInicioReal.Text = dt.Rows[0]["InicioReal"].ToString() == "" ? "---" : string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Parse(dt.Rows[0]["InicioReal"].ToString()));
            txtInicioVar.Text = dt.Rows[0]["VarInicio"] + "" != "" ? string.Format("{0:n0}d", float.Parse(dt.Rows[0]["VarInicio"] + "")) : "--";
            txtTerminoPrevisto.Text = dt.Rows[0]["TerminoPrevisto"].ToString() == "" ? "---" : string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Parse(dt.Rows[0]["TerminoPrevisto"].ToString()));
            txtTerminoReprogramado.Text = dt.Rows[0]["Termino"].ToString() == "" ? "---" : string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Parse(dt.Rows[0]["Termino"].ToString()));
            txtTerminoReal.Text = dt.Rows[0]["TerminoReal"].ToString() == "" ? "---" : string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Parse(dt.Rows[0]["TerminoReal"].ToString()));
            txtTerminoVar.Text = dt.Rows[0]["VarTermino"] + "" != "" ? string.Format("{0:n0}d", float.Parse(dt.Rows[0]["VarTermino"] + "")) : "--";
            txtPercentualReal.Text = dt.Rows[0]["PercentualReal"] + "" != "" ? string.Format("{0:n0}%", float.Parse(dt.Rows[0]["PercentualReal"] + "")) : "--";
            codigoNumeroTarefa = dt.Rows[0]["CodigoNumeroTarefa"] + "" != "" ? dt.Rows[0]["CodigoNumeroTarefa"] + "" : "-1";
        }

        DataSet dsAnotacoes = cDados.getAnotacoesTarefasProjeto(codigoProjeto, " AND CodigoTarefa = '" + codigoNumeroTarefa + "'");

        if (cDados.DataSetOk(dsAnotacoes) && cDados.DataTableOk(dsAnotacoes.Tables[0]))
        {
            DataTable dt = dsAnotacoes.Tables[0];

            if (dt.Rows[0]["AnotacoesTarefa"] + "" != "")
            {
                SautinSoft.RtfToHtml.Converter conv = new SautinSoft.RtfToHtml.Converter();
                txtAnotacao.Html = conv.ConvertString(dt.Rows[0]["AnotacoesTarefa"] + "").Replace("Trial version can convert no more than 30000 symbols.", "").Replace("Get the full featured version!", "");
            }
        }

        try
        {
            int aux = int.Parse(codigoNumeroTarefa);
        }
        catch
        {
            pcCronograma.TabPages[4].ClientVisible = false;
        }

        string urlAnexos = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=TC&ID=" + codigoNumeroTarefa + "&ALT=270";
        pcCronograma.JSProperties["cp_URLAnexos"] = urlAnexos;

        carregaGrids(codigoNumeroTarefa);
    }

    private void carregaGrids(string codigoNumeroTarefa)
    {
        DataSet dsGrids = cDados.getRecursosTarefasProjeto(codigoProjeto, codigoNumeroTarefa, "");
        

        if (cDados.DataSetOk(dsGrids))
        {
            gvCustos.DataSource = dsGrids;
            gvCustos.Columns["Custo"].Visible = mostraCusto;
            gvCustos.Columns["VariacaoCusto"].Visible = mostraCusto;
            gvCustos.DataBind();

            gvRH.DataSource = dsGrids;
            gvRH.DataBind();
        }

        DataSet dsParametro = cDados.getParametrosSistema("labelDespesa");
        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            gvCustos.Columns["Custo"].Caption = dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString();
            gvCustos.Columns["CustoPrevisto"].Caption = dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString() + " Previsto";
            gvCustos.Columns["CustoReal"].Caption = dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString() + " Real";

            pcCronograma.TabPages[2].Text = dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString();
        }
    }

    #endregion
 }
