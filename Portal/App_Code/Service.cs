using System;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class Service
{
    private dadosReunioes cReunioes = new dadosReunioes();
    private dadosFluxos cFluxos = new dadosFluxos();

    #region metodos serviçoes web
    #region Sistema Reunião
    /// <summary>
    /// Verifica se a sessão atual necessita ser atualizada ou não
    /// </summary>
    /// <param name="valor">valor informado o tipo de atualização a ser realizada</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public string verificarAtualizacoes(string valor)
    {
        string retorno = "Retorno";
        DataTable dtDados = new DataTable();
        JavaScriptSerializer js = new JavaScriptSerializer();
        switch (valor)
        {
            case "atualizarSistema":
                retorno += cReunioes.atualizarAtualizacaoStatus();
                break;
        }
        return retorno;
    }
    /// <summary>
    /// Busca a tabela usada no sistema guardada em session e retona para tratamento dentro do javascript
    /// </summary>
    /// <param name="tabela">nome da tabela a ser retornada</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public string buscarTabela(string tabela)
    {
        string retorno = "";
        DataTable dtDados = cReunioes.valoresReuniao<DataTable>("valor");
        JavaScriptSerializer js = new JavaScriptSerializer();
        dtDados = cReunioes.valoresReuniao<DataTable>("dtItens");
        retorno = js.Serialize(dadosConversores.tabelaParaDictionary(dtDados));
        return retorno;
    }
    /// <summary>
    /// Carrega todos os dados do banco de dados criando as tabela em session para utilização do sistema
    /// </summary>
    /// <returns>Retorna se aoperação foi sucesso ou não</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public string carregarDadosBanco()
    {
        string retorno = "";
        string mensagem = "";
        Boolean carregado = cReunioes.carregarTudo(ref mensagem);
        if (carregado)
            retorno = carregado.ToString();
        else
            retorno = mensagem;
        return retorno;
    }
    /// <summary>
    /// Verifica o estado da reunião atual e a permissão do usuário atual para a reunião
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public string permissoesUsuario()
    {
        string retorno = "";
        string mensagem;
        cReunioes.carregarStatusReuniao();
        if (cReunioes.valoresReuniao<string>("tipoUsuario") == "")
            cReunioes.carregarParticipantes();
        if (cReunioes.valoresReuniao<string>("tipoUsuario") == "U")
            retorno = "participante";
        if (cReunioes.valoresReuniao<String>("tipoUsuario") == "C")
            retorno = "convidado";
        if (cReunioes.valoresReuniao<String>("tipoUsuario") == "A")
            retorno = "assistente";
        if (cReunioes.valoresReuniao<String>("codigoModerador") == cReunioes.valoresReuniao<String>("codigoUsuario"))
            retorno = "moderador";
        if (cReunioes.valoresReuniao<String>("reuniaoIniciada") == "")
            retorno += "§naoIniciada";
        if (cReunioes.valoresReuniao<String>("reuniaoIniciada") == "true")
            retorno += "§iniciada";
        if (cReunioes.valoresReuniao<String>("reuniaoIniciada") == "false")
            retorno += "§finalizada";
        mensagem = cReunioes.participanteReuniaoAtual();
        retorno += mensagem != "" ? "§" + mensagem : "";
        return retorno;
    }
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public string itemEventoAtual()
    {
        string retorno = cReunioes.valoresReuniao<String>("codigoItemAtual") + "§" + cReunioes.valoresReuniao<String>("tipoItemAtual");
        return retorno;
    }
    #endregion

    #region Sistema Fluxo
    /// <summary>
    /// Chama e retorna em formato json os combos usados pelo o sisstema de fluxo
    /// </summary>
    /// <param name="codigoWorkflow"></param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public string carregarCombosFluxo(string codigoWorkflow)
    {
        string retorno = "";
        DataSet dtDados = cFluxos.carregarCombosFluxo(codigoWorkflow);
        JavaScriptSerializer js = new JavaScriptSerializer();
        retorno = js.Serialize(dadosConversores.datasetParaDectionary(dtDados));
        return retorno;
    }

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    public string carregarFluxo(string codigoWorkflow)
    {
        return cFluxos.carregarFluxo(codigoWorkflow);
    }
    #endregion
    #endregion
}
