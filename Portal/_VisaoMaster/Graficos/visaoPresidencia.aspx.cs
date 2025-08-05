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
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.Web;
using System.IO;
using System.Drawing.Imaging;

public partial class _VisaoMaster_Graficos_visaoPresidencia : System.Web.UI.Page
{
    dados cDados;

    public string larguraTela = "", alturaTela = "", larguraTabela = "", metadeAlturaTela = "", metadeAlturaTela1 = "", alturaGrafico = "", alturaGrafico1 = "", larguraGrafico = "", alturaNumeros = "", larguraGrafico1 = "";

    public string grafico0 = "", grafico1 = "", grafico2 = "", grafico3 = "", urlNumeros = "";
    int codigoEntidade, codigoCategoria;


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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoCategoria = int.Parse(Request.QueryString["CA"].ToString());

        defineLarguraTela();
        //getInfo_Tabela1();
        //getInfo_Tabela2();
        //carregaFotosObra();

        string codigoArea = "-1";
        string nomeArea = "";

        string corCusto = "", corEscopo = "", corTempo = "", corDesembolso = "";
                
        DataSet ds = cDados.getTabelaIndicadoresPainelPresidencia(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow linha3 = ds.Tables[0].Rows[0];
            corCusto = linha3["CorCusto"].ToString();
            corTempo = linha3["CorTempo"].ToString();
            corEscopo = linha3["CorEscopo"].ToString();
            corDesembolso = linha3["CorDesembolso"].ToString();
        }

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "" && Request.QueryString["CA"].ToString().ToUpper().Trim() != "NULL")
            codigoArea = Request.QueryString["CA"].ToString();

        if (Request.QueryString["NA"] != null && Request.QueryString["NA"].ToString() != "" && Request.QueryString["NA"].ToString().ToUpper().Trim() != "NULL")
            nomeArea = Request.QueryString["NA"].ToString();

        grafico0 = "vm_015.aspx?Altura=155&Largura=" + larguraGrafico1 + "&CR=UHE_Principal&CA=" + codigoArea + "&CorCusto=" + corCusto + "&CorTempo=" + corTempo + "&CorEscopo=" + corEscopo + "&CorDesembolso=" + corDesembolso + "&NA=" + nomeArea;
        grafico1 = "vm_018.aspx?Altura=" + alturaGrafico1 + "&Largura=" + larguraGrafico1 + "&CR=UHE_Principal&CA=" + codigoArea + "&CorCusto=" + corCusto + "&NA=" + nomeArea;
        grafico2 = "vm_016.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&CorDesembolso=" + corDesembolso + "&NA=" + nomeArea;
        grafico3 = "vm_017.aspx?Altura=" + alturaGrafico + "&Largura=" + larguraGrafico + "&CR=UHE_Principal&CA=" + codigoArea + "&CorEscopo=" + corEscopo + "&NA=" + nomeArea;
        urlNumeros = "numeros_004.aspx?CA=" + codigoArea + "&NA=" + nomeArea;

        //defineAjuda();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = ((largura - 300) / 2).ToString() + "px";
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)) - 20;

        alturaTela = (altura - 150).ToString() + "px";
        metadeAlturaTela = ((altura - 145) / 2 -75).ToString() + "px";
        metadeAlturaTela1 = ((altura - 145) / 2 - 80).ToString() + "px";

        alturaGrafico1 = ((altura - 177) / 2 - 80).ToString();

        alturaGrafico = ((altura - 160) / 2 -75).ToString();
        larguraGrafico = ((largura - 300) / 2).ToString();
        larguraGrafico1 = (largura - 300).ToString();

        alturaNumeros = (altura - 130).ToString() + "px";
    }

//    private void defineAjuda()
//    {
//        string textoAjuda = @"<p class=""ecxmsolistparagraph"">
//                        <u>
//                        <span style=""font-size:11.5pt;
//font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Pimental 1º Unidade e Belo Monte 1º Unidade
//                        </span></u>
//                        <span style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">
//                        (Marcos do empreendimento)<u><o:p></o:p></u></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Identificar a quantidade e a 
//                        criticidade das entregas e dos seus respectivos pontos de controle para a 
//                        geração da 1º turbina de Pimental e de Belo Monte. Ao clicar nas entregas ou nos 
//                        pontos de controle é possível ver o detalhamento. <o:p></o:p></span>
//                    </p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Cronograma de marcos gerenciais.<o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
//font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Gerência de Planejamento do Empreendimento.<o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p>&nbsp;</o:p></span></p>
//                    <p class=""style5"">
//                        <u>
//                        <span style=""font-size:11.5pt;
//font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Acompanhamento Econômico - Orçado x Realizado<o:p></o:p></span></u></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Comparar o Orçado e o Realizado 
//                        (agrupado por ano para anos anteriores e posteriores e mês a mês para ano 
//                        corrente). Essa comparação abrange o CAPEX e o OPEX.<o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Orçamento NE<o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
//font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Gerência de Informações Gerenciais<o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p>&nbsp;</o:p></span></p>
//                    <p class=""style5"">
//                        <span style=""font-size:7.0pt"">&nbsp;</span><u><span 
//                            style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Previsão 
//                        de Desembolso Financeiro</span></u><span 
//                            style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Verificar a acuracidade trimestral 
//                        da previsão de desembolso. <o:p></o:p></span>
//                    </p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Previsão de desembolso<o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
//font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Superintendência Financeira<o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p>&nbsp;</o:p></span></p>
//                    <p class=""style5"">
//                        <u>
//                        <span style=""font-size:11.5pt;
//font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Acompanhamento do Escopo</span></u><span 
//                            style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Acompanhar o Escopo com base no valor 
//                        total das contratações. Identificar o que foi contratado e o que falta a 
//                        contratar, apontando se o saldo disponível é suficiente para as contratações 
//                        pendentes.</span> <o:p></o:p>
//                    </p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
//11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Escopo de contratações da Diretoria de 
//                        Construção, Diretoria de Fornecimento e Montagem e Diretoria Socioambiental.<b 
//                            style=""mso-bidi-font-weight:normal""><o:p></o:p></b></span></p>
//                    <p class=""style5"">
//                        <b style=""mso-bidi-font-weight:normal"">
//                        <span style=""font-size:11.5pt;font-family:
//&quot;Calibri&quot;,&quot;sans-serif&quot;"">Área responsável</span></b><span style=""font-size:11.5pt;
//font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Diretoria de Construção, Diretoria de 
//                        Fornecimento e Montagem e Diretoria Socioambiental, conforme item de escopo.<o:p></o:p></span></p>";

//        hfAjuda.Set("TextoAjuda", textoAjuda);
//    }

//    private void getInfo_Tabela1()
//    {


//        DataSet dsTurbina = cDados.getDataGeracaoTurbina(1, codigoEntidade);

//        try
//        {
//            DateTime dataTurbina = (DateTime)dsTurbina.Tables[0].Rows[0][0];
//            TimeSpan numeroDias = dataTurbina.Subtract(DateTime.Now);

//            gauge.Value = numeroDias.Days.ToString();
//        }
//        catch
//        {
//            gauge.Value = null;
//        }

//        DataSet dsLO = cDados.getDataObtencaoLO(codigoEntidade);

//        try
//        {
//            DateTime dataLO = (DateTime)dsLO.Tables[0].Rows[0][0];
//            TimeSpan numeroDiasLO = dataLO.Subtract(DateTime.Now);

//            gauge2.Value = numeroDiasLO.Days.ToString();
//        }
//        catch
//        {
//            gauge2.Value = null;
//        }
//    }

//    private void getInfo_Tabela2()
//    {
//        DataSet dsParametro = cDados.getInformacoesPainelGerenciamento(codigoEntidade, codigoCategoria, "");

//        if (cDados.DataSetOk(dsParametro))
//        {
//            if (cDados.DataTableOk(dsParametro.Tables[0]))
//            {
//                DataRow dr = dsParametro.Tables[0].Rows[0];

//                imgFisicoUHE.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoUHE"]);
//                imgFinanceiroUHE.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroUHE"]);
//                lblDesempenhoUHE.Text = string.Format("{0:n2}%", dr["DesempenhoUHE"]);
//            }
//        }
//    }

//    private void carregaFotosObra()
//    {
//        DataSet dsDados = cDados.getFotosPainelGerenciamento("UHE_Principal", codigoEntidade, 9, "");

//        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
//        {
//            int index = 1;
//            foreach (DataRow dr in dsDados.Tables[0].Rows)
//            {
//                ASPxBinaryImage img = Page.FindControl("img00" + index) as ASPxBinaryImage;

//                img.Value = cDados.GetImageThumbnail(dr["Foto"], 95, 45);
//                img.ToolTip = dr["DescricaoFoto"].ToString();
//                img.Cursor = "Pointer";

//                string corpoFuncao = string.Format(@"
//                                                     window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/GaleriaFotos.aspx?NumeroFotos=999&CR=UHE_Principal&CF={0}', ""Últimas Fotos UHE BeloMonte"", 565, 490, """", null);
//                                                     ", dr["CodigoFoto"].ToString());

//                img.ClientSideEvents.Click = "function(s, e) {" + corpoFuncao + "}";
//                index++;
//            }
//        }
//    }   
}
