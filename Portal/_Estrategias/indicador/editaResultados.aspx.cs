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
    private string unidadeMedida = "";

    dados cDados;
    DataTable dtResultados;
    DataTable dtGrid = new DataTable();

    private int idUsuarioLogado;
    private int codigoEntidade;
    private decimal? valorMinimo, valorMaximo;

    public string resolucaoCliente;
    public int alturaPrincipal;

    public string exibeTitulo = "";
    public string sufixoConteudo = "";

    public bool podeEditar = false;
    bool podeEditarMesesBloqueados = false;
    int codigoUnidade = 0;

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

        //cDados.aplicaEstiloVisual(gvResultados);
        //cDados.aplicaEstiloVisual(ddlAnos);
        cDados.aplicaEstiloVisual(Page);


        if (Request.QueryString["COIN"] != null && Request.QueryString["COIN"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["COIN"].ToString());

        if (Request.QueryString["CUN"] != null && Request.QueryString["CUN"].ToString() != "")
        {
            codigoUnidade = int.Parse(Request.QueryString["CUN"].ToString());
            ddlUnidades.ClientEnabled = false;
            ddlUnidades.DropDownButton.ClientVisible = false;
            ddlUnidades.Enabled = false;
        }

        if (Request.QueryString["Popup"] != null && Request.QueryString["Popup"].ToString() == "S")
        {
            exibeTitulo = "display: none; ";
            sufixoConteudo = "Popup";
        }

        setaInformacoesIndicador();

        podeEditarMesesBloqueados = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", 0, "null", "IN_AltResBlq");

        montaCampos();

        carregaComboUnidades();
        carregaComboAnos();
        carregaGridResultados(gvResultados);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AtualizacaoResultados.js""></script>"));
        this.TH(this.TS("AtualizacaoResultados"));
        gvResultados.Settings.ShowFilterRow = false;
        gvResultados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvResultados.SettingsBehavior.AllowSort = false;
        gvResultados.SettingsEditing.Mode = podeEditar ? GridViewEditingMode.Batch : GridViewEditingMode.PopupEditForm;
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
                subtrairAltura = 460;
                break;
            case "atualizacao-resultado":
                //subtrairAltura = 720;
                if (alturaPrincipal > 960)
                {
                    subtrairAltura = 590;
                }
                else
                {
                    subtrairAltura = 490;
                }
                break;
            default:
                subtrairAltura = 460;
                break;
        }
        gvResultados.Settings.VerticalScrollableHeight = alturaPrincipal - subtrairAltura;
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
            casasDecimais = ds.Tables[0].Rows[0]["CasasDecimais"].ToString();
            unidadeMedida = ds.Tables[0].Rows[0]["SiglaUnidadeMedida"].ToString();
        }
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

                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 150;
                ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
                ((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
                ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
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
                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 120;
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

                    ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.ClientSideEvents.Validation = @"
                    function(s,e){
                        if(s.GetValue() != null && hfGeral.Get(indexAtual + 'Max') != null && s.GetValue() > parseFloat(hfGeral.Get(indexAtual + 'Max')))
                        {
                            e.isValid = false;
                            e.errorText = '#$#O valor máximo permitido é ' + parseFloat(hfGeral.Get(indexAtual + 'Max')).toFixed(2).toString().replace('.', ',');
                        }else if(s.GetValue() != null && hfGeral.Get(indexAtual + 'Min') != null && s.GetValue() < parseFloat(hfGeral.Get(indexAtual + 'Min')))
                        {
                            e.isValid = false;
                            e.errorText = traducao.editaResultados_o_valor_m_nimo_permitido___ + parseFloat(hfGeral.Get(indexAtual + 'Min')).toFixed(2).toString().replace('.', ',');
                        }
                    }";
                }

                grid.Columns["_CodigoDado"].Visible = false;
                grid.Columns["Formatacao"].Visible = false;

                grid.DataBind();                
            }
        }
    }

    private DataTable getResultados(int codigoIndicador)
    {
        int codigoUnidade = int.Parse(ddlUnidades.Value.ToString());
        string where = (ddlAnos.Value.ToString() != "-1" ? " AND _Ano = " + ddlAnos.Value.ToString() : "");

        podeEditar = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", codigoUnidade, "null", "IN_RegRes");

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

            DataSet dsListaDados = cDados.getListaResultadosIndicador(codigoUnidade, codigoIndicador, "");

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
        if (e.DataColumn.Index >= 1 && e.VisibleIndex > -1)
        {
            if (dtResultados != null)
            {
                string codigoDado = gvResultados.GetRowValues(e.VisibleIndex, "_CodigoDado").ToString();

                DataRow[] dr = dtResultados.Select("_CodigoDado = " + codigoDado + " AND Periodo = '" + e.DataColumn.FieldName + "'");

                hfGeral.Set(e.DataColumn.FieldName + "_" + e.VisibleIndex, !podeEditar ? "N" : dr[0]["Editavel"].ToString());

                if (dr[0]["Editavel"].ToString() != "S")
                {
                    e.Cell.BackColor = Color.FromName("#C4C4C4");
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.ToolTip = Resources.traducao.per_odo_n_o_edit_vel;
                }
                else if (podeEditar)
                {
                    e.Cell.BackColor = Color.White;
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.ToolTip = Resources.traducao.clique_para_editar;
                }

                if (e.CellValue != null && e.CellValue.ToString() != "" && e.VisibleIndex > -1)
                    e.Cell.Text = string.Format(gvResultados.GetRowValues(e.VisibleIndex, "Formatacao").ToString(), double.Parse(e.CellValue.ToString()));                
            }
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

    #endregion

    protected void gvResultados_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        int codigoUnidade = int.Parse(ddlUnidades.Value.ToString());

        for (int i = 0; i < e.UpdateValues.Count; i++)
        {
            DataRow[] drs = dtResultados.Select("_CodigoDado = " + e.UpdateValues[i].Keys[0]);

            for (int j = 0; j < e.UpdateValues[i].NewValues.Count; j++)
            {
                object[] keys = new object[e.UpdateValues[i].NewValues.Keys.Count];
                e.UpdateValues[i].NewValues.Keys.CopyTo(keys, 0);
                string fieldName = keys[j].ToString();
                if (fieldName != "_NomeDado")
                {
                    int codigoDado = int.Parse(e.UpdateValues[i].Keys[0].ToString());
                    DataRow[] dr = dtResultados.Select("Periodo = '" + fieldName + "' AND _CodigoDado = " + codigoDado);

                    if (dr[0]["Editavel"].ToString() == "S")
                    {                        
                        int mes = int.Parse(dr[0]["_Mes"].ToString());
                        int ano = int.Parse(dr[0]["_Ano"].ToString());

                        string valor = (e.UpdateValues[i].NewValues[j] != null && e.UpdateValues[i].NewValues[j].ToString() != "") ? e.UpdateValues[i].NewValues[j].ToString() : "NULL";

                        cDados.atualizaResultadosIndicador(codigoUnidade, codigoDado, mes, ano, valor, idUsuarioLogado);
                    }
                }
            }
        }
        
        carregaGridResultados(gvResultados);

        e.Handled = true;
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
            if(codigoUnidade == 0)
                ddlUnidades.SelectedIndex = 0;
            else
                ddlUnidades.Value = codigoUnidade;
        }
    }

    protected void gvResultados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            string codigoDado = gvResultados.GetRowValues(e.VisibleIndex, "_CodigoDado").ToString();

            DataSet dsDados = cDados.getDados("AND dun.IndicaUnidadeCriadoraDado = 'S' AND d.CodigoDado = " + codigoDado);

            if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
            {
                if (dsDados.Tables[0].Rows[0]["ValorMinimo"] + "" != "")
                {
                    valorMinimo = decimal.Parse(dsDados.Tables[0].Rows[0]["ValorMinimo"].ToString());
                    hfGeral.Set(e.VisibleIndex + "Min", valorMinimo.Value.ToString().Replace(",", "."));
                }

                if (dsDados.Tables[0].Rows[0]["ValorMaximo"] + "" != "")
                {
                    valorMaximo = decimal.Parse(dsDados.Tables[0].Rows[0]["ValorMaximo"].ToString());
                    hfGeral.Set(e.VisibleIndex + "Max", valorMaximo.Value.ToString().Replace(",", "."));
                }

            }
        }
    }
}
