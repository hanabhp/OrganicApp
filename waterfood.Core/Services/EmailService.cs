using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Objects.Enums;
using waterfood.Core.Objects.Generals;
using waterfood.Core.Services.Interfaces;

namespace waterfood.Core.Services
{
    public class EmailService : IEmailService
    {
        public string SendRegisterCodeEmail(string email)
        {
            var subject = "WaterFood Registration";
            var message = "Your registration code is: ";
            var code = Utilities.Generators.CodeGenerator.GenerateUniqueCode();
            return SenEmail(email, code , subject , message);
        }
        public string SendForgotPasswordCodeEmail(string email)
        {
            var subject = "WaterFood Reset Password";
            var message = "Reset Password Code is: ";
            var code = Utilities.Generators.CodeGenerator.GenerateUniqueCode();
            return SenEmail(email, code, subject, message);
        }

        public string SenEmail(string email, string code , string sub , string message)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //create the mail message 
            MailMessage mail = new MailMessage();

            //set the addresses 
            mail.From = new MailAddress("feydisonfire@gmail.com"); //IMPORTANT: This must be same as your smtp authentication address.
            mail.To.Add(email);
            mail.Subject = sub;
           

            //set the content 
            mail.Body = message + code;
            mail.IsBodyHtml = false;
            //send the message 
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;

            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
            NetworkCredential Credentials = new NetworkCredential("feydisonfire@gmail.com", "gauhqesbhqxefxdi");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


            smtp.Credentials = Credentials;
            smtp.Port = int.Parse("587");    //alternative port number is 8889

            smtp.EnableSsl = true;
            smtp.Send(mail);

            return code;

        }
    }
}
