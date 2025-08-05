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

public partial class espacoTrabalho_ComentariosTarefa : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;
    int idUsuarioLogado;
    int codigoAtribuicao = 0;

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

        if (Request.QueryString["CA"] != null)
        {
            codigoAtribuicao = int.Parse(Request.QueryString["CA"].ToString());
        }

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        cDados.aplicaEstiloVisual(Page);
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        if (alturaPrincipal <= 800)
        {
            divAlturaPopup.Style.Add("height", "380px");
        }
        if ((alturaPrincipal > 800) && (alturaPrincipal <= 1366))
        {
            divAlturaPopup.Style.Add("height", "380px");
        }
        else
        {
            divAlturaPopup.Style.Add("height", "380px");
        }

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack && !IsCallback)
            carregaDados();

        cDados.setaTamanhoMaximoMemo(txtComentariosRecurso, 2000, lblContadorComentarioRecurso);
        cDados.setaTamanhoMaximoMemo(txtComentariosAprovador, 2000, lblContadorComentarioAprovador);

    }

    private void carregaDados()
    {
        DataSet ds = cDados.getComentariosAtribuicao(codigoAtribuicao, codigoEntidade, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtProjeto.Text = dr["Projeto"].ToString();
            txtTarefa.Text = dr["Tarefa"].ToString();
            txtRecurso.Text = dr["Recurso"].ToString();
            txtComentariosRecurso.Text = dr["ComentariosRecurso"].ToString();
            txtComentariosAprovador.Text = dr["ComentariosAprovador"].ToString();
        }
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";

        string msgErro = "";
        bool retorno = cDados.atualizaComentariosAtribuicao(codigoAtribuicao, txtComentariosAprovador.Text, ref msgErro);

        if (retorno)
            callBack.JSProperties["cp_OK"] = "Comentário Gravado com Sucesso!";
        else
            callBack.JSProperties["cp_Erro"] = "Erro ao Gravar o Comentário!";
    }
}
