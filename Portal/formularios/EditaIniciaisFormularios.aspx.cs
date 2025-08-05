using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class formularios_EditaIniciaisFormularios : System.Web.UI.Page
{
    #region Constants

    private const string Const_Sim = "S";
    private const string Const_Nao = "N";
    private const string Const_SenhaAcesso = "rsenha20";

    #endregion

    #region Fields

    dados cDados;
    int codigoUsuarioLogado;
    int codigoEntidade;

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

    #endregion

    #region Event Handlers

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
            Response.RedirectLocation = String.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }
        sdsListaFormularios.ConnectionString = ConnectionString;
       
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        cDados.aplicaEstiloVisual(this);
        Session["ce"] = codigoEntidade;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        
        
        RestringeAcessoPagina();
        defineAlturaTela();
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/EditaIniciaisFormulario.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
    }

    protected void defineAlturaTela()
    {
        /*<div id="divConteudo" runat="server" style="margin: auto; width: 98%;">*/

        int larguraTela = 0;
        int alturaTela = 0;

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out larguraTela, out alturaTela);

        alturaTela = alturaTela - 245;

        gvDados.Settings.VerticalScrollableHeight = alturaTela;
        gvDados.Width = new System.Web.UI.WebControls.Unit("98%");
    }

    private void RestringeAcessoPagina()
    {
        bool acessoPermitido = false;
        if (hfGeral.Contains("AcessoPermitido"))
            acessoPermitido = (bool)hfGeral.Get("AcessoPermitido");
        if (!acessoPermitido)
        {
            acessoPermitido = txtSenha.Text.Equals(Const_SenhaAcesso);
            hfGeral.Set("AcessoPermitido", acessoPermitido);
            lblMensagem.ClientVisible = IsPostBack && !acessoPermitido;
        }
        gvDados.ClientVisible = acessoPermitido;
        popup.ShowOnPageLoad = !acessoPermitido;
    }
    #endregion
    protected void gvDados_CustomErrorText(object sender, DevExpress.Web.ASPxGridViewCustomErrorTextEventArgs e)
    {

    }
    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string IndicaControladoSistema = (e.NewValues["IndicaControladoSistema"] == null) ? "N" : e.NewValues["IndicaControladoSistema"].ToString();
        string IniciaisFormularioControladoSistema = (e.NewValues["IniciaisFormularioControladoSistema"] == null) ? "" : e.NewValues["IniciaisFormularioControladoSistema"].ToString();
        string CodigoModeloFormulario = e.Keys[0].ToString();
        string retorno = "";

        string comandosql = string.Format(@"
        DECLARE @IndicaControladoSistema char(1)
        DECLARE @CodigoModeloFormulario int
        DECLARE @IniciaisFormularioControladoSistema varchar(24)
        
        SET @IndicaControladoSistema = '{2}'
        SET @CodigoModeloFormulario = {3}
        SET @IniciaisFormularioControladoSistema = '{4}'

        UPDATE {0}.{1}.[ModeloFormulario]
           SET [IndicaControladoSistema] = @IndicaControladoSistema
              ,[IniciaisFormularioControladoSistema] = @IniciaisFormularioControladoSistema
        WHERE CodigoModeloFormulario = @CodigoModeloFormulario", cDados.getDbName(), cDados.getDbOwner(), IndicaControladoSistema, CodigoModeloFormulario, IniciaisFormularioControladoSistema);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + " " + comandosql + " " + cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }

        ((ASPxGridView)sender).JSProperties["cp_Erro"] = retorno;
        ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados atualizados com sucesso!";

        if (retorno == "OK")
        {
            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        }
    }
}
