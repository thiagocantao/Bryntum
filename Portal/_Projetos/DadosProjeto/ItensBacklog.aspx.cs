using System;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using DevExpress.Web.ASPxTreeList;

public partial class _Projetos_DadosProjeto_ItensBacklog : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    public int alturaFrameAnexos = 372;
    public bool podeManterItensDoProjeto = false;

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            podeManterItensDoProjeto = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "PR", "PR_IteBkl");
            if(!podeManterItensDoProjeto)
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

        headerOnTela();
        this.TH(this.TS("itensbacklog"));
        if (Request.QueryString["IDProjeto"] != null)
        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }
        tlDados.JSProperties["cp_CodigoProjeto"] = idProjeto.ToString();

        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(idProjeto, "-1", 1, true, false, false, percentualConcluido, data);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);        
        SetConnectionStrings();
        cDados.aplicaEstiloVisual(Page);
        carregaTlDados();
        
    }

    private void SetConnectionStrings()
    {
        sdsRecursoCorporativo.ConnectionString = cDados.ConnectionString;
    }



    #region GRID

    private void carregaTlDados()
    {
        DataSet ds = getItensDoBackLog(idProjeto, "");

        //if (cDados.DataSetOk(ds))
        //{
        tlDados.DataSource = ds;
        tlDados.DataBind();
        //}
    }

    public DataSet getItensDoBackLog(int codigoProjeto, string where)
    {
        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @tabelaItens AS TABLE(
            CodigoItem  int,
            textoTag  varchar(130)
            );

              DECLARE @textoTagItem AS VARCHAR(max)

              DECLARE crsWork CURSOR LOCAL FAST_FORWARD FOR   
              SELECT TextoTag, CodigoItem 
                FROM Agil_TagItemBackLog 
               WHERE CodigoItem in (SELECT codigoitem 
	                                   FROM Agil_ItemBacklog 
		                              WHERE CodigoProjeto = {2})

	            DECLARE @l_TextosTAG as varchar(500)
	            DECLARE @l_CodigoItem as int
	
	            OPEN crsWork    
    
	            FETCH NEXT FROM crsWork INTO @l_TextosTAG, @l_CodigoItem
	            WHILE (@@FETCH_STATUS = 0 )
                BEGIN
	               IF(EXISTS(SELECT 1 
	                           FROM @tabelaItens 
				               WHERE CodigoItem = @l_CodigoItem) )
	               BEGIN
		                UPDATE @tabelaItens
			               SET TextoTag = ISNULL(textoTag, '') + '|' + @l_TextosTAG
		                 WHERE CodigoItem = @l_CodigoItem
	               END
	               ELSE
	               BEGIN
	                   INSERT INTO  @tabelaItens(CodigoItem, textoTag) values(@l_CodigoItem, @l_TextosTAG)
	               END
	               FETCH NEXT FROM crsWork INTO @l_TextosTAG, @l_CodigoItem
	            END
    	        --SELECT * FROM @tabelaItens    
        SELECT ib.[CodigoItem]
              ,ib.[CodigoProjeto]
              ,ib.[CodigoItemSuperior]
              ,ib.[TituloItem]
              ,ibSUP.TituloItem as TituloItemSuperior 
              ,ib.[DetalheItem]
              ,ib.[CodigoTipoStatusItem]
              ,ts.DescricaoTipoStatusItem
              ,ib.[CodigoTipoClassificacaoItem]
              ,tc.DescricaoTipoClassificacaoItem
              ,ib.[CodigoUsuarioInclusao]
              ,ib.[DataInclusao]
              ,ib.[CodigoUsuarioUltimaAlteracao]
              ,ib.[DataUltimaAlteracao]
              ,ib.[CodigoUsuarioExclusao]
              ,ib.[PercentualConcluido]
              ,ib.[CodigoIteracao]
              ,ib.[Importancia]
              ,ib.[Complexidade]
              ,(SELECT CASE ib.[Complexidade]
                 WHEN 0 THEN 'Baixa'
                 WHEN 1 THEN 'Média'
                 WHEN 2 THEN 'Alta'
                 WHEN 3 THEN 'Muito Alta'
               END) AS DescricaoComplexidade
              ,ib.[EsforcoPrevisto]
              ,ib.[IndicaItemNaoPlanejado]
              ,ib.[IndicaQuestao]
              ,ib.[IndicaBloqueioItem]
              ,ib.[CodigoWorkflow]
              ,ib.[CodigoInstanciaWF]
              ,ib.[CodigoCronogramaProjetoReferencia]
              ,ib.[CodigoTarefaReferencia]
              ,p.CodigoRecursoCorporativo as CodigoCliente
              ,p.NomeRecursoCorporativo as NomeCliente
              ,ttf.CodigoTipoTarefaTimeSheet
              ,ttf.DescricaoTipoTarefaTimeSheet
              ,ib.ReceitaPrevista
              ,(SELECT p.NomeProjeto  
                  FROM {0}.{1}.Agil_Iteracao AS i INNER JOIN
                       {0}.{1}.Projeto AS p ON (i.CodigoProjetoIteracao = p.CodigoProjeto) INNER JOIN
                       {0}.{1}.Agil_ItemBacklog As ai ON  (ai.CodigoIteracao = i.CodigoIteracao)
                 WHERE ai.CodigoItem = ib.[CodigoItem]) as Sprint
             ,ib.DataAlvo
             ,(SELECT TextoTag FROM @tabelaItens WHERE CodigoItem = ib.CodigoItem) as TagItem
             ,(SELECT TOP 1 ibs.CodigoItem 
                                      FROM Agil_ItemBacklog ibs 
                                      WHERE ibs.CodigoItemSuperior = ib.CodigoItem)  as IndicaSeItemBacklogTemTarefaAssociada

         FROM {0}.{1}.[Agil_ItemBacklog] ib
		 left join {0}.{1}.[Agil_ItemBacklog] ibSUP on (ibSUP.CodigoItem = ib.CodigoItemSuperior)
         LEFT JOIN {0}.{1}.Agil_TipoStatusItemBacklog ts on (ib.CodigoTipoStatusItem = ts.CodigoTipoStatusItem)
         LEFT JOIN {0}.{1}.Agil_TipoClassificacaoItemBacklog tc on (ib.CodigoTipoClassificacaoItem = tc.CodigoTipoClassificacaoItem)
         LEFT JOIN {0}.{1}.RecursoCorporativo p on (p.CodigoRecursoCorporativo = ib.CodigoPessoa)
         LEFT JOIN {0}.{1}.TipoTarefaTimeSheet AS ttf on (ttf.CodigoTipoTarefaTimeSheet = ib.CodigoTipoTarefaTimesheet) 

		 WHERE IB.CodigoProjeto IN(SELECT {2} as CodigoProjeto 
                             UNION SELECT CodigoProjetoFilho  as CodigoProjeto
		                             FROM LinkProjeto 
									 WHERE CodigoProjetoPai = {2})
									 AND  ISNULL(ib.IndicaTarefa, 'N') = 'N'
        ORDER BY ib.[Importancia] desc
        END
            {3}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, where);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

   

    #endregion

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ItensBacklog.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ItensBacklog"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;

        //tlDados.Settings.VerticalScrollableHeight = alturaPrincipal - 320;
        alturaFrameAnexos = alturaPrincipal - 270;
        tlDados.JSProperties["cp_AlturaFrameAnexos"] = alturaFrameAnexos;
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    // retorna a primary key da tabela.
    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (tlDados.FocusedRowIndex >= 0)
            return tlDados.GetRowValues(tlDados.FocusedRowIndex, tlDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void callbackTela_Callback(object source, CallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        string[] parametros = e.Parameter.Split('|');
        
        ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "";
        ((ASPxCallback)source).JSProperties["cp_Erro"] = "";
        ((ASPxCallback)source).JSProperties["cp_ItemBackLogTemTarefa"] = "N";


        if (parametros[0] == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro(parametros[1]);

            if (mensagemErro_Persistencia == "")
            {
                ((ASPxCallback)source).JSProperties["cp_Sucesso"] = Resources.traducao.ItensBacklog_item_de_backlog_exclu_do_com_sucesso_;
            }
        }

         ((ASPxCallback)source).JSProperties["cp_Erro"] = mensagemErro_Persistencia;
    }

    private string persisteExclusaoRegistro(string codigo)
    {   
        string msg = "";
        bool retorno = cDados.excluiItensDoBackLog(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, int.Parse(codigo), ref msg);
        return msg;
    }
    
    #endregion


    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "incluiItemBacklog(); TipoOperacao = 'Incluir';", true, true, false, "PR_IteBkl", "Itens do Backlog", this);

     

        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                this.Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            this.Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            this.Response.End();
        }

        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");

        bool exportaOLAPTodosFormatos = false;

        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
        {
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
        }

        DevExpress.Web.MenuItem btnExportar = ((ASPxMenu)sender).Items.FindByName("btnExportar");

        btnExportar.ClientVisible = true;

        if (!exportaOLAPTodosFormatos)
        {
            btnExportar.Items.Clear();
            btnExportar.Image.Url = "~/imagens/botoes/btnExcel.png";
            btnExportar.ToolTip = "Exportar para XLS";
        }

        

   

        DevExpress.Web.MenuItem btnIncluir = ((ASPxMenu)sender).Items.FindByName("btnIncluir");
        btnIncluir.ClientEnabled = podeIncluir;

        btnIncluir.ClientVisible = true;

        if (podeIncluir == false)
            btnIncluir.Image.Url = "~/imagens/botoes/incluirRegDes.png";

      

       

        string clickBotaoExportar = "";

        if (exportaOLAPTodosFormatos)
            clickBotaoExportar = @"
            else if(e.item.name == 'btnExportar')
	        {
                e.processOnServer = false;		                                        
	        }";

        ((ASPxMenu)sender).ClientSideEvents.ItemClick =
        @"function(s, e){ 

            e.processOnServer = false;

            if(e.item.name == 'btnIncluir')
            {
                incluiItemBacklog(); TipoOperacao = 'Incluir';
            }" + clickBotaoExportar + @"	
	        else if(e.item.name != 'btnLayout')
	        {
                e.processOnServer = true;		                                        
	        }	

        }";
        
        DevExpress.Web.MenuItem btnLayout = ((ASPxMenu)sender).Items.FindByName("btnLayout");

        btnLayout.ClientVisible = false;

        if (false && !this.IsPostBack)
        {
            DataSet ds = cDados.getDataSet("SELECT 1 FROM Lista WHERE CodigoEntidade = " + cDados.getInfoSistema("CodigoEntidade") + " AND IniciaisListaControladaSistema = 'PR_IteBkl'");

            if (ds.Tables[0].Rows.Count == 0)
            {
                int regAf = 0;

               cDados.execSQL(cDados.constroiInsertLayoutColunas((((ASPxMenu)sender).Parent as GridViewHeaderTemplateContainer).Grid, "PR_IteBkl", "Itens do Backlog"), ref regAf);
            }

           cDados.InitData((((ASPxMenu)sender).Parent as GridViewHeaderTemplateContainer).Grid, "PR_IteBkl");
        }
    }

    protected void menu_ItemClick1(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.exportaGridView(ASPxTreeListExporter1, "XLS");
    }
    
    protected void tlDados_ProcessDragNode(object sender, TreeListNodeDragEventArgs e)
    {
        ((ASPxTreeList)(sender)).JSProperties["cpErroAoArrastar"] = "";
        if (e.Node.HasChildren == true)
        {
            e.Handled = false;
            e.Cancel = true;
            ((ASPxTreeList)(sender)).JSProperties["cpErroAoArrastar"] = Resources.traducao.ItensBacklog_itens_de_backlog_que_tem_itens_inferiores_na_hierarquia_n_o_podem_ser_arrastados_;
            return;
        }
       if(e.Node.GetValue("Sprint") != null && e.Node.GetValue("Sprint").ToString() != "")
        {
            e.Handled = false;
            e.Cancel = true;
            ((ASPxTreeList)(sender)).JSProperties["cpErroAoArrastar"] = Resources.traducao.ItensBacklog__tens_de_backlog_j__associados_a_um_sprint_n_o_podem_ser_arrastados_;
            return;
        }
       if(e.NewParentNode.GetValue("Sprint") != null && e.NewParentNode.GetValue("Sprint").ToString() != "")
        {
            e.Handled = false;
            e.Cancel = true;
            ((ASPxTreeList)(sender)).JSProperties["cpErroAoArrastar"] = "Operação não permitida, pois itens de backlog (pai) que já tem sprint associada e não podem receber itens de backlog filhos.";
            return;
        }
        
        //string CodigoItem_AntigoPaiNoArrastado = e.Node.ParentNode.GetValue("CodigoItem").ToString();//pai do no do item que ta sendo arrastado
        string CodigoItem_NoArrastado = e.Node.GetValue("CodigoItem").ToString();//item que ta sendo arrastado
        string CodigoItem_NoDestino = e.NewParentNode.GetValue("CodigoItem") == null ? "NULL" : e.NewParentNode.GetValue("CodigoItem").ToString();//item em que aponta na hora de soltar
        int conta = 0;
        string updateNos = string.Format(@"
        BEGIN

         declare @CodigoItem_AntigoPaiNoArrastado as int
         declare @CodigoItem_NoDestino as int
         declare @CodigoItem_NoArrastado as int         
         
         set @CodigoItem_NoArrastado = {0}
         set @CodigoItem_NoDestino = {1}

          UPDATE Agil_ItemBacklog 
             SET CodigoItemSuperior = @CodigoItem_NoDestino
          WHERE CodigoItem = @CodigoItem_NoArrastado

        END", CodigoItem_NoArrastado, CodigoItem_NoDestino);

        bool x = cDados.execSQL(updateNos, ref conta);

        carregaTlDados();
        e.Handled = true;
    }

    protected void tlDados_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
    {
        carregaTlDados();
    }
     
    protected void tlDados_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        //if(e.RowKind == TreeListRowKind.Data)
        //{
            //if (e.GetValue("TituloItem") != null)
            //{

                //TreeListNode tln = tlDados. FindNodeByKeyValue(e.GetValue("CodigoItem").ToString());
                //if (tln != null && tln.HasChildren)
                //{
                //    e.Row.BackColor = Color.FromName("#EAEAEA");//http://www.color-hex.com/color/eaeaea
                //    e.Row.ForeColor = Color.Black;
                   
                //}
            //}
        //}
    }

    protected void tlDados_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e)
    {
      
        //if (e.ButtonType == TreeListCommandColumnButtonType.Custom)
        //{
        //    if (e.GetValue("TituloItem") != null)
        //    {
        //       if( e.CustomButtonIndex == 1)
        //        {
        //            TreeListNode tln = tlDados.FindNodeByKeyValue(e.GetValue("CodigoItem").ToString());

        //            string sprintAssociadoAoItem = (e.GetValue("Sprint") != null ? e.GetValue("Sprint").ToString() : "");

        //            if (tln != null && tln.HasChildren)
        //            {
        //                e.Enabled = DevExpress.Utils.DefaultBoolean.False;
        //                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        //            }
        //            else if (!string.IsNullOrEmpty(sprintAssociadoAoItem))
        //            {
        //                e.Enabled = DevExpress.Utils.DefaultBoolean.False;
        //                e.Visible = DevExpress.Utils.DefaultBoolean.False;
        //            }
        //        }
        //    }
        //}
    }

     
    protected void tlDados_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        if(e.Column.FieldName == "TagItem")
        {
            if(e.CellValue != null && e.CellValue.ToString() != "")
            {
                e.Cell.Text = e.CellValue.ToString().Replace("|", ", ");
            }
        }
    }

  

    protected void tlDados_FocusedNodeChanged(object sender, EventArgs e)
    {
        //if(((ASPxTreeList)sender).FocusedNode != null)
        //{
        //    string tituloItem = ((ASPxTreeList)sender).FocusedNode.GetValue("TituloItem").ToString();
        //    string codigoItem = ((ASPxTreeList)sender).FocusedNode.GetValue("CodigoItem").ToString();
        //    ((ASPxTreeList)sender).JSProperties["cpNoSelecionado"] = tituloItem;
        //    ((ASPxTreeList)sender).JSProperties["cpCodigoItemSelecionado"] = codigoItem;

        //}
    }


    protected void callbackPopupItemBacklog_Callback(object source, CallbackEventArgs e)
    {
        var codigoItem = "-1";
        var acao = "";
        var codigoProjeto = "-1";
        var descricaoItemSuperior = "Novo";
        var codigoItemSuperior = "-1";
        var tituloItem = "Adicionar Item de Backlog";

        var arrayParametrosRecebidos = e.Parameter.Split('|');

        codigoItem = arrayParametrosRecebidos[0] == "undefined" ? "-1" : arrayParametrosRecebidos[0];
        acao = arrayParametrosRecebidos[1];
        codigoProjeto = arrayParametrosRecebidos[2];

        string comandoSQL = string.Format(@"
        SELECT top 1 ib.[CodigoItem], ib.[TituloItem], ibSup.[CodigoItem] as CodigoItemSuperior, ibSup.TituloItem as TituloItemSuperior
        FROM [dbo].[Agil_ItemBacklog] ib
        LEFT JOIN [Agil_ItemBacklog] ibSup on (ibSup.CodigoItem = ib.CodigoItemSuperior)
        WHERE ib.CodigoItem = {0}", codigoItem);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            descricaoItemSuperior = "Editar Item de Backlog";//ds.Tables[0].Rows[0]["TituloItemSuperior"].ToString();
            codigoItemSuperior = ds.Tables[0].Rows[0]["CodigoItemSuperior"].ToString();
            codigoItem = ds.Tables[0].Rows[0]["CodigoItem"].ToString();
            tituloItem = ds.Tables[0].Rows[0]["TituloItem"].ToString();
        }
        
        ((ASPxCallback)source).JSProperties["cpCodigoItem"] = codigoItem;
        ((ASPxCallback)source).JSProperties["cpTituloItem"] = tituloItem;
        ((ASPxCallback)source).JSProperties["cpAcao"] = acao;
        ((ASPxCallback)source).JSProperties["cpCodigoProjeto"] = codigoProjeto;
        ((ASPxCallback)source).JSProperties["cpDescricaoItemSuperior"] = descricaoItemSuperior;
        ((ASPxCallback)source).JSProperties["cpCodigoItemSuperior"] = codigoItemSuperior;


    }

    protected void ASPxTreeListExporter1_RenderBrick1(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.TextValue != null && e.TextValue.ToString() != "")
        {
            e.Text = e.TextValue.ToString().Replace("|", ", ");
            e.TextValue = e.Text;
        }
    }

    protected void callbackVerificaSeTemTarefasAssociadas_Callback(object source, CallbackEventArgs e)
    {
        /*		FROM
				[dbo].[Agil_TagItemBackLog]		AS [ti]
				INNER JOIN [dbo].[Agil_ItemBacklog]		AS [ib]		ON
					(ib.[CodigoItem] = ti.[CodigoItem]
						AND ib.[CodigoItemSuperior] = 1038 );*/

        ((ASPxCallback)source).JSProperties["cp_ItemBacklogTemTarefaAssociada"] = "N";
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        string chave = e.Parameter;
        string comandoSQL = string.Format(@"SELECT TOP 1 CodigoItem FROM Agil_ItemBacklog WHERE CodigoItemSuperior = {0}", chave);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ((ASPxCallback)source).JSProperties["cp_ItemBacklogTemTarefaAssociada"] = "S";
        }
    }
}

