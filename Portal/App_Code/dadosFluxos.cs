using System.Data;
using System.Web;

/// <summary>
/// Metodos exclusivos para o sistema de fluxo
/// </summary>
public class dadosFluxos
{
    private dados cDados;
    public dadosFluxos()
    {
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = HttpContext.Current.Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = HttpContext.Current.Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
    }
    /// <summary>
    /// Carrega todos os valores do combos usados no sistema fluxo
    /// </summary>
    /// <param name="codigoWorkflow">Código do workflow a ser usado como referência</param>
    /// <returns></returns>
    public DataSet carregarCombosFluxo(string codigoWorkflow)
    {
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
                    ORDER BY nome
                    "
                , cDados.getDbName()
                , cDados.getDbOwner()
                , nomes[0]
                , nomes[1]
                , nomes[2]
                , complemento
                );


            ds.Tables.Add(cDados.getDataSet(sqlAcoes).Tables[0].Copy());
            int numero = ds.Tables.Count - 1;
            ds.Tables[numero].TableName = nomes[0];
        }
        return ds;

    }
    /// <summary>
    /// Carregar todos os dados do fluxo solicitado
    /// </summary>
    /// <param name="codigoWorkflow"></param>
    /// <returns></returns>
    public string carregarFluxo(string codigoWorkflow)
    {
        string retorno = "";
        DataTable tabela = new DataTable();
        string sqlAcoes = string.Format(@"
                    SELECT TextoXML, DescricaoVersao as descricaoAbreviada, Observacao as descricao, VersaoFormatoXML as versaoXml
                    FROM {0}.{1}.[Workflows]
                    WHERE CodigoWorkflow = {2}
                    "
                    , cDados.getDbName()
                    , cDados.getDbOwner()
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

}