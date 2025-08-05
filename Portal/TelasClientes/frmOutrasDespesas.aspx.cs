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
    private int alturaPrincipal = 0;
    private int codigoWorkflow = 0;
    private int codigoInstanciaWf = 0;
    
    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

    protected bool exibirColunas = false;

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

        if (!string.IsNullOrEmpty(Request.QueryString["exibirColunas"]))
            exibirColunas = Request.QueryString["exibirColunas"].ToLower() == "s";

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());

        if (Request.QueryString["CIWF"] != null && Request.QueryString["CIWF"].ToString() != "")
            codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() != "")
            podeEditar = Request.QueryString["RO"].ToString() != "S";
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;
                
        this.Title = cDados.getNomeSistema();

        gvDados.Columns["QuantidadeReal"].Visible = exibirColunas;
        txtQuantidadeReal.ClientVisible = exibirColunas;
        lblQuantidadeReal.ClientVisible = exibirColunas;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {            
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            
        }
        
        carregaComboItens();
                
        if (!IsPostBack)
        {
            carregaGvDados();            
        }
        cDados.aplicaEstiloVisual(Page);

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gvDados.Settings.ShowFilterRow = false;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/frmOutrasDespesas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(Request.QueryString["ALT"].ToString());

        gvDados.Settings.VerticalScrollableHeight = (alturaPrincipal - 200)/2;
    }
    #endregion
    
    #region GRID

    private void carregaGvDados()
    {
        string comandoSQL;

        if (exibirColunas)
        {
            comandoSQL = string.Format(@"
        BEGIN 
          /* Traz as ações vinculadas ao fluxo */
          DECLARE @in_CodigoWorkflow		Int,
                  @in_CodigoInstanciaWF BigInt

          SET @in_CodigoWorkflow = {0}
          SET @in_CodigoInstanciaWF = {1}
          
          SELECT i.DescricaoItem				AS Despesa,
                 od.Quantidade					AS Quantidade,
				 od.ValorUnitario				AS ValorUnitario,
				 od.Quantidade * od.ValorUnitario AS Valor,
                 od.CodigoOutraDespesa						AS CodigoAcao,
                 od.CodigoItemOutraDespesa,         
                 od.CodigoWorkflow,
                 od.CodigoInstanciaWF,
                 od.QuantidadeReal,
				 CASE WHEN od.CodigoWorkflowPC = od.CodigoWorkflow AND od.CodigoInstanciaPC = od.CodigoInstanciaWF THEN 'N' ELSE 'S' END AS AcaoPlanejada
            FROM SENAR_OutrasDespesasPlanejamentoABC AS od INNER JOIN
                 SENAR_ItemABC AS i ON (od.CodigoItemOutraDespesa = i.CodigoItem) 
           WHERE od.CodigoWorkflowPC = @in_CodigoWorkflow
             AND od.CodigoInstanciaPC = @in_CodigoInstanciaWF        
             AND i.TipoItem = 'O'
        END", codigoWorkflow, codigoInstanciaWf);
        }
        else
        {
            comandoSQL = string.Format(@"
        BEGIN 
          /* Traz as ações vinculadas ao fluxo */
          DECLARE @in_CodigoWorkflow		Int,
                  @in_CodigoInstanciaWF BigInt

          SET @in_CodigoWorkflow = {0}
          SET @in_CodigoInstanciaWF = {1}
          
          SELECT i.DescricaoItem				AS Despesa,
                 od.Quantidade					AS Quantidade,
				 od.ValorUnitario				AS ValorUnitario,
				 od.Quantidade * od.ValorUnitario AS Valor,
                 od.CodigoOutraDespesa						AS CodigoAcao,
                 od.CodigoItemOutraDespesa,         
                 od.CodigoWorkflow,
                 od.CodigoInstanciaWF,
                 od.QuantidadeReal,
                 'S' AS AcaoPlanejada
            FROM SENAR_OutrasDespesasPlanejamentoABC AS od INNER JOIN
                 SENAR_ItemABC AS i ON (od.CodigoItemOutraDespesa = i.CodigoItem) 
           WHERE od.CodigoWorkflow = @in_CodigoWorkflow
             AND od.CodigoInstanciaWF = @in_CodigoInstanciaWF        
             AND i.TipoItem = 'O'
        END", codigoWorkflow, codigoInstanciaWf);
        }
        DataSet ds = cDados.getDataSet(comandoSQL);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }        
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
        else if (e.ButtonID == "btnExcluir")
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
        else if(e.ButtonID== "btnNovo")
        {
            e.Visible = exibirColunas ? 
                DevExpress.Utils.DefaultBoolean.False : 
                DevExpress.Utils.DefaultBoolean.True;
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
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoItemAcao = int.Parse(ddlAcao.Value.ToString());
        string quantidade = txtQuantidade.Value.ToString().Replace(",", ".");
        string valor = txtValor.Value.ToString().Replace(",", ".");
        string quantidadeReal = txtQuantidadeReal.Value == null ? "NULL" : txtQuantidadeReal.Value.ToString().Replace(",", ".");
        string codigoWorkflowPC = "NULL";
        string codigoInstanciaPC = "NULL";
        if (exibirColunas)
        {
            codigoWorkflowPC = codigoWorkflow.ToString();
            codigoInstanciaPC = codigoInstanciaWf.ToString();
        }

        string comandoSQL = string.Format(@"
        INSERT INTO [dbo].[SENAR_OutrasDespesasPlanejamentoABC]
                   ([CodigoItemOutraDespesa]
                   ,[CodigoWorkflow]
                   ,[CodigoInstanciaWF]
                   ,[Quantidade]
                   ,[ValorUnitario]
                   ,[QuantidadeReal]
                   ,[CodigoWorkflowPC]
                   ,[CodigoInstanciaPC])
             VALUES
                   ({0}
                   ,{1}
                   ,{2}
                   ,{3}
                   ,{4}/{3}
                   ,{5}
                   ,{6}
                   ,{7})"
        , codigoItemAcao, codigoWorkflow, codigoInstanciaWf, quantidade, valor, quantidadeReal, codigoWorkflowPC, codigoInstanciaPC);

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
        int codigoAcao = int.Parse(getChavePrimaria());
        int codigoItemAcao = int.Parse(ddlAcao.Value.ToString());
        string quantidade = txtQuantidade.Value.ToString().Replace(",", ".");
        string valor = txtValor.Value.ToString().Replace(",", ".");
        string quantidadeReal = txtQuantidadeReal.Value == null ? "NULL" : txtQuantidadeReal.Value.ToString().Replace(",", ".");

        string comandoSQL = string.Format(@"
        UPDATE [dbo].[SENAR_OutrasDespesasPlanejamentoABC]
           SET [CodigoItemOutraDespesa] = {1}
              ,[Quantidade] = {2}
              ,[ValorUnitario] = {3}/{2}
              ,[QuantidadeReal] = {4}
         WHERE CodigoOutraDespesa = {0}"
        , codigoAcao, codigoItemAcao, quantidade, valor, quantidadeReal);

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

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoAcao = int.Parse(getChavePrimaria());
        bool indicaAcaoPlanejada = gvDados.GetRowValues(gvDados.FocusedRowIndex, "AcaoPlanejada").ToString().Equals("S");

        string comandoSQL;
        if (exibirColunas && indicaAcaoPlanejada)
        {
            comandoSQL = string.Format(@"
         UPDATE [SENAR_OutrasDespesasPlanejamentoABC]
            SET [QuantidadeReal] = NULL,
                [CodigoWorkflowPC] = NULL,
                [CodigoInstanciaPC] = NULL
          WHERE [CodigoOutraDespesa] = {0}"
            , codigoAcao);
        }
        else
        {
            comandoSQL = string.Format(@"
        DELETE [dbo].[SENAR_OutrasDespesasPlanejamentoABC]
         WHERE CodigoOutraDespesa = {0}"
            , codigoAcao);
        }

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return "Erro ao excluir o registro!";
        else
        {
            carregaGvDados();
            return "";
        }
    }

    #endregion

    #region Combos

    private void carregaComboItens()
    {
        string comandoSQL = string.Format(@"SELECT CodigoItem AS Codigo, DescricaoItem AS Descricao, ValorUnitarioItem AS ValorUnitario FROM SENAR_ItemABC WHERE TipoItem = 'O' AND IndicaItemAtivo = 'S'");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            ddlAcao.DataSource = ds;
            ddlAcao.TextField = "Descricao";
            ddlAcao.ValueField = "Codigo";
            ddlAcao.DataBind();
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
    
    protected void ddlAcao_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        ArrayList list = new ArrayList();
        foreach (ListEditItem item in ddlAcao.Items)
            list.Add(item.GetFieldValue("ValorUnitario"));
        e.Properties["cpHiddenColumnValues"] = list;
    }

    protected void gvDados_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpCodigoWorkflow"] = codigoWorkflow;
        e.Properties["cpCodigoInstancia"] = codigoInstanciaWf;
        e.Properties["cpExibirColunas"] = exibirColunas;
    }

    protected string getBotaoIncluir()
    {
        string htmlBotaoIncluir = string.Empty;
        if (!podeIncluir)
            htmlBotaoIncluir = @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>";
        else if (exibirColunas)
            htmlBotaoIncluir = @"<img id='imgIncluirPrestacaoContas' src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""IncluirOutrasDespesasPrestacaoContas();"" style=""cursor: pointer;""/>";
        else
            htmlBotaoIncluir = @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>";

        return string.Format(@"<table id='tblHeader' style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", htmlBotaoIncluir);
    }
}
