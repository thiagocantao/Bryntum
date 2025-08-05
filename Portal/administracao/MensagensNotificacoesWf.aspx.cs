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

public partial class _Projetos_DadosProjeto_editaMensagens : System.Web.UI.Page
{
    dados cDados;
    DataSet dsPermissao = new DataSet();
    public bool IncluiMsg;
    private int idUsuarioLogado;    
    private string resolucaoCliente = "";    
    private int codigoEntidade = -1;  
    private int alturaPrincipal = 0;

    private int codigoWorkflow = 0;
    private int codigoEtapaWf = 0;
    private int codigoInstanciaWf = 0;
    private int sequencia = 0;

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
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        cDados.aplicaEstiloVisual(Page);    

        codigoWorkflow = int.Parse(string.IsNullOrEmpty(Request.QueryString["CWF"]) ? "-1" : Request.QueryString["CWF"]);
        codigoEtapaWf = int.Parse(string.IsNullOrEmpty(Request.QueryString["CEWF"]) ? "-1" : Request.QueryString["CEWF"]);
        codigoInstanciaWf = int.Parse(string.IsNullOrEmpty(Request.QueryString["CIWF"]) ? "-1" : Request.QueryString["CIWF"]);
        sequencia = int.Parse(string.IsNullOrEmpty(Request.QueryString["CSOWF"]) ? "-1" : Request.QueryString["CSOWF"]);
        IncluiMsg = Request.QueryString["RO"] + "" != "S";

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());  

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();        
                
        defineAlturaTela(resolucaoCliente);

        carregaGrid();
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        
        int altura = (alturaPrincipal - 135);
        
        if (altura > 0) 
        {
            //if (gvDados.Settings.VerticalScrollableHeight < (altura - 385))
            //{
                gvDados.Settings.VerticalScrollableHeight = altura - 385;
            //}

            lbDisponiveis.Height = altura - 400;
            lbSelecionados.Height = altura - 400;
            txtMensagem.Height = altura - 500;
        }
    }

    private void carregaGrid()
    {
        string where = "";

        DataSet ds = cDados.getNotificacoesWf(codigoWorkflow, codigoInstanciaWf, codigoEtapaWf, where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];

            gvDados.DataBind();
        }
    }

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        pnCallback.JSProperties["cp_Msg"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "")
        {
            pnCallback.JSProperties["cp_Msg"] = "Mensagem salva com sucesso!";
            hfGeral.Set("StatusSalvar", "1");
        }
        else
        {
            hfGeral.Set("ErroSalvar", "");
            pnCallback.JSProperties["cp_Msg"] = mensagemErro_Persistencia;
        }

        carregaGrid();
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        //-- compilação dos dados para Inserir.
        string descricaoResumida = txtAssunto.Text.Replace("'", "''");
        string mensagem = txtMensagem.Text.Replace("'", "''").Replace("\n", Environment.NewLine);

        string[] arrayParticipantesSelecionados = hfGeral.Get("CodigosSelecionados").ToString().Split(';');

        string codigoNovaNotificacao = "";
        string mesgError = "";

        try
        {
            bool result = cDados.incluiNotificacao(codigoWorkflow, codigoInstanciaWf, codigoEtapaWf, sequencia, descricaoResumida, mensagem, ref codigoNovaNotificacao, ref mesgError);

            if (result == false)
            {
                return mesgError;
            }
            else
            {
                if (arrayParticipantesSelecionados.Length > 0)
                    cDados.incluiUsuariosNotificacaoWf(arrayParticipantesSelecionados, codigoNovaNotificacao, descricaoResumida, mensagem, codigoEntidade);                
                                
                carregaGrid();

                int index = gvDados.FindVisibleIndexByKeyValue(codigoNovaNotificacao);

                gvDados.FocusedRowIndex = index;
                gvDados.ScrollToVisibleIndexOnClient = index;
                return "";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #region LISTBOX

    private void carregaListaParticipantes(string codigoNotificacaoWf)
    {
        string where = string.Format(" AND u.DataExclusao IS NULL AND u.CodigoUsuario NOT IN(SELECT IdentificadorRecurso FROM {0}.{1}.NotificacoesRecursosWf WHERE CodigoNotificacaoWf = {2})", cDados.getDbName(), cDados.getDbOwner(), codigoNotificacaoWf);
        DataSet ds = cDados.getUsuarioDaEntidadeAtiva(codigoEntidade.ToString(), where);

        lbDisponiveis.DataSource = ds;
        lbDisponiveis.TextField = "NomeUsuario";
        lbDisponiveis.ValueField = "CodigoUsuario";
        lbDisponiveis.DataBind();

        if (lbDisponiveis.Items.Count > 0)
            lbDisponiveis.SelectedIndex = -1;
    }

    protected void lbDisponiveis_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            string codigoNotificacaoWf = e.Parameter;
            carregaListaParticipantes(codigoNotificacaoWf);
        }
    }

    private void carregaListaParticipantesAssociados(string codigoNotificacaoWf)
    {
        string where = string.Format(" AND u.DataExclusao IS NULL AND u.CodigoUsuario IN(SELECT IdentificadorRecurso FROM {0}.{1}.NotificacoesRecursosWf WHERE CodigoNotificacaoWf = {2})", cDados.getDbName(), cDados.getDbOwner(), codigoNotificacaoWf);
        DataSet ds = cDados.getUsuarioDaEntidadeAtiva(codigoEntidade.ToString(), where);

        lbSelecionados.DataSource = ds.Tables[0];
        lbSelecionados.TextField = "NomeUsuario";
        lbSelecionados.ValueField = "CodigoUsuario";
        lbSelecionados.DataBind();

        if (lbSelecionados.Items.Count > 0)
            lbSelecionados.SelectedIndex = -1;
    }

    protected void lbSelecionados_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            string codigoNotificacaoWf = e.Parameter;
            carregaListaParticipantesAssociados(codigoNotificacaoWf);
        }
    }

    #endregion    
}
