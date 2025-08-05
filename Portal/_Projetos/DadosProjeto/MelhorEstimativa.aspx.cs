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
            }

            codigoProjetoSelecionado = int.Parse(temp);
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
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);

        grid.Settings.VerticalScrollableHeight = altura - 105;
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
            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;

            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)grid.Columns[0]).Caption = " ";
            grid.Columns[0].Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Width = 300;
            ((GridViewDataTextColumn)grid.Columns[0]).FooterTemplate = new MyDescricaoTotalTemplate();
            ((GridViewDataTextColumn)grid.Columns[0]).GroupFooterCellStyle.Font.Bold = true;
            ASPxSummaryItem totalDesc = new ASPxSummaryItem("DescricaoConta", DevExpress.Data.SummaryItemType.Sum);
            totalDesc.ShowInGroupFooterColumn = "DescricaoConta";
            grid.GroupSummary.Add(totalDesc);
            
            string[] fieldNames = new string[grid.Columns.Count - 3];

            for (int i = 1; i < grid.Columns.Count - 2; i++)
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
            ((GridViewDataTextColumn)grid.Columns["IndicaEntradaSaida"]).SortDescending();
            grid.Columns["Codigo"].Visible = false;
            grid.Columns["GrupoConta"].Visible = false;

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
            }

        }

        grid.ExpandAll();        
    }

    private DataTable getFluxo()
    {
        dtFluxoCaixa = getMelhorEstimativa(codigoEntidadeUsuarioResponsavel, codigoProjetoSelecionado).Tables[0];
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

    public DataSet getMelhorEstimativa(int codEntidade, int codProjeto)
    {
        DataSet ds;
        string comandoSQL = string.Format(@"EXEC {0}.{1}.p_GetMelhorEstimativaProjeto {2}, {3}
            ", cDados.getDbName(), cDados.getDbOwner(), codEntidade, codProjeto);
        ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    private DataRow getLinha(DataTable dtFluxo, DataTable dtNovoFluxo, string codigoConta, string indicaEntradaSaida, string codigoReservado)
    {
        int i = 1;

        DataRow drLinha = dtNovoFluxo.NewRow();

        foreach (DataRow dr in dtFluxo.Rows)
        {
            if (dr["_CodigoConta"].ToString().Trim() == codigoConta)
            {
                if (i == 1)
                {
                    drLinha[0] = dr["DescricaoConta"].ToString() + (codigoReservado != "" ? " (" + codigoReservado + ")" : "");
                }

                if(dr["ValorTendencia"].ToString() != "")
                    drLinha[i] = dr["ValorTendencia"].ToString();
                i++;
            }
        }

        drLinha[i] = codigoConta;
        drLinha[i + 1] = indicaEntradaSaida;
        drLinha[i + 2] = codigoReservado;

        return drLinha;
    }

    public double getLinhaTotal(string periodo)
    {        
        double valorTotal = 0;

        foreach (DataRow dr in dtFluxoCaixa.Select("Periodo='" + periodo + "'"))
        {            
            if (dr["IndicaEntradaSaida"].ToString() == "E")
            {
                if (dr["ValorTendencia"].ToString() != "")
                    valorTotal = valorTotal + double.Parse(dr["ValorTendencia"].ToString());
            }
            else
            {
                if (dr["ValorTendencia"].ToString() != "")
                    valorTotal = valorTotal - double.Parse(dr["ValorTendencia"].ToString());
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

        atualizaMelhorEstimativa(codigoProjetoSelecionado, drs, array, 1, ref msg);

        carregaGrid();

        e.Cancel = true;

        grid.CancelEdit();
    }

    public bool atualizaMelhorEstimativa(int codigoProjeto, DataRow[] drs, string[] valores, int codigoPrevisao, ref string msg)
    {
        int regAfetados = 0;
        try
        {
            int i = 0;

            string comandoSQL = "";

            foreach (DataRow dr in drs)
            {
                comandoSQL += string.Format(@"                                             
                                         IF EXISTS(SELECT 1 FROM {0}.{1}.FluxoCaixaProjeto WHERE CodigoProjeto = {3}
                                                                                           AND CodigoConta = {4}
                                                                                           AND Ano = {5}
                                                                                           AND Mes = {6}
                                                                                           AND CodigoPrevisao = {7})
                                            BEGIN
                                                 UPDATE {0}.{1}.FluxoCaixaProjeto SET ValorTendencia = {2}
                                                  WHERE CodigoProjeto = {3}
                                                    AND CodigoConta = {4}
                                                    AND Ano = {5}
                                                    AND Mes = {6}
                                                    AND CodigoPrevisao = {7}
                                            END
                                        ELSE
                                            BEGIN
                                                 INSERT INTO {0}.{1}.FluxoCaixaProjeto(CodigoProjeto, CodigoConta, Ano, Mes, ValorTendencia, CodigoPrevisao) VALUES
                                                                              ({3}, {4}, {5}, {6}, {2}, {7})
                                                END
                                        "
                    , cDados.getDbName(), cDados.getDbOwner(), valores[i] == "" ? "NULL" : valores[i].ToString().Replace(",", "."), codigoProjeto, dr["_CodigoConta"], dr["_Ano"], dr["_Mes"], codigoPrevisao);

                i++;
            }

            if (comandoSQL != "")
               cDados.execSQL(comandoSQL, ref regAfetados);
            return true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return false;
        }
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
                        int ano = int.Parse(dtFluxoCaixa.Select("_CodigoConta=" + codigo + " AND Periodo='" + e.Column.FieldName + "'")[0]["_Ano"].ToString());
                        int mes = int.Parse(dtFluxoCaixa.Select("_CodigoConta=" + codigo + " AND Periodo='" + e.Column.FieldName + "'")[0]["_Mes"].ToString());

                        if (indicaEditavel == "S")
                        {
                            if (ano < DateTime.Now.Year)
                                indicaEditavel = "N";
                            else if (ano == DateTime.Now.Year && mes < DateTime.Now.Month)
                                indicaEditavel = "N";
                        }

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
}

