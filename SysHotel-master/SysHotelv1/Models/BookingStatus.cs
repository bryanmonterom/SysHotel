using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class BookingStatus
    {
        public int Id { get; set; }
        public string DescriptionStatus { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}