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

public partial class _VisaoMaster_Graficos_visaoCorporativa_08 : System.Web.UI.Page
{
    dados cDados;

    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", metadeAlturaTela1 = "", alturaGrafico = "", alturaGrafico1 = "", larguraGrafico = "", alturaNumeros = "", larguraGrafico1 = "";

    public string grafico1 = "", grafico2 = "", grafico3 = "", urlNumeros = "";

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

        grafico1 = "vm_011.aspx?Altura=" + alturaGrafico1 + "&Largura=" + larguraGrafico1 + "&CR=UHE_Principal&CA=" + codigoArea + "&NA=" + nomeArea;
        grafico2 = "vm_012.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&NA=" + nomeArea;
        grafico3 = "vm_014.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&NA=" + nomeArea;
        urlNumeros = "numeros_003.aspx?CA=" + codigoArea + "&NA=" + nomeArea;
        defineAjuda();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 280) / 2).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 150).ToString() + "px";
        metadeAlturaTela = ((altura - 145) / 2 + 40).ToString() + "px";
        metadeAlturaTela1 = ((altura - 145) / 2 - 40).ToString() + "px";

        alturaGrafico1 = ((altura - 177) / 2 - 40).ToString();

        alturaGrafico = ((altura - 180) / 2 + 38).ToString();
        larguraGrafico = ((largura - 280) / 2).ToString();
        larguraGrafico1 = (largura - 280).ToString();

        alturaNumeros = (altura - 145).ToString() + "px";
    }

    private void defineAjuda()
    {
        string textoAjuda = @"<p class=""ecxmsolistparagraph"">
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Acompanhamento Econômico - Orçado x 
            Realizado</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Comparar o Orçado e o 
            Realizado Reajustado do ano vigente. Essa comparação contempla apenas as 
            informações das áreas de Obra Civil e de Fornecimento e Montagem (CAPEX). 
            Compara também as curvas previstas e realizadas dos contratos (i0) das referidas 
            áreas.<o:p></o:p></span></p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Orçamento (Orçado), SAP 
            (Realizado Reajustado) e curvas previstas e realizadas dos contratos de Obra 
            Civil e de Fornecimento e Montagem (i0).<o:p></o:p></span></p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Valores Orçados e Realizado Reajustado: Gerência de Informações Gerenciais / DF;<o:p></o:p></span></p>
       <p class=""style5"">
            <span style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">
            Curvas Previstas e Realizadas dos contratos (i0): Diretoria de Construção e 
            Diretoria de Fornecimento e Montagem, conforme tipo de contrato.<o:p></o:p></span></p>
       <p class=""style5"">
            <o:p>&nbsp;</o:p></p>
        <p class=""style5"">
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Composição dos valores realizados 
            até janeiro/13</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Demostrar o percentual dos 
            valores realizados por Sítio, da Área selecionada (Civil / Fornecimento e 
            Montagem).<o:p></o:p></span></p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Medição dos contratos.<o:p></o:p></span></p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Gerência de Contratos<o:p></o:p></span></p>
       <p class=""style5"">
            <o:p>&nbsp;</o:p></p>
       <p class=""style5"">
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Avanço por sítio até janeiro/13</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Comparar o Previsto e o 
            Realizado do avanço físico por Sítio, da Área selecionada (Civil / Fornecimento 
            e Montagem). <o:p></o:p></span>
        </p>
        <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Valores previstos: Orçamento NE;<o:p></o:p></span></p>
        <p class=""style5"">
            <span style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">
            Valores realizados: SAP.<o:p></o:p></span></p>
       <p class=""style5"">
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Gerência de Informações 
            Gerenciais / DF.<o:p></o:p></span></p>";

        hfAjuda.Set("TextoAjuda", textoAjuda);
    }
}
