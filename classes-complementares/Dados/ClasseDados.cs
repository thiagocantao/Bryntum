using CDIS.Properties;
using Crypto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;

namespace CDIS
{
    public class ClasseDados
    {
        #region declarações
        public string databaseNameCdis = "";
        public string OwnerdbCdis = "";

        private string TipoBancoDados = "";
        private string PathDB = "";
        private string IDProduto = "";
        public string nomeUsuario = "";
        public string maquinaUsuario = "";

        

        private char Delimitador_Erro = '¥';

        string strConn = "";
        string collate = "";

        public string erroExecucao;

        private static int _timeOutSqlCommand = 200;
        public static int TimeOutSqlCommand 
        {
            get {
                if (System.Configuration.ConfigurationManager.AppSettings["TimeOutSqlCommand"] != null)
                {
                    _timeOutSqlCommand = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TimeOutSqlCommand"].ToString());
                }
                return _timeOutSqlCommand; 
            } 
           // set => _timeOutSqlCommand = value; 
        }

        #endregion

        #region geral banco de dados

        public ClasseDados(string tipoBancoDados, string pathDB, string ID, string ownerDatabase, string collateName, byte mimPoolSize)
        {
            this.TipoBancoDados = tipoBancoDados.ToLower().Trim();
            this.PathDB = pathDB;
            this.IDProduto = ID;
            this.OwnerdbCdis = ownerDatabase;
            //this.TimeOutSqlCommand = sqlTimeOutInSeconds;
            if (mimPoolSize == 0)
                mimPoolSize = 3;

            strConn = getStringConexao();
            if (tipoBancoDados == "sqlserver")
                strConn += ";Min Pool Size=" + mimPoolSize;

            databaseNameCdis = getDataBaseName(strConn);

            if (collateName != "")
                collate = " Collate " + collateName;
        }

        private string getDataBaseName(string texto)
        {
            string temp = "";
            if (TipoBancoDados == "sqlserver")
            {
                try
                {
                    temp = texto.Substring(texto.IndexOf("Initial"), texto.IndexOf(';', texto.IndexOf("Initial")) - texto.IndexOf("Initial"));
                }
                catch (Exception ex)
                {

                    if (ex.HResult == -2146233086)
                    {
                        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl.Replace(HttpContext.Current.Request.RawUrl, "erroConexaoBancoDeDados.aspx"));
                    }
                }
                temp = temp.Substring(temp.IndexOf("=") + 1).Trim();
            }
            return temp;
        }

        public string getStringConexao()
        {
            return getStringConexao(PathDB, IDProduto);
        }

        public string getStringConexao(string PathDB, string IDProduto)
        {
            CDIS_Crypto oCrypto = new CDIS_Crypto(CDIS_Crypto.SymmProvEnum.Rijndael);
            try
            {
                string chave = IDProduto.Replace("\"", "");
                chave = chave.Replace("{", "");
                chave = chave.Replace("-", "");
                chave = chave.Substring(0, 16);

                return oCrypto.descriptografaString(PathDB, chave);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string getErroIncluiRegistro(string Mensagem)
        {
            string padrao = Delimitador_Erro + Properties.Resources.OcorreuUmErroAoSalvarORegistro;
            return padrao + getMensagemErroAcaoPadrao(Mensagem);
        }

        private string getErroAtualizaRegistro(string Mensagem)
        {
            string padrao = Delimitador_Erro + Resources.OcorreuUmErroAoAtualizarORegistro;
            return padrao + getMensagemErroAcaoPadrao(Mensagem);
        }

        private string getErroExcluiRegistro(string Mensagem)
        {
            string padrao = Delimitador_Erro + Resources.OcorreuUmErroAoExcluirORegistro;
            return padrao + getMensagemErroAcaoPadrao(Mensagem);
        }

        private string getMensagemErroAcaoPadrao(string Mensagem)
        {
            if (Mensagem.IndexOf(Delimitador_Erro) > 0)
                return Mensagem + Delimitador_Erro;
            else
                return Delimitador_Erro + Mensagem;
        }

        public string getDateDB()
        {
            if (TipoBancoDados == "sqlserver")
                return getDateDB_SQLSERVER();
            else if (TipoBancoDados == "access2003")
                return getDateDB_ACCESS2003();
            else
                return "";
        }

        public bool execSQL(string ComandoSQL, ref int registrosAfetados)
        {
            if (TipoBancoDados == "sqlserver")
                return execSQL_SQLSERVER(ComandoSQL, ref registrosAfetados);
            if (TipoBancoDados == "access2003")
                return execSQL_ACCESS2003(ComandoSQL, ref registrosAfetados);
            else
                return false;
        }

        public DataSet getDataSet(string ComandoSQL)
        {
            /* string comandoSQL = "Select getdate() agora ";
             DataSet ds = getDataSet_SQLSERVER(comandoSQL);
             if ((DateTime)ds.Tables[0].Rows[0]["agora"] > new DateTime(2013, 03, 18))
             {
                 throw new Exception("O período de avaliação expirou. Entre em contado com seu fornecedor.");
             }
             */
            if (TipoBancoDados == "sqlserver")
                return getDataSet_SQLSERVER(ComandoSQL);
            else if (TipoBancoDados == "access2003")
                return getDataSet_ACCESS2003(ComandoSQL);
            else
                return null;
        }

        public string getInsert(string tabela, ListDictionary dados)
        {
            return getInsert(databaseNameCdis, OwnerdbCdis, tabela, dados);
        }

        public string getInsert(string banco, string owner, string tabela, ListDictionary dados)
        {
            string comando = "";
            if (TipoBancoDados == "sqlserver")
                comando = string.Format(
                    "INSERT INTO {0}.{1}.{2} (", banco, owner, tabela);
            else if (TipoBancoDados == "access2003")
                comando = string.Format(
                    "INSERT INTO {2} (", banco, owner, tabela);
            else
                return "";

            // inclusão dos campos e valores
            string campos = "";
            string valores = "";
            foreach (DictionaryEntry de in dados)
            {
                campos += de.Key + ", ";
                valores += getValor(de.Value);
            }

            campos = campos.Substring(0, campos.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);

            comando += campos + ") VALUES (" + valores + ")";
            return comando;
        }

        public string getUpdate(string tabela, ListDictionary dados, string where)
        {
            return getUpdate(databaseNameCdis, OwnerdbCdis, tabela, dados, where);
        }

        public string getUpdate(string banco, string owner, string tabela, ListDictionary dados, string where)
        {
            string comando = "";
            if (TipoBancoDados == "sqlserver")
                comando = string.Format(
                    "UPDATE {0}.{1}.{2} SET ", banco, owner, tabela);
            else if (TipoBancoDados == "access2003")
                comando = string.Format(
                    "UPDATE {2} SET ", banco, owner, tabela);
            else
                return "";

            foreach (DictionaryEntry de in dados)
            {
                comando += de.Key + " = " + getValor(de.Value);
            }

            comando = comando.Substring(0, comando.Length - 2);
            if (where != "")
                comando += " WHERE " + where;

            return comando;
        }

        public string getDelete(string tabela, string where)
        {
            return getDelete(databaseNameCdis, OwnerdbCdis, tabela, where);
        }

        public string getDelete(string banco, string owner, string tabela, string where)
        {
            string comando = "";
            if (TipoBancoDados == "sqlserver")
                comando = string.Format(
                    "DELETE FROM {0}.{1}.{2} ", banco, owner, tabela);
            else if (TipoBancoDados == "access2003")
                comando = string.Format(
                    "DELETE FROM {2} ", banco, owner, tabela);
            else
                return "";

            if (where != "")
                comando += " WHERE " + where;

            return comando;
        }

        public string getSelect(string tabela, List<string> colunas, string where, string orderBy)
        {
            return getSelect(databaseNameCdis, OwnerdbCdis, tabela, colunas, where, orderBy);
        }

        public string getSelect(string banco, string owner, string tabela, List<string> colunas, string where, string orderBy)
        {
            string comando = "";
            string sColunas = "*";
            if (colunas != null && colunas.Count > 0)
            {
                sColunas = "";
                foreach (string sCol in colunas)
                {
                    sColunas += sCol + ", ";
                }
                sColunas = sColunas.Substring(0, sColunas.Length - 2);
            }

            if (TipoBancoDados == "sqlserver")
                comando = string.Format(
                    "SELECT {3} FROM {0}.{1}.{2} ", banco, owner, tabela, sColunas);
            else if (TipoBancoDados == "access2003")
                comando = string.Format(
                    "SELECT {3} FROM {2} ", banco, owner, tabela, sColunas);
            else
                return "";

            if (where != "")
                comando += " WHERE " + where;

            if (orderBy != "")
                comando += " ORDER By " + orderBy;

            return comando;
        }

        public DataSet Select(string tabela, List<string> colunas, string where, string orderBy)
        {
            string comandoSQL = getSelect(tabela, colunas, where, orderBy);
            return getDataSet(comandoSQL);
        }
        /// <summary>
        /// Insere o registro na tabela especificada
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="dados"></param>
        /// <param name="returnIdentity"></param>
        /// <returns>Retorna o novo código inserido ou -1 quando não existir identity</returns>
        public int Insert(string tabela, ListDictionary dados, bool returnIdentity)
        {
            string comandoSQL = getInsert(tabela, dados);
            try
            {
                if (returnIdentity)
                {
                    comandoSQL += " SELECT scope_identity() as Retorno";
                    DataSet ds = getDataSet(comandoSQL);
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    int afetados = 0;
                    execSQL(comandoSQL, ref afetados);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + comandoSQL);
            }
        }

        /// <summary>
        /// Atualiza o registro 
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="dados"></param>
        /// <param name="where"></param>
        /// <returns>Quantidade de registros atualizados</returns>
        public int update(string tabela, ListDictionary dados, string where)
        {
            string comandoSQL = getUpdate(tabela, dados, where);
            try
            {
                int afetados = 0;
                execSQL(comandoSQL, ref afetados);
                return afetados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + '\n' + comandoSQL);
            }
        }

        /// <summary>
        /// Exclui o registro
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="where"></param>
        /// <returns>Quantidade de registros excluídos</returns>
        public int delete(string tabela, string where)
        {
            string comandoSQL = getDelete(tabela, where);
            try
            {
                int afetados = 0;
                execSQL(comandoSQL, ref afetados);
                return afetados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + '\n' + comandoSQL);
            }
        }

        private string getValor(object oValue)
        {
            string retorno = "";
            if (oValue == null)
                retorno = "null, ";
            else
            {
                if (oValue.GetType().FullName == "System.String")
                    retorno = getSQL_ValorString(oValue.ToString()) + ", ";
                else if (oValue.GetType().FullName == "System.DateTime")
                    retorno = getSQL_ValorData((DateTime)oValue) + ", ";
                else if (oValue.GetType().FullName.Substring(0, 10) == "System.Int")
                    retorno = getSQL_ValorInt(oValue.ToString()) + ", ";
                else if (oValue.GetType().FullName == "System.Decimal")
                    retorno = getSQL_ValorDecimal((Decimal)oValue) + ", ";
                else
                    retorno = getSQL_ValorString(oValue.ToString()) + ", ";
            }
            return retorno;
        }

        private string getSQL_ValorString(string texto)
        {
            if (texto.ToLower() == "null")
                return "null";
            else if (texto.ToLower() == "getdate()")
                return "GETDATE()";
            else
                return "'" + texto.Replace("'", "''") + "'";
        }

        private string getSQL_ValorInt(string valor)
        {
            return valor.ToString();
        }

        private string getSQL_ValorDecimal(decimal valor)
        {
            return valor.ToString().Replace(',', '.');
        }

        private string getSQL_ValorData(DateTime data)
        {
            string dia = data.Day.ToString();
            string mes = data.Month.ToString();
            string ano = data.Year.ToString();
            string hora = data.Hour.ToString();
            string minuto = data.Minute.ToString();
            string segundo = data.Second.ToString();
            string milisegundo = data.Millisecond.ToString();

            if (TipoBancoDados == "sqlserver")
            {
                return string.Format(" Convert(datetime, '{0}/{1}/{2} {3}:{4}:{5}.{6}', 103) ", dia, mes, ano, hora, minuto, segundo, milisegundo);
            }
            else
                return string.Format("#{0}/{1}/{2} {3}:{4}:{5}#", mes, dia, ano, hora, minuto, segundo);
        }

        #region SQL SERVER

        private string getDateDB_SQLSERVER()
        {
            string comandoSQL = "SELECT convert(varchar,GetDate(), 103) + ' ' + convert(varchar,GetDate(), 108)";
            DataSet ds = getDataSet(comandoSQL);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        private bool execSQL_SQLSERVER(string ComandoSQL, ref int registrosAfetados)
        {
            SqlConnection oConn = null;
            SqlCommand oCmd = null;
            int tentativas = 0;
            int maxTentativas = 1;
            while (tentativas < maxTentativas)
            {
                try
                {
                    oConn = new SqlConnection(strConn + ";Application Name=" + nomeUsuario + ";WORKSTATION ID=" + maquinaUsuario);
                    oCmd = new SqlCommand(ComandoSQL, oConn);
                    oCmd.CommandTimeout = TimeOutSqlCommand;
                    oConn.Open();
                    registrosAfetados = oCmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    tentativas++;
                    if (tentativas == maxTentativas)
                    {
                        oConn.Close();
                        oCmd.Dispose();
                        oConn.Dispose();
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oConn.Close();
                    oConn.Dispose();
                    oCmd.Dispose();
                }
            }
            return false;
        }

        private DataSet getDataSet_SQLSERVER(string ComandoSQL)
        {
            SqlConnection cn = null;
            SqlCommand oCmd = null;
            SqlDataAdapter dataAdapter = null;
            DataSet ds = null;
            int tentativas = 0;
            int maxTentativas = 1;
            while (tentativas < maxTentativas)
            {
                try
                {
                    // se o objeto já existe...
                    if (cn != null)
                        cn.Close();

                    // Cria a conexao com o banco
                    cn = new SqlConnection(strConn + ";Application Name=" + nomeUsuario + ";WORKSTATION ID=" + maquinaUsuario);
                    // cn = new SqlConnection(strConn);
                    oCmd = new SqlCommand(ComandoSQL, cn);
                    oCmd.CommandTimeout = TimeOutSqlCommand;

                    // Abre a conexão 
                    cn.Open();

                    // Cria um DataAdapter, com base no comando SQL fornecido.
                    dataAdapter = new SqlDataAdapter(oCmd);
                    ds = new DataSet();

                    // Preenche  o DataSet, com as informações contidas no dataAdapter.
                    dataAdapter.Fill(ds, "rs");
                    return ds;
                }
                catch (SqlException ex)
                {
                    tentativas++;
                    if (tentativas == maxTentativas)
                    {
                        if (ds != null && dataAdapter != null)
                        {
                            ds.Dispose();
                            dataAdapter.Dispose();
                        }
                        else
                        {
                            string nomeServidor = "", nomeBanco = "", nomeUsuario = "";

                            nomeServidor = strConn.Substring(strConn.IndexOf("Data"), strConn.IndexOf(';', strConn.IndexOf("Data")) - strConn.IndexOf("Data"));
                            nomeBanco = strConn.Substring(strConn.IndexOf("Initial"), strConn.IndexOf(';', strConn.IndexOf("Initial")) - strConn.IndexOf("Initial"));
                            nomeUsuario = strConn.Substring(strConn.IndexOf("User"), strConn.IndexOf(';', strConn.IndexOf("User")) - strConn.IndexOf("User"));

                            erroExecucao = "¥" + Resources.NãoFoiPossívelConectarAoBancoDeDadosVerifi + " ¥" + nomeServidor + "¥" +
                                nomeBanco + "¥" +
                                nomeUsuario;
                        }

                        cn.Close();
                        cn.Dispose();
                        oCmd.Dispose();

                        throw ex;
                    }
                }
                catch // erro que não sejam de banco
                {
                    throw;
                }
                finally
                {
                    if (ds != null)
                        ds.Dispose();
                    if (dataAdapter != null)
                        dataAdapter.Dispose();
                    if (cn != null)
                    {
                        cn.Close();
                        cn.Dispose();
                    }
                    if (oCmd != null)
                        oCmd.Dispose();
                }
            }
            return null;
        }

        #endregion

        #region ACCESS

        private string getDateDB_ACCESS2003()
        {
            string comandoSQL = "SELECT Min(Now()) FROM MSysObjects";
            DataSet ds = getDataSet(comandoSQL);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        private bool execSQL_ACCESS2003(string ComandoSQL, ref int registrosAfetados)
        {
            OleDbConnection oConn = null;
            OleDbCommand oCmd = null;
            int tentativas = 0;
            int maxTentativas = 3;
            while (tentativas < maxTentativas)
            {
                try
                {
                    oConn = new OleDbConnection(strConn);
                    oCmd = new OleDbCommand(ComandoSQL, oConn);
                    oCmd.CommandTimeout = TimeOutSqlCommand;
                    oConn.Open();
                    registrosAfetados = oCmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    tentativas++;
                    if (tentativas == maxTentativas)
                    {
                        oConn.Close();
                        oCmd.Dispose();
                        oConn.Dispose();
                        throw ex;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    oConn.Close();
                    oConn.Dispose();
                    oCmd.Dispose();
                }
            }
            return false;
        }

        private DataSet getDataSet_ACCESS2003(string ComandoSQL)
        {
            OleDbConnection cn = null;
            OleDbCommand oCmd = null;
            OleDbDataAdapter dataAdapter = null;
            DataSet ds = null;
            int tentativas = 0;
            int maxTentativas = 3;
            while (tentativas < maxTentativas)
            {
                try
                {
                    // se o objeto já existe...
                    if (cn != null)
                        cn.Close();

                    // Cria a conexao com o banco
                    cn = new OleDbConnection(strConn);
                    oCmd = new OleDbCommand(ComandoSQL, cn);
                    oCmd.CommandTimeout = TimeOutSqlCommand;

                    // Abre a conexão 
                    cn.Open();

                    // Cria um DataAdapter, com base no comando SQL fornecido.
                    dataAdapter = new OleDbDataAdapter(oCmd);
                    ds = new DataSet();

                    // Preenche  o DataSet, com as informações contidas no dataAdapter.
                    dataAdapter.Fill(ds, "rs");
                    return ds;
                }
                catch (SqlException ex)
                {
                    tentativas++;
                    if (tentativas == maxTentativas)
                    {
                        if (ds != null && dataAdapter != null)
                        {
                            ds.Dispose();
                            dataAdapter.Dispose();
                        }
                        else
                        {
                            string nomeServidor = "", nomeBanco = "", nomeUsuario = "";

                            nomeServidor = strConn.Substring(strConn.IndexOf("Data"), strConn.IndexOf(';', strConn.IndexOf("Data")) - strConn.IndexOf("Data"));
                            nomeBanco = strConn.Substring(strConn.IndexOf("Initial"), strConn.IndexOf(';', strConn.IndexOf("Initial")) - strConn.IndexOf("Initial"));
                            nomeUsuario = strConn.Substring(strConn.IndexOf("User"), strConn.IndexOf(';', strConn.IndexOf("User")) - strConn.IndexOf("User"));

                            erroExecucao = "¥" + Resources.NãoFoiPossívelConectarAoBancoDeDadosVerifi + " ¥" + nomeServidor + "¥" +
                                nomeBanco + "¥" +
                                nomeUsuario;
                        }

                        cn.Close();
                        cn.Dispose();
                        oCmd.Dispose();

                        throw ex;
                    }
                }
                catch // erro que não sejam de banco
                {
                    throw;
                }
                finally
                {
                    if (ds != null)
                        ds.Dispose();
                    if (dataAdapter != null)
                        dataAdapter.Dispose();
                    if (cn != null)
                    {
                        cn.Close();
                        cn.Dispose();
                    }
                    if (oCmd != null)
                        oCmd.Dispose();
                }
            }
            return null;
        }

        #endregion

        #endregion

    }
}

