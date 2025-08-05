/*
 * OBSERVAÇÕES
 * 
 * MUDANÇAS:
 * 
 07/01/2011 by Alejandro
            Alteração: alterar o conteudo do comboBox aonde tem os periodos do resumo.
            foi alterado o metodo: private void selecionaPeriodo(DataSet dsPeriodo){...}
 * 04/03/2011 :: Alejandro : Permissões da tela [IN_CnsDtl]
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
using DevExpress.Web;
using System.Drawing;

public partial class _Estrategias_indicador_resumoIndicador : System.Web.UI.Page
{
    dados cDados;
    private int codigoIndicador = 0, codigoObjetivo = 0, codigoMapa = 0, codigoUnidade = 0;
    public int alturaGrafico = 120, alturaGrid = 145;
    private string unidadeMedida = "", casasDecimais = "";
    private string polaridade = "";
    public string graficoInicial = "", telaPeriodo = "";

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


        // pega as informações via variável de sessão
        codigoIndicador = cDados.getInfoSistema("COIN") != null ? int.Parse(cDados.getInfoSistema("COIN").ToString()) : -1;
        codigoObjetivo = cDados.getInfoSistema("COE") != null ? int.Parse(cDados.getInfoSistema("COE").ToString()) : -1;
        codigoMapa = cDados.getInfoSistema("CodigoMapa") != null ? int.Parse(cDados.getInfoSistema("CodigoMapa").ToString()) : 0;

        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        carregaComboUnidades();

        if (!IsPostBack)
        {
            int idObjetoPai;

            if (codigoMapa == 0)
            {
                idObjetoPai = codigoUnidade;
            }
            else
            {
                idObjetoPai = codigoMapa * (-1);
            }

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, codigoIndicador, "null", "IN", idObjetoPai, "null", "IN_CnsDtl"); // código de mapa como pai do indicador tem que ser negativo
        }

        
        carregaComboPeriodo();
        cDados.aplicaEstiloVisual(this);        
        defineAlturaTela();
        defineObjetosSessao();

        if (codigoObjetivo <= 0)
            tdObjetivoMapa.Style.Add("display", "none");

        if (ddlUnidade.SelectedIndex != -1)
            codigoUnidade = int.Parse(ddlUnidade.Value.ToString());
                
        montaCampos();

        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidade);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblUnidade.Text = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString() + ":&nbsp;";
        }

        verificaTelaApresentada();
        telaPeriodo = "./graficos/periodo_002.aspx?COIN=" + codigoIndicador + "&COE=" + codigoObjetivo + "&NumeroCasas=" + casasDecimais + "&AlturaTela=" + alturaGrid;
        btnSelecionar.JSProperties["cp_UNL"] = cDados.getInfoSistema("CodigoEntidade").ToString();
        this.Title = cDados.getNomeSistema();
    }

    private void defineObjetosSessao()
    {
        if (!IsPostBack)
        {
            if (cDados.getInfoSistema("TipoDesempenho") != null)
                ddlDesempenho.Value = cDados.getInfoSistema("TipoDesempenho").ToString();

            if (cDados.getInfoSistema("CodigoUnidade") != null)
                ddlUnidade.SelectedItem = ddlUnidade.Items.FindByValue(int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString()));

            if (cDados.getInfoSistema("AnoIndicador") != null && cDados.getInfoSistema("MesIndicador") != null)
            {
                int ano = int.Parse(cDados.getInfoSistema("AnoIndicador").ToString());
                int mes = int.Parse(cDados.getInfoSistema("MesIndicador").ToString());

                string anoMesSelecioado = string.Format("{0}{1:D2}", ano, mes);

                if(ddlPeriodo.Items.FindByValue(anoMesSelecioado) != null)
                    ddlPeriodo.Value = anoMesSelecioado;
            }
        }

        if (ddlDesempenho.SelectedIndex != -1)
            cDados.setInfoSistema("TipoDesempenho", ddlDesempenho.Value.ToString());

        if (ddlUnidade.SelectedIndex != -1)
            cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Value.ToString());

        if (ddlPeriodo.SelectedIndex != -1)
        {
            int ano = int.Parse(ddlPeriodo.Value.ToString().Substring(0, 4));
            int mes = int.Parse(ddlPeriodo.Value.ToString().Substring(4));

            cDados.setInfoSistema("AnoIndicador", ano);
            cDados.setInfoSistema("MesIndicador", mes);
        }
    }

    private void montaCampos()
    { 
        DataTable dtCampos = cDados.getInformacoesIndicador(codigoIndicador, codigoObjetivo).Tables[0];

        if (cDados.DataTableOk(dtCampos))
        {
            txtMapa.Text = dtCampos.Rows[0]["TituloMapaEstrategico"].ToString();
            txtObjetivoEstrategico.Text = dtCampos.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtIndicador.Text = dtCampos.Rows[0]["NomeIndicador"].ToString();
            txtResponsavel.Text = dtCampos.Rows[0]["NomeUsuario"].ToString();
            txtUnidadeMedida.Text = dtCampos.Rows[0]["DescricaoUnidadeMedida_PT"].ToString();
            txtPolaridade.Text = (dtCampos.Rows[0]["Polaridade"].ToString() == "POS") ? "Quanto Maior, Melhor" : "Quanto Maior, Pior";
            txtPeriodicidade.Text = dtCampos.Rows[0]["DescricaoPeriodicidade_PT"].ToString();
            memoFormula.Text = dtCampos.Rows[0]["FormulaIndicador"].ToString();
            ASPxHtmlEditor1.Html = dtCampos.Rows[0]["GlossarioIndicador"].ToString();
            unidadeMedida = dtCampos.Rows[0]["SiglaUnidadeMedida"].ToString();
            casasDecimais = dtCampos.Rows[0]["CasasDecimais"].ToString();
            polaridade = dtCampos.Rows[0]["Polaridade"].ToString();

            if (dtCampos.Rows[0]["Polaridade"].ToString() == "NEG")
            {
                txtPolaridade.Font.Bold = true;
                imgPolaridade.ImageUrl = "~/imagens/botoes/PolaridadeN.png";
                imgPolaridade.ToolTip = "Polaridade Negativa(Quanto Maior, Pior)";
            }
            else
            {
                imgPolaridade.ImageUrl = "~/imagens/botoes/PolaridadeP.png";
                imgPolaridade.ToolTip = "Polaridade Positiva(Quanto Maior, Melhor)";
            }
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int constanteSomatoria = (codigoObjetivo <= 0) ? 60 : 0;
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaGrafico = alturaPrincipal - 220;
        alturaGrid = (alturaPrincipal - 427) + constanteSomatoria;
    }

    #region Combos

    private void carregaComboPeriodo()
    {
        DataSet dsPeriodos = cDados.getPeriodosAtivosIndicador(codigoIndicador, "");

        if (cDados.DataSetOk(dsPeriodos))
        {
            //-----alteração..
            ddlPeriodo.DataSource = dsPeriodos;
            ddlPeriodo.TextField = "Periodo";
            ddlPeriodo.ValueField = "AnoMes";
            ddlPeriodo.DataBind();
        }

        if (!IsPostBack)
        {
            selecionaPeriodo(dsPeriodos);
        }
    }

    private void selecionaPeriodo(DataSet dsPeriodo)
    {
        bool periodoAtual = false;
        if (ddlPeriodo.Items.Count > 0)
        {
            foreach(DataRow dr in dsPeriodo.Tables[0].Rows)
            {
                if ((bool)dr["PeriodoAtual"])
                {
                    ddlPeriodo.Value = dr["AnoMes"].ToString();
                    periodoAtual = true;
                    //verificaTelaApresentada();
                    break;
                }
            }
            if (!periodoAtual)
                ddlPeriodo.SelectedIndex = ddlPeriodo.Items.Count - 1;
        }
    }

    private void carregaComboUnidades()
    {
        DataSet dsUnidades = cDados.getUnidadesUsuarioIndicador(codigoIndicador, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), "");

        if (cDados.DataSetOk(dsUnidades))
        {
            ddlUnidade.DataSource = dsUnidades;
            ddlUnidade.TextField = "SiglaUnidadeNegocio";
            ddlUnidade.ValueField = "CodigoUnidadeNegocio";
            ddlUnidade.Columns[0].FieldName = "SiglaUnidadeNegocio";
            ddlUnidade.Columns[1].FieldName = "NomeUnidadeNegocio";
           
            ddlUnidade.TextFormatString = "{0}";

            ddlUnidade.DataBind();
        }

        if (!IsPostBack && ddlUnidade.Items.Count > 0)
        {
            int codigoUnidadeSelecionada = int.Parse(cDados.getInfoSistema("CodigoUnidade").ToString());

            if (ddlUnidade.Items.FindByValue(codigoUnidadeSelecionada) != null)
            {                                
                ddlUnidade.Value = codigoUnidadeSelecionada;
            }
            else if (ddlUnidade.Items.FindByValue(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString())) != null)
            {
                ddlUnidade.Value = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            }
            else
            {
                ddlUnidade.SelectedIndex = 0;
            }
        }

        if (ddlUnidade.SelectedIndex != -1)
            cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Value.ToString());
    }

    #endregion   

    private void verificaTelaApresentada()
    {
        DataSet dsParametroViewGoogleMaps = cDados.getParametrosSistema("visualizaIndGoogleMaps");
        bool viewGoogleMaps = false;

        if ((cDados.DataSetOk(dsParametroViewGoogleMaps)) && (cDados.DataTableOk(dsParametroViewGoogleMaps.Tables[0])))
        {
            viewGoogleMaps = (dsParametroViewGoogleMaps.Tables[0].Rows[0]["visualizaIndGoogleMaps"].ToString().Trim() == "S");
        }
        
        
        


        if (ddlUnidade.SelectedIndex != -1)
        {
            DataSet dsUN = cDados.getUnidade(" AND CodigoUnidadeNegocio = " + ddlUnidade.Value);

            if (dsUN.Tables[0].Rows[0]["SiglaUF"].ToString() == "BR")
            {
                if (viewGoogleMaps)
                {
                    graficoInicial = "./graficos/grafico_004.aspx?COIN=" + codigoIndicador + "&COE=" + codigoObjetivo;
                }
                else
                {
                    graficoInicial = "./graficos/grafico_001.aspx?COIN=" + codigoIndicador + "&COE=" + codigoObjetivo; 
                }                                
            }
            else
            {
                graficoInicial = "./graficos/grafico_002.aspx?COIN=" + codigoIndicador + "&COE=" + codigoObjetivo;   
            }

            graficoInicial = graficoInicial + "&NumeroCasas=" + casasDecimais + "&Polaridade=" + polaridade;
            callback.JSProperties["cp_TelaGrafico"] = graficoInicial;

            cDados.setInfoSistema("CodigoUnidade", ddlUnidade.Value.ToString());
        }
    }








    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        verificaTelaApresentada();
    }

    protected void gvUnidades_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string siglaUF = e.Parameters.ToString();
        int mes = 0, ano = 0;
        char tipoDesempenho = 'A';

        ((GridViewDataTextColumn)gvUnidades.Columns["Meta"]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";
        ((GridViewDataTextColumn)gvUnidades.Columns["Realizado"]).PropertiesTextEdit.DisplayFormatString = "{0:n" + casasDecimais + "}";

        if (cDados.getInfoSistema("AnoIndicador") != null)
            ano = int.Parse(cDados.getInfoSistema("AnoIndicador").ToString());

        if (cDados.getInfoSistema("MesIndicador") != null)
            mes = int.Parse(cDados.getInfoSistema("MesIndicador").ToString());

        if (cDados.getInfoSistema("TipoDesempenho") != null)
            tipoDesempenho = char.Parse(cDados.getInfoSistema("TipoDesempenho").ToString());

        DataSet dsUnidades = cDados.getDesempenhoIndicadorUF(codigoIndicador, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), mes, ano, tipoDesempenho, siglaUF, ""); 

        gvUnidades.DataSource = dsUnidades;

        gvUnidades.DataBind();
    }
}
