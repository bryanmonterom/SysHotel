using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public ICollection<City> City { get; set; }
        public ICollection<Clients> Clients { get; set; }
        public ICollection<Employee> Employee { get; set; }

    }
}