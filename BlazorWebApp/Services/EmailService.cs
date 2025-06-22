using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlazorWebApp.Services
{
    public class EmailService
    {
        private readonly string smtpHost;
        private readonly int smtpPort;
        private readonly string smtpUser;
        private readonly string smtpPass;
        private readonly string fromName;
        private readonly string fromAddress;
        private readonly ILogger<EmailService> logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            smtpHost = configuration["EmailSettings:SmtpServer"];
            smtpPort = int.TryParse(configuration["EmailSettings:SmtpPort"], out var port) ? port : 587;
            smtpUser = configuration["EmailSettings:Username"];
            smtpPass = configuration["EmailSettings:Password"];
            fromName = configuration["EmailSettings:FromName"];
            fromAddress = configuration["EmailSettings:FromAddress"];
            this.logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage()
                {
                    From = new MailAddress(fromAddress, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(to);
                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };
                await client.SendMailAsync(message);
                logger.LogInformation($"Email sent to {to} with subject '{subject}'.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to send email to {to} with subject '{subject}'.");
                throw;
            }
        }

        public async Task SendVerificationEmailAsync(string to, string verificationLink)
        {
            string subject = "Nafa Trader: Please verify your email address";
            string body = $@"
                <html>
                <body>
                    <p>Dear User,</p>
                    <p>Thank you for registering with <b>Nafa Trader</b>.</p>
                    <p>Please verify your email address by clicking the link below:</p>
                    <p><a href='{verificationLink}'>Verify Email</a></p>
                    <p>If you did not request this, please ignore this email.</p>
                    <br/>
                    <p>Best regards,<br/>Nafa Trader Team</p>
                </body>
                </html>";
            await SendEmailAsync(to, subject, body);
        }
    }
}
