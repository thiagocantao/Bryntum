using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_DadoWebService : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
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

        this.TH(this.TS("RecursosCorporativos", "barraNavegacao"));

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        populaGrid();
        HeaderOnTela();
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Cadastro De Dados de Webservice", "DADWBS", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }

    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/DadoWebService.js""></script>"));
    }
    private void populaGrid()
    {

        string select = string.Format(@"
        SELECT 
        [idDadoWebService] 
        , [TituloDadoWebService]
        , [DescricaoDadoWebService]
        , [CodigoTipoObjetoAssociado]
        , [DescricaoTipoAssociacao]
        , [IniciaisDadoWebService] 
       FROM dbo.f_ws_GetIntegracoesWs({0}, {1})", codigoEntidadeLogada, codigoUsuarioLogado);
        DataSet dsGrid = cDados.getDataSet(select);

        if (cDados.DataSetOk(dsGrid))
        {
            gvDados.DataSource = dsGrid;
            gvDados.DataBind();
        }
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadPerfAcsFlx");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "incluirDado();", true, true, false, "DadosWebs", "Dados WebServices", this);
    }



    protected void callbackExcluiRegistro_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";

        string cmd = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidadeContexto int
        DECLARE @in_codiogUsuarioSistema int
        DECLARE @in_idDadoWebService int

        SET @in_codigoEntidadeContexto = {0}
        SET @in_codiogUsuarioSistema = {1}
        SET @in_idDadoWebService = {2}

        EXECUTE @RC = [dbo].[p_ws_excluiRegistroIntegracaoWs] 
           @in_codigoEntidadeContexto
          ,@in_codiogUsuarioSistema
          ,@in_idDadoWebService", codigoEntidadeLogada, codigoUsuarioLogado, e.Parameter);

        int quantidade = 0;
        try
        {
            bool sucesso = cDados.execSQL(cmd, ref quantidade);
            if (sucesso)
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Registros excluídos com sucesso!";
            }
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }

    protected void callbackAtualizaDadoIntegracao_Callback(object source, CallbackEventArgs e)
    {
        //IniciaisDadoWebService + '|' + idDadoWebService)
        string[] valores = e.Parameter.Split('|');



        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";

        string cmd = string.Format(@"
        DECLARE @RC int
        DECLARE @in_codigoEntidadeContexto int
        DECLARE @in_iniciaisDadoWebService varchar(44)
        DECLARE @in_codigoObjetoContexto bigint

        SET @in_codigoEntidadeContexto = {0}
        SET @in_iniciaisDadoWebService = '{1}'
        SET @in_codigoObjetoContexto = NULL

        EXECUTE @RC = [dbo].[p_ws_AtualizaRegistrosDadoWebService] 
           @in_codigoEntidadeContexto
          ,@in_iniciaisDadoWebService
          ,@in_codigoObjetoContexto", codigoEntidadeLogada, e.Parameter);

        int quantidade = 0;
        try
        {
            bool sucesso = cDados.execSQL(cmd, ref quantidade);
            if (sucesso)
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Dados atualizados com sucesso!";
            }
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }
}