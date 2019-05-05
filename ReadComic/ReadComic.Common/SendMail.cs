using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace ReadComic.Common
{
    public class SendMail
    {
        

        public static bool Send(string toEmail,string body,string Subject)
        {
            try
            {
                HeThong heThong = new HeThong();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.EnableSsl = true;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 25;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(
                    heThong.email,
                    heThong.matkhau);
                var msg = new MailMessage
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    From = new MailAddress(
                        heThong.email),
                    Subject = Subject,
                    Body = body,
                    Priority = MailPriority.Normal,
                };
                msg.To.Add(toEmail);
                smtpClient.Send(msg);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task SendGird(string toEmail, string body,string Subject)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("truyendatk@gmail.com", "Truyen da");
            var subject = Subject;
            var to = new EmailAddress(toEmail);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}