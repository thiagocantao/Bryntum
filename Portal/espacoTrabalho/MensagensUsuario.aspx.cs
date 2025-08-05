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

public partial class _VisaoExecutiva_MensagensExecutivo : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoMensagem = -1;
    private int codigoObjetoAssociado = -1;
    private string tipoAssociacao = "";

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMensagem = int.Parse(Request.QueryString["CM"].ToString());

        if (Request.QueryString["COA"] != null && Request.QueryString["COA"].ToString() != "")
            codigoObjetoAssociado = int.Parse(Request.QueryString["COA"].ToString());

        if (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "")
            tipoAssociacao = Request.QueryString["TA"].ToString();

        carregaGrid();
    }

    private void carregaGrid()
    {
        string where = "";

        if (codigoMensagem != -1)
            where = " AND m.CodigoMensagem = " + codigoMensagem;

        if (codigoObjetoAssociado != -1 && tipoAssociacao != "")
        {
            int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(tipoAssociacao);
            where += " AND m.CodigoObjetoAssociado = " + codigoObjetoAssociado + " AND m.CodigoTipoAssociacao = " + codigoTipoAssociacao;
        }

        if (Request.QueryString["RespostaNecessaria"] != null && Request.QueryString["RespostaNecessaria"].ToString() == "S")
            where += " AND m.IndicaRespostaNecessaria = 'S'";

        DataSet ds = cDados.getMensagensNovasExecutivo(codigoEntidade, idUsuarioLogado, where);

        if (cDados.DataSetOk(ds))
        {
            gvMensagens.DataSource = ds;
            gvMensagens.DataBind();
        }
    }

    protected void gvMensagens_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Name.ToString() == "colMensagem" && e.CellValue.ToString().Length > 100)
            e.Cell.Text = e.CellValue.ToString().Substring(0, 100) + "...";

    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callback.JSProperties["cp_MSG"] = "";
        callback.JSProperties["cp_Erro"] = "";
        if (gvMensagens.FocusedRowIndex != -1)
        {
            int codigoMensagem = int.Parse(gvMensagens.GetRowValues(gvMensagens.FocusedRowIndex, "CodigoMensagem").ToString());

            if (e.Parameter.ToString() == "L")
            {
                cDados.atualizaMensagemLida(codigoMensagem, codigoMensagem, idUsuarioLogado);
            }
            else if (e.Parameter.ToString() == "R")
            {
                bool resultado = cDados.atualizaRespostaMensagem(codigoMensagem, idUsuarioLogado, MailEditor.Html);

                if (resultado)
                    callback.JSProperties["cp_MSG"] = "Resposta Enviada com Sucesso!";
                else
                    callback.JSProperties["cp_Erro"] = "Erro ao Enviar a Resposta!";

                //if (gvMensagens.VisibleRowCount > 0)
                //    gvMensagens.FocusedRowIndex = 0;
            }
        }
    }

    protected void gvMensagens_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string respostaNecessaria = gvMensagens.GetRowValues(e.VisibleIndex, "RespostaNecessaria").ToString();

        if (e.ButtonID == "btnResponder" && respostaNecessaria.ToLower() != "sim")
        {
            e.Enabled = false;
            e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            e.Text = "";
        }
    }

    protected void MailEditorPopup_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
    {

    }
}
