using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static SysHotelv1.Models.EmployeeMetaData;

namespace SysHotelv1.Models
{

    #region NotationAssign

    [MetadataType(typeof(RoomsMetadata))]
    public partial class Rooms
    {

    }

    [MetadataType(typeof(BuildingMetaData))]
    public partial class Building { }
    [MetadataType(typeof(ReservationDetailsMetadata))]
    public partial class ReservationDetails{ }

    [MetadataType(typeof(ReservationDetailsViewModelMetadata))]
    public partial class ViewModelReservationDetails { }

  
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {

    }

    [MetadataType(typeof(ClientsMetadata))]
    public partial class Clients
    {

    }

    [MetadataType(typeof(CountryMetadata))]
    public partial class Country
    {

    }

    [MetadataType(typeof(BedTypeMetadata))]
    public partial class BedType
    {

    }

    [MetadataType(typeof(CityMetadata))]
    public partial class City
    {

    }

    [MetadataType(typeof(BookingStatusMetadata))]
    public partial class BookingStatus
    {

    }

    [MetadataType(typeof(InvoicesMetadata))]
    public partial class Invoices
    {

    }

    [MetadataType(typeof(PersonTypeMetadata))]
    public partial class PersonType
    {

    }

    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation
    {

    }

    [MetadataType(typeof(RoomTypeMetadata))]
    public partial class RoomType
    {
    }

   
    
    #endregion

    #region SetUpOfNotations
    public class RoomsMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Habitacion")]
        [Required]///(ErrorMessage = "El campo de tipo de habitacion es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Solo numeros entre 0 y 100")]
        public int RoomTypeId { get; set; }

        [Display(Name = "Tipo de Cama")]
        [Required(ErrorMessage = "El campo de tipo de camas es obligatorio")]
        [Range(0, int.MaxValue)]
        public int BedTypeId { get; set; }

        [Display(Name = "Numero de Habitacion")]
        [Required(ErrorMessage = "El campo de numero de habitacion es obligatorio")]
        [MaxLength(10)]
        public string RoomNumber { get; set; }

        [Display(Name ="Edificio")]
        public int BuildingId { get; set; }
        
    }
    public  class BuildingMetaData
    {
        [Display(Name = "Nombre del Edificio")]
        public int Id { get; set; }

        [Display(Name = "Nombre del Edificio")]
        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        public string BuildingName { get; set; }

    }
    public class ClientsMetadata
    {
        public int Id { get; set; }

        [Display(Name ="Nombres")]
        [Required(ErrorMessage ="El campo Nombre es requerido")]
        public string FullName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(10)]
        [DataType(DataType.EmailAddress)]
       // [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "El correo no es valido")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Display(Name = "Pais")]
        [Required]
        public int IdCountry { get; set; }

        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Reservaciones")]
        public ICollection<Reservation> Reservation { get; set; }

        [Display(Name = "Direccion")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Display(Name = "Identificacion")]
        [StringLength(maximumLength :15,MinimumLength =11)]
        public string Identification { get; set; }

    }
    public class CountryMetadata
    {
        [Required]
        [Display(Name="Paises")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Pais")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }
        public ICollection<City> City { get; set; }
        public ICollection<Clients> Clients { get; set; }
    }
    public class BedTypeMetadata
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5,ErrorMessage ="La longitud minima es 5")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Precio/Cama")]
        [DataType(DataType.Currency)]
        public decimal PricerPerBed { get; set; }
    }
    public class CityMetadata
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Introduzca un nombre de ciudad valido")]
        [Display(Name = "Ciudades")]
        public string Name { get; set; }
        [Required]

        [Display(Name ="Paises")]
        public int CountryId { get; set; }

        [Display(Name = "Paises")]
        public Country Country { get; set; }

    }
    public class BookingStatusMetadata
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage ="La longitud maxima de este campo es 10")]
        [Display(Name ="Estatus")]
        public string DescriptionStatus { get; set; }
    }
    public class InvoicesMetadata
    {
        public int Id { get; set; }
        [Required]

        public int ReservationDetailsId { get; set; }
        public ReservationDetails ReservationDetails { get; set; }
        [Required]
        [Display(Name ="Precio/Adulto")]
        [DataType(DataType.Currency)]
        public decimal PriceAdult { get; set; }
        [Required]
        [Display(Name = "Precio/Nino")]
        [DataType(DataType.Currency)]
        public decimal PriceChild { get; set; }

        [Required]
        [Display(Name = "Cantidad de Dias")]
        public int DayCount { get; set; }

        [Required]
        [Display(Name = "Descuento por Temporada")]
        public decimal SeasonalDiscount { get; set; }

        [Required]
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public int Total { get; set; }
    }
    public class PersonTypeMetadata
    {
        [Required]
        [Display(Name ="Tipo de Persona")]
        public int Id { get; set; }

        [Required]
        [StringLength(25,ErrorMessage ="Este campo no puede tener mas de 25 caracteres")]
        [Display(Name = "Tipo de Personas")]
        public string Description { get; set; }

        [Display(Name = "Precio")]
        [Range(0,Double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

    }
    public class ReservationMetadata
    {
        [Display(Name = "Id Reservacion")]
        public int Id { get; set; }
        [Display(Name = "Cliente")]
        [Required]
        public int ClientId { get; set; }
        public Clients Clients { get; set; }
        [Required]
        [Display(Name = "Todo Incluido")]
        public bool AllInclusive { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name="CheckIn")]
        public DateTime CheckIn { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "CheckOut")]
        public DateTime CheckOut { get; set; }
        [Required]
        
        [Display(Name ="Estado")]
        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public ICollection<ReservationDetails> ReservationDetails { get; set; }
        [Display(Name ="Empleado")]
        public int IdEmployee { get; set; }


    }
    public class RoomTypeMetadata
    {

        [Display(Name = "Id Tipo Habitacion")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Habitacion")]
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Precio por Habitacion")]
        [DataType(DataType.Currency)]
        public decimal PricePerRoom { get; set; }
        public ICollection<Rooms> Rooms { get; set; }

    }
    public class ReservationDetailsMetadata
    {
        [Display(Name = "Id DetalleReservacion")]
        public int Id { get; set; }

        [Display(Name = "Id de Habitacion")]
        public int RoomId { get; set; }
        public Rooms Rooms { get; set; }

        [Display(Name = "Id de Reservacion")]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        [Required]
        [Display(Name = "Cantidad de ninos")]
        [Range(0, Double.MaxValue)]
        public int ChildQty { get; set; }

        [Required]
        [Display(Name = "Cantidad de Adultos")]
        [Range(0, Double.MaxValue)]
        public int AdultQty { get; set; }
        public ICollection<Invoices> Invoices { get; set; }


        
    }
    public class EmployeeMetaData
    {
        [Required]
        [Display(Name = "Empleados")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }


        [Required]
        [Display(Name = "Posicion")]
        public string JobTitle { get; set; }


        [Required]
        [Display(Name = "Paises")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [Required]
        [Display(Name = "Fecha de Despido")]
        [DataType(DataType.Date)]
        public DateTime HiredDate { get; set; }


        [Required]
        [Display(Name = "Identificacion")]
        [MaxLength(15)]
        public string Identification { get; set; }


        [Required]
        [Display(Name = "Direccion")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

    }
    public class ReservationDetailsViewModelMetadata
        {
        [Display(Name ="Id")]
        public int Id { get; set; }
        public Rooms Rooms { get; set; }
        [Display(Name ="Id de Reservacion")]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        [Display(Name ="Cantidad [N]")]
        public int ChildQty { get; set; }
        [Display(Name = "Cantidad [A]")]
        public int AdultQty { get; set; }
        [Display(Name = "Precios [N]")]
        [DataType(DataType.Currency)]
        public decimal PricePerChildren { get; set; }
        [Display(Name = "Precios [A]")]
        [DataType(DataType.Currency)]
        public decimal PricePerAdults { get; set; }
        [Display(Name = "Total [A]")]
        [DataType(DataType.Currency)]
        public decimal TotalForAdults { get; set; }
        [Display(Name = "Total [N]")]
        [DataType(DataType.Currency)]
        public decimal TotalForChildren { get; set; }
        [Display(Name ="Total [H]")]
        [DataType(DataType.Currency)]
        public decimal TotalForRoom { get; set; }
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
            public decimal Total { get; set; }
        }

    public class BuildingsMetadata
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Edificio")]
        public string BuildingName { get; set; }
    }
    }



    #endregion



