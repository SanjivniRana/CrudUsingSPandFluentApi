using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _31Oct.Models;
using System.Diagnostics;

namespace _31Oct.Controllers
{
    public class CustomerController : Controller
    {
        private EntitiesContext db = new EntitiesContext();
        public ActionResult FillCity(int state)
        {
            var cities = db.CCities.Where(c => c.StateId == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public CustomerController()
        {
            db.Database.Log = l => Debug.Write(l);
        }

        // GET: CustomerVMs
        public ActionResult Index()
        {
            return View(db.CustomrVMs.ToList());
        }

        // GET: CustomerVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomrVM customerVM = db.CustomrVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }

        // GET: CustomerVMs/Create
        public ActionResult Create()
        {
            ViewBag.StateList = db.CStates;
            var model = new CustomrVM();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomrVM customerVM)
        {
            if (ModelState.IsValid)
            {
                db.CustomrVMs.Add(customerVM);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.StateList = db.CStates;
            return View(customerVM);
        }

        // GET: CustomerVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomrVM customerVM = db.CustomrVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }

        // POST: CustomerVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomrVM customerVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerVM);
        }

        // GET: CustomerVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomrVM customerVM = db.CustomrVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }

        // POST: CustomerVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomrVM customerVM = db.CustomrVMs.Find(id);
            db.CustomrVMs.Remove(customerVM);
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
