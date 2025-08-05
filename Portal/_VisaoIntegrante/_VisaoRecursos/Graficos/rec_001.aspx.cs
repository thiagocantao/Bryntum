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

public partial class _VisaoAnalista_Graficos_an_003 : System.Web.UI.Page
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
            ASPxRoundPanel1.ContentHeight = altura - 35;
            alturaTabela = (altura - 60).ToString();
        }
        else
        {
            larguraTabela = ((largura - 30) / 3).ToString();
            ASPxRoundPanel1.ContentHeight = (altura - 240) / 2;
            alturaTabela = ((altura - 250) / 2).ToString();
        }

    }

    private void geraTabela()
    {
        DataSet ds = cDados.getPendenciasPainelRecursos(codigoEntidade, codigoUsuarioLogado, "", "AND FaseAtribuicao <> 'FE'");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            float tarefasTotal, tarefasAtrasadas, tarefasAprovacao, riscosTotal, riscosAtrasados, issuesTotal, issuesAtrasadas, mensagensTotal, mensagensAtrasadas;

            tarefasTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            tarefasAtrasadas = float.Parse(dt.Rows[0]["Atrasados"].ToString());
            tarefasAprovacao = float.Parse(dt.Rows[1]["Total"].ToString());
            riscosTotal = float.Parse(dt.Rows[2]["Total"].ToString());
            riscosAtrasados = float.Parse(dt.Rows[2]["Atrasados"].ToString());
            issuesTotal = float.Parse(dt.Rows[3]["Total"].ToString());
            issuesAtrasadas = float.Parse(dt.Rows[3]["Atrasados"].ToString());
            mensagensTotal = float.Parse(dt.Rows[4]["Total"].ToString());
            mensagensAtrasadas = float.Parse(dt.Rows[4]["Atrasados"].ToString());

            constroiMensagemTarefasAtualizacao(tarefasTotal, tarefasAtrasadas);
            constroiMensagemTarefasAprovacao(tarefasAprovacao);
            constroiMensagemRiscos(riscosTotal, riscosAtrasados);
            constroiMensagemIssues(issuesTotal, issuesAtrasadas);
            constroiMensagemMensagens(mensagensTotal, mensagensAtrasadas);
        }
    }

    private void constroiMensagemTarefasAtualizacao(float total, float atrasadas)
    {
        DateTime data = DateTime.Now.AddDays(10);

        if (total == 0)
        {
            lkTarefas.Text = string.Format("Você não tem tarefas.", data);
            return;
        }

        switch ((int)total)
        {
            case 0: lkTarefas.Text = string.Format("Você não tem tarefas.", data);
                break;
            case 1: lkTarefas.Text = string.Format("Você tem <a href='#' onclick='abreTarefasAtualizacao(\"N\");'>1</a> <a class='SF'>tarefa", data);
                break;
            default: lkTarefas.Text = string.Format("Você tem <a href='#' onclick='abreTarefasAtualizacao(\"N\");'>{0:n0}</a> <a class='SF'>tarefas", total, data);
                break;
        }

        if (atrasadas == 0)
            lkTarefas.Text += ".</a>";
        if (atrasadas == 1)
            lkTarefas.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreTarefasAtualizacao(\"A\");'>1</a> <a class='SF'>atrasada. </a>");
        else if (atrasadas > 0)
            lkTarefas.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreTarefasAtualizacao(\"A\");'>{0:n0}</a> <a class='SF'>atrasadas. </a>", atrasadas);

    }

    private void constroiMensagemTarefasAprovacao(float total)
    {
        switch ((int)total)
        {
            case 0: lkTarefasAprovacao.Text = string.Format("Não há tarefas de seus recursos para aprovação");
                break;
            case 1: lkTarefasAprovacao.Text = string.Format("Seus recursos enviaram <a href='#' onclick='abreTarefasAprovacao();'>1</a> <a class='SF'>tarefa para aprovação</a>");
                break;
            default: lkTarefasAprovacao.Text = string.Format("Seus recursos enviaram <a href='#' onclick='abreTarefasAprovacao();'>{0:n0}</a> <a class='SF'>tarefas para aprovação</a>", total);
                break;
        }
    }

    private void constroiMensagemRiscos(float total, float atrasadas)
    {
        switch ((int)total)
        {
            case 0: lkRiscos.Text = string.Format("Você não possui riscos ativos de sua responsabilidade");
                break;
            case 1: lkRiscos.Text = string.Format("Você é responsável por <a href='#' onclick='abreRiscos(\"N\");'>1</a> <a class='SF'>risco ativo</a>");
                break;
            default: lkRiscos.Text = string.Format("Você é responsável por <a href='#' onclick='abreRiscos(\"N\");'>{0:n0}</a> <a class='SF'>riscos ativos</a>", total);
                break;
        }
        if (atrasadas == 1)
            lkRiscos.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreRiscos(\"A\");'>1</a> <a class='SF'>crítico</a>");
        else if (atrasadas > 0)
            lkRiscos.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreRiscos(\"A\");'>{0:n0}</a> <a class='SF'>críticos</a>", atrasadas);
    }

    private void constroiMensagemIssues(float total, float atrasados)
    {
        string definicaoQuestao = "questão", definicaoQuestaoPlural = "questões ativas", definicaoQuestaoSingular = "questão ativa";
        string labelCriticaSingular = "crítica", labelCriticaPlural = "críticas";

        DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestoes");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
        {
            string genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestoes"] + "";
            definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
            definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
            definicaoQuestaoSingular = string.Format(@"{0} ativ{1}", definicaoQuestao, genero == "M" ? "o" : "a");
            labelCriticaSingular = "crítica";
            labelCriticaPlural = "críticas";
        }

        switch ((int)total)
        {
            case 0: lkIssues.Text = string.Format("Você não possui {0} de sua responsabilidade", definicaoQuestaoPlural.ToLower());
                break;
            case 1: lkIssues.Text = string.Format("Você é responsável por <a href='#' onclick='abreIssues(\"N\");'>1</a> <a class='SF'>{0}</a>", definicaoQuestaoSingular.ToLower());
                break;
            default: lkIssues.Text = string.Format("Você é responsável por <a href='#' onclick='abreIssues(\"N\");'>{0:n0}</a> <a class='SF'>{1}</a>", total, definicaoQuestaoPlural.ToLower());
                break;
        }

        if (atrasados == 1)
            lkIssues.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreIssues(\"A\");'>1</a> <a class='SF'>{0}</a>", labelCriticaSingular);
        else if (atrasados > 0)
            lkIssues.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreIssues(\"A\");'>{0:n0}</a> <a class='SF'>{1}</a>", atrasados, labelCriticaPlural);
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
