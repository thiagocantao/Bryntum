using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HelpFuncionalidade : System.Web.UI.Page
{
    dados cDados;
    protected void Page_Init(object sender, EventArgs e)
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string codigoGlossario = Request.QueryString["CG"];
        string comandoSQL = string.Format(@"SELECT [CodigoGlossarioAjuda]
      ,[TituloGlossarioAjuda]
      ,[DetalhesGlossarioAjuda]
      ,[CodigoFuncionalidade]
      ,[CodigoModeloFormulario]
      ,[CodigoEntidade]
  FROM [GlossarioAjuda]
WHERE CodigoGlossarioAjuda = {0}", codigoGlossario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            htmlVisualizaHelp.Html = ds.Tables[0].Rows[0]["DetalhesGlossarioAjuda"].ToString();
        }
        else
        {
            DataSet dsParam = cDados.getParametrosSistema("htmlAjudaDefault");
            if(cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
            {
                htmlVisualizaHelp.Html = dsParam.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                htmlVisualizaHelp.Html = "Não foi definido um parametro em seu sistema para exibir aviso quando não existe topico de ajuda para determinada tela, contate o fabricante. Obrigado.";
            }
        }
        
    }
}