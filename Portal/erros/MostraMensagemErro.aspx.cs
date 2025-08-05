using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class erros_MostraMensagemErro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Mensagem"] != null && Request.QueryString["Mensagem"].ToString() != "")
        {

            lblMensagem.Text = Server.UrlDecode(Request.QueryString["Mensagem"].ToString());
        }
    }
}