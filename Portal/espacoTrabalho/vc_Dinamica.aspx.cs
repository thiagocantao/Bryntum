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
using DevExpress.Web;

public partial class espacoTrabalho_vc_Dinamica : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ASPxPopupControl novoPop = new ASPxPopupControl();
        novoPop.ShowOnPageLoad = true;
        novoPop.Width = 350;
        novoPop.Height = 246;
        novoPop.AllowDragging = true;
        novoPop.AllowResize = true;
        novoPop.CloseAction = DevExpress.Web.CloseAction.CloseButton;
        novoPop.ContentUrl = "../_Projetos/VisaoCorporativa/vc_008.aspx";
        novoPop.ID = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");
        ASPxPanel1.Controls.Add(novoPop);
        //Page.Controls.Add(novoPop);
    }
}
