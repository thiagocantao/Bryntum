using Cdis.gantt;
using CDIS;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

[WebService(Namespace = "http://www.tasques.com.br/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(false)]
public class wsTasquesreg : System.Web.Services.WebService
{
    int versaoWS = 20230601;//deve ser atualizado a cada modificação
    string diretorioCronogramas; // determinia o local onde os cronogramas enviados pelo cliente desktop serão salvos
    bool salvarHistoricoCronogramasEmDisco = true;
    static string tipoBancoDados = System.Configuration.ConfigurationManager.AppSettings["tipoBancoDados"].ToString();
    static string IDProduto = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();
    static string PathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
    static string Ownerdb = System.Configuration.ConfigurationManager.AppSettings["dbOwner"].ToString();
    static string bancodb = string.Empty;
    
    ClasseDados classeDados;//= new ClasseDados(tipoBancoDados, PathDB, IDProduto, Ownerdb, "", 2, 200);

    public CdisSoapHeader segurancaWS;

    string key = "#COMANDO#Tasques!";
    int registroAfetados = 0;

    #region Hash

    private int ObtemCodigoHash(string str)
    {
        if (str == "")
            str = " ";
        int valorRetorno = 0;
        int acumulador = 0;
        MD5 md5Hasher = MD5.Create();
        char[] caracteres = str.ToCharArray();

        foreach (char caracter in caracteres)
        {
            acumulador += caracter * Array.IndexOf(caracteres, caracter) + caracteres.Length;
        }

        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        valorRetorno = int.Parse(sBuilder.ToString(0, 8), System.Globalization.NumberStyles.HexNumber) ^ (int.Parse(sBuilder.ToString(8, 8), System.Globalization.NumberStyles.HexNumber) / acumulador) ^ int.MaxValue;
        return (valorRetorno % 2 == 0) ? valorRetorno : -valorRetorno;
    }

    private string getChaveRegistro(string informacoes)
    {
        string chaveTemp1 = Math.Abs(ObtemCodigoHash(informacoes)).ToString();

        int posicaoInicioChaveTemp2 = (informacoes.Length / 2);
        if (posicaoInicioChaveTemp2 >= 3)
            posicaoInicioChaveTemp2 /= 2;
        string informacoes2 = informacoes;
        if (informacoes.Length > 4)
            informacoes2 = informacoes.Substring(posicaoInicioChaveTemp2, posicaoInicioChaveTemp2 + 1);
        string chaveTemp2 = Math.Abs(ObtemCodigoHash(informacoes2)).ToString();

        informacoes = informacoes2 + informacoes;
        string chaveTemp3 = Math.Abs(ObtemCodigoHash(informacoes)).ToString();

        string temp = (int.Parse(chaveTemp2) / 3 + int.Parse(chaveTemp3) / 4 + int.Parse(chaveTemp1) / 5).ToString();
        if (temp.Length > 4)
            temp = temp.Substring(temp.Length - 4);

        string chave = temp + chaveTemp3.Substring(chaveTemp3.Length - 4) + chaveTemp1.Substring(chaveTemp3.Length - 5) + chaveTemp3.Substring(chaveTemp3.Length - 4) + chaveTemp3;
        chave = Math.Abs(ObtemCodigoHash(chave)) + chave;

        return chave.Substring(0, 5) + "-" + chave.Substring(6, 6) + "-" + chave.Substring(14, 5);
    }

    #endregion

    public wsTasquesreg()
    {
        classeDados = new ClasseDados(tipoBancoDados, PathDB, IDProduto, Ownerdb, "", 2);
        bancodb = classeDados.databaseNameCdis;

        // se não existir um local definido no Webconfig, assume o local padrão
        if (System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"] != null && System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"].ToString() != "")
            diretorioCronogramas = System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"].ToString();
        else
            diretorioCronogramas = @"C:\CDIS_Tasques\Cronogramas";

        // verifica se existe no web.config, um controle determinando se o cronograma/Mapa deve ser salvo em disco.
        if (System.Configuration.ConfigurationManager.AppSettings["salvarHistoricoCronogramasEmDisco"] != null && System.Configuration.ConfigurationManager.AppSettings["salvarHistoricoCronogramasEmDisco"].ToString() == "N")
            salvarHistoricoCronogramasEmDisco = false;
    }

    #region Uso Geral

    public bool enviarEmail(int codigoEntidade, string _Assunto, string _EmailDestinatario, string _EmailCopiaOculta, string _Mensagem, string anexo, out string mensagemResultado)
    {
        string smtpServer = "";
        string smtpUser = "";
        string smtpPassword = "";

        //busca as informações do smtp na tabela de parametros
        DataSet ds = getParametrosSistema(codigoEntidade, "smtpServer", "smtpUser", "smtpPassword", "remetenteEmailProjeto");

        string _EmailRemetente = "";

        if (ds != null && ds.Tables[0] != null)
        {
            smtpServer = ds.Tables[0].Rows[0]["smtpServer"] != null ? ds.Tables[0].Rows[0]["smtpServer"].ToString() : "";
            smtpUser = ds.Tables[0].Rows[0]["smtpUser"] != null ? ds.Tables[0].Rows[0]["smtpUser"].ToString() : "";
            smtpPassword = ds.Tables[0].Rows[0]["smtpPassword"] != null ? ds.Tables[0].Rows[0]["smtpPassword"].ToString() : "";
            _EmailRemetente = ds.Tables[0].Rows[0]["remetenteEmailProjeto"] != null ? ds.Tables[0].Rows[0]["remetenteEmailProjeto"].ToString() : _EmailRemetente;
        }

        if (smtpServer.Trim() == "")
        {
            mensagemResultado = "As configurações do servidor de emails não foram encontradas no banco de dados!";
            return false;
        }

        //cria objeto com dados do e-mail
        System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();

        if (anexo != "")
        {
            objEmail.Attachments.Add(new Attachment(anexo));
        }

        //remetente do e-mail
        try
        {
            objEmail.From = new System.Net.Mail.MailAddress(_EmailRemetente);
        }
        catch (Exception ex)
        {
            mensagemResultado = "Email remetente inválido. Consulte os parâmetros do sistema." + Environment.NewLine + ex.Message;
            return false;
        }

        //destinatários do e-mail
        foreach (string emailDest in _EmailDestinatario.Split(';'))
        {
            if (emailDest.Trim() != "")
            {
                try { objEmail.To.Add(emailDest); }
                catch { }
            }
        }

        if (_EmailCopiaOculta != "")
        {
            foreach (string emailCopia in _EmailCopiaOculta.Split(';'))
            {
                objEmail.Bcc.Add(emailCopia);
            }
        }

        //prioridade do e-mail
        objEmail.Priority = System.Net.Mail.MailPriority.Normal;

        //formato do e-mail HTML (caso não queira HTML alocar valor false)
        objEmail.IsBodyHtml = true;

        //título do e-mail
        objEmail.Subject = _Assunto;

        //corpo do e-mail
        objEmail.Body = _Mensagem;

        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");

        //cria objeto com os dados do SMTP
        SmtpClient objSmtp = new SmtpClient();

        //alocamos o endereço do host para enviar os e-mails, localhost(recomendado) ou smtp2.locaweb.com.br
        objSmtp.Host = smtpServer;

        //enviamos o e-mail através do método .send()
        try
        {
            /* Bloco comentado em 08/07/2009 por ACG - Para contas internar não precisa autenticação
             objSmtp.UseDefaultCredentials = false;

             //to authenticate we set the username and password properites on the SmtpClient
             objSmtp.Credentials = new NetworkCredential(smtpUser, smtpPassword);
             */
            //se tem senha, vamos utilizar.
            if (smtpPassword != "")
                objSmtp.Credentials = new NetworkCredential(smtpUser, smtpPassword);

            objSmtp.Send(objEmail);

            //excluímos o objeto de e-mail da memória
            objEmail.Dispose();

            mensagemResultado = "E-mail enviado com sucesso para: " + _EmailDestinatario;
            return true;
        }
        catch (Exception ex)
        {
            //excluímos o objeto de e-mail da memória
            objEmail.Dispose();

            mensagemResultado = "Ocorreram problemas no envio do e-mail. Error = " + ex.Message;
            return false;
        }
    }

    public DataSet getParametrosSistema(int codigoEntidade, params object[] parametros)
    {
        string comandoSQL = "";
        DataSet ds;

        if (codigoEntidade <= 0)
            return null;

        // retorna todos os parametros
        if (parametros[0].ToString() == "-1")
        {
            // busca a lista de todos os parametros disponiveis na tabela parametrosSistema
            string comandoInterno = string.Format(
                @"select Parametro FROM {0}.{1}.ParametroConfiguracaoSistema WHERE CodigoEntidade = {2}", classeDados.databaseNameCdis, classeDados.OwnerdbCdis, codigoEntidade);

            ds = classeDados.getDataSet(comandoInterno);
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

        ds = classeDados.getDataSet(comandoSQL);
        return ds;
    }

    private string montaComandoGetParametro(int codigoEntidade, string nomeParametro)
    {
        return string.Format(
        @"(SELECT Valor 
                 FROM {0}.{1}.ParametroConfiguracaoSistema 
                WHERE codigoEntidade = {3} AND Parametro = '{2}') AS {2}, ", classeDados.databaseNameCdis, classeDados.OwnerdbCdis, nomeParametro, codigoEntidade);
    }

    [SoapHeader("segurancaWS")]
    private int verificaValidadeChaveAutenticacao(string chaveAutenticacao)
    {
        string comando = string.Format("SELECT codigoUsuario FROM Usuario WHERE ChaveAutenticacao = '{0}'", chaveAutenticacao);
        DataSet ds = classeDados.getDataSet(comando);
        string codigoUsuario = "";
        string macUsuario = segurancaWS.Mac;
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            codigoUsuario = ds.Tables[0].Rows[0]["codigoUsuario"].ToString();
            //string chaveAutenticacaoValida = ds.Tables[0].Rows[0]["codigoUsuario"].ToString() + DateTime.Now.ToString("yyyyMMdd") + macUsuario;
            //chaveAutenticacaoValida = ObtemCodigoHash(chaveAutenticacaoValida).ToString();

            //if (chaveAutenticacao == chaveAutenticacaoValida)
            return int.Parse(codigoUsuario);
        }
        return -1;
    }

    #endregion

    [WebMethod]
    public string testeComunicacao()
    {
        return "teste ok";
    }

    [WebMethod]
    public DataSet testeComunicacaoLista()
    {
        DataTable dt = new DataTable("Tasques");
        DataColumn workCol = dt.Columns.Add("ID", typeof(Int32));
        workCol.AllowDBNull = false;
        workCol.Unique = true;
        dt.Columns.Add("Nome", typeof(String));
        dt.Columns.Add("Expressao", typeof(String));
        dt.Columns.Add("Valor", typeof(Double));

        for (int i = 0; i < 50; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = i;
            dr["Nome"] = string.Format("Nome para o elemento {0}.", i);
            dr["Expressao"] = string.Format(" {0} * {1} = {2} ", i, i / 0.3, i * i / 0.3);
            dr["Valor"] = (i * 10.5) / 2.5;
            dt.Rows.Add(dr);
        }

        DataSet ds = new DataSet("DsTasques");
        ds.Tables.Add(dt);
        return ds;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public string testeComunicacao2()
    {
        return "teste ok";
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int getIDSessaoTasques()
    {
        if (segurancaWS != null && segurancaWS.Key == key)
        {
            return Math.Abs(DateTime.Now.ToString().GetHashCode()) + 1;
        }
        else
            return -1;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int getVersaoWS()
    {
        return versaoWS;

        //if (segurancaWS != null && segurancaWS.Key == key)
        //{
        //    return versaoWS;
        //}
        //else
        //    return -1;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DateTime getDataAtual()
    {
        if (segurancaWS != null && segurancaWS.Key == key)
        {
            string comando = string.Format("SELECT getdate() Agora");
            DataSet ds = classeDados.getDataSet(comando);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return (DateTime)ds.Tables[0].Rows[0]["Agora"];
            }
            else
                return DateTime.MinValue;
        }
        else
            return DateTime.MinValue;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int getIDServicoWEB(string nomeProtocolo)
    {
        if (segurancaWS != null && segurancaWS.Key == key && segurancaWS.SystemNome == "Portal da Estratégia")
        {
            string macUsuario = "00 E0 4C 10 C9 5A";

            if (segurancaWS.Mac == macUsuario && segurancaWS.SystemPassword == ObtemCodigoHash(segurancaWS.Mac) + "#!@")
            {
                DateTime dataServidor = getDataAtual();
                string key2 = ObtemCodigoHash(segurancaWS.SystemID.ToString() + dataServidor.ToString("yyyyMMdd") + segurancaWS.Mac).ToString();
                if (segurancaWS.Key2 == key2)
                {
                    if (nomeProtocolo == segurancaWS.Key2 + segurancaWS.SystemPassword)
                    {
                        int regAfetados = 0;
                        string comando = string.Format("UPDATE Usuario SET ChaveAutenticacao = '{1}' WHERE codigoUsuario = {0} ", segurancaWS.SystemID, key2);
                        classeDados.execSQL(comando, ref regAfetados);
                        return segurancaWS.SystemID;
                    }
                }
            }
        }
        return -1;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public string autenticarUsuarioSistema()
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return "";

        string Nome = segurancaWS.SystemUser.Replace("\'", "X").Replace('"', 'X');
        string senha = segurancaWS.SystemPassword;
        string macUsuario = segurancaWS.Mac;
        senha = ObtemCodigoHash(senha).ToString();

        string comando = string.Format("SELECT codigoUsuario, getdate() FROM Usuario where dataExclusao is null and email = '{0}' and SenhaAcessoAutenticacaoSistema = '{1}' ", Nome, senha);
        DataSet ds = classeDados.getDataSet(comando);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            string chaveAutenticacao = ds.Tables[0].Rows[0]["codigoUsuario"].ToString() + DateTime.Now.ToString("yyyyMMdd") + macUsuario;
            chaveAutenticacao = ObtemCodigoHash(chaveAutenticacao).ToString();

            classeDados.execSQL(string.Format("UPDATE Usuario SET ChaveAutenticacao = '{1}' WHERE codigoUsuario = {0} ", ds.Tables[0].Rows[0]["codigoUsuario"].ToString(), chaveAutenticacao), ref registroAfetados);
            segurancaWS.Key2 = chaveAutenticacao;
            segurancaWS.SystemID = int.Parse(ds.Tables[0].Rows[0]["codigoUsuario"].ToString());
            return chaveAutenticacao;
        }
        else
            return "";

        //if (key1 != key)
        //    return "";

        //// Lê as informações do XML
        //XmlDocument xmlCliente = new XmlDocument();
        //xmlCliente.InnerXml = dados;
        // // abre o xml
        //XmlNodeList Registro = xmlCliente.GetElementsByTagName("Usuario");
        //if (Registro.Count > 0)
        //{
        //    string Nome = Registro[0]["Nome"] != null ? Registro[0]["Nome"].InnerText : "";
        //    string senha = Registro[0]["Key"] != null ? Registro[0]["Key"].InnerText : "";
        //    senha = ObtemCodigoHash(senha).ToString();

        //    string comando = string.Format("SELECT codigoUsuario, getdate() FROM Usuario where email = '{0}' and SenhaAcessoAutenticacaoSistema = '{1}' ", Nome, senha);
        //    DataSet ds = classeDados.getDataSet(comando);
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        string chaveAutenticacao = ds.Tables[0].Rows[0]["codigoUsuario"].ToString() + DateTime.Now.ToString("yyyyMMdd");
        //        chaveAutenticacao = ObtemCodigoHash(chaveAutenticacao).ToString();

        //        classeDados.execSQL(string.Format("UPDATE Usuario SET ChaveAutenticacao = '{1}' WHERE codigoUsuario = {0} ", ds.Tables[0].Rows[0]["codigoUsuario"].ToString(), chaveAutenticacao), ref registroAfetados);
        //        return chaveAutenticacao;
        //    }
        //    else
        //        return "";
        //}

        //return ".";
    }

    public string autenticarUsuarioIntegrado(ref string nomeUsuario, ref string mensagemRetorno, CdisSoapHeader segurancaWS)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return "";

        string Nome = segurancaWS.SystemUser.Replace("\'", "X").Replace('"', 'X');
        string macUsuario = segurancaWS.Mac;
        string senha = ObtemCodigoHash("_Integrado_").ToString();

        string comando = string.Format("SELECT codigoUsuario, nomeUsuario, getdate() FROM Usuario where dataExclusao is null and TipoAutenticacao = 'AI' and contaWindows = '{0}' ", Nome);
        DataSet ds = classeDados.getDataSet(comando);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            string chaveAutenticacao = ds.Tables[0].Rows[0]["codigoUsuario"].ToString() + DateTime.Now.ToString("yyyyMMdd") + macUsuario;
            chaveAutenticacao = ObtemCodigoHash(chaveAutenticacao).ToString();
            nomeUsuario = ds.Tables[0].Rows[0]["nomeUsuario"].ToString();

            classeDados.execSQL(string.Format("UPDATE Usuario SET ChaveAutenticacao = '{1}' WHERE codigoUsuario = {0} ", ds.Tables[0].Rows[0]["codigoUsuario"].ToString(), chaveAutenticacao), ref registroAfetados);
            segurancaWS.Key2 = chaveAutenticacao;
            segurancaWS.SystemID = int.Parse(ds.Tables[0].Rows[0]["codigoUsuario"].ToString());
            return chaveAutenticacao;
        }
        else
            return "";
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int getSystemID(ref string nome)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1;

        // o replace é impedir a tentativa de injetar comandos no nome do usuário
        string Nome = segurancaWS.SystemUser.Replace("\'", "X").Replace('"', 'X');
        string senha = segurancaWS.SystemPassword.Replace("\'", "X").Replace('"', 'X'); ;
        string macUsuario = segurancaWS.Mac;
        senha = ObtemCodigoHash(senha).ToString();

        string comando = string.Format("SELECT codigoUsuario, nomeUsuario FROM Usuario where email = '{0}' and SenhaAcessoAutenticacaoSistema = '{1}' ", Nome, senha);
        DataSet ds = classeDados.getDataSet(comando);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            segurancaWS.SystemID = int.Parse(ds.Tables[0].Rows[0]["codigoUsuario"].ToString());
            nome = ds.Tables[0].Rows[0]["nomeUsuario"].ToString();
            return segurancaWS.SystemID;
        }
        else
            return -1;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public bool novoRegistro(string key1, string registro, int key2, out string resultadoRegistro)
    {
        if (key1 != key)
        {
            resultadoRegistro = "ERRO 0";
            return false;
        }


        // Lê as informações do XML
        XmlDocument xmlRegistro = new XmlDocument();
        xmlRegistro.InnerXml = registro;

        // Salva o xml no hd
        string fileXmlRegistro = HostingEnvironment.ApplicationPhysicalPath + @"\ArquivosRegistroTasques\" + DateTime.Now.ToString("yyyyMMdd_hhmmss", null) + "_" + key2.ToString() + ".xml";
        xmlRegistro.Save(fileXmlRegistro);

        // abre o xml
        XmlNodeList Registro = xmlRegistro.GetElementsByTagName("Registro");
        if (Registro.Count > 0)
        {
            string Nome = Registro[0]["Nome"] != null ? Registro[0]["Nome"].InnerText : "";
            string Email = Registro[0]["Email"] != null ? Registro[0]["Email"].InnerText : "";
            string Brasil = Registro[0]["Brasil"] != null ? Registro[0]["Brasil"].InnerText : "";
            string UF = Registro[0]["UF"] != null ? Registro[0]["UF"].InnerText : "";
            string Cidade = Registro[0]["Cidade"] != null ? Registro[0]["Cidade"].InnerText : "";
            string Country = Registro[0]["Country"] != null ? Registro[0]["Country"].InnerText : "";
            string City = Registro[0]["City"] != null ? Registro[0]["City"].InnerText : "";
            string Profissao = Registro[0]["Profissao"] != null ? Registro[0]["Profissao"].InnerText : "";
            string UID = Registro[0]["UID"] != null ? Registro[0]["UID"].InnerText : "";

            int _key2 = ObtemCodigoHash(Nome + Email + Brasil + UF + Cidade + Country + City + Profissao + UID);

            // processa a chave de registro
            string informacoes = Nome + Email + Brasil + UF + Cidade + Country + City + Profissao + UID + key2;
            string chaveRegistro = getChaveRegistro(informacoes);
            int codigoRegistro;
            ListDictionary dadosRegistro = new ListDictionary();
            // tenta gravar as informações no banco de dados
            try
            {
                dadosRegistro.Add("data", "GETDATE()");
                dadosRegistro.Add("nome", Nome);
                dadosRegistro.Add("email", Email);
                dadosRegistro.Add("brasil", Brasil);
                dadosRegistro.Add("uf", UF);
                dadosRegistro.Add("Cidade", Cidade);
                dadosRegistro.Add("country", Country);
                dadosRegistro.Add("city", City);
                dadosRegistro.Add("profissao", Profissao);
                dadosRegistro.Add("CPU_MAC", UID);
                dadosRegistro.Add("hashEnviadoPeloUsuario", key2);
                dadosRegistro.Add("HashCalculado", _key2);
                dadosRegistro.Add("hashOk", key2 == _key2 ? "S" : "N");
                dadosRegistro.Add("chaveRegistro", chaveRegistro);

                codigoRegistro = classeDados.Insert("registro", dadosRegistro, true);

                // se deu certo, renomeia o arquivo do disco
                File.Move(fileXmlRegistro, Path.GetDirectoryName(fileXmlRegistro) + @"\_ok_" + Path.GetFileName(fileXmlRegistro));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            // Tenta enviar o e-mail
            resultadoRegistro = "1"; // o registro foi feito e o e-mail foi enviado
            try
            {
                // Envia o e-mail com a chave
                string mensagemResultadoEnvio = "";
                string mensagemEmail = "<b>Chave: " + chaveRegistro + "</b>";

                if (enviarEmail(1, "TASQUES - Registro", Email, "tasquesregistrosenviados@pensave.com.br", mensagemEmail, "", out mensagemResultadoEnvio))
                {
                    // registra que o e-mail foi enviado
                    dadosRegistro.Clear();
                    dadosRegistro.Add("emailEnviado", "S");
                    classeDados.update("registro", dadosRegistro, "id = " + codigoRegistro);
                }
            }
            catch (Exception ex)
            {
                resultadoRegistro = "2 - " + ex.Message; // O registro foi feito, mas o e-mail não foi enviado.
            }
            return true;
        }
        resultadoRegistro = "ERRO 1";
        return false;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getEntidadesUsuario(string where)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comando = string.Format(
             @"DECLARE @CodigoCarteira int
               DECLARE @CodigoUsuario int
                   SET @CodigoUsuario = {2} 

               SELECT Distinct 
                      Entidade.CodigoUnidadeNegocio, 
                      Entidade.NomeUnidadeNegocio, 
                      Entidade.SiglaUnidadeNegocio,
                      (select COUNT(1) from {0}.{1}.f_GetProjetosUsuario(@CodigoUsuario , Entidade.CodigoUnidadeNegocio, c.CodigoCarteira)) qtdeProjetos,
                      (select case when valor = '' then 'S' else valor end from ParametroConfiguracaoSistema where Parametro = 'VersaoMSProject' and CodigoEntidade = Entidade.CodigoEntidade) usarTasques
                 FROM {0}.{1}.TipoUnidadeNegocio INNER JOIN
                      {0}.{1}.UnidadeNegocio AS Entidade ON TipoUnidadeNegocio.CodigoTipoUnidadeNegocio = Entidade.CodigoTipoUnidadeNegocio INNER JOIN
                      {0}.{1}.UsuarioUnidadeNegocio ON Entidade.CodigoUnidadeNegocio = UsuarioUnidadeNegocio.CodigoUnidadeNegocio INNER JOIN
                      {0}.{1}.Carteira c on c.CodigoEntidade = Entidade.CodigoEntidade and c.IniciaisCarteiraControladaSistema = 'PR'
                WHERE TipoUnidadeNegocio.IndicaEntidade = 'S'
                  AND UsuarioUnidadeNegocio.IndicaUsuarioAtivoUnidadeNegocio = 'S'
                  AND Entidade.IndicaUnidadeNegocioAtiva = 'S'
                  AND Entidade.DataExclusao IS NULL  
                  AND UsuarioUnidadeNegocio.CodigoUsuario = @CodigoUsuario  {3} 
                  AND (select COUNT(1) from {0}.{1}.f_GetProjetosUsuario(@CodigoUsuario , Entidade.CodigoUnidadeNegocio, c.CodigoCarteira)) > 0
                ORDER BY Entidade.NomeUnidadeNegocio", bancodb, Ownerdb, codigoUsuario, where);

        DataSet ds = classeDados.getDataSet(comando);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }
        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet listaArquivosXmlCronogramasServidor(string codigoCronogramaProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        DataSet ds = new DataSet();

        if (salvarHistoricoCronogramasEmDisco)
        {
            // seleciona todas as versões do cronograma
            DirectoryInfo pasta = new DirectoryInfo(diretorioCronogramas);
            FileInfo[] arquivos = pasta.GetFiles(codigoCronogramaProjeto + "*.xml");

            DataTable dt = new DataTable();

            dt.Columns.Add("Nome");
            dt.Columns.Add("Modificado");

            foreach (FileInfo file in arquivos)
            {
                DataRow dr = dt.NewRow();
                dr["Nome"] = file.Name;
                dr["Modificado"] = file.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

                dt.Rows.Add(dr);
            }
            dt.DefaultView.Sort = "Modificado desc";

            ds.Tables.Add(dt);
        }
        else // lê do banco de dados
        {
            string comandoSQL = string.Format(
                @" SELECT CONVERT(char(30), DataInclusao,126) as Nome, 
                              convert(varchar(50), DataInclusao, 103) + ' ' + convert(varchar(50), DataInclusao, 108) as Modificado
                         FROM CronogramaProjeto_ArquivoXMLCliente
                        WHERE CodigoCronogramaProjeto = '{0}'
                        ORDER BY DataInclusao DESC", codigoCronogramaProjeto);

            ds = classeDados.getDataSet(comandoSQL);
        }

        return ds;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public XmlDocument getArquivoCronograma(string nome, string data)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        XmlDocument xd = new XmlDocument();
        if (salvarHistoricoCronogramasEmDisco)
        {
            string arquivo = diretorioCronogramas + @"\" + nome;
            xd.Load(arquivo);
        }
        else
        {
            string comandoSQL = string.Format(
                @" SELECT xmlCronogramaProjeto
                         FROM CronogramaProjeto_ArquivoXMLCliente
                        WHERE CodigoCronogramaProjeto = '{0}'
                          AND DataInclusao = '{1}' ", nome, data);

            DataSet ds = classeDados.getDataSet(comandoSQL);
            string xml = ds.Tables[0].Rows[0]["xmlCronogramaProjeto"] + "";
            xd.LoadXml(xml);
        }

        return xd;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet listaCronogramasServidor(int codigoEntidade)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        // tudo certo, vamos ler os cronogramas do usuário
        //            string comando = string.Format(
        //                @"SELECT codigoCronogramaProjeto, NomeProjeto, DataCheckoutCronograma, DataUltimaPublicacao, NomeUsuario, CodigoCalendario, CodigoUsuario
        //                    FROM {0}.{1}.CronogramaProjeto CP inner join
        //                         {0}.{1}.f_GetProjetosUsuario({2},{3}) PU on PU.CodigoProjeto = CP.CodigoProjeto left join
        //                         {0}.{1}.Usuario U on CP.CodigoUsuarioCheckoutCronograma = U.CodigoUsuario
        //                   WHERE (DataUltimaPublicacao is not null OR CP.CodigoUsuarioInclusao = {2} )
        //                     AND CP.DataExclusao is null
        //                   ORDER By NomeProjeto", bancodb, Ownerdb, codigoUsuario, codigoEntidade);


        string comando = string.Format(
            @"DECLARE @CodigoCarteira int
                  SET  @CodigoCarteira = (select codigocarteira 
                                            from Carteira 
                                           where CodigoEntidade = {3}
                                             and IniciaisCarteiraControladaSistema = 'PR' )

                  SELECT codigoCronogramaProjeto, isnull(cp.NomeProjeto, p.NomeProjeto) NomeProjeto, P.CodigoProjeto,
                         DataCheckoutCronograma, DataUltimaPublicacao, NomeUsuario, CodigoCalendario, CodigoUsuario

                    FROM {0}.{1}.f_GetProjetosUsuario({2},{3},@CodigoCarteira) PU                                INNER JOIN

                         {0}.{1}.Projeto            P   ON P.CodigoProjeto = PU.CodigoProjeto
                                                       AND P.DataExclusao is null                LEFT JOIN

                         {0}.{1}.CronogramaProjeto CP   ON CP.CodigoProjeto = PU.CodigoProjeto
                                                       AND CP.DataExclusao is null
                                                       AND (DataUltimaPublicacao is not null
                                                            OR CP.CodigoUsuarioInclusao = {2} )  LEFT JOIN

                         {0}.{1}.Usuario U on CP.CodigoUsuarioCheckoutCronograma = U.CodigoUsuario

                   ORDER By NomeProjeto", bancodb, Ownerdb, codigoUsuario, codigoEntidade);

        DataSet ds = classeDados.getDataSet(comando);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int getParametros(int codigoEntidade, bool parametros, string[] parametrosIn, out string[] parametrosOut)
    {
        parametrosOut = null;
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1;

        if (codigoEntidade <= 0)
            return -2;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -3;

        DataSet ds = new DataSet();
        if (parametros)
        {
            ds = getParametrosSistema(codigoEntidade, parametrosIn);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                parametrosOut = new string[ds.Tables[0].Columns.Count];
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    parametrosOut[col.Ordinal] = col.ColumnName + ";" + ds.Tables[0].Rows[0][col.ColumnName].ToString();
                }

                return 1;
            }
        }

        return -4;
    }

    /// <summary>
    /// Retorna o calendário da empresa
    /// </summary>
    /// <param name="key"></param>
    /// <param name="codigoEntidade"></param>
    /// <param name="codigoCalendario">Código do calendário desejado. Se for null, retorna o calendário padrão</param>
    /// <returns></returns>
    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getCalendarioEmpresa(int codigoEntidade, int codigoCalendario, int codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        string _CodigoCalendario = "";
        // se informou o codigo do calendário...
        if (codigoCalendario > 0)
            _CodigoCalendario = "SET @CodigoCalendario = " + codigoCalendario;
        // se informou o codigo do projeto
        else if (codigoProjeto > 0)
            _CodigoCalendario = "SET @CodigoCalendario = dbo.f_GetCodigoCalendarioEmpresa(" + codigoProjeto + ")";
        // caso contrario, usa o calendário da entidade
        else
            _CodigoCalendario = string.Format(
                @"SET @CodigoCalendario = 
                            (SELECT min(CodigoCalendario)
                               FROM {0}.{1}.Calendario
                              WHERE IndicaCalendarioPadrao = 'S'
                                AND CodigoEntidade = {2} ) ", bancodb, Ownerdb, codigoEntidade);

        string comandoSQL = string.Format(
            @"
                    DECLARE @CodigoCalendario int
                    DECLARE @CodigoEntidade int
                    SET @CodigoEntidade = {2}

                        {3} 

                    DECLARE @CalendariosEntidade table( codigoCalendario int)
                    INSERT INTO @CalendariosEntidade 
                    SELECT c.codigoCalendario
                      FROM Calendario AS c INNER JOIN
                           AssociacaoCalendario AS ac ON (ac.CodigoCalendario = c.CodigoCalendario
                                                      AND ac.CodigoObjetoAssociado = @CodigoEntidade) INNER JOIN
                           TipoAssociacao AS ta ON (ta.CodigoTipoAssociacao = ac.CodigoTipoAssociacao
                                                AND ta.IniciaisTipoAssociacao = 'EN') 

                    SELECT horasDia, horasSemana, diasMes, c.DescricaoCalendario, c.CodigoCalendarioBase, c.codigoCalendario, 
                           c.IndicaCalendarioPadrao, c.DataUltimaAlteracao, c.DataUltimaProjecao
                      FROM Calendario AS c
                     WHERE codigoCalendario in (select codigoCalendario from @CalendariosEntidade )

                    SELECT codigoCalendario, diaSemana,
                           case when horaInicioTurno1 is null then 'N' else 'S' end as IndicaDiaUtil,
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4,
                           IndicaHorarioPadrao
                      FROM {0}.{1}.CalendarioDiaSemana
                     WHERE CodigoCalendario in (select codigoCalendario from @CalendariosEntidade )

                    SELECT c.CodigoCalendarioBase, dc.CodigoCalendario, convert(varchar, Data, 103) as Data,
                           IndicaDiaUtil, CDS.diaSemana,
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4
                      FROM {0}.{1}.DetalheCalendarioDiaSemana DC inner join
                           {0}.{1}.CalendarioDiaSemana CDS on (CDS.codigoCalendario = DC.codigoCalendario AND
                                                       CDS.diaSemana = DC.diaSemana) INNER JOIN
                           {0}.{1}.Calendario C on C.CodigoCalendario = DC.CodigoCalendario
                     WHERE C.CodigoCalendarioBase  = @CodigoCalendario
                     --WHERE DC.CodigoCalendario IN (SELECT codigoCalendario FROM Calendario WHERE CodigoCalendarioBase = @CodigoCalendario )
                     ORDER BY Data

                 ", bancodb, Ownerdb, codigoEntidade, _CodigoCalendario);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        ds.Tables[0].TableName = "Limites";
        ds.Tables[1].TableName = "Calendario";
        ds.Tables[2].TableName = "Excecoes";
        return ds;

    }

    private DataSet getCalendarioEmpresaOLD(int codigoEntidade, int codigoCalendario)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        string _CodigoCalendario = "";
        // se informou o codigo do calendário...
        if (codigoCalendario > 0)
            _CodigoCalendario = "SET @CodigoCalendario = " + codigoCalendario;
        // caso contrario, usa o calendário da entidade
        else
            _CodigoCalendario = string.Format(
                @"SET @CodigoCalendario = 
                            (SELECT min(CodigoCalendario)
                               FROM {0}.{1}.Calendario
                              WHERE IndicaCalendarioPadrao = 'S'
                                AND CodigoEntidade = {2} ) ", bancodb, Ownerdb, codigoEntidade);

        string comandoSQL = string.Format(
            @"
                    DECLARE @CodigoCalendario int
                        {2} 

                    SELECT horasDia, horasSemana, diasMes, DescricaoCalendario, CodigoCalendarioBase, codigoCalendario
                      FROM dbo.Calendario
                       WHERE CodigoCalendario = @CodigoCalendario

                    SELECT codigoCalendario, diaSemana,
                           case when horaInicioTurno1 is null then 'N' else 'S' end as IndicaDiaUtil,
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4,
                           IndicaHorarioPadrao
                      FROM {0}.{1}.CalendarioDiaSemana
                     WHERE CodigoCalendario = @CodigoCalendario

                    SELECT convert(varchar, Data, 103) as Data,
                           IndicaDiaUtil, CDS.diaSemana,
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4
                      FROM {0}.{1}.DetalheCalendarioDiaSemana DC inner join
                           {0}.{1}.CalendarioDiaSemana CDS on (CDS.codigoCalendario = DC.codigoCalendario AND
                                                       CDS.diaSemana = DC.diaSemana)
                     WHERE DC.CodigoCalendario IN (SELECT codigoCalendario FROM Calendario WHERE CodigoCalendarioBase = @CodigoCalendario )
                     ORDER BY Data

                 ", bancodb, Ownerdb, _CodigoCalendario);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        ds.Tables[0].TableName = "Limites";
        ds.Tables[1].TableName = "Calendario";
        ds.Tables[2].TableName = "Excecoes";
        return ds;

    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getCalendarioRecursosCronograma(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @"DECLARE @CodigoProjeto varchar(50)
                   SET @CodigoProjeto = '{2}'
                   DECLARE @CodigoTipoAssociacao int
                   SET @CodigoTipoAssociacao = (SELECT CodigoTipoAssociacao 
                                                  FROM {0}.{1}.TipoAssociacao 
                                                 WHERE IniciaisTipoAssociacao = 'RC')

                   DECLARE @tb_CalendarioRecursos table(
	                   CodigoCalendario int,
	                   DescricaoCalendario varchar(60),
	                   CodigoCalendarioBase int
	                   )
                   	
                   INSERT INTO @tb_CalendarioRecursos	
                   SELECT c.CodigoCalendario, c.DescricaoCalendario, c.CodigoCalendarioBase
                     FROM {0}.{1}.RecursoCronogramaProjeto rcp                                                              INNER JOIN

                          {0}.{1}.RecursoCorporativo       rc on rc.CodigoRecursoCorporativo = rcp.CodigoRecursoCorporativo INNER JOIN

                          {0}.{1}.AssociacaoCalendario     AC on AC.CodigoObjetoAssociado = RCP.CodigoRecursoCorporativo    AND
                                                                 AC.CodigoTipoAssociacao = @CodigoTipoAssociacao            INNER JOIN

                          {0}.{1}.Calendario               C  on C.CodigoCalendario = AC.CodigoCalendario
                    WHERE CodigoCronogramaProjeto = @CodigoProjeto	

                    SELECT * 
                      FROM @tb_CalendarioRecursos 
                     
                    SELECT CR.CodigoCalendario, convert(varchar, Data, 103) as Data,
                           IndicaDiaUtil, CDS.diaSemana,
                           convert(varchar, horaInicioTurno1, 103) + ' ' + convert(varchar, horaInicioTurno1, 108) as horaInicioTurno1, 
                           convert(varchar, horaTerminoTurno1, 103) + ' ' + convert(varchar, horaTerminoTurno1, 108) as horaTerminoTurno1,       
                           convert(varchar, horaInicioTurno2, 103) + ' ' + convert(varchar, horaInicioTurno2, 108) as horaInicioTurno2,
                           convert(varchar, horaTerminoTurno2, 103) + ' ' + convert(varchar, horaTerminoTurno2, 108) as horaTerminoTurno2,
                           convert(varchar, horaInicioTurno3, 103) + ' ' + convert(varchar, horaInicioTurno3, 108) as horaInicioTurno3,
                           convert(varchar, horaTerminoTurno3, 103) + ' ' + convert(varchar, horaTerminoTurno3, 108) as horaTerminoTurno3,
                           convert(varchar, horaInicioTurno4, 103) + ' ' + convert(varchar, horaInicioTurno4, 108) as horaInicioTurno4,
                           convert(varchar, horaTerminoTurno4, 103) + ' ' + convert(varchar, horaTerminoTurno4, 108) as horaTerminoTurno4
                      FROM {0}.{1}.DetalheCalendarioDiaSemana DC                                                INNER JOIN

                           {0}.{1}.CalendarioDiaSemana        CDS ON CDS.codigoCalendario = DC.codigoCalendario AND
                                                                     CDS.diaSemana = DC.diaSemana               INNER JOIN

                           @tb_CalendarioRecursos             CR  ON  CR.CodigoCalendario = DC.CodigoCalendario
                     ORDER BY CR.CodigoCalendario, Data", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null)
        {
            ds.Tables[0].TableName = "Calendarios";
            ds.Tables[1].TableName = "Excecoes";
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public string getCodigoProjeto(int codigoProjeto, int codigoEntidade)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string codigoGerenteProjeto = "";
        string nomeGerenteProjeto = "";
        string codigoRecursoCorporativo = "";
        string nomeRecursoCorporativo = "";
        string nomeUsuarioRequisitante = ""; // nome do usuário que fez a chamada a esta função - Usuário logado.
        string CodigoCronogramaReplanejamento = "";
        string CodigoCronogramaOrigem = "";
        string DataUltimaPublicacao = "";

        // busca o código do cronograma. Pode ser que ainda não exista cronograma para o projeto, por isso o RIGHT JOIN
        string comandoSQL = string.Format(
             @" SELECT CP.CodigoCronogramaProjeto, P.NomeProjeto, P.CodigoGerenteProjeto, U.NomeUsuario, RC.CodigoRecursoCorporativo, RC.NomeRecursoCorporativo,
                           CP.CodigoCronogramaReplanejamento, CP.CodigoCronogramaOrigem, CP.DataUltimaPublicacao
                      FROM {0}.{1}.Projeto             P LEFT JOIN
                           {0}.{1}.CronogramaProjeto  CP on CP.CodigoProjeto = P.CodigoProjeto AND
                                                            CP.DataExclusao is null                  LEFT JOIN
                           {0}.{1}.Usuario             U on U.CodigoUsuario = P.CodigoGerenteProjeto LEFT JOIN
                           {0}.{1}.RecursoCorporativo rc on rc.CodigoUsuario = P.CodigoGerenteProjeto AND
                                                            rc.codigoentidade = p.codigoEntidade

                     WHERE P.CodigoProjeto  = {2}
                       AND P.DataExclusao is null", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        string codigoCronogramaProjeto = "";
        string nomeProjeto = "";
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
            nomeProjeto = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();

            codigoGerenteProjeto = ds.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString();
            nomeGerenteProjeto = ds.Tables[0].Rows[0]["nomeUsuario"].ToString();
            codigoRecursoCorporativo = ds.Tables[0].Rows[0]["codigoRecursoCorporativo"].ToString();
            nomeRecursoCorporativo = ds.Tables[0].Rows[0]["nomeRecursoCorporativo"].ToString();
            CodigoCronogramaReplanejamento = ds.Tables[0].Rows[0]["CodigoCronogramaReplanejamento"].ToString();
            CodigoCronogramaOrigem = ds.Tables[0].Rows[0]["CodigoCronogramaOrigem"].ToString();
            DataUltimaPublicacao = ds.Tables[0].Rows[0]["DataUltimaPublicacao"] == DBNull.Value ? "" : ((DateTime)ds.Tables[0].Rows[0]["DataUltimaPublicacao"]).ToString("dd/MM/yyyy HH:mm:ss");
        }

        // busca o nome do usuário 
        comandoSQL = "SELECT nomeUsuario FROM usuario WHERE codigousuario = " + codigoUsuario;
        ds = classeDados.getDataSet(comandoSQL);

        nomeUsuarioRequisitante = ds.Tables[0].Rows[0]["nomeUsuario"].ToString();

        // busca informações da entidade
        comandoSQL = string.Format(
         @"SELECT NomeUnidadeNegocio, SiglaUnidadeNegocio 
                 FROM UnidadeNegocio 
                WHERE CodigoEntidade = CodigoUnidadeNegocio
                  AND CodigoEntidade = {2}", bancodb, Ownerdb, codigoEntidade);
        ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return codigoCronogramaProjeto + "¥" +                                  // 0
                   nomeProjeto + "¥" +                                              // 1
                   ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString() + "¥" +    // 2
                   ds.Tables[0].Rows[0]["SiglaUnidadeNegocio"].ToString() + "¥" +   // 3
                   codigoGerenteProjeto + "¥" +                                     // 4   
                   nomeGerenteProjeto + "¥" +                                       // 5
                   codigoRecursoCorporativo + "¥" +                                 // 6
                   nomeRecursoCorporativo + "¥" +                                   // 7
                   nomeUsuarioRequisitante + "¥" +                                  // 8
                   CodigoCronogramaReplanejamento + "¥" +                           // 9
                   CodigoCronogramaOrigem + "¥" +                                   // 10
                   DataUltimaPublicacao;                                            // 11
        }

        return "";
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getPermissoesProjeto(int codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        // VERIFICA SE O TIPO É PROJETO OU PROCESSO
        string comandoSQL = string.Format(
            @"SELECT tp.IndicaTipoProjeto 
                    FROM Projeto P INNER JOIN
                         TipoProjeto TP on tp.CodigoTipoProjeto = p.CodigoTipoProjeto
                   WHERE CodigoProjeto = {0}", codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            string IndicaTipoProjeto = ds.Tables[0].Rows[0]["IndicaTipoProjeto"] + "";

            // inicialmente o comando é construído para pegar permissões de Projetos - PRJ / "PR"
            comandoSQL = string.Format(
                 @" DECLARE @CodigoProjeto int 
                    DECLARE @CodigoUsuario int 
                    DECLARE @CodigoEntidade int 

                    SET @CodigoUsuario = {2}
                    SET @CodigoProjeto = {3}
                    SET @CodigoEntidade = (select codigoEntidade from Projeto where CodigoProjeto = @CodigoProjeto)

                    select (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_IncTrfCrn')) as podeIncluirTarefa, 
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_ExcTrfCrn')) as podeExcluirTarefa, 
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltTrfCrn')) as podeEditarTarefa,  
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltNomTrfCrn')) as podeEditarNomeTarefa, 
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltDatTrfCrn')) as podeEditarDatasTarefa,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltDurTrfCrn')) as podeEditarDuracaoTarefa,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltRlzTrfCrn')) as podeEditarRealizacaoTarefa,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltPreTrfCrn')) as podeEditarRelacionamentoTarefa,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_IncRecCrn')) as podeIncluirRecurso,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_ExcRecCrn')) as podeExcluirRecurso,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltRecCrn')) as podeEditarRecurso, 
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_ImpRecCrn')) as podeImportarRecurso,   
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_SubRecCrn')) as podeSubstituirRecurso, 
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AtrRecCrn')) as podeAlterarAlocacaoRecursos,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_CstRecCrn')) as podeAjustarCustoRecursoLocal,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_SlvLBCrn')) as podeSalvarLinhaBase,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltVisCrn')) as podeAlterarVisao,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_ImpMSPCrn')) as podeImportarCronogramaMSP, 
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_ExpMSPCrn')) as podeExportarCronogramaMSP,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_SlvAtrVazCrn')) as podeSalvarTarefaSemAtribuicao,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_FlxDlcCrn')) as podeIniciarFluxoDilacao,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_FlxAdtCrn')) as podeIniciarFluxoAditivo,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_ConHisSerCrn')) as podeConsultarHistoricoServidor,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AbrXMLTsqCrn')) as podeAbrirXMLTasquesLocal,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_IncVclTrf')) as podeIncluirVinculoTarefas,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AltVclTrf')) as podeAlterarVinculoTarefas,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_PubModCrn')) as podePublicarModeloCronograma,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_IncModCrn')) as podeIncluirModeloCronograma,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_AdmModCrn')) as podeAdministrarModeloCronograma,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_EdtRepCrn')) as podeEditarReplanejamento,
                           (select {0}.{1}.f_VerificaAcessoConcedido(@CodigoUsuario, @CodigoEntidade, @CodigoProjeto, null, 'PR', 0, null, 'PR_ExcRepCrn')) as podeExcluirReplanejamento
                           
                ", bancodb, Ownerdb, codigoUsuario, codigoProjeto);

            // se for do tipo PROCESSO, muda o prefixo da permissão para "PR_"
            if (IndicaTipoProjeto == "PRC")
            {
                comandoSQL = comandoSQL.Replace("'PR_", "'PC_");
                comandoSQL = comandoSQL.Replace("'PR'", "'PC'");
            }

            ds = classeDados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getInformacoesProjeto(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @" SELECT cp.CodigoProjeto, CodigoCronogramaProjeto, NomeProjeto, DataInclusaoServidor, u.nomeUsuario, cp.DataUltimaAlteracao, InicioProjeto, 
                           '09:00' as HoraInicio, '18:00' as HoraTermino, 
                           minutosDia, minutosSemana, 
                           CONVERT(int, DiasMes) * minutosDia as minutosMes, 
                           CodigoCalendario, NomeAutor, VersaoDesktop,
                           DataUltimaPublicacao, CodigoUsuarioUltimaPublicacao,
                           NomeAutor, VersaoDesktop, ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoMoedaProjeto,
                           isnull((SELECT MAX(codigoTarefa) FROM TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = cp.CodigoCronogramaProjeto ),0) as MaiorCodigoTarefa,
                           tipoVisaoCronograma, RecalcularCronogramaAoAbrirTasques, CodigoTipoCronograma, DescricaoTipoCronograma, 
                           CodigoCronogramaReferencia, CodigoCronogramaReplanejamento, CodigoCronogramaOrigem, IndicaUsoPesoManual 
                      FROM {0}.{1}.CronogramaProjeto cp inner join 
                           {0}.{1}.Usuario u on u.codigoUsuario = cp.CodigoUsuarioInclusao
                     WHERE CodigoCronogramaProjeto = '{2}'
                       AND cp.dataExclusao is null", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getInformacoesModelo(int codigoEntidade)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @"select CodigoCronogramaProjeto, nomeprojeto, DescricaoTipoCronograma, NomeAutor, 
                          DataUltimaPublicacao, CodigoUsuarioUltimaPublicacao, CodigoCronogramaReferencia
                     from {0}.{1}.CronogramaProjeto
                    where CodigoTipoCronograma = 2
                      and CodigoEntidade = {2}
                      and DataExclusao is null
                    order by NomeProjeto", bancodb, Ownerdb, codigoEntidade);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getLimitesProjeto(int codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @"SELECT * FROM {0}.{1}.f_GetLimitesCronogramaProjeto({2})", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getFluxosAlteracaoCronograma(int codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @"SELECT * FROM {0}.{1}.f_GetFluxosAlteracaoCronograma({2})", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int getMaiorCodigoTarefaProjeto(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2;

        string comandoSQL = string.Format(
             @" SELECT isnull(MAX(codigoTarefa),0 ) as MaiorCodigoTarefa
                      FROM TarefaCronogramaProjeto 
                     WHERE CodigoCronogramaProjeto = '{2}' ", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return int.Parse(ds.Tables[0].Rows[0]["MaiorCodigoTarefa"].ToString());
        }

        return -3;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getTipoTarefaCronograma(string codigoEntidade)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @"SELECT *
                     FROM {0}.{1}.TipoTarefaCronograma
                    WHERE CodigoEntidade = {2}
                    ORDER BY CodigoTipoTarefaCronograma ", bancodb, Ownerdb, codigoEntidade);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getTarefasProjeto(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @" SELECT CodigoTarefa, SequenciaTarefaCronograma as indice, IndicaMarco, IndicaTarefaResumo, Nivel, 
                           EstruturaHierarquica, CodigoTarefaSuperior, CodigoTipoTarefaCronograma,
                           indicaTarefaExpandida as expandida, indicaTarefaVisivel as visivel, NomeTarefa, 
                           DataInclusao, Inicio, Termino, Duracao, DuracaoAcumuladaEmMinutos, FormatoDuracao, DuracaoEmMinutos, 
                           InicioReal, TerminoReal, DuracaoReal, DuracaoRealEmMinutos,
                           MenorDataTarefa, tipoRestricao, DataRestricao, 
                           0 as QtdeSubtarefas,
                           Predecessoras, indicesTarefasDependentes, PercentualFisicoConcluido, Trabalho, TrabalhoReal, 
                           Custo, CustoReal, Receita, ReceitaReal, StringAlocacaoRecursoTarefa, Anotacoes, indicaTarefaValida, IndicaTarefaCritica,
                           InicioLB, TerminoLB, DuracaoLB, DuracaoLBEmMinutos, TrabalhoLB, custoLB,
                           PercentualFisicoPrevisto, PercentualFisicoPrevistoLB, PercentualFisicoConcluidoLB, IndicaTarefaAtrasadaLB, PossuiTarefaFilhaComLB, IndicaTarefaAtrasadaRecurso,
                           MensagemBloqueio, IndicaTarefaComVinculoExterno, CodigoCronogramaVinculoExterno, CodigoTarefaVinculoExterno, 
                           ValorPesoTarefa, PercentualPesoTarefa, IndicaAtribuicaoManualPesoTarefa, ValorPesoTarefaLB
                      FROM {0}.{1}.f_tasq_GetTarefasCronograma('{2}')

                     /* FROM {0}.{1}.TarefaCronogramaProjeto
                     WHERE CodigoCronogramaProjeto = '{2}' 
                       AND DataExclusao is null */

                     ORDER BY SequenciaTarefaCronograma", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getTarefasProjetoTipoTarefaAssociado(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @" SELECT tctt.CodigoTarefa, tctt.CodigoTipoTarefaCronograma
                      FROM {0}.{1}.TarefaCronogramaProjetoTipoTarefa tctt inner join
                           {0}.{1}.TarefaCronogramaProjeto tc on tc.CodigoCronogramaProjeto = tctt.CodigoCronogramaProjeto AND
	                                                     tc.CodigoTarefa = tctt.CodigoTarefa AND
									                     tc.DataExclusao is null
                     WHERE tc.CodigoCronogramaProjeto = '{2}' ", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            return ds;

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getTarefasExcluidasProjeto(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @" SELECT u.NomeUsuario as nomeUsuarioExclusao, tc.*
                      FROM {0}.{1}.TarefaCronogramaProjeto tc left join
                           {0}.{1}.usuario u on u.CodigoUsuario = tc.codigoUsuarioExclusao
                     WHERE CodigoCronogramaProjeto = '{2}' 
                       AND tc.DataExclusao is not null
                     ORDER BY tc.DataExclusao", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getTarefasPredecessorasProjeto(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @" SELECT tcpp.*
                      FROM {0}.{1}.TarefaCronogramaProjetoPredecessoras tcpp inner join
                           {0}.{1}.tarefaCronogramaProjeto tcp on tcp.CodigoCronogramaProjeto = tcpp.CodigoCronogramaProjeto and
                                                                  tcp.codigoTarefa = tcpp.codigoTarefa AND
                                                                  tcp.dataExclusao is null
                     WHERE tcpp.CodigoCronogramaProjeto = '{2}' ", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getRecursosCronograma(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
            @"SELECT RCP.CodigoCronogramaProjeto, RCP.CodigoRecursoProjeto, RCP.NomeRecurso, RCP.EMail, 
                         RCP.CustoHora, RCP.CustoUso, RCP.CustoHoraExtra, RCP.CodigoGrupoRecurso, RCP.NomeGrupoRecurso, RCP.Anotacoes, 
                         RCP.iniciaisRecurso, RCP.unidadeMaxima, RCP.codigoTipoAcumularCustos, RCP.codigoCalendarioBase,
                         RCP.CodigoTipoRecurso, TR.DescricaoTipoRecurso, RCP.UnidadeMedidaRecurso,
                         isnull((select CodigoCalendario
                                   from AssociacaoCalendario ac inner join
                                        TipoAssociacao ta on ta.CodigoTipoAssociacao = ac.CodigoTipoAssociacao and ta.IniciaisTipoAssociacao = 'RC'                   
                                  where ac.CodigoObjetoAssociado = RCP.CodigoRecursoCorporativo),-1) as CodigoCalendario, rcp.dataInclusao,
                         RCP.CodigoRecursoCorporativo, case when rcp.CodigoRecursoCorporativo is null then 'Não' else 'Sim' end as RecursoCorporativo,
						 RC.DataDesativacaoRecurso DataDesativacaoRecursoCorporativo, RC.IndicaRecursoAtivo IndicaRecursoCorporativoAtivo 
                    FROM {0}.{1}.RecursoCronogramaProjeto RCP inner join
                         {0}.{1}.TipoRecurso TR on (TR.CodigoTipoRecurso = RCP.CodigoTipoRecurso) left join
						 {0}.{1}.RecursoCorporativo RC on RC.CodigoRecursoCorporativo = rcp.CodigoRecursoCorporativo
                   WHERE CodigoCronogramaProjeto = '{2}' ", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getRecursosCorporativos(string codigoProjeto, bool OcultarRecursoCronograma, int CodigoEntidade, DateTime? Inicio, DateTime? Termino)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = "";
        try
        {
            string recursosCronograma = "";
            string where = "";
            // se tem codigo do cronograma é para mostrar os recursos corporativos que estejam associados a ele ou para mostrar os que ainda não estão associados a ele.
            if (codigoProjeto != "")
            {
                // OcultarRecursoCronograma define se é para mostrar os RC associados ou se é para ocultá-los
                string tipoJoin = OcultarRecursoCronograma ? "LEFT " : "INNER ";
                recursosCronograma = string.Format(
                    @" {2} JOIN
                       (SELECT RCP.CodigoRecursoCorporativo
                          FROM {0}.{1}.RecursoCronogramaProjeto RCP
                         WHERE RCP.CodigoCronogramaProjeto = '{3}') RCP on (RCP.CodigoRecursoCorporativo = RC.CodigoRecursoCorporativo)
                     ", bancodb, Ownerdb, tipoJoin, codigoProjeto);

                //se for para ocultar os recursos que já estão associados ao cronograma
                if (OcultarRecursoCronograma)
                    where = " AND RCP.CodigoRecursoCorporativo is null";
            }

            // se informou o período, deve retornar a disponibilidade
            string selectDisponibilidade = ", 0.0 as Disponibilidade";
            if (Inicio != null && Termino != null)
                selectDisponibilidade = string.Format(
                    @", {0}.{1}.f_GetDisponibilidadeRecurso(RC.CodigoRecursoCorporativo, (Select codigoProjeto 
                                                                                            from CronogramaProjeto 
                                                                                           where CodigoCronogramaProjeto = '{2}'), Convert(datetime, '{3} 00:00:00', 103), 
                                                                                                                                   Convert(datetime, '{4} 23:59:59', 103) ) as Disponibilidade ",
                    bancodb, Ownerdb, codigoProjeto, Inicio.Value.ToString("dd/MM/yyyy"), Termino.Value.ToString("dd/MM/yyyy"));
            else
                selectDisponibilidade = string.Format(", convert(decimal, null) as Disponibilidade");


            comandoSQL = string.Format(
                @"SELECT TR.DescricaoTipoRecurso, RC.CodigoTipoRecurso, RC.NomeRecursoCorporativo, 
                          dbo.f_GetCustoUnitarioRecursoCorporativo(RC.CodigoRecursoCorporativo, 'HN') as CustoHora, 
                          dbo.f_GetCustoUnitarioRecursoCorporativo(RC.CodigoRecursoCorporativo, 'HE') as CustoHoraExtra, 
                          dbo.f_GetCustoUnitarioRecursoCorporativo(RC.CodigoRecursoCorporativo, 'UR') as CustoUso, 
                          -- RC.CustoHora, RC.CustoHoraExtra, RC.CustoUso, 
                          RC.CodigoRecursoCorporativo, GR.DescricaoGrupo, GR.CodigoGrupoRecurso, RC.UnidadeMedidaRecurso,
                          RC.Anotacoes
                          {5}
                    FROM  {0}.{1}.RecursoCorporativo RC inner join
                          {0}.{1}.TipoRecurso TR on (TR.CodigoTipoRecurso = RC.CodigoTipoRecurso) inner join
                          {0}.{1}.GrupoRecurso GR on (GR.CodigoGrupoRecurso = RC.CodigoGrupoRecurso )  {2}
                    WHERE RC.CodigoEntidade = {3} {4} AND RC.[DataDesativacaoRecurso] IS NULL AND RC.IndicaRecursoAtivo = 'S'
                    ORDER By TR.DescricaoTipoRecurso, RC.NomeRecursoCorporativo
                 ", bancodb, Ownerdb, recursosCronograma, CodigoEntidade, where, selectDisponibilidade);

            return classeDados.getDataSet(comandoSQL);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " " + comandoSQL);
        }

        // return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getDisponibilidadeRecursosCorporativos(string codigoProjeto, string listaRecursos, DateTime? Inicio, DateTime? Termino)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = "";
        try
        {
            string selectDisponibilidade = ", 0.0 as Disponibilidade";
            if (Inicio != null && Termino != null)
                selectDisponibilidade = string.Format(
                    @"{0}.{1}.f_GetDisponibilidadeRecurso(RC.CodigoRecursoCorporativo, (Select codigoProjeto 
                                                                                            from CronogramaProjeto 
                                                                                           where CodigoCronogramaProjeto = '{2}'), Convert(datetime, '{3} 00:00:00', 103), 
                                                                                                                                   Convert(datetime, '{4} 23:59:59', 103) ) as Disponibilidade ",
                    bancodb, Ownerdb, codigoProjeto, Inicio.Value.ToString("dd/MM/yyyy"), Termino.Value.ToString("dd/MM/yyyy"));

            comandoSQL = string.Format(
            @"SELECT CodigoRecursoCorporativo, NomeRecursoCorporativo,
                         {2}
                    FROM {0}.{1}.RecursoCorporativo RC 
                   WHERE CodigoRecursoCorporativo IN ({3})
                   ORDER BY CodigoRecursoCorporativo", bancodb, Ownerdb, selectDisponibilidade, listaRecursos);

            return classeDados.getDataSet(comandoSQL);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " " + comandoSQL);
        }

        // return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getAtribuicaoRecursosCronograma(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        // ACG: 09/11/2015 - Chamado "P432294". Foi incluído a subquerie para consultar se a atribuição está vinculada a algum lancamento financeiro
        string comandoSQL = string.Format(
             @" SELECT ART.CodigoAtribuicao, ART.CodigoTarefa, ART.CodigoRecursoProjeto, ART.UnidadeAtribuicao, ART.Custo, ART.Trabalho,
                           ART.CustoUso, ART.CustoExtra, ART.PercentualFisicoConcluido, ART.CustoReal, ART.TrabalhoReal, ART.dataAtribuicao as CriadoEm,
                           ART.CustoHoraRecursoAtribuido, ART.Inicio, ART.Termino, ART.UnidadeAtribuicaoReal, ART.CustoUnitarioReal,
                           (SELECT count(1) FROM LancamentoFinanceiro 
						     WHERE CodigoAtribuicao = ART.CodigoAtribuicao 
							   AND DataExclusao is null) QtdeLancamentoFinanceiro
                    FROM {0}.{1}.AtribuicaoRecursoTarefa ART INNER JOIN
                         {0}.{1}.tarefaCronogramaProjeto tcp on tcp.CodigoCronogramaProjeto = art.CodigoCronogramaProjeto and
                                                                tcp.codigoTarefa = art.codigoTarefa AND
                                                                tcp.dataExclusao is null
                   WHERE ART.CodigoCronogramaProjeto = '{2}' 
                   ORDER BY ART.CodigoTarefa", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getLancamentoFinanceiroAtribuicaoRecursosCronograma(string codigoProjeto, Int64 codigoAtribuicao)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        // ACG: 09/11/2015 - Chamado "P432294". Foi incluído para consultar os lancamento financeiro vinculados a atribuição
        string comandoSQL = string.Format(
             @" SELECT LF.*
                      FROM LancamentoFinanceiro LF inner join
                           AtribuicaoRecursoTarefa ART on ART.CodigoAtribuicao = LF.CodigoAtribuicao
                     WHERE ART.CodigoCronogramaProjeto = '{0}'
                       AND LF.CodigoAtribuicao = {1}
                       AND DataExclusao is null", codigoProjeto, codigoAtribuicao);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            return ds;

        return null;
    }

    //        [SoapHeader("segurancaWS")]
    //        [WebMethod]
    //        public decimal? getDisponibilidadeRecurso(string codigoCronograma, int codigoRecurso, DateTime inicio, DateTime termino)
    //        {
    //            if (segurancaWS == null || segurancaWS.Key != key)
    //                return null;

    //            // verifica se a chave2 ainda é válida.
    //            int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
    //            if (codigoUsuario < 0)
    //                return null;

    //            string comandoSQL = string.Format(
    //                 @"declare @CodigoProjeto int
    //                   declare @DataInicio Datetime
    //                   declare @DataTermino Datetime
    //                       set @CodigoProjeto = (Select codigoProjeto from CronogramaProjeto where CodigoCronogramaProjeto = '{2}')
    //	                   set @DataInicio = Convert(datetime, '{4}', 103)
    //	                   set @DataTermino = Convert(datetime, '{5}', 103)
    //
    //                    select {0}.{1}.f_GetDisponibilidadeRecurso({3}, @CodigoProjeto, @DataInicio, @DataTermino) Disponibilidade", 
    //                        bancodb, Ownerdb, codigoCronograma, codigoRecurso, inicio.ToString("dd/MM/yyyy"), termino.ToString("dd/MM/yyyy"));

    //            DataSet ds = classeDados.getDataSet(comandoSQL);
    //            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //            {
    //                decimal disponibilidade = decimal.Parse(ds.Tables[0].Rows[0]["Disponibilidade"].ToString());
    //                return disponibilidade;
    //            }

    //            return null;
    //        }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getGrupoRecursos(string codigoEntidade)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        string comandoSQL = string.Format(
             @"SELECT isnull((SELECT DescricaoGrupo + ' - ' FROM {0}.{1}.GrupoRecurso WHERE CodigoGrupoRecurso = GR.GrupoRecursoSuperior), '') DescricaoGrupoPai,
                      * 
                 FROM {0}.{1}.GrupoRecurso GR
                WHERE CodigoEntidade = {2}
                  AND not exists(SELECT 1 FROM {0}.{1}.GrupoRecurso WHERE GrupoRecursoSuperior =  GR.codigoGrupoRecurso)
                ORDER BY DescricaoGrupo ", bancodb, Ownerdb, codigoEntidade);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getInformacoesCronogramaBloqueado(string codigoCronograma)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return null;

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return null;

        if (codigoUsuario != segurancaWS.SystemID)
            return null;

        // verifica se o cronograma está desbloqueado.
        string comandoSQL = string.Format(
            @"  DECLARE @CodigoProjeto int 
                    DECLARE @CodigoUsuario int 
                    DECLARE @CodigoEntidade int 
                    DECLARE @IniciaisPerfil char(10)
                    DECLARE @podeDesbloquear bit

                    SELECT @CodigoProjeto = p.CodigoProjeto,
					       @CodigoEntidade = p.codigoEntidade
					  FROM Projeto p inner join
					       CronogramaProjeto cp on cp.CodigoProjeto = p.CodigoProjeto
					 WHERE CodigoCronogramaProjeto = '{2}'

                     SET @CodigoUsuario = {3}

		          SELECT TOP 1  @IniciaisPerfil = ISNULL(p.IniciaisPerfil, '')
		            FROM InteressadoObjetoPerfil iop
		      INNER JOIN Perfil p on (iop.CodigoPerfil = p.CodigoPerfil)
		           WHERE iop.CodigoUsuario = @CodigoUsuario
                     AND StatusRegistro = 'A' AND p.IniciaisPerfil = 'ADM'
					 AND iop.DataDesativacaoRegistro IS NULL
					 AND p.CodigoEntidade = @CodigoEntidade

                     IF(@IniciaisPerfil = 'ADM')
                     BEGIN
                        SET @podeDesbloquear = 1
                     END
                     ELSE
                     BEGIN
                        SET @podeDesbloquear = 0
                     END
                      
                  SELECT cp.DataCheckoutCronograma, 
                         cp.CodigoUsuarioCheckoutCronograma, 
                         u.NomeUsuario, 
                         cp.CodigoProjeto,
                         @podeDesbloquear as podeDesbloquear
                    FROM {0}.{1}.CronogramaProjeto cp 
              INNER JOIN {0}.{1}.usuario u on u.CodigoUsuario = cp.CodigoUsuarioCheckoutCronograma
                   WHERE CodigoCronogramaProjeto = '{2}'", bancodb, Ownerdb, codigoCronograma, codigoUsuario);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
        {
            return null;
        }

        return ds;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int chekoutCronogramaUsuario(string codigoProjeto, ref string NomeDoUsuarioQueBloqueou)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        // verifica se o cronograma está desbloqueado.
        string comandoSQL = string.Format(
            @" SELECT isnull(CodigoUsuarioCheckoutCronograma,-1) codigoUsuario
                    FROM {0}.{1}.CronogramaProjeto
                   WHERE CodigoCronogramaProjeto = '{2}' 
                     /*AND DataCheckoutCronograma is null*/", bancodb, Ownerdb, codigoProjeto);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
        {
            return -4; // Não foi possível bloquear o cronograma, erro ao executar o select de conferência
        }
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            DataSet dsNomeUsuarioQueBloqueou = classeDados.getDataSet(@"SELECT ISNULL(NomeUsuario, ' ') 
                                                                              FROM Usuario 
                                                                             WHERE CodigoUsuario = " + ds.Tables[0].Rows[0][0].ToString());

            NomeDoUsuarioQueBloqueou = (dsNomeUsuarioQueBloqueou.Tables[0].Rows.Count == 0) ? "" : dsNomeUsuarioQueBloqueou.Tables[0].Rows[0][0].ToString();
        }

        int codigoUsuarioCheckoutCronograma = (int)ds.Tables[0].Rows[0]["codigoUsuario"];

        // se o cronograma está bloqueado, não pode continuar.
        if (codigoUsuarioCheckoutCronograma >= 0)// && codigoUsuarioCheckoutCronograma != codigoUsuario)
        {
            return 0; // Não foi possível bloquear o cronograma, pois o mesmo já está bloqueado.
        }

        // bloqueia o cronograma para o usuario autenticado
        comandoSQL = string.Format(
             @" UPDATE {0}.{1}.CronogramaProjeto
                       SET DataCheckoutCronograma = GetDate()
                         , CodigoUsuarioCheckoutCronograma = {3}
                     WHERE CodigoCronogramaProjeto = '{2}'", bancodb, Ownerdb, codigoProjeto, codigoUsuario);

        int afetado = 0;
        classeDados.execSQL(comandoSQL, ref afetado);

        return 1; // 1 tá tudo certo. O Cronograma foi bloqueado com sucesso.
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int chekinCronogramaUsuario(string codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        // Desbloqueia o cronograma para o usuario autenticado
        string comandoSQL = string.Format(
             @" UPDATE {0}.{1}.CronogramaProjeto
                       SET DataCheckoutCronograma = null
                         , CodigoUsuarioCheckoutCronograma = null
                     WHERE CodigoCronogramaProjeto = '{2}'
                       /*AND CodigoUsuarioCheckoutCronograma = {3}*/", bancodb, Ownerdb, codigoProjeto, codigoUsuario);

        int afetado = 0;
        classeDados.execSQL(comandoSQL, ref afetado);

        return afetado; // afetado deve ser maior ou igual a 1
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int getPermissaoLinhaBase(int codigoProjetoCorporativo)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        string comandoSQL = string.Format(
            @"SELECT {0}.{1}.f_GetPermissaoLinhaBase({2}, {3})
                 ", bancodb, Ownerdb, codigoProjetoCorporativo, codigoUsuario);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        // não encontrou registro para verificar a permissão do usuário
        return -4;
    }

    /// <summary>
    /// Salva a linha de base para o cronograma indicado em guidProjeto
    /// </summary>
    /// <param name="key"></param>
    /// <param name="codigoUsuario">Usuário que está salvando a linha de base</param>
    /// <param name="PermissaoLinhaBase">(Opcional) Indica a permissão do usuário para salvar linha de base</param>
    /// <param name="guidProjeto">Código do cronograma</param>
    /// <param name="codigoTarefas">Array com as tarefas a serem salvas. Nulo para todas as tarefas</param>
    /// <param name="anotacoes">Anotações</param>
    /// <returns></returns>
    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int salvarLinhaBase(bool UsandoReplanejamento, int? PermissaoLinhaBase, string codigoCronogramaProjeto, int[] codigoTarefas, string anotacoes)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (codigoCronogramaProjeto != "" && codigoCronogramaProjeto.Length == 36)
        {
            string comandoSQL = "";
            try
            {
                // se codigoTarefa é nulo, todas as tarefas ficam pendentes
                string whereTarefas = "";
                // caso contrário, apenas as tarefas indicadas no vetor ficarão pendentes
                if (codigoTarefas != null && codigoTarefas.Length > 0)
                {
                    whereTarefas = " AND CodigoTarefa in (";
                    foreach (int codigoTarefa in codigoTarefas)
                        whereTarefas += codigoTarefa + ", ";
                    whereTarefas = whereTarefas.Substring(0, whereTarefas.Length - 2) + ")";
                }

                // atualiza a tabela de tarefas indicando que existe pendencias nas tarefas relacionadas a linha de base
                comandoSQL = string.Format(
                    @"UPDATE {0}.{1}.TarefaCronogramaProjeto
                         SET IndicaLinhaBasePendente = 'S'
                       WHERE CodigoCronogramaProjeto = '{2}'  {3}

                     ", bancodb, Ownerdb,
                        codigoCronogramaProjeto, whereTarefas);

                // se PermissaoLinhaBase = 2 gravar status = 'AP" // se PermissaoLinhaBase = 1 gravar status = "PA"
                string StatusAprovacao = (PermissaoLinhaBase.HasValue && PermissaoLinhaBase.Value == 2) ? "AP" : "PA";

                // Se trabalha com replanejamento, usar modelo 2, se não, usar modelo 1
                int ModeloLinhaBase = UsandoReplanejamento ? 2 : 1;

                // insere um registro na tabela LinhaBaseCronograma
                comandoSQL += string.Format(
                    @"DECLARE @VersaoLinhaBase int

                          DECLARE @in_CodigoCronogramaProjeto varchar(64)
                          DECLARE @CodigoCronogramaOrigem varchar(64)

                             SET @in_CodigoCronogramaProjeto = '{2}'
 
                           SELECT @CodigoCronogramaOrigem = cp.CodigoCronogramaOrigem
                             FROM {0}.{1}.CronogramaProjeto AS cp
                            WHERE cp.CodigoCronogramaProjeto = @in_CodigoCronogramaProjeto
                
                              SET @VersaoLinhaBase = (SELECT ISNULL(MAX(lb.VersaoLinhaBase),0)+1 AS VersaoLinhaBase
                                                        FROM {0}.{1}.LinhaBaseCronograma AS lb INNER JOIN 
                                                             {0}.{1}.CronogramaProjeto AS cp ON (cp.CodigoCronogramaProjeto = lb.CodigoCronogramaProjeto)
                                                       WHERE cp.CodigoCronogramaOrigem = @CodigoCronogramaOrigem );

                        /* Excluído em 23/09 pelo Cantão */
                        /*  SET @VersaoLinhaBase = (SELECT ISNULL(MAX(VersaoLinhaBase),0)+1 AS VersaoLinhaBase
                                                    FROM {0}.{1}.LinhaBaseCronograma 
                                                   WHERE CodigoCronogramaProjeto = '{2}') */

                       INSERT INTO {0}.{1}.LinhaBaseCronograma
                            (CodigoCronogramaProjeto, VersaoLinhaBase , Anotacoes, DataSolicitacao, CodigoUsuarioSolicitante, DataStatusAprovacao, StatusAprovacao, CodigoUsuarioAprovacao, ModeloLinhaBase)
                       VALUES
                            (@in_CodigoCronogramaProjeto, @VersaoLinhaBase, '{3}'    , GetDate()      , {4}                     , GetDate()          , '{5}'          , {6}               , {7} )
                    
                     ", bancodb, Ownerdb,
                  codigoCronogramaProjeto, anotacoes, codigoUsuario, StatusAprovacao, StatusAprovacao == "AP" ? codigoUsuario.ToString() : "null", ModeloLinhaBase);

                // executa os comandos SQL
                int regAfetados = 0;
                classeDados.execSQL(comandoSQL, ref regAfetados);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        return 0;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int atualizarModeloCronograma(string codigoCronogramaProjeto, string nome, string descricao)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (codigoCronogramaProjeto != "" && codigoCronogramaProjeto.Length == 36)
        {
            string comandoSQL = "";
            try
            {
                // atualiza a tabela de tarefas indicando que existe pendencias nas tarefas relacionadas a linha de base
                comandoSQL = string.Format(
                    @"UPDATE {0}.{1}.CronogramaProjeto
                         SET NomeProjeto = '{3}'
                           , DescricaoTipoCronograma = '{4}'
                           , DataUltimaAlteracao = getdate()
                           , CodigoUsuarioUltimaAlteracao = {5} 
                       WHERE CodigoCronogramaProjeto = '{2}'

                     ", bancodb, Ownerdb,
                        codigoCronogramaProjeto, nome, descricao, codigoUsuario);

                // executa os comandos SQL
                int regAfetados = 0;
                classeDados.execSQL(comandoSQL, ref regAfetados);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return 0;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int excluirModeloCronograma(string codigoCronogramaProjeto, int codigoTipoCronograma)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (codigoCronogramaProjeto != "" && codigoCronogramaProjeto.Length == 36)
        {
            string comandoSQL = "";
            try
            {
                // atualiza a tabela de tarefas indicando que existe pendencias nas tarefas relacionadas a linha de base
                comandoSQL = string.Format(
                    @"UPDATE {0}.{1}.CronogramaProjeto
                         SET DataExclusao = getdate()
                           , CodigoUsuarioUltimaAlteracao = {3} 
                       WHERE CodigoCronogramaProjeto = '{2}'
                         AND CodigoTipoCronograma = {4}

                     ", bancodb, Ownerdb,
                        codigoCronogramaProjeto, codigoUsuario, codigoTipoCronograma);

                // executa os comandos SQL
                int regAfetados = 0;
                classeDados.execSQL(comandoSQL, ref regAfetados);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return 0;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int execP_AtualizaStatusProjetos(string codigoCronogramaProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (codigoCronogramaProjeto != "" && codigoCronogramaProjeto.Length == 36)
        {
            string comandoSQL = "";
            try
            {
                // atualiza a tabela de tarefas indicando que existe pendencias nas tarefas relacionadas a linha de base
                comandoSQL = string.Format(
                    @"DECLARE @NomeUsuario varchar(20)
                              SET @NomeUsuario = (SELECT convert(varchar, codigoUsuario) +';'+ left(nomeUsuario, 40) 
                                                    FROM {0}.{1}.usuario 
                                                   WHERE CodigoUsuario = {2} )

                          DECLARE @CodigoProjeto int
                              SET @CodigoProjeto = (SELECT CodigoProjeto 
                                                      FROM {0}.{1}.CronogramaProjeto 
                                                     WHERE CodigoCronogramaProjeto = '{3}')

                           EXEC {0}.{1}.p_AtualizaStatusProjetos @NomeUsuario, @CodigoProjeto
                     ", bancodb, Ownerdb,
                    codigoUsuario, codigoCronogramaProjeto);

                // executa os comandos SQL
                int afetatos = 0;
                classeDados.execSQL(comandoSQL, ref afetatos);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + comandoSQL);
            }
        }

        return 0;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int execP_IniciaFluxos(string codigoCronogramaProjeto, string justificativa, string inicias, string parametros)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (codigoCronogramaProjeto != "" && codigoCronogramaProjeto.Length == 36)
        {
            string comandoSQL = "";
            try
            {
                // atualiza a tabela de tarefas indicando que existe pendencias nas tarefas relacionadas a linha de base
                comandoSQL = string.Format(
                    @" EXEC {0}.{1}.p_crono_IniciaFluxoAlteracaoCronograma '{2}', {3}, '{4}', '{5}', '{6}'   
                     ", bancodb, Ownerdb,
                    codigoCronogramaProjeto, codigoUsuario, justificativa.Replace("'", "''"), inicias, parametros);

                // executa os comandos SQL
                int afetatos = 0;
                classeDados.execSQL(comandoSQL, ref afetatos);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + comandoSQL);
            }
        }

        return 0;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int execP_Crono_ReplicaCronograma(string codigoCronogramaProjeto, out string codigoCronogramaProjetoReplicado)
    {
        codigoCronogramaProjetoReplicado = "";

        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (codigoCronogramaProjeto != "" && codigoCronogramaProjeto.Length == 36)
        {
            string comandoSQL = "";
            try
            {
                // Executa o procedimento para replicar o cronograma
                comandoSQL = string.Format(
                    @" DECLARE @return_value int,
                                   @MsgRetorno varchar(5000)

                           EXEC	@return_value = {0}.{1}.p_crono_ReplicaCronograma '{2}',{3}, @MsgRetorno OUTPUT

                           SELECT @return_value as CodigoRetorno, @MsgRetorno as MsgRetorno ", bancodb, Ownerdb, codigoCronogramaProjeto, codigoUsuario);

                // executa os comandos SQL
                DataSet ds = classeDados.getDataSet(comandoSQL);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count == 1)
                {
                    if (ds.Tables[0].Rows[0]["CodigoRetorno"].ToString() == "1")
                    {
                        codigoCronogramaProjetoReplicado = ds.Tables[0].Rows[0]["MsgRetorno"].ToString();
                        return 1;
                    }
                    else
                        throw new Exception(ds.Tables[0].Rows[0]["MsgRetorno"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + comandoSQL);
            }
        }

        return 0;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int execP_Crono_ExcluiCronogramaReplanejamento(string codigoCronogramaProjetoReplicado, int codigoProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        string comandoSQL = "";
        try
        {
            // Marca o cronograma como excluído e retira a referencia de replanejamento no projeto oficial
            comandoSQL = string.Format(
                @"-- O processo será executada dentro de uma transação
	                  -------------------------------------------------------------------------------
                      DECLARE @MsgRetorno varchar(8000)
	                  BEGIN TRANSACTION

	                  BEGIN TRY
	
                            -- Cronograma de replanejamento
                              UPDATE CronogramaProjeto
                                 SET DataExclusao = getdate()
                               WHERE CodigoCronogramaProjeto = '{2}'

                            -- Cronograma Oficial 
                             DECLARE @CodigoCronogramaOficial varchar(64)
                              SELECT @CodigoCronogramaOficial = CodigoCronogramaProjeto 
                                FROM CronogramaProjeto 
                               WHERE CodigoProjeto = {3}
							    AND DataExclusao is null                              

                              UPDATE CronogramaProjeto
                                SET CodigoCronogramaReplanejamento = null
                              WHERE CodigoCronogramaProjeto = @CodigoCronogramaOficial

		                      COMMIT TRANSACTION -- Tudo certo, cronograma de replanejamento excluído
		                      SELECT 1 as CodigoRetorno, '' as MsgRetorno -- indica que deu certo

	                  END TRY
	                  BEGIN CATCH
    
		                    ROLLBACK TRANSACTION -- Deu algo errado
                            SET @MsgRetorno = ERROR_MESSAGE()
		                    SELECT 0 as CodigoRetorno, @MsgRetorno as MsgRetorno  -- retorna 0 para indicar que o cronograma não foi excluído
          
	                  END CATCH ", bancodb, Ownerdb, codigoCronogramaProjetoReplicado, codigoProjeto);

            // executa os comandos SQL
            DataSet ds = classeDados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count == 1)
            {
                if (ds.Tables[0].Rows[0]["CodigoRetorno"].ToString() == "0")
                    throw new Exception(ds.Tables[0].Rows[0]["MsgRetorno"].ToString());
                else
                    return 1;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " " + comandoSQL);
        }

        return 0;
    }

    #region Salvar cronograma

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public string getArquivoAtzCronograma(string CodigoCronogramaProjeto)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return "-1"; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return "-2"; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return "-3"; // codigo do usuário com a chave é diferente do usuário autenticado.

        string nomeArquivoComandoSQL = diretorioCronogramas + "\\" + CodigoCronogramaProjeto + ".sql";
        if (!File.Exists(nomeArquivoComandoSQL))
            return "-4"; // arquivo não existe;

        StreamReader arquivoLog = new StreamReader(nomeArquivoComandoSQL);
        string comandoSql = arquivoLog.ReadToEnd();
        arquivoLog.Close();

        return comandoSql;
    }



    public static string Unzip(byte[] bytes)
    {
        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new System.IO.Compression.GZipStream(msi, System.IO.Compression.CompressionMode.Decompress))
            {
                byte[] bytes2 = new byte[4096];
                int cnt;
                while ((cnt = gs.Read(bytes2, 0, bytes2.Length)) != 0)
                {
                    mso.Write(bytes2, 0, cnt);
                }
            }
            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int uploadArquivoCronograma(string CodigoCronogramaProjeto, bool publicarCronograma, byte[] conteudoCronograma, int formato, bool importado, out DateTime DataServidor, out string retorno)
    {
        DataServidor = DateTime.Now;
        retorno = string.Empty;

        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (conteudoCronograma == null || conteudoCronograma.Length == 0)
            return -4;// não veio o cronograma

        // em caso de erro, recebe o comand
        string comandoSQLErro = "";
        try
        {
            bool cronogramaNovo = false;
            // se é um cronograma novo, vamos primeiro criar o "CodigoCronograma"
            if (CodigoCronogramaProjeto == "")
            {
                cronogramaNovo = true;
                CodigoCronogramaProjeto = System.Guid.NewGuid().ToString();
                retorno = CodigoCronogramaProjeto;
            }

            string xmlCronograma = Unzip(conteudoCronograma);

            tasques dsProjetoTasques = new tasques();
            dsProjetoTasques.ReadXml(new StringReader(xmlCronograma));

            SalvarCopiaArquivoCronogramaNoBancoDeDados(CodigoCronogramaProjeto, codigoUsuario, xmlCronograma, dsProjetoTasques);

            // Salva o cronograma no HD para ser consultado como HISTÓRICO
            // -----------------------------------------------------------
            if (salvarHistoricoCronogramasEmDisco)
            {
                SalvarCopiaArquivoCronogramaEmDisco(CodigoCronogramaProjeto, xmlCronograma);
            }

            // Salva o cronograma no banco de dados
            salvarXmlCronograma(cronogramaNovo, importado, segurancaWS.SystemID, CodigoCronogramaProjeto, publicarCronograma, dsProjetoTasques, out comandoSQLErro);
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
            if (formato == verificaValidadeChaveAutenticacao(segurancaWS.Key2))
                retorno = retorno + "¥¥" + comandoSQLErro;
            return -5;
        }
        return 0; // tudo certo, o cronograma foi salvo
    }

    private void SalvarCopiaArquivoCronogramaEmDisco(string CodigoCronogramaProjeto, string xmlCronograma)
    {
        try
        {
            // se o diretório não existir, será criado
            if (!Directory.Exists(diretorioCronogramas))
                Directory.CreateDirectory(diretorioCronogramas);

            // se o diretório ainda não existir, retorna um erro
            if (!Directory.Exists(diretorioCronogramas))
                throw new Exception("Comando é executado mas não surti efeito.");

        }
        catch (Exception ex)
        {
            throw new Exception("Não foi possível criar o diretório para salvar o cronograma: " + ex.Message);
        }

        // Salva o cronograma no HD
        string arquivo = "";
        try
        {
            arquivo = diretorioCronogramas + "\\" + CodigoCronogramaProjeto + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            //segurancaWS.Xml.Save(arquivo);
            File.WriteAllText(arquivo, xmlCronograma);
        }
        catch (Exception ex)
        {
            throw new Exception("Não foi possível salvar o cronograma no servidor: " + ex.Message);
        }
    }

    private void SalvarCopiaArquivoCronogramaNoBancoDeDados(string CodigoCronogramaProjeto, int codigoUsuario, string xmlCronograma, tasques dsProjetoTasques)
    {
        // tenta salvar o XML do cronograma no banco de dados
        try
        {
            DataRow rowProjeto = dsProjetoTasques.Projeto[0];
            string versaoTasquesDestino = rowProjeto["versaoTasquesDestino"].ToString();
            string nomeEquipamento = rowProjeto["nomeEquipamento"].ToString();

            // Salva o arquivo XML no banco de dados - Tabela: "CronogramaProjeto_ArquivoXMLCliente"
            string comandoSQL = string.Format(
                @"INSERT INTO CronogramaProjeto_ArquivoXMLCliente (CodigoCronogramaProjeto, DataInclusao, codigoUsuarioInclusao, IdentificadorMaquina, VersaoDesktop, xmlCronogramaProjeto)
                          VALUES ('{0}', GETDATE(), {1}, '{2}', '{3}', @xml) ", CodigoCronogramaProjeto, codigoUsuario, nomeEquipamento, versaoTasquesDestino);

            SqlConnection conn = new SqlConnection(classeDados.getStringConexao());
            SqlCommand sqlComando = new SqlCommand(comandoSQL, conn);
            sqlComando.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
            sqlComando.CommandType = CommandType.Text;
            sqlComando.Parameters.AddWithValue("@xml", xmlCronograma);
            sqlComando.Connection.Open();
            try
            {
                sqlComando.ExecuteNonQuery();
            }
            finally
            {
                sqlComando.Connection.Close();
            }
        }
        catch // se não conseguiu, não faz nada
        {

        }
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public int uploadXMLCronograma(string CodigoCronogramaProjeto, bool publicarCronograma, XmlDocument XmlCronograma, int formato, bool importado, out DateTime DataServidor, out string retorno)
    {
        DataServidor = DateTime.Now;
        retorno = "";

        if (segurancaWS == null || segurancaWS.Key != key)
            return -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            return -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (segurancaWS.Xml == null)
            return -4;// não veio o cronograma

        // em caso de erro, recebe o comand
        string comandoSQLErro = "";
        try
        {
            bool cronogramaNovo = false;
            // se é um cronograma novo, vamos primeiro criar o "CodigoCronograma"
            if (CodigoCronogramaProjeto == "")
            {
                cronogramaNovo = true;
                CodigoCronogramaProjeto = System.Guid.NewGuid().ToString();
                retorno = CodigoCronogramaProjeto;
            }

            // Cria o objeto dsProjetoTasques a partir do xml recebido do desktop
            Stream CronogramaEmXML = new MemoryStream();
            segurancaWS.Xml.Save(CronogramaEmXML);

            // Posiciona o cursor no início do Stream
            CronogramaEmXML.Position = 0;
            tasques dsProjetoTasques = new tasques();
            dsProjetoTasques.ReadXml(CronogramaEmXML);

            // tenta salvar o XML do cronograma no banco de dados
            try
            {
                DataRow rowProjeto = dsProjetoTasques.Projeto[0];
                string versaoTasquesDestino = rowProjeto["versaoTasquesDestino"].ToString();
                string nomeEquipamento = rowProjeto["nomeEquipamento"].ToString();

                // Salva o arquivo XML no banco de dados - Tabela: "CronogramaProjeto_ArquivoXMLCliente"
                string comandoSQL = string.Format(
                    @"INSERT INTO CronogramaProjeto_ArquivoXMLCliente (CodigoCronogramaProjeto, DataInclusao, codigoUsuarioInclusao, IdentificadorMaquina, VersaoDesktop, xmlCronogramaProjeto)
                          VALUES ('{0}', GETDATE(), {1}, '{2}', '{3}', @xml) ", CodigoCronogramaProjeto, codigoUsuario, nomeEquipamento, versaoTasquesDestino);

                SqlConnection conn = new SqlConnection(classeDados.getStringConexao());
                SqlCommand sqlComando = new SqlCommand(comandoSQL, conn);
                sqlComando.CommandTimeout = CDIS.ClasseDados.TimeOutSqlCommand;
                sqlComando.CommandType = CommandType.Text;
                sqlComando.Parameters.AddWithValue("@xml", segurancaWS.Xml.InnerXml.Trim());
                sqlComando.Connection.Open();
                try
                {
                    sqlComando.ExecuteNonQuery();
                }
                finally
                {
                    sqlComando.Connection.Close();
                }
            }
            catch // se não conseguiu, não faz nada
            {

            }

            // Salva o cronograma no HD para ser consultado como HISTÓRICO
            // -----------------------------------------------------------
            if (salvarHistoricoCronogramasEmDisco)
            {
                try
                {
                    // se o diretório não existir, será criado
                    if (!Directory.Exists(diretorioCronogramas))
                        Directory.CreateDirectory(diretorioCronogramas);

                    // se o diretório ainda não existir, retorna um erro
                    if (!Directory.Exists(diretorioCronogramas))
                        throw new Exception("Comando é executado mas não surti efeito.");

                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível criar o diretório para salvar o cronograma: " + ex.Message);
                }

                // Salva o cronograma no HD
                string arquivo = "";
                try
                {
                    arquivo = diretorioCronogramas + "\\" + CodigoCronogramaProjeto + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                    segurancaWS.Xml.Save(arquivo);
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível salvar o cronograma no servidor: " + ex.Message);
                }
            }

            // Salva o cronograma no banco de dados
            salvarXmlCronograma(cronogramaNovo, importado, segurancaWS.SystemID, CodigoCronogramaProjeto, publicarCronograma, dsProjetoTasques, out comandoSQLErro);
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
            if (formato == verificaValidadeChaveAutenticacao(segurancaWS.Key2))
                retorno = retorno + "¥¥" + comandoSQLErro;
            return -5;
        }
        return 0; // tudo certo, o cronograma foi salvo
    }

    public bool salvarXmlCronograma(bool cronogramaNovo, bool cronogramaImportado, int codigoUsuario, string CodigoCronogramaProjeto, bool publicarCronograma, tasques dsProjetoTasques, out string retornoComandoSQLEerro)
    {
        bool existeLinhaBase = false;
        StringBuilder comandoSQL = new StringBuilder();
        retornoComandoSQLEerro = "";
        string metodoSendoExecutado = "salvarXmlCronograma_Inicio()";
        try
        {
            // tasques dsProjetoTasques = new tasques();
            // dsProjetoTasques.ReadXml(arquivoCronogramaTasques);
            metodoSendoExecutado = "salvarXmlCronograma_ReadXML()";
            DataRow rowProjeto = dsProjetoTasques.Projeto[0];

            int codigoEntidade = (int)rowProjeto["codigoEntidadeCorporativa"];
            int codigoCalendario = (int)rowProjeto["codigoCalendario"];

            // se é um cronograma importado, todas as tarefas existentes devem ser excluídas

            metodoSendoExecutado = "salvarXmlCronograma_InicioSQL()";
            comandoSQL.AppendFormat(
                 @"
SET XACT_ABORT ON
BEGIN TRY
    BEGIN TRAN 
                   DECLARE @CodigoCalendario int
                   DECLARE @CodigoUsuario int
                   DECLARE @MinutosDia int
                   DECLARE @MinutosSemana int
                   DECLARE @DiasMes int
                   DECLARE @CodigoEntidade int
                   DECLARE @CodigoCronogramaProjeto varchar(64)
                   DECLARE @DataAtual datetime
                    
                   SET @DataAtual = dateadd(ms, datepart(ms, GETDATE())*-1, GETDATE())

                   SET @CodigoUsuario = {2}
                   SET @CodigoCalendario = {3}
                   SET @CodigoEntidade = {4}
                   SET @CodigoCronogramaProjeto = '{5}'

                   /*SELECT @CodigoCalendario = c.[CodigoCalendario] 
                     FROM Calendario AS c	INNER JOIN 
                          AssociacaoCalendario	AS ac ON (ac.[CodigoCalendario]			= c.[CodigoCalendario]) INNER JOIN 
                          TipoAssociacao AS ta	ON (ta.[CodigoTipoAssociacao]	= ac.[CodigoTipoAssociacao])
                    WHERE c.[CodigoEntidade] = @CodigoEntidade
                      AND ta.[IniciaisTipoAssociacao]	= 'EN'
                      AND ac.[CodigoObjetoAssociado]	= @CodigoEntidade  
                      AND c.IndicaCalendarioPadrao = 'S' */

                   SELECT @MinutosDia = c.HorasDia * 60 
                        , @MinutosSemana = c.HorasSemana * 60
                        , @DiasMes = c.DiasMes
                     FROM Calendario AS c
                    WHERE CodigoCalendario = {3}
                     
                  ", bancodb, Ownerdb, codigoUsuario, codigoCalendario, codigoEntidade, CodigoCronogramaProjeto);

            metodoSendoExecutado = "comandoSQLIdentificacaoCronograma()";
            comandoSQL.Append(comandoSQLIdentificacaoCronograma(cronogramaNovo, cronogramaImportado, codigoUsuario, CodigoCronogramaProjeto, publicarCronograma, dsProjetoTasques));

            metodoSendoExecutado = "comandoSQLTarefasCronograma()";
            comandoSQL.Append(comandoSQLTarefasCronograma(dsProjetoTasques, cronogramaImportado, out existeLinhaBase));

            metodoSendoExecutado = "comandoSQLRecursosCronograma()";
            comandoSQL.Append(comandoSQLRecursosCronograma(dsProjetoTasques));

            metodoSendoExecutado = "comandoSQLAtribuicaoRecursosCronograma()";
            comandoSQL.Append(comandoSQLAtribuicaoRecursosCronograma(dsProjetoTasques));

            metodoSendoExecutado = "comandoSQLPredecessoras()";
            comandoSQL.Append(comandoSQLPredecessoras(dsProjetoTasques));

            metodoSendoExecutado = "comandoSQLAssociaTipoTarefaCronograma()";
            comandoSQL.Append(comandoSQLAssociaTipoTarefaCronograma(dsProjetoTasques));

            /* LINHA DE BASE IMPORTADA*/
            // se é um cronograma importado e possui linha de base
            if (cronogramaImportado && existeLinhaBase)
            {
                metodoSendoExecutado = "comandoSQLLinhaBaseMSProject()";
                // insere um registro na tabela LinhaBaseCronograma
                string anotacoes = "Linha de base importada do MSProject ";
                comandoSQL.AppendFormat(
                    @"
                      DECLARE @VersaoLinhaBase int
                      SET @VersaoLinhaBase = 1

                      INSERT INTO {0}.{1}.LinhaBaseCronograma
                            (CodigoCronogramaProjeto, VersaoLinhaBase , Anotacoes, DataSolicitacao, CodigoUsuarioSolicitante, DataStatusAprovacao, StatusAprovacao, CodigoUsuarioAprovacao)
                      VALUES
                            ('{2}'                  , @VersaoLinhaBase, '{3}'    , GetDate()      , {4}                     , GetDate()          , 'AP'          , {4}                   )
                    
                     ", bancodb, Ownerdb, CodigoCronogramaProjeto, anotacoes, codigoUsuario);
            }

            /*Em 30/06/14, Geter pediu para atualizar as informações de publicação após salvar todas as outras informações do cronograma
             --------------------------------------------------------------------------------------------------------------------------- */
            metodoSendoExecutado = "comandoSQLStatusPublicacao()"; // apenas para indicar que chegou até aqui.
            if (publicarCronograma)
            {
                comandoSQL.AppendFormat(
                    @"Update cronogramaProjeto
                                set DataUltimaPublicacao = @DataAtual
                                , CodigoUsuarioUltimaPublicacao = @CodigoUsuario
                                , IndicaPublicacaoPendente = 'N'
                            WHERE codigoCronogramaProjeto = @CodigoCronogramaProjeto ");
            }
            else
                comandoSQL.AppendFormat(
                    @"Update cronogramaProjeto
                                set IndicaPublicacaoPendente = 'S' 
                            WHERE codigoCronogramaProjeto = @CodigoCronogramaProjeto ");


            metodoSendoExecutado = "salvarXmlCronograma_FimSQL()"; // apenas para indicar que chegou até aqui.

            comandoSQL.AppendFormat(
                @" 

                 -- Atribuições (e suas dependências) que não tiveram a data de modificaçao atualizada devem ser excluídas
                 ---------------------------------------------------------------------------------------------------------
                 UPDATE {0}.{1}.LancamentoFinanceiro 
				    SET CodigoAtribuicao = null
			      WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                               FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                              WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                                                AND DataUltimaAlteracao <> @DataAtual  )

/*
-- Trecho comentado sob orientação do Ericsson
----------------------------------------------
                 DELETE FROM {0}.{1}.AtualizacaoDiariaTarefaTimeSheet 
                  WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                               FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                              WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                                                AND DataUltimaAlteracao <> @DataAtual  )

                 DELETE FROM {0}.{1}.AtribuicaoDiariaRecurso
                  WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                    AND NOT EXISTS (SELECT 1 
                                      FROM AtribuicaoRecursoTarefa
                                     WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                                       AND CodigoTarefa = {0}.{1}.AtribuicaoDiariaRecurso.CodigoTarefa 
                                       AND CodigoRecursoProjeto = {0}.{1}.AtribuicaoDiariaRecurso.CodigoRecursoProjeto
                                       AND DataUltimaAlteracao = @DataAtual )
*/
                                       
                 DELETE FROM {0}.{1}.AtribuicaoRecursoTarefa
                  WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                    AND DataUltimaAlteracao <> @DataAtual

                 -- Recursos de projeto que não tiveram a data de modificaçao atualizada devem ser excluídos
                 ---------------------------------------------------------------------------------------------------------
                 DELETE FROM {0}.{1}.RecursoCronogramaProjeto
                  WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                    AND DataUltimaAlteracao <> @DataAtual

                 -- Predecessoras que não tiveram a data de modificaçao atualizada devem ser excluídas
                 ---------------------------------------------------------------------------------------------------------
                 DELETE FROM {0}.{1}.TarefaCronogramaProjetoPredecessoras
                  WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                    AND DataUltimaAlteracao <> @DataAtual

                 -- Tarefas que não tiveram a data de modificaçao atualizada devem ser excluídas
                 ---------------------------------------------------------------------------------------------------------
                 UPDATE {0}.{1}.TarefaCronogramaProjeto
                    SET dataExclusao = @DataAtual
                      , codigoUsuarioExclusao = @CodigoUsuario
                  WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                    AND DataUltimaAlteracao <> @DataAtual
                    AND dataExclusao is null

                 SELECT @CodigoCronogramaProjeto

    COMMIT TRAN -- efetivando a transação
END TRY
BEGIN CATCH
      IF (XACT_STATE()) <> 0
            ROLLBACK TRAN 

      DECLARE @mensagem varchar(8000)
      DECLARE @ErrorSeverity INT
      DECLARE @ErrorState INT
      SET @mensagem = 'CDIS: ' + ERROR_MESSAGE()
      SET @ErrorSeverity = ERROR_SEVERITY()
      SET @ErrorState =  ERROR_STATE()
      RAISERROR (@mensagem, @ErrorSeverity,@ErrorState)

END CATCH
", bancodb, Ownerdb);

            // Executa o script para salvar o projeto
            metodoSendoExecutado = "salvarXmlCronograma_ExecutaComando()";
            int regAfetados = 0;
            classeDados.execSQL(comandoSQL.ToString(), ref regAfetados);
            return true;
        }
        catch (Exception ex)
        {
            // se estava tentando executar o comando SQL...
            if (metodoSendoExecutado == "salvarXmlCronograma_ExecutaComando()")
            {
                try
                {
                    retornoComandoSQLEerro = comandoSQL.ToString();
                    // ...salva o comando em disco
                    string nomeArquivoComandoSQL = diretorioCronogramas + "\\" + CodigoCronogramaProjeto + ".sql";
                    StreamWriter arquivoLog = new StreamWriter(nomeArquivoComandoSQL, true);
                    arquivoLog.WriteLine(ex.Message);
                    arquivoLog.WriteLine("-- ---------------------------------------------------------------------------");
                    arquivoLog.WriteLine("-- ----------------------------  COMANDO SQL ---------------------------------");
                    arquivoLog.WriteLine("-- ---------------------------------------------------------------------------");
                    arquivoLog.WriteLine(comandoSQL.Replace("COMMIT", "ROLLBACK").Replace("." + Ownerdb + ".", ".dba."));
                    arquivoLog.Flush();
                    arquivoLog.Close();
                }
                catch
                {

                }
            }

            throw new Exception(metodoSendoExecutado + "\n" + ex.Message);
        }
    }

    private string comandoSQLIdentificacaoCronograma(bool cronogramaNovo, bool cronogramaImportado, int codigoUsuario, string CodigoCronogramaProjeto, bool publicarCronograma, tasques dsProjetoTasques)
    {
        DataRow rowProjeto = dsProjetoTasques.Projeto[0];

        short CodigoTipoCronograma = (short)rowProjeto["CodigoTipoCronograma"];
        string DescricaoTipoCronograma = rowProjeto["DescricaoTipoCronograma"].ToString() == "" ? "null" : "'" + rowProjeto["DescricaoTipoCronograma"].ToString().Replace("'", "''") + "'";
        string CodigoCronogramaReferencia = rowProjeto["CodigoCronogramaReferencia"].ToString() == "" ? "null" : "'" + rowProjeto["CodigoCronogramaReferencia"].ToString().Replace("'", "''") + "'";

        StringBuilder comandoSQL = new StringBuilder();
        // lê as informaçoes de identificação
        string NomeProjeto = rowProjeto["nomeProjeto"].ToString();
        string codigoProjeto = rowProjeto["codigoProjetoCorporativo"].ToString() != "" &&
                               rowProjeto["codigoProjetoCorporativo"].ToString() != "-1" ? rowProjeto["codigoProjetoCorporativo"].ToString() : "null";

        string DataCriacaoDesktop = ((DateTime)rowProjeto["dataCriacao"]).ToString("dd/MM/yyyy HH:mm:ss");
        string NomeAutor = rowProjeto["autor"].ToString();
        // string versaoTasquesOrigem = rowProjeto["versaoTasquesOrigem"].ToString();
        string versaoTasquesDestino = rowProjeto["versaoTasquesDestino"].ToString();
        string dataInicioProjeto = ((DateTime)rowProjeto["dataInicioProjeto"]).ToString("dd/MM/yyyy HH:mm:ss");
        string dataTerminoProjeto = ((DateTime)rowProjeto["dataTerminoProjeto"]).ToString("dd/MM/yyyy HH:mm:ss");
        string formatoDuracao = rowProjeto["formatoDuracao"].ToString();
        string formatoTrabalho = rowProjeto["formatoTrabalho"].ToString();
        string formataDataHora = rowProjeto["formataDataHora"].ToString();
        string moedaProjeto = "1";// rowProjeto["moedaProjeto"].ToString();

        string nomeEquipamento = rowProjeto["nomeEquipamento"].ToString();
        string sistemaOperacional = rowProjeto["sistemaOperacional"].ToString();
        string tipoVisaoCronograma = rowProjeto["tipoVisaoCronograma"].ToString();
        // short codigoCalendario = (short)rowProjeto["codigoCalendario"];
        string IndicaUsoPesoManual = (bool)rowProjeto["IndicaUsoPesoManual"] ? "S" : "N";


        // ----------------------------------------------------------------------------------------------------
        // As informações "minutosDia", "minutosSemana" e "diasMes" foram lidos no método "salvarXmlCronograma" 
        // e estão atribuídas as variáveis "@MinutosDia", "@MinutosSemana" e "@DiasMes".
        // ----------------------------------------------------------------------------------------------------

        // se for novo cronograma, deverá inserir um registro na tabela CronogramaProjeto
        if (cronogramaNovo)
        {
            string indicaCronogramaImportadoMSP = cronogramaImportado ? "S" : "N";

            comandoSQL.AppendFormat(
                @" 
                       INSERT INTO {0}.{1}.CronogramaProjeto
                            ( CodigoCronogramaProjeto, NomeProjeto, DataInclusaoServidor, CodigoUsuarioInclusao, 
                              NomeAutor, Dominio, versaoDesktop, SistemaOperacional, IdentificadorMaquina, 
                              InicioProjeto, DataCheckoutCronograma, CodigoUsuarioCheckoutCronograma, 
                              ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoCodigoMoedaProjeto, 
                              CodigoEntidade, CodigoProjeto,
                              DataUltimaGravacaoDesktop, CodigoCalendario, MinutosDia, MinutosSemana, DiasMes,
                              indicaCronogramaImportadoMSP, tipoVisaoCronograma, CodigoTipoCronograma, DescricaoTipoCronograma, 
                              CodigoCronogramaReferencia, CodigoCronogramaReplanejamento, CodigoCronogramaOrigem, IndicaUsoPesoManual )
                       VALUES 
                            (@CodigoCronogramaProjeto, '{2}', @DataAtual, @CodigoUsuario, 
                             '{3}', '{4}', '{5}', '{6}', '{7}', 
                             convert(datetime, '{8}', 103), @DataAtual, @CodigoUsuario, 
                             '{9}', '{10}', '{11}', {12}, 
                              @CodigoEntidade, {13}, 
                              @DataAtual, @CodigoCalendario, @MinutosDia, @MinutosSemana, @DiasMes,
                              '{14}', '{15}', {16}, {17}, {18}, @CodigoCronogramaProjeto, @CodigoCronogramaProjeto, '{19}' )

                       IF ({16}=2)
                          UPDATE {0}.{1}.CronogramaProjeto 
                             SET DataCheckoutCronograma = null
                               , CodigoUsuarioCheckoutCronograma = null
                               , DataUltimaPublicacao = @DataAtual
                               , CodigoUsuarioUltimaPublicacao = @CodigoUsuario
                          WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto

                    ", bancodb, Ownerdb,
                   NomeProjeto.Replace("'", "''"),
                   NomeAutor.Replace("'", "''"), "", versaoTasquesDestino.Replace("'", "''"), sistemaOperacional.Replace("'", "''"), nomeEquipamento.Replace("'", "''"),
                   dataInicioProjeto, // {8}
                   formatoDuracao.ToLower(), formatoTrabalho.ToLower(), formataDataHora.ToUpper(), moedaProjeto,
                   codigoProjeto,
                   indicaCronogramaImportadoMSP, tipoVisaoCronograma,
                   CodigoTipoCronograma, // {16}
                   DescricaoTipoCronograma, CodigoCronogramaReferencia, IndicaUsoPesoManual);
        }
        //caso contrário, atualiza as informações de identificação do cronograma
        else
        {
            string CronogramaImportadoMSP = "";

            if (cronogramaImportado)
            {
                comandoSQL.AppendFormat(
                   @"
                        -- retirando referência na [LancamentoFinanceiro] 
                        ---------------------------------------------------------------------------------------------------------
                        UPDATE {0}.{1}.LancamentoFinanceiro 
                           SET CodigoAtribuicao = NULL WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                                                                    FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                                                                   WHERE CodigoCronogramaProjeto= @CodigoCronogramaProjeto )
                        ---------------------------------------------------------------------------------------------------------
                   
                        DELETE FROM {0}.{1}.AtualizacaoDiariaTarefaTimeSheet WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                                                                                          FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                                                                                         WHERE CodigoCronogramaProjeto= @CodigoCronogramaProjeto )

                        DELETE FROM {0}.{1}.AtribuicaoDiariaRecurso WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.AtribuicaoRecursoTarefa WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.AtribuicaoRecursoTarefaLinhaBase WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.TarefaCronogramaProjetoLinhaBase WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.LinhaBaseCronograma WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.RecursoCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.TarefaCronogramaProjetoPredecessoras WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto

                        DELETE FROM {0}.{1}.AlertaTarefaDestinatario WHERE CodigoAlertaTarefa in (SELECT CodigoAlertaTarefa
                                                                                                    FROM {0}.{1}.AlertaTarefa 
                                                                                                   WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto)

                        DELETE FROM {0}.{1}.AlertaTarefa WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.TarefaCronogramaProjetoTipoTarefa WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                        DELETE FROM {0}.{1}.TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto

                        ", bancodb, Ownerdb);

                CronogramaImportadoMSP = ", indicaCronogramaImportadoMSP = 'S' ";
            }


            // ------------------------------------------------------------------------------------------------------------------------
            // Em 30/06/2014, a pedido do Geter, o controle da data da última publicação foi transferido para o método anterior a este.
            // ------------------------------------------------------------------------------------------------------------------------

            // Comando para registrar quem (e quando) salvou o cronograma pela última vez
            string UltimaAlteracao = string.Format(
                @"
                           , DataUltimaAlteracao = @DataAtual
                           , CodigoUsuarioUltimaAlteracao = @CodigoUsuario"
                );

            // se é para salvar anônimo, não deve atualizar a data e o código do usuário que fez a alteração/Publicação.
            if (segurancaWS.SystemAnonimo)
            {
                try
                {
                    // Só vai salvar anônimo se o codigo usuário que solicitou a ação for igual ao que está no parametro "TASQUES_CodigoUsuarioAcessoHistoricoArquivosEnviadosAoServidor"
                    int codigoUsuarioAcessoHistorico = 0;
                    string comandoSqlUSuario = string.Format(
                        @"select isnull((select valor from ParametroConfiguracaoSistema where CodigoEntidade = p.CodigoEntidade and Parametro = 'TASQUES_CodigoUsuarioAcessoHistoricoArquivosEnviadosAoServidor' ),-1) codigoUsuarioAcessoHistorico
                            from cronogramaProjeto cp inner join
                                 Projeto p on p.CodigoProjeto = cp.CodigoProjeto
                           where codigoCronogramaProjeto = '{0}'", CodigoCronogramaProjeto);

                    DataSet ds = classeDados.getDataSet(comandoSqlUSuario);
                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        codigoUsuarioAcessoHistorico = int.Parse(ds.Tables[0].Rows[0]["codigoUsuarioAcessoHistorico"].ToString());

                        if (codigoUsuarioAcessoHistorico == segurancaWS.SystemID)
                            UltimaAlteracao = "";
                    }
                }
                catch
                {

                }
            }

            comandoSQL.AppendFormat(
                @"UPDATE {0}.{1}.CronogramaProjeto
                         SET NomeProjeto = '{2}'
                           , InicioProjeto = convert(datetime, '{3}', 103)
                           , ConfiguracaoFormatoDuracao  = '{4}'
                           , ConfiguracaoFormatoTrabalho = '{5}'
                           , ConfiguracaoFormatoDataComHora = '{6}'
                           , ConfiguracaoCodigoMoedaProjeto = '{7}'
                           , DataUltimaGravacaoDesktop = @DataAtual
                           , RecalcularCronogramaAoAbrirTasques = 'N'
                           , versaoDesktop = '{8}'
                           , tipoVisaoCronograma = '{9}'
                           , codigoCalendario = @CodigoCalendario
                           , minutosDia = @MinutosDia
                           , minutosSemana = @MinutosSemana
                           , diasMes = @DiasMes
                           , DescricaoTipoCronograma = {10}
                           , IndicaUsoPesoManual = '{11}'
                           {12}
                           {13}
                       WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                     ", bancodb, Ownerdb,
                    NomeProjeto.Replace("'", "''"), dataInicioProjeto,
                    formatoDuracao.ToLower(), formatoTrabalho.ToLower(), formataDataHora.ToUpper(), moedaProjeto,
                    versaoTasquesDestino, tipoVisaoCronograma, // codigoCalendario, 
                    DescricaoTipoCronograma, IndicaUsoPesoManual,
                    CronogramaImportadoMSP,    // {12}
                    UltimaAlteracao);          // {13}
        }

        return comandoSQL.ToString();
    }

    private string comandoSQLTarefasCronograma(tasques dsProjetoTasques, bool cronogramaImportado, out bool existeLinhaBase)
    {
        existeLinhaBase = false;

        string registroSendoAtualizado = "";
        int posicaoAtualizacao = -1;
        try
        {
            StringBuilder comandoSQL = new StringBuilder();
            // bool versaoAntiga = dsProjetoTasques.Projeto[0]["versaoTasquesOrigem"].ToString() == "Portal da Estrategia - 1.0";

            string codigoUsuarioCorporativo = dsProjetoTasques.Projeto.Rows[0]["codigoUsuarioCorporativo"].ToString();

            foreach (DataRow rowTarefa in dsProjetoTasques.Tarefas)
            {
                posicaoAtualizacao = 0;
                int indice = (int)rowTarefa["indice"];
                string nomeTarefa = rowTarefa["nomeTarefa"].ToString();
                registroSendoAtualizado = string.Format("Linha-Tarefa:{0}-{1}", indice, nomeTarefa);
                bool tarefaValida = (bool)rowTarefa["tarefaValida"];

                // se a tarefa não é válida, envia apenas um comando para excluir logicamente o registro
                if (!tarefaValida)
                {
                    //                        comandoSQL += string.Format(
                    //                            @"UPDATE {0}.{1}.TarefaCronogramaProjeto 
                    //                             SET dataExclusao = @DataAtual
                    //                           WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                    //                             AND SequenciaTarefaCronograma = {2}
                    //
                    //                         ", bancodb, Ownerdb, indice);
                    continue;
                }

                posicaoAtualizacao = 1;
                int codigoTarefa = (int)rowTarefa["codigoTarefa"];
                string codigoTarefaSuperior = rowTarefa["codigoTarefaSuperior"] == DBNull.Value ? "null" : rowTarefa["codigoTarefaSuperior"].ToString();

                // em 02/03/2015, alterei para que o nível seja obtido a partir do "SELECT" do [nível da tarefa superior] + 1
                string nivel = rowTarefa["codigoTarefaSuperior"] == DBNull.Value ? rowTarefa["nivel"].ToString() : string.Format("(select nivel + 1 from TarefaCronogramaProjeto where codigoCronogramaProjeto = @CodigoCronogramaProjeto and codigoTarefa = {0})", codigoTarefaSuperior);
                //string nivel = rowTarefa["nivel"].ToString();

                string inicioTarefa = ((DateTime)rowTarefa["inicioTarefa"]).ToString("dd/MM/yyyy HH:mm");
                string terminoTarefa = ((DateTime)rowTarefa["terminoTarefa"]).ToString("dd/MM/yyyy HH:mm");
                string duracaoTarefa = rowTarefa["duracaoTarefa"].ToString();

                posicaoAtualizacao = 2;
                int duracaoEmMinutos = (int)rowTarefa["duracaoTarefaMinutos"];
                posicaoAtualizacao = 21;
                string formatoDuracaoTarefa = rowTarefa["formatoDuracaoTarefa"].ToString().ToLower();
                posicaoAtualizacao = 22;
                string dataRestricao = rowTarefa["dataRestricao"] == DBNull.Value ? "null" : string.Format("convert(datetime, '{0}', 103)", ((DateTime)rowTarefa["dataRestricao"]).ToString("dd/MM/yyyy HH:mm"));
                posicaoAtualizacao = 23;
                string codigoTipoRestricao = rowTarefa["codigoTipoRestricao"].ToString() == "" ? "0" : rowTarefa["codigoTipoRestricao"].ToString();
                posicaoAtualizacao = 24;
                string estruturaTopicoTarefa = rowTarefa["estruturaTopicoTarefa"].ToString();
                posicaoAtualizacao = 25;
                string trabalhoTarefa = rowTarefa["trabalhoTarefa"] == DBNull.Value ? "0" : ((decimal)rowTarefa["trabalhoTarefa"]).ToString().Replace(",", ".");
                posicaoAtualizacao = 26;
                string custoTarefa = rowTarefa["custoTarefa"] == DBNull.Value ? "0" : ((decimal)rowTarefa["custoTarefa"]).ToString().Replace(",", ".");
                posicaoAtualizacao = 27;
                string receita = rowTarefa["receita"] == DBNull.Value ? "null" : ((decimal)rowTarefa["receita"]).ToString().Replace(",", ".");

                posicaoAtualizacao = 3;
                short percentualConcluido = (short)rowTarefa["percentualConcluido"];
                posicaoAtualizacao = 31;
                string inicioReal = rowTarefa["inicioReal"] == DBNull.Value ? "null" : string.Format("convert(datetime, '{0}', 103)", ((DateTime)rowTarefa["inicioReal"]).ToString("dd/MM/yyyy HH:mm"));
                posicaoAtualizacao = 32;
                string terminoReal = rowTarefa["terminoReal"] == DBNull.Value ? "null" : string.Format("convert(datetime, '{0}', 103)", ((DateTime)rowTarefa["terminoReal"]).ToString("dd/MM/yyyy HH:mm"));
                posicaoAtualizacao = 33;
                string duracaoReal = rowTarefa["duracaoReal"].ToString();
                posicaoAtualizacao = 34;
                int duracaoRealEmMinutos = (int)rowTarefa["duracaoRealMinutos"];

                posicaoAtualizacao = 4;
                string trabalhoReal = ((decimal)rowTarefa["trabalhoReal"]).ToString().Replace(",", ".");
                posicaoAtualizacao = 41;
                string custoReal = ((decimal)rowTarefa["custoReal"]).ToString().Replace(",", ".");
                posicaoAtualizacao = 42;
                string receitaReal = rowTarefa["receitaReal"] == DBNull.Value ? "null" : ((decimal)rowTarefa["receitaReal"]).ToString().Replace(",", ".");
                posicaoAtualizacao = 43;
                string anotacao = rowTarefa["anotacao"].ToString();
                posicaoAtualizacao = 44;
                string CodigoTipoTarefaCronograma = rowTarefa["CodigoTipoTarefaCronograma"] == DBNull.Value || (short)rowTarefa["CodigoTipoTarefaCronograma"] <= 0 ? "null" : rowTarefa["CodigoTipoTarefaCronograma"].ToString();
                posicaoAtualizacao = 45;
                string edt = rowTarefa["edt"] == DBNull.Value || rowTarefa["edt"].ToString().Trim() == "" ? "null" : "'" + rowTarefa["edt"].ToString() + "'";

                posicaoAtualizacao = 5;
                bool tarefaResumo = (bool)rowTarefa["tarefaResumo"];
                bool tarefaMarco = (bool)rowTarefa["tarefaMarco"];
                bool tarefaCritica = (bool)rowTarefa["tarefaCritica"];
                bool tarefaExpandida = (bool)rowTarefa["tarefaExpandida"];
                bool tarefaVisivel = (bool)rowTarefa["tarefaVisivel"];

                // O Texto com a alocação do recurso não será salvo na tabela de tarefas. Este deverá ser construido a partir da tabela recursosCronogramaProjeto
                string textoAlocacaoRecurso = "";
                //string textoAlocacaoRecurso = rowTarefa["textoAlocacaoRecurso"].ToString();
                //textoAlocacaoRecurso = textoAlocacaoRecurso.Replace("'", "''");
                //if (textoAlocacaoRecurso.Length > 7900)
                //    textoAlocacaoRecurso = textoAlocacaoRecurso.Substring(0, 7900);

                posicaoAtualizacao = 6;
                // apenas para os cronogramas importados... verificar se possui linha de base
                string sqlLinhaBase = "";
                if (cronogramaImportado && rowTarefa["inicioLB"] != DBNull.Value)
                {
                    existeLinhaBase = true;
                    string inicioLB = string.Format("convert(datetime, '{0}', 103)", ((DateTime)rowTarefa["inicioLB"]).ToString("dd/MM/yyyy HH:mm"));
                    string terminoLB = string.Format("convert(datetime, '{0}', 103)", ((DateTime)rowTarefa["terminoLB"]).ToString("dd/MM/yyyy HH:mm"));
                    string duracaoLB = rowTarefa["duracaoLB"].ToString();
                    duracaoLB = duracaoLB.Substring(0, duracaoLB.IndexOf(' ')).Replace(".", "").Replace(',', '.');
                    int duracaoLBEmMinutos = (int)rowTarefa["duracaoLBMinutos"];
                    string trabalhoLB = ((decimal)rowTarefa["trabalhoLB"]).ToString().Replace(",", ".");
                    string custoLB = ((decimal)rowTarefa["custoLB"]).ToString().Replace(",", ".");
                    sqlLinhaBase = string.Format(
                        @" , InicioLB = {0}
                               , TerminoLB = {1}
                               , DuracaoLB = {2}
                               , DuracaoLBEmMinutos = {3}
                               , TrabalhoLB = {4}
                               , CustoLB = {5}  ", inicioLB, terminoLB, duracaoLB, duracaoLBEmMinutos, trabalhoLB, custoLB);
                }

                // ATENÇÃO: A coluna "PREDECESSORAS" precisa continuar sendo atualizada na tabela tarefaCronogramaProjeto
                //          O "FOFO" utiliza esta coluna para gerar LOG no projeto da SESCOP.
                string indiceTarefasPredecessoras = rowTarefa["indiceTarefasPredecessoras"] == DBNull.Value ? "" : rowTarefa["indiceTarefasPredecessoras"].ToString();
                // ===================================================================================================================================================

                posicaoAtualizacao = 7;
                // temos que retirar a unidade da variável "duracaoTarefa"
                duracaoTarefa = duracaoTarefa == string.Empty ? "0" : duracaoTarefa.Substring(0, duracaoTarefa.IndexOf(' ')).Replace(".", "").Replace(',', '.');
                duracaoReal = duracaoReal == string.Empty ? "0" : duracaoReal.Substring(0, duracaoReal.IndexOf(' ')).Replace(".", "").Replace(',', '.');

                // As tarefas Resumo não podem ter a data de restrição
                if (tarefaResumo)
                {
                    dataRestricao = "null";
                    codigoTipoRestricao = "0";
                }

                posicaoAtualizacao = 8;
                // tarefa vinculada - Externa
                bool IndicaTarefaComVinculoExterno = (bool)rowTarefa["IndicaTarefaComVinculoExterno"];
                string CodigoCronogramaVinculoExterno = "null";
                string CodigoTarefaVinculoExterno = "null";
                if (IndicaTarefaComVinculoExterno)
                {
                    CodigoCronogramaVinculoExterno = "'" + rowTarefa["CodigoCronogramaVinculoExterno"] + "'";
                    CodigoTarefaVinculoExterno = rowTarefa["CodigoTarefaVinculoExterno"] + "";
                }

                posicaoAtualizacao = 81;
                string ValorPesoTarefa = rowTarefa["ValorPesoTarefa"] == DBNull.Value ? "0" : ((decimal)rowTarefa["ValorPesoTarefa"]).ToString().Replace(",", ".");
                string ValorPesoTarefaLB = rowTarefa["ValorPesoTarefaLB"] == DBNull.Value ? "null" : ((decimal)rowTarefa["ValorPesoTarefaLB"]).ToString().Replace(",", ".");
                string PercentualPesoTarefa = rowTarefa["PercentualPesoTarefa"] == DBNull.Value ? "0" : ((decimal)rowTarefa["PercentualPesoTarefa"]).ToString().Replace(",", ".");
                string IndicaAtribuicaoManualPesoTarefa = (bool)rowTarefa["IndicaAtribuicaoManualPesoTarefa"] ? "S" : "N";

                posicaoAtualizacao = 9;
                // apenas para tarefas válidas
                comandoSQL.AppendFormat(
                    @"  -----------------------------------------------------------------------------------------------------------------------------
                        if ( not exists (Select 1 from {0}.{1}.TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto and CodigoTarefa = {2} and dataExclusao is null))
                            INSERT INTO {0}.{1}.TarefaCronogramaProjeto 
                                   ( CodigoCronogramaProjeto, CodigoTarefa, SequenciaTarefaCronograma, NomeTarefa, CodigoTarefaSuperior, nivel, 
                                     Inicio, Termino, Duracao, FormatoDuracao, DuracaoEmMinutos, Trabalho, Custo, DataInclusao, IDTarefa )
                            VALUES ( @CodigoCronogramaProjeto, {2}, {3}, '{4}', {5}, 1,
                                     convert(datetime, '{7}', 103), convert(datetime, '{8}', 103), {9}, '{10}', {11}, 0, 0, @DataAtual, NEWID()) 

                        UPDATE {0}.{1}.TarefaCronogramaProjeto
                           SET SequenciaTarefaCronograma = {3}
                             , DataUltimaAlteracao = @DataAtual
                             , NomeTarefa = '{4}'
                             , CodigoTarefaSuperior = {5}
                             , nivel = {6}
                             , Inicio = convert(datetime, '{7}', 103)
                             , Termino = convert(datetime, '{8}', 103)
                             , Duracao = {9}
                             , FormatoDuracao = '{10}'
                             , DuracaoEmMinutos = {11}
                             , DataRestricao = {12}
                             , tipoRestricao = {13}
                             , Trabalho = {14}
                             , Custo = {15}
                             , PercentualFisicoConcluido = {16}
                             , InicioReal = {17}
                             , TerminoReal = {18}
                             , DuracaoReal = {19}
                             , DuracaoRealEmMinutos = {20}
                             , TrabalhoReal = {21}
                             , CustoReal = {22}
                             , IndicaTarefaResumo = '{23}'
                             , IndicaMarco = '{24}'
                             , IndicaTarefaCritica = '{25}'
                             , indicaTarefaVisivel= '{26}'
                             , indicaTarefaExpandida= '{27}'
                             , indicaTarefaValida = '{28}'
                             , EstruturaHierarquica = '{29}'
                             , anotacoes = '{30}'
                             , Predecessoras = '{31}'
                             , CodigoTipoTarefaCronograma = {32}
                             , EDT = {33}
                             , StringAlocacaoRecursoTarefa = '{34}'
                             , receita = {35}
                             , receitaReal = {36}
                             , IndicaTarefaComVinculoExterno = '{37}'
                             , CodigoCronogramaVinculoExterno = {38}
                             , CodigoTarefaVinculoExterno = {39}
                             , dataExclusao = (select dataExclusao from TarefaCronogramaProjeto where codigoCronogramaProjeto = @CodigoCronogramaProjeto and codigoTarefa = {5} )
                             , ValorPesoTarefa = {40}
                             , ValorPesoTarefaLB = {41}
                             , PercentualPesoTarefa = {42}
                             , IndicaAtribuicaoManualPesoTarefa = '{43}'
                             {44} 
                         WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                           AND CodigoTarefa = {2}
                           AND DataExclusao is null

                    ", bancodb, Ownerdb, codigoTarefa, indice, nomeTarefa.Replace("'", "''"), codigoTarefaSuperior, nivel, //0...6
                     inicioTarefa, terminoTarefa, duracaoTarefa, formatoDuracaoTarefa.ToLower(), duracaoEmMinutos, // 7...11
                     dataRestricao, codigoTipoRestricao, // 12, 13
                     trabalhoTarefa, custoTarefa, percentualConcluido, inicioReal, terminoReal, duracaoReal, duracaoRealEmMinutos, trabalhoReal, custoReal, // 14...22
                     tarefaResumo ? "S" : "N", tarefaMarco ? "S" : "N", tarefaCritica ? "S" : "N", tarefaVisivel ? "S" : "N", tarefaExpandida ? "S" : "N", tarefaValida ? "S" : "N",
                     estruturaTopicoTarefa, anotacao.Replace("'", "''"),
                     indiceTarefasPredecessoras, CodigoTipoTarefaCronograma, edt, textoAlocacaoRecurso, receita, receitaReal,
                     IndicaTarefaComVinculoExterno ? "S" : "N", CodigoCronogramaVinculoExterno, CodigoTarefaVinculoExterno, // {39}
                     ValorPesoTarefa, ValorPesoTarefaLB, PercentualPesoTarefa, IndicaAtribuicaoManualPesoTarefa, // {43}
                     sqlLinhaBase);

                /* 
                 *     -- , Predecessoras = '{12}'
                         --, MenorDataTarefa = {16}
                         --, EstruturaHierarquica = '{19}'
                         --, StringAlocacaoRecursoTarefa = {20}
                        -- , indicesTarefasDependentes = {21}

                         --, InicioLB = {23}
                         --, TerminoLB = {24}
                         --, DuracaoLB = {25}
                         --, TrabalhoLB = {26}
                         --, DuracaoAcumuladaEmMinutos = {35}
                 */

                //Obtem a informação de percentual concluido alterado
                string PercentualConcluidoAlterado = rowTarefa["PercentualConcluidoAlterado"] == DBNull.Value ? "N" : rowTarefa["PercentualConcluidoAlterado"].ToString();

                //Se houver alteração de percentual concluido monta o SQL de update para cancelamento do histórico de atribuições
                if (PercentualConcluidoAlterado == "S") {
                    comandoSQL.AppendFormat(@"UPDATE {0}.{1}.ApontamentoAtribuicao
	                                            SET StatusAnalise = 'CN', 
			                                        DataStatusAnalise = @DataAtual, 
			                                        CodigoUsuarioStatusAnalise = {3},
			                                        IndicaCancelamentoAutomatico = 'S'
                                                WHERE StatusAnalise NOT IN('CN','RP') 
	                                            AND CodigoCronogramaProjeto IN (SELECT cpi.CodigoCronogramaProjeto FROM {0}.{1}.CronogramaProjeto cp INNER JOIN {0}.{1}.CronogramaProjeto cpi ON cpi.CodigoCronogramaOrigem = cp.CodigoCronogramaOrigem WHERE cp.CodigoCronogramaProjeto = @CodigoCronogramaProjeto)
                                                AND CodigoTarefa = {2}
                                             ", bancodb, Ownerdb, codigoTarefa, codigoUsuarioCorporativo);
                }
            }

            return comandoSQL.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(registroSendoAtualizado + "\nPosição:" + posicaoAtualizacao + "\n" + ex.Message);
        }
    }

    private string comandoSQLRecursosCronograma(tasques dsProjetoTasques)
    {
        string registroSendoAtualizado = "";
        try
        {
            StringBuilder comandoSQL = new StringBuilder();
            foreach (DataRow rowRecurso in dsProjetoTasques.Recursos)
            {
                string codigoRecurso = rowRecurso["codigoRecurso"].ToString();
                string nomeRecurso = rowRecurso["nomeRecurso"].ToString();
                registroSendoAtualizado = string.Format("{0}-{1}", codigoRecurso, nomeRecurso);

                string codigoRecursoCorporativo = rowRecurso["codigoRecursoCorporativo"] == DBNull.Value ? "-1" : rowRecurso["codigoRecursoCorporativo"].ToString();
                string iniciais = rowRecurso["iniciais"] == DBNull.Value ? "null" : "'" + rowRecurso["iniciais"].ToString().Replace("'", "''") + "'";
                string custoHora = ((decimal)rowRecurso["custoHora"]).ToString().Replace(',', '.');
                string custoUso = ((decimal)rowRecurso["custoUso"]).ToString().Replace(',', '.');
                string custoHoraExtra = ((decimal)rowRecurso["custoHoraExtra"]).ToString().Replace(',', '.');
                string codigoTipoRecurso = rowRecurso["codigoTipoRecurso"].ToString();

                string codigoGrupoRecurso = rowRecurso["codigoGrupoRecurso"] == DBNull.Value ? "null" : rowRecurso["codigoGrupoRecurso"].ToString();
                string nomeGrupoRecurso = "null";
                // busca o grupo do recurso
                if (rowRecurso["codigoGrupoRecurso"] != DBNull.Value)
                    nomeGrupoRecurso = "'" + dsProjetoTasques.GrupoRecurso.Select("codigoGrupoRecurso = " + codigoGrupoRecurso)[0]["nomeGrupoRecurso"].ToString().Replace("'", "''") + "'";

                string codigoUnidadeMedidaRecurso = rowRecurso["codigoUnidadeMedidaRecurso"] == DBNull.Value ? "null" : rowRecurso["codigoUnidadeMedidaRecurso"].ToString();
                string nomeUnidadeMedida = "null";
                // busca o nome da unidade de medida
                if (rowRecurso["codigoUnidadeMedidaRecurso"] != DBNull.Value)
                    nomeUnidadeMedida = "'" + dsProjetoTasques.UnidadeMedidaRecurso.Select("codigoUnidadeMedida = " + codigoUnidadeMedidaRecurso)[0]["nomeUnidadeMedida"].ToString().Replace("'", "''") + "'";

                string codigoCalendarioBase = rowRecurso["codigoCalendarioBase"].ToString();
                string anotacao = rowRecurso["anotacao"].ToString();

                comandoSQL.AppendFormat(
                    @"
                        -----------------------------------------------------------------------------------------------------------------------------
                        if ( not exists (Select 1 from {0}.{1}.RecursoCronogramaProjeto WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto and CodigoRecursoProjeto = {2}))
                            INSERT INTO {0}.{1}.RecursoCronogramaProjeto 
                                (CodigoCronogramaProjeto, CodigoRecursoProjeto, NomeRecurso, CodigoTipoRecurso, DataInclusao)
                            VALUES 
                                (@CodigoCronogramaProjeto, {2}, '{3}', {4}, @DataAtual ) 

                        UPDATE {0}.{1}.RecursoCronogramaProjeto
                           SET NomeRecurso = '{3}'
                             , CodigoTipoRecurso = {4}
                             , CodigoRecursoCorporativo = {5}
                             , CustoHora = {6}
                             , CustoUso = {7}
                             , CustoHoraExtra = {8}
                             , CodigoGrupoRecurso = {9}
                             , NomeGrupoRecurso = {10}
                             , iniciaisRecurso = {11}
                             , UnidadeMedidaRecurso = {12}
                             , DataUltimaAlteracao = @DataAtual
                             , Anotacoes = '{13}'
                         WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                           AND CodigoRecursoProjeto = {2}

                    ", bancodb, Ownerdb, codigoRecurso, nomeRecurso.Replace("'", "''"), codigoTipoRecurso,
                    codigoRecursoCorporativo == "-1" ? "null" : codigoRecursoCorporativo.ToString(), // 5
                    custoHora, custoUso, custoHoraExtra, // 6, 7, 8
                    codigoGrupoRecurso, nomeGrupoRecurso, iniciais,
                    nomeUnidadeMedida, anotacao.Replace("'", "''"));
            }
            return comandoSQL.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(registroSendoAtualizado + "\n" + ex.Message);
        }
    }

    private string comandoSQLAtribuicaoRecursosCronograma(tasques dsProjetoTasques)
    {
        string registroSendoAtualizado = "";
        try
        {
            StringBuilder comandoSQL = new StringBuilder();
            foreach (DataRow rowAtribuicao in dsProjetoTasques.Atribuicoes)
            {
                string codigoRecurso = rowAtribuicao["codigoRecurso"].ToString();
                string codigoTarefa = rowAtribuicao["codigoTarefa"].ToString();
                registroSendoAtualizado = string.Format("R{0}-T{1}", codigoRecurso, codigoTarefa);

                // informações da tarefa
                DataRow[] rowsTarefas = dsProjetoTasques.Tarefas.Select("CodigoTarefa = " + rowAtribuicao["codigoTarefa"]);
                // se não achou a tarefa, pega a próxima atribuição
                if (rowsTarefas.Length == 0)
                    continue;

                // achou a tarefa
                DataRow rowTarefa = rowsTarefas[0];

                // se é uma tarefa resumo e não possui registro de atribuição na tarefa, pega a próxima
                if ((bool)rowTarefa["tarefaResumo"] && rowTarefa["textoAlocacaoRecurso"].ToString().Trim() == "")
                    continue;

                bool recursoCorporativo = (bool)dsProjetoTasques.Recursos.Select("codigoRecurso = " + codigoRecurso)[0]["recursoCorporativo"];

                string qtdeUnidadesAtribuida = rowAtribuicao["qtdeUnidadesAtribuida"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["qtdeUnidadesAtribuida"]).ToString().Replace(',', '.');
                string trabalhoAtribuicao = rowAtribuicao["trabalhoAtribuicao"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["trabalhoAtribuicao"]).ToString().Replace(',', '.');
                string custoNormalAtribuicao = rowAtribuicao["custoNormalAtribuicao"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["custoNormalAtribuicao"]).ToString().Replace(',', '.');
                string custoUsoAtribuicao = rowAtribuicao["custoUsoAtribuicao"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["custoUsoAtribuicao"]).ToString().Replace(',', '.');
                string custoExtraAtribuicao = rowAtribuicao["custoExtraAtribuicao"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["custoExtraAtribuicao"]).ToString().Replace(',', '.');

                // processa informações da tarefa
                string inicioTarefa = ((DateTime)rowTarefa["inicioTarefa"]).ToString("dd/MM/yyyy HH:mm");
                string terminoTarefa = ((DateTime)rowTarefa["terminoTarefa"]).ToString("dd/MM/yyyy HH:mm");

                string inicioTarefaReal = rowTarefa["inicioReal"] == DBNull.Value ? "null" : string.Format("convert(datetime, '{0}', 103)", ((DateTime)rowTarefa["inicioReal"]).ToString("dd/MM/yyyy HH:mm"));
                string terminoTarefaReal = rowTarefa["terminoReal"] == DBNull.Value ? "null" : string.Format("convert(datetime, '{0}', 103)", ((DateTime)rowTarefa["terminoReal"]).ToString("dd/MM/yyyy HH:mm"));
                string custoReal = rowAtribuicao["custoReal"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["custoReal"]).ToString().Replace(',', '.');
                string trabalhoReal = rowAtribuicao["trabalhoReal"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["trabalhoReal"]).ToString().Replace(',', '.');
                string percentualConcluido = rowAtribuicao["percentualConcluido"] == DBNull.Value ? "0" : rowAtribuicao["percentualConcluido"].ToString();
                string CustoHoraRecursoAtribuido = rowAtribuicao["custoHoraRecurso"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["custoHoraRecurso"]).ToString().Replace(',', '.');

                //Implementação outros custos taskboard
                string qtdeUnidadesAtribuidaReal = rowAtribuicao["qtdeUnidadesAtribuidaReal"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["qtdeUnidadesAtribuidaReal"]).ToString().Replace(',', '.');
                string custoUnitarioReal = rowAtribuicao["custoUnitarioReal"] == DBNull.Value ? "0" : ((decimal)rowAtribuicao["custoUnitarioReal"]).ToString().Replace(',', '.');

                comandoSQL.AppendFormat(
                    @"
                        -----------------------------------------------------------------------------------------------------------------------------
                        if ( not exists (Select 1 from {0}.{1}.AtribuicaoRecursoTarefa WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto and CodigoRecursoProjeto = {2} and CodigoTarefa = {3} ))
                            INSERT INTO {0}.{1}.AtribuicaoRecursoTarefa 
                                (CodigoCronogramaProjeto, CodigoRecursoProjeto, CodigoTarefa)
                            VALUES ( @CodigoCronogramaProjeto, {2}, {3} ) 
                        
                        UPDATE {0}.{1}.AtribuicaoRecursoTarefa
                           SET Inicio = convert(datetime, '{4}', 103)
                             , Termino = convert(datetime, '{5}', 103)
                             , UnidadeAtribuicao = {6}
                             , Custo = {7} 
                             , custoUso = {8}
                             , custoExtra = {9}
                             , Trabalho = {10}
                             , IndicaAtribuicaoRecursoCorporativo = '{11}'
                             , InicioReal = {12}
                             , TerminoReal = {13}
                             , CustoReal = {14}
                             , TrabalhoReal = {15}
                             , DataUltimaAlteracao = @DataAtual
                             , percentualFisicoConcluido = {16}
                             , CustoHoraRecursoAtribuido = {17}
                             , UnidadeAtribuicaoReal = {18}
                             , CustoUnitarioReal = {19} 
                         WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                           AND CodigoRecursoProjeto = {2}
                           AND CodigoTarefa = {3}

                    ", bancodb, Ownerdb, codigoRecurso, codigoTarefa,
                   inicioTarefa, terminoTarefa,
                   qtdeUnidadesAtribuida, custoNormalAtribuicao, custoUsoAtribuicao, custoExtraAtribuicao,
                   trabalhoAtribuicao, recursoCorporativo ? "S" : "N",
                   inicioTarefaReal, terminoTarefaReal,
                   custoReal, trabalhoReal, percentualConcluido, CustoHoraRecursoAtribuido, qtdeUnidadesAtribuidaReal, custoUnitarioReal);
            }

            return comandoSQL.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(registroSendoAtualizado + "\n" + ex.Message);
        }
    }

    private string comandoSQLPredecessoras(tasques dsProjetoTasques)
    {
        string registroSendoAtualizado = "";
        try
        {
            StringBuilder comandoSQL = new StringBuilder();
            foreach (DataRow rowPredecessora in dsProjetoTasques.Predecessoras)
            {
                string codigoTarefa = rowPredecessora["codigoTarefa"].ToString();
                string codigoTarefaPredecessora = rowPredecessora["codigoTarefaPredecessora"].ToString();
                string indiceTarefaPredecessora = rowPredecessora["indiceTarefaPredecessora"].ToString();

                registroSendoAtualizado = string.Format("{0}-{1};{2}", codigoTarefa, codigoTarefaPredecessora, indiceTarefaPredecessora);

                string tipoLatencia = rowPredecessora["tipoLatencia"].ToString();
                string latencia = rowPredecessora["latencia"].ToString();
                string formatoDuracaoLatencia = rowPredecessora["formatoDuracaoLatencia"].ToString();

                comandoSQL.AppendFormat(
                    @"
                        -----------------------------------------------------------------------------------------------------------------------------
                        if ( not exists (Select 1 from {0}.{1}.TarefaCronogramaProjetoPredecessoras WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto and CodigoTarefa = {2} and codigoTarefaPredecessora = {3}))
                            INSERT INTO {0}.{1}.TarefaCronogramaProjetoPredecessoras 
                                (CodigoCronogramaProjeto, codigoTarefa, codigoTarefaPredecessora, indiceTarefaPredecessora)
                            VALUES ( @CodigoCronogramaProjeto, {2}, {3}, {4} ) 
                        
                        UPDATE {0}.{1}.TarefaCronogramaProjetoPredecessoras
                           SET indiceTarefaPredecessora = {4}
                             , tipoLatencia = '{5}'
                             , latencia = '{6}'
                             , formatoDuracaoLatencia = '{7}'
                             , DataUltimaAlteracao = @DataAtual
                         WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto
                           AND CodigoTarefa = {2}
                           AND codigoTarefaPredecessora = {3}

                    ", bancodb, Ownerdb, codigoTarefa, codigoTarefaPredecessora, indiceTarefaPredecessora,
                     tipoLatencia, latencia, formatoDuracaoLatencia);
            }
            return comandoSQL.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(registroSendoAtualizado + "\n" + ex.Message);
        }
    }

    private string comandoSQLAssociaTipoTarefaCronograma(tasques dsProjetoTasques)
    {
        string registroSendoAtualizado = "";
        try
        {
            // Antes de incluirmos, vamos excluir todos os registros que já estão associados
            StringBuilder comandoSQL = new StringBuilder(@"
                        -----------------------------------------------------------------------------------------------------------------------------
                        DELETE TarefaCronogramaProjetoTipoTarefa WHERE CodigoCronogramaProjeto = @CodigoCronogramaProjeto

                    ");

            // monta o comando de insert para cada associação
            foreach (DataRow rowTipoTarefaAssociada in dsProjetoTasques.TarefaCronogramaProjetoTipoTarefa)
            {
                string codigoTarefa = rowTipoTarefaAssociada["codigoTarefa"].ToString();
                string codigoTipoTarefa = rowTipoTarefaAssociada["codigoTipoTarefaCronograma"].ToString();

                registroSendoAtualizado = string.Format("{0}-{1}", codigoTarefa, codigoTipoTarefa);

                comandoSQL.AppendFormat(
                    @"INSERT INTO TarefaCronogramaProjetoTipoTarefa (CodigoCronogramaProjeto, codigoTarefa, CodigoTipoTarefaCronograma) VALUES ( @CodigoCronogramaProjeto, {0}, {1} )

                        ", codigoTarefa, codigoTipoTarefa);
            }
            return comandoSQL.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(registroSendoAtualizado + "\n" + ex.Message);
        }
    }

    #region metodos antigos

    //        private bool persisteIdentificacaoCronograma(int codigoUsuario, string CodigoCronogramaProjeto, bool publicarCronograma, XmlDocument XmlCronograma)
    //        {
    //            string comandoSQL = "";
    //            int regAfetados = 0;
    //            try
    //            {
    //                XmlNodeList xmlTasques = XmlCronograma.GetElementsByTagName("Projeto", "http://www.tasques.com.br/tasques.xsd");
    //                XmlNode xmlInfoProjeto = xmlTasques[0];// XmlCronograma.SelectSingleNode("Projeto");//, "http://www.tasques.com.br/tasques");


    //                // verifica se o cronograma já existe no banco de dados
    //                bool novoCronograma = !getCronogramaJaExiste(CodigoCronogramaProjeto);

    //                //// se o cronograma foi importado do MSProject sobreescrevendo um cronograma existente, tem que apagar todas as informações atuais
    //                //if (!novoCronograma && cronogramaImportadoMSProject)
    //                //                {
    //                //                    comandoSQL = string.Format(
    //                //                        @"xDELETE FROM {0}.{1}.AtualizacaoDiariaTarefaTimeSheet WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
    //                //                                                                                                          FROM {0}.{1}.AtribuicaoRecursoTarefa 
    //                //                                                                                                         WHERE CodigoCronogramaProjeto='{2}' )
    //                //
    //                //                        xDELETE FROM {0}.{1}.AtribuicaoDiariaRecurso WHERE CodigoCronogramaProjeto = '{2}'
    //                //                        xDELETE FROM {0}.{1}.AtribuicaoRecursoTarefa WHERE CodigoCronogramaProjeto = '{2}'
    //                //                        xDELETE FROM {0}.{1}.AtribuicaoRecursoTarefaLinhaBase WHERE CodigoCronogramaProjeto = '{2}'
    //                //                        xDELETE FROM {0}.{1}.TarefaCronogramaProjetoLinhaBase WHERE CodigoCronogramaProjeto = '{2}'
    //                //                        xDELETE FROM {0}.{1}.LinhaBaseCronograma WHERE CodigoCronogramaProjeto = '{2}'
    //                //                        xDELETE FROM {0}.{1}.RecursoCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}'
    //                //                        xDELETE FROM {0}.{1}.TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}'
    //                //                        xDELETE FROM {0}.{1}.CronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}'
    //                //
    //                //                    ", bancodb, Ownerdb,
    //                //                           codigoProjeto);
    //                //                    classeDados.execSQL(comandoSQL, ref regAfetados);

    //                //                    // passou a ser um novo cronograma
    //                //                    novoCronograma = true;
    //                //                }

    //                // lê as informaçoes de identificação
    //                string NomeProjeto = xmlInfoProjeto["nomeProjeto"].InnerText;
    //                string DataCriacaoDesktop = xmlInfoProjeto["dataCriacao"].InnerText;
    //                string NomeAutor = xmlInfoProjeto["autor"].InnerText;
    //                string versaoTasquesOrigem = xmlInfoProjeto["versaoTasquesOrigem"].InnerText;
    //                string versaoTasquesDestino = xmlInfoProjeto["versaoTasquesDestino"].InnerText;
    //                string dataInicioProjeto = xmlInfoProjeto["dataInicioProjeto"].InnerText;
    //                string fimProjeto = xmlInfoProjeto["termino"].InnerText;
    //                string versaoDesktop = xmlInfoProjeto["versao"].InnerText;
    //                string SistemaOperacional = xmlInfoProjeto["so"].InnerText;
    //                string IdentificadorMaquina = xmlInfoProjeto["idpc"].InnerText;
    //                string ConfiguracaoFormatoDuracao = xmlInfoProjeto["formatoDuracao"].InnerText[0].ToString();
    //                string ConfiguracaoFormatoTrabalho = xmlInfoProjeto["formatoTrabalho"].InnerText[0].ToString();
    //                string ConfiguracaoFormatoDataComHora = xmlInfoProjeto["mostrarHora"].InnerText;
    //                string ConfiguracaoCodigoMoedaProjeto = xmlInfoProjeto["codigoMoeda"].InnerText;
    //                string CodigoEntidade = xmlInfoProjeto["entidade"].InnerText;
    //                string CodigoProjeto = xmlInfoProjeto["codigoProjetoEntidade"].InnerText;
    //                CodigoProjeto = CodigoProjeto == "" || CodigoProjeto == "-1" ? "null" : CodigoProjeto;

    //                // se for novo cronograma, deverá inserir um registro na tabela CronogramaProjeto
    //                if (novoCronograma)
    //                {
    //                    comandoSQL = string.Format(
    //                        @" DECLARE @CodigoCalendario int
    //                       DECLARE @CodigoUsuario int
    //                       DECLARE @NomeUsuario varchar(100)
    //                       DECLARE @MinutosDia int
    //                       DECLARE @MinutosSemana int
    //                       DECLARE @DiasMes int
    //                       DECLARE @CodigoEntidade int
    //
    //                       SET @CodigoUsuario = {10}
    //                       SET @CodigoEntidade = {15}
    //
    //                       SET @MinutosDia = 480
    //                       SET @MinutosSemana = 2400
    //                       SET @DiasMes = 20
    //
    //                       SELECT @NomeUsuario = (Select nomeUsuario from {0}.{1}.Usuario WHERE codigoUsuario = @CodigoUsuario)
    //
    //                       SELECT @CodigoCalendario = c.[CodigoCalendario] 
    //                         FROM Calendario AS c	INNER JOIN 
    //                              AssociacaoCalendario	AS ac ON (ac.[CodigoCalendario]			= c.[CodigoCalendario]) INNER JOIN 
    //                              TipoAssociacao AS ta	ON (ta.[CodigoTipoAssociacao]	= ac.[CodigoTipoAssociacao])
    //                        WHERE c.[CodigoEntidade] = @CodigoEntidade
    //                          AND ta.[IniciaisTipoAssociacao]	= 'EN'
    //                          AND ac.[CodigoObjetoAssociado]	= @CodigoEntidade  
    //
    //                       INSERT INTO {0}.{1}.CronogramaProjeto
    //                            ( CodigoCronogramaProjeto, NomeProjeto, DataInclusaoServidor, CodigoUsuarioInclusao, NomeAutor, Dominio, versaoDesktop, SistemaOperacional, IdentificadorMaquina, 
    //                              InicioProjeto, DataCheckoutCronograma, CodigoUsuarioCheckoutCronograma, 
    //                              ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoCodigoMoedaProjeto, CodigoEntidade, CodigoProjeto,
    //                              DataUltimaPublicacao, CodigoUsuarioUltimaPublicacao, DataUltimaGravacaoDesktop, CodigoCalendario, MinutosDia, MinutosSemana, DiasMes )
    //                       VALUES 
    //                            ('{2}', '{3}', GetDate(), @CodigoUsuario, @NomeUsuario, '{5}', '{6}', '{7}', '{8}',
    //                             convert(datetime, '{9}', 103), GetDate(), {10}, 
    //                             '{11}', '{12}', '{13}', {14}, {15}, {16}, 
    //                              {17}, {18}, GetDate(), @CodigoCalendario, @MinutosDia, @MinutosSemana, @DiasMes  )
    //                    ", bancodb, Ownerdb,
    //                           CodigoCronogramaProjeto, NomeProjeto, "", "", versaoDesktop, SistemaOperacional, IdentificadorMaquina,
    //                           dataInicioProjeto, codigoUsuario,
    //                           ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoCodigoMoedaProjeto, CodigoEntidade, CodigoProjeto,
    //                           publicarCronograma ? "GetDate()" : "null", publicarCronograma ? codigoUsuario.ToString() : "null");
    //                }
    //                //caso contrário, atualiza as informações de identificação do cronograma
    //                else
    //                {
    //                    string UltimaPublicacao;
    //                    if (publicarCronograma)
    //                    {
    //                        UltimaPublicacao = string.Format(
    //                            @", DataUltimaPublicacao = GetDate()
    //                          , CodigoUsuarioUltimaPublicacao = {0}
    //                          , IndicaPublicacaoPendente = 'N' ", codigoUsuario);
    //                    }
    //                    else
    //                        UltimaPublicacao = ", IndicaPublicacaoPendente = 'S' ";

    //                    comandoSQL = string.Format(
    //                        @"UPDATE {0}.{1}.CronogramaProjeto
    //                         SET NomeProjeto = '{3}'
    //                           , InicioProjeto = convert(datetime, '{4}', 103)
    //                           , ConfiguracaoFormatoDuracao  = '{5}'
    //                           , ConfiguracaoFormatoTrabalho = '{6}'
    //                           , ConfiguracaoFormatoDataComHora = '{7}'
    //                           , ConfiguracaoCodigoMoedaProjeto = '{8}'
    //                           , DataUltimaAlteracao = GetDate()
    //                           , CodigoUsuarioUltimaAlteracao = {9}
    //                           , DataUltimaGravacaoDesktop = GetDate()
    //                           {10}
    //                       WHERE CodigoCronogramaProjeto = '{2}'
    //                     ", bancodb, Ownerdb,
    //                           CodigoCronogramaProjeto, NomeProjeto, dataInicioProjeto,
    //                           ConfiguracaoFormatoDuracao, ConfiguracaoFormatoTrabalho, ConfiguracaoFormatoDataComHora, ConfiguracaoCodigoMoedaProjeto,
    //                           codigoUsuario, UltimaPublicacao);
    //                }

    //                classeDados.execSQL(comandoSQL, ref regAfetados);
    //                return true;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new Exception(ex.Message + "\n" + comandoSQL);
    //            }
    //        }

    //        private bool persisteTarefasCronograma(int codigoUsuario, string codigoProjeto, XmlDocument XmlCronograma)
    //        {
    //            // recuperar as tarefas
    //            XmlNodeList xmlTarefas = XmlCronograma.SelectNodes("Projeto/Tarefas");

    //            if (xmlTarefas.Count > 0)
    //            {
    //                string comandoSQL = "";
    //                int afetatos = 0;
    //                int qtdTarefasInseridasComandoSQL = 0;
    //                string Tarefa = "";
    //                string Linha = "";
    //                // variavel para orientar, em caso de erro, se ocorreu durante a montagem do comando ou durante a execução
    //                bool FaseExecutarComando = false;
    //                try
    //                {
    //                    foreach (XmlNode nodeTarefa in xmlTarefas[0].ChildNodes)
    //                    {
    //                        FaseExecutarComando = false;
    //                        string indicaTarefaValida = nodeTarefa.Attributes["tarefaValida"].Value;

    //                        string CodigoTarefa = nodeTarefa.Attributes["codigo"].Value;
    //                        Linha = nodeTarefa.Attributes["indice"].Value;
    //                        Tarefa = nodeTarefa.Attributes["nome"] == null ? "" : nodeTarefa.Attributes["nome"].Value;
    //                        string Marco = nodeTarefa.Attributes["marco"].Value;
    //                        string TarefaResumo = nodeTarefa.Attributes["resumo"].Value.Trim() == "" ? "N" : nodeTarefa.Attributes["resumo"].Value.Trim();
    //                        string Nivel = nodeTarefa.Attributes["nivel"].Value;
    //                        string CodigoTarefaSuperior = nodeTarefa.Attributes["superior"].Value;
    //                        string EstruturaHierarquica = nodeTarefa.Attributes["estrutura"].Value;
    //                        string IndicaTarefaVisivel = nodeTarefa.Attributes["visivel"].Value.Trim() == "" ? "S" : nodeTarefa.Attributes["visivel"].Value;
    //                        string IndicaTarefaExpandida = nodeTarefa.Attributes["expandida"].Value.Trim() == "" ? "S" : nodeTarefa.Attributes["expandida"].Value;


    //                        string Duracao = "";
    //                        string formatoDuracao = "";
    //                        string InicioPrevisto = "";
    //                        string TerminoPrevisto = "";
    //                        string Predecessoras = "";
    //                        string MenorDataTarefa = "";
    //                        string DataRestricao = "";
    //                        string TipoRestricao = "";
    //                        string StringAlocacaoRecursoTarefa = "";
    //                        string indicesTarefasDependentes = "";
    //                        string anotacoes = "";

    //                        string inicioLB = "";
    //                        string terminoLB = "";
    //                        string trabalhoLB = "";
    //                        string duracaoLB = "";

    //                        string inicioReal = "";
    //                        string terminoReal = "";
    //                        string trabalhoReal = "";
    //                        string custoReal = "";
    //                        string duracaoReal = "";

    //                        string PercentualConcluido = "";
    //                        string IndicaTarefaCritica = "";

    //                        string DuracaoAcumuladaEmMinutos = "";
    //                        string TrabalhoTarefa = "";
    //                        string CustoTarefa = "";

    //                        // se for uma tarefa válida...
    //                        if (indicaTarefaValida == "S")
    //                        {
    //                            Duracao = nodeTarefa.Attributes["duracao"].Value;
    //                            formatoDuracao = nodeTarefa.Attributes["formatoDuracao"].Value;
    //                            InicioPrevisto = nodeTarefa.Attributes["inicio"].Value;
    //                            TerminoPrevisto = nodeTarefa.Attributes["termino"].Value;
    //                            Predecessoras = nodeTarefa.Attributes["predecessoras"].Value;
    //                            MenorDataTarefa = nodeTarefa.Attributes["menorData"].Value;
    //                            DataRestricao = nodeTarefa.Attributes["restricao"].Value;
    //                            TipoRestricao = nodeTarefa.Attributes["tipoRestricao"].Value;
    //                            StringAlocacaoRecursoTarefa = nodeTarefa.Attributes["alocacao"].Value;
    //                            indicesTarefasDependentes = nodeTarefa.Attributes["dependentes"].Value;
    //                            anotacoes = nodeTarefa.Attributes["anotacao"].Value;
    //                            if (anotacoes != "")
    //                                anotacoes = anotacoes.Replace("&quot;", "\"");

    //                            inicioLB = nodeTarefa.Attributes["inicioLB"].Value;
    //                            terminoLB = nodeTarefa.Attributes["terminoLB"].Value;
    //                            trabalhoLB = nodeTarefa.Attributes["trabalhoLB"].Value.Replace(',', '.');
    //                            duracaoLB = nodeTarefa.Attributes["duracaoLB"].Value;

    //                            inicioReal = nodeTarefa.Attributes["inicioReal"].Value;
    //                            terminoReal = nodeTarefa.Attributes["terminoReal"].Value;
    //                            trabalhoReal = nodeTarefa.Attributes["trabalhoReal"].Value.Replace(',', '.');
    //                            custoReal = nodeTarefa.Attributes["custoReal"].Value.Replace(',', '.');
    //                            duracaoReal = nodeTarefa.Attributes["duracaoReal"].Value;

    //                            PercentualConcluido = nodeTarefa.Attributes["concluido"].Value.Replace(',', '.');
    //                            IndicaTarefaCritica = nodeTarefa.Attributes["critica"].Value.Trim() == "" ? "N" : nodeTarefa.Attributes["critica"].Value;

    //                            DuracaoAcumuladaEmMinutos = nodeTarefa.Attributes["duracaoAcumulada"].Value.Trim() == "" ? "0" : nodeTarefa.Attributes["duracaoAcumulada"].Value;

    //                            TrabalhoTarefa = nodeTarefa.Attributes["trabalho"].Value.Replace(',', '.');
    //                            CustoTarefa = nodeTarefa.Attributes["custo"].Value.Replace(',', '.');
    //                        }
    //                        else
    //                        {
    //                            // tarefas não validas - linhas em branco
    //                        }


    //                        if (MenorDataTarefa == "")
    //                            MenorDataTarefa = "null";
    //                        else
    //                            MenorDataTarefa = string.Format("convert(datetime, '{0}', 103)", MenorDataTarefa);

    //                        if (DataRestricao == "")
    //                            DataRestricao = "null";
    //                        else
    //                            DataRestricao = string.Format("convert(datetime, '{0}', 103)", DataRestricao);

    //                        if (TipoRestricao == "")
    //                            TipoRestricao = "null";
    //                        else
    //                            TipoRestricao = string.Format("'{0}'", TipoRestricao);

    //                        TrabalhoTarefa = TrabalhoTarefa == "" ? "0" : TrabalhoTarefa;
    //                        CustoTarefa = CustoTarefa == "" ? "0" : CustoTarefa;

    //                        string temp = string.Format(
    //                            @"
    //                        -----------------------------------------------------------------------------------------------------------------------------
    //                        if ( not exists (Select 1 from {0}.{1}.TarefaCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}' and CodigoTarefa = {3} and dataExclusao is null))
    //
    //                            INSERT INTO {0}.{1}.TarefaCronogramaProjeto 
    //                                (CodigoCronogramaProjeto, CodigoTarefa, NomeTarefa, SequenciaTarefaCronograma, CodigoTarefaSuperior, Inicio, Termino, 
    //                                 Duracao, Trabalho, DataInclusao, nivel )
    //                            VALUES ( '{2}', {3}, '{4}', {5}, {6}, convert(datetime, '{7}', 103), convert(datetime, '{8}', 103), {9}, {10}, GetDate(), {15} ) 
    //
    //                        UPDATE {0}.{1}.TarefaCronogramaProjeto
    //                           SET NomeTarefa = '{4}'
    //                             , SequenciaTarefaCronograma = {5}
    //                             , CodigoTarefaSuperior = {6}
    //                             , Inicio = convert(datetime, '{7}', 103)
    //                             , Termino = convert(datetime, '{8}', 103)
    //                             , Duracao = {9}
    //                             , Trabalho = {10}
    //                             , Custo = {11}
    //                             , Predecessoras = '{12}'
    //                             , IndicaTarefaResumo = '{13}'
    //                             , IndicaMarco = '{14}'
    //                             , DataUltimaAlteracao = GetDate()
    //                             , nivel = {15}
    //                             , MenorDataTarefa = {16}
    //                             , DataRestricao = {17}
    //                             , tipoRestricao = {18}
    //                             , EstruturaHierarquica = '{19}'
    //                             , StringAlocacaoRecursoTarefa = {20}
    //                             , indicesTarefasDependentes = {21}
    //                             , anotacoes = '{22}'
    //
    //                             --, InicioLB = {23}
    //                             --, TerminoLB = {24}
    //                             --, DuracaoLB = {25}
    //                             --, TrabalhoLB = {26}
    //
    //                             , InicioReal = {27}
    //                             , TerminoReal = {28}
    //                             , DuracaoReal = {29}
    //                             , TrabalhoReal = {30}
    //                             , CustoReal = {31}
    //
    //                             , PercentualFisicoConcluido = {32}
    //                             , FormatoDuracao = '{33}'
    //                             , IndicaTarefaCritica = '{34}'
    //                             , DuracaoAcumuladaEmMinutos = {35}
    //                             , indicaTarefaVisivel= '{36}'
    //                             , indicaTarefaExpandida= '{37}'
    //                             , indicaTarefaValida = '{38}'
    //                         WHERE CodigoCronogramaProjeto = '{2}'
    //                           AND CodigoTarefa = {3}
    //                           AND DataExclusao is null
    //                    ", bancodb, Ownerdb,
    //                         codigoProjeto, CodigoTarefa, Tarefa, Linha,
    //                         CodigoTarefaSuperior == "" || CodigoTarefaSuperior == "-1" ? "null" : CodigoTarefaSuperior,
    //                         InicioPrevisto, TerminoPrevisto,
    //                         Duracao == "" ? "0" : Duracao,
    //                         TrabalhoTarefa, CustoTarefa,
    //                         Predecessoras, TarefaResumo, Marco, Nivel,
    //                         MenorDataTarefa, DataRestricao, TipoRestricao, EstruturaHierarquica,
    //                         StringAlocacaoRecursoTarefa == "" ? "null" : "'" + StringAlocacaoRecursoTarefa + "'",
    //                         indicesTarefasDependentes == "" ? "null" : "'" + indicesTarefasDependentes + "'",
    //                         anotacoes.Replace("'", "''"),

    //                         inicioLB == "" ? "null" : "convert(datetime, '" + inicioLB + "', 103)",
    //                         terminoLB == "" ? "null" : "convert(datetime, '" + terminoLB + "', 103)",
    //                         duracaoLB == "" ? "null" : duracaoLB,
    //                         trabalhoLB == "" ? "null" : trabalhoLB,

    //                         inicioReal == "" ? "null" : "convert(datetime, '" + inicioReal + "', 103)",
    //                         terminoReal == "" ? "null" : "convert(datetime, '" + terminoReal + "', 103)",
    //                         duracaoReal == "" ? "null" : duracaoReal,
    //                         trabalhoReal == "" ? "null" : trabalhoReal,
    //                         custoReal == "" ? "null" : custoReal,

    //                         PercentualConcluido == "" ? "null" : PercentualConcluido,
    //                         formatoDuracao,
    //                         IndicaTarefaCritica,
    //                         DuracaoAcumuladaEmMinutos == "" ? "0" : DuracaoAcumuladaEmMinutos,
    //                         IndicaTarefaVisivel, IndicaTarefaExpandida, indicaTarefaValida
    //                        );

    //                        comandoSQL += temp;
    //                        qtdTarefasInseridasComandoSQL++;
    //                        if (qtdTarefasInseridasComandoSQL == 100)
    //                        {
    //                            FaseExecutarComando = true;
    //                            classeDados.execSQL(comandoSQL, ref afetatos);
    //                            comandoSQL = "";
    //                            qtdTarefasInseridasComandoSQL = 0;
    //                        }
    //                    }

    //                    if (qtdTarefasInseridasComandoSQL > 0)
    //                        classeDados.execSQL(comandoSQL, ref afetatos);
    //                }
    //                catch (Exception ex)
    //                {
    //                    throw new Exception(string.Format("Erro ao {0} ({1}): {2} ", (FaseExecutarComando ? "Executar o comando para inserir o lote de tarefas finalizado por " : "Construir o comando para salvar a tarefa "), Linha, Tarefa) + "\n" + ex.Message);
    //                }

    //            }
    //            return true;
    //        }

    private bool persisteTarefasExcluidasCronograma(int codigoUsuario, string codigoProjeto, XmlDocument XmlCronograma)
    {
        // recuperar as tarefas
        XmlNodeList xmlTarefasExcluidas = XmlCronograma.SelectNodes("Projeto/TarefasExcluidas");

        if (xmlTarefasExcluidas.Count > 0 && xmlTarefasExcluidas[0].ChildNodes.Count > 0)
        {
            string comandoSQL = "";
            int afetatos = 0;
            int qtdTarefasExcluidasComandoSQL = 0;
            try
            {
                // Se o código da tarefas for -1 e se existir o atributo "tarefasSubstituidas", todas as tarefas originais devem ser excluídas
                if (((XmlNode)xmlTarefasExcluidas[0].ChildNodes[0]).Attributes["codigo"].Value == "-1")
                {
                    XmlNode tarefasSubstituidas = ((XmlNode)xmlTarefasExcluidas[0].ChildNodes[0]).Attributes["tarefasSubstituidas"];
                    if (tarefasSubstituidas != null && tarefasSubstituidas.Value == "S")
                    {
                        comandoSQL = string.Format(
                            @"
                                -----------------------------------------------------------------------------------------------------------------------------
                                DELETE FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                 WHERE CodigoCronogramaProjeto = '{2}'                

                                DELETE FROM {0}.{1}.RecursoCronogramaProjeto 
                                 WHERE CodigoCronogramaProjeto = '{2}'                

                                DELETE FROM {0}.{1}.TarefaCronogramaProjeto 
                                 WHERE CodigoCronogramaProjeto = '{2}'   
                                 ", bancodb, Ownerdb, codigoProjeto);

                        classeDados.execSQL(comandoSQL, ref afetatos);
                    }
                }
                else
                {
                    foreach (XmlNode nodeTarefa in xmlTarefasExcluidas[0].ChildNodes)
                    {
                        string CodigoTarefa = nodeTarefa.Attributes["codigo"].Value;


                        string temp = string.Format(
                            @"
                        -----------------------------------------------------------------------------------------------------------------------------
                        UPDATE {0}.{1}.TarefaCronogramaProjeto
                           SET DataExclusao = GetDate()
                         WHERE CodigoCronogramaProjeto = '{2}'
                           AND CodigoTarefa = {3}
                                 ", bancodb, Ownerdb,
                         codigoProjeto, CodigoTarefa);

                        comandoSQL += temp;
                        qtdTarefasExcluidasComandoSQL++;
                        if (qtdTarefasExcluidasComandoSQL == 100)
                        {
                            classeDados.execSQL(comandoSQL, ref afetatos);
                            comandoSQL = "";
                            qtdTarefasExcluidasComandoSQL = 0;
                        }
                    }

                    if (qtdTarefasExcluidasComandoSQL > 0)
                        classeDados.execSQL(comandoSQL, ref afetatos);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + comandoSQL);
            }

        }
        return true;
    }

    private bool persisteRecursosCronograma(int codigoUsuario, string codigoProjeto, XmlDocument XmlCronograma)
    {
        if (XmlCronograma != null)
        {
            // recuperar os recursos
            XmlNodeList xmlRecursos = XmlCronograma.SelectNodes("Projeto/Recursos");

            if (xmlRecursos.Count > 0)
            {
                string comandoSQL = "";
                int afetatos = 0;
                int qtdRecursosInseridosComandoSQL = 0;
                try
                {
                    foreach (XmlNode nodeRecurso in xmlRecursos[0].ChildNodes)
                    {
                        string sequenciaRecurso = nodeRecurso.Attributes["indice"].Value;
                        string CodigoRecursoProjeto = nodeRecurso.Attributes["Codigo"].Value;
                        string CodigoRecursoCorporativo = nodeRecurso.Attributes["codigoCorporativo"].Value;
                        string NomeRecurso = nodeRecurso.Attributes["Nome"].Value;
                        string CustoHora = nodeRecurso.Attributes["VHora"].Value.Replace(',', '.');
                        string CustoHoraExtra = nodeRecurso.Attributes["VHExtra"].Value.Replace(',', '.');
                        string CustoUso = nodeRecurso.Attributes["VUso"].Value.Replace(',', '.');
                        string EMail = nodeRecurso.Attributes["email"].Value;
                        string NomeGrupoRecurso = nodeRecurso.Attributes["Grupo"].Value;
                        string CodigoTipoRecurso = nodeRecurso.Attributes["Tipo"].Value;
                        string UnidadeMedidaRecurso = nodeRecurso.Attributes["UnidadeMedidaRecurso"].Value;
                        string CodigoGrupoRecurso = "null";// nodeRecurso.Attributes["CodigoGrupoRecurso"].Value;
                        string Anotacoes = "null";//nodeRecurso.Attributes["Anotacoes"].Value;
                        string unidadeMaxima = nodeRecurso.Attributes["unidadeMaxima"].Value.Replace(',', '.');
                        string iniciaisRecurso = nodeRecurso.Attributes["iniciais"].Value;

                        // se não é recurso corporativo
                        if (CodigoRecursoCorporativo == "" || CodigoRecursoCorporativo == "-1")
                            CodigoRecursoCorporativo = "null";

                        string temp = string.Format(
                            @"
                                -----------------------------------------------------------------------------------------------------------------------------
                                if ( not exists (Select 1 from {0}.{1}.RecursoCronogramaProjeto WHERE CodigoCronogramaProjeto = '{2}' and CodigoRecursoProjeto = {3}))

                                    INSERT INTO {0}.{1}.RecursoCronogramaProjeto 
                                        (CodigoCronogramaProjeto, CodigoRecursoProjeto, NomeRecurso, CodigoTipoRecurso, DataInclusao, sequenciaRecurso, UnidadeMedidaRecurso )
                                    VALUES ( '{2}', {3}, '{4}', {5}, getdate(), {14}, '{17}' ) 

                                UPDATE {0}.{1}.RecursoCronogramaProjeto
                                   SET NomeRecurso = '{4}'
                                     , CodigoTipoRecurso = {5}
                                     , CodigoRecursoCorporativo = {6}
                                     , CustoHora = {7}
                                     , CustoHoraExtra = {8}
                                     , CustoUso = {9}
                                     , EMail = '{10}'
                                     , CodigoGrupoRecurso = {11}
                                     , NomeGrupoRecurso = '{12}'
                                     , Anotacoes = '{13}'
                                     , sequenciaRecurso = {14}
                                     , unidadeMaxima = {15}
                                     , iniciaisRecurso = '{16}'
                                     , UnidadeMedidaRecurso = '{17}'
                                 WHERE CodigoCronogramaProjeto = '{2}'
                                   AND CodigoRecursoProjeto = {3}
                            ", bancodb, Ownerdb,
                         codigoProjeto, CodigoRecursoProjeto, NomeRecurso, CodigoTipoRecurso,
                         CodigoRecursoCorporativo == "" ? "null" : CodigoRecursoCorporativo,
                         CustoHora, CustoHoraExtra, CustoUso, // {7}, {8} e {9}
                         EMail, CodigoGrupoRecurso == "" ? "null" : CodigoGrupoRecurso,
                         NomeGrupoRecurso, Anotacoes, sequenciaRecurso,
                         unidadeMaxima, iniciaisRecurso, UnidadeMedidaRecurso); //{15}, {16} e {17}

                        comandoSQL += temp;
                        qtdRecursosInseridosComandoSQL++;
                        if (qtdRecursosInseridosComandoSQL == 100)
                        {
                            classeDados.execSQL(comandoSQL, ref afetatos);
                            comandoSQL = "";
                            qtdRecursosInseridosComandoSQL = 0;
                        }
                    }

                    if (qtdRecursosInseridosComandoSQL > 0)
                        classeDados.execSQL(comandoSQL, ref afetatos);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " " + comandoSQL);
                }
            }
        }
        return false;
    }

    private bool persistAtribuicaoRecursoTarefa(string codigoProjeto, XmlDocument XmlCronograma)
    {
        if (XmlCronograma != null)
        {
            // recuperar os recursos
            XmlNodeList xmlRecursosTarefa = XmlCronograma.SelectNodes("Projeto/Alocacoes");

            if (xmlRecursosTarefa.Count > 0)
            {
                string comandoSQL = "";
                int afetatos = 0;
                int qtdLinhasInseridasComandoSQL = 0;
                try
                {
                    foreach (XmlNode recursoTarefa in xmlRecursosTarefa[0].ChildNodes)
                    {
                        string CodigoTarefa = recursoTarefa.Attributes["codigoTarefa"].Value;
                        string CodigoRecursoProjeto = recursoTarefa.Attributes["codigoRecurso"].Value;
                        string UnidadeAtribuicao = recursoTarefa.Attributes["unidades"].Value.Replace(',', '.');
                        string Trabalho = recursoTarefa.Attributes["trabalho"].Value.Replace(',', '.');
                        string Custo = recursoTarefa.Attributes["Custo"].Value.Replace(',', '.');

                        string Inicio = recursoTarefa.Attributes["Inicio"].Value;
                        string Termino = recursoTarefa.Attributes["Termino"].Value;
                        string TrabalhoReal = recursoTarefa.Attributes["TrabalhoReal"].Value.Replace(',', '.');
                        string InicioReal = recursoTarefa.Attributes["InicioReal"].Value;
                        string TerminoReal = recursoTarefa.Attributes["TerminoReal"].Value;
                        string CustoReal = recursoTarefa.Attributes["CustoReal"].Value.Replace(',', '.');
                        string IndicaAtribuicaoRecursoCorporativo = recursoTarefa.Attributes["IndicaAtribuicaoRecursoCorporativo"].Value;
                        string CodigoTipoRecurso = recursoTarefa.Attributes["CodigoTipoRecurso"].Value;
                        string CustoRecurso = recursoTarefa.Attributes["CustoRecurso"].Value.Replace(',', '.');

                        UnidadeAtribuicao = UnidadeAtribuicao == "" ? "null" : UnidadeAtribuicao;
                        Trabalho = Trabalho == "" ? "null" : Trabalho;
                        Custo = Custo == "" ? "null" : Custo;

                        CustoRecurso = CustoRecurso == "" ? "null" : CustoRecurso;

                        TrabalhoReal = TrabalhoReal == "" ? "null" : TrabalhoReal;
                        InicioReal = InicioReal == "" ? "null" : string.Format("convert(datetime, '{0}', 103)", InicioReal);
                        TerminoReal = TerminoReal == "" ? "null" : string.Format("convert(datetime, '{0}', 103)", TerminoReal);
                        CustoReal = CustoReal == "" ? "null" : CustoReal;

                        if (UnidadeAtribuicao != "null")
                            Custo = CustoRecurso;

                        string comandoDeleleAtribuicao = "";
                        // se não tem custo e nem unidadeAtribuicao, o registro deve ser excluído do banco
                        if (Custo == "null" && UnidadeAtribuicao == "null")
                        {
                            comandoDeleleAtribuicao = string.Format(
                                @"if (1 = 1) 
                                  BEGIN

                                      DELETE FROM {0}.{1}.AtualizacaoDiariaTarefaTimeSheet 
                                       WHERE CodigoAtribuicao in (SELECT CodigoAtribuicao 
                                                                    FROM {0}.{1}.AtribuicaoRecursoTarefa 
                                                                   WHERE CodigoCronogramaProjeto = '{2}' 
                                                                     AND CodigoRecursoProjeto = {3}
                                                                     AND CodigoTarefa = {4}              )

                                      DELETE FROM {0}.{1}.AtribuicaoDiariaRecurso
                                       WHERE CodigoCronogramaProjeto = '{2}'
                                         AND CodigoRecursoProjeto = {3}
                                         AND CodigoTarefa = {4}

                                      DELETE FROM {0}.{1}.AtribuicaoRecursoTarefa
                                       WHERE CodigoCronogramaProjeto = '{2}'
                                         AND CodigoRecursoProjeto = {3}
                                         AND CodigoTarefa = {4}
                                  END
                                  else 
                                 ", bancodb, Ownerdb,
                                    codigoProjeto, CodigoRecursoProjeto, CodigoTarefa);
                        }

                        string trabalho = ", Trabalho = 0 ";
                        // se o recurso é do tipo "Pessoa" ou "Equipamento", a coluna trabalho fica igual a coluna "UnidadeAtribuicao"
                        if (CodigoTipoRecurso == "1" || CodigoTipoRecurso == "2")
                            trabalho = ", Trabalho = " + UnidadeAtribuicao;

                        string temp = string.Format(
                            @"
                                -----------------------------------------------------------------------------------------------------------------------------
                                {5} 
                                BEGIN
                                    if ( not exists (Select 1 from {0}.{1}.AtribuicaoRecursoTarefa WHERE CodigoCronogramaProjeto = '{2}' and CodigoRecursoProjeto = {3} and CodigoTarefa = {4} ))

                                        INSERT INTO {0}.{1}.AtribuicaoRecursoTarefa 
                                            (CodigoCronogramaProjeto, CodigoRecursoProjeto, CodigoTarefa)
                                        VALUES ( '{2}', {3}, {4} ) 
                                    
                                    
                                    UPDATE {0}.{1}.AtribuicaoRecursoTarefa
                                       SET Inicio = convert(datetime, '{6}', 103)
                                         , Termino = convert(datetime, '{7}', 103)
                                         , Custo = {8}
                                         , TrabalhoReal = {12}
                                         , InicioReal = {13}
                                         , TerminoReal = {14}
                                         , CustoReal = {15}
                                         , UnidadeAtribuicao = {9}
                                         , IndicaAtribuicaoRecursoCorporativo = '{10}'
                                         {11}
                                     WHERE CodigoCronogramaProjeto = '{2}'
                                       AND CodigoRecursoProjeto = {3}
                                       AND CodigoTarefa = {4}
                                END
                            ", bancodb, Ownerdb,
                         codigoProjeto, CodigoRecursoProjeto, CodigoTarefa,
                         comandoDeleleAtribuicao,
                         Inicio, Termino, Custo, UnidadeAtribuicao,
                         IndicaAtribuicaoRecursoCorporativo, trabalho, TrabalhoReal, InicioReal, TerminoReal, CustoReal);

                        comandoSQL += temp;
                        qtdLinhasInseridasComandoSQL++;
                        if (qtdLinhasInseridasComandoSQL == 100)
                        {
                            classeDados.execSQL(comandoSQL, ref afetatos);
                            comandoSQL = "";
                            qtdLinhasInseridasComandoSQL = 0;
                        }
                    }

                    if (qtdLinhasInseridasComandoSQL > 0)
                        classeDados.execSQL(comandoSQL, ref afetatos);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " " + comandoSQL);
                }
            }
        }
        return false;
    }

    #endregion

    private bool getCronogramaJaExiste(string codigoProjeto)
    {
        DataSet ds = classeDados.getDataSet(string.Format(
            @"Select 1 
                    from {0}.{1}.CronogramaProjeto
                   where CodigoCronogramaProjeto = '{2}' ", bancodb, Ownerdb, codigoProjeto));

        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count == 1)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region GANTT

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public string getXMLGantt(XmlDocument XmlCronograma)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return "";

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return "";

        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_").Replace(" ", "_");

        string nomeGrafico = @"ArquivosTemporarios\gantt_" + dataHora + ".xml";

        //Cria o arquivo XML
        string nome = escreveXML(XmlCronograma.InnerXml, nomeGrafico);
        nome = nome.Substring(nome.IndexOf("gantt"));
        return nome;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public string getJSONGantt(string JsonCronograma)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return "";

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return "";

        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_").Replace(" ", "_");

        string nomeGrafico = @"ArquivosTemporarios\gantt_" + dataHora + ".json";

        //Cria o arquivo XML
        string nome = escreveJSON(JsonCronograma, nomeGrafico);
        nome = nome.Substring(nome.IndexOf("gantt"));
        return nome;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public bool getModoCalculoAtrasoTotal(int codigoEntidade)
    {
        bool retorno = false;
        if (segurancaWS == null || segurancaWS.Key != key)
            retorno = false;

        string comandoSQL = @"SELECT Valor FROM ParametroConfiguracaoSistema
                               WHERE Parametro = 'calculoDesempenhoFisicoTarefa' 
                                 AND CodigoEntidade = " + codigoEntidade;
        DataSet ds = classeDados.getDataSet(comandoSQL);
        if(ds != null)
        {
            if(ds.Tables[0] != null)
            {
                retorno = ds.Tables[0].Rows[0]["Valor"].ToString().ToUpper() == "TOTAL";
            }            
        }
        return retorno;
    }


    [SoapHeader("segurancaWS")]
    [WebMethod]
    public string getNomeDependencias(string JsonDependencias)
    {
        if (segurancaWS == null || segurancaWS.Key != key)
            return "";

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            return "";

        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_").Replace(" ", "_");

        string nomeGrafico = @"ArquivosTemporarios\ganttDependencias_" + dataHora + ".json";

        //Cria o arquivo XML
        string nome = escreveJSON(JsonDependencias, nomeGrafico);
        nome = nome.Substring(nome.IndexOf("ganttDependencias"));
        return nome;
    }
    [SoapHeader("segurancaWS")]
    [WebMethod]

    private string escreveXML(string xml, string nome)
    {
        StreamWriter strWriter;
        //cabeçalho do XML
        string cabecalho = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n";
        //cria um novo arquivo xml e abre para escrita
        strWriter = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + nome, false, System.Text.Encoding.UTF8);
        //escrever o cabeçalho no arquivo xml criado
        strWriter.Write(cabecalho);
        //escrever o corpo do XML no arquivo xml criado
        strWriter.Write(xml);
        //fecha o arquivo criado
        strWriter.Close();

        return nome;
    }

    private string escreveJSON(string strJson, string nome)
    {
        StreamWriter strWriter;
        //cabeçalho do JSON
        //cria um novo arquivo JSON e abre para escrita
        strWriter = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + nome, false, System.Text.Encoding.UTF8);
        //escrever o cabeçalho no arquivo xml criado
        strWriter.Write(strJson);
        //fecha o arquivo criado
        strWriter.Close();

        return nome;
    }

    #endregion

    #region Vinculo com tarefa externa

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getPossiveisCronogramasVinculoExterno(string codigoCronogramaContexto, int codigoEntidade, out int retorno)
    {
        retorno = 0;
        if (segurancaWS == null || segurancaWS.Key != key)
            retorno = -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            retorno = -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            retorno = -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (retorno > 0)
            return null;

        string comandoSQL = string.Format(
            @"SELECT *, CodigoCronograma +';' + convert(varchar,CodigoProjeto) as Codigo
                    FROM {0}.{1}.f_tasq_GetPossiveisCronogramasVinculoExterno('{2}', {3}, {4})
                   ORDER BY NomeProjeto", bancodb, Ownerdb, codigoCronogramaContexto, codigoEntidade, codigoUsuario);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    [SoapHeader("segurancaWS")]
    [WebMethod]
    public DataSet getInfoVinculoExterno(int CodigoCalendario, string codigoCronogramaExterno, int? codigoTarefaExterna, int? indiceTarefaExterna, DateTime? dataRefenciaInicioTarefa, out int retorno)
    {
        retorno = 0;
        if (segurancaWS == null || segurancaWS.Key != key)
            retorno = -1; // chave1 não é válida

        // verifica se a chave2 ainda é válida.
        int codigoUsuario = verificaValidadeChaveAutenticacao(segurancaWS.Key2);
        if (codigoUsuario < 0)
            retorno = -2; // chave2 não é válida

        if (codigoUsuario != segurancaWS.SystemID)
            retorno = -3; // codigo do usuário com a chave é diferente do usuário autenticado.

        if (retorno > 0)
            return null;

        string _dataRefInicioTarefa = "null";
        if (dataRefenciaInicioTarefa.HasValue)
            _dataRefInicioTarefa = "'" + dataRefenciaInicioTarefa.Value.ToString("dd/MM/yyyy HH:mm:ss") + "'";

        string _codigoTarefaExterna = "null";
        if (codigoTarefaExterna.HasValue)
            _codigoTarefaExterna = codigoTarefaExterna.Value + "";

        string _indiceTarefaExterna = "null";
        if (indiceTarefaExterna.HasValue)
            _indiceTarefaExterna = indiceTarefaExterna.Value + "";


        string comandoSQL = string.Format(
            @"SELECT * 
                    FROM {0}.{1}.f_tasq_GetInfoVinculoExterno({2}, '{3}', {4}, {5}, convert(datetime, {6}, 103)) "
             , bancodb, Ownerdb, CodigoCalendario, codigoCronogramaExterno, _codigoTarefaExterna
             , _indiceTarefaExterna, _dataRefInicioTarefa);

        DataSet ds = classeDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            return ds;
        }

        return null;
    }

    #endregion

}

public class CdisSoapHeader : SoapHeader
{
    private string _key;
    private string _key2;
    private string _mac;
    private XmlDocument _xmlCronograma;

    // private ICredentials _credenciais;
    private string _usuario;
    private string _senha;
    private int _codigo;
    private string _nomeUsuario;
    private bool _anonimo;

    public CdisSoapHeader()
    {
    }

    public CdisSoapHeader(string keyHeader)
    {
        this._key = keyHeader;
    }

    public string Key
    {
        get { return this._key; }
        set { this._key = value; }
    }

    public string Key2
    {
        get { return _key2; }
        set { _key2 = value; }
    }

    public string Mac
    {
        get { return _mac; }
        set { _mac = value; }
    }

    public XmlDocument Xml
    {
        get { return _xmlCronograma; }
        set { _xmlCronograma = value; }
    }

    //public ICredentials NetworkCredentials
    //{
    //    get { return _credenciais; }
    //    set { _credenciais = value; }
    //}

    public string SystemUser
    {
        get { return _usuario; }
        set { _usuario = value; }
    }

    public string SystemPassword
    {
        get { return _senha; }
        set { _senha = value; }
    }

    public int SystemID
    {
        get { return _codigo; }
        set { _codigo = value; }
    }

    public string SystemNome
    {
        get { return _nomeUsuario; }
        set { _nomeUsuario = value; }
    }

    public bool SystemAnonimo
    {
        get { return _anonimo; }
        set { _anonimo = value; }
    }
}


