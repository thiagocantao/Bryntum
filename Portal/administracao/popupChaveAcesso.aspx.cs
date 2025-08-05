using DevExpress.Web;
using System;
using System.Data;
using System.Text;

public partial class administracao_popupChaveAcesso : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string clientSecret = "";
    private int codigoUsuarioSelecionado = -1;
    private string clientID = "";
    protected void Page_Init(object sender, EventArgs e)
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

        this.TH(this.TS("chaveAcessoExternoAPI"));

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        codigoUsuarioSelecionado = int.Parse(Request.QueryString["C"] + "");
        clientID = Request.QueryString["CI"] + "";
        if (!Page.IsPostBack)
        {
            clientSecret = Request.QueryString["CS"] + "";
        }
        
        cDados.aplicaEstiloVisual(this);

        txtClientID.Text = clientID == "null" ? "" : clientID;

        DataSet ds = cDados.getUsuarios(" AND CodigoUsuario = " + codigoUsuarioSelecionado);

        txtDescricaoUsuario.Text = ds.Tables[0].Rows[0]["NomeUsuario"].ToString() + " (" + ds.Tables[0].Rows[0]["EMail"].ToString() + ")";


        StringBuilder sb = new StringBuilder();
        sb.Append(@"

                   ");
        
       // htmlTextoExplicativo.Html = sb.ToString();
    }

    protected void callbackGeraChave_Callback1(object sender, CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(clientSecret))
        {
            
            int client_secret_ComHashCode = -1;

            string comando = "exec dbo.p_geraGUID";
            DataSet ds = cDados.getDataSet(comando);
            clientSecret = ds.Tables[0].Rows[0][0].ToString();
            client_secret_ComHashCode = cDados.ObtemCodigoHash(clientSecret);

            string comandoSQLCredencia = string.Format(@"[dbo].[p_credenciaUsuarioAcessoViaAPI]
         @codigoEntidade = {0}
        , @codigoUsuarioCredenciado   = {1}
        , @usuarioCredenciadorAcesso  = {2}
        , @client_secret              = {3}", codigoEntidadeLogada, codigoUsuarioSelecionado, codigoUsuarioLogado, client_secret_ComHashCode);

            DataSet ds1 = cDados.getDataSet(comandoSQLCredencia);
        }
        txtClientSecret.Text = clientSecret;
        clientSecret = "";
    }

   
}