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
using System.Web.Hosting;
using System.IO;
using DevExpress.Web;

public partial class _Estrategias_Relatorios_popupRelAnaliseDePerformance : System.Web.UI.Page
{
    dados cDados;
    string montaNomeArquivo = "";
    string montaNomeImagemParametro;

    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoUsuarioResponsavel = 0;
    
    public int? codUnidadeSelecionada = -1;
    public int codTipoAssociacao = -1;
    public int codMapa = -1;
    public string dataInicio = "";
    public string dataFim = "";
    public string dataImpressao = "";
    public string tipoAnalise = "";
    public string iniciaisTipoAssociacao = "";


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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_EstPrf");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            /*codUnidadeSelecionada, codTipoAssociacao, codMapa, dteDe.Date.ToString("dd/MM/yyyy"), dteAte.Date.ToString("dd/MM/yyyy")*/
            if (Request.QueryString["CUN"] != null && Request.QueryString["CUN"] + "" != "")
                codUnidadeSelecionada = int.Parse(Request.QueryString["CUN"].ToString());
            if (Request.QueryString["CTA"] != null && Request.QueryString["CTA"] + "" != "")
                codTipoAssociacao = int.Parse(Request.QueryString["CTA"].ToString());
            if (Request.QueryString["CM"] != null && Request.QueryString["CM"] + "" != "")
                codMapa = int.Parse(Request.QueryString["CM"].ToString());
            if (Request.QueryString["DE"] != null && Request.QueryString["DE"] + "" != "")
                dataInicio = Request.QueryString["DE"].ToString();
            if (Request.QueryString["AT"] != null && Request.QueryString["AT"] + "" != "")
                dataFim = Request.QueryString["AT"].ToString();
            if (Request.QueryString["DI"] != null && Request.QueryString["DI"] + "" != "")
                dataImpressao = Request.QueryString["DI"].ToString();
            if (Request.QueryString["TA"] != null && Request.QueryString["TA"] + "" != "")
                tipoAnalise = Request.QueryString["TA"].ToString();
            if (Request.QueryString["INI"] != null && Request.QueryString["INI"] + "" != "")
                iniciaisTipoAssociacao = Request.QueryString["INI"].ToString();

        }
        carregarReportMapaEstrategico();
    }


    private void carregarReportMapaEstrategico()
    {

        //Verificar o mapa estratégico que posee como padrão o usuario.

        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");
        ASPxBinaryImage image1 = new ASPxBinaryImage();

        relAnalisePerformance rel = new relAnalisePerformance(codUnidadeSelecionada, (short?)codTipoAssociacao, codMapa, dataInicio, dataFim, tipoAnalise, codigoEntidadeUsuarioResponsavel, codMapa, iniciaisTipoAssociacao);



        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];

                if (image1.ContentBytes != null)
                {
                    string pathArquivo = "logoRelAnalisePerform_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
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

        ReportViewer1.Report = rel;


        rel.Parameters["pCodigoEntidade"].Value = codigoEntidadeUsuarioResponsavel;
        rel.Parameters["pNomeMapa"].Value = "";
        rel.Parameters["pLogoUnidade"].Value = montaNomeImagemParametro;
        rel.Parameters["pdataImpressao"].Value = dataImpressao;
        ReportViewer1.WritePdfTo(Response);
    }

}
