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

public partial class administracao_CadastroRamosAtividades : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private int codigoProjeto = -1;
    private int codigoProjetoAgil = -1;



    private int codigoItem = -1;

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    private int alturaUrl = 0;

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        bool retorno = int.TryParse(Request.QueryString["ALT"] + "", out alturaUrl);
                
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CodigoItem"] != null && Request.QueryString["CodigoItem"].ToString() != "")
            codigoItem = int.Parse(Request.QueryString["CodigoItem"].ToString());

        bool indicaStatusNAOIniciado = verificaSeItemNaoFoiIniciado();
        bool indicaSprintEmExecucao = verificaSeSprintEstaEmExecucao();
        if (indicaStatusNAOIniciado == false && indicaSprintEmExecucao == false)
        {
            podeEditar = false;
            podeExcluir = false;
            podeIncluir = false;
        }

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        cDados.aplicaEstiloVisual(Page);


        if (!IsPostBack)
        {
            carregaGvDados();
        }

        cDados.setaTamanhoMaximoMemo(mmDetalhes, 4000, lbl_mmDetalhes);

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        if(alturaUrl > 0)
        {
            gvDados.Settings.VerticalScrollableHeight = alturaUrl - 150;
        }
        else
        {
            gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 410;
        }
        //    var url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/popupItensBacklog.aspx?CI=" + codigoItem + "&acao=editarTarefasItem&IDProjeto=" + gvDados.cpCodigoProjeto + "&CPA=" + gvDados.cpCodigoProjetoAgil;
        bool retorno = int.TryParse(Request.QueryString["IDProjeto"] + "", out codigoProjeto);
        retorno = int.TryParse(Request.QueryString["CPA"] + "", out codigoProjetoAgil);

        gvDados.JSProperties["cpCodigoProjeto"] = codigoProjeto;
        gvDados.JSProperties["cpCodigoProjetoAgil"] = codigoProjetoAgil;
        gvDados.JSProperties["cpCodigoItem"] = codigoItem;
        this.TH(this.TS("TarefasItemBacklog", "barraNavegacao"));
    }

    private bool verificaSeSprintEstaEmExecucao()
    {
        bool retorno = false;
        //define se o item está com o status "não iniciado"
        string comandoSQL = string.Format(@"
            SELECT 1
              FROM Agil_ItemBacklog AS ib
             inner join Projeto p on (ib.CodigoProjeto = p.[CodigoProjeto])
             inner join Status s on (s.CodigoStatus = p.CodigoStatusProjeto)                                         
             WHERE ib.CodigoItem = {0} and s.IniciaisStatus = 'EM_EXECUCAO'", codigoItem);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = (ds.Tables[0].Rows[0][0].ToString().Trim() == "1");
        }
        return retorno;
    }

    private bool verificaSeItemNaoFoiIniciado()
    {
        bool retorno = false;
        //define se o item está com o status "não iniciado"
        string comandoSQL = string.Format(@"
            SELECT 1
              FROM Agil_ItemBacklog AS ib
             inner join Agil_TipoStatusItemBacklog tsib on (tsib.CodigoTipoStatusItem = ib.[CodigoTipoStatusItem])                                         
             WHERE ib.CodigoItem = {0} and tsib.IniciaisTipoStatusItemControladoSistema = 'SP_NAOINI'"
            , codigoItem);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = (ds.Tables[0].Rows[0][0].ToString().Trim() == "1");
        }
        return retorno;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/TarefasItemBacklog.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "TarefasItemBacklog", "_Strings"));

    }
    
    #endregion
    
    #region GRID

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
            SELECT ib.CodigoItem,
                   ib.TituloItem,
                   ib.DetalheItem,
                   ib.EsforcoPrevisto
              FROM Agil_ItemBacklog AS ib                                       
             WHERE (ib.CodigoItemSuperior = {0} AND 
                   ib.CodigoUsuarioExclusao IS NULL) AND
                    ISNULL(ib.IndicaTarefa, 'S') = 'S'"
            , codigoItem);

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds;
        gvDados.DataBind();     
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {        
        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion
    
    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteInclusaoRegistro()
    {
        string tituloTarefa = txtTituloTarefa.Text.Replace("'", "''");
        string esforcoPrevisto = txtEsforcoPrevisto.Text == "" ? "NULL" : txtEsforcoPrevisto.Text.Replace(',', '.');
        string detalhe = mmDetalhes.Text.Replace("'", "''");

        string comandoSQL = string.Format(@"            
	            INSERT INTO [dbo].[Agil_ItemBacklog]
                   ([CodigoProjeto]
                   ,[CodigoItemSuperior]
                   ,[TituloItem]
                   ,[DetalheItem]
                   ,[CodigoTipoStatusItem]
                   ,[CodigoTipoClassificacaoItem]
                   ,[CodigoUsuarioInclusao]
                   ,[DataInclusao]
                   ,[CodigoIteracao]
                   ,[Importancia]
                   ,[Complexidade]
                   ,[EsforcoPrevisto]
                   ,[IndicaItemNaoPlanejado]
                   ,[IndicaQuestao]
                   ,[IndicaBloqueioItem]
                   ,[IndicaTarefa])
            SELECT [CodigoProjeto]
                  ,{0}
                  ,'{1}'
                  ,'{3}'
                  ,[CodigoTipoStatusItem]
                  ,[CodigoTipoClassificacaoItem]
                  ,{4}
                  ,GetDate()
                  ,[CodigoIteracao]
                  ,[Importancia]
                  ,[Complexidade]
                  ,{2}
                  ,'N'
                  ,'N'
                  ,'N'
                  ,'S'
              FROM [dbo].[Agil_ItemBacklog]
             WHERE CodigoItem = {0}
            ", codigoItem
             , tituloTarefa
             , esforcoPrevisto
             , detalhe
             , codigoUsuarioResponsavel);

        int regAf = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoTarefa = int.Parse(getChavePrimaria());
        string tituloTarefa = txtTituloTarefa.Text.Replace("'", "''");
        string esforcoPrevisto = txtEsforcoPrevisto.Text == "" ? "NULL" : txtEsforcoPrevisto.Text.Replace(',', '.');
        string detalhe = mmDetalhes.Text.Replace("'", "''");

        string comandoSQL = string.Format(@"
            UPDATE [dbo].[Agil_ItemBacklog]
                SET [TituloItem] = '{1}'
                   ,[DetalheItem] = '{3}'
                   ,EsforcoPrevisto = {2}
                   ,[CodigoUsuarioUltimaAlteracao] = {4}
                   ,[DataUltimaAlteracao] = GetDate()
                WHERE CodigoItem = {0}"
             , codigoTarefa
             , tituloTarefa
             , esforcoPrevisto
             , detalhe
             , codigoUsuarioResponsavel);

        int regAf = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao editar o registro!";
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoTarefa = int.Parse(getChavePrimaria());

        string comandoSQL = string.Format(@"
            UPDATE [dbo].[Agil_ItemBacklog]
               SET [CodigoUsuarioExclusao] = {1}
             WHERE CodigoItem = {0}"
             , codigoTarefa
             , codigoUsuarioResponsavel);

        int regAf = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao editar o registro!";
        else
        {
            carregaGvDados();
            return "";
        }
    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void callbackPopupItemBacklog_Callback(object source, CallbackEventArgs e)
    {
        var codigoItem = "-1";
        var acao = "";
        var codigoProjeto = "-1";
        var codigoProjetoAgil = "-1";

        var descricaoItemSuperior = "";
        var codigoItemSuperior = "-1";

        var arrayParametrosRecebidos = e.Parameter.Split('|');

        codigoItem = arrayParametrosRecebidos[0];
        acao = arrayParametrosRecebidos[1];
        codigoProjeto = arrayParametrosRecebidos[2];
        codigoProjetoAgil = arrayParametrosRecebidos[3];

        string comandoSQL = string.Format(@"
        SELECT ib.[CodigoItem], ibSup.[CodigoItem] as CodigoItemSuperior, ibSup.TituloItem as TituloItemSuperior
        FROM [dbo].[Agil_ItemBacklog] ib
        INNER JOIN [Agil_ItemBacklog] ibSup on (ibSup.CodigoItem = ib.CodigoItemSuperior)
        WHERE ib.CodigoItem = {0}", codigoItem.Replace(",", ""));
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            descricaoItemSuperior = ds.Tables[0].Rows[0]["TituloItemSuperior"].ToString();
            codigoItemSuperior = ds.Tables[0].Rows[0]["CodigoItemSuperior"].ToString();
        }
        ((ASPxCallback)source).JSProperties["cpCodigoItem"] = codigoItem;
        ((ASPxCallback)source).JSProperties["cpAcao"] = acao;
        ((ASPxCallback)source).JSProperties["cpCodigoProjeto"] = codigoProjeto;
        ((ASPxCallback)source).JSProperties["cpCodigoProjetoAgil"] = codigoProjetoAgil;
        ((ASPxCallback)source).JSProperties["cpDescricaoItemSuperior"] = descricaoItemSuperior;
        ((ASPxCallback)source).JSProperties["cpCodigoItemSuperior"] = codigoItemSuperior;
    }
}
