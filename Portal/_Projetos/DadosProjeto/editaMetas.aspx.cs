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
using System.Globalization;

public partial class _Estrategias_wizard_editaMetas : System.Web.UI.Page
{
    dados cDados;
    DataTable dtMetas;

    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoMeta = -1;
    private int codigoIndicador = -1;
    private int codigoProjeto = -1;
    private int casasDecimais = 0;

    private string unidadeMedida = "";

    public bool podeEditar = true;

    string periodicidade = "";
    int qtdDias = 0;
    public string mostraTDPeriodo = "";
    string iniciaisAgrupamento = "";

    string valorMetaBanco = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["CodigoMeta"] != null && Request.QueryString["CodigoMeta"].ToString() != "")
            codigoMeta = int.Parse(Request.QueryString["CodigoMeta"].ToString());

        if (Request.QueryString["CodigoIndicador"] != null && Request.QueryString["CodigoIndicador"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CodigoIndicador"].ToString());

        if (Request.QueryString["CodigoProjeto"] != null && Request.QueryString["CodigoProjeto"].ToString() != "")
            codigoProjeto = int.Parse(Request.QueryString["CodigoProjeto"].ToString());

        if (Request.QueryString["CasasDecimais"] != null)
            casasDecimais = int.Parse(Request.QueryString["CasasDecimais"].ToString());

        if (Request.QueryString["SiglaUnidadeMedida"] != null)
            unidadeMedida = Request.QueryString["SiglaUnidadeMedida"].ToString();

        if (Request.QueryString["Periodicidade"] != null)
            periodicidade = Request.QueryString["Periodicidade"].ToString();

        if(periodicidade != "Semanal" && periodicidade != "Diária")
            mostraTDPeriodo = "display:none;";

        if (txtDe.Value == null)
        {
            if (periodicidade == "Diária")
                qtdDias = 15;
            else if (periodicidade == "Semanal")
                qtdDias = 30;

            txtDe.Date = DateTime.Now.Date;
            txtFim.Date = DateTime.Now.Date.AddDays(qtdDias);

            txtFim.MaxDate = DateTime.Now.Date.AddDays(qtdDias);
            txtFim.MinDate = DateTime.Now.Date;
        }

        montaCampos();

        gvMetas.JSProperties["cp_MetaBanco"] = "NULL";

        carregaGridMetas(gvMetas);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/MetasDesempenhoProjeto.js""></script>"));
        this.TH(this.TS("MetasDesempenhoProjeto"));
        gvMetas.JSProperties["cp_Atualiza"] = "N";

        gvMetas.Settings.ShowFilterRow = false;
        gvMetas.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvMetas.SettingsBehavior.AllowSort = false;
    }

    private void montaCampos()
    {
        txtIndicadorDado.Text = cDados.getNomeIndicadorOperacional(codigoIndicador);
        double metaNumerica = 0, metaInformada = 0, minimo = 0, maximo = 0, media = 0, quantidadeMetas = 0, ultimaMeta = 0;
        string nomeAgrupamento = "";

        cDados.getValoresMetaIndicadorProjeto(codigoMeta, codigoIndicador, codigoProjeto, codigoEntidade, ref metaNumerica, ref metaInformada, ref minimo, ref maximo, ref media, ref quantidadeMetas, ref ultimaMeta, ref nomeAgrupamento, ref iniciaisAgrupamento);
        txtMetaNumerica.Text = metaNumerica.ToString();

        CultureInfo cultureinfo = System.Threading.Thread.CurrentThread.CurrentCulture;

        if(nomeAgrupamento != "")
        lblTituloMeta.Text = nomeAgrupamento.Substring(0, 1).ToUpper() + nomeAgrupamento.Substring(1).ToLower() + ":";

        switch (iniciaisAgrupamento.ToUpper())
        {
            case "SUM": txtMetaInformada.Text = metaInformada.ToString();
                break;
            case "AVG": txtMetaInformada.Text = media.ToString();
                break;
            case "MIN": txtMetaInformada.Text = minimo.ToString();
                break;
            case "MAX": txtMetaInformada.Text = maximo.ToString();
                break;
            case "STT": txtMetaInformada.Text = ultimaMeta.ToString();
                break;
            default: txtMetaInformada.Text = "";
                break;
        }

        valorMetaBanco = txtMetaInformada.Text;

        //gvMetas.JSProperties["cp_MetaNumerica"] = metaNumerica;
        //gvMetas.JSProperties["cp_Soma"] = metaInformada;
        //gvMetas.JSProperties["cp_Minimo"] = minimo;
        //gvMetas.JSProperties["cp_Maximo"] = maximo;
        //gvMetas.JSProperties["cp_Media"] = media;
        //gvMetas.JSProperties["cp_Quantidade"] = quantidadeMetas;
    }

    #region GvMetas

    private void carregaGridMetas(ASPxGridView grid)
    {
        
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
            grid.DataSource = getMetas(casasDecimais);
            grid.DataBind();

            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns[0].Width = 140;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = new Unit("100%");
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.ClientSideEvents.Validation = @"function(s, e) { var mensagemRetornoErro = verificaMetasMesesAnteriores();
                                                                                                                         
                                                                                                                          if(mensagemRetornoErro != '')
                                                                                                                          {
                                                                                                                              window.top.mostraMensagem(mensagemRetornoErro, 'erro', true, false, null);
                                                                                                                                 e.isValid = false;
                                                                                                                          }
                                                                                                                          else if(txtMetaNumerica.GetText() == '' && txtMetaInformada.GetText() != '')
                                                                                                                          {
                                                                                                                                window.top.mostraMensagem('Informe uma meta válida!', 'Atencao', true, false, null);
                                                                                                                                 e.isValid = false;
                                                                                                                           } else if(txtMetaInformada.GetText() != '' && parseFloat(txtMetaNumerica.GetText().replace('.', '').replace(',', '.')).toFixed(2) != parseFloat(txtMetaInformada.GetText().replace('.', '').replace(',', '.')).toFixed(2))
                                                                                                                            {
                                                                                                                                window.top.mostraMensagem('O desdobramento das metas deve coincidir com a meta numérica informada!', 'Atencao', true, false, null);
                                                                                                                                e.isValid = false;
                                                                                                                            }
                                                                                                                        }";

            string[] fieldNames = new string[grid.Columns.Count - 2];

            for (int i = 2; i < grid.Columns.Count; i++)
            {
                fieldNames[i - 2] = ((GridViewDataTextColumn)grid.Columns[i]).FieldName;
                ((GridViewDataTextColumn)grid.Columns[i]).Visible = false;
            }

            ((GridViewDataTextColumn)grid.Columns[1]).Visible = false;

            GridViewDataSpinEditColumn coluna = new GridViewDataSpinEditColumn();
            coluna.FieldName = "_MetaNumerica";

            grid.Columns.Insert(1, coluna);

            ((GridViewDataSpinEditColumn)grid.Columns[1]).Caption = "Meta Numérica";
            grid.Columns[1].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;

            string funcaoMeta = @"txtMetaNumerica.SetText(s.GetValue() == null ? '' : s.GetValue().toString().replace('.', ','));";

           
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.ClientSideEvents.ValueChanged = "function(s, e) {" + funcaoMeta + "}";

            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.EncodeHtml = false;
            if (unidadeMedida == "%")
            {
                ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.DisplayFormatString = "{0:n" + casasDecimais + "}" + unidadeMedida;
            }
            else
            {
                if (unidadeMedida.Contains("$"))
                {
                    ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.DisplayFormatString = unidadeMedida + "{0:n" + casasDecimais + "}";
                }
                else
                {
                    ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";

                }
            }

            ((GridViewDataSpinEditColumn)grid.Columns[1]).Width = 120;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.Style.Font.Name = "Verdana";
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.Style.Font.Size = 8;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.Width = new Unit("100%");
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.DecimalPlaces = casasDecimais;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.DisplayFormatInEditMode = true;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.SpinButtons.Visible = false;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.NumberType = casasDecimais == 0 ? SpinEditNumberType.Integer : SpinEditNumberType.Float;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.NullDisplayText = "";
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.NullText = "";
            ((GridViewDataSpinEditColumn)grid.Columns[1]).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
            ((GridViewDataSpinEditColumn)grid.Columns[1]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
            grid.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Right;

            

            for (int i = 0; i < fieldNames.Length; i++)
            {
                int indexColuna = i + 2;

                coluna = new GridViewDataSpinEditColumn();
                coluna.FieldName = fieldNames[i];

                grid.Columns.Insert(indexColuna, coluna);

                grid.Columns[indexColuna].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;

                funcaoMeta = @"";

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

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 120;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Size = 8;
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

            grid.Columns["_CodigoMeta"].Visible = false;

            grid.DataBind();

            if (!(grid.Columns[0] is GridViewCommandColumn))
            {
                GridViewCommandColumn SelectCol = new GridViewCommandColumn();
                SelectCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
                SelectCol.FixedStyle = GridViewColumnFixedStyle.Left;
                SelectCol.Caption = " ";
                
                SelectCol.ShowEditButton= true;

                /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                SelectCol.EditButton.Image.AlternateText = "Editar";
                SelectCol.EditButton.Text = "Editar";*/

                grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                grid.SettingsCommandButton.EditButton.Image.AlternateText = "Editar";
                grid.SettingsCommandButton.EditButton.Text = "Editar"; 


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


                SelectCol.Width = 80;

                grid.Columns.Insert(0, SelectCol);            
        }
    }    

    private DataTable getMetas(int casasDecimais)
    {
        if (periodicidade == "Diária")
            qtdDias = 15;
        else if (periodicidade == "Semanal")
            qtdDias = 30;

        string dataInicio = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", txtDe.Date);
        string dataTermino = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", txtFim.Date);
        string acompanhaMeta = string.Empty;

        string cmdGetMto = string.Format(@"
        SELECT DataInicioValidadeMeta, DataTerminoValidadeMeta, IndicaAcompanhaMetaVigencia 
          FROM MetaOperacional 
         WHERE CodigoProjeto = {0}
           AND CodigoIndicador = {1}
           AND CodigoMetaOperacional = {2}", codigoProjeto, codigoIndicador, codigoMeta);

        DataSet dsGetMto = cDados.getDataSet(cmdGetMto);

        if (cDados.DataSetOk(dsGetMto) && cDados.DataTableOk(dsGetMto.Tables[0]))
        {
            dataInicio = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dsGetMto.Tables[0].Rows[0]["DataInicioValidadeMeta"].ToString() == string.Empty ? "01/01/1900" : dsGetMto.Tables[0].Rows[0]["DataInicioValidadeMeta"]);
            dataTermino = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", dsGetMto.Tables[0].Rows[0]["DataTerminoValidadeMeta"].ToString() == string.Empty ? "31/12/2078" : dsGetMto.Tables[0].Rows[0]["DataTerminoValidadeMeta"]);
        }

        string where1 = string.Format(@"   AND CONVERT(DateTime, '01/' + Convert(Varchar,_Mes) + '/' +  Convert(Varchar,_Ano), 103) between @DataInicio and @DataTermino ");

        dtMetas = cDados.getMetasProjetoAtualizacao(codigoEntidade, codigoMeta, codigoIndicador, dataInicio, dataTermino, codigoProjeto, casasDecimais, where1).Tables[0];
        DataTable dtNovasMetas = new DataTable();

        string data = "";

        dtNovasMetas.Columns.Add("_NomeIndicador");
        dtNovasMetas.Columns.Add("_MetaNumerica", Type.GetType("System.Double"));

        foreach (DataRow dr in dtMetas.Rows)
        {
            if (data != dr["Data"].ToString())
            {
                dtNovasMetas.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                data = dr["Data"].ToString();
            }
        }

        dtNovasMetas.Columns.Add("_CodigoMeta");

        DataRow drLinha = getLinha(dtMetas, dtNovasMetas, codigoMeta);

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
                if (int.Parse(dr["_CodigoMeta"].ToString()) == codigo)
                {
                    if (i == 1)
                    {
                        drLinha[0] = "Metas do Indicador:";
                        drLinha[1] = txtMetaNumerica.Text;
                    }

                    if (dr["Valor"].ToString() != "")
                    {
                        drLinha[i + 1] = dr["Valor"].ToString();
                    }
                    int ano = DateTime.Parse(dr["Data"].ToString()).Year;
                    arrayValores += (dr["Valor"].ToString() + ";");
                    arrayAnos += ( ano + ";");
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
            drLinha[i + 1] = codigo;
        }
        catch { }

        return drLinha;
    }

    protected void gvMetas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int count = 0;
        
        
            string valorMeta = e.NewValues[1] != null && e.NewValues[1].ToString() != "" ? e.NewValues[1].ToString() : "NULL";
            cDados.atualizaValorMetaPrenchida(codigoMeta.ToString(), valorMeta, idUsuarioLogado.ToString());
        

        foreach (DataRow dr in dtMetas.Rows)
        {
            string data = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["Data"].ToString()));            

            if (e.NewValues[count + 2] != null && e.NewValues[count + 2].ToString() != "")
            {
                string valor = e.NewValues[count + 2].ToString();

                cDados.atualizaMetaIndicadorProjeto(codigoMeta, data, valor, idUsuarioLogado);
            }
            else
            {
                cDados.excluiMetasProjeto(codigoMeta, data);
            }

            count++;
        }

        montaCampos();

        carregaGridMetas(gvMetas);

        gvMetas.JSProperties["cp_Atualiza"] = "S";

        e.Cancel = true;

        gvMetas.CancelEdit();
    }

    protected void gvMetas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        try
        {
            if (e.Column.Index >= 3)
            {
                if (dtMetas != null && dtMetas.Rows.Count > e.Column.Index - 3 && dtMetas.Rows[e.Column.Index - 3]["Editavel"].ToString() != "S")
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

    protected void gvMetas_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        gvMetas.JSProperties["cp_MetaBanco"] = valorMetaBanco;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AtlMetPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "AtlMetPrj", "Desdobramento das Metas de Projeto", this);
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
