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

public partial class _VisaoNE_visaoCorporativa : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "";
    int codigoEntidade;

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
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
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlGesObr");
            cDados.aplicaEstiloVisual(Page);
        } 
              
        defineLarguraTela();
                
        if (!IsPostBack)
        {
            carregaAnos();
            cDados.excluiNiveisAbaixo(0);
            cDados.insereNivel(0, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "PNL_NE", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
        this.Title = cDados.getNomeSistema();

        
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 215).ToString() + "px";
    }

    private void carregaAnos()
    {
        DataSet ds = cDados.getAnosContratos(codigoEntidade, "");

        ddlAno.DataSource = ds;
        ddlAno.TextField = "AnoContrato";
        ddlAno.ValueField = "AnoContrato";
        ddlAno.DataBind();

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && !IsPostBack)
        {

            if (cDados.getInfoSistema("AnoContrato") != null)
                ddlAno.Value = cDados.getInfoSistema("AnoContrato").ToString();
            else
            {
                if (ddlAno.Items.FindByValue(DateTime.Now.Year.ToString()) != null)
                    ddlAno.Value = DateTime.Now.Year.ToString();
                else
                    ddlAno.SelectedIndex = 0;
            }
        }


        cDados.setInfoSistema("AnoContrato", ddlAno.SelectedIndex == -1 ? "-1" : ddlAno.Value.ToString());

    }

    protected void callbackVC_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        carregaAnos();
    }
}
