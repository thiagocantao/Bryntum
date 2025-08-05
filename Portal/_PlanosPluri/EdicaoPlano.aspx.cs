using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _PlanosPluri_EdicaoPlano : System.Web.UI.Page
{
    int codigoPlano = -1;
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        cDados = CdadosUtil.GetCdados(null);

        codigoPlano = (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "") ? int.Parse(Request.QueryString["CP"].ToString()) : -1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);

        carregaComboPortfolios();
        carregaComboPlanosConsolidadores();
        if (!IsPostBack)
            carregaDadosPlano();
    }

    private void carregaDadosPlano()
    {
        string comandoSQL = string.Format(@"
       SELECT NomePlano, CodigoPlanoSuperior, CodigoUnidadeNegocio, CodigoPortfolioAssociado 
         FROM Plano
        WHERE CodigoPlano = {0}", codigoPlano);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtPlano.Text = dr["NomePlano"].ToString();
            ddlPlano.Value = dr["CodigoPlanoSuperior"].ToString() == "" ? "-1" : dr["CodigoPlanoSuperior"].ToString();
            ddlPorfolios.Value = dr["CodigoPortfolioAssociado"].ToString() == "" ? "-1" : dr["CodigoPortfolioAssociado"].ToString();
        }
    }

    private void carregaComboPortfolios()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoPortfolio,
               DescricaoPortfolio
          FROM Portfolio
         WHERE CodigoEntidade = {0}  
         ORDER BY 2", codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            ddlPorfolios.DataSource = ds;
            ddlPorfolios.TextField = "DescricaoPortfolio";
            ddlPorfolios.ValueField = "CodigoPortfolio";
            ddlPorfolios.DataBind();
        }

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlPorfolios.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlPorfolios.SelectedIndex = 0;
    }

    private void carregaComboPlanosConsolidadores()
    {
        string comandoSQL = string.Format(@"
        SELECT pln.CodigoPlano,
               pln.NomePlano
         FROM UnidadeNegocio AS un INNER JOIN
              Plano AS pln ON (pln.CodigoUnidadeNegocio = un.CodigoUnidadeNegocioSuperior
                           AND pln.IndicaPlanoConsolidador = 'S')
       WHERE un.CodigoUnidadeNegocio = {0}
       ORDER BY 2", codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlPlano.DataSource = ds;
            ddlPlano.TextField = "NomePlano";
            ddlPlano.ValueField = "CodigoPlano";
            ddlPlano.DataBind();
        }

        ListEditItem lei = new ListEditItem(" ", "-1");
        ddlPlano.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlPlano.SelectedIndex = 0;
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoSQL = "";
        int regAf = 0;
        bool retorno = false;

        if(codigoPlano == -1)
        {
            comandoSQL = string.Format(@"
            SELECT 1 
              FROM UnidadeNegocio 
             WHERE CodigoUnidadeNegocioSuperior IS NULL
               AND CodigoEntidade = CodigoUnidadeNegocio
               AND CodigoUnidadeNegocio = {0}", codigoEntidadeUsuarioResponsavel);

            DataSet dsEntidade = cDados.getDataSet(comandoSQL);

            string indicaConsolidador = "N";

            if (cDados.DataSetOk(dsEntidade) && cDados.DataTableOk(dsEntidade.Tables[0]))
                indicaConsolidador = "S";

            comandoSQL = string.Format(@"
              INSERT INTO [dbo].[Plano]
                   ([NomePlano]
                   ,[CodigoPlanoSuperior]
                   ,[CodigoUnidadeNegocio]
                   ,[CodigoPortfolioAssociado]
                   ,[CodigoStatusPlano]
                   ,[IndicaPlanoConsolidador])
             VALUES
                   ('{0}'
                   ,{1}
                   ,{2}
                   ,{3}
                   ,1
                   ,'{4}')"
           , txtPlano.Text.Replace("'", "''")
           , ddlPlano.Value.ToString() == "-1" ? "NULL" : ddlPlano.Value.ToString()
           , codigoEntidadeUsuarioResponsavel
           , ddlPorfolios.Value.ToString() == "-1" ? "NULL" : ddlPorfolios.Value.ToString()
           , indicaConsolidador);            
        }
        else
        {
            comandoSQL = string.Format(@"
              UPDATE [dbo].[Plano] SET
                    [NomePlano] = '{0}'
                   ,[CodigoPlanoSuperior] = {1}
                   ,[CodigoPortfolioAssociado] = {2}
              WHERE CodigoPlano = {3}"
          , txtPlano.Text.Replace("'", "''")
          , ddlPlano.Value.ToString() == "-1" ? "NULL" : ddlPlano.Value.ToString()
          , ddlPorfolios.Value.ToString() == "-1" ? "NULL" : ddlPorfolios.Value.ToString()
          , codigoPlano);
        }

        retorno = cDados.execSQL(comandoSQL, ref regAf);

        if (retorno)
        {
            callbackSalvar.JSProperties["cp_Msg"] = "Dados salvos com sucesso!";
            callbackSalvar.JSProperties["cp_Status"] = "OK";
        }
        else
        {
            callbackSalvar.JSProperties["cp_Msg"] = "Erro ao salvar dados!";
            callbackSalvar.JSProperties["cp_Status"] = "ERR";
        }
    }
}