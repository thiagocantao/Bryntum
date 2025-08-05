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

public partial class _Projetos_DadosProjeto_checkinProjetos : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int idProjeto;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;

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
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() + "" != "")
        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

            if (!IsPostBack)
            {
                cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_DesBlq");
            }
        }
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();
    }

    private void populaGrid()
    {
        string where = " AND cp.[CodigoProjeto] = " + idProjeto;
        DataSet ds = cDados.getCronogramasComCheckout(CodigoEntidade, where);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 90;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        int codigoProjeto = (int)gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjeto");
        // ACG: 04/10/2015 - O segundo parametro é para desbloquear cronogramas de replanejamento
        cDados.atualizaCronogramaCheckin(codigoProjeto, "");
        populaGrid();
    }
}
