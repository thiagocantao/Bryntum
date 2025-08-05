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

public partial class _Projetos_Administracao_programasDoProjetos : System.Web.UI.Page
{
    dados cDados;
    
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "EN_CadPrg");
        }

        this.Title = cDados.getNomeSistema();

        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");
        
        cDados.aplicaEstiloVisual(this);


        carregaGvDados();
        carregaGvPesos(getChavePrimaria());

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvDados.Columns["NomeUnidadeNegocio"].Caption = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

    }


    private void carregaGvPesos(string chave)
    {
        string comandoSQL = string.Format(@"
        BEGIN
        DECLARE   @l_imgProjeto   varchar(100)
                , @l_imgProcesso  varchar(100)
                , @l_imgAgil      varchar(100)

        SET @l_imgProjeto  = '<img border=''0'' src=''../../imagens/projeto.PNG'' title=''" + Resources.traducao.projeto + @"''/>';
        SET @l_imgProcesso = '<img border=''0'' src=''../../imagens/processo.PNG'' title=''" + Resources.traducao.processo + @"'' style=''width: 21px; height: 18px;'' />';
        SET @l_imgAgil     = '<img border=''0'' src=''../../imagens/agile.PNG'' title=''" + Resources.traducao.projeto__gil  + @"'' style=''width: 21px; height: 18px;'' />';

        SELECT DISTINCT p.CodigoProjeto,
                        CASE WHEN tp.IndicaTipoProjeto = 'PRJ' THEN @l_imgProjeto
                             WHEN tp.IndicaTipoProjeto = 'PRC' THEN @l_imgProcesso + '/>'
                             WHEN tp.IndicaTipoProjeto = 'PRG' AND tp.IndicaProjetoAgil = 'S' THEN @l_imgAgil + '/>'
                             ELSE ''
                        END + '&nbsp;' +
                        p.NomeProjeto AS NomeProjeto, 
                        p.NomeProjeto AS NomeProjeto2,
                        lp.PesoProjetoFilho
                  FROM {0}.{1}.Projeto p
                  INNER JOIN {0}.{1}.LinkProjeto lp on (lp.CodigoProjetoFilho = p.CodigoProjeto)
                  INNER JOIN {0}.{1}.TipoProjeto  tp ON (tp.CodigoTipoProjeto = p.CodigoTipoProjeto)
                 WHERE lp.CodigoProjetoPai = {2}
                   AND lp.TipoLink = 'PP'
                 ORDER BY NomeProjeto2
END ", cDados.getDbName(), cDados.getDbOwner(), chave);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvPesos.DataSource = ds;
        gvPesos.DataBind();
    }

    #region VARIOS



    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));       
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/programasDoProjetos.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ASPxListbox", "programasDoProjetos"));
    }

    #endregion


    #region GRID

    private void carregaGvDados()
    {
        string where = " AND cp.CodigoEntidade = '" + codigoEntidadeUsuarioResponsavel + "'";
        DataSet ds = cDados.getProgramasDoProjeto(where);
        //bool temRegistros = false;

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
            return "-1";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.

    #endregion
    protected void ddlGerentePrograma_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }

    }
    protected void ddlGerentePrograma_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "CadProgr");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "editarPrograma(-1);", true, true, false, "CadProgr", "Programas", this);
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

    protected void treeList_CustomJSProperties(object sender, TreeListCustomJSPropertiesEventArgs e)
    {
        ASPxTreeList objTree = sender as ASPxTreeList;
        Hashtable nameTable = new Hashtable();
        foreach (TreeListNode node in objTree.GetVisibleNodes())
            nameTable.Add(node.Key, string.Format("{0}", node["NomeUnidadeNegocio"]));
        e.Properties["cpEmployeeNames"] = nameTable;
    }

    protected void gvPesos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string chave = e.Parameters;
        carregaGvPesos(chave);
    }

    protected void gvPesos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cp_erro"] = "";
        ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "";

        string mensagemErro = "";
        string PesoProjetoFilho = e.NewValues["PesoProjetoFilho"].ToString();
        string CodigoProjetoFilho = e.Keys[0].ToString();
        string CodigoProjetoPai = getChavePrimaria();
        string comandoSQL = string.Format(@"
        UPDATE [dbo].[LinkProjeto]
           SET [PesoProjetoFilho] = {0}
         WHERE CodigoProjetoPai = {1}
           and CodigoProjetoFilho = {2}", PesoProjetoFilho, CodigoProjetoPai, CodigoProjetoFilho);
        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine + comandoSQL + Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            mensagemErro = ds.Tables[0].Rows[0][0].ToString();
            if (mensagemErro == "OK")
            {
                ((ASPxGridView)sender).JSProperties["cp_sucesso"] = Resources.traducao.programasDoProjetos_peso_do_projeto_alterado_com_sucesso_;
                e.Cancel = true;
                ((ASPxGridView)sender).CancelEdit();
                carregaGvPesos(getChavePrimaria());
            }
            else
            {
                ((ASPxGridView)sender).JSProperties["cp_erro"] = mensagemErro;
            }
        }
    }

    protected void callbackTela_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";

        string chave = e.Parameter;
        try
        {
            cDados.excluiProgramaDoprojeto(chave, codigoUsuarioResponsavel.ToString());
            ((ASPxCallback)source).JSProperties["cpSucesso"] = "Programa excluído com sucesso!";
        }
        catch(Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }
        
}
