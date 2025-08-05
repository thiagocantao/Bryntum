using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class popUpSelecaoEntidade : System.Web.UI.Page
{
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string urlWSnewBriskBase;
    dados cDados;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
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
        urlWSnewBriskBase = Session["urlWSnewBriskBase"] + "";
        hfSignalR.Set("urlWSnewBriskBase", urlWSnewBriskBase);
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        hfSignalR.Set("codigoEntidadeLogada", codigoEntidadeLogada);
        criaListaEntidadesSelecao();
        cDados.aplicaEstiloVisual(this);

    }

    private void criaListaEntidadesSelecao()
    {
        /*Busca o nome das unidades que o usuário tem acesso*/
        DataSet ds = cDados.getEntidadesUsuario(codigoUsuarioLogado, " AND UsuarioUnidadeNegocio.CodigoUsuario = " + codigoUsuarioLogado);

        hfListaSigla.Clear();

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ListEditItem rbEntidade = new ListEditItem();            
            rbEntidade.Text = row["NomeUnidadeNegocio"].ToString();            
            rbEntidade.Value = row["CodigoUnidadeNegocio"].ToString();
          
            string siglaEntidade = row["SiglaUnidadeNegocio"].ToString();
            string codigoUnidadeNegocio = row["CodigoUnidadeNegocio"].ToString();

            hfListaSigla.Add(codigoUnidadeNegocio, siglaEntidade);

            if (int.Parse(codigoUnidadeNegocio) == codigoEntidadeLogada)
            {
                rbEntidade.Selected = true;
                hfSignalR.Set("codigoEntidadeSelecionada", codigoEntidadeLogada);
                var link = "inicio.aspx?SUN=" + siglaEntidade + "&CE=" + codigoUnidadeNegocio;
                hfSignalR.Set("linkRedirect", link);
            }

            rbEntidades.Items.Add(rbEntidade);            
        }
    }
}