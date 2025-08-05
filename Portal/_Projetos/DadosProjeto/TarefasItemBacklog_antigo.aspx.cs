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

public partial class _Projetos_DadosProjeto_TarefasItemBacklog_antigo : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private int codigoItem = -1;

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

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
            //podeExcluir = false;
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
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 410;
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
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
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
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/TarefasItemBacklog_antigo.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));

        this.TH(this.TS("TarefasItemBacklog_antigo", "barraNavegacao"));


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
             WHERE  ib.CodigoItemSuperior = {0} 
               AND  ib.CodigoUsuarioExclusao IS NULL
               AND  ib.[CodigoTipoStatusItem] <> (SELECT atsib.CodigoTipoStatusItem FROM [dbo].[Agil_TipoStatusItemBacklog] atsib WHERE atsib.IniciaisTipoStatusItemControladoSistema = 'SP_PRONTO')"
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
            //podeExcluir = verificaSeItemNaoFoiIniciado();
            if (podeExcluir) 
            {
                e.Enabled = true;
            }
            //else
            //{
            //    e.Enabled = false;
            //    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            //    e.Image.ToolTip = "Não é permitido excluir tarefa que já possui andamento.";
            //}
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
                   ,[IndicaBloqueioItem])
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
}
