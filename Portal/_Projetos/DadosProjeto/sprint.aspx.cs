using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_sprint : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool podeEditar = true;
    public bool podeAssociar = true;
    public bool podeManterSprint = true;

    int? codigoIteracaoUltimaSprint = null;

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
        sdsProjectOwner.ConnectionString = cDados.classeDados.getStringConexao();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        cDados.setaTamanhoMaximoMemo(txtObjetivos, 4000, lblContadorMemoDescricao);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();

        cDados.setaTamanhoMaximoMemo(txtObjetivos, 4000, lblContadorMemoDescricao);

        if (Request.QueryString["IDProjeto"] != null)
        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }
        gvDados.JSProperties["cp_CodigoProjeto"] = idProjeto.ToString();

        podeIncluir = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_IncItSpt");
        podeExcluir = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_ExcItSpt");
        podeEditar = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_AltItSpt");
        podeAssociar = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_AssItSpt");
        if (!Page.IsPostBack)
        {
            podeManterSprint = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_ManSpt");
            if (!podeManterSprint)
            {
                try
                {

                    this.Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
                }
                catch
                {
                    this.Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcessoNoMaster.aspx";
                    this.Response.End();
                }
            }
        }
        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(idProjeto, "-1", 1, true, false, false, percentualConcluido, data);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        sdsProjectOwner.SelectCommand = cDados.getSelect_Usuario(codigoEntidadeUsuarioResponsavel, "");
        sdsProjectOwner.DataBind();

        populaTrabalhoAssociado();

        carregaGvDados();
        verificaVisibilidadeObjetos();
        cDados.aplicaEstiloVisual(Page);
    }

    private void populaTrabalhoAssociado()
    {
        //ddlPacoteTrabalho.Items.Clear();
        string selectCommand = string.Format(
         @"BEGIN
          SELECT -1 AS CodigoTarefa, -1 AS SequenciaTarefaCronograma, 'NENHUM PACOTE' AS NomeTarefa     
         union
         SELECT  tc.[CodigoTarefa]
				, tc.[SequenciaTarefaCronograma]
				, CONVERT(Varchar,tc.SequenciaTarefaCronograma) + ' - ' + tc.[NomeTarefa]	as NomeTarefa	
			FROM 
					[TarefaCronogramaProjeto]					AS [tc]
					
						INNER JOIN [CronogramaProjeto]	AS [cp]		ON 
							(cp.[CodigoCronogramaProjeto]	= tc.[CodigoCronogramaProjeto] and  tc.[CodigoCronogramaProjeto] = '{1}')
            INNER JOIN [TipoTarefaCronograma]	AS [ttc] ON (ttc.CodigoTipoTarefaCronograma = tc.CodigoTipoTarefaCronograma
																											 AND ttc.IniciaisTipoControladoSistema = 'ITERACAO')						
			WHERE
						cp.[CodigoProjeto]				= {0}
				AND cp.[DataExclusao]					IS NULL
				AND tc.[DataExclusao]					IS NULL				
        AND tc.[IndicaTarefaResumo] = 'N' 
        AND tc.CodigoTarefa  not in(SELECT i.CodigoTarefaItemEAP
                                      FROM [Agil_Iteracao]   i                                                        
                                     WHERE CodigoTarefaItemEAP is not null  
                                     AND i.CodigoCronogramaProjetoItemEAP = '{1}')                
     ORDER BY 2
END", idProjeto, codigoCronogramaProjeto);

        DataSet ds = cDados.getDataSet(selectCommand);
        ddlPacoteTrabalho.DataSource = ds;
        ddlPacoteTrabalho.TextField = "NomeTarefa";
        ddlPacoteTrabalho.ValueField = "CodigoTarefa";
        ddlPacoteTrabalho.DataBind();
    }

    private void carregaGvDados()
    {
        DataSet ds = getSprints(idProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

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
                ckbCopiarRaias.Text = string.Format(Resources.traducao.sprint_copiar_raias_do_sprint_anterior___0__, ultimaSprint.Titulo);
                codigoIteracaoUltimaSprint = ultimaSprint.CodigoIteracao;
            }
            else
            {
                ckbCopiarRaias.ClientVisible = false;
            }
        }
        gvDados.JSProperties["cpCodigoProjeto"] = idProjeto;
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

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        int altura = (alturaPrincipal - 190);

        //gvDados.Settings.VerticalScrollableHeight = altura - 200;
        gvDados.Width = new Unit("100%");
    }
    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        return codigoDado;
    }
    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/sprint.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "sprint"));
        Header.Controls.Add(cDados.getLiteral(@"<title>sprint</title>"));
    }
    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";
        ((ASPxGridView)sender).JSProperties["cpIndicaExclusao"] = "";


        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();

            if (mensagemErro_Persistencia == "OK")
            {
                hfGeral.Set("StatusSalvar", "1");
                ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.sprint_item_inclu_do_com_sucesso_;
            }

        }

        else if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();

            if (mensagemErro_Persistencia == "OK")
            {
                hfGeral.Set("StatusSalvar", "1");
                ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.sprint_item_alterado_com_sucesso_;
            }

        }
        else if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            if (mensagemErro_Persistencia.Contains("REFERENCE") && mensagemErro_Persistencia.Contains("Recurso"))
            {
                mensagemErro_Persistencia = Resources.traducao.sprint_para_ser_poss_vel_excluir_este_sprint__acesse_o_sprint_e_exclua_primeiro_todos_os_membros_da_equipe__gil_;
            }
            if (mensagemErro_Persistencia == "OK")
            {
                hfGeral.Set("StatusSalvar", "1");
                ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.sprint_item_exclu_do_com_sucesso_;
                ((ASPxGridView)sender).JSProperties["cpIndicaExclusao"] = "S";
            }
        }
        ((ASPxGridView)sender).JSProperties["cpErro"] = (mensagemErro_Persistencia == "OK" || mensagemErro_Persistencia == "") ? "" : mensagemErro_Persistencia;
    }

    private void verificaVisibilidadeObjetos()
    {
        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "controlaClientesGestaoAgil");

        //ddlPacoteTrabalho.JSProperties["cp_Visivel"] = "S";

        //if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["controlaClientesGestaoAgil"] + "" == "N")
        //{
        ddlPacoteTrabalho.JSProperties["cp_Visivel"] = "N";
        tdPacote.Style.Add(HtmlTextWriterStyle.Display, "none");
        //gvDados.Columns["NomePacoteTrabalho"].Visible = false;
        //}
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";

        string chave = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjetoIteracao").ToString();
        DataSet ds = cDados.excluiSprint(chave, getChavePrimaria(), idProjeto.ToString());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }
        return retorno;
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        string tituloItem = txtTitulo.Text.Replace("'", "'+char(39)+'");
        string inicio = dtInicio.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        string termino = dtTermino.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        int codigoProjectOwner = int.Parse(ddlProjectOwner.Value.ToString());
        int codigoPacoteTrabalho = ddlPacoteTrabalho.Value == null ? -1 : int.Parse(ddlPacoteTrabalho.Value.ToString());
        string objetivos = txtObjetivos.Text.Replace("'", "'+char(39)+'");
        string chave = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjetoIteracao").ToString();

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
        int codigoPacoteTrabalho = ddlPacoteTrabalho.Value == null ? -1 : int.Parse(ddlPacoteTrabalho.Value.ToString());
        string objetivos = txtObjetivos.Text.Replace("'", "'+char(39)+'");
        int codigoIteracao = 0;
        DataSet ds = incluiSprint(codigoEntidadeUsuarioResponsavel, tituloItem, inicio, termino, codigoProjectOwner, codigoPacoteTrabalho, objetivos, idProjeto, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, codigoCronogramaProjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            codigoIteracao = (int)ds.Tables[0].Rows[0][1];
        }
        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoIteracao);
        return retorno;
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "PR_Sprint", "Sprint", this);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abrePopupSprintInclusao(" + Request.QueryString["IDProjeto"] + ");", true, true, false, "PR_Sprint", "Sprint", this);

        //abrePopupSprintInclusao(codigoProjetoAgil)
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PR_Sprint");
    }

    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.ErrorTextKind == GridErrorTextKind.General)
        {
            e.ErrorText = e.Exception.Message;
        }
        else if (e.ErrorTextKind == GridErrorTextKind.RowValidate)
        {
            e.ErrorText = Resources.traducao.sprint_erro_de_valida__o__ + e.ErrorText;
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        string status = (gvDados.GetRowValues(e.VisibleIndex, "Status") == null) ? "" : gvDados.GetRowValues(e.VisibleIndex, "Status").ToString();
        string codigoPacoteTrabalho = (gvDados.GetRowValues(e.VisibleIndex, "CodigoPacoteTrabalho") == null) ? "" : gvDados.GetRowValues(e.VisibleIndex, "CodigoPacoteTrabalho").ToString();

        int int_codigoPacoteTrabalho = -1;
        bool retorno_codigoPacoteTrabalho = int.TryParse(codigoPacoteTrabalho, out int_codigoPacoteTrabalho);


        if (e.ButtonID == "btnExcluirCustom")
        {
            e.Text = "Excluir";
            if (status == "Não Iniciado")
            {                
                e.Enabled = podeExcluir;
                e.Image.Url = podeExcluir ? "~/imagens/botoes/excluirReg02.PNG" : "~/imagens/botoes/excluirRegDes.png";
            }
            else
            {
                e.Enabled = false;                
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        if(e.ButtonID == "btnEditarCustom")
        {
            e.Text = "Editar";
            e.Enabled = podeEditar;
            e.Image.Url = podeEditar ? "~/imagens/botoes/editarReg02.PNG" : "~/imagens/botoes/editarRegDes.png";
        }
        if(e.ButtonID == "btnSelecionarItensDeBacklog")
        {
            e.Enabled = podeAssociar;
            e.Image.Url = podeAssociar == true ? "~/imagens/exchange-arrows.png" : "~/imagens/exchange-arrows.png";
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    public string getDescricaoObjetosLista()
    {
        string retornoHTML = "";

        string codigoProjetoAtual = Eval("CodigoProjetoIteracao").ToString();
        string nomeProjetoAtual = Eval("Titulo").ToString();
        string codigoProjetoAgil = idProjeto.ToString();

        retornoHTML = "<table><tr><td>";
        string styleCor = "";

        //retornoHTML += "<img border='0' src='../imagens/projeto.PNG'/>";
        //Retirada a verificação de permissão (PBH). A linha abaixo foi colocada e as outras foram comentadas.
        retornoHTML += "</td><td>&nbsp;<a  "
            + styleCor
            + " class='LinkGrid' href='./indexResumoProjeto.aspx?IDProjeto="
            + codigoProjetoAtual
            + "&NomeProjeto="
            + nomeProjetoAtual
            + "&CPA="
            + codigoProjetoAgil
            + "' target='_parent' title='Acessar os detalhes da sprint'>" + nomeProjetoAtual + "</a>";

        retornoHTML += "</td></tr></table>";

        return retornoHTML;
    }

    protected void pnPacoteTrabalho_Callback(object sender, CallbackEventArgsBase e)
    {
        populaTrabalhoAssociado();
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

    public DataSet incluiSprint(int codigoEntidadeLogada, string TituloItem, string Inicio, string Termino,
        int CodigoProjectOwner, int CodigoPacoteTrabalhoAssociado, string Objetivos, int codigoProjeto, int codigoUsuarioInclusao, int CodigoEntidade, int codigoProjetoAtual, string codigoCronogramaProjeto)
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
        100, 
        CodigoTipoPapelRecursoProjeto 
   FROM Agil_RecursoProjeto
  WHERE CodigoProjeto = @CodigoProjetoAgil") : "";


        string sqlCopiaRaiasSprintAnterior = string.Empty;
        if (codigoIteracaoUltimaSprint.HasValue && ckbCopiarRaias.Checked)
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
     WHERE ri.[CodigoIteracao] = @CodigoIteracaoUltimaSprint
", codigoIteracaoUltimaSprint);
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
           ,'#fbfbfb'),
           (@CodigoRetorno
           ,'Fazendo'
           ,0
           ,1
           ,'#fbfbfb'),
           (@CodigoRetorno
           ,'Feito'
           ,100
           ,255
           ,'#fbfbfb')");
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
        sets += string.Format(@" SET @in_CodigoEntidade = {0}", CodigoEntidade);
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

    protected void pnResponsavelScrumMaster_Callback(object sender, CallbackEventArgsBase e)
    {
        string comandoSQLGerenteProjeto = string.Format(@"
       SELECT p.CodigoGerenteProjeto , u.NomeUsuario as NomeGerenteProjeto 
         FROM Projeto p
        INNER JOIN Usuario u on (u.CodigoUsuario = p.CodigoGerenteProjeto)
        WHERE p.CodigoProjeto = {0} ", idProjeto);

        DataSet dsGerente = cDados.getDataSet(comandoSQLGerenteProjeto);
        if (cDados.DataSetOk(dsGerente) && cDados.DataTableOk(dsGerente.Tables[0]))
        {
            ddlProjectOwner.Text = dsGerente.Tables[0].Rows[0]["NomeGerenteProjeto"].ToString();
            ddlProjectOwner.Value = (int)dsGerente.Tables[0].Rows[0]["CodigoGerenteProjeto"];

        }
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cpErro"] = "";
        ((ASPxGridView)sender).JSProperties["cpSucesso"] = "";

        string retorno = persisteExclusaoRegistro();
        if (retorno == "OK")
        {
            ((ASPxGridView)sender).JSProperties["cpSucesso"] = Resources.traducao.sprint_itens_de_backlog_exclu_dos_com_sucesso;
        }
        else
        {
            string msgErro = retorno;
            if (retorno.Contains("REFERENCE") && retorno.Contains("Recurso"))
            {
                msgErro = Resources.traducao.sprint_para_ser_poss_vel_excluir_este_sprint_exclua_primeiro_todos_os_membros_da_equipe__gil;
            }
            ((ASPxGridView)sender).JSProperties["cpErro"] = "Erro: " + msgErro;
        }
        e.Cancel = true;
    }

}