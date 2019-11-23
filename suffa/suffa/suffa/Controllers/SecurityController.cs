using suffa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace suffa.Controllers
{
    public class SecurityController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        OurDbContext db = new OurDbContext();
        [HttpPost]
        public ActionResult Login(string nick,string password)
        {
            var usr = db.user.FirstOrDefault(x => x.userName == nick);
            if (usr != null)
            {
                var emp = db.employes.FirstOrDefault(x => x.employePassword == password);
                if (emp!=null)
                {
                    Session["employeName"] = usr.userName.ToString();
                    Session["employeId"] = usr.userId.ToString();
                    Session["Role"] = usr.userRole.ToString();
                    Session.Timeout = 10;
                    return RedirectToAction("blog", "Admin");
                }
                else
                {
                    ViewBag.mesaj = "Kullanıcı Adı veya Şifre Hatalı";
                    return View();
                }
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı Adı veya Şifre Hatalı";
                return View();
            }
        }
    }
}