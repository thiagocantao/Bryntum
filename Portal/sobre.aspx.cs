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
using System.IO;
using System.Web.Hosting;

public partial class sobre : System.Web.UI.Page
{
    dados cDados;
    protected void Page_Init(object sender, EventArgs e)
    {
        //Dev
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        cDados = CdadosUtil.GetCdados(null);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        spnCopyRight.InnerText = string.Format("Copyright © 2009-{0} CDIS Informática LTDA.", DateTime.Now.Year);
        
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

        lblNomeCliente.Text = "Licenciado para: CDIS Informática LTDA";

        DataSet ds = cDados.getParametrosSistema("nomeEmpresa");
       
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblNomeCliente.Text = string.Format("{0}: {1}", Resources.traducao.licenciado_para, ds.Tables[0].Rows[0]["nomeEmpresa"].ToString());
        }
        nomeProjeto.InnerText = cDados.getParametrosSistema("tituloPaginasWEB").Tables[0].Rows[0][0].ToString();
        Page.Title = nomeProjeto.InnerText;
        try
        {
            var version = Global.Version;
            var buildDate = version.GetVerionDate();
            lblAtualizacao.Text = string.Format("{1}: {0}", version, Resources.traducao.vers_o);
            lblAtualizacao.Text += "<br>" + Resources.traducao.vers_o_do_devexpress + ": " + typeof(DevExpress.Web.ASPxGridView).Assembly.GetName(true).Version.ToString();
            lblAtualizacao.ToolTip = buildDate.ToString("g");
        }
        catch
        {
            lblAtualizacao.Text += "Versão do DevExpress: " + typeof(DevExpress.Web.ASPxGridView).Assembly.GetName(true).Version.ToString();
        }

    }
}
