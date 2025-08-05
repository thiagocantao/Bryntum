using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using DevExpress.Web;

public partial class _Estrategias_wizard_IndicadoresCompartilhamento : System.Web.UI.Page
{
    dados cDados;
    public string definicaoEntidadePlural = "Entidades", definicaoUnidadePlural = "Unidades", definicaoEntidadeSingular = "Entidade", definicaoUnidadeSingular = "Unidade";
    private int codigoUsuarioResponsavel = 0;
    private int codigoEntidadeUsuarioResponsavel = 0;
    private int codigoIndicador = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

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

        definicaoEntidadePlural = Resources.traducao.entidades;
        definicaoUnidadePlural = Resources.traducao.unidades;
        definicaoEntidadeSingular = Resources.traducao.entidade;
        definicaoUnidadeSingular = Resources.traducao.unidade;

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok
        
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource2.ConnectionString = cDados.classeDados.getStringConexao();
        
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {       
        codigoIndicador = int.Parse(Request.QueryString["CodigoIndicador"].ToString());
        this.TH(this.TS("IndicadoresCompartilhamento"));
        DataSet ds = cDados.getDefinicaoEntidade(codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoEntidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
            definicaoEntidadeSingular = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

        ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoUnidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
            definicaoUnidadeSingular = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

        if (definicaoEntidadePlural != "" &&  definicaoUnidadePlural != "")
        {
            if (definicaoEntidadePlural != definicaoUnidadePlural)
                gvDados.SettingsText.Title = definicaoEntidadePlural + "/" + definicaoUnidadePlural + Resources.traducao.IndicadoresCompartilhamento__selecionadas;
            else
                gvDados.SettingsText.Title = definicaoEntidadePlural + " Selecionadas";

            if (definicaoEntidadeSingular != definicaoUnidadeSingular)
            {
                gvDados.Columns["NomeUnidadeNegocio"].Caption = definicaoEntidadeSingular + "/" + definicaoUnidadeSingular;
                lblUnidade.Text = definicaoEntidadeSingular + "/" + definicaoUnidadeSingular + ":";
            }
            else
            {
                gvDados.Columns["NomeUnidadeNegocio"].Caption = definicaoEntidadeSingular;
                lblUnidade.Text = definicaoEntidadeSingular + ":";
            }
        }

        carregaGvDados();

        txtNomeIndicador.Text = cDados.getNomeIndicador(codigoIndicador);

        cDados.aplicaEstiloVisual(this);

        ddlResponsavel.TextField = "NomeUsuario";
        ddlResponsavel.ValueField = "CodigoUsuario";

        ddlResponsavelResultado.TextField = "NomeUsuario";
        ddlResponsavelResultado.ValueField = "CodigoUsuario";

        carregaComboUnidades();
        gvDados.JSProperties["cp_Msg"] = "";
        gvDados.JSProperties["cp_AtualizarCombo"] = "N";
        gvDados.JSProperties["cp_FechaEdicao"] = "N";

        gvDados.Settings.VerticalScrollableHeight = 210;
    }

    private void carregaGvDados()
    {
        DataSet ds = cDados.getUnidadesEntidadesSelecionadasCompartilhamentoIndicador(codigoIndicador, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    #region Combos

    protected void ddlResponsavel_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            SqlDataSource2.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            SqlDataSource2.SelectParameters.Clear();
            SqlDataSource2.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = SqlDataSource2;
            comboBox.DataBind();
        }
        else
        {
            comboBox.DataSource = SqlDataSource1;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    private void carregaComboUnidades()
    {
        DataSet ds = cDados.getUnidadesEntidadesDisponiveisCompartilhamentoIndicador(codigoEntidadeUsuarioResponsavel, codigoIndicador, "");

        if (cDados.DataSetOk(ds))
        {
            ddlUnidades.DataSource = ds;
            ddlUnidades.TextField = "NomeUnidadeNegocio";
            ddlUnidades.ValueField = "CodigoUnidadeNegocio";
            ddlUnidades.DataBind();
        }
    }

    #endregion

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDados.JSProperties["cp_Msg"] = "";
        gvDados.JSProperties["cp_AtualizarCombo"] = "N";
        string mensagem = "";

        gvDados.JSProperties["cp_FechaEdicao"] = "N";

        if (e.Parameters == "S")
        {
            string codigoResponsavel = ddlResponsavel.Value != null && ddlResponsavel.Value.ToString() != "-1" ? ddlResponsavel.Value.ToString() : "NULL";
            string codigoResponsavelResultados = ddlResponsavelResultado.Value != null && ddlResponsavelResultado.Value.ToString() != "-1" ? ddlResponsavelResultado.Value.ToString() : "NULL";

            try
            {

                string insertIndicadorUnidade = @"
            BEGIN
                DECLARE 
                      @Unidade          Int
                    , @DataExclusao     Datetime
                    , @CodigoIndicador Int
                
                    SET @CodigoIndicador = " + codigoIndicador;

                insertIndicadorUnidade += comandoInsertIndicadorNaUnidade(int.Parse(ddlUnidades.Value.ToString()), codigoResponsavel, codigoResponsavelResultados);
                insertIndicadorUnidade += @"
            END";

                string comandoSQL = insertIndicadorUnidade;

                int registrosAfetados = 0;

                cDados.execSQL(comandoSQL, ref registrosAfetados);

                if (registrosAfetados > 0)
                {
                    mensagem = Resources.traducao.IndicadoresCompartilhamento_indicador_compartilhado_com_sucesso_;
                    gvDados.JSProperties["cp_AtualizarCombo"] = "S";
                    carregaGvDados();
                }
                else
                    mensagem = Resources.traducao.IndicadoresCompartilhamento_o_indicador_n_o_foi_compartilhado_;
            }
            catch (Exception ex)
            {
                mensagem = Resources.traducao.IndicadoresCompartilhamento_erro_ao_salvar_o_registro__ + ex.Message;
            }
        }
        else if (e.Parameters != "" && e.Parameters.Contains('X'))
        {
            string comandoDelete = "";

            try
            {
                int codigoUnidadeParametro = int.Parse(e.Parameters.Split(';')[1]);

                verificacaoDescompartilhamentoOk(codigoUnidadeParametro, out comandoDelete, out mensagem);

                if (mensagem == "")
                {
                    int registrosAfetados = 0;

                    string comandoSQL = @"
            BEGIN
                DECLARE 
                      @Unidade          Int
                    , @DataExclusao     Datetime
                    , @CodigoIndicador Int
                
                    SET @CodigoIndicador = " + codigoIndicador;

                    comandoSQL += comandoDelete;
                    comandoSQL += @"
            END";

                    cDados.execSQL(comandoSQL, ref registrosAfetados);

                    if (registrosAfetados > 0)
                    {
                        mensagem = Resources.traducao.IndicadoresCompartilhamento_compartilhamento_exclu_do_com_sucesso_;
                        gvDados.JSProperties["cp_AtualizarCombo"] = "S";
                        carregaGvDados();
                    }
                    else
                        mensagem = Resources.traducao.IndicadoresCompartilhamento_o_compartilhamento_n_o_foi_exclu_do_;
                }
            }
            catch (Exception ex)
            {
                mensagem = Resources.traducao.IndicadoresCompartilhamento_erro_ao_salvar_o_registro__ + ex.Message;
            }

        }
        else if (e.Parameters != "" && e.Parameters.Contains('E'))
        {
            try
            {
                int codigoUnidadeParametro = int.Parse(e.Parameters.Split(';')[1]);
                string codigoResponsavel = ddlResponsavel.Value != null && ddlResponsavel.Value.ToString() != "-1" ? ddlResponsavel.Value.ToString() : "NULL";
                string codigoResponsavelResultados = ddlResponsavelResultado.Value != null && ddlResponsavelResultado.Value.ToString() != "-1" ? ddlResponsavelResultado.Value.ToString() : "NULL";

                int registrosAfetados = 0;

                string comandoSQL = string.Format(@"
                        UPDATE {0}.{1}.IndicadorUnidade SET CodigoResponsavelIndicadorUnidade = {2}, CodigoResponsavelAtualizacaoIndicador = {5} WHERE CodigoIndicador = {3} AND CodigoUnidadeNegocio = {4}"
                    , cDados.getDbName(), cDados.getDbOwner(), codigoResponsavel, codigoIndicador, codigoUnidadeParametro, codigoResponsavelResultados);

                cDados.execSQL(comandoSQL, ref registrosAfetados);

                if (registrosAfetados > 0)
                {
                    mensagem = Resources.traducao.IndicadoresCompartilhamento_respons_veis_alterados_com_sucesso_;
                    gvDados.JSProperties["cp_FechaEdicao"] = "S";
                    gvDados.JSProperties["cp_AtualizarCombo"] = "S";
                    carregaGvDados();
                }
                else
                    mensagem = Resources.traducao.IndicadoresCompartilhamento_os_respons_veis_n_o_foram_alterados_;

            }
            catch (Exception ex)
            {
                mensagem = "Erro ao salvar o registro: " + ex.Message;
            }
        }        

        gvDados.JSProperties["cp_Msg"] = mensagem;
    }

    private string comandoInsertIndicadorNaUnidade(int unidadeSelecionada, string codigoResponsavel, string codigoResponsavelResultados)
    {
        string comandoSQL = string.Format(@"
        SET @Unidade        = NULL 
        SET @DataExclusao   = NULL
    
          SELECT @Unidade = [CodigoUnidadeNegocio], @DataExclusao = [DataExclusao] FROM {0}.{1}.[IndicadorUnidade]
            WHERE [CodigoIndicador] = @CodigoIndicador AND [CodigoUnidadeNegocio] = {2}

            IF (@Unidade IS NULL) BEGIN
                 INSERT INTO {0}.{1}.[IndicadorUnidade]
                       ([CodigoIndicador]
                       ,[CodigoUnidadeNegocio]
                       ,[CodigoResponsavelIndicadorUnidade]
                       ,[IndicaUnidadeCriadoraIndicador]
                       ,[DataInclusao]
                       ,[CodigoUsuarioInclusao]
                       ,[CodigoResponsavelAtualizacaoIndicador])
                 VALUES
                       (@CodigoIndicador, {2}, {3}, 'N', GETDATE(), {4}, {5})
            END
            ELSE BEGIN
                UPDATE {0}.{1}.[IndicadorUnidade]
                    SET [CodigoResponsavelIndicadorUnidade]     = {3}
                       ,[DataInclusao]                          = GETDATE()
                       ,[CodigoUsuarioInclusao]                 = {4}
                       ,[DataExclusao]                          = NULL
                       ,[CodigoUsuarioExclusao]                 = NULL
                       ,[CodigoResponsavelAtualizacaoIndicador] = {5}
                    WHERE
                            [CodigoIndicador]           = @CodigoIndicador
                        AND [CodigoUnidadeNegocio]      = {2}
            END     
", cDados.getDbName(), cDados.getDbOwner(), unidadeSelecionada, codigoResponsavel, codigoUsuarioResponsavel, codigoResponsavelResultados);

        return comandoSQL;
    }

    private bool verificacaoDescompartilhamentoOk(int codigoUnidade, out string comandoDelete, out string stringImpedimento)
    {
        bool retorno = true;

        string motivoImpedimento;
        int impedimentoUnidade;
        comandoDelete = "";
        stringImpedimento = "";

        impedimentoUnidade = obtemImpedimentoUnidade(codigoUnidade);

        // caso NÃO haja impedimentos
        if (0 == impedimentoUnidade)
        {
            // monta comando para retirar o indicador da unidade em questão
            comandoDelete += comandoDeleteIndicadorNaUnidade(codigoUnidade);
        }
        else
        {
            retorno = false;
            switch (impedimentoUnidade)
            {
                case 3:
                    motivoImpedimento = Resources.traducao.IndicadoresCompartilhamento_exist_ncia_de_metas_e_objetivos_estrat_gicos_relacionados_ao_indicador_;
                    break;
                case 2:
                    motivoImpedimento = Resources.traducao.IndicadoresCompartilhamento_exist_ncia_de_metas_relacionadas_ao_indicador_;
                    break;
                case 1:
                    motivoImpedimento = Resources.traducao.IndicadoresCompartilhamento_exist_ncia_objetivos_estrat_gicos_relacionados_ao_indicador_;
                    break;
                default:
                    motivoImpedimento = "";
                    break;
            }
            stringImpedimento = motivoImpedimento;
        }

        return retorno;
    }

    private string comandoDeleteIndicadorNaUnidade(int codigoUnidade)
    {
        string comandoSQL = string.Format(@"

---------------------------------------------------
----  'apaga' o indicador na unidade em questão
UPDATE {0}.{1}.[IndicadorUnidade] 
    SET 
          [DataExclusao] = GETDATE()
        , [CodigoUsuarioExclusao] = {3}
    WHERE 
            [CodigoIndicador]                   = @CodigoIndicador 
        AND [CodigoUnidadeNegocio]              = {2}
---------------------------------------------------



                    ", cDados.getDbName(), cDados.getDbOwner(), codigoUnidade, codigoUsuarioResponsavel);
        return comandoSQL;
    }

    private int obtemImpedimentoUnidade(int unidade)
    {
        int impedimentoUnidade = 0;

        if (EstahIndicadorRelacionadoAObjetivo(unidade))
            impedimentoUnidade |= 1;

        if (ExisteMetaEstipuladaParaIndicador(unidade))
            impedimentoUnidade |= 2;

        return impedimentoUnidade;
    }

    private bool EstahIndicadorRelacionadoAObjetivo(int unidade)
    {
        string comandoSQL = string.Format(@"
            SELECT TOP 1 ioe.[CodigoIndicador] 
                FROM 
		            {0}.{1}.[IndicadorObjetivoEstrategico]		AS [ioe]
		
	                INNER JOIN {0}.{1}.[ObjetoEstrategia]		AS [oe]
				        ON ( oe.[CodigoObjetoEstrategia]	    = ioe.[CodigoObjetivoEstrategico] )
					
				    INNER JOIN {0}.{1}.[MapaEstrategico]		AS [me]
						ON ( me.[CodigoMapaEstrategico]		    = oe.[CodigoMapaEstrategico] )

				    INNER JOIN {0}.{1}.[TipoObjetoEstrategia]	AS [toe]
						ON ( toe.[CodigoTipoObjetoEstrategia]	= oe.[CodigoTipoObjetoEstrategia] )

	            WHERE
				        ioe.[CodigoIndicador]				= {2}
		            AND me.[CodigoUnidadeNegocio]			= {3}
                    AND toe.[IniciaisTipoObjeto]            = 'OBJ'
            ", cDados.getDbName(), cDados.getDbOwner(), codigoIndicador, unidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
            return true;
        else
            return false;
    }

    private bool ExisteMetaEstipuladaParaIndicador(int unidade)
    {
        string comandoSQL = string.Format(@"
            SELECT TOP 1 miu.[CodigoUnidadeNegocio] 
                FROM {0}.{1}.[MetaIndicadorUnidade]	AS [miu] 
                WHERE miu.[CodigoIndicador] = {2} AND miu.[CodigoUnidadeNegocio] = {3} 
            ", cDados.getDbName(), cDados.getDbOwner(), codigoIndicador, unidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
            return true;
        else
            return false;
    }

    protected void ddlUnidades_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        //if (e.Parameter == "A")
        //    ddlResponsavel.SelectedIndex = -1;
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            string tipo = gvDados.GetRowValues(e.VisibleIndex, "Tipo") + "";
            
            if (tipo == "Unidade")
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CompartInd");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "abrePopupEdicao(null);", true, true, false, "CompartInd", "Compartilhamento de Indicadores", this);
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
}