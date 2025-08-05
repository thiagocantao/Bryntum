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
using System.Collections.Specialized;

public partial class inicio : System.Web.UI.Page
{
    dados cDados;
    private string siglaUnidade = "";
    private string codigoUnidade = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        cDados.setInfoSistema("novoMenu", null);
        cDados.setInfoSistema("menuAlterarSenha", null);
        cDados.setInfoSistema("menuGerenciarPreferencias", null);
        cDados.setInfoSistema("menuOrganizarFavoritos", null);
        cDados.setInfoSistema("novoMenuAdministracao", null);

        if (cDados.getInfoSistema("ResolucaoCliente").ToString() == null)
            Response.Redirect("~index.aspx");
        if (null != Request.QueryString["SUN"])
            siglaUnidade = Request.QueryString["SUN"];
        if (null != Request.QueryString["CE"])
            codigoUnidade = Request.QueryString["CE"];

        string idUsuarioLogado = cDados.getInfoSistema("IDUsuarioLogado") != null ? cDados.getInfoSistema("IDUsuarioLogado").ToString() : "";

        cDados.setInfoSistema("CodigosProjetosUsuario", null);
        cDados.setInfoSistema("CodigoUnidade", null);
        cDados.setInfoSistema("CodigoMapa", null);
        cDados.setInfoSistema("CodigoMapaEstrategicoInicial", null);
        cDados.setInfoSistema("COIN", null);
        cDados.setInfoSistema("COE", null);
        cDados.setInfoSistema("TipoDesempenho", null);
        cDados.setInfoSistema("AnoIndicador", null);
        cDados.setInfoSistema("MesIndicador", null);
        cDados.setInfoSistema("UnidadeSelecionadaCombo", null);
        cDados.setInfoSistema("CodigoProjeto", null);
        cDados.setInfoSistema("DesabilitarBotoes", null);
        cDados.setInfoSistema("CodigoPortfolio", null);
        cDados.setInfoSistema("Cenario", null);
        cDados.setInfoSistema("Categoria", null);
        cDados.setInfoSistema("CodigoRH", null);
        cDados.setInfoSistema("CodigoWorkflow", null);
        cDados.setInfoSistema("CodigoInstanciaWf", null);
        cDados.setInfoSistema("CodigoEtapaAtual", null);
        cDados.setInfoSistema("OcorrenciaAtual", null);
        cDados.setInfoSistema("Tipo", null);
        cDados.setInfoSistema("CodigoStatusProcesso", null);
        cDados.setInfoSistema("CodigoStatus", null);
        cDados.setInfoSistema("DsOperacional", null);
        cDados.setInfoSistema("PeriodicidadeEPC", null);
        cDados.setInfoSistema("TipoEdicao", null);
        cDados.setInfoSistema("AnoPainelContrato", null);
        cDados.setInfoSistema("TipoContratacao", null);
        cDados.setInfoSistema("CodigoFonte", null);
        cDados.setInfoSistema("AnoContrato", null);
        cDados.setInfoSistema("DataSetFotos", null);
        cDados.setInfoSistema("CodigoOrcamento", null);
        cDados.setInfoSistema("Mes", null);
        cDados.setInfoSistema("DataEstagio", null);
        cDados.setInfoSistema("InicioOlapEstagio", null);
        cDados.setInfoSistema("TerminoOlapEstagio", null);
        Session["notificacoes"] = null;
        Session["urlWSnewBriskBase"] = null;

        if (siglaUnidade != "" && codigoUnidade != "")
        {
            cDados.setInfoSistema("Opcao", "et"); // espaço de trabalho
            cDados.setInfoSistema("CodigoEntidade", codigoUnidade);
            cDados.setInfoSistema("SiglaUnidadeNegocio", siglaUnidade);

            if (int.Parse(codigoUnidade) != -1)
                cDados.setInfoSistema("CodigoCarteira", cDados.getCodigoCarteiraPadraoUsuario(int.Parse(idUsuarioLogado), int.Parse(codigoUnidade)).ToString());
        }
        else
            cDados.setInfoSistema("Opcao", "se"); // seleciona entidade

        Response.Redirect("~/selecionaOpcao.aspx");
    }
}
