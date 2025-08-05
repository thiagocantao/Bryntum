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
using DevExpress.Web.ASPxPivotGrid;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using System.Diagnostics;

public partial class _Processos_Visualizacao_RelatoriosURL_Projetos_url_relAnaliseProcessosReprojetados : System.Web.UI.Page
{

    #region --- [Variáveis da classe]

    dados cDados;

    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel;
    public int codigoEntidade;
    public string alturaTabela = "";
    public string larguraTabela = "";
    public bool exportaOLAPTodosFormatos = false;

    #endregion

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "null", "EN", 0, "null", "EN_PrjRelPrj");
        }

        cDados.aplicaEstiloVisual(this);

        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        if (!IsPostBack && !IsCallback)
        {
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos", "labelTipoProjeto", "labelQuestoes", "lblGeneroLabelQuestao");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
            {
                
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
                string labelQuestoes = dsTemp.Tables[0].Rows[0]["labelQuestoes"].ToString();
                string generoQuestao = dsTemp.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();
                string labelTipoProjeto = dsTemp.Tables[0].Rows[0]["labelTipoProjeto"].ToString();
                labelQuestoes = string.Format("{0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a");

                fieldProblemasAtivos.Caption = labelQuestoes;
                fieldTipoProjeto.Caption = string.IsNullOrEmpty(labelTipoProjeto) ? "Tipo de Projeto" : labelTipoProjeto;

            }
        }

        populaGrid();
        defineAlturaTela();
        
        this.Title = cDados.getNomeSistema();

        cDados.configuraPainelBotoesOLAP(tbBotoes);
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        alturaTabela = (alturaPrincipal - 220) + "px";//a div vai ficar com essa altura
        larguraTabela = (larguraPrincipal - 5) + "px";

    }

    protected void btnSelecionar_Click(object sender, EventArgs e)
    {
        populaGrid();
    }

    protected void pvgDadosIndicador_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if (e.FieldName.ToLower().StartsWith("cor"))
        {
            object cor = e.CreateDrillDownDataSource()[0][e.DataField];
            e.CustomValue = (cor ?? "branco").ToString().Trim();
        }

        string campoTotal = string.Empty;
        string campoEspecifico = string.Empty;
        double valorTotal = 0;
        double valorEspecifico = 0;
        double dblAux;

        if (e.FieldName.Contains("IndiceDesempenho"))
            campoTotal = "ValorAgregado";

        if (e.FieldName.Equals("IndiceDesempenhoPrazo"))
            campoEspecifico = "ValorPlanejado";
        else if (e.FieldName.Equals("IndiceDesempenhoCusto"))
            campoEspecifico = "CustoReal";

        if (campoEspecifico.Length > 0)
        {
            DevExpress.XtraPivotGrid.PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            foreach (DevExpress.XtraPivotGrid.PivotDrillDownDataRow summaryRow in ds)
            {
                if ((summaryRow[campoTotal] != null) && double.TryParse(summaryRow[campoTotal].ToString(), out dblAux))
                    valorTotal += dblAux;

                if ((summaryRow[campoEspecifico] != null) && double.TryParse(summaryRow[campoEspecifico].ToString(), out dblAux))
                    valorEspecifico += dblAux;
            }
            if (valorTotal == 0)
                e.CustomValue = 0;
            else
                e.CustomValue = valorEspecifico / valorTotal;
        }
    }

    private void populaGrid()
    {
        DataSet ds = getRelatorioAnaliseProcessosRedesenhados(codigoEntidade, int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString()), codigoUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            pvgDadosProjeto.DataSource = ds.Tables[0];
            fieldUnidadeAtendimento.Caption = cDados.defineLabelUnidadeAtendimento().Replace(":", " ").TrimEnd(); 
            pvgDadosProjeto.DataBind();

        }
    }


    public DataSet getRelatorioAnaliseProcessosRedesenhados(int codigoEntidade, int codigoCarteira, int codigoUsuario)
    {
        string comandoSQL = string.Format(@" SELECT * FROM {0}.{1}.f_GetDadosOLAP_AnaliseProcessosRedesenhados({3}, {2}, {4}) "
                , cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, codigoUsuario, codigoCarteira);

        return cDados.getDataSet(comandoSQL);

    }



    protected void pvgDadosProjeto_CustomCellStyle(object sender, PivotCustomCellStyleEventArgs e)
    {

        if (e.Value != null && e.DataField.FieldName == "VariacaoTrabalho")
        {
            if (Convert.ToDecimal(e.Value.ToString()) != 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
        if (e.Value != null && e.DataField.FieldName == "VariacaoCusto")
        {
            if (e.Value != null && Convert.ToDecimal(e.Value.ToString()) != 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
        if (e.Value != null && e.DataField.FieldName == "VariacaoReceita")
        {
            if (e.Value != null && Convert.ToDecimal(e.Value.ToString()) != 0)
            {
                e.CellStyle.ForeColor = Color.Red;
            }
        }
    }

    protected void ASPxPivotGridExporter1_CustomExportCell(object sender, DevExpress.Web.ASPxPivotGrid.WebCustomExportCellEventArgs e)
    {
        if (e.DataField != null)
        {
            if (e.DataField.FieldName.ToLower().Contains("cor") && (e.Value != null))
            {
                e.Appearance.Font = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                e.Brick.Text = "l";
                e.Brick.TextValue = "l";
                if (e.Value.ToString().Equals("Vermelho"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Value.ToString().Equals("Amarelo"))
                {
                    e.Appearance.ForeColor = Color.Yellow;
                }
                else if (e.Value.ToString().Equals("Verde"))
                {
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (e.Value.ToString().Equals("Azul"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("Branco"))
                {
                    e.Appearance.ForeColor = Color.WhiteSmoke;
                }
                else if (e.Value.ToString().Equals("Laranja"))
                {
                    e.Appearance.ForeColor = Color.Orange;
                }
                else
                {
                    e.Brick.Text = " ";
                    e.Brick.Value = " ";
                }

            }
            //else if (e.DataField.FieldName.Contains("Variacao"))
            //{
            //    if (Convert.ToInt32(e.Value) != 0)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
            //else if (e.DataField.FieldName.Contains("Atras"))
            //{
            //    if (Convert.ToInt32(e.Value) > 0)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }
    }
    
    protected void pvgDadosProjeto_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
    {
        bool indicaCampoCorDesempenho =
            e.DataField == fieldCorFinanceiro ||
            e.DataField == fieldCorFisico ||
            e.DataField == fieldCorGeral;
        if (indicaCampoCorDesempenho)
        {
            if (e.Value != null)
                e.DisplayText = "<img alt='' src='../../imagens/" + e.Value.ToString() + "Menor.gif' />";
        }
    }

    protected void pvgDadosProjeto_CustomFilterPopupItems(object sender, PivotCustomFilterPopupItemsEventArgs e)
    {
        if (e.ShowBlanksItem != null)
        {
            e.ShowBlanksItem.IsVisible = false;
            e.ShowBlanksItem.IsChecked = false;
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados.eventoClickMenuOLAP(menu, parameter, ASPxPivotGridExporter1, this, "OLAPPROCREPRO", pvgDadosProjeto);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesExportarLayoutOLAP((sender as ASPxMenu), true, true, "OLAPPROCREPRO", "OLAP Processos Reprojetados", this, pvgDadosProjeto);
    }

    #endregion
}
