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
using System.IO;
using System.Web.Hosting;

public partial class espacoTrabalho_meusProblemas : System.Web.UI.Page
{
    dados cDados;

    private string nomeTabelaDb = "RiscoQuestao";
    private string whereUpdateDelete;
    private int idProjeto;
    private string idUsuario;
    private string idUsuarioLogado;
    private string CodigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;

    private char indRiscoQuestao;
    string labelQuestoes = "Questões";
    string labelQuestao = "Questão";
    string genero = "F";
    private ASPxGridView gvToDoList;

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

        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        CodigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();

        idUsuario = Request.QueryString["IDUsuario"].ToString();
        indRiscoQuestao = Request.QueryString["TT"].ToString()[0]; // TT = Tipo Tela, pode ser "R"isco ou "Q"uestao

        bool mostrarel = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Incluir");
        btnRelatorioRisco.ClientVisible = mostrarel;

        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());
        else
            idProjeto = -1;

        #region -- [Códigos p/ controle de acesso (Gerente Projeto ou Responsável Risco) ]

        //// código para controle se é o gerente do projeto ou o criador do risco que está 'editando o risco'
        hfGeral.Set("codigoUsuarioLogado", idUsuarioLogado);

        DataSet ds = cDados.getGerenteProjeto(int.Parse(idUsuario));
        int codigoGerenteProjeto = -1;
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            int.TryParse(ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString(), out codigoGerenteProjeto);
        hfGeral.Set("codigoGerenteProjeto", codigoGerenteProjeto);

        #endregion
        hfGeral.Set("idiomaEPortugues", System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        carregaPlanoAcao();
        cDados.aplicaEstiloVisual(Page);
        HeaderOnTela();
        
        cDados.setaTamanhoMaximoMemo(txtConsequencia, 2000, lblContadorMemoConsequencias);
        cDados.setaTamanhoMaximoMemo(txtEstrategiaTratamento, 2000, lblContadorMemoTratamento);
        cDados.setaTamanhoMaximoMemo(mmComentarioAcao, 2000, lblContadorMemoComentarioAcao);
        if (!IsPostBack)
        {
            pcAbas.ActiveTabIndex = 0;
        }
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        if (indRiscoQuestao == 'Q')
        {
            DataSet dsParametro = cDados.getParametrosSistema("labelQuestoes", "labelQuestao", "lblGeneroLabelQuestao");

            if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
            {
                labelQuestoes = dsParametro.Tables[0].Rows[0]["labelQuestoes"].ToString();
                labelQuestao = dsParametro.Tables[0].Rows[0]["labelQuestao"].ToString();
                genero = dsParametro.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();
            }


            gvDados.Columns[2].Caption = labelQuestao;
            gvDados.Columns["DescricaoRiscoQuestao"].Caption = labelQuestao;
            gvDados.Columns["DataLimiteResolucao"].Caption = Resources.traducao.limite_resolu__o;
            gvDados.Columns["DataEliminacaoCancelamento"].Caption = Resources.traducao.data_resolu__o;

            ((GridViewDataTextColumn)gvDados.Columns["DataStatusRiscoQuestao"]).Caption = string.Format(@"Data Status Risco {0}", labelQuestao);
            ((GridViewDataTextColumn)gvDados.Columns["DescricaoTipoRiscoQuestao"]).Caption = string.Format(@"Descrição Tipo Risco {0}", labelQuestao);
            ((GridViewDataTextColumn)gvDados.Columns["ConsequenciaRiscoQuestao"]).Caption = string.Format(@"Consequencia Risco {0}", labelQuestao);
            ((GridViewDataTextColumn)gvDados.Columns["TratamentoRiscoQuestao"]).Caption = string.Format(@"Tratamento Risco {0}", labelQuestao);



            pcAbas.TabPages[0].Text = labelQuestao;
            //pcAbas.TabPages.FindByName("tabPageTratamento").Visible = false;
            pnMemosTratamento.Visible = true;

            lblRiscoQuestao.Text = labelQuestao + ":";
            lblProbabilidadeUrgencia.Text = Resources.traducao.urg_ncia_;
            lblImpactoPrioridade.Text = Resources.traducao.prioridade_;

            lblLimiteEliminacaoResolucao.Text = Resources.traducao.limite_resolu__o;
            lblEliminacaoResolucaoCancelamento.Text = Resources.traducao.riscos_resolu__o_canc;
        }
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();
        populaGrid();
        populaDDLTipo();

        if (!IsPostBack && !IsCallback)
        {
            AtribuiFiltroGrid();
        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/meusProblemas.js""></script>"));
        this.TH(this.TS("meusProblemas"));
    }

    private void carregaPlanoAcao()
    {
        // inclui as funcionalidades do plano de ação.
        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("RQ");
        int codigoObjetoAssociado = -1;
        if (hfGeral.Contains("codigoObjetoAssociado"))
            codigoObjetoAssociado = int.Parse(hfGeral.Get("codigoObjetoAssociado").ToString());

        bool problemaEditar = true;
        if (gvDados.FocusedRowIndex > -1)
        {
            int codigoProjeto = (int)gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjeto");
            problemaEditar = cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade), codigoProjeto, "null", "PR", 0, "null", "PR_AltRQ2");
        }
        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao"))
            somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");

        somenteLeitura = somenteLeitura || (!problemaEditar);

        int[] convidados = getParticipantesEvento();

        Unit tamanho = new Unit("100%");

        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, int.Parse(CodigoEntidade), int.Parse(idUsuarioLogado), null, codigoTipoAssociacao, codigoObjetoAssociado, tamanho, 100, somenteLeitura, convidados.Length == 0 ? null : convidados, true, txtRisco.Text);
        
        pcAbas.TabPages.FindByName("tabPageToDoList").FindControl("Content4Div").Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario());

        gvToDoList = myPlanoDeAcao.gvToDoList;
        gvToDoList.Settings.VerticalScrollableHeight = 170;
        gvToDoList.Font.Name = "Verdana";
        gvToDoList.Font.Size = new FontUnit("8pt");
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtRisco.GetText());}";
        gvToDoList.DataBind();
    }

    private void HeaderOnTela()
    {
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/meusProblemas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        /*string comando = string.Format(@"<script type='text/javascript'>onloadDesabilitaBarraNavegacao();</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);*/
    }

    #region VARIOS

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();

        oDadosFormulario.Add("DescricaoRiscoQuestao", txtRisco.Text);
        oDadosFormulario.Add("DetalheRiscoQuestao", txtDescricao.Text);
        oDadosFormulario.Add("CodigoTipoRiscoQuestao", ddlTipo.Value.ToString());
        oDadosFormulario.Add("ProbabilidadePrioridade", getIndexProbabilidadeImpacto(ddlProbabilidade.SelectedIndex));
        oDadosFormulario.Add("ImpactoUrgencia", getIndexProbabilidadeImpacto(ddlImpacto.SelectedIndex));
        oDadosFormulario.Add("CodigoUsuarioResponsavel", hfGeral.Get("lovCodigoResponsavel").ToString());
        oDadosFormulario.Add("ConsequenciaRiscoQuestao", txtConsequencia.Text);
        oDadosFormulario.Add("TratamentoRiscoQuestao", txtEstrategiaTratamento.Text);
        oDadosFormulario.Add("CodigoUsuarioInclusao", idUsuarioLogado);
        oDadosFormulario.Add("DataInclusao", "GETDATE()");
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        oDadosFormulario.Add("CodigoProjeto", hfGeral.Get("CodigoProjeto").ToString());
        oDadosFormulario.Add("IndicaRiscoQuestao", indRiscoQuestao.ToString());
        oDadosFormulario.Add("DataStatusRiscoQuestao", "GETDATE()");

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

    private int getIndexProbabilidadeImpacto(int indexCombo)
    {
        if (indexCombo + 1 == 1)
            return 3; // Alto
        else if (indexCombo + 1 == 3)
            return 1; //Baixo
        else
            return 2; // Médio
    }

    private void processaAcao(int codigoRiscoQuestao, string acao)
    {
        string comentario = mmComentarioAcao.Text;
        string novoStatus = string.Empty;

        if ('Q' == indRiscoQuestao)
        {
            if (acao.Equals("Eliminar"))
                novoStatus = "QR";  // questão resolvida
            else if (acao.Equals("Cancelar"))
                novoStatus = "QC";  // questão cancelada
            else if (acao.Equals("Excluir"))
                novoStatus = "QX";  // questão excluída
            else
                novoStatus = "";  // não fazer nada caso venha algum erro ou uma situação ainda não prevista
        } // if ("Q" == indRiscoQuestao)
        else
        {
            if (acao.Equals("Eliminar"))
                novoStatus = "RE";  // questão resolvida
            else if (acao.Equals("Cancelar"))
                novoStatus = "RC";  // questão cancelada
            else if (acao.Equals("Excluir"))
                novoStatus = "RX";  // questão excluída
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
                    WHERE [CodigoRiscoQuestao] = {2} 
                ", cDados.getDbName(), cDados.getDbOwner()
                 , codigoRiscoQuestao, comentario.Replace("'", "''"), novoStatus, idUsuarioLogado);

            cDados.execSQL(comandoSQL, ref afetados);
        } // if (novoStatus.Length>0)
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int altura, nAlturaDivs;
        string alturaDivPopus;
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        altura = (alturaPrincipal - 190);
        gvDados.Settings.VerticalScrollableHeight = altura - 190;

        if (alturaPrincipal <= 768)
        {
            alturaDivPopus = "260px";
        }
        else if ((alturaPrincipal >= 769) && (alturaPrincipal <= 800))
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

        nAlturaDivs = int.Parse(alturaDivPopus.Replace("px", "")) - 81;
        gvComentarios.SettingsPager.Visible = false;
        gvComentarios.Settings.VerticalScrollableHeight = nAlturaDivs -3;
        gvToDoList.Settings.VerticalScrollableHeight = nAlturaDivs - 18;
    }

    private void AtribuiFiltroGrid()
    {
        gvDados.FilterExpression += " DescricaoStatusRiscoQuestao = '" + (indRiscoQuestao == 'R' ? "Ativo" : Resources.traducao.riscos_aberta) + "'";

        if (Request.QueryString["Publicado"] != null)
            gvDados.FilterExpression += " AND Publicado = '" + Request.QueryString["Publicado"] + "'";

        if (Request.QueryString["Cor"] != null)
            gvDados.FilterExpression += " AND CorRiscoQuestao = '" + Request.QueryString["Cor"] + "'";

        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao"))
            somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");
    }

    private int[] getParticipantesEvento()
    {
        DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtiva(CodigoEntidade.ToString(), "");

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

    #endregion
    
    private void defineLabelsGrid()
    {
        /*Questão = Problema
          Risco = Risco*/

        if ('Q' == indRiscoQuestao)//problema
        {

            gvDados.Columns["ProbabilidadePrioridade"].Caption = "Prioridade";
            gvDados.Columns["ImpactoUrgencia"].Caption = "Impacto";

            //Tratamento de Pronome para Masculino e Feminino em PT e em inglês utiliza-se o "The"
            if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
            {
                gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = string.Format(@"Consequência d{0} {1}", genero == "M" ? "the" : "the", labelQuestao);
                gvDados.Columns["TratamentoRiscoQuestao"].Caption = string.Format(@"Tratamento d{0} {1}", genero == "M" ? "the" : "the", labelQuestao);
                gvDados.Columns["DetalheRiscoQuestao"].Caption = string.Format(@"Detalhes d{0} {1}", genero == "M" ? "the" : "the", labelQuestao);
                gvDados.Columns["DescricaoRiscoQuestao"].Caption = string.Format(@"Descrição d{0} {1}", genero == "M" ? "the" : "the", labelQuestao);
            }
            else
            {
                gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = string.Format(@"Consequência d{0} {1}", genero == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["TratamentoRiscoQuestao"].Caption = string.Format(@"Tratamento d{0} {1}", genero == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["DetalheRiscoQuestao"].Caption = string.Format(@"Detalhes d{0} {1}", genero == "M" ? "o" : "a", labelQuestao);
                gvDados.Columns["DescricaoRiscoQuestao"].Caption = string.Format(@"Descrição d{0} {1}", genero == "M" ? "o" : "a", labelQuestao);
            }





        }
        else
        {
            gvDados.Columns["ProbabilidadePrioridade"].Caption = "Probabilidade";
            gvDados.Columns["ImpactoUrgencia"].Caption = "Urgência";
            gvDados.Columns["ConsequenciaRiscoQuestao"].Caption = "Consequência do Risco";
            gvDados.Columns["TratamentoRiscoQuestao"].Caption = "Tratamento do Risco";
            gvDados.Columns["DetalheRiscoQuestao"].Caption = "Detalhes do Risco";
            gvDados.Columns["DescricaoRiscoQuestao"].Caption = "Descrição do Risco";
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
    
    #region GRID

    //GVDADOS

    private void populaGrid()
    {
        DataSet ds = cDados.getRiscosQuestoes(null, indRiscoQuestao, int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade));

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Filter)
            return;

        bool problemaEditar;
        bool problemaExclui;
        int idProjeto = -1;
        if (gvDados.GetRowValues(e.VisibleIndex, "CodigoProjeto") != null)
        {
            idProjeto = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "CodigoProjeto").ToString());
        }
        problemaEditar = cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade), idProjeto, "null", "PR", 0, "null", "PR_AltRQ2");
        problemaExclui = cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(CodigoEntidade), idProjeto, "null", "PR", 0, "null", "PR_ExcQR2");

        gvDados.JSProperties["cpPodeEditar"] = problemaEditar;

        if (e.ButtonID == "btnEditarCustom")
        {
            e.Enabled = true;
            e.Image.Url = "~/imagens/botoes/editarReg02.PNG";
        }
        else
        {
            if (e.ButtonID == "btnExcluirCustom")
            {
                if (problemaExclui == true)
                {
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/excluirReg02.png";
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
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
    }

    //GVCOMENTARIOS

    private void populaGridComentario(int codigoRiscoQuestao)
    {
        DataSet ds = cDados.getComentarioRiscosQuestoes(codigoRiscoQuestao);

        if (cDados.DataSetOk(ds))
        {
            gvComentarios.DataSource = ds;
            gvComentarios.DataBind();
        }
    }

    protected void gvComentarios_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        bool somenteLeitura = true;
        if (hfGeral.Contains("TipoOperacao") && (hfGeral.Get("TipoOperacao").ToString() == "Incluir" || hfGeral.Get("TipoOperacao").ToString() == "Editar"))
            somenteLeitura = false;

        int codigoProjeto = (int)gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjeto");
        gvComentarios.Columns["colunaControlesOriginal"].Visible = !somenteLeitura;
        gvComentarios.Columns["colunaControlesComentario"].Visible = !gvComentarios.Columns["colunaControlesOriginal"].Visible;
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
              cDados.getDbName(), cDados.getDbOwner(), comentario.Replace("'", "''"), idUsuarioLogado, codigoRiscoQuestao);

        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);

        populaGridComentario(int.Parse(codigoRiscoQuestao));

        e.Cancel = true;
        gvComentarios.CancelEdit();
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
        gvComentarios.CancelEdit();
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

    #region GRIDVIEWEXPORT

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        /*Questão = Problema  baixo medio alto
          Risco   = Risco     baixo medio alto*/
        string txtCelula = e.Value != null ? e.Value.ToString() : "";

        if (e.Column.Name == "col_ProbabilidadePrioridade" && e.RowType == GridViewRowType.Header)
        {
            e.TextValue = (indRiscoQuestao == 'R') ? "Probabilidade" : "Prioridade";
        }

        if (e.Column.Name == "col_DescricaoTipoRiscoQuestao" && e.RowType == GridViewRowType.Header)
        {
            e.Text = (indRiscoQuestao == 'R') ? "Tipo de Risco" : string.Format("Tipo de {0}", labelQuestao);
            e.TextValue = (indRiscoQuestao == 'R') ? "Tipo de Risco" : string.Format("Tipo de {0}", labelQuestao);
        }

        if (e.Column.Name == "col_ProbabilidadePrioridade" && e.RowType == GridViewRowType.Data)
        {
            if (txtCelula == "1")
            {
                e.Text = "Baixa";
                e.TextValue = "Baixa";
            }
            if (txtCelula == "2")
            {
                e.Text = "Média";
                e.TextValue = "Média";
            }
            if (txtCelula == "3")
            {
                e.Text = "Alta";
                e.TextValue = "Alta";
            }
        }
        if (e.Column.Name == "col_ImpactoUrgencia" && e.RowType == GridViewRowType.Header)
        {
            //impacto alto urgencia alta
            //problema urgencia
            e.TextValue = (indRiscoQuestao == 'R') ? "Impacto" : "Urgência";

        }
        if (e.Column.Name == "col_ImpactoUrgencia" && e.RowType == GridViewRowType.Data)
        {
            if (txtCelula == "1")
            {
                e.Text = (indRiscoQuestao == 'R') ? "Baixa" : "Baixo";
                e.TextValue = (indRiscoQuestao == 'R') ? "Baixa" : "Baixo";
            }
            if (txtCelula == "2")
            {
                e.Text = (indRiscoQuestao == 'R') ? "Média" : "Médio";
                e.TextValue = (indRiscoQuestao == 'R') ? "Média" : "Médio";
            }
            if (txtCelula == "3")
            {
                e.Text = (indRiscoQuestao == 'R') ? "Alta" : "Alto";
                e.TextValue = (indRiscoQuestao == 'R') ? "Alta" : "Alto";
            }
        }

        if (e.Column.Name == "col_Severidade" && e.RowType == GridViewRowType.Data)
        {
            if (txtCelula == "1" || txtCelula == "2" || txtCelula == "3")
            {
                e.Text = "Baixa";
                e.TextValue = "Baixa";
            }
            if (txtCelula == "4" || txtCelula == "5")
            {
                e.Text = "Média";
                e.TextValue = "Média";
            }
            if (txtCelula == "6" || txtCelula == "7" || txtCelula == "8" || txtCelula == "9")
            {
                e.Text = "Alta";
                e.TextValue = "Alta";
            }
        }
        if (e.Column.Name == "colStatus" && e.RowType == GridViewRowType.Data)
        {
            e.Text = e.Text == "Aberta" ? "Aberto" : e.Text;
            e.TextValue = e.TextValue.ToString() == "Aberta" ? "Aberto" : e.TextValue;

        }
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }
        
    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        else if (e.Parameter == "PesquisarResp")
        {
            bool mostrarel = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Incluir");
            btnRelatorioRisco.ClientVisible = mostrarel;

            hfGeral.Set("StatusSalvar", "-1"); // -1 indicar que o retorno não está relacionado com persistência
            hfGeral.Set("lovMostrarPopPup", "0"); // 0 indica que não precisa abrir popup de pesquisa

            string nomeUsuario = "";
            string codigoUsuario = "";
            cDados.getLov_NomeValor("usuario", "CodigoUsuario", "NomeUsuario", txtResponsavel.Text, false, "AND DataExclusao is null", "NomeUsuario", out codigoUsuario, out nomeUsuario);

            // se encontrou um único registro
            if (nomeUsuario != "")
            {
                txtResponsavel.Text = nomeUsuario;
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

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();

        //// Lê as informações disponíveis no formulário
        //ListDictionary oDadosFormulario = getDadosFormulario();

        //cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
        populaGrid();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return "";
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   
        // busca a chave primaria
        string chave = getChavePrimaria();
        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DataExclusao", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
        oDadosFormulario.Add("CodigoUsuarioExclusao",  idUsuarioLogado);
        cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
        populaGrid();
        return "";
    }

    #endregion

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
                    throw new Exception("Falha interna no aplicativo. Não foi possível localizar o identificador " + (indRiscoQuestao == 'Q' ? string.Format(@"d{0} {1}.", genero == "M" ? "the" : "the", labelQuestao) : "of risk."));
            }
            else
            {
                if (0 == codigoRiscoQuestao)
                    throw new Exception("Falha interna no aplicativo. Não foi possível localizar o identificador " + (indRiscoQuestao == 'Q' ? string.Format(@"d{0} {1}.", genero == "M" ? "o" : "a", labelQuestao) : "do risco."));
            }


            processaAcao(codigoRiscoQuestao, acao);
            populaGrid();
            gvDados.ClientVisible = false;

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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        defineLabelsGrid();
    }
    protected void txtEstrategiaTratamento_TextChanged(object sender, EventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (((source as ASPxMenu).Parent as GridViewHeaderTemplateContainer).Grid.ID == "gvComentarios")
        {
            int codigo = getChavePrimaria() == "" ? -1 : int.Parse(getChavePrimaria());
            populaGridComentario(codigo);
        }

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstQuestoes");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "LstQuestoes", labelQuestoes, this);
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "gvComentarios.AddNewRow()", true, true, false, "LstQuestoes", labelQuestoes, this);
    }

    #endregion
    
    protected void ASPxGridViewExporter1_RenderBrick1(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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

        if (e.Column.Name == "CorRiscoQuestao" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (e.Value.ToString().Equals("VermelhoOk"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Equals("VerdeOk"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Equals("Verde"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Equals("Laranja"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Orange;
            }
            else if (e.Value.ToString().Equals("Vermelho"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Equals("Amarelo"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Equals("Branco"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }
    protected void cbExportacao_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        bool mostrarel = (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao").ToString() != "Incluir");
        btnRelatorioRisco.ClientVisible = mostrarel;

        rel_RiscosProjetos relatorio;

        string montaNomeArquivo = "";

        int codigoRiscoQuestaoSelecionada = int.Parse(getChavePrimaria());

        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("RQ");


        relatorio = new rel_RiscosProjetos(int.Parse(CodigoEntidade));
        byte[] vetorBytes = null;

        DataSet ds = cDados.getLogoEntidade(int.Parse(CodigoEntidade), "");

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
}
