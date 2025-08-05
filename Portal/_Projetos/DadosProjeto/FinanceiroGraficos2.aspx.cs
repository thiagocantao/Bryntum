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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.IO;
using DevExpress.XtraPivotGrid;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_RecursosHumanos : System.Web.UI.Page
{
    dados cDados;

    int idProjeto = 0;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string grafico1_titulo = "Financeiro";
    public string grafico1_swf = "../../Flashs/ScrollColumn2D.swf";
    public string grafico1_xml = "";
    public string grafico1_xmlzoom = "";

    public int alturaGrafico = 220;
    public int larguraGrafico = 350;
    string mostraGrupo = "S";

    int codigoUsuario;
    int codigoEntidade;

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

        if (Request.QueryString["MostraGrupo"] != null && Request.QueryString["MostraGrupo"].ToString() != "")
            mostraGrupo = Request.QueryString["MostraGrupo"].ToString();

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        
        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            ckTotal.Checked = true;

            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_CnsFin");

            cDados.aplicaEstiloVisual(Page);

            string dataMin = getDataMinimaPrevisoesOrcamentarias(idProjeto);

            if (dataMin != "")
                txtDe.Text = dataMin;

            txtAte.Text = string.Format("{0:MM/yyyy}", DateTime.Now);
        }

        carregaComboPrevisoes();
        carregaComboDiretorias();
        carregaComboAreas();
        carregaComboCentrosCustos();

        geraGraficoPizza();
               
        defineAlturaTela();

        imgGraficos.ClientSideEvents.Click = "function(s, e) {window.location.href = 'Financeiro.aspx?idProjeto=" + idProjeto + "&MostraGrupo=" + mostraGrupo + "&VersaoGrafico=2';}";
        imgGraficos.Style.Add("cursor", "pointer");
        lblGrafico.ClientSideEvents.Click = "function(s, e) {window.location.href = 'Financeiro.aspx?idProjeto=" + idProjeto + "&MostraGrupo=" + mostraGrupo + "&VersaoGrafico=2';}";
        lblGrafico.Style.Add("cursor", "pointer");
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0,resolucaoCliente.IndexOf('x')));

        pRH.ContentHeight = alturaPrincipal - 250;
        pRH.Width = new Unit("100%");

        alturaGrafico = alturaPrincipal - 260;
        larguraGrafico = larguraPrincipal - 200;
    }

    private void geraGraficoPizza()
    {
        int codigoPrevisao = ddlPrevisao.SelectedIndex != -1 ? int.Parse(ddlPrevisao.Value.ToString()) : -1;
        int codigoDiretoria = ddlDiretoria.SelectedIndex != -1 ? int.Parse(ddlDiretoria.Value.ToString()) : -1;
        int codigoArea = ddlArea.SelectedIndex != -1 ? int.Parse(ddlArea.Value.ToString()) : -1;
        int codigoCentro = ddlCentro.SelectedIndex != -1 ? int.Parse(ddlCentro.Value.ToString()) : -1;

        DataSet ds = cDados.getFinanceiroPrevistoRealProjeto(idProjeto, codigoDiretoria, codigoArea, codigoCentro, txtDe.Text, txtAte.Text, codigoPrevisao, "S", "");
        DataTable dtGrafico = new DataTable();

        if (ckTotal.Checked)
        {
            DataTable dtAux = ds.Tables[0];
            object sumPrevisto = dtAux.Compute("Sum(ValorPrevisto)", "");
            object sumReal = dtAux.Compute("Sum(ValorReal)", "");

            dtGrafico = dtAux.Clone();

            DataRow dr = dtGrafico.NewRow();

            dr["Descricao"] = "Total";
            dr["ValorPrevisto"] = sumPrevisto;
            dr["ValorReal"] = sumReal;

            dtGrafico.Rows.Add(dr);
        }
        else
        {
            dtGrafico = ds.Tables[0]; 
        }

        string xml = "";

        string nomeGrafico = @"/ArquivosTemporarios/OrcamentoColunas_" + codigoUsuario + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getFinanceiroGraficoPrevistoReal(dtGrafico, "ValorPrevisto", "ValorReal", "", 9);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico1_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        callback.JSProperties["cp_grafico"] = grafico1_xml;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\OrcamentoPizzaColunas_" + codigoUsuario + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getFinanceiroGraficoPrevistoReal(dtGrafico, "ValorPrevisto", "ValorReal", "Financeiro", 15);

        //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
        cDados.escreveXML(xml, nomeGrafico);

        grafico1_xmlzoom = nomeGrafico;

        grafico1_xmlzoom = grafico1_xmlzoom.Replace("\\", "/");
    }

    public string getDataMinimaPrevisoesOrcamentarias(int codigoProjeto)
    {
        string dataMin = "";

      string  comandoSQL = string.Format(@"
            SELECT MIN(Ano) AS Ano, MIN(Mes) AS Mes
              FROM {0}.{1}.FluxoCaixaProjeto
             WHERE CodigoProjeto = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            dataMin = string.Format("{0:D2}", ds.Tables[0].Rows[0]["Mes"]) + "/" + ds.Tables[0].Rows[0]["Ano"];

        return dataMin;
    }
    private void carregaComboPrevisoes()
    {
        DataSet ds = cDados.getPrevisoesOrcamentarias(codigoEntidade, "");

        if (cDados.DataSetOk(ds))
        {
            ddlPrevisao.DataSource = ds;
            ddlPrevisao.TextField = "DescricaoPrevisao";
            ddlPrevisao.ValueField = "CodigoPrevisao";

            ddlPrevisao.DataBind();

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow drOficial = ds.Tables[0].Select("IndicaPrevisaoOficial = 'S'")[0];

                ddlPrevisao.Value = drOficial["CodigoPrevisao"].ToString();
            }
        }
    }

    private void carregaComboDiretorias()
    {
        DataSet ds = cDados.getDiretoriasPlanoContas(codigoEntidade, "");

        if (cDados.DataSetOk(ds))
        {
            DataRow dr = ds.Tables[0].NewRow();

            dr["CodigoConta"] = "-1";
            dr["DescricaoConta"] = "Nenhum";
            dr["CodigoReservadoGrupoConta"] = "";

            ds.Tables[0].Rows.InsertAt(dr, 0);

            ddlDiretoria.DataSource = ds;
            ddlDiretoria.TextField = "DescricaoConta";
            ddlDiretoria.ValueField = "CodigoConta";

            ddlDiretoria.DataBind();                      

            if (!IsPostBack)
            {
                ddlDiretoria.SelectedIndex = 0;
            }
        }
    }

    private void carregaComboAreas()
    {
        int codigoDiretoria = ddlDiretoria.SelectedIndex != -1 ? int.Parse(ddlDiretoria.Value.ToString()) : -1;

        DataSet ds = cDados.getAreasPlanoContas(codigoEntidade, codigoDiretoria, "");

        if (cDados.DataSetOk(ds))
        {
            DataRow dr = ds.Tables[0].NewRow();

            dr["CodigoConta"] = "-1";
            dr["DescricaoConta"] = "Nenhum";
            dr["CodigoReservadoGrupoConta"] = "";

            ds.Tables[0].Rows.InsertAt(dr, 0);

            ddlArea.DataSource = ds;
            ddlArea.TextField = "DescricaoConta";
            ddlArea.ValueField = "CodigoConta";

            ddlArea.DataBind();

            if (!IsPostBack)
            {
                ddlArea.SelectedIndex = 0;
            }
        }
    }

    private void carregaComboCentrosCustos()
    {
        int codigoArea = ddlArea.SelectedIndex != -1 ? int.Parse(ddlArea.Value.ToString())  : -1;

        DataSet ds = cDados.getCentrosCustoPlanoContas(codigoEntidade, codigoArea, "");

        if (cDados.DataSetOk(ds))
        {
            DataRow dr = ds.Tables[0].NewRow();

            dr["CodigoConta"] = "-1";
            dr["DescricaoConta"] = "Nenhum";
            dr["CodigoReservadoGrupoConta"] = "";

            ds.Tables[0].Rows.InsertAt(dr, 0);

            ddlCentro.DataSource = ds;
            ddlCentro.TextField = "DescricaoConta";
            ddlCentro.ValueField = "CodigoConta";

            ddlCentro.DataBind();

            if (!IsPostBack)
            {
                ddlCentro.SelectedIndex = 0;
            }
        }
    }
}
