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
using CDIS;

public partial class _Portfolios_wf_novaProposta : System.Web.UI.Page
{
    dados cDados;
    string resolucaoCliente;
    public string alturaFrameFormularios;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        /* ========================================================================================== *
         * ATENÇÃO: O código do workflow responsável pela a inclusão de uma nova proposta deve ser    *     
         *          colocado no parâmetro "wfNovaProposta" da tabela "ParametroConfiguracaoSistema"   *
         * ========================================================================================== */
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        alturaFrameFormularios = getAlturaTela();
        if (Request.QueryString["CWF"] != null)
        {
            string CssFilePath = "~/App_Themes/Aqua/{0}/styles.css";
            string CssPostFix = "Aqua";
            int codigoWorkFlow = int.Parse(Request.QueryString["CWF"].ToString());
            int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            string formularioEdicao = "renderizaFormulario.aspx?CWF=" + codigoWorkFlow + "&";
            string urlDestinoAposExecutarAcao = "listaProcessos.aspx";

            Workflow myFluxo = new Workflow(cDados.classeDados, idUsuarioLogado, IsPostBack, codigoWorkFlow, null, null, null, 99, this.Page);
            Control pnFluxo = myFluxo.ObtemPainelExecucaoEtapa(CssFilePath, CssPostFix, formularioEdicao, 1024, int.Parse(alturaFrameFormularios), urlDestinoAposExecutarAcao, "_self",true, false);
            pnConteudo.Controls.Add(pnFluxo);
        }
    }

    private string getAlturaTela()
    {
        int alturaTela = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 235).ToString();
    }
}
