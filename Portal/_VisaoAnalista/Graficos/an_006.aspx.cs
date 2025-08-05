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

public partial class _VisaoAnalista_Graficos_an_006 : System.Web.UI.Page
{
    dados cDados;

    public string alturaTabela = "";
    public string larguraTabela = "";
    int codigoUsuarioLogado, codigoEntidade;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        //Função que gera o gráfico
        geraTabela();
        cDados.aplicaEstiloVisual(this);
    }

    private void defineLarguraTela()
    {
        int largura, altura;


        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            larguraTabela = (largura - 15).ToString();
            ASPxRoundPanel1.Width = (largura - 10);
            ASPxRoundPanel1.ContentHeight = altura - 35;
            alturaTabela = (altura - 60).ToString();
        }
        else
        {
            larguraTabela = ((largura - 30) / 3).ToString();
            ASPxRoundPanel1.Width = ((largura - 20) / 3);
            ASPxRoundPanel1.ContentHeight = (altura - 200) / 2;
            alturaTabela = ((altura - 210) / 2).ToString();
        }

    }

    private void geraTabela()
    {
        DataSet ds = cDados.getComunicacaoPainelAnalista(codigoEntidade, codigoUsuarioLogado, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            float compromissosTotal, compromissosProximosDias, mensagensTotal, mensagensAtrasadas;

            compromissosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            compromissosProximosDias = float.Parse(dt.Rows[0]["Atrasados"].ToString());
            mensagensTotal = float.Parse(dt.Rows[1]["Total"].ToString());
            mensagensAtrasadas = float.Parse(dt.Rows[1]["Atrasados"].ToString());

            constroiMensagemCompromissos(compromissosTotal, compromissosProximosDias);
            constroiMensagemMensagens(mensagensTotal, mensagensAtrasadas);

        }
    }

    private void constroiMensagemCompromissos(float total, float atrasadas)
    {
        switch ((int)total)
        {
            case 0: lkCompromissos.Text = string.Format("Não há compromissos futuros na agenda");
                break;
            case 1: lkCompromissos.Text = string.Format("Há <a href='#' onclick='abreCompromissos(\"N\")'>1</a> <a class='SF'>compromisso futuro na agenda</a>");
                break;
            default: lkCompromissos.Text = string.Format("Há <a href='#' onclick='abreCompromissos(\"N\");'>{0:n0}</a> <a class='SF'>compromissos futuros na agenda</a>", total);
                break;
        }

        if (atrasadas > 0)
            lkCompromissos.Text += string.Format("<a class='SF'>, sendo </a><a href='#' onclick='abreCompromissos(\"A\")'>{0:n0}</a> <a class='SF'>para os próximos 10 dias</a>", atrasadas);
    }

    private void constroiMensagemMensagens(float total, float atrasados)
    {
        switch ((int)total)
        {
            case 0: lkMensagensRecebidas.Text = string.Format("Você não tem mensagens");
                break;
            case 1: lkMensagensRecebidas.Text = string.Format("Você tem <a href='#' onclick='abreMensagensNovas(\"N\");'>1</a> <a class='SF'>mensagem não lida</a>");
                break;
            default: lkMensagensRecebidas.Text = string.Format("Você tem <a href='#' onclick='abreMensagensNovas(\"N\");'>{0:n0}</a> <a class='SF'>mensagens não lidas</a>", total);
                break;
        }
    }
}
