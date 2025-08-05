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

public partial class _Portfolios_visaoCorporativa_01 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade, codigoUsuario, codigoProjeto = -1;
    public string alturaDiv = "";
    string larguraGrafico = "";
    public int largura = 0;
    public int altura = 0;

    public string larguraDiv = "";

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

        codigoProjeto = int.Parse(Request.QueryString["ID"].ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        definealturaTela();

        cDados.aplicaEstiloVisual(this);
        carregaComboIndicadores();
        carregaItens();

        
    }

    private void definealturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;



        //larguraGrafico = (((largura - 230) - 20) - 30).ToString();//o que deve ser
        //teste
        larguraGrafico = (largura - 400).ToString();
        larguraDiv = ((largura - 430) - 20) + "px";
        nb01.Width = new Unit((largura - 400) + "px");


        cDados.getLarguraAlturaTela(ResolucaoCliente, out largura, out altura);
        alturaDiv = (altura - 215).ToString() + "px";

        //divListaMetas.Style.Add("height", alturaDiv);
        //divListaMetas.Style.Add("width", larguraDiv);
        //divListaMetas.Style.Add("overflow", "auto");

    }

    private void carregaItens()
    {
        string where = "";

        int codigoTipoProjeto = cDados.getCodigoTipoProjeto(codigoProjeto);

        if (codigoTipoProjeto == 2)
        {
            where += string.Format(@" AND p.CodigoProjeto IN(SELECT lp.CodigoProjetoFilho
                                                                   FROM {0}.{1}.LinkProjeto AS lp
                                                                  WHERE CodigoProjetoPai = {2}
                                                                    AND TipoLink = 'PP'
                                                                  UNION
                                                                 SELECT {2})", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);
        }
        else
        {
            where += " AND p.CodigoProjeto = " + codigoProjeto;
        }

        if (ddlIndicador.Value.ToString() != "-1")
            where += " AND i.CodigoIndicador = " + ddlIndicador.Value.ToString();
        
        DataSet ds = cDados.getMetasVisaoCorporativaProjetos(codigoEntidade, where);

        nb01.Groups.Clear();

        int count = 0;

        if (cDados.DataSetOk(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                string textoGrupo = string.Format("<table><tr><td style='width:30px;'><img src='../../imagens/{0}Menor.gif' /></td><td>{1}</td></tr></table>",
                    dr["Desempenho"].ToString(), dr["Meta"].ToString());

               
                string urlGrafico = "../../_Portfolios/VisaoMetas/mt_003.aspx?CM=" + dr["CodigoMetaOperacional"] + "&CI=" + dr["CodigoIndicador"] + "&UM=" + dr["SiglaUnidadeMedida"] + "&CD=" + dr["CasasDecimais"];

                string textoItem = string.Format(@"<table style='width:100%;'><tr><td>Indicador: {0}</td></tr><tr><td height='5px'></td></tr><tr><td><table style='width:100%;'><tr><td><iframe id=""frm2_{1}"" frameborder=""0"" height=""260px"" scrolling=""no"" src=""{2}""  style=""width:{3}"" ></iframe></td></tr></table></td></tr></table>", dr["NomeIndicador"].ToString()
                                                          , dr["CodigoMetaOperacional"].ToString()
                                                            , urlGrafico, larguraGrafico + "px");

                NavBarGroup nb;

                nb = nb01.Groups.Add(textoGrupo, dr["CodigoMetaOperacional"].ToString());

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
    
    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
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

    private void carregaComboIndicadores()
    {
        string where = "";
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
