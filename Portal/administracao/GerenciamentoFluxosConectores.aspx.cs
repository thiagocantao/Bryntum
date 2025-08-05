using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
        carregaComboIcones();
        carregaGrid();
        
        if(!ddlPerfil.IsCallback)
            carregaComboPerfis("");

        cDados.aplicaEstiloVisual(this);
        gv_PessoasAcessos_etp.Settings.ShowFilterRow = false;
        gv_PessoasAcessos_etp.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        Header.Controls.Add(cDados.getLiteral("<script type='text/javascript' language='Javascript' src='../scripts/textArea - textInsertion.js'></script>"));
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
            string where = "idEtapa = " + Request.QueryString["idEtapaOrigem"] + " AND idEtapaDestino = " + Request.QueryString["idEtapaDestino"];
            DataView dv = new DataView(ds.Tables[2], where, "idEtapa", DataViewRowState.CurrentRows);
            gv_PessoasAcessos_etp.DataSource = dv;
            gv_PessoasAcessos_etp.DataMember = "idEtapa";
            gv_PessoasAcessos_etp.DataBind();

            if (!IsPostBack && gv_PessoasAcessos_etp.VisibleRowCount > 0)
            {
                DataRow dr = dv[0].Row;
                edtAcao_cnt.Text = dr["textoBotao"].ToString();
                ceCorBotao_cnt.Text = dr["corBotao"].ToString();
                cmbIconeBotao_cnt.Text = dr["iconeBotao"].ToString();
                txtAssunto1_cnt.Text = dr["assuntoNotificacaoAcao"].ToString();
                mmTexto1_cnt.Text = dr["textoNotificacaoAcao"].ToString();
                txtAssunto2_cnt.Text = dr["assuntoNotificacaoAcompanhamento"].ToString();
                mmTexto2_cnt.Text = dr["textoNotificacaoAcompanhamento"].ToString();
            }
        }


        GridViewDataComboBoxColumn column = (gv_PessoasAcessos_etp.Columns["idGrupoNotificado"] as GridViewDataComboBoxColumn);        
                
        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, string.Format(@" AND CodigoEntidade = {0} ", CodigoEntidade)).Tables[0];

        column.PropertiesComboBox.DataSource = dtForms;
        column.PropertiesComboBox.ValueField = "CodigoPerfilWf";
        column.PropertiesComboBox.TextField = "NomePerfilWf";
    }

    private void defineTituloGrid()
    {
        gv_PessoasAcessos_etp.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""novaLinha();"" src=""../imagens/botoes/incluirReg02.png"" alt=""Adicionar Novo Perfil""/>                                               
                                            </td><td align=""center"">Perfis Notificados</td><td style=""width: 50px""></td></tr></table>";
    }

    private bool incluiPerfil(string codigoPerfil, string tipoNotificacao)
    {
        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, " AND CodigoPerfilWf = " + codigoPerfil).Tables[0];

        string nomePerfil = dtForms.Rows[0]["NomePerfilWf"].ToString();

        if (tipoNotificacao == "Ação")
            tipoNotificacao = "E";
        else if (tipoNotificacao == "Acompanhamento")
            tipoNotificacao = "S";

        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesConectoresWorkflow] 
             {0}
            , 'IGN' 
            , {1}
            , {2}
            , null
			, null 
			, null
			, {3}
			, '{4}'
			, '{5}'
			, null
			, null
			, null
			, null
"
         , Request.QueryString["CWF"]
         , Request.QueryString["idEtapaOrigem"]
         , Request.QueryString["idEtapaDestino"]
         , codigoPerfil
        , nomePerfil
        , tipoNotificacao);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        return result;
    }

    private bool editaPerfil(string codigoPerfil, string tipoNotificacao)
    {
        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, " AND CodigoPerfilWf = " + codigoPerfil).Tables[0];

        string nomePerfil = dtForms.Rows[0]["NomePerfilWf"].ToString();
        
        if (tipoNotificacao == "Ação")
            tipoNotificacao = "E";
        else if (tipoNotificacao == "Acompanhamento")
            tipoNotificacao = "S";

        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesConectoresWorkflow] 
             {0}
            , 'AGN' 
            , {1}
            , {2}
            , null
			, null 
			, null
			, {3}
			, '{4}'
			, '{5}'
			, null
			, null
			, null
			, null
"
         , Request.QueryString["CWF"]
         , Request.QueryString["idEtapaOrigem"]
         , Request.QueryString["idEtapaDestino"]
         , codigoPerfil
        , nomePerfil
        , tipoNotificacao);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        return result;
    }

    private bool excluiPerfil(string codigoPerfil, string tipoNotificacao) 
    {
        DataTable dtForms = cDados.getTiposGruposPessoasWf(_key, " AND CodigoPerfilWf = " + codigoPerfil).Tables[0];

        string nomePerfil = dtForms.Rows[0]["NomePerfilWf"].ToString();

        if (tipoNotificacao == "Ação")
            tipoNotificacao = "E";
        else if (tipoNotificacao == "Acompanhamento")
            tipoNotificacao = "S";

        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesConectoresWorkflow] 
             {0}
            , 'EGN' 
            , {1}
            , {2}
            , null
			, null 
			, null
			, {3}
			, '{4}'
			, '{5}'
			, null
			, null
			, null
			, null
"
        , Request.QueryString["CWF"]
        , Request.QueryString["idEtapaOrigem"]
        , Request.QueryString["idEtapaDestino"]
        , codigoPerfil
        , nomePerfil
        , tipoNotificacao);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        return result;
    }

    #endregion

    private bool salvaDados()
    {
        string comandoSQL = string.Format(@"
        EXEC [dbo].[p_wf_SalvaAlteracoesConectoresWorkflow] 
             {0}
            , 'AC' 
            , {1}
            , {2}
            , '{3}'
			, '{4}' 
			, '{5}'
			, null
			, null
			, null
			, '{6}'
			, '{7}'
			, '{8}'
			, '{9}'
"
        , Request.QueryString["CWF"]
        , Request.QueryString["idEtapaOrigem"]
        , Request.QueryString["idEtapaDestino"]
        , edtAcao_cnt.Text
        , ceCorBotao_cnt.Text
        , cmbIconeBotao_cnt.Text
        , txtAssunto1_cnt.Text.Replace("'", "''")
        , mmTexto1_cnt.Text.Replace("'", "''")
        , txtAssunto2_cnt.Text.Replace("'", "''")
        , mmTexto2_cnt.Text.Replace("'", "''"));

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        //if(result)
        //{
        //    callbackSalvar.JSProperties["cp_status"] = "ok";
        //    callbackSalvar.JSProperties["cp_msg"] = "Conector alterado com sucesso!";
        //}else
        //{
        //    callbackSalvar.JSProperties["cp_status"] = "erro";
        //    callbackSalvar.JSProperties["cp_msg"] = "Erro ao alterar conector!";
        //}

        return result;
    }

    private void carregaComboIcones()
    {
        cmbIconeBotao_cnt.Items.Clear();
        DirectoryInfo Dir = new DirectoryInfo(Server.MapPath("~/imagens/iconesBotoesFluxo"));
        // Busca automaticamente todos os arquivos em todos os subdiretórios
        FileInfo[] Files = Dir.GetFiles("*.png", SearchOption.AllDirectories);

        foreach (FileInfo File in Files)
        {
            ListEditItem lei = new ListEditItem();
            lei.Value = File.Name;
            lei.ImageUrl = string.Format(@"{0}imagens/iconesBotoesFluxo/{1}", cDados.getPathSistema(), File.Name);
            lei.Text = File.Name.Replace(File.Extension, "");
            if (lei.Text == "Sem Imagem")
                cmbIconeBotao_cnt.Items.Insert(0, lei);
            else
                cmbIconeBotao_cnt.Items.Add(lei);
            //string text = string.Format(@"<img style='width:16px; height:16px' src='{0}imagens/iconesBotoesFluxo/{1}'/>", cDados.getPathSistema(), File.Name);
            //cmbIconeBotao_cnt.Items.Add(text, File.Name);
        }
        cmbIconeBotao_cnt.ItemImage.Width = 16;
        cmbIconeBotao_cnt.ItemImage.Height = 16;
    }

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
                if (gv_PessoasAcessos_etp.GetRowValues(i, "idGrupoNotificado") != null && gv_PessoasAcessos_etp.GetRowValues(i, "idGrupoNotificado").ToString() != "")
                    gruposEnLaLista += "," + gv_PessoasAcessos_etp.GetRowValues(i, "idGrupoNotificado").ToString().Trim();


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
            resultDelete = excluiPerfil(e.DeleteValues[i].Values["idGrupoNotificado"].ToString(), e.DeleteValues[i].Values["tipoNotificacao"].ToString());
        }

        for (int i = 0; i < e.InsertValues.Count; i++)
        {
            resultInsert = incluiPerfil(e.InsertValues[i].NewValues["idGrupoNotificado"].ToString(), e.InsertValues[i].NewValues["tipoNotificacao"].ToString());
        }

        for (int i = 0; i < e.UpdateValues.Count; i++)
        {
            resultEdit = editaPerfil(e.UpdateValues[i].NewValues["idGrupoNotificado"].ToString(), e.UpdateValues[i].NewValues["tipoNotificacao"].ToString());
        }

        if (!resultGeral)
            msg += "Erro ao salvar dados do conector<br>";
        if (!resultInsert)
            msg += "Erro ao incluir perfil de notificação<br>";
        if (!resultEdit)
            msg += "Erro ao editar perfil de notificação<br>";
        if (!resultDelete)
            msg += "Erro ao excluir perfil de notificação<br>";

        if (msg == "")
        {            
            gv_PessoasAcessos_etp.JSProperties["cp_status"] = "ok";
            gv_PessoasAcessos_etp.JSProperties["cp_msg"] = "Conector alterado com sucesso!";
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
            callbackSalvar.JSProperties["cp_msg"] = "Conector alterado com sucesso!";
        }
        else
        {
            callbackSalvar.JSProperties["cp_status"] = "erro";
            callbackSalvar.JSProperties["cp_msg"] = "Erro ao alterar conector!";
        }
    }
}