using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using CDIS;

public partial class espacoTrabalho_FrmListaPendenciasMobileWf2 : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;
    public string stringConexao = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
       
        // cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // cDados.setInfoSistema("IDUsuarioLogado", DecodificaHashToUserCode(hashCode));

        cDados = CdadosUtil.GetCdados(null);

        if (Request.QueryString["cm"] == null)
        {

            if ((Request.QueryString["msg"] != null) && (Request.QueryString["msg"].ToString() != ""))
            {
                string stringErro = Request.QueryString["msg"].ToString();

                gvDados.SettingsText.EmptyCard = stringErro;
            }
            else if (Request.QueryString["sc"] != null)
            {
                string stringCriptografada = Request.QueryString["sc"].ToString();
                stringConexao = stringCriptografada;

                decriptaStringConexao(stringCriptografada);
            }
        }


        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // How to get rid of this???
        cDados.setInfoSistema("ResolucaoCliente", "320x640");
        cDados.setInfoSistema("IDEstiloVisual", "iOS");
      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Reforço para validação para que em caso de mobile abra a página de fluxo de mobile, e vice versa.
        if (!Request.Browser.IsMobileDevice)
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(HttpContext.Current.Request.RawUrl, "PendenciasWorkflow.aspx"));
        }
        
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if(gvDados.SettingsText.EmptyCard == "")
            populaGrid();
         Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PendenciasWorkflow.js""></script>"));
        this.TH(this.TS("PendenciasWorkflow"));
        // Header.Controls.Add(cDados.getLiteral(@"<script> var cp_Path = '" + cDados.getPathSistema() + "'; console.log(cp_Path); </script>"));

        // gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        DataSet dsParametro = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "iniciaisFluxoTelaPendencia");
        if(cDados.DataSetOk(dsParametro) && 
            cDados.DataTableOk(dsParametro.Tables[0]) && 
            dsParametro.Tables[0].Rows[0]["iniciaisFluxoTelaPendencia"].ToString().Trim() != "")
        {
            ((CardViewColumn)gvDados.Columns["col_NomeFluxo"]).Visible = false;
        }        
    }

    private void decriptaStringConexao(string stringCriptografada)
    {
        string chavePrivada = "", stringPlana;
        DataSet dsChaveWfMobile = cDados.getParametrosSistema(-1, "chaveAutenticacaoWsMobile");
        DateTime datConexao;
        int codigoEntidade = 0, codigoUsuario = 0;

        if (cDados.DataSetOk(dsChaveWfMobile) && cDados.DataTableOk(dsChaveWfMobile.Tables[0]))
        {
            chavePrivada = dsChaveWfMobile.Tables[0].Rows[0]["chaveAutenticacaoWsMobile"] + "";
            stringPlana = Cripto.descriptografar(stringCriptografada, chavePrivada);
            if (string.IsNullOrEmpty(stringPlana) == false)
            {
                string[] valores = stringPlana.Split(';');

                if (valores.Length > 2)
                {
                    if (DateTime.TryParse(valores[1].TrimEnd(' '), out datConexao))
                    {
                        if (datConexao.AddHours(1).CompareTo(DateTime.Now) >= 0)
                        {

                            if (int.TryParse(valores[2], out codigoEntidade))
                            {
                                string strWhere = string.Format(" AND us.[Email] = '{0}' ", valores[0].TrimEnd(' '));
                                DataSet dsUsr = cDados.getDadosResumidosUsuario(strWhere);
                                if (cDados.DataSetOk(dsUsr) && cDados.DataTableOk(dsUsr.Tables[0]))
                                {
                                    codigoUsuario = int.Parse(dsUsr.Tables[0].Rows[0]["CodigoUsuario"].ToString());
                                }
                            }
                        }
                    }

                }
            }
        }

        cDados.setInfoSistema("CodigoEntidade", codigoEntidade);
        cDados.setInfoSistema("IDUsuarioLogado", codigoUsuario);
        return;
    }

    private void populaGrid()
    {

        string comandoSQL = string.Format(@"
            BEGIN
                DECLARE @CodigoFluxo as int
                DECLARE @IniciaisFluxo as varchar(max)
				SET @CodigoFluxo = NULL
				SET @IniciaisFluxo = NULL
				SELECT @IniciaisFluxo = Valor FROM ParametroConfiguracaoSistema
                WHERE Parametro = 'iniciaisFluxoTelaPendencia' AND CodigoEntidade = {1}
				IF @IniciaisFluxo IS NULL
				   EXEC [dbo].[p_wf_obtemListaInstanciasUsuario]
							  @in_identificadorUsuario	= '{0}'
							, @in_codigoEntidade		= {1}
							, @in_codigoFluxo			= NULL
							, @in_codigoProjeto 		= NULL
							, @in_somenteInteracao       = 1
							, @in_somentePendencia       = 1
							, @in_palavraChave           = NULL
                ELSE
				   BEGIN
				     DECLARE @tbl_Retorno TABLE
					   ([CodigoWorkflow] [int] NOT NULL,
						[CodigoFluxo] [int] NULL,
						[NumeroProtocolo] [varchar](10) NULL,
						[NomeFluxo] [varchar](30) NULL,
						[VersaoFluxo] [varchar](10) NULL,
						[CodigoInstanciaWf] [bigint] NOT NULL,
						[NomeInstanciaWf] [varchar](250) NULL,
						[CodigoEtapaAtual] [int] NULL,
						[NomeEtapaAtual] [varchar](250) NULL,
						[DescricaoEtapaAtual] [varchar](1024) NULL,
						[OcorrenciaAtual] [int] NULL,
						[DataInicioInstancia] [datetime] NULL,
						[DataTerminoInstancia] [datetime] NULL,
						[CodigoProjeto] [int] NULL,
						[CodigoEtapaInicial] [int] NULL,
						[DataInicioEtapaAtual] [datetime] NULL,
						[TempoDecorrido] [real] NULL,
						[Status] [varchar](10) NULL,
						[IdentificadorUsuarioCriadorInstancia] [varchar](50) NULL,
						[UsuarioCriacaoInstancia] [varchar](60) NULL,
						[CodigoUltimaEtapa] [int] NULL,
						[UltimaOcorrencia] [int] NULL,
						[IndicaSubFluxoAtual] [char](1) NULL,
						[CodigoEtapaAnterior] [int] NULL,
						[NomeEtapaAnterior] [varchar](250) NULL,
						[DescricaoEtapaAnterior] [varchar](1024) NULL,
						[NivelAcessoEtapaAtual] [int] NULL,
						[NivelAcessoUltimaEtapa] [int] NULL,
						[IndicaPermissaoReversao] [varchar](1) NULL,
						[IndicaPermissaoCancelamento] [char](1) NULL,
						[IndicaInteracaoEtapaAtual] [char](3) NULL,
						[IndicaPendenciaUsuario] [char](1) NOT NULL,
						[IndicaAtraso] [varchar](900) NULL,
						[NomeProjeto] [varchar](255) NULL,
						[NomeUnidadeNegocio] [varchar](100) NULL,
						[Despesas de Viagem] [char](1) NULL,
						[Material de Apoio] [char](1) NULL,
						[EnvioPlacasReconhecimento] [char](1) NULL,
						[Prazo] [datetime] NULL,
						[IndicaEtapaDisjuncao] [varchar](1) NULL,
						[CodigoProjeto2] [int] NULL)
				     DECLARE @b_IniciaisFluxo Varchar(30)
				     DECLARE cCursor CURSOR LOCAL FOR
					 SELECT Item
					   FROM dbo.f_Split(@IniciaisFluxo,';')
                     OPEN cCursor
					 FETCH NEXT FROM cCursor INTO @b_IniciaisFluxo
					 WHILE @@FETCH_STATUS = 0
					   BEGIN
					     SET @CodigoFluxo = NULL
						 SELECT @CodigoFluxo = MIN(CodigoFluxo)
						   FROM Fluxos
						  WHERE IniciaisFluxo = @b_IniciaisFluxo
						    AND CodigoEntidade = {1}
						  IF @CodigoFluxo IS NOT NULL
						     INSERT INTO @tbl_Retorno
							 EXEC [dbo].[p_wf_obtemListaInstanciasUsuario]
								  @in_identificadorUsuario	= '{0}'
								, @in_codigoEntidade		= {1}
								, @in_codigoFluxo			= @CodigoFluxo
								, @in_codigoProjeto 		= NULL
								, @in_somenteInteracao       = 1
								, @in_somentePendencia       = 1
								, @in_palavraChave           = NULL
					     FETCH NEXT FROM cCursor INTO @b_IniciaisFluxo
					   END
					 CLOSE cCursor
					 DEALLOCATE cCursor
				   END
				   SELECT * FROM @tbl_Retorno
            END", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.SettingsSearchPanel.Visible = false;
            gvDados.DataBind();
        }

    }


  /*  public string getRowCount()
    {
        string retorno = "";
        int quantidadeLinhas = 0;
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            if (!gvDados.IsGroupRow(i))
                quantidadeLinhas++;
        }

        retorno = quantidadeLinhas + " pendências";

        return retorno;
    }*/
}