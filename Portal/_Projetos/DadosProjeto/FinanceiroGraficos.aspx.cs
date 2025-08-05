using FusionCharts.Charts;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_RecursosHumanos : System.Web.UI.Page
{
    dados cDados;

    int idProjeto = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string grafico1_titulo = "Financeiro";
    public string grafico1_swf = "../../Flashs/Pie2D.swf";
    public string grafico1_xml = "";
    public string graficoFinanceiro_jsonzoom = "";

    public int alturaGrafico = 220;
    public int larguraGrafico = 350;
    string mostraGrupo = "S";

    public string grafico1_jsonzoom = "";

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

        if (Request.QueryString["MostraGrupo"] != null && Request.QueryString["MostraGrupo"].ToString() != "")
            mostraGrupo = Request.QueryString["MostraGrupo"].ToString();

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        
        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_CnsFin");
        }
        
        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            if (Request.QueryString["Tipo"] != null && Request.QueryString["Tipo"].ToString() != "")
                 ddlTipo.Value = Request.QueryString["Tipo"].ToString();
            else if (cDados.getInfoSistema("Tipo") != null)
                ddlTipo.Value = cDados.getInfoSistema("Tipo").ToString();

            cDados.setInfoSistema("Tipo", ddlTipo.Value.ToString());
        }

        DataSet ds = cDados.getFinanceiroProjeto(idProjeto, ddlTipo.Value.ToString(), "");
        if (cDados.DataSetOk(ds))
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

            graficoFinanceiro_jsonzoom = getGraficoFinanceiroProjetoPizza(ds.Tables[0], "Orçamento do Projeto", 1);
            Session["graficoFinanceiro_jsonzoom"] = graficoFinanceiro_jsonzoom;
            hfGeral.Set("dataSource", graficoFinanceiro_jsonzoom);
            //Literal1.Text = sales.Render();
        }

        defineAlturaTela();

        imgGraficos.ClientSideEvents.Click = "function(s, e) {window.location.href = 'Financeiro.aspx?idProjeto=" + idProjeto + "&MostraGrupo=" + mostraGrupo + "';}";
        imgGraficos.Style.Add("cursor", "pointer");
        lblGrafico.ClientSideEvents.Click = "function(s, e) {window.location.href = 'Financeiro.aspx?idProjeto=" + idProjeto + "&MostraGrupo=" + mostraGrupo + "';}";
        lblGrafico.Style.Add("cursor", "pointer");

        DataSet dsParam = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "labelDespesa");

        string labelDespesa = "Despesa";

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["labelDespesa"].ToString() != "")
            labelDespesa = dsParam.Tables[0].Rows[0]["labelDespesa"].ToString();

        ddlTipo.Items.FindByValue("D").Text = labelDespesa;
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0,resolucaoCliente.IndexOf('x')));

        pRH.ContentHeight = alturaPrincipal - 220;
        pRH.Width = new Unit("100%");

        alturaGrafico = alturaPrincipal - 230;
        larguraGrafico = larguraPrincipal - 200;
    }
    public string getGraficoFinanceiroProjetoPizza(DataTable dt, string titulo, int fonte)
    {
        //Cria as variáveis para a formação do XML
        StringBuilder json = new StringBuilder();

        json.AppendFormat(@"
              
            'chart': {{
                ""caption"" : ""{0}"" ,
                ""showlegend"" : ""0"",
                ""formatNumberScale"" : ""0"",
                ""decimalSeparator"" : "","",
                ""thousandSeparator"" : ""."",
                ""inDecimalSeparator"" : "","",
                ""inThousandSeparator"" : ""."",
                ""showZeroPies"" : ""0"",
                ""decimals"" : ""2"",
                ""chartTopMargin"" : ""5"", 
                ""chartRightMargin"" : ""8"", 
                ""chartBottomMargin"" : ""2"", 
                ""chartLeftMargin"" : ""4"", 
                ""formatNumber"" : ""1"",
                ""showShadow"" : ""0"",  
                ""showBorder"" : ""0"", 
                ""baseFontSize"" : ""8"", 
                ""labelDistance"" : ""1"", 
                ""enableSmartLabels"" : ""0"", 
                ""numberPrefix"" : ""R$"",
                ""legendPosition"": ""bottom"",
                ""theme"": ""fusion"",
                ""bgColor"" : ""F7F7F7"",
                ""borderColor"" : ""A1A1A1"" ,
                ""showToolTip"" : ""0""
            }},
            'data': [", ddlTipo.Text);

        int i = 0;

        for (i = 0; i < dt.Rows.Count; i++)
        {
            json.AppendFormat(@"{{

                    ""label"": ""{0}"",
                    ""value"": ""{1}"" ", dt.Rows[i]["Grupo"].ToString(), dt.Rows[i]["ValorLB"].ToString());
            
            json.AppendFormat(@" }} {0}", (i + 1) == dt.Rows.Count ? "" : ",");
        }
        json.AppendFormat(@"] ");

        return json.ToString();
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        cDados.setInfoSistema("Tipo", ddlTipo.Value.ToString());
    }
}
