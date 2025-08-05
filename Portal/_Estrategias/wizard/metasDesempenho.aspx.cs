/*
 08/12//2010: Mudança by Alejandro: 
            Foi implementado a inserção de Metas.
 15/12//2010: Mudança by Alejandro: 
            Foi mudado la propiedade "Rows" (do componente [txtMeta]) con el
            valor 2.
            
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
using System.Text;
using DevExpress.Web;
using System.Drawing;


public partial class _Estrategias_wizard_metasDesempenho : System.Web.UI.Page
{
    dados cDados;

    private string resolucaoCliente = "";
    private int idUsuarioLogado;
    private int codigoEntidade;
    private int alturaPrincipal = 0, larguraPrincipal = 0;

    public bool podeEditar = false;
    static public bool dadosDaMetaDescritivaForamModificados = false;
    private bool indicaReplicaMetaUnidades = false;
    static DataSet dsGridMetas;

    protected void Page_Init(object sender, EventArgs e)
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(pnCallback);

        DataSet ds = cDados.getParametrosSistema("IndicaReplicaMetaUnidades");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string parametro = ds.Tables[0].Rows[0]["IndicaReplicaMetaUnidades"].ToString();
            indicaReplicaMetaUnidades = parametro.Trim().ToUpper() == "S";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HearderOnTela();
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        populaGrid();

        if (!IsPostBack)
        {

            hfGeral.Set("PermissaoLinha", "N");
            hfGeral.Set("CodigoIndicador", "-1");

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "METAS", "EST", -1, Resources.traducao.metasDesempenho_adicionar_aos_favoritos);
        }
        cDados.aplicaEstiloVisual(this);

    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 260;

        //pcDados.Width = larguraPrincipal - 20;
    }

    private void HearderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/MetasDesempenho.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("MetasDesempenho", "_Strings"));
    }

    private void MenuUsuarioLogado()
    {
        gvDados.Columns[0].Visible = false;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "ALTMETA"))
            gvDados.Columns[0].Visible = true;
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoIndicador").ToString();
        else
            return "-1";
    }

    #endregion

    #region GRIDVIEW

    #region gvDados

    private void populaGrid()
    {
        string select = string.Format(@", CAST(CASE WHEN EXISTS 
				(
					SELECT TOP 1 1 
						FROM {0}.{1}.[IndicadorUnidade]	AS [iu]
							INNER JOIN {0}.{1}.[UnidadeNegocio]	AS [un]	ON 
								(			un.[CodigoUnidadeNegocio] = iu.[CodigoUnidadeNegocio]
									AND un.[DataExclusao]				IS NULL 
									AND un.[CodigoEntidade]			= {3} )
						WHERE
									iu.[CodigoIndicador]		= i.[CodigoIndicador]
							AND {0}.{1}.f_VerificaAcessoConcedido({2},  {3}, iu.[CodigoIndicador], NULL, 'IN', iu.[CodigoUnidadeNegocio], NULL, 'IN_DefMta') = 1
				) THEN 1 ELSE 0 END AS Bit) AS [Permissoes] 
            ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);
        string where = string.Format(@" AND EXISTS 
				(
					SELECT TOP 1 1 
						FROM {0}.{1}.[IndicadorUnidade]	AS [iu]
							INNER JOIN {0}.{1}.[UnidadeNegocio]	AS [un]	ON 
								(			un.[CodigoUnidadeNegocio] = iu.[CodigoUnidadeNegocio]
									AND un.[DataExclusao]				IS NULL 
									AND un.[CodigoEntidade]			= {3} )
						WHERE
									iu.[CodigoIndicador]		= i.[CodigoIndicador]
							AND {0}.{1}.f_VerificaAcessoConcedido({2},  {3}, iu.[CodigoIndicador], NULL, 'IN', iu.[CodigoUnidadeNegocio], NULL, 'IN_CnsMta') = 1
				) 
            ", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);

        string podeAgruparPorMapa = "N";

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidade, "utilizaColunaMapaTelaIndicadores");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            podeAgruparPorMapa = dsParam.Tables[0].Rows[0]["utilizaColunaMapaTelaIndicadores"].ToString();

        if (podeAgruparPorMapa == "N")
        {
            gvDados.Columns["MapaEstrategico"].Visible = false;
            ((GridViewDataTextColumn)gvDados.Columns["MapaEstrategico"]).GroupIndex = -1;
        }
        if (!IsPostBack && dadosDaMetaDescritivaForamModificados == false)
        {
            dsGridMetas = cDados.getIndicadoresMetaUnidadeNegocio(idUsuarioLogado, codigoEntidade, codigoEntidade, select, podeAgruparPorMapa, where);
        }
        else if (IsPostBack && dadosDaMetaDescritivaForamModificados == true)
        {
            dsGridMetas = cDados.getIndicadoresMetaUnidadeNegocio(idUsuarioLogado, codigoEntidade, codigoEntidade, select, podeAgruparPorMapa, where);
            dadosDaMetaDescritivaForamModificados = false;
        }

        if (cDados.DataSetOk(dsGridMetas))
        {
            gvDados.DataSource = dsGridMetas.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "Permissoes") != null)
        {
            //int permissao = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
            podeEditar = (bool)gvDados.GetRowValues(e.VisibleIndex, "Permissoes"); //(permissao & 2) > 0;

            if (e.ButtonID.Equals("btnEditarCustom"))
            {
                if (podeEditar)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Text = Resources.traducao.metasDesempenho_detalhe_da_meta;
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/pFormulario.png";
                }
            }
        }
        if (e.CellType == GridViewTableCommandCellType.Data && e.ButtonID.Equals("btnReplicar"))
        {
            e.Visible = indicaReplicaMetaUnidades ?
                DevExpress.Utils.DefaultBoolean.True :
                DevExpress.Utils.DefaultBoolean.False;
        }
    }

    #endregion


    #endregion
    
    #region CALLBACK'S

    /// <summary>
    /// Painel utiliçado para atualizar o campo 'Meta' da tabela 'IndicadorOperacionalProjeto'.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string chave = getChavePrimaria();
        string meta = "";
        if (txtMeta.Text == null || txtMeta.Text.Trim().Equals(""))
            meta = "NULL";
        else
            meta = "'" + txtMeta.Text.Trim().Replace("'", "''") + "'";

        if (gvDados.FocusedRowIndex != -1)
        {
            int codigoUnidadeNegocio = int.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoUnidadeNegocio").ToString());

            cDados.atualizaMetaDescritivaIndicador(int.Parse(chave), meta, codigoUnidadeNegocio, idUsuarioLogado);
            dadosDaMetaDescritivaForamModificados = true;
        }
    }

    #endregion

    private void carregaComboAnos(int codigoIndicador)
    {
        DataSet dsAnos = cDados.getAnosAtivosIndicador(codigoIndicador, "");

        if (cDados.DataSetOk(dsAnos))
        {
            ddlAnos.DataSource = dsAnos;
            ddlAnos.TextField = Resources.traducao.metasDesempenho_ano;
            ddlAnos.ValueField = "Ano";
            ddlAnos.DataBind();
        }

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

        ddlAnos.Items.Insert(0, lei);

        if (ddlAnos.Items.Count > 0)
        {
            ListEditItem item =
                ddlAnos.Items.FindByValue(DateTime.Now.Year.ToString());
            if (item == null)
                ddlAnos.SelectedIndex = 0;
            else
                ddlAnos.Value = DateTime.Now.Year.ToString();
        }
    }

    private void carregaComboUnidades(int codigoIndicador)
    {
        DataSet dsUnidadesIndicador = cDados.getUnidadesUsuarioIndicadorPorPermissao(codigoIndicador, idUsuarioLogado, codigoEntidade, "IN_DefMta", "");

        if (cDados.DataSetOk(dsUnidadesIndicador))
        {
            ddlUnidades.DataSource = dsUnidadesIndicador;
            ddlUnidades.TextField = "NomeUnidadeNegocio";
            ddlUnidades.ValueField = "CodigoUnidadeNegocio";
            ddlUnidades.DataBind();
        }

        if (ddlUnidades.Items.Count > 0)
            ddlUnidades.SelectedIndex = 0;
    }

    protected void pcAtualizarMetas_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
    {
        int codigoIndicador = int.Parse(e.Parameter);
        carregaComboUnidades(codigoIndicador);
        carregaComboAnos(codigoIndicador);
    }

    protected void callbackReplicarMetas_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        object codigoIndicador = gvDados.GetRowValues(
            gvDados.FocusedRowIndex, gvDados.KeyFieldName);
        object codigoUnidadeNegocio = ddlUnidades.Value;
        object ano = ddlAnos.Value;

        #region Comando SQL

        string comandoSql = string.Format(@"
DECLARE @CodigoIndicador int,
        @CodigoUnidadeNegocio int,
        @Ano smallint,
        @DataInclusao datetime, 
        @CodigoUsuarioInclusao int,
        @CodigoEntidade int,
        @CodigoUnidadeNegocioIncluida int
   
SET @CodigoIndicador = {0}
SET @CodigoUnidadeNegocio = {1}
SET @Ano = {2}
SET @CodigoUsuarioInclusao = {3}
SET @CodigoEntidade = {4}
SET @DataInclusao = GETDATE()
        
DECLARE cCursor CURSOR FOR
SELECT un.CodigoUnidadeNegocio
  FROM f_GetUnidadesUsuarioIndicadorPorPermissao(@CodigoUsuarioInclusao, @CodigoEntidade, @CodigoIndicador, null, 'IN_DefMta') f INNER JOIN
       UnidadeNegocio un ON un.CodigoUnidadeNegocio = f.CodigoUnidade
 WHERE un.CodigoUnidadeNegocio <> @CodigoUnidadeNegocio
 
OPEN cCursor
 
FETCH NEXT FROM cCursor INTO @CodigoUnidadeNegocioIncluida

WHILE @@FETCH_STATUS = 0
BEGIN 
    IF EXISTS(SELECT 1 
                FROM [MetaIndicadorUnidade] 
               WHERE CodigoIndicador = @CodigoIndicador 
                 AND CodigoUnidadeNegocio = @CodigoUnidadeNegocioIncluida 
                 AND (Ano = @Ano OR @Ano = -1))
    BEGIN
        UPDATE miu
           SET ValorMeta = src.ValorMeta,
               DataUltimaAlteracao = @DataInclusao,
               CodigoUsuarioUltimaAlteracao = @CodigoUsuarioInclusao
          FROM [MetaIndicadorUnidade] AS miu INNER JOIN
               [MetaIndicadorUnidade] AS src ON miu.CodigoIndicador = miu.CodigoIndicador AND
                                                miu.Ano = src.Ano AND
                                                miu.Mes = src.Mes AND
                                                src.CodigoUnidadeNegocio = @CodigoUnidadeNegocio
         WHERE miu.CodigoIndicador = @CodigoIndicador 
           AND miu.CodigoUnidadeNegocio = @CodigoUnidadeNegocioIncluida 
           AND (miu.Ano = @Ano OR @Ano = -1)        
    END
    ELSE
    BEGIN
        INSERT INTO [MetaIndicadorUnidade]
                   ([CodigoIndicador]
                   ,[CodigoUnidadeNegocio]
                   ,[Ano]
                   ,[Mes]
                   ,[ValorMeta]
                   ,[DataInclusao]
                   ,[CodigoUsuarioInclusao])
        SELECT @CodigoIndicador,
               @CodigoUnidadeNegocioIncluida,
               miu.Ano,
               miu.Mes,
               miu.ValorMeta,
               @DataInclusao,
               @CodigoUsuarioInclusao
          FROM [MetaIndicadorUnidade] AS miu 
         WHERE miu.CodigoIndicador = @CodigoIndicador 
           AND miu.CodigoUnidadeNegocio = @CodigoUnidadeNegocio 
           AND (miu.Ano = @Ano OR @Ano = -1)
    END
       
    FETCH NEXT FROM cCursor INTO @CodigoUnidadeNegocioIncluida
END

CLOSE cCursor
DEALLOCATE cCursor"
            , codigoIndicador
            , codigoUnidadeNegocio
            , ano
            , idUsuarioLogado
            , codigoEntidade); 

        #endregion

        int registrosAfetados = 0;

        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "DesdMetEst");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "DesdMetEst", lblTituloTela.Text, this);
    }

    #endregion
    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if (e.Column.Name == "FormulaIndicador")
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.Text = "Possui fórmula";
            }
            else
            {
                if (e.RowType == GridViewRowType.Data)
                {
                    if (e.TextValue != null)
                    {
                        if (e.Text != "")
                        {
                            e.Text = "Sim";
                            e.TextValue = "Sim";
                        }
                        else
                        {
                            e.Text = "Não";
                            e.TextValue = "Não";
                        }
                        
                    }
                    else
                    {
                        e.Text = "Não";
                        e.TextValue = "Não";
                    }
                }
            }
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        populaGrid();
    }
}
