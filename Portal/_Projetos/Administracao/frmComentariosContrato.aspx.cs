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

public partial class administracao_frmAditivosContrato : System.Web.UI.Page
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
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncCom");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcCom");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltCom");

            if ((false == podeIncluir) && (false == podeEditar) && (false == podeExcluir))
                somenteLeitura = "S";
        }
        if (!IsPostBack)
        {
            carregaGvDados();           
        }
        DataSet ds = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, " and cont.CodigoContrato = " + codigoContrato);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtNumeroContrato.Text = ds.Tables[0].Rows[0]["NumeroContrato"].ToString();
            txtTipoContrato.Text = ds.Tables[0].Rows[0]["DescricaoTipoContrato"].ToString();
            txtStatusContrato.Text = ds.Tables[0].Rows[0]["DescricaoStatusComplementarContrato"].ToString().Trim() == "" ? ((ds.Tables[0].Rows[0]["StatusContrato"].ToString().ToUpper().Trim() == "A") ? "Ativo" : "Inativo") : ds.Tables[0].Rows[0]["DescricaoStatusComplementarContrato"].ToString().Trim();
            txtInicioVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataInicio"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataInicio"].ToString()).ToString("dd/MM/yyyy");
            txtTerminoVigencia.Text = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DataTermino"].ToString()) ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["DataTermino"].ToString()).ToString("dd/MM/yyyy");

        }
        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }    

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));

    }
       
    #endregion
       
    #region GRID

    private void carregaGvDados()
    {
        DataSet dsAditivo = cDados.getComentariosContratos(" AND cc.CodigoContrato = " + codigoContrato);

        if ((cDados.DataSetOk(dsAditivo)))
        {
            gvDados.DataSource = dsAditivo;
            gvDados.DataBind();
        }        
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

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {               
        string comentarios = txtComentario.Text;

        bool result = cDados.incluirComentarioContrato(codigoContrato, comentarios, codigoUsuarioResponsavel);

        if (result == false)
            return "Erro ao incluir comentário!";
        else
        {
            carregaGvDados();
            return "";
        }

    }
    
    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoComentario = int.Parse(getChavePrimaria());

        string comentarios = txtComentario.Text;

        bool result = cDados.atualizaComentarioContrato(codigoComentario, comentarios);

        if (result == false)
            return "Erro ao atualizar comentário!";
        else
        {
            carregaGvDados();
            return "";
        }    
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoComentario = int.Parse(getChavePrimaria());

        bool result = cDados.excluiComentarioContrato(codigoComentario); ;

        if (result == false)
            return "Erro ao excluir comentário!";
        else
        {
            carregaGvDados();
            return "";
        }

    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "Comentario")
        {
            if (e.CellValue.ToString().Length > 125)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 125) + "...";
            }
        }
    }
}
