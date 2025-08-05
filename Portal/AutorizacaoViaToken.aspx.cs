using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Web.UI;

public partial class AutorizacaoViaToken : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var redirectUrl = Request.QueryString["redirect_url"];
        var resolucao = Request.QueryString["res"];
        var token = Request.QueryString["token"];
        JwtSecurityToken jwtSecurityToken;
        try
        {
            jwtSecurityToken = new JwtSecurityToken(token);
        }
        catch
        {
            jwtSecurityToken = null;
        }
        if (ValidateJwtSecurityToken(jwtSecurityToken))
        {
            var idUsuarioLogado = jwtSecurityToken.Payload.Sub;
            var idEntidadeAcesso = jwtSecurityToken.Payload.Claims.Where(c => c.Type == "ent").FirstOrDefault().Value;
            DefineDadosSessao(idUsuarioLogado, resolucao, idEntidadeAcesso, token);

            if (string.IsNullOrWhiteSpace(redirectUrl))
                redirectUrl = "index.aspx?PassouResolucao=S";

            string baseUrl;
            bool indicaHttpsViaProxyReverso = false;
            DataSet dsParam1 = cDados.getParametrosSistema("httpsViaProxyReverso");
            if (cDados.DataSetOk(dsParam1) && cDados.DataTableOk(dsParam1.Tables[0]))
            {
                indicaHttpsViaProxyReverso = dsParam1.Tables[0].Rows[0]["httpsViaProxyReverso"].ToString().ToUpper() == "S";
            }

            if (indicaHttpsViaProxyReverso == true)
            {
                baseUrl = "https://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
            }
            else
            {

                bool indicaHttps = false;
                DataSet dsParam = cDados.getParametrosSistema("utilizaProtocoloHttps");
                if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
                {
                    indicaHttps = dsParam.Tables[0].Rows[0]["utilizaProtocoloHttps"].ToString().ToUpper() == "S";
                }

                /* Experimento Bootstrap */
                if (indicaHttps == true)
                {
                    baseUrl = "https://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
                }
                else
                {
                    baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');
                }
            }
            
            //ClientScript.RegisterClientScriptBlock(Page.GetType(), "blocodescript", string.Format(@"<script type=""text/javascript"">debugger;localStorage.setItem('token', '{0}');</script>", token));
            redirectUrl = string.Format("{0}/{1}", baseUrl, redirectUrl.TrimStart('/'));
            Response.Redirect(redirectUrl);
        }
        else
        {
            Response.ClearContent();
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.End();
        }
    }

    private void DefineDadosSessao(string idUsuarioLogado, string resolucao, string idEntidadeAcesso, string token)
    {
        cDados.setInfoSistema("IDUsuarioLogado", idUsuarioLogado);
        cDados.setInfoSistema("ResolucaoCliente", resolucao);
        string sWhere = string.Format(@" AND (CodigoUnidadeNegocio = {0} 
            AND EXISTS( 
                    SELECT TOP 1 1 FROM [dbo].[UsuarioUnidadeNegocio] AS uun 
                        WHERE uun.[CodigoUnidadeNegocio] = {0} AND uun.[CodigoUsuario] = {1} AND IndicaUsuarioAtivoUnidadeNegocio = 'S' ) ) ", idEntidadeAcesso, idUsuarioLogado);

        DataSet ds = cDados.getUnidade(sWhere);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            const int MinusOne = -1;
            var row = ds.Tables[0].AsEnumerable().FirstOrDefault();
            var siglaUnidadNegocio = row.Field<string>("SiglaUnidadeNegocio");
            var codigoEntidadeAcesso = row.IsNull("CodigoUnidadeNegocio") ? MinusOne : row.Field<int>("CodigoUnidadeNegocio");

            if (codigoEntidadeAcesso != MinusOne)
            {
                string codigoCarteira = cDados.getCodigoCarteiraPadraoUsuario(int.Parse(idUsuarioLogado), codigoEntidadeAcesso).ToString();

                cDados.setInfoSistema("Opcao", "et"); // espaço de trabalho
                cDados.setInfoSistema("SiglaUnidadeNegocio", siglaUnidadNegocio);
                cDados.setInfoSistema("CodigoEntidade", codigoEntidadeAcesso.ToString());
                cDados.setInfoSistema("CodigoCarteira", codigoCarteira);
                Session["TokenAcessoNewBrisk"] = token;
                cDados.setInfoSistema("novoMenuAdministracao", null);
                DataSet dsParam = cDados.getParametrosSistema("urlWSnewBriskBase");
                if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
                {
                    Session["urlWSnewBriskBase"] = dsParam.Tables[0].Rows[0]["urlWSnewBriskBase"].ToString();
                }
                string nomeUsuario;
                string IDEstiloVisual;
                string usuarioDeveAlterarSenha;
                var codigoUsuario = int.Parse(idUsuarioLogado);
                cDados.getDadosAutenticacaoUsuarioExterno(codigoUsuario, out nomeUsuario, out IDEstiloVisual, out usuarioDeveAlterarSenha);

                cDados.setInfoSistema("IDEstiloVisual", IDEstiloVisual);
                cDados.setInfoSistema("NomeUsuarioLogado", nomeUsuario);
                cDados.setInfoSistema("NomeUsuario", nomeUsuario);
                Session["NomeUsuario"] = nomeUsuario;
                Session["RemoteIPUsuario"] = Request.UserHostAddress;
                Session["notificacoes"] = null;
                if (cDados.PerfilAdministrador(codigoUsuario, codigoEntidadeAcesso))
                    cDados.setInfoSistema("PerfilAdministrador", 1);
            }
        }
        else
            cDados.setInfoSistema("Opcao", "se"); // seleciona entidade
    }

    static bool ValidateJwtSecurityToken(JwtSecurityToken jwtSecurityToken)
    {
        if (jwtSecurityToken == null)
            return false;

        int idUsuarioLogado;
        var agora = DateTime.UtcNow;
        return (
            int.TryParse(jwtSecurityToken.Payload.Sub, out idUsuarioLogado) &&
            jwtSecurityToken.ValidFrom <= agora &&
            jwtSecurityToken.ValidTo >= agora);
    }
}