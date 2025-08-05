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
using DevExpress.Utils;

public partial class _Portfolios_frameProposta_FluxoCaixa : System.Web.UI.Page
{
    dados cDados;
    DataTable dtFluxoCaixa;
    int codigoProjetoSelecionado, codigoEntidadeUsuarioResponsavel;
    bool podeEditar = true;
    bool existeParticipe = false;
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

       
       
        if (Request.QueryString["CP"] != null)
        {
            string temp = Request.QueryString["CP"].ToString();
            if (temp != "")
            {
                cDados.verificaPermissaoProjetoInativo(int.Parse(temp), ref podeEditar, ref podeEditar, ref podeEditar);
                cDados.setInfoSistema("CodigoProjeto", int.Parse(temp));
            }
        }

        if (Request.QueryString["PopUp"] != null && Request.QueryString["PopUp"].ToString() == "S")
            btnFechar.ClientVisible = true;

        if (Request.QueryString["AT"] != null && Request.QueryString["AT"].ToString() != "")
        {
            grid.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["AT"].ToString());
        }
        else
        {
            string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }

        codigoProjetoSelecionado = cDados.getInfoSistema("CodigoProjeto") == null ? -1 : int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        carregaGrid();
        //if (!IsPostBack)
        cDados.aplicaEstiloVisual(Page);

        grid.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        grid.Settings.ShowFilterRow = false;
        grid.SettingsEditing.BatchEditSettings.KeepChangesOnCallbacks = DefaultBoolean.False;
        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);

        tbBotoes.Style.Add("padding", "3px");

        tbBotoes.Style.Add("border-collapse", "collapse");

        tbBotoes.Style.Add("border-bottom", "none");

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);

        grid.Settings.VerticalScrollableHeight = altura - 200;
    }


    private void carregaGrid()
    {
        grid.Columns.Clear();

        grid.AutoGenerateColumns = true;

        DataTable dtGrid = getFluxo();

        dtGrid.DefaultView.Sort = "GrupoConta Desc";

        grid.DataSource = dtGrid;

        grid.DataBind();

        grid.GroupSummary.Clear();

        if (grid.Columns.Count > 0)
        {

            ((GridViewDataTextColumn)grid.Columns[0]).Caption = "Conta";
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Name = "Verdana";
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Size = 8;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            //((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
            grid.Columns[0].Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).GroupFooterCellStyle.Font.Bold = true;
            if (existeParticipe)
            {
                ASPxSummaryItem totalDesc = new ASPxSummaryItem("DescricaoConta", DevExpress.Data.SummaryItemType.Sum);
                totalDesc.ShowInGroupFooterColumn = "DescricaoConta";
                grid.GroupSummary.Add(totalDesc);
            }
            else
            {
                ((GridViewDataTextColumn)grid.Columns[0]).FooterTemplate = new MyDescricaoTotalFooterTemplate();
                ((GridViewDataTextColumn)grid.Columns[0]).GroupFooterTemplate = new MyDescricaoGroupFooterTemplate();
            }

            ((GridViewDataTextColumn)grid.Columns[1]).Caption = "Partícipe";
            ((GridViewDataTextColumn)grid.Columns[1]).FixedStyle = GridViewColumnFixedStyle.Left;
            ((GridViewDataTextColumn)grid.Columns[1]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Font.Name = "Verdana";
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Font.Bold = true;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Font.Size = 8;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            //((GridViewDataTextColumn)grid.Columns[1]).Caption = " ";
            grid.Columns[1].Width = 300;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Width = 300;
            ((GridViewDataTextColumn)grid.Columns[1]).GroupFooterCellStyle.Font.Bold = true;

            if (existeParticipe)
            {
                ((GridViewDataTextColumn)grid.Columns[1]).GroupFooterTemplate = new MyDescricaoGroupFooterTemplate();
                ((GridViewDataTextColumn)grid.Columns[1]).FooterTemplate = new MyDescricaoTotalFooterTemplate();
            }

            GridViewDataTextColumn auxColumn = ((GridViewDataTextColumn)grid.Columns["IndicaEntradaSaida"]);
            auxColumn.GroupIndex = 0;
            auxColumn.Caption = "Tipo";
            auxColumn.SortDescending();

            if (existeParticipe)
            {
                // coloca a coluna descrição no grupo
                ((GridViewDataTextColumn)grid.Columns[0]).GroupIndex = 1;
            }
            else
            {
                // se não existe partícipe, oculta a coluna
                ((GridViewDataTextColumn)grid.Columns[1]).Visible = false;
            }

            for (int i = 3; i < grid.Columns.Count; i++)
            {
                ((GridViewDataTextColumn)grid.Columns[i]).Visible = false;
            }

            string[] fieldNames = new string[grid.Columns.Count - 7];

            for (int i = 0; i < grid.Columns.Count - 7; i++)
            {
                fieldNames[i] = ((GridViewDataTextColumn)grid.Columns[i + 7]).FieldName;
                ((GridViewDataTextColumn)grid.Columns[i + 7]).Visible = false;
            }

            for (int i = 0; i < fieldNames.Length; i++)
            {
                int indexColuna = i + 7;

                GridViewDataSpinEditColumn coluna = new GridViewDataSpinEditColumn();
                coluna.FieldName = fieldNames[i];

                grid.Columns.Insert(indexColuna, coluna);

                grid.Columns[indexColuna].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Width = 120;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.Font.Size = 8;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Width = new Unit("100%");
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DecimalPlaces = 2;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.DisplayFormatString = "N2";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.Visible = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullDisplayText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.NullText = "";
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                grid.Columns[indexColuna].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Settings.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).FooterTemplate = new MyTotalTemplate(getLinhaTotal(fieldNames[i]));
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).GroupFooterCellStyle.Font.Bold = true;
                ASPxSummaryItem sum = new ASPxSummaryItem(fieldNames[i], DevExpress.Data.SummaryItemType.Sum);
                sum.DisplayFormat = "N2";
                sum.ShowInGroupFooterColumn = fieldNames[i];
                grid.GroupSummary.Add(sum);
            }

            //grid.DataBind();
        }

        grid.Settings.ShowFilterRow = false;
        grid.SettingsBehavior.AllowSort = false;
        grid.SettingsEditing.Mode = podeEditar ? GridViewEditingMode.Batch : GridViewEditingMode.PopupEditForm;

        grid.ExpandAll();
    }

    private DataTable getFluxo()
    {
        dtFluxoCaixa = cDados.getFluxoCaixa(codigoEntidadeUsuarioResponsavel, codigoProjetoSelecionado).Tables[0];
        DataTable dtNovoFluxo = new DataTable();
        DataRow drLinha;

        int i, ano = 0, mes = 0;
        string codigoContaControle, codigoContaTemp;
        string codigoParticipeControle, codigoParticipeTemp;
        bool ehNovoRegistro = false;

        dtNovoFluxo.Columns.Add("DescricaoConta");
        dtNovoFluxo.Columns.Add("NomePessoaParticipe");
        dtNovoFluxo.Columns.Add("IndicaEntradaSaida");
        dtNovoFluxo.Columns.Add("GrupoConta");
        dtNovoFluxo.Columns.Add("_CodigoConta");
        dtNovoFluxo.Columns.Add("CodigoPessoaParticipe");
        dtNovoFluxo.Columns.Add("Editavel");

        foreach (DataRow dr in dtFluxoCaixa.Select("", "_Ano, _Mes"))
        {
            if (ano != int.Parse(dr["_Ano"].ToString()) || mes != int.Parse(dr["_Mes"].ToString()))
            {
                dtNovoFluxo.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                ano = int.Parse(dr["_Ano"].ToString());
                mes = int.Parse(dr["_Mes"].ToString());
            }

            if (!existeParticipe)
            {
                if (dr["NomePessoaParticipe"].ToString() != "")
                {
                    existeParticipe = true;
                }
            }
        }

        codigoContaControle = "0";
        codigoParticipeControle = "0";
        i = 0;
        drLinha = null;

        foreach (DataRow dr in dtFluxoCaixa.Select("", "_CodigoConta, CodigoPessoaParticipe, _Ano, _Mes"))
        {
            codigoContaTemp = dr["_CodigoConta"].ToString();
            codigoParticipeTemp = dr["CodigoPessoaParticipe"].ToString();

            if ((codigoContaControle != codigoContaTemp) || (codigoParticipeControle != codigoParticipeTemp))
            {
                drLinha = dtNovoFluxo.NewRow();
                dtNovoFluxo.Rows.Add(drLinha);
                drLinha[0] = string.Format("{0} ({1})", dr["DescricaoConta"].ToString(), dr["CodigoReservadoGrupoConta"].ToString());
                drLinha[1] = dr["NomePessoaParticipe"].ToString();
                drLinha[2] = dr["IndicaEntradaSaida"].ToString() == "E" ? "Receitas" : "Despesas";
                drLinha[3] = dr["CodigoReservadoGrupoConta"].ToString();
                drLinha[4] = dr["_CodigoConta"].ToString();
                drLinha[5] = dr["CodigoPessoaParticipe"].ToString();
                drLinha[6] = dr["Editavel"].ToString();
                i = 7;
                codigoContaControle = codigoContaTemp;
                codigoParticipeControle = codigoParticipeTemp;
            }

            if (dr["Valor"].ToString() != "")
                drLinha[i] = dr["Valor"].ToString();

            i++;
        };

        return dtNovoFluxo;
    }

    public double getLinhaTotal(string periodo)
    {
        double valorTotal = 0;

        foreach (DataRow dr in dtFluxoCaixa.Select("Periodo='" + periodo + "'"))
        {
            if (dr["IndicaEntradaSaida"].ToString() == "E")
            {
                if (dr["Valor"].ToString() != "")
                    valorTotal = valorTotal + double.Parse(dr["Valor"].ToString());
            }
            else
            {
                if (dr["Valor"].ToString() != "")
                    valorTotal = valorTotal - double.Parse(dr["Valor"].ToString());
            }
        }

        return valorTotal;
    }

  

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.Row.Cells.Count > 0 && e.RowType == GridViewRowType.Data)
        {
            if (e.Row.Cells.Count >= 1)
            {
                e.Row.Cells[1].Style.Add("background-color", "#EBEBEB");
                // se há partícipes, coloca a coluna partícipes em cor diferente também
                if (existeParticipe)
                {
                    e.Row.Cells[2].Style.Add("background-color", "#EBEBEB");
                }
            }
            e.Row.Cells[0].Style.Add("background-color", "#FFFFFF");
        }
        //else if (e.Row.Cells.Count > 0 && e.RowType == GridViewRowType.GroupFooter)
        //{
        //    if (e.Row.Cells.Count >= 1)
        //    {
        //        if (existeParticipe)
        //            e.Row.Cells[2].Text = "Total : ";
        //        else
        //            e.Row.Cells[1].Text = "Total : ";
        //    }
        //}
    }


    protected void grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Index >= 7 && e.VisibleIndex > -1)
        {
            if (dtFluxoCaixa != null)
            {
                string codigoConta = grid.GetRowValues(e.VisibleIndex, "_CodigoConta").ToString();
                string codigoParticipe = grid.GetRowValues(e.VisibleIndex, "CodigoPessoaParticipe").ToString();
                string filtro;

                if (existeParticipe)
                {
                    if (codigoParticipe == "")
                    {
                        filtro = string.Format("_CodigoConta = {0} AND CodigoPessoaParticipe IS NULL AND Periodo = '{1}'", codigoConta, e.DataColumn.FieldName);
                    }
                    else
                    {
                        filtro = string.Format("_CodigoConta = {0} AND CodigoPessoaParticipe = {1} AND Periodo = '{2}'", codigoConta, codigoParticipe, e.DataColumn.FieldName);
                    }
                }
                else
                {
                    filtro = string.Format("_CodigoConta = {0} AND Periodo = '{1}'", codigoConta, e.DataColumn.FieldName);
                }

                DataRow[] dr = dtFluxoCaixa.Select(filtro);

                hfGeral.Set(e.DataColumn.FieldName + "_" + e.VisibleIndex, !podeEditar ? "N" : dr[0]["Editavel"].ToString());

                if (dr[0]["Editavel"].ToString() != "S")
                {
                    e.Cell.BackColor = Color.FromName("#C4C4C4");
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.ToolTip = "Célula não editável";
                }
                else if (podeEditar)
                {
                    e.Cell.BackColor = Color.White;
                    e.Cell.ForeColor = Color.Black;
                    e.Cell.ToolTip = "Clique para editar";
                }
            }
        }

    }
    class MyTotalTemplate : ITemplate
    {
        double valorTotal = 0;

        public MyTotalTemplate(double valor)
        {
            valorTotal = valor;
        }

        public void InstantiateIn(Control container)
        {
            GridViewFooterCellTemplateContainer gridContainer = (GridViewFooterCellTemplateContainer)container;

            Literal myLiteral = new Literal();
            myLiteral.Text = string.Format("{0:n2}", valorTotal);

            container.Controls.Add(myLiteral);
        }
    }

    class MyDescricaoGroupFooterTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            GridViewGroupFooterCellTemplateContainer gridContainer = (GridViewGroupFooterCellTemplateContainer)container;
            GridViewColumn colunaAgrupada = gridContainer.GroupedColumn;
            ASPxGridView grid1 = (ASPxGridView)gridContainer.Grid;

            Literal myLiteral = new Literal();
            if ((colunaAgrupada == grid1.Columns["DescricaoConta"]) || (colunaAgrupada == grid1.Columns["NomePessoaParticipe"]))
            {
                myLiteral.Text = "Total Conta: " + grid1.GetRowValues(gridContainer.ItemIndex, "DescricaoConta");
            }
            else if ((colunaAgrupada == grid1.Columns["IndicaEntradaSaida"]))
            {
                myLiteral.Text = "Total " + grid1.GetRowValues(gridContainer.ItemIndex, "IndicaEntradaSaida").ToString().ToUpper();
            }

            container.Controls.Add(myLiteral);
        }
    }

    class MyDescricaoTotalFooterTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            GridViewFooterCellTemplateContainer gridContainer = (GridViewFooterCellTemplateContainer)container;

            Literal myLiteral = new Literal();
            myLiteral.Text = "FLUXO DE CAIXA";

            container.Controls.Add(myLiteral);
        }
    }

    protected void grid_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if (e.Item.FieldName == "DescricaoConta" || e.Item.FieldName == "NomePessoaParticipe")
        {
            e.Text = "Total:";
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
        cDados.eventoClickMenuSemTemplate((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PrevOrc", grid);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "PrevOrc", "Previsão Orçamentária", this);
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

    protected void grid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        string codigoConta;
        string codigoParticipe;
        string filtro;
        for (int i = 0; i < e.UpdateValues.Count; i++)
        {
            codigoConta = e.UpdateValues[i].Keys[0].ToString();
            codigoParticipe = e.UpdateValues[i].Keys[1].ToString();

            if (existeParticipe)
            {
                if (codigoParticipe == "")
                {
                    filtro = string.Format("_CodigoConta = {0} AND CodigoPessoaParticipe IS NULL", codigoConta);
                }
                else
                {
                    filtro = string.Format("_CodigoConta = {0} AND CodigoPessoaParticipe = {1}", codigoConta, codigoParticipe);
                }
            }
            else
            {
                filtro = string.Format("_CodigoConta = {0}", codigoConta);
            }
            DataRow[] drs = dtFluxoCaixa.Select(filtro);

            object[] keys = new object[e.UpdateValues[i].NewValues.Keys.Count];
            e.UpdateValues[i].NewValues.Keys.CopyTo(keys, 0);
            for (int j = 0; j < e.UpdateValues[i].NewValues.Count; j++)
            {
                string fieldName = keys[j].ToString();
                if ((fieldName != "DescricaoConta") && (fieldName != "NomePessoaParticipe"))
                {
                    DataRow[] dr = dtFluxoCaixa.Select(filtro + " AND Periodo = '" + fieldName + "'");

                    if (dr[0]["Editavel"].ToString() == "S")
                    {
                        int mes = int.Parse(dr[0]["_Mes"].ToString());
                        int ano = int.Parse(dr[0]["_Ano"].ToString());

                        string valor = (e.UpdateValues[i].NewValues[j] != null && e.UpdateValues[i].NewValues[j].ToString() != "") ? e.UpdateValues[i].NewValues[j].ToString() : "NULL";

                        if (dr[0]["CodigoPessoaParticipe"].ToString() == "")
                        {
                            if (existeParticipe)
                            {
                                codigoParticipe = "-1";
                            }
                            else
                            {
                                codigoParticipe = "NULL";
                            }
                        }
                        else
                        {
                            codigoParticipe = dr[0]["CodigoPessoaParticipe"].ToString();
                        }

                        string comandoSQL = string.Format(@"
                        EXEC {0}.{1}.p_gravaRegistroPrevisaoOrcamentariaProjeto {2}, '{3}', {4}, {5}, {6}, {7}, {8}, {9};"
                        , cDados.getDbName()
                        , cDados.getDbOwner()
                        , codigoProjetoSelecionado
                        , "P"
                        , codigoConta
                        , codigoParticipe
                        , dr[0]["CodigoPeriodicidade"].ToString() == "" ? "NULL" : dr[0]["CodigoPeriodicidade"].ToString()
                        , ano
                        , mes
                        , valor.Replace(",", "."));

                        int regAf = 0;
                        bool retorno = cDados.execSQL(comandoSQL, ref regAf);
                    }
                }
            }
        }

        e.Handled = true;
        carregaGrid();
    }
}

