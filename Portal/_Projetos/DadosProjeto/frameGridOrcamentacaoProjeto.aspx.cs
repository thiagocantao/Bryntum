using System;
using DevExpress.Web;
using System.Data;
using System.Drawing;
using Cdis.Brisk.DataTransfer.Orcamentacao;
using System.Collections.Generic;
using System.Globalization;
using Cdis.Brisk.Infra.Core.Extensions;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;
using Cdis.Brisk.Application.Applications.StoredProcedure;
using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Service.Services.Projeto;
using Cdis.Brisk.Domain.Generic;
using System.Collections.Specialized;

public partial class _Projetos_frameGridOrcamentacaoProjeto : BasePageBrisk
{
    private int _codProjeto;
    private int? _codigoWorkflow;
    private int? _codigoInstanciaWf;
    private short? _anoRelatorio;
    private short _codigoPrevisao;
    private bool _isPermiteEditar;
    public bool isSemProjeto;
    public string msgSemProjeto;
    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();
        CDados.aplicaEstiloVisual(Page);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _codProjeto = Request.QueryString["CP"] == null ? 0 : int.Parse(Request.QueryString["CP"].ToString());
        isSemProjeto = (_codProjeto == 0);
        msgSemProjeto = Resources.traducao.Antes_de_elaborar_o_orcamento_cadastre_os_dados_do_projeto_atividade;

        if (_codProjeto != 0)
        {
            _codigoWorkflow = string.IsNullOrEmpty(Request.QueryString["CWF"]) ? (int?)null : int.Parse(Request.QueryString["CWF"]);
            _codigoInstanciaWf = string.IsNullOrEmpty(Request.QueryString["CIWF"]) ? (int?)null : int.Parse(Request.QueryString["CIWF"]);
            _isPermiteEditar = string.IsNullOrEmpty(Request.QueryString["RO"]) ? true : (Request.QueryString["RO"] == "N");
            CDados.setaTamanhoMaximoMemo(memoMemoriaCalculo, 4000, lblContadorMemo);
            LoadGrid();

            //string comandosql = string.Format(@"SELECT TerminoPeriodoElaboracao 
            //                                      FROM PrevisaoFluxoCaixaProjeto 
            //                                     WHERE CodigoEntidade = {0} and AnoOrcamento = YEAR(getdate())", UsuarioLogado.CodigoEntidade);

            //DataSet ds = CDados.getDataSet(comandosql);
            //if(CDados.DataSetOk(ds) && CDados.DataTableOk(ds.Tables[0]))
            //{
            //    DateTime terminoPeriodoElaboracao = DateTime.Parse(ds.Tables[0].Rows[0]["TerminoPeriodoElaboracao"].ToString());

            //    imagemFiltroProjetoDespesa.ClientVisible = terminoPeriodoElaboracao >= DateTime.Now;
            //    imagemFiltroProjetoReceita.ClientVisible = terminoPeriodoElaboracao >= DateTime.Now;
            //    lblEscolherContas1.ClientVisible = terminoPeriodoElaboracao >= DateTime.Now;
            //    lblEscolherContas2.ClientVisible = terminoPeriodoElaboracao >= DateTime.Now;

            //}

            //imagemFiltroProjetoDespesa.JSProperties["cpAtivo"] = 

            //select TerminoPeriodoElaboracao from PrevisaoFluxoCaixaProjeto where CodigoEntidade = 352 and AnoOrcamento = YEAR(getdate())
        }

        string comandoSQLDespesa = string.Format(@" select CodigoConta, DescricaoConta from dbo.f_Sescoop_GetContasAssociarOrcamento({0}, 'S')", UsuarioLogado.CodigoEntidade);
        DataSet dsComboDespesa = CDados.getDataSet(comandoSQLDespesa);
        
        comboEscolheContaDespesa.TextField = "DescricaoConta";
        comboEscolheContaDespesa.ValueField = "CodigoConta";
        comboEscolheContaDespesa.DataSource = dsComboDespesa;
        comboEscolheContaDespesa.DataBind();

        string comandoSQLReceita = string.Format(@" select CodigoConta, DescricaoConta from dbo.f_Sescoop_GetContasAssociarOrcamento({0}, 'E')", UsuarioLogado.CodigoEntidade);
        DataSet dsComboReceita = CDados.getDataSet(comandoSQLReceita);

        comboEscolheContaReceita.TextField = "DescricaoConta";
        comboEscolheContaReceita.ValueField = "CodigoConta";
        comboEscolheContaReceita.DataSource = dsComboReceita;
        comboEscolheContaReceita.DataBind();

    }

    private void LoadGrid()
    {
        ParamPGetOrcamentacaoProjetoDomain param = new ParamPGetOrcamentacaoProjetoDomain
        {
            CodigoEntidade = UsuarioLogado.CodigoEntidade,
            CodigoProjeto = _codProjeto,
            CodigoWorkflow = _codigoWorkflow,
            CodigoInstanciaWF = _codigoInstanciaWf
        };

        var orcamentacaoDespesa = UowApplication.GetUowApplication<PGetOrcamentacaoProjetoApplication>().GetDataTableDespesaOrcamentacaoProjeto(param);
        _anoRelatorio = orcamentacaoDespesa.NumAno;
        _codigoPrevisao = orcamentacaoDespesa.CodigoPrevisao;

        //monta a grid de despesas
        DataTable dataTableDespesa = orcamentacaoDespesa.SourceDataTableOrcamentacao;
        gridDespesa.DataSource = dataTableDespesa;
        gridDespesa.DataBind();
        AtualizarNomeColuna(gridDespesa);


        var orcamentacaoReceita = UowApplication.GetUowApplication<PGetOrcamentacaoProjetoApplication>().GetDataTableReceitaOrcamentacaoProjeto(param);
        // se o código de previsão ainda não foi determinado (situação em que o projeto tem apenas receitas no orçamento)
        if (0 == _codigoPrevisao)
        {
            _codigoPrevisao = orcamentacaoReceita.CodigoPrevisao;
        }
        if ( (null == _anoRelatorio) || (0 == _anoRelatorio) )
        {
            _anoRelatorio = orcamentacaoReceita.NumAno;
        }

        //monta a grid de receitas
        DataTable dataTableReceita = orcamentacaoReceita.SourceDataTableOrcamentacao;
        gridReceita.DataSource = dataTableReceita;
        gridReceita.DataBind();
        AtualizarNomeColuna(gridReceita);

        AplicarLayout(gridDespesa);
        AplicarLayout(gridReceita);
        CreateFakeGrid();
    }

    private void CreateFakeGrid()
    {
        gridFakeDespesa.DataSource = gridDespesa.DataSource;
        AtualizarNomeColuna(gridFakeDespesa);
        gridFakeDespesa.Visible = false;

        gridFakeReceita.DataSource = gridReceita.DataSource;
        AtualizarNomeColuna(gridFakeReceita);
        gridFakeReceita.Visible = false;
        // gridFake.Columns.Remove(gridFake.Columns["CodConta"]);
        // gridFake.Columns.Remove(gridFake.Columns["ListValor"]);
    }

    private void AtualizarNomeColuna(ASPxGridView gridDespesa)
    {
        foreach (var coluna in gridDespesa.Columns)
        {
            string typeColuna = coluna.GetType().Name;
            if (typeColuna == "GridViewDataSpinEditColumn")
            {
                GridViewDataSpinEditColumn col = (GridViewDataSpinEditColumn)coluna;
                int mes;
                if (int.TryParse(col.FieldName, out mes))
                {
                    var nameCulture = CultureInfo.CurrentCulture.Name;
                    string month = new CultureInfo(nameCulture).DateTimeFormat.GetMonthName(mes);
                    col.Caption = char.ToUpper(month[0]) + month.Substring(1, 2) + "/" + _anoRelatorio;
                }
            }
        }
    }

    private void AplicarLayout(ASPxGridView grid)
    {
        grid.Settings.ShowFilterRow = false;
        grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        var resolucaoCliente = CDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        //if (altura > 0)
        //    grid.Settings.VerticalScrollableHeight = altura - 325;

        grid.SettingsCommandButton.UpdateButton.RenderMode = GridCommandButtonRenderMode.Button;
        grid.SettingsCommandButton.UpdateButton.Text = "Salvar";
        grid.SettingsCommandButton.CancelButton.RenderMode = GridCommandButtonRenderMode.Button;
        grid.SettingsCommandButton.CancelButton.Text = "Cancelar";
        grid.SettingsCommandButton.CancelButton.Styles.Style.CssClass = "marginLeft10";

        if (grid.Columns.Count > 0)
        {
            ((GridViewDataTextColumn)grid.Columns[0]).Caption = Resources.traducao.codigo;
            ((GridViewDataTextColumn)grid.Columns[0]).FixedStyle = GridViewColumnFixedStyle.Left;
            ((GridViewDataTextColumn)grid.Columns[0]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Font.Bold = true;
            ((GridViewDataTextColumn)grid.Columns[0]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            grid.Columns[0].Width = 130;

            ((GridViewDataTextColumn)grid.Columns[1]).Caption = Resources.traducao.conta;
            ((GridViewDataTextColumn)grid.Columns[1]).FixedStyle = GridViewColumnFixedStyle.Left;
            ((GridViewDataTextColumn)grid.Columns[1]).ReadOnly = true;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.BackColor = System.Drawing.Color.Transparent;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Border.BorderStyle = BorderStyle.None;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Font.Bold = true;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Style.Wrap = DevExpress.Utils.DefaultBoolean.True;
            grid.Columns[1].Width = 300;
            ((GridViewDataTextColumn)grid.Columns[1]).PropertiesTextEdit.Width = 300;
            ((GridViewDataTextColumn)grid.Columns[1]).GroupFooterCellStyle.Font.Bold = true;

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                string typeColuna = grid.Columns[i].GetType().Name;
                if (i == 1)
                {
                    GridViewDataTextColumn col = (GridViewDataTextColumn)grid.Columns[i];
                    col.FooterTemplate = new MyTotalTemplate("Total:");
                    ASPxSummaryItem sum = new ASPxSummaryItem("Conta", DevExpress.Data.SummaryItemType.None);
                    sum.ShowInGroupFooterColumn = "Conta";
                    grid.GroupSummary.Add(sum);
                }

                if (typeColuna == "GridViewDataSpinEditColumn")
                {
                    GridViewDataSpinEditColumn col = (GridViewDataSpinEditColumn)grid.Columns[i];
                    col.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    col.PropertiesSpinEdit.DecimalPlaces = 2;
                    col.PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Currency;
                    col.PropertiesSpinEdit.DisplayFormatString = "C2";
                    col.PropertiesSpinEdit.DisplayFormatInEditMode = true;
                    col.PropertiesSpinEdit.SpinButtons.Visible = false;
                    col.PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                    col.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                    col.PropertiesSpinEdit.NullDisplayText = "";
                    col.PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                    col.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    col.Width = 140;
                    col.GroupFooterCellStyle.Font.Bold = true;

                    double soma = GetSomaColunaGrid(grid, col.FieldName);
                    col.FooterTemplate = new MyTotalTemplate(soma);

                    ASPxSummaryItem sum = new ASPxSummaryItem(col.FieldName, DevExpress.Data.SummaryItemType.Sum);
                    sum.DisplayFormat = "N2";
                    sum.ShowInGroupFooterColumn = col.FieldName;
                    grid.GroupSummary.Add(sum);
                }
            }
        }
    }

    public double GetSomaColunaGrid(ASPxGridView grid, string nameColuna)
    {
        DataTable dataTableGrid = (DataTable)grid.DataSource;
        double soma = 0;
        if (dataTableGrid != null)
        {
            foreach (DataRow row in dataTableGrid.Select())
            {
                string strValor = row[nameColuna].ToString();
                double valor;
                if (double.TryParse(strValor, out valor))
                {
                    soma += valor;
                }
            }
        }

        return soma;
    }

    class MyTotalTemplate : ITemplate
    {
        private double _valor { get; set; }
        private string _texto { get; set; }
        public List<ValorOrcamentacaoDataTransfer> ListValor { get; set; }
        public MyTotalTemplate(double valor)
        {
            _valor = valor;
        }

        public MyTotalTemplate(string texto)
        {
            _texto = texto;
        }

        public void InstantiateIn(Control container)
        {
            //GridViewFooterCellTemplateContainer gridContainer = (GridViewFooterCellTemplateContainer)container;
            Literal myLiteral = new Literal();
            myLiteral.ClientIDMode = ClientIDMode.AutoID;
            if (string.IsNullOrEmpty(_texto))
            {
                var ri = new RegionInfo(System.Threading.Thread.CurrentThread.CurrentUICulture.LCID);
                myLiteral.Text = string.Format(ri.CurrencySymbol + " {0:n2}", _valor);
                container.Controls.Add(myLiteral);
            }
            else
            {
                myLiteral.Text = _texto;
                container.Controls.Add(myLiteral);
            }

        }
    }

    protected void grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.Row.Cells.Count > 0 && e.RowType == GridViewRowType.Data)
        {
            e.Row.Cells[0].Width = new Unit(100);
            e.Row.Cells[1].Width = new Unit(200);
        }
    }

    protected void gridHtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            int mes;
            List<string> listColunaFormatacaoValor = new List<string> { "TotalConta", "ValorOrcamentoAnterior", "VariacaoOrcamentoAnterior" };
            if (int.TryParse(e.DataColumn.FieldName, out mes) || listColunaFormatacaoValor.Contains(e.DataColumn.FieldName))
            {
                if (!listColunaFormatacaoValor.Contains(e.DataColumn.FieldName))
                {
                    var listValor = e.GetValue("ListValor").ToString().JsonToEntity<List<ValorOrcamentacaoDataTransfer>>();
                    bool isReadOnly = !listValor.FirstOrDefault(m => m.NumMes == mes).IsEditavel;

                    if (isReadOnly || !_isPermiteEditar)
                    {
                        if (_isPermiteEditar)
                        {
                            e.Cell.BackColor = Color.FromName("#C4C4C4");
                        }
                        e.Cell.ForeColor = Color.Black;
                        e.Cell.ToolTip = Resources.traducao.celula_nao_editavel;
                        e.Cell.Attributes.Add("noEditFieldValor", "true");
                    }
                }

                double valor;
                if (!string.IsNullOrEmpty(e.CellValue.ToString()) && double.TryParse(e.CellValue.ToString(), out valor))
                {
                    string textFormat = string.Format("{0:c2}", valor);
                    var ts = e.CellValue;
                    e.Cell.Text = textFormat;
                    e.Cell.ToolTip = Resources.traducao.clique_para_editar;
                }

            }
        }
    }

    /// <summary>
    /// Atualizar os valores das linhas modificadas na Grid
    /// </summary>    
    protected void grid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        var linhaAtualizadas = e.UpdateValues.Select(i => i.NewValues).ToList();
        List<ParamValorOrcamentarioDomain> listParam = new List<ParamValorOrcamentarioDomain>();

        foreach (var item in linhaAtualizadas)
        {
            var listValor = item["ListValor"].ToString().JsonToEntity<List<ValorOrcamentacaoDataTransfer>>();
            int codConta = int.Parse(item["CodConta"].ToString());
            short ano = listValor.FirstOrDefault().NumAno;

            for (short mes = 1; mes <= 12; mes++)
            {
                if (listValor.Any(m => m.NumMes == mes && m.IsEditavel))
                {
                    decimal? valorEdicao = item[mes.ToString()] == null ? (decimal?)null : decimal.Parse(item[mes.ToString()].ToString().Replace('.', ','));
                    listParam.Add(new ParamValorOrcamentarioDomain
                    { 
                        CodigoProjeto = _codProjeto,
                        Ano = ano,
                        Mes = mes,
                        CodigoConta = codConta,
                        ValorPrevisto = valorEdicao,
                        CodigoPrevisao = _codigoPrevisao,
                        CodigoWorkflow = _codigoWorkflow,
                        CodigoInstanciaWF = _codigoInstanciaWf
                    });
                }
            }
        }

        UowApplication.GetUowApplication<PGravaRegistroPrevisaoOrcamentariaProjetoApplication>().AtualizarValorDeContaOrcamentariaAnual(listParam);

        LoadGrid();
        e.Handled = true;
    }


    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        var re = e.Column;
        List<string> listNameCol = new List<string>() { "CodConta", "ListValor" };
        if (listNameCol.Contains(e.Column.Caption))
        {
            e.TextValue = "";
            e.Text = "";
            e.Column.Width = new Unit(0);
            //e.Column.Visible = false;
            //e.Column.SetColVisible(false);
        }
        else
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



    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClickDespesa(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        menu_ItemClickBase(source, e, ASPxGridViewExporter1, "OrcPrevDespesa");
    }

    protected void menu_InitDespesa(object sender, EventArgs e)
    {
        Menu_InitBase(sender, e, "OrcPrevDespesa", "Previsão Orçamentária Despesa");
    }

    protected void menu_ItemClickReceita(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        menu_ItemClickBase(source, e, ASPxGridViewExporter2, "OrcPrevReceita");
    }

    protected void menu_InitReceita(object sender, EventArgs e)
    {
        Menu_InitBase(sender, e, "OrcPrevReceita", "Previsão Orçamentária Receita");
    }


    protected void menu_ItemClickBase(object source, DevExpress.Web.MenuItemEventArgs e, ASPxGridViewExporter gvExporter, string iniciaisLayoutTela)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        dados cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, iniciaisLayoutTela);
    }

    private void Menu_InitBase(object sender, EventArgs e, string nomeIniciaisLayout, string tituloPagina)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        dados cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, nomeIniciaisLayout, tituloPagina, this);
    }

    #endregion

    protected void callbackMemoriaCalculo_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_sucesso"] = "";
        ((ASPxCallback)source).JSProperties["cp_erro"] = "";
        string[] parametros = e.Parameter.Split('|');

        int? codigoConta = int.Parse(parametros[0] == "" ? null : parametros[0]);
        int codigoMemoriaCalculoAUX = int.Parse(parametros[1] == "" ? "0" : parametros[1]);
        int? codigoMemoriaCalculo = codigoMemoriaCalculoAUX == 0 ? (int?)null : codigoMemoriaCalculoAUX;


        var test = UowApplication.UowService.GetUowService<MemoriaCalculoService>().AtualizarDescricaoMemoriaCalculo(memoMemoriaCalculo.Text, codigoMemoriaCalculo, _codProjeto, _codigoPrevisao, codigoConta);
        if (test.IsSuccess == true)
        {
            ((ASPxCallback)source).JSProperties["cp_sucesso"] = test.Message;
        }
        else
        {
            ((ASPxCallback)source).JSProperties["cp_erro"] = test.Message;
        }
    }

    protected void checkValoresPreenchidos_CheckedChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }

    protected void gridDespesa_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            if (e.ButtonID == "btnMemoriaCalculoDespesa")
            {
                if (e.VisibleIndex > -1)
                {
                    string jan = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "1").ToString();
                    string fev = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "2").ToString();
                    string mar = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "3").ToString();
                    string abr = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "4").ToString();
                    string mai = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "5").ToString();
                    string jun = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "6").ToString();
                    string jul = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "7").ToString();
                    string ago = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "8").ToString();
                    string set = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "9").ToString();
                    string outu = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "10").ToString();
                    string nov = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "11").ToString();
                    string dez = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "12").ToString();

                    bool mostra = !string.IsNullOrEmpty(jan) || !string.IsNullOrEmpty(fev) || !string.IsNullOrEmpty(mar) || !string.IsNullOrEmpty(abr) || !string.IsNullOrEmpty(mai) ||
                   !string.IsNullOrEmpty(jun) || !string.IsNullOrEmpty(jul) || !string.IsNullOrEmpty(ago) || !string.IsNullOrEmpty(set) || !string.IsNullOrEmpty(outu) ||
                   !string.IsNullOrEmpty(nov) || !string.IsNullOrEmpty(dez);

                    e.Visible = mostra ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
    }

    protected void gridReceita_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.CellType == GridViewTableCommandCellType.Data)
        {
            if (e.ButtonID == "btnMemoriaCalculoReceita")
            {
                if (e.VisibleIndex > -1)
                {
                    string jan = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "1").ToString();
                    string fev = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "2").ToString();
                    string mar = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "3").ToString();
                    string abr = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "4").ToString();
                    string mai = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "5").ToString();
                    string jun = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "6").ToString();
                    string jul = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "7").ToString();
                    string ago = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "8").ToString();
                    string set = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "9").ToString();
                    string outu = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "10").ToString();
                    string nov = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "11").ToString();
                    string dez = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "12").ToString();

                    bool mostra = !string.IsNullOrEmpty(jan) || !string.IsNullOrEmpty(fev) || !string.IsNullOrEmpty(mar) || !string.IsNullOrEmpty(abr) || !string.IsNullOrEmpty(mai) ||
                   !string.IsNullOrEmpty(jun) || !string.IsNullOrEmpty(jul) || !string.IsNullOrEmpty(ago) || !string.IsNullOrEmpty(set) || !string.IsNullOrEmpty(outu) ||
                   !string.IsNullOrEmpty(nov) || !string.IsNullOrEmpty(dez);

                    e.Visible = mostra ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
    }

    protected void callbackIncluirConta_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_erro"] = "";
        string comandoSQL = "";
        int quantidaderegistrosafetados = 0;
        if (tabOrcamentacao.ActiveTabIndex == 0)
        {
            comandoSQL = string.Format(@" exec dbo.p_Sescoop_EfetuaAssociacaoContaOrcamento {0}, {1}, {2}", _codProjeto, comboEscolheContaDespesa.Value, UsuarioLogado.Id);
        }
        else
        {
            comandoSQL = string.Format(@" exec dbo.p_Sescoop_EfetuaAssociacaoContaOrcamento {0}, {1}, {2}", _codProjeto, comboEscolheContaReceita.Value, UsuarioLogado.Id);
        }

        try
        {
            CDados.execSQL(comandoSQL, ref quantidaderegistrosafetados);
            LoadGrid();
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cp_erro"] = ex.Message;
        }
    }



    protected void gridDespesa_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        LoadGrid();
    }

    protected void gridReceita_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        LoadGrid();
    }
}