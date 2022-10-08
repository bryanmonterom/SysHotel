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

    public class BedTypesController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();

        // GET: BedTypes
        public ActionResult Index()
        {
            return View(db.BedType.ToList());
        }

        // GET: BedTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BedType bedType = db.BedType.Find(id);
            if (bedType == null)
            {
                return HttpNotFound();
            }
            return View(bedType);
        }

        // GET: BedTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BedTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,PricerPerBed")] BedType bedType)
        {
            if (ModelState.IsValid)
            {
                db.BedType.Add(bedType);
                db.SaveChanges();
                Alert("La operacion fue exitosa", Utilities.NotificationType.error);
                return View(bedType);

            }
            Alert("Algo fallo", Utilities.NotificationType.error);
            return View(bedType);
        }

        // GET: BedTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BedType bedType = db.BedType.Find(id);
            if (bedType == null)
            {
                return HttpNotFound();
            }
            return View(bedType);
        }

        // POST: BedTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,PricerPerBed")] BedType bedType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bedType).State = EntityState.Modified;
                db.SaveChanges();
                Alert("La operacion fue exitosa", Utilities.NotificationType.success);
                return View(bedType);

            }
            Alert("Algo fallo", Utilities.NotificationType.error);
            return View(bedType);
        }

        // GET: BedTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BedType bedType = db.BedType.Find(id);
            if (bedType == null)
            {
                return HttpNotFound();
            }
            return View(bedType);
        }

        // POST: BedTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BedType bedType = db.BedType.Find(id);
            db.BedType.Remove(bedType);
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
