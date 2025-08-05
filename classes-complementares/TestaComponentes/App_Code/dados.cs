using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CDIS;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections;
using DevExpress.Web;

/// <summary>
/// Summary description for myDados
/// </summary>
public class dados: System.Web.UI.Page
{
    #region definição da classe

    public ClasseDados classeDados;
    private string tipoBancoDados = System.Configuration.ConfigurationManager.AppSettings["tipoBancoDados"].ToString();
    private string PathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
    private string IDProduto = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();

    private string bancodb = System.Configuration.ConfigurationManager.AppSettings["dbBanco"].ToString();
    private string Ownerdb = System.Configuration.ConfigurationManager.AppSettings["dbOwner"].ToString();

    string strConn = "";

    public dados()
    {
        classeDados = getClasseDados();
        strConn = classeDados.getStringConexao() + ";Min Pool Size=2";
    }

    private ClasseDados getClasseDados()
    {
        return new ClasseDados(tipoBancoDados, PathDB, IDProduto, Ownerdb, "", 2, 200);
    }

    public DataSet getDataSet(string comandoSQL)
    {
        return classeDados.getDataSet(comandoSQL);
    }

    public bool execSQL(string comandoSQL, ref int registrosAfetados)
    {
        return classeDados.execSQL(comandoSQL, ref registrosAfetados);
    }

    public bool DataSetOk(DataSet dataSet)
    {
        return (dataSet == null || dataSet.Tables.Count == 0) ? false : true;
    }

    public bool DataTableOk(DataTable dataTable)
    {
        return (dataTable == null || dataTable.Rows.Count == 0) ? false : true;
    }

    public void PopulaDropDownList(DataTable dtDados, string campoValor, string campoTexto, string itemTODOS, ref  DropDownList dropDown)
    {
        // Limpa o DropDwon
        dropDown.Items.Clear();

        // Verifica se existem dados a serem inseridos no DopDown
        if (dtDados == null)
        {
            return;
        }

        // Verififa se deve ser inserido o item 'TODOS'
        if (itemTODOS != "")
        {
            dropDown.Items.Add(itemTODOS);
        }

        // Insere os ítens no DropDwon
        foreach (DataRow dr in dtDados.Rows)
        {
            dropDown.Items.Add(new ListItem(dr[campoTexto].ToString(), dr[campoValor].ToString()));
        }

        dropDown.SelectedIndex = 0;
    }

    public void PopulaDropDownASPx(DataTable dtDados, string campoValor, string campoTexto, string itemTODOS, ref  ASPxComboBox dropDown)
    {
        // Limpa o DropDwon
        dropDown.Items.Clear();

        // Verifica se existem dados a serem inseridos no DopDown
        if (dtDados == null)
        {
            return;
        }

        // Verififa se deve ser inserido o item 'TODOS'
        if (itemTODOS != "")
        {
            dropDown.Items.Add(itemTODOS);

        }

        // Insere os ítens no DropDwon
        foreach (DataRow dr in dtDados.Rows)
        {
            //dropDown.Items.Add(new ListItem(dr[campoTexto].ToString(), dr[campoValor].ToString()));
            dropDown.Items.Add(new ListEditItem(dr[campoTexto].ToString(), dr[campoValor].ToString()));
        }

        dropDown.SelectedIndex = 0;
    }


    #endregion

    #region Informações do sistema - Session

    private OrderedDictionary getInfoSistema()
    {
        if (Session["infoSistema"] != null)
            return (OrderedDictionary)Session["infoSistema"];
        else
            return null;
    }

    public object getInfoSistema(string chave)
    {
        OrderedDictionary infoSistema = getInfoSistema();
        if (infoSistema == null || (!infoSistema.Contains(chave)))
            return null;
        else
            return infoSistema[chave];
    }

    private void setInfoSistema(OrderedDictionary infoSistema)
    {
        Session["infoSistema"] = infoSistema;
    }

    public void setInfoSistema(string chave, object valor)
    {
        OrderedDictionary infoSistema = getInfoSistema();
        if (infoSistema == null)
            infoSistema = new OrderedDictionary();

        if (!infoSistema.Contains(chave))
            infoSistema.Add(chave, valor);
        else
            infoSistema[chave] = valor;

        setInfoSistema(infoSistema);
    }

    public void clearInfoSistema()
    {
        Session["infoSistema"] = null;
        Session.Remove("infoSistema");
    }

    #endregion

    #region funçoes que não estão relacionadas com banco de dados

    public Literal getLiteral(string texto)
    {
        Literal myLiteral = new Literal();
        myLiteral.Text = texto;
        return myLiteral;
    }

    public void alerta(Page page, string mensagem)
    {

        string script = "<script type='text/javascript' language='javascript'>";
        script += Environment.NewLine + "alert(\" " + mensagem.Replace(Environment.NewLine, " ").Replace('\"', '\'').Replace('\n', ' ') + " \");";
        script += Environment.NewLine + "</script>";
        //page.ClientScript.RegisterClientScriptBlock(GetType(), "Client", script);
        ScriptManager.RegisterClientScriptBlock(page, GetType(), "client", script, false);
    }

    public void alertaUpdatePanel(string mensagem, Control componenteClicado)
    {
        string script = "<script language='JavaScript'>";
        script += Environment.NewLine + "alert(\" " + mensagem.Replace(Environment.NewLine, " ").Replace('\"', '\'').Replace('\n', ' ') + " \");";
        script += Environment.NewLine + "</script>";
        ScriptManager.RegisterClientScriptBlock(componenteClicado, GetType(), "Client", script, false);
    }

    public int? getInteiro(string valor)
    {
        if (valor.Trim() != "")
            return int.Parse(valor.Trim());
        else
            return null;
    }

    public decimal? getDecimal(string valor)
    {
        if (valor.Trim() != "")
            return decimal.Parse(valor.Trim());
        else
            return null;
    }

    public DataSet getParametrosSistema(params object[] parametros)
    {
        return getParametrosSistema(int.Parse(getInfoSistema("CodigoEntidade").ToString()), parametros);
    }

    public DataSet getParametrosSistema(int codigoEntidade, params object[] parametros)
    {
        string comandoSQL = "";
        DataSet ds;

        // retorna todos os parametros
        if (parametros[0].ToString() == "-1")
        {
            // busca a lista de todos os parametros disponiveis na tabela parametrosSistema
            string comandoInterno = string.Format(
                @"select Parametro FROM {0}.{1}.ParametroConfiguracaoSistema WHERE CodigoEntidade = {2}", bancodb, Ownerdb, codigoEntidade);

            ds = getDataSet(comandoInterno);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null)
                {
                    // monta o comandoSQL para retornar todos os parametros
                    foreach (DataRow row in dt.Rows)
                    {
                        comandoSQL += montaComandoGetParametro(codigoEntidade, row["Parametro"].ToString());
                    }
                }
            }
        }
        else // retorna apenas os parametros indicados pelo usuário
        {
            foreach (object parametro in parametros)
            {
                comandoSQL += montaComandoGetParametro(codigoEntidade, parametro.ToString());
            }
        }

        if (comandoSQL != "")
        {
            comandoSQL = "SELECT " + comandoSQL.Remove(comandoSQL.Length - 2);
        }

        ds = getDataSet(comandoSQL);
        return ds;

    }

    private string montaComandoGetParametro(int codigoEntidade, string nomeParametro)
    {
        return string.Format(
        @"(SELECT Valor 
                    FROM {0}.{1}.ParametroConfiguracaoSistema 
                   WHERE codigoEntidade = {3} AND Parametro = '{2}') AS {2}, ", bancodb, Ownerdb, nomeParametro, codigoEntidade);

    }

    #endregion

    //#region funções insert, update, delete - *** Não podem ser alteradas

    //private bool insert(string nomeTabela, ListDictionary dados)
    //{
    //    int afetados = 0;
    //    string comandoSQL = classeDados.getInsert(nomeTabela, dados);
    //    execSQL(comandoSQL, ref afetados);
    //    return true;
    //}

    //private bool update(string nomeTabela, ListDictionary dados, string where)
    //{
    //    int afetados = 0;
    //    string comandoSQL = classeDados.getUpdate(nomeTabela, dados, where);
    //    execSQL(comandoSQL, ref afetados);
    //    return true;
    //}

    //private bool delete(string nomeTabela, string where)
    //{
    //    int afetados = 0;
    //    string comandoSQL = classeDados.getDelete(nomeTabela, where);
    //    execSQL(comandoSQL, ref afetados);
    //    return true;
    //}

    //#endregion

    #region TipoUnidadeMedida

    public string getSelect_TipoUnidadeMedida()
    {
        return "SELECT * FROM TipoUnidadeMedida Order by DescricaoUnidadeMedida_PT";
    }

    public bool incluiTipoUnidadeMedida(ListDictionary dados)
    {
        int afetados = 0;
        string comandoSQL = classeDados.getInsert("TipoUnidadeMedida", dados);
        execSQL(comandoSQL, ref afetados);
        return true;
    }

    public bool atualizaTipoUnidadeMedida(ListDictionary dados, string where)
    {
        int afetados = 0;
        string comandoSQL = classeDados.getUpdate("TipoUnidadeMedida", dados, where);
        execSQL(comandoSQL, ref afetados);
        return true;
    }

    public bool excluiTipoUnidadeMedida(string where)
    {
        int afetados = 0;
        string comandoSQL = classeDados.getDelete("TipoUnidadeMedida", where);
        execSQL(comandoSQL, ref afetados);
        return true;
    }

    #endregion

    #region Colaboradores

    public string getSelect_Colaboradores()
    {
        return "SELECT * FROM Colaboradores Order by Nome";
    }

    public bool incluiColaborador(ListDictionary dados)
    {
        int afetados = 0;
        string comandoSQL = classeDados.getInsert("Colaboradores", dados);
        execSQL(comandoSQL, ref afetados);
        return true;
    }

    public bool atualizaColaborador(ListDictionary dados, string where)
    {
        int afetados = 0;
        string comandoSQL = classeDados.getUpdate("Colaboradores", dados, where);
        execSQL(comandoSQL, ref afetados);
        return true;
    }

    public bool excluiColaborador(string where)
    {
        int afetados = 0;
        string comandoSQL = classeDados.getDelete("Colaboradores", where);
        execSQL(comandoSQL, ref afetados);
        return true;
    }

    #endregion

    #region Anexos

    public int getCodigoTipoAssociacao(string iniciaisTipoAssociacao)
    {
        string comandoSQL =
            @"SELECT CodigoTipoAssociacao
                    FROM TipoAssociacao
                   WHERE IniciaisTipoAssociacao = '" + iniciaisTipoAssociacao + "' ";

        DataSet ds = getDataSet(comandoSQL);
        return int.Parse(ds.Tables[0].Rows[0]["CodigoTipoAssociacao"].ToString());
    }

    public string incluirAnexo(string descricaoAnexo, string codigoUsuarioInclusao, string nomeAnexo, string codigoEntidade,
                             int? codigoPastaSuperior, char indicaPasta, char IndicaControladoSistema, int CodigoTipoAssociacao,
                             string codigoProjeto, byte[] arquivo)
    {
        string comandoSQL = "";
        int codigoAnexo = 0;
        try
        {
            comandoSQL = string.Format(@"
                    BEGIN
                            DECLARE @CodigoAnexo AS INT;
                            DECLARE @CodigoPastaSuperior AS INT;

                            INSERT INTO {0}.{1}.Anexo
                                   (DescricaoAnexo
                                   ,DataInclusao
                                   ,CodigoUsuarioInclusao
                                   ,Nome
                                   ,CodigoEntidade
                                   ,CodigoPastaSuperior
                                   ,IndicaPasta
                                   ,IndicaControladoSistema)
                            VALUES
                                   ('{2}', GETDATE(), {3}, '{4}', {5}, {6}, '{7}', '{8}')
                            
                            SELECT @CodigoAnexo = scope_identity()
                            

                            
                            INSERT into {0}.{1}.AnexoAssociacao (CodigoAnexo, CodigoObjetoAssociado, CodigoTipoAssociacao)
                            VALUES (@CodigoAnexo, {9}, {10})

                            --FAZ UM SELECT PRA VER QUAIS SÃO AS PERMISSOES DA PASTA SUPERIOR E AO MESMO TEMPO INSERE
                            --AS MESMAS PERMISSOES DA PASTA SUPERIOR NO ANEXO ATUAL.
                            --vai inserindo o  codigo do anexo atual junto com as entidades associadas a entidade superior.
                            INSERT INTO {0}.{1}.AnexoAssociacao (CodigoAnexo, CodigoObjetoAssociado, CodigoTipoAssociacao)
                                                          SELECT @CodigoAnexo, CodigoObjetoAssociado, CodigoTipoAssociacao
                                                            FROM {0}.{1}.AnexoAssociacao 
                                                           WHERE CodigoAnexo = {6}--codigo da pasta superior
                                                             AND CodigoObjetoAssociado != {9} --Todos as entidades associadas, menos a entidade atual
                                                             AND CodigoTipoAssociacao = {10} 
                                  

                            SELECT @CodigoAnexo
                    END
                    ", bancodb, Ownerdb, descricaoAnexo, codigoUsuarioInclusao
                     , nomeAnexo, codigoEntidade, codigoPastaSuperior.HasValue ? codigoPastaSuperior.Value.ToString() : "null", indicaPasta, IndicaControladoSistema, codigoProjeto, CodigoTipoAssociacao);

            DataSet ds = getDataSet(comandoSQL);

            // se tem arquivo... vamos inserir no banco de dados
            if (arquivo != null)
            {
                codigoAnexo = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                SqlConnection conexao = new SqlConnection(strConn);
                SqlCommand comando = new SqlCommand();

                comando.Connection = conexao;
                comando.CommandType = CommandType.Text;
                comando.CommandText = string.Format(
                                                 @"INSERT INTO {0}.{1}.ConteudoAnexo
                                                    (CodigoAnexo
                                                    , Anexo) 
                                            VALUES (@CodigoAnexo, @Anexo)", bancodb, Ownerdb);
                comando.Parameters.Add(new SqlParameter("@CodigoAnexo", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "CodigoAnexo", DataRowVersion.Current, false, null, "", "", ""));
                comando.Parameters.Add(new SqlParameter("@Anexo", SqlDbType.Image, 0, ParameterDirection.Input, 0, 0, "Anexo", DataRowVersion.Current, false, null, "", "", ""));

                comando.Parameters[0].Value = codigoAnexo;
                comando.Parameters[1].Value = arquivo;

                conexao.Open();
                comando.ExecuteNonQuery();
            }
            return "";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public bool excluiAnexoProjeto(char indicaPasta, int codigoAnexo, int codigoUsuarioExclusao, int codigoEntidade, int codigoProjeto, int codigoTipoAssociacao, ref string erros)
    {
        bool retorno = false;
        string comandoSQL = "";
        try
        {
            if (indicaPasta != 'S' && indicaPasta != 'N')
            {
                erros = "A indicação (IndicaPasta) do tipo de objeto a ser inserido não é válida. Utilize 'S' ou 'N'." ;
                retorno = false;
            }

            int registrosAfetados = 0;

            // se for pasta, verifica se ela não possui "filhos"
            if (indicaPasta == 'S')
            {
                comandoSQL = string.Format(
                    @"SELECT codigoAnexo
                        FROM {0}.{1}.Anexo 
                       WHERE CodigoPastaSuperior = {3}
                         AND CodigoEntidade = {4}
                       --  AND IndicaControladoSistema = 'N'
                         AND DataExclusao is null
                         AND CodigoPastaSuperior <> CodigoAnexo                              
                            ", bancodb, Ownerdb, codigoAnexo, codigoAnexo, codigoEntidade);
                DataSet ds = getDataSet(comandoSQL);
                if (DataSetOk(ds) && DataTableOk(ds.Tables[0]))
                {
                    //throw new Exception("Só é possível excluir pastas que estejam vazias." + Delimitador_Erro);
                    erros = "Só é possível excluir pastas que estejam vazias.";
                    retorno = false;
                }
                else
                {
                    //primeiro exclui registro da tabela AcessoAnexo
                    comandoSQL = string.Format(
                        @"DELETE FROM {0}.{1}.[AcessoAnexo]
                      WHERE CodigoAnexo = {2} and CodigoUsuario = {3}", bancodb, Ownerdb, codigoAnexo, codigoUsuarioExclusao);
                    execSQL(comandoSQL, ref registrosAfetados);

                    comandoSQL = string.Format(
                  @"DELETE FROM {0}.{1}.[AnexoAssociacao]
                      WHERE CodigoAnexo = {2}
                        AND CodigoObjetoAssociado = {3}
                        AND CodigoTipoAssociacao = {4}", bancodb, Ownerdb, codigoAnexo, codigoProjeto, codigoTipoAssociacao);

                    execSQL(comandoSQL, ref registrosAfetados);

                    // Exclui a pasta da tabela Anexo
                    comandoSQL = string.Format(
                        @"DELETE FROM {0}.{1}.Anexo
                           WHERE CodigoAnexo = {2} and CodigoEntidade = {3}", bancodb, Ownerdb, codigoAnexo, codigoEntidade);
                    execSQL(comandoSQL, ref registrosAfetados);
                    retorno = true;
                }
            }
            else // arquivo -- Arquivo não é excluído fisicamente
            {
                comandoSQL = string.Format(
                   @"UPDATE {0}.{1}.Anexo
                        SET DataExclusao = getdate()
                          , codigoUsuarioExclusao = {4}
                      WHERE CodigoAnexo = {2} 
                        AND CodigoEntidade = {3}", bancodb, Ownerdb, codigoAnexo, codigoEntidade, codigoUsuarioExclusao);

                execSQL(comandoSQL, ref registrosAfetados);
                retorno = true;
            }
        }
        catch (Exception ex)
        {
            erros = ex.Message;
            retorno = false;
        }
        return retorno;
    }

    public byte[] getConteudoAnexo(int CodigoAnexo, ref string NomeArquivo)
    {
        byte[] ImagemArmazenada = null;
        string ComandoSQL = string.Format(
             @"SELECT A.Nome, CA.Anexo 
                 FROM {0}.{1}.Anexo AS A INNER JOIN
                      {0}.{1}.ConteudoAnexo AS CA ON A.CodigoAnexo = CA.CodigoAnexo
                WHERE     (CA.CodigoAnexo = {2})", bancodb, Ownerdb, CodigoAnexo);
        DataSet ds = getDataSet(ComandoSQL);
        if (DataSetOk(ds) && DataTableOk(ds.Tables[0]))
        {
            NomeArquivo = ds.Tables[0].Rows[0][0].ToString();
            ImagemArmazenada = (byte[])ds.Tables[0].Rows[0][1];
        }
        return ImagemArmazenada;
    }

    #endregion

    public bool delete(string nomeTabela, string where)
    {
        try
        {
            int afetados = 0;
            string comandoSQL = classeDados.getDelete(nomeTabela, where);
            execSQL(comandoSQL, ref afetados);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataSet getUsuarioDaEntidadeAtiva(string codigoEntidade, string where)
    {
        string comandoSQL = string.Format(@"
                SELECT u.CodigoUsuario
                      ,u.NomeUsuario
                      ,u.EMail

                FROM        {0}.{1}.usuario                 AS u 
                INNER JOIN  {0}.{1}.UsuarioUnidadeNegocio   AS uun ON u.CodigoUsuario = uun.CodigoUsuario

                WHERE uun.CodigoUnidadeNegocio              = {2}
                  AND uun.IndicaUsuarioAtivoUnidadeNegocio  = 'S'
                  {3}

                ORDER BY u.NomeUsuario 
                ", bancodb, Ownerdb, codigoEntidade, where);
        return getDataSet(comandoSQL);
    } 

}
