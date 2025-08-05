using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _Estrategias_wizard_IndicadoresEstrategicosFrm : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel = 0;
    private int codigoEntidadeUsuarioResponsavel = 0;
    int CodigoIndicador = -1, CodigoUnidadeNegocio = -1;
    bool podeEditar = false;
    string codigoResponsavel = "", codigoresponsavelAtualizacao = "", nomeResponsavel = "", nomeResponsavelAtualizacao = "";
    int alturaDaUrl = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); // Ok
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()); // Ok

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_CadInd");

        if (podeEditar == false)
            podeEditar = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "IN", "IN_Alt");

        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource2.ConnectionString = cDados.classeDados.getStringConexao();
        dsFuncao.ConnectionString = cDados.classeDados.getStringConexao();

        if (!IsPostBack && !IsCallback)
            TabControl.ActiveTabIndex = 0;
        
        this.Title = cDados.getNomeSistema();
        bool r = int.TryParse(Request.QueryString["alt"], out alturaDaUrl);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            if (Request.QueryString["CI"] != null)
                CodigoIndicador = int.Parse(Request.QueryString["CI"].ToString());

            hfGeral.Set("COIN", CodigoIndicador);
        }else
        {
            CodigoIndicador = int.Parse(hfGeral.Get("COIN").ToString());
        }

        if (Request.QueryString["CUN"] != null)
            CodigoUnidadeNegocio = int.Parse(Request.QueryString["CUN"].ToString());

        if (Request.QueryString["RO"] + "" == "S")
        {
            hfGeral.Set("TipoOperacao", "Consultar");
            podeEditar = false;
        }
        else
        {
            if (CodigoIndicador == -1)
            {
                hfGeral.Set("TipoOperacao", "Incluir");
                podeEditar = false;
            }
            else
                hfGeral.Set("TipoOperacao", "Editar");
        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var TipoOperacao = """+ hfGeral.Get("TipoOperacao") + @""";</script>"));

        populaCombos();

        if (!IsPostBack)
        {
            carregaDadosIndicador();

            if(CodigoIndicador == -1 && ddlUnidadeNegocio.Items.FindByValue(codigoEntidadeUsuarioResponsavel.ToString()) != null)
            {
                ddlUnidadeNegocio.Value = codigoEntidadeUsuarioResponsavel.ToString();
            }
        }

        carregaGridDadosIndicador();    //Ok  
        //carregaFaixaTolerancia();
        
        cDados.aplicaEstiloVisual(Page);//Ok
        cDados.aplicaEstiloVisual(heGlossario, "Default");
        gridDadosIndicador.Settings.ShowFilterRow = false;
        gridDadosIndicador.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/indicadoresEstrategicos.js""></script>"));
        this.TH(this.TS("IndicadoresEstrategicosFrm", "indicadoresEstrategicos"));
        gridDadosIndicador.Columns[0].Visible = podeEditar;

        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
       // if (alturaPrincipal <= 768)
       // {
            gridDadosIndicador.Settings.VerticalScrollableHeight = 285;
            divTab1.Style.Add("height", (alturaDaUrl - 200).ToString() + "px");
            divTab1.Style.Add("overflow", "scroll");

            divTab2.Style.Add("height", (alturaDaUrl - 200).ToString() + "px");
            divTab2.Style.Add("overflow", "scroll");
        // }
        // else
       // {
       //    gridDadosIndicador.Settings.VerticalScrollableHeight = 450;
       // }

        pnCallback2.JSProperties["cp_AtualizaDados"] = "N";
        pnCallback2.SettingsLoadingPanel.Enabled = false;

        if (!IsPostBack)
        {
            if (codigoResponsavel != "")
                ddlResponsavelIndicador.JSProperties["cp_Codigo"] = int.Parse(codigoResponsavel);
            if (codigoresponsavelAtualizacao != "")
                ddlResponsavelResultado.JSProperties["cp_Codigo"] = int.Parse(codigoresponsavelAtualizacao);

            ddlResponsavelIndicador.JSProperties["cp_Descricao"] = nomeResponsavel;
            ddlResponsavelResultado.JSProperties["cp_Descricao"] = nomeResponsavelAtualizacao;
        }
        else
        {
            ddlResponsavelIndicador.JSProperties["cp_Codigo"] = "";
            ddlResponsavelResultado.JSProperties["cp_Codigo"] = "";
        }

        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);

        string definicaoUnidade = "Unidade de Negócio";

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            definicaoUnidade = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString();
        }

        lblUnidade.Text = definicaoUnidade + ":";
        ddlUnidadeNegocio.JSProperties["cp_NomeUnidade"] = definicaoUnidade;
    }

    private void carregaDadosIndicador()
    {
        string comandoSQL = string.Format(@"        
    SELECT  iu.CodigoReservado, i.CodigoIndicador
        , i.NomeIndicador, i.CodigoUnidadeMedida
        , i.GlossarioIndicador, i.CasasDecimais
        , iu.CodigoResponsavelIndicadorUnidade AS CodigoResponsavel, i.Polaridade
        , i.FormulaIndicador, i.IndicaIndicadorCompartilhado
        , i.IndicadorResultante, i.CodigoPeriodicidadeCalculo
        , i.FonteIndicador, i.IndicaCriterio, i.LimiteAlertaEdicaoIndicador
        , i.CodigoFuncaoAgrupamentoMeta, u.NomeUsuario AS NomeUsuarioResponsavel
        , iu.CodigoResponsavelAtualizacaoIndicador, uRes.NomeUsuario AS NomeUsuarioResponsavelResultado
        , iu.CodigoUnidadeNegocio, un.NomeUnidadeNegocio, i.DataInicioValidadeMeta, i.DataTerminoValidadeMeta, i.IndicaAcompanhamentoMetaVigencia
        , CASE WHEN un.CodigoUnidadeNegocio = un.CodigoEntidade THEN 'S' ELSE 'N' END AS PodeCompartilhar
        , dbo.f_GetFormulaFormatoCliente(i.codigoIndicador) AS FormulaFormatoCliente
        , ISNULL((SELECT top 1 'S' FROM ResumoIndicador ri WHERE ri.CodigoIndicador = i.CodigoIndicador AND ri.CodigoUnidadeNegocio = iu.CodigoUnidadeNegocio), 'N') AS PossuiMetaResultado
        FROM Indicador i INNER JOIN 
             IndicadorUnidade iu  ON iu.CodigoIndicador = i.CodigoIndicador  and iu.CodigoUnidadeNegocio = {1} INNER JOIN 
             UnidadeNegocio un ON un.CodigoUnidadeNegocio = iu.CodigoUnidadeNegocio LEFT JOIN 
             Usuario uRes ON uRes.CodigoUsuario = iu.CodigoResponsavelAtualizacaoIndicador LEFT JOIN 
             Usuario u  ON u.CodigoUsuario = iu.CodigoResponsavelIndicadorUnidade
        WHERE i.CodigoIndicador = {0}
        ", CodigoIndicador, CodigoUnidadeNegocio);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtCodigoReservado.Text = dr["CodigoReservado"].ToString();
            txtIndicador.Text = dr["NomeIndicador"].ToString();
            cmbAgrupamentoDaMeta.Value = dr["CodigoFuncaoAgrupamentoMeta"].ToString();
            ddlUnidadeMedida.Value = dr["CodigoUnidadeMedida"].ToString();
            ddlCasasDecimais.Value = dr["CasasDecimais"].ToString();
            ddlPolaridade.Value = dr["Polaridade"].ToString();
            ddlPeriodicidade.Value = dr["CodigoPeriodicidadeCalculo"].ToString();
            cbCheckResultante.Checked = dr["IndicadorResultante"].ToString() == "S";
            rbCriterio.Value = dr["IndicaCriterio"];
            ddlUnidadeNegocio.Value = dr["CodigoUnidadeNegocio"].ToString();
            codigoResponsavel = dr["CodigoResponsavel"].ToString();
            codigoresponsavelAtualizacao = dr["CodigoResponsavelAtualizacaoIndicador"].ToString();
            nomeResponsavel = dr["NomeUsuarioResponsavel"].ToString();
            nomeResponsavelAtualizacao = dr["NomeUsuarioResponsavelResultado"].ToString();
            txtFonte.Text = dr["FonteIndicador"].ToString();
            txtLimite.Text = dr["LimiteAlertaEdicaoIndicador"].ToString();
            ddlInicioVigencia.Value = dr["DataInicioValidadeMeta"];
            ddlTerminoVigencia.Value = dr["DataTerminoValidadeMeta"];
            cbVigencia.Checked = dr["IndicaAcompanhamentoMetaVigencia"].ToString() == "S";
            heGlossario.Html = dr["GlossarioIndicador"].ToString();
            txtFormulaIndicador.Text = dr["FormulaFormatoCliente"].ToString().Replace(",", "").Replace(".", ",");
        }
    }
    
    #region Combos

    protected void ddlResponsavelIndicador_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    protected void ddlResponsavelIndicador_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;

        ASPxComboBox comboBox = (ASPxComboBox)source;

        SqlDataSource2.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
        comboBox.DataSource = SqlDataSource2;
        comboBox.DataBind();
    }

    private void populaCombos()
    {
        DataSet ds = new DataSet();

        //combo Unidade Medida: [CodigoUnidadeMedida, SiglaUnidadeMedida]
        ds = cDados.getUnidadeMedida();

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlUnidadeMedida.DataSource = ds;
            ddlUnidadeMedida.TextField = "SiglaUnidadeMedida";
            ddlUnidadeMedida.ValueField = "CodigoUnidadeMedida";
            ddlUnidadeMedida.DataBind();
            
        }

        //combo Periodicidade: [CodigoPeriodicidade, DescricaoPeriodicidade_PT]
        ds = cDados.getPeriodicidade("AND IntervaloMeses >= 1");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlPeriodicidade.DataSource = ds;
            ddlPeriodicidade.TextField = "DescricaoPeriodicidade_PT";
            ddlPeriodicidade.ValueField = "CodigoPeriodicidade";
            ddlPeriodicidade.DataBind();
            
        }


        ddlResponsavelIndicador.TextField = "NomeUsuario";
        ddlResponsavelIndicador.ValueField = "CodigoUsuario";


        ddlResponsavelIndicador.Columns[0].FieldName = "NomeUsuario";
        ddlResponsavelIndicador.Columns[1].FieldName = "EMail";
        ddlResponsavelIndicador.TextFormatString = "{0}";

        ddlResponsavelResultado.TextField = "NomeUsuario";
        ddlResponsavelResultado.ValueField = "CodigoUsuario";


        ddlResponsavelResultado.Columns[0].FieldName = "NomeUsuario";
        ddlResponsavelResultado.Columns[1].FieldName = "EMail";
        ddlResponsavelResultado.TextFormatString = "{0}";



        //combo Agrupamento da Meta
        ds = cDados.getAgrupamentoFuncao();

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            cmbAgrupamentoDaMeta.DataSource = ds;
            cmbAgrupamentoDaMeta.TextField = "NomeFuncao";
            cmbAgrupamentoDaMeta.ValueField = "CodigoFuncao";
            cmbAgrupamentoDaMeta.DataBind();
            
        }

        //Unidades de negocios
        string where = string.Format(@" AND DataExclusao IS NULL AND IndicaUnidadeNegocioAtiva = 'S' AND CodigoEntidade = {0}", codigoEntidadeUsuarioResponsavel);

        where += string.Format(@" AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, CodigoUnidadeNegocio, NULL, 'UN', 0, NULL, 'UN_IncInd') = 1 
                        ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

        ds = cDados.getUnidade(where);
        if (cDados.DataSetOk(ds))
        {
            ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeNegocio.TextField = "NomeUnidadeNegocio";

            ddlUnidadeNegocio.DataSource = ds.Tables[0];
            ddlUnidadeNegocio.DataBind();

            //if (!IsPostBack)
            //    ddlUnidadeNegocio.SelectedIndex = 0;
        }
    }

    #endregion

    #region GvDados

    private void carregaGridDadosIndicador()
    {
        DataSet ds = cDados.getDadosGrid(CodigoIndicador.ToString());
        if (cDados.DataSetOk(ds))
        {
            gridDadosIndicador.DataSource = ds;
            gridDadosIndicador.DataBind();
        }
    }

    protected void gridDadosIndicador_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        string gruposEnLaLista = ""; // by Alejandro 17/07/2010.

        //si nao ta no modo edicao e nao ta null o conteudo do banco de dados.
        if ((!gridDadosIndicador.IsEditing) || (e.KeyValue == DBNull.Value))
            return; //retorno

        //vejo si nao ta disenhando campo NomeResponsavel, aonde fica o comboBox
        if (e.Column.FieldName == "CodigoDado")
        {
            //-----------------------------------------------------------------by Alejandro 17/07/2010.
            string where = "";

            string whereIndicador = "";

            for (int i = 0; i < gridDadosIndicador.VisibleRowCount; i++)
            { //Listar los CodigoPerfilWf que se encuentran en la Grid. De esta forma se evitará su 
              //listado en el combo salvando de una selección repetitiva.

                if (e.KeyValue == null)
                    gruposEnLaLista += "," + gridDadosIndicador.GetRowValues(i, "CodigoDado").ToString();
                else if (e.KeyValue.ToString() != gridDadosIndicador.GetRowValues(i, "CodigoDado").ToString())
                {
                    gruposEnLaLista += "," + gridDadosIndicador.GetRowValues(i, "CodigoDado").ToString();
                }
            }
            if (gruposEnLaLista != "")
            {
                where += " AND d.CodigoDado NOT IN(" + gruposEnLaLista.Substring(1) + ")";
                if (e.KeyValue != null)
                    whereIndicador = string.Format(@" OR i.CodigoIndicador = " + (int.Parse(e.KeyValue.ToString()) * -1));
            }
            DataTable dtForms = cDados.getDadosIndicadorEstrategiaGrid(codigoEntidadeUsuarioResponsavel, CodigoIndicador, where, whereIndicador).Tables[0];
            //-----------------------------------------------------------------

            //si e asim, cargo o cambo...
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            combo.Callback += new CallbackEventHandlerBase(cmbDado_OnCallback);
            combo.DataSource = dtForms; // by Alejandro 17/07/2010.
            combo.DataBind();

            bool novaLinha = (sender as ASPxGridView).IsNewRowEditing;
            // se NÃO for uma nova linha, desabilite o controle
            if (!novaLinha)
            {
                e.Editor.ClientEnabled = false;
                return;
            }

            //carregaPopUpResponsaveis(combo);
        }
    }

    private void cmbDado_OnCallback(object source, CallbackEventArgsBase e) //Inicia alguma atividade no combo.
    {
        ASPxComboBox combo = source as ASPxComboBox; //pego o combo.
        cDados.incluiDadoGrid(txtNome.Text, codigoUsuarioResponsavel.ToString(), codigoEntidadeUsuarioResponsavel.ToString());
        combo.DataBind();
    }

    protected void gridDadosIndicador_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int CodigoComponente = int.Parse(hfGeral.Get("NovoCodigoDado").ToString());
        int CodigoFuncaoIndicador = hfGeral.Get("NovoCodigoFuncao") != null && hfGeral.Get("NovoCodigoFuncao").ToString() != "" ? int.Parse(hfGeral.Get("NovoCodigoFuncao").ToString()) : 0;

        cDados.incluiComponenteIndicador(CodigoIndicador, CodigoComponente, CodigoFuncaoIndicador);

        e.Cancel = true;
        gridDadosIndicador.CancelEdit();
        carregaGridDadosIndicador();
    }

    protected void gridDadosIndicador_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int CodigoComponente = int.Parse(e.Keys[0].ToString());

        int CodigoFuncaoIndicador = hfGeral.Get("NovoCodigoFuncao") != null && hfGeral.Get("NovoCodigoFuncao").ToString() != "" ? int.Parse(hfGeral.Get("NovoCodigoFuncao").ToString()) : 0;

        cDados.atualizaComponenteIndicador(CodigoIndicador, CodigoComponente, CodigoFuncaoIndicador);

        e.Cancel = true;
        gridDadosIndicador.CancelEdit();
        carregaGridDadosIndicador();
    }

    protected void gridDadosIndicador_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        gridDadosIndicador.JSProperties["cp_DadoExcluido"] = "";
        
        int CodigoComponente = int.Parse(e.Keys[0].ToString());

        cDados.excluiComponenteIndicador(CodigoIndicador, CodigoComponente);
        gridDadosIndicador.JSProperties["cp_DadoExcluido"] = "S";

        e.Cancel = true;
        //gridDadosIndicador.CancelEdit();
        carregaGridDadosIndicador();
    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "IndEstrat");
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeEditar, "gridDadosIndicador.AddNewRow();", true, false, false, "IndEstrat", "", this);
    }

    #endregion

    #region Faixas de Tolerancia

    protected void pnCallbackFaixaTolerancia_Callback(object sender, CallbackEventArgsBase e)
    {
        string opcao = e.Parameter;
        pnCallbackFaixaTolerancia.JSProperties["cp_FallaEditada"] = "";

        if (opcao.Equals("Salvar"))
        {
            gravaFaixaTolerancia(CodigoIndicador);
            pnCallbackFaixaTolerancia.JSProperties["cp_FallaEditada"] = "OK";
        }
        else
            carregaFaixaTolerancia();
    }

    private void carregaFaixaTolerancia()
    {
        DataSet dsTolerancia = cDados.getFaixaToleranciaObjeto(CodigoIndicador, "IN", "");

        ddlC4.Value = "S";

        txtD1.Text = "";
        txtD2.Text = "";
        txtD3.Text = "";
        txtD4.Text = "";

        txtA1.Text = "";
        txtA2.Text = "";
        txtA3.Text = "";
        txtA4.Text = "";

        if (cDados.DataSetOk(dsTolerancia) && cDados.DataTableOk(dsTolerancia.Tables[0]))
        {
            DataTable dtFT = dsTolerancia.Tables[0];
            int quantidade = dtFT.Rows.Count;

            if (quantidade > 0)
            {
                txtD1.Text = dtFT.Rows[0]["ValorLimiteInferior"].ToString();
                txtA1.Text = dtFT.Rows[0]["ValorLimiteSuperior"].ToString();

                if (quantidade > 1)
                {
                    txtD2.Text = dtFT.Rows[1]["ValorLimiteInferior"].ToString();
                    txtA2.Text = dtFT.Rows[1]["ValorLimiteSuperior"].ToString();

                    if (quantidade > 2)
                    {
                        txtD3.Text = dtFT.Rows[2]["ValorLimiteInferior"].ToString();
                        txtA3.Text = dtFT.Rows[2]["ValorLimiteSuperior"].ToString();

                        if (quantidade > 3)
                        {
                            ddlC4.Value = dtFT.Rows[3]["CorDesempenho"].ToString();
                            txtD4.Text = dtFT.Rows[3]["ValorLimiteInferior"].ToString();
                            txtA4.Text = dtFT.Rows[3]["ValorLimiteSuperior"].ToString();
                        }
                    }
                }
            }
        }
    }

    private void gravaFaixaTolerancia(int codigoIndicador)
    {
        cDados.excluiFaixasToleranciaObjeto("IN", codigoIndicador);

        insereFaixa(ddlC1.Value.ToString(), txtD1.Text, txtA1.Text, codigoIndicador);
        insereFaixa(ddlC2.Value.ToString(), txtD2.Text, txtA2.Text, codigoIndicador);
        insereFaixa(ddlC3.Value.ToString(), txtD3.Text, txtA3.Text, codigoIndicador);
        insereFaixa(ddlC4.Value.ToString(), txtD4.Text, txtA4.Text, codigoIndicador);
    }

    private void insereFaixa(string valorCmb, string valorTxt1, string valorTxt2, int codigoIndicador)
    {
        if (valorCmb != "S" && (double.Parse(valorTxt2) != 0))
        {
            cDados.incluiFaixaTolerancia("IN", codigoIndicador, double.Parse(valorTxt1), double.Parse(valorTxt2), valorCmb, "Meta");
        }
    }

    #endregion

    #region Banco de Dados

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        pnCallback2.JSProperties["cp_OperacaoOk"] = "";

        string mensagemErro_Persistencia = "";
        //defineTituloGridDados(false);

        if (hfGeral.Get("TipoOperacao").ToString() == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (hfGeral.Get("TipoOperacao").ToString() == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            

            pcMensagemGravacao.Modal = false;
            pnCallback2.JSProperties["cp_StatusSalvar"] = "1"; // 1 indica que foi salvo com sucesso.
            pnCallback2.JSProperties["cp_OperacaoOk"] = hfGeral.Get("TipoOperacao").ToString();
        }
        else // alguma coisa deu errado...
        {
            pnCallback2.JSProperties["cp_StatusSalvar"] = "-1"; // 1 indica que foi salvo com sucesso.
            pcMensagemGravacao.Modal = true;
            pnCallback2.JSProperties["cp_ErroSalvar"] = mensagemErro_Persistencia;
        }
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusão do registro
    {
        int novoCodigoIndicador = 0;
        string msg = "";
        string formulaIndicador = "";
        if (hfGeral.Contains("FormulaIndicador"))
            formulaIndicador = hfGeral.Get("FormulaIndicador").ToString();

        string dataInicio = (ddlInicioVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlInicioVigencia.Date));
        string dataTermino = (ddlTerminoVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlTerminoVigencia.Date));
        string indicaAcompanhamentoMetaVigencia = cbVigencia.Checked ? "S" : "N";

        try
        {
            string indicaCriterio = (rbCriterio.Value == null) ? "NULL" : rbCriterio.Value.ToString();

            if (cDados.incluiIndicador(txtIndicador.Text.Replace("'", "''"),
                                       ddlResponsavelIndicador.Value.ToString(),
                                       txtFonte.Text.Replace("'", "''"),
                                       ddlUnidadeMedida.Value.ToString(), ddlPeriodicidade.Value.ToString(),
                                       ddlPolaridade.Value.ToString(),
                                       formulaIndicador,
                                       ddlUnidadeNegocio.Value.ToString(), codigoUsuarioResponsavel.ToString(),
                                       heGlossario.Html.Replace("'", "''"),
                                       ddlCasasDecimais.Value.ToString(),
                                       (cbCheckResultante.Checked ? "S" : "N"),
                                       cmbAgrupamentoDaMeta.Value.ToString(),
                                       indicaCriterio, int.Parse(txtLimite.Text), txtCodigoReservado.Text, ddlResponsavelResultado.Value.ToString(),
                                       dataInicio, dataTermino, indicaAcompanhamentoMetaVigencia,
                                       ref novoCodigoIndicador))
            {

                
                pnCallback2.JSProperties["cp_COIN"] =  novoCodigoIndicador;
                pnCallback2.JSProperties["cp_TipoOperacao"] = "Editar";
                pnCallback2.JSProperties["cp_modoGridDados"] = "Editar";
                gridDadosIndicador.Columns[0].Visible = true;
                pnCallback2.JSProperties["cp_AtualizaDados"] = "S";            
                msg = "";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        return msg;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        // busca a chave primaria
        string formulaIndicador = hfGeral.Get("FormulaIndicador").ToString();
        string chave = CodigoIndicador.ToString();

        string indicaCriterio = (rbCriterio.Value == null) ? "NULL" : rbCriterio.Value.ToString();
        string dataInicio = (ddlInicioVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlInicioVigencia.Date));
        string dataTermino = (ddlTerminoVigencia.Text.Equals("")) ? "NULL" : string.Format("CONVERT(DateTime, '{0}', 103)", string.Format("{0:dd/MM/yyyy}", ddlTerminoVigencia.Date));
        string indicaAcompanhamentoMetaVigencia = cbVigencia.Checked ? "S" : "N";
        string msgErro = "";

        bool retorno = cDados.atualizaIndicador(chave, txtIndicador.Text.Replace("'", "''"),
                                 ddlResponsavelIndicador.Value.ToString(),
                                 txtFonte.Text.Replace("'", "''"),
                                 ddlUnidadeMedida.Value.ToString(), ddlPeriodicidade.Value.ToString(),
                                 ddlPolaridade.Value.ToString(),
                                 formulaIndicador,
                                 codigoUsuarioResponsavel.ToString(), ddlCasasDecimais.Value.ToString(),
                                 (cbCheckResultante.Checked ? "S" : "N"),
                                 heGlossario.Html.ToString().Replace("'", "''"),
                                 cmbAgrupamentoDaMeta.Value.ToString(),
                                 indicaCriterio, int.Parse(txtLimite.Text), txtCodigoReservado.Text, int.Parse(ddlUnidadeNegocio.Value.ToString()), ddlResponsavelResultado.Value.ToString(),
                                 dataInicio, dataTermino, indicaAcompanhamentoMetaVigencia, ref msgErro);


        
        return msgErro;
    }

    #endregion


    protected void callbackCalc_Callback(object source, CallbackEventArgs e)
    {
        string valor = "";

        if (e.Parameter != "")
        {
            string comandoSQL = string.Format(@"
        begin
          declare @retorno decimal(25,6)
  
          begin try
            select @retorno = {0}
          end try  
  
          begin catch  
   
          end catch    
  
          select @retorno as valor
        end", e.Parameter);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                valor = string.Format("{0:n" + ddlCasasDecimais.Value + "}", ds.Tables[0].Rows[0]["valor"]);
            }
        }

        callbackCalc.JSProperties["cp_Valor"] = valor;
    }
}