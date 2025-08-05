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

public partial class _Projetos_Administracao_DadosIndicadoresOperacional : System.Web.UI.Page
{
    DataSet dset;
    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool podeIncluir = false;

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());  //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "PR_CadDadInd");
        }

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        defineAlturaTela(resolucaoCliente);

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            carregaGrid();
            carregaComboUnidadeMedida();
            carregaComboAgrupamentos();
            //MenuUsuarioLogado();
        }

        
        podeIncluir = true;
        

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region Grid's

    private void carregaGrid()
    {
        dset = cDados.getDadoOperacional(CodigoEntidade);

        if (cDados.DataSetOk(dset))
        {
            gvDados.DataSource = dset;
            gvDados.DataBind();
        }

        string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
        captionGrid += string.Format(@"<td align=""left""><img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/></td>");
        captionGrid += string.Format(@"</tr></table>");
        gvDados.SettingsText.Title = captionGrid;


    }

    #endregion

    #region COMBOBOX

    private void carregaComboUnidadeMedida()
    {
        dset = cDados.getUnidadeMedida();

        if (cDados.DataSetOk(dset) && cDados.DataTableOk(dset.Tables[0]))
            cDados.PopulaDropDownASPx(this, dset.Tables[0], "CodigoUnidadeMedida", "SiglaUnidadeMedida", "", ref cmbUnidadeDeMedida);
    }

    private void carregaComboAgrupamentos()
    {
        dset = cDados.getAgrupamentoFuncao();

        if (cDados.DataSetOk(dset) && cDados.DataTableOk(dset.Tables[0]))
            cDados.PopulaDropDownASPx(this, dset.Tables[0], "CodigoFuncao", "NomeFuncao", "", ref cmbAgrupamentoDoDado);
    }

    #endregion

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        alturaPrincipal = altura;
        gvDados.Settings.VerticalScrollableHeight = altura - 215;
        gvDados.Width = new Unit((largura - 10) + "px");
        heGlossario.Height = new Unit((altura - 315) + "px");

    }

    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoDado").ToString();

        return codigoDado;
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/DadosIndicadoresOperacional.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "DadosIndicadoresOperacional"));
        /*string comando = string.Format(@"<script type='text/javascript'>onloadDesabilitaBarraNavegacao();</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);*/
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

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
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoUsuario = 0;
        string msg = "";

        //Coletar dados...
        string descricaoDado = txtDado.Text.Replace("'", "''");
        string glossario = heGlossario.Html;
        string unidadeMedida = cmbUnidadeDeMedida.Value.ToString();
        string casasDecimais = cmbCasasDecimais.Value.ToString();
        string agrupamentoDado = cmbAgrupamentoDoDado.Value.ToString();
        string CodigoReservado = txtCodigoReservado.Text == "" ? "NULL" : "'" + txtCodigoReservado.Text + "'";

        //idUsuarioLogado, CodigoEntidade

        try
        {
            DataSet ds = cDados.incluiDadoIndicadoresOperacional(descricaoDado, glossario, unidadeMedida, casasDecimais,
                                                                 agrupamentoDado, idUsuarioLogado.ToString(),
                                                                 CodigoEntidade.ToString(), CodigoReservado);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                codigoUsuario = int.Parse(ds.Tables[0].Rows[0]["codigoDado"].ToString());
                carregaGrid();
                gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUsuario);
                gvDados.ClientVisible = false;
            }
            msg = "";
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
        // busca a chave primaria
        int codigoUsuario = int.Parse(getChavePrimaria());
        //Coletar dados...
        string descricaoDado = txtDado.Text.Replace("'", "''");
        string glossario = heGlossario.Html;
        string unidadeMedida = cmbUnidadeDeMedida.Value.ToString();
        string casasDecimais = cmbCasasDecimais.Value.ToString();
        string agrupamentoDado = cmbAgrupamentoDoDado.Value.ToString();
        //idUsuarioLogado, CodigoEntidade
        string CodigoReservado = txtCodigoReservado.Text == "" ? "NULL" : "'" + txtCodigoReservado.Text + "'";


        cDados.atualizaDadoIndicadoresOperacional(descricaoDado, glossario, unidadeMedida, casasDecimais,
                                                  agrupamentoDado, codigoUsuario.ToString(), CodigoReservado);
        carregaGrid();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoUsuario);
        gvDados.ClientVisible = false;

        return "";
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        // busca a chave primaria
        string chave = getChavePrimaria();

        cDados.excluiDadoIndicadoresOperacional(chave, idUsuarioLogado.ToString());

        carregaGrid();

        return "";
    }

    #endregion

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {

        if (e.ButtonID == "btnEditar")
        {
            if (podeIncluir)
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
            if (podeIncluir)
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
}
