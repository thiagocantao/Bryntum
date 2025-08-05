using CDIS;
using System;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for wsWfMobile
/// </summary>
[WebService(Namespace = "http://www.cdis.inf.br/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class wsWfMobile : System.Web.Services.WebService
{

    string _key = "#COMANDO#wfMobile!";
    static string PathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
    static string IDProduto = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();
    static string tipoBancoDados = System.Configuration.ConfigurationManager.AppSettings["tipoBancoDados"].ToString();
    static string Ownerdb = System.Configuration.ConfigurationManager.AppSettings["dbOwner"].ToString();
    static string bancodb = string.Empty;
    static string utilizaAutenticacaoExterna_TIPO;
    static string utilizaAutenticacaoExterna_DOMINIO;
    static string utilizaAutenticacaoExterna_ServidorLDAP_AD;


    dados cDados;
    ClasseDados classeDados;

    public class dadosConexao
    {
        public string urlAcessoMobAppWf;
        public int codigoUsuario;
        public string nomeUsuario;
        public string emailUsuario;
        public int codigoEntidadeAcesso;
        public string siglaEntidadeAcesso;
        public string stringConexao;
        public int codigoResultadoProcessamento;
        public string mensagemResultadoProcessamento;
    }

    public wsWfMobile()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["WSPortal"] = "WSPortal";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        classeDados = new ClasseDados(tipoBancoDados, PathDB, IDProduto, Ownerdb, "", 2);

        bancodb = cDados.getDbName();
    }

    // Codifica para base 64
    static public string EncodeTo64(string toEncode)
    {
        byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
        string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void obtemDadosConexaoUsuario(string key, string emailUsuario, string senhaAcessoPortal)
    {

        if (key == _key)
        {
            dadosConexao dConnection = new dadosConexao();
            autenticarUsuario(emailUsuario, senhaAcessoPortal, ref dConnection);

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(dConnection));
        }
    }

    /// <summary>
    ///  Função para autenticar o usuário e devolver suas informações de conexões. As situações de exceções são devolvidas no parâmetro de saida <paramref name="codigoResultadoProcessamento"/>
    /// Como "autenticação" deve-se entender validar as credenciais do usuário e determinar suas informações de conexão (entidade de acesso, etc)
    /// </summary>
    /// <param name="usuario">identificação do usuário a ser autenticada no sistema</param>
    /// <param name="senha">Senha do usuário a ser usada na autenticação</param>
    /// <param name="conexao">Objeto da classe dadosConexao que conterá as informações da conexão do usuário. Os valores contidos nos atributos desse objeto são os seguintes:  
    ///   Atributo                          Descrição
    ///  =======================            ==========================================================================================================================================
    ///  "codigoUsuario"                    Código que identifica o usuário no sistema. Conterá o valor 0 caso haja problema na autenticação.
    ///  "nomeUsuario"                      Come do usuário registrado no sistema. Conterá uma string vazia caso haja problema na autenticação.
    ///  "emailUsuario"                     Email do usuário registrado no sistema. Conterá uma string vazia caso haja problema na autenticação.
    ///  "codigoEntidadeAcesso"             Código que identifica a entidade que usuário acessará no sistema. Conterá o valor 0 caso haja problema na autenticação.
    ///  "siglaEntidadeAcesso"              Sigla que identifica a entidade de acesso do usuário. Conterá uma string vazia caso haja problema na autenticação.
    ///  "urlAcessoMobAppWf"                Em princípio, conterá a mesma URL utilizada para a chamada do webservice. Nas situações em que estiver definido no parâmetros 
    ///                                     do usuário um valor diferente, conterá esse valor (usado para os casos de acessar os dados em ambiente de homologação.
    ///  "codigoResultadoProcessamento      Conterá códigos resultados do processamento conforme tabela abaixo:
    ///                                      0      validação feita com sucesso
    ///                                     -1      não foi possível validar as credenciais informadas
    ///                                     -2      as credenciais não foram validadas usando a autenticação do sistema e, apesar de indicado que deveria usar autenticação externa, não foi determinado o tipo de autenticação a usar.
    ///                                     -3      as credenciais não foram validadas usando a autenticação do sistema e não há suporte para o tipo de autenticação externa configurado.
    ///                                     -4      as credenciais não foram validadas usando a autenticação do sistema e houve uma falha ao obter as informações do usuário no LDAP do cliente.
    ///                                     -5      as credenciais não foram validadas usando a autenticação do sistema e não foi localizada nenhuma unidade organizacional de pesquisa para validar o usuário no sistema de autenticação do cliente
    ///                                     -6      as credenciais não foram validadas usando a autenticação do sistema e o usuário não foi validado em nenhuma unidade organizacional de pesquisa do sistema de autenticação do cliente
    ///                                     -7      as credenciais não foram validadas usando a autenticação do sistema e houve uma falha ao obter as informações do usuário no AD do cliente.
    ///                                     -8      O usuário foi autenticado no sistema do cliente, mas não foi identificado como um usuário do Portal.
    ///                                     -9      O usuário foi autenticado no sistema do cliente, mas ocorreu uma falha ao identificar o usuário no Portal.
    ///                                     -10      não foi determinado nenhuma entidade a que o usuário tem acesso</param>
    private void autenticarUsuario(string usuario, string senha, ref dadosConexao conexao)
    {
        int senhaCriptografa;
        string usuarioDeveAlterarSenha = "";
        string IDEstiloVisual = "";

        conexao.codigoUsuario = 0;
        conexao.nomeUsuario = "";
        conexao.codigoEntidadeAcesso = 0;
        conexao.siglaEntidadeAcesso = "";
        conexao.codigoResultadoProcessamento = -1;
        conexao.mensagemResultadoProcessamento = "";
        conexao.emailUsuario = usuario;

        conexao.urlAcessoMobAppWf = HttpContext.Current.Request.Url.AbsoluteUri;
        int posUrl = conexao.urlAcessoMobAppWf.IndexOf("wfMobile", 0, StringComparison.InvariantCultureIgnoreCase);

        if (posUrl > 1)
        {
            conexao.urlAcessoMobAppWf = conexao.urlAcessoMobAppWf.Substring(0, posUrl - 2);
        }

        DataSet dsAutenticacaoExterna = cDados.getParametrosSistema(-1, "utilizaAutenticacaoExterna_TIPO", "utilizaAutenticacaoExterna_DOMINIO", "utilizaAutenticacaoExterna_ServidorLDAP_AD");

        if (cDados.DataSetOk(dsAutenticacaoExterna) && cDados.DataTableOk(dsAutenticacaoExterna.Tables[0]))
        {
            //lê os valores retornados para alguns parametros
            utilizaAutenticacaoExterna_TIPO = dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna_TIPO"] + "";
            utilizaAutenticacaoExterna_DOMINIO = dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna_DOMINIO"] + "";
            utilizaAutenticacaoExterna_ServidorLDAP_AD = dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna_ServidorLDAP_AD"] + "";
        }


        // --------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 1º Tenta autenticar no Sistema - Tipo "AS"
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Tenta autenticar gerando a senha pelo metodo do .NET Framework - GetHashCode()
        senhaCriptografa = senha.GetHashCode();
        conexao.codigoUsuario = cDados.getAutenticacaoUsuario(usuario, senhaCriptografa, "AS", out conexao.nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

        // se não conseguiu autenticar, tenta pelo metodo desenvolvido pela CDIS - ObtemCodigoHash()
        if (conexao.codigoUsuario == 0)
        {
            senhaCriptografa = cDados.ObtemCodigoHash(senha);
            conexao.codigoUsuario = cDados.getAutenticacaoUsuario(usuario, senhaCriptografa, "AS", out conexao.nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
        }
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------
        // ------ FIM Autenticação "AS"
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 2º se NÃO conseguiu autenticar no sistema, tenta autenticar em um sistema próprio (LDAP/AD/Outros)
        // --------------------------------------------------------------------------------------------------------------------------------------------------------------
        if (conexao.codigoUsuario == 0)
        {
            // se o parametro que informa o tipo de autenticação externa não foi informado
            if (utilizaAutenticacaoExterna_TIPO == "")
            {
                conexao.nomeUsuario = ""; // "Não foi possível identificar o modelo de autenticação a ser utilizado."
                conexao.codigoResultadoProcessamento = -2;
            }
            else
            {
                // Se conseguiu autenticar no ldap/ad
                if (autenticacaoExternaServidorLDAP_AD(utilizaAutenticacaoExterna_TIPO, usuario, senha, out conexao.codigoUsuario, out conexao.codigoResultadoProcessamento))
                {
                    // se conseguiu autenticar... busca outras informações do usuário
                    if (conexao.codigoUsuario > 0)
                        cDados.getDadosAutenticacaoUsuarioExterno(conexao.codigoUsuario, out conexao.nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);
                }
            }
        }

        // se foi possível validar as credenciais do usuário
        if (conexao.codigoUsuario > 0)
        {

            // lê as informações do usuário
            cDados.setInfoSistema("IDUsuarioLogado", conexao.codigoUsuario);
            cDados.setInfoSistema("IDEstiloVisual", IDEstiloVisual);
            cDados.setInfoSistema("NomeUsuarioLogado", conexao.nomeUsuario);

            // busca a(s) entidade(s) do usuário logado
            DataSet ds = cDados.getEntidadesUsuario(conexao.codigoUsuario, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + conexao.codigoUsuario);
            int QtdeEntidades = ds.Tables[0].Rows.Count;

            if (QtdeEntidades == 0) // o usuário não tem acesso a nenhuma entidade
            {
                conexao.codigoEntidadeAcesso = 0;
                conexao.codigoResultadoProcessamento = -10;
            }
            else if (QtdeEntidades >= 1) // o usuário com acesso a pelo menos uma entidade
            {
                conexao.codigoResultadoProcessamento = 0;
                conexao.codigoEntidadeAcesso = (ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString() != "") ? int.Parse(ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString()) : 0;
                conexao.siglaEntidadeAcesso = ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();

                // se o usuário tem acesso a mais de uma entidade, verifica se entre elas está a que o usuário configurou como padrão
                if (QtdeEntidades > 1)
                {
                    string strWhere = string.Format(" AND UsuarioUnidadeNegocio.CodigoUsuario = {2} AND UsuarioUnidadeNegocio.CodigoUnidadeNegocio IN (SELECT u2.CodigoEntidadeAcessoPadrao FROM {0}.{1}.Usuario AS u2 WHERE u2.CodigoUsuario = {2}) ", bancodb, Ownerdb, conexao.codigoUsuario);
                    ds = cDados.getEntidadesUsuario(conexao.codigoUsuario, strWhere);
                    QtdeEntidades = ds.Tables[0].Rows.Count;

                    if (QtdeEntidades >= 1) // se veio registro considerando a entidade padrão do usuário
                    {
                        conexao.codigoEntidadeAcesso = (ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString() != "") ? int.Parse(ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString()) : 0;
                        conexao.siglaEntidadeAcesso = ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();
                    }
                }
            }
        }

        // se o usuário tem acesso a alguma entidade, busca a URL a acessar
        if (conexao.codigoEntidadeAcesso > 0)
        {
            string strAuxUrl = "", strIDAcesso = "";
            DataSet dsParametrosUsuario = cDados.getParametrosUsuario(conexao.codigoUsuario, "urlAcessoDadosWFMobile", "IDAcessoDadosWfMobile");

            if (cDados.DataSetOk(dsParametrosUsuario) && cDados.DataTableOk(dsParametrosUsuario.Tables[0]))
            {
                //verifica qual URL usar, para o usuário, no acesso à lista de pendências de workflow na aplicação mobile 
                strAuxUrl = dsParametrosUsuario.Tables[0].Rows[0]["urlAcessoDadosWFMobile"] + "";
                if (string.IsNullOrEmpty(strAuxUrl) == false)
                {
                    conexao.urlAcessoMobAppWf = strAuxUrl;
                }

                //verifica qual ID usar, para o usuário, no acesso à lista de pendências de workflow na aplicação mobile 
                strIDAcesso = dsParametrosUsuario.Tables[0].Rows[0]["IDAcessoDadosWfMobile"] + "";
                if (string.IsNullOrEmpty(strIDAcesso) == true)
                {
                    string strWhere = string.Format(" AND us.[CodigoUsuario] = {0} ", conexao.codigoUsuario);
                    DataSet dsUsr = cDados.getDadosResumidosUsuario(strWhere);
                    if (cDados.DataSetOk(dsUsr) && cDados.DataTableOk(dsUsr.Tables[0]))
                    {
                        conexao.emailUsuario = dsUsr.Tables[0].Rows[0]["EMail"].ToString();
                    }
                }
                else
                {
                    conexao.emailUsuario = strIDAcesso;
                }
            }
        }

        //criptografa a string de conexão a retornar
        conexao.stringConexao = Server.UrlEncode(criptografaStringConexao(conexao));

        if (conexao.codigoEntidadeAcesso == 0)
        {
            switch (conexao.codigoResultadoProcessamento)
            {
                case 0:
                    conexao.mensagemResultadoProcessamento = "";
                    break;
                case -1:
                    conexao.mensagemResultadoProcessamento = string.Format("Não foi possível validar as credenciais informadas ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -2:
                    conexao.mensagemResultadoProcessamento = string.Format("Não foi determinado o tipo de autenticação externa a usar ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -3:
                    conexao.mensagemResultadoProcessamento = string.Format("Não há suporte para o tipo de autenticação externa configurado ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -4:
                    conexao.mensagemResultadoProcessamento = string.Format("Falha ao obter as informações do usuário no LDAP do cliente ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -5:
                    conexao.mensagemResultadoProcessamento = string.Format("Não foi localizada nenhuma unidade organizacional de pesquisa para validar o usuário no sistema de autenticação do cliente ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -6:
                    conexao.mensagemResultadoProcessamento = string.Format("O usuário não foi validado em nenhuma unidade organizacional de pesquisa do sistema de autenticação do cliente ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -7:
                    conexao.mensagemResultadoProcessamento = string.Format("Houve uma falha ao obter as informações do usuário no AD do cliente ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -8:
                    conexao.mensagemResultadoProcessamento = string.Format("O usuário foi autenticado no sistema do cliente, mas não foi identificado como um usuário do Portal ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -9:
                    conexao.mensagemResultadoProcessamento = string.Format("O usuário foi autenticado no sistema do cliente, mas ocorreu uma falha ao identificar o usuário no Portal ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                case -10:
                    conexao.mensagemResultadoProcessamento = string.Format("Não foi determinado nenhuma entidade a que o usuário tem acesso ({0}).", conexao.codigoResultadoProcessamento);
                    break;
                default:
                    conexao.mensagemResultadoProcessamento = string.Format("Falha ao autenticar o usuário ({0}).", conexao.codigoResultadoProcessamento);
                    break;
            }

        }
    }

    /// <summary>
    ///  criptografa a string de conexão a ser devolvida na autenticação do usuário
    /// </summary>
    /// <param name="conexao">Objeto que contém os dados da conexão do usuário</param>
    /// <returns>retorna a string de conexão criptografada com a chave registrada nos parâmetros do sistema.</returns>
    private string criptografaStringConexao(dadosConexao conexao)
    {
        string chavePrivada, stringToCrypto, stringCriptografada, stringPlana;
        string strDataHora;
        DateTime dtNow = DateTime.Now;
        int iQtdX;
        strDataHora = string.Format("{0:O}", dtNow);

        DataSet dsChaveWfMobile = cDados.getParametrosSistema(-1, "chaveAutenticacaoWsMobile");
        stringCriptografada = string.Format("{0};{1};{2}", conexao.emailUsuario, strDataHora, conexao.codigoEntidadeAcesso);

        if (cDados.DataSetOk(dsChaveWfMobile) && cDados.DataTableOk(dsChaveWfMobile.Tables[0]))
        {
            chavePrivada = dsChaveWfMobile.Tables[0].Rows[0]["chaveAutenticacaoWsMobile"] + "";
            stringPlana = null;
            iQtdX = 0;

            while ((iQtdX < 10) && string.IsNullOrEmpty(stringPlana))
            {
                stringToCrypto = string.Format("{0};{1};{2}", conexao.emailUsuario.PadRight(conexao.emailUsuario.Length + (iQtdX % 3), ' '), strDataHora.PadRight(strDataHora.Length + (iQtdX / 3), ' '), conexao.codigoEntidadeAcesso);
                stringCriptografada = Cripto.criptografar(stringToCrypto, chavePrivada);
                stringPlana = Cripto.descriptografar(stringCriptografada, chavePrivada);
            }
        }
        return stringCriptografada;
    }

    /// <summary>
    /// Função para fazer a autenticação via LDAP do usuário. Usadas nas situações em que os parâmetros do sistema indicam que é esse tipo de autenticação que deve ser usado.
    /// </summary>
    /// <param name="tipo">Parâmetro indicando que tipo de autenticação externa será usada (LDAP ou AD)</param>
    /// <param name="usuario">identificação do usuário a ser autenticada no sistema</param>
    /// <param name="senha">Senha do usuário a ser usada na autenticação</param>
    /// <param name="codigoUsuario">Parâmetro de saída: código que identifica o usuário no sistema. Conterá o valor 0 caso haja problema na autenticação.</param>
    /// <param name="codigoResultadoProcessamento">Parâmetro de saída: conterá códigos resultados do processamento conforme tabela abaixo:
    ///  0      validação feita com sucesso
    /// -3      não há tratamento para o tipo de autenticação configurado
    /// -4      falha ao obter as informações do usuário no LDAP do cliente
    /// -5      não foi localizada nenhuma unidade organizacional de pesquisa para validar o usuário no sistema de autenticação do cliente
    /// -6      o usuário não foi validado em nenhuma unidade organizacional de pesquisa do sistema de autenticação do cliente
    /// -7      falha ao obter as informações do usuário no AD do cliente
    /// -8      o usuário foi autenticado no sistema do cliente, mas não foi identificado como um usuário do Portal.
    /// -9      falha ao obter as informações do usuário no Portal</param>
    /// <returns>Retorna true se a autenticação foi bem sucedida. Caso contrário, retorna false.</returns>
    private bool autenticacaoExternaServidorLDAP_AD(string tipo, string usuario, string senha, out int codigoUsuario, out int codigoResultadoProcessamento)
    {
        codigoUsuario = 0;

        string comandoSql = "Select codigoUsuario, IDEstiloVisual, NomeUsuario from usuario where ";

        if (tipo == "LDAP")
        {
            // posição do caracter "@" no campo nome do usuário.
            int posicaoArrobaNoCampoUsuario = usuario.IndexOf('@');

            string username = "uid=" + (posicaoArrobaNoCampoUsuario >= 0 ? usuario.Substring(0, posicaoArrobaNoCampoUsuario) : usuario) + ",";

            // se tem contrabarra no nome do usuário, o complemento não será utilizado, pois será feito com AD
            if (usuario.IndexOf('\\') > 0)
                username = usuario;

            // seleciona as unidades organizacionais de pesquisas
            string comandoSQL_LDAP_AD = "SELECT valor FROM ParametroConfiguracaoSistema WHERE Parametro LIKE 'LDAP_AD_DN%' ORDER BY Parametro";
            DataSet dsLDAP_AD = cDados.getDataSet(comandoSQL_LDAP_AD);
            bool autenticadoComSucesso = false;

            codigoResultadoProcessamento = -5;

            foreach (DataRow dr in dsLDAP_AD.Tables[0].Rows)
            {
                codigoResultadoProcessamento = -6;
                string complemento = dr["valor"].ToString().Trim();
                if (complemento == "")
                    continue;

                if (usuario.IndexOf('\\') > 0)
                    complemento = "";


                string usuarioLDAP = username + complemento;
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + utilizaAutenticacaoExterna_ServidorLDAP_AD,
                                                           usuarioLDAP,
                                                           senha, AuthenticationTypes.ServerBind);
                try
                {
                    Object obj = entry.NativeObject;
                    // se chegou aqui é por ter autenticado no LDAP com sucesso. Não precisa continuar.
                    autenticadoComSucesso = true;
                    codigoResultadoProcessamento = 0;
                    break;
                }
                catch (Exception)
                {
                    codigoResultadoProcessamento = -4;
                }
            }

            // se deu erro em todas as tentativas de autenticação, mostra a mensagem e sai.
            if (!autenticadoComSucesso)
            {
                return false;
            }

            // sucesso ao autenticar o usuário no ldap/ad. Agora vamos verificar se ele existe no banco do portal.
            // se não é usuário de domínio (não tem contrabarra)
            if (usuario.IndexOf('\\') < 0)
            {
                // se não digitou o caracter "@", vamos incluí-lo
                if (posicaoArrobaNoCampoUsuario < 0)
                    usuario += "@" + utilizaAutenticacaoExterna_DOMINIO;
                comandoSql += string.Format(" email = '{0}' and dataExclusao is null", usuario);
            }
            else // usuário de domínio, vamos pesquisar com o campo contaWindows
            {
                comandoSql += string.Format(" ContaWindows = '{0}' and dataExclusao is null", usuario);
            }
        }
        else if (tipo == "AD")
        {
            try
            {
                LdapConnection ldc = new LdapConnection(new LdapDirectoryIdentifier(utilizaAutenticacaoExterna_ServidorLDAP_AD, 389, true, false));

                NetworkCredential ncon = new NetworkCredential(usuario, senha);//, "cdis");
                ldc.Credential = ncon;
                ldc.AuthType = AuthType.Basic;
                ldc.Bind(ncon);
            }
            catch (Exception)
            {
                codigoResultadoProcessamento = -7;
                return false;
            }
            comandoSql += string.Format(" ContaWindows = '{0}' and dataExclusao is null", usuario);
        }
        else
        {
            codigoResultadoProcessamento = -3;
            return false;
        }

        try
        {
            DataSet ds = cDados.getDataSet(comandoSql);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUsuario = int.Parse(ds.Tables[0].Rows[0]["codigoUsuario"].ToString());
                codigoResultadoProcessamento = 0;
                return true;
            }

            codigoResultadoProcessamento = -8;
            return false;
        }
        catch (Exception ex)
        {
            codigoResultadoProcessamento = -9;
            return false;
        }
    }

}
