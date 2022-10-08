using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class ViewModelReservationDetails
    {
        public int Id { get; set; }
        public Rooms Rooms {get;set;}
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int ChildQty { get; set; }
        public int AdultQty { get; set; }
        public decimal PricePerChildren {get;set;}
        public decimal PricePerAdults {get;set;}
        public decimal TotalForAdults{get; set;}
        public decimal TotalForChildren{get; set;}
        public decimal TotalForRoom {get; set;}
        public decimal Total {get; set;}

        }




}
