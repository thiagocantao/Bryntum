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
using DevExpress.Web;
using CDIS;
using System.Collections.Specialized;
using System.Drawing;
using DevExpress.Web.Rendering;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

public partial class wfRenderizaFormularioMobile2 : System.Web.UI.Page
{
    dados cDados;
    string resolucaoCliente;

    //Variável exclusiva para identificar se o fluxo é o de "inclusão de uma nova proposta"
    int codigoModeloFormulario;
    long? codigoObjetoAssociado;
    string iniciaisTipoObjeto;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    string tituloFormulario = "";
    string tituloInstanciaWf = "";

    private int larguraTela;
    public string alturaFormulario;
    string versaoFormulario = "Atual";
    bool possuiVersoes = false;
    bool indicaAssinado = false;

    bool readOnly = false;
    Hashtable parametros = new Hashtable();

    protected void Page_Init(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["CMF"] != null && Request.QueryString["CMF"].ToString() != "")
        {
            codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());
        }
        else
        {
            codigoModeloFormulario = getCodigoModeloFormulario(Request.QueryString["INIMF"].ToString());
        }

        callbackWF.JSProperties["cp_FechaModal"] = Request.QueryString["FechaModalPosSalvar"] + "";

        // se foi passado o código projeto, seta na variável de sessão
        if (Request.QueryString["CPWF"] != null && Request.QueryString["CPWF"].ToString() != "")
        {
            iniciaisTipoObjeto = "PR";
            codigoObjetoAssociado = long.Parse(Request.QueryString["CPWF"].ToString());
            // a linha abaixo é necessária para os formulários do tipo "Tela"
            if (codigoObjetoAssociado.Value != -1)
                cDados.setInfoSistema("CodigoProjeto", codigoObjetoAssociado);
            else
                cDados.setInfoSistema("CodigoProjeto", null);
        }
        else 
        {
            if (Request.QueryString["COA"] != null && Request.QueryString["COA"].ToString() != "" && long.Parse(Request.QueryString["COA"]) > 0
                && Request.QueryString["INTO"] != null && Request.QueryString["INTO"].ToString() != "")
            {
                codigoObjetoAssociado = long.Parse(Request.QueryString["COA"].ToString());
                iniciaisTipoObjeto = Request.QueryString["INTO"].ToString();
            }
            else if (Request.QueryString["CIWF"] != null && Request.QueryString["CIWF"].ToString() != "" && int.Parse(Request.QueryString["CIWF"]) != -1)
            {
                var codigoProjetoInfoSistema = cDados.getInfoSistema("CodigoProjeto");
                if (codigoProjetoInfoSistema != null && codigoProjetoInfoSistema is int && ((int)codigoProjetoInfoSistema != -1))
                {
                    codigoObjetoAssociado = (long)codigoProjetoInfoSistema;
                    iniciaisTipoObjeto = "PR";
                }
            }
        }

        // //caso não tenha sido passado o código do projeto pela URL, ou é um código inválido tenta obtê-lo da variável do sistema
        //if ((!codigoProjeto.HasValue) || (codigoProjeto.Value == -1))
        //{
        //    if (cDados.getInfoSistema("CodigoProjeto") != null)
        //        codigoProjeto = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        //}


        if (Request.QueryString["INIPERM"] != null)
        {
            readOnly = readOnly == true ? true : !cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, (int)codigoObjetoAssociado.Value, "null", "PR", 0, "null", Request.QueryString["INIPERM"].ToString());
        }

        renderizaFormulario();

        callbackWF.JSProperties["cp_QS"] = Request.QueryString.ToString();

        // se a chamada veio de dentro de um Workflow
        if (Request.QueryString["CWF"] == null)
        {
            string estiloFooter = "dxgvControl dxgvGroupPanel";

            string cssPostfix = "", cssPath = "";

            cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

            if (cssPostfix != "")
                estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        }
    }

    private string getAlturaTela()
    {
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaTela = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 155).ToString();
    }


    private int getLarguraTela()
    {
        return int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
    }

    private int getCodigoModeloFormulario(string iniciaisModeloFormulario)
    {
        int codigoModeloFormulario = 0;
        // busca o modelo do formulário de propostas
        string comandoSQL = string.Format("Select codigoModeloFormulario from modeloFormulario where IniciaisFormularioControladoSistema = '{0}'", iniciaisModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            DataTable dt = ds.Tables[0];
            if (cDados.DataTableOk(dt))
            {
                codigoModeloFormulario = int.Parse(dt.Rows[0]["codigoModeloFormulario"].ToString());
            }
        }
        return codigoModeloFormulario;
    }

    private void renderizaFormulario()
    {
        int codigoWorkflow = -1;
        int codigoInstanciaWf = -1;

        if (codigoModeloFormulario > 0)
        {
            // se tem filtro por formularios
            string filtroCodigosFormularios = "";
            if (Request.QueryString["FF"] != null)
            {
                filtroCodigosFormularios = Request.QueryString["FF"].ToString().Trim();
            }

            int? codigoFormulario = null;

            if ((Request.QueryString["CF"] != null) && (Request.QueryString["CF"].ToString() != "") && (Request.QueryString["CF"].ToString() != @"undefined"))
            {
                codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());

                if (codigoFormulario.HasValue)
                    hfSessao.Set("_CodigoFormularioMaster_", codigoFormulario);
            }

            bool readOnly = false;

            if (Request.QueryString["RO"] != null)
                readOnly = Request.QueryString["RO"].ToString().ToUpper().Contains("S");

            if (Request.QueryString["CFV"] != null && Request.QueryString["CFV"].ToString() != "")
            {
                readOnly = true;
                codigoFormulario = int.Parse(Request.QueryString["CFV"].ToString());
            }

            if (indicaAssinado)
                readOnly = true;

            if (codigoFormulario.HasValue)
                hfSessao.Set("_CodigoFormularioMaster_", codigoFormulario);


            /*----
            alturaFormulario = getAlturaTela();//"800";

            larguraTela = getLarguraTela();
            ------*/

            alturaFormulario = "600";
            larguraTela = 800;

            if (Request.QueryString["CWF"] != null)
            {
                codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());
                codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());

                parametros.Add("CodigoWorkflow", codigoWorkflow.ToString());
                parametros.Add("CodigoInstanciaWorkflow", codigoInstanciaWf.ToString());
                parametros.Add("CodigoEtapaAtual", Request.QueryString["CEWF"].ToString());
                parametros.Add("CodigoOcorrenciaAtual", Request.QueryString["COWF"].ToString());
            }
            if (Request.QueryString["COA"] != null && Request.QueryString["COA"].ToString() != "" && long.Parse(Request.QueryString["COA"]) > 0
                && Request.QueryString["INTO"] != null && Request.QueryString["INTO"].ToString() != "")
            {
                parametros.Add("CodigoObjetoAssociado", Request.QueryString["COA"].ToString());
                parametros.Add("IniciaisTipoObjeto", Request.QueryString["INTO"].ToString());
            }
            parametros.Add("nomeSessao", "Form");

            /*-----*/
            string alturaMyForm = (int.Parse(alturaFormulario) - 60) + "px";

            alturaFormulario = alturaFormulario + "px";
            /*-----*/


            FormularioMobile myForm = new FormularioMobile(cDados.classeDados, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoModeloFormulario, new Unit((larguraTela - 15).ToString() + "px"), new Unit(alturaMyForm), readOnly, this, parametros, ref hfSessao, indicaAssinado);

            if (myForm.TipoFormularioPreDefinido == FormulariosPreDefinidosMobile.NovaProposta)
            {
                if ((!codigoObjetoAssociado.HasValue) || (codigoObjetoAssociado.Value == -1))
                {
                    hfSessao.Set("_TipoOperacao_", "I");
                }
                myForm.AntesSalvar += new FormularioMobile.AntesSalvarEventHandler(myForm_AntesSalvarFormularioProposta);
            }

            if (hfSessao.Contains("_CodigoFormularioMaster_"))
                codigoFormulario = int.Parse(hfSessao.Get("_CodigoFormularioMaster_").ToString());

            // os eventos abaixo só podem ser executados quando a chamada desta tela está relacionada a Worflow
            if (codigoWorkflow > 0 || codigoInstanciaWf > 0)
                myForm.AposSalvar += new FormularioMobile.AposSalvarEventHandler(myForm_AposSalvarFormularioWorkflow);

            string cssFile = "";
            string cssPostFix = "";

            cDados.getVisual("MaterialCompact", ref cssFile, ref cssPostFix);

            Control ctrl = myForm.constroiInterfaceFormulario(IsPostBack, codigoFormulario, codigoObjetoAssociado, iniciaisTipoObjeto, cssFile, cssPostFix);

            form1.Controls.Add(ctrl);

        }
        callbackWF.JSProperties["cp_CIWF"] = codigoInstanciaWf;
        callbackWF.JSProperties["cp_CWF"] = codigoWorkflow;
        callbackWF.JSProperties["cp_CI"] = codigoInstanciaWf;
    }

    void myForm_AntesSalvarFormularioProposta(object sender, EventFormsWFMobile e, ref string mensagemErroEvento)
    {
        // apenas por garantia, já que o evento só é adicionado caso seja o formulário de nova proposta;
        if (e.TipoFormularioPreDefinido != FormulariosPreDefinidosMobile.NovaProposta)
            return;

        string detalheProjeto = null; ; // mantendo null para tratamento diferenciado
        string categoriaProjeto = "";
        string unidadeProjeto = "";
        string descricaoProposta = "";

        // se vai salvar o formulário de proposta de iniciativa, cria ou atualiza o projeto associado.
        if (e.TipoFormularioPreDefinido == FormulariosPreDefinidosMobile.NovaProposta)
        {
            for (int i = 0; i < e.camposControladoSistema.Count; i++)
            {
                object[] Controles = e.camposControladoSistema[i];
                if (Controles[0].ToString() == "DESC")
                {
                    Control temp = (Controles[2] as ASPxTextBox);
                    if (temp != null)
                        tituloFormulario = (Controles[2] as ASPxTextBox).Text.Replace("'", "´");
                }
                if (Controles[0].ToString() == "DETA")
                {
                    detalheProjeto = (Controles[2] as ASPxMemo).Text.Replace("'", "´");
                }
                if (Controles[0].ToString() == "CATE")
                    categoriaProjeto = (Controles[2] as ASPxComboBox).Value == null ? "" : (Controles[2] as ASPxComboBox).Value.ToString();
                if (Controles[0].ToString() == "UNID")
                    unidadeProjeto = (Controles[2] as ASPxComboBox).Value.ToString();
            }


            if (detalheProjeto == null)
                descricaoProposta = "NULL";
            else if (detalheProjeto == "")
                descricaoProposta = "NULL";
            else
                descricaoProposta = string.Format("'{0}'", detalheProjeto);

            if (categoriaProjeto == "")
                categoriaProjeto = "NULL";

            if (unidadeProjeto == "")
                unidadeProjeto = "NULL";

            if (tituloFormulario != "")
            {
                string comandoSQL;
                int afetados = 0;
                // se ainda não tem o projeto, inclui.
                if ((!codigoObjetoAssociado.HasValue) || (codigoObjetoAssociado.Value == -1))
                {
                    // chama a procedure para incluir o projeto;
                    comandoSQL = string.Format(
                        @"BEGIN
                            DECLARE @CodigoProjeto int

                            EXEC [dbo].[p_InsereProposta] '{0}', {1}, {2}, {3}, {4}, {5}, @CodigoProjeto out

                            SELECT @CodigoProjeto

                          END", tituloFormulario, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, categoriaProjeto, unidadeProjeto, descricaoProposta);
                    DataSet ds = cDados.getDataSet(comandoSQL);
                    e.codigoObjetoAssociado = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                } // if (e.codigoProjeto <= 0)
                else
                {
                    comandoSQL = string.Format(
                        @"  UPDATE projeto
                               SET NomeProjeto = '{0}' ", tituloFormulario);

                    if (detalheProjeto != null)
                        comandoSQL = comandoSQL + string.Format(@" 
                            , DescricaoProposta = {0} ", descricaoProposta);

                    if (categoriaProjeto != "NULL")
                        comandoSQL = comandoSQL + string.Format(@" 
                            , CodigoCategoria = {0} ", categoriaProjeto);

                    if (unidadeProjeto != "NULL")
                        comandoSQL = comandoSQL + string.Format(@" 
                            , CodigoUnidadeNegocio = {0} ", unidadeProjeto);

                    comandoSQL = comandoSQL + string.Format(@"
                            WHERE codigoProjeto = {0}", e.codigoObjetoAssociado);
                    cDados.execSQL(comandoSQL, ref afetados);
                }
                // chama a procedure para atualizar a lista de objetivos relacionados à proposta (projeto)
                comandoSQL = string.Format(
                    @" EXEC [dbo].[p_AtualizaInfoNovaProposta] {0}, {1}", e.codigoObjetoAssociado, e.codigoFormulario);
                cDados.execSQL(comandoSQL, ref afetados);

            }
        }

        return;
    }

    void myForm_AposSalvarFormularioWorkflow(object sender, EventFormsWFMobile e, ref string mensagemErroEvento)
    {
        gravaInstanciaWf(e);

        int auxCodigoInstanciaWf = int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString());

        // se já auxCodigoInstanciaWf> 0 -> significa que quando o formulário foi chamado, já havia uma instância;
        if (auxCodigoInstanciaWf > 0)
        {
            string comandoSQL = string.Format(
                @"BEGIN
                if (not exists(SELECT 1 FROM FormulariosInstanciasWorkflows
                                WHERE [CodigoWorkflow] = {0}
                                  AND [CodigoInstanciaWf] = {1}
                                  AND [SequenciaOcorrenciaEtapa] = {2}
                                  AND [CodigoEtapaWf] = {3}
                                  AND [CodigoFormulario] = {4} ) )

                        INSERT INTO [FormulariosInstanciasWorkflows]
                            (
                                [CodigoWorkflow]
                                , [CodigoInstanciaWf]
                                , [SequenciaOcorrenciaEtapa]
                                , [CodigoEtapaWf]
                                , [CodigoFormulario]
                            )
                            VALUES
                          (
                                {0}
                                , {1}
                                , {2}
                                , {3}
                                , {4}
                          )
                END
            ", e.parametrosEntrada["CodigoWorkflow"].ToString(),
                   e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString(),
                   e.parametrosEntrada["CodigoOcorrenciaAtual"].ToString(),
                   e.parametrosEntrada["CodigoEtapaAtual"].ToString(),
                   e.codigoFormulario);
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
        }
    }

    private bool gravaInstanciaWf(EventFormsWFMobile e)
    {
        int auxCodigoInstanciaWf = e.parametrosEntrada["CodigoInstanciaWorkflow"] != null && e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString() != "" ? int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString()) : -1;

        // se estiver incluindo um formulário, verifica se precisa criar a instância
        if (auxCodigoInstanciaWf <= 0)
            verificaNecessidadeCriacaoInstanciaWf(e);
        else
            verificaNecessidadeAtualizacaoInstanciaWf(e);

        return true;
    }

    private void verificaNecessidadeCriacaoInstanciaWf(EventFormsWFMobile e)
    {
        int auxCodigoInstanciaWf = int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString());

        // se já auxCodigoInstanciaWf<= 0 -> significa que quando o formulário foi chamado, NÃO havia instância;
        if (auxCodigoInstanciaWf <= 0)
        {
            int auxCodigoWorkflow = int.Parse(e.parametrosEntrada["CodigoWorkflow"].ToString());

            tituloInstanciaWf = montaTituloInstancia(e.codigoObjetoAssociado, auxCodigoWorkflow);

            string comandoSQL = string.Format(
                            @"BEGIN 
                    BEGIN TRAN
                    BEGIN TRY
                    INSERT INTO [InstanciasWorkflows]
                               ([CodigoWorkflow]
                               ,[NomeInstancia]
                               ,[DataInicioInstancia]
                               ,[DataTerminoInstancia]
                               ,[OcorrenciaAtual]
                               ,[EtapaAtual]
                               ,[IdentificadorUsuarioCriadorInstancia]
                               ,[IdentificadorProjetoRelacionado])
                         VALUES
                               ({0}
                               ,LEFT('{1}',250)
                               , GETDATE()
                               , NULL
                               , NULL
                               , NULL
                               , '{2}'
                               , {3} )

                    DECLARE @CodigoInstanciaWf int 
                    DECLARE @CodigoEtapaInicial int
                    SELECT @CodigoInstanciaWf = scope_identity(), 
                           @CodigoEtapaInicial = [CodigoEtapaInicial]  
	                  FROM Workflows 
                     WHERE CodigoWorkflow = {0}

                    INSERT INTO [EtapasInstanciasWf]
                               ([CodigoWorkflow]
                               ,[CodigoInstanciaWf]
                               ,[SequenciaOcorrenciaEtapaWf]
                               ,[CodigoEtapaWf]
                               ,[DataInicioEtapa]
                               ,[IdentificadorUsuarioIniciador]
                               ,[DataTerminoEtapa]
                               ,IdentificadorUsuarioFinalizador)
                         VALUES
                               ({0}
                               ,@CodigoInstanciaWf
                               ,1
                               ,@CodigoEtapaInicial
                               , GETDATE()
                               , '{2}'
                               , NULL
                               , NULL)

                        UPDATE InstanciasWorkflows
                           SET [OcorrenciaAtual] = 1
                             , [EtapaAtual] = @CodigoEtapaInicial
                        WHERE [CodigoWorkflow] = {0}
                          AND [CodigoInstanciaWf] = @CodigoInstanciaWf

                        INSERT INTO [FormulariosInstanciasWorkflows]
	                        (
		                        [CodigoWorkflow]
		                        , [CodigoInstanciaWf]
		                        , [SequenciaOcorrenciaEtapa]
		                        , [CodigoEtapaWf]
		                        , [CodigoFormulario]
	                        )
                            VALUES
                          (
		                        {0}
		                        , @CodigoInstanciaWf
		                        , 1
		                        , @CodigoEtapaInicial
		                        , {4}
                          )

                        SELECT @CodigoInstanciaWf as CodigoInstanciaWf, @CodigoEtapaInicial as CodigoEtapaInicial
                        COMMIT
                        END TRY
                        BEGIN CATCH
		                    DECLARE 
			                      @ErrorMessage		Nvarchar(4000)
			                    , @ErrorSeverity	Int
			                    , @ErrorState		Int
			                    , @ErrorNumber		Int;

		                    SET @ErrorMessage		= ERROR_MESSAGE();
		                    SET @ErrorSeverity	    = ERROR_SEVERITY();
		                    SET @ErrorState			= ERROR_STATE();
		                    SET @ErrorNumber		= ERROR_NUMBER();
                            ROLLBACK TRANSACTION
			                RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
                        END CATCH
                END
            ", auxCodigoWorkflow, tituloInstanciaWf.Replace("'", "''"), codigoUsuarioResponsavel, e.codigoObjetoAssociado, e.codigoFormulario);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataTable dt = ds.Tables[0];
                auxCodigoInstanciaWf = int.Parse(dt.Rows[0]["CodigoInstanciaWf"].ToString());

                if (e.parametros.Contains("CodigoInstanciaWf"))
                    e.parametros.Remove("CodigoInstanciaWf");

                if (e.parametros.Contains("CodigoEtapaWf"))
                    e.parametros.Remove("CodigoEtapaWf");

                if (e.parametros.Contains("SequenciaOcorrenciaEtapaWf"))
                    e.parametros.Remove("SequenciaOcorrenciaEtapaWf");

                e.parametros.Add("CodigoInstanciaWf", auxCodigoInstanciaWf);
                e.parametros.Add("CodigoEtapaWf", dt.Rows[0]["CodigoEtapaInicial"].ToString());
                e.parametros.Add("SequenciaOcorrenciaEtapaWf", "1");

                // todo: verificar os nomes dos parametros incluídos e seus usos;

            } // if (cDados.DataSetOk(ds) && ...
        } // if (codigoInstanciaWf <= 0)
    }

    private void verificaNecessidadeAtualizacaoInstanciaWf(EventFormsWFMobile e)
    {
        // por enquanto, só temos atualização do nome da instância quando se atualiza o nome do projeto no formulário NovaProposta
        if (e.TipoFormularioPreDefinido == FormulariosPreDefinidosMobile.NovaProposta)
        {
            int auxCodigoInstanciaWf = int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString());
            int auxCodigoWorkflow = int.Parse(e.parametrosEntrada["CodigoWorkflow"].ToString());
            tituloInstanciaWf = montaTituloInstancia(codigoObjetoAssociado, auxCodigoWorkflow);

            string comandoSQL = string.Format(
                @"  UPDATE InstanciasWorkflows
                        SET NomeInstancia = '{2}'
                        WHERE [CodigoWorkflow] = {0}
                        AND [CodigoInstanciaWf] = {1}
                            ", auxCodigoWorkflow, auxCodigoInstanciaWf, tituloInstanciaWf.Replace("'", "''"));
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
        }
        return;
    }

    private string montaTituloInstancia(long? codigoObjetoAssociado, int codigoWorkflow)
    {
        DataSet ds;
        DataRow dr;
        string titulo = "";
        string comandoSQL = string.Format(@"
            SELECT f.[NomeFluxo]
	            FROM
		            [dbo].[Workflows]				AS [wf]
				        INNER JOIN [dbo].[Fluxos]	AS [f]	ON 
					        ( f.[CodigoFluxo] = wf.[CodigoFluxo] )
	            WHERE
				        wf.[CodigoWorkflow]	= {0} ", codigoWorkflow);
        try
        {
            ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                dr = ds.Tables[0].Rows[0];
                titulo = dr["NomeFluxo"].ToString();
            }
        }
        catch
        {
            titulo = "";
        }
        if (codigoObjetoAssociado != null)
        {
            comandoSQL = string.Format(@"
            SELECT 
		            p.[NomeProjeto]
	            FROM
		            [dbo].[Projeto]					AS [p]
	            WHERE
				        p.CodigoProjeto		= {0}", codigoObjetoAssociado);
            try
            {
                ds = cDados.getDataSet(comandoSQL);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    dr = ds.Tables[0].Rows[0];
                    titulo = titulo + " - " + dr["NomeProjeto"].ToString();
                }
            }
            catch
            {
            }
        }
        return titulo;
    }


    private void criaInstanciaWF_ASPX()
    {
        if (!codigoObjetoAssociado.HasValue && cDados.getInfoSistema("CodigoProjeto") != null)
            codigoObjetoAssociado = long.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());

        string comandoSQL = string.Format(
                        @"BEGIN
                            DECLARE @CodigoFormularioMaster int

                            INSERT INTO Formulario (CodigoModeloFormulario, DescricaoFormulario, DataInclusao, IncluidoPor, DataExclusao, DataPublicacao)
                            VALUES ({0}, '{1}', getdate(), {2}, getdate(), getdate())

                            SELECT @CodigoFormularioMaster = scope_identity()

			                INSERT INTO FormularioProjeto (CodigoFormulario, CodigoProject)
                            VALUES (@CodigoFormularioMaster, {3})
                            
                            SELECT @CodigoFormularioMaster AS CodigoFormulario

                          END", codigoModeloFormulario, "Formulario ASPX", codigoUsuarioResponsavel, codigoObjetoAssociado);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            int codigoFormulario = -1;

            codigoFormulario = int.Parse(ds.Tables[0].Rows[0]["CodigoFormulario"].ToString());

            int auxCodigoInstanciaWf = -1;

            // se já auxCodigoInstanciaWf<= 0 -> significa que quando o formulário foi chamado, NÃO havia instância;
            if (auxCodigoInstanciaWf <= 0)
            {
                int auxCodigoWorkflow = int.Parse(parametros["CodigoWorkflow"].ToString());

                tituloInstanciaWf = montaTituloInstancia(codigoObjetoAssociado, auxCodigoWorkflow);
                comandoSQL = string.Format(
                                @"BEGIN
                    INSERT INTO [InstanciasWorkflows]
                               ([CodigoWorkflow]
                               ,[NomeInstancia]
                               ,[DataInicioInstancia]
                               ,[DataTerminoInstancia]
                               ,[OcorrenciaAtual]
                               ,[EtapaAtual]
                               ,[IdentificadorUsuarioCriadorInstancia]
                               ,[IdentificadorProjetoRelacionado])
                         VALUES
                               ({0}
                               ,LEFT('{1}',250)
                               , GETDATE()
                               , NULL
                               , NULL
                               , NULL
                               , '{2}'
                               , {3} )

                    DECLARE @CodigoInstanciaWf int 
                    DECLARE @CodigoEtapaInicial int
                    SELECT @CodigoInstanciaWf = scope_identity(), 
                           @CodigoEtapaInicial = [CodigoEtapaInicial]  
	                  FROM Workflows 
                     WHERE CodigoWorkflow = {0}

                    INSERT INTO [EtapasInstanciasWf]
                               ([CodigoWorkflow]
                               ,[CodigoInstanciaWf]
                               ,[SequenciaOcorrenciaEtapaWf]
                               ,[CodigoEtapaWf]
                               ,[DataInicioEtapa]
                               ,[IdentificadorUsuarioIniciador]
                               ,[DataTerminoEtapa]
                               ,IdentificadorUsuarioFinalizador)
                         VALUES
                               ({0}
                               ,@CodigoInstanciaWf
                               ,1
                               ,@CodigoEtapaInicial
                               , GETDATE()
                               , '{2}'
                               , NULL
                               , NULL)

                        UPDATE InstanciasWorkflows
                           SET [OcorrenciaAtual] = 1
                             , [EtapaAtual] = @CodigoEtapaInicial
                        WHERE [CodigoWorkflow] = {0}
                          AND [CodigoInstanciaWf] = @CodigoInstanciaWf

                        INSERT INTO [FormulariosInstanciasWorkflows]
	                        (
		                        [CodigoWorkflow]
		                        , [CodigoInstanciaWf]
		                        , [SequenciaOcorrenciaEtapa]
		                        , [CodigoEtapaWf]
		                        , [CodigoFormulario]
	                        )
                            VALUES
                          (
		                        {0}
		                        , @CodigoInstanciaWf
		                        , 1
		                        , @CodigoEtapaInicial
		                        , {4}
                          )

                        SELECT @CodigoInstanciaWf as CodigoInstanciaWf, @CodigoEtapaInicial as CodigoEtapaInicial

                END
            ", auxCodigoWorkflow, tituloInstanciaWf.Replace("'", "''"), codigoUsuarioResponsavel, codigoObjetoAssociado, codigoFormulario);

                ds = cDados.getDataSet(comandoSQL);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    DataTable dt = ds.Tables[0];
                    auxCodigoInstanciaWf = int.Parse(dt.Rows[0]["CodigoInstanciaWf"].ToString());
                    string codigoEtapaWf = dt.Rows[0]["CodigoEtapaInicial"].ToString();

                    callbackWF.JSProperties["cp_CodigoInstanciaWf"] = auxCodigoInstanciaWf;

                    // se já auxCodigoInstanciaWf> 0 -> significa que quando o formulário foi chamado, já havia uma instância;
                    if (auxCodigoInstanciaWf > 0)
                    {
                        comandoSQL = string.Format(
                            @"BEGIN
                if (not exists(SELECT 1 FROM FormulariosInstanciasWorkflows
                                WHERE [CodigoWorkflow] = {0}
                                  AND [CodigoInstanciaWf] = {1}
                                  AND [SequenciaOcorrenciaEtapa] = {2}
                                  AND [CodigoEtapaWf] = {3}
                                  AND [CodigoFormulario] = {4} ) )

                        INSERT INTO [FormulariosInstanciasWorkflows]
                            (
                                [CodigoWorkflow]
                                , [CodigoInstanciaWf]
                                , [SequenciaOcorrenciaEtapa]
                                , [CodigoEtapaWf]
                                , [CodigoFormulario]
                            )
                            VALUES
                          (
                                {0}
                                , {1}
                                , {2}
                                , {3}
                                , {4}
                          )
                END
            ", parametros["CodigoWorkflow"].ToString(),
                               auxCodigoInstanciaWf,
                               1,
                               codigoEtapaWf,
                               codigoFormulario);
                        int afetados = 0;
                        cDados.execSQL(comandoSQL, ref afetados);

                    } // if (cDados.DataSetOk(ds) &&
                }
            }
        }
    }

    protected void callbackWF_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        criaInstanciaWF_ASPX();
    }

    protected void callbackReload_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackReload.JSProperties["cp_URL"] = "";
        if (e.Parameter != "" && Request.QueryString["Engine"] != null && Request.QueryString["Engine"].ToString() == "S")
        {
            string queryString = "";

            if (Request.QueryString["CFX"] != null)
                queryString += "&CF=" + Request.QueryString["CFX"];

            if (Request.QueryString["AEI"] != null)
                queryString += "&AEI=" + Request.QueryString["AEI"];

            callbackReload.JSProperties["cp_URL"] = cDados.getPathSistema() + "wfEngineInterno.aspx?CW=" + Request.QueryString["CWF"] + e.Parameter + queryString;
        }

    }

    private void RegistraLog(long codigoOperacao, string descricao, string contexto = null)
    {
        cDados.RegistraPassoOperacaoCritica(codigoOperacao, descricao, contexto);
    }


}