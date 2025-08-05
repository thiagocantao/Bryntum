using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class administracao_configuraConexaoMySql : System.Web.UI.Page
{
    private dados cDados;
    private MySqlConnection bdConn; //MySQL
    string connStr;
    int codigoEntidade;
    int idUsuarioLogado;

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

        idUsuarioLogado = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado"));
        codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // se não é postback, procura no banco pelos valores dos parametros.
        if (!IsPostBack)
        {
            // Procura pelos valores na tabela de parametro do sistema
            string comandoSql = string.Format(
                @"SELECT parametro, valor 
                FROM ParametroConfiguracaoSistema 
               WHERE CodigoEntidade = 1
                 AND Parametro like 'AutenticaoExternaBD_%'
               ORDER BY Parametro");

            DataSet ds = cDados.getDataSet(comandoSql);
            if (cDados.DataSetOk(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["parametro"] + "" == "AutenticaoExternaBD_Servidor")
                        txtServidor.Text = row["valor"].ToString();
                    else if (row["parametro"] + "" == "AutenticaoExternaBD_Porta")
                        txtPorta.Text = row["valor"].ToString();
                    else if (row["parametro"] + "" == "AutenticaoExternaBD_Banco")
                        txtBanco.Text = row["valor"].ToString();
                    else if (row["parametro"] + "" == "AutenticaoExternaBD_Usuario")
                        txtUsuario.Text = row["valor"].ToString();
                    else if (row["parametro"] + "" == "AutenticaoExternaBD_Senha")
                        txtSenha.Text = row["valor"].ToString();
                }
                cbSalvar.Checked = false;
            }
            else
                cbSalvar.Checked = true;
        }
    }

    protected void btnTestarConexao_Click(object sender, EventArgs e)
    {
        // constrói a string de conexão com o mysql
        connStr = string.Format("server={0};port={1};user={2};password={3};database={4}", txtServidor.Text, txtPorta.Text, txtUsuario.Text, txtSenha.Text, txtBanco.Text);
        bdConn = new MySqlConnection(connStr);

        //tenta abrir uma conexão com o mySql
        try
        {
            bdConn.Open();
            lblResultadoTesteConexao.Text = "Conexão realizada com sucesso.";
            bdConn.Close();

            // se marcou para salvar após o teste...
            if (cbSalvar.Checked)
            {
                // os parametros devem ser salvos na entidade 1
                codigoEntidade = 1;

                // salva os parametros no banco de dados
                string comandoSQL = string.Format(
                    @"declare @codigoEntidade int
                  declare @servidor varchar(100)
                  declare @porta int
                  declare @banco varchar(100)
                  declare @usuario varchar(100)
                  declare @senha varchar(100)
                  declare @DescricaoParametro varchar(200)
                  declare @Parametro varchar(50)
                  declare @ValorParametro varchar(50)

                  set @codigoEntidade = {0}
                  set @servidor = '{1}'
                  set @porta = {2}
                  set @banco = '{3}'
                  set @usuario = '{4}'
                  set @senha = '{5}'
 
                  SET @Parametro = 'AutenticaoExternaBD_Servidor'
                  SET @DescricaoParametro = 'Servidor Banco de dados para autenticação externa'
                  SET @ValorParametro = @servidor
                  -------------------------------------------------------------------
                  if not exists (select 1 from ParametroConfiguracaoSistema where codigoentidade = @codigoEntidade and Parametro = @Parametro)
                     INSERT INTO ParametroConfiguracaoSistema (CodigoEntidade, Parametro, Valor, DescricaoParametro_PT, DescricaoParametro_EN, DescricaoParametro_ES, IndicaControladoSistema)
	                 VALUES (@codigoEntidade, @Parametro, @ValorParametro, @DescricaoParametro, @DescricaoParametro, @DescricaoParametro, 'N')
                  else
                     UPDATE ParametroConfiguracaoSistema
                        SET Valor = @ValorParametro
                      WHERE CodigoEntidade = @codigoEntidade
                        AND Parametro = @Parametro

                  SET @Parametro = 'AutenticaoExternaBD_Porta'
                  SET @DescricaoParametro = 'Porta Banco de dados para autenticação externa'
                  SET @ValorParametro = @porta
                  -------------------------------------------------------------------
                  if not exists (select 1 from ParametroConfiguracaoSistema where codigoentidade = @codigoEntidade and Parametro = @Parametro)
                     INSERT INTO ParametroConfiguracaoSistema (CodigoEntidade, Parametro, Valor, DescricaoParametro_PT, DescricaoParametro_EN, DescricaoParametro_ES, IndicaControladoSistema)
	                 VALUES (@codigoEntidade, @Parametro, @ValorParametro, @DescricaoParametro, @DescricaoParametro, @DescricaoParametro, 'N')
                  else
                     UPDATE ParametroConfiguracaoSistema
                        SET Valor = @ValorParametro
                      WHERE CodigoEntidade = @codigoEntidade
                        AND Parametro = @Parametro

                  SET @Parametro = 'AutenticaoExternaBD_Banco'
                  SET @DescricaoParametro = 'Banco de dados para autenticação externa'
                  SET @ValorParametro = @banco
                  -------------------------------------------------------------------
                  if not exists (select 1 from ParametroConfiguracaoSistema where codigoentidade = @codigoEntidade and Parametro = @Parametro)
                     INSERT INTO ParametroConfiguracaoSistema (CodigoEntidade, Parametro, Valor, DescricaoParametro_PT, DescricaoParametro_EN, DescricaoParametro_ES, IndicaControladoSistema)
	                 VALUES (@codigoEntidade, @Parametro, @ValorParametro, @DescricaoParametro, @DescricaoParametro, @DescricaoParametro, 'N')
                  else
                     UPDATE ParametroConfiguracaoSistema
                        SET Valor = @ValorParametro
                      WHERE CodigoEntidade = @codigoEntidade
                        AND Parametro = @Parametro

                  SET @Parametro = 'AutenticaoExternaBD_Usuario'
                  SET @DescricaoParametro = 'Usuario Banco de dados para autenticação externa'
                  SET @ValorParametro = @usuario
                  -------------------------------------------------------------------
                  if not exists (select 1 from ParametroConfiguracaoSistema where codigoentidade = @codigoEntidade and Parametro = @Parametro)
                     INSERT INTO ParametroConfiguracaoSistema (CodigoEntidade, Parametro, Valor, DescricaoParametro_PT, DescricaoParametro_EN, DescricaoParametro_ES, IndicaControladoSistema)
	                 VALUES (@codigoEntidade, @Parametro, @ValorParametro, @DescricaoParametro, @DescricaoParametro, @DescricaoParametro, 'N')
                  else
                     UPDATE ParametroConfiguracaoSistema
                        SET Valor = @ValorParametro
                      WHERE CodigoEntidade = @codigoEntidade
                        AND Parametro = @Parametro

                  SET @Parametro = 'AutenticaoExternaBD_Senha'
                  SET @DescricaoParametro = 'Senha Banco de dados para autenticação externa'
                  SET @ValorParametro = @senha
                  -------------------------------------------------------------------
                  if not exists (select 1 from ParametroConfiguracaoSistema where codigoentidade = @codigoEntidade and Parametro = @Parametro)
                     INSERT INTO ParametroConfiguracaoSistema (CodigoEntidade, Parametro, Valor, DescricaoParametro_PT, DescricaoParametro_EN, DescricaoParametro_ES, IndicaControladoSistema)
	                 VALUES (@codigoEntidade, @Parametro, @ValorParametro, @DescricaoParametro, @DescricaoParametro, @DescricaoParametro, 'N')
                  else
                     UPDATE ParametroConfiguracaoSistema
                        SET Valor = @ValorParametro
                      WHERE CodigoEntidade = @codigoEntidade
                        AND Parametro = @Parametro", codigoEntidade, txtServidor.Text, txtPorta.Text, txtBanco.Text, txtUsuario.Text, txtSenha.Text);

                int regAfetados = 0;
                cDados.execSQL(comandoSQL, ref regAfetados);
            }
            
        }
        catch (Exception ex)
        {
            lblResultadoTesteConexao.Text = "Erro ao tentar conectar ao mysql: " + ex.Message;
        }
        finally
        {
            bdConn.Dispose();
        }
    }
}