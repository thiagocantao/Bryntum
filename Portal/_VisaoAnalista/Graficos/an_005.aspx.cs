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

public partial class _VisaoAnalista_Graficos_an_005 : System.Web.UI.Page
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
        int quantidadeDiasAlertaContratos = 60;

        DataSet dsParametros = cDados.getParametrosSistema("QuantidadeDiasAlertaContratosVencimento");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["QuantidadeDiasAlertaContratosVencimento"] + "" != "")
            quantidadeDiasAlertaContratos = int.Parse(dsParametros.Tables[0].Rows[0]["QuantidadeDiasAlertaContratosVencimento"] + "");

        DataSet ds = cDados.getGestaoPainelAnalista(codigoEntidade, codigoUsuarioLogado, quantidadeDiasAlertaContratos, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            float riscosTotal, riscosAtrasados, issuesTotal, issuesAtrasadas, contratosTotal, contratosVencidos;

            riscosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            riscosAtrasados = float.Parse(dt.Rows[0]["Atrasados"].ToString());
            issuesTotal = float.Parse(dt.Rows[1]["Total"].ToString());
            issuesAtrasadas = float.Parse(dt.Rows[1]["Atrasados"].ToString());
            contratosTotal = float.Parse(dt.Rows[2]["Total"].ToString());
            contratosVencidos = float.Parse(dt.Rows[2]["Atrasados"].ToString());

            constroiMensagemRiscos(riscosTotal, riscosAtrasados);
            constroiMensagemIssues(issuesTotal, issuesAtrasadas);
            constroiMensagemContratos(contratosTotal, contratosVencidos, quantidadeDiasAlertaContratos);
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
        string definicaoQuestao = "Questão", definicaoQuestaoPlural = "questões ativas", definicaoQuestaoSingular = "questão ativa";
        string labelCriticaSingular = "crítica", labelCriticaPlural = "críticas";

        DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
        {
            string genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
            definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
            definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
            definicaoQuestaoSingular = string.Format(@"{0} ativ{1}", definicaoQuestao, genero == "M" ? "o" : "a");
            labelCriticaSingular = string.Format("crític{0}", genero == "M" ? "o" : "a");
            labelCriticaPlural = string.Format("crític{0}", genero == "M" ? "os" : "as");
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
            lkIssues.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreIssues(\"A\");'>1</a> <a class='SF'>{0}</a>", labelCriticaSingular.ToLower());
        else if (atrasados > 0)
            lkIssues.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreIssues(\"A\");'>{0:n0}</a> <a class='SF'>{1}</a>", atrasados, labelCriticaPlural.ToLower());
    }

    private void constroiMensagemContratos(float total, float vencidos, int quantidadeDiasAlerta)
    {
        string varLigacao = "e";
        string varUsaContratoEstendido = "N";
        DataSet dsAux = cDados.getParametrosSistema(codigoEntidade, "UtilizaContratosExtendidos");

        if (cDados.DataSetOk(dsAux) && cDados.DataTableOk(dsAux.Tables[0]) && dsAux.Tables[0].Rows[0]["UtilizaContratosExtendidos"].ToString() == "S")
            varUsaContratoEstendido = "S";
         
        switch ((int)total)
        {
            case 0: lkContratos.Text = string.Format("Não há contratos de sua responsabilidade com vencimento nos próximos {0} dias", quantidadeDiasAlerta);
                varLigacao = ", mas";
                break;
            case 1: lkContratos.Text = string.Format("Há <a href='#' onclick='abreContratos(\"N\", {0}, \"{1}\");'>1</a> <a class='SF'>contrato de sua responsabilidade com vencimento nos próximos {0} dias</a>", quantidadeDiasAlerta, varUsaContratoEstendido);
                break;
            default: lkContratos.Text = string.Format("Há <a href='#' onclick='abreContratos(\"N\", {1}, \"{2}\");'>{0:n0}</a> <a class='SF'>contratos de sua responsabilidade com vencimento nos próximos {1} dias</a>", total, quantidadeDiasAlerta, varUsaContratoEstendido);
                break;
        }

        if (vencidos == 1)
            lkContratos.Text += string.Format("<a class='SF'> {0} </a><a href='#' style='Color:red' onclick='abreContratos(\"A\", 0, \"{1}\");'>1</a> <a class='SF'> de sua responsabilidade está vencido</a>", varLigacao, varUsaContratoEstendido);
        else if (vencidos > 0)
            lkContratos.Text += string.Format("<a class='SF'> {1} </a><a href='#' style='Color:red' onclick='abreContratos(\"A\", 0, \"{2}\");'>{0:n0}</a> <a class='SF'> de sua responsabilidade estão vencidos</a>", vencidos, varLigacao, varUsaContratoEstendido);
    }
}
