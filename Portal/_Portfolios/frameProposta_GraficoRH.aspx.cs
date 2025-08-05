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

public partial class _Portfolios_frameProposta_RH : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidadeUsuarioResponsavel;
    string codigoRH = "-1";
    int cenario = -1, categoria = -1;

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    //Cria as variáveis para carregar o 1º gráfico de Bullets (Custos)
    public string grafico_titulo = "Desempenho";
    public string grafico_swf = "../Flashs/ScrollStackedColumn2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "";
    public string larguraGrafico = "";

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CRH"] != null)
        {
            codigoRH = Request.QueryString["CRH"].ToString();
        }
        else
        {
            if (cDados.getInfoSistema("CodigoRH") != null)
                codigoRH = cDados.getInfoSistema("CodigoRH").ToString();
        }

        if (Request.QueryString["Cenario"] != null)
        {
            if (cDados.getInfoSistema("Cenario") != null)
                cenario = int.Parse(cDados.getInfoSistema("Cenario").ToString());

            if (cDados.getInfoSistema("Categoria") != null)
                categoria = int.Parse(cDados.getInfoSistema("Categoria").ToString());
        }

        defineTamanhoGrafico();
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        geraGrafico();
    }

    private void geraGrafico()
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/RHProposta_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        string where = "";

        if (codigoRH != "-1")
            where = " AND Codigo = '" + codigoRH + "'";

        DataSet ds;

        if (cenario != -1)
        {
            string whereCategoria = "";

            if (categoria != -1)
                whereCategoria += " AND p.CodigoCategoria = " + categoria;

            //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
            ds = cDados.getDisponibilidadeRHCenario(codigoEntidadeUsuarioResponsavel, cenario, where, whereCategoria);
        }
        else
        {
            //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
            ds = cDados.getDisponibilidadeRHEntidade(codigoEntidadeUsuarioResponsavel, where);
        }

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && codigoRH != "-1")
        {
            string whereCampo = "";

            if (cDados.getInfoSistema("Cenario") + "" != "")
                whereCampo = " AND IndicaCenario" + cDados.getInfoSistema("Cenario") + " = 'S'";

            DataSet dsGrupo = cDados.getRecursosSelecaoBalanceamento(codigoEntidadeUsuarioResponsavel, whereCampo, " AND t.Codigo = '" + codigoRH + "'");
            if (cDados.DataSetOk(dsGrupo) && cDados.DataTableOk(dsGrupo.Tables[0]))
            {
                pnRH.HeaderText = "Disponibilidade dos Recursos - " + dsGrupo.Tables[0].Rows[0]["DescricaoGrupo"].ToString();
            }
            else
            {
                pnRH.HeaderText = "Disponibilidade dos Recursos";
            }

        }

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoRHProposta(ds.Tables[0], "", 9);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado
        grafico_xml = ".." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

        //define o caminho e o nome do XML do gráfico de ZOOM (PIZZA)
        nomeGrafico = "\\ArquivosTemporarios\\RHPropostaZOOM_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        //função para gerar a estrutura xml para criação do gráfico de quantidade de projetos por entidade
        xml = cDados.getGraficoRHProposta(ds.Tables[0], "Disponibilidade dos Recursos", 15);

        //escreve o arquivo xml de quantidade de projetos por entidade
        cDados.escreveXML(xml, nomeGrafico);

        //atribui o valor do caminho do XML a ser carregado (ZOOM)
        grafico_xmlzoom = nomeGrafico;
        grafico_xmlzoom = grafico_xmlzoom.Replace("\\", "/");

    }

    private void defineTamanhoGrafico()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        if (Request.QueryString["DefineAltura"] != null && Request.QueryString["DefineAltura"] + "" == "S")
        {
            alturaGrafico = (altura - 370).ToString();
            larguraGrafico = (largura - 35).ToString();
        }
        else
        {
            larguraGrafico = (820).ToString();
            alturaGrafico = (355).ToString();
        }
    }
}
