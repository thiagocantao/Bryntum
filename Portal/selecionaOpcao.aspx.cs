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
using System.Collections.Specialized;
using System.Globalization;
using DevExpress.Utils.OAuth;

public partial class selecionaOpcao : System.Web.UI.Page
{
    dados cDados;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (cDados.getInfoSistema("Opcao") != null)
        {
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            int idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

            bool entidadeExpirada = false;

            DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "DataExpiracaoSistema");

            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["DataExpiracaoSistema"].ToString() != "")
            {
                DateTime dtCampo = new DateTime();
                bool converteu = DateTime.TryParseExact(dsParametros.Tables[0].Rows[0]["DataExpiracaoSistema"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCampo);

                if (converteu && dtCampo < DateTime.Now.Date)
                {
                    entidadeExpirada = true;
                    cDados.setInfoSistema("CodigoEntidade", -1);
                }
            }

            string opcao = "";
            if (Request.QueryString["op"] != null)
            {
                opcao = Request.QueryString["op"];
                cDados.setInfoSistema("Opcao", opcao);
            }
            else
                opcao = cDados.getInfoSistema("Opcao").ToString();

            if (opcao == "sa") // sair
            {
                // Lê a resolução, pois na linha debaixo, todas as informações de sistema serão apagagas
                string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
                // apaga todas as informações de sistema
                cDados.clearInfoSistema();
                Session.Remove("notificacoes");
                // volta a resolução para as informações de sistema
                cDados.setInfoSistema("ResolucaoCliente", ResolucaoCliente);
                // novo login
                Response.Redirect("~/login.aspx");
            }
            else if (opcao == "se") // seleciona Entidade
            {
                Response.Redirect("~/selecionaEntidade.aspx");
            }
           
            
            string urlDestino = "~/espacoTrabalho/dashboard.aspx";

            if (entidadeExpirada)
            {
                urlDestino = "~/erros/LicencaExpirada.aspx";
            }
            else
            {
                if (Request.QueryString["TelaIni"] != null && Request.QueryString["TelaIni"].ToString() != "")
                {
                    urlDestino = Server.UrlDecode(Request.QueryString["TelaIni"].ToString()) + "?" + Request.QueryString.ToString();
                }
                else
                {
                    DataSet ds = cDados.getURLTelaInicialUsuario(codigoEntidade.ToString(), idUsuarioLogado.ToString());

                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {

                        if (VerificaSeEhMobile() == true)
                        {

                            urlDestino = getTelaInicialPreferencialMobile(idUsuarioLogado);
                            if (string.IsNullOrEmpty(urlDestino))
                            {
                                urlDestino = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                            }
                        }
                        else
                        {
                            urlDestino = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                        }
                    }
                }

                ListDictionary tabDados = new ListDictionary();
                tabDados.Add("CodigoUsuario", idUsuarioLogado);
                tabDados.Add("DataAcesso", "GetDate()");
                tabDados.Add("CodigoEntidade", codigoEntidade.ToString());
                tabDados.Add("TipoAcesso", "Sistema");
                string sqlIncluir = cDados.classeDados.getInsert("LogAcessoUsuario", tabDados);
                int regafetados = 0;
                bool retorno = cDados.execSQL(sqlIncluir, ref regafetados);
                if (!retorno)
                {
                    //erro na hora de incluir no log do sistema
                    Response.Redirect("~/erro.aspx");
                }
            }
            Session["CodigoAutorizacaoBrisk1"] = geraTokenAutorizacaoNewBrisk();
            Response.Redirect(urlDestino);  
        }
        else
            Response.Redirect("~/index.aspx");
    }

    private string geraTokenAutorizacaoNewBrisk()
    {
        var retorno = "";
        string comandoSQL = "";
        try
        {
            Guid tokenGuid = Guid.NewGuid();
            //x.ToString()
            //Gerar o token, Acionar a proc para gravar o token e ao final gravar o token na variavel de sessão.
            //            string comandoSQL = @"
            int idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

             comandoSQL =  string.Format(@"
            DECLARE @RC int
            DECLARE @siglaFederacaoIdentidade varchar(30)
            DECLARE @codigoAutorizacaoAcesso varchar(500)
            DECLARE @tokenAcesso varchar(4000)
            DECLARE @CodigoUsuario int
            DECLARE @codigoControleToken bigint

           set @siglaFederacaoIdentidade = 'brisk_em'
           set @codigoAutorizacaoAcesso = '{0}'
           set @tokenAcesso = null
           set @CodigoUsuario = {1}

           EXECUTE @RC = [dbo].[p_brk_rw_registraTokenUsuarioFederacaoIdentidade]
               @siglaFederacaoIdentidade
              ,@codigoAutorizacaoAcesso
              ,@tokenAcesso
              ,@CodigoUsuario
              ,@codigoControleToken OUTPUT
          
        SELECT @codigoAutorizacaoAcesso as codigoAutorizacaoAcesso, @codigoControleToken as codigoControleToken", tokenGuid.ToString(), idUsuarioLogado);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                retorno = ds.Tables[0].Rows[0]["codigoAutorizacaoAcesso"].ToString();
            }
        }
        catch (Exception ex)
        {
            retorno = "";
        }
        return retorno;

    }

    private string getTelaInicialPreferencialMobile(int idUsuarioLogado)
    {
        string retorno = "";
        DataSet dsTelaUsuario = cDados.getUsuarios(string.Format(" and CodigoUsuario = {0} ", idUsuarioLogado));
        if (cDados.DataSetOk(dsTelaUsuario) && cDados.DataTableOk(dsTelaUsuario.Tables[0]))
        {
            retorno = dsTelaUsuario.Tables[0].Rows[0]["UrlTelaInicialMobileUsuario"].ToString();
        }
        return retorno;
    }

    public bool VerificaSeEhMobile()
    {
        if (this.Request.Browser.IsMobileDevice || Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null ||
          (Request.ServerVariables["HTTP_ACCEPT"] != null && Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap")))
        {
            return true;
        }
        else if (Request.ServerVariables["HTTP_USER_AGENT"] != null)
        {
            string[] mobiles = new[]
            {
                    "midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "NEC", "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone", "ipad", "windows phone"
                };

            foreach (string s in mobiles)
            {
                if (Request.ServerVariables["HTTP_USER_AGENT"].ToLower().Contains(s.ToLower()))
                {
                    return true;
                }
            }

        }

        return false;
    }
}
