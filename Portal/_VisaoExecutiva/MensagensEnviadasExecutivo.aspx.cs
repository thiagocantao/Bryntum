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

public partial class _VisaoExecutiva_MensagensEnviadasExecutivo : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private int codigoEntidade;
    string tipoMensagemApresentada = "A";
    string statusMSG = "";

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

        if (Request.QueryString["TipoMensagem"] != null && Request.QueryString["TipoMensagem"].ToString() != "A")
        {
            tipoMensagemApresentada = Request.QueryString["TipoMensagem"].ToString();
        }

        if (Request.QueryString["Status"] != null)
        {
            statusMSG = Request.QueryString["Status"].ToString();
        }

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(Page);

        carregaGrid();
    }

    private void carregaGrid()
    {
        DataSet ds;
        string where = "";

        if (statusMSG == "A")
            where = " AND m.DataLimiteResposta < GetDate()";

        if(tipoMensagemApresentada == "R")
            ds = cDados.getMensagensRespondidasExecutivo(codigoEntidade, idUsuarioLogado, where);
        else
            ds = cDados.getMensagensAbertasExecutivo(codigoEntidade, idUsuarioLogado, where);


        if (cDados.DataSetOk(ds))
        {
            gvMensagens.DataSource = ds;
            gvMensagens.DataBind();
        }
    }

    protected void gvMensagens_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if ((e.DataColumn.Name.ToString() == "colMensagem" || e.DataColumn.Name.ToString() == "colResposta") && e.CellValue.ToString().Length > 70)
            e.Cell.Text = e.CellValue.ToString().Substring(0, 70) + "...";

    }    
}
