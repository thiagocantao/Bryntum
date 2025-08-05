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

public partial class espacoTrabalho_frameProposta_Criterios : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoProjetoSelecionado;
    private string NomeProjeto;
    private decimal totalGridCritPos, totalGridCritNeg;
    bool desabilitaTela = false;
    DataSet dsOpcoes;
    int codigoCategoria = -1;
    int codigoEtapa = 0, codigoWorkflow = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        dsDados.ConnectionString = cDados.classeDados.getStringConexao();
        dsOpcao.ConnectionString = cDados.classeDados.getStringConexao();
        dsRiscos.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            callbackSalvar.JSProperties["cp_PodePassarFluxo"] = "N";
        }

        if (Request.QueryString["CP"] != null)
        {
            string temp = Request.QueryString["CP"].ToString();
            if (temp != "")
            {
                cDados.setInfoSistema("CodigoProjeto", int.Parse(temp));
            }
        }

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        string cp = cDados.getInfoSistema("CodigoProjeto") + "";
        codigoProjetoSelecionado = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);

        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());

        btnFechar.JSProperties["cpEhPopup"] = (Request.QueryString["PopUp"] != null && Request.QueryString["PopUp"].ToString() == "S").ToString();
        btnSalvar2.JSProperties["cpEhPopup"] = (Request.QueryString["PopUp"] != null && Request.QueryString["PopUp"].ToString() == "S").ToString();

        if (codigoProjetoSelecionado == -1)
        {
            try
            {
                int codigoInstanciaWf = int.Parse(Request.QueryString["CI"].ToString());
                codigoProjetoSelecionado = cDados.getCodigoProjetoInstanciaFluxo(codigoWorkflow, codigoInstanciaWf);
            }
            catch { }
        }

        if (Request.QueryString["CEWF"] != null && Request.QueryString["CEWF"].ToString() != "" && Request.QueryString["CEWF"].ToString() != "-1" && Request.QueryString["EU"] + "" != "S") 
        {
            codigoEtapa = int.Parse(Request.QueryString["CEWF"].ToString());
        }

        carregaComboCategoria();

        if(!IsPostBack)
            getInfoProjetoSelecionado();

        if (ddlCategoria.SelectedIndex != -1)
            codigoCategoria = int.Parse(ddlCategoria.Value.ToString());

        carregaDataSetOpcoes();       
        
        dsDados.SelectParameters["CodigoProjeto"].DefaultValue = codigoProjetoSelecionado + "";
        dsRiscos.SelectParameters["CodigoProjeto"].DefaultValue = codigoProjetoSelecionado + "";
        dsDados.SelectParameters["EtapaPreenchimento"].DefaultValue = codigoEtapa + "";
        dsRiscos.SelectParameters["EtapaPreenchimento"].DefaultValue = codigoEtapa + "";

        if (   ( (cDados.getInfoSistema("DesabilitarBotoes") != null && cDados.getInfoSistema("DesabilitarBotoes").ToString() == "S") ) ||
               ( (Request.QueryString["RO"] != null) && (Request.QueryString["RO"] == "S") )    )
        {
            desabilitaTela = true;
        }

        btnSalvar.ClientVisible = !desabilitaTela;
        btnSalvar2.ClientVisible = !desabilitaTela;
        ddlCategoria.ClientEnabled = !desabilitaTela;
        //Desabilita o combo de categoria para unidades que utilizam o Termo de Abertura "TAI" -> p.e. DIRET
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "identificaTermodeAbertura");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            ddlCategoria.ClientEnabled = dsParametros.Tables[0].Rows[0]["identificaTermodeAbertura"].ToString() == "TAI" ? false : !desabilitaTela;
        }


        this.Title = cDados.getNomeSistema();

        if (!IsPostBack)
        {
            gvCritPos.GroupBy(gvCritPos.Columns["NomeFatorPortfolio"]);
            gvCritPos.GroupBy(gvCritPos.Columns["NomeGrupo"]);
            gvCritNeg.GroupBy(gvCritNeg.Columns["NomeFatorPortfolio"]);
            gvCritNeg.GroupBy(gvCritNeg.Columns["NomeGrupo"]);
        }

        verificaPassagemFluxo();

        gvCritNeg.Settings.ShowFilterRow = false;
        gvCritNeg.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        gvCritPos.Settings.ShowFilterRow = false;
        gvCritPos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void verificaPassagemFluxo()
    {
        string comandoSQL = string.Format(@"DECLARE @CodigoProjeto Int
      
                                                SET @CodigoProjeto = {0}

                                                SELECT COUNT(1) AS QTD
                                                  FROM dbo.[ProjetoCriterioSelecao] AS [pcs] 
                                                 WHERE pcs.CodigoProjeto = @CodigoProjeto", codigoProjetoSelecionado);

        DataSet dsFormObr = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(dsFormObr) && cDados.DataTableOk(dsFormObr.Tables[0]) && int.Parse(dsFormObr.Tables[0].Rows[0][0].ToString()) != 0)
        {
            callbackSalvar.JSProperties["cp_PodePassarFluxo"] = "S";
        }
        else
        {
            callbackSalvar.JSProperties["cp_PodePassarFluxo"] = "N";
        }
    }

    private void getInfoProjetoSelecionado()
    {
        string comandoSQL = string.Format(
            @"SELECT P.NomeProjeto, C.DescricaoCategoria, P.CodigoCategoria
                FROM Projeto AS P INNER JOIN
                     Categoria AS C ON P.CodigoCategoria = C.CodigoCategoria
               WHERE P.CodigoProjeto = {0}", codigoProjetoSelecionado);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (ds != null & ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            NomeProjeto = dt.Rows[0]["NomeProjeto"].ToString();
            ddlCategoria.Value = dt.Rows[0]["CodigoCategoria"].ToString();
        }

    }

    protected void GridsCriterios_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "ATUALIZAR")
        {
            ((ASPxGridView)sender).ExpandAll();
            ((ASPxGridView)sender).DataBind();
            return;
        }

        ASPxGridView grid = (sender as ASPxGridView);
        string[] parametroCliente = e.Parameters.Split(';');
        string nomeCombo = parametroCliente[0];
        string Valor = parametroCliente[1];
        string texto = parametroCliente[2];

        string linhaAfetada = nomeCombo.Substring(nomeCombo.IndexOf("_cell") + 5, 2).Replace("_", "");

        string codigoCriterioSelecao = Valor.Substring(0, Valor.Length - 1);
        string CodigoOpcaoCriterioSelecao = Valor[Valor.Length - 1].ToString();
        if (codigoCriterioSelecao == "")
            codigoCriterioSelecao = "-1";

        ASPxTextBox txtDetalhe = grid.FindRowCellTemplateControl(int.Parse(linhaAfetada), (GridViewDataColumn)grid.Columns["DescricaoOpcaoCriterioSelecao"], "txtDetalhe") as ASPxTextBox;
        ASPxTextBox txtValor = grid.FindRowCellTemplateControl(int.Parse(linhaAfetada), (GridViewDataColumn)grid.Columns["Valor"], "txtValor") as ASPxTextBox;
        string Detalhe = "";
        Valor = "";

        // busca os valores correspondente a opção selecionada
        string comandoSQL = string.Format(
            @"SELECT ocs.[DescricaoOpcaoCriterioSelecao], m2.[PesoObjetoMatriz] AS [ValorOpcaoCriterioSelecao] 
	            FROM 
		            {0}.{1}.[Projeto]				        AS [p] 
		
			    INNER JOIN {0}.{1}.[OpcaoCriterioSelecao]	AS [ocs]	ON 
			        (   ocs.[CodigoCriterioSelecao]= {2} 
				    AND ocs.[CodigoOpcaoCriterioSelecao]= '{3}')
				
			INNER JOIN {0}.{1}.[MatrizObjetoCriterio]	    AS [m2]		ON 
				(			m2.[CodigoCategoria]			= p.[CodigoCategoria] 
					AND m2.IniciaisTipoObjetoCriterioPai	= 'CR' 
					AND m2.[CodigoObjetoCriterioPai]		= ocs.[CodigoCriterioSelecao] 
					AND m2.CodigoObjetoCriterio				= ocs.[CodigoCriterioSelecao]*1000+ASCII(ocs.[CodigoOpcaoCriterioSelecao]) ) 
	WHERE 
		(p.[CodigoProjeto] = {4}) ", cDados.getDbName(), cDados.getDbOwner()
                                   ,  codigoCriterioSelecao, CodigoOpcaoCriterioSelecao, codigoProjetoSelecionado);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Detalhe = dt.Rows[0]["DescricaoOpcaoCriterioSelecao"].ToString();
                Valor = dt.Rows[0]["ValorOpcaoCriterioSelecao"].ToString();
            }
        }

        if (txtDetalhe != null)
            txtDetalhe.Text = Detalhe;

        if (txtValor != null)
        {
            txtValor.Text = Valor;
        }

        // totaliza a grid
        decimal totalGrid = 0;

        for (int row = 0; row < grid.VisibleRowCount; row++)
        {
            if (!grid.IsGroupRow(row))
            {
                txtValor = grid.FindRowCellTemplateControl(row, (GridViewDataColumn)grid.Columns["Valor"], "txtValor") as ASPxTextBox;
                if (txtValor.Text != "")
                    totalGrid += decimal.Parse(txtValor.Text);
            }
        }
        ASPxTextBox txtTotal = grid.FindFooterRowTemplateControl("txtTotal") as ASPxTextBox;
        txtTotal.Text = string.Format("{0:P2}", totalGrid);

        verificaPassagemFluxo();
    }

    private bool Salvar(string parametro)
    {
        string[] opcoes = parametro.Split(';');

        // monta os comandos para atualizar as informações
        string comandoSQL = string.Format(
                @"
            BEGIN TRY
                BEGIN TRAN

                    DELETE FROM ProjetoCriterioSelecao 
                     WHERE CodigoProjeto = {0} 
                       AND EtapaPreenchimento = {1}
                 ", codigoProjetoSelecionado
                  , codigoEtapa);

        for (int i = 0; i < opcoes.Length - 1; i++)
        {
            string CodigoCriterioSelecao = opcoes[i].Substring(0, opcoes[i].Length - 1);
            string CodigoOpcaoCriterioSelecao = opcoes[i][opcoes[i].Length - 1].ToString();
            if (CodigoCriterioSelecao.Trim() != "")
            {
                comandoSQL += string.Format(
                    @"
                     INSERT INTO ProjetoCriterioSelecao (CodigoProjeto, CodigoCriterioSelecao, CodigoOpcaoCriterioSelecao, EtapaPreenchimento, DataGravacaoRegistro)
                         VALUES ({0}, {1}, '{2}', {3}, GETDATE())

                 ", codigoProjetoSelecionado, CodigoCriterioSelecao, CodigoOpcaoCriterioSelecao, codigoEtapa);
            }
        }

        comandoSQL += @"
                COMMIT
            END TRY
            BEGIN CATCH
                ROLLBACK
                DECLARE @ErrorMessage NVARCHAR(4000);
                DECLARE @ErrorSeverity INT;
                DECLARE @ErrorState INT;

                SELECT @ErrorMessage = 'Houve um erro ao gravar os critérios para o projeto. Favor entrar em contato com o administrador ' + 
                        'do Sistema. Mensagem original do erro:' + CHAR(13) + CHAR(13) + ERROR_MESSAGE();
                SELECT @ErrorSeverity = ERROR_SEVERITY();
                SELECT @ErrorState = ERROR_STATE();
                RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
            END CATCH";
        if (codigoEtapa != 0)
        {
            comandoSQL += string.Format(@"
            DECLARE @ou_result Int
            EXEC p_RegistraMediaCriterioSelecaoProjeto {0}, @ou_result output 

            IF (@ou_result!= 0) BEGIN
	            DECLARE @mensagem NVarchar(4000)
	            SET @mensagem = 'Ocorreu um erro ao determinar o valor final dos critérios para o projeto em questão. ' + 
		            'Favor entrar em contato com o administrador do sistema informando o código retornado pelo processamento: %i.'
	            RAISERROR(@mensagem, 16, 1, @ou_result)
            END", codigoProjetoSelecionado);
        }

        int afetados = 0;
        return cDados.execSQL(comandoSQL, ref afetados);
    }

    private void carregaDataSetOpcoes()
    {

        string comandoSql = string.Format(
                   @"  SELECT CONVERT (varchar, CodigoCriterioSelecao) + CodigoOpcaoCriterioSelecao AS codigoOpcao, 
                         CodigoOpcaoCriterioSelecao + ' - ' + DescricaoOpcaoCriterioSelecao AS descricaoOpcao 
                         , DescricaoEstendidaOpcao
                         , DescricaoOpcaoCriterioSelecao
                         , CodigoCriterioSelecao
                         , m2.[PesoObjetoMatriz] AS [Valor] 
                         , CodigoOpcaoCriterioSelecao
                    FROM {0}.{1}.OpcaoCriterioSelecao  ocs	INNER JOIN		
						 {0}.{1}.MatrizObjetoCriterio  m2 ON 
						(	m2.[CodigoCategoria]= {2} 
							AND m2.IniciaisTipoObjetoCriterioPai='CR' 
							AND m2.[CodigoObjetoCriterioPai]=ocs.[CodigoCriterioSelecao] 
							AND m2.CodigoObjetoCriterio=ocs.[CodigoCriterioSelecao]*1000+ASCII(ocs.[CodigoOpcaoCriterioSelecao]) )                     
                   ORDER BY descricaoOpcao", cDados.getDbName(), cDados.getDbOwner(), codigoCategoria);

        dsOpcoes = cDados.getDataSet(comandoSql);

    }

    private DataTable getIndicacoesCriterio(string codigoCriterioIndicacao, string codigoProjetoIndicacao)
    {

        string comandoSql = string.Format(
                      @"DECLARE @CodigoProjeto Int
                               ,@CodigoCriterioSelecao Int
      
                        set @CodigoProjeto = {2}
                        set @CodigoCriterioSelecao = {3}

                        SELECT  ocs.[CodigoOpcaoCriterioSelecao] AS [Opcao]
                              , COUNT(pcs.CodigoCriterioSelecao) AS [QtdeVotos]
                          FROM {0}.{1}.[OpcaoCriterioSelecao] AS [ocs] LEFT JOIN 
			                         {0}.{1}.[ProjetoCriterioSelecao] AS [pcs] ON (pcs.[CodigoCriterioSelecao] = ocs.[CodigoCriterioSelecao]
                                                     AND pcs.[CodigoOpcaoCriterioSelecao] = ocs.[CodigoOpcaoCriterioSelecao]
                                                     AND pcs.[CodigoProjeto] = @CodigoProjeto                   
                                                     AND pcs.[EtapaPreenchimento] != 0) INNER JOIN 
			                         {0}.{1}.[CriterioSelecao] AS [cs] ON (cs.[CodigoCriterioSelecao] = ocs.CodigoCriterioSelecao)
                         WHERE ocs.[CodigoCriterioSelecao]  = @CodigoCriterioSelecao
                           AND cs.[DataExclusao] IS NULL
                         GROUP BY ocs.[CodigoOpcaoCriterioSelecao]
                         ORDER BY ocs.[CodigoOpcaoCriterioSelecao]", cDados.getDbName(), cDados.getDbOwner(), codigoProjetoIndicacao, codigoCriterioIndicacao);

        DataSet dsIndicacoes = cDados.getDataSet(comandoSql);

        return dsIndicacoes.Tables[0];
    }

    protected void GridsCriterios_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        string nomeGrid = string.Empty;
        ASPxGridView grid = (sender as ASPxGridView);
        nomeGrid = grid.ID;

        if (e.RowType == GridViewRowType.Footer)
        {
            ASPxTextBox txtTotalCriterio = grid.FindFooterRowTemplateControl("txtTotal") as ASPxTextBox;
                        
            if (txtTotalCriterio != null)
            {
                txtTotalCriterio.ID = "txtTotal";
                txtTotalCriterio.ClientInstanceName = "txtTotal";
                txtTotalCriterio.DisplayFormatString = "{0:p2}";
                
                if (nomeGrid.Equals("gvCritPos"))
                    txtTotalCriterio.Value = totalGridCritPos;
                else
                    txtTotalCriterio.Value = totalGridCritNeg;

                txtTotalCriterio.Font.Name = "Verdana";
                txtTotalCriterio.Font.Size = new FontUnit("8pt");
            }
        }

        if (e.RowType == GridViewRowType.Data)
        {
            ASPxComboBox cbOpcao = grid.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)grid.Columns["codigoOpcao"], "cbOpcao") as ASPxComboBox;
            ASPxImage imgLegenda = grid.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)grid.Columns["codigoOpcao"], "imgLegenda") as ASPxImage;
            ASPxImage imgVotacao = grid.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)grid.Columns["codigoOpcao"], "imgVotacao") as ASPxImage;

            imgVotacao.ClientVisible = codigoEtapa.Equals(0);

            cbOpcao.ClientEnabled = !desabilitaTela;

            if (cbOpcao != null && cDados.DataSetOk(dsOpcoes))
            {
                DataRow[] drOpcoes = dsOpcoes.Tables[0].Select("CodigoCriterioSelecao = " + e.KeyValue.ToString());

                // Limpa o DropDwon
                cbOpcao.Items.Clear();
                                
                //cbOpcao.Items.Add(" ");

                string valoresCombo = "";
                string valoresDecimaisCombo = "";

                // Insere os ítens no DropDwon
                foreach (DataRow dr in drOpcoes)
                {
                    //dropDown.Items.Add(new ListItem(dr[campoTexto].ToString(), dr[campoValor].ToString()));
                    cbOpcao.Items.Add(new ListEditItem(dr["descricaoOpcao"].ToString(), dr["codigoOpcao"].ToString()));
                    valoresCombo += string.Format("{0:p2}", float.Parse(dr["Valor"].ToString())) + ";";
                    valoresDecimaisCombo += dr["Valor"].ToString() + ";";
                }

                cbOpcao.SelectedIndex = 0;

                string CodigoCriterioSelecao = e.GetValue("CodigoCriterioSelecao").ToString();
                string CodigoOpcaoCriterioSelecao = e.GetValue("CodigoOpcaoCriterioSelecao").ToString();
                string descricao = grid.GetRowValues(e.VisibleIndex, "DescricaoCriterioSelecao").ToString();

                string legenda = "";
                string eventoImg = "";

                string legendaVotacao = "<b>Critério: " + descricao + "</b><BR><BR>";
                string eventoImgVotacao = "";

                legendaVotacao += @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" ><tr style=""background-color:#EBEBEB""><td style=""padding-left:5px; width:95px; height: 22px; border-style: solid; border-color:Black; border-width: 1px"">Opção</td><td align=""right"" style=""padding-right:5px;width:150px; height: 22px; border-style: solid; border-color:Black; border-width: 1px"">Nº de Indicações</td></tr>";

                DataTable dtIndicacoes = getIndicacoesCriterio(CodigoCriterioSelecao, codigoProjetoSelecionado.ToString());
                int quantidadeTotal = 0;

                foreach (DataRow dr in drOpcoes)
                {
                    DataRow[] drIndicacoes = dtIndicacoes.Select("Opcao = '" + dr["CodigoOpcaoCriterioSelecao"] + "'");
                    
                    eventoImg += string.Format(@"
                                              if(s.GetValue() == '{0}')
                                                document.getElementById('{4}_cell{3}_2_imgLegenda_{3}').title = '{1}: {2}\nClique na imagem para visualizar a legenda completa...';", dr["codigoOpcao"]
                                                                                        , descricao + " " + dr["DescricaoOpcaoCriterioSelecao"]
                                                                                        , dr["DescricaoEstendidaOpcao"]
                                                                                        , e.VisibleIndex, nomeGrid);

                    eventoImgVotacao += string.Format(@"
                                              if(s.GetValue() == '{0}')
                                                document.getElementById('{2}_cell{1}_2_imgVotacao_{1}').title = 'Visualizar Indicações do Critério';", dr["codigoOpcao"]
                                                                                        , e.VisibleIndex, nomeGrid);

                    int quantidadeIndicacoes = 0;

                    try {
                        quantidadeIndicacoes = int.Parse(drIndicacoes[0]["QtdeVotos"].ToString());
                        quantidadeTotal += quantidadeIndicacoes;
                    }
                    catch { }

                    legenda += "<b>" + descricao + " " + dr["DescricaoOpcaoCriterioSelecao"] + ": </b><br>- " + dr["DescricaoEstendidaOpcao"] + "<br><br>";
                    legendaVotacao += @"<tr><td style=""padding-left:5px; height: 22px; border-style: solid; border-color:Black; border-width: 1px"">" + dr["CodigoOpcaoCriterioSelecao"] +
                        @"</td><td align=""right"" style=""padding-right:5px; height: 22px; border-style: solid; border-color:Black; border-width:1px"">" + quantidadeIndicacoes + "</td></tr>";
                }

                legendaVotacao += @"<tr style=""background-color:#EBEBEB""><td style=""padding-left:5px; height: 22px; border-style: solid; border-color:Black; border-width: 1px"">Total</td>";
                legendaVotacao += @"<td align=""right"" style=""padding-right:5px;height: 22px; border-style: solid; border-color:Black; border-width: 1px"">" + quantidadeTotal + "</td></tr>";
                legendaVotacao += "</table>";

                imgLegenda.ClientSideEvents.Click = "function(s, e) { document.getElementById('" + nomeGrid + "_cell" + e.VisibleIndex + @"_2_pcLegenda_"+ e.VisibleIndex + @"_PWC-1').innerHTML = '<div style=""width:100%; height: 180px; overflow:auto"">" + legenda + "</div>' }";
                imgVotacao.ClientSideEvents.Click = "function(s, e) { document.getElementById('" + nomeGrid + "_cell" + e.VisibleIndex + @"_2_pcVotacao_" + e.VisibleIndex + @"_PWC-1').innerHTML = '<div style=""width:100%; height: 180px; overflow:auto"">" + legendaVotacao + "</div>' }";
                cbOpcao.Value = CodigoCriterioSelecao + CodigoOpcaoCriterioSelecao;


                cbOpcao.ClientSideEvents.SelectedIndexChanged = @"function(s, e) { processaOpcaoEscolhida(s, e, '" + nomeGrid + "', '" + valoresCombo + "', '" + e.VisibleIndex + "','" + valoresDecimaisCombo + "'); document.getElementById('" + nomeGrid + "_cell" + e.VisibleIndex + "_2_imgLegenda_" + e.VisibleIndex + "').title = ''; " + eventoImg + "}";
                cbOpcao.ClientSideEvents.Init = "function(s, e) { document.getElementById('" + nomeGrid + "_cell" + e.VisibleIndex + "_2_imgLegenda_" + e.VisibleIndex + "').title = ''; " + eventoImg + "}";

                if (cbOpcao.SelectedIndex == -1)
                    cbOpcao.Value = "";

                ASPxTextBox txtDetalhe = grid.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)grid.Columns["DescricaoOpcaoCriterioSelecao"], "txtDetalhe") as ASPxTextBox;
                if (txtDetalhe != null)
                    txtDetalhe.Text = e.GetValue("DescricaoOpcaoCriterioSelecao").ToString();

                ASPxTextBox txtValor = grid.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)grid.Columns["Valor"], "txtValor") as ASPxTextBox;

                if (txtValor != null)
                {
                    txtValor.Font.Name = "Verdana";
                    txtValor.Font.Size = new FontUnit("8pt");
                    txtValor.ID = "valor_" + e.VisibleIndex;
                    txtValor.ClientInstanceName = "valor_" + e.VisibleIndex; 

                    txtValor.Text = e.GetValue("Valor").ToString();
                    if (e.GetValue("Valor").ToString() != "")
                    {
                        if (nomeGrid.Equals("gvCritPos"))
                            totalGridCritPos += decimal.Parse(e.GetValue("Valor").ToString());
                        else
                            totalGridCritNeg += decimal.Parse(e.GetValue("Valor").ToString());
                    }
                }  
            }
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackSalvar.JSProperties["cp_msg"] = "";
        callbackSalvar.JSProperties["cp_erro"] = "";
        // se o callback for para gravar as informações, processa e retorna.
        if (e.Parameter.Substring(0, 6) == "SALVAR")
        {
            bool resultado = Salvar(e.Parameter.Substring(6));

            if (resultado)
                callbackSalvar.JSProperties["cp_msg"] = "Dados salvos com sucesso!";
            else
                callbackSalvar.JSProperties["cp_erro"] = "Erro ao salvar os dados!";

            verificaPassagemFluxo();
        }
    }

    private void carregaComboCategoria()
    {
        string where = string.Format(@" AND CodigoEntidade = {0}", codigoEntidadeUsuarioResponsavel);


        //Categorias
        DataSet ds = cDados.getCategoria(where);
        if (cDados.DataSetOk(ds))
        {
            ddlCategoria.ValueField = "CodigoCategoria";
            ddlCategoria.TextField = "DescricaoCategoria";

            ddlCategoria.DataSource = ds.Tables[0];
            ddlCategoria.DataBind();
        }
    }

    protected void callbackCategoria_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string comandoSQL = string.Format(
                @"DELETE FROM ProjetoCriterioSelecao 
                     WHERE CodigoProjeto = {0} 
                       AND EtapaPreenchimento = {1}
                 ", codigoProjetoSelecionado
                  , codigoEtapa);

        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);

        cDados.atualizaCategoriaProjeto(codigoProjetoSelecionado, codigoCategoria);

        verificaPassagemFluxo();
    }
}
