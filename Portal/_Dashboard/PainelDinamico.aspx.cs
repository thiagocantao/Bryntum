using DevExpress.Web;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Dashboard_PainelDinamico : Page
{
    private int codigoUsuario;

    private dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
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

        codigoUsuario = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        Session["CodigoUsuario"] = codigoUsuario;

      
        //cDados.aplicaEstiloVisual(this);
        DefineStringsConexao();
    }

    private void DefineStringsConexao()
    {
        dsParametros.ConnectionString = ConnectionString;
        dsPortlet.ConnectionString = ConnectionString;
    }

    private string ConnectionString
    {
        get
        {
            return cDados.classeDados.getStringConexao();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "PNDIN", "ENT", -1, "Adicionar Lista aos Favoritos");
        }

        cDados.aplicaEstiloVisual(this);
        gvParametros.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvParametros.Settings.ShowFilterRow = false;
    }

    protected void callbackPanel_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoUsuario INT
    SET @CodigoUsuario = {0}
    
DECLARE @Tabela TABLE(Codigo INT, Parametro VARCHAR(1000), Valor VARCHAR(500))

 INSERT INTO @Tabela   
 SELECT pu.CodigoTipoParametro, 
        tpu.Parametro,
        pu.Valor
   FROM TipoParametroUsuario AS tpu LEFT JOIN
        ParametroUsuario AS pu ON pu.CodigoTipoParametro = tpu.CodigoTipoParametro
  WHERE pu.CodigoUsuario = @CodigoUsuario
  
 SELECT (SELECT (SELECT (CASE p.TipoPortlet WHEN 'F' THEN p.URLDashboardFixo ELSE p.IDDashboard END) FROM Portlet AS p WHERE p.CodigoPortlet = t.Valor) FROM @Tabela AS t WHERE t.Parametro = 'PrimeiroPortletPainelPessoal') AS 'PrimeiroPortletPainelPessoal',
        (SELECT (SELECT (CASE p.TipoPortlet WHEN 'F' THEN p.URLDashboardFixo ELSE p.IDDashboard END) FROM Portlet AS p WHERE p.CodigoPortlet = t.Valor) FROM @Tabela AS t WHERE t.Parametro = 'SegundoPortletPainelPessoal') AS 'SegundoPortletPainelPessoal',
        (SELECT (SELECT (CASE p.TipoPortlet WHEN 'F' THEN p.URLDashboardFixo ELSE p.IDDashboard END) FROM Portlet AS p WHERE p.CodigoPortlet = t.Valor) FROM @Tabela AS t WHERE t.Parametro = 'TerceiroPortletPainelPessoal') AS 'TerceiroPortletPainelPessoal',
        (SELECT (SELECT (CASE p.TipoPortlet WHEN 'F' THEN p.URLDashboardFixo ELSE p.IDDashboard END) FROM Portlet AS p WHERE p.CodigoPortlet = t.Valor) FROM @Tabela AS t WHERE t.Parametro = 'QuartoPortletPainelPessoal') AS 'QuartoPortletPainelPessoal',
        (SELECT (SELECT (CASE p.TipoPortlet WHEN 'F' THEN p.URLDashboardFixo ELSE p.IDDashboard END) FROM Portlet AS p WHERE p.CodigoPortlet = t.Valor) FROM @Tabela AS t WHERE t.Parametro = 'QuintoPortletPainelPessoal') AS 'QuintoPortletPainelPessoal',
        (SELECT (SELECT (CASE p.TipoPortlet WHEN 'F' THEN p.URLDashboardFixo ELSE p.IDDashboard END) FROM Portlet AS p WHERE p.CodigoPortlet = t.Valor) FROM @Tabela AS t WHERE t.Parametro = 'SextoPortletPainelPessoal') AS 'SextoPortletPainelPessoal',
        (SELECT t.Valor FROM @Tabela AS t WHERE t.Parametro = 'NumeroColunasPainelPessoal') AS 'NumeroColunasPainelPessoal',
        (SELECT t.Valor FROM @Tabela AS t WHERE t.Parametro = 'NumeroLinhasPainelPessoal') AS 'NumeroLinhasPainelPessoal'",
        codigoUsuario);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];

        var objetoParametros = new
        {
            Urls = new string[]
            {
                ObtemUrlItemPainel(dr["PrimeiroPortletPainelPessoal"] as string),
                ObtemUrlItemPainel(dr["SegundoPortletPainelPessoal"] as string),
                ObtemUrlItemPainel(dr["TerceiroPortletPainelPessoal"] as string),
                ObtemUrlItemPainel(dr["QuartoPortletPainelPessoal"] as string),
                ObtemUrlItemPainel(dr["QuintoPortletPainelPessoal"] as string),
                ObtemUrlItemPainel(dr["SextoPortletPainelPessoal"] as string)
            },
            Colunas = Convert.ToInt32(dr["NumeroColunasPainelPessoal"]),
            Linhas = Convert.ToInt32(dr["NumeroLinhasPainelPessoal"])
        };
        e.Properties.Add("cpParametros", new JavaScriptSerializer().Serialize(objetoParametros));
    }

    private string ObtemUrlItemPainel(string valor)
    {
        if (valor.StartsWith("~"))
            return VirtualPathUtility.MakeRelative(Request.RawUrl, valor);

        return string.Format("VisualizacaoDashboardPainelDinamico.aspx?id={0}", valor);
    }

    protected void cmb_Init(object sender, EventArgs e)
    {
        var cmb = sender as ASPxComboBox;
        var container = cmb.NamingContainer as GridViewDataItemTemplateContainer;
        string parametro = container.Grid.GetRowValues(container.ItemIndex, "Parametro") as string;
        if (parametro == "NumeroColunasPainelPessoal")
        {
            cmb.Items.Clear();
            cmb.DataSource = null;
            cmb.DataSourceID = null;
            for (int i = 1; i <= 3; i++)
                cmb.Items.Add(string.Format("{0} {1}", i, i > 1 ? "Colunas" : "Coluna"), i);
        }
        cmb.ClientInstanceName = string.Format("cmb_{0}", container.ItemIndex);
    }

    protected void gvParametros_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpQuantidadeLinhas"] = gvParametros.VisibleRowCount;
    }

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        StringBuilder comandoSql;
        string[] parametros = e.Parameter.Split(new char[] { ';' },
            StringSplitOptions.RemoveEmptyEntries);

        #region Comando SQL

        comandoSql = new StringBuilder(@"
DECLARE @CodigoTipoParametro INT
DECLARE @CodigoUsuario INT
DECLARE @Valor VARCHAR(500)");
        foreach (string parametro in parametros)
        {
            string codigo = parametro.Split('=')[0];
            string valor = parametro.Split('=')[1];
            comandoSql.AppendFormat(@"

    SET @CodigoTipoParametro = {0}
    SET @CodigoUsuario = {1}
    SET @Valor = '{2}'

 UPDATE ParametroUsuario
    SET Valor = @Valor
  WHERE CodigoUsuario = @CodigoUsuario
    AND CodigoTipoParametro = @CodigoTipoParametro",
    codigo, codigoUsuario, valor);
        }

        #endregion

        try
        {
            int qtdeRegistrosAfetados = 0;
            cDados.execSQL(comandoSql.ToString(), ref qtdeRegistrosAfetados);
        }
        catch (Exception ex)
        {
            e.Result = ex.Message;
        }
    }
}