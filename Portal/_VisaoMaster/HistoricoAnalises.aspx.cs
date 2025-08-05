using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _VisaoMaster_HistoricoAnalises : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoIndicador;
    private int codigoProjeto;
    private int codigoMeta;
    private int anoResultado = 0;
    private int mesResultado = 0;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        this.TH(this.TS("HistoricoAnalises"));
        if (!IsPostBack && !IsCallback)
        {
            hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
            hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
            hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
            hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
            hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
            hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());
            cDados.setInfoSistema("TipoEdicao", "I");
        }

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
        cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
        cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
        cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

        codigoUsuarioResponsavel = int.Parse(hfDadosSessao.Get("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(hfDadosSessao.Get("CodigoEntidade").ToString());

        codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());
        codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        codigoMeta = int.Parse(Request.QueryString["CM"].ToString()); 
        mesResultado = int.Parse(Request.QueryString["Mes"].ToString());
        anoResultado = int.Parse(Request.QueryString["Ano"].ToString());

        cDados.aplicaEstiloVisual(this);

        carregaGvAnalises();
        carregaAnalise();

        getDadosAnaliseperfomanceProjeto();
        gvPeriodo.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvPeriodo.Settings.ShowFilterRow = false;
    
    }

    private void carregaAnalise()
    {
        if (!IsPostBack && !IsCallback)
        {
            DataSet dsIndicador = cDados.getMapaIndicadoresPainelPresidencia(codigoEntidadeUsuarioResponsavel, " AND CodigoIndicador = " + codigoIndicador + " AND CodigoProjeto = " + codigoProjeto + " AND CodigoMeta = " + codigoMeta);

            if (cDados.DataSetOk(dsIndicador) && cDados.DataTableOk(dsIndicador.Tables[0]))
            {
                txtIndicador.Text = dsIndicador.Tables[0].Rows[0]["NomeIndicador"].ToString();
                txtSitio.Text = dsIndicador.Tables[0].Rows[0]["NomeSitio"].ToString();
                imgStatusIndicador.ImageUrl = "~/imagens/" + dsIndicador.Tables[0].Rows[0]["StatusIndicador"].ToString() + ".gif";
            }
        }

        DataSet ds = cDados.getAnalisePerformanceObjeto(codigoMeta.ToString(), anoResultado.ToString(), mesResultado.ToString(), "MT");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            cDados.setInfoSistema("TipoEdicao", "E");
            if (!IsPostBack)
            {
                mmAnaliseAtual.Text = dr["Analise"].ToString();
                mmRecomendacoesAtuais.Text = dr["Recomendacoes"].ToString();
            }
            lblDataInclusaoAtual.Text = string.Format(@"{0:dd/MM/yyyy HH:mm:ss}", dr["DataInclusao"]);
            lblUsuarioInclusaoAtual.Text = dr["UsuarioInclusao"].ToString();
            lblCaptionIncluidoPorAtual.ClientVisible = true;
            lblCaptionInclusaoAtual.ClientVisible = true;

            if (dr["UsuarioAlteracao"].ToString() != "" && dr["DataUltimaAlteracao"].ToString() != "")
            {
                lblDataAlteracaoAtual.Text = string.Format(@"{0:dd/MM/yyyy HH:mm:ss}", dr["DataUltimaAlteracao"]);
                lblUsuarioAlteracaoAtual.Text = dr["UsuarioAlteracao"].ToString();
                lblCaptionAlteradoPorAtual.ClientVisible = true;
                lblCaptionUltimaAlteracaoAtual.ClientVisible = true;
            }
            else
            {
                lblCaptionAlteradoPorAtual.ClientVisible = false;
                lblCaptionUltimaAlteracaoAtual.ClientVisible = false;
            }
        }
        else
        {
            lblCaptionIncluidoPorAtual.ClientVisible = false;
            lblCaptionInclusaoAtual.ClientVisible = false;
            lblCaptionAlteradoPorAtual.ClientVisible = false;
            lblCaptionUltimaAlteracaoAtual.ClientVisible = false;
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        bool retorno = true;

        if (cDados.getInfoSistema("TipoEdicao").ToString() == "I")
        {
            retorno = cDados.incluirAnalisePerformanceMeta(codigoIndicador.ToString(), codigoMeta.ToString()
                , anoResultado.ToString(), mesResultado.ToString(), mmAnaliseAtual.Text, mmRecomendacoesAtuais.Text, codigoUsuarioResponsavel.ToString(), "MT", "O");
        }
        else if (cDados.getInfoSistema("TipoEdicao").ToString() == "E")
        {
            retorno = cDados.atualizaAnalisePerformanceMeta(codigoIndicador.ToString(), codigoMeta.ToString()
                , anoResultado.ToString(), mesResultado.ToString(), mmAnaliseAtual.Text, mmRecomendacoesAtuais.Text, codigoUsuarioResponsavel.ToString(), "MT");
        }

        if (retorno)
        {
            callbackSalvar.JSProperties["cp_Status"] = "1";
        }
        else
        {
            callbackSalvar.JSProperties["cp_Status"] = "0";
        }
    }

    private void carregaGvAnalises()
    {
        DataTable dtGrid = cDados.getPeriodicidadeHistoricoAnalises(codigoMeta, "").Tables[0];
                
        gvPeriodo.DataSource = dtGrid;
        gvPeriodo.DataBind();


        if (cDados.DataTableOk(dtGrid))
        {
            string unidadeMedida = dtGrid.Rows[0]["SiglaUnidadeMedida"].ToString();
            string casasDecimais = dtGrid.Rows[0]["CasasDecimais"].ToString();

            gvPeriodo.Columns[1].Caption = "Resultado (" + unidadeMedida + ")";

            ((GridViewDataTextColumn)gvPeriodo.Columns[1]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";

            if (!IsPostBack)
                gvPeriodo.FocusedRowIndex = 0;
        }        
    }

    private void getDadosAnaliseperfomanceProjeto()
    {
        if (gvPeriodo.FocusedRowIndex != -1)
        {
            string ano = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "Ano").ToString(); //hfGeral.Get("Ano").ToString();
            string mes = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "Mes").ToString();
            string codigoMeta = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "CodigoMetaOperacional").ToString();
            string codigoIndicador = gvPeriodo.GetRowValues(gvPeriodo.FocusedRowIndex, "CodigoIndicador").ToString();
            bool indicaMesAnoAtual = (ano == anoResultado.ToString() && mes == mesResultado.ToString());
            DataTable dt = cDados.getAnalisePerformanceObjeto(codigoMeta, ano, mes, "MT").Tables[0];

            if (cDados.DataTableOk(dt) && !indicaMesAnoAtual)
            {
                visibilidadLabels(true);

                mmAnalise.Text = dt.Rows[0]["Analise"].ToString();
                mmRecomendacoes.Text = dt.Rows[0]["Recomendacoes"].ToString();
                lblDataInclusao.Text = dt.Rows[0]["DataInclusao"].ToString();
                lblUsuarioInclusao.Text = dt.Rows[0]["UsuarioInclusao"].ToString();

                lblDataAlteracao.Text = dt.Rows[0]["DataUltimaAlteracao"].ToString();
                lblUsuarioAlteracao.Text = dt.Rows[0]["UsuarioAlteracao"].ToString();

                lblDataInclusao.ClientVisible = ("" != lblUsuarioInclusao.Text);
                lblUsuarioInclusao.ClientVisible = ("" != lblUsuarioInclusao.Text);
                lblCaptionInclusao.ClientVisible = ("" != lblUsuarioInclusao.Text);
                lblCaptionIncluidoPor.ClientVisible = ("" != lblUsuarioInclusao.Text);

                lblDataAlteracao.ClientVisible = ("" != lblUsuarioAlteracao.Text);
                lblUsuarioAlteracao.ClientVisible = ("" != lblUsuarioAlteracao.Text);
                lblCaptionUltimaAlteracao.ClientVisible = ("" != lblUsuarioAlteracao.Text);
                lblCaptionAlteradoPor.ClientVisible = ("" != lblUsuarioAlteracao.Text);
            }
            else
            {
                mmAnalise.Text = "";
                mmRecomendacoes.Text = "";
                lblDataInclusao.Text = "";
                lblUsuarioInclusao.Text = "";
                lblDataAlteracao.Text = "";
                lblUsuarioAlteracao.Text = "";

                visibilidadLabels(false);
            }
        }
        else
        {
            mmAnalise.Text = "";
            mmRecomendacoes.Text = "";
            lblDataInclusao.Text = "";
            lblUsuarioInclusao.Text = "";
            lblDataAlteracao.Text = "";
            lblUsuarioAlteracao.Text = "";

            visibilidadLabels(false);
        }
    }

    private void visibilidadLabels(bool visible)
    {
        lblCaptionIncluidoPor.ClientVisible = visible;
        lblCaptionInclusao.ClientVisible = visible;
        lblCaptionUltimaAlteracao.ClientVisible = visible;
        lblCaptionAlteradoPor.ClientVisible = visible;
        lblDataInclusao.ClientVisible = visible;
        lblUsuarioInclusao.ClientVisible = visible;
        lblDataAlteracao.ClientVisible = visible;
        lblUsuarioAlteracao.ClientVisible = visible;
    }
}