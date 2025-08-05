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

public partial class _VisaoExecutiva_visaoCorporativa : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "", telaVisao = "";

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

        int codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlExe");
        }

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        defineLarguraTela();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(0);
            cDados.insereNivel(0, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "PNL_EX", "ENT", -1, "Adicionar Painel aos Favoritos");
        }
        this.Title = cDados.getNomeSistema();

        telaVisao = "./Graficos/visaoCorporativa_01.aspx";

        if (Request.QueryString["TipoVisao"] != null && Request.QueryString["TipoVisao"].ToString() == "2")
        {
            telaVisao = "./Graficos/visaoCorporativa_02.aspx";
        }

        int codigoCarteira = int.Parse(cDados.getInfoSistema("CodigoCarteira").ToString());

        DataSet dsProjetos = cDados.f_getProjetosUsuario(codigoUsuarioResponsavel, codigoEntidade, codigoCarteira);

        string codigosProjetos = "-1";

        if (cDados.DataSetOk(dsProjetos) && cDados.DataTableOk(dsProjetos.Tables[0]))
        {
            foreach (DataRow dr in dsProjetos.Tables[0].Rows)
            {
                codigosProjetos += "," + dr["CodigoProjeto"];
            }
        }

        cDados.setInfoSistema("CodigosProjetosUsuario", codigosProjetos);
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 185).ToString() + "px";
    }    
}
