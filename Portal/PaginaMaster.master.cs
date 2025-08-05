using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;

public partial class PaginaMaster : System.Web.UI.MasterPage
{
    dados cDados;

    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    public string texet = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

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
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        //callbackSession.JSProperties["cp_Usuario"] = cDados.getInfoSistema("NomeUsuarioLogado").ToString();
        //callbackSession.JSProperties["cp_IDUsuario"] = cDados.getInfoSistema("IDUsuarioLogado").ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        montaBoasVindas();

        DataSet dsMenu = cDados.getMenuAcessoUsuario(codigoEntidadeLogada, codigoUsuarioLogado, "");

        carregaMenuPrincipal(dsMenu.Tables[0]);

        splitterMaster.Panes[0].Collapsed = cDados.getInfoSistema("IndicaMenuFechado") != null && cDados.getInfoSistema("IndicaMenuFechado").ToString() == "S";
    }

    private void montaBoasVindas()
    {
        string primeiroNome = cDados.getInfoSistema("NomeUsuarioLogado").ToString();
        string siglaUnidade = " ";

        if (cDados.getInfoSistema("NomeUsuarioLogado").ToString().IndexOf(" ") != -1)
            primeiroNome = cDados.getInfoSistema("NomeUsuarioLogado").ToString().Substring(0, cDados.getInfoSistema("NomeUsuarioLogado").ToString().IndexOf(" "));

        //// Para a tela de seleçao de entidades, não pode mostrar a sigla, pois o usuário ainda não definiu com qual entidade deseja trabalhar
        if (cDados.getInfoSistema("Opcao").ToString() != "se")
        {
            // busca a(s) entidade(s) do usuário logado
            DataSet dsAux = cDados.getEntidadesUsuario(codigoUsuarioLogado, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuarioLogado.ToString());
            int QtdeEntidades = dsAux.Tables[0].Rows.Count;

            if (QtdeEntidades > 1) // o usuário tiver acesso a mais de uma entidade
                siglaUnidade = string.Format(@" (<a href='#' style='cursor:pointer;FONT-SIZE: 7pt; FONT-FAMILY: Verdana;' title='Alterar Entidade' id='linkEntidades' onclick='showModal(""{1}"", ""Alterar Entidade"", 620, 400, """", null);'>{0}</a>)"
                    , cDados.getInfoSistema("SiglaUnidadeNegocio").ToString()
                    , cDados.getPathSistema() + "popUpSelecaoEntidade.aspx");
        }

        lblBemVindo.Text = "Olá, " + primeiroNome + siglaUnidade + "&nbsp;";
    }

    private void carregaMenuPrincipal(DataTable dtMenu)
    {
        DataRow[] drMenu = dtMenu.Select("NivelObjetoMenu = 1", "OrdemObjeto");

        menuPrincipalMasterPage.Items.Clear();
        
        foreach (DataRow dr in drMenu)
        {
            DevExpress.Web.MenuItem item = new DevExpress.Web.MenuItem();
            item.Text = dr["NomeMenu"].ToString();
            item.ItemStyle.Font.Bold = true;
            item.ItemStyle.Font.Name = "Verdana";
            item.ItemStyle.Font.Size = new FontUnit("9pt");
            item.NavigateUrl = dr["URLObjetoMenu"].ToString();
            item.SubMenuItemStyle.Font.Bold = true;
            if (dr["CorObjetoMenu"].ToString() != "")
                item.ItemStyle.ForeColor = Color.FromName(dr["CorObjetoMenu"].ToString());

            item.Name = dr["Iniciais"].ToString();

            menuPrincipalMasterPage.Items.Add(item);//DataSourceID="ASPxSiteMapDataSource1" 
        }

        for (int i = 0; i < drMenu.Length; i++)
        {
            string nomeArquivo = @"/ArquivosTemporarios/Menu_" + i + "_" + codigoUsuarioLogado + "_" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".sitemap";
            string xmlInterno = constroiItemMenu(drMenu[i]["CodigoObjetoMenu"].ToString(), dtMenu);

            string xml = string.Format(@"
<siteMap xmlns=""http://schemas.microsoft.com/AspNet/SiteMap-File-1.0"" >
	<siteMapNode>
        <siteMapNode url="""" title=""{1}"" >
		    {0}
        </siteMapNode>
	</siteMapNode>
</siteMap>", xmlInterno, drMenu[i]["NomeMenu"].ToString());


            if (xmlInterno == "" && drMenu[i]["URLObjetoMenu"].ToString() == "")
            {
                menuPrincipalMasterPage.Items.FindByName(drMenu[i]["Iniciais"].ToString()).Visible = false;               
            }
            else
            {
                ASPxSiteMapControl sm = ((ASPxSiteMapControl)menuPrincipalMasterPage.Items[i].FindControl("smMenu"));

                if (xmlInterno != "")
                {
                    ASPxSiteMapDataSource dataSource = new ASPxSiteMapDataSource();
                    cDados.escreveXML(xml, nomeArquivo);
                    dataSource.SiteMapFileName = "~/" + nomeArquivo;
                    sm.DataSource = dataSource;
                    sm.DataBind();
                }
            }
        }
    }

    private string constroiItemMenu(string codigoObjetoPai, DataTable dtMenu)
    {
        string xml = "";

        foreach (DataRow dr in dtMenu.Select("CodigoObjetoMenuPai = " + codigoObjetoPai, "OrdemObjeto"))
        {
            string texto = dr["NomeMenu"].ToString();
            string url = dr["URLObjetoMenu"].ToString();
            string codigoObjetoMenu = dr["CodigoObjetoMenu"].ToString();
            string xmlAux = "";

            if(url != "")
                url += (url.Contains("?") ? "&" : "?") + "TITULO=" + dr["NomeMenu"].ToString();

            url = url.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;");

            xmlAux = constroiItemMenu(codigoObjetoMenu, dtMenu);

            if (xmlAux == "")
            {
                xml += string.Format(@"
            <siteMapNode url=""{1}"" title=""{0}"" />", texto, url);
            }
            else
            {
                xml += string.Format(@"
            <siteMapNode url=""{1}"" title=""{0}"" >", texto, url);
                xml += xmlAux;
                xml += @"
            </siteMapNode>";
            }
        }
        
        return xml;
    }


    protected void callbackResolucao_Callback(object source, CallbackEventArgs e)
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        string novaLargura = e.Parameter.Split(';')[0];
        string indicaMenuFechado = e.Parameter.Split(';')[1];


        cDados.setInfoSistema("ResolucaoCliente", largura + "x" + (int.Parse(novaLargura) + 60));

        cDados.setInfoSistema("IndicaMenuFechado", indicaMenuFechado);
        
    }
    protected void callbackPanel_Callback(object sender, CallbackEventArgsBase e)
    {
        
    }
}
