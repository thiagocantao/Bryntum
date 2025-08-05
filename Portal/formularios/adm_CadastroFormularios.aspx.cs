using CDIS;
using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Formularios_adm_CadastroFormularios : System.Web.UI.Page
{
    dados cDados;
    ASPxGridView gvCampos_;
    object objCodigo;
    private char DelimitadorPropriedadeCampo = '¥';
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;

    private string dbName;
    private string dbOwner;

    private char delimitadorValores = '$';
    private char delimitadorElementoLista = '¢';
    public string ajudaMascara = "";
    public string entidadeDestino = "";
    public static DataSet dsComboLOOList;
    public static DataSet dsComboSubFormulario;
    public static DataSet dsComboVAO;
    public static ASPxComboBox comboSubFormularioGlobal;
    public static int codigoFormularioPaiGlobal = -1;
    bool temPermissaoDeEditarTagsForm = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        dsTipoCampo.ConnectionString = cDados.classeDados.getStringConexao();
        dsTipoFormulario.ConnectionString = cDados.classeDados.getStringConexao();

        //O trecho de código abaixo passou do Page_Load para o Page_Init para resolver o 
        //erro que acontecia ao tentar incluir um registro e logo em seguida incluir outro
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        //lblTituloTela.Text = "Administração de formulários";
        populaGridFormularios();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool indicaIdiomaPortugues = System.Globalization.CultureInfo.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        hfGeral.Set("indicaIdiomaPortugues", indicaIdiomaPortugues);
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioLogado, codigoEntidadeLogada, codigoEntidadeLogada, "null", "EN", 0, "null", "FO_Cad");
        }

        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());

        int codigoModelo = getChavePrimaria() == "" ? -1 : int.Parse(getChavePrimaria());

        populaGridTiposProjetos(codigoModelo);
        cDados.aplicaEstiloVisual(this);
        gvFormularios.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        gvFormularios.JSProperties["cp_Msg"] = "";

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        this.Title = cDados.getNomeSistema();
        populaComboEntidade();


        gvFormularios.JSProperties["cp_Ajuda"] = Resources.traducao.adm_CadastroFormularios_s_o_permitidos_apenas_os_caracteres____h_h_0_9__________ + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_e_mail_preencha_somente_com_o_caractere__ + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_hora__hh_mm__preencha_somente_com_o_caractere_h + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_n_meros_obrigat_rios_utilize_0 + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_n_meros_opcionais_utilize_9 + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_exemplo___99__0000_0000;


        temPermissaoDeEditarTagsForm = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioLogado, codigoEntidadeLogada, "EN", "EN_EdtTagFrm");

        ((GridViewDataCheckColumn)gvFormularios.Columns["IndicaControladoSistema"]).EditFormSettings.Visible = (temPermissaoDeEditarTagsForm == true) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataTextColumn)gvFormularios.Columns["IniciaisFormularioControladoSistema"]).EditFormSettings.Visible = (temPermissaoDeEditarTagsForm == true) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;


        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/FormularioTipoProjeto.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/FormularioCopiaFormulario.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_CadastroFormularios.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/uc_cfg_validacaoCampo.js""></script>"));
        this.TH(this.TS("FormularioTipoProjeto", "FormularioCopiaFormulario", "ASPxListbox", "adm_CadastroFormularios", "uc_cfg_validacaoCampo", "geral"));
        gvTiposProjetos.Settings.ShowFilterRow = false;
        gvTiposProjetos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvTiposProjetos.SettingsBehavior.AllowSort = false;
        gvTiposProjetos.SettingsBehavior.AllowDragDrop = false;

        gvFormularios.SettingsPager.Mode = GridViewPagerMode.ShowPager;
        gvFormularios.SettingsPager.PageSize = 50;


        if (gvCampos_ != null)
            gvCampos_.JSProperties["cp_Msg"] = "";
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvFormularios.Settings.VerticalScrollableHeight = altura - 100;
    }

    #region --- [Cadastro de Formulários]

    private void populaGridFormularios()
    {
        // 07/06/2010 : Modificado by Alejandro Fuentes: sentencia SQL, AND CodigoEntidade = {2} <- CodigoEntidadeLogada.
        string comandoSQL = string.Format(@"
        SELECT m.*, tf.DescricaoTipoFormulario
              ,CASE WHEN m.IndicaModeloPublicado = 'S' THEN '" + Resources.traducao.sim + @"' ELSE '" + Resources.traducao.nao + @"' END AS IndicaModeloPublicadoStr
              ,{0}.{1}.f_VerificaModeloFormularioAssinado(m.CodigoModeloFormulario) AS IndicaAssinado
        FROM {0}.{1}.modeloFormulario m INNER JOIN
             {0}.{1}.TipoFormulario tf ON tf.CodigoTipoFormulario = m.CodigoTipoFormulario
        WHERE dataExclusao is null
          AND CodigoEntidade = {2}
        ORDER BY NomeFormulario
        ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvFormularios.DataSource = ds;
        gvFormularios.DataBind();
    }

    private void populaGridCamposFormulario(int codigoModeloFormulario)
    {
        string comandoSQL = string.Format(
            @"SELECT cmf.CodigoCampo, cmf.NomeCampo, cmf.DescricaoCampo, cmf.CampoObrigatorio, cmf.CodigoTipoCampo,tc.DescricaoCampoUsuario, cmf.DefinicaoCampo, 
                     cmf.OrdemCampoFormulario, cmf.CodigoLookup, cmf.Aba, cmf.IndicaControladoSistema, cmf.IndicaCampoVisivelGrid, 
                     cmf.IniciaisCampoControladoSistema, cmf.IndicaCampoAtivo, cmf.CodigoModeloFormulario
                FROM {0}.{1}.CampoModeloFormulario cmf 
				inner join TipoCampo tc on (tc.CodigoTipoCampo = cmf.CodigoTipoCampo)
               WHERE cmf.codigoModeloFormulario = {2}
                 AND dataExclusao is null
               ORDER BY Aba, OrdemCampoFormulario", cDados.getDbName(), cDados.getDbOwner(), codigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvCampos_.DataSource = ds;
        gvCampos_.DataBind();
    }

    private void populaComboSubFormularios(ASPxComboBox combo, int codigoFormularioPai, string where)
    {
        string comandoSQL = string.Format(@"
              SELECT codigoModeloFormulario, nomeFormulario 
                FROM {0}.{1}.ModeloFormulario
               WHERE codigoModeloFormulario <> {2}
                 AND IndicaModeloPublicado = 'S'
                 AND IndicaToDoListAssociado = 'N'
                 AND CodigoTipoFormulario = 2
                 AND DataExclusao is null
                 AND CodigoEntidade = {3}
                 {4}
               ORDER BY nomeFormulario
        ", cDados.getDbName(), cDados.getDbOwner(), codigoFormularioPai, codigoEntidadeLogada, where);
        dsComboSubFormulario = cDados.getDataSet(comandoSQL);
        combo.DataSource = dsComboSubFormulario.Tables[0];
        combo.DataBind();
        comboSubFormularioGlobal = combo;
        codigoFormularioPaiGlobal = codigoFormularioPai;
    }

    private void populaComboVAO(ASPxComboBox combo, string filtro)
    {
        string comandoSQL = string.Format(@"SELECT [CodigoCampo] AS Codigo, [NomeCampo] AS [Descricao] 
                                                              FROM dbo.f_GetListaCamposDeProjeto({0},{1}) {2} ORDER BY 2 ASC", codigoEntidadeLogada, codigoUsuarioLogado, filtro);
        dsComboVAO = cDados.getDataSet(comandoSQL);
        combo.DataSource = dsComboVAO.Tables[0];
        combo.DataBind();
    }

    private void populaComboCampoPredefinido(ASPxComboBox combo)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoCampoPreDefinido as Codigo, NomeCampoPreDefinido as Descricao
                FROM {0}.{1}.CampoPreDefinido
               ORDER BY NomeCampoPreDefinido ", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds.Tables[0];
        combo.DataBind();
    }

    private void populaComboListaLookup(ASPxComboBox combo)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoLookup as Codigo, DescricaoLookup as Descricao
                FROM {0}.{1}.Lookup
               ORDER BY DescricaoLookup ", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds.Tables[0];
        combo.DataBind();
    }
    private void populaComboListaLookup(ASPxComboBox combo, string filtro)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoLookup as Codigo, DescricaoLookup as Descricao
                FROM {0}.{1}.Lookup {2}
               ORDER BY DescricaoLookup ", cDados.getDbName(), cDados.getDbOwner(), filtro);

        dsComboLOOList = cDados.getDataSet(comandoSQL);
        combo.DataSource = dsComboLOOList.Tables[0];
        combo.DataBind();
    }

    private void populaComboModeloFormulario(ASPxComboBox combo, int codigoFormularioPai)
    {
        string comandoSQL = string.Format(@"
              SELECT codigoModeloFormulario, nomeFormulario 
                FROM {0}.{1}.ModeloFormulario
               WHERE codigoModeloFormulario <> {2}
                 AND IndicaModeloPublicado = 'S'
                 AND CodigoTipoFormulario = 1
                 AND DataExclusao is null
                 AND CodigoEntidade = {3}
               ORDER BY nomeFormulario
        ", cDados.getDbName(), cDados.getDbOwner(), codigoFormularioPai, codigoEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds.Tables[0];
        combo.DataBind();
    }

    private void populaComboCampoModeloFormulario(ASPxComboBox combo, int codigoModeloFormulario)
    {
        string comandoSQL = string.Format(@"
              SELECT codigoCampo, nomeCampo
                FROM {0}.{1}.CampoModeloFormulario
               WHERE codigoModeloFormulario = {2}
                 AND DataExclusao is null
                 AND IndicaCampoAtivo = 'S'
               ORDER BY nomeCampo
        ", cDados.getDbName(), cDados.getDbOwner(), codigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds.Tables[0];
        combo.DataBind();
    }


    private DataTable getCamposNumericosFormulario(int codigoModeloFormulario)
    {
        string comandoSQL = string.Format(
           @"SELECT CodigoCampo, NomeCampo, null as ValorCampo
                FROM {0}.{1}.CampoModeloFormulario
               WHERE CodigoModeloFormulario = {2}
                 --AND CodigoTipoCampo in ('LOO', 'DAT', 'NUM')
                 AND CodigoTipoCampo in ('NUM')
                 AND DataExclusao is null
               ORDER BY NomeCampo", cDados.getDbName(), cDados.getDbOwner(), codigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds.Tables[0];
    }

    private void populaGridCamposCalculaveis(ASPxGridView grid, int codigoModeloFormulario)
    {
        //        string comandoSQL = string.Format(
        //            @"SELECT CodigoCampo, NomeCampo, null as ValorCampo
        //                FROM {0}.{1}.CampoModeloFormulario
        //               WHERE CodigoModeloFormulario = {2}
        //                 --AND CodigoTipoCampo in ('LOO', 'DAT', 'NUM')
        //                 AND CodigoTipoCampo in ('NUM')
        //                 AND DataExclusao is null
        //               ORDER BY NomeCampo", cDados.getDbName(), cDados.getDbOwner(), codigoModeloFormulario);
        //        DataSet ds = cDados.getDataSet(comandoSQL);
        //        grid.DataSource = ds.Tables[0];
        grid.DataSource = getCamposNumericosFormulario(codigoModeloFormulario);
        grid.DataBind();
    }

    protected void gvFormularios_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        // 07/06/2010 : Modificado by Alejandro Fuentes: sentencia SQL, CodigoEntidade <- {6} <- CodigoEntidadeLogada.
        string Abas = "";
        if (e.NewValues["Abas"] != null)
            Abas = e.NewValues["Abas"].ToString().Trim().Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        else // se o usuário não informou nenhuma aba, vamos inserir uma com o nome Principal
            Abas = Resources.traducao.adm_CadastroFormularios_principal;


        string comandoSQL = string.Format(@"
            BEGIN 
                DECLARE @CodigoModeloFormulario int

                INSERT INTO ModeloFormulario (  NomeFormulario, DescricaoFormulario, IndicaControladoSistema
                                          , CodigoTipoFormulario, DataInclusao, IncluidoPor, Abas
                                          , IndicaToDoListAssociado, IndicaAnexoAssociado, CodigoEntidade, IniciaisFormularioControladoSistema)
                VALUES ('{0}', '{1}', '{9}', {2}, getDate(), 1, '{3}', '{4}', '{5}', {6}, '{10}')

                SET @CodigoModeloFormulario = scope_identity()

                INSERT INTO CampoModeloFormulario (CodigoModeloFormulario, NomeCampo, DescricaoCampo, CampoObrigatorio
                                                  , OrdemCampoFormulario, CodigoTipoCampo, DefinicaoCampo, DataInclusao
                                                  , IncluidoPor, IndicaControladoSistema, Aba
                                                  , IniciaisCampoControladoSistema, IndicaCampoVisivelGrid)
                VALUES (@CodigoModeloFormulario, '{7}', '{8}', 'S',
                        1, 'VAR', 'Tam: 50¥', getDate(), 1, 'S', 0, 'DESC', 'S') 

            END 
             ", e.NewValues["NomeFormulario"] != null ? e.NewValues["NomeFormulario"].ToString().Replace("'","''") : "",
                e.NewValues["DescricaoFormulario"] != null ? e.NewValues["DescricaoFormulario"].ToString().Replace("'", "''") : "",
                e.NewValues["CodigoTipoFormulario"] != null ? e.NewValues["CodigoTipoFormulario"].ToString() : "",
                Abas,
                e.NewValues["IndicaToDoListAssociado"] != null ? e.NewValues["IndicaToDoListAssociado"].ToString() : "N",
                e.NewValues["IndicaAnexoAssociado"] != null ? e.NewValues["IndicaAnexoAssociado"].ToString() : "N",
                codigoEntidadeLogada,
                 Resources.traducao.adm_CadastroFormularios_descri__o,
                Resources.traducao.adm_CadastroFormularios_descri__o_do_formul_rio,
                e.NewValues["IndicaControladoSistema"] != null ? e.NewValues["IndicaControladoSistema"].ToString() : "N",
                e.NewValues["IniciaisFormularioControladoSistema"] != null ? e.NewValues["IniciaisFormularioControladoSistema"].ToString() : "N");
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
        e.Cancel = true;
        gvFormularios.CancelEdit();
        populaGridFormularios();
    }

    protected void gvFormularios_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string Abas = "";
        if (e.NewValues["Abas"] != null)
            Abas = e.NewValues["Abas"].ToString().Trim().Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        else // se o usuário não informou nenhuma aba, vamos inserir uma com o nome Principal
            Abas = Resources.traducao.adm_CadastroFormularios_principal;

        // vamos ver a quantidade de abas do formulário
        string[] aAbas = Abas.Split('\r');
        int qtdeMaximaAbas = 0;
        foreach (string aba in aAbas)
        {
            if (aba.Trim() != "")
                qtdeMaximaAbas++;
        }

        string comandoSQL = string.Format(
                @"BEGIN
                    UPDATE ModeloFormulario 
                       SET NomeFormulario = '{1}'
                         , DescricaoFormulario = '{2}' 
                         , CodigoTipoFormulario = {3}
                         , DataUltimaAlteracao = getdate()
                         , AlteradoPor = 1
                         , Abas = '{4}'
                         , IndicaToDoListAssociado = '{6}'
                         , IndicaAnexoAssociado = '{7}'
                         , IniciaisFormularioControladoSistema = '{8}'
                         , IndicaControladoSistema = '{9}'
                   WHERE CodigoModeloFormulario = {0}

                   UPDATE CampoModeloFormulario
                      SET Aba = 0 
                    WHERE codigoModeloFormulario = {0}
                      AND (Aba is null or Aba > {5} )

                END",
                e.Keys[0].ToString(),
                e.NewValues["NomeFormulario"] != null ? e.NewValues["NomeFormulario"].ToString().Replace("'", "''") : "",
                e.NewValues["DescricaoFormulario"] != null ? e.NewValues["DescricaoFormulario"].ToString().Replace("'", "''") : "",
                e.NewValues["CodigoTipoFormulario"].ToString(),
                Abas,
                qtdeMaximaAbas - 1,
                e.NewValues["IndicaToDoListAssociado"] != null ? e.NewValues["IndicaToDoListAssociado"].ToString() : "N",
                e.NewValues["IndicaAnexoAssociado"] != null ? e.NewValues["IndicaAnexoAssociado"].ToString() : "N",
                e.NewValues["IniciaisFormularioControladoSistema"] != null ? e.NewValues["IniciaisFormularioControladoSistema"].ToString() : "",
                e.NewValues["IndicaControladoSistema"] != null ? e.NewValues["IndicaControladoSistema"].ToString() : "N");
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
        e.Cancel = true;
        gvFormularios.CancelEdit();
        populaGridFormularios();
    }

    protected void gvFormularios_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string msgErro = "";
        bool podeExcluir = VerificaPodeExcluirFormulario(Convert.ToInt32(e.Keys[0]), out msgErro);
        if (!podeExcluir)
        {
            //string script = string.Format("<script language=JavaScript> window.top.mostraMensagem('Não foi possível excluír o formulário selecionado:{0}', 'sucesso', false, false, null);</script>", msgErro);
            //System.Web.HttpContext.Current.Response.Write(script);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", script, false);
            //Show(msgErro);
            msgErro = Resources.traducao.adm_CadastroFormularios_n_o_foi_poss_vel_excluir_o_formul_rio_selecionado__n + msgErro;
            gvFormularios.JSProperties["cp_Erro"] = msgErro;
            e.Cancel = true;
        }
        else
        {
            string comandoSQL = string.Format(
            @"UPDATE ModeloFormulario 
                 SET DataExclusao = getdate()
                   , ExcluidoPor = 1
               WHERE CodigoModeloFormulario = {0}
              ", e.Keys[0].ToString());
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
            e.Cancel = true;
            populaGridFormularios();
            gvFormularios.JSProperties["cp_Sucesso"] = Resources.traducao.adm_CadastroFormularios_formul_rio_exclu_do_com_sucesso_;
        }

    }

    private bool VerificaPodeExcluirFormulario(int codigoFormulario, out string msgErro)
    {
        msgErro = string.Empty;
        string comandoSql = string.Format(@"
DECLARE @CodigoModeloFormulario INT 
    SET @CodigoModeloFormulario = {0}
 SELECT 1 
   FROM Formulario
  WHERE CodigoModeloFormulario = @CodigoModeloFormulario  
 SELECT 1 
   FROM FormulariosEtapasWf
  WHERE CodigoModeloFormulario = @CodigoModeloFormulario", codigoFormulario);
        DataSet ds = cDados.getDataSet(comandoSql);
        if (ds.Tables[0].Rows.Count > 0)
            msgErro += Resources.traducao.adm_CadastroFormularios__n____o_formul_rio_possui_inst_ncias_criadas_com_dados_preenchidos_;
        if (ds.Tables[1].Rows.Count > 0)
            msgErro += Resources.traducao.adm_CadastroFormularios__n____o_formul_rio_est__relacionado_a_fluxos_;

        bool podeExcluir = string.IsNullOrEmpty(msgErro);
        return podeExcluir;
    }



    protected void gvFormularios_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        string indicaAssinado = "N";
        string codigoTipoFormulario = "";
        if (e.VisibleIndex < 0)
            return;
        if ((sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaAssinado") != null)
        {
            indicaAssinado = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaAssinado").ToString();
        }
        if ((sender as ASPxGridView).GetRowValues(e.VisibleIndex, "CodigoTipoFormulario") != null)
        {
            codigoTipoFormulario = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "CodigoTipoFormulario").ToString();
        }

        if (indicaAssinado == "S" || codigoTipoFormulario == "4")
        {
            e.Enabled = false;
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.Url = "~/imagens/botoes/editarRegDes.PNG";
                if (codigoTipoFormulario == "4")
                {
                    e.Image.ToolTip = "Nesse modelo de formulário, é possível apenas incluir ou excluir campos.";
                }
            }
        }
    }


    protected void gvFormularios_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "btnPublicar")
        {
            int CodigoModeloFormulario = int.Parse((sender as ASPxGridView).GetRowValues(e.VisibleIndex, "CodigoModeloFormulario").ToString());
            string NomeModeloFormulário = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "NomeFormulario").ToString();
            string mensagem = publicarFormulario(CodigoModeloFormulario);
            if (mensagem == "")
            {
                mensagem = Resources.traducao.formul_rio + " \"" + NomeModeloFormulário + "\" " + Resources.traducao.adm_CadastroFormularios_publicado_com_sucesso_;
                gvFormularios.JSProperties["cp_Msg"] = mensagem;
                populaGridFormularios();
            }
            else
            {
                gvFormularios.JSProperties["cp_Erro"] = mensagem;
            }



        }
    }

    protected void gvCampos_BeforePerformDataSelect(object sender, EventArgs e)
    {
        // Este evento ocorre antes da grid gvCampos receber os dados do select que a popula
        // como é um master-detail, antes de popularmos o detail, temos que o obter o código (keyFieldName) da grid master
        objCodigo = (sender as ASPxGridView).GetMasterRowKeyValue();

        // obter a relação de abas do formulário selecionado
        string Abas = (sender as ASPxGridView).GetMasterRowFieldValues("Abas").ToString().Replace("\r\n", "\r");
        string[] aAbas = Abas.Split('\r');
        int seqAba = 0;

        ((GridViewDataComboBoxColumn)(sender as ASPxGridView).Columns["Aba"]).PropertiesComboBox.Items.Clear();
        foreach (string aba in aAbas)
        {
            if (aba.Trim() != "")
                ((GridViewDataComboBoxColumn)(sender as ASPxGridView).Columns["Aba"]).PropertiesComboBox.Items.Add(aba.Trim(), seqAba++);
        }
    }

    protected void gvFormularios_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            //hfIndexFormulario.Value = e.VisibleIndex.ToString();
            // procura pela grid "Campos" dentro do detailRow da grid Formularios
            gvCampos_ = gvFormularios.FindDetailRowTemplateControl(e.VisibleIndex, "gvCampos") as ASPxGridView;
            string indicaAssinado = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaAssinado").ToString();


            if (gvCampos_ != null)
            {
                var parent = gvCampos_.Parent;
                cDados.traduzControles(parent, new Control[] { gvCampos_ });
                gvCampos_.Settings.ShowFilterRow = false;
                gvCampos_.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                gvCampos_.SettingsBehavior.AllowSort = false;
                gvCampos_.SettingsBehavior.AllowDragDrop = false;
                bool temPermissaoDeEditarTagsForm = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioLogado, codigoEntidadeLogada, "EN", "EN_EdtTagFrm");
                ((GridViewDataCheckColumn)gvCampos_.Columns["IndicaControladoSistema"]).EditFormSettings.Visible = (temPermissaoDeEditarTagsForm == true) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                ((GridViewDataTextColumn)gvCampos_.Columns["IniciaisCampoControladoSistema"]).EditFormSettings.Visible = (temPermissaoDeEditarTagsForm == true) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

                if (indicaAssinado == "S")
                {
                    gvCampos_.Columns[1].CellStyle.BorderLeft.BorderStyle = BorderStyle.Solid;
                    gvCampos_.Columns[0].Visible = false;
                }

                // a variavel "objCodigo" é lida no evento "gvCampos_BeforePerformDataSelect"
                if (objCodigo != null)
                {
                    populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
                    gvCampos_.DataBind();

                    // busca a lista de abas do formulário selecionado
                    DataRowView drv = (DataRowView)gvFormularios.GetRow(e.VisibleIndex);
                    string Abas = drv["Abas"].ToString();
                    ((GridViewDataComboBoxColumn)gvCampos_.Columns["Aba"]).PropertiesComboBox.Items.Add("Aba 1_", 3);
                    ((GridViewDataComboBoxColumn)gvCampos_.Columns["Aba"]).PropertiesComboBox.Items.Add("Aba 1_", 4);
                    // busca a lista de campos para o formulário selecionado.

                    // guarda a linha selecionada na grid de Formularios
                    //ASPxHiddenField hfControle_ = gvCampos_.FindControl("hfControle") as ASPxHiddenField;
                    //gvCampos_.ToolTip = Abas;
                    //hfIndexFormulario.Value = e.VisibleIndex.ToString();
                }
            }
        }
    }

    protected void gvCampos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //object objCodigoMaster = (sender as ASPxGridView).GetMasterRowKeyValue();
        try
        {
            int OrdemCampo = 1;
            if (e.NewValues.Contains("OrdemCampoFormulario") && e.NewValues["OrdemCampoFormulario"] != null)
                int.TryParse(e.NewValues["OrdemCampoFormulario"].ToString(), out OrdemCampo);

            char campoVisivel = 'N';
            if (e.NewValues.Contains("IndicaCampoVisivelGrid") && e.NewValues["IndicaCampoVisivelGrid"] != null && e.NewValues["CodigoTipoCampo"].ToString() != "LNP")
                campoVisivel = e.NewValues["IndicaCampoVisivelGrid"].ToString()[0];

            /*
             e.NewValues["IniciaisCampoControladoSistema"]
e.NewValues["IndicaControladoSistema"]
             */

            string comandoSQL = string.Format(
                    @"INSERT INTO CampoModeloFormulario (CodigoModeloFormulario, NomeCampo, DescricaoCampo, CampoObrigatorio, 
                         OrdemCampoFormulario, CodigoTipoCampo, DefinicaoCampo, DataInclusao, IncluidoPor, Aba, IndicaCampoVisivelGrid, IniciaisCampoControladoSistema, IndicaControladoSistema, LarguraSpan)
                  VALUES ({0}, '{1}', '{2}', '{3}',
                         {4}, '{5}', '{8}', getDate(), 1, '{6}', '{7}', '{9}', '{10}', {11})

                 DECLARE @CodigoModeloFormulario INT
                     SET @CodigoModeloFormulario = {0}
                  
                 UPDATE ModeloFormulario
	                SET IndicaModeloPublicado = 'N'
                  WHERE CodigoModeloFormulario = @CodigoModeloFormulario",
                    int.Parse(objCodigo.ToString()),
                    e.NewValues["NomeCampo"].ToString(),
                    e.NewValues["DescricaoCampo"] != null ? e.NewValues["DescricaoCampo"].ToString() : "",
                    e.NewValues["CampoObrigatorio"].ToString(),
                    OrdemCampo,
                    e.NewValues["CodigoTipoCampo"].ToString(),
                    e.NewValues["Aba"] != null ? e.NewValues["Aba"].ToString() : "0",
                    campoVisivel,
                    e.NewValues["CodigoTipoCampo"].ToString() == "LNP" ? "Tam: 4000¥" : "",
                    e.NewValues["IniciaisCampoControladoSistema"] != null ? e.NewValues["IniciaisCampoControladoSistema"].ToString() : "",
                    e.NewValues["IndicaControladoSistema"] != null ? e.NewValues["IndicaControladoSistema"].ToString() : "N",
                    e.NewValues["LarguraSpan"] != null ? e.NewValues["LarguraSpan"].ToString() : "NULL");
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
            e.Cancel = true;
            gvCampos_ = (sender as ASPxGridView);
            gvCampos_.CancelEdit();
            populaGridCamposFormulario(int.Parse(objCodigo.ToString()));

            gvCampos_.AddNewRow();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void gvCampos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            gvCampos_ = (sender as ASPxGridView);
            string[] aCampos = new string[e.NewValues.Count];
            e.NewValues.Keys.CopyTo(aCampos, 0);
            string camposAlterados = "";
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                camposAlterados += ", " + aCampos[i] + " = '" + ((e.NewValues[i] != null) == true ? e.NewValues[i].ToString() : "") + "' ";
            }

            // se o tipo foi alterado
            if (e.NewValues.Contains("CodigoTipoCampo"))
            {
                if (e.NewValues["CodigoTipoCampo"].ToString() != e.OldValues["CodigoTipoCampo"].ToString())
                    camposAlterados += ", DefinicaoCampo = '' ";
            }
            string comandoSQL = string.Format(
            @"UPDATE CampoModeloFormulario 
                     SET DataUltimaAlteracao = getdate()
                       , AlteradoPor = {2}
                       {1}
                  WHERE CodigoCampo = {0}
            
             DECLARE @CodigoModeloFormulario INT

             SELECT @CodigoModeloFormulario = CodigoModeloFormulario
               FROM CampoModeloFormulario
              WHERE CodigoCampo = {0}
              
             UPDATE ModeloFormulario
	            SET IndicaModeloPublicado = 'N'
              WHERE CodigoModeloFormulario = @CodigoModeloFormulario",
            e.Keys[0].ToString(), camposAlterados, codigoUsuarioLogado);
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
            e.Cancel = true;

            gvCampos_.CancelEdit();
            populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void gvCampos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            string comandoSQL = string.Format(
            @"UPDATE CampoModeloFormulario 
                     SET DataExclusao = getdate()
                       , ExcluidoPor = {1}
                  WHERE CodigoCampo = {0}

             DECLARE @CodigoModeloFormulario INT

             SELECT @CodigoModeloFormulario = CodigoModeloFormulario
               FROM CampoModeloFormulario
              WHERE CodigoCampo = {0}
              
             UPDATE ModeloFormulario
	            SET IndicaModeloPublicado = 'N'
              WHERE CodigoModeloFormulario = @CodigoModeloFormulario",
            e.Keys[0].ToString(), codigoUsuarioLogado);
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
            e.Cancel = true;
            gvCampos_ = (sender as ASPxGridView);
            gvCampos_.CancelEdit();
            populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void gvCampos_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            ASPxGridView gridCampos = (sender as ASPxGridView);
            ASPxGridView gridFormularios = (((sender as ASPxGridView).Parent) as GridViewDetailRowTemplateContainer).Grid;
            int index = (((sender as ASPxGridView).Parent) as GridViewDetailRowTemplateContainer).ItemIndex;
            bool indicaAssinado = gridFormularios.GetRowValues(index, "IndicaAssinado").ToString() == "S";

            gridCampos.FocusedRowIndex = e.VisibleIndex;
            gridCampos.ScrollToVisibleIndexOnClient = e.VisibleIndex;
            string codigoTipoCampo = gridCampos.GetRowValues(e.VisibleIndex, "CodigoTipoCampo").ToString();
            string codigoCampo = gridCampos.GetRowValues(e.VisibleIndex, "CodigoCampo").ToString();
            string IniciaisCampoControladoSistema = gridCampos.GetRowValues(e.VisibleIndex, "IniciaisCampoControladoSistema").ToString();
            string codigoModeloFormulario = objCodigo.ToString();

            ASPxHiddenField hfControle_ = gridCampos.FindDetailRowTemplateControl(e.VisibleIndex, "hfControle") as ASPxHiddenField;

            hfControle_.Set("codigoCampo", codigoCampo);
            hfControle_.Set("CodigoTipoCampo", codigoTipoCampo);
            hfControle_.Set("indexLinhaCampos", e.VisibleIndex);
            hfControle_.Set("cogigoFormulario", codigoModeloFormulario);

            ASPxPanel divTipoCampoSelecionado = gridCampos.FindDetailRowTemplateControl(e.VisibleIndex, "dv" + codigoTipoCampo) as ASPxPanel;

            cDados.aplicaEstiloVisual(divTipoCampoSelecionado);



            if (divTipoCampoSelecionado != null)
            {
                divTipoCampoSelecionado.Visible = true;
                divTipoCampoSelecionado.Styles.Disabled.BackColor = Color.FromName("EBEBEB");
                divTipoCampoSelecionado.Styles.Disabled.ForeColor = Color.Black;

                if (indicaAssinado)
                    divTipoCampoSelecionado.Enabled = false;

                string definicaoCampo = gridCampos.GetRowValues(e.VisibleIndex, "DefinicaoCampo").ToString().Trim();

                if (codigoTipoCampo == "TXT")
                {
                    // se for o campo "Descrição", não poderá ter a opção "Formatação Simples Habilitada"
                    //TODO 3. Não permite alterar o campo na Tag de controle e não comunica o usuário com uma mensagem;
                    if (IniciaisCampoControladoSistema == "DESC")
                    {
                        (divTipoCampoSelecionado.FindControl("rb_TXT_FormatacaoSimples") as ASPxRadioButton).ClientEnabled = false;
                        (divTipoCampoSelecionado.FindControl("rb_TXT_FormatacaoSimples") as ASPxRadioButton).ToolTip = Resources.traducao.adm_CadastroFormularios_o_campo_com_tag_de_controle_desc_n_o_pode_utilizar_formata__o_;
                    }
                }

                if (definicaoCampo != "" || codigoTipoCampo == "SUB" || codigoTipoCampo == "CPD" || codigoTipoCampo == "LOO" || codigoTipoCampo == "CAL" || codigoTipoCampo == "VAO")
                {
                    string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);

                    if (codigoTipoCampo == "VAR")
                    {
                        string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string mascara = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string padrao = aDefinicaoCampo.Length > 2 ? aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim() : "";
                        (divTipoCampoSelecionado.FindControl("txt_VAR_tamanho") as ASPxTextBox).Value = tamanho;
                        (divTipoCampoSelecionado.FindControl("txt_VAR_mascara") as ASPxTextBox).Value = mascara;
                        (divTipoCampoSelecionado.FindControl("txt_VAR_padrao") as ASPxTextBox).Value = padrao;
                        (divTipoCampoSelecionado.FindControl("imgAjudaMascara") as ASPxImage).ToolTip = Resources.traducao.adm_CadastroFormularios_s_o_permitidos_apenas_os_caracteres____h_h_0_9__________ + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_e_mail_preencha_somente_com_o_caractere__ + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_hora__hh_mm__preencha_somente_com_o_caractere_h + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_n_meros_obrigat_rios_utilize_0 + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_para_n_meros_opcionais_utilize_9 + Environment.NewLine +
                                                                                                        Resources.traducao.adm_CadastroFormularios_exemplo___99__0000_0000;

                    }
                    else if (codigoTipoCampo == "TXT")
                    {
                        // se for o campo "Descrição", não poderá ter a opção "Formatação Simples Habilitada"
                        //if (IniciaisCampoControladoSistema == "DESC")
                        // {
                        //     (divTipoCampoSelecionado.FindControl("rb_TXT_FormatacaoSimples") as ASPxRadioButton).ClientEnabled = false;
                        // }

                        string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string linhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string formato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        string padrao = aDefinicaoCampo.Length > 3 ? aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim() : "";

                        (divTipoCampoSelecionado.FindControl("txt_TXT_tamanho") as ASPxTextBox).Value = tamanho;
                        (divTipoCampoSelecionado.FindControl("txt_TXT_linhas") as ASPxTextBox).Value = linhas;
                        (divTipoCampoSelecionado.FindControl("txt_TXT_padrao") as ASPxMemo).Value = padrao;
                        if (formato == "0")
                            (divTipoCampoSelecionado.FindControl("rb_TXT_SemFormatacao") as ASPxRadioButton).Checked = true;
                        else if (formato == "1")
                            (divTipoCampoSelecionado.FindControl("rb_TXT_FormatacaoSimples") as ASPxRadioButton).Checked = true;
                        else if (formato == "2")
                            (divTipoCampoSelecionado.FindControl("rb_TXT_FormatacaoAvancada") as ASPxRadioButton).Checked = true;
                    }
                    else if (codigoTipoCampo == "NUM")
                    {
                        string minimo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string maximo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string precisao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        string formato = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                        string agregacao = "";
                        if (aDefinicaoCampo[4] != "")
                            agregacao = aDefinicaoCampo[4].Substring(aDefinicaoCampo[4].IndexOf(":") + 1).Trim();
                        string padrao = aDefinicaoCampo.Length > 5 ? aDefinicaoCampo[5].Substring(aDefinicaoCampo[5].IndexOf(":") + 1).Trim() : "";

                        (divTipoCampoSelecionado.FindControl("txt_NUM_Minimo") as ASPxSpinEdit).Value = minimo;
                        (divTipoCampoSelecionado.FindControl("txt_NUM_Maximo") as ASPxSpinEdit).Value = maximo;
                        (divTipoCampoSelecionado.FindControl("ddl_NUM_Precisao") as ASPxComboBox).Value = precisao;
                        (divTipoCampoSelecionado.FindControl("ddl_NUM_Formato") as ASPxComboBox).Value = formato;
                        (divTipoCampoSelecionado.FindControl("ddl_NUM_Agregacao") as ASPxComboBox).Value = agregacao;
                        (divTipoCampoSelecionado.FindControl("txt_NUM_padrao") as ASPxSpinEdit).Value = padrao;
                    }
                    else if (codigoTipoCampo == "LST")
                    {
                        string opcoes = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string tamanhoLista = "";

                        if (aDefinicaoCampo.Length > 2 && aDefinicaoCampo[2] != "")
                            tamanhoLista = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        string padrao = aDefinicaoCampo.Length > 3 ? aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim() : "";

                        (divTipoCampoSelecionado.FindControl("txt_LST_Opcoes") as ASPxMemo).Value = opcoes;
                        if (formato == "0")
                            (divTipoCampoSelecionado.FindControl("rb_LST_Combo") as ASPxRadioButton).Checked = true;
                        else if (formato == "1")
                            (divTipoCampoSelecionado.FindControl("rb_LST_Radio") as ASPxRadioButton).Checked = true;
                        else if (formato == "2")
                            (divTipoCampoSelecionado.FindControl("rb_LST_Check") as ASPxRadioButton).Checked = true;

                        (divTipoCampoSelecionado.FindControl("txt_LST_tamanho") as ASPxTextBox).Text = tamanhoLista;
                        (divTipoCampoSelecionado.FindControl("txt_LST_padrao") as ASPxTextBox).Text = padrao;
                    }
                    else if (codigoTipoCampo == "DAT")
                    {
                        string incluirHora = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string valorInicial = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        if (incluirHora == "S")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Sim") as ASPxRadioButton).Checked = true;
                        else if (incluirHora == "N")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Nao") as ASPxRadioButton).Checked = true;

                        if (valorInicial == "B")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Branco") as ASPxRadioButton).Checked = true;
                        if (valorInicial == "A")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Atual") as ASPxRadioButton).Checked = true;
                    }
                    else if (codigoTipoCampo == "BOL")
                    {
                        string textoVerdadeiro = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string valorVerdadeiro = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string textoFalso = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        string valorFalso = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                        (divTipoCampoSelecionado.FindControl("txt_BOL_TextoVerdadeiro") as ASPxTextBox).Value = textoVerdadeiro;
                        (divTipoCampoSelecionado.FindControl("txt_BOL_ValorVerdadeiro") as ASPxTextBox).Value = valorVerdadeiro;
                        (divTipoCampoSelecionado.FindControl("txt_BOL_TextoFalso") as ASPxTextBox).Value = textoFalso;
                        (divTipoCampoSelecionado.FindControl("txt_BOL_ValorFalso") as ASPxTextBox).Value = valorFalso;
                    }
                    else if (codigoTipoCampo == "SUB")
                    {
                        string codigoSubFormulario = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        // popula o combo de subFormularios
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_SUB_Formulario") as ASPxComboBox;
                        if (combo != null)
                        {
                            populaComboSubFormularios(combo, int.Parse(objCodigo.ToString()), "");
                            combo.ToolTip = Resources.traducao.adm_CadastroFormularios_ser_o_listados_aqui_formul_rios_do_tipo_lista__publicados_e_que_n_o_tenham_tarefas_de_to_do_list_associadas;
                            if (codigoSubFormulario != "")
                            {
                                combo.Value = int.Parse(codigoSubFormulario);
                            }
                        }
                    }
                    else if (codigoTipoCampo == "CPD")
                    {
                        string codigoCampoPre = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string tamanho = "";
                        string linhas = "1";
                        if (aDefinicaoCampo.Length > 1 && aDefinicaoCampo[1] != "")
                            linhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

                        if (aDefinicaoCampo.Length > 2 && aDefinicaoCampo[2] != "")
                            tamanho = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();

                        // popula o combo de campos pré-definidos
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_CPD_campoPre") as ASPxComboBox;
                        (divTipoCampoSelecionado.FindControl("txt_CPD_linhas") as ASPxTextBox).Value = linhas;
                        (divTipoCampoSelecionado.FindControl("txt_CPD_tamanho") as ASPxTextBox).Value = tamanho;
                        if (combo != null)
                        {
                            populaComboCampoPredefinido(combo);
                            if (codigoCampoPre != "")
                                combo.Value = int.Parse(codigoCampoPre);
                        }
                    }
                    else if (codigoTipoCampo == "LOO")
                    {
                        string codigoLista = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string tamanho = "";
                        bool mostrarComoLOV = false;
                        if (aDefinicaoCampo.Length > 1 && aDefinicaoCampo[1] != "")
                            tamanho = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

                        if (aDefinicaoCampo.Length > 2 && aDefinicaoCampo[2] != "")
                            mostrarComoLOV = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim().ToUpper() == "S";

                        // popula o combo de subFormularios
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_LOO_ListaPre") as ASPxComboBox;
                        if (combo != null)
                        {
                            populaComboListaLookup(combo);
                            if (codigoLista != "")
                                combo.Value = int.Parse(codigoLista);
                        }

                        (divTipoCampoSelecionado.FindControl("txt_LOO_tamanho") as ASPxTextBox).Value = tamanho;
                        if (mostrarComoLOV)
                            (divTipoCampoSelecionado.FindControl("rb_LOO_Lov") as ASPxRadioButton).Checked = true;
                        else
                            (divTipoCampoSelecionado.FindControl("rb_LOO_Combo") as ASPxRadioButton).Checked = true;
                    }
                    else if (codigoTipoCampo == "REF")
                    {
                        string codigoModelo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string codigoCampoModelo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string somenteLeitura = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        string tituloExterno = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                        string tituloInterno = aDefinicaoCampo[4].Substring(aDefinicaoCampo[4].IndexOf(":") + 1).Trim();
                        // popula o combo de Formularios
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_REF_ModeloFormulario") as ASPxComboBox;
                        if (combo != null)
                        {
                            populaComboModeloFormulario(combo, int.Parse(objCodigo.ToString()));
                            if (codigoModelo != "")
                                combo.Value = int.Parse(codigoModelo);
                        }

                        if (codigoModelo != "")
                        {
                            // popula o combo de campos
                            ASPxComboBox combo2 = divTipoCampoSelecionado.FindControl("ddl_REF_CampoFormulario") as ASPxComboBox;
                            if (combo2 != null)
                            {
                                populaComboCampoModeloFormulario(combo2, int.Parse(codigoModelo));
                                if (codigoModelo != "")
                                    combo2.Value = int.Parse(codigoCampoModelo);
                            }
                        }
                        (divTipoCampoSelecionado.FindControl("ddl_REF_SomenteLeitura") as ASPxComboBox).Value = somenteLeitura;
                        (divTipoCampoSelecionado.FindControl("ddl_REF_TituloExterno") as ASPxComboBox).Value = tituloExterno;
                        (divTipoCampoSelecionado.FindControl("ddl_REF_TituloInterno") as ASPxComboBox).Value = tituloInterno;
                    }
                    else if (codigoTipoCampo == "CAL")
                    {
                        if (definicaoCampo != "")
                        {
                            string precisao = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                            string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                            string agregacao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                            string formulaVariavel = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                            string formulaCodigo = aDefinicaoCampo[4].Substring(aDefinicaoCampo[4].IndexOf(":") + 1).Trim();
                            formulaVariavel = getFormulaCampoCalculado_comVariavel(formulaCodigo, int.Parse(codigoModeloFormulario));

                            //Remove os Colchetes caso o banco retorne, aplica filtro no TextBox.
                            formulaVariavel = formulaVariavel.Replace("[", "").Replace("]", "");
                            (divTipoCampoSelecionado.FindControl("txt_CAL_Formula") as ASPxTextBox).NullText = "Calc. Ex: B1+B2";
                            (divTipoCampoSelecionado.FindControl("txt_CAL_Formula") as ASPxTextBox).ClientSideEvents.KeyPress = @"function(s,e){var keyCode = e.htmlEvent.keyCode; if(keyCode === 91 || keyCode === 93){ ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);}}";
                            (divTipoCampoSelecionado.FindControl("ddl_CAL_Precisao") as ASPxComboBox).Value = precisao;
                            (divTipoCampoSelecionado.FindControl("ddl_CAL_Formato") as ASPxComboBox).Value = formato;
                            (divTipoCampoSelecionado.FindControl("ddl_CAL_Agregacao") as ASPxComboBox).Value = agregacao;
                            (divTipoCampoSelecionado.FindControl("ddl_CAL_Agregacao") as ASPxComboBox).Value = agregacao;
                            (divTipoCampoSelecionado.FindControl("txt_CAL_Formula") as ASPxTextBox).Value = formulaVariavel;
                        }
                        // popula a grid de campos com aqueles que podem ser utilizados na fórmula
                        ASPxGridView grid = divTipoCampoSelecionado.FindControl("gvd_CAL_CamposCalculaveis") as ASPxGridView;
                        if (grid != null)
                            populaGridCamposCalculaveis(grid, int.Parse(objCodigo.ToString()));
                    }
                    else if (codigoTipoCampo == "VAO")
                    {
                        string[] codigo = aDefinicaoCampo[0].Split(':');
                        int codigoSelecionado = -1;
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_VAO") as ASPxComboBox;
                        if (combo != null)
                        {
                            populaComboVAO(combo, " WHERE NomeCampo like '%%' ");
                        }

                        if (!string.IsNullOrEmpty(codigo[0]))
                        {
                            bool retorno = int.TryParse(codigo[1], out codigoSelecionado);
                            combo.Value = codigoSelecionado;
                        }
                    }
                }
            }
        }
    }

    protected void gvCampos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView);
        if (grid.IsNewRowEditing || grid.IsEditing)
        {
            if (e.Column.FieldName == "CodigoTipoCampo")
            {
                string CodigoTipoFormulario = "";
                string cmdSelect = string.Format(@"SELECT CodigoTipoFormulario FROM ModeloFormulario WHERE CodigoModeloFormulario = {0} ", objCodigo.ToString());

                DataSet ds = cDados.getDataSet(cmdSelect);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    CodigoTipoFormulario = ds.Tables[0].Rows[0]["CodigoTipoFormulario"].ToString();
                }

                string where = (CodigoTipoFormulario == "4") ? string.Format(@" WHERE CodigoTipoCampo NOT IN ('VAO', 'CAL', 'LNP') ") : string.Empty;
                ASPxComboBox combo1 = e.Editor as ASPxComboBox;

                DataSet ds1 = cDados.getDataSet(string.Format(@"
                SELECT CodigoTipoCampo, DescricaoTipoCampo, DescricaoCampoUsuario
                  FROM TipoCampo {0} ORDER BY 3 ASC", where));

                combo1.TextField = "DescricaoCampoUsuario";
                combo1.ValueField = "CodigoTipoCampo";
                combo1.DataSource = ds1.Tables[0];
                combo1.DataBind();
            }
            return;
        }

        string fieldName = e.Column.FieldName;
        bool IndicaControladoSistema = grid.GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString() == "S";
        if (IndicaControladoSistema && (fieldName == "CampoObrigatorio" || fieldName == "Aba" || fieldName == "IndicaCampoVisivelGrid"))
        {
            e.Editor.ReadOnly = true;
            //e.Editor.Enabled = false;
            e.Editor.BackColor = Color.LightYellow;
        }
    }

    protected void gvCampos_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        int indexRow = grid.EditingRowVisibleIndex;
        bool IndicaControladoSistema;
        if (e.IsNewRow)
            IndicaControladoSistema = false;
        else
            IndicaControladoSistema = grid.GetRowValues(indexRow, "IndicaControladoSistema").ToString() == "S";

        if (e.NewValues["NomeCampo"] == null || e.NewValues["NomeCampo"].ToString() == "")
        {
            AddError(e.Errors, grid.Columns["NomeCampo"], Resources.traducao.adm_CadastroFormularios_o_nome_do_campo___de_preenchimento_obrigat_rio_);
        }
        if (!IndicaControladoSistema)
        {
            if (e.NewValues["CampoObrigatorio"] == null || e.NewValues["CampoObrigatorio"].ToString() == "")
            {
                AddError(e.Errors, grid.Columns["CampoObrigatorio"], Resources.traducao.adm_CadastroFormularios_o_preenchimento_do_campo___de_preenchimento_obrigat_rio_);
            }
            if (e.NewValues["CodigoTipoCampo"] == null || e.NewValues["CodigoTipoCampo"].ToString() == "")
            {
                AddError(e.Errors, grid.Columns["CodigoTipoCampo"], Resources.traducao.adm_CadastroFormularios_o_tipo_do_campo___de_preenchimento_obrigat_rio_);
            }
        }

        if (e.Errors.Count > 0)
            e.RowError = Resources.traducao.adm_CadastroFormularios_preencha_todos_os_campos_obrigat_rios_;
    }

    protected void gvCampos_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        // o botão delete não pode aparecer para campos controlados pelo sistema
        if (e.ButtonType != DevExpress.Web.ColumnCommandButtonType.Delete)
            return;
        //botão de excluir
        string IndicaControladoSistema = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString();
        e.Enabled = (IndicaControladoSistema != "S");
        e.Image.Url = (IndicaControladoSistema != "S") ? "~/imagens/botoes/excluirReg02.png" : "~/imagens/botoes/excluirRegDes.png";
    }

    protected void btnSalvar_VAR_Click(object sender, EventArgs e)
    {
        try
        {
            string comandoSQL = "";

            // obtem o objeto "Grid" que está sendo editado.
            GridViewDetailRowTemplateContainer container = (sender as ASPxButton).NamingContainer.NamingContainer as GridViewDetailRowTemplateContainer;
            gvCampos_ = container.Grid;

            ASPxHiddenField hfControle = gvCampos_.FindDetailRowTemplateControl(gvCampos_.FocusedRowIndex, "hfControle") as ASPxHiddenField;

            string codigoCampo = hfControle.Get("codigoCampo").ToString();
            string codigoTipoCampo = hfControle.Get("CodigoTipoCampo").ToString();
            string codigoFormulario = hfControle.Get("cogigoFormulario").ToString();
            if (codigoTipoCampo == "VAR")
            {
                string tamanho = hfControle.Get("VAR_tamanho").ToString();
                string mascara = hfControle.Get("VAR_mascara").ToString();
                string padrao = hfControle.Get("VAR_padrao").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Tam: {2}{1}Msk: {3}{1}Pdr:{4}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, tamanho, mascara, padrao);
                //dvVAR.Visible = true;
            }
            else if (codigoTipoCampo == "TXT")
            {
                string tamanho = hfControle.Get("TXT_tamanho").ToString();
                string linhas = hfControle.Get("TXT_linhas").ToString();
                string formatacao = hfControle.Get("TXT_formatacao").ToString();
                string padrao = hfControle.Get("TXT_padrao").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Tam: {2}{1}Lin: {3}{1}For:{4}{1}Pdr:{5}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, tamanho, linhas, formatacao, padrao);
                //dvTXT.Visible = true;
            }

            else if (codigoTipoCampo == "NUM")
            {
                string NUM_Minimo = hfControle.Get("NUM_Minimo").ToString();
                string NUM_Maximo = hfControle.Get("NUM_Maximo").ToString();
                string NUM_Precisao = hfControle.Get("NUM_Precisao").ToString();
                string NUM_Formato = hfControle.Get("NUM_Formato").ToString();
                string NUM_Agregacao = hfControle.Get("NUM_Agregacao").ToString();
                string NUM_padrao = hfControle.Get("NUM_padrao").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Min: {2}{1}Max: {3}{1}Pre: {4}{1}For: {5}{1}Agr: {6}{1}Pdr:{7}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, NUM_Minimo, NUM_Maximo, NUM_Precisao, NUM_Formato, NUM_Agregacao, NUM_padrao);
                //dvNUM.Visible = true;
            }
            else if (codigoTipoCampo == "LST")
            {
                string LST_Opcoes = hfControle.Get("LST_Opcoes").ToString();
                string LST_Formatacao = hfControle.Get("LST_Formatacao").ToString();
                string LST_Tamanho = hfControle.Get("LST_Tamanho").ToString();
                string LST_padrao = hfControle.Get("LST_padrao").ToString();
                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Opc: {2}{1}For: {3}{1}Tam: {4}{1}Pdr:{5}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, LST_Opcoes, LST_Formatacao, LST_Tamanho, LST_padrao);
                //dvLST.Visible = true;
            }
            else if (codigoTipoCampo == "DAT")
            {
                string DAT_IncluirHora = hfControle.Get("DAT_IncluirHora").ToString();
                string DAT_ValorInicial = hfControle.Get("DAT_ValorInicial").ToString();
                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Hor: {2}{1}Ini: {3}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, DAT_IncluirHora, DAT_ValorInicial);
                //dvDAT.Visible = true;
            }
            else if (codigoTipoCampo == "BOL")
            {
                string BOL_TextoVerdadeiro = hfControle.Get("BOL_TextoVerdadeiro").ToString();
                string BOL_ValorVerdadeiro = hfControle.Get("BOL_ValorVerdadeiro").ToString();
                string BOL_TextoFalso = hfControle.Get("BOL_TextoFalso").ToString();
                string BOL_ValorFalso = hfControle.Get("BOL_ValorFalso").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'TVe: {2}{1}VVe: {3}{1}TFa: {4}{1}VFa: {5}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, BOL_TextoVerdadeiro, BOL_ValorVerdadeiro, BOL_TextoFalso, BOL_ValorFalso);
                //dvBOL.Visible = true;
            }
            else if (codigoTipoCampo == "SUB")
            {
                string CodigoFormulario = hfControle.Get("SUB_CodigoFormulario").ToString();
                comandoSQL = string.Format(
                    @"BEGIN
                        UPDATE CampoModeloFormulario
                           SET DefinicaoCampo = 'Sub: {2}{1}'
                         WHERE codigoCampo = {0} 
                          
                      -- subFormularios não podem aparecer no menu de Formularios do projeto
                      DELETE FROM ModeloFormularioTipoProjeto 
                       WHERE CodigoModeloFormulario = {2}
                  END", codigoCampo, DelimitadorPropriedadeCampo, CodigoFormulario);
                //dvSUB.Visible = true;
            }
            else if (codigoTipoCampo == "CPD")
            {
                string CodigoCampoPre = hfControle.Get("CPD_CampoPre").ToString();
                string linhas = hfControle.Get("CPD_Linhas").ToString();
                string tamanho = hfControle.Get("CPD_Tamanho").ToString();
                int linha = 1;
                if (int.TryParse(linhas, out linha))
                {
                    if (linha > 10)
                        linha = 10;
                }

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'CPD: {2}{1}Lin: {3}{1}Tam: {4}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoCampoPre, linha, tamanho);
                //dvCPD.Visible = true;
            }
            else if (codigoTipoCampo == "LOO")
            {
                string CodigoListaPre = hfControle.Get("LOO_ListaPre").ToString();
                string tamanho = hfControle.Get("LOO_Tamanho").ToString();
                string LOO_ApresentacaoLOV = hfControle.Get("LOO_ApresentacaoLOV").ToString();
                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'LOO: {2}{1}Tam: {3}{1}LOV: {4}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoListaPre, tamanho, LOO_ApresentacaoLOV);
                //dvLOO.Visible = true;
            }
            else if (codigoTipoCampo == "REF")
            {
                string CodigoModeloFormulario = hfControle.Get("REF_ModeloFormulario").ToString();
                string CodigoCampoFormulario = hfControle.Get("REF_CampoFormulario").ToString();
                string SomenteLeitura = hfControle.Get("REF_SomenteLeitura").ToString();
                string TituloExterno = hfControle.Get("REF_TituloExterno").ToString();
                string TituloInterno = hfControle.Get("REF_TituloInterno").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'CMF: {2}{1}CC: {3}{1}RO: {4}{1}TIE: {5}{1}TII: {6}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoModeloFormulario, CodigoCampoFormulario, SomenteLeitura, TituloExterno, TituloInterno);
                //dvREF.Visible = true;
            }
            else if (codigoTipoCampo == "CAL")
            {
                string CAL_Precisao = hfControle.Get("CAL_Precisao").ToString();
                string CAL_Formato = hfControle.Get("CAL_Formato").ToString();
                string CAL_Agregacao = hfControle.Get("CAL_Agregacao").ToString();
                string CAL_Formula = hfControle.Get("CAL_Formula").ToString();

                //26477 - Gravação incorreta de modelos de campos calculados
                string trimFormula = Regex.Replace(CAL_Formula, "\\s+", "");

                string CAL_novaFormula = trimFormula.Replace("+", "_+").Replace("-", "_-").Replace("*", "_*").Replace("/", "_/").Replace("%", "_%").Replace("^", "_^").Replace(")", "_)") + "_";

                /* Ao salvar um campo calculado, temos que substituir a variável (b1, b2, b3...) pelo código do campo*/
                string formulaComCodigo = getFormulaCampoCalculado_comCodigo(CAL_novaFormula, int.Parse(codigoFormulario));

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Pre: {2}{1}For: {3}{1}Agr: {4}{1}CAL: {5}{1}CL2:{6}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CAL_Precisao, CAL_Formato, CAL_Agregacao, trimFormula, formulaComCodigo);
                //dvCAL.Visible = true;
            }
            else if (codigoTipoCampo == "VAO")
            {
                string VAO_Codigo = hfControle.Get("VAO_Codigo").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'VAO: {1}{2}'
                   WHERE codigoCampo = {0} ", codigoCampo, VAO_Codigo, DelimitadorPropriedadeCampo);
            }

            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);

            if (gvCampos_ != null)
            {
                populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
                gvCampos_.DetailRows.CollapseAllRows();
                gvCampos_.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region apenas para campos calculados

    private string getFormulaCampoCalculado_comCodigo(string FormulaCampoCalculado_comVariavel, int codigoModeloFormulario)
    {
        FormulaCampoCalculado_comVariavel = FormulaCampoCalculado_comVariavel.ToUpper();

        DataTable dt = getCamposNumericosFormulario(codigoModeloFormulario);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string codigo = "[" + dt.Rows[i]["CodigoCampo"].ToString() + "]";
            FormulaCampoCalculado_comVariavel = FormulaCampoCalculado_comVariavel.Replace("B" + (i + 1) + "_", codigo);
        }
        return FormulaCampoCalculado_comVariavel.Replace("_", "");
    }

    private string getFormulaCampoCalculado_comVariavel(string FormulaCampoCalculado_comCodigo, int codigoModeloFormulario)
    {
        DataTable dt = getCamposNumericosFormulario(codigoModeloFormulario);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string codigo = "[" + dt.Rows[i]["CodigoCampo"].ToString() + "]";
            FormulaCampoCalculado_comCodigo = FormulaCampoCalculado_comCodigo.Replace(codigo, "B" + (i + 1));
        }
        return FormulaCampoCalculado_comCodigo;
    }

    #endregion

    protected void gvFormularios_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
    }

    protected void gvFormularios_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["NomeFormulario"] == null || e.NewValues["NomeFormulario"].ToString() == "")
        {
            AddError(e.Errors, gvFormularios.Columns["NomeFormulario"], Resources.traducao.adm_CadastroFormularios_o_nome_do_formul_rio___de_preenchimento_obrigat_rio_);
        }
        if (e.NewValues["CodigoTipoFormulario"] == null || e.NewValues["CodigoTipoFormulario"].ToString() == "")
        {
            AddError(e.Errors, gvFormularios.Columns["CodigoTipoFormulario"], Resources.traducao.adm_CadastroFormularios_o_tipo_do_formul_rio___de_preenchimento_obrigat_rio_);
        }

        if (e.Errors.Count > 0)
            e.RowError = Resources.traducao.adm_CadastroFormularios_preencha_todos_os_campos_obrigat_rios_;
        else if (Convert.ToInt32(e.NewValues["CodigoTipoFormulario"]) == 3)
        {
            string msgErro = Resources.traducao.adm_CadastroFormularios_n_o___poss_vel_adicionar_um_novo_formul_rio_do_tipo__formul_rio_pr__definido__;
            AddError(e.Errors, gvFormularios.Columns["CodigoTipoFormulario"], msgErro);
            e.RowError = msgErro;
        }
        else if (e.IsNewRow)
        {
            string nomeFormulario = e.NewValues["NomeFormulario"] == null ? "" : e.NewValues["NomeFormulario"].ToString().Replace("'","''");
            string descricaoFormulario = e.NewValues["DescricaoFormulario"] == null ? "" : e.NewValues["DescricaoFormulario"].ToString().Replace("'", "''");
            string comandoSql = string.Format(@"
DECLARE @CodEntidade INT,
		@descricaoFormulario VARCHAR(250),
		@nomeFormulario VARCHAR(250)	

    SET @CodEntidade = {2}
    SET @descricaoFormulario = '{3}'
    SET @nomeFormulario = '{4}'

 SELECT 1 
   FROM {0}.{1}.ModeloFormulario m
  WHERE m.CodigoEntidade = @CodEntidade
	AND m.DataExclusao IS NULL
	AND m.NomeFormulario = @nomeFormulario

 SELECT 1 
   FROM {0}.{1}.ModeloFormulario m
  WHERE m.CodigoEntidade = @CodEntidade
	AND m.DataExclusao IS NULL
	AND m.DescricaoFormulario = @descricaoFormulario"
                , dbName, dbOwner, codigoEntidadeLogada, descricaoFormulario, nomeFormulario);
            DataSet ds = cDados.getDataSet(comandoSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string msgErro = Resources.traducao.adm_CadastroFormularios_erro_ao_incluir_modelo_de_formul_rio__j__existe_um_formul_rio_com_o_nome_informado_;
                AddError(e.Errors, gvFormularios.Columns["NomeFormulario"], msgErro);
                e.RowError = msgErro;
            }
            else if (ds.Tables[1].Rows.Count > 0)
            {
                string msgErro = Resources.traducao.adm_CadastroFormularios_erro_ao_incluir_modelo_de_formul_rio__j__existe_um_formul_rio_com_a_descri__o_informada_;
                AddError(e.Errors, gvFormularios.Columns["DescricaoFormulario"], msgErro);
                e.RowError = msgErro;
            }
        }
    }

    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;
        errors[column] = errorText;
    }

    protected void btnNovoFormulario_Click(object sender, EventArgs e)
    {
        gvFormularios.AddNewRow();
    }

    protected void btnNovoCampo_Click(object sender, EventArgs e)
    {
        // obtem o objeto "Grid" que está sendo editado.
        GridViewDetailRowTemplateContainer container = (sender as ASPxButton).NamingContainer as GridViewDetailRowTemplateContainer;
        foreach (Control grid in container.Controls)
        {
            if ((grid as ASPxGridView) != null)
            {
                (grid as ASPxGridView).AddNewRow();
            }
        }
    }

    protected void btnSalvarFormulario_Click(object sender, EventArgs e)
    {
        ASPxPanel pnAplicacaoFormulario = gvFormularios.FindEditFormTemplateControl("pnAplicacaoFormulario") as ASPxPanel;
    }

    private string publicarFormulario(int CodigoModeloFormulario)
    {
        Hashtable parametros = new Hashtable();
        Formulario myForm = new Formulario(cDados.classeDados, codigoUsuarioLogado, 1, CodigoModeloFormulario, new Unit(100), new Unit(100), false, this.Page, parametros, ref hfStatusCopiaFormulario, false);

        string mensagemErro = parametros["modeloCodigoTipoFormulario"] + "" != "3" ? myForm.validarModeloFormulario() : ""; //Só faz verificação se não for Formulario ASPx

        if (mensagemErro == "") // publicar o modelo
        {
            try
            {
                myForm.publicarModeloFormulario();
            }
            catch (Exception ex)
            {
                mensagemErro = "Erro ao publicar o modelo: " + ex.Message;
            }
        }
        return mensagemErro;
    }

    protected void gvFormularios_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView);
        if (grid.IsNewRowEditing)
            return;
        string fieldName = e.Column.FieldName;
        bool indicaFormularioPreDefinido = Convert.ToInt32(grid.GetRowValues(e.VisibleIndex, "CodigoTipoFormulario")).Equals(3);
        bool IndicaControladoSistema = grid.GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString() == "S";
        bool indicaCamposVerificacao = (fieldName == "Abas" || fieldName == "IndicaToDoListAssociado" || fieldName == "CodigoTipoFormulario" || fieldName == "IndicaAnexoAssociado" || fieldName == "DescricaoFormulario" || fieldName == "NomeFormulario");
        if (!indicaCamposVerificacao)
            return;
        if (IndicaControladoSistema || indicaFormularioPreDefinido)
        {
            e.Editor.ReadOnly = true;
            //e.Editor.Enabled = false;
            e.Editor.BackColor = Color.LightYellow;

        }
    }

    protected void gvd_CAL_CamposCalculaveis_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex >= 0)
            e.Row.Cells[0].Text = (e.VisibleIndex + 1).ToString();
    }

    #endregion

    #region --- [Associação a Tipos de Projetos]

    #region ---[Preenchimento Grid TipoDeProjetos e ListBoxes]

    protected void gvTiposProjetos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.Length >= 6)
        {
            string comando = e.Parameters.Substring(0, 6).ToUpper();

            if (comando == "POPFRM")
            {
                string CodModeloFormulario;
                int CodigoModeloFormulario;
                CodModeloFormulario = e.Parameters.Substring(7);
                if (int.TryParse(CodModeloFormulario, out CodigoModeloFormulario))
                {
                    Session.Remove("dtTiposProjetos");

                    populaGridTiposProjetos(CodigoModeloFormulario);

                    gvTiposProjetos.FocusedRowIndex = -1;

                    // posiciona na primeira linha caso haja;
                    if (gvTiposProjetos.VisibleRowCount > 0)
                        gvTiposProjetos.FocusedRowIndex = 0;
                }
            } /// if (comando == "POPFLX")
            else if (comando.Equals("EXCROW"))
            {
                for (int count = gvTiposProjetos.VisibleRowCount; count > 0; count--)
                    gvTiposProjetos.DeleteRow(count - 1);

                // remove jsproperties para evitar processamento no javascript desnecessário 
                // em virtude da exclusão das linhas
                gvTiposProjetos.JSProperties.Remove("cpCodigoTipoProjetoDeletado");
            }
        }
    }

    private void populaGridTiposProjetos(int codigoModeloForm)
    {
        DataTable dt = Session["dtTiposProjetos"] == null ? obtemDataTableGridTiposProjetos(codigoModeloForm) : (DataTable)Session["dtTiposProjetos"];
        gvTiposProjetos.DataSource = dt;
        gvTiposProjetos.DataBind();
    }

    protected void lbDisponiveisStatus_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string CodModeloFormulario;
            string codTipoProjeto;
            int CodigoModeloFormulario;
            int codigoTipoProjeto;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do modelo de Formulario seguido do código da tipo de projeto delimitados por [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    CodModeloFormulario = e.Parameter.Substring(7, posDelimit - 7);
                    codTipoProjeto = e.Parameter.Substring(posDelimit + 1);

                    if (int.TryParse(CodModeloFormulario, out CodigoModeloFormulario) &&
                        int.TryParse(codTipoProjeto, out codigoTipoProjeto))
                    {
                        populaListaBox_StatusDisponivel(CodigoModeloFormulario, codigoTipoProjeto);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_StatusDisponivel(int CodigoModeloFormulario, int codigoTipoProjeto)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
            BEGIN
                SELECT st.[CodigoStatus], st.[DescricaoStatus] 
                FROM {0}.{1}.[Status] AS [st]
                WHERE st.[TipoStatus] = 'PRJ' AND 
                    NOT EXISTS( SELECT 1 FROM {0}.{1}.[ModeloFormularioStatusTipoProjeto] AS [mfstp]
                        WHERE   mfstp.[CodigoStatus] = st.[CodigoStatus] 
                            AND mfstp.[CodigoModeloFormulario] = {2} 
                            AND mfstp.[CodigoTipoProjeto]  = {3}
                            AND mfstp.[StatusRelacionamento] = 'A' )
            END", dbName, dbOwner, CodigoModeloFormulario, codigoTipoProjeto);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbDisponiveisStatus.DataSource = dt;
            lbDisponiveisStatus.TextField = "DescricaoStatus";
            lbDisponiveisStatus.ValueField = "CodigoStatus";
            lbDisponiveisStatus.DataBind();
        }
    }

    protected void lbSelecionadosStatus_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string CodModeloFormulario;
            string codTipoProjeto;
            int CodigoModeloFormulario;
            int codigoTipoProjeto;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do modelo de Formulario seguido do código da tipo de projeto delimitados por [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    CodModeloFormulario = e.Parameter.Substring(7, posDelimit - 7);
                    codTipoProjeto = e.Parameter.Substring(posDelimit + 1);

                    if (int.TryParse(CodModeloFormulario, out CodigoModeloFormulario) &&
                        int.TryParse(codTipoProjeto, out codigoTipoProjeto))
                    {
                        populaListaBox_StatusSelecionados(CodigoModeloFormulario, codigoTipoProjeto);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_StatusSelecionados(int CodigoModeloFormulario, int codigoTipoProjeto)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
 BEGIN
                SELECT st.[CodigoStatus], st.[DescricaoStatus] 
                FROM {0}.{1}.[Status] AS [st] 
                INNER JOIN {0}.{1}.[ModeloFormularioStatusTipoProjeto] AS [mfstp]
                ON ( mfstp.[CodigoStatus] = st.[CodigoStatus] )
                WHERE 
                mfstp.[CodigoModeloFormulario] = {2} 
                AND mfstp.[CodigoTipoProjeto]  = {3}
                and mfstp.[StatusRelacionamento] = 'A'
                END", dbName, dbOwner, CodigoModeloFormulario, codigoTipoProjeto);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbSelecionadosStatus.DataSource = dt;
            lbSelecionadosStatus.TextField = "DescricaoStatus";
            lbSelecionadosStatus.ValueField = "CodigoStatus";
            lbSelecionadosStatus.DataBind();
        }
    }

    /// <summary>
    /// Devolve uma datatable com os tipos de projetos relacionados ao modelo de Formulario em questão
    /// </summary>
    /// <param name="CodigoModeloFormulario"></param>
    /// <returns></returns>
    private DataTable obtemDataTableGridTiposProjetos(int CodigoModeloFormulario)
    {
        DataTable dt = null;
        string sCommand = string.Format(@"
SELECT
          ftp.[CodigoTipoProjeto]
        , tp.[TipoProjeto]
		, ftp.[TextoOpcaoFormulario]
        , ftp.[TipoOcorrenciaFormulario]    AS [TipoOcorrencia]
        , ftp.[SomenteLeitura]              AS [SomenteLeitura]
        , CAST( 'N' AS Char(1) )            AS [RegistroNovo]
	FROM
		{0}.{1}.[ModeloFormularioTipoProjeto]	AS [ftp]
			INNER JOIN {0}.{1}.[TipoProjeto]	AS [tp]
				ON (tp.[CodigoTipoProjeto] = ftp.[CodigoTipoProjeto] )
	WHERE
		ftp.[CodigoModeloFormulario]			= {2} ", dbName, dbOwner, CodigoModeloFormulario);
        DataSet ds = cDados.getDataSet(sCommand);
        if (cDados.DataSetOk(ds))
        {
            dt = ds.Tables[0];

            if (gvTiposProjetos.IsEditing)
                Session["dtTiposProjetos"] = dt;
        }

        return dt;
    }

    /// <summary>
    /// Devolve a datatable que está sendo usada na grid de tipos de projetos
    /// </summary>
    /// <returns></returns>
    private DataTable obtemDataTableGridTiposProjetos()
    {
        string codigoFormulario = getChavePrimaria();
        DataTable dt = null;
        if (null != Session["dtTiposProjetos"])
            dt = (DataTable)Session["dtTiposProjetos"];
        else if (codigoFormulario != "")
            dt = obtemDataTableGridTiposProjetos(int.Parse(codigoFormulario));


        return dt;
    }

    #endregion

    #region ---[Tratamento para Interações com a Tela]

    /// <summary>
    /// Insere os itens no ComboBox em que o usuário irá escolher o tipo de projeto
    /// </summary>
    /// <remarks>
    ///  Esta função é acionada quando o usuário está para incluir ou editar uma linha da grid tipo de projeto
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvTiposProjetos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

        if (!gvTiposProjetos.IsEditing)
            return;

        if (e.Column.FieldName == "CodigoTipoProjeto")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            string where = "";

            DataTable dt = obtemDataTableGridTiposProjetos();

            if (null != dt)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if ((null == e.Value) ||
                        (dr["CodigoTipoProjeto"].ToString() != e.Value.ToString()))
                        where += dr["CodigoTipoProjeto"] + ",";
                }
            } /// if (null != dt)
              /// 
            if (where != "")
            {
                where = " AND CodigoTipoProjeto NOT IN (" + where.Substring(0, where.Length - 1) + ")";
            }
            where += " AND CodigoTipoProjeto <> 6";
            DataSet ds = cDados.getListaTiposProjetos(where);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                combo.DataSource = ds;
                combo.TextField = "TipoProjeto";
                combo.ValueField = "CodigoTipoProjeto";
                combo.DataBind();
            } /// if (cDados.DataSetOk(ds) && ...
        } // if (e.Column.FieldName == "CodigoTipoProjeto")
    }

    protected void gvTiposProjetos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = obtemDataTableGridTiposProjetos();

        if (null != dt)
        {
            DataRow dr = dt.NewRow();

            if (e.NewValues["CodigoTipoProjeto"] != null)
            {
                dr["CodigoTipoProjeto"] = e.NewValues["CodigoTipoProjeto"];

                DataSet ds = cDados.getListaTiposProjetos("AND CodigoTipoProjeto = " + e.NewValues["CodigoTipoProjeto"]);

                if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
                    dr["TipoProjeto"] = ds.Tables[0].Rows[0]["TipoProjeto"] + "";
            } /// if (e.NewValues["CodigoTipoProjeto"] != null)

            dr["TextoOpcaoFormulario"] = e.NewValues["TextoOpcaoFormulario"];
            dr["RegistroNovo"] = "S";
            dr["TipoOcorrencia"] = e.NewValues["TipoOcorrencia"];
            dr["SomenteLeitura"] = e.NewValues["SomenteLeitura"];
            dt.Rows.Add(dr);

            Session["dtTiposProjetos"] = dt;

            gvTiposProjetos.DataSource = dt;
            gvTiposProjetos.DataBind();
            gvTiposProjetos.FocusedRowIndex = gvTiposProjetos.FindVisibleIndexByKeyValue(e.NewValues["CodigoTipoProjeto"]);
        }  // if (null != dt)

        e.Cancel = true;
        gvTiposProjetos.CancelEdit();
    }

    protected void gvTiposProjetos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt = obtemDataTableGridTiposProjetos();

        if (null != dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
                {
                    if (e.NewValues["CodigoTipoProjeto"] != null)
                    {
                        dr["CodigoTipoProjeto"] = e.NewValues["CodigoTipoProjeto"];

                        DataSet ds = cDados.getListaTiposProjetos("AND CodigoTipoProjeto = " + e.NewValues["CodigoTipoProjeto"]);

                        if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
                            dr["TipoProjeto"] = ds.Tables[0].Rows[0]["TipoProjeto"] + "";
                    } /// if (e.NewValues["CodigoTipoProjeto"] != null)

                    dr["TextoOpcaoFormulario"] = e.NewValues["TextoOpcaoFormulario"];
                    dr["TipoOcorrencia"] = e.NewValues["TipoOcorrencia"];
                    dr["SomenteLeitura"] = e.NewValues["SomenteLeitura"];

                    dt.AcceptChanges();
                    break;
                } // if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
            } // foreach (DataRow dr in dt)

            Session["dtTiposProjetos"] = dt;

            gvTiposProjetos.DataSource = dt;
            gvTiposProjetos.DataBind();
        }  // if (null != dt)

        e.Cancel = true;
        gvTiposProjetos.CancelEdit();
    }

    protected void gvTiposProjetos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {


        DataTable dt = obtemDataTableGridTiposProjetos();
        if ((null != dt) && (e.Keys["CodigoTipoProjeto"] != null))
        {

            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["CodigoTipoProjeto"] != null) && (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString()))
                {
                    //retiraListaStatusDoHiddenField(dr["CodigoTipoProjeto"].ToString());
                    dr.Delete();
                    dt.AcceptChanges();

                    // registra o código do tipo de projeto para tratamento on EndCallback da grid;
                    gvTiposProjetos.JSProperties["cpCodigoTipoProjetoDeletado"] = e.Keys["CodigoTipoProjeto"].ToString();

                    break;
                }
            }

            Session["dtTiposProjetos"] = dt;
            gvTiposProjetos.DataSource = dt;
            gvTiposProjetos.DataBind();
        }  // if (null != dt)

        e.Cancel = true;
        gvTiposProjetos.CancelEdit();
    }

    #endregion

    #region ---[Gravação das informações na base de dados]

    protected void hfStatus_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        // Grava a mensagem de erro. Se não houve erro, terá conteúdo ""
        hfStatus.Set("ErroSalvar", mensagemErro_Persistencia);
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfStatus.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfStatus.Set("StatusSalvar", "0"); // 1 indica que foi salvo com sucesso.
    }

    protected void hfStatusCopiaFormulario_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistroCopiaFormulario();
        }

        // Grava a mensagem de erro. Se não houve erro, terá conteúdo ""
        hfStatusCopiaFormulario.Set("ErroSalvar", mensagemErro_Persistencia);
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfStatusCopiaFormulario.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            //ddlEntidade.SelectedIndex = -1;
            txtNomeFormularioCopia.Text = "";
        }
        else // alguma coisa deu errado...
            hfStatusCopiaFormulario.Set("StatusSalvar", "0"); // 1 indica que foi salvo com sucesso.
    }

    private string persisteEdicaoRegistro()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            salvaRegistro("E", int.Parse(chave));

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistroCopiaFormulario()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            string msgErro = salvaRegistroCopiaFormulario("E", int.Parse(chave), codigoEntidadeLogada);

            populaGridFormularios();
            return msgErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void salvaRegistro(string modo, int CodigoModeloFormulario)
    {
        string sqlDados = "";
        string sqlInsertStatus;
        string comandoSQL;

        montaInsertStatus(modo, out sqlInsertStatus);

        if (modo.Equals("E"))
        {
            sqlDados = string.Format(@"

                DECLARE @CodigoModeloFormulario Int
                SET @CodigoModeloFormulario = {2}

                ", dbName, dbOwner, CodigoModeloFormulario);

        } // else if (modo.Equals("E"))

        comandoSQL = sqlDados + sqlInsertStatus;
        int registrosAfetados = 0;
        cDados.execSQL(comandoSQL, ref registrosAfetados);

        Session.Remove("dtTiposProjetos");

        populaGridFormularios();
    }

    private string salvaRegistroCopiaFormulario(string modo, int CodigoModeloFormulario, int EntidadeDestino)
    {


        //chamar procedure  p_ClonaModeloFormulario
        string comandoSQL = string.Format(
                            @"BEGIN
                            DECLARE @retorno varchar(2048)

                            EXEC [dbo].[p_adm_clonaFormulario] {0}, '{1}', {2}, {3}, @retorno output 

                            SELECT  @retorno

                          END", CodigoModeloFormulario, txtNomeFormularioCopia.Text, codigoEntidadeLogada, codigoUsuarioLogado);
        DataSet ds = cDados.getDataSet(comandoSQL);
        string retorno = ds.Tables[0].Rows[0][0].ToString();
        return retorno;
    }

    private string getChavePrimaria()
    {
        if (gvFormularios.FocusedRowIndex >= 0)
            return gvFormularios.GetRowValues(gvFormularios.FocusedRowIndex, gvFormularios.KeyFieldName).ToString();
        else
            return "";
    }

    private void montaInsertStatus(string modo, out string comandoSQL)
    {
        bool bLinhaClickada;
        string codigoTipoProjeto, textoOpcao, tipoOcorrencia, acesso, registroNovo;
        string deleteDadosAntigos = "", insertFormulariosTipos = "", insertFormulariosStatus = "";
        string notInDelete = "";

        DataTable dt = obtemDataTableGridTiposProjetos();
        List<int> listaStatus = new List<int>(); ;

        foreach (DataRow dr in dt.Rows)
        {
            codigoTipoProjeto = dr["CodigoTipoProjeto"].ToString();
            textoOpcao = dr["TextoOpcaoFormulario"].ToString();
            tipoOcorrencia = dr["TipoOcorrencia"].ToString();
            acesso = dr["SomenteLeitura"].ToString();
            registroNovo = dr["RegistroNovo"].ToString();

            bLinhaClickada = obtemListaStatusSelecionados(int.Parse(codigoTipoProjeto), ref listaStatus);

            if ((true == bLinhaClickada) || (registroNovo.Equals("S")))
            {
                // se não tiver selecionada status algum tipo de projeto, gera exceção
                if (0 == listaStatus.Count)
                    throw new Exception(Resources.traducao.adm_CadastroFormularios_aten__o__para_cada_tipo_de_projeto_relacionado_ao_modelo____preciso_selecionar_pelo_menos_um_status_);

                insertFormulariosTipos += string.Format(@"

                    INSERT INTO {0}.{1}.[ModeloFormularioTipoProjeto]
                           ([CodigoModeloFormulario]
                           ,[CodigoTipoProjeto]
                           ,[TextoOpcaoFormulario]
                           ,[StatusRelacionamento]
                           ,[DataAtivacao]
                           ,[IdentificadorUsuarioAtivacao]
                           ,[TipoOcorrenciaFormulario]
                           ,[SomenteLeitura])
                     VALUES
                           (@CodigoModeloFormulario, {2}, '{3}', 'A', GETDATE(), '{4}', '{5}', '{6}')

                    ", dbName, dbOwner, codigoTipoProjeto, textoOpcao, codigoUsuarioLogado, tipoOcorrencia, acesso);

                foreach (int status in listaStatus)
                {
                    insertFormulariosStatus += string.Format(@"
                        INSERT INTO {0}.{1}.[ModeloFormularioStatusTipoProjeto]
                               ([CodigoModeloFormulario]
                               ,[CodigoTipoProjeto]
                               ,[CodigoStatus]
                               ,[StatusRelacionamento]
                               ,[DataAtivacao]
                               ,[IdentificadorUsuarioAtivacao])
                         VALUES
                               (@CodigoModeloFormulario, {2}, {3}, 'A', GETDATE(), '{4}')

                        ", dbName, dbOwner, codigoTipoProjeto, status, codigoUsuarioLogado);
                } //  foreach (int status in listaStatus)
            } // if ((true == bLinhaClickada) || (registroNovo.Equals("S")))
            else
            {   // se a linha não foi clicada e nem é um novo registro, 
                // não deixa mexer nesta linha no banco de dados
                if (0 != notInDelete.Length)
                    notInDelete += ',';
                notInDelete += codigoTipoProjeto;
            } // else ((true == bLinhaClickada) || (registroNovo.Equals("S")))


        } // foreach (DataRow dr in dt.Rows)

        // se estiver editando um modelo já cadastrado, apaga as linhas no bd para reinseri-las
        if (modo.Equals("E"))
        {
            if (0 != notInDelete.Length)
            {
                deleteDadosAntigos = string.Format(@"
                    DELETE {0}.{1}.[ModeloFormularioStatusTipoProjeto] WHERE 
                    [CodigoModeloFormulario] = @CodigoModeloFormulario AND [CodigoTipoProjeto] NOT IN ({2})

                    DELETE {0}.{1}.[ModeloFormularioTipoProjeto] WHERE 
                    [CodigoModeloFormulario] = @CodigoModeloFormulario AND [CodigoTipoProjeto] NOT IN ({2})

                    ", dbName, dbOwner, notInDelete);
            }// if (0 != notInDelete.Length)
            else
            {
                deleteDadosAntigos = string.Format(@"
                    DELETE {0}.{1}.[ModeloFormularioStatusTipoProjeto] WHERE 
                    [CodigoModeloFormulario] = @CodigoModeloFormulario 

                    DELETE {0}.{1}.[ModeloFormularioTipoProjeto] WHERE 
                    [CodigoModeloFormulario] = @CodigoModeloFormulario 

                    ", dbName, dbOwner);
            } // else (0 != notInDelete.Length)
        } // if (modo.Equals("E"))

        comandoSQL = deleteDadosAntigos + insertFormulariosTipos + insertFormulariosStatus;
    }

    private bool obtemListaStatusSelecionados(int codigoTipoProjeto, ref List<int> listaStatus)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaStatus, temp;

        idLista = "Sel_" + codigoTipoProjeto + delimitadorValores;

        listaStatus.Clear();

        if (hfStatus.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfStatus.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaStatus = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaStatus.Length; j++)
            {
                if (strListaStatus[j].Length > 0)
                {
                    temp = strListaStatus[j].Split(delimitadorValores);
                    listaStatus.Add(int.Parse(temp[1]));
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }

    #endregion

    #endregion

    protected void gvFormularios_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }

    protected void gvFormularios_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        int codigoTipoFormulario = Convert.ToInt32(gvFormularios.GetRowValuesByKeyValue(e.KeyValue, "CodigoTipoFormulario"));
        e.ButtonState = codigoTipoFormulario == 3 ?
            GridViewDetailRowButtonState.Hidden :
            GridViewDetailRowButtonState.Visible;
    }

    protected void gvFormularios_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        bool filtrado = (bool)(Session["tipoFormularioFiltrado"] ?? false);
        object codigoTipoFormulario = gvFormularios.GetRowValues(gvFormularios.EditingRowVisibleIndex, "CodigoTipoFormulario");
        if (Convert.ToInt32(codigoTipoFormulario) == 3)
            return;

        dsTipoFormulario.FilterExpression = (!filtrado) ?
            "CodigoTipoFormulario NOT IN (3,4)" : string.Empty;
        Session["tipoFormularioFiltrado"] = !filtrado;
        gvFormularios.HtmlEditFormCreated -= gvFormularios_HtmlEditFormCreated;
    }

    protected void gvCampos_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView gvCampos = (ASPxGridView)sender;
        if (gvCampos != null && !gvCampos.IsNewRowEditing && gvCampos.FocusedRowIndex != -1 && gvCampos.GetRowValues(gvCampos.FocusedRowIndex, "IniciaisCampoControladoSistema").Equals("DESC"))
        {
            dsTipoCampo.FilterExpression = gvCampos.Loaded ?
                "CodigoTipoCampo IN ('LST', 'LOO', 'VAR', 'TXT')" : string.Empty;

            RemoveEvent(gvCampos, "htmlEditFormCreated");
        }
    }

    private void RemoveEvent(ASPxGridView grid, string eventName)
    {
        System.Reflection.FieldInfo f1 = typeof(ASPxGridView).GetField(eventName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        if (f1 != null)
        {
            object obj = f1.GetValue(grid);
            System.Reflection.PropertyInfo pi = grid.GetType().GetProperty("Events", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            System.ComponentModel.EventHandlerList list = (System.ComponentModel.EventHandlerList)pi.GetValue(grid, null);
            list.RemoveHandler(obj, list[obj]);
        }

    }

    protected void ddl_REF_CampoFormulario_Callback(object sender, CallbackEventArgsBase e)
    {
        ASPxComboBox combo2 = sender as ASPxComboBox;
        if (combo2 != null && e.Parameter != "")
        {
            populaComboCampoModeloFormulario(combo2, int.Parse(e.Parameter));
            combo2.SelectedIndex = -1;
        }
    }

    protected void populaComboEntidade()
    {
        entidadeDestino = "-1";
        string where = string.Format(" AND UsuarioUnidadeNegocio.CodigoUsuario = {0} AND CodigoEntidade != 1", codigoUsuarioLogado, codigoEntidadeLogada);
        DataSet ds = cDados.getEntidadesUsuario(codigoUsuarioLogado, where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows.Count > 1)
            {
                ddlEntidade.DataSource = ds.Tables[0];
                ddlEntidade.ValueField = "CodigoUnidadeNegocio";
                ddlEntidade.TextField = "NomeUnidadeNegocio";
                ddlEntidade.DataBind();
            }
            else
            {
                rpEntidade.Visible = false;
                entidadeDestino = codigoEntidadeLogada + "";
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadModForm");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "gvFormularios.AddNewRow();", true, true, false, "CadModForm", lblTituloTela.Text, this);
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "gvCampos.AddNewRow();", true, false, false, "CadModForm", lblTituloTela.Text, this);
    }

    protected void menu_Init2(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "gvTiposProjetos.AddNewRow();", true, false, false, "CadModForm", lblTituloTela.Text, this);
    }

    #endregion

    protected void gvFormularios_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "SORT" || e.CallbackName == "APPLYCOLUMNFILTER")
        {
            gvFormularios.DetailRows.CollapseAllRows();
        }
    }

    protected void gvCampos_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        //cDados = CdadosUtil.GetCdados(null);
        //cDados.aplicaEstiloVisual(sender as ASPxGridView);
    }
    protected void gvCampos_Init(object sender, EventArgs e)
    {
        //cDados = CdadosUtil.GetCdados(null);
        //cDados.aplicaEstiloVisual(sender as ASPxGridView);
    }

    protected void listBox_Load(object sender, EventArgs e)
    {
        ASPxComboBox gv = sender as ASPxComboBox;
        cDados = CdadosUtil.GetCdados(null);
        cDados.aplicaEstiloVisual(gv);
    }
    protected void gvCampos_Load(object sender, EventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;
        cDados = CdadosUtil.GetCdados(null);
        cDados.aplicaEstiloVisual(gv);
        gv.Settings.ShowFilterRow = false;
        gv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gv.SettingsBehavior.AllowSort = false;
        gv.SettingsBehavior.AllowDragDrop = false;


        ((GridViewDataCheckColumn)gv.Columns["IndicaControladoSistema"]).EditFormSettings.Visible = (temPermissaoDeEditarTagsForm == true) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
        ((GridViewDataTextColumn)gv.Columns["IniciaisCampoControladoSistema"]).EditFormSettings.Visible = (temPermissaoDeEditarTagsForm == true) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;



        if (gv != null)
        {
            var parent = gv.Parent;
            cDados.traduzControles(parent, new Control[] { gv });
        }
        gv.JSProperties["cp_Msg"] = "";
    }
    protected void gvFormularios_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.Exception != null)
            e.ErrorText = e.Exception.Message;
    }
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


    protected void gvFormularios_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            //string IndicaControladoSistema = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString();
            string IndicaAspx = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "URLOrigemServidor").ToString() != "" ? "S" : "N";

            bool readOnly = /*IndicaControladoSistema == "S" ||*/ IndicaAspx == "S";
            string indicaAssinado = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaAssinado").ToString();

            string IndicaControladoSistema = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaControladoSistema").ToString();
            string codigoTipoFormulario = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "CodigoTipoFormulario").ToString();
            string indicaModeloPublicado = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaModeloPublicado").ToString();

            if (e.ButtonID == "btnCopiaFormulario")
            {
                e.Enabled = (readOnly || (codigoTipoFormulario == "4")) ? false : true;//DevExpress.Utils.DefaultBoolean.False : e.Visible = DevExpress.Utils.DefaultBoolean.True;
                e.Image.Url = (readOnly || (codigoTipoFormulario == "4")) ? "~/imagens/botoes/btnDuplicarDes.png" : "~/imagens/botoes/btnDuplicar.png";
                if (e.Enabled == false)
                {
                    e.Image.ToolTip = "Não é possível fazer cópia desse modelo de formulário";
                }
            }
            else if (e.ButtonID == "btnPublicar")
            {
                if (indicaAssinado == "S")
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/PublicarRegDes.png";
                }
                else
                {
                    if (indicaModeloPublicado == "S")
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/PublicarRegDes.png";
                    }
                }

            }
            else if (e.ButtonID == "btnExcluir")
            {

                if ((IndicaControladoSistema != "S" && indicaAssinado == "N"))
                {
                    e.Enabled = (codigoTipoFormulario == "4") ? false : true;
                    if (e.Enabled == false)
                    {
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
                    }
                }
                else
                {
                    e.Enabled = (false);
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
                }
                if (e.Enabled == false)
                {
                    e.Image.ToolTip = "Não é possível excluir esse modelo de formulário.";
                }
            }
            else if (e.ButtonID == "btnTipoProjeto")
            {
                e.Enabled = !(codigoTipoFormulario == "4");
                e.Image.Url = (e.Enabled == true) ? "~/imagens/botoes/permissoes.png" : "~/imagens/botoes/permissoesDes.png";
                if (e.Enabled == false)
                {
                    e.Image.ToolTip = "Esse modelo de formulário não pode ser associado a tipos de projetos";
                }
            }
        }
    }

    protected void ddl_SUB_Formulario_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        string filtro = " and nomeFormulario like '%" + e.Filter + "%' ";
        if (codigoFormularioPaiGlobal != -1)
        {
            populaComboSubFormularios((ASPxComboBox)source, codigoFormularioPaiGlobal, filtro);
        }

    }

    protected void ddl_SUB_Formulario_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        string filtro = " and codigoModeloFormulario = " + (e.Value != null ? e.Value.ToString() : "-1");
        if (codigoFormularioPaiGlobal != -1)
        {
            populaComboSubFormularios((ASPxComboBox)source, codigoFormularioPaiGlobal, filtro);
        }
    }

    protected void ddl_LOO_ListaPre_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        string filtro = "  WHERE DescricaoLookup LIKE '%" + e.Filter + "%' ";
        populaComboListaLookup((ASPxComboBox)source, filtro);
    }

    protected void ddl_LOO_ListaPre_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        string filtro = "  WHERE CodigoLookup = " + (e.Value != null ? e.Value.ToString() : "-1");
        populaComboListaLookup((ASPxComboBox)source, filtro);
    }

    protected void ddl_VAO_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        populaComboVAO((ASPxComboBox)source, string.Format(@" WHERE NomeCampo like '%{0}%'", e.Filter));
    }

    protected void ddl_VAO_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        string filtro = "";
        if(e.Value != null)
        {
            filtro = "  WHERE CodigoCampo  = "  + e.Value.ToString();
            populaComboVAO((ASPxComboBox)source, filtro);
        }
    }
}
