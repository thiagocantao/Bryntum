using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections.Specialized;

public partial class _Projetos_Administracao_LancamentosFinanceirosConvenio : BasePageBrisk
{
    bool utilizaConvenio;
    int codigoLancamentoFinanceiro;
    public bool ExistemParcelasRepetidas;
    /*
     * tipos possíveis:
     * - PAR -> parcela
     * - LEV -> lançamento de emprenho vinculado à parcela
     * - LEN -> lançamento de emprenho não vinculado à parcela
     * - LPV -> lançamento de pagamento vinculado à parcela
     * - LPN -> lançamento de pagamento não vinculado à parcela
     */
    string tipo;

    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();

        codigoLancamentoFinanceiro = int.Parse(Request.QueryString["clf"]);
        Session["ce"] = UsuarioLogado.CodigoEntidade;
        Session["cp"] = ObtemCodigoProjeto();
        tipo = Request.QueryString["tipo"];

        pageControl.TabPages[1].ClientVisible = !(codigoLancamentoFinanceiro == -1);
        pageControl.JSProperties["cp_CodigoObjeto"] = codigoLancamentoFinanceiro;
        pageControl.JSProperties["cp_Altura"] = (TelaAltura - 300).ToString() + "px";
        pageControl.JSProperties["cp_Largura"] = (TelaLargura - 300).ToString() + "px";

        SetDataSourcesConnectionString();
    }

    private int ObtemCodigoProjeto()
    {
        int codigoProjeto;
        if (!int.TryParse(Request.QueryString["cp"], out codigoProjeto))
        {
            string comandoSql;
            if (codigoLancamentoFinanceiro > 0)
                comandoSql = string.Format("SELECT CodigoProjeto FROM LancamentoFinanceiro WHERE CodigoLancamentoFinanceiro = {0}", codigoLancamentoFinanceiro);
            else if (!string.IsNullOrEmpty(Request.QueryString["cc"]))
                comandoSql = string.Format("SELECT c.CodigoProjeto FROM Contrato AS c Where c.CodigoContrato = {0}", Request.QueryString["cc"]);
            else
                return -1;
            //throw new Exception("Não é possível obter o código do projeto");

            DataSet ds = CDados.getDataSet(comandoSql);
            DataRow dr = ds.Tables[0].Rows[0];
            if (dr.IsNull("CodigoProjeto"))
                return -1;

            codigoProjeto = dr.Field<int>("CodigoProjeto");
        }

        return codigoProjeto;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CDados.aplicaEstiloVisual(this.Page);

        VerificaUtilizaConvenio();
        ConfiguraCamposHabilitados();
        if (codigoLancamentoFinanceiro > 0)
        {
            seNumeroAditivo.ReadOnly = true;
            seNumeroParcela.ReadOnly = true;
        }
    }

    private void ConfiguraCamposHabilitados()
    {
        int codigoProjeto = ObtemCodigoProjeto();

        switch (tipo)
        {
            case "PAR":
                #region Contratos – Financeiro (Parcelas)
                ConfiguraCampoHabilidado("NumeroAditivo", true);
                ConfiguraCampoHabilidado("NumeroParcela", true);
                if (codigoProjeto > 0)
                    ConfiguraCampoHabilidado("CodigoParticipe");
                ConfiguraCampoHabilidado("ValorPrevisto", true);
                ConfiguraCampoHabilidado("DataVencimento", true);
                ConfiguraCampoHabilidado("PrevisaoPagamento");
                ConfiguraCampoHabilidado("ContaContabil");
                ConfiguraCampoHabilidado("ObservacaoEmpenho");
                ConfiguraCampoHabilidado("EmissaoDocFiscal");
                ConfiguraCampoHabilidado("NumeroDocFiscal");
                #endregion
                break;
            case "LEV":
                #region Lançamento Financeiro – Empenho Vinculado a Parcela de Contrato
                ConfiguraCampoHabilidado("ValorPrevisto", true);
                ConfiguraCampoHabilidado("DataVencimento", true);
                ConfiguraCampoHabilidado("PrevisaoPagamento");
                ConfiguraCampoHabilidado("ContaContabil");
                ConfiguraCampoHabilidado("ObservacaoEmpenho");
                ConfiguraCampoHabilidado("EmissaoDocFiscal");
                ConfiguraCampoHabilidado("NumeroDocFiscal");
                #endregion
                break;
            case "LEN":
                #region Lançamento Financeiro – Empenho Não Vinculado a Parcela de Contrato
                ConfiguraCampoHabilidado("IndicaDespesaReceita");
                ConfiguraCampoHabilidado("CodigoPessoaEmitente");
                if (codigoProjeto > 0)
                    ConfiguraCampoHabilidado("CodigoParticipe");
                ConfiguraCampoHabilidado("ValorPrevisto", true);
                ConfiguraCampoHabilidado("DataVencimento", true);
                ConfiguraCampoHabilidado("PrevisaoPagamento");
                ConfiguraCampoHabilidado("ContaContabil");
                ConfiguraCampoHabilidado("ObservacaoEmpenho");
                ConfiguraCampoHabilidado("EmissaoDocFiscal");
                ConfiguraCampoHabilidado("NumeroDocFiscal");
                #endregion
                break;
            case "LPV":
                #region Lançamento Financeiro – Pagamento Vinculado a Parcela de Contrato
                ConfiguraCampoHabilidado("EmissaoDocFiscal");
                ConfiguraCampoHabilidado("NumeroDocFiscal");
                ConfiguraCampoHabilidado("DataPagamento", true);
                ConfiguraCampoHabilidado("ValorPago", true);
                ConfiguraCampoHabilidado("ValorRetencoes");
                ConfiguraCampoHabilidado("ObservacaoPagamento");
                #endregion
                break;
            case "LPN":
                #region Lançamento Financeiro – Pagamento Empenhado Não Vinculado a Parcela de Contrato
                ConfiguraCampoHabilidado("IndicaDespesaReceita");
                ConfiguraCampoHabilidado("CodigoPessoaEmitente");
                if (codigoProjeto > 0)
                    ConfiguraCampoHabilidado("CodigoParticipe");
                ConfiguraCampoHabilidado("ValorPrevisto", true);
                ConfiguraCampoHabilidado("DataVencimento", true);
                ConfiguraCampoHabilidado("PrevisaoPagamento");
                ConfiguraCampoHabilidado("ContaContabil");
                ConfiguraCampoHabilidado("ObservacaoEmpenho");
                ConfiguraCampoHabilidado("EmissaoDocFiscal");
                ConfiguraCampoHabilidado("NumeroDocFiscal");
                ConfiguraCampoHabilidado("DataPagamento", true);
                ConfiguraCampoHabilidado("ValorPago", true);
                ConfiguraCampoHabilidado("ValorRetencoes");
                ConfiguraCampoHabilidado("ObservacaoPagamento");
                #endregion
                break;
            default:
                btnSalvar.ClientVisible = false;
                break;
        }
    }

    private void ConfiguraCampoHabilidado(string nomeCampo, bool requerido = false)
    {
        LayoutItem layoutItem = formLayout.FindItemByFieldName(nomeCampo);
        ASPxEdit editor = (ASPxEdit)layoutItem.GetNestedControl();

        editor.ClientEnabled = true;
        editor.ValidationSettings.Display = Display.Dynamic;
        editor.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
        editor.ValidationSettings.RequiredField.IsRequired = requerido;
        editor.ValidationSettings.RequiredField.ErrorText = "Campo obrigatório";
        layoutItem.RequiredMarkDisplayMode = requerido ?
            FieldRequiredMarkMode.Required :
            FieldRequiredMarkMode.Hidden;
    }

    private void VerificaUtilizaConvenio()
    {
        var nomeParametro = "utilizaConvenio";
        var ds = CDados.getParametrosSistema(nomeParametro);
        var dr = ds.Tables[0].Rows[0];
        if (dr.IsNull(nomeParametro))
            utilizaConvenio = false;
        else
            utilizaConvenio = dr.Field<string>(nomeParametro).ToUpper() == "S";
    }

    private void SetDataSourcesConnectionString()
    {
        dataSourceContaContabil.ConnectionString =
            dataSourceParticipe.ConnectionString =
            dataSourceEmitente.ConnectionString =
            dataSource.ConnectionString =
            CDados.ConnectionString;
    }

    private bool SalvarAlteracoes(out string mensagemFalhaAoSalvar)
    {
        mensagemFalhaAoSalvar = string.Empty;
        if (!ASPxEdit.ValidateEditorsInContainer(formLayout.NamingContainer, false))
            return false;

        try
        {
            DefineConfiguracoesComandoAtualizacao();
            if (tipo != "LPN" && tipo != "LEN" && verificaSeTemParcelasRepetidas() == true)
            {
                throw new Exception("Já existem parcelas com os números de aditivo e de parcela informados!");
            }
            dataSource.Update();
        }
        catch (Exception ex)
        {
            mensagemFalhaAoSalvar = ex.Message;
        }
        return string.IsNullOrEmpty(mensagemFalhaAoSalvar);
    }

    private bool verificaSeTemParcelasRepetidas()
    {
        bool retorno = false;
        if (codigoLancamentoFinanceiro < 0)
        {
            string comando = string.Format(@"SELECT 1 FROM ParcelaContrato
            WHERE CodigoContrato = {0} 
            AND NumeroAditivoContrato = {1} AND NumeroParcela = {2} AND [DataExclusao] IS NULL", Request.QueryString["cc"], seNumeroAditivo.Value, seNumeroParcela.Value);
            DataSet ds = CDados.getDataSet(comando);
            if(CDados.DataSetOk(ds) && CDados.DataTableOk(ds.Tables[0]))
            {
                retorno = true;
            }
        }
        return retorno;
    }

    private void DefineConfiguracoesComandoAtualizacao()
    {
        int codigoProjeto = ObtemCodigoProjeto();
        var comandoSql = new StringBuilder();
        var layoutItemsList = formLayout.Items.OfType<LayoutItem>()
            .Where(li => (li.GetNestedControl() as ASPxEdit).ClientEnabled).ToList();
        comandoSql.AppendFormat("EXEC [dbo].[{0}] ", ObtemNomeProcedimentoBancoAtualizacaoAlteracoes());
        comandoSql.AppendFormat("@in_CodigoUsuario = {0}, ", UsuarioLogado.Id);
        comandoSql.AppendFormat("@in_CodigoProjeto = {0}", codigoProjeto == -1 ? "NULL" : codigoProjeto.ToString());
        dataSource.UpdateParameters.Clear();
        if (tipo == "PAR")
            layoutItemsList.Add(formLayout.FindItemByFieldName("CodigoPessoaEmitente"));
        foreach (var layoutItem in layoutItemsList)
        {
            var nomeCampo = layoutItem.FieldName;
            var nomeParametroEntrada = string.Format("@in_{0}", nomeCampo);
            var identificacaoControle = layoutItem.GetNestedControl().UniqueID;
            var parametro = new ControlParameter(nomeCampo, identificacaoControle, "Value");
            comandoSql.AppendFormat(", {0} = @{1}", nomeParametroEntrada, nomeCampo);
            dataSource.UpdateParameters.Add(parametro);
        }
        dataSource.UpdateCommand = comandoSql.ToString();
    }

    private string ObtemNomeProcedimentoBancoAtualizacaoAlteracoes()
    {
        return tipo.EndsWith("N") ? "p_gestconv_salvaLancamentoFinanceiro" : "p_gestconv_salvaParcelaContrato";
    }

    private string ValidaCampos()
    {
        var mensagemValidacao = new StringBuilder();
        var requiredLayoutItems = formLayout.Items.OfType<LayoutItem>()
            .Where(li => li.RequiredMarkDisplayMode == FieldRequiredMarkMode.Required);
        foreach (var layoutItem in requiredLayoutItems)
        {
            string nomeCampo = layoutItem.FieldName;
            object valor = formLayout.GetNestedControlValueByFieldName(nomeCampo);
            if (valor == null)
            {
                mensagemValidacao.AppendFormat("O campo '{0}' é obrigatório.", layoutItem.Caption);
                mensagemValidacao.AppendLine();
            }
        }

        return mensagemValidacao.ToString();
    }

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        string mensagem;
        bool resultado = SalvarAlteracoes(out mensagem);
        if (resultado)
        {

        }
        else
        {
            e.Result = mensagem;
        }
    }

    protected void dateEdit_DataBound(object sender, EventArgs e)
    {
        if (tipo == "PAR")
        {
            var dateEdit = (ASPxDateEdit)sender;
            var dataMaxima = deTermino.Date;
            var dataMinima = deInicio.Date;
            dateEdit.MaxDate = dataMaxima;
            dateEdit.MinDate = dataMinima;
        }
    }
}