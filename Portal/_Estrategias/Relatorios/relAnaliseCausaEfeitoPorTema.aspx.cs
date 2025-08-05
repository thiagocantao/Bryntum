/*
 09/12//2010: Mudança by Alejandro: 
            Foi implementado o filtro de mapa, so da entidade logada.
            private void populaComboMapaEstrategico(int);
            
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
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Drawing;

public partial class _Estrategias_Relatorios_relAnaliseCausaEfeitoPorTema : System.Web.UI.Page
{
    dados cDados;
    private bool usarDetalheObjetigoAntigo = false; // variável a ser eliminada após terminada a nova tela de detalhe de objetivos
    private bool exportaOLAPTodosFormatos = false;
    private string dbName;
    private string dbOwner;
    private int alturaPrincipal = 0;

    public string codigoUsuarioResponsavel;
    public string codigoEntidadeUsuario;
    public int alturaDivTree = 0; //para indicar o height de la div que contem a visualização do Tree.
    public bool podeExportarTree = false;

    public string estiloFooter = "dxtlFooter";

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

        codigoUsuarioResponsavel = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        codigoEntidadeUsuario = cDados.getInfoSistema("CodigoEntidade").ToString();
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        HeaderOnTela();
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());

        string cssPostfix = "", cssPath = "";

        cDados.aplicaEstiloVisual(Page);
        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter += "_" + cssPostfix;

        if (!IsCallback)
        {
            checkPodeExportar(ref podeExportarTree);
            populaComboMapaEstrategico(int.Parse(codigoEntidadeUsuario));

            if (!hfGeral.Contains("tipoArquivo"))
                hfGeral.Set("tipoArquivo", "XLS");
        }

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        populaOpcoesExportacao();

        populaTreeRelatorioCausaEfeito();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region gvDADOS

    private void populaComboMapaEstrategico(int codigoUnidadeNegocio)
    {
        string where = string.Format(@"
                        AND Mapa.IndicaMapaEstrategicoAtivo = 'S' 
                        AND un.CodigoEntidade               = {2}
                        AND {0}.{1}.f_VerificaAcessoConcedido({4}, {3}, Mapa.CodigoMapaEstrategico, NULL, 'ME', 0, NULL, 'ME_Vsl') = 1
                        ",dbName, dbOwner, codigoEntidadeUsuario.ToString(), codigoUsuarioResponsavel, codigoEntidadeUsuario);
        string comandoSQL = cDados.getSelect_MapaEstrategico(int.Parse(codigoEntidadeUsuario), int.Parse(codigoUsuarioResponsavel), where);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";

            ddlMapa.DataSource = ds.Tables[0];
            ddlMapa.DataBind();            
        }
    }

    #endregion

    #region COMBOBOX

    protected void ddlTemaEstrategico_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.ToString();
        populaComboTemaEstrategico(parametro, "TEM");
    }

    private void populaComboTemaEstrategico(string codigoMapa, string tipoObjeto)
    {
        DataSet ds = cDados.getTemaEstrategicoFromMapa(codigoMapa, tipoObjeto);

        if (cDados.DataSetOk(ds))
        {
            ddlTemaEstrategico.TextField = "DescricaoObjetoEstrategia";
            ddlTemaEstrategico.ValueField = "CodigoObjetoEstrategia";

            ddlTemaEstrategico.DataSource = ds.Tables[0];
            ddlTemaEstrategico.DataBind();

            if (!IsPostBack)
                ddlTemaEstrategico.SelectedIndex = -1;
        }
    }

    #endregion

    #region TREELIST

    private void populaTreeRelatorioCausaEfeito()
    {
        string codigoMapa = ddlMapa.SelectedIndex != -1 ? ddlMapa.Value.ToString() : "-1";
        populaComboTemaEstrategico(codigoMapa, "TEM");

        if (ddlMapa.SelectedIndex != -1 && ddlTemaEstrategico.SelectedIndex != -1)
        {
            string codigoTema = ddlTemaEstrategico.Value.ToString();

            DataSet ds = cDados.getTreeRelatorioCausaEfeito(codigoEntidadeUsuario, codigoMapa, codigoTema);

            if (cDados.DataSetOk(ds))
            {
                tlRelatorioCausaEfeito.ExpandAll();//.ExpandToLevel(3);
                tlRelatorioCausaEfeito.DataSource = ds;
                tlRelatorioCausaEfeito.DataBind();
            }
        }
        else
        {
            tlRelatorioCausaEfeito.DataSource = null;
            tlRelatorioCausaEfeito.DataBind();
        }
    }

    protected void tlRelatorioCausaEfeito_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        populaTreeRelatorioCausaEfeito();
    }

    #endregion

    #region VARIOS

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        //Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/relAnaliseCausaEfeitoPorTema.js""></script>"));
        this.TH(this.TS("relAnaliseCausaEfeitoPorTema"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        tlRelatorioCausaEfeito.Settings.ScrollableHeight = altura - 270;
    }

    private void checkPodeExportar(ref bool podeExportarTree)
    {
        DataSet ds = cDados.getParametrosSistema(int.Parse(codigoEntidadeUsuario), "exportaOLAPTodosFormatos");
        if (cDados.DataSetOk(ds))
        {
            string resultado = ds.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString();
            podeExportarTree = ("S" == resultado);
            hfGeral.Set("podeExportarTree", resultado);
        }
    }

    public string getLinkIndicadorObjetivo(string tipoObjeto, string codigo, string codigoPai, string descricao)
    {
        if (tipoObjeto == "OBJ")
        {
            if (usarDetalheObjetigoAntigo)
                return string.Format(@"<a href='detalhesObjetivoEstrategico.aspx?COE={0}' target='_parent'>{1}</a>", codigo, descricao);
            else
                return string.Format(@"<a href='objetivoestrategico/indexResumoObjetivo.aspx?COE={0}' target='_parent'>{1}</a>", codigo, descricao);
        }
        else
        {
            if (tipoObjeto == "IND")
            {
                codigo = codigo.IndexOf(".") == -1 ? codigo : codigo.Substring(0, codigo.IndexOf('.'));
                return string.Format(@"<a href='indicador/index.aspx?COIN={0}&COE={1}' target='_parent'>{2}</a>", codigo, codigoPai, descricao);
            }
            else
            {
                return descricao;
            }
        }
    }

    #endregion

    #region EXPORTAR

    /// <summary>
    /// Atualiza a imagen referente a exportação do dados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";

        //if (e.Parameter == "HTML") //export_to_html.png
        //    nomeArquivo = "~/imagens/menuExportacao/iconoHtml.jpg";

        if (e.Parameter == "PDF")//export_to_pdf.png
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (e.Parameter == "XLS")//excel.PNG
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (e.Parameter == "RTF")//export_to_rtf.PNG
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        //if (e.Parameter == "CSV")//export_to_cvs.PNG
        //    nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
        btnExportar.Image.Url = nomeArquivo;
    }

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();
        ddlExporta.Items.Add(new ListEditItem("XLS", "XLS"));
        ddlExporta.ClientEnabled = false;
        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;
            //ddlExporta.Items.Add(new ListEditItem("XLS", "XLS"));
            ddlExporta.Items.Add(new ListEditItem("PDF", "PDF"));
            //ddlExporta.Items.Add(new ListEditItem("HTML", "HTML"));
            ddlExporta.Items.Add(new ListEditItem("RTF", "RTF"));
            //ddlExporta.Items.Add(new ListEditItem("CSV", "CSV"));
        }
        ddlExporta.SelectedIndex = 0;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "HTML")
                {
                    //string caminhoArquivo = HostingEnvironment.ApplicationPhysicalPath + "\\ArquivosTemporarios";
                    //nomeArquivo = "analiseIndicador_" + dataHora + ".html";
                    //nomeArquivo = caminhoArquivo + "\\" + nomeArquivo;
                    //HtmlExportOptions h = new HtmlExportOptions();
                    //h.ExportMode = HtmlExportMode.SingleFile;
                    //pvgCausaEfeito.w.ExportToHtml(stream, h);
                    //pvgCausaEfeito.
                    //pvgCausaEfeito.ExportToHtml(nomeArquivo, h);
                    //StartProcess(nomeArquivo);
                    //app = "text/html";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "PDF")
                {
                    nomeArquivo = "analiseDados_" + dataHora + ".pdf";
                    PdfExportOptions p = new PdfExportOptions();
                    p.DocumentOptions.Author = "CDIS Informática";
                    pvgCausaEfeito.WritePdf(stream, p);
                    app = "application/pdf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = "analiseDados_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    x.TextExportMode = TextExportMode.Value;
                    pvgCausaEfeito.WriteXls(stream, x);

                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "RTF")
                {
                    nomeArquivo = "analiseDados_" + dataHora + ".rtf";
                    pvgCausaEfeito.WriteRtf(stream);
                    app = "application/rtf";
                }
                if (hfGeral.Get("tipoArquivo").ToString() == "CSV")
                {
                    //nomeArquivo = "analiseDados_" + dataHora + ".csv";
                    //pvgCausaEfeito.ExportToText(stream, ";");
                    //app = "text/html";
                }
            }
            catch
            {
                erro = "S";
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
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                 
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }

    public void StartProcess(string path)
    {
        Process process = new Process();
        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();
        }
        catch { }
    }

    #endregion

    #region TREELIST EXPORT

    protected void pvgCausaEfeito_RenderBrick(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.Column.FieldName == "Status")
        {
            if (e.Text != "Status")
            {
                if (e.TextValue.ToString().Trim() == "Vermelho")
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Red;
                }
                else
                {
                    if (e.TextValue.ToString().Trim() == "Amarelo")
                    {
                        e.Text = "l";
                        e.TextValue = "l";
                        e.BrickStyle.ForeColor = Color.Yellow;
                    }
                    else if (e.TextValue.ToString().Trim() == "Verde")
                    {
                        e.Text = "l";
                        e.TextValue = "l";
                        e.BrickStyle.ForeColor = Color.Green;
                    }
                    else if (e.TextValue.ToString().Equals("Azul"))
                    {
                        e.Text = "l";
                        e.TextValue = "l";
                        e.BrickStyle.ForeColor = Color.Blue;
                    }
                    else if (e.TextValue.ToString().Equals("Branco"))
                    {
                        e.Text = "l";
                        e.TextValue = "l";
                        e.BrickStyle.ForeColor = Color.WhiteSmoke;
                    }
                    else if (e.TextValue.ToString().Equals("Laranja"))
                    {
                        e.Text = "l";
                        e.TextValue = "l";
                        e.BrickStyle.ForeColor = Color.Orange;
                    }
                    else
                    {
                        e.Text = " ";
                        e.TextValue = " ";
                    }
                }

                Font fonte = new Font("Wingdings", 18, FontStyle.Bold);
                e.BrickStyle.Font = fonte;
            }
            else
            {
                Font fonte = new Font("Verdana", 9);

                e.BrickStyle.Font = fonte;
            }

            e.Column.Width = 80;
            e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
        }
        else
        {
            e.Column.Width = new Unit(100, UnitType.Percentage);
            //e.Column.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            Font fonte = new Font("Verdana", 9);
            e.BrickStyle.Font = fonte;
        }
    }

    #endregion
}
