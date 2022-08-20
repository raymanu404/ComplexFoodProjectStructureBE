using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Application.Components
{
    public static class SendEmailGrid
    {
        public static  async Task<int> SendMail(string mailFrom, string mailTo, string Subject, string body, string? nameTo = "", string? nameFrom = "Complex Food")
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient("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");
            var from = new EmailAddress(mailFrom, nameFrom);
            var subject = Subject;
            var to = new EmailAddress(mailTo, nameTo);
            var plainTextContent = body;
            var htmlContent = $"<strong>{plainTextContent}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return (int)response.StatusCode;
        }
    }
}
