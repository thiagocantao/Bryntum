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

public partial class _VisaoExecutiva_Graficos_ex_004 : System.Web.UI.Page
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
        montaCampos();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        ASPxRoundPanel1.Width = ((largura - 30) / 3);
        ASPxRoundPanel1.ContentHeight = (altura - 295) / 2;
        alturaTabela = ((altura - 295) / 2) + "px";
    }

    private void montaCampos()
    {
        DataSet ds = cDados.getNumeroMensagensExecutivo(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            int numeroMensagensNovas = int.Parse(ds.Tables[0].Rows[0]["MensagensNovas"].ToString());
            int numeroMensagensEnviadaNaoRespondidas = int.Parse(ds.Tables[0].Rows[0]["MensagensNaoRespondidas"].ToString());
            int numeroReunioes = int.Parse(ds.Tables[0].Rows[0]["Reunioes"].ToString());
            int numeroFluxos = int.Parse(ds.Tables[0].Rows[0]["Fluxos"].ToString());

            if (numeroMensagensNovas != 1)
                lkMensagensNovas.Text = string.Format("Você tem {0} mensagens não lidas", numeroMensagensNovas);
            else
                lkMensagensNovas.Text = string.Format("Você tem {0} mensagem não lida", numeroMensagensNovas);
                                  

            if (numeroReunioes != 1)
                lkReunioes.Text = string.Format("Você possui {0} reuniões na semana", numeroReunioes);
            else
                lkReunioes.Text = string.Format("Você possui {0} reunião na semana", numeroReunioes);

            if (numeroFluxos != 1)
                lkFluxos.Text = string.Format("Você precisa fazer {0} aprovações", numeroFluxos);
            else
                lkFluxos.Text = string.Format("Você precisa fazer {0} aprovação", numeroFluxos);

            if (numeroMensagensNovas > 0)
            {
                lkMensagensNovas.Font.Underline = true;
                lkMensagensNovas.Cursor = "pointer";
                lkMensagensNovas.ClientSideEvents.Click = "function(s, e) {abreMensagensNovas();}";
            }

            if (numeroReunioes > 0)
            {
                lkReunioes.NavigateUrl = "~/espacoTrabalho/frameEspacoTrabalho_Agenda.aspx";
                lkReunioes.Target = "_top";
                lkReunioes.Cursor = "pointer";
            }

            if (numeroFluxos > 0)
            {
                lkFluxos.NavigateUrl = "~/espacoTrabalho/PendenciasWorkflow.aspx?IP=S&ATR=N&PND=S";
                lkFluxos.Target = "_top";
                lkFluxos.Cursor = "pointer";
            }

        }
        else
        {
            ASPxRoundPanel1.ClientVisible = false;
        }
    }
}
