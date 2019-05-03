//using System;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System.Threading.Tasks;

//namespace ReadComic.Common
//{
//    internal class SendMail
//    {
//        private static void Main()
//        {
//            Execute().Wait();
//        }

//        static async Task Execute()
//        {
//            var apiKey = Environment.GetEnvironmentVariable("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");
//            var client = new SendGridClient(apiKey);
//            var from = new EmailAddress("truyendatk@gmail.com", "TruyenDa");
//            var subject = "Thay đổi mật khẩ";
//            var to = new EmailAddress("minhhoang97hk@gmail.com");
//            var plainTextContent = "and easy to do anywhere, even with C#";
//            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//            var response = await client.SendEmailAsync(msg);
//        }

//        static async Task Execute(string toEmail,string Subject)
//        {
//            var apiKey = Environment.GetEnvironmentVariable("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");
//            var client = new SendGridClient(apiKey);
//            var from = new EmailAddress("truyendatk@gmail.com", "TruyenDa");
//            var subject = "Thay đổi mật khẩ";
//            var to = new EmailAddress("minhhoang97hk@gmail.com");
//            var plainTextContent = "and easy to do anywhere, even with C#";
//            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//            var response = await client.SendEmailAsync(msg);
//        }

//    }
//}