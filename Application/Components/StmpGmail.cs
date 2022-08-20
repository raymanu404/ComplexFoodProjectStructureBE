using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Application.Components
{
    public static class StmpGmail
    {
        public static string SendMail(string mailFrom, string password, string mailTo, string subject, string body, string? nameTo= "", string? nameFrom = "Complex Food")
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(nameFrom, mailFrom));
            mailMessage.To.Add(new MailboxAddress(nameTo, mailTo));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = body,
                
            };
            string resultStatus = "";
            using (var smtpClient = new SmtpClient())
            {

                //smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Connect("smtp.gmail.com", 587, false);
                
                smtpClient.Authenticate(mailFrom, password);
                smtpClient.Send(mailMessage);
                resultStatus = smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);               

            }

            return resultStatus.Split(" ")[1];
        }
    }
}
