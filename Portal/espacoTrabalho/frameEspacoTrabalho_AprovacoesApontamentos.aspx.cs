//Revisado
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;

public partial class espacoTrabalho_frameEspacoTrabalho_AprovacoesApontamentos : System.Web.UI.Page
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

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/aprovacaoApontamentos.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/custom.css"" rel=""stylesheet"" />"));

        btnPublicar.ClientEnabled = false;
        btnAplicar.ClientEnabled = true;
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
            Master.verificaAcaoFavoritos(true, "Aprovações de Apontamentos", "APROV", "ENT", -1, Resources.traducao.frameEspacoTrabalho_AprovacoesTarefas_adicionar_aos_favoritos);
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

        this.TH(this.TS("frameEspacoTrabalho", "frameEspacoTrabalho_AprovacoesTarefas", "aprovacaoTarefas"));
    }

    private void carregaGrid()
    {
        DataSet ds = cDados.getApontamentoAprovacao(idUsuarioLogado, codigoEntidade);

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
            btnAplicar.ClientEnabled = false;
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
            cmd.Width = 155;

            cmd.VisibleIndex = 0;

            cmd.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            #region [Inclusão dos grupos de colunas]
            gvDados.Columns.Insert(0, cmd);
            GridViewBandColumn bandTrabalho = new GridViewBandColumn("Horas");
            bandTrabalho.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bandTrabalho.Columns.Add(gvDados.Columns["TrabalhoRestante"]);
            bandTrabalho.Columns.Add(gvDados.Columns["TrabalhoRealTotal"]);
            bandTrabalho.Columns.Add(gvDados.Columns["PercConcluido"]);
            gvDados.Columns.Insert(5, bandTrabalho);

            GridViewBandColumn bandPrevisto = new GridViewBandColumn("Previsto");
            bandPrevisto.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bandPrevisto.Columns.Add(gvDados.Columns["UnidadeAtribuicaoLB"]);
            bandPrevisto.Columns.Add(gvDados.Columns["CustoUnitarioLB"]);
            bandPrevisto.Columns.Add(gvDados.Columns["CustoLB"]);
            gvDados.Columns.Insert(6, bandPrevisto);

            GridViewBandColumn bandAprovado = new GridViewBandColumn("Aprovado");
            bandAprovado.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bandAprovado.Columns.Add(gvDados.Columns["UnidadeAtribuicaoAcumulado"]);
            bandAprovado.Columns.Add(gvDados.Columns["CustoUnitarioAcumulado"]);
            bandAprovado.Columns.Add(gvDados.Columns["CustoAcumulado"]);
            gvDados.Columns.Insert(7, bandAprovado);

            //string labelOutrosCustos = RetornaLabelGrupoOutrosCustos();

            GridViewBandColumn bandOutrosCustos = new GridViewBandColumn("Solicitado");
            bandOutrosCustos.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bandOutrosCustos.Columns.Add(gvDados.Columns["UnidadeAtribuicaoRealInformado"]);
            bandOutrosCustos.Columns.Add(gvDados.Columns["CustoUnitarioRealInformado"]);
            bandOutrosCustos.Columns.Add(gvDados.Columns["CustoRealInformado"]);
            gvDados.Columns.Insert(8, bandOutrosCustos);

            GridViewBandColumn bandSaldo = new GridViewBandColumn("Saldo");
            bandSaldo.VisibleIndex = 9;
            bandSaldo.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bandSaldo.Columns.Add(gvDados.Columns["SaldoUnidadeAtribuicao"]);
            bandSaldo.Columns.Add(gvDados.Columns["CustoUnitarioSaldo"]);
            bandSaldo.Columns.Add(gvDados.Columns["SaldoCusto"]);
            gvDados.Columns.Insert(9, bandSaldo);
            #endregion

            #region [Visibilidade das colunas]
            gvDados.Columns["CodigoApontamentoAtribuicao"].Visible = false;
            gvDados.Columns["StatusAprovacao"].Visible = false;
            gvDados.Columns["CodigoUsuarioApontamento"].Visible = false;
            gvDados.Columns["CodigoCronogramaProjeto"].Visible = false;
            gvDados.Columns["CodigoTarefa"].Visible = false;
            gvDados.Columns["CodigoRecursoProjeto"].Visible = false;
            gvDados.Columns["CodigoAtribuicao"].Visible = false;
            gvDados.Columns["CodigoCronogramaApontamento"].Visible = false;
            gvDados.Columns["TipoAtualizacao"].Visible = false;
            gvDados.Columns["DataApontamento"].Visible = false;
            gvDados.Columns["TrabalhoReal"].Visible = false;
            gvDados.Columns["CodigoAtribuicaoRecurso"].Visible = false;
            #endregion

            #region [Configuração de tamanho das colunas]
            gvDados.Columns["NomeUsuarioApontamento"].Width = 155;            //Registrado por
            gvDados.Columns["NomeProjeto"].Width = 210;                       // Projeto
            gvDados.Columns["NomeTarefa"].Width = 210;                        // Tarefa
            gvDados.Columns["NomeRecursoAlocacao"].Width = 210;               //Recurso

            gvDados.Columns["UnidadeAtribuicaoLB"].Width = 85;
            gvDados.Columns["CustoUnitarioLB"].Width = 75;
            gvDados.Columns["CustoLB"].Width = 110;
            gvDados.Columns["UnidadeAtribuicaoAcumulado"].Width = 85;
            gvDados.Columns["CustoUnitarioAcumulado"].Width = 75;
            gvDados.Columns["CustoAcumulado"].Width = 110;
            gvDados.Columns["UnidadeAtribuicaoRealInformado"].Width = 85;    //Quantidade
            gvDados.Columns["CustoUnitarioRealInformado"].Width = 75;        //Vlr. Unit.
            gvDados.Columns["CustoRealInformado"].Width = 110;                //Total
            gvDados.Columns["SaldoUnidadeAtribuicao"].Width = 85;
            gvDados.Columns["CustoUnitarioSaldo"].Width = 75;
            gvDados.Columns["SaldoCusto"].Width = 110;

            gvDados.Columns["TrabalhoRestante"].Width = 90;                  //Restante(h)
            gvDados.Columns["TrabalhoRealTotal"].Width = 95;                 //Realizado(h)
            gvDados.Columns["PercConcluido"].Width = 80;                      //% Concluído
            gvDados.Columns["InicioReal"].Width = 90;                        //Início Real
            gvDados.Columns["TerminoReal"].Width = 95;                       //Término Real

            #endregion

            #region [Posicionamento das colunas]
            gvDados.Columns["NomeUsuarioApontamento"].VisibleIndex = 1;
            gvDados.Columns["NomeProjeto"].VisibleIndex = 2;
            gvDados.Columns["NomeTarefa"].VisibleIndex = 3;
            gvDados.Columns["NomeRecursoAlocacao"].VisibleIndex = 4;
            bandTrabalho.VisibleIndex = 5;
            bandPrevisto.VisibleIndex = 6;
            bandAprovado.VisibleIndex = 7;
            bandOutrosCustos.VisibleIndex = 8;
            gvDados.Columns["TrabalhoRestante"].VisibleIndex = gvDados.Columns["TrabalhoRealTotal"].VisibleIndex + 1;
            gvDados.Columns["PercConcluido"].VisibleIndex = gvDados.Columns["TrabalhoRestante"].VisibleIndex + 1;

            gvDados.Columns["CustoUnitarioLB"].VisibleIndex = gvDados.Columns["UnidadeAtribuicaoLB"].VisibleIndex + 1;
            gvDados.Columns["CustoLB"].VisibleIndex = gvDados.Columns["CustoUnitarioLB"].VisibleIndex + 1;

            gvDados.Columns["CustoUnitarioAcumulado"].VisibleIndex = gvDados.Columns["UnidadeAtribuicaoAcumulado"].VisibleIndex + 1;
            gvDados.Columns["CustoAcumulado"].VisibleIndex = gvDados.Columns["CustoUnitarioAcumulado"].VisibleIndex + 1;

            gvDados.Columns["CustoUnitarioRealInformado"].VisibleIndex = gvDados.Columns["UnidadeAtribuicaoRealInformado"].VisibleIndex + 1;
            gvDados.Columns["CustoRealInformado"].VisibleIndex = gvDados.Columns["CustoUnitarioRealInformado"].VisibleIndex + 1;

            gvDados.Columns["CustoUnitarioSaldo"].VisibleIndex = gvDados.Columns["SaldoUnidadeAtribuicao"].VisibleIndex + 1;
            gvDados.Columns["SaldoCusto"].VisibleIndex = gvDados.Columns["CustoUnitarioSaldo"].VisibleIndex + 1;
            #endregion

            #region [Caption das colunas]
            gvDados.Columns["NomeUsuarioApontamento"].Caption = "Registrado por";
            gvDados.Columns["NomeProjeto"].Caption = "Projeto";
            gvDados.Columns["NomeTarefa"].Caption = "Tarefa";
            gvDados.Columns["NomeRecursoAlocacao"].Caption = "Recurso";
            gvDados.Columns["TrabalhoRealTotal"].Caption = "Realizado";
            gvDados.Columns["TrabalhoRestante"].Caption = "Restante";
            gvDados.Columns["PercConcluido"].Caption = "Concluído";
            gvDados.Columns["InicioReal"].Caption = "Início Real";
            gvDados.Columns["TerminoReal"].Caption = "Término Real";

            gvDados.Columns["UnidadeAtribuicaoLB"].Caption = "Quantidade";
            gvDados.Columns["CustoUnitarioLB"].Caption = "Vlr. Unit.";
            gvDados.Columns["CustoLB"].Caption = "Total";

            gvDados.Columns["UnidadeAtribuicaoAcumulado"].Caption = "Quantidade";
            gvDados.Columns["CustoUnitarioAcumulado"].Caption = "Vlr. Unit.";
            gvDados.Columns["CustoAcumulado"].Caption = "Total";

            gvDados.Columns["UnidadeAtribuicaoRealInformado"].Caption = "Quantidade";
            gvDados.Columns["CustoUnitarioRealInformado"].Caption = "Vlr. Unit.";
            gvDados.Columns["CustoRealInformado"].Caption = "Total";

            gvDados.Columns["SaldoUnidadeAtribuicao"].Caption = "Quantidade";
            gvDados.Columns["CustoUnitarioSaldo"].Caption = "Vlr. Unit.";
            gvDados.Columns["SaldoCusto"].Caption = "Total";
            #endregion

            #region [Opções de filtros das colunas]
            ((GridViewDataTextColumn)gvDados.Columns["NomeUsuarioApontamento"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)gvDados.Columns["NomeUsuarioApontamento"]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;

            ((GridViewDataTextColumn)gvDados.Columns["NomeProjeto"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)gvDados.Columns["NomeProjeto"]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;

            ((GridViewDataTextColumn)gvDados.Columns["NomeTarefa"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)gvDados.Columns["NomeTarefa"]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;

            ((GridViewDataTextColumn)gvDados.Columns["TrabalhoRestante"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["TrabalhoRealTotal"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["PercConcluido"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataDateColumn)gvDados.Columns["InicioReal"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataDateColumn)gvDados.Columns["TerminoReal"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

            ((GridViewDataTextColumn)gvDados.Columns["UnidadeAtribuicaoLB"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioLB"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["CustoLB"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

            ((GridViewDataTextColumn)gvDados.Columns["UnidadeAtribuicaoAcumulado"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioAcumulado"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["CustoAcumulado"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

            ((GridViewDataTextColumn)gvDados.Columns["UnidadeAtribuicaoRealInformado"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioRealInformado"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["CustoRealInformado"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

            ((GridViewDataTextColumn)gvDados.Columns["SaldoUnidadeAtribuicao"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioSaldo"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataTextColumn)gvDados.Columns["SaldoCusto"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
            #endregion

            #region [Formatação dos dados]
            ((GridViewDataTextColumn)gvDados.Columns["PercConcluido"]).PropertiesTextEdit.DisplayFormatString = "{0:n0}%";
            ((GridViewDataDateColumn)gvDados.Columns["InicioReal"]).PropertiesDateEdit.DisplayFormatString = "{0:dd/MM/yyyy}";
            ((GridViewDataDateColumn)gvDados.Columns["TerminoReal"]).PropertiesDateEdit.DisplayFormatString = "{0:dd/MM/yyyy}";

            ((GridViewDataTextColumn)gvDados.Columns["UnidadeAtribuicaoLB"]).PropertiesTextEdit.DisplayFormatString = "{0:n0}";
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioLB"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";
            ((GridViewDataTextColumn)gvDados.Columns["CustoLB"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";

            ((GridViewDataTextColumn)gvDados.Columns["UnidadeAtribuicaoAcumulado"]).PropertiesTextEdit.DisplayFormatString = "{0:n0}";
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioAcumulado"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";
            ((GridViewDataTextColumn)gvDados.Columns["CustoAcumulado"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";

            ((GridViewDataTextColumn)gvDados.Columns["UnidadeAtribuicaoRealInformado"]).PropertiesTextEdit.DisplayFormatString = "{0:n0}";
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioRealInformado"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";
            ((GridViewDataTextColumn)gvDados.Columns["CustoRealInformado"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";

            ((GridViewDataTextColumn)gvDados.Columns["SaldoUnidadeAtribuicao"]).PropertiesTextEdit.DisplayFormatString = "{0:n0}";
            ((GridViewDataTextColumn)gvDados.Columns["CustoUnitarioSaldo"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";
            ((GridViewDataTextColumn)gvDados.Columns["SaldoCusto"]).PropertiesTextEdit.DisplayFormatString = "{0:c2}";
            #endregion

            #region [Alinhamento das colunas]
            gvDados.Columns["NomeUsuarioApontamento"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["NomeProjeto"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["NomeTarefa"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TrabalhoRestante"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TrabalhoRealTotal"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["PercConcluido"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["InicioReal"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TerminoReal"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["NomeRecursoAlocacao"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["UnidadeAtribuicaoRealInformado"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["CustoUnitarioRealInformado"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["CustoRealInformado"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["UnidadeAtribuicaoRealInformado"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["UnidadeAtribuicaoLB"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["CustoUnitarioLB"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["CustoLB"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["UnidadeAtribuicaoAcumulado"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["CustoUnitarioAcumulado"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["CustoAcumulado"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            gvDados.Columns["TrabalhoRestante"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TrabalhoRealTotal"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["PercConcluido"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["InicioReal"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["TerminoReal"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["UnidadeAtribuicaoRealInformado"].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDados.Columns["CustoUnitarioRealInformado"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["CustoRealInformado"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["UnidadeAtribuicaoRealInformado"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["UnidadeAtribuicaoLB"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["CustoUnitarioLB"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["CustoLB"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["UnidadeAtribuicaoAcumulado"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["CustoUnitarioAcumulado"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDados.Columns["CustoAcumulado"].CellStyle.HorizontalAlign = HorizontalAlign.Right;
            #endregion
        }
    }

    private string RetornaLabelGrupoOutrosCustos()
    {
        string labelGrupoOutrosCustos = "Outros Custos";

        DataSet ds1 = cDados.getParametrosSistema(codigoEntidade, "popupLabelAbaOutrosCustos");
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            labelGrupoOutrosCustos = ds1.Tables[0].Rows[0]["popupLabelAbaOutrosCustos"].ToString();
        }

        return labelGrupoOutrosCustos;
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";
        callBack.JSProperties["cp_MSG"] = "";
        bool retorno;
        string msgErro = "";

        if (e.Parameter.ToString() == "A")
        {
            string status = ddlAcao.Value.ToString();            

            int[] arrayCodigosAtribuicoes = new int[gvDados.Selection.Count];

            for (int i = 0; i < gvDados.Selection.Count; i++)
            {
                arrayCodigosAtribuicoes[i] = int.Parse(gvDados.GetSelectedFieldValues("CodigoApontamentoAtribuicao")[i].ToString());
            }

            if (gvDados.Selection.Count > 0)
            {
                retorno = cDados.atualizaStatusApontamentos(arrayCodigosAtribuicoes, status, idUsuarioLogado, ref msgErro);
                if(retorno)
                    callBack.JSProperties["cp_OK"] = "Status do apontamento alterado com sucesso.";
                else
                    callBack.JSProperties["cp_Erro"] = "Erro ao alterar o status.";
            }
            else
            {
                callBack.JSProperties["cp_MSG"] = "Nenhum apontamento foi selecionado.";
            }
        }
        else
        {
            if (e.Parameter.ToString() == "P")
            {
                if (!VerificaSeExistemInformacoesASeremPublicadas())
                {
                    callBack.JSProperties["cp_MSG"] = "Não existem apontamentos a serem publicados";
                    return;
                }
                retorno = cDados.publicaAprovacaoApontamentosEntidade(idUsuarioLogado, codigoEntidade, ref msgErro);

                if (retorno)
                    callBack.JSProperties["cp_OK"] = "Apontamentos publicados com sucesso.";
                else
                    callBack.JSProperties["cp_Erro"] = "Erro ao publicar apontamentos.";
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
                e.Text = "Em Aprovação e Aguardando Publicação.";
            }
            else if (gvDados.GetRowValues(e.VisibleIndex, "StatusAprovacao") + "" == "ER")
            {
                e.Image.Url = "~/imagens/botoes/tarefasER_pequeno.PNG";
                e.Text = "Em Reprovação e Aguardando Publicação.";
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        decimal valor = 0;
        if (e.DataColumn.FieldName.Equals("SaldoUnidadeAtribuicao") || e.DataColumn.FieldName.Equals("SaldoCusto"))
        {
            bool retorno = decimal.TryParse(e.GetValue(e.DataColumn.FieldName).ToString(), out valor);
            if (retorno == true && valor < 0)
            {
                e.Cell.ForeColor = Color.Red;
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
        DataSet ds = cDados.getApontamentoAprovacao(idUsuarioLogado, codigoEntidade);

        gvDados.Columns.Clear();
        gvDados.AutoGenerateColumns = true;

        if (cDados.DataSetOk(ds))
        {
            ds.Tables[0].DefaultView.Sort = "StatusAprovacao";
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

        defineConfiguracoesGrid();
        gvDados.JSProperties["cp_ExistemInformacoesASeremPublicadas"] = VerificaSeExistemInformacoesASeremPublicadas() == true ? "S" : "N";

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

}
