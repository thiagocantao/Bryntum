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
using System.Text.RegularExpressions;
using System.Linq;

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
    private bool exibeBotaoEdicao = true;
    private bool existeAnaliseOrcamento = true;
    private bool habilitaInclusaoAnaliseOrcamento = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        cDados = CdadosUtil.GetCdados(null);

        if (!IsPostBack && !IsCallback)
        {
            try
            {
                hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
                hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
                hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
                hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
                hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
                hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());
            }
            catch
            {
                Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
                Response.End();
            }
        }

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
        cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
        cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
        cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());

        this.Title = cDados.getNomeSistema();

        exibeBotaoEdicao = VerificaPeriodoEdicao();
        ConfiguraTamanhoMaximoImagem();
        cDados.aplicaEstiloVisual(Page);
    }

    private void ConfiguraTamanhoMaximoImagem()
    {
        DataTable dsParametros = cDados.getParametrosSistema("tamanhoMaximoArquivoAnexoEmMegaBytes").Tables[0];
        string parametro = dsParametros.Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"] as string;
        int tamanhoAnexoEmMegaBytes;
        if (int.TryParse(parametro, out tamanhoAnexoEmMegaBytes))
        {
            const int quantidadeBytesEmUmMegaByte = 1024 * 1024;
            long tamanhoImagemEmBytes = quantidadeBytesEmUmMegaByte * tamanhoAnexoEmMegaBytes;
            htmlAnalise.SettingsDialogs.InsertImageDialog.SettingsImageUpload.ValidationSettings.MaxFileSize = tamanhoImagemEmBytes;
        }
    }

    private bool VerificaPeriodoEdicao()
    {
        bool permiteEdicao = true;
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel
            , "diaInicioEdicaoAnaliseCritica", "diaTerminoEdicaoAnaliseCritica");
        DataRow row = dsParametros.Tables[0].AsEnumerable().First();
        int diaInicio = 0;
        int diaTermino = 0;
        int diaHoje = DateTime.Today.Day;
        try
        {
            diaInicio = int.Parse(row.Field<string>("diaInicioEdicaoAnaliseCritica"));
            diaTermino = int.Parse(row.Field<string>("diaTerminoEdicaoAnaliseCritica"));
            permiteEdicao = (diaInicio <= diaHoje && diaHoje <= diaTermino) ||
                (diaInicio > diaTermino && (diaInicio < diaHoje || diaHoje < diaTermino));
        }
        catch (Exception) { }

        return permiteEdicao;
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

        if (cDados.verificaAcessoStatusProjeto(codigoProjeto))
        {
            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_IncAnl");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_AltAnl");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_ExcAnl");

            cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);
        }

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CnsAnl");
            carregaGvAnalise();
        }

        carregaGvDados();

        gvDados.JSProperties["cp_Salvar"] = "N";
        gvDados.JSProperties["cp_Msg"] = "";

        gvAnalises.Settings.ShowFilterRow = false;
        gvAnalises.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvAnalises.SettingsBehavior.AllowSort = false;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 400;
        gvDados.Width = new Unit("100%");
    }
    #endregion

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getAnalisePerformanceObjeto(codigoProjeto.ToString(), "", "", "PR");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }


    }

    private void carregaGvAnalise()
    {
        DataSet dsParametros = cDados.getParametrosSistema("MostraAnaliseOrcamento"); // UtilizaOrcamentoERP
        existeAnaliseOrcamento = false;

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["MostraAnaliseOrcamento"] + "" == "S")
        {
            // verifica se existem crs associados ao projeto
            DataSet dsAnalise = cDados.getAnalisesCriticas(codigoProjeto.ToString());

            if ((cDados.DataSetOk(dsAnalise) && dsAnalise.Tables[0].Rows.Count > 0))
            {
                existeAnaliseOrcamento = dsAnalise.Tables[1].Rows.Count > 0;
                habilitaInclusaoAnaliseOrcamento = existeAnaliseOrcamento;
                gvAnalises.DataSource = dsAnalise.Tables[1];
                gvAnalises.DataBind();


            }
        }

        lblAnalise.ClientVisible = existeAnaliseOrcamento;
        gvAnalises.ClientVisible = existeAnaliseOrcamento;
        htmlAnalise.Width = existeAnaliseOrcamento ? 600 : 800;
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvAnalise();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (gvDados.VisibleRowCount == 0) return;
        switch (e.ButtonID)
        {
            case "btnEditar":
                bool permiteEditarRegistro = "S".Equals(
                    gvDados.GetRowValues(e.VisibleIndex, "IndicaRegistroEditavel"));
                e.Enabled = podeEditar && permiteEditarRegistro;
                e.Visible = exibeBotaoEdicao ?
                    DevExpress.Utils.DefaultBoolean.Default :
                    DevExpress.Utils.DefaultBoolean.False;
                if (!e.Enabled)
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                //else
                //{
                //    if (!existeAnaliseOrcamento)
                //    {
                //        e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                //        e.Text = "Não existem análises de orçamento relacionadas com o projeto, você deve primeiro incluir estas análises!";
                //        e.Enabled = false;
                //    }
                //}
                break;
            case "btnExcluir":
                bool permiteExcluirRegistro = "N".Equals(
                    gvDados.GetRowValues(e.VisibleIndex, "ExisteVinculoStatusReport"));
                e.Enabled = podeExcluir && permiteExcluirRegistro;
                if (!e.Enabled)
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
        gvDados.JSProperties["cp_Operacao"] = e.Parameter;

        htmlAnalise.Html = AtualizaReferenciasImagensConteudoHtml();
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "EditarInclusao")
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

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    private string AtualizaReferenciasImagensConteudoHtml()
    {
        string input = htmlAnalise.Html;
        string pattern = @"<img(?:\s[^/>]*)?\ssrc=""(?<url>[^""]*)""(?:\s[^/>]*)?/>";
        return Regex.Replace(input, pattern, AvaliaUrl);
    }

    string AvaliaUrl(Match match)
    {
        var value = match.Value;
        var url = match.Groups["url"].Value;
        if (!url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
        {
            var path = url;
            const string BARRA = "/";
            const string BARRA_INVERTIDA = "\\";
            var separador = path.StartsWith(BARRA) ? string.Empty : path.StartsWith(BARRA_INVERTIDA) ? string.Empty : BARRA;
            var enderecoAplicacao = Request.Url.GetLeftPart(UriPartial.Authority);
            url = string.Format("{0}{1}{2}", enderecoAplicacao, separador, path);
            value = value.Replace(path, url);
        }
        return value;
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {



        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string analise = htmlAnalise.Html.Replace("'", "''");

        bool result = cDados.incluiAnaliseProjeto(codigoProjeto, analise, codigoUsuarioResponsavel);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvAnalise();
            //carregaGvDados();
            gvDados.JSProperties["cp_Operacao"] = "EditarInclusao";
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {

        int codigoAnalise;

        bool chavePrimaria = int.TryParse(getChavePrimaria(), out codigoAnalise);

        if (chavePrimaria)
        {
            string analise = htmlAnalise.Html.Replace("'", "''");

            bool result = true;
            if (VerificaHouveAlteracaoAnalise(codigoAnalise, analise))
                result = cDados.atualizaAnaliseProjeto(codigoAnalise, analise, codigoUsuarioResponsavel);

            if (!result)
                return "Erro ao salvar o registro!";
            else
            {
                carregaGvAnalise();
                //carregaGvDados();

                gvDados.JSProperties["cp_Operacao"] = gvDados.JSProperties["cp_Operacao"].ToString() == "EditarInclusao" ? "EditarInclusao" : "Editar";
                return "";
            }
        }
        else
        {
            return "Erro ao salvar o registro!";
        }
    }

    private bool VerificaHouveAlteracaoAnalise(int codigoAnalise, string analise)
    {
        string comandoSql = string.Format(@"
 SELECT ap.Analise
   FROM [AnalisePerformance] ap
  WHERE ap.CodigoAnalisePerformance = {0}", codigoAnalise);
        DataSet dsTemp = cDados.getDataSet(comandoSql);
        if (dsTemp.Tables[0].Rows.Count > 0)
        {
            string anliseOriginal = dsTemp.Tables[0].Rows[0]["Analise"] as string;
            return analise != anliseOriginal;
        }

        return !string.IsNullOrWhiteSpace(analise);
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoAnalise = int.Parse(getChavePrimaria());

        bool result = cDados.excluiAnaliseProjeto(codigoAnalise);

        if (result == false)
            return "Erro ao excluir o registro!";
        else
        {
            carregaGvAnalise();
            carregaGvDados();
            return "";
        }
    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvAnalise();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;

        if (coluna.FieldName == "Analise")
        {
            string strValue = e.CellValue.ToString();
            strValue = Regex.Replace(strValue, @"<[^>]*>", " ");
            if (e.CellValue != null && strValue.Length > 200)
            {
                e.Cell.ToolTip = strValue;
                strValue = strValue.Substring(0, 200) + "...";
            }
            e.Cell.Text = strValue;
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "S")
        {
            gvDados.JSProperties["cp_Operacao"] = hfGeral.Get("TipoOperacao") + "";

            if (hfGeral.Get("TipoOperacao") + "" == "Incluir")
            {
                gvDados.JSProperties["cp_Msg"] = persisteInclusaoRegistro();
            }
            if (hfGeral.Get("TipoOperacao") + "" == "Editar")
            {
                gvDados.JSProperties["cp_Msg"] = persisteEdicaoRegistro();

            }
            if (hfGeral.Get("TipoOperacao") + "" == "EditarInclusao")
            {
                gvDados.JSProperties["cp_Msg"] = persisteEdicaoRegistro();
            }
            gvDados.JSProperties["cp_Salvar"] = "S";
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AnalisePrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir && exibeBotaoEdicao, "onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "AnalisePrj", "Análises do Projeto", this);
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
        if (e.Column.Name == "Analise")
        {
            string strValue = gvDados.GetRowValuesByKeyValue(e.KeyValue, "Analise") + "";
            strValue = Regex.Replace(strValue, @"<[^>]*>", "", RegexOptions.IgnoreCase).Replace(Environment.NewLine, "");
            strValue = Server.HtmlDecode(strValue);
            e.Column.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False;
            if (strValue.Replace("&nbsp;", "").Trim() != "")
            {
                e.Text = strValue.Replace("&nbsp;", "").TrimStart();
                e.TextValue = strValue.Replace("&nbsp;", "").TrimStart();
            }
        }
    }
}
