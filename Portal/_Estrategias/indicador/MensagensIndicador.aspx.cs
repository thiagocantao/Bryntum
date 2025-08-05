/*
 * OBSERVAÇÕES
 * 
 * Arquivos criados no 14/03/2011
 * 
 * MensagensIndicador.aspx - MensagensIndicador.aspx.cs - MensagensIndicador.js
 * 
 * Recibe os siguentes parâmetros via resquest:
 * 
 *  COIN :: codigoIndicador :
 *  COE  :: codigoObjetivo  :
 * 
 * 
 * MODIFICAÇÕES
 * 
 * 15/03/2011 :: Alejandro : Definir as permissão de usuario com respeito de si pode o nao alterar mensagens 
 *                              [private void definePermissoesUsuario()]
 * 30/03/2011 :: Alejandro : Atulaização das permissões [IN_IncMsg].
 * 04/03/2011 :: Alejandro : Permissões da tela [IN_CnsMsg]
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

public partial class _Estrategias_indicador_MensagensIndicador : System.Web.UI.Page
{
    dados cDados;

    public bool IncluiMsg = false;
    public bool EditarMsg = false;
    public bool RespMesg = false;
    public bool ConsMesg = false;

    private int codigoIndicador = 0;
    private int codigoObjetivo = 0;
    private int idObjetoPai = 0;
    private int idUsuarioLogado = -1;
    private int idUnidadeLogada = -1;

    public int alturaGrid = 0;

    private string possoResponder = "";

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

        idUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        codigoIndicador = cDados.getInfoSistema("COIN") != null ? int.Parse(cDados.getInfoSistema("COIN").ToString()) : -1;
        codigoObjetivo = cDados.getInfoSistema("COE") != null ? int.Parse(cDados.getInfoSistema("COE").ToString()) : -1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();
        definePermissoesUsuario();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, idUnidadeLogada, codigoIndicador, "null", "IN", 0, "null", "IN_CnsMsg");
        
            if (!ConsMesg)
                cDados.RedirecionaParaTelaSemAcesso(this);
        }

        montaCampos();
        carregaGrid();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/MensagensIndicador.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "MensagensIndicador", "_Strings"));
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        if(alturaPrincipal>0)
            gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 360;
    }

    private void definePermissoesUsuario()
    {
        ////--------Permissao de Riscos e Questoes
        //IncluiMsg = cDados.VerificaPermissaoUsuario(idUsuarioLogado, idUnidadeLogada, codigoIndicador, "NULL", "IN", 0, "NULL", "IN_CnsMsg");
        //EditarMsg = cDados.VerificaPermissaoUsuario(idUsuarioLogado, idUnidadeLogada, codigoIndicador, "NULL", "IN", 0, "NULL", "IN_EdtMsg");
        //RespMesg = cDados.VerificaPermissaoUsuario(idUsuarioLogado, idUnidadeLogada, codigoIndicador, "NULL", "IN", 0, "NULL", "IN_RptMsg");

        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, idUnidadeLogada, codigoIndicador, idObjetoPai, "IN", "IN_CnsMsg", "IN_IncMsg", "IN_EdtMsg", "IN_RptMsg");
        if (cDados.DataSetOk(ds))
        {
            IncluiMsg = int.Parse(ds.Tables[0].Rows[0]["IN_IncMsg"].ToString()) > 0;
            EditarMsg = int.Parse(ds.Tables[0].Rows[0]["IN_EdtMsg"].ToString()) > 0;
            RespMesg = int.Parse(ds.Tables[0].Rows[0]["IN_RptMsg"].ToString()) > 0;
            ConsMesg = int.Parse(ds.Tables[0].Rows[0]["IN_CnsMsg"].ToString()) > 0;
        }
    }

    #endregion

    #region GRIDVIEW

    private void carregaGrid()
    {
        string enviei = (ckbEnviei.Checked == true) ? "S" : "N";
        possoResponder = (ckbRespondo.Checked == true) ? "S" : "N";
        string erro = "";
        string status = "Todas";
        if (ddlStatus.SelectedItem != null)
        {
            status = ddlStatus.SelectedItem.Value.ToString();
        }
        //alterar ess função para que ela também receba como parâmetro o tipo de associação

        DataSet ds = cDados.getMensagemObjeto("IN", idUsuarioLogado, codigoIndicador, status, enviei, possoResponder, "", ref erro);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        try
        {
            string respNecessaria = gvDados.GetRowValues(e.VisibleIndex, "IndicaRespostaNecessaria").ToString();
            string podeEditarResposta = gvDados.GetRowValues(e.VisibleIndex, "EditaResposta").ToString();
            string podeEditarMensagem = gvDados.GetRowValues(e.VisibleIndex, "EditaMensagem").ToString();
            string podeExcluirMensagem = gvDados.GetRowValues(e.VisibleIndex, "ExcluiMensagem").ToString();

            if (e.ButtonID == "btnExcluirCustom")
            {
                if (podeExcluirMensagem == "S")
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Text = "Excluir";
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

    #region BANCO DE DADOS

    private void montaCampos()
    {
        string where = " AND oe.CodigoObjetoEstrategia = " + codigoObjetivo;

        DataTable dtCampos = cDados.getDadosIndicador(codigoIndicador, where).Tables[0];

        if (cDados.DataTableOk(dtCampos))
        {
            txtMapa.Text = dtCampos.Rows[0]["TituloMapaEstrategico"].ToString();
            txtObjetivoEstrategico.Text = dtCampos.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtIndicador.Text = dtCampos.Rows[0]["NomeIndicador"].ToString();
        }
    }

    private string getChavePrimaria()
    {
        return gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoMensagem").ToString();
    }

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

        cDados.editaMensagemProjeto(codMensagem, idUnidadeLogada, codigoIndicador, idUsuarioLogado, txtMensagem.Text.Trim().Replace("'", "''"), txtResposta.Text.Trim().Replace("'", "''"), ref erro);

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

    #endregion

    #region CALLBACK's

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Responder")         mensagemErro_Persistencia = persisteRespondeRegistro();
        else if (e.Parameter == "Editar")       mensagemErro_Persistencia = persisteEdicaoRegistro();
        else if (e.Parameter == "Excluir")      mensagemErro_Persistencia = persisteExclusaoRegistro();
        else if (e.Parameter == "CarregarGrid") carregaGrid();

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    protected void pnBotaoResponder_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        /*A verificação se o usuario que incluiu é o mesmo que leu a mensagem já foi feita no javascript*/
        string respNecessaria = gvDados.GetRowValues(gvDados.FocusedRowIndex, "IndicaRespostaNecessaria").ToString();
        string podeEditarResposta = gvDados.GetRowValues(gvDados.FocusedRowIndex, "EditaResposta").ToString();
        string podeEditarMensagem = gvDados.GetRowValues(gvDados.FocusedRowIndex, "EditaMensagem").ToString();
        string podeExcluirMensagem = gvDados.GetRowValues(gvDados.FocusedRowIndex, "ExcluiMensagem").ToString();
        string dataResposta = gvDados.GetRowValues(gvDados.FocusedRowIndex, "DataResposta").ToString();

        if (e.Parameter.Equals("Salvar"))
        {
            /*  verificar se é o usuário que incluiu a mensagem. 
                Se for, HABILITAR o campo MENSAGEM e DESABILITAR o campo RESPOSTA.*/
            btnSalvar.ClientVisible = true;
            btnResponder.ClientVisible = false;

            if (!dataResposta.Equals(""))
            {
                //se a mensagem ja tiver sido respondida
                //nao editar nem a mensagem nem a resposta
                txtMensagem.ClientEnabled = false;
                txtResposta.ClientEnabled = false;
                btnSalvar.ClientEnabled = false;
            }
            else
            {
                txtMensagem.ClientEnabled = (podeEditarMensagem.Equals("S")) && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
                txtResposta.ClientEnabled = false;
                btnSalvar.ClientEnabled = (podeEditarMensagem.Equals("S")) && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
            }
        }
        else
        {
            /*Se não for o usuário que incluiu a mensagem, 
              a mensagem estiver marcada com RESPOSTA NECESSÁRIA 
              e o usuário TIVER PERMISSÃO para RESPONDER MENSAGEM, 
              desabilitar o campo MENSAGEM e HABILITAR o campo RESPOSTA.*/
            btnSalvar.ClientVisible = false;
            btnResponder.ClientVisible = true;

            if (!txtResposta.Text.Equals(""))
            {
                txtMensagem.ClientEnabled = false;
                txtResposta.ClientEnabled = false;
                btnResponder.ClientEnabled = false;
            }
            else if (podeEditarMensagem.Equals("S"))
            {
                btnSalvar.ClientVisible = true;
                btnResponder.ClientVisible = false;
                txtResposta.ClientEnabled = false;
                txtMensagem.ClientEnabled = (podeEditarMensagem.Equals("S")) && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
            }
            else
            {
                txtMensagem.ClientEnabled = false;
                txtResposta.ClientEnabled = RespMesg && (respNecessaria.Equals("S")) && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
                btnResponder.ClientEnabled = RespMesg && (respNecessaria.Equals("S")) && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
            }
        }
    }

    #endregion
}