using System;
using System.Net;
using System.Net.Mail;
using ParkingProject.Models.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingProject.Models
{
    public class Program
    {
        public static bool SendGmail(string recipients)
        {
            DataServices ds = new DataServices();
            string password = ds.ReadPaswword(recipients);

            string subject = "Your Password";
            string content = "Your Password is " + password;
            string from = "CoParking@outlook.co.il";

            bool success = recipients != null;

            if (success)
            {
                SmtpClient gmailClient = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("CoParking@outlook.co.il", "I12345678!") //you need to add some valid gmail account credentials to authenticate with gmails SMTP server.
                };


                using (MailMessage gMessage = new MailMessage(from, recipients, subject, content))
                {
                        gMessage.To.Add(recipients);

                    try
                    {
                        gmailClient.Send(gMessage);
                        success = true;
                    }
                    catch (Exception) { success = false; }
                }
            }
            return success;
        }
    }
}