using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class administracao_dotacoesOrcamentarias : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";
    private string whereSomenteExcluidas = "";
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

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CadDotOrca");
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = cDados.getNomeSistema();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }
        cDados.aplicaEstiloVisual(Page);
        
        carregaGvDados();
        
        if (!IsPostBack)
        {            
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }


    public DataSet getDotacoesOrcamentarias1(int codigoEntidadeUsuarioResponsavel, string where)
    {
        string comandoSQL = string.Format(@"
            SELECT Dotacao
                   ,DataExclusao
                   ,CodigoEntidade
             FROM {0}.{1}.pbh_DotacaoOrcamentaria
             WHERE CodigoEntidade = {2} {3}
             ORDER BY Dotacao", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, where);

        return cDados.getDataSet(comandoSQL);
    }


    private void carregaGvDados()
    {
        whereSomenteExcluidas = "and DataExclusao is null";
        if (ckbExcluidas.Checked == true)
        {
            whereSomenteExcluidas = " and DataExclusao is not null";
        }
        DataSet ds = getDotacoesOrcamentarias1(codigoEntidadeUsuarioResponsavel, whereSomenteExcluidas);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 375;
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/dotacoesOrcamentarias.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "dotacoesOrcamentarias", "_Strings"));

    }
    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

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
                if (ckbExcluidas.Checked == true)
                {
                    e.Image.Url = "~/imagens/botoes/retornar.png";
                    e.Image.ToolTip = "Restaurar";
                }
                else
                {
                    e.Image.Url = "~/imagens/botoes/excluirReg02.PNG";
                }
            }
            else
            {
                e.Enabled = false;
                if (ckbExcluidas.Checked == true)
                {
                    e.Image.Url = "~/imagens/botoes/retornarDes.png";
                    e.Image.ToolTip = "Restaurar";
                }
                else
                {
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
                
            }
        }
    }
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
        if (e.Parameter == "Restaurar")
        {
            mensagemErro_Persistencia = persisteRestauracaoRegistro();
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

    public bool restauraDotacaoOrcamentaria(string dotacaoSelecionada, int codigoEntidade, ref string mensagemErro)
    {
        bool retorno = false;
        int registrosAfetados = 0;
        string comandoSQL = string.Format(@"
        UPDATE {0}.{1}.pbh_DotacaoOrcamentaria
           SET DataExclusao = null
         WHERE CodigoEntidade = {2} AND Dotacao =  '{3}'",cDados.getDbName(),cDados.getDbOwner(), codigoEntidade, dotacaoSelecionada);
        try
        {
            retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;
    }


    private string persisteRestauracaoRegistro()
    {
        string codigo = getChavePrimaria();
        string mensagemErro = "";
        bool result = restauraDotacaoOrcamentaria(codigo, codigoEntidadeUsuarioResponsavel, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
        {
            Object[] objj = (Object[])gvDados.GetRowValues(gvDados.FocusedRowIndex, "Dotacao","CodigoEntidade");

            string retorno = "";
            retorno = objj[0].ToString();
            return retorno;
        }
            
        else
            return "null";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {

        string descricao = hfGeral.Get("txtDescricao").ToString();

        //DataSet ds = cDados.getDotacoesOrcamentarias(codigoEntidadeUsuarioResponsavel, " and Dotacao like '" + txtDescricao.Text + "'");

        //if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        //    return "Dotação Orçamentária já existente!";
        string mensagemErro = "";
        bool result = cDados.incluiDotacaoOrcamentaria(descricao, codigoEntidadeUsuarioResponsavel, ref mensagemErro);
        hfGeral.Set("txtDescricao", "");
        if (result == false)
            return "Erro ao salvar o registro: " + mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }

    }



    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        string codigo = getChavePrimaria();

        string descricao = hfGeral.Get("txtDescricao").ToString();

        string dotacaoSelecionada =  gvDados.GetRowValues(gvDados.FocusedRowIndex, "Dotacao").ToString();
        DataSet ds = cDados.getDotacoesOrcamentarias(codigoEntidadeUsuarioResponsavel, string.Format(" AND Dotacao = '{0}' AND CodigoEntidade = {1}", descricao, codigoEntidadeUsuarioResponsavel));
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (descricao == dotacaoSelecionada)
            {
                return "";
            }
            else
            {
                return "Dotação Orçamentária já existente!";
            }
        }
            
        string mensagemErro = "";
        bool result = cDados.atualizaDotacaoOrcamentaria(descricao, codigoEntidadeUsuarioResponsavel, dotacaoSelecionada, ref mensagemErro);
        hfGeral.Set("txtDescricao", "");
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
        string codigo = getChavePrimaria();
        string mensagemErro = "";
        bool result = cDados.excluiDotacaoOrcamentaria(codigo, codigoEntidadeUsuarioResponsavel, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }

    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadDotOrc");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CadDotOrc", lblTituloTela.Text, this);
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