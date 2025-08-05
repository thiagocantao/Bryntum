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
        <link href=""../scripts/Kanban/resources/css/AtualizacaoTarefas.css"" rel=""stylesheet"" type=""text/css""/>
        <!--Ext lib and UX components-->
        <script src=""http://cdn.sencha.com/ext/gpl/4.2.1/ext-all.js"" type=""text/javascript""></script>
        <script src=""http://cdn.sencha.com/ext/gpl/4.2.1/examples/ux/DataView/DragSelector.js"" type=""text/javascript""></script>
        <script src=""../scripts/Kanban/taskboard-all.js"" type=""text/javascript""></script>
        <!--Application files-->
        <script src=""../scripts/Kanban/app/app.js"" type=""text/javascript""></script>
        <script type=""text/javascript"">
         
            var arrayColumns = [{state: 'NI', title: 'A Fazer'}, { state: 'EE', title: 'Fazendo' }, { state: 'FE', title: 'Feitas' }];
            var arquivoTarefas = '.." + nomeArquivo + @"';
            var mostrarConsulta = 'S';
            var mostrarEdicao = 'S';
            var mostrarComentarios = 'N';
            var mostrarAnexos = 'N';
            var multiplaSelecao = false;
            var colunaOrdenacao = 'ColunaOrder';
            function validaMovimento(task, toState)
            {
                var fromState = task.getState();
                if((toState == 'FE' && task.data.ColunaValorAux == '') || (fromState == 'FE')) {
                        return false;
                } 
                
                return true;       
            }        

            function constroiTarefa(task, renderData)
            {        
                 var estilo = '';

                 if(task.data.IndicaCorDiferente == 1)
                    estilo += 'background:#FFFFCC;color:#00000;';
                 
                 if(task.data.IndicaCorBorda != 1 || (task.data.StatusAux == 'AP' && task.data.State == 'FE'))
                        estilo += 'border-left-color : #EBEBEB;border: 2px;';
                 else if(task.data.IndicaCorBorda == 1)
                    estilo += 'border-left-color : red;';                    
 
                 renderData.style = estilo;

                 if(task.data.StatusAux == 'AP')
                 {
                    task.data.ResourceImageUrl = '../imagens/OK.png';
                 }
                 else if(task.data.StatusAux == 'RP')
                 {
                    task.data.ResourceImageUrl = '../imagens/Block.png';
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
            where += string.Format(" AND (NomeTarefa LIKE '%{0}%' OR NomeProjeto LIKE '%{0}%' OR TarefaSuperior LIKE '%{0}%' )", palavraChave);
        }

        if (Request.QueryString["Atrasadas"] != null && Request.QueryString["Atrasadas"].ToString() == "S")
        {
            where += " AND IndicaTarefaAtrasada = 'S'";
        }

        DataSet ds = cDados.getTarefasAtualizacaoUsuario(idUsuarioLogado, codigoEntidade, where);

        string conteudoJS = "[";

        if (cDados.DataSetOk(ds))
        {
            int quantidadeTotal = ds.Tables[0].Rows.Count;
            int indexAtual = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string detalhamentoCampo = "";

                if(dr["TarefaSuperior"].ToString() == "")
                    detalhamentoCampo = string.Format(@"<table><tr><td valign=""top"">Tarefa:</td><td valign=""top"">{0}</td></tr><tr><td valign=""top"">Assunto:</td><td valign=""top"">{1}</td></tr></table>", dr["NomeTarefa"], dr["NomeProjeto"]);
                else
                    detalhamentoCampo = string.Format(@"<table><tr><td valign=""top"">Tarefa Superior:</td><td valign=""top"">{2}</td></tr><tr><td valign=""top"">Tarefa:</td><td valign=""top"">{0}</td></tr><tr><td valign=""top"">Assunto:</td><td valign=""top"">{1}</td></tr></table>", dr["NomeTarefa"], dr["NomeProjeto"], dr["TarefaSuperior"]);

                conteudoJS += Environment.NewLine;
                conteudoJS += "{" + string.Format("Id : {0}, Name : '{1}', State : '{2}', IndicaCorDiferente : {3}, IndicaCorBorda : {4}, StatusAux : '{5}', ColunaValorAux : '{6}', ColunaOrder : '{7}'"
                    ,dr["CodigoAtribuicao"]
                    ,detalhamentoCampo
                    ,dr["FaseAtribuicao"]
                    , dr["IndicaToDoList"].ToString() == "S" ? 1 : 0
                    , dr["IndicaTarefaAtrasada"].ToString() == "S" ? 1 : 0
                    , dr["StatusAtribuicao"]
                    , dr["IndicaToDoList"].ToString() == "S" ? dr["InicioRealKanban"].ToString() : dr["InicioReal"].ToString()
                    , dr["IndicaTarefaAtrasada"].ToString() == "S" ? "A" : "B") + "}";

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
        callbackStatus.JSProperties["cp_AtualizaTela"] = "N";

        if (e.Parameter != "")
        {
            int registrosAfetados = 0;
            string codigo = e.Parameter.Split(';')[0];
            string status = e.Parameter.Split(';')[1];

            string comandoSQL = "";

            if (codigo != "" && status != "")
            {
                if (int.Parse(codigo) > 0)
                {
                    comandoSQL += string.Format(@"
               
                        UPDATE {0}.{1}.AtribuicaoRecursoTarefa
                           SET StatusAprovacao = '{3}',
                               DataStatusAprovacao = GETDATE()                            
                         WHERE CodigoAtribuicao = {2}

                      ", cDados.getDbName(), cDados.getDbOwner(), codigo, status == "EE" ? "EE" : "PP", idUsuarioLogado, codigoEntidade);

                    if (status == "FE")
                        comandoSQL += string.Format(@"EXEC {0}.{1}.p_PublicaTimeSheetRecurso {2}, {3}", cDados.getDbName(), cDados.getDbOwner(), idUsuarioLogado, codigoEntidade);
                }
                else
                {
                    if (status == "EE")
                    {
                        comandoSQL += string.Format(@"
               
                        UPDATE {0}.{1}.TarefaToDoList
                           SET CodigoStatusTarefa = {3},
                               InicioReal = null,
                               TerminoReal = null,
                               EsforcoReal = 0                              
                         WHERE CodigoTarefa = {2}

                      ", cDados.getDbName(), cDados.getDbOwner(), int.Parse(codigo) * -1, 4, idUsuarioLogado, codigoEntidade);
                    }
                    else
                    {
                        comandoSQL += string.Format(@"
               
                        UPDATE {0}.{1}.TarefaToDoList
                           SET CodigoStatusTarefa = {3},
                               InicioReal = InicioRealKanban,
                               TerminoReal = TerminoRealKanban,
                               EsforcoReal = EsforcoRealKanban                                
                         WHERE CodigoTarefa = {2}

                      ", cDados.getDbName(), cDados.getDbOwner(), int.Parse(codigo) * -1, 2, idUsuarioLogado, codigoEntidade);
                    }
                }
            }

            if (comandoSQL != "")
                cDados.execSQL(comandoSQL, ref registrosAfetados);

            DataSet ds = cDados.getTarefasAtualizacaoUsuario(idUsuarioLogado, codigoEntidade, " AND CodigoAtribuicao = " + codigo);

            if (cDados.DataSetOk(ds))
            {
                DataRow dr = ds.Tables[0].Rows[0];
                callbackStatus.JSProperties["cp_AtualizaTela"] = "S";
                callbackStatus.JSProperties["cp_IndicaCorBorda"] = dr["IndicaTarefaAtrasada"].ToString() == "S" ? 1 : 0;
                callbackStatus.JSProperties["cp_StatusAux"] = dr["StatusAtribuicao"].ToString();
                callbackStatus.JSProperties["cp_ColunaValorAux"] = dr["IndicaToDoList"].ToString() == "S" ? dr["InicioRealKanban"].ToString() : dr["InicioReal"].ToString();
            }
        }
    }
}