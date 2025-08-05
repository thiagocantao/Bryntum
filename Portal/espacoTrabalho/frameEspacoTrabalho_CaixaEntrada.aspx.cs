using System;
using System.Drawing;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class espacoTrabalho_frameEspacoTrabalho_CaixaEntrada : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;
    public string executaLink = "S";
    private string resolucaoCliente = "";
    private int tipoAssociacaoParametro;

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
        this.Title = cDados.getNomeSistema();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        onHeaderTela();

        if(Request.QueryString["CTA"]!=null && Request.QueryString["CTA"].ToString() != "")
        {
            tipoAssociacaoParametro = cDados.getCodigoTipoAssociacao(Request.QueryString["CTA"].ToString());
        }

        hfGeral.Set("tipoAssociacaoWf", cDados.getCodigoTipoAssociacao("NF")); // tipo de associação Notificação de Workflow
        hfGeral.Set("tipoAssociacaoMsg", cDados.getCodigoTipoAssociacao("MG")); // tipo de associação Mensagem

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            // deixando a aba de caixa de entrada ativa quando entrando na tela;
            if ((!IsCallback))
                tcBandeixa.ActiveTabIndex = 0;
        }

        carregaGrid();

        if (tipoAssociacaoParametro == int.Parse(hfGeral.Get("tipoAssociacaoWf").ToString()) ) //se for notificações de fluxos
        {
            lblCaixaDeMensagem.Text = "Aprovações";
            
            tcBandeixa.TabPages[0].Text = "Aprovar";
            lblCaixaEntrada.Text = "Ações que preciso aprovar";

            tcBandeixa.TabPages[1].Text = "Acompanhar";
            lblCaixaSaida.Text = "Ações que estou acompanhando";
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    #region VARIOS

    private void onHeaderTela()
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/frameEspacoTrabalho_CaixaEntrada.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int alturaPrincipal = 0;
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = alturaPrincipal - 300;

        gvCaixaEntrada.Settings.VerticalScrollableHeight = altura - 10;
        gvCaixaSaida.Settings.VerticalScrollableHeight = altura - 10;
    }

    #endregion

    #region GRIDVIEW

    private void carregaGrid()
    {
        DataSet ds1 = new DataSet();

        //Cargando Caixa do Entrada
        //string where = "AND     cm.CodigoTipoAssociacao =  " + tipoAssociacaoParametro;

        ds1 = getCaixaMensagens(codigoUsuarioResponsavel, "E", codigoEntidadeUsuarioResponsavel, tipoAssociacaoParametro, "");
        gvCaixaEntrada.DataSource = ds1.Tables[0];
        gvCaixaEntrada.DataBind();

        //if (ds1.Tables[0].Rows.Count > 0)
        //    gvCaixaEntrada.FocusedRowIndex = 0;

        //Cargando Caixa do Saida
        //DataSet ds2 = new DataSet();
        //ds2 = cDados.getCaixaMensagens(codigoUsuarioResponsavel, "S", codigoEntidadeUsuarioResponsavel, where);
        gvCaixaSaida.DataSource = ds1.Tables[1]; //ds2;
        gvCaixaSaida.DataBind();

        //if (ds2.Tables[0].Rows.Count > 0)
        //    gvCaixaSaida.FocusedRowIndex = 0;
    }

    public DataSet getCaixaMensagens(int codUsuario, string tipoMensagem, int codigoEntidade, int tipoAssociacaoParametro, string where)
    {
        DataSet ds;
        string comandoSQL = string.Format(@"
            BEGIN
                DECLARE @RC int

                DECLARE @CodigoUsuario int 
                DECLARE @CodigoEntidade int 
                DECLARE @TipoRetorno char(5) 
                DECLARE @PcodigoTipoAssociacao int
                DECLARE @QtdFluxos int   

                -- TODO: Defina valores de parâmetros aqui.
                SET @CodigoUsuario = {2}
                SET @CodigoEntidade  = {4} 
                SET @TipoRetorno  = 'LISTA'
                SET @PcodigoTipoAssociacao = '{5}'   

                EXECUTE @RC = {0}.{1}.[p_wf_GetMensagens] 
                   @CodigoUsuario
                  ,@CodigoEntidade
                  ,@TipoRetorno
                  ,@PcodigoTipoAssociacao
                  ,@QtdFluxos output
            END

        ", cDados.getDbName(), cDados.getDbOwner(), codUsuario
         , tipoMensagem, codigoEntidade, tipoAssociacaoParametro, where);

        ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    protected void gvCaixaEntrada_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGrid();
    }

    protected void gvCaixaSaida_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGrid();
    }

    protected void gvCaixaEntrada_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex != (-1) && e.VisibleIndex > 0)
        {
            try
            {
                if (!e.GetValue("DataPrevistaAcaoMensagem").ToString().Equals(""))
                {
                    string DataHoje = DateTime.Now.ToString("dd/MM/yyyy");
                    string minhaData = e.GetValue("DataPrevistaAcaoMensagem").ToString();
                    string mdata = Convert.ToDateTime(minhaData).ToString("dd/MM/yyyy");

                    DateTime DataAtual = Convert.ToDateTime(DataHoje);
                    DateTime DataBanco = Convert.ToDateTime(mdata);
                    if (DataAtual > DataBanco)
                    {
                        e.Row.ForeColor = Color.Red;
                    }
                }
                if (e.GetValue("DataPrimeiroAcessoMensagem").ToString() != "")
                {
                    e.Row.Font.Bold = false;
                }
                else
                {
                    e.Row.Font.Bold = true;
                }
            }
            catch { }
        }
    }

    protected void gvCaixaSaida_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex != (-1))
        {
            if (!e.GetValue("DataPrevistaAcaoMensagem").ToString().Equals(""))
            {
                string DataHoje = DateTime.Now.ToString("dd/MM/yyyy");
                string minhaData = e.GetValue("DataPrevistaAcaoMensagem").ToString();
                string mdata = Convert.ToDateTime(minhaData).ToString("dd/MM/yyyy");

                DateTime DataAtual = Convert.ToDateTime(DataHoje);
                DateTime DataBanco = Convert.ToDateTime(mdata);
                if (DataAtual > DataBanco)
                {
                    e.Row.ForeColor = Color.Red;
                }
            }
            if (e.GetValue("DataPrimeiroAcessoMensagem").ToString() != "")
            {
                e.Row.Font.Bold = false;
            }
            else
            {
                bool bCriaLink = true;

                int CodigoMensagem = int.Parse(e.GetValue("CodigoCaixaMensagem").ToString());
                int CodigoTipoAssociacao = int.Parse(e.GetValue("CodigoTipoAssociacao").ToString());
                int CodigoObjetoAssociado = int.Parse(e.GetValue("CodigoObjetoAssociado").ToString());

                if (CodigoTipoAssociacao == int.Parse(hfGeral.Get("tipoAssociacaoWf").ToString())) //se for notificações de fluxos
                {
                    // busca as informações do workflow
                    DataSet ds = cDados.getDadosLinkEtapaNotificacaoWf(CodigoObjetoAssociado, codigoUsuarioResponsavel);
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        int nivelAcesso = (int)ds.Tables[0].Rows[0]["NivelAcesso"];

                        if ((nivelAcesso & 2) == 0) // se o usuário NÃO tiver acesso de ação
                            bCriaLink = false;
                    } // if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))

                } // if (CodigoTipoAssociacao == hfGeral.Get("tipoAssociacaoWf"))

                else if (CodigoTipoAssociacao == int.Parse(hfGeral.Get("tipoAssociacaoMsg").ToString()))
                {
                    if (e.GetValue("DataPrimeiroAcessoMensagem").ToString() != "")
                    {
                        e.Row.Font.Bold = false;
                    }
                    else
                    {
                        e.Row.Font.Bold = false;
                    }
                }

                if (true == bCriaLink)
                {
                    e.Row.Font.Bold = true;
                }
            }
        }
    }

    protected void gvCaixaMensagens_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex != (-1))
        {
            if ("Descricao" == e.DataColumn.FieldName)
            {
                bool bCriaLink = true;

                DataRow dr = gvCaixaSaida.GetDataRow(e.VisibleIndex);

                int CodigoMensagem = int.Parse(dr["CodigoCaixaMensagem"].ToString());
                int CodigoTipoAssociacao = int.Parse(dr["CodigoTipoAssociacao"].ToString());
                int CodigoObjetoAssociado = int.Parse(dr["CodigoObjetoAssociado"].ToString());

                if (CodigoTipoAssociacao == int.Parse(hfGeral.Get("tipoAssociacaoWf").ToString())) //se for notificações de fluxos
                {
                    // busca as informações do workflow
                    DataSet ds = cDados.getDadosLinkEtapaNotificacaoWf(CodigoObjetoAssociado, codigoUsuarioResponsavel);
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        int nivelAcesso = (int)ds.Tables[0].Rows[0]["NivelAcesso"];

                        if ((nivelAcesso & 2) == 0) // se o usuário NÃO tiver acesso de ação
                            bCriaLink = false;
                    } // if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))

                } // if (CodigoTipoAssociacao == Wf)

                if (true == bCriaLink)
                {
                    executaLink = "S";
                    e.Cell.Style["cursor"] = "pointer";
                    e.Cell.Style["text-decoration"] = "underline";
                    e.Cell.Style["color"] = "Blue";
                }
                else
                    executaLink = "N";

            } // if ( "Descricao" == e.DataColumn.Name)

        } // if (e.VisibleIndex != (-1))
    }

    #endregion

    protected void pnCallback_Callback(object sender, CallbackEventArgsBase e)
    {
        int CodigoMensagem = int.Parse(hfGeral.Get("CodigoMensagem").ToString());
        int CodigoTipoAssociacao = int.Parse(hfGeral.Get("CodigoTipoAssociacao").ToString());
        int CodigoObjetoAssociado = int.Parse(hfGeral.Get("CodigoObjetoAssociado").ToString());

        if (tipoAssociacaoParametro == int.Parse(hfGeral.Get("tipoAssociacaoMsg").ToString()))
        {
            cDados.atualizaMensagemLida(CodigoMensagem, CodigoObjetoAssociado, codigoUsuarioResponsavel);
            carregaGrid();
        }

        string callbackMessage = string.Empty;

        if (CodigoTipoAssociacao == int.Parse(hfGeral.Get("tipoAssociacaoWf").ToString())) //se for notificações de fluxos
        {
            // busca as informações do workflow
            DataSet ds = cDados.getDadosLinkEtapaNotificacaoWf(CodigoObjetoAssociado, codigoUsuarioResponsavel);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                string urlWfEngine = string.Empty;
                int nivelAcesso = (int)ds.Tables[0].Rows[0]["NivelAcesso"];
                if ((nivelAcesso & 2) > 0) // se o usuário TIVER acesso de ação
                {
                    string codigoFluxo = ds.Tables[0].Rows[0]["CodigoFluxo"].ToString();
                    string codigoWorkflow = ds.Tables[0].Rows[0]["CodigoWorkflow"].ToString();
                    string codigoInstanciaWf = ds.Tables[0].Rows[0]["CodigoInstanciaWf"].ToString();
                    string codigoEtapaAtual = ds.Tables[0].Rows[0]["CodigoEtapaWf"].ToString();
                    string ocorrenciaAtual = ds.Tables[0].Rows[0]["SequenciaOcorrenciaEtapaWf"].ToString();
                    string codigoProjeto = ds.Tables[0].Rows[0]["CodigoProjeto"].ToString();
                    urlWfEngine = "wfEngine.aspx?" + "CW=" + codigoWorkflow + "&CI=" + codigoInstanciaWf + "&CE=" + codigoEtapaAtual + "&CS=" + ocorrenciaAtual;
                }
                hfGeral.Set("urlWfEngine", urlWfEngine);
            }
        }

        hfGeral.Set("CallbackMessage", callbackMessage);
    }

    protected void pnPcDados_Callback(object sender, CallbackEventArgsBase e)
    {
        int CodigoTipoAssociacao = int.Parse(hfGeral.Get("CodigoTipoAssociacao").ToString());
        int codigoObjetoAssociado = int.Parse(hfGeral.Get("CodigoObjetoAssociado").ToString());
        int codigoProjeto = int.Parse(hfGeral.Get("CodigoProjeto").ToString());
        string mesgEnviei = (tcBandeixa.ActiveTabIndex == 0) ? "S" : "N";
        pnPcDados.JSProperties["cp_Responder"] = "S";
        pnPcDados.JSProperties["cp_Acao"] = e.Parameter;

        /*se tá na caixa de entrada é pra responder*/
        /*se tá na caixa de saída é pra editar o campo mensagem*/

        if (e.Parameter == "preencherDados")
        {
            string erro = "";
            string where = " AND getmsg.CodigoMensagem = " + hfGeral.Get("CodigoDoMensagem").ToString();
            
            //bool lectura = cDados.atualizaDataLeituraMensagemProjeto(int.Parse(hfGeral.Get("CodigoDoMensagem").ToString()), codigoUsuarioResponsavel, ref erro);
            string iniciaisTipoAssociacao = cDados.getIniciaisTipoObjetoMensagem(int.Parse(hfGeral.Get("CodigoDoMensagem").ToString()), "");

            //cargo os dados do mensagem.
            DataSet ds = cDados.getMensagemObjeto(iniciaisTipoAssociacao, codigoUsuarioResponsavel, codigoProjeto, "Todas", "N"
                                                , "N", where, ref erro);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                string podeEditarResposta = ds.Tables[0].Rows[0]["EditaResposta"].ToString();
                string podeEditarMensagem = ds.Tables[0].Rows[0]["EditaMensagem"].ToString();
                string indicaRespostaNecessaria = ds.Tables[0].Rows[0]["IndicaRespostaNecessaria"].ToString();
                string conteudoTxtMensagem = ds.Tables[0].Rows[0]["Mensagem"].ToString();
                string conteudoTxtResposta = ds.Tables[0].Rows[0]["Resposta"].ToString();

                pnPcDados.JSProperties["cp_Responder"] = (indicaRespostaNecessaria == "S") ? "S" : "N";
                lblDAtosMensagem.Text = "<b>Por:</B> " + ds.Tables[0].Rows[0]["UsuarioInclusao"].ToString() + "  :: <B>Data:</B> " + ds.Tables[0].Rows[0]["DataInclusao"].ToString(); 
                txtMensagem.Text = conteudoTxtMensagem;
                txtResposta.Text = conteudoTxtResposta;// +lblDAtosMensagem.Text;

                string dataResposta = ds.Tables[0].Rows[0]["DataResposta"].ToString();
                int codUsuarioInclusao = int.Parse(ds.Tables[0].Rows[0]["CodigoUsuarioInclusao"].ToString());

                //Personalizar a grid Lista os Destinatarios do mesmo mensagem que se está lendo.
                if (indicaRespostaNecessaria.Equals("S"))
                    gvListaDestinatarios.Columns["DataLeitura"].Caption = "Data Resposta";
                else
                {
                    bool lectura = cDados.atualizaDataLeituraMensagemProjeto(int.Parse(hfGeral.Get("CodigoDoMensagem").ToString()), codigoUsuarioResponsavel, ref erro);
                    gvListaDestinatarios.Columns["DataLeitura"].Caption = "Data Leitura";
                }

                if (indicaRespostaNecessaria.Equals("N"))
                    txtMensagem.Rows = 15;

                /*verificar se é o usuário que incluiu a mensagem. */
                if (codigoUsuarioResponsavel == codUsuarioInclusao)
                {
                    /*  verificar se é o usuário que incluiu a mensagem. Se for, HABILITAR o campo MENSAGEM e DESABILITAR o campo RESPOSTA.*/
                    // btnSalvar.ClientVisible = true;
                    //btnResponder.ClientVisible = false;

                    if (dataResposta != "")
                    {
                        //se a mensagem ja tiver sido respondida nao editar nem a mensagem nem a resposta
                        txtMensagem.ClientEnabled = false;
                        txtResposta.ClientEnabled = false;
                        //btnSalvar.ClientEnabled = false;
                    }
                    else
                    {
                        txtMensagem.ClientEnabled = (podeEditarMensagem == "S");
                        txtResposta.ClientEnabled = false;
                        //btnSalvar.ClientEnabled = (podeEditarMensagem == "S");
                    }
                }
                else
                {
                    /*Se não for o usuário que incluiu a mensagem, a mensagem estiver marcada com RESPOSTA NECESSÁRIA 
                      e o usuário TIVER PERMISSÃO para RESPONDER MENSAGEM, desabilitar o campo MENSAGEM e HABILITAR o campo RESPOSTA.*/

                    //btnSalvar.ClientVisible = false;
                    btnResponder.ClientVisible = true;

                    if (txtResposta.Text != "")
                    {
                        txtMensagem.ClientEnabled = false;
                        txtResposta.ClientEnabled = false;
                        btnResponder.ClientEnabled = false;
                    }
                    else
                    {
                        txtMensagem.ClientEnabled = (podeEditarMensagem == "S") && (indicaRespostaNecessaria == "S");
                        txtResposta.ClientEnabled = (podeEditarResposta == "S") && (indicaRespostaNecessaria == "S");
                        btnResponder.ClientEnabled = (indicaRespostaNecessaria == "S");
                    }
                }
            }

            //cargo os dados do destinatarios
            DataSet dsDestinatarios = cDados.getListaUsuariosDestinoMensagem(int.Parse(hfGeral.Get("CodigoDoMensagem").ToString()), "", "");
            if (cDados.DataSetOk(dsDestinatarios))
            {
                gvListaDestinatarios.DataSource = dsDestinatarios;
                gvListaDestinatarios.DataBind();
            }


        }
        else if (e.Parameter == "Salvar")
        {
            string retorno = persisteEdicaoRegistro(codigoObjetoAssociado, codigoEntidadeUsuarioResponsavel, codigoProjeto, codigoUsuarioResponsavel);
            //carregaGrid();
        }
        else if (e.Parameter == "Responder")
        {
            string retorno = persisteRespondeRegistro(codigoObjetoAssociado);
            //carregaGrid();
        }
        else if (e.Parameter == "Fechar")
        {
           // string retorno = persisteAtualizaDataLeitura(codigoObjetoAssociado);
            //carregaGrid();
        }
    }

    //private string persisteAtualizaDataLeitura(int codigoObjetoAssociado)
    //{
    //    string erro = "";

    //    bool retorno = cDados.atualizaDataLeituraMensagemProjeto(codigoObjetoAssociado, ref erro);
    //    return erro;
    //}

    private string persisteRespondeRegistro(int codigoMensagem)
    {
        string erro = "";
        string respuesta = hfGeral.Get("txtResposta").ToString();

        bool lectura = cDados.atualizaDataLeituraMensagemProjeto(codigoMensagem, codigoUsuarioResponsavel, ref erro);
        bool retorno = cDados.respondeMensagemProjeto(codigoMensagem, respuesta, codigoUsuarioResponsavel, ref erro);
        //carregaGrid();
        return erro;
    }

    private string persisteEdicaoRegistro(int codigoMensagem, int codigoEntidade, int codigoProjeto, int idUsuarioLogado)
    {
        string erro = "";

        bool retorno = cDados.editaMensagemProjeto(codigoMensagem, codigoEntidade, codigoProjeto, idUsuarioLogado, txtMensagem.Text, txtResposta.Text, ref erro);
        //carregaGrid();
        return erro;
    }

}
