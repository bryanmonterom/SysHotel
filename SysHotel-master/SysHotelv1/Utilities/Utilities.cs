using SysHotelv1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SysHotelv1.Utilities
{

 public enum ErrorsCode
    {
        ItemDoesNotExist,
        ItemAlreadyExists,
        AnErrorOcurred,
        CantDelete,
        NotFound,
        NoError,
        CustomError

    }
    public enum NotificationType
    {
        error,
        success,
        warning,
        info
    }
    
      public enum PersonTypes
        {
            Adults = 1,
            Childrens=2
        }

    public enum BedTypes
    {
        Simple = 1,
        Doble =2,
        Triple = 3

    }

   
    public static class Utilities
    {
       public static SysHotelDataContext db = new SysHotelDataContext();
        public static SysHotelEntities db1 = new SysHotelEntities();
        static SysHotelBusiness business = new SysHotelBusiness();
        public static SelectList GetClients()
        {
            var reservations = db.Reservations.AsNoTracking().OrderBy(a => a.Clients.FullName).
                Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Clients.FullName + " [" + a.Id + "]"

                }).ToList();
            reservations.Insert(0, FirstItem());
            return new SelectList(reservations, "Value", "Text");
        }
        public static SelectListItem FirstItem()
        {
            var firstItem = new SelectListItem
            {
                Text = "Seleccione un valor",
                Value = null
            };
            return firstItem;
        }
        public static SelectList GetBedTypes()
        {
            var bedTypes = db.BedType.AsNoTracking().OrderBy(a => a.Description).Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }).ToList();
            bedTypes.Insert(0, FirstItem());
            return new SelectList(bedTypes, "Value", "Text");
        }
        public static SelectList GetRooms(DateTime entranceDate, DateTime outDate)
        {
            var rooms = business.GetRoomsAvailable(entranceDate,outDate).Select(
                a => new SelectListItem
                {
                    Text = a.RoomNumber + " " + a.RoomType.Description + " "+ a.BedType.Description,
                    Value = a.Id.ToString()
                }).ToList();
            rooms.Insert(0, FirstItem());
            SelectList s1 = new SelectList(rooms, "Value", "Text");
            return s1;
        }

        public static SelectList GetRoles()
        {
            var roles = db1.AspNetRoles.AsNoTracking().OrderBy(a => a.Name).Select
                (a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }).ToList();

            roles.Insert(0, FirstItem());
            SelectList s1 = new SelectList(roles, "Value", "Text");
            return s1;

        }
        public static SelectList GetBuildings()
        {
            var building = db.Building.AsNoTracking().OrderBy(a => a.BuildingName).
                Select(a => new SelectListItem
                {
                    Text = a.BuildingName,
                    Value = a.Id.ToString()
                }).ToList();

            building.Insert(0, FirstItem());
            SelectList s1 = new SelectList(building, "Value", "Text");
            return s1;
        }
        public static SelectList GetRooms()
        {
            var rooms = db.Rooms.AsNoTracking().Select(
                a => new SelectListItem
                {
                    Text = a.RoomNumber + " " + a.RoomType.Description,
                    Value = a.Id.ToString()
                }).ToList();
            rooms.Insert(0, FirstItem());
            SelectList s1 = new SelectList(rooms, "Value", "Text");
            return s1;
        }
        //public static string ActionToString(AuditTrailAction Action)
        //{
        //    switch (Action)
        //    {
        //        case AuditTrailAction.Insert:
        //            return "I";
        //        case AuditTrailAction.Update:
        //            return "U";
        //        case AuditTrailAction.Delete:
        //            return "D";
        //        case AuditTrailAction.System:
        //            return "S";
        //        default:
        //            return "E";
        //    }
        //}

        public static string ErrorHandling( string model ="", string message="", string parameterName="" , ErrorsCode error = ErrorsCode.NoError )
        {
            string ErrorMessage = "";
            switch (error)
            {
                case ErrorsCode.ItemAlreadyExists:
                    ErrorMessage = string.Format("Ya existe un {0} con est@ {1} {2} ", model, parameterName, message);
                    break;
                case ErrorsCode.AnErrorOcurred:
                    ErrorMessage = string.Format("Ha ocurrido un error {0}, comunicate con el departamento de IT", message);
                    break;
                case ErrorsCode.CantDelete:
                    ErrorMessage = string.Format("No puedes eliminar este {0}, existe algunos otros vinculados al mismo", model);
                    break;
                case ErrorsCode.NotFound:
                    ErrorMessage = string.Format("No se encontraron {0}, con estas condiciones", model);
                    break;
                case ErrorsCode.ItemDoesNotExist:
                    ErrorMessage = string.Format("No se encontraron {0}, con estas condiciones", model);
                    break;
                case ErrorsCode.CustomError:
                    ErrorMessage = string.Format("{0}",message);
                    break;
                default:
                    ErrorMessage = string.Format("La operacion se ha completado con exito!");
                    break;
            }
            return ErrorMessage;
        }

      
    }
}
