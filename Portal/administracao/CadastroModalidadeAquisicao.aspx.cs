using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class administracao_CadastroModalidadeAquisicao : System.Web.UI.Page
{

    dados cDados;


    private int alturaPrincipal = 0;
    private int idUsuarioLogado;
    private int CodigoEntidade;

    private string dbName;
    private string dbOwner;


    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;


    
    
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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        podeIncluir = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "incluiModAquisic");
        podeEditar = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "alteraModAquisic");
        podeExcluir = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "excluiModAquisic");

        this.Title = cDados.getNomeSistema();
    }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();
        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack)
        {

            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "AcessModalAqui2");



            hfGeral.Set("TipoOperacao", "");
            CDIS_GridLocalizer.Activate();
        }

        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());

        carregaGvDados();
        pnCallback.JSProperties["cp_Mostrar"] = "N";


        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "AcessModalAqui2", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }



    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroModalidadeAquisicao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "CadastroModalidadeAquisicao"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 135;
    }

    private void carregaGvDados()
    {
        DataSet ds =  getModalidadeAquisicao("");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    private DataSet getModalidadeAquisicao(string where)
    {
        string comandoSQL = string.Format(@"
            SELECT [Codigo]
                  ,[Descricao]
                  ,[CodigoEntidade]
             FROM {0}.{1}.ModalidadeAquisicao where CodigoEntidade = {2} {3}
             order by [Descricao]", cDados.getDbName(), cDados.getDbOwner(), CodigoEntidade, where);

        return cDados.getDataSet(comandoSQL);
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadUsu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadUsu", lblTituloTela.Text, this);
    }

    #endregion

    
    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        pnCallback.JSProperties["cp_ErroSalvar"] = "";
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
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
            pnCallback.JSProperties["cp_ErroSalvar"] = mensagemErro_Persistencia;
            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    private string persisteExclusaoRegistro()
    {
        int codigoModalidadeAquisicao = int.Parse(getChavePrimaria());

        string mensagemErro = "";

        bool result = excluiModalidadeAquisicao(codigoModalidadeAquisicao, ref mensagemErro);

        if (mensagemErro != "" || result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private string persisteEdicaoRegistro()
    {
        int codigoModalidadeAquisicao = int.Parse(getChavePrimaria());

        string modalidadeAquisicao = txtDescricaoModalidadeAquisicao.Text;

        string mensagemErro = "";

        bool result = atualizaModalidadeAquisicao(codigoModalidadeAquisicao, modalidadeAquisicao, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteInclusaoRegistro()
    {
        int codigoModalidadeAquisicao = -1;

        string modalidadeAquisicao = txtDescricaoModalidadeAquisicao.Text;

        string mensagemRetorno = "";

        DataSet dsProcuraModalidade = getModalidadeAquisicao(string.Format(" AND Descricao = '{0}' ", modalidadeAquisicao));

        if (cDados.DataSetOk(dsProcuraModalidade) && cDados.DataTableOk(dsProcuraModalidade.Tables[0]))
        {
            mensagemRetorno = "Modalidade de Aquisição já Existente!";
        }           

        bool result = incluiModalidadeAquisicao(modalidadeAquisicao, ref codigoModalidadeAquisicao, ref mensagemRetorno);

        if (result == false || mensagemRetorno != "")
        {
            return mensagemRetorno;
        }
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private bool incluiModalidadeAquisicao(string modalidadeAquisicao, ref int codigoModalidadeAquisicao, ref string mensagemErro)
    {
       bool retorno = false;
        int registrosAfetados = 0;
        string comandoSQL = string.Format(
        @"INSERT INTO {0}.{1}.ModalidadeAquisicao(Descricao, CodigoEntidade)
                                           VALUES('{2}',{3})", cDados.getDbName(), cDados.getDbOwner(), modalidadeAquisicao, CodigoEntidade);
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            retorno = false;
        }
        return retorno;
    }

    private bool excluiModalidadeAquisicao(int codigoModalidadeAquisicao, ref string mensagemErro)
    {
        bool retorno = false;
        int registrosAfetados = 0;
        string comandoSQL = string.Format(
        @"DELETE FROM {0}.{1}.ModalidadeAquisicao
           WHERE Codigo = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoModalidadeAquisicao);
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            retorno = false;
        }
        return retorno;
    }

    private bool atualizaModalidadeAquisicao(int codigoModalidadeAquisicao, string descricao, ref string mensagemErro)
    {
        bool retorno = false;
        int registrosAfetados = 0;
        string comandoSQL = string.Format(
        @"UPDATE {0}.{1}.[ModalidadeAquisicao]
             SET Descricao = '{2}'
           WHERE Codigo = {3}", cDados.getDbName(), cDados.getDbOwner(), descricao, codigoModalidadeAquisicao);
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            retorno = false;
        }
        return retorno;
    }

    #endregion

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
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
        if (e.ButtonID == "btnExcluir")
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
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }
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