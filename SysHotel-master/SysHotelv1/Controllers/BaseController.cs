using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysHotelv1.Utilities;

namespace SysHotelv1.Controllers
{
    public abstract class BaseController : Controller
    {

        public string Alert(string message, NotificationType notificationType)
        {
            string msg = "<script language='javascript'>swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "</script>";
            ViewBag.notification = msg;
            return msg;
        }

    }
}
