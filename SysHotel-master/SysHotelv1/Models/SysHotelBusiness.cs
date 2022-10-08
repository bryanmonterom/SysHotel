using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysHotelv1.Models
{
    public class SysHotelBusiness
    {
        SysHotelDataContext db = new SysHotelDataContext();
        public List<Rooms> GetRoomsAvailable(DateTime? entranceDate, DateTime? outDate)
        {
           // var itemIds = inMemoryList.Select(x => x.Id).ToArray();
           //var otherObjects = context.ItemList.Where(x => !itemIds.Contains(x.Id));
           var roomsNotAvailable = GetRoomsNotAvailable(entranceDate, outDate);
           var allRooms = db.Rooms.Include("BedType").Include("RoomType").ToList();

            foreach (var item in roomsNotAvailable)
            {
                var roomToRemove = allRooms.Find(a => a.Id == item.Id);
                allRooms.Remove(roomToRemove);
            }


            //var roomsAvailable = allRooms.Except(roomsNotAvailable).ToList();


            return allRooms;
        }
        /// <summary>
        /// All the rooms that has a reservation according to reservation dates
        /// </summary>
        /// <param name="EntranceDate"></param>
        /// <param name="outDate"></param>
        /// <returns></returns>
        public List<Rooms> GetRoomsNotAvailable(DateTime? entranceDate, DateTime? outDate)
        {
            var query = (from rooms in db.Rooms
                         join rdetails in db.ReservationDetails on rooms.Id equals rdetails.RoomId
                         join r in db.Reservations on rdetails.ReservationId equals r.Id
                         where r.CheckIn >= entranceDate && r.CheckOut <= outDate
                         select new 
                         {
                             id = rooms.Id,
                             roomType = rooms.RoomType,
                             bedType = rooms.BedType,
                             bedTypeId = rooms.BedTypeId,
                             roomTypeId = rooms.RoomTypeId
                         }).ToList().Select(x=> new Rooms {Id = x.id, BedType = x.bedType , RoomType = x.roomType, RoomTypeId = x.roomTypeId, BedTypeId = x.bedTypeId }).ToList();

            return query;
        }

        public decimal GetTotalForReservation(int reservationId, DateTime entranceDate, DateTime outDate)
        {
            decimal totalforChildrens;
            decimal totalforAdults;
            decimal totalForRoom;
            decimal reservationTotal = 0;
            double days = outDate.Subtract(entranceDate).TotalDays;
            var listOfRooms = db.ReservationDetails.Where(a => a.ReservationId == reservationId).ToList();
            foreach (var rooms in listOfRooms)
            {
                totalforChildrens = GetTotalForPerson(rooms.ChildQty, Utilities.PersonTypes.Childrens);
                totalforAdults = GetTotalForPerson(rooms.AdultQty, Utilities.PersonTypes.Adults);
                totalForRoom = GetTotalForRooms(rooms.RoomId, totalforChildrens, totalforAdults);
                if (rooms.Reservation.AllInclusive == true)
                {
                    reservationTotal += (totalforChildrens + totalforAdults + totalForRoom)*(decimal)(0.05);
                }
                reservationTotal += totalforChildrens + totalforAdults + totalForRoom;

            }

            return reservationTotal*Convert.ToDecimal(days);
        }

        public decimal GetTotalForPerson(int qty, Utilities.PersonTypes personTypes)
        {
            decimal pricePerPerson = db.PricesPerPeople.Where(a => a.Id == (int)(personTypes))
                                        .Select(b => b.Price).FirstOrDefault();
            return pricePerPerson * qty;
        }

        public decimal GetTotalForRooms(int idRoom, decimal totalForAdults, decimal totalForChildrens)
        {
            var Room = db.Rooms.Include("BedType").Include("RoomType").Where(a => a.Id == idRoom).FirstOrDefault();
            decimal totalForRoom = Room.BedType.PricerPerBed*(totalForAdults+totalForChildrens) + Room.RoomType.PricePerRoom * (totalForAdults + totalForChildrens);
            return totalForRoom;
        }
        
        public decimal GetPricePerPerson(Utilities.PersonTypes personTypes)
        {
            var price = db.PricesPerPeople.Where(a => a.Id == (int)personTypes).Select(a => a.Price).FirstOrDefault();
            return price;
        }

        public IEnumerable<ViewModelReservationDetails> GetReservationDetailsList (int? idReservation)
        {
            List<ViewModelReservationDetails> vmRD = new List<ViewModelReservationDetails>();
            var query = db.ReservationDetails.Include("Rooms").Include("Reservation").Where(a=> a.ReservationId == idReservation).ToList();
            foreach(var item in query)
            {
                vmRD.Add(new ViewModelReservationDetails
                {
                    Id = item.Id,
                    Rooms = item.Rooms,
                    ReservationId = item.ReservationId,
                    Reservation = item.Reservation,
                    ChildQty = item.ChildQty,
                    AdultQty = item.AdultQty,
                    PricePerChildren = GetPricePerPerson(Utilities.PersonTypes.Childrens),
                    PricePerAdults = GetPricePerPerson(Utilities.PersonTypes.Adults),
                    TotalForAdults = GetTotalForPerson(item.AdultQty, Utilities.PersonTypes.Adults),
                    TotalForChildren = GetTotalForPerson(item.ChildQty, Utilities.PersonTypes.Childrens),
                    Total = GetTotalForReservation(item.ReservationId, item.Reservation.CheckIn, item.Reservation.CheckOut),
                    TotalForRoom = GetTotalForRooms(item.RoomId, GetTotalForPerson(item.AdultQty, Utilities.PersonTypes.Adults), GetTotalForPerson(item.ChildQty, Utilities.PersonTypes.Childrens))

                });

            }
            return vmRD;
        }

        public bool ValidaCedula(string cedula)
        {
            var a = db.Employee.Where(b => b.Identification == cedula);
            if (a == null)
            {
                return true;
            }
            return false;
        }

        public bool ValidateOccupation(int idRoom, int personQuantity)
        {
            var room = db.Rooms.Include("BedType").Include("RoomType").Where(a => a.Id == idRoom).FirstOrDefault().BedTypeId;
            switch (room)
            {
                case 1:
                    if (personQuantity <=1)
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (personQuantity <= 2)
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (personQuantity <= 3)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;

        }

    }
}
