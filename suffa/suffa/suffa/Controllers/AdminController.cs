using suffa.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace suffaTech.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        OurDbContext db = new OurDbContext();
        //Referanslar-Get
        [Route("Admin/Referanslar")]
        public ActionResult works()
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            var work = db.works.ToList();
            return View(work);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Referans Ekleme-Post
        public ActionResult worksadd(works work, HttpPostedFileBase foto)
        {
            works w = new works();
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            try
            {
                if (foto != null)
                {
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Image/worksImage/" + newfoto);
                    work.worksImage = "~/Image/worksImage/" + newfoto;
                    db.works.Add(work);
                    db.SaveChanges();

                    var wid = db.works.Where(x => x.worksName == work.worksName && x.worksImage == work.worksImage).FirstOrDefault();
                    process(wid.worksId, "works", "Eklendi");
                }
                return RedirectToAction("works", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        //Referans Silme-Get
        public ActionResult worksdelete(int id)
        {
            try
            {
                if (online() == false)
                {
                    return RedirectToAction("Login", "Security");
                }
                var usr = db.works.FirstOrDefault(x => x.worksId == id);
                System.IO.File.Delete(Server.MapPath(usr.worksImage));
                process(usr.worksId, "works", "Silindi");
                db.works.Remove(usr);
                db.SaveChanges();
                return RedirectToAction("works", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Admin/Hizmetler")]
        //Hizmetler-Get
        public ActionResult services()
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            return View(db.services.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Hizmet EKleme-Post
        public ActionResult servicesadd(services services)
        {
            try
            {
                if (online() == false)
                {
                    return RedirectToAction("Login", "Security");
                }
                db.services.Add(services);
                db.SaveChanges();
                var s = db.services.Where(x => x.servicesName == services.servicesName && x.servicesDesc == services.servicesDesc).FirstOrDefault();
                process(s.servicesId, "services", "Eklendi");
                return RedirectToAction("services", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        //Hizmet Silme-Get
        public ActionResult servicesdelete(int? id)
        {
            try
            {
                if (online() == false)
                {
                    return RedirectToAction("Login", "Security");
                }
                var usr = db.services.FirstOrDefault(x => x.servicesId == id);
                process(usr.servicesId,"services","Silindi");
                db.services.Remove(usr);
                db.SaveChanges();
                return RedirectToAction("services", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Admin/Hakkımızda")]
        //Hakkımızda-Get
        public ActionResult About()
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            var usr = db.abouts;
            if (usr != null)
            {
                return View(db.abouts.ToList());
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        //Hakkımızda Ekleme-Post
        public ActionResult Aboutadd(about about, int type)
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            try
            {
                string s = "";
                if (type == 1)
                {
                    about.aboutType = true;
                    s = "Misyon Eklendi";
                }
                else
                {
                    about.aboutType = false;
                    s = "Vizyon Eklendi";
                }
                db.abouts.Add(about);
                db.SaveChanges();
                var usr = db.abouts.Where(x => x.abouts == about.abouts && x.aboutType == about.aboutType).FirstOrDefault();
                process(usr.aboutId,"About",s);
                return RedirectToAction("About", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Hakkımızda Güncelleme-Post
        public ActionResult Aboutupdate(about ab, int type)
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            var a = db.abouts.FirstOrDefault(x => x.aboutId == ab.aboutId);
            a.abouts = ab.abouts;
            db.SaveChanges();
            process(a.aboutId,"About","Güncellendi");
            return RedirectToAction("About", "Admin");
        }
        [Route("Admin/Takım")]
        //Takım-Get
        public ActionResult Team()
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            string ro = Session["Role"].ToString();
            if (ro!="Patron")
            {
                return RedirectToAction("blog","Admin",new { id=1});
            }
            empview e = new empview();
            e.epm = db.employes.ToList();
            e.usr = db.user.ToList();
            return View(e);
        }
        [HttpPost]
        //Takım ekleme-Post
        public ActionResult Teamadd(HttpPostedFileBase foto, empview e)
        {//0-çalışan//1-stajyerlise//2-stajyerüni
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            try
            {
                string ro = Session["Role"].ToString();
                string s = "";
                if (ro == "Patron")
                {
                    if (foto != null)
                    {
                        user usr = new user();
                        usr.userEmail = e.userEmail;
                        usr.userName = e.userName;
                        usr.userPhone = e.userPhone;
                        int i = Convert.ToInt32(e.userRole);
                        if (i == 0)
                        {
                            usr.userRole = "Çalışan";
                            s = "Çalışan Eklendi";
                        }
                        else if (i == 1)
                        {
                            usr.userRole = "Stajyer-Lise";
                            s = "Stajyer-Lise Eklendi";
                        }
                        else
                        {
                            usr.userRole = "Stajyer-Üniversite";
                            s = "Stajyer-Üniversite Eklendi";
                        }
                        usr.userSurname = e.userSurname;
                        db.user.Add(usr);
                        db.SaveChanges();

                        var u = db.user.Where(x => x.userPhone == usr.userPhone && x.userEmail == usr.userEmail).FirstOrDefault();
                        process(u.userId,"user",s);
                        var newuser = db.user.Where(x => x.userEmail == e.userEmail && x.userPhone == e.userPhone).FirstOrDefault();
                        employe emp = new employe();

                        WebImage img = new WebImage(foto.InputStream);
                        FileInfo fotoinfo = new FileInfo(foto.FileName);
                        string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        img.Resize(600, 400);
                        img.Save("~/Image/employeImage/" + newfoto);
                        emp.employeImage = "~/Image/employeImage/" + newfoto;
                        emp.employePassword = e.employePassword;
                        emp.userId = newuser.userId;
                        db.employes.Add(emp);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Team", "Admin");
                }
                else
                {
                    return RedirectToAction("blog", "Admin", new { id = 1 });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        //Takım Silme-Get
        public ActionResult Teamdelete(int id)
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            try
            {
                var usr = db.employes.FirstOrDefault(x => x.employeId == id);
                int uid = Convert.ToInt32(usr.userId);
                process(uid, "User", "Silindi");
                db.employes.Remove(usr);
                System.IO.File.Delete(Server.MapPath(usr.employeImage));
                db.SaveChanges();
                return RedirectToAction("Team", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [Route("Admin/Blog")]
        //Blog-Get
        public ActionResult blog(int? id)
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            if (id != null)
            {
                ViewBag.mesaj = "Yetkiniz Yok";
            }
            blogviewmodel bv = new blogviewmodel();
            bv.blog = db.blogposts.ToList();
            bv.category = db.categories.ToList();
            return View(bv);
        }
        [HttpGet]
        //Blog Ekleme-Get
        public ActionResult blogadd()
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            blogviewmodel bv = new blogviewmodel();
            bv.category = db.categories.ToList();
            return View(bv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        //Blog Ekleme -Post
        public ActionResult blogadd(blogpost post, HttpPostedFileBase foto, blogviewmodel bvm)
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            try
            {
                WebImage img = new WebImage(foto.InputStream);
                FileInfo fotoinfo = new FileInfo(foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Image/blogImage/" + newfoto);
                post.postImagePath = "~/Image/blogImage/" + newfoto;
                post.categoryId = Convert.ToInt32(bvm.categorylist[0]);
                post.postDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                db.blogposts.Add(post);
                db.SaveChanges();
                var b = db.blogposts.Where(x=>x.postTitle==post.postTitle&&x.postImagePath==post.postImagePath&&x.postDescription==post.postDescription).FirstOrDefault();
                process(b.postid,"Blog","Eklendi");
                return RedirectToAction("blog", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        //Blog Sİlme-Get
        public ActionResult blogdelete(int? id)
        {
            try
            {
                if (online() == false)
                {
                    return RedirectToAction("Login", "Security");
                }
                var usr = db.blogposts.FirstOrDefault(x => x.postid == id);
                process(usr.postid,"Blog","Silindi");
                db.blogposts.Remove(usr);
                System.IO.File.Delete(Server.MapPath(usr.postImagePath));
                db.SaveChanges();
                return RedirectToAction("blog", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        [HttpPost]
        //Kategori Ekleme-Post
        public ActionResult categoryadd(category category)
        {
            try
            {
                if (category.categories != null)
                {
                    if (online() == false)
                    {
                        return RedirectToAction("Login", "Security");
                    }
                    db.categories.Add(category);
                    db.SaveChanges();
                    var c = db.categories.Where(x=>x.categories==category.categories).FirstOrDefault();
                    process(c.categoryId,"category","Eklendi");

                }
                return RedirectToAction("Blog", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        //Proje-Post
        public ActionResult project()
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            return View(db.project.ToList());
        }
        [HttpPost]
        //Proje Ekleme-Post
        public ActionResult projectadd(project p, HttpPostedFileBase foto)
        {
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            try
            {
                if (foto != null)
                {
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Image/projectImage/" + newfoto);
                    p.projectImagePath = "~/Image/projectImage/" + newfoto;
                    db.project.Add(p);
                    db.SaveChanges();

                    var pro = db.project.Where(x => x.projectName == p.projectName && x.projectDescrption == p.projectDescrption).FirstOrDefault();
                    process(pro.projectId,"project","Eklendi");
                }
                return RedirectToAction("project", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }

        }
        //Proje Silme-Get
        public ActionResult projectdelete(int id)
        {
            try
            {
                if (online() == false)
                {
                    return RedirectToAction("Login", "Security");
                }
                var usr = db.project.FirstOrDefault(x => x.projectId == id);
                process(usr.projectId,"project","Silindi");
                db.project.Remove(usr);
                System.IO.File.Delete(Server.MapPath(usr.projectImagePath));
                db.SaveChanges();
                return RedirectToAction("project", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ex = ex });
            }
        }
        public ActionResult pro()
        {
            string ro = Session["Role"].ToString();
            if (ro!="Patron")
            {
                return RedirectToAction("blog","Admin",new { id=1});
            }
            if (online() == false)
            {
                return RedirectToAction("Login", "Security");
            }
            processview pv = new processview();
            pv.process = db.process.ToList();
            pv.user = db.user.ToList();
            pv.employe = db.employes.ToList();
            return View(pv);
        }
        //Çıkış-Get
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Online Kontol-Get
        public bool online()
        {
            if (Session["employeName"] != null)
            {
                string nick = Session["employeName"].ToString();
                var usr = db.user.FirstOrDefault(x => x.userName == nick);
                Session["employeName"] = usr.userName.ToString();
                Session["Role"] = usr.userRole.ToString();
                Session.Timeout = 10;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void process(int id, string name, string type)
        {
            process p = new process();
            int empid = Convert.ToInt32(Session["employeId"]);
            p.employeId = empid;
            p.procesName = name;
            p.id = id;
            p.processType = type;
            p.processTime = DateTime.Now;
            db.process.Add(p);
            db.SaveChanges();
        }    
    }
}