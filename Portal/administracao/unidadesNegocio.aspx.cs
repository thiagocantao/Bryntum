/*
 OBSERVAÇÕES
 * 
 * MUDANÇA
 * 10/12/2010 - Alejandro: Não pode gravar la palabra "NULL" no campo [CodigoReservado], mais sim deve gravar
 *                          o valor 'null' caso não seja prenchido.
 * 10/01/2011 - Alejandro: Problema ao guardar no campo [CodigoReservado], foi alterada no dados.cs o método
 *                          public DataSet incluiUnidadeNegocio(...){...}
 * 28/02/2011 - Alejandro: Troca de propiedades do comboBox (ddlGerente, ddlUnidadeSuperior).
 *                          a troca da propiedade ClientValue() por a propiedades Value, SelectedIndex, etc. 
 * 21/03/2011 :: Alejandro : - Adiccionar o controle do botão de Permissão [UN_AdmPrs].
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
using DevExpress.Web;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraPrinting;

public partial class administracao_unidadesNegocio : System.Web.UI.Page
{
    dados cDados;
    DataSet ds;

    private int idUsuarioLogado;
    private int codigoEntidade;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    private string definicaoUnidade = "";
    private string definicaoUnidadePlural = "";
    public string definicionLenda = "";

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;
    public bool exportaOLAPTodosFormatos = false;

    string dataAtual = "";


    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoUnidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            definicaoUnidadePlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();
        }

        setarDefinicaoUnidade();

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "EN_IncUnd"))
        {
            podeIncluir = true;
            podeEditar = true;
        }

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "EN_ExcUnd"))
        {
            podeExcluir = true;
        }

        if (!IsPostBack)
        {
            bool bPodeAcessarTela;

            /// verifica se o usuário tem permissão para incluir unidade
            bPodeAcessarTela = podeIncluir;

            /// verifica se o usuário tem permissão para administrar permissão de alguma unidade            
            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, codigoEntidade, "UN", "UN_AdmPrs");

            /// verifica se o usuário tem permissão para excluir    
            if (bPodeAcessarTela == false)
                bPodeAcessarTela = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "EN_ExcUnd");

            // se o usuário não puder acessar a tela, redireciona para a página sem acesso
            if (bPodeAcessarTela == false)
                cDados.RedirecionaParaTelaSemAcesso(this);
        }

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!Page.IsPostBack)
        {
            hfGeral.Set("DataAtual", DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + idUsuarioLogado);
            hfGeral.Set("NomeArquivo", "");
                
            cDados.aplicaEstiloVisual(Page);
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            carregaUnidadeSuperior("");
        }

        carregaGerente();
        carregaGrid();        
       
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        cDados.setaTamanhoMaximoMemo(txtObservacoes, 250, lblContadorMemo);
        dataAtual = hfGeral.Get("DataAtual").ToString();

        //pnCallbackUnidadeSuperior.ShowLoadingPanel = false;
        pnCallbackUnidadeSuperior.SettingsLoadingPanel.Enabled = false;

        //pnCallbackUnidadeSuperior.ShowLoadingPanelImage = false;
        pnCallbackUnidadeSuperior.SettingsLoadingPanel.ShowImage = false;

        pnCallbackUnidadeSuperior.HideContentOnCallback = false;
    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 170;
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        //string comando = string.Format(@"<script type='text/javascript'>desabilitaEdicao(tabControl);</script>");
        //this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/unidadesNegocio.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "unidadesNegocio"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=true&libraries=places""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places""></script>"));

    }

    private void setLabelTela()
    {
        DataSet ds = cDados.getDefinicaoEntidade(codigoEntidade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string descriçaoBanco = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            string descriçaoBancoPlural = ds.Tables[0].Rows[0]["DescricaoPluralTipoUnidade"].ToString();

            lblDescricaoCorReCor.Text = Resources.traducao.unidadesNegocio_recursos_corporativos_relacionado_a_ + descriçaoBanco;
            lblDescricaoCorUnidade.Text = descriçaoBancoPlural + " " + Resources.traducao.unidadesNegocio_inferiores_relacionadas_a_ + descriçaoBanco;
            lblDescricaoCorMaEst.Text = Resources.traducao.unidadesNegocio_mapas_estrategicos_relacionado_a_ + descriçaoBanco;
            lblDescricaoCorProjeto.Text = Resources.traducao.unidadesNegocio_projetos_ou_programas_relacionado_a_ + descriçaoBanco;
            lblDescricaoCorDemanda.Text = Resources.traducao.unidadesNegocio_demandass_relacionado_a_ + descriçaoBanco;
            lblDescricaoCorContrato.Text = Resources.traducao.unidadesNegocio_contratos_relacionado_a_ + descriçaoBanco;
        }
    }

    private void setarDefinicaoUnidade()
    {
        //lblNomeEntidade.Text = "Nome da " + definicaoEntidade;
        hfGeral.Set("definicaoUnidade", definicaoUnidade);
        hfGeral.Set("definicaoUnidadePlural", definicaoUnidadePlural);

        if (!IsPostBack)
        {
            gvDados.Columns["col_NomeUnidade"].Caption = definicaoUnidade;
            gvDados.Columns["col_UnidadeSuperior"].Caption = definicaoUnidade + " " + Resources.traducao.unidadesNegocio_superior;
        }

        pcDados.HeaderText = Resources.traducao.unidadesNegocio_detalhes;

        checkUnidade.Text = definicaoUnidade + " " + Resources.traducao.unidadesNegocio_ativa_;

        lblNome.Text = Resources.traducao.unidadesNegocio_descri__o_da_ + " " + definicaoUnidade + ":*";
        lblUnidadeSuperior.Text = definicaoUnidade + " " + Resources.traducao.unidadesNegocio_superior_+"*";
        //lblDescricaoNaoAtiva.Text = definicaoUnidadePlural + " Inativas";
        definicionLenda = definicaoUnidadePlural + " " + Resources.traducao.unidadesNegocio_inativas;
    }

    #endregion

    #region GRID

    //Carrega grid Principal
    public void carregaGrid()
    {
        string where1 = string.Format(@" AND un.CodigoUnidadeNegocio != un.CodigoEntidade ");
        ds = cDados.getUnidadesNegocioTela(codigoEntidade, idUsuarioLogado, where1);

        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();

        //Se o dataset possuir linha(s) será selecionada a primeira linha da grid principal
        //if (ds.Tables[0].Rows.Count > 0)
        //gvDados.FocusedRowIndex = 0;

        //if (gvDados.VisibleRowCount <= 0)
        //    gvDados.SettingsText.EmptyDataRow = "Não há " + definicaoUnidadePlural + " disponíveis...!";
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string unidadeAtivo = e.GetValue("IndicaUnidadeNegocioAtiva").ToString();

            if (unidadeAtivo == "N")
            {
                e.Row.ForeColor = Color.FromName("#914800");
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        bool acessaGoogleMaps = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, codigoEntidade, "EN", "EN_GoogleMaps");

        string unidadeAtivo = gvDados.GetRowValues(e.VisibleIndex, "IndicaUnidadeNegocioAtiva") + ""; //.ToString();
        if (unidadeAtivo != "")
        {
            int permissoes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Permissoes").ToString());
            bool unidadAtual = gvDados.GetRowValues(e.VisibleIndex, ("CodigoUnidadeNegocio") + "").ToString().Equals(codigoEntidade.ToString());
            podePermissao = (permissoes & 8) > 0;

            if (e.ButtonID == "btnEditar")
            {
                if (!podeEditar)
                {
                    e.Enabled = false;
                    e.Text = "Edição";
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
            else if (e.ButtonID == "btnExcluir")
            {
                if (!podeExcluir)
                {
                    e.Enabled = false;
                    e.Text = "Excluir";

                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else if (e.ButtonID == "btnCompartilharCustom")
            {
                if (unidadeAtivo.Equals("S")) { }

                if (!unidadAtual && !podePermissao)
                {
                    e.Enabled = false;
                    e.Text = "Compartilhar";
                    e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
                }
            }
            else if (e.ButtonID == "btnGoogleMaps" && acessaGoogleMaps == true && e.CellType == GridViewTableCommandCellType.Data)
            {
                if (acessaGoogleMaps)
                {
                    bool exibeBotaoGoogleMaps = false;
                    DataSet ds = cDados.getParametrosSistema(codigoEntidade, "visualizaIndGoogleMaps");
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        exibeBotaoGoogleMaps = (ds.Tables[0].Rows[0]["visualizaIndGoogleMaps"].ToString() == "S");
                    }
                    if (exibeBotaoGoogleMaps == true)
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
                else
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
    }

    protected void gvImpedimentos_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        Color color = new Color();
        if (!pnCallback.JSProperties.ContainsKey("cp_corRC")) pnCallback.JSProperties["cp_corRC"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corUN")) pnCallback.JSProperties["cp_corUN"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corME")) pnCallback.JSProperties["cp_corME"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corPP")) pnCallback.JSProperties["cp_corPP"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corDE")) pnCallback.JSProperties["cp_corDE"] = "";
        if (!pnCallback.JSProperties.ContainsKey("cp_corCO")) pnCallback.JSProperties["cp_corCO"] = "";

        if (e.RowType == GridViewRowType.Data)
        {
            string usuarioAtivo = e.GetValue("codImpedimento").ToString();

            if (usuarioAtivo == "RC")
            {//6A 5A CD
                int ri = Int32.Parse("6A", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("5A", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("CD", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corRC"] = "S";
            }
            if (usuarioAtivo == "UN")
            {
                int ri = Int32.Parse("98", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("FB", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("98", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corUN"] = "S";
            }
            if (usuarioAtivo == "ME")
            {
                int ri = Int32.Parse("B0", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("C4", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("DE", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corME"] = "S";
            }
            if (usuarioAtivo == "PP")
            {
                int ri = Int32.Parse("DC", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("DC", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("DC", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corPP"] = "S";
            }
            if (usuarioAtivo == "DE")
            {
                int ri = Int32.Parse("D8", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("BF", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("D8", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corDE"] = "S";
            }
            if (usuarioAtivo == "CO")
            {
                int ri = Int32.Parse("EE", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("E8", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("AA", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.BackColor = color;

                pnCallback.JSProperties["cp_corCO"] = "S";
            }
        }
    }

    #endregion

    #region COMBOBOX

    public void carregaGerente() //Carrega combo Gerente
    {
        //ds = cDados.getGerenteEntidade("", codigoEntidade);

        //ddlGerente.DataSource = ds.Tables[0];
        //ddlGerente.TextField = "NomeUsuario";
        //ddlGerente.ValueField = "CodigoUsuario";


        //DataRow dr = ds.Tables[0].NewRow();
        //dr["NomeUsuario"] = "Nenhum";
        //dr["EMail"] = "Nenhum";
        //dr["CodigoUsuario"] = 0;

        //ds.Tables[0].Rows.InsertAt(dr, 0);

        ////ddlGerente.Items.Insert(0,new ListEditItem("Nenhum"));
        ////ddlGerente.Items[0].Value = 0;
        //ddlGerente.DataBind();

        //if (!IsPostBack)
        //    ddlGerente.SelectedIndex = 0;

    }

    public void carregaUnidadeSuperior(string where) //Carrega combo Unidade Superior.
    {
        DataSet ds;
        //int codigoUnidadeNegocio = 0;
        int codigoUnidadeSuperior = 0;
        int codigoUnidadeAtual = -1;

        if ("IncluirNovo" != where)
        {
            try
            {
                codigoUnidadeAtual = int.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString());
                codigoUnidadeSuperior = int.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoUnidadeNegocioSuperior").ToString());
            }
            catch
            {
                codigoUnidadeAtual = -1;
            }
        }

        ds = cDados.getUnidadeSuperiorPermitidas(codigoEntidade, codigoUnidadeAtual, "");
        //ds = cDados.getUnidadeSuperior(string.Format(" AND CodigoUnidadeNegocio != {0} ", codigoUnidadeNegocio), codigoEntidade);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlUnidadeSuperior.DataSource = ds.Tables[0];
            ddlUnidadeSuperior.TextField = "SiglaUnidadeNegocio";
            ddlUnidadeSuperior.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeSuperior.DataBind();

            if (where != "")
            {
                if (ddlUnidadeSuperior.Items.Count > 0 && codigoUnidadeSuperior > 0)
                    ddlUnidadeSuperior.Value = codigoUnidadeSuperior;
                else
                    ddlUnidadeSuperior.SelectedIndex = -1;
            }
        }

        //Habilita/Deshabilita o componente comboBox 'ddlUnidadeSuperior'.
        ddlUnidadeSuperior.ClientEnabled = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
        ddlUnidadeSuperior.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
    }

    #endregion

    #region CALLBACK'S

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e) // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        carregaUnidadeSuperior("");
        hfGeral.Set("ErroSalvar", "");
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            //carregaUnidadeSuperior("IncluirNovo");
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            //carregaUnidadeSuperior("");
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
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


    protected void pnCallbackUnidadeSuperior_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = "0";

        if (e.Parameter != null)
            parametro = e.Parameter.ToString();

        if ("IncluirNovo" == parametro)
            carregaUnidadeSuperior(parametro);
        else
        {
            //...
            carregaUnidadeSuperior(parametro);
        }
    }

    /// <summary>
    /// Verifica a existencia do nome da unidade que se quer cadastrar.
    /// Prenche um parâmetro no hidden fiel [hfNovoNomeUnidade], tendo dois posivel valores:
    /// N : novo, que nao existe o nome ja cadastrado e nao excluido na tabela [UnidadeNegocio].
    /// E : existente, que existe o nome ja cadastrado e nao excluido na tabela [UnidadeNegocio].
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void hfNovoNomeUnidade_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
        string codigoUnidadeBanco = "-1";
        hfNovoNomeUnidade.Set("NomeVerificarNovaUnidade", "N");
        hfNovoNomeUnidade.Set("SiglaVerificarNovaUnidade", "N");
        hfNovoNomeUnidade.Set("CodigoVerificarNovaUnidade", "N");
        string nomeNovaUnidade = txtNome.Text.Trim();
        string siglaNovaUnidade = txtSigla.Text.Trim();
        string codigoReservadoNovaUnidade = txtCodigo.Text.Trim();
        DataSet ds;

        if (parametro.Equals("verificarNovo"))
        {

        }
        else if (parametro.Equals("verificarEditar"))
        {
            codigoUnidadeBanco = getChavePrimaria();
        }

        ds = cDados.getVerificarNomeUnidadeCadastrada(nomeNovaUnidade, siglaNovaUnidade, codigoReservadoNovaUnidade, " AND CodigoEntidade = " + codigoEntidade);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string nomeBanco = dr["NomeUnidadeNegocio"].ToString();
                string siglaBanco = dr["SiglaUnidadeNegocio"].ToString();
                string codigoReservadoBanco = dr["CodigoReservado"].ToString();

                if (parametro.Equals("verificarEditar"))
                {
                    if (!codigoUnidadeBanco.Equals(dr["CodigoUnidadeNegocio"].ToString()))
                    {
                        if (nomeBanco == nomeNovaUnidade)
                            hfNovoNomeUnidade.Set("NomeVerificarNovaUnidade", "E");
                        if (siglaBanco == siglaNovaUnidade.ToUpper())
                            hfNovoNomeUnidade.Set("SiglaVerificarNovaUnidade", "E");
                        if (codigoReservadoBanco == codigoReservadoNovaUnidade.ToUpper())
                            hfNovoNomeUnidade.Set("CodigoVerificarNovaUnidade", "E");
                    }
                }
                else
                {
                    if (nomeBanco == nomeNovaUnidade)
                        hfNovoNomeUnidade.Set("NomeVerificarNovaUnidade", "E");
                    if (siglaBanco == siglaNovaUnidade.ToUpper())
                        hfNovoNomeUnidade.Set("SiglaVerificarNovaUnidade", "E");
                    if (codigoReservadoBanco == codigoReservadoNovaUnidade.ToUpper())
                        hfNovoNomeUnidade.Set("CodigoVerificarNovaUnidade", "E");
                }
            }
        }
    }

    #endregion

    #region BANCO DE DADOS.

    private string getChavePrimaria() // retorna a primary key da tabela
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro
    {
        //Aqui estou declarando e inicializando as variaveis que irei usar
        string msg = "";
        char unidadeNegocioAtiva = checkUnidade.Checked ? 'S' : 'N';
        int codigoUnidadeNegocio = 0;
        int? unidadeSuperior = null;
        int codigoGerente = 0;
        if (ddlGerente.SelectedIndex != -1)
            codigoGerente = int.Parse(ddlGerente.Value.ToString());


        try
        {
            if (ddlUnidadeSuperior.SelectedIndex != -1)
                unidadeSuperior = int.Parse(ddlUnidadeSuperior.Value.ToString());

            //Insere a Unidade Negocio com NivelHierarquico como 0 e EstruturaHierarquica com 0 pois será feito um "scope_identity()" para poder montar o Nivel e a Estrutura
            DataSet ds = cDados.incluiUnidadeNegocio(txtNome.Text.Trim().Replace("'", "''")
                                                    , txtSigla.Text.Trim().Replace("'", "''")
                                                    , false, 0, unidadeNegocioAtiva, idUsuarioLogado
                                                    , txtObservacoes.Text.Trim().Replace("'", "''")
                                                    , codigoEntidade, unidadeSuperior, ref codigoUnidadeNegocio
                                                    , codigoGerente
                                                    , txtCodigo.Text.Replace("'", "''")


                                                    , ref msg);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUnidadeNegocio = int.Parse(ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());

                //--------------------------Cargo a imagen da LogoMarca
                if (hfGeral.Get("NomeArquivo").ToString() != "")
                {
                    string pathImg = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + hfGeral.Get("NomeArquivo");
                    StreamReader img = new StreamReader(pathImg);
                    Byte[] stream = StreamToByteArray(img.BaseStream);

                    ////verifica o tamanho da imagem
                    int tamanhoImagem = stream.Length;

                    byte[] imagemBinario = stream;

                    //pra cargar imagen no banco, se faz uso do pasaje de parametros, de ese jeito, permite
                    //indicar o tipo de dado qeu se esta pasando.
                    cDados.atualizaLogoUnidade(codigoUnidadeNegocio, imagemBinario);
                }

                carregaGrid();
                carregaGerente();
                //carregaUnidadeSuperior("");
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUnidadeNegocio);
                gvDados.ClientVisible = false;
            }
            return msg;
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        //Aqui estou declarando e inicializando as variaveis que irei usar
        string msg = "";
        char unidadeNegocioAtiva = checkUnidade.Checked ? 'S' : 'N';
        int nivelHierarquico = 0;
        int codigoUnidadeNegocio = 0;
        int? unidadeSuperior = null;
        int codigoGerente = 0;
        if (ddlGerente.SelectedIndex != -1)
            codigoGerente = int.Parse(ddlGerente.Value.ToString());
        bool podeInativarExcluirUnidade = true;
        DataSet ds;






        try
        {
            codigoUnidadeNegocio = int.Parse(getChavePrimaria());

            string estadoNoBanco = gvDados.GetRowValues(gvDados.FocusedRowIndex, "IndicaUnidadeNegocioAtiva").ToString();
            if (estadoNoBanco.Equals("S") && !checkUnidade.Checked)
                verificarPodeInativarExcluirUnidade(codigoUnidadeNegocio, ref podeInativarExcluirUnidade);

            if (podeInativarExcluirUnidade)
            {

                if (ddlUnidadeSuperior.Value != null && ddlUnidadeSuperior.Value.ToString() != "-1")
                    unidadeSuperior = int.Parse(ddlUnidadeSuperior.Value.ToString());

                if (codigoUnidadeNegocio == codigoEntidade)
                    nivelHierarquico = 1;
                else
                {
                    if (ddlUnidadeSuperior.SelectedIndex != -1 && ddlUnidadeSuperior.Value.ToString() != "-1")
                    {
                        ds = cDados.getEstruturaHierarquicaPai(int.Parse(ddlUnidadeSuperior.Value.ToString()), "");
                        nivelHierarquico = (int.Parse(ds.Tables[0].Rows[0]["NivelHierarquicoUnidade"].ToString()) + 1);
                    }
                    else
                        nivelHierarquico = 1;
                }

                ds = cDados.atualizaUnidadeNegocio(txtNome.Text.Replace("'", "''"),
                                                   txtSigla.Text.Replace("'", "''"),
                                                   idUsuarioLogado, nivelHierarquico, unidadeNegocioAtiva,
                                                   txtObservacoes.Text.Replace("'", "''"),
                                                   codigoUnidadeNegocio, unidadeSuperior, codigoGerente,
                                                   txtCodigo.Text.Replace("'", "''"),
                                                   ref msg);

                if (cDados.DataSetOk(ds))
                {

                    //Stream imagem = LogoUpload.UploadedFiles[0].PostedFile.InputStream;
                    if (hfGeral.Get("NomeArquivo").ToString() != "")
                    {
                        string pathImg = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + hfGeral.Get("NomeArquivo");
                        StreamReader img = new StreamReader(pathImg);
                        Byte[] stream = StreamToByteArray(img.BaseStream);

                        ////verifica o tamanho da imagem
                        int tamanhoImagem = stream.Length;

                        byte[] imagemBinario = stream;

                        //pra cargar imagen no banco, se faz uso do pasaje de parametros, de ese jeito, permite
                        //indicar o tipo de dado qeu se esta pasando.
                        cDados.atualizaLogoUnidade(codigoUnidadeNegocio, imagemBinario);
                    }

                    carregaGrid();
                    carregaGerente();
                    //carregaUnidadeSuperior("");
                    gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUnidadeNegocio);
                    gvDados.ClientVisible = false;
                }
            } // ... if (podeInativarExcluirUnidade) ...
            else
            {
                throw new Exception(Resources.traducao.unidadesNegocio_erro_ao_inativar_o_dado__n_o_foi_poss_vel_ja_que_a_unidade_possui_refer_ncias);
            }
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = ex.Message;
        }

        return msg;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        string msg = "";
        string chave = getChavePrimaria();
        string definicaoEntidade = "";
        int regAfetados = 0;
        bool podeInativarExcluirUnidade = false;

        try
        {
            DataSet ds = cDados.getDefinicaoEntidade(codigoEntidade);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                definicaoEntidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
            }

            if (int.Parse(chave) == codigoEntidade)
                return string.Format(Resources.traducao.unidadesNegocio_a__0__n_o_pode_ser_exclu_da_pois_ela___a__1__pai_de_todas_, definicaoUnidade, definicaoEntidade);

            verificarPodeInativarExcluirUnidade(int.Parse(chave), ref podeInativarExcluirUnidade);

            if (podeInativarExcluirUnidade)
            {
                cDados.excluiUnidadeNegocio(int.Parse(chave), idUsuarioLogado, ref regAfetados, ref msg);
                carregaGrid();
                msg = msg.Replace("unidades", definicaoUnidadePlural);
                msg = msg.Replace("unidade", definicaoUnidade);
            }
            else
            {
                throw new Exception(Resources.traducao.unidadesNegocio_erro_ao_excluir_a_unidade_de_neg_cio__esta_unidade_de_neg_cio___superior_a_alguma_j__existente_conforme_apresentado_na_tabela_);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private void verificarPodeInativarExcluirUnidade(int codigoUnidadeAtual, ref bool podeInativarExcluirUnidade)
    {
        //getPodeDesativarUsuario
        limpaGridImpedimento();

        DataSet ds = cDados.getPodeExcluirDesativarUnidadeNegocio(codigoUnidadeAtual);
        DataTable dt = DataTableGridImpedimento();
        DataRow newRow;
        podeInativarExcluirUnidade = true;

        if (cDados.DataSetOk(ds))
        {
            if (cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeRecursoCorporativo"].ToString();
                    newRow["codImpedimento"] = "RC";
                    dt.Rows.Add(newRow);
                }
                podeInativarExcluirUnidade = false;
            }
            if (cDados.DataTableOk(ds.Tables[1]))
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeUnidadeNegocio"].ToString();
                    newRow["codImpedimento"] = "UN";
                    dt.Rows.Add(newRow);
                }
                podeInativarExcluirUnidade = false;
            }
            if (cDados.DataTableOk(ds.Tables[2]))
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["TituloMapaEstrategico"].ToString();
                    newRow["codImpedimento"] = "ME";
                    dt.Rows.Add(newRow);
                }
                podeInativarExcluirUnidade = false;
            }
            if (cDados.DataTableOk(ds.Tables[3]))
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "PP";
                    dt.Rows.Add(newRow);
                }
                podeInativarExcluirUnidade = false;
            }
            if (cDados.DataTableOk(ds.Tables[4]))
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["NomeProjeto"].ToString();
                    newRow["codImpedimento"] = "DE";
                    dt.Rows.Add(newRow);
                }
                podeInativarExcluirUnidade = false;
            }
            if (cDados.DataTableOk(ds.Tables[5]))
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    newRow = dt.NewRow();
                    newRow["NomeUnidade"] = dr["DescricaoObjetoContrato"].ToString();
                    newRow["codImpedimento"] = "CO";
                    dt.Rows.Add(newRow);
                }
                podeInativarExcluirUnidade = false;
            }

            gvImpedimentos.DataSource = dt;
            gvImpedimentos.DataBind();
        }
    }

    private void limpaGridImpedimento()
    {
        gvImpedimentos.DataSource = null;
        gvImpedimentos.DataBind();
    }

    private DataTable DataTableGridImpedimento()
    {
        DataTable dtResult = new DataTable();
        DataColumn NewColumn = null;

        NewColumn = new DataColumn("NomeUnidade", Type.GetType("System.String"));
        NewColumn.Caption = "Unidade";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        NewColumn = new DataColumn("codImpedimento", Type.GetType("System.String"));
        NewColumn.Caption = "codImpedimento";
        NewColumn.ReadOnly = false;
        dtResult.Columns.Add(NewColumn);

        return dtResult;
    }

    #endregion

    protected void LogoUpload_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        string dataHora = dataAtual + e.UploadedFile.FileName;
        byte[] imagem = e.UploadedFile.FileBytes;

        e.CallbackData = e.UploadedFile.FileName;

        string arquivo = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + e.UploadedFile.FileName;
        FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
        fs.Write(imagem, 0, imagem.Length);
        fs.Close();
    }

    public static byte[] StrToByteArray(string str)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        return encoding.GetBytes(str);
    }

    protected Byte[] StreamToByteArray(Stream stream)
    {
        Int32 streamLength = (Int32)stream.Length;
        Byte[] byteArray = new Byte[streamLength];
        stream.Read(byteArray, 0, streamLength);
        stream.Close();

        return byteArray;
    }

    protected void pnLogo_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();

        if (parametro == "Limpar")
        {
            imageLogo.ContentBytes = null;
        }
        else
        {
            if (parametro == "SE")
            {
                string pathImg = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + hfGeral.Get("DataAtual") + hfGeral.Get("NomeArquivo");
                StreamReader img = new StreamReader(pathImg);
                Byte[] stream = StreamToByteArray(img.BaseStream);
                imageLogo.ContentBytes = stream;
            }
            else
            {
                if (hfGeral.Contains("CodigoUnidade"))
                {
                    DataSet dsLogo = cDados.getLogoEntidade(int.Parse(hfGeral.Get("CodigoUnidade").ToString()), "");
                    if (cDados.DataSetOk(dsLogo) && cDados.DataTableOk(dsLogo.Tables[0]))
                    {
                        try
                        {
                            imageLogo.ContentBytes = (byte[])dsLogo.Tables[0].Rows[0]["LogoUnidadeNegocio"];
                        }
                        catch { }
                    }
                }
            }
        }
    }
    
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

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

    protected void gvDados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }


    protected void callback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string[] strParametro = e.Parameter.Split(';');

        bool retorno = false;

        int codigoUnidadeNegocio = int.Parse(strParametro[0]);
        string latitude = strParametro[1];
        string longitude = strParametro[2];

        string mensagemErro = "";
        try
        {
            string comandoSQL = string.Format(@"
            UPDATE {0}.{1}.UnidadeNegocio
               SET Latitude = {2}
                  ,Longitude = {3}
             WHERE CodigoUnidadeNegocio = {4}", cDados.getDbName(), cDados.getDbOwner(), latitude, longitude, codigoUnidadeNegocio);
            int registrosAfetados = 0;
            retorno = cDados.classeDados.execSQL(comandoSQL, ref registrosAfetados);

        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }

        callback1.JSProperties.Add("cp_Sucesso", (retorno == true) ? "S" : "N");
        callback1.JSProperties.Add("cp_Erro", mensagemErro);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadUnNeg");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClick_CustomIncluirUnidade();", true, true, false, "CadUnNeg", Resources.traducao.unidadesNegocio_cadastro_de_ + definicaoUnidadePlural, this);
    }

    #endregion

    protected void ddlGerenteProjeto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidade);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }

    }

    protected void ddlGerenteProjeto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string filtro = "";

        //if (!IsPostBack && ddlGerenteProjeto.Value != null)
        //    filtro = ddlGerenteProjeto.Text;
        //else
        filtro = e.Filter.ToString();


        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidade, filtro, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }
}
