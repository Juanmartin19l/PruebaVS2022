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
    public class categoriesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: categories
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        // GET: categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categories category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,category_name")] categories category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categories category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,category_name")] categories category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            categories category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categories category = db.categories.Find(id);
            db.categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Método para buscar categorías
        public ActionResult Search(string query)
        {
            // Filtrar las categorías cuyo nombre contenga el texto de búsqueda
            var results = db.categories
                            .Where(c => c.category_name.Contains(query) || string.IsNullOrEmpty(query))
                            .ToList();

            // Devolver una vista parcial con los resultados filtrados
            return PartialView("_CategoriesTable", results);
        }

        // Método para ordenar categorías
        public ActionResult Sort(string column, string direction)
        {
            IQueryable<categories> categories = db.categories;

            // Ordenar según la columna y dirección
            switch (column.ToLower())
            {
                case "category_name":
                    categories = direction.ToLower() == "asc" ?
                        categories.OrderBy(c => c.category_name) :
                        categories.OrderByDescending(c => c.category_name);
                    break;
                default:
                    categories = categories.OrderBy(c => c.category_id); // Por defecto, ordenar por ID
                    break;
            }

            return PartialView("_CategoriesTable", categories.ToList());
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
