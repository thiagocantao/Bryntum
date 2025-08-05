using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _OS_ControleOS : System.Web.UI.Page
{

    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    bool readOnly = false;

    private int idProjeto = -1;
    private int codigoWorkflow = -1;
    private int codigoInstanciaWf = -1;
    public bool podeIncluir = true;
    private int codigoOS = -1;

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
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        }

        cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["RO"] != null)
            readOnly = Request.QueryString["RO"].ToString() == "S";

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString()) - 250;
        }
        else
        {
            // Calcula a altura da tela
            int largura = 0;
            int altura = 0;

            cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


            alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
            alturaPrincipal = (altura - 190);

            gvDados.Settings.VerticalScrollableHeight = altura - 320;
        }

        if (Request.QueryString["CP"] != null)
            idProjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());

        if (Request.QueryString["CIWF"] != null && Request.QueryString["CIWF"].ToString() != "")
            codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());

        if(hfWF.Contains("CodigoCI"))
        {
            codigoInstanciaWf = int.Parse(hfWF.Get("CodigoCI").ToString());
        }

        carregaDados();
        carregaGvDados();

        cDados.setaTamanhoMaximoMemo(txtDescricao, 2000, lbl_descricao);

        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        callbackSalvar.JSProperties["cp_OS"] = codigoOS;
    }

    private void carregaDados()
    {
        string comandoSQL = string.Format(@"
        SELECT * 
          FROM dbo.f_cos_getDadosOS({0}, {1}, {2}, {3}, {4})"
        , codigoEntidadeUsuarioResponsavel
        , idProjeto
        , codigoUsuarioResponsavel
        , codigoWorkflow
        , codigoInstanciaWf);

        DataSet ds = cDados.getDataSet(comandoSQL);
        bool podeAlterar =  readOnly == false;

        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            if(!IsPostBack)
            {
                txtNumeroOS.Text = dr["NumeroOS"].ToString();
                txtTituloOS.Text = dr["IdentificacaoOS"].ToString();
                txtStatus.Text = dr["DescricaoStatus"].ToString();
                txtDescricao.Text = dr["DescricaoOS"].ToString();
            }

            callbackSalvar.JSProperties["cpNumeroOS"] = dr["NumeroOS"].ToString();
            callbackSalvar.JSProperties["cpTituloOS"] = dr["IdentificacaoOS"].ToString();
            callbackSalvar.JSProperties["cpStatus"] = dr["DescricaoStatus"].ToString();
            callbackSalvar.JSProperties["cpDescricao"] = dr["DescricaoOS"].ToString();

            if (dr["CodigoOS"].ToString() != "")
                codigoOS = int.Parse(dr["CodigoOS"].ToString());

            if (codigoOS != -1)
            {
                podeIncluir = dr["IndicaPodeIncluirItens"].ToString() == "S" && readOnly == false;
                podeAlterar = dr["IndicaPodeAlterar"].ToString() == "S" && readOnly == false;
            }
        }

        if(codigoInstanciaWf == -1)
        {
            btnSalvar.ClientSideEvents.Click = "function(s, e){ gravaInstanciaWf(); }";
        }
        else
        {
            btnSalvar.ClientSideEvents.Click = "function(s, e){ callbackSalvar.PerformCallback(); }";
        }

        txtDescricao.ClientEnabled = podeAlterar;
        txtTituloOS.ClientEnabled = podeAlterar;
        btnSalvar.ClientVisible = podeAlterar;
    }

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        SELECT * 
          FROM dbo.f_cos_getDadosItensOS({0}, {1}, {2})"
        , codigoEntidadeUsuarioResponsavel
        , codigoOS
        , codigoUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        if(e.VisibleIndex > -1)
        {
            string podeEditar = gvDados.GetRowValues(e.VisibleIndex, "IndicaPodeAlterar").ToString();
            string podeExcluir = gvDados.GetRowValues(e.VisibleIndex, "IndicaPodeExcluir").ToString();

            if(e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
            {
                if (podeEditar == "N")
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
            {
                if (podeExcluir == "N")
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
                e.Image.ToolTip = "Cancelar";
                e.Text = "Cancelar";
            }
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        salvaDados();
    }

    private void salvaDados()
    {
        string comandoSQL = "";
        string msgRetorno = "";
        bool result = false;
        callbackSalvar.JSProperties["cp_AtualizarTela"] = "N";

        if (codigoOS == -1)
        {
            comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoOS int
            EXEC dbo.[p_cos_incluiDadosOS] '{0}', '{1}', {2}, {3}, {4}, @CodigoOS OUTPUT
            SELECT @CodigoOS AS CodigoOS
        END"
            , txtTituloOS.Text.Replace("'", "''")
            , txtDescricao.Text.Replace("'", "''")
            , codigoUsuarioResponsavel
            , codigoWorkflow
            , codigoInstanciaWf);

            try
            {
                DataSet ds = cDados.getDataSet(comandoSQL);

                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["CodigoOS"].ToString() != "")
                {
                    codigoOS = int.Parse(ds.Tables[0].Rows[0]["CodigoOS"].ToString());
                    callbackSalvar.JSProperties["cp_OS"] = codigoOS;
                    result = true;
                }

                if (result)
                {
                    callbackSalvar.JSProperties["cp_AtualizarTela"] = "S";
                    msgRetorno = "Dados salvos com sucesso!";
                }
                else
                    msgRetorno = "Erro ao salvar os dados!";
            }
            catch (Exception ex)
            {
                msgRetorno = "Erro ao salvar dados! " + ex.Message;
            }
        }
        else
        {
            try
            {
                comandoSQL = string.Format(@"
            EXEC dbo.[p_cos_alteraDadosOS] {0}, '{1}', '{2}', {3}"
               , codigoOS
               , txtTituloOS.Text.Replace("'", "''")
               , txtDescricao.Text.Replace("'", "''")
               , codigoUsuarioResponsavel);

                int regAf = 0;
                result = cDados.execSQL(comandoSQL, ref regAf);

                if (result)
                    msgRetorno = "Dados salvos com sucesso!";
                else
                    msgRetorno = "Erro ao salvar os dados!";
            } catch (Exception ex)
            {
                msgRetorno = "Erro ao salvar dados! " + ex.Message;
            }
        }

        callbackSalvar.JSProperties["cp_Msg"] = msgRetorno;
        carregaDados();
    }

    protected void gvDados_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        
            if (e.Column.FieldName == "NomeProjetoAtendido")
            {
                string comandoSQL = string.Format(@"
                SELECT p.NomeProjeto, p.CodigoProjeto, p.Numero1
                  FROM Projeto p
                 WHERE EXISTS(SELECT 1 FROM dbo.f_GetProjetosUsuario({0}, {1}, {2}) f WHERE f.CodigoProjeto = p.CodigoProjeto)
                   AND ISNULL([Numero1], 0)>0
                ORDER BY p.NomeProjeto"
                , codigoUsuarioResponsavel
                , codigoEntidadeUsuarioResponsavel
                , int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()));

                DataSet dsProjetos = cDados.getDataSet(comandoSQL);
                (e.Editor as ASPxComboBox).DataSource = dsProjetos;
                (e.Editor as ASPxComboBox).TextField = "NomeProjeto";
                (e.Editor as ASPxComboBox).ValueField = "CodigoProjeto";
                (e.Editor as ASPxComboBox).DataBind();

            if (e.VisibleIndex > -1)
                (e.Editor as ASPxComboBox).Value = gvDados.GetRowValues(e.VisibleIndex, "CodigoProjetoAtendido").ToString();
            }
            else if (e.Column.FieldName == "IdentificacaoItemCatalogo")
            {
                string comandoSQL = string.Format(@"
               BEGIN
                 DECLARE @CodigoOS Bigint
                 SET @CodigoOS = {0};

                 SELECT   
                    ic.[CodigoItemCatalogo]
                  , ic.[IdentificacaoItemCatalogo]
                  , ic.DescricaoProduto
                 FROM
                  dbo.[cos_itemCatalogoContrato]   AS [ic]
                 WHERE
                  ic.[CodigoContrato] IN ( SELECT os.[CodigoContrato] FROM [dbo].[cos_OrdemServico] AS [os] WHERE os.[CodigoOS] = @CodigoOS )
                  AND ic.[IndicaItemAtivo] = 'S'
                 ORDER BY 
                  ic.[IdentificacaoItemCatalogo]
                END"
                , codigoOS);

                DataSet dsCatalogos = cDados.getDataSet(comandoSQL);
                (e.Editor as ASPxComboBox).DataSource = dsCatalogos;
                (e.Editor as ASPxComboBox).TextField = "IdentificacaoItemCatalogo";
                (e.Editor as ASPxComboBox).ValueField = "CodigoItemCatalogo";
                (e.Editor as ASPxComboBox).DataBind();

            if (e.VisibleIndex > -1)
                (e.Editor as ASPxComboBox).Value = gvDados.GetRowValues(e.VisibleIndex, "CodigoItemCatalogo").ToString();
        }        
    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int regAf = 0;

        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoItemOS int
            EXEC dbo.[p_cos_incluiDadosItemOS] {0}, {1}, {2}, '{3}', '{4}', '{5}', {6}, @CodigoItemOS
        END", codigoOS
            , e.NewValues["IdentificacaoItemCatalogo"]
            , e.NewValues["NomeProjetoAtendido"]
            , e.NewValues["IndicaCriticidadeItem"]
            , e.NewValues["IndicaPrioridadeItem"]
            , e.NewValues["DescricaoItemOS"] == null ? "" : e.NewValues["DescricaoItemOS"].ToString().Replace("'", "''")
            , codigoUsuarioResponsavel);

        cDados.execSQL(comandoSQL, ref regAf);

        carregaGvDados();
        e.Cancel = true;
        gvDados.CancelEdit();
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int regAf = 0;

        string comandoSQL = string.Format(@"
        BEGIN
            EXEC dbo.[p_cos_alteraDadosItemOS] {0}, {1}, {2}, '{3}', '{4}', '{5}', {6}
        END", e.Keys[0]
            , e.NewValues["IdentificacaoItemCatalogo"]
            , e.NewValues["NomeProjetoAtendido"]
            , e.NewValues["IndicaCriticidadeItem"]
            , e.NewValues["IndicaPrioridadeItem"]
            , e.NewValues["DescricaoItemOS"] == null ? "" : e.NewValues["DescricaoItemOS"].ToString().Replace("'", "''")
            , codigoUsuarioResponsavel);

        cDados.execSQL(comandoSQL, ref regAf);

        carregaGvDados();
        e.Cancel = true;
        gvDados.CancelEdit();
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int regAf = 0;

        string comandoSQL = string.Format(@"
        BEGIN
            UPDATE cos_ItemOS SET DataCancelamento = GetDate(), CodigoUsuarioCancelamento = {1}, SiglaStatusItem = 'CN' 
             WHERE CodigoItemOS = {0}
        END", e.Keys[0]
            , codigoUsuarioResponsavel);

        cDados.execSQL(comandoSQL, ref regAf);

        carregaGvDados();
        e.Cancel = true;
        gvDados.CancelEdit();
    }
}