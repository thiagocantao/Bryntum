using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Estrategias_wizard_seriesMeta : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int codigoEntidade;
    private int codigoIndicador = -1;
    private int casasDecimais = 0;

    private string unidadeMedida = "";

    public bool podeEditar = false;
    public bool podeIncluir = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["CodigoIndicador"] != null && Request.QueryString["CodigoIndicador"].ToString() != "")
            codigoIndicador = int.Parse(Request.QueryString["CodigoIndicador"].ToString());

        if (Request.QueryString["CasasDecimais"] != null)
            casasDecimais = int.Parse(Request.QueryString["CasasDecimais"].ToString());

        if (Request.QueryString["SiglaUnidadeMedida"] != null)
            unidadeMedida = Request.QueryString["SiglaUnidadeMedida"].ToString();

        if (Request.QueryString["Permissao"] != null)
            podeEditar = Request.QueryString["Permissao"].ToString() == "S";

        if (Request.QueryString["Altura"] != null)
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString());

        spValorMeta.DecimalPlaces = casasDecimais;

        if (casasDecimais == 0)
            spValorMeta.NumberType = DevExpress.Web.SpinEditNumberType.Integer;
        else
            spValorMeta.NumberType = DevExpress.Web.SpinEditNumberType.Float;

        spValorMeta.DisplayFormatString = "N" + casasDecimais;

        ((GridViewDataTextColumn)gvDados.Columns["meta"]).PropertiesTextEdit.DisplayFormatString = "N" + casasDecimais; 

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        cDados.aplicaEstiloVisual(gvDados);
        //cDados.aplicaEstiloVisual(ddlAnos);
        cDados.aplicaEstiloVisual(ddlUnidades);
         carregaComboUnidades();  

        //txtIndicadorDado.Text =
        if (!Page.IsPostBack)
        {
            DataSet ds = cDados.getIndicadores(codigoEntidade, idUsuarioLogado, "N", " and  i.CodigoIndicador = " + codigoIndicador);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                txtIndicadorDado.Text = ds.Tables[0].Rows[0]["NomeIndicador"].ToString();
            }
        }
        carregaGridMetas();

        defineValidacao();
    }

    private void defineValidacao()
    {
        string comandoSQL = string.Format(
       @"SELECT Ano 
           FROM PeriodoEstrategia 
          WHERE CodigoUnidadeNegocio = {0} 
            AND IndicaAnoAtivo = 'S'", codigoEntidade);
        DataSet ds = cDados.getDataSet(comandoSQL);
        string anos = "";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            anos += dr["Ano"] + ";";
        }
        hfGeral.Set("Anos", anos);

        string scriptValidacao = "";

        scriptValidacao += "if(s.GetValue() > " + DateTime.Now.Year + @"){ e.isValid = false; e.errorText = 'O ano não pode ser posterior ao atual!'; }
";

        scriptValidacao += @"else if(hfGeral.Get('Anos').indexOf(s.GetValue()) != -1){ e.isValid = false; e.errorText = 'O ano não pode estar ativo no período estratégia!'; }
";


        spAno.ClientSideEvents.Validation = "function (s, e){ " + scriptValidacao + " }";
        //spAno.JSProperties["cp_AnoAtual"] = DateTime.Now.Year;
    }

    private void carregaGridMetas()
    {
        string comandoSQL = string.Format(
        @"BEGIN
             DECLARE @CodigoEntidade          INT,
                     @CodigoIndicador   INT
                 
                 SET @CodigoEntidade = {0} --1
                 SET @CodigoIndicador = {1}--187

              SELECT miu.Ano as ano, Sum(miu.ValorMeta) as meta
                FROM MetaIndicadorUnidade AS miu
          INNER JOIN IndicadorUnidade AS iu 
                  ON (miu.CodigoIndicador = iu.CodigoIndicador
                 AND iu.CodigoUnidadeNegocio = @CodigoEntidade
                 AND iu.CodigoIndicador = @CodigoIndicador)
               WHERE miu.IndicaValorSerieHistorica = 'S'
                 AND iu.DataExclusao IS NULL
            GROUP BY miu.Ano
            END", codigoEntidade, codigoIndicador);
        gvDados.DataSource = cDados.getDataSet(comandoSQL);
        gvDados.DataBind();
        
    }

    private void carregaComboUnidades()
    {
        DataSet dsUnidadesIndicador = cDados.getUnidadesUsuarioIndicadorPorPermissao(codigoIndicador, idUsuarioLogado, codigoEntidade, "IN_DefMta", "");

        if (cDados.DataSetOk(dsUnidadesIndicador))
        {
            ddlUnidades.DataSource = dsUnidadesIndicador;
            ddlUnidades.TextField = "NomeUnidadeNegocio";
            ddlUnidades.ValueField = "CodigoUnidadeNegocio";
            ddlUnidades.DataBind();
        }

        if (!IsPostBack)
        {
            ddlUnidades.SelectedIndex = 0;
        }
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/seriesMeta.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "seriesMeta"));
        /*string comando = string.Format(@"<script type='text/javascript'>onloadDesabilitaBarraNavegacao();</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "onLoadCall", comando);*/
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {
            // alguma coisa deu errado...
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
            if (mensagemErro_Persistencia.Contains("REFERENCE"))
                mensagemErro_Persistencia = "O dado que se tenta excluir está sendo usado por outra tela do sistema.";
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
        carregaGridMetas();
    }

    private string persisteExclusaoRegistro()
    {

        string mensagemErro = "";
        string ano = gvDados.GetRowValues(gvDados.FocusedRowIndex, "ano").ToString();
        //string mes = gvDados.GetRowValues(gvDados.FocusedRowIndex, "Mes").ToString();nao deu certo, pois se for assim implica mudança no select
        // procurou-se não modificar o select.

        string mes = DateTime.Now.Month.ToString();

        bool retorno = excluiMeta(codigoIndicador, int.Parse(ddlUnidades.SelectedItem.Value.ToString()), int.Parse(ano), int.Parse(mes), ref mensagemErro);
        return mensagemErro;
    }

    private string persisteEdicaoRegistro()
    {

        string mensagemErro = "";
        bool retorno = editaMeta(codigoIndicador, int.Parse(ddlUnidades.SelectedItem.Value.ToString()), int.Parse(spAno.Text), 12, decimal.Parse(spValorMeta.Text), ref mensagemErro);
        return mensagemErro;
    }

    private string persisteInclusaoRegistro()
    {
        string mensagemErro = "";
        bool retorno = incluiMeta(codigoIndicador, int.Parse(ddlUnidades.SelectedItem.Value.ToString()), int.Parse(spAno.Text), 12, decimal.Parse(spValorMeta.Text), idUsuarioLogado, 'S', ref mensagemErro);
        return mensagemErro;
    }

    private bool incluiMeta(int codigoIndicador, int codigoUnidade, int ano, int mes, decimal valorMeta, int codigoUsuario, char IndicaValorSerieHistorica, ref string mensagemErro)
    {
        bool retorno = false;
        try
        {

            int regAfetados = 0;
            string comandoSQL = string.Format(@"
            BEGIN 

                DECLARE @ANO AS int
                set @ANO = {4}
                --Não permitir inclusão de série histórica para anos ativos em PeriodoEstrategia.
                IF NOT EXISTS (SELECT 1 
                                 FROM PeriodoEstrategia 
                                WHERE CodigoUnidadeNegocio = {3} 
                                  AND IndicaAnoAtivo = 'S' 
                                  AND Ano = @Ano)
                BEGIN 
                        INSERT INTO {0}.{1}.[MetaIndicadorUnidade]
                               ([CodigoIndicador]       ,[CodigoUnidadeNegocio]           ,[Ano]
                               ,[Mes]                   ,[ValorMeta]                      ,[DataInclusao]
                               ,[CodigoUsuarioInclusao] ,[IndicaValorSerieHistorica])
                        VALUES ({2}                    ,{3}                              ,{4}
                               ,{5}                    ,{6}                              ,getdate()
                               ,{7}                    ,'{8}')
                END
             END
             ", cDados.getDbName(), cDados.getDbOwner(), codigoIndicador, codigoUnidade, ano,
                                              mes, valorMeta.ToString().Replace(",", "."), codigoUsuario, IndicaValorSerieHistorica);
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if (regAfetados == 0)
            {
                mensagemErro = "Erro: Nenhum registro foi afetado.";
            }
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            retorno = false;
        }

        return retorno;
    }

    private bool excluiMeta(int codigoIndicador, int codigoUnidade, int ano, int mes,  ref string mensagemErro)
    {
        bool retorno = false;
        try
        {

            int regAfetados = 0;
            string comandoSQL = string.Format(@"
        DELETE FROM {0}.{1}.MetaIndicadorUnidade
              WHERE CodigoIndicador = {2} 
                and CodigoUnidadeNegocio = {3} 
                and Ano = {4}", cDados.getDbName(), cDados.getDbOwner(), codigoIndicador, codigoUnidade, ano);
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if (regAfetados == 0)
            {
                mensagemErro = "Erro: Nenhum registro foi afetado.";
            }
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            retorno = false;
        }

        return retorno;
    }

    private bool editaMeta(int codigoIndicador, int codigoUnidade, int ano, int mes, decimal valorMeta, ref string mensagemErro)
    {
        bool retorno = false;
        try
        {

            int regAfetados = 0;
            string comandoSQL = string.Format(@"
        UPDATE {0}.{1}.MetaIndicadorUnidade
           SET [ValorMeta] = {6}      
         WHERE CodigoIndicador = {2} AND 
               CodigoUnidadeNegocio = {3} AND 
               ano = {4} AND 
               mes = {5}", cDados.getDbName(), cDados.getDbOwner(), codigoIndicador, codigoUnidade, ano,
                                              mes, valorMeta.ToString().Replace(",", "."));
            retorno = cDados.execSQL(comandoSQL, ref regAfetados);
            if (regAfetados == 0)
            {
                mensagemErro = "Erro: Nenhum registro foi afetado.";
            }
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            retorno = false;
        }

        return retorno;
    }
}