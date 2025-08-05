using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_AssociacaoContas : System.Web.UI.Page
{
    //variaveis de acceso ao banco de dados.
    dados cDados;

    //variaveis de logeo.
    private int codigoEntidade = 0, codigoProjeto = 0;
    private int codigoUsuario = 0;

    //variaveis de configuração
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

    //variaveis de permissão.
    public bool podeEditar = false;
    public bool podeConsultar = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(Page);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }



        setPermissoesTela();

        carregaTreeList();

        //ISPOPUP
        if (Request.QueryString["ISPOPUP"] != null && Request.QueryString["ISPOPUP"].ToString() != "" && Request.QueryString["ISPOPUP"].ToString() == "S")
        {
            btnFechar.ClientVisible = true;
            ((TreeListTextColumn)tlDados.Columns["TipoConta"]).Visible = false;
        }

        tlDados.JSProperties["cp_MSG"] = "";

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
    }

    private void setPermissoesTela()
    {
        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CnsCfgPlnCta");
        }

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade,
            codigoProjeto, "null", "PR", 0, "null", "PR_AltCfgPlnCta");

        cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeEditar, ref podeEditar, ref podeEditar);
    }

    private void carregaTreeList(string filtroDigitado = "")
    {
        string comandoSQL = string.Format(@"
            SELECT * FROM dbo.f_gestconv_getPlanoContas({0}, {1}) where DescricaoConta like '%{2}%'", codigoProjeto, codigoEntidade, filtroDigitado);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            tlDados.DataSource = ds;
            tlDados.DataBind();
        }
    }

    public string getBotoes()
    {
        string table = "";
        string indicaContaAnalitica = Eval("IndicaContaAnalitica").ToString();
        string indicaContaAtiva = Eval("IndicaContaAtivaProjeto").ToString();
        string codigoConta = Eval("CodigoConta").ToString();
        string descricao = Eval("DescricaoConta").ToString();

        if (indicaContaAnalitica == "S" && podeEditar)
        {
            if (indicaContaAtiva == "S")
            {
                table = string.Format(@"
                    <table cellpadding=""0"" cellspacing=""0"" style=""width: 100% "">
                      <tr>
                        <td title='Excluir' style='width:20px; cursor:pointer'><img onclick='excluiConta({1});' src='../../imagens/botoes/excluirReg02.png' alt='Excluir'/></td>
                        <td title='Desativar' style='width:20px; cursor:pointer'><img onclick='desativaConta({1});' src='../../imagens/botoes/btnOff.png' alt='Desativar'/></td>
                        <td title='Conta ATIVA vinculada ao projeto' align='right' style='width:20px; padding-right:1px'><img src='../../imagens/botoes/tarefasAP_pequeno.PNG' alt='Conta vinculada ao projeto'/></td>
                        <td>{0}</td>
                      </tr>
                    </table>", descricao, codigoConta);
            }
            else if (indicaContaAtiva == "N")
            {
                table = string.Format(@"
                    <table cellpadding=""0"" cellspacing=""0"" style=""width: 100% "">
                      <tr>
                        <td title='Excluir' style='width:20px; cursor:pointer'><img onclick='excluiConta({1});' src='../../imagens/botoes/excluirReg02.png' alt='Excluir'/></td>
                        <td title='Ativar' style='width:20px; cursor:pointer'><img onclick='ativaConta({1});' src='../../imagens/botoes/btnOn.png' alt='Ativar'/></td>
                        <td title='Conta INATIVA vinculada ao projeto' align='right' style='width:20px; padding-right:1px'><img src='../../imagens/botoes/tarefasAP_pequeno.PNG' alt='Conta vinculada ao projeto'/></td>
                        <td>{0}</td>
                      </tr>
                    </table>", descricao, codigoConta);
            }
            else
            {
                table = string.Format(@"
                    <table cellpadding=""0"" cellspacing=""0"" style=""width: 100% "">
                      <tr>
                        <td title='Incluir' style='width:20px; cursor:pointer'><img onclick='incluiConta({1});' src='../../imagens/botoes/incluirReg02.png' alt='Incluir'/></td>
                        <td title='' style='width:20px; padding-right:5px'><img src='../../imagens/botoes/btnOnDes.png' alt=''/></td>
                        <td>{0}</td>
                      </tr>
                    </table>", descricao, codigoConta);
            }
        }
        else
        {
            table = descricao;
        }

        return table;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);

        //tlDados.Settings.ScrollableHeight = altura;
    }

    protected void tlDados_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        string parametro = e.Argument;

        if (parametro != "")
        {
            string tipoOperacao = parametro.Split(';')[0];
            string codigoConta = parametro.Split(';')[1];

            if (tipoOperacao == "INC")
            {
                tlDados.JSProperties["cp_MSG"] = incluiConta(codigoConta);
            }
            else if (tipoOperacao == "DST")
            {
                tlDados.JSProperties["cp_MSG"] = desativaConta(codigoConta);
            }
            else if (tipoOperacao == "ATV")
            {
                tlDados.JSProperties["cp_MSG"] = ativaConta(codigoConta);
            }
            else if (tipoOperacao == "EXC")
            {
                tlDados.JSProperties["cp_MSG"] = excluiConta(codigoConta);
            }
            else if(tipoOperacao == "SCH")
            {
                carregaTreeList(txtBusca.Text);
            }
        }
    }

    private string incluiConta(string codigoConta)
    {
        string msgRetorno = "";
        string comandoSQL = "";

        if (codigoConta != "")
        {
            comandoSQL = string.Format(@"
            IF NOT EXISTS (SELECT 1 
                             FROM ProjetoPlanoContasFluxoCaixa 
                            WHERE CodigoProjeto = {0} 
                              AND CodigoConta = {1}) 
            BEGIN
                INSERT INTO ProjetoPlanoContasFluxoCaixa VALUES({0}, {1}, 'S', GetDate(), {2}, NULL, NULL)
            END
            ELSE
            BEGIN
                UPDATE [dbo].[ProjetoPlanoContasFluxoCaixa]
                   SET [IndicaContaAtiva] = 'S'    
                      ,DataUltimaAlteracao = GetDate()
                      ,CodigoUsuarioUltimaAlteracao = {2}
                 WHERE CodigoProjeto = {0} AND CodigoConta = {1}
            END", codigoProjeto, codigoConta, codigoUsuario);

            int regAf = 0;

            bool retorno = cDados.execSQL(comandoSQL, ref regAf);

            if (retorno)
            {
                msgRetorno = "Conta incluída com sucesso.";
                carregaTreeList();
            }
            else
                msgRetorno = "Erro ao incluir conta.";
        }

        return msgRetorno;
    }

    private string ativaConta(string codigoConta)
    {
        string msgRetorno = "";
        string comandoSQL = "";

        if (codigoConta != "")
        {
            comandoSQL = string.Format(@"
                UPDATE ProjetoPlanoContasFluxoCaixa 
                   SET IndicaContaAtiva = 'S'
                      ,DataUltimaAlteracao = GetDate()
                      ,CodigoUsuarioUltimaAlteracao = {2}
                WHERE CodigoProjeto = {0}
                  AND CodigoConta = {1}", codigoProjeto, codigoConta, codigoUsuario);

            int regAf = 0;

            bool retorno = cDados.execSQL(comandoSQL, ref regAf);

            if (retorno)
            {
                msgRetorno = "Conta ativada com sucesso.";
                carregaTreeList();
            }
            else
                msgRetorno = "Erro ao ativar conta.";
        }

        return msgRetorno;
    }

    private string desativaConta(string codigoConta)
    {
        string msgRetorno = "";
        string comandoSQL = "";

        if (codigoConta != "")
        {
            comandoSQL = string.Format(@"
                UPDATE ProjetoPlanoContasFluxoCaixa 
                   SET IndicaContaAtiva = 'N'
                      ,DataUltimaAlteracao = GetDate()
                      ,CodigoUsuarioUltimaAlteracao = {2}
                WHERE CodigoProjeto = {0}
                  AND CodigoConta = {1}", codigoProjeto, codigoConta, codigoUsuario);

            int regAf = 0;

            bool retorno = cDados.execSQL(comandoSQL, ref regAf);

            if (retorno)
            {
                msgRetorno = "Conta desativada com sucesso.";
                carregaTreeList();
            }
            else
                msgRetorno = "Erro ao desativar conta.";
        }

        return msgRetorno;
    }

    private string excluiConta(string codigoConta)
    {
        string msgRetorno = "";
        string comandoSQL = "";

        if (codigoConta != "")
        {
            comandoSQL = string.Format(@"
                SELECT dbo.f_gestconv_podeExcluirContaProjeto({0},{1}) AS PodeExcluirConta", codigoProjeto, codigoConta);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                if ((bool)ds.Tables[0].Rows[0]["PodeExcluirConta"] == false)
                    return "Essa conta não pode ser excluída, apenas desativada, pois existem lançamentos para ela.";
            }

            comandoSQL = string.Format(@"
                DELETE ProjetoPlanoContasFluxoCaixa 
                 WHERE CodigoProjeto = {0}
                   AND CodigoConta = {1}", codigoProjeto, codigoConta);

            int regAf = 0;

            bool retorno = cDados.execSQL(comandoSQL, ref regAf);

            if (retorno)
            {
                msgRetorno = "Conta excluída do projeto com sucesso.";
                carregaTreeList();
            }
            else
                msgRetorno = "Erro ao excluir a conta do projeto.";
        }

        return msgRetorno;
    }
}