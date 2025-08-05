using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Estrategias_CadastroMenusEstrategia : System.Web.UI.Page
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

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CadMenuEstrat");
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

        cDados.aplicaEstiloVisual(Page);

        carregaGvDados();
        populaDdlItemOpcaoMenu();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 320;
    }

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoMenu]
              ,[DescricaoGrupoMenu]
              ,[CodigoItemMenu]
              ,[NomeItemMenu]
              ,[SequenciaItemGrupoMenu]
              ,[IniciaisObjeto]
              ,[IndicaMenuDisponivel]
              ,[CodigoEntidade]
          FROM [MenuObjeto] 
         WHERE [CodigoEntidade] = {0}
          ORDER BY 2", codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    private void populaDdlItemOpcaoMenu()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoItemMenuObjeto]
              ,[DescricaoItemMenuObjeto]
              ,[URL]
              ,[IniciaisItemMenuObjeto]
              ,[CodigoTipoAssociacao]
          FROM [ItemMenuObjeto]
         WHERE CodigoTipoAssociacao <>  [dbo].[f_GetCodigoTipoAssociacao]('PR')
         ORDER BY 2");

        DataSet ds = cDados.getDataSet(comandoSQL);
        ddlOpcao.TextField = "DescricaoItemMenuObjeto";
        ddlOpcao.ValueField = "CodigoItemMenuObjeto";
        ddlOpcao.DataSource = ds;
        ddlOpcao.DataBind();
    }

    private void HeaderOnTela()
    {

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroMenusEstrategias.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "CadastroMenusEstrategias", "_Strings"));
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {

    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";

        gvDados.JSProperties["cp_Erro"] = "";
        gvDados.JSProperties["cp_Sucesso"] = "";

        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            gvDados.JSProperties["cp_Erro"] = "";
            gvDados.JSProperties["cp_Sucesso"] = "Dados atualizados com sucesso!";
        }
        else
        {// alguma coisa deu errado...
            gvDados.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            gvDados.JSProperties["cp_Sucesso"] = "";
        }
    }

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteEdicaoRegistro()
    {
        string chave = getChavePrimaria();

        string retorno = "";
        string DescricaoGrupoMenu = txtGrupo.Text;
        string CodigoItemMenu = ddlOpcao.Value.ToString();
        string NomeItemMenu = txtTituloMenu.Text;
        string SequenciaItemGrupoMenu = spnOrdem.Value.ToString();
        string IniciaisObjeto = "PL";
        string IndicaMenuDisponivel = ddlIndicaDisponivel.Value.ToString();
        //string CodigoEntidade = { 6 }

        string comandSql = string.Format(@"
           declare @DescricaoGrupoMenu varchar(100)
           declare @CodigoItemMenu int
           declare @NomeItemMenu varchar(100)
           declare @SequenciaItemGrupoMenu tinyint
           declare @IniciaisObjeto char(2)
           declare @IndicaMenuDisponivel char(1)
           declare @CodigoEntidade int
           declare @CodigoMenu int
           
           SET @DescricaoGrupoMenu = '{0}'
           SET @CodigoItemMenu = {1}
           SET @NomeItemMenu = '{2}'
           SET @SequenciaItemGrupoMenu = {3}
           SET @IniciaisObjeto = '{4}'
           SET @IndicaMenuDisponivel = '{5}'
           SET @CodigoEntidade = {6}
           SET @CodigoMenu = {7}

           UPDATE [MenuObjeto]
           SET [DescricaoGrupoMenu] = @DescricaoGrupoMenu
               ,[CodigoItemMenu] = @CodigoItemMenu
               ,[NomeItemMenu] = @NomeItemMenu
               ,[SequenciaItemGrupoMenu] = @SequenciaItemGrupoMenu
               ,[IndicaMenuDisponivel] = @IndicaMenuDisponivel
          WHERE CodigoMenu = @CodigoMenu",
           DescricaoGrupoMenu,
           CodigoItemMenu,
           NomeItemMenu,
           SequenciaItemGrupoMenu,
           IniciaisObjeto,
           IndicaMenuDisponivel,
           codigoEntidadeUsuarioResponsavel, chave);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + Environment.NewLine + comandSql + Environment.NewLine + cDados.geraBlocoEndTran());
        retorno = ds.Tables[0].Rows[0][0].ToString().Trim().ToUpper().Replace("OK", "");
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        string DescricaoGrupoMenu = txtGrupo.Text;
        string CodigoItemMenu = ddlOpcao.Value.ToString();
        string NomeItemMenu = txtTituloMenu.Text;
        string SequenciaItemGrupoMenu = spnOrdem.Value.ToString();
        string IniciaisObjeto = "PL";
        string IndicaMenuDisponivel = ddlIndicaDisponivel.Value.ToString();
        //string CodigoEntidade = { 6 }

        string comandSql = string.Format(@"
           declare @DescricaoGrupoMenu varchar(100)
           declare @CodigoItemMenu int
           declare @NomeItemMenu varchar(100)
           declare @SequenciaItemGrupoMenu tinyint
           declare @IniciaisObjeto char(2)
           declare @IndicaMenuDisponivel char(1)
           declare @CodigoEntidade int
           
           SET @DescricaoGrupoMenu = '{0}'
           SET @CodigoItemMenu = {1}
           SET @NomeItemMenu = '{2}'
           SET @SequenciaItemGrupoMenu = {3}
           SET @IniciaisObjeto = '{4}'
           SET @IndicaMenuDisponivel = '{5}'
           SET @CodigoEntidade = {6}

       INSERT INTO [MenuObjeto]
           ([DescricaoGrupoMenu]
           ,[CodigoItemMenu]
           ,[NomeItemMenu]
           ,[SequenciaItemGrupoMenu]
           ,[IniciaisObjeto]
           ,[IndicaMenuDisponivel]
           ,[CodigoEntidade])
     VALUES
           (@DescricaoGrupoMenu 
           ,@CodigoItemMenu 
           ,@NomeItemMenu
           ,@SequenciaItemGrupoMenu
           ,@IniciaisObjeto
           ,@IndicaMenuDisponivel
           ,@CodigoEntidade)",
           DescricaoGrupoMenu,
           CodigoItemMenu,
           NomeItemMenu,
           SequenciaItemGrupoMenu,
           IniciaisObjeto,
           IndicaMenuDisponivel,
           codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() + Environment.NewLine + comandSql + Environment.NewLine + cDados.geraBlocoEndTran());
        retorno = ds.Tables[0].Rows[0][0].ToString().Trim().ToUpper().Replace("OK", "");
        return retorno;
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CmEstr");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "CmEstr", lblTituloTela.Text, this);
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

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }
}