using KineApp.BLL.Interfaces;
using KineApp.IL.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.IL.Services
{
    public class Mailer : IMailer
    {
        private readonly MailerConfig _config;
        private readonly SmtpClient _smtpClient;

        public Mailer(MailerConfig config, SmtpClient smtpClient)
        {
            _config = config;
            _smtpClient = smtpClient;
            _smtpClient.Host = _config.Host;
            _smtpClient.Port = _config.Port;
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_config.Username, _config.Password);
        }

        public async Task Send(string subject, string message, params string[] to)
        {
            using MailMessage mail = CreateMail(subject, message, to);
            await _smtpClient.SendMailAsync(mail);
        }

        public async Task Send(string subject, string message, Attachment attachment, params string[] to)
        {
            using MailMessage mail = CreateMail(subject, message, to);
            mail.Attachments.Add(attachment);
            await _smtpClient.SendMailAsync(mail);
        }

        private MailMessage CreateMail(string subject, string message, string[] to)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(_config.Username);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            foreach (string dest in to)
            {
                mailMessage.To.Add(new MailAddress(dest));
            }
            return mailMessage;
        }
    }
}
