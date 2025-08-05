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
using System.Web.Hosting;
using System.IO;
using DevExpress.XtraReports.Web.Localization;
using DevExpress.Utils.Localization.Internal;

public partial class _Estrategias_Relatorios_AnalisesDePerformance : System.Web.UI.Page
{
    dados cDados;
    string montaNomeArquivo = "";
    string montaNomeImagemParametro;
    string dataImpressao = "";
    public string alturaDivGrid = "";
    public string larguraDivGrid = "";
    public int codigoEntidadeUsuarioResponsavel = 0;
    public int codigoUsuarioResponsavel = 0;
    int codUnidade = -1;


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
        this.Title = cDados.getNomeSistema();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsCallback)
            {

                dteDe.Date = DateTime.Now.AddMonths(-3);
                dteAte.Date = DateTime.Now.AddMonths(1);

                //dteDe.Value = dteDe.Date.AddMonths(-3).ToString("dd/MM/yyyy");
                //dteAte.Value = dteAte.Date.AddMonths(1).ToString("dd/MM/yyyy");
            }
            if (!hfGeral.Contains("tipoAnalise"))
            {
                hfGeral.Set("tipoAnalise", "Indicador");
            }
        }

        carregaComboUnidades();
        carregaComboMapas();
        carregarReportMapaEstrategico(-1, "");
        defineAlturaTela();
        ASPxReportsResLocalizer.Activate();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }


    private void carregarReportMapaEstrategico(int codigoMapaEstrategico, string nomeMapaEstrategico)
    {
        int codMapa = codigoMapaEstrategico; // -1;
        string nomeMapa = nomeMapaEstrategico; // "";
        int? codUnidadeSelecionada = codUnidade;

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



        codUnidadeSelecionada = int.Parse(ddlUnidade.SelectedIndex != -1 ? ddlUnidade.Value.ToString() : "-1");
        short? codTipoAssociacao = (short)cDados.getCodigoTipoAssociacao((hfGeral.Get("tipoAnalise").ToString() == "Indicador") ? "IN" : "OB");
        
        hfGeral.Set("CodigoTipoAssociacao", codTipoAssociacao);
        relAnalisePerformance rel = new relAnalisePerformance(codUnidadeSelecionada, codTipoAssociacao, codMapa, dteDe.Date.ToString("dd/MM/yyyy"), dteAte.Date.ToString("dd/MM/yyyy"), hfGeral.Get("tipoAnalise").ToString(), codigoEntidadeUsuarioResponsavel, codMapa, rblTipoRelatorio.SelectedItem.Value.ToString());



        if (cDados.DataSetOk(dsLogoUnidade) && cDados.DataTableOk(dsLogoUnidade.Tables[0]))
        {
            try
            {
                image1.ContentBytes = (byte[])dsLogoUnidade.Tables[0].Rows[0]["LogoUnidadeNegocio"];

                if (image1.ContentBytes != null)
                {
                    string pathArquivo = "logoRelAnalisePerform_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                    ReportViewer1.Report = null;
                    montaNomeArquivo = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + pathArquivo;
                    montaNomeImagemParametro = @"~\ArquivosTemporarios\" + pathArquivo;
                    FileStream fs = new FileStream(montaNomeArquivo, FileMode.CreateNew);
                    fs.Write(image1.ContentBytes, 0, image1.ContentBytes.Length);
                    fs.Close();
                    fs.Dispose();
                    rel.Parameters["pathArquivo"].Value = montaNomeImagemParametro;
                }
            }
            catch (Exception ex)
            {
                string mensage = ex.Message;
            }
        }

        ReportViewer1.Report = rel;


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

        //ASPxReportsResLocalizer.Activate();
    }
    

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = 0;
        int larguraPrincipal = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out larguraPrincipal, out alturaPrincipal);
        ReportViewer1.Height = new Unit((alturaPrincipal - 290) + "px");
    }

    protected void pnRelatorio_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parametro = e.Parameter.Split('|');

        int codigoMapa = int.Parse(parametro[0].ToString());
        string nomeMapa = parametro[1].ToString();

        carregarReportMapaEstrategico(codigoMapa, nomeMapa);
    }

    private void carregaComboUnidades()
    {
        DataSet ds = cDados.getUnidadeSuperiorAnalises(codigoEntidadeUsuarioResponsavel);

        if (cDados.DataSetOk(ds))
        {
            ddlUnidade.TextField = "SiglaUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";
            ddlUnidade.TextFormatString = "{0}";

            DataRow dr = ds.Tables[0].NewRow();

            dr["SiglaUnidadeNegocio"] = "Todas";
            dr["CodigoUnidadeNegocio"] = "-1";
            dr["NomeUnidadeNegocio"] = "Todas";

            ds.Tables[0].Rows.InsertAt(dr, 0);
            
            ddlUnidade.DataSource = ds.Tables[0];

            ddlUnidade.Columns[0].FieldName = "NomeUnidadeNegocio";
            ddlUnidade.Columns[1].FieldName = "SiglaUnidadeNegocio";
           
            ddlUnidade.DataBind();
            //ddlUnidade.Items.Insert(0, new ListEditItem(Resources.traducao.todos, -1));
        }


        if(!IsPostBack)
            ddlUnidade.SelectedIndex = 0;        
    }

    private void carregaComboMapas()
    {
        DataSet dsMapas = cDados.getMapasUsuarioEntidade(codigoEntidadeUsuarioResponsavel.ToString(), codigoUsuarioResponsavel, "");

        if (cDados.DataSetOk(dsMapas))
        {
            ddlMapa.DataSource = dsMapas;
            ddlMapa.TextField = "TituloMapaEstrategico";
            ddlMapa.ValueField = "CodigoMapaEstrategico";
            ddlMapa.DataBind();
        }

        if (!IsPostBack && ddlMapa.Items.Count > 0)
        {
            DataSet dsParametro = cDados.getMapaDefaultUsuario(codigoUsuarioResponsavel, "");

            if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                string codigoMapa = dsParametro.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"] + "";

                if (codigoMapa != "" && ddlMapa.Items.FindByValue(codigoMapa) != null)
                    cDados.setInfoSistema("CodigoMapa", codigoMapa);
                else
                    ddlMapa.SelectedIndex = 0;
            }
            else
            {
                ddlMapa.SelectedIndex = 0;
            }
        }
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

}
