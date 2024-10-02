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
    public class brandsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: brands
        public ActionResult Index()
        {
            return View(db.brands.ToList());
        }

        // GET: brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brands brands = db.brands.Find(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // GET: brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "brand_id,brand_name")] brands brands)
        {
            if (ModelState.IsValid)
            {
                db.brands.Add(brands);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brands);
        }

        // GET: brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brands brands = db.brands.Find(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "brand_id,brand_name")] brands brands)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brands).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brands);
        }

        // GET: brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brands brands = db.brands.Find(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            brands brands = db.brands.Find(id);
            db.brands.Remove(brands);
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

        public ActionResult Search(string query)
        {
            var results = db.brands
                            .Where(b => b.brand_name.Contains(query) || string.IsNullOrEmpty(query))
                            .ToList();

            return PartialView("_BrandsTable", results);
        }

        public ActionResult Sort(string column, string direction)
        {
            IQueryable<brands> brands = db.brands;

            // Ordenar según la columna seleccionada
            switch (column)
            {
                case "brand_name":
                    brands = direction == "asc" ? brands.OrderBy(b => b.brand_name) : brands.OrderByDescending(b => b.brand_name);
                    break;
                default:
                    brands = direction == "asc" ? brands.OrderBy(b => b.brand_id) : brands.OrderByDescending(b => b.brand_id);
                    break;
            }

            // Retornar los resultados ordenados como una vista parcial para AJAX
            return PartialView("_BrandsTable", brands.ToList());
        }
    }
}
