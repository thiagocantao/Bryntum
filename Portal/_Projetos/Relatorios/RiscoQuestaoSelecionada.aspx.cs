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
using System.Web.Hosting;
using System.IO;
public partial class _Projetos_Relatorios_RiscoQuestaoSelecionada : System.Web.UI.Page
{
    dados cDados;
    rel_RiscosProjetos relatorio;
    private int codigoEntidadeUsuarioResponsavel = 0;
    private string montaNomeArquivo = "";
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
        int codigoRiscoQuestaoSelecionada = int.Parse(Request.QueryString["CRQ"].ToString());
        
        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("RQ");

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        relatorio = new rel_RiscosProjetos(codigoEntidadeUsuarioResponsavel);        
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
        
        relatorio.Parameters["pCodigoRiscoQuestaoSelecionada"].Value = codigoRiscoQuestaoSelecionada;
        relatorio.Parameters["pCodigoTipoAssociacao"].Value = codigoTipoAssociacao;
        relatorio.Parameters["pPathLogo"].Value = montaNomeArquivo;
        relatorio.Parameters["pVetorBytes"].Value = vetorBytes;
        viewer.Report = relatorio;

        viewer.Visible = false;
        viewer.WritePdfTo(Response);
    }
}
