using suffa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace suffa.Controllers
{
    public class HomeController : Controller
    {
        OurDbContext db = new OurDbContext();
        public ActionResult Index()
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
        public ActionResult allblog(int? id)
        {
            try
            {
                homeındexview hm = new homeındexview();
                if (id == null)
                {
                    hm.blogposts = db.blogposts.ToList().Take(6);
                }
                else
                {
                    hm.blogposts = db.blogposts.Where(x => x.categoryId == id).ToList().Take(6);
                }
                hm.categories = db.categories.ToList();
                hm.post = db.blogposts.ToList();
                hm.abouts = db.abouts.ToList();
                hm.services = db.services.ToList();
                return View(hm);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [Route("Anasayfa/Blog/BlogDetay/{title}")]
        public ActionResult singleblog(int? id,string title)
        {
            try
            {
                homeındexview hm = new homeındexview();
                if (id==null)
                {
                    return RedirectToAction("allblog", "Home");
                }
                else
                {
                    hm.blogposts = db.blogposts.Where(x => x.postid == id).ToList();
                    hm.post = db.blogposts.Where(x=>x.postid!=id).ToList();
                    hm.categories = db.categories.ToList();
                    hm.abouts = db.abouts.ToList();
                    hm.services = db.services.ToList();
                    return View(hm);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home",new { ex=ex});
            }
        }
        public ActionResult bloglike(int id)
        {
            try
            {
                var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                var post = db.blogposts.Where(x => x.postid == id).FirstOrDefault();
                post.postLike = post.postLike + 1;
                db.SaveChanges();
                return RedirectToAction("singleblog", "Home", new { id = id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home",new { ex=ex});
            }
        }
        [Route("Home/Error")]
        public ActionResult Error(Exception ex)
        {
            ViewBag.hata = ex.ToString();
            return View();
        }
        [Route("Anasayfa/Başvurular")]
        public ActionResult request(int? id)
        {
            try
            {
                homeındexview hv = new homeındexview();
                hv.abouts = db.abouts.ToList();
                hv.services = db.services.ToList();
                if (id!=null)
                {
                    ViewBag.mesaj = "İşlem";
                }
                return View(hv);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Anasayfa/Servisler")]
        public ActionResult services()
        {
            try
            {
                homeındexview hv = new homeındexview();
                hv.abouts = db.abouts.ToList();
                hv.services = db.services.ToList();
                return View(hv);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
            }
        [Route("Anasayfa/Referanslar")]
        public ActionResult works()
        {
            try
            {
            homeındexview hv = new homeındexview();
            hv.abouts = db.abouts.ToList();
            hv.services = db.services.ToList();
            hv.works = db.works.ToList();
            return View(hv);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Anasayfa/Hakkımızda")]
        public ActionResult about()
        {
            try
            {
            homeındexview hv = new homeındexview();
            hv.abouts = db.abouts.ToList();
            hv.services = db.services.ToList();
            return View(hv);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Anasayfa/Ekip")]
        public ActionResult team()
        {
            try
            {
            homeındexview hv = new homeındexview();
            hv.abouts = db.abouts.ToList();
            hv.employes = db.employes.ToList();
            hv.services = db.services.ToList();
            hv.user = db.user.ToList();
            return View(hv);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Anasayfa/Projeler")]
        public ActionResult project()
        {
            try
            {
            homeındexview hv = new homeındexview();
            hv.abouts = db.abouts.ToList();
            hv.services = db.services.ToList();
            hv.project = db.project.ToList();
            return View(hv);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Anasayfa/Proje/Proje-Detay/{title}")]
        public ActionResult singleproject(int? id,string title)
        {
            try
            {
            homeındexview hv = new homeındexview();
            hv.abouts = db.abouts.ToList();
            hv.services = db.services.ToList();
            hv.project = db.project.Where(x=>x.projectId==id).ToList();
            hv.pro = db.project.Where(x=>x.projectId!=id).ToList();
            return View(hv);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
    }
}