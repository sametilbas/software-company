using suffa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace suffa.Controllers
{
    public class SharedController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: Shared
        public ActionResult _Layout()
        {
            homeındexview hv = new homeındexview();
            hv.abouts = db.abouts.ToList();
            hv.blogposts = db.blogposts.Take(6).ToList();
            hv.categories = db.categories.ToList();
            hv.employes = db.employes.ToList();
            hv.services = db.services.ToList();
            hv.works = db.works.ToList();
            return View(hv);
        }
    }
}