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

public partial class administracao_frmAditivosContrato : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    int codigoItem = -1;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["CI"] != null)
            codigoItem = int.Parse(Request.QueryString["CI"].ToString());


        carregaGvDados();


        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }
          
    #region GRID

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoMovimentoItem, Comentario, tsib.TituloStatusItem, u.NomeUsuario, mi.DataMovimento, mi.EsforcoReal
          FROM Agil_MovimentoItem mi INNER JOIN
			   Agil_TipoStatusItemBacklog tsib ON tsib.CodigoTipoStatusItem = mi.CodigoNovoStatusItem LEFT JOIN
			   Usuario u ON u.CodigoUsuario = mi.CodigoUsuarioMovimento
         WHERE CodigoItem = {0}
         ORDER BY DataMovimento DESC", codigoItem);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }        
    }

    #endregion

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "Comentario")
        {
            if (e.CellValue.ToString().Length > 125)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 125) + "...";
            }
        }
    }
}
