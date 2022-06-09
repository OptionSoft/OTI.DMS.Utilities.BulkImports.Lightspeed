using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RawImport_Lightspeed
{
    public static class EmailHandler
    {
        private static MailAddress From
        {
            get
            {
                return new MailAddress("RawImport_Lightspeed@otiservices.com");
            }
        }

        private static List<string> To
        {
            get
            {
                return new List<string>() { "dmsdata@otiservices.com" };
            }
        }

        private static List<string> Credentials
        {
            get
            {
                return new List<string>() { "noreply@otiservices.com", "otimail55%" };
            }
        }

        private static List<string> ParseEmails(string data, List<string> defaults)
        {
            try
            {
                return data.Split(';').ToList();
            }
            catch
            {
                return defaults;
            }
        }

        public static void SendEmail(string emailSubject, string emailBody)
        {
            try
            {
                MailMessage mailMsg = new MailMessage();
                SmtpClient sc = new SmtpClient();

                //set all the involved parties from the config information
                mailMsg.From = From;

                foreach (var t in To)
                {
                    mailMsg.To.Add(t);
                }

                //setting IsBodyHtml to true allows me to format all of this with bolding and the "pre" tags to make it look all spiffy and readable
                mailMsg.IsBodyHtml = true;
                mailMsg.Subject = emailSubject;
                mailMsg.Body = emailBody;

                //set the information needed to connect and send the email
                sc.Host = "smtp.mailgun.org";
                sc.Port = 587;
                var creds = Credentials;
                sc.Credentials = new System.Net.NetworkCredential(creds[0], creds[1]);
                sc.EnableSsl = false;

                sc.Send(mailMsg);
            }
            catch (Exception ex)
            {
                var ie = ex.InnerException;
                //well, i tried
            }
        }
    }
}
