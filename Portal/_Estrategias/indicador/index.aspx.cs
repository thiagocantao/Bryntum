/*
16/12/2010 - by Alejandro
            Alteração da função 'carregaComboIndicador()', para realizar filtro dos indicadores segundo a unidade logada.
*/
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

public partial class _Estrategias_indicador_index : System.Web.UI.Page
{
    public string telaInicial = "inicial";
    public string alturaTabela;
    dados cDados;
    public int codigoIndicador = 0;
    public int codigoObjetivoEstrategico = 0;
    public int codigoUnidadeSelecionada = 0;
    public int codigoUnidadeMapa = 0;
    public int codigoMapa = 0;
    public int codigoEntidade = 0;
    private int idUsuarioLogado = -1;

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
        this.Title = cDados.getNomeSistema();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        alturaTabela = getAlturaTela() + "px";
        getLarguraTela();

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (cDados.getInfoSistema("CodigoMapa") == null && Request.QueryString["CM"] != null)
        {
            cDados.setInfoSistema("CodigoMapa", Request.QueryString["CM"].ToString());           
        }

        if (cDados.getInfoSistema("CodigoMapa") != null)
        {
            codigoMapa = int.Parse(cDados.getInfoSistema("CodigoMapa").ToString());
        }

        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());

        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
        {
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        }
        else
        {
            codigoUnidadeSelecionada = codigoEntidade;
        }

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (!IsPostBack)
        {            
            if (Request.QueryString["COIN"] != null && Request.QueryString["COIN"].ToString() != "")
                codigoIndicador = int.Parse(Request.QueryString["COIN"].ToString());

            if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
                codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());

            //cDados.VerificaAcessoTela(this, idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", codigoMapa, "null", "IN_CnsDtl");
            cDados.setInfoSistema("COIN", codigoIndicador);
            cDados.setInfoSistema("COE", codigoObjetivoEstrategico);
            cDados.setInfoSistema("CodigoUnidade", codigoUnidadeSelecionada);
            cDados.setInfoSistema("UNM", codigoUnidadeMapa);

            cDados.setInfoSistema("AnoIndicador", null);
            cDados.setInfoSistema("MesIndicador", null);
        }

        cDados.aplicaEstiloVisual(this);

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "usaUnidadesFederacao");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["usaUnidadesFederacao"].ToString() == "N")
        {
            telaInicial = "./resumoIndicador2.aspx";
        }
        else
        {
            telaInicial = "./resumoIndicador.aspx";
        }

        carregaComboObjetivos();
        carregaComboIndicador("N");

        int nivel = 2;

        if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
            nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());
        if (!IsPostBack)
        {
            string nomeIndicador = "Indicador: ";
            DataTable dtCampos = cDados.getInformacoesIndicador(codigoIndicador, codigoObjetivoEstrategico).Tables[0];

            if (cDados.DataTableOk(dtCampos))
            {
                nomeIndicador += dtCampos.Rows[0]["NomeIndicador"].ToString();
            }

            sp_Tela.Panes[1].ContentUrl = telaInicial;

            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, nomeIndicador, "RES_IN", "IND", codigoIndicador, "Adicionar o Indicador aos Favoritos");


        }
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        sp_Tela.Panes[1].Size = new Unit(largura - 180);
        return largura - 140;
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        //sp_Tela.Height = alturaTela - 160;
        return (alturaTela - 120).ToString();
    }

    protected void ddlIndicador_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaComboIndicador("S");

        if (ddlIndicador.Items.Count > 0)
            ddlIndicador.SelectedIndex = 0;

        ddlIndicador.JSProperties["cp_codigoIndicador"] = ddlIndicador.Items.Count > 0 ? ddlIndicador.Value.ToString() : "-1";
    }

    //todo: [listagem do Objetivos Estratégico] Na verificação de acesso, no parâmetro de "codigo do objeto pai", ta fixado en '0' (zero).
    private void carregaComboObjetivos()
    {
        //a permissão qeu se aplica e a de consultar detalhe do objeto 'OB_CnsDtl'
        //string where = string.Format(@"AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, oe.CodigoObjetoEstrategia, null, 'OB', 0, null, 'OB_CnsDtl') > 0"
        //                            , cDados.getDbName(),cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);
        DataSet ds = cDados.getObjetivosEstrategicosMapa(codigoMapa, "");

        if (cDados.DataSetOk(ds))
        {
            ddlObjetivo.DataSource = ds;
            ddlObjetivo.TextField = "DescricaoObjetoEstrategia";
            ddlObjetivo.ValueField = "codigoObjetoEstrategia";
            ddlObjetivo.DataBind();
        }

        if (!IsPostBack && ddlObjetivo.Items.Count > 0)
        {
                ddlObjetivo.SelectedIndex = 0;
        }
    }

    private void carregaComboIndicador(string recarrega)
    {
        if (ddlObjetivo.SelectedIndex != -1 || recarrega.Equals("S"))
        {
            //a permissão qeu se aplica e a de consultar detalhe do objeto 'IN_CnsDtl'
            string where = string.Format(@"AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, i.[CodigoIndicador], null, 'IN', {4}, null, 'IN_CnsDtl') = 1"
                                        , cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade, codigoMapa*(-1));
            DataSet ds = cDados.getIndicadoresOE(where, int.Parse(ddlObjetivo.Value.ToString()), codigoUnidadeSelecionada, ""); //  - 1);

            if (cDados.DataSetOk(ds))
            {
                ddlIndicador.DataSource = ds;
                ddlIndicador.TextField = "NomeIndicador";
                ddlIndicador.ValueField = "CodigoIndicador";
                ddlIndicador.DataBind();
            }

            if (!IsPostBack && ddlIndicador.Items.Count > 0)
            {
                ddlIndicador.SelectedIndex = 0;                
            }
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string objetivo = e.Parameter.Split(';')[0];
        string indicador = e.Parameter.Split(';')[1];
        cDados.setInfoSistema("COIN", indicador);
        cDados.setInfoSistema("COE", objetivo);
        cDados.setInfoSistema("CodigoUnidade", codigoUnidadeSelecionada);
    }
}
