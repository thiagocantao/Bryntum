/*
 ALTERAÇÕES:
 * 
 * 30/11/2010 : BY Alejandro
    Alterar opção onde registra-se a análise de indicadores de projetos para contemplar 
    o novo modelo que irá gravar as análises em uma tabela denominada AnalisePerformance
    ao invés de AnalisePerformanceProjeto.
 
 * 23/03/2011 :: Alejandro : Alteração de acesso a tela, e dos codigos de permissão.
 * * */
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

public partial class _Estrategias_objetivoEstrategico_ObjetivoEstrategico_Analises : System.Web.UI.Page
{
    //variaveis de acceso ao banco de dados.
    dados cDados;

    //variaveis de logeo.
    private int codigoUnidadeLogada = -1, codigoUnidadeSelecionada = 0, codigoUnidadeMapa = 0;
    private int idUsuarioLogado = 0;

    //variaveis de configuração
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

    //variaveis de permissão.
    public bool podeEditar = false;     //ALTETGOBJ
    public bool podeIncluir = false;    //INCETGOBJ
    public bool podeExcluir = false;    //EXCETGOBJ
    public bool podeConsultar = false;  //CONSETGOBJ

    //variaveis gerais.
    private int codigoObjetivoEstrategico = 0;
    private int codigoTipoAssociacao;
    bool permissaoMapa = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoUnidadeSelecionada, "INCETGOBJ"))
            podeIncluir = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();
        cDados.aplicaEstiloVisual(this);
        carregaCodigoTipoAssociacao(ref codigoTipoAssociacao);
        getPermissoesTela();
        carregaGrid();
        

        if (!IsPostBack)
        {
            //Verificação de acesso a tela atual.
            bool bPodeAcessarTela = false;
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, codigoUnidadeLogada, "OB", "OB_CnsAnl");
            if (!bPodeAcessarTela)
                RedirecionaParaTelaSemAcesso(this);

            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            carregaCampos();
        }

        cDados.setaTamanhoMaximoMemo(txtAnalise, 2000, lblContadorCaracterAnalise);
        cDados.setaTamanhoMaximoMemo(txtRecomendacoes, 2000, lblContadorCaracterRecomendacao);

    }

    public void RedirecionaParaTelaSemAcesso(Page page)
    {
        try
        {

            page.Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
        }
        catch
        {
            page.Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcessoNoMaster.aspx";
            page.Response.End();
        }
    }

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ObjetivoEstrategico_Analises.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ObjetivoEstrategico_Analises", "_Strings"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int alturaCalculada = alturaPrincipal - 280;
        
        grid.Settings.VerticalScrollableHeight = alturaCalculada - 115;
    }

    private void carregaCampos()
    {
        DataTable dt = cDados.getObjetivoEstrategico(null, codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            //txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            //txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtResponsavel.Text = dt.Rows[0]["NomeUsuario"].ToString();
            //qqtxtTema.Text = dt.Rows[0]["Tema"].ToString();
        }
        else
        {
            //txtPerspectiva.Text = "";
            txtObjetivoEstrategico.Text = "";
            //txtMapa.Text = "";
            txtResponsavel.Text = "";
            //txtTema.Text = "";
        }
    }

    private void carregaCodigoTipoAssociacao(ref int codigoTipoAssociacao)
    {
        DataTable dt = cDados.getTipoAssociacaoEventos("OB", "").Tables[0];

        if (dt.Rows.Count > 0)
            codigoTipoAssociacao = int.Parse(dt.Rows[0]["CodigoTipoAssociacao"].ToString());
    }

    private void getPermissoesTela()
    {
        int codigoEntidadeUnidadeMapa = cDados.getEntidadUnidadeNegocio(codigoUnidadeMapa);
        int codigoEntidadeUnidadeSelecionada = cDados.getEntidadUnidadeNegocio(codigoUnidadeSelecionada);

        //permissaoMapa = true; //  cDados.verificaPermissaoOE(codigoObjetivoEstrategico, codigoUnidade);
        if (codigoUnidadeLogada == codigoEntidadeUnidadeMapa && codigoUnidadeMapa == codigoUnidadeSelecionada)
            permissaoMapa = cDados.verificaPermissaoOE(codigoObjetivoEstrategico, codigoUnidadeSelecionada);
        else
            permissaoMapa = false;

        podeIncluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidadeUnidadeSelecionada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_IncAnl"));
        podeEditar = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidadeUnidadeSelecionada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_AltAnl"));
        podeExcluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidadeUnidadeSelecionada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_ExcAnl"));

        podeEditar = permissaoMapa == false ? permissaoMapa : podeEditar;
        podeExcluir = permissaoMapa == false ? permissaoMapa : podeExcluir;
        podeIncluir = permissaoMapa == false ? permissaoMapa : podeIncluir;
    }

    #endregion

    #region GRIDVIEW

    private void carregaGrid()
    {
        DataTable dt = cDados.getObjetivoEstrategicoAnalises(codigoObjetivoEstrategico.ToString(), codigoUnidadeSelecionada.ToString(), "OB").Tables[0];

        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void grid_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        carregaGrid();
    }

    protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete || e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
        {
            //if (idUsuarioLogado != int.Parse(grid.GetRowValues(e.VisibleIndex, "CodigoUsuarioInclusao").ToString()) || permissaoMapa == false)
            //{
            //    e.Enabled = false;
            //    if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
            //        e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
            //    if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
            //        e.Image.Url = "~/imagens/botoes/editarRegDes.PNG";
            //}
            if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
            {
                if (podeExcluir)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Text = "Excluir";
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.PNG";
                }
            }
            else if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
            {
                if (idUsuarioLogado == int.Parse(grid.GetRowValues(e.VisibleIndex, "CodigoUsuarioInclusao").ToString()) && podeIncluir)
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Text = "Editar";
                    e.Image.Url = "~/imagens/botoes/editarRegDes.PNG";
                }
            }
        }
    }
    private string getChavePrimaria()
    {
        if (grid.FocusedRowIndex >= 0)
            return grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName).ToString();
        else
            return "";
    }
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro = "";
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";


        if (e.Parameters == "Incluir")
        {
            int registrosAfetados = 0;
            cDados.incluirObjetivoEstrategicoAnalises(codigoObjetivoEstrategico, codigoUnidadeSelecionada, txtAnalise.Text.Replace("'", ""), txtRecomendacoes.Text.Replace("'", ""), idUsuarioLogado, "OB", ref registrosAfetados, ref mensagemErro);
            ((ASPxGridView)sender).JSProperties["cpSucesso"] = "Análise incluída com sucesso!";
        }
        else if (e.Parameters == "Editar")
        {
            int registrosAfetados = 0;
            cDados.atualizaAnalise(txtAnalise.Text.Replace("'", ""), txtRecomendacoes.Text.Replace("'", ""), idUsuarioLogado, int.Parse(getChavePrimaria()), ref registrosAfetados, ref mensagemErro);
            ((ASPxGridView)sender).JSProperties["cpSucesso"] = "Análise atualizada com sucesso!";
        }
        ((ASPxGridView)sender).JSProperties["cpErro"] = mensagemErro;
    }

    protected void grid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.CellValue.ToString().Length > 60)
        {
            e.Cell.Text = e.CellValue.ToString().Substring(0, 60) + "  ...";
        }
    }

    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int registrosAfetados = 0;

        cDados.excluiAnalise(idUsuarioLogado, int.Parse(e.Keys[0].ToString()), ref registrosAfetados);
        e.Cancel = true;
        carregaGrid();
    }
    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AnlObjEst");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar.SetVisible(true);TipoOperacao = 'Incluir';LimpaCamposFormulario();pcDetalhesAnalise.Show();", true, true, false, "CatPrj", "AnlObjEst", this);
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

}
