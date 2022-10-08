using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class Building
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public ICollection<Rooms> Rooms { get; set;}

    }
}