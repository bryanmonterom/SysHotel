using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Clients Clients { get; set; }
        public bool AllInclusive { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public ICollection<ReservationDetails> ReservationDetails { get; set; }
        public int IdEmployee { get; set; }
        public Employee Employee { get; set; }


    }
}