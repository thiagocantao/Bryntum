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
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

public partial class ReportOutput : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReport report = Session["report"] as XtraReport;

        if (report != null)
            ExportReport(report, "report", Request.QueryString["exportType"], Convert.ToBoolean(Request.QueryString["bInline"]));
        else
            Label1.Visible = true;
    }

    public void ExportReport(XtraReport report, string fileName, string fileType, bool inline)
    {
        MemoryStream stream = new MemoryStream();

        Response.Clear();
        switch (fileType)
        {
            case "xls":
                XlsExportOptionsEx x = new XlsExportOptionsEx();
                x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                report.ExportToXls(stream, x);
                break;
            case "pdf":
                report.ExportToPdf(stream);
                break;
            case "rtf":
                report.ExportToRtf(stream);
                break;
            case "csv":
                report.ExportToCsv(stream);
                break;
            default:
                break;
        }

        Response.ContentType = "application/" + fileType;
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Disposition", (inline ? "Inline" : "Attachment") + "; filename=" + fileName + "." + fileType);
        Response.AddHeader("Content-Length", stream.Length.ToString());
        //Response.ContentEncoding = System.Text.Encoding.Default;
        Response.BinaryWrite(stream.ToArray());
        Response.End();
    }

}
