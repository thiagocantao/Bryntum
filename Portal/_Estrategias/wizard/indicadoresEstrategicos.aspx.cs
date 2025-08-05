/*
 OBSERVAÇÕES
 * 
 * MUDANÇA
 * 07/02/2011 - Alejandro: 
                a.- Implementar Compartilhamento dos indicadores.
                    #region SESSION COMPARTILHAR...
 * 
 * 18/03/2011 :: Alejandro : Adicionar o control do acesso a tela.
 * 21/03/2011 :: Alejandro : Adicionar o control do botão de permissao [IN_AdmPrs].
 */
using System;
using System.Data;
using System.Web;
using DevExpress.Web;
using System.Drawing;
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class _Estrategias_wizard_indicadores : System.Web.UI.Page
{
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

    private string dbName;
    private string dbOwner;
    private string resolucaoCliente = "";
    private int codigoUsuarioResponsavel = 0;
    private int codigoEntidadeUsuarioResponsavel = 0;

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;
    public bool podeCompartilhar = false;
    public bool podeIncluirComponente = false; 
    public bool podeTrocarResponsavel = false;
    public string definicaoEntidadePluralDisp = "";
    public string definicaoEntidadePluralSel = "";
    string podeAgruparPorMapa = "N";
    
    protected void Page_Init(object sender, EventArgs e)
    {
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

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        if (!IsPostBack)
        {
            bool bPodeAcessarTela;
            bPodeAcessarTela = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadInd");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_AdmPrs");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_AltRsp");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_Compart");

            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_Alt");

            // se não puder, redireciona para a página sem acesso
            if (bPodeAcessarTela == false)
                cDados.RedirecionaParaTelaSemAcesso(this);
        }

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        if (!hfGeral.Contains("TipoOperacao"))
            hfGeral.Set("TipoOperacao", "Consultar");    
           
        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadInd"))
            podeIncluir = true;

        definicaoEntidadePluralDisp = Resources.traducao.indicadoresEstrategicos_entidades_dispon_veis;
        definicaoEntidadePluralSel = Resources.traducao.indicadoresEstrategicos_entidades_selecionadas;
       
        DataSet ds = cDados.getDefinicaoEntidade(codigoEntidadeUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoEntidadePluralDisp = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString() + " disponíveis";
            definicaoEntidadePluralSel = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString() + " selecionadas";
            
        }
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("barraNavegacao", "indicadores", "indicadoresEstrategicos"));

        HeaderOnTela();
        
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "utilizaColunaMapaTelaIndicadores");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            podeAgruparPorMapa = dsParam.Tables[0].Rows[0]["utilizaColunaMapaTelaIndicadores"].ToString();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        carregaGvDados(); 
        populaCombos();
        cDados.aplicaEstiloVisual(Page);//Ok
        //gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvDados.Columns["NomeUnidadeNegocio"].Caption = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }
    }
    

    #region gvDados

    private void carregaGvDados() //Carga a grid Lista.
    {
        string where = ""; // "AND iu.IndicaUnidadeCriadoraIndicador = 'S'";        

        if (podeAgruparPorMapa == "N")
        {
            gvDados.Columns["MapaEstrategico"].Visible = false;
            ((GridViewDataTextColumn)gvDados.Columns["MapaEstrategico"]).GroupIndex = -1;
        }

        DataSet ds = cDados.getIndicadores(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, podeAgruparPorMapa, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        

        if (e.RowType == GridViewRowType.Data)
        {
            string unidadeAtivo = e.GetValue("IndicaUnidadeCriadoraIndicador").ToString();

            if (unidadeAtivo != "S")
            {
                /*
                e.Row.BackColor = Color.FromName("#DDFFCC");
                e.Row.ForeColor = Color.Black;
                */
                e.Row.ForeColor = Color.FromName("#619340");
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            string unidadeCriadora = "";
            unidadeCriadora = gvDados.GetRowValues(e.VisibleIndex, "IndicaUnidadeCriadoraIndicador").ToString();
            int permissoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
            string podeCompartilharIndi = gvDados.GetRowValues(e.VisibleIndex, "PodeCompartilhar").ToString();

            podeEditar = (permissoes & 2) > 0;
            podePermissao = (permissoes & 4) > 0;
            podeCompartilhar = (permissoes & 8) > 0 && podeCompartilharIndi == "S";
            podeTrocarResponsavel = (permissoes & 16) > 0;

            podeExcluir = podeEditar;

            if (e.ButtonID.Equals("btnEditar"))
            {
                if (unidadeCriadora.Equals("S") && podeEditar)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Text = "Editar";
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnExcluir"))
            {
                if (unidadeCriadora.Equals("S") && podeExcluir)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Text = "Excluir";
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnResponsavel"))
            {
                if (podeTrocarResponsavel)
                    e.Enabled = true;
                else
                {
                    //e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    e.Enabled = false;
                    e.Text = Resources.traducao.indicadoresEstrategicos_editar_respons_vel;
                    e.Image.Url = "~/imagens/TrocaRespDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnPermissoesCustom"))
            {
                if (podePermissao)
                    e.Enabled = true;
                else
                {
                    e.Text = Resources.traducao.indicadoresEstrategicos_alterar_permiss_es;
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnCompartilhar"))
            {
                if (podeCompartilhar)
                    e.Enabled = true;
                else
                {
                    //e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    e.Enabled = false;
                    e.Text = Resources.traducao.indicadoresEstrategicos_compartilhar;
                    e.Image.Url = "~/imagens/compartilharDes.png";
                }
            }
        }
    }

    #endregion

    #region COMBOBOX

    private void populaCombos()
    {
        ddlResponsavel.TextField = "NomeUsuario";
        ddlResponsavel.ValueField = "CodigoUsuario";

        ddlResponsavelResultados2.TextField = "NomeUsuario";
        ddlResponsavelResultados2.ValueField = "CodigoUsuario";
    }

    #endregion

    #region varios

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/indicadores.js""></script>"));
    }

    #endregion

    #region CALLBACK'S

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        //defineTituloGridDados(false);

        if (e.Parameter == "Excluir")
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

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        // busca a chave primaria
        string chave = getChavePrimaria();

        cDados.excluiIndicador(chave, codigoUsuarioResponsavel.ToString());
        carregaGvDados();
        return "";
    }

    protected void pnCallbackResponsavel_Callback(object sender, CallbackEventArgsBase e)
    {
        pnCallbackResponsavel.JSProperties["cp_sucesso"] = "";
        pnCallbackResponsavel.JSProperties["cp_erro"] = "";

        string opcao = e.Parameter;

        if (opcao == "salvar" && ddlResponsavel.SelectedIndex != -1 && ddlResponsavelResultados2.SelectedIndex != -1)
        {
            string codigoIndicador = getChavePrimaria();
            string codigoUnidadeNegocio = "";

            if (gvDados.GetSelectedFieldValues("CodigoUnidadeNegocio").Count > 0)
                codigoUnidadeNegocio = gvDados.GetSelectedFieldValues("CodigoUnidadeNegocio")[0].ToString();
            else
                codigoUnidadeNegocio = "-1";

            string comandoSQL = string.Format(@"

                IF EXISTS (SELECT 1 FROM {0}.{1}.IndicadorUnidade iu 
							          WHERE iu.CodigoIndicador = {3} 
								        AND iu.CodigoUnidadeNegocio = {4}
                                        AND iu.IndicaUnidadeCriadoraIndicador = 'S')
              BEGIN
                    UPDATE {0}.{1}.Indicador 
                       SET CodigoUsuarioResponsavel = {2}
                     WHERE CodigoIndicador = {3}
              END
                UPDATE {0}.{1}.IndicadorUnidade
                   SET CodigoResponsavelIndicadorUnidade = {2}
                     , CodigoReservado = '{5}'
                     , CodigoResponsavelAtualizacaoIndicador = {6}
                 WHERE CodigoIndicador      = {3}
                   AND CodigoUnidadeNegocio = {4}

            ", cDados.getDbName(), cDados.getDbOwner(), ddlResponsavel.Value.ToString()
             , codigoIndicador, codigoUnidadeNegocio, txtCodigoReservadoNovoResp.Text, ddlResponsavelResultados2.Value.ToString());

            string mensagem = "";
            DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + Environment.NewLine + comandoSQL + Environment.NewLine + cDados.geraBlocoEndTran());
            if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                mensagem = ds.Tables[0].Rows[0][0].ToString();
            }
            if(mensagem.ToUpper().Trim() == "OK")
            {
                pnCallbackResponsavel.JSProperties["cp_sucesso"] = Resources.traducao.indicadoresEstrategicos_dados_alterados_com_sucesso_;
            }
            else
            {
                pnCallbackResponsavel.JSProperties["cp_erro"] = mensagem;
            }
        }
    }

    #endregion

    #region BANCO DE DADOS.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex > -1)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }
      
    #endregion    

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

        dsResponsavel.SelectParameters.Clear();
        dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
        comboBox.DataSource = dsResponsavel;
        comboBox.DataBind();
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "IndEstrat");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abreDetalhes(-1, 'N');", true, true, false, "IndEstrat", "Cadastro de Indicadores", this);
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
