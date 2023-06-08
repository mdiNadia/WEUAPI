using System;
using System.Linq;

namespace Application.Services.Sms
{
    public class SendSmsData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Text { get; set; }
        public string SenderNumber { get; set; }
        public SendSmsData()
        {
            this.Username = "9127794361";
            this.Password = "7BH5$";
            this.Text = "سلام ما بستینال هستیم کد شما: ";
            this.SenderNumber = "50004000780779";
        }

    }
}
