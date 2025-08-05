using System;
using System.Data;
using System.Web;
using System.Globalization;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class espacoTrabalho_popupApontamentosTarefasAprovados : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string idAtribucao = "";

    private string bloqueio = "N";
    public decimal AcumuladoRealizado;

    protected void Page_Init(object sender, EventArgs e)
    {
        var culture = CultureInfo.CurrentCulture;

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        if (Request.QueryString["coa"] != null)
            idAtribucao = Request.QueryString["coa"].ToString();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page);
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        var codigoTipoRecurso = Request.QueryString["ctr"];
        bool ehTelaDeAprovacao = (Request.QueryString["tipotela"] + "" == "APROVACAO");

        string comandoSQL = string.Format(@"
        DECLARE @codigoEntidadeContexto	int
        DECLARE @codigoUsuarioSistema	int
        DECLARE @codigoAtribuicaoRecurso bigint

        SET @codigoEntidadeContexto	= {0}
        SET @codigoUsuarioSistema	= {1}
        SET @codigoAtribuicaoRecurso =  null 

        SELECT CodigoApontamentoAtribuicao,
                CodigoCronogramaProjeto,
                CodigoTarefa,
                CodigoRecursoProjeto,
                DataApontamento,
                CodigoUsuarioApontamento,
                NomeUsuarioApontamento,
                TrabalhoRealInformado,
                TrabalhoRestanteInformado,
                InicioRealInformado,
                TerminoInformado,
                TerminoRealInformado,
                UnidadeAtribuicaoRealInformado,
                CustoUnitarioRealInformado,
                CustoRealInformado,
                ReceitaRealInformado,
                SiglaStatusAnalise,
                DescricaoStatusAnalise,
                DataStatusAnalise,
                CodigoUsuarioStatusAnalise,
                IndicaCancelamentoAutomatico,
                IndicaCancelarDisponivel,
                NomeUsuarioStatusAnalise,
                NomeProjeto,
                NomeTarefa,
                NomeRecurso
	       FROM dbo.f_art_GetHistoricoApontamentoTarefa(@codigoEntidadeContexto, @codigoUsuarioSistema, @codigoAtribuicaoRecurso, 1)



", codigoEntidadeLogada, codigoUsuarioLogado, idAtribucao, ehTelaDeAprovacao ? 1 : 0);

        DataSet ds = cDados.getDataSet(comandoSQL);

        switch (codigoTipoRecurso)
        {
            case "2":
                ((GridViewColumn)gvDados.Columns["UnidadeAtribuicaoRealInformado"]).Visible = true;
                ((GridViewColumn)gvDados.Columns["CustoUnitarioRealInformado"]).Visible = true;
                break;
            case "3":
                ((GridViewColumn)gvDados.Columns["UnidadeAtribuicaoRealInformado"]).Visible = false;
                ((GridViewColumn)gvDados.Columns["CustoUnitarioRealInformado"]).Visible = false;
                break;
            default:
                break;
        }

        gvDados.Settings.ShowFilterRow = true;
        //((GridViewCommandColumn)gvDados.Columns["colunaCancelar"]).Visible = ehTelaDeAprovacao;
        
        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();

    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        var status = e.GetValue("SiglaStatusAnalise").ToString();
        if ("CN" == status || "RP" == status)
        {
            Style estiloTachado = new Style();
            estiloTachado.Font.Strikeout = true;
            e.Cell.ApplyStyle(estiloTachado);

        }
        if ("RP" == status)
        {
            if (coluna.FieldName == "DescricaoStatusAnalise")
            {
                e.Cell.ForeColor = Color.Red;
                e.Cell.Font.Strikeout = false;
            }
        }

        if ("CN" == status)
        {
            if (coluna.FieldName == "DescricaoStatusAnalise")
            {
                e.Cell.Font.Bold = true;
                e.Cell.Font.Strikeout = false;
                string textoTitle = string.Format(@"Cancelado por {0} em {1}.", e.GetValue("NomeUsuarioStatusAnalise"), e.GetValue("DataStatusAnalise"));
                e.Cell.Text = e.GetValue("DescricaoStatusAnalise").ToString() +
                    string.Format(@"<img src=""data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAACXBIWXMAAAsTAAALEwEAmpwYAAABXElEQVQ4jWNgoAUwm35Ly2T67UrjGbfngzCIbTj1piZBjcYz7/IbT7+5xmT6rX8mM27/R8EgsWk3V5tNvMWHT/M9DI1o2Hj6jbtYDTGeemMHuuK1F5+DMbq40ZTr29Bsv64L9CeGwvAF58AY0zu3/+tNu6YDN8Bw0qUF6IqClt//X7T10f/YNQ+wesVg8uW5cAMMJl2+jK4gZeW1/4duAr1w/il2AyZduoQwYMLFO9gUrTn7CIyxGjDx4m2EAX3nTpBqgH7fuWNwA7TbD9dgC0TcBtz6r9N2uBJugFb9DiGDvvO/kBWtu/zq//Wnb8EYxEZx/oRzP/Xr9wugRKVW075ioylX4Yp0Oo6gYEQauPJfq2lvIdbUqNW0Z6LBhPP/sXkH5GygzSDN/Vg1I7yzy0+75cANve6T//X7z4OxXveJ/1ote69r1O/wwasZGag171TUbNzpCcIgNi51AOSQtytPNTc8AAAAAElFTkSuQmCC"" title=""{0}""/>", textoTitle);
            }
        }
    }

    protected void gvDados_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
    {
        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
        {
            AcumuladoRealizado = 0;
        }
        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
        {
            decimal tempAcumuladoRealizado = 0;
            bool retorno = decimal.TryParse(e.GetValue("CustoRealInformado") != null ? e.GetValue("CustoRealInformado").ToString() : "0", out tempAcumuladoRealizado);
            if (retorno)
            {
                var status = e.GetValue("SiglaStatusAnalise").ToString();
                if ("CN" != status && "RP" != status)
                {
                    AcumuladoRealizado += tempAcumuladoRealizado;
                }                   
            }
        }
    }

    protected void gvDados_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if (e.Item.FieldName == "CustoRealInformado")
        {
            e.Text = string.Format(@"{0:c2}", AcumuladoRealizado);
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";
        string comandoSQL = string.Format(@"DECLARE @nRetProc int EXEC @nRetProc = [dbo].[p_art_cancelaApontamento] {0} , {1} , {2}", codigoEntidadeLogada, codigoUsuarioLogado, e.Parameters);

        int qtdRegRetornados = 0;
        try
        {
            bool retorno = cDados.execSQL(comandoSQL, ref qtdRegRetornados);
        }
        catch(Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cpErro"] = ex.Message;
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            bool IndicaCancelarDisponivel = (bool)((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "IndicaCancelarDisponivel");
            if (e.ButtonID.Equals("btnCancelar"))
            {
                if (true == IndicaCancelarDisponivel)
                {
                    e.Enabled = true;
                    e.Text = "Cancelar";
                    e.Image.Url = "~/imagens/botoes/aprovarReprovar.png";
                }
                else
                {
                    e.Enabled = false;
                    e.Text = "Tarefa com apontamentos pendentes de análise";
                    e.Image.Url = "~/imagens/botoes/aprovarReprovarDes.png";
                }
            }
        }
    }
}
