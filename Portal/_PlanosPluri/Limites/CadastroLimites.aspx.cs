using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _PlanosPluri_Limites_CadastroLimites : System.Web.UI.Page
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
        DefineConnectionStrings();
        cDados.aplicaEstiloVisual(this);
    }

    private void DefineConnectionStrings()
    {
        dsCampo.ConnectionString = ConnectionString;
        dataSource.ConnectionString = ConnectionString;
        dsUnidadeMedida.ConnectionString = ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "EN_EdtLimite");
        ConfiguraGrid();
    }

    private void ConfiguraGrid()
    {
        gvLimite.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    protected void gvCamposLimite_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        var rowCount = gvCamposLimite.VisibleRowCount;
        var keys = new string[rowCount];
        for (int i = 0; i < rowCount; i++)
            keys[i] = gvCamposLimite.GetRowValues(i, "SiglaCampo") as string;
        e.Properties.Add("cpKeys", keys);
    }

    protected void gvLimite_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "DescricaoObjetoEstrategia")
        {
            string comandoSQL = string.Format(@"
               SELECT oe.CodigoObjetoEstrategia,
       oe.DescricaoObjetoEstrategia
  FROM ObjetoEstrategia AS oe INNER JOIN
       MapaEstrategico AS me ON (me.CodigoMapaEstrategico = oe.CodigoMapaEstrategico
                             AND oe.DataExclusao IS NULL
                             AND me.CodigoUnidadeNegocio = {0}
                             AND oe.CodigoTipoObjetoEstrategia = 12)
ORDER BY 2

   ", codigoEntidadeUsuarioResponsavel);

            DataSet dsObjetivos = cDados.getDataSet(comandoSQL);
            (e.Editor as ASPxComboBox).DataSource = dsObjetivos;
            (e.Editor as ASPxComboBox).TextField = "DescricaoObjetoEstrategia";
            (e.Editor as ASPxComboBox).ValueField = "CodigoObjetoEstrategia";
            (e.Editor as ASPxComboBox).DataBind();

            if (e.VisibleIndex > -1)
            {
                if (gvLimite.GetRowValues(e.VisibleIndex, "CodigoObjetivoEstrategico") + "" != "")
                    (e.Editor as ASPxComboBox).Value = gvLimite.GetRowValues(e.VisibleIndex, "CodigoObjetivoEstrategico").ToString();
            }

            (e.Editor as ASPxComboBox).DataBound += combo_DataBound;
        }
        else if (e.Column.FieldName == "DescricaoContaReceita")
        {
            string comandoSQL = string.Format(@"
            SELECT pcfc.CodigoConta,
                   pcfc.DescricaoConta
              FROM PlanoContasFluxoCaixa AS pcfc 
             WHERE pcfc.CodigoEntidade = {0}
               AND pcfc.IndicaContaAnalitica = 'S'
               AND pcfc.EntradaSaida = 'E'
            ORDER BY pcfc.DescricaoConta
            ", codigoEntidadeUsuarioResponsavel);
            DataSet dsObjetivos = cDados.getDataSet(comandoSQL);
            (e.Editor as ASPxComboBox).DataSource = dsObjetivos;
            (e.Editor as ASPxComboBox).TextField = "DescricaoConta";
            (e.Editor as ASPxComboBox).ValueField = "CodigoConta";
            (e.Editor as ASPxComboBox).DataBind();

            if (e.VisibleIndex > -1)
            {
                if (gvLimite.GetRowValues(e.VisibleIndex, "CodigoContaReceita") + "" != "")
                    (e.Editor as ASPxComboBox).Value = gvLimite.GetRowValues(e.VisibleIndex, "CodigoContaReceita").ToString();
            }

                (e.Editor as ASPxComboBox).DataBound += combo_DataBound;
        }
        else if (e.Column.FieldName == "DescricaoContaDespesa")
        {
            ASPxComboBox cb = (e.Editor as ASPxComboBox);

            string comandoSQL = string.Format(@"
            SELECT pcfc.CodigoConta,
                   pcfc.DescricaoConta
              FROM PlanoContasFluxoCaixa AS pcfc 
             WHERE pcfc.CodigoEntidade = {0}
               AND pcfc.IndicaContaAnalitica = 'S'
               AND pcfc.EntradaSaida = 'S'
            ORDER BY pcfc.DescricaoConta
            ", codigoEntidadeUsuarioResponsavel);
            DataSet dsObjetivos = cDados.getDataSet(comandoSQL);
            cb.DataSource = dsObjetivos;
            cb.TextField = "DescricaoConta";
            cb.ValueField = "CodigoConta";
            cb.DataBind();

            if (e.VisibleIndex > -1)
            {
                if (gvLimite.GetRowValues(e.VisibleIndex, "CodigoContaDespesa") + "" != "")
                    cb.Value = gvLimite.GetRowValues(e.VisibleIndex, "CodigoContaDespesa").ToString();
            }

            cb.DataBound += combo_DataBound;


        }
    }

    void combo_DataBound(object sender, EventArgs e)
    {
        ASPxComboBox combo = (ASPxComboBox)sender;
        combo.Items.Insert(0, new ListEditItem("Não se Aplica", DBNull.Value));

        if (combo.Value == null)
            combo.SelectedIndex = 0;
    }
}