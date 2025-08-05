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
using CDIS;
using System.Collections.Specialized;
using System.Drawing;
using System.Web.Hosting;
using System.IO;

public partial class _Projetos_DadosProjeto_riscos : System.Web.UI.Page
{
    dados cDados;
    DataSet dsPermissao = new DataSet();
    private ASPxGridView gvToDoList;

    bool riscoInclui;
    bool riscoEditar;
    bool riscoExclui;
    bool riscoComentar;
    bool riscoEliminar;
    bool riscoCancelar;
    bool questoeInclui;
    bool questoeEditar;
    bool questoeExclui;
    bool questoeComentar;
    bool questoeEliminar;
    bool questoeCancelar;

    private string nomeTabelaDb = "RiscoQuestao";
    private string whereUpdateDelete;
    private string resolucaoCliente = "";

    string labelQuestoes = "Questões";
    string labelQuestao = "Questão";
    string generoQuestao = "F";

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioLogado;
    private int codigoEntidadeUsuarioResponsavel;
    string nomeTipoProjeto = "";

    private char indRiscoQuestao;

    protected void Page_Init(object sender, EventArgs e)
    {
        
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        verificaRedirecionamentoTelaDeRiscoUnigest();
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());
        indRiscoQuestao = Request.QueryString["TT"].ToString()[0]; // TT = Tipo Tela, pode ser "R"isco ou "Q"uestao

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            string iniciaisTela = indRiscoQuestao == 'Q' ? "PR_CnsRQ2" : "PR_CnsRQ1";

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", iniciaisTela);
        }

        bool mostrarel = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Incluir");
        //btnRelatorioRisco.ClientVisible = mostrarel;
        GerarRelatorioEmPDF1.ClientVisible = mostrarel;
        GerarRelatorioEmPDF2.ClientVisible = mostrarel;
        GerarRelatorioEmPDF3.ClientVisible = mostrarel;
        GerarRelatorioEmPDF4.ClientVisible = mostrarel;
        GerarRelatorioEmPDF5.ClientVisible = mostrarel;

        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        sdsTipoRespostaRisco.ConnectionString = cDados.classeDados.getStringConexao();

        #region -- [Códigos p/ controle de acesso (Gerente Projeto ou Responsável Risco) ]
        // código para controle se é o gerente do projeto ou o criador do risco que está 'editando o risco'
        hfGeral.Set("codigoUsuarioLogado", codigoUsuarioLogado);
        DataSet ds = cDados.getGerenteProjeto(idProjeto);

        int codigoGerenteProjeto = -1;
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            int.TryParse(ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString(), out codigoGerenteProjeto);

        hfGeral.Set("codigoGerenteProjeto", codigoGerenteProjeto);

        cDados.setaTamanhoMaximoMemo(txtDescricao, 2000, lblContadorMemoDescricao);
        cDados.setaTamanhoMaximoMemo(txtConsequencia, 2000, lblContadorMemoConsequencia);
        cDados.setaTamanhoMaximoMemo(txtEstrategiaTratamento, 2000, lblContadorMemoEstrategiaTratamento);
        cDados.setaTamanhoMaximoMemo(mmComentarioAcao, 2000, lblContadorMemoComentarioAcao);
        #endregion

    }

    private void verificaRedirecionamentoTelaDeRiscoUnigest()
    {
        string IndicaTelaRiscoUNIGEST = "N"; 
        DataSet ds = cDados.getParametrosSistema("IndicaTelaRiscoUNIGEST");
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            IndicaTelaRiscoUNIGEST = ds.Tables[0].Rows[0]["IndicaTelaRiscoUNIGEST"].ToString().Trim();
        }
        if (IndicaTelaRiscoUNIGEST == "S")
        {
            Response.Redirect("~/_Projetos/DadosProjeto/riscos_unigest.aspx?" + Request.QueryString.ToString());
        }        
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        carregaPlanoAcao();
        definePermissoesUsuario();
        bool indicaQuestao = indRiscoQuestao.Equals('Q');
        bool podeEliminar = indicaQuestao ? questoeEliminar : riscoEliminar;
        bool podeCancelar = indicaQuestao ? questoeCancelar : riscoCancelar;
        btnEliminar.ClientVisible = podeEliminar;
        btnCancelar.ClientVisible = podeCancelar;
        DataSet dsParametro = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

        if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
        {
            labelQuestoes = dsParametro.Tables[0].Rows[0]["labelQuestoes"].ToString();
            generoQuestao = dsParametro.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();
            labelQuestao = dsParametro.Tables[0].Rows[0]["labelQuestao"].ToString();
        }

        //btnRelatorioRisco.JSProperties["cp_labelRiscoQuestao"] = "Risco do Projeto";
        GerarRelatorioEmPDF1.JSProperties["cp_labelRiscoQuestao"] = "Risco do Projeto";
        GerarRelatorioEmPDF2.JSProperties["cp_labelRiscoQuestao"] = "Risco do Projeto";
        GerarRelatorioEmPDF3.JSProperties["cp_labelRiscoQuestao"] = "Risco do Projeto";
        GerarRelatorioEmPDF4.JSProperties["cp_labelRiscoQuestao"] = "Risco do Projeto";
        GerarRelatorioEmPDF5.JSProperties["cp_labelRiscoQuestao"] = "Risco do Projeto";

        if (indRiscoQuestao == 'Q')
        {
            btnTransforma.ClientVisible = false;

            // btnNovoRisco.Text = novaQuestao;
            //btnRelatorioRisco.JSProperties["cp_labelRiscoQuestao"] = labelQuestao + " do Projeto";
            GerarRelatorioEmPDF1.JSProperties["cp_labelRiscoQuestao"] = labelQuestao + " do Projeto";
            GerarRelatorioEmPDF2.JSProperties["cp_labelRiscoQuestao"] = labelQuestao + " do Projeto";
            GerarRelatorioEmPDF3.JSProperties["cp_labelRiscoQuestao"] = labelQuestao + " do Projeto";
            GerarRelatorioEmPDF4.JSProperties["cp_labelRiscoQuestao"] = labelQuestao + " do Projeto";
            GerarRelatorioEmPDF5.JSProperties["cp_labelRiscoQuestao"] = labelQuestao + " do Projeto";

            gvDados.Columns[2].Caption = labelQuestao;
            gvDados.Columns["DescricaoRiscoQuestao"].Caption = labelQuestao;
            gvDados.Columns["DataLimiteResolucao"].Caption = "Limite Resolução";
            gvDados.Columns["DataEliminacaoCancelamento"].Caption = "Data Resolução";

            pcAbas.TabPages[0].Text = labelQuestao;
            //pcAbas.TabPages.FindByName("tabPageTratamento").Visible = false;
            pnMemosTratamento.Visible = true;

            lblRiscoQuestao.Text = labelQuestao + ":";
            lblRiscoAssociado.Text = labelQuestao + " (superior):";

            lblProbabilidadeUrgencia.Text = Resources.traducao.riscos_urg_ncia_;
            lblImpactoPrioridade.Text = Resources.traducao.riscos_prioridade_;

            ddlImpacto.Items[0].Text = Resources.traducao.mnuopt_prioridade_alta;
            ddlImpacto.Items[1].Text = Resources.traducao.mnuopt_prioridade_media;
            ddlImpacto.Items[2].Text = Resources.traducao.mnuopt_prioridade_baixa;

            lblLimiteEliminacaoResolucao.Text = Resources.traducao.riscos_limite_resolu__o;
            lblEliminacaoResolucaoCancelamento.Text = Resources.traducao.riscos_resolu__o_canc;

            btnEliminar.Text = Resources.traducao.riscos_eliminar_ + labelQuestao;
            btnCancelar.Text = Resources.traducao.riscos_cancelar_ + labelQuestao;

            pcDados.HeaderText = labelQuestao;
        }
        btnTransforma.Text = string.Format("{0} {1}", Resources.traducao.riscos_transformar_em_, labelQuestao);
        hfGeral.Set("labelQuestao", labelQuestao);

        cDados.aplicaEstiloVisual(Page);

        ddlResponsavel.Columns[0].FieldName = "NomeUsuario";
        ddlResponsavel.Columns[1].FieldName = "EMail";
        ddlResponsavel.TextFormatString = "{0}";

        if (!IsPostBack)
        {
            pcAbas.ActiveTabIndex = 0;
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        populaDDLTipo();

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();
        nomeTipoProjeto = cDados.getNomeTipoProjeto(idProjeto);
        populaGrid();

        if (!IsPostBack && !IsCallback)
        {
            AtribuiFiltroGrid();
            //btnRelatorioRisco.Style.Add("cursor", "pointer");
            GerarRelatorioEmPDF1.Style.Add("cursor", "pointer");
            GerarRelatorioEmPDF2.Style.Add("cursor", "pointer");
            GerarRelatorioEmPDF3.Style.Add("cursor", "pointer");
            GerarRelatorioEmPDF4.Style.Add("cursor", "pointer");
            GerarRelatorioEmPDF5.Style.Add("cursor", "pointer");
        }
        gvDados.FindFooterRowTemplateControl("lblMsgEmExecucao").Visible = nomeTipoProjeto == "Programa";

        if(indRiscoQuestao == 'R')
        {
            tdRisco.Style.Add("visibility", "visible");
        }
        else if (indRiscoQuestao == 'Q')
        {
            tdRisco.Style.Add("display", "none");
        }
        hfGeral.Set("idiomaEPortugues", System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/riscos.js""></script>"));

        this.TH(this.TS("riscos", "barraNavegacao"));
    }

    private void carregaPlanoAcao()
    {
        // inclui as funcionalidades do plano de ação.
        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("RQ");
        int codigoObjetoAssociado = -1;
        if (hfGeral.Contains("codigoObjetoAssociado"))
            codigoObjetoAssociado = int.Parse(hfGeral.Get("codigoObjetoAssociado").ToString());

        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao"))
            somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");

        bool indicaQuestao = indRiscoQuestao.Equals('Q');
        riscoEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AltRQ1");
        questoeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AltRQ2");
        bool podeEditar = indicaQuestao ? questoeEditar : riscoEditar;
        somenteLeitura = somenteLeitura || (!podeEditar);

        int[] convidados = getParticipantesEvento();

        Unit tamanho = new Unit("700px");

        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoEntidadeUsuarioResponsavel, codigoUsuarioLogado, idProjeto, codigoTipoAssociacao, codigoObjetoAssociado, tamanho, 289, somenteLeitura, convidados.Length == 0 ? null : convidados, true, txtRisco.Text);
        pcAbas.TabPages.FindByName("tabPageToDoList").FindControl("Content4Div").Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario());
        gvToDoList = myPlanoDeAcao.gvToDoList;
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtRisco.GetText());}";

        

        if (!IsCallback)
            gvToDoList.DataBind();
    }

    private void AtribuiFiltroGrid()
    {
        string status = Request.QueryString["Status"];
        if (status != "Eliminado")
            status = (indRiscoQuestao == 'R' ? "Ativo" : "Aberta");
        else
            status = (indRiscoQuestao == 'R' ? "Eliminado" : "Resolvida");

        if (Request.QueryString["Status"] != null)
            gvDados.FilterExpression += " DescricaoStatusRiscoQuestao = '" + status /*Request.QueryString["Status"]*/ + "'";

        if (Request.QueryString["Publicado"] != null)
            gvDados.FilterExpression += " AND Publicado = '" + Request.QueryString["Publicado"] + "'";

        if (Request.QueryString["Cor"] != null)
            gvDados.FilterExpression += " AND CorRiscoQuestao = '" + Request.QueryString["Cor"] + "'";

        if (Request.QueryString["GPP"] != null)
            gvDados.FilterExpression += " AND (Polaridade = 'Negativa' AND ProbabilidadePrioridade = " + Request.QueryString["GPP"] + " OR " +
                "Polaridade = 'Positiva' AND ProbabilidadePrioridade = (4-" + Request.QueryString["GPP"] + ") )";


        if (Request.QueryString["GIU"] != null)
            gvDados.FilterExpression += " AND (Polaridade = 'Negativa' AND ImpactoUrgencia = " + Request.QueryString["GIU"] + " OR " +
                "Polaridade = 'Positiva' AND ImpactoUrgencia = (4-" + Request.QueryString["GIU"] + ") )";

        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao"))
            somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");
    }

    private void definePermissoesUsuario()
    {
        //--------Permissao de Riscos e Questoes
        riscoInclui = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_IncRQ1");
        riscoEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AltRQ1");
        riscoExclui = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_ExcQR1");
        riscoComentar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_CmtRQ1");
        riscoEliminar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_ElmRQ1");
        riscoCancelar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_CncRQ1");

        questoeInclui = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_IncRQ2");
        questoeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AltRQ2");
        questoeExclui = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_ExcQR2");
        questoeComentar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_CmtRQ2");
        questoeEliminar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_ElmRQ2");
        questoeCancelar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_CncRQ2");

        cDados.verificaPermissaoProjetoInativo(idProjeto, ref riscoInclui, ref riscoEditar, ref riscoExclui);

        cDados.verificaPermissaoProjetoInativo(idProjeto, ref questoeInclui, ref questoeEditar, ref questoeExclui);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int altura, nAlturaDivs;
        string alturaDivPopus;
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        altura = (alturaPrincipal - 190);

        if(alturaPrincipal <=768)
        {
            alturaDivPopus = "260px";
        }
        else if ((alturaPrincipal >= 769) && (alturaPrincipal <=800) )
        {
            alturaDivPopus = "370px";
        }
        else if ((alturaPrincipal >= 801) && (alturaPrincipal <= 960))
        {
            alturaDivPopus = "420px";
        }
        else 
        {
            alturaDivPopus = "510px";
        }

        divTab1.Style.Add("max-height", alturaDivPopus);
        divTab2.Style.Add("max-height", alturaDivPopus);
        divTab5.Style.Add("max-height", alturaDivPopus);
        
        //divTab5.Style.Add("max-height", alturaDivPopus);

        nAlturaDivs = int.Parse(alturaDivPopus.Replace("px", ""))-81;
        gvComentarios1.SettingsPager.Visible = false;
        gvComentarios1.Settings.VerticalScrollableHeight = nAlturaDivs;
        frmAnexos.Attributes.Add("height", (nAlturaDivs + 100) + "px");
        gvToDoList.Settings.VerticalScrollableHeight = nAlturaDivs - 18;
    }

    private void populaDDLTipo()
    {
        DataSet ds = cDados.getTipoRiscoQuestao(indRiscoQuestao);
        ddlTipo.DataSource = ds.Tables[0];
        ddlTipo.ValueField = "CodigoTipoRiscoQuestao";
        ddlTipo.TextField = "DescricaoTipoRiscoQuestao";
        ddlTipo.DataBind();

        if (!IsPostBack && indRiscoQuestao == 'R')
            ddlTipo.SelectedIndex = 0;
    }

    private int getIndexProbabilidadeImpacto(int indexCombo)
    {
        if (indexCombo + 1 == 1)
            return 3; // Alto
        else if (indexCombo + 1 == 3)
            return 1; //Baixo
        else
            return 2; // Médio
    }

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();

        oDadosFormulario.Add("DescricaoRiscoQuestao", txtRisco.Text);
        oDadosFormulario.Add("DetalheRiscoQuestao", txtDescricao.Text);
        oDadosFormulario.Add("CodigoTipoRiscoQuestao", ddlTipo.Value.ToString());
        oDadosFormulario.Add("ProbabilidadePrioridade", getIndexProbabilidadeImpacto(ddlProbabilidade.SelectedIndex));
        oDadosFormulario.Add("ImpactoUrgencia", getIndexProbabilidadeImpacto(ddlImpacto.SelectedIndex));
        oDadosFormulario.Add("CodigoUsuarioResponsavel", ddlResponsavel.Value);
        oDadosFormulario.Add("ConsequenciaRiscoQuestao", txtConsequencia.Text);
        oDadosFormulario.Add("TratamentoRiscoQuestao", txtEstrategiaTratamento.Text);
        oDadosFormulario.Add("CodigoUsuarioInclusao", codigoUsuarioLogado);
        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() == "Incluir")
            oDadosFormulario.Add("DataInclusao", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
        oDadosFormulario.Add("CodigoEntidade", codigoEntidadeUsuarioResponsavel);
        oDadosFormulario.Add("CodigoProjeto", idProjeto);
        oDadosFormulario.Add("IndicaRiscoQuestao", indRiscoQuestao.ToString());
        oDadosFormulario.Add("DataStatusRiscoQuestao", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));

        oDadosFormulario.Add("CodigoRiscoQuestaoSuperior",ddlRiscoAssociado.Value);
        oDadosFormulario.Add("CustoRiscoQuestao", spnCusto.Value);
        oDadosFormulario.Add("CodigoTipoRespostaRisco", ddlTipoRespostaRisco.Value);


        if (indRiscoQuestao == 'R')
            oDadosFormulario.Add("CodigoStatusRiscoQuestao", "RA");
        else
            oDadosFormulario.Add("CodigoStatusRiscoQuestao", "QA");

        if (ddeLimiteEliminacao.Date != null && ddeLimiteEliminacao.Date.Year >= DateTime.Now.Year)
            oDadosFormulario.Add("DataLimiteResolucao", ddeLimiteEliminacao.Date);
        else
            oDadosFormulario.Add("DataLimiteResolucao", "NULL");

        return oDadosFormulario;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        /*Questão = Problema
          Risco = Risco*/

        if ('Q' == indRiscoQuestao)//problema
        {
            gvDados.Columns["ProbabilidadePrioridade"].Caption = Resources.traducao.riscos_prioridade;
            gvDados.Columns["ImpactoUrgencia"].Caption = Resources.traducao.riscos_impacto;

            //Tratamento de Pronome para Masculino e Feminino em PT e em inglês utiliza-se o "The"
            if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
            {
                gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = string.Format(@"Consequence {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
                gvDados.Columns["TratamentoRiscoQuestao"].Caption = string.Format(@"Treatment {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
                gvDados.Columns["DetalheRiscoQuestao"].Caption = string.Format(@"Details {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
                gvDados.Columns["DescricaoRiscoQuestao"].Caption = string.Format(@"description {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
            }
            else
            {
                gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = string.Format(@"Consequência d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["TratamentoRiscoQuestao"].Caption = string.Format(@"Tratamento d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["DetalheRiscoQuestao"].Caption = string.Format(@"Detalhes d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["DescricaoRiscoQuestao"].Caption = string.Format(@"Descrição d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
            }
        }
        else
        {
            gvDados.Columns["ProbabilidadePrioridade"].Caption = Resources.traducao.riscos_probabilidade;
            gvDados.Columns["ImpactoUrgencia"].Caption = Resources.traducao.riscos_urg_ncia;
            gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = Resources.traducao.riscos_consequ_ncia_do_risco;
            gvDados.Columns["TratamentoRiscoQuestao"].Caption = Resources.traducao.riscos_tratamento_do_risco;
            gvDados.Columns["DetalheRiscoQuestao"].Caption = Resources.traducao.riscos_detalhes_do_risco;
            gvDados.Columns["DescricaoRiscoQuestao"].Caption = Resources.traducao.riscos_descri__o_do_risco;
        }

        gvDados.Columns["DescricaoTipoRiscoQuestao"].Visible = true;
        gvDados.Columns["DetalheRiscoQuestao"].Visible = true;
        gvDados.Columns["DetalheRiscoQuestao"].VisibleIndex = 1;

        gvDados.Columns["ProbabilidadePrioridade"].Visible = true;
        gvDados.Columns["ImpactoUrgencia"].Visible = true;
        gvDados.Columns["Severidade"].Visible = true;
        gvDados.Columns["ConsequenciaRiscoQuestao"].Visible = true;
        gvDados.Columns["TratamentoRiscoQuestao"].Visible = true;
        gvDados.Columns["NomeProjeto"].Visible = true;
        gvDados.Columns["NomeProjeto"].VisibleIndex = 0;

        gvDados.Columns["acao"].Visible = false;
        gvDados.Columns["CorRiscoQuestao"].Visible = false;


        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });

        gvDados.Columns["DescricaoTipoRiscoQuestao"].Visible = false;
        gvDados.Columns["DetalheRiscoQuestao"].Visible = false;
        gvDados.Columns["ProbabilidadePrioridade"].Visible = false;
        gvDados.Columns["ImpactoUrgencia"].Visible = false;
        gvDados.Columns["Severidade"].Visible = false;
        gvDados.Columns["ConsequenciaRiscoQuestao"].Visible = false;
        gvDados.Columns["TratamentoRiscoQuestao"].Visible = false;
        gvDados.Columns["NomeProjeto"].Visible = false;
        gvDados.Columns["acao"].Visible = true;
        gvDados.Columns["CorRiscoQuestao"].Visible = true;

    }

    private void processaAcao(int codigoRiscoQuestao, string acao)
    {
        string comentario = mmComentarioAcao.Text;
        string novoStatus = string.Empty;
        string updateIndicaRiscoQuestao = "";
        if ('Q' == indRiscoQuestao)
        {
            if (acao.Equals(Resources.traducao.riscos_eliminar))
                novoStatus = "QR";  // questão resolvida
            else if (acao.Equals(Resources.traducao.riscos_cancelar))
                novoStatus = "QC";  // questão cancelada
            else if (acao.Equals(Resources.traducao.riscos_excluir))
                novoStatus = "QX";  // questão excluída
            else
                novoStatus = "";  // não fazer nada caso venha algum erro ou uma situação ainda não prevista
        } // if ("Q" == indRiscoQuestao)
        else
        {
            if (acao.Equals(Resources.traducao.riscos_eliminar))
                novoStatus = "RE";  // questão resolvida
            else if (acao.Equals(Resources.traducao.riscos_cancelar))
                novoStatus = "RC";  // questão cancelada
            else if (acao.Equals(Resources.traducao.riscos_excluir))
                novoStatus = "RX";  // questão excluída
            else if (acao.Equals(Resources.traducao.riscos_transformar))
            {
                novoStatus = "";  // chamar a proc diretamente
                // insere o novo comentário
                int afetados = 0;
                string comandoSQL = string.Format(
                    @" EXEC {0}.{1}.p_TransformaRiscoEmProblema @in_CodigoRisco = {2}, @in_indicaTransformacaoAutomatica = 0, @in_codigoUsuarioTransformacao = {3};"
                    , cDados.getDbName(), cDados.getDbOwner() , codigoRiscoQuestao, codigoUsuarioLogado);

                cDados.execSQL(comandoSQL, ref afetados);
            }
            else
                novoStatus = "";  // não fazer nada caso venha algum erro ou uma situação ainda não prevista
        }

        if (novoStatus.Length > 0)
        {
            // insere o novo comentário
            int afetados = 0;
            string comandoSQL = string.Format(
                @"  DECLARE @DataAtual DateTime
                    SET @DataAtual = GETDATE()

                    INSERT INTO {0}.{1}.ComentarioRiscoQuestao (CodigoRiscoQuestao, DescricaoComentario, DataComentario, CodigoUsuarioComentario)
                        VALUES ({2}, '{3}', @DataAtual, {5} )
                    UPDATE {0}.{1}.[RiscoQuestao] 
                    SET   [CodigoStatusRiscoQuestao]     = '{4}'
                        , [DataStatusRiscoQuestao]       = @DataAtual 
                        , [CodigoUsuarioUltimaAlteracao] = {5}
                        , [DataUltimaAlteracao]          = @DataAtual 
                          {6}
                    WHERE [CodigoRiscoQuestao] = {2} 
                ", cDados.getDbName(), cDados.getDbOwner()
                 , codigoRiscoQuestao, comentario.Replace("'", "''"), novoStatus, codigoUsuarioLogado, updateIndicaRiscoQuestao);

            cDados.execSQL(comandoSQL, ref afetados);
        } // if (novoStatus.Length>0)
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
        if (e.Column != null)
        {
            /*Questão = Problema  baixo medio alto
              Risco   = Risco     baixo medio alto*/
            string txtCelula = e.Value != null ? e.Value.ToString() : "";

            if (e.Column.Name == "col_ImpactoUrgencia" && e.RowType == GridViewRowType.Header)
            {
                //impacto alto urgencia alta
                //problema urgencia
                e.TextValue = (indRiscoQuestao == 'R') ? Resources.traducao.riscos_impacto : Resources.traducao.riscos_urg_ncia;

            }

            if (e.Column.Name == "col_ProbabilidadePrioridade" && e.RowType == GridViewRowType.Header)
            {
                e.TextValue = (indRiscoQuestao == 'R') ? "Probabilidade" : Resources.traducao.riscos_prioridade;
            }

            if (e.Column.Name == "col_DescricaoTipoRiscoQuestao" && e.RowType == GridViewRowType.Header)
            {
                e.Text = (indRiscoQuestao == 'R') ? "Tipo de Risco" : string.Format("Tipo de {0}", labelQuestao);
                e.TextValue = (indRiscoQuestao == 'R') ? "Tipo de Risco" : string.Format("Tipo de {0}", labelQuestao);
            }

            //if (e.Column.Name == "col_ProbabilidadePrioridade" && e.RowType == GridViewRowType.Data)
            //{
            //    if (txtCelula == "1")
            //    {
            //        e.Text = "Baixa";
            //        e.TextValue = "Baixa";
            //    }
            //    if (txtCelula == "2")
            //    {
            //        e.Text = "Média";
            //        e.TextValue = "Média";
            //    }
            //    if (txtCelula == "3")
            //    {
            //        e.Text = "Alta";
            //        e.TextValue = "Alta";
            //    }
            //}

            //if (e.Column.Name == "col_ImpactoUrgencia" && e.RowType == GridViewRowType.Data)
            //{
            //    if (txtCelula == "1")
            //    {
            //        e.Text = (indRiscoQuestao == 'R') ? "Baixa" : "Baixo";
            //        e.TextValue = (indRiscoQuestao == 'R') ? "Baixa" : "Baixo";
            //    }
            //    if (txtCelula == "2")
            //    {
            //        e.Text = (indRiscoQuestao == 'R') ? "Média" : "Médio";
            //        e.TextValue = (indRiscoQuestao == 'R') ? "Média" : "Médio";
            //    }
            //    if (txtCelula == "3")
            //    {
            //        e.Text = (indRiscoQuestao == 'R') ? "Alta" : "Alto";
            //        e.TextValue = (indRiscoQuestao == 'R') ? "Alta" : "Alto";
            //    }
            //}

            if (e.Column.Name == "col_Severidade" && e.RowType == GridViewRowType.Data)
            {
                if (txtCelula == "1" || txtCelula == "2" || txtCelula == "3")
                {
                    e.Text = Resources.traducao.riscos_baixa;
                    e.TextValue = Resources.traducao.riscos_baixa;
                }
                if (txtCelula == "4" || txtCelula == "5")
                {
                    e.Text = Resources.traducao.riscos_m_dia;
                    e.TextValue = Resources.traducao.riscos_m_dia;
                }
                if (txtCelula == "6" || txtCelula == "7" || txtCelula == "8" || txtCelula == "9")
                {
                    e.Text = Resources.traducao.riscos_alta;
                    e.TextValue = Resources.traducao.riscos_alta;
                }
            }
            if (e.Column.Name == "colStatus" && e.RowType == GridViewRowType.Data)
            {
                e.Text = e.Text == Resources.traducao.riscos_aberta ? Resources.traducao.riscos_aberto : e.Text;
                e.TextValue = e.TextValue.ToString() == Resources.traducao.riscos_aberta ? Resources.traducao.riscos_aberto : e.TextValue;

            }
            if (e.Column.Name == "CorRiscoQuestao" && e.RowType == GridViewRowType.Data)
            {
                e.BrickStyle.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);

                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                e.Text = "l";
                e.TextValue = "l";

                if (e.Value.ToString().Equals("Vermelho"))
                {
                    e.BrickStyle.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Equals("Amarelo"))
                {
                    e.BrickStyle.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Equals("Verde"))
                {
                    e.BrickStyle.ForeColor = Color.Green;
                }
                else if (e.Value.ToString().Equals("Azul"))
                {
                    e.BrickStyle.ForeColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.BrickStyle.ForeColor = Color.WhiteSmoke;
                }
                else if (e.Value.ToString().Equals("Laranja"))
                {
                    e.BrickStyle.ForeColor = Color.Orange;
                }
                else
                {
                    e.Text = " ";
                    e.TextValue = " ";
                }
            }
        }
    }

    private int[] getParticipantesEvento()
    {
        DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtiva(codigoEntidadeUsuarioResponsavel.ToString(), "");

        int[] convidados = new int[dsConvidados.Tables[0].Rows.Count];

        if (cDados.DataSetOk(dsConvidados))
        {
            int i = 0;
            foreach (DataRow dr in dsConvidados.Tables[0].Rows)
            {
                convidados[i] = int.Parse(dr["CodigoUsuario"].ToString());
                i++;
            }
        }

        return convidados;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        /*Questão = Problema
          Risco = Risco*/

        if ('Q' == indRiscoQuestao)//problema
        {
            gvDados.Columns["ProbabilidadePrioridade"].Caption = Resources.traducao.riscos_prioridade;
            gvDados.Columns["ImpactoUrgencia"].Caption = Resources.traducao.riscos_impacto;


            //Tratamento de Pronome para Masculino e Feminino em PT e em inglês utiliza-se o "The"
            if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
            {
                gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = string.Format("Consequence {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
                gvDados.Columns["TratamentoRiscoQuestao"].Caption = string.Format("Treatment {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
                gvDados.Columns["DetalheRiscoQuestao"].Caption = string.Format("Details {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
                gvDados.Columns["DescricaoRiscoQuestao"].Caption = string.Format("description {0} {1}", generoQuestao == "M" ? "of" : "of", labelQuestao);
            }
            else
            {
                gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = string.Format("Consequência d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["TratamentoRiscoQuestao"].Caption = string.Format("Tratamento d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["DetalheRiscoQuestao"].Caption = string.Format("Detalhes d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["DescricaoRiscoQuestao"].Caption = string.Format("Descrição d{0} {1}", generoQuestao == "M" ? "o" : "a", labelQuestao);
            }

        }
        else
        {
            gvDados.Columns["ProbabilidadePrioridade"].Caption = Resources.traducao.riscos_probabilidade;
            gvDados.Columns["ImpactoUrgencia"].Caption = Resources.traducao.riscos_urg_ncia;
            gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = Resources.traducao.riscos_consequ_ncia_do_risco;
            gvDados.Columns["TratamentoRiscoQuestao"].Caption = Resources.traducao.riscos_tratamento_do_risco;
            gvDados.Columns["DetalheRiscoQuestao"].Caption = Resources.traducao.riscos_detalhes_do_risco;
            gvDados.Columns["DescricaoRiscoQuestao"].Caption = Resources.traducao.riscos_descri__o_do_risco;
        }

        gvDados.Columns["DescricaoTipoRiscoQuestao"].Visible = true;
        gvDados.Columns["DetalheRiscoQuestao"].Visible = true;
        gvDados.Columns["DetalheRiscoQuestao"].VisibleIndex = 1;

        gvDados.Columns["ProbabilidadePrioridade"].Visible = true;
        gvDados.Columns["ImpactoUrgencia"].Visible = true;
        gvDados.Columns["Severidade"].Visible = true;
        gvDados.Columns["ConsequenciaRiscoQuestao"].Visible = true;
        gvDados.Columns["TratamentoRiscoQuestao"].Visible = true;
        gvDados.Columns["NomeProjeto"].Visible = true;
        gvDados.Columns["NomeProjeto"].VisibleIndex = 0;

        gvDados.Columns["acao"].Visible = false;
        gvDados.Columns["CorRiscoQuestao"].Visible = false;


        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });

        gvDados.Columns["DescricaoTipoRiscoQuestao"].Visible = false;
        gvDados.Columns["DetalheRiscoQuestao"].Visible = false;
        gvDados.Columns["ProbabilidadePrioridade"].Visible = false;
        gvDados.Columns["ImpactoUrgencia"].Visible = false;
        gvDados.Columns["Severidade"].Visible = false;
        gvDados.Columns["ConsequenciaRiscoQuestao"].Visible = false;
        gvDados.Columns["TratamentoRiscoQuestao"].Visible = false;
        gvDados.Columns["NomeProjeto"].Visible = false;
        gvDados.Columns["acao"].Visible = true;
        gvDados.Columns["CorRiscoQuestao"].Visible = true;

    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_FechaEdicao"] = "N";
        Session["Pesquisa"] = ddlResponsavel.Text;
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            ddlResponsavel.SelectedIndex = -1;
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            ddlResponsavel.SelectedIndex = -1;
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        else if (e.Parameter == "Transformar")
        {
            persisteAcaoNoRegistro(e.Parameter);
            pnCallback.JSProperties["cp_FechaEdicao"] = "S";
            //pcDados.ShowOnPageLoad = false;
        }

        else if (e.Parameter == "PesquisarResp")
        {
            bool mostrarel = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Incluir");
            //btnRelatorioRisco.ClientVisible = mostrarel;
            GerarRelatorioEmPDF1.ClientVisible = mostrarel && System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
            GerarRelatorioEmPDF2.ClientVisible = mostrarel && System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase); 
            GerarRelatorioEmPDF3.ClientVisible = mostrarel && System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase); 
            GerarRelatorioEmPDF4.ClientVisible = mostrarel && System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
            GerarRelatorioEmPDF5.ClientVisible = mostrarel && System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);

            hfGeral.Set("StatusSalvar", "-1"); // -1 indicar que o retorno não está relacionado com persistência
            hfGeral.Set("lovMostrarPopPup", "0"); // 0 indica que não precisa abrir popup de pesquisa

            string nomeUsuario = "";
            string codigoUsuario = "";
            cDados.getLov_NomeValor("usuario", "CodigoUsuario", "NomeUsuario", ddlResponsavel.Text, false, hfGeral.Get("hfWheregetLov_NomeValor").ToString(), "NomeUsuario", out codigoUsuario, out nomeUsuario);

            // se encontrou um único registro
            if (nomeUsuario != "")
            {
                ddlResponsavel.Text = nomeUsuario;
                hfGeral.Set("lovCodigoResponsavel", codigoUsuario);
            }
            else // mostrar popup
            {
                hfGeral.Set("lovMostrarPopPup", "1"); // 1 indica que precisa abrir popup de pesquisa
                hfGeral.Set("lovCodigoResponsavel", "");
            }
            gvDados.ClientVisible = false;
            return;
        }
        else if (e.Parameter == "Excel")
        {
            hfGeral.Set("StatusSalvar", "-1"); // -1 indicar que o retorno não está relacionado com persistência
            ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }
        else if ((e.Parameter == "Eliminar") || (e.Parameter == "Cancelar"))
            persisteAcaoNoRegistro(e.Parameter);

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        }
        else // alguma coisa deu errado...
        {
            if (mensagemErro_Persistencia.IndexOf("Erro Interno") > 0)
                throw new Microsoft.VisualBasic.CompilerServices.InternalErrorException();
            else
                hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }

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
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario();

            // se o parâmetro na base de dados estiver para publicar automaticamente
            // já grava o registro publicado.
            DataSet ds = cDados.getParametrosSistema("riscos_PublicacaoAutomatica");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                string AutoPub = ds.Tables[0].Rows[0]["riscos_PublicacaoAutomatica"].ToString();

                if (AutoPub.ToUpper().Equals("S"))
                {
                    oDadosFormulario.Add("CodigoUsuarioPublicacao", codigoUsuarioLogado);
                    oDadosFormulario.Add("DataPublicacao", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
                } // if (AutoPub.Equals("1") 
            } // if (cDados.DataSetOk(ds) && ...

            int novoCodigo = cDados.insert(nomeTabelaDb, oDadosFormulario, true);
            hfGeral.Set("CodigoRiscoQuestao", novoCodigo.ToString());
            populaGrid();
            gvDados.ExpandAll();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigo);
            gvDados.ClientVisible = false;
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();

        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = getDadosFormulario();

        cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
        populaGrid();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return "";
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();

        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DataExclusao", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
        oDadosFormulario.Add("CodigoUsuarioExclusao", codigoUsuarioLogado);

        cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
        populaGrid();
        return "";
    }



    protected void persisteAcaoNoRegistro(string acao)
    {
        try
        {
            string chave = getChavePrimaria();
            int codigoRiscoQuestao = 0;

            int.TryParse(chave, out codigoRiscoQuestao);

            //Tratamento de Pronome para Masculino e Feminino em PT e em inglês utiliza-se o "The"
            if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
            {
                if (0 == codigoRiscoQuestao)
                    throw new Exception(Resources.traducao.riscos_falha_interna_no_aplicativo__n_o_foi_poss_vel_localizar_o_identificador_ + (indRiscoQuestao == 'Q' ? string.Format(@"d{0} {1}.", generoQuestao == "M" ? "the" : "the", labelQuestao) : "of risk."));
            }
            else
            {
                if (0 == codigoRiscoQuestao)
                    throw new Exception(Resources.traducao.riscos_falha_interna_no_aplicativo__n_o_foi_poss_vel_localizar_o_identificador_ + (indRiscoQuestao == 'Q' ? string.Format(@"d{0} {1}.", generoQuestao == "M" ? "o" : "a", labelQuestao) : "do risco."));
            }

            processaAcao(codigoRiscoQuestao, acao);
            populaGrid();
            if (acao != "Transformar")
            {
                gvDados.ClientVisible = false;
            }

            hfAcao.Set("StatusAcao", "OK");
            hfAcao.Set("MensagemErro", "");
        }
        catch (Exception ex)
        {
            string msg = @"Falha ao atualizar as informações. Mensagem original do erro : " + ex.Message;
            hfAcao.Set("StatusAcao", "NOK");
            hfAcao.Set("MensagemErro", msg);
        }
    }

    #endregion

    #region GVDADOS

    private void populaGrid()
    {
        DataSet ds = cDados.getRiscosQuestoes(idProjeto, indRiscoQuestao);
        
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }


    private void populaGridComentario(int codigoRiscoQuestao)
    {
        DataSet ds = cDados.getComentarioRiscosQuestoes(codigoRiscoQuestao);

        if (cDados.DataSetOk(ds))
        {
            gvComentarios1.DataSource = ds;
            gvComentarios1.DataBind();
        }
    }

    #region gvDados

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        bool indicaQuestao = indRiscoQuestao.Equals('Q');

        bool PertenceAoProjeto = (Convert.ToInt32(gvDados.GetRowValues(e.VisibleIndex, "CodigoProjeto")) == idProjeto);
        bool podeEditar = indicaQuestao ? questoeEditar : riscoEditar;
        bool podeExcluir = indicaQuestao ? questoeExclui : riscoExclui;
        bool podeComentar = indicaQuestao ? questoeComentar : riscoComentar;

        gvDados.JSProperties["cpPodeEditar"] = podeEditar;

        if (e.ButtonID == "btnEditarCustom")
        {
            if ((podeEditar || podeComentar) && PertenceAoProjeto)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluirCustom")
        {
            if (podeExcluir && PertenceAoProjeto)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        /*probabilidadeprioridade 10
            impactourgencia 11*/
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "ProbabilidadePrioridade")
        {
            if (e.CellValue.ToString() == "1")
            {
                e.Cell.Text = "Baixo";
            }
            if (e.CellValue.ToString() == "2")
            {
                e.Cell.Text = "Médio";
            }
            if (e.CellValue.ToString() == "3")
            {
                e.Cell.Text = "Alto";
            }
        }

        //if (coluna.FieldName == "NomeProjeto")
        //{

        //}

        if (e.CellValue.ToString().Length > 60)
        {
            e.Cell.Text = e.CellValue.ToString().Substring(0, 60) + "  ...";
        }
    }

    #endregion

    #region gvComentarios

    protected void gvComentarios_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao") && (hfGeral.Get("TipoOperacao").ToString() == "Incluir" || hfGeral.Get("TipoOperacao").ToString() == "Editar"))
            somenteLeitura = false;

        bool indicaQuestao = indRiscoQuestao.Equals('Q');
        bool podeComentar = indicaQuestao ? questoeComentar : riscoComentar;
        gvComentarios1.Columns["colunaControlesOriginal"].Visible = !somenteLeitura && podeComentar;
        gvComentarios1.Columns["colunaControlesComentario"].Visible = !gvComentarios1.Columns["colunaControlesOriginal"].Visible;

        if (e.Parameters.Substring(0, 7) == "Popular")
        {
            string codigoRiscoQuestao = e.Parameters.Substring(7);
            populaGridComentario(int.Parse(codigoRiscoQuestao));
        }
    }

    protected void gvComentarios_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string codigoRiscoQuestao = getChavePrimaria();

        string comentario = e.NewValues["DescricaoComentario"] != null ? e.NewValues["DescricaoComentario"].ToString() : "";

        // insere o novo comentário
        string comandoSQL = string.Format(
            @"INSERT INTO {0}.{1}.ComentarioRiscoQuestao (DescricaoComentario, DataComentario, CodigoUsuarioComentario, CodigoRiscoQuestao )
                    VALUES ('{2}', GETDATE(), {3}, {4} )",
              cDados.getDbName(), cDados.getDbOwner(), comentario.Replace("'", "''"), codigoUsuarioLogado, codigoRiscoQuestao);

        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);

        populaGridComentario(int.Parse(codigoRiscoQuestao));

        e.Cancel = true;
        gvComentarios1.CancelEdit();
    }

    protected void gvComentarios_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string codigoRiscoQuestao = getChavePrimaria();
        int codigoComentario = int.Parse(e.Keys["CodigoComentario"].ToString());
        string comentario = e.NewValues["DescricaoComentario"] != null ? e.NewValues["DescricaoComentario"].ToString() : "";

        // atualiza o comentário
        string comandoSQL = string.Format(
            @"UPDATE {0}.{1}.ComentarioRiscoQuestao 
                 SET DescricaoComentario = '{2}'
               WHERE CodigoComentario = {3} ",
              cDados.getDbName(), cDados.getDbOwner(), comentario.Replace("'", "''"), codigoComentario);

        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);

        populaGridComentario(int.Parse(codigoRiscoQuestao));

        e.Cancel = true;
        gvComentarios1.CancelEdit();
    }

    protected void gvComentarios_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string codigoRiscoQuestao = getChavePrimaria();
        int codigoComentario = int.Parse(e.Keys["CodigoComentario"].ToString());
        // Exclui o comentário
        string comandoSQL = string.Format(
            @"DELETE FROM {0}.{1}.ComentarioRiscoQuestao 
               WHERE CodigoComentario = {2} ",
              cDados.getDbName(), cDados.getDbOwner(), codigoComentario);

        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);

        populaGridComentario(int.Parse(codigoRiscoQuestao));

        e.Cancel = true;
    }

    #endregion

    #endregion

    protected string ObtemHtmlBtnIncluir()
    {
        string htmlBtnIncluir = string.Empty;

        if (indRiscoQuestao.Equals('Q') ? questoeInclui : riscoInclui)
            htmlBtnIncluir = @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""abreRiscoQuestao();"" style=""cursor: pointer;""/>";
        else
            htmlBtnIncluir = @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>";

        return htmlBtnIncluir;
    }

    protected void gvDados_CustomGroupDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (gvDados.GetRowValues(e.VisibleIndex, "IndicaPrograma").ToString() == "S")
        {
            e.DisplayText = Resources.traducao.riscos_nome_do_programa__ + e.Value.ToString();

        }
        else
        {
            e.DisplayText = Resources.traducao.riscos_nome_do_projeto__ + e.Value.ToString();
        }


    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
        else
        {
            comboBox.DataSource = SqlDataSource1;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel
                                                           , e.Filter, "");

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "RisQuePrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), (indRiscoQuestao.Equals('Q') ? questoeInclui : riscoInclui), "abreRiscoQuestao();", true, true, false, "RisQuePrj", (indRiscoQuestao.Equals('Q') ? "Questões" : "Riscos"), this);
    }

    #endregion
    protected void cbRelatorioPDF_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        bool mostrarel = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Incluir");
        //btnRelatorioRisco.ClientVisible = mostrarel;
        GerarRelatorioEmPDF1.ClientVisible = mostrarel;
        GerarRelatorioEmPDF2.ClientVisible = mostrarel;
        GerarRelatorioEmPDF3.ClientVisible = mostrarel;
        GerarRelatorioEmPDF4.ClientVisible = mostrarel;
        GerarRelatorioEmPDF5.ClientVisible = mostrarel;

        GerarRelatorioEmPDF1.ClientVisible = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        GerarRelatorioEmPDF2.ClientVisible = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        GerarRelatorioEmPDF3.ClientVisible = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        GerarRelatorioEmPDF4.ClientVisible = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        GerarRelatorioEmPDF5.ClientVisible = System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        

        rel_RiscosProjetos relatorio;

        string montaNomeArquivo = "";

        int codigoRiscoQuestaoSelecionada = int.Parse(getChavePrimaria());

        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("RQ");

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        relatorio = new rel_RiscosProjetos(codigoEntidadeUsuarioResponsavel);
        byte[] vetorBytes = null;

        DataSet ds = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["LogoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["LogoUnidadeNegocio"].ToString() != "")
            {
                vetorBytes = (byte[])ds.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            }
        }

        ASPxBinaryImage image1 = new ASPxBinaryImage();
        try
        {
            image1.ContentBytes = vetorBytes;

            if (image1.ContentBytes != null)
            {
                montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "/ArquivosTemporarios/" + "logo" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim(' ') + ".png";
                FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                fs.Close();
                fs.Dispose();
            }
        }
        catch
        {

        }

        relatorio.Parameters["pCodigoRiscoQuestaoSelecionada"].Value = codigoRiscoQuestaoSelecionada;
        relatorio.Parameters["pCodigoTipoAssociacao"].Value = codigoTipoAssociacao;
        relatorio.Parameters["pPathLogo"].Value = montaNomeArquivo;
        relatorio.Parameters["pVetorBytes"].Value = vetorBytes;

        MemoryStream stream = new MemoryStream();
        relatorio.ExportToPdf(stream);
        Session["exportStream"] = stream;

    }

    protected void gvComentarios1_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }
    protected void gvComentarios1_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        string codigoRiscoQuestao = getChavePrimaria();
        populaGridComentario(int.Parse(codigoRiscoQuestao));
    }

    protected void pnRiscoAssociadoSuperior_Callback(object sender, CallbackEventArgsBase e)
    {
        //(hfGeral.Get("CodigoRiscoQuestao") + '|' + CodigoRiscoQuestaoSuperior + '|' + TipoOperacao);
        string operacao = hfGeral.Get("TipoOperacao") as string;
        bool podeEditar = ((!gvDados.JSProperties.ContainsKey("cpPodeEditar")) || ((bool)gvDados.JSProperties["cpPodeEditar"]));
        bool BoolEnabled = operacao != "Consultar" && podeEditar;
        string[] chaves = e.Parameter.Split('|');
        string chave = chaves[0];
        string chavesuperior = chaves[1];
        int chaveSuperior = -1;
        int.TryParse(chavesuperior, out chaveSuperior);
        string tipoOperacao = chaves[2];

        DataSet ds = cDados.getRiscosQuestoes(idProjeto, indRiscoQuestao);

        DataSet dsdestino = ds.Clone();

        if (!string.IsNullOrEmpty(chave))
        {
            DataRow[] dr = ds.Tables[0].Select("[CodigoRiscoQuestao] <> " + chave);

            for (int i = 0; i < dr.Length; i++)
            {
                dsdestino.Tables[0].ImportRow(dr[i]);
            }


            dsdestino.AcceptChanges();

            ddlRiscoAssociado.TextField = "DescricaoRiscoQuestao";
            ddlRiscoAssociado.ValueField = "CodigoRiscoQuestao";

            ddlRiscoAssociado.DataSource = tipoOperacao == "Editar" ? dsdestino : ds;
            ddlRiscoAssociado.DataBind();

            //inserido na posições 0 do DDL, texto: "Selecione a empresa", e valor: "0".
            ddlRiscoAssociado.Items.Insert(0, new DevExpress.Web.ListEditItem(Resources.traducao.riscos_selecione_risco__superior_, ""));

            ddlRiscoAssociado.SelectedItem = ddlRiscoAssociado.Items.FindByValue(chaveSuperior);
            ddlRiscoAssociado.ClientEnabled = BoolEnabled;
        }
    }

    protected void gvDados_BeforeGetCallbackResult(object sender, EventArgs e)
    {
        SetItemCount();
    }

    protected void gvDados_DataBound(object sender, EventArgs e)
    {
        SetItemCount();
    }

    protected void gvDados_PreRender(object sender, EventArgs e)
    {
        SetItemCount();
    }

    void SetItemCount()
    {
        int itemCount = (int)gvDados.GetTotalSummaryValue(gvDados.TotalSummary["DescricaoRiscoQuestao"]);
        gvDados.SettingsPager.Summary.Text = "Page {0} of {1} (" + itemCount.ToString() + " items)";
    }

    protected void gvDados_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        bool indicaQuestao = indRiscoQuestao.Equals('Q');
        bool podeEliminar = indicaQuestao ? questoeEliminar : riscoEliminar;
        bool podeCancelar = indicaQuestao ? questoeCancelar : riscoCancelar;
        e.Properties["cp_podeEliminar"] = podeEliminar;
        e.Properties["cp_podeCancelar"] = podeCancelar;

    }
}
