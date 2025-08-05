//Revisado
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
using DevExpress.Web.ASPxTreeList;
using System.IO;
using System.Collections.Generic;
using DevExpress.Web;

public partial class espacoTrabalho_frameEspacoTrabalho_Biblioteca : System.Web.UI.Page
{
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
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoUnidade)
        {
            return ListaDeCodigos.Contains(codigoUnidade);
        }

    }
    dados cDados;

    private char delimitadorValores = '$';
    private char delimitadorElementoLista = '¢';

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoTipoAssociacao;
    int IDObjetoAssociado;
    public string alturaTabela;
    public string mostraBarraTitulo = "";
    public string mostrarIconesEdicao = "block";
    public string estiloFooter = "dxtlFooter";
    string IniciaisTipoAssociacao = "";
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    bool somenteLeitura = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados();

        codigoUsuarioResponsavel = 373;
        codigoEntidadeUsuarioResponsavel = 1;

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString() == "S";

        IniciaisTipoAssociacao = (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "") ? Request.QueryString["TA"].ToString() : "";
        IDObjetoAssociado = (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "") ? int.Parse(Request.QueryString["ID"].ToString()) : -1;


        //mostrarIconesEdicao = podeAdministrar ? "block" : "none";

        if (IniciaisTipoAssociacao != "IN")
            mostraBarraTitulo = "display:none;";

        // se os objetos da tela estiverem associados com outros objetos...
        if (IniciaisTipoAssociacao != "")
            codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(IniciaisTipoAssociacao);

        hfGeral.Set("IniciaisTipoAssociacao", IniciaisTipoAssociacao);
        hfGeral.Set("IDObjetoAssociado", IDObjetoAssociado);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        string cssPostfix = "";

        if (cssPostfix != "")
            estiloFooter += "_" + cssPostfix;

        //        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/AnexoBibliotecaDocumentos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));

        // esconde o cabecçalho das colunas
        tlAnexos.Settings.ShowColumnHeaders = false;


        populaTreeListArquivos();

        tlAnexos.JSProperties["cp_Path"] = VirtualPathUtility.ToAbsolute("~/") + "espacoTrabalho";
        int altura = 400;
        pnAnexos.Height = new Unit((altura - 210) + "px");
        pnCallback.Height = (altura - 185);
    }

    
    #region TREELIST


    public bool anexoEPasta(int codigoAnexo)
    {
        bool retorno = false;
        try
        {
            string indicapasta = "";
            string comandoSQL = string.Format(@"SELECT IndicaPasta FROM Anexo WHERE CodigoAnexo = {2}", codigoAnexo);
            DataSet ds = cDados.getDataSet(comandoSQL);
            if (ds != null)
            {
                indicapasta = ds.Tables[0].Rows[0]["IndicaPasta"].ToString();
            }
            retorno = (indicapasta == "S");

        }
        catch
        {
            retorno = false;
        }
        return retorno;
    }

    public DataSet getAnexos(int CodigoObjetoAssociado, int codigoTipoAssociacao, int codigoEntidade)
    {
        string comandoSQL = string.Format(@"
                    SELECT a.CodigoAnexo, a.DescricaoAnexo, convert(varchar(20), a.DataInclusao, 113) as DataInclusao, 
                           a.CodigoUsuarioInclusao, a.Nome, a.CodigoEntidade, a.CodigoPastaSuperior, a.IndicaPasta, 
                           a.IndicaControladoSistema, U.NomeUsuario, PSUP.Nome AS NomePastaSuperior,
                           a.dataCheckOut, a.codigoUsuarioCheckOut, a.dataCheckIn, UCK.NomeUsuario as nomeUsuarioCheckout,
                           (select MAX(numeroVersao) from AnexoVersao where codigoAnexo = a.CodigoAnexo) UltimaVersao
                      FROM Anexo AS a INNER JOIN
                           AnexoAssociacao AS aa ON (aa.CodigoAnexo = a.CodigoAnexo ) INNER JOIN
                           Usuario AS U ON a.CodigoUsuarioInclusao = U.CodigoUsuario LEFT OUTER JOIN
                           Anexo AS PSUP ON a.CodigoPastaSuperior = PSUP.CodigoAnexo LEFT OUTER JOIN
                           Usuario AS UCK ON a.codigoUsuarioCheckOut = UCK.CodigoUsuario
                     WHERE a.dataExclusao is null
                       AND aa.CodigoObjetoAssociado = {2} 
                       AND aa.CodigoTipoAssociacao = {3} 
                       
                       -- AND a.CodigoEntidade = {4}   -- deixado de levar em conta a entidade do anexo uma vez que todos os anexos estão associados a um objeto
                   ORDER BY CodigoPastaSuperior, IndicaPasta desc, Nome"
                    , "", "", CodigoObjetoAssociado, codigoTipoAssociacao, codigoEntidade);
        return cDados.getDataSet(comandoSQL);
    }

    private void populaTreeListArquivos()
    {
        DataSet ds = getAnexos(IDObjetoAssociado, codigoTipoAssociacao, codigoEntidadeUsuarioResponsavel);
        if ((cDados.DataSetOk(ds)))
        {
            tlAnexos.DataSource = ds.Tables[0];
            tlAnexos.DataBind();
        }
        if (tlAnexos.Nodes.Count == 0)
        {
            tlAnexos.Settings.ShowColumnHeaders = true;
            tlAnexos.Columns[0].Caption = "Nenhuma informação a ser apresentada!";
        }
    }


    protected bool GetPermissaoCompartilhar(TreeListDataCellTemplateContainer container)
    {
        return false;
    }

    protected string GetIconUrl(TreeListDataCellTemplateContainer container)
    {
        ASPxButton btnDownLoad = tlAnexos.FindDataCellTemplateControl(container.NodeKey, null, "btnDownLoad") as ASPxButton;
        string nomeIcone = "pastaCompartilhada.PNG";
        string IndicaPasta = container.GetValue("IndicaPasta").ToString();
        //ASPxImage imgIndicaCompartilhou = tlAnexos.FindDataCellTemplateControl(container.NodeKey, null, "imgIndicaCompartilhou") as ASPxImage;

        bool EhdaEntidadeLogada = false;


        if (IndicaPasta == "N")
        {
            btnDownLoad.ClientInstanceName = "Btn_CodigoAnexo_" + container.GetValue("CodigoAnexo").ToString();
            if (btnDownLoad != null)
                btnDownLoad.Visible = true;
            if (EhdaEntidadeLogada)
            {
                nomeIcone = "arquivo.gif";
                //imgIndicaCompartilhou.ClientVisible = cDados.anexoEstaCompartilhadoEmEntidades(int.Parse(container.NodeKey), codigoEntidadeUsuarioResponsavel);

            }
            else
            {
                nomeIcone = "arquivoCompartilhado.PNG";
            }

        }
        else
        {
            if (EhdaEntidadeLogada)
            {
                nomeIcone = "pasta.GIF";
                //imgIndicaCompartilhou.ClientVisible = cDados.anexoEstaCompartilhadoEmEntidades(int.Parse(container.NodeKey), codigoEntidadeUsuarioResponsavel);
            }
            if (btnDownLoad != null)
                btnDownLoad.Visible = false;
        }
        return string.Format("~/imagens/anexo/{0}", nomeIcone);
    }

    protected string GetToolTip(TreeListDataCellTemplateContainer container)
    {
        string toolTip = "Incluído em: " + container.GetValue("DataInclusao").ToString() + Environment.NewLine + "por: " + container.GetValue("NomeUsuario").ToString() + Environment.NewLine + "==================================" + Environment.NewLine + container.GetValue("DescricaoAnexo").ToString();
        return toolTip;
    }

    protected string GetVersaoIcone(TreeListDataCellTemplateContainer container)
    {
        string icone = "";
        // apenas para arquivos
        if (container.GetValue("IndicaPasta").ToString() == "N")
        {
            string link = string.Format("window.top.showModal('{1}espacoTrabalho/frameEspacoTrabalho_BibliotecaListaVersoes.aspx?CA={0}', 'Versões', 1000, (screen.height - 255), '', null)", int.Parse(container.GetValue("codigoAnexo").ToString()), VirtualPathUtility.ToAbsolute("~/"));
            icone = string.Format("<a href=\"javascript:{0}\"><img alt='Visualizar versões do arquivo' src='../imagens/anexo/version.png'></a>", link);
        }
        return icone;
    }

    protected string GetVersaoTexto(TreeListDataCellTemplateContainer container)
    {
        string versao = "";
        // apenas para arquivos
        if (container.GetValue("IndicaPasta").ToString() == "N")
            versao = string.Format("(Versão: {0:D2}) ", (int)container.GetValue("UltimaVersao"));
        return versao;
    }

    #endregion

    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        string CodigoAnexo = (sender as ASPxButton).ClientInstanceName.Substring(16);
       // cDados.download(int.Parse(CodigoAnexo), null, Page, Response, Request, true);

    }

    #region --- [Popup Compartilhamento de Indicadores]

    #region --- [Manipulação dos List Boxes]

    protected void lbItensDisponiveis_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codIndicador;
            int codigoDocumentoPasta;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do indicador seguido do [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codIndicador = e.Parameter.Substring(7, posDelimit - 7);

                    if (int.TryParse(codIndicador, out codigoDocumentoPasta))
                    {
                        populaListaBox_UnidadesDisponiveis(codigoDocumentoPasta);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_UnidadesDisponiveis(int codigoDocumentoPasta)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
    SELECT  
			un.[CodigoUnidadeNegocio]
		, un.[NomeUnidadeNegocio]
	FROM 
		dbo.[UnidadeNegocio]								AS [un]
	WHERE
			un.[CodigoUnidadeNegocioSuperior]	= {2}
        AND un.[CodigoUnidadeNegocio]	        != un.[CodigoUnidadeNegocioSuperior]
		AND un.[DataExclusao]					IS NULL
		AND un.[IndicaUnidadeNegocioAtiva]		= 'S'
        AND un.[CodigoUnidadeNegocio] =un.[CodigoEntidade]
		AND NOT EXISTS(SELECT un.[CodigoUnidadeNegocio] FROM dbo.AnexoAssociacao aa
	                    WHERE aa.CodigoTipoAssociacao=(SELECT  CodigoTipoAssociacao FROM TipoAssociacao WHERE IniciaisTipoAssociacao='EN') AND AA.CodigoObjetoAssociado = un.CodigoUnidadeNegocio and aa.CodigoAnexo = {3})
	ORDER BY 
		un.[NomeUnidadeNegocio] ", "","", codigoEntidadeUsuarioResponsavel, codigoDocumentoPasta);
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
            int codigoDocumentoPasta;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do indicador seguido do [delimitadorValores]
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codIndicador = e.Parameter.Substring(7, posDelimit - 7);

                    if (int.TryParse(codIndicador, out codigoDocumentoPasta))
                    {
                        populaListaBox_UnidadesSelecionadas(codigoDocumentoPasta);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_UnidadesSelecionadas(int codigoDocumentoPasta)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
        SELECT un.[CodigoUnidadeNegocio]
		       ,un.[NomeUnidadeNegocio]
	     FROM dbo.[UnidadeNegocio]	AS [un]
   INNER JOIN dbo.AnexoAssociacao aa ON ((un.[CodigoUnidadeNegocio] = aa.[CodigoObjetoAssociado]) and (aa.CodigoTipoAssociacao in (SELECT CodigoTipoAssociacao FROM dbo.TipoAssociacao WHERE IniciaisTipoAssociacao='EN')))
	WHERE
			un.[CodigoUnidadeNegocioSuperior]	= {2}
        AND un.[CodigoUnidadeNegocio]	        != un.[CodigoUnidadeNegocioSuperior]
		AND un.[DataExclusao]					IS NULL
		AND un.[IndicaUnidadeNegocioAtiva]		= 'S'
        AND un.[CodigoUnidadeNegocio] =un.[CodigoEntidade]
        and aa.CodigoAnexo = {3}
		ORDER BY un.[NomeUnidadeNegocio] ", "","", codigoEntidadeUsuarioResponsavel, codigoDocumentoPasta);
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
        hfGeral.Set("ErroSalvar", "");
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Listar")
        {
            populaTreeListArquivos();
            //defineAlturaTela(resolucaoCliente);
        }
        if (e.Parameter == "setaControles")
        {
            int codigoAnexo1 = int.Parse(hfGeral.Contains("CodigoAnexo") == true ? hfGeral.Get("CodigoAnexo").ToString() : "-1");
            imgAnexarArquivoPasta.ClientVisible = false;
            imgEditarArquivoPasta.ClientVisible = false;
            imgIncluirPastaRaiz.ClientVisible = false;

            if (false)
            {
                imgExcluirArquivoPasta.ClientVisible = true;
                imgAnexarArquivoPasta.ClientVisible = true;
                imgEditarArquivoPasta.ClientVisible = true;
                imgIncluirPastaRaiz.ClientVisible = true;
                //se o arquivo tá dentro de uma pasta então não precisa de mostrar o botão de compartilhar
                //já que as permissões são herdadas da pasta que o arquivo/pasta estão dentro.
                if ((hfGeral.Contains("CodigoPastaSuperior") && hfGeral.Get("CodigoPastaSuperior").ToString() == "-1") && !anexoEPasta(codigoAnexo1))
                {
                    btnCompartilhar.ClientVisible = true;
                }
                if ((anexoEPasta(codigoAnexo1)))
                {
                    btnCompartilhar.ClientVisible = true;
                }
            }
            else
            {
                //se o arquivo é de outra entidade e está dentro de uma pasta
                //botão de anexar arquivo não pode estar disponivel para nao anexar arquivo dentro de uma pasta que é
                //se está navegando pela raiz então pode incluir uma arquivo na raiz
                if ((hfGeral.Contains("CodigoPastaSuperior") && hfGeral.Get("CodigoPastaSuperior").ToString() == "-1") && anexoEPasta(codigoAnexo1) == false)
                {
                    imgAnexarArquivoPasta.ClientVisible = true;
                    imgEditarArquivoPasta.ClientVisible = false;
                    imgIncluirPastaRaiz.ClientVisible = true;
                }
            }
            if (codigoAnexo1 == -1)
            {
                //é porque adicionou um anexo do tipo pasta raiz e o seu codigo só será gerado depois que já estiver 
                //incluido, nesse caso
                imgAnexarArquivoPasta.ClientVisible = true;
                imgEditarArquivoPasta.ClientVisible = true;
                imgIncluirPastaRaiz.ClientVisible = true;
            }
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "excluir")
        {
            int codigoAnexo = int.Parse(hfGeral.Contains("CodigoAnexo") == true ? hfGeral.Get("CodigoAnexo").ToString() : "-1");

            int codigoPastaSup = int.Parse(hfGeral.Contains("CodigoPastaSuperior") == true ? hfGeral.Get("CodigoPastaSuperior").ToString() : "-1");

            string indicaPasta = hfGeral.Contains("IndicaPasta") == true ? hfGeral.Get("IndicaPasta").ToString() : "N";
            string erro = "";
            bool estaCompartilhado = false;
            if (estaCompartilhado)
            {
                erro = "O arquivo ou pasta em que se deseja excluir está compartilhado com outras unidades, para excluir remova o compartilhamento.";
                hfGeral.Set("ErroSalvar", erro);
                mensagemErro_Persistencia = erro;
            }
            else if (indicaPasta != "")
            {
                bool retorno = cDados.excluiAnexoProjeto(char.Parse(indicaPasta), codigoAnexo, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, IDObjetoAssociado, codigoTipoAssociacao, ref erro);
                if (!retorno)
                {
                    hfGeral.Set("ErroSalvar", erro);
                    mensagemErro_Persistencia = erro;
                }
                else
                {
                    hfGeral.Set("CodigoAnexo", -1);
                    hfGeral.Set("CodigoPastaSuperior", -1);
                    hfGeral.Set("IndicaPasta", "");

                }
            }
            populaTreeListArquivos();
            pnCallback.JSProperties["cp_OperacaoOk"] = "excluir";
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

        if (IniciaisTipoAssociacao != "EN")
            btnCompartilhar.ClientVisible = false;

    }

    private string persisteEdicaoRegistro()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();
            return salvaRegistro("E", int.Parse(chave));
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string getChavePrimaria()
    {
        //string valor = tlAnexos.FocusedNode.GetValue("CodigoAnexo").ToString();
        //return valor;
        return hfGeral.Contains("CodigoAnexo") ? hfGeral.Get("CodigoAnexo").ToString() : "-1";
    }

    private string salvaRegistro(string modo, int codigoDocumentoPasta)
    {
        string erro = "";
        try
        {
            string sqlInsertUnidades;
            string comandoSQL;

            montaInsertUnidades(modo, codigoDocumentoPasta, out sqlInsertUnidades);

            comandoSQL = sqlInsertUnidades;
            int registrosAfetados = 0;
            if (comandoSQL != "")
                cDados.execSQL(comandoSQL, ref registrosAfetados);

            return "";
        }
        catch (Exception ex)
        {
            erro = ex.Message;
            return erro;
        }
    }

    private void montaInsertUnidades(string modo, int codigoDocumentoPasta, out string comandoSQL)
    {
        string insertIndicadorUnidade = "";
        ListaDeUnidades unidadesSelecionadas = new ListaDeUnidades();
        ListaDeUnidades unidadesPreExistentes = new ListaDeUnidades();

        obtemListaUnidadesSelecionadas(codigoDocumentoPasta, ref unidadesSelecionadas);
        obtemListaUnidadesPreExistentes(codigoDocumentoPasta, ref unidadesPreExistentes);

        int codigoPastaSup = int.Parse(hfGeral.Contains("CodigoPastaSuperior") == true ? hfGeral.Get("CodigoPastaSuperior").ToString() : "-1");


        insertIndicadorUnidade = "";
        if (anexoEPasta(codigoDocumentoPasta))
        {
            //se o anexo clicado for uma pasta então deve-se excluir a propria pasta e os anexos dentro dela
            bool retorno1 = cDados.delete("AnexoAssociacao", string.Format(@"
            CodigoAnexo 
            IN(SELECT a.CodigoAnexo FROM dbo.Anexo a WHERE a.CodigoPastaSuperior = {2}) 
            AND CodigoObjetoAssociado != {3} 
            AND CodigoTipoAssociacao IN(SELECT CodigoTipoAssociacao 
                                          FROM dbo.TipoAssociacao 
                                         WHERE IniciaisTipoAssociacao = 'EN')"
          , "","", codigoDocumentoPasta, codigoEntidadeUsuarioResponsavel, codigoPastaSup));

            retorno1 = cDados.delete("AnexoAssociacao", string.Format(@"
            CodigoAnexo = {2}
            AND CodigoObjetoAssociado != {3} 
            AND CodigoTipoAssociacao IN(SELECT CodigoTipoAssociacao 
                                          FROM dbo.TipoAssociacao 
                                         WHERE IniciaisTipoAssociacao = 'EN')"
              , "","", codigoDocumentoPasta, codigoEntidadeUsuarioResponsavel, codigoDocumentoPasta));



            insertIndicadorUnidade = string.Format(@"BEGIN
            DECLARE @codTipoAssociacao as Int 
            SELECT @codTipoAssociacao = CodigoTipoAssociacao  FROM dbo.TipoAssociacao WHERE iniciaisTipoAssociacao='EN'", "","");
            foreach (int unidadeSelecionada in unidadesSelecionadas.ListaDeCodigos)
            {
                insertIndicadorUnidade += comandoInsertPastaNaUnidade(unidadeSelecionada);
            }
            if (unidadesSelecionadas.ListaDeCodigos.Count > 0)
                insertIndicadorUnidade += " END";
            else
                insertIndicadorUnidade = "";
        }
        else
        {

            //se for um arquivo deve-se excluir somente o arquivo sem se preocupar com a pasta que ele está
            bool retorno1 = cDados.delete("AnexoAssociacao", string.Format(@"
           CodigoAnexo = {2} AND CodigoObjetoAssociado != {3} 
            AND CodigoTipoAssociacao IN(SELECT CodigoTipoAssociacao 
                                          FROM dbo.TipoAssociacao 
                                         WHERE IniciaisTipoAssociacao = 'EN')"

          , "","", codigoDocumentoPasta, codigoEntidadeUsuarioResponsavel, codigoPastaSup));

            foreach (int unidadeSelecionada in unidadesSelecionadas.ListaDeCodigos)
            {
                insertIndicadorUnidade += comandoInsertAnexoNaUnidade(unidadeSelecionada);
            }
        }

        comandoSQL = insertIndicadorUnidade;
    }

    private string comandoInsertPastaNaUnidade(int unidadeSelecionada)
    {
        string codigoPastaSelecionada = getChavePrimaria();

        string comandoSQL = string.Format(@"
           INSERT INTO dbo.[AnexoAssociacao] ([CodigoAnexo] ,[CodigoObjetoAssociado],[CodigoTipoAssociacao])
               (SELECT                             a.CodigoAnexo,                    {3},@codTipoAssociacao FROM dbo.Anexo a 
                 WHERE a.CodigoPastaSuperior = {2})
        ", "","", codigoPastaSelecionada, unidadeSelecionada);

        comandoSQL += string.Format(@"
            INSERT INTO dbo.[AnexoAssociacao] ([CodigoAnexo] ,[CodigoObjetoAssociado],[CodigoTipoAssociacao])
                                            VALUES(           {2},                    {3},@codTipoAssociacao)
         ", "","", codigoPastaSelecionada, unidadeSelecionada);

        return comandoSQL;
    }

    private string comandoInsertAnexoNaUnidade(int unidadeSelecionada)
    {
        string codigoanexoSelecionado = getChavePrimaria();

        string comandoSQL = string.Format(@"
            INSERT INTO dbo.[AnexoAssociacao] ([CodigoAnexo] ,[CodigoObjetoAssociado],[CodigoTipoAssociacao])
                                            VALUES(           {2},                    {3},(SELECT CodigoTipoAssociacao 
                                                                                             FROM dbo.TipoAssociacao 
                                                                                            WHERE iniciaisTipoAssociacao='EN'))
         ", "","", codigoanexoSelecionado, unidadeSelecionada);
        return comandoSQL;
    }

    private void obtemListaUnidadesSelecionadas(int codigoDocumentoPasta, ref ListaDeUnidades listaDeUnidades)
    {
        obtemListaUnidades("Sel_", codigoDocumentoPasta, ref listaDeUnidades);
    }

    private void obtemListaUnidadesPreExistentes(int codigoDocumentoPasta, ref ListaDeUnidades listaDeUnidades)
    {
        obtemListaUnidades("InDB_", codigoDocumentoPasta, ref listaDeUnidades);
    }

    private bool obtemListaUnidades(string inicial, int codigoDocumentoPasta, ref ListaDeUnidades listaDeUnidades)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaUnidades, temp;

        idLista = inicial + codigoDocumentoPasta + delimitadorValores;

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
