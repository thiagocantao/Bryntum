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
using CDIS;

public partial class testaWorkflow : System.Web.UI.Page
{
    dados cDados = new dados();

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        int codigoUsuarioResponsavel = 1;
        int CodigoWorkflow = 614;
        int? CodigoInstanciaWf = null;// 1597;
        int? CodigoEtapaAtual = 1;
        int? OcorrenciaAtual = 1;
        int? CodigoProjeto = null;
        string alturaFrameFluxo = "628";

        string strAdd = "";
        string strReadOnly = "";

        string formularioEdicao = "testaFormulario.aspx?AT=" + alturaFrameFluxo + strAdd + "&CWF=" + CodigoWorkflow + strReadOnly + "&";

        Workflow myFluxo = new Workflow(cDados.classeDados, codigoUsuarioResponsavel, IsPostBack, CodigoWorkflow, CodigoInstanciaWf, CodigoEtapaAtual, OcorrenciaAtual, CodigoProjeto, this.Page);
       // Control pnFluxo = myFluxo.ObtemPainelExecucaoEtapa(CssFilePath, CssPostFix, formularioEdicao, larguraTela, int.Parse(alturaFrameFluxo), urlDestinoAposExecutarAcao);
     //   form1.Controls.Add(pnFluxo);
    }
}
