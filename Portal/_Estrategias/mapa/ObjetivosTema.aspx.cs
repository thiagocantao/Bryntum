using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;

public partial class _Estrategias_mapa_Macrometa : System.Web.UI.Page
{
    int codigoTema = 0, codigoFator = 0;
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        defineAlturaTela(resolucaoCliente);

        if (Request.QueryString["CodigoTema"] != null && Request.QueryString["CodigoTema"].ToString() != "")
            codigoTema = int.Parse(Request.QueryString["CodigoTema"].ToString());

        if (Request.QueryString["CF"] != null && Request.QueryString["CF"].ToString() != "")
            codigoFator = int.Parse(Request.QueryString["CF"].ToString());

        carregaObjetivosTema();

        //cDados.aplicaEstiloVisual(this);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 175);
    }

    private void carregaObjetivosTema()
    {
        DataSet ds = cDados.getObjetivosTemaPrioritario(codigoTema);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            nb.GroupHeaderStyle.BackColor = Color.FromName("#EBD8D8");
            nb.GroupHeaderStyle.Font.Bold = false;
            nb.Width = new Unit("98%");
            nb.Groups.Clear();
            //nb.AutoCollapse = true;

            nb.ClientSideEvents.ExpandedChanged = @"function(s, e) {
	                if(e.group.GetExpanded())
	                {
		                document.getElementById('frm_' + e.group.name).src = 'AcoesTransformadoras.aspx?CodigoObjetivo=' + e.group.name;
	                }
                }";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                NavBarGroup grupo;

                string tabelaDescricao = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" style='width:100%;'>
                                                           <tr>
                                                                <td><table cellpadding=""0"" cellspacing=""0"" style='width:100%;'>
                                                                        <tr><td style=""border-bottom:1px solid #EBEBEB;padding-bottom:2px"">Objetivo: {2}</td></tr>
                                                                        <tr><td style=""padding-top:2px"">Indicador: {3}</td></tr>
                                                                    </table>
                                                                </td>
                                                                <td style='width:25px'><img style=""cursor:pointer"" src='../../imagens/{0}.gif' alt='{1}' onclick='window.open(""./Micrometa.aspx?Cod={4}&INI=OB&CF={5}"", ""_top"")' /></td>
                                                           </tr>
                                                         </table>"
                    , dr["StatusCor"]
                    , dr["StatusDescricao"]
                    , dr["DescricaoObjetivo"]
                    , dr["NomeIndicador"]
                    , dr["CodigoObjetivo"]
                    , codigoFator);

                grupo = nb.Groups.Add(tabelaDescricao, dr["CodigoObjetivo"].ToString());
                grupo.Expanded = false;
                string textoItem = string.Format(@"<table style='width:100%;'><tr><td><iframe id=""frm_{0}"" frameborder=""0"" height=""340px"" scrolling=""auto"" src="""" width=""100%""></iframe></td></tr></table>"
                    , dr["CodigoObjetivo"].ToString());

                NavBarItem nbi = grupo.Items.Add(textoItem);
                nbi.ClientEnabled = false;
            }
        }
    }
}