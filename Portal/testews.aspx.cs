using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testews : System.Web.UI.Page
{
    public string url;
    /*
    Mudanças no web.config para cross-domain

    <system.web>
        <webServices>
            <protocols>
                <add name="HttpPost"/>
            </protocols>
        </webServices>
     </system.web>


     <system.webServer>
         <httpProtocol>
            <customHeaders>
              <add name="Access-Control-Allow-Origin" value="*" />
              <add name="Access-Control-Allow-Methods" value="POST" />
              <add name="Access-Control-Allow-Headers" value="Origin, Content-Type, Accept" />
            </customHeaders>
          </httpProtocol>
       </system.webServer>
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        url = "http://www.cdis.inf.br/sicoob/wstasquesreg.asmx";
        url = Request.Url.AbsoluteUri.Replace("testews.aspx", "wstasquesreg.asmx");
        lblRetorno.Text = url;
    }

    protected void btnWSTasques_Click(object sender, EventArgs e)
    {
        try
        {
            WS_Tasques.wsTasquesreg ws = new WS_Tasques.wsTasquesreg();
            ws.Url = url;

            string teste1 = ws.testeComunicacao();
            lblWsTasques1.Text = string.Format("Resultado do Teste 1: {0} em {1}", teste1, url);

            DataSet teste2 = ws.testeComunicacaoLista();
            gvTeste.DataSource = teste2;
            gvTeste.DataBind();

        }
        catch (Exception ex)
        {
            lblRetorno.Text = ex.Message;
        }

    }

}