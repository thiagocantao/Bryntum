/*OBSERVAÇÕES
 * 
 * MODIFICAÇÕES
 * 28/02/2011 : Alejandro :: altero a opção de visibilizar so os mapas da entidade logada.
 * 
 *              1- no Page_Init, altero a variavel global 'temMapa', aonde dependendo si tem mapa asociados e ativos
 *                  dejara a variavel em true. Isto e so para desabilitar o botão 'incluir' da grid.
 *              2- no método 'carregaGrid' se altero a parâmetro 'where', aonde se indica que os mapas a listar sejan
 *                  da entidade logada.
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

public partial class _Estrategias_wizard_menuMapa_TemasMapa : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado = 0;
    private int idEntidadeLogada = 0;
    private int idMapaEstrategico = 0;
    private int __PosicaoKeyObjeto = 0;
    private int __PosicaoKeyMapa = 1;
    private int __PosicaoKeyVersaoMapa = 2;
    
    public bool temMapa = false;
    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        idEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        //Recupero parâmetros referente ao mapa que será tratado.
        idMapaEstrategico = int.Parse(Request.QueryString["CM"].ToString());
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        cDados.aplicaEstiloVisual(Page);
        cDados.aplicaEstiloVisual(heGlossario, "Default");
        if (IsPostBack)
        {
            /// em postBack, não carregará novamente o combo, então é preciso atribuir valor à variável [podeIncluir]
            podeIncluir = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, idEntidadeLogada, "ME", "ME_IncPspTemObj");
        }
        else
        {
            hfGeral.Set("chave", "-1");
            cDados.aplicaEstiloVisual(Page);
            defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());
            podeIncluir = carregaComboMapas() > 0;
        }
        carregaComboUsuario();
        carregaGrid();
        carregaComboTemasPerspectivasPageLoad();
    }

    #region GRIDVIEW

    private void carregaGrid()
    {
        string where = string.Format(@"
            AND toe.[IniciaisTipoObjeto]            = 'TEM' 
            AND me.[IndicaMapaEstrategicoAtivo]     = 'S' 
            AND oe.CodigoObjetoEstrategia IN (SELECT CodigoObjeto FROM {0}.{1}.f_GetObjetosDeInteresse({2}, NULL, 'TM', {3}, NULL, NULL)) "
            , cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, idEntidadeLogada);

        DataSet dsObjetos = cDados.getObjetosEstrategicos(idUsuarioLogado, idEntidadeLogada, where
            , "ORDER BY me.[TituloMapaEstrategico],  oe.[DescricaoObjetoEstrategia]", true, "TM");

        if (cDados.DataSetOk(dsObjetos))
        {
            gvDados.DataSource = dsObjetos;
            gvDados.DataBind();
        }
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        int permissoes = -1;
        if (gvDados.GetRowValues(e.VisibleIndex, "Permissoes") != null)
        {
            permissoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
        }

        podeEditar = (permissoes & 2) > 0;
        podeExcluir = (permissoes & 4) > 0;
        podePermissao = (permissoes & 8) > 0;

        if (e.ButtonID.Equals("btnEditarCustom"))
        {
            if (!podeEditar)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        else if (e.ButtonID.Equals("btnExcluirCustom"))
        {
            if (!podeExcluir)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        else if (e.ButtonID.Equals("btnPermissaoCustom"))
        {
            if (!podePermissao)
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
            }
        }
    }

    #endregion

    #region COMBOBOX

    private int carregaComboMapas()
    {
        string where = string.Format(@" AND Mapa.[IndicaMapaEstrategicoAtivo] = 'S' 
            AND {0}.{1}.f_VerificaAcessoConcedido({2}, un.[CodigoEntidade], Mapa.[CodigoMapaEstrategico], NULL, 'ME', 0, NULL, 'ME_IncPspTemObj') = 1 "
            , cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado);
        int qtdMapas = 0;
        DataSet dsMapas = cDados.getMapasEstrategicos(idEntidadeLogada, where);

        if (cDados.DataSetOk(dsMapas))
        {
            ddlMapaEstrategico.DataSource = dsMapas;
            ddlMapaEstrategico.TextField = "TituloMapaEstrategico";
            ddlMapaEstrategico.ValueField = "CodigoMapaEstrategico";
            ddlMapaEstrategico.DataBind();

            ddlMapaEstrategico.Items.Insert(0, new DevExpress.Web.ListEditItem("", "0"));
        }

        qtdMapas = ddlMapaEstrategico.Items.Count;
        if (!IsPostBack && ddlMapaEstrategico.Items.Count > 0)
        {
            ddlMapaEstrategico.SelectedIndex = 0;
            carregaComboPerspectiva(int.Parse(ddlMapaEstrategico.Value.ToString()));
        }

        return qtdMapas;
    }

    private void carregaComboUsuario()
    {

        ddlResponsavel.TextField = "NomeUsuario";
        ddlResponsavel.ValueField = "CodigoUsuario";


        ddlResponsavel.Columns[0].FieldName = "NomeUsuario";
        ddlResponsavel.Columns[1].FieldName = "EMail";
        ddlResponsavel.TextFormatString = "{0}";

    }

    private void carregaComboPerspectiva(int codigoMapa)
    {
        string where = string.Format(@" 
            AND me.CodigoMapaEstrategico            = {0}
            AND toe.[IniciaisTipoObjeto]            = 'PSP' "
            , codigoMapa);

        DataSet dsObjetos = cDados.getObjetosEstrategicos(idUsuarioLogado, idEntidadeLogada, where
            , "ORDER BY oe.[TituloObjetoEstrategia]", false, "");

        if (cDados.DataSetOk(dsObjetos))
        {
            ddlPerspectiva.DataSource = dsObjetos;
            ddlPerspectiva.TextField = "DescricaoObjeto";
            ddlPerspectiva.ValueField = "CodigoObjetoEstrategia";
            ddlPerspectiva.ImageUrlField = "urlImagemObjetoCombo";
            ddlPerspectiva.DataBind();
        }
    }

    protected void ddlPerspectiva_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parametro = e.Parameter.ToString().Split(';');
        ddlPerspectiva.JSProperties["cp_ValueEdicao"] = "";

        carregaComboPerspectiva(int.Parse(parametro[0] == "null" ? "-1" : parametro[0]));

        if (parametro.Length > 1)
            if(parametro[1] != "")
                ddlPerspectiva.JSProperties["cp_ValueEdicao"] = parametro[1];
    }

    protected void callbackPerspectiva_Callback(object sender, CallbackEventArgsBase e)
    {
        string[] parametro = e.Parameter.ToString().Split(';');
        ddlPerspectiva.JSProperties["cp_ValueEdicao"] = "";

        carregaComboPerspectiva(int.Parse(parametro[0] == "null" ? "-1" : parametro[0]));

        if (parametro.Length > 1)
            if (parametro[1] != "")
                ddlPerspectiva.JSProperties["cp_ValueEdicao"] = parametro[1];
    }

    #endregion

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui um scripts nesta tela
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../../scripts/TemasMapa.js""></script>"));
        this.TH(this.TS("barraNavegacao", "TemasMapa"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = alturaPrincipal - 200;

        gvDados.Settings.VerticalScrollableHeight = altura - 200;
    }

    #endregion

    #region CALLBACK'S

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
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

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
        {
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }

    #endregion

    #region BANCO DE DADOS.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "-1";
        //CodigoObjetoEstrategia;CodigoMapaEstrategico;CodigoVersaoMapaEstrategico
        if (gvDados.FocusedRowIndex != -1)
        {
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoObjetoEstrategia").ToString() + ";" + gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoMapaEstrategico").ToString() + ";" + gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoVersaoMapaEstrategico").ToString();
        }
          
        return codigoDado.ToString();
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro
    {
        string novoCodigoObjeto = "0";
        string resultado = "";
        string msg = "";

        //Valida Combobox Usuário (ddlResponsavel)
        if (validaComboboxUsuario())
        {
            msg = Resources.traducao.TemasMapa_usu_rio_n_o_cadastrado;
            return msg;
        }

        int idMapa = int.Parse(ddlMapaEstrategico.Value.ToString());
        int idResponsavel = ((ddlResponsavel.Value != null) && (ddlResponsavel.Value.ToString() != "")) ? int.Parse(ddlResponsavel.Value.ToString()) : -1;
        int idPerspectiva = int.Parse(hfGeral.Get("idPerspectiva").ToString());//ddlPerspectiva.Value.ToString());


        try
        {
            if (cDados.incluiTemasMapa(idMapa, 1, "TEM", txtTituloTema.Text,txtDescricaoTema.Text, heGlossario.Html, idUsuarioLogado, idResponsavel, idPerspectiva, ref novoCodigoObjeto, ref resultado, idEntidadeLogada))
            {
                if (resultado.Equals("OK"))
                {
                    carregaGrid();
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigoObjeto);
                    gvDados.ClientVisible = false;
                }
                else
                    msg = Resources.traducao.TemasMapa_n_o_se_permite_a_inclus_o_de_perspectivas_com_um_mesmo_nome_para_um_mesmo_mapa_estrat_gico_;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        // busca a chave primaria
        string[] chave = getChavePrimaria().Split(';');
        string resultado = "";
        string msg = "";

        try
        {

            //Valida Combobox Usuário (ddlResponsavel)
            if (validaComboboxUsuario())
            {
                msg = Resources.traducao.TemasMapa_usu_rio_n_o_cadastrado;
                return msg;
            }

            int idResponsavel = ((ddlResponsavel.Value != null) && (ddlResponsavel.Value.ToString() != "")) ? int.Parse(ddlResponsavel.Value.ToString()) : -1;

            if (cDados.atualizaTemasMapa(txtTituloTema.Text, txtDescricaoTema.Text, idUsuarioLogado, heGlossario.Html, int.Parse(chave[__PosicaoKeyObjeto])
                                , int.Parse(chave[__PosicaoKeyMapa]), int.Parse(chave[__PosicaoKeyVersaoMapa]), idResponsavel
                                , ref resultado, idEntidadeLogada))
            {
                if (resultado.Equals("OK"))
                {
                    carregaGrid();
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
                    gvDados.ClientVisible = false;
                }
                else
                    msg = Resources.traducao.TemasMapa_n_o_se_permite_a_edi__o_de_perspectivas_com_um_mesmo_nome_para_um_mesmo_mapa_estrat_gico_;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registrojaj
    {
        // busca a chave primaria
        string[] chave = getChavePrimaria().Split(';');
        string resultado = "";
        string msg = "";

        try
        {
            if (cDados.excluiTemasMapa(int.Parse(chave[__PosicaoKeyObjeto]), int.Parse(chave[__PosicaoKeyMapa])
                                            , int.Parse(chave[__PosicaoKeyVersaoMapa]), idUsuarioLogado, ref resultado))
            {
                if (resultado.Equals("OK"))
                {
                    carregaGrid();
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
                    gvDados.ClientVisible = false;
                }
                else
                    msg = Resources.traducao.TemasMapa_existem_temas_ou_objetivos_estrat_gicos_diretamente_associados_;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private bool validaComboboxUsuario()
    {
        try
        {
            long value = 0;
            if (!Int64.TryParse(ddlResponsavel.Value.ToString(), out value))
                return true;  /*"Usuário não cadastrado";*/
        }
        catch (Exception)
        {
            return true;
        }
        return false;
    }

    #endregion
    protected void ddlResponsavel_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(idEntidadeLogada);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }

    }
    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(idEntidadeLogada, e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);


    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "TemasMapa");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); desabilitaHabilitaComponentes(); TipoOperacao = 'Incluir';", true, true, false, "TemasMapa", "Temas Mapa", this);
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
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }


    private void carregaComboTemasPerspectivasPageLoad()
    {
        try
        {
            string where = string.Format(@" 
                AND me.CodigoMapaEstrategico            = {0}
                AND toe.[IniciaisTipoObjeto]            IN('TEM', 'PSP')"
                , ddlMapaEstrategico.Value);

            DataSet dsObjetos = cDados.getObjetosEstrategicos(idUsuarioLogado, idEntidadeLogada, where
                , "ORDER BY [DescricaoObjeto]", false, "");

            if (cDados.DataSetOk(dsObjetos))
            {

                ddlPerspectiva.DataSource = dsObjetos;
                ddlPerspectiva.TextField = "DescricaoObjeto";
                ddlPerspectiva.ValueField = "CodigoObjetoEstrategia";
                ddlPerspectiva.ImageUrlField = "urlImagemObjetoCombo";

                ddlPerspectiva.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


}
