using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_DadosRelatorioAcompanhamento : System.Web.UI.Page
{
    #region Fields

    dados cDados;
    int codigoRelatorio;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    bool indicaNovoRelatorio;
    bool readOnly;

    protected int altura;

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        codigoRelatorio = int.Parse(Request.QueryString["CR"]);
        indicaNovoRelatorio = Request.QueryString["novo"] == "S";
        if (!string.IsNullOrWhiteSpace(Request.QueryString["RO"]))
            readOnly = Request.QueryString["RO"].ToUpper() == "S";
        pnlConteudo.Enabled = !readOnly;
        btnSalvar.ClientEnabled = !readOnly;
        altura = int.Parse(Request.QueryString["altura"]);
        DefineAlturasPagina();

        //Dados do usuario logado e da entidade logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok
        //Define variável de sessão que será utilizado na manipulação de dados da tela
        Session["CodigoUsuario"] = codigoUsuarioResponsavel;
        Session["CodigoEntidade"] = codigoEntidadeUsuarioResponsavel;
        //
        if (indicaNovoRelatorio)
            hfGeral["Situacao"] = string.Format("pendente;{0}", codigoRelatorio);

        bool retornaAltura = int.TryParse(Request.QueryString["altura"], out altura);

        string url = string.Format("../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=PR&ID={0}&RO={1}&ALT={2}", codigoRelatorio, readOnly ? "S" : "N", (altura - 100));
        frmAnexos.Attributes.Add("src", url);
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this);
        DefineStringConexao();
        if (!IsPostBack)
            CarregaDados();
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string resultado = string.Empty;
        try
        {
            DefineParametros();

            int updatedRowsCount = sdsRelatorio.Update();
        }
        catch (Exception ex)
        {
            resultado = ex.Message;
        }
        e.Result = resultado;
    }

    protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        if (readOnly)
        {
            e.Enabled = false;
            switch (e.ButtonType)
            {
                case DevExpress.Web.ColumnCommandButtonType.Edit:
                    e.Image.Url = @"~/imagens/botoes/editarRegDes.PNG";
                    break;
                case DevExpress.Web.ColumnCommandButtonType.Delete:
                    e.Image.Url = @"~/imagens/botoes/excluirRegDes.PNG";
                    break;
            }
        }
    }

    #endregion

    #region Methods

    private void DefineStringConexao()
    {
        string connectionString = cDados.classeDados.getStringConexao();
        sdsAquisicoes.ConnectionString =
            sdsAquisicoesPendentes.ConnectionString =
            sdsDestinatarios.ConnectionString =
            sdsDesvios.ConnectionString =
            sdsEntregas.ConnectionString =
            sdsIndicadores.ConnectionString =
            sdsRelatorio.ConnectionString =
            sdsRiscos.ConnectionString =
            sdsUnidade.ConnectionString = connectionString;
    }

    private void CarregaDados()
    {
        DataView dados =
            (DataView)sdsRelatorio.Select(new DataSourceSelectArguments());
        DataRowView row = dados[0];
        hfGeral["CodigoProjeto"] = row["CodigoProjeto"];
        txtProjeto.Value = row["NomeProjeto"];
        txtGerente.Value = row["NomeGerente"];
        txtDataElaboracao.Value = row["DataElaboracao"];
        txtResponsavelElaboracao.Value = row["NomeElaborador"];
        txtUnidade.Value = row["SiglaUnidadeNegocio"];
        txtInicioPlanejado.Value = row["DataInicioPlanejada"];
        txtInicioReal.Value = row["DataInicioReal"];
        txtTerminoPlanejado.Value = row["TerminoPlanejado"];
        txtTerminoProgresso.Value = row["ProgressoFisico"];
        txtProgressoFisico.Value = row["ProgressoFisicoAnterior"];
        txtTerminoTendencia.Value = row["TendenciaTermino"];
        txtTerminoStatus.Value = row["StatusTermino"];
        txtTerminoVariacao.Value = row["VariacaoTermino"];
        txtPFPlanejado.Value = row["PontoFuncaoPlanejado"];
        txtPFProgresso.Value = row["PontoFuncaoProgresso"];
        txtPFTendencia.Value = row["PontoFuncaoTendencia"];
        txtPFStatus.Value = row["PontoFuncaoStatus"];
        txtPFVariacao.Value = row["VariacaoPontoFuncao"];
        txtInvestimentoPlanejado.Value = row["InvestimentoPlanejado"];
        txtInvestimentoProgresso.Value = row["InvestimentoProgresso"];
        txtInvestimentoTendencia.Value = row["InvestimentoTendencia"];
        txtInvestimentoStatus.Value = row["InvestimentoStatus"];
        txtInvestimentoVariacao.Value = row["VaricaoInvestimento"];
        txtCusteioPlanejado.Value = row["CusteioPlanejado"];
        txtCusteioProgresso.Value = row["CusteioProgresso"];
        txtCusteioTendencia.Value = row["CusteioTendencia"];
        txtCusteioStatus.Value = row["CusteioStatus"];
        txtCusteioVariacao.Value = row["VariacaoCusteio"];
        txtComentarioPrazo.Value = row["ComentarioPrazo"];
        txtComentarioMotivoAtraso.Value = row["ComentarioAtraso"];
        txtComentarioEscopo.Value = row["ComentarioEscopo"];
        txtComentarioCustos.Value = row["ComentarioCusto"];
        txtComentarioRH.Value = row["ComentarioRH"];
        txtComentarioComunicacoes.Value = row["ComentarioComunicacoes"];
        //txtComentarioAquisicoes.Value = row["ComentarioAquisicoesRealizar"];
        txtComentarioGarantiaQualidade.Value = row["ComentarioGarantiaQualidade"];
        txtComentarioGerenciaConfiguracoes.Value = row["ComentarioGerenciaConfiguracao"];
        rblAreaDefinida.Value = row["IndicaDefinicaoAreaSustentacao"];
        txtJustificativaSustentacao.Value = row["JustificativaAreaSustentacao"];
        rblDocumentoEmitido.Value = row["IndicaEmissaoDocumentoRepasse"];
        seCronograma.Value = row["QuantMudancaCronograma"];
        seCustos.Value = row["QuantMudancaCusto"];
        seEscopo.Value = row["QuantMudancaEscopo"];
        seParalizacao.Value = row["QuantParalisacao"];
        seQuantReativacao.Value = row["QuantReativacao"];
        cmbAreaSustentacao.Value = row["CodigoUnidadeSustentacao"];
    }

    private void DefineParametros()
    {
        ParameterCollection parameters = sdsRelatorio.UpdateParameters;

        #region Parameters

        parameters.Clear();
        parameters.Add(new QueryStringParameter("CodigoRelatorio", "CR"));
        parameters.Add(new SessionParameter("CodigoUsuario", "CodigoUsuario"));
        parameters.Add("CodigoProjeto",
            (hfGeral["CodigoProjeto"] ?? string.Empty).ToString());
        parameters.Add("TerminoPlanejado",
            (txtTerminoPlanejado.Value ?? string.Empty).ToString());
        parameters.Add("ProgressoFisico",
            (txtTerminoProgresso.Value ?? string.Empty).ToString());
        parameters.Add("ProgressoFisicoAnterior",
            (txtProgressoFisico.Value ?? string.Empty).ToString());
        parameters.Add("TendenciaTermino",
            (txtTerminoTendencia.Value ?? string.Empty).ToString());
        parameters.Add("StatusTermino",
            (txtTerminoStatus.Value ?? string.Empty).ToString());
        parameters.Add("VariacaoTermino",
            (txtTerminoVariacao.Value ?? string.Empty).ToString());
        parameters.Add("PontoFuncaoPlanejado",
            (txtPFPlanejado.Value ?? string.Empty).ToString());
        parameters.Add("PontoFuncaoProgresso",
            (txtPFProgresso.Value ?? string.Empty).ToString());
        parameters.Add("PontoFuncaoTendencia",
            (txtPFTendencia.Value ?? string.Empty).ToString());
        parameters.Add("PontoFuncaoStatus",
            (txtPFStatus.Value ?? string.Empty).ToString());
        parameters.Add("VariacaoPontoFuncao",
            (txtPFVariacao.Value ?? string.Empty).ToString());
        parameters.Add("InvestimentoPlanejado",
            (txtInvestimentoPlanejado.Value ?? string.Empty).ToString());
        parameters.Add("InvestimentoProgresso",
            (txtInvestimentoProgresso.Value ?? string.Empty).ToString());
        parameters.Add("InvestimentoTendencia",
            (txtInvestimentoTendencia.Value ?? string.Empty).ToString());
        parameters.Add("InvestimentoStatus",
            (txtInvestimentoStatus.Value ?? string.Empty).ToString());
        parameters.Add("VaricaoInvestimento",
            (txtInvestimentoVariacao.Value ?? string.Empty).ToString());
        parameters.Add("CusteioPlanejado",
            (txtCusteioPlanejado.Value ?? string.Empty).ToString());
        parameters.Add("CusteioProgresso",
            (txtCusteioProgresso.Value ?? string.Empty).ToString());
        parameters.Add("CusteioTendencia",
            (txtCusteioTendencia.Value ?? string.Empty).ToString());
        parameters.Add("CusteioStatus",
            (txtCusteioStatus.Value ?? string.Empty).ToString());
        parameters.Add("VariacaoCusteio",
            (txtCusteioVariacao.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioPrazo",
            (txtComentarioPrazo.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioAtraso",
            (txtComentarioMotivoAtraso.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioEscopo",
            (txtComentarioEscopo.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioCusto",
            (txtComentarioCustos.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioRH",
            (txtComentarioRH.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioComunicacoes",
            (txtComentarioComunicacoes.Value ?? string.Empty).ToString());
        //parameters.Add("ComentarioAquisicoesRealizar",
        //    (txtComentarioAquisicoes.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioGarantiaQualidade",
            (txtComentarioGarantiaQualidade.Value ?? string.Empty).ToString());
        parameters.Add("ComentarioGerenciaConfiguracao",
            (txtComentarioGerenciaConfiguracoes.Value ?? string.Empty).ToString());
        parameters.Add("IndicaDefinicaoAreaSustentacao",
            (rblAreaDefinida.Value ?? string.Empty).ToString());
        parameters.Add("JustificativaAreaSustentacao",
            (txtJustificativaSustentacao.Value ?? string.Empty).ToString());
        parameters.Add("IndicaEmissaoDocumentoRepasse",
            (rblDocumentoEmitido.Value ?? string.Empty).ToString());
        parameters.Add("QuantMudancaCronograma",
            (seCronograma.Value ?? string.Empty).ToString());
        parameters.Add("QuantMudancaCusto",
            (seCustos.Value ?? string.Empty).ToString());
        parameters.Add("QuantMudancaEscopo",
            (seEscopo.Value ?? string.Empty).ToString());
        parameters.Add("QuantParalisacao",
            (seParalizacao.Value ?? string.Empty).ToString());
        parameters.Add("QuantReativacao",
            (seQuantReativacao.Value ?? string.Empty).ToString());
        parameters.Add("CodigoUnidadeSustentacao",
            (cmbAreaSustentacao.Value ?? string.Empty).ToString());

        #endregion
    }

    private void DefineAlturasPagina()
    {
        string alturaDivPrincipal = string.Format("{0}px", altura - 155);
        string alturaDivAnexos = string.Format("{0}px", altura - 120);
        string alturaFrmAnexos = string.Format("{0}px", altura - 120);
        divPrincipal.Style.Add("max-height", alturaDivPrincipal);
        divPrincipal.Style.Add("height", alturaDivPrincipal);
        frmAnexos.Style.Add("height", alturaFrmAnexos);
    }

    protected string ObtemBotaoInclusaoRegistro(string nomeGrid, string assuntoGrid)
    {
        string htmlBotaoDesabilitado = @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Desabilitado para edição"" style=""cursor: default;""/>";
        string htmlBotaoHabilitado = string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" alt=""{1}"" onclick=""{0}.AddNewRow();"" title=""{1}"" style=""cursor: pointer;""/>", nomeGrid, "Incluir " + assuntoGrid);

        string strRetorno = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>"
            , (!readOnly) ? htmlBotaoHabilitado : htmlBotaoDesabilitado);

        return strRetorno;
    }

    #endregion

}