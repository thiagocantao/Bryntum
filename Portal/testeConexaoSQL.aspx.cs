using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class testeConexaoSQL : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblResultado.Text = "";
        lblStringConexao.Text = "";
    }
    protected void btnGerarStringConexao_Click(object sender, EventArgs e)
    {

        lblStringConexao.Text = obtemStringConexao();
        //Data Source=NEDFGRE\CDIS;Initial Catalog=dbCDISportal; User ID=usr_dbCdisPortal;Password=NE12345678!
    }

    protected void btnTestarCoxexao_Click(object sender, EventArgs e)
    {
        lblStringConexao.Text = obtemStringConexao();
        testarSqlServer(lblStringConexao.Text);
    }

    private string obtemStringConexao()
    {
        string datasource = "";
        if (txtServidor.Text != "")
            datasource = txtServidor.Text;

        if (txtInstancia.Text != "")
        {
            if (datasource != "")
                datasource += "\\";
            datasource += txtInstancia.Text;
        }

        if (txtPorta.Text != "")
            datasource += "," + txtPorta.Text;


        string strConexao = string.Format(
            @"Data Source={0};Initial Catalog={1}; User ID={2};Password={3}", datasource, txtBanco.Text, txtUsuario.Text, txtSenha.Text);

        return strConexao;
        
    }
    

    public bool testarSqlServer(string strConexao)
    {
        SqlConnection oConn;
        try
        {
            oConn = new SqlConnection(strConexao);
            oConn.Open();
            oConn.Close();
            lblResultado.Text = "Conexão realizada com sucesso no banco de dados SQL Server";
            return true;
        }
        catch (Exception ex)
        {
            lblResultado.Text = "Não foi possível conectar ao banco de dados SQL Server.\n" + ex.Message;
            return false;
        }
    }
}