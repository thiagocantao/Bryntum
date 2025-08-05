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

public partial class _Estrategias_wizard_editaMetas : System.Web.UI.Page
{
    dados cDados;
    DataTable dtMetas;

    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoIndicador = -1;
    private int casasDecimais = 0;
    
    string iniciaisAgrupamento = "";

    public bool podeEditar = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["CodigoIndicador"] != null && Request.QueryString["CodigoIndicador"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CodigoIndicador"].ToString());

        if (Request.QueryString["CasasDecimais"] != null)
            casasDecimais = int.Parse(Request.QueryString["CasasDecimais"].ToString());
        
        if (Request.QueryString["Permissao"] != null)
            podeEditar = Request.QueryString["Permissao"].ToString() == "S";

        if (Request.QueryString["Altura"] != null)
            gvMetas.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString()) - 25;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(gvMetas);
        cDados.aplicaEstiloVisual(ddlAnos);
        cDados.aplicaEstiloVisual(ddlUnidades);
        montaCampos();

        carregaComboAnos();
        carregaComboUnidades();
        carregaGridMetas(gvMetas);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/MetasDesempenho.js""></script>"));
        this.TH(this.TS("MetasDesempenho"));
        gvMetas.Settings.ShowFilterRow = false;
        gvMetas.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvMetas.SettingsBehavior.AllowSort = false;
        txtMetaInformada.DisplayFormatString = "{0:n" + casasDecimais + "}";
    }

    private void montaCampos()
    {
        txtIndicadorDado.Text = cDados.getNomeIndicador(codigoIndicador);
        string nomeFuncao = "";

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

        cDados.getAgrupamentoIndicador(codigoIndicador, ref iniciaisAgrupamento, ref nomeFuncao);

        if (nomeFuncao != "")
            lblTituloMeta.Text = nomeFuncao.Substring(0, 1).ToUpper() + nomeFuncao.Substring(1).ToLower() + ":";

        string funcaoInit = "";

        switch (iniciaisAgrupamento.ToUpper())
        {
            case "SUM": funcaoInit = string.Format(@"var valor = getSumArray(valores); 
                                                     if (isNaN(valor) || valor == null) 
                                                         txtMetaInformada.SetText('');
                                                     else
                                                         txtMetaInformada.SetText(valor.toString().replace('.', ','));");
                break;
            case "AVG": funcaoInit = string.Format(@"var valor = getAvgArray(valores); 
                                                     if (isNaN(valor) || valor == null) 
                                                         txtMetaInformada.SetText('');
                                                     else
                                                         txtMetaInformada.SetText(valor.toString().replace('.', ','));");
                break;
            case "MIN": funcaoInit = string.Format(@"var valor = getMinArray(valores); 
                                                     if (isNaN(valor) || valor == null) 
                                                         txtMetaInformada.SetText('');
                                                     else
                                                         txtMetaInformada.SetText(valor.toString().replace('.', ','));");
                break;
            case "MAX": funcaoInit = string.Format(@"var valor = getMaxArray(valores); 
                                                     if (isNaN(valor) || valor == null) 
                                                         txtMetaInformada.SetText('');
                                                     else
                                                         txtMetaInformada.SetText(valor.toString().replace('.', ','));");
                break;
            case "STT": funcaoInit = string.Format(@"var valor = getLstArray(valores); 
                                                     if (isNaN(valor) || valor == null) 
                                                         txtMetaInformada.SetText('');
                                                     else
                                                         txtMetaInformada.SetText(valor.toString().replace('.', ','));");
                break;
            default: funcaoInit = string.Format(@"var valor = getSumArray(valores); 
                                                     if (isNaN(valor) || valor == null) 
                                                         txtMetaInformada.SetText('');
                                                     else
                                                         txtMetaInformada.SetText(valor.toString().replace('.', ','));");
                break;
        }

        txtMetaInformada.ClientSideEvents.Init = "function(s, e) {" + funcaoInit + "}";

        gvMetas.ClientSideEvents.EndCallback = "function(s, e) {inicializaVariaveis();" + funcaoInit + "}";
    }

    #region GvMetas

    private void carregaGridMetas(ASPxGridView grid)
    {
        if ((ddlAnos.SelectedIndex != -1) && (ddlUnidades.SelectedIndex != -1))
        {
            string chave = codigoIndicador.ToString();
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
            grid.DataSource = getMetas(int.Parse(chave), casasDecimais);
            grid.DataBind();

            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
            //((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns[0].Width = 140;
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

                string funcaoMeta = @"";

                switch (iniciaisAgrupamento.ToUpper())
                {
                    case "SUM": funcaoMeta = @"getSoma(s, e, " + (i) + ");";
                        break;
                    case "AVG": funcaoMeta = @"getMedia(s, e, " + (i) + ");";
                        break;
                    case "MIN": funcaoMeta = @"getMinimo(s, e, " + (i) + ");";
                        break;
                    case "MAX": funcaoMeta = @"getMaximo(s, e, " + (i) + ");";
                        break;
                    case "STT": funcaoMeta = @"getUltima(s, e, " + (i) + ");";
                        break;
                    default: funcaoMeta = "";
                        break;
                }

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.ClientSideEvents.ValueChanged = "function(s, e) {" + funcaoMeta + "}";

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

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 140;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Width = new Unit("100%");
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DecimalPlaces = casasDecimais;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.Visible = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberType = casasDecimais == 0 ? SpinEditNumberType.Integer : SpinEditNumberType.Float;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullDisplayText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                grid.Columns[indexColuna].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            }

            grid.Columns["_CodigoIndicador"].Visible = false;

            grid.DataBind();

            if (!(grid.Columns[0] is GridViewCommandColumn))
            {
                GridViewCommandColumn SelectCol = new GridViewCommandColumn();
                SelectCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
                //SelectCol.FixedStyle = GridViewColumnFixedStyle.Left;
                SelectCol.Caption = " ";

                SelectCol.ShowEditButton = true;
                //SelectCol.ShowEditButton= true;
                //SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                //SelectCol.EditButton.Image.AlternateText = "Alterar";
                //SelectCol.EditButton.Text = "Alterar";

                SelectCol.ShowUpdateButton = true;
                //SelectCol.ShowUpdateButton = true;
                //SelectCol.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
                //SelectCol.UpdateButton.Text = "Salvar";

                SelectCol.ShowCancelButton = true;
                //SelectCol.ShowCancelButton = true;
                //SelectCol.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
                //SelectCol.CancelButton.Text = "Cancelar";

                SelectCol.VisibleIndex = 0;
                SelectCol.Visible = true;


                SelectCol.Width = 85;

                grid.Columns.Insert(0, SelectCol);
            }
        }
    }

    private DataTable getMetas(int codigoIndicador, int casasDecimais)
    {
        string where = (ddlAnos.Value.ToString() != "-1" ? " AND _Ano = " + ddlAnos.Value.ToString() : "");
        int codigoUnidade = int.Parse(ddlUnidades.Value.ToString());

        dtMetas = cDados.getMetasAtualizacao(codigoEntidade, codigoIndicador, codigoUnidade, casasDecimais, where).Tables[0];
        DataTable dtNovasMetas = new DataTable();

        int ano = 0, mes = 0;

        dtNovasMetas.Columns.Add("_NomeIndicador");

        foreach (DataRow dr in dtMetas.Rows)
        {
            if (ano != int.Parse(dr["_Ano"].ToString()) || mes != int.Parse(dr["_Mes"].ToString()))
            {
                dtNovasMetas.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                ano = int.Parse(dr["_Ano"].ToString());
                mes = int.Parse(dr["_Mes"].ToString());
            }
        }

        dtNovasMetas.Columns.Add("_CodigoIndicador");
        DataRow drLinha = getLinha(dtMetas, dtNovasMetas, codigoIndicador);
        dtNovasMetas.Rows.Add(drLinha);
        return dtNovasMetas;
    }

    private DataRow getLinha(DataTable dt, DataTable dtNova, int codigo)
    {
        int i = 1;

        DataRow drLinha = dtNova.NewRow();

        string arrayValores = "";
        string arrayAnos = "";
        string arrayMeses = "";

        foreach (DataRow dr in dt.Rows)
        {
            try
            {
                if (int.Parse(dr["_CodigoIndicador"].ToString()) == codigo)
                {
                    if (i == 1)
                    {
                        drLinha[0] = "Metas do Indicador:";
                    }

                    if (dr["Valor"].ToString() != "")
                        drLinha[i] = dr["Valor"].ToString();
                    string ano = dr["_Ano"].ToString();
                    arrayValores += (dr["Valor"].ToString().Replace(',', '.') + ";");
                    arrayAnos += (ano + ";");
                    arrayMeses += (dr["Periodo"].ToString() + ";");
                    i++;
                }
            }
            catch { }
        }

        gvMetas.JSProperties["cp_Valores"] = arrayValores != "" ? arrayValores.Substring(0, arrayValores.Length - 1) : arrayValores;
        gvMetas.JSProperties["cp_Anos"] = arrayAnos != "" ? arrayAnos.Substring(0, arrayAnos.Length - 1) : arrayAnos;
        gvMetas.JSProperties["cp_Meses"] = arrayMeses != "" ? arrayMeses.Substring(0, arrayMeses.Length - 1) : arrayMeses;

        try
        {
            drLinha[i] = codigo;
        }
        catch { }

        return drLinha;
    }

    protected void gvMetas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int count = 0;
        int codigoUnidade = int.Parse(ddlUnidades.Value.ToString());

        foreach (DataRow dr in dtMetas.Rows)
        {
            int mes = int.Parse(dr["_Mes"].ToString());
            int ano = int.Parse(dr["_Ano"].ToString());

            string valor = (e.NewValues[count + 1] != null && e.NewValues[count + 1].ToString() != "") ? e.NewValues[count + 1].ToString() : "NULL";

            cDados.atualizaMetaIndicador(codigoUnidade, codigoIndicador, mes, ano, valor, idUsuarioLogado);

            count++;
        }
        carregaGridMetas(gvMetas);

        e.Cancel = true;

        gvMetas.CancelEdit();
    }

    protected void gvMetas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        try
        {
            if (e.Column.Index >= 2)
            {
                if (dtMetas != null && dtMetas.Rows.Count > e.Column.Index - 2 && dtMetas.Rows[e.Column.Index - 2]["Editavel"].ToString() != "S")
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

    protected void gvMetas_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
        DataSet dsUnidadesIndicador = cDados.getUnidadesUsuarioIndicadorPorPermissao(codigoIndicador, idUsuarioLogado, codigoEntidade, "IN_DefMta", "");

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
