using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_popupAssociaItensBacklog : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int codigoProjetoAgil;
    private int codigoProjetoIteracao;
    private int CodigoUsuarioLogado;
    private int CodigoEntidade;
    private int codigoIteracao;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    bool podeEditar = true;
    bool podeExcluir = true;
    public int alturaFrameAnexos = 372;
    public int alturaTelaUrl = 0;





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

        CodigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();        
        if (Request.QueryString["CP"] != null)
        {
            codigoProjetoIteracao = int.Parse(Request.QueryString["CP"].ToString());
        }
        codigoIteracao = getCodigoIteracao();

        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(codigoProjetoAgil, "-1", 1, true, false, false, percentualConcluido, data);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int.TryParse(Request.QueryString["ALT"] + "", out alturaTelaUrl);

        carregaTlDados();
        cDados.aplicaEstiloVisual(Page);
    }

    private int getCodigoIteracao()
    {
        bool retornoL = false;
        int retornoInt = -1;
        string comandoSQL = string.Format(@" SELECT TOP 1 CodigoIteracao FROM Agil_Iteracao WHERE CodigoProjetoIteracao = {0}", codigoProjetoIteracao);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retornoL = int.TryParse(ds.Tables[0].Rows[0]["CodigoIteracao"].ToString(), out retornoInt);
        }
        return retornoInt;
    }

    private int getCodigoProjetoAgil()
    {
        bool retornoL = false;
        int retornoInt = -1;
        string comandoSQL = string.Format(@" SELECT [CodigoProjetoPai]
  FROM [dbo].[LinkProjeto]
where CodigoProjetoFilho = {0}", codigoProjetoIteracao);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retornoL = int.TryParse(ds.Tables[0].Rows[0]["CodigoProjetoPai"].ToString(), out retornoInt);
        }
        return retornoInt;
    }

    private void carregaTlDados()
    {
        DataSet ds = getItensDoBackLog("");

        if (cDados.DataSetOk(ds))
        {
            tlDados.DataSource = ds;
            tlDados.DataBind();
        }

    }

    private void headerOnTela()
    {
       // Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/popupAssociaItensBacklog.js""></script>"));
    }

    public DataSet getItensDoBackLog(string where)
    {
        string comandoSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoProjetoAgil int
        DECLARE @in_CodigoIteracao int

        SET @in_CodigoProjetoAgil = {0}
        SET  @in_CodigoIteracao = {1}
      
        EXECUTE @RC = [dbo].[p_Agil_ItensBacklogSprint] 
                @in_CodigoProjetoAgil
  ,             @in_CodigoIteracao         
        
", getCodigoProjetoAgil(), codigoIteracao, where);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }



    protected void treeList_DataBound(object sender, EventArgs e)
    {
        //TreeListNodeIterator iterator = ((ASPxTreeList)sender).CreateNodeIterator();
        //TreeListNode node;
        //while (true)
        //{
        //    node = iterator.GetNext();
        //    if (node == null)
        //    {
        //        break;
        //    }                
        //    else
        //    {
        //        node.AllowSelect = !node.HasChildren;
        //        string CodigoIteracao = (node.GetValue("CodigoIteracao") != null && node.GetValue("CodigoIteracao").ToString() != "") ? node.GetValue("CodigoIteracao").ToString() : "-1";
        //        if (int.Parse(CodigoIteracao) == codigoIteracao)
        //        {
        //            node.Selected = true;
        //        }
        //    }
            
        //}
    }

    protected void tlDados_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
    {
        if(e.Argument == "marcar")
        {
            ((ASPxTreeList)(sender)).JSProperties["cpErro"] = "";
            ((ASPxTreeList)(sender)).JSProperties["cpSucesso"] = "";
            string inn = "(";
            string innNao = "(";


            List<string> itemsAssociados = new List<string>();
            List<string> itemsNaoAssociados = new List<string>();
            var column = tlDados.Columns["colSelecaoIB"] as TreeListDataColumn;
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
                    var cb = tlDados.FindDataCellTemplateControl(node.Key, column, "cbSelecaoIB") as DevExpress.Web.ASPxCheckBox;
                    if (cb.Checked)
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
                inn += itemsAssociados[i] + ",";
            }

            for (int i = 0; i < itemsNaoAssociados.Count; i++)
            {
                innNao += itemsNaoAssociados[i] + ",";
            }

            inn += ")";
            innNao += ")";

            inn = inn.Replace(",)", ")");
            innNao = innNao.Replace(",)", ")");

            string comandoSQLAtualizaMarcados = "";
            string comandoSQLAtualizaDesMarcados = "";
            if (inn != "()")
            {
                comandoSQLAtualizaMarcados = string.Format(@" UPDATE [Agil_ItemBacklog] 
                                                                SET [CodigoIteracao] = {0} 
                                                               ,CodigoTipoStatusItem = (SELECT TOP 1 CodigoTipoStatusItem FROM Agil_TipoStatusItemBacklog WHERE iniciaistipostatusitemControladoSistema = 'SP_NAOINI')
                                                              WHERE CodigoItem in {1} ;

                                                             UPDATE [dbo].[Agil_ItemBacklog]
                                                                SET [CodigoIteracao] = {0}
                                                              WHERE [CodigoIteracao] IS NULL
                                                                AND ISNULL([IndicaTarefa], 'N') = 'S'
                                                                AND [CodigoItemSuperior] in {1} ", codigoIteracao, inn);
            }
            if (innNao != "()")
            {
                comandoSQLAtualizaDesMarcados = string.Format(@" UPDATE [Agil_ItemBacklog] 
                                                                   SET [CodigoIteracao] = NULL 
                                                                 WHERE CodigoItem in {0};

                                                                UPDATE [dbo].[Agil_ItemBacklog]
                                                                   SET [CodigoIteracao] = NULL
                                                                 WHERE [CodigoIteracao] IS NOT NULL
                                                                   AND ISNULL([IndicaTarefa], 'N') = 'S'
                                                                   AND [CodigoItemSuperior] in {0} ", innNao);
            }


            if (inn != "()" || innNao != "()")
            {
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
        }       
    }

    protected void cbSelecaoIB_Init(object sender, EventArgs e)
    {
        var textoItem = "";
        var cb = (DevExpress.Web.ASPxCheckBox)sender;
        var key = ((TreeListCellTemplateContainerBase)cb.Parent).NodeKey;
        bool habilita = false;

        string comandoSQL = string.Format(@"select 1 from Agil_ItemBacklog where CodigoItemSuperior = {0}", key);
        DataSet ds = cDados.getDataSet(comandoSQL);
        habilita = !(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]));

        string comandoSQLTextoItem = string.Format(@"select 1 from Agil_ItemBacklog where CodigoItemSuperior = {0}", key);
        DataSet dsTextoItem = cDados.getDataSet(string.Format(@"select TituloItem from Agil_ItemBacklog where CodigoItem = {0}", key));
        if (cDados.DataSetOk(dsTextoItem) && cDados.DataTableOk(dsTextoItem.Tables[0]))
        {
            textoItem = dsTextoItem.Tables[0].Rows[0][0].ToString();
        }
        cb.Text = textoItem;
        var node = tlDados.FindNodeByKeyValue(key);
        var indicaBloqueioItem = (node.GetValue("IndicaBloqueioItem") as string) ?? string.Empty;
        var codigoIteracaoItem = (node.GetValue("CodigoIteracao"));

        if(habilita == true)
        {
            cb.ClientEnabled = !indicaBloqueioItem.Equals("S", StringComparison.InvariantCultureIgnoreCase);
        }
        else
        {
            cb.ClientEnabled = habilita;
        }

        cb.Checked = 
            !Convert.IsDBNull(codigoIteracaoItem) && 
            ((int)codigoIteracaoItem) == codigoIteracao;
    }
}