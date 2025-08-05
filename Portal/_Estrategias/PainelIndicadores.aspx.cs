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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraPrinting;
using System.Web.Hosting;
using System.Diagnostics;
using DevExpress.Web;
using System.IO;
using System.Drawing;

public partial class ListaIndicadores : System.Web.UI.Page
{
    public string alturaTabela = "";
    public string larguraTabela = "";
    dados cDados;
    private int codigoUsuarioLogado = 0;
    private int codigoEntidade = 0;

    protected void Page_Load(object sender, EventArgs e)
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                              
        populaGrid();
        defineAlturaTela();
        cDados.aplicaEstiloVisual(this);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ListaPlanosInvestimento.js""></script>"));
        this.TH(this.TS("ListaPlanosInvestimento"));

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            cDados.setInfoSistema("CodigoMapa", null);
        }
    }
        
    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        larguraTabela = gvDados.Width.ToString();
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 370;
    }

    private void populaGrid()
    {
        string where = "";

        string comandoSQL = string.Format(@"
						BEGIN
                DECLARE 
								  @CorIndicador                                  Varchar(8)
								, @CodigoIndicador                               Int
								, @CodigoEntidade                                Int
								, @NomeIndicador                                 Varchar(255)
								, @Meta                                          Varchar(2000)
								, @IndicadorResultante                           char(1)
								, @AnoAtual                                      SmallInt
								, @MesAtual                                      SmallInt
								, @CasasDecimais                                 SmallInt
								, @NomeResponsavel                               VarChar(60)
								, @CodigoUsuarioResponsavel                      Int
								, @CodigoTipoAssociacao                          Int
								, @Polaridade                                    Char(3)
								, @codigoUsuario																	Int
								, @vinculaMetasMapas                             Char(1)
								, @codigoEntidadeMapa                            Int
								, @NomeUnidadeNegocio														VarChar(60)
								, @CodigoUnidadeNegocio                          Int
								, @CodigoUsuarioRespAtualizacao									Int
								, @NomeResponsavelAtualizacao										VarChar(60)
								, @NomeOrgao									 Varchar(250)
								, @MapasEstrategicos							 Varchar(2000)

                DECLARE @tmp TABLE 
                 (            CodigoIndicador               			Int
                         ,    NomeIndicador                 			Varchar(255)
                         ,    Meta                          			Varchar(2000)
                         ,    Desempenho                    			Varchar(8)
                         ,    IndicadorResultante           			Char(1)
                         ,    CasasDecimais                 			SmallInt
                         ,    NomeResponsavel               			Varchar(60)
                         ,    CodigoUsuarioResponsavel      			Int                                               
                         ,    Polaridade                    			Char(3)
                         ,    NomeUnidadeNegocio            			Varchar(500)
						 ,	  CodigoUnidadeNegocio          			Int
						 ,	  CodigoUsuarioRespAtualizacao				Int
						 ,	  NomeResponsavelAtualizacao				VarChar(60)
						 ,    MapasEstrategicos							Varchar(2000)
						 ,    Orgao										Varchar(250)
                  )

                DECLARE @tbResumo TABLE 
                 (
                        [CodigoIndicador]						  Int
                      , [IndicaUnidadeCriadoraIndicador]          Char(1)
                      , [CodigoResponsavelIndicadorUnidade]       Int
                      , [CodigoResponsavelAtualizacaoIndicador]   Int
                      , [CodigoUnidadeNegocio]                    Int
                 )

                SET @CodigoEntidade			= {0};
                SET @codigoUsuario			= {1};

                SELECT @CodigoTipoAssociacao = CodigoTipoAssociacao
                FROM dbo.TipoAssociacao 
                WHERE IniciaisTipoAssociacao = 'IN'

                SET @AnoAtual = Year(GetDate());
                SET @MesAtual = Month(GetDate());

                INSERT INTO @tbResumo                                          
                SELECT -- dos indicadores criados na ENTIDADE atual
                         i.[CodigoIndicador]
                       , 'S'
                       , iu.[CodigoResponsavelIndicadorUnidade]
                       , iu.[CodigoResponsavelAtualizacaoIndicador]
                       , iu.[CodigoUnidadeNegocio]
                FROM
                     dbo.[Indicador]     AS [i]

                     INNER JOIN dbo.[IndicadorUnidade]                              AS [iu]    ON 
                          (       iu.[CodigoIndicador]                         = i.[CodigoIndicador]
                              AND iu.[CodigoUnidadeNegocio]                    = @CodigoEntidade
                              AND iu.[DataExclusao]                            IS NULL
                              AND iu.[IndicaUnidadeCriadoraIndicador]          = 'S'
                           )
                 WHERE   i.[DataExclusao]          IS NULL
                 UNION ALL
                 SELECT -- dos indicadores criados em UNIDADE da ENTIDADE atual
                         i.[CodigoIndicador]
                       , 'S'
                       , iu.[CodigoResponsavelIndicadorUnidade]
                       , iu.[CodigoResponsavelAtualizacaoIndicador]
                       , iu.[CodigoUnidadeNegocio]
                 FROM
                       dbo.[Indicador]                AS [i]

                       INNER JOIN dbo.[IndicadorUnidade]   AS [iu]   ON 
                            (        iu.[CodigoIndicador]                 = i.[CodigoIndicador]
                                  AND iu.[DataExclusao]                   IS NULL
                                  AND iu.[IndicaUnidadeCriadoraIndicador] = 'S'
                            )

                       INNER JOIN dbo.[UnidadeNegocio]                      AS [un]     ON 
                            (         un.[CodigoUnidadeNegocio]       = iu.[CodigoUnidadeNegocio]
                                  AND un.DataExclusao                 IS NULL
                                  AND un.[IndicaUnidadeNegocioAtiva]  = 'S'
                                  AND un.[CodigoEntidade]             = @CodigoEntidade
                            )
                 WHERE   i.[DataExclusao]          IS NULL
                 AND NOT EXISTS
                      ( SELECT TOP 1 1 
												FROM dbo.[IndicadorUnidade]            AS [iu2]
                        WHERE iu2.CodigoIndicador                  = i.CodigoIndicador
                        AND iu2.DataExclusao                       IS NULL
                        AND iu2.[CodigoUnidadeNegocio]             = @CodigoEntidade
                        AND iu2.[IndicaUnidadeCriadoraIndicador]   = 'S'
                      );

                INSERT INTO @tbResumo                                          
                SELECT -- dos indicadores criados em outra entidade, mas compartilhado com a entidade atual
                        i.[CodigoIndicador]
                       , 'N'
                       , iu.[CodigoResponsavelIndicadorUnidade]
                       , iu.[CodigoResponsavelAtualizacaoIndicador]
                       , iu.[CodigoUnidadeNegocio]
                 FROM
                     dbo.[Indicador]    AS [i]

                     INNER JOIN dbo.[IndicadorUnidade]   AS [iu]          ON 
                          (        iu.[CodigoIndicador]                       = i.[CodigoIndicador]
                               AND iu.[CodigoUnidadeNegocio]                  = @CodigoEntidade
                               AND iu.[DataExclusao]                          IS NULL
                               AND iu.[IndicaUnidadeCriadoraIndicador]        != 'S'
                          )
                   WHERE   i.[DataExclusao]  IS NULL
                   AND i.[CodigoIndicador] NOT IN ( SELECT [CodigoIndicador] FROM @tbResumo )
                            
                
				DECLARE cCursorIndicador CURSOR FOR 
                   SELECT DISTINCT
                              i.CodigoIndicador,
                              i.NomeIndicador,
                              iu.Meta,
                              i.IndicadorResultante,
                              i.CasasDecimais,
                              u.NomeUsuario,
                              iu.CodigoResponsavelIndicadorUnidade,
                              i.Polaridade,
                              un.NomeUnidadeNegocio,
                              tmp.CodigoUnidadeNegocio,
                              ISNULL(iu.CodigoResponsavelAtualizacaoIndicador, i.CodigoResponsavelAtualizacaoIndicador),
                              uatl.NomeUsuario
                     FROM @tbResumo      AS [tmp]

                          INNER JOIN dbo.[Indicador] AS [i] ON (i.[CodigoIndicador] = tmp.[CodigoIndicador])
                          
                          INNER JOIN dbo.[IndicadorUnidade]               AS [iu]     ON 
                                      (       iu.[CodigoIndicador]        = tmp.[CodigoIndicador] 
                                          AND iu.[CodigoUnidadeNegocio]   = tmp.[CodigoUnidadeNegocio]     
                                          AND iu.[IndicaUnidadeCriadoraIndicador] = 'S'                                                                                                                        
                                      )
                          
                          INNER JOIN dbo.UnidadeNegocio un ON un.CodigoUnidadeNegocio = iu.CodigoUnidadeNegocio
                          LEFT JOIN dbo.Usuario u ON u.CodigoUsuario = iu.CodigoResponsavelIndicadorUnidade
                          LEFT JOIN dbo.Usuario uatl ON uatl.CodigoUsuario = ISNULL(iu.CodigoResponsavelAtualizacaoIndicador, i.CodigoResponsavelAtualizacaoIndicador)
                     WHERE i.[DataExclusao]              IS NULL 
                       AND iu.[DataExclusao]             IS NULL
											 AND i.[CodigoIndicador] IN ( SELECT f.[CodigoIndicador] FROM  dbo.f_GetIndicadoresUsuario(@CodigoUsuario, @CodigoEntidade, 'N') f )
                       AND un.DataExclusao IS NULL
                       AND un.CodigoEntidade = @CodigoEntidade
                                                                              
								OPEN cCursorIndicador;

                FETCH NEXT FROM cCursorIndicador INTO 
                                    @CodigoIndicador
                               ,    @NomeIndicador
                               ,    @Meta
                               ,    @IndicadorResultante
                               ,    @CasasDecimais
                               ,    @NomeResponsavel
                               ,    @CodigoUsuarioResponsavel
                               ,    @Polaridade
                               ,    @NomeUnidadeNegocio
                               ,    @CodigoUnidadeNegocio
                               ,		@CodigoUsuarioRespAtualizacao
															 ,		@NomeResponsavelAtualizacao

                WHILE (@@FETCH_STATUS = 0) BEGIN
    
										SET @CorIndicador = dbo.f_GetUltimoDesempenhoIndicador(@CodigoUnidadeNegocio, @CodigoIndicador, @AnoAtual, @MesAtual, 'A')
										
										/* Obtém o nome do órgão no qual está vinculado o indicador em questão */
										SELECT @NomeOrgao = un.NomeUnidadeNegocio
										  FROM UnidadeNegocio AS un 
										 WHERE un.CodigoUnidadeNegocio = dbo.f_getCodigoOrgaoComMapa(@CodigoUnidadeNegocio)
										
										SET @MapasEstrategicos = dbo.f_GetMapasIndicador(@CodigoIndicador)
										IF( LEN(@MapasEstrategicos) =  0)
										BEGIN
										   SET @MapasEstrategicos = 'Sem Mapa Estratégico'
										END
										INSERT INTO @tmp
										 (         CodigoIndicador                 ,   NomeIndicador               ,  Meta
												 ,    Desempenho                       ,   IndicadorResultante         ,  CasasDecimais
												 ,    NomeResponsavel                  ,   CodigoUsuarioResponsavel    ,  Polaridade
												 ,    NomeUnidadeNegocio               ,   CodigoUnidadeNegocio				 ,	CodigoUsuarioRespAtualizacao
												 ,		NomeResponsavelAtualizacao     ,   Orgao					   ,  MapasEstrategicos
										 )
										VALUES
											(      @CodigoIndicador                  ,   @NomeIndicador               , @Meta
													,  @CorIndicador                     ,   @IndicadorResultante         , @CasasDecimais
													,  @NomeResponsavel                  ,   @CodigoUsuarioResponsavel    , @Polaridade
													,  @NomeUnidadeNegocio               ,   @CodigoUnidadeNegocio				, @CodigoUsuarioRespAtualizacao
													,	 @NomeResponsavelAtualizacao	 ,   @NomeOrgao					  , @MapasEstrategicos	
											 )

										 FETCH NEXT FROM cCursorIndicador INTO 
																			 @CodigoIndicador
																		 , @NomeIndicador
																		 , @Meta
																		 , @IndicadorResultante
																		 , @CasasDecimais
																		 , @NomeResponsavel
																		 , @CodigoUsuarioResponsavel
																		 , @Polaridade
																		 , @NomeUnidadeNegocio
																		 , @CodigoUnidadeNegocio
																		 , @CodigoUsuarioRespAtualizacao
																		 , @NomeResponsavelAtualizacao
								END

                CLOSE cCursorIndicador
                DEALLOCATE cCursorIndicador
                
                
								DELETE @tmp
								 FROM @tmp AS tmp
								WHERE EXISTS(SELECT 1 
															 FROM dbo.Indicador AS i 
															WHERE i.CodigoIndicador = tmp.CodigoIndicador
																AND GETDATE() NOT BETWEEN IsNull(i.DataInicioValidadeMeta,'01/01/1900') AND IsNull(i.DataTerminoValidadeMeta,'31/12/2999')
																AND i.IndicaAcompanhamentoMetaVigencia = 'S')

                SELECT *                                               
                  FROM @tmp tmp
                 WHERE 1 = 1
                 ORDER BY NomeIndicador;
END

", codigoEntidade, codigoUsuarioLogado);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();            
        }

    }
    
    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName != "COLLAPSEROW" && e.CallbackName != "EXPANDROW")
            gvDados.ExpandAll();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
    {
        gvDados.ExpandAll();
    }
    
    public string getLinkIndicador()
    {
        string codigoIndicador = Eval("CodigoIndicador").ToString();
        string indicador = Eval("NomeIndicador").ToString();
        string codigoUnidade = Eval("CodigoUnidadeNegocio").ToString();
        
        string descricao = string.Format("target='_top' href = './indicador/index.aspx?COIN={0}&UN={1}'", codigoIndicador, codigoUnidade);       

        return string.Format(@"<a {0} style='cursor: pointer'>{1}</a>", descricao
                                                                      , indicador);
    }
               
    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {        
        if (e.Parameters != string.Empty)
        {
            (sender as ASPxGridView).Columns[e.Parameters].Visible = false;
            (sender as ASPxGridView).Columns[e.Parameters].ShowInCustomizationForm = true;
        }
    }

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
        if (e.Column.Name == "Desempenho" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;

            if (e.Value.ToString().Equals("Verde"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Equals("Laranja"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Orange;
            }
            else if (e.Value.ToString().Equals("Vermelho"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Equals("Amarelo"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Equals("Branco"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PnlLstInd");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, true, "PnlLstInd", lblTituloTela.Text, this);
    }

    #endregion
}
