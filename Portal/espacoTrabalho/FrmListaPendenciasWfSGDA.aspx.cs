using DevExpress.Web;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class espacoTrabalho_FrmListaPendenciasWfSGDA : Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        populaGrid();

        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PendenciasWorkflowSGDA.js""></script>"));

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void populaGrid()
    {

        string comandoSQL = string.Format(@"EXEC [dbo].[p_wf_obtemListaInstanciasUsuario] 
                  @in_identificadorUsuario	= '{0}'
                , @in_codigoEntidade		= {1}
                , @in_codigoFluxo			= NULL
                , @in_codigoProjeto 		= NULL                
                , @in_somenteInteracao		= 1
                , @in_somentePendencia 		= 1
                ", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            var dt = ds.Tables[0];
            var apenasAtrasadas = Request.QueryString["apenasAtrasadas"];
            if (!string.IsNullOrEmpty(apenasAtrasadas))
            {
                if (apenasAtrasadas.ToLower().Equals("s"))
                    dt.DefaultView.RowFilter = "IndicaAtraso = 'Sim'";
                else
                    dt.DefaultView.RowFilter = "IndicaAtraso = 'Não'";
            }
            dt.Columns.AddRange(new DataColumn[] {
                ObtemColunaComputadaSituacaoSubEtapa("colMaterialApoio", "Material de apoio"),
                ObtemColunaComputadaSituacaoSubEtapa("colDespesasViagem", "Despesas de viagem"),
                ObtemColunaComputadaSituacaoSubEtapa("colEnvioPlacasReconhecimento", "EnvioPlacasReconhecimento"),
            });
            gvDados.DataSource = dt;
            gvDados.DataBind();

            //((GridViewCommandColumn)gvDados.Columns[0]).CustomButtons[0].Visibility = GridViewCustomButtonVisibility.BrowsableRow;
        }

    }

    private static DataColumn ObtemColunaComputadaSituacaoSubEtapa(string nomeColuna, string nomeColunaReferencia)
    {
        var col = new DataColumn();
        col.ColumnName = nomeColuna;
        col.DataType = Type.GetType("System.String");
        col.Expression = string.Format("IIF([{0}] = 'S', '~/imagens/VerdeOK.gif', IIF([{0}] = 'N', '~/imagens/iconesBotoesFluxo/Problema.png', ''))", nomeColunaReferencia);
        return col;
    }

    #region VARIOS

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        alturaPrincipal -= 300;

        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal;
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LtPFlxUsu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, true, "LtPFlxUsu", "Pendências de Workflow", this);

        if (!IsPostBack)
        {
            if (Request.QueryString["ATR"] + "" == "S")
            {
                if (gvDados.FilterExpression != "")
                    gvDados.FilterExpression += " AND [IndicaAtraso] = 'Sim'";
                else
                    gvDados.FilterExpression = " [IndicaAtraso] = 'Sim'";
            }
        }
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
        else if (e.RowType == GridViewRowType.Header)
        {
            if (e.Column.Name == "Acao")
                e.Text = "Possui Pendências?";
        }
        else if (e.RowType == GridViewRowType.Data)
        {
            var dataColumn = e.Column as GridViewDataColumn;
            if (dataColumn == null) return;

            if (dataColumn is GridViewDataImageColumn)
                e.ImageValue = GetImageBinaryData(e.Value.ToString());
            else if (dataColumn.FieldName == "Prazo" && e.Value is DateTime)
            {
                var prazo = (DateTime)e.Value;
                e.BrickStyle.ForeColor = DateTime.Today.AddDays(1) > prazo ? Color.Red : Color.Black;
            }
        }
    }

    byte[] GetImageBinaryData(string relativePath)
    {
        string path = Server.MapPath(relativePath);
        return File.Exists(path) ? File.ReadAllBytes(path) : null;
    }

    public string getRowCount()
    {
        string retorno = "";
        int quantidadeLinhas = 0;
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            if (!gvDados.IsGroupRow(i))
                quantidadeLinhas++;
        }

        retorno = quantidadeLinhas + " pendências";

        return retorno;
    }
}