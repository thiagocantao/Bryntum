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
using System.Collections.Specialized;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.Web;

public partial class administracao_FormulacaoReformulacao : System.Web.UI.Page
{
    private string _connectionString;
    /// <summary>
    /// String de conexão para acesso a dados da tela.
    /// </summary>
    public string ConnectionString
    {
        get { return _connectionString; }
        private set
        {
            sdsResponsaveisAcao.ConnectionString = value;
            _connectionString = value;
        }
    }

  
    dados cDados;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int codigoTipoAssociacao;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    public DateTime dtInicioReformulacao;
    public DateTime dtTerminoReformulacao;
    public string mesReferenciaZeus;
    public int qtdProjetosDesbloqueio = 0;
    public int qtdProjetosReformular = 0;
    public int qtdProjetosReformulando = 0;

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool podePermissao = false;
    public bool podeConsultar = false;


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
        codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("PR");

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_FormRefor");
        }

        string connectionString = cDados.classeDados.getStringConexao();
        sdsResponsaveisAcao.ConnectionString = connectionString;

        carregaDataSource();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        gvDadosGeraArquivo.JSProperties["cp_Msg"] = "";
        gvDadosGeraArquivo.JSProperties["cp_Limpa"] = "";


        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);            

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
       
        
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        verificaAcessoBotoes();
    }

    private void verificaAcessoBotoes()
    {
        lblMostraMensagem.Text = "";
        populaGridTerminoReformulacao();
        populaGridGeraArquivo();
        populaGridDesbloqueioReformulacao();

        string FormulacaoEncerrada = "N";
        string nomeNovoAnexo = "";
        DataSet dsParametros = cDados.getParametrosSistema(CodigoEntidade, "nomeArquivoTAIReformulacao");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            nomeNovoAnexo = dsParametros.Tables[0].Rows[0]["nomeArquivoTAIReformulacao"].ToString();
            string extensao = Path.GetExtension(nomeNovoAnexo).ToLower();
            if (extensao != ".pdf")
            {
                nomeNovoAnexo = nomeNovoAnexo + ".pdf";
            }
            lblMostraMensagem.Text = "Para iniciar o processo você deve selecionar o mês de referência que será utilizado para buscar os dados de realização no ZEUS. " +
                                                               "Em seguida pressione o botão iniciar reformulação." +
                                                               "\nEste procedimento irá gerar para cada projeto, um arquivo com o nome [" + nomeNovoAnexo + "] representando a situação atual do termo de abertura," +
                                                               " que será inserido nos anexos do projeto." +
                                                               "\nApós o término deste procedimento, no menu lateral do projeto uma nova opção será inserida no grupo fluxos, possibilitando a reformulação do mesmo.\n\n";
        }
        else
        {
            qtdProjetosReformular = 0;
            lblMostraMensagem.Text = "Não foi possível obter o parâmetro que identifica o nome do relatório do TAI original.\n\n";
        }

        DataSet dsMes = cDados.getDadosInicioReformulacao(CodigoEntidade);
        if (cDados.DataSetOk(dsMes) && cDados.DataTableOk(dsMes.Tables[0]))
        {
            dtInicioReformulacao = (dsMes.Tables[0].Rows[0]["dtInicioReformulacao"].ToString() != null && dsMes.Tables[0].Rows[0]["dtInicioReformulacao"].ToString() != "") ? DateTime.Parse(dsMes.Tables[0].Rows[0]["dtInicioReformulacao"].ToString()) : new DateTime();
            dtTerminoReformulacao = (dsMes.Tables[0].Rows[0]["dtTerminoReformulacao"].ToString() != null && dsMes.Tables[0].Rows[0]["dtTerminoReformulacao"].ToString() != "") ? DateTime.Parse(dsMes.Tables[0].Rows[0]["dtTerminoReformulacao"].ToString()) : new DateTime();
            mesReferenciaZeus = dsMes.Tables[0].Rows[0]["mesReferenciaZeus"].ToString();
            FormulacaoEncerrada = (dsMes.Tables[0].Rows[0]["dtTerminoReformulacao"].ToString() != null && dsMes.Tables[0].Rows[0]["dtTerminoReformulacao"].ToString() != "") ? "S" : "N";
            carregaComboMeses();
        }
        ddlMes.ClientVisible = (FormulacaoEncerrada == "N" && (mesReferenciaZeus == null || mesReferenciaZeus == "") && (qtdProjetosReformular > 0));
        lblMes.ClientVisible = (FormulacaoEncerrada == "N" && (mesReferenciaZeus == null || mesReferenciaZeus == "") && (qtdProjetosReformular > 0));
        ddlMes.Enabled = (FormulacaoEncerrada == "N" && (mesReferenciaZeus == null || mesReferenciaZeus == "") && (qtdProjetosReformular > 0));
        //AspxbuttonLimpaReformulacao.Visible = (FormulacaoEncerrada == "S" && (mesReferenciaZeus != null || mesReferenciaZeus != ""));
        AspxbuttonInicioReformulacao.Visible = (FormulacaoEncerrada == "N" && (mesReferenciaZeus == null || mesReferenciaZeus == "") && qtdProjetosReformular > 0);
        AspxbuttonDesbloqueio.Visible =        ((mesReferenciaZeus != null && mesReferenciaZeus != "") && qtdProjetosDesbloqueio > 0);
        AspxbuttonTermino.Visible = (FormulacaoEncerrada == "N" && (mesReferenciaZeus != null && mesReferenciaZeus != ""));
        gvDadosGeraArquivo.ClientVisible = AspxbuttonInicioReformulacao.Visible && (qtdProjetosReformular > 0);
        if (qtdProjetosReformular <= 0)
        {

            lblMostraMensagem.Text = lblMostraMensagem.Text+"Não existem projetos para iniciar reformulação neste momento!";

        }
        else
        {
            if (qtdProjetosReformular > 1)
                lblMostraMensagem.Text = lblMostraMensagem.Text + "Existem neste momento " + qtdProjetosReformular.ToString() + " projetos em condições de iniciar o processo reformulação!";
            else
                lblMostraMensagem.Text = lblMostraMensagem.Text + "Existe neste momento " + qtdProjetosReformular.ToString() + " projeto em condições de iniciar o processo reformulação!";
        }
 
        if (mesReferenciaZeus != null && mesReferenciaZeus != "")
        {
            
            lblMostraMensagem.Text = "Foi iniciado um processo de reformulação em: " + dsMes.Tables[0].Rows[0]["dtInicioReformulacao"].ToString();
            if (FormulacaoEncerrada == "S" )
            {
             lblMostraMensagem.Text = lblMostraMensagem.Text +"\n\nEste processo foi encerrado em: "+ dsMes.Tables[0].Rows[0]["dtTerminoReformulacao"].ToString() ;     
            }
            lblMostraMensagem.Text = lblMostraMensagem.Text + "\n\nNão é possível iniciar outro!";
        }

    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 270);
        if (altura > 0)
        {
            
            gvDadosGeraArquivo.Settings.VerticalScrollableHeight = altura-50;
            gvDados.Settings.VerticalScrollableHeight = altura;
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/adm_FormulacaoReformulacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "adm_FormulacaoReformulacao"));
    }

    #endregion

    #region COMBOBOX


    #endregion

    #region DVGRID

    private void populaGridGeraArquivo()
    {
        // busca na base todos os projetos que tem na termo de abertura a etapa = 1
        DataSet ds = cDados.getDadosGeraRelatoriosReformulacao(CodigoEntidade);
        if (cDados.DataSetOk(ds))
        {
            gvDadosGeraArquivo.DataSource = ds;
            gvDadosGeraArquivo.DataBind();
            qtdProjetosReformular = gvDadosGeraArquivo.VisibleRowCount;
        }
    }

    private void populaGridTerminoReformulacao()
    {
        //busca na base de dados todos os registros de reformulação já inseridos no sistema
        //que vai permitir encerrar os ainda não encerrados
        DataSet ds = cDados.getDadosTerminoReformulacao(CodigoEntidade);
        if (cDados.DataSetOk(ds))
        {
            gvDadosGeraArquivo.DataSource = ds;
            gvDadosGeraArquivo.DataBind();
            qtdProjetosReformulando = gvDadosGeraArquivo.VisibleRowCount;
        }
    }
    private void populaGridLog()
    {
        //// DataSet ds = cDados.getDadosGetDadosLogReformulacao();
        //if (cDados.DataSetOk(ds))
        //{
        //    gv.DataSource = ds;
        //    gvDadosGeraArquivo.DataBind();
        //}
    }

     

    private void populaGridDesbloqueioReformulacao()
    {
        //busca na base de dados todos os projetos que já foram bloqueados, ou seja, já tiveram suas etapas alteradas para 2 e estão em reformulação
        //possibilitando o desbloqueio dos mesmos, que implica em alterar o código do novo usuário responsável pela reformulação
        DataSet ds = cDados.getDadosDesbloqueioReformulacao(CodigoEntidade) ;
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
            qtdProjetosDesbloqueio = gvDados.VisibleRowCount;
        }
    }


    #endregion

    #region BANCO DE DADOS

    private void carregaDataSource()
    {
        SqlDataSource dataSource = sdsResponsaveisAcao;
        dataSource.SelectCommand = string.Format(@"
SELECT CodigoUsuario,
       NomeUsuario, 
       EMail,
       rn
  FROM (SELECT distinct 
                us.CodigoUsuario, 
                us.NomeUsuario, 
                us.EMail,
                ROW_NUMBER()over(order by nomeUsuario) as rn
           FROM Usuario us INNER JOIN 
                UsuarioUnidadeNegocio uun on us.CodigoUsuario = uun.CodigoUsuario
          WHERE uun.IndicaUsuarioAtivoUnidadeNegocio = 'S'
            AND uun.CodigoUnidadeNegocio = @CodigoEntidade
            AND us.DataExclusao IS NULL
            {2}) rs"
            , cDados.getDbName(), cDados.getDbOwner(), "");
        dataSource.SelectParameters.Clear();
        dataSource.SelectParameters.Add("CodigoEntidade", TypeCode.Int32, CodigoEntidade.ToString());
    }

    #endregion
   
    private string incluiNovoAnexo(string nomeNovoAnexo, byte[] imagemBinario, string descricaoNovoAnexo, int codigoProjeto)
    {
        try
        {

            nomeNovoAnexo = "___" + nomeNovoAnexo;

            if (nomeNovoAnexo.Length > 255)
            {
                return "O nome do arquivo não pode ter mais que 255 caracteres! Renomeie o arquivo e tente novamente.";
            }

            // VERIFICA A EXTENSÃO DO ARQUIVO.
            string extensao = Path.GetExtension(nomeNovoAnexo).ToLower();
            if (extensao == ".exe" || extensao == ".com" || extensao == ".dll")
                return "O tipo do arquivo não pode ser anexado.";

            string mensagem = "";
            // insere o arquivo no banco de dados
            mensagem = cDados.incluirAnexo(descricaoNovoAnexo, idUsuarioLogado.ToString(), nomeNovoAnexo, CodigoEntidade.ToString(),
                           null, 'N', 'S', codigoTipoAssociacao, "NULL","", codigoProjeto.ToString(), imagemBinario, "N");

            if (mensagem != "")
                return mensagem;

            return "";
        }

        catch (Exception ex)
        {
            return ex.Message;
        }
    }
        
    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != "")
        {
            string parametros = e.Parameters;
            string codigoProjeto;
            string acao;

            string[] par = parametros.Split(';');
            codigoProjeto = par[1];
            acao = par[0];
            if (acao == "Editar")
            {
                cDados.alteraUsuarioReformulacao(int.Parse(codigoProjeto), idUsuarioLogado);
                cDados.incluiLogReformulacao(int.Parse(codigoProjeto), "Projeto", "Desbloqueio Reformulação", idUsuarioLogado);
                populaGridDesbloqueioReformulacao();
            }
        }
    }

    

    private void carregaComboMeses()
    {


        ddlMes.Items.Clear();

        int meses = 12;

        meses = DateTime.Now.Month - 1;
        
        for (int i = 1; i <= meses; i++)
        {
            string mes = string.Format("{0:MMMM}", DateTime.Parse(i + "/" + i + "/2010")).ToUpper();

            DevExpress.Web.ListEditItem lei = new DevExpress.Web.ListEditItem(mes, i);

            ddlMes.Items.Insert(i - 1, lei);
        }


        if (!IsPostBack)
            selecionaMes();

    }

    private void selecionaMes()
    {
        ddlMes.Enabled = true;
        if (mesReferenciaZeus != "" && mesReferenciaZeus != null)
        {
            ddlMes.Value = (mesReferenciaZeus);
            ddlMes.Enabled = false;
        }
        else
        {
            ddlMes.Value = (DateTime.Now.Month - 1);
        }

        ddlMes.JSProperties["cp_Mes"] = ddlMes.Value;
    }

    protected void ddlMes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        selecionaMes();
    }


  
    #region cmbUsuario

    protected void cmbUsuario_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        SqlDataSource dataSource = sdsResponsaveisAcao;

        string comandoSQL = cDados.getSQLComboUsuarios(CodigoEntidade, e.Filter, "");

        cDados.populaComboVirtual(dataSource, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    protected void cmbUsuario_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        if (string.IsNullOrEmpty(ConnectionString))
            ConnectionString = cDados.classeDados.getStringConexao();
        if (e.Value != null)
        {
            long value;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            SqlDataSource dataSource =  sdsResponsaveisAcao;

            dataSource.SelectCommand = cDados.getSQLComboUsuariosPorID(CodigoEntidade);

            dataSource.SelectParameters.Clear();
            dataSource.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());

            comboBox.DataBind();
        }
    }

    #endregion

   
    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int codigoUsuario = Convert.ToInt32(e.NewValues["CodigoUsuario"]);
        int codigoProjeto = Convert.ToInt32(gvDados.GetRowValuesByKeyValue(e.Keys[0],"CodigoProjeto").ToString());
        cDados.alteraUsuarioReformulacao(codigoProjeto, codigoUsuario);
        cDados.incluiLogReformulacao(codigoProjeto, "Projeto", "Alteração usuário bloqueio", idUsuarioLogado);

        e.Cancel = true;
        gvDados.CancelEdit();
        populaGridDesbloqueioReformulacao();
    }

    protected void AspxbuttonInicioReformulacao_Click(object sender, EventArgs e)
    {
        
        populaGridGeraArquivo();
        gvDadosGeraArquivo.JSProperties["cp_Msg"] = "";

        using (MemoryStream stream = new MemoryStream())
        {
            string erro = "";
            int qtdProjetos = 0;
            string nomeNovoAnexo ="";
            try
            {
                cDados.setInfoSistema("CodigoEntidade", CodigoEntidade);
                string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_");


                qtdProjetos = gvDadosGeraArquivo.VisibleRowCount;

                gvDadosGeraArquivo.JSProperties["cp_Limpa"] = "S";

                int buscaRealizadoZeus = cDados.buscaRealizadoZeus(ddlMes.Text); 


                if (buscaRealizadoZeus == -1)
                {
                    erro = "Erro ao executar a rotina de atualização do Valor Realizado no Zeus";
                }
                else
                {

                    DataSet dsParametros = cDados.getParametrosSistema(CodigoEntidade, "nomeArquivoTAIReformulacao");
                    if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
                    {
                        nomeNovoAnexo = dsParametros.Tables[0].Rows[0]["nomeArquivoTAIReformulacao"].ToString();
                        string extensao = Path.GetExtension(nomeNovoAnexo).ToLower();
                        if (extensao != ".pdf")
                        {
                            nomeNovoAnexo = nomeNovoAnexo + ".pdf";
                        }
                    }
                    else
                    {
                        qtdProjetos = 0;
                        erro = "Não foi possível obter o parâmetro que identifica o nome do relatório do TAI original.";
                    }

                    if (qtdProjetos == 0)
                    {
                        erro = "Não existem projetos para serem processados";
                    }
                    else
                    {
                        cDados.iniciaReformulacao('I', idUsuarioLogado, qtdProjetos, ddlMes.Text, CodigoEntidade);

                        for (int i = 0; i < qtdProjetos; i++)
                        {

                            DataRowView dt = (DataRowView)gvDadosGeraArquivo.GetRow(i);

                            int codigoProjeto = int.Parse(dt["CodigoProjeto"].ToString());

                            string descricaoNovoAnexo = "Relatório do termo de abertura gerado no processo de reformulação iniciado em " + "_" + dataHora;

                            relImpressaoTai_001 rel = new relImpressaoTai_001(codigoProjeto);

                            rel.CreateDocument();

                            rel.PrintingSystem.ExportToPdf(stream);
                            byte[] arquivo = stream.GetBuffer();

                            //rel.ExportToPdf(@"D:\reformulacao\" + nomeNovoAnexo);
                            incluiNovoAnexo(nomeNovoAnexo, arquivo, descricaoNovoAnexo, codigoProjeto);
                            cDados.alteraEtapaReformulacao(codigoProjeto, 2);
                            cDados.insereFluxosProjetos(codigoProjeto, CodigoEntidade, idUsuarioLogado);
                            cDados.incluiLogReformulacao(codigoProjeto, "Abertura", "Processo Reformulação", idUsuarioLogado);
                            cDados.incluiLogReformulacao(codigoProjeto, "Arquivo", "Gerado e anexado", idUsuarioLogado);
                            cDados.incluiLogReformulacao(codigoProjeto, "Projeto", "Etapa alterada para 2", idUsuarioLogado);
                            cDados.incluiLogReformulacao(codigoProjeto, "Fluxos", "Inclusão fluxos projetos", idUsuarioLogado);

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            if (erro == "")
            {

                if (qtdProjetos > 1)
                    mostraAlerta(@qtdProjetos.ToString() + " projetos foram processados  com sucesso.", false);
                else
                    mostraAlerta(@qtdProjetos.ToString() + " projeto foi processado  com sucesso.", false);
            }
            else
            {
                cDados.iniciaReformulacao('D', idUsuarioLogado, qtdProjetos, ddlMes.Text, CodigoEntidade);
                mostraAlerta( @"Erro ao gerar os arquivos e iniciar o processo de reformulação. \n\n" + erro.Replace("'", "#"), true);
            }
        }

        verificaAcessoBotoes();
        //LoadingPanel.Visible = false;
    }


    protected void AspxbuttonTermino_Click(object sender, EventArgs e)
    {
        
        string erro = "";
        try
        {

            cDados.encerraReformulacao(idUsuarioLogado, CodigoEntidade);
            verificaAcessoBotoes();

        }
        catch (Exception ex)
        {
            erro = ex.Message;
        }

        if (erro == "")
        {
            mostraAlerta(@"Processo de encerramento reformulação executado com sucesso.", false);

        }
        else
        {
            mostraAlerta(@"Erro ao executar o processo de encerramento reformulação. " + erro.Replace("'", "#"), true);
        }
    }
    

    private void mostraAlerta(string mensagem, bool indicaErro)
    {
            string script = "<script type='text/javascript' language='javascript'>";
        if(indicaErro)
            script += Environment.NewLine + "window.top.mostraMensagem(\" " + mensagem.Replace(Environment.NewLine, " ").Replace('\"', '\'').Replace('\n', ' ') + " \", 'erro', true, false, null);";
        else
            script += Environment.NewLine + "window.top.mostraMensagem(\" " + mensagem.Replace(Environment.NewLine, " ").Replace('\"', '\'').Replace('\n', ' ') + " \", 'sucesso', false, false, null);";
        script += Environment.NewLine + "</script>";
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Client", script);

    }

    protected void gvDados_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (gvDados.IsEditing && e.Column.FieldName == "CodigoUsuario")
        {
            ASPxComboBox comboUsuario = (ASPxComboBox)e.Editor;
            comboUsuario.ItemRequestedByValue += cmbUsuario_ItemRequestedByValue;
            comboUsuario.ItemsRequestedByFilterCondition += cmbUsuario_ItemsRequestedByFilterCondition;
        }

    }
    
    protected void btnLimpaReformulacao_Click(object sender, EventArgs e)
    {
        cDados.limpaReformulacao(idUsuarioLogado,CodigoEntidade);
    }


}


