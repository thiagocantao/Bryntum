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

public partial class testaFormulario : System.Web.UI.Page
{
    dados cDados = new dados();
    Formulario fx;

    bool readOnly = false;
    Hashtable parametros = new Hashtable();
    string xt, xv;

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        cDados.setInfoSistema("IDUsuarioLogado", 373);
        cDados.setInfoSistema("CodigoEntidade", 111);
        cDados.setInfoSistema("ResolucaoCliente", "1908x945");

        int codigoEntidade = 1;
        int codigoWorkflow = -1;
        int codigoInstanciaWf = -1;
        int? codigoProjeto = null;

        int codigoModeloFormulario = -1;
        codigoModeloFormulario = 677;// 382;// 278;// 174;// 166;
        codigoProjeto = 1026;// 878; // 930;// 564;

        codigoEntidade = 111;
        codigoModeloFormulario = 652;
        codigoProjeto = 1112;

        codigoModeloFormulario = 671;
        codigoProjeto = 1013;

        // geter - caixa hm
        //  codigoEntidade = 1;
        //  codigoModeloFormulario = 1193;
        // codigoProjeto = 49;

        if (Request.QueryString["CWF"] != null)
        {
            //codigoModeloFormulario = 166;
            codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());

            codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());
            codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());

            parametros.Add("CodigoWorkflow", codigoWorkflow.ToString());
            parametros.Add("CodigoInstanciaWorkflow", codigoInstanciaWf.ToString());
            parametros.Add("CodigoEtapaAtual", Request.QueryString["CEWF"].ToString());
            parametros.Add("CodigoOcorrenciaAtual", Request.QueryString["COWF"].ToString());
        }

        // se foi passado o código projeto, seta na variável de sessão
        if (Request.QueryString["CPWF"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CPWF"].ToString());

        if (Request.QueryString["RO"] != null)
            readOnly = Request.QueryString["RO"].ToString() == "S";

        //        parametros.Add("formularioEdicao", Request.ApplicationPath + "/wfRenderizaFormulario_Edicao.aspx");
        parametros.Add("formularioEdicao", "wfRenderizaFormulario_Edicao.aspx");
        parametros.Add("formularioImpressao", "wfRenderizaFormulario_Impressao.aspx");

        // 22 - Formulário (Tipo 1); 16 - Grid (tipo 2); 26 - tela (tipo 3); 

        int? codigoFormulario = null;// 3079;// null;// 3015;// 1597;
        fx = new Formulario(cDados.classeDados, 1, codigoEntidade, codigoModeloFormulario, new Unit("1000px"), new Unit("500px"), readOnly, this, parametros, ref hfSessao, false);

        string xxx = fx.validarModeloFormulario();

        Control ctrl = fx.constroiInterfaceFormulario(false, IsPostBack, codigoFormulario, codigoProjeto, "", "");
        if (ctrl != null)
        {

            form1.Controls.Add(ctrl);

            //Control btnImprimir = this.FindControl("btnImpressao_1"); //pnExterno_btnImpressao_1
        }
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        xt = cmbDotacao.Text;
        xv = cmbDotacao.Value.ToString();
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
       
    }
}
