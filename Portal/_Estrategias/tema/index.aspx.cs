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

public partial class _Estrategias_wizard_index : System.Web.UI.Page
{
    public string telaInicial = "inicial";
    public string alturaTabela;
    dados cDados;
    public int codigoObjetivoEstrategico = 0;
    int codigoUsuarioResponsavel;
    public int codigoUnidadeMapa = 0, codigoUnidadeSelecionada = 0, codigoUnidadeLogada = 0, codigoMapa = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Title = cDados.getNomeSistema();
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

        if (Request.QueryString["CT"] != null && Request.QueryString["CT"].ToString() != "")
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["CT"].ToString());
        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());
        if (Request.QueryString["CM"] != null && Request.QueryString["CM"].ToString() != "")
            codigoMapa = int.Parse(Request.QueryString["CM"].ToString());

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        alturaTabela = getAlturaTela() + "px";

        cDados.aplicaEstiloVisual(this);
        
        carregaComboUnidades();

        if (ddlUnidade.SelectedIndex != -1)
            codigoUnidadeSelecionada = int.Parse(ddlUnidade.Value.ToString());

        ddlUnidade.JSProperties["cp_COE"] = codigoObjetivoEstrategico;
        ddlUnidade.JSProperties["cp_UNM"] = codigoUnidadeMapa;
        ddlUnidade.JSProperties["cp_UN"] = codigoUnidadeSelecionada;
        ddlUnidade.JSProperties["cp_UNL"] = codigoUnidadeLogada;

        //telaInicial = "./detalhesObjetivoEstrategico.aspx?COE=" + codigoObjetivoEstrategico + "&UN=" + ddlUnidade.Value + "&UNM=" + codigoUnidadeMapa + "&CM=" + codigoMapa;
        if (!IsPostBack)
            ClientScript.RegisterStartupScript(GetType(), "ok", "defineTI();", true);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "OBJ_ES", "OBJ", codigoObjetivoEstrategico, "Adicionar Tema aos Favoritos");
        }
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 200).ToString();
    }

    private void carregaComboUnidades()
    {
        string where = "";
        
        DataSet dsUnidades = cDados.getEntidadesMapasEstrategicos(codigoUsuarioResponsavel, codigoMapa, where);

        if (cDados.DataSetOk(dsUnidades))
        {
            ddlUnidade.DataSource = dsUnidades;
            ddlUnidade.TextField = "SiglaUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";

            ddlUnidade.Columns[0].FieldName = "SiglaUnidadeNegocio";
            ddlUnidade.Columns[0].Caption = "Sigla da Entidade";

            ddlUnidade.Columns[1].FieldName = "NomeUnidadeNegocio";
            ddlUnidade.Columns[1].Caption = "Nome da Entidade";

            ddlUnidade.DataBind();

        }

        if (!IsPostBack && ddlUnidade.Items.Count > 0)
        {
            if (ddlUnidade.Items.FindByValue(codigoUnidadeMapa) == null)
                ddlUnidade.Value = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            else
                ddlUnidade.Value = codigoUnidadeMapa;
        }
    }
}
