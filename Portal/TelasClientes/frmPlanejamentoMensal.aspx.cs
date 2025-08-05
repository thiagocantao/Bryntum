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

        //gvDados.Columns["AcaoExecutada"].Visible = exibirColunas;
        gvDados.Columns["DataInicioReal"].Visible = exibirColunas;
        gvDados.Columns["DataTerminoReal"].Visible = exibirColunas;
        //ddlAcao.ReadOnly = exibirColunas;
        //ddlInstrutor.ReadOnly = exibirColunas;
        //ddlMunicipio.ReadOnly = exibirColunas;
        //txtValor.ReadOnly = exibirColunas;
        //ddlInicio.ReadOnly = exibirColunas;
        //ddlTermino.ReadOnly = exibirColunas;
        tblDatasRealizacao.Style.Add(HtmlTextWriterStyle.Display, exibirColunas ? "" : "none");
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

        carregaComboInstrutores();
        carregaComboItens();
        carregaComboMunicipios();
                
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
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/frmPlanejamentoMensal.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(Request.QueryString["ALT"].ToString());

        gvDados.Settings.VerticalScrollableHeight = Math.Max((alturaPrincipal - 120) / 2, 225);
    }
    #endregion

    #region GRID

    private void carregaGvDados()
    {
        string comandoSQL;

        #region Comando SQL

        if (exibirColunas)
        {
            comandoSQL = string.Format(@"
        BEGIN 
          /* Traz as ações vinculadas ao fluxo */
        DECLARE @in_CodigoWorkflow		Int,
                  @in_CodigoInstanciaWF BigInt

          SET @in_CodigoWorkflow = {0}
          SET @in_CodigoInstanciaWF = {1}

          
          SELECT u.NomeUF							  AS Regional,
				         Right('00' + Convert(Varchar,Month(DateAdd(m,1,iw.DataInicioInstancia))),2) + '/' +
                              Convert(Varchar,Year(DateAdd(m,1,iw.DataInicioInstancia))) AS Mes,
                 i.DescricaoItem				AS Acao,
                 c.NomeConsultor				AS NomeInstrutor,
                 ap.ValorUnitario				AS Valor,
                 m.NomeMunicipio				AS Municipio,
                 ap.DataInicioPrevisto  AS Inicio,
                 ap.DataTerminoPrevisto	AS Termino,
                 ap.CodigoAcao,
                 ap.CodigoItemAcao,
                 c.CodigoConsultor,
                 ap.CodigoWorkflow,
                 ap.CodigoInstanciaWF,
                 ap.CodigoMunicipio,
                 ap.DataInicioReal,
                 ap.DataTerminoReal,
                 CASE WHEN ap.DataInicioReal IS NULL THEN 'N' ELSE 'S' END AS AcaoExecutada,
				 CASE WHEN ap.CodigoWorkflowPC = ap.CodigoWorkflow AND ap.CodigoInstanciaPC = ap.CodigoInstanciaWF THEN 'N' ELSE 'S' END AS AcaoPlanejada
            FROM SENAR_AcaoPlanejamentoABC AS ap INNER JOIN
                 SENAR_ItemABC AS i ON (ap.CodigoItemAcao = i.CodigoItem) INNER JOIN
                 SENAR_ConsultorABC AS c ON (c.CodigoConsultor = ap.CodigoConsultor) INNER JOIN
                 Municipio As m ON (m.CodigoMunicipio = ap.CodigoMunicipio) INNER JOIN
                 Workflows AS w ON (w.CodigoWorkflow = ap.CodigoWorkflow) INNER JOIN
                 Fluxos AS f ON (f.CodigoFluxo = w.CodigoFluxo) INNER JOIN
                 UF AS u ON (u.SiglaUF = RIGHT(f.IniciaisFluxo,2)) INNER JOIN
                 InstanciasWorkflows AS iw ON (iw.CodigoWorkflow = ap.CodigoWorkflow
                                           AND iw.CodigoInstanciaWf = ap.CodigoInstanciaWF)
           WHERE ap.CodigoWorkflowPC = @in_CodigoWorkflow
             AND ap.CodigoInstanciaPC = @in_CodigoInstanciaWF        
             AND i.TipoItem = 'A'
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

          
          SELECT u.NomeUF							  AS Regional,
				         Right('00' + Convert(Varchar,Month(DateAdd(m,1,iw.DataInicioInstancia))),2) + '/' +
                              Convert(Varchar,Year(DateAdd(m,1,iw.DataInicioInstancia))) AS Mes,
                 i.DescricaoItem				AS Acao,
                 c.NomeConsultor				AS NomeInstrutor,
                 ap.ValorUnitario				AS Valor,
                 m.NomeMunicipio				AS Municipio,
                 ap.DataInicioPrevisto  AS Inicio,
                 ap.DataTerminoPrevisto	AS Termino,
                 ap.CodigoAcao,
                 ap.CodigoItemAcao,
                 c.CodigoConsultor,
                 ap.CodigoWorkflow,
                 ap.CodigoInstanciaWF,
                 ap.CodigoMunicipio,
                 ap.DataInicioReal,
                 ap.DataTerminoReal,
                 CASE WHEN ap.DataInicioReal IS NULL THEN 'N' ELSE 'S' END AS AcaoExecutada,
                 'S' AS AcaoPlanejada
            FROM SENAR_AcaoPlanejamentoABC AS ap INNER JOIN
                 SENAR_ItemABC AS i ON (ap.CodigoItemAcao = i.CodigoItem) INNER JOIN
                 SENAR_ConsultorABC AS c ON (c.CodigoConsultor = ap.CodigoConsultor) INNER JOIN
                 Municipio As m ON (m.CodigoMunicipio = ap.CodigoMunicipio) INNER JOIN
                 Workflows AS w ON (w.CodigoWorkflow = ap.CodigoWorkflow) INNER JOIN
                 Fluxos AS f ON (f.CodigoFluxo = w.CodigoFluxo) INNER JOIN
                 UF AS u ON (u.SiglaUF = RIGHT(f.IniciaisFluxo,2)) INNER JOIN
                 InstanciasWorkflows AS iw ON (iw.CodigoWorkflow = ap.CodigoWorkflow
                                           AND iw.CodigoInstanciaWf = ap.CodigoInstanciaWF)
           WHERE ap.CodigoWorkflow = @in_CodigoWorkflow
             AND ap.CodigoInstanciaWF = @in_CodigoInstanciaWF        
             AND i.TipoItem = 'A'
        END", codigoWorkflow, codigoInstanciaWf);
        } 

        #endregion

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
        else if (e.ButtonID == "btnNovo")
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
        int codigoInstrutor = int.Parse(ddlInstrutor.Value.ToString());
        int codigoMunicipio = int.Parse(ddlMunicipio.Value.ToString());
        string inicio = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicio.Date);
        string termino = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTermino.Date);
        string valor = txtValor.Value.ToString().Replace(",", ".");
        string inicioReal = "NULL";
        string terminoReal = "NULL";
        string codigoWorkflowPC = "NULL";
        string codigoInstanciaPC = "NULL";
        if (exibirColunas)
        {
            inicioReal = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicioReal.Date);
            terminoReal = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoReal.Date);
            codigoWorkflowPC = codigoWorkflow.ToString();
            codigoInstanciaPC = codigoInstanciaWf.ToString();
        }

        string comandoSQL = string.Format(@"
        INSERT INTO [dbo].[SENAR_AcaoPlanejamentoABC]
               ([CodigoItemAcao]
               ,[CodigoWorkflow]
               ,[CodigoInstanciaWF]
               ,[CodigoConsultor]
               ,[CodigoMunicipio]
               ,[Quantidade]
               ,[ValorUnitario]
               ,[DataInicioPrevisto]
               ,[DataTerminoPrevisto]
               ,[DataInicioReal]
               ,[DataTerminoReal]
               ,[CodigoWorkflowPC]
               ,[CodigoInstanciaPC])
         VALUES
               ({0}
               ,{1}
               ,{2}
               ,{3}
               ,{4}
               ,1
               ,{5}
               ,{6}
               ,{7}
               ,{8}
               ,{9}
               ,{10}
               ,{11})"
        , codigoItemAcao, codigoWorkflow, codigoInstanciaWf, codigoInstrutor, codigoMunicipio, valor, inicio, termino, inicioReal, terminoReal, codigoWorkflowPC, codigoInstanciaPC);

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
        int codigoInstrutor = int.Parse(ddlInstrutor.Value.ToString());
        int codigoMunicipio = int.Parse(ddlMunicipio.Value.ToString());
        string inicio = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicio.Date);
        string termino = string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTermino.Date);
        string valor = txtValor.Value.ToString().Replace(",", ".");
        string inicioReal = ddlInicioReal.Value == null? "NULL": string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicioReal.Date);
        string terminoReal = ddlTerminoReal.Value == null ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoReal.Date);

        string comandoSQL = string.Format(@"
        UPDATE [dbo].[SENAR_AcaoPlanejamentoABC]
           SET [CodigoItemAcao] = {1}
              ,[CodigoConsultor] = {2}
              ,[CodigoMunicipio] = {3}
              ,[ValorUnitario] = {4}
              ,[DataInicioPrevisto] = {5}
              ,[DataTerminoPrevisto] = {6}
              ,[DataInicioReal] = {7}
              ,[DataTerminoReal] = {8}
         WHERE CodigoAcao = {0}"
        , codigoAcao, codigoItemAcao, codigoInstrutor, codigoMunicipio, valor, inicio, termino, inicioReal, terminoReal);

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
         UPDATE [SENAR_AcaoPlanejamentoABC]
            SET [DataInicioReal] = NULL,
                [DataTerminoReal] = NULL,
                [CodigoWorkflowPC] = NULL,
                [CodigoInstanciaPC] = NULL
          WHERE [CodigoAcao] = {0}"
            , codigoAcao);
        }
        else
        {
            comandoSQL = string.Format(@"
        DELETE [dbo].[SENAR_AcaoPlanejamentoABC]
         WHERE CodigoAcao = {0}"
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
        string comandoSQL = string.Format(@"SELECT CodigoItem AS Codigo, DescricaoItem AS Descricao, ValorUnitarioItem AS ValorUnitario FROM SENAR_ItemABC WHERE TipoItem = 'A' AND IndicaItemAtivo = 'S'");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds))
        {
            ddlAcao.DataSource = ds;
            ddlAcao.TextField = "Descricao";
            ddlAcao.ValueField = "Codigo";
            ddlAcao.DataBind();
        }
    }

    private void carregaComboInstrutores()
    {
        string comandoSQL = string.Format(@"
        BEGIN 
          DECLARE @in_CodigoWorkflow  Int,
                  @l_SiglaUF      Char(2)
  
          SET @in_CodigoWorkflow = {0}

          SELECT @l_SiglaUF = RIGHT(f.IniciaisFluxo,2) 
            FROM Workflows AS w INNER JOIN
                 Fluxos AS f ON (w.CodigoWorkflow = @in_CodigoWorkflow
                             AND w.CodigoFluxo = f.CodigoFluxo)
          
          SELECT c.CodigoConsultor AS Codigo,
                 c.NomeConsultor AS Descricao
            FROM SENAR_ConsultorABC AS c
           WHERE c.SiglaUF = @l_SiglaUF
           ORDER BY 2
        END", codigoWorkflow);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlInstrutor.DataSource = ds;
            ddlInstrutor.TextField = "Descricao";
            ddlInstrutor.ValueField = "Codigo";
            ddlInstrutor.DataBind();
        }
    }

    private void carregaComboMunicipios()
    {
        string comandoSQL = string.Format(@"
        BEGIN 
          DECLARE @in_CodigoWorkflow  Int,
                  @l_SiglaUF      Char(2)
  
          SET @in_CodigoWorkflow = {0}

           SELECT @l_SiglaUF = RIGHT(f.IniciaisFluxo,2) 
            FROM Workflows AS w INNER JOIN
                 Fluxos AS f ON (w.CodigoWorkflow = @in_CodigoWorkflow
                             AND w.CodigoFluxo = f.CodigoFluxo)
          
          SELECT m.CodigoMunicipio AS Codigo,
                 m.NomeMunicipio AS Descricao
            FROM Municipio AS m
           WHERE m.SiglaUF = @l_SiglaUF
           ORDER BY 2
        END", codigoWorkflow);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlMunicipio.DataSource = ds;
            ddlMunicipio.TextField = "Descricao";
            ddlMunicipio.ValueField = "Codigo";
            ddlMunicipio.DataBind();
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

    protected string getBotaoIncluir()
    {
        string htmlBotaoIncluir = string.Empty;
        if (!podeIncluir)
            htmlBotaoIncluir = @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>";
        else if(exibirColunas)
            htmlBotaoIncluir = @"<img id='imgIncluirPrestacaoContas' src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""IncluirPrestacaoContas();"" style=""cursor: pointer;""/>";
        else
            htmlBotaoIncluir = @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>";

        return string.Format(@"<table id='tblHeader' style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", htmlBotaoIncluir);
    }

    protected void gvDados_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        e.Properties["cpCodigoWorkflow"] = codigoWorkflow;
        e.Properties["cpCodigoInstancia"] = codigoInstanciaWf;
        e.Properties["cpExibirColunas"] = exibirColunas;
    }
}
