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
using System.Text;
using System.Collections.Specialized;


public partial class _Portfolios_Administracao_riscosPadroes : System.Web.UI.Page
{
    dados cDados;
    private string nomeTabelaDb = "RiscoPadrao";
    private string whereUpdateDelete;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";
    
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
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "PO_CadTipRscPdr");
        }

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "PO_CadTipRscPdr"))
        {
            podeIncluir = true;
        }
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

        populaGrid();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        ASPxWebControl.RegisterBaseScript(Page);
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));       
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/riscosPadroes.js""></script>"));
        this.TH(this.TS("barraNavegacao", "riscosPadroes"));
    }

    /*private void MenuUsuarioLogado()
    {
        BarraNavegacao1.MostrarInclusao = false;
        BarraNavegacao1.MostrarEdicao = false;
        BarraNavegacao1.MostrarExclusao = false;

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "ADMPMTPTF"))
        {
            BarraNavegacao1.MostrarInclusao = true;
            BarraNavegacao1.MostrarEdicao = true;
            BarraNavegacao1.MostrarExclusao = true;
        }
    }*/

    #endregion

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        string where = " AND CodigoEntidade = '" + CodigoEntidade + "'";
        DataSet ds = cDados.getRiscosPadroes(where);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
        //string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
        //captionGrid += string.Format(@"<td align=""left""><img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/></td>");
        //captionGrid += string.Format(@"</tr></table>");
        //gvDados.SettingsText.Title = captionGrid;
        //gvDados.column0.headertemplate.aspximage
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvDados.Settings.VerticalScrollableHeight = altura - 215;
        gvDados.Width = new Unit((largura - 10) + "px");
    }

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DescricaoRiscoPadrao", txtRisco.Text);
        oDadosFormulario.Add("DescricaoImpactoAlto", mmDescricaoImpactoAlto.Text);
        oDadosFormulario.Add("DescricaoImpactoMedio", mmDescricaoImpactoMedio.Text);
        oDadosFormulario.Add("DescricaoImpactoBaixo", mmDescricaoImpactoBaixo.Text);
        oDadosFormulario.Add("DescricaoProbabilidadeAlta", mmDescricaoProbabilidadeAlta.Text);
        oDadosFormulario.Add("DescricaoProbabilidadeMedia", mmDescricaoProbabilidadeMedia.Text);
        oDadosFormulario.Add("DescricaoProbabilidadeBaixa", mmDescricaoProbabilidadeBaixa.Text);

        oDadosFormulario.Add("CodigoUsuarioInclusao", idUsuarioLogado);
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        oDadosFormulario.Add("DataInclusao", DateTime.Now.Date.ToString());
        return oDadosFormulario;
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "PO_CadTipRscPdr"))
        {
            podeIncluir = true;
        }
        else
        {
            if (e.ButtonID == btnEditar.ID)
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            else if (e.ButtonID == btnExcluir.ID)
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
        }

        if (e.ButtonID == btnEditar.ID || e.ButtonID == btnExcluir.ID)
        {
            e.Enabled = podeIncluir;
        }
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
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
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }


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
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario();

            int novoCodigo = cDados.insert(nomeTabelaDb, oDadosFormulario, true);
            populaGrid();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(novoCodigo);
            gvDados.ClientVisible = false;
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario();

            oDadosFormulario.Remove("DataInclusao");
            cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Exclusão do registro
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

    #endregion
    
    
}
