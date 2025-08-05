using System;
using System.Data;
using DevExpress.Web.ASPxTreeList;
using System.Drawing;
using System.Collections.Generic;

public partial class _Projetos_DadosProjeto_SelecaoItensPlanejamentoSprint : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int codigoProjetoSprint;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    public int alturaFrameAnexos = 372;
    private int codigoProjetoIteracao;

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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();

        if (Request.QueryString["CP"] != null)
        {
            codigoProjetoSprint = int.Parse(Request.QueryString["CP"].ToString());
        }
        tlDados.JSProperties["cp_CodigoProjeto"] = codigoProjetoSprint.ToString();

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        }

        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(codigoProjetoSprint, "-1", 1, true, false, false, percentualConcluido, data);        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        carregaTlDados();
        cDados.aplicaEstiloVisual(Page);
    }




    #region GRID

    private void carregaTlDados()
    {
        DataSet ds = getItensDoBackLog();

        if (cDados.DataSetOk(ds))
        {
            tlDados.DataSource = ds;
            tlDados.DataBind();
        }
    }

    public int getCodigoIteracao()
    {
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoProjetoAgil int
        DECLARE @in_CodigoProjetoSprint int
        DECLARE @l_CodigoIteracao       int

        SET @in_CodigoProjetoSprint = {0}--codigoiteracao
        SET  @in_CodigoProjetoAgil = NULL--codigo projeto agil ou codigo projeto pai

        SELECT TOP 1 @l_CodigoIteracao = it.CodigoIteracao,
		       @in_CodigoProjetoAgil = lp.CodigoProjetoPai
        FROM Agil_Iteracao it INNER JOIN
		     LinkProjeto  lp ON (lp.CodigoProjetoFilho = it.CodigoProjetoIteracao)   
        WHERE it.CodigoProjetoIteracao = @in_CodigoProjetoSprint

        SELECT @l_CodigoIteracao as CodigoIteracao

        ", codigoProjetoSprint);

        DataSet ds = cDados.getDataSet(comandoSQL);
        int retonoCodigoIteracao = -1;
        bool retornoInt = int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out retonoCodigoIteracao);
        return retonoCodigoIteracao;
    }

    public DataSet getItensDoBackLog()
    {
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoProjetoAgil int
        DECLARE @in_CodigoProjetoSprint int
        DECLARE @l_CodigoIteracao       int

        SET @in_CodigoProjetoSprint = {0}--codigoiteracao
        SET  @in_CodigoProjetoAgil = NULL--codigo projeto agil ou codigo projeto pai

        SELECT TOP 1 @l_CodigoIteracao = it.CodigoIteracao,
		       @in_CodigoProjetoAgil = lp.CodigoProjetoPai
        FROM Agil_Iteracao it INNER JOIN
		     LinkProjeto  lp ON (lp.CodigoProjetoFilho = it.CodigoProjetoIteracao)   
        WHERE it.CodigoProjetoIteracao = @in_CodigoProjetoSprint

        EXECUTE @RC = [dbo].[p_Agil_ItensBacklogSprint] 
                @in_CodigoProjetoAgil
  ,             @l_CodigoIteracao ",  codigoProjetoSprint);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }
    
    #endregion

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ItensBacklog_antigo.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));

        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        //gvDados.Settings.VerticalScrollableHeight = altura - 130;
        //tlDados.Height = new Unit((alturaPrincipal) + "px");
        tlDados.Settings.ScrollableHeight = alturaPrincipal - 355;
        // txtDetalheItem.Height = altura - 370;
        alturaFrameAnexos = alturaPrincipal - 370;
        tlDados.JSProperties["cp_AlturaFrameAnexos"] = alturaFrameAnexos;
    }
         
    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    private string getChavePrimaria()
    {
        if (tlDados.FocusedNode != null)
        {
            return tlDados.FocusedNode.Key;
        }
        else
        {
            return "";
        }

    }
    #endregion




    protected void tlDados_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
    {
        ((ASPxTreeList)(sender)).JSProperties["cpErro"] = "";
        ((ASPxTreeList)(sender)).JSProperties["cpSucesso"] = "";
        string inn = "(-1";
        string innNao = "(-1";

        List<string> itemsAssociados = new List<string>();
        List<string> itemsNaoAssociados = new List<string>();
        TreeListNodeIterator iterator = ((ASPxTreeList)(sender)).CreateNodeIterator();
        TreeListNode node;
        while (true)
        {
            node = iterator.GetNext();
            if (node == null)
            {
                break;
            }
            else
            {
                if (node.Selected == true)
                {
                    itemsAssociados.Add(node.GetValue("CodigoItem").ToString());
                }
                else
                {
                    itemsNaoAssociados.Add(node.GetValue("CodigoItem").ToString());
                }
            }
        }
        for (int i = 0; i < itemsAssociados.Count; i++)
        {
            inn += "," + itemsAssociados[i];
        }
        for (int i = 0; i < itemsNaoAssociados.Count; i++)
        {
            innNao += "," + itemsNaoAssociados[i];
        }

        inn += ")";
        innNao += ")";

        string comandoSQLAtualizaMarcados = string.Format(@" UPDATE [Agil_ItemBacklog] SET [CodigoIteracao] = {0} WHERE CodigoItem in {1}  ", getCodigoIteracao(), inn);
        string comandoSQLAtualizaDesMarcados = string.Format(@" UPDATE [Agil_ItemBacklog] SET [CodigoIteracao] = NULL WHERE CodigoItem in {0}  ", innNao);

        DataSet dsItem = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            comandoSQLAtualizaMarcados + comandoSQLAtualizaDesMarcados +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(dsItem) && cDados.DataTableOk(dsItem.Tables[0]))
        {
            if (dsItem.Tables[0].Rows[0][0].ToString().ToUpper().Trim() == "OK")
            {
                ((ASPxTreeList)(sender)).JSProperties["cpSucesso"] = "Dados atualizados com sucesso!";
            }
            else
            {
                ((ASPxTreeList)(sender)).JSProperties["cpErro"] = dsItem.Tables[0].Rows[0][0].ToString();
            }
        }
    }
    
    protected void tlDados_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        if (e.RowKind == TreeListRowKind.Data)
        {
            if (e.GetValue("TituloItem") != null)
            {
                TreeListNode tln = tlDados.FindNodeByKeyValue(e.GetValue("CodigoItem").ToString());
                if (tln != null && tln.HasChildren)
                {
                    e.Row.BackColor = Color.FromName("#EAEAEA");//http://www.color-hex.com/color/eaeaea
                    e.Row.ForeColor = Color.Black;

                }
            }
        }
    }

    protected void tlDados_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e)
    {
        if (e.ButtonType == TreeListCommandColumnButtonType.Delete)
        {
            if (e.GetValue("TituloItem") != null)
            {
                TreeListNode tln = tlDados.FindNodeByKeyValue(e.GetValue("CodigoItem").ToString());
                if (tln != null && tln.HasChildren)
                {
                    e.Enabled = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
    }

    protected void tlDados_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        if (e.Column.FieldName == "TagItem")
        {
            if (e.CellValue != null && e.CellValue.ToString() != "")
            {
                e.Cell.Text = e.CellValue.ToString().Replace("|", ", ");
            }
        }
    }

    protected void ASPxTreeListExporter1_RenderBrick(object sender, ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.Column.FieldName == "TagItem")
        {
            if (e.TextValue != null && e.TextValue.ToString() != "")
            {
                e.Text = e.TextValue.ToString().Replace("|", ", ");
                e.TextValue = e.Text;
            }
        }
    }

    protected void tlDados_FocusedNodeChanged(object sender, EventArgs e)
    {
        if (((ASPxTreeList)sender).FocusedNode != null)
        {
            string tituloItem = ((ASPxTreeList)sender).FocusedNode.GetValue("TituloItem").ToString();
            ((ASPxTreeList)sender).JSProperties["cpNoSelecionado"] = tituloItem;
        }
    }

    protected void tlDados_DataBound(object sender, EventArgs e)
    {
        TreeListNodeIterator iterator = ((ASPxTreeList)sender).CreateNodeIterator();
        TreeListNode node;
        while (true)
        {
            node = iterator.GetNext();
            if (node == null)
            {
                break;
            }
            else
            {
                node.AllowSelect = !node.HasChildren;
                string CodigoIteracao = (node.GetValue("CodigoIteracao") != null && node.GetValue("CodigoIteracao").ToString() != "") ? node.GetValue("CodigoIteracao").ToString() : "-1";
                if (int.Parse(CodigoIteracao) ==  getCodigoIteracao())
                {
                    node.Selected = true;
                }
            }

        }
    }
}