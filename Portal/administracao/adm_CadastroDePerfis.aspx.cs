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
using System.Text;
using System.Collections.Generic;

public partial class administracao_adm_CadastroDePerfis : System.Web.UI.Page
{
    protected class ListaDeUsuarios
    {
        public List<int> ListaDeCodigos;
        public List<string> ListaDeNomes;
        public ListaDeUsuarios()
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
        /// Adiciona um item na lista de Usuarios
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuario a adicionar</param>
        /// <param name="nomeUsuario">Descrição do Usuario a adicionar</param>
        public void Add(int codigoUsuario, string nomeUsuario)
        {
            ListaDeCodigos.Add(codigoUsuario);
            ListaDeNomes.Add(nomeUsuario);
        }

        public string GetDescricaoProjeto(int codigoUsuario)
        {
            string descricao = string.Empty;

            int index = ListaDeCodigos.IndexOf(codigoUsuario);
            if ((index >= 0) && (index < ListaDeNomes.Count))
                descricao = ListaDeNomes[index];

            return descricao;
        }

        public bool ContemCodigo(int codigoUsuario)
        {
            return ListaDeCodigos.Contains(codigoUsuario);
        }

    }

    dados cDados;
    DataSet ds;
    private string idUsuarioLogado;
    private string CodigoEntidade;
    private string dbName;
    private string dbOwner;
    private string resolucaoCliente = "";
    private char delimitadorValores = 'ֆ';
    private char delimitadorElementoLista = '¢';
    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;

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
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();  //usuario logado.
        CodigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();   //entidad logada.
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade), int.Parse(CodigoEntidade), "null", "EN", 0, "null", "CadPflFlx");
        }

        //if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade), "CriPflFlx"))
        podeIncluir = true;
        //if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade), "CriPflFlx"))
        podeEditar = true;
        //if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade), "CriPflFlx"))
        podeExcluir = true;
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        carregaGrid();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            defineAlturaTela();

        }

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        lblTituloTela.Text = Resources.traducao.adm_CadastroDePerfis_cadastro_de_perfis;

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/custom.css"" rel=""stylesheet""/>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_CadastroDePerfis.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "adm_CadastroDePerfis", "ASPxListbox"));
    }

    private void defineAlturaTela()
    {
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int altura2 = 0;
        int largura2 = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura2, out altura2);

        gvDados.Settings.VerticalScrollableHeight = altura2 - 320;
    }

    public void excluirGrupo()
    {
        string nomeGrupo = getNomeGrupo();
        string tipoGrupo = getTipo();
        bool retorno = false;

        retorno = cDados.excluirPerfilNotificacaoWorkflow(nomeGrupo, Resources.traducao.adm_CadastroDePerfis_excluir);

        carregaGrid();
    }

    /// <summary>
    /// Verifica se existe na base de dados um usuário com o critério informado no parâmetro <paramref name="where"/>
    /// </summary>
    /// <remarks>
    /// Em caso positivo, a função gera uma exceção caso o parâmetro <paramref name="bRaiseError"/> seja true.
    /// Se o parâmetro <paramref name="bRaiseError"/> for false, é devolvido o código do usuário encontrado ou 
    /// zero caso não tenha sido encontrado usuário algum.
    /// </remarks>
    /// <param name="codigoUsuario"></param>
    /// <param name="where"></param>
    /// <param name="mensagem"></param>
    /// <param name="bRaiseError"></param>
    /// <returns></returns>
    private bool verificaExistenciaPerfil(int codigoPerfil, string where)
    {
        // não é permitido passar o valor zero.
        // quando for inclusão, deve ser passado o valor -1;
        if (codigoPerfil == 0)
            return false; // throw new Exception("Erro ao gravar os dados. Falha interna aplicação. (código 4)");

        int codigoPerfil2 = 0;
        DataSet ds = cDados.getPerfisWorkFlow(CodigoEntidade, where);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            int.TryParse(ds.Tables[0].Rows[0]["CodigoPerfilWf"].ToString(), out codigoPerfil2);

        return (codigoPerfil2 != 0) && (codigoPerfil != codigoPerfil2);

    }

    #endregion

    #region GRID

    public void carregaGrid(int? codigo = null)
    {
        ds = cDados.getPerfisWorkFlow(CodigoEntidade, "AND StatusPerfilWf != 'D'");
        string r = Resources.traducao.adm_CadastroDePerfis_recurso_de_projeto;
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
            if (codigo.HasValue == true)
            {
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigo);
            }
        }
    }

    protected void gvDados_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        carregaGrid();
    }

    protected void gvDados_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "btnApagarCustom")
        {
            excluirGrupo();
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            string tipoPerfil = e.GetValue("tipo").ToString();

            if ("PE" != tipoPerfil)
            {
                //e.Row.BackColor = Color.FromName("#DDFFCC");
                //e.Row.ForeColor = Color.Black;
                e.Row.ForeColor = Color.FromName("#619340");
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            string codigoPerfil = (gvDados.GetRowValues(e.VisibleIndex, "CodigoPerfilWf") != null) ? gvDados.GetRowValues(e.VisibleIndex, "CodigoPerfilWf").ToString() : "";
            string tipoPerfil = (gvDados.GetRowValues(e.VisibleIndex, "tipo") != null) ? gvDados.GetRowValues(e.VisibleIndex, "tipo").ToString() : "";

            if (tipoPerfil == "RF")
            {
                tipoPerfil = "RP";
            }

            if ("btnExcluir" == e.ButtonID)
            {
                //Si Perfil se encuentra en la tabla [AcessoEtapasWf] y [RegrasnotificacionesRecursosWf]
                // entonces se puede eliminar el perfil.
                if (("RP" != tipoPerfil) && cDados.habilitarExcluirPerfisWf(codigoPerfil))
                {
                    //e.IsVisible = DevExpress.Utils.DefaultBoolean.True;
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/excluirReg02.PNG";
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    e.Text = "";
                }
            }
            else if ("btnEditar" == e.ButtonID)
            {
                if ("RP" == tipoPerfil)
                {
                    e.Enabled = true;
                    //e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                    e.Text = "";
                }
            }
            else if ("btnDetalhesCustom" == e.ButtonID)
            {
                if ("RP" == tipoPerfil)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
                    e.Text = "";
                }
            }
        }
    }

    public string getTipo()
    {
        return gvDados.GetRowValues(gvDados.FocusedRowIndex, "tipo").ToString();
    }

    public string getNomeGrupo()
    {
        return gvDados.GetRowValues(gvDados.FocusedRowIndex, "perfil").ToString();
    }

    #endregion


    #region LISTBOX

    protected void lbItensDisponiveis_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codPerfil;
            int codigoPerfil;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do usuário a partir da 7a. posição
                codPerfil = e.Parameter.Substring(7);

                if (int.TryParse(codPerfil, out codigoPerfil))
                {
                    populaListaBox_UsuariosDisponiveis(codigoPerfil);
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_UsuariosDisponiveis(int codigoPerfil)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
        SELECT	  us.NomeUsuario
                , us.CodigoUsuario
        FROM    {0}.{1}.Usuario AS us
				    INNER JOIN {0}.{1}.UsuarioUnidadeNegocio as uun 
                        ON (uun.CodigoUsuario = us.CodigoUsuario)
        WHERE uun.CodigoUnidadeNegocio = {2}
		    AND us.DataExclusao IS NULL
			AND uun.IndicaUsuarioAtivoUnidadeNegocio = 'S'
	        AND NOT EXISTS( 
		        SELECT	1
		        FROM    {0}.{1}.DetalhesTGPWfPessoasEspecificas AS dtpe
		        WHERE   us.CodigoUsuario = dtpe.IdentificadorRecurso
						AND  CodigoPerfilWf = {3}
                        AND StatusRegistro <> 'D'
	            )
        ORDER BY NomeUsuario
        ", dbName, dbOwner, CodigoEntidade, codigoPerfil);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensDisponiveis.DataSource = dt;
            lbItensDisponiveis.ValueField = "CodigoUsuario";
            lbItensDisponiveis.TextField = "NomeUsuario";
            lbItensDisponiveis.DataBind();
        }
    }

    protected void lbItensSelecionados_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codPerfil;
            int codigoPerfil;

            if (comando == "POPLBX")
            {
                /// quando se tratando de popular listBox, o parâmetro tem que conter o 
                /// código do usuário a partir da 7a. posição
                codPerfil = e.Parameter.Substring(7);
                if (int.TryParse(codPerfil, out codigoPerfil))
                {
                    populaListaBox_UsuariosSelecionados(codigoPerfil);
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_UsuariosSelecionados(int codigoPerfil)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
        SELECT    dtpe.IdentificadorRecurso
	            , us.NomeUsuario
        FROM {0}.{1}.DetalhesTGPWfPessoasEspecificas AS dtpe
	        INNER JOIN {0}.{1}.Usuario AS us 
		        ON (us.CodigoUsuario = dtpe.IdentificadorRecurso) 
        WHERE
	        dtpe.CodigoPerfilWf = {3}
            AND StatusRegistro <> 'D'
        ORDER BY 
        us.NomeUsuario
        ", dbName, dbOwner, CodigoEntidade, codigoPerfil);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensSelecionados.DataSource = dt;
            lbItensSelecionados.ValueField = "IdentificadorRecurso";
            lbItensSelecionados.TextField = "NomeUsuario";
            lbItensSelecionados.DataBind();
        }
    }

    #endregion

    #region Gravação dos dados

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {

    }

    private string persisteInclusaoRegistro()
    {
        try
        {
            salvaRegistro("I", "-1");

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro()
    {
        try
        {

            string codigoPerfil = getChavePrimaria(); ;
            salvaRegistro("E", codigoPerfil);

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteExclusaoRegistro()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            excluiRegistro(int.Parse(chave));

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    private void salvaRegistro(string modo, string codigoPerfil)
    {
        string sqlAtualizacaoUsuarios = string.Empty;
        string sqlComandoPerfil = string.Empty;
        montaComandoSQLAtualizacaoUsuarios(modo, codigoPerfil, out sqlAtualizacaoUsuarios);

        string nomePerfil = txtNomePerfil.Text.Replace("'", "''");
        if (modo.Equals("I"))
        {
            sqlComandoPerfil = string.Format(@"
DECLARE @CodigoPerfilWf Int;
INSERT INTO {0}.{1}.PerfisWf (NomePerfilWf, TipoPerfilWf, StatusPerfilWf, CodigoEntidade, DataInclusaoPerfilWf, IdentificadorUsuarioInclusao)
    VALUES ('{2}', 'PE', 'A', {3}, GETDATE(), {4})
SET @CodigoPerfilWf = SCOPE_IDENTITY()

                ", dbName, dbOwner, nomePerfil, CodigoEntidade, idUsuarioLogado);
        } // if (modo.Equals("I"))
        else if (modo.Equals("E"))
        {
            sqlComandoPerfil = string.Format(@"
DECLARE @CodigoPerfilWf Int
SET @CodigoPerfilWf = {2}

UPDATE {0}.{1}.PerfisWf SET NomePerfilWf = '{3}' WHERE CodigoPerfilWf = @CodigoPerfilWf;

                ", dbName, dbOwner, codigoPerfil, nomePerfil);

        } // else if (modo.Equals("E"))

        sqlComandoPerfil += sqlAtualizacaoUsuarios;

        int registrosAfetados = 0;
        cDados.execSQL(sqlComandoPerfil, ref registrosAfetados);
        carregaGrid();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(getChavePrimaria());
    }

    private void montaComandoSQLAtualizacaoUsuarios(string modo, string codigoPerfil, out string comandoSQL)
    {
        StringBuilder sqlDeleteUsuariosDesselecionados = new StringBuilder();
        StringBuilder sqlInsertNovosUsuarios = new StringBuilder();
        ListaDeUsuarios usuarioSelecionados = new ListaDeUsuarios();
        ListaDeUsuarios usuariosPreExistentes = new ListaDeUsuarios();

        obtemListaUsuariosSelecionados(ref usuarioSelecionados);
        obtemListaUsuariosPreExistentes(ref usuariosPreExistentes);

        montaDeleteUsuariosDesselecionados(usuarioSelecionados, usuariosPreExistentes, sqlDeleteUsuariosDesselecionados);
        montaInsertNovosUsuarios(usuarioSelecionados, usuariosPreExistentes, sqlInsertNovosUsuarios);

        comandoSQL = sqlDeleteUsuariosDesselecionados.ToString() + sqlInsertNovosUsuarios.ToString();
    }

    private void obtemListaUsuariosSelecionados(ref ListaDeUsuarios listaDeUsuarios)
    {
        obtemListaUsuarios("Sel_", ref listaDeUsuarios);
    }

    private void obtemListaUsuariosPreExistentes(ref ListaDeUsuarios listaDeUsuarios)
    {
        obtemListaUsuarios("InDB_", ref listaDeUsuarios);
    }

    private bool obtemListaUsuarios(string inicial, ref ListaDeUsuarios listaDeUsuarios)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaUsuarios, temp;

        idLista = inicial;

        listaDeUsuarios.Clear();

        if (hfUsuarios.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfUsuarios.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaUsuarios = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaUsuarios.Length; j++)
            {
                if (strListaUsuarios[j].Length > 0)
                {
                    temp = strListaUsuarios[j].Split(delimitadorValores);
                    listaDeUsuarios.Add(int.Parse(temp[1]), temp[0]);
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }

    private bool montaDeleteUsuariosDesselecionados(ListaDeUsuarios UsuariosSelecionados, ListaDeUsuarios UsuariosPreExistentes, StringBuilder comandoSQL)
    {
        bool bRet = false;

        foreach (int UsuarioPreExistente in UsuariosPreExistentes.ListaDeCodigos)
        {
            // se o Usuario não constar mais nos Usuarios selecionados, desativa o Usuario na lista 
            if (false == UsuariosSelecionados.ContemCodigo(UsuarioPreExistente))
            {
                bRet = true;
                comandoSQL.Append(string.Format(@"
UPDATE {0}.{1}.[DetalhesTGPWfPessoasEspecificas] SET [StatusRegistro] = 'D', [DataDesativacaoRegistro] = GETDATE(), [IdentificadorUsuarioDesativacao] = '{3}' 
WHERE [CodigoPerfilWf] = @CodigoPerfilWf AND [IdentificadorRecurso] = '{2}';

                ", cDados.getDbName(), cDados.getDbOwner(), UsuarioPreExistente, idUsuarioLogado));

            } // if (false == UsuariosSelecionados.ContemCodigo(UsuarioPreExistente))
        } // foreach (int UsuarioPreExistente in UsuariosPreExistentes.ListaDeCodigos)
        return bRet;
    }

    private bool montaInsertNovosUsuarios(ListaDeUsuarios UsuariosSelecionados, ListaDeUsuarios UsuariosPreExistentes, StringBuilder comandoSQL)
    {
        bool bRet = false;
        foreach (int UsuarioSelecionado in UsuariosSelecionados.ListaDeCodigos)
        {
            // se o Usuario selecionado não constar nos Usuarios pré-existentes
            // compõe comando que irá incluí-lo no compartilhamento
            if (false == UsuariosPreExistentes.ContemCodigo(UsuarioSelecionado))
            {
                bRet = true;
                comandoSQL.Append(string.Format(@"
IF EXISTS( SELECT 1 FROM {0}.{1}.[DetalhesTGPWfPessoasEspecificas] WHERE [CodigoPerfilWf] = @CodigoPerfilWf AND [IdentificadorRecurso] = '{2}' )
    UPDATE {0}.{1}.[DetalhesTGPWfPessoasEspecificas] SET [StatusRegistro] = 'A', [DataAtivacaoRegistro] = GETDATE(), [IdentificadorUsuarioAtivacao] = '{3}' 
        WHERE [CodigoPerfilWf] = @CodigoPerfilWf AND [IdentificadorRecurso] = '{2}'
ELSE
    INSERT INTO {0}.{1}.[DetalhesTGPWfPessoasEspecificas] 
        ([IdentificadorRecurso], [StatusRegistro], [DataInclusaoRegistro], [IdentificadorUsuarioInclusao], [CodigoPerfilWf])
        VALUES ('{2}', 'A', GETDATE(), '{3}', @CodigoPerfilWf);

                    ", cDados.getDbName(), cDados.getDbOwner(), UsuarioSelecionado, idUsuarioLogado));

            } // if (false == UsuariosSelecionados.ContemCodigo(UsuarioPreExistente))
        } // foreach (int UsuarioSelecionado in UsuariosSelecionados.ListaDeCodigos)
        return bRet;
    }

    private void excluiRegistro(int codigoFluxo)
    {
        int registrosAfetados = 0;

        string comandoSQL = string.Format(@"
DECLARE @CodigoPerfilWf Int
SET @CodigoPerfilWf = {2}

UPDATE {0}.{1}.[DetalhesTGPWfPessoasEspecificas] SET [StatusRegistro] = 'D', [DataDesativacaoRegistro] = GETDATE(), [IdentificadorUsuarioDesativacao] = '{3}'
WHERE [CodigoPerfilWf] = @CodigoPerfilWf;

UPDATE {0}.{1}.PerfisWf SET [StatusPerfilWf] = 'D', 
[DataDesativacaoPerfilWf] = GETDATE(), 
[IdentificadorUsuarioDesativacao] = '{3}',
[NomePerfilWf] = [NomePerfilWf] + ' [{4}]'
WHERE CodigoPerfilWf = @CodigoPerfilWf
            ", dbName, dbOwner, codigoFluxo, idUsuarioLogado, Resources.traducao.adm_CadastroDePerfis_exclu_do);

        cDados.execSQL(comandoSQL, ref registrosAfetados);
        carregaGrid();
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadPerfAcsFlx");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickNovoPerfil()", true, true, false, "CadPerfAcsFlx", lblTituloTela.Text, this);
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

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            if (string.IsNullOrEmpty(mensagemErro_Persistencia))
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Registro incluído com sucesso!";
            }
        }
        else if (e.Parameter == "Editar")
        {
            string tipoOperacao = e.Parameter;


            string where = @"
            AND NomePerfilWf = '" + txtNomePerfil.Text.Replace("'", "''") + @"'
            AND CodigoEntidade = " + CodigoEntidade;

            int perfilNovo = -1;
            perfilNovo = int.Parse(getChavePrimaria());
            if (verificaExistenciaPerfil(perfilNovo, where))
            {
                mensagemErro_Persistencia = Resources.traducao.adm_CadastroDePerfis_perfil_j__existente__n_o___poss_vel_gravar_os_dados_usando_este_nome_de_perfil_;
            }
            else
            {
                mensagemErro_Persistencia = persisteEdicaoRegistro();
                if (string.IsNullOrEmpty(mensagemErro_Persistencia))
                {
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Registro atualizado com sucesso!";
                }
            }
            ((ASPxCallback)source).JSProperties["cpCodigoSelecionado"] = perfilNovo;
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            if (string.IsNullOrEmpty(mensagemErro_Persistencia))
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Registro excluído com sucesso!";
            }
        }
        ((ASPxCallback)source).JSProperties["cpErro"] = mensagemErro_Persistencia;
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        int valor = 0;
        bool retornoOk = int.TryParse(e.Parameters, out valor);
        carregaGrid(valor);
    }
}