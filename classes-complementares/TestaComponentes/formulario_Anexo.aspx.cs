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

public partial class formulario_Anexo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void uc_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        lblArquivo.Text = "";
        lblArquivo.Visible = false;
        string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + e.UploadedFile.FileName;
        byte[] imagemBinario = e.UploadedFile.FileBytes;

        FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
        fs.Write(imagemBinario, 0, imagemBinario.Length);
        fs.Close();
        lblArquivo.Text = e.UploadedFile.FileName;
        lblArquivo.Visible = true;
        hf.Set("nomeArquivo", e.UploadedFile.FileName);
    }
}
