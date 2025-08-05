using System;
using System.Data;
using System.IO;
using System.Web.Services;

/// <summary>
/// WebService dos itêns necessários para o sistema fluxo em flash
/// </summary>
[WebService(Namespace = "http://www.cdis.com.br/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class wsFluxo : System.Web.Services.WebService
{
    string _key = "#COMANDO#CDIS!";
    private string PathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
    private string IDProduto = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();
    private string tipoBancoDados = System.Configuration.ConfigurationManager.AppSettings["tipoBancoDados"].ToString();
    dados cDados;
    private string bancodb = string.Empty;
    private string ownerdb = string.Empty;
    public wsFluxo()
    {

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["WSPortal"] = "WSPortal";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        bancodb = cDados.getDbName();
        ownerdb = cDados.getDbOwner();
    }

    [WebMethod]
    public string carregarCombos(String codigoWorkflow)
    {
        string retorno = "";
        DataSet ds = new DataSet();
        string complemento = "";
        string[][] tabelasBanco = new string[][]
                            {
                                    new string[] {"AcoesAutomaticasWf", "CodigoAcaoAutomaticaWf", "Nome"}
                                    , new string[] {"PerfisWf", "CodigoPerfilWf", "NomePerfilWf"}
                                    , new string[] {"ModeloFormulario", "CodigoModeloFormulario", "NomeFormulario"}
                           };

        foreach (string[] nomes in tabelasBanco)
        {
            string codigo = "";
            string nome = "";
            if (nomes.Length > 1)
            {
                codigo = nomes[1];
                nome = nomes[2];
            }
            if (nomes[0] == "EtapasWf")
                complemento = "WHERE CodigoWorkflow = " + int.Parse(codigoWorkflow);
            string sqlAcoes = string.Format(@"
                    SELECT {3} as codigo, {4} as nome
                    FROM {0}.{1}.{2}
                    {5}
                    "
                , bancodb
                , ownerdb
                , nomes[0]
                , nomes[1]
                , nomes[2]
                , complemento
                );


            ds.Tables.Add(cDados.getDataSet(sqlAcoes).Tables[0].Copy());
            int numero = ds.Tables.Count - 1;
            ds.Tables[numero].TableName = nomes[0];


        }
        retorno += "<dados><combos>";
        foreach (DataTable tabela in ds.Tables)
        {
            retorno += "<" + tabela.TableName + ">";
            foreach (DataRow linha in tabela.Rows)
            {
                retorno += String.Format(@"<{0} {1}=""{2}"" {3}=""{4}"" ></{0}>", tabela.TableName, tabela.Columns[0].ColumnName, linha["codigo"], tabela.Columns[1].ColumnName, linha["nome"]);
            }

            retorno += "</" + tabela.TableName + ">";
        }
        retorno += "</combos></dados>";
        return retorno.Replace("&amp;", "&");
    }

    [WebMethod]
    public string cerregarWorkflow(String codigoWorkflow)
    {
        string retorno = "";
        DataTable tabela = new DataTable();
        string sqlAcoes = string.Format(@"
                    SELECT TextoXML, DescricaoVersao as descricaoAbreviada, Observacao as descricao, VersaoFormatoXML as versaoXml
                    FROM {0}.{1}.[Workflows]
                    WHERE CodigoWorkflow = {2}
                    "
                    , bancodb
                    , ownerdb
                    , codigoWorkflow
                    );

        tabela = cDados.getDataSet(sqlAcoes).Tables[0].Copy();
        DataRow row = tabela.Rows[0];
        retorno = row["TextoXML"].ToString();
        string nomeRef = "</workflows>";
        int numeroNovo = retorno.IndexOf(nomeRef) + nomeRef.Length;
        string novo = string.Format("<dadosVersao descricaoAbreviada=\"{0}\" descricao=\"{1}\" versaoXml=\"{2}\" />", row["descricaoAbreviada"], row["descricao"], row["versaoXml"]);
        retorno = retorno.Insert(numeroNovo, novo);
        return retorno;


    }

    [WebMethod]
    public string atualizarWorkflow(String workflowCodigo, String workflowXml, String workflowDescricao, String workflowObservacao)
    {
        return cDados.atualizaWorkflow(_key, int.Parse(workflowCodigo), workflowXml, workflowDescricao, workflowObservacao).ToString();
    }

    [WebMethod]
    public string salvarWorkflow(String fluxoCodigo, String workflowVersao, String idUsuario, String workflowXml, String workflowObservacao)
    {
        return cDados.gravaWorkflow(_key, int.Parse(fluxoCodigo), workflowVersao, idUsuario, workflowVersao, workflowObservacao, workflowXml).ToString();
    }

    [WebMethod]
    public string carregarFluxos()
    {
        string retorno = "";
        DataSet ds = new DataSet();
        string comando1 = string.Format(@"
            SELECT [CodigoFluxo]
            ,[NomeFluxo]
            ,[DataDesativacao]
            FROM {0}.{1}.[Fluxos]
            WHERE [DataDesativacao] IS NULL
            ORDER BY [NomeFluxo] ASC        
            "
            , bancodb
            , ownerdb
            );
        ds.Tables.Add(cDados.getDataSet(comando1).Tables[0].Copy());
        ds.Tables[0].TableName = "fluxos";
        string comando2 = string.Format(@"
            SELECT [CodigoWorkflow]
            ,[CodigoFluxo]
            ,[DescricaoVersao]
            FROM {0}.{1}.[Workflows]
            ORDER BY [DescricaoVersao] ASC        
            "
            , bancodb
            , ownerdb
            );
        ds.Tables.Add(cDados.getDataSet(comando2).Tables[0].Copy());
        ds.Tables[1].TableName = "workflows";
        StringWriter textoXml = new StringWriter();
        ds.WriteXml(textoXml);
        string novoTextoXml = textoXml.ToString();
        retorno = novoTextoXml;
        return retorno;

    }

    [WebMethod]
    public string carregarDados(String codigoWorkflow, string tabelasRetorno)
    {
        //string retorno = @"<?xml version=""1.0"" encoding=""utf-8"" ?>" + Environment.NewLine;
        string retorno = "";
        DataTable ds = new DataTable();

        int tipo = 0;
        if (tabelasRetorno != "combos")
            tipo = 1;
        string[][][] tabelasBanco = new string[][][] {
            new string[][] {
                                    new string[] {"AcoesAutomaticasWf", "CodigoAcaoAutomaticaWf", "Nome"}
                                    , new string[] {"PerfisWf", "CodigoPerfilWf", "NomePerfilWf"}
                                    , new string[] {"ModeloFormulario", "CodigoModeloFormulario", "NomeFormulario"}
                           }
            , new string[][] {
                                    new string[] {"AcoesEtapasWf"}
                                    , new string[] {"AcoesAutomaticasEtapasWf"}
                                    , new string[] {"EtapasWf"}
                                    , new string[] {"AcessosEtapasWf"}
                                    , new string[] {"FormulariosEtapasWf"}
                             }
        };

        if (codigoWorkflow != "")
        {
            foreach (string[] nomes in tabelasBanco[tipo])
            {
                string complemento = "";
                string complemento2 = "";
                string codigo = "";
                string nome = "";
                if (nomes.Length > 1)
                {
                    codigo = nomes[1];
                    nome = nomes[2];
                }
                if (nomes[0] == "EtapasWf" || tipo == 1)
                    complemento2 = "WHERE CodigoWorkflow = " + int.Parse(codigoWorkflow);
                if (tipo == 0)
                    complemento = "SELECT " + codigo + " as codigo, " + nome + " as nome";
                else
                    complemento = "SELECT *";
                string sqlAcoes = string.Format(@"
                    {3}
                    FROM {0}.{1}.{2}
                    {4}
                    "
                    , bancodb
                    , ownerdb
                    , nomes[0]
                    , complemento
                    , complemento2
                    );


                ds = cDados.getDataSet(sqlAcoes).Tables[0];
                ds.TableName = nomes[0];
                StringWriter textoXml = new StringWriter();
                ds.WriteXml(textoXml);
                string novoTextoXml = textoXml.ToString();
                retorno += novoTextoXml.Replace("NewDataSet", nomes[0].ToString());
            }

        }
        return retorno.Replace("&amp;", "&");
    }


}