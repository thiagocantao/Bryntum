using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class administracao_ListaMedicoes : System.Web.UI.Page
{
    dados cDados;
    private string dbName;
    private string dbOwner;

    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;
    public string alturaDiv = "";

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

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "NULL", "EN", 0, "NULL", "EN_ConsMedicao");
        }

        dsMedicao.ConnectionString = cDados.classeDados.getStringConexao();
        dsItensMedicao.ConnectionString = cDados.classeDados.getStringConexao();
//        cbMedicoesPendentes.Checked = true;

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        gvContratos.JSProperties["cp_Msg"] = "";
        gvContratos.JSProperties["cp_Status"] = "1";
        
      //  dsMedicao.SelectParameters[0].DefaultValue = codigoEntidadeUsuarioResponsavel.ToString();
        cDados.aplicaEstiloVisual(Page);//Ok
        carrega_gvMedicao();               //Ok
        carrega_tlItensMedicao();
        carrega_gvContratos();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        gvHistorico.Settings.ShowFilterRow = false;
        gvHistorico.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvHistorico.SettingsBehavior.AllowSort = false;

        gvImpostos.Settings.ShowFilterRow = false;
        gvImpostos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
        gvImpostos.SettingsBehavior.AllowSort = false;

        gvMedicao.JSProperties["cp_Msg"] = "";
    }

    #region VARIOS

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 195);
        if (altura > 0)
        {
            gvMedicao.Settings.VerticalScrollableHeight = altura - 300;
        }
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ASPxListbox.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ListaMedicoes.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ASPxListbox", "ListaMedicoes"));
    }

    #endregion

    #region POPULA GRIDS

    private void carrega_gvMedicao()
    {
        
        string sComandoSql = string.Format(@"SELECT m.CodigoMedicao, 
                                                   case when m.MesMedicao = 1 then 'Jan/'
                                                        when m.MesMedicao = 2 then 'Fev/'
                                                        when m.MesMedicao = 3 then 'Mar/'
                                                        when m.MesMedicao = 4 then 'Abr/'
                                                        when m.MesMedicao = 5 then 'Mai/'
                                                        when m.MesMedicao = 6 then 'Jun/'
                                                        when m.MesMedicao = 7 then 'Jul/'
                                                        when m.MesMedicao = 8 then 'Ago/'
                                                        when m.MesMedicao = 9 then 'Set/'
                                                        when m.MesMedicao = 10 then 'Out/'
                                                        when m.MesMedicao = 11 then 'Nov/'
                                                        when m.MesMedicao = 12 then 'Dez/'
                                                    end + cast(m.AnoMedicao as varchar)                as Mes_Ano, 
                                                    c.CodigoContrato                                   as CodigoContrato,
                                                    c.NumeroContrato                                   as Contrato,  
                                                    sm.DescricaoStatusMedicao                          as Status, 
                                                    ISNULL(p.NomeProjeto, c.DescricaoObjetoContrato)   as Projeto, 
                                                    pes.NomePessoa                                     as Fornecedor,
                                                    c.DescricaoObjetoContrato                          as ObjetoContrato,
                                                    CONVERT(varchar, c.DataInicio, 103)                as DataInicio, 
                                                    CONVERT(varchar, c.DataTermino, 103)               as DataTermino, 
                                                    c.ValorContrato, 
                                                    CONVERT(varchar, c.DataBaseReajuste, 103)          as DataBaseReajuste,
                                                    dbo.f_GetValorTotalMedicao(m.CodigoMedicao)        as ValorTotalMedicao,
                                                    p.CodigoProjeto,
                                                    dbo.f_GetValorMedidoAteMes(m.CodigoContrato, m.CodigoProjeto, m.AnoMedicao, m.MesMedicao ) as ValorMedidoAteMes,
                                                    sm.IniciaisStatus
                                            FROM {0}.{1}.Medicao m inner join 
                                                 {0}.{1}.StatusMedicao sm on (sm.CodigoStatusMedicao = m.CodigoStatusMedicao) inner join
                                                 {0}.{1}.Contrato c on ( c.CodigoContrato = m.CodigoContrato 
                                                                         and c.tipoPessoa = 'F' ) left join
                                                 {0}.{1}.Pessoa  pes on ( pes.CodigoPessoa = c.CodigoPessoaContratada )  LEFT JOIN 
                                                 {0}.{1}.[PessoaEntidade] AS [pe] ON (
			                                        pe.[CodigoPessoa] = c.[CodigoPessoaContratada]
			                                        AND pe.codigoEntidade = c.codigoEntidade
                                                    --AND pe.IndicaFornecedor = 'S'
			                                        )   left join
                                                 {0}.{1}.Projeto p on ( p.CodigoProjeto = m.CodigoProjeto ) left join
                                                 {0}.{1}.Obra o on ( o.CodigoContrato = c.CodigoContrato and
                                                                     o.DataExclusao is null )
                                            ORDER BY m.AnoMedicao, m.MesMedicao, c.NumeroContrato ", dbName, dbOwner);
        DataSet ds = cDados.getDataSet(sComandoSql);
        DataSet dsMedicao = new DataSet();
        if (cDados.DataSetOk(ds))
        {
            dsMedicao = ds.Clone();
            foreach (DataRow linha in ds.Tables[0].Rows)
            {
                if ( (VerificaPermissaoAlterarStatus(linha["CodigoMedicao"] + "", int.Parse("0" + linha["CodigoContrato"])) == true) || (cbMedicoesPendentes.Checked == false) )
                    dsMedicao.Tables[0].ImportRow(linha);
            }
            gvMedicao.DataSource = dsMedicao;
            gvMedicao.DataBind();
        }
    }

    private void carrega_tlItensMedicao()
    {
        int nCodMedicao = -1;
        if (gvMedicao.FocusedRowIndex >= 0)
        {
            if (gvMedicao.GetRowValues(gvMedicao.FocusedRowIndex, "CodigoMedicao") != null)
                nCodMedicao = int.Parse(gvMedicao.GetRowValues(gvMedicao.FocusedRowIndex, "CodigoMedicao").ToString());
        }

        // -------------------------------------------------------
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("CodigoItemMedicaoContrato");
        dtResult.Columns.Add("CodigoTarefaPai");
        dtResult.Columns.Add("DescricaoItem");
        dtResult.Columns.Add("QuantidadePrevistaTotal", System.Type.GetType("System.Double"));
        dtResult.Columns.Add("UnidadeMedidaItem");
        dtResult.Columns.Add("ValorUnitarioItem", System.Type.GetType("System.Double"));
        dtResult.Columns.Add("ValorMedidoMes", System.Type.GetType("System.Double"));
        dtResult.Columns.Add("ValorPrevistoTotal", System.Type.GetType("System.Double"));
        dtResult.Columns.Add("QuantidadeMedidaMes", System.Type.GetType("System.Double"));
        // -------------------------------------------------------
        tlItensMedicao.ClearNodes();
        tlItensMedicao.DataSource = null;

        string sComandoSql = string.Format(@"SELECT CodigoAtribuicao, 
                                                    CodigoItemMedicaoContrato, 
                                                    ISNULL(CodigoTarefaPai, -88) as CodigoTarefaPai, 
                                                    IndicaItemPrecoGlobal,
                                                    SUBSTRING(DescricaoItem, 0,90) + CASE WHEN LEN(DescricaoItem) > 90 THEN '...' ELSE '' END as DescricaoItem,
                                                    QuantidadePrevistaTotal, 
                                                    UnidadeMedidaItem, 
                                                    ValorUnitarioItem, 
                                                    ValorMedidoMes,
                                                    ValorPrevistoTotal, 
                                                    QuantidadeMedidaMes
                                             FROM {0}.{1}.f_GetItensMedicao({2})
                                             ORDER BY NumeroOrdem", dbName, dbOwner, nCodMedicao);
        DataSet dsItensMedicao = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(dsItensMedicao) && cDados.DataTableOk(dsItensMedicao.Tables[0]))
        {
            foreach (DataRow dr in dsItensMedicao.Tables[0].Rows)
            {
                DataRow novaLinha = dtResult.NewRow();
                novaLinha["CodigoItemMedicaoContrato"] = dr["CodigoItemMedicaoContrato"];
                novaLinha["CodigoTarefaPai"] = dr["CodigoTarefaPai"];
                novaLinha["DescricaoItem"] = dr["DescricaoItem"];
                novaLinha["QuantidadePrevistaTotal"] = dr["QuantidadePrevistaTotal"];
                novaLinha["UnidadeMedidaItem"] = dr["UnidadeMedidaItem"];
                novaLinha["ValorUnitarioItem"] = dr["ValorUnitarioItem"];
                novaLinha["ValorMedidoMes"] = dr["ValorMedidoMes"];
                novaLinha["ValorPrevistoTotal"] = dr["ValorPrevistoTotal"];
                novaLinha["QuantidadeMedidaMes"] = dr["QuantidadeMedidaMes"];
                dtResult.Rows.Add(novaLinha);
            }
            tlItensMedicao.DataSource = dtResult;
            tlItensMedicao.DataBind();
        }

    }

    private void carrega_gvHistorico(string codMedicao)
    {
        string sComandoSql = string.Format(@"SELECT hm.DataHistorico,
                                                    sm1.DescricaoStatusMedicao as DescricaoStatusAnterior,
                                                    sm.DescricaoStatusMedicao,
                                                    LEFT(us.NomeUsuario, 15) as NomeUsuario,
                                                    hm.ComentarioMudancaStatus
                                              FROM {0}.{1}.HistoricoMedicao hm inner join 
                                                   {0}.{1}.StatusMedicao sm on (sm.CodigoStatusMedicao = hm.CodigoStatusMedicaoAtual) inner join
                                                   {0}.{1}.StatusMedicao sm1 on (sm1.CodigoStatusMedicao = hm.CodigoStatusMedicaoAnterior) inner join
                                                   {0}.{1}.Usuario  us on ( us.CodigoUsuario = hm.CodigoUsuarioMudancaStatus )
                                             WHERE hm.CodigoMedicao = {2}
                                            ORDER BY hm.DataHistorico ", dbName, dbOwner, codMedicao);
        DataSet dsHistMedicao = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(dsHistMedicao))
        {
            gvHistorico.DataSource = dsHistMedicao;
            gvHistorico.DataBind();
        }
    }


    private void carrega_gvImpostos(string codMedicao)
    {
        string sComandoSql = string.Format(@"SELECT 1, CodigoMedicao, 'Faturamento Direto (-)' AS Descricao, null AS Aliquota, ValorFaturamentoDireto AS Valor
                                                FROM {0}.{1}.Medicao
                                                WHERE CodigoMedicao = {2}
                                                UNION
                                                SELECT 2, rv.CodigoMedicao, 
                                                       acpc.DescricaoAcessorio + CASE WHEN RTRIM(acpc.Tipo) = 'Desconto' THEN ' (-)'
                                                                                      WHEN RTRIM(acpc.Tipo) = 'Acréscimo' THEN ' (+)'
                                                                                      ELSE ''
                                                                                 END,
                                                       acpc.Aliquota, rv.ValorAcessorio
                                                FROM {0}.{1}.RetornoValoresAcessoriosMedicao rv inner join
                                                     {0}.{1}.AcessorioCalculoPagamentoContrato acpc on ( rv.CodigoAcessorioContrato = acpc.CodigoAcessorioContrato )
                                                WHERE CodigoMedicao = {2}     
                                                UNION
                                                SELECT 3, CodigoMedicao, 'Adiantamento (-)', null, ValorAdiantamento
                                                FROM {0}.{1}.Medicao
                                                WHERE CodigoMedicao = {2}
                                                UNION
                                                SELECT 4, CodigoMedicao, 'Glosa (-)', null, ValorGlosa
                                                FROM {0}.{1}.Medicao
                                                WHERE CodigoMedicao = {2}
                                                ORDER BY 1 ", dbName, dbOwner, codMedicao);
        DataSet dsImpostos = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(dsImpostos))
        {
            gvImpostos.DataSource = dsImpostos;
            gvImpostos.DataBind();
        }
    }
    private void carrega_gvContratos()
    {

        string sComandoSql = string.Format(@"SELECT CodigoContrato,
                                                    NumeroContrato,  
                                                    NomeProjeto, 
                                                    NomePessoaResp as Fornecedor,
                                                    CodigoProjeto
                                            FROM {0}.{1}.f_GetContratosMedicao({2}, {3})
                                            ORDER BY NumeroContrato ", dbName, dbOwner, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);
        DataSet ds = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(ds))
        {
            gvContratos.DataSource = ds;
            gvContratos.DataBind();
        }
    }


    #endregion
    
    #region CONTROLE DE ACESSO

    protected void gvMedicao_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex >= 0)
        {
            try
            {
                if (e.ButtonID == "btnEditar")
                {
                    string codMedicao = gvMedicao.GetRowValues(e.VisibleIndex, "CodigoMedicao").ToString();
                    int codContrato = int.Parse(gvMedicao.GetRowValues(e.VisibleIndex, "CodigoContrato").ToString());
                    if (VerificaPermissaoAlterarStatus(codMedicao, codContrato) == false)
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
                else if (e.ButtonID == "btnExcluir")
                {
                    string iniciaisStatus = gvMedicao.GetRowValues(e.VisibleIndex, "IniciaisStatus").ToString();

                    if ( iniciaisStatus != "REGISTRO" && iniciaisStatus != "MEDICAO_INEXISTENTE"  && !cDados.PerfilAdministrador(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel))
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
            catch { }
        }
    }

    private bool VerificaPermissaoAlterarStatus(string codMedicao, int codContrato)
    {
        bool bRetorno = false;
        string sPermissaoRequerida;

        if (codMedicao != "")
        {
            string sComandoSql = string.Format(@"SELECT PermissaoRequerida
                                             FROM {0}.{1}.Medicao as m INNER JOIN
                                                  {0}.{1}.RelacaoStatusMedicao as rs ON ( rs.CodigoStatusMedicaoDe = m.CodigoStatusMedicao )
                                             WHERE m.CodigoMedicao = {2}"
                                              , dbName, dbOwner, codMedicao);
            DataSet ds = cDados.getDataSet(sComandoSql);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                sPermissaoRequerida = ds.Tables[0].Rows[0]["PermissaoRequerida"].ToString();
                bRetorno = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codContrato, "NULL", "CT", 0, "NULL", sPermissaoRequerida);
            }
        }
        return bRetorno;
    }

    #endregion

    #region Mudança de Status

    private void DeterminaBotoesMudancaStatus(string codMedicao, int codContrato)
    {
        
        string sPermissaoRequerida;
        string idBotao;

        pnBotoes.Controls.Clear();

        Table tb = new Table();

        TableRow tr = new TableRow();        

        string sComandoSql = string.Format(@"SELECT CodigoStatusMedicaoDe, CodigoStatusMedicaoPara, NomeAcaoStatus, PermissaoRequerida
                                             FROM {0}.{1}.Medicao as m INNER JOIN
                                                  {0}.{1}.RelacaoStatusMedicao as rs ON ( rs.CodigoStatusMedicaoDe = m.CodigoStatusMedicao )
                                             WHERE m.CodigoMedicao = {2}"
                                          , dbName, dbOwner, codMedicao);
        DataSet ds = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            foreach (DataRow linha in ds.Tables[0].Rows)
            {
                sPermissaoRequerida = linha["PermissaoRequerida"].ToString();
                if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codContrato, "NULL", "CT", 0, "NULL", sPermissaoRequerida) == true)
                {

                    idBotao = "id_" + linha["CodigoStatusMedicaoDe"].ToString() + "_" + linha["CodigoStatusMedicaoPara"].ToString() + "_" + codMedicao.ToString();

                    ASPxButton bt = new ASPxButton();
                    bt.Text = linha["NomeAcaoStatus"].ToString();
                    bt.ID = idBotao;
                    bt.AutoPostBack = false;
                    bt.ClientSideEvents.Click = "function (s, e) { callBackSalvar.PerformCallback('" + idBotao + "'); }";
                    bt.Font.Name = "Verdana";
                    bt.Font.Size = new FontUnit("8pt");
                    TableCell td = new TableCell();
                    td.Style.Add("padding-right", "5px");

                    td.Controls.Add(bt);
                    tr.Cells.Add(td);
                }
            }
        }

        tb.Rows.Add(tr);

        pnBotoes.Controls.Add(tb);
    }

    protected void pnGeral_Callback(object sender, CallbackEventArgsBase e)
    {

        string codMedicao = gvMedicao.GetRowValues(int.Parse(e.Parameter), "CodigoMedicao").ToString();
        int codContrato = int.Parse(gvMedicao.GetRowValues(int.Parse(e.Parameter), "CodigoContrato").ToString());
        string nrContrato = gvMedicao.GetRowValues(int.Parse(e.Parameter), "Contrato").ToString();
        string mesAno = gvMedicao.GetRowValues(int.Parse(e.Parameter), "Mes_Ano").ToString();
        
        pnGeral.JSProperties["cp_CodigoMedicao"] = codMedicao;
        pnGeral.JSProperties["cp_header"] = "Contrato: " + nrContrato + "  -  Medição: " + mesAno;   

        string sComentarioAux = "";
        lbComentarioAnterior.Text = "Comentário Anterior:";
        lbComentarioAtual.Text = "Comentário Atual:";
        int nQtdComentario;
        int nAuxContaComentario = 0;
        mmComentarioAnterior.Text = "";
        mmComentarioAtual.Text = "";

        DeterminaBotoesMudancaStatus(codMedicao, codContrato);

        string sComandoSql = string.Format(@"SELECT CONVERT(varchar, hm.dataHistorico, 103 ) as DataHistorico, 
                                                    ISNULL( u.NomeUsuario, CAST(hm.CodigoUsuarioMudancaStatus as varchar) ) as Usuario, 
                                                    hm.ComentarioMudancaStatus, sm.DescricaoStatusMedicao
                                             FROM {0}.{1}.Medicao as m INNER JOIN
                                                  {0}.{1}.HistoricoMedicao as hm ON ( hm.CodigoMedicao = m.CodigoMedicao AND
                                                                                      hm.CodigoStatusMedicaoAtual = m.CodigoStatusMedicao ) LEFT JOIN
                                                  {0}.{1}.StatusMedicao as sm ON ( sm.CodigoStatusMedicao = hm.CodigoStatusMedicaoAnterior ) LEFT JOIN
                                                  {0}.{1}.Usuario as u ON ( u.CodigoUsuario = hm.CodigoUsuarioMudancaStatus )
                                             WHERE m.CodigoMedicao = {2}
                                             ORDER BY hm.DataHistorico"
                                          , dbName, dbOwner, codMedicao);
        DataSet ds = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            nQtdComentario = ds.Tables[0].Rows.Count;
            foreach (DataRow linha in ds.Tables[0].Rows)
            {
                nAuxContaComentario += 1;
                sComentarioAux += linha["DataHistorico"].ToString() + " - " + linha["Usuario"] + "\r";
                sComentarioAux += "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - \r";
                sComentarioAux += linha["ComentarioMudancaStatus"].ToString() + "\r";
                if (nAuxContaComentario < nQtdComentario)
                    sComentarioAux += "\r";
            }
            mmComentarioAnterior.Text = sComentarioAux;
        }
    }

    #endregion
    
    protected void gvMedicao_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        gvMedicao.JSProperties["cp_Msg"] = "";
        gvMedicao.JSProperties["cp_Erro"] = "";

        if (e.Parameters != "")
        {
            int registrosAfetados = 0;
            string codigoMedicao = gvMedicao.GetRowValues(int.Parse(e.Parameters), "CodigoMedicao").ToString();

            try
            {

                string sComandoSql = string.Format(@"

                                                      DELETE FROM {0}.{1}.HistoricoMedicao
                                                      WHERE CodigoMedicao = {2};

                                                      DELETE FROM {0}.{1}.ItemMedicao
                                                      WHERE CodigoMedicao = {2};

                                                      DELETE FROM {0}.{1}.ParcelaContrato
                                                      WHERE CodigoMedicao = {2};

                                                      DELETE FROM {0}.{1}.RetornoValoresAcessoriosMedicao
                                                      WHERE CodigoMedicao = {2};

                                                      DELETE FROM {0}.{1}.Medicao
                                                      WHERE CodigoMedicao = {2};
                                                
                                                ", dbName, dbOwner, codigoMedicao);

                cDados.execSQL(sComandoSql, ref registrosAfetados);

                gvMedicao.JSProperties["cp_Msg"] = "Medição excluída com sucesso!";
                carrega_gvMedicao();

            }
            catch (Exception ex)
            {
                gvMedicao.JSProperties["cp_Erro"] = "Erro ao excluir a medição" + Environment.NewLine + ex.Message;
            }
        }        
    }

    protected void callBackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        int registrosAfetados = 0;

        string idBotao = e.Parameter;
        string statusAnterior = idBotao.Split('_')[1];
        string statusAtual = idBotao.Split('_')[2];
        string codMedicao = idBotao.Split('_')[3];
        string sComentario = mmComentarioAtual.Text;

        try
        {

            string sComandoSql = string.Format(@"UPDATE {0}.{1}.Medicao
                                             SET CodigoStatusMedicao = {2}
                                             WHERE CodigoMedicao = {3} 
                                            ", dbName, dbOwner, statusAtual, codMedicao);
            cDados.execSQL(sComandoSql, ref registrosAfetados);

            if (registrosAfetados == 1)
            {
                sComandoSql = string.Format(@"INSERT INTO {0}.{1}.HistoricoMedicao
                                       (codigoMedicao, dataHistorico, codigoStatusMedicaoAnterior, codigoStatusMedicaoAtual, 
                                        codigoUsuarioMudancaStatus, ComentarioMudancaStatus )
                                      VALUES
                                       ( {2}, GETDATE(), {3}, {4}, {5}, '{6}' )
                                     ", dbName, dbOwner, codMedicao, statusAnterior, statusAtual, codigoUsuarioResponsavel, sComentario);
                cDados.execSQL(sComandoSql, ref registrosAfetados);
            }

            callBackSalvar.JSProperties["cp_Msg"] = "Sucesso!";
            callBackSalvar.JSProperties["cp_Status"] = "1";
        }
        catch {

            callBackSalvar.JSProperties["cp_Msg"] = "Erro ao mudar status!";
            callBackSalvar.JSProperties["cp_Status"] = "0";
        }
    }

    protected void gvMedicao_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void gvHistorico_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ComentarioMudancaStatus" && e.CellValue != null && e.CellValue.ToString().Length > 20)
        {
            string texto = e.CellValue.ToString().Replace("\"", "&quot;").Replace(Environment.NewLine, "<br>");

            texto = texto.Replace("\n", "<br>");
            e.Cell.Attributes["id"] = "cel_" + e.VisibleIndex;
            e.Cell.Attributes["onmouseover"] = "getToolTip(\"" + texto + "\", this.id)";// e.CellValue.ToString();
            e.Cell.Attributes["onmouseout"] = "escondeToolTip()";// e.CellValue.ToString();
            e.Cell.Text = e.CellValue.ToString().Substring(0, 19) + "...";
        }

    }

    protected void gvHistorico_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string codMedicao = gvMedicao.GetRowValues(int.Parse(e.Parameters), "CodigoMedicao").ToString();
        carrega_gvHistorico(codMedicao);
        carrega_gvImpostos(codMedicao);
    }

    protected void gvContratos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string mes, ano;
        string msg = "";
        string status = "1";


        int nQtdMedicao = 0;


        if (e.Parameters.ToString() != "" && e.Parameters.ToString().Split(';')[0] == "Verificar")
        {            
            int indexLinha = int.Parse(e.Parameters.ToString().Split(';')[1]);
            if (gvContratos.Selection.IsRowSelected(indexLinha) == true)
            {
                string codigoContrato, codigoProjeto, numeroContrato;
                codigoContrato = gvContratos.GetRowValues(indexLinha, "CodigoContrato").ToString();
                codigoProjeto = gvContratos.GetRowValues(indexLinha, "CodigoProjeto").ToString();
                numeroContrato = gvContratos.GetRowValues(indexLinha, "NumeroContrato").ToString();
                mes = tbMes.Text;
                ano = tbAno.Text;

                if ((mes.Trim().Length > 0) && (ano.Trim().Length > 0))
                    nQtdMedicao = podeSalvarContratoMedicao(mes, ano, codigoContrato, codigoProjeto);

                if (nQtdMedicao == 1)
                {
                    msg = "Já existe uma medição para o Contrato nº " + numeroContrato + " no mês " + mes + "/" + ano + ". \n";
                    gvContratos.JSProperties["cp_Msg"] = msg;
                    gvContratos.JSProperties["cp_Status"] = "1";
                    gvContratos.Selection.UnselectRow(indexLinha);
                    return;
                }
                else if (nQtdMedicao > 1)
                {
                    msg = "Existem medições para o Contrato nº " + numeroContrato + " posteriores ao mês " + mes + "/" + ano + ". \n ";
                    gvContratos.JSProperties["cp_Msg"] = msg;
                    gvContratos.JSProperties["cp_Status"] = "1";
                    gvContratos.Selection.UnselectRow(indexLinha);
                    return;
                }


            }
        }

        if (e.Parameters.ToString() == "S")
        {

            bool retorno = true;

            mes = tbMes.Text;
            ano = tbAno.Text;

            if (gvContratos.Selection.Count == 0)
            {
                msg += "Selecione pelo menos um contrato para gerar a medição.\n";
                status = "0";
            }

            if ((mes.Trim().Length == 0) || (int.Parse(mes) < 1) || int.Parse(mes) > 12)
            {
                msg += "Informe corretamente o mês da medição que deseja incluir.\n";
                status = "0";
            }

            if ((ano.Trim().Length == 0) || (int.Parse(ano) < 2010) || int.Parse(ano) > 2045)
            {
                msg += "Informe corretamente o ano da medição que deseja incluir.\n";
                status = "0";
            }

            if (status == "1")
            {
                for (int i = 0; i < gvContratos.Selection.Count; i++)
                {
                    string codigoContrato, codigoProjeto, numeroContrato;
                    codigoContrato = gvContratos.GetSelectedFieldValues("CodigoContrato")[i].ToString();
                    codigoProjeto = gvContratos.GetSelectedFieldValues("CodigoProjeto")[i].ToString();
                    numeroContrato = gvContratos.GetSelectedFieldValues("NumeroContrato")[i].ToString();

                    nQtdMedicao = podeSalvarContratoMedicao(mes, ano, codigoContrato, codigoProjeto);

                    if (nQtdMedicao == 0)
                    {
                        retorno = salvarContratoMedicao(mes, ano, codigoContrato, codigoProjeto);
                    }
                    else
                    {
                        if (nQtdMedicao == 1) { 
                            msg += "Já existe uma medição para o Contrato nº " + numeroContrato + " no mês " + mes + "/" + ano + ". \n";
                            status = "0";
                        } else if (nQtdMedicao > 1)
                        {
                            msg += "Existem medições para o Contrato nº " + numeroContrato + " posteriores ao mês " + mes + "/" + ano + ". \n";
                            status = "0";
                        }

                    }

                    if (retorno == false)
                    {
                        msg += "Erro ao gravar a medição para o Contrato nº " + numeroContrato + ".\n";
                        status = "0";
                    }
                }
            }

            if (status == "1")
            {
                if (gvContratos.Selection.Count > 1)
                    gvContratos.JSProperties["cp_Msg"] = "As medições destes contratos foram geradas.Utilize o aplicativo do Boletim de Medição para registrar os dados das medições.";
                else
                    gvContratos.JSProperties["cp_Msg"] = "A medição deste contrato foi gerada.Utilize o aplicativo do Boletim de Medição para registrar os dados da medição.";
                gvContratos.JSProperties["cp_Status"] = "1";
                gvContratos.Selection.UnselectAll();
            }
            else
            {
                gvContratos.JSProperties["cp_Msg"] = msg;
                gvContratos.JSProperties["cp_Status"] = status;
            }
        }

        carrega_gvContratos();
        
    }

    private int podeSalvarContratoMedicao(string mes, string ano, string codigoContrato, string codigoProjeto)
    {
        int qtdReg = 0;

        codigoProjeto = codigoProjeto + "";
        if (codigoProjeto.Length == 0)
            codigoProjeto = "null";

        string sComandoSql = string.Format(@"SELECT count(1) qtdMedicao
                                            FROM {0}.{1}.Medicao
                                            WHERE AnoMedicao = {2}
                                            AND MesMedicao = {3}
                                            AND CodigoContrato = {4}
                                            AND ISNULL(CodigoProjeto,0) = ISNULL( {5}, 0 ) ", dbName, dbOwner, ano, mes, codigoContrato, codigoProjeto);
        DataSet ds = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            qtdReg = int.Parse(ds.Tables[0].Rows[0]["qtdMedicao"].ToString());
            if (qtdReg == 0)
            {

                sComandoSql = string.Format(@"SELECT count(1) qtdMedicao
                                            FROM {0}.{1}.Medicao m
                                            WHERE CodigoContrato = {4}
                                            AND ISNULL(CodigoProjeto,0) = ISNULL( {5}, 0 )
                                            AND CAST(m.AnoMedicao AS VARCHAR) + case when m.MesMedicao < 10 then '0' else '' end + CAST(m.MesMedicao AS VARCHAR) >
                                                CAST({2} AS VARCHAR) + case when {3} < 10 then '0' else '' end + CAST({3} AS VARCHAR)  ", dbName, dbOwner, ano, mes, codigoContrato, codigoProjeto);
                ds = cDados.getDataSet(sComandoSql);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    qtdReg = int.Parse(ds.Tables[0].Rows[0]["qtdMedicao"].ToString());
                    if (qtdReg > 0)
                    {
                        qtdReg++;
                    }
                }
            }
        }
        return qtdReg;
    }


    private bool salvarContratoMedicao(string mes, string ano, string codigoContrato, string codigoProjeto)
    {
        string sComandoSql = string.Format(@"BEGIN
                                                DECLARE   @l_Ret                  int
                                                        , @in_comentarioMedicao   varchar(4000)
                                                        , @ou_codigoRetorno       int; 
                                                  
                                                SET @in_comentarioMedicao = '';
                                                SET @ou_codigoRetorno = NULL;
                                                EXEC @l_Ret = {0}.{1}.[p_IncluiMedicao]
                                                             			@in_Ano  = {2}
		                                                              , @in_Mes  = {3}
		                                                              , @in_codigoContrato  = {4}
		                                                              , @in_codigoUsuarioInclusao  = {5}
		                                                              , @in_comentarioMedicao = @in_comentarioMedicao OUTPUT
		                                                              , @ou_codigoRetorno = @ou_codigoRetorno OUTPUT;
                                                  
                                                SELECT @l_Ret as lRet , @ou_codigoRetorno as codigoRetorno, @in_comentarioMedicao as comentarioMedicao;
                                             END",
                                           dbName, dbOwner, ano, mes, codigoContrato, codigoUsuarioResponsavel);
        DataSet ds = cDados.getDataSet(sComandoSql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            if (int.Parse(ds.Tables[0].Rows[0]["lRet"].ToString()) != 0 || int.Parse(ds.Tables[0].Rows[0]["codigoRetorno"].ToString()) != 0)
                return false;
        }
        return true;


    }

    public string getValorFormatado(string valor)
    {
        string valorFormatado = "";

        if (valor != "" && valor.IndexOf(".") != -1)
        {
            valorFormatado = string.Format("{0:n}", double.Parse(valor));
        }
        else
        {
            valorFormatado = valor;
        }

        return valorFormatado;
    }

    protected void gvImpostos_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string codMedicao = gvMedicao.GetRowValues(int.Parse(e.Parameters), "CodigoMedicao").ToString();
        carrega_gvImpostos(codMedicao);
    }
}
