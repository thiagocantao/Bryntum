using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using DevExpress.Web;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

public partial class _Processos_Visualizacao_FilterEditorPopup : System.Web.UI.Page
{
    int codigoEntidade;
    private int codigoLista;
    int codigoUsuarioLogado;
    string dbName;
    string dbOwner;
    int codigoCarteira;
    int? codigoProjeto;
    int? codigoFluxo;
    int? codigoWorkflow;
    int? codigoInstancia;
    int? codigoEtapaAtual;
    int? ocorrenciaAtual;
    int? codigoListaUsuario;
    string indicaContextoPreFiltro;

    dados cDados;


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

    public string SFilterExpressionLoad
    {
        get
        {
            return ViewState["sFilterExpressionLoad"].ToString();
        }

        set
        {
            ViewState["sFilterExpressionLoad"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.aplicaEstiloVisual(cbIndicaPreFiltroPersonalizado);
        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = string.Format(
                "{0}erros/erroInatividade.aspx", cDados.getPathSistema());
            Response.End();
        }

        CarregaVariaveis();

        //controla visibilidade do checkbox.
        cbIndicaPreFiltroPersonalizado.Visible = indicaContextoPreFiltro.Equals("CFG");

        if (!IsPostBack)
        {
            loadFilterEditor();
        }

        this.TH(this.TS("FilterEditorPopup"));
    }
    

    private void CarregaVariaveis()
    {
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoLista = int.Parse(Request.QueryString["cl"]);
        if (Request.QueryString["clu"] != null)
            codigoListaUsuario = int.Parse(Request.QueryString["clu"]);
        else
            codigoListaUsuario = -1;

        this.retorno_popup.Value = codigoListaUsuario.ToString();

        indicaContextoPreFiltro = Request.QueryString["ctx"] != null ? "CFG" : "VSL";


        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        codigoUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
        codigoCarteira = Convert.ToInt32(cDados.getInfoSistema("CodigoCarteira"));
    }

    protected void SalvarPosConfirm_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        CallbackSalvarPosConfirm.JSProperties["cp_OK"] = "";
        CallbackSalvarPosConfirm.JSProperties["cp_Erro"] = "";
        CallbackSalvarPosConfirm.JSProperties["cp_clu"] = "";

        gravaFiltroLista(CallbackSalvarPosConfirm.JSProperties);
    } 
    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";
        callBack.JSProperties["cp_callConfirm"] = "";
        callBack.JSProperties["cp_clu"] = "";

        if (this.indicaContextoPreFiltro.ToUpper().Equals("VSL") || !cbIndicaPreFiltroPersonalizado.Checked || !filter.FilterExpression.Equals(SFilterExpressionLoad))
        {
            gravaFiltroLista(((DevExpress.Web.ASPxCallback)source).JSProperties);

        }
        else
        {
            
            if (existeFiltroCustomizado())
            {
                callBack.JSProperties["cp_callConfirm"] = "s";
            }
            else { gravaFiltroLista(callBack.JSProperties); }

        }


      }

   

    private bool existeFiltroCustomizado()
    {
        var commandsql = string.Format(@"DECLARE	@return_value Bit EXEC @return_value = [dbo].[f_rdn_existeFiltroCustomizado] @CodigoLista = {0} SELECT @return_value as 'Value'", codigoLista);
        var ds = cDados.getDataSet(commandsql);
        DataTable dt = ds.Tables[0];
        return bool.Parse(dt.Rows[0]["Value"].ToString());
    }

    private void gravaFiltroLista(Dictionary<string, object> jSProperties)
    {
        try
        {
            var commandSql = string.Format(@"
            

	          DECLARE @in_codigoEntidadeContexto				int
	          DECLARE @in_codigoUsuarioSistema					int
	          DECLARE @in_codigoLista							int
              DECLARE @in_codigoListaUsuario			int
	          DECLARE @in_indicaContextoPreFiltro				char(3)
	          DECLARE @in_instrucaoPreFiltro					varchar(max)
              DECLARE @in_instrucaoPreFiltroOriginal					varchar(max)
              DECLARE @in_indicaPreFiltroPersonalizavel     CHAR(1)
              DECLARE @ou_codigoListaUsuario int

             SET @in_codigoEntidadeContexto	 = {0}
             SET @in_codigoUsuarioSistema	 = {1}
             SET @in_codigoLista			 = {2}
             SET @in_codigoListaUsuario     = {3}
             SET @in_indicaContextoPreFiltro = '{4}'
             SET @in_instrucaoPreFiltro		 = '{5}'
             SET @in_instrucaoPreFiltroOriginal		 = '{6}'
             SET @in_indicaPreFiltroPersonalizavel		 = '{7}'

             EXECUTE [dbo].[p_rdn_gravaPreFiltroLista] 
                @in_codigoEntidadeContexto	
                ,@in_codigoUsuarioSistema	
                ,@in_codigoLista
                ,@in_codigoListaUsuario
                ,@in_indicaContextoPreFiltro
                ,@in_instrucaoPreFiltro		
                ,@in_instrucaoPreFiltroOriginal
                ,@in_indicaPreFiltroPersonalizavel
                ,@ou_codigoListaUsuario OUTPUT

            SELECT @ou_codigoListaUsuario AS codigoListaUsuario
        ", codigoEntidade, codigoUsuarioLogado, codigoLista, codigoListaUsuario, indicaContextoPreFiltro,
        filter.GetFilterExpressionForDataSet().Replace("'", "''"), filter.FilterExpression.Replace("'", "''"), indicaContextoPreFiltro.Equals("CFG") ? cbIndicaPreFiltroPersonalizado.Checked ? "S" : "N" : "");

            DataSet ds = cDados.getDataSet(commandSql);
            
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                jSProperties["cp_clu"] = (int)ds.Tables[0].Rows[0]["codigoListaUsuario"];
                jSProperties["cp_OK"] = Resources.traducao.DetalhesTS_atribui__o_atualizada_com_sucesso_;
            }
            else
                jSProperties["cp_Erro"]  = Resources.traducao.DetalhesTS_erro_ao_atualizar_a_atribui__o;

        }
        catch (Exception ex)
        {

            callBack.JSProperties["cp_Erro"] = Resources.traducao.DetalhesTS_erro_ao_atualizar_a_atribui__o;
        }
    }

    private void loadFilterEditor()
    {
        var comandoSql = string.Format(@"
        DECLARE @in_codigoEntidadeContexto		int
	    DECLARE @in_codigoUsuarioSistema		int
	    DECLARE @in_codigoLista					int
	    DECLARE @in_codigoListaUsuario			int
	    DECLARE @in_indicaContextoPreFiltro		char(3)
	    DECLARE @ou_instrucaoSelect				varchar(max)	
	    DECLARE @ou_instrucaoPreFiltro			varchar(max) 
        DECLARE @ou_instrucaoPreFiltroOriginal	varchar(max)
        DECLARE @ou_indicaPreFiltroPersonalizavel     CHAR(1)

        SET @in_codigoEntidadeContexto	 = {0}
        SET @in_codigoUsuarioSistema	 = {1}
        SET @in_codigoLista				 = {2}
        SET @in_indicaContextoPreFiltro	 = '{3}'
        SET @in_codigoListaUsuario       = {4}   
       

        EXECUTE [dbo].[p_rdn_obtemDadosPreFiltro]
           @in_codigoEntidadeContexto	
          ,@in_codigoUsuarioSistema	
          ,@in_codigoLista	
          ,@in_codigoListaUsuario
          ,@in_indicaContextoPreFiltro	
          ,@ou_instrucaoSelect	            output		
          ,@ou_instrucaoPreFiltro           output
          ,@ou_instrucaoPreFiltroOriginal   output
          ,@ou_indicaPreFiltroPersonalizavel output
                




         SELECT @ou_instrucaoSelect AS instrucaoSelect, @ou_instrucaoPreFiltro AS instrucaoPreFiltro , @ou_instrucaoPreFiltroOriginal AS instrucaoPreFiltroOriginal, @ou_indicaPreFiltroPersonalizavel as indicaPreFiltroPersonalizavel         "
                   , codigoEntidade
                   , codigoUsuarioLogado
                   , codigoLista
                   , indicaContextoPreFiltro
                   , (codigoListaUsuario.Equals("")? -1 : codigoListaUsuario));




        var ds = cDados.getDataSet(comandoSql);
        DataTable dt = ds.Tables[0];

        SetDataSourceSettings(dataSource, dt.Rows[0][0].ToString());


        //if (!ds.Tables[0].Rows[0].Equals(""))
        //{
            this.SFilterExpressionLoad =  filter.FilterExpression = ds.Tables[0].Rows[0]["instrucaoPreFiltroOriginal"].ToString();
            cbIndicaPreFiltroPersonalizado.Checked = ds.Tables[0].Rows[0]["indicaPreFiltroPersonalizavel"].ToString().Equals("S");

        //}

        int maxHierarchyDepth = 1;
       
        filter.BindToSource(dataSource.Select(DataSourceSelectArguments.Empty), false, maxHierarchyDepth);
    }

    private string ReplaceParameters(string comandoPreencheLista)
    {
        /*
		 * {0} - BD
		 * {1} - Owner
		 * {2} - Código do Projeto
		 * {3} - Código da Entidade
		 * {4} - Usuário Logado 
		 * {5} - Código Fluxo
		 * {6} - Código Workflow
		 * {7} - Código Instância
		 * {8} - Código Etapa
		 * {9} - Ocorrência Atual
		 * {10} - Código Carteira
		 */
        const string valorNaoDefinido = "-1";
        comandoPreencheLista = comandoPreencheLista
            .Replace("{0}", dbName)
            .Replace("{1}", dbOwner)
            .Replace("{2}", codigoProjeto.HasValue ? codigoProjeto.ToString() : valorNaoDefinido)
            .Replace("{3}", codigoEntidade.ToString())
            .Replace("{4}", codigoUsuarioLogado.ToString())
            .Replace("{5}", codigoFluxo.HasValue ? codigoFluxo.ToString() : valorNaoDefinido)
            .Replace("{6}", codigoWorkflow.HasValue ? codigoWorkflow.ToString() : valorNaoDefinido)
            .Replace("{7}", codigoInstancia.HasValue ? codigoInstancia.ToString() : valorNaoDefinido)
            .Replace("{8}", codigoEtapaAtual.HasValue ? codigoEtapaAtual.ToString() : valorNaoDefinido)
            .Replace("{9}", ocorrenciaAtual.HasValue ? ocorrenciaAtual.ToString() : valorNaoDefinido)
            .Replace("{10}", codigoCarteira.ToString()
            );

        return comandoPreencheLista;
    }

    private void SetDataSourceSettings(SqlDataSource dataSource, string selectCommand)
    {
        var applicationName = string.Format("app=portal,ce={0},cul={1}", codigoEntidade, codigoUsuarioLogado);
        var conexao = new SqlConnectionStringBuilder(ConnectionString);
        conexao.ApplicationName = applicationName;

        SetDefaultSessionInfo();
        dataSource.SelectCommand = ReplaceParameters(selectCommand);
        dataSource.ConnectionString = conexao.ConnectionString;
        dataSource.SetSelectParameters();
        
    }
    private void SetDefaultSessionInfo()
    {
        var data = new Dictionary<string, object>();
        data.Add("pa_CodigoEntidadeContexto", codigoEntidade);
        data.Add("pa_CodigoUsuarioSistema", codigoUsuarioLogado);
        DataExtensions.SetSessionInfo(data);
    }

}