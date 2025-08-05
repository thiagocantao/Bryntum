using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class administracao_GaleriaFotos : System.Web.UI.Page
{
    dados cDados;
    
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

        int qtdFotos = int.Parse(Request.QueryString["NumeroFotos"].ToString());

        if (!IsPostBack)
        {
            cDados.setInfoSistema("DataSetFotos", null);

            DataSet dsDados = cDados.getFotosGaleria("PRJ", -1, qtdFotos, "");

            if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
            {
                DataRow dr = dsDados.Tables[0].Rows[0];
                imgZoom.ContentBytes = (byte[])dr["Anexo"];
                txtDescricaoFoto.Text = dr["DescricaoAnexo"].ToString();
            }

            pnFotos.JSProperties["cp_NumeroFotos"] = dsDados.Tables[0].Rows.Count - 1;

            cDados.setInfoSistema("DataSetFotos", dsDados);
        }
    }

    protected void pnFoto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != "" && cDados.getInfoSistema("DataSetFotos") != null)
        {
            int numeroFoto = int.Parse(e.Parameter.ToString());

            DataSet dsDados = (DataSet)cDados.getInfoSistema("DataSetFotos");

            if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
            {
                DataRow dr = dsDados.Tables[0].Rows[numeroFoto];
                imgZoom.ContentBytes = (byte[])dr["Anexo"];
                pnFotos.JSProperties["cp_DescricaoFotos"] = dr["DescricaoAnexo"].ToString();
            }

            pnFotos.JSProperties["cp_NumeroFotos"] = dsDados.Tables[0].Rows.Count - 1;
        }
    }
}