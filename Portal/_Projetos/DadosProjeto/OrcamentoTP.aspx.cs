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

public partial class OrcamentoTP : System.Web.UI.Page
{
    dados cDados;
    DataTable dtFluxoCaixa;
    int codigoProjetoSelecionado, codigoEntidadeUsuarioResponsavel;
    bool podeEditar = true;
    double totalGeralAnos = 0;
    
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

        cDados.aplicaEstiloVisual(Page);

        ddlPrevisao.JSProperties["cp_CodigoProjeto"] = "-1";

        if (Request.QueryString["CP"] != null)
        {
            string temp = Request.QueryString["CP"].ToString();
            if (temp != "")
            {
                ddlPrevisao.JSProperties["cp_CodigoProjeto"] = temp;
                cDados.verificaPermissaoProjetoInativo(int.Parse(temp), ref podeEditar, ref podeEditar, ref podeEditar);
                cDados.setInfoSistema("CodigoProjeto", int.Parse(temp));
            }
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

        codigoProjetoSelecionado = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaComboPrevisoes();

        carregaGrid();        

        if (grid.Columns.Count > 0 && (grid.Columns[0] is GridViewCommandColumn))
        {
            /*GridViewCommandColumn SelectCol = grid.Columns[0] as GridViewCommandColumn;
            SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            SelectCol.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            SelectCol.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";*/
            grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
            grid.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.png";
            grid.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.png";

        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);

        grid.Settings.VerticalScrollableHeight = altura - 135;
    }


    private void carregaGrid()
    {
        totalGeralAnos = 0;
        
        grid.Columns.Clear();

        grid.AutoGenerateColumns = true;

        DataTable dtGrid = getFluxo();

        //dtGrid.DefaultView.Sort = "GrupoConta Desc";

        grid.DataSource = dtGrid;

        grid.DataBind();

        grid.GroupSummary.Clear();

        if (grid.Columns.Count > 0)
        {

            ((GridViewDataTextColumn)grid.Columns[0]).Caption = "Centro de Custo";
            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            //((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
            grid.Columns[0].Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).FooterTemplate = new MyDescricaoTotalTemplate();
            ((GridViewDataTextColumn)grid.Columns[0]).GroupFooterCellStyle.Font.Bold = true;
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            ASPxSummaryItem totalDesc = new ASPxSummaryItem("DescricaoConta", DevExpress.Data.SummaryItemType.None);
            totalDesc.ShowInGroupFooterColumn = "DescricaoConta";
            grid.GroupSummary.Add(totalDesc);
            
            string[] fieldNames = new string[grid.Columns.Count - 6];

            for (int i = 1; i < grid.Columns.Count - 5; i++)
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
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).Settings.AllowGroup = DevExpress.Utils.DefaultBoolean.False;

                double varTotal = getLinhaTotal(fieldNames[i]);

                totalGeralAnos += varTotal;

                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).FooterTemplate = new MyTotalTemplate(varTotal);
                ((GridViewDataSpinEditColumn)grid.Columns[indexColuna]).GroupFooterCellStyle.Font.Bold = true;
                ASPxSummaryItem sum = new ASPxSummaryItem(fieldNames[i], DevExpress.Data.SummaryItemType.Sum);
                sum.DisplayFormat = "N2";
                sum.ShowInGroupFooterColumn = fieldNames[i];
                grid.GroupSummary.Add(sum);
            }

            ((GridViewDataTextColumn)grid.Columns["CodigoReservadoGrupoConta"]).Caption = "CR";
            ((GridViewDataTextColumn)grid.Columns["CodigoReservadoGrupoConta"]).Width = 100;
            ((GridViewDataTextColumn)grid.Columns["CodigoReservadoGrupoConta"]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns["CodigoReservadoGrupoConta"]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns["CodigoReservadoGrupoConta"]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns["CodigoReservadoGrupoConta"]).PropertiesTextEdit.Style.Font.Bold = true;


            ((GridViewDataTextColumn)grid.Columns["DescricaoContaNivel0"]).GroupIndex = 0;
            ((GridViewDataTextColumn)grid.Columns["DescricaoContaNivel0"]).Caption = "Diretoria";
            ((GridViewDataTextColumn)grid.Columns["DescricaoContaNivel0"]).SortAscending();
            ((GridViewDataTextColumn)grid.Columns["DescricaoContaNivel1"]).GroupIndex = 1;
            ((GridViewDataTextColumn)grid.Columns["DescricaoContaNivel1"]).Caption = "Área";
            ((GridViewDataTextColumn)grid.Columns["DescricaoContaNivel1"]).SortAscending();

            grid.Columns["Codigo"].Visible = false;

            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).Caption = "Total";
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).Width = 150;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).PropertiesTextEdit.DisplayFormatString = "N2";
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).PropertiesTextEdit.Style.HorizontalAlign = HorizontalAlign.Right;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).CellStyle.Font.Bold = true;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).CellStyle.HorizontalAlign = HorizontalAlign.Right;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).FooterTemplate = new MyTotalTemplate(totalGeralAnos);
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).FooterCellStyle.HorizontalAlign = HorizontalAlign.Right;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).GroupFooterCellStyle.Font.Bold = true;
            ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).GroupFooterCellStyle.HorizontalAlign = HorizontalAlign.Right;

            ASPxSummaryItem totalGrupo = new ASPxSummaryItem("ValorTotal", DevExpress.Data.SummaryItemType.Sum);
            totalGrupo.DisplayFormat = "N2";
            totalGrupo.ShowInGroupFooterColumn = "ValorTotal";
            grid.GroupSummary.Add(totalGrupo);

            grid.DataBind();

            if (!(grid.Columns[0] is GridViewCommandColumn))
            {
                GridViewCommandColumn SelectCol = new GridViewCommandColumn();
                SelectCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
                SelectCol.FixedStyle = GridViewColumnFixedStyle.Left;

                SelectCol.ShowEditButton= true;
                
                /*SelectCol.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                SelectCol.EditButton.Image.AlternateText = "Editar";
                SelectCol.EditButton.Text = "Editar";*/

                grid.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                grid.SettingsCommandButton.EditButton.Image.AlternateText = "Editar";
                grid.SettingsCommandButton.EditButton.Text = "Editar";

                SelectCol.ShowUpdateButton= true;
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

                ((GridViewDataTextColumn)grid.Columns["ValorTotal"]).VisibleIndex = grid.Columns.Count - 1;
            }
        }

        grid.ExpandAll();        
    }

    private DataTable getFluxo()
    {
        int codigoPrevisao = ddlPrevisao.SelectedIndex != -1 ? int.Parse(ddlPrevisao.Value.ToString()) : -1;
        dtFluxoCaixa = getFluxoCaixa2(codigoEntidadeUsuarioResponsavel, codigoProjetoSelecionado, codigoPrevisao).Tables[0];
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

        dtNovoFluxo.Columns.Add("Codigo");
        dtNovoFluxo.Columns.Add("DescricaoContaNivel0");
        dtNovoFluxo.Columns.Add("DescricaoContaNivel1");
        dtNovoFluxo.Columns.Add("CodigoReservadoGrupoConta");
        dtNovoFluxo.Columns.Add("ValorTotal");

        DataSet dsListaDados = getContasAnaliticasFluxoCaixaEntidade2(codigoEntidadeUsuarioResponsavel, "");

        foreach (DataRow dr in dsListaDados.Tables[0].Rows)
        {
            DataRow drLinha = getLinha(dtFluxoCaixa, dtNovoFluxo, dr["CodigoConta"].ToString(), dr["DescricaoContaNivel0"].ToString(), dr["DescricaoContaNivel1"].ToString(), dr["CodigoReservadoGrupoConta"].ToString());

            dtNovoFluxo.Rows.Add(drLinha);
        };

        return dtNovoFluxo;
    }

    /// <summary>
    /// Função para devolver, no formato Teles Pires, o result de contas para previsão orçamentária. 
    /// </summary>
    /// <param name="codigoEntidade"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public DataSet getContasAnaliticasFluxoCaixaEntidade2(int codigoEntidade, string where)
    {
        DataSet ds;
        string comandoSQL = string.Format(@"SELECT pc.CodigoConta, pc2.DescricaoConta AS DescricaoContaNivel0
                    , pc1.DescricaoConta AS DescricaoContaNivel1, pc.CodigoReservadoGrupoConta
                FROM {0}.{1}.PlanoContasFluxoCaixa pc
		            left join {0}.{1}.PlanoContasFluxoCaixa pc1 on pc1.CodigoConta = pc.CodigoContaSuperior
		            left join {0}.{1}.PlanoContasFluxoCaixa pc2 on pc2.CodigoConta = pc1.CodigoContaSuperior
                WHERE pc.CodigoEntidade = {2}
                AND pc.IndicaContaAnalitica = 'S'
                    {3}
            ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, where);
        ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    public DataSet getFluxoCaixa2(int codEntidade, int codProjeto, int codigoPrevisao)
    {
        DataSet ds;
        string comandoSQL = string.Format(@"EXEC {0}.{1}.p_GetFluxoCaixaProjeto2 {2}, {3}, {4}
            ", cDados.getDbName(), cDados.getDbOwner(), codEntidade, codProjeto, codigoPrevisao);
        ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    private DataRow getLinha(DataTable dtFluxo, DataTable dtNovoFluxo, string codigoConta, string diretoria, string area, string codigoReservado)
    {
        int i = 1;

        DataRow drLinha = dtNovoFluxo.NewRow();

        foreach (DataRow dr in dtFluxo.Rows)
        {
            if (dr["_CodigoConta"].ToString().Trim() == codigoConta)
            {
                if (i == 1)
                {
                    drLinha[0] = dr["DescricaoConta"].ToString();
                }

                if(dr["Valor"].ToString() != "")
                    drLinha[i] = dr["Valor"].ToString();
                i++;
            }
        }

        DataRow[] drs = dtFluxoCaixa.Select("_CodigoConta = " + codigoConta);

        DataTable dtAux = new DataTable();

        dtAux = dtFluxoCaixa.Clone();

        foreach (DataRow dr in drs)
            dtAux.ImportRow(dr);

        object sumObject = dtAux.Compute("Sum(Valor)", "");

        drLinha[i] = codigoConta;
        drLinha[i + 1] = diretoria;
        drLinha[i + 2] = area;
        drLinha[i + 3] = codigoReservado;
        drLinha[i + 4] = sumObject;

        return drLinha;
    }

    public double getLinhaTotal(string periodo)
    {        
        double valorTotal = 0;

        foreach (DataRow dr in dtFluxoCaixa.Select("Periodo='" + periodo + "'"))
        {            
           if (dr["Valor"].ToString() != "")
                    valorTotal = valorTotal + double.Parse(dr["Valor"].ToString());           
        }

        return valorTotal;
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int codigoPrevisao = ddlPrevisao.SelectedIndex != -1 ? int.Parse(ddlPrevisao.Value.ToString()) : -1;
         
        string msg = "";

        DataRow[] drs = dtFluxoCaixa.Select("_CodigoConta = " + e.Keys[0].ToString());

        string[] array = new string[e.NewValues.Count - 2];

        for (int i = 0; i < e.NewValues.Count - 2; i++)
        {
            array[i] = e.NewValues[i + 2] == null ? "" : e.NewValues[i + 2].ToString();
        }

        cDados.atualizaFluxoCaixa(codigoProjetoSelecionado, drs, array, codigoPrevisao, ref msg);

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
        if (e.Column.FieldName == "Codigo")
        {
            ASPxTextBox txt = e.Editor as ASPxTextBox;

            DataRow[] drs = dtFluxoCaixa.Select("_CodigoConta = " + e.KeyValue);

            DataTable dtAux = dtFluxoCaixa.Clone();

            foreach (DataRow dr in drs)
                dtAux.ImportRow(dr);

            object sumObject = dtAux.Compute("Sum(Valor)", "");

            txt.Text = string.Format("{0:n2}", sumObject);

        }else
        if (e.Column.Index >= 2 && e.Column.Index < grid.Columns.Count - 1)
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
            myLiteral.Text = "Total Geral:";

            container.Controls.Add(myLiteral);
        }
    }
    protected void grid_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if (e.Item.FieldName == "DescricaoConta")
        {
            object gridGetRowValues = grid.GetRowValues(e.VisibleIndex, "DescricaoConta");
            if (gridGetRowValues != null)
            {
                e.Text = "Total " + gridGetRowValues.ToString();
            }
        }
    }

    private void carregaComboPrevisoes()
    {
        DataSet ds = cDados.getPrevisoesOrcamentarias(codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds))
        {
            ddlPrevisao.DataSource = ds;
            ddlPrevisao.TextField = "DescricaoPrevisao";
            ddlPrevisao.ValueField = "CodigoPrevisao";

            ddlPrevisao.DataBind();

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow drOficial = ds.Tables[0].Select("IndicaPrevisaoOficial = 'S'")[0];

                ddlPrevisao.Value = drOficial["CodigoPrevisao"].ToString();
            }
        }
    }

    protected void grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ValorTotal")
        {            
            if(e.CellValue != null && e.CellValue.ToString() != "")
                e.Cell.Text = string.Format("{0:n2}", double.Parse(e.CellValue.ToString()));

            if (e.Cell.Text == "")
                e.Cell.Text = "&nbsp;";
        }
    }
}

