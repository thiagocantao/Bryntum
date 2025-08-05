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

public partial class lov : System.Web.UI.Page
{
    dados cDados;
    string tabela;
    string colunaValor;
    string colunaNome;
    string where;
    string order;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HeaderOnTela();
        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
        }

        /*
         ======================================================================================================================
         ATENÇÂO: Para utilizar esta tela, É OBRIGATÓRIO que os parâmetros: tab, val, nom sejam passados pela URL (QueryString)
         ======================================================================================================================
         * tab = nome da tabela no banco de dados onde será feita a pesquisa
         * val = nome da coluna que possui o valor identificador
         * nom = nome da coluna que possui o texto a ser apresentado na grid para seleção
         * 
         Parametros opcionais:
         * whe = clausúla where (Ex: "DataExclusao is null")
         * ord = coluna(s) para a clausula order by
         * pes = valor a ser utilizado na primeira pesquisa
         * 
         */

        if (Request.QueryString["tab"] == null || Request.QueryString["val"] == null && Request.QueryString["nom"] == null ||
            Request.QueryString["tab"].ToString() == "" || Request.QueryString["val"].ToString() == "" && Request.QueryString["nom"].ToString() == "")
        {
            throw new Exception("Os parâmetros estão incompletos");
        }

        tabela = Request.QueryString["tab"].ToString();
        colunaValor = Request.QueryString["val"].ToString();
        colunaNome = Request.QueryString["nom"].ToString();

        if (Request.QueryString["whe"] != null)
            where = " AND " + Request.QueryString["whe"].ToString();

        if (Request.QueryString["ord"] != null)
            order = Request.QueryString["ord"].ToString();

        if (!IsPostBack && !IsCallback)
        {
            if (Session["Pesquisa"] != null && Session["Pesquisa"].ToString() != "")
            {
                txtNome.Text = Session["Pesquisa"].ToString();
                executarPesquisa(txtNome.Text);
            }
            else
            {
                if (Request.QueryString["pes"] != null && Request.QueryString["pes"].ToString() != "")
                {
                    txtNome.Text = Request.QueryString["pes"].ToString();
                    executarPesquisa(txtNome.Text);
                }
            }            
        }
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/lov.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("lov", "_Strings"));

    }

    protected void gvResultado_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        
        if (e.Parameters.Length >= 9 && e.Parameters.Substring(0, 9) == "Pesquisar")
        {
            string pesquisarPor = e.Parameters.Substring(10);
            executarPesquisa(pesquisarPor);
        }
        if (e.Parameters.Length > 0 && e.Parameters == "doOnload")
            executarPesquisa(txtNome.Text);
    }

    private void executarPesquisa(string pesquisarPor)
    {
        string valorRetorno = "";
        string nomeRetorno = "";

        DataSet ds = cDados.getLov_NomeValor(tabela, colunaValor, colunaNome, pesquisarPor, true, where, order, out valorRetorno, out nomeRetorno);
        if (ds != null)
        {
            gvResultado.DataSource = ds.Tables[0];
            gvResultado.DataBind();
        }
    }
}
