using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class espacoTrabalho_Anexo_Link : System.Web.UI.Page
{
    dados cDados;
    public int codigoEntidade = 0;
    private int idUsuarioLogado = 0;
    private int codigoAnexo = 0;

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "")
            codigoAnexo = int.Parse(Request.QueryString["CA"].ToString());

        carregaGridProjetos();
        carregaGridConsultores();

        gvProjetos.Settings.ShowFilterRow = true;
        gvProjetos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvProjetos.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

        gvConsultores.Settings.ShowFilterRow = true;
        gvConsultores.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvConsultores.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
    }

    private void carregaGridProjetos()
    {
        string comandoSQL = string.Format(@"SELECT * FROM f_GetListaObjetosAssociaveisAnexos({0}, 'PR') ORDER BY NomeObjeto", codigoAnexo);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvProjetos.DataSource = ds;
            gvProjetos.DataBind();

            if (!IsPostBack)
            {
                gvProjetos.Selection.UnselectAll();

                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["IndicaAnexoJaAssociado"].ToString() == "S")
                        gvProjetos.Selection.SelectRow(i);

                    i++;
                }
            }
        }
    }

    private void carregaGridConsultores()
    {
        string comandoSQL = string.Format(@"SELECT * FROM f_GetListaObjetosAssociaveisAnexos({0}, 'CO') ORDER BY NomeObjeto", codigoAnexo);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvConsultores.DataSource = ds;
            gvConsultores.DataBind();

            if (!IsPostBack)
            {
                gvConsultores.Selection.UnselectAll();

                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["IndicaAnexoJaAssociado"].ToString() == "S")
                        gvConsultores.Selection.SelectRow(i);

                    i++;
                }
            }
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        atualizaLinkObjeto("PR", gvProjetos);
        atualizaLinkObjeto("CO", gvConsultores);
    }

    private void atualizaLinkObjeto(string tipoAssociacao, ASPxGridView grid)
    {
        int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(tipoAssociacao);

        string comandoSQL = "DELETE AnexoAssociacao WHERE CodigoAnexo = " + codigoAnexo + " AND CodigoTipoAssociacao = " + codigoTipoAssociacao + Environment.NewLine;

        foreach (object registro in grid.GetSelectedFieldValues("CodigoObjeto"))
        {
            comandoSQL += string.Format("INSERT INTO AnexoAssociacao(CodigoAnexo, CodigoObjetoAssociado, CodigoTipoAssociacao,IndicaLinkCompartilhado) VALUES({0}, {1}, {2},'S');" + Environment.NewLine, codigoAnexo, registro, codigoTipoAssociacao);
        }

        int regAf = 0;

        cDados.execSQL(comandoSQL, ref regAf);
    }
}