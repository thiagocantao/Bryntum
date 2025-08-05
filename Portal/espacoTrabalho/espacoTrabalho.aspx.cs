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
using System.Collections.Generic;
public partial class espacoTrabalho_espacoTrabalho : System.Web.UI.Page
{
    //public string parametrosURL;
    public string alturaTabela;
    dados cDados;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //parametrosURL = Request.QueryString.ToString();
        alturaTabela = getAlturaTela() + "px";
        cDados.aplicaEstiloVisual(Page);

        //aplicaEstiloVisual(this.Page, cDados.getInfoSistema("IDEstiloVisual").ToString());
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 130).ToString();
    }

    public int aplicaEstiloVisual(Page page, string estilo)
    {
        List<Control> controles = cDados.getControles(page);
        System.Collections.Generic.List<object> objControlesAspX = new System.Collections.Generic.List<object>();
        foreach (Control controle in controles)
        {
            if (controle is DevExpress.Web.ASPxWebControl)
                objControlesAspX.Add(controle);
        }
        //cDados.setVisual(estilo, false, objControlesAspX.ToArray());
        return 1;
    }


}
