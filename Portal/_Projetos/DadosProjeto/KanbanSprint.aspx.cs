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
    int idUsuarioLogado, idProjeto = -1;
    string nomeArquivo = "";
    string mostraBotoes = "";

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


        if (Request.QueryString["CP"] != null)
            idProjeto = int.Parse(Request.QueryString["CP"].ToString());

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());        

        cDados.aplicaEstiloVisual(Page);

        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".js";

        nomeArquivo = @"/ArquivosTemporarios/KanbanItens_" + cDados.getInfoSistema("IDUsuarioLogado").ToString() + "_" + dataHora;

        HeaderOnTela();

        if (Request.QueryString["MostraBotoes"] != null && Request.QueryString["MostraBotoes"].ToString() == "N")
            mostraBotoes = "display:none;";

        callbackStatus.JSProperties["cp_MostraBotoes"] = mostraBotoes;

        callbackStatus.JSProperties["cp_Url"] = "KanbanSprint.aspx?MostraBotoes=N&CP=" + idProjeto;

        if(!IsPostBack)
            constroiKanban();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"
        <link href=""http://cdn.sencha.com/ext/gpl/4.2.1/resources/css/ext-all-gray.css"" rel=""stylesheet"" type=""text/css""/>
        <link href=""http://cdn.sencha.com/ext/gpl/4.2.1/examples/ux/DataView/DragSelector.css"" rel=""stylesheet"" type=""text/css""/>
        <link href=""../../scripts/Kanban/resources/css/taskboard-all-debug.css"" rel=""stylesheet"" type=""text/css""/>
        <!--Implementation specific styles-->
        <link href=""../../scripts/Kanban/resources/css/AtualizacaoTarefas.css"" rel=""stylesheet"" type=""text/css""/>
        <!--Ext lib and UX components-->
        <script src=""http://cdn.sencha.com/ext/gpl/4.2.1/ext-all.js"" type=""text/javascript""></script>
        <script src=""http://cdn.sencha.com/ext/gpl/4.2.1/examples/ux/DataView/DragSelector.js"" type=""text/javascript""></script>
        <script src=""../../scripts/Kanban/taskboard-all.js"" type=""text/javascript""></script>
        <!--Application files-->
        <script src=""../../scripts/Kanban/app/app.js"" type=""text/javascript""></script>
        <script type=""text/javascript"">
         
            var arrayColumns = [{state: 'INI', title: 'Não Iniciado'}, { state: 'FAZ', title: 'Fazendo' }, { state: 'PRO', title: 'Pronto' }];
            var arquivoTarefas = '../.." + nomeArquivo + @"';
            var mostrarConsulta = 'S';
            var mostrarEdicao = 'N';
            var mostrarComentarios = 'N';
            var mostrarAnexos = 'N';
            var multiplaSelecao = false;
            var colunaOrdenacao = 'ColunaOrder';
            function validaMovimento(task, toState)
            {
                var fromState = task.getState();

                if(task.data.ColunaValorAux == 'N')
                    return false;

                if(fromState == 'INI' && toState == 'PRO') {
                        return false;
                } 
                
                return true;       
            }        

            function constroiTarefa(task, renderData)
            {        
                 var estilo = '';

                 if(task.data.IndicaCorDiferente == 1)
                    estilo += 'background:#FFFFCC;color:#00000;';
                else
                   estilo += 'background:#F9EF3E;color:#00000;';
                 
                 if(task.data.State == 'INI')
                         estilo += 'border-left-color : red;';   
                 else if(task.data.State == 'FAZ')
                    estilo += 'border-left-color : yellow;';  
                 else if(task.data.State == 'PRO')
                    estilo += 'border-left-color : green;';       
 
                 renderData.style = estilo;

                 if(task.data.State == 'PRO')
                 {
                    task.data.ResourceImageUrl = '../../imagens/OK.png';
                 }              
            }                
        </script>
        
      "));
    }

    #endregion

    private void constroiKanban()
    {
        string comandoSQL = string.Format(@"
            SELECT * FROM dbo.f_Agil_GetItensKanban({0}, {1})", idProjeto, idUsuarioLogado);

        
        DataSet ds = cDados.getDataSet(comandoSQL);

        string conteudoJS = "[";

        if (cDados.DataSetOk(ds))
        {
            int quantidadeTotal = ds.Tables[0].Rows.Count;
            int indexAtual = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string detalhamentoCampo = getTabelaDescricao(dr);
                ///int codigoUsuarioResponsavel = int.Parse(dr["CodigoUsuarioResponsavel"].ToString());
                string podeMover = dr["PodeMovimentarItem"].ToString();                             

                conteudoJS += Environment.NewLine;
                conteudoJS += "{" + string.Format("Id : {0}, Name : '{1}', State : '{2}', IndicaCorDiferente : {3}, IndicaCorBorda : {4}, StatusAux : '{5}', ColunaValorAux : '{6}', ColunaOrder : {7}"
                    , dr["CodigoItem"]
                    , detalhamentoCampo
                    , dr["IniciaisEtapaKanban"].ToString().Trim()
                    , dr["IndicaItemNaoPlanejado"].ToString() == "S" ? 1 : 0
                    , 1
                    , dr["IniciaisEtapaKanban"].ToString().Trim()
                    , podeMover
                    , int.Parse(dr["Importancia"].ToString()) * -1) + "}";

                if (indexAtual < quantidadeTotal)
                    conteudoJS += ",";
                indexAtual++;
                conteudoJS += Environment.NewLine;
            }
        }

        conteudoJS += "]";

        cDados.escreveTexto(conteudoJS, nomeArquivo);
    }

    private string getTabelaDescricao(DataRow dr)
    {
        string strResp = dr["IniciaisEtapaKanban"].ToString().Trim() == "FAZ" ? string.Format(@"<tr><td><table><tr><td valign=""top"">Responsável:</td><td valign=""top"">{0}</td></tr></table></td></tr>", dr["Responsavel"]) : "";

        return string.Format(@"<table><tr><td><table><tr><td valign=""top"">Item:</td><td valign=""top"">{1}</td></tr></table></td></tr><tr><td><table><tr><td valign=""top"">ID:</td><td style=""padding-right:5px"" valign=""top"">{0}</td><td valign=""top"">Estimativa:</td><td style=""padding-right:5px"" valign=""top"">{2:n0}h</td><td valign=""top"">Importância:</td><td valign=""top"">{3:n0}</td></tr></table></td></tr>{4}</table>", dr["CodigoItem"], dr["TituloItem"].ToString(), dr["EsforcoPrevisto"], dr["Importancia"], strResp);
    }

    protected void callbackStatus_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackStatus.JSProperties["cp_AtualizaTela"] = "N";

        if (e.Parameter != "")
        {
            int registrosAfetados = 0;
            string codigo = e.Parameter.Split(';')[0];
            string status = e.Parameter.Split(';')[1];
            string statusAnterior = e.Parameter.Split(';')[2];
            string statusAux = "";
            
            string comandoSQL = "";

            if (codigo != "" && status != "")
            {
                switch (status)
                {
                    case "INI": statusAux = "SP_NAOINI";
                        break;
                    case "FAZ": statusAux = "SP_FAZENDO";
                        break;
                    case "PRO": statusAux = "SP_PRONTO";
                        break;
                    default: statusAux = "";
                        break;
                }

                if (int.Parse(codigo) > 0)
                {
                    if (statusAnterior == "INI" && status == "FAZ")
                    {
                        comandoSQL += string.Format(@"
                       DECLARE @CodigoNovoStatus INT

                            SELECT @CodigoNovoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                        EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoNovoStatus

                      ", cDados.getDbName(), cDados.getDbOwner(), codigo, idUsuarioLogado, status);
                    }
                    else if ((statusAnterior == "PRO" && status == "FAZ") || (statusAnterior == "FAZ" && status == "INI") || (statusAnterior == "PRO" && status == "INI"))
                    {
                        comandoSQL += string.Format(@"
                        BEGIN
                            DECLARE @CodigoStatus INT

                            SELECT @CodigoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                             EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoStatus
                        END                       
                      ", cDados.getDbName(), cDados.getDbOwner(), codigo, idUsuarioLogado, statusAux);
                    }else if (statusAnterior == "FAZ" && status == "PRO")
                    {
                        comandoSQL += string.Format(@"
                           DECLARE @CodigoStatus INT

                            SELECT @CodigoStatus = CodigoTipoStatusItem 
                              FROM Agil_TipoStatusItemBacklog
                             WHERE IniciaisTipoStatusItemControladoSistema = '{4}'

                        EXEC {0}.{1}.p_Agil_ReadequaImplementacaoItem {2}, {3}, '', @CodigoStatus
                      ", cDados.getDbName(), cDados.getDbOwner(), codigo, idUsuarioLogado, status);
                    }
                }
            }

            if (comandoSQL != "")
                cDados.execSQL(comandoSQL, ref registrosAfetados);


            string comandoSQLDetalhes = string.Format(@"
            SELECT * FROM dbo.f_Agil_GetItensKanban({0}, {1}) WHERE CodigoItem = {2}", idProjeto, idUsuarioLogado, codigo);


            DataSet ds = cDados.getDataSet(comandoSQLDetalhes);

            string detalheItem = getTabelaDescricao(ds.Tables[0].Rows[0]);

            callbackStatus.JSProperties["cp_AtualizaTela"] = "S";
            callbackStatus.JSProperties["cp_IndicaCorBorda"] = 1;
            callbackStatus.JSProperties["cp_StatusAux"] = status;
            callbackStatus.JSProperties["cp_DetalheItem"] = detalheItem;
        }
    }
}