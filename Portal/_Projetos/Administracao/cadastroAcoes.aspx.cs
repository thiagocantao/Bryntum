using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_Administracao_cadastroAcoes : System.Web.UI.Page
{
    dados cDados;
    int codigoProjeto = -1;
    int codigoAcao = -1;
    int codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel;
    public bool podeIncluir = false;
    string faseTAI = "";

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

        cDados.aplicaEstiloVisual(Page);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        faseTAI = Request.QueryString["FaseTAI"] != null ? Request.QueryString["FaseTAI"].ToString() : "";

        if (!IsPostBack)
        {
            if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "")
            {
                hfCodigoAcao.Set("CodigoAcao", Request.QueryString["CA"].ToString());
            }
            else
            {
                hfCodigoAcao.Set("CodigoAcao", "-1");
            }
        }

        codigoAcao = int.Parse(hfCodigoAcao.Get("CodigoAcao").ToString());

        int sequenciaAcao = 1;

        if (Request.QueryString["SQ"] != null && Request.QueryString["SQ"].ToString() != "")
        {
            sequenciaAcao = codigoAcao == -1 ? int.Parse(Request.QueryString["SQ"].ToString()) : int.Parse(Request.QueryString["SQ"].ToString()) - 1;
        }

        txtNumero.MaxValue = sequenciaAcao;

        if (!IsPostBack && codigoAcao == -1)
            txtNumero.Value = sequenciaAcao;

        carregaComboResponsaveis();

        if(!IsPostBack)
            carregaCampos();

        gvMetasAcao.JSProperties["cp_Msg"] = "";

        carregaGvMetas();

        txtNumero.ClientEnabled = faseTAI != "R";
    }

    private void carregaCampos()
    {
        gvMetasAcao.JSProperties["cp_Retorno"] = "N";

        if (codigoAcao != -1)
        {
            DataSet ds = cDados.getPlanoTrabalhoProjeto(codigoProjeto, " AND t1.CodigoAcao = " + codigoAcao);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow dr = ds.Tables[0].Rows[0];

                txtNumero.Text = dr["Numero"].ToString();
                txtNome.Text = dr["Descricao"].ToString();
                txtCodigoReservado.Text = dr["CodigoReservado"].ToString();
                ddlResponsavel.Value = dr["CodigoUsuarioResponsavel"].ToString();
                ddlFonteRecursos.Value = dr["FonteRecurso"].ToString();
            }
        }
    }

    protected void gvMetasAcao_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "A")
        {
            int numeroAcao = int.Parse(txtNumero.Text);
            string nomeAcao = txtNome.Text;
            string codigoReservado = txtCodigoReservado.Text;
            int codigoResponsavel = int.Parse(ddlResponsavel.Value.ToString());
            string fonteRecursos = ddlFonteRecursos.Value.ToString();
            bool retorno = false;
            

            if (codigoAcao == -1)
            {
                retorno = cDados.incluiAcaoIniciativa(codigoProjeto, numeroAcao, nomeAcao, codigoResponsavel, codigoEntidadeUsuarioResponsavel, fonteRecursos, codigoReservado, faseTAI, ref codigoAcao);
                if (retorno)
                {
                    gvMetasAcao.JSProperties["cp_Msg"] = "Ação Incluída com Sucesso!";
                    gvMetasAcao.JSProperties["cp_Retorno"] = "S";
                }
                else
                {
                    gvMetasAcao.JSProperties["cp_Msg"] = "Erro ao Incluir a Ação!";
                }
            }
            else
            {
                retorno = cDados.atualizaAcaoIniciativa(codigoProjeto, numeroAcao, nomeAcao, codigoResponsavel, fonteRecursos, codigoAcao, codigoReservado);
                
                if (retorno)
                {
                    gvMetasAcao.JSProperties["cp_Msg"] = "Ação Alterada com Sucesso!";
                    gvMetasAcao.JSProperties["cp_Retorno"] = "S";
                }
                else
                {
                    gvMetasAcao.JSProperties["cp_Msg"] = "Erro ao Alterar a Ação!";
                }
            }
        }

        gvMetasAcao.JSProperties["cp_CodigoAcao"] = codigoAcao;

        carregaGvMetas();
    }

    private void carregaComboResponsaveis()
    {
        DataSet ds = cDados.getUsuarioDaEntidadeAtiva(codigoEntidadeUsuarioResponsavel.ToString(), 
                 string.Format(@"AND u.[CodigoUsuario] IN (
						SELECT rc.[CodigoUsuario] FROM {0}.{1}.[RecursoCorporativo] AS [rc] 
						WHERE rc.[CodigoEntidade]					=  {2} 
							AND rc.[CodigoTipoRecurso]			= 1 
							AND rc.[DataDesativacaoRecurso]	IS NULL)", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel.ToString()));

        ddlResponsavel.DataSource = ds;
        ddlResponsavel.DataBind();
    }

    private void carregaGvMetas()
    {
        podeIncluir = codigoAcao != -1;

        DataSet ds = cDados.getMetasPlanoTrabalhoProjeto(codigoProjeto, "AND CodigoAcao = " + codigoAcao);

        gvMetasAcao.DataSource = ds;
        gvMetasAcao.DataBind();
    }

    protected void gvMetasAcao_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        bool retorno = cDados.excluiMetaAcaoIniciativa(int.Parse(e.Keys[0].ToString()));
        
        if (!retorno)
            throw new Exception("Erro ao excluir a meta!");
        else
        {
            gvMetasAcao.JSProperties["cp_Retorno"] = "S";
            gvMetasAcao.JSProperties["cp_Msg"] = "";
            gvMetasAcao.JSProperties["cp_CodigoAcao"] = codigoAcao;
            e.Cancel = true;
            gvMetasAcao.CancelEdit();
            carregaGvMetas();
        }
    }

    protected void gvMetasAcao_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        bool retorno = cDados.incluiMetaAcaoIniciativa(codigoProjeto, codigoAcao, e.NewValues[0] == null ? "" : e.NewValues[0].ToString());

        if (!retorno)
            throw new Exception("Erro ao incluir a meta!");
        else
        {
            gvMetasAcao.JSProperties["cp_Retorno"] = "S";
            gvMetasAcao.JSProperties["cp_Msg"] = "";
            gvMetasAcao.JSProperties["cp_CodigoAcao"] = codigoAcao;
            e.Cancel = true;
            gvMetasAcao.CancelEdit();
            carregaGvMetas();
        }
    }

    protected void gvMetasAcao_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        bool retorno = cDados.atualizaMetaAcaoIniciativa(int.Parse(e.Keys[0].ToString()), e.NewValues[0].ToString());

        if (!retorno)
            throw new Exception("Erro ao editar a meta!");
        else
        {
            gvMetasAcao.JSProperties["cp_Retorno"] = "S";
            gvMetasAcao.JSProperties["cp_Msg"] = "";
            gvMetasAcao.JSProperties["cp_CodigoAcao"] = codigoAcao;
            e.Cancel = true;
            gvMetasAcao.CancelEdit();
            carregaGvMetas();
        }
    }

}
