using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
public partial class _Projetos_DadosProjeto_popupSprint : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    public int codigoIteracao = 0;
    public int codigoProjetoIteracao = -1;
    public int codigoProjetoAgil = 0;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    public bool somenteLeitura = false;

    static int? codigoIteracaoUltimaSprint = null;

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
        cDados.setaTamanhoMaximoMemo(txtObjetivos, 4000, lblContadorMemoDescricao);

        bool retorno = int.TryParse(Request.QueryString["CI"] + "", out codigoIteracao);
        retorno = int.TryParse(Request.QueryString["CPI"] + "", out codigoProjetoIteracao);
        retorno = int.TryParse(Request.QueryString["CPA"] + "", out codigoProjetoAgil);

        somenteLeitura = !cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioLogado, codigoEntidadeLogada, "PR", "PR_ManSpt");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();

        cDados.setaTamanhoMaximoMemo(txtObjetivos, 4000, lblContadorMemoDescricao);
        populaProjectOwner();
        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(codigoProjetoAgil, "-1", 1, true, false, false, percentualConcluido, data);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        if (!IsPostBack)
        {
            carregarDadostela();
        }
        populaComboEscolheModeloRaia();
        cDados.aplicaEstiloVisual(Page);
    }

    private void populaComboEscolheModeloRaia()
    {

        DataSet dsSprintsDesteProjetoAgil = cDados.getSprints(codigoProjetoAgil, "");
        DataTable novoDataTable = dsSprintsDesteProjetoAgil.Tables[0];
        novoDataTable.DefaultView.Sort = "Termino DESC";
        DataSet novoDataSet = new DataSet();
        novoDataSet.Tables.Add(novoDataTable.DefaultView.ToTable());

        ddlEscolheModeloRaia.TextField = "Titulo";
        ddlEscolheModeloRaia.ValueField = "CodigoIteracao";
        ddlEscolheModeloRaia.DataSource = novoDataSet;
        ddlEscolheModeloRaia.DataBind();
        ddlEscolheModeloRaia.Items.Insert(0, new ListEditItem("(usar modelo padrão)", -1));
        if (!IsPostBack)
        {
            //37843 - Corrigir a falha do IB-37842 ao incluir a primeira sprint num projeto
            ListEditItem litem = (codigoIteracaoUltimaSprint != null) ? ddlEscolheModeloRaia.Items.FindByValue(codigoIteracaoUltimaSprint.Value.ToString()) : null;
            if (litem == null)
            {
                ddlEscolheModeloRaia.SelectedIndex = 0;
            }
            else
            {
                ddlEscolheModeloRaia.SelectedItem = litem;
            }
        }
    }

    private void populaProjectOwner()
    {

        string comandoSQL = string.Format(@"
               
               SELECT us.CodigoUsuario
                ,   us.NomeUsuario
                ,   us.ContaWindows
                ,   us.TipoAutenticacao
                ,   us.EMail
                ,   us.ResourceUID
                ,   us.TelefoneContato1
                ,   us.TelefoneContato2
                ,   us.Observacoes
                ,   us.DataInclusao
                ,   us.CodigoUsuarioInclusao
                ,   us.DataUltimaAlteracao
                ,   us.CodigoUsuarioUltimaAlteracao
                ,   us.DataExclusao
                ,   us.CodigoUsuarioExclusao
                ,   us.IDEstiloVisual
                ,   us.CodigoEntidadeResponsavelUsuario
                ,   uun.IndicaUsuarioAtivoUnidadeNegocio
                ,   us.CPF 
              
                FROM        Usuario AS us 
                INNER JOIN  UsuarioUnidadeNegocio AS uun ON (uun.CodigoUsuario = us.CodigoUsuario )
				inner join RecursoCorporativo rc on rc.CodigoUsuario = us.CodigoUsuario
				inner join Agil_RecursoProjeto arp on (arp.CodigoRecursoCorporativo  = rc.CodigoRecursoCorporativo)

				WHERE Uun.CodigoUnidadeNegocio = {0} and arp.CodigoProjeto = {1}
				and arp.CodigoTipoPapelRecursoProjeto in (select CodigoTipoPapelRecurso 
				                                            from Agil_TipoPapelRecurso 
														   where IniciasTipoPapelRecursoControladoSistema = 'SCM')
                ORDER BY us.NomeUsuario
                ", codigoEntidadeLogada, codigoProjetoAgil);

        DataSet dsProjectOwner = cDados.getDataSet(comandoSQL);

        ddlProjectOwner.TextField = "NomeUsuario";
        ddlProjectOwner.ValueField = "CodigoUsuario";

        ddlProjectOwner.DataSource = dsProjectOwner;
        ddlProjectOwner.DataBind();
    }

    private void carregarDadostela()
    {
        tdCopiaRaiasRecursos.Style.Clear();

        string comandoSQL = string.Format(@"
        SELECT ai.[CodigoIteracao]
              ,ai.[CodigoProjetoIteracao]
              ,p.[NomeProjeto] as Titulo
              ,ai.[InicioPrevisto] as Inicio
              ,ai.[TerminoPrevisto] as Termino
              ,dbo.f_Agil_GetStatusIteracao(ai.[CodigoIteracao], ai.[DataPublicacaoPlanejamento], ai.[TerminoReal]) as Status              
              ,u.NomeUsuario as NomeProjectOwner
              ,p.[CodigoGerenteProjeto] as CodigoProjectOwner
              , tc.NomeTarefa as NomePacoteTrabalho
              ,ai.[CodigoTarefaItemEAP] as CodigoPacoteTrabalho
              ,p.[DescricaoProposta] as Objetivos
              ,ai.FatorProdutividade
			  ,(SELECT COUNT(1) 
				  FROM [dbo].[Agil_ItemBacklog] ib
            INNER JOIN Agil_Iteracao aii on (aii.CodigoIteracao = ib.CodigoIteracao)
				 WHERE ib.[CodigoIteracao] = ai.CodigoIteracao
				   AND ib.[CodigoUsuarioExclusao] IS NULL
				   AND ISNULL(ib.[IndicaTarefa], 'N') = 'N') as Quantidade
              ,CASE WHEN (SELECT  aiii.DataPublicacaoPlanejamento 
			                FROM [Agil_Iteracao] aiii 
					       WHERE aiii.CodigoIteracao = ai.CodigoIteracao) IS NOT NULL  THEN 'S' ELSE 'N' END AS IndicaPublicado  
		, p.codigoProjeto
        FROM [Projeto] p
        INNER JOIN LinkProjeto lp on (lp.CodigoProjetoFilho  = p.CodigoProjeto) 
        INNER JOIN [Agil_Iteracao] ai on (ai.CodigoProjetoIteracao = p.CodigoProjeto) 
        left JOIN [Usuario] u on (u.CodigoUsuario = p.CodigoGerenteProjeto)
        left join [TarefaCronogramaProjeto] tc on (ai.[CodigoTarefaItemEAP] = tc.CodigoTarefa and tc.CodigoCronogramaProjeto = (SELECT CodigoCronogramaProjeto 
		                                                                                                                          FROM CronogramaProjeto  
																																  WHERE CodigoProjeto = (SELECT CodigoProjetoPai 
		                                                                                                                                                  FROM linkprojeto 
		                                                                                                                                                 WHERE CodigoProjetoFilho = {0})))
	    WHERE ai.codigoprojetoiteracao = {0}", codigoProjetoIteracao);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            txtTitulo.Text = ds.Tables[0].Rows[0]["Titulo"].ToString();

            DateTime dataInicio = DateTime.MinValue;
            DateTime.TryParse(ds.Tables[0].Rows[0]["Inicio"].ToString(), out dataInicio);
            dtInicio.Value = dataInicio;

            DateTime dataTermino = DateTime.MinValue;
            DateTime.TryParse(ds.Tables[0].Rows[0]["Termino"].ToString(), out dataTermino);
            dtTermino.Value = dataTermino;

            txtStatus.Text = ds.Tables[0].Rows[0]["Status"].ToString();

            ddlProjectOwner.Value = int.Parse(ds.Tables[0].Rows[0]["CodigoProjectOwner"].ToString());
            ddlProjectOwner.Text = (ds.Tables[0].Rows[0]["NomeProjectOwner"] + "");

            txtObjetivos.Text = ds.Tables[0].Rows[0]["Objetivos"].ToString();

            txtTitulo.ReadOnly = somenteLeitura;
            dtInicio.ReadOnly = somenteLeitura;
            dtTermino.ReadOnly = somenteLeitura;
            ddlProjectOwner.ReadOnly = somenteLeitura;
            txtObjetivos.ReadOnly = somenteLeitura;
            //btnSalvar.ClientVisible = !somenteLeitura;
            tdCopiaRaiasRecursos.Style.Add("display", "none");

        }
        else
        {
            //tdCopiaRaiasRecursos.Style.Add("display", "block");
            carregaChecksCopiaRaiasSprintAnterior();
        }
    }

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/popupSprint.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "popupSprint"));
        Header.Controls.Add(cDados.getLiteral(@"<title>sprint</title>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        int altura = (alturaPrincipal - 190);
    }

    protected void pnResponsavelScrumMaster_Callback(object sender, CallbackEventArgsBase e)
    {
        string comandoSQLGerenteProjeto = string.Format(@"
       SELECT p.CodigoGerenteProjeto , u.NomeUsuario as NomeGerenteProjeto 
         FROM Projeto p
        INNER JOIN Usuario u on (u.CodigoUsuario = p.CodigoGerenteProjeto)
        WHERE p.CodigoProjeto = {0} ", codigoProjetoIteracao);

        DataSet dsGerente = cDados.getDataSet(comandoSQLGerenteProjeto);
        if (cDados.DataSetOk(dsGerente) && cDados.DataTableOk(dsGerente.Tables[0]))
        {
            ddlProjectOwner.Text = dsGerente.Tables[0].Rows[0]["NomeGerenteProjeto"].ToString();
            ddlProjectOwner.Value = (int)dsGerente.Tables[0].Rows[0]["CodigoGerenteProjeto"];

        }
    }

    private void carregaChecksCopiaRaiasSprintAnterior()
    {
        DataSet ds = getSprints(codigoProjetoAgil, "");

        if (cDados.DataSetOk(ds))
        {
            var rows = ds.Tables[0].AsEnumerable();
            if (rows.Count() > 0)
            {
                var ultimaSprint = rows
                    .OrderByDescending(r => r.Field<int>("CodigoProjetoIteracao"))
                    .Select(r => new
                    {
                        CodigoIteracao = r.Field<int>("CodigoIteracao"),
                        Titulo = r.Field<string>("Titulo")
                    })
                    .FirstOrDefault();
                //ckbCopiarRaias.Text = string.Format(Resources.traducao.sprint_copiar_raias_do_sprint_anterior___0__, ultimaSprint.Titulo);
                codigoIteracaoUltimaSprint = ultimaSprint.CodigoIteracao;
            }
            else
            {
                //ckbCopiarRaias.ClientVisible = false;
            }
        }
    }

    private DataSet getSprints(int idProjeto, string where)
    {

        string comandoSQL = string.Format(@"
    SELECT ai.[CodigoIteracao]
              ,ai.[CodigoProjetoIteracao]
              ,p.[NomeProjeto] as Titulo
              ,ai.[InicioPrevisto] as Inicio
              ,ai.[TerminoPrevisto] as Termino
              ,{0}.{1}.f_Agil_GetStatusIteracao(ai.[CodigoIteracao], ai.[DataPublicacaoPlanejamento], ai.[TerminoReal]) as Status              
              ,u.NomeUsuario as NomeProjectOwner
              ,p.[CodigoGerenteProjeto] as CodigoProjectOwner
              , tc.NomeTarefa as NomePacoteTrabalho
              ,ai.[CodigoTarefaItemEAP] as CodigoPacoteTrabalho
              ,p.[DescricaoProposta] as Objetivos
              ,ai.FatorProdutividade
			  ,(SELECT COUNT(1) 
				  FROM [dbo].[Agil_ItemBacklog] ib
            INNER JOIN Agil_Iteracao aii on (aii.CodigoIteracao = ib.CodigoIteracao)
				 WHERE ib.[CodigoIteracao] = ai.CodigoIteracao
				   AND ib.[CodigoUsuarioExclusao] IS NULL
				   AND ISNULL(ib.[IndicaTarefa], 'N') = 'N') as Quantidade
              ,CASE WHEN (SELECT  aiii.DataPublicacaoPlanejamento 
			                FROM [Agil_Iteracao] aiii 
					       WHERE aiii.CodigoIteracao = ai.CodigoIteracao) IS NOT NULL  THEN 'S' ELSE 'N' END AS IndicaPublicado  
        FROM {0}.{1}.[Projeto] p
        INNER JOIN {0}.{1}.LinkProjeto lp on (lp.CodigoProjetoFilho  = p.CodigoProjeto) 
        INNER JOIN {0}.{1}.[Agil_Iteracao] ai on (ai.CodigoProjetoIteracao = p.CodigoProjeto) 
        left JOIN {0}.{1}.[Usuario] u on (u.CodigoUsuario = p.CodigoGerenteProjeto)
        left join {0}.{1}.[TarefaCronogramaProjeto] tc on (ai.[CodigoTarefaItemEAP] = tc.CodigoTarefa and tc.CodigoCronogramaProjeto in (select CodigoCronogramaProjeto from CronogramaProjeto  where CodigoProjeto = {2}))
        where lp.CodigoProjetoPai = {2} and p.DataExclusao is null
        order by p.[NomeProjeto] asc
        {3}", cDados.getDbName(), cDados.getDbOwner(), idProjeto, where);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;

    }



    protected void callbackTela_Callback(object source, CallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";

        if (e.Parameter == "salvar")
        {

            if (codigoProjetoIteracao == 0)
            {
                mensagemErro_Persistencia = persisteInclusaoRegistro();
                if (mensagemErro_Persistencia == "OK")
                {
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Sprint incluída com sucesso!";
                }
            }
            else
            {
                mensagemErro_Persistencia = persisteEdicaoRegistro();
                if (mensagemErro_Persistencia == "OK")
                {
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Sprint alterada com sucesso!";
                }
            }
            if (mensagemErro_Persistencia != "OK" && mensagemErro_Persistencia != "")
            {
                ((ASPxCallback)source).JSProperties["cpErro"] = mensagemErro_Persistencia;
            }
        }
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        string tituloItem = txtTitulo.Text.Replace("'", "'+char(39)+'");
        string inicio = dtInicio.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        string termino = dtTermino.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        int codigoProjectOwner = int.Parse(ddlProjectOwner.Value.ToString());
        int codigoPacoteTrabalho = -1;
        string objetivos = txtObjetivos.Text.Replace("'", "'+char(39)+'");
        string chave = codigoProjetoIteracao.ToString();

        DataSet ds = atualizaSprint(int.Parse(chave), tituloItem, inicio, termino, objetivos, codigoProjectOwner.ToString(), codigoPacoteTrabalho.ToString());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";

        string tituloItem = txtTitulo.Text.Replace("'", "'+char(39)+'");
        string inicio = dtInicio.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        string termino = dtTermino.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        int codigoProjectOwner = int.Parse(ddlProjectOwner.Value.ToString());
        int codigoPacoteTrabalho = -1;
        string objetivos = txtObjetivos.Text.Replace("'", "'+char(39)+'");
        int codigoIteracao = 0;
        DataSet ds = incluiSprint(tituloItem, inicio, termino, codigoProjectOwner, codigoPacoteTrabalho, objetivos, codigoProjetoAgil, codigoUsuarioLogado, codigoProjetoAgil, codigoCronogramaProjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            codigoIteracao = (int)ds.Tables[0].Rows[0][1];
        }


        return retorno;
    }


    public DataSet incluiSprint(string TituloItem, string Inicio, string Termino,
        int CodigoProjectOwner, int CodigoPacoteTrabalhoAssociado, string Objetivos, int codigoProjeto, int codigoUsuarioInclusao, int codigoProjetoAtual, string codigoCronogramaProjeto)
    {
        string copiaRecursosDoProjeto = ckbCopiarRecursos.Checked == true ? string.Format(@"DECLARE @CodigoProjetoAgil INT,
        @CodigoIteracao INT

    SET @CodigoIteracao = @CodigoRetorno

 SELECT @CodigoProjetoAgil = CodigoProjetoPai 
   FROM LinkProjeto AS lp INNER JOIN
        Agil_Iteracao AS i ON i.CodigoProjetoIteracao = lp.CodigoProjetoFilho
  WHERE i.CodigoIteracao = @CodigoIteracao
    AND lp.TipoLink = 'PJPJ'

 INSERT INTO Agil_RecursoIteracao 
 (
        CodigoRecursoCorporativo, 
        CodigoIteracao, 
        PercentualAlocacao, 
        CodigoTipoPapelRecursoIteracao
 )
 SELECT CodigoRecursoCorporativo, 
        @CodigoIteracao, 
        ISNULL(PercentualAlocacao, 0),
        CodigoTipoPapelRecursoProjeto 
   FROM Agil_RecursoProjeto
  WHERE CodigoProjeto = @CodigoProjetoAgil") : "";


        string sqlCopiaRaiasSprintAnterior = string.Empty;

        if (ddlEscolheModeloRaia.SelectedIndex > 0)
        {
            sqlCopiaRaiasSprintAnterior = string.Format(@"
DECLARE @CodigoIteracaoUltimaSprint INT
    SET @CodigoIteracaoUltimaSprint = {0}

INSERT INTO [dbo].[Agil_RaiasIteracao]
           ([CodigoIteracao]
           ,[NomeRaia]
           ,[PercentualConcluido]
           ,[SequenciaApresentacaoRaia]
           ,[CorCabecalho])
    SELECT  @CodigoRetorno
           ,ri.[NomeRaia]
           ,ri.[PercentualConcluido]
           ,ri.[SequenciaApresentacaoRaia]
           ,ri.[CorCabecalho]
      FROM [dbo].[Agil_RaiasIteracao] AS ri
     WHERE ri.[CodigoIteracao] = @CodigoIteracaoUltimaSprint ", ddlEscolheModeloRaia.Value);
        }
        else
        {
            sqlCopiaRaiasSprintAnterior = string.Format(@"
INSERT INTO [dbo].[Agil_RaiasIteracao]
           ([CodigoIteracao]
           ,[NomeRaia]
           ,[PercentualConcluido]
           ,[SequenciaApresentacaoRaia]
           ,[CorCabecalho])
     VALUES
           (@CodigoRetorno
           ,'A fazer'
           ,0
           ,0
           ,'#58978e'),
           (@CodigoRetorno
           ,'Fazendo'
           ,50
           ,1
           ,'#dc6a2e'),
           (@CodigoRetorno
           ,'Feito'
           ,100
           ,255
           ,'#25364a')");
        }

        string comandoSQL = "";
        string sets = "";

        sets += string.Format(@" SET @in_Titulo = '{0}'", TituloItem);
        sets += string.Format(@" SET @in_DataInicio =  CONVERT(DateTime,'{0}',103)", Inicio);
        sets += string.Format(@" SET @in_DataTermino = CONVERT(DateTime,'{0}',103)", Termino);
        sets += string.Format(@" SET @in_codigoProjectOwner = {0}", CodigoProjectOwner);
        sets += string.Format(@" SET @in_CodigoPacoteTrabalho = {0}", CodigoPacoteTrabalhoAssociado == -1 ? "NULL" : CodigoPacoteTrabalhoAssociado.ToString());
        sets += string.Format(@" SET @in_Objetivos = '{0}'", Objetivos);
        sets += string.Format(@" SET @in_CodigoProjeto = {0}", codigoProjeto);
        sets += string.Format(@" SET @in_CodigoUsuarioInclusao = {0}", codigoUsuarioInclusao);
        sets += string.Format(@" SET @in_CodigoEntidade = {0}", codigoEntidadeLogada);
        sets += string.Format(@" SET @in_CodigoProjetoAtual = {0}", codigoProjetoAtual);
        sets += string.Format(@" SET @in_codigoCronogramaProjeto = '{0}'", codigoCronogramaProjeto);

        comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_Titulo varchar(150)
        DECLARE @in_DataInicio datetime
        DECLARE @in_DataTermino datetime
        DECLARE @in_CodigoStatus int
        DECLARE @in_codigoProjectOwner int
        DECLARE @in_CodigoPacoteTrabalho int
        DECLARE @in_Objetivos varchar(4000)
        DECLARE @in_CodigoProjeto int
        DECLARE @in_CodigoUsuarioInclusao INT
        DECLARE @in_CodigoEntidade Int
        DECLARE @in_CodigoProjetoAtual Int
        DECLARE @in_codigoCronogramaProjeto varchar(64)
       
        {2}

       EXECUTE @RC = {0}.{1}.[p_Agil_IncluiIteracao] 
        @in_Titulo
	    ,@in_DataInicio
	    ,@in_DataTermino
        ,@in_CodigoStatus
        ,@in_codigoProjectOwner		
	    ,@in_CodigoPacoteTrabalho
	    ,@in_Objetivos
        ,@in_CodigoUsuarioInclusao
        ,@in_CodigoEntidade
        ,@in_CodigoProjetoAtual
        ,0
        ,@in_codigoCronogramaProjeto

SET @CodigoRetorno = @RC
{3}
{4}
", cDados.getDbName(), cDados.getDbOwner(), sets, copiaRecursosDoProjeto, sqlCopiaRaiasSprintAnterior);

        DataSet ds1 = cDados.getDataSet(cDados.geraBlocoBeginTran() + "  " + comandoSQL + "  " + cDados.geraBlocoEndTran_ComRetorno());
        return ds1;
    }


    public DataSet atualizaSprint(int idProjeto, string tituloSprint, string inicio, string termino, string Objetivos, string codigoProjectOwner, string codigoPacoteTrabalho)
    {
        string comandoSQL = string.Format(@"    
        UPDATE {0}.{1}.Projeto 
               SET NomeProjeto = '{3}'
                  ,DescricaoProposta = '{4}'
                  ,CodigoGerenteProjeto = {7}
             WHERE CodigoProjeto = {2}

            UPDATE {0}.{1}.Agil_Iteracao 
               SET InicioPrevisto = CONVERT(DateTime, '{5:dd/MM/yyyy}', 103)
                  ,TerminoPrevisto = CONVERT(DateTime, '{6:dd/MM/yyyy}', 103)
                  ,CodigoTarefaItemEAP = {8}
             WHERE CodigoProjetoIteracao = {2} --{6} depois fazer o tratamento destes dois"
            , /*{0}*/cDados.getDbName(), /*{1}*/cDados.getDbOwner()
            , /*{2}*/idProjeto
            , /*{3}*/tituloSprint
            , /*{4}*/Objetivos
            , /*{5}*/inicio
            , /*{6}*/termino
            , /*{7}*/codigoProjectOwner
            , /*{8}*/(codigoPacoteTrabalho == "-1") ? "null" : codigoPacoteTrabalho);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + " " + comandoSQL + " " + cDados.geraBlocoEndTran());
        return ds;
    }
}