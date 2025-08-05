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
using System.Collections.Generic;
using DevExpress.Web;
using System.Collections.Specialized;

public partial class _Projetos_Administracao_tipoTarefaTimesheet : System.Web.UI.Page
{
    dados cDados;
    private string nomeTabelaDb = "TipoTarefaTimeSheet";
    private string whereUpdateDelete;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;
    private char delimitadorValores = '$';
    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;

    private char delimitadorElementoLista = '¢';

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


        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "PR_CadTipAtv");
        }

        podeIncluir = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/tipoTarefasTimeSheet.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ASPxListbox.js""></script>"));
        this.TH(this.TS("barraNavegacao", "tipoTarefasTimeSheet", "ASPxListbox"));

    }

    private void populaGrid()
    {
        DataSet ds = cDados.getTipoTarefasTimeSheet(CodigoEntidade, "");
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 90;
    }
    
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        //if (e.Parameter == "Compartilhar")
        //{
        //    mensagemErro_Persistencia = persisteInclusaoCompartilhar();
        //}
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {
            // alguma coisa deu errado...
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            if (mensagemErro_Persistencia.Contains("REFERENCE"))
                mensagemErro_Persistencia = "O dado que se tenta excluir está sendo usado por outra tela do sistema.";
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
        populaGrid();
    }

    private string persisteExclusaoRegistro()
    {
        try
        {
            cDados.delete("ProjetoTipoTarefaTimeSheet", "CodigoTipoTarefaTimeSheet = " + getChavePrimaria());
            cDados.delete(nomeTabelaDb, whereUpdateDelete);
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string persisteEdicaoRegistro()
    {

        cDados.update(nomeTabelaDb, getDadosFormulario(), whereUpdateDelete);
        return "";
    }

    private string persisteInclusaoRegistro()
    {
        try
        {
            //na hora de incluir um novo tipo de atividade 
            //deve-se pegar todos os projetos da entidade e associar cada projeto ao tipo de tarefa a ser
            //associado e atualizar a data de desativação como a data atual para que os mesmos fiquem disponiveis

            
            cDados.insert(nomeTabelaDb, getDadosFormulario(), false);
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        

    }

    private ListDictionary getDadosFormulario()
    {
        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();
        //oDadosFormulario.Add("CodigoTipoTarefaTimeSheet", getChavePrimaria());
        oDadosFormulario.Add("DescricaoTipoTarefaTimeSheet", txtDescricaoTipoTarefa.Text);
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        return oDadosFormulario;
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        podeEditar = true;
        podeExcluir = true;

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
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    protected void lbItensDisponiveis_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codTipoAtividade;
            int codigoTipoAtividade;

            if (comando == "POPLBX")
            {
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codTipoAtividade = e.Parameter.Substring(7, posDelimit - 7);

                    if (int.TryParse(codTipoAtividade, out codigoTipoAtividade))
                    {
                        populaListaBox_TipoTarefaTimeSheetDisponiveis(codigoTipoAtividade);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }
   
    protected void lbItensSelecionados_Callback(object sender, CallbackEventArgsBase e)
    {
        if (e.Parameter.Length >= 6)
        {
            string comando = e.Parameter.Substring(0, 6).ToUpper();
            string codTipoAtividade;
            int codigoTipoAtividade;

            if (comando == "POPLBX")
            {
                int posDelimit = e.Parameter.IndexOf(delimitadorValores);
                if (posDelimit > 7)
                {
                    codTipoAtividade = e.Parameter.Substring(7, posDelimit - 7);

                    if (int.TryParse(codTipoAtividade, out codigoTipoAtividade))
                    {
                        populaListaBox_TipoTarefaTimeSheetSelecionados(codigoTipoAtividade);
                    }
                }
            } /// if (comando == "POPLBX")
        } /// if (e.Parameter.Length >= 6)
    }

    private void populaListaBox_TipoTarefaTimeSheetSelecionados(int codigoTipoTarefaTimeSheet)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
        SELECT 
              p.[CodigoProjeto]
            , p.[NomeProjeto]
          FROM 
            {0}.{1}.[Projeto]               AS [p]

                INNER JOIN {0}.{1}.[Status]	AS [s] ON 
                    (s.[CodigoStatus] = p.[CodigoStatusProjeto])
         WHERE 
                p.[DataExclusao]                    IS NULL 
            AND s.[IndicaAcompanhamentoExecucao]    = 'S'
            AND p.[CodigoEntidade]                  = {3}            

              AND EXISTS( SELECT 1 FROM {0}.{1}.[ProjetoTipoTarefaTimesheet] ptt 
                                WHERE   ptt.[CodigoProjeto]             = p.[CodigoProjeto] 
                                    AND ptt.[CodigoTipoTarefaTimeSheet] = {2} 
                                    AND ptt.[DataDesativacao]           IS NULL ) "
            , cDados.getDbName(), cDados.getDbOwner(), codigoTipoTarefaTimeSheet, CodigoEntidade);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensSelecionados.DataSource = dt;
            lbItensSelecionados.ValueField = "CodigoProjeto";
            lbItensSelecionados.TextField = "NomeProjeto";
            lbItensSelecionados.DataBind();
        }
    }

    private void populaListaBox_TipoTarefaTimeSheetDisponiveis(int codigoTipoTarefaTimeSheet)
    {
        DataTable dt = null;

        string sComando = string.Format(@"
        SELECT 
              p.[CodigoProjeto]
            , p.[NomeProjeto]
          FROM 
            {0}.{1}.[Projeto]               AS [p]

                INNER JOIN {0}.{1}.[Status]	AS [s] ON 
                    (s.[CodigoStatus] = p.[CodigoStatusProjeto])
         WHERE 
                p.[DataExclusao]                    IS NULL 
            AND s.[IndicaAcompanhamentoExecucao]    = 'S'
            AND p.[CodigoEntidade]                  = {3}            

            AND NOT EXISTS( SELECT 1 FROM {0}.{1}.[ProjetoTipoTarefaTimesheet] ptt 
                                WHERE   ptt.[CodigoProjeto]             = p.[CodigoProjeto] 
                                    AND ptt.[CodigoTipoTarefaTimeSheet] = {2} 
                                    AND ptt.[DataDesativacao]           IS NULL ) "
            , cDados.getDbName(), cDados.getDbOwner(), codigoTipoTarefaTimeSheet, CodigoEntidade);
        DataSet ds = cDados.getDataSet(sComando);
        if (cDados.DataSetOk(ds))
            dt = ds.Tables[0];

        if (null != dt)
        {
            lbItensDisponiveis.DataSource = dt;
            lbItensDisponiveis.ValueField = "CodigoProjeto";
            lbItensDisponiveis.TextField = "NomeProjeto";
            lbItensDisponiveis.DataBind();
        }
    }

    private string persisteGravarTipoTarefaTimeSheet()
    {
        try
        {
            // busca a chave primaria
            string chave = getChavePrimaria();

            salvaTipoTarefaTimeSheet("E", int.Parse(chave));

            return "";
        }
        catch (Exception ex)
        {
            return string.Format(@"Erro ao salvar as informações dos Tipos de Atividade. Mensagem original: '{0}'", ex.Message);
        }
    }

    private void salvaTipoTarefaTimeSheet(string modo, int codigoTipoTarefaTimeSheet)
    {
        string comandoSQL = " ";
        montaInsertTipoTarefaTimeSheet(modo, codigoTipoTarefaTimeSheet, ref comandoSQL);
        int registrosAfetados = 0;
        cDados.execSQL(comandoSQL, ref registrosAfetados);
    }

    private void montaInsertTipoTarefaTimeSheet(string modo, int codigoTipoTarefaTimeSheet, ref string comandoSQL)
    {
         List<int> listaTipoTarefaDisponiveis = new List<int>();
         obtemListaTipoTarefaTimeSheetDisponiveis(codigoTipoTarefaTimeSheet, ref listaTipoTarefaDisponiveis);

         foreach (int codProjetos in listaTipoTarefaDisponiveis)
         {
             comandoSQL += string.Format(@"
            IF (EXISTS(SELECT 1 FROM {0}.{1}.[ProjetoTipoTarefaTimeSheet] ptt 
	                           WHERE ptt.CodigoTipoTarefaTimeSheet = {2} 
                                 AND ptt.[CodigoProjeto] = {3}))
                              UPDATE {0}.{1}.[ProjetoTipoTarefaTimeSheet] 
                                 SET [DataDesativacao] = GETDATE() 
		                       WHERE CodigoTipoTarefaTimeSheet = {2} AND [CodigoProjeto] = {3} AND [DataDesativacao] IS NULL"
                , cDados.getDbName(), cDados.getDbOwner(), codigoTipoTarefaTimeSheet, codProjetos);
         }


         List<int> listaTipoTarefaSelecionadas = new List<int>();
         obtemListaTipoTarefaTimeSheetSelecionados(codigoTipoTarefaTimeSheet, ref listaTipoTarefaSelecionadas);
         foreach (int codProjetos in listaTipoTarefaSelecionadas)
         {
             comandoSQL += string.Format(@"
            IF ( EXISTS(SELECT 1 FROM {0}.{1}.[ProjetoTipoTarefaTimeSheet] ptt 
	                            WHERE ptt.CodigoTipoTarefaTimeSheet = {2} 
                                  AND ptt.[CodigoProjeto] = {3}))
               UPDATE {0}.{1}.[ProjetoTipoTarefaTimeSheet] 
                  SET [DataDesativacao] = NULL 
                WHERE CodigoTipoTarefaTimeSheet = {2} AND CodigoProjeto = {3}
            ELSE
	             INSERT INTO {0}.{1}.[ProjetoTipoTarefaTimeSheet] ([CodigoProjeto], [CodigoTipoTarefaTimeSheet]) 
                                                           VALUES (            {3}, {2})"
               , cDados.getDbName(), cDados.getDbOwner(), codigoTipoTarefaTimeSheet, codProjetos);
         }
    }

    private bool obtemListaTipoTarefaTimeSheetDisponiveis(int codigoTipoTarefaTimeSheet, ref List<int> listaDeTipoTarefaTimeSheet)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaTipoTarefaTimeSheet, temp;

        idLista = "Disp_" + codigoTipoTarefaTimeSheet + delimitadorValores;

        listaDeTipoTarefaTimeSheet.Clear();

        if (hfCompartilhaAssuntos.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfCompartilhaAssuntos.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaTipoTarefaTimeSheet = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaTipoTarefaTimeSheet.Length; j++)
            {
                if (strListaTipoTarefaTimeSheet[j].Length > 0)
                {
                    temp = strListaTipoTarefaTimeSheet[j].Split(delimitadorValores);
                    listaDeTipoTarefaTimeSheet.Add(int.Parse(temp[1]));
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }


    private bool obtemListaTipoTarefaTimeSheetSelecionados(int codigoTipoTarefaTimeSheet, ref List<int> listaDeTipoTarefaTimeSheet)
    {
        bool bExisteReferencia;
        string idLista;
        string listaAsString = "";
        string[] strListaTipoTarefaTimeSheet, temp;

        idLista = "Sel_" + codigoTipoTarefaTimeSheet + delimitadorValores;

        listaDeTipoTarefaTimeSheet.Clear();

        if (hfCompartilhaAssuntos.Contains(idLista))
        {
            bExisteReferencia = true;
            listaAsString = hfCompartilhaAssuntos.Get(idLista).ToString();
        }
        else
            bExisteReferencia = false;

        if (null != listaAsString)
        {
            strListaTipoTarefaTimeSheet = listaAsString.Split(delimitadorElementoLista);
            for (int j = 0; j < strListaTipoTarefaTimeSheet.Length; j++)
            {
                if (strListaTipoTarefaTimeSheet[j].Length > 0)
                {
                    temp = strListaTipoTarefaTimeSheet[j].Split(delimitadorValores);
                    listaDeTipoTarefaTimeSheet.Add(int.Parse(temp[1]));
                }
            }
        } // if (null == listaAsString)

        return bExisteReferencia;
    }

    protected void hfCompartilhaAssuntos_CustomCallback(object sender, CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "SalvarTiposTarefaTimeSheet")
        {
            mensagemErro_Persistencia = persisteGravarTipoTarefaTimeSheet();
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfCompartilhaAssuntos.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            hfCompartilhaAssuntos.Set("ErroSalvar", "");
        }
        else // alguma coisa deu errado...
        {
            hfCompartilhaAssuntos.Set("StatusSalvar", "0"); // 0 indica que NÃO foi salvo com sucesso.
            hfCompartilhaAssuntos.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }
}
