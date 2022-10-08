using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class Rooms
    {
        public int Id { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }
        public BedType BedType { get; set; }
        public int BedTypeId { get; set; }
        public string RoomNumber { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public ICollection<ReservationDetails> ReservationDetails { get; set; }
    }
}