using DevExpress.Web;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_succ_Projetos_popup : System.Web.UI.Page
{

    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaGrid = 0;
    private string resolucaoCliente = "";
    private string codigoInstrumento = "";
    object objCodigo;

    ASPxGridView gvTrimestreDotacoes_;
    ASPxGridView gvTrimestrePrevisaoOrcamentaria_;
    ASPxGridView gvItensDeDespesa_;

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
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        
        if (Request.QueryString["CI"] != null && Request.QueryString["CI"] + "" != "")
        {
            codigoInstrumento = Request.QueryString["CI"];
        }
        if (Request.QueryString["AlturaGrid"] != null && Request.QueryString["AlturaGrid"] + "" != "")
        {
            alturaGrid = int.Parse(Request.QueryString["AlturaGrid"]);
        }
        defineAlturaTela();
        populaTela();

        carregaGvPrevisaoOrc();
        carregaGvProjetos();
        carregaGVPlanoAplicacaoConvenio();
        carregaGVParticipes();
        carregaGVDotacoes();
        cDados.aplicaEstiloVisual(this.Page);
        if (!IsPostBack)
        {
            pgControl.ActiveTabIndex = 0;
        }
        gvProjetos.Settings.ShowFilterRow = false;
        gvDotacoes.Settings.ShowFilterRow = false;
        gvParticipesConvenio.Settings.ShowFilterRow = false;
        gvPlanoAplicacaoConvenio.Settings.ShowFilterRow = false;
    }

    private void carregaGVDotacoes()
    {
        string comandoSQL = string.Format(@"
        SELECT Exercicio,
                IdDotacaoParcela,
                IdItemDespesaFicha,
                Dotacao,
                ItemDespesa,
                ValorParcela
          FROM f_pbh_IJ_GetDotacao({0})", codigoInstrumento);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvDotacoes.DataSource = ds;
            gvDotacoes.DataBind();
        }
    }

    private void carregaGVParticipes()
    {
        string comandoSQL = string.Format(@"select TipoParticipe, Orgao from f_pbh_IJ_GetParticipes({0})", codigoInstrumento);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvParticipesConvenio.DataSource = ds;
            gvParticipesConvenio.DataBind();
        }
    }

    private void carregaGVPlanoAplicacaoConvenio()
    {
        string comandoSQL = string.Format(@"
        SELECT NaturezaDespesa,
               Fonte,
               ValorAutorizado,
               ValorEmpenhado,
               ValorLiquidado,
               ValorPago
          FROM f_pbh_IJ_GetPlanoAplicacao({0})", codigoInstrumento);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvPlanoAplicacaoConvenio.DataSource = ds;
            gvPlanoAplicacaoConvenio.DataBind();
        }
    }

    private void carregaGvProjetos()
    {
        string comandoSQL = string.Format(@"select 
        CodigoProjeto,
        NomeProjeto,
        ValorProjeto,
        PercentualProjeto
        from dbo.f_pbh_IJ_GetProjetosAssociados({0})", codigoInstrumento);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            gvProjetos.DataSource = ds.Tables[0];
            gvProjetos.DataBind();
        }        
    }

    private void defineAlturaTela()
    {
        dv01.Style.Add("height", alturaGrid.ToString() + "px");
        dv03.Style.Add("height", alturaGrid.ToString() + "px");
        dv04.Style.Add("height", alturaGrid.ToString() + "px");
        dv05.Style.Add("height", alturaGrid.ToString() + "px");

        gvPrevisaoOrcamentaria0.Settings.VerticalScrollableHeight =
        gvDotacoes.Settings.VerticalScrollableHeight = alturaGrid - 200;
        gvProjetos.Settings.VerticalScrollableHeight = alturaGrid - 200;
        memoObjeto.Height = new Unit((alturaGrid - 235).ToString() + "px");

        gvPlanoAplicacaoConvenio.Settings.VerticalScrollableHeight = ((alturaGrid - 200) / 2) - 40;
        gvParticipesConvenio.Settings.VerticalScrollableHeight = ((alturaGrid - 200) / 2) - 40;
    }

    private void carregaGvPrevisaoOrc()
    {
        string comandoSQL_PrevisaoOrcamentaria1 = string.Format(@"
        declare @codigoInstrumento as decimal(15)
        set @codigoInstrumento = {0}
        SELECT ExercicioPrevisao, 
               TrimestrePrevisao, 
               ClassificacaoOrcamentaria, 
               ValorPrevisto, 
               ValorTotal 
         FROM [dbo].[f_pbh_IJ_GetPrevisaoOrcamentariaMD](@codigoInstrumento, 'M', null, null)", codigoInstrumento);
        
        DataSet ds1 = cDados.getDataSet(comandoSQL_PrevisaoOrcamentaria1);
        gvPrevisaoOrcamentaria0.DataSource = ds1;
        gvPrevisaoOrcamentaria0.DataBind();

    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/succ_MenuPrincipal.js""></script>"));
        this.TH(this.TS("_Strings", "succ_MenuPrincipal"));
    }

    private void populaTela()
    {
        string comandoSQL = string.Format(@"
         SELECT CodigoContratoPortal,
                CodigoInstrumentoJuridico,
                NumeroInstrumentoJuridico,
                NomeFornecedor,
                DataInicioVigencia,
                DataTerminoVigencia,
                ValorInstrumentoJuridico,
                TipoContrato,
                DescricaoNatureza,
                NomeGestorInstrumentoJuridico,
                DataAssinatura,
                DescricaoObjeto 
           FROM [dbo].[f_pbh_IJ_GetDetalhesInstrumento] ({0}) ", codigoInstrumento);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtIJ3.Text =
            txtIJ2.Text =
            txtIJ1.Text =
                txtIJ.Text =
               txtIJConvenios.Text = ds.Tables[0].Rows[0]["NumeroInstrumentoJuridico"].ToString();

            dtInicioVigencia3.Text =
            dtInicioVigencia2.Text =
            dtInicioVigencia.Text =
                dtInicioVigencia1.Text =
                dtInicioVigenciaConvenio.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DataInicioVigencia"].ToString()).ToString("dd/MM/yyyy");


            dtTerminoVigencia3.Text = 
            dtTerminoVigencia1.Text =
                 dtTerminoVigencia2.Text =
                 dtTerminoVigencia.Text =
                 dtTerminoVigenciaConvenio.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DataTerminoVigencia"].ToString()).ToString("dd/MM/yyyy");

            dtAssinatura.Text =
                dtAssinatura1.Text =
                dtAssinatura2.Text =
                 dtAssinatura3.Text =
                dtAssinaturaConvenio.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DataAssinatura"].ToString()).ToString("dd/MM/yyyy");

            txtFornecedor.Text = ds.Tables[0].Rows[0]["NomeFornecedor"].ToString();

            spValorContrato.Value = ds.Tables[0].Rows[0]["ValorInstrumentoJuridico"].ToString();
            txtTipo.Text = ds.Tables[0].Rows[0]["TipoContrato"].ToString();
            txtNatureza.Text = ds.Tables[0].Rows[0]["DescricaoNatureza"].ToString();
            txtGestor.Text = ds.Tables[0].Rows[0]["NomeGestorInstrumentoJuridico"].ToString();
            memoObjeto.Text = ds.Tables[0].Rows[0]["DescricaoObjeto"].ToString();
        }
    }

    #region MENU ITEM CLICK

    protected void menuPrevisaoOrc_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "succDetalhes", "Detalhes IJ", this);
    }

    protected void menuPrevisaoOrc_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "succDetalhes");
    }

    protected void menu_gvProjeto_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "succDetalhes", "Detalhes IJ", this);
    }

    protected void menu_gvProjeto_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "succDetalhes");
    }

    protected void menu_Dotacoes_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "succDetalhes");
    }

    protected void menu_Dotacoes_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "succDetalhes", "Detalhes IJ", this);
    }
   
    #endregion

    protected void gvExporter_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    
    protected void gvExercicioDotacoes_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            //hfIndexFormulario.Value = e.VisibleIndex.ToString();
            // procura pela grid "Campos" dentro do detailRow da grid Formularios
            string ID_IJ_ITEM_DESPESA_FICHA = (gvDotacoes.GetRowValues(e.VisibleIndex, "IdDotacaoParcela") == null) ? "-1" :
                gvDotacoes.GetRowValues(e.VisibleIndex, "IdDotacaoParcela").ToString();
            gvTrimestreDotacoes_ = gvDotacoes.FindDetailRowTemplateControl(e.VisibleIndex, "gvTrimestreDotacoes") as ASPxGridView;
            
            if (gvTrimestreDotacoes_ != null)
            {
                cDados.aplicaEstiloVisual(gvTrimestreDotacoes_);
                gvTrimestreDotacoes_.Settings.ShowFilterRow = false;
                gvTrimestreDotacoes_.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                gvTrimestreDotacoes_.SettingsBehavior.AllowSort = false;
                gvTrimestreDotacoes_.SettingsBehavior.AllowDragDrop = false;


                // a variavel "objCodigo" é lida no evento "gvCampos_BeforePerformDataSelect"
                if (objCodigo != null)
                {
                    populaGrid_gvTrimestreDotacoes(int.Parse(objCodigo.ToString()));
                    gvTrimestreDotacoes_.DataBind();
                }
            }


            gvItensDeDespesa_ = gvDotacoes.FindDetailRowTemplateControl(e.VisibleIndex, "gvItensDeDespesa") as ASPxGridView;

            if (gvItensDeDespesa_ != null)
            {
                cDados.aplicaEstiloVisual(gvItensDeDespesa_);
                gvItensDeDespesa_.Settings.ShowFilterRow = false;
                gvItensDeDespesa_.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                gvItensDeDespesa_.SettingsBehavior.AllowSort = false;
                gvItensDeDespesa_.SettingsBehavior.AllowDragDrop = false;


                // a variavel "objCodigo" é lida no evento "gvCampos_BeforePerformDataSelect"
                if (objCodigo != null)
                {
                    populaGrid_gvItensDeDespesa(ID_IJ_ITEM_DESPESA_FICHA);
                }
            }
        }
    }

    private void populaGrid_gvTrimestreDotacoes(int codigo)
    {

        string comandoSQL = string.Format(
            @"select Trimestre,
                     ValorAutorizado, 
                     ValorEmpenhado,
                     ValorLiquidado,
                     ValorPago 
                from {0}.{1}.f_pbh_IJ_GetPagamentos({2})", cDados.getDbName(), cDados.getDbOwner(), codigo);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvTrimestreDotacoes_.DataSource = ds;
        gvTrimestreDotacoes_.DataBind();    
    }

    private void populaGrid_gvTrimestrePrevisaoOrcamentaria(string codigo)
    {

        string ano = codigo.Split('|')[0];
        string classificacao = codigo.Split('|')[1];

        string comandoSQL = string.Format(
            @"SELECT 
                 ExercicioPrevisao, 
                 TrimestrePrevisao, 
	             ClassificacaoOrcamentaria, 
	             ValorPrevisto, 
	             ValorTotal 
	        FROM [dbo].[f_pbh_IJ_GetPrevisaoOrcamentariaMD]({0}, 'D', {1}, '{2}')"
        , codigoInstrumento, ano, classificacao);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvTrimestrePrevisaoOrcamentaria_.DataSource = ds;
        gvTrimestrePrevisaoOrcamentaria_.DataBind();
    }

    private void populaGrid_gvItensDeDespesa(string codigo)
    {

        string comandoSQL = string.Format(
            @"SELECT 
             [CodigoControleCCG], 
             [TituloCCG]
	        FROM [dbo].[f_pbh_IJ_GetCCGItemDespesa]({0})"
        , codigo);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvItensDeDespesa_.DataSource = ds;
        gvItensDeDespesa_.DataBind();
    }

    protected void gvTrimestreDotacoes_BeforePerformDataSelect(object sender, EventArgs e)
    {
        // Este evento ocorre antes da grid gvCampos receber os dados do select que a popula
        // como é um master-detail, antes de popularmos o detail, temos que o obter o código (keyFieldName) da grid master
        objCodigo = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void gvTrimestreDotacoes_Load(object sender, EventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;
        cDados = CdadosUtil.GetCdados(null);
        cDados.aplicaEstiloVisual(gv);
        gv.Settings.ShowFilterRow = false;
        gv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gv.SettingsBehavior.AllowSort = false;
        gv.SettingsBehavior.AllowDragDrop = false;
    }
     
    protected void gvPrevisaoOrcamentaria0_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            //hfIndexFormulario.Value = e.VisibleIndex.ToString();
            // procura pela grid "Campos" dentro do detailRow da grid Formularios
            gvTrimestrePrevisaoOrcamentaria_ = ((ASPxGridView)sender).FindDetailRowTemplateControl(e.VisibleIndex, "gvTrimestrePrevisaoOrcamentaria") as ASPxGridView;

            if (gvTrimestrePrevisaoOrcamentaria_ != null)
            {
                cDados.aplicaEstiloVisual(gvTrimestrePrevisaoOrcamentaria_);
                gvTrimestrePrevisaoOrcamentaria_.Settings.ShowFilterRow = false;
                gvTrimestrePrevisaoOrcamentaria_.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                gvTrimestrePrevisaoOrcamentaria_.SettingsBehavior.AllowSort = false;
                gvTrimestrePrevisaoOrcamentaria_.SettingsBehavior.AllowDragDrop = false;


                // a variavel "objCodigo" é lida no evento "gvCampos_BeforePerformDataSelect"
                if (objCodigo != null)
                {
                    populaGrid_gvTrimestrePrevisaoOrcamentaria(objCodigo.ToString());
                    gvTrimestrePrevisaoOrcamentaria_.DataBind();
                }
            }
        }
    }

    protected void gvTrimestrePrevisaoOrcamentaria_Load(object sender, EventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;
        cDados = CdadosUtil.GetCdados(null);
        cDados.aplicaEstiloVisual(gv);
        gv.Settings.ShowFilterRow = false;
        gv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gv.SettingsBehavior.AllowSort = false;
        gv.SettingsBehavior.AllowDragDrop = false;
    }

    protected void gvTrimestrePrevisaoOrcamentaria_BeforePerformDataSelect(object sender, EventArgs e)
    {
        // Este evento ocorre antes da grid gvCampos receber os dados do select que a popula
        // como é um master-detail, antes de popularmos o detail, temos que o obter o código (keyFieldName) da grid master
        objCodigo = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void gvItensDeDespesa_BeforePerformDataSelect(object sender, EventArgs e)
    {
        // Este evento ocorre antes da grid gvCampos receber os dados do select que a popula
        // como é um master-detail, antes de popularmos o detail, temos que o obter o código (keyFieldName) da grid master
        objCodigo = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void gvItensDeDespesa_Load1(object sender, EventArgs e)
    {
        ASPxGridView gv = sender as ASPxGridView;
        cDados = CdadosUtil.GetCdados(null);
        cDados.aplicaEstiloVisual(gv);
        gv.Settings.ShowFilterRow = false;
        gv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        gv.SettingsBehavior.AllowSort = false;
        gv.SettingsBehavior.AllowDragDrop = false;
    }
}