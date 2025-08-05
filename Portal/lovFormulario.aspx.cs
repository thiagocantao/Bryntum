using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class lovFormulario : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto;
    int codigoEntidade;
    int codigoUsuarioResponsavel;
    int CodigoWorkflow;
    int CodigoInstanciaWorkflow;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        SqlDataSource1.ConnectionString = cDados.classeDados.getStringConexao();
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        if (Request.QueryString["CL"] == null || Request.QueryString["CL"] == "")
            return;

        inicializaVariavies();

        int codigoListaPreDefinida = int.Parse(Request.QueryString["CL"]);
        string valor = Request.QueryString["Value"] != null ? Request.QueryString["Value"] : "";
        string texto = Request.QueryString["Text"] != null ? Request.QueryString["Text"] : "";
        
        populaConteudoCampoLOO_Lov(cmbLov, codigoListaPreDefinida, "");

        if (valor != "")
        {
            cmbLov.Value = valor;
            cmbLov.Text = texto;
        }

        cDados.aplicaEstiloVisual(this);
    }

    private void inicializaVariavies()
    {
        codigoProjeto = -1;
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoWorkflow = -1;
        CodigoInstanciaWorkflow = -1;

        if (Request.QueryString["CPWF"] != null)
        {
            codigoProjeto = int.Parse(Request.QueryString["CPWF"].ToString());
        }

        if (Request.QueryString["CWF"] != null)
        {
            CodigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());
            CodigoInstanciaWorkflow = int.Parse(Request.QueryString["CIWF"].ToString());
        }
    }

    private bool populaConteudoCampoLOO_Lov(ASPxComboBox controle, int codigoLookup, string valor)
    {
        //controle.Items.Clear();
        //string comandoSQL = getComandoTabelaLookup(codigoLookup, "").ToUpper();

        //DataSet ds = cDados.getDataSet(comandoSQL);
        //if (ds != null && ds.Tables[0] != null)
        //{
        //    controle.DataSource = ds;
            controle.TextField = "Descricao";
            controle.ValueField = "Codigo";
            //controle.DataBind();
            return true;
        //}
        //return false;
    }

    private string getComandoTabelaLookup(int codigoListaPre, string iniciaisLookup)
    {
        // monta o WHERE de pesquisa na tabela Lookup. Se o código for menor ou igual a zero, utilizaremos as iniciais
        string where = " WHERE CodigoLookup = " + codigoListaPre;
        if (codigoListaPre <= 0 && iniciaisLookup.Trim() != "")
            where = "WHERE IniciaisLookup = '" + iniciaisLookup + "' ";

        // busca o comando sql para recuperar o valor do campo pré-definido
        string comandoSQL = string.Format(
            @"SELECT ComandoRetornoLookup
                    FROM {0}.{1}.Lookup
                     {2}", cDados.classeDados.databaseNameCdis, cDados.classeDados.OwnerdbCdis, where);

        DataSet ds = cDados.getDataSet(comandoSQL);
        string ComandoRetornoCampo = "";
        if (ds != null && ds.Tables[0] != null)
        {
            ComandoRetornoCampo = ds.Tables[0].Rows[0]["ComandoRetornoLookup"].ToString();
            ComandoRetornoCampo = ajustaComandoSQLCamposLookup_E_CampoPreDefinido(ComandoRetornoCampo);
        }

        return ComandoRetornoCampo;
    }

    private string ajustaComandoSQLCamposLookup_E_CampoPreDefinido(string comandoSQLOriginal)
    {
        string comandoSQL = string.Format(
            @"SELECT De, Para
                    FROM {0}.{1}.DeParaObjetosDB
                   ORDER By De", cDados.classeDados.databaseNameCdis, cDados.classeDados.OwnerdbCdis);
        DataSet ds = cDados.getDataSet(comandoSQL);
        DataTable dt = ds.Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            int De = int.Parse(dr["De"].ToString());
            string Para = dr["Para"].ToString().ToUpper();
            // se for campo reservado - #CodigoProjeto#, #Entidade#, #Usuario#
            if (Para == "#CODIGOPROJETO#")
            {
                comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoProjeto.ToString());
            }
            else if (Para == "#ENTIDADE#")
            {
                comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoEntidade.ToString());
            }
            else if (Para == "#USUARIO#")
            {
                comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoUsuarioResponsavel.ToString());
            }
            else if (Para == "#CODIGOWORKFLOW#")
            {
                comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", CodigoWorkflow.ToString());
            }
            else if (Para == "#CODIGOINSTANCIAWF#")
            {
                comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", CodigoInstanciaWorkflow.ToString());
            }
            else
                comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", Para);
        }

        return comandoSQLOriginal;
    }

    protected void ddlListaPre_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        int codigoListaPre = int.Parse(Request.QueryString["CL"].ToString());

        if (e.Value != null)
        {
            // monta o WHERE de pesquisa na tabela Lookup. Se o código for menor ou igual a zero, utilizaremos as iniciais
            string where = " WHERE CodigoLookup = " + codigoListaPre;

            // busca o comando sql para recuperar o valor do campo pré-definido
            string comandoSQL = string.Format(
                @"SELECT ComandoRetornoLookup
                    FROM {0}.{1}.Lookup
                     {2}", cDados.getDbName(), cDados.getDbOwner(), where);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (ds != null && ds.Tables[0] != null)
            {
                string ComandoRetornoCampo = ds.Tables[0].Rows[0]["ComandoRetornoLookup"].ToString();

                ComandoRetornoCampo = ajustaComandoSQLCamposLookup_E_CampoPreDefinido(ComandoRetornoCampo);
                // executa o comando pré-definido
                try
                {
                    if (ComandoRetornoCampo.ToUpper().IndexOf("ORDER BY") != -1)
                    {
                        ComandoRetornoCampo = ComandoRetornoCampo.Substring(0, ComandoRetornoCampo.ToUpper().IndexOf("ORDER BY"));
                    }

                    dsResponsavel.SelectCommand = string.Format(@"SELECT u.Codigo, u.Descricao
                                 FROM ({0}) as u
                                WHERE u.Codigo = @ID
                                ORDER BY u.Descricao", ComandoRetornoCampo);

                    dsResponsavel.SelectParameters.Clear();
                    dsResponsavel.SelectParameters.Add("ID", TypeCode.String, e.Value.ToString());
                    comboBox.DataSource = dsResponsavel;

                    comboBox.DataBind();

                }
                catch { }
            }
        }
    }

    protected void ddlListaPre_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        int codigoListaPre = int.Parse(Request.QueryString["CL"].ToString());

        // monta o WHERE de pesquisa na tabela Lookup. Se o código for menor ou igual a zero, utilizaremos as iniciais
        string where = " WHERE CodigoLookup = " + codigoListaPre;

        // busca o comando sql para recuperar o valor do campo pré-definido
        string comandoSQL = string.Format(
            @"SELECT ComandoRetornoLookup
                    FROM {0}.{1}.Lookup
                     {2}", cDados.getDbName(), cDados.getDbOwner(), where);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (ds != null && ds.Tables[0] != null)
        {
            string ComandoRetornoCampo = ds.Tables[0].Rows[0]["ComandoRetornoLookup"].ToString();

            ComandoRetornoCampo = ajustaComandoSQLCamposLookup_E_CampoPreDefinido(ComandoRetornoCampo);
            // executa o comando pré-definido
            try
            {
                if (ComandoRetornoCampo.ToUpper().IndexOf("ORDER BY") != -1)
                {
                    ComandoRetornoCampo = ComandoRetornoCampo.Substring(0, ComandoRetornoCampo.ToUpper().IndexOf("ORDER BY"));
                }

                string comandoParam = string.Format(@"SELECT c.Codigo, c.Descricao, ROW_NUMBER()over(order by c.Descricao) as rn
                                 FROM ({0}) as c
                                WHERE c.Descricao LIKE '%{1}%'", ComandoRetornoCampo, e.Filter);

                populaComboVirtual(SqlDataSource1, comandoParam, comboBox, e.BeginIndex, e.EndIndex);

            }
            catch { }
        }
    }

    public void populaComboVirtual(SqlDataSource ds, string comandoSQL, ASPxComboBox comboBox, int startIndex, int endIndex)
    {
        ds.SelectCommand =
               string.Format(@"SELECT *
                                 FROM ({0}) as u
                                WHERE u.rn Between @startIndex and @endIndex                                
                                ORDER BY u.Descricao", comandoSQL);

        ds.SelectParameters.Clear();

        ds.SelectParameters.Add("startIndex", TypeCode.Int64, (startIndex + 1).ToString());
        ds.SelectParameters.Add("endIndex", TypeCode.Int64, (endIndex + 1).ToString());

        comboBox.DataSource = ds;
        comboBox.DataBind();
    }
}