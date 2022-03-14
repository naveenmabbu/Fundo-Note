using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class Msmq
    {
        MessageQueue messageQue = new MessageQueue();
        public void sender(string token)
        {
            this.messageQue.Path = @".\private$\Token";
            try
            {
                if(!MessageQueue.Exists(this.messageQue.Path))
                {
                    MessageQueue.Create(this.messageQue.Path);
                }
                this.messageQue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                this.messageQue.ReceiveCompleted += MessageQue_ReceiveCompleted;
                this.messageQue.Send(token);
                this.messageQue.BeginReceive();
                this.messageQue.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MessageQue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = this.messageQue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.Gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("bobafett0207@gmail.com", "boba0207"),
                    EnableSsl = true
                };
                mailMessage.From = new MailAddress("bobafett0207@gmail.com");
                mailMessage.To.Add(new MailAddress("bobafett0207@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "Fundo Note APP Reset Link";
                smtpClient.Send(mailMessage);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
