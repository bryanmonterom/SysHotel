using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public int CountryId {get;set;}
        public Country Country { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiredDate { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public ICollection<Reservation> Reservations { get; set; }



    }
}