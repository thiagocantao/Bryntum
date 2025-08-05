using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class workflow_dados : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string recebido = Request["name3"];
        string [][] arrayTeste = {new string[] {"25", "34", "87"}, new string[]{"ferreira", "farias", "josemberg"}};
        string completo = String.Join(",", arrayTeste[0]) + "/" + String.Join(",", arrayTeste[1]);
        Response.Write("var1=" + completo + "&var2=" + recebido);
    }
}