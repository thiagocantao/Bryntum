using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class HistoricoPlanoInvestimento : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoProjeto;
    private int codigoPlanoInvestimento;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        codigoPlanoInvestimento = int.Parse(Request.QueryString["CPI"].ToString());

        cDados.aplicaEstiloVisual(this);

        carregaGvDados();

        txtProjeto.Text = cDados.getNomeProjeto(codigoProjeto, "");

        gvPeriodo.Settings.ShowFilterRow = false;
        gvPeriodo.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }           

    private void carregaGvDados()
    {
        DataSet ds = cDados.getMovimentoProjetoPlanoInvestimentoTIC(codigoPlanoInvestimento, codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvPeriodo.DataSource = ds;
            gvPeriodo.DataBind();

            if (!IsPostBack && !IsCallback && gvPeriodo.VisibleRowCount > 0)
                gvPeriodo.FocusedRowIndex = 0;
        }

        if (gvPeriodo.VisibleRowCount > 0 && gvPeriodo.FocusedRowIndex != -1)
        {
            DataRow[] drs = ds.Tables[0].Select("CodigoMovimentoPlanoInvestimento = " + gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "CodigoMovimentoPlanoInvestimento"));
            DataRow drLinha = drs[0];
            string tipoMovimento = drLinha["TipoMovimento"].ToString();

            if (tipoMovimento == "AV" || tipoMovimento == "RM")
            {
                lblDe.Text = "Valor Anterior(R$):";
                lblPara.Text = "Novo Valor(R$):";
                txtDe.Value = drLinha["ValorAnterior"];
                txtPara.Value = drLinha["NovoValor"];
                txtDe.Width = 140;
                txtPara.Width = 140;
            }
            else
            {
                lblDe.Text = "Status Anterior:";
                lblPara.Text = "Novo Status:";
                txtDe.Text = drLinha["StatusDe"].ToString();
                txtPara.Text = drLinha["StatusPara"].ToString();
                txtDe.Width = 250;
                txtPara.Width = 250;
            }

            mmComentarios.Text = drLinha["ComentarioMovimento"].ToString();
        }
        else
        {
            txtDe.Text = "";
            txtPara.Text = "";
            mmComentarios.Text = "";
        }
    }    
}