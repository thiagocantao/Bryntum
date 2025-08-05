using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_configParticipes : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioLogado = 0;
    int codigoEntidade = 0;

    private string resolucaoCliente = "";
    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;

    public int CodigoProjeto;

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
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        if (int.TryParse(Request.QueryString["CP"], out CodigoProjeto))
        {
            CodigoProjeto = int.Parse(Request.QueryString["CP"] + "");
        }

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioLogado, codigoEntidade, CodigoProjeto, "NULL", "PR", 0, "NULL", "PR_CnsCfgPtc");
        }

        bool bPodeAlterarDados = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidade, CodigoProjeto, "NULL", "PR", 0, "NULL", "PR_AltCfgPtc");
        podeIncluir = podeEditar = podeExcluir = bPodeAlterarDados;
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        defineAlturaTela(resolucaoCliente);
        carregaGvDados();
        populaPapeis();
        cDados.aplicaEstiloVisual(Page);
    }

    private void populaPapeis()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoPapelParticipe, DescricaoPapelParticipe 
          FROM TipoPapelParticipe
         WHERE IndicaPapelAtivo = 'S' 
           AND (IndicaConcedente = 'S' OR IndicaConvenente = 'S')
          ORDER BY 2 ASC");
        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlPapel.TextField = "DescricaoPapelParticipe";
        ddlPapel.ValueField = "CodigoPapelParticipe";
        ddlPapel.DataSource = ds;
        ddlPapel.DataBind();
    }



    private void defineAlturaTela(string resolucaoCliente)
    {
        int altura = 0;
        int largura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        gvDados.Settings.VerticalScrollableHeight = (altura - 360);
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/configParticipes.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "configParticipes"));
    }

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        SELECT pp.CodigoProjeto, 
               pp.CodigoPessoa,
               p.NomePessoa,
               pp.IndicaParticipeAtivo, 
               pp.PercentualLimiteUsoRecurso, 
               pp.DataInclusao, 
               pp.CodigoUsuarioInclusao, 
               pp.DataUltimaAlteracao, 
               pp.CodigoUsuarioUltimaAlteracao,
               pp.CodigoPapelParticipe,
               tpp.DescricaoPapelParticipe
          FROM ProjetoParticipe pp
    INNER JOIN Pessoa p ON (p.CodigoPessoa = pp.CodigoPessoa)
    INNER JOIN TipoPapelParticipe tpp ON (tpp.CodigoPapelParticipe = pp.CodigoPapelParticipe)
         WHERE pp.CodigoProjeto = {0}
  order by 3 asc ", CodigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();

    }



    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "ConfigParticipe", "Configuração de partícipes no projeto", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ConfigParticipe");
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        string codigoPessoa = gvDados.GetRowValues(e.VisibleIndex, "CodigoPessoa") != null ?
            gvDados.GetRowValues(e.VisibleIndex, "CodigoPessoa").ToString() :
            "-1";
        if (e.ButtonID.Equals("btnExcluir"))
        {
            bool podeExcluirP;
            podeExcluirP = podeExcluirParticipe(codigoPessoa);

            if (podeEditar == true)
            {
                if (podeExcluirP)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/ExcluirRegDes.png";
                    e.Image.ToolTip = "O partícipe não pode ser excluído pois há valores registrados para o partícipe nesse projeto";
                }
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/ExcluirRegDes.png";
            }
        }
    }

    protected bool podeExcluirParticipe(string codigoPessoa)
    {
        bool retorno = false;
        string comandoSQL = string.Format(@"
            declare @CodigoProjeto as int
            declare @CodigoPessoaParticipe as int

            SET @CodigoProjeto = {1}
            SET @CodigoPessoaParticipe = {0}

            SELECT [dbo].[f_gestconv_podeExcluirParticipeProjeto] (
                   @CodigoProjeto
                  ,@CodigoPessoaParticipe)", codigoPessoa, CodigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            retorno = (bool)ds.Tables[0].Rows[0][0];
        }
        return retorno;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cp_erro"] = "";
        ((ASPxGridView)sender).JSProperties["cp_aviso"] = "";


        string mensagemErro_Persistencia = "";

        if (e.Parameters == "Incluir")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Partícipe Incluído com sucesso!";
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameters == "Editar")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Partícipe atualizado com sucesso!";
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameters == "Excluir")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Partícipe excluído com sucesso!";
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia != "")
        {// alguma coisa deu errado...
            ((ASPxGridView)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
            if (e.Parameters != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";

        string chave = getChavePrimaria();

        string comandoSQL = string.Format(@"
        DECLARE @podeExcluir as bit
        DECLARE @CodigoProjeto as int
        DECLARE @CodigoPessoa as int

        SET @CodigoProjeto = {0}
        SET @CodigoPessoa = {1}

        DELETE FROM[dbo].[ProjetoParticipe]
         WHERE CodigoProjeto = @CodigoProjeto
           AND CodigoPessoa = @CodigoPessoa", CodigoProjeto, chave);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            if (retorno.Trim().ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";

        string codigoParticipe = ddlParticipe.Value.ToString();
        string codigoPapel = ddlPapel.Value.ToString();
        string indicaAtivo = "S";
        string percentualLimite = "NULL";

        if (ckbAtivo.Value != null)
        {
            indicaAtivo = (ckbAtivo.Checked == true) ? "S" : "N";
        }

        if (spinLimite.Value != null)
        {
            percentualLimite = spinLimite.Value.ToString();
        }

        string codigoUsuarioUltimaAlteracao = codigoUsuarioLogado.ToString();

        string comandoSQL = cDados.geraBlocoBeginTran() + Environment.NewLine +
            string.Format(@"
           declare @CodigoProjeto as int
           declare @CodigoPessoa as int
           declare @CodigoPapelParticipe as smallint
           declare @IndicaParticipeAtivo as char(1)
           declare @PercentualLimiteUsoRecurso as smallint
           declare @CodigoUsuarioUltimaAlteracao as int

           SET @CodigoProjeto = {0}
           SET @CodigoPessoa = {1}
           SET @CodigoPapelParticipe = {2}
           SET @IndicaParticipeAtivo = '{3}'
           SET @CodigoUsuarioUltimaAlteracao = {4}
           SET @PercentualLimiteUsoRecurso = {5}

           UPDATE [dbo].[ProjetoParticipe]
              SET [CodigoPapelParticipe] = @CodigoPapelParticipe
                 ,[IndicaParticipeAtivo] = @IndicaParticipeAtivo
                 ,[PercentualLimiteUsoRecurso] = @PercentualLimiteUsoRecurso
                 ,[DataUltimaAlteracao] = GetDate()
                 ,[CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioUltimaAlteracao
            WHERE CodigoProjeto = @CodigoProjeto 
              AND CodigoPessoa = @CodigoPessoa", CodigoProjeto, codigoParticipe, codigoPapel, indicaAtivo, codigoUsuarioUltimaAlteracao, percentualLimite) + Environment.NewLine +
            cDados.geraBlocoEndTran();
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            if (retorno.Trim().ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";

        string codigoParticipe = ddlParticipe.Value.ToString();
        string codigoPapel = ddlPapel.Value.ToString();
        string indicaAtivo = "S";
        string percentualLimite = "NULL";

        if (ckbAtivo.Value != null)
        {
            indicaAtivo = (ckbAtivo.Checked == true) ? "S" : "N";
        }

        if (spinLimite.Value != null)
        {
            percentualLimite = spinLimite.Value.ToString();
        }

        string codigoUsuarioInclusao = codigoUsuarioLogado.ToString();

        string comandoSQL = cDados.geraBlocoBeginTran() + Environment.NewLine +
            string.Format(@"
           declare @CodigoProjeto as int
           declare @CodigoPessoa as int
           declare @CodigoPapelParticipe as smallint
           declare @IndicaParticipeAtivo as char(1)
           declare @PercentualLimiteUsoRecurso as smallint
           declare @DataInclusao as datetime
           declare @CodigoUsuarioInclusao as int

           SET @CodigoProjeto = {0}
           SET @CodigoPessoa = {1}
           SET @CodigoPapelParticipe = {2}
           SET @IndicaParticipeAtivo = '{3}'
           SET @DataInclusao = GetDate()
           SET @CodigoUsuarioInclusao = {4}
           SET @PercentualLimiteUsoRecurso = {5}

            INSERT INTO [dbo].[ProjetoParticipe]
                            ([CodigoProjeto]
                            ,[CodigoPessoa]
                            ,[CodigoPapelParticipe]
                            ,[IndicaParticipeAtivo]
                            ,[PercentualLimiteUsoRecurso]
                            ,[DataInclusao]
                            ,[CodigoUsuarioInclusao])
                      VALUES(@CodigoProjeto
                            ,@CodigoPessoa
                            ,@CodigoPapelParticipe
                            ,@IndicaParticipeAtivo
                            ,@PercentualLimiteUsoRecurso
                            ,@DataInclusao
                            ,@CodigoUsuarioInclusao)
                ", CodigoProjeto, codigoParticipe, codigoPapel, indicaAtivo, codigoUsuarioInclusao, percentualLimite) + Environment.NewLine +
            cDados.geraBlocoEndTran();
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            if (retorno.Trim().ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
            if (e.Text.IndexOf("Status: N") != -1)
            {
                e.TextValue = "Inativo";
                e.Text = "Inativo";
            }
            if (e.Text.IndexOf("Status: S") != -1)
            {
                e.TextValue = "Ativo";
                e.Text = "Ativo";
            }
        }

    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "IndicaParticipeAtivo")
        {
            if (e.CellValue.ToString() == "S")
            {
                e.Cell.Text = "Ativo";
            }
            else
            {
                e.Cell.Text = "Inativo";
            }
        }
    }

    protected void pnDdlParticipe_Callback(object sender, CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cp_Ativo"] = "N";

        //codigo da pessoa selecionda
        string argumentos_codigoPessoaAtual = e.Parameter;

        string nome = "";
        string codigo = "";

        string codigoPessoaAtual = argumentos_codigoPessoaAtual.Split('|')[0];
        string indicaAtivo = argumentos_codigoPessoaAtual.Split('|')[1];



        string selectPessoaAtual = string.Format(@" select CodigoPessoa, NomePessoa from Pessoa where CodigoPessoa = {0}", codigoPessoaAtual);
        DataSet ds = cDados.getDataSet(selectPessoaAtual);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nome = ds.Tables[0].Rows[0]["NomePessoa"].ToString();
            codigo = ds.Tables[0].Rows[0]["CodigoPessoa"].ToString();
        }


        string comandoSQL = string.Format(@"
        SELECT p.CodigoPessoa, 
               p.NomePessoa
          FROM PessoaEntidade pe
    INNER JOIN Pessoa p on (p.CodigoPessoa = pe.CodigoPessoa)
         WHERE pe.CodigoEntidade = {0}  
           and pe.IndicaParticipe = 'S' 
           AND pe.IndicaPessoaAtivaEntidade = 'S'    
          AND p.codigoPessoa NOT IN (SELECT CodigoPessoa 
                                        FROM projetoparticipe 
                                       WHERE  codigoprojeto = {1})
         
          
       order by 2 asc", codigoEntidade, CodigoProjeto, codigoPessoaAtual);

        DataSet dsDll = cDados.getDataSet(comandoSQL);

        ddlParticipe.TextField = "NomePessoa";
        ddlParticipe.ValueField = "CodigoPessoa";
        ddlParticipe.DataSource = dsDll.Tables[0];
        ddlParticipe.DataBind();
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlParticipe.Items.Add(new ListEditItem(nome, int.Parse(codigo)));
        }

        ((ASPxCallbackPanel)sender).JSProperties["cp_Ativo"] = indicaAtivo;
    }
}