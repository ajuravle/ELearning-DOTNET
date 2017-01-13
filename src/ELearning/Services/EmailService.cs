using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using MailKit.Net.Smtp;
using MimeKit;

namespace ELearning.Services
{
    public class EmailService
    {

        public void SendMail(string toMailAddress)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ELearning", "woha.project@gmail.com"));
            message.To.Add(new MailboxAddress("", "iuly2795@gmail.com"));
            message.Subject = "Welkome to Elearning";

            message.Body = new TextPart("plain")
            {
                Text = @"Welkome to Elearning"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);


                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("woha.project", "proiecttw");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
