/*
 09/12//2010: Mudança by Alejandro: 
            Foi implementado o filtro de mapa, so da entidade logada.
            private void carregaComboMapas()
            
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
using System.IO;
using DevExpress.Web;
using System.Web.Hosting;
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.XtraReports.Web.Localization;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Drawing.Imaging;
using System.Drawing;
using DevExpress.XtraReports.Web;

public partial class _Processos_Visualizacao_RelatoriosURL_Estrategia_url_relGestaoEstrategia : System.Web.UI.Page
{
    dados cDados;
    private string montaNomeArquivo = "";
    private string montaNomeImagemParametro;
    private string dataImpressao = "";
    public string alturaDivGrid = "";
    public string larguraDivGrid = "";
    private int codUnidade = -1;
    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoUsuarioResponsavel = 0;
    private bool menu = false;

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

        codUnidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());


        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_EstRelGesEst");
        }

        if (Request.QueryString["M"] != null && Request.QueryString["M"].ToString() + "" != "")
            menu = Request.QueryString["M"].ToString() == "S" ? true : false;
        else
            menu = false;
        DefineFormatosExportacao();
    }

    private void DefineFormatosExportacao()
    {
        var ds = cDados.getParametrosSistema("exportaRelatorioFormatoPPT", "exportaRelatorioFormatoDOC");
        var exportaRelatorioFormatoPPT = (ds.Tables[0].Rows[0]["exportaRelatorioFormatoPPT"] as string) == "S";
        var exportaRelatorioFormatoDOC = (ds.Tables[0].Rows[0]["exportaRelatorioFormatoDOC"] as string) == "S";
        var item = (ReportToolbarComboBox)ReportToolbar2.Items.Find(i => i.ItemKind == ReportToolbarItemKind.SaveFormat);
        if (!(exportaRelatorioFormatoPPT && exportaRelatorioFormatoDOC))
        {
            for (int i = item.Elements.Count - 1; i >= 0; i--)
            {
                var element = item.Elements[i];
                string valor = element.Value ?? string.Empty;
                if (!exportaRelatorioFormatoPPT && valor.ToLower().StartsWith("ppt") ||
                    !exportaRelatorioFormatoDOC && valor.ToLower().StartsWith("doc"))
                {
                    item.Elements.Remove(element);
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            carregaComboMapas();
        }

        carregaComboMapas();
        defineAlturaTela();
        cDados.aplicaEstiloVisual(Page);
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
        }
    }

    #region VARIOS

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaDivGrid = (alturaPrincipal - 195) + "px";
        //larguraDivGrid = (larguraPrincipal - 195) + "px";

        larguraPrincipal = larguraPrincipal - (int)ReportViewer1.Width.Value;
        larguraPrincipal = (larguraPrincipal / 2);

        ReportViewer1.Height = new Unit((alturaPrincipal - 250) + "px");
        //tdRelatorio.Attributes.Add("width", larguraPrincipal.ToString());
    }

    public class ASPxReportsResLocalizer : DevExpress.XtraReports.Web.Localization.ASPxReportsResLocalizer
    {
        public static void Activate()
        {
            ASPxReportsResLocalizer localizer = new ASPxReportsResLocalizer();
            DefaultActiveLocalizerProvider<ASPxReportsStringId> provider = new DefaultActiveLocalizerProvider<ASPxReportsStringId>(localizer);
            ASPxReportsResLocalizer.SetActiveLocalizerProvider(provider);
        }

        public override string GetLocalizedString(ASPxReportsStringId id)
        {
            switch (id)
            {
                case ASPxReportsStringId.SearchDialog_Cancel: return "Cancelar";
                case ASPxReportsStringId.SearchDialog_FindNext: return "Próximo";
                case ASPxReportsStringId.SearchDialog_Header: return "Buscar";
                case ASPxReportsStringId.SearchDialog_Up: return "Para cima";
                case ASPxReportsStringId.SearchDialog_Down: return "Para baixo";
                case ASPxReportsStringId.SearchDialog_WholeWord: return "Palavra inteira";
                case ASPxReportsStringId.SearchDialog_FindWhat: return "Palavra";
                case ASPxReportsStringId.SearchDialog_Case: return "Maiúsculas";
                case ASPxReportsStringId.SearchDialog_Finished: return "A busca retornou sem resultados";
                case ASPxReportsStringId.ToolBarItemText_FirstPage: return "Primeira página";
                case ASPxReportsStringId.ToolBarItemText_LastPage: return "Última página";
                case ASPxReportsStringId.ToolBarItemText_NextPage: return "Próxima página";
                case ASPxReportsStringId.ToolBarItemText_OfLabel: return "de";
                case ASPxReportsStringId.ToolBarItemText_PreviousPage: return "Página anterior";
                case ASPxReportsStringId.ToolBarItemText_PrintPage: return "Imprimir a página atual";
                case ASPxReportsStringId.ToolBarItemText_PrintReport: return "Imprimir o relatório";
                case ASPxReportsStringId.ToolBarItemText_Search: return "Mostra a janela de busca";
                case ASPxReportsStringId.ToolBarItemText_SaveToWindow: return "Exportar o relatório e mostrar em uma nova janela";
                case ASPxReportsStringId.ToolBarItemText_SaveToDisk: return "Exportar o relatório e salvar no disco";

                default: return base.GetLocalizedString(id);
            }
        }
    }

    #endregion

    #region BANCO DADOS

    private void carregaComboMapas()
    {
        string where = " AND un.CodigoEntidade = " + codigoEntidadeUsuarioResponsavel.ToString();
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidadeUsuarioResponsavel.ToString(), codigoUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsMapas) && cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();

            //Testar O mapa padrão do usuario...
            DataSet ds = cDados.getMapaDefaultUsuario(codigoUsuarioResponsavel, "");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                int codMapa = int.Parse(ds.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"].ToString());
                string nomeMapa = ds.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString();

                int codigoMapaPadraoBanco = 0;
                cDados.getAcessoPadraoMapaEstrategico(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codMapa, ref codigoMapaPadraoBanco);

                if (codMapa == codigoMapaPadraoBanco)
                {
                    //ddlMapa.Value = codigoMapaPadraoBanco.ToString();
                    ListEditItem li = ddlMapa.Items.FindByValue(codigoMapaPadraoBanco.ToString());
                    if (li != null)
                    {
                        ddlMapa.SelectedIndex = li.Index;
                        carregarReportMapaEstrategico(int.Parse(li.Value.ToString()), li.Text.ToString());
                    }
                    else
                    {
                        if (ddlMapa.Items.Count > 0)
                        {
                            if (ddlMapa.SelectedIndex == -1)
                            {
                                ddlMapa.SelectedIndex = 0;
                                carregarReportMapaEstrategico(int.Parse(ddlMapa.SelectedItem.Value.ToString()), ddlMapa.SelectedItem.Text.ToString());
                            }
                            else
                            {
                                carregarReportMapaEstrategico(int.Parse(ddlMapa.SelectedItem.Value.ToString()), ddlMapa.SelectedItem.Text.ToString());
                            }
                        }
                    }

                    hfGeral.Set("CodigoMapaSelecionado", codMapa);
                    hfGeral.Set("NomeMapaSelecionado", nomeMapa);
                }
                else
                {
                    if (ddlMapa.Items.Count > 0)
                    {
                        if (ddlMapa.SelectedIndex == -1)
                        {
                            ddlMapa.SelectedIndex = 0;
                            carregarReportMapaEstrategico(int.Parse(ddlMapa.SelectedItem.Value.ToString()), ddlMapa.SelectedItem.Text.ToString());
                        }
                        else
                        {
                            carregarReportMapaEstrategico(int.Parse(ddlMapa.SelectedItem.Value.ToString()), ddlMapa.SelectedItem.Text.ToString());
                        }
                    }
                }
            }
            else
            {
                if (ddlMapa.Items.Count > 0)
                {
                    if (ddlMapa.SelectedIndex == -1)
                    {
                        ddlMapa.SelectedIndex = 0;
                        carregarReportMapaEstrategico(int.Parse(ddlMapa.SelectedItem.Value.ToString()), ddlMapa.SelectedItem.Text.ToString());
                    }
                    else
                    {
                        carregarReportMapaEstrategico(int.Parse(ddlMapa.SelectedItem.Value.ToString()), ddlMapa.SelectedItem.Text.ToString());
                    }
                }

            }
        }
    }

    private void carregarReportMapaEstrategico(int codigoMapaEstrategico, string nomeMapaEstrategico)
    {
        int codMapa = codigoMapaEstrategico; // -1;
        string nomeMapa = nomeMapaEstrategico; // "";

        //Verificar o mapa estratégico que posee como padrão o usuario.
        if (hfGeral.Contains("CodigoMapaSelecionado"))
        {
            codMapa = int.Parse(hfGeral.Get("CodigoMapaSelecionado").ToString());
            nomeMapa = hfGeral.Get("NomeMapaSelecionado").ToString();
            //codMapa = int.Parse(ddlMapa.SelectedItem.Value.ToString());
            //nomeMapa = ddlMapa.SelectedItem.Text;
        }
        else
        {
            DataSet ds = cDados.getMapaDefaultUsuario(codigoUsuarioResponsavel, "");
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codMapa = int.Parse(ds.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"].ToString());
                nomeMapa = ds.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString();

                int codigoMapaPadraoBanco = 0;
                cDados.getAcessoPadraoMapaEstrategico(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codMapa, ref codigoMapaPadraoBanco);

                if (codMapa != codigoMapaPadraoBanco)
                {
                    //ddlMapa.SelectedIndex = 0;
                    codMapa = -1;
                    nomeMapa = "";
                }

                hfGeral.Set("CodigoMapaSelecionado", codMapa);
                hfGeral.Set("NomeMapaSelecionado", nomeMapa);
            }
        }

        DataSet dsLogoUnidade = cDados.getLogoEntidade(codUnidade, "");
        ASPxBinaryImage image1 = new ASPxBinaryImage();
        relGestaoEstrategia rel = new relGestaoEstrategia(codMapa, codUnidade);
        rel.Parameters["pCodigoUnidade"].Value = codUnidade;

        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];

                if (image1.ContentBytes != null)
                {
                    string pathArquivo = "logoRelatorio_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                    ReportViewer1.Report = null;
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + pathArquivo;
                    montaNomeImagemParametro = @"~\ArquivosTemporarios\" + pathArquivo;
                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                string mensage = ex.Message;
            }
        }

        if (menu == true)
        {
            ReportViewer1.Report = rel;
        }
        else
        {
            ReportViewer1.Report = rel;
            ReportViewer1.WritePdfTo(Response);
        }

        rel.Parameters["pCodigoEntidade"].Value = codigoEntidadeUsuarioResponsavel;
        rel.Parameters["pNomeMapa"].Value = nomeMapa;
        rel.Parameters["pLogoUnidade"].Value = montaNomeImagemParametro;

        if (!hfGeral.Contains("dataImpressao"))
        {
            DataSet dsdata = cDados.getDataSet("select getdate()");
            dataImpressao = dsdata.Tables[0].Rows[0][0].ToString();
            DateTime dt = DateTime.Parse(dataImpressao);
            dataImpressao = "Impresso em: " + dt.ToString("dd/MM/yyyy hh:mm:ss");
            rel.Parameters["pdataImpressao"].Value = dataImpressao;
            hfGeral.Set("dataImpressao", dataImpressao);
        }
        else
        {
            rel.Parameters["pdataImpressao"].Value = hfGeral.Get("dataImpressao").ToString();
        }

        ASPxReportsResLocalizer.Activate();
    }

    #endregion

    #region CALLBACK'S

    protected void pnRelatorio_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parametro = e.Parameter.Split('|');

        int codigoMapa = int.Parse(parametro[0].ToString());
        string nomeMapa = parametro[1].ToString();

        carregarReportMapaEstrategico(codigoMapa, nomeMapa);
    }

    #endregion

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        /*switch (e.Parameter)
        {
            case "ppt":
                break;
            case "pptx":
                break;
            default:
                break;
        }*/
        string fileName;
        string parametro = e.Parameter;
        if (parametro.ToLower().StartsWith("ppt"))
            fileName = ExportToPowerPoint(parametro);
        else if (parametro.ToLower().StartsWith("doc"))
            fileName = ExportToMSWord(parametro);
        else
            return;

        if (File.Exists(fileName))
            e.Result = VirtualPathUtility.MakeRelative(Request.RawUrl, string.Format("~/DownloadArquivo.aspx?arq={0}", fileName));
    }

    private string ExportToPowerPoint(string extensao)
    {
        XtraReport _report = ReportViewer1.Report;
        _report.CreateDocument();
        // To the desktop (for demo purposes)
        var path = Server.MapPath("~/ArquivosTemporarios");
        // image export options
        ImageExportOptions exportOptions = new ImageExportOptions
        {
            ExportMode = ImageExportMode.SingleFilePageByPage,
            Format = ImageFormat.Png,
            PageBorderColor = Color.White,
            Resolution = 150
        };

        // PowerPoint Presentation
        Presentation p = new Presentation();

        // go through each page
        for (int i = 0; i < _report.Pages.Count; i++)
        {
            // export image
            var file = string.Format(@"{0}\{1}.png", path, i);
            exportOptions.PageRange = (i + 1).ToString();
            _report.ExportToImage(file, exportOptions);
            // add the image to the presentation
            p.AddPage(file);
            // clean up!
            File.Delete(file);
        }

        // save presentation to desktop
        var fileName = string.Format(@"{0}\Relatorio.{1}", path, extensao);
        p.SaveAs(fileName);

        return fileName;
    }

    string ExportToMSWord(string extensao)
    {
        XtraReport _report = ReportViewer1.Report;
        var path = Server.MapPath("~/ArquivosTemporarios");
        var fileName = string.Format(@"{0}\Relatorio.{1}", path, extensao);
        _report.ExportToRtf(fileName);

        return fileName;
    }

    //protected void ddlMapa_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    hfGeral.Set("CodigoMapaSelecionado", ddlMapa.SelectedItem.Value.ToString());
    //    hfGeral.Set("NomeMapaSelecionado", ddlMapa.SelectedItem.Text);

    //}
}
