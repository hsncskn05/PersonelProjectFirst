using PersonelMvcUI.Models.EntityFramework;
using PersonelMvcUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelMvcUI.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        // GET: Personel
        [Authorize(Roles = "A,U")]
        public ActionResult Index()
        {
            var model = db.Personel.ToList();
            return View(model);
        }
       
        public ActionResult Yeni()
        {
            PersonelFormViewModel model = new PersonelFormViewModel();
            model.Departmanlar = db.Departman.ToList();
            model.Personel = new Personel();
            return View("PersonelForm",model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                };
                return View("PersonelForm",model);
            }
            if (personel.Id == 0)
            {
                db.Personel.Add(personel);
            }
            else
            {
                db.Entry(personel).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel() { 
            Departmanlar=db.Departman.ToList(),
            Personel=db.Personel.Find(id)
            };

            return View("PersonelForm",model);
        }

        public ActionResult Sil(int id)
        {
            var personel = db.Personel.Find(id);
            db.Personel.Remove(personel);
            if (personel == null)
                return HttpNotFound();

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x => x.DepartmanId == id).ToList();
            return PartialView(model);
        }
    }
}