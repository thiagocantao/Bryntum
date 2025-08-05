/*
 09/12/2010: Mudança by Alejandro: 
            Foi implementado o filtro de mapa, so da entidade logada.
            private void populaMapaEstrategico()
            
 */
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.Web;
using DevExpress.XtraReports.Web.Localization;
using DevExpress.Utils.Localization.Internal;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Drawing.Imaging;
using System.Drawing;
using DevExpress.XtraReports.Web;

public partial class _Processos_Visualizacao_RelatoriosURL_Estrategia_url_identidadeOrganizacional : System.Web.UI.Page
{
    dados cDados;
    public string nomeMapa = "";
    private int codUnidade = -1;
    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codMapa = -1;
    public int idUsuarioLogado = 0;

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
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_EstRelPlnEst");
        }

        this.Title = cDados.getNomeSistema();
        DefineFormatosExportacao();
    }

    private void DefineFormatosExportacao()
    {
        var ds = cDados.getParametrosSistema("exportaRelatorioFormatoPPT", "exportaRelatorioFormatoDOC");
        var exportaRelatorioFormatoPPT = (ds.Tables[0].Rows[0]["exportaRelatorioFormatoPPT"] as string) == "S";
        var exportaRelatorioFormatoDOC = (ds.Tables[0].Rows[0]["exportaRelatorioFormatoDOC"] as string) == "S";
        var item = (ReportToolbarComboBox)ReportToolbar1.Items.Find(i => i.ItemKind == ReportToolbarItemKind.SaveFormat);
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

        populaMapaEstrategico();

        carregarReportMapaEstrategico();
        defineAlturaTela();
        cDados.aplicaEstiloVisual(Page);
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
        }
    }

    //nao esquecer de colocar essas funcoes no cdados quando estiver pronto

    #region VARIOS

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        ReportViewer1.Height = new Unit((alturaPrincipal - 260) + "px");

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
                case ASPxReportsStringId.SearchDialog_Up: return "Para Cima";
                case ASPxReportsStringId.SearchDialog_Down: return "Para Baixo";
                case ASPxReportsStringId.SearchDialog_WholeWord: return "Palavra Inteira";
                case ASPxReportsStringId.SearchDialog_FindWhat: return "Palavra";
                case ASPxReportsStringId.SearchDialog_Case: return "Maiúsculas";
                case ASPxReportsStringId.SearchDialog_Finished: return "A Busca Retornou Sem Resultados";
                case ASPxReportsStringId.ToolBarItemText_FirstPage: return "Primeira Página";
                case ASPxReportsStringId.ToolBarItemText_LastPage: return "Última Página";
                case ASPxReportsStringId.ToolBarItemText_NextPage: return "Próxima Página";
                case ASPxReportsStringId.ToolBarItemText_OfLabel: return "de";
                case ASPxReportsStringId.ToolBarItemText_PreviousPage: return "Página Anterior";
                case ASPxReportsStringId.ToolBarItemText_PrintPage: return "Imprimir a Página Atual";
                case ASPxReportsStringId.ToolBarItemText_PrintReport: return "Imprimir o Relatório";
                case ASPxReportsStringId.ToolBarItemText_Search: return "Mostra a Janela de Busca";
                case ASPxReportsStringId.ToolBarItemText_SaveToWindow: return "Exportar o Relatório e Mostrar em Uma Nova Janela";

                default: return base.GetLocalizedString(id);
            }
        }
    }

    #endregion

    #region BANCO DADOS

    private void carregarReportMapaEstrategico()
    {
        if (ddlMapa.SelectedIndex != -1)
        {
            codMapa = int.Parse(ddlMapa.SelectedItem.Value.ToString());
            nomeMapa = ddlMapa.SelectedItem.Text;
            Session.Add("nomeMapa", nomeMapa);
        }
        XtraReport rel = ObtemRelatorio();
        ReportViewer1.Report = rel;
        ASPxReportsResLocalizer.Activate();
    }

    private void populaMapaEstrategico()
    {
        //DataSet dsMapas = cDados.getMapasUsuarioEntidade(cDados.getInfoSistema("CodigoEntidade").ToString(), idUsuarioLogado, "");
        string where = " AND un.CodigoEntidade = " + codigoEntidadeUsuarioResponsavel.ToString();
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidadeUsuarioResponsavel.ToString(), idUsuarioLogado, where);

        if (cDados.DataSetOk(dsMapas) && cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();

            //ddlMapa.Items.Insert(0, new ListEditItem("", -1));

            if (!IsPostBack)
            {
                //todo: Testar a função de verificar mapa estrategico padrão.
                DataSet ds = cDados.getMapaDefaultUsuario(idUsuarioLogado, "");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    int codMapa = int.Parse(ds.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"].ToString());
                    string nomeMapa = ds.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString();

                    int codigoMapaPadraoBanco = 0;
                    cDados.getAcessoPadraoMapaEstrategico(idUsuarioLogado, codigoEntidadeUsuarioResponsavel, codMapa, ref codigoMapaPadraoBanco);

                    if (codMapa == codigoMapaPadraoBanco)
                    {
                        //ddlMapa.Value = codigoMapaPadraoBanco.ToString();
                        ListEditItem li = ddlMapa.Items.FindByValue(codigoMapaPadraoBanco.ToString());
                        if (li != null)
                            ddlMapa.SelectedIndex = li.Index;

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
                }
            }

        }
    }

    #endregion

    #region CALLBACK'S

    protected void pnRelatorio_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parametro = e.Parameter.Split('|');

        int codigoMapa = int.Parse(parametro[0].ToString());
        string nomeMapa = parametro[1].ToString();

        carregarReportMapaEstrategico();
    }

    #endregion



    protected void callback_Callback(object source, CallbackEventArgs e)
    {

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

    private byte[] ObtemLayoutRelatorio()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
SELECT [ConteudoRelatorio]      
  FROM [dbo].[ModeloRelatorio]
where IniciaisControle = 'EN_EstRelPlnEst' ");

        #endregion

        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
        if (dt.Rows.Count == 0)
            return null;

        return dt.Rows[0]["ConteudoRelatorio"] as byte[];
    }

    private XtraReport ObtemRelatorio()
    {
        byte[] reportLayout = ObtemLayoutRelatorio();
        if (reportLayout == null || reportLayout.Length == 0)
            return new relIdentidade4();

        using (var stream = new MemoryStream(reportLayout))
        {
            //XtraReport rel = XtraReport.FromStream(stream, true);
            CdisReport rel = new CdisReport();
            rel.LoadLayout(stream);
            ((DevExpress.DataAccess.Sql.SqlDataSource)rel.DataSource).ValidateCustomSqlQuery += (sender, e) => e.Valid = true;

            DataSet dsRelatorioSindicato = cDados.getdsRelIdentidadeOrganizacional(codMapa);

            if (cDados.DataSetOk(dsRelatorioSindicato) && cDados.DataTableOk(dsRelatorioSindicato.Tables[0]))
            {
                rel.Parameters["pMissao"].Value = dsRelatorioSindicato.Tables[0].Rows[0]["missao"];
                rel.Parameters["pVisao"].Value = dsRelatorioSindicato.Tables[0].Rows[0]["visao"];
                rel.Parameters["pCrencasValores"].Value = dsRelatorioSindicato.Tables[0].Rows[0]["crencasvalores"];
            }
            rel.Parameters["pNomeMapa"].Value = nomeMapa;
            rel.Parameters["pUrlLogo"].Value = ObtemUrlLogoEntidade();
            rel.Parameters["pCodigoMapa"].Value = codMapa;
            return rel;
        }
    }

    private string ObtemUrlLogoEntidade()
    {
        DataTable dt = cDados.getLogoEntidade(codigoEntidadeUsuarioResponsavel, "").Tables[0];
        byte[] byteArrayIn = dt.Rows[0].Field<byte[]>("LogoUnidadeNegocio");
        string caminhoArquivoLogoEntidade = string.Format("~/ArquivosTemporarios/{0}",
            Path.ChangeExtension(Path.GetRandomFileName(), "jpg"));
        using (var ms = new MemoryStream(byteArrayIn))
        {
            var img = System.Drawing.Image.FromStream(ms);
            img.Save(Server.MapPath(caminhoArquivoLogoEntidade));
        }

        return caminhoArquivoLogoEntidade;
    }
}

