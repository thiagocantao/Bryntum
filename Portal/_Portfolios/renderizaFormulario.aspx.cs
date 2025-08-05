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

public partial class _Portfolios_renderizaFormulario : System.Web.UI.Page
{
    dados cDados;

    int codigoModeloFormulario;
    int? codigoProjeto;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel; 
    int codigoWorkFlow = -1;
    string tituloFormulario = "";

        
    bool readOnly;
    string CssFilePath = "";
    string CssPostfix = "";
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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
        // =========================== Verifica se a sessão existe FIM ========================


        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref CssFilePath, ref CssPostfix);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());
        if (cDados.getInfoSistema("CodigoProjeto") != null)
            codigoProjeto = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        if (Request.QueryString["RO"] != null)
            readOnly = Request.QueryString["RO"].ToString() == "S";
        
        if (Request.QueryString["CWF"] != null)
            codigoWorkFlow = int.Parse(Request.QueryString["CWF"].ToString());

        renderizaFormulario();
    }

    private void renderizaFormulario()
    {
        if (codigoModeloFormulario > 0)
        {
            // se tem filtro por formularios
            string filtroCodigosFormularios = "";
            if (Request.QueryString["FF"] != null)
            {
                filtroCodigosFormularios = Request.QueryString["FF"].ToString().Trim();
            }

            ASPxHiddenField hf = new ASPxHiddenField();
            Hashtable parametros = new Hashtable();
            //Formularios.Formulario myForm = new Formularios.Formulario(cDados.classeDados, IsPostBack, 1, codigoModeloFormulario, null, codigoProjeto, readOnly, codigoUsuarioResponsavel, false, this.Page, filtroCodigosFormularios, true, false, true, parametros);
            Formulario myForm = new Formulario(cDados.classeDados, 1, 1, codigoModeloFormulario, new Unit("1000px"), new Unit("500px"), false, this, parametros, ref hf,false);

            // se for o formulario de propostas
            if (codigoModeloFormulario == getCodigoModeloFormulario("PROP"))
            {
                myForm.AntesSalvar += new Formulario.AntesSalvarEventHandler(myForm_AntesSalvarFormularioProposta);
                myForm.AposSalvar += new Formulario.AposSalvarEventHandler(myForm_AposSalvarFormularioProposta);
            }

            //Control gridFormulario = myForm.getFormulario('C', CssFilePath, CssPostfix, "propostas_popUpEditaFormulario.aspx", new Unit("100%"), new Unit("600px"));
            Control gridFormulario = myForm.constroiInterfaceFormulario(false, IsPostBack, null, null, "", "");

            form1.Controls.Add(gridFormulario);
        }
    }

    void myForm_AntesSalvarFormularioProposta(object sender, EventFormsWF e, ref string mensagemErroEvento)
    {
        string detalheProjeto = "";
        string categoriaProjeto = "";
        if (e.camposControladoSistema != null)
        {
            for (int i = 0; i < e.camposControladoSistema.Count; i++)
            {
                object[] Controles = e.camposControladoSistema[i];
                if (Controles[0].ToString() == "DESC")
                    tituloFormulario = (Controles[2] as ASPxTextBox).Text.Replace("'", "´");
                if (Controles[0].ToString() == "DETA")
                    detalheProjeto = (Controles[2] as ASPxMemo).Text.Replace("'", "´");
                if (Controles[0].ToString() == "CATE")
                    categoriaProjeto = (Controles[2] as ASPxComboBox).Value.ToString();

            }
            if (tituloFormulario != "")
            {
                if (e.operacaoInclusaoEdicao == 'I')
                {
                    // chama a procedure para incluir o projeto;
                    string comandoSQL = string.Format(
                        @"BEGIN
                            DECLARE @CodigoProjeto int

                            EXEC [dbo].[p_InsereProposta] '{0}', {1}, {2}, {3}, '{4}', @CodigoProjeto out

                            SELECT @CodigoProjeto

                          END", tituloFormulario, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, categoriaProjeto, detalheProjeto );
                    DataSet ds = cDados.getDataSet(comandoSQL);
                    e.codigoProjeto = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                else if (e.operacaoInclusaoEdicao== 'E')
                {
                    string comandoSQL = string.Format(
                        @"  UPDATE projeto
                               SET NomeProjeto = '{1}'
                                 , DescricaoProposta = '{2}'
                                 , CodigoCategoria = {3}
                            WHERE codigoProjeto = {0}
                          ", e.codigoProjeto, tituloFormulario, detalheProjeto, categoriaProjeto);
                    int afetados = 0;
                    cDados.execSQL(comandoSQL, ref afetados);
                }
            }
        }
    }

    void myForm_AposSalvarFormularioProposta(object sender, EventFormsWF e, ref string mensagemErroEvento)
    {
        // o pós-evento Salvar só pode ser executado para a inclusão da proposta
        // =====================================================================
        if (e.operacaoInclusaoEdicao == 'I')
        {
            string comandoSQL = string.Format(
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
            ", codigoWorkFlow, tituloFormulario, codigoUsuarioResponsavel, e.codigoProjeto, e.codigoFormulario);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataTable dt = ds.Tables[0];
                e.parametros.Add("CodigoInstanciaWf", dt.Rows[0]["CodigoInstanciaWf"].ToString());
                e.parametros.Add("CodigoEtapaWf", dt.Rows[0]["CodigoEtapaInicial"].ToString());
                e.parametros.Add("SequenciaOcorrenciaEtapaWf", "1");
            }
        }
        else if (e.operacaoInclusaoEdicao == 'E')
        {
            string CodigoInstanciaWf = e.parametros["CodigoInstanciaWf"].ToString();
            string comandoSQL = string.Format(
                @"  UPDATE InstanciasWorkflows
                       SET NomeInstancia = LEFT('{2}',250)
                     WHERE [CodigoWorkflow] = {0}
                       AND [CodigoInstanciaWf] = {1}
                          ", codigoWorkFlow, CodigoInstanciaWf, tituloFormulario);
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
        }
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

}
