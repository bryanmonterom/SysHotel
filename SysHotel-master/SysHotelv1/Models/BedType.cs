using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class BedType
    {
       public int Id { get; set; }
       public string Description { get; set; }
       public decimal PricerPerBed { get; set; }
       public ICollection<Rooms> Rooms { get; set; }


    }
}