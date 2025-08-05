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

public partial class _Estrategias_Relatorios_relGestaoEstrategia2 : System.Web.UI.Page
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

        if (Request.QueryString["M"] != null && Request.QueryString["M"].ToString() + "" != "")
            menu = Request.QueryString["M"].ToString() == "S" ? true : false;
        else
            menu = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            carregaComboMapas();
        }

        defineAlturaTela();
        carregarReportMapaEstrategico(-1, "");

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        this.Title = cDados.getNomeSistema();
    }

    //nao esquecer de colocar essas funcoes no cdados quando estiver pronto

    #region VARIOS

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaDivGrid = (alturaPrincipal - 215) + "px";
        //larguraDivGrid = (larguraPrincipal - 195) + "px";
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

    private void carregaComboMapas()
    {
        //DataSet dsMapas = cDados.getMapasUsuarioEntidade(cDados.getInfoSistema("CodigoEntidade").ToString(), codigoUsuarioResponsavel, "");
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
                        ddlMapa.SelectedIndex = li.Index;

                    hfGeral.Set("CodigoMapaSelecionado", codMapa);
                    hfGeral.Set("NomeMapaSelecionado", nomeMapa);
                }
                else
                {
                    ddlMapa.Items.Insert(0, new ListEditItem("", -1));
                    ddlMapa.SelectedIndex = 0;
                    //ddlMapa.SelectedIndex = 0;
                }
            }

            ////ddlMapa.Value = hfGeral.Contains("CodigoMapaSelecionado") ? int.Parse(hfGeral.Get("CodigoMapaSelecionado").ToString()) : -1;
            //if (hfGeral.Contains("CodigoMapaSelecionado"))
            //{
            //    ListEditItem li = ddlMapa.Items.FindByValue((hfGeral.Get("CodigoMapaSelecionado").ToString()));
            //    if (li != null)
            //    {
            //        ddlMapa.SelectedIndex = li.Index;
            //    }
            //}
            //else
            //{
            //    ddlMapa.Items.Insert(0, new ListEditItem("Nenhum Mapa Selecionado", -1));
            //    ddlMapa.SelectedIndex = 0;
            //}
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
        relGestaoEstrategia2 rel = new relGestaoEstrategia2(codMapa, codUnidade);

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

    //protected void ddlMapa_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    hfGeral.Set("CodigoMapaSelecionado", ddlMapa.SelectedItem.Value.ToString());
    //    hfGeral.Set("NomeMapaSelecionado", ddlMapa.SelectedItem.Text);

    //}
}
