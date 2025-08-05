using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;

public partial class _Projetos_Administracao_PlanoProjeto_001 : System.Web.UI.Page
{
    #region Fields

    private Dictionary<string, List<ITextControl>> dicionarioColunasControles;
    private string _ConnectionString;
    private long codigoRevisaoPPJ;
    private int codigoProjeto;
    private int codigoUsuario;
    private int codigoEntidade;
    private bool somenteLeitura;
    private dados cDados;

    #endregion

    #region Properties

    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                _ConnectionString = cDados.classeDados.getStringConexao();
            return _ConnectionString;
        }
    }

    private Dictionary<string, List<ITextControl>> DicionarioColunasControles
    {
        get
        {
            if (dicionarioColunasControles == null)
            {
                dicionarioColunasControles = new Dictionary<string, List<ITextControl>>();

                #region Preenche dicionário

                AdicionarItemDicionario("DescricaoCategoriaProjeto", lblCategoria);
                AdicionarItemDicionario("DataInicioProjeto", lblDataInicio);
                AdicionarItemDicionario("DataTerminoProjeto", lblDataFim);
                AdicionarItemDicionario("NomeGerenteProjeto", lblNomeResponsavel);
                AdicionarItemDicionario("SiglaUnidadeNegocioGP", lblSiglaUnidadeResponsavel);
                AdicionarItemDicionario("NumeroMatriculaGP", lblMatriculaResponsavel);
                AdicionarItemDicionario("NomeUsuarioPatrocinador", lblNomePatrocinador, lblPatrocinadorProjeto);
                AdicionarItemDicionario("SiglaUnidadeNegocioPatrocinador", lblSiglaUnidadePatrocinador);
                AdicionarItemDicionario("NumeroMatriculaPatrocinador", lblMatriculaPatrocinador);
                AdicionarItemDicionario("DescricaoObjetivoGeral", txtObjetivoGeral);
                AdicionarItemDicionario("DescricaoObjetivosEspecificos", txtObjetivosEspecificos);
                AdicionarItemDicionario("DescricaoJustificativa", txtJustificativa);
                AdicionarItemDicionario("DescricaoEscopo", txtEscopo);
                AdicionarItemDicionario("DescricaoNaoEscopo", txtNaoEscopo);
                AdicionarItemDicionario("DescricaoStakeholders", txtStakeholders);
                AdicionarItemDicionario("DescricaoPremissas", txtPremissas);
                AdicionarItemDicionario("DescricaoRestricoes", txtRestricoes);
                AdicionarItemDicionario("DescricaoBeneficiosEsperados", txtBeneficiosEsperados);
                AdicionarItemDicionario("DescricaoOrcamentoPrevisto", txtOrcamentoPrevisto);
                AdicionarItemDicionario("NomeUsuarioGerenteNacional", lblGerenteNacionalProjeto);
                AdicionarItemDicionario("NomeUsuarioSuporteEP", lblEscritorioProjetos);
                AdicionarItemDicionario("IdentificacaoAprovacaoDocumento", txtComprovacaoAprovacaoDocumento);

                #endregion
            }
            return dicionarioColunasControles;
        }
    }

    #endregion

    #region Methods

    private void DefineValorComponentesPeloNomeColuna(string nomeColuna, object valor)
    {
        if (DicionarioColunasControles.ContainsKey(nomeColuna))
        {
            string strValor = valor is DateTime ?
                string.Format("{0:d}", valor) :
                string.Format("{0}", valor);
            DicionarioColunasControles[nomeColuna].ForEach(ctrl => ctrl.Text = strValor);
        }
    }

    private void AdicionarItemDicionario(string nomeColuna, params ITextControl[] controles)
    {
        DicionarioColunasControles.Add(nomeColuna, new List<ITextControl>(controles));
    }

    private void InicializaDados()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoProjeto INT
DECLARE @CodigoUsuario INT
DECLARE @CodigoEntidade INT
	SET @CodigoProjeto = {0}
	SET @CodigoUsuario = {1}
	SET @CodigoEntidade = {2}

EXEC	[dbo].[p_ppj_obtemDadosRevisao]
		@in_codigoProjeto = @CodigoProjeto,
		@in_codigoUsuario = @CodigoUsuario,
		@in_codigoEntidade = @CodigoEntidade",
        codigoProjeto,
        codigoUsuario,
        codigoEntidade);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        DataTable dt = ds.Tables[0];
        DataRow dr = dt.Rows[0];
        foreach (string nomeColuna in DicionarioColunasControles.Keys)
            DefineValorComponentesPeloNomeColuna(nomeColuna, dr[nomeColuna]);
        somenteLeitura = dr["IndicaPermissaoAlteracao"].ToString().ToUpper().Equals("N");
        codigoRevisaoPPJ = (long)dr["CodigoRevisaoPPJ"];
        Session["CodigoRevisaoPPJ"] = codigoRevisaoPPJ;
        Session["CodigoUsuario"] = codigoUsuario;
    }

    private void SetConnectionStrings()
    {
        dsAlinhamentoEstrategico.ConnectionString = ConnectionString;
        dsEntregas.ConnectionString = ConnectionString;
        dsEquipeProjeto.ConnectionString = ConnectionString;
        dsHistoricoRevisoes.ConnectionString = ConnectionString;
        dsPlanoComunicacao.ConnectionString = ConnectionString;
        dsRelacionamento.ConnectionString = ConnectionString;
        dsProjetos.ConnectionString = ConnectionString;
        dsMatrizResponsabilidade.ConnectionString = ConnectionString;
        dsAnaliseRiscos.ConnectionString = ConnectionString;
    }

    private bool SalvaAlteracoes()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoRevisaoPPJ bigint,
	    @CodigoProjeto int,
	    @CodigoUsuarioAtualizacao int,
	    @DescricaoObjetivoGeral varchar(2000),
	    @DescricaoObjetivosEspecificos varchar(8000),
	    @DescricaoJustificativa varchar(8000),
	    @DescricaoStakeholders varchar(8000),
	    @DescricaoEscopo varchar(8000),
	    @DescricaoNaoEscopo varchar(8000),
	    @DescricaoPremissas varchar(8000),
	    @DescricaoRestricoes varchar(8000),
	    @DescricaoBeneficiosEsperados varchar(8000),
	    @DescricaoOrcamentoPrevisto varchar(8000),
	    @IdentificacaoAprovacaoDocumento varchar(500)

	SET @CodigoRevisaoPPJ = {0}
	SET @CodigoProjeto = {1}
	SET @CodigoUsuarioAtualizacao = {2}
	SET @DescricaoObjetivoGeral = '{3}'
	SET @DescricaoObjetivosEspecificos = '{4}'
	SET @DescricaoJustificativa = '{5}'
	SET @DescricaoStakeholders = '{6}'
	SET @DescricaoEscopo = '{7}'
	SET @DescricaoNaoEscopo = '{8}'
	SET @DescricaoPremissas = '{9}'
	SET @DescricaoRestricoes = '{10}'
	SET @DescricaoBeneficiosEsperados = '{11}'
	SET @DescricaoOrcamentoPrevisto = '{12}'
	SET @IdentificacaoAprovacaoDocumento = '{13}'

EXEC	[dbo].[p_ppj_AtualizaCaracterizacao]
		@in_codigoRevisaoPPJ = @CodigoRevisaoPPJ,
		@in_codigoProjeto = @CodigoProjeto,
		@in_codigoUsuarioAtualizacao = @CodigoUsuarioAtualizacao,
		@in_descricaoObjetivoGeral = @DescricaoObjetivoGeral,
		@in_descricaoObjetivosEspecificos =@DescricaoObjetivosEspecificos,
		@in_descricaoJustificativa = @DescricaoJustificativa,
		@in_descricaoStakeholders = @DescricaoStakeholders,
		@in_descricaoEscopo = @DescricaoEscopo,
		@in_descricaoNaoEscopo = @DescricaoNaoEscopo,
		@in_descricaoPremissas = @DescricaoPremissas,
		@in_descricaoRestricoes = @DescricaoRestricoes,
		@in_descricaoBeneficiosEsperados = @DescricaoBeneficiosEsperados,
		@in_descricaoOrcamentoPrevisto = @DescricaoOrcamentoPrevisto,
		@in_identificacaoAprovacaoDocumento = @IdentificacaoAprovacaoDocumento",
        codigoRevisaoPPJ,
        codigoProjeto,
        codigoUsuario,
        txtObjetivoGeral.Text.Replace("'", "''"),
        txtObjetivosEspecificos.Text.Replace("'", "''"),
        txtJustificativa.Text.Replace("'", "''"),
        txtStakeholders.Text.Replace("'", "''"),
        txtEscopo.Text.Replace("'", "''"),
        txtNaoEscopo.Text.Replace("'", "''"),
        txtPremissas.Text.Replace("'", "''"),
        txtRestricoes.Text.Replace("'", "''"),
        txtBeneficiosEsperados.Text.Replace("'", "''"),
        txtOrcamentoPrevisto.Text.Replace("'", "''"),
        txtComprovacaoAprovacaoDocumento.Text.Replace("'", "''"));

        #endregion

        int registrosAfetados = 0;
        return cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    private void DesabilitaControles()
    {
        var controles = new List<ASPxEdit>()
        {
            txtBeneficiosEsperados,
            txtComprovacaoAprovacaoDocumento,
            txtEscopo,
            txtJustificativa,
            txtNaoEscopo,
            txtObjetivoGeral,
            txtObjetivosEspecificos,
            txtOrcamentoPrevisto,
            txtPremissas,
            txtRestricoes,
            txtStakeholders
        };
        var grids = new List<ASPxGridView>()
        {
            gvAlinhamentoEstrategico,
            gvAnaliseRiscos,
            gvEntregas,
            gvEquipeProjeto,
            gvHistorico,
            gvPlanoComunicacao,
            gvRelacionamento
        };
        controles.ForEach(ctrl =>
        {
            ctrl.ReadOnly = true;
            ctrl.ReadOnlyStyle.BackColor = Color.LightGray;
        });
        grids.ForEach(grid =>
        {
            var col = grid.Columns.OfType<GridViewCommandColumn>().SingleOrDefault();
            if (col != null)
                col.Visible = false;
        });
        btnSalvar.Enabled = false;
    }

    private void SalvaAlteracaoNomesColunas()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
EXECUTE [dbo].[p_ppj_AtualizaRegistroMtzRsp] 
    {0}
  , {1}
  ,'{2}'
  ,'{3}'
  ,'{4}'
  ,'{5}'
  ,'{6}'
  ,'{7}'
  ,'{8}'
  ,'{9}'
  ,'{10}'
  , {11}",
codigoRevisaoPPJ,
codigoProjeto,
txtGrupo1.Text.Replace("'", "''"),
txtGrupo2.Text.Replace("'", "''"),
txtGrupo3.Text.Replace("'", "''"),
txtGrupo4.Text.Replace("'", "''"),
txtGrupo5.Text.Replace("'", "''"),
txtGrupo6.Text.Replace("'", "''"),
txtGrupo7.Text.Replace("'", "''"),
txtGrupo8.Text.Replace("'", "''"),
txtGrupo9.Text.Replace("'", "''"),
codigoUsuario
);

        #endregion

        int qtdeRegistrosAfetados = 0;
        cDados.execSQL(comandoSql, ref qtdeRegistrosAfetados);
    }

    private void AtualizaNomesColunas()
    {
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoRevisaoPPJ bigint,
        @CodigoProjeto int
        
    SET @CodigoRevisaoPPJ = {0}
    SET @CodigoProjeto = {1}

SELECT * FROM [f_ppj_getDadosMtzRspIdtGrupos] (@CodigoRevisaoPPJ,@CodigoProjeto)"
, codigoRevisaoPPJ, codigoProjeto);

        #endregion

        var colunas = gvMatrizResponsabilidade.Columns;
        DataSet ds = cDados.getDataSet(comandoSql);
        DataRow dr = ds.Tables[0].Rows[0];
        for (int i = 1; i <= 9; i++)
        {
            string identificacaoGrupo = dr["IdentificacaoGrupo" + i] as string;
            GridViewColumn col = colunas["DescricaoIntegrantesGrupo" + i];
            col.Caption = identificacaoGrupo;
        }
    }

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        codigoProjeto = int.Parse(Request.QueryString["cp"]);
        codigoUsuario = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));

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
        cDados.aplicaEstiloVisual(this);
       
        SetConnectionStrings();
        InicializaDados();        
    }

    public void CustomizarGrids()
    {
        var grids = new List<ASPxGridView>()
        {
            gvAlinhamentoEstrategico,
            gvAnaliseRiscos,
            gvEntregas,
            gvEquipeProjeto,
            gvHistorico,
            gvPlanoComunicacao,
            gvRelacionamento,
            gvMatrizResponsabilidade            
        };
        foreach (var grid in grids)
        {
            grid.Settings.ShowFilterRow = false;
            grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        }        
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (somenteLeitura)
        {
            DesabilitaControles();
        }            
        CustomizarGrids();
    }

    protected void callback_Callback(object source, CallbackEventArgs e)
    {        
        try
        {
            SalvaAlteracoes();
            callback.JSProperties["cp_status"] = "ok";
            e.Result = "Alterações salvas com sucesso.";
        }
        catch (Exception ex)
        {
            callback.JSProperties["cp_status"] = "erro";
            e.Result = ex.Message;
        }
    }

    protected void gvMatrizResponsabilidade_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "IdentificacaoEntregavel")
        {
            string indicaNomeTarefaEditavel = gvMatrizResponsabilidade
                .GetRowValues(e.VisibleIndex, "IndicaNomeTarefaEditavel") as string;
            e.Editor.ReadOnly = indicaNomeTarefaEditavel == "N";
        }
    }

    protected void gvMatrizResponsabilidade_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        switch (e.Parameters)
        {
            case "alterar_nomes_colunas":
                ASPxTextBox[] controles = {
                    txtGrupo1, txtGrupo2, txtGrupo3,
                    txtGrupo4, txtGrupo5, txtGrupo6,
                    txtGrupo7, txtGrupo8, txtGrupo9
                };
                var colunas = gvMatrizResponsabilidade.Columns;
                for (int i = 1; i <= 9; i++)
                {
                    GridViewColumn col = colunas["DescricaoIntegrantesGrupo" + i];
                    col.Caption = controles[i - 1].Text;
                }
                SalvaAlteracaoNomesColunas();
                break;
            default:
                break;
        }
    }

    protected void gvMatrizResponsabilidade_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        AtualizaNomesColunas();
    }

    protected void gvMatrizResponsabilidade_Load(object sender, EventArgs e)
    {
        if (!IsCallback)
            AtualizaNomesColunas();
    }

    protected void gvMatrizResponsabilidade_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        var colunas = gvMatrizResponsabilidade.Columns;
        for (int i = 1; i <= 9; i++)
        {
            GridViewColumn col = colunas["DescricaoIntegrantesGrupo" + i];
            e.Properties["cpColGrupo" + i] = col.Caption;
        }
    }

    #endregion
}