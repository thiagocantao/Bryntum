/*OBSERVAÇÕES
 * 
 * Data Criação: 05/04/2011
 * Author      : Alejandro
 * 
 * Arquivo auxiliar : ~/scripts/ListagemPerfisUsuario.js
 * 
 * Telas qeu recibe via Request os siguentes parâmetros para preparar a tela:
 * 
 * "CU" : codigoU   :: Código de usuário alvo da permissão.
 * "NU" : nomeU     :: Descrição do usuário alvo da permissão.
 * 
 * MUDIFICAÇÕES
 * 
 * 
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
using System.Drawing;
using DevExpress.Web;

public partial class administracao_ListagemPerfisUsuario : System.Web.UI.Page
{
    /// <summary>
    /// Clases com conteudo de 'Codigo de Usuario', e 'Codigo Entidade'.
    /// </summary>
    class UsuarioLogado
    {
        int IdUsuarioLogado;
        public int IDusuario
        {
            set { IdUsuarioLogado = value; }
            get { return IdUsuarioLogado; }
        }

        int IdEntidadeLogada;
        public int IDEntidade
        {
            set { IdEntidadeLogada = value; }
            get { return IdEntidadeLogada; }
        }

        public UsuarioLogado()
        {
            IdUsuarioLogado = -1;
            IdEntidadeLogada = -1;
        }
    }
    UsuarioLogado usuarioLogado = new UsuarioLogado();

    class ChaveCompostaObjeto
    {
        //CodigoObjetoAssociado;CodigoTipoAssociacao;CodigoUsuario
        int idObjetoAssociado;
        public int IDObjeto
        {
            set { idObjetoAssociado = value; }
            get { return idObjetoAssociado; }
        }

        string tipoAssociacao;
        public string TipoAssociacao
        {
            set { tipoAssociacao = value; }
            get { return tipoAssociacao; }
        }

        int idAssociacao;
        public int IDAssociacao
        {
            set { idAssociacao = value; }
            get { return idAssociacao; }
        }

        int idUsuario;
        public int IDUsuario
        {
            set { idUsuario = value; }
            get { return idUsuario; }
        }

        public ChaveCompostaObjeto()
        {
            idObjetoAssociado = -1;
            tipoAssociacao = "";
            idAssociacao = -1;
            idUsuario = -1;
        }
    }
    ChaveCompostaObjeto chaveCompostaObjeto = new ChaveCompostaObjeto();

    dados cDados;
    private string dbName;
    private string dbOwner;
    private string resolucaoCliente = "";
    private string definicaoUnidade = "Unidade";
    private string definicaoUnidadePlural = "Unidades";
    private string definicaoEntidade = "Entidade";
    private string definicaoEntidadePlural = "Entidades";
    private string nomeU = "";
    private int codigoU = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = new dados(listaParametrosDados);
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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        usuarioLogado.IDusuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        usuarioLogado.IDEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet ds = cDados.getDefinicaoUnidade(usuarioLogado.IDEntidade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoUnidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            definicaoUnidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
        }

        ds = cDados.getDefinicaoEntidade(usuarioLogado.IDEntidade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoEntidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            definicaoEntidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
        }

        nbAssociacao.AutoCollapse = true; // necssário para que o javascript funcione (GetActiveGroup);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        if (Request.QueryString["CU"] != null && !Request.QueryString["CU"].ToString().Equals("")) codigoU = int.Parse(Request.QueryString["CU"].ToString());
        //if (Request.QueryString["NU"] != null && !Request.QueryString["NU"].ToString().Equals("")) nomeU = Request.QueryString["NU"].ToString();
        DataSet dstempNomeUsuario = cDados.getUsuarios(string.Format(" and u.[CodigoUsuario] = {0} ", codigoU));

        if (cDados.DataSetOk(dstempNomeUsuario) && cDados.DataTableOk(dstempNomeUsuario.Tables[0]))
        {
            nomeU = dstempNomeUsuario.Tables[0].Rows[0]["Nomeusuario"].ToString();
        }

        if (!IsPostBack)
        {
            cerrarSessionCheck();

            hfGeral.Set("HerdaPermissoes", "S");
            hfGeral.Set("iniciaisTO", "");
            hfGeral.Set("CodigoObjetoAssociado", "-1");
            hfGeral.Set("CodigoObjetoPai", "0");
            hfGeral.Set("CodigoUsuarioInteressado", codigoU);
            hfGeral.Set("NomeUsuarioInteressado", nomeU);
            hfGeral.Set("itemMenu", "");

            ddlInteressado.Text = nomeU;
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);

        }
        carregaGvDados();
        string nomeSession = "dt" + hfGeral.Get("itemMenu").ToString();
        if (Session[nomeSession] != null)
        {
            DataTable dt = (DataTable)Session[nomeSession]; //(DataTable)Session["dtPermissoes"];

            if (null != dt)
            {
                gvPermissoes.DataSource = dt;
                gvPermissoes.DataBind();
            }

            string siglaAssociacao = hfGeral.Get("itemMenu").ToString();

            //Prencho o Title da grid gvPermissoes.
            getTituloGridPermissoes(siglaAssociacao);
        }

        gvPermissoes.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {

        int alturaGrid = 420;

        if (Request.QueryString["AlturaGrid"] != null && Request.QueryString["AlturaGrid"].ToString() != "")
            alturaGrid = int.Parse(Request.QueryString["AlturaGrid"].ToString());

        //gvDados.Settings.VerticalScrollableHeight = alturaGrid + 20;
        //gvPermissoes.Settings.VerticalScrollableHeight = alturaGrid - 130;

        if (Request.QueryString["LarguraGrid"] != null && Request.QueryString["LarguraGrid"].ToString() != "")
            gvDados.Width = int.Parse(Request.QueryString["LarguraGrid"].ToString());
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ListagemPerfisUsuario.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
    }

    private string getIniciaisTipoAssociacao(string textoAssociacao)
    {
        string retorno = "";

        /* EN UN ST PR ME PP TM OB IN CT */
        if (textoAssociacao == "mnEntidade") retorno = "EN";
        else if (textoAssociacao == "mnUnidade") retorno = "UN";
        else if (textoAssociacao == "mnEstrategia") retorno = "ST";
        else if (textoAssociacao == "mnProjeto") retorno = "PR";
        else if (textoAssociacao == "mnMapa") retorno = "ME";
        else if (textoAssociacao == "mnPerspectiva") retorno = "PP";
        else if (textoAssociacao == "mnTema") retorno = "TM";
        else if (textoAssociacao == "mnObjetivo") retorno = "OB";
        else if (textoAssociacao == "mnIndicador") retorno = "IN";
        else if (textoAssociacao == "mnContrato") retorno = "CT";
        else if (textoAssociacao == "mnDemandaComplexa") retorno = "DC";
        else if (textoAssociacao == "mnDemandaSimple") retorno = "DS";
        else if (textoAssociacao == "mnProcesso") retorno = "PC";
        else if (textoAssociacao == "mnEquipe") retorno = "EQ";

        return retorno;
    }

    private string getDescricaoAssociacaoFromIniciais(string iniciaisTo)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        string retorno = "";
        if (iniciaisTo.Equals("")) return retorno;
        else if (iniciaisTo.Equals("EN")) retorno = definicaoEntidade;
        else if (iniciaisTo.Equals("UN")) retorno = definicaoUnidade;
        else if (iniciaisTo.Equals("ST")) retorno = "Estratégia";
        else if (iniciaisTo.Equals("PR")) retorno = "Projeto";
        else if (iniciaisTo.Equals("ME")) retorno = "Mapa Estratégico";
        else if (iniciaisTo.Equals("PP")) retorno = "Perspectiva";
        else if (iniciaisTo.Equals("TM")) retorno = "Tema";
        else if (iniciaisTo.Equals("OB")) retorno = "Objetivo Estratégico";
        else if (iniciaisTo.Equals("IN")) retorno = "Indicador";
        else if (iniciaisTo.Equals("CT")) retorno = "Contrato";
        else if (iniciaisTo.Equals("DC")) retorno = "Demanda Complexa";
        else if (iniciaisTo.Equals("DS")) retorno = "Demanda Simple";
        else if (iniciaisTo.Equals("PC")) retorno = "Processo";

        return retorno;
    }

    private void carregaMenuLateral(string iniciaisTO)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        string strComando = string.Format(@"
            SELECT 
                  [CodigoTipoAssociacao]
                , [DescricaoTipoAssociacao]
                , [IniciaisTipoAssociacao] 
            FROM 
                {0}.{1}.f_GetTiposAssociacoesDescendentes('{2}', 'PERMISSAO') 
            ", dbName, dbOwner, iniciaisTO);
        DataSet ds = cDados.getDataSet(strComando);
        if (cDados.DataSetOk(ds))
        {
            string menuId, DescricaoTipoObjeto, iniciaisTipoObjeto;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DescricaoTipoObjeto = dr["DescricaoTipoAssociacao"].ToString();
                iniciaisTipoObjeto = dr["IniciaisTipoAssociacao"].ToString();

                menuId = getDescricaoIdMenu(iniciaisTipoObjeto);

                if (menuId != "")
                    nbAssociacao.Groups.FindByName("gpAssociacao").Items.Add(new NavBarItem(DescricaoTipoObjeto, menuId));
            }

            if (nbAssociacao.Items.Count > 0)
            {
                hfGeral.Set("itemMenu", getIniciaisTipoAssociacao(nbAssociacao.Items[0].Name));
                nbAssociacao.Items[0].Selected = true;
            }
        }
    }

    private static string getDescricaoIdMenu(string iniciaisTipoObjeto)
    {
        string menuId;

        if (iniciaisTipoObjeto.Equals("EN")) menuId = "mnEntidade";
        else if (iniciaisTipoObjeto.Equals("UN")) menuId = "mnUnidades";
        else if (iniciaisTipoObjeto.Equals("ST")) menuId = "mnEstrategia";
        else if (iniciaisTipoObjeto.Equals("ME")) menuId = "mnMapas";
        else if (iniciaisTipoObjeto.Equals("PR")) menuId = "mnProjeto";
        else if (iniciaisTipoObjeto.Equals("PP")) menuId = "mnPerspectiva";
        else if (iniciaisTipoObjeto.Equals("TM")) menuId = "mnTema";
        else if (iniciaisTipoObjeto.Equals("OB")) menuId = "mnObjetivo";
        else if (iniciaisTipoObjeto.Equals("IN")) menuId = "mnIndicador";
        else if (iniciaisTipoObjeto.Equals("CT")) menuId = "mnContrato";
        else if (iniciaisTipoObjeto.Equals("DC")) menuId = "mnDemandaComplexa";
        else if (iniciaisTipoObjeto.Equals("DS")) menuId = "mnDemandaSimple";
        else if (iniciaisTipoObjeto.Equals("PC")) menuId = "mnProcesso";
        else if (iniciaisTipoObjeto.Equals("EQ")) menuId = "mnEquipe";
        else menuId = "";

        return menuId;
    }

    #endregion

    #region SESSIONES

    private void cerrarSessionCheck()
    {
        /* EN UN ST PR ME PP TM OB IN CT */

        if (Session["dtEN"] != null) Session.Remove("dtEN");
        if (Session["dtUN"] != null) Session.Remove("dtUN");
        if (Session["dtST"] != null) Session.Remove("dtST");
        if (Session["dtPR"] != null) Session.Remove("dtPR");
        if (Session["dtME"] != null) Session.Remove("dtME");
        if (Session["dtPP"] != null) Session.Remove("dtPP");
        if (Session["dtTM"] != null) Session.Remove("dtTM");
        if (Session["dtOB"] != null) Session.Remove("dtOB");
        if (Session["dtIN"] != null) Session.Remove("dtIN");
        if (Session["dtCT"] != null) Session.Remove("dtCT");
        if (Session["dtDC"] != null) Session.Remove("dtDC");
        if (Session["dtDS"] != null) Session.Remove("dtDS");
        if (Session["dtPC"] != null) Session.Remove("dtPC");
        if (Session["dtEQ"] != null) Session.Remove("dtEQ");
    }

    private string getSQLPermissao(int codigoOE, string iniciaisTO, int codigoUsuarioPermissao
                            , int idUsuarioLogado, int codigoObjetoPai, int idEntidadeLogada
                            , string listaPerfis, string iniciaisTipoAssociacao)
    {
        string sqlPermissao = "";

        sqlPermissao = string.Format(@"
            DECLARE @CodigoTipoAssociacao INT,
                    @SiglaTipoAssociacao VARCHAR(2)

            SET @SiglaTipoAssociacao = '{9}' --'EN'

            SELECT  *
                ,	CASE    WHEN [Personalizada] = 1 THEN    'Editado.png'
                            WHEN [Herdada]       = 1 THEN    'Perfil_Herdada.png' 
                            ELSE ''
                    END     AS [imagemIcono]

            FROM    {0}.{1}.f_GetPermissoesUsuario ( {2} , '{3}', {6}, {7}, {4}, @SiglaTipoAssociacao, {5}, {8})
            ", dbName, dbOwner, codigoOE, iniciaisTO
             , codigoUsuarioPermissao, idUsuarioLogado
             , codigoObjetoPai, idEntidadeLogada, listaPerfis
             , iniciaisTipoAssociacao);

        return sqlPermissao;
    }

    #endregion

    #region CALLBACK'S

    /// <summary>
    /// Editar          : Seleccionar '[EDITAR]', apos click no botão '[Concluir]'
    /// EditarPermissao : Seleccionar '[EDITAR]', apos 'Personaliçar Perfil', click no botão '[Salvar]'
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Editar") mensagemErro_Persistencia = persisteEdicaoInteressadoObjeto();
        else if (e.Parameter == "EditarPermissao") mensagemErro_Persistencia = persisteEdicaoPermissao();
        else if (e.Parameter == "Excluir") mensagemErro_Persistencia = persisteExclusaoRegistro();
        //else if (e.Parameter == "IncluirPermissao") mensagemErro_Persistencia = persisteInclusaoPermissao();

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    protected void pnCallbackPermissoes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        int CodigoUsuarioInteressado = int.Parse(hfGeral.Get("CodigoUsuarioInteressado").ToString());
        int idObjetoAssociado = int.Parse(hfGeral.Get("CodigoObjetoAssociado").ToString());
        int idObjetoPai = int.Parse(hfGeral.Get("CodigoObjetoPai").ToString());
        string iniciaisTipoAssociacao = hfGeral.Get("iniciaisTO").ToString();
        carregaMenuLateral(iniciaisTipoAssociacao);
    }

    protected void ddlPapelNoProjeto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
    }

    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter.Equals("CerrarSession"))
            cerrarSessionCheck();
    }

    protected void callbackConceder_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string nomeSession = "dt" + hfGeral.Get("itemMenu").ToString();
        DataTable dt = (DataTable)Session[nomeSession]; //(DataTable)Session["dtPermissoes"];

        string codigoPermissao = e.Parameter.ToString().Split(';')[0];
        string permissaoConceder = e.Parameter.ToString().Split(';')[1];
        string check = e.Parameter.ToString().Split(';')[2];

        if (null != dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CodigoPermissao"].ToString() == codigoPermissao)
                {
                    if (check.Equals("C"))
                    {
                        dr["Concedido"] = permissaoConceder;
                        dr["Delegavel"] = false;
                        dr["Negado"] = false;
                    }
                    else if (check.Equals("D"))
                    {
                        dr["Concedido"] = true;
                        dr["Delegavel"] = permissaoConceder;
                        dr["Negado"] = false;
                    }
                    else if (check.Equals("N"))
                    {
                        dr["Concedido"] = false;
                        dr["Delegavel"] = false;
                        dr["Negado"] = permissaoConceder;
                    }
                    else if (check.Equals("I"))
                    {
                        dr["Incondicional"] = permissaoConceder;
                    }

                    if (!(bool)dr["Concedido"] && !(bool)dr["Delegavel"] && !(bool)dr["Negado"])
                        dr["Incondicional"] = false;

                    dt.AcceptChanges();
                    break;
                } // if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
            } // foreach (DataRow dr in dt)

            Session[nomeSession] = dt;
            gvPermissoes.DataSource = dt;
            gvPermissoes.DataBind();
            
        }  // if (null != dt)
    }

    protected void pnCallbackDetalhe_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        // preenche campo da tela de detalhe;

        Object[] valores = (Object[])gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoObjeto", "IniciaisTipoObjeto", "CodigoObjetoPai", "HerdaPermissoesObjetoSuperior");
        int codigoObjeto = int.Parse(valores[0].ToString());
        string tipoObjetoAssociado = valores[1].ToString();
        int codigoObjetoPai = int.Parse(valores[2].ToString());
        carregaListaPerfis(codigoU, codigoObjeto, tipoObjetoAssociado, codigoObjetoPai);
        carregaListaPerfisAssociados(codigoU, codigoObjeto, tipoObjetoAssociado, codigoObjetoPai);
        checkHerdarPermissoes.Checked = (valores[3].ToString() == "S");
    }

    #endregion

    #region GRIDVIEW

    #region gvDados

    private void carregaGvDados()
    {
        DataSet ds = cDados.getListagemPerfisUsuario(usuarioLogado.IDusuario, usuarioLogado.IDEntidade, codigoU);            //.getInteressadosObjetos(iniciaisTA, int.Parse(codigoOE), int.Parse(codigoOP), int.Parse(idEntidadeLogada));

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.SettingsText.Title = string.Format(@"
                <table style='TEXT-ALIGN: left'>
                <tr>
                    <td vAlign='top'><b>Usuario : </b></td>
                    <td><i>{0}</i></td>
                </tr>
                </table>
                ", nomeU);

            gvDados.DataBind();
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "Perfis")
        {
            if (e.CellValue.ToString().Length > 50)
            {
                e.Cell.Text = e.CellValue.ToString().Substring(0, 50) + "...";
            }
        }
    }

    //todo: OBSERVAÇÃO: reveer o controle de habilitar o botão 'Excluir'.
    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "Perfis") != null)
        {
            string perfisAtual = gvDados.GetRowValues(e.VisibleIndex, "Perfis").ToString();
            string podeSerEditado = gvDados.GetRowValues(e.VisibleIndex, "IndicaEdicaoPermitida").ToString();
            string podeSerExcluido = gvDados.GetRowValues(e.VisibleIndex, "IndicaExclusaoPermitida").ToString();

            if (e.ButtonID == "btnExcluirCustom")
            {
                if (perfisAtual.Equals("*") || podeSerEditado.Equals("N") || podeSerExcluido.Equals("N"))
                {
                    e.Enabled = false;
                    e.Text = "Excluir não disponível";
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
                else
                    e.Enabled = true;
            }
            if (e.ButtonID == "btnEditarCustom")
            {
                if (podeSerEditado.Equals("N"))
                {
                    e.Enabled = false;
                    e.Text = "Edição não disponível";
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
                else
                    e.Enabled = true;
            }
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName.Equals("SORT") || e.CallbackName.Equals("APPLYCOLUMNFILTER"))
            carregaGvDados();
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string parametro = e.Parameters;
        string iniciaisTO = hfGeral.Get("iniciaisTO").ToString();
        int idObjetoAssociado = int.Parse(hfGeral.Get("CodigoObjetoAssociado").ToString());
        int idObjetoPai = int.Parse(hfGeral.Get("CodigoObjetoPai").ToString());

        cDados.atualizaAcessoRestringidoDaPermissao(idObjetoAssociado, usuarioLogado.IDEntidade, idObjetoPai, usuarioLogado.IDusuario, iniciaisTO, parametro);
        carregaGvDados();
    }

    #endregion

    #region gvPERMISSOES

    protected void gvPermissoes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString().Equals("ATL"))
        {
            string siglaAssociacao = hfGeral.Get("itemMenu").ToString();

            //Prencho o Title da grid gvPermissoes.
            getTituloGridPermissoes(siglaAssociacao);
        }
        else if (e.Parameters.ToString().Equals("VOLTAR"))
        {
            cerrarSessionCheck();
        }
        else
        {
            populaChecks();
        }
    }

    private void getTituloGridPermissoes(string siglaAssociacao)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        if (siglaAssociacao.Equals("EN")) gvPermissoes.SettingsText.Title = "Permissões - Entidades";
        else if (siglaAssociacao.Equals("UN")) gvPermissoes.SettingsText.Title = "Permissões - Unidades";
        else if (siglaAssociacao.Equals("ST")) gvPermissoes.SettingsText.Title = "Permissões - Estratégias";
        else if (siglaAssociacao.Equals("PR")) gvPermissoes.SettingsText.Title = "Permissões - Projetos";
        else if (siglaAssociacao.Equals("ME")) gvPermissoes.SettingsText.Title = "Permissões - Mapas";
        else if (siglaAssociacao.Equals("PP")) gvPermissoes.SettingsText.Title = "Permissões - Perspectivas";
        else if (siglaAssociacao.Equals("TM")) gvPermissoes.SettingsText.Title = "Permissões - Temas";
        else if (siglaAssociacao.Equals("OB")) gvPermissoes.SettingsText.Title = "Permissões - Objetivos Estratégicos";
        else if (siglaAssociacao.Equals("IN")) gvPermissoes.SettingsText.Title = "Permissões - Indicadores";
        else if (siglaAssociacao.Equals("CT")) gvPermissoes.SettingsText.Title = "Permissões - Contrato";
        else if (siglaAssociacao.Equals("DC")) gvPermissoes.SettingsText.Title = "Permissões - Demandas Complexas";
        else if (siglaAssociacao.Equals("DS")) gvPermissoes.SettingsText.Title = "Permissões - Demandas Simples";
        else if (siglaAssociacao.Equals("PC")) gvPermissoes.SettingsText.Title = "Permissões - Processos";
    }

    protected void gvPermissoes_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        Color color = new Color();

        if (e.RowType == GridViewRowType.Data)
        {
            bool readOnly = (bool)e.GetValue("ReadOnly");

            if (readOnly)
            {
                int red = Int32.Parse("DD", System.Globalization.NumberStyles.HexNumber);
                int green = Int32.Parse("FF", System.Globalization.NumberStyles.HexNumber);
                int blue = Int32.Parse("CC", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(red, green, blue);
                e.Row.BackColor = color;
            }
        }
    }

    protected void gvPermissoes_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        //if (e.VisibleIndex != -1)
        //{
        //    bool readOnly = (bool)e.GetValue("ReadOnly");
        //    bool indicaCheckConcedido = (bool)gvPermissoes.GetRowValues(e.VisibleIndex, "Concedido");
        //    bool indicaCheckDelegavel = (bool)gvPermissoes.GetRowValues(e.VisibleIndex, "Delegavel");
        //    bool indicaCheckNegado = (bool)gvPermissoes.GetRowValues(e.VisibleIndex, "Negado");
        //    bool incondicionalBloqueado = (bool)gvPermissoes.GetRowValues(e.VisibleIndex, "IncondicionalBloqueado");

        //    if (e.Row.Cells.Count > 5)
        //    {
        //        if (hfGeral.Get("TipoOperacao").ToString().Equals("Consultar") || readOnly)
        //        {
        //            e.Row.Cells[3].Enabled = false;
        //            e.Row.Cells[4].Enabled = false;
        //            e.Row.Cells[5].Enabled = false;
        //            e.Row.Cells[6].Enabled = false;
        //        }
        //        else
        //        {
        //            if ((indicaCheckConcedido || indicaCheckDelegavel || indicaCheckNegado) && (incondicionalBloqueado == false))
        //                e.Row.Cells[6].Enabled = true;
        //            else
        //                e.Row.Cells[6].Enabled = false;
        //        }
        //    }
        //}
    }

    public string getCheckBox(string nomeCheck, string coluna, string inicial)
    {
        string retorno = "";
        bool readOnly = (bool)Eval("ReadOnly");
        string desabilitado = hfGeral.Get("TipoOperacao").ToString().Equals("Consultar") || readOnly ? "disabled='disabled'" : "";

        if (nomeCheck == "CheckIncondicional" && !hfGeral.Get("TipoOperacao").ToString().Equals("Consultar"))
        {
            bool indicaCheckConcedido = (bool)Eval("Concedido");
            bool indicaCheckDelegavel = (bool)Eval("Delegavel");
            bool indicaCheckNegado = (bool)Eval("Negado");
            bool incondicionalBloqueado = (bool)Eval("IncondicionalBloqueado");


            if ((indicaCheckConcedido || indicaCheckDelegavel || indicaCheckNegado) && (incondicionalBloqueado == false))
                desabilitado = "";
            else
                desabilitado = "disabled='disabled'";

        }

        retorno = "<input " + desabilitado + " id='" + nomeCheck + "' " + ((Eval(coluna).ToString() == "1" || Eval(coluna).ToString() == "True") ? "checked='CHECKED'" : "") + " onclick='clicaConceder(" + Eval("CodigoPermissao") + ", this.checked, \"" + inicial + "\")' type='checkbox' />";

        return retorno;
    }

    #endregion

    #endregion

    #region CHECK BOX

    private void populaChecks()
    {
        string sqlPermissao;
        string codigoTipoAssociacaoSQL = "";
        string listaPerfis = "";
        string herdaPermissoes = "S";

        string siglaAssociacao = hfGeral.Get("itemMenu").ToString();
        string iniciaisTO = hfGeral.Get("iniciaisTO").ToString();
        int idObjetoAssociado = int.Parse(hfGeral.Get("CodigoObjetoAssociado").ToString());
        int idObjetoPai = int.Parse(hfGeral.Get("CodigoObjetoPai").ToString());
        string codigoUsuarioInteressado = codigoU.ToString();

        //Prencho o Title da grid gvPermissoes.
        getTituloGridPermissoes(siglaAssociacao);

        //Vejo si a sessión existe. Caso que não, cargo o dados do Banco.
        string nomeSession = "dt" + siglaAssociacao;
        if (Session[nomeSession] == null)
        {
            if (hfGeral.Contains("CodigosPerfisSelecionados") && !hfGeral.Get("CodigosPerfisSelecionados").ToString().Equals("-1"))
                listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();
            if (hfGeral.Contains("HerdaPermissoes"))
                herdaPermissoes = hfGeral.Get("HerdaPermissoes").ToString();
            if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString().Equals("Consultar"))
                herdaPermissoes = "NULL";

            codigoTipoAssociacaoSQL = getSetSqlCheck(siglaAssociacao);

            sqlPermissao = string.Format(@"
                                DECLARE @CodigoTipoAssociacao INT,
                                        @SiglaTipoAssociacao VARCHAR(2)

                                {6}
                                
                                SELECT  *
                                    ,	CASE    WHEN [Herdada]       = 1 THEN    'Perfil_Herdada.png' 
                                                WHEN [Personalizada] = 1 THEN    'Editado.png'
                                                ELSE ''
                                        END     AS [imagemIcono]

                                FROM {0}.{1}.f_GetPermissoesUsuario ( {2}, '{3}', {7}, {8}, {4}, @SiglaTipoAssociacao, {5}, {9}, '{10}' )

                                ", dbName, dbOwner, idObjetoAssociado
                             , iniciaisTO, codigoUsuarioInteressado, usuarioLogado.IDusuario
                             , codigoTipoAssociacaoSQL, idObjetoPai, usuarioLogado.IDEntidade
                             , listaPerfis.Equals("") ? "NULL" : "'" + listaPerfis + "'"
                             , herdaPermissoes);

            DataSet ds = cDados.getDataSet(sqlPermissao);

            gvPermissoes.DataSource = ds.Tables[0];
            gvPermissoes.DataBind();

            Session[nomeSession] = ds.Tables[0];
        }
        else
        {
            //Caso que a sessión exista, prencho a grid gvPermissoes con ela.
            DataTable dt = (DataTable)Session[nomeSession];
            gvPermissoes.DataSource = dt;
            gvPermissoes.DataBind();
        }
    }

    private string getSetSqlCheck(string siglaAssociacao)
    {
        string retorno = "";
        if (siglaAssociacao == "EN") retorno = string.Format("SET @SiglaTipoAssociacao = 'EN' ", dbName, dbOwner);
        else if (siglaAssociacao == "UN") retorno = string.Format("SET @SiglaTipoAssociacao = 'UN' ", dbName, dbOwner);
        else if (siglaAssociacao == "ST") retorno = string.Format("SET @SiglaTipoAssociacao = 'ST' ", dbName, dbOwner);
        else if (siglaAssociacao == "PR") retorno = string.Format("SET @SiglaTipoAssociacao = 'PR' ", dbName, dbOwner);
        else if (siglaAssociacao == "ME") retorno = string.Format("SET @SiglaTipoAssociacao = 'ME' ", dbName, dbOwner);
        else if (siglaAssociacao == "PP") retorno = string.Format("SET @SiglaTipoAssociacao = 'PP' ", dbName, dbOwner);
        else if (siglaAssociacao == "TM") retorno = string.Format("SET @SiglaTipoAssociacao = 'TM' ", dbName, dbOwner);
        else if (siglaAssociacao == "OB") retorno = string.Format("SET @SiglaTipoAssociacao = 'OB' ", dbName, dbOwner);
        else if (siglaAssociacao == "IN") retorno = string.Format("SET @SiglaTipoAssociacao = 'IN' ", dbName, dbOwner);
        else if (siglaAssociacao == "CT") retorno = string.Format("SET @SiglaTipoAssociacao = 'CT' ", dbName, dbOwner);
        else if (siglaAssociacao == "DC") retorno = string.Format("SET @SiglaTipoAssociacao = 'DC' ", dbName, dbOwner);
        else if (siglaAssociacao == "DS") retorno = string.Format("SET @SiglaTipoAssociacao = 'DS' ", dbName, dbOwner);
        else if (siglaAssociacao == "PC") retorno = string.Format("SET @SiglaTipoAssociacao = 'PC' ", dbName, dbOwner);
        else if (siglaAssociacao == "EQ") retorno = string.Format("SET @SiglaTipoAssociacao = 'EQ' ", dbName, dbOwner);

        return retorno;
    }

    #endregion

    #region BANCO DE DADOS

    private void getChavePrimaria() // retorna a primary key da tabela.
    {
        if (gvDados.FocusedRowIndex >= 0)
        {
            Object[] obj = (Object[])gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoObjeto", "IniciaisTipoObjeto", "CodigoTipoObjeto");

            chaveCompostaObjeto.IDObjeto = int.Parse(obj[0].ToString()); // codigoObjetoAssociadoPK = obj[0].ToString(); //gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoObjetoAssociado").ToString();
            chaveCompostaObjeto.TipoAssociacao = obj[1].ToString(); //  codigoTipoAssociacaoPK = obj[1].ToString(); //gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoTipoAssociacao").ToString();
            chaveCompostaObjeto.IDAssociacao = int.Parse(obj[2].ToString());
            chaveCompostaObjeto.IDUsuario = codigoU; // codigoUsuarioPK = obj[2].ToString(); //gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoUsuario").ToString();
        }
    }

    private string persisteEdicaoInteressadoObjeto()
    {
        getChavePrimaria();

        string listaPerfis = "-1";
        int idObjetoPai = int.Parse(hfGeral.Get("CodigoObjetoPai").ToString());
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        if (hfGeral.Contains("CodigosPerfisSelecionados"))
            listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();

        string mensagemErro = "";
        try
        {
            if (cDados.incluirInteressadoObjeto(chaveCompostaObjeto.IDObjeto, chaveCompostaObjeto.IDAssociacao.ToString(), "NULL"
                                            , chaveCompostaObjeto.IDUsuario, herdaPermissaoObjetoSuperior
                                            , usuarioLogado.IDusuario, idObjetoPai, listaPerfis
                                            , usuarioLogado.IDEntidade, ref mensagemErro))
            {
                carregaGvDados();
            }
            return mensagemErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoPermissao()
    {
        getChavePrimaria();

        DataTable dt;
        string mensagemErro = "";
        string sqlPermissoes = "";
        string listaPerfis = "-1";
        int idObjetoPai = int.Parse(hfGeral.Get("CodigoObjetoPai").ToString());
        //int codigoObjetoAssociacao = int.Parse(codigoObjetoAssociadoPK);
        //int codigoUsuarioInteressado = int.Parse(codigoUsuarioPK);
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        if (hfGeral.Contains("CodigosPerfisSelecionados"))
            listaPerfis = hfGeral.Get("CodigosPerfisSelecionados").ToString();

        try
        {
            cDados.incluirInteressadoObjeto(chaveCompostaObjeto.IDObjeto, chaveCompostaObjeto.IDAssociacao.ToString(), "NULL"
                                        , chaveCompostaObjeto.IDUsuario, herdaPermissaoObjetoSuperior
                                        , usuarioLogado.IDusuario, idObjetoPai, listaPerfis
                                        , usuarioLogado.IDEntidade, ref mensagemErro);

            /* EN UN ST PR ME PP TM OB IN CT */
            if (Session["dtEN"] != null)
            {
                dt = (DataTable)Session["dtEN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtUN"] != null)
            {
                dt = (DataTable)Session["dtUN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtST"] != null)
            {
                dt = (DataTable)Session["dtST"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPR"] != null)
            {
                dt = (DataTable)Session["dtPR"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtME"] != null)
            {
                dt = (DataTable)Session["dtME"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPP"] != null)
            {
                dt = (DataTable)Session["dtPP"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtTM"] != null)
            {
                dt = (DataTable)Session["dtTM"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtOB"] != null)
            {
                dt = (DataTable)Session["dtOB"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtIN"] != null)
            {
                dt = (DataTable)Session["dtIN"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtCT"] != null)
            {
                dt = (DataTable)Session["dtCT"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtDC"] != null)
            {
                dt = (DataTable)Session["dtDC"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtDS"] != null)
            {
                dt = (DataTable)Session["dtDS"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtPC"] != null)
            {
                dt = (DataTable)Session["dtPC"];
                sqlPermissoes += getSQLpermissoes(dt);
            }
            if (Session["dtEQ"] != null)
            {
                dt = (DataTable)Session["dtEQ"];
                sqlPermissoes += getSQLpermissoes(dt);
            }

            //cDados.incluirPermissaoObjetoAoUsuario(siglaAssociacao, codigoUsuario, codigoOE, sqlPermissoes, ref mensagemErro);
            if (cDados.registraPermissoesUsuario(sqlPermissoes, chaveCompostaObjeto.TipoAssociacao, chaveCompostaObjeto.IDObjeto
                                                , chaveCompostaObjeto.IDUsuario, usuarioLogado.IDusuario, idObjetoPai
                                                , usuarioLogado.IDEntidade, ref mensagemErro))
            {
                carregaGvDados();
            }
            return mensagemErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteExclusaoRegistro()
    {
        string msgErro = "";
        int idObjetoPai = int.Parse(hfGeral.Get("CodigoObjetoPai").ToString());
        bool herdaPermissaoObjetoSuperior = checkHerdarPermissoes.Checked;

        getChavePrimaria();

        if (cDados.excluiPermissaoObjetoAoUsuario(chaveCompostaObjeto.IDAssociacao, chaveCompostaObjeto.IDObjeto
                                            , chaveCompostaObjeto.IDUsuario, usuarioLogado.IDusuario
                                            , chaveCompostaObjeto.TipoAssociacao, idObjetoPai, usuarioLogado.IDEntidade
                                            , herdaPermissaoObjetoSuperior, ref msgErro))
        {
            carregaGvDados();
        }
        return "";
    }

    private string getSQLpermissoes(DataTable dt)
    {
        int valorOriginal = 0;
        string listaPermissoes = "";
        //string codigoUsuario = hfGeral.Get("CodigoUsuarioPermissao").ToString();

        foreach (DataRow dr in dt.Rows)
        {
            int valorPermissao = 0;

            valorPermissao += dr["Concedido"].ToString().Equals("True") ? 1 : 0;
            valorPermissao += dr["Negado"].ToString().Equals("True") ? 2 : 0;
            valorPermissao += dr["Delegavel"].ToString().Equals("True") ? 4 : 0;
            valorPermissao += dr["Incondicional"].ToString().Equals("True") ? 8 : 0;

            valorOriginal = int.Parse(dr["FlagsOrigem"].ToString());
            //string efeitoAplicado = (dr["Negado"].ToString().Equals("True")) ? "-1" : (dr["Delegavel"].ToString().Equals("True")) ? "2" : (dr["Concedido"].ToString().Equals("True")) ? "1" : "0";

            // OBSERVAÇÃO Comparando apenas o primeiros 4 bit's.
            //if (valorPermissao != 0 && (valorPermissao & 15) != (valorOriginal & 15))
            if ((valorPermissao & 15) != (valorOriginal & 15))
            {
                listaPermissoes += dr["CodigoPermissao"].ToString() + "|" + valorPermissao + ";";
            }
        }

        return listaPermissoes; // retorno;
    }

    #endregion

    #region LISTBOX

    private void carregaListaPerfis(int codigoUsuario, int codigoObjeto, string tipoAssociacao, int codigoObjetoPai)
    {
        DataSet ds = cDados.getListaPerfisDisponiveisUsuario(codigoObjeto, tipoAssociacao, codigoObjetoPai
                                                            , usuarioLogado.IDEntidade, codigoUsuario);//getListaProjetoDisponivel(codigoGerente, codigoUnidade, codigoPerfis);
        lbListaPerfisDisponivel.DataSource = ds.Tables[0];
        lbListaPerfisDisponivel.DataBind();

        if (lbListaPerfisDisponivel.Items.Count > 0)
        {
            lbListaPerfisDisponivel.SelectedIndex = -1;
            btnADDTodos.ClientEnabled = true;
        }
    }

    private void carregaListaPerfisAssociados(int codigoUsuario, int codigoObjeto, string tipoAssociacao, int codigoObjetoPai)
    {
        DataSet ds = cDados.getListaPerfisAtribuidosUsuario(codigoObjeto, tipoAssociacao, codigoObjetoPai
                                                            , usuarioLogado.IDEntidade, codigoUsuario);//getListaProjetoSelecionados(codigoGerente, codigoUnidade, codigoPerfis);
        lbListaPerfisSelecionados.DataSource = ds.Tables[0];
        lbListaPerfisSelecionados.DataBind();

        if (lbListaPerfisSelecionados.Items.Count > 0)
        {
            lbListaPerfisSelecionados.SelectedIndex = -1;
            btnRMVTodos.ClientEnabled = true;
        }
    }

    #endregion

    protected void gvPermissoes_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName.Equals("APPLYCOLUMNFILTER"))
        {
            string siglaAssociacao = hfGeral.Get("itemMenu").ToString();

            //Prencho o Title da grid gvPermissoes.
            getTituloGridPermissoes(siglaAssociacao);
        }
    }
}

