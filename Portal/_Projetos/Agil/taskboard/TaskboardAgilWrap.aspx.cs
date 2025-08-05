using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Drawing;

public partial class taskboard_TaskboardAgilWrap : System.Web.UI.Page
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

    public string kanban_dias = "0";

    public static DateTime dtInicioPrevisto;
    public static DateTime dtTerminoPrevisto;

    public static string dataHoraAtual = "";
    private DateTime dataAtual;

    public int alturaTela = 768;
    bool podeVisualizarPainelSprint = false;
    public bool podeExcluirTarefa = false;
    public bool ehSprint = true;
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
        podeVisualizarPainelSprint = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuario, codigoEntidadeUsuarioResponsavel, "PR", "PR_VisPnlSpt");
        
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
        string baseUrl = "";

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
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/_Projetos/Agil/taskboard/Content/taskboardAgil.css"" rel=""stylesheet"" type=""text/css"" />", baseUrl)));

        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery-ui/v1.12.1/jquery-ui.min.js""></script>", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/js-cookie/v2.2.0/js.cookie.js""></script>", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/_Projetos/Agil/taskboard/Scripts/taskboardAgil.js""></script>", baseUrl)));
        Master.FindControl("cabecalhoPagina").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/_Projetos/Agil/taskboard/Scripts/marvinj-1.0.js""></script>", baseUrl)));
        codigoProjeto = Request.QueryString["CP"];
        podeExcluirTarefa = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidadeUsuarioResponsavel, int.Parse(codigoProjeto), "null", "PR", int.Parse(codigoProjeto), "null", "PR_ExcItSpt");

        DataSet dsEhSprint = cDados.getDataSet(string.Format(@"SELECT [dbo].[f_Agil_Verifica_eh_Sprint] ({0})", codigoProjeto));
        if (cDados.DataSetOk(dsEhSprint))
        {
            ehSprint = dsEhSprint.Tables[0].Rows[0][0].ToString() == "S";
        }
        if (!ehSprint)
        {
            icoReuniaoDiariaSprint.Style.Add("display", "none");
            icoEditarEquipe.Style.Add("display", "none");
            dropdownAssociarItemBacklog.Style.Add("display", "none");
            divKanbanAgilDataSprint.Style.Add("display", "none");
            icoBurndown.Visible = false;
        }


        /*
         * *** AlturaPrincipal ***
         * 
         * No arquivo "\taskboard\Content\taskboard.css"
         * 
         * #conteudoPrincipal {
         *     height: calc(100vh - 130px);
         * }
         *
         * Para que esta página seja 100% responsiva, inclusive na altura, deve-se desabilitar as linhas abaixo que atribuem uma altura fixa ao "div" do conteúdo principal <div id="conteudoPrincipal">.
         * O cálculo da altura está sendo baseado na altura da tela do dispositivo menos 130px, considerando o menu do topo, o rastro e o título da página. Este valor poderá ser revisto.
        */
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
        else
        {
            string dashboardId = GetDashboardId();

            imgFullscreen.ClientSideEvents.Click = "function(s, e){abreFullscreen('taskboard/TaskboardAgilWrap.aspx?MostraBotoes=N&CP=" + codigoProjeto + "', " + (alturaPrincipal - 70) + ");}";
            imgBurndown.ClientSideEvents.Click = "function(s, e){abreGrafico(" + codigoProjeto + ",'" + dashboardId + "');}";
            imgReuniaoDiariaSprint.ClientSideEvents.Click = @"function(s, e){ 
                                                                      if (document.fullscreen)
                                                                             document.exitFullscreen(); 
                                                                  abreReuniaoDiaria(" + codigoProjeto + ");}";

            imgBurndown.Style.Add("visibility", (podeVisualizarPainelSprint == true) ? "visible" : "hidden");


        }

        //conteudoPrincipal.Attributes.CssStyle.Add("height", altura.ToString() + "px");

        configuraCalendario();

    }

    private string GetDashboardId()
    {
        const string ComandoSQL = "SELECT TOP 1 IDDashboard FROM Dashboard WHERE IniciaisControle LIKE 'TASKBOARD'";
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
            calendario.SelectedDate = (DateTime.Compare(dtInicioPrevisto, DateTime.MinValue) == 0) ? DateTime.Now : dtInicioPrevisto;
            System.Globalization.CultureInfo cultureInfoTmp = System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR");
            dataAtual = DateTime.Parse(dataHoraAtual, cultureInfoTmp);
        }
    }

    protected void ASPxCalendar1_DayCellPrepared(object sender, DevExpress.Web.CalendarDayCellPreparedEventArgs e)
    {
        string funcao_javascript = "abreReuniaoDiaria(" + codigoProjeto + "," + e.Date.Day + "," + e.Date.Month + "," + e.Date.Year + "); pcCalendario.Hide();";

        if (dtInicioPrevisto <= e.Date && e.Date <= dtTerminoPrevisto)
        {
            e.Cell.Font.Bold = true;
            if (e.Date <= dataAtual && e.Date.DayOfWeek != DayOfWeek.Sunday)
            {
                e.Cell.Text = @"<a onclick= " + funcao_javascript + " href='javascript:void(0)'>" + e.Date.Day.ToString() + "</a>";

                string whereAux = string.Format(@" AND ev.CodigoTipoAssociacao = (SELECT {0}.{1}.[f_GetCodigoTipoAssociacao]('PR'))   
                                               AND ev.CodigoTipoEvento = (SELECT top 1 CodigoTipoEvento 
                                                                            FROM TipoEvento 
                                                                           WHERE DescricaoTipoEvento like '%Sprint%')
                                               AND  (YEAR(ev.TerminoReal) = {2} 
                                               AND MONTH(ev.TerminoReal) = {3}
                                               AND   DAY(ev.TerminoReal) = {4})
                                               AND ev.CodigoObjetoAssociado = {5}", cDados.getDbName(), cDados.getDbOwner(), e.Date.Year, e.Date.Month, e.Date.Day, codigoProjeto);
                DataSet dsReuniao = cDados.getEventosEntidade(codigoEntidadeUsuarioResponsavel.ToString(), whereAux, codigoUsuario.ToString(), "PR_AdmReu");

                if (cDados.DataSetOk(dsReuniao) && cDados.DataTableOk(dsReuniao.Tables[0]))
                {
                    e.Cell.BackColor = Color.LightGreen;
                    e.Cell.Font.Bold = true;
                }
            }
        }
    }
}

