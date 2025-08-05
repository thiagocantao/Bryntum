using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Drawing;

public partial class _Estrategias_wizard_editaResultados : System.Web.UI.Page
{
    private int codigoIndicador = -1;
    private string casasDecimais = "0";

    dados cDados;
    DataTable dtResultados;
    DataTable dtGrid = new DataTable();

    private int idUsuarioLogado;
    private int codigoEntidade;
    private decimal? valorMinimo, valorMaximo;

    public bool podeEditar = false;
    bool podeEditarMesesBloqueados = false;

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

        cDados.aplicaEstiloVisual(gvResultados);
        cDados.aplicaEstiloVisual(ddlAnos);
        cDados.aplicaEstiloVisual(ddlUnidades);

        if (Request.QueryString["CodigoIndicador"] != null && Request.QueryString["CodigoIndicador"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CodigoIndicador"].ToString());

        if (Request.QueryString["CasasDecimais"] != null)
            casasDecimais = Request.QueryString["CasasDecimais"].ToString();
                
        if (Request.QueryString["Permissao"] != null)
            podeEditar = Request.QueryString["Permissao"].ToString() == "S";


        podeEditarMesesBloqueados = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", 0, "null", "IN_AltResBlq");

        montaCampos();

        carregaComboAnos();
        carregaComboUnidades();
        carregaGridResultados(gvResultados);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AtualizacaoResultados.js""></script>"));
        this.TH(this.TS("AtualizacaoResultados"));
        gvResultados.Settings.ShowFilterRow = false;
        gvResultados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvResultados.SettingsBehavior.AllowSort = false;
    }

    private void montaCampos()
    {
        txtIndicadorDado.Text = cDados.getNomeIndicador(codigoIndicador);

        DataSet dsPeriodo = cDados.getPeriodoVigenciaIndicador(codigoIndicador);

        if (cDados.DataSetOk(dsPeriodo) && cDados.DataTableOk(dsPeriodo.Tables[0]))
        {
            string inicioVigencia = dsPeriodo.Tables[0].Rows[0]["DataInicioValidadeMeta"].ToString();
            string terminoVigencia = dsPeriodo.Tables[0].Rows[0]["DataTerminoValidadeMeta"].ToString();

            if (inicioVigencia != "" && terminoVigencia != "")
            {
                lblPeriodoVigencia.Text = string.Format(@"{2} {0:dd/MM/yyyy} {3} {1:dd/MM/yyyy}.", dsPeriodo.Tables[0].Rows[0]["DataInicioValidadeMeta"], dsPeriodo.Tables[0].Rows[0]["DataTerminoValidadeMeta"], Resources.traducao.per_odo_de_vig_ncia_entre, Resources.traducao.e);

            }
            else if (inicioVigencia != "")
            {
                lblPeriodoVigencia.Text = string.Format(@"{1} {0:dd/MM/yyyy}.", dsPeriodo.Tables[0].Rows[0]["DataInicioValidadeMeta"], Resources.traducao.per_odo_de_vig_ncia_a_partir_de);

            }
            else if (terminoVigencia != "")
            {
                lblPeriodoVigencia.Text = string.Format(@"{1} {0:dd/MM/yyyy}.", dsPeriodo.Tables[0].Rows[0]["DataTerminoValidadeMeta"], Resources.traducao.per_odo_de_vig_ncia_at_);

            }
        }
    }

    #region Grid Resultados

    private void carregaGridResultados(ASPxGridView grid)
    {
        if ((ddlAnos.SelectedIndex != -1) && (ddlUnidades.SelectedIndex != -1))
        {

            string chave = codigoIndicador.ToString();


            string qtdZeros = "";
            string qtdNoves = "";

            for (int i = 0; i < int.Parse(casasDecimais); i++)
            {
                qtdZeros += "0";
                qtdNoves += "9";
            }

            if (qtdZeros != "")
                qtdZeros = ".<" + qtdZeros + ".." + qtdNoves + ">";

            grid.Columns.Clear();

            grid.AutoGenerateColumns = true;

            dtGrid = getResultados(int.Parse(chave));

            grid.DataSource = dtGrid;

            grid.DataBind();

            if (grid.Columns.Count > 0)
            {
                ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 200;
                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
                ((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
                // ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
                grid.Columns[0].Width = 215;
                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = new Unit("100%");


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
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 160;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Size = 8;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Width = new Unit("100%");
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DecimalPlaces = int.Parse(casasDecimais);
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = "N" + casasDecimais;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.Visible = false;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullDisplayText = "";
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullText = "";
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    grid.Columns[indexColuna].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Left;
                }

                grid.Columns["_CodigoDado"].Visible = false;
                grid.Columns["Formatacao"].Visible = false;

                grid.DataBind();

                if (!(grid.Columns[0] is GridViewCommandColumn))
                {
                    GridViewCommandColumn SelectCol = new GridViewCommandColumn();
                    SelectCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
                    //SelectCol.FixedStyle = GridViewColumnFixedStyle.Left;

                    SelectCol.ShowEditButton= true;
                    /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                    SelectCol.EditButton.Image.AlternateText = "Alterar";
                    SelectCol.EditButton.Text = "Alterar";*/

                    grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                    grid.SettingsCommandButton.EditButton.Image.AlternateText = "Alterar";
                    grid.SettingsCommandButton.EditButton.Text = "Alterar";


                    SelectCol.ShowUpdateButton = true;
                    /*SelectCol.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
                    SelectCol.UpdateButton.Text = "Salvar";*/

                    grid.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
                    grid.SettingsCommandButton.UpdateButton.Text = "Salvar";

                    SelectCol.ShowCancelButton = true;
                    
                    /*SelectCol.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
                    SelectCol.CancelButton.Text = "Cancelar";*/
                    
                    grid.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
                    grid.SettingsCommandButton.CancelButton.Text = "Cancelar";

                    SelectCol.VisibleIndex = 0;
                    SelectCol.Visible = true;


                    SelectCol.Width = 60;
                    SelectCol.Caption = " ";

                    grid.Columns.Insert(0, SelectCol);
                }
            }
        }
    }

    private DataTable getResultados(int codigoIndicador)
    {

        string where = (ddlAnos.Value.ToString() != "-1" ? " AND _Ano = " + ddlAnos.Value.ToString() : "");
        int codigoUnidade = int.Parse(ddlUnidades.Value.ToString());

        dtResultados = cDados.getResultadosIndicador(codigoEntidade, codigoIndicador, codigoUnidade, int.Parse(casasDecimais), podeEditarMesesBloqueados, where).Tables[0];

        if (cDados.DataTableOk(dtResultados))
        {
            DataTable dtNovosResultados = new DataTable();

            int ano = 0, mes = 0;

            dtNovosResultados.Columns.Add("_NomeDado");

            foreach (DataRow dr in dtResultados.Rows)
            {
                if (ano != int.Parse(dr["_Ano"].ToString()) || mes != int.Parse(dr["_Mes"].ToString()))
                {
                    dtNovosResultados.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                    ano = int.Parse(dr["_Ano"].ToString());
                    mes = int.Parse(dr["_Mes"].ToString());
                }
            }

            dtNovosResultados.Columns.Add("_CodigoDado");
            dtNovosResultados.Columns.Add("Formatacao");

            DataSet dsListaDados = cDados.getListaResultadosIndicador(codigoEntidade, codigoIndicador, "");

            foreach (DataRow dr in dsListaDados.Tables[0].Rows)
            {
                DataRow drNovoResultado = getLinha(dtResultados, dtNovosResultados, int.Parse(dr["CodigoDado"].ToString()));

                if (drNovoResultado != null)
                    dtNovosResultados.Rows.Add(drNovoResultado);
            }
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

        if (dt.Select("_CodigoDado = " + codigo).Length == 0)
            return null;

        DataRow drLinha = dtNova.NewRow();

        foreach (DataRow dr in dt.Rows)
        {
            if (int.Parse(dr["_CodigoDado"].ToString()) == codigo)
            {
                if (i == 1)
                {
                    drLinha[0] = dr["_NomeDado"];
                }
                if (dr["Valor"].ToString() != "")
                    drLinha[i] = dr["Valor"].ToString();
                i++;
            }
        }

        drLinha[i] = codigo;

        drLinha[i + 1] = getFormatacaoDado(codigo);

        return drLinha;
    }

    private string getFormatacaoDado(int codigoDado)
    {
        string unidadeMedidaDado = "";
        int casaDecimaisDado = 0;

        DataSet dsDados = cDados.getDados("AND dun.IndicaUnidadeCriadoraDado = 'S' AND d.CodigoDado = " + codigoDado);

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            casaDecimaisDado = dsDados.Tables[0].Rows[0]["CasasDecimais"] + "" != "" ? int.Parse(dsDados.Tables[0].Rows[0]["CasasDecimais"].ToString()) : 0;
            unidadeMedidaDado = dsDados.Tables[0].Rows[0]["SiglaUnidadeMedida"] + "";
        }

        if (unidadeMedidaDado == "%")
        {
            return "{0:n" + casaDecimaisDado + "}" + unidadeMedidaDado;
        }
        else
        {
            if (unidadeMedidaDado.Contains("$"))
            {
                return unidadeMedidaDado + " {0:n" + casaDecimaisDado + "}";
            }
            else
            {
                return " {0:n" + casaDecimaisDado + "}";
            }
        }
    }

    protected void gvResultados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Index >= 2)
        {
            try
            {
                e.Cell.Text = string.Format(gvResultados.GetRowValues(e.VisibleIndex, "Formatacao").ToString(), double.Parse(e.CellValue.ToString()));
            }
            catch
            {
            }
        }
    }

    protected void gvResultados_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        int codigoUnidade = int.Parse(ddlUnidades.Value.ToString());

        string where = (ddlAnos.Value.ToString() != "-1" ? " AND _Ano = " + ddlAnos.Value.ToString() : "");

        dtResultados = cDados.getResultadosIndicador(codigoEntidade, codigoIndicador, codigoUnidade, 0, podeEditarMesesBloqueados, where + " AND _CodigoDado = " + e.EditingKeyValue.ToString()).Tables[0];

        DataSet dsDados = cDados.getDados("AND dun.IndicaUnidadeCriadoraDado = 'S' AND d.CodigoDado = " + e.EditingKeyValue.ToString());

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            if (dsDados.Tables[0].Rows[0]["ValorMinimo"] + "" != "")
                valorMinimo = decimal.Parse(dsDados.Tables[0].Rows[0]["ValorMinimo"].ToString());

            if (dsDados.Tables[0].Rows[0]["ValorMaximo"] + "" != "")
                valorMaximo = decimal.Parse(dsDados.Tables[0].Rows[0]["ValorMaximo"].ToString());

        }
    }

    protected void gvResultados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int count = 0;
        int codigoUnidade = int.Parse(ddlUnidades.Value.ToString());

        carregaGridResultados(gvResultados);

        DataRow[] drs = dtResultados.Select("_CodigoDado = " + e.Keys[0]);

        foreach (DataRow dr in drs)
        {
            if (dr["Editavel"].ToString() == "S")
            {
                int codigoDado = int.Parse(e.Keys[0].ToString());
                int mes = int.Parse(dr["_Mes"].ToString());
                int ano = int.Parse(dr["_Ano"].ToString());

                string valor = (e.NewValues[count + 1] != null && e.NewValues[count + 1].ToString() != "") ? e.NewValues[count + 1].ToString() : "NULL";

                cDados.atualizaResultadosIndicador(codigoUnidade, codigoDado, mes, ano, valor, idUsuarioLogado);
            }
            count++;
        }
        carregaGridResultados(gvResultados);

        e.Cancel = true;

        gvResultados.CancelEdit();
    }

    protected void gvResultados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.Index >= 2)
        {
            if (dtResultados != null)
            {
                if ((e.Editor as ASPxSpinEdit) != null)
                {
                    string formatacao = gvResultados.GetRowValues(e.VisibleIndex, "Formatacao").ToString();
                    int indexCaractere = formatacao.IndexOf('}');

                    int casasDecimais = 0;
                    try
                    {
                        casasDecimais = indexCaractere == -1 ? 0 : int.Parse(formatacao.Substring(indexCaractere - 1, 1));
                    }
                    catch { }

                    (e.Editor as ASPxSpinEdit).ClientSideEvents.KeyUp = "function(s, e) {valorAtualDado = s.GetText();}";
                    (e.Editor as ASPxSpinEdit).ClientSideEvents.NumberChanged = "function(s, e) {verificaValorMaximoMinimo(s, e);}";

                    (e.Editor as ASPxSpinEdit).DecimalPlaces = casasDecimais;
                    (e.Editor as ASPxSpinEdit).NumberType = casasDecimais == 0 ? SpinEditNumberType.Float : SpinEditNumberType.Float;
                    //(e.Editor as ASPxSpinEdit). = SpinEditNumberFormat.Custom;
                    (e.Editor as ASPxSpinEdit).DisplayFormatString = "N" + casasDecimais;
                    (e.Editor as ASPxSpinEdit).AllowNull = true;
                    (e.Editor as ASPxSpinEdit).NullText = "";
                    //(e.Editor as ASPxSpinEdit).JSProperties["cp_CasaDecimais"] = casasDecimais == 0 ? "N" : "S";

                    string funcaoValidacaoMaximo = @"if(1 != 1){
                                                                    var a = 1; 
                                                                }";
                    string funcaoValidacaoMinimo = @"";

                    if (valorMaximo.HasValue)
                    {
                        funcaoValidacaoMaximo = @"
                                                if(s.GetValue() != null && s.GetValue() > " + valorMaximo.Value.ToString().Replace(",", ".") + @")
                                                {
                                                    e.isValid = false;
                                                    e.errorText = 'O valor máximo permitido é " + string.Format("{0:n" + casasDecimais + "}", valorMaximo.Value) + @"';
                                                }";
                    }

                    if (valorMinimo.HasValue)
                    {
                        funcaoValidacaoMinimo = @"else if(s.GetValue() != null && s.GetValue() < " + valorMinimo.Value.ToString().Replace(",", ".") + @")
                                                {
                                                    e.isValid = false;
                                                    e.errorText = 'O valor mínimo permitido é " + string.Format("{0:n" + casasDecimais + "}", valorMinimo.Value) + @"';
                                                }";
                    }

                    (e.Editor as ASPxSpinEdit).ClientSideEvents.Validation = "function(s, e) {"  + funcaoValidacaoMaximo + funcaoValidacaoMinimo + "}";
                    (e.Editor as ASPxSpinEdit).ClientSideEvents.ValueChanged = "function(s, e) { if(" + casasDecimais + " == 0 && s.GetValue() != null){ s.SetValue(parseInt((s.GetValue() + ''))); }}";

                    if (dtResultados.Rows.Count > e.Column.Index - 2)
                    {
                        if (dtResultados.Rows[e.Column.Index - 2]["Editavel"].ToString() != "S")
                        {
                            (e.Editor as ASPxSpinEdit).ClientEnabled = false;
                            (e.Editor as ASPxSpinEdit).DisabledStyle.BackColor = Color.Transparent;
                            (e.Editor as ASPxSpinEdit).DisabledStyle.ForeColor = Color.Black;
                            (e.Editor as ASPxSpinEdit).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }

    protected void gvResultados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (!podeEditar)
        {
            e.Enabled = false;
            e.Image.Url = "~/imagens/botoes/editarRegDes.png";
        }
    }

    #endregion


    #region COMBOBOX

    private void carregaComboAnos()
    {
        DataSet dsAnos = cDados.getAnosAtivosIndicador(codigoIndicador, "");

        if (cDados.DataSetOk(dsAnos))
        {
            ddlAnos.DataSource = dsAnos;
            ddlAnos.TextField = "Ano";
            ddlAnos.ValueField = "Ano";
            ddlAnos.DataBind();
        }

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

        ddlAnos.Items.Insert(0, lei);

        if (!IsPostBack)
        {
            if (ddlAnos.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
                ddlAnos.Value = DateTime.Now.Year.ToString();
            else
                ddlAnos.SelectedIndex = 0;
        }
    }

    private void carregaComboUnidades()
    {
        DataSet dsUnidadesIndicador = cDados.getUnidadesUsuarioIndicadorPorPermissao(codigoIndicador, idUsuarioLogado, codigoEntidade, "IN_RegRes", "");

        if (cDados.DataSetOk(dsUnidadesIndicador))
        {
            ddlUnidades.DataSource = dsUnidadesIndicador;
            ddlUnidades.TextField = "NomeUnidadeNegocio";
            ddlUnidades.ValueField = "CodigoUnidadeNegocio";
            ddlUnidades.DataBind();
        }

        if (!IsPostBack)
        {
            ddlUnidades.SelectedIndex = 0;
        }
    }

    #endregion
}
