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
using System.Collections.Generic;
using System.Xml;

public partial class _Projetos_DadosProjeto_EdicaoEAP : System.Web.UI.Page
{
    #region --- Variáveis usadas na montagem do HTML
    public string webServicePath = "";  // caminho do web service
    public string doubleClipTarea = ""; // link del popup.
    public string codigoEAP = "";       // código do controle de edição da EAP
    public string alturaObject = "";
    public string alturaObjectFlash = "";
    public string largoObject = "";

    #endregion

    dados cDados;
    private string bancodb;
    private string Ownerdb;
    private string codigoProjeto = "";
    private string IdUsuario = "";
    private string IdEntidade = "";
    private string nomeProjeto = "";
    private string modoAcessoDesejado = "";
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    public class Tarefas
    {
        private string _nombre;
        /// <summary>
        /// Nombre de la tarea.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private int _nivel;
        /// <summary>
        /// Nivel de la tarea.
        /// </summary>
        public int Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        private int _codigoTarea;
        /// <summary>
        /// Codigo de la Tarea
        /// </summary>
        public int CodigoTarea
        {
            get { return _codigoTarea; }
            set { _codigoTarea = value; }
        }

        public Tarefas(string nombre, int nivel, int codigoTarea)
        {
            this.Nombre = nombre;
            this.Nivel = nivel;
            this.CodigoTarea = codigoTarea;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Ownerdb = cDados.getDbOwner();
        bancodb = cDados.getDbName();

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
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = Request.QueryString["IDProjeto"].ToString();
        }
        if (Request.QueryString["CE"] != null)
            IdEntidade = Request.QueryString["CE"].ToString();
        if (Request.QueryString["CU"] != null)
            IdUsuario = Request.QueryString["CU"].ToString();
        if (Request.QueryString["NP"] != null)
            nomeProjeto = Request.QueryString["NP"].ToString();
        if (Request.QueryString["AM"] != null)
            modoAcessoDesejado = Request.QueryString["AM"].ToString();
        if (modoAcessoDesejado == "RW")
        {
            modoAcessoDesejado = "G";
        }
        else
        {
            modoAcessoDesejado = "L";
        }

        // se a tela ainda não foi processada 
        if (!hfGeral.Contains("AutoRecargaTela"))
        {
            string modoAcessoFinal;
            object CheckoutBy;

            webServicePath = getWebServicePath();

            codigoEAP = getIDEdicaoEAP(modoAcessoDesejado, out modoAcessoFinal, out CheckoutBy);

            hfGeral.Set("AutoRecargaTela", "S");
            hfGeral.Set("codigoEAP", codigoEAP);
            hfGeral.Set("ModoAcessoDesejado", modoAcessoDesejado);
            hfGeral.Set("ModoAcessoFinal", modoAcessoFinal);

            // os modos só serão diferentes quando se desejar acessar RW e não se consegue
            if (modoAcessoDesejado != modoAcessoFinal)
            {
                if (CheckoutBy == System.DBNull.Value)
                    hfGeral.Set("MensajeBloqueio", "EAP aberta apenas para leitura.");
                else
                    hfGeral.Set("MensajeBloqueio", "EAP aberta apenas para leitura, pois o conograma deste projeto está bloqueado para edição por " + CheckoutBy.ToString());

                hfGeral.Set("Parametros", "&MO=V");
            }
            else
                hfGeral.Set("MensajeBloqueio", "");
                        

            if (modoAcessoFinal == "G")
            {
                Header.Controls.Add(cDados.getLiteral(@"<link rel=""shortcut icon"" href=""../../imagens/WBSTools/edicao.png"">"));
                doubleClipTarea = "popupEdicaoTarefaEAP.aspx";
                hfGeral.Set("Parametros", "&MO=E");
                //MO=E
            }
            else
            {
                Header.Controls.Add(cDados.getLiteral(@"<link rel=""shortcut icon"" href=""../../imagens/WBSTools/photo.png"">"));
                doubleClipTarea = "popupEdicaoTarefaEAP.aspx";
                hfGeral.Set("Parametros", "&MO=V");
                //MO=V
                lblModoVisualizacao.Text = "   .: modo Visualização :.";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

    }
           
    private string getWebServicePath()
    {
        string url = string.Empty;
        HttpRequest request = HttpContext.Current.Request;

        if (request.IsSecureConnection)
            url = "https://";
        else
            return cDados.getPathSistema() + "wsPortal.asmx?WSDL";

        url += request["HTTP_HOST"] + cDados.getPathSistema() + "wsPortal.asmx?WSDL";

        return url;
    }

    private string getIDEdicaoEAP(string modoAcessoDesejado, out string modoAcessoFinal, out object CheckoutBy)
    {
        CheckoutBy = System.DBNull.Value;
        modoAcessoFinal = "L";
        string idEdicao = "";
        string comandoSQL = string.Format(@"
            DECLARE
		              @IdEdicaoEAP          Varchar(64)
	                , @CheckoutBy           Varchar(64)
                    , @modoAcessoFinal      char(1)
            	
            EXEC {0}.{1}.[p_crono_GeraIDEdicaoEAP]
		                  @in_codigoProjeto         = {2}
	                    , @in_codigoUsuarioEdicao   = {3}
	                    , @in_modoAcessoDesejado    = {4}
	                    , @ou_IdEdicaoEAP           = @IdEdicaoEAP          OUTPUT
	                    , @ou_checkoutBy            = @CheckoutBy           OUTPUT
                        , @ou_modoAcessoFinal       = @modoAcessoFinal      OUTPUT

            SELECT @IdEdicaoEAP AS CodigoEAP, @CheckoutBy AS CheckoutBy, @modoAcessoFinal AS ModoAcessoFinal
            ", bancodb, Ownerdb, codigoProjeto, IdUsuario, modoAcessoDesejado);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            idEdicao = ds.Tables[0].Rows[0]["codigoEAP"].ToString();
            CheckoutBy = ds.Tables[0].Rows[0]["CheckoutBy"];
            modoAcessoFinal = ds.Tables[0].Rows[0]["ModoAcessoFinal"].ToString();
        }
        return idEdicao;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        //Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        int altura = (alturaPrincipal - 130);
        if (altura > 0)
        {
            alturaObject = altura + "px";
            alturaObjectFlash = altura + "";
            largoObject = largura - 110 + "";
        }
        //alturaObject = Request.QueryString["Altura"] + "" == "" ? alturaPrincipal + "" : int.Parse(Request.QueryString["Altura"].ToString()) - 55 + "";
    }

    protected void callbackGeral_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        ////fazer as mudanças do desbloquei do cronograma.
        ////pasando como parametro o codigoEAP
        //desbloquear();
    }
}

