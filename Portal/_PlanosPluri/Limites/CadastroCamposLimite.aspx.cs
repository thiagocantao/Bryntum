using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _PlanosPluri_Limites_CadastroCamposLimite : Page
{
    dados cDados;

    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;

    string _ConnectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
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
            Response.RedirectLocation = string.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dataSource.ConnectionString = ConnectionString;
        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "EN_EdtCamLim");
        ConfiguraGrid();
    }

    private void ConfiguraGrid()
    {
        gvCampoLimite.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    protected void gvCampoLimite_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        int codigoCampo = e.IsNewRow ? -1 : (int)e.Keys[0];
        var siglaCampo = e.NewValues["SiglaCampo"] as string;
        var formulaCampo = e.NewValues["FormulaCampo"] as string;
        if (VerificaSiglaDuplicada(codigoCampo, siglaCampo))
            AddError(e.Errors, gvCampoLimite.Columns["SiglaCampo"], "Sigla duplicada");
        if (!VerificaFormulaCampoValida(formulaCampo))
            AddError(e.Errors, gvCampoLimite.Columns["FormulaCampo"], "Fórmula inválida");

        if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            e.RowError = "Não foi possível salvar as informações devido a inconsistências nos dados informados.";
    }

    private bool VerificaFormulaCampoValida(string formulaCampo)
    {
        try
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(formulaCampo, connection);
                command.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
                command.Parameters.Add(new SqlParameter("@IDPlano", IDPlano));
                command.Parameters.Add(new SqlParameter("@AnoPlano", AnoPlano));
                var resultado = command.ExecuteScalar();
                return resultado == null || IsNumber(resultado);
            }
        }
        catch
        {
            return false;
        }
    }

    private string AnoPlano
    {
        get
        {
            return Request.QueryString["ano"] ?? "2015";
        }
    }

    private string IDPlano
    {
        get
        {
            return Request.QueryString["id"] ?? "1";
        }
    }

    public static bool IsNumber(object value)
    {
        return value is sbyte
            || value is byte
            || value is short
            || value is ushort
            || value is int
            || value is uint
            || value is long
            || value is ulong
            || value is float
            || value is double
            || value is decimal;
    }

    private bool VerificaSiglaDuplicada(int codigoCampo, string siglaCampo)
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
 SELECT 1 
   FROM CampoLimite 
  WHERE SiglaCampo = '{0}' 
    AND CodigoCampo <> {1}",
    siglaCampo, codigoCampo);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);

        return ds.Tables[0].Rows.Count > 0;
    }

    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;

        errors[column] = errorText;
    }
}