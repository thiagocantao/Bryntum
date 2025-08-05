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

public partial class administracao_frmAditivosContratos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public string oldAditivar = "";

    int codigoContrato = -1;
    public string somenteLeitura = "";

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        bool ret = int.TryParse(Request.QueryString["CC"] + "", out codigoContrato);
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        cDados.aplicaEstiloVisual(this.Page);
        carregaGvDados();
    }
    private void carregaGvDados()
    {
        carregaComboTipoContrato("");
        string strWhere = " AND cont.CodigoContrato = " + codigoContrato;
        DataSet dsContratos = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, strWhere);
        if (cDados.DataSetOk(dsContratos) && cDados.DataTableOk(dsContratos.Tables[0]))
        {
            txtNumeroContrato_tabItens.Text = dsContratos.Tables[0].Rows[0]["NumeroContrato"].ToString();
            ddlTipoContrato_tabItens.Value = dsContratos.Tables[0].Rows[0]["CodigoTipoContrato"];
            ddlTipoContrato_tabItens.Text = dsContratos.Tables[0].Rows[0]["DescricaoTipoContrato"].ToString();

            ddlStatusComplementarContrato_tabItens.Value = dsContratos.Tables[0].Rows[0]["StatusContrato"];
            ddlInicioDeVigencia_tabItens.Value = dsContratos.Tables[0].Rows[0]["DataInicio"];
            ddlTerminoDeVigencia_tabItens.Value = dsContratos.Tables[0].Rows[0]["DataTermino"];

        }
        cDados.aplicaEstiloVisual(this);
        DataSet ds = cDados.getInformacoesContrato(codigoContrato);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0]["StatusContrato"].ToString() == "I")
            {
                //podeIncluir = false;
            }
        }

        DataSet dsAditivo = cDados.getAditivosContratos(" AND CodigoContrato = " + codigoContrato);

        if ((cDados.DataSetOk(dsAditivo)))
        {
            gvDados.DataSource = dsAditivo;
            gvDados.DataBind();
        }
    }
    private void carregaComboTipoContrato(string where)
    {
        DataSet ds = cDados.getTipoContrato(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(ds))
        {
            ddlTipoContrato_tabItens.DataSource = ds.Tables[0];
            ddlTipoContrato_tabItens.DataBind();
        }

        ListEditItem sinTipoContrato = new ListEditItem(Resources.traducao.frmContratosNovo_selecionar___, "0");
        ddlTipoContrato_tabItens.Items.Insert(0, sinTipoContrato);

        if (!IsPostBack && ddlTipoContrato_tabItens.Items.Count > 0)
            ddlTipoContrato_tabItens.SelectedIndex = 0;
    }
    protected string botaoNovo()
    {
        return string.Format(@"<table style=""width:100%""><tr><td align=""center"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""incluiAditivoContrato();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Incluir"" style=""cursor: default;""/>");
    }


    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "incluiAditivoContrato(" + codigoContrato + ");", true, true, false, "CtrPrj", "Aditivos", this);

    }

    protected void menu_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "CtrPrj");
    }

    protected void gvExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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


/*
 
    protected void Page_Load(object sender, EventArgs e)
    {
        carregaComboTipoContrato("");
        
    }

    
 
 */