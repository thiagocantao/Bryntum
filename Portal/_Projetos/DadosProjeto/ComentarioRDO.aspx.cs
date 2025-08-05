using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class _Projetos_DadosProjeto_ComentarioRDO : System.Web.UI.Page
{
    dados cDados;
    public int codigoProjeto = 0;
    int codigoUsuario = 0;
    int codigoEntidade = 0;
    int codigoRDO = -1;
    DateTime dtCampo = new DateTime();

    bool podeIncluir = false;
    bool podeEditar = false;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, "EN_RespInternoRDO");
        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, "EN_RespInternoRDO");

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
       
        btnSalvar.ClientEnabled = podeEditar;
        htmlComentarios.ClientEnabled = podeEditar;
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        int altura = int.Parse(Request.QueryString["Altura"].ToString());
        altura = altura - 150;


        htmlComentarios.Height = new Unit(altura.ToString() + "px");
        string data = Request.QueryString["DataRdo"] != null ? Request.QueryString["DataRdo"].ToString() : DateTime.Now.ToString("dd/MM/yyyy");

        bool converteu = DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCampo);

        if (converteu)
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
            lblTituloTela.Text = "Comentários - Diário de Obra - Data: " + data;

            setCodigoRDO();

            if (!IsPostBack && !IsCallback)
                carregaDadosRDO();
        }
    }

    private void setCodigoRDO()
    {
        string comandoSQL = string.Format(@"SELECT ISNULL(CodigoRdo, -1) AS CodigoRdo FROM {0}.{1}.Rdo_DadosColetados WHERE DataRdo = CONVERT(DateTime, '{2:dd/MM/yyyy}', 103) AND CodigoProjeto = {3}", cDados.getDbName(), cDados.getDbOwner(), dtCampo, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoRDO = int.Parse(ds.Tables[0].Rows[0]["CodigoRdo"].ToString());
    }

    private void insereRDO()
    {
        string comandoSQL = string.Format(@"
                    DECLARE @CodigoRDO Int
                    INSERT INTO {0}.{1}.Rdo_DadosColetados (DataRdo, CodigoProjeto, DataInclusao, CodigoUsuarioInclusao)
				               VALUES(CONVERT(DateTime, '{2}', 103),           {3},    getdate(),{4})
                    SET @CodigoRDO = SCOPE_IDENTITY()", cDados.getDbName(), cDados.getDbOwner(), dtCampo, codigoProjeto, codigoUsuario);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            codigoRDO = int.Parse(ds.Tables[0].Rows[0]["CodigoRDO"].ToString());
    }

    private void carregaDadosRDO()
    {
        string comandoSQL = string.Format(@"
            SELECT [Comentarios]
              FROM [{0}].[{1}].[Rdo_DadosColetados]
             WHERE CodigoRdo = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoRDO);

        DataSet ds = cDados.getDataSet(comandoSQL);


        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            htmlComentarios.Html = dr["Comentarios"].ToString();
        }
        else
        {
            htmlComentarios.Html = "";
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (codigoRDO == -1)
        {
            insereRDO();
        }

        setCodigoRDO();

        int regAf = 0;

        string comandoSQL = string.Format(@"UPDATE {0}.{1}.Rdo_DadosColetados SET Comentarios = '{2}',   DataUltimaAlteracao = getdate(), CodigoUsuarioAlteracao = {4} WHERE CodigoRdo = {3}", cDados.getDbName(), cDados.getDbOwner(), htmlComentarios.Html.Replace("'", "''"), codigoRDO, codigoUsuario);

        try
        {
            cDados.execSQL(comandoSQL, ref regAf);
            callbackSalvar.JSProperties["cp_Msg"] = "";
            callbackSalvar.ClientSideEvents.EndCallback = "function(s, e){ }";
        }
        catch (Exception ex)
        {
            callbackSalvar.JSProperties["cp_Msg"] = "Erro ao salvar o Comentário: " + Environment.NewLine + ex.Message;
        }
    }
}