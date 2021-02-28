using PersonelMvcUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelMvcUI.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        // GET: Security
       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult Login(Kullanıcı kullanıcı)
        {
            var user = db.Kullanıcı.FirstOrDefault(x => x.Ad == kullanıcı.Ad && x.Sifre==kullanıcı.Sifre);
            if (user == null)
            {
                ViewBag.Message = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Ad,false);
                return RedirectToAction("Index", "Departman");
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}