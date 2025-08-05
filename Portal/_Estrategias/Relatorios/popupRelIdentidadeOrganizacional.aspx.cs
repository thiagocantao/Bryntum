using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using DevExpress.Web;
using System.Web.Hosting;

public partial class _Estrategias_Relatorios_popupRelIdentidadeOrganizacional : System.Web.UI.Page
{
    dados cDados;
    string montaNomeArquivo = "";

    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoMapaEstrategico = 0;
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
        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() + "" != "")
        {
            codigoMapaEstrategico = int.Parse(Request.QueryString["CM"].ToString());
        }

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet dsRelatorioSindicato = cDados.getdsRelIdentidadeOrganizacional(codigoMapaEstrategico);
        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");
        ASPxBinaryImage image1 = new ASPxBinaryImage();
        relIdentidade4 rel = new relIdentidade4();
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];

                if (image1.ContentBytes != null)
                {
                    ReportViewer1.Report = null;
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + "logo" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim(' ') + ".png";

                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();
                    rel.Parameters["pathArquivo"].Value = montaNomeArquivo;
                }
            }
            catch
            {

            }
        }

        if (cDados.DataSetOk(dsRelatorioSindicato) && cDados.DataTableOk(dsRelatorioSindicato.Tables[0]))
        {
            rel.Parameters["missao"].Value = dsRelatorioSindicato.Tables[0].Rows[0]["missao"];
            rel.Parameters["visao"].Value = dsRelatorioSindicato.Tables[0].Rows[0]["visao"];
            rel.Parameters["crencasvalores"].Value = dsRelatorioSindicato.Tables[0].Rows[0]["crencasvalores"];
            rel.Parameters["nomemapa"].Value = Session["nomeMapa"].ToString();
            
        }

        rel.DataSource = cDados.getDadosRelatorioMapa(codigoMapaEstrategico);
        ReportViewer1.Report = rel;
        ReportViewer1.WritePdfTo(Response);

    }
    //nao esquecer de colocar essas funcoes no cdados quando estiver pronto


}