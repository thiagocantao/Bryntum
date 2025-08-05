using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _PlanosPluri_DadosPlano_AprovacaoPPA : System.Web.UI.Page
{
    private int codigoPlano = -1;

    dados cDados;

    private int idUsuarioLogado;
    private int codigoEntidade;
    bool podeAprovar = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        cDados.aplicaEstiloVisual(this);

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoPlano = int.Parse(Request.QueryString["CP"].ToString());

        carregaGridPlano(gvPlano, "PLA");
        carregaGridPlano(gvOrcamento, "ORC");

        gvPlano.Settings.ShowFilterRow = false;
        gvPlano.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;

        gvOrcamento.Settings.ShowFilterRow = false;
        gvOrcamento.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;

        btnAprovar.ClientEnabled = podeAprovar && cDados.podeEditarPPA(codigoPlano, codigoEntidade, idUsuarioLogado);

        carregaDadosFinanceiros();
    }

    private void carregaGridPlano(ASPxGridView gv, string filtro)
    {        

        string comandoSQL = string.Format(@"
        EXEC dbo.p_pln_obtemAvaliacaoLimites {0}, '{1}'", codigoPlano, filtro);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            gv.DataSource = ds;
            gv.DataBind();

            if (ds.Tables[0].Select("Status <> 'OK'").Length > 0)
                podeAprovar = false;

        }
    }

    private void carregaDadosFinanceiros()
    {
        string comandoSQL = string.Format(@"
        SELECT * FROM dbo.f_pln_GetTotaisFinanceiros({0}, -1)", codigoPlano);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtDespesas.Value = ds.Tables[0].Rows[0]["ValorTotalDespesas"];
            txtReceita.Value = ds.Tables[0].Rows[0]["ValorTotalReceitas"];
        }
        }

    protected void callbackAprovar_Callback(object source, CallbackEventArgs e)
    {
        string cenario = "1";
        if (cDados.getInfoSistema("Cenario") != null)
            cenario = cDados.getInfoSistema("Cenario").ToString();

        cDados.setInfoSistema("Cenario", cenario);

        string comandoSQL = string.Format(@"
        EXEC dbo.p_pln_AprovaPPA  {0}, {1}", codigoPlano, cenario);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        callbackAprovar.JSProperties["cp_Status"] = "ERR";

        if (result)
        {
            callbackAprovar.JSProperties["cp_Msg"] = "Plano aprovado com sucesso!";
            callbackAprovar.JSProperties["cp_Status"] = "OK";
        }
        else
            callbackAprovar.JSProperties["cp_Msg"] = "Erro ao aprovar o plano!";

    }
}