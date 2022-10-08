using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class Clients
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Identification { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int IdCountry { get; set; }
        public Country Country { get; set; }
        public string Phone { get; set; }
        public string Direccion { get; set; }

        public ICollection<Reservation> Reservation { get; set; }

        
    }
}