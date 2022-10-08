using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysHotelv1.Models
{
    public class UserRoleViewModel
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public string RoleName { get; set; }

        public int RoleId { get; set; }
        public ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}