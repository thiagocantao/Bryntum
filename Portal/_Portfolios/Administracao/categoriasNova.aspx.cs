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
using System.Drawing;

public partial class _Portfolios_Administracao_categorias : System.Web.UI.Page
{
    dados cDados;
    private string whereUpdateDelete;
    private int CodigoEntidade = 0;
    private int idUsuarioLogado;
    private int alturaPrincipal = 0;


    public  bool podeIncluir = false;

    public bool utilizaMatrizAHP = true;

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_CadCtgPrj");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dsParametros = cDados.getParametrosSistema("utilizaMatrizAHP");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0][0].ToString() != "")
        {
            utilizaMatrizAHP = dsParametros.Tables[0].Rows[0][0].ToString() == "S";
        }
        HeaderOnTela();

        cDados.aplicaEstiloVisual(Page);

        
        populaGrid();
        

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;
        
       
        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EN_CadCtgPrj"))
        {
            podeIncluir = true;
        }

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Cadastro.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/categoriasNova.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Cadastro de Categorias</title>"));
        this.TH(this.TS("barraNavegacao", "categoriasNova", "Cadastro"));
    }



    #endregion

    #region Popula Objetos

    private void populaGrid()
    {
        DataSet ds = cDados.getCategoriasEntidade(CodigoEntidade, "");
        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();

        string captionGrid = string.Format(@"<table style=""width:100%""><tr>");
        captionGrid += string.Format(@"<td align=""left""><img src=""../../imagens/botoes/incluirReg02.png"" alt=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados)"";TipoOperacao = 'Incluir')"" style=""cursor: pointer;""/></td>");
        captionGrid += string.Format(@"</tr></table>");
        gvDados.SettingsText.Title = captionGrid;
    }

    #endregion

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback.JSProperties["cp_MSG"] = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        hfGeral.Set("ErroSalvar", "");

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
        {
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
            pnCallback.JSProperties["cp_MSG"] = mensagemErro_Persistencia;
        }
        
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        try
        {
            salvaRegistro("I", -1, ref retorno);

            return retorno;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            salvaRegistro("E", int.Parse(chave), ref retorno);

            return retorno;
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
            // busca a chave primaria
            string chave = getChavePrimaria();

            string msg = "";
            int regAfetados = 0;

            int codCategoria = int.Parse(chave);
            cDados.excluiCategoria(codCategoria, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), ref msg, ref regAfetados);
            populaGrid();
            return msg;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void salvaRegistro(string modo, int codCategoria, ref string msg )
    {
        bool retorno = false;
        int regAfetados = 0;               
        
        if (modo == "I")
        {
            retorno = cDados.incluiCategoria(txtCategoria.Text, txtSigla.Text, int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), CodigoEntidade, ref msg, ref regAfetados);
        }
        else if (modo == "E")
        {
            retorno = cDados.atualizaCategoria(codCategoria, txtCategoria.Text, txtSigla.Text, ref msg, ref regAfetados);
        }
        populaGrid();
        gvDados.FocusedRowIndex = 0;
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
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
        else if (e.ButtonID == "btnMatriz")
        {
            if (e.CellType == GridViewTableCommandCellType.Data)
                e.Visible = (utilizaMatrizAHP) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CatPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CatPrj", "Categorias de Projetos", this);
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
