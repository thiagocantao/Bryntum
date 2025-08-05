using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bootstrap_Default : System.Web.UI.Page
{
    dados cDados;
    DataSet ds;

    private int codigoEntidade;
    private int idUsuarioLogado;

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Grid();
        Tema();
    }

    protected void Grid()
    {
        ds = cDados.getUnidadesNegocioTela(codigoEntidade, idUsuarioLogado, " AND un.CodigoUnidadeNegocio != un.CodigoEntidade ");
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
        gvDados.DataBind();
    }

    protected void Tema()
    {
        gvDados.CssFilePath = "~/App_Themes/MaterialCompact/{0}/styles.css";
        gvDados.CssPostfix = "MaterialCompact";
    }
}