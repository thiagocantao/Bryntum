using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Demandas_VinculosDemanda : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado = 0;
    private int codigoEntidade = 0;
    private int codigoDemanda = 0;

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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoDemanda = int.Parse(Request.QueryString["CodigoDemanda"].ToString());
        txtNomeDemanda.Text = Request.QueryString["NomeDemanda"].ToString();

        cDados.aplicaEstiloVisual(this);

        carregaComboVinculosPDTI();
        carregaComboVinculosLOA();
        carregaComboVinculosProjetosTIC();

        if (!IsPostBack)
            setVinculosDemanda();
    }

    private void carregaComboVinculosPDTI()
    {
        DataSet ds = cDados.getListaVinculosPDTI(codigoEntidade, "");
        ddlVinculoPDTI.ValueField = "Codigo";
        ddlVinculoPDTI.DataSource = ds;
        ddlVinculoPDTI.DataBind();

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlVinculoPDTI.Items.Insert(0, lei);
    }

    private void carregaComboVinculosLOA()
    {
        DataSet ds = cDados.getListaVinculosLOA(codigoEntidade, "");
        ddlVinculoLOA.ValueField = "Codigo";
        ddlVinculoLOA.DataSource = ds;
        ddlVinculoLOA.DataBind();

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlVinculoLOA.Items.Insert(0, lei);
    }

    private void carregaComboVinculosProjetosTIC()
    {
        DataSet ds = cDados.getListaVinculosTIC(codigoEntidade, "");
        ddlVinculoPojetoTIC.ValueField = "Codigo";
        ddlVinculoPojetoTIC.DataSource = ds;
        ddlVinculoPojetoTIC.DataBind();

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlVinculoPojetoTIC.Items.Insert(0, lei);
    }

    private void setVinculosDemanda()
    {
        DataSet ds = cDados.getVinculosDemanda(codigoDemanda, "");

        string codigoPDTI = "-1", codigoLOA = "-1", codigoTIC = "-1", valorSolicitado = "", valorLiberado = "", valorSolicitadoAno = "", valorLiberadoAno = "";

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            codigoPDTI = dr["CodigoPDTI"].ToString() != "" ? dr["CodigoPDTI"].ToString() : "-1";
            codigoLOA = dr["CodigoLOA"].ToString() != "" ? dr["CodigoLOA"].ToString() : "-1";
            codigoTIC = dr["CodigoTIC"].ToString() != "" ? dr["CodigoTIC"].ToString() : "-1";
            valorSolicitado = dr["ValorSolicitado"].ToString();
            valorLiberado = dr["ValorLiberado"].ToString();
            valorSolicitadoAno = dr["ValorSolicitadoAno"].ToString();
            valorLiberadoAno = dr["ValorLiberadoAno"].ToString();
        }

        ddlVinculoPDTI.Value = codigoPDTI;
        ddlVinculoLOA.Value = codigoLOA;
        ddlVinculoPojetoTIC.Value = codigoTIC;
        txtValorSolicitadoTotal.Text = valorSolicitado;
        txtValorAprovadoTotal.Text = valorLiberado;
        txtValorSolicitadoAno.Text = valorSolicitadoAno;
        txtValorAprovadoAno.Text = valorLiberadoAno;
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string vinculoPDTI = ddlVinculoPDTI.SelectedIndex > 0 && ddlVinculoPDTI.Value.ToString() != "-1" ? ddlVinculoPDTI.Value.ToString() : "NULL";
        string vinculoLOA = ddlVinculoLOA.SelectedIndex > 0 && ddlVinculoLOA.Value.ToString() != "-1" ? ddlVinculoLOA.Value.ToString() : "NULL";
        string vinculoTIC = ddlVinculoPojetoTIC.SelectedIndex > 0 && ddlVinculoPojetoTIC.Value.ToString() != "-1" ? ddlVinculoPojetoTIC.Value.ToString() : "NULL";

        string valorSolicitado = txtValorSolicitadoTotal.Text != "" ? txtValorSolicitadoTotal.Value.ToString().Replace(",", ".") : "NULL";
        string valorLiberado = txtValorAprovadoTotal.Text != "" ? txtValorAprovadoTotal.Value.ToString().Replace(",", ".") : "NULL";
        string valorSolicitadoAno = txtValorSolicitadoAno.Text != "" ? txtValorSolicitadoAno.Value.ToString().Replace(",", ".") : "NULL";
        string valorLiberadoAno = txtValorAprovadoAno.Text != "" ? txtValorAprovadoAno.Value.ToString().Replace(",", ".") : "NULL";

        string comandoSQL = string.Format(@"
                                          BEGIN
                                            EXEC {0}.{1}.p_wf_pa_pbh_AjustaVinculosDemanda {2}, {3}, 'PDTI'
                                            EXEC {0}.{1}.p_wf_pa_pbh_AjustaVinculosDemanda {2}, {4}, 'LOA'
                                            EXEC {0}.{1}.p_wf_pa_pbh_AjustaVinculosDemanda {2}, {5}, 'TIC'

                                            UPDATE {0}.{1}.pbh_HistoricoDemanda SET  ValorSolicitado = {6}
                                                                                    ,ValorLiberado = {7}
                                                                                    ,ValorSolicitadoAno = {8}
                                                                                    ,ValorLiberadoAno = {9}
                                             WHERE CodigoDemandaComoProjeto = {2}
                                          END", cDados.getDbName(), cDados.getDbOwner(), codigoDemanda, vinculoPDTI, vinculoLOA, vinculoTIC, valorSolicitado, valorLiberado, valorSolicitadoAno, valorLiberadoAno);

        int regAf = 0;

        try
        {
            cDados.execSQL(comandoSQL, ref regAf);
            callbackSalvar.JSProperties["cp_Msg"] = "Dados salvos com sucesso!";
        }
        catch
        {
            callbackSalvar.JSProperties["cp_Msg"] = "Erro ao salvar os dados!";
        }
    }
}