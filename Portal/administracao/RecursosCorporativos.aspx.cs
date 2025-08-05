/*
 OBSERVAÇÕES
 * 
 * MUDANÇA  
 * 13/12/2010 - Alejandro: Não pode tener o mesmo código de ussuario cuando cadastra um recurso novo.
 * 31/03/200 :: Alejandro : - Control de acesso utilizando as permissões da tela.
 *                          - Control de registros utilizando as permissões individuais.
 *                          - Control de usuarios, regra de negocio: não admite dois recursos iguais. 
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
using System.Drawing;
using System.IO;
using DevExpress.XtraPrinting;
using System.Diagnostics;

public partial class administracao_RecursosCorporativos : System.Web.UI.Page
{
    dados cDados;
    public bool podeIncluir = true;
    public bool podeEditar = true;
    public bool podeExcluir = true;
    public bool podeEditarCalendario = true;

    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private int alturaPrincipal = 0;
    private static DataSet dsGrid;
    public bool exportaOLAPTodosFormatos;
    public string tooltipBotaoIncluir = "";
    //-----------------------------------------------------------

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

        this.TH(this.TS("RecursosCorporativos", "barraNavegacao"));

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
        if (!IsPostBack)
        {
            populaGrid();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        cDados.aplicaEstiloVisual(Page);

        //if (!IsPostBack)
        //{            
        bool existeGrupoRecursosEntidade = false;
        bool bPodeAcessarTela = false;

        //Verifica se existe algum grupo de recursos coorporativos cadastrado para entidade
        existeGrupoRecursosEntidade = cDados.getGruposRecursos(codigoEntidadeLogada, "").Tables[0].Rows.Count > 0;
        //Verificar si pode incluir Recursos em alguma Unidade.
        bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioLogado, codigoEntidadeLogada, "UN", "UN_IncRec");

        podeIncluir = bPodeAcessarTela && existeGrupoRecursosEntidade;
        
        tooltipBotaoIncluir = "Você não possui permissão ou grupo de recurso cadastrado para incluir um novo registro nesta tela";
        
        podeEditarCalendario = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, codigoEntidadeLogada, "null", "EN", 0, "null", "EN_EdtClc");

        //Pode acontecer qeu possa consultar Recursos em alguma Unidade.
        if (bPodeAcessarTela == false)
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioLogado, codigoEntidadeLogada, "UN", "RC_Cns");

        if (bPodeAcessarTela == false)
            cDados.RedirecionaParaTelaSemAcesso(this);

        hfGeral.Set("hfWheregetLov_NomeValor", "");

        cDados.setaTamanhoMaximoMemo(memoAnotacoes, 4000, lblContadorMemo);

        if (!existeGrupoRecursosEntidade)
        {
            gvDados.Styles.GroupPanel.ForeColor = Color.Red;
            gvDados.SettingsText.GroupPanel = "Atenção! Não será possível cadastrar recursos corporativos no sistema. É preciso cadastrar antes os grupos de recursos.";
        }
        //}

        //getResourceGridDados(); //dsGrid = cDados.getRecursosCorporativos(codigoEntidadeLogada.ToString(),"","");
        //getWhereUsuarios();
        carregaResponsavel();
        //populaGrid();
        gvDados.DataSource = dsGrid;
        gvDados.DataBind();
        carregarTipoRecurso("");
        carregarUnidadeNegocio(" ORDER BY un.NomeUnidadeNegocio -- ");
        //btnCalcDisp.ClientVisible = hfGeral.Contains("calcDispVisivel") && hfGeral.Get("calcDispVisivel") == "S";

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Cadastro De Recursos Corporativos", "CADRCO", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }

    }

    //-----------------------------------------------------------

    #region VARIOS



    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/RecursosCorporativos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "RecursosCorporativos"));
    }

    #endregion

    #region COMBOBOX

    private void carregarTipoRecurso(string where)
    {
        DataSet ds = cDados.getTiposRecursos(where);

        if (cDados.DataSetOk(ds))
        {
            ddlTipoRecurso.DataSource = ds.Tables[0];
            ddlTipoRecurso.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!IsPostBack && !IsCallback)
                    ddlTipoRecurso.SelectedIndex = 0;
                if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString().Equals("Incluir") && ddlTipoRecurso.Value == null)
                    ddlTipoRecurso.SelectedIndex = 0;

                if (ddlTipoRecurso.SelectedIndex != -1)
                    carregarGrupoRecurso(ddlTipoRecurso.Value.ToString());
            }
        }
    }

    private void carregaResponsavel()
    {
        //string valor = "";
        //string nome = "";
        string where = string.Format(@"
                    AND un.CodigoUnidadeNegocio = {2}
                    AND us.CodigoUsuario NOT IN (SELECT rc.CodigoUsuario 
												FROM {0}.{1}.RecursoCorporativo AS rc 
													WHERE rc.CodigoEntidade = {2} 
													AND rc.CodigoUsuario IS NOT NULL
                                                 )
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeLogada);
        hfGeral.Set("hfWheregetLov_NomeValor", where);
        //where = getWhereUsuarios();

        ddlResponsavel.Columns.Clear();
        ListBoxColumn lbc1 = new ListBoxColumn("NomeUsuario", "Nome", 200);
        ListBoxColumn lbc2 = new ListBoxColumn("EMail", "Email", 350);
        ListBoxColumn lbc3 = new ListBoxColumn("StatusUsuario", "Status", 80);

        ddlResponsavel.Columns.Insert(0, lbc1);
        ddlResponsavel.Columns.Insert(1, lbc2);
        ddlResponsavel.Columns.Insert(2, lbc3);

        ddlResponsavel.DataSource = cDados.getUsuarioUnidadeNegocioAtivo(where);///getLovNomeValor2("usuario", "CodigoUsuario", "NomeUsuario", "EMail", "", true, where, "NomeUsuario", out valor, out nome);
        ddlResponsavel.DataBind();
    }

    private void carregarGrupoRecurso(string codigoTipoRecurso)
    {
        string where = string.Format(@" AND gr.CodigoTipoRecurso = {0} ", codigoTipoRecurso);
        DataSet ds = cDados.getGruposRecursos(codigoEntidadeLogada, where);

        if (cDados.DataSetOk(ds))
        {
            ddlGrupo.DataSource = ds.Tables[0];
            ddlGrupo.DataBind();
        }
    }

    private void carregarUnidadeNegocio(string where)
    {
        DataSet ds = cDados.getUnidadesNegocioEntidade(codigoEntidadeLogada, where);

        if (cDados.DataSetOk(ds))
        {
            ddlUnidadeNegocio.DataSource = ds.Tables[0];
            ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeNegocio.TextField = "NomeUnidadeNegocio";
            ddlUnidadeNegocio.DataBind();
        }
    }

    #endregion

    #region gvDADOS

    /// <summary>
    /// Carrega a grid, atualizando os dados.
    /// 
    /// Control de acesso:
    /// Adicionar uma columna com a suma do peso das permissões (2:ed; 4:ex; ...) -> 'select'
    /// Limitar a lista so com os registros que tem permissõe para consultar-os. -> 'where'
    /// </summary>
    private void populaGrid()
    {

        string select = string.Format(@"
                    ,   {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, ISNULL(rc.CodigoUnidadeNegocio, rc.CodigoEntidade), NULL, 'UN', 0, NULL, 'RC_Alt')   *   2   +	-- 2	alterar
                        {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, ISNULL(rc.CodigoUnidadeNegocio, rc.CodigoEntidade), NULL, 'UN', 0, NULL, 'RC_Exc')   *   4		-- 4	excluir
                        AS [Permissoes]
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioLogado, codigoEntidadeLogada);
        string where = string.Format(@"
                    AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, ISNULL(rc.CodigoUnidadeNegocio, rc.CodigoEntidade), NULL, 'UN', 0, NULL, 'RC_Cns') = 1
                    ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioLogado, codigoEntidadeLogada);

       dsGrid = cDados.getRecursosCorporativos(codigoEntidadeLogada.ToString(), select, where);

        if (cDados.DataSetOk(dsGrid))
        {
            gvDados.DataSource = dsGrid;
            gvDados.DataBind();
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            string usuarioAtivo = e.GetValue("IndicaRecursoAtivo").ToString();

            if (usuarioAtivo == "N")
            {

                e.Row.ForeColor = Color.FromName("#914800");
                //e.Row.ForeColor = Color.Black;

            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex >= 0)
        {
            //string codigoCalendario = gvDados.GetRowValues(e.VisibleIndex, "CodigoCalendario").ToString(); //linha original
            string codigoCalendario = gvDados.GetRowValues(e.VisibleIndex, "CodigoCalendario") + ""; // linha alterada
            //int permissoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString()); //linha original
            int permissoes = (gvDados.GetRowValues(e.VisibleIndex, "Permissoes") + "" == "") ? 0 : int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes") + ""); //linha alterada
            bool eequipe = gvDados.GetRowValues(e.VisibleIndex, "IndicaEquipe") + "" == "Sim";

            podeEditar = (permissoes & 2) > 0;
            podeExcluir = (permissoes & 4) > 0;

            if (e.ButtonID.Equals("btnEditar"))
            {
                if (podeEditar)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnExcluir"))
            {
                if (podeExcluir)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnCalendario"))
            {
                if (podeEditarCalendario)
                {
                    if (!codigoCalendario.Equals(""))
                        e.Enabled = true;
                    else
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/CalendarioDes.png";
                        e.Text = "Não Existe Calendário para este Recurso";
                    }
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/CalendarioDes.png";
                    e.Text = "";
                }
            }
            else if (e.ButtonID.Equals("btnInteressados"))
            {
                if (eequipe)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
                    e.Text = "Opção disponível apenas para Equipe.";
                    e.Image.ToolTip = e.Text;
                }
            }
        }
    }

    #endregion

    #region SALVAR DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private string getWhereUsuarios()
    {
        string where = string.Format(@"AND CodigoUsuario in(SELECT us.CodigoUsuario FROM {0}.{1}.Usuario AS us INNER JOIN {0}.{1}.UsuarioUnidadeNegocio AS uun ON (uun.CodigoUsuario = us.CodigoUsuario) WHERE Uun.CodigoUnidadeNegocio = {2} AND us.DataExclusao IS NULL AND us.CodigoUsuario NOT IN(SELECT rc.CodigoUsuario FROM {0}.{1}.RecursoCorporativo AS rc WHERE rc.CodigoEntidade = {2} AND rc.CodigoUsuario IS NOT NULL))"
            , cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeLogada);

        //Para inserir o recurso, se deve verificar que não exista el usuario relacionado ao recurso ja existente.
        where += " AND CodigoUsuario NOT IN(-1";

        if (cDados.DataSetOk(dsGrid) && cDados.DataTableOk(dsGrid.Tables[0]))
        {
            foreach (DataRow dr in dsGrid.Tables[0].Rows)
            {
                if (dr["CodigoUsuario"].ToString() != "")
                    where += "," + dr["CodigoUsuario"].ToString();
            }
        }

        where += ") ";

        return where; //hfGeral.Set("hfWheregetLov_NomeValor", where);
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string codigoResponsavel = "";
        string codigoNovoRecurso = "";
        string codigoErro = "";
        string mensagemErro = "";
        bool repuestaBanco = false;
        string mensagemRetorno = "";

        //if (ddlTipoRecurso.Text == "Pessoa" && ddlResponsavel.SelectedIndex != -1)
        if (ddlTipoRecurso.Value.ToString() == "1" && ddlResponsavel.SelectedIndex != -1)
                codigoResponsavel = ddlResponsavel.Value.ToString();
        else
            codigoResponsavel = "";


        string IndicaGenerico = chkGenerico.Checked ? "S" : "N";
        string IndicaEquipe = chkEquipe.Checked ? "S" : "N";
        string nomeResponsavel = "";

        //if (ddlTipoRecurso.Text == "Pessoa")
        if (ddlTipoRecurso.Value.ToString() == "1")
                nomeResponsavel = (IndicaGenerico != "S" && IndicaEquipe != "S") ? ddlResponsavel.Text.Replace("'", "''") : txtResp.Text.Replace("'", "''");
        else
            nomeResponsavel = txtResp.Text.Replace("'", "''");

        int tipoRecurso = int.Parse(ddlTipoRecurso.Value.ToString());
        string recursoAtivo = chkAtivo.Checked ? "S" : "N";
        int codigoGrupo = int.Parse(ddlGrupo.Value.ToString());
        string codigoUnidadeNegocio = ddlUnidadeNegocio.Text == "" ? "NULL" : ddlUnidadeNegocio.Value.ToString();
        string valorHora = string.IsNullOrEmpty(txtValorHora.Text) ? "NULL" : txtValorHora.Text.Replace(".", "").Replace(",", ".");
        string valorUso = string.IsNullOrEmpty(txtValorUso.Text) ? "NULL" : txtValorUso.Text.Replace(".", "").Replace(",", ".");
        string disponibilidaInicio = "NULL";
        string disponibilidadeTermino = "NULL";
        //if (ddlDisponibilidadeInicio.Date != null && ddlDisponibilidadeInicio.Date.Year >= DateTime.Now.Year)
        //if (ddlDisponibilidadeTermino.Date != null && ddlDisponibilidadeTermino.Date.Year >= DateTime.Now.Year)
        disponibilidaInicio = ddlDisponibilidadeInicio.Text;
        disponibilidadeTermino = ddlDisponibilidadeTermino.Text;

        disponibilidaInicio = disponibilidaInicio == "" ? "NULL" : disponibilidaInicio;
        disponibilidadeTermino = disponibilidadeTermino == "" ? "NULL" : disponibilidadeTermino;

        string anotacoes = (memoAnotacoes.Text != "" ? "'" + memoAnotacoes.Text.Replace("'", "''") + "'" : "NULL");
        string unidadeMedida = !string.IsNullOrWhiteSpace(txtUnidadeMedida.Text) ? string.Format("'{0}'", txtUnidadeMedida.Text.Replace("'", "''")) : "NULL";

        repuestaBanco = cDados.VerificaExisteRecursoCorporativo(tipoRecurso, recursoAtivo, codigoResponsavel, codigoGrupo
                                                        , codigoUnidadeNegocio, valorHora, valorUso, disponibilidaInicio
                                                        , disponibilidadeTermino, anotacoes, codigoUsuarioLogado.ToString()
                                                        , unidadeMedida, nomeResponsavel, codigoEntidadeLogada, ref mensagemRetorno);

        if (!repuestaBanco)
        {
            repuestaBanco = cDados.incluirRecursoCorporativos(tipoRecurso, recursoAtivo, codigoResponsavel, codigoGrupo
                                                            , codigoUnidadeNegocio, valorHora, valorUso, disponibilidaInicio
                                                            , disponibilidadeTermino, anotacoes, codigoUsuarioLogado.ToString()
                                                            , unidadeMedida, nomeResponsavel, codigoEntidadeLogada, IndicaGenerico, IndicaEquipe, 
                                                              ref codigoNovoRecurso, ref codigoErro, ref mensagemErro);
            if (repuestaBanco)
            {
                int codigoNovoCalendario = 0;

                if (tipoRecurso != 3) //aca to verificando que seja diferente ao "FINANCEIRO" [CodigoTipoRecurso]
                    cDados.incluiCopiaCalendario(null, codigoEntidadeLogada, codigoUsuarioLogado, "RC", int.Parse(codigoNovoRecurso), nomeResponsavel, ref codigoNovoCalendario);

                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoNovoRecurso);
                gvDados.ClientVisible = false;
                mensagemRetorno = "";
            }
            else
            {
                mensagemRetorno += Resources.traducao.RecursosCorporativos_o_recurso_n_o_p_de_ser_inclu_do__ + mensagemErro;
            }
        }

        return mensagemRetorno;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        string chave = getChavePrimaria();
        if (!chkAtivo.Checked)
        {
            string msgExclusao = cDados.verificaExclusaoRecursoCorporativo(chave, "I"); //inativar

            if (msgExclusao.Trim() != "")
                return msgExclusao;
        }

        bool repuestaBanco = false;
        // busca a chave primaria
        string tipoRecurso = ddlTipoRecurso.Value.ToString();
        string recursoAtivo = chkAtivo.Checked ? "S" : "N";
        string codigoResponsavel = "";
        //if (ddlTipoRecurso.Text == "Pessoa" && ddlResponsavel.SelectedIndex != -1)
        if (ddlTipoRecurso.Value.ToString() == "1" && ddlResponsavel.SelectedIndex != -1)
                codigoResponsavel = ddlResponsavel.Value.ToString();
        else
            codigoResponsavel = "";

        string IndicaGenerico = chkGenerico.Checked ? "S" : "N";
        string IndicaEquipe = chkEquipe.Checked ? "S" : "N";
        string nomeResponsavel = "";

        //if (ddlTipoRecurso.Text == "Pessoa")
        if (ddlTipoRecurso.Value.ToString() == "1")
                nomeResponsavel = (IndicaGenerico != "S" && IndicaEquipe != "S") ? ddlResponsavel.Text.Replace("'", "''") : txtResp.Text.Replace("'", "''");
        else
            nomeResponsavel = txtResp.Text.Replace("'", "''");

        //string nomeResponsavel = ddlTipoRecurso.Text == "Pessoa" ? ddlResponsavel.Text.Replace("'", "''") : txtResp.Text.Replace("'", "''");
        string codigoGrupo = hfCodigoGrupo.Get("hfCodigoGrupo").ToString();
        string codigoUnidadeNegocio = ddlUnidadeNegocio.Text == "" ? "NULL" : ddlUnidadeNegocio.Value.ToString();
        string valorHora = string.IsNullOrEmpty(txtValorHora.Text) ? "NULL" : txtValorHora.Text.Replace(".", "").Replace(",", ".");
        string valorUso = string.IsNullOrEmpty(txtValorUso.Text) ? "NULL" : txtValorUso.Text.Replace(".", "").Replace(",", ".");

        string disponibilidaInicio = "NULL";
        if (ddlDisponibilidadeInicio.Value != null)
        {
            disponibilidaInicio = ddlDisponibilidadeInicio.Text;
        }            

        string disponibilidadeTermino = "NULL";
        if (ddlDisponibilidadeTermino.Value != null)
        {
            disponibilidadeTermino = ddlDisponibilidadeTermino.Text;
        }
            
        string anotacoes = (memoAnotacoes.Text != "" ? "'" + memoAnotacoes.Text.Replace("'", "''") + "'" : "NULL");
        string unidadeMedida = !string.IsNullOrWhiteSpace(txtUnidadeMedida.Text) ?
            string.Format("'{0}'", txtUnidadeMedida.Text.Replace("'", "''")) : "NULL";

        repuestaBanco = cDados.atualizaRecursoCorporativos(tipoRecurso, recursoAtivo, codigoResponsavel
                                                        , codigoGrupo, codigoUnidadeNegocio, valorHora
                                                        , valorUso, disponibilidaInicio, disponibilidadeTermino
                                                        , anotacoes, codigoUsuarioLogado.ToString(), nomeResponsavel
                                                        , unidadeMedida, codigoEntidadeLogada.ToString(), chave, IndicaGenerico, IndicaEquipe);
        if (repuestaBanco)
        {
            populaGrid();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
            gvDados.ClientVisible = false;
            return "";
        }
        else
        {
            return Resources.traducao.RecursosCorporativos_o_recurso_n_o_p_de_ser_alterado__consulte_o_administrador_;
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();

        string msgExclusao = cDados.verificaExclusaoRecursoCorporativo(chave,"E");//excluir

        if (msgExclusao.Trim() != "")
            return msgExclusao;

        if (cDados.excluiRecursoCorporativos(chave))
        {
            populaGrid();
            return "";
        }
        else
        {
            populaGrid();
            return Resources.traducao.RecursosCorporativos_o_recurso_n_o_pode_ser_exclu_do__pois_existem_informa__es_hist_ricas_para_ele_ + Environment.NewLine + Resources.traducao.RecursosCorporativos__neste_caso__o_recurso_poder__ser_colocado_como_inativo_;
        }
    }

    #endregion

    #region CALLBACK'S

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        pnCallback.JSProperties["cp_Pesquisa"] = "0";
        pnCallback.JSProperties["cp_lovCodigoResponsavel"] = "-1";

        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        //else if (e.Parameter == "PesquisarResp")
        //{
        //    //so coloca na sessão se realmente precisar abrir o popup.
        //    Session["Pesquisa"] = ddlResponsavel.Text;

        //    pnCallback.JSProperties["cp_Pesquisa"] = "1";
        //    hfGeral.Set("StatusSalvar", "-1"); // -1 indicar que o retorno não está relacionado com persistência
        //    hfGeral.Set("lovMostrarPopPup", "0"); // 0 indica que não precisa abrir popup de pesquisa
        //    pnCallback.JSProperties["cp_lovMostrarPopPup"] = "0";

        //    string nomeUsuario = "";
        //    string codigoUsuario = "";

        //    cDados.getLov_NomeValor("usuario", "CodigoUsuario", "NomeUsuario", ddlResponsavel.Text, true, hfGeral.Get("hfWheregetLov_NomeValor").ToString(), "NomeUsuario", out codigoUsuario, out nomeUsuario);

        //    // se encontrou um único registro
        //    if (nomeUsuario != "")
        //    {
        //        ddlResponsavel.Text = nomeUsuario;
        //        hfGeral.Set("lovCodigoResponsavel", codigoUsuario);
        //        //se ja encontrou, então esvazia a session para que ao se abrir o popup denovo 
        //        //o campo ja nao venha preeenchido

        //        pnCallback.JSProperties["cp_lovCodigoResponsavel"] = codigoUsuario;
        //    }
        //    else // mostrar popup
        //    {
        //        hfGeral.Set("lovMostrarPopPup", "1"); // 1 indica que precisa abrir popup de pesquisa
        //        hfGeral.Set("lovCodigoResponsavel", "");

        //        pnCallback.JSProperties["cp_lovCodigoResponsavel"] = "";
        //        pnCallback.JSProperties["cp_lovMostrarPopPup"] = "1";
        //    }
        //    lblCantCarater.ClientVisible = true;
        //    lblDe250.ClientVisible = true;
        //}


        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
        {
            if (mensagemErro_Persistencia.IndexOf("Erro Interno") > 0)
                throw new Microsoft.VisualBasic.CompilerServices.InternalErrorException();
            else
                hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }

        //getResourceGridDados(); //dsGrid = cDados.getRecursosCorporativos(codigoEntidadeLogada.ToString(),"","");

        //if(e.Parameter != "PesquisarResp")
        populaGrid();

        carregaResponsavel(); // getWhereUsuarios();
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        Session["Pesquisa"] = "";
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
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadRecCorp");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abreNovoReg();", true, true, false, "CadRecCorp", "Cadastro de Recursos Corporativos", this, tooltipBotaoIncluir);
    }

    #endregion
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ErrorText = e.Exception.Message;
        }
    }

    protected void pn_ddlGrupo_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
        string[] parametros = parametro.Split('|');
        

        carregarGrupoRecurso(parametros[0]);
        ((ASPxCallbackPanel)sender).JSProperties["cp_Grupo"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cp_Habilita"] = "";


        if (ddlGrupo.Items.Count > 0)
        {
            bool bFound = false;
            if (hfCodigoGrupo.Contains("hfCodigoGrupo"))
            {
                int codigoGrupoRecurso;
                if (int.TryParse(hfCodigoGrupo.Get("hfCodigoGrupo").ToString(), out codigoGrupoRecurso))
                {
                    if (ddlGrupo.Items.FindByValue(codigoGrupoRecurso) != null)
                    {
                        ddlGrupo.Value = codigoGrupoRecurso;
                        bFound = true;
                    }
                }
            }

            if (!bFound)
            {
                ddlGrupo.SelectedIndex = 0;
            }
            ((ASPxCallbackPanel)sender).JSProperties["cp_Grupo"] = ddlGrupo.Value.ToString();
            ((ASPxCallbackPanel)sender).JSProperties["cp_Habilita"] = parametros[1] == "Consultar" ? "false" : "true";
            ddlGrupo.ClientEnabled = (parametros[1] != "Consultar");
        }
    }
}
