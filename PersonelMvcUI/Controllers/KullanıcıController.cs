using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonelMvcUI.Models.EntityFramework;

namespace PersonelMvcUI.Controllers
{
    [Authorize(Roles = "A")]
    public class KullanıcıController : Controller
    {
        private PersonelDbEntities db = new PersonelDbEntities();

        // GET: Kullanıcı
        public ActionResult Index()
        {
            return View(db.Kullanıcı.ToList());
        }

        // GET: Kullanıcı/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanıcı kullanıcı = db.Kullanıcı.Find(id);
            if (kullanıcı == null)
            {
                return HttpNotFound();
            }
            return View(kullanıcı);
        }

        // GET: Kullanıcı/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanıcı/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Ad,Sifre,Role")] Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                db.Kullanıcı.Add(kullanıcı);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kullanıcı);
        }

        // GET: Kullanıcı/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanıcı kullanıcı = db.Kullanıcı.Find(id);
            if (kullanıcı == null)
            {
                return HttpNotFound();
            }
            return View(kullanıcı);
        }

        // POST: Kullanıcı/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Ad,Sifre,Role")] Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kullanıcı).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kullanıcı);
        }

        // GET: Kullanıcı/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanıcı kullanıcı = db.Kullanıcı.Find(id);
            if (kullanıcı == null)
            {
                return HttpNotFound();
            }
            return View(kullanıcı);
        }

        // POST: Kullanıcı/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanıcı kullanıcı = db.Kullanıcı.Find(id);
            db.Kullanıcı.Remove(kullanıcı);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
