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

public partial class espacoTrabalho_forumPortal : System.Web.UI.Page
{
    dados cDados;
    public string telaInicial = "";
    public string alturaTabela;
    private string idUsuarioLogado = "";
    private string idUnidadeLogada = "";
    public bool podeConsultarForum = false;

    protected void Page_Init(object sender, EventArgs e)
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

        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        idUnidadeLogada = cDados.getInfoSistema("CodigoEntidade").ToString();
        podeConsultarForum = cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idUnidadeLogada), "CONSFORUM");

        if(podeConsultarForum)
            telaInicial = "./foruns/frameEspacoTrabalho_ForumDiscussao.aspx?Tit=Forúns";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        alturaTabela = getAlturaTela() + "px";
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 125).ToString();
    }
}
