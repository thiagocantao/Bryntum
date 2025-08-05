using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Estrategias_mapa_DetalhesAnoMacrometa : System.Web.UI.Page
{
    int codigoFator = 0, ano = 0;
    dados cDados;
    private int codigoUsuarioResponsavel;
    private string iniciaisObjeto = "";
    public int nivelNavegacao = 2;
    
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
        if(Request.QueryString["CF"] != null && Request.QueryString["CF"].ToString() != "")
            codigoFator = int.Parse(Request.QueryString["CF"].ToString());

        if (Request.QueryString["Ano"] != null && Request.QueryString["Ano"].ToString() != "")
            ano = int.Parse(Request.QueryString["Ano"].ToString());

        if (Request.QueryString["INI"] != null && Request.QueryString["INI"].ToString() != "")
            iniciaisObjeto =  Request.QueryString["INI"].ToString();

        if (iniciaisObjeto != "PP")
            nivelNavegacao = 3;

        carregaDadosMacrometa();

        if (ano == DateTime.Now.Year)
            gvProjetos.ClientVisible = false;
        else
            carregaProjetosFatorChave();

        //cDados.aplicaEstiloVisual(this);
    }

    private void carregaDadosMacrometa()
    {
        DataSet ds = cDados.getDadosMacrometaAno(codigoFator, iniciaisObjeto, ano);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string tabelaPrincipal = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""style1"">");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                string tendencia = dr["Tendencias"].ToString().Replace(Environment.NewLine, "<BR>").Replace("\n", "{BR}");
                string agenda = dr["Agenda"].ToString().Replace(Environment.NewLine, "<BR>").Replace("\n", "{BR}");

                string tabelaStatus = "";

                if (ano != DateTime.Now.Year)
                    tabelaStatus = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" >
                                                    <tr>
                                                        <td style=""font-family: verdana; font-size: 9pt;"">Status da Meta: </td>
                                                        <td align=""center"" style=""width:20px"">
                                                         <img title=""{1}"" src=""../../imagens/{0}.gif"" />
                                                        </td>
                                                        <td style=""font-weight:bold;"">
                                                            {1}
                                                        </td>
                                             </tr>
                                            </table>", dr["StatusCor"]
                                                           , dr["StatusDescricao"]);

                string tabelaTendencias = string.Format(@"<table cellpadding=""0"" cellspacing=""5"" class=""style1"">
                                                            <tr>
                                                                <td>Tendências:</td>
                                                                <td>Agenda:</td>
                                                            </tr>
                                                            <tr>
                                                               <td valign=""top"" style=""overflow:hidden;padding:2px;border: 1px solid #999999; width:50%; height:220px;"">
                                                                 {0}
                                                               </td>
                                                                <td valign=""top"" style=""overflow:hidden; padding:2px;border: 1px solid #999999;  height:220px;"">
                                                                 {1}
                                                               </td>
                                                            </tr>
                                                           </table>", tendencia, agenda);


                tabelaPrincipal += string.Format(@"
                                                   <tr>
                                                     <td style=""padding-bottom:1px;padding-top:3px"">{0}</td>
                                                   </tr>
                                                   <tr>
                                                     <td style=""padding-bottom:15px"">{1}</td>
                                                   </tr>"
                    , tabelaTendencias
                    , tabelaStatus);
            }

            tabelaPrincipal += "</table>";
            spanAnalises.InnerHtml = tabelaPrincipal;
        }
    }
    
    private void carregaProjetosFatorChave()
    {
        DataSet ds = cDados.getProjetosFatorChave(codigoFator, iniciaisObjeto, codigoUsuarioResponsavel, ano);

        if (cDados.DataSetOk(ds))
        {
            gvProjetos.DataSource = ds;
            gvProjetos.DataBind();
        }
    }

    public string getDescricao()
    {
        string descricaoProjeto = Eval("NomeProjeto").ToString();

        if (Eval("PodeAcessarProjeto").ToString() == "S")
        {
            string linkProjeto = Eval("IndicaProjetoCarteiraPrioritaria").ToString() == "S" ? "indexResumoProjeto" : "cni_ResumoProjeto";

            descricaoProjeto = string.Format(@"<a class='LinkGrid' target='_top' href='../../_Projetos/DadosProjeto/" + linkProjeto + ".aspx?IDProjeto=" + Eval("CodigoProjeto") + "&NomeProjeto=" + Eval("NomeProjeto") + "&NivelNavegacao=" + nivelNavegacao + "'>" + Eval("NomeProjeto") + "</a>");
        }

        string table = string.Format(@"<table>
                                            <tr>                                                
                                                <td>{0}</td>
                                            </tr>
                                       </table>", descricaoProjeto);

        return table;
    }
}