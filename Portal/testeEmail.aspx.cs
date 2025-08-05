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
using System.Net.Mail;
using System.Net;

public partial class administracao_testeEmail : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        if (cDados.getInfoSistema("CodigoEntidade") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack && !IsCallback)
            recuperaDados();
    }

    private void recuperaDados()
    {
        //busca as informações do smtp na tabela de parametros
        DataSet ds = cDados.getParametrosSistema("smtpServer", "smtpUser", "smtpPassword", "smtpPort", "smtpUtilizaSSL", "remetenteEmailProjeto", "EnviarEmailPeloBancoDeDados");

        string _EmailRemetente = "Portal da Estratégia<admin@project.com>";

        if (ds != null && ds.Tables[0] != null)
        {
            txtServidor.Text = ds.Tables[0].Rows[0]["smtpServer"] != null ? ds.Tables[0].Rows[0]["smtpServer"].ToString() : "";
            txtUsuario.Text = ds.Tables[0].Rows[0]["smtpUser"] != null ? ds.Tables[0].Rows[0]["smtpUser"].ToString() : "";
            txtSenha.Text = ds.Tables[0].Rows[0]["smtpPassword"] != null ? ds.Tables[0].Rows[0]["smtpPassword"].ToString() : "";
            txtPorta.Text = ds.Tables[0].Rows[0]["smtpPort"] + "" != "" ? ds.Tables[0].Rows[0]["smtpPort"].ToString() : "";
            txtRemetenteEmail.Text = Server.HtmlEncode(ds.Tables[0].Rows[0]["remetenteEmailProjeto"] != null ? ds.Tables[0].Rows[0]["remetenteEmailProjeto"].ToString() : _EmailRemetente);
            ckSSL.Checked = ds.Tables[0].Rows[0]["smtpUtilizaSSL"] + "" == "S";
            string mensagem = "O e-mail será enviado pelo servidor de aplicação (IIS).";
            if (ds.Tables[0].Rows[0]["EnviarEmailPeloBancoDeDados"] != null && ds.Tables[0].Rows[0]["EnviarEmailPeloBancoDeDados"].ToString() == "S")
                mensagem = "O e-mail será enviado pelo servidor de banco de dados.";
            lblTipoServidorEnvioEmail.Text = mensagem;
        }

    }

    protected void callBackEnviar_Callback1(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBackEnviar.JSProperties["cp_Msg"] = ExcutarEnvioEmail();
    }

    public string ExcutarEnvioEmail()
    {        
        if (txtServidor.Text.Trim() == "")
        {
            return "As configurações do servidor de emails não foram encontradas no banco de dados!";
        }

        string assunto = "Teste Email - " + cDados.getNomeSistema();
        string corpoEmail = string.Format("Sucesso! Email enviado em {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);

        int retornoStatus = 0;
        string emailEnviado = cDados.enviarEmail(assunto, txtDestinatario.Text, "", corpoEmail, "", "", ref retornoStatus);
        // se deu erro...
        if (retornoStatus == 0)
            emailEnviado = "Ocorreram problemas no envio do e-mail. Error = " + emailEnviado;
        else
            emailEnviado = "E-mail enviado com sucesso para: " + txtDestinatario.Text;

        return emailEnviado;

        /*


        //cria objeto com dados do e-mail
        System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();
                                
        //remetente do e-mail
        try
        {
            objEmail.From = new System.Net.Mail.MailAddress(txtRemetente.Text);
        }
        catch (Exception ex)
        {
            return "Email remetente inválido. Consulte os parâmetros do sistema." + Environment.NewLine + ex.Message;
        }

        //destinatários do e-mail
        foreach (string emailDest in txtDestinatario.Text.Split(';'))
        {
            if (emailDest.Trim() != "")
            {
                try { objEmail.To.Add(emailDest); }
                catch { }
            }
        }

        //prioridade do e-mail
        objEmail.Priority = System.Net.Mail.MailPriority.Normal;

        //formato do e-mail HTML (caso não queira HTML alocar valor false)
        objEmail.IsBodyHtml = true;

        //título do e-mail
        objEmail.Subject = assunto;

        //corpo do e-mail
        objEmail.Body = corpoEmail;

        //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        //cria objeto com os dados do SMTP
        SmtpClient objSmtp = new SmtpClient();

        //alocamos o endereço do host para enviar os e-mails, localhost(recomendado) ou smtp2.locaweb.com.br
        objSmtp.Host = txtServidor.Text;

        if (txtPorta.Text.Trim() != "")
            objSmtp.Port = int.Parse(txtPorta.Text);

        objSmtp.EnableSsl = ckSSL.Checked;

        //enviamos o e-mail através do método .send()
        try
        {            
            if (txtSenha.Text != "")
                objSmtp.Credentials = new NetworkCredential(txtUsuario.Text, txtSenha.Text);

            objSmtp.Send(objEmail);

            //excluímos o objeto de e-mail da memória
            objEmail.Dispose();

            return "E-mail enviado com sucesso para: " + txtDestinatario.Text;
        }
        catch (Exception ex)
        {
            //excluímos o objeto de e-mail da memória
            objEmail.Dispose();

            return "Ocorreram problemas no envio do e-mail. Error = " + ex.Message;
        }
        */
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        recuperaDados();
    }

}
