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

public partial class _VisaoEPC_visaoCorporativa : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    int codigoEntidade;

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
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"">var statusVC = 1;</script>"));
        // =========================== Verifica se a sessão existe FIM ========================

        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlGesCtt");
            cDados.aplicaEstiloVisual(Page);
        } 
              
        defineLarguraTela();
                
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(0);
            cDados.insereNivel(0, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "PNL_CTEPC", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
        this.Title = cDados.getNomeSistema();

        cDados.setInfoSistema("PeriodicidadeEPC", ddlPeriodicidade.SelectedIndex == -1 ? "-1" : ddlPeriodicidade.Value.ToString());
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 153).ToString() + "px";
    }

    protected void callbackVC_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {        
    }
}
