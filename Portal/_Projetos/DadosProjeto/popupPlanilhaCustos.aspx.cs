using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_popupPlanilhaCustos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoItemOrcamento = -1;
    private int codigoProjeto = -1;
    private int codigoWorkflow = -1;
    private int codigoInstanciaWorkflow = -1;
    private int codigoLinhaBase = -1;
    private int anoCorrente = -1;

    private int codigoItemRecurso = -1;
    private int quantidadeOrcada = -1;
    private decimal valorUnitario = -1;
    private decimal valorTotal = -1;
    private decimal valorRequeridoAnoCorrente = -1;
    private decimal valorRequeridoAnoSeguinte = -1;
    private int codigoFonteRecursosFinanceiros = -1;
    private string dotacaoOrcamentaria = "";
    private string indicaContratarItem = "";
    private string unidadeMedida = "";
    private string detalheItemRecurso = "";
    private decimal valorRequeridoPosAnoSeguinte = -1;
    private string descricaoItemOrcamentoProjeto = "";
    private string tipoOperacao = "";
    private int alturaDoPopup = -1;

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

        this.Title = cDados.getNomeSistema();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        IFormatProvider iFormatProvider = new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false);

        int.TryParse(Request.QueryString["CIO"] + "", out codigoItemOrcamento);
        int.TryParse(Request.QueryString["CP"] + "", out codigoProjeto);
        int.TryParse(Request.QueryString["CWF"] + "", out codigoWorkflow);
        int.TryParse(Request.QueryString["CI"] + "", out codigoInstanciaWorkflow);
        int.TryParse(Request.QueryString["CLB"] + "", out codigoLinhaBase);
        int.TryParse(Request.QueryString["AC"] + "", out anoCorrente);
        int.TryParse(Request.QueryString["CIR"] + "", out codigoItemRecurso);
        int.TryParse(Request.QueryString["QO"] + "", out quantidadeOrcada);
        int.TryParse(Request.QueryString["ALT"] + "", out alturaDoPopup);
        decimal.TryParse((Request.QueryString["VU"] + "").Replace(".", ","), out valorUnitario);
        decimal.TryParse((Request.QueryString["VT"] + "").Replace(".", ","), out valorTotal);
        decimal.TryParse((Request.QueryString["VRAC"] + "").Replace(".", ","), out valorRequeridoAnoCorrente);
        decimal.TryParse((Request.QueryString["VRAS"] + "").Replace(".", ","), out valorRequeridoAnoSeguinte);
        decimal.TryParse((Request.QueryString["VRPAS"] + "").Replace(".", ","), out valorRequeridoPosAnoSeguinte);
        int.TryParse(Request.QueryString["CFRF"] + "", out codigoFonteRecursosFinanceiros);
        dotacaoOrcamentaria = Request.QueryString["DO"] + "";
        indicaContratarItem = Request.QueryString["ICI"] + "";
        unidadeMedida = Request.QueryString["UM"] + "";
        detalheItemRecurso = Request.QueryString["DIR"] + "";

        
        descricaoItemOrcamentoProjeto = Request.QueryString["DIOP"] + "";
        int.TryParse(Request.QueryString["CIO"] + "", out codigoItemOrcamento);
        tipoOperacao = Request.QueryString["TO"] + "";
        cDados.aplicaEstiloVisual(this.Page);
        defineAlturaTela();

        carregaComboFontesRecursos();
        carregaComboItems();
        if (!IsPostBack)
        {
            carregaCampos();
        }
        
    }

    private void defineAlturaTela()
    {
        int altura = 0;
        int largura = 0;
        string resolucao = cDados.getInfoSistema("ResolucaoCliente").ToString();
        bool retorno = cDados.getLarguraAlturaTela(resolucao, out largura, out altura);

        txtComentario.Height = new Unit((alturaDoPopup - 290) + "px");
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/PlanilhaCustos.js""></script>"));
        this.TH(this.TS("barraNavegacao", "PlanilhaCustos", "_Strings"));

    }

    private void carregaCampos()
    {
        txtComentario.Text = descricaoItemOrcamentoProjeto;
        ckContratar.Checked = (indicaContratarItem == "S");
        txtDotacao.Value = dotacaoOrcamentaria;
        txtValorUnitario.Value = valorUnitario;
        txtValorTotal.Value = valorTotal;
        txtQuantidade.Value = quantidadeOrcada;
        ddlFonte.Value = codigoFonteRecursosFinanceiros.ToString();
        ddlItem.Value = codigoItemOrcamento.ToString();

        lblRequeridoAnoCorrente.Text = string.Format("Requerido {0} (R$):", anoCorrente);
        lblRequeridoAnoSeguinte.Text = string.Format("Requerido {0} (R$):", anoCorrente + 1);
        lblRequeridoAnoSeguinte2.Text = string.Format("Requerido {0} (R$):", anoCorrente + 2);

        txtValorRequeridoAnoCorrente.Text = valorRequeridoAnoCorrente.ToString();
        txtValorRequeridoAnoSeguinte.Text = valorRequeridoAnoSeguinte.ToString();
        txtValorRequeridoAnoSeguinte2.Text = valorRequeridoPosAnoSeguinte.ToString();


        string comandoSQL = string.Format(@"
        SELECT [CodigoItemOrcamento]
              ,[CodigoGrupoRecurso]
          FROM [dbo].[pbh_ItemOrcamentoProjeto]
        WHERE [CodigoItemOrcamento] = {0}", codigoItemOrcamento);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlItem.Value = ds.Tables[0].Rows[0]["CodigoGrupoRecurso"].ToString();
        }
        else
        {
            ddlItem.Text = "";
        }

        txtComentario.ReadOnly = tipoOperacao == "Consultar";
        ckContratar.ReadOnly = tipoOperacao == "Consultar";
        txtDotacao.ReadOnly = tipoOperacao == "Consultar";
        txtValorUnitario.ReadOnly = tipoOperacao == "Consultar";
        //txtValorTotal.ReadOnly = readOnly == "Consultar";
        txtQuantidade.ReadOnly = tipoOperacao == "Consultar";
        ddlFonte.ReadOnly = tipoOperacao == "Consultar";
        ddlItem.ReadOnly = tipoOperacao == "Consultar";
        txtValorRequeridoAnoCorrente.ReadOnly = tipoOperacao == "Consultar";
        txtValorRequeridoAnoSeguinte.ReadOnly = tipoOperacao == "Consultar";
        txtValorRequeridoAnoSeguinte2.ReadOnly = tipoOperacao == "Consultar";
        btnSalvar.ClientVisible = !(tipoOperacao == "Consultar");
    }

    private void carregaComboItems()
    {
        DataSet ds = cDados.getGruposRecursosPlanilhaCustosProjeto(codigoEntidadeUsuarioResponsavel, "");

        ddlItem.DataSource = ds;
        ddlItem.TextField = "NomeItem";
        ddlItem.ValueField = "CodigoItem";
        ddlItem.DataBind();
    }

    private void carregaComboDotacaoOrcamentaria()
    {
        DataSet ds = cDados.getDotacoesOrcamentarias(codigoEntidadeUsuarioResponsavel, "");

        txtDotacao.DataSource = ds;
        txtDotacao.TextField = "Dotacao";
        txtDotacao.ValueField = "Dotacao";
        txtDotacao.DataBind();
    }

    private void carregaComboFontesRecursos()
    {
        DataSet ds = cDados.getFontesRecursosFinanceiros(codigoEntidadeUsuarioResponsavel, "");

        ddlFonte.DataSource = ds;
        ddlFonte.TextField = "NomeFonte";
        ddlFonte.ValueField = "CodigoFonteRecursosFinanceiros";
        ddlFonte.DataBind();
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        bool result = false;
        string erro = "";
        int codigoItem = ddlItem.SelectedIndex == -1 ? -1 : int.Parse(ddlItem.Value.ToString());
        string quantidade = txtQuantidade.Text == "" ? "NULL" : txtQuantidade.Text.Replace(".", "").Replace(",", ".");
        string valorUnitario = txtValorUnitario.Text == "" ? "NULL" : txtValorUnitario.Text.Replace(".", "").Replace(",", ".");
        string valorTotal = txtValorTotal.Text == "" ? "NULL" : txtValorTotal.Text.Replace(".", "").Replace(",", ".");
        string codigoFonteRecursos = ddlFonte.SelectedIndex == -1 ? "NULL" : ddlFonte.Value.ToString();
        string dotacaoOrcamentaria = txtDotacao.Value == null ? "NULL" : "'" + txtDotacao.Value.ToString() + "'";
        string indicaContratarItem = (ckContratar.Checked) ? "S" : "N";
        string valorAnoCorrente = txtValorRequeridoAnoCorrente.Text == "" ? "NULL" : txtValorRequeridoAnoCorrente.Text.Replace(".", "").Replace(",", ".");
        string valorAnoSeguinte = txtValorRequeridoAnoSeguinte.Text == "" ? "NULL" : txtValorRequeridoAnoSeguinte.Text.Replace(".", "").Replace(",", ".");
        string valorAnoSeguinte2 = txtValorRequeridoAnoSeguinte2.Text == "" ? "NULL" : txtValorRequeridoAnoSeguinte2.Text.Replace(".", "").Replace(",", ".");
        string comentario = txtComentario.Text;
        try
        {
            result = cDados.atualizaItemPlanilhaCustosProjeto(codigoItemOrcamento, codigoItem, quantidade, valorUnitario, valorTotal
            , codigoFonteRecursos, dotacaoOrcamentaria, indicaContratarItem, codigoUsuarioResponsavel, valorAnoCorrente, valorAnoSeguinte
            , anoCorrente, valorAnoSeguinte2, comentario);
            if (result == false)
                erro = "Erro ao salvar o registro!";
            else
            {
                erro = "";
            }
        }
        catch(Exception ex)
        {
            erro = ex.Message;
        }
        return erro;
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        bool result = false;
        string erro = "";
        int codigoItem = ddlItem.SelectedIndex == -1 ? -1 : int.Parse(ddlItem.Value.ToString());
        string quantidade = txtQuantidade.Text == "" ? "NULL" : txtQuantidade.Text.Replace(".", "").Replace(",", ".");
        string valorUnitario = txtValorUnitario.Text == "" ? "NULL" : txtValorUnitario.Text.Replace(".", "").Replace(",", ".");
        string valorTotal = txtValorTotal.Text == "" ? "NULL" : txtValorTotal.Text.Replace(".", "").Replace(",", ".");
        string codigoFonteRecursos = ddlFonte.SelectedIndex == -1 ? "NULL" : ddlFonte.Value.ToString();
        string dotacaoOrcamentaria = txtDotacao.Value == null ? "NULL" : "'" + txtDotacao.Value.ToString() + "'";
        string indicaContratarItem = (ckContratar.Checked) ? "S" : "N";
        string valorAnoCorrente = txtValorRequeridoAnoCorrente.Text == "" ? "NULL" : txtValorRequeridoAnoCorrente.Text.Replace(".", "").Replace(",", ".");
        string valorAnoSeguinte = txtValorRequeridoAnoSeguinte.Text == "" ? "NULL" : txtValorRequeridoAnoSeguinte.Text.Replace(".", "").Replace(",", ".");
        string valorAnoSeguinte2 = txtValorRequeridoAnoSeguinte2.Text == "" ? "NULL" : txtValorRequeridoAnoSeguinte2.Text.Replace(".", "").Replace(",", ".");
        string comentario = txtComentario.Text;

        if (codigoProjeto == -1)
        {
            erro = "Erro ao salvar o registro, o codigo do projeto não foi carregado!";
        }
        try
        {
            result = cDados.incluiItemPlanilhaCustosProjeto(codigoProjeto, codigoItem, quantidade, valorUnitario, valorTotal
            , codigoFonteRecursos, dotacaoOrcamentaria, indicaContratarItem, codigoUsuarioResponsavel, valorAnoCorrente
            , valorAnoSeguinte, anoCorrente, valorAnoSeguinte2, comentario);
            if (result == false)
            {
                erro = "Erro ao salvar o registro!";
            }
            else
            {
                erro = "";
            }
        }
        catch(Exception ex)
        {
            erro = ex.Message;
        }
        return erro;
    }

    protected void txtDotacao_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;

            DataSet ds = cDados.getDotacoesOrcamentarias(codigoEntidadeUsuarioResponsavel, " AND Dotacao = '" + e.Value + "'");

            txtDotacao.DataSource = ds;
            txtDotacao.TextField = "Dotacao";
            txtDotacao.ValueField = "Dotacao";
            txtDotacao.DataBind();
        }
        else
        {
            DataSet ds = cDados.getDotacoesOrcamentarias(codigoEntidadeUsuarioResponsavel, " AND Dotacao = '-1'");

            txtDotacao.DataSource = ds;
            txtDotacao.TextField = "Dotacao";
            txtDotacao.ValueField = "Dotacao";
            txtDotacao.DataBind();
        }
    }

    protected void txtDotacao_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        string comandoSQL = string.Format(@"SELECT ROW_NUMBER()over(order by Dotacao) as rn ,Dotacao
             FROM {0}.{1}.pbh_DotacaoOrcamentaria
             WHERE DataExclusao is null and CodigoEntidade = {2} 
               AND Dotacao LIKE '%{3}%'", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, e.Filter);

        DataSet ds = cDados.getDataSet(
               string.Format(@"SELECT *
                                 FROM ({0}) as u
                                WHERE u.rn Between {1} and {2}", comandoSQL, e.BeginIndex + 1, e.EndIndex + 1));

        txtDotacao.DataSource = ds;
        txtDotacao.TextField = "Dotacao";
        txtDotacao.ValueField = "Dotacao";
        txtDotacao.DataBind();
    }
    
    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        if (codigoItemOrcamento == -1)
        {
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "Ítem de orçamento incluído com sucesso!";
            ((ASPxCallback)source).JSProperties["cpErro"] = persisteInclusaoRegistro();
        }
        else
        {
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "Ítem de orçamento alterado com sucesso!";
            ((ASPxCallback)source).JSProperties["cpErro"] = persisteEdicaoRegistro();
        }
    }

    protected void pnCallbackValorUnitario_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigo = e.Parameter;

        string comando = string.Format(@"
        SELECT[CodigoGrupoRecurso]
             ,[DescricaoGrupo]
            , [CustoHora] CustoUnitario
        FROM[dbo].[GrupoRecurso]
       WHERE CodigoGrupoRecurso = {0}", codigo);
        DataSet ds = cDados.getDataSet(comando);

        txtValorUnitario.Value = ds.Tables[0].Rows[0]["CustoUnitario"].ToString();
    }
}