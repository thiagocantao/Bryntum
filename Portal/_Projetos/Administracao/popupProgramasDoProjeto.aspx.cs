using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Administracao_popupProgramasDoProjeto : System.Web.UI.Page
{
    dados cDados;

    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    public bool podeIncluir = true;
    public bool somenteLeitura = false;
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
        idProjeto = int.Parse(Request.QueryString["CP"].ToString());
        somenteLeitura = (Request.QueryString["RO"] == "S") ? true : false;
        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "EN_CadPrg");
        }

        this.Title = cDados.getNomeSistema();

        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        cDados.aplicaEstiloVisual(this.Page);
        carregaComboUnidade();
        carregaListaProjetos(idProjeto.ToString());
        
        if (!Page.IsPostBack)
        {
            carregaDadosTela();
            populaDdlProjetosToProgramas();
        }
    }

    private void carregaDadosTela()
    {
        if(idProjeto != -1)
        {
            tdProjToProg.Style.Add("display", "none");
            tdProjToProg1.Style.Add("display", "none");

        }
        else
        {
            tdProjToProg.Style.Add("display", "block");
            tdProjToProg1.Style.Add("display", "block");
            tdProjToProg1.Style.Add("width", "100%");
        }

        string comandoSQL = string.Format(@"select CodigoProjeto, NomeProjeto, CodigoUnidadeNegocio, CodigoGerenteProjeto FROM Projeto WHERE codigoprojeto = {0}", idProjeto);
        DataSet dstemp = cDados.getDataSet(comandoSQL);
        if(cDados.DataSetOk(dstemp) && cDados.DataTableOk(dstemp.Tables[0]))
        {
            txtNomePrograma.Text = dstemp.Tables[0].Rows[0]["NomeProjeto"].ToString();
            ddlGerentePrograma.Value = int.Parse(dstemp.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString());
            ddlUnidadeNegocio.Value = int.Parse(dstemp.Tables[0].Rows[0]["CodigoUnidadeNegocio"].ToString());
        }

        txtNomePrograma.ReadOnly = somenteLeitura;
        ddlGerentePrograma.ReadOnly = somenteLeitura;
        ddlUnidadeNegocio.ReadOnly = somenteLeitura;
        gvProjetos.Enabled = !somenteLeitura;
        btnSalvar.ClientVisible = !somenteLeitura;

    }

    protected void ddlGerentePrograma_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }

    }
    protected void ddlGerentePrograma_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }
    protected void gvProjetos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != "")
        {
            gvProjetos.ExpandAll();
            //carregaListaProjetos(e.Parameters);
            selecionaProjetos();
            gvProjetos.PageIndex = 0;
        }
    }

    private void selecionaProjetos()
    {
        gvProjetos.Selection.UnselectAll();

        for (int i = 0; i < gvProjetos.VisibleRowCount; i++)
        {
            if (gvProjetos.GetRowValues(i, "Selecionado").ToString() == "S")
                gvProjetos.Selection.SelectRow(i);
        }
    }

    private void carregaComboUnidade()
    {
        string where = string.Format(@" AND DataExclusao IS NULL AND IndicaUnidadeNegocioAtiva = 'S' AND CodigoEntidade = {0}", codigoEntidadeUsuarioResponsavel);

        if ((Request.QueryString["IDProjeto"] == null || Request.QueryString["IDProjeto"].ToString() == ""))
        {
            where += string.Format(@" AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, CodigoUnidadeNegocio, NULL, 'UN', 0, NULL, 'UN_IncPrj') = 1 
                        ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);
        }

        DataSet ds = cDados.getUnidade(where);
        if (cDados.DataSetOk(ds))
        {
            ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeNegocio.TextField = "NomeUnidadeNegocio";

            ddlUnidadeNegocio.DataSource = ds.Tables[0];
            ddlUnidadeNegocio.DataBind();
        }
    }

    private void carregaListaProjetos(string codigoPrograma)
    {

            string comandoSQL = string.Format(@"

        DECLARE   @l_imgProjeto   varchar(100)
                , @l_imgProcesso  varchar(100)
                , @l_imgAgil      varchar(100)

        SET @l_imgProjeto  = '<img border=''0'' src=''../../imagens/projeto.PNG'' title=''" + Resources.traducao.projeto + @"''/>';
        SET @l_imgProcesso = '<img border=''0'' src=''../../imagens/processo.PNG'' title=''" + Resources.traducao.processo + @"'' style=''width: 21px; height: 18px;'' />';
        SET @l_imgAgil     = '<img border=''0'' src=''../../imagens/agile.PNG'' title=''" + Resources.traducao.projeto__gil + @" '' style=''width: 21px; height: 18px;'' />';

        SELECT DISTINCT p.CodigoProjeto,
                        CASE WHEN tp.IndicaTipoProjeto = 'PRJ' THEN @l_imgProjeto
                             WHEN tp.IndicaTipoProjeto = 'PRC' THEN @l_imgProcesso + '/>'
                             WHEN tp.IndicaTipoProjeto = 'PRG' AND tp.IndicaProjetoAgil = 'S' THEN @l_imgAgil + '/>'
                             ELSE ''
                        END + '&nbsp;' +
                        p.NomeProjeto AS NomeProjeto, 
                        p.NomeProjeto AS NomeProjeto2,
                        'N' AS Selecionado, 1 AS ColunaAgrupamento
                  FROM {0}.{1}.Projeto p      INNER JOIN
                       {0}.{1}.TipoProjeto  tp ON (tp.CodigoTipoProjeto = p.CodigoTipoProjeto)
                 WHERE p.DataExclusao IS NULL   
                   AND p.IndicaPrograma = 'N' 
                   AND IndicaTipoProjeto <> 'SPT'
                   AND p.CodigoStatusProjeto <> 4 -- Diferente de Cancelado
                   AND p.CodigoProjeto <> {4}
                   AND p.CodigoEntidade = {3} --by Alejandro 17/07/2010
                   AND NOT EXISTS(SELECT 1 
                                  FROM {0}.{1}.LinkProjeto AS lp
                                  WHERE (lp.CodigoProjetoPai = p.CodigoProjeto OR CodigoProjetoFilho = p.CodigoProjeto) 
                                    AND TipoLink = 'PP')
                /* Inclui todos os projetos que estão em unidades onde o usuário é gerente */
                 UNION SELECT p.CodigoProjeto,
                                CASE WHEN tp.IndicaTipoProjeto = 'PRJ' THEN @l_imgProjeto
                                     WHEN tp.IndicaTipoProjeto = 'PRC' THEN @l_imgProcesso + '/>'
                                     WHEN tp.IndicaTipoProjeto = 'PRG' AND tp.IndicaProjetoAgil = 'S' THEN @l_imgAgil + '/>'
                                     ELSE ''
                                END + '&nbsp;' +
                                p.NomeProjeto AS NomeProjeto,
                               p.NomeProjeto AS NomeProjeto2,
                              'N', 
                              1
                         FROM {0}.{1}.Projeto p INNER JOIN
                              {0}.{1}.UnidadeNegocio AS UN ON p.CodigoUnidadeNegocio = UN.CodigoUnidadeNegocio    INNER JOIN
                              {0}.{1}.TipoProjeto  tp ON (tp.CodigoTipoProjeto = p.CodigoTipoProjeto)                    
                        WHERE (UN.CodigoUsuarioGerente = {2} )  
                          AND p.DataExclusao IS NULL  
                          AND p.IndicaPrograma = 'N' 
                          AND IndicaTipoProjeto <> 'SPT'
                          AND p.CodigoStatusProjeto <> 4 -- Diferente de Cancelado
                          AND p.CodigoProjeto <> {4}
                          AND p.CodigoEntidade = {3} --by Alejandro 17/07/2010
                          AND NOT EXISTS(SELECT 1 
                                         FROM {0}.{1}.LinkProjeto AS lp
                                         WHERE (lp.CodigoProjetoPai = p.CodigoProjeto OR CodigoProjetoFilho = p.CodigoProjeto) 
                                           AND TipoLink = 'PP') 
                  UNION
                SELECT DISTINCT p.CodigoProjeto,                                 
                                CASE WHEN tp.IndicaTipoProjeto = 'PRJ' THEN @l_imgProjeto
                                     WHEN tp.IndicaTipoProjeto = 'PRC' THEN @l_imgProcesso + '/>'
                                     WHEN tp.IndicaTipoProjeto = 'PRG' AND tp.IndicaProjetoAgil = 'S' THEN @l_imgAgil + '/>'
                                     ELSE ''
                                END + '&nbsp;' +
                                p.NomeProjeto AS NomeProjeto,
                               p.NomeProjeto AS NomeProjeto2,
                                'S', 
                                0
                  FROM {0}.{1}.Projeto p    INNER JOIN
                       {0}.{1}.TipoProjeto  tp ON (tp.CodigoTipoProjeto = p.CodigoTipoProjeto)    
                 WHERE p.DataExclusao IS NULL   
                   AND p.IndicaPrograma = 'N' 
                   AND IndicaTipoProjeto <> 'SPT'
                   AND p.CodigoProjeto IN (SELECT lp.CodigoProjetoFilho
                                  FROM {0}.{1}.LinkProjeto AS lp
                                  WHERE CodigoProjetoPai = {4}
                                    AND TipoLink = 'PP')
                /* Inclui todos os projetos que estão em unidades onde o usuário é gerente */
                 UNION SELECT p.CodigoProjeto,                                 
                                CASE WHEN tp.IndicaTipoProjeto = 'PRJ' THEN @l_imgProjeto
                                     WHEN tp.IndicaTipoProjeto = 'PRC' THEN @l_imgProcesso + '/>'
                                     WHEN tp.IndicaTipoProjeto = 'PRG' AND tp.IndicaProjetoAgil = 'S' THEN @l_imgAgil + '/>'
                                     ELSE ''
                                END + '&nbsp;' +
                                p.NomeProjeto AS NomeProjeto,
                               p.NomeProjeto AS NomeProjeto2,
                              'S', 
                              0
                         FROM {0}.{1}.Projeto p INNER JOIN
                              {0}.{1}.UnidadeNegocio AS UN ON p.CodigoUnidadeNegocio = UN.CodigoUnidadeNegocio    INNER JOIN
                              {0}.{1}.TipoProjeto  tp ON (tp.CodigoTipoProjeto = p.CodigoTipoProjeto)                    
                        WHERE (UN.CodigoUsuarioGerente = {2} )  
                          AND p.DataExclusao IS NULL  
                          AND p.IndicaPrograma = 'N' 
                          AND IndicaTipoProjeto <> 'SPT'
                          AND p.CodigoProjeto IN (SELECT lp.CodigoProjetoFilho
                                  FROM {0}.{1}.LinkProjeto AS lp
                                  WHERE CodigoProjetoPai = {4}
                                    AND TipoLink = 'PP')
                ORDER BY NomeProjeto2
        ",  cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel.ToString(), codigoEntidadeUsuarioResponsavel.ToString(), codigoPrograma);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvProjetos.DataSource = ds;
        gvProjetos.DataBind();
        if (!IsCallback)
        {
            selecionaProjetos();
        }
    }


    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string mesgError = "";
        string nomePrograma = txtNomePrograma.Text.Replace("'", "''");
        string codigoGerenteProjeto = ddlGerentePrograma.Value.ToString();
        string codigoUnidadeNegocio = ddlUnidadeNegocio.Value.ToString();
        string[] arrayProjetosSelecionados = new string[gvProjetos.GetSelectedFieldValues("CodigoProjeto").Count];
        bool result = false;
        for (int i = 0; i < arrayProjetosSelecionados.Length; i++)
        {
            arrayProjetosSelecionados[i] = gvProjetos.GetSelectedFieldValues("CodigoProjeto")[i].ToString();
        }


        idProjeto = (ddlTransformaProjetoEmPrograma.Value != null) ? int.Parse(ddlTransformaProjetoEmPrograma.Value.ToString()) : idProjeto;
        try
        {
            if (idProjeto != -1)
            {
                cDados.atualizaProgramaDoProjeto(nomePrograma, codigoGerenteProjeto, codigoUnidadeNegocio, codigoEntidadeUsuarioResponsavel.ToString(), codigoUsuarioResponsavel.ToString(), idProjeto.ToString());
                result = cDados.incluiProjetoSelecionados(arrayProjetosSelecionados, idProjeto.ToString());
            }
            else
            {

                string identityCodigoProjeto = "";
                result = cDados.incluiProgramasDoProjeto(nomePrograma, codigoGerenteProjeto, codigoUnidadeNegocio.ToString(), codigoEntidadeUsuarioResponsavel.ToString(), codigoUsuarioResponsavel.ToString(), ref identityCodigoProjeto, ref mesgError);
                result = cDados.incluiProjetoSelecionados(arrayProjetosSelecionados, identityCodigoProjeto);
            }
        }
        catch (Exception ex)
        {
            mesgError = ex.Message;
        }

        return mesgError;
    }


    protected void callbackTela_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";
        
        string msgErro = persisteEdicaoRegistro();

        ((ASPxCallback)source).JSProperties["cpSucesso"] = (idProjeto > -1) ? "Programa alterado com sucesso!" : "Programa incluído com sucesso!";
        ((ASPxCallback)source).JSProperties["cpErro"] = msgErro;
    }

    public void populaDdlProjetosToProgramas()
    {
        
        string comandoSQL = string.Format(@"
                SELECT DISTINCT TOP 250 cp.CodigoProjeto, cp.CodigoMSProject, cp.NomeProjeto, cp.CodigoGerenteProjeto, 
                                un.CodigoUnidadeNegocio, un.SiglaUnidadeNegocio, g.NomeUsuario AS NomeGerente 
                FROM {0}.{1}.Projeto CP INNER JOIN 
                     {0}.{1}.UnidadeNegocio AS UN ON CP.CodigoUnidadeNegocio = UN.CodigoUnidadeNegocio INNER JOIN
                     {0}.{1}.Usuario g ON g.CodigoUsuario = cp.CodigoGerenteProjeto 
                WHERE CP.DataExclusao IS NULL 
                    AND CP.IndicaPrograma = 'N'
                    AND CP.CodigoStatusProjeto <> 4
                    AND CP.CodigoEntidade = {2}
                    AND NOT EXISTS(SELECT 1 
                                     FROM {0}.{1}.LinkProjeto 
                                    WHERE (CodigoProjetoPai = cp.CodigoProjeto OR CodigoProjetoFilho = cp.CodigoProjeto) 
                                      AND TipoLink = 'PP')
                ORDER BY cp.NomeProjeto
                ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel);
        
        DataSet dsProjetosProgramas = cDados.getDataSet(comandoSQL);

        ddlTransformaProjetoEmPrograma.TextField = "NomeProjeto";
        ddlTransformaProjetoEmPrograma.ValueField = "CodigoProjeto";
        ddlTransformaProjetoEmPrograma.DataSource = dsProjetosProgramas;
        ddlTransformaProjetoEmPrograma.DataBind();
    }

    protected void callbackGerenteProjeto_Callback(object sender, CallbackEventArgsBase e)
    {
        ((ASPxCallbackPanel)sender).JSProperties["cpCodigoGerente"] = -1;
        ((ASPxCallbackPanel)sender).JSProperties["cpNomeGerente"] = "";

        idProjeto = int.Parse(e.Parameter);

        string comandoSQL = string.Format(@"SELECT TOP 1 p.CodigoGerenteProjeto, u.NomeUsuario  
                                            FROM Projeto p 
                                            INNER JOIN Usuario u on (u.CodigoUsuario = p.CodigoGerenteProjeto)
                                            WHERE CodigoProjeto = {0} ", idProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ((ASPxCallbackPanel)sender).JSProperties["cpCodigoGerente"] = ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString();
            ((ASPxCallbackPanel)sender).JSProperties["cpNomeGerente"] = ds.Tables[0].Rows[0]["NomeUsuario"].ToString();
        }
    }

    protected void ddlTransformaProjetoEmPrograma_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        int outcodigo = -1;
        
        if(e.Value != null && e.Value.ToString() != "" && int.TryParse(e.Value.ToString(), out outcodigo) == true)
        {
            string comandoSQL = string.Format(@"
                SELECT DISTINCT top 250 cp.CodigoProjeto, cp.CodigoMSProject, cp.NomeProjeto, cp.CodigoGerenteProjeto, 
                                un.CodigoUnidadeNegocio, un.SiglaUnidadeNegocio, g.NomeUsuario AS NomeGerente 
                FROM {0}.{1}.Projeto CP INNER JOIN 
                     {0}.{1}.UnidadeNegocio AS UN ON CP.CodigoUnidadeNegocio = UN.CodigoUnidadeNegocio INNER JOIN
                     {0}.{1}.Usuario g ON g.CodigoUsuario = cp.CodigoGerenteProjeto 
                WHERE CP.DataExclusao IS NULL 
                    AND CP.IndicaPrograma = 'N'
                    AND CP.CodigoStatusProjeto <> 4
                    AND CP.CodigoEntidade = {2} and cp.CodigoProjeto = {3}
                    AND NOT EXISTS(SELECT 1 
                                     FROM {0}.{1}.LinkProjeto 
                                    WHERE (CodigoProjetoPai = cp.CodigoProjeto OR CodigoProjetoFilho = cp.CodigoProjeto) 
                                      AND TipoLink = 'PP')
                ORDER BY cp.NomeProjeto
                ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, outcodigo);
            DataSet dsProjetosProgramas = cDados.getDataSet(comandoSQL);
            ddlTransformaProjetoEmPrograma.TextField = "NomeProjeto";
            ddlTransformaProjetoEmPrograma.ValueField = "CodigoProjeto";
            ddlTransformaProjetoEmPrograma.DataSource = dsProjetosProgramas;
            ddlTransformaProjetoEmPrograma.DataBind();
        }
    }

    protected void ddlTransformaProjetoEmPrograma_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        if(e.Filter.Length > 0)
        {
            string comandoSQL = string.Format(@"
                SELECT DISTINCT top 250 cp.CodigoProjeto, cp.CodigoMSProject, cp.NomeProjeto, cp.CodigoGerenteProjeto, 
                                un.CodigoUnidadeNegocio, un.SiglaUnidadeNegocio, g.NomeUsuario AS NomeGerente 
                FROM {0}.{1}.Projeto CP INNER JOIN 
                     {0}.{1}.UnidadeNegocio AS UN ON CP.CodigoUnidadeNegocio = UN.CodigoUnidadeNegocio INNER JOIN
                     {0}.{1}.Usuario g ON g.CodigoUsuario = cp.CodigoGerenteProjeto 
                WHERE CP.DataExclusao IS NULL 
                    AND CP.IndicaPrograma = 'N'
                    AND CP.CodigoStatusProjeto <> 4
                    AND CP.CodigoEntidade = {2} and cp.NomeProjeto like '%{3}%'
                    AND NOT EXISTS(SELECT 1 
                                     FROM {0}.{1}.LinkProjeto 
                                    WHERE (CodigoProjetoPai = cp.CodigoProjeto OR CodigoProjetoFilho = cp.CodigoProjeto) 
                                      AND TipoLink = 'PP')
                ORDER BY cp.NomeProjeto
                ", cDados.getDbName(), cDados.getDbOwner(), codigoEntidadeUsuarioResponsavel, e.Filter);

            DataSet dsProjetosProgramas = cDados.getDataSet(comandoSQL);

            ddlTransformaProjetoEmPrograma.TextField = "NomeProjeto";
            ddlTransformaProjetoEmPrograma.ValueField = "CodigoProjeto";
            ddlTransformaProjetoEmPrograma.DataSource = dsProjetosProgramas;
            ddlTransformaProjetoEmPrograma.DataBind();
        }
    }
}