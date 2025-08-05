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

public partial class _Estrategias_detalhesObjetivoEstrategico : System.Web.UI.Page
{
    int codigoUnidade = 0;
    int idUsuarioLogado = 0;
    int codigoObjetivoEstrategico = 0;
    dados cDados;

    int alturaPrincipal;
    public string alturaTabela;
    public string alturaLista;
    private string resolucaoCliente;
    DataTable dt;
        
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

        codigoUnidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
        {
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
        }

        cDados.aplicaEstiloVisual(this);

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        //setea la tela
        defineAlturaTela();

        if (!IsPostBack)
        {
            //Carrega a grid
            carregaGridIndicador("");
            carregaGridProjetos("");
            carregaCampos();
            
        }
    }

    private void carregaGridIndicador(string where)
    {
        dt = cDados.getIndicadoresOE(where, codigoObjetivoEstrategico, codigoUnidade, "").Tables[0];

        gridIndicadores.DataSource = dt;
        gridIndicadores.DataBind();
    }

    private void carregaGridProjetos(string where)
    {
        DataSet dsProjetos = cDados.getProjetosOE(codigoObjetivoEstrategico,codigoUnidade, "", "");

        if (cDados.DataSetOk(dsProjetos))
        {
            gridProjetos.DataSource = dsProjetos;

            gridProjetos.DataBind();
        }
    }

    private void carregaCampos()
    {
        dt = cDados.getObjetivoEstrategico(null, codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
        }
        else
        {
            txtPerspectiva.Text = "";
            txtObjetivoEstrategico.Text = "";
            txtMapa.Text = "";
        }
    }


    private void defineAlturaTela()
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = alturaPrincipal - 310;

        gridIndicadores.Settings.VerticalScrollableHeight = altura / 2;
        gridProjetos.Settings.VerticalScrollableHeight = altura / 2;
    }

    protected void gridIndicadores_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        carregaGridIndicador("");
    }
}