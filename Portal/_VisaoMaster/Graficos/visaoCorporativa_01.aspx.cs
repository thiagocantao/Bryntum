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

public partial class _VisaoMaster_Graficos_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;

    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", alturaGrafico = "", larguraGrafico = "", alturaNumeros = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", grafico4 = "";

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

        defineLarguraTela();

        string codigoArea = "-1", nomeArea = "";

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "" && Request.QueryString["CA"].ToString().ToUpper().Trim() != "NULL")
            codigoArea = Request.QueryString["CA"].ToString();

        if (Request.QueryString["NA"] != null && Request.QueryString["NA"].ToString() != "" && Request.QueryString["NA"].ToString().ToUpper().Trim() != "NULL")
            nomeArea = Request.QueryString["NA"].ToString();

        grafico1 = "vm_003.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&NA=" + nomeArea;
        grafico2 = "vm_004.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&NA=" + nomeArea;
        grafico3 = "vm_005.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&NA=" + nomeArea;
        grafico4 = "vm_006.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&NA=" + nomeArea;

        //DataSet dsParametro = cDados.getParametrosSistema("an_grafico001", "an_grafico002", "an_grafico003", "an_grafico004", "an_grafico005", "an_grafico006");

        //if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        //{
        //    string extensao = ".aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico;

        //    grafico1 = dsParametro.Tables[0].Rows[0]["an_grafico001"] + "" == "" ? grafico1 : dsParametro.Tables[0].Rows[0]["an_grafico001"] + extensao;
        //    grafico2 = dsParametro.Tables[0].Rows[0]["an_grafico002"] + "" == "" ? grafico2 : dsParametro.Tables[0].Rows[0]["an_grafico002"] + extensao;
        //    grafico3 = dsParametro.Tables[0].Rows[0]["an_grafico003"] + "" == "" ? grafico3 : dsParametro.Tables[0].Rows[0]["an_grafico003"] + extensao;
        //    grafico4 = dsParametro.Tables[0].Rows[0]["an_grafico004"] + "" == "" ? grafico4 : dsParametro.Tables[0].Rows[0]["an_grafico004"] + extensao;
        //    grafico5 = dsParametro.Tables[0].Rows[0]["an_grafico005"] + "" == "" ? grafico5 : dsParametro.Tables[0].Rows[0]["an_grafico005"] + extensao;
        //    grafico6 = dsParametro.Tables[0].Rows[0]["an_grafico006"] + "" == "" ? grafico6 : dsParametro.Tables[0].Rows[0]["an_grafico006"] + extensao;
        //}

        defineAjuda();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 410) / 2).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 159).ToString() + "px";
        metadeAlturaTela = ((altura - 160) / 2).ToString() + "px";
               
        alturaGrafico = ((altura - 160) / 2).ToString();
        larguraGrafico = ((largura - 425) / 2).ToString();

        alturaNumeros = (altura - 160).ToString() + "px";
    }

    private void defineAjuda()
    {
        string textoAjuda = @"<p class=""ecxmsolistparagraph"" 
            style=""margin: 0cm; margin-bottom: .0001pt; text-indent: 0pt; background: white"">
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Desempenho Físico</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Apresentar o desempenho 
            físico da UHE Belo Monte, referente à Área selecionada (Civil / Fornecimento e 
            Montagem). O velocímetro sinaliza o desempenho em 3 níveis (verde, amarelo e 
            vermelho). O ponteiro indica o realizado e a linha pontilhada o previsto. <o:p></o:p>
            </span>
        </p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Curva S do empreendimento.<o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Superintendência de Planejamento 
            do Empreendimento / DC.<o:p></o:p></span></p>
        <p>
            <o:p>&nbsp;</o:p></p>
        <p>
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Desempenho Econômico</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Apresentar o desempenho 
            econômico da UHE Belo Monte, referente à Área selecionada (Civil / Fornecimento 
            e Montagem). O velocímetro sinaliza o desempenho em 3 níveis (verde, amarelo e 
            vermelho). O ponteiro indica o realizado e a linha pontilhada o previsto. <o:p></o:p>
            </span>
        </p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> -<o:p></o:p></span></p>
        <p>
            <span style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">
            Valores previstos: Orçamento NE;<o:p></o:p></span></p>
        <p>
            <span style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">
            Valores realizados: SAP.<o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Gerência de Informações 
            Gerenciais / DF.<o:p></o:p></span></p>
        <p>
            <span style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p>
            &nbsp;</o:p></span></p>
        <p>
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Mão de Obra</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Comparar o Previsto e o 
            Realizado, mês a mês, da mão-de-obra alocada na UHE Belo Monte, referente à Área 
            selecionada (Civil / Fornecimento e Montagem). <o:p></o:p></span>
        </p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Portal EPBM.<o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - EPBM.<o:p></o:p></span></p>
        <p>
            <o:p>&nbsp;</o:p></p>
        </p>
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Equipamentos</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Comparar o Previsto e o 
            Realizado, mês a mês, dos equipamentos alocados na UHE Belo Monte, referente à 
            Área selecionada (Civil / Fornecimento e Montagem).<o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Portal EPBM.<o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - EPBM.<o:p></o:p></span></p>";

        hfAjuda.Set("TextoAjuda", textoAjuda);
    }
}
