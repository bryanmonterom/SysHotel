using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class RoomType
    {

        
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal PricePerRoom { get; set; }         
        public ICollection <Rooms> Rooms { get; set; }
    }
}