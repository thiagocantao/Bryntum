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

public partial class _Portfolios_visaoUnidade : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    int codigoEntidadeUsuarioResponsavel;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================
        
        imgVisao.Style.Add("cursor", "pointer");

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"">var statusVC = 1;</script>"));
                
        carregaComboUnidades();

        if (!IsPostBack)
        {
            cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Items.Count > 0 ? ddlUnidade.Value.ToString() : "-1");            
        }

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        defineLarguraTela();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 150).ToString() + "px";        
    }


    private void carregaComboUnidades()
    {
        DataSet dsUnidades = cDados.getUnidade(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(dsUnidades))
        {
            ddlUnidade.DataSource = dsUnidades;

            ddlUnidade.TextField = "SiglaUnidadeNegocio";

            ddlUnidade.ValueField = "CodigoUnidadeNegocio";

            ddlUnidade.DataBind();

            if (!IsPostBack && cDados.DataTableOk(dsUnidades.Tables[0]))
                ddlUnidade.SelectedIndex = 0;
        }
    }

    protected void callBackVC_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
            cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Items.Count > 0 ? ddlUnidade.Value.ToString() : "-1");
    }    
}
