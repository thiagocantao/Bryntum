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
//using System.Security.Principal;
using System.Web.Hosting;
using System.Threading;
using System.Collections.Specialized;
using System.Xml;

public partial class po_autentica : System.Web.UI.Page
{
    dados cDados;
    string usuarioRede;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bAutenticacaoExterna;
        cDados = CdadosUtil.GetCdados(null);

        DataSet dsAutenticacaoExterna = cDados.getParametrosSistema(-1, "utilizaAutenticacaoExterna");

        bAutenticacaoExterna = (cDados.DataSetOk(dsAutenticacaoExterna) && cDados.DataTableOk(dsAutenticacaoExterna.Tables[0]) && dsAutenticacaoExterna.Tables[0].Rows[0]["utilizaAutenticacaoExterna"].ToString() == "S");

        bAutenticacaoExterna = bAutenticacaoExterna && (Session["modoLoginFNDE"] == null || Session["modoLoginFNDE"].ToString() != "2");

        /* No FNDE, a autenticação é feita por outro sistema e o valor do parâmetro "utilizaAutenticacaoExterna" deve ser "S" (se houver registro na sessão de que o modologin=2, ignora autenticação externa
         * Em PBH, a autenticação é feita no LDAP, mas o valor do parâmetro "utilizaAutenticacaoExterna" deve ser "N"
         * Em todos os outros clientes a autenticação é feita pelo IIS ou pelo próprio sistema, o valor do parametro também deve ser "N"
         * =================================================================================================================================================*/
        if (bAutenticacaoExterna)
            usuarioRede = obtemUsuarioFromHmtlHeaders(); // tenta obter o id do usuário através dos headers HTML; (nova forma de autenticação do FNDE)
        else
            usuarioRede = User.Identity.Name.ToString(); // para esta linha funcionar, o arquivo deve estar marcado no IIS apenas como "Autenticação Windows"

        lblUsuarioRede.Text = usuarioRede;

        //usuarioRede = "CDIS\\ericsson.cantao";

        if (usuarioRede == "")
            usuarioRede = "UNI"; // Usuario Nao Identificado

        /*tenta autenticar o usuário da rede no banco de dados*/
        string nomeUsuario = "";
        string IDEstiloVisual = "";
        string usuarioDeveAlterarSenha = "";
        int codigoUsuario;

        if (usuarioRede == "UNI")
            codigoUsuario = 0;
        else
            codigoUsuario = cDados.getAutenticacaoUsuario(usuarioRede, null, "AI", out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

        // se é um usuário válido...
        if (codigoUsuario > 0)
        {
            // lê as informações do usuário
            cDados.setInfoSistema("IDUsuarioLogado", codigoUsuario);
            cDados.setInfoSistema("IDEstiloVisual", IDEstiloVisual);
            cDados.setInfoSistema("NomeUsuarioLogado", nomeUsuario);
            Session["NomeUsuario"] = nomeUsuario;
            Session["RemoteIPUsuario"] = Request.UserHostAddress.ToString();

            // busca a(s) entidade(s) do usuário logado
            DataSet ds = cDados.getEntidadesUsuario(codigoUsuario, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuario);
            int QtdeEntidades = ds.Tables[0].Rows.Count;
            // determina qual tela será a inicial;
            int codigoEntidade = -1;
            if (QtdeEntidades == 0) // o usuário não tem acesso a nenhuma entidade
                codigoEntidade = 0;
            else if (QtdeEntidades == 1) // o usuário não tem acesso a apenas uma entidade
            {
                codigoEntidade = (ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"] != null && ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString() != "") ? int.Parse(ds.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString()) : 0;
                cDados.setInfoSistema("SiglaUnidadeNegocio", ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString());
            }
            // se o usuário tem acesso a uma ou mais entidades, volta para Index e continua a aplicação
            if (QtdeEntidades >= 1)
            {
                cDados.setInfoSistema("CodigoEntidade", codigoEntidade);

                if (codigoEntidade != -1)
                    cDados.setInfoSistema("CodigoCarteira", cDados.getCodigoCarteiraPadraoUsuario(codigoUsuario, codigoEntidade).ToString());

                try
                {
                    //Apaga os arquivos temporários
                    Thread thr = new Thread(new ThreadStart(cDados.apagaArquivosTemporarios));

                    thr.Name = Thread.CurrentThread.GetHashCode().ToString();

                    thr.Start();
                }
                catch
                { }
                //TODO: alterar e redirecionar para login quando a resposta do Hub for desconexao forçada
                Response.Redirect("~/index.aspx?" + Request.QueryString);
            }
            
            // Se chegou aqui é porque o usuário não está associado a nenhuma unidade. 
            //Neste caso, permanece nesta tela que irá mostrar a mensagem informando-o da falta de acesso.
            lblErro.Text = "No momento, você não é um usuário ativo em nenhuma unidade. Entre em contato com o administrador do sistema.";
        }
        // o usuário de rede naõ é válido... mostra a tela de login do sistema, somente se não for autenticação externa
        else if (false == bAutenticacaoExterna)
        {
            Response.Redirect("~/login.aspx?UNI="+usuarioRede + "&" + Request.QueryString);
        }
    }

    private string obtemUsuarioFromHmtlHeaders()
    {
        string perfisUsuario = "", nomeUsuarioExterno = "", emailUsuario = "", userLogin = "", userID = "";
        int codigoUsuarioExterno = 0;

        if (Request.Headers["fnde_nome_usuario"] != null)
        {
            nomeUsuarioExterno = Request.Headers["fnde_nome_usuario"];
        }

        if (Request.Headers["fnde_email_usuario"] != null)
        {
            emailUsuario = Request.Headers["fnde_email_usuario"];
        }

        if (Request.Headers["fnde_dn_usuario"] != null)
        {
            userLogin = Request.Headers["fnde_dn_usuario"];
        }

        if (Request.Headers["fnde_id_usuario"] != null)
        {
            userID = Request.Headers["fnde_id_usuario"];
        }

        bool retornoSinc = false;
        if ((nomeUsuarioExterno == "") || (emailUsuario == "") || (userLogin == "") || (userID == ""))
        {
            lblErro.Text = "Não foi possível obter a identificação do usuário.";
            userID = "";  // para garantir que será retornado usuário em branco.
        }
        else
        {
            if (true == obtemPerfisUsuarioAutenticacaoExterna(userLogin, out perfisUsuario))
                retornoSinc = cDados.sincronizaUsuarioExterno(userID, "", nomeUsuarioExterno, emailUsuario, perfisUsuario, ref codigoUsuarioExterno);
        }

        if (retornoSinc == false)
        {
            btnLogin.Text = "Tentar novamente";
            userID = ""; // para garantir que será retornado usuário em branco.
        }
        return userID;
    }

    /// <summary>
    /// Busca os perfis do usuário de autenticação externa.
    /// </summary>
    /// <remarks>
    /// Função para buscar os perfis que um usuário possui quando a autenticação está sendo feita por um sistema externo.
    /// . Implementação para o FNDE
    /// </remarks>
    /// <param name="userId">ID do usuário de acordo com o obtido durante a autenticação externa</param>
    /// <param name="perfis">parâmetro de saída. Conterá a lista de perfis que o usuário possui. A lista estará delimitada pelo caracter ¥</param>
    /// <returns>Se conseguir obter algum retorno do webservice, retornará true. Caso contrário, retorna false. Nos casos em que retornar true, 
    /// o parâmetro de saída <paramref name="perfis"/>conterá um valor válido, mesmo que vazio.</returns>
    private bool obtemPerfisUsuarioAutenticacaoExterna(string userId, out string perfis)
    {
        string xmlInformacoes;
        string idPortal = "portalCDIS";
        string versaoXml = "1";
        string idPerfilAdm = "", idPerfilExecutivo = "", idPerfilUsuario = "", idPerfilPerson1 = "";
        string innerText = "";
        bool resultado = false;
        perfis = "";

        DataSet dsPmt = cDados.getParametrosSistema(-1, "IDPortalAutenticacaoExterna", "VersaoXMLAutenticacaoExterna", "itgExt_ID_PerfilAdministrador", "itgExt_ID_PerfilExecutivo", "itgExt_ID_PerfilUsuario", "itgExt_ID_PerfilPerson1");
        if (cDados.DataSetOk(dsPmt) && cDados.DataTableOk(dsPmt.Tables[0]))
        {
            idPortal = dsPmt.Tables[0].Rows[0]["IDPortalAutenticacaoExterna"].ToString();
            versaoXml = dsPmt.Tables[0].Rows[0]["VersaoXMLAutenticacaoExterna"].ToString();
            idPerfilAdm = dsPmt.Tables[0].Rows[0]["itgExt_ID_PerfilAdministrador"].ToString().ToLower();
            idPerfilExecutivo = dsPmt.Tables[0].Rows[0]["itgExt_ID_PerfilExecutivo"].ToString().ToLower();
            idPerfilUsuario = dsPmt.Tables[0].Rows[0]["itgExt_ID_PerfilUsuario"].ToString().ToLower();
            idPerfilPerson1 = dsPmt.Tables[0].Rows[0]["itgExt_ID_PerfilPerson1"].ToString().ToLower();
        }

        xmlInformacoes = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>
                                    <request>
                                      <header>
                                        <app>{0}</app>
                                        <version>{1}</version>
                                        <created>{2:yyyy-MM-dd}T{2:HH:mm:ss}</created>
                                      </header>
                                      <body>
                                        <user_dn>{3}</user_dn>
                                      </body>
                                    </request>"
            , idPortal
            , versaoXml
            , DateTime.Now
            , userId);

        XmlDocument xmlRetorno = new XmlDocument();
        try
        {
            xmlRetorno = cDados.getDadosAutenticacaoExterna(xmlInformacoes, "urlInformacoesUsuarioExterno");
        }
        catch (Exception ex)
        {
            lblErro.Text = string.Format("Falha na comunicação com o serviço de informações de perfis. Mensagem original: {0}", ex.Message);
            return false;
        }

        try
        {
            XmlNode noStatus = xmlRetorno.SelectSingleNode("/response/status");
            string strResult = noStatus.SelectSingleNode("result").InnerText;

            resultado = strResult == "1" || strResult.ToLower() == "true";

            if (resultado)
            {
                XmlNode noPerfis = xmlRetorno.SelectSingleNode("/response/body");

                foreach (XmlNode no in noPerfis.ChildNodes) 
                {
                    innerText = no.InnerText.ToLower();
                    if (innerText.Contains(idPerfilAdm))
                        perfis += idPerfilAdm + "¥";

                    if (innerText.Contains(idPerfilExecutivo))
                        perfis += idPerfilExecutivo + "¥";

                    if (innerText.Contains(idPerfilUsuario))
                        perfis += idPerfilUsuario + "¥";

                    if (innerText.Contains(idPerfilPerson1))
                        perfis += idPerfilPerson1 + "¥";
                }

                if (perfis != "")
                    perfis = perfis.Substring(0, perfis.Length - 1);
            }
            else
            {

                lblErro.Text = noStatus.SelectSingleNode("error/message/text").InnerText;
                return false;
            }
        }
        catch
        {
            lblErro.Text = "Erro ao obter os perfis do usuário! O XML de retorno está em um formato inválido.";
            return false;
        }

        return true;
    }
    
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/login.aspx?UNI=" + usuarioRede);
    }

    public string getIPMaquina()
    {
        return Request.UserHostAddress;
    }
}
