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

public partial class _VisaoIntegrante_Graficos_ie_004 : Page
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

        larguraTabela = ((largura - 30) / 3).ToString();
        ASPxRoundPanel1.Width = ((largura - 20) / 3);
        ASPxRoundPanel1.ContentHeight = (altura - 245) / 2;
        alturaTabela = ((altura - 260) / 2).ToString();

    }

    private void geraTabela()
    {
        DataSet ds = cDados.getAtualizacoesPainelAnalista(codigoEntidade, codigoUsuarioLogado, "", 'L', "AND TerminoReal IS NULL");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            float tarefasTotal, tarefasAtrasadas, indicadoresTotal, indicadoresAtrasados, contratosTotal, contratosAtrasados, toDoList, toDoListAtrasadas;

            tarefasTotal = float.Parse(dt.Rows[0]["Total"].ToString());
            tarefasAtrasadas = float.Parse(dt.Rows[0]["Atrasados"].ToString());
            indicadoresTotal = float.Parse(dt.Rows[1]["Total"].ToString());
            indicadoresAtrasados = float.Parse(dt.Rows[1]["Atrasados"].ToString());
            contratosTotal = float.Parse(dt.Rows[2]["Total"].ToString());
            contratosAtrasados = float.Parse(dt.Rows[2]["Atrasados"].ToString());
            toDoList = float.Parse(dt.Rows[3]["Total"].ToString());
            toDoListAtrasadas = float.Parse(dt.Rows[3]["Atrasados"].ToString());

            constroiMensagemTarefas(tarefasTotal, tarefasAtrasadas, toDoList, toDoListAtrasadas);
            constroiMensagemIndicadores(indicadoresTotal, indicadoresAtrasados);
            constroiMensagemContratos(contratosTotal, contratosAtrasados);
        }
    }

    private void constroiMensagemTarefas(float total, float atrasadas, float toDoList, float toDoListAtrasadas)
    {
        DateTime data = DateTime.Now.AddDays(10);

        total += toDoList;
        atrasadas += toDoListAtrasadas;

        if (total == 0)
        {
            lkTarefas.Text = string.Format("Você não tem tarefas.", data);
            return;
        }

        switch ((int)total)
        {
            case 0:
                lkTarefas.Text = string.Format("Você não tem tarefas.", data);
                break;
            case 1:
                lkTarefas.Text = string.Format("Você tem <a href='#' onclick='abreTarefas(\"N\");'>1</a> <a class='SF'>tarefa", data);
                break;
            default:
                lkTarefas.Text = string.Format("Você tem <a href='#' onclick='abreTarefas(\"N\");'>{0:n0}</a> <a class='SF'>tarefas", total, data);
                break;
        }

        if (atrasadas == 0)
            lkTarefas.Text += ".</a>";
        if (atrasadas == 1)
            lkTarefas.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreTarefas(\"A\");'>1</a> <a class='SF'>atrasada. </a>");
        else if (atrasadas > 0)
            lkTarefas.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreTarefas(\"A\");'>{0:n0}</a> <a class='SF'>atrasadas. </a>", atrasadas);


    }

    private void constroiMensagemIndicadores(float total, float atrasados)
    {
        switch ((int)total)
        {
            case 0:
                lkIndicadores.Text = string.Format("Você não tem indicadores que precisam ser atualizados");
                break;
            case 1:
                lkIndicadores.Text = string.Format("Você tem <a href='#' onclick='abreIndicadores(\"N\");'>1</a> <a class='SF'>indicador que precisa ser atualizado</a>");
                break;
            default:
                lkIndicadores.Text = string.Format("Você tem <a href='#' onclick='abreIndicadores(\"N\");'>{0:n0}</a> <a class='SF'>indicadores que precisam ser atualizados</a>", total);
                break;
        }

        if (atrasados == 1)
            lkIndicadores.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreIndicadores(\"A\");'>1</a> <a class='SF'>atrasado</a>");
        else if (atrasados > 0)
            lkIndicadores.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreIndicadores(\"A\");'>{0:n0}</a> <a class='SF'>atrasados</a>", atrasados);
    }

    private void constroiMensagemContratos(float total, float atrasados)
    {
        double diasParcelas = 0;

        DataSet dsParametro = cDados.getParametrosSistema(codigoEntidade, "diasParcelasVencendo");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]) && dsParametro.Tables[0].Rows[0]["diasParcelasVencendo"].ToString() != "")
            diasParcelas = double.Parse(dsParametro.Tables[0].Rows[0]["diasParcelasVencendo"].ToString());

        DateTime dataVencimento = DateTime.Now.AddDays(diasParcelas);

        switch ((int)total)
        {
            case 0:
                lkContratos.Text = string.Format("Não há lançamentos financeiros vencendo até {0:dd/MM/yyyy}", dataVencimento);
                break;
            case 1:
                lkContratos.Text = string.Format("Há <a href='#' onclick='abreLancamentosFinanceiros(\"N\");'>1</a> <a class='SF'>lançamentos financeiros vencendo até {0:dd/MM/yyyy}</a>", dataVencimento);
                break;
            default:
                lkContratos.Text = string.Format("Há <a href='#' onclick='abreLancamentosFinanceiros(\"N\");'>{0:n0}</a> <a class='SF'>lançamentos financeiros vencendo até {1:dd/MM/yyyy}</a>", total, dataVencimento);
                break;
        }

        if (atrasados == 1)
            lkContratos.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreLancamentosFinanceiros(\"A\");'>1</a> <a class='SF'>atrasado</a>");
        else if (atrasados > 0)
            lkContratos.Text += string.Format("<a class='SF'>, sendo </a><a href='#' style='Color:red' onclick='abreLancamentosFinanceiros(\"A\");'>{0:n0}</a> <a class='SF'>atrasados</a>", atrasados);
    }
}
