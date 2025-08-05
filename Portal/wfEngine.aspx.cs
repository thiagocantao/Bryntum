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
using CDIS;

public partial class wfEngine : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela;
    public string parametrosURL;
    public string modeloProposta;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        int codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        string iniciaisFluxo = Request.QueryString["IF"] == null ? "" : Request.QueryString["IF"].ToString().ToUpper();


        parametrosURL = "?" + Request.QueryString.ToString();

        if (iniciaisFluxo == "PRPINI")
        {
            int codigoFluxoNovaProposta, codigoWFNovaProposta;
            cDados.getCodigoWfAtualFluxoNovaProposta(codigoEntidadeUsuarioResponsavel, out codigoFluxoNovaProposta, out codigoWFNovaProposta);
            parametrosURL += "&CF=" + codigoFluxoNovaProposta.ToString() + "&CW=" + codigoWFNovaProposta.ToString();
            modeloProposta = " [Projeto]";
            if (!IsPostBack)
            {

                cDados.setInfoSistema("CodigoProjeto", null);

                DataSet dsPermissao = cDados.getIniciaisPermissaoWf(iniciaisFluxo);

                if (cDados.DataSetOk(dsPermissao) && cDados.DataTableOk(dsPermissao.Tables[0]))
                {
                    string iniciaisPermissao = dsPermissao.Tables[0].Rows[0]["IniciaisPermissao"].ToString();
                    cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", iniciaisPermissao);
                }
                else
                {
                    // se não encontrar nenhuma permissão relacionada, redireciona para a tela sem acesso.
                    cDados.RedirecionaParaTelaSemAcesso(this);
                }
            }
        }
        else if (iniciaisFluxo == "PRCINI")
        {
            int codigoFluxoNovaProposta, codigoWFNovaProposta;
            cDados.getCodigoWfAtualFluxoNovaPropostaProcesso(codigoEntidadeUsuarioResponsavel, out codigoFluxoNovaProposta, out codigoWFNovaProposta);
            parametrosURL += "&CF=" + codigoFluxoNovaProposta.ToString() + "&CW=" + codigoWFNovaProposta.ToString();

            if (!IsPostBack)
            {

                cDados.setInfoSistema("CodigoProjeto", null);

                DataSet dsPermissao = cDados.getIniciaisPermissaoWf(iniciaisFluxo);
                modeloProposta = " [Processo]";
                if (cDados.DataSetOk(dsPermissao) && cDados.DataTableOk(dsPermissao.Tables[0]))
                {
                    string iniciaisPermissao = dsPermissao.Tables[0].Rows[0]["IniciaisPermissao"].ToString();
                    cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", iniciaisPermissao);
                }
                else
                {
                    // se não encontrar nenhuma permissão relacionada, redireciona para a tela sem acesso.
                    cDados.RedirecionaParaTelaSemAcesso(this);
                }
            }
        }
        else if (iniciaisFluxo.Length>0)
        {
            int codigoFluxo, codigoWorkflow, codigoEtapaInicial;
            cDados.getCodigoWfAtualPorIniciaisFluxo(codigoEntidadeUsuarioResponsavel, out codigoFluxo, out codigoWorkflow, out codigoEtapaInicial, iniciaisFluxo);
            parametrosURL += "&CF=" + codigoFluxo.ToString() + "&CW=" + codigoWorkflow.ToString();

            if (!IsPostBack)
            {

                cDados.setInfoSistema("CodigoProjeto", null);

                DataSet dsPermissao = cDados.getIniciaisPermissaoWf(iniciaisFluxo);
                modeloProposta = " [Projeto]";   // ???
                if (cDados.DataSetOk(dsPermissao) && cDados.DataTableOk(dsPermissao.Tables[0]))
                {
                    string iniciaisPermissao = dsPermissao.Tables[0].Rows[0]["IniciaisPermissao"].ToString();
                    cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", iniciaisPermissao);
                }
                else
                {
                    // se não encontrar nenhuma permissão relacionada, redireciona para a tela sem acesso.
                    cDados.RedirecionaParaTelaSemAcesso(this);
                }
            }
        }

        alturaTela = getAlturaTela() + "px";

        parametrosURL += "&Altura=" + (int.Parse(getAlturaTela()) - 115);

        if (!IsPostBack)
        {
            int nivel = 1;

            if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
                nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
        }
        this.Title = cDados.getNomeSistema();
    }

    private string getAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 150).ToString();
    }
}
