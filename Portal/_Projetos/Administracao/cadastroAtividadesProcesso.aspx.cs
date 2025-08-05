using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
public partial class _Projetos_Administracao_cadastroAtividadesProcesso : System.Web.UI.Page
{
    dados cDados;
    int codigoProjeto = -1;
    int codigoAcao = -1;
    int codigoAtividade = -1;
    int codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel;
    public bool podeIncluir = false;

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

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "")
        {
            codigoAcao = int.Parse(Request.QueryString["CA"].ToString());
        }

        if (Request.QueryString["NA"] != null && Request.QueryString["NA"].ToString() != "")
        {
            txtNumero.Text = Request.QueryString["NA"].ToString();
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["CodigoAtividade"] != null && Request.QueryString["CodigoAtividade"].ToString() != "")
            {
                hfCodigoAtividade.Set("CodigoAtividade", Request.QueryString["CodigoAtividade"].ToString());
            }
            else
            {
                hfCodigoAtividade.Set("CodigoAtividade", "-1");
            }
        }        

        codigoAtividade = int.Parse(hfCodigoAtividade.Get("CodigoAtividade").ToString());

        podeIncluir = codigoAtividade != -1;

        int sequenciaAcao = 1;

        DataSet ds = cDados.getDataSet(string.Format(@"SELECT ISNULL(COUNT(1), 0) AS QTD
                                                         FROM {0}.{1}.Tai06_AcoesIniciativa
                                                        WHERE CodigoAcaoSuperior = {2}
                                                          AND CodigoAcao <> {2}", cDados.getDbName(), cDados.getDbOwner(), codigoAcao));

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            sequenciaAcao = int.Parse(ds.Tables[0].Rows[0]["QTD"].ToString());

        if (codigoAtividade == -1)
            sequenciaAcao++;

        txtNumeroAtividade.MaxValue = sequenciaAcao;

        if (!IsPostBack && codigoAtividade == -1)
            txtNumeroAtividade.Value = sequenciaAcao;

        ds = cDados.getDataSet(string.Format(@"SELECT FonteRecurso, NumeroAcao
                                                 FROM {0}.{1}.Tai06_AcoesIniciativa
                                                WHERE CodigoAcao = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoAcao));

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["FonteRecurso"].ToString() == "SR")
            {
                ckbSemRecursos.Checked = true;
                ckbSemRecursos.ClientEnabled = false;
            }

            txtNumero.Text = ds.Tables[0].Rows[0]["NumeroAcao"].ToString();
        }

        carregaComboResponsaveis();

        if (!IsPostBack)
            carregaCampos();

        gvParceria.JSProperties["cp_Msg"] = "";
        gvProduto.JSProperties["cp_Msg"] = "";
        gvMarco.JSProperties["cp_Msg"] = "";
        gvParceria.JSProperties["cp_AtualizaGrids"] = "N";

        carregaGvParcerias();
        carregaGvProdutos();
        //carregaGvMetas;
    }

    private void carregaCampos()
    {
        gvParceria.JSProperties["cp_Retorno"] = "N";
        gvProduto.JSProperties["cp_Retorno"] = "N";
        gvMarco.JSProperties["cp_Retorno"] = "N";

        if (codigoAtividade != -1)
        {
            DataSet ds = cDados.getAtividadeAcaoIniciativaProcesso(codigoAtividade, "");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow dr = ds.Tables[0].Rows[0];

                txtNumero.Text = dr["NumeroAcao"].ToString();
                txtNumeroAtividade.Text = dr["NumeroAtividade"].ToString();
                txtNomeAtividade.Text = dr["Descricao"].ToString();
                dataInicio.Value = dr["Inicio"];
                dataTermino.Value = dr["Termino"];
                ddlResponsavel.Value = dr["CodigoUsuarioResponsavel"].ToString();
                ckbSemRecursos.Checked = dr["IndicaSemRecurso"].ToString() == "S";
            }
        }
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

    private void carregaGvParcerias()
    {
        DataSet ds = cDados.getParceriasPlanoTrabalhoProcesso(codigoProjeto, "AND p.CodigoAcao = " + codigoAtividade);

        gvParceria.DataSource = ds;
        gvParceria.DataBind();
    }

    private void carregaGvProdutos()
    {
        DataSet ds = cDados.getMetasPlanoTrabalhoProjetoProcesso(codigoProjeto, "AND CodigoAcao = " + codigoAtividade);

        gvProduto.DataSource = ds;
        gvProduto.DataBind();
    }

    //private void carregaGvMarcos()
    //{
    //    DataSet ds = cDados.getMarcosPlanoTrabalhoProcesso(codigoProjeto, "AND m.CodigoAcao = " + codigoAtividade);

    //    gvMarco.DataSource = ds;
    //    gvMarco.DataBind();
    //}

    protected void gvParceria_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "A")
        {
            int numeroAtividade = int.Parse(txtNumeroAtividade.Text);
            string nomeAtividade = txtNomeAtividade.Text;
            string inicio = (dataInicio.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dataInicio.Date);
            string termino = (dataTermino.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dataTermino.Date);
            int codigoResponsavel = int.Parse(ddlResponsavel.Value.ToString());
            string naoUtilizaRecursos = ckbSemRecursos.Checked ? "S" : "N";
            string indicaSemRecursos = ckbSemRecursos.Checked ? "S" : "N";
            bool retorno = false;

            if (codigoAtividade == -1)
            {
                retorno = cDados.incluiAtividadeAcaoIniciativaProcesso(codigoProjeto, codigoAcao, numeroAtividade, nomeAtividade, inicio
                    , termino, codigoResponsavel, codigoEntidadeUsuarioResponsavel, indicaSemRecursos, ref codigoAtividade);
                if (retorno)
                {
                    gvParceria.JSProperties["cp_Msg"] = "Atividade Incluída com Sucesso!";
                    gvParceria.JSProperties["cp_Retorno"] = "S";
                }
                else
                {
                    gvParceria.JSProperties["cp_Msg"] = "Erro ao Incluir a Atividade!";
                }
            }
            else
            {
                retorno = cDados.atualizaAtividadeAcaoIniciativaProcesso(codigoAtividade, codigoProjeto, codigoAcao, numeroAtividade, nomeAtividade, inicio
                , termino, codigoResponsavel, codigoEntidadeUsuarioResponsavel, indicaSemRecursos);

                if (retorno)
                {
                    gvParceria.JSProperties["cp_Msg"] = "Atividade Alterada com Sucesso!";
                    gvParceria.JSProperties["cp_Retorno"] = "S";
                }
                else
                {
                    gvParceria.JSProperties["cp_Msg"] = "Erro ao Alterar a Atividade!";
                }
            }

            gvParceria.JSProperties["cp_CodigoAtividade"] = codigoAtividade;

            gvParceria.JSProperties["cp_AtualizaGrids"] = "S";

            podeIncluir = codigoAtividade != -1;

            carregaGvParcerias();
        }
    }

    #region BD Parcerias

    protected void gvParceria_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        bool retorno = cDados.excluiParceriaAcaoIniciativaProcesso(int.Parse(e.Keys[0].ToString()));

        if (!retorno)
            throw new Exception("Erro ao excluir o parceiro!");
        else
        {
            gvParceria.JSProperties["cp_Retorno"] = "S";
            e.Cancel = true;
            gvParceria.CancelEdit();
            carregaGvParcerias();
        }
    }

    protected void gvParceria_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string codigoParceiro = (e.NewValues[0] == null) ? "NULL" : e.NewValues[0].ToString();
        string produto = (e.NewValues[1] == null) ? "NULL" : e.NewValues[1].ToString();

        bool retorno = cDados.incluiParceriaAcaoIniciativaProcesso(codigoProjeto, codigoAtividade, codigoParceiro, produto);

        if (!retorno)
            throw new Exception("Erro ao incluir o parceiro!");
        else
        {
            gvParceria.JSProperties["cp_Retorno"] = "S";
            e.Cancel = true;
            gvParceria.CancelEdit();
            carregaGvParcerias();
        }
    }

    protected void gvParceria_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string codigoParceiro = (e.NewValues[0] == null) ? "NULL" : e.NewValues[0].ToString();
        string produto = (e.NewValues[0] == null) ? "NULL" : e.NewValues[0].ToString();

        bool retorno = cDados.atualizaParceriaAcaoIniciativaProcesso(int.Parse(e.Keys[0].ToString()), codigoParceiro, produto);

        if (!retorno)
            throw new Exception("Erro ao editar o parceiro!");
        else
        {
            gvParceria.JSProperties["cp_Retorno"] = "S";
            e.Cancel = true;
            gvParceria.CancelEdit();
            carregaGvParcerias();
        }
    }

    protected void gvParceria_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        //Si não for encontrado en modo edição, retornar.
        if (!gvParceria.IsEditing)
            return;
        else if (e.Column.FieldName == "Area" && cDados != null)
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            DataSet ds = cDados.getUnidadesDaEntidadeAtiva(codigoEntidadeUsuarioResponsavel.ToString(), "");

            if (cDados.DataSetOk(ds))
            {
                combo.DataSource = ds;
                combo.TextField = "SiglaUnidadeNegocio";
                combo.ValueField = "CodigoUnidadeNegocio";
                combo.DataBind();
            }
        }
    }

    #endregion

    #region BD Produtos

    protected void gvProduto_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        bool retorno = cDados.excluiMetaAcaoIniciativaProcesso(int.Parse(e.Keys[0].ToString()));

        if (!retorno)
            throw new Exception("Erro ao excluir o produto!");
        else
        {
            gvProduto.JSProperties["cp_Retorno"] = "S";
            e.Cancel = true;
            gvProduto.CancelEdit();
            carregaGvProdutos();
        }
    }

    protected void gvProduto_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        bool retorno = cDados.incluiMetaAcaoIniciativaProcesso(codigoProjeto, codigoAtividade, e.NewValues[0] == null ? "" : e.NewValues[0].ToString());

        if (!retorno)
            throw new Exception("Erro ao incluir o produto!");
        else
        {
            gvProduto.JSProperties["cp_Retorno"] = "S";
            e.Cancel = true;
            gvProduto.CancelEdit();
            carregaGvProdutos();
        }
    }

    protected void gvProduto_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        bool retorno = cDados.atualizaMetaAcaoIniciativaProcesso(int.Parse(e.Keys[0].ToString()), e.NewValues[0].ToString());

        if (!retorno)
            throw new Exception("Erro ao editar o produto!");
        else
        {
            gvProduto.JSProperties["cp_Retorno"] = "S";
            e.Cancel = true;
            gvProduto.CancelEdit();
            carregaGvProdutos();
        }
    }

    #endregion

    #region BD Marcos

    protected void gvMarco_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
  
    }

    protected void gvMarco_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
 
    }

    protected void gvMarco_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
  
    }

    #endregion
}