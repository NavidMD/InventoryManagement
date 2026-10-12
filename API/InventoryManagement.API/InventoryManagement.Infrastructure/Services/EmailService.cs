using InventoryManagement.Core.Interfaces.IServices;
using InventoryManagement.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendEmailAsync(EmailModel email, CancellationToken cancellationToken)
        {
            var smtpHost = configuration["EmailSettings:Host"];
            var smtpPort = int.Parse(configuration["EmailSettings:Port"]!);
            var smtpUser = configuration["EmailSettings:UserName"];
            var smtpPassword = configuration["EmailSettings:Password"];
            var fromEmail = configuration["EmailSettings:FromEmail"];
            var fromName = configuration["EmailSettings:FromName"];

            var emailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail!, fromName),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };
            emailMessage.To.Add(email.To);

            using var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(emailMessage, cancellationToken);
        }
    }
}
