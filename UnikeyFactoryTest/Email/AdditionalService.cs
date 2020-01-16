using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class AdditionalService
    {
        public void SendMail(string user)
        {
            var fromAddress = new MailAddress("unikeyfactory@gmail.com");
            var toAddress = new MailAddress(user);
            const string fromPassword = "Unikey01!";
            const string subject = "Assestement";

            StringBuilder myStringBuilder = new StringBuilder(ReadFile(@"C:\CodiceHtml.txt"));
            myStringBuilder.Replace("NomeUtente", "Lorenzo").Replace("URLTest", @"http://localhost:99/ExTest\\TestStart?guid=c9b82a6b-d08a-431d-9082-caafebce972e").ToString();
            string body = myStringBuilder.ToString();
            
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
                

        }

        public string ReadFile(string path)
        {
            if (!File.Exists(path))
                throw new Exception("Path not valids");
            StreamReader myReader = new StreamReader(path);
            var readed = myReader.ReadToEnd();
            return readed;
        }
    }
}
