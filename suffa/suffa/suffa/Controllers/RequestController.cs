using suffa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace suffa.Controllers
{
    public class RequestController : Controller
    {
        OurDbContext db = new OurDbContext();
        public ActionResult contact(contact c,string type,string typeIntern)
        {
            try
            {
                if (typeIntern!=null)
                {
                    if (typeIntern=="0")
                    {
                        type = "Stajyer-Üniversite";
                    }
                    else
                    {
                        type = "Stajyer-Lise";
                    }
                }
                string mesaj = c.Name +c.Surname+ Environment.NewLine + c.Phone+Environment.NewLine+c.messages;
                MailSender(mesaj,c.Email,type);
                return RedirectToAction("request","Home",new { id=1});
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error","Home",new { ex=ex});
            }
        }
        public static void MailSender(string body,string email,string type)
        {
            var fromAddress = new MailAddress("kanbagis66@gmail.com");
            var toAddress = new MailAddress(email);
            string subject = "Site Adı | Sitenizden Gelen"+"   "+ type+ "   "+"İletişim Formu";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "kan123qwe")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}