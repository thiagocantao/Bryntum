using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_Administracao_frmIndiceReajusteContrato : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

    int codigoContrato = -1;
    public string somenteLeitura = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();
        string IniciaisTipoAssociacao = "CT";

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
           
        }

        cDados.aplicaEstiloVisual(Page);

        if (Request.QueryString["CC"] != null)
            codigoContrato = int.Parse(Request.QueryString["CC"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString()) - 85;
        }

        //somenteLeitura = "S";
        if (somenteLeitura == "S")
        {
            podeEditar = false;
            podeExcluir = false;
            podeIncluir = false;
        }
        else
        {
            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncRjt");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcRjt");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltRjt");
            if ((false == podeIncluir) && (false == podeEditar) && (false == podeExcluir))
                somenteLeitura = "S";
        }
        if (!IsPostBack)
        {
            carregaGvDados();
        }

        gvDados.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
    }

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
    
    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoIndiceReajusteContrato = int.Parse(getChavePrimaria());
        double spPercentualReajuste1 = double.Parse(spPercentualReajuste.Value.ToString());

        bool result = cDados.atualizaIndiceReajusteContrato(codigoIndiceReajusteContrato, txtDescricao.Text, spPercentualReajuste1, dtDataAplicacaoReajuste.Value.ToString(), "N");

        if (result == false)
            return "Erro ao atualizar Reajuste do contrato!";
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteInclusaoRegistro()
    {
        double spPercentualReajuste1 = double.Parse(spPercentualReajuste.Value.ToString());
        string dataAplicacaoReajuste = dtDataAplicacaoReajuste.Value.ToString();
        bool result = cDados.incluiIndiceReajusteContrato(codigoContrato, txtDescricao.Text, spPercentualReajuste1, dataAplicacaoReajuste, "N", codigoUsuarioResponsavel);

        if (result == false)
            return "Erro ao incluir reajuste!";
        else
        {
            carregaGvDados();
            return "";
        }

    }

    private void carregaGvDados()
    {
        string where = "";
        
        DataSet dsReajuste = cDados.getIndiceReajusteContrato(codigoContrato, where);

        if ((cDados.DataSetOk(dsReajuste)))
        {
            gvDados.DataSource = dsReajuste;
            gvDados.DataBind();
        }
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoIndiceReajusteContrato  = int.Parse(getChavePrimaria());

        bool result = cDados.excluiReajusteContrato(codigoIndiceReajusteContrato); ;

        if (result == false)
            return "Erro ao excluir reajuste do contrato!";
        else
        {
            carregaGvDados();
            return "";
        }

    }
    
    #endregion

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));

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
    
    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }
}