using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

public partial class taskboard_TaskboardWrap : System.Web.UI.Page
{
    public dados cDados;

    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
    Dictionary<string, string> resource = new Dictionary<string, string>();

    public string idioma;

    public string alturaKanban = "600px";

    public string kanban_dias = "0";

    public string indicaTarefasAtrasadasURL = "";

    public string tituloPagina = "Kanban";

    protected void Page_Init(object sender, EventArgs e)
    {
        indicaTarefasAtrasadasURL = Request.QueryString["Atrasadas"] + "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TITULO"] != null)
        {
            tituloPagina = Request.QueryString["Titulo"].ToString();
            if (tituloPagina == "")
            {
                tituloPagina = "Kanban";
            }

        }

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

        this.TH(this.TS("kanban"));

        if (!IsPostBack)
        {
            int nivel = 1;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["NivelNavegacao"]))
                nivel = int.Parse(Request.QueryString["NivelNavegacao"]);
            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
        }

        DataSet ds = cDados.getParametrosSistema("qtdDiasLimiteParaMostrarTarefaKanban");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            kanban_dias = ds.Tables[0].Rows[0]["qtdDiasLimiteParaMostrarTarefaKanban"].ToString();
        }

        cDados.aplicaEstiloVisual(Page);

        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link rel=""stylesheet"" href=""{0}/Bootstrap/fonts/open-sans/v15/open-sans.css"" />", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link rel=""stylesheet"" href=""{0}/Bootstrap/fonts/roboto-mono/v5/roboto-mono.css"" />", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/jquery-ui/v1.12.1/jquery-ui.min.css"" rel=""stylesheet"" type=""text/css"" />", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/bootstrap-select/v1.12.4/css/bootstrap-select.min.css"" rel=""stylesheet"" type=""text/css"" />", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/daterangepicker/v3.0.3/daterangepicker.css"" rel=""stylesheet"" type=""text/css"" />", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/datepicker/v1.8.0/css/bootstrap-datepicker.min.css"" rel=""stylesheet"" type=""text/css"" />", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/Bootstrap/vendor/datepicker/v1.8.0/css/bootstrap-datepicker.standalone.min.css"" rel=""stylesheet"" type=""text/css"" />", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<link href=""{0}/taskboard/Content/taskboard.css"" rel=""stylesheet"" type=""text/css"" />", Master.baseUrl)));

        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery-ui/v1.12.1/jquery-ui.min.js""></script>", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/jquery-ui-touch-punch/v0.2.3/jquery.ui.touch-punch.min.js""></script>", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/js-cookie/v2.2.0/js.cookie.js""></script>", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/bootstrap-select/v1.12.4/js/bootstrap-select.min.js""></script>", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/moment/v2.22.2/moment-with-locales.min.js""></script>", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/daterangepicker/v3.0.3/daterangepicker.js""></script>", Master.baseUrl)));
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/datepicker/v1.8.0/js/bootstrap-datepicker.min.js""></script>", Master.baseUrl)));
        if (Master.idioma == "pt-BR")
        {
            Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/Bootstrap/vendor/datepicker/v1.8.0/locales/bootstrap-datepicker.pt-BR.min.js"" charset=""UTF-8""></script>", Master.baseUrl)));
        }
        Master.FindControl("HeadContent").Controls.Add(cDados.getLiteral(string.Format(@"<script src=""{0}/taskboard/Scripts/taskboard.js""></script>", Master.baseUrl)));
        hfGeral.Set("indicaTarefasAtrasadasURL", indicaTarefasAtrasadasURL);
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
        #region AlturaPrincipal
        string resolucaoCliente;
        if ((cDados.getInfoSistema("ResolucaoClienteReal") != null) && (cDados.getInfoSistema("ResolucaoClienteReal") != ""))
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoClienteReal").ToString();
        }
        else
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        }
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 310);
        if (alturaPrincipal >= 900)
        {
            altura += 50;
        }
        //conteudoPrincipal.Attributes.CssStyle.Add("height", altura.ToString() + "px");
        alturaKanban = altura.ToString() + "px";

        #endregion
    }
}

