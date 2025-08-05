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

public partial class _cadastroProcessos : System.Web.UI.Page
{
    dados cDados;
    //DataSet ds;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    //private string versaoMSProject = "";
    private int codProjetoSalvo = -1;
    //string atualizacaoPorProjeto = "N";

    int codigoProjeto = -1;
    public string mostraTitulo = "block";

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

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/cadastroProcessos.js""></script>"));
        this.TH(this.TS("cadastroProcessos"));
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
            btnNovo.ClientVisible = false;
            mostraTitulo = "none";
        }

        carregarCombos();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            //inicializaCampos();
        }
        
        txtObjetivos.Height = new Unit("125px");       
    }

    #region Combos

    private void carregarCombos()
    {
        DataSet ds;        

        //Unidades de negocios
        ds = cDados.getUnidade(" AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);
        
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlUnidadeNegocio.ValueField = "CodigoUnidadeNegocio";
            ddlUnidadeNegocio.TextField = "NomeUnidadeNegocio";

            ddlUnidadeNegocio.DataSource = ds.Tables[0];
            ddlUnidadeNegocio.DataBind();

            if (!IsPostBack)
                ddlUnidadeNegocio.SelectedIndex = 0;
        }

        //Gerente de projeto
        //ds = cDados.getUsuariosAtivosEntidade(codigoEntidadeUsuarioResponsavel, "");
        //if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        //{
        //    ddlGerenteProjeto.ValueField = "CodigoUsuario";
        //    ddlGerenteProjeto.TextField = "NomeUsuario";

        //    ddlGerenteProjeto.DataSource = ds.Tables[0];
        //    ddlGerenteProjeto.DataBind();
        //}
        //ddlGerenteProjeto.TextField = "NomeUsuario";
        //ddlGerenteProjeto.ValueField = "CodigoUsuario";


        //ddlGerenteProjeto.Columns[0].FieldName = "NomeUsuario";
        //ddlGerenteProjeto.Columns[1].FieldName = "EMail";
        //ddlGerenteProjeto.TextFormatString = "{0}";

        //if (!IsPostBack)
        //    ddlGerenteProjeto.SelectedIndex = 0;
        ////}
    }

    #endregion

    #region Varios

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        
        string unidadeNegocio = (ddlUnidadeNegocio.SelectedIndex == -1) ? "NULL" : ddlUnidadeNegocio.Value.ToString();
        string gerenteProjeto = (ddlGerenteProjeto.SelectedIndex == -1) ? "NULL" : ddlGerenteProjeto.Value.ToString();
        string msgErro = "";
        
        string IndicaTipoAtualizacaoTarefas = "'P'";
        string IndicaAprovadorTarefas = "'" + rbTipoAprovacao.Value.ToString() + "'";
        string IndicaRecursosAtualizamTarefas = rbQuemAtualiza.Value.ToString();

        string codigoReservado = (txtCodigoReservado.Text == "") ? "NULL" : "'" + txtCodigoReservado.Text + "'";

        if (codigoProjeto == -1)
        {
            bool resposta = cDados.incluiDadosProcesso(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, txtNomeProjeto.Text.Replace("'", ""),
                                                       unidadeNegocio, gerenteProjeto,  codigoReservado, txtObjetivos.Text.Replace("'", "''"), IndicaRecursosAtualizamTarefas, IndicaTipoAtualizacaoTarefas, IndicaAprovadorTarefas, out codProjetoSalvo, out msgErro);
            if (!resposta)
            {
                string msgAlert = "";

                if (msgErro.Contains("UQ_Projeto_NomeProjeto"))
                    msgAlert = "Erro ao salvar! Nome de processo já existente!";
                else
                    msgAlert = msgErro.Replace("'", "\"").Replace(Environment.NewLine, " ");

                ClientScript.RegisterStartupScript(GetType(), "ok", string.Format("window.top.mostraMensagem('{0}', 'erro', true, false, null);", msgAlert), true);
            }
            else
            {                
                btnSalvar.ClientEnabled = false;
                ClientScript.RegisterStartupScript(GetType(), "ok", "window.top.mostraMensagem('Processo Salvo com Sucesso!', 'sucesso', false, false, null);", true);
            }
        }
        else
        {
            bool resposta = cDados.atualizaDadosProjeto(codigoProjeto, codigoUsuarioResponsavel, txtNomeProjeto.Text.Replace("'", ""),
                "NULL", unidadeNegocio, gerenteProjeto, codigoReservado, txtObjetivos.Text.Replace("'", "''"), IndicaRecursosAtualizamTarefas, IndicaTipoAtualizacaoTarefas, IndicaAprovadorTarefas, "","3", "NULL","NULL", out msgErro);

            if (!resposta)
            {
                string msgAlert = "";

                if (msgErro.Contains("UQ_Projeto_NomeProjeto"))
                    msgAlert = "Erro ao salvar! Nome de processo já existente!";
                else
                    msgAlert = msgErro.Replace("'", "\"").Replace(Environment.NewLine, " ");

                ClientScript.RegisterStartupScript(GetType(), "ok", string.Format("window.top.mostraMensagem('{0}', 'erro', true, false, null);", msgAlert), true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "ok", "window.top.mostraMensagem('Processo Salvo com Sucesso!', 'sucesso', false, false, null); mudaNomeProjeto('" + txtNomeProjeto.Text + "');", true);
            }
        }
        ddlGerenteProjeto.JSProperties["cp_ddlGerenteProjeto"] = ddlGerenteProjeto.Text;
    }

    #endregion
    
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        txtNomeProjeto.Text = "";
        txtCodigoReservado.Text = "";
        txtObjetivos.Text = "";
        
        ddlUnidadeNegocio.SelectedIndex = -1;
        ddlGerenteProjeto.SelectedIndex = -1;

        inicializaCampos();

        btnSalvar.ClientEnabled = true;
    }

    private void inicializaCampos()
    {
        if (codigoProjeto == -1)
        {
            rbTipoAprovacao.Value = "GR";
            rbQuemAtualiza.Value = "N";
            btnFechar.ClientVisible = true;
        }
        else
        {
            btnFechar.ClientVisible = false;

            DataSet dsDados = cDados.getDadosGeraisProjeto(codigoProjeto, "");

            if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
            {
                DataTable dt = dsDados.Tables[0];

                txtNomeProjeto.Text = dt.Rows[0]["NomeProjeto"].ToString();

                ddlGerenteProjeto.Value = dt.Rows[0]["CodigoGerente"].ToString();
                ddlGerenteProjeto.Text = dt.Rows[0]["Gerente"].ToString();


                ddlGerenteProjeto.JSProperties["cp_ddlGerenteProjeto"] = dt.Rows[0]["Gerente"].ToString();

                ddlUnidadeNegocio.Value = dt.Rows[0]["CodigoUnidadeNegocio"] + "" != "" ? dt.Rows[0]["CodigoUnidadeNegocio"].ToString() : null;
                txtCodigoReservado.Text = dt.Rows[0]["CodigoReservado"].ToString();
                txtObjetivos.Text = dt.Rows[0]["DescricaoProposta"].ToString();
                rbQuemAtualiza.Value = dt.Rows[0]["IndicaRecursosAtualizamTarefas"] + "" != "" ? dt.Rows[0]["IndicaRecursosAtualizamTarefas"].ToString() : null;
                rbTipoAprovacao.Value = dt.Rows[0]["IndicaAprovadorTarefas"] + "" != "" ? dt.Rows[0]["IndicaAprovadorTarefas"].ToString() : null;
               
            }
        }
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
}
