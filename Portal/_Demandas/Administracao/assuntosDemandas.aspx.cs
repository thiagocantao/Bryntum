/*
 OBSERVAÇÕES
 * 
 * MUDANÇA
 * 10/12/2010 - Alejandro: Não pode gravar la palabra "NULL" no campo [CodigoReservado], mais sim deve gravar
 *                          o valor 'null' caso não seja prenchido.
 * 10/01/2011 - Alejandro: Problema ao guardar no campo [CodigoReservado], foi alterada no dados.cs o método
 *                          public DataSet incluiUnidadeNegocio(...){...}
 * 28/02/2011 - Alejandro: Troca de propiedades do comboBox (ddlGerente, ddlUnidadeSuperior).
 *                          a troca da propiedade ClientValue() por a propiedades Value, SelectedIndex, etc. 
 * 21/03/2011 :: Alejandro : - Adiccionar o controle do botão de Permissão [UN_AdmPrs].
 * 
 */
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
using System.Drawing;

public partial class _Demandas_Administracao_assuntosDemandas : System.Web.UI.Page
{
    dados cDados;
    DataSet ds;

    private int idUsuarioLogado;
    private int codigoEntidade;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeIncluir = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!Page.IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);            
        }

        carregaComboProjetos();
        carregaComboResponsaveis();
        carregaComboFluxos();
        carregaGrid();
        
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 197);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura;
    }

    private void HeaderOnTela()
    {       
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/assuntosDemandas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "assuntosDemandas"));
    }

    #endregion

    #region GRID

    //Carrega grid Principal
    public void carregaGrid()
    {
        string where = "";
        ds = cDados.getAssuntosDemandas(codigoEntidade, where);

        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();
    }   
    
    #endregion
       
    #region CALLBACK'S

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e) // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    {
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            //carregaUnidadeSuperior("IncluirNovo");
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            //carregaUnidadeSuperior("");
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
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }    
   
    #endregion

    #region BANCO DE DADOS.

    private string getChavePrimaria() // retorna a primary key da tabela
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro
    {
        try
        {
            string mesgError = "";
            bool result = cDados.incluiAssuntoDemanda(txtNome.Text, codigoEntidade, int.Parse(ddlGerenteProjeto.Value.ToString())
                , int.Parse(ddlFluxo.Value.ToString()), int.Parse(ddlProjetos.Value.ToString()), txtObservacoes.Text, ref mesgError);

            if (result == false)
            {
                return mesgError;
            }
            else
            {
                carregaGrid();
                gvDados.ClientVisible = true;
                return "";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        try
        {
            int codigoAssunto = int.Parse(getChavePrimaria());
            string mesgError = "";
            bool result = cDados.atualizaAssuntoDemanda(codigoAssunto, txtNome.Text, int.Parse(ddlGerenteProjeto.Value.ToString())
                , int.Parse(ddlFluxo.Value.ToString()), int.Parse(ddlProjetos.Value.ToString()), txtObservacoes.Text, ref mesgError);

            if (result == false)
            {
                return mesgError;
            }
            else
            {
                carregaGrid();
                gvDados.ClientVisible = true;
                return "";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        try
        {
            int codigoAssunto = int.Parse(getChavePrimaria());
            string mesgError = "";

            bool result = cDados.excluiAssuntoDemanda(codigoAssunto, ref mesgError);

            if (result == false)
            {
                return mesgError;
            }
            else
            {
                carregaGrid();
                gvDados.ClientVisible = true;
                return "";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    
    #endregion

    #region Combos

    private void carregaComboProjetos()
    {
        string where = string.Format(@" AND Projeto.CodigoEntidade = {0} ORDER BY Projeto.NomeProjeto", codigoEntidade);

        DataSet ds = cDados.getPropostas(idUsuarioLogado, codigoEntidade, where);
        if (cDados.DataSetOk(ds))
        {
            ddlProjetos.DataSource = ds.Tables[0];
            ddlProjetos.DataBind();

        }

        ListEditItem semProjeto = new ListEditItem(Resources.traducao.nenhum, "-1");
        ddlProjetos.Items.Insert(0, semProjeto);
        
        if (!IsPostBack && ddlProjetos.Items.Count > 0)
            ddlProjetos.SelectedIndex = 0;
    }

    private void carregaComboResponsaveis()
    {
        string where = "";

        DataSet ds = cDados.getUsuariosAtivosEntidade(codigoEntidade, where);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlGerenteProjeto.ValueField = "CodigoUsuario";
            ddlGerenteProjeto.TextField = "NomeUsuario";

            ddlGerenteProjeto.DataSource = ds.Tables[0];
            ddlGerenteProjeto.DataBind();

            if (!IsPostBack)
                ddlGerenteProjeto.SelectedIndex = 0;
        }
    }

    private void carregaComboFluxos()
    {
        string where = " AND StatusFluxo = 'A' AND CodigoEntidade = " + codigoEntidade;
        DataSet ds = cDados.getFluxos(where);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlFluxo.ValueField = "CodigoFluxo";
            ddlFluxo.TextField = "NomeFluxo";

            ddlFluxo.DataSource = ds.Tables[0];
            ddlFluxo.DataBind();

            if (!IsPostBack)
                ddlFluxo.SelectedIndex = 0;
        }
    }

    #endregion
}
