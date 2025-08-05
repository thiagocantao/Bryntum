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
using DevExpress.XtraPivotGrid.Localization;
using DevExpress.Utils.Localization.Internal;
using System.Drawing;
using DevExpress.XtraPivotGrid;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using System.Collections.Generic;
using System.IO;
using DevExpress.Utils;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using System.Windows.Forms;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_OLAP_RH_Tabela : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoUsuario;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";
    private string dbName, dbOwner;

    public string alturaTabela = "";
    public string larguraTabela = "";
    protected class Alocacoes
    { 
        private List<string> ListaDeRecursos;
        
        private List<DateTime> ListaDeDatas;
        
        public Alocacoes()
        {
            ListaDeRecursos = new List<string>();
            ListaDeDatas = new List<DateTime>();
        }
        
        public void Clear()
        {
            ListaDeRecursos.Clear();
            ListaDeDatas.Clear();
        }

        public void Add(string recurso, DateTime data)
        {
            ListaDeRecursos.Add(recurso);
            ListaDeDatas.Add(data);
        }

        public bool ContemAlocacao(string recurso, DateTime data)
        {
            return ( ListaDeRecursos.Contains(recurso) && ListaDeDatas.Contains(data) );
        }

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        this.TH(this.TS("url_OLAP_RH_Tabela"));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            AtribuiPeriodo();            
        }

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        buscaDadosGrid();


        grid.OptionsPager.Visible = false;

        //Se o grid Olap retorna viazio seta o grid como vazio.
        if (((System.Data.DataSet)grid.DataSource).Tables.Count == 0)
        {

            grid.DataSource = null;

            grid.DataBind();

            //grid.Dispose();
        }
        else
        {
            if (!IsPostBack)
                grid.CollapseAll();
        }



     
        
        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    private void AtribuiPeriodo()
    {
        dteInicio.Date = DateTime.Today.AddDays(-15);
        dteFim.Date = DateTime.Today.AddDays(15);
    }

    private void buscaDadosGrid()
    {
        if ((!dteInicio.IsValid) || (!dteFim.IsValid))
            return;

        string strDataInicial = string.Format("{0}/{1}/{2}", dteInicio.Date.Day, dteInicio.Date.Month, dteInicio.Date.Year);
        string strDataFinal = string.Format("{0}/{1}/{2}", dteFim.Date.Day, dteFim.Date.Month, dteFim.Date.Year);

        string comandoSQL = string.Format(@" 

            DECLARE @DataInicial    SmallDateTime
            DECLARE @DataFinal      SmallDateTime
            SET @DataInicial        = CONVERT(SmallDateTime, '{4}', 103)
            SET @DataFinal          = CONVERT(SmallDateTime, '{5}', 103)

            /*
            SELECT 
	            [Data]		                AS [DataReferencia]
			    , [Ano]	
			    , [Mes] 
			    , [Semana] 
                , [ResourceUID]
                , [SiglaUnidadeNegocio]		
                , [NomeProjeto]				
                , [DescricaoStatus]			
                , [NomeRecurso]				
                , [Trabalho]				
                , [TrabalhoLB]				
                , [TrabalhoReal]			
                , [Capacidade]				
                , [Disponibilidade]
                , [CustoHora]
		        , [Anotacoes]
                , [NomeUnidadeNegocio]
				, [NomeEntidade]
            FROM 
                {0}.{1}.f_GetDadosOLAP_ProjetosAnaliseRH({2}, {3}, @DataInicial, @DataFinal )
            */

            EXEC {0}.{1}.p_GetDadosOLAP_ProjetosAnaliseRH {2}, {3}, @DataInicial, @DataFinal

		    ", dbName, dbOwner, codigoUsuario, codigoEntidadeUsuarioResponsavel, strDataInicial, strDataFinal);

        grid.DataSource = cDados.getDataSet(comandoSQL);

        //Valida se o Grid está vazio.
        if (((System.Data.DataSet)grid.DataSource).Tables.Count == 0)
        {
            return;
        }

        grid.DataBind();
    }


    
    protected void grid_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
    {
        if (e.DataField == fieldDisponibilidade)
        {
            double x;
            if ( (e.Value != null) && double.TryParse(e.Value.ToString(), out x) && (x<0) )
                e.CellStyle.ForeColor = Color.Red;
        }

        if ( (e.DataField == fieldDisponibilidade) || (e.DataField == fieldCapacidade) )
        {
            if (e.Value != null) 
            {
                double valor;
                double.TryParse(e.Value.ToString(), out valor);
                e.CellStyle.Font.Italic = (valor > 0 );
            }
        }
    }

    protected void grid_CustomFieldSort(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs e)
    {
        if ( e.Field == fieldNomeProjeto && (e.Value1 != e.Value2) )
        {
            if (e.Value1.ToString().Equals("Disponibilidade") )
            {
                e.Result = -1;
                e.Handled = true;
            }
            else if (e.Value2.ToString().Equals("Disponibilidade") )
            {
                e.Result = 1;
                e.Handled = true;
            }
        }
        else if ( (e.Field == fieldMes) || ( e.Field.FieldName == "Semana"  ) )
        { // se estiver ordenando a coluna 'Mes' ou 'Semana'
            ASPxPivotGrid grid = (ASPxPivotGrid)sender;
            if (e.Value1.ToString() == e.Value2.ToString())
            {
                e.Result = 0;
                e.Handled = true;
            }
            else if (e.Field == fieldMes)
            {
                if (((DateTime)e.Value1) > ((DateTime)e.Value2))
                {
                    e.Result = 1;
                }
                else if (((DateTime)e.Value1) < ((DateTime)e.Value2))
                {
                    e.Result = -1;
                }
                else if (((DateTime)e.Value1) == ((DateTime)e.Value2))
                {
                    e.Result = 0;
                }
                e.Handled = true;
            }
            else
            {
                if (e.Field.FieldName == "Semana")
                {
                    if ((e.ListSourceRowIndex1 >= 0) && (e.ListSourceRowIndex2 >= 0))
                    {
                        object valor1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "DataReferencia");
                        object valor2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "DataReferencia");
                        if ((null != valor1) && (null != valor2))
                        {
                            // se as duas datas de referência forem iguais, compara por periodicidade
                            if ((System.DateTime)valor1 == (System.DateTime)valor2)
                            {
                                valor1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Periodicidade");
                                valor2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Periodicidade");
                            }
                            e.Result = System.Collections.Comparer.Default.Compare(valor1, valor2);
                            e.Handled = true;
                        } // if ((null != valor1) && ...
                    } // if ((e.ListSourceRowIndex1 >= 0) && ...
                }                
            } // else (e.Value1 == e.Value2)

        } // 
    }
    
    protected void grid_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
    {
        if ( e.Parameters.Equals("PopularGrid") )
            buscaDadosGrid();
    }
    
    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        //Font fonte1 = new Font("Verdana", 8.0f, FontStyle.Regular);
        //e.Brick.Style.Font = fonte1;
        if (e.DataField != null)
        {
            if (e.DataField == fieldDisponibilidade)
            {
                double x;
                if ((e.Value != null) && double.TryParse(e.Value.ToString(), out x) && (x < 0))
                    e.Appearance.ForeColor = Color.Red;
            }

            if ((e.DataField == fieldDisponibilidade) || (e.DataField == fieldCapacidade))
            {
                if (e.Value != null)
                {
                    double valor;
                    double.TryParse(e.Value.ToString(), out valor);
                    Font fonteItalica = new Font("Verdana", 8.0f, (valor > 0) ? FontStyle.Italic : FontStyle.Regular);
                    e.Appearance.Font = fonteItalica;
                }
            }
        }
    }
    protected void ASPxPivotGridExporter1_CustomExportHeader(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportHeaderEventArgs e)
    {
        //Font fonte1 = new Font("Verdana", 8.0f, FontStyle.Regular);
        //e.Brick.Style.Font = fonte1;
    }
    protected void ASPxPivotGridExporter1_CustomExportFieldValue(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportFieldValueEventArgs e)
    {
        //Font fonte1 = new Font("Verdana", 8.0f, FontStyle.Regular);
        //e.Brick.Style.Font = fonte1;
    }

    protected void grid_CustomCellDisplayText(object sender, DevExpress.Web.ASPxPivotGrid.PivotCellDisplayTextEventArgs e)
    {
        //if (e.DataField.FieldName == "ResourceUID")
        //{
        //    DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
        //    ASPxPivotGrid gridPV = (ASPxPivotGrid)sender;
        //    DataSet dt = (DataSet)gridPV.DataSource;          
        //    double valorTrabalho = 0, valorCapacidade = 0;            

        //    if ((dt != null) && (ds != null))
        //    {
        //        foreach (DataRow dr in dt.Tables[0].Select("ResourceUID='" + e.Value + "'"))
        //        {
        //            valorCapacidade += dr["Capacidade"].ToString() == "" ? 0 : double.Parse(dr["Capacidade"].ToString());
        //            valorTrabalho += dr["Trabalho"].ToString() == "" ? 0 : double.Parse(dr["Trabalho"].ToString());
        //        }
        //    }

        //    e.DisplayText = string.Format("{0:p2}", valorCapacidade == 0 ? 0 : valorTrabalho / valorCapacidade);
        //}
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OlapAnlRec", grid);
        }            
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OlapAnlRec", "OLAP Análise Recursos", this, grid);
    }

    #endregion

    protected void grid_Init(object sender, EventArgs e)
    {
        int largura = 0;
        int altura = 0;
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        cDados.getLarguraAlturaTela(ResolucaoCliente, out largura, out altura);
        altura = altura - 220;
           
        ((ASPxPivotGrid)sender).Width = new Unit(largura.ToString() + "px");
        ((ASPxPivotGrid)sender).Height = new Unit(altura.ToString() + "px");

    }
}

/*
    protected void grid_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if ((e.FieldName.Equals("Disponibilidade")) || (e.FieldName.Equals("Capacidade")))
        {
            ASPxPivotGrid grid = (ASPxPivotGrid)sender;
            Alocacoes alocacoes = new Alocacoes();
            if (MostrarTotalDisponibilidade(grid, e))
            {
                DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                DateTime data;
                string recurso;
                double valorSomar = 0;
                double dblAux;

                foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
                {
                    data = DateTime.MinValue;
                    recurso = string.Empty;

                    if (summaryRow["DataReferencia"] != null)
                        data = (DateTime) summaryRow["DataReferencia"];

                    if (summaryRow["NomeRecurso"] != null)
                        recurso = summaryRow["NomeRecurso"].ToString();

                    if (!alocacoes.ContemAlocacao(recurso, data))
                    {
                        if (double.TryParse(summaryRow[e.FieldName].ToString(), out dblAux))
                            valorSomar += dblAux;

                        alocacoes.Add(recurso, data);
                   } // 
                }
                e.CustomValue = valorSomar;
            } // if (MostrarTotalDisponibilidade(grid, e))
        } // if (e.FieldName.Equals("Disponibilidade"))
    }

    private bool MostrarTotalDisponibilidade(ASPxPivotGrid grid, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        bool mostrar = false;

        if ((e.RowField == null) || (e.ColumnField == null))
            mostrar = true;
        else
        {
            int fieldIndex = -1;
            if ((e.ColumnField != null) && (e.ColumnField.FieldName.Equals("NomeRecurso")))
                fieldIndex = e.ColumnField.AreaIndex;
            else if ((e.RowField != null) && (e.RowField.FieldName.Equals("NomeRecurso")))
                fieldIndex = e.RowField.AreaIndex;

            if (fieldIndex == 0)
                mostrar = true;
        }

        return mostrar;
    }
*/

