/*
 OBSERVAÇÕES
 
MODIFICAÇÕES:
 * 
 * 18/02/2011 : Error esporádico con javaScript. indicando error no scroll de gvEntidades. 
 *              O que se observo foi qeu: carrega a grid desde un panelcallback.
 *              O que se feiz: fue trocado para carrega do callback da propia grid. 
 *              O teste continuos de seleção desde o menu, nao dio error.
 * 
 * 18/03/2011 :: Alejandro : - control de acesso a controles da tela.
 * 30/03/2001 :: Alejandro : - control de acesso no momento de inserir/editar mapa estratégico [UN_IncMap].
 */
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
using System.Collections.Generic;
using System.Drawing;

public partial class _Estrategias_wizard_mapaEstrategico : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;
    private string dbName;
    private string dbOwner;
    //private bool multiplosMapasEntidades = false;
    private bool podeEditar = false;
    private bool podePermissao = false;
    private bool podeCompartilhar = false;

    public string defEntidade = Resources.traducao.mapaEstrategico_entidade;
    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuario;
    public bool variosMapas = false;
    public bool podeIncluir = false;
    #endregion

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

        dsMapa.ConnectionString = cDados.classeDados.getStringConexao();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuario = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        this.Title = cDados.getNomeSistema();
        if (!hfGeral.Contains("definicaoEntidade"))
        {
            hfGeral.Set("definicaoEntidade", defEntidade);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        cDados.aplicaEstiloVisual(Page);

        if (!IsCallback)
        {

            hfGeral.Set("CodigoMapa", "");
            hfGeral.Set("NomeMapa", "");
            hfGeral.Set("CodigoUsuario", codigoUsuarioResponsavel);
            hfGeral.Set("CodigoUnidade", codigoEntidadeUsuario);
                                        
            if (!IsPostBack)
            {
                cDados.excluiNiveisAbaixo(1);
                cDados.insereNivel(1, this);
                Master.geraRastroSite();         
            }
        }


        getMultiplosMapasEntidades();
        // esta linha deve ser excluída apois que a funcionalidade de exclusão for implementada.
        //(gvMapa.Columns[0] as GridViewCommandColumn).DeleteButton.Visible = false;//antes
        (gvMapa.Columns[0] as GridViewCommandColumn).ShowDeleteButton = false;
        getDefinicoesDaTela();

        if (variosMapas == true)
            podeIncluir = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuario, "UN", "UN_IncMap");

        populaGridMapaEstrategico(codigoEntidadeUsuario);

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuario, "permiteCarregarImagemMapa");

        bool permiteCarregarImagem = cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["permiteCarregarImagemMapa"].ToString() == "S";

        
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui um scripts nesta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var CUR = " + codigoUsuarioResponsavel + ";</script>"));
        //Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<link href =""../../estilos/custom.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/MapaEstrategico.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript""  language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("MapaEstrategico", "ASPxListbox"));


    }


    private void getMultiplosMapasEntidades()
    {
        DataSet ds = cDados.getValorParametroConfiguracaoSistema("multilplosMapasEntidade", codigoEntidadeUsuario.ToString(), "");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            variosMapas = ("S" == ds.Tables[0].Rows[0]["Valor"].ToString());  //multiplosMapasEntidades = ("S" == ds.Tables[0].Rows[0]["Valor"].ToString());

        //Ti posui [multiplosMapasEntidad], mais ainda nao tem nihum mapa cadastrado, então, seto a variavel para
        //permitir adicionar o primer mapa.
        //if (!multiplosMapasEntidades)
        //{
        ds = cDados.getMapasEstrategicos(codigoEntidadeUsuario, "");
        if (cDados.DataSetOk(ds) && !cDados.DataTableOk(ds.Tables[0]))
            variosMapas = true;
        //}
    }

    private void populaDdlUnidadeNegocio()
    {
        //DataSet ds = cDados.getUnidadesExecutivoEstrategia(codigoUsuarioResponsavel);
        DataSet ds = cDados.getUnidadeNegocio("AND CodigoEntidade = " + codigoEntidadeUsuario);
        ddlUnidadeNegocio.TextField = "nomeUnidade";
        ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
        ddlUnidadeNegocio.DataSource = ds.Tables[0];
        ddlUnidadeNegocio.DataBind();

        if (!IsPostBack)
            ddlUnidadeNegocio.SelectedIndex = 0;
    }

    private void getDefinicoesDaTela()
    {
        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuario);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string defUnidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            lblUnidadeNegocio.Text = defUnidade;
            gvMapa.SettingsText.Title = string.Format(@"Mapas Associados à {0}", defUnidade);
        }

        DataSet ds1 = cDados.getDefinicaoEntidade(codigoEntidadeUsuario);
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            string defEntidade1 = ds1.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            hfGeral.Set("definicaoEntidade", defEntidade1);
            string defPluralEntidade = ds1.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();

            gvEntidades.SettingsText.Title = string.Format(@" " + Resources.traducao.mapaEstrategico_outras + " {0} " + Resources.traducao.mapaEstrategico_com_acesso_ao_mapa, defPluralEntidade);
            ((GridViewDataComboBoxColumn)gvEntidades.Columns["CodigoUnidadeNegocio"]).EditFormSettings.Caption = defEntidade1 + ":";
            //((GridViewDataComboBoxColumn)gvMapa.Columns["CodigoUnidadeNegocio"]).EditFormSettings.Caption = defEntidade1 + ":";
            gvEntidades.Columns["NomeUnidadeNegocio"].Caption = defEntidade1;

            gvEntidades.SettingsText.ConfirmDelete = string.Format(@" " + Resources.traducao.mapaEstrategico_retirar_o_acesso_ao_mapa_para_esta + " " + defEntidade1);
            /*((GridViewCommandColumn)gvEntidades.Columns[0]).NewButton.Text = string.Format(@"Incluir {0} na lista", defEntidade1);
            ((GridViewCommandColumn)gvEntidades.Columns[0]).DeleteButton.Text = string.Format(@"Excluir {0} da lista", defEntidade1);*/


            gvEntidades.SettingsCommandButton.NewButton.Text = string.Format(@" " + Resources.traducao.mapaEstrategico_incluir + " " + defEntidade1 + " " + Resources.traducao.mapaEstrategico_na_lista);
            gvEntidades.SettingsCommandButton.DeleteButton.Text = string.Format(@" " + Resources.traducao.mapaEstrategico_excluir + " " + defEntidade1 + " da lista");
            
        }
    }
    #endregion

    #region CALLBACK'S

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string parametro = e.Parameter;
        if (parametro.Length > 3)
        {
            // mudou o valor no dropDrowList de Unidade
            if (parametro.Substring(0, 3).ToLower() == "ddl")
            {
                string conteudo = parametro.Substring(3);
                cDados.setInfoSistema("UnidadeSelecionadaCombo", conteudo);
            }
        }
    }

    #endregion

    #region --- [Grid do Mapa Estratégico]

    private void populaGridMapaEstrategico(int codigoUnidadeNegocio)
    {
        string where = string.Format(@"
                AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_Vsl') = 1
                ", dbName, dbOwner, codigoUsuarioResponsavel, codigoEntidadeUsuario);
        dsMapa.SelectCommand = string.Format(@"
                SELECT  Mapa.CodigoMapaEstrategico
                    ,   Mapa.TituloMapaEstrategico
                    ,   Mapa.IndicaMapaEstrategicoAtivo
                    ,   CASE WHEN Mapa.IndicaMapaEstrategicoAtivo = 'S' THEN '{5}' ELSE '{6}' END AS TextoIndicaMapaEstrategicoAtivo
                    ,   Ver.DataInicioVersaoMapaEstrategico
                    ,   Ver.VersaoMapaEstrategico
                    ,   Ver.DataTerminoVersaoMapaEstrategico
                    ,   Mapa.CodigoUnidadeNegocio
                    ,   Mapa.IndicaMapaCarregado
                    ,   {0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_Alt')      *   2   +	-- 2	alterar
                      --{0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_AdmPrs')   *   4	+	-- 4	excluir
                        {0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_AdmPrs')   *   8	+	-- 8	administrar permissões
                        {0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_Compart')  *   16      -- 16	Compartilhar
                        AS [Permissoes]
                 FROM       {0}.{1}.MapaEstrategico         AS Mapa
                INNER JOIN  {0}.{1}.VersaoMapaEstrategico   AS Ver      ON Mapa.CodigoMapaEstrategico   = Ver.CodigoMapaEstrategico 
                INNER JOIN  {0}.{1}.UnidadeNegocio          AS Un       ON Un.CodigoUnidadeNegocio      = Mapa.CodigoUnidadeNegocio
                WHERE Mapa.CodigoMapaEstrategico IN (SELECT me.[CodigoMapaEstrategico]
				 FROM [dbo].[MapaEstrategico]					AS [me]		
				 
					INNER JOIN [dbo].[UnidadeNegocio]		AS [un]		ON 
						(un.[CodigoUnidadeNegocio]	= me.[CodigoUnidadeNegocio])
						
				WHERE un.[DataExclusao]											IS NULL
					AND un.[CodigoEntidade]										= {3}
			UNION 
			SELECT me.[CodigoMapaEstrategico]
				FROM 
					[dbo].[MapaEstrategico]																AS [me]
							
						INNER JOIN [dbo].[UnidadeNegocio]										AS [un]		ON 
							(			un.[CodigoUnidadeNegocio]			= me.[CodigoUnidadeNegocio]
								AND un.[CodigoEntidade]						!= {3}					)
								
						INNER JOIN [dbo].[PermissaoMapaEstrategicoUnidade]	AS perm		ON
							(			perm.[CodigoMapaEstrategico]	= me.[CodigoMapaEstrategico]
								AND perm.[CodigoUnidadeNegocio]		= {3})
				WHERE un.[DataExclusao]											IS NULL) AND  
                (    {0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_Alt')    = 1 
                  OR {0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_AdmPrs') = 1
                  OR {0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_Compart')= 1
                )  
                {2}
             ORDER BY IndicaMapaEstrategicoAtivo DESC, Ver.DataInicioVersaoMapaEstrategico DESC
            ", cDados.getDbName(), cDados.getDbOwner(), where, codigoEntidadeUsuario, codigoUsuarioResponsavel, Resources.traducao.sim, Resources.traducao.n_o);
        gvMapa.DataBind();
        //Inclusão de novo mapa.
        //(gvMapa.Columns[0] as GridViewCommandColumn).NewButton.Visible = gvMapa.VisibleRowCount == 0;
    }

    protected void gvMapa_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string parametro = e.Parameters;
        if (parametro.Length > 3)
        {
            //Houve a mudança do combo de unidades
            if (parametro.Substring(0, 3).ToLower() == "ddl")
            {
                string conteudo = parametro.Substring(3);
                populaGridMapaEstrategico(int.Parse(conteudo));
            }
        }
    }

    protected void gvMapa_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";


        // se o objeto que identifica a Unidade não existir mais (sessao expirou)
        if (cDados.getInfoSistema("UnidadeSelecionadaCombo") == null && 1 == 2)
        {
            // ------------------------------------------------------------------
            // Redirecionar de dentro de uma chamada callback (Response.Redirect)
            // ------------------------------------------------------------------
            // Pode ser feito de duas maneiras:
            // 1. Usando o DevExpress - ASPxWebControl.RedirectOnCallback(url)
            //ASPxWebControl.RedirectOnCallback("~\\erros\\erroInatividade.aspx");

            // 2.Usando o próprio objeto Response.
            Response.RedirectLocation = "../../erros/erroInatividade.aspx";
            Response.End();
        }
        else
        {
            bool existeNomeBanco = false;
            string tituloMapa = e.NewValues["TituloMapaEstrategico"].ToString().Replace("'", "''").Trim();
            int codigoUnidadeNegocio = int.Parse(e.NewValues["CodigoUnidadeNegocio"].ToString());
            string indicaMapaAtivo = e.NewValues["IndicaMapaEstrategicoAtivo"].ToString();
            
            cDados.getExisteNomeMapaEstrategicoNoBanco(codigoEntidadeUsuario, tituloMapa, -1, codigoUsuarioResponsavel, ref existeNomeBanco);

            //bool estadoMapaNoBanco = false;
            //cDados.getEstadoDoMapaEstrategicoNoBanco(chave, ref estadoMapaNoBanco);

            if (!existeNomeBanco)
            {
                
                    string comandoSql;

                #region Comando SQL
                comandoSql = string.Format(
            @"
                BEGIN TRAN
                    DECLARE @CodigoMapaEstrategico int
                    DECLARE @CodigoObjetoEstrategico int
                    DECLARE @MapaAtivo char(1)
                    DECLARE @DataTerminoVersao Datetime
                    DECLARE @CodigoUnidadeNegocio int
                    DECLARE @IndicaMapaCarregado CHAR(1)

                    SET @MapaAtivo = '{3}';
                    SET @IndicaMapaCarregado = '{6}';
                    if (@MapaAtivo = 'N')    
                        SET @DataTerminoVersao = GetDate();

                    SET @CodigoUnidadeNegocio = {4}

                    INSERT INTO {0}.{1}.MapaEstrategico (TituloMapaEstrategico, IndicaMapaEstrategicoAtivo, CodigoUnidadeNegocio, IndicaMapaCarregado)
                            VALUES ('{2}', @MapaAtivo, @CodigoUnidadeNegocio, @IndicaMapaCarregado ) 

                    SET @CodigoMapaEstrategico = scope_identity()

                    INSERT INTO {0}.{1}.VersaoMapaEstrategico (CodigoMapaEstrategico, VersaoMapaEstrategico, DataInicioVersaoMapaEstrategico, DataTerminoVersaoMapaEstrategico)
                            VALUES (@CodigoMapaEstrategico, 1, GetDate(), @DataTerminoVersao )

                    -- insere o objeto estratégico padrão: mapa
                    -------------------------------------------
                    INSERT INTO ObjetoEstrategia 
                        ( CodigoMapaEstrategico, CodigoVersaoMapaEstrategico, CodigoTipoObjetoEstrategia, OrdemObjeto,
                          TituloObjetoEstrategia, DescricaoObjetoEstrategia,
                          AlturaObjetoEstrategia, LarguraObjetoEstrategia, TopoObjetoEstrategia, EsquerdaObjetoEstrategia,
                          CorFundoObjetoEstrategia, CorBordaObjetoEstrategia, CorFonteObjetoEstrategia, 
                          CodigoObjetoEstrategiaSuperior, DataInclusao, CodigoUsuarioInclusao
                        )
                    VALUES 
                        (  @CodigoMapaEstrategico, 1, 1, 1,
                           'Mapa Estratégico', '{2}',
                           700, 950, 5, 5, 
                           '#c0c0c0', '#000080', '#000000', 
                           null,  getdate(), {5}
                        )
   
                    --Em seguida à gravação dos dados, deve ser acionada a proc [p_ProcessaMatrizAcessos] 
                    --para que a matriz de acesso do usuário logado seja refeita para o mapa cujos dados estão sendo gravadas;
                    
                    DECLARE @RC int
                    DECLARE @in_iniciaisTipoObjeto char(2)
                        DECLARE @in_codigoObjetoPai bigint
                    DECLARE @in_codigoEntidade int
                    DECLARE @in_codigoPermissao smallint
                    DECLARE @in_codigoUsuario int
                    DECLARE @in_codigoPerfil int
                    DECLARE @in_codigoObjeto INT

                    SET @in_iniciaisTipoObjeto =  'ME'       
                    SET @in_codigoObjeto = @CodigoMapaEstrategico
                    SET @in_codigoObjetoPai = 0
                    SET @in_codigoEntidade = {7}
                    SET @in_codigoPermissao = NULL
                    SET @in_codigoUsuario = {5}
                    SET @in_codigoPerfil = NULL

                    EXECUTE @RC = [dbo].[p_ProcessaMatrizAcessos] 
                        @in_iniciaisTipoObjeto
                        ,@in_codigoObjeto
                        ,@in_codigoObjetoPai
                        ,@in_codigoEntidade
                        ,@in_codigoPermissao
                        ,@in_codigoUsuario
                        ,@in_codigoPerfil

                   --Em seguida ao acionamento da proc do item anterior, deve ser acionada outra proc, 
                   --a [p_incluiJobProcessaMatrizAcessos] para que seja incluído um job que referá a matriz de permissão do mapa para todos os usuários;

                    EXECUTE @RC = [dbo].[p_incluiJobProcessaMatrizAcessos] 
                       @in_iniciaisTipoObjeto
                      ,@in_codigoObjeto
                      ,@in_codigoObjetoPai
                      ,@in_codigoEntidade
                      ,@in_codigoPermissao
                      ,NULL
                      ,@in_codigoPerfil
                      ,NULL


                  COMMIT"
        , cDados.getDbName()
        , cDados.getDbOwner()
        , tituloMapa
        , indicaMapaAtivo
        , codigoUnidadeNegocio
        , codigoUsuarioResponsavel
        , "S", codigoEntidadeUsuario);

                        #endregion

                    int registrosAfetados = 0;
                    cDados.execSQL(comandoSql, ref registrosAfetados);
                    e.Cancel = true;
                ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.mapaEstrategico_mapa_inclu_do_com_sucesso_;
                gvMapa.CancelEdit();
               
            }
            else
            {
                ((ASPxGridView)sender).JSProperties["cpErro"] = Resources.traducao.mapaEstrategico_n_o_pode_existir_nomes_duplicados_;

            }
        }
    }

    protected void gvMapa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";

        bool existeNomeBanco = false;
        int chave = int.Parse(e.Keys[(sender as ASPxGridView).KeyFieldName].ToString());
        int codigoUnidadeEditada = int.Parse(e.NewValues["CodigoUnidadeNegocio"].ToString());
        cDados.getExisteNomeMapaEstrategicoNoBanco(codigoEntidadeUsuario, e.NewValues["TituloMapaEstrategico"].ToString().Replace("'", "''").Trim(), chave, codigoUsuarioResponsavel, ref existeNomeBanco);

        //bool estadoMapaNoBanco = false;
        //cDados.getEstadoDoMapaEstrategicoNoBanco(chave, ref estadoMapaNoBanco);

        if (!existeNomeBanco)
        {
            // busca o código da unidade e a versão do mapa a partir da própria grid.
            object[] objTemp = (object[])(sender as ASPxGridView).GetRowValuesByKeyValue(chave, "CodigoUnidadeNegocio", "VersaoMapaEstrategico");
            int CodigoUnidadeNegocio = int.Parse(objTemp[0].ToString());
            int VersaoMapaEstrategico = int.Parse(objTemp[1].ToString());
            if (CodigoUnidadeNegocio != codigoUnidadeEditada)
            {
                if (false == cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuario, codigoUnidadeEditada, "NULL", "UN", 0, "NULL", "UN_IncMap"))
                {
                    ((ASPxGridView)sender).JSProperties["cpErro"] = Resources.traducao.mapaEstrategico_altera__o_n_o_permitida__voc__n_o_tem_acesso_para_incluir_mapa_estrat_gico_na_unidade_escolhida_;
                }
            }
            string tituloMapa = e.NewValues["TituloMapaEstrategico"].ToString().Replace("'", "''").Trim();
            int codigoUnidadeNegocio = int.Parse(e.NewValues["CodigoUnidadeNegocio"].ToString());
            string indicaMapaAtivo = e.NewValues["IndicaMapaEstrategicoAtivo"].ToString();
            string indicaMapaCarregado = "S";
            bool retorno = cDados.atualizaMapaEstrategico(
                              chave
                            , tituloMapa
                            , indicaMapaAtivo[0]
                            , VersaoMapaEstrategico
                            , codigoUnidadeNegocio
                            , indicaMapaCarregado, codigoEntidadeUsuario);

            if (retorno)
            {
                // se o objeto que identifica a Unidade não existir mais (sessao expirou), vamos aproveitar o que foi lido da grid
                if (cDados.getInfoSistema("UnidadeSelecionadaCombo") == null)
                {
                    cDados.setInfoSistema("UnidadeSelecionadaCombo", CodigoUnidadeNegocio);
                }                    
                ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.mapaEstrategico_mapa_alterado_com_sucesso_;
                //populaGridMapaEstrategico(int.Parse(cDados.getInfoSistema("UnidadeSelecionadaCombo").ToString()));
                populaGridMapaEstrategico(codigoEntidadeUsuario);
                e.Cancel = true;
                gvMapa.CancelEdit();
            }
        }
        else
        {
            ((ASPxGridView)sender).JSProperties["cpErro"] = Resources.traducao.mapaEstrategico_n_o_pode_existir_nomes_duplicados_;
        }
    }

    protected void gvMapa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        /* - A opçaõ de exlusão deve ser implementada aqui 
                     antes de começar a implementação, a linha que oculta o botão de exclusão deve ser retirada do evento PAGE_LOAD
         * 
         *   Após a implementação... exclua este comentário
        */
        int chave = int.Parse(e.Keys[(sender as ASPxGridView).KeyFieldName].ToString());
        // busca o código da unidade e a versão do mapa a partir da própria grid.
        object[] objTemp = (object[])(sender as ASPxGridView).GetRowValuesByKeyValue(chave, "CodigoUnidadeNegocio", "VersaoMapaEstrategico");
        int CodigoUnidadeNegocio = int.Parse(objTemp[0].ToString());
        int VersaoMapaEstrategico = int.Parse(objTemp[1].ToString());
    }

    protected void gvMapa_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        bool novoReg = gvMapa.IsNewRowEditing;
        string where = "";
        if (e.Column.Name == "CodigoUnidadeNegocio")
        {
            if (novoReg)
                where = string.Format(@" AND DataExclusao IS NULL AND IndicaUnidadeNegocioAtiva = 'S' 
                        AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, un.CodigoUnidadeNegocio, NULL, 'UN', 0, NULL, 'UN_IncMap') = 1
                        AND CodigoEntidade = {4}
                        ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, codigoEntidadeUsuario, codigoEntidadeUsuario);
            else
                where = string.Format(@" AND DataExclusao IS NULL AND IndicaUnidadeNegocioAtiva = 'S' 
                        AND CodigoEntidade = {4}
                        ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, codigoEntidadeUsuario, codigoEntidadeUsuario);


            ASPxComboBox combo = e.Editor as ASPxComboBox;
            DataSet ds = cDados.getUnidadeNegocio(where); //cDados.getObjetivoEstrategico(codigoEntidade, null, " AND obj.DataExclusao IS NULL AND obj.CodigoMapaEstrategico = " + ddlMapa.Value);

            if (cDados.DataSetOk(ds))
            {
                combo.DataSource = ds;
                combo.TextField = "nomeUnidade";
                combo.ValueField = "CodigoUnidadeNegocio";
                combo.DataBind();
            }
        }
    }

    protected void gvMapa_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string mapaAtivo = e.GetValue("IndicaMapaEstrategicoAtivo").ToString();

            if (mapaAtivo == "N")
            {
                e.Row.ForeColor = Color.FromName("#914800");
            }
        }
    }

    protected void gvMapa_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex <= 0 || gvMapa.GetRowValues(e.VisibleIndex, "Permissoes") == null)
        {
            return;
        }

        e.Enabled = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
        int permissoes = int.Parse(gvMapa.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
        podeEditar = (permissoes & 2) > 0;
        podePermissao = (permissoes & 8) > 0;
        podeCompartilhar = (permissoes & 16) > 0;

        if (e.ButtonType == ColumnCommandButtonType.Edit && !e.Enabled)
        {
            if (!podeEditar)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.PNG";
            }
            else
            {
                e.Enabled = true;
            }
        }
    }

    protected void gvMapa_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex <= 0 || gvMapa.GetRowValues(e.VisibleIndex, "Permissoes") == null)
        {
            return;
        }

        int permissoes = int.Parse(gvMapa.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
        podeEditar = (permissoes & 2) > 0;
        podePermissao = (permissoes & 8) > 0;
        podeCompartilhar = (permissoes & 16) > 0;

        if (e.ButtonID == "btnDisenhoMapa")
        {
            if (!podeEditar)
            {
                e.Text = "Disenho";
                e.Enabled = false;
                e.Image.Url = "~/imagens/mapaEstrategico/DisenhoMapaDes.png";
            }
        }
        if (e.ButtonID == "btnCompartilhar")
        {
            if (!podeCompartilhar)
            {
                e.Text = "Compartilhar";
                e.Enabled = false;
                e.Image.Url = "~/imagens/compartilharDes.png";
            }
        }
        if (e.ButtonID == "btnPermissoes")
        {
            if (!podePermissao)
            {
                e.Text = "Permissões";
                e.Enabled = false;
                e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
            }
        }
    }


    #endregion

    #region --- [Popup Permissão de Acesso ao Mapa]

    #region --- [Grid Entidades com Acesso]

    /// <summary>
    /// Popula a grid de entidades com as entidades que já têm acesso ao mapa
    /// </summary>
    /// <param name="codigoMapa"></param>
    private void populaGridEntidades(int codigoMapa)
    {
        DataTable dt = obtemDataTableGridEntidades(codigoMapa);
        gvEntidades.DataSource = dt;
    }

    /// <summary>
    /// Inicializa o 'combobox' da grid  de entidades que terão acesso ao mapa.
    /// </summary>
    /// <remarks>
    /// Este ComboBox conterá a lista de entidadess para a pessoa escolher qual passará a compor o 
    /// roll de entidades com acesso ao mapa.
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEntidades_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (!gvEntidades.IsEditing)
            return;

        if (e.Column.FieldName == "CodigoUnidadeNegocio")
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            string where = "";

            DataTable dt = obtemDataTableGridEntidades();

            if (null != dt)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if ((null == e.Value) ||
                        (dr["CodigoUnidadeNegocio"].ToString() != e.Value.ToString()))
                        where += dr["CodigoUnidadeNegocio"] + ",";
                }
            } // if (null != dt

            // inclui a entidade do usuário no NOT IN
            where += codigoEntidadeUsuario.ToString() + ",";
            where = " AND e.[CodigoUnidadeNegocio] NOT IN (" + where.Substring(0, where.Length - 1) + ")";
            where += " AND e.CodigoUnidadeNegocio = e.CodigoEntidade ";
            where += " AND e.[IndicaUnidadeNegocioAtiva] = 'S'";

            DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuario, "modoCompartilhamentoMapa");

            if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            {
                string modo = dsParam.Tables[0].Rows[0]["modoCompartilhamentoMapa"].ToString();
                switch (modo)
                {
                    case "I":
                        where += string.Format(" AND e.CodigoUnidadeNegocioSuperior = (SELECT CodigoUnidadeNegocioSuperior FROM UnidadeNegocio WHERE CodigoUnidadeNegocio = {0})", codigoEntidadeUsuario);
                        break;
                    case "F":
                        where += string.Format(" AND dbo.f_GetUnidadeSuperior(e.CodigoUnidadeNegocio,{0}) IS NOT NULL", codigoEntidadeUsuario);
                        break;
                    case "IF":
                        where += string.Format(" AND (e.CodigoUnidadeNegocioSuperior = (SELECT CodigoUnidadeNegocioSuperior FROM UnidadeNegocio WHERE CodigoUnidadeNegocio = {0})", codigoEntidadeUsuario);
                        where += string.Format(" OR dbo.f_GetUnidadeSuperior(e.CodigoUnidadeNegocio,{0}) IS NOT NULL)", codigoEntidadeUsuario);
                        break;
                    default:
                        break;
                }
            }

            // todo: trocar função
            DataSet ds = cDados.getEntidades(where);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                combo.DataSource = ds;
                combo.TextField = "NomeUnidadeNegocio";
                combo.ValueField = "CodigoUnidadeNegocio";
                combo.DataBind();
            } /// if (cDados.DataSetOk(ds) && ...
        } // if (e.Column.FieldName == "CodigoTipoProjeto")
    }

    protected void gvEntidades_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DataTable dt = obtemDataTableGridEntidades();

        if (null != dt)
        {
            DataRow dr = dt.NewRow();

            if (e.NewValues["CodigoUnidadeNegocio"] != null)
            {
                dr["CodigoUnidadeNegocio"] = e.NewValues["CodigoUnidadeNegocio"];

                DataSet ds = cDados.getEntidades("AND e.[CodigoUnidadeNegocio] = " + e.NewValues["CodigoUnidadeNegocio"]);

                if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
                    dr["NomeUnidadeNegocio"] = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"] + "";
            } /// if (e.NewValues["CodigoTipoProjeto"] != null)

            dr["RegistroNovo"] = "S";
            dt.Rows.Add(dr);

            Session["dtEntidades"] = dt;

            gvEntidades.DataSource = dt;
            gvEntidades.DataBind();
        }  // if (null != dt)

        e.Cancel = true;
        gvEntidades.CancelEdit();
    }

    protected void gvEntidades_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        DataTable dt = obtemDataTableGridEntidades();

        if (null != dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CodigoUnidadeNegocio"].ToString() == e.Keys["CodigoUnidadeNegocio"].ToString())
                {
                    if (e.NewValues["CodigoUnidadeNegocio"] != null)
                    {
                        dr["CodigoUnidadeNegocio"] = e.NewValues["CodigoUnidadeNegocio"];

                        DataSet ds = cDados.getEntidades("AND e.[CodigoUnidadeNegocio] = " + e.NewValues["CodigoUnidadeNegocio"]);

                        if ((true == cDados.DataSetOk(ds)) && (true == cDados.DataTableOk(ds.Tables[0])))
                            dr["NomeUnidadeNegocio"] = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"] + "";
                    } /// if (e.NewValues["CodigoTipoProjeto"] != null)

                    dt.AcceptChanges();
                    break;
                } // if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
            } // foreach (DataRow dr in dt)

            Session["dtEntidades"] = dt;

            gvEntidades.DataSource = dt;
            gvEntidades.DataBind();
        }  // if (null != dt)

        e.Cancel = true;
        gvEntidades.CancelEdit();
    }

    protected void gvEntidades_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        DataTable dt = obtemDataTableGridEntidades();

        if (null != dt)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CodigoUnidadeNegocio"] + "" == e.Keys["CodigoUnidadeNegocio"] + "")
                {
                    dr.Delete();
                    dt.AcceptChanges();
                    break;
                }
            }

            Session["dtEntidades"] = dt;
            gvEntidades.DataSource = dt;
            gvEntidades.DataBind();
        }  // if (null != dt)

        e.Cancel = true;
        gvEntidades.CancelEdit();
    }

    protected void gvEntidades_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.Length >= 6)
        {
            string comando = e.Parameters.Substring(0, 6).ToUpper();
            string codMapa;
            int codigoMapa;

            if (comando == "POPMAP")
            {
                codMapa = e.Parameters.Substring(7);
                if (int.TryParse(codMapa, out codigoMapa))
                {
                    populaGridEntidades(codigoMapa);
                    gvEntidades.DataBind();
                }
            } /// if (comando == "POPFLX")
        }
    }

    #endregion

    #region --- [ Funções Comuns aos Popup Permissão Acesso ao Mapa]

    /// <summary>
    /// Devolve uma datatable com as unidades relacionados ao mapa em questão
    /// </summary>
    /// <param name="codigoMapa"></param>
    /// <returns></returns>
    private DataTable obtemDataTableGridEntidades(int codigoMapa)
    {
        DataTable dt = null;
        string sCommand = string.Format(@"
SELECT
          meu.[CodigoUnidadeNegocio]
        , un.[NomeUnidadeNegocio]
        , CAST( 'N' AS Char(1) )        AS [RegistroNovo]
	FROM
		{0}.{1}.[PermissaoMapaEstrategicoUnidade]       AS [meu]
			INNER JOIN {0}.{1}.[UnidadeNegocio]	        AS [un]
				ON (un.[CodigoUnidadeNegocio] = meu.[CodigoUnidadeNegocio] )
	WHERE
		meu.[CodigoMapaEstrategico] = {2} 
    ORDER BY un.[NomeUnidadeNegocio] ", dbName, dbOwner, codigoMapa);
        DataSet ds = cDados.getDataSet(sCommand);
        if (cDados.DataSetOk(ds))
        {
            dt = ds.Tables[0];
            Session["dtEntidades"] = dt;
        }

        return dt;
    }

    /// <summary>
    /// Devolve a datatable que está sendo usada na grid unidades
    /// </summary>
    /// <returns></returns>
    private DataTable obtemDataTableGridEntidades()
    {
        DataTable dt = null;
        if (null != Session["dtEntidades"])
            dt = (DataTable)Session["dtEntidades"];

        return dt;
    }

    #endregion

    #region --- [Gravação das Informações]

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
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

    private string getChavePrimaria()
    {
        if (gvMapa.GetSelectedFieldValues(gvMapa.KeyFieldName).Count > 0)
            return gvMapa.GetSelectedFieldValues(gvMapa.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private void salvaRegistro(string modo, int codigoMapa)
    {
        string sqlDadosMapa = "";
        string sqlInsertUsuarios;
        string comandoSQL;

        montaInsert(modo, out sqlInsertUsuarios);

        if (modo.Equals("E"))
        {
            sqlDadosMapa = string.Format(@"
                DECLARE @CodigoMapa Int
                SET @CodigoMapa = {0}

                ", codigoMapa);

        } // else if (modo.Equals("E"))

        comandoSQL = sqlDadosMapa + sqlInsertUsuarios;

        int registrosAfetados = 0;
        cDados.execSQL(comandoSQL, ref registrosAfetados);
    }

    private void montaInsert(string modo, out string comandoSQL)
    {
        string codigoEntidade, registroNovo;
        string deleteDadosAntigos = "", insertMapaEntidade = "";
        string notInDelete = "";

        DataTable dt = obtemDataTableGridEntidades();

        foreach (DataRow dr in dt.Rows)
        {
            codigoEntidade = dr["CodigoUnidadeNegocio"].ToString();
            registroNovo = dr["RegistroNovo"].ToString();

            if (registroNovo.Equals("S"))
            {
                insertMapaEntidade += string.Format(@"

                    INSERT INTO {0}.{1}.[PermissaoMapaEstrategicoUnidade]
                           ([CodigoMapaEstrategico]
                           ,[CodigoUnidadeNegocio])
                     VALUES
                           (@CodigoMapa, {2})

                    ", dbName, dbOwner, codigoEntidade);
            }
            else
            {   // se a linha não foi clicada e nem é um novo registro, 
                // não deixa mexer nesta linha no banco de dados
                if (0 != notInDelete.Length)
                    notInDelete += ',';
                notInDelete += codigoEntidade;
            }


        }

        if (modo.Equals("E"))
        {
            if (0 != notInDelete.Length)
            {
                deleteDadosAntigos = string.Format(@"
                    DELETE {0}.{1}.[PermissaoMapaEstrategicoUnidade] WHERE 
                    [CodigoMapaEstrategico] = @CodigoMapa AND [CodigoUnidadeNegocio] NOT IN ({2})

                    ", dbName, dbOwner, notInDelete);
            }
            else
            {
                deleteDadosAntigos = string.Format(@"
                    DELETE {0}.{1}.[PermissaoMapaEstrategicoUnidade] WHERE 
                    [CodigoMapaEstrategico] = @CodigoMapa 

                    ", dbName, dbOwner);
            }
        }

        comandoSQL = deleteDadosAntigos + insertMapaEntidade;
    }

    #endregion

    #endregion
    protected void gvMapa_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "DesMapa");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "gvMapa.AddNewRow();", true, true, false, "DesMapa", Request.QueryString["Tit"].ToString(), this);
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

    protected void gvMapa_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (!e.HasErrors)
        {
            var tituloMapa = (e.NewValues["TituloMapaEstrategico"] as string ?? string.Empty).Trim().Replace("'", "''");
            var codigoMapa = e.IsNewRow ? -1 : (int)e.Keys["CodigoMapaEstrategico"];
            var existeNomeBanco = false;
            cDados.getExisteNomeMapaEstrategicoNoBanco(codigoEntidadeUsuario, tituloMapa, codigoMapa, codigoUsuarioResponsavel, ref existeNomeBanco);
            if(existeNomeBanco)
                e.RowError = Resources.traducao.mapaEstrategico_j__existe_outro_mapa_com_o_mesmo_nome_;
        }
    }
}
