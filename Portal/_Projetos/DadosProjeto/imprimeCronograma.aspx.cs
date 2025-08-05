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

public partial class _Projetos_DadosProjeto_imprimeCronograma : System.Web.UI.Page
{
    dados cDados;
    public int codigoEntidadeUsuarioResponsavel = -1;
    private string imprimeDadosLinhaBaseCronograma = "N";

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

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        string marcos = "";
        string naoConcluidas = "";
        string tarefasAtrasadas = "";
        int codigoRecurso = -1;
        int codigoProjeto = -1;
        int versaoLB = -1;
        string nomeProjeto = "";
        string montaNomeImagemParametro = "";
        string dataImpressao = "";
        string nomeRecurso = "";

        DataSet dsAux = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "imprimeDadosLinhaBaseCronograma");
        if (cDados.DataSetOk(dsAux) && cDados.DataTableOk(dsAux.Tables[0]) && dsAux.Tables[0].Rows[0]["imprimeDadosLinhaBaseCronograma"].ToString() == "S")
            imprimeDadosLinhaBaseCronograma = "S";

        if(Request.QueryString["MA"] != null && Request.QueryString["MA"].ToString() + "" != "")
        {
            marcos = Request.QueryString["MA"].ToString();
        }
        if(Request.QueryString["NC"] != null && Request.QueryString["NC"].ToString() + "" != "")
        {
            naoConcluidas = Request.QueryString["NC"].ToString();
        }
        if(Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() + "" != "")
        {
            tarefasAtrasadas = Request.QueryString["TA"].ToString();
        }
        if(Request.QueryString["REC"] != null && Request.QueryString["REC"].ToString() + "" != "")
        {
            codigoRecurso = int.Parse(Request.QueryString["REC"].ToString());
        }
        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() + "" != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }
        if (Request.QueryString["NP"] != null && Request.QueryString["NP"].ToString() != "")
        {
            nomeProjeto = Request.QueryString["NP"].ToString();
        }
        if (Request.QueryString["DI"] != null && Request.QueryString["DI"].ToString() != "")
        {
            dataImpressao = Request.QueryString["DI"].ToString();
        }
        if (Request.QueryString["NR"] != null && Request.QueryString["NR"].ToString() != "")
        {
            nomeRecurso = Request.QueryString["NR"].ToString();
        } 
        if (Request.QueryString["LB"] != null && Request.QueryString["LB"].ToString() + "" != "")
        {
            versaoLB = int.Parse(Request.QueryString["LB"].ToString());
        }

        DataSet dsLogoUnidade = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");
        ASPxBinaryImage image1 = new ASPxBinaryImage();
        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];
                string montaNomeArquivo = "";
                if (image1.ContentBytes != null)
                {
                    string pathArquivo = "logoRelAnalisePerform_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + pathArquivo;
                    montaNomeImagemParametro = @"~\ArquivosTemporarios\" + pathArquivo;
                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();
                    //rel.Parameters["pathArquivo"].Value = montaNomeImagemParametro;
                    pathArquivo = montaNomeImagemParametro;
                }
            }
            catch (Exception ex)
            {
                string mensage = ex.Message;
            }
        }

        int indicaMarco = 0, indicaTarefaResumo = 0, indicaTarefaNaoConcluida = 0, indicaRecursoSelecionado = 0;
        
        if (marcos == "S")
        {
            indicaMarco = 1;
        }

        if (tarefasAtrasadas == "S")
        {
            indicaTarefaResumo = 1;
        }

        if (naoConcluidas == "S")
        {
            indicaTarefaNaoConcluida = 1;
        }

        if (codigoRecurso.ToString() != "-1")
        {
            indicaRecursoSelecionado = 1;
        }

        relImprimeCronograma relatorio = new relImprimeCronograma(codigoProjeto, versaoLB, codigoRecurso, indicaMarco, indicaTarefaResumo, indicaTarefaNaoConcluida, indicaRecursoSelecionado, nomeProjeto, montaNomeImagemParametro, dataImpressao, nomeRecurso, imprimeDadosLinhaBaseCronograma,"");
        ReportViewer1.Report = relatorio;
        ReportViewer1.WritePdfTo(this.Response);
    }
}
