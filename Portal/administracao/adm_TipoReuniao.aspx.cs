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

public partial class administracao_adm_TipoReuniao : System.Web.UI.Page
{

    dados cDados;
    private string nomeTabelaDb = "TipoEvento";
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
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_CadTipReu");
        }

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EN_CadTipReu"))
            podeIncluir = true;


        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        populaModuloSistema();
        populaGrid();
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 100;
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_TipoReuniao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "adm_TipoReuniao"));
    }

    #endregion

    #region COMBOBOX

    private void populaModuloSistema()
    {
        DataSet ds = cDados.getModuloSistema();
        if (cDados.DataSetOk(ds))
        {
            ddlModuloSistema.TextField = "DescricaoModuloSistema";
            ddlModuloSistema.ValueField = "CodigoModuloSistema";
            ddlModuloSistema.DataSource = ds.Tables[0];
            ddlModuloSistema.DataBind();
        }
    }

    #endregion

    #region DVGRID

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        DataSet ds = cDados.getTiposEventos(CodigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EN_CadTipReu"))
            podeEditar = true;
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EN_CadTipReu"))
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

    #endregion

    #region CALLBACK's

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
                mensagemErro_Persistencia = Resources.traducao.adm_TipoReuniao_o_dado_que_se_tenta_excluir_est__sendo_usado_por_outra_tela_do_sistema_;
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }

    #endregion

    #region BANCO DE DADOS

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }

    private string persisteExclusaoRegistro()
    {
        try
        {
            cDados.delete(nomeTabelaDb, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        
    }

    //todo: nao deixar salvar ao editar pelo outro nome qeu ja exista no banco.
    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        int codigoTipoReuniao = int.Parse(getChavePrimaria());
        bool noExisteEntidadAtual = cDados.getExisteNomeTipoReuniaoEntidadAtual(CodigoEntidade, txtTipoReuniao.Text.Trim(), codigoTipoReuniao);

        if (noExisteEntidadAtual)
        {

            cDados.update(nomeTabelaDb, getDadosFormulario(), whereUpdateDelete);
            populaGrid();
        }
        else
            retorno = Resources.traducao.adm_TipoReuniao_erro_ao_salvar_o_tipo_de_reuni_o__n_o_pode_existir_descri__o_duplicadas__;

        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        bool noExisteEntidadAtual = cDados.getExisteNomeTipoReuniaoEntidadAtual(CodigoEntidade, txtTipoReuniao.Text.Trim(), -1);

        if (noExisteEntidadAtual)
        {
            cDados.insert(nomeTabelaDb, getDadosFormulario(), false);
            populaGrid();
        }
        else
            retorno = Resources.traducao.adm_TipoReuniao_erro_ao_salvar_o_tipo_de_reuni_o__n_o_pode_existir_descri__o_duplicadas__;

        return retorno;
    }

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DescricaoTipoEvento", txtTipoReuniao.Text);
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        oDadosFormulario.Add("CodigoModuloSistema", ddlModuloSistema.SelectedItem.Value);
        return oDadosFormulario;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadTiposReu");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadTiposReu", lblTituloTela.Text, this);
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
