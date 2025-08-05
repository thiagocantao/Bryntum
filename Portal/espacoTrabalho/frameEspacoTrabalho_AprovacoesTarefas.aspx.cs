//Revisado
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;

public partial class espacoTrabalho_frameEspacoTrabalho_AprovacoesTarefas : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;
    int idUsuarioLogado;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

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

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/aprovacaoTarefas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/custom.css"" rel=""stylesheet"" />"));

        btnPublicar.ClientEnabled = false;
        ASPxButton2.ClientEnabled = true;
        ddlAcao.ClientEnabled = true;

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        carregaGrid();
        
        cDados.aplicaEstiloVisual(Page);
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Aprovações de Tarefas", "APROV", "ENT", -1, Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_adicionar_aos_favoritos);
        }

        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);
        tbBotoes.Style.Add("padding", "3px");
        tbBotoes.Style.Add("border-collapse", "collapse");
        tbBotoes.Style.Add("border-bottom", "none");

        gvOutrosCustosEmAprovacao.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvOutrosCustosEmAprovacao.Settings.ShowFilterRow = false;
        gvOutrosCustosEmAprovacao.SettingsBehavior.AllowSort = false;
        gvOutrosCustosEmAprovacao.SettingsBehavior.AllowAutoFilter = false;
        gvOutrosCustosEmAprovacao.SettingsBehavior.AllowHeaderFilter = false;
        gvOutrosCustosEmAprovacao.SettingsBehavior.AllowGroup = false;
        gvOutrosCustosEmAprovacao.SettingsBehavior.AllowSelectSingleRowOnly = true;
        gvOutrosCustosEmAprovacao.SettingsBehavior.AllowFocusedRow = true;
        gvOutrosCustosEmAprovacao.SettingsBehavior.AllowFixedGroups = true;
        gvOutrosCustosEmAprovacao.Styles.Cell.Font.Name = "Verdana";
        gvOutrosCustosEmAprovacao.Styles.Cell.Font.Size = new FontUnit("8pt");
        //gvOutrosCustosEmAprovacao.Styles.Header.Border.BorderWidth = new Unit("2px");
        gvOutrosCustosEmAprovacao.Styles.Header.Border.BorderStyle = BorderStyle.Solid;
        gvOutrosCustosEmAprovacao.Styles.Header.Border.BorderColor = Color.Black;

        //gvOutrosCustosEmAprovacao.Paddings.PaddingLeft = new Unit("2px");





        this.TH(this.TS("frameEspacoTrabalho", "frameEspacoTrabalho_AprovacoesTarefas", "aprovacaoTarefas"));
    }

    private void carregaGrid()
    {
        DataSet ds = cDados.getTimeSheetAprovacao(idUsuarioLogado, codigoEntidade);

        //gvDados.Columns.Clear();

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ds.Tables[0].DefaultView.Sort = "StatusAprovacao";
            gvDados.DataSource = ds;
            gvDados.DataBind();

            if (!IsPostBack)
            {
                defineConfiguracoesGrid();
                btnPublicar.ClientEnabled = VerificaSeExistemInformacoesASeremPublicadas();
            }
        }
        else
        {
            gvDados.Columns.Clear();
            btnPublicar.ClientEnabled = false;
            ASPxButton2.ClientEnabled = false;
            ddlAcao.ClientEnabled = false;
            gvDados.Dispose();
            gvDados.DataBind();
        }
    }

    private bool VerificaSeExistemInformacoesASeremPublicadas()
    {
        for (int count = 0; count < gvDados.VisibleRowCount; count++)
        {
            var statusAprovacao = gvDados.GetRowValues(count, "StatusAprovacao") as string;
            if (statusAprovacao == "EA" || statusAprovacao == "ER")
                return true;
        }
        return false;
    }

    private void defineConfiguracoesGrid()
    {
        //<SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>
        gvDados.SettingsBehavior.AllowDragDrop = false;
        gvDados.SettingsBehavior.AllowSort = false;
        gvDados.SettingsBehavior.AllowGroup = false;
        if (gvDados.Columns.Count > 0)
        {
            GridViewCommandColumn cmd = new GridViewCommandColumn(" ");

            cmd.ShowSelectCheckbox = true;
            cmd.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.AllPages;

            GridViewCommandColumnCustomButton botao = new GridViewCommandColumnCustomButton();
            GridViewCommandColumnCustomButton botaoComentarios = new GridViewCommandColumnCustomButton();
            GridViewCommandColumnCustomButton botaoAnexos = new GridViewCommandColumnCustomButton();

            botao.ID = "btnStatus";
            botao.Image.Url = "~/imagens/botoes/tarefasPA.png";
            botao.Text = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_aguardando_aprova__o;

            botaoComentarios.ID = "btnComentarios";
            botaoComentarios.Image.Url = "~/imagens/botoes/comentarios.PNG";
            botaoComentarios.Text = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_visualizar_coment_rios;

            botaoAnexos.ID = "btnAnexos";
            botaoAnexos.Image.Url = "~/imagens/anexar.png";
            botaoAnexos.Text = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_visualizar_anexos;
            cmd.CustomButtons.Add(botao);
            cmd.CustomButtons.Add(botaoComentarios);
            cmd.CustomButtons.Add(botaoAnexos);
            cmd.ButtonRenderMode = GridCommandButtonRenderMode.Image;
            cmd.Width = 130;

            cmd.VisibleIndex = 0;

            //cmd.Caption = @"<input id=""Checkbox1"" onclick=""selecionaTodos(this.checked);"" type=""checkbox"" />";

            cmd.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            gvDados.Columns.Insert(0, cmd);

            gvDados.Columns["CodigoAtribuicao"].Visible = false;
            gvDados.Columns["StatusAprovacao"].Visible = false;

            gvDados.Columns["NomeRecurso"].Width = 200;
            gvDados.Columns["NomeProjeto"].Width = 220;
            gvDados.Columns["NomeTarefa"].Width = 230;
            gvDados.Columns["TrabalhoRestante"].Width = 130;
            gvDados.Columns["TrabalhoRealTotal"].Width = 110;
            gvDados.Columns["PercConcluido"].Width = 90;
            gvDados.Columns["InicioReal"].Width = 100;
            gvDados.Columns["TerminoReal"].Width = 100;
            gvDados.Columns["CodigoAtribuicao"].Width = 150;
            gvDados.Columns["StatusAprovacao"].Width = 150;

            gvDados.Columns["NomeRecurso"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_recurso;
            gvDados.Columns["NomeProjeto"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_projeto;
            gvDados.Columns["NomeTarefa"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_tarefa;
            gvDados.Columns["TrabalhoRestante"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_trab__restante_h_;
            gvDados.Columns["TrabalhoRealTotal"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_trab__real_h_;
            gvDados.Columns["PercConcluido"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_conclu_do;
            gvDados.Columns["InicioReal"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_in_cio_real;
            gvDados.Columns["TerminoReal"].Caption = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_t_rmino_real;

            ((GridViewDataTextColumn)gvDados.Columns["NomeRecurso"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)gvDados.Columns["NomeProjeto"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;

            ((GridViewDataTextColumn)gvDados.Columns["NomeRecurso"]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            ((GridViewDataTextColumn)gvDados.Columns["NomeProjeto"]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;

            ((GridViewDataTextColumn)gvDados.Columns["NomeTarefa"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)gvDados.Columns["TrabalhoRestante"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["TrabalhoRealTotal"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["PercConcluido"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataDateColumn)gvDados.Columns["InicioReal"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataDateColumn)gvDados.Columns["TerminoReal"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

            ((GridViewDataTextColumn)gvDados.Columns["PercConcluido"]).PropertiesTextEdit.DisplayFormatString = "{0:n0}%";
            ((GridViewDataDateColumn)gvDados.Columns["InicioReal"]).PropertiesDateEdit.DisplayFormatString = "{0:dd/MM/yyyy}";
            ((GridViewDataDateColumn)gvDados.Columns["TerminoReal"]).PropertiesDateEdit.DisplayFormatString = "{0:dd/MM/yyyy}";

            gvDados.Columns["TrabalhoRestante"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TrabalhoRealTotal"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["PercConcluido"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["InicioReal"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TerminoReal"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            gvDados.Columns["TrabalhoRestante"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TrabalhoRealTotal"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["PercConcluido"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["InicioReal"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TerminoReal"].CellStyle.HorizontalAlign = HorizontalAlign.Center;

            ordenaColunasDaGrid();

            for (int i = 11; i < gvDados.Columns.Count; i++)
            {
                gvDados.Columns[i].Width = 110;
                gvDados.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                ((GridViewDataTextColumn)gvDados.Columns[i]).PropertiesTextEdit.DisplayFormatString = "{0:n0}";
                ((GridViewDataTextColumn)gvDados.Columns[i]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                ((GridViewDataTextColumn)gvDados.Columns[i]).Visible = false;
            }
        }
    }

    private void ordenaColunasDaGrid()
    {
        gvDados.Columns["NomeRecurso"].VisibleIndex = 0;
        gvDados.Columns["NomeTarefa"].VisibleIndex = 1;
        gvDados.Columns["NomeProjeto"].VisibleIndex = 2;
        gvDados.Columns["TrabalhoRealTotal"].VisibleIndex = 3;
        gvDados.Columns["TrabalhoRestante"].VisibleIndex = 4;

        DataSet ds1 = cDados.getParametrosSistema(codigoEntidade, "usaCustosAdicionaisTaskboardUsuario");
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]) &&
            ds1.Tables[0].Rows[0]["usaCustosAdicionaisTaskboardUsuario"].ToString().ToUpper().Trim() == "S")
        {


            GridViewDataTextColumn colunaOutrosCustos = new GridViewDataTextColumn();
            colunaOutrosCustos.Name = "colunaOutrosCustos";
            colunaOutrosCustos.FieldName = "ValorApontamentoOutrosCustos";
            colunaOutrosCustos.Caption = "Outros Custos";
            colunaOutrosCustos.CellStyle.HorizontalAlign = HorizontalAlign.Right;
            colunaOutrosCustos.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;

            colunaOutrosCustos.Width = new Unit("200px");
            gvDados.Columns.Insert(5, colunaOutrosCustos);
            gvDados.Columns["colunaOutrosCustos"].VisibleIndex = 5;

            gvDados.Columns["PercConcluido"].VisibleIndex = 6;
            gvDados.Columns["InicioReal"].VisibleIndex = 7;
            gvDados.Columns["TerminoReal"].VisibleIndex = 8;
        }
        else
        {
            gvDados.Columns["PercConcluido"].VisibleIndex = 5;
            gvDados.Columns["InicioReal"].VisibleIndex = 6;
            gvDados.Columns["TerminoReal"].VisibleIndex = 7;
        }



    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";
        callBack.JSProperties["cp_MSG"] = "";
        bool retorno;
        string msgErro = "";

        if (e.Parameter.ToString() == "A")
        {
            string status = ddlAcao.Value.ToString();            

            int[] codigosAtribuicoes = new int[gvDados.Selection.Count];

            for (int i = 0; i < gvDados.Selection.Count; i++)
            {
                codigosAtribuicoes[i] = int.Parse(gvDados.GetSelectedFieldValues("CodigoAtribuicao")[i].ToString());
            }

            if (gvDados.Selection.Count > 0)
            {
                retorno = cDados.atualizaStatusTarefas(codigosAtribuicoes, status, ref msgErro);
                if(retorno)
                    callBack.JSProperties["cp_OK"] = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_status_alterado_com_sucesso_;
                else
                    callBack.JSProperties["cp_Erro"] = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_erro_ao_alterar_status_;
            }
            else
            {
                callBack.JSProperties["cp_MSG"] = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_nenhuma_tarefa_foi_selecionada_;
            }


        }
        else
        {
            if (e.Parameter.ToString() == "P")
            {
                retorno = cDados.publicaAprovacaoTarefasEntidade(idUsuarioLogado, codigoEntidade, ref msgErro);

                if (retorno)
                    callBack.JSProperties["cp_OK"] = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_tarefas_publicadas_com_sucesso_;
                else
                    callBack.JSProperties["cp_Erro"] = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_erro_ao_publicar_tarefas_;
            }
        }

        carregaGrid();
    }
        
    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnStatus")
        {
            e.Enabled = false;

            if (gvDados.GetRowValues(e.VisibleIndex, "StatusAprovacao") + "" == "EA")
            {
                e.Image.Url = "~/imagens/botoes/tarefasEA.PNG";
                e.Text = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_em_aprova__o_e_aguardando_publica__o;
            }
            else if (gvDados.GetRowValues(e.VisibleIndex, "StatusAprovacao") + "" == "ER")
            {
                e.Image.Url = "~/imagens/botoes/tarefasER_pequeno.PNG";
                e.Text = Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_em_reprova__o_e_aguardando_publica__o;
            }
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {        
        gvDados.Selection.UnselectAll();
        gvDados.Columns.Clear();
        gvDados.AutoGenerateColumns = true;
        carregaGrid();
        defineConfiguracoesGrid();
        gvDados.Selection.UnselectAll();

        
    }
    
    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        DataSet ds = cDados.getTimeSheetAprovacao(idUsuarioLogado, codigoEntidade);

        gvDados.Columns.Clear();
        gvDados.AutoGenerateColumns = true;

        if (cDados.DataSetOk(ds))
        {
            ds.Tables[0].DefaultView.Sort = "StatusAprovacao";
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

        defineConfiguracoesGrid();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        
        gvDados.Selection.UnselectAll();
        gvDados.Columns.Clear();
        gvDados.AutoGenerateColumns = true;
        carregaGrid();
        defineConfiguracoesGrid();
        gvDados.Selection.UnselectAll();

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AprovTar");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "AprovTar", "Aprovações de Tarefas", this);
    }

    #endregion    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void callBack_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        e.Properties["cp_ExistemInformacoesASeremPublicadas"] = VerificaSeExistemInformacoesASeremPublicadas() ? "S" : "N";
    }

    //protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    //{
    //    if (e.RowType == GridViewRowType.Data)
    //    {
    //        string indicaLink = e.GetValue("IndicaLinkDetalhesOutrosCustos").ToString();

    //        if ("S" != indicaLink)
    //        {
    //            //e.Row.BackColor = Color.FromName("#DDFFCC");
    //            //e.Row.ForeColor = Color.Black;
    //            //e.Row.ForeColor = Color.FromName("#619340");
    //        }
    //    }
    //}

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ValorApontamentoOutrosCustos")
        {
            string indicaLink = e.GetValue("IndicaLinkDetalhesOutrosCustos").ToString().Trim().ToUpper();
            string codigoAtribuicao = e.GetValue("CodigoAtribuicao").ToString().Trim().ToUpper();
            string atag = indicaLink == "S" ? "<a href='#' onclick='abrirLinkDetalhesOutrosCustos(" + codigoAtribuicao + ");'>" : "";
            string barraatag = indicaLink == "S" ? "</a>" : "";
            e.Cell.Text = atag + string.Format("{0:c2}", e.CellValue) + barraatag;
            if (indicaLink == "S")
            {
                e.Cell.ForeColor = Color.FromName("#619340");
            }
        }
    }

    protected void gvOutrosCustosEmAprovacao_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        var codigoAtribuicao = e.Parameters;
        string comandoSQL = string.Format(@"
        SELECT 
          [CodigoAtribuicao]
         , [NomeRecurso]
         , [UnidadeMedidaRecurso]
  
         , [UnidadeAtribuicaoPrevisto]
         , [UnidadeAtribuicaoReal]
         , [UnidadeAtribuicaoRealInformado]
         , [UnidadeAtribuicaoRestanteInformado]
  
         , [CustoUnitarioPrevisto]
         , [CustoUnitarioReal]
         , [CustoUnitarioRealInformado]
         , [CustoUnitarioRestanteInformado]
  
         , [CustoPrevisto]
         , [CustoReal]
         , [CustoRealInformado]
         , [CustoRestanteInformado]
          FROM [dbo].[f_art_GetDetalhesOutrosCustoEmAprovacao] ({0}, {1}, {2})", codigoEntidade, idUsuarioLogado, codigoAtribuicao);
        ((ASPxGridView)sender).JSProperties["cpCodigoAtribuicao"] = codigoAtribuicao;
        DataSet ds = cDados.getDataSet(comandoSQL);
        ((ASPxGridView)sender).DataSource = ds;
        ((ASPxGridView)sender).DataBind();
    }



    protected void gvOutrosCustosEmAprovacao_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        e.Cell.BorderColor = Color.Black;
        //e.Cell.BorderStyle = BorderStyle.Solid;
        //e.Cell.CssClass += " border";
        
        if (e.DataColumn.FieldName.Equals("UnidadeAtribuicaoPrevisto") ||
            e.DataColumn.FieldName.Equals("CustoUnitarioPrevisto") ||
            e.DataColumn.FieldName.Equals("CustoPrevisto"))
            e.Cell.BackColor = Color.FromName("#E1DFCD");
    }
}
