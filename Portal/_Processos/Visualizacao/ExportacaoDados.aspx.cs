using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class _Processos_Visualizacao_ExportacaoDados : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        MemoryStream stream = Session["exportStream"] as MemoryStream;
        if (stream != null)
        {
            string fileName = string.Format("Dados_{0:yyyyMMdd_HHmmss}", DateTime.Now);
            string exportType = Request.QueryString["exportType"];
            bool bInline = Convert.ToBoolean(Request.QueryString["bInline"]);
            ExportReport(stream, fileName, exportType, bInline);
        }
        else
            lblMensagem.Visible = true;
    }

    public void ExportReport(MemoryStream stream, string fileName, string fileType, bool inline)
    {
        Response.Clear();
        Response.Buffer = false;
        Response.ContentType = "application/" + fileType;
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        string contentDisposition = String.Format("{0}; filename={1}.{2}", 
            (inline ? "Inline" : "Attachment"), fileName, fileType);
        Response.AddHeader("Content-Disposition", contentDisposition);
        Response.AddHeader("Content-Length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        Response.End();

        stream.Close();
        stream.Dispose();
        Session["exportStream"] = null;
    }
}