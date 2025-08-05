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
using System.Drawing;

public partial class administracao_CadastroMenusObjetos : System.Web.UI.Page
{
    string msgErro = string.Empty;
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    
    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    DataSet dsItemMenu = new DataSet();
    DataSet dsGrupos = new DataSet();

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
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AltMnuPrj");
        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_IncMnuPrj");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_ExcMnuPrj");
                
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");
        
        cDados.aplicaEstiloVisual(Page);

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        
            populaTipoProjeto();
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();

        }
        
        
        carregaGvDados();
        carregaGvItems();

        gvMenuTipoProjeto.Settings.ShowFilterRow = false;
        gvMenuTipoProjeto.Settings.ShowFilterRowMenu = false;
        gvMenuTipoProjeto.SettingsPager.AlwaysShowPager = false;
        gvMenuTipoProjeto.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroMenusObjetos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "CadastroMenusObjetos", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 150);

        gvDados.Settings.VerticalScrollableHeight = altura - 320;
    }
    #endregion
    
    #region GRID

    public bool hiddenFieldContem(ASPxHiddenField hf, string nomePropriedade)
    {
        object naoExiste = null;
        bool retorno = false;
        try
        {
            retorno = hf.TryGet(nomePropriedade, out naoExiste);
        }
        catch
        {
            retorno = false;
        }
        return retorno;
    }

    
    private void carregaGvDados()
    {
        int codigoTipoProjetoNaoExistente = -1;

        int codigoTipoProjetoSelecionado = -1;

        if (hiddenFieldContem(hfGeral,"codigoTipoProjetoSelecionado"))
        {
            if (int.TryParse(hfGeral.Get("codigoTipoProjetoSelecionado").ToString(), out codigoTipoProjetoNaoExistente))
            {
                codigoTipoProjetoSelecionado = int.Parse(hfGeral.Get("codigoTipoProjetoSelecionado").ToString());
            }
        
        }
        dsGrupos = cDados.getGrupoMenuTipoObjeto(codigoEntidadeUsuarioResponsavel, codigoTipoProjetoSelecionado, "");

        if ((cDados.DataSetOk(dsGrupos)))
        {
            gvDados.DataSource = dsGrupos;
            gvDados.DataBind();
        }
    }

    private void carregaGvItems()
    {
       int codigoGrupoSelecionado = -1;

       if (getChavePrimaria() != "")
       {
           codigoGrupoSelecionado = int.Parse(getChavePrimaria());
       }

       dsItemMenu = cDados.getMenuTipoProjeto(codigoGrupoSelecionado);

       if ((cDados.DataSetOk(dsItemMenu)))
        {
            gvMenuTipoProjeto.DataSource = dsItemMenu.Tables[0];
            gvMenuTipoProjeto.DataBind();
        }
    }

    private void populaTipoProjeto()
    {
        DataSet ds1 = cDados.getTipoProjetoPorIniciais();
        ddlTipoProjeto.DataSource = ds1.Tables[0];
        ddlTipoProjeto.DataBind();
        if (ddlTipoProjeto.Items.Count > 0)
        {
            ddlTipoProjeto.SelectedIndex = 0;
            hfGeral.Set("codigoTipoProjetoSelecionado", ddlTipoProjeto.SelectedItem.Value.ToString());
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string iniciaisGrupo = (gvDados.GetRowValues(e.VisibleIndex, "IniciaisGrupoMenu") != null) ? gvDados.GetRowValues(e.VisibleIndex, "IniciaisGrupoMenu").ToString() : "";

        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar && iniciaisGrupo == "")
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir && iniciaisGrupo == "")
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "filtra")
        {
            carregaGvItems();
        }
        if (e.Parameter == "filtraGrupos")
        {
            carregaGvDados();
        }        
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }

        pnCallback.JSProperties["cp_Msg"] = mensagemErro_Persistencia;
    }

    #endregion
    
    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string getChavePrimariagvMenuTipoProjeto()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvMenuTipoProjeto.GetRowValues(gvMenuTipoProjeto.FocusedRowIndex, gvMenuTipoProjeto.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {

        int codigoTipoProjeto = int.Parse(ddlTipoProjeto.SelectedItem.Value.ToString());
        string DescricaoOpcaoMenu = txtGrupo.Text;
        int SequenciaItemMenuGrupo = int.Parse(txtOrdem.Text);
        string mensagemErro = "";
        char indicaGrupoVisivel = (ckGrupoAtivo.Checked == true) ? 'S' : 'N';

        bool nomeDisponivel = dsGrupos.Tables[0].Select("DescricaoGrupoMenuTipoProjeto = '" + DescricaoOpcaoMenu + "'").Length == 0;//cDados.verificaInclusaoGrupoMenuTipoProjeto(codigoTipoProjeto, DescricaoOpcaoMenu, codigoEntidadeUsuarioResponsavel);

        if (nomeDisponivel == true)
        {
            bool result = cDados.incluiGrupoMenuTipoProjeto(DescricaoOpcaoMenu, codigoEntidadeUsuarioResponsavel, codigoTipoProjeto, SequenciaItemMenuGrupo, indicaGrupoVisivel, mensagemErro);
            if (result == false)
                return "Erro: " + mensagemErro;
            else
            {
                carregaGvDados();
                return Resources.traducao.CadastroMenusObjetos_grupo_de_menu_inclu_do_com_sucesso_;
            }
        }
        else
        {
            return Resources.traducao.CadastroMenusObjetos_n_o_foi_poss_vel_incluir_o_grupo_de_menu__o_nome_informado_j__existe_;
        }
    }


    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoGrupoMenuTipoProjeto = int.Parse(getChavePrimaria());
        int codigoTipoProjeto = int.Parse(ddlTipoProjeto.SelectedItem.Value.ToString());
        string nomeDogrupo = txtGrupo.Text;
        int ordem = int.Parse(txtOrdem.Text);
        // DataSet ds = cDados.(string.Format(" AND RamoAtividade = '{0}' AND CodigoRamoAtividade <> {1}", ramoAtividade, codigoRamoAtividade));
        string indicaGrupoVisivel = (ckGrupoAtivo.Checked == true) ? "S" : "N";
        string mensagemErro = "";
        bool nomeDisponivel = dsGrupos.Tables[0].Select("CodigoGrupoMenuTipoProjeto <> " + codigoGrupoMenuTipoProjeto + " AND DescricaoGrupoMenuTipoProjeto = '" + nomeDogrupo + "'").Length == 0;//cDados.verificaInclusaoGrupoMenuTipoProjeto(codigoTipoProjeto, DescricaoOpcaoMenu, codigoEntidadeUsuarioResponsavel);

        if (nomeDisponivel == true)
        {
            bool result = cDados.atualizaGrupoMenuTipoProjeto(nomeDogrupo, ordem, indicaGrupoVisivel, codigoGrupoMenuTipoProjeto, mensagemErro);

            if (result == false)
                return mensagemErro;
            else
            {
                carregaGvDados();
                return Resources.traducao.CadastroMenusObjetos_grupo_de_menu_alterado_com_sucesso_;
            }
        }
        else
        {
            return Resources.traducao.CadastroMenusObjetos_n_o_foi_poss_vel_editar_o_grupo_de_menu__o_nome_informado_j__existe_;
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        return "";
    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }
     protected void gvMenuTipoProjeto_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //carregaGvItems();
    }
    protected void gvMenuTipoProjeto_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        //if (!gvMenuTipoProjeto.IsEditing)
        //    return;
        string mensagemErro = "";
        

        if (e.NewValues["DescricaoOpcaoMenu"] != null)
        {

            bool podeIncluirMenuTipoProjeto = dsItemMenu.Tables[0].Select("DescricaoOpcaoMenu = '" + e.NewValues["DescricaoOpcaoMenu"] + "'").Length == 0;
            if (podeIncluirMenuTipoProjeto == false)
            {
                msgErro = Resources.traducao.CadastroMenusObjetos_n_o_foi_poss_vel_incluir_a_op__o_de_menu__nome_informado_j__existe_;
                e.Cancel = true;
                return;
            }
            else
            {
                msgErro = string.Empty;
            }

            int opcaoDeMenuSelecionado = int.Parse(hfGeral.Get("codigoOpcaoDeMenuSelecionado").ToString());


            int codigoItemMenu = opcaoDeMenuSelecionado;
            int codigoGrupoMenu = int.Parse(getChavePrimaria());

            string descricaoOpcaoMenu = e.NewValues["DescricaoOpcaoMenu"].ToString();
            int sequenciaItemMenuGrupo = int.Parse(e.NewValues["SequenciaItemMenuGrupo"].ToString());
            bool retorno = cDados.incluiMenuTipoProjeto(codigoGrupoMenu, codigoItemMenu, descricaoOpcaoMenu, sequenciaItemMenuGrupo, ref mensagemErro);
            if (false == retorno)
            {
                throw new Exception(mensagemErro);
            }
            else
            {
                carregaGvItems();
            }
        }
        e.Cancel = true;
        gvMenuTipoProjeto.CancelEdit();
    }

    protected void gvMenuTipoProjeto_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

        e.Editor.ClientEnabled = true;

        object codigoTipoProjetoNaoExistente;
        int codigoTipoProjetoSelecionado = 1;
        if (hfGeral.TryGet("codigoTipoProjetoSelecionado", out codigoTipoProjetoNaoExistente) == true)
        {
            codigoTipoProjetoSelecionado = int.Parse(hfGeral.Get("codigoTipoProjetoSelecionado").ToString());
        }

        if (e.Column.FieldName == "DescricaoItemMenuObjeto")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            combo.ClientInstanceName = "ddlOpcaoDeMenu";

            

            DataSet ds1 = cDados.getItemMenuObjeto(codigoTipoProjetoSelecionado,codigoEntidadeUsuarioResponsavel, "PR");
            DataTable dt = ds1.Tables[0];
            //ds1.Tables[0].Columns[0].
            if (cDados.DataTableOk(ds1.Tables[0]))
            {
                combo.DataSource = ds1.Tables[0];
                combo.TextField = "DescricaoItemMenuObjeto";
                combo.ValueField = "CodigoItemMenuObjeto";
                combo.DataBind();
            }

            combo.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            combo.DisabledStyle.ForeColor = Color.Black;

            combo.ClientEnabled = gvMenuTipoProjeto.IsNewRowEditing;
            combo.ClientSideEvents.SelectedIndexChanged = "function(s, e) {ddlOpcaoMenuSelectedIndexChanged();}";
        }
        else if (e.Column.FieldName == "SequenciaItemMenuGrupo")
        {
            ASPxSpinEdit txt = e.Editor as ASPxSpinEdit;

            txt.MinValue = 1;
            txt.MaxValue = gvMenuTipoProjeto.IsNewRowEditing ? gvMenuTipoProjeto.VisibleRowCount + 1 : gvMenuTipoProjeto.VisibleRowCount;
        }
    }

    protected void gvMenuTipoProjeto_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string mensagemErro = "";

            //if()
        
        if (e.NewValues["DescricaoOpcaoMenu"] != null)
        {

            int codigoItemMenu = int.Parse(getChavePrimariagvMenuTipoProjeto());
            int codigoGrupoMenu = int.Parse(getChavePrimaria());
            string descricaoOpcaoMenu = e.NewValues["DescricaoOpcaoMenu"].ToString();
            int sequenciaItemMenuGrupo = int.Parse(e.NewValues["SequenciaItemMenuGrupo"].ToString());

            bool nomeDisponivel = dsItemMenu.Tables[0].Select("CodigoItemMenu <> " + codigoItemMenu + " and DescricaoOpcaoMenu = '" + e.NewValues["DescricaoOpcaoMenu"] + "'").Length == 0;

            if (nomeDisponivel == false)
            {
                e.Cancel = false;
                gvMenuTipoProjeto.CancelEdit();
                throw new Exception(Resources.traducao.CadastroMenusObjetos_n_o_foi_poss_vel_editar_o_item_de_menu__o_nome_informado_j__existe_);
            }

            cDados.atualizaMenuTipoProjeto(codigoGrupoMenu, codigoItemMenu, descricaoOpcaoMenu, sequenciaItemMenuGrupo, ref mensagemErro);
            carregaGvItems();
        }
        e.Cancel = true;
        gvMenuTipoProjeto.CancelEdit();
    }
    protected void gvMenuTipoProjeto_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string mensagemErro = "";
        int codigoGrupoMenu = int.Parse(getChavePrimaria());
        int codigoItemMenu = int.Parse(getChavePrimariagvMenuTipoProjeto());

        cDados.excluiMenuTipoProjeto(codigoItemMenu, codigoGrupoMenu, ref mensagemErro);
        if (mensagemErro == "")
        {
            e.Cancel = true;
            gvMenuTipoProjeto.CancelEdit();
            carregaGvItems();
        }
        else
        {
            throw new Exception(mensagemErro);
        }

        
    }
    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string grupoAtivo = e.GetValue("IndicaGrupoVisivel").ToString();

            if (grupoAtivo == "N")
            {
                e.Row.ForeColor = Color.FromName("#914800");
            }
        }
    }
    protected void gvMenuTipoProjeto_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvItems();
    }
    protected void gvMenuTipoProjeto_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {

            e.Enabled = podeExcluir;
            if (e.Enabled == false)
            {
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        if (e.ButtonType == ColumnCommandButtonType.Edit)
        {

            e.Enabled = podeEditar;
            if (e.Enabled == false)
            {
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadMenuObj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "pcItemsMenus.Hide();onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "CadMenuObj", lblTituloTela.Text, this);
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "gvMenuTipoProjeto.AddNewRow();", true, true, false, "CadMenuObj", lblTituloTela.Text, this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void gvMenuTipoProjeto_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        if(!string.IsNullOrEmpty(msgErro))
            e.Properties["cp_msgErro"] = msgErro;
    }
}
