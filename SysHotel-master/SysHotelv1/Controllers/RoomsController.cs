using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysHotelv1.Models;
using SysHotelv1.Utilities;


namespace SysHotelv1.Controllers
{
    [Authorize]

    public class RoomsController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.BedType).Include(r => r.RoomType);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.BedTypeId = new SelectList(db.BedType, "Id", "Description");
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "Description");
            ViewBag.BuildingId = new SelectList(db.Building, "Id", "BuildingName");

            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomTypeId,BedTypeId,RoomNumber,BuildingId")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(rooms);
                db.SaveChanges();
                Alert("Operacion exitosa", NotificationType.success);
            }

            ViewBag.BedTypeId = new SelectList(db.BedType, "Id", "Description", rooms.BedTypeId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "Description", rooms.RoomTypeId);
            ViewBag.BuildingId = new SelectList(db.Building, "Id", "BuildingName",rooms.BuildingId);

            return View(rooms);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            ViewBag.BedTypeId = new SelectList(db.BedType, "Id", "Description", rooms.BedTypeId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "Description", rooms.RoomTypeId);
            ViewBag.BuildingId = new SelectList(db.Building, "Id", "BuildingName");
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomTypeId,BedTypeId,RoomNumber,BuildingId")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rooms).State = EntityState.Modified;
                db.SaveChanges();
                Alert("Operacion exitosa", NotificationType.success);
            }
            ViewBag.BedTypeId = new SelectList(db.BedType, "Id", "Description", rooms.BedTypeId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "Description", rooms.RoomTypeId);
            ViewBag.BuildingId = new SelectList(db.Building, "Id", "BuildingName",rooms.BuildingId);

            return View(rooms);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rooms rooms = db.Rooms.Find(id);
            db.Rooms.Remove(rooms);
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
