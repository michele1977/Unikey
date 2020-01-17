using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace UnikeyFactoryTest.Service.Providers.MailProvider
{
    public class MailProvider : IMailProvider
    {
        public bool SendMail(string user, string UserName, string URL)
        {
            var mailtools = new MailTools();
            var path = HttpContext.Current.Server.MapPath("/Templates") + @"\MailTemplate\CodiceHtml.txt";
            StringBuilder myStringBuilder = new StringBuilder(mailtools.ReadFile(path));
            myStringBuilder.Replace("NomeUtente", UserName).Replace("URLTest", URL).ToString();
            var body = myStringBuilder.ToString();


            try
            {
                var fromAddress = new MailAddress("unikeyfactory@gmail.com");
                var toAddress = new MailAddress(user);
                const string fromPassword = "Unikey01!";
                const string subject = "Assestement";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return true;

            }
            catch 
            {
                return false;
            }



        }
    }
}
