using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class ReservationDetails
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Rooms Rooms {get;set;}
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int ChildQty { get; set; }
        public int AdultQty { get; set; }
        public ICollection<Invoices> Invoices { get; set; }
        

        }




    }
