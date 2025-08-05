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
using System.Collections.Specialized;
using DevExpress.Web;

public partial class _Projetos_Administracao_assuntosLicoesAprendidas : System.Web.UI.Page
{
    dados cDados;
    private string nomeTabelaDb = "TipoAssuntoLicaoAprendida";
    private string whereUpdateDelete;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;

    public bool podeEditar = false;
    public bool podeIncluir = false;
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
        

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "CadAsnLic");
        }

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "CadAsnLic"))
            podeIncluir = true;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        //if (!IsPostBack)
        cDados.aplicaEstiloVisual(Page);
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/assuntosLicoesAprendidas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "assuntosLicoesAprendidas"));

    }

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        DataSet ds = cDados.getAssuntoLicaoAprendida(CodigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        alturaPrincipal = altura;
        gvDados.Settings.VerticalScrollableHeight = altura - 280;

    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {
            // alguma coisa deu errado...
            if (mensagemErro_Persistencia.Contains("REFERENCE"))
                mensagemErro_Persistencia = Resources.traducao.assuntosLicoesAprendidas_o_dado_que_se_tenta_excluir_est__sendo_usado_por_outra_tela_do_sistema_;
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
         populaGrid();
    }

    private string persisteExclusaoRegistro()
    {
        cDados.delete(nomeTabelaDb, whereUpdateDelete);
        return "";
    }

    private string persisteEdicaoRegistro()
    {

        cDados.update(nomeTabelaDb, getDadosFormulario(), whereUpdateDelete);
        return "";
    }

    private string persisteInclusaoRegistro()
    {

        cDados.insert(nomeTabelaDb, getDadosFormulario(), false);
        return "";

    }

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        //  oDadosFormulario.Add("DescricaoRiscoPadrao", txtRisco.Text);
        oDadosFormulario.Add("DescricaoAssuntoLicaoAprendida", txtAssunto.Text);
        oDadosFormulario.Add("IndicaControladoSistema", "S");
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);

        return oDadosFormulario;
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "CadAsnLic"))
            podeEditar = true;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "CadAsnLic"))
            podeExcluir = true;

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

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        if (e.Item.Image.Url != "~/imagens/botoes/btnDownload.png")
        {
            cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AssLicApren");
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "AssLicApren", lblTituloTela.Text, this);
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
