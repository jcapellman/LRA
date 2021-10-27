using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LRA.Services
{
    public class MailService
    {
        private const string FILENAME_SETTINGS = "settings.json";

        public class MailSettings
        {
            public string Username { get; set; }
            
            public string Password { get; set; }
            
            public int Port { get; set; }
            
            public string FromEmail { get; set; }

            public string Host { get; set; }
        }

        private readonly MailSettings _mailConfig = new();

        private readonly bool _initialized = false;

        public MailService()
        {
            var settingsFile = Path.Combine(AppContext.BaseDirectory, FILENAME_SETTINGS);

            if (!File.Exists(settingsFile))
            {
                return;
            }

            var json = File.ReadAllText(settingsFile);

            var result = System.Text.Json.JsonSerializer.Deserialize<MailSettings>(json);

            if (result == null)
            {
                return;
            }

            _mailConfig = result;

            _initialized = true;
        }

        public async Task<bool> SendEntryForm(string name, string emailAddress, string courseType, string phoneNumber, string preferredMethod)
        {
            if (!_initialized)
            {
                return false;
            }

            var formEntry = $"Name: {name}<br/>" + $"Email Address: {emailAddress}<br/>" + $"Phone Number: {phoneNumber}<br/>" + $"Course: {courseType}<br/><br/>" +
                $"Preferred Method: {preferredMethod}";

            return await SendEmailAsync(_mailConfig.FromEmail, "New Course Registration", formEntry);
        }

        private async Task<bool> SendEmailAsync(string ToEmail, string Subject, string HTMLBody)
        {
            MailMessage message = new();
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