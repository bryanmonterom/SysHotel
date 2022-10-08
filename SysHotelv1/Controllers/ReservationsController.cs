using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SysHotelv1.Models;

namespace SysHotelv1.Controllers
{
    [Authorize]
    public class ReservationsController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();
        SysHotelv1.Models.SysHotelBusiness business = new Models.SysHotelBusiness();


        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.BookingStatus).Include(r => r.Clients);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus");
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName");
            ViewBag.IdEmployee = new SelectList(db.Employee, "Id", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,RoomNumber,AllInclusive,CheckIn,CheckOut,DaysNumber,BookingStatusId, IdEmployee")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (reservation.CheckIn >= reservation.CheckOut)
                {
                    Alert("Error en las fechas, las fechas o iguales o la fecha de checkin es mayor que la de checkout", Utilities.NotificationType.warning);
                    ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", reservation.BookingStatusId);
                    ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", reservation.ClientId);
                    ViewBag.IdEmployee = new SelectList(db.Employee, "Id", "Name", reservation.IdEmployee);

                    return View(reservation);

                }
               

                string message;
                db.Reservations.Add(reservation);
                try
                {
                    if (business.GetRoomsAvailable(reservation.CheckIn, reservation.CheckOut).Count !=0)
                    {
                        db.SaveChanges();
                        message = "Todo bien";
                        Alert(message, Utilities.NotificationType.success);
                        return RedirectToAction("Create", "ReservationDetails", new {id=reservation.Id});

                    }
                    message = Utilities.Utilities.ErrorHandling("", "No hay habitaciones disponibles para esta fecha", "", Utilities.ErrorsCode.CustomError);
                    Alert(message, Utilities.NotificationType.error);

                }
                catch (Exception e)
                {
                     message = Utilities.Utilities.ErrorHandling("", e.Message + e.InnerException, "", Utilities.ErrorsCode.AnErrorOcurred);
                    Alert(message, Utilities.NotificationType.error);

                }
            }

            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", reservation.BookingStatusId);
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", reservation.ClientId);
            ViewBag.IdEmployee = new SelectList(db.Employee, "Id", "Name",reservation.IdEmployee);

            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", reservation.BookingStatusId);
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", reservation.ClientId);
            ViewBag.IdEmployee = new SelectList(db.Employee, "Id", "Name", reservation.IdEmployee);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId,RoomNumber,AllInclusive,CheckIn,CheckOut,DaysNumber,BookingStatusId, IdEmployee")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (business.GetRoomsAvailable(reservation.CheckIn, reservation.CheckOut).Count != 0)
                    {
                        db.Entry(reservation).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.Error = "No hay habitaciones disponibles para esta fecha";

                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message + e.InnerException;
                }

            }
            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", reservation.BookingStatusId);
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", reservation.ClientId);
            ViewBag.IdEmployee = new SelectList(db.Employee, "Id", "Name", reservation.IdEmployee);

            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Rooms([Bind(Include = "CheckIn, CheckOut")] DateTime? checkIn, DateTime? CheckOut)
        {
            List<Rooms> rooms = new List<Rooms>();
            if (checkIn == null || CheckOut == null)
            {

                string message = Utilities.Utilities.ErrorHandling("","El campo de fecha no debe ser nulo","",Utilities.ErrorsCode. CustomError);
                ViewBag.Error = Alert(message, Utilities.NotificationType.warning);
            }
            else
            {
                rooms = business.GetRoomsAvailable(checkIn, CheckOut);
                if (rooms.Count == 0)
                {
                    string message = Utilities.Utilities.ErrorHandling("Habitaciones", "", "", Utilities.ErrorsCode.NotFound);
                    ViewBag.Error = Alert(message, Utilities.NotificationType.error);
                }

            }
            return PartialView("AvailableRooms", rooms);

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
