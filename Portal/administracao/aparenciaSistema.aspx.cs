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

public partial class administracao_aparenciaSistema : System.Web.UI.Page
{
    dados cDados;

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

        dsModelo.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            dsModelo.SelectParameters["CodigoEntidade"].DefaultValue = cDados.getInfoSistema("CodigoEntidade").ToString();
            gvModelo.DataBind();
        }
        defineAlturaTela();
        // cDados.getVisualAlturaMenuPrincipal(cmbModeloVisual.Value != null ? cmbModeloVisual.Value.ToString() : "", ref alturaBarraMenu, ref Master.corFundo);
    }

    private void defineAlturaTela()
    {
        // Calcula a altura da tela
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
       int  alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = alturaPrincipal - 310;

        gvModelo.Settings.VerticalScrollableHeight = altura;
    }

    protected void pnCallBackModelo_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        cDados.aplicaEstiloVisual(Page, cmbModeloVisual.Value.ToString());
        //cDados.setVisual(cmbModeloVisual.Value.ToString(), lblSelecione, cmbModeloVisual, gvModelo, btnSalvar);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        cDados.salvarVisual(cmbModeloVisual.Value.ToString(), int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()));
        cDados.setInfoSistema("IDEstiloVisual", cmbModeloVisual.Value.ToString());
    }

    protected void cmbModeloVisual_SelectedIndexChanged(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page, cmbModeloVisual.Value.ToString());
    }
}
