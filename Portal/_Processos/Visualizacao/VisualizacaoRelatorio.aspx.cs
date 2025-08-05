using DevExpress.Web;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Processos_Visualizacao_VisualizacaoRelatorio : System.Web.UI.Page
{
    #region Constants

    const string CONST_CodigoFluxo = "CF";
    const string CONST_CodigoProjeto = "CP";
    const string CONST_CodigoWorkflow = "CW";
    const string CONST_CodigoInstanciaWF = "CI";
    const string CONST_CodigoEtapaInicial = "EI";
    const string CONST_CodigoEtapaAtual = "CE";
    const string CONST_OcorrenciaAtual = "CS";
    const string CONST_CodigoStatus = "ST";
    const string CONST_Etapa = "ET";
    const Int16 CONST_LarguraMinimaColuna = 100;

    #endregion

    #region Fields

    dados cDados;
    int codigoLista;
    string dbName;
    string dbOwner;
    int codigoUsuarioLogado;
    int codigoEntidade;
    int codigoCarteira;
    protected bool indicaDadosInicializados;
    protected int alturaTela;
    protected int larguraTela;

    #endregion

    #region Properties

    private string _ConnectionString;
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
        set { _ConnectionString = value; }
    }

    private string _IdRelatorio;
    public string IdRelatorio
    {
        get
        {
            if (string.IsNullOrEmpty(_IdRelatorio))
            {
                string comandoSql;

                #region Comando SQL

                comandoSql = string.Format(@"

DECLARE @CodigoLista INT
    SET @CodigoLista = {0}
    
 SELECT l.IDRelatorio 
   FROM Lista AS l 
  WHERE l.CodigoLista = @CodigoLista",
  codigoLista);

                #endregion

                DataSet ds = cDados.getDataSet(comandoSql);
                _IdRelatorio = ds.Tables[0].Rows[0]["IDRelatorio"] as string;
            }
            return _IdRelatorio;
        }
    }

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }

        indicaDadosInicializados = false;
        string cl = Request.QueryString["cl"];
        if (cl == null) return;

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoLista = int.Parse(Request.QueryString["cl"]);
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));

        this.TH(this.TS("VisualizacaoRelatorio"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DefineRelatorio();

        if (!(IsPostBack || IsPostBack))
            DefineDimensoesTela();
    }

    protected void documentViewer_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        e.Properties.Add("cp_IdRelatorio", IdRelatorio);
    }

    #endregion

    #region Methods

    private void DefineDimensoesTela()
    {
        string res = cDados.getInfoSistema("ResolucaoCliente").ToString();
        larguraTela = int.Parse(res.Split('x')[0]);
        alturaTela = int.Parse(res.Split('x')[1]);
    }

    private void DefineRelatorio()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @IDRelatorio uniqueidentifier,
        @CodigoUsuario INT
    SET @IDRelatorio = '{0}'
    SET @CodigoUsuario = {1}
  
 SELECT ISNULL(ru.ConteudoRelatorio, mr.ConteudoRelatorio) AS ConteudoRelatorio
   FROM ModeloRelatorio AS mr LEFT JOIN
        RelatorioUsuario AS ru ON ru.IDRelatorio = mr.IDRelatorio AND ru.CodigoUsuario = @CodigoUsuario
  WHERE mr.IDRelatorio = @IDRelatorio",
  IdRelatorio, codigoUsuarioLogado);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        var reportLayout = ds.Tables[0].Rows[0]["ConteudoRelatorio"] as byte[];
        XtraReport relatorio = ObtemRelaroio(reportLayout);
        //relatorio.Parameters["pUrlLogo"].Value = ObtemUrlLogo();
        documentViewer.Report = relatorio;
        string res = cDados.getInfoSistema("ResolucaoCliente").ToString();
        larguraTela = int.Parse(res.Split('x')[0]);
        alturaTela = int.Parse(res.Split('x')[1]);
        documentViewer.Height = alturaTela - 195;
    }

    private static XtraReport ObtemRelaroio(byte[] reportLayout)
    {
        if (reportLayout == null)
            return new XtraReport();

        using (var stream = new MemoryStream(reportLayout))
            return XtraReport.FromStream(stream, true);
    }

    private static string ObtemUrlLogo()
    {
        var dados = new dados(null);
        var imgLogo = dados.ObtemLogoEntidade();
        var caminhoFisicoArquivo = Path.Combine(
            HostingEnvironment.ApplicationPhysicalPath, "ArquivosTemporarios",
            Path.ChangeExtension(Path.GetRandomFileName(), "bmp"));

        imgLogo.Save(caminhoFisicoArquivo);

        return caminhoFisicoArquivo;
    }

    #endregion

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        switch (e.Parameter)
        {
            case "restaurar":
                string comandoSql;

                #region ComandoSql

                comandoSql = string.Format("DELETE FROM RelatorioUsuario WHERE IDRelatorio = '{0}' AND CodigoUsuario = {1}", IdRelatorio, codigoUsuarioLogado);

                #endregion

                int registrosAfetados = 0;
                cDados.execSQL(comandoSql, ref registrosAfetados);

                break;
            default:
                break;
        }
    }
}