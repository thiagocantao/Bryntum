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

public partial class grafico_030 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;

    public int alturaGrafico = 220;
    public string conteudoPainelEntregas = "";
    public string conteudoPainelDesemp = "";

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        defineTamanhoObjetos();
        conteudoPainelEntregas = string.Format(@"
        <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%; height:{1}px; vertical-align: top; text-align: left;"">
            <tr>
                <td style = ""width:5%;""></td>
                <td style = ""width:90%;"">{0}</td>
                <td style = ""width:5%;""></td>
            </tr>
        </table>"
        , getDadosTabela1()
        , alturaGrafico);

        conteudoPainelDesemp = string.Format(@"
        <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%; height:{1}px; "">
            <tr>
                <td style = ""width:5%;""></td>
                <td align=""center"" style = ""width:90%;"">{0}</td>
                <td style = ""width:5%;""></td>
            </tr>
        </table>"
        , getDadosTabela2()
        , alturaGrafico);
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        alturaGrafico = (altura - 225) / 2;
    }

    private string getDadosTabela1()
    {
        string linhaRetorno = "";
        string HTML = @"";
        DataSet dsMarcos = cDados.getMarcosTarefasCliente(codigoProjeto, "");

        if (cDados.DataSetOk(dsMarcos))
        {
            DataTable dt = dsMarcos.Tables[0];

            HTML += getLinhaMarco(dt, "Verde", "No Prazo");
            HTML += getLinhaMarco(dt, "Vermelho", "Atrasadas");
            HTML += getLinhaMarco(dt, "Amarelo", "Tendência de Atraso");
            HTML += getLinhaMarco(dt, "VerdeOK", "Concluídas");
            HTML += getLinhaMarco(dt, "VermelhoOK", "Concluídas com Atraso");
        }

        linhaRetorno += @"<table cellspacing=""0"">";
        linhaRetorno += HTML;
        linhaRetorno += "</table>";

        return linhaRetorno;
    }

    private string getDadosTabela2()
    {
        string linhaRetorno = "";
        string spi = "";
        string corHexa = "";
        string cor = "";

        getDadosSPI(ref cor, ref spi);
        cor = cor.TrimEnd();
        switch (cor.ToUpper())
        {
            case "VERDE":
                corHexa = "#003300";
                break;
            case "AMARELO":
                corHexa = "#FF6D00";
                break;
            case "VERMELHO":
                corHexa = "#CC0000";
                break;
            case "LARANJA":
                corHexa = "#FF9900";
                break;
            default:
                corHexa = "#000000";
                break;
        }

        linhaRetorno += string.Format(@"
        <table cellspacing=""0"">
            <tr>
                    <tdalign=""center"">
                        <img src=""../../../imagens/CEF/StatusProjetoSPI{0}.png"" />
                    </td>
            </tr>
            <tr>
                    <td align=""center"" style=""font-family: 'Verdana'; font-size: 14pt; color: {1};"" >
                      SPI: {2}
                    </td >
                </tr>", cor
                , corHexa
                , spi == "" ? "---" : spi);
        linhaRetorno += "</table>";

        return linhaRetorno;
    }

    private void getDadosSPI(ref string cor, ref string spi)
    {
        DataSet ds = cDados.getSPIProjeto(codigoProjeto);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            cor = ds.Tables[0].Rows[0]["CorGeral"].ToString();
            spi = string.Format("{0:n2}", ds.Tables[0].Rows[0]["SPI"]);

        }
    }

    private string getLinhaMarco(DataTable dt, string cor, string texto)
    {
        string linha = "";

        int qtd = dt.Select("Status = '" + cor + "'").Length;

        if (qtd > 0)
        {
            linha += string.Format(@"
                <tr>
                    <td>
                    <img src=""../../../imagens/{0}.gif"" />
                    </ td>
                    <td style = ""font-family: 'Verdana'; font-size: 9pt;font-weight:bold;"" >
                      {1} ({2:n0})
                    </td >
                </tr>", cor, texto, qtd);
        }

        return linha;
    }
}
