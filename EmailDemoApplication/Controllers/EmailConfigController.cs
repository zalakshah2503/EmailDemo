using EmailDemoApplication.Models;
using Hangfire;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Http;

namespace EmailDemoApplication.Controllers
{
    public class EmailConfigController : ApiController
    {

        [HttpPost]
        public void PostMail(MailViewModel ConfigData)
        {
            BackgroundJob.Enqueue(() => SendMail(ConfigData));
        }

        [HttpGet]
        public string SendMail(MailViewModel ConfigData)
        {
            try
            {
                using (var smtp = new SmtpClient(ConfigData.HostName, ConfigData.PortNo))
                {
                    smtp.EnableSsl = ConfigData.enableSSl;
                    //smtp.UseDefaultCredentials = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(ConfigData.Email, ConfigData.Password);

                    using (var _mailMessage = new MailMessage())
                    {
                        if (!ConfigData.To.Contains(","))
                        {
                            _mailMessage.To.Add(new MailAddress(ConfigData.To));
                        }
                        else
                        {
                            foreach (string address in ConfigData.To.Split(','))
                            {
                                if (address != "" && address!=" ")
                                    _mailMessage.To.Add(new MailAddress(address));
                            }
                        }
                        _mailMessage.From = new MailAddress(ConfigData.Email);
                        _mailMessage.Subject = "Test mail";
                        _mailMessage.Body = ConfigData.Content;
                        _mailMessage.IsBodyHtml = true;

                        smtp.Send(_mailMessage);
                        return "Send";
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;

            }
        }
    }
}