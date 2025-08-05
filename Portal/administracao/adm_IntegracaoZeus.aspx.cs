using System.Linq;
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
using System.Collections.Specialized;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Drawing;
using System.Diagnostics;

public partial class administracao_adm_IntegracaoZeus : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool exportaOLAPTodosFormatos = false;
    public string labelDotacaoOrcamento = "Dotação",
                  labelSuplementoOrcamento = "Suplementação",
                  labelTranspostoOrcamento = "Transposição",
                  nomeArquivoDotacao = "dpun_",
                  nomeArquivoSuplemento = "spun_",
                  nomeArquivoTransposto = "tpun_";
    //define o título da página
    public string titulo = "Integração Zeus";
    // bloco de permissões na tela 
    private string permissao = "EN_IntegZeus";

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    //fim bloco de permissões na tela 

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        lblTituloTela.Text = titulo;
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
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", permissao);
        }


        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

            hfGeral.Set("tipoArquivo", "ZEUS");

            DataSet ds = cDados.getParametrosSistema(CodigoEntidade, "exportaOLAPTodosFormatos",
                                                                     "labelDotacaoOrcamento",
                                                                     "labelSuplementoOrcamento",
                                                                     "labelTranspostoOrcamento",
                                                                     "nomeArquivoDotacao",
                                                                     "nomeArquivoSuplemento",
                                                                     "nomeArquivoTransposto");
            if (ds != null && ds.Tables[0] != null)
            {
                exportaOLAPTodosFormatos = (ds.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
                labelDotacaoOrcamento = ds.Tables[0].Rows[0]["labelDotacaoOrcamento"] != null ? ds.Tables[0].Rows[0]["labelDotacaoOrcamento"].ToString() : "";
                labelSuplementoOrcamento = ds.Tables[0].Rows[0]["labelSuplementoOrcamento"] != null ? ds.Tables[0].Rows[0]["labelSuplementoOrcamento"].ToString() : "";
                labelTranspostoOrcamento = ds.Tables[0].Rows[0]["labelTranspostoOrcamento"] != null ? ds.Tables[0].Rows[0]["labelTranspostoOrcamento"].ToString() : "";
                nomeArquivoDotacao = ds.Tables[0].Rows[0]["nomeArquivoDotacao"] + "" != "" ? ds.Tables[0].Rows[0]["nomeArquivoDotacao"].ToString() : "";
                nomeArquivoSuplemento = ds.Tables[0].Rows[0]["nomeArquivoSuplemento"] + "" != "" ? ds.Tables[0].Rows[0]["nomeArquivoSuplemento"].ToString() : "";
                nomeArquivoTransposto = ds.Tables[0].Rows[0]["nomeArquivoTransposto"] + "" != "" ? ds.Tables[0].Rows[0]["nomeArquivoTransposto"].ToString() : "";
            }

            //if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
            //    exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");

            AspxbuttonGeraDotação.Text = "Gera " + labelDotacaoOrcamento;
            AspxbuttonGeraSuplementacao.Text = "Gera " + labelSuplementoOrcamento;
            AspxbuttonGeraTransposicao.Text = "Gera " + labelTranspostoOrcamento;
            populaOpcoesExportacao();
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 175);
        if (altura > 0)
        {
            gvDadosGeraArquivo.Settings.VerticalScrollableHeight = altura - 170;
        }
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_IntegracaoZeus.js""></script>"));
    }



    #region DVGRID

    private void populaGridGeraArquivo(string tipo)
    {
        string nomeArquivoGerar="Invalido";
        if (tipo == "T")
        {
            gvDadosGeraArquivo.SettingsText.Title = "Dados de " + labelTranspostoOrcamento + " a serem exportados para o Zeus";
            nomeArquivoGerar = nomeArquivoTransposto;
        }
        else if (tipo == "S"){
            gvDadosGeraArquivo.SettingsText.Title = "Dados de " + labelSuplementoOrcamento + " a serem exportados para o Zeus";
            nomeArquivoGerar = nomeArquivoSuplemento;
        }
        else if (tipo == "D")
        {
            gvDadosGeraArquivo.SettingsText.Title = "Dados de " + labelDotacaoOrcamento + " a serem exportados para o Zeus";
            nomeArquivoGerar = nomeArquivoDotacao;
        }



        DataSet ds = cDados.getDadosGeraArquivo(CodigoEntidade, tipo, nomeArquivoGerar);
        if (cDados.DataSetOk(ds))
        {
            gvDadosGeraArquivo.DataSource = ds;
            gvDadosGeraArquivo.DataBind();
            pnExportacao.ClientVisible = true;
            gvDadosGeraArquivo.ClientVisible = true;
        }
    }

    #endregion

    #region EXPORTAÇÃO



    protected void btnExcel_Click(object sender, EventArgs e)
    {

        populaGridGeraArquivo(hfGeral.Get("TipoConsulta").ToString());

        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");

            string nomeArquivo = "", app = "", erro = "", arquivoGerado = "";
            if (hfGeral.Get("TipoConsulta").ToString() == "T")
                nomeArquivo = nomeArquivoTransposto + dataHora;
            else if (hfGeral.Get("TipoConsulta").ToString() == "S")
                nomeArquivo = nomeArquivoSuplemento + dataHora;
            else if (hfGeral.Get("TipoConsulta").ToString() == "D")
                nomeArquivo = nomeArquivoDotacao + dataHora;
            try
            {

                if (hfGeral.Get("tipoArquivo").ToString() == "ZEUS")
                {

                    nomeArquivo = nomeArquivo + ".txt";
                    string raiz = "";
                    DataSet dsParametros = cDados.getParametrosSistema(CodigoEntidade, "diretorioIntegracaoZeus");
                    if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
                    {
                        raiz = dsParametros.Tables[0].Rows[0]["diretorioIntegracaoZeus"].ToString();
                        if (!raiz.Equals("") && !raiz.Substring(raiz.Length - 1, 1).Equals("\\"))
                        {
                            raiz = raiz + '\\';
                        }
                    }
                    if (raiz == "")
                    {
                        throw new Exception(@"O parâmetro que identifica o Diretório onde serão armazenados os arquivos gerados para integração com o ZEUS não foi informado." +
                                            @"\n\nVerifique por favor na tela de parâmetros o grupo Outras Configurações!");
                    }
                    raiz = raiz + "Exportar\\";

                    //Se o diretório não existir...
                    if (!Directory.Exists(raiz))
                    {
                        //Criamos um com o nome folder
                        Directory.CreateDirectory(raiz);
                    }
                    string folder = @raiz;
                    arquivoGerado = @folder + nomeArquivo;
                    

                    StreamWriter swriterExporta = new StreamWriter(arquivoGerado, false, System.Text.Encoding.GetEncoding("UTF-8"));


                    geraArquivoZeus(swriterExporta, arquivoGerado);
                    swriterExporta.Flush();
                    swriterExporta.Close();

                   
                    nomeArquivo = arquivoGerado;


                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {  
                    gvDadosGeraArquivo.Columns["col09"]. Visible = true;
                    gvDadosGeraArquivo.Columns["col11"].Visible = true;
                    gvDadosGeraArquivo.Columns["col13"].Visible = true;
                    gvDadosGeraArquivo.Columns["col14"].Visible = true;
                    gvDadosGeraArquivo.Columns["col15"].Visible = true;
                    gvDadosGeraArquivo.Columns["col17"].Visible = true;
                    gvDadosGeraArquivo.Columns["col18"].Visible = true;
                    gvDadosGeraArquivo.Columns["col19"].Visible = true;
                    nomeArquivo = nomeArquivo + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Text;
                    ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    app = "application/ms-excel";
                    gvDadosGeraArquivo.Columns["col09"].Visible = false;
                    gvDadosGeraArquivo.Columns["col11"].Visible = false;
                    gvDadosGeraArquivo.Columns["col13"].Visible = false;
                    gvDadosGeraArquivo.Columns["col14"].Visible = false;
                    gvDadosGeraArquivo.Columns["col15"].Visible = false;
                    gvDadosGeraArquivo.Columns["col17"].Visible = false;
                    gvDadosGeraArquivo.Columns["col18"].Visible = false;
                    gvDadosGeraArquivo.Columns["col19"].Visible = false;
                }
            }
            catch (Exception ex)
            {

                erro = ex.Message;
            }
           

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML" && hfGeral.Get("tipoArquivo").ToString() != "ZEUS")
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
                if (hfGeral.Get("tipoArquivo").ToString() == "ZEUS")
                {
                    lblMostraMensagem.Text = "Arquivo gerado com sucesso \n[" + arquivoGerado.Replace("\\", "/") + "] ";
                    ASPxRoundPanel1.ClientVisible = true;
                    string name = Path.GetFileName(nomeArquivo);

                    string type = "application/octet-stream";                   
                    Response.AppendHeader("content-disposition",
                            "attachment; filename=\"" + name + "\"");
                    Response.ContentType = type;
                    Response.WriteFile(nomeArquivo);
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {

                string script = String.Format(@"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Não foi possível exportar\n{0}', 'erro', true, false, null);</script>", erro);

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }



    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();
        ListEditItem liPDF = new ListEditItem("ZEUS", "ZEUS");
        liPDF.ImageUrl = "~/imagens/menuExportacao/iconoTXT.png";

        ddlExporta.Items.Add(liPDF);
        ListEditItem liXLS = new ListEditItem("XLS", "XLS");
        liXLS.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";

        ddlExporta.Items.Add(liXLS);
        ddlExporta.ClientEnabled = false;

        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;
        }

        ddlExporta.SelectedIndex = 0;
        setImagenExportacao("ZEUS");
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

        if (opcao == "ZEUS")
            nomeArquivo = "~/imagens/menuExportacao/iconoTXT.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }


    protected void geraArquivoZeus(StreamWriter swriterExporta, string arquivo)
    {
      

        
        string linha = "";
        for (int i = 0; i < gvDadosGeraArquivo.VisibleRowCount; i++)
        {
            DataRowView dt = (DataRowView)gvDadosGeraArquivo.GetRow(i);

            linha = dt["col01"].ToString().Trim() + "\t" +
                    dt["col02"].ToString().Trim() + "\t" +
                    dt["col03"].ToString().Trim() + "\t" +
                    dt["col04"].ToString().Trim() + "\t" +
                    dt["col05"].ToString().Trim() + "\t" +
                    dt["col06"].ToString().Trim() + "\t" +
                    dt["col07"].ToString().Trim() + "\t" +
                    dt["col08"].ToString().Trim() + "\t" +
                    dt["col09"].ToString().Trim() + "\t" +
                    dt["col10"].ToString().Trim() + "\t" +
                    dt["col11"].ToString().Trim() + "\t" +
                    dt["col12"].ToString().Trim() + "\t" +
                    dt["col13"].ToString().Trim() + "\t" +
                    dt["col14"].ToString().Trim() + "\t" +
                    dt["col15"].ToString().Trim() + "\t" +
                    dt["col16"].ToString().Trim() + "\t" +
                    dt["col17"].ToString().Trim() + "\t" +
                    dt["col18"].ToString().Trim() + "\t" +
                    dt["col19"].ToString().Trim();
            swriterExporta.WriteLine(linha);
            //swriterExporta.Write(linha.Trim());

            //if (i < gvDadosGeraArquivo.VisibleRowCount - 1)
            //    swriterExporta.WriteLine("");
        }
        //swriterExporta.Close();

    }


    #endregion


    protected void CallbackCarga_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        lblMostraMensagem.Text = "";
        ASPxRoundPanel1.ClientVisible = false;
        populaGridGeraArquivo(e.Parameter);

    }
    protected void gvDadosGeraArquivo_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
           if(e.GetValue("col06").ToString() == null || e.GetValue("col06").ToString() == "")
            {
                e.Row.BackColor = Color.FromName("#FFFF00");
                e.Row.ForeColor = Color.Black;
            }
        }
    }
}
