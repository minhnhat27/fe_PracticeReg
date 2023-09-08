using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DangKyPhongThucHanhCNTT.Services
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class SendMailService : IEmailSender
    {
        private readonly MailSettings _settings;
        private readonly ILogger<SendMailService> _logger;

        public SendMailService(IOptions<MailSettings> settings, ILogger<SendMailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
            _logger.LogInformation("Creation SendMailService");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_settings.DisplayName, _settings.Mail);
            message.From.Add(new MailboxAddress(_settings.DisplayName, _settings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_settings.Mail, _settings.Password);
                await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                Directory.CreateDirectory("MailsSave");
                var emailsavefile = string.Format(@"MailsSave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailsavefile);

                _logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                _logger.LogError(ex.Message);
            }
            smtp.Disconnect(true);
            _logger.LogInformation("Send mail to: " + email);
        }
    }
}
