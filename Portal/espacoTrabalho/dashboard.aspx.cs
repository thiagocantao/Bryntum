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
using DevExpress.Web;

public partial class espacoTrabalho_dashboard : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    private int codigoEntidade;

    private string[] Meses = { "JAN", "FEV", "MAR", "ABR", "MAI", "JUN", "JUL", "AGO", "SET", "OUT", "NOV", "DEZ" };

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            DateTime auxDt = DateTime.Today;
            auxDt = auxDt.AddDays(-(auxDt.DayOfYear - 1));
            dteInicio.Date = auxDt;
            dteFim.Date = DateTime.Today;
        }

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlIns");
        }

        defineLarguraTela();

        carregaComboOrcamentos();
        carregaComboMesAnoEstagios();

        if (!IsPostBack)
        {
            cDados.setInfoSistema("CodigoStatus", "3");

            if (ddlOrcamento.Items.Count > 0 && cDados.getInfoSistema("CodigoOrcamento") != null)
            {
                if (cDados.getInfoSistema("CodigoOrcamento").ToString() != "-1")
                {
                    ddlOrcamento.Value = cDados.getInfoSistema("CodigoOrcamento").ToString();
                }
            }

            cDados.setInfoSistema("CodigoOrcamento", ddlOrcamento.Items.Count > 0 ? ddlOrcamento.Value.ToString() : "-1");
        }

        carregaComboMesesOrcamento();

        if (!IsPostBack && !IsCallback)
        {
            if (ddlMes.Items.Count > 0 && cDados.getInfoSistema("Mes") != null && cDados.getInfoSistema("Mes").ToString() != "-1")
                ddlMes.Value = cDados.getInfoSistema("Mes").ToString();

            cDados.setInfoSistema("Mes", ddlMes.Items.Count > 0 ? ddlMes.Value.ToString() : "-1");
        }

        cDados.setInfoSistema("DataEstagio", ddlAnoMes.Value + "");
        cDados.setInfoSistema("InicioOlapEstagio", dteInicio.Value + "");
        cDados.setInfoSistema("TerminoOlapEstagio", dteFim.Value + "");

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(0);
            cDados.insereNivel(0, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTitulo.Text, "PNLINS", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 145).ToString() + "px";
    }

    protected void callBackVC_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        cDados.setInfoSistema("CodigoOrcamento", ddlOrcamento.Items.Count > 0 ? ddlOrcamento.Value.ToString() : "-1");
        cDados.setInfoSistema("Mes", ddlMes.Items.Count > 0 ? ddlMes.Value.ToString() : "-1");
    }

    private void carregaComboOrcamentos()
    {
        string where = "AND CodigoStatusMovimentoOrcamento in (1,3) AND CodigoEntidade = " + codigoEntidade;

        DataSet dsOrcamento = cDados.getMovimentoOrcamento(where);

        int codigoOrcamento = -1;

        if (cDados.DataSetOk(dsOrcamento))
        {
            ddlOrcamento.DataSource = dsOrcamento;

            ddlOrcamento.TextField = "DescricaoMovimentoOrcamento";

            ddlOrcamento.ValueField = "CodigoMovimentoOrcamento";

            ddlOrcamento.DataBind();

            DataSet dsCodigo = cDados.getCodigoMovimentoOrcamentoAtual(where);

            if (cDados.DataSetOk(dsCodigo) && cDados.DataTableOk(dsCodigo.Tables[0]))
                codigoOrcamento = int.Parse(dsCodigo.Tables[0].Rows[0]["CodigoOrcamento"].ToString());
        }

        if (!IsPostBack && ddlOrcamento.Items.Count > 0)
        {
            if (codigoOrcamento != -1)
                ddlOrcamento.Value = codigoOrcamento.ToString();
            else
                ddlOrcamento.SelectedIndex = 0;
        }
    }

    private void carregaComboMesesOrcamento()
    {
        int codigoOrcamento = ddlOrcamento.SelectedIndex == -1 ? 0 : int.Parse(ddlOrcamento.Value.ToString());

        DataSet dsMeses = cDados.getMesesOrcamento(codigoOrcamento, "");

        if (cDados.DataSetOk(dsMeses))
        {
            ddlMes.DataSource = dsMeses;

            ddlMes.TextField = "nomeMes";

            ddlMes.ValueField = "Mes";

            ddlMes.DataBind();
        }

        if (!IsPostBack && ddlMes.Items.Count > 0)
        {
            string mesSugestao = obtemMesSugestao();

            if (ddlMes.Items.FindByText(mesSugestao) != null && ddlMes.Items.FindByText(mesSugestao).Index > 0)
                ddlMes.SelectedIndex = ddlMes.Items.FindByText(mesSugestao).Index;
            else
                ddlMes.SelectedIndex = 0;

            ddlMes.JSProperties["cp_index"] = ddlMes.SelectedIndex.ToString();
        }

    }

    private string obtemMesSugestao()
    {
        DateTime data = DateTime.Today;

        // mostra dezembro enquanto estiver no mês de janeiro
        if (data.Month == 1)
            data = data.AddMonths(-1);   // volta a data para o mês anterior
        else
        {
            // se não for janeiro, volta a data para o mês anterior, se o dia for anterior ao dia do fechamento
            int diaFechamento = 22;
            DataSet ds = cDados.getParametrosSistema("Financeiro_DiaFechamentoMes");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))

                // se não conseguir achar o parâmetro, assume dia 22
                if (!int.TryParse(ds.Tables[0].Rows[0]["Financeiro_DiaFechamentoMes"].ToString(), out diaFechamento))
                    diaFechamento = 22;

            // se o dia atual for anterior ao parâmetro do dia do fechamento financeiro
            if (data.Day <= diaFechamento)
                data = data.AddMonths(-1);   // volta a data para o mês anterior
        }
        return Meses[data.Month-1];
    }

    protected void ddlMes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (ddlMes.Items.Count > 0)
        {
            string mesSugestao = obtemMesSugestao();

            if (ddlMes.Items.FindByText(mesSugestao) != null && ddlMes.Items.FindByText(mesSugestao).Index > 0)
                ddlMes.SelectedIndex = ddlMes.Items.FindByText(mesSugestao).Index;
            else
                ddlMes.SelectedIndex = 0;

            ddlMes.JSProperties["cp_index"] = ddlMes.SelectedIndex.ToString();
        }

    }

    private void carregaComboMesAnoEstagios()
    {
        ddlAnoMes.Items.Clear();

        DataSet ds = cDados.getPrimeiroAnoAtivoEntidade(codigoEntidade, "");

        int primeiroAnoAtivo = DateTime.Now.Year;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["Ano"].ToString() != "")
        {
            primeiroAnoAtivo = int.Parse(ds.Tables[0].Rows[0]["Ano"].ToString());
        }

        for (int i = primeiroAnoAtivo; i <= DateTime.Now.Year; i++)
        {
            int meses = 12;

            if (i == DateTime.Now.Year)
                meses = DateTime.Now.Month;

            for (int j = 1; j <= meses; j++)
            {
                string display = string.Format("{0:MMM/yyyy}", DateTime.Parse(j + "/" + j + "/" + i));
                string value = string.Format("{0:MM/yyyy}", DateTime.Parse(j + "/" + j + "/" + i));
                ListEditItem lei = new ListEditItem(display.ToUpper(), value);
                ddlAnoMes.Items.Insert(0, lei);
            }
        }

        if (!IsPostBack)
            ddlAnoMes.SelectedIndex = 0;
    }
}
