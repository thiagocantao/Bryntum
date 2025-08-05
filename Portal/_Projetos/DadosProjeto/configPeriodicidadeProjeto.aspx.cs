using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_configPeriodicidadeProjeto : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;

    public int CodigoProjeto = 0;

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

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        int.TryParse(Request.QueryString["CP"] + "", out CodigoProjeto);


        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, CodigoProjeto, "NULL", "PR", 0, "NULL", "PR_CnsCfgPer");
        }

        bool bPodeAlterarDados = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, CodigoProjeto, "NULL", "PR", 0, "NULL", "PR_AltCfgPer");
        podeIncluir = podeEditar = podeExcluir = bPodeAlterarDados;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        carregaGvDados();
        populaTipoPeriodicidade();
        cDados.aplicaEstiloVisual(Page);
    }

    private void populaTipoPeriodicidade()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoPeriodicidade, DescricaoPeriodicidade_PT 
          FROM TipoPeriodicidade
          WHERE IntervaloMeses BETWEEN 1 AND 12");
        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlTipoPeriodicidade.TextField = "DescricaoPeriodicidade_PT";
        ddlTipoPeriodicidade.ValueField = "CodigoPeriodicidade";
        ddlTipoPeriodicidade.DataSource = ds.Tables[0];
        ddlTipoPeriodicidade.DataBind();
    }

    #region GRID

    private void carregaGvDados()
    {

        string comandoSQL = string.Format(@"
        SELECT pap.[CodigoProjeto]
              ,pap.[Ano]
              ,pap.[IndicaAnoAtivo]
              ,pap.[IndicaAnoPeriodoEditavel]
              ,pap.[IndicaMetaEditavel]
              ,pap.[IndicaResultadoEditavel]
              ,pap.[CodigoPeriodicidadeValoresFinanceiros]
              ,tp.[DescricaoPeriodicidade_PT]
              ,pap.[DataInclusao]
              ,pap.[CodigoUsuarioInclusao]
              ,pap.[DataUltimaAlteracao]
              ,pap.[CodigoUsuarioUltimaAlteracao]
          FROM [PeriodoAnaliseProjeto] pap
    INNER JOIN [TipoPeriodicidade] tp on (tp.CodigoPeriodicidade = pap.[CodigoPeriodicidadeValoresFinanceiros])
         WHERE [CodigoProjeto] = {0}", CodigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
            //----------------------------------------------------- getAnos
            //Procuro os anos que ja tenho cadastrado, pra evitar repetição.
            string anos = "";
            if (cDados.DataTableOk(ds.Tables[0]))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    anos += dr["Ano"] + ";";
                }
                hfGeral.Set("hfAnos", anos);
                //*** fim getAnos
            }
            else
            {
                hfGeral.Set("hfAnos", anos);
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (!podeEditar)
            {
                e.Text = "Edição";
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        else if (e.ButtonID == "btnExcluir")
        {
            if (!podeExcluir)
            {
                e.Text = "Excluir";
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
            else
            {

                e.Text = "Excluir";
                e.Enabled = true;
                e.Image.Url = "~/imagens/botoes/excluirReg02.png";
                e.Image.ToolTip = "Excluir";

            }
        }
    }
    #endregion

    #region VARIOS

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/configPeriodicidadeProjeto.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        alturaPrincipal = altura;
        gvDados.Settings.VerticalScrollableHeight = altura - 290;
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
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
        string retorno = "";

        string ano = spAno.Value.ToString();
        string indicaAnoAtivo = ckbAnoAtivo.Value.ToString();
        string indicaAnoPeriodoEditavel = ckbAnoPeriodoEditavel.Value.ToString();
        string indicaMetaEditavel = ckbMetaEditavel.Value.ToString();
        string indicaResultadoEditavel = ckbResultadoEditavel.Value.ToString();
        string codigoTipoPeriodicidade = ddlTipoPeriodicidade.Value.ToString();



        string comandoSQL = string.Format(@"

           declare @CodigoProjeto as int
           declare @Ano as smallint
           declare @IndicaAnoAtivo as char(1)
           declare @IndicaAnoPeriodoEditavel as char(1)
           declare @IndicaMetaEditavel as char(1)
           declare @IndicaResultadoEditavel as char(1)
           declare @CodigoPeriodicidadeValoresFinanceiros as smallint
           declare @DataInclusao as datetime
           declare @CodigoUsuarioInclusao as int
           declare @DataUltimaAlteracao as datetime
           declare @CodigoUsuarioUltimaAlteracao as int

           SET @CodigoProjeto = {0}
           SET @Ano = {1}
           SET @IndicaAnoAtivo = '{2}'
           SET @IndicaAnoPeriodoEditavel = '{3}'
           SET @IndicaMetaEditavel = '{4}'
           SET @IndicaResultadoEditavel = '{5}'
           SET @CodigoPeriodicidadeValoresFinanceiros = '{6}'
           SET @DataInclusao = getdate()
           SET @CodigoUsuarioInclusao = {7}
           SET @DataUltimaAlteracao = null
           SET @CodigoUsuarioUltimaAlteracao = null

         INSERT INTO [dbo].[PeriodoAnaliseProjeto]
           ([CodigoProjeto]
           ,[Ano]
           ,[IndicaAnoAtivo]
           ,[IndicaAnoPeriodoEditavel]
           ,[IndicaMetaEditavel]
           ,[IndicaResultadoEditavel]
           ,[CodigoPeriodicidadeValoresFinanceiros]
           ,[DataInclusao]
           ,[CodigoUsuarioInclusao]
           ,[DataUltimaAlteracao]
           ,[CodigoUsuarioUltimaAlteracao])
     VALUES (@CodigoProjeto
           ,@Ano
           ,@IndicaAnoAtivo
           ,@IndicaAnoPeriodoEditavel
           ,@IndicaMetaEditavel
           ,@IndicaResultadoEditavel
           ,@CodigoPeriodicidadeValoresFinanceiros
           ,@DataInclusao
           ,@CodigoUsuarioInclusao
           ,@DataUltimaAlteracao
           ,@CodigoUsuarioUltimaAlteracao)",
           /*{0}*/CodigoProjeto,
           /*{1}*/ano,
           /*{2}*/indicaAnoAtivo,
           /*{3}*/indicaAnoPeriodoEditavel,
           /*{4}*/indicaMetaEditavel,
           /*{5}*/indicaResultadoEditavel,
           /*{6}*/codigoTipoPeriodicidade,
           /*{7}*/codigoUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (ds.Tables[0].Rows[0][0].ToString().Trim().ToLower() == "ok")
            {
                retorno = "";
            }
            else
            {
                retorno = ds.Tables[0].Rows[0][0].ToString().Trim();
            }
        }
        return retorno;
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";

        string chave = getChavePrimaria();

        string comandoSQL = string.Format(@"
        declare @CodigoProjeto as int
        declare @Ano as smallint
        
        SET @CodigoProjeto = {0}
        SET @Ano = {1}
        
        DELETE FROM [PeriodoAnaliseProjeto]
         WHERE CodigoProjeto = @CodigoProjeto
           AND Ano = @Ano", CodigoProjeto, chave);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString().Trim().ToLower();
            if (retorno == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        string ano = spAno.Value.ToString();
        string indicaAnoAtivo = ckbAnoAtivo.Value.ToString();
        string indicaAnoPeriodoEditavel = ckbAnoPeriodoEditavel.Value.ToString();
        string indicaMetaEditavel = ckbMetaEditavel.Value.ToString();
        string indicaResultadoEditavel = ckbResultadoEditavel.Value.ToString();
        string codigoTipoPeriodicidade = ddlTipoPeriodicidade.Value.ToString();

        string msgErro = "";

        string comando = string.Format(@"
           declare @CodigoProjeto as int
           declare @Ano as smallint
           declare @IndicaAnoAtivo as char(1)
           declare @IndicaAnoPeriodoEditavel as char(1)
           declare @IndicaMetaEditavel as char(1)
           declare @IndicaResultadoEditavel as char(1)
           declare @CodigoPeriodicidadeValoresFinanceiros as smallint
           declare @DataInclusao as datetime
           declare @CodigoUsuarioInclusao as int
           declare @DataUltimaAlteracao as datetime
           declare @CodigoUsuarioUltimaAlteracao as int

           SET @CodigoProjeto = {0}
           SET @Ano = {1}
           SET @IndicaAnoAtivo = '{2}'
           SET @IndicaAnoPeriodoEditavel = '{3}'
           SET @IndicaMetaEditavel = '{4}'
           SET @IndicaResultadoEditavel = '{5}'
           SET @CodigoPeriodicidadeValoresFinanceiros = '{6}'
           SET @DataInclusao = null
           SET @CodigoUsuarioInclusao = null 
           SET @DataUltimaAlteracao = getdate()
           SET @CodigoUsuarioUltimaAlteracao = {7}

           UPDATE [PeriodoAnaliseProjeto]
              SET [IndicaAnoAtivo] = @IndicaAnoAtivo
                 ,[IndicaAnoPeriodoEditavel] = @IndicaAnoPeriodoEditavel
                 ,[IndicaMetaEditavel] = @IndicaMetaEditavel
                 ,[IndicaResultadoEditavel] = @IndicaResultadoEditavel
                 ,[CodigoPeriodicidadeValoresFinanceiros] = @CodigoPeriodicidadeValoresFinanceiros
                 --,[DataInclusao] = @DataInclusao
                 --,[CodigoUsuarioInclusao] = @CodigoUsuarioInclusao
                 ,[DataUltimaAlteracao] = @DataUltimaAlteracao
                 ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioUltimaAlteracao
            WHERE [CodigoProjeto] = @CodigoProjeto 
              AND [Ano] = @Ano",
           /*{0}*/CodigoProjeto,
           /*{1}*/ano,
           /*{2}*/indicaAnoAtivo,
           /*{3}*/indicaAnoPeriodoEditavel,
           /*{4}*/indicaMetaEditavel,
           /*{5}*/indicaResultadoEditavel,
           /*{6}*/codigoTipoPeriodicidade,
           /*{7}*/codigoUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran()
            + Environment.NewLine +
            comando
            + Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            msgErro = ds.Tables[0].Rows[0][0].ToString().Trim().ToLower();
            if (msgErro == "ok")
            {
                msgErro = "";
            }
        }
        return msgErro;
    }

    // Método responsável pela Exclusão do registro
    // {...}

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PeriodoEst");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "PeriodoEst", "Periodicidade do Projeto", this);
    }

    #endregion
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cp_erro"] = "";
        ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cp_aviso"] = "";

        string mensagemErro_Persistencia = "";
        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Configuração de periodicidade incluída com sucesso!";
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Configuração de periodicidade atualizada com sucesso!";
        }
        if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Configuração de periodicidade excluída com sucesso!";
        }
        ((ASPxGridView)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
        //carregaGvDados();

    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }
}