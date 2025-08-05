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

public partial class AnaliseProjeto : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoProjeto;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;

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
        codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        cDados.aplicaEstiloVisual(Page);

        if (cDados.verificaAcessoStatusProjeto(codigoProjeto))
        {
            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_IncAssFlx");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_AltAssFlx");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_ExcAssFlx");

            cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);
        }

        carregaComboFluxosDisponiveis();
        carregaListaStatusProjetos(-1, false);

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CnsAssFlx");
            carregaGvDados();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings", "FluxosProjeto"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 370;
        //gvDados.Width = new Unit((largura - 205) + "px");
    }
    #endregion

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getFluxosAssociadosProjeto(codigoProjeto, "");

        if ((cDados.DataSetOk(ds)))
        {            
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvDados.VisibleRowCount == 0) return;
        switch (e.ButtonID)
        {
            case "btnEditar":            
                if (!podeEditar)
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                break;
            case "btnExcluir":
                if (!podeExcluir)
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                break;
        }

    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
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
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "")
                gvDados.ClientVisible = false;
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string msgErro = "";
        string statusRelacionamento = rbStatus.Value.ToString();
        int codigoFluxo = int.Parse(ddlFluxo.Value.ToString());
        string nomeOpcao = txtNomeOpcao.Text;
        string ocorrencia = ddlOcorrencia.Value.ToString();

        bool nomeExistente = cDados.verificaExistenciaNomeFluxoAssociadoProjeto(codigoProjeto, nomeOpcao, -1);

        if (nomeExistente)
            return Resources.traducao.FluxosProjeto_n_o___poss_vel_incluir_o_fluxo__a__op__o_de_menu__informada_j__est__cadastrada_em_outro_fluxo_para_este_projeto_;
        else
        {
            int[] arrayStatus = new int[gvStatus.GetSelectedFieldValues("CodigoStatus").Count];

            for (int i = 0; i < arrayStatus.Length; i++)
                arrayStatus[i] = int.Parse(gvStatus.GetSelectedFieldValues("CodigoStatus")[i].ToString());

            bool result = cDados.incluiAssociacaoFluxoProjeto(codigoProjeto, codigoFluxo, statusRelacionamento, nomeOpcao, ocorrencia, arrayStatus, codigoUsuarioResponsavel, ref msgErro);

            if (result == false)
                return Resources.traducao.FluxosProjeto_erro_ao_salvar_o_registro_;
            else
            {
                carregaGvDados();
                carregaComboFluxosDisponiveis();
                return "";
            }
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoFluxo = int.Parse(getChavePrimaria());

        string msgErro = "";
        string statusRelacionamento = rbStatus.Value.ToString();
        string nomeOpcao = txtNomeOpcao.Text;
        string ocorrencia = ddlOcorrencia.Value.ToString();
        string statusRelacionamentoAnterior = gvDados.GetRowValues(gvDados.FocusedRowIndex, "StatusRelacionamento").ToString();

        bool nomeExistente = cDados.verificaExistenciaNomeFluxoAssociadoProjeto(codigoProjeto, nomeOpcao, codigoFluxo);

        if (nomeExistente)
            return Resources.traducao.FluxosProjeto_n_o___poss_vel_alterar_o_fluxo__a__op__o_de_menu__informada_j__est__cadastrada_em_outro_fluxo_para_este_projeto_;
        else
        {
            int[] arrayStatus = new int[gvStatus.GetSelectedFieldValues("CodigoStatus").Count];

            for (int i = 0; i < arrayStatus.Length; i++)
                arrayStatus[i] = int.Parse(gvStatus.GetSelectedFieldValues("CodigoStatus")[i].ToString());

            bool result = cDados.atualizaAssociacaoFluxoProjeto(codigoProjeto, codigoFluxo, statusRelacionamento, statusRelacionamentoAnterior, nomeOpcao, ocorrencia, arrayStatus, codigoUsuarioResponsavel, ref msgErro);

            if (result == false)
                return Resources.traducao.FluxosProjeto_erro_ao_salvar_o_registro_;
            else
            {
                carregaGvDados();
                return "";
            }
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoFluxo = int.Parse(getChavePrimaria());

        bool permissaoExclusao = cDados.verificaExclusaoFluxoAssociadoProjeto(codigoProjeto, codigoFluxo);

        if (permissaoExclusao)
        {
            bool result = cDados.excluiAssociacaoFluxoProjeto(codigoFluxo, codigoProjeto);

            if (result == false)
                return Resources.traducao.FluxosProjeto_erro_ao_excluir_o_registro_;
            else
            {
                carregaGvDados();
                carregaComboFluxosDisponiveis();
                return "";
            }
        }
        else
        {
            return Resources.traducao.FluxosProjeto_a_associa__o_n_o_pode_ser_exclu_da__existem_instancias_criadas_para_o_fluxo_neste_projeto_;
        }
    }

    #endregion
    
    private void carregaComboFluxosDisponiveis()
    {
        DataSet ds = cDados.getFluxosDisponiveisAssociacaoProjeto(codigoProjeto);

        if (cDados.DataSetOk(ds))
        {
            ddlFluxo.DataSource = ds;
            ddlFluxo.DataBind();
        }
    }

    private void carregaListaStatusProjetos(int codigoFluxoSelecionado, bool seleciona)
    {
        DataSet ds = cDados.getListaPossiveisStatusFluxosProjeto(codigoProjeto, codigoFluxoSelecionado, "");

        if (cDados.DataSetOk(ds))
        {
            gvStatus.DataSource = ds;
            gvStatus.DataBind();

            if (seleciona)
            {
                gvStatus.Selection.UnselectAll();
                DataRow[] listaSelecionados = ds.Tables[0].Select("IndicaSelecionado = 'S'");

                if (listaSelecionados.Length == ds.Tables[0].Rows.Count)
                    gvStatus.JSProperties["cp_Todos"] = "S";
                else
                    gvStatus.JSProperties["cp_Todos"] = "N";

                foreach (DataRow dr in listaSelecionados)
                {
                    gvStatus.Selection.SelectRowByKey(dr["CodigoStatus"]);
                }
            }
        }
    }

    protected void gvStatus_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao") + "" != "Consultar")
            gvStatus.Columns[0].Visible = true;
        else
            gvStatus.Columns[0].Visible = false;


        if (e.Parameters != "")
        {
            carregaListaStatusProjetos(int.Parse(e.Parameters), true);
        }
    }
}
