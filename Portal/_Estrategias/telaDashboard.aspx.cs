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

public partial class _Estrategias_telaDashboard : System.Web.UI.Page
{
    dados cDados;
    string perspectivaAtual = "";
    int codigoEntidade = 0;
    private int codigoUsuarioLogado = 0;

    public string funcaoClick = "return;", tipoCursor = "default";
    
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
        ASPxRoundPanel1.Style.Add("cursor", "pointer");
        gvDados.Style.Add("cursor", "pointer");

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

        cDados.aplicaEstiloVisual(this);

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        
        defineLarguraTela();

        carregaGrid();

        if (cDados.getInfoSistema("CodigoMapa") == null)
            cDados.setInfoSistema("CodigoMapa", "-1");

        bool permissao = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, "ME_Vsl");

        if (permissao)
        {
            funcaoClick = "window.top.gotoURL('_Estrategias/mapaEstrategico.aspx', '_top');";
            tipoCursor = "pointer";
        }
    }

    private void defineLarguraTela()
    { 
        int largura, altura;

        if (Request.QueryString["Altura"] != null && Request.QueryString["Largura"] != null)
        {
            largura = int.Parse(Request.QueryString["Largura"].ToString());
            altura = int.Parse(Request.QueryString["Altura"].ToString());

            ASPxRoundPanel1.Width = (largura - 10);
            ASPxRoundPanel1.ContentHeight = (altura - 38);

            gvDados.Width = largura - 15;
            gvDados.Settings.VerticalScrollableHeight = altura - 69;
        }
        else
        {
            string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
            largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
            altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

            ASPxRoundPanel1.Width = ((largura - 200) / 3 - 60);
            ASPxRoundPanel1.ContentHeight = (altura - 195) / 2 - 20;

            gvDados.Width = ((largura - 200) / 3 - 80);

            gvDados.Settings.VerticalScrollableHeight = altura - 69;
        }        
    }

    private void carregaGrid()
    {
        int mesesAnteriores = -1;

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "mesesAnterioresAtual");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["mesesAnterioresAtual"] + "" != "")
            mesesAnteriores = int.Parse(dsParametros.Tables[0].Rows[0]["mesesAnterioresAtual"].ToString()) * -1;
        
        DateTime dataMapa = DateTime.Now.AddMonths(mesesAnteriores);

        DataSet ds = cDados.getPerspectivasTemas(int.Parse(cDados.getInfoSistema("CodigoMapa").ToString()), dataMapa.Month, dataMapa.Year, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }
    protected void gvDados_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex != -1)
        {
            perspectivaAtual = gvDados.GetRowValues(e.VisibleIndex, "Perspectiva").ToString();

            if (e.VisibleIndex != 0 && perspectivaAtual == gvDados.GetRowValues(e.VisibleIndex - 1, "Perspectiva").ToString())
            {
                e.Row.Cells[0].Text = "&nbsp;";
            }

            if (gvDados.VisibleRowCount > e.VisibleIndex + 1)
            {
                if (gvDados.GetRowValues(e.VisibleIndex + 1, "Perspectiva").ToString() == gvDados.GetRowValues(e.VisibleIndex, "Perspectiva").ToString())
                {
                    e.Row.Cells[0].Style.Add("border-bottom", "none");
                }
            }

            //e.Row.Cells[1].Text = "<table><tr><td style='width: 20;' align='center'><img src='../imagens/" + gvDados.GetRowValues(e.VisibleIndex, "CorTema").ToString() + ".gif' /></td><td>" + gvDados.GetRowValues(e.VisibleIndex, "Tema").ToString() + "</td></tr></table>";
        }
    }
}
