/*
 * OBSERVAÇÕES
 * 
 * Data criação: 29/07/2010
 * Author: Alejandro.
 * 
 * MUDANÇAS
 * 
 * 23/03/2011 :: Alejandro : Alteração de acesso a tela, e dos codigos de permissão.
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
using DevExpress.Web;


public partial class _Estrategias_objetivoEstrategico_ObjetivoEstrategico_AcoesSugeridas : System.Web.UI.Page
{
    //variaveis de acceso ao banco de dados.
    dados cDados;

    //variaveis de logeo.
    private int codigoUnidadeSelecionada = 0, codigoUnidadeMapa = 0, codigoUnidadeLogada = 0;
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
    private int codigoObjetivoEstrategico = 0;
    private int codigoTipoAssociacao;
    private string labelSingular = "";
    private string labelPlural = "";
    bool permissaoMapa = false;

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

        if (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "")
            codigoObjetivoEstrategico = int.Parse(Request.QueryString["COE"].ToString());
        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        if (Request.QueryString["UNM"] != null && Request.QueryString["UNM"].ToString() != "")
            codigoUnidadeMapa = int.Parse(Request.QueryString["UNM"].ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();
        cDados.aplicaEstiloVisual(this);
        carregaCodigoTipoAssociacao(ref codigoTipoAssociacao);

        getPermissoesTela();

        if (!IsPostBack)
        {
            //Verificação a acesso a tela atual.
            bool bPodeAcessarTela = false;
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, codigoUnidadeLogada, "OB", "OB_CnsAcnSug");
            if (!bPodeAcessarTela)
                RedirecionaParaTelaSemAcesso(this);

            carregaLabels();
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            
            carregaCampos();
        }
        carregarGrid();
    }

    public void RedirecionaParaTelaSemAcesso(Page page)
    {
        try
        {

            page.Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
        }
        catch
        {
            page.Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcessoNoMaster.aspx";
            page.Response.End();
        }
    }

    private void getPermissoesTela()
    {
        int codigoEntidadeUnidadeMapa = cDados.getEntidadUnidadeNegocio(codigoUnidadeMapa);
        int codigoEntidadeUnidadeSelecionada = cDados.getEntidadUnidadeNegocio(codigoUnidadeSelecionada);

        if (codigoUnidadeLogada == codigoEntidadeUnidadeMapa && codigoUnidadeMapa == codigoUnidadeSelecionada)
            permissaoMapa = cDados.verificaPermissaoOE(codigoObjetivoEstrategico, codigoUnidadeSelecionada);
        else
            permissaoMapa = false;

        podeIncluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidadeUnidadeSelecionada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_IncAcnSug"));
        podeEditar = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidadeUnidadeSelecionada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_AltAcnSug"));
        podeExcluir = (cDados.VerificaPermissaoUsuario(idUsuarioLogado, codigoEntidadeUnidadeSelecionada, codigoObjetivoEstrategico, "NULL", "OB", 0, "NULL", "OB_ExcAcnSug"));

        podeEditar = permissaoMapa == false ? permissaoMapa : podeEditar;
        podeExcluir = permissaoMapa == false ? permissaoMapa : podeExcluir;
        podeIncluir = permissaoMapa == false ? permissaoMapa : podeIncluir;
        podeConsultar = true; // sem tratamento de permissõa por tela.
    }

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ObjetivoEstrategico_AcoesSugeridas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ObjetivoEstrategico_AcoesSugeridas"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int alturaCalculada = alturaPrincipal - 190;

        gvDados.Settings.VerticalScrollableHeight = alturaCalculada - 195;
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

    private void carregaCodigoTipoAssociacao(ref int codigoTipoAssociacao)
    {
        DataTable dt = cDados.getTipoAssociacaoEventos("OB", "").Tables[0];

        if (dt.Rows.Count > 0)
            codigoTipoAssociacao = int.Parse(dt.Rows[0]["CodigoTipoAssociacao"].ToString());
    }

    private void carregaLabels()
    {
        hfGeral.Set("labelAcoesSugeridasEstrategia", "Ações Sugeridas");
        hfGeral.Set("labelAcaoSugeridaEstrategia", "Ação Sugerida");

        DataTable dt = cDados.getParametrosSistema("labelAcoesSugeridasEstrategia", "labelAcaoSugeridaEstrategia").Tables[0];
        if (dt.Rows.Count > 0)
        {
            labelPlural = dt.Rows[0]["labelAcoesSugeridasEstrategia"].ToString();
            labelSingular = dt.Rows[0]["labelAcaoSugeridaEstrategia"].ToString();

            hfGeral.Set("labelAcoesSugeridasEstrategia", labelPlural);
            hfGeral.Set("labelAcaoSugeridaEstrategia", labelSingular);
        }

        lblDescricao.Text = labelSingular + ":";
        gvDados.Columns[1].Caption = labelPlural;
    }

    #endregion

    #region GRIDVIEW

    private void carregarGrid()
    {
        DataSet ds = cDados.getAcoesSugeridas(codigoObjetivoEstrategico.ToString());

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        //if (e.ButtonID == "btnIncluirCustom")
        //{
        //    if (podeIncluir)
        //    {
        //        e.Enabled = true;
        //    }
        //    else
        //    {
        //        e.Enabled = false;
        //        e.Text = "";
        //        e.Image.Url = "~/imagens/botoes/incluirRegDes.png";
        //    }
        //}

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
                e.Text = "Detalhes";
                e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
            }
        }
    }

    #endregion

    #region CALLBACK

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Incluir")       mensagemErro_Persistencia = persisteInclusaoRegistro();
        else if (e.Parameter == "Editar")   mensagemErro_Persistencia = persisteEdicaoRegistro();
        else if (e.Parameter == "Excluir")  mensagemErro_Persistencia = persisteExclusaoRegistro();

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);


        gvDados.ClientVisible = true;
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        //Aqui estou declarando e inicializando as variaveis que irei usar
        string msg = "";
        string descricao = txtDescricao.Text.Replace("'", "''");
        string memoTexto = htmlTexto.Html.Replace("'", "''");
        try
        {
            //Insere a Unidade Negocio com NivelHierarquico como 0 e EstruturaHierarquica com 0 pois será feito um "scope_identity()" para poder montar o Nivel e a Estrutura
            if (cDados.incluiAcoesSugeridas(descricao, memoTexto, idUsuarioLogado.ToString(), codigoTipoAssociacao.ToString(), codigoObjetivoEstrategico.ToString(), codigoUnidadeLogada.ToString()))
            {
                carregarGrid();
                gvDados.ClientVisible = false;
            }
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
    {   // busca a chave primaria
        string chave = getChavePrimaria();

        string msg = "";
        string descricao = txtDescricao.Text.Replace("'", "''");
        string memoTexto = htmlTexto.Html.Replace("'", "''");

        try
        {
            if (cDados.atualizaAcoesSugeridas(descricao, memoTexto, codigoTipoAssociacao.ToString()
                                              , codigoObjetivoEstrategico.ToString(), chave))
            {
                carregarGrid();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
                gvDados.ClientVisible = false;
            }
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

        cDados.excluiAcoesSugeridas(idUsuarioLogado.ToString(), chave);
        carregarGrid();
        //gvDados.ClientVisible = false;

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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AcaoSugObjEst");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "Click_NovaAcaoSugerida();", true, true, false, "AcaoSugObjEst", "Ação Sugerida", this);
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
}
