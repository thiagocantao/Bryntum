using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class _Projetos_Administracao_propostaDeIniciativa_009 : System.Web.UI.Page
{
    int codigoProjeto = -1;
    dados cDados;

    private int idUsuarioLogado = 0;
    private int codigoEntidade = 0;

    protected void Page_Init(object sender, EventArgs e)
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string np = "";
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        if (Request.QueryString["NP"] != null && Request.QueryString["NP"].ToString() != "")
        {
            np = Request.QueryString["NP"].ToString();
        }

        DataSet dsNomeProjeto = cDados.getDataSet(string.Format(@"SELECT P.NomeProjeto
                                                     FROM Projeto     AS P
                                                     WHERE P.CodigoProjeto = {0}", codigoProjeto));
        if (cDados.DataSetOk(dsNomeProjeto) && cDados.DataTableOk(dsNomeProjeto.Tables[0]))
        {
            np = dsNomeProjeto.Tables[0].Rows[0]["NomeProjeto"].ToString();
        }

        rel_TAI_sesipe relatorio = new rel_TAI_sesipe(codigoProjeto);
        relatorio.Parameters["pNomeIniciativa"].Value = np;
        //ReportViewer1.Report = relatorio;
        

        MemoryStream stream = new MemoryStream();
        relatorio.ExportToPdf(stream);
        
        if (stream != null)
        {
            string fileName = string.Format("Dados_{0:yyyyMMdd_HHmmss}", DateTime.Now);
            string exportType = "pdf";
            bool bInline = Convert.ToBoolean(Request.QueryString["bInline"]);
            ExportReport(stream, fileName, exportType, bInline);
        }

        
    }

    public void ExportReport(MemoryStream stream, string fileName, string fileType, bool inline)
    {
        Response.Clear();
        Response.Buffer = false;
        Response.ContentType = "application/" + fileType;
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        string contentDisposition = String.Format("{0}; filename={1}.{2}",
            (inline ? "Inline" : "Attachment"), fileName, fileType);
        Response.AddHeader("Content-Disposition", contentDisposition);
        Response.AddHeader("Content-Length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        Response.End();

        stream.Close();
        stream.Dispose();



    }

}