using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class Tarefas_Aprovacao : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;
    int idUsuarioLogado;
    string nomeArquivo = "";
    string palavraChave = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
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


        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["PalavraChave"] != null && Request.QueryString["PalavraChave"].ToString() != "")
        {
            palavraChave = Server.UrlDecode(Request.QueryString["PalavraChave"].ToString()).Replace("'", "''");
        }

        cDados.aplicaEstiloVisual(Page);

        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".js";

        nomeArquivo = @"/ArquivosTemporarios/Kanban_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        HeaderOnTela();
        constroiKanban();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"
        <link href=""http://cdn.sencha.com/ext/gpl/4.2.1/resources/css/ext-all-gray.css"" rel=""stylesheet"" type=""text/css""/>
        <link href=""http://cdn.sencha.com/ext/gpl/4.2.1/examples/ux/DataView/DragSelector.css"" rel=""stylesheet"" type=""text/css""/>
        <link href=""../scripts/Kanban/resources/css/taskboard-all-debug.css"" rel=""stylesheet"" type=""text/css""/>
        <!--Implementation specific styles-->
        <link href=""../scripts/Kanban/resources/css/advanced.css"" rel=""stylesheet"" type=""text/css""/>
        <!--Ext lib and UX components-->
        <script src=""http://cdn.sencha.com/ext/gpl/4.2.1/ext-all.js"" type=""text/javascript""></script>
        <script src=""http://cdn.sencha.com/ext/gpl/4.2.1/examples/ux/DataView/DragSelector.js"" type=""text/javascript""></script>
        <script src=""../scripts/Kanban/taskboard-all-debug.js"" type=""text/javascript""></script>
        <!--Application files-->
        <script src=""../scripts/Kanban/app/app.js"" type=""text/javascript""></script>
        <script type=""text/javascript"">
         
            var arrayColumns = [{ state: 'PA', title: 'Pendentes' }, { state: 'AP', title: 'Aprovadas nos últimos 7 dias' }, { state: 'RP', title: 'Reprovadas nos últimos 7 dias'}];
            var arquivoTarefas = '.." + nomeArquivo + @"';
            var mostrarConsulta = 'N';
            var mostrarEdicao = 'N';
            var mostrarComentarios = 'S';
            var mostrarAnexos = 'S';
            var multiplaSelecao = true;
            var colunaOrdenacao = '';

            function validaMovimento(task, toState)
            {
                var fromState = task.getState();
                if(toState == 'PA') {
                        return false;
                } 

                return true;       
            }        

            function constroiTarefa(task, renderData)
            {               
                 if(renderData.State == 'AP')
                 {
                    renderData.style = 'border-left-color : green;';
                    task.data.ResourceImageUrl = '../imagens/OK.png';
                 }
                 else if(renderData.State == 'RP')
                 {
                    renderData.style = 'border-left-color : red;';
                    task.data.ResourceImageUrl = '../imagens/Block.png';
                 }
                 else
                 {
                    task.data.ResourceImageUrl = '';
                 }
            }                      
        </script>
        
      "));



    }

    #endregion

    private void constroiKanban()
    {
        string where = "";

        if (palavraChave.Trim() != "")
        {
            where += string.Format(" AND (NomeTarefa LIKE '%{0}%' OR NomeProjeto LIKE '%{0}%' OR NomeRecurso LIKE '%{0}%')", palavraChave);
        }

        DataSet ds = cDados.getTarefasAprovacao(idUsuarioLogado, codigoEntidade, where);

        string conteudoJS = "[";

        if (cDados.DataSetOk(ds))
        {
            int quantidadeTotal = ds.Tables[0].Rows.Count;
            int indexAtual = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string detalhamentoCampo = string.Format(@"<table><tr><td valign=""top"">Tarefa:</td><td valign=""top"">{0}</td></tr><tr><td valign=""top"">Projeto:</td><td valign=""top"">{1}</td></tr><tr><td valign=""top"">Recurso:</td><td valign=""top"">{2}</td></tr></table>", dr["NomeTarefa"], dr["NomeProjeto"], dr["NomeRecurso"]);
                conteudoJS += Environment.NewLine;
                conteudoJS += "{" + string.Format("Id : {0}, Name : '{1}', State : '{2}'", dr["CodigoAtribuicao"], detalhamentoCampo, dr["StatusAprovacao"]) + "}";
                if (indexAtual < quantidadeTotal)
                    conteudoJS += ",";
                indexAtual++;
                conteudoJS += Environment.NewLine;
            }
        }

        conteudoJS += "]";

        cDados.escreveTexto(conteudoJS, nomeArquivo);
    }
    protected void callbackStatus_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            int registrosAfetados = 0;
            string codigosAtrib = e.Parameter.Split(';')[0];
            string status = e.Parameter.Split(';')[1];

            string comandoSQL = "";

            foreach (string codigo in codigosAtrib.Split(','))
            {
                if (codigo != "" && status != "")
                {
                    comandoSQL += string.Format(@"
               
                    UPDATE {0}.{1}.AtribuicaoRecursoTarefa
                       SET StatusAprovacao = '{3}',
                           DataStatusAprovacao = GETDATE()                            
                     WHERE CodigoAtribuicao = {2}
                     
                     EXEC dbo.p_AtualizaRealizadoAtribuicao {2}

                  ", cDados.getDbName(), cDados.getDbOwner(), codigo, status, idUsuarioLogado, codigoEntidade);
                }
            }

            if(comandoSQL != "")
                cDados.execSQL(comandoSQL, ref registrosAfetados);
        }
    }
}