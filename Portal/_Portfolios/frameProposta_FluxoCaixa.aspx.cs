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

public partial class _Portfolios_frameProposta_FluxoCaixa : System.Web.UI.Page
{
    dados cDados;
    DataTable dtFluxoCaixa;
    int codigoProjetoSelecionado, codigoEntidadeUsuarioResponsavel;
    bool podeEditar = true;
    string entradaSaida = "";
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

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

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

        if (Request.QueryString["ES"] != null && Request.QueryString["ES"].ToString() != "")
        {
            entradaSaida = Request.QueryString["ES"].ToString();
        }


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

        if (grid.Columns.Count > 0 && (grid.Columns[0] is GridViewCommandColumn))
        {
            GridViewCommandColumn SelectCol = grid.Columns[0] as GridViewCommandColumn;

            /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            SelectCol.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            SelectCol.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";*/
            grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            grid.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            grid.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";

        }

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

        grid.Settings.VerticalScrollableHeight = altura - 230;
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

            ((GridViewDataTextColumn)grid.Columns[0]).Caption = "Descrição da Conta";
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            grid.Columns[0].Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).FooterTemplate = new MyDescricaoTotalTemplate();
            ((GridViewDataTextColumn)grid.Columns[0]).GroupFooterCellStyle.Font.Bold = true;
            ASPxSummaryItem totalDesc = new ASPxSummaryItem("DescricaoConta", DevExpress.Data.SummaryItemType.Sum);
            totalDesc.ShowInGroupFooterColumn = "DescricaoConta";
            grid.GroupSummary.Add(totalDesc);
            ((GridViewDataTextColumn)grid.Columns[0]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            string[] fieldNames = new string[grid.Columns.Count - 2];

            for (int i = 1; i < grid.Columns.Count - 1; i++)
            {
                fieldNames[i - 1] = ((GridViewDataTextColumn)grid.Columns[i]).FieldName;
                ((GridViewDataTextColumn)grid.Columns[i]).Visible = false;
            }

            for (int i = 0; i < fieldNames.Length - 1; i++)
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

            ((GridViewDataTextColumn)grid.Columns["IndicaEntradaSaida"]).GroupIndex = 0;
            ((GridViewDataTextColumn)grid.Columns["IndicaEntradaSaida"]).Caption = "Tipo";
            ((GridViewDataTextColumn)grid.Columns["IndicaEntradaSaida"]).SortAscending();
            grid.Columns["Codigo"].Visible = false;
            grid.Columns["GrupoConta"].Visible = false;

            grid.DataBind();

            if (!(grid.Columns[0] is GridViewCommandColumn))
            {
                GridViewCommandColumn SelectCol = new GridViewCommandColumn();
                SelectCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
                SelectCol.FixedStyle = GridViewColumnFixedStyle.Left;

                SelectCol.ShowEditButton = true;
                /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                SelectCol.EditButton.Image.AlternateText = "Editar";
                SelectCol.EditButton.Text = "Alterar";*/
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

                if (((cDados.getInfoSistema("DesabilitarBotoes") != null && cDados.getInfoSistema("DesabilitarBotoes").ToString() == "S")) ||
                       ((Request.QueryString["RO"] != null) && (Request.QueryString["RO"] == "S")) || podeEditar == false)
                {
                    SelectCol.VisibleIndex = -1;
                    SelectCol.Visible = false;
                }
                else
                {
                    SelectCol.VisibleIndex = 0;
                    SelectCol.Visible = true;
                }

                SelectCol.Width = 55;
                SelectCol.Caption = " ";

                grid.Columns.Insert(0, SelectCol);
            }

        }

        grid.ExpandAll();

        grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        grid.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
    }

    private DataTable getFluxo()
    {
        dtFluxoCaixa = cDados.getFluxoCaixa(codigoEntidadeUsuarioResponsavel, codigoProjetoSelecionado).Tables[0];
        DataTable dtNovoFluxo = new DataTable();

        int ano = 0, mes = 0;

        dtNovoFluxo.Columns.Add("DescricaoConta");

        foreach (DataRow dr in dtFluxoCaixa.Rows)
        {
            if (ano != int.Parse(dr["_Ano"].ToString()) || mes != int.Parse(dr["_Mes"].ToString()))
            {
                dtNovoFluxo.Columns.Add(dr["Periodo"].ToString(), Type.GetType("System.Double"));
                ano = int.Parse(dr["_Ano"].ToString());
                mes = int.Parse(dr["_Mes"].ToString());
            }
        }

        dtNovoFluxo.Columns.Add("TotalConta");
        dtNovoFluxo.Columns.Add("Codigo");
        dtNovoFluxo.Columns.Add("IndicaEntradaSaida");
        dtNovoFluxo.Columns.Add("GrupoConta");

        DataSet dsListaDados = cDados.getContasAnaliticasFluxoCaixaEntidade(codigoEntidadeUsuarioResponsavel, "");

        foreach (DataRow dr in dsListaDados.Tables[0].Rows)
        {
            DataRow drLinha = getLinha(dtFluxoCaixa, dtNovoFluxo, dr["CodigoConta"].ToString(), dr["EntradaSaida"].ToString(), dr["CodigoReservadoGrupoConta"].ToString());

            dtNovoFluxo.Rows.Add(drLinha);
        };

        return dtNovoFluxo;
    }

    private DataRow getLinha(DataTable dtFluxo, DataTable dtNovoFluxo, string codigoConta, string indicaEntradaSaida, string codigoReservado)
    {
        int i = 1;
        float soma = 0;
        DataRow drLinha = dtNovoFluxo.NewRow();

        foreach (DataRow dr in dtFluxo.Rows)
        {
            if (dr["_CodigoConta"].ToString().Trim() == codigoConta)
            {
                if (i == 1)
                {
                    drLinha[0] = dr["DescricaoConta"].ToString() + (codigoReservado != "" ? " (" + codigoReservado + ")" : "");
                }

                if (dr["Valor"].ToString() != "")
                {
                    drLinha[i] = dr["Valor"].ToString();
                    soma += float.Parse(dr["Valor"].ToString());
                }

                i++;
            }
        }

        drLinha[i] = soma;
        drLinha[i + 1] = codigoConta;
        drLinha[i + 2] = indicaEntradaSaida;
        drLinha[i + 3] = codigoReservado;
        return drLinha;
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

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string msg = "";

        DataRow[] drs = dtFluxoCaixa.Select("_CodigoConta = " + e.Keys[0].ToString());

        string[] array = new string[e.NewValues.Count - 1];

        for (int i = 0; i < e.NewValues.Count - 1; i++)
        {
            array[i] = e.NewValues[i + 1] == null ? "" : e.NewValues[i + 1].ToString();
        }

        cDados.atualizaFluxoCaixa(codigoProjetoSelecionado, drs, array, 1, ref msg);

        carregaGrid();

        e.Cancel = true;

        grid.CancelEdit();
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.Row.Cells.Count > 0 && e.RowType == GridViewRowType.Data)
        {
            if (e.Row.Cells.Count >= 1)
                e.Row.Cells[1].Style.Add("background-color", "#EBEBEB");

            e.Row.Cells[0].Style.Add("background-color", "#FFFFFF");
        }
    }

    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName != "TotalConta" && e.Column.Index >= 2 && e.Column.Index < grid.Columns.Count - 1)
        {
            if (dtFluxoCaixa != null)
            {
                if ((e.Editor as ASPxSpinEdit) != null)
                {
                    if (dtFluxoCaixa.Rows.Count > e.Column.Index - 2)
                    {
                        string codigo = grid.GetRowValues(e.VisibleIndex, "Codigo").ToString();

                        string indicaEditavel = dtFluxoCaixa.Select("_CodigoConta=" + codigo + " AND Periodo='" + e.Column.FieldName + "'")[0]["Editavel"].ToString();

                        if (indicaEditavel != "S")
                        {
                            (e.Editor as ASPxSpinEdit).ClientEnabled = false;
                            (e.Editor as ASPxSpinEdit).DisabledStyle.BackColor = Color.Transparent;
                            (e.Editor as ASPxSpinEdit).DisabledStyle.ForeColor = Color.Black;
                            (e.Editor as ASPxSpinEdit).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                        }
                    }

                    (e.Editor as ASPxSpinEdit).Font.Name = "Verdana";
                    (e.Editor as ASPxSpinEdit).Font.Size = new FontUnit("8pt");
                }
            }
        }
        else
        {
            if ((e.Editor as ASPxSpinEdit) != null)
            {
                (e.Editor as ASPxSpinEdit).ClientEnabled = false;
                (e.Editor as ASPxSpinEdit).DisabledStyle.BackColor = Color.Transparent;
                (e.Editor as ASPxSpinEdit).DisabledStyle.ForeColor = Color.Black;
                (e.Editor as ASPxSpinEdit).DisabledStyle.Border.BorderStyle = BorderStyle.None;
                (e.Editor as ASPxSpinEdit).Font.Name = "Verdana";
                (e.Editor as ASPxSpinEdit).Font.Size = new FontUnit("8pt");
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

    class MyDescricaoTotalTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            GridViewFooterCellTemplateContainer gridContainer = (GridViewFooterCellTemplateContainer)container;

            Literal myLiteral = new Literal();
            myLiteral.Text = "Fluxo de Caixa:";

            container.Controls.Add(myLiteral);
        }
    }
    protected void grid_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if (e.IsGroupSummary && e.Item.FieldName == "DescricaoConta")
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
}

