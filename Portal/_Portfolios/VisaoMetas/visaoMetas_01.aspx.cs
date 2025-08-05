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



public partial class _Portfolios_VisaoMetas_visaoMetas_01 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade, codigoUsuario, largura;
    public string alturaDiv = "";
    public string larguraDiv = "";
    private int codigoCarteira;

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoCarteira = int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString());

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x'))) / 2;

        cDados.aplicaEstiloVisual(this);
        carregaComboProjetos();
        carregaComboIndicadores();
        carregaItens();

        definealturaTela();
    }

    private void definealturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        //int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        int largura = 0, altura = 0;

        cDados.getLarguraAlturaTela(ResolucaoCliente, out largura, out altura);
        alturaDiv = (altura - 275).ToString() + "px";
        larguraDiv = (largura - 15) + "px";
        nb01.Width = new Unit(((largura - 45) / 2) + "px");
        nb02.Width = new Unit(((largura - 45) / 2) + "px");
        divListaMetas.Style.Add("height", alturaDiv);
        divListaMetas.Style.Add("width", larguraDiv);
        divListaMetas.Style.Add("overflow", "auto");
    }

    private void carregaItens()
    {
        string where = "";

        if (ddlProjeto.Value.ToString() != "-1")
        {
            int codigoTipoProjeto = cDados.getCodigoTipoProjeto(int.Parse(ddlProjeto.Value.ToString()));

            if (codigoTipoProjeto == 2)
            {
                where += string.Format(@" AND p.CodigoProjeto IN(SELECT lp.CodigoProjetoFilho
                                                                   FROM {0}.{1}.LinkProjeto AS lp
                                                                  WHERE CodigoProjetoPai = {2}
                                                                    AND TipoLink = 'PP'
                                                                 UNION
                                                                 SELECT {2})", cDados.getDbName(), cDados.getDbOwner(), ddlProjeto.Value.ToString());
            }
            else
            {
                where += " AND p.CodigoProjeto = " + ddlProjeto.Value.ToString();
            }
        }

        if (ddlIndicador.SelectedIndex > 0)
            where += " AND i.CodigoIndicador = " + ddlIndicador.Value.ToString();

        if (ddlTipoIndicador.SelectedIndex > 0)
            where += " AND i.TipoIndicador = " + ddlTipoIndicador.Value.ToString();

        where += string.Format(" AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, codigoCarteira);

        DataSet ds = cDados.getMetasVisaoCorporativaProjetos(codigoEntidade, where);

        nb01.Groups.Clear();
        nb02.Groups.Clear();
        hfUrls.Clear();

        int count = 0;

        if (cDados.DataSetOk(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string textoGrupo = string.Format("<table><tr><td><img src='../../imagens/{0}Menor.gif' /></td><td>{1}</td></tr></table>",
                    dr["Desempenho"].ToString(), dr["Meta"].ToString());

                string urlGrafico = "../../_Portfolios/VisaoMetas/mt_003.aspx?CM=" + dr["CodigoMetaOperacional"] + "&CI=" + dr["CodigoIndicador"] + "&UM=" + dr["SiglaUnidadeMedida"] + "&CD=" + dr["CasasDecimais"];
                
                string textoItem = string.Format(@"<table style='width:100%;'><tr><td>{6}: <strong title='Detalhes do Projeto' onclick=""window.top.gotoURL('_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto={5}&NomeProjeto={0}', '_top');"" class=""lk"">{0}</strong></td></tr><tr><td height='5px'></td></tr><tr><td>Indicador: {4}</td></tr><tr><td height='5px'></td></tr><tr><td><table style='width:100%;'><tr><td><iframe id=""frm2_{3}"" frameborder=""0"" height=""245px"" scrolling=""no"" src="""" width=""{7}""></iframe></td></tr></table></td></tr></table>", dr["NomeProjeto"].ToString()
                                                            , urlGrafico
                                                            , urlGrafico
                                                            , dr["CodigoMetaOperacional"].ToString()
                                                            , dr["NomeIndicador"].ToString()
                                                            , dr["CodigoProjeto"].ToString()
                                                            , cDados.getNomeTipoProjeto(int.Parse(dr["CodigoProjeto"].ToString()))
                                                            , largura - 52);

                NavBarGroup nb;

                hfUrls.Set("url_" + dr["CodigoMetaOperacional"].ToString(), urlGrafico); 

                if (count % 2 == 0)
                {
                    nb = nb01.Groups.Add(textoGrupo, dr["CodigoMetaOperacional"].ToString());
                }
                else
                {
                    nb = nb02.Groups.Add(textoGrupo, dr["CodigoMetaOperacional"].ToString());
                }               


                nb.Expanded = false;

                count++;

                NavBarItem nbi = nb.Items.Add(textoItem);

                nbi.ClientEnabled = false;
            }
        }

        if (count == 0)
        {
            popUpStatusTela.ShowOnPageLoad = true;
        }
    }

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        callback.JSProperties["cp_Analise"] = "";
        callback.JSProperties["cp_recomendacoes"] = "";
        callback.JSProperties["cp_titulo"] = "Análise";
        callback.JSProperties["cp_Meta"] = "";
        callback.JSProperties["cp_Indicador"] = "";

        string ano = e.Parameter.ToString().Split(';')[0];
        string mes = e.Parameter.ToString().Split(';')[1];
        string codigoMeta = e.Parameter.ToString().Split(';')[2];

        string where = " AND Ano = " + ano + " AND mes = " + mes;

        DataSet dsGrafico = cDados.getMetaProjetoIndicadorVisaoCorporativa(int.Parse(codigoMeta), where);

        DataSet ds = cDados.getMetasVisaoCorporativaProjetos(codigoEntidade, " AND mo.CodigoMetaOperacional = " + codigoMeta);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            callback.JSProperties["cp_Meta"] = ds.Tables[0].Rows[0]["Meta"].ToString();
            callback.JSProperties["cp_Indicador"] = ds.Tables[0].Rows[0]["NomeIndicador"].ToString();
        }

        if (cDados.DataSetOk(dsGrafico) && cDados.DataTableOk(dsGrafico.Tables[0]))
        {
            callback.JSProperties["cp_Analise"] = dsGrafico.Tables[0].Rows[0]["Analise"].ToString();
            callback.JSProperties["cp_recomendacoes"] = dsGrafico.Tables[0].Rows[0]["Recomendacoes"].ToString();
            callback.JSProperties["cp_titulo"] = "Análise - " + dsGrafico.Tables[0].Rows[0]["Periodo"].ToString();

            string nomeResponsavel = dsGrafico.Tables[0].Rows[0]["NomeUsuarioUltimaAlteracao"].ToString();

            if (nomeResponsavel.IndexOf(' ') != -1)
            {
                nomeResponsavel = nomeResponsavel.Substring(0, nomeResponsavel.IndexOf(' ')) + " " + nomeResponsavel.Substring(nomeResponsavel.LastIndexOf(' '));
            }

            string ultimaAtualizacao = string.Format("Última atualização realizada por {1} em {0}", dsGrafico.Tables[0].Rows[0]["DataUltimaAlteracaoFormatada"].ToString()
                , nomeResponsavel);

            callback.JSProperties["cp_DataAnalise"] = ultimaAtualizacao;
        }

    }

    private void carregaComboProjetos()
    {
        string where = string.Format(@" AND p.CodigoEntidade = {0} AND p.DataExclusao IS NULL  AND p.CodigoTipoProjeto IN (SELECT tp.CodigoTipoProjeto FROM TipoProjeto tp WHERE tp.IndicaTipoProjeto = 'PRJ' OR tp.IndicaTipoProjeto = 'PRG')", codigoEntidade);
        where += string.Format(" AND p.CodigoProjeto in (SELECT codigoProjeto FROM {0}.{1}.f_getProjetosUsuario({2}, {3}, {4})) ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuario, codigoEntidade, codigoCarteira);

        DataSet ds = cDados.getProjetos(where);

        ddlProjeto.DataSource = ds;
        ddlProjeto.TextField = "NomeProjeto";
        ddlProjeto.ValueField = "CodigoProjeto";
        ddlProjeto.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");
        ddlProjeto.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlProjeto.SelectedIndex = 0;
    }

    private void carregaComboIndicadores()
    {
        string where = "";

        if (ddlTipoIndicador.SelectedIndex > 0)
            where += " AND ind.TipoIndicador = " + ddlTipoIndicador.Value.ToString();

        DataSet ds = cDados.getIndicadoresOperacional(codigoEntidade,  where);

        ddlIndicador.DataSource = ds;
        ddlIndicador.TextField = "NomeIndicador";
        ddlIndicador.ValueField = "CodigoIndicador";
        ddlIndicador.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");
        ddlIndicador.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlIndicador.SelectedIndex = 0;
    }

   
}
