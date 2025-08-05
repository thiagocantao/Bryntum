using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_RelatorioSOE_senar : System.Web.UI.Page
{
    dados cDados;
    private string idUsuarioLogado;
    private int idProjeto;

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
        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();  //usuario logado.
        idProjeto = -1;
        bool retorno = int.TryParse(Request.QueryString["IDProjeto"], out idProjeto);

        this.Title = cDados.getNomeSistema();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        if (!IsPostBack)
        {
            populaDdlOrigem();            
        }

        populaGrid();
        cDados.aplicaEstiloVisual(this);
        defineAlturaTela();
        
    }

    protected void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int largura;
        int altura;
        
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        gvDados.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
        gvDados.Settings.VerticalScrollableHeight = (altura - 500);
        //gvDados.Width = new Unit((largura - 180) + "px");
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/RelatorioSOE_senar.js""></script>"));
        this.TH(this.TS("RelatorioSOE_senar"));
    }

    protected void populaDdlOrigem()
    {
        string comandoSQL = string.Format(@"
 SELECT SiglaUf, 
        NomeUf 
   FROM f_SENAR_GetListaUFUsuario({0}) 
 ORDER BY 
        SiglaUf", idUsuarioLogado);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if(cDados.DataSetOk(ds))
        {
            ddlOrigem.TextField = "NomeUf";
            ddlOrigem.ValueField = "SiglaUF";
            ddlOrigem.DataSource = ds.Tables[0];
            ddlOrigem.DataBind();
        }
        ListEditItem li = new ListEditItem(Resources.traducao.todos, "NULL");
        ddlOrigem.Items.Insert(0, li);
        ddlOrigem.SelectedIndex = 0;
    }

    protected void populaGrid()
    {
        //codigo usuario
        string sqlDeclareESets = string.Format("declare @in_CodigoUsuario Int" + Environment.NewLine);
        sqlDeclareESets += string.Format("SET @in_CodigoUsuario = " + idUsuarioLogado + Environment.NewLine + Environment.NewLine);

        //data início
        string dataInicio = (dtInicio.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", dtInicio.Date));
        sqlDeclareESets += string.Format("declare @in_DataInicio DateTime" + Environment.NewLine);
        sqlDeclareESets += string.Format("SET @in_DataInicio = {0}" + Environment.NewLine + Environment.NewLine, dataInicio);

        //data término
        string dataTermino = (dtTermino.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", dtTermino.Date));
        sqlDeclareESets += string.Format("declare @in_DataTermino DateTime" + Environment.NewLine);
        sqlDeclareESets += string.Format("SET @in_DataTermino = {0}" + Environment.NewLine + Environment.NewLine, dataTermino);

        //Origem
        string origem = (ddlOrigem.Value == null) ? "NULL" : 
            (ddlOrigem.Value.ToString() == "NULL") ? "NULL" : "'" + ddlOrigem.Value.ToString() + "'";
        

        sqlDeclareESets += string.Format("declare @in_Origem Char(2)" + Environment.NewLine);
        sqlDeclareESets += string.Format("SET @in_Origem = {0}" + Environment.NewLine + Environment.NewLine, origem);

        //CodigoProjeto
        sqlDeclareESets += string.Format("declare @in_CodigoProjeto Int" + Environment.NewLine);
        sqlDeclareESets += string.Format("SET @in_CodigoProjeto = " + idProjeto + Environment.NewLine + Environment.NewLine);

        string comandoSQL = string.Format(@"
        {0}

        SELECT  IdItem,
	            Origem,
	            Fornecedor,
	            TipoGasto,
	            NumeroContrato,
	            RevisaoPrevia,
	            MoedaContrato,
	            ValorTotalContrato,
	            ValorPagoAcumulado,
	            NumeroNF,
	            DataPagamento,
	            ValorPagamento,
	            PercentualBIRD,
	            ValorFinanciadoBanco,
	            Observacoes,
	            Parcela 
          FROM  [f_SENAR_RelatorioSOE] (
                @in_CodigoUsuario,
                @in_DataInicio,
                @in_DataTermino,
                @in_Origem,
                @in_CodigoProjeto)", sqlDeclareESets);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        populaGrid();
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "RelSoe", "Relatório SOE", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "RelSOE");
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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