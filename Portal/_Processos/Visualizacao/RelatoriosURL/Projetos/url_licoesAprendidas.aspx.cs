//Revisado
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
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.XtraReports.UI;

public partial class _Processos_Visualizacao_RelatoriosURL_Estrategia_url_licoesAprendidas : System.Web.UI.Page
{
    string resolucaoCliente = "";
    dados cDados;
    int idUsuarioLogado = 0;
    int CodigoEntidade = 0;
    public bool exportaOLAPTodosFormatos = false;

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
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_PrjRelLicApr");
        }

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        cDados.aplicaEstiloVisual(Page);

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;        

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();

        ImageButton1.Attributes.Add("title", "Gerar Relatório em PDF");
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
        }
    }

    private string getChavePrimaria()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private ListDictionary getDadosFormulario()
    {

        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DescricaoRiscoPadrao", dteData.Text);
        oDadosFormulario.Add("CodigoUsuarioInclusao", idUsuarioLogado);
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        oDadosFormulario.Add("UsuarioInclusao", txtIncluidaPor.Text);
        oDadosFormulario.Add("NomeProjeto", txtProjeto.Text);
        oDadosFormulario.Add("TipoLicao", txtTipo.Text);
        oDadosFormulario.Add("AssuntoLicao", txtAssunto.Text);
        return oDadosFormulario;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 140;
    }

    private void HeaderOnTela()
    {

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../../../scripts/url_licoesAprendidas.js""></script>"));
        this.TH(this.TS("barraNavegacao", "url_licoesAprendidas"));
    }

    private void populaGrid()
    {
        gvDados.DataSource = cDados.getLicoesAprendidas(CodigoEntidade, " and f.DataPublicacao is not null ");
        gvDados.DataBind();
    }
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

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    private string persisteExclusaoRegistro()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private string persisteEdicaoRegistro()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private string persisteInclusaoRegistro()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {        
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        if (e.Item.Name == "btnRelatorio")
        {

            rel_LicoesAprendidasProjRelatorios rel = new rel_LicoesAprendidasProjRelatorios(CodigoEntidade);
            rel.Parameters["pPathLogo"].Value = constroiNomeDeArquivo();
            rel.Parameters["pDataImpressao"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            ExportReport(rel, "relatorio", "pdf", false);
        }
        else
        {
            System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

            listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
            listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

            cDados = CdadosUtil.GetCdados(listaParametrosDados);
            if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
            {
                cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CatPrj");
            }
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "RelLicoesAprend", "Lições Aprendidas");
    }

    private void setaDefinicoesBotoesInserirExportar(ASPxMenu menu, bool podeIncluir, string funcaoJSbtnIncluir, bool mostraBtnIncluir, bool mostraBtnExportar, bool mostraBtnLayout, string iniciaisLayout, string tituloPagina)
    {
        #region EXPORTAÇÃO

        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");

        bool exportaOLAPTodosFormatos = false;

        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
        {
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        }

        DevExpress.Web.MenuItem btnExportar = menu.Items.FindByName("btnExportar");

        btnExportar.ClientVisible = mostraBtnExportar;

        if (!exportaOLAPTodosFormatos)
        {
            btnExportar.Items.Clear();
            btnExportar.Image.Url = "~/imagens/botoes/btnExcel.png";
            btnExportar.ToolTip = "Exportar para XLS";
        }

        #endregion

        #region INCLUIR

        DevExpress.Web.MenuItem btnIncluir = menu.Items.FindByName("btnIncluir");
        btnIncluir.ClientEnabled = podeIncluir;

        btnIncluir.ClientVisible = mostraBtnIncluir;

        if (podeIncluir == false)
            btnIncluir.Image.Url = "~/imagens/botoes/incluirRegDes.png";

        #endregion

        #region JS

        menu.ClientSideEvents.ItemClick =
        @"function(s, e){ 

            e.processOnServer = false;

            if(e.item.name == 'btnIncluir')
            {
                " + funcaoJSbtnIncluir + @"
            }
	        else if(e.item.name != 'btnLayout')
	        {
                e.processOnServer = true;		                                        
	        }	
        }";

        #endregion

        #region LAYOUT

        //DevExpress.Web.MenuItem btnLayout = menu.Items.FindByName("btnLayout");

        //btnLayout.ClientVisible = mostraBtnLayout;

        //if (mostraBtnLayout && !IsPostBack)
        //{
        //    DataSet ds = getDataSet("SELECT 1 FROM Lista WHERE CodigoEntidade = " + getInfoSistema("CodigoEntidade") + " AND IniciaisListaControladaSistema = '" + iniciaisLayout + "'");

        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        int regAf = 0;

        //        execSQL(constroiInsertLayoutColunas((menu.Parent as GridViewHeaderTemplateContainer).Grid, iniciaisLayout, tituloPagina), ref regAf);
        //    }

        //    InitData((menu.Parent as GridViewHeaderTemplateContainer).Grid, iniciaisLayout);
        //}

        #endregion
    }

    #endregion

  

    private string constroiNomeDeArquivo()
    {
        string montaNomeArquivo = "";

        int codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        byte[] vetorBytes = null;

        DataSet ds = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["LogoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["LogoUnidadeNegocio"].ToString() != "")
            {
                vetorBytes = (byte[])ds.Tables[0].Rows[0]["LogoUnidadeNegocio"];
            }
        }

        ASPxBinaryImage image1 = new ASPxBinaryImage();
        try
        {
            image1.ContentBytes = vetorBytes;

            if (image1.ContentBytes != null)
            {
                montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "/ArquivosTemporarios/" + "logo" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim(' ') + ".png";
                FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                fs.Close();
                fs.Dispose();
            }
        }
        catch
        {

        }
        return montaNomeArquivo;
    }


    public void ExportReport(XtraReport report, string fileName, string fileType, bool inline)
    {
        MemoryStream stream = new MemoryStream();

        Response.Clear();

        if (fileType == "xls")
        {
            XlsExportOptionsEx x = new XlsExportOptionsEx();
            x.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            report.ExportToXls(stream, x);
        }
            
        if (fileType == "pdf")
            report.ExportToPdf(stream);
        if (fileType == "rtf")
            report.ExportToRtf(stream);
        if (fileType == "csv")
            report.ExportToCsv(stream);

        Response.ContentType = "application/" + fileType;
        Response.AddHeader("Accept-Header", stream.Length.ToString());
        Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}",
            (inline ? "Inline" : "Attachment"), fileName, fileType));
        Response.AddHeader("Content-Length", stream.Length.ToString());
        //Response.ContentEncoding = System.Text.Encoding.Default;
        Response.BinaryWrite(stream.ToArray());
        Response.End();

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        rel_LicoesAprendidas relatorio = new rel_LicoesAprendidas();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        DataSet ds = new DataSet();
        DataRow dr = gvDados.GetDataRow(gvDados.FocusedRowIndex);

        string codigo = dr[0].ToString();
        ds = cDados.getLicoesAprendidas(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), " AND li.CodigoLicaoAprendida = " + codigo);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            relatorio.Parameters["pDataInclusao"].Value = string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["data"]);
            relatorio.Parameters["pIncluidaPor"].Value = ds.Tables[0].Rows[0]["incluidoPor"].ToString();
            relatorio.Parameters["pTipo"].Value = ds.Tables[0].Rows[0]["tipo"].ToString();
            relatorio.Parameters["pAssunto"].Value = ds.Tables[0].Rows[0]["assunto"].ToString();
            relatorio.Parameters["pNomeProjeto"].Value = ds.Tables[0].Rows[0]["projeto"].ToString();
            relatorio.Parameters["pLicao"].Value = ds.Tables[0].Rows[0]["licao"].ToString();
            relatorio.Parameters["pPathLogo"].Value = constroiNomeDeArquivo();
        }
        ExportReport(relatorio, "relatorioLicoesAprendidas", "pdf", false);
    }
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
