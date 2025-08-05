using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Drawing;

public partial class _Projetos_DadosProjeto_FrmResultadosIndicador : System.Web.UI.Page
{
    dados cDados;
    DataTable dtResultados;
    DataTable dtGrid = new DataTable();

    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoProjeto, casasDecimais = 0, codigoMeta;
    private string siglaUnidadeMedida = "";
    public string larguraTabela = "";
    public bool podeEditar = true;

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
        codigoProjeto = int.Parse(Request.QueryString["CodigoProjeto"].ToString());
        codigoMeta = int.Parse(Request.QueryString["CodigoMeta"].ToString());
        casasDecimais = int.Parse(Request.QueryString["CasasDecimais"].ToString());
        siglaUnidadeMedida = Request.QueryString["SiglaUnidadeMedida"].ToString();

        txtIndicadorDado.Text = Request.QueryString["NomeIndicador"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        //if (!IsPostBack)
        //{
        //    cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_RegRes");
        //}

        cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeEditar, ref podeEditar, ref podeEditar);

        carregaComboAnos();

        carregaGridResultados(gvResultados);

        cDados.aplicaEstiloVisual(Page);

        gvResultados.Settings.ShowFilterRow = false;
        gvResultados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvResultados.SettingsBehavior.AllowDragDrop = false;
        gvResultados.SettingsBehavior.AllowSort = false;
    }

    #region Grid Resultados

    private void carregaGridResultados(ASPxGridView grid)
    {
        //carregaComboAnos(int.Parse(getCodigoIndicador()));

        string unidadeMedida = "";

        string qtdZeros = "";
        string qtdNoves = "";

        for (int i = 0; i < casasDecimais; i++)
        {
            qtdZeros += "0";
            qtdNoves += "9";
        }

        if (qtdZeros != "")
            qtdZeros = ".<" + qtdZeros + ".." + qtdNoves + ">";

        grid.Columns.Clear();

        grid.AutoGenerateColumns = true;

        dtGrid = getResultados(codigoMeta, casasDecimais);

        grid.DataSource = dtGrid;

        grid.DataBind();

        if (grid.Columns.Count > 0)
        {
            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 220;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns[0].Width = 330;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 220;
            ((GridViewDataTextColumn)grid.Columns[0]).Visible = false;


            string[] fieldNames = new string[grid.Columns.Count - 1];

            for (int i = 1; i < grid.Columns.Count; i++)
            {
                fieldNames[i - 1] = ((GridViewDataTextColumn)grid.Columns[i]).FieldName;
                ((GridViewDataTextColumn)grid.Columns[i]).Visible = false;
            }

            for (int i = 0; i < fieldNames.Length; i++)
            {
                int indexColuna = i + 1;

                GridViewDataSpinEditColumn coluna = new GridViewDataSpinEditColumn();
                coluna.FieldName = fieldNames[i];

                grid.Columns.Insert(i + 1, coluna);

                grid.Columns[indexColuna].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.EncodeHtml = false;
                if (unidadeMedida == "%")
                {
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = "{0:n" + casasDecimais + "}" + unidadeMedida;
                }
                else
                {
                    if (unidadeMedida.Contains("$"))
                    {
                        ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = unidadeMedida + "{0:n" + casasDecimais + "}";
                    }
                    else
                    {
                        ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";

                    }
                }

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 100;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Size = 8;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Width = new Unit("100%");
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DecimalPlaces = casasDecimais;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = "N" + casasDecimais;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.Visible = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberType = casasDecimais == 0 ? SpinEditNumberType.Integer : SpinEditNumberType.Float;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullDisplayText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                grid.Columns[indexColuna].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            }

            grid.Columns["_CodigoMeta"].Visible = false;

            grid.DataBind();            
        }
    }

    private DataTable getResultados(int codigoMeta, int casasDecimais)
    {
        if (ddlAno.SelectedIndex == -1)
        {
            return null;
        }

        string where = "";

        string dataInicio = string.Format("CONVERT(DateTime, '01/01/{0}', 103)" , ddlAno.Value);
        string dataTermino = string.Format("CONVERT(DateTime, '31/12/{0}', 103)", ddlAno.Value);

        where += " AND _Ano = " + ddlAno.Value;

        bool podeEditarMesesBloqueados = false;

        podeEditarMesesBloqueados = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_AltResBlq");

        dtResultados = cDados.getResultadosIndicadorProjeto(codigoEntidade, codigoMeta, dataInicio, dataTermino, casasDecimais, podeEditarMesesBloqueados, where).Tables[0];

        if (cDados.DataTableOk(dtResultados))
        {
            DataTable dtNovosResultados = new DataTable();

            string data = "";

            dtNovosResultados.Columns.Add("_NomeIndicador");

            foreach (DataRow dr in dtResultados.Rows)
            {
                if (data != dr["Data"].ToString())
                {
                    dtNovosResultados.Columns.Add(dr["Periodo"].ToString());
                    data = dr["Data"].ToString();
                }
            }

            dtNovosResultados.Columns.Add("_CodigoMeta");

            DataRow drNovoResultado = getLinha(dtResultados, dtNovosResultados, codigoMeta);

            dtNovosResultados.Rows.Add(drNovoResultado);

            return dtNovosResultados;
        }
        else
        {
            return null;
        }

    }

    private DataRow getLinha(DataTable dt, DataTable dtNova, int codigo)
    {
        int i = 1;

        DataRow drLinha = dtNova.NewRow();


        foreach (DataRow dr in dt.Rows)
        {
            if (int.Parse(dr["_CodigoMeta"].ToString()) == codigo)
            {
                if (i == 1)
                {
                    drLinha[0] = dr["_NomeIndicador"];
                }
                if (dr["Valor"].ToString() != "")
                    drLinha[i] = dr["Valor"].ToString();
                i++;
            }
        }

        drLinha[i] = codigo;

        return drLinha;
    }

    protected void gvResultados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName != "_NomeIndicador")
        {
            ASPxSpinEdit spin = new ASPxSpinEdit();

            spin.DecimalPlaces = casasDecimais;
            spin.DisplayFormatString = "N" + casasDecimais;
            spin.SpinButtons.Visible = false;
            spin.NumberType = casasDecimais == 0 ? SpinEditNumberType.Integer : SpinEditNumberType.Float;
            spin.SpinButtons.ShowIncrementButtons = false;
            spin.NullText = "";
            spin.Font.Name = "Verdana";
            spin.Border.BorderStyle = BorderStyle.None;
            spin.Font.Size = new FontUnit("8pt");
            spin.BackColor = Color.FromName("#E1EAFF");
            spin.Height = new Unit("28px");
            spin.Value = e.CellValue;
            spin.HorizontalAlign = HorizontalAlign.Right;
            spin.Width = new Unit("100%");
            e.Cell.Text = "";
            e.Cell.BackColor = Color.FromName("#E1EAFF");
            e.Cell.Style.Add("padding", "0px");
            e.Cell.Style.Add("margin", "0px");

            if (dtResultados != null && dtResultados.Rows.Count > e.DataColumn.Index - 1 && dtResultados.Rows[e.DataColumn.Index - 1]["Editavel"].ToString() != "S")
            {
                spin.ClientEnabled = false;
                spin.DisabledStyle.BackColor = Color.Transparent;
                spin.DisabledStyle.ForeColor = Color.Black;
                spin.DisabledStyle.Border.BorderStyle = BorderStyle.None;
                e.Cell.BackColor = Color.FromName("#EBEBEB");
                spin.BackColor = Color.FromName("#EBEBEB");
            }

            e.Cell.Controls.Add(spin);

            spin.ClientSideEvents.ValueChanged = @"function(s, e) { callbackSalvar.PerformCallback('" + e.DataColumn.FieldName + ";' + s.GetValue());}";
        }
    }

    protected void gvResultados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        try
        {
            if (e.Column.Index >= 2)
            {
                if (dtResultados != null && dtResultados.Rows.Count > e.Column.Index - 2 && dtResultados.Rows[e.Column.Index - 2]["Editavel"].ToString() != "S")
                {
                    (e.Editor as ASPxSpinEdit).ClientEnabled = false;
                    (e.Editor as ASPxSpinEdit).DisabledStyle.BackColor = Color.Transparent;
                    (e.Editor as ASPxSpinEdit).DisabledStyle.ForeColor = Color.Black;
                    (e.Editor as ASPxSpinEdit).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                }
            }
        }
        catch { }
    }

    #endregion   
   
    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            string periodo = e.Parameter.Split(';')[0];
            string valor = e.Parameter.Split(';')[1] != "" && e.Parameter.Split(';')[1] != "null" ? e.Parameter.Split(';')[1].Replace(",", ".") : "0";
            DataRow[] drs = dtResultados.Select("Periodo = '" + periodo + "'");

            if (drs.Length > 0)
            {
                string data = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(drs[0]["Data"].ToString()));

                cDados.atualizaResultadoIndicadorProjeto(codigoMeta, data, valor, idUsuarioLogado);
            }
        }
    }



    private void carregaComboAnos()
    {
        string where = " AND IndicaAnoAtivo = 'S' AND IndicaResultadoEditavel = 'S'";
        DataSet dsAnos = cDados.getPeriodoAnalisePortfolio(codigoEntidade, where);

        if (cDados.DataSetOk(dsAnos))
        {
            ddlAno.DataSource = dsAnos;

            ddlAno.TextField = "Ano";

            ddlAno.ValueField = "Ano";

            ddlAno.DataBind();

            if (!IsPostBack && ddlAno.Items.Count > 0)
            {
                if (ddlAno.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
                    ddlAno.Value = DateTime.Now.Year.ToString();
                else
                    ddlAno.SelectedIndex = 0;
            }
        }
    }
}