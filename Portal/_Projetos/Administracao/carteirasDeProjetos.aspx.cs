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
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web;
using System.Drawing;
using DevExpress.Data.Filtering;

public partial class _Projetos_Administracao_carteirasDeProjetos : System.Web.UI.Page
{
    dados cDados;

    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";
    public string definelegenda = "";
    public string definelegenda1 = "";

    //Variáveis para controle de permissões
    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;
    public bool podeConsultar = false;

    private static DataSet dsCarteirasGlobal;
    private static DataSet dsGvProjetos;
    private static DataSet dsGvProjetosChek;
    private static DataSet dsGvProjetosExcluirChek;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        podeIncluir = (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_IncCrtPrj"));
        podeEditar = (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_AltCrtPrj"));
        podeExcluir = (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_ExcCrtPrj"));

        podePermissao = (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_AdmCrtPrj"));
        podeConsultar = (podeIncluir || podeEditar || podeExcluir);

        if (!IsPostBack)
        {
            if (!podeConsultar)
            {
                cDados.RedirecionaParaTelaSemAcesso(this);
            }
            carregaGvDados();
        }
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        defineLabelCarteiraEVisibilidadeColunaAnoOrcamento();
        cDados.setaTamanhoMaximoMemo(memDescricaoCarteira, 500, lblContadorMemo);
        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        carregaGvDados();

        if (getChavePrimaria() != "")
        {
            carregaListaProjetos(getChavePrimaria());
            carregaListaProjetosDisponiveis(getChavePrimaria());
        }
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        cDados.aplicaEstiloVisual(Page);
        gvProjetos.Settings.VerticalScrollableHeight = 150;
        ((GridViewDataTextColumn)gvCheck.Columns["NomeProjeto"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        //DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);
    }

    #region VARIOS

    private void defineLabelCarteiraEVisibilidadeColunaAnoOrcamento()
    {
        DataSet dsParametro = cDados.getParametrosSistema("labelCarteiras", "labelCarteirasPlural", "usaAnoEmCarteiraProjeto");
        string label = "Carteira";
        string labelPlural = "Carteiras";
        bool usaColunaAno = false;
        if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
        {
            label = dsParametro.Tables[0].Rows[0]["labelCarteiras"].ToString();
            labelPlural = dsParametro.Tables[0].Rows[0]["labelCarteirasPlural"].ToString();
            usaColunaAno = dsParametro.Tables[0].Rows[0]["usaAnoEmCarteiraProjeto"].ToString().Trim().ToUpper() == "S";
        }

        lblTituloTela.JSProperties["cp_LabelCarteira"] = label;

        lblTituloTela.Text = labelPlural;
        gvDados.Columns["NomeCarteira"].Caption = label;
        lblCarteira.Text = label + ":";

        bool indicaIdiomaPortugues = System.Globalization.CultureInfo.CurrentCulture.Name.StartsWith("pt", StringComparison.InvariantCultureIgnoreCase);
        if (indicaIdiomaPortugues == true)
        {
            definelegenda = labelPlural + " " + Resources.traducao.carteirasDeProjetos_inativas;
            checkAtivo.Text = label + " " + Resources.traducao.carteirasDeProjetos_ativa + "?";
        }
        else
        {
            definelegenda = Resources.traducao.carteirasDeProjetos_inativas + " " + labelPlural;
            checkAtivo.Text = Resources.traducao.carteirasDeProjetos_ativa + " " + label + "?";
        }
        
        ((GridViewDataTextColumn)gvProjetos.Columns["AnoOrcamentoProjeto"]).Visible = usaColunaAno;
        ((GridViewDataTextColumn)gvCheck.Columns["AnoOrcamentoProjeto"]).Visible = usaColunaAno;

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        alturaPrincipal = altura;
        gvDados.Settings.VerticalScrollableHeight = altura - 360;
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/carteirasDeProjetos.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ASPxListbox", "carteirasDeProjetos"));
    }

    #endregion

    #region GRID

    private void carregaGvDados()
    {
        string where = string.Format(
            @" AND isnull(cp.IniciaisCarteiraControladaSistema, '')  <> 'PR' 
               AND (cp.IniciaisCarteiraControladaSistema is null or cp.IndicaCarteiraAtiva = 'S')
               AND cp.CodigoEntidade = '{0}'", codigoEntidadeUsuarioResponsavel);
        dsCarteirasGlobal = cDados.getCarteirasDeProjetos(where);

        if (cDados.DataSetOk(dsCarteirasGlobal))
        {
            gvDados.DataSource = dsCarteirasGlobal;
            gvDados.DataBind();
        }

        gvDados.PageIndex = 0;
        gvCheck.PageIndex = 0;
        gvExcluirCheck.PageIndex = 0;
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        gvDados.DataSource = dsCarteirasGlobal;
        gvDados.DataBind();
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



    // retorna a primary key da tabela.
    private string getChavePrimariaProjeto()
    {
        if (gvProjetos.FocusedRowIndex >= 0)
            return gvProjetos.GetRowValues(gvProjetos.FocusedRowIndex, gvProjetos.KeyFieldName).ToString();
        else
            return "";
    }

    // retorna a primary key da tabela.
    private string getChavePrimariaChek()
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
        if (e.Parameter == "ExcluirProjeto")
        {
            mensagemErro_Persistencia = PersisteExclusaoRegistroProjeto();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
        {
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        //string chave = getChavePrimaria();
        string mesgError = "";
        string nomeCarteira = txtNomeCarteiraADD.Text.Replace("'", "''");
        string descricaoCarteira = memDescricaoCarteiraADD.Text.Replace("'", "''");
        string identityCodigoCarteira = "";
        string indicaCarteiraAtiva = (bool)checkAtivoADD.Checked ? "S" : "N";

        string[] arrayProjetosSelecionados = new string[gvProjetos.GetSelectedFieldValues("CodigoProjeto").Count];

        for (int i = 0; i < arrayProjetosSelecionados.Length; i++)
        {
            arrayProjetosSelecionados[i] = gvProjetos.GetSelectedFieldValues("CodigoProjeto")[i].ToString();
        }

        try
        {
            bool result = cDados.incluiCarteirasDoProjeto(nomeCarteira, descricaoCarteira, codigoEntidadeUsuarioResponsavel.ToString(), indicaCarteiraAtiva, ref identityCodigoCarteira, ref mesgError);

            if (result == false)
            {
                return mesgError;
            }
            else
            {
                //cDados.incluiProjetoSelecionadosCarteira(arrayProjetosSelecionados, identityCodigoCarteira);
                carregaGvDados();
                //gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
                gvDados.ClientVisible = false;
                return "";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string nomeCarteira = txtNomeCarteiraADD.Text.Replace("'", "''");
        string descricaoCarteira = memDescricaoCarteiraADD.Text.Replace("'", "''");
        string codigoCarteira = hfGeral.Get("CodigoCarteira").ToString();
        string indicaCarteiraAtiva = (bool)checkAtivoADD.Checked ? "S" : "N";

        string[] arrayProjetosSelecionados = new string[gvProjetos.GetSelectedFieldValues("CodigoProjeto").Count];

        for (int i = 0; i < arrayProjetosSelecionados.Length; i++)
        {
            arrayProjetosSelecionados[i] = gvProjetos.GetSelectedFieldValues("CodigoProjeto")[i].ToString();
        }

        cDados.atualizaCarteiraDoProjeto(nomeCarteira, descricaoCarteira, indicaCarteiraAtiva, codigoCarteira);
        //cDados.incluiProjetoSelecionadosCarteira(arrayProjetosSelecionados, codigoCarteira);
        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return "";
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgRetorno = "";

        cDados.excluiCarteira(chave);
        carregaGvDados();

        return msgRetorno;
    }


    private string PersisteExclusaoRegistroProjeto() // Método responsável pela Exclusão do registro
    {
        string chave = getChavePrimaria();
        string chaveProjeto = getChavePrimariaProjeto();
        string msgRetorno = "";
        cDados.excluiProjetoCarteira(chave, chaveProjeto);
        carregaListaProjetos(getChavePrimaria());
        return msgRetorno;
    }


    #endregion

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        bool indicaCarteiraControladaSistema = (gvDados.GetRowValues(e.VisibleIndex, "IniciaisCarteiraControladaSistema") != null) ? gvDados.GetRowValues(e.VisibleIndex, "IniciaisCarteiraControladaSistema").ToString() != "" : false;

        if (e.ButtonID == "btnEditarCustom")
        {
            if (podeEditar && !indicaCarteiraControladaSistema)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "AdicionarProjeto")
        {
            if (podeEditar && !indicaCarteiraControladaSistema)
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
            if (podeExcluir && !indicaCarteiraControladaSistema)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        else if (e.ButtonID.Equals("btnPermissoesCustom"))
        {
            if (podePermissao)
                e.Enabled = true;
            else
            {
                e.Text = "Permissões";
                e.Enabled = false;
                e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
            }
        }
        else if (e.ButtonID.Equals("btnDetalheCustom"))
        {
            if (podeConsultar && !indicaCarteiraControladaSistema)
                e.Enabled = true;
            else
            {
                e.Text = "Detalhe";
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
            }
        }

    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "DescricaoCarteira")
        {
            if (e.CellValue.ToString().Length > 100)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 100) + "...";
            }
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {

            string indicaCarteiraAtiva = e.GetValue("IndicaCarteiraAtiva").ToString();
            string indicaControladaSistema = e.GetValue("ControladaSistema").ToString();

            if (indicaControladaSistema == "Sim")
            {
                e.Row.ForeColor = Color.FromName("#619340");
            }
            if (indicaCarteiraAtiva == "N")
            {
                e.Row.ForeColor = Color.FromName("#914800");
            }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CatPrj");

        pcDadosNovaCarteira.HeaderText = "Inserir Nova Carteira";

    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "novaCarteira();", true, true, false, "CatPrj", "Categorias", this);
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

    protected void gvProjetos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != "")
        {
            gvProjetos.ExpandAll();
            selecionaProjetos();
        }
    }

    private void selecionaProjetos()
    {
        gvProjetos.Selection.UnselectAll();

        for (int i = 0; i < gvProjetos.VisibleRowCount; i++)
        {
            if (gvProjetos.GetRowValues(i, "Selecionado").ToString() == "S")
                gvProjetos.Selection.SelectRow(i);
        }
    }

    private void carregaListaProjetos(string codigoCarteira)
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoCarteiraSelecionada as int
            DECLARE @CodigoEntidade as int

            SET @CodigocarteiraSelecionada = {0}
            SET @CodigoEntidade = {1}
            SELECT * FROM [dbo].[f_GetProjetosAssociadosCarteira] (@CodigoEntidade, @CodigocarteiraSelecionada)
        END", codigoCarteira, codigoEntidadeUsuarioResponsavel);
        dsGvProjetos = cDados.getDataSet(comandoSQL);

        gvProjetos.DataSource = dsGvProjetos;
        gvProjetos.DataBind();

    }


    private void carregaListaProjetosExcluir(string codigoCarteira)
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoCarteiraSelecionada as int
            DECLARE @CodigoEntidade as int

            SET @CodigocarteiraSelecionada = {0}
            SET @CodigoEntidade = {1}


SELECT p.CodigoProjeto,  (select DescricaoStatus from Status where CodigoStatus = p.CodigoStatusProjeto) as  CodigoStatusProjeto,
                ent.SiglaUnidadeNegocio + ' - ' + CASE WHEN p.IndicaPrograma = 'N' THEN 'Projeto: ' ELSE 'Programa: ' END + p.NomeProjeto  as NomeProjeto,
 'S' as Selecionado, 0 AS ColunaAgrupamento
                FROM Projeto p 
                  inner join UnidadeNegocio as ent on ent.CodigoUnidadeNegocio = p.CodigoEntidade 
                  inner join Status as sta on sta.CodigoStatus = p.CodigoStatusProjeto
                WHERE p.DataExclusao IS NULL
                   AND p.CodigoProjeto IN (SELECT lp.CodigoProjeto
                                  FROM CarteiraProjeto AS lp
                                  WHERE lp.CodigoCarteira = @CodigoCarteiraSelecionada)
end", codigoCarteira, codigoEntidadeUsuarioResponsavel);
        dsGvProjetosExcluirChek = cDados.getDataSet(comandoSQL);

        gvExcluirCheck.DataSource = dsGvProjetosExcluirChek;
        gvExcluirCheck.DataBind();

    }

    private void carregaListaProjetosDisponiveis(string codigoCarteira)
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @CodigoCarteiraSelecionada as int
            DECLARE @CodigoEntidade as int

            SET @CodigocarteiraSelecionada = {0}
            SET @CodigoEntidade = {1}
SELECT * FROM [dbo].[f_GetProjetosAssociarCarteira] (
   @CodigoEntidade
  ,@CodigocarteiraSelecionada)

end", codigoCarteira, codigoEntidadeUsuarioResponsavel);
        dsGvProjetosChek = cDados.getDataSet(comandoSQL);

        gvCheck.DataSource = dsGvProjetosChek;
        gvCheck.DataBind();
    }

    protected void menu_AdiconarProjeto_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "novaCarteira();", true, false, false, "CatPrj", "Categorias", this);
    }

    protected void menu_AdiconarProjeto_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CatPrj");
    }

    protected void menuProjeto_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CatPrj");
    }

    protected void menuProjeto_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "InserirProjetos();", true, false, false, "CatPrj", "Categorias", this);
    }

    protected void callbackSalvaselecao_Callback(object source, CallbackEventArgs e)
    {
        string cx = e.Parameter;
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        string chave = getChavePrimaria();
        string chaveProjeto = e.Parameter;
        try
        {
            cDados.excluiProjetoCarteira(chave, chaveProjeto);
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "Projeto excluido com sucesso.";
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }

    protected void gvProjetos_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaListaProjetos(e.Parameters);
    }

    protected void callbackCheck_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";

        carregaListaProjetos(getChavePrimaria());
        carregaListaProjetosDisponiveis(getChavePrimaria());

        //gvCheck.Refresh(); pcCheck.Hide();
        string chave = getChavePrimariaChek();

        //Prenche o objeto de Projetos selecionados e se já contém no banco e é igual ao projeto e Carteira preserva o parametro IndicaCarteiraPrincipal.

        string codigoCarteira = hfGeral.Get("CodigoCarteira").ToString();
        string[] arrayProjetosSelecionados = new string[gvCheck.GetSelectedFieldValues("CodigoProjeto").Count];
        string[] arrayIndicaCarteiraPrincipal = new string[gvCheck.GetSelectedFieldValues("CodigoProjeto").Count];

        for (int i = 0; i < arrayProjetosSelecionados.Length; i++)
        {
            //Mantém o Valor de IndicaCarteiraPrincipal para inserção do objeto
            arrayIndicaCarteiraPrincipal[i] = cDados.SelectProjetoSelecionadosCarteira(codigoCarteira, gvCheck.GetSelectedFieldValues("CodigoProjeto")[i].ToString());

            arrayProjetosSelecionados[i] = gvCheck.GetSelectedFieldValues("CodigoProjeto")[i].ToString();
        }
        try
        {
            cDados.UpdateProjetoSelecionadosCarteira(arrayProjetosSelecionados, codigoCarteira, arrayIndicaCarteiraPrincipal);
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "Projeto(s) inserido(s) na carteira com sucesso.";
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "erro:" + ex.Message;
        }

        //pcCheck.CloseAction = 

    }

    protected void callbackExcluirCheck_Callback1(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";

        //Prenche o objeto de Projetos selecionados e se já contém no banco e é igual ao projeto e Carteira preserva o parametro IndicaCarteiraPrincipal.
        string codigoCarteira = hfGeral.Get("CodigoCarteira").ToString();
        string[] arrayProjetosSelecionados = new string[gvExcluirCheck.GetSelectedFieldValues("CodigoProjeto").Count];

        for (int i = 0; i < arrayProjetosSelecionados.Length; i++)
        {
            arrayProjetosSelecionados[i] = gvExcluirCheck.GetSelectedFieldValues("CodigoProjeto")[i].ToString();
        }
        try
        {
            cDados.excluirProjetoSelecionadosCarteira(arrayProjetosSelecionados, codigoCarteira);
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "Projeto(s) excluidos(s) da carteira com sucesso.";
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "erro:" + ex.Message;
        }
    }

    protected void callbackExcluirCheck_Load(object sender, EventArgs e)
    {
        carregaListaProjetosExcluir(getChavePrimaria());
    }

    protected void menuExcluir_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "ExcluirProjetos();", true, false, false, "CatPrj", "Carteiras", this);

    }

    protected void callbackExcluirFechar_Callback1(object source, CallbackEventArgs e)
    {

        ASPxLoadingExcluirComboboxProjeto.Visible = true;

        gvExcluirCheck.Selection.UnselectAll();
        gvExcluirCheck.FilterExpression = "";

        ASPxLoadingExcluirComboboxProjeto.Visible = false;
    }

    protected void callbackrFechar_Callback2(object source, CallbackEventArgs e)
    {
        ASPxLoadingExcluirComboboxProjeto.Visible = true;
        gvCheck.Selection.UnselectAll();
        gvCheck.FilterExpression = "";

        ASPxLoadingExcluirComboboxProjeto.Visible = false;
    }

    protected void gvCheck_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "limpar")
        {
            gvCheck.Selection.UnselectAll();
            gvCheck.FilterExpression = "";
        }
    }

    protected void gvExcluirCheck_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "limpar")
        {
            gvExcluirCheck.Selection.UnselectAll();
            gvExcluirCheck.FilterExpression = "";
        }
    }
}
