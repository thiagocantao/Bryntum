/*
 * OBSERVAÇÕES
 * 
 * 
 * MUDANÇAS
 * 
 * 15/03/2011 :: Alejandro :- Alteração dos parâmetros que envia dados ao banco. Se utilizo o metodo 'Replace', para
 *                              evitar error con algum caracteres e a sentencia SQL.
 *                          - Definir as permissão de usuario com respeito de si pode o nao alterar mensagens 
 *                              [private void definePermissoesUsuario()].
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

public partial class _Estrategias_objetivoEstrategico_mensagensObjetivoEstrategico : System.Web.UI.Page
{
    dados cDados;
    DataSet dsPermissao = new DataSet();
    
    public bool IncluiMsg = false;
    public bool EditarMsg = false;
    public bool RespMesg = false;

    private int idUsuarioLogado;
    private int codigoUnidadeLogada = -1, codigoUnidadeSelecionada = 0;
    private int idObjetoPai = 0;
    private int codigoObjetivoEstrategico = -1;
    private int alturaPrincipal = 0;
    private int codigoTipoAssociacao = -1;
    
    private string resolucaoCliente = "";
    private string possoResponder = "";
    private string iniciaisTipoAssociacao = ""; 
    public string nomeProjeto = "";

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

        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        if (Request.QueryString["COE"] != null)
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
        if (Request.QueryString["NMP"] != null)
            pcDados.HeaderText = "Detalhes " + Request.QueryString["NMP"].ToString();
        if (Request.QueryString["TA"] != null)
        {
            iniciaisTipoAssociacao = Request.QueryString["TA"].ToString();
            codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(iniciaisTipoAssociacao);
        }

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        hfGeral.Set("hfNomeObjetivo", Request.QueryString["NMP"] + "");
        hfGeral.Set("hfCodigoObjetivo", Request.QueryString["COE"] + "");
        hfGeral.Set("hfIdUsuarioLogado", idUsuarioLogado);

        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        definePermissoesUsuario();

        if (!IsPostBack)
        {
            if (!IncluiMsg)
                cDados.RedirecionaParaTelaSemAcesso(this);

            cDados.aplicaEstiloVisual(Page);
            if (Request.QueryString["Status"] != null && Request.QueryString["Status"].ToString().Equals("NR"))
                ddlStatus.SelectedIndex = 2;
        }

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        defineAlturaTela(resolucaoCliente);
        
        carregaGrid();
        carregaCampos();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        ASPxWebControl.RegisterBaseScript(this.Page);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/editaMensagens.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings", "editaMensagens"));
    }

    private void carregaCampos()
    {
        DataTable dt = cDados.getObjetivoEstrategico(null, codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtResponsavel.Text = dt.Rows[0]["NomeUsuario"].ToString();
            txtTema.Text = dt.Rows[0]["Tema"].ToString();
        }
        else
        {
            txtPerspectiva.Text = "";
            txtObjetivoEstrategico.Text = "";
            txtMapa.Text = "";
            txtResponsavel.Text = "";
            txtTema.Text = "";
        }
    }

    private void definePermissoesUsuario()
    {
        //--------Permissao de Riscos e Questoes
        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, idObjetoPai, "OB", "OB_CnsMsg", "OB_EdtMsg", "OB_RptMsg");
        if (cDados.DataSetOk(ds))
        {
            IncluiMsg = int.Parse(ds.Tables[0].Rows[0]["OB_CnsMsg"].ToString()) > 0;
            EditarMsg = int.Parse(ds.Tables[0].Rows[0]["OB_EdtMsg"].ToString()) > 0;
            RespMesg = int.Parse(ds.Tables[0].Rows[0]["OB_RptMsg"].ToString()) > 0;
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);

        if (altura > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = altura - 190;
        }
    }

    #endregion

    #region GVDADOS

    private void carregaGrid()
    {
        possoResponder = (ckbRespondo.Checked == true) ? "S" : "N";
        string enviei = (ckbEnviei.Checked == true) ? "S" : "N";
        string erro = "";
        string status = "Todas";

        if (ddlStatus.SelectedItem != null)
        {
            status = ddlStatus.SelectedItem.Value.ToString();
        }
        //alterar ess função para que ela também receba como parâmetro o tipo de associação

        DataSet ds = cDados.getMensagemObjeto("OB", idUsuarioLogado, codigoObjetivoEstrategico, status
                                                , enviei, possoResponder, "", ref erro);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        try
        {
            string respNecessaria = gvDados.GetRowValues(e.VisibleIndex, "IndicaRespostaNecessaria").ToString();
            string podeEditarResposta = gvDados.GetRowValues(e.VisibleIndex, "EditaResposta").ToString();
            string podeEditarMensagem = gvDados.GetRowValues(e.VisibleIndex, "EditaMensagem").ToString();
            string podeExcluirMensagem = gvDados.GetRowValues(e.VisibleIndex, "ExcluiMensagem").ToString();

            if (e.ButtonID == "btnExcluirCustom")
            {
                if (!podeExcluirMensagem.Equals("S"))
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            if (e.ButtonID == "btnEditarCustom")
            {
                //todo: OBSERVAÇÃO: verificar las permissões
                if (EditarMsg && (podeEditarMensagem == "S" || podeEditarResposta == "S"))
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Text = "Editar";
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region  CALLBACK

    protected void pnBotaoResponder_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        /*A verificação se o usuario que incluiu é o mesmo que leu a mensagem já foi feita no javascript*/
        string respNecessaria = gvDados.GetRowValues(gvDados.FocusedRowIndex, "IndicaRespostaNecessaria").ToString();
        string podeEditarResposta = gvDados.GetRowValues(gvDados.FocusedRowIndex, "EditaResposta").ToString();
        string podeEditarMensagem = gvDados.GetRowValues(gvDados.FocusedRowIndex, "EditaMensagem").ToString();
        string podeExcluirMensagem = gvDados.GetRowValues(gvDados.FocusedRowIndex, "ExcluiMensagem").ToString();
        string dataResposta = gvDados.GetRowValues(gvDados.FocusedRowIndex, "DataResposta").ToString();

        if (e.Parameter == "Salvar")
        {
            /*  verificar se é o usuário que incluiu a mensagem. 
                Se for, HABILITAR o campo MENSAGEM e DESABILITAR o campo RESPOSTA.*/

            btnSalvar.ClientVisible = true;
            btnResponder.ClientVisible = false;

            if (dataResposta != "")
            {
                //se a mensagem ja tiver sido respondida
                //nao editar nem a mensagem nem a resposta
                txtMensagem.ClientEnabled = false;
                txtResposta.ClientEnabled = false;
                btnSalvar.ClientEnabled = false;
            }
            else
            {
                txtMensagem.ClientEnabled = (podeEditarMensagem == "S") && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
                txtResposta.ClientEnabled = false;
                btnSalvar.ClientEnabled = (podeEditarMensagem == "S") && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
            }
        }
        else
        {
            /*Se não for o usuário que incluiu a mensagem, 
              a mensagem estiver marcada com RESPOSTA NECESSÁRIA 
              e o usuário TIVER PERMISSÃO para RESPONDER MENSAGEM, 
              desabilitar o campo MENSAGEM e HABILITAR o campo RESPOSTA.*/

            btnSalvar.ClientVisible = false;
            btnResponder.ClientVisible = RespMesg;
           if (txtResposta.Text != "")
            {
                txtMensagem.ClientEnabled = false;
                txtResposta.ClientEnabled = false;
                btnResponder.ClientEnabled = false;
            }
            else
            {
                txtMensagem.ClientEnabled = false;
                txtResposta.ClientEnabled = RespMesg && respNecessaria.Equals("S") && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
                btnResponder.ClientEnabled = RespMesg && respNecessaria.Equals("S") && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar"); //(respNecessaria == "S")
            }
        }
    }

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Responder")     mensagemErro_Persistencia = persisteRespondeRegistro();
        else if (e.Parameter == "Editar")   mensagemErro_Persistencia = persisteEdicaoRegistro();
        else if (e.Parameter == "Excluir")  mensagemErro_Persistencia = persisteExclusaoRegistro();
        
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

        carregaGrid();
    }

    #endregion

    #region BANCO DE DADOS

    private string persisteRespondeRegistro()
    {
        int codMensagem = int.Parse(hfGeral.Get("hfCodigoMensagem").ToString());
        string erro = "";
        bool retorno = false;

        retorno = cDados.respondeMensagemProjeto(codMensagem, txtResposta.Text.Trim().Replace("'", "''"), idUsuarioLogado, ref erro);

        carregaGrid();
        return erro;
    }

    private string persisteEdicaoRegistro()
    {
        int codMensagem = int.Parse(hfGeral.Get("hfCodigoMensagem").ToString());
        string erro = "";

        cDados.editaMensagemProjeto(codMensagem, codigoUnidadeSelecionada, codigoObjetivoEstrategico, idUsuarioLogado, txtMensagem.Text.Trim().Replace("'", "''"), txtResposta.Text.Trim().Replace("'", "''"), ref erro);

        carregaGrid();
        return erro;
    }

    private string persisteExclusaoRegistro()
    {
        int codMensagem = int.Parse(getChavePrimaria());
        string erro = "";

        bool retorno = cDados.excluiMensagemProjeto(codMensagem, ref erro);

        return erro;
    }

    private string getChavePrimaria()
    {
        return gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoMensagem").ToString();
    }

    #endregion
}
