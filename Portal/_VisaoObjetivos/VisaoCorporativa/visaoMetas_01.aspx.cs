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
using DevExpress.Web;

public partial class _VisaoObjetivos_VisaoCorporativa_visaoMetas_01 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade = 0, codigoUsuario = 0;

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

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);

        carregaItens();
    }

    private void carregaItens()
    {
        DataSet ds = cDados.getObjetivosVisaoCorporativa(codigoEntidade, codigoUsuario);

        nb01.Groups.Clear();

        int count = 0;

        if (cDados.DataSetOk(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string textoGrupo = string.Format("<table><tr><td style='width:30px;'><img src='../../imagens/{0}Menor.gif' /></td><td>{1}</td></tr></table>",
                     dr["DesempenhoAtual"].ToString().Trim(), dr["DescricaoObjetivo"].ToString());

                NavBarGroup nb;

                nb = nb01.Groups.Add(textoGrupo, dr["CodigoObjetivo"].ToString() + "_" + count);
                nb01.JSProperties["cp_CodigoObjetivo"] = dr["CodigoObjetivo"].ToString();
                nb01.JSProperties["cp_CodigoEntidade"] = codigoEntidade;


                nb.Expanded = true;

                string textoItem = string.Format(@"<table style='width:100%;'><tr><td><table style='width:100%;'><tr><td><iframe id=""frm2_{0}"" frameborder=""0"" height=""210px"" scrolling=""no"" src=""mt_001.aspx?CO={0}"" width=""100%""></iframe></td></tr></table></td></tr></table>", dr["CodigoObjetivo"].ToString());


                count++;

                NavBarItem nbi = nb.Items.Add(textoItem);
                nbi.Name = dr["CodigoObjetivo"].ToString();
                nbi.ClientEnabled = false;
            }
        }

        if (count == 0)
        {
            popUpStatusTela.ShowOnPageLoad = true;
        }
    }
}
