using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Utils;
using DevExpress.Web;

public partial class _Processos_Visualizacao_ListaConsultas : System.Web.UI.Page
{
    #region Constants

    private const string Const_Sim = "S";
    private const string Const_Nao = "N";
    private const string Const_SenhaAcesso = "Pa$$w0rd_CDIS";

    #endregion

    #region Fields

    dados cDados;
    int codigoUsuarioLogado;
    int codigoEntidade;
    bool exigirSenha = false;

    #endregion

    #region Properties

    private string _ConnectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
        set { _ConnectionString = value; }
    }

    #endregion

    #region Event Handlers

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
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        DefineConfiguracaoConexao();
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        Session["ce"] = codigoEntidade;

        this.TH(this.TS("ListaConsultas"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();

        }
        cDados.aplicaEstiloVisual(this);
        //Master.geraRastroSite();
        if (exigirSenha)
            RestringeAcessoPagina();
        defineAlturaTela();
        this.Page.Form.Attributes.Add("autocomplete", "off");
       
    }

    protected void defineAlturaTela()
    {
        /*<div id="divConteudo" runat="server" style="margin: auto; width: 98%;">*/

        int larguraTela = 0;
        int alturaTela = 0;

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out larguraTela, out alturaTela);

        alturaTela = alturaTela - 325;
        //larguraTela = larguraTela - x

        //divConteudo.Style.Add("margin", "auto");
        //divConteudo.Style.Add("width", "98%");
        //divConteudo.Style.Add("height", alturaTela + "px");
        //divConteudo.Style.Add("overflow", "auto");
        gvDados.Settings.VerticalScrollableHeight = alturaTela;
        gvDados.Width = new System.Web.UI.WebControls.Unit("98%");
        gvDados.SettingsPopup.EditForm.Width = new System.Web.UI.WebControls.Unit((larguraTela - 50).ToString() + "px");
        gvDados.SettingsPopup.EditForm.Height = new System.Web.UI.WebControls.Unit((alturaTela - 1).ToString() + "px");
        ((GridViewDataMemoColumn)gvDados.Columns["ComandoSelect"]).PropertiesMemoEdit.Height = new System.Web.UI.WebControls.Unit((alturaTela - 125).ToString() + "px");
    }

    protected void gvDados_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["IndicaOpcaoDisponivel"] = Const_Sim;
        e.NewValues["IndicaListaZebrada"] = Const_Nao;
    }



    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnAtualizarCampos")
        {
            if (gvDados.GetRowValues(e.VisibleIndex, gvDados.KeyFieldName) != null)
            {
                int codigoLista = (int)gvDados.GetRowValues(e.VisibleIndex, gvDados.KeyFieldName);
                string camandoSql = gvDados.GetRowValuesByKeyValue(codigoLista, "ComandoSelect") as string;
                e.Visible = string.IsNullOrWhiteSpace(camandoSql) ? DefaultBoolean.False : DefaultBoolean.True;
            }
        }
        if (e.ButtonID == "btnPopupFilter")
        {
            if (gvDados.GetRowValues(e.VisibleIndex, gvDados.KeyFieldName) != null)
            {
                int codigoLista = (int)gvDados.GetRowValues(e.VisibleIndex, gvDados.KeyFieldName);
                string camandoSql = gvDados.GetRowValuesByKeyValue(codigoLista, "ComandoSelect") as string;
                e.Visible = string.IsNullOrWhiteSpace(camandoSql) ? DefaultBoolean.False : DefaultBoolean.True;
            }
        }
    }

    protected void gvDetalhe_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        cDados.aplicaEstiloVisual(grid);
    }

    protected void gvDetalhe_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        e.NewValues["IndicaAreaFiltro"] = "N";
        e.NewValues["IndicaAreaDado"] = "N";
        e.NewValues["IndicaAreaColuna"] = "N";
        e.NewValues["IndicaAreaLinha"] = "N";
        e.NewValues["IndicaAgrupamento"] = "N";
        e.NewValues["IndicaCampoVisivel"] = "S";
        e.NewValues["IndicaCampoControle"] = "N";
        e.NewValues["IndicaLink"] = "N";
        e.NewValues["TipoFiltro"] = "E";
        e.NewValues["TipoTotalizador"] = "NENHUM";
        e.NewValues["AreaDefault"] = "L";
        e.NewValues["AlinhamentoCampo"] = "E";
        e.NewValues["IndicaCampoHierarquia"] = "N";
        e.NewValues["TipoCampo"] = "VAR";
        e.NewValues["OrdemCampo"] = grid.VisibleRowCount + 1;
        e.NewValues["OrdemAgrupamentoCampo"] = -1;
    }

    protected void gvDetalheFluxos_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        cDados.aplicaEstiloVisual(grid);
    }

    protected void gvDetalheFluxos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        if (grid.IsEditing && !grid.IsNewRowEditing)
            if (e.Column.FieldName == "CodigoFluxo")
                e.Editor.Enabled = false;
    }

    protected void gvDados_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        string camandoSql = gvDados.GetRowValuesByKeyValue(
            e.KeyValue, "ComandoSelect") as string;
        if (string.IsNullOrWhiteSpace(camandoSql))
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }

    protected void gvDados_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            string sql = gvDados.GetRowValues(i, "ComandoSelect") as string;
            if (string.IsNullOrWhiteSpace(sql))
                gvDados.DetailRows.CollapseRow(i);
        }
    }

    protected void gvDados_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string grupoPermissao = e.NewValues["GrupoPermissao"] as string;
        string itemPermissao = e.NewValues["ItemPermissao"] as string;
        string grupoPermissaoAntigo = e.OldValues["GrupoPermissao"] as string;
        string itemPermissaoAntigo = e.OldValues["ItemPermissao"] as string;
        if (grupoPermissao == grupoPermissaoAntigo && itemPermissao == itemPermissaoAntigo)
            return;

        string comandoSelect = string.Format(@"
 SELECT 1
   FROM PermissaoSistema AS ps 
  WHERE dbo.f_GetIniciaisTipoAssociacao(ps.CodigoTipoAssociacao) = 'EN'
    AND ps.DescricaoItemPermissao = '{0}'
    AND ps.DescricaoAcaoPermissao = '{1}'"
            , grupoPermissao
            , itemPermissao);
        DataSet dsTemp = cDados.getDataSet(comandoSelect);
        if (dsTemp.Tables[0].Rows.Count > 0)
        {
            string msgErro = string.Format(
                "Já existe um item de permissão '{0}' no grupo de permissao '{1}'"
                , itemPermissao, grupoPermissao);
            e.RowError = msgErro;
        }
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";

        if (e.NewValues["ComandoSelect"] != null)
        {
            string comandoSQL = e.NewValues["ComandoSelect"].ToString();
            const string valorNaoDefinido = "-1";
            comandoSQL = comandoSQL
                .Replace("{0}", cDados.getDbName())
                .Replace("{1}", cDados.getDbOwner())
                .Replace("{2}", /*codigoProjeto.HasValue ? codigoProjeto.ToString() :*/ valorNaoDefinido)
                .Replace("{3}", codigoEntidade.ToString())
                .Replace("{4}", codigoUsuarioLogado.ToString())
                .Replace("{5}", /*codigoFluxo.HasValue ? codigoFluxo.ToString() :*/ valorNaoDefinido)
                .Replace("{6}", /*codigoWorkflow.HasValue ? codigoWorkflow.ToString() :*/ valorNaoDefinido)
                .Replace("{7}", /*codigoInstancia.HasValue ? codigoInstancia.ToString() :*/ valorNaoDefinido)
                .Replace("{8}", /*codigoEtapa.HasValue ? codigoEtapa.ToString() :*/ valorNaoDefinido)
                .Replace("{9}", /*ocorrenciaAtual.HasValue ? ocorrenciaAtual.ToString() :*/ valorNaoDefinido)
                .Replace("{10}", cDados.getInfoSistema("CodigoCarteira").ToString());
            try
            {
                DataSet ds = cDados.getDataSet(comandoSQL);
                ((ASPxGridView)sender).JSProperties["cpSucesso"] = "Relatório alterado com sucesso!";
            }
            catch (Exception ex)
            {
                ((ASPxGridView)sender).JSProperties["cpErro"] = "Ocorreu um erro no comando de banco de dados.";
            }
        }


        if (e.NewValues["IniciaisPermissao"] == null)
        {
            string iniciaisPermissao = gvDados.GetRowValuesByKeyValue(
                e.Keys["CodigoLista"], "IniciaisPermissao") as string;
            e.NewValues["IniciaisPermissao"] = iniciaisPermissao;
        }


    }
    /*
        protected void gvDados_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            if (e.Expanded)
            {
                var row = ((DataRowView)gvDados.GetRow(e.VisibleIndex)).Row;
                string tipoLista = row.Field<string>("TipoLista").ToUpper();
                ASPxPageControl pageControl = (ASPxPageControl)
                    gvDados.FindDetailRowTemplateControl(e.VisibleIndex, "pageControl");
                pageControl.TabPages.FindByName("tabFluxos").Visible = tipoLista.Equals("PROCESSO");
                pageControl.TabPages.FindByName("tabDetalhe").Visible = !row.IsNull("CodigoSubLista"); 
            }
        }
    */
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.ErrorText) && e.Exception != null)
            e.ErrorText = "Ocorreu um erro ao atualizar os registros, verifique se o comando SQL está correto";// e.Exception.Message;
    }

    protected void pageControl_Init(object sender, EventArgs e)
    {
        int visibleIndex = gvDados.FocusedRowIndex;
        var row = ((DataRowView)gvDados.GetRow(visibleIndex)).Row;
        string tipoLista = row.Field<string>("TipoLista").ToUpper();
        ASPxPageControl pageControl = (ASPxPageControl)sender;
        pageControl.TabPages.FindByName("tabFluxos").ClientVisible = tipoLista.Equals("PROCESSO");
        pageControl.TabPages.FindByName("tabDetalhe").ClientVisible = !row.IsNull("CodigoSubLista");
    }

    protected void grid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var grid = (ASPxGridView)sender;
        Session["cl"] = grid.GetMasterRowKeyValue();
        Session["csl"] = grid.GetMasterRowFieldValues("CodigoSubLista");
    }

    #endregion

    #region Methods

    private void DefineConfiguracaoConexao()
    {
        sdsLista.ConnectionString = ConnectionString;
        sdsCamposLista.ConnectionString = ConnectionString;
        sdsFluxos.ConnectionString = ConnectionString;
        sdsFluxosLista.ConnectionString = ConnectionString;
        sdsDashboards.ConnectionString = ConnectionString;
        sdsRelatorios.ConnectionString = ConnectionString;
        sdsSubLista.ConnectionString = ConnectionString;
        sdsCamposLink.ConnectionString = ConnectionString;
        sdsCamposSubLista.ConnectionString = ConnectionString;
    }

    private IEnumerable<string> ObtemCamposDisponiveisPorLista(int codigoLista, ref string erro)
    {
        SetDefaultSessionInfo();
        string comandoOriginal = gvDados.GetRowValuesByKeyValue(codigoLista, "ComandoSelect") as string;
        //Remove 'dbname' e 'dbowner'
        comandoOriginal = Regex.Replace(comandoOriginal, @"\{[0-1]\}\.", string.Empty);
        //substitui os demais parâmetros por '1'
        comandoOriginal = Regex.Replace(comandoOriginal, @"\{\d+\}", "1");
        string comandoSql = string.Format("SELECT TOP 0 rs.* FROM ({0}) as rs", comandoOriginal);
        string connectionString = cDados.ConnectionString;
        try
        {
            return DataExtensions.GetColumnsNamesFromSelectCommand(comandoSql, connectionString);
        }
        catch
        {

            try
            {
                return DataExtensions.GetColumnsNamesFromSelectCommand(comandoOriginal, connectionString);
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return null;
            }
        }
    }

    private IEnumerable<string> ObtemCamposSelecionadosPorLista(int codigoLista)
    {
        string camandoSql = string.Format(@"
 SELECT NomeCampo 
   FROM ListaCampo
  WHERE CodigoLista = {0}", codigoLista);
        DataTable dt = cDados.getDataSet(camandoSql).Tables[0];
        return dt.AsEnumerable().Select(c => c.Field<string>("NomeCampo"));
    }

    private void RestringeAcessoPagina()
    {
        bool acessoPermitido = false;
        if (hfGeral.Contains("AcessoPermitido"))
            acessoPermitido = (bool)hfGeral.Get("AcessoPermitido");
        if (!acessoPermitido)
        {
            acessoPermitido = txtSenha.Text.Equals(Const_SenhaAcesso);
            hfGeral.Set("AcessoPermitido", acessoPermitido);
            lblMensagem.ClientVisible = IsPostBack && !acessoPermitido;
        }
        gvDados.ClientVisible = acessoPermitido;
        popup.ShowOnPageLoad = !acessoPermitido;
    }

    #endregion

    protected void callbackAtualizaCampos_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";


        int codigoLista = int.Parse(e.Parameter);
        var camposSelecionados = ObtemCamposSelecionadosPorLista(codigoLista);
        string erro_ObtemCamposDisponiveisPorLista = "";
        var camposDisponiveis = ObtemCamposDisponiveisPorLista(codigoLista, ref erro_ObtemCamposDisponiveisPorLista);
        if (camposDisponiveis == null)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = erro_ObtemCamposDisponiveisPorLista;
        }
        if (!string.IsNullOrEmpty(erro_ObtemCamposDisponiveisPorLista))
        {
            return;
        }

        StringBuilder comandoSql = new StringBuilder("DECLARE @OrdemCampo INT");
        foreach (string campo in camposDisponiveis.Where(cd => !camposSelecionados.Contains(cd)))
        {
            #region Comando SQL

            comandoSql.AppendFormat(@"

    SET @OrdemCampo = ISNULL((SELECT MAX(lc.OrdemCampo) FROM ListaCampo AS lc WHERE CodigoLista = {0}), 0) + 1

INSERT INTO [ListaCampo]
           ([CodigoLista]
           ,[NomeCampo]
           ,[TituloCampo]
           ,[OrdemCampo]
           ,[OrdemAgrupamentoCampo]
           ,[TipoCampo]
           ,[IndicaAreaFiltro]
           ,[TipoFiltro]
           ,[IndicaAgrupamento]
           ,[TipoTotalizador]
           ,[IndicaAreaDado]
           ,[IndicaAreaColuna]
           ,[IndicaAreaLinha]
           ,[AreaDefault]
           ,[IndicaCampoVisivel]
           ,[AlinhamentoCampo]
           ,[IndicaCampoHierarquia]
		   ,[IndicaColunaFixa])
     VALUES
           ({0}
           ,'{1}'
           ,'{1}'
           , @OrdemCampo
           ,-1
           ,'VAR'
           ,'S'
           ,'E'
           ,'N'
           ,'NENHUM'
           ,'N'
           ,'N'
           ,'N'
           ,'L'
           ,'S'
           ,'E'
           ,'N'
		   ,'N')

INSERT INTO [ListaCampoUsuario]([CodigoCampo],[CodigoListaUsuario],[OrdemCampo],[OrdemAgrupamentoCampo],[AreaDefault],[IndicaCampoVisivel])
SELECT SCOPE_Identity(), lcu.[CodigoListaUsuario],@OrdemCampo,-1,'L','N'
  FROM [ListaCampoUsuario] AS lcu
 WHERE [CodigoCampo] IN (SELECT [CodigoCampo] FROM ListaCampo AS lc WHERE lc.CodigoLista = {0})
 GROUP BY lcu.[CodigoListaUsuario]"
                , codigoLista
                , campo);

            #endregion
        }
        string strComandoSql = comandoSql.ToString();
        if (!string.IsNullOrWhiteSpace(strComandoSql))
        {
            int registrosAfetados = 0;
            try
            {
                bool retorno = cDados.execSQL(strComandoSql, ref registrosAfetados);
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Campos atualizados com sucesso!";
            }
            catch (Exception ex)
            {
                ((ASPxCallback)source).JSProperties["cpErro"] = "Ocorreu um erro no campo SQL do formulário.";
            }
        }
    }

    private void SetDefaultSessionInfo()
    {
        var data = new Dictionary<string, object>();
        data.Add("pa_CodigoEntidadeContexto", codigoEntidade);
        data.Add("pa_CodigoUsuarioSistema", codigoUsuarioLogado);
        DataExtensions.SetSessionInfo(data);
    }
}