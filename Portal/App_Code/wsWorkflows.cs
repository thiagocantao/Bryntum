using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Services;

/// <summary>
/// Descrição resumida de wsWorkflows
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
//[System.Web.Script.Services.ScriptService]
public class wsWorkflows : System.Web.Services.WebService
{
    dados cDados;
    string codigoEntidade;
    string idUsuarioLogado;
    JsonSerializerSettings settings;

    public wsWorkflows()
    {
        cDados = CdadosUtil.GetCdados(null);
        codigoEntidade = cDados.getInfoSistema("CodigoEntidade").ToString();
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        settings = new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" };
    }

    [WebMethod(EnableSession = true, MessageName = "valida-expressao-caminho-condicional")]
    public bool ValidaExpressaoCaminhoCondicional(string expressao, int codigoFluxo, int codigoWorkflow )
    {
        try
        {
            expressao = System.Web.HttpUtility.UrlDecode(expressao);

            string comandoSQL = string.Format(
                @"DECLARE @in_CodigoEntidadeContexto int,
                   @in_CodigoUsuarioSistema int,
                   @in_CodigoFluxo int,
                   @in_CodigoWorkflow int,
                   @in_ExpressaoCondicional varchar(2048),
                   @ou_indicaExpressaoValida int;

        SET @in_CodigoEntidadeContexto = {0};
        SET @in_CodigoUsuarioSistema = {1};
        SET @in_CodigoFluxo = {2};
        SET @in_CodigoWorkflow = {3};
        SET @in_ExpressaoCondicional = '{4}';

        EXEC [dbo].[p_wf_validaExpressaoCaminhoCondicional]
            @in_CodigoEntidadeContexto,
            @in_CodigoUsuarioSistema,
            @in_CodigoFluxo,
            @in_CodigoWorkflow,
            @in_ExpressaoCondicional,
            @ou_indicaExpressaoValida OUTPUT;

        SELECT @ou_indicaExpressaoValida;",
                codigoEntidade, idUsuarioLogado, codigoFluxo, codigoWorkflow, expressao.Replace("'","''"));

            var ds = cDados.getDataSet(comandoSQL);
            bool retorno = ds.Tables[0].Rows[0][0].ToString() == "0" ? false : true;
            return retorno;

        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

}
