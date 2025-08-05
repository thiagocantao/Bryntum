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
using System.Collections.Specialized;

public partial class _Portfolios_Administracao_Configuracoes : System.Web.UI.Page
{
    dados cDados;

    private string whereUpdateDelete;
    private string nomeTabelaDb = "PeriodoAnalisePortfolio";
    private string resolucaoCliente = "";
    private string codigoUsuarioResponsavel = "";
    private string codigoEntidadeUsuarioResponsavel = "";

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = cDados.getInfoSistema("IDUsuarioLogado").ToString();
        codigoEntidadeUsuarioResponsavel = cDados.getInfoSistema("CodigoEntidade").ToString();

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, int.Parse(codigoUsuarioResponsavel), int.Parse(codigoEntidadeUsuarioResponsavel), int.Parse(codigoEntidadeUsuarioResponsavel), "null", "EN", 0, "null", "PO_CnsPmt");
        }
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria() + " AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel;

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);

            MenuUsuarioLogado();
        }
        if (cDados.VerificaPermissaoUsuario(int.Parse(codigoUsuarioResponsavel), int.Parse(codigoEntidadeUsuarioResponsavel), "PO_AdmPmt"))
        {
            podeIncluir = true;
        }

        carregaGrid(codigoEntidadeUsuarioResponsavel);
        populaDdlTipoEdicao();
        //---- FALTA TRADUÇÃO DA TELA ----//


        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            //Master.geraRastroSite();
        }
        cDados.aplicaEstiloVisual(this);
    }

    private void populaDdlTipoEdicao()
    {
        string comandoSQL = string.Format(@"select CodigoPeriodicidade, 
                                           DescricaoPeriodicidade_PT from TipoPeriodicidade where IntervaloMeses between 1 and 12 order by 2 asc");

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            ddlTipoEdicao.TextField = "DescricaoPeriodicidade_PT";
            ddlTipoEdicao.ValueField = "CodigoPeriodicidade";
            ddlTipoEdicao.DataSource = ds.Tables[0];
            ddlTipoEdicao.DataBind();
        }

    }

    #region VARIOS

    private void HeaderOnTela()
    {
        //A pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        //Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Configuracoes.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "Configuracoes"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Portfólios</title>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 205);
        gvDados.Settings.VerticalScrollableHeight = altura - 120;
    }

    private void MenuUsuarioLogado()
    {

    }

    #endregion

    #region ComboBox's



    #endregion

    #region Grid's

    private void carregaGrid(string codigoEntidadeUsuarioResponsavel)
    {
        DataSet ds = cDados.getGridPeriodicidade(codigoEntidadeUsuarioResponsavel);


        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();
        //gvDados.FocusedRowIndex = 0;

        string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
        captionGrid += string.Format(@"<td align=""left"">
                                       <img src=""../../imagens/botoes/incluirReg02.png"" 
                                        alt=""Novo"" 
                                        onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";
                                        TipoOperacao = 'Incluir')"" 
                                        style=""cursor: pointer;""/>
                                        </td>");
        captionGrid += string.Format(@"</tr></table>");
        gvDados.SettingsText.Title = captionGrid;
    }

    #endregion

    // Método responsável por obter os valores que estão preenchidos no formulário
    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("CodigoEntidade", codigoEntidadeUsuarioResponsavel);
        oDadosFormulario.Add("Ano", txtAno.Text);
        oDadosFormulario.Add("IndicaAnoPeriodoEditavel", (ddlEditavel.Value.ToString() == "-1") ? "null" : ddlEditavel.Value.ToString());
        oDadosFormulario.Add("IndicaTipoDetalheEdicao", (ddlTipoEdicao.Value == null) ? "null" : ddlTipoEdicao.Value.ToString());
        oDadosFormulario.Add("IndicaAnoAtivo", (ddlAnoAtivo.Value == null) ? "null" : ddlAnoAtivo.Value.ToString());
        oDadosFormulario.Add("IndicaMetaEditavel", (ddlMetaEditavel.Value == null) ? "null" : ddlMetaEditavel.Value.ToString());
        oDadosFormulario.Add("IndicaResultadoEditavel", (ddlResultadoEditavel.Value == null) ? "null" : ddlResultadoEditavel.Value.ToString());

        return oDadosFormulario;
    }

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string getAno()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, "Ano").ToString();
        else
            return "";
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = "";
        if (e.Parameter.Equals("Incluir"))
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        if (e.Parameter.Equals("Editar"))
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        if (e.Parameter.Equals("Excluir"))
            mensagemErro_Persistencia = persisteExclusaoRegistro();

        if (mensagemErro_Persistencia.Equals(""))
        {
            hfGeral.Set("StatusSalvar", "1"); //-- 1: Sucesso.
            ((ASPxCallbackPanel)sender).JSProperties["cp_OperacaoOk"] = e.Parameter;
        }

        else
        {
            ((ASPxCallbackPanel)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
        }
    }

    private string persisteInclusaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            //ListDictionary oDadosFormulario = getDadosFormulario();

            //codigoEntidadeUsuarioResponsavel
            string ano = txtAno.Text;

            string anoPeriodoEditavel = (ddlEditavel.Value == null) ? "null" : ddlEditavel.Value.ToString();

            string tipoEdicao = (ddlTipoEdicao.Value == null) ? "null" : ddlTipoEdicao.Value.ToString();
            string tipo = tipoEdicao;


            string mesgError = "";

            //cDados.insert(nomeTabelaDb, oDadosFormulario, false);
            bool result = cDados.incluiConfiguracoes(codigoEntidadeUsuarioResponsavel.ToString(), ano, anoPeriodoEditavel, tipo, ddlAnoAtivo.SelectedItem.Value.ToString(), ddlMetaEditavel.SelectedItem.Value.ToString(), ddlResultadoEditavel.SelectedItem.Value.ToString(), ref mesgError);

            if (result)
            {
                carregaGrid(codigoEntidadeUsuarioResponsavel);
                //gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(txtAno.Text);
                //gvDados.ClientVisible = false;
                return "";
            }
            else
            {
                // if (mesgError.Contains("UQ_Projeto_NomeProjeto")) Personaliçar o mensagem do ERROR
                //     return "Nome do Projeto já Existe!";          ....
                // else                                              ....
                return mesgError;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        int codigoConfiguracao = 0;
        try
        {
            codigoConfiguracao = int.Parse(getChavePrimaria());

            // Lê as informações disponíveis no formulário
            //ListDictionary oDadosFormulario = getDadosFormulario();

            //codigoEntidadeUsuarioResponsavel
            string ano = txtAno.Text;
            string anoPeriodoEditavel = (ddlEditavel.Value.ToString() == "-1") ? "null" : ddlEditavel.Value.ToString();
            string tipoEdicao = (ddlTipoEdicao.Value == null) ? "null" : ddlTipoEdicao.Value.ToString();
            string tipo = tipoEdicao;

            string mesgError = "";

            //oDadosFormulario.Remove("CodigoEntidade");
            //cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
            bool result = cDados.atualizaConfiguracoes(codigoEntidadeUsuarioResponsavel, ano, anoPeriodoEditavel.ToString().Substring(0, 1), tipo, ddlAnoAtivo.SelectedItem.Value.ToString(), ddlMetaEditavel.SelectedItem.Value.ToString(), ddlResultadoEditavel.SelectedItem.Value.ToString(), ref mesgError);
            if (result)
            {
                carregaGrid(codigoEntidadeUsuarioResponsavel);
                //gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoConfiguracao);
                //gvDados.ClientVisible = false;
                return "";
            }
            else
            {
                // if (mesgError.Contains("UQ_Projeto_NomeProjeto")) Personaliçar o mensagem do ERROR
                //     return "Nome do Projeto já Existe!";          ....
                // else                                              ....
                return mesgError;
            }
        }
        catch (Exception ex)
        {
            gvDados.ClientVisible = false;
            return ex.Message;
        }
    }

    // Método responsável pela Exclusão do registro
    private string persisteExclusaoRegistro()
    {
        try
        {
            cDados.delete(nomeTabelaDb, whereUpdateDelete);
            carregaGrid(codigoEntidadeUsuarioResponsavel);
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
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

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ParamPort");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "ParamPort", lblTituloTela.Text, this);
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
