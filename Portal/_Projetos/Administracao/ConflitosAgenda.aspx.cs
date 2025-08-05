using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using DevExpress.Web;

public partial class _Projetos_Administracao_ConflitosAgenda : System.Web.UI.Page
{
    #region Fields
    private dados cDados;
    private int codigoEntidade;
    private int codigoPojeto;
    private int altura;
    #endregion

    #region Event Handlers
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        codigoEntidade = int.Parse(Request.QueryString["CE"]);
        codigoPojeto = int.Parse(Request.QueryString["CP"]);
        altura = int.Parse(Request.QueryString["AL"]);
        gvDados.Settings.VerticalScrollableHeight = altura - 125;

        hfGeral.Set("CodigoEntidade", codigoEntidade);
        hfGeral.Set("CodigoProjeto", codigoPojeto);

        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        sdsDadosConflitosAgenda.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void cbAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
    }

    protected void gvDados_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Properties["cpVisibleRowCount"] = grid.VisibleRowCount;
    }

    protected void btnIgnorar_Click(object sender, EventArgs e)
    {
        //Obtem uma lista com os códigos de todas as ações selecionadas
        List<object> acoesSelecionados =
            gvDados.GetSelectedFieldValues(gvDados.KeyFieldName);
        if (acoesSelecionados.Count == 0)
            return;
        //Concatena todos os códigos usando vírgulas como separador
        string codigos = string.Join<object>(",", acoesSelecionados);

        string comandoSql = string.Format(@"
UPDATE {0}.{1}.[tai02_AcoesIniciativa]
   SET [IndicaIgnoraConflitoAgenda] = 'S'
 WHERE [CodigoAcao] IN ({2})"
            , cDados.getDbName(), cDados.getDbOwner(), codigos);

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
        gvDados.Selection.UnselectAll();
        gvDados.DataBind();
    }
    #endregion

    protected void gvDados_DataBound(object sender, EventArgs e)
    {
        if (gvDados.VisibleRowCount == 0)
        {
            ASPxLabel1.Text = "ATENÇÃO: Todos os conflitos de agenda das Atividades do Projeto foram resolvidos.Clique no botão fechar para continuar.";
        }
    }

}