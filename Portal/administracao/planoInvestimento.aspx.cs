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
using System.Diagnostics;
using DevExpress.XtraPrinting;
using System.IO;

public partial class administracao_planoInvestimento : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

    #region eventos da página
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

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AdmPlanoInv");
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }

        cDados.aplicaEstiloVisual(Page);
        carregaGvDados();

        if (!IsPostBack)
        {
            populaDldStatus();

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }
    #endregion

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/planoInvestimento.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "planoInvestimento", "_Strings"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 340;
    }
    
    private void populaDldStatus()
    {
        //ddlStatus.DataSource =
        string comandoSQL = string.Format(@"SELECT CodigoStatusPlanoInvestimento
                                  ,DescricaoStatusPlanoInvestimento
                              FROM {0}.{1}.pbh_StatusPlanoInvestimento", cDados.getDbName(), cDados.getDbOwner());
        ddlStatus.TextField = "DescricaoStatusPlanoInvestimento";
        ddlStatus.ValueField = "CodigoStatusPlanoInvestimento";

        DataSet dsRetorno = cDados.getDataSet(comandoSQL);
        ddlStatus.DataSource = dsRetorno.Tables[0];
        ddlStatus.DataBind();
    }

    #endregion

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getPlanoInvestimento("");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        bool podeEditar1 = true;
        bool podeExcluir1 = true;

        string statusPlanoInvestimento = "";
        if (e.VisibleIndex > -1 && gvDados.GetRowValues(e.VisibleIndex, "CodigoStatusPlanoInvestimento") != null)
        {
            statusPlanoInvestimento = gvDados.GetRowValues(e.VisibleIndex, "CodigoStatusPlanoInvestimento").ToString();
        }

        if (statusPlanoInvestimento != "" && statusPlanoInvestimento == "5")
        {
            //bloqueie qualquer edição (inclusive exclusão) quando o status do plano de investimento for igual a 5.
            podeEditar1 = false;
            podeExcluir1 = false;
        }

        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                if (podeEditar1 == true)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
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
                if (podeExcluir1 == true)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }
   
    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

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
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
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
        int codigoPlanoInvestimento = -1;

        string descricaoPlanoInvestimento = txtDescricaoPlanoInvestimento.Text;
        string mensagemErro = "";

        DataSet ds = cDados.getPlanoInvestimento(string.Format(" where pi.Ano = '{0}'", txtAno.Text));

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return "Ano do Plano de Investimento já existente!";

        bool result = cDados.incluiPlanoInvestimento(descricaoPlanoInvestimento, int.Parse(txtAno.Text), dteInicio.Text, dteFinal.Text, 1, ref codigoPlanoInvestimento, ref mensagemErro);


        if (result == false)
        {
            return mensagemErro;
        }
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoPlanoInvestimento = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        string descricaoPlanoInvestimento = txtDescricaoPlanoInvestimento.Text;

        DataSet ds = cDados.getPlanoInvestimento(string.Format(" AND pi.DescricaoPlanoInvestimento = '{0}' AND pi.CodigoPlanoInvestimento <> {1}", descricaoPlanoInvestimento, codigoPlanoInvestimento));

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return "Plano de investimento já existente!";

        bool result = cDados.atualizaPlanoInvestimento(codigoPlanoInvestimento, descricaoPlanoInvestimento,int.Parse(txtAno.Text),dteInicio.Text, dteFinal.Text,int.Parse(ddlStatus.Value.ToString()), ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoPlanoInvestimento = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        bool result = cDados.excluiPlanoInvestimento(codigoPlanoInvestimento, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return mensagemErro;
        }
    }

    #endregion
    
    #region OPÇÕES DE EXPORTAÇÃO

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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadPnlInv");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadPnlInv", lblTituloTela.Text, this);
    }

    #endregion
}
