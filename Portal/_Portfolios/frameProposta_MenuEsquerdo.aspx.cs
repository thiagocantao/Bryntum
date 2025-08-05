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

public partial class _Portfolios_frameProposta_MenuEsquerdo : System.Web.UI.Page
{
    dados cDados;
    
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

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        criarOpcoesMenuLateral();
    }

    private void criarOpcoesMenuLateral()
    {
        bool mostrarPareceres = true;
        bool mostrarFluxo = true;
        string readOnlyFormDinamico = "N";

        if (cDados.getInfoSistema("DesabilitarBotoes") != null && cDados.getInfoSistema("DesabilitarBotoes").ToString() == "S")
        {
            mostrarFluxo = false;
            mostrarPareceres = false;
            readOnlyFormDinamico = "S";
        }

        int codigoModeloFormulario;
        // Opção Principal
        NavBarItem itemPrincipal = new NavBarItem("Principal", "principal", "", "~/_Portfolios/frameProposta_Resumo.aspx", "framePrincipal");

        // Opção Proposta Completa
        codigoModeloFormulario = getCodigoModeloFormulario("PROP");
        NavBarItem itemPropostaCompleta = new NavBarItem("Proposta Completa", "propostaCompleta", "", "~/_Portfolios/renderizaFormulario.aspx?RO=" + readOnlyFormDinamico + "&CMF=" + codigoModeloFormulario, "framePrincipal");

        // Opção Pareceres
        codigoModeloFormulario = getCodigoModeloFormulario("PARECER");
        NavBarItem itemPareceres = new NavBarItem("Pareceres", "pareceres", "", "~/_Portfolios/renderizaFormulario.aspx?RO=" + readOnlyFormDinamico + "&CMF=" + codigoModeloFormulario, "framePrincipal");

        // Opção Fluxo
        NavBarItem itemFluxo = new NavBarItem("Fluxo", "fluxo", "", "~/_Portfolios/frameProposta_Fluxo.aspx", "framePrincipal");

        // Opção Detalhamento
        codigoModeloFormulario = getCodigoModeloFormulario("DETPROP");
        NavBarItem itemDetalhamento = new NavBarItem("Detalhamento", "detalhamento", "", "~/_Portfolios/renderizaFormulario.aspx?CMF=" + codigoModeloFormulario, "framePrincipal");

        // Opção Fluxo de Caixa
        NavBarItem itemFluxoCaixa = new NavBarItem("Fluxo de Caixa", "fluxoCaixa", "", "~/_Portfolios/frameProposta_FluxoCaixa.aspx?AT=200", "framePrincipal");

        // Opção Criterios
        NavBarItem itemCriterios = new NavBarItem("Critérios", "criterios", "", "~/_Portfolios/frameProposta_Criterios.aspx", "framePrincipal");


        navBarLateral.Groups[0].Items.Add(itemPrincipal);
        navBarLateral.Groups[0].Items.Add(itemPropostaCompleta);
        if (mostrarPareceres)
            navBarLateral.Groups[0].Items.Add(itemPareceres);
        if (mostrarFluxo)
            navBarLateral.Groups[0].Items.Add(itemFluxo);
        navBarLateral.Groups[0].Items.Add(itemDetalhamento);
        navBarLateral.Groups[0].Items.Add(itemFluxoCaixa);
        navBarLateral.Groups[0].Items.Add(itemCriterios);

    }

    private int getCodigoModeloFormulario(string iniciaisModeloFormulario)
    {
        int codigoModeloFormulario = 0;
        // busca o modelo do formulário de propostas
        string comandoSQL = string.Format("Select codigoModeloFormulario from modeloFormulario where IniciaisFormularioControladoSistema = '{0}'", iniciaisModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            DataTable dt = ds.Tables[0];
            if (cDados.DataTableOk(dt))
            {
                codigoModeloFormulario = int.Parse(dt.Rows[0]["codigoModeloFormulario"].ToString());
            }
        }
        return codigoModeloFormulario;
    }
}
