using CDIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Xml;


/// <summary>
/// Summary description for wsPortal
/// </summary>
[WebService(Namespace = "http://www.cdis.com.br/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class wsPortal : System.Web.Services.WebService
{
    string _key = "#COMANDO#CDIS!";
    string diretorioCronogramas; // determinia o local onde os cronogramas enviados pelo cliente desktop serão salvos
    bool salvarHistoricoXMLCronogramaMapaEstrategicoEmDisco = true; // Determinas se o XML Cronograma/MapaEstratégio será salvo em disco
    private string PathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
    private string IDProduto = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();
    private string tipoBancoDados = System.Configuration.ConfigurationManager.AppSettings["tipoBancoDados"].ToString();
    private string Ownerdb = System.Configuration.ConfigurationManager.AppSettings["dbOwner"].ToString();
    dados cDados;
    ClasseDados classeDados;
    private string bancodb = string.Empty;
    private string ownerdb = string.Empty;

    public wsPortal()
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["WSPortal"] = "WSPortal";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        // A classe dados foi incluída para ser utilizada ao salvar o mapa estratégico
        classeDados = new ClasseDados(tipoBancoDados, PathDB, IDProduto, Ownerdb, "", 2);

        bancodb = cDados.getDbName();
        ownerdb = cDados.getDbOwner();

        // se não existir um local definido no Webconfig, assume o local padrão
        if (System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"] != null && System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"].ToString() != "")
            diretorioCronogramas = System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"].ToString();
        else
            diretorioCronogramas = @"C:\CDIS_PortalEstrategia\Cronogramas";

        // verifica se existe no web.config, um controle determinando se o cronograma/Mapa deve ser salvo em disco.
        if (System.Configuration.ConfigurationManager.AppSettings["salvarHistoricoCronogramasEmDisco"] != null && System.Configuration.ConfigurationManager.AppSettings["salvarHistoricoCronogramasEmDisco"].ToString() == "N")
            salvarHistoricoXMLCronogramaMapaEstrategicoEmDisco = false;

    }

    #region ===== Edição de EAP =====

    // Codifica para base 64
    static public string EncodeTo64(string toEncode)
    {
        byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
        string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
    }

    // Decodifica de base 64
    static public string DecodeFrom64(string encodedData)
    {
        byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
        string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        return returnValue;
    }

    [WebMethod]
    public string loadData(string idEdicaoEap)
    {
        string xml = "";
        if (idEdicaoEap != "")
        {
            xml = getXmlEapProjeto(idEdicaoEap); // string teste = getUsuarioToXml(idEdicaoEap);
        }
        return xml;
    }

    public class Tarefas
    {
        private string _nombre;
        /// <summary>
        /// Nombre de la tarea.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private int _nivel;
        /// <summary>
        /// Nivel de la tarea.
        /// </summary>
        public int Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        private int _codigoTarea;
        /// <summary>
        /// Codigo de la Tarea
        /// </summary>
        public int CodigoTarea
        {
            get { return _codigoTarea; }
            set { _codigoTarea = value; }
        }

        private string _nombreProjeto;
        /// <summary>
        /// Nombre del Projeto.
        /// </summary>
        public string NombreProjeto
        {
            get { return _nombreProjeto; }
            set { _nombreProjeto = value; }
        }

        private string _idTarefa;
        /// <summary>
        /// ID da tarea.
        /// </summary>
        public string IDTarefa
        {
            get { return _idTarefa; }
            set { _idTarefa = value; }
        }

        private string _xmlEstiloEap;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string XmlEstiloEap
        {
            get { return _xmlEstiloEap; }
            set { _xmlEstiloEap = value; }
        }

        private string _codigoEntidade;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string CodigoEntidade
        {
            get { return _codigoEntidade; }
            set { _codigoEntidade = value; }
        }

        private string _inicio;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string Inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }

        private string _termino;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string Termino
        {
            get { return _termino; }
            set { _termino = value; }
        }

        private string _duracao;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string Duracao
        {
            get { return _duracao; }
            set { _duracao = value; }
        }

        private string _trabalho;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string Trabalho
        {
            get { return _trabalho; }
            set { _trabalho = value; }
        }

        private string _custo;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string Custo
        {
            get { return _custo; }
            set { _custo = value; }
        }

        private string _codigoUsuarioResponsavel;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string CodigoUsuarioResponsavel
        {
            get { return _codigoUsuarioResponsavel; }
            set { _codigoUsuarioResponsavel = value; }
        }

        private string _anotacoes;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string Anotacoes
        {
            get { return _anotacoes; }
            set { _anotacoes = value; }
        }

        private string _nomeResponsavel;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string NomeResponsavel
        {
            get { return _nomeResponsavel; }
            set { _nomeResponsavel = value; }
        }

        public Tarefas(string nombre, int nivel, int codigoTarea)
        {
            this.Nombre = nombre;
            this.Nivel = nivel;
            this.CodigoTarea = codigoTarea;
        }

        public Tarefas(string nombreTarefa, int nivel, int codigoTarea, string nomeProjeto, string idTarefa, string xmlEstiloEap)
        {
            this.Nombre = nombreTarefa;
            this.Nivel = nivel;
            this.CodigoTarea = codigoTarea;
            this.NombreProjeto = nomeProjeto;
            this.IDTarefa = idTarefa;
            this.XmlEstiloEap = xmlEstiloEap;

        }

        public Tarefas(string nombreTarefa, int nivel, int codigoTarea, string nomeProjeto, string idTarefa, string xmlEstiloEap
                      , string codigoEntidade, string inicio, string termino, string duracao, string trabalho, string custo
                      , string codigoUsuarioResponsavel, string anotacoes, string nomeResponsavel)
        {
            this.Nombre = nombreTarefa;
            this.Nivel = nivel;
            this.CodigoTarea = codigoTarea;
            this.NombreProjeto = nomeProjeto;
            this.IDTarefa = idTarefa;
            this.XmlEstiloEap = xmlEstiloEap;
            this.CodigoEntidade = codigoEntidade;
            this.Inicio = inicio;
            this.Termino = termino;
            this.Duracao = duracao;
            this.Trabalho = trabalho;
            this.Custo = custo;
            this.CodigoUsuarioResponsavel = CodigoUsuarioResponsavel;
            this.Anotacoes = anotacoes;
            this.NomeResponsavel = nomeResponsavel;
        }
    }

    public class Cronograma
    {
        private string _nombreTarefa;
        /// <summary>
        /// Nombre de la tarea.
        /// </summary>
        public string NombreTarefa
        {
            get { return _nombreTarefa; }
            set { _nombreTarefa = value; }
        }

        private int _nivel;
        /// <summary>
        /// Nivel de la tarea.
        /// </summary>
        public int Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        private int _codigoTarea;
        /// <summary>
        /// Codigo de la Tarea
        /// </summary>
        public int CodigoTarea
        {
            get { return _codigoTarea; }
            set { _codigoTarea = value; }
        }

        private string _nombreProjeto;
        /// <summary>
        /// Nombre del Projeto.
        /// </summary>
        public string NombreProjeto
        {
            get { return _nombreProjeto; }
            set { _nombreProjeto = value; }
        }

        private string _idTarefa;
        /// <summary>
        /// ID da tarea.
        /// </summary>
        public string IDTarefa
        {
            get { return _idTarefa; }
            set { _idTarefa = value; }
        }

        private string _xmlEstiloEap;
        /// <summary>
        /// Estilo do Xml da tarea.
        /// </summary>
        public string XmlEstiloEap
        {
            get { return _xmlEstiloEap; }
            set { _xmlEstiloEap = value; }
        }

        public Cronograma(string nombreTarefa, int nivel, int codigoTarea, string nomeProjeto, string idTarefa, string xmlEstiloEap)
        {
            this.NombreTarefa = nombreTarefa;
            this.Nivel = nivel;
            this.CodigoTarea = codigoTarea;
            this.NombreProjeto = nomeProjeto;
            this.IDTarefa = idTarefa;
            this.XmlEstiloEap = xmlEstiloEap;

        }
    }

    /// <summary>
    /// método para criar a estructura XML generando a lista de usuario como aceso ao projeto.
    /// </summary>
    /// <returns>retorna STRING</returns>
    private string getUsuarioToXml(string idEdicaoEap)
    {
        string xmlUsuario = "";
        string comandoSQL = string.Format(@"
                BEGIN
                DECLARE @CodigoProjeto INT
                DECLARE @CodigoAcesso  VARCHAR(2)

                SELECT @CodigoAcesso = ceap.[ModoAcesso],
                       @CodigoProjeto = ceap.[CodigoProjeto]
                FROM    {0}.{1}.[ControleEdicaoEAP]                    AS [ceap]
                WHERE
                        ceap.IDEdicaoEap    = '{2}'

                SELECT @CodigoAcesso ModoAcesso, Codigo, Descricao  FROM {0}.{1}.f_GetInteressadosProjeto (@CodigoProjeto)
                END
            ", bancodb, ownerdb, idEdicaoEap);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            xmlUsuario = @"
                        <UsuariosInteresados>";
            string modoAcesso = ds.Tables[0].Rows[0]["ModoAcesso"].ToString();
            //recorriendo las tareas del cronograma.
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                string codigoUsuario = row["Codigo"].ToString();
                string nomeUsuario = row["Descricao"].ToString();

                xmlUsuario += string.Format(@"
                            <usuario>
                                <codigo>{0}</codigo>
                                <nome>{1}</nome>
                            </usuario>", codigoUsuario, nomeUsuario);
            }

            xmlUsuario += string.Format(@"
                        </UsuariosInteresados>
                        <modoVisualiza>{0}</modoVisualiza>", modoAcesso);

        }
        return xmlUsuario;
        //hfGeral.Set("XML", generandoXML);
        //System.Diagnostics.Debug.WriteLine(hfGeral.Get("XML").ToString());
    }

    private string getXmlEapProjeto(string idEdicaoEap)
    {
        List<Tarefas> tarefas = new List<Tarefas>();

        string generandoXML = "<?xml version='1.0' encoding='UTF-8' ?>";
        string estiloPadrao = @" <Style><fillType>linear</fillType> 
            <fontName>Verdana</fontName> 
            <fontSize>10</fontSize> 
            <fontUnderline>false</fontUnderline> 
            <fontItalic>false</fontItalic> 
            <fontBold>false</fontBold> 
            <fontColor>0</fontColor> 
            <cornerRadius>0</cornerRadius> 
            <brdColor>3693962</brdColor> 
            <brdSize>2</brdSize> 
            <brdAlpha>1</brdAlpha> 
            <fillRotation>90</fillRotation> 
            <childVerticalSpace>40</childVerticalSpace> 
            <childHorizontalSpace>40</childHorizontalSpace> 
            <textTopOffset>7</textTopOffset> 
            <textLeftOffset>3</textLeftOffset> 
            <textBottomOffset>9</textBottomOffset> 
            <textRightOffset>3</textRightOffset> 
            <childPositionMethod>0</childPositionMethod> 
            <connectorL>false</connectorL> 
            <dropShadow>true</dropShadow> 
            <FillSteps>
                <Step>
                    <color>16777215</color> 
                    <alpha>1</alpha> 
                    <position>0</position> 
                </Step>
            </FillSteps>
 </Style>
        ";

        //Pegando as tarefas do cronograma.
        string comandoSQL = string.Format(@"
        BEGIN
      DECLARE @MostraTarefasNaEAP  Char(1)
      
      SET @MostraTarefasNaEAP = 'S'
      
      SELECT TOP 1 @MostraTarefasNaEAP = ISNULL(pcs.Valor,'N')
        FROM ParametroConfiguracaoSistema AS pcs INNER JOIN
             Projeto AS p ON (p.CodigoEntidade = pcs.CodigoEntidade) INNER JOIN
             ControleEdicaoEAP AS cea ON (cea.CodigoProjeto = p.CodigoProjeto
                                      AND cea.IDEdicaoEAP = '{2}')
      WHERE pcs.Parametro = 'MostraTarefasNaEAP'

    IF @MostraTarefasNaEAP = 'N'
            
            SELECT t.CodigoTarefa, 
                     t.NomeTarefa, 
                     t.Nivel, 
                     t.IDTarefa, 
                     cp.NomeProjeto, 
                     ISNULL (t.XmlEstiloEap, '' ) AS XmlEstiloEap, 
                     cp.CodigoEntidade, 
                     t.Inicio, 
                     t.Termino, 
                     t.Duracao, 
                     t.Trabalho, 
                     t.Custo, 
                     t.CodigoUsuarioResponsavel, 
                     t.Anotacoes , 
                     usu.NomeUsuario
              FROM {0}.{1}.ControleEdicaoEap ceap
                     INNER JOIN {0}.{1}.CronogramaProjeto cp on (cp.CodigoProjeto = ceap.CodigoProjeto)
                     INNER JOIN {0}.{1}.TarefaCronogramaProjeto t on (t.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto)
                     LEFT JOIN {0}.{1}.Usuario usu on (t.CodigoUsuarioResponsavel = usu.CodigoUsuario)
            WHERE ceap.IDEdicaoEap    = '{2}'
               AND t.IndicaEAP  = 'S'   
               AND t.DataExclusao IS NULL
        ORDER BY t.SequenciaTarefaCronograma
      ELSE
        SELECT t.CodigoTarefa, 
                   t.NomeTarefa, 
                   t.Nivel, 
                   t.IDTarefa, 
                   cp.NomeProjeto, 
                   ISNULL (t.XmlEstiloEap, '' ) AS XmlEstiloEap, 
                   cp.CodigoEntidade, 
                   t.Inicio, 
                   t.Termino, 
                   t.Duracao, 
                   t.Trabalho, 
                   t.Custo, 
                   t.CodigoUsuarioResponsavel, 
                   t.Anotacoes , 
                   usu.NomeUsuario
        FROM {0}.{1}.ControleEdicaoEap ceap
             INNER JOIN {0}.{1}.CronogramaProjeto cp on (cp.CodigoProjeto = ceap.CodigoProjeto)
             INNER JOIN {0}.{1}.TarefaCronogramaProjeto t on (t.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto)
             LEFT JOIN {0}.{1}.Usuario usu on (t.CodigoUsuarioResponsavel = usu.CodigoUsuario)
       WHERE ceap.IDEdicaoEap    = '{2}'          
         AND t.DataExclusao IS NULL
    ORDER BY t.SequenciaTarefaCronograma
    
END


        ", bancodb, ownerdb, idEdicaoEap);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (ds != null && ds.Tables.Count > 0)
        {
            //recorriendo las tareas del cronograma.
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                string estiloPadraoEAP = "";
                if (row["XmlEstiloEap"].ToString().Equals(""))
                    estiloPadraoEAP = estiloPadrao;
                else
                    estiloPadraoEAP = row["XmlEstiloEap"].ToString();


                //inicializar la tarea, con sus propiedades.
                Tarefas tarefa = new Tarefas(row["NomeTarefa"].ToString(), int.Parse(row["Nivel"].ToString()), int.Parse(row["CodigoTarefa"].ToString())
                                           , row["NomeProjeto"].ToString(), row["IDTarefa"].ToString(), estiloPadraoEAP
                                           , row["CodigoEntidade"].ToString(), row["Inicio"].ToString(), row["Termino"].ToString()
                                           , row["Duracao"].ToString(), row["Trabalho"].ToString(), row["Custo"].ToString()
                                           , row["CodigoUsuarioResponsavel"].ToString(), row["Anotacoes"].ToString()
                                           , row["NomeUsuario"].ToString());

                //si la lista esta vacia, entonces, voy a colocar el primer elemento.
                if (tarefas.Count == 0)
                {
                    //tarefas.Add(new Tarefas(row["NomeTarefa"].ToString(), int.Parse(row["Nivel"].ToString()), int.Parse(row["CodigoTarefa"].ToString())));
                    tarefas.Add(new Tarefas(tarefa.Nombre, tarefa.Nivel, tarefa.CodigoTarea));
                    generandoXML += string.Format(@"
                    <WBSElement>
                        <name>{0}</name>", tarefa.NombreProjeto); // row["NomeProjeto"].ToString());

                    //if (row["IDTarefa"].ToString().Length > 0)
                    if (tarefa.IDTarefa.ToString().Length > 0)
                        generandoXML += string.Format(@"
                        <GUID>{0}</GUID>", tarefa.IDTarefa);

                    //if (row["XmlEstiloEap"].ToString().Length > 0)
                    if (tarefa.XmlEstiloEap.ToString().Length > 0)
                        generandoXML += string.Format(@"
                        {0}", tarefa.XmlEstiloEap);
                    else
                        generandoXML += string.Format(@"
                       {0}", estiloPadrao);

                    //Datos auxiliares de cada tarefa.
                    generandoXML += string.Format(@"
                        <inicio>{1}</inicio>
                        <termino>{2}</termino>
                        <duracao>{3}</duracao>
                        <trabalho>{4}</trabalho>
                        <custo>{5}</custo>
                        <codigoUsuarioResponsavel>{6}</codigoUsuarioResponsavel>
                        <nomeUsuario>{7}</nomeUsuario>
                        <anotacoes>{8}</anotacoes>", tarefa.CodigoEntidade, tarefa.Inicio, tarefa.Termino, tarefa.Duracao, tarefa.Trabalho
                     , tarefa.Custo, tarefa.CodigoUsuarioResponsavel, tarefa.NomeResponsavel, tarefa.Anotacoes); // row["NomeProjeto"].ToString());

                    generandoXML += getUsuarioToXml(idEdicaoEap);
                }
                else // caso que la lista ya tenga una o varias tareas, voy analizar esas tareas con la actual de la lista.
                {
                    //Tarefas tarefaAux = new Tarefas(row["NomeTarefa"].ToString(), int.Parse(row["Nivel"].ToString()), int.Parse(row["CodigoTarefa"].ToString()));
                    Tarefas tarefaAux = new Tarefas(tarefa.Nombre, tarefa.Nivel, tarefa.CodigoTarea);
                    int nivel = tarefaAux.Nivel; //int.Parse(row["Nivel"].ToString());
                    int limiteSup = tarefas.Count - 1;

                    for (int i = limiteSup; i >= 0; i--)
                    {
                        string nameTarefa = tarefaAux.Nombre;       //row["NomeTarefa"].ToString();
                        string guidTarefa = tarefa.IDTarefa;    //row["IDTarefa"].ToString();
                        string styleTarefa = tarefa.XmlEstiloEap; //row["XmlEstiloEap"].ToString();

                        if (styleTarefa == null || styleTarefa == "")
                            styleTarefa = estiloPadrao;

                        if (tarefas[i].Nivel < nivel)
                        {
                            tarefas.Add(tarefaAux);
                            generandoXML += string.Format(@"
                            <WBSElement>
                                <name>{0}</name>
                                <GUID>{1}</GUID>
                                {2}", nameTarefa, guidTarefa, styleTarefa);

                            generandoXML += string.Format(@"
                                <inicio>{1}</inicio>
                                <termino>{2}</termino>
                                <duracao>{3}</duracao>
                                <trabalho>{4}</trabalho>
                                <custo>{5}</custo>
                                <codigoUsuarioResponsavel>{6}</codigoUsuarioResponsavel>
                                <nomeUsuario>{7}</nomeUsuario>
                                <anotacoes>{8}</anotacoes>", tarefa.CodigoEntidade, tarefa.Inicio, tarefa.Termino, tarefa.Duracao, tarefa.Trabalho
                             , tarefa.Custo, tarefa.CodigoUsuarioResponsavel, tarefa.NomeResponsavel, tarefa.Anotacoes); // row["NomeProjeto"].ToString());

                            break;
                        }
                        else if (tarefas[i].Nivel == nivel)
                        {
                            generandoXML += string.Format(@"
                            </WBSElement>
                            <WBSElement>
                                <name>{0}</name>
                                <GUID>{1}</GUID>
                                {2}", nameTarefa, guidTarefa, styleTarefa);

                            generandoXML += string.Format(@"
                                <inicio>{1}</inicio>
                                <termino>{2}</termino>
                                <duracao>{3}</duracao>
                                <trabalho>{4}</trabalho>
                                <custo>{5}</custo>
                                <codigoUsuarioResponsavel>{6}</codigoUsuarioResponsavel>
                                <nomeUsuario>{7}</nomeUsuario>
                                <anotacoes>{8}</anotacoes>", tarefa.CodigoEntidade, tarefa.Inicio, tarefa.Termino, tarefa.Duracao, tarefa.Trabalho
                             , tarefa.Custo, tarefa.CodigoUsuarioResponsavel, tarefa.NomeResponsavel, tarefa.Anotacoes); // row["NomeProjeto"].ToString());

                            break;
                        }
                        else if (tarefas[i].Nivel > nivel)
                        {
                            tarefas.RemoveAt(i);
                            generandoXML += @"
                            </WBSElement>
                            ";
                        }
                    }
                }
            }
            if (tarefas.Count > 0)
            {
                int limiteSup = tarefas.Count - 1;
                for (int i = limiteSup; i >= 0; i--)
                {
                    tarefas.RemoveAt(i);
                    generandoXML += @"
                    </WBSElement>
                    ";
                }
            }
        }
        return generandoXML;
        //hfGeral.Set("XML", generandoXML);
        //System.Diagnostics.Debug.WriteLine(hfGeral.Get("XML").ToString());
    }

    [WebMethod]
    public string saveData(string idEdicaoEap, string projectXML)
    {
        string retorno = "";
        if (PodeAlterarEap(idEdicaoEap))
        {
            gravaEapNaBaseDeDados(idEdicaoEap, projectXML); //DecodeFrom64(projectXML));
            retorno = "OK";
        }
        else
        {
            gravaStyleEapNaBaseDeDados(projectXML);
        }
        return retorno;
    }

    private bool PodeAlterarEap(string idEdicaoEap)
    {
        bool podeSalbar = false;
        object CodigoCronogramaProjeto;
        object DataUltimaGravacaoDesktop;
        object CodigoUsuarioCheckoutCronograma;
        object DataCheckoutCronograma;
        object DataInicioEdicao;
        object CodigoUsuarioEdicao;
        string ModoAcesso;

        string commandoSQL = string.Format(@"
            DECLARE @idEdicaoEap      VARCHAR(64)
            DECLARE @CodigoProjeto  INT

            SET @idEdicaoEap = '{2}'

            SET @CodigoProjeto = (SELECT CodigoProjeto FROM {0}.{1}.ControleEdicaoEap WHERE IDEdicaoEap = @idEdicaoEap)

            SELECT CP.CodigoCronogramaProjeto
                ,CP.DataUltimaGravacaoDesktop
                ,CP.CodigoUsuarioCheckoutCronograma
                ,CP.DataCheckoutCronograma
                ,CEAP.DataInicioEdicao
                ,CEAP.CodigoUsuarioEdicao
                ,CEAP.ModoAcesso
            FROM 
                {0}.{1}.ControleEdicaoEap CEAP 
                    INNER JOIN {0}.{1}.CronogramaProjeto CP  ON 
                            (CP.CodigoProjeto = CEAP.CodigoProjeto)   

            WHERE
                    CEAP.[IDEdicaoEap]  =  @idEdicaoEap
            ", cDados.getDbName(), cDados.getDbOwner(), idEdicaoEap);

        DataSet ds = cDados.getDataSet(commandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            System.Data.DataRow row = ds.Tables[0].Rows[0];
            CodigoCronogramaProjeto = row["CodigoCronogramaProjeto"];
            DataUltimaGravacaoDesktop = row["DataUltimaGravacaoDesktop"];
            CodigoUsuarioCheckoutCronograma = row["CodigoUsuarioCheckoutCronograma"];
            DataCheckoutCronograma = row["DataCheckoutCronograma"];
            DataInicioEdicao = row["DataInicioEdicao"];
            CodigoUsuarioEdicao = row["CodigoUsuarioEdicao"];
            ModoAcesso = row["ModoAcesso"].ToString();

            if ((DataUltimaGravacaoDesktop == System.DBNull.Value) && ModoAcesso.Equals("G")
                 && (CodigoUsuarioEdicao.Equals(CodigoUsuarioCheckoutCronograma))
                 && (DataInicioEdicao.Equals(DataCheckoutCronograma)))
                podeSalbar = true;
        }
        return podeSalbar;
    }

    private void gravaStyleEapNaBaseDeDados(string XMLtela)
    {
        string comandoSQL = "";

        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(XMLtela);

        XmlNodeList wbsElement = xDoc.GetElementsByTagName("WBSElement");
        if (wbsElement.Count > 0)
        {
            foreach (XmlElement nodo in wbsElement)
            {
                string varXmlGuidTarefa = "";
                string varXmlChildren = "";
                string varXmlStyle = "";

                XmlNode nGuid = nodo.SelectSingleNode("GUID");
                varXmlGuidTarefa = nGuid.InnerText;
                XmlNode nStyle = nodo.SelectSingleNode("showChildren");
                varXmlChildren = nStyle.InnerXml;
                XmlNode nStyleAux = nodo.SelectSingleNode("Style");
                varXmlStyle = nStyleAux.InnerXml;

                comandoSQL += string.Format(@"
                UPDATE {0}.{1}.TarefaCronogramaProjeto
                SET 
                      [DataUltimaAlteracao] = GETDATE()
                    , [XmlEstiloEap]        = '{2}'
                WHERE [IDTarefa] = '{3}'
                ", bancodb, ownerdb
                 , "<showChildren>" + varXmlChildren + "</showChildren><Style>" + varXmlStyle + "</Style>"
                 , varXmlGuidTarefa);
                //-----------------------------------------------------------
                //System.Diagnostics.Debug.WriteLine(comandoSQL);
            }

            int regAfetados = 0;
            cDados.execSQL(comandoSQL, ref regAfetados);
        }

    }

    private void gravaEapNaBaseDeDados(string idEdicaoEap, string XMLtela)
    {
        DataRow enTareaEap;
        DataRow enCronogramaProjeto;// = new DataRow();
        int idPai = 0;
        int nivelPai = -1;
        int nivelTarefa = 0;
        int codigoTarefa = 1;
        int secuenciaTarefa = 0;
        string comandoSQL = string.Format(@"
                --1ro) Apago o registros do conograma do projeto.
                
                DECLARE @CodigoCronogramaProjeto VARCHAR(64)
                
                SELECT
                    @CodigoCronogramaProjeto = cp.[CodigoCronogramaProjeto]
                FROM {0}.{1}.[ControleEdicaoEap]	AS [ceap]
                    INNER JOIN {0}.{1}.[CronogramaProjeto]	AS [cp] ON (cp.[CodigoProjeto] = ceap.[CodigoProjeto])   
                WHERE ceap.[IDEdicaoEap]  =  '{2}'

                DELETE {0}.{1}.[TarefaCronogramaProjeto] WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto 

                --2do) Insero o nosvos registros fazendo referencia do disenho para la tabela TarefaCronogramaProjeto.
                ", bancodb, ownerdb, idEdicaoEap);


        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(XMLtela);

        XmlNodeList wbsElement = xDoc.GetElementsByTagName("WBSElement");
        if (wbsElement.Count > 0)
        {
            foreach (XmlElement nodo in wbsElement)
            {
                string varNomeTarefa = "";
                string varGuidTarefa = "";
                string varCodigoTarefa = "";
                string varCodigoSupTarefa = "";
                string varSecuenciaTarefa = "";
                string varNivelTarefa = "";
                string varFilho = "";
                string varXmlStyle = "";
                string varXmlStyleAux = "";
                int i = 0;

                string inicioTarefa = "";
                string terminoTarefa = "";
                //string terminoTarefaPadrao = "";
                string duracaoTarefa = "";
                string trabalhoTarefa = "";
                string custoTarefa = "";
                string IndicaTarefaResumoCronograma = "N";
                string IndicaInicioFixado = "";
                string IndicaTerminoFixado = "";
                string anotacoes = "";
                string usuarioResponsavel = "";
                DateTime d;

                XmlNode newAtt = xDoc.CreateNode(XmlNodeType.Element, "codigoTarefa", "");
                newAtt.InnerXml = string.Format("{0}", codigoTarefa++);
                nodo.AppendChild(newAtt);

                newAtt = xDoc.CreateNode(XmlNodeType.Element, "secuenciaTarefa", "");
                newAtt.InnerXml = string.Format("{0}", secuenciaTarefa++);
                nodo.AppendChild(newAtt);

                if ((secuenciaTarefa - 1) == 0)
                    IndicaTarefaResumoCronograma = "S";
                else
                    IndicaTarefaResumoCronograma = "N";

                newAtt = xDoc.CreateNode(XmlNodeType.Element, "nivel", "");
                if (nivelPai != -1)
                {
                    //saber si tem pai
                    XmlNode pai = nodo.ParentNode;
                    XmlNode nodoNivelPai = pai.SelectSingleNode("nivel");
                    XmlNode nodoIdPai = pai.SelectSingleNode("codigoTarefa");
                    nivelPai = int.Parse(nodoNivelPai.InnerText);
                    idPai = int.Parse(nodoIdPai.InnerText);
                }
                nivelTarefa = ++nivelPai;
                newAtt.InnerXml = nivelTarefa.ToString();
                nodo.AppendChild(newAtt);

                newAtt = xDoc.CreateNode(XmlNodeType.Element, "tarefaSuperior", "");
                newAtt.InnerXml = idPai.ToString();
                nodo.AppendChild(newAtt);

                XmlNodeList nNome = nodo.GetElementsByTagName("name"); varNomeTarefa = nNome[i].InnerText;
                XmlNodeList nGuid = nodo.GetElementsByTagName("GUID"); varGuidTarefa = nGuid[i].InnerText;
                XmlNodeList nCodTa = nodo.GetElementsByTagName("codigoTarefa"); varCodigoTarefa = nCodTa[i].InnerText;
                XmlNodeList nSecTa = nodo.GetElementsByTagName("secuenciaTarefa"); varSecuenciaTarefa = nSecTa[i].InnerText;
                XmlNodeList nNivTa = nodo.GetElementsByTagName("nivel"); varNivelTarefa = nNivTa[i].InnerText;
                XmlNodeList nCodSupTa = nodo.GetElementsByTagName("tarefaSuperior"); varCodigoSupTarefa = nCodSupTa[i].InnerText;


                XmlNodeList nFilho = nodo.GetElementsByTagName("WBSElement");
                if (nFilho[i] != null)
                    varFilho = nFilho[i].InnerText;
                else
                    varFilho = "";

                XmlNodeList nStyle = nodo.GetElementsByTagName("showChildren"); varXmlStyle = nStyle[i].InnerXml;
                XmlNode nStyleAux = nodo.SelectSingleNode("Style"); varXmlStyleAux = nStyleAux.InnerXml;
                i++;

                //Verifico si o GUID esta na tabela TarefaEap. Caso que exista, será os dados a utilzar pra prencher o elemento
                //grafico.
                if (ExisteGuidNaTarefaEap(idEdicaoEap, varGuidTarefa, out enTareaEap))
                {
                    if (enTareaEap["Inicio"] != null && enTareaEap["Inicio"].ToString() != "")
                    {
                        inicioTarefa = enTareaEap["Inicio"].ToString();
                        IndicaInicioFixado = "S";
                    }
                    else
                    {
                        IndicaInicioFixado = "N";
                    }

                    if (enTareaEap["Termino"] != null && enTareaEap["Termino"].ToString() != "")
                    {
                        terminoTarefa = enTareaEap["Termino"].ToString();
                        IndicaTerminoFixado = "S";
                    }
                    else
                    {
                        IndicaTerminoFixado = "N";
                    }

                    if (enTareaEap["Duracao"] != null && enTareaEap["Duracao"].ToString() != "")
                        duracaoTarefa = enTareaEap["Duracao"].ToString().Replace(",", ".");
                    else
                        duracaoTarefa = "1";

                    if (enTareaEap["Trabalho"] != null && enTareaEap["Trabalho"].ToString() != "")
                        trabalhoTarefa = enTareaEap["Trabalho"].ToString().Replace(",", ".");
                    else
                        trabalhoTarefa = "0";

                    if (enTareaEap["Custo"] != null && enTareaEap["Custo"].ToString() != "")
                        custoTarefa = enTareaEap["Custo"].ToString().Replace(",", ".");
                    else
                        custoTarefa = "0";

                    if (enTareaEap["Anotacoes"] != null && enTareaEap["Anotacoes"].ToString() != "")
                        anotacoes = "'" + enTareaEap["Anotacoes"].ToString().Replace(",", ".") + "'";
                    else
                        anotacoes = "NULL";

                    usuarioResponsavel = enTareaEap["CodigoUsuarioResponsavel"].ToString().Replace(",", ".");

                } // if (ExisteGuidNaTarefaEap(idEdicaoEap, varGuidTarefa, ref enTareaEap))...

                else if (ExisteGuidNaTarefaCronogramaProjeto(idEdicaoEap, varGuidTarefa, out enCronogramaProjeto))
                {
                    if (enCronogramaProjeto["Inicio"] != null && enCronogramaProjeto["Inicio"].ToString() != "")
                    {
                        inicioTarefa = enCronogramaProjeto["Inicio"].ToString();

                    }
                    if (enCronogramaProjeto["IndicaInicioFixado"] != null && enCronogramaProjeto["IndicaInicioFixado"].ToString() != "")
                        IndicaInicioFixado = enCronogramaProjeto["IndicaInicioFixado"].ToString();

                    if (enCronogramaProjeto["IndicaTerminoFixado"] != null && enCronogramaProjeto["IndicaTerminoFixado"].ToString() != "")
                        IndicaTerminoFixado = enCronogramaProjeto["IndicaInicioFixado"].ToString();

                    if (enCronogramaProjeto["Termino"] != null && enCronogramaProjeto["Termino"].ToString() != "")
                    {
                        terminoTarefa = enCronogramaProjeto["Termino"].ToString();
                        IndicaTerminoFixado = "S";
                    }
                    else
                    {
                        IndicaTerminoFixado = "N";
                    }

                    if (enCronogramaProjeto["Duracao"] != null && enCronogramaProjeto["Duracao"].ToString() != "")
                        duracaoTarefa = enCronogramaProjeto["Duracao"].ToString().Replace(",", ".");
                    else
                        duracaoTarefa = "1";

                    if (enCronogramaProjeto["Trabalho"] != null && enCronogramaProjeto["Trabalho"].ToString() != "")
                        trabalhoTarefa = enCronogramaProjeto["Trabalho"].ToString().Replace(",", ".");
                    else
                        trabalhoTarefa = "0";

                    if (enCronogramaProjeto["Custo"] != null && enCronogramaProjeto["Custo"].ToString() != "")
                        custoTarefa = enCronogramaProjeto["Custo"].ToString().Replace(",", ".");
                    else
                        custoTarefa = "0";

                    if (enCronogramaProjeto["Anotacoes"] != null && enCronogramaProjeto["Anotacoes"].ToString() != "")
                        anotacoes = "'" + enCronogramaProjeto["Anotacoes"].ToString().Replace(",", ".") + "'";
                    else
                        anotacoes = "NULL";

                    usuarioResponsavel = enCronogramaProjeto["CodigoUsuarioResponsavel"].ToString().Replace(",", ".");
                } // else if (ExisteGuidNaTarefaCronogramaProjeto(idEdicaoEap, varGuidTarefa, ref enCronogramaProjeto))...


                //
                //Verificar os valores caso qeu nao tenha valor, seran setados com valores padrões...
                //
                if (inicioTarefa == null || inicioTarefa == "")
                {
                    DataSet ds = GetCronogramaProjetoByIdEdicaoEap(idEdicaoEap);

                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        System.Data.DataRow row = ds.Tables[0].Rows[0];
                        inicioTarefa = row["InicioProjeto"].ToString();
                    }

                }
                if ("" == duracaoTarefa || null == duracaoTarefa)
                    duracaoTarefa = "1";
                if (terminoTarefa == null || terminoTarefa == "")
                {
                    d = DateTime.Parse(inicioTarefa);
                    terminoTarefa = d.AddDays(double.Parse(duracaoTarefa)).ToString();
                }

                if ("" == trabalhoTarefa || null == trabalhoTarefa)
                    trabalhoTarefa = "0";
                if ("" == custoTarefa || null == custoTarefa)
                    custoTarefa = "0";
                if (IndicaInicioFixado == "")
                    IndicaInicioFixado = "N";
                if (IndicaTerminoFixado == "")
                    IndicaTerminoFixado = "N";
                if ("" == anotacoes || null == anotacoes)
                    anotacoes = "NULL";

                comandoSQL += string.Format(@"
                INSERT INTO {0}.{1}.TarefaCronogramaProjeto(
                         [CodigoCronogramaProjeto], [CodigoTarefa], [NomeTarefa], [SequenciaTarefaCronograma]
                        , [CodigoTarefaSuperior], [Nivel], [Duracao], [Inicio], [Termino], [Trabalho], [Custo]
                        , [DataInclusao], [FormatoDuracao], [FormatoTrabalho], [IndicaMarco], [IndicaTarefaCritica]
                        , [IndicaTarefaResumo], [IndicaEap], [IndicaTarefaResumoCronograma], [IndicaInicioFixado]
                        , [IndicaTerminoFixado], [TipoCalculoTarefa], [Anotacoes], [IndicaLinhaBasePendente], [IDTarefa]
                        , [CodigoUsuarioResponsavel], [XmlEstiloEap]
                        )
                VALUES(@CodigoCronogramaProjeto
                    , {3}
                    , '{4}'
                    , {5}
                    , {6}
                    , {7}
                    ,{8}
                    ,{9}
                    ,{10}
                    ,{11}
                    ,{12}
                    ,GETDATE()
                    ,'D'
                    ,'H'
                    ,'N'
                    ,'N'
                    ,'{13}'
                    ,'S'
                    ,'{14}'
                    ,'{15}'
                    ,'{16}'
                    ,'DF'
                    ,{17}
                    ,'N'
                    ,'{18}'
                    ,{19}
                    ,'{20}'
                    ) 
                ", bancodb, ownerdb, idEdicaoEap
                 , varCodigoTarefa, varNomeTarefa, varSecuenciaTarefa
                 , (varCodigoSupTarefa == "0" ? "NULL" : varCodigoSupTarefa)
                 , varNivelTarefa
                 , duracaoTarefa
                 , (inicioTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + inicioTarefa + "', 103)")
                 , (terminoTarefa == "NULL" ? "NULL" : "CONVERT(DateTime, '" + terminoTarefa + "', 103)")
                 , trabalhoTarefa, custoTarefa, varFilho != "" ? "S" : "N"
                 , IndicaTarefaResumoCronograma, IndicaInicioFixado, IndicaTerminoFixado, anotacoes, varGuidTarefa
                 , (usuarioResponsavel != "" ? usuarioResponsavel : "NULL")
                 , "<showChildren>" + varXmlStyle + "</showChildren><Style>" + varXmlStyleAux + "</Style>");
                //-----------------------------------------------------------
                //System.Diagnostics.Debug.WriteLine(comandoSQL);
            }


            comandoSQL += string.Format(@"
                    --3ro) atualizando a estructura gerarquita.

                    UPDATE {0}.{1}.[TarefaCronogramaProjeto] 
                       SET [EstruturaHierarquica] = {0}.{1}.f_GetEstruturaHierarquicaTarefa(cp.[CodigoProjeto], [CodigoTarefa])

                    FROM {0}.{1}.[ControleEdicaoEap]                    AS [ceap]
                        INNER JOIN {0}.{1}.[CronogramaProjeto]          AS [cp] 
                            ON (cp.[CodigoProjeto] = ceap.[CodigoProjeto])   
                        INNER JOIN {0}.{1}.[TarefaCronogramaProjeto]    AS [tc] 
                            ON (tc.[CodigoCronogramaProjeto] = cp.[CodigoCronogramaProjeto])
                    WHERE ceap.[IDEdicaoEap]  =  '{2}'
                    ", bancodb, ownerdb, idEdicaoEap);
            //System.Diagnostics.Debug.WriteLine(comandoSQL);
            int regAfetados = 0;
            cDados.execSQL(comandoSQL, ref regAfetados);
        }
    }

    private bool ExisteGuidNaTarefaEap(string idEdicaoEap, string varGuidTarefa, out DataRow enTareaEap)
    {
        //ver si esta en tarefaEap
        //Retorna false si ainda nao se mecho los nodos desde la tela de disenho, fazendo double clip
        bool encontrado = false;
        string comandoSqlTarefa = string.Format(@"
                    SELECT * from {0}.{1}.TarefaEap 
                    WHERE IdEdicaoEap = '{2}' AND IDTarefa = '{3}'
                    ORDER BY IDTarefa
                ", bancodb, ownerdb, idEdicaoEap, varGuidTarefa);
        DataSet ds = cDados.getDataSet(comandoSqlTarefa);
        enTareaEap = null;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            enTareaEap = ds.Tables[0].Rows[0];
            encontrado = true;
        }
        return encontrado;
    }

    private bool ExisteGuidNaTarefaCronogramaProjeto(string idEdicaoEap, string varGuidTarefa, out DataRow enCronogramaProjeto)
    {
        bool encontrado = false;

        string comandoSqlTarefa = string.Format(@"
                SELECT  *
                FROM    {0}.{1}.TarefaCronogramaProjeto
                WHERE   IDTarefa = '{2}'
                ", bancodb, ownerdb, varGuidTarefa);
        DataSet ds = cDados.getDataSet(comandoSqlTarefa);
        enCronogramaProjeto = null;

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            enCronogramaProjeto = ds.Tables[0].Rows[0];
            encontrado = true;
        }
        return encontrado;
    }

    private DataSet GetTarefaEapByIdEdicaoEap(string idEdicaoEap)
    {
        //ver si esta en tarefaEap
        //Retorna null si ainda nao se mecho los nodos desde la tela de disenho, fazendo double clip
        //--------------------
        string comandoSqlTarefa = string.Format(@"
                    SELECT * from {0}.{1}.TarefaEap 
                    WHERE IDEdicaoEap = '{2}'
                    ORDER BY IDTarefa
                ", bancodb, ownerdb, idEdicaoEap);

        DataSet ds = cDados.getDataSet(comandoSqlTarefa);
        //--------------------
        return ds;
    }

    private string GetCodigoCronogramaFromProjectId(string idEdicaoEap)
    {
        string codigoCronogramaProjeto = "";

        string comandoSQL = string.Format(@"
                SELECT t.CodigoCronogramaProjeto
	                FROM
		                {0}.{1}.ControleEdicaoEap ceap
			                INNER JOIN CronogramaProjeto cp on (cp.CodigoProjeto = ceap.CodigoProjeto)
				                INNER JOIN TarefaCronogramaProjeto t on (t.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto)
	                WHERE
		                ceap.IDEdicaoEap = '{2}'
                ", bancodb, ownerdb, idEdicaoEap);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        return codigoCronogramaProjeto;
    }

    private DataSet GetTarefaCronogramaProjetoByIdEdicaoEap(string idEdicaoEap)
    {
        string codigoCronogramaProjeto = GetCodigoCronogramaFromProjectId(idEdicaoEap);
        string comandoSQL = string.Format(@"
                    SELECT * from {0}.{1}.TarefaCronogramaProjeto 
                    WHERE CodigoCronogramaProjeto = '{2}'
                    ORDER BY IDTarefa
                ", bancodb, ownerdb, codigoCronogramaProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    private DataSet GetCronogramaProjetoByIdEdicaoEap(string idEdicaoEap)
    {
        string codigoCronogramaProjeto = GetCodigoCronogramaFromProjectId(idEdicaoEap);
        string comandoSQL = string.Format(@"
                    SELECT * from {0}.{1}.CronogramaProjeto 
                    WHERE CodigoCronogramaProjeto = '{2}'
                ", bancodb, ownerdb, codigoCronogramaProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    #endregion

    #region ===== Mapa Estrategico - XML =====

    [WebMethod]
    public string loadXmlMapa(string codigoMapa, string codigoEntidad, string codigoUsuario)
    {
        //codigoUsuario = "4";
        DataSet ds;
        string xmlMapaEstrategico = @"<?xml version=""1.0"" encoding=""utf-8""?>" + Environment.NewLine;

        //MAPA - Obtem o valores do mapa actual.

        ds = cDados.getMapaEstrategicoToXML(codigoMapa, codigoEntidad);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string xmlMapa = string.Format(@"<mapa CodigoMapa=""{0}"" TituloMapa=""{1}"" CodigoUnidadeNegocio=""{2}"" VersaoMapaEstrategico=""{3}"" DataInicioVersaoMapaEstatregico=""{4}"">" + Environment.NewLine, ds.Tables[0].Rows[0]["CodigoMapaEstrategico"].ToString()
                                    , ds.Tables[0].Rows[0]["TituloMapaEstrategico"].ToString()
                                    , ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString()
                                    , ds.Tables[0].Rows[0]["VersaoMapaEstrategico"].ToString()
                                    , ds.Tables[0].Rows[0]["DataInicioVersaoMapaEstrategico"].ToString());
            if (xmlMapa != "")
            {
                xmlMapaEstrategico += xmlMapa;
                xmlMapaEstrategico += ObtemXmlElementosMapas(codigoMapa, codigoEntidad, codigoUsuario);
                xmlMapaEstrategico += obtemXmlParametrosSistema(codigoEntidad);
                xmlMapaEstrategico += "</mapa>";

                //XmlDocument document = new XmlDocument();
                //document.LoadXml(xmlMapaEstrategico);
                //xmlMapaEstrategico = document.InnerXml;
            }
        }

        return xmlMapaEstrategico.Replace("&amp;", "&");
    }

    #region ObtemXmlElementosMapas_old
    ///// <summary>
    ///// Nesta função, criara o XML dos Objetod de Estrategia, creando para cada objeto un 'set'.
    ///// Cada 'set' tendrá os parametrôs necessários para redisenhar os elementos do mapa
    ///// extratégico (cores, textos, etc.).
    ///// </summary>
    ///// <returns>string con Xml do mapa estratégico.</returns>
    //private string ObtemXmlElementosMapas(string codigoMapa)
    //{
    //    DataSet ds;
    //    string xmlObjetosMapa = "<dataSet>" + Environment.NewLine;
    //    int count = 1;

    //    //Objeto pai: Objeto Mapa.
    //    ds = cDados.getObjetosMapaEstrategicoToXML(codigoMapa, "AND ob.CodigoTipoObjetoEstrategia = 1");
    //    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
    //    {
    //        DataTable dt = ds.Tables[0];

    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            xmlObjetosMapa += string.Format(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao ="""" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"">" + Environment.NewLine, dr["CodigoObjetoEstrategia"].ToString()
    //                           , dr["CodigoVersaoMapaEstrategico"].ToString()
    //                           , dr["TituloObjetoEstrategia"].ToString()
    //                           , dr["DescricaoObjetoEstrategia"].ToString()
    //                           , dr["CorFundoObjetoEstrategia"].ToString()
    //                           , dr["CorBordaObjetoEstrategia"].ToString()
    //                           , dr["CorFonteObjetoEstrategia"].ToString()
    //                           , dr["AlturaObjetoEstrategia"].ToString()
    //                           , dr["LarguraObjetoEstrategia"].ToString()
    //                           , dr["TopoObjetoEstrategia"].ToString()
    //                           , dr["EsquerdaObjetoEstrategia"].ToString()
    //                           , dr["CodigoObjetoEstrategiaDe"].ToString()
    //                           , dr["CodigoObjetoEstrategiaPara"].ToString()
    //                           , dr["IniciaisTipoObjeto"].ToString()
    //                           , dr["CodigoObjetoEstrategiaSuperior"].ToString()
    //                           );
    //        }
    //    }


    //    //Todos os objetos filhos do Objeto Mapa.
    //    ds = cDados.getObjetosMapaEstrategicoToXML(codigoMapa, "AND ob.CodigoTipoObjetoEstrategia <> 1");

    //    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
    //    {

    //        DataTable dt = ds.Tables[0];

    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            int nivel = getNivelOE(dr["EstruturaHierarquica"].ToString());

    //            xmlObjetosMapa += string.Format(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao ="""" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"">" + Environment.NewLine, dr["CodigoObjetoEstrategia"].ToString()
    //                           , dr["CodigoVersaoMapaEstrategico"].ToString()
    //                           , dr["TituloObjetoEstrategia"].ToString()
    //                           , dr["DescricaoObjetoEstrategia"].ToString()
    //                           , dr["CorFundoObjetoEstrategia"].ToString()
    //                           , dr["CorBordaObjetoEstrategia"].ToString()
    //                           , dr["CorFonteObjetoEstrategia"].ToString()
    //                           , dr["AlturaObjetoEstrategia"].ToString()
    //                           , dr["LarguraObjetoEstrategia"].ToString()
    //                           , dr["TopoObjetoEstrategia"].ToString()
    //                           , dr["EsquerdaObjetoEstrategia"].ToString()
    //                           , dr["CodigoObjetoEstrategiaDe"].ToString()
    //                           , dr["CodigoObjetoEstrategiaPara"].ToString()
    //                           , dr["IniciaisTipoObjeto"].ToString()
    //                           , dr["CodigoObjetoEstrategiaSuperior"].ToString()
    //                           );

    //            if (count == dt.Rows.Count)
    //            {
    //                for (int i = 0; i < nivel; i++)
    //                    xmlObjetosMapa += @"</set>" + Environment.NewLine;
    //            }
    //            else
    //            {
    //                int proximoNivel = count == 1 ? 1 : getNivelOE(dt.Rows[count]["EstruturaHierarquica"].ToString());

    //                if (nivel > proximoNivel)
    //                {
    //                    xmlObjetosMapa += @"</set>" + Environment.NewLine;
    //                    xmlObjetosMapa += @"</set>" + Environment.NewLine;
    //                }
    //                else
    //                {
    //                    if (nivel == proximoNivel)
    //                    {
    //                        xmlObjetosMapa += @"</set>" + Environment.NewLine;
    //                    }
    //                }
    //            }

    //            count++;
    //        }
    //        xmlObjetosMapa += "</set></dataSet>" + Environment.NewLine;
    //        XmlDocument xml = new XmlDocument();
    //        xml.LoadXml(xmlObjetosMapa);
    //        xmlObjetosMapa = xml.InnerXml;
    //    }
    //    return xmlObjetosMapa;
    //} 
    #endregion

    /// <summary>
    /// Nesta função, criara o XML dos Objetod de Estrategia, creando para cada objeto un 'set'.
    /// Cada 'set' tendrá os parametrôs necessários para redisenhar os elementos do mapa
    /// extratégico (cores, textos, etc.).
    /// </summary>
    /// <returns>string con Xml do mapa estratégico.</returns>
    private string ObtemXmlElementosMapas(string codigoMapa, string codigoEntidade, string codigoUsuario)
    {
        DataSet ds;
        StringBuilder xmlObjetosMapa = new StringBuilder();
        int count = 1;

        ds = ObtemObjetosMapaEstrategico(codigoMapa, codigoEntidade, codigoUsuario);

        #region Objeto pai: Objeto Mapa.

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Select("CodigoTipoObjetoEstrategia = 1"))
            {
                xmlObjetosMapa.AppendFormat(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao =""{15}"" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"">" + Environment.NewLine
                   , dr["CodigoObjetoEstrategia"]
                   , dr["CodigoVersaoMapaEstrategico"]
                   , dr["TituloObjetoEstrategia"]
                   , dr["DescricaoObjetoEstrategia"]
                   , dr["CorFundoObjetoEstrategia"]
                   , dr["CorBordaObjetoEstrategia"]
                   , dr["CorFonteObjetoEstrategia"]
                   , dr["AlturaObjetoEstrategia"]
                   , dr["LarguraObjetoEstrategia"]
                   , dr["TopoObjetoEstrategia"]
                   , dr["EsquerdaObjetoEstrategia"]
                   , dr["CodigoObjetoEstrategiaDe"]
                   , dr["CodigoObjetoEstrategiaPara"]
                   , dr["IniciaisTipoObjeto"]
                   , dr["CodigoObjetoEstrategiaSuperior"]
                   , dr["NumRotacaoObjeto"]);
            }
        }
        #endregion

        #region Todos os objetos filhos do Objeto Mapa.
        //ds = cDados.getObjetosMapaEstrategicoToXML(codigoMapa, "AND ob.CodigoTipoObjetoEstrategia <> 1");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            int countTagsAbertas = 0;
            DataTable dt = ds.Tables[0];
            DataRow[] rows = dt.Select("CodigoTipoObjetoEstrategia <> 1", "EstruturaHierarquica");
            foreach (DataRow dr in rows)
            {
                int nivel = getNivelOE(dr["EstruturaHierarquica"].ToString());
                countTagsAbertas++;

                #region Objeto estratégia = Objetivo

                if (dr["IniciaisTipoObjeto"].ToString() == "OBJ")
                {
                    string corStatus = string.Empty;
                    switch (dr["corObjetivo"].ToString().ToLower())
                    {
                        case "vermelho":
                            corStatus = "#FF0000";
                            break;
                        case "verde":
                            corStatus = "#00FF00";
                            break;
                        case "azul":
                            corStatus = "#0000FF";
                            break;
                        case "amarelo":
                            corStatus = "#FFFF00";
                            break;
                        case "branco":
                            corStatus = "#FFFFFF";
                            break;
                        default:
                            corStatus = "#FFFFFF";
                            break;
                    }
                    xmlObjetosMapa.AppendFormat(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao =""{15}"" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"" target=""_top"" link=""{16}""  corStatus=""{17}"">" + Environment.NewLine
                           , dr["CodigoObjetoEstrategia"]
                           , dr["CodigoVersaoMapaEstrategico"]
                           , dr["TituloObjetoEstrategia"]
                           , dr["DescricaoObjetoEstrategia"]
                           , dr["CorFundoObjetoEstrategia"]
                           , dr["CorBordaObjetoEstrategia"]
                           , dr["CorFonteObjetoEstrategia"]
                           , dr["AlturaObjetoEstrategia"]
                           , dr["LarguraObjetoEstrategia"]
                           , dr["TopoObjetoEstrategia"]
                           , dr["EsquerdaObjetoEstrategia"]
                           , dr["CodigoObjetoEstrategiaDe"]
                           , dr["CodigoObjetoEstrategiaPara"]
                           , dr["IniciaisTipoObjeto"]
                           , dr["CodigoObjetoEstrategiaSuperior"]
                           , dr["NumRotacaoObjeto"]
                           , getLinkObjetivo(codigoMapa, dr["CodigoObjetoEstrategia"].ToString(), (bool)dr["IndicaConsultaDetalhesPermitida"])
                           , corStatus);
                }

                #endregion

                #region Objeto estratégia = Causa e efeito

                else if (dr["IniciaisTipoObjeto"].ToString() == "CE1" || dr["IniciaisTipoObjeto"].ToString() == "CE2")
                {
                    xmlObjetosMapa.AppendFormat(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao =""{15}"" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"">" + Environment.NewLine
                           , dr["CodigoObjetoEstrategia"]
                           , dr["CodigoVersaoMapaEstrategico"]
                           , dr["TituloObjetoEstrategia"]
                           , dr["DescricaoObjetoEstrategia"]
                           , dr["CorFundoObjetoEstrategia"]
                           , dr["CorBordaObjetoEstrategia"]
                           , dr["CorFonteObjetoEstrategia"]
                           , dr["AlturaObjetoEstrategia"]
                           , dr["LarguraObjetoEstrategia"]
                           , dr["TopoObjetoEstrategia"]
                           , dr["EsquerdaObjetoEstrategia"]
                           , dr["CodigoObjetoEstrategiaDe"]
                           , dr["CodigoObjetoEstrategiaPara"]
                           , dr["IniciaisTipoObjeto"]
                           , dr["CodigoObjetoEstrategiaSuperior"]
                           , dr["NumRotacaoObjeto"]);
                }
                #endregion

                #region Demais objetos estratégia

                else
                {
                    xmlObjetosMapa.AppendFormat(@"<set id=""{0}"" tipo=""{13}"" versao=""{1}"" titulo=""{2}"" texto=""{3}"" esquerda=""{10}"" topo=""{9}"" largura=""{8}"" altura=""{7}"" Rotacao =""{15}"" numBorda="""" corFundo=""{4}"" corBorda=""{5}"" corTexto=""{6}"" origem=""{11}"" destino=""{12}"" codigoObjetoSuperior=""{14}"">" + Environment.NewLine
                           , dr["CodigoObjetoEstrategia"]
                           , dr["CodigoVersaoMapaEstrategico"]
                           , dr["TituloObjetoEstrategia"]
                           , dr["DescricaoObjetoEstrategia"]
                           , dr["CorFundoObjetoEstrategia"]
                           , dr["CorBordaObjetoEstrategia"]
                           , dr["CorFonteObjetoEstrategia"]
                           , dr["AlturaObjetoEstrategia"]
                           , dr["LarguraObjetoEstrategia"]
                           , dr["TopoObjetoEstrategia"]
                           , dr["EsquerdaObjetoEstrategia"]
                           , dr["CodigoObjetoEstrategiaDe"]
                           , dr["CodigoObjetoEstrategiaPara"]
                           , dr["IniciaisTipoObjeto"]
                           , dr["CodigoObjetoEstrategiaSuperior"]
                           , dr["NumRotacaoObjeto"]);
                }

                #endregion

                if (count == rows.Length)
                {
                    for (int i = 0; i < countTagsAbertas; i++)
                        xmlObjetosMapa.Append(@"</set>" + Environment.NewLine);
                }
                else
                {
                    int proximoNivel = getNivelOE(rows[count]["EstruturaHierarquica"].ToString());
                    for (int i = 0; i <= nivel - proximoNivel; i++)
                    {
                        xmlObjetosMapa.Append(@"</set>" + Environment.NewLine);
                        countTagsAbertas--;
                    }
                    //if (nivel >= proximoNivel)
                    //{
                    //    xmlObjetosMapa.Append(@"</set>" + Environment.NewLine);
                    //    for (int i = 0; i < nivel - proximoNivel; i++)
                    //        xmlObjetosMapa.Append(@"</set>" + Environment.NewLine);
                    //}
                }
                count++;
            }
        }
        #endregion

        xmlObjetosMapa.Insert(0, "<dataSet>" + Environment.NewLine);
        xmlObjetosMapa.Append("</set></dataSet>");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlObjetosMapa.ToString());
        xml.Normalize();
        return xml.InnerXml;
    }

    private DataSet ObtemObjetosMapaEstrategico(string codigoMapa, string codigoEntidade, string idUsuarioLogado)
    {
        DataSet ds = cDados.getObjetosMapaEstrategicoToXML(codigoMapa, idUsuarioLogado, codigoEntidade, "");
        ds.Tables[0].Columns.Add("CorObjetivo");

        using (DataSet dsTemp = cDados.getObjetosMapaEstrategico(int.Parse(codigoMapa), int.Parse(codigoEntidade), 'A', DateTime.Today.Month, DateTime.Today.Year, int.Parse(idUsuarioLogado), ""))
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DataRow drTemp = dsTemp.Tables[0].Select("CodigoObjetoEstrategia = " + row["CodigoObjetoEstrategia"])[0];
                row["CorObjetivo"] = drTemp["CorObjetivo"];
            }
        }
        return ds;
    }

    private string getLinkObjetivo(string codigoMapa, string codigoObjetoEstrategia, bool podeAcessar)
    {
        if (!podeAcessar)
            return "";

        object codigoUnidadeNegocio = cDados.getMapasEstrategicos(null
            , " AND Mapa.CodigoMapaEstrategico = " + codigoMapa).Tables[0].Rows[0]["CodigoUnidadeNegocio"];

        return cDados.getPathSistema() + string.Format(@"_Estrategias/objetivoestrategico/indexResumoObjetivo.aspx?COE={0}&amp;UNM={1}&amp;CM={2}", codigoObjetoEstrategia, codigoUnidadeNegocio, codigoMapa);
    }

    private int getNivelOE(string estrutura)
    {
        if (estrutura == "" || (estrutura.Contains(".") == false))
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
    /// </summary>
    /// <returns>string con Xml do mapa estratégico.</returns>
    private string obtemXmlParametrosSistema(string codigoEntidade)
    {
        DataTable dt;
        string xmlStyles = "";

        dt = cDados.getParametroSistemaToXML(codigoEntidade).Tables[0];
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
        }
        return xmlStyles;
    }

    [WebMethod]
    public string saveXmlMapa(string xmlMapa, string codigoUsuario, string codigoMapa, string codigoEntidade)
    {
        xmlMapa = xmlMapa.Replace("&lt;", "<").Replace("&gt;", ">");
        //StreamWriter arquivoCtrl;
        //arquivoCtrl = new StreamWriter(@"d:\testexml.log", true, System.Text.Encoding.UTF8);
        //arquivoCtrl.WriteLine(xmlMapa);
        //arquivoCtrl.Close();

        gravaXmlMapaNaBase(xmlMapa, codigoUsuario);

        return loadXmlMapa(codigoMapa, codigoEntidade, codigoUsuario);
    }

    /// <summary>
    /// Generar a instrução SQL a partir do XML que recibe como parâmetro.
    /// Vai gerar em duas etapas:
    /// 1: Mapa.
    /// 2: Objeto Estrategico do Mapa.
    /// </summary>
    /// <param name="xmlMapa">string que contem o xml ao convertir en SQL.</param>
    public void gravaXmlMapaNaBase(string xmlMapa, string codigoUsuario)
    {
        string versaoMapa = "";
        string codigoMapa = "";
        string sqlMapaEstrategico = "";
        string tituloMapa = "";
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
                    versaoMapa = nodo.GetAttribute("VersaoMapaEstrategico");
                }
            }
        }

        nodoMapa = xDoc.SelectNodes("/mapa/dataSet/set[@tipo='map']");
        if (nodoMapa.Count > 0)
        {
            foreach (XmlElement nodo in nodoMapa)
            {
                if (nodo.GetAttribute("id") != "")
                {
                    tituloMapa = nodo.GetAttribute("texto").Trim().Replace("'", "''");
                }
            }
        }

        sqlMapaEstrategico += string.Format(@"
        -- Gerar sentencia SQL do MAPA.
        UPDATE {0}.{1}.MapaEstrategico
        SET
            TituloMapaEstrategico = '{2}'
        WHERE CodigoMapaEstrategico = {3}
        ", bancodb, ownerdb, tituloMapa, codigoMapa);

        //Gerar SQL dos Objetos Estratégicos do Mapa.
        XmlNodeList nodoObjetoEstrategico = xDoc.GetElementsByTagName("set");
        if (nodoObjetoEstrategico.Count > 0)
        {
            Dictionary<string, int> novosObjetos = new Dictionary<string, int>();

            foreach (XmlElement nodo in nodoObjetoEstrategico)
            {
                string codigoOb = nodo.GetAttribute("id");
                string tipoOb = nodo.GetAttribute("tipo");
                string codigoTipoOb = cDados.getCodigoTipoObjeto(tipoOb);
                string versao = versaoMapa;//nodo.GetAttribute("versao");
                string titulo = nodo.GetAttribute("titulo").Trim().Replace("'", "''");
                string texto = nodo.GetAttribute("texto").Trim().Replace("'", "''");
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
                string acao = nodo.GetAttribute("acao");

                if (string.IsNullOrEmpty(codigoObjetoSuperior))
                    codigoObjetoSuperior = "NULL";

                switch (acao)
                {
                    case "A":
                        #region Objeto estratégia alterado

                        sqlMapaEstrategico += string.Format(@"
                            if ( not exists (SELECT 1 FROM {0}.{1}.ObjetoEstrategia WHERE CodigoObjetoEstrategia = {2} ))
                            BEGIN
                                INSERT INTO {0}.{1}.ObjetoEstrategia (CodigoMapaEstrategico, CodigoVersaoMapaEstrategico, CodigoTipoObjetoEstrategia, OrdemObjeto, DataInclusao, CodigoUsuarioInclusao)
                                VALUES ( {3}, {16}, {14}, 1, GETDATE(), {15}) 
                            END

                            UPDATE {0}.{1}.ObjetoEstrategia
                               SET TituloObjetoEstrategia    = '{4}'
                                 , DescricaoObjetoEstrategia = '{5}'
                                 , AlturaObjetoEstrategia    = {6}
                                 , LarguraObjetoEstrategia   = {7}
                                 , TopoObjetoEstrategia      = {8}
                                 , EsquerdaObjetoEstrategia  = {9}
                                 , CorFundoObjetoEstrategia  = '{10}'
                                 , CorBordaObjetoEstrategia  = '{11}'
                                 , CorFonteObjetoEstrategia  = '{12}'
                                 , DataUltimaAlteracao = GETDATE()
                                 , CodigoUsuarioUltimaAlteracao = {15}
                                 , NumRotacaoObjeto = {17}
                             WHERE CodigoObjetoEstrategia = {2}
                               AND CodigoMapaEstrategico = {3}

                          ", bancodb, ownerdb, codigoOb, codigoMapa
                            , titulo, texto, altura, largura, topo, esquerda
                            , corFundo, corBorda, corTexto, codigoTipoOb
                            , codigoObjetoSuperior, codigoUsuario, versao, rotacao);

                        #endregion
                        break;
                    case "I":
                        #region Objeto estratégia incluído


                        #region Comentario
                        /*
                        ListDictionary dados = new ListDictionary();
                        dados.Add("CodigoMapaEstrategico", codigoMapa);
                        dados.Add("TituloObjetoEstrategia", titulo);
                        dados.Add("DescricaoObjetoEstrategia", texto);
                        dados.Add("AlturaObjetoEstrategia", altura);
                        dados.Add("LarguraObjetoEstrategia", largura);
                        dados.Add("TopoObjetoEstrategia", topo);
                        dados.Add("EsquerdaObjetoEstrategia", esquerda);
                        dados.Add("CorFundoObjetoEstrategia", corFundo);
                        dados.Add("CorBordaObjetoEstrategia", corBorda);
                        dados.Add("CorFonteObjetoEstrategia", corTexto);
                        dados.Add("CodigoTipoObjetoEstrategia", codigoTipoOb);
                        dados.Add("CodigoObjetoEstrategiaSuperior", codigoObjetoSuperior);
                        dados.Add("CodigoUsuarioInclusao", codigoUsuario);
                        dados.Add("DataInclusao", "GETDATE()");
                        dados.Add("CodigoVersaoMapaEstrategico", versao);
                        dados.Add("NumRotacaoObjeto", rotacao);
                        int novoCodigoObjetoEstategia = cDados.insert("ObjetoEstrategia", dados, true);
                        novosObjetos.Add(codigoOb, novoCodigoObjetoEstategia);*/
                        #endregion

                        if (novosObjetos.ContainsKey(codigoObjetoSuperior))
                            codigoObjetoSuperior = novosObjetos[codigoObjetoSuperior].ToString();

                        string sqlInsert = string.Format(@"
                            INSERT INTO {0}.{1}.ObjetoEstrategia
                               (CodigoMapaEstrategico
                               ,CodigoVersaoMapaEstrategico
                               ,TituloObjetoEstrategia
                               ,DescricaoObjetoEstrategia
                               ,AlturaObjetoEstrategia
                               ,LarguraObjetoEstrategia
                               ,TopoObjetoEstrategia
                               ,EsquerdaObjetoEstrategia
                               ,CorFundoObjetoEstrategia
                               ,CorBordaObjetoEstrategia
                               ,CorFonteObjetoEstrategia
                               ,CodigoTipoObjetoEstrategia
                               ,CodigoObjetoEstrategiaSuperior
                               ,DataInclusao
                               ,CodigoUsuarioInclusao
                               ,NumRotacaoObjeto)
                            VALUES
                                ( {2}
                                , {15}
                                ,'{3}'
                                ,'{4}'
                                , {5}
                                , {6}
                                , {7}
                                , {8}
                                ,'{9}'
                                ,'{10}'
                                ,'{11}'
                                , {12}
                                , {13}
                                , GETDATE()
                                , {14}
                                , {16})
                    
                    Select scope_identity() AS novoCodigoObjetoEstategia
"
                            , bancodb
                            , ownerdb
                            , codigoMapa
                            , titulo
                            , texto
                            , altura
                            , largura
                            , topo
                            , esquerda
                            , corFundo
                            , corBorda
                            , corTexto
                            , codigoTipoOb
                            , codigoObjetoSuperior
                            , codigoUsuario
                            , versao
                            , rotacao);
                        DataSet ds = cDados.getDataSet(sqlInsert);
                        int novoCodigoObjetoEstategia = Convert.ToInt32(ds.Tables[0].Rows[0]["novoCodigoObjetoEstategia"]);
                        novosObjetos.Add(codigoOb, novoCodigoObjetoEstategia);

                        #endregion
                        break;
                    case "E":
                        #region Objeto estratégia exluído

                        sqlMapaEstrategico += string.Format(@"
                         UPDATE {0}.{1}.ObjetoEstrategia
	                        SET DataExclusao = GETDATE(),
		                        CodigoUsuarioExclusao = {2}
                          WHERE CodigoObjetoEstrategia = {3}
"
                            , bancodb, ownerdb, codigoUsuario, codigoOb);
                        /*sqlMapaEstrategico += string.Format(@"DELETE FROM {0}.{1}.ObjetoEstrategia WHERE CodigoObjetoEstrategia = {2}"
                            , bancodb, ownerdb, codigoOb);*/

                        #endregion
                        break;
                    default:
                        continue;
                }
            }
            foreach (XmlElement node in xDoc.SelectNodes("//set[@tipo='ce1']"))
            {
                sqlMapaEstrategico += DefineCausaEfeito(novosObjetos, node);
            }
            foreach (XmlElement node in xDoc.SelectNodes("//set[@tipo='ce2']"))
            {
                sqlMapaEstrategico += DefineCausaEfeito(novosObjetos, node);
            }
        }
        if (!string.IsNullOrEmpty(sqlMapaEstrategico))
        {
            sqlMapaEstrategico += "/*=============  F I M  ============= */";
            int regAlterados = 0;

            // Salva o XML no HD para ser consultado como HISTÓRICO
            // -----------------------------------------------------------
            string arquivoSQL = "";
            if (salvarHistoricoXMLCronogramaMapaEstrategicoEmDisco)
            {
                // antes de executar, salva o script no hd. Será utilizado o mesmo endereço do cronograma
                // se o diretório não existir, será criado
                if (!Directory.Exists(diretorioCronogramas))
                    Directory.CreateDirectory(diretorioCronogramas);

                arquivoSQL = diretorioCronogramas + "\\" + string.Format("MapaEstrategico_Id{0}_V{1}_U{2}_{3}", codigoMapa, versaoMapa, codigoUsuario, DateTime.Now.ToString("yyyyMMdd_HHmmss")) + ".sql";
                string arquivoXML = arquivoSQL.Replace(".sql", ".xml");
                try
                {
                    // Salva o arquivo com os comandos sql que serão utilizados para atualizar o mapa
                    StreamWriter sw = new StreamWriter(arquivoSQL, false);
                    sw.Write(sqlMapaEstrategico);
                    sw.Close();

                    // Salva o arquivo original enviado pelo componente de desenho do mapa
                    xDoc.Save(arquivoXML);
                }
                catch
                {
                    // se der erro ao salvar o arquivo... ignora e segue em frente
                }

            }

            try
            {
                classeDados.execSQL(sqlMapaEstrategico, ref regAlterados);
            }
            catch (Exception ex)
            {
                if (arquivoSQL != "")
                {
                    // se der erro ao executar o comando... salva o erro
                    string arquivoErro = arquivoSQL.Replace(".sql", ".err");
                    StreamWriter sw = new StreamWriter(arquivoErro, false);
                    sw.Write("Erro ao atualizar o mapa estratégido: " + ex.Message);
                    sw.Close();
                }
            }

        }
    }

    private string DefineCausaEfeito(Dictionary<string, int> novosObjetos, XmlElement node)
    {
        string sqlMapaEstrategico = string.Empty;
        string acao = node.GetAttribute("acao");
        int origem;
        int destino;

        if (!(acao == "I" || acao == "A")) return string.Empty;

        if (!(int.TryParse(node.GetAttribute("origem"), out origem)) ||
            !(int.TryParse(node.GetAttribute("destino"), out destino))) return string.Empty;

        string id = (novosObjetos.ContainsKey(node.GetAttribute("id"))) ?
            novosObjetos[node.GetAttribute("id")].ToString() : node.GetAttribute("id");
        string codigoObjetoEstrategiaDe = (novosObjetos.ContainsKey(origem.ToString())) ?
            novosObjetos[origem.ToString()].ToString() : origem.ToString();
        string codigoObjetoEstrategiaPara = (novosObjetos.ContainsKey(destino.ToString())) ?
            novosObjetos[destino.ToString()].ToString() : destino.ToString();

        if (acao == "A")
        {
            sqlMapaEstrategico += string.Format(@"
DELETE FROM {0}.{1}.RelacaoObjetoEstrategia
 WHERE CodigoObjetoDesenhoCausaEfeito = {2}"
                , bancodb
                , ownerdb
                , id);
        }
        sqlMapaEstrategico += string.Format(@"
INSERT INTO {0}.{1}.RelacaoObjetoEstrategia
       (CodigoObjetoEstrategiaDe
       ,CodigoObjetoEstrategiaPara
       ,CodigoObjetoDesenhoCausaEfeito)
VALUES({2},{3},{4})"
            , bancodb
            , ownerdb
            , codigoObjetoEstrategiaDe
            , codigoObjetoEstrategiaPara
            , id);

        return sqlMapaEstrategico;
    }

    #endregion

    #region ===== Integração Indicadores =====

    #region ===== Classes da Integração de Indicadores =====

    private enum ide_ResultadoValidacaoAcesso
    {
        naoProcessado,
        erroNoProcessamento,
        credenciaisInvalidas,
        semAcessoAEntidade,
        semPermissaoParaExportar,
        acessoValidado
    }

    private enum ide_OperacaoEfetuada
    {
        Incluido,
        Atualizado,
        Ignorado,
        Rejeitado,
        Nenhuma
    }

    private enum ide_OperacaoSolicitada
    {
        Gravar,
        Excluir,
        NaoInformada
    }

    private enum ide_ResultadoIntegracao
    {
        arquivoProcessado = 0,
        entidadeNaoEncontrada = 1,
        erroCriacaoRegistroProcesso = 2,
        credenciaisInvalidas = 3,
        credenciaisSemAcessoAEntidade = 4,
        credenciaisSemPermissaoParaExportar = 5,
        erroValidacaoCredenciais = 6,
        erroImportacaoRegistro = 7,
        erroValidacaoXml = 8
    }

    /// <summary>
    /// Classe ancestral de registro de importação de dados de estratégia do mecanimos de integração do portal
    /// </summary>
    class ide_RegistroImportacao
    {
        public ide_OperacaoSolicitada TipoOperacaoSolicitada;
        public int? Ano, Mes;
        public int? CodigoUnidade;
        public DateTime DataApuracao;
        public ide_OperacaoEfetuada TipoOperacaoEfetuada;

        public string TipoOperacaoInformada;
        public string AnoInformado, MesInformado;
        public string CodigoUnidadeInformada;
        public string CodigoIndicadorInformado;
        public string CodigoDadoInformado;
        public string ValorInformado;
        public string DataApuracaoInformada;

        public ide_RegistroImportacao()
            : base()
        {
            TipoOperacaoInformada = "";
            AnoInformado = "";
            MesInformado = "";
            CodigoUnidadeInformada = "";
            CodigoIndicadorInformado = "";
            CodigoDadoInformado = "";
            ValorInformado = "";
            DataApuracaoInformada = "";
        }

    }

    /// <summary>
    /// Classe referente à meta de um indicador no contexto do mecanimos de integração do portal
    /// </summary>
    class ide_Meta : ide_RegistroImportacao
    {
        public int? CodigoIndicador;
        public decimal? ValorMeta;
        public ide_Meta()
            : base()
        {
        }
    }

    /// <summary>
    /// Classe referente ao resultado de um dado no contexto do mecanimos de integração do portal
    /// </summary>
    class ide_Resultado : ide_RegistroImportacao
    {
        public int? CodigoDado;
        public decimal? ValorResultado;
        public ide_Resultado()
            : base()
        {
        }
    }

    class ide_EntityNotFoundException : Exception
    {
        public ide_EntityNotFoundException()
            : base()
        {
        }

        public ide_EntityNotFoundException(string message)
            : base(message)
        {
        }

        public ide_EntityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class ide_ProcessNotCreatedException : Exception
    {
        public ide_ProcessNotCreatedException()
            : base()
        {
        }

        public ide_ProcessNotCreatedException(string message)
            : base(message)
        {
        }

        public ide_ProcessNotCreatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class ide_AcessNotValidatedException : Exception
    {
        public ide_AcessNotValidatedException()
            : base()
        {
        }

        public ide_AcessNotValidatedException(string message)
            : base(message)
        {
        }

        public ide_AcessNotValidatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class ide_InvalidXmlException : Exception
    {
        public ide_InvalidXmlException()
            : base()
        {
        }

        public ide_InvalidXmlException(string message)
            : base(message)
        {
        }

        public ide_InvalidXmlException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion

    /// <summary>
    /// Método 'público' chamado para fazer integração das informações de indicadores (metas, resultados)
    /// </summary>
    /// <param name="idUsuario">ID do usuário utilizado para fazer validação no sistema</param>
    /// <param name="senha">senha do usuário</param>
    /// <param name="xmlEntrada">XML com as informações a serem atualizadas</param>
    /// <returns></returns>
    [WebMethod]
    public string AtualizaInformacoesIndicadores(string siglaEntidade, string idUsuario, string senha, string strXmlEntrada, bool ignoraDataApuracao)
    {
        XmlDocument xmlEntrada = new XmlDocument();
        XmlDocument xmlRetorno;
        XmlNode node, node2, rootNode;
        XmlDeclaration xmlDeclaration;

        ide_ResultadoValidacaoAcesso resultadoValidacao = ide_ResultadoValidacaoAcesso.naoProcessado;
        int? codigoUsuario = null;
        int? codigoEntidade = null;
        int? numeroProtocolo = null;
        DateTime dataIntegracao = new DateTime();

        int? nRegistrosImportar, nRegistrosEfetivados, nRegistrosIgnorados, nRegistrosRejeitados;

        xmlRetorno = new XmlDocument();

        // Declaração do XML
        xmlDeclaration = xmlRetorno.CreateXmlDeclaration("1.0", "utf-16", null);
        xmlRetorno.AppendChild(xmlDeclaration);

        // Cria o elemento raiz - root element
        rootNode = xmlRetorno.AppendChild(xmlRetorno.CreateElement("resultadoIntegracao"));
        try
        {
            try
            {
                xmlEntrada.LoadXml(strXmlEntrada);
            }
            catch (Exception ex)
            {
                throw new ide_InvalidXmlException(ex.Message);
            }

            string msgErroValidacaoCredenciais = "";

            try
            {
                ide_validaCredenciais(siglaEntidade, idUsuario, senha, out codigoUsuario, out codigoEntidade, out resultadoValidacao);
            } // try
            catch (Exception ex)
            {
                resultadoValidacao = ide_ResultadoValidacaoAcesso.erroNoProcessamento;
                msgErroValidacaoCredenciais = ex.Message;
            }

            // se não tiver localizado a entidade, gera-se erro, uma vez que não haverá como logar nada sem entidade;
            if (!codigoEntidade.HasValue)
            {
                // verifica se não encontrou a entidade por que ocorreu erro ou credencias inválidas;
                if ((resultadoValidacao == ide_ResultadoValidacaoAcesso.erroNoProcessamento) ||
                      (resultadoValidacao == ide_ResultadoValidacaoAcesso.credenciaisInvalidas))
                    throw new ide_AcessNotValidatedException(msgErroValidacaoCredenciais);
                else
                    throw new ide_EntityNotFoundException();
            }

            try
            {
                ide_registraNovoProcessoIntegracao(codigoEntidade.Value, codigoUsuario, out numeroProtocolo, out dataIntegracao);
            }
            catch (Exception ex)
            {
                throw new ide_ProcessNotCreatedException(ex.Message);
            }

            if (!numeroProtocolo.HasValue)
                throw new ide_ProcessNotCreatedException("");

            // se as credenciais não foram validadas, gera erro
            if (resultadoValidacao != ide_ResultadoValidacaoAcesso.acessoValidado)
            {
                // verifica se não validou por que ocorreu erro ou por que realmente as credenciais são inválidas
                if (msgErroValidacaoCredenciais.Length > 0)
                    throw new ide_AcessNotValidatedException(msgErroValidacaoCredenciais);
                else
                    throw new ide_AcessNotValidatedException();
            }

            // número do protocolo
            node = xmlRetorno.CreateElement("numeroProtocolo");
            node.InnerXml = numeroProtocolo.ToString();
            rootNode.AppendChild(node);

            // data da integração
            node = xmlRetorno.CreateElement("dataIntegracao");
            node.InnerXml = dataIntegracao.ToString("dd/MM/yyyy hh:mm:ss");
            rootNode.AppendChild(node);

            ide_processaIntegracao(numeroProtocolo.Value, codigoEntidade.Value, codigoUsuario.Value, xmlEntrada, xmlRetorno, rootNode, ignoraDataApuracao, out nRegistrosImportar, out nRegistrosEfetivados, out nRegistrosIgnorados, out nRegistrosRejeitados);

            ide_atualizaLogImportacao(numeroProtocolo.Value, xmlEntrada, xmlRetorno, nRegistrosImportar, nRegistrosEfetivados, nRegistrosIgnorados, nRegistrosRejeitados);

            node = xmlRetorno.CreateElement("codigoResultado");
            node.InnerXml = ide_ResultadoIntegracao.arquivoProcessado.ToString();
            rootNode.AppendChild(node);

            node = xmlRetorno.CreateElement("descricaoResultado");

            if (nRegistrosImportar == 0)
                node.InnerText = @"Não foram localizados registros a processar.";
            else if (nRegistrosRejeitados > 0)
                node.InnerText = @"Arquivo processado mas contém erro. Consulte o log.";
            else
                node.InnerText = @"Arquivo processado com sucesso! Verifique as informações sobre cada registro processado.";

            rootNode.AppendChild(node);

        } // try
        catch (ide_EntityNotFoundException)
        {
            rootNode.RemoveAll();
            rootNode.AppendChild(xmlRetorno.CreateElement("numeroProtocolo"));
            rootNode.AppendChild(xmlRetorno.CreateElement("dataIntegracao"));

            node = xmlRetorno.CreateElement("codigoResultado");
            node.InnerXml = ide_ResultadoIntegracao.entidadeNaoEncontrada.ToString();
            rootNode.AppendChild(node);

            node = xmlRetorno.CreateElement("descricaoResultado");
            node.InnerText = @"Não foi possível proceder com a integração. Não foi encotrado no sistema entidade corresponde à informada.";
            rootNode.AppendChild(node);
        } // catch(ide_EntityNotFoundException ex)
        catch (ide_ProcessNotCreatedException ex)
        {
            rootNode.RemoveAll();
            rootNode.AppendChild(xmlRetorno.CreateElement("numeroProtocolo"));
            rootNode.AppendChild(xmlRetorno.CreateElement("dataIntegracao"));

            node = xmlRetorno.CreateElement("codigoResultado");
            node.InnerXml = ide_ResultadoIntegracao.erroCriacaoRegistroProcesso.ToString();
            rootNode.AppendChild(node);

            node = xmlRetorno.CreateElement("descricaoResultado");
            node.InnerText = @"Não foi possível proceder com a integração. Ocorreu um erro interno ao criar registro do processo de integração. Entre em contato com o administrador do sistema.";
            if (ex.Message.Length > 0)
                node.InnerText += @" Mensagem original do erro: " + ex.Message.Replace("'", "''");

            rootNode.AppendChild(node);
        } // catch(ide_ProcessNotCreatedException e)
        catch (ide_AcessNotValidatedException ex)
        {
            rootNode.RemoveAll();
            node = xmlRetorno.CreateElement("numeroProtocolo");
            if (numeroProtocolo.HasValue)
                node.InnerXml = numeroProtocolo.ToString();
            rootNode.AppendChild(node);

            node = xmlRetorno.CreateElement("dataIntegracao");
            if (dataIntegracao > DateTime.MinValue)
                node.InnerXml = dataIntegracao.ToString("dd/MM/yyyy hh:mm:ss");
            rootNode.AppendChild(node);

            node = xmlRetorno.CreateElement("codigoResultado");
            node2 = xmlRetorno.CreateElement("descricaoResultado");
            node2.InnerText = "Não foi possível proceder com a integração.";

            switch (resultadoValidacao)
            {
                case ide_ResultadoValidacaoAcesso.credenciaisInvalidas:
                    node.InnerText = ide_ResultadoIntegracao.credenciaisInvalidas.ToString();
                    node2.InnerText += "As credenciais informadas para acesso não foram reconhecidas pelo sistema.";
                    break;
                case ide_ResultadoValidacaoAcesso.erroNoProcessamento:
                    node.InnerText = ide_ResultadoIntegracao.erroValidacaoCredenciais.ToString();
                    node2.InnerText += "Ocorreu um erro interno ao validar as credenciais. Entre em contato com o administrador do sistema.";
                    if (ex.Message.Length > 0)
                        node2.InnerText += @" Mensagem original do erro: " + ex.Message.Replace("'", "''");
                    break;
                case ide_ResultadoValidacaoAcesso.semAcessoAEntidade:
                    node.InnerText = ide_ResultadoIntegracao.credenciaisSemAcessoAEntidade.ToString();
                    node2.InnerText += "As credenciais informadas não têm acesso a entidade informada.";
                    break;
                case ide_ResultadoValidacaoAcesso.semPermissaoParaExportar:
                    node.InnerText = ide_ResultadoIntegracao.credenciaisSemPermissaoParaExportar.ToString();
                    node2.InnerText += "As credenciais informadas não têm permissões para realizar integração.";
                    break;
            }
            rootNode.AppendChild(node);
            rootNode.AppendChild(node2);

        } // catch(ide_AcessNotValidatedException ex)
        catch (ide_InvalidXmlException ex)
        {
            rootNode.RemoveAll();
            rootNode.AppendChild(xmlRetorno.CreateElement("numeroProtocolo"));
            rootNode.AppendChild(xmlRetorno.CreateElement("dataIntegracao"));

            node = xmlRetorno.CreateElement("codigoResultado");
            node.InnerXml = ide_ResultadoIntegracao.erroValidacaoXml.ToString();
            rootNode.AppendChild(node);

            node = xmlRetorno.CreateElement("descricaoResultado");
            node.InnerText = string.Format(@"Não foi possível proceder com a integração. O XML informado não foi reconhecido pelo sistema. Mensagem original do erro: {0}", ex.Message.Replace("'", "''"));
            rootNode.AppendChild(node);
        } // catch(ide_InvalidXmlException e)
        catch (Exception ex)
        {
            bool bProcessado = true;

            node = rootNode.SelectSingleNode("codigoResultado");
            if (node == null)
                node = xmlRetorno.CreateElement("codigoResultado");
            node.InnerXml = "-1";
            rootNode.AppendChild(node);

            node = rootNode.SelectSingleNode("descricaoResultado");
            if (node == null)
                node = xmlRetorno.CreateElement("descricaoResultado");
            node.InnerText = @"Ocorreu um erro desconhecido ao proceder com a integração. O processo foi interrompido.";

            if (bProcessado)
                node.InnerText += @" Verifique as informações para saber a situação de cada registro processado até a ocorrência do erro.";

            node.InnerText += @"Entre em contato com o administrador do sistema.";
            node.InnerText += string.Format(@" Informações adicionais sobre o erro: Método:{0}, Mensagem original: {1}", ex.TargetSite.Name.Replace("'", "''"), ex.Message.Replace("'", "''"));
            rootNode.AppendChild(node);
        } // catch(ide_EntityNotFoundException ex)

        return xmlRetorno.InnerXml;
    }

    private void ide_atualizaLogImportacao(int numeroProtocolo, XmlDocument xmlEntrada, XmlDocument xmlRetorno, int? nRegistrosImportar, int? nRegistrosEfetivados, int? nRegistrosIgnorados, int? nRegistrosRejeitados)
    {
        int regAfetados = 0;
        string comandoSQL = string.Format(@"
            UPDATE {0}.{1}.[ImportacaoEstrategia]
                SET 
			         [XMLEntrada] = N'{3}'
                    ,[XMLSaida] = N'{4}'
                    ,[RegistrosDetectados] = {5}
                    ,[RegistrosEfetivados] = {6}
                    ,[RegistrosIgnorados] = {7}
                    ,[RegistrosRejeitados] = {8}
             WHERE [CodigoImportacao] = {2}
 "
            , bancodb, ownerdb
            , numeroProtocolo, xmlEntrada.InnerXml, xmlRetorno.InnerXml
            , nRegistrosImportar.HasValue ? nRegistrosImportar.Value.ToString() : "NULL"
            , nRegistrosEfetivados.HasValue ? nRegistrosEfetivados.Value.ToString() : "NULL"
            , nRegistrosIgnorados.HasValue ? nRegistrosIgnorados.Value.ToString() : "NULL"
            , nRegistrosRejeitados.HasValue ? nRegistrosRejeitados.Value.ToString() : "NULL");
        cDados.execSQL(comandoSQL, ref regAfetados);
    }

    private bool ide_registraNovoProcessoIntegracao(int codigoEntidade, int? codigoUsuario, out int? numeroProtocolo, out DateTime dataIntegracao)
    {
        bool bRet;
        string strCodigoUsuario;
        numeroProtocolo = null;
        dataIntegracao = DateTime.MinValue;

        if (codigoUsuario.HasValue)
            strCodigoUsuario = codigoUsuario.Value.ToString();
        else
            strCodigoUsuario = "NULL";


        string comandoSQL = string.Format(@"
                DECLARE @DataAtual DateTime
                SET @DataAtual = GETDATE()
                INSERT INTO {0}.{1}.[ImportacaoEstrategia]
	            (
                      [DataImportacao]
                    , [CodigoEntidade]
                    , [CodigoUsuarioImportacao]
                )
	            VALUES
	            ( @DataAtual, {2}, {3})
                
                SELECT SCOPE_IDENTITY() AS [NumeroProtocolo], @DataAtual AS [DataIntegracao] "
            , bancodb, ownerdb
            , codigoEntidade, strCodigoUsuario);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            numeroProtocolo = int.Parse(ds.Tables[0].Rows[0]["NumeroProtocolo"].ToString());
            dataIntegracao = (DateTime)ds.Tables[0].Rows[0]["DataIntegracao"];
            bRet = true;
        }
        else
            bRet = false;

        return bRet;
    }

    private void ide_processaIntegracao(int numeroProtocolo, int codigoEntidade, int codigoUsuario, XmlDocument xmlEntrada, XmlDocument xmlRetorno, XmlNode returnNode, bool ignoraDataApuracao, out int? nRegistrosImportar, out int? nRegistrosEfetivados, out int? nRegistrosIgnorados, out int? nRegistrosRejeitados)
    {
        const string K_str_TipoErro_DadoNaoInformado = "Dado não informado.";
        const string K_str_TipoErro_DadoIncorreto = "Dado incorreto.";
        const string K_str_TipoErro_DadoNaoLocalizado = "Dado não localizado na base de dados.";
        const string K_str_TipoErro_DadoDesatualizado = "Dado desatualizado.";
        const string K_str_TipoErro_PeriodoNaoEditavel = "Período não editável.";
        const string K_str_TipoErro_ErroInterno = "Erro interno na aplicação.";

        string strValor, mensagemDeErro;
        int nValor;
        decimal dValor;

        XmlNode xmlEfetivados, xmlIgnorados, xmlRejeitados;
        XmlNode node;

        // formato para converter a data a importar;
        IFormatProvider ifpBR = new CultureInfo("pt-BR", false);

        // formato para valores em decimais;
        System.Globalization.CultureInfo ciEN = new CultureInfo("en-US", false);

        nRegistrosImportar = nRegistrosEfetivados = nRegistrosIgnorados = nRegistrosRejeitados = 0;
        int alertas_efetiv, erros_reject, alertas_ignor, nRegistrosIncluidos, nRegistrosAtualizados;
        alertas_efetiv = erros_reject = alertas_ignor = nRegistrosIncluidos = nRegistrosAtualizados = 0;

        // registros efetivados e rejeitados
        xmlEfetivados = xmlRetorno.CreateElement("registrosEfetivados");
        xmlIgnorados = xmlRetorno.CreateElement("registrosIgnorados");
        xmlRejeitados = xmlRetorno.CreateElement("registrosRejeitados");
        int sequenciaImportacao = 1;

        #region ===== Importação de Metas =====
        XmlNodeList xmlMetas = xmlEntrada.SelectNodes("integracaoEstrategia/metasEstrategicas");

        if (xmlMetas.Count > 0)
        {
            XmlNode registroSaida, erros, erro, alertas, alerta;
            ide_Meta meta = new ide_Meta();
            ide_Meta metaBD = new ide_Meta();

            foreach (XmlNode nodeMeta in xmlMetas[0].ChildNodes)
            {
                mensagemDeErro = "";
                meta.Ano = null;
                meta.Mes = null;
                meta.CodigoUnidade = null;
                meta.CodigoIndicador = null;
                meta.ValorMeta = null;
                meta.DataApuracao = DateTime.MinValue;
                meta.TipoOperacaoSolicitada = ide_OperacaoSolicitada.NaoInformada;
                meta.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Nenhuma;

                registroSaida = xmlRetorno.CreateElement("registro");
                erros = xmlRetorno.CreateElement("erros");
                alertas = xmlRetorno.CreateElement("alertas");

                #region === - Unidade - ===

                strValor = "";
                node = nodeMeta["codigoReservadoUnidade"];
                if (node != null)
                    strValor = node.InnerXml;

                meta.CodigoUnidadeInformada = strValor;
                node = xmlRetorno.CreateElement("codigoReservadoUnidade");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o código reservado da unidade
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o código reservado da unidade.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!ide_existeUnidade(codigoEntidade, strValor, out meta.CodigoUnidade))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoLocalizado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi localizada UNIDADE com o código reservado informado.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }

                #endregion

                #region === - Indicador - ===

                strValor = "";
                node = nodeMeta["codigoReservadoIndicador"];
                if (node != null)
                    strValor = node.InnerXml;

                meta.CodigoIndicadorInformado = strValor;
                node = xmlRetorno.CreateElement("codigoReservadoIndicador");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o código reservado do indicador
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o código reservado do indicador.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!ide_existeIndicadorEstrategia(meta.CodigoUnidade, strValor, out meta.CodigoIndicador))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoLocalizado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi localizado INDICADOR com o código reservado informado para a unidade em questão.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                #endregion

                node = xmlRetorno.CreateElement("tipoRegistro");
                node.InnerXml = "Meta";
                registroSaida.AppendChild(node);

                #region === - Ano - ===

                strValor = "";
                node = nodeMeta["ano"];
                if (node != null)
                    strValor = node.InnerXml;

                meta.AnoInformado = strValor;
                node = xmlRetorno.CreateElement("ano");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o ano
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o ano de referência da meta.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!int.TryParse(strValor, out nValor))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Ano inválido.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!ide_AnoPermiteRegistroDeMeta(codigoEntidade, nValor))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_PeriodoNaoEditavel;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"O ano informado não está registrado no sistema com permissão de registro de metas.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    meta.Ano = nValor;

                #endregion

                #region === - Mês - ===

                strValor = "";
                node = nodeMeta["mes"];
                if (node != null)
                    strValor = node.InnerXml;

                meta.MesInformado = strValor;
                node = xmlRetorno.CreateElement("mes");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o mês
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o mês de referência da meta.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if ((!int.TryParse(strValor, out nValor)) || (nValor < 1) || (nValor > 12))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Mês inválido.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    meta.Mes = nValor;
                #endregion

                #region === - Valor Meta - ===

                strValor = "";
                node = nodeMeta["valor"];
                if (node != null)
                    strValor = node.InnerXml;

                meta.ValorInformado = strValor;
                node = xmlRetorno.CreateElement("valor");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o valor da meta
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o valor da meta.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!decimal.TryParse(strValor, out dValor))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Valor de meta inválido.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    meta.ValorMeta = dValor;
                #endregion

                #region === - Data de Apuração - ===

                strValor = "";
                node = nodeMeta["dataApuracaoInformacao"];
                if (node != null)
                    strValor = node.InnerXml;

                meta.DataApuracaoInformada = strValor;
                node = xmlRetorno.CreateElement("dataApuracaoInformacao");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o tipo de operacação
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informada a DATA DE APURACÃO do dado";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                } // tenta converter a data usando o formato FULL dd/MM/YYYY hh:nn:ss
                else if (!DateTime.TryParse(strValor, ifpBR, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out meta.DataApuracao))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi possível reconhecer a DATA DE APURAÇÃO informada.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                #endregion

                #region === - Tipo de Operação - ===

                strValor = "";
                node = nodeMeta["tipoOperacao"];
                if (node != null)
                    strValor = node.InnerXml;

                meta.TipoOperacaoInformada = strValor;
                node = xmlRetorno.CreateElement("operacaoSolicitada");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o tipo de operacação
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado tipo de operação ao ser realizada";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if ("G".IndexOf(node.InnerXml.ToUpper()) < 0) // todo: incluir posteriormente operação de Exclusão
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    //node.InnerXml = @"O tipo de operação informado está incorreto. Os valores permitidos são G ou E (gravação ou exclusão de meta).";
                    node.InnerXml = @"O tipo de operação informado está incorreto. Os valores permitidos são G ou g (gravação de meta).";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    meta.TipoOperacaoSolicitada = ide_OperacaoSolicitada.Gravar;
                #endregion

                // tratamento caso ocorra erro no servidor de BD
                try
                {
                    //se não houve erro até aqui, é por que todos os valores estão ok no XML.
                    // passando então às demais verificações;
                    if (erros.ChildNodes.Count == 0)
                    {
                        mensagemDeErro = " ao verificar as informações no servidor de banco de dados.";
                        // se a meta existir já existir na base de dados
                        if (ide_obtemMetaRegistradaBD(meta, ref metaBD))
                        {
                            // se não é para ignorar a data de apuração, alerta para meta com data de apuração mais antiga
                            if ((ignoraDataApuracao == false) && (metaBD.DataApuracao > meta.DataApuracao))
                            {
                                erro = xmlRetorno.CreateElement("erro");

                                node = xmlRetorno.CreateElement("tipoErro");
                                node.InnerXml = K_str_TipoErro_DadoDesatualizado;
                                erro.AppendChild(node);

                                node = xmlRetorno.CreateElement("descricao");
                                node.InnerXml = @"Já existe a meta registrada no sistema com data de apuração mais atual que a informada.";
                                erro.AppendChild(node);
                                erros.AppendChild(erro);
                                meta.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Ignorado;
                                nRegistrosIgnorados++;
                            }
                        }
                    }
                    else
                    {
                        meta.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Rejeitado;
                        nRegistrosRejeitados++;
                    }

                    if (erros.ChildNodes.Count == 0)
                    {
                        if ((metaBD.ValorMeta != null) && (metaBD.ValorMeta == meta.ValorMeta) && (metaBD.DataApuracao == meta.DataApuracao))
                        {
                            alerta = xmlRetorno.CreateElement("alerta");
                            alerta.InnerXml = @"Meta já registrada no sistema. Registro ignorado.";
                            alertas.AppendChild(alerta);
                            meta.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Ignorado;
                            nRegistrosIgnorados++;
                        } // if ((metaBD.ValorMeta != null) && 
                        else
                        {
                            mensagemDeErro = " ao registrar os valores no servidor de banco de dados.";
                            ide_registraNovaMeta(meta, codigoUsuario, ref ciEN);

                            if (metaBD.ValorMeta == null)
                            {
                                meta.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Incluido;
                                nRegistrosIncluidos++;
                            }
                            else
                            {
                                meta.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Atualizado;
                                nRegistrosAtualizados++;
                            }
                        } // else ((metaBD.ValorMeta != null) && 
                    } // if (erros.ChildNodes.Count == 0)

                    node = xmlRetorno.CreateElement("operacaoEfetuada");
                    switch (meta.TipoOperacaoEfetuada)
                    {
                        case ide_OperacaoEfetuada.Incluido:
                            node.InnerXml = "Registro incluído";
                            break;
                        case ide_OperacaoEfetuada.Atualizado:
                            node.InnerXml = "Registro alterado";
                            break;
                        case ide_OperacaoEfetuada.Ignorado:
                            node.InnerXml = "Registro ignorado";
                            break;
                        case ide_OperacaoEfetuada.Rejeitado:
                            node.InnerXml = "Registro rejeitado";
                            break;
                    }
                    registroSaida.AppendChild(node); // adiciona o nó operacaoEfetuada

                    if (erros.ChildNodes.Count > 0)
                        registroSaida.AppendChild(erros);

                    if (alertas.ChildNodes.Count > 0)
                        registroSaida.AppendChild(alertas);

                    mensagemDeErro = " ao gravar informações no servidor sobre o registro sendo processado.";
                    ide_registraDetalheImportacao(numeroProtocolo, ref sequenciaImportacao, "Meta", meta);

                    if ((erros.ChildNodes.Count > 0) || (alertas.ChildNodes.Count > 0))
                    {
                        mensagemDeErro = " ao gravar informações no servidor sobre erros do registro sendo processado.";
                        ide_registraErrosImportacaoRegistro(numeroProtocolo, sequenciaImportacao, erros, alertas);
                    }

                    mensagemDeErro = "";
                    if (meta.TipoOperacaoEfetuada == ide_OperacaoEfetuada.Nenhuma)
                        throw new Exception("Falha na implementação das instruções... O registro já deveria ter sido processado");

                    try
                    {
                        switch (meta.TipoOperacaoEfetuada)
                        {
                            case ide_OperacaoEfetuada.Atualizado:
                            case ide_OperacaoEfetuada.Incluido:
                                xmlEfetivados.AppendChild(registroSaida);
                                alertas_efetiv += alertas.ChildNodes.Count;
                                break;
                            case ide_OperacaoEfetuada.Ignorado:
                                xmlIgnorados.AppendChild(registroSaida);
                                alertas_ignor += alertas.ChildNodes.Count;
                                break;
                            case ide_OperacaoEfetuada.Rejeitado:
                                xmlRejeitados.AppendChild(registroSaida);
                                erros_reject += erros.ChildNodes.Count;
                                break;
                        }
                    } // não se espera erro nesta parte do código, mas de qq forma, adota-se uma proteção.(continua caso ocorra erro)
                    catch
                    { };

                }
                catch (Exception e)
                {
                    string msgErro = "";
                    // cria mais um registro de erro para o registro
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_ErroInterno;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    if (meta.TipoOperacaoEfetuada == ide_OperacaoEfetuada.Nenhuma)
                    {
                        msgErro = string.Format(@"Ocorreu um erro interno {0} durante o processamento do registro. O registro foi descartado. Entre em contato com o administrador do sistema. ", mensagemDeErro);
                        meta.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Rejeitado;
                    }
                    else
                        msgErro = string.Format(@"Ocorreu um erro interno {0} após o processamento do registro. O erro não afetou o processamento do registro.", mensagemDeErro);

                    node.InnerXml = msgErro + string.Format(@"Mesagem original do erro: {0}.", e.Message.Replace("'", "''"));
                    erro.AppendChild(node);
                    erros.AppendChild(erro);

                    registroSaida.AppendChild(erros);
                    switch (meta.TipoOperacaoEfetuada)
                    {
                        case ide_OperacaoEfetuada.Atualizado:
                        case ide_OperacaoEfetuada.Incluido:
                            xmlEfetivados.AppendChild(registroSaida);
                            break;
                        case ide_OperacaoEfetuada.Ignorado:
                            xmlIgnorados.AppendChild(registroSaida);
                            break;
                        case ide_OperacaoEfetuada.Rejeitado:
                            xmlRejeitados.AppendChild(registroSaida);
                            break;
                    }
                } // catch (Exception e)
            } // foreach (XmlNode nodeMeta in xmlMetas[0].ChildNodes)

        }// if (xmlMetas.Count > 0)

        #endregion

        #region ===== Importação de Resultados =====
        XmlNodeList xmlResultados = xmlEntrada.SelectNodes("integracaoEstrategia/resultadosEstrategicos");

        if (xmlResultados.Count > 0)
        {
            XmlNode registroSaida, erros, erro, alertas, alerta;
            ide_Resultado resultado = new ide_Resultado();
            ide_Resultado resultadoBD = new ide_Resultado();

            int anoAtual = DateTime.Today.Year;
            int mesAtual = DateTime.Today.Month;

            foreach (XmlNode nodeResultado in xmlResultados[0].ChildNodes)
            {
                mensagemDeErro = "";
                resultado.Ano = null;
                resultado.Mes = null;
                resultado.CodigoUnidade = null;
                resultado.CodigoDado = null;
                resultado.ValorResultado = null;
                resultado.DataApuracao = DateTime.MinValue;
                resultado.TipoOperacaoSolicitada = ide_OperacaoSolicitada.NaoInformada;
                resultado.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Nenhuma;

                registroSaida = xmlRetorno.CreateElement("registro");
                erros = xmlRetorno.CreateElement("erros");
                alertas = xmlRetorno.CreateElement("alertas");

                #region === - Unidade - ===

                strValor = "";
                node = nodeResultado["codigoReservadoUnidade"];
                if (node != null)
                    strValor = node.InnerXml;

                resultado.CodigoUnidadeInformada = strValor;
                node = xmlRetorno.CreateElement("codigoReservadoUnidade");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o código reservado da unidade
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o código reservado da unidade.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!ide_existeUnidade(codigoEntidade, strValor, out resultado.CodigoUnidade))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoLocalizado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi localizada UNIDADE com o código reservado informado.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }

                #endregion

                #region === - Indicador - ===

                strValor = "";
                node = nodeResultado["codigoReservadoDado"];
                if (node != null)
                    strValor = node.InnerXml;

                resultado.CodigoDadoInformado = strValor;
                node = xmlRetorno.CreateElement("codigoReservadoDado");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o código reservado do indicador
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o código reservado do dado.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!ide_existeDadoEstrategia(resultado.CodigoUnidade, strValor, out resultado.CodigoDado))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoLocalizado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi localizado DADO com o código reservado informado na unidade em questão.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                #endregion

                node = xmlRetorno.CreateElement("tipoRegistro");
                node.InnerXml = "Resultado";
                registroSaida.AppendChild(node);

                #region === - Ano - ===

                strValor = "";
                node = nodeResultado["ano"];
                if (node != null)
                    strValor = node.InnerXml;

                resultado.AnoInformado = strValor;
                node = xmlRetorno.CreateElement("ano");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o ano
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o ano de referência do resultado.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!int.TryParse(strValor, out nValor))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Ano inválido.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!ide_AnoPermiteRegistroDeResultado(codigoEntidade, nValor))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_PeriodoNaoEditavel;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"O ano informado não está registrado no sistema com permissão de registro de resultados.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    resultado.Ano = nValor;

                #endregion

                #region === - Mês - ===

                strValor = "";
                node = nodeResultado["mes"];
                if (node != null)
                    strValor = node.InnerXml;

                resultado.MesInformado = strValor;
                node = xmlRetorno.CreateElement("mes");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o mês
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o mês de referência do resultado.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if ((!int.TryParse(strValor, out nValor)) || (nValor < 1) || (nValor > 12))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Mês inválido.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    resultado.Mes = nValor;

                if (resultado.Ano.HasValue && resultado.Mes.HasValue && (
                    (resultado.Ano.Value > anoAtual) || ((resultado.Ano.Value == anoAtual) && (resultado.Mes >= mesAtual))))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_PeriodoNaoEditavel;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"O ano e mês informados referem-se a período ainda não fechado.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }

                #endregion

                #region === - Valor Resultado - ===

                strValor = "";
                node = nodeResultado["valor"];
                if (node != null)
                    strValor = node.InnerXml;

                resultado.ValorInformado = strValor;
                node = xmlRetorno.CreateElement("valor");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o valor do resultado
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado o valor do dado.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if (!decimal.TryParse(strValor, out dValor))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Valor do dado inválido.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    resultado.ValorResultado = dValor;
                #endregion

                #region === - Data de Apuração - ===

                strValor = "";
                node = nodeResultado["dataApuracaoInformacao"];
                if (node != null)
                    strValor = node.InnerXml;

                resultado.DataApuracaoInformada = strValor;
                node = xmlRetorno.CreateElement("dataApuracaoInformacao");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o tipo de operacação
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informada a DATA DE APURACÃO do dado";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                } // tenta converter a data usando o formato FULL dd/MM/YYYY hh:nn:ss
                else if (!DateTime.TryParse(strValor, ifpBR, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out resultado.DataApuracao))
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi possível reconhecer a DATA DE APURAÇÃO informada.";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                #endregion

                #region === - Tipo de Operação - ===

                strValor = "";
                node = nodeResultado["tipoOperacao"];
                if (node != null)
                    strValor = node.InnerXml;

                resultado.TipoOperacaoInformada = strValor;
                node = xmlRetorno.CreateElement("operacaoSolicitada");
                node.InnerXml = strValor;
                registroSaida.AppendChild(node);
                // se não foi informado o tipo de operacação
                if (strValor == "")
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoNaoInformado;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    node.InnerXml = @"Não foi informado tipo de operação ao ser realizada";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else if ("G".IndexOf(node.InnerXml.ToUpper()) < 0) // todo: incluir posteriormente operação de Exclusão
                {
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_DadoIncorreto;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    //node.InnerXml = @"O tipo de operação informado está incorreto. Os valores permitidos são G ou E (gravação ou exclusão de resultado).";
                    node.InnerXml = @"O tipo de operação informado está incorreto. Os valores permitidos são G ou g (gravação de resultado).";
                    erro.AppendChild(node);

                    erros.AppendChild(erro);
                }
                else
                    resultado.TipoOperacaoSolicitada = ide_OperacaoSolicitada.Gravar;
                #endregion

                // tratamento caso ocorra erro no servidor de BD
                try
                {
                    //se não houve erro até aqui, é por que todos os valores estão ok no XML.
                    // passando então às demais verificações;
                    if (erros.ChildNodes.Count == 0)
                    {
                        mensagemDeErro = " ao verificar as informações no servidor de banco de dados.";
                        // se o resultado já existir na base de dados
                        if (ide_obtemResultadoRegistradoBD(resultado, ref resultadoBD))
                        {
                            // se não é para ignorar a data de apuração, alerta para resultado com data de apuração mais antiga
                            if ((ignoraDataApuracao == false) && (resultadoBD.DataApuracao > resultado.DataApuracao))
                            {
                                erro = xmlRetorno.CreateElement("erro");

                                node = xmlRetorno.CreateElement("tipoErro");
                                node.InnerXml = K_str_TipoErro_DadoDesatualizado;
                                erro.AppendChild(node);

                                node = xmlRetorno.CreateElement("descricao");
                                node.InnerXml = @"Já existe o resultado registrado no sistema com data de apuração mais atual que a informada.";
                                erro.AppendChild(node);
                                erros.AppendChild(erro);
                                resultado.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Ignorado;
                                nRegistrosIgnorados++;
                            }
                        }
                    }
                    else
                    {
                        resultado.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Rejeitado;
                        nRegistrosRejeitados++;
                    }

                    if (erros.ChildNodes.Count == 0)
                    {
                        if ((resultadoBD.ValorResultado != null) && (resultadoBD.ValorResultado == resultado.ValorResultado) && (resultadoBD.DataApuracao == resultado.DataApuracao))
                        {
                            alerta = xmlRetorno.CreateElement("alerta");
                            alerta.InnerXml = @"Resultado já registrado no sistema. Registro ignorado.";
                            alertas.AppendChild(alerta);
                            resultado.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Ignorado;
                            nRegistrosIgnorados++;
                        } // if ((resultadoBD.ValorMeta != null) && 
                        else
                        {
                            mensagemDeErro = " ao registrar os valores no servidor de banco de dados.";
                            ide_registraNovoResultado(resultado, codigoUsuario, ref ciEN);

                            if (resultadoBD.ValorResultado == null)
                            {
                                resultado.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Incluido;
                                nRegistrosIncluidos++;
                            }
                            else
                            {
                                resultado.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Atualizado;
                                nRegistrosAtualizados++;
                            }
                        } // else ((resultadoBD.ValorMeta != null) && 
                    } // if (erros.ChildNodes.Count == 0)

                    node = xmlRetorno.CreateElement("operacaoEfetuada");
                    switch (resultado.TipoOperacaoEfetuada)
                    {
                        case ide_OperacaoEfetuada.Incluido:
                            node.InnerXml = "Registro incluído";
                            break;
                        case ide_OperacaoEfetuada.Atualizado:
                            node.InnerXml = "Registro alterado";
                            break;
                        case ide_OperacaoEfetuada.Ignorado:
                            node.InnerXml = "Registro ignorado";
                            break;
                        case ide_OperacaoEfetuada.Rejeitado:
                            node.InnerXml = "Registro rejeitado";
                            break;
                    }
                    registroSaida.AppendChild(node); // adiciona o nó operacaoEfetuada

                    if (erros.ChildNodes.Count > 0)
                        registroSaida.AppendChild(erros);

                    if (alertas.ChildNodes.Count > 0)
                        registroSaida.AppendChild(alertas);

                    mensagemDeErro = " ao gravar informações no servidor sobre o registro sendo processado.";
                    ide_registraDetalheImportacao(numeroProtocolo, ref sequenciaImportacao, "Resultado", resultado);

                    if ((erros.ChildNodes.Count > 0) || (alertas.ChildNodes.Count > 0))
                    {
                        mensagemDeErro = " ao gravar informações no servidor sobre erros do registro sendo processado.";
                        ide_registraErrosImportacaoRegistro(numeroProtocolo, sequenciaImportacao, erros, alertas);
                    }

                    mensagemDeErro = "";
                    if (resultado.TipoOperacaoEfetuada == ide_OperacaoEfetuada.Nenhuma)
                        throw new Exception("Falha na implementação das instruções... O registro já deveria ter sido processado");

                    try
                    {
                        switch (resultado.TipoOperacaoEfetuada)
                        {
                            case ide_OperacaoEfetuada.Atualizado:
                            case ide_OperacaoEfetuada.Incluido:
                                xmlEfetivados.AppendChild(registroSaida);
                                alertas_efetiv += alertas.ChildNodes.Count;
                                break;
                            case ide_OperacaoEfetuada.Ignorado:
                                xmlIgnorados.AppendChild(registroSaida);
                                alertas_ignor += alertas.ChildNodes.Count;
                                break;
                            case ide_OperacaoEfetuada.Rejeitado:
                                xmlRejeitados.AppendChild(registroSaida);
                                erros_reject += erros.ChildNodes.Count;
                                break;
                        }
                    } // não se espera erro nesta parte do código, mas de qq forma, adota-se uma proteção.(continua caso ocorra erro)
                    catch
                    { };

                }
                catch (Exception e)
                {
                    string msgErro = "";
                    // cria mais um registro de erro para o registro
                    erro = xmlRetorno.CreateElement("erro");

                    node = xmlRetorno.CreateElement("tipoErro");
                    node.InnerXml = K_str_TipoErro_ErroInterno;
                    erro.AppendChild(node);

                    node = xmlRetorno.CreateElement("descricao");
                    if (resultado.TipoOperacaoEfetuada == ide_OperacaoEfetuada.Nenhuma)
                    {
                        msgErro = string.Format(@"Ocorreu um erro interno {0} durante o processamento do registro. O registro foi descartado. Entre em contato com o administrador do sistema. ", mensagemDeErro);
                        resultado.TipoOperacaoEfetuada = ide_OperacaoEfetuada.Rejeitado;
                    }
                    else
                        msgErro = string.Format(@"Ocorreu um erro interno {0} após o processamento do registro. O erro não afetou o processamento do registro.", mensagemDeErro);

                    node.InnerXml = msgErro + string.Format(@"Mesagem original do erro: {0}.", e.Message.Replace("'", "''"));
                    erro.AppendChild(node);
                    erros.AppendChild(erro);

                    registroSaida.AppendChild(erros);
                    switch (resultado.TipoOperacaoEfetuada)
                    {
                        case ide_OperacaoEfetuada.Atualizado:
                        case ide_OperacaoEfetuada.Incluido:
                            xmlEfetivados.AppendChild(registroSaida);
                            break;
                        case ide_OperacaoEfetuada.Ignorado:
                            xmlIgnorados.AppendChild(registroSaida);
                            break;
                        case ide_OperacaoEfetuada.Rejeitado:
                            xmlRejeitados.AppendChild(registroSaida);
                            break;
                    }
                } // catch (Exception e)
            } // foreach (XmlNode nodeResultado in xmlResultados[0].ChildNodes)

        }// if (xmlResultados.Count > 0)

        #endregion

        #region ===== Sumarização do processamento =====

        nRegistrosEfetivados = nRegistrosIncluidos + nRegistrosAtualizados;

        XmlAttribute atributo;

        #region === Atributos de xmlEfetivados ===

        atributo = xmlRetorno.CreateAttribute("quantidade");
        atributo.Value = nRegistrosEfetivados.ToString();
        xmlEfetivados.Attributes.Append(atributo);

        atributo = xmlRetorno.CreateAttribute("incluidos");
        atributo.Value = nRegistrosIncluidos.ToString();
        xmlEfetivados.Attributes.Append(atributo);

        atributo = xmlRetorno.CreateAttribute("atualizados");
        atributo.Value = nRegistrosAtualizados.ToString();
        xmlEfetivados.Attributes.Append(atributo);

        atributo = xmlRetorno.CreateAttribute("alertas");
        atributo.Value = alertas_efetiv.ToString();
        xmlEfetivados.Attributes.Append(atributo);

        #endregion

        #region === Atributos de xmlIgnorados ===

        atributo = xmlRetorno.CreateAttribute("quantidade");
        atributo.Value = nRegistrosIgnorados.ToString();
        xmlIgnorados.Attributes.Append(atributo);

        atributo = xmlRetorno.CreateAttribute("alertas");
        atributo.Value = alertas_ignor.ToString();
        xmlIgnorados.Attributes.Append(atributo);

        #endregion

        #region === Atributos de xmlRejeitados ===

        atributo = xmlRetorno.CreateAttribute("quantidade");
        atributo.Value = nRegistrosRejeitados.ToString();
        xmlRejeitados.Attributes.Append(atributo);

        atributo = xmlRetorno.CreateAttribute("erros");
        atributo.Value = erros_reject.ToString();
        xmlRejeitados.Attributes.Append(atributo);

        #endregion

        returnNode.AppendChild(xmlEfetivados);
        returnNode.AppendChild(xmlIgnorados);
        returnNode.AppendChild(xmlRejeitados);

        nRegistrosImportar = nRegistrosEfetivados + nRegistrosIgnorados + nRegistrosRejeitados;

        #endregion
    }

    private void ide_registraErrosImportacaoRegistro(int numeroProtocolo, int sequenciaImportacao, XmlNode erros, XmlNode alertas)
    {
        //todo: implementar ide_registraErrosImportacaoRegistro
        //throw new Exception("The method or operation is not implemented.");
    }

    private void ide_registraDetalheImportacao(int numeroProtocolo, ref int sequenciaImportacao, string tipoRegistro, ide_RegistroImportacao reg)
    {
        #region ======  -- Formatação para gravar na base de dados --  ======
        string codigoUnidadeInformada, codigoIndicadorInformado, codigoDadoInformado;
        string anoInformado, mesInformado, valorInformado, dataApuracaoInformada;

        if (reg.CodigoUnidadeInformada.Length == 0)
            codigoUnidadeInformada = "NULL";
        else if (reg.CodigoUnidadeInformada.Length > 50)
            codigoUnidadeInformada = reg.CodigoUnidadeInformada.Substring(0, 50);
        else
            codigoUnidadeInformada = reg.CodigoUnidadeInformada;

        codigoUnidadeInformada.Replace("'", "''");
        if (reg.CodigoUnidadeInformada.Length > 0)
            codigoUnidadeInformada = string.Format("'{0}'", codigoUnidadeInformada);

        if (reg.CodigoIndicadorInformado.Length == 0)
            codigoIndicadorInformado = "NULL";
        else if (reg.CodigoIndicadorInformado.Length > 50)
            codigoIndicadorInformado = reg.CodigoIndicadorInformado.Substring(0, 50);
        else
            codigoIndicadorInformado = reg.CodigoIndicadorInformado;

        codigoIndicadorInformado.Replace("'", "''");
        if (reg.CodigoIndicadorInformado.Length > 0)
            codigoIndicadorInformado = string.Format("'{0}'", codigoIndicadorInformado);

        if (reg.CodigoDadoInformado.Length == 0)
            codigoDadoInformado = "NULL";
        else if (reg.CodigoDadoInformado.Length > 50)
            codigoDadoInformado = reg.CodigoDadoInformado.Substring(0, 50);
        else
            codigoDadoInformado = reg.CodigoDadoInformado;

        codigoDadoInformado.Replace("'", "''");
        if (reg.CodigoDadoInformado.Length > 0)
            codigoDadoInformado = string.Format("'{0}'", codigoDadoInformado);

        if (reg.AnoInformado.Length == 0)
            anoInformado = "NULL";
        else if (reg.AnoInformado.Length > 50)
            anoInformado = reg.AnoInformado.Substring(0, 50);
        else
            anoInformado = reg.AnoInformado;

        anoInformado.Replace("'", "''");
        if (reg.AnoInformado.Length > 0)
            anoInformado = string.Format("'{0}'", anoInformado);

        if (reg.MesInformado.Length == 0)
            mesInformado = "NULL";
        else if (reg.MesInformado.Length > 50)
            mesInformado = reg.MesInformado.Substring(0, 50);
        else
            mesInformado = reg.MesInformado;

        mesInformado.Replace("'", "''");
        if (reg.MesInformado.Length > 0)
            mesInformado = string.Format("'{0}'", mesInformado);

        if (reg.ValorInformado.Length == 0)
            valorInformado = "NULL";
        else if (reg.ValorInformado.Length > 50)
            valorInformado = reg.ValorInformado.Substring(0, 50);
        else
            valorInformado = reg.ValorInformado;

        valorInformado.Replace("'", "''");
        if (reg.ValorInformado.Length > 0)
            valorInformado = string.Format("'{0}'", valorInformado);

        if (reg.DataApuracaoInformada.Length == 0)
            dataApuracaoInformada = "NULL";
        else if (reg.DataApuracaoInformada.Length > 50)
            dataApuracaoInformada = reg.DataApuracaoInformada.Substring(0, 50);
        else
            dataApuracaoInformada = reg.DataApuracaoInformada;

        dataApuracaoInformada.Replace("'", "''");
        if (reg.DataApuracaoInformada.Length > 0)
            dataApuracaoInformada = string.Format("'{0}'", dataApuracaoInformada);

        #endregion

        string registroEfetivado;

        switch (reg.TipoOperacaoEfetuada)
        {
            case ide_OperacaoEfetuada.Incluido:
            case ide_OperacaoEfetuada.Atualizado:
                registroEfetivado = "S";
                break;
            default:
                registroEfetivado = "N";
                break;
        }

        int regAfetados = 0;

        string comandoSQL = string.Format(@"
                INSERT INTO {0}.{1}.[DetalheImportacaoEstrategia]
	            (
			          [CodigoImportacao]
		            , [SequenciaImportacao]
		            , [TipoRegistro]
		            , [CodigoReservadoUnidade]
		            , [CodigoReservadoIndicador]
		            , [CodigoReservadoDado]
		            , [Ano]
		            , [Mes]
		            , [Valor]
		            , [DataApuracao]
		            , [IndicaRegistroEfetivado]
	            )
	            VALUES
	            ({2}, {3}, '{4}', {5}, {6}, {7}, {8}, {9}, {10}, {11}, '{12}')       "
            , bancodb, ownerdb
            , numeroProtocolo, sequenciaImportacao, tipoRegistro
            , codigoUnidadeInformada, codigoIndicadorInformado, codigoDadoInformado
            , anoInformado, mesInformado, valorInformado, dataApuracaoInformada, registroEfetivado);

        cDados.execSQL(comandoSQL, ref regAfetados);
        sequenciaImportacao++; // atualiza a seqüência para a próxima inclusão;
    }

    private void ide_registraNovaMeta(ide_Meta meta, int codigoUsuario, ref CultureInfo ci)
    {
        int regAfetados = 0;
        string comandoSQL = string.Format(
            @"
    IF( EXISTS( SELECT 1 FROM {0}.{1}.[MetaIndicadorUnidade]	AS [miu]
            WHERE 
                    miu.[CodigoUnidadeNegocio]			= {2}
			    AND miu.[CodigoIndicador]				= {3}
				AND miu.[Ano]							= {4}
				AND miu.[Mes]							= {5}) )
	    UPDATE [dbo].[MetaIndicadorUnidade]	
		    SET 
				[ValorMeta]					    = {6}
			, [DataApuracaoMeta]		        = CONVERT(DateTime, '{7}', 103)
			, [DataUltimaAlteracao]	            = GETDATE()
			, [CodigoUsuarioUltimaAlteracao]    = {8}
		WHERE
				[CodigoUnidadeNegocio]		= {2}
			AND [CodigoIndicador]			= {3}
			AND [Ano]						= {4}
			AND [Mes]						= {5}
    ELSE
	    INSERT INTO [dbo].[MetaIndicadorUnidade] 
		( 
			  [CodigoUnidadeNegocio]
			, [CodigoIndicador]
			, [Ano]
			, [Mes]
			, [ValorMeta]
			, [DataInclusao]
			, [CodigoUsuarioInclusao]
			, [DataApuracaoMeta]
		)
	    VALUES
		(
				{2}
			, {3}
			, {4}
			, {5}
			, {6}
			, GETDATE()
			, {8}
			, CONVERT(DateTime, '{7}', 103)
		)
       ", bancodb, ownerdb
        , meta.CodigoUnidade.HasValue ? meta.CodigoUnidade.Value.ToString() : "NULL"
        , meta.CodigoIndicador.HasValue ? meta.CodigoIndicador.Value.ToString() : "NULL"
        , meta.Ano.Value, meta.Mes.Value, meta.ValorMeta.Value.ToString("F", ci)
        , string.Format("{0}/{1}/{2} {3}:{4}:{5}", meta.DataApuracao.Day, meta.DataApuracao.Month, meta.DataApuracao.Year,
            meta.DataApuracao.Hour, meta.DataApuracao.Minute, meta.DataApuracao.Second)
        , codigoUsuario);

        cDados.execSQL(comandoSQL, ref regAfetados);
    }

    private void ide_registraNovoResultado(ide_Resultado resultado, int codigoUsuario, ref CultureInfo ci)
    {
        int regAfetados = 0;
        string comandoSQL = string.Format(
            @"
    IF( EXISTS( SELECT 1 FROM {0}.{1}.[ResultadoDadoUnidade]	AS [rdu]
            WHERE 
                    rdu.[CodigoUnidadeNegocio]			= {2}
			    AND rdu.[CodigoDado]				    = {3}
				AND rdu.[Ano]							= {4}
				AND rdu.[Mes]							= {5}) )
	    UPDATE [dbo].[ResultadoDadoUnidade]	
		    SET 
				[ValorResultado]			    = {6}
			, [DataApuracaoResultado]		    = CONVERT(DateTime, '{7}', 103)
			, [DataUltimaAlteracao]	            = GETDATE()
			, [CodigoUsuarioUltimaAlteracao]    = {8}
		WHERE
				[CodigoUnidadeNegocio]		= {2}
			AND [CodigoDado]			    = {3}
			AND [Ano]						= {4}
			AND [Mes]						= {5}
    ELSE
	    INSERT INTO [dbo].[ResultadoDadoUnidade] 
		( 
			  [CodigoUnidadeNegocio]
			, [CodigoDado]
			, [Ano]
			, [Mes]
			, [ValorResultado]
			, [DataInclusao]
			, [CodigoUsuarioInclusao]
			, [DataApuracaoResultado]
		)
	    VALUES
		(
				{2}
			, {3}
			, {4}
			, {5}
			, {6}
			, GETDATE()
			, {8}
			, CONVERT(DateTime, '{7}', 103)
		)
       ", bancodb, ownerdb
        , resultado.CodigoUnidade.HasValue ? resultado.CodigoUnidade.Value.ToString() : "NULL"
        , resultado.CodigoDado.HasValue ? resultado.CodigoDado.Value.ToString() : "NULL"
        , resultado.Ano.Value, resultado.Mes.Value, resultado.ValorResultado.Value.ToString("F", ci)
        , string.Format("{0}/{1}/{2} {3}:{4}:{5}", resultado.DataApuracao.Day, resultado.DataApuracao.Month, resultado.DataApuracao.Year,
            resultado.DataApuracao.Hour, resultado.DataApuracao.Minute, resultado.DataApuracao.Second)
        , codigoUsuario);

        cDados.execSQL(comandoSQL, ref regAfetados);
    }

    private bool ide_obtemMetaRegistradaBD(ide_Meta meta, ref ide_Meta metaBD)
    {
        bool bRet;

        metaBD = new ide_Meta();

        metaBD.CodigoUnidade = null;
        metaBD.CodigoIndicador = null;
        metaBD.Ano = null;
        metaBD.Mes = null;
        metaBD.ValorMeta = null;
        metaBD.DataApuracao = DateTime.MinValue;

        string comandoSQL = string.Format(
            @"
                SELECT
			              miu.[ValorMeta]
		                , miu.[DataApuracaoMeta]
	                FROM
		                {0}.{1}.[MetaIndicadorUnidade]	AS [miu]
	                WHERE
				            miu.[CodigoUnidadeNegocio]	= {2}
		                AND miu.[CodigoIndicador]		= {3}
		                AND miu.[Ano]					= {4}
		                AND miu.[Mes]					= {5}
                 ", bancodb, ownerdb
                  , meta.CodigoUnidade.HasValue ? meta.CodigoUnidade.Value.ToString() : "NULL"
                  , meta.CodigoIndicador.HasValue ? meta.CodigoIndicador.Value.ToString() : "NULL",
                  meta.Ano.Value, meta.Mes.Value);

        DataSet ds = cDados.getDataSet(comandoSQL);
        bRet = (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0);

        if (bRet)
        {
            metaBD.CodigoUnidade = meta.CodigoUnidade;
            metaBD.CodigoIndicador = meta.CodigoIndicador;
            metaBD.Ano = meta.Ano;
            metaBD.Mes = meta.Mes;
            object valorMeta = ds.Tables[0].Rows[0]["ValorMeta"];
            object dataApuracao = ds.Tables[0].Rows[0]["DataApuracaoMeta"];

            if (valorMeta.ToString() != "")
                metaBD.ValorMeta = (decimal?)ds.Tables[0].Rows[0]["ValorMeta"];

            if (dataApuracao.ToString() != "")
                metaBD.DataApuracao = (DateTime)dataApuracao;
        }

        return bRet;
    }

    private bool ide_obtemResultadoRegistradoBD(ide_Resultado resultado, ref ide_Resultado resultadoBD)
    {
        bool bRet;

        resultadoBD = new ide_Resultado();

        resultadoBD.CodigoUnidade = null;
        resultadoBD.CodigoDado = null;
        resultadoBD.Ano = null;
        resultadoBD.Mes = null;
        resultadoBD.ValorResultado = null;
        resultadoBD.DataApuracao = DateTime.MinValue;

        string comandoSQL = string.Format(
            @"
                SELECT
			              rdu.[ValorResultado]
		                , rdu.[DataApuracaoResultado]
	                FROM
		                {0}.{1}.[ResultadoDadoUnidade]	AS [rdu]
	                WHERE
				            rdu.[CodigoUnidadeNegocio]	= {2}
		                AND rdu.[CodigoDado]    		= {3}
		                AND rdu.[Ano]					= {4}
		                AND rdu.[Mes]					= {5}
                 ", bancodb, ownerdb
                  , resultado.CodigoUnidade.HasValue ? resultado.CodigoUnidade.Value.ToString() : "NULL"
                  , resultado.CodigoDado.HasValue ? resultado.CodigoDado.Value.ToString() : "NULL",
                  resultado.Ano.Value, resultado.Mes.Value);

        DataSet ds = cDados.getDataSet(comandoSQL);
        bRet = (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0);

        if (bRet)
        {
            resultadoBD.CodigoUnidade = resultado.CodigoUnidade;
            resultadoBD.CodigoDado = resultado.CodigoDado;
            resultadoBD.Ano = resultado.Ano;
            resultadoBD.Mes = resultado.Mes;
            object valorResult = ds.Tables[0].Rows[0]["ValorResultado"];
            object dataApuracao = ds.Tables[0].Rows[0]["DataApuracaoResultado"];

            if (valorResult.ToString() != "")
                resultadoBD.ValorResultado = (decimal?)ds.Tables[0].Rows[0]["ValorResultado"];

            if (dataApuracao.ToString() != "")
                resultadoBD.DataApuracao = (DateTime)dataApuracao;
        }

        return bRet;
    }

    private bool ide_AnoPermiteRegistroDeMeta(int codigoEntidade, int ano)
    {
        bool bRet;
        string comandoSQL = string.Format(
            @"
SELECT  pe.[IndicaMetaEditavel] FROM {0}.{1}.[PeriodoEstrategia] pe WHERE pe.[CodigoUnidadeNegocio] = {2} AND pe.[Ano] = {3}
                 ", bancodb, ownerdb, codigoEntidade, ano);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            bRet = ds.Tables[0].Rows[0][0].ToString().ToUpper() == "S";
        else
            bRet = false;

        return bRet;
    }

    private bool ide_AnoPermiteRegistroDeResultado(int codigoEntidade, int ano)
    {
        bool bRet;
        string comandoSQL = string.Format(
            @"
SELECT  pe.[IndicaResultadoEditavel] FROM {0}.{1}.[PeriodoEstrategia] pe WHERE pe.[CodigoUnidadeNegocio] = {2} AND pe.[Ano] = {3}
                 ", bancodb, ownerdb, codigoEntidade, ano);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            bRet = ds.Tables[0].Rows[0][0].ToString().ToUpper() == "S";
        else
            bRet = false;

        return bRet;
    }

    private bool ide_existeIndicadorEstrategia(int? codigoUnidade, string codigoReservadoIndicador, out int? codigoIndicador)
    {
        bool bRet;
        codigoIndicador = null;
        codigoReservadoIndicador.Replace("'", "''");
        string comandoSQL = string.Format(
            @"
        SELECT iu2.[CodigoIndicador] 
					FROM {0}.{1}.[IndicadorUnidade] iu 
						INNER JOIN {0}.{1}.[IndicadorUnidade]	iu2 ON 
							(		iu2.[CodigoIndicador]   = iu.[CodigoIndicador]
								AND iu2.DataExclusao		IS NULL
								AND iu2.IndicaUnidadeCriadoraIndicador = 'S' 
								AND iu2.[CodigoReservado]   = '{3}' )
					WHERE iu.[CodigoUnidadeNegocio] = {2} 
						AND iu.DataExclusao	    	IS NULL;
                 ", bancodb, ownerdb, codigoUnidade.HasValue ? codigoUnidade.Value.ToString() : "NULL", codigoReservadoIndicador);

        DataSet ds = cDados.getDataSet(comandoSQL);
        bRet = (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0);

        if (bRet)
            codigoIndicador = (int)ds.Tables[0].Rows[0][0];

        return bRet;
    }

    private bool ide_existeDadoEstrategia(int? codigoUnidade, string codigoReservadoDado, out int? codigoDado)
    {
        bool bRet;
        codigoDado = null;
        codigoReservadoDado.Replace("'", "''");
        string comandoSQL = string.Format(
            @"
        SELECT du2.[CodigoDado] 
					FROM {0}.{1}.[DadoUnidade] du 
						INNER JOIN {0}.{1}.[DadoUnidade]	du2 ON 
							(		du2.[CodigoDado]				= du.[CodigoDado]
								AND du2.[DataExclusao]				IS NULL
								AND du2.IndicaUnidadeCriadoraDado	= 'S'
								AND du2.[CodigoReservado]			= '{3}' )
					WHERE
								du.CodigoUnidadeNegocio	= {2}
						AND du.[DataExclusao]			IS NULL;
                 ", bancodb, ownerdb, codigoUnidade.HasValue ? codigoUnidade.Value.ToString() : "NULL", codigoReservadoDado);

        DataSet ds = cDados.getDataSet(comandoSQL);
        bRet = (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0);

        if (bRet)
            codigoDado = (int)ds.Tables[0].Rows[0][0];

        return bRet;
    }

    /// <summary>
    /// Localiza na base de dados um unidade que seja da entidade passada como parâmetro <paramref name="codigoEntidade"/> e que tenha
    /// o código reservado informado no parâmetro <paramref name="codigoReservadoUnidade"/> 
    /// </summary>
    /// <param name="codigoEntidade"></param>
    /// <param name="codigoReservadoUnidade"></param>
    /// <param name="codigoUnidade"></param>
    /// <returns></returns>
    private bool ide_existeUnidade(int codigoEntidade, string codigoReservadoUnidade, out int? codigoUnidade)
    {
        bool bRet;
        codigoUnidade = null;
        codigoReservadoUnidade.Replace("'", "''");
        string comandoSQL = string.Format(
            @"SELECT un.[CodigoUnidadeNegocio] FROM {0}.{1}.[UnidadeNegocio] AS [un] WHERE un.[CodigoEntidade] = {2} AND un.[CodigoReservado] = '{3}'
                 ", bancodb, ownerdb, codigoEntidade, codigoReservadoUnidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        bRet = (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0);

        if (bRet)
            codigoUnidade = (int)ds.Tables[0].Rows[0][0];

        return bRet;
    }

    private bool ide_validaCredenciais(string siglaEntidade, string idUsuario, string senha, out int? outCodigoUsuario, out int? outCodigoEntidade, out ide_ResultadoValidacaoAcesso outResultadoValidacao)
    {
        // inicia como credenciais inválidas;
        outCodigoUsuario = null;
        outCodigoEntidade = null;
        outResultadoValidacao = ide_ResultadoValidacaoAcesso.credenciaisInvalidas;
        if ((idUsuario != "") && (senha != ""))
        {
            string nomeUsuario = "";
            string IDEstiloVisual = "";
            string usuarioDeveAlterarSenha = "";
            int codigoUsuario = 0;

            codigoUsuario = cDados.getAutenticacaoUsuario(idUsuario, senha.GetHashCode(), "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

            // se não conseguiu autenticar, tenta pelo metodo onde a senha é gerada pelo metodo desenvolvido pela CDIS - ObtemCodigoHash()
            if (codigoUsuario == 0)
            {
                codigoUsuario = cDados.getAutenticacaoUsuario(idUsuario, cDados.ObtemCodigoHash(senha), "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
            }

            // se é um usuário válido...
            if (codigoUsuario > 0)
            {
                outCodigoUsuario = codigoUsuario; // usuário existente na base e com senha validada;

                string siglaEntidadeBD;
                DataSet ds = cDados.getEntidadesUsuario(codigoUsuario, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuario);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    siglaEntidadeBD = row["SiglaUnidadeNegocio"].ToString();
                    if (siglaEntidadeBD.ToLower() == siglaEntidade.ToLower())
                    {
                        outCodigoEntidade = (int)row["CodigoUnidadeNegocio"];
                        if (VerificaPermissaoUsuario(outCodigoUsuario.Value, outCodigoEntidade.Value, "ST_ExeMecItg"))
                            outResultadoValidacao = ide_ResultadoValidacaoAcesso.acessoValidado;
                        else
                            outResultadoValidacao = ide_ResultadoValidacaoAcesso.semPermissaoParaExportar;

                        break; // já que localizou a entidade entre as entidades do usuário, sai do loop
                    } // if (codigoReservadoBD.ToLower() == ...
                } // foreach (System.Data.DataRow  ...

                // se terminou o loop sem encontrar a entidade mencionada entre as entidades do usuário
                // registra que o usuário não tem acesso à entidade e tenta localizar a entidade em questão
                if (outResultadoValidacao == ide_ResultadoValidacaoAcesso.credenciaisInvalidas)
                {
                    outResultadoValidacao = ide_ResultadoValidacaoAcesso.semAcessoAEntidade;
                    ide_localizaEntidade(siglaEntidade, out outCodigoEntidade);
                } // if (outResultadoValidacao == ...
            } // if (codigoUsuario > 0) ...
        } // if ((idUsuario != "") && (senha != ""))
        return true;
    }

    /// <summary>
    /// Localiza uma determinada entidade com base no seu código reservado.
    /// </summary>
    /// <param name="siglaEntidade"></param>
    /// <param name="outCodigoEntidade">Conterá o código da entidade caso tenha sido localizada</param>
    private void ide_localizaEntidade(string siglaEntidade, out int? outCodigoEntidade)
    {
        bool bRet;
        outCodigoEntidade = null;
        siglaEntidade.Replace("'", "''");
        string comandoSQL = string.Format(
            @"SELECT un.[CodigoUnidadeNegocio] FROM {0}.{1}.[UnidadeNegocio] AS [un] WHERE un.[SiglaUnidadeNegocio] = '{2}' AND un.CodigoUnidadeNegocio = un.CodigoEntidade 
                 ", bancodb, ownerdb, siglaEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        bRet = (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0);

        if (bRet)
            outCodigoEntidade = (int)ds.Tables[0].Rows[0][0];
    }

    public bool VerificaPermissaoUsuario(int codigoUsuario, int CodigoEntidade, string iniciaisTela)
    {
        bool bPermissao = false;

        string comandoSQL = string.Format(@"SELECT {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, {3}, null, 'EN', 0, null, '{4}') AS Permissao", bancodb, ownerdb, codigoUsuario, CodigoEntidade, iniciaisTela);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
            if (ds.Tables[0].Rows.Count > 0)
                bPermissao = (bool)ds.Tables[0].Rows[0]["Permissao"];

        return bPermissao;
    }

    /// <summary>
    /// Retorna verdadeiro caso o usuário tenha perfil de administrador na entidade em questão
    /// </summary>
    /// <remarks> 
    /// Foi feito uma cópia da versão da cDados por que no processo de integração não há o objeto SESSÃO 
    /// disponível.
    /// </remarks>
    /// <param name="codigoUsuario"></param>
    /// <param name="CodigoEntidade"></param>
    /// <returns></returns>
    public bool ide_perfilAdministrador(int codigoUsuario, int CodigoEntidade)
    {
        bool bRetorno = false;

        string comandoSQL = string.Format(@"
            SELECT TOP 1 1 FROM {0}.{1}.f_GetPerfisUsuario({2}, {3}) WHERE [IniciaisPerfil] = 'ADM'
            ", bancodb, ownerdb, codigoUsuario, CodigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
            if (ds.Tables[0].Rows.Count > 0)
                bRetorno = true;

        return bRetorno;
    }

    #endregion

    [WebMethod]
    public string getIDSessao(string key)
    {
        if (key == _key)
        {
            return "1";
        }
        else
            return key;
    }

    [WebMethod]
    public string getDataHoraAtualBancoDeDados(string key)
    {
        if (key == _key)
        {
            string comandoSQL = "SELECT convert(varchar,GetDate(), 103) + ' ' + convert(varchar,GetDate(), 108)";
            DataSet ds = cDados.getDataSet(comandoSQL);
            return ds.Tables[0].Rows[0][0].ToString();
        }
        else
            return "-1";
    }

    // Este método foi criado para que as tabelas do portal possam ser consultadas pela aplicação "SQLCDIS"
    [WebMethod]
    public DataSet getDataSet(string key, string nomeTabela, string comandoSQL, ref int regAf)
    {
        if (key == _key)
        {
            return cDados.getDataSet(comandoSQL);
        }
        else
            return null;
    }

    [WebMethod]
    public int ObtemCodigoHash(string str)
    {
        return cDados.ObtemCodigoHash(str);
    }

    [WebMethod]
    public int getCodigoUsuarioAutenticacaoSistema(string key, string login, int senha, out string nomeUsuario)
    {
        nomeUsuario = "";
        if (key == _key)
        {
            string IDEstiloVisual = "";
            string usuarioDeveAlterarSenha = "";
            return cDados.getAutenticacaoUsuario(login, senha, "AS", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
        }
        else
            return -1;
    }

    [WebMethod]
    public DataSet getListaProjetoDisponiveis(string key, int codigoUsuario)
    {
        if (key == _key)
        {
            //TODO: mostar apenas os projetos permitidos ao usuário do parametro
            string comandoSQL = string.Format(
                @"SELECT CodigoCronogramaProjeto, NomeProjeto, ISNULL(cp.DataUltimaAlteracao, cp.DataInclusaoServidor) as DataAtualizacao,
                         u.NomeUsuario as NomeUsuarioCheckoutCronograma, 
                         convert(varchar, DataCheckoutCronograma, 103) + ' ' + convert(varchar, DataCheckoutCronograma, 108) as DataCheckoutCronograma
                    FROM {0}.{1}.CronogramaProjeto CP left join 
                         {0}.{1}.Usuario u on (u.CodigoUsuario = CP.CodigoUsuarioCheckoutCronograma)
                  ORDER BY u.NomeUsuario
                 ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario);

            return cDados.getDataSet(comandoSQL);

        }
        return null;
    }

    [WebMethod]
    public DataSet getProjetoToDesktop(string key, string codigoProjeto, int codigoUsuario, bool somenteLeitura)
    {
        if (key == _key)
        {
            string comandoSQL = "";
            try
            {
                //TODO: Verificar se o usuário do parametro pode obter o projeto
                comandoSQL = string.Format(
                    @"SELECT CP.*, UCO.NomeUsuario NomeUsuarioCheckOut
                    FROM {0}.{1}.CronogramaProjeto CP left join
                         {0}.{1}.Projeto P on (P.CodigoProjeto = CP.CodigoProjeto) left join
                         Usuario UCO on UCO.CodigoUsuario = CP.CodigoUsuarioCheckoutCronograma
                  WHERE CodigoCronogramaProjeto = '{2}'

                  SELECT MAX(SequenciaTarefaCronograma) as ultimaLinha,
                         MAX(CodigoTarefa) as MaiorCodigoTarefa
                    FROM {0}.{1}.TarefaCronogramaProjeto 
                   WHERE CodigoCronogramaProjeto = '{2}'
                    
 
                  SELECT * 
                    FROM  {0}.{1}.TarefaCronogramaProjeto
                   WHERE CodigoCronogramaProjeto = '{2}'
                     AND dataExclusao is null

                 ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto,
                        codigoUsuario);
                DataSet ds = cDados.getDataSet(comandoSQL);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    // Se o projeto está com checkout para alguem, deve ser aberto somente para leitura
                    if (ds.Tables[0].Rows[0]["DataCheckoutCronograma"].ToString() != "")
                        somenteLeitura = true;

                    if (!somenteLeitura)
                    {
                        comandoSQL = string.Format(
                            @"UPDATE {0}.{1}.CronogramaProjeto
                                 SET DataCheckoutCronograma = GetDate()
                                   , CodigoUsuarioCheckoutCronograma = {3}
                              WHERE CodigoCronogramaProjeto = '{2}'

                             ", cDados.getDbName(), cDados.getDbOwner(),
                                codigoProjeto, codigoUsuario);

                        int regAfetados = 0;
                        cDados.execSQL(comandoSQL, ref regAfetados);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + comandoSQL);
            }
        }
        return null;
    }

    [WebMethod]
    public DataSet getProjetoEmpresa(string key, int codigoProjeto)
    {
        if (key == _key)
        {
            //TODO: Verificar se o usuário do parametro pode obter o projeto
            string comandoSQL = string.Format(
                @"SELECT NomeProjeto
                    FROM {0}.{1}.Projeto
                  WHERE CodigoProjeto = {2}

                  SELECT CodigoCronogramaProjeto
                    FROM {0}.{1}.CronogramaProjeto
                  WHERE CodigoProjeto = {2}
                 ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

            return cDados.getDataSet(comandoSQL);
        }
        return null;
    }

    [WebMethod]
    public bool ChekinCronograma(string key, int codigoUsuario, string guidProjeto)
    {
        if (key == _key)
        {
            if (guidProjeto != "" && guidProjeto.Length == 36)
            {
                string comandoSQL = "";
                try
                {
                    comandoSQL = string.Format(
                        @"UPDATE {0}.{1}.CronogramaProjeto
                             SET CodigoUsuarioCheckoutCronograma = null
                               , DataCheckoutCronograma = null
                          WHERE CodigoCronogramaProjeto = '{2}'

                     ", cDados.getDbName(), cDados.getDbOwner(),
                            guidProjeto);

                    // executa os comandos SQL
                    int afetatos = 0;
                    cDados.execSQL(comandoSQL, ref afetatos);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " " + comandoSQL);
                }
            }
        }
        return false;
    }

    [WebMethod]
    public bool execP_AtualizaStatusProjetos(string key, int codigoUsuario, string guidProjeto)
    {
        if (key == _key)
        {
            if (guidProjeto != "" && guidProjeto.Length == 36)
            {
                string comandoSQL = "";
                try
                {
                    // atualiza a tabela de tarefas indicando que existe pendencias nas tarefas relacionadas a linha de base
                    comandoSQL = string.Format(
                        @"DECLARE @NomeUsuario varchar(20)
                              SET @NomeUsuario = (SELECT convert(varchar, codigoUsuario) +';'+ left(nomeUsuario, 40) 
                                                    FROM {0}.{1}.usuario 
                                                   WHERE CodigoUsuario = {2} )

                          DECLARE @CodigoProjeto int
                              SET @CodigoProjeto = (SELECT CodigoProjeto 
                                                      FROM {0}.{1}.CronogramaProjeto 
                                                     WHERE CodigoCronogramaProjeto = '{3}')

                           EXEC {0}.{1}.p_AtualizaStatusProjetos @NomeUsuario, @CodigoProjeto
                     ", cDados.getDbName(), cDados.getDbOwner(),
                        codigoUsuario, guidProjeto);

                    // executa os comandos SQL
                    int afetatos = 0;
                    cDados.execSQL(comandoSQL, ref afetatos);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " " + comandoSQL);
                }
            }
        }
        return false;
    }

    [WebMethod]
    public DataSet getRecursoCronogramaProjeto(string key, string codigoProjeto, int codigoUsuario)
    {
        if (key == _key)
        {
            string comandoSQL = string.Format(
                @"SELECT CodigoTipoRecurso, DescricaoTipoRecurso
                    FROM {0}.{1}.TipoRecurso

                  SELECT RCP.CodigoCronogramaProjeto, RCP.CodigoRecursoProjeto, RCP.CodigoRecursoCorporativo, RCP.NomeRecurso, 
                         RCP.CustoHora, RCP.CustoUso, RCP.EMail, RCP.CodigoGrupoRecurso, RCP.NomeGrupoRecurso, RCP.CodigoTipoRecurso, 
                         RCP.Anotacoes, TR.DescricaoTipoRecurso,
                         case when CodigoRecursoCorporativo is null then 'Não' else 'Sim' end as RecursoCorporativo,
                         RCP.UnidadeMedidaRecurso
                    FROM {0}.{1}.RecursoCronogramaProjeto RCP inner join
                         {0}.{1}.TipoRecurso TR on (TR.CodigoTipoRecurso = RCP.CodigoTipoRecurso)
                   WHERE CodigoCronogramaProjeto = '{2}'
                   ORDER BY RCP.NomeRecurso   
                 ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto,
                    codigoUsuario);

            return cDados.getDataSet(comandoSQL);
        }
        return null;
    }

    [WebMethod]
    public DataSet getAtribuicaoRecursoTarefa(string key, string codigoProjeto, int? codigoTarefa, int codigoUsuario)
    {
        if (key == _key)
        {
            string whereCodigoTarefa = "";
            if (codigoTarefa.HasValue)
                whereCodigoTarefa = " AND ART.CodigoTarefa = " + codigoTarefa.Value;

            string comandoSQL = string.Format(
                @"SELECT TR.DescricaoTipoRecurso, ART.CodigoAtribuicao, ART.CodigoTarefa, RCP.CodigoRecursoProjeto, 
                         RCP.NomeRecurso, rcp.CustoHora, rcp.CustoUso, rcp.CodigoTipoRecurso,
                         ART.Trabalho, ART.Inicio, ART.Termino, ART.Custo, ART.UnidadeAtribuicao, 
                         ART.IndicaAtribuicaoRecursoCorporativo, rcp.CustoHora * ART.UnidadeAtribuicao as CustoRecurso,
                         ART.PercentualFisicoConcluido, ART.TrabalhoLB, ART.TrabalhoReal, ART.InicioLB, ART.InicioReal, 
                         ART.TerminoLB, ART.TerminoReal, ART.CustoLB, ART.CustoReal, 
                         convert(char(1),'N') as Alterado
                    FROM {0}.{1}.RecursoCronogramaProjeto RCP inner join
                         {0}.{1}.TipoRecurso TR on (TR.CodigoTipoRecurso = RCP.CodigoTipoRecurso) left join
                         {0}.{1}.AtribuicaoRecursoTarefa ART on (ART.CodigoCronogramaProjeto = RCP.CodigoCronogramaProjeto AND
                                                                 ART.CodigoRecursoProjeto = RCP.CodigoRecursoProjeto  {3} ) 
                   WHERE RCP.CodigoCronogramaProjeto = '{2}'    
                   ORDER BY RCP.NomeRecurso
                 ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, whereCodigoTarefa);

            return cDados.getDataSet(comandoSQL);
        }
        return null;
    }

    [WebMethod]
    public DataSet getRecursosCorporativos(string key, string CodigoCronogramaProjeto, bool OcultarRecursoCronograma, int codigoUsuario, int CodigoEntidade)
    {
        if (key == _key)
        {
            string comandoSQL = "";
            try
            {
                string recursosCronograma = "";
                string where = "";
                // se tem codigo do cronograma é para mostrar os recursos corporativos que estejam associados a ele ou para mostrar os que ainda não estão associados a ele.
                if (CodigoCronogramaProjeto != "")
                {
                    // OcultarRecursoCronograma define se é para mostrar os RC associados ou se é para ocultá-los
                    string tipoJoin = OcultarRecursoCronograma ? "LEFT " : "INNER ";
                    recursosCronograma = string.Format(
                        @" {2} JOIN
                       (SELECT RCP.CodigoRecursoCorporativo
                          FROM {0}.{1}.RecursoCronogramaProjeto RCP
                         WHERE RCP.CodigoCronogramaProjeto = '{3}') RCP on (RCP.CodigoRecursoCorporativo = RC.CodigoRecursoCorporativo)
                     ", cDados.getDbName(), cDados.getDbOwner(), tipoJoin, CodigoCronogramaProjeto);

                    //se for para ocultar os recursos que já estão associados ao cronograma
                    if (OcultarRecursoCronograma)
                        where = " AND RCP.CodigoRecursoCorporativo is null";
                }

                comandoSQL = string.Format(
                    @"SELECT TR.DescricaoTipoRecurso, RC.CodigoTipoRecurso, RC.NomeRecursoCorporativo, RC.CustoHora, 
                             RC.CustoUso, RC.CodigoRecursoCorporativo, GR.DescricaoGrupo, GR.CodigoGrupoRecurso, RC.UnidadeMedidaRecurso
                    FROM  {0}.{1}.RecursoCorporativo RC inner join
                          {0}.{1}.TipoRecurso TR on (TR.CodigoTipoRecurso = RC.CodigoTipoRecurso) inner join
                          {0}.{1}.GrupoRecurso GR on (GR.CodigoGrupoRecurso = RC.CodigoGrupoRecurso ) {2}
                    WHERE RC.CodigoEntidade = {3} {4} AND RC.[DataDesativacaoRecurso] IS NULL 
                    ORDER By RC.NomeRecursoCorporativo
                 ", cDados.getDbName(), cDados.getDbOwner(), recursosCronograma, CodigoEntidade, where);

                return cDados.getDataSet(comandoSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + comandoSQL);
            }
        }
        return null;
    }

    [WebMethod]
    public int getPermissaoLinhaBase(string key, int codigoProjetoEmpresa, int codigoUsuario)
    {
        if (key == _key)
        {
            string comandoSQL = string.Format(
                @"SELECT {0}.{1}.f_GetPermissaoLinhaBase({2}, {3})
                 ", cDados.getDbName(), cDados.getDbOwner(), codigoProjetoEmpresa, codigoUsuario);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        return -1;
    }

    /// <summary>
    /// Retorna um calendário da empresa
    /// </summary>
    /// <param name="key"></param>
    /// <param name="codigoEntidade"></param>
    /// <param name="codigoCalendario">Código do calendário desejado. Se for null, retorna o calendário padrão</param>
    /// <returns></returns>
    [WebMethod]
    public XmlDocument getXmlCalendarioEmpresa(string key, int codigoEntidade, int? codigoCalendario)
    {
        if (key == _key)
        {
            string _CodigoCalendario = "";
            if (codigoCalendario.HasValue)
                _CodigoCalendario = "SET @CodigoCalendario = " + codigoCalendario.Value;
            else
                _CodigoCalendario = string.Format(
                    @"SET @CodigoCalendario = 
                            (SELECT CodigoCalendario 
                               FROM {0}.{1}.Calendario
                              WHERE IndicaCalendarioPadrao = 'S'
                                AND CodigoEntidade = {2} ) ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade);

            string comandoSQL = string.Format(
                @"
                    DECLARE @CodigoCalendario int
                        {2} 

                    SELECT codigoCalendario, diaSemana,
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4,
                           IndicaHorarioPadrao
                      FROM {0}.{1}.CalendarioDiaSemana
                     WHERE CodigoCalendario = @CodigoCalendario

                    SELECT convert(varchar, Data, 103) as Data,
                           IndicaDiaUtil, 
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4
                      FROM {0}.{1}.DetalheCalendarioDiaSemana DC inner join
                           {0}.{1}.CalendarioDiaSemana CDS on (CDS.codigoCalendario = DC.codigoCalendario AND
                                                       CDS.diaSemana = DC.diaSemana)
                     WHERE DC.CodigoCalendario IN (SELECT codigoCalendario FROM Calendario WHERE CodigoCalendarioBase = @CodigoCalendario )
                     ORDER BY Data

                 ", cDados.getDbName(), cDados.getDbOwner(), _CodigoCalendario);

            DataSet ds = cDados.getDataSet(comandoSQL);

            // se não encontrou nenhuma informação, retorna nulo
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;

            // constrói o XML
            XmlDocument xmlCalendario = new XmlDocument();

            // Declaração do XML
            XmlDeclaration xmlDeclaration = xmlCalendario.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlCalendario.AppendChild(xmlDeclaration);

            // Cria o elemento raiz - root element
            XmlNode rootNode = xmlCalendario.AppendChild(xmlCalendario.CreateElement("Calendario"));

            // calendário
            rootNode.AppendChild(xmlCalendario.CreateElement("Nome"));

            // regras padrão para os dias da semana
            XmlNode diasSemana = rootNode.AppendChild(xmlCalendario.CreateElement("DiasSemana"));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                byte qtdTurnoDia = 0;
                XmlNode dia = diasSemana.AppendChild(xmlCalendario.CreateElement("dia"));
                XmlAttribute atributo = xmlCalendario.CreateAttribute("id");
                atributo.Value = row["DiaSemana"].ToString();
                dia.Attributes.Append(atributo);

                // se tem turno1
                if (row["HoraInicioTurno1"].ToString() != "")
                    qtdTurnoDia++;
                // se tem turno2
                if (row["HoraInicioTurno2"].ToString() != "")
                    qtdTurnoDia++;
                // se tem turno3
                if (row["HoraInicioTurno3"].ToString() != "")
                    qtdTurnoDia++;
                // se tem turno4
                if (row["HoraInicioTurno4"].ToString() != "")
                    qtdTurnoDia++;

                atributo = xmlCalendario.CreateAttribute("turnos");
                atributo.Value = qtdTurnoDia + "";
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("InicioTurno1");
                atributo.Value = row["HoraInicioTurno1"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("TerminoTurno1");
                atributo.Value = row["HoraTerminoTurno1"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("InicioTurno2");
                atributo.Value = row["HoraInicioTurno2"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("TerminoTurno2");
                atributo.Value = row["HoraTerminoTurno2"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("InicioTurno3");
                atributo.Value = row["HoraInicioTurno3"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("TerminoTurno3");
                atributo.Value = row["HoraTerminoTurno3"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("InicioTurno4");
                atributo.Value = row["HoraInicioTurno4"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("TerminoTurno4");
                atributo.Value = row["HoraTerminoTurno4"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("HorarioPadrao");
                atributo.Value = row["IndicaHorarioPadrao"].ToString();
                dia.Attributes.Append(atributo);

                diasSemana.AppendChild(dia);
            }

            // Exceções
            // regras padrão para os dias da semana
            XmlNode Excecoes = rootNode.AppendChild(xmlCalendario.CreateElement("Excecoes"));
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                XmlNode dia = Excecoes.AppendChild(xmlCalendario.CreateElement("excecao"));
                XmlAttribute atributo = xmlCalendario.CreateAttribute("data");
                atributo.Value = row["data"].ToString();
                dia.Attributes.Append(atributo);

                atributo = xmlCalendario.CreateAttribute("diaUtil");
                atributo.Value = row["IndicaDiaUtil"].ToString();
                dia.Attributes.Append(atributo);

                byte _qtdTurno = 0;
                for (byte qtdTurno = 1; qtdTurno < 4; qtdTurno++)
                {
                    if (row["HoraInicioTurno" + qtdTurno].ToString() != "")
                    {
                        TimeSpan inicioTurno = DateTime.ParseExact(row["HoraInicioTurno" + qtdTurno].ToString(), "dd/MM/yyyy HH:mm:ss", null).TimeOfDay;
                        TimeSpan terminoTurno = DateTime.ParseExact(row["HoraTerminoTurno" + qtdTurno].ToString(), "dd/MM/yyyy HH:mm:ss", null).TimeOfDay;
                        TimeSpan duracao = terminoTurno.Subtract(inicioTurno);

                        string periodo = inicioTurno.ToString().Substring(0, 5) + " " + terminoTurno.ToString().Substring(0, 5) + " " + duracao.TotalMinutes;

                        atributo = xmlCalendario.CreateAttribute("turno" + qtdTurno);
                        atributo.Value = periodo;
                        dia.Attributes.Append(atributo);

                        _qtdTurno++;
                    }
                }
                if (_qtdTurno > 0)
                {
                    atributo = xmlCalendario.CreateAttribute("turnos");
                    atributo.Value = _qtdTurno + "";
                    dia.Attributes.Append(atributo);
                }
                Excecoes.AppendChild(dia);
            }

            return xmlCalendario;
        }
        else
            return null;

    }

    /// <summary>
    /// Retorna o calendário PADRÃO da empresa
    /// </summary>
    /// <param name="key"></param>
    /// <param name="codigoEntidade"></param>
    /// <param name="codigoCalendario">Código do calendário desejado. Se for null, retorna o calendário padrão</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet getCalendarioEmpresa(string key, int codigoEntidade, int? codigoCalendario)
    {
        if (key == _key)
        {
            string _CodigoCalendario = "";
            if (codigoCalendario.HasValue)
                _CodigoCalendario = "SET @CodigoCalendario = " + codigoCalendario.Value;
            else
                _CodigoCalendario = string.Format(
                    @"SET @CodigoCalendario = 
                            (SELECT CodigoCalendario 
                               FROM {0}.{1}.Calendario
                              WHERE IndicaCalendarioPadrao = 'S'
                                AND CodigoEntidade = {2} ) ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade);

            string comandoSQL = string.Format(
                @"
                    DECLARE @CodigoCalendario int
                        {2} 

                    Select 8 horasDia, 40 horasSemana, 20 diasMes

                    SELECT codigoCalendario, diaSemana,
                           case when horaInicioTurno1 is null then 'N' else 'S' end as IndicaDiaUtil,
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4,
                           IndicaHorarioPadrao
                      FROM {0}.{1}.CalendarioDiaSemana
                     WHERE CodigoCalendario = @CodigoCalendario

                    SELECT convert(varchar, Data, 103) as Data,
                           IndicaDiaUtil, 
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4
                      FROM {0}.{1}.DetalheCalendarioDiaSemana DC inner join
                           {0}.{1}.CalendarioDiaSemana CDS on (CDS.codigoCalendario = DC.codigoCalendario AND
                                                       CDS.diaSemana = DC.diaSemana)
                     WHERE DC.CodigoCalendario IN (SELECT codigoCalendario FROM Calendario WHERE CodigoCalendarioBase = @CodigoCalendario )
                     ORDER BY Data

                 ", cDados.getDbName(), cDados.getDbOwner(), _CodigoCalendario);

            DataSet ds = cDados.getDataSet(comandoSQL);
            ds.Tables[0].TableName = "Limites";
            ds.Tables[1].TableName = "Calendario";
            ds.Tables[2].TableName = "Excecoes";
            return ds;
        }
        else
            return null;

    }

    /// <summary>
    /// Retorna TODOS os Calendários da empresa
    /// </summary>
    /// <param name="key"></param>
    /// <param name="codigoEntidade"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet getCalendariosEmpresa(string key, int codigoEntidade)
    {
        if (key == _key)
        {
            //TODO: Verificar se o usuário do parametro pode obter o projeto
            string comandoSQL = string.Format(
                @"SELECT c.CodigoCalendario, c.DescricaoCalendario, c.IndicaCalendarioPadrao
                    FROM {0}.{1}.AssociacaoCalendario ac inner join
                         {0}.{1}.TipoAssociacao ta on ta.CodigoTipoAssociacao = ac.CodigoTipoAssociacao inner join
                         {0}.{1}.Calendario c on c.CodigoCalendario = ac.CodigoCalendario
                   WHERE ta.IniciaisTipoAssociacao = 'EN'
                     AND c.CodigoEntidade = {2}
                   ORDER BY c.DescricaoCalendario ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade);

            return cDados.getDataSet(comandoSQL);
        }
        return null;
    }


    /// <summary>
    /// Recebe o arquivo xml da aplicação desktop e salva-o na pasta padrão
    /// </summary>
    /// <param name="key"></param>
    /// <param name="guidProjeto"></param>
    /// <param name="arquivoInByte"></param>
    /// <returns></returns>
    [WebMethod]
    public bool uploadCronograma(string key, string guidProjeto, byte[] arquivoInByte, out DateTime DataServidor)
    {
        DataServidor = DateTime.Now;
        if (key == _key)
        {
            // se existir arquivo, vamos inserir a imagem
            if (guidProjeto != "" && guidProjeto.Length == 36 && arquivoInByte.Length > 0)
            {
                // se o diretório não existir, será criado
                if (!Directory.Exists(diretorioCronogramas))
                    Directory.CreateDirectory(diretorioCronogramas);

                string arquivo = diretorioCronogramas + "\\" + guidProjeto + ".xml";
                try
                {
                    FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
                    fs.Write(arquivoInByte, 0, arquivoInByte.Length);
                    fs.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }
        else
            return false;
    }

    [WebMethod]
    public bool uploadXMLCronograma(string key, string guidProjeto, bool publicarCronograma, XmlDocument XmlCronograma, bool cronogramaImportadoMSProject, out DateTime DataServidor)
    {
        DataServidor = DateTime.Now;
        if (key == _key)
        {
            // se existir arquivo, vamos inserir a imagem
            if (guidProjeto != "" && guidProjeto.Length == 36 && XmlCronograma != null)
            {
                // se o diretório não existir, será criado
                if (!Directory.Exists(diretorioCronogramas))
                    Directory.CreateDirectory(diretorioCronogramas);

                // Salva o cronograma no HD
                string arquivo = diretorioCronogramas + "\\" + guidProjeto + ".xml";
                XmlCronograma.Save(arquivo);

                // Salva o cronograma no banco de dados
                salvarXmlCronograma(publicarCronograma, cronogramaImportadoMSProject, XmlCronograma);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Salva a linha de base para o cronograma indicado em guidProjeto
    /// </summary>
    /// <param name="key"></param>
    /// <param name="codigoUsuario">Usuário que está salvando a linha de base</param>
    /// <param name="PermissaoLinhaBase">(Opcional) Indica a permissão do usuário para salvar linha de base</param>
    /// <param name="guidProjeto">Código do cronograma</param>
    /// <param name="codigoTarefas">Array com as tarefas a serem salvas. Nulo para todas as tarefas</param>
    /// <param name="anotacoes">Anotações</param>
    /// <returns></returns>
    [WebMethod]
    public bool salvarLinhaBase(string key, int codigoUsuario, int? PermissaoLinhaBase, string guidProjeto, int[] codigoTarefas, string anotacoes)
    {
        if (key == _key)
        {
            if (guidProjeto != "" && guidProjeto.Length == 36)
            {
                string comandoSQL = "";
                try
                {
                    // se codigoTarefa é nulo, todas as tarefas ficam pendentes
                    string whereTarefas = "";
                    // caso contrário, apenas as tarefas indicadas no vetor ficarão pendentes
                    if (codigoTarefas != null)
                    {
                        whereTarefas = " AND CodigoTarefa in (";
                        foreach (int codigoTarefa in codigoTarefas)
                            whereTarefas += codigoTarefa + ", ";
                        whereTarefas = whereTarefas.Substring(0, whereTarefas.Length - 2) + ")";
                    }

                    // atualiza a tabela de tarefas indicando que existe pendencias nas tarefas relacionadas a linha de base
                    comandoSQL = string.Format(
                        @"UPDATE {0}.{1}.TarefaCronogramaProjeto
                         SET IndicaLinhaBasePendente = 'S'
                       WHERE CodigoCronogramaProjeto = '{2}'  {3}

                     ", cDados.getDbName(), cDados.getDbOwner(),
                            guidProjeto, whereTarefas);

                    // se PermissaoLinhaBase = 2 gravar status = 'AP" // se PermissaoLinhaBase = 1 gravar status = "PA"
                    string StatusAprovacao = (PermissaoLinhaBase.HasValue && PermissaoLinhaBase.Value == 2) ? "AP" : "PA";

                    // insere um registro na tabela LinhaBaseCronograma
                    comandoSQL += string.Format(
                        @"DECLARE @VersaoLinhaBase int
                          SET @VersaoLinhaBase = (SELECT ISNULL(MAX(VersaoLinhaBase),0)+1 AS VersaoLinhaBase
                                                    FROM {0}.{1}.LinhaBaseCronograma 
                                                   WHERE CodigoCronogramaProjeto = '{2}')

                       INSERT INTO {0}.{1}.LinhaBaseCronograma
                            (CodigoCronogramaProjeto, VersaoLinhaBase , Anotacoes, DataSolicitacao, CodigoUsuarioSolicitante, DataStatusAprovacao, StatusAprovacao, CodigoUsuarioAprovacao)
                       VALUES
                            ('{2}'                  , @VersaoLinhaBase, '{3}'    , GetDate()      , {4}                     , GetDate()          , '{5}'          , {6}                   )
                    
                     ", cDados.getDbName(), cDados.getDbOwner(),
                      guidProjeto, anotacoes, codigoUsuario, StatusAprovacao, StatusAprovacao == "AP" ? codigoUsuario.ToString() : "null");

                    // executa os comandos SQL
                    int afetatos = 0;
                    cDados.execSQL(comandoSQL, ref afetatos);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        return false;
    }

    [WebMethod]
    public bool salvarRecursoCronograma(string key, string guidProjeto, XmlDocument xmlRecursosCronograma, XmlDocument xmlAtribuicaoRecursoTarefa)
    {
        if (key == _key)
        {
            int controle = 36; // os números anteriores estão no editor de cronograma
            try
            {
                // se existir arquivo, vamos inseri-lo
                if (guidProjeto != "" && guidProjeto.Length == 36)
                {
                    bool salvouRecursoCronograma = salvarRecursosCronograma(guidProjeto, xmlRecursosCronograma);
                    controle = 37;
                    if (salvouRecursoCronograma)
                    {
                        salvarAtribuicaoRecursosCronograma(guidProjeto, xmlAtribuicaoRecursoTarefa);
                        controle = 38;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro WS: " + controle + " - " + ex.Message);
            }
        }
        return false;
    }

    private bool salvarRecursosCronograma(string guidProjeto, XmlDocument xmlRecursosCronograma)
    {
        if (xmlRecursosCronograma != null)
        {
            // recuperar os recursos
            XmlNodeList xmlRecursos = xmlRecursosCronograma.SelectNodes("RecursosCronograma/Recursos");

            if (xmlRecursos.Count > 0)
            {
                string comandoSQL = "";
                int afetatos = 0;
                int qtdRecursosInseridosComandoSQL = 0;
                try
                {
                    foreach (XmlNode nodeRecurso in xmlRecursos[0].ChildNodes)
                    {
                        string CodigoRecursoProjeto = nodeRecurso.Attributes["CodigoRecursoProjeto"].Value;
                        string AcaoDB = nodeRecurso.Attributes["AcaoDB"] != null ? nodeRecurso.Attributes["AcaoDB"].Value : "";
                        string temp = "";
                        // se é para excluir o recurso do cronograma
                        if (AcaoDB == "D")
                        {
                            temp = string.Format(
                                @"
                                -----------------------------------------------------------------------------------------------------------------------------
                                DELETE FROM {0}.{1}.AtualizacaoDiariaTarefaTimeSheet 
                                 WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                                            FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                                           WHERE CodigoCronogramaProjeto = '{2}' 
                                                             AND CodigoRecursoProjeto = {3} )

                              DELETE FROM {0}.{1}.AtribuicaoDiariaRecurso
                               WHERE CodigoCronogramaProjeto = '{2}'
                                 AND CodigoRecursoProjeto = {3}

                              DELETE FROM {0}.{1}.AtribuicaoRecursoTarefa
                               WHERE CodigoCronogramaProjeto = '{2}'
                                 AND CodigoRecursoProjeto = {3}

                                DELETE FROM {0}.{1}.RecursoCronogramaProjeto
                                 WHERE CodigoCronogramaProjeto = '{2}'
                                   AND CodigoRecursoProjeto = {3}
                            ", cDados.getDbName(), cDados.getDbOwner(), guidProjeto, CodigoRecursoProjeto);
                        }
                        else
                        {
                            string CodigoRecursoCorporativo = nodeRecurso.Attributes["CodigoRecursoCorporativo"].Value;
                            string NomeRecurso = nodeRecurso.Attributes["NomeRecurso"].Value;
                            string CustoHora = nodeRecurso.Attributes["CustoHora"].Value.Replace(',', '.');
                            string CustoUso = nodeRecurso.Attributes["CustoUso"].Value.Replace(',', '.');
                            string EMail = nodeRecurso.Attributes["EMail"].Value;
                            string NomeGrupoRecurso = nodeRecurso.Attributes["NomeGrupoRecurso"].Value;
                            string CodigoTipoRecurso = nodeRecurso.Attributes["CodigoTipoRecurso"].Value;
                            string CodigoGrupoRecurso = nodeRecurso.Attributes["CodigoGrupoRecurso"].Value;
                            string Anotacoes = nodeRecurso.Attributes["Anotacoes"].Value;
                            string UnidadeMedidaRecurso = nodeRecurso.Attributes["UnidadeMedidaRecurso"].Value;

                            temp = string.Format(
                                @"
                                -----------------------------------------------------------------------------------------------------------------------------
                                if ( not exists (Select 1 from {0}.{1}.RecursoCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}' and CodigoRecursoProjeto = {3}))

                                    INSERT INTO {0}.{1}.RecursoCronogramaProjeto 
                                        (CodigoCronogramaProjeto, CodigoRecursoProjeto, NomeRecurso, CodigoTipoRecurso, DataInclusao )
                                    VALUES ( '{2}', {3}, '{4}', {5}, getdate() ) 

                                UPDATE {0}.{1}.RecursoCronogramaProjeto
                                   SET NomeRecurso = '{4}'
                                     , CodigoTipoRecurso = {5}
                                     , CodigoRecursoCorporativo = {6}
                                     , CustoHora = {7}
                                     , CustoUso = {8}
                                     , EMail = '{9}'
                                     , CodigoGrupoRecurso = {10}
                                     , NomeGrupoRecurso = '{11}'
                                     , Anotacoes = '{12}'
                                     , UnidadeMedidaRecurso = '{13}'
                                 WHERE CodigoCronogramaProjeto = '{2}'
                                   AND CodigoRecursoProjeto = {3}
                            ", cDados.getDbName(), cDados.getDbOwner(),
                             guidProjeto, CodigoRecursoProjeto, NomeRecurso, CodigoTipoRecurso,
                             CodigoRecursoCorporativo == "" ? "null" : CodigoRecursoCorporativo,
                             CustoHora, CustoUso, // {7} e {8}
                             EMail, CodigoGrupoRecurso == "" ? "null" : CodigoGrupoRecurso,
                             NomeGrupoRecurso, Anotacoes, UnidadeMedidaRecurso);
                        }
                        comandoSQL += temp;
                        qtdRecursosInseridosComandoSQL++;
                        if (qtdRecursosInseridosComandoSQL == 100)
                        {
                            cDados.execSQL(comandoSQL, ref afetatos);
                            comandoSQL = "";
                            qtdRecursosInseridosComandoSQL = 0;
                        }
                    }

                    if (qtdRecursosInseridosComandoSQL > 0)
                        cDados.execSQL(comandoSQL, ref afetatos);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " " + comandoSQL);
                }
            }
        }
        return false;
    }

    private bool salvarAtribuicaoRecursosCronograma(string guidProjeto, XmlDocument xmlAtribuicaoRecursoTarefa)
    {
        if (xmlAtribuicaoRecursoTarefa != null)
        {
            // recuperar os recursos
            XmlNodeList xmlRecursosTarefa = xmlAtribuicaoRecursoTarefa.SelectNodes("Atribuicoes/RecursosTarefa");

            if (xmlRecursosTarefa.Count > 0)
            {
                string comandoSQL = "";
                int afetatos = 0;
                int qtdLinhasInseridasComandoSQL = 0;
                try
                {
                    foreach (XmlNode recursoTarefa in xmlRecursosTarefa[0].ChildNodes)
                    {
                        string CodigoTarefa = recursoTarefa.Attributes["CodigoTarefa"].Value;
                        string CodigoRecursoProjeto = recursoTarefa.Attributes["CodigoRecursoProjeto"].Value;
                        string Inicio = recursoTarefa.Attributes["Inicio"].Value;
                        string Termino = recursoTarefa.Attributes["Termino"].Value;
                        string Custo = recursoTarefa.Attributes["Custo"].Value.Replace(',', '.');
                        string TrabalhoReal = recursoTarefa.Attributes["TrabalhoReal"].Value.Replace(',', '.');
                        string InicioReal = recursoTarefa.Attributes["InicioReal"].Value;
                        string TerminoReal = recursoTarefa.Attributes["TerminoReal"].Value;
                        string CustoReal = recursoTarefa.Attributes["CustoReal"].Value.Replace(',', '.');
                        string UnidadeAtribuicao = recursoTarefa.Attributes["UnidadeAtribuicao"].Value.Replace(',', '.');
                        string IndicaAtribuicaoRecursoCorporativo = recursoTarefa.Attributes["IndicaAtribuicaoRecursoCorporativo"].Value;
                        string CodigoTipoRecurso = recursoTarefa.Attributes["CodigoTipoRecurso"].Value;
                        string CustoRecurso = recursoTarefa.Attributes["CustoRecurso"].Value.Replace(',', '.');

                        Custo = Custo == "" ? "null" : Custo;
                        UnidadeAtribuicao = UnidadeAtribuicao == "" ? "null" : UnidadeAtribuicao;
                        CustoRecurso = CustoRecurso == "" ? "null" : CustoRecurso;

                        TrabalhoReal = TrabalhoReal == "" ? "null" : TrabalhoReal;
                        InicioReal = InicioReal == "" ? "null" : string.Format("convert(datetime, '{0}', 103)", InicioReal);
                        TerminoReal = TerminoReal == "" ? "null" : string.Format("convert(datetime, '{0}', 103)", TerminoReal);
                        CustoReal = CustoReal == "" ? "null" : CustoReal;

                        if (UnidadeAtribuicao != "null")
                            Custo = CustoRecurso;

                        string comandoDeleleAtribuicao = "";
                        // se não tem custo e nem unidadeAtribuicao, o registro deve ser excluído do banco
                        if (Custo == "null" && UnidadeAtribuicao == "null")
                        {
                            comandoDeleleAtribuicao = string.Format(
                                @"if (1 = 1) 
                                  BEGIN

                                      DELETE FROM {0}.{1}.AtualizacaoDiariaTarefaTimeSheet 
                                       WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                                                    FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                                                   WHERE CodigoCronogramaProjeto = '{2}' 
                                                                     AND CodigoRecursoProjeto = {3}
                                                                     AND CodigoTarefa = {4}              )

                                      DELETE FROM {0}.{1}.AtribuicaoDiariaRecurso
                                       WHERE CodigoCronogramaProjeto = '{2}'
                                         AND CodigoRecursoProjeto = {3}
                                         AND CodigoTarefa = {4}

                                      DELETE FROM {0}.{1}.AtribuicaoRecursoTarefa
                                       WHERE CodigoCronogramaProjeto = '{2}'
                                         AND CodigoRecursoProjeto = {3}
                                         AND CodigoTarefa = {4}
                                  END
                                  else 
                                 ", cDados.getDbName(), cDados.getDbOwner(),
                                    guidProjeto, CodigoRecursoProjeto, CodigoTarefa);
                        }

                        string trabalho = ", Trabalho = 0 ";
                        // se o recurso é do tipo "Pessoa" ou "Equipamento", a coluna trabalho fica igual a coluna "UnidadeAtribuicao"
                        if (CodigoTipoRecurso == "1" || CodigoTipoRecurso == "2")
                            trabalho = ", Trabalho = " + UnidadeAtribuicao;

                        string temp = string.Format(
                            @"
                                -----------------------------------------------------------------------------------------------------------------------------
                                {5} 
                                BEGIN
                                    if ( not exists (Select 1 from {0}.{1}.AtribuicaoRecursoTarefa WHERE CodigoCronogramaProjeto = '{2}' and CodigoRecursoProjeto = {3} and CodigoTarefa = {4} ))

                                        INSERT INTO {0}.{1}.AtribuicaoRecursoTarefa 
                                            (CodigoCronogramaProjeto, CodigoRecursoProjeto, CodigoTarefa)
                                        VALUES ( '{2}', {3}, {4} ) 
                                    
                                    
                                    UPDATE {0}.{1}.AtribuicaoRecursoTarefa
                                       SET Inicio = convert(datetime, '{6}', 103)
                                         , Termino = convert(datetime, '{7}', 103)
                                         , Custo = {8}
                                         , TrabalhoReal = {12}
                                         , InicioReal = {13}
                                         , TerminoReal = {14}
                                         , CustoReal = {15}
                                         , UnidadeAtribuicao = {9}
                                         , IndicaAtribuicaoRecursoCorporativo = '{10}'
                                         {11}
                                     WHERE CodigoCronogramaProjeto = '{2}'
                                       AND CodigoRecursoProjeto = {3}
                                       AND CodigoTarefa = {4}
                                END
                            ", cDados.getDbName(), cDados.getDbOwner(),
                         guidProjeto, CodigoRecursoProjeto, CodigoTarefa,
                         comandoDeleleAtribuicao,
                         Inicio, Termino, Custo, UnidadeAtribuicao,
                         IndicaAtribuicaoRecursoCorporativo, trabalho, TrabalhoReal, InicioReal, TerminoReal, CustoReal);

                        comandoSQL += temp;
                        qtdLinhasInseridasComandoSQL++;
                        if (qtdLinhasInseridasComandoSQL == 100)
                        {
                            cDados.execSQL(comandoSQL, ref afetatos);
                            comandoSQL = "";
                            qtdLinhasInseridasComandoSQL = 0;
                        }
                    }

                    if (qtdLinhasInseridasComandoSQL > 0)
                        cDados.execSQL(comandoSQL, ref afetatos);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " " + comandoSQL);
                }
            }
        }
        return false;
    }

    //    [WebMethod]
    //    public bool salvarAtribuicaoRecursoTarefa(string key, string guidProjeto, XmlDocument xmlAtribuicaoRecursoTarefa)
    //    {
    //        if (key == _key)
    //        {
    //            // se existir arquivo, vamos inseri-lo
    //            if (guidProjeto != "" && guidProjeto.Length == 36 && xmlAtribuicaoRecursoTarefa != null)
    //            {
    //                // recuperar os recursos
    //                XmlNodeList xmlRecursoTarefa = xmlAtribuicaoRecursoTarefa.SelectNodes("Atribuicoes/RecursoTarefa");

    //                if (xmlRecursoTarefa.Count > 0)
    //                {
    //                    string comandoSQL = "";
    //                    int afetatos = 0;
    //                    int qtdRecursosInseridosComandoSQL = 0;
    //                    try
    //                    {
    //                        foreach (XmlNode nodeRecurso in xmlRecursoTarefa[0].ChildNodes)
    //                        {
    //                            string CodigoRecursoProjeto = nodeRecurso.Attributes["CodigoRecursoProjeto"].Value;
    //                            string CodigoRecursoCorporativo = nodeRecurso.Attributes["CodigoRecursoCorporativo"].Value;
    //                            string NomeRecurso = nodeRecurso.Attributes["NomeRecurso"].Value;
    //                            string CustoHora = nodeRecurso.Attributes["CustoHora"].Value;
    //                            string CustoUso = nodeRecurso.Attributes["CustoUso"].Value;
    //                            string EMail = nodeRecurso.Attributes["EMail"].Value;
    //                            string NomeGrupoRecurso = nodeRecurso.Attributes["NomeGrupoRecurso"].Value;
    //                            string CodigoTipoRecurso = nodeRecurso.Attributes["CodigoTipoRecurso"].Value;
    //                            string CodigoGrupoRecurso = nodeRecurso.Attributes["CodigoGrupoRecurso"].Value;
    //                            string Anotacoes = nodeRecurso.Attributes["Anotacoes"].Value;

    //                            string temp = string.Format(
    //                                @"
    //                                -----------------------------------------------------------------------------------------------------------------------------
    //                                if ( not exists (Select 1 from {0}.{1}.RecursoCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}' and CodigoRecursoProjeto = {3}))
    //
    //                                    INSERT INTO {0}.{1}.RecursoCronogramaProjeto 
    //                                        (CodigoCronogramaProjeto, CodigoRecursoProjeto, NomeRecurso, CodigoTipoRecurso )
    //                                    VALUES ( '{2}', {3}, '{4}', {5} ) 
    //
    //                                UPDATE {0}.{1}.RecursoCronogramaProjeto
    //                                   SET NomeRecurso = '{4}'
    //                                     , CodigoTipoRecurso = {5}
    //                                     , CodigoRecursoCorporativo = {6}
    //                                     , CustoHora = {7}
    //                                     , CustoUso = {8}
    //                                     , EMail = '{9}'
    //                                     , CodigoGrupoRecurso = {10}
    //                                     , NomeGrupoRecurso = '{11}'
    //                                     , Anotacoes = '{12}'
    //                                 WHERE CodigoCronogramaProjeto = '{2}'
    //                                   AND CodigoRecursoProjeto = {3}
    //                            ", cDados.getDbName(), cDados.getDbOwner(),
    //                             guidProjeto, CodigoRecursoProjeto, NomeRecurso, CodigoTipoRecurso,
    //                             CodigoRecursoCorporativo == "" ? "null" : CodigoRecursoCorporativo,
    //                             CustoHora.Replace(',', '.'), CustoUso.Replace(',', '.'), // {7} e {8}
    //                             EMail, CodigoGrupoRecurso == "" ? "null" : CodigoGrupoRecurso,
    //                             NomeGrupoRecurso, Anotacoes);

    //                            comandoSQL += temp;
    //                            qtdRecursosInseridosComandoSQL++;
    //                            if (qtdRecursosInseridosComandoSQL == 100)
    //                            {
    //                                cDados.execSQL(comandoSQL, ref afetatos);
    //                                comandoSQL = "";
    //                                qtdRecursosInseridosComandoSQL = 0;
    //                            }
    //                        }

    //                        if (qtdRecursosInseridosComandoSQL > 0)
    //                            cDados.execSQL(comandoSQL, ref afetatos);

    //                        return true;
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        throw new Exception(ex.Message + " " + comandoSQL);
    //                    }
    //                }
    //            }
    //        }
    //        return false;
    //    }

    public bool salvarXmlCronograma(bool publicarCronograma, bool cronogramaImportadoMSProject, XmlDocument XmlCronograma)
    {
        persisteIdentificacaoCronograma(publicarCronograma, cronogramaImportadoMSProject, XmlCronograma);

        persisteTarefasExcluidasCronograma(XmlCronograma);

        bool existeLinhaBase = false;
        persisteTarefasCronograma(XmlCronograma, cronogramaImportadoMSProject, out existeLinhaBase);


        // se é um cronograma importado do Project e Tem linha de base
        if (cronogramaImportadoMSProject && existeLinhaBase)
        {
            XmlNode xmlInfoProjeto = XmlCronograma.SelectSingleNode("Projeto");
            string guidProjeto = xmlInfoProjeto["Codigo"].InnerText;
            string codigoUsuario = xmlInfoProjeto["codigoUltimoUsuario_PE"].InnerText;

            // insere um registro na tabela LinhaBaseCronograma
            string anotacoes = "Linha de base importada do MSProject ";
            string comandoSQL = string.Format(
                @"DECLARE @VersaoLinhaBase int
                      SET @VersaoLinhaBase = 1

                       INSERT INTO {0}.{1}.LinhaBaseCronograma
                            (CodigoCronogramaProjeto, VersaoLinhaBase , Anotacoes, DataSolicitacao, CodigoUsuarioSolicitante, DataStatusAprovacao, StatusAprovacao, CodigoUsuarioAprovacao)
                       VALUES
                            ('{2}'                  , @VersaoLinhaBase, '{3}'    , GetDate()      , {4}                     , GetDate()          , 'AP'          , {4}                   )
                    
                     ", cDados.getDbName(), cDados.getDbOwner(),
              guidProjeto, anotacoes, codigoUsuario);

            // executa os comandos SQL
            int afetatos = 0;
            cDados.execSQL(comandoSQL, ref afetatos);

        }

        return true;
    }

    private bool persisteIdentificacaoCronograma(bool publicarCronograma, bool cronogramaImportadoMSProject, XmlDocument XmlCronograma)
    {
        string comandoSQL = "";
        int regAfetados = 0;
        try
        {
            XmlNode xmlInfoProjeto = XmlCronograma.SelectSingleNode("Projeto");
            string guidProjetoAberto = xmlInfoProjeto["Codigo"].InnerText;

            // verifica se o cronograma já existe no banco de dados
            bool novoCronograma = !getCronogramaJaExiste(guidProjetoAberto);

            // se o cronograma foi importado do MSProject sobreescrevendo um cronograma existente, tem que apagar todas as informações atuais
            if (!novoCronograma && cronogramaImportadoMSProject)
            {
                comandoSQL = string.Format(
                    @"  DELETE FROM {0}.{1}.AtualizacaoDiariaTarefaTimeSheet WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                                                                                          FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                                                                                         WHERE CodigoCronogramaProjeto='{2}' )

                        DELETE FROM {0}.{1}.AtribuicaoDiariaRecurso WHERE CodigoCronogramaProjeto = '{2}'
                        DELETE FROM {0}.{1}.AtribuicaoRecursoTarefa WHERE CodigoCronogramaProjeto = '{2}'
                        DELETE FROM {0}.{1}.AtribuicaoRecursoTarefaLinhaBase WHERE CodigoCronogramaProjeto = '{2}'
                        DELETE FROM {0}.{1}.TarefaCronogramaProjetoLinhaBase WHERE CodigoCronogramaProjeto = '{2}'
                        DELETE FROM {0}.{1}.LinhaBaseCronograma WHERE CodigoCronogramaProjeto = '{2}'
                        DELETE FROM {0}.{1}.RecursoCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}'
                        DELETE FROM {0}.{1}.TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}'
                        DELETE FROM {0}.{1}.CronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}'

                    ", cDados.getDbName(), cDados.getDbOwner(),
                       guidProjetoAberto);
                cDados.execSQL(comandoSQL, ref regAfetados);

                // passou a ser um novo cronograma
                novoCronograma = true;
            }

            // lê as informaçoes de identificação
            string NomeProjeto = xmlInfoProjeto["NomeProjeto"].InnerText;
            string DataCriacaoDesktop = xmlInfoProjeto["DataCriacao"].InnerText;
            string NomeAutor = xmlInfoProjeto["Autor"].InnerText;
            string Dominio = xmlInfoProjeto["Dominio"].InnerText;
            string versaoDesktop = xmlInfoProjeto["Versao"].InnerText;
            string SistemaOperacional = xmlInfoProjeto["SO"].InnerText;
            string IdentificadorMaquina = xmlInfoProjeto["Computador"].InnerText;
            string InicioProjeto = xmlInfoProjeto["DataInicio"].InnerText;
            string ConfiguracaoFormatoDuracao = xmlInfoProjeto["PadraoDuracao"].InnerText[0].ToString();
            string ConfiguracaoFormatoTrabalho = xmlInfoProjeto["PadraoTrabalho"].InnerText[0].ToString();
            string ConfiguracaoFormatoDataComHora = xmlInfoProjeto["MostrarHora"].InnerText;
            string ConfiguracaoCodigoMoedaProjeto = xmlInfoProjeto["CodigoMoeda"].InnerText;
            string codigoUsuario = xmlInfoProjeto["codigoUltimoUsuario_PE"].InnerText;
            string CodigoEntidade = xmlInfoProjeto["codigoEntidadeUltimoUsuario_PE"].InnerText;
            string CodigoCalendarioProjeto = xmlInfoProjeto["CodigoCalendarioProjeto"].InnerText;
            string CodigoProjeto = xmlInfoProjeto["CodigoProjetoEmpresa"].InnerText;
            CodigoProjeto = CodigoProjeto == "" || CodigoProjeto == "-1" ? "null" : CodigoProjeto;

            // se for novo cronograma, deverá inserir um registro na tabela CronogramaProjeto
            if (novoCronograma)
            {
                comandoSQL = string.Format(
                    @" DECLARE @CodigoCalendario int
                       DECLARE @MinutosDia int
                       DECLARE @MinutosSemana int
                       DECLARE @DiasMes int
                       DECLARE @CodigoEntidade int

                       SET @MinutosDia = 480
                       SET @MinutosSemana = 2400
                       SET @DiasMes = 20
                       SET @CodigoEntidade = {15}

                       SELECT @CodigoCalendario = c.[CodigoCalendario] 
                         FROM Calendario AS c	INNER JOIN 
                              AssociacaoCalendario	AS ac ON (ac.[CodigoCalendario]			= c.[CodigoCalendario]) INNER JOIN 
                              TipoAssociacao AS ta	ON (ta.[CodigoTipoAssociacao]	= ac.[CodigoTipoAssociacao])
                        WHERE c.[CodigoEntidade] = @CodigoEntidade
                          AND ta.[IniciaisTipoAssociacao]	= 'EN'
                          AND ac.[CodigoObjetoAssociado]	= @CodigoEntidade  

                       SET @CodigoCalendario = {19}

                       INSERT INTO {0}.{1}.CronogramaProjeto
                            ( CodigoCronogramaProjeto, NomeProjeto, DataInclusaoServidor, CodigoUsuarioInclusao, NomeAutor, Dominio, versaoDesktop, SistemaOperacional, IdentificadorMaquina, 
                              InicioProjeto, DataCheckoutCronograma, CodigoUsuarioCheckoutCronograma, 
                              ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoCodigoMoedaProjeto, CodigoEntidade, CodigoProjeto,
                              DataUltimaPublicacao, CodigoUsuarioUltimaPublicacao, DataUltimaGravacaoDesktop, CodigoCalendario, MinutosDia, MinutosSemana, DiasMes )
                       VALUES 
                            ('{2}', '{3}', GetDate(), {10}, '{4}', '{5}', '{6}', '{7}', '{8}',
                             convert(datetime, '{9}', 103), GetDate(), {10}, 
                             '{11}', '{12}', '{13}', {14}, {15}, {16}, 
                              {17}, {18}, GetDate(), @CodigoCalendario, @MinutosDia, @MinutosSemana, @DiasMes  )
                    ", cDados.getDbName(), cDados.getDbOwner(),
                       guidProjetoAberto, NomeProjeto, NomeAutor, Dominio, versaoDesktop, SistemaOperacional, IdentificadorMaquina,
                       InicioProjeto, codigoUsuario,
                       ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoCodigoMoedaProjeto, CodigoEntidade, CodigoProjeto,
                       publicarCronograma ? "GetDate()" : "null", publicarCronograma ? codigoUsuario.ToString() : "null",
                       CodigoCalendarioProjeto);
            }
            //caso contrário, atualiza as informações de identificação do cronograma
            else
            {
                string UltimaPublicacao;
                if (publicarCronograma)
                {
                    UltimaPublicacao = string.Format(
                        @", DataUltimaPublicacao = GetDate()
                          , CodigoUsuarioUltimaPublicacao = {0}
                          , IndicaPublicacaoPendente = 'N' ", codigoUsuario);
                }
                else
                    UltimaPublicacao = ", IndicaPublicacaoPendente = 'S' ";

                comandoSQL = string.Format(
                    @"UPDATE {0}.{1}.CronogramaProjeto
                         SET NomeProjeto = '{3}'
                           , InicioProjeto = convert(datetime, '{4}', 103)
                           , ConfiguracaoFormatoDuracao  = '{5}'
                           , ConfiguracaoFormatoTrabalho = '{6}'
                           , ConfiguracaoFormatoDataComHora = '{7}'
                           , ConfiguracaoCodigoMoedaProjeto = '{8}'
                           , DataUltimaAlteracao = GetDate()
                           , CodigoUsuarioUltimaAlteracao = {9}
                           , DataUltimaGravacaoDesktop = GetDate()
                           , CodigoCalendario = {11}
                           {10}
                       WHERE CodigoCronogramaProjeto = '{2}'
                     ", cDados.getDbName(), cDados.getDbOwner(),
                       guidProjetoAberto, NomeProjeto, InicioProjeto,
                       ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoCodigoMoedaProjeto,
                       codigoUsuario, UltimaPublicacao, CodigoCalendarioProjeto);
            }

            cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "\n" + comandoSQL);
        }
    }

    private bool persisteTarefasCronograma(XmlDocument XmlCronograma, bool cronogramaImportadoMSProject, out bool existeLinhaBase)
    {
        string codigoProjeto = XmlCronograma.SelectSingleNode("Projeto")["Codigo"].InnerText;
        existeLinhaBase = false;

        // recuperar as tarefas
        XmlNodeList xmlTarefas = XmlCronograma.SelectNodes("Projeto/Tarefas");

        if (xmlTarefas.Count > 0)
        {
            string comandoSQL = "";
            int afetatos = 0;
            int qtdTarefasInseridasComandoSQL = 0;
            string Tarefa = "";
            string Linha = "";
            // variavel para orientar, em caso de erro, se ocorreu durante a montagem do comando ou durante a execução
            bool FaseExecutarComando = false;
            try
            {
                foreach (XmlNode nodeTarefa in xmlTarefas[0].ChildNodes)
                {
                    FaseExecutarComando = false;
                    string CodigoTarefa = nodeTarefa["CodigoTarefa"].InnerText;
                    Linha = nodeTarefa["Linha"].InnerText;
                    Tarefa = nodeTarefa["Tarefa"].InnerText;
                    string Nivel = nodeTarefa["Nivel"].InnerText;
                    string TarefaResumo = nodeTarefa["TarefaResumo"].InnerText.Trim() == "" ? "N" : nodeTarefa["TarefaResumo"].InnerText.Trim();

                    string Duracao = nodeTarefa["Duracao"].InnerText;
                    string valorDuracao = Duracao.Substring(0, Duracao.IndexOf(' ')).Replace(',', '.');
                    string formatoDuracao = Duracao.Substring(Duracao.IndexOf(' ') + 1).ToUpper();
                    // duração Minuto - grava como "N"
                    if (formatoDuracao.Substring(0, 2) == "MI")
                        formatoDuracao = "N";
                    else
                        formatoDuracao = formatoDuracao[0] + "";

                    string InicioPrevisto = nodeTarefa["InicioPrevisto"].InnerText;
                    string TerminoPrevisto = nodeTarefa["TerminoPrevisto"].InnerText;
                    string Predecessoras = nodeTarefa["Predecessoras"].InnerText;
                    string CodigoTarefaSuperior = nodeTarefa["CodigoTarefaSuperior"].InnerText;
                    string MenorDataTarefa = nodeTarefa["MenorDataTarefa"].InnerText;
                    string DataRestricao = nodeTarefa["DataRestricao"].InnerText;
                    string TipoRestricao = nodeTarefa["TipoRestricao"].InnerText;
                    string Marco = nodeTarefa["Marco"].InnerText;
                    string EstruturaHierarquica = nodeTarefa["EstruturaHierarquica"].InnerText;
                    string StringAlocacaoRecursoTarefa = nodeTarefa["StringAlocacaoRecursoTarefa"].InnerText;
                    string indicesTarefasDependentes = nodeTarefa["indicesTarefasDependentes"].InnerText;
                    string anotacoes = nodeTarefa["anotacoes"].InnerText;

                    string inicioLB = nodeTarefa["inicioLB"].InnerText;
                    string terminoLB = nodeTarefa["terminoLB"].InnerText;
                    string trabalhoLB = nodeTarefa["trabalhoLB"].InnerText.Replace(',', '.');
                    string duracaoLB = nodeTarefa["duracaoLB"].InnerText;
                    string valorDuracaoLB = "";
                    string formatoDuracaoLB = "";
                    if (duracaoLB != "")
                    {
                        valorDuracaoLB = duracaoLB.Substring(0, duracaoLB.IndexOf(' ')).Replace(',', '.');
                        formatoDuracaoLB = duracaoLB.Substring(duracaoLB.IndexOf(' ') + 1).ToUpper();
                        // duração Minuto - grava como "N"
                        if (formatoDuracaoLB.Substring(0, 2) == "MI")
                            formatoDuracaoLB = "N";
                    }

                    string inicioReal = nodeTarefa["inicioReal"].InnerText;
                    string terminoReal = nodeTarefa["terminoReal"].InnerText;
                    string trabalhoReal = nodeTarefa["trabalhoReal"].InnerText.Replace(',', '.');
                    string custoReal = nodeTarefa["custoReal"].InnerText.Replace(',', '.');
                    string duracaoReal = nodeTarefa["duracaoReal"].InnerText;
                    string valorDuracaoReal = "";
                    string formatoDuracaoReal = "";

                    if (duracaoReal != "")
                    {
                        valorDuracaoReal = duracaoReal.Substring(0, duracaoReal.IndexOf(' ')).Replace(',', '.');
                        formatoDuracaoReal = duracaoReal.Substring(duracaoReal.IndexOf(' ') + 1).ToUpper();
                        // duração Minuto - grava como "N"
                        if (formatoDuracaoReal.Substring(0, 2) == "MI")
                            formatoDuracaoReal = "N";
                    }

                    string PercentualConcluido = nodeTarefa["PercentualConcluido"].InnerText.Replace(',', '.');
                    string IndicaTarefaCritica = nodeTarefa["IndicaTarefaCritica"].InnerText.Trim() == "" ? "N" : nodeTarefa["IndicaTarefaCritica"].InnerText;
                    string DuracaoAcumuladaEmMinutos = nodeTarefa["DuracaoAcumuladaEmMinutos"].InnerText.Trim() == "" ? "0" : nodeTarefa["DuracaoAcumuladaEmMinutos"].InnerText;

                    if (MenorDataTarefa == "")
                        MenorDataTarefa = "null";
                    else
                        MenorDataTarefa = string.Format("convert(datetime, '{0}', 103)", MenorDataTarefa);

                    if (DataRestricao == "")
                        DataRestricao = "null";
                    else
                        DataRestricao = string.Format("convert(datetime, '{0}', 103)", DataRestricao);

                    if (TipoRestricao == "")
                        TipoRestricao = "null";
                    else
                        TipoRestricao = string.Format("'{0}'", TipoRestricao);


                    string TrabalhoTarefa = nodeTarefa["TrabalhoTarefa"].InnerText.Replace(',', '.');
                    string CustoTarefa = nodeTarefa["CustoTarefa"].InnerText.Replace(',', '.');
                    TrabalhoTarefa = TrabalhoTarefa == "" ? "0" : TrabalhoTarefa;
                    CustoTarefa = CustoTarefa == "" ? "0" : CustoTarefa;

                    // se o cronograma foi importado do MSProject e possui linha base
                    string sqlLinhaBase = "";
                    if (cronogramaImportadoMSProject && inicioLB != "")
                    {
                        existeLinhaBase = true;
                        sqlLinhaBase = string.Format(
                            @" , InicioLB = {0}
                               , TerminoLB = {1}
                               , DuracaoLB = {2}", inicioLB == "" ? "null" : "convert(datetime, '" + inicioLB + "', 103)",
                                                   terminoLB == "" ? "null" : "convert(datetime, '" + terminoLB + "', 103)",
                                                   duracaoLB == "" ? "null" : valorDuracaoLB);
                    }


                    string temp = string.Format(
                        @"
                        -----------------------------------------------------------------------------------------------------------------------------
                        if ( not exists (Select 1 from {0}.{1}.TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}' and CodigoTarefa = {3} and dataExclusao is null))

                            INSERT INTO {0}.{1}.TarefaCronogramaProjeto 
                                (CodigoCronogramaProjeto, CodigoTarefa, NomeTarefa, SequenciaTarefaCronograma, CodigoTarefaSuperior, Inicio, Termino, 
                                 Duracao, Trabalho, DataInclusao, nivel )
                            VALUES ( '{2}', {3}, '{4}', {5}, {6}, convert(datetime, '{7}', 103), convert(datetime, '{8}', 103), {9}, {10}, GetDate(), {15} ) 

                        UPDATE {0}.{1}.TarefaCronogramaProjeto
                           SET NomeTarefa = '{4}'
                             , SequenciaTarefaCronograma = {5}
                             , CodigoTarefaSuperior = {6}
                             , Inicio = convert(datetime, '{7}', 103)
                             , Termino = convert(datetime, '{8}', 103)
                             , Duracao = {9}
                             , Trabalho = {10}
                             , Custo = {11}
                             , Predecessoras = '{12}'
                             , IndicaTarefaResumo = '{13}'
                             , IndicaMarco = '{14}'
                             , DataUltimaAlteracao = GetDate()
                             , nivel = {15}
                             , MenorDataTarefa = {16}
                             , DataRestricao = {17}
                             , tipoRestricao = {18}
                             , EstruturaHierarquica = '{19}'
                             , StringAlocacaoRecursoTarefa = {20}
                             , indicesTarefasDependentes = {21}
                             , anotacoes = '{22}'

                             , InicioReal = {23}
                             , TerminoReal = {24}
                             , DuracaoReal = {25}
                             , TrabalhoReal = {26}
                             , CustoReal = {27}

                             , PercentualFisicoConcluido = {28}
                             , FormatoDuracao = '{29}'
                             , IndicaTarefaCritica = '{30}'
                             , DuracaoAcumuladaEmMinutos = {31}

                             {32} -- linha de base

                         WHERE CodigoCronogramaProjeto = '{2}'
                           AND CodigoTarefa = {3}
                           AND DataExclusao is null
                    ", cDados.getDbName(), cDados.getDbOwner(),
                     codigoProjeto, CodigoTarefa, Tarefa, Linha,
                     CodigoTarefaSuperior == "" ? "null" : CodigoTarefaSuperior,
                     InicioPrevisto, TerminoPrevisto, valorDuracao, TrabalhoTarefa, CustoTarefa,
                     Predecessoras, TarefaResumo, Marco, Nivel,
                     MenorDataTarefa, DataRestricao, TipoRestricao, EstruturaHierarquica,
                     StringAlocacaoRecursoTarefa == "" ? "null" : "'" + StringAlocacaoRecursoTarefa + "'",
                     indicesTarefasDependentes == "" ? "null" : "'" + indicesTarefasDependentes + "'",
                     anotacoes.Replace("'", "''"),                                                                 // 22

                     inicioReal == "" ? "null" : "convert(datetime, '" + inicioReal + "', 103)",                   // 23
                     terminoReal == "" ? "null" : "convert(datetime, '" + terminoReal + "', 103)",                 // 24
                     duracaoReal == "" ? "null" : valorDuracaoReal,                 // 25
                     trabalhoReal == "" ? "null" : trabalhoReal,                 // 26
                     custoReal == "" ? "null" : custoReal,                 // 27

                     PercentualConcluido == "" ? "null" : PercentualConcluido,                  // 28
                     formatoDuracao,                  // 29
                     IndicaTarefaCritica,                  // 30
                     DuracaoAcumuladaEmMinutos,                  // 31
                     sqlLinhaBase);                  // 32

                    comandoSQL += temp;
                    qtdTarefasInseridasComandoSQL++;
                    if (qtdTarefasInseridasComandoSQL == 100)
                    {
                        FaseExecutarComando = true;
                        cDados.execSQL(comandoSQL, ref afetatos);
                        comandoSQL = "";
                        qtdTarefasInseridasComandoSQL = 0;
                    }
                }

                if (qtdTarefasInseridasComandoSQL > 0)
                    cDados.execSQL(comandoSQL, ref afetatos);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao {0} ({1}): {2} ", (FaseExecutarComando ? "Executar o comando para inserir o lote de tarefas finalizado por " : "Construir o comando para salvar a tarefa "), Linha, Tarefa) + "\n" + ex.Message);
            }

        }
        return true;
    }

    private bool persisteTarefasExcluidasCronograma(XmlDocument XmlCronograma)
    {
        string codigoProjeto = XmlCronograma.SelectSingleNode("Projeto")["Codigo"].InnerText;

        // recuperar as tarefas
        XmlNodeList xmlTarefasExcluidas = XmlCronograma.SelectNodes("Projeto/TarefasExcluidas");

        if (xmlTarefasExcluidas.Count > 0)
        {
            string comandoSQL = "";
            int afetatos = 0;
            int qtdTarefasExcluidasComandoSQL = 0;
            try
            {
                foreach (XmlNode nodeTarefa in xmlTarefasExcluidas[0].ChildNodes)
                {
                    string CodigoTarefa = nodeTarefa.Attributes["CodigoTarefa"].Value;
                    string temp = string.Format(
                        @"
                        -----------------------------------------------------------------------------------------------------------------------------
                        UPDATE {0}.{1}.TarefaCronogramaProjeto
                           SET DataExclusao = GetDate()
                         WHERE CodigoCronogramaProjeto = '{2}'
                           AND CodigoTarefa = {3}

                    ", cDados.getDbName(), cDados.getDbOwner(),
                     codigoProjeto, CodigoTarefa);

                    comandoSQL += temp;
                    qtdTarefasExcluidasComandoSQL++;
                    if (qtdTarefasExcluidasComandoSQL == 100)
                    {
                        cDados.execSQL(comandoSQL, ref afetatos);
                        comandoSQL = "";
                        qtdTarefasExcluidasComandoSQL = 0;
                    }
                }

                if (qtdTarefasExcluidasComandoSQL > 0)
                    cDados.execSQL(comandoSQL, ref afetatos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + comandoSQL);
            }

        }
        return true;
    }

    /// <summary>
    /// Verifica se já existe no banco de dados um cronograma com o código informado
    /// </summary>
    /// <param name="guidProjetoAberto">Código do cronograma que deve ser verificado</param>
    /// <returns>true se o cronograma já existe e false se o cronograma não existe</returns>
    private bool getCronogramaJaExiste(string guidProjetoAberto)
    {
        DataSet ds = cDados.getDataSet(string.Format(
            @"Select 1 
                    from {0}.{1}.CronogramaProjeto
                   where CodigoCronogramaProjeto = '{2}' ", cDados.getDbName(), cDados.getDbOwner(), guidProjetoAberto));
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count == 1)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Grava o cronograma que já está na pasta padrão no banco de dados
    /// </summary>
    /// <param name="key"></param>
    /// <param name="guidProjeto"></param>
    /// <param name="codigoUsuario"></param>
    /// <returns></returns>
    [WebMethod]
    public bool salvarCronograma(string key, string guidProjeto, int codigoUsuario)
    {
        if (key == _key)
        {
            // se existir arquivo, vamos inserir a imagem
            if (guidProjeto != "" && guidProjeto.Length == 36)
            {
                string arquivo = diretorioCronogramas + "\\" + guidProjeto + ".xml";

                string arquivoExecutavelDOS = diretorioCronogramas + "\\agPortalEstrategiaDesktop.exe";
                // Parametros:
                // 1º String de Conexao Criptografada - PathDB
                // 2º Chave do Produto - IDProduto
                // 3º Tipo do banco de dados
                // 4º Owner do banco de dados
                // 5º Nome do arquivo a ser inserido no banco de dados
                // 6º codigo do usuario que iniciou a acao
                string parametrosAgente =
                    PathDB + " " +
                    IDProduto + " " +
                    tipoBancoDados + " " +
                    ownerdb + " " +
                    arquivo + " " +
                    codigoUsuario;

                // se não precisar esperar o término do processo use apenas a linha abaixo
                //Process.Start(arquivoExecutavelDOS, parametrosAgente);

                // inicia um processo e espera o seu término.
                Process proc = new Process();
                proc.StartInfo.FileName = arquivoExecutavelDOS;
                proc.StartInfo.Arguments = parametrosAgente;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();


                // referencia para informações sobre a classe process
                // http://www.linhadecodigo.com.br/artigo/271/manipulando-processos-atraves-da-classe-systemdiagnosticsprocess.aspx

                return true;
            }
        }
        return false;
    }

    [WebMethod]
    public string salvarXMLGantt(string key, XmlDocument XmlCronograma)
    {
        if (key == _key)
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");

            string nomeGrafico = @"ArquivosTemporarios\gantt_" + dataHora + ".xml";

            //Cria o arquivo XML
            string nome = cDados.escreveXML(XmlCronograma.InnerXml, nomeGrafico);
            nome = nome.Substring(nome.IndexOf("gantt"));
            return nome;
        }
        return "";
    }

}

