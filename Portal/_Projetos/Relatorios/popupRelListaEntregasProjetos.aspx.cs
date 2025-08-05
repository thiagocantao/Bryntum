using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Web.Hosting;
using System.IO;

public partial class _Projetos_Relatorios_popupRelListaEntregasProjetos : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoProjeto = -1;
    public bool podeIncluir = false;
    private string where = "";

    public bool gerouDataImpressao = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();
    }

    private string constroiNomeDeArquivo()
    {
        string montaNomeArquivo = "";

        int codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

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

    protected void Page_Load(object sender, EventArgs e)
    {
        string dataGeracaoRel = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["DT"] != null)
            {
                dataGeracaoRel = Request.QueryString["DT"].ToString();
                hfGeral.Set("DataGeracaoRelatorio", dataGeracaoRel);
            }
            if (Request.QueryString["CP"] != null)
            {
                codigoProjeto = int.Parse(Request.QueryString["CP"]);
            }
            if (Request.QueryString["WH"] != null)
            {
                where = Request.QueryString["WH"];
            }
        }

        relListaEntregasProjetos rel = new relListaEntregasProjetos(codigoProjeto, dataGeracaoRel, where);
        //rel.Parameters["pPathLogo"].Value = constroiNomeDeArquivo();
        //rel.Parameters["pDataImpressao"].Value = hfGeral.Get("DataGeracaoRelatorio").ToString();
        ReportViewer1.Report = rel;
        ReportViewer1.WritePdfTo(this.Response);
    }
}