using DevExpress.Web;
using System;

public partial class TelasClientes_frmSelecaoAcoesPlanejadasPrestacaoContas : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
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
        cDados.aplicaEstiloVisual(Page);
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetConnectionString();
    }

    private void SetConnectionString()
    {
        dataSource.ConnectionString = cDados.ConnectionString;
    }

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        string comandoSql;
        var codigoWorkflow = int.Parse(Request.QueryString["cw"]);
        var codigoInstancia = long.Parse(Request.QueryString["ci"]);

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoWorkflow INT,
		@CodigoInstancia BIGINT

	SET @CodigoWorkflow = {0}
	SET @CodigoInstancia = {1}

 UPDATE [SENAR_AcaoPlanejamentoABC]
	SET [DataInicioReal] = [DataInicioPrevisto],
		[DataTerminoReal] = [DataTerminoPrevisto],
		[CodigoWorkflowPC] = @CodigoWorkflow,
		[CodigoInstanciaPC] = @CodigoInstancia
  WHERE [CodigoAcao] IN ({2})",
  codigoWorkflow, codigoInstancia, e.Parameter);

        #endregion

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }
}