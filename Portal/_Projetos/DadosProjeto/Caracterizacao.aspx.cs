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
using DevExpress.Web.ASPxTreeList;

public partial class _Projetos_DadosProjeto_Caracterizacao : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;
    public int codigoProjeto = 0;
    public int alturaDivArvore = 0; //para indicar o height de la div que contem a visualização "Em Arvore"
    private int alturaPrincipal = 0;

    string tipoTela = "P";

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

        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());        

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_AltCaract");
        }

        if (Request.QueryString["TipoTela"] != null && Request.QueryString["TipoTela"].ToString() != "")
        {
            tipoTela = Request.QueryString["TipoTela"].ToString();
        }

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaComboCategorias();
        carregaComboResponsaveis();
        carregaListaUnidades();

        if (!IsPostBack)
            preencheCampos();

        if (tipoTela == "D")
        {
            lblCategoria.ClientVisible = false;
            ddlCategoria.ClientVisible = false;
            lblProjeto.Text = "Demanda:";
        }
        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblUnidade.Text = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString() + ":";
        }
    }

    private void preencheCampos()
    {
        DataSet dsDados = cDados.getDadosGeraisProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            string nomeUsuario = "";
            DataTable dt = dsDados.Tables[0];

            txtNomeProjeto.Text = dt.Rows[0]["NomeProjeto"].ToString();
            ddlCategoria.Value = dt.Rows[0]["CodigoCategoria"].ToString();
            ddlGerente.Value = int.Parse(dt.Rows[0]["CodigoGerente"].ToString());
            DataSet dsUsuario = cDados.getUsuarios(" AND CodigoUsuario = " + ddlGerente.Value);
            if (cDados.DataSetOk(dsUsuario) && cDados.DataTableOk(dsUsuario.Tables[0]))
            {
                nomeUsuario = dsUsuario.Tables[0].Rows[0]["NomeUsuario"].ToString();
            }
            ddlGerente.Text = nomeUsuario;
            //ddlGerente.Text = 


            TreeListNode tln = tlUnidades.FindNodeByKeyValue(dt.Rows[0]["CodigoUnidadeNegocio"].ToString());
            
            if (tln != null)
                tln.Focus();
        }
    }

    private void carregaComboCategorias()
    {
        DataSet ds = cDados.getCategoria(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

        ddlCategoria.DataSource = ds;

        ddlCategoria.TextField = "DescricaoCategoria";

        ddlCategoria.ValueField = "CodigoCategoria";

        ddlCategoria.DataBind();

        ListEditItem lei = new ListEditItem(" ", "-1");

        ddlCategoria.Items.Insert(0, lei);
    }

    private void carregaComboResponsaveis()
    {
        //Gerente de projeto
        DataSet ds = cDados.getUsuariosAtivosEntidade(codigoEntidadeUsuarioResponsavel, "");
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["NomeUsuario"] = " ";
            dr["EMail"] = " ";
            dr["CodigoUsuario"] = "-1";

            ds.Tables[0].Rows.InsertAt(dr, 0);
            ddlGerente.DataSource = ds.Tables[0];
            ddlGerente.DataBind();
        }
    }

    private void carregaListaUnidades()
    {
        //Gerente de projeto
        DataSet ds = cDados.getUnidade(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            tlUnidades.DataSource = ds;

            tlUnidades.DataBind();
        }

    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        TreeListNode tln = tlUnidades.FocusedNode;

        string categoria = "";

        if (tipoTela == "D")
        {
            categoria = "NULL";
        }
        else
        {
            categoria = ddlCategoria.Value.ToString();
        }

        bool resultado = cDados.atualizaCaracterizacaoProjeto(codigoProjeto, categoria, int.Parse(tln.Key.ToString()), int.Parse(ddlGerente.Value.ToString()), txtNomeProjeto.Text);

        string statusAtualizacao = (resultado) ? "Dados Atualizados com Sucesso!" : "Erro ao Atualizar os Dados do Projeto";

        string script = @"<script type='text/jscript' language='JavaScript'>";
        if(resultado == true)
        {
            script += "window.top.mostraMensagem('" + statusAtualizacao + "', 'sucesso', false, false, null);";
        }
        else
        {
            script += "window.top.mostraMensagem('" + statusAtualizacao + "', 'erro', true, false, null);";
        }
        
        script += @"</script>";

        ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        preencheCampos();
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            alturaDivArvore = altura - 130;
    }
}
