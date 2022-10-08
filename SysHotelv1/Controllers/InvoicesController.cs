﻿using System;
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

    public class InvoicesController : Controller
    {
        private SysHotelDataContext db = new SysHotelDataContext();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.ReservationDetails);
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.ReservationDetailsId = new SelectList(db.ReservationDetails, "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReservationDetailsId,PriceAdult,PriceChild,DayCount,SeasonalDiscount,Total")] Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservationDetailsId = new SelectList(db.ReservationDetails, "Id", "Id", invoices.ReservationDetailsId);
            return View(invoices);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationDetailsId = new SelectList(db.ReservationDetails, "Id", "Id", invoices.ReservationDetailsId);
            return View(invoices);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReservationDetailsId,PriceAdult,PriceChild,DayCount,SeasonalDiscount,Total")] Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationDetailsId = new SelectList(db.ReservationDetails, "Id", "Id", invoices.ReservationDetailsId);
            return View(invoices);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoices invoices = db.Invoices.Find(id);
            db.Invoices.Remove(invoices);
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
