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

public partial class _Projetos_DadosProjeto_popupEdicaoTarefaEAP : System.Web.UI.Page
{
    dados cDados;

    private string IDEdicaoEAP;
    private string modoVisualizacao;
    private string code;
    private string elementName;
    private string IdTarefa;
    private int codigoEntidade;
    private string resolucaoCliente = "";

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }
        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        SqlDataSource2.ConnectionString = cDados.classeDados.getStringConexao();
        carregaComboRecursos();
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
    }

    private void carregaComboRecursos()
    {
        string comandoSql = string.Format(@"
         SELECT NULL AS CodigoUsuario, NULL As NomeRecursoCorporativo
        UNION
         SELECT CodigoUsuario, 
                NomeRecursoCorporativo 
           FROM RecursoCorporativo AS rs 
          WHERE CodigoTipoRecurso = 1 
            AND CodigoEntidade = {0}
            AND CodigoUsuario IS NOT NULL
          ORDER BY 2", codigoEntidade);
        DataSet dsRecursos = cDados.getDataSet(comandoSql);
        if (cDados.DataSetOk(dsRecursos))
        {
            ddlResponsavel.DataSource = dsRecursos.Tables[0];

            ddlResponsavel.TextField = "NomeRecursoCorporativo";

            ddlResponsavel.ValueField = "CodigoUsuario";

            ddlResponsavel.DataBind();
        }
        if (!IsPostBack)
            ddlResponsavel.SelectedIndex = 0;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/popupEdicaoTarefaEAP.js""></script>"));
        this.TH(this.TS("popupEdicaoTarefaEAP"));
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (Request.QueryString["projectID"] != null && (Request.QueryString["projectID"] + "") != "")
            IDEdicaoEAP = Request.QueryString["projectID"].ToString();

        if (Request.QueryString["code"] != null && (Request.QueryString["code"] + "") != "")
            code = Request.QueryString["code"];

        if (Request.QueryString["elementName"] != null && (Request.QueryString["elementName"] + "") != "")
            elementName = Uri.UnescapeDataString(Request.QueryString["elementName"].ToString());

        if (Request.QueryString["GUID"] != null && (Request.QueryString["GUID"] + "") != "")
            IdTarefa = Request.QueryString["GUID"];

        if (Request.QueryString["MO"] != null && (Request.QueryString["MO"] + "") != "")
            modoVisualizacao = Request.QueryString["MO"];

        if (!IsPostBack)
        {
            habilitaComponentes(modoVisualizacao != "V");
            cDados.aplicaEstiloVisual(Page);
            
        }
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        ddlResponsavel.JSProperties["cp_NomeResponsavel"] = "";

        if (!IsPostBack)
        {
            if (Request.QueryString["CR"] + "" != "")
            {
                string comandoSQL = string.Format(@"
            SELECT NomeUsuario FROM Usuario WHERE CodigoUsuario = " + Request.QueryString["CR"]);

                DataSet ds = cDados.getDataSet(comandoSQL);

                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    ddlResponsavel.JSProperties["cp_NomeResponsavel"] = ds.Tables[0].Rows[0]["NomeUsuario"].ToString();
            }
        }

        cDados.setaTamanhoMaximoMemo(mmCriterios, 4000, lbl_mmCriterios);
        cDados.setaTamanhoMaximoMemo(txtDicionario, 4000, lbl_mmDicionario);

        //int largura = 0;
        //int altura = 0;
        //cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        //txtDicionario.Height = new Unit((altura - 650) + "px");
        //mmCriterios.Height = new Unit((altura - 650) + "px");

        string displayFormatString = System.Globalization.CultureInfo.CurrentCulture.Name.StartsWith("en")
            ? "MM/dd/yyyy"
            : "dd/MM/yyyy";
        dteInicio.EditFormatString = displayFormatString;
        dteInicio.DisplayFormatString = displayFormatString;
        dteTermino.EditFormatString = displayFormatString;
        dteTermino.DisplayFormatString = displayFormatString;
    }

    private void habilitaComponentes(bool sim)
    {
        bool possuiFilhos = Request.QueryString["PossuiFilhos"] + "" == "S";
        txtTarefa.ClientEnabled = sim;
        dteInicio.ClientEnabled = sim && !possuiFilhos;
        dteTermino.ClientEnabled = sim && !possuiFilhos;
        //txtDuracao.ClientEnabled = sim && !possuiFilhos;
        txtTrabalho.ClientEnabled = sim && !possuiFilhos;
        //txtCustoUnitario.ClientEnabled = sim && !possuiFilhos;
        txtCusto.ClientEnabled = sim && !possuiFilhos;
        txtReceita.ClientEnabled = sim && !possuiFilhos;
        txtPeso.ClientEnabled = sim;
        ddlResponsavel.ClientEnabled = sim;
        mmCriterios.ClientEnabled = sim;
        txtDicionario.ClientEnabled = sim;
        btnSalvar.ClientVisible = sim;        
    }
    /*
    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidade, e.Filter, "");

        cDados.populaComboVirtual(SqlDataSource1, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }

    protected void ddlResponsavel_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;

        ASPxComboBox comboBox = (ASPxComboBox)source;

        SqlDataSource2.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidade);

        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
        comboBox.DataSource = SqlDataSource2;
        comboBox.DataBind();
    }
    */
}

