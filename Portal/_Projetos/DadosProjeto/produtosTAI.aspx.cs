using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using DevExpress.Web;
using System.Data;

public partial class _Projetos_DadosProjeto_produtosTAI : System.Web.UI.Page
{
    private string _connectionString;
    /// <summary>
    /// String de conexão para acesso a dados da tela.
    /// </summary>
    public string ConnectionString
    {
        get { return _connectionString; }
        private set
        {
            sdsTipoSituacaoProduto.ConnectionString = 
            _connectionString = value;
        }
    }
    /// <summary>
    /// Indica se está sendo criado um novo projeto (codigoProjeto == -1).
    /// </summary>
    public bool IndicaNovoProjeto
    {
        get
        {
            return _codigoProjeto == -1;
        }
    }

    private int _codigoProjeto;
    /// <summary>
    /// Código do projeto vinculado a proposta de iniciativa. Caso seja igual a '-1' trata-se de uma nova proposta de iniciativa
    /// </summary>
    public int CodigoProjeto
    {
        get { return _codigoProjeto; }
        set
        {
            cDados.setInfoSistema("CodigoProjeto", value); // variável de sessão usada no fluxo
            Session["codigoProjeto"] = value;
            _codigoProjeto = value;
        }
    }

    private int _codigoEntidadeUsuarioResponsavel;
    /// <summary>
    /// Código da entidade do usuário logado
    /// </summary>
    public int CodigoEntidadeUsuarioResponsavel
    {
        get { return _codigoEntidadeUsuarioResponsavel; }
        set
        {
            cDados.setInfoSistema("CodigoEntidade", value);
            Session["codigoEntidade"] = value;
            _codigoEntidadeUsuarioResponsavel = value;
        }
    }

    private int _codigoUsuarioResponsavel;
    /// <summary>
    /// Código do usuário logado
    /// </summary>
    public int CodigoUsuarioResponsavel
    {
        get { return _codigoUsuarioResponsavel; }
        set
        {
            cDados.setInfoSistema("IDUsuarioLogado", value);
            Session["codigoUsuario"] = value;
            _codigoUsuarioResponsavel = value;
        }
    }

    private bool readOnly;
    private bool podeEditar;
    private dados cDados;
    private string Atrasados;

    public string alturaTela = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //TODO: Descomentar esse código

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
         
        if (!IsPostBack)
        {
            int alturaInt;
            if (int.TryParse(Request.QueryString["Altura"], out alturaInt))
                alturaTela = (alturaInt - 113).ToString();
            if (!string.IsNullOrEmpty(alturaTela))
            {
                dv01.Style.Add("height", alturaTela + "px");
                dv01.Style.Add("overflow", "auto");
            }
        }

        string cp = Request.QueryString["idProjeto"];
        CodigoProjeto = int.Parse(string.IsNullOrEmpty(cp) ? "-1" : cp);
        readOnly = "S".Equals(Request.QueryString["RO"]);
        podeEditar = !readOnly;
        Atrasados = (Request.QueryString["Atrasados"] != null) ? Request.QueryString["Atrasados"] + "" : "null";
        //existemAlteracoesNaoSalvas = true;
        CodigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        CodigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);

        hfGeral.Set("CodigoProjeto", CodigoProjeto);

        //Obtem a string de conexao e seta a propriedade 'ConnectionString' dos SQL Data Sources da página.
        ConnectionString = cDados.classeDados.getStringConexao();
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        gvProdutosAcao.DataSource = getProdutosTAI();
        gvProdutosAcao.DataBind();
    }

    private string getChavePrimaria() // retorna a primary key da tabela
    {
        if (gvProdutosAcao.FocusedRowIndex >= 0)
            return gvProdutosAcao.GetRowValues(gvProdutosAcao.FocusedRowIndex, gvProdutosAcao.KeyFieldName).ToString();
        else
            return "";
    }

    public DataSet getProdutosTAI()
    {
        string comandoSQL = "";
        if (Atrasados == "S")
        {
            comandoSQL = string.Format(@"SELECT * FROM tai_ProdutosAcoesIniciativa 
                                         WHERE CodigoProjeto = {0} 
                                         AND DataLimitePrevista < getdate()
                                         AND DataReal is null
                                         AND ISNULL(CodigoSituacaoProduto,0) NOT IN(1,2)", CodigoProjeto);
        }
        else
        {
            comandoSQL = string.Format(@"SELECT * FROM tai_ProdutosAcoesIniciativa 
                                            WHERE CodigoProjeto = {0}", CodigoProjeto);
        }
        return cDados.getDataSet(comandoSQL);

    }

    public bool atualizaProdutosTAI(string codigoSituacaoProduto, string dataReal, int sequenciaRegistro, ref string mensagemErro)
    {
        bool retorno = false;
        string comandoSQL = "";
        try
        {
            comandoSQL = string.Format(@"
            UPDATE {0}.{1}.tai_ProdutosAcoesIniciativa
            SET CodigoSituacaoProduto = {2}
                            ,DataReal = {3}
                  WHERE CodigoProjeto = {4} 
                AND SequenciaRegistro = {5}", cDados.getDbName(), cDados.getDbOwner(), codigoSituacaoProduto,
                                            ( dataReal=="null" ? "DataReal" : "convert(datetime,'" + dataReal + "',103)"), CodigoProjeto, sequenciaRegistro);
            int regAfetados = 0;
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
        }
        catch(Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;

    }



    //protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    //{
    //    ASPxGridView grid = (ASPxGridView)sender;
    //    switch (e.ButtonType)
    //    {
    //        case ColumnCommandButtonType.Edit:
    //            if (!podeEditar)
    //            {
    //                e.Enabled = false;
    //                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}


    protected void gvProdutosAcao_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }
    protected void gvProdutosAcao_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string codigoSituacaoProduto = e.NewValues["CodigoSituacaoProduto"] != null ? e.NewValues["CodigoSituacaoProduto"].ToString() : "null";
        string dataReal = e.NewValues["DataReal"] != null ? e.NewValues["DataReal"].ToString() : "null";
        //int seqRegistro = int.Parse(e.NewValues["SequenciaRegistro"].ToString());
        int seqRegistro = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        bool retorno = atualizaProdutosTAI(codigoSituacaoProduto, dataReal, seqRegistro, ref mensagemErro);
        if (mensagemErro != "")
        {
            throw new Exception(mensagemErro);
        }
        else
        {
            if (retorno == true)
            {
                gvProdutosAcao.JSProperties["cp_Retorno"] = "S";
                e.Cancel = true;
                gvProdutosAcao.CancelEdit();
                gvProdutosAcao.DataSource = getProdutosTAI();
                gvProdutosAcao.DataBind();
            }
            
        }

    }
}