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

public partial class _Portfolios_Administracao_criteriosSelecaoNova : System.Web.UI.Page
{
    dados cDados;
    private string nomeTabelaDb = "CriterioSelecao";
    private string whereUpdateDelete;
    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int alturaPrincipal = 0;
    public bool podeIncluir = false;

    //DataSet dsValoresCriterios = new DataSet();
    //DataSet dsValoresRiscos = new DataSet();

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "PO_CadCriSel");
        }

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "PO_CadCriSel"))
        {
            podeIncluir = true;
        }
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        cDados.aplicaEstiloVisual(Page);
        if (!IsPostBack)
        {

            defineAlturaTela();
            gridDescricao.Columns[0].Visible = false;
        }

        populaGrid();

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        gridDescricao.Settings.ShowFilterRow = false;
        gridDescricao.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/criteriosSelecaoNova.js""></script>"));
        this.TH(this.TS("barraNavegacao", "criteriosSelecaoNova"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Cadastro de Critérios de Seleção</title>"));
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = altura;
    }
    #endregion

    private void populaGrid()
    {
        string where = " AND CodigoEntidade = '" + CodigoEntidade + "'";
        DataSet ds = cDados.getCriteriosSelecao(where);
        gvDados.DataSource = ds.Tables[0].DefaultView;
        gvDados.DataBind();


        string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
        captionGrid += string.Format(@"<td align=""left""><img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/></td>");
        captionGrid += string.Format(@"</tr></table>");
        gvDados.SettingsText.Title = captionGrid;
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {

        if (e.ButtonID == "btnEditar")
        {
            if (podeIncluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeIncluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    private ListDictionary getDadosFormulario(string TipoOperacao)
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DescricaoCriterioSelecao", txtCriterio.Text);
        if (TipoOperacao == "Incluir")
        {
            oDadosFormulario.Add("DataInclusao", DateTime.Now.Date.ToString());
            oDadosFormulario.Add("CodigoUsuarioInclusao", idUsuarioLogado);
            oDadosFormulario.Add("IndicaControladoSistema", 'N');
            oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        }
        return oDadosFormulario;
    }

    #region BANCO DE DADOS.

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            int temp = 0;
            mensagemErro_Persistencia = persisteInclusaoRegistro(ref temp);
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
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro(ref int codigoNovaCategoria)
    {
        try
        {
            // verifica se já existe um critério com o mesmo nome e, em caso positivo, gera uma exceção
            verificaExistenciaCriterioMesmoNome(-1);

            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario("Incluir");

            codigoNovaCategoria = cDados.insert(nomeTabelaDb, oDadosFormulario, true);
            populaGrid();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoNovaCategoria);
            gridDescricao.Columns[0].Visible = true;
            gvDados.ClientVisible = false;
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// verifica se já existe um critério com o mesmo nome e, em caso positivo, gera uma exceção. 
    /// </summary>
    /// <remarks>
    /// A função procura um critério com o nome informado e, caso seja encontrado, verifica se o código é diferente do atual, o que caracteriza
    /// duplicação de registros. Neste caso, será gerada uma exceção.
    /// </remarks>
    /// <param name="codigoCriterioAtual">Código do critério atual da tela. Deve ser informado o valor -1 caso seja uma inclusão</param>
    private void verificaExistenciaCriterioMesmoNome(int codigoCriterioAtual)
    {
        // não é permitido passar o valor zero.
        // quando for inclusão, deve ser passado o valor -1;
        if (codigoCriterioAtual == 0)
            throw new Exception(Resources.traducao.criteriosSelecaoNova_erro_ao_gravar_os_dados__falha_interna_aplica__o___c_digo_4_);

        int codigoCriterio2 = 0;
        string strSelect = string.Format(@"SELECT cs.[CodigoCriterioSelecao] FROM {0}.{1}.[CriterioSelecao] AS [cs] 
                WHERE   cs.[CodigoEntidade]             = {2}
                    AND cs.[DescricaoCriterioSelecao]   = '{3}' ",
            cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade, txtCriterio.Text);

        DataSet ds = cDados.getDataSet(strSelect);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            int.TryParse(ds.Tables[0].Rows[0]["CodigoCriterioSelecao"].ToString(), out codigoCriterio2);

        if ((codigoCriterio2 != 0) && (codigoCriterioAtual != codigoCriterio2))
        {
            throw new Exception(Resources.traducao.criteriosSelecaoNova_erro_ao_gravar_o_crit_rio__j__existe_um_crit_rio_com_esse_nome_);
        }
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        try
        {
            // verifica se já existe um critério com o mesmo nome e, em caso positivo, gera uma exceção
            verificaExistenciaCriterioMesmoNome(int.Parse(getChavePrimaria()));

            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario("Editar");
            cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        try
        {
            // antes de excluir o critério, tem que excluir as opções... isto será feito pela própria função de exclusão
            int codigoCriterioSelecao = int.Parse(getChavePrimaria());
            string msg = "";
            excluiCriteriosSelecao(codigoCriterioSelecao, ref msg);
            if (msg != "")
            {
                if (msg.IndexOf("REFERENCE") >= 0)
                {
                    if (msg.IndexOf("FK_ProjetoCriterioSelecao_OpcaoCriterioSelecao") >= 0)
                        msg = Resources.traducao.criteriosSelecaoNova_j__existe_projeto_associado_a_este_crit_rio__a_exclus_o_n_o_pode_ser_realizada_;
                    else if (msg.IndexOf("FK_CategoriaCriterioSelecao_CriterioSelecaoProposta") >= 0)
                        msg = Resources.traducao.criteriosSelecaoNova_j__existe_categoria_associada_a_este_crit_rio__a_exclus_o_n_o_pode_ser_realizada_;
                    else
                        msg = Resources.traducao.criteriosSelecaoNova_existem_informa__es_do_sistema_associada_a_este_crit_rio__a_exclus_o_n_o_pode_ser_realizada_;
                }

                throw new Exception(msg);
            }

            //cDados.delete(nomeTabelaDb, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion

    #region GRIDdESCRICAO

    private void mostraBotaoInsercaoGridDetail()
    {
        string CaptionGrid =
            @"  <table style='width:100%'>
                    <tr><td style='width:30px'>
                        <img src='../../imagens/botoes/novoReg.png' alt='" + Resources.traducao.criteriosSelecaoNova_nova_descri__o + @"' onclick='gridDescricao.AddNewRow();' style='cursor: pointer; border: 0px solid #CDCDCD'/>
                    </td>
                    <td>" + Resources.traducao.criteriosSelecaoNova_descri__es + @"</td>
                    </tr>
                </table>";

        gridDescricao.SettingsText.Title = CaptionGrid;
    }

    public void populaGridDescricao(int codigoCriterioSelecao)
    {
        DataSet ds = getCriteriosDescricao(codigoCriterioSelecao, "");
        gridDescricao.DataSource = ds.Tables[0];
        gridDescricao.DataBind();
    }

    protected void gridDescricao_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int codigoCriterioSelecao = int.Parse(getChavePrimaria());
        string CodigoOpcaoCriterioSelecao = e.Values[0].ToString();// ((char)(gridDescricao.VisibleRowCount + 65)).ToString();
        string msg = "";
        bool retorno = false;

        retorno = cDados.excluiOpcoesCriteriosSelecao(codigoCriterioSelecao, CodigoOpcaoCriterioSelecao, ref msg);

        if (!retorno)
        {
            cDados.alerta(this, msg);
        }
        e.Cancel = true;

        //Redistribui as letras
        int letraInicio = CodigoOpcaoCriterioSelecao[0];
        int letraTermino = (64 + gridDescricao.VisibleRowCount);

        if (letraInicio < letraTermino)
        {
            for (int letra = letraInicio; letra < letraTermino; letra++)
            {
                retorno = cDados.atualizaApenasOpcoesCriteriosSelecao(codigoCriterioSelecao, (char)(letra + 1), (char)letra, ref msg);
            }
        }
        e.Cancel = true;
        gridDescricao.CancelEdit();
        populaGridDescricao(codigoCriterioSelecao);
        return;
    }

    protected void gridDescricao_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string msg = "";
        bool retorno = false;

        int codigoCriterioSelecao = int.Parse(getChavePrimaria());
        string ItemCriterioSelecao = e.NewValues[0].ToString();// ((char)(gridDescricao.VisibleRowCount + 65)).ToString();
        string OpcaoCriterioSelecao = e.NewValues[1].ToString();
        int ValorOpcaoCriterioSelecao = int.Parse(e.NewValues[2].ToString());

        string DescricaoCriterioSelecao = string.Empty;
        if (null != e.NewValues[3])
            DescricaoCriterioSelecao = e.NewValues[3].ToString();

        retorno = incluiOpcoesCriterioSelecao(codigoCriterioSelecao, ItemCriterioSelecao, OpcaoCriterioSelecao, ValorOpcaoCriterioSelecao, DescricaoCriterioSelecao, ref msg);
        if (!retorno)
        {
            cDados.alerta(this, msg);
        }
        e.Cancel = true;
        gridDescricao.CancelEdit();
        populaGridDescricao(codigoCriterioSelecao);
        return;
    }

    protected void gridDescricao_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string msg = "";
        bool retorno = false;

        int codigoCriterioSelecao = int.Parse(getChavePrimaria());
        string ItemCriterioSelecao = e.OldValues[0].ToString();// ((char)(gridDescricao.VisibleRowCount + 65)).ToString();
        string OpcaoCriterioSelecao = e.NewValues[1].ToString();
        int ValorOpcaoCriterioSelecao = int.Parse(e.NewValues[2].ToString());

        string DescricaoCriterioSelecao = string.Empty;
        if (null != e.NewValues[3])
            DescricaoCriterioSelecao = e.NewValues[3].ToString();

        retorno = cDados.atualizaOpcoesCriteriosSelecao(codigoCriterioSelecao, ItemCriterioSelecao, OpcaoCriterioSelecao, ValorOpcaoCriterioSelecao, DescricaoCriterioSelecao, ref msg);
        if (!retorno)
        {
            cDados.alerta(this, msg);
        }

        e.Cancel = true;
        gridDescricao.CancelEdit();
        populaGridDescricao(codigoCriterioSelecao);
        return;

    }

    protected void gridDescricao_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        if (txtCriterio.Text.Trim() == "")
            throw new Exception(Resources.traducao.criteriosSelecaoNova_o_nome_do_crit_rio_deve_ser_informado_);
        else if (hfGeral.Get("TipoOperacao").ToString() == "Incluir")
            throw new Exception(Resources.traducao.criteriosSelecaoNova_o_crit_rio_deve_ser_salvo_antes_da_inclus_o_das_op__es_);

        e.NewValues["CodigoOpcaoCriterioSelecao"] = ((char)(gridDescricao.VisibleRowCount + 65)).ToString();
        e.NewValues["DescricaoEstendidaOpcao"] = string.Empty;
    }

    protected void gridDescricao_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.IndexOf("Incluir") >= 0)
        {
            // no modo de inclusão da grid Master, a grid detail não pode ter nenhuma ação habilitada
            gridDescricao.Columns[0].Visible = false;
            populaGridDescricao(-1);
        }
        else if (e.Parameters.IndexOf("Editar") >= 0)
        {
            //mostraBotaoInsercaoGridDetail();
            gridDescricao.Columns[0].Visible = true;
            populaGridDescricao(-1);
        }
        else
        {
            // retira o botão de inserçao da grid detail
            gridDescricao.SettingsText.Title = "&nbsp;";
            // esconde a coluna de ações da grid detail
            gridDescricao.Columns[0].Visible = false;
        }

        // se o codigo da grid Master veio no parametro, vamos popular a grid detail a partir dela
        int posDelimitadorCodigo = e.Parameters.IndexOf("_");
        if (posDelimitadorCodigo >= 0)
        {
            int CodigoCriterioSelecao = int.Parse(e.Parameters.Substring(posDelimitadorCodigo + 1));
            populaGridDescricao(CodigoCriterioSelecao);
        }
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CritSelPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CritSelPrj", "Critérios de Projetos", this);
    }

    protected void menu_ItemClick1(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CritSelPrj1");
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "gridDescricao.AddNewRow();", true, false, false, "CritSelPrj1", "Critérios de Projetos", this);
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

    public DataSet getCriteriosDescricao(int codigoCriterioSelecao, string where)
    {
       string comandoSQL = string.Format(
             @"SELECT * 
                FROM {0}.{1}.OpcaoCriterioSelecao
               WHERE CodigoCriterioSelecao = {2} {3}
            order by CodigoOpcaoCriterioSelecao", cDados.getDbName(), cDados.getDbOwner(), codigoCriterioSelecao, where);
        return cDados.getDataSet(comandoSQL);
    }
    public bool incluiOpcoesCriterioSelecao(int CodigoCriterioSelecao, string ItemCriterioSelecao
                                       , string OpcaoCriterioSelecao, int ValorOpcaoCriterioSelecao
                                       , string DescricaoCriterioSelecao, ref string mensagem)
    {
        int regAfetados = 0;
        try
        {
            string comandoSQL = string.Format(@"
                    INSERT INTO {0}.{1}.OpcaoCriterioSelecao(CodigoCriterioSelecao,CodigoOpcaoCriterioSelecao,DescricaoOpcaoCriterioSelecao,ValorOpcaoCriterioSelecao, DescricaoEstendidaOpcao)
                    VALUES({2}, '{3}', '{4}' , {5}, {6})
                    ", cDados.getDbName(), cDados.getDbOwner(), CodigoCriterioSelecao, ItemCriterioSelecao
                     , OpcaoCriterioSelecao.Trim().Replace("'", "''")
                     , ValorOpcaoCriterioSelecao
                     , DescricaoCriterioSelecao.Equals("") ? "NULL" : string.Format("'{0}'", DescricaoCriterioSelecao.Trim().Replace("'", "''")));
           cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception ex)
        {
            mensagem = ex.Message;
            return false;
        }
    }

    public bool excluiCriteriosSelecao(int CodigoCriterioSelecao, ref string msg)
    {
        int regAfetados = 0;
        try
        {
           string comandoSQL = string.Format(
                @"BEGIN 

                    begin tran 	

                    DELETE FROM {0}.{1}.OpcaoCriterioSelecao 
                     WHERE CodigoCriterioSelecao = {2} 

                    DELETE FROM {0}.{1}.CriterioSelecao 
                    WHERE CodigoCriterioSelecao = {2}

                    if @@error <> 0 
                        rollback tran
                    else
                        commit
 
              END", cDados.getDbName(), cDados.getDbOwner(), CodigoCriterioSelecao);
          cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return false;
        }

    }

}
