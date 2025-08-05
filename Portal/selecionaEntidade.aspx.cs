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

public partial class selecionaEntidade : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel;
    public string alturaLista;


    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        /*Busca o nome das unidades que o usuário tem acesso*/
        DataSet ds = cDados.getEntidadesUsuario(codigoUsuarioResponsavel, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuarioResponsavel);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            HyperLink hl = new HyperLink();
            hl.Target = "_parent";
            hl.NavigateUrl = "inicio.aspx?SUN=" + row["SiglaUnidadeNegocio"].ToString() + "&CE=" + row["CodigoUnidadeNegocio"].ToString();
            hl.Text = row["NomeUnidadeNegocio"].ToString() + "<br><br>";
            pnEntidades.Controls.Add(hl);
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(0);
            cDados.insereNivel(0, this);
            Master.geraRastroSite();
        }

        defineAlturaTela();
    }

    private void defineAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        alturaLista = (alturaPrincipal - 160) + "px";
    }
}
