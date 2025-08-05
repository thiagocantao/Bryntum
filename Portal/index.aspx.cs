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

public partial class index : System.Web.UI.Page
{
    dados cDados;

    // será utilizada para passar parametros para as outras telas via URL (inclusive para titulo.aspx)
    //public string parametrosURL;

    // define a tela inicial que o usuário terá acesso
    //public string telaInicial;
    public string alturaPrincipal;
    public string idUsuarioLogado;


    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        if (Request.QueryString["PassouResolucao"] == null)
            Response.Redirect("getResolucaoCliente.aspx?" + Request.QueryString);

        // obtem a resolução da área cliente do browser que está sendo utilizado
        if (cDados.getInfoSistema("ResolucaoCliente") == null) // RC = Resolução Cliente
            Response.Redirect("getResolucaoCliente.aspx?" + Request.QueryString);
        else
        {
            string resolucao = cDados.getInfoSistema("ResolucaoCliente").ToString();

            int largura = int.Parse(resolucao.Substring(0, resolucao.IndexOf('x')));
            int altura = int.Parse(resolucao.Substring(resolucao.IndexOf('x') + 1));
            /*
            * Comentário de Guilherme Cruz em 26/10/2018
            * Estas linhas abaixo já existiam no Portal, e o que elas fazem é ignorar os dispositivos móveis menores (smartphones, etc).
            * int novaLargura = (largura < 975) ? 975 : largura;
            * int novaAltura = (altura < 540) ? 540 : altura;
            * Em função disso, algumas telas não se comportam bem em smartphones. Uma possível solução seria não fixar uma largura/altura padrão para dispositivos móveis.
            * Bastaria fazer:
            * int novaLargura = largura;
            * int novaAltura = altura;
            * No entanto, após aplicar este método e colocá-lo em prática, muitas alturas calculadas nas telas ficaram negativas e geraram erros de runtime.
            * Portanto, estamos voltando às linhas anteriores e tentaremos outra abordagem.
            */
            /*
           int novaLargura = largura;
           int novaAltura = altura;
           */
            cDados.setInfoSistema("ResolucaoClienteReal", largura + "x" + altura);

            int novaLargura = (largura < 975) ? 975 : largura;
            int novaAltura = (altura < 540) ? 540 : altura;
            resolucao = novaLargura + "x" + novaAltura;


            cDados.setInfoSistema("ResolucaoCliente", resolucao);
        }

        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado") != null ? cDados.getInfoSistema("IDUsuarioLogado").ToString() : "";
       
        if (idUsuarioLogado == "")
        {
            string novaUrl = "";
            // se não tem a variavel de sessão abaixo, terá que logar novamente - pode ser Login.aspx ou Autentica.aspx
            if (cDados.getInfoSistema("Origem") != null && cDados.getInfoSistema("Origem").ToString()=="Login")
                novaUrl = "login.aspx?" + Request.QueryString;
            else
                novaUrl = "po_autentica.aspx?" + Request.QueryString;

            // retira o objeto da sessão
            cDados.clearInfoSistema("Origem");

            Response.Redirect(novaUrl);
        }

        alturaPrincipal = getAlturaPrincipal().ToString() + "px";
        
        // se o usuário está em mais de uma entidade
        if (cDados.getInfoSistema("CodigoEntidade").ToString() == "-1")
        {            
            DataSet ds = cDados.obtemEntidadPadraoUsuarioLogado(idUsuarioLogado);

            if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                string codigoEntidadPadrao = ds.Tables[0].Rows[0]["CodigoEntidadeAcessoPadrao"].ToString();
                string siglaUnidadNegocio = ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString();

                if ("-1" != codigoEntidadPadrao)
                {
                    
                    cDados.setInfoSistema("Opcao", "et"); // espaço de trabalho
                    cDados.setInfoSistema("CodigoEntidade", codigoEntidadPadrao);
                    cDados.setInfoSistema("SiglaUnidadeNegocio", siglaUnidadNegocio);

                    if (int.Parse(codigoEntidadPadrao) != -1)
                        cDados.setInfoSistema("CodigoCarteira", cDados.getCodigoCarteiraPadraoUsuario(int.Parse(idUsuarioLogado), int.Parse(codigoEntidadPadrao)).ToString());
                }
            }
            else
                cDados.setInfoSistema("Opcao", "se"); // seleciona entidade
        }
        else
            cDados.setInfoSistema("Opcao", "et"); // espaço de trabalho


        // Carrega o Mapa Estratégico Padrão

        DataSet dsMapaDefaultUsuario = cDados.getCodigoMapaEstrategicoPadraoUsuario(int.Parse(idUsuarioLogado));
        if (cDados.DataSetOk(dsMapaDefaultUsuario) && cDados.DataTableOk(dsMapaDefaultUsuario.Tables[0]))
        {
            cDados.setInfoSistema("CodigoMapaEstrategicoInicial", dsMapaDefaultUsuario.Tables[0].Rows[0]["CodigoMapaEstrategicoPadrao"].ToString());
        }

        //Pega o Nome da Tela Inicial
        var dsTelaNomePadrao = cDados.GetNomeTelaUrlPadraoUsuarioIdioma(idUsuarioLogado.ToString(), cDados.getInfoSistema("CodigoEntidade").ToString());
        if (Session["NomeArquivoNavegacao"] == null)
        {
            string nomeArquivo = "/ArquivosTemporarios/xml_Caminho" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + idUsuarioLogado + ".xml"; ;
            string urlDestino = "";
            string nomeTela = "";

            if (Request.QueryString["TelaIni"] != null && Request.QueryString["TelaIni"].ToString() != "")
            {
                urlDestino = Server.UrlDecode(Request.QueryString["TelaIni"].ToString()) + "?" + Request.QueryString.ToString();
                nomeTela = dsTelaNomePadrao.Tables[0].Rows[0]["NomeObjetoMenu"].ToString();
            }
            else
            {
                DataSet ds = cDados.getURLTelaInicialUsuario(cDados.getInfoSistema("CodigoEntidade").ToString(), idUsuarioLogado.ToString());

                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    urlDestino = ds.Tables[0].Rows[0]["TelaInicial"].ToString();
                    nomeTela = dsTelaNomePadrao.Tables[0].Rows[0]["NomeObjetoMenu"].ToString();
                }
            }
            string xml = string.Format(@"<caminho>
	                                            <N>
		                                            <id>0</id>
                                                    <nivel>0</nivel>
		                                            <url>{0}</url>
		                                            <nome>{1}</nome>
		                                            <parametros></parametros>
	                                            </N>
                                            </caminho>", urlDestino.Replace("~/", ""), nomeTela);

            Session["NomeArquivoNavegacao"] = Request.PhysicalApplicationPath + nomeArquivo;

            cDados.escreveXML(xml.Replace("&", "&amp;"), nomeArquivo);
        }

        Response.Redirect("~/selecionaOpcao.aspx?" + Request.QueryString);
    }

    private int getAlturaPrincipal()
    {
        string resolucaoTela = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(resolucaoTela.Substring(resolucaoTela.IndexOf('x') + 1));
        return alturaTela - 60;
    }
}
