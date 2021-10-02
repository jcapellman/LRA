using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LRA.Services
{
    public class MailService
    {
        public class MailSettings
        {
            public string Username { get; set; }
            
            public string Password { get; set; }
            
            public int Port { get; set; }
            
            public string FromEmail { get; set; }

            public string Host { get; set; }
        }

        private readonly MailSettings _mailConfig;

        public MailService()
        {
            _mailConfig = new MailSettings();


        }

        public async Task<bool> SendEntryForm(string name, string emailAddress, string courseType)
        {
            var formEntry = $"Name: {name}<br/>" + $"Email Address: {emailAddress}<br/>" + $"Course: {courseType}";

            return await SendEmailAsync(_mailConfig.FromEmail, "New Course Registration", formEntry);
        }

        private async Task<bool> SendEmailAsync(string ToEmail, string Subject, string HTMLBody)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new();

            message.From = new MailAddress(_mailConfig.FromEmail);
            message.To.Add(new MailAddress(ToEmail));
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = HTMLBody;
            smtp.Port = _mailConfig.Port;
            smtp.Host = _mailConfig.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            await smtp.SendMailAsync(message);

            return true;
        }
    }
}