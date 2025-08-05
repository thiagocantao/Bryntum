using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_CadastroOrcTematicos : System.Web.UI.Page
{
    dados cDados;
    private bool podeIncluir;
    private bool podeEditar;

    public int codigoUsuarioResponsavel { get; private set; }
    public int codigoEntidadeUsuarioResponsavel { get; private set; }
    public bool podePermissao { get; private set; }
    public bool podeConsultar { get; private set; }
    public bool podeExcluir { get; private set; }

    public string resolucaoCliente { get; private set; }

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());


        podeIncluir = (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_OrcTematicos"));
        podeEditar = podeIncluir;
        podeExcluir = podeEditar;

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        if (!IsPostBack)
        {
            if (!podeIncluir)
            {
                cDados.RedirecionaParaTelaSemAcesso(this);
            }
        }

        this.Title = cDados.getNomeSistema();
    }

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        carregaGvDados();

        cDados.aplicaEstiloVisual(this.Page);

        string chave = getChavePrimaria();
        if (hfGeral.Contains("TipoOperacao") && hfGeral.Get("TipoOperacao") + "" != "Incluir")
        {
            carregaListaDotacoes(chave);
        }
        else
        {
            carregaListaDotacoes("-1");
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "ORCTEM", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }



        defineAlturaTela(resolucaoCliente);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int altura = 0;
        int largura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 350;
    }

    private void carregaListaDotacoes(string codigoOrcamentoTematico)
    {
        string comandoSQL = "";
        DataSet ds;
        if (codigoOrcamentoTematico != "-1")
        {
            comandoSQL = string.Format(@"
            SELECT dot.[Dotacao]
                  ,CASE WHEN  exists (SELECT TOP 1 1 
                                        FROM  [pbh_OrcamentoTematicoDotacao] ot_dot
                                       WHERE ot_dot.CodigoOrcamentoTematico = {2} AND ot_dot.Dotacao = dot.Dotacao AND ot_dot.CodigoEntidade = {3}) 
                  THEN 'S' 
                  ELSE 'N' END as Selecionado                  
                  ,CASE WHEN  exists (SELECT TOP 1 1 
                                        FROM  [pbh_OrcamentoTematicoDotacao] ot_dot
                                       WHERE ot_dot.CodigoOrcamentoTematico = {2} AND ot_dot.Dotacao = dot.Dotacao AND ot_dot.CodigoEntidade = {3}) 
                  THEN 1 
                  ELSE 0 END as ColunaAgrupamento
              FROM {0}.{1}.[pbh_DotacaoOrcamentaria] dot
               WHERE dot.[DataExclusao] IS NULL 
                 AND dot.[CodigoEntidade] = {3}
               order by 3 desc, 1 ASC", cDados.getDbName(), cDados.getDbOwner(), codigoOrcamentoTematico, codigoEntidadeUsuarioResponsavel);
        }
        else
        {
            comandoSQL = string.Format(@"
            SELECT dot.[Dotacao]
                  ,'N' as Selecionado                  
                  , 0  as ColunaAgrupamento
              FROM {0}.{1}.[pbh_DotacaoOrcamentaria] dot
               WHERE dot.[DataExclusao] IS NULL 
                 AND dot.[CodigoEntidade] = {3}
               order by 3 desc, 1 ASC", cDados.getDbName(), cDados.getDbOwner(), codigoOrcamentoTematico, codigoEntidadeUsuarioResponsavel);
        }

        ds = cDados.getDataSet(comandoSQL);
        ((GridViewDataTextColumn)gvDotacoes.Columns["Dotacao"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gvDotacoes.Columns["Dotacao"]).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
        gvDotacoes.DataSource = ds.Tables[0];
        gvDotacoes.DataBind();
    }

    private void carregaGvDados()
    {

        string comandoSQL = string.Format(@"SELECT CodigoOrcamentoTematico, 
                                                   DescricaoOrcamentoTematico, 
                                                   CodigoEntidade
                                              FROM {0}.{1}.pbh_OrcamentoTematico
                                             where CodigoEntidade = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));      
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/CadastroOrcTematicos.js""></script>"));
        this.TH(this.TS("barraNavegacao", "CadastroOrcTematicos"));
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "OrcTem");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "OrcTem", "Orçamentos Temáticos", this);
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
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cp_erro"] = "";


        if (e.Parameters == "Incluir")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = Resources.traducao.CadastroOrcTematicos_or_amento_tem_tico_inclu_do_com_sucesso_;
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameters == "Editar")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = Resources.traducao.CadastroOrcTematicos_or_amento_tem_tico_alterado_com_sucesso_;
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameters == "Excluir")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = Resources.traducao.CadastroOrcTematicos_or_amento_tem_tico_exclu_do_com_sucesso_;
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        if (mensagemErro_Persistencia != "")
        {// alguma coisa deu errado...
            ((ASPxGridView)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
            if (e.Parameters != "Excluir")
                gvDados.ClientVisible = false;
        }
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";
        string codigoOrcamentoTematico = getChavePrimaria();
        string comando = string.Format(@"
        DELETE FROM {0}.{1}.[pbh_OrcamentoTematicoDotacao]
         WHERE CodigoOrcamentoTematico = {2}

        DELETE FROM {0}.{1}.[pbh_OrcamentoTematico]
         WHERE CodigoOrcamentoTematico = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoOrcamentoTematico);

        DataSet ds = cDados.getDataSet(
            cDados.geraBlocoBeginTran() + Environment.NewLine +
            comando + Environment.NewLine +
            cDados.geraBlocoEndTran());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string auxiliar = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim();
            if (auxiliar == "ok")
            {
                retorno = string.Empty;
            }
            else
            {
                retorno = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        else
        {
            retorno = Resources.traducao.CadastroOrcTematicos_erro_interno__nenhum_comando_foi_executado_;
        }
        return retorno;
    }

    private string persisteEdicaoRegistro()
    {
        string codigoOrcTematico = getChavePrimaria();
        string retorno = "";
        string retornoAtualizaOrc = "";

        string[] arrayDotacoesSelecionadas = new string[gvDotacoes.GetSelectedFieldValues("Dotacao").Count];

        for (int i = 0; i < arrayDotacoesSelecionadas.Length; i++)
        {
            arrayDotacoesSelecionadas[i] = gvDotacoes.GetSelectedFieldValues("Dotacao")[i].ToString();
        }

        retornoAtualizaOrc = atualizaOrcamentoTematicoDotacao(arrayDotacoesSelecionadas, codigoOrcTematico);

        string comandoSQL = string.Format(@"
        DECLARE @DescricaoOrcamentoTematico varchar(250)
        DECLARE @CodigoOrcamentoTematico int

        SET @DescricaoOrcamentoTematico = '{2}'
        SET @CodigoOrcamentoTematico = {3}  
        
        UPDATE {0}.{1}.[pbh_OrcamentoTematico]
           SET [DescricaoOrcamentoTematico] = @DescricaoOrcamentoTematico
         WHERE CodigoOrcamentoTematico = @CodigoOrcamentoTematico ",
          cDados.getDbName(),
        cDados.getDbOwner(),
        txtDescricaoOrcamento.Text,
        codigoOrcTematico);

        DataSet ds = cDados.getDataSet(
            cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) & cDados.DataTableOk(ds.Tables[0]))
        {
            string auxiliar = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim();
            if (auxiliar == "ok")
            {
                retorno = "";
            }
            else
            {
                retorno = auxiliar;
            }
        }
        else
        {
            retorno = Resources.traducao.CadastroOrcTematicos_problema_interno__nenhum_comando_de_banco_foi_executado_;
        }
        retorno = string.IsNullOrWhiteSpace(retorno) ? retornoAtualizaOrc : retorno + Environment.NewLine + retornoAtualizaOrc;
        return retorno;
    }

    private string atualizaOrcamentoTematicoDotacao(string[] arrayDotacoesSelecionadas, string codigoOrcTematico)
    {
        DataSet ds;
        string retorno;
        string retornoExcluir = "";
        string retornoIncluir = "";

        string deletarOrcamentosTematicos = string.Format(@"
        DELETE FROM {0}.{1}.[pbh_OrcamentoTematicoDotacao]
         WHERE [CodigoEntidade] = {2} and
               [CodigoOrcamentoTematico] = {3}", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, codigoOrcTematico);
        ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            deletarOrcamentosTematicos +
            Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string auxiliar = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim();
            if (auxiliar == "ok")
            {
                retornoExcluir = string.Empty;
            }
            else
            {
                retornoExcluir = auxiliar;
            }
        }

        ds = null;
        string comandoIncluiDotNosOrcTematicos = "";
        foreach (string codigoDotacao in arrayDotacoesSelecionadas)
        {
            if (codigoDotacao != "")
            {
                // Insere um registro na tabela CarteiraProjeto
                comandoIncluiDotNosOrcTematicos += string.Format(@"
                        INSERT INTO {0}.{1}.[pbh_OrcamentoTematicoDotacao] (CodigoOrcamentoTematico, CodigoEntidade,  Dotacao)
                                                                     VALUES({2}, {3},'{4}') {5}"
                       , cDados.getDbName(), cDados.getDbOwner(), codigoOrcTematico, codigoEntidadeUsuarioResponsavel, codigoDotacao, Environment.NewLine);
            }
        }

        if (comandoIncluiDotNosOrcTematicos != string.Empty)
        {
            ds = cDados.getDataSet(
            cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoIncluiDotNosOrcTematicos +
            Environment.NewLine +
            cDados.geraBlocoEndTran());
        }


        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            string auxiliar = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim();
            if (auxiliar == "ok")
            {
                retornoIncluir = string.Empty;
            }
            else
            {
                retornoIncluir = auxiliar;
            }
        }
        retorno = string.IsNullOrEmpty(retornoIncluir) ? retornoExcluir : (retornoIncluir + Environment.NewLine + retornoExcluir);
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        string comandoSQL = string.Format(@"
        DECLARE @DescricaoOrcamentoTematico varchar(250)
        DECLARE @CodigoEntidade int

        SET @DescricaoOrcamentoTematico = '{2}'
        SET @CodigoEntidade = {3}           

        INSERT INTO {0}.{1}.[pbh_OrcamentoTematico]
              ([DescricaoOrcamentoTematico],[CodigoEntidade])           
        VALUES(@DescricaoOrcamentoTematico, @CodigoEntidade)

        set @CodigoRetorno = SCOPE_IDENTITY();",
        cDados.getDbName(),
        cDados.getDbOwner(),
        txtDescricaoOrcamento.Text,
        codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(
            cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran_ComRetorno());
        string codigoRetornoOrcamentoTematico = "";
        retorno = TrataErroDataset(ds, ref codigoRetornoOrcamentoTematico);

        string[] arrayDotacoesSelecionadas = new string[gvDotacoes.GetSelectedFieldValues("Dotacao").Count];

        for (int i = 0; i < arrayDotacoesSelecionadas.Length; i++)
        {
            arrayDotacoesSelecionadas[i] = gvDotacoes.GetSelectedFieldValues("Dotacao")[i].ToString();
        }

        ds = null;
        string comandoIncluiDotNosOrcTematicos = "";
        foreach (string codigoDotacao in arrayDotacoesSelecionadas)
        {
            if (codigoDotacao != "")
            {
                // Insere um registro na tabela CarteiraProjeto
                comandoIncluiDotNosOrcTematicos += string.Format(@"
                        INSERT INTO {0}.{1}.[pbh_OrcamentoTematicoDotacao] (CodigoOrcamentoTematico, CodigoEntidade,  Dotacao)
                                                                     VALUES({2}, {3},'{4}') {5}"
                       , cDados.getDbName(), cDados.getDbOwner(), codigoRetornoOrcamentoTematico, codigoEntidadeUsuarioResponsavel, codigoDotacao, Environment.NewLine);
            }
        }
        if (comandoIncluiDotNosOrcTematicos != "")
        {
            ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoIncluiDotNosOrcTematicos +
            Environment.NewLine +
            cDados.geraBlocoEndTran());

            retorno = TrataErroDataset(ds);
        }

        return retorno;
    }

    private string TrataErroDataset(DataSet ds, ref string codigoRetornoOrcamentoTematico)
    {
        string retorno;
        if (cDados.DataSetOk(ds) & cDados.DataTableOk(ds.Tables[0]))
        {
            string auxiliar = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim();
            if (auxiliar == "ok")
            {
                retorno = "";
                codigoRetornoOrcamentoTematico = ds.Tables[0].Rows[0][1].ToString().ToLower().Trim();
            }
            else
            {
                retorno = auxiliar;
            }
        }
        else
        {
            retorno = Resources.traducao.CadastroOrcTematicos_problema_interno__nenhum_comando_de_banco_foi_executado_;
        }

        return retorno;
    }
    private string TrataErroDataset(DataSet ds)
    {
        string retorno;
        if (cDados.DataSetOk(ds) & cDados.DataTableOk(ds.Tables[0]))
        {
            string auxiliar = ds.Tables[0].Rows[0][0].ToString().ToLower().Trim();
            if (auxiliar == "ok")
            {
                retorno = "";
            }
            else
            {
                retorno = auxiliar;
            }
        }
        else
        {
            retorno = Resources.traducao.CadastroOrcTematicos_problema_interno__nenhum_comando_de_banco_foi_executado_;
        }

        return retorno;
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void gvDotacoes_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDotacoes.FilterExpression = "";
        gvDotacoes.PageIndex = 0;
        if (e.Parameters != "")
        {
            gvDotacoes.ExpandAll();
            selecionaDotacoes();
        }
    }

    private void selecionaDotacoes()
    {
        gvDotacoes.Selection.UnselectAll();

        for (int i = 0; i < gvDotacoes.VisibleRowCount; i++)
        {
            if (gvDotacoes.GetRowValues(i, "Selecionado").ToString() == "S")
                gvDotacoes.Selection.SelectRow(i);
        }
    }
}