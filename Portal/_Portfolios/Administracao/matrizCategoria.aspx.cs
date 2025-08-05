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
using System.Collections.Generic;

public partial class _Portfolios_Administracao_matrizCategoria : System.Web.UI.Page
{
    #region ==== Variáveis e classes da Classe ==== 
    protected class ListaDeCriterios
    {
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;
        public ListaDeCriterios()
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
        /// Adiciona um item na lista de criterios
        /// </summary>
        /// <param name="codigoCriterio">Código do critério a adicionar</param>
        /// <param name="descricaoCriterio">Descrição do criério a adicionar</param>
        public void Add(int codigoCriterio, string descricaoCriterio)
        {
            ListaDeCodigos.Add(codigoCriterio);
            ListaDeNomes.Add(descricaoCriterio);
        }

        public string GetDescricaoCriterio(int codigoCriterio)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoCriterio);
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoCriterio)
        {
            return ListaDeCodigos.Contains(codigoCriterio);
        }

    }

    dados cDados;
    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoCategoria = -1;
    private char delimitadorValores = '|';
    private char delimitadorElementoLista = '¢';
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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CC"] != null && Request.QueryString["CC"].ToString() != "")
            codigoCategoria = int.Parse(Request.QueryString["CC"].ToString());

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();
        int altura = 0;
       // int altura = 560;
        int largura = 900;
        if ((Request.QueryString["alt"] != null && Request.QueryString["alt"].ToString() != "") ||
            (Request.QueryString["larg"] != null && Request.QueryString["larg"].ToString() != ""))
        {
            altura = int.Parse(Request.QueryString["alt"].ToString());
            largura = int.Parse(Request.QueryString["larg"].ToString());
        }

        //tlDados.Height = 430;
        //tlDados.Settings.ScrollableHeight = 430;
      //tlDados.Settings.VerticalScrollBarMode = DevExpress.Web.ScrollBarMode.Visible;
        //tlDados.Settings.HorizontalScrollBarMode = DevExpress.Web.ScrollBarMode.Visible;
        // tlDados.BackColor = System.Drawing.Color.FromName("#f0f0f0");

        int maximo = altura - 100;
       // tlDados.Style.Add("max-height", maximo.ToString() + "px" );
        //tlDados.Height = new Unit((altura - 455) + "px");

        //if (!IsPostBack)
        cDados.aplicaEstiloVisual(Page);

        tlDados.JSProperties["cp_Categoria"] = codigoCategoria;
        this.TH(this.TS("geral", "matrizCategoria"));
        carregaGrid();
    }

    private void carregaGrid()
    {
        DataSet ds = cDados.getMatrizCategoria(codigoCategoria, "");

        if (cDados.DataSetOk(ds))
        {
            tlDados.DataSource = ds;
            tlDados.DataBind();
        }
    }

    public string montaLinksMatriz(string codigoObjetoCriterio, string nomeObjetoCriterio, string iniciaisTipoObjetoCriterio, string pesoObjetoMatriz)
    {
        string link = "";
        float valorPeso = float.Parse(pesoObjetoMatriz);

        if (iniciaisTipoObjetoCriterio == "CR")
            return string.Format("{0} ({1:n2}%)", nomeObjetoCriterio, valorPeso);

        if (iniciaisTipoObjetoCriterio == "CT")
            link = string.Format(@"<a href='#'   onclick='abreEdicaoPesos({1}, ""{2}"");'>{0}</a>", nomeObjetoCriterio, codigoObjetoCriterio, iniciaisTipoObjetoCriterio);
        else if (iniciaisTipoObjetoCriterio == "FT")
        {
            link = string.Format(@"<table><tr><td><img style='cursor:pointer' src='../../imagens/botoes/incluirReg02.png' title='" + Resources.traducao.matrizCategoria_incluir_novo_grupo + @"' onclick='abreNovoGrupo({1});'/></td><td><a href='#' onclick='abreEdicaoPesos({1}, ""{2}"");'>{0}</a> ({3:n2}%)</td></tr></table>", nomeObjetoCriterio, codigoObjetoCriterio, iniciaisTipoObjetoCriterio, valorPeso);
        }
        else if (iniciaisTipoObjetoCriterio == "GP")
        {
            link = string.Format(@"<table><tr><td><img style='cursor:pointer' src='../../imagens/botoes/editarReg02.PNG' title='" + Resources.traducao.matrizCategoria_editar_grupo + @"' onclick='editaGrupo(""{0}"", {1});'/></td><td ><img style='cursor:pointer' src='../../imagens/botoes/excluirReg02.PNG' title='" +  Resources.traducao.matrizCategoria_excluir_grupo + @"' onclick='excluiGrupo({1});'/></td><td><a href='#' title='" + Resources.traducao.matrizCategoria_editar_peso_dos_crit_rios + @"' onclick='abreEdicaoPesos({1}, ""{2}"");'>{0}</a> ({3:n2}%)</td></tr></table>", nomeObjetoCriterio, codigoObjetoCriterio, iniciaisTipoObjetoCriterio, valorPeso);
            
        }
        else
        {
            link = string.Format(@"<a href='#'  onclick='abreEdicaoPesos({1}, ""{2}"");'>{0}</a> ({3:n2}%)", nomeObjetoCriterio, codigoObjetoCriterio, iniciaisTipoObjetoCriterio, valorPeso);
        }             
        return link;
    }

    #region ==== Manutenção no Cadastro de Grupos ====
    protected void tlDados_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        int regAf = 0;
        if (e.Argument.ToString() != "")
        {
            if (e.Argument.ToString().Substring(0, 1) == "I")
            {
                int codigoFator = int.Parse(e.Argument.ToString().Substring(1));

                bool result = incluiGrupoFatorPortfolio(txtGrupo.Text, codigoEntidade, codigoCategoria, codigoFator, idUsuarioLogado, ref regAf);

                if (result)
                {
                    tlDados.JSProperties["cp_msg"] = Resources.traducao.matrizCategoria_grupo_inclu_do_com_sucesso_;
                    tlDados.JSProperties["cp_status"] = "0";
                    carregaGrid();
                }
                else
                {
                    tlDados.JSProperties["cp_msg"] = Resources.traducao.matrizCategoria_erro_ao_incluir_o_grupo_;
                    tlDados.JSProperties["cp_status"] = "-1";
                }
            }
            else if (e.Argument.ToString().Substring(0, 1) == "X")
            {
                int codigoGrupo = int.Parse(e.Argument.ToString().Substring(1));

                bool result = cDados.excluiGrupoFator(codigoGrupo, idUsuarioLogado, ref regAf);

                if (result)
                {
                    tlDados.JSProperties["cp_msg"] = Resources.traducao.matrizCategoria_grupo_exclu_do_com_sucesso_;
                    tlDados.JSProperties["cp_status"] = "0";
                    carregaGrid();
                }
                else
                {
                    tlDados.JSProperties["cp_msg"] = Resources.traducao.matrizCategoria_erro_ao_excluir_o_grupo_;
                    tlDados.JSProperties["cp_status"] = "-1";
                }
            }
            else if (e.Argument.ToString().Substring(0, 1) == "E")
            {
                int codigoGrupo = int.Parse(e.Argument.ToString().Substring(1));

                bool result = atualizaGrupoFatorPortfolio(txtGrupo.Text, codigoGrupo, ref regAf);

                if (result)
                {
                    tlDados.JSProperties["cp_msg"] = Resources.traducao.matrizCategoria_grupo_alterado_com_sucesso_;
                    tlDados.JSProperties["cp_status"] = "0";
                    carregaGrid();
                }
                else
                {
                    tlDados.JSProperties["cp_msg"] = Resources.traducao.matrizCategoria_erro_ao_alterar_o_grupo_;
                    tlDados.JSProperties["cp_status"] = "-1";
                }
            }
        }
    }

    private bool incluiGrupoFatorPortfolio(string descricao, int codigoEntidade, int codigoCategoria, int codigoFator, int codigoUsuarioInclusao, ref int regAfetados)
    {
        string sqlDadosGrupo = "";
        string comandoSQLCriterios = "";
        string comandoSQL = "";
        try
        {
            montaComandoSQLCriterios(out comandoSQLCriterios);
            sqlDadosGrupo = string.Format(@"
                DECLARE @CodigoGrupoCriterio Int
                INSERT INTO {0}.{1}.GrupoCriterioSelecao (NomeGrupo, DataInclusao, CodigoUsuarioInclusao, CodigoEntidade, CodigoCategoria, CodigoFatorPortfolio)
				                                        VALUES('{2}', GetDate(), {3}, {4}, {5}, {6});
                SET @CodigoGrupoCriterio = SCOPE_IDENTITY();

                ", cDados.getDbName(), cDados.getDbOwner(), descricao, codigoUsuarioInclusao, codigoEntidade, codigoCategoria, codigoFator);

            comandoSQL = sqlDadosGrupo + comandoSQLCriterios;
            cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool atualizaGrupoFatorPortfolio(string descricao, int codigoGrupo, ref int regAfetados)
    {
        string sqlDadosGrupo = "";
        string comandoSQLCriterios = "";
        string comandoSQL = "";
        try
        {
            montaComandoSQLCriterios(out comandoSQLCriterios);
            sqlDadosGrupo = string.Format(@"
                DECLARE @CodigoGrupoCriterio Int
                UPDATE {0}.{1}.GrupoCriterioSelecao SET NomeGrupo = '{2}'
                                          WHERE CodigoGrupoCriterio = {3};
                SET @CodigoGrupoCriterio = {3};
	                                        ", cDados.getDbName(), cDados.getDbOwner(), descricao, codigoGrupo);
            comandoSQL = sqlDadosGrupo + comandoSQLCriterios;
            cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void montaComandoSQLCriterios(out string comandoSQL)
    {
        string sqlDeleteCriteriosDesselecionados = "", sqlInsertNovosCriterios = "";
        ListaDeCriterios criteriosSelecionados = new ListaDeCriterios();
        ListaDeCriterios criteriosPreExistentes = new ListaDeCriterios();

        obtemListaCriteriosSelecionados(ref criteriosSelecionados);
        obtemListaCriteriosPreExistentes(ref criteriosPreExistentes);

        // se alguma das funções que monta os comandos para insert ou delete retornou true, é porque há alterações
        // nos registro. Neste caso, incluir a chamada a proc que recalcula os pesos dos objetos 
        if (montaDeleteCriteriosDesselecionados(criteriosSelecionados, criteriosPreExistentes, out sqlDeleteCriteriosDesselecionados)
            || montaInsertNovosCriterios(criteriosSelecionados, criteriosPreExistentes, out sqlInsertNovosCriterios))
        {
            sqlInsertNovosCriterios += string.Format(" EXEC {0}.{1}.[p_RecalculaMatrizPesoPortfolio] {2}, @CodigoGrupoCriterio, 'GP';",
                cDados.getDbName(), cDados.getDbOwner(), codigoCategoria);
        }

        comandoSQL = sqlDeleteCriteriosDesselecionados + sqlInsertNovosCriterios;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="criteriosSelecionados"></param>
    /// <param name="criteriosPreExistentes"></param>
    /// <param name="sqlInsertNovosCriterios"></param>
    /// <returns>true se houver critérios a serem inseridos</returns>
    private bool montaInsertNovosCriterios(ListaDeCriterios criteriosSelecionados, ListaDeCriterios criteriosPreExistentes, out string comandoSQL)
    {
        bool bRet = false;
        comandoSQL = "";

        foreach (int criterioSelecionado in criteriosSelecionados.ListaDeCodigos)
        {
            // se o critério selecionado não constar nos critérios pré-existentes
            // compõe comando que irá incluí-lo na matriz e na relação de peso com os outros critérios
            //  (será assumido que o novo critério terá o mesmo peso em relação demais critérios.
            if (false == criteriosPreExistentes.ContemCodigo(criterioSelecionado))
            {
                bRet = true;
                comandoSQL = comandoSQL + string.Format(@"
                INSERT INTO {0}.{1}.[MatrizObjetoCriterio] 
	                ( [CodigoCategoria], [IniciaisTipoObjetoCriterioPai], [CodigoObjetoCriterioPai]
	                , [CodigoObjetoCriterio], [PesoObjetoNivel], [PesoObjetoMatriz] )
	                VALUES
	                ( {2}, 'GP', @CodigoGrupoCriterio, {3}, 0, 0);
                	
                INSERT INTO {0}.{1}.[RelacaoObjetoCriterioPortfolio]
	                ( [CodigoCategoria], [IniciaisTipoObjetoCriterioPai], [CodigoObjetoCriterioPai]
	                , [CodigoObjetoCriterioDe], [CodigoObjetoCriterioPara], [ValorRelacaoObjetoDePara])
	                SELECT {2}, 'GP', @CodigoGrupoCriterio, {3}, moc.[CodigoObjetoCriterio], 1 FROM {0}.{1}.[MatrizObjetoCriterio] AS [moc] 
		                WHERE moc.[CodigoCategoria]					= {2}
			                AND moc.[IniciaisTipoObjetoCriterioPai] = 'GP'
			                AND moc.[CodigoObjetoCriterioPai]		= @CodigoGrupoCriterio;
                			
                INSERT INTO {0}.{1}.[RelacaoObjetoCriterioPortfolio]
	                ( [CodigoCategoria], [IniciaisTipoObjetoCriterioPai], [CodigoObjetoCriterioPai]
	                , [CodigoObjetoCriterioDe], [CodigoObjetoCriterioPara], [ValorRelacaoObjetoDePara])
	                SELECT {2}, 'GP', @CodigoGrupoCriterio, moc.[CodigoObjetoCriterio], {3}, 1 FROM {0}.{1}.[MatrizObjetoCriterio] AS [moc] 
		                WHERE moc.[CodigoCategoria]								= {2}
			                AND moc.[IniciaisTipoObjetoCriterioPai] = 'GP'
			                AND moc.[CodigoObjetoCriterioPai]		= @CodigoGrupoCriterio
			                AND moc.[CodigoObjetoCriterio]			!= {3}; -- a relação ELE<=>ELE é só 1 vez

                    ", cDados.getDbName(), cDados.getDbOwner(), codigoCategoria, criterioSelecionado);

            } // if (false == criteriosSelecionados.ContemCodigo(criterioPreExistente))
        } // foreach (int criterioSelecionado in criteriosSelecionados.ListaDeCodigos)
        return bRet;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="criteriosSelecionados"></param>
    /// <param name="criteriosPreExistentes"></param>
    /// <param name="comandoSQL"></param>
    /// <returns>true se houver critérios a serem excluídos</returns>
    private bool montaDeleteCriteriosDesselecionados(ListaDeCriterios criteriosSelecionados, ListaDeCriterios criteriosPreExistentes, out string comandoSQL)
    {
        bool bRet = false;
        comandoSQL = "";

        foreach (int criterioPreExistente in criteriosPreExistentes.ListaDeCodigos)
        {
            // se o critério não constar mais nos critérios selecionados
            // compõe comando que irá excluí-lo da matriz e da relação de peso entre os outros critérios.
            if (false == criteriosSelecionados.ContemCodigo(criterioPreExistente))
            {
                bRet = true;
                comandoSQL = comandoSQL + string.Format(@"
                DELETE {0}.{1}.[RelacaoObjetoCriterioPortfolio] 
	                WHERE [CodigoCategoria]					= {2}
		                AND [IniciaisTipoObjetoCriterioPai] = 'GP'
		                AND [CodigoObjetoCriterioPai]		= @CodigoGrupoCriterio
		                AND ( [CodigoObjetoCriterioDe]	= {3} OR [CodigoObjetoCriterioPara] = {3} )
                DELETE {0}.{1}.[MatrizObjetoCriterio]
	                WHERE [CodigoCategoria]					= {2}
		                AND [IniciaisTipoObjetoCriterioPai]	= 'GP'
		                AND [CodigoObjetoCriterioPai]		= @CodigoGrupoCriterio
		                AND [CodigoObjetoCriterio]			= {3}

                    ", cDados.getDbName(), cDados.getDbOwner(), codigoCategoria, criterioPreExistente);

            } // if (false == criteriosSelecionados.ContemCodigo(criterioPreExistente))
        } // foreach (int criterioPreExistente in criteriosPreExistentes.ListaDeCodigos)
        return bRet;
    }

    private void obtemListaCriteriosSelecionados(ref ListaDeCriterios listaDeCriterios)
    {
        obtemListaCriterios("Sel_", ref listaDeCriterios);
    }

    private void obtemListaCriteriosPreExistentes(ref ListaDeCriterios listaDeCriterios)
    {
        obtemListaCriterios("InDB_", ref listaDeCriterios);
    }

    private bool obtemListaCriterios(string inicial, ref ListaDeCriterios listaDeCriterios)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaCriterios, temp;

        idLista = inicial;

        listaDeCriterios.Clear();

        if (hfCriterios.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfCriterios.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaCriterios = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaCriterios.Length; j++)
            {
                if (strListaCriterios[j].Length > 0)
                {
                    temp = strListaCriterios[j].Split(delimitadorValores);
                    listaDeCriterios.Add(int.Parse(temp[1]), temp[0]);
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }

    #endregion

    protected void lbDisponiveisCriterios_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if ( (e.Parameter.Length >= 6) && ( e.Parameter.Substring(0, 6).ToUpper() == "POPLBX") )
            populaListaBox_CriteriosDisponiveis();
    }

    private void populaListaBox_CriteriosDisponiveis()
    {
        DataTable dt = null;
        string sComando = string.Format(@"
            DECLARE @CodigoEntidade	Int, @CodigoCategoria SmallInt
            	
                SET @CodigoEntidade			= {2}
                SET @CodigoCategoria		= {3}
            	
            SELECT 
                    cs.[CodigoCriterioSelecao], cs.[DescricaoCriterioSelecao]
                FROM
                    {0}.{1}.[CriterioSelecao]	AS [cs]
                WHERE
		                cs.[CodigoEntidade]		= @CodigoEntidade
                    AND cs.[DataExclusao]		IS NULL
                    AND NOT EXISTS( SELECT 1 FROM {0}.{1}.[MatrizObjetoCriterio] AS [moc] WHERE moc.[CodigoCategoria] = @CodigoCategoria 
													                    AND moc.[IniciaisTipoObjetoCriterioPai] = 'GP' AND moc.[CodigoObjetoCriterio] = cs.CodigoCriterioSelecao )
                 ORDER BY 2 asc 
		                ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, codigoCategoria);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbDisponiveisCriterios.DataSource = dt;
            lbDisponiveisCriterios.TextField = "DescricaoCriterioSelecao";
            lbDisponiveisCriterios.ValueField = "CodigoCriterioSelecao";
            lbDisponiveisCriterios.DataBind();
        }
    }

    protected void lbSelecionadosCriterios_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codGrupo;
            int codigoGrupo;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox de selecionados, o parâmetro tem que conter o 
                /// código do grupo após o termo 'POPLBX'
                if (e.Parameter.Length > 7)
                {
                    codGrupo = e.Parameter.Substring(7);

                    if (int.TryParse(codGrupo, out codigoGrupo))
                        populaListaBox_CriteriosSelecionados(codigoGrupo);
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_CriteriosSelecionados(int codigoGrupoCriterio)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
            DECLARE @CodigoCategoria SmallInt, @CodigoGrupoCriterio Int
            SET @CodigoCategoria		= {2}
            SET @CodigoGrupoCriterio	= {3}
            SELECT 
                cs.[CodigoCriterioSelecao], cs.[DescricaoCriterioSelecao]
            FROM
                {0}.{1}.[CriterioSelecao]					AS [cs]	
                INNER JOIN {0}.{1}.[MatrizObjetoCriterio]	AS [moc]	ON 
					(	    moc.[CodigoCategoria]				= @CodigoCategoria
						AND moc.[IniciaisTipoObjetoCriterioPai]	= 'GP'
						AND moc.[CodigoObjetoCriterioPai]		= @CodigoGrupoCriterio
						AND moc.CodigoObjetoCriterio			= cs.[CodigoCriterioSelecao] )
                WHERE
		                cs.[DataExclusao]		IS NULL
                ", cDados.getDbName(), cDados.getDbOwner(), codigoCategoria, codigoGrupoCriterio);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbSelecionadosCriterios.DataSource = dt;
            lbSelecionadosCriterios.TextField = "DescricaoCriterioSelecao";
            lbSelecionadosCriterios.ValueField = "CodigoCriterioSelecao";
            lbSelecionadosCriterios.DataBind();
        }
    }

}
