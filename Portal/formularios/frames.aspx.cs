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

public partial class formularios_frames : System.Web.UI.Page
{
    public string alturaTabela;

    protected void Page_Load(object sender, EventArgs e)
    {
        alturaTabela = "400px";
    }
}
