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
using DevExpress.XtraPrinting;
using System.IO;

public partial class CadastroAquisicoes : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    private int codigoProjeto = -1;
    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    private bool exportaOLAPTodosFormatos = false;

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
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoProjeto, "null", "PR", 0, "null", "PR_IncAqsCtr");
        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoProjeto, "null", "PR", 0, "null", "PR_AltAqsCtr");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoProjeto, "null", "PR", 0, "null", "PR_ExcAqsCtr");

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");
        populaResponsaveis();
        populaDdlConta();
        carregaGvDados();
        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        populaOpcoesExportacao();
        if (!IsPostBack)
        {
            hfGeral.Set("tipoArquivo", "XLS");
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }


    }

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();
        ListEditItem liXLS = new ListEditItem("XLS", "XLS");
        liXLS.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";

        ddlExporta.Items.Add(liXLS);
        ddlExporta.ClientEnabled = false;

        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);
        }

        ddlExporta.SelectedIndex = 0;
    }

    private void populaDdlConta()
    {
        string where1 = string.Format(@"AND pcfc.CodigoConta IN (SELECT CodigoConta 
                                                                    FROM PlanoContaSFluxoCaixa  WHERE
                                                                        CodigoEntidade = {0})", codigoEntidadeUsuarioResponsavel);

        string comandoSQL = string.Format(
                  @"SELECT pcfc.CodigoConta,
                           pcfc.CodigoReservadoGrupoConta + ' - ' +  pcfc.DescricaoConta as DescricaoConta,
                           pcfc.CodigoReservadoGrupoConta
                      FROM {0}.{1}.PlanoContasFluxoCaixa AS pcfc
                     WHERE pcfc.CodigoEntidade = {2}
                       --AND pcfc.IndicaContaAnalitica = 'S'
                       --AND pcfc.EntradaSaida = '{3}'
                       {4}
                       ORDER BY pcfc.CodigoReservadoGrupoConta, pcfc.DescricaoConta ASC
               ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, 'E', where1);
        DataSet dsContas = cDados.getDataSet(comandoSQL); //cDados.getContasAnaliticasEntidade(codigoEntidadeUsuarioResponsavel, "E", where1);
        ddlConta.DataSource = dsContas.Tables[0];
        ddlConta.TextField = "DescricaoConta";
        ddlConta.ValueField = "CodigoConta";
        ddlConta.DataBind();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/CadastroAquisicoes.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings", "CadastroAquisicoes"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 320;
    }
    #endregion

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getAquisicoesProjeto(codigoProjeto, "");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
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

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

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

    #endregion

    #region BANCO DE DADOS

    //popula o combobox de responsaveis
    protected void populaResponsaveis()
    {

        //cDados = new dados();
        string where1 = "";

        DataSet ds = cDados.getUsuariosAtivosEntidade(codigoEntidadeUsuarioResponsavel, where1);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlResponsavel.TextField = "NomeUsuario";
            ddlResponsavel.ValueField = "CodigoUsuario";
            ddlResponsavel.Columns[0].FieldName = "NomeUsuario";
            ddlResponsavel.Columns[1].FieldName = "EMail";
            ddlResponsavel.TextFormatString = "{0}";

            ddlResponsavel.DataSource = ds.Tables[0];
            ddlResponsavel.DataBind();
        }

        //ddlResponsavel.Items.Insert(0, new DevExpress.Web.ListEditItem("Todos", "null"));
        //ddlResponsavel.SelectedIndex = 0;
    }


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
        int codigoAquisicao = -1;
        string mensagemErro = "";
        string aquisicao = txtAquisicao.Text;
        string dataPrevista = (ddlDataPrevista.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrevista.Date);
        string valorPrevisto = spnValorPrevisto.Text == "" ? "NULL" : spnValorPrevisto.Value.ToString();
        string valorReal = (spnValorRealizado.Text == "") ? "NULL" : spnValorRealizado.Value.ToString();
        int codigoResponsavel = (ddlResponsavel.Value != null) ? int.Parse(ddlResponsavel.Value.ToString()) : -1;
        int codigoConta = (ddlConta.Value != null) ? int.Parse(ddlConta.Value.ToString()) : -1;
        string tipoDeItem = txtGrupoAquisicao.Text;
        int status = 1;
        if (ddlContratado.Value != null && ddlContratado.Text == "Sim")
        {
            status = 1;
        }
        else if (ddlContratado.Value != null && ddlContratado.Text == "Não")
        {
            status = 2;
        }
        else if (ddlContratado.Value != null && ddlContratado.Text == "Parcial")
        {
            status = 3;
        }
        bool result = cDados.incluiAquisicaoProjeto(codigoProjeto, aquisicao, dataPrevista, valorPrevisto, valorReal, codigoResponsavel, codigoUsuarioResponsavel, tipoDeItem, status, codigoConta, ref codigoAquisicao, ref mensagemErro);

        if (result == false)
            return "Erro: " + mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoAquisicao = int.Parse(getChavePrimaria());

        string aquisicao = txtAquisicao.Text;
        string dataPrevista = (ddlDataPrevista.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataPrevista.Date);
        string valorPrevisto = spnValorPrevisto.Text == "" ? "NULL" : spnValorPrevisto.Text;
        string valorReal = (spnValorRealizado.Text == "") ? "NULL" : spnValorRealizado.Text;
        int codigoResponsavel = (ddlResponsavel.Value != null) ? int.Parse(ddlResponsavel.Value.ToString()) : -1;
        string mensagemErro = "";
        string tipoDeItem = txtGrupoAquisicao.Text;
        int codigoConta = (ddlConta.Value != null) ? int.Parse(ddlConta.Value.ToString()) : -1;
        int status = 1;
        if (ddlContratado.Value != null && ddlContratado.Text == "Sim")
        {
            status = 1;
        }
        else if (ddlContratado.Value != null && ddlContratado.Text == "Não")
        {
            status = 2;
        }
        else if (ddlContratado.Value != null && ddlContratado.Text == "Parcial")
        {
            status = 3;
        }

        bool result = cDados.atualizaAquisicaoProjeto(codigoAquisicao, aquisicao, dataPrevista, valorPrevisto, valorReal, tipoDeItem, codigoResponsavel, codigoUsuarioResponsavel, status, codigoConta, ref mensagemErro);

        if (result == false)
            return "Erro: " + mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoAquisicao = int.Parse(getChavePrimaria());

        bool result = cDados.excluiAquisicaoProjeto(codigoAquisicao, codigoUsuarioResponsavel);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvDados();
            return "";
        }
    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();

        setImagenExportacao(parametro);
    }

    private void setImagenExportacao(string opcao)
    {
        string nomeArquivo = "";

        if (opcao == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (opcao == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (opcao == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (opcao == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (opcao == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";

            try
            {
                gvDados.Columns[0].Visible = false;
                if (hfGeral.Get("tipoArquivo").ToString() == "PDF")
                {
                    nomeArquivo = "ListaDemandas_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    ASPxGridViewExporter1.WritePdfToResponse(p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "ListaDemandas_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Text;
                    ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    app = "application/ms-excel";
                }
                gvDados.Columns[0].Visible = true;
            }
            catch (Exception ex)
            {
                gvDados.Columns[0].Visible = true;
                erro = ex.Message;// "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML")
                {
                    nomeArquivo = "\"" + nomeArquivo + "\"";
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", app);
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }
            }
            else
            {
                /*string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";*/
                string script = String.Format(@"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Não foi possível exportar\n{0}', 'erro', true, false, null);</script>", erro);

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
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
        if (e.RowType == GridViewRowType.Footer && e.Text != "")
        {
            e.TextValueFormatString = "{0:c2}";
            try
            {
                e.TextValue = double.Parse(e.Text.Replace(".", ""));
            }
            catch { }
        }
    }
}
