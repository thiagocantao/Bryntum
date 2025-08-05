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
    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool podeConsultar = true;

    //variaveis gerais.
    private char Delimitador_Erro = '¥';
    private int codigoObjetivoEstrategico = 0;
    private int codigoTipoAssociacao;
    private ASPxGridView gvToDoList;

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

        if (Request.QueryString["CT"] != null && Request.QueryString["CT"].ToString() != "")
        {
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["CT"].ToString());
            hfGeral.Set("codigoObjetoAssociado", codigoObjetivoEstrategico);
        }
        carregaCodigoTipoAssociacao(ref codigoTipoAssociacao);

        imgGantt.ClientSideEvents.Click = "function(s, e) { abreGanttOE(" + codigoObjetivoEstrategico + ", '-1');}";
        carregaPlanoAcao();
        cDados.aplicaEstiloVisual(Page);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();
        getPermissoesTela();
        
        if (!IsPostBack)
        {
            //Verificar acesso do usuario logado a ista tela.
            //bool bPodeAcessarTela = false;
            //bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, codigoUnidadeLogada, "OB", "OB_CnsPlnAcn");
            //if (!bPodeAcessarTela)
            //    cDados.RedirecionaParaTelaSemAcesso(this);

            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            carregaCampos();
        }

        carregarGrid();
        carregaResponsavel();
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

        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
        {
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        }

        Unit tamanho = new Unit("774px");

        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoUnidadeSelecionada, idUsuarioLogado, null, codigoTipoAssociacao, codigoObjetivoEstrategico, tamanho, 165, somenteLeitura, convidados.Length == 0 ? null : convidados, true, txtDescricaoPlanoAcao.Text);
        pAcoes.Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario(codigoToDoList));
        gvToDoList = myPlanoDeAcao.gvToDoList;

        gvToDoList.ClientInstanceName = "gvToDoList";
        gvToDoList.ClientSideEvents.EndCallback = "function(s, e) {pcDados.Show();}";
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtDescricaoPlanoAcao.GetText());}";
        gvToDoList.DataBind();

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
        int alturaCalculada = alturaPrincipal - 198;

        gvDados.Settings.VerticalScrollableHeight = alturaCalculada - 105;
    }

    private void carregaCodigoTipoAssociacao(ref int codigoTipoAssociacao)
    {
        DataTable dt = cDados.getTipoAssociacaoEventos("TM", "").Tables[0];

        if (dt.Rows.Count > 0)
            codigoTipoAssociacao = int.Parse(dt.Rows[0]["CodigoTipoAssociacao"].ToString());
    }

    private void carregaCampos()
    {
        DataTable dt = cDados.getTemaEstrategico(codigoObjetivoEstrategico, "").Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtPerspectiva.Text = dt.Rows[0]["Perspectiva"].ToString();
            txtMapa.Text = dt.Rows[0]["TituloMapaEstrategico"].ToString();
            txtTema.Text = dt.Rows[0]["Tema"].ToString();
        }
        else
        {
            txtPerspectiva.Text = "";
            txtMapa.Text = "";
            txtTema.Text = "";
        }
    }

    private int[] getParticipantesEvento()
    {
        DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtiva(codigoUnidadeLogada.ToString(), "");

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
        DataSet ds = cDados.getUsuarioUnidadeNegocioAtivo(string.Format(" AND uun.CodigoUnidadeNegocio = {0} ", codigoUnidadeSelecionada.ToString()));

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
            
            if(!IsPostBack)
                ddlResponsavel.SelectedIndex = 0;
        }
    }

    private void getPermissoesTela()
    {
        //podeIncluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_IncPlnAcn"));
        //podeEditar = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_AltPlnAcn"));
        //podeExcluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoUnidadeLogada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_ExcPlnAcn"));
        podeConsultar = true;

        //cDados.verificaPermissaoOE(codigoObjetivoEstrategico, codigoUnidadeSelecionada);
        //podeEditar = permissaoMapa == false ? permissaoMapa : podeEditar;
        //podeExcluir = permissaoMapa == false ? permissaoMapa : podeExcluir;
        //podeIncluir = permissaoMapa == false ? permissaoMapa : podeIncluir;
    }

    #endregion

    #region GRIDVIEW

    private void carregarGrid()
    {
        DataSet ds = cDados.getToDoListEstrategica(codigoObjetivoEstrategico.ToString(), codigoTipoAssociacao, codigoUnidadeSelecionada.ToString());

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
        string responsavel = ddlResponsavel.SelectedIndex == -1 ? "NULL" : ddlResponsavel.Value.ToString();
        int codigoToDoList;
        try
        {
            // se ainda não gravou tarefa dentro do plano de ação, inclui apenas o registro do ToDoList
            if (Session["_CodigoToDoList_"] == null)
            {
                if (cDados.incluiToDoList_Estrategica(idUsuarioLogado.ToString(), codigoTipoAssociacao.ToString(), responsavel
                                                    ,codigoObjetivoEstrategico.ToString(), codigoUnidadeSelecionada.ToString()
                                                    , descricao))
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
        string responsavel = ddlResponsavel.SelectedIndex == -1 ? "NULL" : ddlResponsavel.Value.ToString();
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
        cDados.excluiToDoList(chave, idUsuarioLogado, ref msgRetorno);
        carregarGrid();
        //gvDados.ClientVisible = false;

        return msgRetorno;
    }

    #endregion
}
