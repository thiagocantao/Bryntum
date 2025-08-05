using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_DadoWebService_popup : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private int idDadoWebServices = -1;
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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bool retorno = int.TryParse(Request.QueryString["C"] + "", out idDadoWebServices);
        if (!Page.IsPostBack)
        {
            populaTela();
        }

        lblTitulo.Text = Resources.traducao.DadoWebService_popup_t_tulo;
        lblDescricao.Text = Resources.traducao.DadoWebService_popup_descri__o;
        lblSiglaIdentificacao.Text = Resources.traducao.DadoWebService_popup_sigla_de_identifica__o_;
        lblParametrosWebServices.Text = Resources.traducao.DadoWebService_popup_par_metros_web_service;
        lblInstrucaoProjecaoDados.Text = Resources.traducao.DadoWebService_popup_instru__o_de_proje__o_dos_dados;


        cDados.aplicaEstiloVisual(this.Page);
        this.TH(this.TS("DadoWebService_popup"));
    }

    private void populaComboDadosAssociados()
    {
        string comando = string.Format(@"
        SELECT [CodigoTipoAssociacao] 
             , [DescricaoTipoAssociacao]
             , [IniciaisTipoAssociacao]
          FROM dbo.[f_ws_GetClassesObjetoAssociarWs] ({0},{1})", codigoEntidadeLogada, codigoUsuarioLogado);
        DataSet ds1 = cDados.getDataSet(comando);
        ddlConjuntoDadosAssociados.TextField = "DescricaoTipoAssociacao";
        ddlConjuntoDadosAssociados.ValueField = "CodigoTipoAssociacao";
        ddlConjuntoDadosAssociados.DataSource = ds1.Tables[0];
        ddlConjuntoDadosAssociados.DataBind();

    }

    private void populaTela()
    {
        populaComboDadosAssociados();
        string select = string.Format(@"
        SELECT 
            [idDadoWebService]
          , [TituloDadoWebService]
          , [DescricaoDadoWebService]
          , [CodigoTipoObjetoAssociado]
          , [DescricaoTipoAssociacao]
          , [IniciaisDadoWebService]
          , [ParametrosWebService]
          , [InstrucaoSelectDados]
       FROM dbo.f_ws_GetDetalhesIntegracaoWs({0}, {1}, {2})", codigoEntidadeLogada, codigoUsuarioLogado, idDadoWebServices);
        DataSet dsDadoWebService = cDados.getDataSet(select);

        if (cDados.DataSetOk(dsDadoWebService) && cDados.DataTableOk(dsDadoWebService.Tables[0]))
        {
            //modo de edição
            txtTitulo.Text = dsDadoWebService.Tables[0].Rows[0]["TituloDadoWebService"].ToString();
            memoDescricao.Text = dsDadoWebService.Tables[0].Rows[0]["DescricaoDadoWebService"].ToString();
            ddlConjuntoDadosAssociados.Value = dsDadoWebService.Tables[0].Rows[0]["CodigoTipoObjetoAssociado"].ToString();
            ddlConjuntoDadosAssociados.Text = dsDadoWebService.Tables[0].Rows[0]["DescricaoTipoAssociacao"].ToString();

            ddlConjuntoDadosAssociados.ClientEnabled = false;
            txtSiglaIdentificacao.Text = dsDadoWebService.Tables[0].Rows[0]["IniciaisDadoWebService"].ToString();

            memoParametrosWebServices.Text = dsDadoWebService.Tables[0].Rows[0]["ParametrosWebService"].ToString();

            memoProjecaoDados.Text = dsDadoWebService.Tables[0].Rows[0]["InstrucaoSelectDados"].ToString();
            view_divBotoesAplicarECancelar.Style.Add("display", "none");
        }
        else
        {
            //modo de inclusão
            txtTitulo.Text = "";
            memoDescricao.Text = "";
            ddlConjuntoDadosAssociados.ClientEnabled = true;
            btnAlterar.ClientVisible = false;
            div_lblParametrosWebServices.Style.Add("display", "none");
            div_memoParametrosWebServices.Style.Add("display", "none");

            div_lblInstrucaoProjecaoDados.Style.Add("display", "none");
            div_memoProjecaoDados.Style.Add("display", "none");

            view_divBotoesAplicarECancelar.Style.Add("display", "block");
            divBotoes.Style.Add("display", "none");
        }
    }

    protected void callbackDadosSugestao_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {

        string cmdSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidadeContexto int
        DECLARE @in_codigoUsuarioSistema int
        DECLARE @in_idWebServiceDado int
        DECLARE @in_codigoTipoObjetoAssociado smallint
        DECLARE @in_iniciaisTipoObjetoAssociado char(2)
        DECLARE @in_iniciaisDadoWebService varchar(44)
        DECLARE @ou_parametrosWebService nvarchar(max)
        DECLARE @ou_comandoSelectDadosWs nvarchar(max)

        SET @in_codigoEntidadeContexto  = {0} 
        SET @in_codigoUsuarioSistema = {1}
        SET @in_idWebServiceDado = {2}
        SET @in_codigoTipoObjetoAssociado = {3}
        SET @in_iniciaisTipoObjetoAssociado = NULL
        SET @in_iniciaisDadoWebService = '{4}'

        EXECUTE @RC = [dbo].[p_ws_obtemDadosSugestaoTelaCadastroWs] 
           @in_codigoEntidadeContexto
          ,@in_codigoUsuarioSistema
          ,@in_idWebServiceDado
          ,@in_codigoTipoObjetoAssociado
          ,@in_iniciaisTipoObjetoAssociado
          ,@in_iniciaisDadoWebService
          ,@ou_parametrosWebService OUTPUT
          ,@ou_comandoSelectDadosWs OUTPUT

         SELECT @ou_parametrosWebService AS parametrosWebService, @ou_comandoSelectDadosWs AS comandoSelectDadosWs 
", codigoEntidadeLogada, codigoUsuarioLogado, idDadoWebServices, ddlConjuntoDadosAssociados.Value, txtSiglaIdentificacao.Text);

        DataSet dsSugestao = cDados.getDataSet(cmdSQL);
        if (cDados.DataSetOk(dsSugestao))
        {
            ((ASPxCallback)source).JSProperties["cp_parametrosWebService"] = dsSugestao.Tables[0].Rows[0]["parametrosWebService"].ToString();
            ((ASPxCallback)source).JSProperties["cp_comandoSelectDadosWs"] = dsSugestao.Tables[0].Rows[0]["comandoSelectDadosWs"].ToString();

        }
    }

    protected void callbackGravaConfiguracaosDadoWebService_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";

        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidadeContexto int
        DECLARE @in_codigoUsuarioSistema int
        DECLARE @in_idDadoWebService int
        DECLARE @in_tituloDadoWebService varchar(100)
        DECLARE @in_descricaoDadoWebService varchar(500)
        DECLARE @in_codigoTipoObjetoAssociado smallint
        DECLARE @in_iniciaisDadoWebService varchar(44)
        DECLARE @in_parametrosWebService nvarchar(max)
        DECLARE @in_instrucaoSelectDados nvarchar(max)

        SET @in_codigoEntidadeContexto = {0}
        SET @in_codigoUsuarioSistema = {1}
        SET @in_idDadoWebService = {2}
        SET @in_tituloDadoWebService = '{3}'
        SET @in_descricaoDadoWebService = '{4}'
        SET @in_codigoTipoObjetoAssociado = {5}
        SET @in_iniciaisDadoWebService = '{6}'
        SET @in_parametrosWebService = '{7}'
        SET @in_instrucaoSelectDados = '{8}'

        EXECUTE @RC = [dbo].[p_ws_gravaConfiguracaosDadoWebService] 
           @in_codigoEntidadeContexto
          ,@in_codigoUsuarioSistema
          ,@in_idDadoWebService
          ,@in_tituloDadoWebService
          ,@in_descricaoDadoWebService
          ,@in_codigoTipoObjetoAssociado
          ,@in_iniciaisDadoWebService
          ,@in_parametrosWebService
          ,@in_instrucaoSelectDados", codigoEntidadeLogada, codigoUsuarioLogado, idDadoWebServices,
  txtTitulo.Text.Replace("'", "''"), memoDescricao.Text.Replace("'", "''"), ddlConjuntoDadosAssociados.Value, txtSiglaIdentificacao.Text/*aceitar apenas letras, numeros e undeline*/,
  memoParametrosWebServices.Text.Replace("'", "''"), memoProjecaoDados.Text.Replace("'", "''"));

        int contador = 0;
        try
        {
            bool sucesso = cDados.execSQL(comandoSQL, ref contador);
            if (sucesso)
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Configurações gravadas com sucesso!";
                
            }
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }
}