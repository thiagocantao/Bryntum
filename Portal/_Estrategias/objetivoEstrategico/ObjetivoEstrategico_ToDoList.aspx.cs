/*OBSERVAÇÕES
 * 
 * Data criação: 29/07/2010
 * 
 * MUDANÇAS
 * 
 * 24/03/2011 :: Alejandro : Adaptação da tela com as permissões. 
 *               [OB_AltPlnAcn], [OB_CnsPlnAcn], [OB_ExcPlnAcn], [OB_IncPlnAcn]
 * 
 */
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
using System.Drawing;
using DevExpress.Web;
using CDIS;

public partial class _Estrategias_objetivoEstrategico_ObjetivoEstrategico_ToDoList : System.Web.UI.Page
{
    //variaveis de acceso ao banco de dados.
    dados cDados;

    //variaveis de logeo.
    private int codigoUnidadeLogada = 0, codigoUnidadeSelecionada = 0;
    private int idUsuarioLogado = 0;

    //variaveis de configuração
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

    //variaveis de permissão.
    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    public bool podeConsultar = false;

    //variaveis gerais.
    private char Delimitador_Erro = '¥';
    private int codigoObjetivoEstrategico = 0;
    private int codigoTipoAssociacao;
    private ASPxGridView gvToDoList;
    DataSet dsParametros;
    private bool utilizaPesoDesempenho = false;
    private decimal peso = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        hfGeral.Set("urlGantt", Session["baseUrl"] + "/_Public/Gantt/paginas/planoAcao/Default.aspx?IniciaisObjeto=OB&TrazerIniciativas=N");

        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
        {
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
            hfGeral.Set("codigoObjetoAssociado", codigoObjetivoEstrategico);
        }
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        carregaCodigoTipoAssociacao(ref codigoTipoAssociacao);

        carregaPlanoAcao();
        cDados.aplicaEstiloVisual(Page);
        dsParametros = cDados.getParametrosSistema("utilizaPesoDesempenhoObjetivo");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            utilizaPesoDesempenho = dsParametros.Tables[0].Rows[0]["utilizaPesoDesempenhoObjetivo"].ToString() == "S" ? true : false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("ObjetivoEstrategico_ToDoList"));

        headerOnTela();
        getPermissoesTela();
        defineAlturaTela(resolucaoCliente);
        if (!IsPostBack)
        {
            //Verificar acesso do usuario logado a ista tela.
            bool bPodeAcessarTela = false;
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, codigoUnidadeLogada, "OB", "OB_CnsPlnAcn");
            if (!bPodeAcessarTela)
                cDados.RedirecionaParaTelaSemAcesso(this);
            carregaCampos();
        }
        carregarGrid();
        carregaResponsavel();

        if (utilizaPesoDesempenho)
        {
            tdPeso3.Visible = true;
            tdPeso3Label.Visible = true;
        }
        else
        {
            tdPeso3.Visible = false;
            tdPeso3Label.Visible = false;
        }
    }

    private void carregaPlanoAcao()
    {
        //---- Escopo que setea a grid PlanoAcao.

        bool somenteLeitura = false;

        if (hfGeral.Contains("TipoOperacao"))
            somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");

        int codigoToDoList;

        if (!IsPostBack)
            Session["_CodigoToDoList_"] = null;

        if (Session["_CodigoToDoList_"] != null)
            codigoToDoList = int.Parse(Session["_CodigoToDoList_"].ToString());
        else
        {
            if (hfGeral.Contains("codigoToDoList") && hfGeral.Get("codigoToDoList").ToString() != "")
                codigoToDoList = int.Parse(hfGeral.Get("codigoToDoList").ToString());
            else
                codigoToDoList = -1;
        }

        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
        {
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        }

        int[] convidados = getParticipantesEvento();

        Unit tamanho = new Unit("100%");

        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, getCodigoEntidadeDaUnidade(codigoUnidadeSelecionada), idUsuarioLogado, null, codigoTipoAssociacao, codigoObjetivoEstrategico, tamanho, 175, somenteLeitura, convidados.Length == 0 ? null : convidados, true, txtDescricaoPlanoAcao.Text);
        pAcoes.Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario(codigoToDoList));
        gvToDoList = myPlanoDeAcao.gvToDoList;

        gvToDoList.ClientInstanceName = "gvToDoList";
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtDescricaoPlanoAcao.GetText());hfGeralToDoList.Set('command', e.command);}";
        gvToDoList.ClientSideEvents.EndCallback = @"function(s, e) {
    pcDados.Show(); 
    if(s.cp_Msg != '') {
        window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);
    }
    else {
        var command = hfGeralToDoList.Get('command');
        if(command === 'UPDATEEDIT')
            window.top.mostraMensagem(traducao.ObjetivoEstrategico_ToDoList_MensagemTarefaTDLSalvoComSucesso, 'sucesso', false, false, null);
        else if(command === 'DELETEROW')
            window.top.mostraMensagem(traducao.ObjetivoEstrategico_ToDoList_MensagemTarefaTDLExcluidoComSucesso, 'sucesso', false, false, null);
    }
}";
        gvToDoList.ClientSideEvents.Init = @"function(s, e) {
            window.top.lpAguardeMasterPage.Hide(); 
            var height = Math.max(0, document.documentElement.clientHeight - 230);
            s.SetHeight(height);
}";
        foreach (GridViewColumn col in gvToDoList.Columns)
        {
            if (col.Visible)
            {
                int acrescimo = 0;
                if (col is GridViewDataComboBoxColumn)
                    acrescimo = 90;
                else if (col is GridViewDataDateColumn)
                    acrescimo = 60;
                col.Width = new Unit(col.Width.Value + acrescimo);
            }
        }

        //gvToDoList.DataBind();

        //---- FIM Escopo que setea a grid PlanoAcao.
    }

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ObjetivoEstrategico_ToDoList.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ObjetivoEstrategico_ToDoList"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        int alturaCalculada = alturaPrincipal - 198;
        pcDados.Width = new Unit("100%");// largura - 300;
        gvDados.Settings.VerticalScrollableHeight = alturaCalculada - 240;
    }

    private void carregaCodigoTipoAssociacao(ref int codigoTipoAssociacao)
    {
        DataTable dt = cDados.getTipoAssociacaoEventos("OB", "").Tables[0];

        if (dt.Rows.Count > 0)
            codigoTipoAssociacao = int.Parse(dt.Rows[0]["CodigoTipoAssociacao"].ToString());
    }

    private void carregaCampos()
    {
        DataTable dt = cDados.getObjetivoEstrategico(null, codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            //txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            //txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtResponsavel.Text = dt.Rows[0]["NomeUsuario"].ToString();
            //txtTema.Text = dt.Rows[0]["Tema"].ToString();
        }
        else
        {
            //txtPerspectiva.Text = "";
            txtObjetivoEstrategico.Text = "";
            //txtMapa.Text = "";
            txtResponsavel.Text = "";
            //txtTema.Text = "";
        }
    }

    private int[] getParticipantesEvento()
    {
        int codigoEntidadeUnidade = cDados.getEntidadUnidadeNegocio(codigoUnidadeSelecionada);
        DataSet dsConvidados = cDados.getUsuarioUnidadeNegocioAtivo(string.Format(" AND uun.CodigoUnidadeNegocio = {0} ", codigoEntidadeUnidade.ToString()));

        int[] convidados = new int[dsConvidados.Tables[0].Rows.Count];

        if (cDados.DataSetOk(dsConvidados))
        {
            int i = 0;
            foreach (DataRow dr in dsConvidados.Tables[0].Rows)
            {
                convidados[i] = int.Parse(dr["CodigoUsuario"].ToString());
                i++;
            }
        }

        return convidados;
    }

    private void carregaResponsavel()
    {
        int codigoEntidadeUnidade = cDados.getEntidadUnidadeNegocio(codigoUnidadeSelecionada);
        DataSet ds = cDados.getUsuarioUnidadeNegocioAtivo(string.Format(" AND uun.CodigoUnidadeNegocio = {0} ", codigoEntidadeUnidade.ToString()));

        if (cDados.DataSetOk(ds))
        {
            ddlResponsavel.Columns.Clear();
            ListBoxColumn lbc1 = new ListBoxColumn("NomeUsuario", "Nome", 200);
            ListBoxColumn lbc2 = new ListBoxColumn("EMail", "Email", 350);
            ListBoxColumn lbc3 = new ListBoxColumn("StatusUsuario", "Status", 80);

            ddlResponsavel.Columns.Insert(0, lbc1);
            ddlResponsavel.Columns.Insert(1, lbc2);
            ddlResponsavel.Columns.Insert(2, lbc3);

            ddlResponsavel.DataSource = ds.Tables[0];
            ddlResponsavel.TextField = "NomeUsuario";
            ddlResponsavel.ValueField = "CodigoUsuario";
            ddlResponsavel.DataBind();

            if (!IsPostBack)
                ddlResponsavel.SelectedIndex = 0;
        }
    }

    private void getPermissoesTela()
    {
        podeIncluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_IncPlnAcn"));
        podeEditar = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_AltPlnAcn"));
        podeExcluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_ExcPlnAcn"));
        podeConsultar = true;

        //  cDados.verificaPermissaoOE(codigoObjetivoEstrategico, codigoUnidadeSelecionada);
        //podeEditar = permissaoMapa == false ? permissaoMapa : podeEditar;
        //podeExcluir = permissaoMapa == false ? permissaoMapa : podeExcluir;
        //podeIncluir = permissaoMapa == false ? permissaoMapa : podeIncluir;
    }

    #endregion

    #region GRIDVIEW

    private void carregarGrid()
    {
        DataSet ds = cDados.getToDoListEstrategica(codigoObjetivoEstrategico.ToString(), codigoTipoAssociacao, getCodigoEntidadeDaUnidade(codigoUnidadeSelecionada).ToString());

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditarCustom")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Text = "Editar";
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }

        if (e.ButtonID == "btnExcluirCustom")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Text = "Excluir";
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

        if (e.ButtonID == "btnFormularioCustom")
        {
            if (podeConsultar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Text = "Detalhe";
                e.Image.Url = "~/imagens/botoes/verDetalheDes.png";
            }
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        codigoTipoAssociacao = 7;
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "";
        ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = "";

        string mensagemErro_Persistencia = "";
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
            ((ASPxCallbackPanel)sender).JSProperties["cpSucesso"] = "Dados atualizados com sucesso!";
            if (e.Parameter != "ShowToDoList")
                Session.Remove("_CodigoToDoList_"); // remove o código do último todo list;
        }
        else // alguma coisa deu errado...
            ((ASPxCallbackPanel)sender).JSProperties["cpErro"] = mensagemErro_Persistencia;
    }

    private int getCodigoEntidadeDaUnidade(int codigoUnidade)
    {
        int retorno = -1;
        string comandoSQL = string.Format(@"SELECT [CodigoEntidade]
                                              FROM [dbo].[UnidadeNegocio] 
                                             WHERE codigounidadenegocio = {0}", codigoUnidade);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        return retorno;
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string msg = "";
        string descricao = (txtDescricaoPlanoAcao.Text == null || txtDescricaoPlanoAcao.Text.Trim().Equals("")) ? "NULL" : "'" + txtDescricaoPlanoAcao.Text.Trim().Replace("'", "''") + "'";
        string responsavel = ddlResponsavel.SelectedIndex == -1 ? "NULL" : ddlResponsavel.Value.ToString();
        int codigoToDoList = 0;

        int codEntidade = getCodigoEntidadeDaUnidade(codigoUnidadeSelecionada);

        if (utilizaPesoDesempenho)
        {
            if (PesoObjetoLink3.Value != null)
            {
                decimal.TryParse(PesoObjetoLink3.Value.ToString(), out peso);
            }
            else
            {
                decimal.TryParse(PesoObjetoLink3.NullText, out peso);
            }
        }

        try
        {

            // se ainda não gravou tarefa dentro do plano de ação, inclui apenas o registro do ToDoList
            if (Session["_CodigoToDoList_"] == null)
            {
                if (cDados.incluiToDoList_Estrategica(idUsuarioLogado.ToString(), codigoTipoAssociacao.ToString(), responsavel
                                                    , codigoObjetivoEstrategico.ToString(), codEntidade.ToString()
                                                    , descricao, out codigoToDoList))
                {
                    cDados.incluiAtualizaPeso(codigoObjetivoEstrategico, "OB", codigoToDoList.ToString(), "TD", peso);
                    carregarGrid();
                }
            }
            else if (int.TryParse(Session["_CodigoToDoList_"].ToString(), out codigoToDoList))
            {
                if (cDados.complementaDadosToDoListEstrategico(codigoToDoList, descricao, responsavel))
                {
                    cDados.incluiAtualizaPeso(codigoObjetivoEstrategico, "OB", codigoToDoList.ToString(), "TD", peso);
                    carregarGrid();
                }
            }
            else
                throw new Exception(Delimitador_Erro + "Ocorreu um erro ao atualizar os dados.\n\n" + Delimitador_Erro + " \n Não foi possível determinar o código interno do plano de ação. \n");


        }

        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        string msg = "";
        string descricao = (txtDescricaoPlanoAcao.Text == null || txtDescricaoPlanoAcao.Text.Trim().Equals("")) ? "NULL" : "'" + txtDescricaoPlanoAcao.Text.Trim().Replace("'", "''") + "'";
        string responsavel = ddlResponsavel.SelectedIndex == -1 ? "NULL" : ddlResponsavel.Value.ToString();
        string auxCodigo = getChavePrimaria();
        int codigoToDoList;
        try
        {
            if (int.TryParse(auxCodigo, out codigoToDoList))
            {
                if (cDados.complementaDadosToDoListEstrategico(codigoToDoList, descricao, responsavel))
                {
                    if (utilizaPesoDesempenho)
                    {
                        if (PesoObjetoLink3.Value != null)
                        {
                            decimal.TryParse(PesoObjetoLink3.Value.ToString(), out peso);
                        }
                        else
                        {
                            decimal.TryParse(PesoObjetoLink3.NullText, out peso);
                        }
                    }

                    cDados.incluiAtualizaPeso(codigoObjetivoEstrategico, "OB", codigoToDoList.ToString(), "TD", peso);
                    carregarGrid();
                }
            }
            else
                throw new Exception(Delimitador_Erro + "Ocorreu um erro ao atualizar os dados.\n\n" + Delimitador_Erro + " \n Não foi possível determinar o código interno do plano de ação. \n");

        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgRetorno = "";
        cDados.excluiToDoList(chave, idUsuarioLogado, codigoObjetivoEstrategico, ref msgRetorno);
        carregarGrid();
        return msgRetorno;
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ToDoListOE");
    }

    protected void menu_Init2(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, @"", true, true, false, "ToDoListOE", "Tarefas Objetivo Estratégico", this);        
        
        (sender as ASPxMenu).ClientSideEvents.ItemClick =
        @"function(s, e){ 
            if(e.item.name == 'btnIncluir')
            {
                e.processOnServer = false;
                var larguraPopup = Math.max(0, document.documentElement.clientWidth - 50);  
                pcDados.SetWidth(larguraPopup);
                Click_NovaAcaoSugerida();
            }		                     
	        else if(e.item.name == 'btnGantt')
            {
                e.processOnServer = false;
                var urlGantt = hfGeral.Get('urlGantt') + '&COE=" + codigoObjetivoEstrategico + @"';
                var altura = Math.max(0, document.documentElement.clientHeight - 20);
                window.top.showModal( urlGantt, traducao.ObjetivoEstrategico_ToDoList_gantt_do_plano_de_ac_o_do_objetivo_estrat_gico_selecionado, screen.width - 40, altura, '', null);
            }	
            else
	        {
                e.processOnServer = true;		                                        
	        }	
        }";
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if (e.Column.Name == "Status" && e.Value != null)
        {
            Font fontex = new Font("Wingdings", 18, FontStyle.Bold, GraphicsUnit.Point);
            e.BrickStyle.Font = fontex;

            if (e.Value.ToString().Equals("VermelhoOk"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Equals("VerdeOk"))
            {
                e.Text = "ü";
                e.TextValue = "ü";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Equals("Verde"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Green;
            }
            else if (e.Value.ToString().Equals("Laranja"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Orange;
            }
            else if (e.Value.ToString().Equals("Vermelho"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Red;
            }
            else if (e.Value.ToString().Equals("Amarelo"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.Yellow;
            }
            else if (e.Value.ToString().Equals("Branco"))
            {
                e.Text = "l";
                e.TextValue = "l";
                e.BrickStyle.ForeColor = Color.WhiteSmoke;
            }
        }
    }
}
