using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Processos_Visualizacao_WizardDefinicaoListaCampos : System.Web.UI.Page
{
    #region Fields

    dados cDados;
    int codigoEntidade;
    int codigoUsuarioLogado; 

    #endregion

    #region Event Handlers

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
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));

        SetConnectionString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetVisibleColumns();
        PermiteUsuarioConfigurarCampoControle();
    }

    protected void gvDados_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        StringBuilder comandoSql = new StringBuilder();
        foreach (var registro in e.UpdateValues)
        {
            var codigoSubLista = registro.NewValues["unbound_CampoSubLista"];
            if (codigoSubLista == null)
                continue;


            #region Comando SQL

            comandoSql.AppendFormat(@"
INSERT INTO ListaCampoLink
           (CodigoCampoLista
           ,CodigoCampoSubLista)
     VALUES
           ({0},{1})",
           registro.Keys[0], codigoSubLista);

            #endregion

        }
        string strComandoSql = comandoSql.ToString();
        if (!string.IsNullOrEmpty(strComandoSql))
        {
            int regAfetados = 0;
            cDados.execSQL(strComandoSql, ref regAfetados);
        }
    }

    protected void gvDados_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cp_rowCount"] = gvDados.VisibleRowCount;
        e.Properties["cp_existeColunaCodigoProjeto"] = VerficaExisteColunaCodigoProjeto();
    }

    private bool VerficaExisteColunaCodigoProjeto()
    {
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            var values = (object[])gvDados.GetRowValues(
                i, "IndicaCampoControle", "IniciaisCampoControlado");
            var indicaCampoControle = (values[0] as string) ?? string.Empty;
            var iniciaisCampoControlado = (values[1] as string) ?? string.Empty;
            if (indicaCampoControle.Equals("S", StringComparison.InvariantCultureIgnoreCase) &&
                iniciaisCampoControlado.Equals("CP", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Methods

    private void SetVisibleColumns()
    {
        string tipoLista = Request.QueryString["tipoLista"];
        string[] visibleColumnNames = GetVisibleColumnNames(tipoLista);
        foreach (var colName in visibleColumnNames)
        {
            gvDados.Columns[colName].Visible = true;
        }
        int codigoSubLista;
        if (int.TryParse(Request.QueryString["csl"], out codigoSubLista))
            gvDados.Columns["unbound_CampoSubLista"].Visible = codigoSubLista > -1;
    }

    private static string[] GetVisibleColumnNames(string tipoLista)
    {
        switch (tipoLista)
        {
            case "RELATORIO":
            case "PROCESSO":
                return new string[] { "TipoFiltro", "IndicaAgrupamento", "OrdemAgrupamentoCampo", "TipoTotalizador", "AlinhamentoCampo", "IndicaCampoHierarquia", "LarguraColuna", "TituloColunaAgrupadora", "IndicaColunaFixa" };
            case "OLAP":
                return new string[] { "IndicaAreaFiltro", "IndicaAreaDado", "IndicaAreaColuna", "IndicaAreaLinha", "AreaDefault" };
            case "ARVORE":
                return new string[] { "TipoTotalizador", "AlinhamentoCampo", "IndicaCampoHierarquia", "LarguraColuna" };
            default:
                return new string[] { };
        }
    }

    private void SetConnectionString()
    {
        dataSource.ConnectionString = cDados.ConnectionString;
        sdsCamposSubLista.ConnectionString = cDados.ConnectionString;
    }

    private void PermiteUsuarioConfigurarCampoControle()
    {
        bool acessoConcedido = false;
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoUsuario Int, 
        @CodigoEntidade Int

    SET @CodigoUsuario = {2}
    SET @CodigoEntidade = {0}.{1}.f_GetCodigoEntidadePrimariaSistema();

 SELECT {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, 1, NULL, 'SI', 0, NULL, 'SI_incRel') AS AcessoConcedido;",
cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioLogado);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        if (ds.Tables[0].Rows.Count > 0)
            acessoConcedido = (bool)ds.Tables[0].Rows[0]["AcessoConcedido"];
        gvDados.Columns["IniciaisCampoControlado"].Visible = acessoConcedido;
        gvDados.Columns["IndicaCampoControle"].Visible = acessoConcedido;
    }

    #endregion
}