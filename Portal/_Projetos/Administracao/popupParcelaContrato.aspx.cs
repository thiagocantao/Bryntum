using DevExpress.Web;
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class _Projetos_Administracao_popupParcelaContrato : System.Web.UI.Page
{
    dados cDados;

    public int codigoContrato = -1;
    public int NumeroAditivoContrato = -1;
    public int NumeroParcela = -1;
    public int CodigoProjetoParcela = -1;

    public string somenteLeitura = "";
    DateTime dtInicioVigencia = DateTime.Now;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    public bool podeEditar = true;

    public bool geraLancamentoFinanceiro = false;
    string IniciaisTipoAssociacao = "CT";
    private string swhere1;

    private static decimal campoValorPrevistoInicial = 0;
    private static decimal campoValorPagoContratoInicial = 0;

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
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());



    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        cDados.aplicaEstiloVisual(this);
        if (Request.QueryString["na"] != null && Request.QueryString["na"] + "" != "")
            NumeroAditivoContrato = int.Parse(Request.QueryString["na"].ToString());

        if (Request.QueryString["np"] != null && Request.QueryString["np"] + "" != "")
            NumeroParcela = int.Parse(Request.QueryString["np"].ToString());

        if (Request.QueryString["cpp"] != null && Request.QueryString["cpp"] + "" != "")
            CodigoProjetoParcela = int.Parse(Request.QueryString["cpp"].ToString());

        if (Request.QueryString["cc"] != null && Request.QueryString["cc"] + "" != "")
            codigoContrato = int.Parse(Request.QueryString["cc"].ToString());

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltPar");

        bool atualizaRealizadoConformePrevisto = getAtualizaRealizadoConformePrevisto();

        if (Request.QueryString["IVG"] != null && Request.QueryString["IVG"].ToString() != "")
        {
            string format = "dd/MM/yyyy";
            DateTime.TryParseExact(Request.QueryString["IVG"].ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtInicioVigencia);
            //se o parametro estiver ligado não restringir a data de vencimento
            if (atualizaRealizadoConformePrevisto == false)
            {
                dtVencimento.MinDate = dtInicioVigencia;
            }            
        }
        if (Request.QueryString["RO"] != null)
        {
            somenteLeitura = Request.QueryString["RO"].ToString();
        }

        if (atualizaRealizadoConformePrevisto == false)
        {
            dtPagamento.MaxDate = DateTime.Now;
        }

        ConfiguraLinkIniciativasAssociadasParcela();

        configuraLabelsVencimentoEPrevistoContrato();

        VerificaSeGeraLancamentosFinanceiro();
        
        verificaSeExibeAbaDocumentos();

        if (geraLancamentoFinanceiro == true)
        {
            //ddlContaContabil.ClientVisible = true;
            //lblTituloContaContabil.ClientVisible = true;
            divContaContabil.Style.Add("display", "block");
            divContaContabil.Style.Add("padding-top", "5px");

            // se tipo pessoa foi informado na chamada da página e parâmetro gera lançamento financeiro = sim, popula o combo de conta contábil de acordo com tipo da pessoa
            // caso contrário as informações de conta são inativadas
            if (Request.QueryString["TP"] != null)
            {
                string depesaReceita = Request.QueryString["TP"].ToString();

                switch (depesaReceita)
                {
                    case "F":
                        depesaReceita = "S";
                        break;
                    case "C":
                        depesaReceita = "E";
                        break;
                    default:
                        depesaReceita = "";
                        break;
                }
                swhere1 = depesaReceita;

            }
            Session["sfEntradaSaida"] = swhere1;
            populaDdlConta();
        }
        else
        {
            divContaContabil.Style.Add("display", "none");
            //ddlContaContabil.ClientVisible = false;
            //lblTituloContaContabil.ClientVisible = false;
        }



        populaddlProjetoPrograma();
       


        if (!IsPostBack)
        {
            pupulaTela();

            campoValorPrevistoInicial = (decimal)spValorPrevisto.Value;
            campoValorPagoContratoInicial = (spValorPago.Value == null) ? 0 : (decimal)spValorPago.Value;
        }
        defineAlturaTela();
        cDados.setaTamanhoMaximoMemo(memoHistorico, 500, lblContadorMemo);

        setaMensagemDeAviso();

        
        dtVencimento.JSProperties["cp_atualizaRealizadoConformePrevisto"] = (atualizaRealizadoConformePrevisto == true) ? "S" : "N";

       
        lblAvisos.Font.Size = new FontUnit("11pt");
    }

    private void verificaSeExibeAbaDocumentos()
    {
        //se retornar valor > 0 então a aba de documentos deve ser exibida
        //se não retornar valor > 0 então a aba de documentos não deverá ser exibida
        string comandosql = string.Format(@"
        SELECT ISNULL(COUNT(1), 0) as quantidadeAnexos
          FROM [dbo].[ParcelaContrato]
         WHERE [CodigoContrato] = {0} 
           AND [NumeroAditivoContrato] = {1} 
           AND [NumeroParcela] = {2} AND IndicaTipoIntegracao = 'SD'", codigoContrato, NumeroAditivoContrato, NumeroParcela);


        bool retorno = false;

        DataSet ds = cDados.getDataSet(comandosql);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0)
        {
            retorno = true;
        }

        tabControl.TabPages[1].Visible = retorno;
        //tabControl.TabStyle.
    }

    private void setaMensagemDeAviso()
    {
        var campoVencimentoPrevisto = dtVencimento.Value == null ? DateTime.Parse("01/01/1901") : dtVencimento.Value;
        var campoVencimentoPrevistoContrato = dtTerminoContrato.Value == null ? DateTime.Parse("01/01/2500") : dtTerminoContrato.Value;
        var spValorPagoAux = (spValorPago.Value == null) ? 0 : (decimal)spValorPago.Value;

        var campoValorContrato = (decimal)spValorContrato.Value;
        var campoSaldoContrato = (decimal)spSaldoContrato.Value;
        var campoValorPrevisto = (decimal)spValorPrevisto.Value + campoValorPrevistoInicial;
        var campoValorPagoContrato = (decimal)spValorPagoAux + campoValorPagoContratoInicial;
        var valorantigo = (decimal)0.00;
        var valorNovo = (decimal)0.00; ;
        if (dtPagamento.Value == null)
        {
            valorantigo = campoValorPrevistoInicial;
            valorNovo = (decimal)spValorPrevisto.Value;
        }
        else
        {
            valorantigo = campoValorPagoContratoInicial;
            valorNovo = (decimal)spValorPago.Value;
        }
        StringBuilder sb = new StringBuilder();

        if ((DateTime)campoVencimentoPrevisto > (DateTime)campoVencimentoPrevistoContrato)
        {
            sb.AppendLine(" * O vencimento previsto para a parcela atual é maior que o vencimento do contrato.");
        }

        if (((decimal)campoSaldoContrato + valorantigo - valorNovo) < 0)
        {
            sb.AppendLine(" * Os valores envolvidos irão extrapolar o valor do contrato.");
        }

        lblAvisos.Text = sb.ToString();
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        int alturaDoFormPOPUP = int.Parse(Request.QueryString["ALT"].ToString());

        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        //uma combo e seu respectivo titulo tem 18 e 20px de altura
        int alturaOcupadaPorComponentesVisiveis = alturaDoFormPOPUP;//376;
        if (divContaContabil.Style.Value.Contains("block") == true)
        {
            alturaOcupadaPorComponentesVisiveis -= 45;
        }
        if (divProjeto.Style.Value.Contains("block") == true)
        {
            alturaOcupadaPorComponentesVisiveis -= 45;
        }
        if (divPacoteEAP.Style.Value.Contains("block") == true)
        {
            alturaOcupadaPorComponentesVisiveis -= 39;
        }

        if (divRecurso.Style.Value.Contains("block") == true)
        {
            alturaOcupadaPorComponentesVisiveis -= 45;
        }
        alturaOcupadaPorComponentesVisiveis -= 25;//botoes e espaçamentos
        alturaOcupadaPorComponentesVisiveis -= 20;
        alturaOcupadaPorComponentesVisiveis -= 28;

        alturaOcupadaPorComponentesVisiveis -= 50;//Colocação dos componentes read only no topo do popup

        int alturaQueSobraProMemoHistorico = alturaOcupadaPorComponentesVisiveis - 130;
        memoHistorico.Height = new Unit(alturaQueSobraProMemoHistorico + "px");
    }

    private void ConfiguraLinkIniciativasAssociadasParcela()
    {
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "pda_associaParcelaContratoIniciativa");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            var associaParcelaContratoIniciativa = dsParametros.Tables[0].Rows[0]["pda_associaParcelaContratoIniciativa"] as string;
            if (associaParcelaContratoIniciativa == "S")
            {
                if (NumeroParcela == -1)
                {
                    linkIniciativasAssociadasParcela.ClientEnabled = false;
                    linkIniciativasAssociadasParcela.ToolTip = "É necessário salvar as alterações para acessar o link";
                }
                else
                {
                    linkIniciativasAssociadasParcela.ClientEnabled = true;
                    linkIniciativasAssociadasParcela.Text = ObtemTextoLinkIniciativasAssociadasParcela();
                    linkIniciativasAssociadasParcela.ClientSideEvents.Click = ObtemCodigoClientEventoClickLinkIniciativasAssociadasParcela();
                }
            }
        }
    }

    private string ObtemCodigoClientEventoClickLinkIniciativasAssociadasParcela()
    {
        string codigo = string.Format("function(s, e) {{AbrePopupIniciativasAssociadasParcela({0}, {1}, {2})}}", codigoContrato, NumeroAditivoContrato, NumeroParcela);

        return codigo;
    }

    private string ObtemTextoLinkIniciativasAssociadasParcela()
    {
        string comandoSql = string.Format(@"
 SELECT COUNT(1) AS QtdeIniciativasAssociadas
   FROM pda_ExecucaoAcao 
  WHERE CodigoContrato = {0}
		AND NumeroAditivoContrato = {1}
		AND NumeroParcela = {2}",
        codigoContrato,
        NumeroAditivoContrato,
        NumeroParcela);
        var ds = cDados.getDataSet(comandoSql);
        var qtdeIniciativasAssociadas = Convert.ToInt32(ds.Tables[0].Rows[0]["QtdeIniciativasAssociadas"]);

        return string.Format("{0} Iniciativa(s) Associada(s) à Parcela", qtdeIniciativasAssociadas);
    }

    private void configuraLabelsVencimentoEPrevistoContrato()
    {
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelPrevistoParcelaContrato", "labelVencimentoParcelaContrato");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() != "")
            {
                labelPrevistoParcelaContrato.Text = dsParametros.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() + ":";
            }

            if (dsParametros.Tables[0].Rows[0]["labelVencimentoParcelaContrato"].ToString() != "")
            {
                labelVencimentoParcelaContrato.Text = dsParametros.Tables[0].Rows[0]["labelVencimentoParcelaContrato"].ToString() + ":";
            }
        }
    }

    private void VerificaSeGeraLancamentosFinanceiro()
    {
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "parcelaGeraLancamentoFinanceiro");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["parcelaGeraLancamentoFinanceiro"].ToString() != "")
            {
                geraLancamentoFinanceiro = dsParametros.Tables[0].Rows[0]["parcelaGeraLancamentoFinanceiro"].ToString().Trim().ToUpper() == "S";
            }
        }
    }

    private void populaddlProjetoPrograma()
    {
        bool colunaVisivel = false;
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
DECLARE @CodigoContrato INT
    SET @CodigoContrato = {0}
  
 SELECT p.CodigoProjeto AS CodigoPrograma
   FROM Contrato AS c INNER JOIN
        Projeto AS p ON p.CodigoProjeto = c.CodigoProjeto
  WHERE c.CodigoContrato = @CodigoContrato
    AND p.IndicaPrograma = 'S'", codigoContrato);

        #endregion

        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            var valor = dt.Rows[0]["CodigoPrograma"];
            if (valor is int)
            {
                Session["codProg"] = valor;
                colunaVisivel = true;
            }
        }

        divProjeto.Style.Add("display", (colunaVisivel == true) ? "block" : "none");
        if (colunaVisivel == true)
        {
            divProjeto.Style.Add("padding-top", "5px");
        }

        divPacoteEAP.Style.Add("display", (colunaVisivel == true) ? "block" : "none");
        if (colunaVisivel == true)
        {
            divPacoteEAP.Style.Add("padding-top", "5px");
        }

        divRecurso.Style.Add("display", (colunaVisivel == true) ? "block" : "none");
        if (colunaVisivel == true)
        {
            divRecurso.Style.Add("padding-top", "5px");
        }

        if (colunaVisivel == true)
        {
            string comandoSQL = string.Format(@"SELECT rs.* 
   FROM
    (
     SELECT p.CodigoProjeto,
            p.NomeProjeto
       FROM Projeto AS p INNER JOIN
            LinkProjeto AS lp ON lp.CodigoProjetoFilho = p.CodigoProjeto
      WHERE lp.CodigoProjetoPai = {0}
        AND lp.TipoLink = 'PP'
        AND p.DataExclusao IS NULL
    UNION
     SELECT NULL, NULL
    ) AS rs
  ORDER BY 2", Session["codProg"]);

            DataSet ds = cDados.getDataSet(comandoSQL);

            ddlProjetos.TextField = "NomeProjeto";
            ddlProjetos.ValueField = "CodigoProjeto";
            ddlProjetos.DataSource = ds.Tables[0];
            ddlProjetos.DataBind();

        }

    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/popupParcelaContrato.js""></script>"));
        this.TH(this.TS("popupParcelaContrato"));
    }

    private void populaDdlConta()
    {
        string comandoSQL = string.Format(@"        
         SELECT pcfc.CodigoConta,
               pcfc.CodigoReservadoGrupoConta + ' - ' + pcfc.DescricaoConta as DescricaoConta
          FROM PlanoContasFluxoCaixa AS pcfc
         WHERE pcfc.CodigoEntidade = {0}
           AND pcfc.IndicaContaAnalitica = 'S'
           AND pcfc.EntradaSaida = '{1}'
           ORDER BY pcfc.DescricaoConta ASC", codigoEntidadeUsuarioResponsavel, swhere1);

        DataSet ds = cDados.getDataSet(comandoSQL);
        ddlContaContabil.TextField = "DescricaoConta";
        ddlContaContabil.ValueField = "CodigoConta";
        ddlContaContabil.DataSource = ds;
        ddlContaContabil.DataBind();
    }

    private void pupulaTela()
    {
        DataSet ds;

        string strFiltroAditivoContrato = (NumeroAditivoContrato > -1) ? string.Format(" and pc.NumeroAditivoContrato = {0} ", NumeroAditivoContrato) : "";
        string strFiltroAditivoParcela = (NumeroParcela > -1) ? string.Format(" and pc.NumeroParcela = {0} ", NumeroParcela) : ""; ;

        string comandoSQL = string.Format(@"
        SELECT pc.[CodigoContrato]
              ,pc.[NumeroAditivoContrato]
              ,pc.[NumeroParcela]
              ,pc.[ValorPrevisto]
              ,pc.[DataVencimento]
              ,pc.[DataPagamento]
              ,pc.[ValorPago]
              ,pc.[HistoricoParcela]
              ,pc.[DataInclusao]
              ,pc.[CodigoUsuarioInclusao]
              ,pc.[DataUltimaAlteracao]
              ,pc.[CodigoUsuarioUltimaAlteracao]
              ,pc.[NumeroNF]
              ,pc.[ValorRetencao]
              ,pc.[ValorMultas]
              ,pc.[CodigoMedicao]
              ,pc.[Sequencialparcela]
              ,pc.[CodigoConta]
              ,pcfc.CodigoReservadoGrupoConta + ' - ' + pcfc.DescricaoConta as DescricaoConta
              ,pc.[CodigoProjetoParcela]
              ,p.NomeProjeto as NomeProjetoParcela
              ,pc.CodigoCronogramaProjetoPacoteEAP --campo adicionado dia 2 de junho de 2016
              ,pc.CodigoTarefaPacoteEAP            --campo adicionado dia 2 de junho de 2016
              ,pc.CodigoRecursoProjeto             --campo adicionado dia 2 de junho de 2016
			  , (SELECT NomeRecurso FROM RecursoCronogramaProjeto 
                  WHERE CodigoRecursoProjeto = ISNULL(pc.CodigoRecursoProjeto, -1) 
                    AND CodigoCronogramaProjeto = pc.CodigoCronogramaProjetoPacoteEAP) as NomeRecursoProjeto
              --Task 428 PortalEstrategia2016\Gestão Convênio 2 e SGDA Pct 6.1
			  ,c.NumeroContrato
			  ,c.ValorContrato
			  ,pe.NomePessoa
              ,c.DataTermino
              ,pc.DataEmissaoNF
              ,pc.IndicaTipoIntegracao
          FROM [ParcelaContrato] pc
   INNER JOIN [Contrato] c on (c.CodigoContrato = pc.CodigoContrato)
   LEFT JOIN [Pessoa] pe on (pe.CodigoPessoa = c.CodigoPessoaContratada) 
    LEFT JOIN [PlanoContasFluxoCaixa] pcfc on (pcfc.CodigoConta = pc.CodigoConta)
    LEFT JOIN [Projeto] p on (p.CodigoProjeto = pc.CodigoProjetoParcela)
    left JOIN LinkProjeto AS lp ON (lp.CodigoProjetoFilho = p.CodigoProjeto)
    LEFT JOIN [CronogramaProjeto] cp on (cp.CodigoProjeto = p.CodigoProjeto)
         WHERE pc.CodigoContrato = {0} AND pc.[DataExclusao] IS NULL
            {1} 
           {2} ", codigoContrato, strFiltroAditivoContrato, strFiltroAditivoParcela, CodigoProjetoParcela);

        ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow[] dr = ds.Tables[0].Select();
            if (NumeroParcela > -1 && NumeroAditivoContrato > -1)
            {
                preencheCampos(dr);
                desabilitaHabilitaComponentes();
            }
            else
            {
                preencheCamposFixo(dr);
            }

        }
        else
        {
            string comando = string.Format(@"
            SELECT c.NumeroContrato, c.ValorContrato, pe.NomePessoa, c.DataTermino, c.CodigoContrato
              FROM Contrato c 
              left JOIN Pessoa pe on (pe.CodigoPessoa = c.CodigoPessoaContratada)
              WHERE c.CodigoContrato = {0} ", codigoContrato);
            DataSet dsComando = cDados.getDataSet(comando);
            if (cDados.DataSetOk(dsComando) && cDados.DataTableOk(dsComando.Tables[0]))
            {
                DataRow[] dr = dsComando.Tables[0].Select();
                preencheCamposFixo(dr);
            }
        }
    }

    private void preencheCamposFixo(DataRow[] dr)
    {
        txtFornecedor.Text = dr[0]["NomePessoa"].ToString();
        txtNumeroContrato.Text = dr[0]["NumeroContrato"].ToString();
        dtTerminoContrato.Value = dr[0]["DataTermino"];
        spValorContrato.Text = dr[0]["ValorContrato"].ToString();
        setSaldoContrato(spValorContrato.Value, spNumeroParcela.Value, dr[0]["CodigoContrato"]);
    }

    private void preencheCampos(DataRow[] dr)
    {
        spNumeroAditivo.Value = dr[0]["NumeroAditivoContrato"];
        spNumeroParcela.Value = dr[0]["NumeroParcela"];
        spValorPrevisto.Value = dr[0]["ValorPrevisto"];
        dtVencimento.Value = dr[0]["DataVencimento"];
        spValorPago.Value = dr[0]["ValorPago"];
        dtPagamento.Value = dr[0]["DataPagamento"];
        txtNotaFiscal.Text = (dr[0]["NumeroNF"] != null) ? dr[0]["NumeroNF"].ToString() : "";
        dtEmissao.Value = dr[0]["DataEmissaoNF"];
        spRetencao.Value = dr[0]["ValorRetencao"];
        spMultas.Value = dr[0]["ValorMultas"];

        ddlContaContabil.Value = dr[0]["CodigoConta"];
        ddlContaContabil.Text = dr[0]["DescricaoConta"].ToString();

        carregaComboLinhaBase((dr[0]["CodigoProjetoParcela"] == null ||
            dr[0]["CodigoProjetoParcela"].ToString() == "") ? -1 : int.Parse(dr[0]["CodigoProjetoParcela"].ToString()));

        ddlVersaoLB.Value = dr[0]["CodigoCronogramaProjetoPacoteEAP"].ToString();

        memoHistorico.Value = dr[0]["HistoricoParcela"];


        preencheCamposFixo(dr);

        //depois de popular a tela, popula o combo de projetos, o pacote da eap e a combo de recursos no sentido de já populá-los
        //de acordo com os valores que vieram no dataset.
        populaddlProjetoPrograma(dr[0]["CodigoProjetoParcela"], dr[0]["CodigoTarefaPacoteEAP"], dr[0]["CodigoRecursoProjeto"]);
        ddlProjetos.Value = dr[0]["CodigoProjetoParcela"];
        ddlProjetos.Text = dr[0]["NomeProjetoParcela"].ToString();

        
        ddlRecurso.Value = dr[0]["CodigoRecursoProjeto"].ToString();
        ddlRecurso.Text = dr[0]["NomeRecursoProjeto"].ToString();

        if (dr[0]["IndicaTipoIntegracao"] + "" != "")
        {
            spValorPrevisto.ReadOnly = true;
            dtVencimento.ReadOnly = true;
            spValorPago.ReadOnly = true;
            dtPagamento.ReadOnly = true;
            dtEmissao.ReadOnly = true;
            memoHistorico.ReadOnly = true;
        }
    }

    private void setSaldoContrato(object valorContrato, object numeroParcela, object codigoContrato)
    {
        decimal valorPago = 0;
        decimal valorPrevistoSemPagamento = 0;
        decimal saldo = 0;
        //Valor pago
        string comandoSQL = string.Format(@"
        SELECT sum( isnull(ValorPago,0)) as valorPago FROM ParcelaContrato 
          WHERE CodigoContrato = {0} and DataPagamento is not null AND [DataExclusao] IS NULL ", codigoContrato);

        DataSet dsvalorPago = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(dsvalorPago) && cDados.DataTableOk(dsvalorPago.Tables[0]))
        {
            decimal.TryParse(dsvalorPago.Tables[0].Rows[0]["valorPago"].ToString(), out valorPago);
        }

        //Valor previsto sem pagamento
        comandoSQL = string.Format(@"
        SELECT sum( isnull(ValorPrevisto,0)) as ValPrevSemPagto FROM ParcelaContrato 
          WHERE codigocontrato = {0} and DataPagamento is null AND [DataExclusao] IS NULL ", codigoContrato, numeroParcela);

        DataSet dsValorPrevSemPagamento = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(dsValorPrevSemPagamento) && cDados.DataTableOk(dsValorPrevSemPagamento.Tables[0]))
        {
            decimal.TryParse(dsValorPrevSemPagamento.Tables[0].Rows[0]["ValPrevSemPagto"].ToString(), out valorPrevistoSemPagamento);
        }


        saldo = (decimal)valorContrato - valorPago - valorPrevistoSemPagamento;

        spSaldoContrato.Value = saldo;
    }

    private void populaddlProjetoPrograma(object CodigoProjetoParcela, object CodigoTarefaPacoteEAP, object CodigoRecursoProjeto)
    {
        bool colunaVisivel = false;
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
        DECLARE @CodigoContrato INT
            SET @CodigoContrato = {0}
  
         SELECT p.CodigoProjeto AS CodigoPrograma
           FROM Contrato AS c INNER JOIN
                Projeto AS p ON p.CodigoProjeto = c.CodigoProjeto
          WHERE c.CodigoContrato = @CodigoContrato
        AND p.IndicaPrograma = 'S'", codigoContrato);

        #endregion

        DataTable dt = cDados.getDataSet(comandoSql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            var valor = dt.Rows[0]["CodigoPrograma"];
            if (valor is int)
            {
                colunaVisivel = true;
            }
        }

        if (colunaVisivel == true)
        {
            string comandoSQL1 = string.Format(@"SELECT rs.* 
            FROM
            (
                SELECT p.CodigoProjeto,
                    p.NomeProjeto
                FROM Projeto AS p INNER JOIN
                    LinkProjeto AS lp ON lp.CodigoProjetoFilho = p.CodigoProjeto
                WHERE lp.CodigoProjetoPai = {0}
                AND lp.TipoLink = 'PP'
                AND p.DataExclusao IS NULL
            UNION
                SELECT NULL, NULL
            ) AS rs
            ORDER BY 2", Session["codProg"]);

            DataSet ds = cDados.getDataSet(comandoSQL1);

            ddlProjetos.TextField = "NomeProjeto";
            ddlProjetos.ValueField = "CodigoProjeto";
            ddlProjetos.DataSource = ds.Tables[0];
            ddlProjetos.DataBind();

            ListEditItem li_CodigoProjetoParcela = ddlProjetos.Items.FindByValue(CodigoProjetoParcela.ToString());
            ddlProjetos.SelectedItem = li_CodigoProjetoParcela;
        }


        string codigoProjetoSelecionado = CodigoProjetoParcela.ToString();

        if (codigoProjetoSelecionado.Trim() != "")
        {
            setDdlPacoteDaEAP(CodigoTarefaPacoteEAP, codigoProjetoSelecionado);
        }
        else
        {
            ddlPacoteDaEAP.Text = "";
            ddlPacoteDaEAP.Value = null;
        }


        string codigoTarefaSelecionada = CodigoTarefaPacoteEAP.ToString();
        string codigoCronogramaSelecionado = getCodigoCronogramaSelecionadoNoCombo();
        if (codigoTarefaSelecionada != "" && codigoCronogramaSelecionado != "")
        {
            setDdlRecurso(CodigoRecursoProjeto, codigoTarefaSelecionada, codigoCronogramaSelecionado);
        }



    }

    private void setDdlRecurso(object CodigoRecursoProjeto, string codigoTarefaSelecionada, string codigoCronogramaSelecionado)
    {
        string comandoSQL2 = string.Format(@"
        BEGIN
            DECLARE @l_CodigoCronogramaProjeto			Varchar(64),
                    @l_CodigoTarefa						Int
          
            SET @l_CodigoCronogramaProjeto = '{0}' 
            SET @l_CodigoTarefa = {1}
  
            SELECT DISTINCT 
                   rcp.CodigoRecursoProjeto,
                   rcp.NomeRecurso
              FROM RecursoCronogramaProjeto AS rcp
              INNER JOIN AtribuicaoRecursoTarefa AS art ON (rcp.CodigoCronogramaProjeto = art.CodigoCronogramaProjeto
                                                            AND rcp.CodigoRecursoProjeto = art.CodigoRecursoProjeto) 
              INNER JOIN TarefaCronogramaProjeto AS tcp ON (tcp.CodigoCronogramaProjeto = art.CodigoCronogramaProjeto
                                                            AND tcp.CodigoTarefa = art.CodigoTarefa
                                                            AND tcp.DataExclusao IS NULL)                                        
              WHERE rcp.CodigoCronogramaProjeto = @l_CodigoCronogramaProjeto
              AND rcp.CodigoTipoRecurso <> 1 -- Não pode ser pessoa     
              AND EXISTS(SELECT 1
                           FROM TarefaCronogramaProjeto AS tcpPai
                           WHERE tcpPai.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto
                             AND tcp.edt LIKE tcpPai.edt + '%'
                             AND tcpPai.DataExclusao IS NULL
                             AND tcpPai.CodigoTarefa = @l_CodigoTarefa)
             UNION select    -1 as CodigoRecursoProjeto,
                  '' as NomeRecurso
             ORDER BY 2
        END", codigoCronogramaSelecionado, codigoTarefaSelecionada);
        DataSet ds2 = cDados.getDataSet(comandoSQL2);
        if (cDados.DataSetOk(ds2) && cDados.DataTableOk(ds2.Tables[0]))
        {
            ddlRecurso.ValueField = "CodigoRecursoProjeto";
            ddlRecurso.TextField = "NomeRecurso";
            ddlRecurso.DataSource = ds2;
            ddlRecurso.DataBind();

            ListEditItem li_recursoSelecionado = ddlRecurso.Items.FindByValue(CodigoRecursoProjeto.ToString());
            ddlRecurso.SelectedItem = li_recursoSelecionado;
        }
    }

    private void setDdlPacoteDaEAP(object CodigoTarefaPacoteEAP, string codigoProjetoSelecionado)
    {
        string comandoSQL = string.Format(@"
        BEGIN
           DECLARE @l_CodigoProjetoParam	Int
           SET @l_CodigoProjetoParam = {0}
  
           SELECT tcp.CodigoCronogramaProjeto,
				  tcp.CodigoTarefa,
				  tcp.edt AS Pacote,
				  tcp.NomeTarefa AS NomePacote
		     FROM Projeto AS p 
             INNER JOIN CronogramaProjeto AS cp ON (cp.CodigoProjeto = p.CodigoProjeto
													AND cp.DataExclusao IS NULL) 
             INNER JOIN TarefaCronogramaProjeto AS tcp ON (tcp.CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto
																				AND tcp.DataExclusao IS NULL
																				AND tcp.SequenciaTarefaCronograma > 0
																				AND tcp.IndicaTarefaResumo = 'S' AND tcp.Nivel = 2) 
             WHERE p.CodigoProjeto = @l_CodigoProjetoParam																				
	         ORDER BY tcp.edt, tcp.NomeTarefa 
        END", codigoProjetoSelecionado);
        DataSet ds1 = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {

            ddlPacoteDaEAP.ValueField = "CodigoTarefa";
            ddlPacoteDaEAP.TextField = "NomePacote";
            ddlPacoteDaEAP.DataSource = ds1;
            ddlPacoteDaEAP.DataBind();

            ListEditItem li_PacoteSelecionado = ddlPacoteDaEAP.Items.FindByValue(CodigoTarefaPacoteEAP.ToString());
            ddlPacoteDaEAP.SelectedItem = li_PacoteSelecionado;
        }
    }

    private void desabilitaHabilitaComponentes()
    {
        /* se nao for somente leitura e se pode editar = sim*/


        spNumeroAditivo.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        spNumeroParcela.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        spValorPrevisto.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        dtVencimento.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        spValorPago.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        dtPagamento.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        txtNotaFiscal.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        spRetencao.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        spMultas.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        ddlContaContabil.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        ddlProjetos.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        ddlPacoteDaEAP.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        ddlRecurso.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        memoHistorico.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        dtEmissao.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");
        //estes dois ficam sempre desabilitados em caso de edição de registros
        //e como esta função só é chamada quando edita os componentes então pode ser desabilitado incondicionalmente aqui. 
        spNumeroAditivo.ClientEnabled = false;
        spNumeroParcela.ClientEnabled = false;
        btnSalvar.ClientVisible = (podeEditar != false) && (somenteLeitura != "S");
        ddlVersaoLB.ClientEnabled = (podeEditar != false) && (somenteLeitura != "S");

    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_sucesso"] = "";
        ((ASPxCallback)source).JSProperties["cp_erro"] = "";
        ((ASPxCallback)source).JSProperties["cp_aviso"] = "";

        string numeroParcela = spNumeroParcela.Value.ToString();
        string numeroAditivoContrato = spNumeroAditivo.Value.ToString();
        string valorPrevisto = spValorPrevisto.Value.ToString().Replace(',', '.');
        string dataVencimento = dtVencimento.Value != null ? dtVencimento.Value.ToString() : "";
        string dataEmissao = dtEmissao.Value != null ? dtEmissao.Value.ToString() : "";
        string dataPagamento = dtPagamento.Value != null ? dtPagamento.Value.ToString() : "";
        string valorPago = spValorPago.Value != null ? spValorPago.Value.ToString().Replace(',', '.') : "";
        string historicoParcela = memoHistorico.Text;

        string strVencto = dataVencimento == "" ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dataVencimento);
        string strDataEmissao = dataEmissao == "" ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dataEmissao);
        string strPagto = dataPagamento == "" ? "NULL" : string.Format("CONVERT(DATETIME, '{0:dd/MM/yyyy}', 103)", dataPagamento);
        


        //Customizações 09/2012
        string numeroNF = !string.IsNullOrEmpty(txtNotaFiscal.Text) ? string.Format("'{0}'", txtNotaFiscal.Text) : "NULL";
        string valorretencao = spRetencao.Value != null ? spRetencao.Value.ToString().Replace(',', '.') : "";
        string valorMulta = spMultas.Value != null ? spMultas.Value.ToString().Replace(',', '.') : "";
        string codigoConta = ddlContaContabil.Value != null ? ddlContaContabil.Value.ToString().Replace(',', '.') : "";

        //Bug 580: [SENAR] - [P649515] - Erro ao solicitar alteração do Valor previsto na aba "Financeiro" dos Contratos
        int _codigoProjetoParcela = -1;
        string codigoProjetoParcela = (int.TryParse(ddlProjetos.Value != null ? ddlProjetos.Value.ToString() : "", out _codigoProjetoParcela) == true ? ddlProjetos.Value.ToString() : "");


        //campos adicionados dia 2 de junho de 2016
        //CodigoCronogramaProjetoPacoteEAP, CodigoTarefaPacoteEAP e CodigoRecursoProjeto
        string CodigoCronogramaProjetoPacoteEAP = getValCombo_CodigoCronogramaProjetoPacoteEAP();
        string CodigoTarefaPacoteEAP = getValCombo_CodigoTarefaPacoteEAP();
        string CodigoRecursoProjeto = getValCombo_CodigoRecursoProjeto();
        if (geraLancamentoFinanceiro == false)
        {
            codigoConta = "";
        }
        string mensagemErro = "";

        if (NumeroParcela == -1)
        {
            //incluir nova parcela
            string mensagemPodeIncluirNovaParcela = getPodeIncluirNovaParcela(numeroParcela, numeroAditivoContrato);
            if (!string.IsNullOrEmpty(mensagemPodeIncluirNovaParcela))
            {
                ((ASPxCallback)source).JSProperties["cp_aviso"] = mensagemPodeIncluirNovaParcela;
            }
            else
            {
                bool retorno = cDados.incluiParcelaContratoAquisicoes(codigoContrato, numeroParcela, valorPrevisto, strVencto, strPagto, valorPago,
                historicoParcela, numeroAditivoContrato, codigoUsuarioResponsavel, numeroNF, valorretencao, valorMulta, codigoConta, codigoProjetoParcela, CodigoCronogramaProjetoPacoteEAP, CodigoTarefaPacoteEAP, CodigoRecursoProjeto, strDataEmissao, ref mensagemErro);
                if (mensagemErro == "")
                {
                    ((ASPxCallback)source).JSProperties["cp_sucesso"] = "Parcela incluída com sucesso!";
                }
                else
                {
                    ((ASPxCallback)source).JSProperties["cp_erro"] = mensagemErro;
                }
            }
        }
        else
        {
            //adicionar mais os seguintes campos:

            bool retorno = cDados.atualizaParcelaContratoAquisicoes(codigoContrato, numeroParcela, valorPrevisto, strVencto, strPagto, valorPago,
                historicoParcela, numeroAditivoContrato, codigoUsuarioResponsavel, numeroNF, valorretencao, valorMulta, codigoConta, codigoProjetoParcela, CodigoCronogramaProjetoPacoteEAP, CodigoTarefaPacoteEAP, CodigoRecursoProjeto, strDataEmissao, ref mensagemErro);
            if (mensagemErro == "")
            {
                ((ASPxCallback)source).JSProperties["cp_sucesso"] = "Parcela atualizada com sucesso!";
            }
            else
            {
                ((ASPxCallback)source).JSProperties["cp_erro"] = mensagemErro;
            }
        }
    }

    private bool getAtualizaRealizadoConformePrevisto()
    {
        bool atualizaRealizadoConformePrevisto = false;
        DataSet dsParametro = cDados.getParametrosSistema("AtualizaRealizadoConformePrevisto");
        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            atualizaRealizadoConformePrevisto = dsParametro.Tables[0].Rows[0]["AtualizaRealizadoConformePrevisto"].ToString().Trim() == "S";
        }
        return atualizaRealizadoConformePrevisto;
    }

    private string getValCombo_CodigoRecursoProjeto()
    {
        string retorno = "NULL";
        if (ddlRecurso.IsVisible() == true)
        {
            retorno = ddlRecurso.Value == null ? "NULL" : ddlRecurso.Value.ToString();
        }
        return retorno;
    }

    private string getValCombo_CodigoTarefaPacoteEAP()
    {
        string retorno = "NULL";
        if (ddlPacoteDaEAP.IsVisible() == true)
        {
            retorno = ddlPacoteDaEAP.Value == null ? "NULL" : ddlPacoteDaEAP.Value.ToString();
        }
        return retorno;
    }

    private string getValCombo_CodigoCronogramaProjetoPacoteEAP()
    {
        string retorno = "NULL";
        try
        {
            if (divPacoteEAP.Visible == true)
            {
                if(ddlVersaoLB.Value != null)
                {
                    retorno = "'" + ddlVersaoLB.Value.ToString() + "'";
                }                
            }
        }
        catch (Exception ex)
        {
           
        }
        
        return retorno;
    }

    private string getPodeIncluirNovaParcela(string numeroParcela, string numeroAditivoContrato)
    {
        /*primeiro tem que verificar se a parcela e o aditivo ja existem */
        string retorno = "";
        string comandosql = string.Format(@"SELECT 1
          FROM [ParcelaContrato] pc
    INNER JOIN [PlanoContasFluxoCaixa] pcfc on (pcfc.CodigoConta = pc.CodigoConta)
         WHERE pc.CodigoContrato = {0} 
           and pc.NumeroAditivoContrato = {1} 
           and pc.NumeroParcela = {2}  AND pc.[DataExclusao] IS NULL", codigoContrato, numeroAditivoContrato, numeroParcela);
        DataSet ds = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = "Erro ao inserir o registro, já existe um lançamento com mesmo número de aditivo e parcela de contrato";
        }
        return retorno;
    }

    protected void pnRecurso_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigoTarefaSelecionada = e.Parameter;
        string codigoCronogramaSelecionado = ddlVersaoLB.Value.ToString();// getCodigoCronogramaSelecionadoNoCombo();

        string comandoSQL = string.Format(@"
        BEGIN
            DECLARE @l_CodigoCronogramaProjeto			Varchar(64),
                    @l_CodigoTarefa						Int
          
            SET @l_CodigoCronogramaProjeto = '{0}' 
            SET @l_CodigoTarefa = {1}
  
            SELECT DISTINCT 
                   rcp.CodigoRecursoProjeto,
                   rcp.NomeRecurso
              FROM RecursoCronogramaProjeto AS rcp
              INNER JOIN AtribuicaoRecursoTarefa AS art ON (rcp.CodigoCronogramaProjeto = art.CodigoCronogramaProjeto
                                                            AND rcp.CodigoRecursoProjeto = art.CodigoRecursoProjeto) 
              INNER JOIN TarefaCronogramaProjeto AS tcp ON (tcp.CodigoCronogramaProjeto = art.CodigoCronogramaProjeto
                                                            AND tcp.CodigoTarefa = art.CodigoTarefa
                                                            AND tcp.DataExclusao IS NULL)                                        
              WHERE rcp.CodigoCronogramaProjeto = @l_CodigoCronogramaProjeto
              AND rcp.CodigoTipoRecurso <> 1 -- Não pode ser pessoa     
              AND EXISTS(SELECT 1
                           FROM TarefaCronogramaProjeto AS tcpPai
                           WHERE tcpPai.CodigoCronogramaProjeto = tcp.CodigoCronogramaProjeto
                             AND tcp.edt LIKE tcpPai.edt + '%'
                             AND tcpPai.DataExclusao IS NULL
                             AND tcpPai.CodigoTarefa = @l_CodigoTarefa)
             ORDER BY rcp.NomeRecurso
        END", codigoCronogramaSelecionado, codigoTarefaSelecionada);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlRecurso.ValueField = "CodigoRecursoProjeto";
            ddlRecurso.TextField = "NomeRecurso";
            ddlRecurso.DataSource = ds;
            ddlRecurso.DataBind();
        }
    }

    private string getCodigoCronogramaSelecionadoNoCombo()
    {
        int codigoSelecionado = -1;
        string retornoCodigoCronograma = "NULL";
        int _codigoProjetoParcela = -1;
        //Bug 580: [SENAR] - [P649515] - Erro ao solicitar alteração do Valor previsto na aba "Financeiro" dos Contratos
        string codigoProjetoSelecionado = (int.TryParse(ddlProjetos.Value != null ? ddlProjetos.Value.ToString() : "", out _codigoProjetoParcela) == true ? ddlProjetos.Value.ToString() : "");
        bool retorno = int.TryParse(codigoProjetoSelecionado, out codigoSelecionado);
        if (retorno)
        {
            var percentualConcluido = (int?)(null);
            var data = (DateTime?)(null);
            DataSet ds = cDados.getCronogramaGantt(codigoSelecionado, "-1", 1, true, false, false, percentualConcluido, data);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                retornoCodigoCronograma = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
            }
        }
        return retornoCodigoCronograma;
    }

    protected void pnPacote_Callback(object sender, CallbackEventArgsBase e)
    {
        // var strParametros = ddlProjetos.GetValue() + '|' + s.GetValue();
        string codigoProjetoSelecionadoECodigoCronograma = e.Parameter;
        string[] codigoProjetoSelecionadoECodigoCronograma_S = e.Parameter.Split('|');


        int _codigoProjetoSelecionado = 0;
        string _codigoCronograma = codigoProjetoSelecionadoECodigoCronograma_S[1];
        if(int.TryParse(codigoProjetoSelecionadoECodigoCronograma_S[0], out _codigoProjetoSelecionado) == false)
        {
            return;
        }


        string selectCommand = string.Format(
         @"BEGIN
          SELECT -1 AS CodigoTarefa, -1 AS SequenciaTarefaCronograma, '' AS NomePacote    
         union
         SELECT  tc.[CodigoTarefa]
				, tc.[SequenciaTarefaCronograma]
				, CONVERT(Varchar,tc.SequenciaTarefaCronograma) + ' - ' + tc.[NomeTarefa]	as NomePacote	
			FROM 
					[TarefaCronogramaProjeto]					AS [tc]
					
						INNER JOIN [CronogramaProjeto]	AS [cp]		ON 
							(cp.[CodigoCronogramaProjeto]	= tc.[CodigoCronogramaProjeto] and  tc.[CodigoCronogramaProjeto] = '{0}')			
			WHERE  cp.[DataExclusao]					IS NULL
				AND tc.[DataExclusao]					IS NULL				
        AND tc.[IndicaTarefaResumo] = 'S' AND tc.Nivel = 2           
     ORDER BY 2
END",  _codigoCronograma);

        DataSet ds = cDados.getDataSet(selectCommand);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            ddlPacoteDaEAP.ValueField = "CodigoTarefa";
            ddlPacoteDaEAP.TextField = "NomePacote";
            ddlPacoteDaEAP.DataSource = ds;
            ddlPacoteDaEAP.DataBind();
        }
        else
        {
            ddlPacoteDaEAP.Value = null;
            ddlPacoteDaEAP.Text = "";
        }

    }

    protected void cbAvisos_Callback(object sender, CallbackEventArgsBase e)
    {
        setaMensagemDeAviso();
    }

    protected void pnSaldo_Callback(object sender, CallbackEventArgsBase e)
    {
        decimal parametro = 0;
        decimal saldoAtual = 0;
        bool retorno = decimal.TryParse(e.Parameter, out parametro);
        retorno = decimal.TryParse(spSaldoContrato.Value != null ? spSaldoContrato.Value.ToString() : "0", out saldoAtual);

        spSaldoContrato.Value = saldoAtual - parametro;
    }

    protected void pnVersaoLB_Callback(object sender, CallbackEventArgsBase e)
    {
        string codigoProjeto = e.Parameter == "null" ? "-1" : e.Parameter;
        carregaComboLinhaBase(int.Parse(codigoProjeto));        
    }


    private void carregaComboLinhaBase(int codigoProjeto)
    {
        DataSet dsLinhaBase = cDados.getDataSet(string.Format("SELECT * FROM f_GetVersoesCronogramaProjeto({0})", codigoProjeto));

        if (cDados.DataSetOk(dsLinhaBase))
        {
            ddlVersaoLB.DataSource = dsLinhaBase;
            ddlVersaoLB.TextField = "DescricaoVersao";
            ddlVersaoLB.ValueField = "CodigoCronogramaProjeto";
            ddlVersaoLB.DataBind();
            if(ddlVersaoLB.Items.Count == 0)
            {
                ddlVersaoLB.Text = "";
            }
        }
    }
}