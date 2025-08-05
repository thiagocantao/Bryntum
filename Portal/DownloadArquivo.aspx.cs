using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DownloadArquivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string caminhoArquivo = Request.QueryString["arq"];
        RealizaDownloadArquivo(caminhoArquivo);
    }

    private void RealizaDownloadArquivo(string caminhoArquivo)
    {
        FileInfo fileInfo = new FileInfo(caminhoArquivo);

        if (fileInfo.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.Flush();
            Response.TransmitFile(fileInfo.FullName);
            Response.End();
        }
    }
}