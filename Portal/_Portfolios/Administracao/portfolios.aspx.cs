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
using System.Collections.Specialized;

public partial class _Portfolios_Administracao_portfolios : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int codigoEntidade = 0;

    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";
    private string nomeTabelaDb = "Portfolio";
    private string whereUpdateDelete;

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "PO_Cad");
        }
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "PO_Cad"))
            podeIncluir = true;

        HeaderOnTela();
        carregaComboUsuarios();
        carregaComboStatus();
        carregaComboCarteiras();
        cDados.aplicaEstiloVisual(this);
        if (!Page.IsPostBack)
        {   
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            //carregaComboPortfolioSuperior("");
        }

        populaGrid();
        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void carregaComboCarteiras()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoCarteira, NomeCarteira FROM Carteira 
         WHERE CodigoEntidade = {0} 
           AND IniciaisCarteiraControladaSistema IS NULL
           AND IndicaCarteiraAtiva = 'S'", codigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlCarteira.TextField = "NomeCarteira";
        ddlCarteira.ValueField = "CodigoCarteira";
        ddlCarteira.DataSource = ds.Tables[0];
        ddlCarteira.DataBind();
        ddlCarteira.Items.Insert(0, new ListEditItem("", null));
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/portfolios.js""></script>"));
        this.TH(this.TS("barraNavegacao", "portfolios"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Portfólios</title>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = altura;
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 320;
        //gvDados.Width = new Unit((largura - 10) + "px");
    }

    /*private void MenuUsuarioLogado()
    {
        BarraNavegacao1.MostrarInclusao = false;
        BarraNavegacao1.MostrarEdicao = false;
        BarraNavegacao1.MostrarExclusao = false;

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "INCPTF"))
            BarraNavegacao1.MostrarInclusao = true;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "ALTPTF"))
            BarraNavegacao1.MostrarEdicao = true;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "EXCPTF"))
            BarraNavegacao1.MostrarExclusao = true;
    }*/

    #endregion

    #region GRID

    private void populaGrid()
    {
        DataSet ds = cDados.getPortfolios(codigoEntidade, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    #endregion

    #region COMBOBOX

    private void carregaComboUsuarios()
    {

        //        string where = string.Format(@" AND EXISTS (SELECT 1 FROM
        //                                                                UsuarioUnidadeNegocio AS uu 
        //                                                               WHERE uu.CodigoUsuario = u.CodigoUsuario
        //                                                                 AND uu.CodigoUnidadeNegocio = " + codigoEntidade + ")");
        //DataSet dsUsuarios = cDados.getUsuarios(where);
        //        if (cDados.DataSetOk(dsUsuarios))
        //        {
        //            ddlGerente.DataSource = dsUsuarios;
        //            ddlGerente.TextField = "NomeUsuario";
        //            ddlGerente.ValueField = "CodigoUsuario";
        //            ddlGerente.DataBind();
        //        }
        ddlGerente.TextField = "NomeUsuario";
        ddlGerente.ValueField = "CodigoUsuario";


        ddlGerente.Columns[0].FieldName = "NomeUsuario";
        ddlGerente.Columns[1].FieldName = "EMail";
        ddlGerente.TextFormatString = "{0}";

    }

    private void carregaComboStatus()
    {
        DataSet dsStatus = cDados.getStatusPortfolios(" and TipoStatus = 'POR'");

        if (cDados.DataSetOk(dsStatus))
        {
            ddlStatus.DataSource = dsStatus;
            ddlStatus.TextField = "DescricaoStatus";
            ddlStatus.ValueField = "CodigoStatus";
            ddlStatus.DataBind();
        }
    }

    private void carregaComboPortfolioSuperior(string where)
    {
        DataSet dsPortfolioSuperior = cDados.getPortfolios(codigoEntidade, where);

        if (cDados.DataSetOk(dsPortfolioSuperior))
        {
            ddlPortfolioSuperior.DataSource = dsPortfolioSuperior;
            ddlPortfolioSuperior.TextField = "DescricaoPortfolio";
            ddlPortfolioSuperior.ValueField = "CodigoPortfolio";
            ddlPortfolioSuperior.DataBind();
        }

        ListEditItem item = new ListEditItem(" ", "-1");
        ddlPortfolioSuperior.Items.Insert(0, item);
    }

    #endregion

    // Método responsável por obter os valores que estão preenchidos no formulário
    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DescricaoPortfolio", txtPortfolio.Text);
        if (ddlPortfolioSuperior.Value != null)
            oDadosFormulario.Add("CodigoPortfolioSuperior", (ddlPortfolioSuperior.Value.ToString() == "-1") ? "null" : ddlPortfolioSuperior.Value.ToString());
        oDadosFormulario.Add("CodigoUsuarioGerente", (ddlGerente.Value == null) ? "null" : ddlGerente.Value.ToString());
        oDadosFormulario.Add("CodigoStatusPortfolio", int.Parse(ddlStatus.Value.ToString()));
        oDadosFormulario.Add("CodigoEntidade", codigoEntidade);
        oDadosFormulario.Add("CodigoCarteiraAssociada", (ddlCarteira.Value == null) ? "null" : ddlCarteira.Value.ToString());

        return oDadosFormulario;
    }

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "")// não deu erro durante o processo de persistência
        {
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        }

        else // alguma coisa deu errado...
        {
            pnCallback.JSProperties["cp_OperacaoErro"] = e.Parameter;
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario();

            cDados.insert(nomeTabelaDb, oDadosFormulario, false);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario();

            oDadosFormulario.Remove("CodigoEntidade");
            cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        try
        {
            cDados.delete(nomeTabelaDb, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    #endregion
    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "PO_Cad"))
            podeEditar = true;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, "PO_Cad"))
            podeExcluir = true;

        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir)
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
    protected void ddlGerente_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidade);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }

    }
    protected void ddlGerente_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidade, e.Filter, "");


        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "MntPort");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "MntPort", lblTituloTela.Text, this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    }

    protected void callbackPortfolioSuperior_Callback(object sender, CallbackEventArgsBase e)
    {
        //CodigoPortfolio + '|' + TipoOperacao
        string[] parametrosRecebidos = e.Parameter.Split('|');

        string where = "";
        string codigoPortfolio = "";
        string tipoOperacao = "";

        if (parametrosRecebidos.Length > 1)
        {
            codigoPortfolio = parametrosRecebidos[0];
            tipoOperacao = parametrosRecebidos[1];
        }
        if (codigoPortfolio != "")
        {
            where = string.Format(@" AND (p.CodigoPortfolio != {0}) ", codigoPortfolio);
        }
        carregaComboPortfolioSuperior(where);

        string retornoHabilita = "";
        if (tipoOperacao == "Consultar")
        {
            retornoHabilita = "N";
        }
        else
        {
            retornoHabilita = "S";
        }


        ((ASPxCallbackPanel)(sender)).JSProperties["cp_habilita"] = retornoHabilita;
    }
}
