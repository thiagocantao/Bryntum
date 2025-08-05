using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Estrategias_mapa_Macrometa : System.Web.UI.Page
{
    int codigoObjeto = 0;
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    private string iniciaisObjeto = "PP";

    //pega a data e hora atual do sistema
    string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".xml";
    string corFatorChave = "000000";

    //Mensagens de erro para os gráficos
    public string msgErro = "?LoadDataErrorText=Sem Informações a Apresentar";
    public string msgNoData = "&ChartNoDataText=Sem Informações a Apresentar";
    public string msgInvalid = "&InvalidXMLText=Sem Informações a Apresentar";
    public string desenhando = "&PBarLoadingText=Gerando imagem...";
    public string msgLoading = "&XMLLoadingText=Carregando...";

    public string grafico_titulo = "";
    public string grafico_swf = "../../Flashs/ScrollLine2D.swf";
    public string grafico_xml = "";
    public string grafico_xmlzoom = "";

    public string alturaGrafico = "143";
    public int nivelNavegacao = 2;
    public string escondeTdComentario = "";

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        if (Request.QueryString["INI"] != null && Request.QueryString["INI"].ToString() != "")
            iniciaisObjeto = Request.QueryString["INI"].ToString();

        defineAlturaTela(resolucaoCliente);

        if (Request.QueryString["Cod"] != null && Request.QueryString["Cod"].ToString() != "")
            codigoObjeto = int.Parse(Request.QueryString["Cod"].ToString());

        if (iniciaisObjeto != "PP")
            nivelNavegacao = 3;

        carregaDadosMacrometa();
        carregaProjetosFatorChave();
        carregaAnosAnalisesFatorChave();

        //cDados.aplicaEstiloVisual(this);
        lblFonte.Font.Size = new FontUnit("7pt");

        if (iniciaisObjeto == "OB")
        {
            alturaGrafico = "293";
            escondeTdComentario = "display:none;";
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 175);
    }

    private void carregaProjetosFatorChave()
    {
        DataSet ds = cDados.getProjetosFatorChave2(codigoObjeto, iniciaisObjeto, codigoUsuarioResponsavel, DateTime.Now.Year);

        if (cDados.DataSetOk(ds))
        {
            gvProjetos.DataSource = ds;
            gvProjetos.DataBind();
        }
    }

    private string getTabelaDetalhes(DataRow dr)
    {
        string table = "";

        if (iniciaisObjeto == "PP")
        {
            table = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""style1"">
                                             <tr>
                                               <td style=""border-bottom:1px solid #CCCCCC; color: {0}; font-weight: bold; width:130px; height:44px;font-size: 11pt;"">
                                                    Macrometa
                                               </td>
                                               <td style=""border-bottom:1px solid #CCCCCC; height:44px"">
                                                    {1}
                                                </td>
                                             </tr>
                                             <tr>
                                               <td style=""border-bottom:1px solid #CCCCCC; color: {0}; font-weight: bold; width:130px; height:44px;font-size: 11pt;"">
                                                    Indicador
                                               </td>
                                               <td style=""border-bottom:1px solid #CCCCCC; height:44px"">
                                                    {2}
                                                </td>
                                             </tr>
                                             <tr>
                                               <td style=""color: {0}; font-weight: bold; width:130px; height:44px;font-size: 11pt;"">
                                                    Descrição
                                               </td>
                                               <td style=""height:44px"">
                                                    {3}
                                                </td>
                                             </tr>
                                            </table>", dr["CorFatorChave"].ToString().ToUpper() == "#FFFFFF" ? "" : dr["CorFatorChave"].ToString()
                                                       , dr["Macrometa"]
                                                       , dr["Indicador"]
                                                       , dr["Descricao"]);
        }
        else
        {
            table = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""style1"">
                                             <tr>
                                               <td style=""border-bottom:1px solid #CCCCCC; color: {0}; font-weight: bold; width:130px; height:33px;font-size: 11pt;"">
                                                    Tema
                                               </td>
                                               <td style=""border-bottom:1px solid #CCCCCC; height:33px"">
                                                    {1}
                                                </td>
                                             </tr>
                                             <tr>
                                               <td style=""border-bottom:1px solid #CCCCCC; color: {0}; font-weight: bold; width:130px; height:33px;font-size: 11pt;"">
                                                    Objetivo
                                               </td>
                                               <td style=""border-bottom:1px solid #CCCCCC; height:33px"">
                                                    {2}
                                                </td>
                                             </tr>
                                             <tr>
                                               <td style=""border-bottom:1px solid #CCCCCC; color: {0}; font-weight: bold; width:130px; height:33px;font-size: 11pt;"">
                                                    Indicador
                                               </td>
                                               <td style=""border-bottom:1px solid #CCCCCC; height:33px"">
                                                    {3}
                                                </td>
                                             </tr>
                                             <tr>
                                               <td style=""color: {0}; font-weight: bold; width:130px; height:33px;font-size: 11pt;"">
                                                    Descrição
                                               </td>
                                               <td style=""height:33px"">
                                                    {4}
                                                </td>
                                             </tr>
                                            </table>", dr["CorFatorChave"].ToString().ToUpper() == "#FFFFFF" ? "" : dr["CorFatorChave"].ToString()
                                                       , dr["Tema"]
                                                       , dr["Objetivo"]
                                                       , dr["Indicador"]
                                                       , dr["Descricao"]);
        }

        corFatorChave = dr["CorFatorChave"].ToString().ToUpper() == "FFFFFF" ? "000000" : dr["CorFatorChave"].ToString().Substring(1);

        return table;
    }

    private void carregaDadosMacrometa()
    {
        DataSet ds = cDados.getDadosMetaFatorChave(codigoObjeto, iniciaisObjeto);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            string table = getTabelaDetalhes(dr);

            spanDetalhes.InnerHtml = table;

            spanComentarios.InnerHtml = dr["Comentario"].ToString().Replace(Environment.NewLine, "<BR>").Replace("\n", "<BR>");
            lblFonte.Text = "Fonte: " + dr["Fonte"].ToString();

            string tableStatus = string.Format(@"<table cellpadding=""0"" cellspacing=""0"">
                                                    <tr>
                                                        <td>Status da Meta: </td>
                                                        <td align=""center"" style=""width:20px"">
                                                         <img title=""{1}"" src=""../../imagens/{0}.gif"" />
                                                        </td>
                                                        <td style=""font-weight:bold;"">
                                                            {1}
                                                        </td>
                                             </tr>
                                            </table>", dr["StatusCor"].ToString() == "" ? "Branco" : dr["StatusCor"].ToString()
                                                   , dr["StatusDescricao"]);

            spanStatus.InnerHtml = tableStatus;

            int casasDecimais = dr["CasasDecimais"].ToString() == "" ? 0 : int.Parse(dr["CasasDecimais"].ToString());

            geraGrafico(dr["CodigoIndicador"].ToString(), casasDecimais, dr["SiglaUnidadeMedida"].ToString());
        }
    }

    private void carregaAnosAnalisesFatorChave()
    {
        DataSet ds = cDados.getAnosAnalisesFatorChave(codigoObjeto, iniciaisObjeto);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ASPxNavBar nb = new ASPxNavBar();

            nb.Paddings.Padding = new Unit("0px");
            nb.Width = new Unit("100%");
            nb.EncodeHtml = false;
            nb.GroupSpacing = new Unit("4px");
            nb.Groups.Clear();
            nb.JSProperties["cp_codigoObjeto"] = codigoObjeto;
            nb.JSProperties["cp_IniciaisObjeto"] = iniciaisObjeto;
            //nb.AutoCollapse = true;

            nb.ClientSideEvents.ExpandedChanged = @"function(s, e) {
	                if(e.group.GetExpanded())
	                {
		                document.getElementById('frm_' + e.group.name).src = 'DetalhesAnoMacrometa.aspx?Ano=' + e.group.name + '&CF=' + s.cp_codigoObjeto + '&INI=' + s.cp_IniciaisObjeto;
	                }
                }";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                NavBarGroup grupo;

                grupo = nb.Groups.Add("Tendências e Agenda " + dr["Ano"].ToString(), dr["Ano"].ToString());
                grupo.Expanded = false;

                string textoItem = string.Format(@"<table style='width:100%;'><tr><td><iframe id=""frm_{0}"" frameborder=""0"" height=""{1}"" scrolling=""auto"" src="""" width=""100%""></iframe></td></tr></table>"
                    , dr["Ano"].ToString()
                    , dr["Ano"].ToString() == DateTime.Now.Year.ToString() ? "415px" : "550px");

                NavBarItem nbi = grupo.Items.Add(textoItem);
                nbi.ClientEnabled = false;
            }

            spanAnos.Controls.Add(cDados.getLiteral("<br>"));
            spanAnos.Controls.Add(nb);
        }
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrafico(string codigoIndicador,int casasDecimais, string unidadeMedida)
    {
        //cria  a variável para armazenar o XML
        string xml = "";

        //cria uma variável com o nome e o caminho do XML do gráfico de PIZZA
        string nomeGrafico = @"/ArquivosTemporarios/metas_" + codigoIndicador + "_" + dataHora;

        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataSet dsGrafico = cDados.getPeriodicidadeIndicadorFatorChave(codigoEntidadeUsuarioResponsavel, (codigoIndicador == "" ? -1 : int.Parse(codigoIndicador)), "");

        if (cDados.DataSetOk(dsGrafico))
        {
            DataTable dt = dsGrafico.Tables[0];

            //gera o xml do gráfico Gauge do percentual concluido
            xml = cDados.getGraficoMacrometa(dt, "", 11, casasDecimais, 9, 2, 24, codigoIndicador, corFatorChave, unidadeMedida);

            //escreve o arquivo xml de quantidade de projetos por entidade
            cDados.escreveXML(xml, nomeGrafico);

            //atribui o valor do caminho do XML a ser carregado
            grafico_xml = "../.." + nomeGrafico + msgErro + msgNoData + msgInvalid + desenhando + msgLoading;
        }
    }

    public string getDescricao()
    {
        string descricaoProjeto = Eval("NomeProjeto").ToString();

        if(Eval("PodeAcessarProjeto").ToString() == "S")
        {
            string linkProjeto = Eval("IndicaProjetoCarteiraPrioritaria").ToString() == "S" ? "indexResumoProjeto" : "cni_ResumoProjeto";

            descricaoProjeto = string.Format(@"<a class='LinkGrid' target='_top' href='../../_Projetos/DadosProjeto/" + linkProjeto + ".aspx?IDProjeto=" + Eval("CodigoProjeto") + "&NomeProjeto=" + Eval("NomeProjeto") + "&NivelNavegacao=" + nivelNavegacao + "'>" + Eval("NomeProjeto") + "</a>");
        }

        string table = string.Format(@"<table>
                                            <tr>                                                
                                                <td>{0}</td>
                                            </tr>
                                       </table>", descricaoProjeto);         

        return table;
    }
}