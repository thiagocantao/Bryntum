using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Agil_ImportarTarefasParaItemBacklog : System.Web.UI.Page
{
    dados cDados;
    private int idProjeto;
    private int codigoUsuarioLogado;
    private int codigoEntidadeUsuarioLogado;
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
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioLogado = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        this.Title = cDados.getNomeSistema();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        carregaGvTarefas();
        cDados.aplicaEstiloVisual(this);
    }
    private void carregaGvTarefas()
    {
        string comandoSQL = string.Format(@"SELECT CodigoTarefa, SequenciaTarefa, NomeTarefa,  Termino, EDT 
                                              FROM [dbo].[f_Agil_GetTarefasParaItemBacklog] ({0})", idProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvTarefas.DataSource = ds;
        gvTarefas.DataBind();
    }



    protected void callbackTela_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        bool retorno = false;
        //key  += '|UNSELECT';
        string cmdSQL = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoProjeto int
        DECLARE @in_CodigoUsuario int
        DECLARE @in_CodigoIteracao int

        SET @in_CodigoProjeto = {0}
        SET @in_CodigoUsuario = {1}
        SET @in_CodigoIteracao = NULL {2}", idProjeto, codigoUsuarioLogado, Environment.NewLine);

        try
        {
            int registrosAfetados = 0;

            var listaDeTarefas = gvTarefas.GetSelectedFieldValues("CodigoTarefa");
            foreach (var codigoTarefa in listaDeTarefas)
            {
                cmdSQL += string.Format(@"
                EXECUTE @RC = [dbo].[p_Agil_ItemBacklogFromTarefa] 
                @in_CodigoProjeto
                ,{0}
                ,@in_CodigoUsuario
                ,@in_CodigoIteracao {1}", codigoTarefa, Environment.NewLine);
            }

            retorno = cDados.execSQL(cmdSQL, ref registrosAfetados);
            if(retorno == true)
            {
                ((ASPxCallback)source).JSProperties["cpSucesso"] = "Tarefas importadas com sucesso!";
            }
        }
        catch (Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
    }
}