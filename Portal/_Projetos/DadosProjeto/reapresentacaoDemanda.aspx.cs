using DevExpress.Web;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_reapresentacaoDemanda : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool podeIncluir = true;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        headerOnTela();

        if (Request.QueryString["IDProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        gvDados.JSProperties["cp_CodigoProjeto"] = idProjeto.ToString();

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "DC_REAPDEM");
        }

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            
        }
        cDados.aplicaEstiloVisual(Page);
        defineSePodeReapresentarDemanda();

        carregaGvDados();
    }

    private void defineSePodeReapresentarDemanda()
    {
        string comandoSQL = string.Format(@"SELECT [dbo].[f_pbh_podeReapresentarDemanda]({0},{1})", idProjeto, codigoUsuarioResponsavel);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            podeIncluir = ds.Tables[0].Rows[0][0].ToString() == "S";
        }
    }

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/reapresentacaoDemanda.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "reapresentacaoDemanda"));
        Header.Controls.Add(cDados.getLiteral(@"<title>TO DO List</title>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        int altura = (alturaPrincipal - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 200;
        gvDados.Width = new Unit("100%");
    }

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        SELECT Protocolo, Descricao, DataAbertura, Solicitante 
        FROM [dbo].[f_pbh_getDemandasReapresentadas] ({0})", idProjeto);


        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";


        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        string textoMensagem = Resources.traducao.reapresentacaoDemanda_confirma_a_abertura_do_fluxo_de_reapresenta__o_da_demanda__ao_confirmar_a_abertura__o_fluxo___automaticamente_criado_e_a_a__o_n_o_pode_ser_desfeita_;
        string strJsMostraConfirmacao = string.Format(@"window.top.mostraConfirmacao('{0}', executaAberturaFluxo, null)", textoMensagem);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, strJsMostraConfirmacao, true, false, false, "CadReapDem", Resources.traducao.reapresentacaoDemanda_reapresenta__o_de_demandas, this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadReapDem");
    }

    protected void callbackAberturaFluxo_Callback(object source, CallbackEventArgs e)
    {
        string mensagemRetorno = "";
        int retorno = -1;
        int regAfetados = 0;
        string comandoSQL = string.Format(@"
         DECLARE @RC int
        DECLARE @in_CodigoProjeto int
        DECLARE @in_CodigoUsuario int

        SET @in_CodigoProjeto = {0}
        SET @in_CodigoUsuario = {1}

        EXECUTE @RC = [dbo].[p_pbh_criaNovoFluxoReapresentacaoDemanda] @in_CodigoProjeto, @in_CodigoUsuario 

        SELECT @RC", idProjeto, codigoUsuarioResponsavel);

        DataSet dsRetorno = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(dsRetorno) && cDados.DataTableOk(dsRetorno.Tables[0]))
        {
            retorno = int.Parse(dsRetorno.Tables[0].Rows[0][0].ToString());
        }
        if(retorno == 0)
        {
            mensagemRetorno = Resources.traducao.reapresentacaoDemanda_fluxo_de_reapresenta__o_de_demanda_criado_com_sucesso__acesse_a_op__o_demandas___ccg_para_dar_prosseguimento_ao_processo_;
        }
        else if (retorno == -1)
        {
            mensagemRetorno = Resources.traducao.reapresentacaoDemanda_houve_um_problema_ao_tentar_criar_o_fluxo_de_reapresenta__o_da_demanda__favor_verificar_se_voc__possui_permiss_o_para_reapresentar_a_demanda_em_quest_o_;
        }

        ((ASPxCallback)(source)).JSProperties["cp_mensagemRetorno"] = mensagemRetorno;
        ((ASPxCallback)(source)).JSProperties["cp_codigoRetorno"] = retorno;

    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {

    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }
}