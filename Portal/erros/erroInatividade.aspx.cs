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

public partial class erros_erroInatividade : System.Web.UI.Page
{
    public string caminho;
    public string target;
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        string usuarioRede = User.Identity.Name.ToString();

        //usuarioRede = "Sistema-cni\\t-ecantao";

        if (usuarioRede == "")
        {
            caminho = "../login.aspx";
            hfGeral.Set("login", caminho);
           
        }
        else
        {

            /*tenta autenticar o usuário da rede no banco de dados*/
            string nomeUsuario = "";
            string IDEstiloVisual = "";
            string usuarioDeveAlterarSenha = "";

            int codigoUsuario = cDados.getAutenticacaoUsuario(usuarioRede, null, "AI", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

            if (codigoUsuario > 0)
            {
                caminho = "../po_autentica.aspx";
                hfGeral.Set("login", caminho);
            }
            else
            {
                caminho = "../login.aspx";
                hfGeral.Set("login", caminho);
            }
            
        }

        target = "_top";

        if (Request.QueryString["orcamento"] != null)
        {
            caminho = "../Orcamento/orc_index.aspx";
            target = "desktopOrc";
        }
    }
}
