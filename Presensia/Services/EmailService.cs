using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Presensia.Services
{
    public class SmtpOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }

    public interface IEmailService
    {
        Task SendAsync(string subject, string body, string? replyTo = null);
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _opts;

        public EmailService(IOptions<SmtpOptions> opts) => _opts = opts.Value;

        public async Task SendAsync(string subject, string body, string? replyTo = null)
        {
            using var client = new SmtpClient(_opts.Host, _opts.Port)
            {
                EnableSsl = _opts.EnableSsl,
                Credentials = new NetworkCredential(_opts.User, _opts.Password)
            };

            var msg = new MailMessage
            {
                From = new MailAddress(_opts.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            msg.To.Add(_opts.To);
            if (!string.IsNullOrWhiteSpace(replyTo))
                msg.ReplyToList.Add(new MailAddress(replyTo!));

            await client.SendMailAsync(msg);
        }
    }
}