/*
 * OBSERVAÇÕES
 * 
 * MUDANÇAS:
 * 
 * 04/03/2011 :: Alejandro : Permissões da tela [IN_CnsAnl]
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
using System.Drawing;

public partial class _Estrategias_indicador_analises : System.Web.UI.Page
{
    dados cDados;
    DataTable dt;

    private int codigoIndicador = 0, codigoObjetivo = 0, codigoMapa = 0; //ok
    private int codigoEntidade = 0; //ok
    private int idUsuarioLogado = 0;
    private int mes = 0; //ok
    private int ano = 0; //ok
    private int casasDecimais = 0; //ok
    private int codigoUnidade = 0; //ok
    public string resolucaoCliente;
    public int alturaPrincipal;
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

         if (cDados.getInfoSistema("COIN") != null)
            codigoIndicador = int.Parse(cDados.getInfoSistema("COIN").ToString());

        if (cDados.getInfoSistema("COE") != null)
            codigoObjetivo = int.Parse(cDados.getInfoSistema("COE").ToString());

        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HeaderOnTela();

        carregaComboUnidades();
        defineObjetosSessao();

        if (ddlUnidade.SelectedIndex != -1)
            codigoUnidade = int.Parse(ddlUnidade.Value.ToString());

        cDados.setaTamanhoMaximoMemo(txtAnalise, 2000, lblContadorMemoAnalise);
        cDados.setaTamanhoMaximoMemo(txtRecomendacoes, 2000, lblContadorMemoRecomendacoes);

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", codigoMapa*(-1), "null", "IN_CnsAnl");
        
            cDados.aplicaEstiloVisual(this);
            txtAnalise.Font.Size = new FontUnit("10pt");
            txtRecomendacoes.Font.Size = new FontUnit("10pt");
            defineAlturaTela();
        }
        getPermissoesTela();
        carregaGrid("");
        carregaCampos();

        if (codigoObjetivo <= 0)
            tdObjetivoMapa.Style.Add("display", "none");
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/indicador_Analise.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "indicador_Analise", "_Strings", "analises"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Cadastro de Avisos</title>"));
    }

    private void defineAlturaTela()
    {
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int constanteSomatoria = (codigoObjetivo <= 0) ? 90 : 0;

        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 450 + constanteSomatoria;
    }

    private void defineObjetosSessao()
    {
        if (!IsPostBack)
        {
            if (cDados.getInfoSistema("CodigoUnidade") != null)
                ddlUnidade.Value = cDados.getInfoSistema("CodigoUnidade").ToString();
        }

        if (ddlUnidade.SelectedIndex != -1)
            cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Value.ToString());
    }

    private void carregaCampos()
    {
        dt = cDados.getInformacoesIndicador(codigoIndicador, codigoObjetivo).Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtIndicador.Text = dt.Rows[0]["NomeIndicador"].ToString();
            casasDecimais = int.Parse(dt.Rows[0]["CasasDecimais"].ToString());

            ((GridViewDataTextColumn)gvDados.Columns[1]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
            ((GridViewDataTextColumn)gvDados.Columns[2]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
        }
        else
        {
            txtObjetivoEstrategico.Text = "";
            txtIndicador.Text = "";
            txtMapa.Text = "";
        }
    }

    private void getPermissoesTela()
    {
        //Procurar permissão para visualizar Ações Sugeridas.
        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoEntidade, codigoIndicador, codigoMapa*(-1), "IN", "IN_IncAnl", "IN_ExcAnl");
        if (cDados.DataSetOk(ds))
        {
            podeIncluir = int.Parse(ds.Tables[0].Rows[0]["IN_IncAnl"].ToString()) > 0;
            podeExcluir = int.Parse(ds.Tables[0].Rows[0]["IN_ExcAnl"].ToString()) > 0;
        }
    }

    #endregion

    #region GRID VIEW
    
    private void carregaGrid(string where)
    {
        DataTable dtGrid = cDados.getPeriodicidadeIndicador(codigoUnidade, codigoIndicador, "").Tables[0];

        gvDados.DataSource = dtGrid;
        gvDados.DataBind();
    }

    protected void grid_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            bool periodoPermitidoAnalise = gvDados.GetRowValues(e.VisibleIndex, "PeriodoPermitidoAnalise").ToString() == "S";
            string analise = gvDados.GetRowValues(e.VisibleIndex, "Analise").ToString();

            if (periodoPermitidoAnalise)
            {

                if (e.ButtonID.Equals("btnCustomInserir"))
                {
                    if (string.IsNullOrEmpty(analise) && podeIncluir)
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
                else if (e.ButtonID.Equals("btnCustomEditar"))
                {
                    if (string.IsNullOrEmpty(analise) || (!podeIncluir))
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                    else
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
            }
            else
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }

    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex == -1)
        {
            return;
        }
        Color color = new Color();
        int ano = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Ano").ToString());
        int mes = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "mes").ToString());
        int anoAtual = DateTime.Now.Year;
        int mesAtual = DateTime.Now.Month;

        if (e.RowType == GridViewRowType.Data)
        {
            if (!(ano < anoAtual || (ano == anoAtual && mes < mesAtual)))
            {
                color = Color.FromName("#619340");
                /*
                e.Row.BackColor = color;
                e.Row.ForeColor = Color.Black;
                */
                e.Row.ForeColor = color;
            }
        }
    }

    #endregion

    #region COMBOBOX

    private void carregaComboUnidades()
    {
        DataSet dsUnidades = cDados.getUnidadesUsuarioIndicador(codigoIndicador, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), "");

        if (cDados.DataSetOk(dsUnidades))
        {
            ddlUnidade.DataSource = dsUnidades;
            ddlUnidade.TextField = "SiglaUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";
            ddlUnidade.DataBind();
        }

        if (!IsPostBack && ddlUnidade.Items.Count > 0)
        {
            int codigoUnidadeSelecionada = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());

            if (ddlUnidade.Items.FindByValue(codigoUnidadeSelecionada) != null)
            {
                ddlUnidade.Value = codigoUnidadeSelecionada;
            }
            else if (ddlUnidade.Items.FindByValue(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString())) != null)
            {
                ddlUnidade.Value = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            }
            else
            {
                ddlUnidade.SelectedIndex = 0;
            }
        }

        if (ddlUnidade.SelectedIndex != -1)
            cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Value.ToString());
    }

    #endregion

    #region CALLBACK'S

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = "";
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "Análises alteradas com sucesso!";
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "Análise excluída com sucesso!";
        }

        else 
        {
            ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = mensagemErro_Persistencia;
        }
    }

    #endregion

    #region BANCO DE DADOS

    private string getChavePrimaria() // retorna a primary key da tabela.
    {
        string codigoDado = "-1";

        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAnalisePerformance").ToString();

        return codigoDado;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Inclusão do registro
    {
        int registrosAfetados = 0;
        int novoCodigoIndicador = 0;
        string chave = getChavePrimaria();
        string msg = "";

        string analise = txtAnalise.Text;
        string recomendacoe = txtRecomendacoes.Text.Equals("") ? "NULL" : txtRecomendacoes.Text;
        ano = int.Parse(hfGeral.Get("Ano").ToString());
        mes = int.Parse(hfGeral.Get("Mes").ToString());

        try
        {
            if (chave.Equals(""))
            {
                cDados.incluirAnalise(codigoIndicador, codigoUnidade, analise, recomendacoe, idUsuarioLogado
                                        , "E", ano, mes, ref registrosAfetados);
                carregaGrid("");
                gvDados.ClientVisible = false;
            }
            else
            {
                cDados.atualizaAnalise(analise, recomendacoe, idUsuarioLogado, int.Parse(chave), ref registrosAfetados, ref msg);
                hfGeral.Set("hfCodigoIndicador", novoCodigoIndicador);
                carregaGrid("");
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
                gvDados.ClientVisible = false;
            }
            msg = "";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private string persisteExclusaoRegistro()
    {
        string msg = "";
        string chave = getChavePrimaria();
        int registrosAfetados = 0;

        try
        {
            cDados.excluiAnalise(idUsuarioLogado, int.Parse(chave), ref registrosAfetados);
            gvDados.ClientVisible = false;
            msg = "";
            carregaGrid("");
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AnlIndic");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "AnlIndic", lblTituloTela.Text, this);
    }

    #endregion
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
        if (e.Column.Name == "CorIndicador" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;
            e.Text = "l";
            e.TextValue = "l";
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (e.Value.ToString().Contains("Vermelho"))
            {

                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Contains("Amarelo"))
            {

                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Contains("Verde"))
            {
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Contains("Azul"))
            {
                e.BrickStyle.ForeColor = Color.Blue;
            }
            else if (e.Value.ToString().Contains("Branco"))
            {

                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }

        }
    }
}