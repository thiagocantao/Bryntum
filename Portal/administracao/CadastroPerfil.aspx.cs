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
using System.Collections.Specialized;

public partial class administracao_CadastroPerfil : BasePageBrisk
{


    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    private string dbName;
    private string dbOwner;
    private bool fUsandoAutenticacaoExterna;

    public bool podeIncluir = true;
    public bool podeEditar = true;
    public bool podeExcluir = true;
    public bool podeCopiarPermissoes = false;

    public string perfilDestino = "";
    public string perfilOrigem = "";
    public string tipoAssociacaoDestino = "";
    

    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();

        dsPerfilOrigem.ConnectionString = CDados.classeDados.getStringConexao();
        sds.ConnectionString = CDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.TH(this.TS("barraNavegacao", "CadastroPerfil"));
        gvDados.Settings.VerticalScrollableHeight = TelaAltura - 270;
        CDados.aplicaEstiloVisual(Page);

        
        if (!IsPostBack) 
        {
            CDados.VerificaAcessoTela(this, UsuarioLogado.Id, UsuarioLogado.CodigoEntidade, UsuarioLogado.CodigoEntidade, "NULL", "EN", 0, "NULL", "CriPflAcs");
            cerrarSessionCheck();
            hfGeral.Set("itemMenu", "EN");
            hfGeral.Set("textoItem", "EN");
            hfGeral.Set("TipoOperacao", "Consultar");
            hfGeral.Set("CodigoPerfil", "-1");
            
            carregaMenuLateral(hfGeral.Get("textoItem").ToString());
            tcDados.ActiveTabIndex = 0;
            //populaCheckBoxList(getChavePrimaria());
        }
        podeCopiarPermissoes = CDados.VerificaPermissaoUsuario(UsuarioLogado.Id, UsuarioLogado.CodigoEntidade, "US_CopPrf");
        fUsandoAutenticacaoExterna = verificaAutenticacaoExterna();

        getPermissoesTela();
        populaComboTipoObjeto();
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
            setTituloGridPermissao(siglaAssociacao);
        }

        if (!IsPostBack)
        {
            CDados.excluiNiveisAbaixo(1);
            CDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        CDados.setaTamanhoMaximoMemo(mmObservacao, 250, lblContadorMemoObservacao);
        
        //gvPermissoes.Settings.ShowFilterRow = false;
        gvPermissoes.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    private bool verificaAutenticacaoExterna()
    {
        bool bRet = false;
        DataSet dsParam = CDados.getParametrosSistema(UsuarioLogado.CodigoEntidade, "utilizaAutenticacaoExterna");
            
        if (CDados.DataSetOk(dsParam) && CDados.DataTableOk(dsParam.Tables[0]) && dsParam.Tables[0].Rows[0]["utilizaAutenticacaoExterna"].ToString() != "")
        {
            bRet = dsParam.Tables[0].Rows[0]["utilizaAutenticacaoExterna"].ToString().ToUpper().Equals("S");
        }
        return bRet;
    }

    private void getPermissoesTela()
    {
        podeIncluir = CDados.VerificaPermissaoUsuario( UsuarioLogado.Id, UsuarioLogado.CodigoEntidade, "CriPflAcs");
        podeEditar = podeIncluir;
        podeExcluir = podeIncluir;
        //podeEditar = CDados.VerificaPermissaoUsuario(idUsuarioLogado, idEntidadeLogada, "CriPflAcs");
        //podeExcluir = CDados.VerificaPermissaoUsuario(idUsuarioLogado, idEntidadeLogada, "CriPflAcs");
    }

    #region VARIOS

 

    private string getSiglaAssociacao(string textoAssociacao)
    {
        string retorno = "";

        if (textoAssociacao == "Entidade")          retorno = "EN";
        else if (textoAssociacao == "Unidade")      retorno = "UN";
        else if (textoAssociacao == "Estratégia")   retorno = "ST";
        else if (textoAssociacao == "Projeto")      retorno = "PR";
        else if (textoAssociacao == "Mapa")         retorno = "ME";
        else if (textoAssociacao == "Perspectiva")  retorno = "PP";
        else if (textoAssociacao == "Tema")         retorno = "TM";
        else if (textoAssociacao == "Objetivo")     retorno = "OB";
        else if (textoAssociacao == "Indicador")    retorno = "IN";
        else if (textoAssociacao == "Contrato")     retorno = "CT";
        else if (textoAssociacao == "Demanda Complexa") retorno = "DC";
        else if (textoAssociacao == "Demanda Simple")   retorno = "DS";
        else if (textoAssociacao == "Processo") retorno = "PC";
        else if (textoAssociacao == "Riscos Corporativos") retorno = "R1";


        return retorno;
    }

    #endregion

    #region NAVBAR

    private void carregaMenuLateral(string iniciaisTO)
    {
        /* EN UN ST PR ME PP TM OB IN CT */

        //vaciando o conteudo do antiguo menú.
        for (int i = 0; i < nbAssociacao.Groups.Count; i++)
            nbAssociacao.Groups[i].Items.Clear();

        //carregar o novo menú
        string getMenuSql = string.Format(@"
            SELECT 
                  [CodigoTipoAssociacao]
                , [DescricaoTipoAssociacao]
                , [IniciaisTipoAssociacao] 
            FROM 
                {0}.{1}.f_GetTiposAssociacoesDescendentes('{2}', 'PERMISSAO')
            ", dbName, dbOwner, iniciaisTO);

        DataSet ds = CDados.getDataSet(getMenuSql);

        if (CDados.DataSetOk(ds))
        {
            string menuId, descricaoTipoObjeto, iniciaisTipoObjeto;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                /* EN UN ST PR ME PP TM OB IN CT DC DS PC AD CP DM EQ*/
                descricaoTipoObjeto = dr["DescricaoTipoAssociacao"].ToString();
                iniciaisTipoObjeto = dr["IniciaisTipoAssociacao"].ToString();

                if (iniciaisTipoObjeto.Equals("EN")) menuId = "mnEntidade";
                else if (iniciaisTipoObjeto.Equals("UN")) menuId = "mnUnidades";
                else if (iniciaisTipoObjeto.Equals("ST")) menuId = "mnEstrategia";
                else if (iniciaisTipoObjeto.Equals("PR")) menuId = "mnProjeto";
                else if (iniciaisTipoObjeto.Equals("ME")) menuId = "mnMapas";
                else if (iniciaisTipoObjeto.Equals("PP")) menuId = "mnPerspectiva";
                else if (iniciaisTipoObjeto.Equals("TM")) menuId = "mnTema";
                else if (iniciaisTipoObjeto.Equals("OB")) menuId = "mnObjetivo";
                else if (iniciaisTipoObjeto.Equals("IN")) menuId = "mnIndicador";
                else if (iniciaisTipoObjeto.Equals("CT")) menuId = "mnContrato";
                else if (iniciaisTipoObjeto.Equals("DC")) menuId = "mnDemandaComplexa";
                else if (iniciaisTipoObjeto.Equals("DS")) menuId = "mnDemandaSimple";
                else if (iniciaisTipoObjeto.Equals("PC")) menuId = "mnProcesso";
                else if (iniciaisTipoObjeto.Equals("AD")) menuId = "mnAssuntoDemanda";
                else if (iniciaisTipoObjeto.Equals("CP")) menuId = "mnCarteiraProjeto";
                else if (iniciaisTipoObjeto.Equals("DM")) menuId = "mnDemanda";
                else if (iniciaisTipoObjeto.Equals("EQ")) menuId = "mnEquipe";
                else if (iniciaisTipoObjeto.Equals("R1")) menuId = "mnRiscosCorporativos";


                else menuId = "";

                if(!menuId.Equals(""))
                    nbAssociacao.Groups.FindByName("gpAssociacao").Items.Add(new NavBarItem(descricaoTipoObjeto, menuId));
            }

            if(nbAssociacao.Items.Count > 0)
            {
                hfGeral.Set("itemMenu", getIniciaisTipoAssociacao(nbAssociacao.Items[0].Name));
                nbAssociacao.Items[0].Selected = true;
            }
        }
    }

    private string getIniciaisTipoAssociacao(string textoAssociacao)
    {
        string retorno = "";

        /* EN UN ST PR ME PP TM OB IN CT DC DS PC AD CP DM EQ R1*/

        if (textoAssociacao == "mnEntidade") retorno = "EN";
        else if (textoAssociacao == "mnUnidade") retorno = "UN";
        else if (textoAssociacao == "mnEstrategia") retorno = "ST";
        else if (textoAssociacao == "mnProjeto") retorno = "PR";
        else if (textoAssociacao == "mnMapas") retorno = "ME";
        else if (textoAssociacao == "mnPerspectiva") retorno = "PP";
        else if (textoAssociacao == "mnTema") retorno = "TM";
        else if (textoAssociacao == "mnObjetivo") retorno = "OB";
        else if (textoAssociacao == "mnIndicador") retorno = "IN";
        else if (textoAssociacao == "mnContrato") retorno = "CT";
        else if (textoAssociacao == "mnDemandaComplexa") retorno = "DC";
        else if (textoAssociacao == "mnDemandaSimple") retorno = "DS";
        else if (textoAssociacao == "mnProcesso") retorno = "PC";
        else if (textoAssociacao == "mnAssuntoDemanda") retorno = "AD";
        //TODO: Alterado Eduardo.Rocha
        else if (textoAssociacao == "mnCarteiraProjeto") retorno = "CP";
        else if (textoAssociacao == "mnDemanda") retorno = "DM";
        else if (textoAssociacao == "mnEquipe") retorno = "EQ";
        else if (textoAssociacao == "mnRiscosCorporativos") retorno = "R1";
        return retorno;
    }

    #endregion

    #region GRIDVIEW

    #region GVDADOS

    /// <summary>
    /// Tabelas que involucra a consulta
    /// FROM        {0}.{1}.Perfil                      AS p
    /// INNER JOIN  {0}.{1}.TipoAssociacao              AS ta   ON p.CodigoTipoAssociacao = ta.CodigoTipoAssociacao
    /// INNER JOIN	{0}.{1}.HierarquiaTipoAssociacao    AS hta  ON p.CodigoTipoAssociacao = hta.CodigoTipoAssociacaoFilho 
    /// 
    /// ter en cuenta caso se tenha qeu fazer algum where o order by...
    /// </summary>
    private void carregaGvDados()
    {
        string where = " AND (p.[IniciaisPerfil] IS NULL OR p.[IniciaisPerfil] != 'COMPART') AND p.CodigoEntidade = " + UsuarioLogado.CodigoEntidade;
        string orderBy = " ORDER BY hta.NivelHierarquicoFilho  ";
        DataSet ds = CDados.getPerfil(where, orderBy);

        if (CDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        bool bLocalPodeExcluir, bLocalPodeEditar;
        string iniciaisPerfil = "";
        string ativacao = "";

        if (gvDados.GetRowValues(e.VisibleIndex, "IniciaisPerfil") != null && gvDados.GetRowValues(e.VisibleIndex, "IniciaisPerfil").ToString() != "")
            iniciaisPerfil = "OK";
        if (gvDados.GetRowValues(e.VisibleIndex, "StatusPerfil") != null && gvDados.GetRowValues(e.VisibleIndex, "StatusPerfil").ToString() != "A")
            ativacao = "OK";

        bLocalPodeExcluir = podeExcluir;
        bLocalPodeEditar = podeEditar;

        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            if (e.ButtonID.Equals("btnExcluir"))
            {
                // se estiver usando autenticação externa, não permite excluir os perfis da entidade
                if (bLocalPodeExcluir && fUsandoAutenticacaoExterna)
                    bLocalPodeExcluir = (gvDados.GetRowValues(e.VisibleIndex, "IniciaisTipoAssociacao").ToString().ToUpper() != "EN");

                if (bLocalPodeExcluir)
                {
                    if (iniciaisPerfil.Equals(""))
                    {
                        if (ativacao.Equals(""))
                        {
                            e.Text = Resources.traducao.desativar;
                        }
                        else
                        {
                            e.Image.Url = "~/imagens/botoes/btnOn.png";
                            e.Text = "Reativar";
                        }
                    }
                    else
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/btnOnDes.png";
                        e.Text = "";
                    }
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/btnOnDes.png";
                    e.Text = "";
                }
            }
            if (e.ButtonID.Equals("btnEditar"))
            {
                if (!bLocalPodeEditar)
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                    e.Text = "";
                }
            }
            if (e.ButtonID == "btnCopiaPerfil")
            {
                if (!podeCopiarPermissoes)
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            bool unidadeAtivo = (e.GetValue("IniciaisPerfil").ToString() != "");

            if (unidadeAtivo)
            {
                //e.Row.BackColor = Color.FromName("#DDFFCC");
                e.Row.ForeColor = Color.FromName("#619340");

            }
        }
    }

    #endregion

    #region GVPERMISSAO

    protected void gvPermissoes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        //Tratamento da coluna de botoes.
        //bool somenteLeitura = true;
        //if (hfGeral.Contains("TipoOperacao") && (hfGeral.Get("TipoOperacao").ToString() == "Incluir" || hfGeral.Get("TipoOperacao").ToString() == "Editar"))
        //    somenteLeitura = false;
        //gvPermissoes.Columns[0].Visible = !somenteLeitura;

        string parametro = e.Parameters.ToString();

        if (parametro.Equals("ATL"))
        {
            string textoItem = hfGeral.Get("itemMenu").ToString();

            //Prencho o Title da grid gvPermissoes.
            setTituloGridPermissao(textoItem);

            string nomeSession = "dt" + textoItem;
            DataTable dt = (DataTable)Session[nomeSession]; //(DataTable)Session["dtPermissoes"];
            if (null != dt)
            {
                gvPermissoes.DataSource = dt;
                gvPermissoes.DataBind();
            }
        }
        else if (parametro.Equals("VOLTAR"))
        {
            cerrarSessionCheck();
        }
        else
        {
            //if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() == "Editar")
            //    populaSessiones(int.Parse(hfGeral.Get("CodigoPerfil").ToString()));

            populaChecks(parametro);
        }
    }

    private void setTituloGridPermissao(string textoItem)
    {
        if (textoItem == "EN") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___entidades;
        else if (textoItem == "UN") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___unidades;
        else if (textoItem == "ST") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___estrat_gias;
        else if (textoItem == "PR") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___projetos;
        else if (textoItem == "ME") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___mapas;
        else if (textoItem == "PP") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___perspectivas;
        else if (textoItem == "TM") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___tema;
        else if (textoItem == "OB") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___objetivo_estrat_gico;
        else if (textoItem == "IN") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___indicador;
        else if (textoItem == "CT") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___contrato;
        else if (textoItem == "DC") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___demandas_complexas;
        else if (textoItem == "DS") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___demandas_simples;
        else if (textoItem == "PC") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___processos;
        //TODO: Alterado Eduardo.Rocha
        else if (textoItem == "CP") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___carteiras_de_projeto;
        else if (textoItem == "EQ") gvPermissoes.SettingsText.Title = Resources.traducao.CadastroPerfil_permiss_es___equipe_de_recursos;
        else if (textoItem == "R1") gvPermissoes.SettingsText.Title = "Permissões - Riscos Corporativos";
    }

    protected void gvPermissoes_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        //if (e.VisibleIndex != -1)
        //{
        //    bool indicaCheckConcedido = (bool)gvPermissoes.GetRowValues(e.VisibleIndex, "Concedido");
        //    bool indicaCheckDelegavel = (bool)gvPermissoes.GetRowValues(e.VisibleIndex, "Delegavel");
        //    bool indicaCheckNegado = (bool)gvPermissoes.GetRowValues(e.VisibleIndex, "Negado");

        //    if (e.Row.Cells.Count > 5)
        //    {
        //        if (hfGeral.Get("TipoOperacao").ToString().Equals("Consultar"))
        //        {
        //            e.Row.Cells[2].Enabled = false;
        //            e.Row.Cells[3].Enabled = false;
        //            e.Row.Cells[4].Enabled = false;
        //            e.Row.Cells[5].Enabled = false;
        //            e.Row.Cells[6].Enabled = false;
        //            e.Row.Cells[5].Attributes.Add("disabled", "disabled");
        //            e.Row.Cells[6].Attributes.Add("disabled", "disabled"); 
        //        }
        //        else
        //        {
        //            if (indicaCheckConcedido || indicaCheckDelegavel || indicaCheckNegado)
        //                e.Row.Cells[6].Enabled = true;
        //            else
        //                e.Row.Cells[6].Enabled = false;
        //        }
        //    }
        //}
    }

    protected void gvPermissoes_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        Color color = new Color();

        if (e.RowType == GridViewRowType.Data)
        {
            bool indicaPermissaoHerdada = (bool)e.GetValue("Herdada");
            bool indicaPermissaoIncondicional = (bool)e.GetValue("Incondicional");

            if (indicaPermissaoHerdada)
            {
                int red = Int32.Parse("DD", System.Globalization.NumberStyles.HexNumber);
                int green = Int32.Parse("FF", System.Globalization.NumberStyles.HexNumber);
                int blue = Int32.Parse("CC", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(red, green, blue);
                e.Row.BackColor = color;
                //e.Row.Enabled = false;
                //if (indicaPermissaoIncondicional)
                //    e.Row.Enabled = false;
            }

            if (indicaPermissaoIncondicional)
            {
                //e.Row.Enabled = false;
            }
        }
    }

    #endregion

    #endregion

    #region COMBOBOX

    private void populaComboTipoObjeto()
    {
        string where = "";

        // se estiver usando autenticação externa, não permite incluir perfil para a entidade;
        if (fUsandoAutenticacaoExterna)
        {
            where = " AND taf.[IniciaisTipoAssociacao] != 'EN' ";
        }

        DataSet ds = CDados.getTipoAssociacaoTelaPerfil(where);
        if ((CDados.DataSetOk(ds)))
        {
            //NivelHierarquicoFilho
            /*
              taf.CodigoTipoAssociacao
                    ,   taf.DescricaoTipoAssociacao
                    ,   hta.NivelHierarquicoFilho
             */

            DataRow dr = ds.Tables[0].NewRow();
            dr["CodigoTipoAssociacao"] = "-2";
            dr["DescricaoTipoAssociacao"] = Resources.traducao.CadastroPerfil_tipo_existente;
            dr["NivelHierarquicoFilho"] = "1";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ddlTipoObjeto.DataSource = ds.Tables[0];
            ddlTipoObjeto.DataBind();
        }
    }

    #endregion

    #region SESSIONES

    private void cerrarSessionCheck()
    {
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
        //TODO: Alterado Eduardo.Rocha
        if (Session["dtCP"] != null) Session.Remove("dtCP");
        if (Session["dtEQ"] != null) Session.Remove("dtEQ");
    }

    private void populaSessiones(int idPerfil)
    {
        /* EN UN ST PR ME PP TM OB IN CT */
        DataSet ds;
        DataSet dsTA;
        
        string sqlPermissao;
        //string listaPerfis = "NULL";
        string iniciais = hfGeral.Get("itemMenu").ToString();
        hfGeral.Set("textoItem", iniciais);

        string strComando = string.Format(@"
            SELECT 
                  [CodigoTipoAssociacao]
                , [DescricaoTipoAssociacao]
                , [IniciaisTipoAssociacao] 
            FROM 
                {0}.{1}.f_GetTiposAssociacoesDescendentes('{2}', 'PERMISSAO')
            ", dbName, dbOwner, iniciais);
        dsTA = CDados.getDataSet(strComando);

        if (CDados.DataSetOk(dsTA))
        {
            string descricaoTipoObjeto;
            string iniciaisTipoObjeto;

            foreach (DataRow dr in dsTA.Tables[0].Rows)
            {
                string nomeSession = "";
                descricaoTipoObjeto = dr["DescricaoTipoAssociacao"].ToString();
                iniciaisTipoObjeto = dr["IniciaisTipoAssociacao"].ToString();
                nomeSession = "dt" + iniciaisTipoObjeto;

                sqlPermissao = getSQLPermissao(idPerfil, iniciaisTipoObjeto);
                ds = CDados.getDataSet(sqlPermissao);
                Session[nomeSession] = ds.Tables[0];
            }

            if (nbAssociacao.Items.Count > 0)
            {
                hfGeral.Set("itemMenu", getIniciaisTipoAssociacao(nbAssociacao.Items[0].Name));
                nbAssociacao.Items[0].Selected = true;
            }
        }

    }

    private string getSQLPermissao(int idPerfil, string iniciais)
    {
        string sqlPermissao = "";

        sqlPermissao = string.Format(@"
            SELECT  * 
            FROM    {0}.{1}.f_GetPermissoesPerfil({2}, NULL, '{3}')
            ", dbName, dbOwner, idPerfil, iniciais);

        return sqlPermissao;
    }

    #endregion

    #region CHECKBOX

    private void populaChecks(string associacao)
    {
        /* EN UN ST PR ME PP TM OB IN CT */

        string sqlPermissao;
        string codigoTipoAssociacao = "";
        // string codigoUsuarioPermissao = "-1";

        //Prencho o Title da grid gvPermissoes.
        setTituloGridPermissao(associacao);

        //Vejo si a sessión existe. Caso que não, cargo o dados do Banco.
        string nomeSession = "dt" + associacao;
        if (Session[nomeSession] == null)
        {
            if (associacao == "EN") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'EN'", dbName, dbOwner);
            else if (associacao == "UN") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'UN'", dbName, dbOwner);
            else if (associacao == "ST") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'ST'", dbName, dbOwner);
            else if (associacao == "PR") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'PR'", dbName, dbOwner);
            else if (associacao == "ME") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'ME'", dbName, dbOwner);
            else if (associacao == "PP") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'PP'", dbName, dbOwner);
            else if (associacao == "TM") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'TM'", dbName, dbOwner);
            else if (associacao == "OB") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'OB'", dbName, dbOwner);
            else if (associacao == "IN") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'IN'", dbName, dbOwner);
            else if (associacao == "CT") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'CT'", dbName, dbOwner);
            else if (associacao == "DC") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'DC' ", dbName, dbOwner);
            else if (associacao == "DS") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'DS' ", dbName, dbOwner);
            else if (associacao == "PC") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'PC' ", dbName, dbOwner);
            //TODO: Alterado Eduardo.Rocha
            else if (associacao == "CP") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'CP' ", dbName, dbOwner);
            else if (associacao == "EQ") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'EQ' ", dbName, dbOwner);
            else if (associacao == "R1") codigoTipoAssociacao = string.Format("SET @SiglaTipoAssociacao = 'R1' ", dbName, dbOwner);
            sqlPermissao = string.Format(@"
            DECLARE @SiglaTipoAssociacao VARCHAR(2)

            {3}

            SELECT * 
            FROM {0}.{1}.f_GetPermissoesPerfil( {2}, NULL, @SiglaTipoAssociacao) 

            ", dbName, dbOwner, int.Parse(hfGeral.Get("CodigoPerfil").ToString()), codigoTipoAssociacao);

            DataSet ds = CDados.getDataSet(sqlPermissao);
            if (CDados.DataSetOk(ds) && CDados.DataTableOk(ds.Tables[0]))
            {
                gvPermissoes.DataSource = ds.Tables[0];
                gvPermissoes.DataBind();
            }

            Session[nomeSession] = ds.Tables[0];
        }
        //else
        //{
        //Caso que a sessión exista, prencho a grid gvPermissoes con ela.
        //DataTable dt = (DataTable)Session[nomeSession];
        //gvPermissoes.DataSource = dt;
        //gvPermissoes.DataBind();
        //}
    }

    #endregion

    #region CALLBACK'S

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cpErroSalvar"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpStatusSalvar"] = "0";
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_TipoNovoPerfil"] = "";
        string retorno = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoPerfil(ref retorno);
            pnCallback.JSProperties["cp_TipoNovoPerfil"] = retorno;
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoPerfil(ref retorno);

            if(mensagemErro_Persistencia == "")
                mensagemErro_Persistencia = persisteInclusaoPermissao();

            pnCallback.JSProperties["cp_TipoNovoPerfil"] = retorno;
        }
        else if (e.Parameter == "ExcluirDesativar")
        {
            mensagemErro_Persistencia = persisteDesativarPerfil();
        }
        else if (e.Parameter == "ExcluirReativar")
        {
            mensagemErro_Persistencia = persisteReativarPerfil();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoPerfil();
        }
        //else if (e.Parameter == "EditarPermissao")
        //{
        //mensagemErro_Persistencia = persisteInclusaoPermissao();
        //}

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            ((ASPxCallbackPanel)sender).JSProperties["cpStatusSalvar"] = "1"; // 1 indica que foi salvo com sucesso.
        }
        else // alguma coisa deu errado...
            ((ASPxCallbackPanel)sender).JSProperties["cpErroSalvar"] = mensagemErro_Persistencia;
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

                    dt.AcceptChanges();
                    break;
                } // if (dr["CodigoTipoProjeto"].ToString() == e.Keys["CodigoTipoProjeto"].ToString())
            } // foreach (DataRow dr in dt)

            //Session["dtPermissoes"] = dt;
            Session[nomeSession] = dt;

            gvPermissoes.DataSource = dt;
            gvPermissoes.DataBind();
        }  // if (null != dt)
    }

    protected void pnCallbackPermissoes_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();

        carregaMenuLateral(parametro);
    }

    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter.Equals("CerrarSession"))
            cerrarSessionCheck();
        else if (e.Parameter.Equals("editarPermissao"))
            populaSessiones(int.Parse(hfGeral.Get("CodigoPerfil").ToString()));
    }

    #endregion

    #region BANCO DE DADOS

    private string getChavePrimaria() // retorna a primary key da tabela
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }

    //Perfil.

    /// <summary>
    /// Inclui o usuário como interessado no objeto.
    /// </summary>
    /// <returns></returns>
    private string persisteInclusaoPerfil(ref string getIniciaiPerfilNovo)
    {
        int getCodigoPerfil = -1;
        string getMensagem = "";
       // string getIniciaiPerfil = "";

        try
        {
            if (CDados.incluirPerfil(int.Parse(ddlTipoObjeto.Value.ToString()), txtNomePerfil.Text, mmObservacao.Text
                                    ,UsuarioLogado.CodigoEntidade, UsuarioLogado.Id, ref getCodigoPerfil, ref getIniciaiPerfilNovo
                                    , ref getMensagem))
            {
                if (getMensagem.Equals(Resources.traducao.CadastroPerfil_ok))
                {
                    hfGeral.Set("CodigoPerfil", getCodigoPerfil.ToString());
                    carregaGvDados();
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(getCodigoPerfil);
                    gvDados.ClientVisible = false;
                    getMensagem = "";
                }
                else
                    getMensagem = Resources.traducao.CadastroPerfil_n_o_se_permite_a_inclus_o_de_perfil_com_um_mesmo_nome_para_um_mesmo_tipo_de_objeto_;
            }
        }
        catch (Exception ex)
        {
            getMensagem = ex.Message;
        }
        return getMensagem;
    }

    private string persisteEdicaoPerfil(ref string getIniciaiPerfil)
    {
        int idPerfil = int.Parse(getChavePrimaria());
        string getMensagem = "";

        try
        {
            if (CDados.atualizaPerfil(idPerfil, txtNomePerfil.Text, mmObservacao.Text, UsuarioLogado.CodigoEntidade, ref getIniciaiPerfil, ref getMensagem))
            {
                if (getMensagem.Equals(Resources.traducao.CadastroPerfil_ok))
                {
                    carregaGvDados();
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(idPerfil);
                    gvDados.ClientVisible = false;
                    getMensagem = "";
                }
                else
                    getMensagem = Resources.traducao.CadastroPerfil_n_o_se_permite_a_inclus_o_de_perfil_com_um_mesmo_nome_para_um_mesmo_tipo_de_objeto_;
            }
        }
        catch (Exception ex)
        {
            getMensagem = ex.Message;
        }

        return getMensagem;
    }

    /// <summary>
    /// Método responsável pela Exclusão do registro.
    /// </summary>
    /// <returns></returns>
    private string persisteExclusaoPerfil()
    {
        string mensagemErro = "";
        int idPerfil = int.Parse(getChavePrimaria());

        try
        {
            if (CDados.excluiPerfil(idPerfil,UsuarioLogado.Id, UsuarioLogado.CodigoEntidade, ref mensagemErro))
            {
                carregaGvDados();
            }
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }

        return mensagemErro;
    }

    /// <summary>
    /// Método responsável pela Exclusão do registro.
    /// </summary>
    /// <returns></returns>
    private string persisteDesativarPerfil()
    {
        string mensagemErro = "";
        int idPerfil = int.Parse(getChavePrimaria());

        try
        {
            if (CDados.desativarPerfil(idPerfil, UsuarioLogado.Id, UsuarioLogado.CodigoEntidade, ref mensagemErro))
            {
                carregaGvDados();
            }
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }

        return mensagemErro;
    }

    /// <summary>
    /// Método responsável pela Exclusão do registro.
    /// </summary>
    /// <returns></returns>
    private string persisteReativarPerfil()
    {
        string mensagemErro = "";
        int idPerfil = int.Parse(getChavePrimaria());

        try
        {
            if (CDados.reativarPerfil(idPerfil, UsuarioLogado.Id, UsuarioLogado.CodigoEntidade, ref mensagemErro))
            {
                carregaGvDados();
            }
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }

        return mensagemErro;
    }

    //Permissões.

    /// <summary>
    /// Inclui as permissões ao interessado do objeto
    /// </summary>
    /// <returns></returns>
    private string persisteInclusaoPermissao()
    {
        DataTable dt;
        int idPerfil = int.Parse(hfGeral.Get("CodigoPerfil").ToString());
        string mensagemErro = "";
        string sqlPermissoes = "";

        try
        {
            /* EN         UN         ST         PR         ME         PP         TM         OB         IN */
            if (Session["dtEN"] != null)
            {
                dt = (DataTable)Session["dtEN"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtUN"] != null)
            {
                dt = (DataTable)Session["dtUN"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtST"] != null)
            {
                dt = (DataTable)Session["dtST"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtPR"] != null)
            {
                dt = (DataTable)Session["dtPR"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtME"] != null)
            {
                dt = (DataTable)Session["dtME"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtPP"] != null)
            {
                dt = (DataTable)Session["dtPP"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtTM"] != null)
            {
                dt = (DataTable)Session["dtTM"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtOB"] != null)
            {
                dt = (DataTable)Session["dtOB"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtIN"] != null)
            {
                dt = (DataTable)Session["dtIN"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtCT"] != null)
            {
                dt = (DataTable)Session["dtCT"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtDC"] != null)
            {
                dt = (DataTable)Session["dtDC"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtDS"] != null)
            {
                dt = (DataTable)Session["dtDS"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtPC"] != null)
            {
                dt = (DataTable)Session["dtPC"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            //TODO: Alterado Eduardo.Rocha
            if (Session["dtCP"] != null)
            {
                dt = (DataTable)Session["dtCP"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (Session["dtEQ"] != null)
            {
                dt = (DataTable)Session["dtEQ"];
                sqlPermissoes += getSQLpermissoes(dt, idPerfil);
            }
            if (CDados.incluirPermissoesPerfil(idPerfil, sqlPermissoes, UsuarioLogado.Id, ref mensagemErro))
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

    private string getSQLpermissoes(DataTable dt, int idPerfil)
    {
        //int valorOriginal = 0;
        //string retorno = "";
        string listaPermissoes = "";
        //string codigoUsuario = hfGeral.Get("CodigoUsuarioPermissao").ToString();

        //digitando SQL para inserir um novo objeto.
        foreach (DataRow dr in dt.Rows)
        {
            int valorPermissao = 0;

            valorPermissao += dr["Concedido"].ToString().Equals("True") ? 1 : 0;
            valorPermissao += dr["Negado"].ToString().Equals("True") ? 2 : 0;
            valorPermissao += dr["Delegavel"].ToString().Equals("True") ? 4 : 0;
            valorPermissao += dr["Incondicional"].ToString().Equals("True") ? 8 : 0;

            listaPermissoes += dr["CodigoPermissao"].ToString() + "|" + valorPermissao + ";";
        }

//        retorno = string.Format(@"EXEC {0}.{1}.p_perm_registraPermissoesPerfil {2}, '{3}', {4} ", dbName, dbOwner, idPerfil, listaPermissoes, idUsuarioLogado);
        //digitando SQL para atualizar um objeto.
        return listaPermissoes; // retorno; // retorno;
    }

    #endregion

    protected void gvPermissoes_DataBinding(object sender, EventArgs e)
    {
        //gvPermissoes.ExpandAll();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        CDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadPerfAcs");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        CDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "IncluirNovoRegistroPerfil();", true, true, false, "CadPerfAcs", "Cadastro de Perfis", this);
    }

    #endregion
    
    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    public string getCheckBox(string nomeCheck, string coluna, string inicial)
    {
        string retorno = "";
        string desabilitado = hfGeral.Get("TipoOperacao").ToString().Equals("Consultar") ? "disabled='disabled'" : "";
                
        if (nomeCheck == "CheckIncondicional" && !hfGeral.Get("TipoOperacao").ToString().Equals("Consultar"))
        {
            bool indicaCheckConcedido = (bool)Eval("Concedido");
            bool indicaCheckDelegavel = (bool)Eval("Delegavel");
            bool indicaCheckNegado = (bool)Eval("Negado");


            if (indicaCheckConcedido || indicaCheckDelegavel || indicaCheckNegado)
                desabilitado = "";
            else
                desabilitado = "disabled='disabled'";

        }

        retorno = "<input " + desabilitado + " id='" + nomeCheck + "' " + ((Eval(coluna).ToString() == "1" || Eval(coluna).ToString() == "True") ? "checked='CHECKED'" : "") + " onclick='clicaConceder(" + Eval("CodigoPermissao") + ", this.checked, \"" + inicial + "\")' type='checkbox' />";

        return retorno;
    }
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

    // copia perfil

    protected void hfStatusCopiaPerfil_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistroCopiaPermissao();
        }

        // Grava a mensagem de erro. Se não houve erro, terá conteúdo ""
        hfStatusCopiaPerfil.Set("ErroSalvar", mensagemErro_Persistencia);
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfStatusCopiaPerfil.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            ddlPerfilOrigem.SelectedIndex = -1;

        }
        else // alguma coisa deu errado...
            hfStatusCopiaPerfil.Set("StatusSalvar", "0"); // 1 indica que foi salvo com sucesso.
    }

    private string persisteEdicaoRegistroCopiaPermissao()
    {
        try
        {
            perfilDestino = hfStatusCopiaPerfil.Get("CodigoPerfilDestino").ToString();
            perfilOrigem = hfStatusCopiaPerfil.Get("CodigoPerfilOrigem").ToString();

            string msgErro = salvaRegistroCopiaPermissao("E", int.Parse(perfilOrigem), int.Parse(perfilDestino));

            //populaGrid();

            return msgErro;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string salvaRegistroCopiaPermissao(string modo, int CodigoPerfilOrigem, int CodigoPerfilDestino)
    {

        //chamar procedure  p_adm_clonaPerfil
        string comandoSQL = string.Format(
                            @"BEGIN
                            DECLARE @retorno varchar(2048)

                            EXEC [dbo].[p_adm_clonaPerfil] {0}, {1}, {2}, {3}, null, null, null, null,  {4},  @retorno output 

                            SELECT  @retorno

                          END", "CLO",  UsuarioLogado.CodigoEntidade, CodigoPerfilOrigem, CodigoPerfilDestino, UsuarioLogado.Id);
        DataSet ds = CDados.getDataSet(comandoSQL);
        string retorno = ds.Tables[0].Rows[0][0].ToString();
        return retorno;

    }


    protected void populaComboPerfilOrigem()
    {
        
        string comandoSQL = string.Format(@"
                select * from 
                   (SELECT -1 AS codigo
	                ,'            ------ " + Resources.traducao.CadastroPerfil_selecione + @" ----- 'AS descricao
                union 
                    SELECT p.CodigoPerfil AS codigo
	                        ,p.DescricaoPerfil_PT AS descricao
                        FROM {0}.{1}.Perfil p
                        WHERE CodigoEntidade = {3}
	                        AND p.StatusPerfil = 'A'
	                        AND codigoPerfil NOT IN ({2})
                            AND codigoTipoAssociacao = {4}
             
                ) lista
                order by lista.descricao     
                          ", CDados.getDbName(), CDados.getDbOwner(), hfStatusCopiaPerfil.Contains("CodigoPerfilDestino") ? hfStatusCopiaPerfil.Get("CodigoPerfilDestino") : -1,
                           UsuarioLogado.CodigoEntidade, hfStatusCopiaPerfil.Contains("CodigoTipoAssociacaoDestino") ? hfStatusCopiaPerfil.Get("CodigoTipoAssociacaoDestino") : -1) ;
        DataSet ds = CDados.getDataSet(comandoSQL);

        if (CDados.DataSetOk(ds) && CDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows.Count > 1)
            {
                ddlPerfilOrigem.DataSource = ds.Tables[0];
                ddlPerfilOrigem.ValueField = "codigo";
                ddlPerfilOrigem.TextField = "descricao";
                ddlPerfilOrigem.DataBind();
            }
            else
            {
                ddlPerfilOrigem.Visible = false;
                
            }
        }
    }  
    protected void callbackPerfilOrigem_Callback(object sender, CallbackEventArgsBase e)
    {
        populaComboPerfilOrigem();
    }
}