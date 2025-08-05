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
using System.Drawing;

public partial class _Projetos_DadosProjeto_ItensBacklog_antigo : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    bool podeEditar = true;
    bool podeExcluir = true;
    public int alturaFrameAnexos = 372;

    protected void Page_Init(object sender, EventArgs e)
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        cDados.setaTamanhoMaximoMemo(txtDetalheItem, 4000, lblContadorMemoDescricao);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();

        cDados.setaTamanhoMaximoMemo(txtDetalheItem, 4000, lblContadorMemoDescricao);

        if (Request.QueryString["IDProjeto"] != null)
        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }
        gvDados.JSProperties["cp_CodigoProjeto"] = idProjeto.ToString();

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            //cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_IteBkl");
        }

        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(idProjeto, "-1", 1, true, false, false, percentualConcluido, data);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaDdlStatus();
        populaDdlCliente();
        populaDdlTipoTarefa();
        populaClassificacao();
        carregaGvDados();
        verificaVisibilidadeObjetos();

        cDados.aplicaEstiloVisual(Page);
    }

    private void populaDdlCliente()
    {
        DataSet ds = cDados.getFornecedores(codigoEntidadeUsuarioResponsavel, " AND pe.IndicaCliente = 'S' ");
        if (cDados.DataSetOk(ds))
        {
            ddlCliente.TextField = "NomePessoa";
            ddlCliente.ValueField = "CodigoPessoa";
            ddlCliente.DataSource = ds.Tables[0];
            ddlCliente.DataBind();
        }
    }

    private void populaDdlTipoTarefa()
    {

        DataSet ds = cDados.getTipoTarefasTimeSheet(codigoEntidadeUsuarioResponsavel, "");
        if (cDados.DataSetOk(ds))
        {
            ddlTipoAtividade.TextField = "DescricaoTipoTarefaTimeSheet";
            ddlTipoAtividade.ValueField = "CodigoTipoTarefaTimeSheet";
            ddlTipoAtividade.DataSource = ds.Tables[0];
            ddlTipoAtividade.DataBind();
        }

    }
    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getItensDoBackLog(idProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        bool bloqueia = true;
        string indicaBloqueio = (gvDados.GetRowValues(e.VisibleIndex, "IndicaBloqueioItem") != null) ? gvDados.GetRowValues(e.VisibleIndex, "IndicaBloqueioItem").ToString() : "S";
        bloqueia = (indicaBloqueio == "S");
        try
        {
            if (e.ButtonID == "btnExcluirCustom")
            {
                if (podeExcluir && bloqueia == false)
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
                if (podeEditar && bloqueia == false)
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

    #endregion

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ItensBacklog_antigo.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<title>TO DO List</title>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        if (alturaPrincipal > 900)
        {
            txtDetalheItem.Height = 40;
        }
        else
        {
            txtDetalheItem.Height = 35;
        }

        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        int altura = (alturaPrincipal - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 130;
        gvDados.Width = new Unit("100%");
        // txtDetalheItem.Height = altura - 370;
        alturaFrameAnexos = altura - 140;
        gvDados.JSProperties["cp_AlturaFrameAnexos"] = alturaFrameAnexos;
    }

    private void verificaVisibilidadeObjetos()
    {
        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "controlaClientesGestaoAgil");

        ddlClassificacao.JSProperties["cp_Visivel"] = "S";

        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["controlaClientesGestaoAgil"] + "" == "N")
        {
            ddlClassificacao.JSProperties["cp_Visivel"] = "N";
            tdInfoSecundarias.Style.Add(HtmlTextWriterStyle.Display, "none");
            tdTituloReceita.Style.Add(HtmlTextWriterStyle.Display, "none");
            tdReceita.Style.Add(HtmlTextWriterStyle.Display, "none");
            tdStatus1.Style.Add(HtmlTextWriterStyle.Display, "none");
            tdStatus2.Style.Add(HtmlTextWriterStyle.Display, "none");
            gvDados.Columns["DescricaoTipoClassificacaoItem"].Visible = false;
            gvDados.Columns["DescricaoTipoStatusItem"].Visible = false;

            string comandoSQL = "SELECT TOP 1 CodigoTipoClassificacaoItem FROM Agil_TipoClassificacaoItemBacklog";

            DataSet dsClassificacao = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(dsClassificacao) && cDados.DataTableOk(dsClassificacao.Tables[0]))
                ddlClassificacao.Value = dsClassificacao.Tables[0].Rows[0]["CodigoTipoClassificacaoItem"].ToString();

            comandoSQL = "SELECT CodigoTipoStatusItem FROM Agil_TipoStatusItemBacklog WHERE IniciaisTipoStatusItemControladoSistema = 'AG_IMPL'";

            DataSet dsStatus = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(dsStatus) && cDados.DataTableOk(dsStatus.Tables[0]))
                ddlStatus.Value = dsStatus.Tables[0].Rows[0]["CodigoTipoStatusItem"].ToString();
        }
    }

    private void populaDdlStatus()
    {
        DataSet ds = new DataSet();
        string comandoSQL = string.Format(@"SELECT [CodigoTipoStatusItem]
      ,[DescricaoTipoStatusItem]
      ,[IniciaisTipoStatusItemControladoSistema]
      ,[IndicaAtribuicaoManualItem]
  FROM {0}.{1}.[Agil_TipoStatusItemBacklog] where [IndicaAtribuicaoManualItem] = 'S'", cDados.getDbName(), cDados.getDbOwner());

        ds = cDados.getDataSet(comandoSQL);


        ddlStatus.DataSource = ds.Tables[0];
        ddlStatus.TextField = "DescricaoTipoStatusItem";
        ddlStatus.ValueField = "CodigoTipoStatusItem";

        ddlStatus.DataBind();
    }


    private void populaClassificacao()
    {
        DataSet ds = new DataSet();

        ds = cDados.getClassificacaoItensBackLog("");

        ddlClassificacao.DataSource = ds.Tables[0];
        ddlClassificacao.TextField = "DescricaoTipoClassificacaoItem";
        ddlClassificacao.ValueField = "CodigoTipoClassificacaoItem";

        ddlClassificacao.DataBind();
    }
    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        return codigoDado;
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_MSG"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = "Item incluído com sucesso!";
            }

        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = "Item alterado com sucesso!";
            }

        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = "Item excluído com sucesso!";
            }
        }

        pnCallback.JSProperties["cp_MSG"] = mensagemErro_Persistencia;
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusao do registro
    {
        string msgErro = "";

        string tituloItem = txtTituloItem.Text.Replace("'", "'+char(39)+'"); //memoAtividade.Text.Replace("'", "'+char(39)+'");
        string detalheItem = txtDetalheItem.Text.Replace("'", "'+char(39)+'");

        int complexidade = int.Parse((ddlComplexidade.Value == null) ? "-1" : ddlComplexidade.Value.ToString());

        int codigoStatus = int.Parse((ddlStatus.Value != null) ? ddlStatus.Value.ToString() : "-1");
        int classificacao = int.Parse((ddlClassificacao.Value != null) ? ddlClassificacao.Value.ToString() : "-1");
        int importancia = int.Parse(txtImportancia.Text == "" ? "0" : txtImportancia.Text);

        int codigoCliente = (ddlCliente.Value == null) ? -1 : int.Parse(ddlCliente.Value.ToString());
        int codigoTipoTarefaTimesheet = (ddlTipoAtividade.Value == null) ? -1 : int.Parse(ddlTipoAtividade.Value.ToString());

        decimal receitaPrevista = 0;
        bool retorno = decimal.TryParse((spnReceitaPrevista.Value == null) ? "0" : spnReceitaPrevista.Value.ToString(), out receitaPrevista);

        decimal esforcoPrevisto = 0;
        bool retorno1 = decimal.TryParse((txtEsforco.Value == null) ? "0" : txtEsforco.Value.ToString(), out esforcoPrevisto);


        DataSet dsRetorno = cDados.incluiItensBackLog(idProjeto, tituloItem, detalheItem, codigoStatus.ToString(), classificacao, codigoUsuarioResponsavel, importancia, complexidade, esforcoPrevisto, codigoCronogramaProjeto, codigoCliente, codigoTipoTarefaTimesheet, receitaPrevista);
        msgErro = dsRetorno.Tables[0].Rows[0][0].ToString();
        carregaGvDados();
        return msgErro;
    }


    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";

        string tituloItem = txtTituloItem.Text.Replace("'", "'+char(39)+'");
        string detalheItem = txtDetalheItem.Text.Replace("'", "'+char(39)+'");
        int complexidade = int.Parse((ddlComplexidade.Value == null) ? "-1" : ddlComplexidade.Value.ToString());
        int codigoStatus = int.Parse((ddlStatus.Value != null) ? ddlStatus.Value.ToString() : "-1");
        int classificacao = int.Parse((ddlClassificacao.Value != null) ? ddlClassificacao.Value.ToString() : "-1");
        int importancia = int.Parse(txtImportancia.Text == "" ? "0" : txtImportancia.Text);
        int codigoCliente = (ddlCliente.Value == null) ? -1 : int.Parse(ddlCliente.Value.ToString());
        int codigoTipoTarefaTimesheet = (ddlTipoAtividade.Value == null) ? -1 : int.Parse(ddlTipoAtividade.Value.ToString());


        decimal receitaPrevista = 0;
        bool retorno = decimal.TryParse((spnReceitaPrevista.Value == null) ? "0" : spnReceitaPrevista.Value.ToString(), out receitaPrevista);

        decimal esforcoPrevisto = 0;
        bool retorno1 = decimal.TryParse((txtEsforco.Value == null) ? "0" : txtEsforco.Value.ToString(), out esforcoPrevisto);


        DataSet dsRetorno = cDados.atualizaItensDoBackLog(int.Parse(chave), idProjeto, tituloItem, detalheItem, codigoStatus, classificacao, codigoUsuarioResponsavel, importancia, complexidade, esforcoPrevisto, codigoCronogramaProjeto, codigoCliente, codigoTipoTarefaTimesheet, receitaPrevista);

        msgErro = dsRetorno.Tables[0].Rows[0][0].ToString();

        carregaGvDados();
        //populaDdlTarefas();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return msgErro;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";
        bool retorno = cDados.excluiItensDoBackLog(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, int.Parse(chave), ref msgErro);
        return msgErro;
    }

    #endregion

    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.ErrorTextKind == GridErrorTextKind.General)
        {
            e.ErrorText = e.Exception.Message;
        }
        else if (e.ErrorTextKind == GridErrorTextKind.RowValidate)
        {
            e.ErrorText = "Erro de validação: " + e.ErrorText;
        }
    }
    protected void menu_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PR_IteBkl");
    }
    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "PR_IteBkl", "Itens do Backlog", this);
    }
    protected void pnDescricaoSprint_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigoitem = e.Parameter;

        string comandoSQL = string.Format(@"SELECT p.NomeProjeto, i.CodigoIteracao  
                                              FROM {0}.{1}.Agil_Iteracao AS i INNER JOIN
                                                   {0}.{1}.Projeto AS p ON (i.CodigoProjetoIteracao = p.CodigoProjeto) INNER JOIN
                                                   {0}.{1}.Agil_ItemBacklog As ai ON  (ai.CodigoIteracao = i.CodigoIteracao)
                                             WHERE ai.CodigoItem = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoitem);

        DataSet dsTextoItem = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(dsTextoItem) && cDados.DataTableOk(dsTextoItem.Tables[0]))
        {
            lblDescricaoSprint.Text = dsTextoItem.Tables[0].Rows[0]["NomeProjeto"].ToString();
        }
    }
}

