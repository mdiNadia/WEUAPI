
using Application.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Services.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IApplicationDbContext context) : base()
        {

        }
        public Task SendEmailAsync(string email, string subject, string message)
        {

            try
            {
                MailMessage msg = new MailMessage();
                msg.Body = message;
                msg.BodyEncoding = Encoding.UTF8;
                msg.From = new MailAddress("nadiamditest@gmail.com", "بستینال سرویس", Encoding.UTF8);//باید از تنظیمات بیاد
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.Sender = msg.From;
                msg.Subject = subject;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.To.Add(new MailAddress(email, "گیرنده", Encoding.UTF8));

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("nadiamuhammadi080@gmail.com", "pass");//باید از تنظیمات بیاد

                smtp.Host = "smtp.gmail.com";//باید از تنظیمات بیاد
                smtp.Port = 25;//باید از تنظیمات بیاد
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                smtp.Send(msg);

                return Task.FromResult(0);
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}
