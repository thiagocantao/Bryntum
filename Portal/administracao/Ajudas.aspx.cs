using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_Ajudas : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool exportaOLAPTodosFormatos = false;

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
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CadTopAjd");
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = cDados.getNomeSistema();
        Session["ce"] = codigoEntidadeUsuarioResponsavel;
        sdsGlossarioAjuda.ConnectionString = cDados.classeDados.getStringConexao();
        sdsFuncionalidade.ConnectionString = cDados.classeDados.getStringConexao();
        gvDados.JSProperties["cp_Erro"] = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        cDados.aplicaEstiloVisual((Page)sender);
        cDados.aplicaEstiloVisual(htmlDetalhesAjuda, "Default");
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 350;
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Ajudas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "Ajudas"));
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados);TipoOperacao = 'Incluir';", true, true, false, "EN_CadTopAjd", lblTituloTela.Text, this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "EN_CadTopAjd");
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }


    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {

    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxGridView)sender).JSProperties["cp_Erro"] = "";
        ((ASPxGridView)sender).JSProperties["cp_MensagemSucesso"] = "";

        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_MensagemSucesso"] = Resources.traducao.Ajudas_item_de_ajuda_inclu_do_com_sucesso_;
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_MensagemSucesso"] = Resources.traducao.Ajudas_item_de_ajuda_alterado_com_sucesso_;
        }
        if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_MensagemSucesso"] = Resources.traducao.Ajudas_item_de_ajuda_exclu_do_com_sucesso_;
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            ((ASPxGridView)sender).JSProperties["cp_Erro"] = "OK";
        }
        else
        {
            ((ASPxGridView)sender).JSProperties["cp_Erro"] = mensagemErro_Persistencia;
        }
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";
       string codigoGlossario = getChavePrimaria();
        try
        {
            sdsGlossarioAjuda.DeleteParameters["CodigoGlossarioAjuda"].DefaultValue = codigoGlossario;
            sdsGlossarioAjuda.Delete();
            gvDados.DataBind();
        }
        catch(Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;
        
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        string codigoGlossario = getChavePrimaria();
        try
        {
            //— 	travessão ‘em’ 	&mdash;
            string htmlAux = htmlDetalhesAjuda.Html.Replace("'", "'+char(39)+'").Replace("-", "&ndash;");
            htmlAux = htmlDetalhesAjuda.Html.Replace("'", "'+char(39)+'").Replace("–", "&mdash;");

            sdsGlossarioAjuda.UpdateParameters["TituloGlossarioAjuda"].DefaultValue = txtTituloAjuda.Text.Replace("'", "'+char(39)+'");
            sdsGlossarioAjuda.UpdateParameters["DetalhesGlossarioAjuda"].DefaultValue = htmlAux;

            sdsGlossarioAjuda.UpdateParameters["CodigoEntidade"].DefaultValue = codigoEntidadeUsuarioResponsavel.ToString();
            sdsGlossarioAjuda.UpdateParameters["CodigoFuncionalidade"].DefaultValue = (ddlFuncionalidade.Value == null) ? "-1" : ddlFuncionalidade.Value.ToString();
            sdsGlossarioAjuda.UpdateParameters["CodigoGlossarioAjuda"].DefaultValue = codigoGlossario;



            sdsGlossarioAjuda.Update();
            gvDados.DataBind();
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        try
        {

            string htmlAux = htmlDetalhesAjuda.Html.Replace("'", "'+char(39)+'").Replace("-", "&ndash;");
            htmlAux = htmlDetalhesAjuda.Html.Replace("'", "'+char(39)+'").Replace("–", "&mdash;");

            sdsGlossarioAjuda.InsertParameters["TituloGlossarioAjuda"].DefaultValue = txtTituloAjuda.Text.Replace("'", "'+char(39)+'");
            sdsGlossarioAjuda.InsertParameters["DetalhesGlossarioAjuda"].DefaultValue = htmlAux;
            sdsGlossarioAjuda.InsertParameters["CodigoFuncionalidade"].DefaultValue = (ddlFuncionalidade.Value == null) ? "-1" : ddlFuncionalidade.Value.ToString();
            sdsGlossarioAjuda.InsertParameters["CodigoEntidade"].DefaultValue = codigoEntidadeUsuarioResponsavel.ToString();



            sdsGlossarioAjuda.Insert();
            gvDados.DataBind();
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;
    }


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