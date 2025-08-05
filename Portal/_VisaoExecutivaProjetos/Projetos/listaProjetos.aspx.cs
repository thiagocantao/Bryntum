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

public partial class _VisaoExecutivaProjetos_Projetos_listaProjetos : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade = 0;
    private int idUsuarioLogado;
    public string alturaTela = "";
    public string paginaIncialMetas = "", displayLaranja = "";

    string verde = "", amarelo = "", vermelho = "", laranja = "", branco = "", mostrarFechados = "&Fechados=S";

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

        DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao", "MostraFisicoLaranja");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
            {
                string definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
                lblQuestao.Text = definicaoQuestao;
            }

            if (dsParametros.Tables[0].Rows[0]["MostraFisicoLaranja"].ToString().Trim() == "N")
                displayLaranja = "display:none;";
        }

        if (!IsPostBack)
        {

            if (Request.QueryString["Verde"] != null && Request.QueryString["Verde"].ToString() != "")
                ckbVerde.Checked = Request.QueryString["Verde"].ToString() == "S";

            if (Request.QueryString["Amarelo"] != null && Request.QueryString["Amarelo"].ToString() != "")
                ckbAmarelo.Checked = Request.QueryString["Amarelo"].ToString() == "S";

            if (Request.QueryString["Vermelho"] != null && Request.QueryString["Vermelho"].ToString() != "")
                ckbVermelho.Checked = Request.QueryString["Vermelho"].ToString() == "S";

            if (Request.QueryString["Laranja"] != null && Request.QueryString["Laranja"].ToString() != "")
                checkLaranja.Checked = Request.QueryString["Laranja"].ToString() == "S";

            if (Request.QueryString["Branco"] != null && Request.QueryString["Branco"].ToString() != "")
                ckbBranco.Checked = Request.QueryString["Branco"].ToString() == "S";

            if (Request.QueryString["Fechados"] != null && Request.QueryString["Fechados"].ToString() != "")
                mostrarFechados = "&Fechados=" + Request.QueryString["Fechados"].ToString();

        }

        verde = ckbVerde.Checked ? "Verde=S&" : "Verde=N&";

        amarelo = ckbAmarelo.Checked ? "Amarelo=S&" : "Amarelo=N&";

        vermelho = ckbVermelho.Checked ? "Vermelho=S&" : "Vermelho=N&";

        laranja = checkLaranja.Checked ? "Laranja=S&" : "Laranja=N&";

        branco = ckbBranco.Checked ? "Branco=S" : "Branco=N";

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                
        cDados.aplicaEstiloVisual(this);

        defineLarguraTela();

        int nivel = 0;

        if (Request.QueryString["NivelNavegacao"] != null && Request.QueryString["NivelNavegacao"].ToString() != "")
            nivel = int.Parse(Request.QueryString["NivelNavegacao"].ToString());

        if (!IsPostBack)
        {            
            if (nivel > 0)
                lblTituloTela.Text = "Painel Executivo de Projetos";

            cDados.excluiNiveisAbaixo(nivel);
            cDados.insereNivel(nivel, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "VISPEX", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }

        paginaIncialMetas = "../ListaProjetos/visaoProjetos_01.aspx?NomeProjeto=" + txtNomeProjeto.Text + "&" + verde + amarelo + vermelho + laranja + branco + mostrarFechados + "&NivelNavegacao=" + nivel;

        string estiloFooter = "dxeTextBox";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxeTextBox_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);

        tbBotoes.Style.Add("padding", "3px");

        tbBotoes.Style.Add("border-collapse", "collapse");
    }   

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 190).ToString() + "px";
    }  
}
