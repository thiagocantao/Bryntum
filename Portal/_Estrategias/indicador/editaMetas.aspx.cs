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
using System.Collections.Specialized;
using DevExpress.Utils;

public partial class _Estrategias_wizard_editaMetas : System.Web.UI.Page
{
    dados cDados;
    DataTable dtMetas;
    //a integração contínua não foi
    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoIndicador = -1;
    private int casasDecimais = 0;

    private string unidadeMedida = "";
    string iniciaisAgrupamento = "";

    public string resolucaoCliente;
    public int alturaPrincipal;

    public string exibeTitulo = "";
    public string sufixoConteudo = "";

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
        int codigoUnidade = 0;

        if (Request.QueryString["COIN"] != null && Request.QueryString["COIN"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["COIN"].ToString());

        if (Request.QueryString["CUN"] != null && Request.QueryString["CUN"].ToString() != "")
            codigoUnidade = int.Parse(Request.QueryString["CUN"].ToString());

        setaInformacoesIndicador();

        podeEditar = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", codigoUnidade, "null", "IN_DefMta");

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //cDados.aplicaEstiloVisual(gvMetas);
        //cDados.aplicaEstiloVisual(ddlAnos);
        cDados.aplicaEstiloVisual(Page);
        montaCampos();

        carregaComboAnos();
        carregaGridMetas(gvMetas);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/MetasDesempenho.js""></script>"));
        this.TH(this.TS("MetasDesempenho"));
        if (Request.QueryString["Popup"] != null && Request.QueryString["Popup"].ToString() == "S")
        {
            exibeTitulo = "display: none; ";
            sufixoConteudo = "Popup";
        }

        gvMetas.Settings.ShowFilterRow = false;
        gvMetas.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvMetas.SettingsBehavior.AllowSort = false;
        gvMetas.SettingsEditing.Mode = podeEditar ? GridViewEditingMode.Batch : GridViewEditingMode.PopupEditForm;
        txtMetaInformada.DisplayFormatString = "{0:n" + casasDecimais + "}";
        txtIndicadorDado.Text = cDados.getNomeIndicador(codigoIndicador);
        //gvMetas.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden;
        gvMetas.SettingsEditing.BatchEditSettings.KeepChangesOnCallbacks = DefaultBoolean.False;
        gvMetas.SettingsText.CommandBatchEditPreviewChanges = Resources.traducao.preview_changes;

        gvMetas.SettingsCommandButton.UpdateButton.RenderMode = GridCommandButtonRenderMode.Button;
        gvMetas.SettingsCommandButton.CancelButton.RenderMode = GridCommandButtonRenderMode.Button;
        gvMetas.SettingsCommandButton.CancelButton.Styles.Style.CssClass = "marginLeft10";


        defineAlturaTela();
    }

    private void defineAlturaTela()
    {
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        string tipo;

        tipo = string.IsNullOrEmpty(Request.QueryString["tipo"]) ? "indicador" : tipo = Request.QueryString["tipo"];

        int subtrairAltura;
        switch (tipo)
        {
            case "indicador":
                subtrairAltura = 380;
                break;
            case "meta-desempenho":
                //subtrairAltura = 720;
                if (alturaPrincipal > 960)
                {
                    subtrairAltura = 700;
                }
                else
                {
                    subtrairAltura = 500;
                }
                break;
            default:
                subtrairAltura = 380;
                break;
        }
        gvMetas.Settings.VerticalScrollableHeight = alturaPrincipal - subtrairAltura;
    }

    private void setaInformacoesIndicador()
    {
        string comandoSQL = string.Format(@"
        SELECT ISNULL(CasasDecimais, 0) as CasasDecimais, um.SiglaUnidadeMedida  
          FROM Indicador i INNER JOIN
			   TipoUnidadeMedida um ON um.CodigoUnidadeMedida = i.CodigoUnidadeMedida
         WHERE i.CodigoIndicador = {0}", codigoIndicador);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            casasDecimais = int.Parse(ds.Tables[0].Rows[0]["CasasDecimais"].ToString());
            unidadeMedida = ds.Tables[0].Rows[0]["SiglaUnidadeMedida"].ToString();
        }
    }

    private void montaCampos()
    {
        string nomeFuncao = "";

        DataSet dsPeriodo = cDados.getPeriodoVigenciaIndicador(codigoIndicador);

        if (cDados.DataSetOk(dsPeriodo) && cDados.DataTableOk(dsPeriodo.Tables[0]))
        {
            string inicioVigencia = dsPeriodo.Tables[0].Rows[0]["DataInicioValidadeMeta"].ToString();
            string terminoVigencia = dsPeriodo.Tables[0].Rows[0]["DataTerminoValidadeMeta"].ToString();

            if (inicioVigencia != "" && terminoVigencia != "")
            {
                
                lblPeriodoVigencia.Text = string.Format(@"{2} {0:" + Resources.traducao.geral_formato_data_csharp + "} {3} {1:" + Resources.traducao.geral_formato_data_csharp + "}.", dsPeriodo.Tables[0].Rows[0]["DataInicioValidadeMeta"], dsPeriodo.Tables[0].Rows[0]["DataTerminoValidadeMeta"], Resources.traducao.per_odo_de_vig_ncia_entre, Resources.traducao.e);

            }
            else if (inicioVigencia != "")
            {
                lblPeriodoVigencia.Text = string.Format(@"{1} {0:" + Resources.traducao.geral_formato_data_csharp + "}.", dsPeriodo.Tables[0].Rows[0]["DataInicioValidadeMeta"], Resources.traducao.per_odo_de_vig_ncia_a_partir_de);
               
            }
            else if (terminoVigencia != "")
            {
                lblPeriodoVigencia.Text = string.Format(@"{1} {0:" + Resources.traducao.geral_formato_data_csharp + "}.", dsPeriodo.Tables[0].Rows[0]["DataTerminoValidadeMeta"], Resources.traducao.per_odo_de_vig_ncia_at_);

            }
        }

        cDados.getAgrupamentoIndicador(codigoIndicador, ref iniciaisAgrupamento, ref nomeFuncao);

        if (nomeFuncao != "")
            lblTituloMeta.Text = nomeFuncao.Substring(0, 1).ToUpper() + nomeFuncao.Substring(1).ToLower() + ":";

        txtMetaInformada.ClientSideEvents.Init = "function(s, e) {atualizaValoresMeta('" + iniciaisAgrupamento.ToUpper() + "');}";

        gvMetas.ClientSideEvents.EndCallback = @"
function(s, e) {
    inicializaVariaveis('" + iniciaisAgrupamento.ToUpper() + @"');            
	var command = hfGeral.Get('command');
    if(command === 'UPDATEEDIT')
        window.top.mostraMensagem(traducao.MetasDesempenho_MensagemRegistroSalvoComSucesso, 'sucesso', false, false, null);
    else if(command === 'DELETEROW')
        window.top.mostraMensagem(traducao.MetasDesempenho_MensagemRegistroExcluidoComSucesso, 'sucesso', false, false, null);
}";
        gvMetas.ClientSideEvents.FocusedRowChanged = "function(s, e) {inicializaVariaveis('" + iniciaisAgrupamento.ToUpper() + "');}";
        gvMetas.ClientSideEvents.BatchEditEndEditing = @"function(s, e) {atualizaValoresMeta('" + iniciaisAgrupamento.ToUpper() + "');}";
        //gvMetas.ClientSideEvents.

    }

    #region GvMetas

    private void carregaGridMetas(ASPxGridView grid)
    {
        if ((ddlAnos.SelectedIndex != -1))
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
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns[0].Width = 220;
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
                    case "SUM":
                        funcaoMeta = @"getSoma(s, e, " + (i) + ");";
                        break;
                    case "AVG":
                        funcaoMeta = @"getMedia(s, e, " + (i) + ");";
                        break;
                    case "MIN":
                        funcaoMeta = @"getMinimo(s, e, " + (i) + ");";
                        break;
                    case "MAX":
                        funcaoMeta = @"getMaximo(s, e, " + (i) + ");";
                        break;
                    case "STT":
                        funcaoMeta = @"getUltima(s, e, " + (i) + ");";
                        break;
                    default:
                        funcaoMeta = "";
                        break;
                }

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.ClientSideEvents.NumberChanged = "function(s, e) {" + funcaoMeta + "}";

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

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 125;
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

            grid.Columns["CodigoUnidade"].Visible = false;

            grid.DataBind();


        }
    }

    private DataTable getMetas(int codigoIndicador, int casasDecimais)
    {
        string where = (ddlAnos.Value.ToString() != "-1" ? " AND _Ano = " + ddlAnos.Value.ToString() : "");

        dtMetas = cDados.getMetasAtualizacao(codigoEntidade, codigoIndicador, codigoEntidade, casasDecimais, where).Tables[0];
        DataTable dtNovasMetas = new DataTable();

        int ano = 0, mes = 0;

        dtNovasMetas.Columns.Add("NomeUnidade");

        foreach (DataRow dr in dtMetas.Rows)
        {
            if (ano != int.Parse(dr["_Ano"].ToString()) || mes != int.Parse(dr["_Mes"].ToString()))
            {
                if (dtNovasMetas.Columns.Contains(dr["Periodo"].ToString()))
                    break;

                dtNovasMetas.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                ano = int.Parse(dr["_Ano"].ToString());
                mes = int.Parse(dr["_Mes"].ToString());
            }
        }

        dtNovasMetas.Columns.Add("CodigoUnidade");

        DataSet dsUnidadesIndicador = cDados.getListaUnidadesIndicador(codigoEntidade, codigoIndicador, "");

        foreach (DataRow dr in dsUnidadesIndicador.Tables[0].Rows)
        {
            DataRow drLinha = getLinha(dtMetas, dtNovasMetas, int.Parse(dr["CodigoUnidade"].ToString()));

            if (drLinha != null)
                dtNovasMetas.Rows.Add(drLinha);
        }

        return dtNovasMetas;
    }

    private DataRow getLinha(DataTable dt, DataTable dtNova, int codigo)
    {
        int i = 1;

        DataRow drLinha = dtNova.NewRow();

        string arrayValores = "";
        string arrayAnos = "";
        string arrayMeses = "";

        foreach (DataRow dr in dt.Select("CodigoUnidade = " + codigo))
        {
            try
            {
                if (i == 1)
                {
                    drLinha[0] = dr["NomeUnidade"].ToString();
                }

                if (dr["Valor"].ToString() != "")
                    drLinha[i] = dr["Valor"].ToString();
                string ano = dr["_Ano"].ToString();
                arrayValores += (dr["Valor"].ToString().Replace(',', '.') + ";");
                arrayAnos += (ano + ";");
                arrayMeses += (dr["Periodo"].ToString() + ";");
                i++;
            }
            catch { }
        }

        gvMetas.JSProperties["cp_Valores_" + codigo] = arrayValores != "" ? arrayValores.Substring(0, arrayValores.Length - 1) : arrayValores;
        gvMetas.JSProperties["cp_Anos_" + codigo] = arrayAnos != "" ? arrayAnos.Substring(0, arrayAnos.Length - 1) : arrayAnos;
        gvMetas.JSProperties["cp_Meses_" + codigo] = arrayMeses != "" ? arrayMeses.Substring(0, arrayMeses.Length - 1) : arrayMeses;

        try
        {
            drLinha[i] = codigo;
        }
        catch { }

        return drLinha;
    }

    protected void gvMetas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        try
        {
            if (e.Column.Index >= 1 && e.VisibleIndex > -1)
            {
                if (dtMetas != null)
                {
                    string codigoUnidade = gvMetas.GetRowValues(e.VisibleIndex, "CodigoUnidade").ToString();
                    string periodo = e.Column.FieldName;
                    DataRow[] drs = dtMetas.Select("CodigoUnidade = " + codigoUnidade + " AND Periodo = '" + periodo + "'");
                    if (drs[0]["Editavel"].ToString() != "S")
                    {
                        (e.Editor as ASPxSpinEdit).ClientEnabled = false;
                        (e.Editor as ASPxSpinEdit).DisabledStyle.BackColor = Color.Transparent;
                        (e.Editor as ASPxSpinEdit).DisabledStyle.ForeColor = Color.Black;
                        (e.Editor as ASPxSpinEdit).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    }
                }
            }
        }
        catch { }
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

    #endregion

    protected void gvMetas_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        try
        {
            if (e.DataColumn.Index >= 1 && e.VisibleIndex > -1)
            {
                if (dtMetas != null && dtMetas.Rows.Count > e.DataColumn.Index - 1 && dtMetas.Rows[e.DataColumn.Index - 1]["Editavel"].ToString() != "S")
                {
                    hfGeral.Set(e.DataColumn.FieldName + "_" + e.VisibleIndex + "_" + e.DataColumn.Index, "N");
                    e.Cell.BackColor = Color.FromName("#C4C4C4");
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.ToolTip = Resources.traducao.per_odo_n_o_edit_vel;
                }
                else if (podeEditar)
                {
                    hfGeral.Set(e.DataColumn.FieldName + "_" + e.VisibleIndex + "_" + e.DataColumn.Index, "S");
                    e.Cell.BackColor = Color.White;
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.ToolTip = Resources.traducao.clique_para_editar;
                }
            }
            //else
            //{
            //    e.Cell.BackColor = Color.White;
            //    e.Cell.ForeColor = Color.Black;
            //}
        }
        catch { }
    }

    protected void gvMetas_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        for (int i = 0; i < e.UpdateValues.Count; i++)
        {
            for (int j = 0; j < e.UpdateValues[i].NewValues.Count; j++)
            {
                object[] keys = new object[e.UpdateValues[i].NewValues.Keys.Count];
                e.UpdateValues[i].NewValues.Keys.CopyTo(keys, 0);
                string fieldName = keys[j].ToString();
                if (fieldName != "NomeUnidade")
                {
                    DataRow[] dr = dtMetas.Select("Periodo = '" + fieldName + "' AND CodigoUnidade = " + e.UpdateValues[i].Keys[0]);

                    int mes = int.Parse(dr[0]["_Mes"].ToString());
                    int ano = int.Parse(dr[0]["_Ano"].ToString());

                    string valor = (e.UpdateValues[i].NewValues[j] != null && e.UpdateValues[i].NewValues[j].ToString() != "") ? e.UpdateValues[i].NewValues[j].ToString() : "NULL";

                    cDados.atualizaMetaIndicador(int.Parse(e.UpdateValues[i].Keys[0].ToString()), codigoIndicador, mes, ano, valor, idUsuarioLogado);
                }
            }
        }

        carregaGridMetas(gvMetas);
        e.Handled = true;
    }
}
