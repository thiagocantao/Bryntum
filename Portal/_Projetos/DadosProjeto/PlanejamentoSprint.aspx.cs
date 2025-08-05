using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Text;

public partial class _Projetos_DadosProjeto_PlanejamentoSprint : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool indicaPublicado = false;


    public decimal Capacidade_AbaEquipe;
    public decimal FatorProdutividadeEquipe;
    public decimal HorasDedicadasEquipe;

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

        if (Request.QueryString["CP"] != null)
            idProjeto = int.Parse(Request.QueryString["CP"]);

        gvItens.JSProperties["cpCodigoProjeto"] = idProjeto;
        gvItens.JSProperties["cpCodigoProjetoAgil"] = getCodigoProjetoAgil();
        gvItens.JSProperties["cpCodigoIteracao"] = getCodigoIteracao();
        gvItens.JSProperties["cpCodigoProjetoIteracao"] = getCodigoProjetoIteracao();

        carregaComboRecursos();
        carregaComboPapeisRecursos();

        cDados.setaTamanhoMaximoMemo(mmObjetivos, 4000, lbl_mmObjetivos);
        cDados.setaTamanhoMaximoMemo(mmAnotacoes, 2000, lbl_mmAnotacoes);

        sdsMembrosEquipe.ConnectionString = cDados.ConnectionString;
    }

    private object getCodigoProjetoIteracao()
    {
        int retorno = -1;
        string comandoSQL = string.Format(@"
        SELECT CodigoProjetoIteracao 
        FROM Agil_Iteracao 
        WHERE CodigoProjetoIteracao = {0} ", idProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            bool ret = int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out retorno);
        }
        return retorno;
    }

    protected int getCodigoProjetoAgil()
    {
        int retorno = -1;
        string comandoSQL = string.Format(@"SELECT TOP 1 [CodigoProjetoPai]
        FROM [dbo].[LinkProjeto] WHERE CodigoProjetoFilho = {0}", idProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            bool ret = int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out retorno);
        }
        return retorno;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {
            carregaCampos();
            pcSprint.ActiveTabIndex = 0;
        }

        gvItens.JSProperties["cp_Msg"] = "";
        gvItens.JSProperties["cp_CP"] = idProjeto;
        

        verificaPublicacao();

        carregaGridEquipe();
        carregaGridItens();

        ConfiguraVisibilidadeLabelMensagemScrumMaster();

        if (!IsPostBack)
            carregaCamposPlanejamento();


        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "controlaClientesGestaoAgil");

        txtReceita.ClientVisible = true;
        lblReceita.ClientVisible = true;

        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["controlaClientesGestaoAgil"] + "" == "N")
        {
            txtReceita.ClientVisible = false;
            lblReceita.ClientVisible = false;
        }
        gvItens.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/PlanejamentoSprint.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings", "PlanejamentoSprint"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int alturaPrincipalBackup = alturaPrincipal;
        alturaPrincipal = (altura - 190);

        //aba de equipe
        gvDados.Settings.VerticalScrollableHeight = altura - 550;
        //aba de itens
        gvItens.Settings.VerticalScrollableHeight = altura - 500;
        if (alturaPrincipalBackup > 900)
        {
            mmObjetivos.Height = alturaPrincipalBackup - 835;
            mmAnotacoes.Height = alturaPrincipalBackup - 835;
        }
        else
        {
            mmObjetivos.Height = alturaPrincipalBackup - 610;
            mmAnotacoes.Height = alturaPrincipalBackup - 610;
        }
    }
    #endregion

    #region ABA 1

    private void carregaCampos()
    {
        string comandoSQL = string.Format(@"
            SELECT p.NomeProjeto AS NomeSprint,
                   p.DescricaoProposta AS OBjetivos,
                   a.InicioPrevisto,
                   a.TerminoPrevisto,
                   p.Comentario AS Anotacoes,
                   a.DataReuniaoEncerramento,
                   a.CodigoIteracao AS CodigoSprint,
                   p.CodigoProjeto AS CodigoProjeto
              FROM Projeto AS p INNER JOIN
                   Agil_Iteracao AS a ON (a.CodigoProjetoIteracao = p.CodigoProjeto
                                      AND p.DataExclusao IS NULL)
             WHERE p.CodigoProjeto = {0}", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtSprint.Text = dr["NomeSprint"].ToString();
            mmObjetivos.Text = dr["OBjetivos"].ToString();
            ddlInicio.Value = dr["InicioPrevisto"];
            ddlTermino.Value = dr["TerminoPrevisto"];
            mmAnotacoes.Text = dr["Anotacoes"].ToString();
        }
    }

    private void verificaPublicacao()
    {
        string comandoSQL = string.Format(@"
            SELECT a.DataPublicacaoPlanejamento
              FROM Agil_Iteracao AS a
             WHERE a.CodigoProjetoIteracao = {0}", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            indicaPublicado = dr["DataPublicacaoPlanejamento"].ToString() != "";
        }
        //indicaPublicado = false;
        desabilitaCampos();
    }

    private void desabilitaCampos()
    {
        txtSprint.ClientEnabled = !indicaPublicado;
        mmObjetivos.ClientEnabled = !indicaPublicado;
        ddlInicio.ClientEnabled = !indicaPublicado;
        ddlTermino.ClientEnabled = !indicaPublicado;
        mmAnotacoes.ClientEnabled = !indicaPublicado;
        btnPublicar.ClientEnabled = !indicaPublicado;
    }

    private void salvaCampos()
    {
        string comandoSQL = string.Format(@"
            UPDATE Projeto 
               SET NomeProjeto = '{1}'
                  ,DescricaoProposta = '{2}'
                  ,Comentario = '{3}'
             WHERE CodigoProjeto = {0}

            UPDATE Agil_Iteracao 
               SET InicioPrevisto = CONVERT(DateTime, '{4:dd/MM/yyyy}', 103)
                  ,TerminoPrevisto = CONVERT(DateTime, '{5:dd/MM/yyyy}', 103)
             WHERE CodigoProjetoIteracao = {0}"
            , idProjeto
            , txtSprint.Text.Replace("'", "''")
            , mmObjetivos.Text.Replace("'", "''")
            , mmAnotacoes.Text.Replace("'", "''")
            , ddlInicio.Date
            , ddlTermino.Date);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result)
            callback.JSProperties["cp_Msg"] = Resources.traducao.PlanejamentoSprint_dados_salvos_com_sucesso_;
        else
            callback.JSProperties["cp_Msg"] = Resources.traducao.PlanejamentoSprint_erro_ao_salvar_dados_;
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        salvaCampos();
    }

    #endregion

    #region ABA 2

    private void carregaGridEquipe()
    {
        string comandoSQL = string.Format(@"
            BEGIN
	            DECLARE @CodigoIteracao Int
                       ,@DataInicio DateTime
                       ,@DataTermino DateTime         
	
	            SELECT @CodigoIteracao = CodigoIteracao
                      ,@DataInicio = InicioPrevisto
                      ,@DataTermino = TerminoPrevisto
		            FROM Agil_Iteracao
	             WHERE CodigoProjetoIteracao = {0}

	          SELECT ri.CodigoRecursoIteracao
                    ,ri.CodigoRecursoCorporativo
				    ,rc.NomeRecurso
				    ,ri.CodigoTipoPapelRecursoIteracao
				    ,ri.CustoUnitario
				    ,ri.PercentualAlocacao
				    ,ri.ReceitaUnitaria 
				    ,tpr.DescricaoTipoPapelRecurso
                    ,dbo.f_Agil_GetCapacidadeAlocacaoRecurso(ri.CodigoRecursoCorporativo, @DataInicio, @DataTermino) * ri.PercentualAlocacao / 100 AS HorasDedicadas
		    FROM Agil_RecursoIteracao ri INNER JOIN
				 vi_RecursoCorporativo rc ON rc.CodigoRecursoCorporativo = ri.CodigoRecursoCorporativo INNER JOIN
				 Agil_TipoPapelRecurso tpr ON tpr.CodigoTipoPapelRecurso = ri.CodigoTipoPapelRecursoIteracao
	        WHERE ri.CodigoIteracao = @CodigoIteracao
	        ORDER BY rc.NomeRecurso
 												
             END", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

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
            carregaComboRecursos();
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }

        ConfiguraVisibilidadeLabelMensagemScrumMaster();
    }

    private void ConfiguraVisibilidadeLabelMensagemScrumMaster()
    {
        var ds = (gvDados.DataSource as DataSet);
        if (ds != null)
        {
            var dt = ds.Tables[0];
            var quantidadeScrumMaster = dt.AsEnumerable()
                .Count(dr => (dr.Field<string>("DescricaoTipoPapelRecurso") ?? string.Empty).Trim().Equals("Scrum Master", StringComparison.InvariantCultureIgnoreCase));
            lblMensagemScrumMaster.ClientVisible = quantidadeScrumMaster > 1;
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
            return "-1";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string codigoRecurso = ddlRecurso.Value.ToString();
        string codigoPapelRecurso = ddlPapelRecurso.Value.ToString();
        string percentualDedicacao = txtPercentualDedicacao.Text;
        string custoUnitario = txtCusto.Text == "" ? "NULL" : txtCusto.Text.Replace(',', '.');
        string receitaUnitaria = txtReceita.Text == "" ? "NULL" : txtReceita.Text.Replace(',', '.');

        string comandoSQL = string.Format(@"
            BEGIN
	            DECLARE @CodigoIteracao Int
	
	            SELECT @CodigoIteracao = CodigoIteracao
		            FROM Agil_Iteracao
	             WHERE CodigoProjetoIteracao = {0}

	          INSERT INTO [dbo].[Agil_RecursoIteracao]
                   ([CodigoRecursoCorporativo]
                   ,[CodigoIteracao]
                   ,[PercentualAlocacao]
                   ,[CodigoTipoPapelRecursoIteracao]
                   ,[CustoUnitario]
                   ,[ReceitaUnitaria])
             VALUES
                   ({1}
                   ,@CodigoIteracao 
                   ,{2}
                   ,{3}
                   ,{4}
                   ,{5})
 												
             END
            ", idProjeto
             , codigoRecurso
             , percentualDedicacao
             , codigoPapelRecurso
             , custoUnitario
             , receitaUnitaria);

        int regAf = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return Resources.traducao.PlanejamentoSprint_erro_ao_salvar_o_registro_;
        else
        {
            carregaGridEquipe();
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoIteracaoRecurso = int.Parse(getChavePrimaria());
        string codigoRecurso = ddlRecurso.Value.ToString();
        string codigoPapelRecurso = ddlPapelRecurso.Value.ToString();
        string percentualDedicacao = txtPercentualDedicacao.Text;
        string custoUnitario = txtCusto.Text == "" ? "NULL" : txtCusto.Text.Replace(',', '.');
        string receitaUnitaria = txtReceita.Text == "" ? "NULL" : txtReceita.Text.Replace(',', '.');

        string comandoSQL = string.Format(@"
            UPDATE [dbo].[Agil_RecursoIteracao]
                SET  [PercentualAlocacao] = {2}
                    ,[CodigoTipoPapelRecursoIteracao] = {3}
                    ,[CustoUnitario] = {4}
                    ,[ReceitaUnitaria] = {5}
                WHERE CodigoRecursoIteracao = {0}"
             , codigoIteracaoRecurso
             , codigoRecurso
             , percentualDedicacao
             , codigoPapelRecurso
             , custoUnitario
             , receitaUnitaria);

        int regAf = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return Resources.traducao.PlanejamentoSprint_erro_ao_editar_o_registro_;
        else
        {
            carregaGridEquipe();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoIteracaoRecurso = int.Parse(getChavePrimaria());

        string comandoSQL = string.Format(@"
            DELETE [dbo].[Agil_RecursoIteracao]
                WHERE CodigoRecursoIteracao = {0}"
             , codigoIteracaoRecurso);

        int regAf = 0;
        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result == false)
            return Resources.traducao.PlanejamentoSprint_erro_ao_excluir_o_registro_;
        else
        {
            carregaGridEquipe();
            return "";
        }
    }

    #endregion

    private void carregaComboRecursos()
    {
        string comandoSQL = string.Format(@"        
        BEGIN
	       DECLARE @CodigoIteracao Int
	
	        SELECT @CodigoIteracao = CodigoIteracao
		      FROM Agil_Iteracao
	         WHERE CodigoProjetoIteracao = {1}

	        SELECT CodigoRecursoCorporativo, 
                   NomeRecurso 
              FROM vi_RecursoCorporativo rc
             WHERE CodigoEntidade = {0} 
               AND TipoRecurso = 1
               AND NOT EXISTS(SELECT 1 FROM Agil_RecursoIteracao ri WHERE ri.CodigoRecursoCorporativo = rc.CodigoRecursoCorporativo AND ri.CodigoIteracao = @CodigoIteracao)
             ORDER BY NomeRecurso
         END
", codigoEntidadeUsuarioResponsavel, idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlRecurso.DataSource = ds;
        ddlRecurso.TextField = "NomeRecurso";
        ddlRecurso.ValueField = "CodigoRecursoCorporativo";
        ddlRecurso.DataBind();
    }

    private void carregaComboPapeisRecursos()
    {
        string comandoSQL = string.Format(@"        
        BEGIN
	      
	        SELECT CodigoTipoPapelRecurso, 
                   DescricaoTipoPapelRecurso 
              FROM Agil_TipoPapelRecurso
            
         END
");

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlPapelRecurso.DataSource = ds;
        ddlPapelRecurso.TextField = "DescricaoTipoPapelRecurso";
        ddlPapelRecurso.ValueField = "CodigoTipoPapelRecurso";
        ddlPapelRecurso.DataBind();
    }

    protected void callbackCusto_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoSQL = string.Format(@"       
      BEGIN
        DECLARE @DataInicio DateTime,
                @DataTermino DateTime 

        SELECT @DataInicio = InicioPrevisto
              ,@DataTermino = TerminoPrevisto
		      FROM Agil_Iteracao
	         WHERE CodigoProjetoIteracao = {0}

        SELECT CustoHora,
               dbo.f_Agil_GetCapacidadeAlocacaoRecurso(CodigoRecursoCorporativo, @DataInicio, @DataTermino) AS HorasDedicadas
          FROM RecursoCorporativo
         WHERE CodigoRecursoCorporativo = {0}
        
      END", ddlRecurso.Value == null ? "-1" : ddlRecurso.Value.ToString());

        DataSet ds = cDados.getDataSet(comandoSQL);

        string valorCusto = "";
        string horasDedicadas = "";

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            valorCusto = ds.Tables[0].Rows[0]["CustoHora"].ToString();
            horasDedicadas = ds.Tables[0].Rows[0]["HorasDedicadas"].ToString();
        }
        callbackCusto.JSProperties["cp_CustoUnitario"] = valorCusto;
        callbackCusto.JSProperties["cp_HorasDedicadas"] = horasDedicadas.Replace(',', '.');
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            e.Enabled = true;


        }
        if (e.ButtonID == "btnExcluir")
        {
            int quantidadeTarefas = gvItens.GetRowValues(e.VisibleIndex, "QuantidadeTarefa") == null ? 0 : int.Parse(gvItens.GetRowValues(e.VisibleIndex, "QuantidadeTarefa").ToString());
            if (quantidadeTarefas == 0)
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

    #region ABA 3

    private void carregaGridItens()
    {
        string comandoSQL = string.Format(@"
            SELECT ib.CodigoItem,
                   ib.TituloItem,
                   ib.DetalheItem,
                   ib.Importancia,
                   ib.EsforcoPrevisto,
                   ib.Complexidade,
                   CASE WHEN ib.Complexidade = 0 THEN 'Baixa'
                        WHEN ib.Complexidade = 1 THEN 'Média'
                        WHEN ib.Complexidade = 2 THEN 'Alta'
                        WHEN ib.Complexidade = 3 THEN 'Muito Alta' ELSE '' END AS DescricaoComplexidade,
                   (SELECT COUNT(1)
                      FROM Agil_ItemBacklog AS ib_tar
                     WHERE ib_tar.CodigoItemSuperior = ib.CodigoItem and ib_tar.CodigoUsuarioExclusao is null) AS QuantidadeTarefa
              FROM Agil_ItemBacklog AS ib INNER JOIN
                   Agil_TipoStatusItemBacklog AS tsi ON (tsi.CodigoTipoStatusItem = ib.CodigoTipoStatusItem)  INNER JOIN
                   Agil_iteracao  it ON (it.CodigoIteracao = ib.CodigoIteracao)
             WHERE  it.CodigoProjetoIteracao = {0}
             AND ISNULL(ib.[IndicaTarefa],'N') = 'N'", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        gvItens.DataSource = ds;
        gvItens.DataBind();
    }

    private int getCodigoIteracao()
    {
        int retorno = -1;
        string comandoSQL = string.Format(@"
            SELECT top 1 it.CodigoIteracao
              FROM Agil_ItemBacklog AS ib INNER JOIN
                   Agil_TipoStatusItemBacklog AS tsi ON (tsi.CodigoTipoStatusItem = ib.CodigoTipoStatusItem)  INNER JOIN
                   Agil_iteracao  it ON (it.CodigoIteracao = ib.CodigoIteracao)
             WHERE  it.CodigoProjetoIteracao = {0}
             AND ISNULL(ib.[IndicaTarefa],'N') = 'N'", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            bool retornob = int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out retorno);
        }
        return retorno;
    }

    protected void gvItens_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Excluir")
        {
            string comandoSQL = string.Format(@"
            BEGIN                
                UPDATE Agil_ItemBacklog
                   SET CodigoIteracao = NULL
                 WHERE CodigoItem = {0}
            END"
            , getChavePrimariaItem());

            int regAf = 0;

            bool result = cDados.execSQL(comandoSQL, ref regAf);

            if (result)
            {
                gvItens.JSProperties["cp_Msg"] = Resources.traducao.PlanejamentoSprint_item_exclu_do_com_sucesso_;
                carregaGridItens();
            }
            else
                gvItens.JSProperties["cp_Msg"] = Resources.traducao.PlanejamentoSprint_erro_ao_excluir_o_item_;
        }

        carregaGridItens();
    }

    //retorna a primary key da tabela.
    private string getChavePrimariaItem()
    {
        if (gvItens.FocusedRowIndex >= 0)
            return gvItens.GetRowValues(gvItens.FocusedRowIndex, gvItens.KeyFieldName).ToString();
        else
            return "-1";
    }

    protected void gvItens_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditarItem")
        {

            e.Enabled = true;
        }
        if (e.ButtonID == "btnExcluirItem")
        {
            int quantidadeTarefas = gvItens.GetRowValues(e.VisibleIndex, "QuantidadeTarefa") == null ? 0 : int.Parse(gvItens.GetRowValues(e.VisibleIndex, "QuantidadeTarefa").ToString());
            if (quantidadeTarefas == 0)
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

    protected void callbackDadosPlanejamento_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        carregaCamposPlanejamento();
    }

    private void carregaCamposPlanejamento()
    {
        string comandoSQL = string.Format(@"
        BEGIN

            DECLARE @Estimativa Decimal(10,2),
                    @Capacidade Decimal(10,2),
                    @Saldo Decimal(10,2),
                    @CodigoIteracao Int,
                    @DataInicio DateTime,
                    @DataTermino DateTime

             SELECT @CodigoIteracao = CodigoIteracao
                      ,@DataInicio = InicioPrevisto
                      ,@DataTermino = TerminoPrevisto
		            FROM Agil_Iteracao
	             WHERE CodigoProjetoIteracao = {0}

            SELECT @Estimativa = SUM(ib.EsforcoPrevisto)
              FROM Agil_ItemBacklog AS ib INNER JOIN
                   Agil_TipoClassificacaoItemBacklog AS tci ON (tci.CodigoTipoClassificacaoItem = ib.CodigoTipoClassificacaoItem) INNER JOIN
                   Agil_TipoStatusItemBacklog AS tsi ON (tsi.CodigoTipoStatusItem = ib.CodigoTipoStatusItem) INNER JOIN
                   LinkProjeto AS lp ON (lp.CodigoProjetoFilho = {0}
                                     AND lp.CodigoProjetoPai = ib.CodigoProjeto)                                         
             WHERE /*ib.CodigoItemSuperior IS NULL
               AND ib.IndicaItemNaoPlanejado = 'N'
               AND */ib.CodigoIteracao IN (SELECT i.CodigoIteracao FROM Agil_Iteracao AS i WHERE i.CodigoProjetoIteracao = {0})

	       SELECT @Capacidade = SUM((dbo.f_Agil_GetCapacidadeAlocacaoRecurso(ri.CodigoRecursoCorporativo, @DataInicio, @DataTermino) * ri.PercentualAlocacao / 100))
		     FROM Agil_RecursoIteracao ri INNER JOIN
			      vi_RecursoCorporativo rc ON rc.CodigoRecursoCorporativo = ri.CodigoRecursoCorporativo INNER JOIN
			      Agil_TipoPapelRecurso tpr ON tpr.CodigoTipoPapelRecurso = ri.CodigoTipoPapelRecursoIteracao
	        WHERE ri.CodigoIteracao = @CodigoIteracao

            SELECT ISNULL(@Estimativa, 0) AS Estimativa, ISNULL(@Capacidade, 0) AS Capacidade, @Capacidade - @Estimativa AS Saldo

        END", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtCapacidadeEquipe.Value = (decimal)ds.Tables[0].Rows[0]["Capacidade"] * 1/*(decimal)(getFatorProdutividadeEquipe())*/;
            txtEstimativa.Value = ds.Tables[0].Rows[0]["Estimativa"];
            txtSaldo.Value = (decimal)txtCapacidadeEquipe.Value - (decimal)txtEstimativa.Value;// (decimal)ds.Tables[0].Rows[0]["Saldo"] * getFatorProdutividadeEquipe();
        }

        decimal soma = 0;
        decimal saidaDecimal = 0;
        bool retorno = false;
        for( int i = 0; i< gvItens.VisibleRowCount; i++)
        {
           retorno =  decimal.TryParse(gvItens.GetRowValues(i, "EsforcoPrevisto").ToString(), out saidaDecimal);
            soma += saidaDecimal;
        }
        txtEstimativa.Text = decimal.Round(soma, 2).ToString();
    }

    protected void gvItens_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "QuantidadeTarefa" && e.CellValue != null)
        {
            string somenteLeitura = indicaPublicado ? "S" : "N";
            string codigoItem = gvItens.GetRowValues(e.VisibleIndex, "CodigoItem").ToString();
            e.Cell.Text = "<a href='#' style='color:#000000;font-weight:bold;font-size:8pt;text-decoration: underline;' onclick='abreTarefas(" + codigoItem + ", \"" + somenteLeitura + "\");'>" + e.CellValue + "<a/>";
        }
    }

    #endregion

    protected void pnCallbackGeral_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string comandoSQL = string.Format(@"            
             UPDATE Agil_Iteracao 
               SET DataPublicacaoPlanejamento = GetDate()
             WHERE CodigoProjetoIteracao = {0}"
           , idProjeto);

        int regAf = 0;

        bool result = cDados.execSQL(comandoSQL, ref regAf);

        if (result)
        {
            pnCallbackGeral.JSProperties["cp_Msg"] = Resources.traducao.PlanejamentoSprint_publica__o_realizada_com_sucesso_;
            verificaPublicacao();
            carregaGridEquipe();
            carregaGridItens();
        }
        else
            pnCallbackGeral.JSProperties["cp_Msg"] = Resources.traducao.PlanejamentoSprint_erro_ao_publicar_;
    }

    protected void callbackDadosEquipe_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        //txtFatorProdutividadeEquipe.Text = string.Format(@"{0:p2}", getFatorProdutividadeEquipe());
     
        decimal soma = 0;
        decimal saidaDecimal = 0;
        bool retorno = false;
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            retorno = decimal.TryParse(gvDados.GetRowValues(i, "HorasDedicadas").ToString(), out saidaDecimal);
            soma += saidaDecimal;
        }
        //txtHorasDedicadas.Text = decimal.Round(soma, 2).ToString();

    }

    private decimal getFatorProdutividadeEquipe()
    {
        decimal retorno = 0;
        string comandosql = string.Format(@"
            SELECT ai.[FatorProdutividade]
              FROM [Agil_Iteracao] ai
        INNER JOIN projeto p on (p.CodigoProjeto = ai.CodigoProjetoIteracao)
        WHERE p.CodigoProjeto = {0}", idProjeto);
        DataSet dsFatorP = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(dsFatorP) && cDados.DataTableOk(dsFatorP.Tables[0]))
        {
            decimal aux = 0;
            if (decimal.TryParse(dsFatorP.Tables[0].Rows[0][0].ToString(), out aux))
            {
                retorno = aux;
            }
            else
            {
                retorno = 0;
            }
        }
        return retorno;
    }

    protected void gvDados_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
    {
        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
        {
            HorasDedicadasEquipe = 0;
            FatorProdutividadeEquipe = 0;
        }
        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
        {
            decimal tempHorasDedicadas = 0;
            decimal tempPercentualAlocacao = 0;
            bool retorno = decimal.TryParse(e.GetValue("HorasDedicadas") != null ? e.GetValue("HorasDedicadas").ToString() : "0", out tempHorasDedicadas);
            if (retorno)
            {
                HorasDedicadasEquipe += tempHorasDedicadas;
            }
            retorno = decimal.TryParse(e.GetValue("PercentualAlocacao") != null ? e.GetValue("PercentualAlocacao").ToString() : "0", out tempPercentualAlocacao);
            if (retorno)
            {
                FatorProdutividadeEquipe += tempPercentualAlocacao;
            }
        }
        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
        {
            int contaLinhasGrid = gvItens.VisibleRowCount == 0 ? 1 : gvItens.VisibleRowCount;
            txtCapacidade_AbaEquipe.Text = string.Format("{0:n2}", HorasDedicadasEquipe * 1 /*(getFatorProdutividadeEquipe())*/);
            txtFatorProdutividadeEquipe.Text = string.Format(@"{0:p2}", getFatorProdutividadeEquipe());
        }
    }

    protected void gvDados_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
    {
        if (!e.IsTotalSummary)
        {
            if (e.Item.FieldName == "HorasDedicadas")
            {
                decimal temp = 0;
                bool retorno = decimal.TryParse(e.Value != null ? e.Value.ToString() : "0", out temp);
                if (retorno)
                    HorasDedicadasEquipe += temp;
            }
        }
        else
        {
            txtTotalHorasEquipe.Text = string.Format("{0:n2}", HorasDedicadasEquipe);
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        // <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (!indicaPublicado) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""abreSelecaoTarefas();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "abreSelecaoTarefas();", true, false, false, "PlanSpri", "Planejamento", this);
    }

    protected void menu_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadUsu");
    }

    protected void menu_equipe_Init(object sender, EventArgs e)
    {
        /*
        <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (!indicaPublicado) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        */
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "pcSelecaoMembrosEquipe.Show();", true, false, false, "PlanSpri", "Planejamento", this);
    }

    protected void menu_equipe_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadUsu");
    }

    protected void gvSelecaoMembrosEquipe_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        StringBuilder sql = new StringBuilder();

        #region Comando SQL

        sql.AppendFormat(@"
BEGIN
    DECLARE @CodigoIteracao Int
     SELECT @CodigoIteracao = CodigoIteracao
       FROM Agil_Iteracao
      WHERE CodigoProjetoIteracao = {0}", idProjeto);
        sql.AppendLine();

        #endregion

        var listValues = gvSelecaoMembrosEquipe.GetSelectedFieldValues("CodigoRecursoCorporativo", "CodigoTipoPapelRecursoProjeto");
        listValues.ForEach(o =>
        {
            var values = (object[])o;
            var codigoRecursoCorporativo = values.First();
            var codigoTipoPapelRecursoProjeto = values.Last();

            #region Comando SQL

            sql.AppendFormat(@"
    INSERT INTO [dbo].[Agil_RecursoIteracao]
               ([CodigoRecursoCorporativo]
               ,[CodigoIteracao]
               ,[PercentualAlocacao]
               ,[CodigoTipoPapelRecursoIteracao])
         VALUES
               ({0}
               ,@CodigoIteracao
               ,100
               ,{1})",
               codigoRecursoCorporativo,
               codigoTipoPapelRecursoProjeto);
            sql.AppendLine();

            #endregion
        });
        sql.Append("END");
        int qtdeRegistrosAfetados = 0;
        cDados.execSQL(sql.ToString(), ref qtdeRegistrosAfetados);
        var sucesso = qtdeRegistrosAfetados == gvSelecaoMembrosEquipe.Selection.Count;
        if (sucesso)
        {
            gvSelecaoMembrosEquipe.Selection.UnselectAll();
            gvSelecaoMembrosEquipe.DataBind();
        }
    }

    protected void callbackPopupItemBacklog_Callback(object source, CallbackEventArgs e)
    {
        //    callbackPopupItemBacklog.PerformCallback(valores[0] + '|' + 'editarItensPlanejamento|' + gvItens.cpCodigoProjeto + '|' + gvItens.cpCodigoProjetoAgil);

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
        WHERE ib.CodigoItem = {0}", codigoItem);
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