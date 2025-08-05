using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Text;

public partial class taskboard_TaskboardAgilWrap_v2 : System.Web.UI.Page
{
    public dados cDados;

    TaskBoard dsTaskBoard;

    int codigoUsuario;
    public int codigoEntidadeUsuarioResponsavel = 0;
    string nomeUsuario;

    public string codigoProjeto;

    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
    Dictionary<string, string> resource = new Dictionary<string, string>();

    public string idioma;
    public string traducao;

    public string dashboardId = "";

    public string kanban_dias = "0";

    public static DateTime dtInicioPrevisto;
    public static DateTime dtTerminoPrevisto;

    public static string dataHoraAtual = "";
    private DateTime dataAtual;

    public int alturaTela = 768;
    public bool podeManterQuadroKanban = false;
    bool podeVisualizarPainelSprint = false;
    public string baseUrl = "";
    string codigoEntidade;
    string idUsuarioLogado;
    public bool permiteIncluirNovaSprint = false;
    public bool permiteAlterarSprint = false;
    public bool permiteExcluirSprint = false;
    public bool permiteManterEquipe = false;
    public bool permiteManterItensBacklog = false;
    public bool permiteExcluirItensBacklog = false;
    protected void Page_Init(object sender, EventArgs e)
    {
        Master.exibeMenu = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
       
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Title = cDados.getNomeSistema();
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        codigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();
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

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        if (!IsPostBack)
        {
            int nivel = 1;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["NivelNavegacao"]))
                nivel = int.Parse(Request.QueryString["NivelNavegacao"]);
            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            //Master.geraRastroSite();
        }

        this.TH(this.TS("kanban", "taskboardAgil"));

        DataSet ds = cDados.getParametrosSistema("qtdDiasLimiteParaMostrarTarefaKanban");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            kanban_dias = ds.Tables[0].Rows[0]["qtdDiasLimiteParaMostrarTarefaKanban"].ToString();
        }

        cDados.aplicaEstiloVisual(Page);

        bool indicaHttpsViaProxyReverso = false;
        DataSet dsParam1 = cDados.getParametrosSistema("httpsViaProxyReverso");
        if (cDados.DataSetOk(dsParam1) && cDados.DataTableOk(dsParam1.Tables[0]))
        {
            indicaHttpsViaProxyReverso = dsParam1.Tables[0].Rows[0]["httpsViaProxyReverso"].ToString().ToUpper() == "S";
        }

        if (indicaHttpsViaProxyReverso == true)
        {
            baseUrl = "https://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
        }
        else
        {

            bool indicaHttps = false;
            DataSet dsParam = cDados.getParametrosSistema("utilizaProtocoloHttps");
            if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            {
                indicaHttps = dsParam.Tables[0].Rows[0]["utilizaProtocoloHttps"].ToString().ToUpper() == "S";
            }

            /* Experimento Bootstrap */
            if (indicaHttps == true)
            {
                baseUrl = "https://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
            }
            else
            {
                baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
            }
        }
        Session["baseUrl"] = baseUrl;

        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<link rel=""stylesheet"" href=""{0}/Bootstrap/fonts/open-sans/v15/open-sans.css"" />", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<link rel=""stylesheet"" href=""{0}/Bootstrap/fonts/roboto-mono/v5/roboto-mono.css"" />", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/jquery-ui/v1.12.1/jquery-ui.min.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/_Projetos/Agil/taskboard/Content/taskboardAgil_v2.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/_Projetos/Agil/taskboard/Content/variaveis_taskboardAgil.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));

        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery-ui/v1.12.1/jquery-ui.min.js""></script>", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/js-cookie/v2.2.0/js.cookie.js""></script>", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/_Projetos/Agil/taskboard/Scripts/taskboardAgil_v2.js""></script>", baseUrl)));

        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/_Projetos/Agil/taskboard/Content/jquery.mCustomScrollbar.css"" rel=""stylesheet"" />", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/_Projetos/Agil/taskboard/Scripts/jquery.mCustomScrollbar.concat.min.js""></script>", baseUrl)));

        codigoProjeto = Request.QueryString["CP"];

        podeVisualizarPainelSprint = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuario, codigoEntidadeUsuarioResponsavel, "PR", "PR_VisPnlSpt");
        permiteExcluirSprint = permiteAlterarSprint = permiteIncluirNovaSprint = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, int.Parse(codigoProjeto), "null", "PR", 0, "null", "PR_ManSpt");
        permiteManterEquipe = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, int.Parse(codigoProjeto), "null", "PR", 0, "null", "PR_EqAgil");
        permiteManterItensBacklog = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, int.Parse(codigoProjeto), "null", "PR", 0, "null", "PR_IteBkl");
        permiteExcluirItensBacklog = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, int.Parse(codigoProjeto), "null", "PR", 0, "null", "PR_IteBkl");


        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaTela = alturaPrincipal;
        int altura = (alturaPrincipal - 290);

        if (Request.QueryString["MostraBotoes"] != null && Request.QueryString["MostraBotoes"].ToString() == "N")
        {
            /*
            imgFullscreen.ClientVisible = false;
            imgBurndown.ClientVisible = false;
            imgReuniaoDiariaSprint.ClientVisible = false;
            */
            altura = altura + 100;
        }

        //conteudoPrincipal.Attributes.CssStyle.Add("height", altura.ToString() + "px");
        dashboardId = GetDashboardId();
        configuraCalendario();

     
        if(!IsPostBack)
        {
            rbOrdemSprints.SelectedIndex = 1;
        }
        var codigosIteracoes = GetCodeIteracaoByProject(codigoProjeto, rbOrdemSprints.Value.ToString());
        StringBuilder sb = new StringBuilder();
        foreach(var iteracao in codigosIteracoes)
        {
            if (iteracaoPossuiItem(iteracao))
                sb.Append(iteracao).Append(",");
        }
        iteracoesCode.Value = sb.ToString() != "" && sb.ToString().Length > 1 ? sb.ToString().Substring(0, sb.ToString().Length -1 ) : "";

    }

    private string GetDashboardId()
    {
        const string ComandoSQL = "SELECT TOP 1 IDDashboard FROM Dashboard WHERE IniciaisControle LIKE 'CARGA_SPRINT'";
        var dataSet = cDados.getDataSet(ComandoSQL);
        var rows = dataSet.Tables[0].AsEnumerable();
        var dataRow = rows.FirstOrDefault();

        if (rows.Count() == 0 || dataRow == null)
            return null;

        return dataRow.Field<Guid>("IDDashboard").ToString();
    }

    private void configuraCalendario()
    {
        string comandosql = string.Format(@"SELECT [CodigoIteracao]
                                                  ,[InicioPrevisto]
                                                  ,[TerminoPrevisto]
                                              FROM {0}.{1}.[Agil_Iteracao] where [CodigoProjetoIteracao] = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        DataSet ds = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            dtInicioPrevisto = DateTime.Parse(ds.Tables[0].Rows[0]["InicioPrevisto"].ToString());
            dtTerminoPrevisto = DateTime.Parse(ds.Tables[0].Rows[0]["TerminoPrevisto"].ToString());
        }

        if (!Page.IsPostBack)
        {
            dataHoraAtual = cDados.classeDados.getDateDB();
            System.Globalization.CultureInfo cultureInfoTmp = System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR");
            dataAtual = DateTime.Parse(dataHoraAtual, cultureInfoTmp);
        }
    }

    private List<string> GetCodeIteracaoByProject(string codigoProjeto, string ordenacao)
    {
        string sql;
        string orderBy = "";
        if (ordenacao == "A" || string.IsNullOrEmpty(ordenacao))
        {
            orderBy = "order by ai.[InicioPrevisto] asc";
        }
        else
        {
            orderBy = "order by ai.[InicioPrevisto] desc";
        }
        #region Comando SQL
        sql = string.Format(@"
 
       SELECT  ROW_NUMBER()over({1}) as seq , 
               ai.[CodigoIteracao]
              ,ai.[CodigoProjetoIteracao]
              ,p.[NomeProjeto] as TituloSprint
              ,ai.[InicioPrevisto] as Inicio
              ,ai.[TerminoPrevisto] as Termino
              ,dbo.f_Agil_GetStatusIteracao(ai.[CodigoIteracao], ai.[DataPublicacaoPlanejamento], ai.[TerminoReal]) as Status 
              ,st.IniciaisStatus
              
              ,(SELECT CASE WHEN    isnull(capacidade,0) = 0 then 'Nenhuma capacidade alocada'          
			                WHEN     Convert(Decimal(10,2),IsNull(Capacidade,0)) = 0 THEN  'Sprint não Ocupada' 
                            ELSE convert(varchar(20), Convert(Decimal(10,2), ((IsNull(Estimativa,0) / Capacidade) * 100) )) + '% da Sprint Ocupada' END 
                  FROM dbo.f_Agil_GetAlocacaoCapacidadeIteracao(ai.[CodigoProjetoIteracao]) ) as OcupacaoSprint
     

	          ,(SELECT CASE WHEN isnull(count(DISTINCT CodigoItem),0) = 0 THEN 'Nenhum item' 
			                WHEN count(DISTINCT CodigoItem) = 1 then '1 item'   
			                ELSE convert(varchar(15),  count(DISTINCT CodigoItem))   + ' itens' end
                 FROM agil_itembacklog aib    
                WHERE aib.CodigoIteracao =  ai.[CodigoIteracao]   
                 and ISNULL(aib.IndicaTarefa, 'N') = 'N' and aib.dataExclusao  IS NULL) as QuantidadeItens   
             
        FROM [Projeto] p
        INNER JOIN LinkProjeto lp on (lp.CodigoProjetoFilho  = p.CodigoProjeto) 
        INNER JOIN [Agil_Iteracao] ai on (ai.CodigoProjetoIteracao = p.CodigoProjeto) 
         LEFT JOIN [Status] st on (st.CodigoStatus = p.CodigoStatusProjeto)
        where lp.CodigoProjetoPai = {0} and p.DataExclusao is null {2}
        {1} ", codigoProjeto, orderBy, "", idUsuarioLogado, codigoEntidade, ordenacao);

        #endregion

        DataTable dt = cDados.getDataSet(sql).Tables[0];
        var codigosIteracoes = new List<string>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                codigosIteracoes.Add(dr["CodigoIteracao"].ToString());
            }
        }
        return codigosIteracoes;
    }
    private bool iteracaoPossuiItem(string codigoIteracao)
    {
        string comandosql = string.Format(@"select * from {0}.{1}.[Agil_ItemBacklog] where CodigoIteracao={2}", cDados.getDbName(), cDados.getDbOwner(), codigoIteracao);

        DataSet ds = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            return true;
        }

        return false;
    }
}

