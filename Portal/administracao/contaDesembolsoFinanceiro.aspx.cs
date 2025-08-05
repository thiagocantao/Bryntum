using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class administracao_contaDesembolsoFinanceiro : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

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

        //podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
        //    codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_EdtRamAtv");
        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AdmDesembFin");

        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }
        populaCategoria();
        if (!IsPostBack)
        {
            carregaGvDados();
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            //Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/contaDesembolsoFinanceiro.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "contaDesembolsoFinanceiro", "_Strings"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 275;
        gvDados.Width = new Unit((largura - 10) + "px");
    }
    #endregion

    #region GRID

    private void carregaGvDados()
    {

        string comandoSQL = string.Format(@"
        SELECT cdf.CodigoConta
              ,cdf.DescricaoConta
              ,cdf.IndicaInvestimentoFinanciavel
              ,cdf.GrupoConta
              ,cdf.CodigoEntidade
              ,cdf.CodigoCategoria
              ,ca.DescricaoCategoria
        FROM {0}.{1}.uhe_ContaDesembolsoFinanceiro cdf
  INNER JOIN {0}.{1}.Categoria ca on (ca.CodigoCategoria = cdf.CodigoCategoria)
       WHERE cdf.CodigoEntidade = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);


        DataSet ds = cDados.getDataSet(comandoSQL);
        
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

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
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

    private void populaCategoria()
    {
        DataSet dsCategoria = cDados.getCategoriasEntidade(codigoEntidadeUsuarioResponsavel, "");

        ddlCategoria.DataSource = dsCategoria.Tables[0];

        ddlCategoria.ValueField = "CodigoCategoria";
        ddlCategoria.TextField = "DescricaoCategoria";
        ddlCategoria.DataBind();

    }


    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {

        int codigoCategoria = (ddlCategoria.Value == null)?  -1 : int.Parse(ddlCategoria.Value.ToString());
        
        string mensagemErro = "";

        bool result = cDados.incluiContaDesembolsoFinanceiro(txtNomeConta.Text, (ckbFinanciavel.Checked == true) ? 'S' : 'N', txtNomeGrupo.Text, codigoEntidadeUsuarioResponsavel, codigoCategoria, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoConta = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        char indicaInvestimentoFinanciavel = (ckbFinanciavel.Checked == true )? 'S':'N';
        int codigoCategoria = (ddlCategoria.Value == null)?  -1 : int.Parse(ddlCategoria.Value.ToString());

        bool result = cDados.atualizaContaDesembolsoFinanceiro(txtNomeConta.Text, indicaInvestimentoFinanciavel, txtNomeGrupo.Text, codigoEntidadeUsuarioResponsavel, codigoCategoria, codigoConta, ref mensagemErro);

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
        int codigoDesembolsoFinanceiro = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        //if (cDados.verificaExclusaoTipoRamoAtividade(codigoRamoAtividade) == false)
        //{
        //    return "O ramo de atividade não pode ser excluído. Existem fornecedores ou clientes cadastrados nele.";
        //}
        //else
        //{

        bool result = cDados.excluiContaDesembolsoFinanceiro(codigoDesembolsoFinanceiro, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }
        //}
    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }
   
}