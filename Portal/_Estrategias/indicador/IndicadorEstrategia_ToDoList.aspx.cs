/*
 * OBSERVAÇÕES
 * 
 * MUDANÇAS:
 * 
 * 04/03/2011 :: Alejandro : Permissões da tela [IN_CnsPlnAcn]
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

public partial class _Estrategias_indicador_IndicadorEstrategia_ToDoList : System.Web.UI.Page
{
    //variaveis de acceso ao banco de dados.
    dados cDados;

    //variaveis de logeo.
    private int codigoEntidade = 0;
    private int idUsuarioLogado = 0;

    //variaveis de configuração
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

    //variaveis de permissão.
    public bool podeEditar = false;         //ALTTDLOBJ
    public bool podeIncluir = false;        //INCTDLOBJ
    public bool podeExcluir = false;        //EXCTDLOBJ
    public bool podeConsultar = false;      //CONSTDLOBJ

    //variaveis gerais.
    private char Delimitador_Erro = '¥';
    private int codigoIndicador = 0, codigoObjetivo = 0, codigoMapa = 0;
    private int codigoTipoAssociacao;
    private ASPxGridView gvToDoList;
    DataTable dt;    
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
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["COIN"] != null && Request.QueryString["COIN"].ToString() != "")
        {
            codigoIndicador = int.Parse(Request.QueryString["COIN"].ToString());
            hfGeral.Set("codigoObjetoAssociado", codigoIndicador);

            codigoObjetivo = cDados.getInfoSistema("COE") != null ? int.Parse(cDados.getInfoSistema("COE").ToString()) : -1;
            codigoMapa = cDados.getInfoSistema("CodigoMapa") != null ? int.Parse(cDados.getInfoSistema("CodigoMapa").ToString()) : 0;
        }   

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, idUsuarioLogado, codigoEntidade, codigoIndicador, "null", "IN", codigoMapa*(-1), "null", "IN_CnsPlnAcn");
        }

        carregaCodigoTipoAssociacao(ref codigoTipoAssociacao);       
        
        carregaPlanoAcao();
        cDados.aplicaEstiloVisual(Page);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();
       
        carregaResponsavel();
        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            
            carregaCampos();

        }
        hfGeral.Set("urlGantt", Session["baseUrl"] + "/_Public/Gantt/paginas/planoAcao/Default.aspx?IniciaisObjeto=IN&TrazerIniciativas=N");
        getPermissoesTela();
        carregarGrid();
            
        if (codigoObjetivo <= 0)
            tdObjetivoMapa.Style.Add("display", "none");
    }

    private void carregaPlanoAcao()
    {
        //---- Escopo que setea a grid PlanoAcao.
        int[] convidados = getParticipantesEvento();
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


        Unit tamanho = new Unit("100%");

        PlanoDeAcao myPlanoDeAcao = null;
        //todo: ATENÇÃO, observar o error da variavel somenteLeitura. atualmente forçado para 'false'
        //somenteLeitura = false; //<- forçado.
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoEntidade, idUsuarioLogado, null, codigoTipoAssociacao, codigoIndicador, tamanho, 189, somenteLeitura, convidados.Length == 0 ? null : convidados, true, txtDescricaoPlanoAcao.Text);
        pAcoes.Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario(codigoToDoList));
        gvToDoList = myPlanoDeAcao.gvToDoList;

        gvToDoList.ClientInstanceName = "gvToDoList";
        gvToDoList.ClientSideEvents.EndCallback = "function(s, e) {pcDados.Show();}";
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtDescricaoPlanoAcao.GetText());}";
        gvToDoList.ClientSideEvents.Init = @"function(s, e) {
            window.top.lpAguardeMasterPage.Hide(); 
            var height = Math.max(0, document.documentElement.clientHeight - 260);
            s.SetHeight(height);
}";
        if (!IsCallback)
            gvToDoList.DataBind();
        //---- FIM Escopo que setea a grid PlanoAcao.
    }

    #region VARIOS

    private void carregaCampos()
    {
        dt = cDados.getInformacoesIndicador(codigoIndicador, codigoObjetivo).Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtObjetivoEstrategico.Text = dt.Rows[0]["DescricaoObjetoEstrategia"].ToString();
            txtIndicador.Text = dt.Rows[0]["NomeIndicador"].ToString();
        }
        else
        {
            txtObjetivoEstrategico.Text = "";
            txtIndicador.Text = "";
            txtMapa.Text = "";
        }
    }

    private void getPermissoesTela()
    {
        int idObjetoPai;

        if (codigoMapa == 0)
        {
            idObjetoPai = codigoEntidade;
        }
        else
            idObjetoPai = codigoMapa * (-1);

        //Procurar permissão para visualizar Ações Sugeridas.
        DataSet ds = cDados.getPermissoesDoObjetivoPelaTela(idUsuarioLogado, codigoEntidade, codigoIndicador, idObjetoPai, "IN", "IN_IncPlnAcn", "IN_AltPlnAcn", "IN_ExcPlnAcn");
        if (cDados.DataSetOk(ds))
        {
            podeIncluir = int.Parse(ds.Tables[0].Rows[0]["IN_IncPlnAcn"].ToString()) > 0;
            podeEditar = int.Parse(ds.Tables[0].Rows[0]["IN_AltPlnAcn"].ToString()) > 0;
            podeExcluir = int.Parse(ds.Tables[0].Rows[0]["IN_ExcPlnAcn"].ToString()) > 0;
        }
    }

    private void headerOnTela()
    {
        this.TH(this.TS("barraNavegacao", "ObjetivoEstrategico_ToDoList", "IndicadorEstrategia_ToDoList"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/IndicadorEstrategia_ToDoList.js""></script>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int alturaCalculada = alturaPrincipal - 200;
        int constanteSomatoria = (codigoObjetivo <= 0) ? 90 : 0;

        gvDados.Settings.VerticalScrollableHeight = alturaCalculada - 265 + constanteSomatoria;
    }

    private void carregaCodigoTipoAssociacao(ref int codigoTipoAssociacao)
    {
        DataTable dt = cDados.getTipoAssociacaoEventos("IN", "").Tables[0];

        if (dt.Rows.Count > 0)
            codigoTipoAssociacao = int.Parse(dt.Rows[0]["CodigoTipoAssociacao"].ToString());
    }

    private int[] getParticipantesEvento()
    {
        DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtiva(codigoEntidade.ToString(), "");

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
        DataSet ds = cDados.getUsuarioUnidadeNegocioAtivo(string.Format(" AND uun.CodigoUnidadeNegocio = {0} ", codigoEntidade.ToString()));

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
            //ddlResponsavel.SelectedIndex = 0;
        }
    }

    #endregion

    #region GRIDVIEW

    private void carregarGrid()
    {
        DataSet ds = cDados.getToDoListEstrategica(codigoIndicador.ToString(), codigoTipoAssociacao, codigoEntidade.ToString());

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
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

        if (e.ButtonID == "btnFormularioCustom")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/verDetalheDes.png";
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {

    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

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
        if (e.Parameter == "ShowToDoList")
        {
            bool somenteLeitura = false;

            if (hfGeral.Contains("TipoOperacao"))
                somenteLeitura = (hfGeral.Get("TipoOperacao").ToString() != "Incluir" && hfGeral.Get("TipoOperacao").ToString() != "Editar");
                        
            int codresp = int.Parse(hfGeral.Contains("codRespSelecionado") ? hfGeral.Get("codRespSelecionado").ToString() : "-1");
            ListEditItem li = ddlResponsavel.Items.FindByValue(codresp);
            ddlResponsavel.SelectedItem = li;
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;

            if (e.Parameter != "ShowToDoList")
                Session.Remove("_CodigoToDoList_"); // remove o código do último todo list;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string msg = "";
        string descricao = (txtDescricaoPlanoAcao.Text == null || txtDescricaoPlanoAcao.Text.Trim().Equals("")) ? "NULL" : "'" + txtDescricaoPlanoAcao.Text.Trim().Replace("'", "''") + "'";
        string responsavel = ddlResponsavel.Value.ToString();
        int codigoToDoList;
        try
        {
            // se ainda não gravou tarefa dentro do plano de ação, inclui apenas o registro do ToDoList
            if (Session["_CodigoToDoList_"] == null)
            {
                if (cDados.incluiToDoList_Estrategica(idUsuarioLogado.ToString(), codigoTipoAssociacao.ToString(), responsavel
                                                            , codigoIndicador.ToString(), codigoEntidade.ToString(), descricao))
                {
                    carregarGrid();
                    gvDados.ClientVisible = false;
                }
            }
            else if (int.TryParse(Session["_CodigoToDoList_"].ToString(), out codigoToDoList))
            {
                if (cDados.complementaDadosToDoListEstrategico(codigoToDoList, descricao, responsavel))
                {
                    carregarGrid();
                    gvDados.ClientVisible = false;
                }
            }
            else
                throw new Exception(Delimitador_Erro + "Ocorreu um erro ao atualizar os dados.\n\n" + Delimitador_Erro + " \n Não foi possível determinar o código interno do plano de ação. \n");
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = ex.Message;
        }
        return msg;
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        string msg = "";
        string descricao = (txtDescricaoPlanoAcao.Text == null || txtDescricaoPlanoAcao.Text.Trim().Equals("")) ? "NULL" : "'" + txtDescricaoPlanoAcao.Text.Trim().Replace("'", "''") + "'";
        string responsavel = ddlResponsavel.Value.ToString();
        string auxCodigo = getChavePrimaria();
        int codigoToDoList;
        try
        {
            if (int.TryParse(auxCodigo, out codigoToDoList))
            {
                if (cDados.complementaDadosToDoListEstrategico(codigoToDoList, descricao, responsavel))
                {
                    carregarGrid();
                    gvDados.ClientVisible = false;
                }
            }
            else
                throw new Exception(Delimitador_Erro + "Ocorreu um erro ao atualizar os dados.\n\n" + Delimitador_Erro + " \n Não foi possível determinar o código interno do plano de ação. \n");
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            msg = ex.Message;
        }
        return msg;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgRetorno = "";

        cDados.excluiToDoList(chave, int.Parse(idUsuarioLogado.ToString()), ref msgRetorno);
        carregarGrid();
        //gvDados.ClientVisible = false;

        return msgRetorno;
    }

    #endregion

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        codigoTipoAssociacao = 7;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ToDoListInd");
    }

    protected void menu_Init2(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, @"", true, true, false, "ToDoListInd", "Tarefas Indicador", this);

        
        
        (sender as ASPxMenu).ClientSideEvents.ItemClick =
        @"function(s, e){ 
            if(e.item.name == 'btnIncluir')
            {
                e.processOnServer = false;
                Click_NovaAcaoSugerida();
            }		                     
	        else if(e.item.name == 'btnGantt')
            {
                e.processOnServer = false;
                abreGanttOEIndicador(" + codigoIndicador + @");
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
            e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
