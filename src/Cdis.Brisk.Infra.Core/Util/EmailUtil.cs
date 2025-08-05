using Cdis.Brisk.Infra.Core.RegularExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Cdis.Brisk.Infra.Core.Util
{

    public static class EmailUtil
    {
        public struct InfoEmail
        {
            public string NameHost { get; set; }
            public int NumberPort { get; set; }
            public string EmailFrom { get; set; }
            public string SubjectEmail { get; set; }
            public string BodyEmail { get; set; }
            public string CredentialUser { get; set; }
            public string CredentialPass { get; set; }
            public List<string> ListEmailTo { get; set; }
            public List<Attachment> ListAttachment { get; set; }

            public bool IsSsl { get; set; }
        }

        #region Methods

        /// <summary>
        ///   Método para o envio de e-mail.
        /// </summary>      
        public static void Enviar(InfoEmail infoEmail)
        {
            MailMessage mail = new MailMessage();
            List<string> listInvalidEmail = new List<string>();
            foreach (string email in infoEmail.ListEmailTo)
            {
                if (IsValidEmail(email))
                {
                    mail.To.Add(email);
                }
                else
                {
                    listInvalidEmail.Add(email);
                }
            }

            if (listInvalidEmail.Any())
            {
                throw new FormatException("Falha de envio - Existe algum e-mail inválido na sua lista: " + string.Join(";", listInvalidEmail));
            }

            mail.From = new MailAddress(infoEmail.EmailFrom);
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Subject = infoEmail.SubjectEmail;
            mail.BodyEncoding = Encoding.UTF8;

            if (infoEmail.ListAttachment != null)
            {
                foreach (Attachment attachment in infoEmail.ListAttachment)
                {
                    mail.Attachments.Add(attachment);
                }
            }

            mail.Body = infoEmail.BodyEmail;

            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            Send(mail, infoEmail.NameHost, infoEmail.NumberPort, infoEmail.CredentialUser, infoEmail.CredentialPass, infoEmail.IsSsl);

            
        }

        /// <summary>
        /// Verificar se o endereço de email é válido
        /// </summary>        
        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, PatternInfraCore.Email);
        }

        /// <summary>
        /// Enviar o e-mail
        /// </summary>        
        private static void Send(MailMessage mail, string host, int port, string user, string pass, bool isSsl)
        {
            try
            {
                SmtpClient smtpMailObj = new SmtpClient { Host = host, Port = port };
                smtpMailObj.UseDefaultCredentials = false;
                smtpMailObj.EnableSsl = isSsl;
                smtpMailObj.Credentials = new System.Net.NetworkCredential(user, pass);
                smtpMailObj.Send(mail);
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }
        #endregion

    }
}
