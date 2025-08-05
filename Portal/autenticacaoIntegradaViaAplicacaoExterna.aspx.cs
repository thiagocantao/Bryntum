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

/// <summary>
/// Página a ser utilizada para autenticar usuários no AD via aplicações Externas
/// Dúvidas? Antonio Carlos.
/// </summary>
public partial class autenticaoIntegradaViaAplicacaoExterna : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        //DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //Pega o usuário logado na rede
        string usuarioRede = User.Identity.Name.ToString();

        // se não conseguiu obter o usuário...
        if (usuarioRede == "")
            lblRetornoIIS.Text = "ERROCDIS;UNI";
        else
        {
            // verifica a aplicação que solicitou a autenticação
            string aplicacaoExterna = Request.Headers["AE_"] == null ? "" : Request.Headers["AE_"];
            if (aplicacaoExterna == "TASQUES")
                autenticaTasques(usuarioRede);
            else
                autenticaOutras(usuarioRede);
        }
    }

    private void autenticaTasques(string usuarioRede)
    {
        string key = "#COMANDO#Tasques!";
        wsTasquesreg oWebService = new wsTasquesreg();
        CdisSoapHeader segurancaWS = new CdisSoapHeader(key);

        lblRetornoIIS.Text = "";
        string nomeUsuario = "";

        try
        {
            segurancaWS.SystemUser = usuarioRede;
            segurancaWS.Key = key;
            segurancaWS.Mac = Request.Headers["MAC_"].ToString();//"Integrado";
            string mensagemRetorno = "";
            bool autenticado = (oWebService.autenticarUsuarioIntegrado(ref nomeUsuario, ref mensagemRetorno, segurancaWS) != "");

            if (autenticado)
            {
                lblRetornoIIS.Text = "OKCDIS;" + segurancaWS.SystemID + ";" + nomeUsuario + ";" + usuarioRede + ";" + segurancaWS.Key2;
            }
        }
        catch (Exception ex)
        {
            lblRetornoIIS.Text = "ERROCDIS;" + usuarioRede + ";" + ex.Message;
        }
    }

    private void autenticaOutras(string usuarioRede)
    {
        lblRetornoIIS.Text = "";

        try
        {
            string nomeUsuario = "";
            string IDEstiloVisual = "";
            string usuarioDeveAlterarSenha = "";
            int codigoUsuario = cDados.getAutenticacaoUsuario(usuarioRede, null, "AI", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

            // se é um usuário válido...
            if (codigoUsuario > 0)
            {
                lblRetornoIIS.Text = "OKCDIS;" + codigoUsuario + ";" + nomeUsuario + ";" + usuarioRede + ";";
            }
        }
        catch (Exception ex)
        {
            lblRetornoIIS.Text = "ERROCDIS;" + usuarioRede + ";" + ex.Message;
        }
    }
}
