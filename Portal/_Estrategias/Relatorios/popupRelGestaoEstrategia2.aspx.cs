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
using DevExpress.XtraReports.Web.Localization;
using DevExpress.Utils.Localization.Internal;
using System.IO;
using System.Web.Hosting;
using DevExpress.Web;

public partial class _Estrategias_Relatorios_popupRelGestaoEstrategia2 : System.Web.UI.Page
{
    dados cDados;
    string montaNomeArquivo = "", montaNomeImagemParametro;

    public string alturaDivGrid = "";
    public string larguraDivGrid = "";
    string dataImpressao = "";
    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoUsuarioResponsavel = 0;

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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


        int codMapa = -1;
        string nomeMapa = "";



        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() + "" != "")
        {
            codMapa = int.Parse(Request.QueryString["CM"].ToString());
        }
        if (Request.QueryString["NM"] != null && Request.QueryString["NM"].ToString() + "" != "")
        {
            nomeMapa = Request.QueryString["NM"].ToString();
        }

        int codUnidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet dsLogoUnidade = cDados.getLogoEntidade(codUnidade, "");
        ASPxBinaryImage image1 = new ASPxBinaryImage();

        relGestaoEstrategia2 rel = new relGestaoEstrategia2(codMapa, codUnidade);

        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];

                if (image1.ContentBytes != null)
                {
                    string pathArquivo = "logoRelatorio_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                    ReportViewer1.Report = null;
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + pathArquivo;
                    montaNomeImagemParametro = @"~\ArquivosTemporarios\" + pathArquivo;
                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                string mensage = ex.Message;
            }
        }

        rel.Parameters["pCodigoEntidade"].Value = codigoEntidadeUsuarioResponsavel;
        rel.Parameters["pNomeMapa"].Value = nomeMapa;
        rel.Parameters["pLogoUnidade"].Value = montaNomeImagemParametro;

        if (!hfGeral.Contains("dataImpressao"))
        {
            DataSet dsdata = cDados.getDataSet("select getdate()");
            dataImpressao = dsdata.Tables[0].Rows[0][0].ToString();
            DateTime dt = DateTime.Parse(dataImpressao);
            dataImpressao = "Impresso em: " + dt.ToString("dd/MM/yyyy hh:mm:ss");
            rel.Parameters["pdataImpressao"].Value = dataImpressao;
            hfGeral.Set("dataImpressao", dataImpressao);
        }
        else
        {
            rel.Parameters["pdataImpressao"].Value = hfGeral.Get("dataImpressao").ToString();
        }


        ReportViewer1.Report = rel;
        ReportViewer1.WritePdfTo(Response);
        ReportViewer1.Report = null;

    }



}
