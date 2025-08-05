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
using System.Drawing;
using DevExpress.Web;

public partial class _Portfolios_historicoProcessoInterno : System.Web.UI.Page
{
    dados cDados;

    int codigoWorkflow = 0;
    int codigoInstanciaWorkflow = 0;

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
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        codigoWorkflow = int.Parse(Request.QueryString["CW"].ToString());
        codigoInstanciaWorkflow = int.Parse(Request.QueryString["CI"].ToString());

        carregaGridHistorico();
        if (!IsPostBack)
        {

            carregaCampos();
            
        }
        cDados.aplicaEstiloVisual(this);
    }

    public void carregaGridHistorico()
    {
        DataSet dset = cDados.getHistoricoInstanciaWf(codigoWorkflow, codigoInstanciaWorkflow);

        gvHistorico.DataSource = dset;
        gvHistorico.DataBind();
    }

    public void carregaCampos()
    {
        DataSet dset = cDados.getInstanciaWf(codigoWorkflow, codigoInstanciaWorkflow);

        string processo = dset.Tables[0].Rows[0]["NomeInstancia"].ToString();
        string dataInicio = dset.Tables[0].Rows[0]["DataInicioInstancia"].ToString();
        string etapaAtual = dset.Tables[0].Rows[0]["EtapaAtual"].ToString();
        string status = dset.Tables[0].Rows[0]["Status"].ToString();

        txtProcesso.Text = processo;
        txtDataInicio.Text = dataInicio;
        txtEtapaAtual.Text = etapaAtual;

        if ( status.Equals("Cancelado") )
        {
            string dataCancelamento = dset.Tables[0].Rows[0]["DataCancelamentoInstancia"].ToString();
            status = status +  " " +  Resources.traducao.historicoProcessoInterno_em + " " + dataCancelamento;
        }

        txtStatus.Text = status;

        lblTituloTela.Text = Resources.traducao.historicoProcessoInterno_hist_rico_de_processo + " - " + processo;

    }

    protected void gvHistorico_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        carregaGridHistorico();
    }



    protected void gvHistorico_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            bool comAtraso = e.GetValue("ComAtraso").ToString().Equals("True");
            bool naoFinalizada = e.GetValue("DataTerminoEtapa").ToString().Trim().Length == 0;

            if ( (comAtraso || naoFinalizada) && ( Convert.IsDBNull(e.GetValue("TerminoPrevisto")) == false ) )
            {
                string toolTip;
                DateTime termPrev = (DateTime)e.GetValue("TerminoPrevisto");

                // se a etapa estiver atrasada
                if (comAtraso)
                    toolTip = Resources.traducao.historicoProcessoInterno_o_prazo_para_t_rmino_expirou_em + " ";
                else
                {
                    toolTip = Resources.traducao.historicoProcessoInterno_prazo_para_conclus_o + ":";
                    e.Row.Cells[5].Text = "0h";
                }

                toolTip += termPrev.ToString("dd/MM/yyyy HH:mm");
                // altera toolTip da coluna 'atraso'
                e.Row.Cells[5].ToolTip = toolTip;

                // pinta coluna atraso
                e.Row.Cells[5].Style.Add("color", "#FF0000");
            }
        }
    }
}
