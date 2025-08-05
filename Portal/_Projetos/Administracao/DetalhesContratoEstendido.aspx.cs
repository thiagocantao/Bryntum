using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_Administracao_DetalhesContratoEstendido : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeAlterarNumero = false;
    public string pUsaNumeracaoAutomatica = "N";
    public string vlabelNumeroInterno2 = "";
    public string vlabelNumeroInterno3 = "";
    public string pValidaFormatoNumeroContrato = "N", mostraLinkProjeto = "", mostraNumeroInterno2 = "", mostraNumeroInterno3 = "", mostraNumeroTrabalhadores = "";

    bool permissaoProjeto = true;
    private int codigoProjeto = -1, codigoContrato = -1;
    private bool bHabilitaFrameDetalhes = true;

    public string mostraStatus = "";
    public string alturaFrames = "";
    public string larguraFrames = "";
    private string larguraTelas = "950px";
    string dtContratoAditivo = "", dtContratoOriginal = "", vValorOriginal = "", vValorContrato = "", indicaObra = "", readOnly = "", readOnlyAditivo = "", temAditivo = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        if (!IsPostBack)
            hfGeral.Set("CodigoContrato", Request.QueryString["CodigoContrato"] != null && Request.QueryString["CodigoContrato"].ToString() != "" ? Request.QueryString["CodigoContrato"].ToString() : "-1");

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "numeracaoAutomaticaContratos", "mostraNumeroTrabalhadoresDiretos", "validaFormatacaoNumeroContrato", "labelNumeroInterno2", "labelNumeroInterno3", "mostrarAbaAcrescimosContrato", "mostrarAbaReajustesContrato", "labelAcessoriosContrato");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            pUsaNumeracaoAutomatica = dsParametros.Tables[0].Rows[0]["numeracaoAutomaticaContratos"].ToString();
            pValidaFormatoNumeroContrato = dsParametros.Tables[0].Rows[0]["validaFormatacaoNumeroContrato"].ToString();
            vlabelNumeroInterno2 = dsParametros.Tables[0].Rows[0]["labelNumeroInterno2"].ToString();
            vlabelNumeroInterno3 = dsParametros.Tables[0].Rows[0]["labelNumeroInterno3"].ToString();
            mostraNumeroTrabalhadores = dsParametros.Tables[0].Rows[0]["mostraNumeroTrabalhadoresDiretos"] + "" == "N" ? "display:none;" : "";

            DevExpress.Web.TabPage tabAcessorios = tabControl.TabPages.FindByName("tbAC");
            tabAcessorios.Visible = dsParametros.Tables[0].Rows[0]["mostrarAbaAcrescimosContrato"].ToString() == "S";
            tabAcessorios.Text = dsParametros.Tables[0].Rows[0]["labelAcessoriosContrato"] as string;
            tabControl.TabPages.FindByName("TabReajuste").Visible = dsParametros.Tables[0].Rows[0]["mostrarAbaReajustesContrato"].ToString() == "S";
        }
        hfGeral.Set("UsaNumeracaoAutomatica", pUsaNumeracaoAutomatica);
        hfGeral.Set("ValidaFormatoNumeracaoContrato", pValidaFormatoNumeroContrato);
        hfGeral.Set("labelNumeroInterno2", vlabelNumeroInterno2);
        hfGeral.Set("labelNumeroInterno3", vlabelNumeroInterno3);

        ASPxLabelNumeroInterno2.Text = vlabelNumeroInterno2 + ":";
        ASPxLabelNumeroInterno3.Text = vlabelNumeroInterno3 + ":";

        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (Request.QueryString["ID"] != null)
        {
            codigoProjeto = int.Parse(Request.QueryString["ID"].ToString());
            cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref permissaoProjeto, ref permissaoProjeto, ref permissaoProjeto);
        }
        else
        {
            mostraStatus = "display:none;";
        }

        //if (Request.QueryString["Larg"] != null &&  Request.QueryString["Larg"].ToString() != "")
        //    larguraTelas = Request.QueryString["Larg"].ToString();

        larguraFrames = @"style=""width:" + larguraTelas + @""" ";

        codigoContrato = int.Parse(hfGeral.Get("CodigoContrato").ToString());

        if (codigoProjeto != -1)
            ddlProjetos.JSProperties["cp_MostraLink"] = "N";
        else
            ddlProjetos.JSProperties["cp_MostraLink"] = "S";

        carregaComboTipoContrato();
        carregaComboRazaoSocial();
        carregaComboMunicipio();
        carregaCombosegmento();
        carregaComboCriterioReajuste();
        carregaComboTipoContratacao();
        carregaComboOrigem();
        carregaComboFonte();
        carregaComboCriterioMedicao();
        carregaComboGestorContrato();
        carregaComboProjetos();
        carregaComboUnidadeGestora(string.Format(@" AND CodigoEntidade = {0} AND IndicaUnidadeNegocioAtiva = 'S' AND DataExclusao IS NULL", codigoEntidadeUsuarioResponsavel));
        carregaComboPagamentos();
        carregaComboStatus();

        readOnly = Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() != "" ? Request.QueryString["RO"].ToString() : "S";
        readOnlyAditivo = readOnly;

        if (true == readOnly.Equals("S"))
        {
            bHabilitaFrameDetalhes = false;
        }
        else
        {
            // se for inclusão, habilita o frame detalhe
            if (codigoContrato == -1)
            {
                bHabilitaFrameDetalhes = true;
            }
            else
            {
                // mesmo que não tenha vindo somente leitura, se não tiver acesso para alterar o contrato, desabilita o frame detalhe
                // isto é necessário por que o botão de editar passou a ficar habilitado mesmo que o usuário não possa editar contrato mas possa 'incluir' um comentário, por exemplo.
                if (false == cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoContrato, "null", "CT", 0, "null", "CT_Alt"))
                    bHabilitaFrameDetalhes = false;
                else
                    bHabilitaFrameDetalhes = true;
            }
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        if (!IsPostBack)
        {
            tabControl.ActiveTabIndex = 0;
            montaCampos();
        }

        cDados.aplicaEstiloVisual(Page);
        if (hfGeral.Get("CodigoContrato").ToString() == "-1")
        {
            tabControl.TabPages[1].ClientEnabled = false;
            tabControl.TabPages[2].ClientEnabled = false;
            tabControl.TabPages[3].ClientEnabled = false;
            tabControl.TabPages[4].ClientEnabled = false;
            tabControl.TabPages[5].ClientEnabled = false;
            tabControl.TabPages[6].ClientEnabled = false;
            tabControl.TabPages[7].ClientEnabled = false;
            tabControl.TabPages[8].ClientEnabled = false;
            tabControl.TabPages[9].ClientEnabled = false;

        }
        else
        {
            tabControl.TabPages[1].ClientEnabled = true;
            tabControl.TabPages[2].ClientEnabled = true;
            tabControl.TabPages[3].ClientEnabled = true;
            tabControl.TabPages[4].ClientEnabled = true;
            tabControl.TabPages[5].ClientEnabled = true;
            tabControl.TabPages[6].ClientEnabled = true;
            tabControl.TabPages[7].ClientEnabled = true;
            tabControl.TabPages[8].ClientEnabled = true;
            tabControl.TabPages[9].ClientEnabled = true;

            if (!IsPostBack)
                btnSalvar_MSR.ClientSideEvents.Click = @"function(s, e){ popUp = window.open('../DadosProjeto/popupRelContratos.aspx?codigoContrato=" +
                    hfGeral.Get("CodigoContrato") + @"', 'form', 'resizable=yes,menubar=no,scrollbars=yes,toolbar=no,width=800,height=' + (screen.height - 200)); }";
        }
        //tabControl.TabPages.FindByName("tabSubContrato").Visible = ddlPermitirSub.Text.Equals("Sim");
    }

    private void montaCampos()
    {
        string statusContrato = "A";
        lkItensMedicao.ClientVisible = false;
        if (int.Parse(hfGeral.Get("CodigoContrato").ToString()) != -1)
        {
            DataSet ds = cDados.getDadosContratoExtendido(int.Parse(hfGeral.Get("CodigoContrato").ToString()), "");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lkItensMedicao.ClientVisible = true;
                codigoProjeto = dr["CodigoProjeto"] != null && dr["CodigoProjeto"].ToString() != "" ? int.Parse(dr["CodigoProjeto"].ToString()) : -1;
                indicaObra = dr["IndicaObra"] != null && dr["IndicaObra"].ToString() != "" ? dr["IndicaObra"].ToString() : "N";
                statusContrato = dr["StatusContrato"] != null && dr["StatusContrato"].ToString() != "" ? dr["StatusContrato"].ToString() : "I";

                dtContratoAditivo = dr["DataTermino"] != null && dr["DataTermino"].ToString() != "" ? dr["DataTermino"].ToString() : "";
                dtContratoOriginal = dr["DataTerminoOriginal"] != null && dr["DataTerminoOriginal"].ToString() != "" ? dr["DataTerminoOriginal"].ToString() : "";
                vValorContrato = dr["ValorContrato"] != null && dr["ValorContrato"].ToString() != "" ? dr["ValorContrato"].ToString() : "";
                vValorOriginal = dr["ValorContratoOriginal"] != null && dr["ValorContratoOriginal"].ToString() != "" ? dr["ValorContratoOriginal"].ToString() : "";

                ddlTipoContrato.Value = dr["CodigoTipoContrato"] != null && dr["CodigoTipoContrato"].ToString() != "" ? dr["CodigoTipoContrato"].ToString() : null;
                ddlSituacao.Value = dr["StatusContrato"] != null && dr["StatusContrato"].ToString() != "" ? dr["StatusContrato"].ToString() : null;
                ddlRazaoSocial.Value = dr["CodigoPessoaContratada"] != null && dr["CodigoPessoaContratada"].ToString() != "" ? dr["CodigoPessoaContratada"].ToString() : null;

                ddlMunicipio.Value = dr["CodigoMunicipioObra"] != null && dr["CodigoMunicipioObra"].ToString() != "" ? dr["CodigoMunicipioObra"].ToString() : null;
                ddlsegmento.Value = dr["CodigoSegmentoObra"] != null && dr["CodigoSegmentoObra"].ToString() != "" ? dr["CodigoSegmentoObra"].ToString() : null;
                ddlUnidadeGestora.Value = dr["CodigoUnidadeNegocio"] != null && dr["CodigoUnidadeNegocio"].ToString() != "" ? dr["CodigoUnidadeNegocio"].ToString() : null;
                ddlInicioDeVigencia.Value = dr["DataInicio"];
                ddlTerminoDeVigencia.Value = dr["DataTerminoOriginal"];
                ddlDataBase.Value = dr["DataBaseReajuste"];
                ddlCriterioReajuste.Value = dr["CodigoCriterioReajusteContrato"] != null && dr["CodigoCriterioReajusteContrato"].ToString() != "" ? dr["CodigoCriterioReajusteContrato"].ToString() : null;
                ddlTipoContratacao.Value = dr["CodigoTipoServico"] != null && dr["CodigoTipoServico"].ToString() != "" ? dr["CodigoTipoServico"].ToString() : null;
                ddlOrigem.Value = dr["CodigoOrigemObra"] != null && dr["CodigoOrigemObra"].ToString() != "" ? dr["CodigoOrigemObra"].ToString() : null;
                ddlFonte.Value = dr["CodigoFonteRecursosFinanceiros"] != null && dr["CodigoFonteRecursosFinanceiros"].ToString() != "" ? dr["CodigoFonteRecursosFinanceiros"].ToString() : null;
                ddlCriterioMedicao.Value = dr["CodigoCriterioMedicaoContrato"] != null && dr["CodigoCriterioMedicaoContrato"].ToString() != "" ? dr["CodigoCriterioMedicaoContrato"].ToString() : null;
                ddlAssinatura.Value = dr["DataAssinatura"];
                ddlProjetos.Value = dr["CodigoProjeto"] != null && dr["CodigoProjeto"].ToString() != "" ? int.Parse(dr["CodigoProjeto"].ToString()) : 0;
                ddlTerminoComAditivo.Value = dtContratoAditivo != dtContratoOriginal ? dr["DataTermino"] : null;
                temAditivo = dr["TemAditivo"].ToString();

                if (dr["CodigoUsuarioResponsavel"] != null && dr["CodigoUsuarioResponsavel"].ToString() != "")
                    ddlGestorContrato.JSProperties["cp_NomeGestor"] = dr["GestorContrato"].ToString();

                ddlGestorContrato.Value = dr["CodigoUsuarioResponsavel"] != null && dr["CodigoUsuarioResponsavel"].ToString() != "" ? dr["CodigoUsuarioResponsavel"].ToString() : null;


                txtNumeroContrato.Text = dr["NumeroContrato"].ToString();
                mmObjeto.Text = dr["DescricaoObjetoContrato"].ToString();
                txtnumeroInterno2.Text = dr["NumeroInterno2"].ToString();
                txtnumeroInterno3.Text = dr["NumeroInterno3"].ToString();
                txtGestorContratada.Text = dr["NomeContato"].ToString();
                txtNumeroTrabalhadores.Text = dr["NumeroTrabalhadoresDiretos"].ToString();
                txtOrigemContratada.Text = dr["NomeMunicipioOrigem"].ToString();
                mmObservacoes.Text = dr["Observacao"].ToString();
                txtValorComAditivo.Value = (vValorOriginal != vValorContrato || dr["TemAditivoTEC"].ToString() == "S") ? dr["ValorContrato"] : null;
                txtValorDoContrato.Value = dr["ValorContratoOriginal"];

                //customizações 09/2012
                txtNumeroContratoSAP.Text = dr["NumeroContratoSAP"].ToString();
                ddlPermitirSub.Value = dr["SubContratacao"] != null && dr["SubContratacao"].ToString() != "" ? dr["SubContratacao"].ToString() : null;
                cbNacionalidade.Items[0].Selected = dr["ClassificacaoFornecedor"].ToString().Contains("N");
                cbNacionalidade.Items[1].Selected = dr["ClassificacaoFornecedor"].ToString().Contains("I");
                txtCentroCusto.Text = dr["CentroCusto"].ToString();
                ddlTipoPagamento.Value = dr["TipoPagamento"] != null && dr["TipoPagamento"].ToString() != "" ? dr["TipoPagamento"].ToString() : null;
                ddlTipoCusto.Value = dr["TipoCusto"] != null && dr["TipoCusto"].ToString() != "" ? dr["TipoCusto"].ToString() : null;
                txtContaContabil.Text = dr["ContaContabil"].ToString();
                ddlRetencaoGarantia.Value = dr["RetencaoGarantia"] != null && dr["RetencaoGarantia"].ToString() != "" ? dr["RetencaoGarantia"].ToString() : null;
                txtValorGarantia.Value = dr["ValorGarantia"];
                txtPercGarantia.Value = dr["PercGarantia"];
                ddlDataInicioGarantia.Value = dr["DtInicioGarantia"];//!= null && dr["DtInicioGarantia"].ToString() != "" ? dr["DtInicioGarantia"].ToString() : "";
                ddlDataTerminoGarantia.Value = dr["DtFimGarantia"];// != null && dr["DtFimGarantia"].ToString() != "" ? dr["DtFimGarantia"].ToString() : "";

                //tabControl.TabPages.FindByName("tabSubContrato").Visible = dr["SubContratacao"] != null && dr["SubContratacao"].ToString() != "" && dr["SubContratacao"].ToString() == "S";

                string codigo = ddlCriterioMedicao.Value as string;
                string iniciais = string.IsNullOrEmpty(codigo) ? "" : ObtemIniciaisCriterioMedicaoPeloCodigo(codigo);
                lkItensMedicao.ClientEnabled = iniciais == "PG" || iniciais == "PU";
            }

 
            tabControl.JSProperties["cp_TabFornecedor"] = "./frmCadastroPessoa.aspx?RO=S&PP=N";
            tabControl.JSProperties["cp_TabFornecedor2"] = "./frmObrigacoesContratada.aspx?CC=" + codigoContrato + "&RO=" + readOnly;
            adicionaFrameTabFornecedor(1, "frmFornecedor");

            //tabControl.JSProperties["cp_TabFornecedor2"] = "./frmObrigacoesContratada.aspx?CC=" + codigoContrato + "&RO=" + readOnly;
            tabControl.JSProperties["cp_TabFornecedor3"] = "./frmSubContratacao.aspx?CC=" + codigoContrato + "&RO=" + readOnly + "&ALT=" + alturaFrames;
            adicionaFrameTab(2, "frmFornecedor3");

            tabControl.JSProperties["cp_TabPrevisao"] = "./frmPrevisaoFinanceira.aspx?CC=" + codigoContrato + "&RO=" + readOnlyAditivo + "&ALT=" + alturaFrames;
            adicionaFrameTab(3, "frmPrevisao");

            tabControl.JSProperties["cp_TabParcelas"] = "./frmParcelasContratos.aspx?CC=" + codigoContrato + "&RO=" + readOnly + "&IVG=" + ddlInicioDeVigencia.Text + "&ALT=" + alturaFrames;
            adicionaFrameTab(4, "frmParcelas");

            string tipoOperacao = (readOnly == "S") ? "Consultar" : "";
            tabControl.JSProperties["cp_TabAnexos"] = "../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=CT&ID=" + codigoContrato + "&RO=" + readOnly + "&ALT=" + (int.Parse(alturaFrames) - 25) + "&TO=" + tipoOperacao;
            adicionaFrameTab(5, "frmAnexos");

            tabControl.JSProperties["cp_TabAditivos"] = "./frmAditivosContrato.aspx?CC=" + codigoContrato + "&RO=" + readOnlyAditivo + "&ALT=" + alturaFrames;
            adicionaFrameTab(6, "frmAditivos");

            tabControl.JSProperties["cp_TabComentarios"] = "./frmComentariosContrato.aspx?CC=" + codigoContrato + "&RO=" + readOnly + "&ALT=" + alturaFrames;
            adicionaFrameTab(7, "frmComentarios");

            tabControl.JSProperties["cp_TabAcessorios"] = "./frmAcessorioCalculoPagamentoContrato.aspx?CC=" + codigoContrato + "&RO=" + readOnly + "&ALT=" + alturaFrames;
            adicionaFrameTab(8, "frmAcessorios");

            tabControl.JSProperties["cp_TabReajuste"] = "./frmIndiceReajusteContrato.aspx?CC=" + codigoContrato + "&RO=" + readOnly + "&ALT=" + alturaFrames;
            adicionaFrameTab(9, "frmReajuste");

        }

        if (vlabelNumeroInterno2 == "")
            mostraNumeroInterno2 = "display:none";

        if (vlabelNumeroInterno3 == "")
            mostraNumeroInterno3 = "display:none";

        if (statusContrato == "I")
            readOnly = "S";

        if (statusContrato != "A")
            readOnlyAditivo = "S";

       if(codigoProjeto != -1)
        {
            ddlProjetos.SelectedItem = ddlProjetos.Items.FindByValue(codigoProjeto.ToString());
            lkProjeto.NavigateUrl = "../../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + codigoProjeto + "&NomeProjeto=" + ddlProjetos.Text;
        }
        else
        {
            mostraLinkProjeto = "display:none;";
            lkProjeto.NavigateUrl = "";
        }
        habilitaComponentes(bHabilitaFrameDetalhes, statusContrato, temAditivo);
    }

    private void adicionaFrameTab(int tabIndex, string nomeFrame)
    {
        Literal controle;

        tabControl.TabPages[tabIndex].Controls.Clear();

        string frm = string.Format(@"<iframe id=""{0}"" frameborder=""0"" 
                                        scrolling=""no"" src=""""
                                            width=""100%""></iframe>", nomeFrame);

        controle = cDados.getLiteral(frm);

        tabControl.TabPages[tabIndex].Controls.Add(controle);
    }

    private void adicionaFrameTabFornecedor(int tabIndex, string nomeFrame)
    {
        Literal controle;

        tabControl.TabPages[tabIndex].Controls.Clear();

        string frm = string.Format(@"<iframe id=""{0}"" frameborder=""0"" height=""{1}px"" 
                                        scrolling=""yes"" src=""""
                                            width=""100%""></iframe>
                                     <iframe id=""{0}2"" frameborder=""1"" height=""180px"" style='border:1px solid Gray'
                                        scrolling=""no"" src=""""
                                            width=""100%""></iframe>", nomeFrame
                                                                      , int.Parse(alturaFrames) - 400);

        controle = cDados.getLiteral(frm);

        tabControl.TabPages[tabIndex].Controls.Add(controle);
    }

    private void habilitaComponentes(bool habilita, string statusContrato, string temAditivo)
    {
        
        bool habilitaStatus = habilita;
        bool bTemAditivo = temAditivo == "S";

        bool habilitaValorData = habilita && (statusContrato == "P" || hfGeral.Get("CodigoContrato").ToString() == "-1");

        habilita = habilita && statusContrato != "I";

        if (pUsaNumeracaoAutomatica == "S")
            txtNumeroContrato.ClientEnabled = podeAlterarNumero && habilita;
        else
            txtNumeroContrato.ClientEnabled = habilita;

        ddlTipoContrato.ClientEnabled = habilita;
        ddlSituacao.ClientEnabled = habilitaStatus;
        ddlRazaoSocial.ClientEnabled = habilita;
        mmObjeto.ClientEnabled = habilita;
        ddlUnidadeGestora.ClientEnabled = habilita;
        ddlMunicipio.ClientEnabled = habilita;
        ddlsegmento.ClientEnabled = habilita;
        ddlInicioDeVigencia.ClientEnabled = habilita;
        ddlTerminoDeVigencia.ClientEnabled = habilitaValorData && dtContratoAditivo == dtContratoOriginal && !bTemAditivo;
        ddlAssinatura.ClientEnabled = habilita;
        txtValorDoContrato.ClientEnabled = habilitaValorData && vValorOriginal == vValorContrato && !bTemAditivo;
        ddlDataBase.ClientEnabled = habilita;
        ddlCriterioReajuste.ClientEnabled = habilita;
        ddlTipoContratacao.ClientEnabled = habilita && indicaObra != "S";
        ddlOrigem.ClientEnabled = habilita;
        ddlFonte.ClientEnabled = habilita;
        ddlCriterioMedicao.ClientEnabled = habilita;
        ddlGestorContrato.ClientEnabled = habilita;
        txtGestorContratada.ClientEnabled = false;
        txtNumeroTrabalhadores.ClientEnabled = habilita;
        txtOrigemContratada.ClientEnabled = false;
        mmObservacoes.ClientEnabled = habilita;
        ddlProjetos.ClientEnabled = (codigoProjeto == -1); // habilita && indicaObra != "S";
        txtnumeroInterno2.ClientEnabled = habilita;
        txtnumeroInterno3.ClientEnabled = habilita;
        //customizações 09/2012
        txtNumeroContratoSAP.ClientEnabled = habilita;
        ddlPermitirSub.ClientEnabled = habilita;
        cbNacionalidade.ClientEnabled = habilita;
        txtCentroCusto.ClientEnabled = habilita;
        ddlTipoPagamento.ClientEnabled = habilita;
        ddlTipoCusto.ClientEnabled = habilita;
        txtContaContabil.ClientEnabled = habilita;
        ddlRetencaoGarantia.ClientEnabled = habilita;
        txtValorGarantia.ClientEnabled = habilita;
        txtPercGarantia.ClientEnabled = habilita;
        ddlDataInicioGarantia.ClientEnabled = habilita;
        ddlDataTerminoGarantia.ClientEnabled = habilita;

        btnSalvar.ClientVisible = habilitaStatus;
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/DetalhesContratosExtendido.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "DetalhesContratosExtendido", "_Strings", "ListaContratosExtendido"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 185);

        if (Request.QueryString["Alt"] != null && Request.QueryString["Alt"].ToString() != "")
            alturaFrames = Request.QueryString["Alt"].ToString();
        else
            alturaFrames = (alturaPrincipal).ToString();

        //divRolagem.Attributes.Add("style", string.Format("OVERFLOW:auto; WIDTH: 100%;", alturaFrames));
        
    }

    protected void callbackFornecedor_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (ddlRazaoSocial.SelectedIndex != -1)
        {
            DataSet ds = cDados.getDadosPessoa(int.Parse(ddlRazaoSocial.Value.ToString()), "");

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                callbackFornecedor.JSProperties["cp_municipioFornecedor"] = ds.Tables[0].Rows[0]["MunicipioFornecedor"].ToString();
                callbackFornecedor.JSProperties["cp_contatoFornecedor"] = ds.Tables[0].Rows[0]["NomeContato"].ToString();
            }
            else
            {
                callbackFornecedor.JSProperties["cp_municipioFornecedor"] = "";
                callbackFornecedor.JSProperties["cp_contatoFornecedor"] = "";
            }
        }
    }

    protected void ddlRazaoSocial_Callback(object sender, CallbackEventArgsBase e)
    {
        ddlRazaoSocial.JSProperties["cp_NovoValor"] = "-1";

        if (e.Parameter + "" != "")
        {
            ddlRazaoSocial.JSProperties["cp_NovoValor"] = e.Parameter.ToString();
        }
    }

    protected void ddlGestorContrato_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
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

    protected void ddlGestorContrato_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");


        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    #region Combos

    private void carregaComboTipoContrato()
    {
        DataSet ds = cDados.getTipoContrato(codigoEntidadeUsuarioResponsavel, " AND DataExclusao IS NULL");

        ddlTipoContrato.TextField = "DescricaoTipoContrato";
        ddlTipoContrato.ValueField = "CodigoTipoContrato";
        ddlTipoContrato.DataSource = ds;
        ddlTipoContrato.DataBind();
    }

    private void carregaComboRazaoSocial()
    {
        DataSet ds = cDados.getFornecedores(codigoEntidadeUsuarioResponsavel, "");

        ddlRazaoSocial.TextField = "NomePessoa";
        ddlRazaoSocial.ValueField = "CodigoPessoa";
        ddlRazaoSocial.DataSource = ds;
        ddlRazaoSocial.DataBind();
    }

    private void carregaComboMunicipio()
    {
        DataSet ds = cDados.getMunicipiosObra("");

        ddlMunicipio.TextField = "NomeMunicipio";
        ddlMunicipio.ValueField = "CodigoMunicipio";
        ddlMunicipio.DataSource = ds;
        ddlMunicipio.DataBind();
    }

    private void carregaCombosegmento()
    {
        DataSet ds = cDados.getTipoSegmentoObra(codigoEntidadeUsuarioResponsavel, "");

        ddlsegmento.TextField = "DescricaoSegmentoObra";
        ddlsegmento.ValueField = "CodigoSegmentoObra";
        ddlsegmento.DataSource = ds;
        ddlsegmento.DataBind();
    }

    private void carregaComboCriterioReajuste()
    {
        DataSet ds = cDados.getTipoCriterioReajusteContrato(codigoEntidadeUsuarioResponsavel, "");

        ddlCriterioReajuste.TextField = "DescricaoCriterioReajusteContrato";
        ddlCriterioReajuste.ValueField = "CodigoCriterioReajusteContrato";
        ddlCriterioReajuste.DataSource = ds;
        ddlCriterioReajuste.DataBind();
    }

    private void carregaComboTipoContratacao()
    {
        DataSet ds = cDados.getTipoServico(codigoEntidadeUsuarioResponsavel, "");

        ddlTipoContratacao.TextField = "DescricaoTipoServico";
        ddlTipoContratacao.ValueField = "CodigoTipoServico";
        ddlTipoContratacao.DataSource = ds;
        ddlTipoContratacao.DataBind();
    }

    private void carregaComboOrigem()
    {
        DataSet ds = cDados.getTipoOrigemContrato(codigoEntidadeUsuarioResponsavel, "");

        ddlOrigem.TextField = "DescricaoOrigemObra";
        ddlOrigem.ValueField = "CodigoOrigemObra";
        ddlOrigem.DataSource = ds;
        ddlOrigem.DataBind();
    }

    private void carregaComboPagamentos()
    {
        DataSet ds = cDados.getComboPagamentos(codigoEntidadeUsuarioResponsavel, "");

        ddlTipoPagamento.TextField = "DescricaoTipoPagamentos";
        ddlTipoPagamento.ValueField = "CodigoTipoPagamentos";
        ddlTipoPagamento.DataSource = ds;
        ddlTipoPagamento.DataBind();
    }

    private void carregaComboFonte()
    {
        DataSet ds = cDados.getFontesRecursosFinanceiros(codigoEntidadeUsuarioResponsavel, "");

        ddlFonte.TextField = "NomeFonte";
        ddlFonte.ValueField = "CodigoFonteRecursosFinanceiros";
        ddlFonte.DataSource = ds;
        ddlFonte.DataBind();
    }

    private void carregaComboCriterioMedicao()
    {
        DataSet ds = cDados.getTipoCriterioMedicaoContrato(codigoEntidadeUsuarioResponsavel, "");

        ddlCriterioMedicao.TextField = "DescricaoCriterioMedicaoContrato";
        ddlCriterioMedicao.ValueField = "CodigoCriterioMedicaoContrato";
        ddlCriterioMedicao.DataSource = ds;
        ddlCriterioMedicao.DataBind();
    }

    private void carregaComboGestorContrato()
    {
        ddlGestorContrato.TextField = "NomeUsuario";
        ddlGestorContrato.ValueField = "CodigoUsuario";


        ddlGestorContrato.Columns[0].FieldName = "NomeUsuario";
        ddlGestorContrato.Columns[1].FieldName = "EMail";
        ddlGestorContrato.TextFormatString = "{0}";
    }

    private void carregaComboProjetos()
    {
        string where = string.Format(@" AND Projeto.CodigoEntidade = {0} ORDER BY Projeto.NomeProjeto", codigoEntidadeUsuarioResponsavel);

        if (codigoProjeto != -1 && Request.QueryString["PodeAlterarProjeto"] + "" == "N")
            where = " AND Projeto.CodigoProjeto = " + codigoProjeto + where;

        DataSet ds = cDados.getProjetosContratos(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, where);
        if (cDados.DataSetOk(ds))
        {
            ddlProjetos.DataSource = ds.Tables[0];
            ddlProjetos.DataBind();
        }

        if (codigoProjeto == -1)
        {
            ListEditItem sinProjeto = new ListEditItem(Resources.traducao.nenhum, "0");
            ddlProjetos.Items.Insert(0, sinProjeto);
        }

        if (!IsPostBack && ddlProjetos.Items.Count > 0)
            ddlProjetos.SelectedIndex = 0;
    }

    private int carregaComboUnidadeGestora(string where)
    {
        int qtdUnidades = 0;

        ddlUnidadeGestora.DataSource = null;
        DataSet ds = cDados.getUnidade(where);

        if (cDados.DataSetOk(ds))
        {
            ddlUnidadeGestora.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeGestora.DataSource = ds;
            ddlUnidadeGestora.DataBind();
        }

        return qtdUnidades;
    }

    private void carregaComboStatus()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoStatusComplementarContrato]
              ,[DescricaoStatusComplementarContrato]
              ,[IndicaStatusContrato]
              ,[IniciaisStatusControladoSistema]
          FROM [StatusComplementarContrato]");

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlSituacao.DataSource = ds;
        ddlSituacao.TextField = "DescricaoStatusComplementarContrato";
        ddlSituacao.ValueField = "IndicaStatusContrato";
        ddlSituacao.DataBind();

    }
    #endregion

    #region Inclusão Comentada
    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string msg = "";
        int novoCodigoContrato = -1;

        try
        {
            int tipoContrato = int.Parse(ddlTipoContrato.Value.ToString());
            string numeroContrato = txtNumeroContrato.Text;
            string situacao = ddlSituacao.Value.ToString();
            int razaoSocial = int.Parse(ddlRazaoSocial.Value.ToString());
            string objeto = mmObjeto.Text;
            string codigoUnidadeNegocio = ddlUnidadeGestora.Value != null ? ddlUnidadeGestora.Value.ToString() : "NULL";
            int municipio = int.Parse(ddlMunicipio.Value.ToString());
            int segmento = int.Parse(ddlsegmento.Value.ToString());
            string inicio = (ddlInicioDeVigencia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicioDeVigencia.Date);
            string termino = (ddlTerminoDeVigencia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoDeVigencia.Date);
            string assinatura = (ddlAssinatura.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlAssinatura.Date);
            string valor = txtValorDoContrato.Text;
            string dataBase = (ddlDataBase.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataBase.Date);
            int criterioReajuste = int.Parse(ddlCriterioReajuste.Value.ToString());
            int tipoContratacao = int.Parse(ddlTipoContratacao.Value.ToString());
            int origem = int.Parse(ddlOrigem.Value.ToString());
            int fonte = int.Parse(ddlFonte.Value.ToString());
            int criterioMedicao = int.Parse(ddlCriterioMedicao.Value.ToString());
            int gestor = int.Parse(ddlGestorContrato.Value.ToString());
            string numeroTrabalhadores = txtNumeroTrabalhadores.Text;
            string observacoes = mmObservacoes.Text;
            int codigoProjetoAssociado = (ddlProjetos.Value.ToString() == "0" || ddlProjetos.SelectedIndex == -1) ? -1 : int.Parse(ddlProjetos.Value.ToString());
            string numeroInterno2 = txtnumeroInterno2.Text;
            string numeroInterno3 = txtnumeroInterno3.Text;
            //customizações 09/2012
            string numeroContratoSAP = txtNumeroContratoSAP.Text;
            string permitirSub = ddlPermitirSub.Value != null ? ddlPermitirSub.Value.ToString() : "";
            string centroCusto = txtCentroCusto.Text;
            string classificacaoFornecedor = "";
            int tipoPagamento = ddlTipoPagamento.Value != null ? int.Parse(ddlTipoPagamento.Value.ToString()) : -1;
            string tipoCusto = ddlTipoCusto.Value != null ? ddlTipoCusto.Value.ToString() : "";
            string contaContabil = txtContaContabil.Text;
            string retencaoGarantia = ddlRetencaoGarantia.Value != null ? ddlRetencaoGarantia.Value.ToString() : "";
            string valorGarantia = retencaoGarantia == "CF" ? txtValorGarantia.Text : "0,00";
            string percGarantia = retencaoGarantia == "GR" ? txtPercGarantia.Text : "0,0000";
            string inicioGarantia = (ddlDataInicioGarantia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataInicioGarantia.Date);
            string terminoGarantia = (ddlDataTerminoGarantia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataTerminoGarantia.Date);

            if (cbNacionalidade.Items[0].Selected)
                classificacaoFornecedor = classificacaoFornecedor + cbNacionalidade.Items[0].Value;
            if (cbNacionalidade.Items[1].Selected)
                classificacaoFornecedor = classificacaoFornecedor + cbNacionalidade.Items[1].Value;

            if (hfGeral.Get("UsaNumeracaoAutomatica").ToString() == "N")
            {
                if (hfGeral.Get("ValidaFormatoNumeracaoContrato").ToString() == "S" && !cDados.validaFormatoNumeroContrato(numeroContrato, ""))
                {
                    msg = "O número do contrato não está no formato válido (XX-X-000/0000).\nAltere o número do contrato e tente incluir novamente!";
                    return msg;
                }

                if (cDados.verificaExistenciaContrato(numeroContrato, codigoContrato, codigoEntidadeUsuarioResponsavel, ""))
                {
                    msg = "Já existe um contrato cadastrado com este número. \nAltere o número do contrato e tente incluir novamente!";
                    return msg;
                }

            }

            cDados.incluirContratoExtendido(tipoContrato, numeroContrato, situacao, razaoSocial, objeto, municipio, segmento, inicio, termino, assinatura
                                           , valor, dataBase, criterioReajuste, tipoContratacao, origem, fonte, criterioMedicao, gestor, numeroTrabalhadores
                                           , observacoes, codigoUsuarioResponsavel, codigoUnidadeNegocio, codigoEntidadeUsuarioResponsavel
                                           , codigoProjetoAssociado, ref novoCodigoContrato, ref msg, numeroInterno2, numeroInterno3, numeroContratoSAP, permitirSub
                                           , classificacaoFornecedor, centroCusto, tipoPagamento, tipoCusto, contaContabil, retencaoGarantia, valorGarantia, percGarantia
                                           , inicioGarantia, terminoGarantia);


            if (msg == "")
            {
                hfGeral.Set("CodigoContrato", novoCodigoContrato.ToString());
                callbackSalvar.JSProperties["cp_CodigoContrato"] = novoCodigoContrato;
                codigoContrato = novoCodigoContrato;
                montaCampos();
            }
        }
        catch (Exception ex)
        {
            msg = "Houve um erro ao salvar o registro. \nMensagem Servidor:\n" + ex.Message;
        }

        return msg;

    }
    #endregion

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        string msg = "";

        try
        {
            string numeroContrato = txtNumeroContrato.Text;
            int tipoContrato = int.Parse(ddlTipoContrato.Value.ToString());
            string situacao = ddlSituacao.Value.ToString();
            int razaoSocial = int.Parse(ddlRazaoSocial.Value.ToString());
            string objeto = mmObjeto.Text;
            string codigoUnidadeNegocio = ddlUnidadeGestora.Value != null ? ddlUnidadeGestora.Value.ToString() : "NULL";
            int municipio = int.Parse(ddlMunicipio.Value.ToString());
            int segmento = int.Parse(ddlsegmento.Value.ToString());
            string inicio = (ddlInicioDeVigencia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlInicioDeVigencia.Date);
            string termino = (ddlTerminoDeVigencia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoDeVigencia.Date);
            string assinatura = (ddlAssinatura.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlAssinatura.Date);
            string valor = txtValorDoContrato.Text;
            string dataBase = (ddlDataBase.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataBase.Date);
            int criterioReajuste = int.Parse(ddlCriterioReajuste.Value.ToString());
            int tipoContratacao = int.Parse(ddlTipoContratacao.Value.ToString());
            int origem = int.Parse(ddlOrigem.Value.ToString());
            int fonte = int.Parse(ddlFonte.Value.ToString());
            int criterioMedicao = int.Parse(ddlCriterioMedicao.Value.ToString());
            int gestor = int.Parse(ddlGestorContrato.Value.ToString());
            string numeroTrabalhadores = txtNumeroTrabalhadores.Text;
            string observacoes = mmObservacoes.Text;
            int codigoProjetoAssociado = (ddlProjetos.Value.ToString() == "0" || ddlProjetos.SelectedIndex == -1) ? -1 : int.Parse(ddlProjetos.Value.ToString());
            string valorcAditivo = txtValorComAditivo.Text == "" ? txtValorDoContrato.Text : txtValorComAditivo.Text;
            string dtTerminocAditivo = ddlTerminoComAditivo.Text == "" ? termino : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlTerminoComAditivo.Date);
            string numeroInterno2 = txtnumeroInterno2.Text;
            string numeroInterno3 = txtnumeroInterno3.Text;
            //customizações 09/2012
            string numeroContratoSAP = txtNumeroContratoSAP.Text;
            string permitirSub = ddlPermitirSub.Value != null ? ddlPermitirSub.Value.ToString() : "";
            string centroCusto = txtCentroCusto.Text;
            int tipoPagamento = ddlTipoPagamento.Value != null ? int.Parse(ddlTipoPagamento.Value.ToString()) : -1;
            string tipoCusto = ddlTipoCusto.Value != null ? ddlTipoCusto.Value.ToString() : "";
            string contaContabil = txtContaContabil.Text;
            string retencaoGarantia = ddlRetencaoGarantia.Value != null ? ddlRetencaoGarantia.Value.ToString() : "NULL";
            string valorGarantia = retencaoGarantia == "CF" ? txtValorGarantia.Text : "0,00";
            string percGarantia = retencaoGarantia == "GR" ? txtPercGarantia.Text : "0,0000";
            string inicioGarantia = (ddlDataInicioGarantia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataInicioGarantia.Date);
            string terminoGarantia = (ddlDataTerminoGarantia.Text == "") ? "NULL" : string.Format("CONVERT(DateTime, '{0:dd/MM/yyyy}', 103)", ddlDataTerminoGarantia.Date);


            string classificacaoFornecedor = "";
            if (cbNacionalidade.Items[0].Selected)
                classificacaoFornecedor = classificacaoFornecedor + cbNacionalidade.Items[0].Value;
            if (cbNacionalidade.Items[1].Selected)
                classificacaoFornecedor = classificacaoFornecedor + cbNacionalidade.Items[1].Value;


            if (hfGeral.Get("ValidaFormatoNumeracaoContrato").ToString() == "S" && !cDados.validaFormatoNumeroContrato(numeroContrato, ""))
            {
                msg = "O número do contrato não está no formato válido (XX-X-000/0000).\nAltere o número do contrato e tente incluir novamente!";
                return msg;
            }
            if (hfGeral.Get("UsaNumeracaoAutomatica").ToString() == "S" && cDados.validaSequenciaContrato(numeroContrato, "", codigoEntidadeUsuarioResponsavel))
            {
                msg = "Você não pode informar um sequencial de contrato maior que o controlado pelo sistema.\nAltere o número do contrato e tente incluir novamente!";
                return msg;
            }

            if (cDados.verificaExistenciaContrato(numeroContrato, codigoContrato, codigoEntidadeUsuarioResponsavel, ""))
            {
                msg = "Já existe um contrato cadastrado com este número. \nAltere o número do contrato e tente incluir novamente!";
                return msg;
            }


            //if (permitirSub == "S" && !cDados.verificaExistenciaSubContratada(codigoContrato, ""))
            //{
            //    msg = "Não foram inseridos registros de SubContratadas. \nInforme e tente incluir novamente!";
            //    return msg;
            //}


            cDados.atualizaContratoExtendido(codigoContrato, tipoContrato, situacao, razaoSocial, objeto, municipio, segmento, inicio, termino, assinatura
                                            , valor, dataBase, criterioReajuste, tipoContratacao, origem, fonte, criterioMedicao, gestor, numeroTrabalhadores
                                            , observacoes, codigoUsuarioResponsavel, codigoUnidadeNegocio
                                            , codigoProjetoAssociado, valorcAditivo, dtTerminocAditivo, ref msg, numeroContrato, numeroInterno2, numeroInterno3, numeroContratoSAP, permitirSub
                                            , classificacaoFornecedor, centroCusto, tipoPagamento, tipoCusto, contaContabil, retencaoGarantia, valorGarantia, percGarantia
                                            , inicioGarantia, terminoGarantia);
        }
        catch (Exception ex)
        {
            msg = "Houve um erro ao salvar o registro.\nMensagem Servidor:\n" + ex.Message;
        }
        return msg;
    }

    protected void callbackSalvar_Callback1(object sender, CallbackEventArgsBase e)
    {
        string msgRetorno = "";
        string statusGravacao = "";

        //callbackSalvar.JSProperties["cp_CodigoContrato"] = codigoContrato;

        if (codigoContrato == -1)
        {
            msgRetorno = persisteInclusaoRegistro();

            if (msgRetorno == "")
            {
                statusGravacao = "1";
                tabControl.TabPages[1].ClientEnabled = true;
                tabControl.TabPages[2].ClientEnabled = true;
                tabControl.TabPages[3].ClientEnabled = true;
                tabControl.TabPages[4].ClientEnabled = true;
                tabControl.TabPages[5].ClientEnabled = true;
                tabControl.TabPages[6].ClientEnabled = true;
                tabControl.TabPages[7].ClientEnabled = true;
                tabControl.TabPages[8].ClientEnabled = true;
                tabControl.TabPages[9].ClientEnabled = true;

                codigoContrato = int.Parse(hfGeral.Get("CodigoContrato").ToString());

                msgRetorno = "Contrato incluído com sucesso!";
            }
        }
        else
        {
            msgRetorno = persisteEdicaoRegistro();

            if (msgRetorno == "")
            {
                statusGravacao = "1";
                msgRetorno = "Contrato alterado com sucesso!";
            }
        }

        callbackSalvar.JSProperties["cp_Status"] = statusGravacao;
        callbackSalvar.JSProperties["cp_MSG"] = msgRetorno;

        montaCampos();
    }
    protected void callbackCriteriosMedicao_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string codigo = e.Parameter;
        string iniciais = ObtemIniciaisCriterioMedicaoPeloCodigo(codigo);
        e.Result = iniciais;
    }

    private string ObtemIniciaisCriterioMedicaoPeloCodigo(string codigo)
    {
        string comandoSql = string.Format(@"
        SELECT tcm.IniciaisCriterioMedicao
          FROM TipoCriterioMedicaoContrato tcm
         WHERE tcm.CodigoCriterioMedicaoContrato = {0}", codigo);
        DataSet dsTemp = cDados.getDataSet(comandoSql);
        string iniciais = dsTemp.Tables[0].Rows[0]["IniciaisCriterioMedicao"] as string;
        return iniciais;
    }



}