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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack && !IsCallback)
            carregaDados();

        cDados.setaTamanhoMaximoMemo(txtComentariosAprovador, 2000, lblContadorComentarioAprovador);
    }

    private void carregaDados()
    {
        string where = " AND CodigoAtribuicao = " + codigoAtribuicao;

        DataSet ds = cDados.getTarefasAprovacao(idUsuarioLogado, codigoEntidade, where);
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtProjeto.Text = dr["NomeProjeto"].ToString();
            txtTarefaSuperior.Text = dr["TarefaSuperior"].ToString();
            txtTarefa.Text = dr["NomeTarefa"].ToString();
            txtRecurso.Text = dr["NomeRecurso"].ToString();
            txtInicioPrevisto.Text = string.Format("{0:dd/MM/yyyy}", dr["InicioLB"]);
            txtTerminoPrevisto.Text = string.Format("{0:dd/MM/yyyy}", dr["TerminoLB"]);
            txtInicioReal.Text = string.Format("{0:dd/MM/yyyy}", dr["InicioReal"]);
            txtTerminoReal.Text = string.Format("{0:dd/MM/yyyy}", dr["TerminoReal"]);
            txtTrabalhoPrevisto.Value = dr["TrabalhoLB"];
            txtTrabalhoReal.Value = dr["TrabalhoRealTotal"];
            txtTrabalhoRestante.Value = dr["TrabalhoRestante"];
            txtPercentualConcluido.Value = dr["PercConcluido"];
            txtComentariosAprovador.Text = dr["ComentariosAprovador"].ToString();
            txtComentariosRecurso.Text = dr["UltimoComentario"].ToString();
        }
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_Status"] = "0";
        string msgErro = "";
        bool retorno = cDados.atualizaComentariosAtribuicao(codigoAtribuicao, txtComentariosAprovador.Text, ref msgErro);
        
        if (retorno)
        {
            callBack.JSProperties["cp_OK"] = "Comentário Gravado com Sucesso!";
            callBack.JSProperties["cp_Status"] = "1";
        }
        else
            callBack.JSProperties["cp_ERRO"] = "Erro ao Gravar o Comentário!";
    }
}
