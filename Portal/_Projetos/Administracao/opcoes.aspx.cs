/*
12/01/2011 : by Alejandro : alteração do método 'private void carregarMenuLateral(){...}' 
                            consultando a permissão disponivel do usuario.

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

public partial class _Projetos_Administracao_opcoes : System.Web.UI.Page
{
    dados cDados;
    private string idUsuarioLogado;
    private string idEntidadeLogada;
    public string alturaTabela;

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

        idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        idEntidadeLogada = cDados.getInfoSistema("CodigoEntidade").ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        alturaTabela = getAlturaTela() + "px";
        carregarMenuLateral();
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 135).ToString();
    }

    private void carregarMenuLateral()
    {
        string modoLancamentoResultadoProjeto = "Dado";
        DataSet ds = cDados.getParametrosSistema("modoLancamentoResultadoProjeto");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string modoLanc = ds.Tables[0].Rows[0]["modoLancamentoResultadoProjeto"].ToString();
            if (modoLanc != "")
                modoLancamentoResultadoProjeto = modoLanc;
        }

        if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSPMTPRJ"))
        {
            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Programas", "opPrograma", "", "~/_Projetos/Administracao/programasDoProjetos.aspx?Tit=" + "Programas", "projeto_desktop"));
        }

        if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSCTT"))
            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Contratos", "opContratos", "", "~/_Projetos/Administracao/Contratos.aspx?Tit=" + "Contratos", "projeto_desktop"));

        if (cDados.VerificaPermissaoUsuario(int.Parse(idUsuarioLogado), int.Parse(idEntidadeLogada), "CONSPMTPRJ"))
        {
            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Cadastro de Papéis", "opCadastroPapeis", "", "~/_Projetos/Administracao/CadastroPapeis.aspx?Tit=" + "Cadastro de Papéis", "projeto_desktop"));
            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Inclusão de Projeto", "opInclusaoProjeto", "", "~/_Projetos/Administracao/cadastroProjetos.aspx?Tit=" + "Inclusão do Projeto", "projeto_desktop"));

            if (modoLancamentoResultadoProjeto == "Dado")
                nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Dados de Indicadores", "opDadosIndicadores", "", "~/_Projetos/Administracao/DadosIndicadoresOperacional.aspx?Tit=" + "Dados de Indicadores", "projeto_desktop"));

            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Indicadores de Projeto", "opIndicadoresProjeto", "", "~/_Projetos/Administracao/IndicadoresOperacional.aspx?Tit=" + "Indicadores de Projeto", "projeto_desktop"));
            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Desbloqueio de Projeto", "opDesbloqueioProjeto", "", "~/administracao/adm_CheckinProjetos.aspx?Tit=" + "Desbloqueio de Projeto", "projeto_desktop"));

            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Assuntos de Lições Aprendidas", "opAssuntosLicoesAprendidas", "", "~/_Projetos/Administracao/assuntosLicoesAprendidas.aspx?Tit=" + "Assuntos de Lições Aprendidas", "projeto_desktop"));

            nvbMenuProjeto.Groups.FindByName("gpProjeto").Items.Add(new NavBarItem("Tipos de Atividades", "opTimeSheet", "", "~/_Projetos/Administracao/tipoTarefaTimesheet.aspx?Tit=" + "Tipos de Tarefas", "projeto_desktop"));
        }
 
    }
}
