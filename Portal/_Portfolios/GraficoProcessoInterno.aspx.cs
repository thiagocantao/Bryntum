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
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Web.Hosting;

public partial class _Portfolios_GraficoProcessoInterno : System.Web.UI.Page
{
    #region === Variáveis da Classe ===

    private XmlDocument xmlDoc;
    private int codigoEntidade;
    private string codigoWorkflow = string.Empty;
    private string codigoInstanciaWorkflow = string.Empty;
    private string telaLateral = "";
    public string heightWf = "";
    public string widthWf = "";

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    private string htmlColorEtapaAtual = "#B08B5D";
    private string htmlColorEtapaPercorrida = "#0068B2";
    private string htmlColorEtapaNaoPercorrida = "#FF6D00";
    private string htmlColorFonte = "#B08B5D";
    private string htmlColorLinhaPercorrida = "#0068B2";
    private string htmlColorLinhaNaoPercorrida = "#FF6D00";

    private const string _tamLinha = "0.75";

    private int codigoUsuarioResponsavel;

    private List<Etapa> EtapasWf = new List<Etapa>();
    private DataTable tabelaMenu = new DataTable();
    private dados cDados;

    public string codigoProjeto;
    public string codigoFluxo;

    public int larguraPopup = 500;
    public int alturaPopup = 300;

    protected class Etapa
    {
        public string CodigoWorkflow;
        public string CodigoInstanciaWf;
        public string SequenciaOcorrenciaEtapaWf;
        public string CodigoEtapaWf;
        public string NomeEtapa;
        public string NomeResponsavel;
        public string TextoAcao;
        public string To;
        public string dataInicio;
        public string dataTermino;
        public string atraso;
        public bool EhEtapaAtual;
        public bool EhDisjuncao;
        public bool EhEtapaDecisao;
        public int CodigoSubfluxo;
        public int CodigoSubworkflow;
        public int CodigoSubinstancia;
    }
    #endregion 

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        hfXML.Set("xmlWorkflow", "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache); //Pagina não cacheable.

        if ((Request.QueryString["Largura"] != null) && (Request.QueryString["Largura"] != ""))
        {
            larguraPopup = int.Parse(Request.QueryString["Largura"].Trim());
            if (larguraPopup <= 0)
            {
                larguraPopup = 500;
            }
        }
        if ((Request.QueryString["Altura"] != null) && (Request.QueryString["Altura"] != ""))
        {
            alturaPopup = int.Parse(Request.QueryString["Altura"].Trim());
            if (alturaPopup <= 0)
            {
                alturaPopup = 300;
            }
        }

        if (null != Request.QueryString["TL"])
            telaLateral = Request.QueryString["TL"];

        codigoWorkflow = Request.QueryString["CW"].ToString();
        codigoInstanciaWorkflow = Request.QueryString["CI"].ToString();

        if (null != Request.QueryString["CP"])
            codigoProjeto = Request.QueryString["CP"].ToString();
        if (null != Request.QueryString["CF"])
            codigoFluxo = Request.QueryString["CF"].ToString();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        ObtemDadosBanco(codigoInstanciaWorkflow, codigoWorkflow);
        setLabelsColors();
        setXMLColors();
        getWidthHeigthWf();
        
        if (!Page.IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "MensagemSemFlashFluxo");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
            lblFlsh.Text = dsParametros.Tables[0].Rows[0]["MensagemSemFlashFluxo"].ToString();
        else
            lblFlsh.Text = "";

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/GraficoProcesso.js""></script>"));
        this.TH(this.TS("GraficoProcesso"));
    }

    #region ajuste do XML

    private void ObtemDadosBanco(string codigoInstanciaWorkflow, string codigoWorkflow)
    {
        normalizaCodigoWorkflowInstancia(ref codigoInstanciaWorkflow, ref codigoWorkflow);
        DataSet ds = cDados.getInstanciasWF(codigoInstanciaWorkflow, codigoWorkflow, codigoEntidade);
        DataSet dsH = cDados.getHistoricoInstanciaWf(int.Parse(codigoWorkflow), int.Parse(codigoInstanciaWorkflow));

        //TABLE[0].
        foreach (DataRow dr in dsH.Tables[0].Rows)
        {
            Etapa etapa = new Etapa();

            etapa.CodigoWorkflow = dr["CodigoWorkflow"].ToString();
            etapa.CodigoInstanciaWf = dr["CodigoInstanciaWf"].ToString();
            etapa.SequenciaOcorrenciaEtapaWf = dr["SequenciaOcorrenciaEtapaWf"].ToString();
            etapa.CodigoEtapaWf = dr["CodigoEtapaWf"].ToString();
            etapa.TextoAcao = dr["TextoAcao"].ToString();
            etapa.dataInicio = dr["DataInicioEtapa"].ToString();
            etapa.dataTermino = dr["DataTerminoEtapa"].ToString();
            etapa.NomeEtapa = dr["NomeEtapaWf"].ToString();
            etapa.NomeResponsavel = dr["NomeUsuarioFinalizador"].ToString();
            etapa.atraso = dr["Atraso"].ToString();
            etapa.EhEtapaAtual = dr["IndicaEtapaAtual"].ToString().Equals("S");
            etapa.EhDisjuncao = dr["IndicaInicioSubWorkflow"].ToString().Equals("S");
            etapa.EhEtapaDecisao = dr["IndicaEtapaDecisao"].ToString().Equals("S");
            etapa.CodigoSubfluxo = int.Parse(dr["CodigoFluxoSubprocesso"].ToString());
            etapa.CodigoSubworkflow= int.Parse(dr["CodigoWorkflowSubprocesso"].ToString());
            etapa.CodigoSubinstancia = int.Parse(dr["CodigoInstanciaSubprocesso"].ToString());

            EtapasWf.Add(etapa);
        }

        //TABLE[1].
        // se tiver retornado mais de uma tabela no data set e se houver linhas na 2ª tabela (dados da instância)
        if ((1 < ds.Tables.Count) && (0 < ds.Tables[1].Rows.Count))
        {
            // se houver a informação nome da instância
            if (0 != ds.Tables[1].Rows[0]["NomeInstancia"].ToString().Length)
                lblTituloFluxo.Text = "   <<" + ds.Tables[1].Rows[0]["NomeInstancia"].ToString() + ">>";
            else
                lblTituloTela.Text = string.Empty;
        } // if ((1 < ds.Tables.Count) && (0 < ds.Tables[1].Rows.Count))
        else
        {
            lblTituloTela.Text = string.Empty;
        }

        //TABLE[2].
        if(ds.Tables[2].Rows.Count != 0)
        {
            hfXML.Set("xmlWorkflow", ds.Tables[2].Rows[0]["TextoXML"].ToString().Replace('"', '\''));
            lblTituloTela.Text = "WORKFLOW " + ds.Tables[2].Rows[0]["NomeWorkflow"].ToString() + ":    ";
        }

        //TABLE[3].
        foreach (DataRow dr in ds.Tables[3].Rows) //Obtem as configurações do worflow.
        {
            if (dr["Parametro"].ToString().Equals("corFonte_HistoricoWf", StringComparison.OrdinalIgnoreCase))
                htmlColorFonte = dr["Valor"].ToString().Remove(0,1); 
            else if (dr["Parametro"].ToString().Equals("corFundoEtapaAtual_HistoricoWF", StringComparison.OrdinalIgnoreCase))
                htmlColorEtapaAtual = dr["Valor"].ToString().Remove(0, 1);
            else if (dr["Parametro"].ToString().Equals("corFundoEtapaPercorrida_HistoricoWf", StringComparison.OrdinalIgnoreCase))
                htmlColorEtapaPercorrida = dr["Valor"].ToString().Remove(0, 1);
            else if (dr["Parametro"].ToString().Equals("corFundoEtapaNaoPercorrida_HistoricoWf", StringComparison.OrdinalIgnoreCase))
                htmlColorEtapaNaoPercorrida = dr["Valor"].ToString().Remove(0, 1);
            else if (dr["Parametro"].ToString().Equals("corLinhaConectorPercorrido_HistoricoWf", StringComparison.OrdinalIgnoreCase))
                htmlColorLinhaPercorrida = dr["Valor"].ToString().Remove(0, 1);
            else if (dr["Parametro"].ToString().Equals("corLinhaConectorNaoPercorrido_HistoricoWf", StringComparison.OrdinalIgnoreCase))
                htmlColorLinhaNaoPercorrida = dr["Valor"].ToString().Remove(0, 1);
        }
    }

    private void normalizaCodigoWorkflowInstancia(ref string codigoInstanciaWorkflow, ref string codigoWorkflow)
    {
        string comandoSQL = string.Format(@" 
DECLARE 
		@codigoWorkflow			int
	, @codigoInstanciaWf		bigint
	
SET @codigoWorkflow		= {2};
SET @codigoInstanciaWf	= {3};

EXEC {0}.{1}.[p_wf_normalizaCodigoWorkflowInstancia]
		@io_codigoWorkFlow		= @codigoWorkflow			OUTPUT
	,	@io_codigoInstanciaWf	= @codigoInstanciaWf		OUTPUT

SELECT @codigoWorkflow AS [CodigoWorkflow], @codigoInstanciaWf AS [CodigoInstanciaWf];        
", cDados.getDbName(), cDados.getDbOwner(), codigoWorkflow, codigoInstanciaWorkflow);

        DataSet ds1 = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            codigoWorkflow = ds1.Tables[0].Rows[0]["CodigoWorkflow"].ToString();
            codigoInstanciaWorkflow = ds1.Tables[0].Rows[0]["CodigoInstanciaWf"].ToString();
        }
    }

    private string procuraIdTimer(string from)
    {
        string toDoFrom = "";
        string toTimer = "";
        XmlNodeList nodoLista = obtemConnectorFromXml(from);
        foreach (XmlElement elemento in nodoLista)
        {
            toDoFrom = elemento.Attributes["to"].Value.ToString();

            XmlNodeList toLista = obtemSetFromTo(toDoFrom);
            foreach (XmlElement tipoElementos in toLista)
            {
                string tipoElemento = tipoElementos.Attributes["tipoElemento"].Value.ToString();
                if (tipoElemento.Equals("3"))
                    toTimer = toDoFrom;
            }
        }
        return toTimer;
    }

    private void padronizaConnectores()
    {
        XmlNodeList objEtapa;
        xmlDoc = obtemXmlDocFromXmlTela();

        objEtapa = xmlDoc.SelectNodes("/chart/connectors/connector");

        foreach (XmlElement elemento in objEtapa)
        {
            elemento.SetAttribute("dashed","0");
            hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
        }
    }
    
    private void setLabelsColors()
    {
        Color color;
        color = System.Drawing.ColorTranslator.FromHtml("#" + htmlColorEtapaAtual);
        lblCorEtapaAtual.BackColor = color;
        lblCorEtapaAtual.ForeColor = color;

        color = System.Drawing.ColorTranslator.FromHtml("#" + htmlColorEtapaNaoPercorrida);
        lblCorNaoPercorrido.BackColor = color;
        lblCorNaoPercorrido.ForeColor = color;

        color = System.Drawing.ColorTranslator.FromHtml("#" + htmlColorEtapaPercorrida);
        lblCorPercorrido.BackColor = color;
        lblCorPercorrido.ForeColor = color;
    }

    private void setXMLColors()
    {
        xmlDoc = obtemXmlDocFromXmlTela();
        string linkEtapaAtual = "";
        string linkEtapaPercorrida = "";
        //Seteando el cor da Fonte
        XmlElement elmRoot = xmlDoc.DocumentElement;
        XmlElement elmNew = xmlDoc.CreateElement("styles");

        //habilitar o uso do link no grafico.
        elmRoot.SetAttribute("enableLink", "1");
        elmRoot.AppendChild(elmNew);
        //setear stylo do grafico.
        string innerDefinition =
                @"<definition>
                        <style name='MyFirstFontStyle'   type='font' color='FFFFFF' />
                        <style name='MyToolTipFontStyle' type='font' color='330066' isHTML='1'/>
                        <style name='MyFirstShadow'      type='Shadow' color='" + htmlColorFonte + @"' />
                  </definition>
                  <application>
                        <apply toObject='DATALABELS' styles='MyFirstFontStyle' />
                        <apply toObject='TOOLTIP' styles='MyToolTipFontStyle' />
                        <apply toObject='CONNECTORLABELS' styles='MyFirstFontStyle' />
                  </application>";
        elmNew.InnerXml = innerDefinition;

        hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));

        //Inicialização de Cor.
        pintaWorkflowInicial();

        //Seteando el cor a TipoElemento = 0 (inicio)
        XmlElement objEtapaInicio = (XmlElement)xmlDoc.SelectSingleNode(@"/chart/dataSet/set[@id='0']");
        objEtapaInicio.Attributes["color"].Value = htmlColorEtapaPercorrida;
        hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));

        //Seteando el cor do 1er connector.
        XmlNodeList nodoLista = obtemConnectorFromXml("0");
        foreach (XmlElement elemento in nodoLista)
        {
            elemento.Attributes["color"].Value = htmlColorLinhaPercorrida;
            elemento.Attributes["strength"].Value = _tamLinha.ToString();
            hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
        }

        //Seteo o cor das [ETAPAS] das quais foram transitadas.
        //for (int i = 0; i < EtapasWf.Count; i++)
        foreach(Etapa etapa in EtapasWf)
        {
            XmlElement objEtapa;
            string xmlElement = "";
            string toTimer = "";
            string dadosPasso = "";
            string __processada = "";

            xmlDoc = obtemXmlDocFromXmlTela();
            xmlElement = @"/chart/dataSet/set[@id='" + etapa.CodigoEtapaWf + "']";
            objEtapa = (XmlElement)xmlDoc.SelectSingleNode(xmlElement);

            dadosPasso = "<pre> \n" +
                                                    "<b>" + Resources.traducao.GraficoProcessoInterno_in_cio + ":</b> " + etapa.dataInicio + "  \n" +
                                                    "<b>" + Resources.traducao.GraficoProcessoInterno_t_rmino + ":</b> " + etapa.dataTermino + "  \n" +
                                                    "<b>" + Resources.traducao.GraficoProcessoInterno_a__o + ":</b> " + etapa.TextoAcao + "  \n" +
                                                    "<b>" + Resources.traducao.GraficoProcessoInterno_respons_vel + ":</b> <i>" + etapa.NomeResponsavel.ToString().Replace('\'', '`') + "</i>  \n" +
                                                    "<b>" + Resources.traducao.GraficoProcessoInterno_atraso + ":</b> " + etapa.atraso + " \n<br></pre>";

            __processada = objEtapa.GetAttribute("__processada");

            // se o fluxo já tiver passado pela etapa, soma o histórico no 'toolText'
            if ("1" == __processada)
                objEtapa.Attributes["toolText"].Value += dadosPasso;
            else
            {
                objEtapa.SetAttribute("__processada", "1");
                objEtapa.Attributes["toolText"].Value = dadosPasso;
            }

            if (!etapa.EhEtapaDecisao)
            {
                objEtapa.Attributes["color"].Value = htmlColorEtapaPercorrida;
            }
            hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
            
            // preenchimento e link da etapa
            if (!etapa.EhDisjuncao)
            {
                if (etapa.EhEtapaAtual)
                {
                    objEtapa.Attributes["color"].Value = htmlColorEtapaAtual;
                    hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));

                    linkEtapaAtual = generarLinkEtapaAtual(etapa);

                    if ("" != linkEtapaAtual)
                    {
                        linkEtapaAtual = "F-_self-" + linkEtapaAtual;
                        objEtapa.SetAttribute("link", linkEtapaAtual);
                        objEtapa.SetAttribute("allowDrag", "0");
                        objEtapa.SetAttribute("cursor", "pointer");
                    }
                    else
                    {
                        objEtapa.SetAttribute("link", linkEtapaAtual);
                    }

                    hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
                }
                else if (!etapa.EhEtapaDecisao)
                {
                    string parametrosPopup = "";
                    if (Request.QueryString["Popup"] + "" == "S")
                    {
                        parametrosPopup += "&Popup=S&Altura=" + Request.QueryString["AlturaEtapaPercorrida"] + "&Largura=" + Request.QueryString["Largura"];
                    }

                    if (etapa.CodigoSubfluxo > 0)
                    {
                        string alturaEtapaPercorrida = (Request.QueryString["AlturaEtapaPercorrida"] + "") != "" ? ("&AlturaEtapaPercorrida=" + Request.QueryString["AlturaEtapaPercorrida"]) : "";
                        linkEtapaPercorrida = /*cDados.getPathSistema() +*/ "GraficoProcessoInterno.aspx?CW=" + etapa.CodigoSubworkflow.ToString() + "&CF=" + etapa.CodigoSubfluxo.ToString() + "&CI=" + etapa.CodigoSubinstancia.ToString() + "&CP=" + codigoProjeto + alturaEtapaPercorrida + parametrosPopup;
                    }
                    else
                    {
                        int nivelAcesso = cDados.obtemNivelAcessoEtapaWf(int.Parse(etapa.CodigoWorkflow.ToString()), int.Parse(etapa.CodigoInstanciaWf.ToString()), int.Parse(etapa.SequenciaOcorrenciaEtapaWf.ToString()), int.Parse(etapa.CodigoEtapaWf.ToString()), codigoUsuarioResponsavel.ToString());
                        if (nivelAcesso > 0)
                        {
                            string telaPendencias = (Request.QueryString["PND"] + "") != "" ? "&PND=S" : "";
                            string chamaTelaMenu = (Request.QueryString["TL"] + "") != "" ? "&TL=CHI" : "";
                            string moduloMenuLDP = (Request.QueryString["ModuloMenuLDP"] + "") != "" ? ("&ModuloMenuLDP=" + Request.QueryString["ModuloMenuLDP"]) : "";
                            linkEtapaPercorrida = cDados.getPathSistema() + "wfEngineInterno.aspx?CW=" + etapa.CodigoWorkflow.ToString() + "&CI=" + etapa.CodigoInstanciaWf.ToString() + "&CE=" + etapa.CodigoEtapaWf.ToString() + "&CS=" + etapa.SequenciaOcorrenciaEtapaWf.ToString() + chamaTelaMenu + telaPendencias + moduloMenuLDP + parametrosPopup;
                        }
                        else
                        {
                            linkEtapaPercorrida = "";
                        }
                    }
                    if (linkEtapaPercorrida != "")
                    {
                        linkEtapaPercorrida = "F-_self-" + linkEtapaPercorrida;
                        objEtapa.SetAttribute("allowDrag", "0");
                        objEtapa.SetAttribute("cursor", "pointer");
                    }
                    objEtapa.SetAttribute("link", linkEtapaPercorrida);
                    hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
                }
            }
            //pintar Connector
            if (etapa.TextoAcao == "timer")
            {
                toTimer = procuraIdTimer(etapa.CodigoEtapaWf); //Obtemgo o To do Timer.
                pintarConnectores(etapa.CodigoEtapaWf, toTimer);
                pintaConnectorFromTimer(toTimer); //Agora pinto do Timer ao Elemento qeu apunte.
            }
            else
            {
                etapa.To = obtemAcoesToFromXml(etapa.CodigoEtapaWf, etapa.TextoAcao); //Obtemgo o To da Etapa.
                pintarConnectores(etapa.CodigoEtapaWf, etapa.To);
            }
            //System.Diagnostics.Debug.WriteLine(objEtapa.GetAttribute("btnTextColor"));
        }
        padronizaConnectores();
    }
    
    private void pintarConnectores(string from, string to)
    {
        XmlElement objEtapaTo;
        string tipoElemento = "";

        XmlNodeList nodoLista = obtemConnectorFromToXml(from, to);
        foreach (XmlElement elemento in nodoLista)
        {
            

            elemento.Attributes["color"].Value = htmlColorLinhaPercorrida;
            elemento.Attributes["strength"].Value = _tamLinha.ToString();
            hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));

            //Agora vo a ver si o To apunta a um tipoElemento 6 (Fim)
            objEtapaTo = (XmlElement)xmlDoc.SelectSingleNode(@"/chart/dataSet/set[@id='" + to + "']");
            tipoElemento = objEtapaTo.Attributes["tipoElemento"].Value.ToString();

            if (tipoElemento.Equals("6"))
                objEtapaTo.Attributes["color"].Value = htmlColorEtapaPercorrida;

            hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
        }
    }

    private void pintaConnectorFromTimer(string from)
    {
        string toTimer = "";
        XmlNodeList nodoLista = obtemConnectorFromXml(from);
        foreach (XmlElement elemento in nodoLista)
        {
            toTimer = elemento.Attributes["to"].Value.ToString();
            pintarConnectores(from, toTimer);
        }
    }
    
    private void pintaWorkflowInicial()
    {
        XmlNodeList objEtapa;
        xmlDoc = obtemXmlDocFromXmlTela();
        string tipoElemento = "";
        //Inicio das Etapas.
        objEtapa = xmlDoc.SelectNodes("/chart/dataSet/set");

        foreach (XmlElement elemento in objEtapa)
        {
            tipoElemento = elemento.Attributes["tipoElemento"].Value.ToString();
            if ((tipoElemento != "3") && (tipoElemento != "7"))
            {
                elemento.Attributes["color"].Value =htmlColorEtapaNaoPercorrida;
                hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
            }
        }

        //Inicio dos Connectores.
        objEtapa = xmlDoc.SelectNodes("/chart/connectors/connector");

        foreach (XmlElement elemento in objEtapa)
        {
            elemento.Attributes["color"].Value = htmlColorLinhaNaoPercorrida;
            elemento.Attributes["strength"].Value = "0.25";
            hfXML.Set("xmlWorkflow", xmlDoc.InnerXml.ToString().Replace('"', '\''));
        }
    }

    //<workflows xmlVersion='001.1.016' width='1300' height='600'>
    private void getWidthHeigthWf()
    {
        XmlNodeList objWorkflow;
        xmlDoc = obtemXmlDocFromXmlTela();
        //Inicio das Etapas.
        objWorkflow = xmlDoc.SelectNodes("/chart/workflows");

        foreach (XmlElement elemento in objWorkflow)
        {
            heightWf = (elemento.Attributes["height"] != null ? elemento.Attributes["height"].Value.ToString() : "");
            widthWf = (elemento.Attributes["width"] != null ? elemento.Attributes["width"].Value.ToString() : "");
        }

        heightWf = (heightWf == "" ? "100%" : heightWf);
        widthWf = (widthWf == "" ? "100%" : widthWf);
    }

    #endregion

    #region Tratamento de XML

    private XmlDocument obtemXmlDocFromXmlTela()
    {
        xmlDoc = new XmlDocument();
        XmlReader xmlR = XmlReader.Create(new StringReader(hfXML.Get("xmlWorkflow").ToString()));
        xmlDoc.Load(xmlR);
        return xmlDoc;
    }

    private XmlNodeList obtemSetFromTo(string idFromTo)
    {
        string selectNode = string.Format("/chart/dataSet/set[@id='{0}']", idFromTo);
        xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.SelectNodes(selectNode);
        return lista;
    }

    private string obtemAcoesToFromXml(string idEtapa, string idAcao)
    {
        return obtemValoresAcaoFromXml(idEtapa, idAcao, "to");
    }

    private string obtemValoresAcaoFromXml(string idEtapa, string idAcao, string tipo)
    {
        if (idAcao.Equals("Fim de subfluxo"))
        {
            idAcao = "";
        }
        string selectNode = string.Format("/chart/dataSet/set[@id='{0}']/acoes/acao[@id='{1}']", idEtapa, idAcao, tipo);
        xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.SelectNodes(selectNode);
        string descricao = "";

        if ((lista.Item(0) != null) && (lista.Item(0).FirstChild != null))
        {
            descricao = lista.Item(0).Attributes[tipo].Value.ToString();
        }

        return descricao;
    }

    private XmlNodeList obtemConnectorFromXml(string from)
    {
        string selectNode = string.Format("/chart/connectors/connector[@from='{0}']", from);
        xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.SelectNodes(selectNode);
        return lista;
    }

    private XmlNodeList obtemConnectorFromToXml(string from, string to)
    {
        string selectNode = string.Format("/chart/connectors/connector[@from='{0}' and @to='{1}']", from, to);
        xmlDoc = obtemXmlDocFromXmlTela();
        XmlNodeList lista = xmlDoc.SelectNodes(selectNode);
        return lista;
    }

    #endregion

    #region VARIOS

    private string generarLinkEtapaAtual(Etapa etapa)
    {
        string telaPendencias = (Request.QueryString["PND"] + "") != "" ? "&PND=S" : "";
        string chamaTelaMenu = (Request.QueryString["TL"] + "") != "" ? "&TL=CHI" : "";
        string moduloMenuLDP = (Request.QueryString["ModuloMenuLDP"] + "") != "" ? ("&ModuloMenuLDP=" + Request.QueryString["ModuloMenuLDP"]) : "";
        string linkEtapaAtual = "";

        string parametrosPopup = "";
        if (Request.QueryString["Popup"] + "" == "S")
        {
            parametrosPopup += "&Popup=S&Altura=" + (int.Parse(Request.QueryString["Altura"].ToString()) + 38) + "&Largura=" + Request.QueryString["Largura"];
        }

        if (etapa.CodigoSubfluxo > 0)
        {
            string alturaEtapaPercorrida = (Request.QueryString["AlturaEtapaPercorrida"] + "") != "" ? ("&AlturaEtapaPercorrida=" + Request.QueryString["AlturaEtapaPercorrida"]) : "";
            linkEtapaAtual = "./GraficoProcessoInterno.aspx?CW=" + etapa.CodigoSubworkflow.ToString() + "&CF=" + etapa.CodigoSubfluxo.ToString() + "&CI=" + etapa.CodigoSubinstancia.ToString() + "&CP=" + codigoProjeto + alturaEtapaPercorrida + parametrosPopup;
        }
        else
        {
            int nivelAcesso = cDados.obtemNivelAcessoEtapaWf(int.Parse(etapa.CodigoWorkflow), int.Parse(etapa.CodigoInstanciaWf), int.Parse(etapa.SequenciaOcorrenciaEtapaWf), int.Parse(etapa.CodigoEtapaWf), codigoUsuarioResponsavel.ToString());
            if (nivelAcesso > 0)
                linkEtapaAtual = "../wfEngineInterno.aspx?CW=" + etapa.CodigoWorkflow + "&CI=" + etapa.CodigoInstanciaWf + "&CE=" + etapa.CodigoEtapaWf + "&CS=" + etapa.SequenciaOcorrenciaEtapaWf + "&CP=" + codigoProjeto + "&CF=" + codigoFluxo + chamaTelaMenu + telaPendencias + moduloMenuLDP + parametrosPopup;
        }
        return linkEtapaAtual;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        int altura = (alturaPrincipal - 55);

        hfXML.Set("alturaDiv", altura-80);
        pnFlash.Height = altura - 190;
        //if ("" != telaLateral)
        //    pnFlash.Width = ;
        //else
        pnFlash.Width = new Unit("100%");
        //pnFlash.Style.Add("OVERFLOW", "auto");
    }
    #endregion
}
