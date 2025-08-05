using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_DetalhesItemSprint : System.Web.UI.Page
{
    dados cDados;
    private int codigoItem;

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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


        if (Request.QueryString["CI"] != null)
            codigoItem = int.Parse(Request.QueryString["CI"].ToString());
        
        carregaCampos();

        cDados.aplicaEstiloVisual(Page);
    }

    private void carregaCampos()
    {
        string comandoSQL = string.Format(@"
            SELECT ib.TituloItem,
                   ib.DetalheItem,
                   ib.Importancia,
                   ib.EsforcoPrevisto,
                   tci.DescricaoTipoClassificacaoItem
              FROM Agil_ItemBacklog AS ib INNER JOIN
                   Agil_TipoClassificacaoItemBacklog AS tci ON (tci.CodigoTipoClassificacaoItem = ib.CodigoTipoClassificacaoItem)                                    
             WHERE ib.CodigoItem = {0}", codigoItem);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtTituloItem.Text = dr["TituloItem"].ToString();
            txtDescricaoTipoClassificacaoItem.Text = dr["DescricaoTipoClassificacaoItem"].ToString();
            txtImportancia.Value = dr["Importancia"];
            txtEsforcoPrevisto.Value = dr["EsforcoPrevisto"];
            mmDescricaoItem.Text = dr["DetalheItem"].ToString();
        }

        tabControl.JSProperties["cp_Anexo"] = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=IB&ID=" + codigoItem + "&RO=S&TO=Consultar&ALT=254";
    }
}