
//Revisado
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
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;
using System.Collections.Generic;
using DevExpress.Web.ASPxPivotGrid;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_OLAP_Contratos : System.Web.UI.Page
{
    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    int codigoEntidade = 0;

    public string alturaTabela = "";
    public string larguraTabela = "";

    public bool exportaOLAPTodosFormatos = false;

    private List<string> Meses = new List<string>();
    private string tipoConsulta = ""; //Tipo de consulta a ser efetuada 'C'liente ou 'F'ornecedor

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_PrjRelCnt");
        }

        if (!IsPostBack)
        {
            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            
        }

        if (Request.QueryString["TC"] != null) 
            tipoConsulta = Request.QueryString["TC"].ToString();
        else
            this.Response.Redirect("~/erros/MostraMensagemErro.aspx?mensagem=Não foi definido o tipo de consulta a ser efetuada!");

        if (tipoConsulta == "C")
        {
            pvgContratos.Fields["NomeFornecedor"].Caption = "Cliente";
            pvgContratos.Fields["AnoPagamento"].Caption = "Ano Recebimento"; //Ano Pagamento
            pvgContratos.Fields["DataPagamento"].Caption = "Data Recebimento"; //Data Pagamento
            pvgContratos.Fields["MesPagamento"].Caption = "Mês Recebimento"; //Mes Pagamento
            pvgContratos.Fields["ValorPago"].Caption = "Valor recebido parcela"; //Valor pago parcela
        }
        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot
        populaGrid();
        fldStatus.FilterValues.Clear();
        fldStatus.FilterValues.Add("Ativo");
        fldStatus.FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included;
        defineAlturaTela();
        Meses.Clear();
        Meses.Add("Jan");
        Meses.Add("Fev");
        Meses.Add("Mar");
        Meses.Add("Abr");
        Meses.Add("Mai");
        Meses.Add("Jun");
        Meses.Add("Jul");
        Meses.Add("Ago");
        Meses.Add("Set");
        Meses.Add("Out");
        Meses.Add("Nov");
        Meses.Add("Dez");
        Meses.Add("Nenhum");


        this.Title = cDados.getNomeSistema();
    }
    private void populaGrid()
    {
        bool UtilizaCustoPrevistoSUCC = false;
        bool UtilizaCustoRealizadoSUCC = false;
        DataSet dsPar = cDados.getParametrosSistema(codigoEntidade, "UtilizaCustoPrevistoSUCC", "UtilizaCustoRealizadoSUCC");

        if (cDados.DataSetOk(dsPar) && cDados.DataTableOk(dsPar.Tables[0]))
        {
            UtilizaCustoPrevistoSUCC = dsPar.Tables[0].Rows[0]["UtilizaCustoPrevistoSUCC"].ToString() == "S";
            UtilizaCustoRealizadoSUCC = dsPar.Tables[0].Rows[0]["UtilizaCustoRealizadoSUCC"].ToString() == "S";
        }
        if(UtilizaCustoPrevistoSUCC || UtilizaCustoRealizadoSUCC)
        {
            pvgContratos.Fields.Remove(field1);
        }
 
        DataSet ds = cDados.getOLAPContratos(codigoEntidade, codigoUsuarioResponsavel, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), tipoConsulta);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgContratos.DataSource = ds.Tables[0];
            pvgContratos.DataBind();
        }
    }
    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 191) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 10) + "px";

    }

    protected void pvgContratos_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        // se estiver ordenando a coluna 'Mes' 
        if (e.Field == fldMesVencimento)
        {
            object valor1 = Meses.IndexOf(e.Value1.ToString());
            object valor2 = Meses.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        } // if (e.Field == fldMes)

        if (e.Field == fldMesPagamento)
        {
            object valor1 = Meses.IndexOf(e.Value1.ToString());
            object valor2 = Meses.IndexOf(e.Value2.ToString());
            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
            e.Handled = true;
        } // if (e.Field == fldMes)
    }
    protected void pvgContratos_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
        ASPxPivotGrid grid = (ASPxPivotGrid)sender;
        DataTable dt = (DataTable)grid.DataSource;
        string numeroContrato;
        decimal valorTotal = 0;
        List<string> lstContratos = new List<string>();
        string nomeCampoSomar = "";

        if ((dt != null) && (ds != null))
        {

            if (e.DataField.FieldName == fieldValorContrato.FieldName)
                nomeCampoSomar = "ValorContrato";
            else if (e.DataField.FieldName == fieldValorRestante.FieldName)
                nomeCampoSomar = "ValorRestante";
        }

        if (nomeCampoSomar != "")
        {
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if ((summaryRow.ListSourceRowIndex >= 0) && (dt.Rows.Count >= summaryRow.ListSourceRowIndex))
                {
                    DataRow dataRow = dt.Rows[summaryRow.ListSourceRowIndex];
                    numeroContrato = dataRow["NumeroContrato"].ToString();

                    if (!lstContratos.Contains(numeroContrato))
                    {
                        valorTotal += (decimal)dataRow[nomeCampoSomar];
                        lstContratos.Add(numeroContrato);
                    }
                }
            } // foreach (DevExpress..

            e.CustomValue = valorTotal;
        } // if (nomeCampoSomar != "")
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        if(e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapContr", pvgContratos);
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapContr", "OLAP Contratos", this, pvgContratos);
    }

    #endregion
}
