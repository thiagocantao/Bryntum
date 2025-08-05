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

public partial class _Estrategias_wizard_compartilhamentoIndicador : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    /// <summary>
    /// classe para guardar uma lista de unidades com seus códigos e descrições.
    /// </summary>
    /// <remarks>
    /// Usada nas funções de verificações antes da gravação dos dados;
    /// </remarks>
    protected class ListaDeUnidades
    { 
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;
        public ListaDeUnidades()
        {
            ListaDeCodigos = new List<int>();
            ListaDeNomes = new List<string>();
        }
        public void Clear()
        {
            ListaDeCodigos.Clear();
            ListaDeNomes.Clear();
        }

        /// <summary>
        /// Adiciona um item na lista de unidades
        /// </summary>
        /// <param name="codigoUnidade">Código da unidade a adicionar</param>
        /// <param name="descricaoUnidade">Descrição da unidade a adicionar</param>
        public void Add(int codigoUnidade, string descricaoUnidade)
        {
            ListaDeCodigos.Add(codigoUnidade);
            ListaDeNomes.Add(descricaoUnidade);
        }

        public string GetDescricaoUnidade(int codigoUnidade)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoUnidade);
            if ( (index >=0) && (index < ListaDeNomes.Count) )
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoUnidade)
        {
            return ListaDeCodigos.Contains(codigoUnidade);
        }

    }

    dados cDados;
    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuario;

    private char delimitadorValores = '$';
    private char delimitadorElementoLista = '¢';

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (cDados.getInfoSistema("IDUsuarioLogado") == null)
        {
            Response.RedirectLocation = "~/erros/erroInatividade.aspx";
            Response.End();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuario = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsCallback)
        {
            cDados.aplicaEstiloVisual(Page);
            lblTituloTela.Text = Request.QueryString["Tit"].ToString();

            populaDdlUnidadeNegocio();

            //if (cDados.getInfoSistema("UnidadeSelecionadaCombo") != null)
            //    ddlUnidadeNegocio.Value = int.Parse(cDados.getInfoSistema("UnidadeSelecionadaCombo").ToString());

           // if (ddlUnidadeNegocio.SelectedIndex >= 0)
            //{
                /* --> Alterado por Ericssonem 17/04/2010.Não há necessidade de escolher a unidade de negócio
                 *     para compartilhar os indicadores.
                 populaGridIndicadores(int.Parse(ddlUnidadeNegocio.Value.ToString()));
                cDados.setInfoSistema("UnidadeSelecionadaCombo", int.Parse(ddlUnidadeNegocio.Value.ToString()));
                 */
               
                cDados.setInfoSistema("UnidadeSelecionadaCombo", codigoEntidadeUsuario);
            //}
        }
        populaGridIndicadores(codigoEntidadeUsuario);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/compartilhamentoIndicador.js""></script>"));
        this.TH(this.TS("compartilhamentoIndicador"));
    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 195);
        if (altura > 0)
            gvIndicadores.Settings.VerticalScrollableHeight = altura - 50;
    }

    #endregion

    #region --- [Grid Indicadores]
    private void populaDdlUnidadeNegocio()
    {
        //DataSet ds = cDados.getUnidadeNegocio(codigoUsuarioResponsavel);
        //ddlUnidadeNegocio.TextField = "nomeUnidade";
        //ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
        //ddlUnidadeNegocio.DataSource = ds.Tables[0];
        //ddlUnidadeNegocio.DataBind();

        //if (!IsPostBack)
        //    ddlUnidadeNegocio.SelectedIndex = 0;
    }

    private void populaGridIndicadores(int codigoUnidadeNegocio)
    {
        string comandoSQL = string.Format(@"
                SELECT 
		            i.[CodigoIndicador]
		            , i.[NomeIndicador] 
	            FROM 
		            {0}.{1}.[Indicador]						    AS [i]
			            INNER JOIN {0}.{1}.[IndicadorUnidade]	AS [iu]
				            ON (iu.[CodigoIndicador] = i.[CodigoIndicador])
	            WHERE
        		        iu.[CodigoUnidadeNegocio]		    = {2}
		            AND iu.[IndicaUnidadeCriadoraIndicador]	= 'S'
		            AND iu.[DataExclusao]					IS NULL
                ORDER BY 
                    i.[NomeIndicador] ", dbName, dbOwner, codigoUnidadeNegocio);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvIndicadores.DataSource = ds;
            gvIndicadores.DataBind();
        }
    }

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

    #region --- [Popup Compartilhamento de Indicadores]

    #region --- [Manipulação dos List Boxes] 

    protected void lbItensDisponiveis_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codIndicador;
            int codigoIndicador;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do indicador seguido do [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codIndicador = e.Parameter.Substring(7, posDelimit - 7);

                    if (int.TryParse(codIndicador, out codigoIndicador))
                    {
                        populaListaBox_UnidadesDisponiveis(codigoIndicador);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_UnidadesDisponiveis(int codigoIndicador)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
    SELECT  
			un.[CodigoUnidadeNegocio]
		, un.[NomeUnidadeNegocio]
	FROM 
		{0}.{1}.[UnidadeNegocio]								AS [un]
	WHERE
			un.[CodigoUnidadeNegocioSuperior]	= {2}
        AND un.[CodigoUnidadeNegocio]	        != un.[CodigoUnidadeNegocioSuperior]
		AND un.[DataExclusao]					IS NULL
		AND un.[IndicaUnidadeNegocioAtiva]		= 'S'
        AND un.[CodigoUnidadeNegocio] =un.[CodigoEntidade]
		AND NOT EXISTS( SELECT 1 FROM  {0}.{1}.[IndicadorUnidade]	AS [iu] 
											WHERE iu.[CodigoUnidadeNegocio]	= un.[CodigoUnidadeNegocio] 
												AND iu.[CodigoIndicador]	= {3}
                                                AND iu.[DataExclusao]       IS NULL )
	ORDER BY 
		un.[NomeUnidadeNegocio] ", dbName, dbOwner, codigoEntidadeUsuario, codigoIndicador);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensDisponiveis.DataSource = dt;
            lbItensDisponiveis.TextField = "NomeUnidadeNegocio";
            lbItensDisponiveis.ValueField = "CodigoUnidadeNegocio";
            lbItensDisponiveis.DataBind();
        }
    }

    protected void lbItensSelecionados_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codIndicador;
            int codigoIndicador;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do indicador seguido do [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codIndicador = e.Parameter.Substring(7, posDelimit - 7);

                    if (int.TryParse(codIndicador, out codigoIndicador))
                    {
                        populaListaBox_UnidadesSelecionadas(codigoIndicador);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_UnidadesSelecionadas(int codigoIndicador)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
    SELECT  
	        un.[CodigoUnidadeNegocio]
		, un.[NomeUnidadeNegocio]
	FROM 
		{0}.{1}.[UnidadeNegocio]								AS [un]
			INNER JOIN {0}.{1}.[IndicadorUnidade]	AS [iu] 
				ON ( iu.[CodigoUnidadeNegocio]	= un.[CodigoUnidadeNegocio] )
	WHERE
			iu.[CodigoIndicador]					= {2}
		AND iu.[IndicaUnidadeCriadoraIndicador]		!= 'S'
        AND iu.[DataExclusao]                       IS NULL
	ORDER BY 
		un.[NomeUnidadeNegocio]  ", dbName, dbOwner, codigoIndicador);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensSelecionados.DataSource = dt;
            lbItensSelecionados.TextField = "NomeUnidadeNegocio";
            lbItensSelecionados.ValueField = "CodigoUnidadeNegocio";
            lbItensSelecionados.DataBind();
        }
    }

    #endregion

    #region --- [Gravação das Informações]

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_LastOperation"] = e.Parameter;
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            pcMensagemGravacao.Modal = false;
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
        {
            pcMensagemGravacao.Modal = true;
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
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
        if (gvIndicadores.FocusedRowIndex >= 0)
            return gvIndicadores.GetRowValues(gvIndicadores.FocusedRowIndex, gvIndicadores.KeyFieldName).ToString();
        else
            return "";
    }

    private void salvaRegistro(string modo, int codigoIndicador)
    {
        string sqlDadosIndicador = "";
        string sqlInsertUnidades;
        string comandoSQL;

        montaInsertUnidades(modo, codigoIndicador, out sqlInsertUnidades);

        if (modo.Equals("E"))
        {
            sqlDadosIndicador = string.Format(@"
                DECLARE @CodigoIndicador Int
                SET @CodigoIndicador = {0}

                ", codigoIndicador);

        } // else if (modo.Equals("E"))

        comandoSQL = sqlDadosIndicador + sqlInsertUnidades;
        int registrosAfetados = 0;
        cDados.execSQL(comandoSQL, ref registrosAfetados);
    }

    private void montaInsertUnidades(string modo, int codigoIndicador, out string comandoSQL)
    {
        string deleteDadosAntigos = "", insertIndicadorUnidade = "";
        ListaDeUnidades unidadesSelecionadas = new ListaDeUnidades();
        ListaDeUnidades unidadesPreExistentes = new ListaDeUnidades();

        obtemListaUnidadesSelecionadas(codigoIndicador, ref unidadesSelecionadas);
        obtemListaUnidadesPreExistentes(codigoIndicador, ref unidadesPreExistentes);

        // verifica se está ocorrendo algum 'descompartilhamento' e se isto não está deixando a base inconsistente.
        // devolve o comando de delete para as unidades das quais está sendo retirado o indicador
        if (false == verificacaoDescompartilhamentoOk(codigoIndicador, unidadesSelecionadas, unidadesPreExistentes, out deleteDadosAntigos))
            throw new Exception("Erro ao gravar os dados. O indicador não pode ser retirado de algumas unidades.");
        else
        {
            insertIndicadorUnidade = @"
BEGIN
    DECLARE 
          @Unidade          Int
        , @DataExclusao     Datetime";

            foreach (int unidadeSelecionada in unidadesSelecionadas.ListaDeCodigos)
            {
                // só atua nas unidades ainda não estavam previamente selecionadas
                if (false == unidadesPreExistentes.ContemCodigo(unidadeSelecionada))
                {
                    insertIndicadorUnidade += comandoInsertIndicadorNaUnidade(unidadeSelecionada);
                } // if (false == unidadesPreExistentes.ContemCodigo(unidadeSelecionada))
            } //  foreach (int unidadeSelecionada in listaUnidades)

            insertIndicadorUnidade += @"
END";
        }

        comandoSQL = deleteDadosAntigos + insertIndicadorUnidade;
    }

    private string comandoInsertIndicadorNaUnidade(int unidadeSelecionada)
    {
        string comandoSQL = string.Format(@"

--------------------------------------------------------------------------------------------------------
----  [Incluindo ou atualizando [IndicadorUnidade] ]
--....................................................................................................--

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
               ,[CodigoUsuarioInclusao])
         VALUES
               (@CodigoIndicador, {2}, {3}, 'N', GETDATE(), {3})
    END
    ELSE BEGIN
        UPDATE {0}.{1}.[IndicadorUnidade]
            SET [CodigoResponsavelIndicadorUnidade] = {3}
               ,[DataInclusao]                      = GETDATE()
               ,[CodigoUsuarioInclusao]             = {3}
               ,[DataExclusao]                      = NULL
               ,[CodigoUsuarioExclusao]             = NULL
            WHERE
                    [CodigoIndicador]           = @CodigoIndicador
                AND [CodigoUnidadeNegocio]      = {2}
    END
--....................................................................................................--
----  fim de [Incluindo ou atualizando [IndicadorUnidade] ]
--------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------
----  [Incluindo e/ou atualizando [DadoUnidade] ]
--....................................................................................................--

--------------------------------------------
---- inserindo os registros que não existir

INSERT INTO [dbo].[DadoUnidade]
	(		
          [CodigoDado]
		, [CodigoUnidadeNegocio]
		, [IndicaUnidadeCriadoraDado]
		, [DataInclusao]
		, [CodigoUsuarioInclusao]
	)
	SELECT 
          di.[CodigoDado]
		, {2}
		, 'N'
		, GETDATE()
		, {3}
		FROM [dbo].[DadoIndicador]			AS [di]
		WHERE
				di.[CodigoIndicador]		= @CodigoIndicador
			AND NOT EXISTS( SELECT 1 FROM [dbo].[DadoUnidade] AS [du] 
					WHERE du.[CodigoDado] = di.[CodigoDado] AND du.[CodigoUnidadeNegocio] = {2} )
--------------------------------------------

--------------------------------------------
---- 'desapagando' os registros que tiverem [DataExclusao] 

UPDATE [dbo].[DadoUnidade]
	SET 
		  [DataInclusao]				= GETDATE()
		, [CodigoUsuarioInclusao]		= {3}
		, [DataExclusao]				= NULL
		, [CodigoUsuarioExclusao]		= NULL
	FROM 
			[dbo].[DadoUnidade]									AS [du]
				INNER JOIN [dbo].[DadoIndicador]	AS [di]
					ON (di.[CodigoDado] = du.[CodigoDado])
	WHERE
				di.[CodigoIndicador]		= @CodigoIndicador
		AND du.[CodigoUnidadeNegocio]		= {2}
		AND du.DataExclusao							IS NOT NULL
--------------------------------------------
--....................................................................................................--
----  fim de [Incluindo e/ou atualizando [DadoUnidade] ]
--------------------------------------------------------------------------------------------------------

", dbName, dbOwner, unidadeSelecionada, codigoUsuarioResponsavel);

        return comandoSQL;
    }


    /// <summary>
    /// Verifica se não há nenhum problema em retirar o compartilhamento de indicadores
    /// </summary>
    /// <remarks>
    /// Caso esteja sendo retirado o compartilhamento de algum indicador, verifica se não registro
    /// de informações que impeça a retirada do compartilhamento.
    /// Para cada problema encontrado, é insirida uma linha na grid de impedimentos encontrados da tela.
    /// </remarks>
    /// <param name="codigoIndicador">Código do indicador compartilhado entre as unidades</param>
    /// <param name="unidadesSelecionadas">Lista das unidades que estão sendo selecionadas para gravação</param>
    /// <param name="unidadesPreExistentes">Lista das unidades existentes na base de dados quando se iniciou a edição dos dados</param>
    /// <returns></returns>
    private bool verificacaoDescompartilhamentoOk(int codigoIndicador, ListaDeUnidades unidadesSelecionadas, ListaDeUnidades unidadesPreExistentes, out string comandoDelete)
    {
        bool retorno = true;
        DataTable dt = DataTableGridImpedimento();
        DataRow newRow;

        string descricaoUnidade, motivoImpedimento;
        int impedimentoUnidade;
        comandoDelete = "";

        foreach (int unidadePreExistente in unidadesPreExistentes.ListaDeCodigos)
        {
            // se a unidade que estava no banco de dados foi retirada, verifica se 
            // não há impedimento para isto.
            if (false == unidadesSelecionadas.ContemCodigo(unidadePreExistente))
            {
                impedimentoUnidade = obtemImpedimentoUnidade(codigoIndicador, unidadePreExistente);

                // caso NÃO haja impedimentos
                if (0 == impedimentoUnidade)
                {
                    // monta comando para retirar o indicador da unidade em questão
                    comandoDelete += comandoDeleteIndicadorNaUnidade(unidadePreExistente);
                }
                else
                {
                    retorno = false;
                    descricaoUnidade = unidadesPreExistentes.GetDescricaoUnidade(unidadePreExistente);
                    switch (impedimentoUnidade)
                    {
                        case 3:
                            motivoImpedimento = "Existência de metas e objetivos estratégicos relacionados ao indicador.";
                            break;
                        case 2:
                            motivoImpedimento = "Existência de metas relacionadas ao indicador.";
                            break;
                        case 1:
                            motivoImpedimento = "Existência objetivos estratégicos relacionados ao indicador.";
                            break;
                        default:
                            motivoImpedimento = "";
                            break;
                    }
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = descricaoUnidade;
                    newRow["Impedimento"] = motivoImpedimento;
                    dt.Rows.Add(newRow);
                } // if (0 != impedimentoUnidade)

            } // if (false == unidadesSelecionadas.ListaDeCodigos.Contains(unidadePreExistente))
        } // foreach (int unidadePreExistente in unidadesPreExistentes.ListaDeCodigos)

        gvImpedimentos.DataSource = dt;
        gvImpedimentos.DataBind();
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

---------------------------------------------------
----  'apaga', da unidade em questão, o relacionamento 
----  com todos os dados do indicador. Apaga somente para 
----  os dados que não estão sendo usados por outro indicador 
----  nesta unidade


UPDATE {0}.{1}.[DadoUnidade]
    SET 
          [DataExclusao] = GETDATE()
        , [CodigoUsuarioExclusao] = {3}
		FROM
			{0}.{1}.[DadoIndicador]					AS [di1]
				INNER JOIN {0}.{1}.[DadoUnidade]	AS [du1] 
					ON (du1.[CodigoDado] = di1.[CodigoDado] )
    WHERE 
            di1.[CodigoIndicador]               = @CodigoIndicador 
        AND du1.[CodigoUnidadeNegocio]          = {2}
        AND NOT EXISTS( SELECT 1 
                    FROM 
                        {0}.{1}.[IndicadorUnidade]              AS [iu] 
                            INNER JOIN {0}.{1}.[DadoIndicador]  AS [di2] 
                                ON ( di2.[CodigoIndicador]   = iu.[CodigoIndicador] )
                    WHERE
                            iu.[CodigoUnidadeNegocio]       = {2}
                        AND iu.[CodigoIndicador]            != @CodigoIndicador
                        AND iu.[DataExclusao]               IS NULL
                        AND di2.[CodigoDado]                 = di1.[CodigoDado] )
---------------------------------------------------

                    ", dbName, dbOwner, codigoUnidade, codigoUsuarioResponsavel);
        return comandoSQL;
    }

    private DataTable DataTableGridImpedimento()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("NomeUnidade", Type.GetType("System.String"));
        NewColumn.Caption = "Unidade";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("Impedimento", Type.GetType("System.String"));
        NewColumn.Caption = "Impedimento";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        return dtResult;
    }

    private int obtemImpedimentoUnidade(int codigoIndicador, int unidade)
    {
        int impedimentoUnidade = 0;

        if (EstahIndicadorRelacionadoAObjetivo(codigoIndicador, unidade))
            impedimentoUnidade |= 1;

        if (ExisteMetaEstipuladaParaIndicador(codigoIndicador, unidade))
            impedimentoUnidade |= 2;

        return impedimentoUnidade;
    }

    /// <summary>
    /// Verifica se determinado indicador está relacionado a alguma objetivo estratégico para 
    /// uma unidade.
    /// </summary>
    /// <param name="codigoIndicador"></param>
    /// <param name="unidade"></param>
    /// <returns></returns>
    private bool EstahIndicadorRelacionadoAObjetivo(int codigoIndicador, int unidade)
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
            ", dbName, dbOwner, codigoIndicador, unidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
            return true;
        else
            return false;
    }

    private bool ExisteMetaEstipuladaParaIndicador(int codigoIndicador, int unidade)
    {
        string comandoSQL = string.Format(@"
            SELECT TOP 1 miu.[CodigoUnidadeNegocio] 
                FROM {0}.{1}.[MetaIndicadorUnidade]	AS [miu] 
                WHERE miu.[CodigoIndicador] = {2} AND miu.[CodigoUnidadeNegocio] = {3} 
            ", dbName, dbOwner, codigoIndicador, unidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
            return true;
        else
            return false;
    }

    private void obtemListaUnidadesSelecionadas(int codigoIndicador, ref ListaDeUnidades listaDeUnidades)
    {
        obtemListaUnidades("Sel_", codigoIndicador, ref listaDeUnidades);
    }

    private void obtemListaUnidadesPreExistentes(int codigoIndicador, ref ListaDeUnidades listaDeUnidades)
    {
        obtemListaUnidades("InDB_", codigoIndicador, ref listaDeUnidades);
    }

    private bool obtemListaUnidades(string inicial, int codigoIndicador, ref ListaDeUnidades listaDeUnidades)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaUnidades, temp;

        idLista = inicial + codigoIndicador + delimitadorValores;

        listaDeUnidades.Clear();

        if (hfUnidades.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfUnidades.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaUnidades = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaUnidades.Length; j++)
            {
                if (strListaUnidades[j].Length > 0)
                {
                    temp = strListaUnidades[j].Split(delimitadorValores);
                    listaDeUnidades.Add(int.Parse(temp[1]), temp[0]);
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }


    #endregion

    #endregion
}
