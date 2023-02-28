﻿using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;
using HouseRent.Services;

namespace Medicoz.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string html)
        {

            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ceka592@yandex.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.yandex.ru", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ceka592@yandex.com", "Ceyhun3252558");
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}