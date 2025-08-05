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
using System.IO;
using System.Web.Hosting;

public partial class _Projetos_Relatorios_relatorioLicoesAprendidas : System.Web.UI.Page
{
    int codigoLicaoAprendida = 0;
    
    dados cDados;
    
    rel_LicoesAprendidas relatorio = new rel_LicoesAprendidas();
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (Request.QueryString["CLA"] != null && Request.QueryString["CLA"].ToString() != "")
        {
            codigoLicaoAprendida = int.Parse(Request.QueryString["CLA"].ToString());
        }
        
        DataSet ds = new DataSet();
        
        ds = cDados.getLicoesAprendidas(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), " AND li.CodigoLicaoAprendida = " + codigoLicaoAprendida);
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            relatorio.Parameters["pDataInclusao"].Value = string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["data"]);
            relatorio.Parameters["pIncluidaPor"].Value = ds.Tables[0].Rows[0]["incluidoPor"].ToString();
            relatorio.Parameters["pTipo"].Value = ds.Tables[0].Rows[0]["tipo"].ToString();
            relatorio.Parameters["pAssunto"].Value = ds.Tables[0].Rows[0]["assunto"].ToString();
            relatorio.Parameters["pNomeProjeto"].Value = ds.Tables[0].Rows[0]["projeto"].ToString();
            relatorio.Parameters["pLicao"].Value = ds.Tables[0].Rows[0]["licao"].ToString();
            relatorio.Parameters["pPathLogo"].Value = constroiNomeDeArquivo();
        }
        
        viewer.Report = relatorio;
        viewer.Visible = false;
        viewer.WritePdfTo(Response);
    }
    
    private string constroiNomeDeArquivo()
    {
        string montaNomeArquivo = "";

        int  codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        byte[] vetorBytes = null;

        DataSet ds = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["LogoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["LogoUnidadeNegocio"].ToString() != "")
            {
                vetorBytes = (byte[])ds.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            }
        }

        ASPxBinaryImage image1 = new ASPxBinaryImage();
        try
        {
            image1.ContentBytes = vetorBytes;

            if (image1.ContentBytes != null)
            {
                montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "/ArquivosTemporarios/" + "logo" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim(' ') + ".png";
                FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                fs.Close();
                fs.Dispose();
            }
        }
        catch
        {

        }
        return montaNomeArquivo;
    }
}
