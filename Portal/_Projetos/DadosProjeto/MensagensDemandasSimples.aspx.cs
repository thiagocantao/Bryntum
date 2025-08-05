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

public partial class MensagensDemandasSimples : System.Web.UI.Page
{
    dados cDados;
    DataSet dsPermissao = new DataSet();
    public bool IncluiMsg;
   // bool ExcluirMsg;


    
    private int idUsuarioLogado;
    
    private string resolucaoCliente = "";
    
    private int codigoEntidade = -1;
    
    private int codigoProjeto = -1;
    
    public string nomeProjeto = "";

    string possoResponder = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
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
        
        ASPxWebControl.RegisterBaseScript(this.Page);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            if ((Request.QueryString["Status"] != null) &&
                Request.QueryString["Status"].ToString() == "NR")
            {
                ddlStatus.SelectedIndex = 2;
            }
        }

        if(!IsCallback)
            pnCallback.HideContentOnCallback = false;

        if (Request.QueryString["IDProjeto"] != null)
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        definePermissoesUsuario();

        //if (!IsPostBack)
        //{
        //    cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CnsMsg");
        //}

        if (Request.QueryString["RESP"] != null)
        {
            ckbRespostaNecessaria.Checked = Request.QueryString["RESP"].ToString() == "S";
        }

        if (Request.QueryString["NMP"] != null)
        {
            pcDados.HeaderText = "Detalhes " + Request.QueryString["NMP"].ToString();
        }

        

        hfGeral.Set("hfIdUsuarioLogado", idUsuarioLogado);
        hfGeral.Set("CodigoProjeto", codigoProjeto);
	    hfGeral.Set("NomeProjeto", "");
        

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/editaMensagens.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "editaMensagens", "_Strings"));
        defineAlturaTela(resolucaoCliente);

        carregaGrid();
       

    }


    private void definePermissoesUsuario()
    {
        //IncluiMsg = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_IncMsg");
        //EditarMsg = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_EdtMsg");
        //RespMesg = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_RptMsg"); 
        IncluiMsg = true;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        gvDados.Settings.VerticalScrollableHeight = 360;
    }

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

        string where = "";
        
        if (ckbRespostaNecessaria.Checked)
        {
            where = " AND m.IndicaRespostaNecessaria = 'S'";
        }

        DataSet ds = cDados.getMensagemObjeto("DS", idUsuarioLogado, codigoProjeto, status, enviei
                                            , possoResponder, where, ref erro);
        
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];

            gvDados.DataBind();
        }
    }

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

            btnResponder.ClientVisible = true;

            if (txtResposta.Text != "")
            {
                txtMensagem.ClientEnabled = false;

                txtResposta.ClientEnabled = false;

                btnResponder.ClientEnabled = false;
            }
            else
            {
                txtMensagem.ClientEnabled = false;

                txtResposta.ClientEnabled = (respNecessaria == "S") && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");

                btnResponder.ClientEnabled = (respNecessaria == "S") && (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Consultar");
            }
        }
    }
    
    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        
        if (e.Parameter == "Responder")
        {
            
            mensagemErro_Persistencia = persisteRespondeRegistro();
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

        carregaGrid();
    }

    private string persisteRespondeRegistro()
    {
        int codMensagem = int.Parse(hfGeral.Get("hfCodigoMensagem").ToString());
        
        string erro = "";

        bool retorno = false;

        retorno = cDados.respondeMensagemProjeto(codMensagem, txtResposta.Text, idUsuarioLogado, ref erro);
        
        carregaGrid();
        
        return erro;
    }

    private string persisteEdicaoRegistro()
    {
        int codMensagem = int.Parse(hfGeral.Get("hfCodigoMensagem").ToString());
        
        string erro = "";

        cDados.editaMensagemProjeto(codMensagem, codigoEntidade, codigoProjeto, idUsuarioLogado, txtMensagem.Text, txtResposta.Text, ref erro);
        
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
    
    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }
    
    private string getChavePrimaria()
    {
        return gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoMensagem").ToString();
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
                if (podeExcluirMensagem == "S")
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            if (e.ButtonID == "btnEditarCustom")
            {
                if (podeEditarMensagem == "S" || podeEditarResposta == "S")
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }
        }
        catch (Exception)
        {
        }
    }
}
