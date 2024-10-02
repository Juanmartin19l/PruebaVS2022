using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Store;

namespace Store.Controllers
{
    public class storesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: stores
        public ActionResult Index()
        {
            return View(db.stores.ToList());
        }

        // GET: stores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // GET: stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: stores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "store_id,store_name,phone,email,street,city,state,zip_code")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.stores.Add(stores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stores);
        }

        // GET: stores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: stores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_id,store_name,phone,email,street,city,state,zip_code")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stores);
        }

        // GET: stores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stores stores = db.stores.Find(id);
            db.stores.Remove(stores);
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
