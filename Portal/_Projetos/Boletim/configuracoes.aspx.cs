using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_Boletim_configuracoes : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    private int codigoUnidade = -1;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

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

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CodigoUnidade"] != null && Request.QueryString["CodigoUnidade"].ToString() != "")
        {
            codigoUnidade = int.Parse(Request.QueryString["CodigoUnidade"].ToString());
        }

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
            carregaGridUsuarios(true);
        }

        
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 225;
    }

    private void carregaGridUsuarios(bool marcarSelecionados)
    {
        if (marcarSelecionados)
        {
            DataSet ds = new DataSet();

            if (cDados.DataSetOk(ds))
            {
                gvDados.DataSource = ds;
                gvDados.DataBind();

                gvDados.Selection.UnselectAll();

                int i = 0;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Selecionado"].ToString() == "S")
                        gvDados.Selection.SelectRow(i);

                    i++;
                }
            }
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGridUsuarios(true);
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        carregaGridUsuarios(false);

        int[] arrayUsuarios = new int[gvDados.GetSelectedFieldValues("CodigoUsuario").Count];

        for (int i = 0; i < arrayUsuarios.Length; i++)
            arrayUsuarios[i] = int.Parse(gvDados.GetSelectedFieldValues("CodigoUsuario")[i].ToString());

        cDados.incluiUsuariosBoletinsUnidade(codigoUnidade, arrayUsuarios);
    }
}