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

public partial class _cadastroProjetos : System.Web.UI.Page
{
    dados cDados;
    public string baseUrl;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string versaoMSProject = "";
    private int codProjetoSalvo = -1;
    string atualizacaoPorProjeto = "N";
    public string displayUnidadeAtendimento = "";
    int codigoProjeto = -1;
    public string mostraTitulo = "block";
    public string mostraAssociacaoRelStatus = "block";

    string codigoModeloFormulario = "-1";
    public string urlCaracterizacao = "";
    private bool isPopUP = false;
    bool podeEditar = true;

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
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        /*ds1.Tables[0].Rows[0]["MostraUnidadeAtendimento"].ToString().Trim()*/


        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DataSet ds1 = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelGerente", "VersaoMSProject", "AtualizaTimesheetPorProjeto",
                                                                                    "CodigoModeloFormularioCaracterizacaoProjeto", "MostraUnidadeAtendimento");
        isPopUP = false;
        bool.TryParse(Request.QueryString["PopUP"] + "", out isPopUP);  

        defineLabelTipoProjeto();
        defineLabelUnidadeAtendimento();
        displayUnidadeAtendimento = "";
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            versaoMSProject = ds1.Tables[0].Rows[0]["VersaoMSProject"].ToString();
            atualizacaoPorProjeto = ds1.Tables[0].Rows[0]["AtualizaTimesheetPorProjeto"] + "" == "" ? "N" : ds1.Tables[0].Rows[0]["AtualizaTimesheetPorProjeto"] + "";
            hfGeral.Set("AtualizaProjeto", atualizacaoPorProjeto);
            codigoModeloFormulario = ds1.Tables[0].Rows[0]["CodigoModeloFormularioCaracterizacaoProjeto"] + "" == "" ? "-1" : ds1.Tables[0].Rows[0]["CodigoModeloFormularioCaracterizacaoProjeto"] + "";
            string labelGerente = ds1.Tables[0].Rows[0]["labelGerente"] + "" != "" ? ds1.Tables[0].Rows[0]["labelGerente"] + "" : "Gerente";

            lblGerenteProjeto.Text = labelGerente + ": *";
            rbQuemAtualiza.Items[0].Text = labelGerente;
            rbTipoAprovacao.Items[0].Text = labelGerente;

            if (ds1.Tables[0].Rows[0]["MostraUnidadeAtendimento"].ToString().Trim() == "N")
            {
                //txtObjetivos.Height = new Unit("70px");
                txtObjetivos.JSProperties["cp_MostraUnidadeAtendimento"] = "N";
                td_UnidadeAtendimento.Style.Add("display", "none");
            }
            else
            {
                //txtObjetivos.Height = new Unit("40px");
                txtObjetivos.JSProperties["cp_MostraUnidadeAtendimento"] = "S";
            }
                
        }

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

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/cadastroProjetos.js""></script>"));
        this.TH(this.TS("cadastroProjetos"));
        /*if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_AltTipProj"))
        {
            ddlTipoProjeto.ClientEnabled = true;
        }
        else
        {
            ddlTipoProjeto.ClientEnabled = false;
        }*/


        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_CnsCaract");
            btnNovo.ClientVisible = false;
            btnCancelar.ClientVisible = false;
            mostraTitulo = "none";
            mostraAssociacaoRelStatus = "none";
            
        }
        else
        {
            //pcProjeto.TabPages[1].ClientVisible = false;

            bool bPodeAcessarTela;
            /// se houver algum contrato que o usuário pode consultar
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "UN", "UN_IncPrj");
                        
            // se não puder, redireciona para a página sem acesso
            if (bPodeAcessarTela == false)
                cDados.RedirecionaParaTelaSemAcesso(this);
        }

        carregaGridRelatorios();
        carregarCombos();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            //inicializaCampos();
        }

        if (versaoMSProject == "")
        {
            /*Se ainda não tiver nenhuma versão do project associada a esse projeto
              3.1. Incluir campos "Recursos atualizam Tarefa", "TipoAtualização" e "Aprovação de Atualização"
              3.2. Os campos TipoAtualizacao e Aprovacao de Atualização só são habilitados se check box "Recursos Atualizam..." estiver marcado
              3.3. Ocultar campo Associar Cronograma Existente
              3.4. Após salvar o registro, desabilitar o botão salvar e habilitar o link "Editar Cronograma" (ver como é chamado o link em Projetos)*/
            pnVersaoProject.ClientVisible = true;//tipo de atualização e quem edita o cronograma
            spnAssociaCronograma.Visible = false;//combobox de associar cronogramas existentes.
        }
        else
        {
            /*se tiver uma versão do project associada a esse projeto
              a maneira como o projeto é salvo não se altera, isso é, o link de editar cronograma não aparece, pois,
              existem cronogramas associados ao projeto*/

            if (atualizacaoPorProjeto != "S")
                pnVersaoProject.ClientVisible = false;//tipo de atualização e quem edita o cronograma
            else
            {
                rbQuemAtualiza.Value = "N";
                rbQuemAtualiza.ClientEnabled = false;
            }

            spnAssociaCronograma.Visible = true;//combobox de associar cronogramas existentes.

            if (codigoProjeto != -1)
            {
                txtNomeProjeto.ClientEnabled = false;
                if (ddlAssoCroExistente.Value.ToString() != "-1")
                    ddlAssoCroExistente.ClientEnabled = false;
            }
        }
        


        DataSet ds = cDados.getDefinicaoUnidade(codigoEntidadeUsuarioResponsavel);
        hfGeral.Set("definicaoUnidade", "Unidade: *");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            lblUnidade.Text = ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString() + ": *";
            hfGeral.Set("definicaoUnidade", ds.Tables[0].Rows[0]["DescricaoTipoUnidadeNegocio"].ToString());
        }
        
        if (codigoProjeto != -1)
            verificaPermissaoProjeto();

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "PodeEditarCRProjeto");

        if (cDados.DataSetOk(dsParam) && cDados.DataTableOk(dsParam.Tables[0]))
        {
            if (dsParam.Tables[0].Rows[0]["PodeEditarCRProjeto"].ToString() == "N")
                txtCodigoReservado.ClientEnabled = false;
        }

        verificaAbaVisivel();
        populaComboCarteira();
    }

    private void populaComboCarteira()
    {

        string comandoSQL = string.Format(@"
        SELECT CodigoCarteira, NomeCarteira 
          FROM Carteira
         WHERE IndicaCarteiraAtiva = 'S' 
           AND IniciaisCarteiraControladaSistema IS NULL
           AND CodigoEntidade = {0}
        UNION
        SELECT CodigoCarteira, NomeCarteira 
	      FROM Carteira
	      WHERE CodigoEntidade = {0}
		    AND {1} != -1 
		    AND CodigoCarteira IN (SELECT cp.[CodigoCarteira] FROM [dbo].[CarteiraProjeto] AS [cp] WHERE cp.[CodigoProjeto] = {1} AND cp.[IndicaCarteiraPrincipal] = 'S' )
	ORDER BY 2 ASC", codigoEntidadeUsuarioResponsavel, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlCarteiraPrincipal.ValueField = "CodigoCarteira";
        ddlCarteiraPrincipal.TextField = "NomeCarteira";
        ddlCarteiraPrincipal.DataSource = ds;
        ddlCarteiraPrincipal.DataBind();

        ListEditItem li = new ListEditItem(Resources.traducao.nenhum, null);

        ddlCarteiraPrincipal.Items.Insert(0, li);

    }

    private void habilitaDesabilitaComponentes(bool habilitado)
    {
        txtNomeProjeto.ClientEnabled = habilitado;
        ddlTipoProjeto.ClientEnabled = habilitado;
        txtCodigoReservado.ClientEnabled = habilitado;
        txtObjetivos.ClientEnabled = habilitado;
        ddlCategoria.ClientEnabled = habilitado;
        ddlUnidadeNegocio.ClientEnabled = habilitado;
        ddlGerenteProjeto.ClientEnabled = habilitado;
        ddlAssoCroExistente.ClientEnabled = habilitado;
        rbQuemAtualiza.ClientEnabled = habilitado;
        rbTipoAprovacao.ClientEnabled = habilitado;
    }

    private void verificaPermissaoProjeto()
    {               
        cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref podeEditar, ref podeEditar, ref podeEditar);

        if(podeEditar)
            podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto, "null", "PR", 0, "null", "PR_EdtCaract");

        txtNomeProjeto.ClientEnabled = podeEditar;
        //ddlTipoProjeto.ClientEnabled = podeEditar;
        txtCodigoReservado.ClientEnabled = podeEditar;
        txtObjetivos.ClientEnabled = podeEditar;
        ddlCategoria.ClientEnabled = podeEditar;
        ddlUnidadeNegocio.ClientEnabled = podeEditar;
        ddlUnidadeAtendimento.ClientEnabled = podeEditar;
        ddlGerenteProjeto.ClientEnabled = podeEditar;
        ddlAssoCroExistente.ClientEnabled = podeEditar;
        rbQuemAtualiza.ClientEnabled = podeEditar;
        rbTipoAprovacao.ClientEnabled = podeEditar;
        btnSalvar.ClientVisible = podeEditar;
        ddlCarteiraPrincipal.ClientEnabled = podeEditar;
    }

    private void verificaAbaVisivel()
    {
        int largura = getLarguraTela();
        string larguraFrameForms = largura.ToString();
        int altura = 0;
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();

        if (codigoProjeto != -1 && int.Parse(codigoModeloFormulario) != -1)
        {
            bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);            

            //pcProjeto.TabPages[1].ClientVisible = true;
            urlCaracterizacao = "../../wfRenderizaFormulario.aspx?CPWF=" + codigoProjeto + "&CMF=" + codigoModeloFormulario + "&AT=" + (altura - 80) + "&WSCR=" + larguraFrameForms + "&RO=" + (podeEditar ? "N" : "S") + "&INIPERM=PR_AltCnuFrm";
            carregaAbaCaracterizacao(altura - 50);
        }
        else
        {
            //pcProjeto.TabPages[1].ClientVisible = false;
        }

        //pcProjeto.Height = (altura - 190) > 0 ? (altura - 190) : pcProjeto.Height;
    }

    private void preencheInformacoesCampos()
    {
        //pcProjeto.JSProperties["cp_txtNomeProjeto"] = txtNomeProjeto.Text;
        //pcProjeto.JSProperties["cp_txtCodigoReservado"] = txtCodigoReservado.Text;
        //pcProjeto.JSProperties["cp_txtObjetivos"] = txtObjetivos.Text;
        //pcProjeto.JSProperties["cp_ddlCategoria"] = ddlCategoria.Text;
        //pcProjeto.JSProperties["cp_ddlTipoProjeto"] = ddlTipoProjeto.Text;
        //pcProjeto.JSProperties["cp_ddlUnidadeNegocio"] = ddlUnidadeNegocio.Text;
        //pcProjeto.JSProperties["cp_ddlUnidadeAtendimento"] = ddlUnidadeAtendimento.Text;
        //pcProjeto.JSProperties["cp_CodigoGerenteProjeto"] = ddlGerenteProjeto.Value;
        //pcProjeto.JSProperties["cp_ddlAssoCroExistente"] = ddlAssoCroExistente.Text;
        //pcProjeto.JSProperties["cp_rbQuemAtualiza"] = rbQuemAtualiza.SelectedIndex;
        //pcProjeto.JSProperties["cp_rbTipoAprovacao"] = rbTipoAprovacao.SelectedIndex;
    }

    #region Combos

    private void carregarCombos()
    {
        DataSet ds;
        string where;

        if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "EN_AltTipProj"))
        {
            ddlTipoProjeto.ClientEnabled = true;
        }
        else
        {
            ddlTipoProjeto.ClientEnabled = false;
        }

        //TipoProjeto
        if (codigoProjeto == -1 && ddlTipoProjeto.ClientEnabled == false)
        {
            where = string.Format(@" AND CodigoTipoProjeto = {0}", 1);
        }
        else
        {
            where = "";
        }
        ds = getTipoProjeto(where);
        
        if (cDados.DataSetOk(ds))
        {
            string nomeTipoProjeto = cDados.getNomeTipoProjeto(codigoProjeto);

            if (nomeTipoProjeto.ToUpper() == "PROGRAMA")
            {
                ddlTipoProjeto.Items.Clear();

                int codTipo = cDados.getCodigoTipoProjeto(codigoProjeto);

                ListEditItem leiTipoProjeto = new ListEditItem(nomeTipoProjeto, codTipo);

                ddlTipoProjeto.Items.Insert(0, leiTipoProjeto);
            }
            else
            {

                ddlTipoProjeto.ValueField = "CodigoTipoProjeto";
                ddlTipoProjeto.TextField = "TipoProjeto";

                ddlTipoProjeto.DataSource = ds.Tables[0];
                ddlTipoProjeto.DataBind();
            }

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]) && ddlTipoProjeto.ClientEnabled == false)
                ddlCategoria.SelectedIndex = 0;
        }


        where = string.Format(@" AND CodigoEntidade = {0}", codigoEntidadeUsuarioResponsavel);


        //Categorias
        ds = cDados.getCategoria(where);
        if (cDados.DataSetOk(ds))
        {
            ddlCategoria.ValueField = "CodigoCategoria";
            ddlCategoria.TextField = "DescricaoCategoria";

            ddlCategoria.DataSource = ds.Tables[0];
            ddlCategoria.DataBind();


        }

        //Unidades de negocios
        where = string.Format(@" AND DataExclusao IS NULL AND IndicaUnidadeNegocioAtiva = 'S' AND CodigoEntidade = {0}", codigoEntidadeUsuarioResponsavel);

        if ((Request.QueryString["IDProjeto"] == null || Request.QueryString["IDProjeto"].ToString() == ""))
        {
            where += string.Format(@" AND {0}.{1}.f_VerificaAcessoConcedido({2}, {3}, CodigoUnidadeNegocio, NULL, 'UN', 0, NULL, 'UN_IncPrj') = 1 
                        ", cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);
        }

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

        //Unidades de atendimento
        where = string.Format(@" AND DataExclusao IS NULL AND IndicaUnidadeNegocioAtiva = 'S' ");

        ds = cDados.getUnidade(where);
        if (cDados.DataSetOk(ds))
        {
            ddlUnidadeAtendimento.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeAtendimento.TextField = "NomeUnidadeNegocio";

            ddlUnidadeAtendimento.DataSource = ds.Tables[0];
            ddlUnidadeAtendimento.DataBind();

            //if (!IsPostBack)
            //    ddlUnidadeNegocio.SelectedIndex = 0;
        }

        //Gerente de projeto
        //where = "";
        //ds = cDados.getUsuariosAtivosEntidade(codigoEntidadeUsuarioResponsavel, where);
        //if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        //{
        //    ddlGerenteProjeto.ValueField = "CodigoUsuario";
        //    ddlGerenteProjeto.TextField = "NomeUsuario";

        //    ddlGerenteProjeto.DataSource = ds.Tables[0];
        //    ddlGerenteProjeto.DataBind();

        //    if (!IsPostBack)
        //        ddlGerenteProjeto.SelectedIndex = 0;
        //}
        //ddlGerenteProjeto.TextField = "NomeUsuario";
        //ddlGerenteProjeto.ValueField = "CodigoUsuario";


        //ddlGerenteProjeto.Columns[0].FieldName = "NomeUsuario";
        //ddlGerenteProjeto.Columns[1].FieldName = "EMail";
        //ddlGerenteProjeto.TextFormatString = "{0}";


        //Associar cronograma existente
        ds = cDados.getMSProjet(codigoProjeto);

        ddlAssoCroExistente.Items.Clear();

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlAssoCroExistente.ValueField = "CodigoMSProject";
            ddlAssoCroExistente.TextField = "NomeProjeto";

            ddlAssoCroExistente.DataSource = ds.Tables[0];
            ddlAssoCroExistente.DataBind();            
        }

        ListEditItem lei = new ListEditItem(" ", "-1");

        ddlAssoCroExistente.Items.Insert(0, lei);

        if (!IsPostBack)
            ddlAssoCroExistente.SelectedIndex = 0;

    }

    #endregion

    #region Varios

    public DataSet getTipoProjeto(string where)
    {
        string comandoSQL = "";
        string codigoTipoProjeto = "";
        string comandoSQL_CodigoTipoProjeto = string.Format(@"SELECT CodigoTipoProjeto FROM Projeto WHERE CodigoProjeto = {0}", codigoProjeto);
        string comandoSQL_IndicaTipoProjeto = "";
        DataSet ds_CodigoTipoProjeto = cDados.getDataSet(comandoSQL_CodigoTipoProjeto);
        if (cDados.DataSetOk(ds_CodigoTipoProjeto) && cDados.DataTableOk(ds_CodigoTipoProjeto.Tables[0]))
        {
            codigoTipoProjeto = ds_CodigoTipoProjeto.Tables[0].Rows[0]["CodigoTipoProjeto"].ToString();
        }

        string indicaTipoProjeto = "";
        if(codigoTipoProjeto != "")
        {
            comandoSQL_IndicaTipoProjeto = string.Format(@"SELECT IndicaTipoProjeto FROM TipoProjeto WHERE CodigoTipoProjeto = {0}", codigoTipoProjeto);
            DataSet ds_IndicaTipoProjeto = cDados.getDataSet(comandoSQL_IndicaTipoProjeto);
            if (cDados.DataSetOk(ds_IndicaTipoProjeto) && cDados.DataTableOk(ds_IndicaTipoProjeto.Tables[0]))
            {
                indicaTipoProjeto = ds_IndicaTipoProjeto.Tables[0].Rows[0]["IndicaTipoProjeto"].ToString();
            }
        }
        

        if (indicaTipoProjeto == "")
        {
            comandoSQL = string.Format(
                             @"SELECT  CodigoTipoProjeto
                                      ,TipoProjeto
                                  FROM {0}.{1}.TipoProjeto  
                                 WHERE IndicaTipoProjeto IN ('PRJ','PRC')
                                 UNION 
                                SELECT CodigoTipoProjeto
                                      ,TipoProjeto
                                  FROM {0}.{1}.TipoProjeto  
                                 WHERE IndicaProjetoAgil = 'S'
                            ORDER BY TipoProjeto", cDados.getDbName(), cDados.getDbOwner());
        }

        else
        {
            comandoSQL = string.Format(
                                 @"SELECT  CodigoTipoProjeto
                                      ,TipoProjeto
                                  FROM {0}.{1}.TipoProjeto
                                 WHERE  IndicaTipoProjeto IN (SELECT indicatipoprojeto 
                                                                FROM tipoprojeto AS tp
                                                                WHERE IndicaTipoprojeto = '{2}')
                            ORDER BY TipoProjeto", cDados.getDbName(), cDados.getDbOwner(), indicaTipoProjeto);

        }
        return cDados.getDataSet(comandoSQL);
    }

    private int getLarguraTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
        return largura - 170;
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        
    }

    private void carregaAbaCaracterizacao(int altura)
    {
        string frm = string.Format(@"<iframe id=""frmCaracterizacao"" frameborder=""0"" height=""" + altura + @"px"" scrolling=""no"" src=""""
                                    width=""100%""></iframe>");

        //pcProjeto.JSProperties["cp_Url"] = urlCaracterizacao;

        //pcProjeto.TabPages[1].Controls.Add(cDados.getLiteral(frm));
    }

    #endregion

    private void carregaGridRelatorios()
    {
        string where = "";

        DataSet ds = cDados.getModeloStatusReport(codigoEntidadeUsuarioResponsavel, where);

        if (cDados.DataSetOk(ds))
        {
            gvRelatorios.DataSource = ds.Tables[0];
            gvRelatorios.DataBind();
        }
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        txtNomeProjeto.Text = "";
        txtCodigoReservado.Text = "";
        txtObjetivos.Text = "";
        
        ddlCategoria.SelectedIndex = -1;
        ddlTipoProjeto.SelectedIndex = -1;
        ddlUnidadeNegocio.SelectedIndex = -1;
        ddlUnidadeAtendimento.SelectedIndex = -1;
        ddlGerenteProjeto.SelectedIndex = -1;
        ddlAssoCroExistente.SelectedIndex = -1;
        codigoProjeto = -1;
        verificaAbaVisivel();

        inicializaCampos();

        btnSalvar.ClientEnabled = true;
        habilitaDesabilitaComponentes(btnSalvar.ClientEnabled);
        linkEditarCronograma.ClientVisible = false;
    }

    private void inicializaCampos()
    {


        if (codigoProjeto == -1)
        {
            rbTipoAprovacao.Value = "GP";
            rbQuemAtualiza.Value = "N";

            //rbTipoAprovacao.ClientEnabled = false;
            ddlTipoProjeto.Value = 1;
        }
        else
        {
            DataSet dsDados = cDados.getDadosGeraisProjeto(codigoProjeto, "");

            if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
            {
                DataTable dt = dsDados.Tables[0];

                txtNomeProjeto.Text = dt.Rows[0]["NomeProjeto"].ToString();

                //ddlTipoProjeto.Value = 
                string codTipoProjeto = dt.Rows[0]["CodigoTipoProjeto"] + "" != "" ? dt.Rows[0]["CodigoTipoProjeto"].ToString() : "-1";

                //ddlCategoria.Value = 
                string codCategoria = dt.Rows[0]["CodigoCategoria"] + "" != "" ? dt.Rows[0]["CodigoCategoria"].ToString() : "-1";

                //ddlGerenteProjeto.Value = 
                string codGerente = dt.Rows[0]["CodigoGerente"] + "" != "" ? dt.Rows[0]["CodigoGerente"].ToString() : "-1";

                ddlGerenteProjeto.Text = dt.Rows[0]["Gerente"].ToString();
                ddlGerenteProjeto.Value = dt.Rows[0]["CodigoGerente"].ToString();

                ddlGerenteProjeto.JSProperties["cp_ddlGerenteProjeto"] = dt.Rows[0]["Gerente"].ToString();

                //ddlUnidadeNegocio.Value =
                string codUnidade = dt.Rows[0]["CodigoUnidadeNegocio"] + "" != "" ? dt.Rows[0]["CodigoUnidadeNegocio"].ToString() : "-1";
                hfGeral.Set("CodigoUnidadeNegocioOriginal", codUnidade); // valor usado no momento da gravação.

                string codUnidadeAtendimento = dt.Rows[0]["CodigoUnidadeAtendimento"] + "" != "" ? dt.Rows[0]["CodigoUnidadeAtendimento"].ToString() : "-1";
                hfGeral.Set("CodigoUnidadeAtendimento", codUnidadeAtendimento); // valor usado no momento da gravação.


                ListEditItem li0 = ddlTipoProjeto.Items.FindByText(dt.Rows[0]["TipoProjeto"].ToString());
                ddlTipoProjeto.SelectedItem = li0;

                ListEditItem li1 = ddlCategoria.Items.FindByText(dt.Rows[0]["DescricaoCategoria"].ToString());
                ddlCategoria.SelectedItem = li1;

                //ListEditItem li2 = ddlGerenteProjeto.Items.FindByValue(int.Parse(codGerente));
                //ddlGerenteProjeto.SelectedItem = li2;

                ListEditItem li3 = ddlUnidadeNegocio.Items.FindByValue(int.Parse(codUnidade));
                ddlUnidadeNegocio.SelectedItem = li3;


                ListEditItem li4 = ddlUnidadeAtendimento.Items.FindByValue(int.Parse(codUnidadeAtendimento));
                ddlUnidadeAtendimento.SelectedItem = li4;

                string codigoCronograma = dt.Rows[0]["CodigoMSProject"] + "";

                if (codigoCronograma != "" && ddlAssoCroExistente.Items.FindByValue(codigoCronograma.ToLower()) != null)
                    ddlAssoCroExistente.Value = dt.Rows[0]["CodigoMSProject"].ToString();

                txtCodigoReservado.Text = dt.Rows[0]["CodigoReservado"].ToString();
                txtObjetivos.Text = dt.Rows[0]["DescricaoProposta"].ToString();
                rbQuemAtualiza.Value = dt.Rows[0]["IndicaRecursosAtualizamTarefas"] + "" != "" ? dt.Rows[0]["IndicaRecursosAtualizamTarefas"].ToString() : null;
                rbTipoAprovacao.Value = dt.Rows[0]["IndicaAprovadorTarefas"] + "" != "" ? dt.Rows[0]["IndicaAprovadorTarefas"].ToString() : null;
                
                string indicaCarteiraPrincipal = dt.Rows[0]["IndicaCarteiraPrincipal"].ToString();

                if(indicaCarteiraPrincipal.Trim().ToLower() == "s")
                {
                    ddlCarteiraPrincipal.Value = dt.Rows[0]["CodigoCarteira"];
                    ddlCarteiraPrincipal.Text = dt.Rows[0]["NomeCarteira"].ToString();
                }
            }
        }
    }


    protected void txtObjetivos_TextChanged(object sender, EventArgs e)
    {

    }

    private void defineLabelTipoProjeto()
    {
        DataSet dsParametro = cDados.getParametrosSistema("labelTipoProjeto");
        string label = "Tipo de Projeto";

        if ((cDados.DataSetOk(dsParametro)) && (cDados.DataTableOk(dsParametro.Tables[0])))
        {
            label = dsParametro.Tables[0].Rows[0]["labelTipoProjeto"].ToString();
        }

        lbTipoProjeto.Text = label + ":";

    }

    private void defineLabelUnidadeAtendimento()
    {

        lblUnidadeAtendimento.Text = cDados.defineLabelUnidadeAtendimento();

    }


    protected void ddlGerenteProjeto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
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
    protected void ddlGerenteProjeto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string filtro = "";

        filtro = e.Filter.ToString();

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, filtro, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

        if (!IsPostBack)
            inicializaCampos();
    }

    
 
    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cp_erro"] = "";
        ((ASPxCallback)source).JSProperties["cp_sucesso"] = "";
        ((ASPxCallback)source).JSProperties["cp_indicaPopup"] = (isPopUP == true) ? "S" : "N";

        string codigoMSprojet = "";

        codigoMSprojet = (ddlAssoCroExistente.Value != null && ddlAssoCroExistente.Value.ToString() != "-1") ? "'" + ddlAssoCroExistente.Value.ToString() + "'" : "NULL";

        string unidadeNegocio = (ddlUnidadeNegocio.SelectedIndex == -1) ? "NULL" : ddlUnidadeNegocio.Value.ToString();
        string unidadeAtendimento = (ddlUnidadeAtendimento.SelectedIndex == -1) ? "NULL" : ddlUnidadeAtendimento.Value.ToString();
        string gerenteProjeto = (ddlGerenteProjeto.SelectedIndex == -1) ? "NULL" : ddlGerenteProjeto.Value.ToString();
        string categoria = (ddlCategoria.SelectedIndex == -1) ? "NULL" : ddlCategoria.Value.ToString();
        string tipoProjeto = (ddlTipoProjeto.SelectedIndex == -1) ? "NULL" : ddlTipoProjeto.Value.ToString();
        string msgErro = "";

        string IndicaTipoAtualizacaoTarefas = "'P'";
        string IndicaAprovadorTarefas = "'" + rbTipoAprovacao.Value.ToString() + "'";
        string IndicaRecursosAtualizamTarefas = rbQuemAtualiza.Value.ToString();

        string codigoReservado = (txtCodigoReservado.Text == "") ? "NULL" : "'" + txtCodigoReservado.Text + "'";
        string codigoCarteiraPrincipal = ddlCarteiraPrincipal.Value == null ? "NULL" : ddlCarteiraPrincipal.Value.ToString();

        //BUSCA POR NOME DE PROJETO IGUAL
        string nomeProjeto = txtNomeProjeto.Text.Replace("'", "");

        if (codigoProjeto == -1)
        {
            int[] codigosRelatorios = new int[gvRelatorios.Selection.Count];

            for (int i = 0; i < gvRelatorios.Selection.Count; i++)
            {
                codigosRelatorios[i] = int.Parse(gvRelatorios.GetSelectedFieldValues("CodigoModeloStatusReport")[i].ToString());
            }
            
            bool resposta = cDados.incluiDadosProjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, txtNomeProjeto.Text.Replace("'", ""),
                                                      categoria, unidadeNegocio, gerenteProjeto, codigoMSprojet, codigoReservado, txtObjetivos.Text.Replace("'", "''"),
                                                      IndicaRecursosAtualizamTarefas, IndicaTipoAtualizacaoTarefas, IndicaAprovadorTarefas, codigosRelatorios,
                                                      tipoProjeto, unidadeAtendimento, codigoCarteiraPrincipal, out codProjetoSalvo, out msgErro);
            if (!resposta)
            {
                btnSalvar.ClientEnabled = true;
                string msgAlert = "";
                if (msgErro.Contains("UQ_Projeto_NomeProjeto"))
                    msgAlert = Resources.traducao.cadastroProjetos_erro_ao_salvar__nome_de_projeto_j__existente_;
                else
                    msgAlert = msgErro.Replace("'", "\"").Replace(Environment.NewLine, " ");

                ((ASPxCallback)source).JSProperties["cp_erro"] = msgAlert;
            }
            else
            {
                /*Após salvar o registro, desabilitar o botão salvar e habilitar o link "Editar Cronograma"*/
                string linkOpcao = cDados.getLinkPortalDesktop(Request.Url, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, codProjetoSalvo, "./../../");
                //link so está VISÍVEL se o projeto foi realmente salvo e se não houver ainda nenhum cronograma associado AO PROJETO
                //linkEditarCronograma.ClientVisible = (codProjetoSalvo != -1 && versaoMSProject == "");
                linkEditarCronograma.NavigateUrl = linkOpcao;
                btnSalvar.ClientEnabled = false;
                habilitaDesabilitaComponentes(btnSalvar.ClientEnabled);
                ((ASPxCallback)source).JSProperties["cp_sucesso"] = Resources.traducao.cadastroProjetos_projeto_salvo_com_sucesso_;
            }
        }
        else
        {
            // se for alteração de um projeto, verifica se está alterando a unidade e se a pessoa tem permissão para "incluir" 
            // projeto na unidade escolhida, caso tenha sido escolhida uma nova unidade

            bool bOk = true;
            string codigoUnidadeComparar = unidadeNegocio == "NULL" ? "-1" : unidadeNegocio;
            string unidadeOriginal = hfGeral.Contains("CodigoUnidadeNegocioOriginal") ? hfGeral["CodigoUnidadeNegocioOriginal"].ToString() : "-1";
            if ((codigoUnidadeComparar != unidadeOriginal) && (codigoUnidadeComparar != "-1"))
            {
                if (cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
                        int.Parse(codigoUnidadeComparar), "NULL", "UN", 0, "NULL", "UN_IncPrj") == false)
                {
                    bOk = false;
                    ((ASPxCallback)source).JSProperties["cp_erro"] = Resources.traducao.cadastroProjetos_altera__o_n_o_permitida__voc__n_o_tem_acesso_para_incluir_projeto_na_unidade_escolhida_;
                }
            }
            if (bOk == true)
            {

                bool resposta = cDados.atualizaDadosProjeto(codigoProjeto, codigoUsuarioResponsavel, txtNomeProjeto.Text.Replace("'", ""),
                    categoria, unidadeNegocio, gerenteProjeto, codigoReservado, txtObjetivos.Text.Replace("'", "''"), IndicaRecursosAtualizamTarefas,
                    IndicaTipoAtualizacaoTarefas, IndicaAprovadorTarefas, codigoMSprojet.Replace("'", ""), tipoProjeto, unidadeAtendimento, codigoCarteiraPrincipal, out msgErro);

                if (!resposta)
                {
                    string msgAlert = "";

                    if (msgErro.Contains("UQ_Projeto_NomeProjeto"))
                        msgAlert = Resources.traducao.cadastroProjetos_erro_ao_salvar__nome_de_projeto_j__existente_;
                    else
                        msgAlert = msgErro.Replace("'", "\"").Replace(Environment.NewLine, " ");

                    ((ASPxCallback)source).JSProperties["cp_erro"] = msgAlert;
                    
                }
                else
                {
                    ((ASPxCallback)source).JSProperties["cp_sucesso"] = Resources.traducao.cadastroProjetos_projeto_salvo_com_sucesso_;
                    ClientScript.RegisterStartupScript(GetType(), Resources.traducao.cadastroProjetos_ok, "mudaNomeProjeto('" + txtNomeProjeto.Text + "');", true);
                }
            }
        }
        ddlGerenteProjeto.JSProperties["cp_ddlGerenteProjeto"] = ddlGerenteProjeto.Text;


        baseUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath.TrimEnd('/');

        //URL do Projeto Recem Salvo
        string redirect = "";
        if(codProjetoSalvo != -1)
        {
            redirect = baseUrl + "/_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + codProjetoSalvo;
        }
        ((ASPxCallback)source).JSProperties["cp_redirectURLProjeto"] = redirect;

        //Response.Redirect(redirect, false);

        //Response.RedirectLocation = cDados.getPathSistema() + "espacoTrabalho/frameEspacoTrabalho_Favoritos.aspx?" + Request.QueryString.ToString();

        //DevExpress.Web.ASPxWebControl.RedirectOnCallback(redirect );
    }

    /// <summary>
    /// callback para validar se está sendo informado um código reservado duplicado ao cadastrar/alterar um projeto.
    /// </summary>
    protected void callbackValida_Callback(object source, CallbackEventArgs e)
    {
        string comandoSQL = "";
        ((ASPxCallback)source).JSProperties["cp_erro"] = "";
        string codigoReservado = txtCodigoReservado.Text.Trim();

        // se nenhum código reservado foi informado, não há que se validar a duplicidade 
        if (string.IsNullOrEmpty(codigoReservado))
        {
            return;
        }
        else
        {
            if (codigoProjeto == -1)
            {
                comandoSQL = string.Format("SELECT 1 FROM projeto WHERE RTRIM(LTRIM(CodigoReservado)) = '{0}' AND CodigoEntidade = {1} AND DataExclusao is null", codigoReservado, codigoEntidadeUsuarioResponsavel);
                DataSet ds = cDados.getDataSet(comandoSQL);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows.Count > 0)
                {
                    ((ASPxCallback)source).JSProperties["cp_erro"] = "Falha ao gravar os dados: código reservado já associado a outro projeto";
                }
            }
            if (codigoProjeto > 0)
            {
                comandoSQL = string.Format("SELECT 1 FROM projeto WHERE RTRIM(LTRIM(CodigoReservado)) = '{0}' AND CodigoEntidade = {1} AND CodigoProjeto <> {2} AND DataExclusao is null", codigoReservado, codigoEntidadeUsuarioResponsavel, codigoProjeto);
                DataSet ds = cDados.getDataSet(comandoSQL);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows.Count > 0)
                {
                    ((ASPxCallback)source).JSProperties["cp_erro"] = "Falha ao gravar os dados: código reservado já associado a outro projeto";
                }
            }
        }
    }
}
