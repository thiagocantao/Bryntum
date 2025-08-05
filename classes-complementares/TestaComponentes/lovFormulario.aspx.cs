using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class lovFormulario : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto;
    int codigoEntidade;
    int codigoUsuarioResponsavel;
    int CodigoWorkflow;
    int CodigoInstanciaWorkflow;

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = new dados();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["CL"]==null || Request.QueryString["CL"]=="")
            return;

        inicializaVariavies();

        int codigoListaPreDefinida = int.Parse(Request.QueryString["CL"]);
        string valor = Request.QueryString["V"] != null ? Request.QueryString["V"] : "";
        populaConteudoCampoLOO_Lov(cmbLov, codigoListaPreDefinida, "");
        if (valor != "")
            cmbLov.Value = valor;
    }

    private void inicializaVariavies()
    {
        codigoProjeto = -1;
        codigoEntidade = 1;
        codigoUsuarioResponsavel = 1;
        CodigoWorkflow = -1;
        CodigoInstanciaWorkflow = -1;
    }

    private bool populaConteudoCampoLOO_Lov(ASPxComboBox controle, int codigoLookup, string valor)
    {
        controle.Items.Clear();
        string comandoSQL = getComandoTabelaLookup(codigoLookup, "").ToUpper();

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null)
        {
            controle.DataSource = ds;
            controle.TextField = "Descricao";
            controle.ValueField = "Codigo";
            controle.DataBind();
            return true;
        }
        return false;
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

}