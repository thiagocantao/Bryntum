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
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
 //para identar o xml

public partial class _Estrategias_mapaEstrategicoReuniao : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel;
    public string xmlMapaEstrategico = ""; //variavel temporal, teste de gerar xml do mapa estratégico.
    public string webServicePath = "";  // caminho do web service
    public string alturaObject = "";
    public string codigoMapa = "";
    public string nomeMapa = "";
    public string codigoUsuario = "";
    public string codigoEntidadeMapa = "";
    public int alturaDivMapa = 0;
    private int codigoEntidade, codigoUnidadeMapa = 0;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        codigoUsuario = codigoUsuarioResponsavel.ToString();
        codigoEntidadeMapa = codigoEntidade.ToString();
        webServicePath = getWebServicePath();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.TH(this.TS("mapaEstrategicoReuniao"));
        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());


        codigoMapa = cDados.getInfoSistema("CodigoMapa").ToString();


        DataSet dsUnidadeMapa = cDados.getMapasEstrategicos(null, " AND Mapa.CodigoMapaEstrategico = " + codigoMapa);

        if (cDados.DataSetOk(dsUnidadeMapa) && cDados.DataTableOk(dsUnidadeMapa.Tables[0]))
        {
            codigoUnidadeMapa = int.Parse(dsUnidadeMapa.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());
            lblMapa.Text = dsUnidadeMapa.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString();
        }

        if (codigoUnidadeMapa != codigoEntidade)
        {
            DataSet dsUnidade = cDados.getUnidadeNegocio(" AND un.CodigoUnidadeNegocio = " + codigoUnidadeMapa);
            if (cDados.DataSetOk(dsUnidade) && cDados.DataTableOk(dsUnidade.Tables[0]))
                lblEntidadeDiferente.Text = Resources.traducao.mapaEstrategicoReuniao__voc__est__visualizando_as_informa__es_da_unidade__ + dsUnidade.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
        }
        else
        {
            lblEntidadeDiferente.Text = "";
        }

        if (codigoUnidadeMapa != 0)
            codigoEntidade = codigoUnidadeMapa;
        

    }

    #region VARIOS
    
    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 105);
        if (altura > 0)
        {         
            alturaDivMapa = altura - 35;
            alturaObject = (altura - 35) + "px";
        }
    }

    private string getWebServicePath()
    {
        return cDados.getPathSistema() + "wsPortal.asmx?WSDL";
    }

    #endregion

    private int getCodigoMapaAtivo(int codigoEntidade)
    {
        int codigoMapa = -1;

        DataSet dsMapa = cDados.getMapasEstrategicos(codigoEntidade, " AND IndicaMapaEstrategicoAtivo = 'S'");

        if (cDados.DataSetOk(dsMapa) && cDados.DataTableOk(dsMapa.Tables[0]))
            codigoMapa = int.Parse(dsMapa.Tables[0].Rows[0]["CodigoMapaEstrategico"].ToString());

        return codigoMapa;
    }

   
    #region GERA XML

    /// <summary>
    /// Teste de criação do XML a partir do dados do mapa.
    /// A criação será feita em etapas:
    /// - mapa> dados principais do mapam CodigoMapa, TituloMapa, Versão, etc.
    /// - dataSet> os objetos qeu contem o mapa.
    /// - styles> as configurações padrão do mapa.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTempXmlMapa_Click(object sender, EventArgs e)
    {
        DataSet ds;



        //MAPA - Obtem o valores do mapa actual.
        string xmlMapa = @"<?xml version=""1.0"" encoding=""utf-8""?>" + Environment.NewLine;
        ds = cDados.getMapaEstrategicoToXML(codigoMapa, codigoEntidade.ToString());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            xmlMapa += string.Format(@"<mapa CodigoMapa=""{0}"" TituloMapa=""{1}"" CodigoUnidadeNegocio=""{2}"" VersaoMapaEstrategico=""{3}"" DataInicioVersaoMapaEstatregico=""{4}"">" + Environment.NewLine, ds.Tables[0].Rows[0]["CodigoMapaEstrategico"].ToString()
                                    , ds.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString()
                                    , ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString()
                                    , ds.Tables[0].Rows[0]["VersaoMapaEstrategico"].ToString()
                                    , ds.Tables[0].Rows[0]["DataInicioVersaoMapaEstrategico"].ToString());

            if (xmlMapa != "")
            {
                xmlMapaEstrategico += xmlMapa;
                ObtemXmlElementosMapas();
                obtemXmlParametrosSistema();
                xmlMapaEstrategico += "</mapa>";

                ////A continuação vo a indentar o xml
                ////fonte: http://usmanshaheen.wordpress.com/2009/05/12/c-indent-xml-string/
                MemoryStream w = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(w, Encoding.Unicode);

                XmlDocument document = new XmlDocument();
                document.LoadXml(xmlMapaEstrategico);
                xmlMapaEstrategico = document.InnerXml;
                writer.Formatting = Formatting.Indented;
                document.WriteContentTo(writer);

                writer.Flush();
                w.Seek(0L, SeekOrigin.Begin);

                StreamReader reader = new StreamReader(w);
                xmlMapaEstrategico = reader.ReadToEnd();

                gravaXmlMapaNaBase(xmlMapaEstrategico, codigoUsuarioResponsavel.ToString());
            }
        }
    }

    /// <summary>
    /// Nesta função, criara o XML dos Objetod de Estrategia, creando para cada objeto un 'set'.
    /// Cada 'set' tendrá os parametrôs necessários para redisenhar os elementos do mapa
    /// extratégico (cores, textos, etc.).
    /// </summary>
    private void ObtemXmlElementosMapas()
    {
        DataSet ds;
        string xmlObjetosMapa = "<dataSet>" + Environment.NewLine;
        int count = 1;

        //Objeto pai : Objeto Mapa
        ds = cDados.getObjetosMapaEstrategicoToXML(codigoMapa, codigoUsuarioResponsavel.ToString(), codigoEntidade.ToString(), "AND ob.CodigoTipoObjetoEstrategia = 1");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                xmlObjetosMapa += string.Format(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao ="""" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"">" + Environment.NewLine, dr["CodigoObjetoEstrategia"].ToString()
                               , dr["CodigoVersaoMapaEstrategico"].ToString()
                               , dr["TituloObjetoEstrategia"].ToString()
                               , dr["DescricaoObjetoEstrategia"].ToString()
                               , dr["CorFundoObjetoEstrategia"].ToString()
                               , dr["CorBordaObjetoEstrategia"].ToString()
                               , dr["CorFonteObjetoEstrategia"].ToString()
                               , dr["AlturaObjetoEstrategia"].ToString()
                               , dr["LarguraObjetoEstrategia"].ToString()
                               , dr["TopoObjetoEstrategia"].ToString()
                               , dr["EsquerdaObjetoEstrategia"].ToString()
                               , dr["CodigoObjetoEstrategiaDe"].ToString()
                               , dr["CodigoObjetoEstrategiaPara"].ToString()
                               , dr["IniciaisTipoObjeto"].ToString()
                               , dr["CodigoObjetoEstrategiaSuperior"].ToString()
                               );
            }
        }

        //todos os objetos filhos do Objeto Mapa.
        ds = cDados.getObjetosMapaEstrategicoToXML(codigoMapa, codigoUsuarioResponsavel.ToString(), codigoEntidade.ToString(), "AND ob.CodigoTipoObjetoEstrategia <> 1");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int nivel = getNivelOE(dr["EstruturaHierarquica"].ToString());

                xmlObjetosMapa += string.Format(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao ="""" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"">" + Environment.NewLine, dr["CodigoObjetoEstrategia"].ToString()
                               , dr["CodigoVersaoMapaEstrategico"].ToString()
                               , dr["TituloObjetoEstrategia"].ToString()
                               , dr["DescricaoObjetoEstrategia"].ToString()
                               , dr["CorFundoObjetoEstrategia"].ToString()
                               , dr["CorBordaObjetoEstrategia"].ToString()
                               , dr["CorFonteObjetoEstrategia"].ToString()
                               , dr["AlturaObjetoEstrategia"].ToString()
                               , dr["LarguraObjetoEstrategia"].ToString()
                               , dr["TopoObjetoEstrategia"].ToString()
                               , dr["EsquerdaObjetoEstrategia"].ToString()
                               , dr["CodigoObjetoEstrategiaDe"].ToString()
                               , dr["CodigoObjetoEstrategiaPara"].ToString()
                               , dr["IniciaisTipoObjeto"].ToString()
                               , dr["CodigoObjetoEstrategiaSuperior"].ToString()
                               );

                if (count == dt.Rows.Count)
                {
                    for (int i = 0; i < nivel; i++)
                        xmlObjetosMapa += @"</set>" + Environment.NewLine;
                }
                else
                {
                    int proximoNivel = count == 1 ? 1 : getNivelOE(dt.Rows[count]["EstruturaHierarquica"].ToString());

                    if (nivel > proximoNivel)
                    {
                        xmlObjetosMapa += @"</set>" + Environment.NewLine;
                        xmlObjetosMapa += @"</set>" + Environment.NewLine;
                    }
                    else
                    {
                        if (nivel == proximoNivel)
                        {
                            xmlObjetosMapa += @"</set>" + Environment.NewLine;
                        }
                    }
                }

                count++;
            }
        }
        xmlObjetosMapa += "</set></dataSet>" + Environment.NewLine;
        xmlMapaEstrategico += xmlObjetosMapa;
    }

    private int getNivelOE(string estrutura)
    {
        if (estrutura == "" || estrutura.Contains(".") == false)
        {
            return 1;
        }
        else
        {
            return estrutura.Split('.').Length;
        }
    }

    /// <summary>
    /// Nesta função, se criara a tag 'style' correspondiente ao estilo padrão definido no 
    /// sistema (tabela [ParametroConfiguracaoSistema]).
    /// 
    /// Observação
    /// Perspectiva1: Pessoas.
    /// Perspectiva2: Processos.
    /// Perspectiva3: Imagem e Mercado.
    /// Perspectiva4: Financiera.
    /// </summary>
    private void obtemXmlParametrosSistema()
    {
        DataTable dt;
        string xmlStyles = "";

        dt = cDados.getParametroSistemaToXML(codigoEntidade.ToString()).Tables[0];
        if (cDados.DataTableOk(dt))
        {
            xmlStyles += string.Format(@"
                          <styles>
                            <mapa>
                                <style>
                                    <borda></borda>
                                    <corFundo></corFundo>
                                </style>
                                <missao>
                                    <style>
                                          <corBorda>{0}</corBorda>
                                          <corFundo>{1}</corFundo>
                                          <corFonte>{2}</corFonte>
                                    </style>
                                </missao>
                                <visao>
                                    <style>
                                          <corBorda>{3}</corBorda>
                                          <corFundo>{4}</corFundo>
                                          <corFonte>{5}</corFonte>
                                    </style>
                                </visao>
                                <Perspectiva>
                                    <style>
                                        <corBorda>{6}</corBorda>
                                        <corFundo>{7}</corFundo>
                                        <corFonte>{8}</corFonte>
                                    </style>
                                    <objetivos>
                                        <style>
                                            <corBorda>{9}</corBorda>
                                            <corFundo>{10}</corFundo>
                                            <corFonte>{11}</corFonte>
                                        </style>
                                    </objetivos>
                                </Perspectiva>
                            </mapa>
                          </styles>
                        ", dt.Rows[0]["corBordaMissao"].ToString()
                         , dt.Rows[0]["corFundoMissao"].ToString()
                         , dt.Rows[0]["corFonteMissao"].ToString()
                         , dt.Rows[0]["corBordaVisao"].ToString()
                         , dt.Rows[0]["corFundoVisao"].ToString()
                         , dt.Rows[0]["corFonteVisao"].ToString()
                         , dt.Rows[0]["corBordaPerspectiva1"].ToString()
                         , dt.Rows[0]["corFundoPerspectiva1"].ToString()
                         , dt.Rows[0]["corFontePerspectiva1"].ToString()
                         , dt.Rows[0]["corBordaObjetivosPerspectiva1"].ToString()
                         , dt.Rows[0]["corFundoObjetivosPerspectiva1"].ToString()
                         , dt.Rows[0]["corFonteObjetivosPerspectiva1"].ToString()
                         );
            xmlMapaEstrategico += xmlStyles;
        }
    }

    //Genera SQL
    public void gravaXmlMapaNaBase(string xmlMapa, string codigoUsuario)
    {
        string codigoMapa = "";
        string sqlMapaEstrategico = "";
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(xmlMapa);

        //Gerar SQL da tabela [MapaEstrategico].
        XmlNodeList nodoMapa = xDoc.GetElementsByTagName("mapa");
        if (nodoMapa.Count > 0)
        {
            foreach (XmlElement nodo in nodoMapa)
            {
                if (nodo.GetAttribute("CodigoMapa") != "")
                {
                    codigoMapa = nodo.GetAttribute("CodigoMapa");
                    string tituloMapa = nodo.GetAttribute("TituloMapa");
                    string codigoUnidade = nodo.GetAttribute("CodigoUnidadeNegocio");
                    string versaoMapa = nodo.GetAttribute("VersaoMapaEstrategico");

                    sqlMapaEstrategico += string.Format(@"
                    -- Gerar sentencia SQL do MAPA.
                    UPDATE {0}.{1}.MapaEstrategico
                    SET
		                TituloMapaEstrategico = '{2}'
                    WHERE CodigoMapaEstrategico = {3}
                    AND CodigoUnidadeNegocio = {4}
                    ", cDados.getDbName()
                     , cDados.getDbOwner(), tituloMapa, codigoMapa, codigoUnidade);
                }
            }
        }

        //Gerar SQL dos Objetos Estratégicos do Mapa.
        XmlNodeList nodoObjetoEstrategico = xDoc.GetElementsByTagName("set");
        if (nodoObjetoEstrategico.Count > 0)
        {
            foreach (XmlElement nodo in nodoObjetoEstrategico)
            {
                string codigoOb = nodo.GetAttribute("id");
                string tipoOb = nodo.GetAttribute("tipo");
                string codigoTipoOb = cDados.getCodigoTipoObjeto(tipoOb);
                string versao = nodo.GetAttribute("versao");
                string titulo = nodo.GetAttribute("titulo");
                string texto = nodo.GetAttribute("texto");
                string esquerda = nodo.GetAttribute("esquerda");
                string topo = nodo.GetAttribute("topo");
                string largura = nodo.GetAttribute("largura");
                string altura = nodo.GetAttribute("altura");
                string rotacao = nodo.GetAttribute("rotacao");
                string numBorda = nodo.GetAttribute("numBorda");
                string corFundo = nodo.GetAttribute("corFundo");
                string corBorda = nodo.GetAttribute("corBorda");
                string corTexto = nodo.GetAttribute("corTexto");
                string origem = nodo.GetAttribute("origem");
                string destino = nodo.GetAttribute("destino");
                string codigoObjetoSuperior = nodo.GetAttribute("codigoObjetoSuperior");

                sqlMapaEstrategico += string.Format(@"
                -- Gerar sentencia SQL do Objetos Estratégicos.

                if ( not exists (Select 1 from {0}.{1}.ObjetoEstrategia WHERE CodigoObjetoEstrategia = {2} ))

                    INSERT INTO {0}.{1}.ObjetoEstrategia (CodigoMapaEstrategico, CodigoVersaoMapaEstrategico, CodigoTipoObjetoEstrategia, OrdemObjeto, DataInclusao, CodigoUsuarioInclusao)
                    VALUES ( {3}, {16}, {14}, 1, GETDATE(), {15}) 

                UPDATE {0}.{1}.ObjetoEstrategia
                SET   TituloObjetoEstrategia    = '{4}'
                    , DescricaoObjetoEstrategia = '{5}'
                    , AlturaObjetoEstrategia    = {6}
                    , LarguraObjetoEstrategia   = {7}
                    , TopoObjetoEstrategia      = {8}
                    , EsquerdaObjetoEstrategia  = {9}
                    , CorFundoObjetoEstrategia  = '{10}'
                    , CorBordaObjetoEstrategia  = '{11}'
                    , CorFonteObjetoEstrategia  = '{12}'
                    , CodigoTipoObjetoEstrategia = {13}
                    , OrdemObjeto = 1
                    , CodigoObjetoEstrategiaSuperior = {14}
                    , DataUltimaAlteracao = GETDATE()
                    , CodigoUsuarioUltimaAlteracao = {15}
                WHERE CodigoObjetoEstrategia = {2}
                  AND CodigoMapaEstrategico = {3}
                ", cDados.getDbName()
                 , cDados.getDbOwner(), codigoOb, codigoMapa
                 , titulo, texto, altura, largura, topo, esquerda
                 , corFundo, corBorda, corTexto, codigoTipoOb
                 , codigoObjetoSuperior != "" ? codigoObjetoSuperior : "NULL"
                 , codigoUsuario, versao
                 );
            }
        }
        sqlMapaEstrategico += " --- by Alejandro.";
    }

    #endregion

}