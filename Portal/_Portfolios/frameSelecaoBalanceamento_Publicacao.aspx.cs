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
using DevExpress.Data;
using System.IO;
using DevExpress.XtraPrinting;
using System.Drawing;

public partial class _Portfolios_frameSelecaoBalanceamento_Publicacao : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;
    int codigoUsuarioResponsavel;
    int codigoPortfolio = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        if (Request.QueryString["CodigoPortfolio"] != null && Request.QueryString["CodigoPortfolio"].ToString() != "")
            codigoPortfolio = int.Parse(Request.QueryString["CodigoPortfolio"].ToString());
        else if (cDados.getInfoSistema("CodigoPortfolio") != null)
            codigoPortfolio = int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString());

        cDados.aplicaEstiloVisual(Page);
        gvProjetos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        defineAlturaTela(); 

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "PO_Pub");
        }

        cDados.setaDefinicoesBotoesInserirExportarSemTemplate(menu0, false, "", false, true, false, "LstSelBalPub", "Seleção e Balanceamento Publicação", this, gvProjetos);

        //carregaComboAnos();
        carregaGrid();
        DataSet dsParametro = cDados.getParametrosSistema("labelDespesa");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            gvProjetos.Columns["Custo"].Caption = string.Format("{0} (R$)", dsParametro.Tables[0].Rows[0]["labelDespesa"].ToString());
        }

        if (int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString()) == -1)
        {
            btnPublicar.ClientEnabled = false;
            btnSelecionar.ClientEnabled = false;
        }

        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);

        tbBotoes.Style.Add("padding", "3px");

        tbBotoes.Style.Add("border-collapse", "collapse");

        tbBotoes.Style.Add("border-bottom", "none");
    }

    private void carregaComboAnos()
    {
        string where = " AND IndicaAnoPeriodoEditavel = 'S'";
        DataSet dsAnos = cDados.getPeriodoAnalisePortfolio(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsAnos))
        {
            ddlAno.DataSource = dsAnos;

            ddlAno.TextField = "Ano";

            ddlAno.ValueField = "Ano";

            ddlAno.DataBind();

            ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

            ddlAno.Items.Insert(0, lei);

            if (!IsPostBack)
                ddlAno.SelectedIndex = 0;
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        gvProjetos.Settings.VerticalScrollableHeight = (alturaPrincipal - 485);
    }

    private void carregaGrid()
    {
        gvProjetos.TotalSummary.Clear();

        string where = " AND IndicaCenario" + ddlCenario.Value + " = 'S'";

        DataSet dsGrid = cDados.getProjetosPorCriterio(-1, codigoPortfolio, codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(dsGrid))
        {
            gvProjetos.DataSource = dsGrid;

            gvProjetos.DataBind();

            geraTotalColuna("Custo", SummaryItemType.Sum, "{0:n2}");
            geraTotalColuna("Receita", SummaryItemType.Sum, "{0:n2}");
            geraTotalColuna("Duração", SummaryItemType.Max, "{0:n2}");
            geraTotalColuna("ScoreRiscos", SummaryItemType.Average, "{0:n2}");
            geraTotalColuna("ScoreCriterios", SummaryItemType.Average, "{0:n2}");
            geraTotalColuna("RH", SummaryItemType.Sum, "{0:n2}");
        }       
        
    }

    private void geraTotalColuna(string coluna, SummaryItemType tipo, string formato)
    {
        ASPxSummaryItem totalSummary = new ASPxSummaryItem();
        totalSummary.FieldName = coluna;
        totalSummary.ShowInColumn = coluna;
        totalSummary.SummaryType = tipo;
        totalSummary.DisplayFormat = formato;
        gvProjetos.TotalSummary.Add(totalSummary);
    }

    protected void gvProjetos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGrid();
    }

    protected void gvProjetos_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGrid();
    }

    protected void btnPublicar_Click(object sender, EventArgs e)
    {
        if (int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString()) != -1)
        {
            int regAf = 0;
            cDados.publicaPortfolio(int.Parse(cDados.getInfoSistema("CodigoPortfolio").ToString()), codigoEntidadeUsuarioResponsavel, int.Parse(ddlCenario.Value.ToString()), ref regAf);
            cDados.alerta(this, "Portfólio publicado com sucesso !!!", "sucesso");
            carregaGrid();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;

            string nomeArquivo = "", app = "", erro = "";

            try
            {

                nomeArquivo = "PublicacaoPortfolio_" + dataHora + ".xls";
                XlsExportOptionsEx x = new XlsExportOptionsEx();
                 x.TextExportMode = TextExportMode.Value;
                 ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                app = "application/ms-excel";

            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
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
            else
            {
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                  
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenuSemTemplate((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstSelBal", gvProjetos);
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {

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
        if (e.Column.Name == "DesempenhoProjeto" && e.Value != null)
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
