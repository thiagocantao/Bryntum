using System;
using System.Web;
using DevExpress.Web;
using System.Data;
using System.Collections.Specialized;

public partial class _Projetos_Administracao_frmAcessorioCalculoPagamentoContrato : System.Web.UI.Page
{
    #region Fields

    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public bool podeDefinir = true;

    int codigoContrato = -1;
    public string somenteLeitura = ""; 

    #endregion

    #region Event Handlers

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

        this.Title = cDados.getNomeSistema();

        string connectionString = cDados.classeDados.getStringConexao();
        sdsAcessoriosContrato.ConnectionString = connectionString;
        sdsOpcoesAcessorios.ConnectionString = connectionString;
        ConfiguraParametros();
    }

    private void ConfiguraParametros()
    {
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelAcessoriosContrato");
        string labelAcessoriosContrato = dsParametros.Tables[0].Rows[0]["labelAcessoriosContrato"] as string;
        gvDados.Columns["CodigoAcessorio"].Caption = labelAcessoriosContrato;
        gvDados.Columns["DescricaoAcessorio"].Caption = labelAcessoriosContrato;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        string IniciaisTipoAssociacao = "CT";
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            
        }

        cDados.aplicaEstiloVisual(Page);

        if (!string.IsNullOrEmpty(Request.QueryString["CC"]))
            codigoContrato = int.Parse(Request.QueryString["CC"].ToString());

        if (!string.IsNullOrEmpty(Request.QueryString["RO"]))
            somenteLeitura = Request.QueryString["RO"].ToString();

        if (!string.IsNullOrEmpty(Request.QueryString["ALT"]))
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString()) - 85;

        //somenteLeitura = "S";
        if (somenteLeitura == "S")
        {
            podeEditar = false;
            podeExcluir = false;
            podeIncluir = false;
            podeDefinir = false;
        }
        else
        {
            if (!IsPostBack)
            {
                cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_CnsAce");
            }
            podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                 codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_IncAce");
            podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_ExcAce");
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_AltAce");
            podeDefinir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                codigoContrato, "null", IniciaisTipoAssociacao, 0, "null", "CT_DefAce");
            //podeEditar = false;
            //podeExcluir = false;
            //podeIncluir = false;
            //podeDefinir = false;
        }

        gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    protected void gvDados_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        /*
        object acessorioBloqueado = gvDados.GetRowValues(
            e.VisibleIndex, "IndicaAcessorioBloqueado").ToString().ToUpper();
        bool bloqueado = acessorioBloqueado.Equals("S");
        switch (e.ButtonType)
        {
            case ColumnCommandButtonType.Edit:
                if (podeEditar && !bloqueado)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
                break;
            case ColumnCommandButtonType.Delete:
                if (podeExcluir && !bloqueado)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
                break;
        }
        */
    }

    protected void sdsOpcoesAcessorios_Selecting(object sender, System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs e)
    {
        if (!gvDados.IsNewRowEditing)
        {
            int rowIndex = gvDados.FocusedRowIndex;
            object codigoAcessorioContrato = gvDados.GetRowValues(
                rowIndex, "CodigoAcessorioContrato");
            object codigoAcessorio = gvDados.GetRowValues(
                rowIndex, "CodigoAcessorio");
            e.Command.CommandText = string.Format(@"
 SELECT CodigoAcessorio, 
        DescricaoAcessorio 
   FROM AcessorioCalculoPagamento 
  WHERE (CodigoAcessorio NOT IN (SELECT CodigoAcessorio 
                                   FROM AcessorioCalculoPagamentoContrato 
                                  WHERE (CodigoContrato = @CodigoContrato) 
                                    AND (DataTerminoVigencia IS NULL)
                                    AND (CodigoAcessorioContrato <> {0})))
     OR CodigoAcessorio = {1}"
                , codigoAcessorioContrato, codigoAcessorio);
        }
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string[] parametros = e.Parameter.Split(';');
        string operacao = parametros[0];
        string codigo = parametros[1];
        string comandoSql;
        switch (operacao)
        {
            case "EncerraVigenciaAcessorio":
                comandoSql = string.Format(@"
         UPDATE [AcessorioCalculoPagamentoContrato]
            SET [DataTerminoVigencia] = GETDATE()
          WHERE [CodigoAcessorioContrato] = {0}"
                                , codigo);
                int registrosAfetados = 0;
                cDados.execSQL(comandoSql, ref registrosAfetados);
                break;
            case "InitEditForm":
            case "ObtemValorAcessorio":
                comandoSql = string.Format(@"
        SELECT ISNULL(CONVERT(NUMERIC(10,3),Aliquota), CONVERT(NUMERIC(10,3),Valor)) AS ValorAcessorio,
               CASE WHEN Aliquota IS NOT NULL THEN 'A' ELSE 'V' END TipoAcessorio
          FROM AcessorioCalculoPagamento
         WHERE CodigoAcessorio = {0}", codigo);
                DataSet ds = cDados.getDataSet(comandoSql);
                object valor = ds.Tables[0].Rows[0]["ValorAcessorio"];
                object tipo = ds.Tables[0].Rows[0]["TipoAcessorio"];
                e.Result = String.Format("{0};{1}", tipo, valor);
                break;
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnTerminoVigencia" )
        {
            object dataTerminoVigencia = gvDados.GetRowValues(e.VisibleIndex, "DataTerminoVigencia");
            bool habilitado = Convert.IsDBNull(dataTerminoVigencia) && podeDefinir;
            e.Enabled = habilitado;
            if (!habilitado)
                e.Image.Url = "~/imagens/botoes/contratoEncerradoDes.png";
        }
    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OrderedDictionary values = e.NewValues;
        if (!VerificaValorAcessorioValido(values))
            throw new Exception("Deve ser informado um valor válido para o campo 'Aliquota' ou para o campo 'Valor'.");
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OrderedDictionary values = e.NewValues;
        if (!VerificaValorAcessorioValido(values))
            throw new Exception("Deve ser informado um valor válido para o campo 'Aliquota' ou para o campo 'Valor'.");
    }

    protected void gvDados_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        object dataInicioVigencia = e.NewValues["DataInicioVigencia"];
        object dataTerminoVigencia = e.NewValues["DataTerminoVigencia"];

        if (dataTerminoVigencia == null || Convert.IsDBNull(dataTerminoVigencia)) return;

        if (((DateTime)dataInicioVigencia).Date > ((DateTime)dataTerminoVigencia).Date)
            throw new ArgumentException("A data de início da vigência deve ser menor ou igual a data de término da vigência.");
    }

    #endregion

    #region Methods

    private static bool VerificaValorAcessorioValido(OrderedDictionary values)
    {
        bool aliquotaValida;
        bool valorValido;
        decimal aliquota;
        decimal valor;
        //Verifica se o campo 'Aliquota' foi informado e se ter um valor válido
        if (values.Contains("Aliquota") && values["Aliquota"] != null)
            aliquotaValida = decimal.TryParse(values["Aliquota"].ToString(), out aliquota);
        else
            aliquotaValida = false;
        //Verifica se o campo 'Valor' foi informado e se ter um valor válido
        if (values.Contains("Valor") && values["Valor"] != null)
            valorValido = decimal.TryParse(values["Valor"].ToString(), out valor);
        else
            valorValido = false;

        return aliquotaValida || valorValido;
    }

    protected string ObtemBtnIncluir()
    {
        string imagemHabilitada = @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""gvDados.AddNewRow();"" style=""cursor: pointer;""/>";
        string imagemDesabilitada = @"<img src=""../../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>";
        string conteudoHtml = string.Format(@"<table style=""width:100%""><tr><td align=""center"">{0}</td></tr></table>",
            (podeIncluir) ? imagemHabilitada : imagemDesabilitada);
        return conteudoHtml;
    } 

    #endregion
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }
}