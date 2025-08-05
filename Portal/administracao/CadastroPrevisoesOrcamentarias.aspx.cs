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

public partial class administracao_CadastroPrevisoesOrcamentarias : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    
    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

    int codigoProjeto = -1;

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

        //podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
        //    codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_EdtRamAtv");
        //podeIncluir = podeEditar;
        //podeExcluir = podeEditar;
                
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {            
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
                
        if (!IsPostBack)
        {
            carregaGvDados();
        }
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "_Strings"));


    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = 370;
    }
    #endregion
    
    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getPrevisoesOrcamentarias(codigoEntidadeUsuarioResponsavel, "");

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            carregaComboPrevisoesBases(ds);

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]))
            {
                if (ds.Tables[0].Rows.Count == 1 && ds.Tables[0].Select("IndicaPrevisaoOficial = 'S'").Length == 0)
                {
                    marcaPrevisaoOficial(0);
                }

                DataRow drOficial = ds.Tables[0].Select("IndicaPrevisaoOficial = 'S'")[0];

                gvDados.Selection.SelectRowByKey(drOficial["CodigoPrevisao"]);
            }
        }        
    }

    protected void gvDados_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {        
        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }

    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    #endregion
    
    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        int codigoPrevisao = -1;

        string descricao = txtDescricao.Text;
        string observacao = txtObservacoes.Text;

        DataSet ds = cDados.getPrevisoesOrcamentarias(codigoEntidadeUsuarioResponsavel, string.Format(" AND DescricaoPrevisao = '{0}'", descricao)); 

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return "Previsão já existente!";

        bool result = incluiPrevisaoOrcamentaria(descricao, observacao, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, int.Parse(ddlPrevisaoBase.Value.ToString()), ref codigoPrevisao);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvDados();
            return "";
        }

    }

    public bool incluiPrevisaoOrcamentaria(string descricao, string observacoes, int codigoUsuario, int codigoEntidade, int codigoProjeto, int codigoPrevisaoBase, ref int novoCodigo)
    {
        bool retorno = false;
        try
        {
           string comandoSQL = string.Format(@"
                        BEGIN
                            DECLARE @CodigoPrevisao int
                                   ,@CodigoProjeto int
                                   ,@CodigoPrevisaoOrigem int
                                   ,@CodigoPrevisaoDestino int

                            SET @CodigoProjeto = {6}
                            SET @CodigoPrevisaoOrigem = {7}
                        
                            INSERT INTO {0}.{1}.PrevisaoFluxoCaixaProjeto(DescricaoPrevisao, Observacao, DataCriacaoPrevisao, CodigoUsuarioCriacao, IndicaPrevisaoOficial, CodigoEntidade) 
                                 VALUES ('{2}', '{3}', GetDate(), {4}, 'N', {5})

                            SELECT @CodigoPrevisao = SCOPE_IDENTITY()
                            
                            IF(@CodigoPrevisaoOrigem > 0)
                                INSERT INTO {0}.{1}.FluxoCaixaProjeto 
                                (
                                        [CodigoProjeto]
                                      , [CodigoConta]
                                      , [Ano]
                                      , [Mes]
                                      , [ValorPrevisto]
                                      , [ValorTendencia]
                                      , [CodigoPrevisao]
                                )
                                SELECT 
                                        [CodigoProjeto]
                                      , [CodigoConta]
                                      , [Ano]
                                      , [Mes]
                                      , [ValorPrevisto]
                                      , [ValorTendencia]
                                      , @CodigoPrevisao
                                 FROM {0}.{1}.[FluxoCaixaProjeto]     AS fcp
                                WHERE fcp.[CodigoProjeto] = @CodigoProjeto
                                  AND fcp.[CodigoPrevisao] = @CodigoPrevisaoOrigem

                           SELECT @CodigoPrevisao AS CodigoPrevisao

                          END
                        ", cDados.getDbName(), cDados.getDbOwner()
                         , descricao.Replace("'", "''")
                         , observacoes.Replace("'", "''")
                         , codigoUsuario
                         , codigoEntidade
                         , codigoProjeto
                         , codigoPrevisaoBase);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                novoCodigo = int.Parse(ds.Tables[0].Rows[0]["CodigoPrevisao"].ToString());

            retorno = true;
        }
        catch
        {
            retorno = false;
        }
        return retorno;
    }


    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoPrevisao = int.Parse(getChavePrimaria());

        string descricao = txtDescricao.Text;
        string observacao = txtObservacoes.Text;

        DataSet ds = cDados.getPrevisoesOrcamentarias(codigoEntidadeUsuarioResponsavel, string.Format(" AND DescricaoPrevisao = '{0}' AND CodigoPrevisao <> {1}", descricao, codigoPrevisao));

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            return "Previsão já existente!";

        bool result = cDados.atualizaPrevisaoOrcamentaria(codigoPrevisao, descricao, observacao, codigoUsuarioResponsavel);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGvDados();
            return "";
        }    
    }

    private string marcaPrevisaoOficial(int indexLinha)
    {
        string retorno = "";

        if (indexLinha != -1)
        {
            int codigoPrevisao = int.Parse(gvDados.GetRowValues(indexLinha, gvDados.KeyFieldName).ToString());

            bool result = atualizaPrevisaoOrcamentariaOficial(codigoPrevisao, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel);
        }

        return retorno;
    }

    public bool atualizaPrevisaoOrcamentariaOficial(int codigoPrevisao, int codigoEntidade, int codigoUsuario)
    {
        bool retorno = false;
        try
        {
            string comandoSQL = string.Format(@"
                      BEGIN
                            DECLARE @CodigoPrevisao int

                            SET @CodigoPrevisao = {2}
                             
                            UPDATE {0}.{1}.PrevisaoFluxoCaixaProjeto SET IndicaPrevisaoOficial = 'N'
                             WHERE IndicaPrevisaoOficial = 'S'
                               AND CodigoEntidade = {4}

                            UPDATE {0}.{1}.PrevisaoFluxoCaixaProjeto SET IndicaPrevisaoOficial = 'S'
                                                                        ,DataUltimaAlteracao = GetDate()
                                                                        ,CodigoUsuarioUltimaAlteracao = {3}
                             WHERE CodigoPrevisao = @CodigoPrevisao 
                        END
                        ", cDados.getDbName(), cDados.getDbOwner()
                         , codigoPrevisao
                         , codigoUsuario
                         , codigoEntidade);

            int regAf = 0;

            cDados.execSQL(comandoSQL, ref regAf);

            retorno = true;
        }
        catch
        {
            retorno = false;
        }
        return retorno;
    }


    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        //int codigoRamoAtividade = int.Parse(getChavePrimaria());

        //if (cDados.verificaExclusaoTipoRamoAtividade(codigoRamoAtividade) == false)
        //{
        //    return "O ramo de atividade não pode ser excluído. Existem fornecedores ou clientes cadastrados nele.";
        //}
        //else
        //{
        //    bool result = cDados.excluiTipoRamoAtividade(codigoRamoAtividade);

        //    if (result == false)
        //        return "Erro ao salvar o registro!";
        //    else
        //    {
        //        carregaGvDados();
        //        return "";
        //    }
        //}

        return "";
    }

    #endregion

    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString() != "")
        {
            marcaPrevisaoOficial(int.Parse(e.Parameters.ToString()));
        }
    }
    
    private void carregaComboPrevisoesBases(DataSet ds)
    {
        ddlPrevisaoBase.DataSource = ds;
        ddlPrevisaoBase.TextField = "DescricaoPrevisao";
        ddlPrevisaoBase.ValueField = "CodigoPrevisao";
        ddlPrevisaoBase.DataBind();

        ListEditItem lei = new ListEditItem(Resources.traducao.nenhum, "-1");

        ddlPrevisaoBase.Items.Insert(0, lei);
    }
}
