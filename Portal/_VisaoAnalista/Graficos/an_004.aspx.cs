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

public partial class _VisaoAnalista_Graficos_an_004 : System.Web.UI.Page
{
    dados cDados;

    public string alturaTabela = "";
    public string larguraTabela = "";
    int codigoUsuarioLogado, codigoEntidade;
    string formatoTelaPendenciaWorkflows = "";

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
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "formatoTelaPendenciaWorkflows");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["formatoTelaPendenciaWorkflows"].ToString() == "SGDA")
        {
            formatoTelaPendenciaWorkflows = "SGDA";
        }


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
        DataSet ds = cDados.getAprovacoesPainelAnalista(codigoEntidade, codigoUsuarioLogado, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            float tarefas, workflow, workflowAtrasados;

            tarefas = float.Parse(dt.Rows[0]["Total"].ToString());
            workflow = float.Parse(dt.Rows[1]["Total"].ToString());
            workflowAtrasados = float.Parse(dt.Rows[1]["Atrasados"].ToString());

            constroiMensagemTarefas(tarefas);
            constroiMensagemWorkflow(workflow, workflowAtrasados);
        }
    }

    private void constroiMensagemTarefas(float total)
    {
        switch ((int)total)
        {
            case 0: lkTarefas.Text = string.Format("Não há tarefas de seus recursos para aprovação");
                break;
            case 1: lkTarefas.Text = string.Format("Seus recursos enviaram <a href='#' onclick='abreTarefas();'>1</a> <a class='SF'>tarefa para aprovação</a>");
                break;
            default: lkTarefas.Text = string.Format("Seus recursos enviaram <a href='#' onclick='abreTarefas();'>{0:n0}</a> <a class='SF'>tarefas para aprovação</a>", total);
                break;
        }
    }

    private void constroiMensagemWorkflow(float total, float atrasados)
    {
        switch ((int)total)
        {
            case 0: lkFluxos.Text = string.Format("Você não tem pendências de workflow");
                break;
            case 1: lkFluxos.Text = string.Format("Você tem <a href='#' onclick='abreWorkflow(\"N\",\""+ formatoTelaPendenciaWorkflows +"\", this);'>1</a> <a class='SF'>pendência de workflow</a>");
                break;
            default: lkFluxos.Text = string.Format("Você tem <a href='#' onclick='abreWorkflow(\"N\",\"" + formatoTelaPendenciaWorkflows + "\", this);'>{0:n0}</a> <a class='SF'>pendências de workflow</a>", total);
                break;
        }

        if (atrasados == 1)
            lkFluxos.Text += string.Format("<a class='SF'>, sendo que </a><a href='#' style='Color:red'  onclick='abreWorkflow(\"A\",\"" + formatoTelaPendenciaWorkflows + "\", this);'>1</a> <a class='SF'>está com prazo vencido</a>");
        else if (atrasados > 0)
            lkFluxos.Text += string.Format("<a class='SF'>, sendo que </a><a href='#' style='Color:red'  onclick='abreWorkflow(\"A\",\"" + formatoTelaPendenciaWorkflows + "\", this);'>{0:n0}</a> <a class='SF'>estão com prazo vencido</a>", atrasados);
    }
}
