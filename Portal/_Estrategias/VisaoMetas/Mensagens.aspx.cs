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
    private int codigoIndicador = -1, codigoResponsavel = -1;

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

        if (Request.QueryString["CI"] != null && Request.QueryString["CI"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());

        if (Request.QueryString["CRESP"] != null && Request.QueryString["CRESP"].ToString() + "" != "")
        {
            codigoResponsavel = int.Parse(Request.QueryString["CRESP"].ToString());
        }

        carregaGrid();
    }

    private void carregaGrid()
    {
        string where = "";

        DataSet ds = cDados.getMensagensIndicador(codigoEntidade, codigoIndicador, where);

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
        callback.JSProperties["cp_status"] = "";

        if (e.Parameter.ToString() == "E")
        {
            if (gvMensagens.FocusedRowIndex != -1)
            {
                DateTime auxDt = DateTime.MinValue;

                auxDt = dtePrazo.Date;
                int codigoMensagem = int.Parse(gvMensagens.GetRowValues(gvMensagens.FocusedRowIndex, "CodigoMensagem").ToString());

                bool resultado = cDados.atualizaMensagem(codigoMensagem, auxDt, txtMensagem.Text);

                if (resultado)
                {
                    callback.JSProperties["cp_MSG"] = "Mensagem Alterada com Sucesso!";
                    callback.JSProperties["cp_status"] = "ok";
                }
                else
                {
                    callback.JSProperties["cp_MSG"] = "Erro ao Alterar a Mensagem!";
                    callback.JSProperties["cp_status"] = "erro";
                }
            }
        }
        else if (e.Parameter.ToString() == "I")
        {
            string msgErro = "";
            int[] listaUsuariosSelecionados = new int[1];
            DateTime auxDt = DateTime.MinValue;

            auxDt = dtePrazo.Date;

            listaUsuariosSelecionados[0] = codigoResponsavel;

            bool resultado = cDados.incluiMensagemObjeto(codigoEntidade, codigoIndicador, idUsuarioLogado, "", txtMensagem.Text.Replace("'", "''"), auxDt, true, "", listaUsuariosSelecionados, "IN", ref msgErro);

            if (resultado)
            {
                callback.JSProperties["cp_MSG"] = "Mensagem Incluída com Sucesso!";
                callback.JSProperties["cp_status"] = "ok";
            }
            else
            {
                callback.JSProperties["cp_MSG"] = "Erro ao Incluir a Mensagem!";
                callback.JSProperties["cp_status"] = "erro";
            }
        }    
    }

    protected void gvMensagens_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        int codigoUsuarioInclusao = int.Parse(gvMensagens.GetRowValues(e.VisibleIndex, "CodigoUsuarioInclusao").ToString());

        if (codigoUsuarioInclusao != idUsuarioLogado)
        {
            e.Enabled = false;
            e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            e.Text = "";
        }
    }
}
