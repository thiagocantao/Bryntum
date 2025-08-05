/*
 ALTERAÇÕES:
 
    29/11/2010 : BY Alejandro
    Alterar opção onde registra-se a análise de indicadores de projetos para contemplar 
    o novo modelo que irá gravar as análises em uma tabela denominada AnalisePerformance
    ao invés de AnalisePerformanceProjeto.
 
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

public partial class _Projetos_DadosProjeto_AnaliseDasMetas : System.Web.UI.Page
{
    dados cDados;
    private int alturaPrincipal = 0;
    private string idUsuarioLogado;
    private string codigoEntidade;
    private string codigoProjeto;
    private bool podeEditar = false;

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        codigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();
        codigoProjeto = Request.QueryString["ID"].ToString();

        podeEditar = (cDados.verificaAcessoStatusProjeto(int.Parse(codigoProjeto)));

        cDados.setaTamanhoMaximoMemo(mmAnalise, 2000, lblContadorMemo);
        cDados.setaTamanhoMaximoMemo(mmRecomendacoes, 2000, lblContadorMemo0);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, int.Parse(idUsuarioLogado), int.Parse(codigoEntidade), int.Parse(codigoProjeto), "null", "PR", 0, "null", "PR_AnlMta");
        }

        HeaderOnTela();
        cDados.aplicaEstiloVisual(this);
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());

        populaGridMeta();

        gvPeriodo.Settings.ShowFilterRow = false;
        gvPeriodo.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvMeta.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AnaliseDasMetas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("AnaliseDasMetas", "_Strings"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int altura1 = 0;
        int largura1 = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura1, out altura1);
        alturaPrincipal = altura1;
        if (altura1 > 0)
        {
            int altura2 = ((altura1 - 420) / 2);
            gvPeriodo.Settings.VerticalScrollableHeight = altura2;
            gvMeta.Settings.VerticalScrollableHeight = altura2;
        }
        gvPeriodo.Width = new Unit("100%");
        gvMeta.Width = new Unit("100%");
    }

    #endregion

    #region GRID

    private void populaGridMeta()
    {
        DataSet ds = cDados.getIndicadoresProjeto(int.Parse(codigoProjeto), "");

        if (cDados.DataSetOk(ds))
        {
            gvMeta.DataSource = ds.Tables[0];
            gvMeta.DataBind();

            if (gvMeta.FocusedRowIndex >= 0)
            {
                hfGeral.Set("CodigoMeta", gvMeta.GetRowValues(gvMeta.FocusedRowIndex, "CodigoMetaOperacional").ToString());
                carregaGridPeriodo(int.Parse(hfGeral.Get("CodigoMeta").ToString()));
                gvPeriodo.ClientVisible = true;
            }
        }

    }

    private void carregaGridPeriodo(int codigoMeta)
    {
        DataTable dtGrid = cDados.getPeriodicidadeIndicadorOperacional(codigoMeta).Tables[0];

        gvPeriodo.DataSource = dtGrid;
        gvPeriodo.DataBind();


        if (cDados.DataTableOk(dtGrid))
        {
            string unidadeMedida = dtGrid.Rows[0]["SiglaUnidadeMedida"].ToString();
            string casasDecimais = dtGrid.Rows[0]["CasasDecimais"].ToString();

            gvPeriodo.Columns[2].Caption = "Meta (" + unidadeMedida + ")";
            gvPeriodo.Columns[3].Caption = "Realizado (" + unidadeMedida + ")";
            gvPeriodo.Columns[6].Caption = "Meta Acumulada (" + unidadeMedida + ")";
            gvPeriodo.Columns[7].Caption = "Realizado Acumulado (" + unidadeMedida + ")";

            ((GridViewDataTextColumn)gvPeriodo.Columns[2]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
            ((GridViewDataTextColumn)gvPeriodo.Columns[3]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
            ((GridViewDataTextColumn)gvPeriodo.Columns[6]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
            ((GridViewDataTextColumn)gvPeriodo.Columns[7]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
        }
    }

    protected void gvPeriodo_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {

        string parametro = e.Parameters.ToString();

        if (parametro == "btnCustomExcluir")
        {
            string ano = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "Ano").ToString(); //hfGeral.Get("Ano").ToString();
            string mes = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "Mes").ToString();
            string codigoMeta = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "CodigoMetaOperacional").ToString();
            string codigoIndicador = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "CodigoIndicador").ToString(); 


            if (cDados.excluirAnalisePerformanceMeta(codigoIndicador, codigoMeta, ano, mes, "MT"))
                carregaGridPeriodo(int.Parse(codigoIndicador));
        }
        if (hfGeral.Contains("CodigoMeta"))
            carregaGridPeriodo(int.Parse(hfGeral.Get("CodigoMeta").ToString()));
    }

    protected void gvPeriodo_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex >= 0)
        {
            string tipoEdicao = gvPeriodo.GetRowValues(e.VisibleIndex, "TipoEdicao") != null ? gvPeriodo.GetRowValues(e.VisibleIndex, "TipoEdicao").ToString() : "";
            int ano = int.Parse(gvPeriodo.GetRowValues(e.VisibleIndex, "Ano") != null ? gvPeriodo.GetRowValues(e.VisibleIndex, "Ano").ToString() : DateTime.Now.Year.ToString());
            int mes = int.Parse(gvPeriodo.GetRowValues(e.VisibleIndex, "Mes") != null ? gvPeriodo.GetRowValues(e.VisibleIndex, "Mes").ToString() : DateTime.Now.Month.ToString());
            int anoActual = DateTime.Now.Year;
            int mesActual = DateTime.Now.Month;

            if (ano > anoActual || podeEditar == false)
            {
                if (e.ButtonID == "btnCustomNovo")
                {
                    e.Image.Url = "~/imagens/botoes/IncluirRegDes.png";
                    e.Enabled = false;
                }
                if (e.ButtonID == "btnCustomEdit")
                {
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                    e.Enabled = false;
                }
                if (e.ButtonID == "btnCustomExcluir")
                {
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    e.Enabled = false;
                }
                if (e.ButtonID == "btnCustomDetalhe")
                {
                    e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
                    e.Enabled = false;
                }
            }
            else if (ano == anoActual)
            {
                if (mes > mesActual)
                {
                    if (e.ButtonID == "btnCustomNovo")
                    {
                        e.Image.Url = "~/imagens/botoes/IncluirRegDes.png";
                        e.Enabled = false;
                    }
                    if (e.ButtonID == "btnCustomEdit")
                    {
                        e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                        e.Enabled = false;
                    }
                    if (e.ButtonID == "btnCustomExcluir")
                    {
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Enabled = false;
                    }
                    if (e.ButtonID == "btnCustomDetalhe")
                    {
                        e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
                        e.Enabled = false;
                    }
                }
                else
                {
                    if (tipoEdicao == "I")
                    {
                        if (e.ButtonID == "btnCustomEdit")
                        {
                            e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                            e.Enabled = false;
                        }
                        if (e.ButtonID == "btnCustomExcluir")
                        {
                            e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                            e.Enabled = false;
                        }
                        if (e.ButtonID == "btnCustomDetalhe")
                        {
                            e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
                            e.Enabled = false;
                        }
                    }
                    else
                    {
                        if (e.ButtonID == "btnCustomNovo")
                        {
                            e.Image.Url = "~/imagens/botoes/IncluirRegDes.png";
                            e.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                if (tipoEdicao == "I")
                {
                    if (e.ButtonID == "btnCustomEdit")
                    {
                        e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                        e.Enabled = false;
                    }
                    if (e.ButtonID == "btnCustomExcluir")
                    {
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Enabled = false;
                    }
                    if (e.ButtonID == "btnCustomDetalhe")
                    {
                        e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
                        e.Enabled = false;
                    }
                }
                else
                {
                    if (e.ButtonID == "btnCustomNovo")
                    {
                        e.Image.Url = "~/imagens/botoes/IncluirRegDes.png";
                        e.Enabled = false;
                    }
                }
            }
        }
    }

    #endregion

    /// <summary>
    /// PainelCallback usado para executar diversas ações previstas para a tabela [AnalisePerformanceProjeto].
    /// </summary>
    /// <param name="sender">Objeto PainelCallback</param>
    /// <param name="e">Ela recebe os parâmetros enviados para o painelCallback</param>
    protected void pnCallbackDetalhe_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        //Parametro: "btnCustomNovo" - "btnCustomEdit" - "btnCustomExcluir"
        string parametro = e.Parameter;
        string ano = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "Ano").ToString(); //hfGeral.Get("Ano").ToString();
        string mes = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "Mes").ToString();
        string codigoMeta = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "CodigoMetaOperacional").ToString();
        string indicador = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "CodigoIndicador").ToString(); 

        getDadosAnaliseperfomanceProjeto(codigoMeta, indicador, ano, mes);

        if("btnCustomNovo" == parametro)
        {
            hfAcao.Set("acao", "Novo");
        }
        else if("btnCustomEdit" == parametro)
        {
            hfAcao.Set("acao", "Editar");
        }
        //else if ("btnCustomExcluir" == parametro)
        //{
        //    hfAcao.Set("acao", "Excluir");
        //    persisteExclusaoRegistro(codigoProjeto, indicador, ano, mes);
        //}
        else if ("btnCustomDetalhe" == parametro)
        {
            hfAcao.Set("acao", "Detalhe");
            btnSalvar.ClientVisible = false;
            mmAnalise.ClientEnabled = false;
            mmRecomendacoes.ClientEnabled = false;
        }
        else if ("Salvar" == parametro)
        {
            string analise = hfAcao.Get("analise").ToString();
            string recomendacoes = hfAcao.Get("recomendacoes").ToString();

            if (hfAcao.Contains("acao"))
            {
                if ("Novo" == hfAcao.Get("acao").ToString())
                {
                    persisteInclusaoRegistro(codigoMeta, indicador, ano, mes, analise, recomendacoes);
                    hfAcao.Set("acao", "Salvar");
                }
                else if ("Editar" == hfAcao.Get("acao").ToString())
                {
                    persisteEditacaoRegistro(codigoMeta, indicador, ano, mes, analise, recomendacoes);
                    hfAcao.Set("acao", "Salvar");
                }
            }
        }
    }

    #region tabela [AnalisePerformanceProjeto]

    private void getDadosAnaliseperfomanceProjeto(string codigoMeta, string codigoIndicador, string ano, string mes)
    {
        DataTable dt = cDados.getAnalisePerformanceObjeto(codigoMeta, ano, mes, "MT").Tables[0];

        if (cDados.DataTableOk(dt))
        {
            visibilidadLabels(true);

            mmAnalise.Text = dt.Rows[0]["Analise"].ToString();
            mmRecomendacoes.Text = dt.Rows[0]["Recomendacoes"].ToString();
            lblDataInclusao.Text = dt.Rows[0]["DataInclusao"].ToString();
            lblUsuarioInclusao.Text = dt.Rows[0]["UsuarioInclusao"].ToString();
            
            lblDataAlteracao.Text = dt.Rows[0]["DataUltimaAlteracao"].ToString();
            lblUsuarioAlteracao.Text = dt.Rows[0]["UsuarioAlteracao"].ToString();

            lblDataInclusao.ClientVisible = ("" != lblUsuarioInclusao.Text);
            lblUsuarioInclusao.ClientVisible = ("" != lblUsuarioInclusao.Text);
            lblCaptionInclusao.ClientVisible = ("" != lblUsuarioInclusao.Text);
            lblCaptionIncluidoPor.ClientVisible = ("" != lblUsuarioInclusao.Text);

            lblDataAlteracao.ClientVisible = ("" != lblUsuarioAlteracao.Text);
            lblUsuarioAlteracao.ClientVisible = ("" != lblUsuarioAlteracao.Text);
            lblCaptionUltimaAlteracao.ClientVisible = ("" != lblUsuarioAlteracao.Text);
            lblCaptionAlteradoPor.ClientVisible = ("" != lblUsuarioAlteracao.Text);
        }
        else
        {
            mmAnalise.Text = "";
            mmRecomendacoes.Text = "";
            lblDataInclusao.Text = ""; 
            lblUsuarioInclusao.Text = ""; 
            lblDataAlteracao.Text = ""; 
            lblUsuarioAlteracao.Text = ""; 

            visibilidadLabels(false);
        }
    }

    private void visibilidadLabels(bool visible)
    {
        lblCaptionIncluidoPor.ClientVisible = visible;
        lblCaptionInclusao.ClientVisible = visible;
        lblCaptionUltimaAlteracao.ClientVisible = visible;
        lblCaptionAlteradoPor.ClientVisible = visible;
        lblDataInclusao.ClientVisible = visible;
        lblUsuarioInclusao.ClientVisible = visible;
        lblDataAlteracao.ClientVisible = visible;
        lblUsuarioAlteracao.ClientVisible = visible;
    }

    private void persisteInclusaoRegistro(string codigoMeta, string indicador, string ano, string mes, string analise, string recomendacoes)
    {
        if (cDados.incluirAnalisePerformanceMeta(indicador, codigoMeta, ano, mes, analise, recomendacoes, idUsuarioLogado, "MT", "O"))
        {
            carregaGridPeriodo(int.Parse(codigoMeta));
            getDadosAnaliseperfomanceProjeto(codigoMeta, indicador, ano, mes);
        }
    }

    private void persisteEditacaoRegistro(string codigoMeta, string indicador, string ano, string mes, string analise, string recomendacoes)
    {
        if (cDados.atualizaAnalisePerformanceMeta(indicador, codigoMeta, ano, mes, analise, recomendacoes, idUsuarioLogado, "MT"))
        {
            carregaGridPeriodo(int.Parse(codigoMeta));
            getDadosAnaliseperfomanceProjeto(codigoMeta, indicador, ano, mes);
        }
    }

    private void persisteExclusaoRegistro(string codigoMeta, string indicador, string ano, string mes)
    {
        if (cDados.excluirAnalisePerformanceMeta(indicador, codigoMeta, ano, mes, "MT"))
            carregaGridPeriodo(int.Parse(codigoMeta));
    }

    #endregion
}


