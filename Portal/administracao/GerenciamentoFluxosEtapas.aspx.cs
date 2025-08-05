using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class administracao_GerenciamentoFluxosEtapas : System.Web.UI.Page
{
    dados cDados;
    private string _key = System.Configuration.ConfigurationManager.AppSettings["key"].ToString();
    private int codigoUsuarioResponsavel;
    private int CodigoEntidade;

    protected void Page_Init(object sender, EventArgs e)
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);
        
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        defineTituloGrid();
        carregaGrid();

        if (!ddlPerfil.IsCallback)
            carregaComboPerfis("");

        if (!(IsCallback || IsPostBack))
            carregaComboEtapasAssociadas("");

        cDados.aplicaEstiloVisual(this);
        gv_PessoasAcessos_etp.Settings.ShowFilterRow = false;
        gv_PessoasAcessos_etp.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    #region Grid Acessos

    private void carregaGrid()
    {
        int codigoWf = int.Parse(Request.QueryString["CWF"].ToString());

        string comandoSQL = string.Format(@"
          EXEC dbo.p_wf_GetEtapasConectoresWorkflow {0}", codigoWf);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            DataView dv = new DataView(ds.Tables[0], "id = " + Request.QueryString["id"], "name", DataViewRowState.CurrentRows);
            gv_PessoasAcessos_etp.DataSource = dv;
            gv_PessoasAcessos_etp.DataMember = "name";
            gv_PessoasAcessos_etp.DataBind();

            if(!IsPostBack && gv_PessoasAcessos_etp.VisibleRowCount > 0)
            {
                DataRow dr = dv[0].Row;
                edtIdEtapa_etp.Text = dr["id"].ToString();
                edtNomeAbreviado_etp.Text = dr["Name"].ToString();
                edtDescricaoResumida_etp.Text = dr["toolText"].ToString();
                txtQtdTempo.Text = dr["timeOutValue"].ToString();
                ddlUnidadeTempo.Value = dr["timeoutUnit"].ToString();
                ddlReferenciaTempo.Value = dr["timeoutOffset"].ToString();
                ddlEtapaControladaAssociada.Value = dr["etapaControladaAssociada"].ToString();
            }
        }

        GridViewDataComboBoxColumn column = (gv_PessoasAcessos_etp.Columns["idGrupoAcesso"] as GridViewDataComboBoxColumn);

        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, string.Format(@" AND CodigoEntidade = {0} ", CodigoEntidade)).Tables[0];

        column.PropertiesComboBox.DataSource = dtForms;
        column.PropertiesComboBox.ValueField = "CodigoPerfilWf";
        column.PropertiesComboBox.TextField = "NomePerfilWf";
    }

    private void defineTituloGrid()
    {
        gv_PessoasAcessos_etp.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""novaLinha();"" src=""../imagens/botoes/incluirReg02.png"" alt=""Adicionar Novo Perfil""/>                                               
                                            </td><td align=""center"">Pessoas com Acesso à Etapa</td><td style=""width: 50px""></td></tr></table>";
    }
        
    private bool incluiPerfil(string codigoPerfil, string acesso)
    {
        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, " AND CodigoPerfilWf = " + codigoPerfil).Tables[0];

        string nomePerfil = dtForms.Rows[0]["NomePerfilWf"].ToString();

        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesEtapasWorkflow] 
             {0}
            , 'IGA' 
            , {1}
            , null
            , null
            , null
            , null
            , null
            , {2}
            , '{3}'
            , '{4}'
            , {5}"
        , Request.QueryString["CWF"]
        , Request.QueryString["id"]
        , codigoPerfil
        , nomePerfil
        , acesso
        , ddlEtapaControladaAssociada.Value);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        return result;
    }

    private bool editaPerfil(string codigoPerfil, string acesso)
    {
        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, " AND CodigoPerfilWf = " + codigoPerfil).Tables[0];

        string nomePerfil = dtForms.Rows[0]["NomePerfilWf"].ToString();
        string tipoAcesso = acesso;

        if (tipoAcesso == "Ação")
            tipoAcesso = "A";
        else if (tipoAcesso == "Consulta")
            tipoAcesso = "C";

        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesEtapasWorkflow] 
             {0}
            , 'AGA' 
            , {1}
            , null
            , null
            , null
            , null
            , null
            , {2}
            , '{3}'
            , '{4}'"
       , Request.QueryString["CWF"]
       , Request.QueryString["id"]
        ,codigoPerfil
        , nomePerfil
       , tipoAcesso);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        return result;
    }

    private bool excluiPerfil(string codigoPerfil, string acesso)
    {
        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, " AND CodigoPerfilWf = " + codigoPerfil).Tables[0];

        string nomePerfil = dtForms.Rows[0]["NomePerfilWf"].ToString();

        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesEtapasWorkflow] 
             {0}
            , 'EGA' 
            , {1}
            , null
            , null
            , null
            , null
            , null
            , {2}
            , '{3}'
            , '{4}'
            , {5}"
        , Request.QueryString["CWF"]
        , Request.QueryString["id"]
        , codigoPerfil
        , nomePerfil
        , acesso
        , ddlEtapaControladaAssociada.Value);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        return result;
    }

    #endregion

    private void carregaComboPerfis(string whereParam)
    {
        //DataTable dtSessao = (DataTable)Session["dtGruposEtapas"];
        string where = "";
        string gruposEnLaLista = "";

        if (whereParam == "")
        {
            for (int i = 0; i < gv_PessoasAcessos_etp.VisibleRowCount; i++)
            { //Listar los CodigoPerfilWf que se encuentran en la Grid. De esta forma se evitará su 
              //listado en el combo salvando de una selección repetitiva.
                if (gv_PessoasAcessos_etp.GetRowValues(i, "idGrupoAcesso") != null && gv_PessoasAcessos_etp.GetRowValues(i, "idGrupoAcesso").ToString() != "")
                    gruposEnLaLista += "," + gv_PessoasAcessos_etp.GetRowValues(i, "idGrupoAcesso").ToString().Trim();


            }
            if (gruposEnLaLista != "")
            {
                if (gruposEnLaLista[gruposEnLaLista.Length - 1] == ',')
                {
                    gruposEnLaLista = gruposEnLaLista.Substring(0, gruposEnLaLista.Length - 1);
                }
                where += " AND CodigoPerfilWf NOT IN(" + gruposEnLaLista.Substring(1) + ")";
            }
        }
        else
            where += whereParam;

        where += string.Format(@" AND CodigoEntidade = {0} ", CodigoEntidade);


        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, where).Tables[0];

        ddlPerfil.DataSource = dtForms;
        ddlPerfil.ValueField = "CodigoPerfilWf";
        ddlPerfil.TextField = "NomePerfilWf";
        ddlPerfil.DataBind();
    }

    private void carregaComboEtapasAssociadas(string v)
    {
        string comandoSQL = @" SELECT [CodigoEtapaWfControladaSistema], [NomeIndicadorEtapaWf]
FROM [dbo].[EtapasWfControladasSistema]
UNION SELECT -1, '(Nenhuma)'
ORDER BY [NomeIndicadorEtapaWf] ";

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlEtapaControladaAssociada.DataSource = ds;
            ddlEtapaControladaAssociada.ValueField = "CodigoEtapaWfControladaSistema";
            ddlEtapaControladaAssociada.TextField = "NomeIndicadorEtapaWf";
            ddlEtapaControladaAssociada.DataBind();
        }
    }

    protected void ddlPerfil_Callback(object sender, CallbackEventArgsBase e)
    {
        carregaComboPerfis(e.Parameter);
    }

    protected void gv_PessoasAcessos_etp_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        bool resultGeral = true, resultInsert = true, resultEdit = true, resultDelete = true;
        string msg = "";

        resultGeral = salvaDados();

        for (int i = 0; i < e.DeleteValues.Count; i++)
        {
            resultDelete = excluiPerfil(e.DeleteValues[i].Values["idGrupoAcesso"].ToString(), e.DeleteValues[i].Values["accessType"].ToString());
        }

        for (int i = 0; i < e.InsertValues.Count; i++)
        {
            resultInsert = incluiPerfil(e.InsertValues[i].NewValues["idGrupoAcesso"].ToString(), e.InsertValues[i].NewValues["accessType"].ToString());
        }

        for (int i = 0; i < e.UpdateValues.Count; i++)
        {
            resultEdit = editaPerfil(e.UpdateValues[i].NewValues["idGrupoAcesso"].ToString(), e.UpdateValues[i].NewValues["accessType"].ToString());
        }

        if (!resultGeral)
            msg += "Erro ao salvar dados da etapa<br>";
        if (!resultInsert)
            msg += "Erro ao incluir perfil de acesso<br>";
        if (!resultEdit)
            msg += "Erro ao editar perfil de acesso<br>";
        if (!resultDelete)
            msg += "Erro ao excluir perfil de acesso<br>";

        if (msg == "")
        {
            gv_PessoasAcessos_etp.JSProperties["cp_status"] = "ok";
            gv_PessoasAcessos_etp.JSProperties["cp_msg"] = "Etapa alterada com sucesso!";
            carregaGrid();
        }
        else
        {
            gv_PessoasAcessos_etp.JSProperties["cp_status"] = "erro";
            gv_PessoasAcessos_etp.JSProperties["cp_msg"] = msg;
        }

        e.Handled = true;
    }

    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        bool resultGeral = salvaDados();

        if (resultGeral)
        {
            callbackSalvar.JSProperties["cp_status"] = "ok";
            callbackSalvar.JSProperties["cp_msg"] = "Etapa alterada com sucesso!";
        }
        else
        {
            callbackSalvar.JSProperties["cp_status"] = "erro";
            callbackSalvar.JSProperties["cp_msg"] = "Erro ao alterar etapa!";
        }
    }

    private bool salvaDados()
    {
        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesEtapasWorkflow] 
             {0}
            , 'AC' 
            , {1}
            , '{2}'
            , '{3}'
            , {4}
            , '{5}'
            , '{6}'
            , null
            , null
            , null
            , {7}
"
        , Request.QueryString["CWF"]
        , Request.QueryString["id"]
        , edtNomeAbreviado_etp.Text
        , edtDescricaoResumida_etp.Text
        , txtQtdTempo.Text
        , ddlUnidadeTempo.Value
        , ddlReferenciaTempo.Value
        , ddlEtapaControladaAssociada.Value);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);        

        return result;
    }
}