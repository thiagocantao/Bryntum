using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UltimoComandoSQL : System.Web.UI.Page
{
    //dados cDados;

    protected void Page_Load(object sender, EventArgs e)
    {
       // cDados = CdadosUtil.GetCdados(null);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtSenha.Text == "rsenha" && Session["UltimoComandoSQL"] != null)
        {
            TextBox1.Text = Session["UltimoComandoSQL"].ToString();
        }
    }
}