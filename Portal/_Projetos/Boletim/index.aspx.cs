using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_Boletim_index : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade, codigoUsuario, codigoUnidade;
    string nomeUnidade = "";
    public string telaInicial = "";
    public string alturaTabela;

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

        if (!string.IsNullOrEmpty(Request.QueryString["CodigoUnidade"]))
            codigoUnidade = int.Parse(Request.QueryString["CodigoUnidade"].ToString());
        if (!string.IsNullOrEmpty(Request.QueryString["NomeUnidade"]))
            nomeUnidade = Request.QueryString["NomeUnidade"];

        string cmdsql = string.Format(@"select nomeunidadenegocio from UnidadeNegocio where CodigoUnidadeNegocio = {0}", codigoUnidade);

        DataSet ds = cDados.getDataSet(cmdsql);
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nomeUnidade = ds.Tables[0].Rows[0][0].ToString();
        }

        nvbMenuProjeto.JSProperties["cp_titulo"] = nomeUnidade;
        lblTituloTelaOld.Text = "Relatórios de Projetos da Unidade: " + nomeUnidade;

        alturaTabela = getAlturaTela() + "px";

        if (!IsPostBack)
        {
            carregaMenuLateral();

            int nivel = 2;

            if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
                nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

            cDados.aplicaEstiloVisual(Page);
            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTelaOld.Text, "BL_UND", "UN", codigoUnidade, "Adicionar Boletins da Unidade aos Favoritos");
        }
        this.Title = cDados.getNomeSistema();
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 150).ToString();
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        return largura - 140;
    }

    private void carregaMenuLateral()
    {
        NavBarGroup group = nvbMenuProjeto.Groups.FindByName("opOpcoes");
        string iniciaisTipoObjeto = codigoEntidade == codigoUnidade ? "EN" : "UN";
        string iniciaisPermissaoEditarComentario = string.Format("{0}_CmtSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoEnviarRelatorio = string.Format("{0}_EnvSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoPublicar = string.Format("{0}_PubSttRpt", iniciaisTipoObjeto);
        string iniciaisPermissaoConfiguracoes = string.Format("{0}_AltDstSttRpt", iniciaisTipoObjeto);

        bool acessaMenuPublicarRelatorioStatus = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade,
            codigoUnidade, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoPublicar);
        bool acessaMenuEditarComentariosRelatorioStatus = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade,
            codigoUnidade, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoEditarComentario);
        bool acessaMenuEnviarDestinatariosRelatorioStatus = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade,
            codigoUnidade, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoEnviarRelatorio);
        bool acessaMenuConfigurarRelatorioStatus = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade,
            codigoUnidade, "null", iniciaisTipoObjeto, 0, "null", iniciaisPermissaoConfiguracoes);

        string frameName = "framePrincipal";
        string urlConfiguracoes = string.Format("~/_Projetos/DadosProjeto/ConfiguracoesStatusReport.aspx?idObjeto={0}&tp=UN", codigoUnidade);
        string urlHistorico = "~/_Projetos/DadosProjeto/HistoricoStatusReport.aspx?idObjeto=" + codigoUnidade + "&tp=UN";
        //if (acessaMenuPublicarRelatorioStatus || acessaMenuEditarComentariosRelatorioStatus || acessaMenuEnviarDestinatariosRelatorioStatus)
        group.Items.Add(new NavBarItem("Histórico", "opHistorico", "", urlHistorico, frameName));
        if (acessaMenuConfigurarRelatorioStatus)
            group.Items.Add(new NavBarItem("Configurações", "opConfiguracoes", "", urlConfiguracoes, frameName));

        //Análise Crítica
        //string labelAnalises = "Análise Crítica";
        //DataSet dsParametros = cDados.getParametrosSistema("LabelAnaliseProjeto");

        //if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        //{
        //    if (dsParametros.Tables[0].Rows[0]["LabelAnaliseProjeto"].ToString().Trim() != "")
        //        labelAnalises = dsParametros.Tables[0].Rows[0]["LabelAnaliseProjeto"].ToString();
        //}
        //group.Items.Add(new NavBarItem(labelAnalises, "opPermiAnalise", "", "~/_Projetos/DadosProjeto/AnaliseProjeto.aspx?IDProjeto=" + codigoUnidade + "&tp=" + labelAnalises, "framePrincipal"));

        telaInicial = String.Format("../DadosProjeto/HistoricoStatusReport.aspx?idObjeto={0}&tp={1}", codigoUnidade, iniciaisTipoObjeto);
    }
}
