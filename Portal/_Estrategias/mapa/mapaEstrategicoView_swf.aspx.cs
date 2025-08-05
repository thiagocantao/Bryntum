using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class _Estrategias_mapaEstrategicoView_swf : System.Web.UI.Page
{
    public int codigoMapa;
    public int codigoEntidade;
    public int codigoUsuario;
    public string resolucaoCliente;
    public string alturaObject;
    public string largoObject;
    public string webServicePath = "";
    public string alturaDivMapa = "";
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);



    }

    protected void Page_Load(object sender, EventArgs e)
    {
        codigoMapa = -1;
        bool retorno = int.TryParse(Request.QueryString["cm"] + "", out codigoMapa);


        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        webServicePath = getWebServicePath();
        defineAlturaTela(resolucaoCliente);


    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        /*
                    <!-- TD FLASH VIEW MAPA ESTRATÉGICO -->
            <td id="tdMapaFlashView" align="center" valign="top">
                <div style="height: <%= alturaDivMapa %>px; overflow: auto;">
                    <div id="divObjet">
                        <object enableviewstate="false" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
                            style="border: 0px;" id="WBSComponent" width="100%" height="825px" codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab">
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="wmode" value="transparent" />
                            <param name="movie" value="../flashs/mapaEstrategicoView.swf?caminhoWs=<%=webServicePath%>&codMapa=<%=codigoMapa %>&codEntidade=<%=codigoEntidade %>&codUsuario=<%=codigoUsuario %>" />
                            <param name="quality" value="high" />
                            <param name="bgcolor" value="#ffffff" />
                            <embed src="../flashs/mapaEstrategicoView.swf?caminhoWs=<%=webServicePath%>&codMapa=<%=codigoMapa %>&codEntidade=<%=codigoEntidade %>&codUsuario=<%=codigoUsuario %>"
                                wmode="transparent" quality="high" bgcolor="#ffffff" width="<%= largoObject %>"
                                height="<%= alturaObject %>" name="mapaEstrategicoView" align="top" play="true"
                                loop="false" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer">
                            </embed>
                        </object>
                    </div>
                </div>
            </td>
            <!-- FIM TD FLASH VIEW MAPA ESTRATÉGICO -->
        */


        // Calcula a altura da tela
        int altura;
        int largura;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaDivMapa = (altura) + "px";
        alturaObject = altura + "px";
        largoObject = altura + 100 + "px";



        //style=""border:0px; width:auto; height:auto""; float:left
        //width=""100%"" height=""825px""
        StringBuilder trechoHTML = new StringBuilder();

        trechoHTML.AppendLine(@"<div runat=""server"" id=""divObjet"">");
        trechoHTML.AppendLine(@"<object enableviewstate=""false"" classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000""");
        trechoHTML.AppendLine(@"style=""border:0px;"" id=""WBSComponent"" codebase=""http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab"">");

        //PARAMETROS DO MAPA
        trechoHTML.AppendLine(@"<param name=""allowScriptAccess"" value=""sameDomain""/>");
        trechoHTML.AppendLine(@"<param name=""wmode"" value=""transparent""/>");
        trechoHTML.AppendFormat(@"<param name= ""movie"" value = ""../../flashs/mapaEstrategicoView.swf?caminhoWs={0}&codMapa={1}&codEntidade={2}&codUsuario={3}""/>" + Environment.NewLine, webServicePath, codigoMapa, codigoEntidade, codigoUsuario);
        trechoHTML.AppendLine(@"<param name=""quality"" value=""low""/>");
        trechoHTML.AppendLine(@"<param name=""bgcolor"" value=""#ffffff""/>");
        //FIM DOS PARAMETROS
        //90,3320 por cento da largura
        //99,609375 por cento da altura
        //largura = largura

        largura = (int)(largura + ((0.973320 - 1)) * largura);
        altura = (int)(altura + ((0.99609375 - 1)) * altura);
        //MAPA ENCAIXADO NO COMPONENTE
        trechoHTML.AppendFormat(@"<embed src= ""../../flashs/mapaEstrategicoView.swf?caminhoWs={0}&codMapa={1}&codEntidade={2}&codUsuario={3}""" + Environment.NewLine, webServicePath, codigoMapa, codigoEntidade, codigoUsuario);
        trechoHTML.AppendFormat(@"wmode=""transparent"" quality=""low"" bgcolor=""#ffffff"" width=""{0}""" + Environment.NewLine, largura + "px");
        trechoHTML.AppendFormat(@" name=""mapaEstrategicoView"" align=""center"" play=""false"" height=""{0}""" + Environment.NewLine, altura + "px");
        trechoHTML.AppendLine(@"loop=""false"" type=""application/x-shockwave-flash"" pluginspage=""http://www.adobe.com/go/getflashplayer"">");
        trechoHTML.AppendLine(@"</embed>");
        //FIM MAPA ENCAIXADO NO COMPONENTE
        // 
        //height=""{0}""
        trechoHTML.AppendLine(@"</object>");
        trechoHTML.AppendLine(@"</div>");

        myDiv.InnerHtml = trechoHTML.ToString();

        myDiv.Style.Add("text-align", "center");
        myDiv.Style.Add("min-width", "auto");
        myDiv.Style.Add("min-height", "auto");

        myDiv.Attributes.Add("valign", "top");

    }

    private string getWebServicePath()
    {
        return cDados.getPathSistema() + "wsPortal.asmx?WSDL";
    }



    
}