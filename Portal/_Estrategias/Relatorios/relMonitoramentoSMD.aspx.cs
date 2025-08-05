using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Olap = DevExpress.Web.ASPxPivotGrid;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Web;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraPivotGrid;
using System.Text;
using System.IO;
using DevExpress.XtraPrinting;
using System.Drawing;


public partial class _Estrategias_Relatorios_relMonitoramentoSMD : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;
    private string dbName;
    private string dbOwner;
    public string alturaTabela = "";
    public string larguraTabela = "";
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeLogada;
    public int codigoLista;
    public int codigoListaUsuario;
    DsListaProcessos ds;
    DataSet dsMonitoramento;

    #region Properties

    private string _ConnectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
        set { _ConnectionString = value; }
    }

    #endregion

    #endregion

    #region Page Events

    protected void Page_Init(object sender, EventArgs e)
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeLogada, codigoEntidadeLogada, "null", "EN", 0, "null", "EN_EstRelDad");
        }

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        spAno.MaxValue = DateTime.Now.Year;
        spAno.MinValue = 1900;

        if (!Page.IsPostBack)
        {
            spAno.Value = DateTime.Now.Year;
        }
        carregaGrid((spAno.Value == null) ? DateTime.Now.Year.ToString() : spAno.Value.ToString());
        setaTitulospvGrid((spAno.Value == null) ? DateTime.Now.Year.ToString() : spAno.Value.ToString());
        ds = new DsListaProcessos();

        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());
        cDados.aplicaEstiloVisual(this);

        cDados.configuraPainelBotoesOLAP(tbBotoes);

    }

    private void carregaGrid(string ano)
    {
        string comandoSQL = string.Format(@"      
        DECLARE @RC int
        DECLARE @ano int
        set @ano = {2}
        SELECT [GrupoProduto]
              ,[valorRealOrcAno]
              ,[ValorPrevOrcAno]
              ,[AnoMesCarga]
              ,[Ano]
              ,[CodigoEntidade]
              ,[SiglaUnidadeNegocio]
              ,[Mes]
              ,[CodigoIndicador]
              ,[CodigoUnidadeNegocio]
              ,[TipoControle]
              ,[CargaHoraria]
              ,[CNAE]
              ,[CNPJ]
              ,[CR]
              ,[Industrial]
              ,[Modalidade]
              ,[Projeto]
              ,[Série]
              ,[qtdRealAno2]
              ,[qtdRealAno1]
              ,[qtdRealAno0]
              ,[qtdPrevistoAno2]
              ,[qtdPrevistoAno1]
              ,[qtdPrevistoAno0]
              ,[qtdRealPeriodoAno2]
              ,[qtdRealPeriodoAno1]
              ,[qtdRealPeriodoAno0]
              ,[qtdPrevistoPeriodoAno2]
              ,[qtdPrevistoPeriodoAno1]
              ,[qtdPrevistoPeriodoAno0]
              ,[NomeIndicador]
              ,[NomeUnidadeNegocio]
              ,[Financiamento]
              ,[Produto]
              ,[TituloFormulario]
              ,[Clientela]           
         FROM {0}.{1}.[SMD_RelatorioMonitoramento]
         WHERE [AnoReferencia] = @ano", cDados.getDbName(), cDados.getDbOwner(), ano);
        dsMonitoramento = cDados.getDataSet(comandoSQL);
        pgDados.DataSource = dsMonitoramento.Tables[0];
        pgDados.DataBind();

    }

    public void setaTitulospvGrid(string ano)
    {
        int anoo = int.Parse(ano);
        int ano0 = anoo;//ano que passou como parametro
        int ano1 = anoo - 1;
        int ano2 = anoo - 2;

        string str_periodo = cDados.classeDados.getDateDB();
        string mesAnoFormatado = str_periodo.Substring(3, 7);
        string periodo = "";
        int mes = int.Parse(mesAnoFormatado.Substring(0, 2));
        if (mes == 1)
        {
            periodo = "JAN";
        }
        else if (mes == 2)
        {
            periodo = "JAN-FEV";
        }
        else if (mes == 3)
        {
            periodo = "JAN-MAR";
        }
        else if (mes == 4)
        {
            periodo = "JAN-ABR";
        }
        else if (mes == 5)
        {
            periodo = "JAN-MAI";
        }
        else if (mes == 6)
        {
            periodo = "JAN-JUN";
        }
        else if (mes == 7)
        {
            periodo = "JAN-JUL";
        }
        else if (mes == 8)
        {
            periodo = "JAN-AGO";
        }
        else if (mes == 9)
        {
            periodo = "JAN-SET";
        }
        else if (mes == 10)
        {
            periodo = "JAN-OUT";
        }
        else if (mes == 11)
        {
            periodo = "JAN-NOV";
        }
        else if (mes == 12)
        {
            periodo = "JAN-DEZ";
        }

        pgDados.Fields["ValorPrevOrcAno"].Caption = "Orc. Previsto (R$)";
        pgDados.Fields["valorRealOrcAno"].Caption = "Orc. Realizado (R$)";


        pgDados.Fields["qtdRealAno2"].Caption = string.Format("REAL {0}", ano2);
        pgDados.Fields["qtdRealAno1"].Caption = string.Format("REAL {0}", ano1);
        pgDados.Fields["qtdRealAno0"].Caption = string.Format("REAL {0}", ano0);

        pgDados.Fields["qtdRealPeriodoAno2"].Caption = string.Format("REAL {0}, {1}", ano2, periodo);
        pgDados.Fields["qtdRealPeriodoAno1"].Caption = string.Format("REAL {0} {1}", ano1, periodo);
        //fieldqtdRealPeriodoAno0.Caption = string.Format("PLANO {0} {1}", ano0, periodo);

        //fieldqtdPrevistoAno2.Caption = string.Format("PLANO {0}", ano2);
        //fieldqtdPrevistoAno1.Caption = string.Format("PLANO {0}", ano1);
        pgDados.Fields["qtdPrevistoAno0"].Caption = string.Format("PLANO {0}", ano0);//PINTADO DE AMARELO

        //fieldqtdPrevistoPeriodoAno2.Caption = string.Format("PLANO {0} {1}", ano2, periodo);
        //fieldqtdPrevistoPeriodoAno1.Caption = string.Format("PLANO {0} {1}", ano1, periodo);
        pgDados.Fields["qtdPrevistoPeriodoAno0"].Caption = string.Format("PLANO {0} {1}", ano0, periodo);//PINTADO DE AMARELO


        //SEÇÃO DE VARIAÇÃO DO RELATÓRIO
        pgDados.Fields["fieldRealPlanoAnual"].Caption = string.Format("Real  / Plano {0}", ano0);
        pgDados.Fields["fieldRealPlano0"].Caption = string.Format("Real  / Plano {0} {1}", ano0, periodo);
        pgDados.Fields["fieldRealPlano1"].Caption = string.Format("Real {0} / Real {1} {2}", ano0, ano1, periodo);
        pgDados.Fields["fieldRealPlano2"].Caption = string.Format("Real {0} / Real {1} {2}", ano0, ano2, periodo);


        fieldRealPlanoAnual.AreaIndex = 90;
        fieldRealPlano0.AreaIndex = 91;
        fieldRealPlano1.AreaIndex = 92;
        fieldRealPlano2.AreaIndex = 93;
    }
    #endregion

    #region Eventos Menu Botões menu_ItemClick e menu_Init

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        
        cDados.eventoClickMenuOLAP(menu, parameter, exporter, this, "SMDMonitora", pgDados);
        setaTitulospvGrid((spAno.Value == null) ? DateTime.Now.Year.ToString() : spAno.Value.ToString());
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "SMDMonitora", "Monitoramento SMD", this, pgDados);
        //ajusta();
    }

    #endregion

    #region CUSTOMIZAÇÃO E ESTILIZAÇÃO DO PIVOT GRID

    protected void pgDados_CustomCellDisplayText(object sender, Olap.PivotCellDisplayTextEventArgs e)
    {
        var dados = e.CreateDrillDownDataSource();
        decimal numerador = 0;
        decimal denominador = 0;

        switch (e.DataField.ID)
        {
            case "fieldRealPlanoAnual":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdPrevistoAno0"] == null) ? 0 : (decimal)linha["qtdPrevistoAno0"];
                }
                e.DisplayText = string.Format("{0:p1}", (denominador == 0) ? 0 : (numerador / denominador));
                break;

            case "fieldRealPlano0":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdPrevistoPeriodoAno0"] == null) ? 0 : (decimal)linha["qtdPrevistoPeriodoAno0"];
                }
                e.DisplayText = string.Format("{0:p1}", (denominador == 0) ? 0 : ((numerador / denominador) - 1));
                break;

            case "fieldRealPlano1":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdRealPeriodoAno1"] == null) ? 0 : (decimal)linha["qtdRealPeriodoAno1"];
                }
                e.DisplayText = string.Format("{0:p1}", (denominador == 0) ? 0 : ((numerador / denominador) - 1));
                break;

            case "fieldRealPlano2":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdRealPeriodoAno2"] == null) ? 0 : (decimal)linha["qtdRealPeriodoAno2"];
                }
                e.DisplayText = string.Format("{0:p1}", (denominador == 0) ? 0 : ((numerador / denominador) - 1));
                break;

            default:
                break;
        }


        if (e.DisplayText == "0" || e.DisplayText == "0,0%" || e.DisplayText == "0,00")
        {
            e.DisplayText = "-";
        }
    }

    protected void pgDados_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
    {
        decimal out_Valor = 0;
        e.CellStyle.BackColor = Color.White;
        if (decimal.TryParse(e.DataField.GetDisplayText(e.Value).ToString(), out out_Valor) == true)
        {
            if (out_Valor > 0)
            {
                e.CellStyle.ForeColor = Color.Blue;
            }
            else if (out_Valor == 0)
            {
                e.CellStyle.ForeColor = Color.Black;
            }
            else if (out_Valor < 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
        if (e.RowValueType == PivotGridValueType.Total || e.RowValueType == PivotGridValueType.GrandTotal)
        {

            if (e.RowValueType == PivotGridValueType.Total)
            {
                e.CellStyle.BackColor = Color.WhiteSmoke;
            }
            else if (e.RowValueType == PivotGridValueType.GrandTotal)
            {
                e.CellStyle.BackColor = Color.Gainsboro;
            }


            e.CellStyle.ForeColor = Color.Black;
            e.CellStyle.Font.Bold = true;

            if (decimal.TryParse(e.DataField.GetDisplayText(e.Value).ToString(), out out_Valor) == true)
            {
                if (out_Valor > 0)
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
                else if (out_Valor == 0)
                {
                    e.CellStyle.ForeColor = Color.Black;
                }
                else if (out_Valor < 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }
    }

    protected void pgDados_CustomCellValue(object sender, Olap.PivotCellValueEventArgs e)
    {
        var dados = e.CreateDrillDownDataSource();
        decimal numerador = 0;
        decimal denominador = 0;

        switch (e.DataField.ID)
        {
            case "fieldRealPlanoAnual":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdPrevistoAno0"] == null) ? 0 : (decimal)linha["qtdPrevistoAno0"];
                }
                e.Value = (denominador == 0) ? 0 : (numerador / denominador);
                break;

            case "fieldRealPlano0":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdPrevistoPeriodoAno0"] == null) ? 0 : (decimal)linha["qtdPrevistoPeriodoAno0"];
                }
                e.Value = (denominador == 0) ? 0 : ((numerador / denominador) - 1);
                break;

            case "fieldRealPlano1":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdRealPeriodoAno1"] == null) ? 0 : (decimal)linha["qtdRealPeriodoAno1"];
                }
                e.Value = (denominador == 0) ? 0 : ((numerador / denominador) - 1);
                break;

            case "fieldRealPlano2":

                numerador = 0;
                denominador = 0;
                foreach (PivotDrillDownDataRow linha in dados)
                {
                    numerador += (linha["qtdRealAno0"] == null) ? 0 : (decimal)linha["qtdRealAno0"];
                    denominador += (linha["qtdRealPeriodoAno2"] == null) ? 0 : (decimal)linha["qtdRealPeriodoAno2"];
                }
                e.Value = (denominador == 0) ? 0 : ((numerador / denominador) - 1);
                break;

            default:
                break;
        }
    }

    protected void pgDados_HtmlFieldValuePrepared(object sender, PivotHtmlFieldValuePreparedEventArgs e)
    {
        if (e.IsColumn == true)
        {
            if (e.DataField.Caption != null && e.DataField.FieldName.Equals("qtdPrevistoAno0") || e.DataField.FieldName.Equals("qtdPrevistoPeriodoAno0"))
            {
                e.Cell.BackColor = Color.Yellow;
            }
            e.Cell.Font.Bold = true;
        }
        else
        {
            if (e.ValueType == PivotGridValueType.Value)
            {
                e.Cell.BackColor = Color.White;
            }
            else if (e.ValueType == PivotGridValueType.Total)
            {
                e.Cell.BackColor = Color.WhiteSmoke;
            }
            else if (e.ValueType == PivotGridValueType.GrandTotal)
            {
                e.Cell.BackColor = Color.Gainsboro;
            }
        }
    }

    #endregion

    #region CUSTOMIZAÇÃO E ESTILIZAÇÃO DA EXPORTAÇÃO

    protected void exporter_CustomExportHeader(object sender, WebCustomExportHeaderEventArgs e)
    {
        if (e.Field.Caption != null && e.Field.FieldName.Equals("qtdPrevistoAno0") || e.Field.FieldName.Equals("qtdPrevistoPeriodoAno0"))
        {
            e.Brick.BackColor = Color.Yellow;
            e.Brick.Style.TextAlignment = TextAlignment.MiddleJustify;
        }
        e.Brick.Style.Font = new Font(e.Brick.Style.Font, FontStyle.Bold);
        e.Appearance.WordWrap = true;
    }

    protected void exporter_CustomExportCell(object sender, WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.Area == PivotArea.DataArea && (e.Value != null))
            {
                if (e.RowValue.ValueType == PivotGridValueType.Total)
                {
                    e.Appearance.BackColor = Color.WhiteSmoke;
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point);
                }
                if (e.RowValue.ValueType == PivotGridValueType.GrandTotal)
                {
                    e.Appearance.BackColor = Color.Gainsboro;
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point);
                }
                decimal d = 0;
                string value = e.Value.ToString().ToLower();
                if (decimal.TryParse(value, out d) == true)
                {
                    if (d == 0)
                    {
                        e.Appearance.ForeColor = Color.Black;
                        e.Brick.Text = "-";
                        e.Brick.TextValue = "-";
                    }
                    else if (d > 0)
                    {
                        e.Appearance.ForeColor = Color.Blue;
                    }
                    else if (d < 0)
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }

                    if (e.DataField.ID == "fieldRealPlanoAnual" ||
                        e.DataField.ID == "fieldRealPlano0" ||
                        e.DataField.ID == "fieldRealPlano1" ||
                        e.DataField.ID == "fieldRealPlano2")
                    {
                        e.Brick.Text = string.Format("{0:p1}", d);
                        e.Brick.TextValue = string.Format("{0:p1}", d);
                    }
                }
            }
        }
    }

    protected void exporter_CustomExportFieldValue(object sender, WebCustomExportFieldValueEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.IsColumn == true)
            {

                if ((e.DataField.FieldName.Contains("qtd") ||
                e.DataField.FieldName.Contains("Orc") ||
                e.DataField.FieldName.Equals("")))
                {
                    e.Brick.Sides = BorderSide.All;
                    e.Brick.BorderStyle = BrickBorderStyle.Outset;
                    e.Brick.BorderWidth = (float)7;
                    e.Appearance.Font = new Font("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point);
                }
                if (e.DataField.FieldName.Equals("qtdPrevistoAno0"))
                {
                    e.Brick.BackColor = Color.Yellow;
                    e.Appearance.BackColor = Color.Yellow;

                }
                if (e.DataField.FieldName.Equals("qtdPrevistoPeriodoAno0"))
                {
                    e.Brick.Sides = BorderSide.All;
                    e.Brick.BackColor = Color.Yellow;
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
            if (e.ValueType == PivotGridValueType.Value)
            {
                e.Brick.BackColor = Color.White;
                e.Appearance.BackColor = Color.White;
            }
            else if (e.ValueType == PivotGridValueType.Total)
            {
                e.Brick.BackColor = Color.WhiteSmoke;
                e.Appearance.BackColor = Color.WhiteSmoke;
            }
            else if (e.ValueType == PivotGridValueType.GrandTotal)
            {
                e.Brick.BackColor = Color.Gainsboro;
                e.Appearance.BackColor = Color.Gainsboro;
            }
        }
    }

    #endregion

    #region OUTROS

  

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int altura = 0;
        int largura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        pgDados.Height = new Unit((altura - 240) + "px");
    }



    protected void callback_Callback(object source, CallbackEventArgs e)
    {

    }

    protected void pgDados_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
    {
        var parametros = e.Parameters;
        //string[] listaParametros = parametros.Split('|');
        carregaGrid(parametros);
        setaTitulospvGrid((spAno.Value == null) ? DateTime.Now.Year.ToString() : spAno.Value.ToString());
    }

    #endregion 
}