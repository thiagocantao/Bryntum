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
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web;
using System.Drawing;

public partial class administracao_CadastroGrandesDesafios : System.Web.UI.Page
{
    dados cDados;
    
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";
    public string definelegenda = "Grande Desafio Inativo";

    //Variáveis para controle de permissões
    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool podePermissao = true;
    public bool podeConsultar = true;

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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        


        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        carregaGvDados();

        
        if (!IsPostBack)
        {

            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);
        
        
    }

    #region VARIOS

  
    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = altura;

        gvDados.Settings.VerticalScrollableHeight = altura - 280;
        gvDados.Width = new Unit((largura - 10) + "px");
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));       
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroGrandesDesafios.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ASPxListbox", "CadastroGrandesDesafios"));
    }

    #endregion

    

   

    #region GRID

    private void carregaGvDados()
    {
        string where = string.Format(
            @" AND gd.CodigoEntidade = '{0}'" , codigoEntidadeUsuarioResponsavel);
        DataSet ds = cDados.getGrandesDesafios(where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }

    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    #endregion

    

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
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

        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        //string chave = getChavePrimaria();
        string mesgError = "";
        string GrandeDesafio = memGrandeDesafio.Text.Replace("'","''");
        string MetaPeriodo = txtMetaPeriodoEstrategico.Text.Replace("'", "''");
        string identityCodigoGrandeDesafio = "";
        string indicaGrandeDesafioAtivo = (bool)checkAtivo.Checked ? "S" : "N";
        try
        {
            bool result = cDados.incluiGrandeDesafio(MetaPeriodo, GrandeDesafio, codigoEntidadeUsuarioResponsavel.ToString(), indicaGrandeDesafioAtivo, ref identityCodigoGrandeDesafio, ref mesgError);
            
            if (result == false)
            {               
                    return mesgError;
            }
            else
            {
                carregaGvDados();
                gvDados.ClientVisible = false;
                return "";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string GrandeDesafio = memGrandeDesafio.Text.Replace("'", "''");
        string MetaPeriodo = txtMetaPeriodoEstrategico.Text.Replace("'", "''");
        string indicaGrandeDesafioAtivo = (bool)checkAtivo.Checked ? "S" : "N";
        string codigoGrandeDesafio = hfGeral.Get("codigoGrandeDesafio").ToString();
 
        cDados.atualizaGrandeDesafio(MetaPeriodo, GrandeDesafio, indicaGrandeDesafioAtivo, codigoGrandeDesafio);
        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return "";
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgRetorno = "";

       // cDados.excluiGrandeDesafio(chave);
        carregaGvDados();

        return msgRetorno;
    }

    #endregion

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        

        if (e.ButtonID == "btnEditarCustom")
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
        if (e.ButtonID == "btnExcluirCustom")
        {
            if (podeExcluir )
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
        else if (e.ButtonID.Equals("btnPermissoesCustom"))
        {
            if (podePermissao )
                e.Enabled = true;
            else
            {
                e.Text = "Permissões";
                e.Enabled = false;
                e.Image.Url = "~/imagens/Perfis/Perfil_PermissoesDes.png";
            }
        }
        else if (e.ButtonID.Equals("btnDetalheCustom"))
        {
            if (podeConsultar)
                e.Enabled = true;
            else
            {
                e.Text = "Detalhe";
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/pFormularioDes.png";
            }
        }

    }
    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "DescricaoGrandeDesafio")
        {
            if (e.CellValue.ToString().Length > 100)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 100) + "...";
            }
        }
    }
    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {

            string IndicaGrandeDesafioAtivo = e.GetValue("IndicaGrandeDesafioAtivo").ToString();

            if (IndicaGrandeDesafioAtivo == "N")
            {
                e.Row.BackColor = Color.FromName("#DDFFCC");
                e.Row.ForeColor = Color.Black;
                
                
            }
        }
    }
}
