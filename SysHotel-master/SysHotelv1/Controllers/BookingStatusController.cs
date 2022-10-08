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

    public class BookingStatusController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();

        // GET: BookingStatus
        public ActionResult Index()
        {
            return View(db.BookingStatuses.ToList());
        }

        // GET: BookingStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingStatus bookingStatus = db.BookingStatuses.Find(id);
            if (bookingStatus == null)
            {
                return HttpNotFound();
            }
            return View(bookingStatus);
        }

        // GET: BookingStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DescriptionStatus")] BookingStatus bookingStatus)
        {
            if (ModelState.IsValid)
            {
                db.BookingStatuses.Add(bookingStatus);
                db.SaveChanges();
                Alert("Guardado con exito", Utilities.NotificationType.warning);
            }

            return View(bookingStatus);
        }

        // GET: BookingStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingStatus bookingStatus = db.BookingStatuses.Find(id);
            if (bookingStatus == null)
            {
                return HttpNotFound();
            }
            return View(bookingStatus);
        }

        // POST: BookingStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DescriptionStatus")] BookingStatus bookingStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingStatus).State = EntityState.Modified;
                db.SaveChanges();
                Alert("Guardado con exito", Utilities.NotificationType.warning);
            }
            return View(bookingStatus);
        }

        // GET: BookingStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingStatus bookingStatus = db.BookingStatuses.Find(id);
            if (bookingStatus == null)
            {
                return HttpNotFound();
            }
            return View(bookingStatus);
        }

        // POST: BookingStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingStatus bookingStatus = db.BookingStatuses.Find(id);
            db.BookingStatuses.Remove(bookingStatus);
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
