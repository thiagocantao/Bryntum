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
using System.Text;

public partial class _Estrategias_indicador_graficos_grafico_004 : System.Web.UI.Page
{
    dados cDados;

    int codigoIndicador = 0, codigoUnidadeLogada = 0, mes = 0, ano = 0, casasDecimais = 0, codigoUnidadeSelecionada = 0;
    string polaridade = "";

    char tipoDesempenho = 'A';

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=" + Resources.traducao.grafico_004_sem_informa__es_a_apresentar;
    public string msgNoData = "&ChartNoDataText=" + Resources.traducao.grafico_004_sem_informa__es_a_apresentar;
    public string msgInvalid = "&InvalidXMLText=" + Resources.traducao.grafico_004_sem_informa__es_a_apresentar;
    public string desenhando = "&PBarLoadingText=" + Resources.traducao.grafico_004_gerando_imagem___;
    public string msgLoading = "&XMLLoadingText=" + Resources.traducao.grafico_004_carregando___;
    
    public string grafico2_titulo = Resources.traducao.grafico_004_desempenho_geral;
    public string grafico2_swf = "../../../Flashs/HLinearGauge.swf";
    public string grafico2_xml = "";
    public string grafico2_xmlzoom = "";

    public int alturaGrafico = 220;
    public int larguraGrafico = 350;

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

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoIndicador = int.Parse(cDados.getInfoSistema("COIN").ToString());

        if (Request.QueryString["NumeroCasas"] != null && Request.QueryString["NumeroCasas"].ToString() != "")
        {
            casasDecimais = int.Parse(Request.QueryString["NumeroCasas"].ToString());
        }

        if (Request.QueryString["Polaridade"] != null && Request.QueryString["Polaridade"].ToString() != "")
        {
            polaridade = Request.QueryString["Polaridade"].ToString();
        }

        if (cDados.getInfoSistema("TipoDesempenho") != null)
            tipoDesempenho = char.Parse(cDados.getInfoSistema("TipoDesempenho").ToString());

        if (cDados.getInfoSistema("CodigoEntidade") != null)
            codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (cDados.getInfoSistema("CodigoUnidade") != null && cDados.getInfoSistema("CodigoUnidade").ToString() != "")
            codigoUnidadeSelecionada = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());

        if (cDados.getInfoSistema("AnoIndicador") != null)
            ano = int.Parse(cDados.getInfoSistema("AnoIndicador").ToString());

        if (cDados.getInfoSistema("MesIndicador") != null)
            mes = int.Parse(cDados.getInfoSistema("MesIndicador").ToString());

        defineTamanhoObjetos();
        carregaGraficoDesempenho();
        geraXMLGeoreferenciamento();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../../scripts/geoReferenciamento.js""></script>"));
        this.TH(this.TS("geoReferenciamento"));

        cDados.aplicaEstiloVisual(this);
    }

    public void geraXMLGeoreferenciamento()
    {

        DataSet dsRetorno;
        string mensagemErro = "";
        string comandoSQLPontosPoligono = string.Format("select latitude, longitude, SiglaUF from {0}.{1}.poligonoUF where SiglaUF = 'PE'", cDados.getDbName(), cDados.getDbOwner());
        DataSet dsRetornoPontos = cDados.getDataSet(comandoSQLPontosPoligono);

        string comandoSQL = string.Format(
        @"SELECT * FROM {0}.{1}.f_GetDesempenhoIndicadorCoordenadas (
                        {2}--<@in_CodigoIndicador, int,>
                        ,'{3}'--<@in_TipoInformacao, char(1),>
                        ,{4}--<@in_Ano, smallint,>
                        ,{5}--<@in_Mes, tinyint,>
                        ,{6}--<@in_CodigoUnidadeNegocio, int,>
                        ,{7})--<@in_CodigoUsuario, int,>",
                              cDados.classeDados.databaseNameCdis/*nome do banco*/,
                              cDados.classeDados.OwnerdbCdis/*nome owner banco*/,
                              codigoIndicador/*<@in_CodigoIndicador, int,>*/,
                              tipoDesempenho/*<@in_TipoInformacao, char(1),>*/,
                              ano/*<@in_Ano, smallint,>*/,
                              mes/*<@in_Mes, tinyint,>*/,
                              codigoUnidadeSelecionada/*<@in_CodigoUnidadeNegocio, int,>*/,
                              cDados.getInfoSistema("IDUsuarioLogado").ToString()/*<@in_CodigoUsuario, int,>*/);

        StringBuilder xml = new StringBuilder();
        try
        {

            dsRetorno = cDados.getDataSet(comandoSQL);
            //Cria as variáveis para a formação do XML

            string xmlCabecalhoGeral = string.Format(
             @"<mapa latitudeCentral=""-15.79886"" 
                        longitudeCentral=""-47.74405"" 
                        zoom=""4"" tipo=""ROADMAP"" 
                        mostra_panControl=""true"" 
                        mostra_zoomControl=""true"" 
                        mostra_mapTypeControl=""true"" 
                        mostra_scaleControl=""true"" 
                        mostra_streetViewControl=""true"" 
                        mostra_overviewMapControl=""true"" 
                        draggable=""false"">");
            xml.Append(xmlCabecalhoGeral);

            if (dsRetorno.Tables[0].Rows.Count > 0)
            {
                xml.Append(@"<markers>");
            }

            foreach (DataRow dr in dsRetorno.Tables[0].Rows)
            {

                string CodigoUnidadeNegocio = dr["CodigoUnidadeNegocio"].ToString();
                string CorUF = dr["CorUF"].ToString();
                string Realizado = dr["Realizado"].ToString();
                string Meta = dr["Meta"].ToString();
                string SiglaUnidade = dr["SiglaUnidade"].ToString();
                string NomeUnidade = dr["NomeUnidade"].ToString();
                string Latitude = dr["Latitude"].ToString();
                string Longitude = dr["Longitude"].ToString();

                string xmlMarcadores = "";

                xmlMarcadores += string.Format(@"<marker id=""{0}""", Latitude + Longitude);
                xmlMarcadores += string.Format(@" lat=""{0}"" ", Latitude);
                xmlMarcadores += string.Format(@" lng=""{0}"" ", Longitude);

                string trataCor = CorUF;
                string corPino = "FFFFFF";
                if(CorUF.Trim() == "Branco")
                {
                    trataCor = string.Format(@"../../../imagens/brancoMenor.gif");
                    corPino = "FFFFFF";
                }

                if (CorUF.Trim() == "Verde")
                {
                    trataCor = string.Format(@"../../../imagens/verdeMenor.gif");
                    corPino = "32CD32";
                }

                if (CorUF.Trim() == "Vermelho")
                {
                    trataCor = string.Format(@"../../../imagens/vermelhoMenor.gif");
                    corPino = "FF0000";
                }

                if (CorUF.Trim() == "Azul")
                {
                    trataCor = string.Format(@"../../../imagens/azulMenor.gif");
                    corPino = "0000FF";
                }
                
                if (CorUF.Trim() == "Amarelo")
                {
                    trataCor = string.Format(@"../../../imagens/amareloMenor.gif");
                    corPino = "FFFF00";
                }

                xmlMarcadores += string.Format(@" status=""{0}"" ", trataCor);
                xmlMarcadores += string.Format(@" arrastavel=""N"" ");
                xmlMarcadores += string.Format(@" title=""{0}"" ", NomeUnidade);
                xmlMarcadores += string.Format(@" cor=""{0}"" ", corPino);
                xmlMarcadores += string.Format(@" meta=""{0}"" ", Meta);
                xmlMarcadores += string.Format(@" resultado=""{0}"" ", Realizado);
                xmlMarcadores += ">";

                string htmlShowWindowMarcador = string.Format(@"<![CDATA[
                <html>
                    <body>
                        <table style=""border: thin solid #008000; width: 280px; height: 140px;"">
                            <tr>
                                <td>Unidade:</td><td style=""border: thin solid #0000FF;"">{0}</td>
                            </tr>
                            <tr>
                                <td>Meta:</td><td style=""border: thin solid #0000FF;"">{1:0.00}</td>
                            </tr>
                            <tr>
                                <td>Resultado:</td><td style=""border: thin solid #0000FF;"">{2:0.00}</td>
                            </tr>
                            <tr>
                                <td>Desempenho:</td><td style=""border: thin solid #0000FF;""><img src=""{3}"" /></td>
                            </tr>
                        </table>
                    </body>
                </html>]]>", NomeUnidade, decimal.Parse(Meta),decimal.Parse(Realizado), trataCor);
                
                xmlMarcadores += htmlShowWindowMarcador;
                xml.Append(xmlMarcadores);
                xml.Append("</marker>");
            }

            if (dsRetorno.Tables[0].Rows.Count > 0)
            {
                xml.Append(@"</markers>");
            }
            xml.Append(string.Format(@"
            <controles>
                <!-- Controles que aparecem no mapa. -->
                <controle  nome=""panControl"" visivel=""true"" posicionamento=""TOP_LEFT""/>
                <controle  nome=""zoomControl"" visivel=""true"" posicionamento=""TOP_CENTER"" zoomControlStyle=""DEFAULT""/>
                <controle  nome=""mapTypeControl"" visivel=""true"" posicionamento=""TOP_RIGHT""/>
                <controle  nome=""scaleControl"" visivel=""true"" posicionamento=""BOTTOM_LEFT""/>
                <controle  nome=""streetViewControl"" visivel=""true"" posicionamento=""BOTTOM_CENTER""/>
                <controle  nome=""overviewMapControl"" visivel=""true"" posicionamento=""BOTTOM_RIGHT""/>
             </controles>"));


            if (dsRetornoPontos.Tables[0].Rows.Count > 0)
            {
                xml.Append(@"<pontos>");
            }
                       
            foreach (DataRow dr in dsRetornoPontos.Tables[0].Rows)
            {

                string latitude = dr["latitude"].ToString();
                string longitude = dr["longitude"].ToString();
                string siglaUF = dr["SiglaUF"].ToString();

                xml.Append(string.Format(@"<ponto latitude=""{0}"" longitude=""{1}"" SiglaUF=""{2}""/>", latitude, longitude, siglaUF));
            }

            if (dsRetornoPontos.Tables[0].Rows.Count > 0)
            {
                xml.Append(@"</pontos>");
            }

            xml.Append("</mapa>");

            hfGeral.Set("xmlReferenciamento", xml.ToString());
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
        }
    }



    //Carrega o gráfico 
    private void carregaGraficoDesempenho()
    {
        string xml = "";

        string nomeGrafico = @"/ArquivosTemporarios/desempenhoGeralIndicador_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        DataSet dsGrafico = cDados.getDesempenhoIndicadorUnidade(codigoIndicador, mes, ano, tipoDesempenho, codigoUnidadeSelecionada, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), "");

        if (cDados.DataSetOk(dsGrafico))
        {
            DataTable dt = dsGrafico.Tables[0];

            //gera o xml do gráfico Gauge do percentual concluido
            xml = cDados.getGraficoDesempenhoGeralIndicador(dt, 9, "", casasDecimais, polaridade, codigoIndicador);

            //escreve o arquivo XML do gráfico Gauge do percentual concluido
            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado
            grafico2_xml = "../../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;

            nomeGrafico = "/ArquivosTemporarios/desempenhoGeralIndicadorZoom_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

            //gera o xml do gráfico Gauge do percentual concluido ZOOM
            xml = cDados.getGraficoDesempenhoGeralIndicador(dt, 15, "Desempenho Geral", casasDecimais, polaridade, codigoIndicador);
            //escreve o arquivo XML do gráfico Gauge do percentual concluido ZOOM
            cDados.escreveXML(xml, nomeGrafico);

            grafico2_xmlzoom = nomeGrafico;
        }
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        alturaGrafico = (altura - 307);
        
        divGoogleMaps.Style.Add("height", (altura - 295) + "px");
        divGoogleMaps.Style.Add("overflow", "scroll");
    }
}
