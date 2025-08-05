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

public partial class _Dashboard_VisaoCorporativa_md_006 : System.Web.UI.Page
{
    dados cDados;
    public string alturaTabela = "";

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

        //Função que gera o gráfico
        geraTabela();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        ASPxRoundPanel1.Width = ((largura - 45) / 3);
        ASPxRoundPanel1.ContentHeight = (altura - 180) / 2 - 25;
        alturaTabela = ((altura - 180) / 2 - 26) + "px";
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraTabela()
    {
        string where = "";

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet ds = cDados.getAcoesDashUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            int qtdAprovacoes = int.Parse(dr["Aprovacoes"].ToString());
            int qtdReunioes = int.Parse(dr["Reunioes"].ToString());
            int qtdIndicadores = int.Parse(dr["Indicadores"].ToString());
            int qtdAvisos = int.Parse(dr["Avisos"].ToString());
            if (qtdAprovacoes == 0)
            {
                lkAprovacoes.Text = " Nenhuma Atualização de Fluxo Pendente";
            }
            else
            {
                lkAprovacoes.Text = qtdAprovacoes == 1 ? " 1 Atualização de Fluxo Pendente" : " " + qtdAprovacoes + " Atualizações de Fluxos";
                lkAprovacoes.NavigateUrl = "~/espacoTrabalho/PendenciasWorkflow.aspx?IP=S&ATR=N&PND=S";
                lkAprovacoes.Target = "_top";

            }

            if (qtdReunioes == 0)
            {
                lkReunioes.Text = " Nenhuma Reunião na Semana";
            }
            else
            {
                lkReunioes.Text = qtdReunioes == 1 ? " 1 Reunião na Semana" : " " + qtdReunioes + " Reuniões na Semana";
                lkReunioes.NavigateUrl = "~/espacoTrabalho/frameEspacoTrabalho_Agenda.aspx";
                lkReunioes.Target = "_top";
            }

            if (qtdIndicadores == 0)
            {
                lkIndicadores.Text = " Nenhum Indicador a Atualizar";
            }
            else
            {
                lkIndicadores.Text = qtdIndicadores == 1 ? " 1 Indicador a Atualizar" : " " + qtdIndicadores + " Indicadores a Atualizar";
                lkIndicadores.NavigateUrl = "~/_Estrategias/wizard/atualizacaoResultados.aspx";
                lkIndicadores.Target = "_top";
            }
            if (qtdAvisos == 0)
            {
                hlAvisos.Text = "Nenhum Aviso";
            }
            else
            {
                hlAvisos.Text = qtdAvisos == 1 ? " 1 Aviso não lido " : " " + qtdAvisos + " avisos não lidos";
                hlAvisos.NavigateUrl = "~/espacoTrabalho/frameEspacoTrabalho_Avisos.aspx";
                hlAvisos.Target = "_top";
            }
        }
    }
}