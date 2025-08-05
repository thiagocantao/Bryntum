using CDIS.Properties;
using DevExpress.Web;
using System;
//using CDIS;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDIS
{
    public class Workflow : System.Web.UI.Page
    {
        /* ============================================================================================================================================================================
         * ATEN��O: 10/07/2015 - Altera��es para o tratamento da certifica��o digital:
         * ============================================================================================================================================================================
         *              => Inclus�o do ASPxPopupControl "pcPaginaExterna" no m�todo "ObtemPainelExecucaoEtapa()"
         *                 A certifica��o digital ser� feita em uma p�gina ASPX desenvolvida fora deste componente. Esta p�gian ser� aberta dentro do popup "pcPaginaExterna".
         *                 O evento "Closing" (pcPaginaExterna.ClientSideEvents) ser� respons�vel por passar o fluxo para a pr�xima etapa, caso o retorno "rcd" seja igual a "ad_OK".

         *              => Inclus�o da vari�vel "indicaCertificacaoDigital" no m�todo "insereBotoesEncerramento";
         *              => Altera��o da assinatura da fun��o javascript "podeAvancarFluxo()" no m�todo constroiInterfaceWorkflow_FluxoUnico, para receber a vari�vel citada acima;
         *              => Inclus�o da fun��o javascript "RealizaAssinaturaDigital()" para abrir o popup com a p�gina aspx externa. 
         *                                                Esta fun��o sempre retorna "FALSE", o tratamento do "TRUE" ser� feito ao fechar o popup (evento Closing) citado acima;
         * 
         * =========================================================================================================================================================================== */

        private ClasseDados dados;
        private bool isPostBack;
        private int codigoUsuarioInteracao;

        private string cssFilePath;
        private string cssPostFix;
        private string formularioEdicao;
        private string urlDestinoAposExecutarAcao;
        private string targetRedirecionamento;
        private int alturaFrame = 400;
        private int larguraFrame;
        public Page myPage;
        private bool mostrarBotoesAcao = false;
        private bool indicaPopup = false;

        // dados do workflow
        // -------------------------------------
        private int _CodigoWorkflow;
        private int _CodigoTipoWorkflow;
        private string _NomeFluxo;
        private int _CodigoProjeto = -1;
        private string _NomeSolicitante;

        // dados da instancia de workflow
        // -------------------------------------
        private int _CodigoInstanciaWorkflow;
        private string _NomeInstaciaWorkflow;
        private int _CodigoEtapa;
        private int _OcorrenciaEtapa;
        private string _NomeEtapa;
        private string _IndicaOcultaBotoesAcao;
        private string _NomeEtapaAnterior;
        private string _VersaoFluxo;
        private string _DataInicioInstancia;
        private string _IniciaisFormObrigatorio;

        private bool _ModoConsulta = false;
        private bool _HabilitaTramitacaoParecer = false;
        private bool _HabilitaTramitacaoFormulario = false;

        ASPxHiddenField hfGeralWorkflow = new ASPxHiddenField();
        ASPxCallbackPanel pnWorkflow = new ASPxCallbackPanel();
        ASPxPopupControl pcPaginaExterna = new ASPxPopupControl();

        public Workflow(ClasseDados dados, int codigoUsuarioInteracao, bool isPostBack, int CodigoWorkflow, int? codigoInstanciaWorkflow, int? codigoEtapaWorkflow, int? sequenciaOcorrencia, int? codigoProjeto, Page page)
        {
            int CodigoEtapaAtual;
            int OcorrenciaAtual;

            this.dados = dados;
            this.codigoUsuarioInteracao = codigoUsuarioInteracao;
            this.isPostBack = isPostBack;
            this.myPage = page;
            DataSet ds = null;
            DataTable dt = null;
            _NomeSolicitante = "";

            // Se o codigoInstanciaWorkflow != null, os valores ser�o buscados a partir da tabela InstanciasWorkflows
            if (codigoInstanciaWorkflow != null)
            {
                if (!codigoEtapaWorkflow.HasValue)
                    codigoEtapaWorkflow = -1;

                if (!sequenciaOcorrencia.HasValue)
                    sequenciaOcorrencia = -1;

                string comandoSQL = string.Format(
                    @"SELECT  
		                i.[CodigoWorkflow]
	                , CAST( ISNULL(i.[NumeroProtocolo] + ' - ' + i.[NomeInstancia],i.[NomeInstancia])  AS varchar(250) ) AS [NomeInstancia]
	                , i.[EtapaAtual]
	                , f.[NomeFluxo]
	                , e.[NomeEtapaWf]
	                , e.[IndicaOcultaBotoesAcao]
	                , i.[OcorrenciaAtual]
	                , i.[IdentificadorProjetoRelacionado]
                    , u.NomeUsuario as NomeUsuarioCriadorInstancia
			        , eAnterior.[NomeEtapaWf]	as NomeEtapaAnterior
                    , '( V.'+CONVERT(varchar(4),wf.versaoworkflow	)+' )'  as VersaoFluxo
                    , convert(varchar(10), i.datainicioinstancia, 103 )+' '+convert(varchar(8), i.datainicioinstancia, 114 ) as DataInicioInstancia 	
                FROM 
	                [EtapasInstanciasWf]									AS [ei]
            		
		                INNER JOIN [InstanciasWorkflows]		AS [i]	ON 
			                (			i.[CodigoWorkflow]			= ei.[CodigoWorkflow] 
				                AND	i.[CodigoInstanciaWf]		= ei.[CodigoInstanciaWf]	)
            					
			                INNER JOIN [Workflows]						AS [wf] ON 
				                (	wf.[CodigoWorkflow]				= i.[CodigoWorkflow] )
            						
				                INNER JOIN [Fluxos]							AS [f] ON 
					                ( f.[CodigoFluxo]					= wf.[CodigoFluxo] )
            							
		                INNER JOIN [EtapasWf]							AS [e] ON 
			                (			e.[CodigoWorkflow]			= ei.[CodigoWorkflow] 
				                AND e.[CodigoEtapaWf]				= ei.[CodigoEtapaWf] )
                        INNER JOIN Usuario                             AS [u] ON
				            ( u.CodigoUsuario                       = i.IdentificadorUsuarioCriadorInstancia)
				            

					    LEFT JOIN [dbo].[EtapasInstanciasWf]				AS ieAnterior ON 
						(			ieAnterior.[CodigoWorkflow]							= i.[CodigoWorkflow] 
							AND ieAnterior.[CodigoInstanciaWf]						= i.[CodigoInstanciaWf]
							AND ieAnterior.[SequenciaOcorrenciaEtapaWf]	= i.[OcorrenciaAtual]-1 )
							
					    LEFT JOIN [dbo].[EtapasWf]									AS eAnterior ON
						(			eAnterior.[CodigoWorkflow]	= ieAnterior.[CodigoWorkflow] 
							AND eAnterior.[CodigoEtapaWf]		= ieAnterior.[CodigoEtapaWf] 			)				            
            						
             WHERE 
		            ei.[CodigoWorkflow]		        = {0}
                AND	ei.[CodigoInstanciaWf]	        = {1}
                AND	ei.[CodigoEtapaWf]	            = {2}
                AND	ei.[SequenciaOcorrenciaEtapaWf]	= {3} 
", CodigoWorkflow, codigoInstanciaWorkflow.Value, codigoEtapaWorkflow.Value, sequenciaOcorrencia.Value);
                ds = dados.getDataSet(comandoSQL);
                if ((ds == null) || (ds.Tables.Count < 1) || (ds.Tables[0].Rows.Count < 1))
                {
                    _CodigoWorkflow = -1;
                    _NomeFluxo = "";
                    _CodigoTipoWorkflow = 1;
                    _CodigoInstanciaWorkflow = -1;
                    _NomeInstaciaWorkflow = "";
                    _CodigoEtapa = -1;
                    _OcorrenciaEtapa = -1;
                    _NomeEtapa = "";
                    _IndicaOcultaBotoesAcao = "";
                    _NomeEtapaAnterior = "";
                    _VersaoFluxo = "";
                    _DataInicioInstancia = "";
                    _CodigoProjeto = -1;
                    _NomeSolicitante = "";
                } // if ( (ds == null) || ...
                else
                {
                    dt = ds.Tables[0];

                    _CodigoWorkflow = CodigoWorkflow;
                    _NomeFluxo = dt.Rows[0]["NomeFluxo"].ToString();
                    _CodigoTipoWorkflow = 1;// VAMOS ARRUMAR ISSO... UM DIA. int.Parse(dt.Rows[0]["CodigoTipo"].ToString());
                    _CodigoInstanciaWorkflow = codigoInstanciaWorkflow.Value;
                    _NomeInstaciaWorkflow = dt.Rows[0]["NomeInstancia"].ToString();
                    _CodigoEtapa = codigoEtapaWorkflow.Value;
                    _OcorrenciaEtapa = sequenciaOcorrencia.Value;
                    _NomeEtapa = dt.Rows[0]["NomeEtapaWf"].ToString();
                    _IndicaOcultaBotoesAcao = dt.Rows[0]["IndicaOcultaBotoesAcao"].ToString();
                    _NomeEtapaAnterior = dt.Rows[0]["NomeEtapaAnterior"].ToString();
                    _VersaoFluxo = dt.Rows[0]["VersaoFluxo"].ToString();
                    _DataInicioInstancia = dt.Rows[0]["DataInicioInstancia"].ToString();
                    _NomeSolicitante = dt.Rows[0]["NomeUsuarioCriadorInstancia"].ToString();

                    // se o WF N�O estiver vinculado a nenhum projeto, seta a vari�vel _CodigoProjeto para -1, caso contr�rio, seta com o c�digo do projeto vinculado
                    _CodigoProjeto = dt.Rows[0]["IdentificadorProjetoRelacionado"].ToString().Trim() != "" ? int.Parse(dt.Rows[0]["IdentificadorProjetoRelacionado"].ToString()) : -1;

                    CodigoEtapaAtual = dt.Rows[0]["EtapaAtual"].ToString().Trim() != "" ? int.Parse(dt.Rows[0]["EtapaAtual"].ToString()) : -1;
                    OcorrenciaAtual = dt.Rows[0]["OcorrenciaAtual"].ToString().Trim() != "" ? int.Parse(dt.Rows[0]["OcorrenciaAtual"].ToString()) : -1;

                    // se a ocorr�ncia que est� sendo solicitada � a ocorr�ncia atual da etapa,
                    // verifica como vai ser a intera��o do usu�rio (de a��o ou de consulta, bem como a quest�o de tramita��o de parecer ou de formul�rio, se for o caso)
                    if (OcorrenciaAtual == _OcorrenciaEtapa)
                        verificaModoInteracaoTela(_CodigoWorkflow, _CodigoInstanciaWorkflow, _OcorrenciaEtapa, _CodigoEtapa, codigoUsuarioInteracao.ToString());
                    else
                    {
                        _ModoConsulta = true;
                        _HabilitaTramitacaoParecer = false;
                        _HabilitaTramitacaoFormulario = false;
                    }
                } // else ( (ds == null) || ...
            } // if (codigoInstanciaWorkflow != null)
            else
            {
                // N�o foi passado o c�digo da inst�ncia, ent�o => novo fluxo 
                string comandoSQL = string.Format(
                    @"SELECT  fluxos.NomeFluxo, CodigoEtapaInicial, EtapasWf.NomeEtapaWf
			       , eAnterior.[NomeEtapaWf] as NomeEtapaAnterior	
                    , '( V.'+CONVERT(varchar(4), versaoworkflow	)+' )'  as VersaoFluxo
                    , null	as DataInicioInstancia 
                    , EtapasWf.[IndicaOcultaBotoesAcao]
                        FROM  Workflows INNER JOIN
                              fluxos on (fluxos.CodigoFluxo = Workflows.CodigoFluxo) inner join
                              EtapasWf ON Workflows.CodigoWorkflow = EtapasWf.CodigoWorkflow AND Workflows.CodigoEtapaInicial = EtapasWf.CodigoEtapaWf AND 
                                          Workflows.CodigoWorkflow = EtapasWf.CodigoWorkflow
							
					    LEFT JOIN [dbo].[EtapasWf]									AS eAnterior ON
						(			eAnterior.[CodigoWorkflow]	= Workflows.[CodigoWorkflow] 
							AND eAnterior.[CodigoEtapaWf]		= Workflows.CodigoEtapaInicial-1			)	
                       WHERE  Workflows.CodigoWorkflow = {0}", CodigoWorkflow);
                ds = dados.getDataSet(comandoSQL);
                if ((ds == null) || (ds.Tables.Count < 1) || (ds.Tables[0].Rows.Count < 1))
                {
                    _CodigoWorkflow = -1;
                    _NomeFluxo = "";
                    _CodigoTipoWorkflow = 1;
                    _CodigoInstanciaWorkflow = -1;
                    _NomeInstaciaWorkflow = "";
                    _CodigoEtapa = -1;
                    _OcorrenciaEtapa = -1;
                    _NomeEtapa = "";
                    _IndicaOcultaBotoesAcao = "";
                    _NomeEtapaAnterior = "";
                    _VersaoFluxo = "";
                    _DataInicioInstancia = "";
                    _CodigoProjeto = -1;
                } // if ( (ds == null) || ...
                else
                {
                    dt = ds.Tables[0];

                    _CodigoWorkflow = CodigoWorkflow;
                    _NomeFluxo = dt.Rows[0]["NomeFluxo"].ToString();
                    _CodigoTipoWorkflow = 1;// VAMOS ARRUMAR ISSO... UM DIA. int.Parse(dt.Rows[0]["CodigoTipo"].ToString());
                    _CodigoInstanciaWorkflow = -1;  // inst�ncia ainda n�o criada
                    _NomeInstaciaWorkflow = "";
                    _CodigoEtapa = int.Parse(dt.Rows[0]["CodigoEtapaInicial"].ToString());
                    _OcorrenciaEtapa = 1; // inicia na seq��ncia 1
                    _NomeEtapa = dt.Rows[0]["NomeEtapaWf"].ToString();
                    _IndicaOcultaBotoesAcao = dt.Rows[0]["IndicaOcultaBotoesAcao"].ToString();
                    _NomeEtapaAnterior = dt.Rows[0]["NomeEtapaAnterior"].ToString();
                    _VersaoFluxo = dt.Rows[0]["VersaoFluxo"].ToString();
                    _DataInicioInstancia = dt.Rows[0]["DataInicioInstancia"].ToString();

                    // se foi informado um c�digo de Projeto. Esta informa��o ser� repassada para os formul�rios.
                    if (codigoProjeto.HasValue)
                        _CodigoProjeto = codigoProjeto.Value;

                    // como se trata de novo fluxo, assume que o usu�rio poder� 'criar' o fluxo
                    _ModoConsulta = false;
                    _HabilitaTramitacaoParecer = false;
                    _HabilitaTramitacaoFormulario = false;

                } // else ( (ds == null) || ...
            } // else (codigoInstanciaWorkflow != null)

            hfGeralWorkflow.Set("CodigoInstanciaWf", _CodigoInstanciaWorkflow);
            hfGeralWorkflow.Set("SequenciaOcorrenciaEtapaWf", _OcorrenciaEtapa);
            hfGeralWorkflow.Set("CodigoEtapaWf", _CodigoEtapa);
        }

        /// <summary>
        /// Devolve true caso o workflow esteja no modo apenas de consulta
        /// </summary>
        /// <returns>bool</returns>
        public bool ModoConsulta()
        {
            return _ModoConsulta;
        }

        /// <summary>
        /// Devolve true caso seja para habilitar o formul�rio de tramita��o de parecer
        /// </summary>
        /// <returns>bool</returns>
        public bool HabilitaTramitacaoParecer()
        {
            return _HabilitaTramitacaoParecer;
        }

        /// <summary>
        /// Devolve true caso seja para habilitar o formul�rio de tramita��o de formul�rio
        /// </summary>
        /// <returns>bool</returns>
        public bool HabilitaTramitacaoFormulario()
        {
            return _HabilitaTramitacaoFormulario;
        }

        /// <summary>
        /// Devolve um painel para mostrar as informa��es da etapa do fluxo
        /// </summary>
        /// <remarks>
        /// O painel devolvido cont�m todos os formul�rios da etapa. 
        /// Se a etapa estiver num estado em que pode interagir e o usu�rio em quest�o (codigoUsuarioInteracao)
        /// tiver acesso de a��o na etapa, o painel conter� tamb�m os bot�es de op��es das a��es da etapa
        /// </remarks>
        /// <param name="CssFilePath"></param>
        /// <param name="CssPostFix"></param>
        /// <param name="FormularioEdicao"></param>
        /// <param name="larguraFrame"></param>
        /// <param name="alturaFrame"></param>
        /// <param name="urlDestinoAposExecutarAcao"></param>
        /// <returns></returns>
        public Control ObtemPainelExecucaoEtapa(string CssFilePath, string CssPostFix, string FormularioEdicao, int larguraFrame, int alturaFrame, string urlDestinoAposExecutarAcao, string targetRedirecionamento, bool mostraBotoes, bool indicaPopup)
        {
            this.cssPostFix = CssPostFix;
            this.formularioEdicao = FormularioEdicao;
            this.alturaFrame = alturaFrame;
            this.larguraFrame = larguraFrame - 5;
            this.urlDestinoAposExecutarAcao = urlDestinoAposExecutarAcao;
            this.targetRedirecionamento = targetRedirecionamento;
            this.mostrarBotoesAcao = (_IndicaOcultaBotoesAcao.Equals("S") ? false : mostraBotoes);
            this.indicaPopup = indicaPopup;

            if (formularioEdicao.IndexOf('?') < 0)
                this.formularioEdicao += "?";

            Panel pnPrincipal = new Panel();
            pnPrincipal.Attributes.Add("Style", "border: #CCCCCC 1px solid; text-align: left;margin-left:3px;margin-top:3px;background-color: #EFEFEF;");
            pnPrincipal.Width = new Unit("99%");// new Unit("840px");

            // se o tipo for "Fluxo �nico de Projeto"
            if (_CodigoTipoWorkflow == 1)
            {
                constroiInterfaceWorkflow_FluxoUnico(pnPrincipal);
            }
            // se o tipo for "Lista", controi a grid
            else if (_CodigoTipoWorkflow == 2)
            {

            }

            // -------------------------------------------------
            //              CERTIFICA��O DIGITAL
            // -------------------------------------------------
            // Adiciona o ASPxPopupControl - "pcPaginaExterna" 
            pcPaginaExterna.ID = "pcPaginaExterna";
            pcPaginaExterna.ClientInstanceName = "pcPaginaExterna";
            pcPaginaExterna.HeaderText = "Runtime Popup";
            pcPaginaExterna.CloseAction = CloseAction.CloseButton;
            pcPaginaExterna.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;
            pcPaginaExterna.PopupVerticalAlign = PopupVerticalAlign.WindowCenter;
            pcPaginaExterna.Modal = true;
            pcPaginaExterna.AllowResize = true;
            pcPaginaExterna.AllowDragging = true;
            pcPaginaExterna.ShowMaximizeButton = false;
            pcPaginaExterna.ShowCloseButton = false;
            // "rcd" = retorno certificacao digital
            pcPaginaExterna.ClientSideEvents.Shown =
                @"function(s, e) {
                        hfGeralWorkflow.Set('rcd', '0');
                        pnBotoes.SetVisible(false);
                  }";
            pcPaginaExterna.ClientSideEvents.Closing =
                @"function(s, e) {
                        var rcd = hfGeralWorkflow.Get('rcd');
                        if (rcd == 'ad_OK')
                        {
                            varVerificacaoFluxo = true;
                            var codigoAcao = hfGeralWorkflow.Get('rcd_acao');
                            pnWorkflow.PerformCallback(codigoAcao);
                        }
                        else
                        {
                           pnBotoes.SetVisible(true);
                           window.top.mostraMensagem('" + Resources.A��oCanceladaPeloUsu�rio + @"', 'erro', false, false, null);
                        }
                  }";
            pnPrincipal.Controls.Add(pcPaginaExterna);
            hfGeralWorkflow.Set("rcd", "0");
            return pnPrincipal;
        }

        private void constroiInterfaceWorkflow_FluxoUnico(Panel pnPrincipal)
        {
            myPage.ClientScript.RegisterClientScriptBlock(GetType(), "clie",
                @"<script type='text/javascript'>
                    var mudaAba = false;
                    function executaAcaoPosSalvarFluxo()
                    {
                                    
                    }

                     var mensagemCustomizada = '';

                    function podeAvancarFluxo(codigoWorkflow, codigoEtapa, codigoAcao, indicaCertificacaoDigital, textoAcao)
                    {
                        var bPodeAvancar = true;
                        if (true == faltaCriarAlgumFormulario())
                            bPodeAvancar = false;
                        else {
                            if (window.frames['wfFormularios'].window.frames['wfTela'])
                            {
                                if (undefined == window.frames['wfFormularios'].frames['wfTela'].verificaAvancoWorkflow)
                                    bPodeAvancar = true;
                                else
                                    bPodeAvancar = window.frames['wfFormularios'].frames['wfTela'].verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao);
                            }
                            else
                                bPodeAvancar = window.frames['wfFormularios'].verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao);
                        }

                        if(frmCriteriosPendente != null && frmCriteriosPendente != '')
                        {
                            window.top.mostraMensagem('" + Resources.ParaProsseguirComOFluxo�Necess�rioPreenche + @"', 'erro', true, false, null);
                            bPodeAvancar = false;
                        }
                        
                        var msg;
                        if (codigoAcao == 1001)
                            msg = '" + Resources.ConfirmaATramita��oDoParecer + @"';
                        else if (codigoAcao == 1002)
                            msg = '" + Resources.ConfirmaAFinaliza��oDoPreenchimentoDoSForm + @"';
                        else 
                            msg = mensagemCustomizada + '" + Resources.ConfirmaAFinaliza��oDestaEtapaBrBrOp��oSel + @" `'+textoAcao+'�';

                        if(bPodeAvancar)
                        {
                            var funcObj = { avancaFluxo: function(codigoWorkflow, codigoEtapa, codigoAcao, indicaCertificacaoDigital, textoAcao){ AvancaFluxoOK(codigoWorkflow, codigoEtapa, codigoAcao, indicaCertificacaoDigital, textoAcao); } }

                            window.top.mostraMensagem(msg, 'confirmacao', true, true, function(){funcObj['avancaFluxo'](codigoWorkflow, codigoEtapa, codigoAcao, indicaCertificacaoDigital, textoAcao)});
                        }

                        
                        return bPodeAvancar;
                    }

                    function AvancaFluxoOK(codigoWorkflow, codigoEtapa, codigoAcao, indicaCertificacaoDigital, textoAcao)
                    {
                        var bPodeAvancar = true;

                        if(bPodeAvancar)
	                    { 
                             if (indicaCertificacaoDigital=='N')
                                bPodeAvancar = true;  
                            else
                                bPodeAvancar = RealizaAssinaturaDigital(codigoWorkflow, codigoAcao); 
                        } 
                        else
                        {
                            bPodeAvancar = false;
                        }
                        varVerificacaoFluxo = bPodeAvancar;

                        if(bPodeAvancar)
                        {
                            pnBotoes.SetVisible(false);
                            pnWorkflow.PerformCallback(codigoAcao);
                        }

                    }

                    function faltaCriarAlgumFormulario()
                    {
                        var formularios = hfGeralWorkflow.Get('Formularios').split(';');
                        for(controle = 0; controle<formularios.length;controle++)
                        {
                            var codigoModeloFormulario = formularios[controle];
                            if (codigoModeloFormulario != '')
                            {
                                var ordemFormulario = controle;
                                var FormID = hfGeralWorkflow.Get('FormID' + codigoModeloFormulario + '_' + ordemFormulario);
                                var FormNM = hfGeralWorkflow.Get('FormNM' + codigoModeloFormulario + '_' + ordemFormulario);
                                var FormOB = hfGeralWorkflow.Get('FormOB' + codigoModeloFormulario + '_' + ordemFormulario);
                                var FormRO = hfGeralWorkflow.Get('FormRO' + codigoModeloFormulario + '_' + ordemFormulario);
                                if (FormOB == 'S' && FormID=='0' && FormRO == 'N')
                                {
                                    window.top.mostraMensagem('" + Resources.AAba + @" ""' + FormNM + '"" " + Resources.precisaSerSalva + @"', 'erro', true, false, null);
                                    return true;
                                }
                            }
                        }
                        return false;
                    }

                    function selecionaFormulario(s,e)
                    {
                        if (tcLinksFormuarios.GetActiveTab() != e.tab)
                        {
                            var ordemFormulario = e.tab.index;
                            var codigoModeloFormulario = e.tab.name.substring(3);
                            var codigoFormulario = hfGeralWorkflow.Get('FormID' + codigoModeloFormulario + '_' + ordemFormulario);
                            if (codigoFormulario == '0')
                               codigoFormulario = '';
                            else 
                               codigoFormulario = '&CF='+ codigoFormulario;
                            
                            var readOnly = hfGeralWorkflow.Get('FormRO' + codigoModeloFormulario + '_' + ordemFormulario);
                            if (readOnly=='N')
                            {
                                readOnly = '';
                                var CodigoInstanciaWf = hfGeralWorkflow.Get('CodigoInstanciaWf');

                                // 01/11/2011 - Para corrigir o problema de criar dois fluxos no SIG. 
                                //              Aconteceu qdo foi criado dois formul�rios no fluxo de nova iniciativa
                                // se n�o tem uma instancia do fluxo salva no banco de dados, com exce��o da primeira aba, todas as outras ser�o somente leitura
                                if (CodigoInstanciaWf<0 && ordemFormulario>0)
                                    readOnly = '&RO=S';
                            }
                            else  
                               readOnly = '&RO=S';
                            var codigoInstancia       = '&CIWF=" + _CodigoInstanciaWorkflow + @"'
                            var codigoEtapa           = '&CEWF=" + _CodigoEtapa + @"'
                            var codigoOcorrenciaAtual = '&COWF=" + _OcorrenciaEtapa + @"'
                            var codigoProjeto = " + (_CodigoProjeto != -1 ? @"'&CPWF=" + _CodigoProjeto : "'") + @"'
                            var iniciaisFrmObr = '" + _IniciaisFormObrigatorio + @"'
                            var destino = window.top.pcModal.cp_Path + '" + formularioEdicao + @"CMF='+codigoModeloFormulario+codigoFormulario+readOnly+codigoInstancia+codigoEtapa+codigoOcorrenciaAtual+codigoProjeto+iniciaisFrmObr;
                            window.open(destino, 'wfFormularios');
                        }
                    }

                    function RealizaAssinaturaDigital(codigoWorkflow, codigoAcao)
                    {
                        var codigoInstancia       = '&CIWF=" + _CodigoInstanciaWorkflow + @"'
                        var codigoEtapa           = '&CEWF=" + _CodigoEtapa + @"'
                        var codigoOcorrenciaAtual = '&COWF=" + _OcorrenciaEtapa + @"'

                        var url = './_CertificadoDigital/assinatura.aspx?CW=' + codigoWorkflow + codigoInstancia + codigoEtapa + codigoOcorrenciaAtual + '&ca=' + codigoAcao;
                        var altura = 350;
                        var largura = 500
                        hfGeralWorkflow.Set('rcd_acao', codigoAcao);
                        pcPaginaExterna.SetContentUrl(url);
                        pcPaginaExterna.SetHeight(altura);
                        pcPaginaExterna.SetWidth(largura);
                        pcPaginaExterna.SetHeaderText('" + Resources.AssinaturaDigital + @"')
                        pcPaginaExterna.Show();    

                        // ATEN��O: Deve SEMPRE retornar FALSE
                        return false;
                    }
                  </script>");

            int tamMaxTipoFluxo, tamMaxNomeEtapa, tamMaxNomeSolicitante, tamMaxNomeInstancia;
            if (this.larguraFrame > 1100)
            {
                tamMaxTipoFluxo = 45;
                tamMaxNomeEtapa = 45;
                tamMaxNomeSolicitante = 40;
                tamMaxNomeInstancia = 138;
            }
            else if (this.larguraFrame > 1000)
            {
                tamMaxTipoFluxo = 40;
                tamMaxNomeEtapa = 38;
                tamMaxNomeSolicitante = 35;
                tamMaxNomeInstancia = 125;
            }
            else if (this.larguraFrame > 800)
            {
                tamMaxTipoFluxo = 30;
                tamMaxNomeEtapa = 30;
                tamMaxNomeSolicitante = 28;
                tamMaxNomeInstancia = 98;
            }
            else
            {
                tamMaxTipoFluxo = 25;
                tamMaxNomeEtapa = 25;
                tamMaxNomeSolicitante = 20;
                tamMaxNomeInstancia = 85;
            }

            string nomeFluxo = _NomeFluxo;
            string nomeInstancia = _NomeInstaciaWorkflow;
            string nomeEtapa = _NomeEtapa;
            string nomeEtapaAnterior = _NomeEtapaAnterior;
            string VersaoFluxo = _VersaoFluxo;
            string DataInicioInstancia = _DataInicioInstancia;
            string nomeSolicitante = _NomeSolicitante;
            string hintFluxo = "";
            string hintInstancia = "";
            string hintEtapa = "";
            string hintEtapaAnterior = "";
            string hintSolicitante = "";
            string hintDataInicioInstancia = "";
            string hintVersao = "";

            if (nomeFluxo.Length > tamMaxTipoFluxo)
            {
                nomeFluxo = nomeFluxo.Substring(0, tamMaxTipoFluxo) + "...";
                hintFluxo = _NomeFluxo;
            }

            if (nomeEtapa.Length > tamMaxNomeEtapa)
            {
                nomeEtapa = nomeEtapa.Substring(0, tamMaxNomeEtapa) + "...";
                hintEtapa = _NomeEtapa;
            }

            if (nomeEtapaAnterior.Length > tamMaxNomeEtapa)
            {
                nomeEtapaAnterior = nomeEtapaAnterior.Substring(0, tamMaxNomeEtapa) + "...";
                hintEtapaAnterior = _NomeEtapaAnterior;
            }

            if (nomeSolicitante.Length > tamMaxNomeSolicitante)
            {
                nomeSolicitante = nomeSolicitante.Substring(0, tamMaxNomeSolicitante) + "...";
                hintSolicitante = _NomeSolicitante;
            }

            if (nomeInstancia.Length > tamMaxNomeInstancia)
            {
                nomeInstancia = nomeInstancia.Substring(0, tamMaxNomeInstancia) + "...";
                hintInstancia = _NomeInstaciaWorkflow;
            }
            // Adiciona o cabe�alho da tela de workflow

            //            string strCabecalho = string.Format(
            //                @"
            //<TABLE border=0 cellpadding=5 cellspacing=0 width=""100%"" >
            //<TR><TD align=left style=""BORDER-RIGHT: green 1px solid; BORDER-TOP: green 1px solid; 
            //BORDER-LEFT: green 1px solid; BORDER-BOTTOM: green 1px solid; WIDTH: 100%; FONT-SIZE: small; 
            //FONT-FAMILY: Verdana, Arial"" >
            //    <TABLE cellSpacing=5 cellPadding=0 border=0 width=""100%"" >
            //        <TR>
            //            <TD align=left style=""WIDTH: 40px"">Fluxo:</TD>
            //            <TD align=left style=""FONT-WEIGHT: bold; COLOR: blue"" title=""{3}"">{0}</TD>
            //            <TD align=left style=""WIDTH: 40px"">Nome:</TD>
            //            <TD align=left style=""WIDTH: 35%; COLOR: blue"" title=""{4}"">{1}</TD>
            //            <TD align=left style=""WIDTH: 40px"">Etapa:</TD>
            //            <TD align=left style=""FONT-WEIGHT: bold; WIDTH: 25%; COLOR: blue"" title=""{5}"">{2}</TD>
            //        </TR></TABLE></TD></TR>", nomeFluxo, nomeInstancia, nomeEtapa, hintFluxo, hintInstancia, hintEtapa);

            string strCabecalho = string.Format(
@"
<TABLE style=""width:100%"" border=0 cellpadding=3 cellspacing=0>
  <TR>
    <TD align=left style=""BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: #CCCCCC 1px solid; WIDTH: 100%; FONT-SIZE: 8pt; FONT-FAMILY: Verdana, Arial"" >
      <table  style=""width:100%"" cellSpacing=0 cellPadding=0 border=0>
        <tr height=17px>
          <TD align=left title=""{7}"" colspan=2>{14}: <span>{3}</span></TD>
          <TD align=left title=""{5}"">{15}: <span>{1}</span> </TD>
        </tr>
        <tr height=17px>
          <TD align=left title=""{6}"">{16}: <span>{2}</span></TD>
          <TD align=left title=""{11}"">{17}: <span>{10}</span></TD>
          <TD align=left title=""{9}"">{18}: <span>{8}</span> </TD>
          <TD align=right title=""{12}""><span style=""font-family:Verdana;font-size:7pt;"" >{13}</span> </TD>
        </tr>
      </table>
    </td>
  </tr>", nomeFluxo, nomeEtapa, nomeSolicitante, nomeInstancia, hintFluxo, hintEtapa, hintSolicitante, hintInstancia,
          nomeEtapaAnterior, hintEtapaAnterior, DataInicioInstancia, hintDataInicioInstancia, hintVersao, VersaoFluxo,
          Resources.Fluxo, Resources.EtapaAtual, Resources.Solicitante, Resources.Data, Resources.EtapaAnterior);

            //if (_ModoConsulta)
            //    strCabecalho += @"<TR><TABLE style=""width:100%"" cellSpacing=0 cellPadding=0 border=0>
            //    <TR><TD align=center>(visualiza��o de etapa percorrida)</TD></TR></TABLE></TR>";

            strCabecalho += @"</TABLE>";

            pnPrincipal.Controls.Add(getLiteral(strCabecalho));
            // callback para processar as a��es do workflow
            pnWorkflow.ID = "pnWorkflow";
            pnWorkflow.ClientInstanceName = "pnWorkflow";
            pnWorkflow.Callback += pnWorkflow_Callback;
            string acaoJSPosExecutarAcao = indicaPopup ? "window.top.fechaModal2();" : ("window.open('" + urlDestinoAposExecutarAcao + @"', '" + targetRedirecionamento + @"');");
            pnWorkflow.ClientSideEvents.EndCallback =
                @"function(s, e) {
                    
                    if (hfGeralWorkflow.Get('StatusSalvar')=='1')
                    { 
                        if (hfGeralWorkflow.Get('CodigoAcaoWf')=='1001')
                            window.top.mostraMensagem('" + Resources.ParecerTramitadoComSucesso + @"', 'sucesso', false, false, null);
                        else if (hfGeralWorkflow.Get('CodigoAcaoWf')=='1002')
                            window.top.mostraMensagem('" + Resources.PreenchimentoDoSFormul�rioSFinalizadoComSu + @"', 'sucesso', false, false, null);
                        else
                            window.top.mostraMensagem('" + Resources.EtapaFinalizadaComSucesso + @"', 'sucesso', false, false, null);
                        
                        if(window.executaAcaoPosSalvarFluxo)
                            executaAcaoPosSalvarFluxo();
                        
                        " + acaoJSPosExecutarAcao + @"
                    } 
                    else
                    {
                        window.top.lpAguardeMasterPage.Hide();
                        window.top.mostraMensagem(hfGeralWorkflow.Get('ErroSalvar'), 'erro', true, false, null);
                        pnBotoes.SetVisible(true);
                    }
                    
                  }";

            pnWorkflow.ClientSideEvents.BeginCallback =
                @"function(s, e) {
                    window.top.lpAguardeMasterPage.Show();
                    
                  }";

            hfGeralWorkflow.ID = "hfGeralWorkflow";
            hfGeralWorkflow.ClientInstanceName = "hfGeralWorkflow";
            pnWorkflow.Controls.Add(hfGeralWorkflow);

            // Insere os links para cada formul�rio associado.
            ASPxTabControl tcLinksFormuarios = new ASPxTabControl();
            tcLinksFormuarios.EnableTabScrolling = true;
            tcLinksFormuarios.ID = "tcLinksFormuarios";
            tcLinksFormuarios.ClientInstanceName = "tcLinksFormuarios";
            tcLinksFormuarios.CssFilePath = cssFilePath;
            tcLinksFormuarios.CssPostfix = cssPostFix;
            tcLinksFormuarios.ContentStyle.Paddings.Padding = 2;
            tcLinksFormuarios.Width = new Unit("100%");
            tcLinksFormuarios.ClientSideEvents.ActiveTabChanging =
                @"function(s, e) 
                {
                    e.processOnServer = false;

                    var existeAlteracao = false;
                    if (window.frames['wfFormularios'].window.frames['wfTela'])
                        existeAlteracao = window.frames['wfFormularios'].frames['wfTela'].existeConteudoCampoAlterado;
                    else
                        existeAlteracao = window.frames['wfFormularios'].existeConteudoCampoAlterado;

                    if (existeAlteracao && mudaAba == false)
                    {
                        var textoMsg = """ + Resources.ExistemAltera��esAindaN�oSalvasBrBrAoPress + @""";
                        
                        var funcObj = { cancelaEvento: function(s, e, tabIndex){  mudaAba = true; tcLinksFormuarios.SetActiveTabIndex(tabIndex);} }

                        window.top.mostraMensagem(textoMsg, 'confirmacao', true, true, function(){funcObj['cancelaEvento'](s, e, e.tab.index)});
                        
                        e.cancel = true; return;
                    } 
                    
                    mudaAba = false;
                    selecionaFormulario(s,e);

                }";

            string srcPrimeiroFormul�rio = "";
            insereAbasFormularios(_CodigoEtapa, tcLinksFormuarios, ref srcPrimeiroFormul�rio, _ModoConsulta);
            pnPrincipal.Controls.Add(tcLinksFormuarios);

            // Frame para a renderiza��o de formul�rios // #a9a9a9
            pnPrincipal.Controls.Add(getLiteral(string.Format(
                                @"<iframe name='wfFormularios' src='{0}' width='{1}' scrolling='auto' frameborder='0'
                                    style='height: {2}px;  width:100%;border: #a9a9a9 1px solid;background-color:white'></iframe>", srcPrimeiroFormul�rio, larguraFrame - 20, alturaFrame)));

            // Insere os bot�es de a��es para o fluxo
            insereBotoesDeAcoes(_CodigoEtapa, pnPrincipal);

            pnPrincipal.Controls.Add(pnWorkflow);
        }

        // M�todo respons�vel por escolher o tipo de persist�ncia a ser executada no banco de dados
        protected void pnWorkflow_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string CodigoAcaoWF = e.Parameter;
            string mensagemErro_Persistencia = "";

            if (mensagemErro_Persistencia == "")
            {
                mensagemErro_Persistencia = executaAcaoSalvarInformacoes(int.Parse(CodigoAcaoWF));
            }

            if (mensagemErro_Persistencia == "") // n�o deu erro durante o processo de persist�ncia
                hfGeralWorkflow.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            else
            {
                // alguma coisa deu errado...
                hfGeralWorkflow.Set("StatusSalvar", "0"); // 0 indica que N�O foi salvo.
                hfGeralWorkflow.Set("ErroSalvar", mensagemErro_Persistencia);
            }
            hfGeralWorkflow.Set("CodigoAcaoWf", CodigoAcaoWF); // c�digo de a��o ser� usado para determinar a mensagem a ser exibida ao fim do callback

        }

        private string executaAcaoSalvarInformacoes(int CodigoAcaoWF)
        {
            try
            {
                string mensagemErro = "";
                long CodigoInstanciaWF = long.Parse(hfGeralWorkflow.Get("CodigoInstanciaWf").ToString());
                int SequenciaOcorrenciaEtapaWf = int.Parse(hfGeralWorkflow.Get("SequenciaOcorrenciaEtapaWf").ToString());
                int CodigoEtapaWf = int.Parse(hfGeralWorkflow.Get("CodigoEtapaWf").ToString());

                // se for a a��o 1001, apenas grava o parecer digitado;
                if (1001 == CodigoAcaoWF)
                {
                    registraFinalizacaoParecerTramitacao(codigoUsuarioInteracao, _CodigoWorkflow, CodigoInstanciaWF, SequenciaOcorrenciaEtapaWf, CodigoEtapaWf, CodigoAcaoWF, out mensagemErro);
                }
                else if (1002 == CodigoAcaoWF) // se for a a��o 1002, apenas finaliza o preenchimento do formul�rio
                {
                    registraFinalizacaoPreenchimentoFormularioTramitacao(codigoUsuarioInteracao, _CodigoWorkflow, CodigoInstanciaWF, SequenciaOcorrenciaEtapaWf, CodigoEtapaWf, CodigoAcaoWF, out mensagemErro);
                }
                else
                {
                    processaAcaoWorkflow(codigoUsuarioInteracao, _CodigoWorkflow, CodigoInstanciaWF, SequenciaOcorrenciaEtapaWf, CodigoEtapaWf, CodigoAcaoWF, out mensagemErro);
                }
                return mensagemErro;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void insereAbasFormularios(int codigoEtapa, ASPxTabControl tcLinksFormuarios, ref string srcPrimeiroFormul�rio, bool somenteConsulta)
        {
            string comandoSQL = string.Format(
                @" exec dbo.p_wf_formulariosInstanciaEtapa {0}, {1}, {2}, {3}, '{4}';"
                , _CodigoWorkflow, _CodigoInstanciaWorkflow, _OcorrenciaEtapa, codigoEtapa, codigoUsuarioInteracao);
            srcPrimeiroFormul�rio = "";
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null)
                {
                    string sIDModeloForms = "";
                    int ordemFormulario = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sIDModeloForms += dr["CodigoModeloFormulario"].ToString() + ";";

                        string sReadOnly;
                        // se o formul�rio estiver como somente na etapa, n�o tem discuss�o: ser� somente leitura
                        if (dr["TipoAcessoFormulario"].ToString().Trim() == "R")
                        {
                            sReadOnly = "S";
                        }
                        else
                        {

                            // se o formul�rio n�o estiver somente leitura e 
                            // a pessoa estiver com poder de interagir na etapa, mostra formul�rio para escrita.
                            if (somenteConsulta == false)
                                sReadOnly = "N";
                            else
                            {

                                // formul�rio no modo escrita e pessoa sem acesso de a��o. 
                                // se for o formul�rio "TRAMETP", habilita se estivermos numa Tramita��o de Parecer

                                if (dr["IniciaisFormularioControladoSistema"].ToString().Trim() == "TRAMETP")
                                {
                                    if (_HabilitaTramitacaoParecer)
                                        sReadOnly = "N";
                                    else
                                        sReadOnly = "S";
                                }
                                else // se n�o for o formul�rio de parecer {formul�rio modo escrita, pessoa sem acesso de a��o...}, se estiver numa tramita��o de formul�rio deixa no modo escrita
                                {
                                    if (_HabilitaTramitacaoFormulario)
                                        sReadOnly = "N";
                                    else
                                        sReadOnly = "S";
                                }
                            }
                        }
                        string Obrigatorio = "S";
                        if ((sReadOnly == "S") || (dr["CodigoTipoFormulario"].ToString().Trim() == "3") || (dr["PreenchimentoObrigatorio"].ToString().Trim() == "N"))
                            Obrigatorio = "N";

                        //int _CodigoProjeto = int.Parse(dr["IdentificadorProjetoRelacionado"].ToString());

                        hfGeralWorkflow.Set("FormID" + dr["CodigoModeloFormulario"].ToString() + "_" + ordemFormulario.ToString(), dr["CodigoFormulario"].ToString() == "" ? "0" : dr["CodigoFormulario"].ToString());
                        hfGeralWorkflow.Set("FormNM" + dr["CodigoModeloFormulario"].ToString() + "_" + ordemFormulario.ToString(), dr["TituloFormulario"].ToString());
                        hfGeralWorkflow.Set("FormOB" + dr["CodigoModeloFormulario"].ToString() + "_" + ordemFormulario.ToString(), Obrigatorio);
                        hfGeralWorkflow.Set("FormRO" + dr["CodigoModeloFormulario"].ToString() + "_" + ordemFormulario.ToString(), sReadOnly);
                        ordemFormulario++;

                        if (_IniciaisFormObrigatorio == null)
                        {
                            DataRow[] drsFormObr = dt.Select("IniciaisFormularioControladoSistema='CRITERIOS'");

                            if (drsFormObr.Length > 0)
                                _IniciaisFormObrigatorio = "&INIFRMOBR=CRITERIOS";
                            else
                                _IniciaisFormObrigatorio = "";
                        }

                        if (srcPrimeiroFormul�rio == "")
                        {
                            srcPrimeiroFormul�rio = string.Format(
                                formularioEdicao + "CMF={0}{1}{2}{3}{4}{5}{6}", dr["CodigoModeloFormulario"].ToString(),
                                                                             dr["CodigoFormulario"].ToString() == "" ? "" : "&CF=" + dr["CodigoFormulario"].ToString(),
                                                                             sReadOnly == "N" ? "" : "&RO=S",
                                                                             "&CIWF=" + _CodigoInstanciaWorkflow,
                                                                             "&CEWF=" + _CodigoEtapa,
                                                                             "&COWF=" + _OcorrenciaEtapa,
                                                                             _IniciaisFormObrigatorio);

                            // se tem codigoProjeto vinculado ao wf, repassa esta informa��o para os formul�rios
                            if (_CodigoProjeto > 0)
                                srcPrimeiroFormul�rio += "&CPWF=" + _CodigoProjeto;

                        }
                        Tab tb = tcLinksFormuarios.Tabs.Add(
                            dr["TituloFormulario"].ToString(),
                            "CMF" + dr["CodigoModeloFormulario"].ToString());

                        //tb.TabStyle.Font.Name = "Verdana";
                        //tb.TabStyle.Font.Size = new FontUnit("8pt");
                    }
                    if (sIDModeloForms != "")
                        sIDModeloForms = sIDModeloForms.Substring(0, sIDModeloForms.Length - 1);
                    hfGeralWorkflow.Set("Formularios", sIDModeloForms);
                }
            }
        }

        private void insereBotoesDeAcoes(int codigoEtapa, Panel pnPrincipal)
        {
            if (mostrarBotoesAcao)
            {
                bool bInserirBotaoTramitarParecer = _HabilitaTramitacaoParecer;
                bool bInserirBotaoTramitarFormulario = _HabilitaTramitacaoFormulario;
                bool bInserirBotoesEncerramento = false;

                // busca as a��es a etapa especificada (apenas a��es de usu�rios 'U'
                string comandoSQL = string.Format(
                    @"SELECT * 
                    FROM AcoesEtapasWf 
                   WHERE CodigoWorkflow = {0} 
                     AND codigoEtapaWF = {1} AND [TipoAcao] = 'U'
                   ORDER BY OrdemAcaoEtapa", _CodigoWorkflow, codigoEtapa); // 29/07/2015 - Inclus�o do Order by para que os bot�es sejam apresentados conforme determina��o

                DataSet ds = dados.getDataSet(comandoSQL);
                DataTable dt = null;
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0 && _ModoConsulta == false)
                    {
                        bInserirBotoesEncerramento = true;
                    }
                }

                if (bInserirBotoesEncerramento || bInserirBotaoTramitarParecer || bInserirBotaoTramitarFormulario || indicaPopup)
                {
                    ASPxPanel botoes = new ASPxPanel();
                    botoes.ClientInstanceName = "pnBotoes";
                    botoes.ClientVisible = false;
                    botoes.ID = "pnBotoes";

                    botoes.Controls.Add(getLiteral(@"<table style='width:100%;' border=""0"" cellpadding=""1"" cellspacing=""1""><tr>"));

                    if (bInserirBotoesEncerramento)
                    {
                        insereBotoesEncerramento(codigoEtapa, dt, botoes);
                    }

                    if (bInserirBotaoTramitarParecer)
                    {
                        insereBotaoTramitarParecer(codigoEtapa, botoes);
                    }

                    if (bInserirBotaoTramitarFormulario)
                    {
                        insereBotaoTramitarFormulario(codigoEtapa, botoes);
                    }

                    if (indicaPopup)
                        insereBotaoFechar(botoes);
                    else
                        botoes.Controls.Add(getLiteral(@"<td>&nbsp;</td>"));

                    botoes.Controls.Add(getLiteral(@"</tr></table>"));
                    pnPrincipal.Controls.Add(botoes);
                }
            }
            else
            {
                ASPxPanel botoes = new ASPxPanel();
                botoes.ClientInstanceName = "pnBotoes";
                botoes.ClientVisible = false;
                botoes.ID = "pnBotoes";

                botoes.Controls.Add(getLiteral(@"<table style='width:100%;' border=""0"" cellpadding=""1"" cellspacing=""1""><tr>"));

                insereBotaoFechar(botoes);

                botoes.Controls.Add(getLiteral(@"</tr></table>"));
                pnPrincipal.Controls.Add(botoes);
            }
        }

        /// <summary>
        /// adiciona o HTML criando os bot�es de encerramento da etapa no fluxo. 
        /// </summary>
        /// <remarks>Esta fun��o ir� adicionar ao ASPxPanel <paramref name="botoes"/> o HTML criando os bot�es 
        /// de encerramento da etapa no fluxo. Assume que o par�metro <paramref name="dtBotoes"/> j� cont�m as linhas com 
        /// as defini��es dos bot�es a serem inseridos. 
        /// Assume tamb�m que os bot�es est�o sendo inseridos dentro de uma table no html, inserindo cada bot�o dentro de uma "td"
        /// </remarks>
        /// <param name="codigoEtapa">C�digo da etapa atual do workflow. Esta informa��es ser� passada como par�metro nas chamadas 
        /// de javascripts que tratar�o os clicks nos bot�es</param>
        /// <param name="dtBotoes">Datatable contendo as linhas com as defini��es dos bot�es a serem inseridos</param>
        /// <param name="botoes">ASPxPanel onde ser�o inseridos os bot�es</param>
        private void insereBotoesEncerramento(int codigoEtapa, DataTable dtBotoes, ASPxPanel botoes)
        {
            if (dtBotoes != null)
            {
                foreach (DataRow dr in dtBotoes.Rows)
                {
                    // Registra se a a��o exige certifica��o digital
                    string indicaCertificacaoDigital = dr["SolicitaAssinaturaDigital"].ToString().ToLower() == "true" ? "S" : "N";

                    botoes.Controls.Add(getLiteral(@"<td style='width:135px;text-align:left;'>"));

                    ASPxButton btnAcao = new ASPxButton();
                    btnAcao.ID = "_ID" + dr["CodigoAcaoWF"].ToString();
                    btnAcao.ClientInstanceName = "_ID" + dr["CodigoAcaoWF"].ToString();
                    btnAcao.Text = dr["TextoAcao"].ToString();// +(indicaCertificacaoDigital == "S" ? " *" : "");

                    if (dr["CorBotao"].ToString() != "")
                    {
                        btnAcao.CssFilePath = "";
                        btnAcao.CssPostfix = "";
                        btnAcao.SpriteCssFilePath = "SF";
                        btnAcao.Style.Add(HtmlTextWriterStyle.BackgroundColor, dr["CorBotao"].ToString());
                    }

                    if (indicaCertificacaoDigital == "S")
                        btnAcao.ImageUrl = "~/imagens/certificado.png";
                    else
                    {
                        if (dr["IconeBotao"].ToString() != "")
                        {
                            btnAcao.ImageUrl = "~/imagens/iconesBotoesFluxo/" + dr["IconeBotao"].ToString();
                        }
                        else
                            btnAcao.ImageUrl = "~/imagens/btnVazio.png";
                    }
                    btnAcao.Image.Height = 16;
                    btnAcao.CssFilePath = cssFilePath;
                    btnAcao.CssPostfix = cssPostFix;
                    btnAcao.Width = new Unit("130px");
                    btnAcao.Height = new Unit("40px");
                    //btnAcao.Font.Name = "Verdana";
                    //btnAcao.Font.Size = new FontUnit("8pt");
                    btnAcao.ClientSideEvents.Click =
                        @"function(s, e) 
                              {
                                    e.processOnServer = false;
                              
                                    podeAvancarFluxo(" + _CodigoWorkflow.ToString() + ", " + codigoEtapa.ToString() + ", " + dr["CodigoAcaoWF"].ToString() + ", '" + indicaCertificacaoDigital + "', '" + btnAcao.Text + @"')
                                    
                              }";
                    botoes.Controls.Add(btnAcao);
                    botoes.Controls.Add(getLiteral(@"</td>"));
                }
            }
        }

        /// <summary>
        /// adiciona o HTML criando o bot�o "Tramitar Parecer" 
        /// </summary>
        /// <remarks>Esta fun��o ir� adicionar ao ASPxPanel <paramref name="botoes"/> o HTML criando o bot�o "Tramitar Parecer"
        /// Assume que o bot�o est� sendo inserido dentro de uma table no html, inserindo-o dentro de uma "td"
        /// </remarks>
        /// <param name="codigoEtapa">C�digo da etapa atual do workflow. Esta informa��es ser� passada como par�metro nas chamadas 
        /// de javascripts que tratar�o os click no bot�o</param>
        /// <param name="botoes">ASPxPanel onde ser�o inserido o bot�o</param>
        private void insereBotaoTramitarParecer(int codigoEtapa, ASPxPanel botoes)
        {
            botoes.Controls.Add(getLiteral(@"<td style='width:135px;text-align:left;'>"));
            ASPxButton btnAcao = new ASPxButton();
            btnAcao.ID = "_ID1001";
            btnAcao.ClientInstanceName = "_ID1001";
            btnAcao.Text = Properties.Resources.TramitarParecer;
            btnAcao.CssFilePath = cssFilePath;
            btnAcao.CssPostfix = cssPostFix;
            btnAcao.ImageUrl = "~/imagens/btnVazio.png";
            btnAcao.Width = new Unit("130px");
            btnAcao.Height = new Unit("40px");
            //btnAcao.Font.Name = "Verdana";
            //btnAcao.Font.Size = new FontUnit("8pt");
            btnAcao.Image.Height = 16;
            btnAcao.ClientSideEvents.Click =
                @"function(s, e) 
                    {
                        e.processOnServer = false; 
                        callbackParecer.PerformCallback('1001;" + _CodigoWorkflow + ";" + codigoEtapa + ";N" + ";" + btnAcao.Text + ";" + _CodigoInstanciaWorkflow + ";" + _OcorrenciaEtapa + @"');
                    }";
            botoes.Controls.Add(btnAcao);
            botoes.Controls.Add(getLiteral(@"</td>"));
        }

        /// <summary>
        /// adiciona o HTML criando o bot�o "Finalizar Preenchimento do(s) Formul�rio(s)" 
        /// </summary>
        /// <remarks>Esta fun��o ir� adicionar ao ASPxPanel <paramref name="botoes"/> o HTML criando o bot�o "Finalizar Preenchimento do(s) Formul�rio(s)"
        /// Assume que o bot�o est� sendo inserido dentro de uma table no html, inserindo-o dentro de uma "td"
        /// </remarks>
        /// <param name="codigoEtapa">C�digo da etapa atual do workflow. Esta informa��es ser� passada como par�metro nas chamadas 
        /// de javascripts que tratar�o os click no bot�o</param>
        /// <param name="botoes">ASPxPanel onde ser�o inserido o bot�o</param>
        private void insereBotaoTramitarFormulario(int codigoEtapa, ASPxPanel botoes)
        {
            botoes.Controls.Add(getLiteral(@"<td style='width:135px;text-align:left;'>"));
            ASPxButton btnAcao = new ASPxButton();
            btnAcao.ID = "_ID1002";
            btnAcao.ClientInstanceName = "_ID1002";
            btnAcao.Text = Resources.Finalizar;
            btnAcao.CssFilePath = cssFilePath;
            btnAcao.CssPostfix = cssPostFix;
            btnAcao.ImageUrl = "~/imagens/btnVazio.png";
            btnAcao.Width = new Unit("200px");
            btnAcao.Height = new Unit("40px");
            btnAcao.Image.Height = 16;
            //btnAcao.Font.Name = "Verdana";
            //btnAcao.Font.Size = new FontUnit("8pt");
            btnAcao.ClientSideEvents.Click =
                @"function(s, e) 
                    {
                        e.processOnServer = false;
                              
                        podeAvancarFluxo(" + _CodigoWorkflow.ToString() + ", " + codigoEtapa.ToString() + ", 1002, 'N', '" + btnAcao.Text + @"')
                            
                    }";
            botoes.Controls.Add(btnAcao);
            botoes.Controls.Add(getLiteral(@"</td>"));
        }

        private void insereBotaoFechar(ASPxPanel botoes)
        {

            botoes.Controls.Add(getLiteral(@"<td align='right'>"));

            ASPxButton btnAcao = new ASPxButton();
            btnAcao.ID = "_IDFECHAR";
            btnAcao.ClientInstanceName = "_IDFECHAR";
            btnAcao.Text = Resources.Fechar;
            btnAcao.CssFilePath = cssFilePath;
            btnAcao.CssPostfix = cssPostFix;
            btnAcao.ImageUrl = "~/imagens/btnVazio.png";
            btnAcao.Width = new Unit("130px");
            btnAcao.Height = new Unit("40px");
            btnAcao.Image.Height = 16;
            //btnAcao.Font.Name = "Verdana";
            //btnAcao.Font.Size = new FontUnit("8pt");
            btnAcao.ClientSideEvents.Click =
                @"function(s, e) 
                              {
                                    e.processOnServer = false;
                              
                                    window.top.fechaModal2();
                              }";
            botoes.Controls.Add(btnAcao);
            botoes.Controls.Add(getLiteral(@"</td>"));
        }

        private Literal getLiteral(string texto)
        {
            Literal myLiteral = new Literal();
            myLiteral.Text = texto;
            return myLiteral;
        }

        #region wf - Engenharia

        /// <summary>
        /// verifica se o usu�rio pode interagir na etapa atual do fluxo
        /// </summary>
        /// <param name="workflow">C�digo do workflow da etapa que se quer obter o n�vel de acesso</param>
        /// <param name="instanciaWf">C�digo da inst�ncia do workflow para a qual se quer obter o n�vel de acesso</param>
        /// <param name="seqEtapa">N�mero sequencial de ocorr�ncia de etapa. 
        /// Este n�mero � usado para identificar em qual ocorr�ncia da etapa se quer verificar o n�vel de acesso</param>
        /// <param name="etapa">C�digo da Etapa</param>
        /// <param name="usuario">Identificador do usu�rio para o qual se deseja obter o n�vel de acesso.</param>
        /// <returns>
        /// </returns>
        private void verificaModoInteracaoTela(int? workflow, long? instanciaWf, int? seqEtapa, int? etapa, string usuario)
        {
            int nivelAcessoEtapa, nivelAcessoTramitacaoParecer, nivelAcessoTramitacaoFormulario;
            bool bAcessoPorTramitacaoParecer, bAcessoPorTramitacaoFormulario;

            nivelAcessoEtapa = verificaAcessoEtapa(workflow, instanciaWf, seqEtapa, etapa, usuario, out bAcessoPorTramitacaoParecer, out bAcessoPorTramitacaoFormulario);

            // teste conforme documenta��o da proc
            // se o acesso 2 (intera��o estiver presente)
            if ((nivelAcessoEtapa & 2) > 0)
            {
                // se o acesso de a��o s� foi concedido em fun��o de que o usu�rio tem que dar um parecer na etapa
                if (bAcessoPorTramitacaoParecer == true)
                {
                    _ModoConsulta = true;       // assume somente leitura
                    _HabilitaTramitacaoParecer = true;   // habilita o bot�o "Enviar Parecer";
                }

                // se o acesso de a��o s� foi concedido em fun��o de que o usu�rio tem que preencher um formul�rio a convite
                if (bAcessoPorTramitacaoFormulario == true)
                {
                    _ModoConsulta = true;       // assume somente leitura
                    _HabilitaTramitacaoFormulario = true;   // habilita o bot�o "Enviar dados do(s) formul�rio(s)";
                }

                // se o acesso n�o foi em fun��o de etapa tramita��o
                if (!(bAcessoPorTramitacaoParecer || bAcessoPorTramitacaoFormulario))
                {
                    _ModoConsulta = false;       // desativa modo somente leitura

                    // mesmo que o usu�rio j� tenha acesso direto na etapa, verifica mesmo assim se o usu�rio precisa dar um parecer, ou preencher um formulario.
                    nivelAcessoTramitacaoParecer = verificaAcessoEtapaTramitacaoParecer(workflow, instanciaWf, seqEtapa, etapa, usuario);
                    if ((nivelAcessoTramitacaoParecer & 2) > 0)
                    {
                        _HabilitaTramitacaoParecer = true;   // habilita o bot�o "Tramitar" parecer;
                    }
                    else
                    {
                        _HabilitaTramitacaoParecer = false;
                    }
                    nivelAcessoTramitacaoFormulario = verificaAcessoEtapaTramitacaoFormulario(workflow, instanciaWf, seqEtapa, etapa, usuario);
                    if ((nivelAcessoTramitacaoFormulario & 2) > 0)
                    {
                        _HabilitaTramitacaoFormulario = true;   // habilita o bot�o "Tramitar" parecer;
                    }
                    else
                    {
                        _HabilitaTramitacaoFormulario = false;
                    }
                }
            }
            else
            {
                _ModoConsulta = true;
                _HabilitaTramitacaoParecer = false;
                _HabilitaTramitacaoFormulario = false;
            }
        }

        /// <summary>
        /// verifica o acesso do usu�rio na etapa em quest�o
        /// </summary>
        /// <param name="workflow">C�digo do workflow da etapa que se quer obter o n�vel de acesso</param>
        /// <param name="instanciaWf">C�digo da inst�ncia do workflow para a qual se quer obter o n�vel de acesso</param>
        /// <param name="seqEtapa">N�mero sequencial de ocorr�ncia de etapa. 
        /// Este n�mero � usado para identificar em qual ocorr�ncia da etapa se quer verificar o n�vel de acesso</param>
        /// <param name="etapa">C�digo da Etapa</param>
        /// <param name="usuario">Identificador do usu�rio para o qual se deseja obter o n�vel de acesso.</param>
        /// <param name="acessoEmFuncaoDeTramitacaoParecer">Par�metro de sa�da. Indicar� se o acesso s� foi concedido em fun��o
        /// da etapa de tramita��o de parecer ou de formul�rio</param>
        /// <returns>Valores retornados:
        /// 0  usu�rio n�o tem acesso � etapa
        /// 1  acesso de leitura
        /// 2 acesso de escrita
        /// </returns>
        private int verificaAcessoEtapa(int? workflow, long? instanciaWf, int? seqEtapa, int? etapa, string usuario, out bool acessoEmFuncaoDeTramitacaoParecer, out bool acessoEmFuncaoDeTramitacaoFormulario)
        {
            int nivelAcesso = 0, codigoRetorno = 0;
            acessoEmFuncaoDeTramitacaoParecer = false;
            acessoEmFuncaoDeTramitacaoFormulario = false;
            string comandoSQL = "";
            try
            {
                comandoSQL = string.Format(
                @"
                DECLARE @RC int
                DECLARE @in_codigoWorkFlow int
                DECLARE @in_codigoInstanciaWf bigint
                DECLARE @in_SequenciaOcorrenciaEtapaWf int
                DECLARE @in_codigoEtapaWf int
                DECLARE @in_identificadorUsuario varchar(50)
                DECLARE @ou_nivelAcesso int
                DECLARE @ou_codigoRetorno int
                DECLARE @ou_mensagemErro nvarchar(2048)

                SET @in_codigoWorkFlow								= {2} 
                SET @in_codigoInstanciaWf							= {3}
                SET @in_SequenciaOcorrenciaEtapaWf		            = {4}
                SET @in_codigoEtapaWf								= {5}
                SET @in_identificadorUsuario					    = '{6}'

                EXECUTE @RC = {0}.{1}.[p_wf_verificaAcessoEtapaWf] 
                   @in_codigoWorkFlow
                  ,@in_codigoInstanciaWf
                  ,@in_SequenciaOcorrenciaEtapaWf
                  ,@in_codigoEtapaWf
                  ,@in_identificadorUsuario
                  ,@ou_nivelAcesso OUTPUT
                  ,@ou_codigoRetorno OUTPUT
                  ,@ou_mensagemErro OUTPUT

                SELECT 
                    @RC                     AS RetornoProc, 
                    @ou_nivelAcesso         AS NivelAcesso, 
                    @ou_codigoRetorno       AS CodigoRetorno, 
                    @ou_mensagemErro        AS MensagemErro "
                               , ""
                               , ""
                               , workflow, instanciaWf, seqEtapa, etapa, usuario);

                DataSet ds = dados.getDataSet(comandoSQL);

                ds = dados.getDataSet(comandoSQL);
                if ((ds != null) && (ds.Tables.Count > 0))
                {
                    nivelAcesso = (int)ds.Tables[0].Rows[0]["NivelAcesso"];
                    codigoRetorno = (int)ds.Tables[0].Rows[0]["CodigoRetorno"];
                }
            }
            catch
            {
                nivelAcesso = 0;
                codigoRetorno = 0;
            }

            // conforme documenta��o da proc, se o usu�rio est� entre os tramitadores (7� bit da vari�vel de retorno), e ainda n�o deu o seu parecer (8� bit) ...
            if (((codigoRetorno & 64) > 0) && ((codigoRetorno & 128) == 0))
            {
                acessoEmFuncaoDeTramitacaoParecer = true;
            }

            // conforme documenta��o da proc, se o usu�rio est� entre os tramitadores (15� bit da vari�vel de retorno), e ainda n�o deu o seu parecer (16� bit) ...
            // conforme documenta��o da proc, se estiver marcado o 15� bit na vari�vel de retorno, indica que o acesso foi em fun��o da etapa de tramita��o de parecer
            if (((codigoRetorno & 16384) > 0) && ((codigoRetorno & 32768) == 0))
            {
                acessoEmFuncaoDeTramitacaoFormulario = true;
            }
            return nivelAcesso;
        }

        /// <summary>
        /// verifica o acesso do usu�rio a uma etapa de tramita��o de parecer
        /// </summary>
        /// <param name="workflow">C�digo do workflow da etapa que se quer obter o n�vel de acesso</param>
        /// <param name="instanciaWf">C�digo da inst�ncia do workflow para a qual se quer obter o n�vel de acesso</param>
        /// <param name="seqEtapa">N�mero sequencial de ocorr�ncia de etapa. 
        /// Este n�mero � usado para identificar em qual ocorr�ncia da etapa se quer verificar o n�vel de acesso</param>
        /// <param name="etapa">C�digo da Etapa</param>
        /// <param name="usuario">Identificador do usu�rio para o qual se deseja obter o n�vel de acesso.</param>
        /// <returns>Valores retornados:
        /// 0  usu�rio n�o tem acesso � etapa, ou a etapa n�o � uma etapa de tramita��o de parecer
        /// 1  acesso de leitura
        /// 2 acesso de escrita
        /// </returns>
        private int verificaAcessoEtapaTramitacaoParecer(int? workflow, long? instanciaWf, int? seqEtapa, int? etapa, string usuario)
        {
            int nivelAcesso = 0, codigoRetorno = 0;
            string comandoSQL = "";
            try
            {
                comandoSQL = string.Format(
                @"
                DECLARE @RC int
                DECLARE @in_codigoWorkFlow int
                DECLARE @in_codigoInstanciaWf bigint
                DECLARE @in_SequenciaOcorrenciaEtapaWf int
                DECLARE @in_codigoEtapaWf int
                DECLARE @in_identificadorUsuario varchar(50)
                DECLARE @ou_nivelAcesso int
                DECLARE @ou_codigoRetorno int

                SET @in_codigoWorkFlow								= {2} 
                SET @in_codigoInstanciaWf							= {3}
                SET @in_SequenciaOcorrenciaEtapaWf		            = {4}
                SET @in_codigoEtapaWf								= {5}
                SET @in_identificadorUsuario					    = '{6}'

                EXECUTE @RC = {0}.{1}.[p_wf_verificaAcessoEtapaTramitacao] 
                   @in_codigoWorkFlow
                  ,@in_codigoInstanciaWf
                  ,@in_SequenciaOcorrenciaEtapaWf
                  ,@in_codigoEtapaWf
                  ,@in_identificadorUsuario
                  ,@ou_nivelAcesso OUTPUT
                  ,@ou_codigoRetorno OUTPUT

                SELECT 
                    @RC                     AS RetornoProc, 
                    @ou_nivelAcesso         AS NivelAcesso, 
                    @ou_codigoRetorno       AS CodigoRetorno"
                               , ""
                               , ""
                               , workflow, instanciaWf, seqEtapa, etapa, usuario);

                DataSet ds = dados.getDataSet(comandoSQL);

                ds = dados.getDataSet(comandoSQL);
                if ((ds != null) && (ds.Tables.Count > 0))
                {
                    nivelAcesso = (int)ds.Tables[0].Rows[0]["NivelAcesso"];
                    codigoRetorno = (int)ds.Tables[0].Rows[0]["CodigoRetorno"];
                }
            }
            catch
            {
                nivelAcesso = 0;
                codigoRetorno = 0;
            }
            return nivelAcesso;
        }

        /// <summary>
        /// verifica o acesso do usu�rio a uma etapa de tramita��o de formul�rio
        /// </summary>
        /// <param name="workflow">C�digo do workflow da etapa que se quer obter o n�vel de acesso</param>
        /// <param name="instanciaWf">C�digo da inst�ncia do workflow para a qual se quer obter o n�vel de acesso</param>
        /// <param name="seqEtapa">N�mero sequencial de ocorr�ncia de etapa. 
        /// Este n�mero � usado para identificar em qual ocorr�ncia da etapa se quer verificar o n�vel de acesso</param>
        /// <param name="etapa">C�digo da Etapa</param>
        /// <param name="usuario">Identificador do usu�rio para o qual se deseja obter o n�vel de acesso.</param>
        /// <returns>Valores retornados:
        /// 0  usu�rio n�o tem acesso � etapa, ou a etapa n�o � uma etapa de tramita��o de formul�rio
        /// 1  acesso de leitura
        /// 2 acesso de escrita
        /// </returns>
        private int verificaAcessoEtapaTramitacaoFormulario(int? workflow, long? instanciaWf, int? seqEtapa, int? etapa, string usuario)
        {
            int nivelAcesso = 0, codigoRetorno = 0;
            string comandoSQL = "";
            try
            {
                comandoSQL = string.Format(
                @"
                DECLARE @RC int
                DECLARE @in_codigoWorkFlow int
                DECLARE @in_codigoInstanciaWf bigint
                DECLARE @in_SequenciaOcorrenciaEtapaWf int
                DECLARE @in_codigoEtapaWf int
                DECLARE @in_identificadorUsuario varchar(50)
                DECLARE @ou_nivelAcesso int
                DECLARE @ou_codigoRetorno int

                SET @in_codigoWorkFlow								= {2} 
                SET @in_codigoInstanciaWf							= {3}
                SET @in_SequenciaOcorrenciaEtapaWf		            = {4}
                SET @in_codigoEtapaWf								= {5}
                SET @in_identificadorUsuario					    = '{6}'

                EXECUTE @RC = {0}.{1}.[p_wf_verificaAcessoEtapaTramitacaoFormulario] 
                   @in_codigoWorkFlow
                  ,@in_codigoInstanciaWf
                  ,@in_SequenciaOcorrenciaEtapaWf
                  ,@in_codigoEtapaWf
                  ,@in_identificadorUsuario
                  ,@ou_nivelAcesso OUTPUT
                  ,@ou_codigoRetorno OUTPUT

                SELECT 
                    @RC                     AS RetornoProc, 
                    @ou_nivelAcesso         AS NivelAcesso, 
                    @ou_codigoRetorno       AS CodigoRetorno"
                               , ""
                               , ""
                               , workflow, instanciaWf, seqEtapa, etapa, usuario);

                DataSet ds = dados.getDataSet(comandoSQL);

                ds = dados.getDataSet(comandoSQL);
                if ((ds != null) && (ds.Tables.Count > 0))
                {
                    nivelAcesso = (int)ds.Tables[0].Rows[0]["NivelAcesso"];
                    codigoRetorno = (int)ds.Tables[0].Rows[0]["CodigoRetorno"];
                }
            }
            catch
            {
                nivelAcesso = 0;
                codigoRetorno = 0;
            }
            return nivelAcesso;
        }

        /// <summary>
        /// Processa as a��es derivadas de uma 'a��o' no workflow 
        /// </summary>
        /// <remarks>
        /// Quando se executa uma a��o no workflow, devem ocorrer v�rios eventos, tais como 
        /// envio de notifica��es, execu��es de a��es autom�ticas, agendamento de jobs, etc.
        /// Esta fun��o faz com que todos estes eventos ocorram.
        /// Os par�metros desta fun��o s�o os valores identificantes na tabela [EtapasInstanciasWf]
        /// acrescido do c�digo da a��o que est� sendo executada. O c�digo de a��o passado no 
        /// par�metro <paramref name="acao"/> deve constar na tabela [AcoesEtapasWf] para o workflow
        /// e etapa passados como par�metro.
        /// </remarks>
        /// <param name="key">chave de compara��o para execu��o de comandos no servidor</param>
        /// <param name="usuario">c�digo do usu�rio que est� executando a a��o</param>
        /// <param name="workflow">C�digo do workflow em cuja inst�ncia est� ocorrendo a a��o</param>
        /// <param name="instanciaWf">C�digo da inst�ncia do workflow em que est� ocorrendo a a��o</param>
        /// <param name="seqEtapa">N�mero sequencial de ocorr�ncia de etapa. 
        /// Este n�mero � usado para identificar em qual ocorr�ncia da etapa foi executada a a��o</param>
        /// <param name="etapa">C�digo da Etapa em que se executou a a��o</param>
        /// <param name="acao">C�digo da A��o executada</param>
        /// <param name="mensagemErro">Par�metro de sa�da. Mensagem do erro caso o valor retornado seja
        /// false.</param>
        /// <returns>caso tudo ocorra sem problema no processamento da a��o, retorna true.
        /// Caso contr�rio, retorna false.</returns>
        /// <exception cref="Exception">Gerada caso ocorra alguma exce��o de processamento
        /// no servidor de banco de dados.</exception>
        private bool processaAcaoWorkflow(int usuario, int workflow, long instanciaWf, int seqEtapa, int etapa, int acao, out string mensagemErro)
        {
            bool bRet;
            string comandoSQL = "";
            try
            {
                comandoSQL = string.Format(
                    @"
                DECLARE @RC                             int 
                DECLARE @in_codigoWorkFlow              int 
                DECLARE @in_codigoInstanciaWf           bigint 
                DECLARE @in_SequenciaOcorrenciaEtapaWf  int 
                DECLARE @in_codigoEtapaWf               int 
                DECLARE @in_codigoAcaoWf                int 
                DECLARE @in_identificadorUsuarioAcao    varchar(50)
                DECLARE @in_opcoes                      int 
                DECLARE @ou_resultado                   int 
                DECLARE @ou_codigoRetorno               int 
                DECLARE @ou_mensagemErro                nvarchar(2048) 

                SET @in_codigoWorkFlow              = {2} 
                SET @in_codigoInstanciaWf           = {3} 
                SET @in_SequenciaOcorrenciaEtapaWf  = {4} 
                SET @in_codigoEtapaWf               = {5} 
                SET @in_codigoAcaoWf                = {6} 
                SET @in_identificadorUsuarioAcao    = '{7}'
                SET @in_opcoes						= {8} 

                EXECUTE @RC = {0}.{1}.[p_processaAcaoWorkflow] 
                   @in_codigoWorkFlow               = @in_codigoWorkFlow 
                  ,@in_codigoInstanciaWf            = @in_codigoInstanciaWf 
                  ,@in_SequenciaOcorrenciaEtapaWf   = @in_SequenciaOcorrenciaEtapaWf 
                  ,@in_codigoEtapaWf                = @in_codigoEtapaWf 
                  ,@in_codigoAcaoWf                 = @in_codigoAcaoWf 
                  ,@in_identificadorUsuarioAcao     = @in_identificadorUsuarioAcao
                  ,@in_opcoes                       = @in_opcoes 
                  ,@ou_resultado                    = @ou_resultado                 OUTPUT 
                  ,@ou_codigoRetorno                = @ou_codigoRetorno             OUTPUT 
                  ,@ou_mensagemErro                 = @ou_mensagemErro              OUTPUT 

                SELECT 
                        @RC									AS RetornoProc,  
                        @ou_resultado				AS Resultado, 
                        @ou_codigoRetorno		AS CodigoRetorno, 
                        @ou_mensagemErro		AS MensagemErro "
                               , ""
                               , ""
                               , workflow, instanciaWf, seqEtapa, etapa, acao, usuario, 0);

                DataSet ds = dados.getDataSet(comandoSQL);

                string msgWhere = "", msgWhat = "";
                int retornoProc, resultado, codigoRetorno;
                string msgProc;

                retornoProc = int.Parse(ds.Tables[0].Rows[0]["RetornoProc"].ToString());
                resultado = int.Parse(ds.Tables[0].Rows[0]["Resultado"].ToString());
                codigoRetorno = int.Parse(ds.Tables[0].Rows[0]["CodigoRetorno"].ToString());
                msgProc = ds.Tables[0].Rows[0]["MensagemErro"].ToString();

                if (0 == retornoProc)
                {
                    mensagemErro = "";
                    bRet = true;
                }
                else
                {
                    if ((16 & resultado) > 0)
                        msgWhere = Resources.noProcessamentoDeTempoLimiteDaEtapa;
                    else if ((32 & resultado) > 0)
                        msgWhere = Resources.naAltera��oDaEtapaDoProcesso;
                    else if ((64 & resultado) > 0)
                        msgWhere = Resources.naDefini��oDaPr�ximaEtapaDoProcesso;
                    else if (((4 & resultado) > 0) || ((8 & resultado) > 0))
                        msgWhere = Resources.naExecu��oDeA��esAutom�ticasDaEtapaDoProce;
                    else if (((1 & resultado) > 0) || ((2 & resultado) > 0))
                        msgWhere = Resources.noEnvioDeNotifica��es;
                    else if ((1024 & resultado) > 0)
                        msgWhere = Resources.noAcionamentoDeApisExternasDaEtapa;

                    switch (codigoRetorno)
                    {
                        case 1:
                        case 2:
                            msgWhat = Resources.AsInforma��esNaBaseDeDadosEncontramSeIncon;
                            break;

                        case 3:
                        case 4:
                            msgWhat = Resources.AConfigura��oDosPar�metrosParaExecu��oDePr;
                            break;
                        case 5:
                            msgWhat = Resources.AEtapaDoProcessoFoiAlteradaPorOutroUsu�rio;
                            break;
                        case 6:
                            msgWhat = Resources.AA��oExecutadaN�oProduziuEfeitoEN�oFoiPoss;
                            break;
                        case 7:
                            msgWhat = Resources.AA��oN�oPodeSerExecutadaPoisOUsu�rioN�oPos;
                            break;
                        case 8:
                            msgWhat = Resources.ErroDuranteOProcessamentoDeCausaIndeterminada;
                            break;
                        default:
                            msgWhat = string.Format(Resources.FalhaNaExecu��oDoProcedimentoNoServidorEnt, codigoRetorno, retornoProc);
                            break;

                    }

                    if (msgProc.Length > 0)
                    {
                        msgWhat += Resources.MensagemOriginalDoErro + msgProc;
                    }

                    mensagemErro = string.Format(@"{2} {0}.{1}.", msgWhere, msgWhat, Resources.Falha);
                    bRet = false;
                }
            }
            catch (Exception ex)
            {
                mensagemErro = string.Format(@"Falha ao executar procedimento no servidor. Mensagem original do erro: {0}", ex.Message);
                bRet = false;
            }

            return bRet;
        }

        /// <summary>
        /// Registra a finaliza��o do parecer de um usu�rio numa etapa de tramita��o de pareceres
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="key">chave de compara��o para execu��o de comandos no servidor</param>
        /// <param name="usuario">c�digo do usu�rio que est� executando a a��o</param>
        /// <param name="workflow">C�digo do workflow em cuja inst�ncia est� ocorrendo a a��o</param>
        /// <param name="instanciaWf">C�digo da inst�ncia do workflow em que est� ocorrendo a a��o</param>
        /// <param name="seqEtapa">N�mero sequencial de ocorr�ncia de etapa. 
        /// Este n�mero � usado para identificar em qual ocorr�ncia da etapa foi executada a a��o</param>
        /// <param name="etapa">C�digo da Etapa em que se executou a a��o</param>
        /// <param name="acao">C�digo da A��o executada</param>
        /// <param name="mensagemErro">Par�metro de sa�da. Mensagem do erro caso o valor retornado seja
        /// false.</param>
        /// <returns>caso tudo ocorra sem problema no registro da finaliza��o do do parecer, retorna true.
        /// Caso contr�rio, retorna false.</returns>
        /// <exception cref="Exception">Gerada caso ocorra alguma exce��o de processamento
        /// no servidor de banco de dados.</exception>
        private bool registraFinalizacaoParecerTramitacao(int usuario, int workflow, long instanciaWf, int seqEtapa, int etapa, int acao, out string mensagemErro)
        {
            bool bRet;
            string comandoSQL = "";
            try
            {
                comandoSQL = string.Format(
                    @"
                DECLARE @RC                             int 
                DECLARE @in_codigoWorkFlow              int 
                DECLARE @in_codigoInstanciaWf           bigint 
                DECLARE @in_SequenciaOcorrenciaEtapaWf  int 
                DECLARE @in_codigoEtapaWf               int 
                DECLARE @in_codigoUsuario               int
                DECLARE @ou_mensagemErro                nvarchar(2048) 

                SET @in_codigoWorkFlow              = {2} 
                SET @in_codigoInstanciaWf           = {3} 
                SET @in_SequenciaOcorrenciaEtapaWf  = {4} 
                SET @in_codigoEtapaWf               = {5} 
                SET @in_codigoUsuario               = {6}

                EXECUTE @RC = p_wf_registraEnvioParecerEtapaTramitacao  
                   @in_codigoWorkFlow               = @in_codigoWorkFlow 
                  ,@in_codigoInstanciaWf            = @in_codigoInstanciaWf 
                  ,@in_SequenciaOcorrenciaEtapaWf   = @in_SequenciaOcorrenciaEtapaWf 
                  ,@in_codigoEtapaWf                = @in_codigoEtapaWf 
                  ,@in_codigoUsuario                = @in_codigoUsuario
                  ,@ou_mensagemErro                 = @ou_mensagemErro              OUTPUT 

                SELECT 
                        @RC						AS RetornoProc,  
                        @ou_mensagemErro		AS MensagemErro "
                               , ""
                               , ""
                               , workflow, instanciaWf, seqEtapa, etapa, usuario);

                DataSet ds = dados.getDataSet(comandoSQL);

                int retornoProc;
                string msgProc;

                retornoProc = int.Parse(ds.Tables[0].Rows[0]["RetornoProc"].ToString());
                msgProc = ds.Tables[0].Rows[0]["MensagemErro"].ToString();

                if (0 == retornoProc)
                {
                    mensagemErro = "";
                    bRet = true;
                }
                else
                {
                    mensagemErro = msgProc;
                    bRet = false;
                }
            }
            catch (Exception ex)
            {
                mensagemErro = string.Format(@"Falha ao executar procedimento no servidor.
        Entre em contato com o administrador do sistema. Mensagem original do erro: {0}", ex.Message);
                bRet = false;
            }

            return bRet;
        }

        /// <summary>
        /// Registra a finaliza��o do preenchimento do(s) formul�rio(s) de tramita��o na etapa
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="key">chave de compara��o para execu��o de comandos no servidor</param>
        /// <param name="usuario">c�digo do usu�rio que est� executando a a��o</param>
        /// <param name="workflow">C�digo do workflow em cuja inst�ncia est� ocorrendo a a��o</param>
        /// <param name="instanciaWf">C�digo da inst�ncia do workflow em que est� ocorrendo a a��o</param>
        /// <param name="seqEtapa">N�mero sequencial de ocorr�ncia de etapa. 
        /// Este n�mero � usado para identificar em qual ocorr�ncia da etapa foi executada a a��o</param>
        /// <param name="etapa">C�digo da Etapa em que se executou a a��o</param>
        /// <param name="acao">C�digo da A��o executada</param>
        /// <param name="mensagemErro">Par�metro de sa�da. Mensagem do erro caso o valor retornado seja
        /// false.</param>
        /// <returns>caso tudo ocorra sem problema no registro da finaliza��o do preenchimento do formul�rio, retorna true.
        /// Caso contr�rio, retorna false.</returns>
        /// <exception cref="Exception">Gerada caso ocorra alguma exce��o de processamento
        /// no servidor de banco de dados.</exception>
        private bool registraFinalizacaoPreenchimentoFormularioTramitacao(int usuario, int workflow, long instanciaWf, int seqEtapa, int etapa, int acao, out string mensagemErro)
        {
            bool bRet;
            string comandoSQL = "";
            try
            {
                comandoSQL = string.Format(
                    @"
                DECLARE @RC                             int 
                DECLARE @in_codigoWorkFlow              int 
                DECLARE @in_codigoInstanciaWf           bigint 
                DECLARE @in_SequenciaOcorrenciaEtapaWf  int 
                DECLARE @in_codigoEtapaWf               int 
                DECLARE @in_codigoUsuario               int
                DECLARE @ou_mensagemErro                nvarchar(2048) 

                SET @in_codigoWorkFlow              = {2} 
                SET @in_codigoInstanciaWf           = {3} 
                SET @in_SequenciaOcorrenciaEtapaWf  = {4} 
                SET @in_codigoEtapaWf               = {5} 
                SET @in_codigoUsuario               = {6}

                EXECUTE @RC = p_wf_registraPreenchimentoFormularioTramitacao  
                   @in_codigoWorkFlow               = @in_codigoWorkFlow 
                  ,@in_codigoInstanciaWf            = @in_codigoInstanciaWf 
                  ,@in_SequenciaOcorrenciaEtapaWf   = @in_SequenciaOcorrenciaEtapaWf 
                  ,@in_codigoEtapaWf                = @in_codigoEtapaWf 
                  ,@in_codigoUsuario                = @in_codigoUsuario
                  ,@ou_mensagemErro                 = @ou_mensagemErro              OUTPUT 

                SELECT 
                        @RC						AS RetornoProc,  
                        @ou_mensagemErro		AS MensagemErro "
                               , ""
                               , ""
                               , workflow, instanciaWf, seqEtapa, etapa, usuario);

                DataSet ds = dados.getDataSet(comandoSQL);

                int retornoProc;
                string msgProc;

                retornoProc = int.Parse(ds.Tables[0].Rows[0]["RetornoProc"].ToString());
                msgProc = ds.Tables[0].Rows[0]["MensagemErro"].ToString();

                if (0 == retornoProc)
                {
                    mensagemErro = "";
                    bRet = true;
                }
                else
                {
                    mensagemErro = msgProc;
                    bRet = false;
                }
            }
            catch (Exception ex)
            {
                mensagemErro = string.Format(@"Falha ao executar procedimento no servidor.
        Entre em contato com o administrador do sistema. Mensagem original do erro: {0}", ex.Message);
                bRet = false;
            }

            return bRet;
        }

        #endregion
    }
}
