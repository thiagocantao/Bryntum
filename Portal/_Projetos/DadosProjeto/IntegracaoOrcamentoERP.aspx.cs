/*
 Data Creação: 1 de Outubro 2010
 *              
 * JavaScript vinculado: Portfolio/scripts/IntegracaoOrcamentoERP.js
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

public partial class _Projetos_DadosProjeto_IntegracaoOrcamentoERP : System.Web.UI.Page
{
    dados cDados;
    private int idProjeto;
    private int idUsuarioLogado;
    private int idEntidadeLogado;
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

    public bool podeIncluir;
    public bool podeAlterar;
    public bool podeExcluir;

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        idEntidadeLogado = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        HeardeOnTela();
        
        cargarComboEntidades();

        if (Request.QueryString["ID"] != null)
            idProjeto = int.Parse(Request.QueryString["ID"].ToString());
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");        

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, idEntidadeLogado, idProjeto, "null", "PR", 0, "null", "PR_CnsCRsRel");
        }

        definePermissoesUsuario();
        if (!IsPostBack)
        {
            hfGeral.Set("codigoCR", "");
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }

        carregaGvDados();
    }

    #region GRIDVIEW

    private void carregaGvDados()
    {
        DataSet ds = cDados.getGridIntegracaoOrcamento(idProjeto.ToString(), "");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = altura - 190;
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 180;
        //pnCallbackPopup.Height = alturaPrincipal - 20;
    }

    private void HeardeOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/IntegracaoOrcamentoERP.js""></script>"));
        this.TH(this.TS("IntegracaoOrcamentoERP"));
    }


    /// <summary>
    /// Define as ermissões para manipular as configurações de CR's no projeto
    /// </summary>
    private void definePermissoesUsuario()
    {
        bool podeAdministrar = cDados.VerificaPermissaoUsuario(idUsuarioLogado, idEntidadeLogado, idProjeto, "null", "PR", 0, "null", "PR_AdmCRsRel");

        podeIncluir = podeAdministrar;
        podeAlterar = podeAdministrar;
        podeExcluir = podeAdministrar;

        cDados.verificaPermissaoProjetoInativo(idProjeto, ref podeIncluir, ref podeAlterar, ref podeExcluir);

        string podeUtilizarCRsOutrasEntidades = "N";

        DataSet ds = cDados.getParametrosSistema(idEntidadeLogado, "PodeUtilizarCRsOutrasEntidades");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            podeUtilizarCRsOutrasEntidades = ds.Tables[0].Rows[0]["PodeUtilizarCRsOutrasEntidades"].ToString();
        }

        if (podeUtilizarCRsOutrasEntidades == "N")
        {
            tbEntidade.Style.Add("display", "none");
        }
    }


    #endregion

    #region CALLBACK

    protected void pnCallbackPopup_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter;
        pnCallbackPopup.JSProperties["cp_OperacaoOk"] = "";

        if (parametro == "iniciarPopup")
        {
            ddlEntidade.Value = idEntidadeLogado.ToString();
            cargarComboMovimientoOrcamentario("");
        }
        else if (parametro == "salvarCR")
        {
            persisteInclusaoRegistro();
        }
        else if (parametro == "excluirCR")
        {
            persisteExclusaoRegistro();
        }

        pnCallbackPopup.JSProperties["cp_OperacaoOk"] = parametro;
    }

    protected void pnCallbackCR_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoMovimientoOrcamentista = e.Parameter;

        cargarComboListagemCR(codigoMovimientoOrcamentista, "");
    }

    protected void pnCallbackEstadoCR_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigCR = e.Parameter;

        manipularCRselecionada(codigCR, idProjeto);
    }

    /// <summary>
    /// Esta função verifica a existencia do codigoCR na tabela [projetoCR](*), si existir tem 3 casos
    /// 1) Mesmo Projeto, 
    /// 2) Outro Projeto mesma unidade.
    /// 3) Outro Projeto em outra unidade.
    /// 
    /// 
    /// (*) Um projeto pode ter varios CR, mais um CR pode estar em so um Projeto.
    /// </summary>
    /// <param name="codigCR"></param>
    /// <param name="idProjeto"></param>
    private void manipularCRselecionada(string codigCR, int idProjeto)
    {
        DataSet ds = cDados.getCRseleccionado(codigCR, idProjeto.ToString(), idEntidadeLogado);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblAtencao.Text = "Atenção!";
            btnSalvar.ClientEnabled = false;

            if (ds.Tables[0].Rows[0]["CodigoProjeto"].ToString().Equals(idProjeto.ToString()))
                lblCorpoAtencao.Text = "Este CR já está relacionado a este projeto.";
            else if (ds.Tables[0].Rows[0]["CodigoEntidade"].ToString().Equals(idEntidadeLogado.ToString()))
                lblCorpoAtencao.Text = string.Format("Não será possível relacionar este CR a este projeto. Este CR já está relacionado ao projeto {0}.", ds.Tables[0].Rows[0]["NomeProjeto"].ToString());
            else
                lblCorpoAtencao.Text = string.Format("Não será possível relacionar este CR a este projeto. Este CR já está relacionado ao projeto {0} da {1}.", ds.Tables[0].Rows[0]["NomeProjeto"].ToString(), ds.Tables[0].Rows[0]["desUnidadeNegocio"].ToString());
        }
        else
        {
            lblAtencao.Text = "";
            lblCorpoAtencao.Text = "";
            btnSalvar.ClientEnabled = true;
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoCR").ToString();
        return codigoDado;
    }

    private void persisteExclusaoRegistro()
    {
        string codigoCR = getChavePrimaria();
        cDados.excluiProjetoIntegraCR(idProjeto.ToString(), codigoCR);
        string msg = "";
        bool result = cDados.atualizaStatusProjeto(idProjeto, idUsuarioLogado, ref msg);
    }

    private void persisteInclusaoRegistro()
    {
        string codigoCR = hfGeral.Get("codigoCR").ToString();//ddlListagemCR.Value.ToString();
        bool retorno = cDados.incluirProjetoIntegraCR(idProjeto.ToString(), codigoCR);
        string msg = "";
        bool result = cDados.atualizaStatusProjeto(idProjeto, idUsuarioLogado, ref msg);

        if (retorno)
        {
            cDados.atualizaCuboOrcamentoAnoCorrente(ddlEntidade.Value.ToString());
        }
    }

    #endregion

    #region COMBOBOX

    private void cargarComboMovimientoOrcamentario(string p)
    {
        string where = string.Format(@" AND CodigoEntidade = {0}", ddlEntidade.Value);
        DataSet ds = cDados.getMovimientoOrcamentario(where);

        if (cDados.DataSetOk(ds))
        {
            ddlMovimientoOrcamentario.DataSource = ds.Tables[0];
            ddlMovimientoOrcamentario.DataBind();

            ddlMovimientoOrcamentario.SelectedIndex = -1;

            inicializarListagemCR();
        }
        else
        {
            ddlMovimientoOrcamentario.SelectedIndex = -1;
        }
    }

    private void cargarComboListagemCR(string codigoMovimentoOrcamento, string where)
    {
        int codigoEntidadeSelecao = ddlEntidade.Value == null ? -1 : int.Parse(ddlEntidade.Value.ToString());

        DataSet ds = cDados.getListagemCR(codigoMovimentoOrcamento, idProjeto, codigoEntidadeSelecao, "");

        if (cDados.DataSetOk(ds))
        {
            ddlListagemCR.DataSource = ds.Tables[0];
            ddlListagemCR.DataBind();

            ddlListagemCR.SelectedIndex = -1;
        }
    }

    private void inicializarListagemCR()
    {
        ddlListagemCR.SelectedIndex = -1;
        ddlListagemCR.Items.Clear();
    }

    private void cargarComboEntidades()
    {
        DataSet ds = cDados.getEntidadesUsuario(idUsuarioLogado, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + idUsuarioLogado);

        if (cDados.DataSetOk(ds))
        {
            ddlEntidade.DataSource = ds;
            ddlEntidade.TextField = "NomeUnidadeNegocio";
            ddlEntidade.ValueField = "CodigoUnidadeNegocio";
            ddlEntidade.DataBind();

            if(!IsPostBack)
                ddlEntidade.Value = idEntidadeLogado.ToString();
        }
    }

    #endregion

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {       
        if (e.ButtonID == "btnExcluirCustom")
        {
            if (podeExcluir)
                e.Enabled = true;
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    protected void ddlMovimientoOrcamentario_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter == "A")
        {
            cargarComboMovimientoOrcamentario("");
        }
    }
}
