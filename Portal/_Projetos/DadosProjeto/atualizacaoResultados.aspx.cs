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
using System.Text;
using DevExpress.Web;
using System.Drawing;


public partial class atualizacaoResultados : System.Web.UI.Page
{
    dados cDados;
    DataTable dtResultados;
    DataTable dtGrid = new DataTable();

    private int idUsuarioLogado;
    private int codigoEntidade;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    private int codigoProjeto;
    int qtdDias = 0;
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
        codigoProjeto = int.Parse(Request.QueryString["ID"].ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_RegRes");
        }

        HearderOnTela();

        cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeEditar, ref podeEditar, ref podeEditar);

        populaGrid();

        if (txtDe.Value == null)
        {
            txtDe.Date = DateTime.Now.Date;
            txtFim.Date = DateTime.Now.Date.AddDays(qtdDias);

            txtFim.MaxDate = DateTime.Now.Date.AddDays(qtdDias);
            txtFim.MinDate = DateTime.Now.Date;
        }

        gvDados.JSProperties["cp_Inicio"] = txtDe.Text;
        gvDados.JSProperties["cp_Termino"] = txtFim.Text;
        gvDados.JSProperties["cp_dias"] = qtdDias.ToString();

        carregaGridResultados(gvResultados);

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        gvResultados.Settings.ShowFilterRow = false;
        gvResultados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvResultados.SettingsBehavior.AllowSort = false;
    }

    #region VARIOS

    //GridPrincipal
    private void populaGrid()
    {
        DataSet ds = cDados.getIndicadoresProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();

            string periodicidade = "";

            if (gvDados.FocusedRowIndex >= 0)
                periodicidade = gvDados.GetRowValues(gvDados.FocusedRowIndex, "DescricaoPeriodicidade_PT").ToString();

            if (periodicidade == "Diária")
                qtdDias = 15;
            else if (periodicidade == "Semanal")
                qtdDias = 30;
        }
    }

    private void HearderOnTela()
    {
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AtualizacaoResultadosProjeto.js""></script>"));
        this.TH(this.TS("AtualizacaoResultadosProjeto"));
        cDados.aplicaEstiloVisual(Page);
    }

    private string getCodigoIndicador()
    {
        if (gvDados.FocusedRowIndex >= 0)
        {
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoIndicador").ToString();

        }
        else
        {
            return "-1";
        }
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }

    #endregion

    #region Grid Resultados

    private void carregaGridResultados(ASPxGridView grid)
    {
        //carregaComboAnos(int.Parse(getCodigoIndicador()));

        string chave = getChavePrimaria();
        string casasDecimais = "0";
        string unidadeMedida = "";

        if (gvDados.FocusedRowIndex >= 0)
        {
            casasDecimais = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CasasDecimais") + "" != "" ? gvDados.GetRowValues(gvDados.FocusedRowIndex, "CasasDecimais").ToString() : "0";
            unidadeMedida = gvDados.GetRowValues(gvDados.FocusedRowIndex, "SiglaUnidadeMedida") + "" != "" ? gvDados.GetRowValues(gvDados.FocusedRowIndex, "SiglaUnidadeMedida").ToString() : "";
        }
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

        dtGrid = getResultados(int.Parse(chave), int.Parse(casasDecimais));

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
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns[0].Width = 175;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 175;


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

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 120;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Size = 8;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Width = new Unit("100%");
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DecimalPlaces = int.Parse(casasDecimais);
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = "N" + casasDecimais;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.Visible = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberType = int.Parse(casasDecimais) == 0 ? SpinEditNumberType.Integer : SpinEditNumberType.Float;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullDisplayText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                grid.Columns[indexColuna].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            }

            grid.Columns["_CodigoMeta"].Visible = false;

            grid.DataBind();

            if (!(grid.Columns[0] is GridViewCommandColumn))
            {
                GridViewCommandColumn SelectCol = new GridViewCommandColumn();
                SelectCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
                SelectCol.FixedStyle = GridViewColumnFixedStyle.Left;

                SelectCol.ShowEditButton = true;
                /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                SelectCol.EditButton.Image.AlternateText = "Editar";
                SelectCol.EditButton.Text = "Editar";*/
                grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                grid.SettingsCommandButton.EditButton.Image.AlternateText = Resources.traducao.atualizacaoResultados_editar;
                grid.SettingsCommandButton.EditButton.Text = Resources.traducao.atualizacaoResultados_editar;

                SelectCol.ShowUpdateButton = true;
                /*SelectCol.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
                SelectCol.UpdateButton.Text = "Salvar";*/
                grid.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
                grid.SettingsCommandButton.UpdateButton.Text = Resources.traducao.atualizacaoResultados_salvar;

                SelectCol.ShowCancelButton = true;
                /*SelectCol.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
                SelectCol.CancelButton.Text = "Cancelar";*/

                grid.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";
                grid.SettingsCommandButton.CancelButton.Text = Resources.traducao.atualizacaoResultados_cancelar;

                SelectCol.VisibleIndex = 0;
                SelectCol.Visible = true;


                SelectCol.Width = 60;
                SelectCol.Caption = " ";

                grid.Columns.Insert(0, SelectCol);
            }
        }
    }

    private DataTable getResultados(int codigoMeta, int casasDecimais)
    {

        string where = "";

        string dataInicio = string.Format("{0:dd/MM/yyyy}", txtDe.Date);
        string dataTermino = string.Format("{0:dd/MM/yyyy}", txtFim.Date);
        string acompanhaMeta = string.Empty;

        string cmdGetMto = string.Format(@"
        SELECT DataInicioValidadeMeta, DataTerminoValidadeMeta, IndicaAcompanhaMetaVigencia 
          FROM MetaOperacional 
         WHERE CodigoProjeto = {0} and CodigoMetaOperacional = {1}", codigoProjeto, codigoMeta);

        DataSet dsGetMto = cDados.getDataSet(cmdGetMto);

        if (cDados.DataSetOk(dsGetMto) && cDados.DataTableOk(dsGetMto.Tables[0]))
        {
            dataInicio = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dsGetMto.Tables[0].Rows[0]["DataInicioValidadeMeta"].ToString() == string.Empty ? "01/01/1900" : dsGetMto.Tables[0].Rows[0]["DataInicioValidadeMeta"]);
            dataTermino = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dsGetMto.Tables[0].Rows[0]["DataTerminoValidadeMeta"].ToString() == string.Empty ? "31/12/2078" : dsGetMto.Tables[0].Rows[0]["DataTerminoValidadeMeta"]);
        }
        string where1 = string.Format(@"   AND CONVERT(DateTime, '01/' + Convert(Varchar,_Mes) + '/' +  Convert(Varchar,_Ano), 103) between @DataInicio and @DataTermino ");


        bool podeEditarMesesBloqueados = false;

        podeEditarMesesBloqueados = cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_AltResBlq");

        dtResultados = cDados.getResultadosIndicadorProjeto(codigoEntidade, codigoMeta, dataInicio, dataTermino, casasDecimais, podeEditarMesesBloqueados, where1).Tables[0];

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

    }

    protected void gvResultados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int count = 0;

        //DataTable dtResult = cDados.getResultadosIndicador(codigoEntidade, codigoIndicador, casasDecimais, "AND _CodigoDado = " + e.Keys[0]).Tables[0];

        foreach (DataRow dr in dtResultados.Rows)
        {
            if (dr["Editavel"].ToString() == "S")
            {
                int codigoMeta = int.Parse(e.Keys[0].ToString());

                //string data = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["Data"].ToString()));
                string data = ((DateTime)dr["Data"]).ToString("dd/MM/yyyy");

                if (e.NewValues[count + 1] != null && e.NewValues[count + 1].ToString() != "")
                {
                    string valor = e.NewValues[count + 1].ToString();

                    cDados.atualizaResultadoIndicadorProjeto(codigoMeta, data, valor, idUsuarioLogado);
                }
                else
                {
                    cDados.excluiResultadosProjeto(codigoMeta, data);
                }
            }
            count++;
        }
        carregaGridResultados(gvResultados);

        e.Cancel = true;

        gvResultados.CancelEdit();
    }

    protected void gvResultados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        try
        {
            if (e.Column.Index >= 2)
            {
                if (dtResultados != null && dtResultados.Rows.Count > e.Column.Index - 2 && dtResultados.Rows[e.Column.Index - 2]["Editavel"].ToString() != "S")
                {
                    if((e.Editor as ASPxTextBox) != null)
                    {
                        (e.Editor as ASPxTextBox).ClientEnabled = false;
                        (e.Editor as ASPxTextBox).DisabledStyle.BackColor = Color.Transparent;
                        (e.Editor as ASPxTextBox).DisabledStyle.ForeColor = Color.Black;
                        (e.Editor as ASPxTextBox).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                    }
                    else
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

    protected void pnCallbackPeriodos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        populaGrid();

        txtDe.Date = DateTime.Now.Date;
        txtFim.Date = DateTime.Now.Date.AddDays(qtdDias);

        txtFim.MaxDate = DateTime.Now.Date.AddDays(qtdDias);
        txtFim.MinDate = DateTime.Now.Date;

        gvDados.JSProperties["cp_Inicio"] = txtDe.Text;
        gvDados.JSProperties["cp_Termino"] = txtFim.Text;
        gvDados.JSProperties["cp_dias"] = qtdDias.ToString();
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (!podeEditar)
        {
            if (e.ButtonID == "imgEditar")
            {
                //e.IsVisible = DevExpress.Utils.DefaultBoolean.False;
                e.Enabled = false;
                e.Image.Url = "../../imagens/botoes/editarRegDes.png";
            }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AtlResPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "AtlResPrj", "Resultados de Projeto", this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }
}
