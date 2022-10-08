using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysHotelv1.Models;

namespace SysHotelv1.Controllers
{
    [Authorize]
    public class PersonTypesController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();

        // GET: PersonTypes
        public ActionResult Index()
        {
            return View(db.PricesPerPeople.ToList());
        }

        // GET: PersonTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonType personType = db.PricesPerPeople.Find(id);
            if (personType == null)
            {
                return HttpNotFound();
            }
            return View(personType);
        }

        // GET: PersonTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Price")] PersonType personType)
        {
            if (ModelState.IsValid)
            {
                db.PricesPerPeople.Add(personType);
                db.SaveChanges();
                Alert("La operacion fue exitosa", Utilities.NotificationType.success);

            }

            return View(personType);
        }

        // GET: PersonTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonType personType = db.PricesPerPeople.Find(id);
            if (personType == null)
            {
                return HttpNotFound();
            }
            return View(personType);
        }

        // POST: PersonTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Price")] PersonType personType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personType).State = EntityState.Modified;
                db.SaveChanges();
                Alert("La operacion fue exitosa", Utilities.NotificationType.success);
                return View(personType);


            }
            Alert("Algo fallo", Utilities.NotificationType.error);
            return View(personType);
        }

        // GET: PersonTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonType personType = db.PricesPerPeople.Find(id);
            if (personType == null)
            {
                return HttpNotFound();
            }
            return View(personType);
        }

        // POST: PersonTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonType personType = db.PricesPerPeople.Find(id);
            db.PricesPerPeople.Remove(personType);
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
